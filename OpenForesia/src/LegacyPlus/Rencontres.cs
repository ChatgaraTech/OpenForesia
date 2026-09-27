// =====================================================================
// LP-007 — les rencontres aléatoires : cadence, chevauchement, nombre de formations.
//
// Hors reconstruction (legacy ++, openspec/changes/add-legacy-plus-encounters).
// Dans le jeu de 1998, `TrySpawnRandomEncounter` ne fait naître une formation que si le minuteur est
// à zéro ET qu'aucune entité de scène ne vit — il faut tout tuer avant la suivante —, puis attend
// 200 à 300 images. Ici :
//   - Frequence   divise chaque délai que le jeu tire (couture DelaiRencontre) ;
//   - MonstresMax laisse une rencontre arriver pendant un combat, sous un plafond d'entités ;
//   - Formations  rappelle la rencontre après une rencontre réussie, pour en faire naître plusieurs.
//
// Les rappels passent par le code d'origine : tirage de la formation parmi les huit de la
// sous-zone, placement « tout ou rien », naissance, son. Le tweak lève seulement, le temps d'un
// rappel, les deux gardes « minuteur » et « entité vivante ». Il ne tire aucun nombre aléatoire
// lui-même ; les rappels, eux, font tirer le jeu — c'est inhérent à « plus de monstres ».
// Les monstres de script (opcode 13), les boss et les scènes ne passent pas par ici.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class Rencontres
{
    /// <summary>
    /// Le plafond ABSOLU des entités de scène vivantes au-delà duquel le tweak ne fait plus rien
    /// naître — ni chevauchement ni formation supplémentaire. Au-delà, le budget de 2 500
    /// primitives par image du moteur de 1998 (`s_Unknown_005e9818`, posé par `FrameBegin`)
    /// s'épuise et des morceaux de la carte ne sont plus dessinés. Fixé à 12 par Vincent le
    /// 2026-09-27 ; relever la limite de rendu est une préconisation notée dans
    /// `Documentation/LegacyPlus/legacy.md`. Un groupe commencé sous le plafond peut le dépasser
    /// de quelques monstres : la naissance d'une formation ne s'interrompt pas.
    /// </summary>
    internal const int LimiteRendu = 12;

    /// <summary>Le délai moyen d'origine entre deux rencontres, (rand()%6 + 10) × 20.</summary>
    private const int DelaiMoyen = 250;

    /// <summary>Le nouvel essai quand une rencontre de chevauchement n'a pu se placer.</summary>
    private const int DelaiReessai = 20;

    private static bool s_Force;           // un rappel du tweak est en cours : gardes levées
    private static int s_Chevauchement;    // le minuteur propre au chevauchement

    internal static void Installer()
    {
        int frequence = ReglagesLegacyPlus.FrequenceRencontres;      // centièmes
        int plafond = ReglagesLegacyPlus.MonstresMax;
        int formations = ReglagesLegacyPlus.FormationsParRencontre;
        if (frequence == 100 && plafond == 0 && formations == 1) return;

        if (frequence != 100)
        {
            Func<int, int> delai = Coutures.DelaiRencontre;
            Coutures.DelaiRencontre = d => Math.Max(1, delai(d) * 100 / frequence);
        }

        Func<bool, bool> attente = Coutures.RencontreEnAttente;
        Coutures.RencontreEnAttente = a => !s_Force && attente(a);

        Func<bool, bool> bloquee = Coutures.RencontreBloquee;
        Coutures.RencontreBloquee = b => !s_Force && bloquee(b);

        int delaiChevauchement = Math.Max(1, DelaiMoyen * 100 / frequence);
        s_Chevauchement = delaiChevauchement;

        Coutures.Tentative precedent = Coutures.Rencontre;
        Coutures.Rencontre = (rencontre, poolScene) =>
        {
            int avant = Vivantes(poolScene);
            precedent(rencontre, poolScene);                          // le chemin d'origine

            bool reussie = Vivantes(poolScene) > avant;

            // Chevauchement : des entités vivent, sous le plafond, et le minuteur du tweak expire.
            if (!reussie && plafond > 0 && avant > 0 && avant < Math.Min(plafond, LimiteRendu))
            {
                if (--s_Chevauchement <= 0)
                {
                    Rappeler(rencontre);
                    reussie = Vivantes(poolScene) > avant;
                    s_Chevauchement = reussie ? delaiChevauchement : DelaiReessai;
                }
            }

            // Quantité : une rencontre a eu lieu, on en appelle d'autres dans la même image.
            if (reussie)
            {
                for (int i = 1; i < formations; i++)
                {
                    int limite = plafond > 0 ? Math.Min(plafond, LimiteRendu) : LimiteRendu;
                    if (Vivantes(poolScene) >= limite) break;
                    Rappeler(rencontre);
                }
            }
        };
    }

    /// <summary>Rappelle la rencontre d'origine, ses deux gardes « minuteur » et « entité vivante » levées.</summary>
    private static void Rappeler(Action rencontre)
    {
        s_Force = true;
        try { rencontre(); }
        finally { s_Force = false; }
    }

    /// <summary>L'octet bas de l'en-tête du pool : le nombre d'entités de scène vivantes (<c>AllocEntitySlot</c>).</summary>
    private static int Vivantes(byte* poolScene) => poolScene[0];
}
