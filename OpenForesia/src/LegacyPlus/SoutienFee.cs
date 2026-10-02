// =====================================================================
// LP-015 — la Fée en soutien du groupe.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-10-02-add-legacy-plus-fairy-support).
// Dans le jeu de 1998, les sorts de l'école 2 ne touchent qu'une cible : l'allié devant la Fée (ou
// elle-même) pour un bonus, la victime qu'une bulle lente finit par toucher pour un malus — et le
// jet de sauvegarde ou l'immunité d'espèce le font souvent échouer. Avec `[Magie] SoutienFee` :
//
//   - un BONUS (Vitesse, Matéria ×4, Coussin d'air, Superman) est COPIÉ, à sa première image, une
//     fois par autre membre vivant : chaque copie joue son corps — l'animation comprise — sur sa
//     cible et y pose l'effet. C'est ce que Turbo fait de Vitesse dans le jeu d'origine ;
//   - Gros sabots est copié de même, une fois par ennemi à moins de 0x5dc de la Fée : un sabot
//     tombe sur chacun ;
//   - un MALUS passe toujours (ni jet ni immunité, boss compris) ; Ralenti, au contact, part en
//     plus sur chaque ennemi à moins de 0x5dc ; Dodo, Chant sirènes et Écrase n'agissent que sur
//     ce que chacune de leurs bulles touche ;
//   - Dodo, Chant sirènes et Écrase lancent quatre bulles, une par quart de tour ;
//   - Vitesse et Gros sabots, dont Turbo et Ralenti sont les versions de groupe, durent moitié.
//
// Les précédents du binaire : Coup de pouce pour le balayage du groupe, Zombie pour celui de la
// scène, Turbo pour un bonus servi à tous. Aucun tirage aléatoire ici : le jet évité n'est pas tiré.
// =====================================================================

using Jeu = OpenForesia.Blaze.Blaze;
using Winsys3d = OpenForesia.Blaze.Winsys3d;

namespace OpenForesia.LegacyPlus;

internal static unsafe class SoutienFee
{
    private const uint MembreDuGroupe = 0x800000;
    private const uint EmplacementOccupe = 0x80000000;
    private const int TailleEntite = 0x9c;

    /// <summary>La classe 7, lue dans la fiche de personnage (<c>+0x150 &amp; 7</c>).</summary>
    private const int ClasseFee = 7;

    /// <summary>L'école de la Fée : la famille de la paire <c>+0x06</c>/<c>+0x07</c>.</summary>
    private const byte EcoleFee = 2;

    /// <summary>La table des entités de scène, celle que Zombie balaye (design D4).</summary>
    private const int PoolScene = 0x005ecaa0, PlacesScene = 0x20;

    /// <summary>
    /// La table des sorts, celle où <c>CastSpell_0053A980</c> fait naître l'entité du sort
    /// (<c>SpawnEffectEntity</c>, Ghidra: FUN_0047a9d0) : 0x20 emplacements, liste libre en tête.
    /// </summary>
    private const int PoolSorts = 0x005ff400, ListeLibreSorts = 0x005fa5a0;

    /// <summary>La portée de Zombie, en unités du monde décalées de 12 bits.</summary>
    private const int Portee = 0x5dc;

    // Les indices des fiches de l'école 2 (Bestiaire/sorts.csv).
    private const byte Dodo = 0, GrosSabots = 1, Vitesse = 2, ChantSirenes = 7, Ecrase = 8, Ralenti = 12;

    /// <summary>Vitesse, les quatre Matéria, Coussin d'air, Superman. Pas Turbo (13) : déjà de groupe.</summary>
    private static bool EstBonus(byte indice) => indice is 2 or 3 or 4 or 5 or 6 or 10 or 14;

    private static bool EstMalus(byte indice) => indice is Dodo or GrosSabots or ChantSirenes or Ecrase or Ralenti;

    /// <summary>
    /// Les malus qui, au contact, partent aussi sur les ennemis proches : Ralenti seul. Les trois
    /// sorts à bulles (Dodo, Chant sirènes, Écrase) en lancent quatre, et chacune n'agit que sur ce
    /// qu'elle touche (retour d'essai du 2026-10-02).
    /// </summary>
    private static bool SeDiffuseAuContact(byte indice) => indice is Ralenti;

    /// <summary>Le contexte de la pose en cours (design D2) : le jeu est mono-fil.</summary>
    private static bool s_MalusForce;
    private static bool s_DureeDivisee;

    /// <summary>Une copie de sort qui n'a pas encore choisi sa cible : celle qu'on lui a réservée.</summary>
    private readonly record struct CibleReservee(nint Cible, nint Lanceur, byte SousType);

    /// <summary>Par emplacement de copie : la cible qu'elle prendra à sa première image.</summary>
    private static readonly Dictionary<nint, CibleReservee> s_Reservees = new();

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.SoutienFee) return;

        Coutures.PoseDEffet posePrecedente = Coutures.EffetDeFee;
        Coutures.EffetDeFee = (lanceur, victime, effet) =>
        {
            if (!EstLaFee((byte*)lanceur, ((byte*)lanceur)[7])) return posePrecedente(lanceur, victime, effet);

            byte indice = ((byte*)lanceur)[6];

            s_MalusForce = EstMalus(indice);
            s_DureeDivisee = indice is Vitesse or GrosSabots;
            try
            {
                uint resultat = posePrecedente(lanceur, victime, effet);
                if (SeDiffuseAuContact(indice)) DiffuserAuxEnnemis(lanceur, victime, effet);
                return resultat;
            }
            finally
            {
                s_MalusForce = false;
                s_DureeDivisee = false;
            }
        };

        Func<bool> forcePrecedent = Coutures.MalusForce;
        Coutures.MalusForce = () => s_MalusForce || forcePrecedent();

        Func<int, int> dureePrecedente = Coutures.DureeDEtat;
        Coutures.DureeDEtat = duree =>
        {
            duree = dureePrecedente(duree);
            return s_DureeDivisee ? duree / 2 : duree;
        };

        Coutures.Cible ciblePrecedente = Coutures.CibleDeFee;
        Coutures.CibleDeFee = (sort, cible) =>
        {
            cible = ciblePrecedente(sort, cible);
            byte* lanceur = *(byte**)(sort + 0x74);

            // Une copie : elle prend la cible qu'on lui a réservée, si l'emplacement est bien
            // encore le sien (même lanceur, même sous-type) et la cible encore là.
            if (s_Reservees.Remove((nint)sort, out CibleReservee reservee))
            {
                if (reservee.Lanceur == (nint)lanceur && reservee.SousType == sort[5]
                    && EstVivante((byte*)reservee.Cible))
                {
                    return (byte*)reservee.Cible;
                }
                return cible;
            }

            if (lanceur == null || !EstLaFee(lanceur, sort[7])) return cible;

            byte indice = sort[6];
            if (EstBonus(indice))
            {
                CopierPourLeGroupe(sort, lanceur, cible);
            }
            else if (indice == GrosSabots && cible != null)
            {
                CopierPourLesEnnemis(sort, lanceur, cible);
            }
            return cible;
        };

        Coutures.Projectile projectilePrecedent = Coutures.ProjectileDeFee;
        Coutures.ProjectileDeFee = (graine, engendrer) =>
        {
            byte* lanceur = *(byte**)(graine + 0x74);
            if (lanceur == null || !EstLaFee(lanceur, graine[7]))
            {
                projectilePrecedent(graine, engendrer);
                return;
            }

            // Quatre bulles, un quart de tour (0x400 sur 0x1000) entre chacune ; l'enfant recopie
            // la graine, cap +0x32 compris, puis cherche sa cible dans son propre cône.
            short cap = *(short*)(graine + 0x32);
            for (int quart = 0; quart < 4; quart++)
            {
                *(short*)(graine + 0x32) = (short)((cap + quart * 0x400) & 0xfff);
                projectilePrecedent(graine, engendrer);
            }
            *(short*)(graine + 0x32) = cap;
        };

        Console.Error.WriteLine("[LEGACY++] LP-015 SoutienFee installé");
    }

    /// <summary>
    /// Le lanceur est un membre du groupe de classe Fée, et la famille de la paire est l'école 2
    /// (design D1).
    /// </summary>
    private static bool EstLaFee(byte* lanceur, byte famille)
    {
        if (famille != EcoleFee) return false;
        if ((*(uint*)lanceur & MembreDuGroupe) == 0) return false;

        byte* fiche = Jeu.GetMainMemPtr() + 0xf0000 + lanceur[8] * 0x2000;
        return (fiche[0x150] & 7) == ClasseFee;
    }

    private static bool EstVivante(byte* entite) =>
        entite != null
        && (*(uint*)entite & EmplacementOccupe) != 0
        && (*(uint*)entite & 0x50000000) == 0;

    /// <summary>
    /// Fait naître dans la table des sorts une copie exacte de <paramref name="sort"/> (les
    /// <c>0x9c</c> octets, sauf son propre indice) qui, à sa première image, prendra
    /// <paramref name="cible"/>. Table pleine : rien, comme un engendrement d'origine qui échoue.
    /// </summary>
    private static void Copier(byte* sort, byte* lanceur, byte* cible)
    {
        int indice = Jeu.AllocEntitySlot((ushort*)Jeu.Mem.Entity(ListeLibreSorts));
        if (indice < 0) return;

        byte* copie = Jeu.Mem.Entity(PoolSorts) + indice * TailleEntite;
        Buffer.MemoryCopy(sort, copie, TailleEntite, TailleEntite);
        copie[0x08] = (byte)indice;

        s_Reservees[(nint)copie] = new CibleReservee((nint)cible, (nint)lanceur, sort[5]);
    }

    /// <summary>Une copie par autre membre vivant du groupe — filtre de Coup de pouce, sans distance.</summary>
    private static void CopierPourLeGroupe(byte* sort, byte* lanceur, byte* cible)
    {
        byte* membre = Jeu.Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, membre += TailleEntite)
        {
            if (membre == cible) continue;

            uint drapeaux = *(uint*)membre;
            if ((drapeaux & EmplacementOccupe) == 0 || (drapeaux & 0x50000000) != 0) continue;

            Copier(sort, lanceur, membre);
        }
    }

    /// <summary>Une copie par autre ennemi à portée de la Fée.</summary>
    private static void CopierPourLesEnnemis(byte* sort, byte* lanceur, byte* cible)
    {
        byte* candidate = Jeu.Mem.Entity(PoolScene);
        for (int i = 0; i < PlacesScene; i++, candidate += TailleEntite)
        {
            if (candidate == cible || !EstEnnemiAPortee(lanceur, candidate)) continue;
            Copier(sort, lanceur, candidate);
        }
    }

    /// <summary>
    /// Chaque autre ennemi à portée de la Fée reçoit l'effet sans mise en scène (Ralenti).
    /// </summary>
    private static void DiffuserAuxEnnemis(
        uint* lanceur, uint* victime, delegate*<uint*, uint*, int, uint> effet)
    {
        byte* candidate = Jeu.Mem.Entity(PoolScene);
        for (int i = 0; i < PlacesScene; i++, candidate += TailleEntite)
        {
            if ((uint*)candidate == victime || !EstEnnemiAPortee((byte*)lanceur, candidate)) continue;
            effet(lanceur, (uint*)candidate, 0);
        }
    }

    /// <summary>
    /// Une entité de scène vivante à moins de <see cref="Portee"/> du lanceur : filtre de Zombie
    /// sans son critère d'espèce ni son <c>+0x74</c>, distance mesurée comme lui.
    /// </summary>
    private static bool EstEnnemiAPortee(byte* lanceur, byte* candidate)
    {
        uint drapeaux = *(uint*)candidate;
        if ((drapeaux & EmplacementOccupe) == 0 || (drapeaux & 0x40002000) != 0) return false;

        int dx = (*(int*)(candidate + 0x1c) - *(int*)(lanceur + 0x1c)) >> 0xc;
        int dy = (*(int*)(candidate + 0x20) - *(int*)(lanceur + 0x20)) >> 0xc;
        int dz = (*(int*)(candidate + 0x24) - *(int*)(lanceur + 0x24)) >> 0xc;
        return (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) < Portee;
    }
}
