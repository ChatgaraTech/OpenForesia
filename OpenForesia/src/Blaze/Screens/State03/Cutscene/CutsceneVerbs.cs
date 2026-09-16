// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA MACHINE A VERBES DE CINEMATIQUE — les 32 verbes de la table 0x0055eb68.
//
// Meme forme que la machine a opcodes des objets : une suite d'octets, un premier octet qui
// indexe une table, chaque verbe lisant ses operandes dans les octets suivants et rendant le
// pointeur sur le verbe suivant. `RunCutsceneVerb` deroule, `CutsceneVerb00` a `CutsceneVerb31`
// sont les verbes un par un.
//
// C'est ce qui prend la main quand la partie cesse de repondre aux boutons : deplacer la camera,
// poser un personnage, attendre, parler, rendre la main.
//
// Les scenes qui emploient ces verbes sont dans `ForestScenes.cs`. L'interpreteur de script
// d'ENTITE - a ne pas confondre, 39 opcodes et une autre table - est dans `WorldMapScript.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// <b>Le repartiteur des verbes de la camera de scenette</b> — la table
    /// <c>PTR_LAB_0055eb68</c>, relevee dans le binaire : <b>trente-deux verbes</b> (indices 0 a
    /// 31, de <c>0x005037d0</c> a <c>0x005043c0</c>). Le dword suivant, <c>0x00504c80</c>, est
    /// conserve dans la table ci-dessous parce qu'il est bien la en memoire, mais <b>ce n'est pas
    /// un verbe</b> : son corps ne lit ni curseur ni drapeau, il enchaine un appel et un saut vers
    /// un tout autre sous-programme. La fin reelle de la table est le mot nul qui le suit.
    /// Ghidra l'a nommee <c>PTR_LAB_</c>, donc il n'a reconnu aucune de ses cibles comme une
    /// fonction et n'en a decompile aucune : chaque verbe se transcrit depuis le desassemblage.
    ///
    /// <para>La convention est uniforme : le verbe recoit le <b>curseur de script</b> et l'adresse
    /// du drapeau <c>continuer</c>, et rend le <b>nouveau curseur</b>. Le premier octet lu est
    /// l'indice du verbe lui-meme, les suivants sont ses operandes ; avancer le curseur de la
    /// bonne longueur est donc a la charge du verbe. Ne rien faire de <c>continuer</c> enchaine sur
    /// le verbe suivant <b>dans le meme appel</b> — c'est le poser a zero qui rend la main a la
    /// boucle de jeu.</para>
    /// </summary>
    private static byte* RunCutsceneVerb(byte* cursor, ref int carryOn)
    {
        switch (*cursor)
        {
            case 0: return CutsceneVerb00_005037D0(ref carryOn);
            case 1: return CutsceneVerb01_005037E0(cursor);
            case 2: return CutsceneVerb02_00503850(cursor);
            case 3: return CutsceneVerb03_005038A0(cursor, ref carryOn);
            case 4: return CutsceneVerb04_00503990(cursor, ref carryOn);
            case 5: return CutsceneVerb05_00503B50(cursor, ref carryOn);
            case 6: return CutsceneVerb06_00503D30(cursor, ref carryOn);
            case 7: return CutsceneVerb07_00503D70(cursor);
            case 8: return CutsceneVerb08_00503D80();
            case 9: return CutsceneVerb09_00503D90(cursor);
            case 10: return CutsceneVerb10_00503DB0(cursor);
            case 11: return CutsceneVerb11_00503DD0(cursor);
            case 12: return CutsceneVerb12_00503DF0(cursor);
            case 13: return CutsceneVerb13_00503E10(cursor);
            case 14: return CutsceneVerb14_00503E50(cursor);
            case 15: return CutsceneVerb15_00503E80(cursor);
            case 16: return CutsceneVerb16_00503F10(cursor);
            case 17: return CutsceneVerb17_00503FA0(cursor);
            case 18: return CutsceneVerb18_00503FD0(cursor);
            case 19: return CutsceneVerb19_00504020(cursor);
            case 20: return CutsceneVerb20_00504070(cursor);
            case 21: return CutsceneVerb21_005040A0(cursor);
            case 22: return CutsceneVerb22_00504110(cursor);
            case 23: return CutsceneVerb23_00504170(cursor);
            case 24: return CutsceneVerb24_005041A0(cursor);
            case 25: return CutsceneVerb25_005041D0(cursor);
            case 26: return CutsceneVerb26_00504200(cursor);
            case 27: return CutsceneVerb27_00504270(cursor);
            case 28: return CutsceneVerb28_00504290(cursor);
            case 29: return CutsceneVerb29_005042B0(cursor);
            case 30: return CutsceneVerb30_00504310(cursor);
            case 31: return CutsceneVerb31_005043C0(cursor);
        }

        throw new NotImplementedException(
            $"Camera de scenette, verbe {*cursor} = 0x{s_CutsceneVerbTable_0055eb68[*cursor]:x8}, " +
            "pas encore reconstruit. Son adresse vient de PTR_LAB_0055eb68, **absente de Blaze.c** " +
            "— Ghidra n'a pas reconnu ses cibles comme des fonctions, donc le corps se releve au " +
            $"desassembleur : `OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe " +
            $"0x{s_CutsceneVerbTable_0055eb68[*cursor]:x8} <n>`. Le verbe prend (curseur, " +
            "&continuer) et rend le nouveau curseur ; poser `continuer` a zero rend la main.");
    }

    /// <summary>
    /// Les trente-trois entrees de <c>PTR_LAB_0055eb68</c>, relevees dans <c>.data</c>. Elles ne
    /// servent qu'a nommer precisement la lacune quand un verbe manque — le repartiteur ci-dessus
    /// ne les appelle pas.
    /// </summary>
    private static readonly uint[] s_CutsceneVerbTable_0055eb68 =
    {
        0x005037d0, 0x005037e0, 0x00503850, 0x005038a0, 0x00503990, 0x00503b50, 0x00503d30,
        0x00503d70, 0x00503d80, 0x00503d90, 0x00503db0, 0x00503dd0, 0x00503df0, 0x00503e10,
        0x00503e50, 0x00503e80, 0x00503f10, 0x00503fa0, 0x00503fd0, 0x00504020, 0x00504070,
        0x005040a0, 0x00504110, 0x00504170, 0x005041a0, 0x005041d0, 0x00504200, 0x00504270,
        0x00504290, 0x005042b0, 0x00504310, 0x005043c0, 0x00504c80,
    };

    /// <summary>
    /// Ghidra: verbe 0 @ 0x005037d0 — <b>fin de scenette</b>. Quatre instructions : il pose
    /// <c>continuer</c> a zero et rend un curseur nul, si bien que la boucle sort et que
    /// <c>DAT_005719b4</c> retombe a zero. C'est le seul verbe qui rend <c>null</c>.
    /// </summary>
    private static byte* CutsceneVerb00_005037D0(ref int carryOn)
    {
        carryOn = 0;
        return null;
    }
    // ---------------------------------------------------------------------------------------------
    // LES SIX BLOCS QUE LA SCENETTE MANIPULE
    //
    // `BeginScriptedCamera` a debranche les trois pointeurs que la carte du monde suit — les angles
    // `DAT_005e9ae0`, la position `DAT_005edebc`, la cible `DAT_005eca94` — et les a fait pointer
    // sur des copies de travail. Les verbes ci-dessous jouent sur deux etages :
    //
    //     travail (ce que la camera montre)      consigne (ce que les verbes visent)
    //     0x005719e8  angles, trois shorts       0x005719f0
    //     0x00571a18  position, trois ints       0x005719f8
    //     0x00571a08  cible, trois ints          0x00571a28
    //
    // Les verbes 15 a 31 ecrivent la consigne. Les verbes 3, 4 et 5 sont les seuls a faire bouger le
    // travail — d'un coup, par quarts, ou en N images — et ce sont donc les seuls a rendre la main.
    // Le compteur d'attente commun est l'octet `DAT_00571a34`.
    // ---------------------------------------------------------------------------------------------

    /// <summary>
    /// Ghidra: verbe 1 @ 0x005037e0 — <b>rebranche la camera libre</b>. Il relit le mode de vue
    /// <c>DAT_005de54c</c> ; si c'est encore celui de la scenette (2, ou 3), il y remet le mode que
    /// <c>BeginScriptedCamera</c> avait mis de cote dans <c>DAT_00604b7c</c>, puis il rend les trois
    /// pointeurs aux blocs de ce mode :
    /// <list type="bullet">
    ///   <item><term>mode 0</term><description>les blocs de la carte du monde</description></item>
    ///   <item><term>mode 1</term><description>les blocs de vue <c>0x006067e8</c> / <c>0x006067d0</c>,
    ///         la position restant celle de la carte</description></item>
    ///   <item><term>autre</term><description>rien n'est rebranche</description></item>
    /// </list>
    /// C'est la sortie propre d'une scenette, celle qui rend la camera au jeu.
    /// </summary>
    private static byte* CutsceneVerb01_005037E0(byte* cursor)
    {
        int mode = s_Unknown_005de54c;
        if (mode == 2 || mode == 3)
        {
            mode = Saves.s_SavedViewMode_00604b7c;
            s_Unknown_005de54c = mode;
        }

        if (mode == 0)
        {
            *(byte**)Mem.Arena(0x005e9ae0) = s_WorldMapCamera_00564650;
            *(byte**)Mem.Entity(0x005edebc) = (byte*)Mem.Camera(0x005de068);
            *(byte**)Mem.Entity(0x005eca94) = (byte*)Mem.Camera(0x005de078);
        }
        else if (mode == 1)
        {
            *(byte**)Mem.Arena(0x005e9ae0) = (byte*)Mem.View(0x006067e8);
            *(byte**)Mem.Entity(0x005edebc) = (byte*)Mem.Camera(0x005de068);
            *(byte**)Mem.Entity(0x005eca94) = (byte*)Mem.View(0x006067d0);
        }

        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 2 @ 0x00503850 — <b>appelle un autre script</b> sur l'entite porteuse, par son
    /// index 16 bits lu en <c>+1..+2</c>. Il compare <c>DAT_005719b4</c> avant et apres : si le
    /// script appele a lui-meme installe une nouvelle scenette, <b>c'est elle qui prend la main</b>
    /// et le verbe rend son curseur ; sinon il reprend simplement trois octets plus loin.
    /// </summary>
    private static byte* CutsceneVerb02_00503850(byte* cursor)
    {
        byte* before = *(byte**)Mem.EntityStateBytes(0x005719b4);

        RunEntityScript(
            s_Unknown_005e97cc,
            *(byte**)Mem.EntityStateBytes(0x005719c4),
            (uint)(cursor[1] | (cursor[2] << 8)));

        byte* after = *(byte**)Mem.EntityStateBytes(0x005719b4);
        return before == after ? cursor + 3 : after;
    }

    /// <summary>
    /// Ghidra: verbe 3 @ 0x005038a0 — <b>saute a la consigne, puis tient N images</b>. Sans
    /// transition : les trois blocs de travail recoivent la consigne telle quelle, et le verbe rend
    /// la main pour <c>+1</c> images.
    ///
    /// <para>Il est le seul des trois verbes de pas a <b>ecrire directement les registres que le
    /// moteur lit</b> : le triplet d'angles <c>DAT_00604928</c> — elevation, cap, roulis — et le
    /// vecteur de translation <c>DAT_006008d0</c>, dont les composantes 0 et 2 partent <b>niees</b>.
    /// Les verbes 4 et 5 ne le font pas : ils se contentent des blocs de travail, que les pointeurs
    /// rebranches suffisent a faire suivre.</para>
    ///
    /// <para><b>Le roulis ne vient pas de la consigne</b> mais du bloc de travail lui-meme
    /// (<c>DAT_005719ec</c>), que seul le verbe 13 remplit : le verbe 17 n'ecrit que deux
    /// composantes sur trois, et c'est coherent.</para>
    ///
    /// <para>Le compteur decroit d'une unite par image ; l'image ou il atteint zero est celle ou le
    /// verbe avance de deux octets <b>sans</b> rendre la main, si bien que le verbe suivant part
    /// dans le meme tour.</para>
    /// </summary>
    private static byte* CutsceneVerb03_005038A0(byte* cursor, ref int carryOn)
    {
        byte* hold = Mem.EntityStateBytes(0x00571a34);
        if (*hold != 0)
        {
            if (--*hold == 0) return cursor + 2;
            carryOn = 0;
            return cursor;
        }

        short* angleGoal = (short*)Mem.EntityStateBytes(0x005719f0);
        int* posGoal = Mem.EntityState(0x005719f8);
        int* tgtGoal = Mem.EntityState(0x00571a28);

        short* viewWork = (short*)Mem.EntityStateBytes(0x005719e8);
        int* cameraWork = Mem.EntityState(0x00571a18);
        int* targetWork = Mem.EntityState(0x00571a08);

        cameraWork[0] = posGoal[0];
        cameraWork[1] = posGoal[1];
        cameraWork[2] = posGoal[2];

        targetWork[0] = tgtGoal[0];
        targetWork[1] = tgtGoal[1];
        targetWork[2] = tgtGoal[2];

        viewWork[0] = angleGoal[0];
        viewWork[1] = angleGoal[1];

        short* cameraAngles = (short*)Mem.s_CameraVector_00604928;
        cameraAngles[0] = angleGoal[0];
        cameraAngles[1] = angleGoal[1];
        cameraAngles[2] = viewWork[2];

        *(int*)Mem.Entity(0x006008d0) = -posGoal[0];
        *(int*)Mem.Entity(0x006008d4) = posGoal[1];
        *(int*)Mem.Entity(0x006008d8) = -posGoal[2];

        *hold = cursor[1];
        carryOn = 0;
        return cursor;
    }

    /// <summary>
    /// Ghidra: verbe 4 @ 0x00503990 — <b>le glissement</b>. A chaque image, chacune des huit
    /// composantes de travail avance <b>d'un quart</b> de ce qui la separe de sa consigne : la
    /// classique approche exponentielle, sans duree fixee.
    ///
    /// <para>Les deux composantes d'angle passent par le <b>chemin le plus court</b> : l'ecart est
    /// ramene sur douze bits (un tour complet vaut <c>0x1000</c>) et, au-dela d'un demi-tour, c'est
    /// le complement qui est pris, en negatif.</para>
    ///
    /// <para>Le verbe rend la main tant qu'<b>au moins un</b> des huit pas vaut 4 ou plus en valeur
    /// absolue ; quand les huit sont sous ce seuil il avance d'un octet <b>sans</b> rendre la main.
    /// Le seuil est teste sur le pas, pas sur l'ecart : la convergence est donc declaree des que
    /// l'ecart passe sous seize.</para>
    /// </summary>
    private static byte* CutsceneVerb04_00503990(byte* cursor, ref int carryOn)
    {
        short* viewWork = (short*)Mem.EntityStateBytes(0x005719e8);
        short* angleGoal = (short*)Mem.EntityStateBytes(0x005719f0);
        int* cameraWork = Mem.EntityState(0x00571a18);
        int* posGoal = Mem.EntityState(0x005719f8);
        int* targetWork = Mem.EntityState(0x00571a08);
        int* tgtGoal = Mem.EntityState(0x00571a28);

        int stepA = ShortestArcStep((angleGoal[0] - viewWork[0]) & 0xfff);
        int stepB = ShortestArcStep((angleGoal[1] - viewWork[1]) & 0xfff);

        int stepPx = (posGoal[0] - cameraWork[0]) >> 2;
        int stepPy = (posGoal[1] - cameraWork[1]) >> 2;
        int stepPz = (posGoal[2] - cameraWork[2]) >> 2;
        int stepTx = (tgtGoal[0] - targetWork[0]) >> 2;
        int stepTy = (tgtGoal[1] - targetWork[1]) >> 2;
        int stepTz = (tgtGoal[2] - targetWork[2]) >> 2;

        viewWork[0] += (short)stepA;
        viewWork[1] += (short)stepB;
        cameraWork[0] += stepPx;
        cameraWork[1] += stepPy;
        cameraWork[2] += stepPz;
        targetWork[0] += stepTx;
        targetWork[1] += stepTy;
        targetWork[2] += stepTz;

        if (Settled(stepA) && Settled(stepB)
            && Settled(stepPx) && Settled(stepPy) && Settled(stepPz)
            && Settled(stepTx) && Settled(stepTy) && Settled(stepTz))
        {
            return cursor + 1;
        }

        carryOn = 0;
        return cursor;

        // `neg` puis `cmp …,4` : on reproduit la valeur absolue de l'original, y compris son
        // comportement sur le minimum entier, plutot que d'appeler Math.Abs qui leverait.
        static bool Settled(int step) => (step < 0 ? -step : step) < 4;
    }

    /// <summary>
    /// Ghidra: verbe 5 @ 0x00503b50 — <b>le deplacement en N images</b>, le troisieme et dernier des
    /// verbes de pas. Contrairement au glissement du verbe 4, la duree est donnee : a la premiere
    /// image il divise chacun des huit ecarts par <c>+1</c> et range les huit pas dans un bloc
    /// dedie (<c>DAT_005719a8</c> pour les angles, <c>DAT_005719b8</c> pour la position,
    /// <c>DAT_005719c8</c> pour la cible), puis il ajoute ce pas a chaque image.
    ///
    /// <para>Les angles prennent le chemin le plus court, comme au verbe 4, mais divise par la duree
    /// au lieu d'etre pris par quarts.</para>
    ///
    /// <para><b>Une duree nulle divise par zero.</b> L'original leve alors la meme exception
    /// materielle que celle-ci ; le comportement est conserve plutot que garde, faute de savoir ce
    /// que le jeu de 1998 attendait d'un tel script.</para>
    ///
    /// <para>La derniere image applique le pas <b>et</b> avance de deux octets, contrairement aux
    /// verbes 3 et 6 ou l'image de sortie ne fait qu'avancer.</para>
    /// </summary>
    private static byte* CutsceneVerb05_00503B50(byte* cursor, ref int carryOn)
    {
        byte* hold = Mem.EntityStateBytes(0x00571a34);

        short* rateAngle = (short*)Mem.EntityStateBytes(0x005719a8);
        int* ratePos = Mem.EntityState(0x005719b8);
        int* rateTarget = Mem.EntityState(0x005719c8);

        short* viewWork = (short*)Mem.EntityStateBytes(0x005719e8);
        int* cameraWork = Mem.EntityState(0x00571a18);
        int* targetWork = Mem.EntityState(0x00571a08);

        if (*hold == 0)
        {
            *hold = cursor[1];
            int frames = *hold;

            short* angleGoal = (short*)Mem.EntityStateBytes(0x005719f0);
            int* posGoal = Mem.EntityState(0x005719f8);
            int* tgtGoal = Mem.EntityState(0x00571a28);

            rateAngle[0] = (short)ShortestArcOver((angleGoal[0] - viewWork[0]) & 0xfff, frames);
            rateAngle[1] = (short)ShortestArcOver((angleGoal[1] - viewWork[1]) & 0xfff, frames);

            ratePos[0] = (posGoal[0] - cameraWork[0]) / frames;
            ratePos[1] = (posGoal[1] - cameraWork[1]) / frames;
            ratePos[2] = (posGoal[2] - cameraWork[2]) / frames;

            rateTarget[0] = (tgtGoal[0] - targetWork[0]) / frames;
            rateTarget[1] = (tgtGoal[1] - targetWork[1]) / frames;
            rateTarget[2] = (tgtGoal[2] - targetWork[2]) / frames;

            carryOn = 0;
            return cursor;
        }

        byte* next = cursor;
        if (--*hold == 0) next = cursor + 2;
        else carryOn = 0;

        viewWork[0] += rateAngle[0];
        viewWork[1] += rateAngle[1];
        cameraWork[0] += ratePos[0];
        cameraWork[1] += ratePos[1];
        cameraWork[2] += ratePos[2];
        targetWork[0] += rateTarget[0];
        targetWork[1] += rateTarget[1];
        targetWork[2] += rateTarget[2];

        return next;
    }

    /// <summary>
    /// Ghidra: verbe 6 @ 0x00503d30 — <b>attendre N images</b> sans rien bouger. Meme compteur que
    /// les verbes 3 et 5.
    ///
    /// <para><b>L'amorcage ne rend pas la main</b> : il pose le compteur et retourne le meme
    /// curseur, si bien que la boucle repasse aussitot sur le meme verbe, trouve le compteur non nul
    /// et le decremente. Une duree de zero fait donc <b>tourner la boucle indefiniment</b> dans une
    /// seule image — un blocage latent du jeu de 1998, transcrit tel quel puisqu'aucun script connu
    /// n'ecrit ce zero.</para>
    /// </summary>
    private static byte* CutsceneVerb06_00503D30(byte* cursor, ref int carryOn)
    {
        byte* hold = Mem.EntityStateBytes(0x00571a34);
        if (*hold != 0)
        {
            if (--*hold == 0) return cursor + 2;
            carryOn = 0;
            return cursor;
        }

        *hold = cursor[1];
        return cursor;
    }

    /// <summary>
    /// Ghidra: verbe 7 @ 0x00503d70 — <b>pose l'etiquette de retour</b> : il range dans
    /// <c>DAT_005719dc</c> l'adresse de l'octet qui le suit. Un seul emplacement, donc une seule
    /// etiquette a la fois : les boucles de scenette ne s'imbriquent pas.
    /// </summary>
    private static byte* CutsceneVerb07_00503D70(byte* cursor)
    {
        *(byte**)Mem.EntityStateBytes(0x005719dc) = cursor + 1;
        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 8 @ 0x00503d80 — <b>saut inconditionnel a l'etiquette</b> posee par le verbe 7.
    /// Deux instructions : il rend <c>DAT_005719dc</c> tel quel, sans meme lire son curseur.
    /// </summary>
    private static byte* CutsceneVerb08_00503D80()
        => *(byte**)Mem.EntityStateBytes(0x005719dc);

    /// <summary>
    /// Ghidra: verbe 9 @ 0x00503d90 — <b>ouvre une repetition</b> : le nombre de tours va dans
    /// <c>DAT_005719e0</c> et l'adresse de reprise dans <c>DAT_005719d4</c>. Emplacements uniques
    /// eux aussi, donc pas d'imbrication.
    /// </summary>
    private static byte* CutsceneVerb09_00503D90(byte* cursor)
    {
        *Mem.EntityStateBytes(0x005719e0) = cursor[1];
        *(byte**)Mem.EntityStateBytes(0x005719d4) = cursor + 2;
        return cursor + 2;
    }

    /// <summary>
    /// Ghidra: verbe 10 @ 0x00503db0 — <b>ferme la repetition</b> : il decremente le compteur et
    /// repart a l'adresse de reprise tant qu'il n'est pas nul.
    ///
    /// <para>Le decompte precede le test, donc un compteur pose a <c>n</c> execute le corps
    /// <c>n</c> fois, et un compteur pose a zero le fait <b>256 fois</b> — l'octet passe par
    /// <c>0xff</c>.</para>
    /// </summary>
    private static byte* CutsceneVerb10_00503DB0(byte* cursor)
    {
        byte* remaining = Mem.EntityStateBytes(0x005719e0);
        if (--*remaining != 0) return *(byte**)Mem.EntityStateBytes(0x005719d4);
        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 11 @ 0x00503dd0 — <b>accroche les angles a l'entite visee</b> : le pointeur
    /// d'angles <c>DAT_005e9ae0</c> va se poser directement sur le triplet que le verbe 13 ou 15
    /// avait retenu dans <c>DAT_005719b0</c>. La camera cesse alors de viser une valeur figee pour
    /// <b>suivre</b> l'entite image par image, sans plus repasser par les verbes.
    /// </summary>
    private static byte* CutsceneVerb11_00503DD0(byte* cursor)
    {
        *(byte**)Mem.Arena(0x005e9ae0) = *(byte**)Mem.EntityStateBytes(0x005719b0);
        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 12 @ 0x00503df0 — <b>accroche la position a l'entite</b>, le pendant du
    /// verbe 11 pour <c>DAT_005edebc</c> et le triplet retenu par le verbe 14 ou 16 dans
    /// <c>DAT_00571a14</c>.
    /// </summary>
    private static byte* CutsceneVerb12_00503DF0(byte* cursor)
    {
        *(byte**)Mem.Entity(0x005edebc) = *(byte**)Mem.EntityStateBytes(0x00571a14);
        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 13 @ 0x00503e10 — <b>le verbe 15 applique a l'entite porteuse</b>
    /// (<c>DAT_005719c4</c>), donc sans operande : ni table ni indice a lire.
    ///
    /// <para>Une difference avec le verbe 15, et elle compte : celui-ci ecrit <b>aussi</b> la
    /// troisieme composante <c>DAT_005719f4</c>, le roulis que le verbe 3 recopie ensuite dans
    /// <c>DAT_00604928</c>. Le verbe 15, lui, ecrit la meme composante mais depuis une entite
    /// choisie ; c'est le verbe 17 qui la laisse ou elle est.</para>
    /// </summary>
    private static byte* CutsceneVerb13_00503E10(byte* cursor)
    {
        byte* owner = *(byte**)Mem.EntityStateBytes(0x005719c4);
        short* goal = (short*)Mem.EntityStateBytes(0x005719f0);

        goal[0] = *(short*)(owner + 0x30);
        goal[1] = (short)-*(short*)(owner + 0x32);
        goal[2] = *(short*)(owner + 0x34);

        *(byte**)Mem.EntityStateBytes(0x005719b0) = owner + 0x30;

        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 14 @ 0x00503e50 — <b>le verbe 16 applique a l'entite porteuse</b>, sans
    /// operande lui non plus.
    /// </summary>
    private static byte* CutsceneVerb14_00503E50(byte* cursor)
    {
        byte* owner = *(byte**)Mem.EntityStateBytes(0x005719c4);
        int* goal = Mem.EntityState(0x005719f8);

        goal[0] = *(int*)(owner + 0x1c);
        goal[1] = *(int*)(owner + 0x20);
        goal[2] = *(int*)(owner + 0x24);

        *(byte**)Mem.EntityStateBytes(0x00571a14) = owner + 0x1c;

        return cursor + 1;
    }

    /// <summary>
    /// Le quart de tour d'angle du verbe 4. <paramref name="delta"/> est deja ramene sur douze bits,
    /// donc positif ; au-dela d'un demi-tour (<c>0x800</c>) c'est le complement au tour complet qui
    /// est pris, en negatif, ce qui fait tourner la camera du bon cote.
    /// </summary>
    private static int ShortestArcStep(int delta)
        => delta < 0x800 ? delta >> 2 : -((0x1000 - delta) >> 2);

    /// <summary>
    /// La meme chose pour le verbe 5, ou la duree remplace le quart.
    /// </summary>
    private static int ShortestArcOver(int delta, int frames)
        => delta < 0x800 ? delta / frames : -((0x1000 - delta) / frames);


    /// <summary>
    /// Ghidra: verbe 15 @ 0x00503e80 — <b>la camera prend une entite pour cible</b>. L'octet
    /// <c>+1</c> choisit la table d'entites et l'octet <c>+2</c> l'indice dedans ; les trois tables
    /// ont le meme pas de <c>0x9c</c> octets :
    /// <list type="bullet">
    ///   <item><term>0</term><description><c>0x005e93c0</c>, les membres du groupe</description></item>
    ///   <item><term>1</term><description><c>0x005ecaa0</c>, les entites de scene</description></item>
    ///   <item><term>2</term><description><c>0x005fa5e0</c>, la troisieme table d'entites</description></item>
    /// </list>
    ///
    /// <para>Le verbe copie ensuite le triplet de position de l'entite (<c>+0x30</c>) dans
    /// <c>DAT_005719f0</c> — le meme triplet que <c>BeginScriptedCamera</c> avait rempli depuis la
    /// vue courante — en <b>niant la composante du milieu</b>, et range dans <c>DAT_005719b0</c>
    /// l'adresse du triplet source, de sorte que la suite puisse <b>suivre</b> l'entite et pas
    /// seulement la viser une fois. Aucune fonction de <c>Blaze.c</c> ne lit <c>DAT_005719b0</c> :
    /// ses lecteurs sont d'autres verbes de la meme table, eux aussi non decompiles.</para>
    ///
    /// <para><b>Le cas par defaut lit le script lui-meme.</b> Pour toute autre valeur que 0, 1 ou 2
    /// l'original fait <c>mov eax,[esp+8]</c>, ce qui — apres le <c>push esi</c> de l'entree — est
    /// le <b>premier</b> parametre, donc le curseur : il va chercher son triplet trente octets plus
    /// loin dans le script. Transcrit tel quel ; c'est vraisemblablement une branche morte que le
    /// scenario n'emprunte jamais, pas une intention.</para>
    /// </summary>
    private static byte* CutsceneVerb15_00503E80(byte* cursor)
    {
        byte* record;
        switch (cursor[1])
        {
            case 0: record = Inp.s_SlotFlags_005e93c0 + cursor[2] * 0x9c; break;
            case 1: record = Mem.Entity(0x005ecaa0) + cursor[2] * 0x9c; break;
            case 2: record = Mem.Entity(0x005fa5e0) + cursor[2] * 0x9c; break;
            default: record = cursor; break;
        }

        short* target = (short*)Mem.EntityStateBytes(0x005719f0);
        target[0] = *(short*)(record + 0x30);
        target[1] = (short)-*(short*)(record + 0x32);
        target[2] = *(short*)(record + 0x34);

        *(byte**)Mem.EntityStateBytes(0x005719b0) = record + 0x30;

        return cursor + 3;
    }

    /// <summary>
    /// Ghidra: verbe 16 @ 0x00503f10 — <b>la camera se pose sur une entite</b>. Le jumeau exact du
    /// verbe 15 : meme choix de table par <c>+1</c>, meme indice par <c>+2</c>, meme cas par defaut
    /// qui relit le curseur. Il copie cette fois le triplet <c>+0x1c</c> — la <b>position</b>, trois
    /// entiers 32 bits — dans la consigne <c>DAT_005719f8</c>, et range l'adresse du triplet source
    /// dans <c>DAT_00571a14</c> pour que la camera puisse suivre l'entite.
    ///
    /// <para>Aucune negation ici, contrairement au verbe 15 : c'est bien une position, pas un cap.</para>
    /// </summary>
    private static byte* CutsceneVerb16_00503F10(byte* cursor)
    {
        byte* record;
        switch (cursor[1])
        {
            case 0: record = Inp.s_SlotFlags_005e93c0 + cursor[2] * 0x9c; break;
            case 1: record = Mem.Entity(0x005ecaa0) + cursor[2] * 0x9c; break;
            case 2: record = Mem.Entity(0x005fa5e0) + cursor[2] * 0x9c; break;
            default: record = cursor; break;
        }

        int* goal = Mem.EntityState(0x005719f8);
        goal[0] = *(int*)(record + 0x1c);
        goal[1] = *(int*)(record + 0x20);
        goal[2] = *(int*)(record + 0x24);

        *(byte**)Mem.EntityStateBytes(0x00571a14) = record + 0x1c;

        return cursor + 3;
    }

    /// <summary>
    /// Ghidra: verbe 17 @ 0x00503fa0 — <b>consigne d'angles litterale</b>. Il lit deux entiers
    /// 16 bits ecrits en clair dans le script — <c>+1..+2</c> et <c>+3..+4</c> — et les pose dans
    /// les deux premieres composantes de <c>DAT_005719f0</c>, la consigne d'angles que le verbe 15
    /// remplissait depuis une entite.
    ///
    /// <para><b>La troisieme composante n'est pas touchee</b> (<c>DAT_005719f4</c> garde ce que
    /// <c>BeginScriptedCamera</c> y avait mis) : le roulis ne se pilote pas par ce verbe.</para>
    /// </summary>
    private static byte* CutsceneVerb17_00503FA0(byte* cursor)
    {
        short* goal = (short*)Mem.EntityStateBytes(0x005719f0);
        goal[0] = (short)(cursor[1] | (cursor[2] << 8));
        goal[1] = (short)(cursor[3] | (cursor[4] << 8));
        return cursor + 5;
    }

    /// <summary>
    /// Ghidra: verbe 18 @ 0x00503fd0 — <b>consigne de position litterale</b>. Trois entiers signes
    /// de 16 bits lus dans le script (<c>+1..+6</c>), <b>decales de douze bits vers la gauche</b>,
    /// vers la consigne de position <c>DAT_005719f8</c> que le verbe 16 remplissait depuis une
    /// entite. Le decalage dit la convention du bloc de camera : du virgule fixe 20.12, dont le
    /// script n'ecrit que la partie entiere.
    /// </summary>
    private static byte* CutsceneVerb18_00503FD0(byte* cursor)
    {
        int* goal = Mem.EntityState(0x005719f8);
        goal[0] = (short)(cursor[1] | (cursor[2] << 8)) << 12;
        goal[1] = (short)(cursor[3] | (cursor[4] << 8)) << 12;
        goal[2] = (short)(cursor[5] | (cursor[6] << 8)) << 12;
        return cursor + 7;
    }

    /// <summary>
    /// Ghidra: verbe 19 @ 0x00504020 — <b>consigne de cible litterale</b>. Meme lecture que le
    /// verbe 18 — trois entiers signes de 16 bits — mais vers <c>DAT_00571a28</c>, le troisieme
    /// triplet que <c>BeginScriptedCamera</c> avait sauvegarde, et <b>sans le decalage de douze
    /// bits</b>. Les deux triplets ne sont donc pas dans la meme echelle, et c'est voulu.
    /// </summary>
    private static byte* CutsceneVerb19_00504020(byte* cursor)
    {
        int* goal = Mem.EntityState(0x00571a28);
        goal[0] = (short)(cursor[1] | (cursor[2] << 8));
        goal[1] = (short)(cursor[3] | (cursor[4] << 8));
        goal[2] = (short)(cursor[5] | (cursor[6] << 8));
        return cursor + 7;
    }

    /// <summary>
    /// Ghidra: verbe 20 @ 0x00504070 — <b>le verbe 17 en relatif</b> : meme lecture de deux entiers
    /// 16 bits, mais <c>add</c> au lieu de <c>mov</c> sur la consigne d'angles. L'addition se fait
    /// <b>sur seize bits</b> (<c>add [5719F0h],cx</c>), donc elle boucle naturellement autour du
    /// tour complet — ce qui est exactement ce qu'on veut d'un cap.
    /// </summary>
    private static byte* CutsceneVerb20_00504070(byte* cursor)
    {
        short* goal = (short*)Mem.EntityStateBytes(0x005719f0);
        goal[0] += (short)(cursor[1] | (cursor[2] << 8));
        goal[1] += (short)(cursor[3] | (cursor[4] << 8));
        return cursor + 5;
    }

    /// <summary>
    /// Ghidra: verbe 21 @ 0x005040a0 — <b>le verbe 18 en relatif</b> : trois entiers signes de
    /// 16 bits, decales de douze bits, <b>ajoutes</b> a la consigne de position.
    /// </summary>
    private static byte* CutsceneVerb21_005040A0(byte* cursor)
    {
        int* goal = Mem.EntityState(0x005719f8);
        goal[0] += (short)(cursor[1] | (cursor[2] << 8)) << 12;
        goal[1] += (short)(cursor[3] | (cursor[4] << 8)) << 12;
        goal[2] += (short)(cursor[5] | (cursor[6] << 8)) << 12;
        return cursor + 7;
    }

    /// <summary>
    /// Ghidra: verbe 22 @ 0x00504110 — <b>le verbe 19 en relatif</b> : trois entiers signes de
    /// 16 bits <b>ajoutes</b> a la consigne de cible, toujours sans decalage.
    /// </summary>
    private static byte* CutsceneVerb22_00504110(byte* cursor)
    {
        int* goal = Mem.EntityState(0x00571a28);
        goal[0] += (short)(cursor[1] | (cursor[2] << 8));
        goal[1] += (short)(cursor[3] | (cursor[4] << 8));
        goal[2] += (short)(cursor[5] | (cursor[6] << 8));
        return cursor + 7;
    }

    // ---------------------------------------------------------------------------------------------
    // Les verbes 23 a 25 rappellent la camera libre. Les trois blocs qu'ils lisent sont ceux que la
    // carte du monde utilise hors scenette, et ce sont les memes que `BeginScriptedCamera` avait
    // debranches en leur substituant ses copies de travail :
    //
    //     DAT_005e9ae0 -> &DAT_00564650   les trois angles      (shorts)
    //     DAT_005edebc -> &DAT_005de068   la position           (ints)
    //     DAT_005eca94 -> &DAT_005de078   la cible              (ints)
    //
    // Un verbe 23/24/25 remet donc la consigne correspondante sur la valeur de la camera libre :
    // c'est le « reviens ou tu etais » du scenariste, composante par composante.
    // ---------------------------------------------------------------------------------------------

    /// <summary>
    /// Ghidra: verbe 23 @ 0x00504170 — <b>consigne d'angles = ceux de la camera libre</b>
    /// (<c>DAT_00564650</c>, trois <c>short</c>). Un seul octet : le verbe n'a pas d'operande.
    /// </summary>
    private static byte* CutsceneVerb23_00504170(byte* cursor)
    {
        short* free = (short*)s_WorldMapCamera_00564650;
        short* goal = (short*)Mem.EntityStateBytes(0x005719f0);
        goal[0] = free[0];
        goal[1] = free[1];
        goal[2] = free[2];
        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 24 @ 0x005041a0 — <b>consigne de position = celle de la camera libre</b>
    /// (<c>DAT_005de068</c>, trois <c>int</c>). Sans operande.
    /// </summary>
    private static byte* CutsceneVerb24_005041A0(byte* cursor)
    {
        int* goal = Mem.EntityState(0x005719f8);
        goal[0] = *Mem.Camera(0x005de068);
        goal[1] = *Mem.Camera(0x005de06c);
        goal[2] = *Mem.Camera(0x005de070);
        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 25 @ 0x005041d0 — <b>consigne de cible = celle de la camera libre</b>
    /// (<c>DAT_005de078</c>, trois <c>int</c>). Sans operande.
    /// </summary>
    private static byte* CutsceneVerb25_005041D0(byte* cursor)
    {
        int* goal = Mem.EntityState(0x00571a28);
        goal[0] = *Mem.Camera(0x005de078);
        goal[1] = *Mem.Camera(0x005de07c);
        goal[2] = *Mem.Camera(0x005de080);
        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 26 @ 0x00504200 — <b>le mouvement inverse des trois precedents</b> : il verse
    /// l'etat courant de la scenette <b>dans</b> les blocs de la camera libre, de sorte que la
    /// sortie de scenette ne fasse pas sauter l'image.
    ///
    /// <list type="bullet">
    ///   <item>les trois angles de travail (<c>DAT_005719e8</c>) vers <c>DAT_00564650</c> ;</item>
    ///   <item>la cible de travail (<c>DAT_00571a08</c>) <b>moins</b> <c>DAT_005de088</c> vers
    ///         <c>DAT_00564658</c>.</item>
    /// </list>
    ///
    /// <para>La soustraction n'est pas gratuite : <c>FUN_00516350</c> fait le chemin dans l'autre
    /// sens — <c>DAT_005de078 = DAT_00564658 + DAT_005de088</c>, apres avoir amorti
    /// <c>DAT_00564658</c> d'un huitieme a chaque image. <c>DAT_00564658</c> est donc un
    /// <b>ecart</b> par rapport a <c>DAT_005de088</c>, et pas une position ; le verbe le reconstitue
    /// dans la bonne unite pour que l'amortissement reprenne sans discontinuite.</para>
    ///
    /// <para>Sans operande, comme les trois precedents.</para>
    /// </summary>
    private static byte* CutsceneVerb26_00504200(byte* cursor)
    {
        short* viewWork = (short*)Mem.EntityStateBytes(0x005719e8);
        short* free = (short*)s_WorldMapCamera_00564650;
        free[0] = viewWork[0];
        free[1] = viewWork[1];
        free[2] = viewWork[2];

        int* targetWork = Mem.EntityState(0x00571a08);
        int* slide = (int*)(s_WorldMapCamera_00564650 + 8);
        slide[0] = targetWork[0] - *Mem.Camera(0x005de088);
        slide[1] = targetWork[1] - *Mem.Camera(0x005de08c);
        slide[2] = targetWork[2] - *Mem.Camera(0x005de090);

        return cursor + 1;
    }

    /// <summary>
    /// Ghidra: verbe 27 @ 0x00504270 — <b>pose des bits dans le mot de scenette</b>. Il lit
    /// l'octet qui suit l'indice et l'ajoute par <c>ou</c> a <c>DAT_00604b78</c>, puis avance de
    /// deux. Il ne touche pas a <c>continuer</c> : le verbe suivant part dans le meme appel.
    ///
    /// <para>L'original travaille <b>sur un octet</b> (<c>mov dl,[604B78h]</c> … <c>or dl,cl</c>) ;
    /// comme l'operande est lui-meme un octet, le resultat sur le mot complet est identique et
    /// l'ecriture ci-dessous est fidele.</para>
    ///
    /// <para>Le bit 0 est celui que <c>BeginScriptedCamera</c> pose et que l'echappement du joueur
    /// teste plus haut : ce verbe est donc, entre autres, le « rends la main au joueur » du
    /// scenariste.</para>
    /// </summary>
    private static byte* CutsceneVerb27_00504270(byte* cursor)
    {
        Saves.s_CutsceneActive_00604b78 |= cursor[1];
        return cursor + 2;
    }

    /// <summary>
    /// Ghidra: verbe 28 @ 0x00504290 — <b>l'exact contraire du verbe 27</b> : il <c>et-non</c>
    /// l'octet <c>+1</c> dans <c>DAT_00604b78</c> au lieu de l'y ajouter par <c>ou</c>, et avance
    /// lui aussi de deux. Meme remarque sur la largeur : l'original travaille sur l'octet bas, et
    /// comme le complement d'un octet laisse les bits hauts a un, l'ecriture sur le mot entier est
    /// fidele.
    /// </summary>
    private static byte* CutsceneVerb28_00504290(byte* cursor)
    {
        Saves.s_CutsceneActive_00604b78 &= ~cursor[1];
        return cursor + 2;
    }

    /// <summary>
    /// Ghidra: verbe 29 @ 0x005042b0 — <b>secousse d'angles</b>. Deux amplitudes de 16 bits lues
    /// dans le script ; pour chacune, <c>CrtRand() % (2 * amplitude) - amplitude</c> est
    /// <b>ajoute</b> a la consigne d'angles. C'est le tremblement de camera, tire a neuf a chaque
    /// passage du verbe.
    ///
    /// <para>Une amplitude nulle est <b>remontee a un</b> avant la division, ce qui evite le
    /// division-par-zero et donne un tirage constamment nul — le verbe reste donc inoffensif si le
    /// scenario ecrit un zero.</para>
    ///
    /// <para><b>Le diviseur et le recentrage n'ont pas la meme signature.</b> L'original divise par
    /// <c>movsx ecx,di</c> — l'amplitude <b>signee</b> sur 16 bits — puis retranche <c>edi</c>, qui
    /// est la meme amplitude <b>non signee</b>. Les deux ne coincident que sous 0x8000 ; transcrit
    /// tel quel plutot que normalise.</para>
    /// </summary>
    private static byte* CutsceneVerb29_005042B0(byte* cursor)
    {
        int rangeA = cursor[1] | (cursor[2] << 8);
        int rangeB = cursor[3] | (cursor[4] << 8);
        if ((ushort)rangeA == 0) rangeA = 1;
        if ((ushort)rangeB == 0) rangeB = 1;

        short* goal = (short*)Mem.EntityStateBytes(0x005719f0);
        goal[0] += (short)(CrtRand() % ((short)rangeA * 2) - rangeA);
        goal[1] += (short)(CrtRand() % ((short)rangeB * 2) - rangeB);

        return cursor + 5;
    }

    /// <summary>
    /// Ghidra: verbe 30 @ 0x00504310 — <b>secousse de position</b>, le verbe 29 porte a trois
    /// composantes et applique a la consigne de position, avec le decalage de douze bits qui est la
    /// convention de ce bloc. Meme remontee des amplitudes nulles a un, meme melange d'amplitude
    /// signee au diviseur et non signee au recentrage.
    /// </summary>
    private static byte* CutsceneVerb30_00504310(byte* cursor)
    {
        int rangeX = cursor[1] | (cursor[2] << 8);
        int rangeY = cursor[3] | (cursor[4] << 8);
        int rangeZ = cursor[5] | (cursor[6] << 8);
        if ((ushort)rangeX == 0) rangeX = 1;
        if ((ushort)rangeY == 0) rangeY = 1;
        if ((ushort)rangeZ == 0) rangeZ = 1;

        int* goal = Mem.EntityState(0x005719f8);
        goal[0] += (CrtRand() % ((short)rangeX * 2) - rangeX) << 12;
        goal[1] += (CrtRand() % ((short)rangeY * 2) - rangeY) << 12;
        goal[2] += (CrtRand() % ((short)rangeZ * 2) - rangeZ) << 12;

        return cursor + 7;
    }

    /// <summary>
    /// Ghidra: verbe 31 @ 0x005043c0 — <b>secousse de cible</b>, le troisieme du triptyque
    /// 29/30/31 : meme tirage <c>CrtRand() % (2 * amplitude) - amplitude</c> sur trois composantes,
    /// applique cette fois a la consigne de cible <c>DAT_00571a28</c>, et sans decalage — la meme
    /// difference d'echelle qui separe deja les verbes 18 et 19.
    ///
    /// <para><b>L'ecart de 0x8c0 octets jusqu'a l'entree suivante de la table ne mesure rien</b> :
    /// le verbe se termine des <c>0x0050445d</c>, et le code qui suit (a partir de
    /// <c>0x00504460</c>, une initialisation qui alloue huit tampons et ouvre un fichier) n'a rien
    /// a voir avec lui. Un ecart entre deux entrees d'une table de pointeurs n'est pas une taille.</para>
    /// </summary>
    private static byte* CutsceneVerb31_005043C0(byte* cursor)
    {
        int rangeX = cursor[1] | (cursor[2] << 8);
        int rangeY = cursor[3] | (cursor[4] << 8);
        int rangeZ = cursor[5] | (cursor[6] << 8);
        if ((ushort)rangeX == 0) rangeX = 1;
        if ((ushort)rangeY == 0) rangeY = 1;
        if ((ushort)rangeZ == 0) rangeZ = 1;

        int* goal = Mem.EntityState(0x00571a28);
        goal[0] += CrtRand() % ((short)rangeX * 2) - rangeX;
        goal[1] += CrtRand() % ((short)rangeY * 2) - rangeY;
        goal[2] += CrtRand() % ((short)rangeZ * 2) - rangeZ;

        return cursor + 7;
    }
}
