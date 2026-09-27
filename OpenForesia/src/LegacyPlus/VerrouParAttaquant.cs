// =====================================================================
// LP-004 — le verrou d'invulnérabilité des ennemis, tenu par attaque.
//
// Hors reconstruction (legacy ++, openspec/changes/add-legacy-plus-enemy-iframes).
// Dans le jeu de 1998, un ennemi touché devient intouchable pour TOUT le monde tant que son verrou
// court — deux verrous en fait : le compteur `+0x96` et le bit d'état `0x40` (« encaisse un coup »),
// retombé seulement à la fin du clignotement. À quatre joueurs, trois coups sur quatre se perdent,
// et un guerrier seul ne peut pas enchaîner. Ici, le refus ne vaut plus que contre LA MÊME attaque :
//
//  - un projectile ou un sort (balayage `Projectile`) : pendant la durée du verrou que son coup a
//    posé — il cherche sa cible à chaque image, et c'est ce délai qui l'empêche de frapper à chaque
//    image ;
//  - un geste de mêlée (balayage `Melee`) : tant que le geste est TENU. La mêlée ne frappe que sur la
//    dernière image d'un geste, que le jeu peut garder plusieurs tours (`PartyMemberStep`) ; un
//    contact au tour précédent signe le même geste. Un nouveau geste passe par d'autres images, donc
//    laisse au moins un tour sans contact, et frappe.
//
// Rien n'est écrit dans la mémoire du jeu : `+0x96` et `0x40` continuent d'être posés et décomptés,
// donc le clignotement, le recul, l'interruption et la fin d'une entité morte sont ceux d'origine.
// Ne tire aucun nombre aléatoire.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class VerrouParAttaquant
{
    /// <summary>La durée prise quand le verrou est déjà retombé à l'image suivante : celle de la mêlée.</summary>
    private const int DureeParDefaut = 0x10;

    /// <summary>Une fenêtre ouverte par un projectile sur une victime. Durée 0 : pas encore connue (design D3).</summary>
    private struct Fenetre
    {
        internal int Debut;
        internal int Duree;
    }

    // Projectiles et sorts : la fenêtre, de la durée du verrou posé.
    private static readonly Dictionary<(nint Source, nint Victime), Fenetre> s_Fenetres = new();
    private static readonly List<(nint Source, nint Victime)> s_EnAttente = new();

    // Mêlée : l'image du dernier contact entre ce membre du groupe et cette victime.
    private static readonly Dictionary<(nint Source, nint Victime), int> s_Gestes = new();

    private static readonly List<(nint Source, nint Victime)> s_Expirees = new();
    private static int s_Image;

    internal static void Installer()
    {
        if (ReglagesLegacyPlus.InvulnerabiliteEnnemis) return;

        Coutures.VictimeProtegee = Protegee;
        Coutures.VictimeExclue = Exclue;
        Coutures.CoupPorte = Noter;
        Coutures.ImageSuivante = Avancer;
    }

    /// <summary>
    /// Refusé seulement si le verrou d'origine court ET que c'est la même attaque : la fenêtre d'un
    /// projectile n'est pas finie, ou le geste de mêlée est encore tenu.
    /// </summary>
    private static bool Protegee(Coutures.Balayage balayage, byte* source, byte* victime, bool verrouOrigine)
    {
        if (!verrouOrigine) return false;
        var cle = ((nint)source, (nint)victime);

        if (balayage == Coutures.Balayage.Melee)
        {
            if (!s_Gestes.TryGetValue(cle, out int contact) || contact < s_Image - 1) return false;
            s_Gestes[cle] = s_Image;                      // le geste est toujours tenu
            return true;
        }

        if (!s_Fenetres.TryGetValue(cle, out Fenetre f)) return false;
        return f.Duree == 0 || s_Image - f.Debut < f.Duree;
    }

    /// <summary>
    /// Le second verrou : le bit <c>0x40</c> (« encaisse un coup ») du filtre d'état. Il n'est levé
    /// que pendant une fenêtre de coup (<c>+0x96</c> non nul — un <c>0x40</c> hors fenêtre garde son
    /// sens d'origine) et pour une autre attaque que celle qui l'a ouverte. Les autres bits (mort…)
    /// ne bougent pas.
    /// </summary>
    private static uint Exclue(Coutures.Balayage balayage, byte* source, byte* victime, uint exclusion)
    {
        const uint Encaisse = 0x40;
        if ((exclusion & Encaisse) == 0) return exclusion;
        if (*(ushort*)(victime + 0x96) == 0) return exclusion;
        if (Protegee(balayage, source, victime, true)) return exclusion;
        return exclusion & ~Encaisse;
    }

    private static void Noter(Coutures.Balayage balayage, byte* source, byte* victime)
    {
        var cle = ((nint)source, (nint)victime);

        if (balayage == Coutures.Balayage.Melee)
        {
            s_Gestes[cle] = s_Image;
            return;
        }

        s_Fenetres[cle] = new Fenetre { Debut = s_Image, Duree = 0 };
        s_EnAttente.Add(cle);
    }

    /// <summary>
    /// Une image commence : l'appelant du balayage a posé le verrou pendant l'image précédente, on en
    /// lit donc la durée (design D3) ; puis les fenêtres finies et les gestes lâchés sont retirés.
    /// </summary>
    private static void Avancer()
    {
        s_Image++;

        foreach (var cle in s_EnAttente)
        {
            if (!s_Fenetres.TryGetValue(cle, out Fenetre f) || f.Duree != 0) continue;
            ushort verrou = *(ushort*)((byte*)cle.Victime + 0x96);
            f.Duree = verrou != 0 ? verrou + 1 : DureeParDefaut;
            s_Fenetres[cle] = f;
        }
        s_EnAttente.Clear();

        foreach (var (cle, f) in s_Fenetres)
        {
            if (f.Duree != 0 && s_Image - f.Debut >= f.Duree) s_Expirees.Add(cle);
        }
        foreach (var cle in s_Expirees) s_Fenetres.Remove(cle);
        s_Expirees.Clear();

        foreach (var (cle, contact) in s_Gestes)
        {
            if (contact < s_Image - 1) s_Expirees.Add(cle);
        }
        foreach (var cle in s_Expirees) s_Gestes.Remove(cle);
        s_Expirees.Clear();
    }
}
