// =====================================================================
// LP-005 — le ramassage automatique des pièces et des cristaux d'expérience.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-auto-loot).
// Dans le jeu de 1998, il faut marcher sur le butin. Ici, quand la collision d'un membre ne désigne
// rien de ramassable, le tweak lui désigne un butin POSÉ au sol ; c'est ensuite le code d'origine
// (`PartyMemberStep`) qui donne l'expérience partagée ou l'argent, joue le son et rend l'emplacement.
// Aucune règle de récompense n'est réécrite ici.
//
// Le cristal : n'importe quel membre debout — son expérience est partagée par le code d'origine.
// Les pièces : À TOUR DE RÔLE entre les membres debout, de 1 à 4 selon l'équipe, pour que les
// bourses montent ensemble (design D3).
//
// Les coffres (bit 0x8000) ne sont jamais désignés. Ne tire aucun nombre aléatoire.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class RamassageAuto
{
    private const int Emplacements = 0x40;
    private const int Taille = 0x9c;
    private const int MembresMax = 4;

    /// <summary>Actif (<c>0x80000000</c>), ramassable (<c>0x80000</c>), coffre (<c>0x8000</c>).</summary>
    private const uint MasqueButin = 0x80088000;
    private const uint PiecesOuCristal = 0x80080000;

    /// <summary>Le bit qui fait d'un butin des pièces plutôt que le cristal d'expérience.</summary>
    private const uint Pieces = 0x10000000;

    /// <summary>
    /// Sous cette durée de vie restante (<c>+0x10</c>, 200 à 229 images à la naissance), une pièce
    /// est à qui la prend : filet si le membre dont c'est le tour ne passe jamais par le ramassage.
    /// </summary>
    private const short SeuilAbandon = 60;

    // L'image où chaque membre a été vu debout pour la dernière fois (-1 : jamais).
    private static readonly int[] s_VuDebout = [-1, -1, -1, -1];
    private static int s_Prochain;          // le membre à qui revient la prochaine pièce
    private static int s_Image;

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.RamassageAuto) return;

        Action image = Coutures.ImageSuivante;
        Coutures.ImageSuivante = () => { image(); s_Image++; };

        Coutures.Contact precedent = Coutures.ButinAtteint;
        Coutures.ButinAtteint = (membre, touche, poolButin) =>
        {
            touche = precedent(membre, touche, poolButin);

            int moi = ((byte*)membre)[8];
            bool debout = (*membre & 0x80000000) != 0 && (*membre & 0x50000000) == 0;   // le « debout » du partage d'expérience
            if (debout && moi < MembresMax) s_VuDebout[moi] = s_Image;

            if (touche != null && Ramassable(touche)) return touche;
            if (!debout || moi >= MembresMax) return touche;

            for (int i = 0; i < Emplacements; i++)
            {
                byte* butin = poolButin + i * Taille;
                if (!Ramassable(butin)) continue;
                if (*(int*)(butin + 0x54) != 0) continue;                   // rebondit encore

                if ((*(uint*)butin & Pieces) == 0) return butin;             // le cristal : à tous

                bool monTour = moi == Destinataire();
                bool abandonnee = *(short*)(butin + 0x10) < SeuilAbandon;
                if (!monTour && !abandonnee) continue;

                s_Prochain = (moi + 1) % MembresMax;
                return butin;
            }

            return touche;
        };
    }

    /// <summary>
    /// Le premier membre vu debout à cette image ou à la précédente, en partant de
    /// <see cref="s_Prochain"/> et en tournant. Les membres passent dans l'ordre 0..3 à chaque
    /// image : « la précédente » couvre ceux qui ne sont pas encore passés à celle-ci.
    /// </summary>
    private static int Destinataire()
    {
        for (int k = 0; k < MembresMax; k++)
        {
            int m = (s_Prochain + k) % MembresMax;
            if (s_VuDebout[m] >= s_Image - 1) return m;
        }
        return -1;
    }

    private static bool Ramassable(byte* entite) => (*(uint*)entite & MasqueButin) == PiecesOuCristal;
}
