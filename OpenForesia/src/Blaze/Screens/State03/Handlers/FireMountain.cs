// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DE LA MONTAGNE DU DRAGON DE FEU — zone 2, table 0x0056adc8.
//
// Ouvert le 2026-09-17 sur le patron de `Mine.cs`. La zone n'a qu'une sous-zone et n'installe
// qu'un bloc dans cette table, 0x00554d28. Ses creneaux 0 a 7 et 15 sont les decors communs de
// `Forest.cs` (0x004734B0 et ses freres) ; les six qui lui sont propres sont ici :
//
//   creneau  8   0x00456180   la dalle qui ondule en hauteur, et sonne a deux points du cycle
//   creneau  9   0x00456270   le decor qui tourne en rond sur un cercle de 200
//   creneau 10   0x00456310   l'evenement unique : un drapeau de script, une chute, la camera qui tremble
//   creneau 11   0x00456590   le jet intermittent : six naissances, puis une pause tiree au sort
//   creneau 13   0x00456110   le son d'ambiance a distance, toutes les 20 images
//   creneau 14   0x004560A0   le son d'ambiance global, toutes les 20 images
//
// Le son d'une zone se lit par `ZoneSound` (`Mine.cs`) : meme classe partielle.
// Les particules de la zone sont dans `Effects/FireMountainParticles.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES DEUX SONS D'AMBIANCE
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004560a0 @ 0x004560a0 — creneau <b>14</b> du bloc <c>0x00554d28</c>. Decompilee
    /// dans <c>Blaze.c</c>, 31 instructions.
    ///
    /// <para>
    /// <b>Le son d'ambiance qui ne depend pas de la camera.</b> Toutes les vingt images il appelle
    /// directement <c>Audio.PlaySoundEffect</c> — le point d'entree du son, sans position ni
    /// distance —, voix <c>0x11</c> de la zone, hauteur <c>0x400</c>, gain <c>0x800</c> sur les
    /// deux canaux. Le groupe l'entend donc au meme volume ou qu'il soit dans la sous-zone.
    /// </para>
    /// <para>
    /// <b>Le bit <c>0x4000</c> n'est pose que si <c>+0x14</c> vaut 1</b> : c'est le placement qui
    /// en decide. Le frere <see cref="EntityHandler_00456110"/> le pose sans condition. Ce que le bit
    /// commande n'est pas etabli ici.
    /// </para>
    /// </summary>
    private static void EntityHandler_004560A0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            if (*(short*)(e + 0x14) == 1) *(uint*)e |= 0x4000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 0x14) return;

        // Ghidra: `xor eax,eax` puis `mov al,[edx+11h]` — l'octet est etendu sans signe.
        Aud.PlaySoundEffect(ZoneSound(0x11), 0x400, 0x800, 0x800);   // Ghidra: FUN_0053cea0
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00456110 @ 0x00456110 — creneau <b>13</b> du bloc <c>0x00554d28</c>. Decompilee
    /// dans <c>Blaze.c</c>, 30 instructions.
    ///
    /// <para>
    /// La meme horloge de vingt images que <see cref="EntityHandler_004560A0"/>, mais le son passe
    /// par <see cref="TryPlaySoundNearEntity"/> : il part de la position de l'entite et <b>se tait
    /// au-dela de 2000 unites</b> de la camera. Voix <c>0x14</c>, hauteur <c>0x400</c>, gain
    /// <c>0x2000</c> (double par l'appele). Le bit <c>0x4000</c> est pose sans condition.
    /// </para>
    /// <para>
    /// <b>Ce n'est pas <see cref="TickRepeatingDistanceSound"/></b>, le corps partage des decors
    /// sonores de la mine : celui-la appelle <see cref="TryPlayDistanceSound"/>, dont la portee est
    /// un argument ; celui-ci appelle la sœur a portee fixe. Le diff LOOSE du 2026-09-17 ne lui
    /// trouve aucun jumeau.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456110(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(uint*)e |= 0x4000;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 0x14) return;

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x14), 0x400, 0x2000, 0, 0);   // Ghidra: FUN_00468080
        *(short*)(e + 0x12) = 0;
    }

    // =====================================================================
    // LES DEUX DECORS QUI BOUGENT SUR UNE SINUSOIDE
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00456180 @ 0x00456180 — creneau <b>8</b> du bloc <c>0x00554d28</c>. Decompilee
    /// dans <c>Blaze.c</c>, 66 instructions.
    ///
    /// <para>
    /// <b>Un decor qui ondule en hauteur</b> autour de l'altitude de sa naissance : <c>+0x20</c> vaut
    /// a chaque image <c>base × 0x1000 + sin(phase) × 200</c>, ou la base est la hauteur de depart
    /// en unites entieres (<c>+0x14</c>) et la phase (<c>+0x12</c>) avance de <c>0x1e</c> par image.
    /// Un tour complet de <c>0x1000</c> dure donc 137 images environ. La phase de depart est tiree
    /// au sort (<c>rand() % 0xffa</c>) : deux decors voisins ne battent pas ensemble.
    /// </para>
    /// <para>
    /// <b>Le produit n'est pas recadre</b> : l'entree de table de sinus (±<c>0x1000</c>) multiplie
    /// 200 tel quel, ce qui fait une amplitude de 200 unites entieres. Meme convention que les
    /// spirales de <c>MineParticles.cs</c>.
    /// </para>
    /// <para>
    /// <b>Il sonne deux fois par cycle</b>, voix <c>0x15</c> par <see cref="TryPlaySoundNearEntity"/> :
    /// quand la phase, apres l'avance, tombe strictement entre <c>0x800</c> et <c>0x81e</c>, et
    /// strictement entre <c>0</c> et <c>0x1e</c> — les deux passages par l'altitude de base, l'un en
    /// descendant, l'autre en montant. La fenetre fait
    /// exactement un pas de large, donc chaque passage sonne une et une seule fois, sauf quand la
    /// phase tombe pile sur une borne. Ghidra rend le premier identifiant en <c>CONCAT22</c> et le
    /// second en octet nu : le desassemblage montre deux <c>movzx</c> identiques.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456180(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x12) = (short)(CrtRand() % 0xffa);        // Ghidra: FUN_00547140
        }

        if (*(short*)(e + 0x10) != 1) return;

        // Ghidra: `call 00545A1Ch` — le saut par la table d'imports vers Ordinal_451, le sinus.
        // L'argument est lu AVANT l'avance de la phase.
        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += 0x1e;
        *(int*)(e + 0x20) = *(short*)(e + 0x14) * 0x1000 + sin * 200;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;

        short phase = *(short*)(e + 0x12);
        if (phase > 0x800 && phase < 0x81e)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x15), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        phase = *(short*)(e + 0x12);
        if (phase > 0 && phase < 0x1e)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x15), 0x400, 0x1000, 0, 0);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00456270 @ 0x00456270 — creneau <b>9</b> du bloc <c>0x00554d28</c>. Decompilee
    /// dans <c>Blaze.c</c>, 44 instructions.
    ///
    /// <para>
    /// Le cousin horizontal de <see cref="EntityHandler_00456180"/> : <b>un decor qui tourne sur un
    /// cercle de 200 unites</b> autour du point de sa naissance, <c>X = base_x × 0x1000 +
    /// sin(phase) × 200</c> et <c>Z = base_z × 0x1000 + cos(phase) × 200</c>. La phase avance de
    /// <c>0x14</c> par image — un tour en 205 images — et part d'une valeur tiree au sort. Il ne
    /// sonne pas.
    /// </para>
    /// <para>
    /// Les deux appels de trigonometrie lisent la phase <b>avant</b> son avance : ils portent sur
    /// le meme angle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456270(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0xffa);        // Ghidra: FUN_00547140
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x1c) >> 0xc);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x24) >> 0xc);
        }

        if (*(short*)(e + 0x10) != 1) return;

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: 0x00545a1c, Ordinal_451
        *(int*)(e + 0x1c) = *(short*)(e + 0x14) * 0x1000 + sin * 200;

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));        // Ghidra: 0x00545a16, Ordinal_450
        *(short*)(e + 0x12) += 0x14;
        *(int*)(e + 0x24) = *(short*)(e + 0x16) * 0x1000 + cos * 200;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
    }

    // =====================================================================
    // L'EVENEMENT UNIQUE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00456310</c> — creneau <b>10</b> du bloc <c>0x00554d28</c>. <b>Absente de
    /// <c>Blaze.c</c></b> ; 200 instructions, <c>Legacy/Raw/Blaze/FUN_00456310.asm</c>.
    ///
    /// <para>
    /// <b>Un evenement qui n'a lieu qu'une fois par partie</b>, garde par un drapeau de la banque
    /// d'etat 0 (<c>0x005e96e0</c>) dont le numero est range en <c>+0x14</c> par le placement.
    /// </para>
    /// <list type="table">
    ///   <item><term>endormi</term><description>tant que le mot d'etat porte <c>0x40000000</c>, le
    ///     corps ne fait que lire le drapeau. S'il est leve, il retire le bit <c>0x80000000</c> — le
    ///     bit d'emplacement occupe, celui que la boucle des creneaux d'equipe teste — et
    ///     l'entite disparait ; sinon il attend.</description></item>
    ///   <item><term>naissance</term><description>il <b>leve lui-meme le drapeau</b> (sans le garde
    ///     a 32 octets de <see cref="ScriptFlagRaise"/>), puis remet <c>+0x14</c> a zero. La
    ///     prochaine visite trouvera donc l'evenement deja consomme.</description></item>
    ///   <item><term>etat 1</term><description>l'attente : a chaque image, une chance sur trente de
    ///     partir, et au plus tard a la trentieme. Pendant l'attente <b>la camera tremble</b> —
    ///     <see cref="TryPublishCameraEffectNearEntity"/> publie un decalage de −2 a +2 sur le second
    ///     mot, a moins de 1500 unites. Au depart : voix <c>0x13</c> de la zone, et la vitesse
    ///     de chute <c>+0x12</c> tiree entre 0 et 4.</description></item>
    ///   <item><term>etat 2</term><description>la chute : <c>+0x20</c> perd <c>vitesse × 0x200</c>
    ///     par image, la vitesse gagne 2 a 4. Le tremblement continue en s'amenuisant tant que la
    ///     vitesse est sous <c>0x3a</c>. En meme temps l'entite derive dans son cap <c>+0x32</c>,
    ///     d'un pas de sinus/cosinus plus un bruit de 0 a <c>0x2000</c>, le tout double, et son cap
    ///     comme son <c>+0x34</c> vacillent de ±10. A une vitesse de 300, le tremblement est
    ///     remis a zero et l'etat passe a 3.</description></item>
    ///   <item><term>etat 3</term><description>l'entite tourne de <c>0x40</c> par image et
    ///     retrecit de <c>0x40</c> sur ses deux echelles. A zero, elle se rendort :
    ///     <c>0x50006000</c>.</description></item>
    /// </list>
    /// <para>
    /// <b>Un detail du binaire, transcrit tel quel.</b> Rendormie par l'etat 3, l'entite repasse par
    /// la branche endormie — mais <c>+0x14</c> ne contient plus le numero du drapeau : la naissance
    /// l'a mis a zero, puis le depart y a range <c>X &gt;&gt; 12</c>. La banque est donc lue a un
    /// indice qui vient de la position, et non du placement. Selon ce qu'il trouve, le corps retire
    /// ou non le bit d'occupation. L'effet visible, s'il y en a un, n'est pas etabli.
    /// </para>
    /// <para>
    /// Les deux decalages de chute (<c>0x00545a1c</c>, <c>0x00545a16</c>) sont les sauts par la
    /// table d'imports vers <c>Ordinal_451</c> et <c>Ordinal_450</c>, recoupes dans
    /// <c>MineParticles.cs</c> : sinus pour <c>X</c>, cosinus pour <c>Z</c>. Ils sont en stdcall —
    /// aucun <c>add esp</c> ne les suit.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456310(byte* e)
    {
        uint state = *(uint*)e;

        if ((state & 0x40000000) != 0)
        {
            if (!ScriptFlagSet(*(short*)(e + 0x14))) return;

            *(uint*)e = state & 0x7fffffff;
            return;
        }

        // Ghidra: LAB_0045635d
        if (*(short*)(e + 0x10) == 0)
        {
            short index = *(short*)(e + 0x14);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;

            // Ghidra: `shl dl,cl` puis `or` — le drapeau est leve sans borne sur la cellule.
            int cell = (short)DivideTowardZeroBy8(index);
            int bit = (index - cell * 8) & 0x1f;
            Mem.s_StateBank0_005e96e0[cell] |= (byte)(1 << bit);

            *(short*)(e + 0x14) = 0;
        }

        // Ghidra: LAB_004563a0 — l'attente.
        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (CrtRand() % 30 == 1 || *(short*)(e + 0x12) == 30)
            {
                // Ghidra: LAB_004563cc — le depart.
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = (short)(CrtRand() % 5);
                *(short*)(e + 0x16) = (short)(*(int*)(e + 0x24) >> 0xc);
                *(short*)(e + 0x14) = (short)(*(int*)(e + 0x1c) >> 0xc);

                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x13), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            }

            // Ghidra: LAB_0045641e — le tremblement, a chaque image de l'etat 1, depart compris.
            TryPublishCameraEffectNearEntity((uint*)e, 999, CrtRand() % 5 - 2, 999, 0, 0);   // Ghidra: FUN_0041be60
        }

        // Ghidra: LAB_00456443 — la chute.
        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(e + 0x12) < 0x3a)
            {
                int remaining = 0x3c - *(short*)(e + 0x12);
                int jolt = CrtRand() % ((remaining >> 2) + 1) - (remaining >> 3);
                TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);
            }

            // Ghidra: LAB_0045648c
            *(int*)(e + 0x20) -= *(short*)(e + 0x12) << 9;
            *(short*)(e + 0x12) += (short)(CrtRand() % 3 + 2);

            int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));   // Ghidra: 0x00545a1c
            *(int*)(e + 0x1c) += (sin + ((CrtRand() % 5) << 0xb)) * 2;

            int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32));        // Ghidra: 0x00545a16
            *(int*)(e + 0x24) += (cos + ((CrtRand() % 5) << 0xb)) * 2;

            *(short*)(e + 0x32) += (short)(CrtRand() % 20 - 10);
            *(short*)(e + 0x34) += (short)(CrtRand() % 20 - 10);

            if (*(short*)(e + 0x12) >= 300)
            {
                *(short*)(e + 0x10) = 3;
                TryPublishCameraEffectNearEntity((uint*)e, 999, 0, 999, 0, 0);
            }
        }

        // Ghidra: LAB_0045654a — l'effacement.
        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x32) += 0x40;
            *(short*)(e + 0x28) -= 0x40;
            short scale = *(short*)(e + 0x28);
            *(short*)(e + 0x2c) -= 0x40;

            if (scale <= 0)
            {
                *(short*)(e + 0x28) = 0;
                *(uint*)e |= 0x50006000;
                *(short*)(e + 0x2c) = 0;
            }
        }
    }

    // =====================================================================
    // LE JET INTERMITTENT
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00456590</c> — creneau <b>11</b> du bloc <c>0x00554d28</c>. <b>Absente de
    /// <c>Blaze.c</c></b> ; 99 instructions, <c>Legacy/Raw/Blaze/FUN_00456590.asm</c>.
    ///
    /// <para>
    /// <b>Une source qui crache par salves.</b> Quatre etats dans <c>+0x10</c>, et un cinquieme que
    /// ce corps ne pose jamais :
    /// </para>
    /// <list type="table">
    ///   <item><term>naissance</term><description>etat <b>3</b>, <c>+0x14</c> recoit la hauteur en
    ///     unites entieres, <c>+0x16 = 0x80</c>, <c>+0x44 = 0</c> (double mot). Ni l'un ni l'autre
    ///     n'est relu par ce corps.</description></item>
    ///   <item><term>etat 3</term><description>quarante et une images d'attente, puis etat
    ///     1.</description></item>
    ///   <item><term>etat 1</term><description>a sa premiere image, <b>six naissances</b> par
    ///     <see cref="SpawnEffectNearCamera_00468110"/> (a moins de 1800 unites de la camera), du
    ///     sous-type <c>0x31</c> si <c>+0x32</c> est nul, <c>0x32</c> sinon — donc <c>0xf1</c> ou
    ///     <c>0xf2</c>, resolus par la table de sous-zone. Le compteur avance ensuite de
    ///     <c>0x200</c> par image et passe la main a l'etat 2 au-dela de <c>0x1f40</c> : seize
    ///     images.</description></item>
    ///   <item><term>etat 2</term><description>la pause : au plus quarante-cinq images, et une
    ///     chance sur quarante-cinq par image d'y couper court. Puis retour a l'etat 1, et une
    ///     nouvelle salve.</description></item>
    ///   <item><term>etat <c>0x63</c></term><description>un regime continu : une chance sur deux par
    ///     image de faire naitre un <c>0x30</c>, une sur trente de sonner la voix <c>0x10</c>.
    ///     <b>Personne dans ce corps ne pose <c>0x63</c></b> — c'est un placement ou un autre
    ///     gestionnaire qui l'ecrit, et lequel n'est pas etabli.</description></item>
    /// </list>
    /// <para>
    /// Le tirage de l'etat <c>0x63</c> est range en <c>[esp+10h]</c> — l'emplacement de sauvegarde
    /// d'<c>EBX</c> reutilise comme variable — et jamais relu : seul son drapeau de zero sert.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456590(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 3;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x16) = 0x80;
            *(int*)(e + 0x44) = 0;
        }

        // Ghidra: LAB_004565bf
        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x28)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;
            }
        }

        // Ghidra: LAB_004565da — la salve.
        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x12) == 0)
            {
                // Ghidra: la boucle de LAB_004565ec, compteur EBX de 6 a 1 ; +0x32 est reteste a
                // chaque tour.
                for (int i = 6; i != 0; i--)
                {
                    short subtype = *(short*)(e + 0x32) == 0 ? (short)0x31 : (short)0x32;
                    SpawnEffectNearCamera_00468110(e, subtype, 0, -1);
                }
            }

            // Ghidra: LAB_00456609
            *(short*)(e + 0x12) += 0x200;
            if (*(short*)(e + 0x12) > 0x1f40) *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_0045661d — la pause.
        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x12))++;
            int roll = CrtRand() % 45;
            if (*(short*)(e + 0x12) == 45 || roll == 1)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;
            }
        }

        // Ghidra: LAB_0045664a
        if (*(short*)(e + 0x10) == 0x63)
        {
            if (CrtRand() % 2 == 0)
            {
                SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);
            }

            // Ghidra: LAB_00456678
            if (CrtRand() % 30 == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x10), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            }
        }
    }
}
