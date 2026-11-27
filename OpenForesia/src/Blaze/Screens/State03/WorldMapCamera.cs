// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md.
//
// LE SOUS-SYSTEME DE CAMERA DE LA CARTE DU MONDE — 0x00401bf0..0x00402d60, plus FUN_004011c0.
//
// Quatorze fonctions groupees dans une seule plage d'adresses : c'est une unite de compilation de
// 1998 prise telle quelle, et elle merite son fichier. Elle est appelee depuis un seul endroit —
// la tete du bloc d'affichage de FUN_00516fc0 — donc la regle de routage (2) d'ARCHITECTURE.md la
// place dans Screens/State03/.
//
// **Son etat etait deja modelise avant qu'on lise une ligne de son code.** Les globales en
// 0x005712xx..0x005713xx tombent dans GameMemory.ViewParams, cree a l'etape (1) du plan du
// 2026-08-21 sur la seule foi des adresses ; celles en 0x006067xx dans s_ViewBlock_006067c0. La
// note de l'epoque annoncait « il alimente s_ViewBlock_006067c0 par addition, recoit la camera en
// deux exemplaires paralleles, et porte des drapeaux a 0x0057139c » — c'est exactement ce que
// PublishCameraFrame_004022B0 fait, et la description tombe juste.
//
// **Les largeurs de champ sont deduites de l'usage, pas d'une passe de desassemblage.** Chaque
// acces ci-dessous reprend la largeur que la pseudo-C emploie au meme endroit. Quand Ghidra montre
// les deux pour un meme symbole, c'est signale sur place. TODO(REVERSE) : une passe `disasm` sur
// FUN_004011c0 confirmerait les quarante champs d'un coup.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // Les deux blocs d'etat, et les noms courts qui les rendent lisibles.
    //
    // Le bloc de vue (0x006067c0) porte QUATRE poses paralleles de la meme camera :
    //   +0x00 c0/c4/c8   la position visee     (« oeil »)
    //   +0x10 d0/d4/d8   la position courante  (« cible »)
    //   +0x20 e0/e2/e4   l'orientation visee   (trois shorts)
    //   +0x28 e8/ea/ec   l'orientation courante
    //   +0x30 f0/f4/f8   la position d'arrivee du glissement
    // =====================================================================

    private static int* ViewEye => (int*)Mem.s_ViewBlock_006067c0;
    private static int* ViewTarget => (int*)(Mem.s_ViewBlock_006067c0 + 0x10);
    private static short* ViewAngleGoal => (short*)(Mem.s_ViewBlock_006067c0 + 0x20);
    private static short* ViewAngle => (short*)(Mem.s_ViewBlock_006067c0 + 0x28);
    private static int* ViewTargetGoal => (int*)(Mem.s_ViewBlock_006067c0 + 0x30);

    /// <summary>Les drapeaux d'etat de la camera, <c>DAT_0057139c</c>. Un dword, dont seuls les
    /// huit bits bas sont poses : 1 = immobile, 2 = secousse, 8 = introduction, 0x20 = rotation,
    /// 0x40/0x80 = pilotage manuel en cours, 0x100 = glissement.</summary>
    private static uint* CameraFlags => (uint*)Mem.ViewParams(0x0057139c);

    /// <summary>Ghidra: DAT_0054f040 et DAT_0054f044 — les deux bornes de la hauteur de camera,
    /// relevees dans le binaire (voir <c>DataSymbols.md</c>). Ce sont des <c>short</c> initialises
    /// en <c>.data</c>, pas des variables : aucun code ne les ecrit.</summary>
    private const int s_CameraHeightMax_0054f040 = 1600;
    private const int s_CameraHeightMin_0054f044 = 900;

    /// <summary>Ghidra: DAT_005de550 — le <b>regime de camera</b> que le bloc d'affichage relit
    /// pour choisir son indicateur d'ecran : 0 automatique, 1 par defaut, 2 rotation ou scenette.
    /// Hors du bloc de camera, qui s'arrete a 0x005de100, d'ou un champ a part.</summary>
    private static byte s_Unknown_005de550 = 1;

    /// <summary>
    /// Ghidra: FUN_004011c0 @ 0x004011c0 — <b>le mode de vue</b>, quatre branches sur
    /// <c>DAT_005de54c</c> : <b>0</b> amorce la camera, <b>1</b> la pilote (c'est le mode de la
    /// carte du monde au repos), <b>2</b> et <b>3</b> la donnent a une scenette.
    ///
    /// <para>
    /// Le mode 1 est de loin le plus long. Il enchaine : glissement en cours (<c>0x100</c>) →
    /// publication de l'image de camera → detection du mouvement du meneur → introduction
    /// (<c>8</c>) → rotation (<c>0x20</c>) → pilotage manuel (<c>0x40</c>/<c>0x80</c>) →
    /// occlusion. Chaque etape peut rendre la main, et la sortie commune remonte la hauteur pour
    /// degager le groupe.
    /// </para>
    /// <para>
    /// Le sous-bloc d'occlusion (<see cref="ProbeCameraOcclusion_004011C0"/>, transcrit le
    /// 2026-11-27) tourne a chaque image qui arrive au bout : il n'a pas de garde de sortie, seul
    /// son travail est etale sur cinq images par le mot bas de <c>DAT_00571380</c>.
    /// </para>
    /// </summary>
    private static void ApplyViewMode_004011C0(int mode)
    {
        short* view = *(short**)Mem.Arena(0x005e9ae0);
        int* sceneCamera = *(int**)Mem.Entity(0x005edebc);
        int* sceneTarget = *(int**)Mem.Entity(0x005eca94);

        if (mode == 0)
        {
            // L'amorcage : les quatre poses recoivent la meme valeur, et les compteurs partent.
            ViewAngleGoal[0] = view[0];
            ViewAngleGoal[1] = view[1];
            ViewAngleGoal[2] = view[2];

            ViewTargetGoal[0] = *Mem.Camera(0x005de078);
            ViewTargetGoal[1] = *Mem.Camera(0x005de07c);
            ViewTargetGoal[2] = *Mem.Camera(0x005de080);

            *(int*)Mem.ViewParams(0x00571360) = sceneCamera[0];
            *(int*)Mem.ViewParams(0x00571364) = sceneCamera[1];
            *(int*)Mem.ViewParams(0x00571368) = sceneCamera[2];

            ViewAngle[0] = view[0];
            ViewAngle[2] = view[2];

            ViewTarget[0] = *Mem.Camera(0x005de078);
            ViewTarget[1] = *Mem.Camera(0x005de07c);
            ViewTarget[2] = *Mem.Camera(0x005de080);

            ViewEye[0] = sceneCamera[0];
            ViewEye[1] = sceneCamera[1];
            ViewEye[2] = sceneCamera[2];

            *(uint*)Mem.ViewParams(0x00571380) &= 0xffff0000;
            *(short*)Mem.ViewParams(0x0057128e) = -1;
            *(short*)Mem.ViewParams(0x00571276) = -1;
            *(short*)Mem.ViewParams(0x005713a0) = 10;
            *(short*)Mem.ViewParams(0x005713b0) = 0;
            return;
        }

        if (mode == 2 || mode == 3)
        {
            // La scenette : la camera suit le bloc de cible et pose son drapeau.
            ViewTarget[0] = sceneTarget[0];
            ViewTarget[1] = sceneTarget[1];
            ViewTarget[2] = sceneTarget[2];
            *CameraFlags |= 0x100;
            return;
        }

        if (mode != 1)
        {
            RaiseCameraOverParty();
            return;
        }

        s_Unknown_005de550 = 1;

        if ((*CameraFlags & 0x100) != 0)
        {
            // Un glissement est en cours : tant qu'il n'a pas converge, rien d'autre ne se passe.
            s_Unknown_005de550 = (byte)((*CameraFlags >> 4) & 2);
            if (AdvanceCameraGlide_00402410() == 0)
            {
                RaiseCameraOverParty();
                return;
            }
        }

        PublishCameraFrame_004022B0();

        (*(short*)Mem.ViewParams(0x005713a0))--;
        if (*(short*)Mem.ViewParams(0x005713a0) < 1)
        {
            *(short*)Mem.ViewParams(0x005713a0) = 0;
            ReadCameraPad();
        }

        if ((*CameraFlags & 8) != 0)
        {
            s_Unknown_005de550 = (byte)((*CameraFlags >> 4) & 2);
            RunCameraIntro_00401FB0();
            RaiseCameraOverParty();
            return;
        }

        if ((*CameraFlags & 0x20) != 0)
        {
            s_Unknown_005de550 = 2;
            ManualCameraControl_00401C90();
            RaiseCameraOverParty();
            return;
        }

        if ((*CameraFlags & 0xc0) != 0)
        {
            s_Unknown_005de550 = 0;
            ManualCameraControl_00401C90();
            if ((*CameraFlags & 0x40) != 0)
            {
                RaiseCameraOverParty();
                return;
            }
        }

        // Un membre du groupe en mouvement fige la camera automatique.
        for (int i = 0; i < 4; i++)
        {
            if ((Inp.s_SlotFlags_005e93c0[i * 0x9c] & 1) != 0)
            {
                RaiseCameraOverParty();
                return;
            }
        }

        if ((*CameraFlags & 0x78) != 0)
        {
            RaiseCameraOverParty();
            return;
        }

        // Le glissement d'angle en cours, borne par les deux limites de DAT_005e9b34.
        short* limits = *(short**)Mem.Arena(0x005e9b34);
        if (*(short*)Mem.ViewParams(0x0057128e) > 0)
        {
            short yaw = (short)(ViewAngle[0] + *(short*)Mem.ViewParams(0x00571288));
            if (limits[1] < yaw) yaw -= (short)((yaw - limits[1]) >> 3);
            if (yaw < limits[0]) yaw += (short)((limits[0] - yaw) >> 3);

            ViewAngle[1] += *(short*)Mem.ViewParams(0x0057128a);
            ViewAngle[0] = yaw;
            ViewAngle[2] += *(short*)Mem.ViewParams(0x0057128c);
        }

        (*(short*)Mem.ViewParams(0x0057128e))--;
        if (*(short*)Mem.ViewParams(0x0057128e) < -3000)
        {
            *(short*)Mem.ViewParams(0x0057128e) = -3000;
        }

        if (*(short*)Mem.ViewParams(0x00571276) > 0)
        {
            ViewTarget[0] += *(short*)Mem.ViewParams(0x00571270);
            ViewTarget[1] += *(short*)Mem.ViewParams(0x00571272);
            ViewTarget[2] += *(short*)Mem.ViewParams(0x00571274);

            if (s_CameraHeightMax_0054f040 < ViewTarget[2])
            {
                ViewTarget[2] -= (ViewTarget[2] - s_CameraHeightMax_0054f040) >> 3;
            }

            if (ViewTarget[2] < s_CameraHeightMin_0054f044)
            {
                ViewTarget[2] += (s_CameraHeightMin_0054f044 - ViewTarget[2]) >> 3;
            }
        }

        (*(short*)Mem.ViewParams(0x00571276))--;
        if (*(short*)Mem.ViewParams(0x00571276) < -3000)
        {
            *(short*)Mem.ViewParams(0x00571276) = -3000;
        }

        // Immobile depuis longtemps : la camera se met a deriver toute seule, deux glissements
        // tires au sort.
        if (*(short*)Mem.ViewParams(0x005713a8) < 0x259)
        {
            *CameraFlags &= 0xfffffffe;
        }
        else
        {
            *CameraFlags |= 1;

            if (*(short*)Mem.ViewParams(0x0057128e) < 1
                && *(short*)Mem.ViewParams(0x00571276) < 1)
            {
                int yaw = CrtRand() % 300 - 0x96;
                int pitch = CrtRand() % 0xfa + 10;
                int roll = CrtRand();
                pitch += ((pitch < 1 ? 1 : 0) - 1) & 800;
                pitch -= 400;

                StartAngleGlide_004025A0(
                    (short)(roll % 0x32 + 0x50),
                    (short)(((((yaw < 1 ? 1 : 0) - 1) & 400) - 200) + yaw + ViewAngle[0]),
                    (short)(pitch + ViewAngle[1]),
                    0);

                int distance = CrtRand() % 500 - 0xfa;
                int seed = CrtRand();
                distance = ViewTarget[2] + ((((distance < 1 ? 1 : 0) - 1) & 400) - 200) + distance;

                StartPositionGlide_00402610(
                    (short)(seed % 0x32 + 0x50),
                    (short)ViewTarget[0],
                    (short)ViewTarget[1],
                    (short)distance);
            }
        }

        if ((*CameraFlags & 1) != 0)
        {
            RaiseCameraOverParty();
            return;
        }

        // Le recadrage : quand le groupe s'est deplace assez longtemps dans la meme direction, la
        // camera se replace derriere lui.
        if ((*CameraFlags & 0x83) == 0
            && (*(short*)Mem.ViewParams(0x0057129c) > 0x14
                || *(short*)Mem.ViewParams(0x00571330) > 0x3c))
        {
            *(int*)Mem.ViewParams(0x00571300) = *(int*)Mem.ViewParams(0x00571370);
            *(int*)Mem.ViewParams(0x00571304) = *(int*)Mem.ViewParams(0x00571374);
            *(int*)Mem.ViewParams(0x00571308) = *(int*)Mem.ViewParams(0x00571378);

            MeasureCameraToParty_00402710();

            ViewTarget[1] = *(int*)Mem.ViewParams(0x0057133c);
            ViewTarget[0] = *(int*)Mem.ViewParams(0x00571338);
            ViewAngle[2] = *(short*)Mem.ViewParams(0x005712fc);

            short yaw = *(short*)Mem.ViewParams(0x005712f8);
            int height = *(int*)Mem.ViewParams(0x00571340);
            ViewTarget[2] = height;

            bool clampedHigh = s_CameraHeightMax_0054f040 < height;
            if (clampedHigh)
            {
                ViewTarget[2] = height - ((height - s_CameraHeightMax_0054f040) >> 3);
            }

            bool clampedLow = ViewTarget[2] < s_CameraHeightMin_0054f044 + 0xfa;
            if (clampedLow)
            {
                ViewTarget[2] += (s_CameraHeightMin_0054f044 - ViewTarget[2] + 0xfa) >> 3;
            }

            short* range = *(short**)Mem.Arena(0x005e9b34);
            bool aboveRange = range[1] < yaw;
            if (aboveRange) yaw -= 10;

            bool belowRange = yaw < range[0];
            if (belowRange) yaw += 10;

            ViewAngle[0] = yaw;

            if (belowRange || aboveRange || clampedLow || clampedHigh)
            {
                // Une des bornes a mordu : on recommence la mesure sur la pose corrigee.
                ProjectCameraTarget_00402670();
                MeasureCameraToParty_00402710();

                ViewAngle[0] = *(short*)Mem.ViewParams(0x005712f8);
                ViewAngle[2] = *(short*)Mem.ViewParams(0x005712fc);
                ViewAngle[1] = *(short*)Mem.ViewParams(0x005712fa);
                ViewTarget[0] = *(int*)Mem.ViewParams(0x00571338);
                ViewTarget[1] = *(int*)Mem.ViewParams(0x0057133c);
                ViewTarget[2] = *(int*)Mem.ViewParams(0x00571340);
            }
        }

        // La secousse, puis l'occlusion. DAT_00571348 est un MOT (`cmp word [571348h], bx` a
        // 0x004017fb) : c'est le nombre de faces qui coupaient la vue au dernier tour complet.
        if (*(short*)Mem.ViewParams(0x00571348) != 0
            && *(short*)Mem.ViewParams(0x00571380) == 0
            && *(short*)Mem.ViewParams(0x0057127e) < 0x3c)
        {
            *CameraFlags |= 2;
            StartAngleGlide_004025A0(
                5,
                (short)(ViewAngle[0] + *(short*)Mem.ViewParams(0x00571278) * 0x14),
                (short)(*(short*)Mem.ViewParams(0x0057127a) * 100 + ViewAngle[1]),
                ViewAngle[2]);
            (*(short*)Mem.ViewParams(0x0057127e))++;
        }

        ProbeCameraOcclusion_004011C0();
        RaiseCameraOverParty();
    }

    /// <summary>
    /// Ghidra: <c>LAB_00401b6d</c> — la sortie commune des quatre modes : quand ni le pilotage
    /// manuel ni la rotation ne sont actifs, la camera remonte pour degager le groupe. La hauteur
    /// visee est <b>le double de l'ecartement du groupe</b>, bornee en bas par
    /// <c>DAT_0054f044</c> et jamais inferieure a la hauteur courante — la camera monte, elle ne
    /// redescend pas par ce chemin.
    /// </summary>
    private static void RaiseCameraOverParty()
    {
        if ((*CameraFlags & 0x28) == 0)
        {
            int wanted = WidestPartySpread_00401BF0() * 2;
            if (wanted < s_CameraHeightMin_0054f044) wanted = s_CameraHeightMin_0054f044;
            if (wanted < ViewTarget[2]) wanted = ViewTarget[2];

            ViewTarget[2] += (wanted - ViewTarget[2]) >> 3;
        }

        // Ghidra: FUN_00401fa0() — corps vide, verifie.
    }

    /// <summary>
    /// Ghidra: FUN_00401bf0 @ 0x00401bf0 — <b>l'ecartement du groupe</b> : la plus grande distance
    /// au sol entre deux membres actifs, parmi les seize paires.
    ///
    /// <para>La boucle est ecrite sur <b>quatre</b> membres en dur, pas sur
    /// <c>DAT_005e9020</c> — c'est ce que fait l'original, et le groupe en compte quatre au
    /// maximum.</para>
    /// </summary>
    private static int WidestPartySpread_00401BF0()
    {
        int widest = 0;

        for (int a = 0; a < 4; a++)
        {
            uint* first = (uint*)(Inp.s_SlotFlags_005e93c0 + a * 0x9c);

            for (int b = 0; b < 4; b++)
            {
                uint* second = (uint*)(Inp.s_SlotFlags_005e93c0 + b * 0x9c);

                if (a != b
                    && (*first & 0x80000000) != 0 && (*first & 0x50000000) == 0
                    && (*second & 0x80000000) != 0 && (*second & 0x50000000) == 0)
                {
                    int dx = (int)(first[7] - second[7]) >> 0xc;
                    int dz = (int)(first[9] - second[9]) >> 0xc;
                    int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
                    if (widest < distance) widest = distance;
                }
            }
        }

        return widest;
    }

    /// <summary>
    /// Ghidra: FUN_00402410 @ 0x00402410 — <b>le glissement</b> : position et orientation
    /// rejoignent leur but d'un quart de l'ecart par image. Rend non nul des que les trois axes de
    /// position sont a moins de six unites du but — auquel cas les quatre poses sont alignees et le
    /// drapeau <c>0x100</c> tombe.
    ///
    /// <para>Les trois angles utilisent le meme pas module <c>0x1000</c> que
    /// <see cref="SmoothCameraStep"/>, mais sur un ecart <b>non masque</b> : la comparaison a
    /// <c>0x800</c> se fait sur la difference brute.</para>
    /// </summary>
    private static int AdvanceCameraGlide_00402410()
    {
        ViewTarget[0] += (ViewTargetGoal[0] - ViewTarget[0]) >> 2;
        ViewTarget[2] += (ViewTargetGoal[2] - ViewTarget[2]) >> 2;
        ViewTarget[1] += (ViewTargetGoal[1] - ViewTarget[1]) >> 2;

        for (int axis = 0; axis < 3; axis++)
        {
            int delta = ViewAngleGoal[axis] - ViewAngle[axis];
            ViewAngle[axis] += delta < 0x800
                ? (short)(delta >> 2)
                : (short)-((0x1000 - delta) >> 2);
        }

        int dx = Math.Abs(ViewTarget[0] - ViewTargetGoal[0]);
        int dy = Math.Abs(ViewTarget[1] - ViewTargetGoal[1]);
        int dz = Math.Abs(ViewTarget[2] - ViewTargetGoal[2]);

        if (dx < 6 && dy < 6 && dz < 6)
        {
            *CameraFlags &= 0xfffffeff;
            ViewTargetGoal[2] = ViewTarget[2];
            ViewTargetGoal[0] = ViewTarget[0];
            ViewAngleGoal[0] = ViewAngle[0];
            ViewTargetGoal[1] = ViewTarget[1];
            ViewAngleGoal[1] = ViewAngle[1];
            ViewAngleGoal[2] = ViewAngle[2];
            *(short*)Mem.ViewParams(0x0057129c) = 0;
            *(short*)Mem.ViewParams(0x00571330) = 0x1e;
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_004022b0 @ 0x004022b0 — <b>l'image de camera</b> : la pose courante devient la
    /// pose precedente, et la nouvelle pose est relue depuis les blocs de scene. C'est ce
    /// decalage d'une image qui donne aux autres fonctions un « avant » et un « apres ».
    /// </summary>
    private static void PublishCameraFrame_004022B0()
    {
        ApplyPadHeightLimits_004021E0();

        *(int*)Mem.ViewParams(0x00571370) = *(int*)Mem.ViewParams(0x00571300);
        ViewAngleGoal[1] = (short)(ViewAngle[1] & 0xfff);
        *(int*)Mem.ViewParams(0x00571374) = *(int*)Mem.ViewParams(0x00571304);
        *(int*)Mem.ViewParams(0x00571378) = *(int*)Mem.ViewParams(0x00571308);
        ViewAngleGoal[2] = (short)(ViewAngle[2] & 0xfff);
        ViewAngleGoal[0] = ViewAngle[0];

        ViewTargetGoal[0] = ViewTarget[0];
        ViewTargetGoal[1] = ViewTarget[1];
        ViewTargetGoal[2] = ViewTarget[2];

        *(int*)Mem.ViewParams(0x00571360) = ViewEye[0];
        *(int*)Mem.ViewParams(0x00571364) = ViewEye[1];
        *(int*)Mem.ViewParams(0x00571368) = ViewEye[2];

        short* view = *(short**)Mem.Arena(0x005e9ae0);
        ViewAngle[0] = view[0];
        ViewAngle[1] = view[1];
        ViewAngle[2] = view[2];

        int* sceneCamera = *(int**)Mem.Entity(0x005edebc);
        ViewEye[0] = sceneCamera[0];
        ViewEye[1] = sceneCamera[1];
        ViewEye[2] = sceneCamera[2];

        int* sceneTarget = *(int**)Mem.Entity(0x005eca94);
        ViewTarget[0] = sceneTarget[0];
        ViewTarget[1] = sceneTarget[1];
        ViewTarget[2] = sceneTarget[2];

        ProjectCameraTarget_00402670();
        ClassifyLeaderMotion_004027F0();

        // Le compteur d'immobilite du meneur : remis a zero des qu'il agit.
        if ((Inp.s_SlotFlags_005e93c0[(byte)Mem.s_Unknown_005de538 * 0x9c] & 6) != 0)
        {
            *(short*)Mem.ViewParams(0x00571330) = 0;
            return;
        }

        (*(short*)Mem.ViewParams(0x00571330))++;
        if (*(short*)Mem.ViewParams(0x00571330) > 10000)
        {
            *(short*)Mem.ViewParams(0x00571330) = 10000;
        }
    }

    /// <summary>
    /// Ghidra: FUN_004021e0 @ 0x004021e0 — <b>les bornes de distance et l'amortissement des
    /// decalages manuels</b>. La distance de camera (<c>DAT_00564660</c>) est ramenee dans
    /// <c>[0x226, 0x4b0]</c> d'un quart de l'exces par image, et les deux decalages que le mode de
    /// mise au point pose (<c>DAT_00564658</c>, <c>DAT_0056465c</c>) perdent un huitieme.
    /// </summary>
    private static void ApplyPadHeightLimits_004021E0()
    {

        int* reach = (int*)(s_WorldMapCamera_00564650 + 0x10);

        if (*reach > 0x4af) *reach += (0x4b0 - *reach) >> 2;
        if (*reach < 0x226) *reach += (0x226 - *reach) >> 2;

        int lift = Winsys3d.GetCosTableEntry(*(short*)Mem.s_CameraVector_00604928);

        *Mem.Camera(0x005de090) +=
            ((*Mem.Camera(0x005de05c) >> 1) + *Mem.Camera(0x005de05c) - *Mem.Camera(0x005de090)) >> 2;

        // Les deux decalages sont des ENTIERS, pas des shorts : `mov edx,[564658h]` et
        // `mov esi,[56465Ch]` en 0x0040223d / 0x00402250. Les lire sur seize bits tronquait
        // l'amortissement a chaque image, en plus de la troncature d'origine au chargement de zone
        // (voir WorldMap.cs). Corrige le 2026-08-23.
        int* slideX = (int*)(s_WorldMapCamera_00564650 + 8);
        int* slideZ = (int*)(s_WorldMapCamera_00564650 + 0x0c);
        *slideX -= *slideX >> 3;
        *slideZ -= *slideZ >> 3;

        *Mem.Camera(0x005de08c) = lift >> 5;
        *Mem.Camera(0x005de07c) = (lift >> 5) + *slideZ;
        *Mem.Camera(0x005de078) = *slideX + *Mem.Camera(0x005de088);
        *Mem.Camera(0x005de080) = *Mem.Camera(0x005de090) + *reach;

        ViewTarget[0] = *Mem.Camera(0x005de078);
        ViewTarget[1] = *Mem.Camera(0x005de07c);
    }

    /// <summary>
    /// Ghidra: FUN_00402670 @ 0x00402670 — la position de l'oeil deduite de l'orientation et de la
    /// distance : un sinus et un cosinus sur chacun des deux angles, en 1.12.
    /// </summary>
    private static void ProjectCameraTarget_00402670()
    {
        int distance = ViewTarget[2];

        int sinPitch = (int)Winsys3d.GetSinTableEntry(ViewAngle[1]);
        int cosPitch = Winsys3d.GetCosTableEntry(ViewAngle[1]);
        int sinYaw = (int)Winsys3d.GetSinTableEntry(ViewAngle[0]);
        int cosYaw = Winsys3d.GetCosTableEntry(ViewAngle[0]);

        *(int*)Mem.ViewParams(0x00571304) = sinYaw * distance + ViewEye[1];
        *(int*)Mem.ViewParams(0x00571300) = cosYaw * (sinPitch * distance >> 0xc) + ViewEye[0];
        *(int*)Mem.ViewParams(0x00571308) = ViewEye[2] - cosYaw * (cosPitch * distance >> 0xc);
    }

    /// <summary>
    /// Ghidra: FUN_00402710 @ 0x00402710 — <b>l'angle et la distance qui remettraient la camera
    /// derriere le groupe</b>. Le resultat sert au recadrage, pas a la pose courante.
    ///
    /// <para>
    /// Le lacet est normalise dans <c>[0, 0x400]</c> par une <b>boucle de soustraction</b> plutot
    /// que par un modulo — la forme du binaire, conservee : elle retire <c>0x400</c> autant de fois
    /// que necessaire, le nombre de tours etant calcule d'avance.
    /// </para>
    /// </summary>
    private static void MeasureCameraToParty_00402710()
    {
        int dz = (*Mem.Camera(0x005de070) >> 0xc) - (*(int*)Mem.ViewParams(0x00571308) >> 0xc);
        int dx = (*Mem.Camera(0x005de068) >> 0xc) - (*(int*)Mem.ViewParams(0x00571300) >> 0xc);
        int flat = dz * dz + dx * dx;
        int dy = (*Mem.Camera(0x005de06c) >> 0xc) - (*(int*)Mem.ViewParams(0x00571304) >> 0xc);

        int ground = (int)Winsys3d.Ordinal_442((uint)flat);
        short pitch = (short)Winsys3d.Ordinal_457(dy, ground);

        short normalized = (short)-pitch;
        if (normalized < 0) normalized += (short)((pitch + 0x3ff) & 0xfc00);

        if (normalized > 0x400)
        {
            uint turns = (uint)(normalized - 1) >> 10;
            do
            {
                normalized -= 0x400;
                turns--;
            }
            while (turns != 0);
        }

        *(short*)Mem.ViewParams(0x005712f8) = normalized;
        *(short*)Mem.ViewParams(0x005712fa) = (short)-Winsys3d.Ordinal_457(dx, dz);

        // La distance en trois dimensions : dy² + dz² + dx², c'est-a-dire `dy² + flat`.
        //
        // **Corrige le 2026-08-23**, sur le releve x32dbg de Vincent. Cette ligne comptait
        // `dx * dx` DEUX FOIS. Ghidra rend la somme en `dy*dy + [esp+0x10] + ebp`, et
        // `[esp+0x10]` avait ete pris pour `flat` alors qu'il ne porte que `dz²` : le
        // desassembleur le montre range en 0x00402752, **avant** que `eax` ne devienne
        // `dz² + dx²` a l'instruction suivante.
        //
        //     00402752  89442410   mov [esp+10h],eax   ; dz²  (eax ne vaut pas encore flat)
        //     00402756  03c5       add eax,ebp         ; eax = flat = dz² + dx²
        //     004027c1  03442410   add eax,[esp+10h]   ; dy² + dz²
        //     004027c5  03c5       add eax,ebp         ; + dx²
        //
        // Le resultat est la distance camera-groupe qui pilote le recadrage : la surestimer
        // repoussait la camera et faussait tous les angles qui en decoulent.
        *(int*)Mem.ViewParams(0x00571340) =
            (int)Winsys3d.Ordinal_442((uint)(dy * dy + flat));
        *(int*)Mem.ViewParams(0x00571338) = ViewTarget[0];
        *(int*)Mem.ViewParams(0x0057133c) = ViewTarget[1];
    }

    /// <summary>
    /// Ghidra: FUN_004025a0 @ 0x004025a0 — arme un glissement d'orientation en
    /// <paramref name="frames"/> images : les trois pas sont l'ecart divise par le compte. Le pas
    /// de lacet passe d'abord par le plus court chemin (<c>±0x800</c>).
    /// </summary>
    private static void StartAngleGlide_004025A0(short frames, short yaw, short pitch, short roll)
    {
        pitch -= ViewAngle[1];
        if (pitch > 0x800 || pitch < -0x800) pitch += 0x1000;

        *(short*)Mem.ViewParams(0x0057128e) = frames;
        *(short*)Mem.ViewParams(0x00571288) = (short)((yaw - ViewAngle[0]) / frames);
        *(short*)Mem.ViewParams(0x0057128a) = (short)(pitch / frames);
        *(short*)Mem.ViewParams(0x0057128c) = (short)((roll - ViewAngle[2]) / frames);
    }

    /// <summary>Ghidra: FUN_00402610 @ 0x00402610 — la jumelle de
    /// <see cref="StartAngleGlide_004025A0"/> pour la position, sans le plus court chemin.</summary>
    private static void StartPositionGlide_00402610(short frames, short x, short y, short z)
    {
        *(short*)Mem.ViewParams(0x00571276) = frames;
        *(short*)Mem.ViewParams(0x00571270) = (short)((x - ViewTarget[0]) / frames);
        *(short*)Mem.ViewParams(0x00571272) = (short)((y - ViewTarget[1]) / frames);
        *(short*)Mem.ViewParams(0x00571274) = (short)((z - ViewTarget[2]) / frames);
    }

    /// <summary>
    /// Ghidra: FUN_004027f0 @ 0x004027f0 — <b>dans quelle des huit directions le groupe avance</b>,
    /// et depuis combien d'images. C'est ce compteur que le recadrage interroge.
    ///
    /// <para>
    /// <b>La cascade de labels de Ghidra n'est pas irreductible</b> : les huit <c>goto</c> qui
    /// s'entrelacent sont une quantification d'angle en huit secteurs de <c>0x200</c>, ecrite par
    /// le compilateur en chaine de comparaisons. Chaque branche assigne puis saute au maillon
    /// suivant, qui echoue toujours — le seul effet net est le secteur. Rendue en <c>if/else if</c>.
    /// </para>
    /// <para>
    /// Ghidra rend le secteur comme un <c>unaff_DI</c> jamais initialise. Ce n'est pas un registre
    /// perdu : les huit branches couvrent la plage entiere, donc il est toujours ecrit avant
    /// d'etre lu.
    /// </para>
    /// </summary>
    private static void ClassifyLeaderMotion_004027F0()
    {
        *(short*)Mem.ViewParams(0x005712f0) = *(short*)Mem.ViewParams(0x005712d8);

        // Un membre en transition suspend la mesure.
        for (int i = 0; i < 4; i++)
        {
            uint* member = (uint*)(Inp.s_SlotFlags_005e93c0 + i * 0x9c);
            if ((*member & 0x80000000) != 0 && (*member & 0x50000000) == 0
                && *(ushort*)((byte*)member + 0x12) != 0)
            {
                *(short*)Mem.ViewParams(0x005713b8) = 10;
                return;
            }
        }

        if (*(short*)Mem.ViewParams(0x005713b8) - 1 > 0)
        {
            *(short*)Mem.ViewParams(0x005712f0) = *(short*)Mem.ViewParams(0x005712d8);
            (*(short*)Mem.ViewParams(0x005713b8))--;
            return;
        }

        uint movedX = (uint)(*(int*)Mem.ViewParams(0x00571388) - *Mem.Camera(0x005de068));
        *(short*)Mem.ViewParams(0x005713b8) = 0;
        uint movedZ = (uint)(*(int*)Mem.ViewParams(0x00571390) - *Mem.Camera(0x005de070));

        int angle = Winsys3d.Ordinal_457((int)movedX, (int)movedZ) + 0x800;
        if (angle > 0xfff) angle -= 0x1000;

        short sector;
        if (angle > 0xeff || angle < 0x100) sector = 1;
        else if (angle < 0x300) sector = 2;
        else if (angle < 0x500) sector = 3;
        else if (angle < 0x700) sector = 4;
        else if (angle < 0x900) sector = 5;
        else if (angle < 0xb00) sector = 6;
        else if (angle < 0xd00) sector = 7;
        else sector = 8;

        // Un deplacement inferieur a une unite monde ne compte pas.
        if ((movedX & 0xfffff000) == 0 && (movedZ & 0xfffff000) == 0) sector = 0;

        *(int*)Mem.ViewParams(0x00571388) = *Mem.Camera(0x005de068);
        *(int*)Mem.ViewParams(0x00571390) = *Mem.Camera(0x005de070);
        *(int*)Mem.ViewParams(0x0057138c) = *Mem.Camera(0x005de06c);
        *(short*)Mem.ViewParams(0x005712d8) = sector;

        if (sector == 0)
        {
            (*(short*)Mem.ViewParams(0x005713a8))++;
            if (*(short*)Mem.ViewParams(0x005713a8) > 10000)
            {
                *(short*)Mem.ViewParams(0x005713a8) = 10000;
            }
        }
        else
        {
            *(short*)Mem.ViewParams(0x005713a8) = 0;
        }

        if (*(short*)Mem.ViewParams(0x005712f0) == sector)
        {
            (*(short*)Mem.ViewParams(0x0057129c))++;
            if (*(short*)Mem.ViewParams(0x0057129c) > 10000)
            {
                *(short*)Mem.ViewParams(0x0057129c) = 10000;
            }
        }
        else
        {
            *(short*)Mem.ViewParams(0x0057129c) = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00401fb0 @ 0x00401fb0 — <b>l'introduction</b> : une machine a cinq etats dans
    /// <c>DAT_005713b0</c> qui recule la camera, attend que le groupe bouge, puis la ramene.
    /// L'etat 0 met de cote la pose, arme deux glissements longs, et l'etat 4 rend la main en
    /// effaçant les bits 8 et 0x10.
    ///
    /// <para>Les <c>CONCAT22(extraout_var, …)</c> de Ghidra sont des demi-mots de retour perdus :
    /// les deux appelees prennent des <c>short</c>, donc seule la moitie basse compte.</para>
    /// </summary>
    private static void RunCameraIntro_00401FB0()
    {
        ApplyPadHeightLimits_004021E0();

        short* stage = (short*)Mem.ViewParams(0x005713b0);

        if (*stage == 0)
        {
            *(short*)Mem.ViewParams(0x005712e0) = ViewAngle[0];
            *(short*)Mem.ViewParams(0x005712e2) = ViewAngle[1];
            *(short*)Mem.ViewParams(0x005712e4) = ViewAngle[2];
            *(short*)Mem.ViewParams(0x00571290) = (short)ViewTarget[0];
            *(short*)Mem.ViewParams(0x00571292) = (short)ViewTarget[1];
            *(short*)Mem.ViewParams(0x00571294) = (short)ViewTarget[2];

            StartPositionGlide_00402610(10, 0, 0, 0xaf0);
            StartAngleGlide_004025A0(10, 0x400, 0, 0);
            *stage = 1;
        }

        if (*stage == 1
            && *(short*)Mem.ViewParams(0x0057128e) < -0x28
            && *(short*)Mem.ViewParams(0x00571276) < -0x28)
        {
            *(int*)Mem.ViewParams(0x00571310) = *Mem.Camera(0x005de068);
            *stage = 2;
            *(int*)Mem.ViewParams(0x00571314) = *Mem.Camera(0x005de06c);
            *(int*)Mem.ViewParams(0x00571318) = *Mem.Camera(0x005de070);
        }

        if (*stage == 2
            && (*(int*)Mem.ViewParams(0x00571310) != *Mem.Camera(0x005de068)
                || *(int*)Mem.ViewParams(0x00571314) != *Mem.Camera(0x005de06c)
                || *(int*)Mem.ViewParams(0x00571318) != *Mem.Camera(0x005de070)))
        {
            *stage = 3;
        }

        if (*stage == 3)
        {
            StartPositionGlide_00402610(6,
                *(short*)Mem.ViewParams(0x00571290),
                *(short*)Mem.ViewParams(0x00571292),
                *(short*)Mem.ViewParams(0x00571294));

            StartAngleGlide_004025A0(6, *(short*)Mem.ViewParams(0x005712e0), 0,
                *(short*)Mem.ViewParams(0x005712e4));

            *stage = 4;
        }

        if (*stage == 4
            && *(short*)Mem.ViewParams(0x0057128e) < 0
            && *(short*)Mem.ViewParams(0x00571276) < 0)
        {
            *CameraFlags &= 0xffffffe7;
            *stage = 0;
        }

        if (*(short*)Mem.ViewParams(0x0057128e) > 0)
        {
            ViewAngle[0] += *(short*)Mem.ViewParams(0x00571288);
            ViewAngle[1] += *(short*)Mem.ViewParams(0x0057128a);
            ViewAngle[2] += *(short*)Mem.ViewParams(0x0057128c);
        }

        (*(short*)Mem.ViewParams(0x0057128e))--;
        if (*(short*)Mem.ViewParams(0x0057128e) < -3000)
        {
            *(short*)Mem.ViewParams(0x0057128e) = -3000;
        }

        if (*(short*)Mem.ViewParams(0x00571276) > 0)
        {
            ViewTarget[0] += *(short*)Mem.ViewParams(0x00571270);
            ViewTarget[1] += *(short*)Mem.ViewParams(0x00571272);
            ViewTarget[2] += *(short*)Mem.ViewParams(0x00571274);
        }

        (*(short*)Mem.ViewParams(0x00571276))--;
        if (*(short*)Mem.ViewParams(0x00571276) < -3000)
        {
            *(short*)Mem.ViewParams(0x00571276) = -3000;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00401c90 @ 0x00401c90 — <b>le pilotage manuel</b> : les quatre gachettes font
    /// tourner la camera et changer sa distance, et chaque action reamorce le compte a rebours de
    /// <c>DAT_005713ac</c> qui finit par rendre la main a la camera automatique.
    ///
    /// <para>
    /// Les combinaisons opposees (0x400+0x800, 0x100+0x200) montent et descendent ; seules, elles
    /// font tourner. Le lacet reste borne par les deux valeurs de <c>DAT_005e9b34</c>, avec un
    /// rappel d'un quart quand la borne est franchie et de dix unites sinon.
    /// </para>
    /// <para>
    /// <b>La lecture de la manette passe par une triple indirection</b> :
    /// <c>DAT_005e9188[DAT_005e93c8[meneur * 0x9c] * 0x4c]</c> donne l'emplacement, puis
    /// <c>DAT_00604a20 + emplacement * 0x10</c> l'etat. La meme forme revient dans
    /// <see cref="ReadCameraPad"/>.
    /// </para>
    /// </summary>
    private static void ManualCameraControl_00401C90()
    {
        short drift;
        if (*(short*)Mem.ViewParams(0x00571280) < 1)
        {
            drift = (short)(((sbyte)(ViewAngle[1] >> 8) * 0x100 - ViewAngle[1]) >> 3);
        }
        else
        {
            (*(short*)Mem.ViewParams(0x00571280))--;
            drift = *(short*)Mem.ViewParams(0x00571398);
        }

        ushort pad = *(ushort*)LeaderPadState();
        short* range = *(short**)Mem.Arena(0x005e9b34);

        if ((pad & 0x400) != 0 && (pad & 0x800) != 0)
        {
            ViewTarget[2] += 0x28;
            if (s_CameraHeightMax_0054f040 + 100 < ViewTarget[2])
            {
                ViewTarget[2] += (short)(s_CameraHeightMax_0054f040 - ViewTarget[2] + 100) >> 1;
            }

            MarkManualCameraActive();
        }

        if ((pad & 0x100) != 0 && (pad & 0x200) != 0)
        {
            ViewTarget[2] -= 0x28;
            if (ViewTarget[2] < s_CameraHeightMin_0054f044 - 100)
            {
                ViewTarget[2] += (short)(s_CameraHeightMin_0054f044 - ViewTarget[2] - 100) >> 1;
            }

            MarkManualCameraActive();
        }

        if ((pad & 0x400) != 0)
        {
            if ((pad & 0x800) == 0)
            {
                if (*(short*)Mem.ViewParams(0x00571280) == 0)
                {
                    *(short*)Mem.ViewParams(0x00571398) = 0x40;
                    *(short*)Mem.ViewParams(0x00571280) = 4;
                }

                MarkManualCameraActive();
            }
        }
        else if ((pad & 0x800) != 0)
        {
            if (*(short*)Mem.ViewParams(0x00571280) == 0)
            {
                *(short*)Mem.ViewParams(0x00571398) = -0x40;
                *(short*)Mem.ViewParams(0x00571280) = 4;
            }

            MarkManualCameraActive();
        }

        if ((pad & 0x100) != 0)
        {
            ViewAngle[0] -= 0x20;
            if (ViewAngle[0] < range[0])
            {
                ushort wrapped = (ushort)((range[0] - ViewAngle[0]) & 0xfff);
                ViewAngle[0] += wrapped < 0x800
                    ? (short)((short)wrapped >> 2)
                    : (short)-((0x1000 - (short)wrapped) >> 2);
            }

            MarkManualCameraActive();
        }

        if ((pad & 0x200) != 0)
        {
            ViewAngle[0] += 0x20;
            if (range[1] < ViewAngle[0])
            {
                int over = range[1] - ViewAngle[0];
                ViewAngle[0] += (short)((over + ((over >> 0x1f) & 3)) >> 2);
            }

            MarkManualCameraActive();
        }

        if (range[1] < ViewAngle[0]) ViewAngle[0] -= 10;
        if (ViewAngle[0] < range[0]) ViewAngle[0] += 10;

        ViewAngle[1] += drift;

        if (s_CameraHeightMax_0054f040 < ViewTarget[2])
        {
            ViewTarget[2] += (s_CameraHeightMax_0054f040 - ViewTarget[2]) >> 3;
        }

        if (ViewTarget[2] < s_CameraHeightMin_0054f044)
        {
            ViewTarget[2] += (s_CameraHeightMin_0054f044 - ViewTarget[2]) >> 3;
        }

        (*(short*)Mem.ViewParams(0x005713ac))++;
        if (*(short*)Mem.ViewParams(0x005713ac) > 0x32)
        {
            *CameraFlags = (*CameraFlags & 0xffffffbf) | 0x80;
        }

        if (*(short*)Mem.ViewParams(0x005713ac) > 100)
        {
            *(short*)Mem.ViewParams(0x005713ac) = 0;
            *CameraFlags &= 0xffffff7f;
        }
    }

    /// <summary>Les trois ecritures que chaque action de pilotage repete : le compte a rebours
    /// repart, le bit 0x80 tombe et le 0x40 se pose.</summary>
    private static void MarkManualCameraActive()
    {
        *(short*)Mem.ViewParams(0x005713ac) = 0;
        *CameraFlags = (*CameraFlags & 0xffffff7f) | 0x40;
    }

    /// <summary>L'etat de manette du meneur, par la triple indirection que le sous-systeme
    /// repete.</summary>
    private static byte* LeaderPadState()
    {
        byte character = Inp.s_SlotFlags_005e93c0[(uint)(byte)Mem.s_Unknown_005de538 * 0x9c + 8];
        byte slot = Inp.s_PlayerSlots_005e9188[character * 0x4c];
        return Inp.s_InputStateBase_00604a20 + slot * 0x10;
    }

    /// <summary>
    /// Ghidra: le bloc de <c>0x00401b0a</c> — la lecture de manette qui arme les trois modes.
    /// Deux gachettes opposees ensemble basculent la rotation ; une seule lance l'introduction ;
    /// une direction quelconque passe en pilotage manuel.
    /// </summary>
    private static void ReadCameraPad()
    {
        ushort pad = *(ushort*)(LeaderPadState() + 2);

        if ((pad & 0x400) != 0 && (pad & 0x100) != 0)
        {
            *(short*)Mem.ViewParams(0x00571398) = 0;
            *(short*)Mem.ViewParams(0x00571280) = 0;

            if ((*CameraFlags & 0x20) == 0) *CameraFlags |= 0x20;
            else *CameraFlags &= 0xffffffdf;

            *(short*)Mem.ViewParams(0x005713a0) = 8;
            return;
        }

        if ((pad & 0x800) != 0 && (pad & 0x200) != 0)
        {
            if ((*CameraFlags & 0x18) == 0) *CameraFlags |= 8;
            return;
        }

        if ((*(ushort*)LeaderPadState() & 0xf00) != 0) *CameraFlags |= 0x40;
    }

    // Ghidra: DAT_00604ba0 / DAT_00604d30 — le cache des faces d'occlusion. Ce n'est PAS deux
    // tableaux : c'est un seul bloc entrelace de neuf cellules de 200 mots. Les 100 premiers mots
    // d'une cellule sont les faces (base 0x00604ba0), les 100 suivants la table de sommets de
    // chacune (base 0x00604d30 = 0x00604ba0 + 100 * 4). L'indice d'une cellule (`cellule * 200 + n`)
    // et le plafond de n a 99 (FUN_00402de0) se recoupent : 9 * 0x320 = 0x1c20, qui finit PILE a
    // 0x006067c0, le debut de ViewBlock. Releve le 2026-11-27 au desassembleur (0x004019e4 /
    // 0x004019ea, meme registre d'indice).
    private static readonly byte* s_OccluderCache_00604ba0 = (byte*)NativeMemory.AllocZeroed(0x1c20);

    private static uint* OccluderFace(int cell, int n)
        => (uint*)(s_OccluderCache_00604ba0 + (cell * 200 + n) * 4);

    private static uint* OccluderVertices(int cell, int n)
        => (uint*)(s_OccluderCache_00604ba0 + 0x190 + (cell * 200 + n) * 4);

    // Ghidra: DAT_0054f048 / DAT_0054f070 — les neuf decalages (dx, dz) du voisinage 3x3, ranges
    // au pas de quatre octets ; le code n'en lit que le mot bas. Relus dans Blaze.exe.
    private static readonly short[] s_OccluderDx_0054f048 = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
    private static readonly short[] s_OccluderDz_0054f070 = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };

    /// <summary>
    /// Ghidra: le bloc de <c>0x0040186b</c>..<c>0x00401b6d</c> de FUN_004011c0 — <b>l'occlusion</b> :
    /// la camera teste le segment qui la joint au meneur contre les faces du decor voisin.
    ///
    /// <para>
    /// Le travail est etale sur cinq images, par le mot bas de <c>DAT_00571380</c> : a 0, les neuf
    /// cellules autour de la camera sont recollectees (<see cref="CollectOccluders_00402D60"/>) et la
    /// cellule 0 testee ; puis 1-2, 3-4, 5-6, 7-8, et retour a 0. Le nombre de faces qui coupent la
    /// vue s'accumule dans <c>DAT_005712dc</c>, et passe dans <c>DAT_00571348</c> a chaque tour
    /// complet : c'est lui qui arme la secousse de l'appelant (un pas d'angle de cote, dans le sens
    /// que choisit le bloc « pas d'obstacle »).
    /// </para>
    /// </summary>
    private static void ProbeCameraOcclusion_004011C0()
    {
        short* round = (short*)Mem.ViewParams(0x00571380);
        short* hits = (short*)Mem.ViewParams(0x005712dc);
        short* lastHits = (short*)Mem.ViewParams(0x00571348);

        if (*lastHits == 0)
        {
            if (*round == 0)
            {
                // Rien ne coupait la vue au dernier tour : on choisit de quel cote la camera
                // s'ecarterait. Le pas d'angle vertical suit l'inclinaison courante, le pas lateral
                // l'ecart entre le cap de la camera et celui du meneur.
                ushort yaw = (ushort)(ViewAngle[1] & 0xfff);
                // Ghidra: `_DAT_006067e8 &= 0xfffffff` — le dword couvre les deux premiers angles,
                // seul le quartet haut du second tombe.
                ViewAngle[1] = (short)(ViewAngle[1] & 0x0fff);

                byte* leader = Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c;
                short gap = (short)((-*(short*)(leader + 0x32) & 0xfff) - yaw);
                if (gap < 0) gap = (short)(gap + ((0xfffu - (uint)gap) & 0xf000));
                while (gap > 0xfff) gap -= 0x1000;

                *CameraFlags &= 0xfffffffd;
                *(short*)Mem.ViewParams(0x00571278) = (short)(ViewAngle[0] < 0x201 ? 1 : -1);
                *(short*)Mem.ViewParams(0x0057127a) = (short)(gap > 0x7ff ? -1 : 1);

                RefillOccluderCells();
            }
        }
        else if (*round == 0)
        {
            RefillOccluderCells();
        }

        // Le test : la cellule du tour, et la suivante hors du tour 0.
        short first = *round;
        int last = first + (first != 0 ? 1 : 0);
        int* counts = (int*)Mem.ViewParams(0x005712a8);
        byte* leaderSlot = Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c;

        short* v0 = stackalloc short[4];
        short* v1 = stackalloc short[4];
        short* v2 = stackalloc short[4];
        short* v3 = stackalloc short[4];
        short* eye = stackalloc short[3];
        short* target = stackalloc short[3];

        for (int cell = first; cell <= last; cell = (short)(cell + 1))
        {
            for (short n = 0; n < counts[cell]; n++)
            {
                uint* face = (uint*)*OccluderFace(cell, n);
                byte* vertices = (byte*)*OccluderVertices(cell, n);

                // Quatre sommets de huit octets, recopies : FUN_004029e0 retourne leur Y en place.
                *(ulong*)v0 = *(ulong*)(vertices + (face[0] & 0xffff) * 8);
                *(ulong*)v1 = *(ulong*)(vertices + (face[0] >> 0x10) * 8);
                *(ulong*)v2 = *(ulong*)(vertices + (face[1] & 0xffff) * 8);
                *(ulong*)v3 = *(ulong*)(vertices + (face[1] >> 0x10) * 8);

                eye[0] = (short)(*(int*)Mem.ViewParams(0x00571300) >> 0xc);
                eye[1] = (short)(*(int*)Mem.ViewParams(0x00571304) >> 0xc);
                eye[2] = (short)(*(int*)Mem.ViewParams(0x00571308) >> 0xc);
                target[0] = (short)(*(int*)(leaderSlot + 0x1c) >> 0xc);
                target[1] = (short)((short)(*(int*)(leaderSlot + 0x20) >> 0xc) + 0x50);
                target[2] = (short)(*(int*)(leaderSlot + 0x24) >> 0xc);

                int result = SegmentCrossesQuad_004029E0(
                    ((face[6] & 1) != 0 ? 1 : 0) + 3, v0, v1, v2, v3, eye, target);

                if ((short)result == 1 && (face[7] & 0x2000000) == 0)
                {
                    (*hits)++;
                }
            }
        }

        short next = first == 0 ? (short)1 : (short)(first + 2);
        *round = next > 8 ? (short)0 : next;
    }

    /// <summary>Ghidra: <c>0x00401913</c>..<c>0x00401981</c> — le debut d'un tour : le compte du tour
    /// passe dans <c>DAT_00571348</c>, puis les neuf cellules sont recollectees.</summary>
    private static void RefillOccluderCells()
    {
        short* hits = (short*)Mem.ViewParams(0x005712dc);
        *(short*)Mem.ViewParams(0x00571348) = *hits;

        ushort* calm = (ushort*)Mem.ViewParams(0x005713a4);
        if (*hits == 0)
        {
            // Quatre tours de suite sans obstacle remettent a zero le compteur de secousses.
            ushort ticks = (ushort)(*calm + 1);
            *calm = (ushort)(ticks & 0x0fff);
            if ((ticks & 0xfff) > 3)
            {
                *(short*)Mem.ViewParams(0x0057127e) = 0;
            }
        }
        else
        {
            *calm = 0;
        }

        *hits = 0;
        for (int cell = 0; cell < 9; cell++)
        {
            CollectOccluders_00402D60(s_OccluderDx_0054f048[cell], s_OccluderDz_0054f070[cell], (short)cell);
        }
    }

    /// <summary>Ghidra: FUN_00402d60 @ 0x00402d60 — le remplissage d'une cellule de la grille
    /// d'occlusion, sur tous les morceaux de decor de la sous-zone (la meme liste close par 0xff que
    /// la premiere famille du dessin, <c>DrawChunkListA</c>).</summary>
    private static void CollectOccluders_00402D60(short dx, short dz, short cell)
    {
        byte* list = ((byte**)*(byte**)(s_Unknown_005e97cc + 0x80))[s_Unknown_005f2ce0[8]];
        *(short*)Mem.ViewParams(0x005713b4) = 0;
        uint chunkBase = *Mem.Entity(0x00600832);
        ((int*)Mem.ViewParams(0x005712a8))[cell] = 0;

        for (; *list != 0xff; list++)
        {
            CollectChunkCellFaces_00402DE0(
                ((byte**)Mem.Entity(0x005f7bc0))[chunkBase + *list], (sbyte)dx, (sbyte)dz, cell);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00402de0 @ 0x00402de0 — les faces d'un morceau de decor dans une case de sa
    /// grille 32 x 32 (cases de 0x200 unites, origine a -0x2000) : la case de la camera, decalee
    /// de (dx, dz).
    ///
    /// <para>La case commence par deux mots de huit compteurs d'un octet, suivis des numeros de
    /// face (16 bits). Le total des huit compteurs s'ajoute au compte de la cellule, plafonne a 99 ;
    /// chaque face est rangee dans le cache avec, pour table de sommets, <c>morceau + 0x40</c>.</para>
    /// </summary>
    private static void CollectChunkCellFaces_00402DE0(byte* chunk, sbyte dx, sbyte dz, short cell)
    {
        byte* faces = *(byte**)(chunk + 0x28);
        byte* vertices = chunk + 0x40;
        int* camera = *(int**)Mem.Entity(0x005edebc);

        byte x = (byte)((byte)(((camera[0] >> 0xc) + 0x2000) >> 9) + dx);
        byte z = (byte)((byte)(((camera[2] >> 0xc) + 0x2000) >> 9) + dz);
        if (x >= 0x20 || z >= 0x20) return;

        uint* box = ((uint**)*(byte**)(chunk + 0x34))[z * 0x20 + x];
        uint lo = box[0];
        uint hi = box[1];

        int* count = (int*)Mem.ViewParams(0x005712a8) + cell;
        *count += (int)((lo >> 0x10 & 0xff) + (lo >> 0x18) + (lo >> 8 & 0xff) + (hi >> 0x10 & 0xff)
                        + (hi >> 0x18) + (hi >> 8 & 0xff) + (lo & 0xff) + (hi & 0xff));
        if (*count > 99) *count = 99;

        // Ghidra lit chaque numero par un dword au pas de deux octets et n'en garde que le mot bas :
        // on lit le mot, meme valeur, sans deborder du dernier.
        ushort* number = (ushort*)(box + 2);
        short* slot = (short*)Mem.ViewParams(0x005713b4);

        while (hi != 0 || lo != 0)
        {
            for (uint left = lo & 0xff; left != 0; left--)
            {
                int index = *slot;
                (*slot)++;
                *OccluderFace(cell, index) = (uint)(*number++ * 0x20 + faces);
                *OccluderVertices(cell, index) = (uint)vertices;
                if (*slot > 99) *slot = 99;
            }

            // Les huit compteurs defilent comme un seul mot de 64 bits.
            lo = (hi << 0x18) | (lo >> 8);
            hi >>= 8;
        }
    }

    /// <summary>
    /// Ghidra: FUN_004029e0 @ 0x004029e0 — <b>le segment coupe-t-il le quadrilatere ?</b> Le
    /// segment va de <paramref name="eye"/> a <paramref name="target"/>, le quadrilatere est
    /// (a, b, c, d). Le Y des quatre sommets est retourne en place (repere du decor vers celui de
    /// la camera). Rend 1 si le point d'intersection avec le plan tombe dans la face, 2 si le
    /// segment est parallele au plan, 0 sinon. Le premier parametre (3 ou 4) n'est pas lu.
    /// </summary>
    private static int SegmentCrossesQuad_004029E0(
        int unusedSides, short* a, short* b, short* c, short* d, short* eye, short* target)
    {
        a[1] = (short)-a[1];
        b[1] = (short)-b[1];
        c[1] = (short)-c[1];
        d[1] = (short)-d[1];

        int by = b[1];
        int bz = b[2];
        int bx = b[0];

        // La normale (nx, ny, nz) = (local_20, local_1c, local_18), chaque produit reduit de 8 bits.
        int* normal = stackalloc int[3];
        normal[2] = ((a[0] - bx) * (c[1] - by) - (c[0] - bx) * (a[1] - by)) >> 8;
        normal[0] = ((a[1] - by) * (c[2] - bz) - (a[2] - bz) * (c[1] - by)) >> 8;
        normal[1] = ((c[0] - bx) * (a[2] - bz) - (a[0] - bx) * (c[2] - bz)) >> 8;

        int ey = eye[1];
        int ez = eye[2];
        int ex = eye[0];
        int sideEye = (bz - ez) * normal[2] + (bx - ex) * normal[0] + (by - ey) * normal[1];

        int ty = target[1];
        int tz = target[2];
        int tx = target[0];
        int sideTarget = (by - ty) * normal[1] + (bz - tz) * normal[2] + (b[0] - tx) * normal[0];

        // Les deux bouts du meme cote strict du plan : pas d'intersection.
        if ((sideEye >= 1 && sideTarget >= 1) || (sideEye <= -1 && sideTarget <= -1)) return 0;

        int fromEye = (ez - b[2]) * normal[2] + (ex - b[0]) * normal[0] + (ey - b[1]) * normal[1];
        int span = fromEye - ((ty - b[1]) * normal[1] + (tz - b[2]) * normal[2] + (tx - b[0]) * normal[0]);
        if (span == 0) return 2;

        int t = (fromEye * 0x100) / span;
        int* hit = stackalloc int[3];
        hit[2] = ((tz - ez) * t + ez * 0x100) >> 8;
        hit[1] = ((ty - ey) * t + ey * 0x100) >> 8;
        hit[0] = ((tx - ex) * t + ex * 0x100) >> 8;

        int edgeAB = EdgeSide_00402CB0(a, b, normal, hit);
        int edgeBC = EdgeSide_00402CB0(b, c, normal, hit);
        int edgeCD = EdgeSide_00402CB0(c, d, normal, hit);
        int edgeDA = EdgeSide_00402CB0(d, a, normal, hit);

        if (edgeAB >= 0)
        {
            if (edgeBC >= 0 && edgeCD >= 0 && edgeDA >= 0) return 1;
            return 0;
        }

        if (edgeBC < 0 && edgeCD < 0 && edgeDA < 0) return 1;
        return 0;
    }

    /// <summary>Ghidra: FUN_00402cb0 @ 0x00402cb0 — de quel cote de l'arete (p, q) tombe le point,
    /// vu le long de la normale : le produit mixte ((q - p) x n) . (point - p), au signe pres.</summary>
    private static int EdgeSide_00402CB0(short* p, short* q, int* normal, int* point)
    {
        int dz = q[2] - p[2];
        int dx = q[0] - p[0];
        int dy = q[1] - p[1];
        return (normal[0] * dz - dx * normal[2]) * (point[1] - p[1])
             + (dx * normal[1] - normal[0] * dy) * (point[2] - p[2])
             + (normal[2] * dy - normal[1] * dz) * (point[0] - p[0]);
    }
}
