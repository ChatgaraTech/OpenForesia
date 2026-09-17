// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DE LA MONTAGNE DU DRAGON DE FEU — zone 2.
//
// Ouvert le 2026-09-17, sur le patron de `MineParticles.cs` et des deux `ForestParticles` : ce qui
// nait, scintille et meurt dans la zone 2, et ne decide de rien. La zone n'a qu'UNE sous-zone, et
// tout son decor tient dans trois tables (0x0056b0c8, 0x0056b1c8, 0x0056b2c8).
//
// NE SONT PAS ICI, bien que la zone les porte :
//   - ses DEUX SCENES CINEMA, 0x0040D930 et 0x0040E3A0, qui se partagent le bloc prive
//     0x005713f0 / 0x00571418 — voir `Cutscene/MineScenes.cs`, qui signale que cet enregistrement
//     chevauche une frontiere d'allocation. Elles attendent un fichier et une decision ;
//   - ses six DECORS de la table 0x0056adc8, qui vont dans `Handlers/FireMountain.cs` ;
//   - son BOSS, le Dragon rouge 0x00499140.
//
// LES SIX PLANCHES DE LA ZONE, ET LEURS BORNES. La region .data 0x0054f620..0x0054f6d8 est une
// grille reguliere d'enregistrements de huit octets. Les bornes ci-dessous ne sont pas des
// hypotheses de forme : elles viennent d'un BALAYAGE DU BINAIRE ENTIER pour chaque adresse
// candidate, qui dit exactement qui installe quoi.
//
//   0x0054f630  2 enreg.  installee par 0x0040DDE0 (trois fois)
//   0x0054f640  3         installee par 0x0040DDE0
//   0x0054f658  1         installee par 0x0040EAF0
//   0x0054f660  4         installee par 0x0040EBE0
//   0x0054f680  7         installee par 0x0040E6F0 et 0x0040E860
//   0x0054f6b8  4         installee par 0x0040EA00
//
// LA PLANCHE DE 0x0054f680 FAIT SEPT ENREGISTREMENTS, PAS QUATRE, et c'est la trouvaille de la
// seance. La forme change nettement au cinquieme (0x0054f6a0 passe de tuiles 0x20 x 0x20 page 02 a
// des 0x18 x 0x18 page 01), ce qui invitait a couper la. Mais le balayage rend **zero reference** a
// 0x0054f6a0 dans tout le binaire : personne ne l'installe, donc la plage continue jusqu'au
// prochain installateur, 0x0054f6b8. Couper sur un changement de forme aurait redonne le defaut
// que CLAUDE.md decrit — une plage transcrite trop courte rend du tas non initialise, sans erreur
// de compilation ni exception.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // LES SIX PLANCHES
    // =============================================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054f630</c> — <b>deux enregistrements</b>, bornes par
    /// <c>0x0054f620</c> (dessous, deja releve) et <c>0x0054f640</c> (dessus, installe par le meme
    /// gestionnaire). Une tuile <c>8 x 8</c> a l'origine et une <c>0x3f x 0x3f</c>.</summary>
    private static readonly byte[] s_Descriptors_0054F630 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f630  <- 0x0040DDE0
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f638
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054f640</c> — <b>trois enregistrements</b> <c>0x0f x 0x0f</c>
    /// qui se suivent sur la ligne <c>0xc0</c>. La variante 2 de <see cref="EntityHandler_0040DDE0"/>
    /// fait tourner son <c>+0x40</c> sur exactement 0, 1, 2 : la plage est employee
    /// entierement.</summary>
    private static readonly byte[] s_Descriptors_0054F640 =
    [
        0x00, 0xc0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x0054f640  <- 0x0040DDE0
        0x10, 0xc0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x0054f648
        0x20, 0xc0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x0054f650
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054f658</c> — <b>un seul enregistrement</b>, une tuile
    /// <c>8 x 8</c> a l'origine de la page <c>0x03</c>. <see cref="EntityHandler_0040EAF0"/>
    /// n'ecrit jamais <c>+0x40</c> : la marge de 1 ne tient que si le pondeur l'a laisse a zero,
    /// et c'est <c>check-descriptors.sh</c> qui le dira.</summary>
    private static readonly byte[] s_Descriptors_0054F658 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f658  <- 0x0040EAF0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054f660</c> — <b>quatre enregistrements</b> <c>0x18 x 0x18</c>
    /// sur la ligne <c>0x30</c>, aux colonnes <c>0x48</c> a <c>0x90</c>. C'est l'animation de
    /// l'etincelle de <see cref="EntityHandler_0040EBE0"/>, qui parcourt les quatre.</summary>
    private static readonly byte[] s_Descriptors_0054F660 =
    [
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f660  <- 0x0040EBE0
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f668
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f670
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054f678
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f680</c> — <b>SEPT enregistrements</b>, et c'est la longueur qu'il
    /// faut retenir.
    ///
    /// <para>
    /// Les quatre premiers sont des tuiles <c>0x20 x 0x20</c> de la page <c>02</c> sur la ligne
    /// <c>0x88</c> ; les trois derniers, a partir de <c>0x0054f6a0</c>, sont des
    /// <c>0x18 x 0x18</c> de la page <c>01</c> sur la ligne <c>0x30</c>. <b>Le changement de forme
    /// n'est pas une borne</b> : le balayage du binaire ne trouve <b>aucune</b> reference a
    /// <c>0x0054f6a0</c>, donc rien ne l'installe et la plage court jusqu'a <c>0x0054f6b8</c>.
    /// </para>
    /// <para>
    /// Ses deux installateurs, <see cref="EntityHandler_0040E6F0"/> et
    /// <see cref="EntityHandler_0040E860"/>, partent tous deux de l'enregistrement <b>1</b> et
    /// s'arretent au <b>3</b> : ils n'emploient donc que la premiere moitie. Les trois derniers
    /// sont hors d'atteinte par eux, mais ils sont dans la plage, et une plage transcrite trop
    /// courte est un defaut silencieux.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F680 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f680  <- 0x0040E6F0, 0x0040E860
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f688
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f690
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f698
        0x00, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f6a0  — installe par personne
        0x18, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f6a8
        0x30, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f6b0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054f6b8</c> — <b>quatre enregistrements</b> <c>8 x 8</c>, les
    /// quatre coins d'un carre de <c>0x10</c> sur la page <c>0x03</c>. Borne en haut par
    /// <c>0x0054f6d8</c>, que <c>0x0040D6C0</c> installe deja.</summary>
    private static readonly byte[] s_Descriptors_0054F6B8 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f6b8  <- 0x0040EA00
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f6c0
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f6c8
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f6d0
    ];

    private static byte* s_Descriptors0054F630Storage;
    private static byte* s_Descriptors0054F640Storage;
    private static byte* s_Descriptors0054F658Storage;
    private static byte* s_Descriptors0054F660Storage;
    private static byte* s_Descriptors0054F680Storage;
    private static byte* s_Descriptors0054F6B8Storage;

    private static byte* Descriptors0054F630()
        => NativeCopy(s_Descriptors_0054F630, ref s_Descriptors0054F630Storage);

    private static byte* Descriptors0054F640()
        => NativeCopy(s_Descriptors_0054F640, ref s_Descriptors0054F640Storage);

    private static byte* Descriptors0054F658()
        => NativeCopy(s_Descriptors_0054F658, ref s_Descriptors0054F658Storage);

    private static byte* Descriptors0054F660()
        => NativeCopy(s_Descriptors_0054F660, ref s_Descriptors0054F660Storage);

    private static byte* Descriptors0054F680()
        => NativeCopy(s_Descriptors_0054F680, ref s_Descriptors0054F680Storage);

    private static byte* Descriptors0054F6B8()
        => NativeCopy(s_Descriptors_0054F6B8, ref s_Descriptors0054F6B8Storage);

    // =============================================================================================
    // LES HUIT CORPS
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x0040cda0</c> — <b>le meme corps que <see cref="EntityHandler_00409E40"/></b>,
    /// le flash bleu de la mine : le compilateur l'a emis une seconde fois pour la montagne.
    ///
    /// <para>
    /// Absente de <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_0040CDA0.asm</c>. <b>89 instructions
    /// identiques sur 89</b>, immediates comprises — le diff STRICT ne rend pas une seule ligne, pas
    /// meme une adresse de planche ou de table de sauts. Il n'y a donc rien a lire en donnee : c'est
    /// un renvoi pur.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040CDA0(byte* e) => EntityHandler_00409E40(e);

    /// <summary>
    /// Ghidra: <c>0x0040d560</c> — <b>la trainee de feu d'un porteur</b>. Decompilee dans
    /// <c>Blaze.c</c>, 57 lignes.
    ///
    /// <para>
    /// Elle n'a pas de vie propre : a chaque image elle se recolle sur l'entite rangee en
    /// <c>+0x6c</c>, <c>0x50000</c> au-dessus, et reprend son cap. Elle meurt avec lui — le bit
    /// <c>0x80000000</c> tombe des que le porteur porte <c>+0x43 == 1</c>, ou qu'il meurt, ou qu'il
    /// s'eteint.
    /// </para>
    /// <para>
    /// <b>Elle sonne sur un cycle de six images</b> : le son <c>0x1f</c> a la naissance, puis a
    /// chaque passage du compteur <c>+0x98</c> par 1 le son <c>0x23</c> ou <c>0x20</c> selon que son
    /// <c>+0x9a</c> vaut 1 ou 2 — et ce <c>+0x9a</c> est fixe a la naissance par la variante du
    /// PORTEUR, pas par la sienne.
    /// </para>
    /// <para>
    /// Chaque image elle seme en plus un <c>0xdf</c> a <c>0xb4</c> unites devant elle et
    /// <c>0xa000</c> plus bas.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040D560(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null) return;

        *(byte**)(e + 0x74) = host;

        *(int*)(e + 0x1c) = *(int*)(host + 0x1c);
        *(int*)(e + 0x20) = *(int*)(host + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(host + 0x24);
        *(short*)(e + 0x32) = *(short*)(host + 0x32);

        if (*(short*)(e + 0x10) == 0)
        {
            short hostVariant = *(short*)(host + 0x16);
            *(short*)(e + 0x9a) = (hostVariant == 0 || hostVariant == 1) ? (short)2 : (short)1;

            *(short*)(e + 0x10) = 1;
            TryPlaySoundNearEntity((uint*)e, 0x1f, 0x6fd, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x98) == 1)
        {
            short sound = *(short*)(e + 0x9a) == 1 ? (short)0x23 : (short)0x20;
            TryPlaySoundNearEntity((uint*)e, sound, 0x1000, 0x800, 0, 0);
        }

        if (*(short*)(e + 0x98) == 5) *(short*)(e + 0x98) = 0;
        *(short*)(e + 0x98) += 1;

        // Trois raisons de s'eteindre, et le porteur les porte toutes les trois.
        if (host[0x43] == 1) *(uint*)e &= 0x7fffffff;

        uint hostFlags = *(uint*)host;
        if ((hostFlags & 0x40000000) != 0 || (hostFlags & 0x80000000) == 0) *(uint*)e &= 0x7fffffff;

        int course = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 0xb4;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 0xb4;
        *(int*)(e + 0x20) -= 0xa000;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xdf, 0, -1);   // Ghidra: FUN_0047ac40
    }

    /// <summary>
    /// Ghidra: <c>0x0040dde0</c> — <b>la braise, en quatre variantes</b>. Decompilee dans
    /// <c>Blaze.c</c>, 232 lignes. C'est le plus gros corps d'effet de la zone, et le seul dont
    /// <c>+0x14</c> choisisse entre quatre comportements sans rapport.
    ///
    /// <list type="table">
    ///   <item><term>0</term><description><b>la spirale montante</b> : un rayon <c>+0x9a</c> tire
    ///     entre 0x46 et 0x59 qui decroit, un cap qui tourne de <c>+0x5c * 0x10</c> par image, une
    ///     hauteur donnee par un second sinus. Elle s'eteint quand le rayon passe sous zero, et
    ///     s'eclaircit de 10 par image jusqu'a 0x80.</description></item>
    ///   <item><term>1</term><description><b>l'etincelle qui monte tout droit</b> : posee a
    ///     <c>+/-0x14</c> sur les trois axes, elle monte de <c>0x1000</c> et s'assombrit de 5 par
    ///     image jusqu'a disparaitre.</description></item>
    ///   <item><term>2</term><description><b>la gerbe</b> : trente et une filles d'un coup a la
    ///     naissance, <c>0x12c000</c> plus haut et <c>0x28</c> unites devant ; puis une par image
    ///     pendant la descente, et trente et une de plus pour finir. Son echelle bat sur un sinus,
    ///     et son <c>+0x40</c> avance d'un cran toutes les deux images sur trois
    ///     enregistrements.</description></item>
    ///   <item><term>4</term><description><b>la spirale descendante</b> : la 0 a l'envers — le
    ///     rayon croit au lieu de decroitre, le cap tourne dans l'autre sens et deux fois moins
    ///     vite, et la teinte s'assombrit d'un pas tire entre 2 et 4.</description></item>
    /// </list>
    /// <para>
    /// <b>Les variantes 0, 1 et 4 lisent la planche <c>0x0054f630</c> et la 2 celle de
    /// <c>0x0054f640</c></b> : c'est le seul gestionnaire de la zone a en installer deux.
    /// </para>
    /// <para>
    /// <b>Le sous-type des filles de la variante 2 n'est pas une constante</b> : c'est
    /// <c>+0x98 + 0xc0</c>, et <c>+0x98</c> vient d'etre pose a <c>0x3d</c> en tete de corps. Les
    /// filles naissent donc en <c>0xfd</c>. Le <c>+0x14</c> est maquille le temps de l'appel — 0, 1
    /// puis 4 — puis rendu : c'est ainsi que la mere choisit la variante de sa fille.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040DDE0(byte* e)
    {
        *(short*)(e + 0x98) = 0x3d;
        *(short*)(e + 0x96) = 10;

        short variant = *(short*)(e + 0x14);

        // ---------------------------------------------------------------- 0 : la spirale montante
        if (variant == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(int*)(e + 0x3c) = (int)Descriptors0054F630();
                e[0x40] = 0;

                *(int*)(e + 0x54) = *(int*)(e + 0x20);
                *(ushort*)(e + 0x32) = (ushort)SignedMask(CrtRand(), 0xfff);
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                *(short*)(e + 0x9a) = (short)(CrtRand() % 0x14 + 0x46);

                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;
                *(int*)(e + 0x5c) = CrtRand() % 3 + 2;

                e[0x0a] = 9;
                e[0x0b] = 0;
                *(ushort*)(e + 0x12) = (ushort)SignedMask(CrtRand(), 0xfff);

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 10, 10, 10);           // Ghidra: FUN_00404de0
                if (e[0x38] > 0x7f) { e[0x38] = 0x80; e[0x39] = 0x80; e[0x3a] = 0x80; }

                short radius = *(short*)(e + 0x9a);
                int course = *(short*)(e + 0x32);

                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);
                *(int*)(e + 0x20) = *(int*)(e + 0x54)
                    + (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12) + course) * 0x14;

                *(short*)(e + 0x9a) -= (short)*(int*)(e + 0x5c);
                *(short*)(e + 0x32) += (short)(*(int*)(e + 0x5c) * 0x10);

                if (*(short*)(e + 0x9a) < 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // ---------------------------------------------------------------- 1 : l'etincelle droite
        if (variant == 1)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(int*)(e + 0x3c) = (int)Descriptors0054F630();
                e[0x40] = 0;
                e[0x0a] = 9;
                e[0x0b] = 0;

                e[0x38] = 0x80;
                e[0x39] = 0x80;
                e[0x3a] = 0x80;

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) * 0x1000;
                *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) * 0x1000;
                *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                *(int*)(e + 0x20) += 0x1000;
                BrightenEntityTint_00404DE0(e, -6, -6, -6);
                if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // ---------------------------------------------------------------- 2 : la gerbe
        if (variant == 2)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(int*)(e + 0x3c) = (int)Descriptors0054F640();
                e[0x40] = 0;
                e[0x0a] = 6;
                e[0x0b] = 0;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;

                *(int*)(e + 0x20) += 0x12c000;

                int course = *(short*)(e + 0x32);
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 0x28;
                e[0x38] = 0;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 0x28;

                *(ushort*)(e + 0x28) = 0x800;
                *(ushort*)(e + 0x2a) = 0x800;

                short keep = *(short*)(e + 0x14);
                e[0x39] = 0;
                e[0x3a] = 0;
                *(short*)(e + 0x14) = 0;

                for (int i = 0x1f; i != 0; i--)
                {
                    SpawnStatusEffectEntity_0047AC40(
                        e, (int*)(e + 0x1c), null, (byte)(*(short*)(e + 0x98) + 0xc0), 0, -1);
                }

                *(int*)(e + 0x5c) = 0;
                *(short*)(e + 0x14) = keep;
                *(short*)(e + 0x9a) = 0;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 3, 3, 3);
                if (e[0x38] > 0x7f) { e[0x38] = 0x80; e[0x39] = 0x80; e[0x3a] = 0x80; }

                *(short*)(e + 0x12) += 1;
                if (*(short*)(e + 0x12) > 0x2a)
                {
                    *(short*)(e + 0x10) = 2;
                    *(short*)(e + 0x12) = 0;
                }
            }

            if (*(short*)(e + 0x10) == 2)
            {
                short keep = *(short*)(e + 0x14);
                *(short*)(e + 0x14) = 1;
                SpawnStatusEffectEntity_0047AC40(
                    e, (int*)(e + 0x1c), null, (byte)(*(short*)(e + 0x98) + 0xc0), 0, -1);
                *(short*)(e + 0x14) = keep;

                if (*(short*)(e + 0x12) < 0x4b) *(int*)(e + 0x20) -= 0x3000;
                *(short*)(e + 0x12) += 1;

                if (*(short*)(e + 0x16) > 0x9a)
                {
                    for (int i = 0x1f; i != 0; i--)
                    {
                        *(short*)(e + 0x14) = 4;
                        SpawnStatusEffectEntity_0047AC40(
                            e, (int*)(e + 0x1c), null, (byte)(*(short*)(e + 0x98) + 0xc0), 0, -1);
                        *(short*)(e + 0x14) = keep;
                    }

                    *(uint*)e &= 0x7fffffff;
                }
            }

            // Une image sur deux, l'enregistrement suivant, sur trois.
            int tick = *(int*)(e + 0x5c);
            *(int*)(e + 0x5c) = tick + 1;
            if (tick + 1 == 2)
            {
                *(int*)(e + 0x5c) = 0;
                e[0x40]++;
                if (e[0x40] > 2) e[0x40] = 0;
            }

            ushort beat = (ushort)((*(short*)(e + 0x9a) + 100) & 0xfff);
            *(ushort*)(e + 0x9a) = beat;
            *(short*)(e + 0x28) = (short)((int)Winsys3d.GetSinTableEntry(beat) >> 1);
            *(short*)(e + 0x2a) = (short)((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x9a)) >> 1);
        }

        // ---------------------------------------------------------------- 4 : la spirale inverse
        if (*(short*)(e + 0x14) == 4)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(int*)(e + 0x3c) = (int)Descriptors0054F630();
                e[0x40] = 0;

                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(short*)(e + 0x9a) = 0;
                *(ushort*)(e + 0x32) = (ushort)SignedMask(CrtRand(), 0xfff);
                *(int*)(e + 0x54) = *(int*)(e + 0x20);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                e[0x38] = 0x80;
                e[0x39] = 0x80;
                e[0x3a] = 0x80;
                *(int*)(e + 0x5c) = SignedMask(CrtRand(), 3) + 5;

                e[0x0a] = 9;
                e[0x0b] = 0;
                *(ushort*)(e + 0x12) = (ushort)SignedMask(CrtRand(), 0xfff);

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(int*)(e + 0x44) = -(CrtRand() % 3) - 2;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                short radius = *(short*)(e + 0x9a);
                int course = *(short*)(e + 0x32);

                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);
                *(int*)(e + 0x20) = *(int*)(e + 0x54)
                    + (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12) + course) * 0x28;

                *(short*)(e + 0x9a) += (short)*(int*)(e + 0x5c);
                *(short*)(e + 0x32) += (short)(*(int*)(e + 0x5c) * -2);

                short fade = (short)*(int*)(e + 0x44);
                BrightenEntityTint_00404DE0(e, fade, fade, fade);

                if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        *(short*)(e + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x0040e6f0</c> — <b>la bouffee qui monte</b>. Decompilee dans <c>Blaze.c</c>,
    /// 58 lignes.
    ///
    /// <para>
    /// Elle nait <c>0x3c000</c> au-dessus de son point, avec une echelle tiree entre <c>0x600</c> et
    /// <c>0x9ff</c>, une derive tiree sur les trois axes (<c>+/-5</c> horizontal, <c>5</c> a
    /// <c>9</c> vers le haut), et une duree de vie <c>+0x14</c> tiree entre 3 et 6. Elle part sur
    /// l'enregistrement <b>1</b> de la planche <c>0x0054f680</c>.
    /// </para>
    /// <para>
    /// <b>Chaque fois que son compteur depasse sa duree</b>, elle grossit de <c>0x100</c>, repasse a
    /// zero et avance d'un enregistrement ; au quatrieme elle s'eteint. Et elle s'assombrit de 6 par
    /// image, tout du long.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040E6F0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x3c) = (int)Descriptors0054F680();
            e[0x40] = 1;
            e[0x0a] = 10;
            e[0x0b] = 0;

            short scale = (short)(SignedMask(CrtRand(), 0x1ff) + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x50) = (CrtRand() % 10 - 5) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % 10 - 5) * 0x1000;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x54) = (CrtRand() % 5 + 5) * 0x1000;

            *(int*)(e + 0x20) += 0x3c000;
            *(ushort*)(e + 0x14) = (ushort)(SignedMask(CrtRand(), 3) + 3);

            TryPlaySoundNearEntity((uint*)e, 0x0b, 0x200, 0x500, 0, 0);   // Ghidra: FUN_00468080

            e[0x38] = 200;
            e[0x39] = 0xa0;
            e[0x3a] = 0xa0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        // Elle brule ce qui passe : rayon 0x28, cinq pour cent des points de vie.
        DamagePartyNearPoint_00418F50(
            *(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24), 0x28, 5, 0);

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(short*)(e + 0x12) += 1;
        *(int*)(e + 0x24) += *(int*)(e + 0x58);

        if (*(short*)(e + 0x12) > *(short*)(e + 0x14))
        {
            *(short*)(e + 0x28) += 0x100;
            *(short*)(e + 0x2a) += 0x100;
            *(short*)(e + 0x12) = 0;

            e[0x40]++;
            if (e[0x40] > 3) *(uint*)e &= 0x7fffffff;
        }

        BrightenEntityTint_00404DE0(e, -10, -10, -10);
    }

    /// <summary>
    /// Ghidra: <c>0x0040e860</c> — <b>la bouffee lancee</b>, la sœur de
    /// <see cref="EntityHandler_0040E6F0"/>. Absente de <c>Blaze.c</c> ; 143 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_0040E860.asm</c>.
    ///
    /// <para>
    /// Meme planche, meme enregistrement de depart, meme grossissement de <c>0x100</c> par palier,
    /// meme mort au quatrieme, meme assombrissement de 6. <b>Ce qui change, c'est qu'elle
    /// PART</b> : sa derive n'est pas tiree au hasard autour de zero mais construite sur le cap
    /// <c>+0x32</c>, a une vitesse tiree entre 5 et 9, plus un ecart de <c>+/-2</c> unites sur
    /// chaque axe. Elle ne monte pas non plus toute seule — sa composante verticale est tiree entre
    /// <c>-5</c> et <c>+4</c>.
    /// </para>
    /// <para>
    /// Et sa duree de palier est tiree entre 3 et 6 comme sa sœur, mais par
    /// <c>rand() &amp; 3</c> signe, pas par le meme motif.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040E860(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x3c) = (int)Descriptors0054F680();
            e[0x40] = 1;
            e[0x0a] = 0x0a;
            e[0x0b] = 0;

            short scale = (short)(SignedMask(CrtRand(), 0x1ff) + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            int speed = (short)(CrtRand() % 5 + 5);
            int course = *(short*)(e + 0x32);

            *(int*)(e + 0x58) =
                ((SignedMask(CrtRand(), 3) - 2) << 0xc) - (int)Winsys3d.GetSinTableEntry(course) * speed;
            *(int*)(e + 0x50) =
                Winsys3d.GetCosTableEntry(course) * speed + ((SignedMask(CrtRand(), 3) - 2) << 0xc);

            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x54) = (CrtRand() % 10 - 5) << 0xc;

            *(short*)(e + 0x14) = (short)(SignedMask(CrtRand(), 3) + 3);

            TryPlaySoundNearEntity((uint*)e, 0x0b, 0x200, 0x500, 0, 0);

            e[0x38] = 0xc8;
            e[0x39] = 0xa0;
            e[0x3a] = 0xa0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        DamagePartyNearPoint_00418F50(
            *(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24), 0x28, 5, 0);

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);
        *(short*)(e + 0x12) += 1;

        if (*(short*)(e + 0x12) > *(short*)(e + 0x14))
        {
            *(short*)(e + 0x28) += 0x100;
            *(short*)(e + 0x2a) += 0x100;
            *(short*)(e + 0x12) = 0;

            e[0x40]++;
            if (e[0x40] >= 4) *(uint*)e &= 0x7fffffff;
        }

        BrightenEntityTint_00404DE0(e, -10, -10, -10);
    }

    /// <summary>
    /// Ghidra: <c>0x0040ea00</c> — <b>le grain de la montagne</b>, le meme corps que
    /// <see cref="EntityHandler_004070F0"/> de la mine. Absente de <c>Blaze.c</c> ; 84 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_0040EA00.asm</c>.
    ///
    /// <para>
    /// Le diff STRICT ne rend que <b>deux lignes</b>, et une seule compte : <c>mov dword
    /// [esi+3Ch],54F6B8h</c> au lieu de <c>54F1E8h</c>. C'est donc le grain de la mine lisant une
    /// AUTRE planche — et, contrairement au cas de l'Ours gris, les deux planches n'ont pas les
    /// memes octets : <c>0x0054f6b8</c> porte quatre tuiles <c>8 x 8</c> de la page <c>0x03</c> la
    /// ou <c>0x0054f1e8</c> en porte d'autres. Le corps est partage, la donnee non.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040EA00(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors0054F6B8());

    /// <summary>
    /// Ghidra: <c>0x0040eaf0</c> — <b>l'escarbille</b>. Decompilee dans <c>Blaze.c</c>, 41 lignes.
    ///
    /// <para>
    /// Elle nait dans un carre de <c>0xa0</c> unites autour de son point, jusqu'a <c>0x14</c>
    /// au-dessus, avec une echelle fixe de <c>0x9c4</c>, et monte d'une vitesse <c>+0x12</c> tiree
    /// entre 10 et 19. <b>Cette vitesse decroit d'un cran toutes les deux images</b>, si bien que la
    /// montee s'essouffle ; sous 7 elle retrecit de 200 par image, et sous 3 elle s'eteint.
    /// </para>
    /// <para>
    /// <b>Elle n'ecrit jamais <c>+0x40</c></b>, et sa planche n'a qu'un enregistrement : elle
    /// dependra donc de ce que le pondeur y aura laisse.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040EAF0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x3c) = (int)Descriptors0054F658();
            e[0x0a] = 6;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0xa0 - 0x50) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0xa0 - 0x50) * 0x1000;

            int lift = CrtRand();
            *(short*)(e + 0x14) = 0;
            *(ushort*)(e + 0x28) = 0x9c4;
            *(ushort*)(e + 0x2a) = 0x9c4;
            *(int*)(e + 0x20) += (lift % 0x14) * 0x1000;

            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 10);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x14) += 1;
        *(int*)(e + 0x20) += *(short*)(e + 0x12) * 0x1000;

        if (*(short*)(e + 0x14) == 2)
        {
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x12) -= 1;
        }

        if (*(short*)(e + 0x12) < 7)
        {
            *(short*)(e + 0x28) -= 200;
            *(short*)(e + 0x2a) -= 200;
        }

        if (*(short*)(e + 0x12) < 3) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0040ebe0</c> — <b>l'etincelle qui s'egare</b>. Absente de <c>Blaze.c</c> ;
    /// 171 instructions, <c>Legacy/Raw/Blaze/FUN_0040EBE0.asm</c>.
    ///
    /// <para>
    /// <b>Elle memorise une ancre a la naissance</b> — sa propre position, <c>0x32000</c> plus
    /// haut, rangee en <c>+0x44/+0x48/+0x4c</c> — puis s'en ecarte par une marche aleatoire
    /// biaisee : trois vitesses tirees une fois pour toutes (<c>+/-0x0c</c> horizontal, <c>6</c> a
    /// <c>0x0d</c> vertical), auxquelles s'ajoute un bruit de <c>+/-5</c> par image et par axe.
    /// <b>C'est la seule particule du depot qui garde un point de reference</b> — et elle n'y
    /// revient pas : elle meurt de s'en etre trop eloignee.
    /// </para>
    /// <para>
    /// <b>Trois facons de passer en phase 2</b>, et c'est bien un eloignement, pas une arrivee : la
    /// distance a l'ancre <b>depasse</b> <c>0x78</c> ; ou le compteur <c>+0x16</c> depasse la duree
    /// <c>+0x14</c> tiree entre 10 et 39 ; ou un tirage sur vingt sort 1.
    /// </para>
    /// <para>
    /// <b>En phase 2 elle meurt de deux facons aussi</b> : passe la cinquantieme image sans
    /// condition, ou entre la trentieme et la cinquantieme avec une chance sur cinquante.
    /// </para>
    /// <para>
    /// Son <c>+0x40</c> est le compteur d'images divise par deux, <b>replie</b> dans 0..3 par une
    /// boucle qui retranche 4 tant que le resultat depasse 3 — transcrite telle quelle.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040EBE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x3c) = (int)Descriptors0054F660();

            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x48) = *(int*)(e + 0x20) + 0x32000;
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x7d0 + 0xc18);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x7d0 + 0xc18);

            *(int*)(e + 0x50) = (CrtRand() % 0x18 - 0x0c) << 0xc;
            *(int*)(e + 0x54) = (SignedMask(CrtRand(), 7) + 6) << 0xc;

            e[0x0a] = 4;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x58) = (CrtRand() % 0x18 - 0x0c) << 0xc;

            *(short*)(e + 0x16) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x1e + 0x0a);
        }

        // Ghidra: LAB_0040ecb2 — la distance a l'ancre, a trois dimensions. Le test est un DEPASSEMENT :
        // l'etincelle meurt de s'eloigner, pas d'arriver.
        int dx = (*(int*)(e + 0x44) >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (*(int*)(e + 0x48) >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
        int dz = (*(int*)(e + 0x4c) >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) > 0x78
            && *(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
        }

        // Ghidra: LAB_0040ed12
        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) += CrtRand() % 10 + *(int*)(e + 0x50) - 5;
            *(int*)(e + 0x24) += CrtRand() % 10 + *(int*)(e + 0x58) - 5;
            *(int*)(e + 0x20) += CrtRand() % 5 + *(int*)(e + 0x54) - 2;

            int roll = CrtRand() % 0x14;
            if (*(short*)(e + 0x16) > *(short*)(e + 0x14) || roll == 1)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        // Ghidra: LAB_0040ed92
        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) += 1;
            int roll = CrtRand() % 0x32;

            short age = *(short*)(e + 0x12);
            if (age > 0x50 || (age > 0x1e && roll == 1)) *(uint*)e &= 0x7fffffff;
        }

        // Ghidra: LAB_0040edc5 — le repli du numero d'enregistrement dans 0..3.
        *(short*)(e + 0x16) += 1;
        byte record = (byte)(*(short*)(e + 0x16) >> 1);
        e[0x40] = record;

        if (record >= 4)
        {
            do { e[0x40] += 0xfc; } while (e[0x40] >= 4);
        }
    }
}
