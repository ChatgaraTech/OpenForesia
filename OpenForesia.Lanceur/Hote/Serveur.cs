// L'hote : les invites, leurs manettes, le premier plan, et l'envoi de l'image et du son
// (taches 1.5, 1.6, 2.3, 3.1, 4.3, 4.4).
//
// Un seul port UDP, chaque paquet chiffre par la cle de session (Chiffrement) : un paquet qui ne
// s'ouvre pas est ignore sans reponse. Un invite dit Bonjour et recoit le plus petit joueur libre
// parmi 2, 3 et 4, ou 0 si les trois sont pris. Il est reconnu a son identifiant, pas a son
// adresse : s'il revient d'ailleurs (sa connexion a change), il retrouve son joueur. Silencieux
// 500 ms, ses touches sont relachees ; silencieux 30 s, ou sur AuRevoir, son joueur est libere.
// Tant qu'aucune fenetre du jeu n'est au premier plan, toutes les touches sont relachees, les
// manettes ignorees, et les invites prevenus (Pause).
//
// Trois fils envoient (celui-ci, l'image, le son) : un seul compteur de paquets, atomique, pour que
// le nonce du chiffrement ne se repete jamais.

using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using OpenForesia.Lanceur.Commun;

namespace OpenForesia.Lanceur.Hote;

internal sealed class Serveur : IDisposable
{
    private const long SilenceRelache = 500, SilenceDepart = 30_000;
    private const int Pas = 4;                                    // ms entre deux tours

    private sealed class Invite
    {
        public required EndPoint Adresse;
        public required uint Id;
        public required int Joueur;
        public long DernierVu;
        public uint DernierNumero;
        public ushort Mot, Applique;
    }

    private readonly Socket _socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private readonly Chiffrement _chiffrement;
    private readonly Invite?[] _invites = new Invite?[3];          // joueurs 2, 3, 4
    private readonly byte[] _envoi = new byte[Protocole.TailleMax];
    private readonly byte[] _envoiImage = new byte[Protocole.TailleMax];
    private readonly byte[] _envoiSon = new byte[Protocole.TailleMax];
    private readonly byte[] _reception = new byte[Protocole.TailleMax];
    private readonly byte[] _clair = new byte[Protocole.TailleMax];
    private readonly Lock _verrou = new();                         // les invités : lus aussi par les fils de diffusion
    private int _numero;                                           // tous les paquets de l'hôte, tous fils confondus
    private uint _numeroImage, _numeroSon;
    private bool _pause = true;
    private int _demandeCle;

    internal Serveur(int port, byte[] cle)
    {
        _chiffrement = new Chiffrement(cle);
        _socket.Bind(new IPEndPoint(IPAddress.Any, port));
        _socket.SendBufferSize = 1 << 20;                          // une image clé part en rafale de morceaux
        // Sans cela, un ICMP « port injoignable » d'un invite parti fait lever la réception suivante.
        const int SioUdpConnreset = unchecked((int)0x9800000C);
        _socket.IOControl(SioUdpConnreset, [0, 0, 0, 0], null);
    }

    private uint Numero() => (uint)Interlocked.Increment(ref _numero);

    /// <summary>Tourne jusqu'à ce que <paramref name="continuer"/> rende faux ; <paramref name="premierPlan"/> dit si le jeu reçoit les touches.</summary>
    internal void Executer(Func<bool> continuer, Func<bool> premierPlan)
    {
        long prochainTour = 0;
        while (continuer())
        {
            if (_socket.Poll(Pas * 1000, SelectMode.SelectRead)) Recevoir();

            long maintenant = Environment.TickCount64;
            if (maintenant < prochainTour) continue;
            prochainTour = maintenant + Pas;
            bool devant = premierPlan();
            lock (_verrou) Tour(maintenant, devant);
        }
        lock (_verrou) RelacherTout();
    }

    // ---- l'image et le son, appelés par leurs fils ------------------------------------------------------

    /// <summary>Au moins un invité est là : l'image vaut d'être encodée.</summary>
    internal bool AUnInvite
    {
        get { lock (_verrou) return Array.Exists(_invites, i => i != null); }
    }

    /// <summary>Les joueurs (2 à 4) tenus par un invité en ce moment.</summary>
    internal int[] Joueurs
    {
        get { lock (_verrou) return _invites.Where(i => i != null).Select(i => i!.Joueur).ToArray(); }
    }

    /// <summary>La prochaine image encodée sera une image clé (un invité arrive, ou en a perdu une).</summary>
    internal void DemanderCle() => Interlocked.Exchange(ref _demandeCle, 1);

    internal bool PrendreDemandeCle() => Interlocked.Exchange(ref _demandeCle, 0) != 0;

    private EndPoint[] Destinataires()
    {
        lock (_verrou) return _invites.Where(i => i != null).Select(i => i!.Adresse).ToArray();
    }

    /// <summary>Une image H.264, coupée en morceaux et envoyée à chaque invité — le même encodage pour tous.</summary>
    internal void DiffuserImage(byte[] h264, bool cle)
    {
        EndPoint[] destinataires = Destinataires();
        if (destinataires.Length == 0) return;
        Video.Morceaux.Decouper(h264, ++_numeroImage, cle, _envoiImage, Numero, morceau => EnvoyerATous(destinataires, morceau.Span));
    }

    /// <summary>5 ms de son, envoyés à chaque invité.</summary>
    internal void DiffuserSon(ReadOnlySpan<byte> pcm)
    {
        EndPoint[] destinataires = Destinataires();
        if (destinataires.Length == 0) return;
        Span<byte> t = _envoiSon.AsSpan(0, Protocole.Entete + 4 + pcm.Length);
        Protocole.EcrireEntete(t, Genre.Son, Numero());
        BinaryPrimitives.WriteUInt32LittleEndian(t[Protocole.Entete..], ++_numeroSon);
        pcm.CopyTo(t[(Protocole.Entete + 4)..]);
        EnvoyerATous(destinataires, t);
    }

    // ---- la réception --------------------------------------------------------------------------------

    private void Recevoir()
    {
        EndPoint de = new IPEndPoint(IPAddress.Any, 0);
        int n;
        try { n = _socket.ReceiveFrom(_reception, ref de); }
        catch (SocketException) { return; }
        if (!_chiffrement.Ouvrir(_reception.AsSpan(0, n), _clair, out int longueur, out uint emetteur)) return;
        if (!Protocole.Lire(_clair.AsSpan(0, longueur), out var p)) return;
        // Un invité ne parle qu'en son nom : l'identifiant du contenu est celui qui a scellé le paquet.
        if (BinaryPrimitives.ReadUInt32LittleEndian(p.Contenu) != emetteur || emetteur == Chiffrement.Hote) return;
        lock (_verrou) Traiter(p, emetteur, de);
    }

    private void Traiter(Protocole.Paquet p, uint id, EndPoint de)
    {
        Invite? invite = Array.Find(_invites, i => i != null && i.Id == id);
        if (invite != null && !invite.Adresse.Equals(de))
        {
            // Le même invité, revenu d'ailleurs : sa connexion a changé (tâche 4.4).
            Journal($"joueur {invite.Joueur} revient de {de} (était {invite.Adresse})");
            invite.Adresse = de;
            DemanderCle();
        }

        switch (p.Genre)
        {
            case Genre.Bonjour:
                if (invite == null)
                {
                    int libre = Array.IndexOf(_invites, null);
                    if (libre < 0)
                    {
                        EnvoyerAccueil(de, id, 0);
                        Journal($"{de} refusé : la partie est complète");
                        return;
                    }
                    invite = _invites[libre] = new Invite { Adresse = de, Id = id, Joueur = libre + 2 };
                    DemanderCle();                                  // il ne décode rien avant une image clé
                    Journal($"{de} rejoint : joueur {invite.Joueur}");
                }
                invite.DernierVu = Environment.TickCount64;
                EnvoyerAccueil(de, id, (byte)invite.Joueur);
                EnvoyerPause(invite);
                break;

            case Genre.Manette when invite != null:
                invite.DernierVu = Environment.TickCount64;
                if (p.Numero <= invite.DernierNumero && invite.DernierNumero - p.Numero < 1u << 31) return;   // en retard, ou rejoué
                invite.DernierNumero = p.Numero;
                invite.Mot = (ushort)(BinaryPrimitives.ReadUInt16LittleEndian(p.Contenu[4..]) & Pad.MasqueValide);
                break;

            case Genre.Presence when invite != null:
                invite.DernierVu = Environment.TickCount64;
                Span<byte> echo = _envoi.AsSpan(0, Protocole.Entete + 12);
                Protocole.EcrireEntete(echo, Genre.Echo, Numero());
                p.Contenu[..12].CopyTo(echo[Protocole.Entete..]);
                Envoyer(de, echo);
                break;

            case Genre.AuRevoir when invite != null:
                Liberer(invite, "au revoir");
                break;

            case Genre.ImageCle when invite != null:
                invite.DernierVu = Environment.TickCount64;
                DemanderCle();
                break;
        }
    }

    private void Tour(long maintenant, bool premierPlan)
    {
        if (premierPlan == _pause)
        {
            _pause = !premierPlan;
            Journal(_pause ? "le jeu n'est plus au premier plan : entrées des invités en pause" : "le jeu est au premier plan : entrées des invités actives");
            foreach (Invite? i in _invites) if (i != null) EnvoyerPause(i);
        }

        foreach (Invite? i in _invites)
        {
            if (i == null) continue;
            long silence = maintenant - i.DernierVu;
            if (silence > SilenceDepart) { Liberer(i, "silence de 30 s"); continue; }
            ushort cible = _pause || silence > SilenceRelache ? (ushort)0 : i.Mot;
            i.Applique = Injection.Appliquer(i.Joueur, i.Applique, cible);
        }
    }

    private void Liberer(Invite invite, string pourquoi)
    {
        invite.Applique = Injection.Appliquer(invite.Joueur, invite.Applique, 0);
        _invites[invite.Joueur - 2] = null;
        Journal($"joueur {invite.Joueur} libéré ({invite.Adresse}, {pourquoi})");
    }

    private void RelacherTout()
    {
        foreach (Invite? i in _invites)
            if (i != null) i.Applique = Injection.Appliquer(i.Joueur, i.Applique, 0);
    }

    private void EnvoyerAccueil(EndPoint a, uint id, byte joueur)
    {
        Span<byte> t = _envoi.AsSpan(0, Protocole.Entete + 5);
        Protocole.EcrireEntete(t, Genre.Accueil, Numero());
        BinaryPrimitives.WriteUInt32LittleEndian(t[Protocole.Entete..], id);
        t[Protocole.Entete + 4] = joueur;
        Envoyer(a, t);
    }

    private void EnvoyerPause(Invite i)
    {
        Span<byte> t = _envoi.AsSpan(0, Protocole.Entete + 1);
        Protocole.EcrireEntete(t, Genre.Pause, Numero());
        t[Protocole.Entete] = (byte)(_pause ? 1 : 0);
        Envoyer(i.Adresse, t);
    }

    private void Envoyer(EndPoint a, ReadOnlySpan<byte> clair) => EnvoyerATous([a], clair);

    /// <summary>Scelle le paquet une fois, et l'envoie à chacun.</summary>
    private void EnvoyerATous(EndPoint[] destinataires, ReadOnlySpan<byte> clair)
    {
        Span<byte> scelle = stackalloc byte[clair.Length + Chiffrement.Surcout];
        int n = _chiffrement.Sceller(clair, Chiffrement.Hote, scelle);
        foreach (EndPoint a in destinataires)
        {
            try { _socket.SendTo(scelle[..n], SocketFlags.None, a); }
            catch (SocketException e) { Journal($"envoi vers {a} impossible : {e.SocketErrorCode}"); }
        }
    }

    private static void Journal(string message) => Console.Out.WriteLine($"[HÔTE] {message}");

    public void Dispose()
    {
        _socket.Dispose();
        _chiffrement.Dispose();
    }
}
