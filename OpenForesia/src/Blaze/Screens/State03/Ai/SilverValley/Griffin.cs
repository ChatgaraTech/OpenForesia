// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GRIFON — 0x004E1380, vallee d'argent (zone 8, sous-zones 0 et 1). Le nom vient de
// Comportement.md (« Grifon ») ; le fichier porte le nom anglais, comme ses voisins de `Ai/`.
//
// Absent de Blaze.c ; transcrit au desassembleur le 2026-09-19
// (Legacy/Raw/Blaze/SilverValley/FUN_004E1380.asm, 3 440 octets), table de sauts a treize entrees
// a 0x004e20b8. Son chercheur de proie, 0x004E0750, est FUN_004a1020 instruction pour instruction.
//
// UNE VOLANTE SUR LA CHARPENTE DU GREMLIN (Ai/OldTower/Gremlin.cs) : memes feuilles
// (CancelGestureOnTrait_004D5DE0, ProbeClearSide_004D5E60, ResetSceneEntityOnStun_004C3810), meme
// ancre de hauteur +0x48 que la proie tire vers elle, meme ondulation par le sinus de +0x14. Mais
// sa queue est plus simple — un seul masque de contact, 0x8882 — et sa volee de gestes differente :
//
//   - L'ACCROCHE (posture 0x0b) : il rejoint la hauteur de sa proie et la frappe UNE IMAGE SUR DEUX,
//     au quart de sa force, verrou de 10 ;
//   - LE PIQUE (posture 0x0c) : il monte de 200 unites au-dessus du sol, fond sur sa proie trente-trois
//     images, et la frappe a moitie de sa force en la PROJETANT (effet 0x39, 0x18000 vers le haut) ;
//   - pas de sort.
//
// Sa sœur Fougre (0x004E20F0, `Fougre.cs`) partage la queue ; ses aides sont ici.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x004e1380</c> — <b>l'intelligence du Grifon</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004E1380(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x2c] — la sonde, batie sur la pile a chaque image.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x3c; probe[2] = 0x50; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);             // Ghidra: FUN_004e0750, copie de FUN_004a1020
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
        }

        *(uint*)e &= 0xfffffffe;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);   // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);
        distance = ValleyFlyerFlatDistance(e, chased);

        if (speed != 0)
        {
            DecideGriffinAction_004E14DA(e, probe, model, chased, boldness, distance, heading, speed);
            BobValleyFlyer(e, chased, 0x0c, 0x0b, -1);
        }

        if (!SettleValleyFlyer(e, speed, &keep, boldness)) return;

        ReplanGriffinAction_004E1DA9(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004e14da..0x004e1b61</c> — le premier temps du Grifon : l'animation, le prelude
    /// de geste (l'accroche et le pique frappent ici), puis la case de la posture. Treize cases,
    /// huit servies : 0 attendre, 1 se tourner, 2 approcher, 3 filer vers un cap, 7 marcher droit,
    /// 9 virer vers un cap tire, 0x0b l'accroche, 0x0c le pique. Le prelude ne rend pas la main.
    /// </summary>
    private static void DecideGriffinAction_004E14DA(
        byte* e, short* probe, byte* model, byte* chased, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopiee en ligne

        // ================================================================ le prelude de geste
        if ((*(uint*)e & 2) != 0)
        {
            // L'accroche : une image sur deux, au quart de la force.
            if (e[0x42] == 0x0b && (e[0x1a] & 1) != 0)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(ushort*)(prey + 0x96) = 0x0a;
                    *(uint*)prey |= 0x40;

                    short strength = *(short*)(model + 0x30);
                    *(short*)(model + 0x30) = (short)(strength >> 2);
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);     // Ghidra: FUN_00539b80
                    *(short*)(model + 0x30) = strength;

                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }
            }

            if (e[0x42] == 0x0c) ValleyFlyerDiveStrike(e, probe, model);
        }

        switch (e[0x42])
        {
            case 0x00: HoldValleyFlyerStill(e, distance, animation: 4); break;
            case 0x01: TurnValleyFlyer(e, boldness, heading, animation: 4); break;
            case 0x02: ApproachValleyFlyer(e, boldness, distance, heading, animation: 1); break;
            case 0x03: RunValleyFlyerCourse(e, boldness, heading, speed, animation: 1); break;
            case 0x07: WalkValleyFlyer(e, boldness, distance, animation: 1); break;

            // ---------------------------------------------------------- 9 : virer vers un cap tire
            case 0x09:
                if (*(short*)(e + 0x12) == 0)
                {
                    int draw = CrtRand();
                    *(short*)(e + 0x32) &= 0x0fff;
                    *(short*)(e + 0x12) = 1;
                    *(short*)(e + 0x18) = 0;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x10) = (short)SignedMask(draw, 0xfff);
                    *(uint*)e |= 0x1000000;
                }

                SteerValleyFlyerToCourse(e);
                break;

            case 0x0b: ClingValleyFlyer(e, chased, animation: 6); break;
            case 0x0c: DiveValleyFlyer(e, boldness, heading, chased, climb: 0xc8, diveFrames: 0x21,
                                        diveFlags: 0x1000002, keepFlags: 0xffffffff, diveAnimation: 5,
                                        clampFrame: -1, slowFloor: 0xa000, restAnimation: 1); break;
            default: break;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004e1da9..0x004e2073</c> — <b>le troisieme temps</b> du Grifon. Trois plages
    /// de distance, a plat : sous 150 le corps a corps, de 150 a 599 l'approche, au-dela la
    /// poursuite.
    /// </summary>
    private static void ReplanGriffinAction_004E1DA9(byte* e, int boldness, int timid)
    {
        ValleyFlyerAim(e, out int distance, out int heading);

        if (distance < 0x96)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 100)
                {
                    if (roll < 0x1e)
                    {
                        // L'accroche.
                        uint flags = *(uint*)e;
                        e[0x42] = 0x0b;
                        e[0x43] = 1;
                        *(int*)(e + 0x5c) = 0;
                        *(uint*)e = (flags & 0xfeffffff) | 2;
                        *(short*)(e + 0x18) = 1;
                        *(short*)(e + 0x1a) = 0;
                    }
                    else
                    {
                        e[0x42] = 0;
                        e[0x43] = (byte)(CrtRand() % 0x28);
                    }
                }
                else
                {
                    StartGriffinCourse(e, timid);
                }
            }
            else if (roll < 0x46 || 100 - roll >= (boldness >> 1) + 10)
            {
                StartGriffinCourse(e, timid);
            }
            else
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
            }
        }

        if (distance < 0x258 && distance >= 0x96)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) >= 0x800) { e[0x42] = 1; e[0x43] = 1; }
                else { e[0x42] = 0x0c; e[0x43] = 1; }
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 2;
                int draw = CrtRand();
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(draw % 10 + (timid >> 1) + 0x14);
            }
            else if (roll < 0x50)
            {
                StartGriffinCourse(e, timid);
            }
            else
            {
                e[0x42] = 0;
                e[0x43] = (byte)(CrtRand() % 0x28);
            }
        }

        if (distance < 0x258) return;

        int far = CrtRand() % 100;
        if (far < 0x3c)
        {
            e[0x42] = 2;
            int draw = CrtRand();
            *(short*)(e + 0x10) = 100;
            e[0x43] = (byte)(draw % 10 + (timid >> 1) + 0x14);
            return;
        }

        if (far < 0x4b)
        {
            StartGriffinCourse(e, timid);
            return;
        }

        e[0x42] = 1;
        e[0x43] = 1;
    }

    /// <summary>La posture 3 du Grifon : un cap tire sur un tour entier, 20 a 29 images plus
    /// la moitie de la timidite.</summary>
    private static void StartGriffinCourse(byte* e, int timid)
    {
        e[0x42] = 3;
        *(short*)(e + 0x12) = 0;
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
        *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
    }

    // =============================================================================================
    // CE QUE LE GRIFON ET FOUGRE PARTAGENT — recopie en ligne dans les deux corps. Les ecarts
    // (animations, seuils du pique) sont passes en parametre.
    // =============================================================================================

    /// <summary>Ghidra: la distance a plat vers la proie, recalculee apres la situation.</summary>
    private static int ValleyFlyerFlatDistance(byte* e, byte* chased)
    {
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        return (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));   // Ghidra: FUN_00545a10
    }

    /// <summary>Ghidra: la tete de la replanification — distance et cap relatif vers
    /// <c>+0x6c</c>, et <c>+0x12</c> remis a zero entre les deux.</summary>
    private static void ValleyFlyerAim(byte* e, out int distance, out int heading)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));   // Ghidra: FUN_00545a10
        int bearing = Winsys3d.Ordinal_457(dx, dz);                        // Ghidra: FUN_00545a22
        *(short*)(e + 0x12) = 0;
        heading = (bearing - *(ushort*)(e + 0x32)) & 0xfff;
    }

    /// <summary>
    /// Ghidra: le pique, dans le prelude — la proie est frappee a MOITIE de la force du modele,
    /// verrou de 30 images, et si elle n'est pas hors d'atteinte, <b>projetee</b> : l'effet
    /// <c>0x39</c> nait (a la place de la bete), la proie prend le bit 1 et une vitesse dans le cap
    /// de la bete, <c>+0x5c &gt;&gt; 11</c> fois le sinus/cosinus, <c>0x18000</c> vers le haut. Le
    /// geste se ferme ensuite — sauf sans camera, ou rien ne se passe.
    /// </summary>
    private static void ValleyFlyerDiveStrike(byte* e, short* probe, byte* model)
    {
        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(prey + 0x96) = 0x1e;
        *(uint*)prey |= 0x40;

        short strength = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(strength >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);             // Ghidra: FUN_00539b80
        *(short*)(model + 0x30) = strength;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

        if ((*(uint*)prey & 0x40000000) == 0)
        {
            SpawnEffectEntity(e, 0x39, 0, -1);                       // Ghidra: FUN_0047a9d0
            *(uint*)prey |= 1;
            *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * (*(int*)(e + 0x5c) >> 0xb);
            *(int*)(prey + 0x54) = 0x18000;
            *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * (*(int*)(e + 0x5c) >> 0xb);
        }

        *(uint*)e &= 0xfffffffd;
    }

    /// <summary>Ghidra: la posture 0 — attendre, l'animation <paramref name="animation"/> en
    /// boucle. Proche de sa distance voulue <c>+0x10</c>, l'hesitation tombe ; sinon elle
    /// decompte, et la poussee est coupee.</summary>
    private static void HoldValleyFlyerStill(byte* e, int distance, short animation)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x18) = animation;
            *(uint*)e |= 0x1000000;
            *(short*)(e + 0x1a) = 0;
            *(short*)(e + 0x12) = 1;
            *(int*)(e + 0x5c) = 0;
        }

        if (distance < *(short*)(e + 0x10))
        {
            e[0x43] = 0;
            return;
        }

        if (e[0x43] != 0) e[0x43]--;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: la posture 1 — se tourner, poussee <c>0x7000</c>, a <c>hardiesse +
    /// 0x80</c> par image ; aligne a ±<c>0xc8</c>, l'hesitation tombe.</summary>
    private static void TurnValleyFlyer(byte* e, int boldness, int heading, short animation)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x18) = animation;
            *(uint*)e |= 0x1000000;
            *(short*)(e + 0x1a) = 0;
            *(short*)(e + 0x12) = 1;
            *(int*)(e + 0x5c) = 0x7000;
        }

        if (((heading + 0xc8) & 0xfff) <= 0x190)
        {
            e[0x43] = 0;
            return;
        }

        if ((heading & 0xfff) < 0x800) *(short*)(e + 0x32) += (short)(boldness + 0x80);
        else *(short*)(e + 0x32) += (short)(-0x80 - boldness);
    }

    /// <summary>Ghidra: la posture 2 — approcher, poussee <c>hardiesse * 0x90 + 0xa000</c>. A sa
    /// distance voulue, l'hesitation tombe et la poussee est coupee ; sinon elle vire a
    /// <c>2 * hardiesse + 0x28</c>.</summary>
    private static void ApproachValleyFlyer(byte* e, int boldness, int distance, int heading, short animation)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x18) = animation;
            *(uint*)e |= 0x1000000;
            *(short*)(e + 0x1a) = 0;
            *(short*)(e + 0x12) = 1;
            *(int*)(e + 0x5c) = boldness * 0x90 + 0xa000;
        }

        if (*(short*)(e + 0x12) != 1) return;

        if (distance < *(short*)(e + 0x10))
        {
            e[0x43] = 0;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (e[0x43] != 0) e[0x43]--;

        if (((heading + 0x100) & 0xfff) <= 0x200) return;
        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x28 : -0x28 - boldness * 2);
    }

    /// <summary>
    /// Ghidra: la posture 3 — filer vers le cap tire <c>+0x10</c>. <b>Elle deplace directement
    /// l'entite</b> au lieu de lui donner une poussee : <c>vitesse * (hardiesse * 0x90 + 0x5000)</c>
    /// au douzieme bit, dans le cap <c>+0x10</c>, et la poussee repart a zero. Le cap propre vire
    /// vers la proie a <c>hardiesse + 0x32</c>. L'animation n'est posee que la premiere image.
    /// </summary>
    private static void RunValleyFlyerCourse(byte* e, int boldness, int heading, int speed, short animation)
    {
        if (*(short*)(e + 0x12) == 1)
        {
            if (e[0x43] != 0) e[0x43]--;

            int push = boldness * 0x90 + 0x5000;
            *(int*)(e + 0x5c) = push;
            int step = speed * push >> 0xc;

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step >> 0xc;   // Ghidra: FUN_00545a1c
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step >> 0xc;        // Ghidra: FUN_00545a16
            *(int*)(e + 0x5c) = 0;

            if (((heading + 0x100) & 0xfff) > 0x200)
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
        }

        if (*(short*)(e + 0x12) != 0) return;

        *(short*)(e + 0x18) = animation;
        *(uint*)e |= 0x1000000;
        *(short*)(e + 0x1a) = 0;
        *(short*)(e + 0x12) = 1;
    }

    /// <summary>Ghidra: la posture 7 — marcher droit (le contournement apres un heurt), poussee
    /// <c>hardiesse * 0x90 + 0x6000</c> posee a la premiere image seulement.</summary>
    private static void WalkValleyFlyer(byte* e, int boldness, int distance, short animation)
    {
        short phase = *(short*)(e + 0x12);

        if (phase == 1)
        {
            if (distance < *(short*)(e + 0x10)) e[0x43] = 0;
            else if (e[0x43] != 0) e[0x43]--;
        }

        if (phase != 0) return;

        *(short*)(e + 0x18) = animation;
        *(short*)(e + 0x12) = 1;
        *(uint*)e |= 0x1000000;
        *(short*)(e + 0x1a) = 0;
        *(int*)(e + 0x5c) = boldness * 0x90 + 0x6000;
    }

    /// <summary>Ghidra: la posture 9, apres son amorce — le cap rattrape <c>+0x10</c> d'un
    /// huitieme de l'ecart par image ; a moins de 20, la marche droite (posture 7) pour 30 a 49
    /// images.</summary>
    private static void SteerValleyFlyerToCourse(byte* e)
    {
        if (*(short*)(e + 0x12) != 1) return;

        short yaw = *(short*)(e + 0x32);
        int course = *(short*)(e + 0x10);
        yaw = (short)(yaw + ((course - yaw) >> 3));
        *(short*)(e + 0x32) = yaw;

        if (course - yaw >= 0x14) return;

        int draw = CrtRand();
        e[0x42] = 7;
        e[0x43] = (byte)(draw % 0x14 + 0x1e);
    }

    /// <summary>Ghidra: la posture 0x0b apres son amorce (animation, 10 a 19 images) — la bete
    /// rejoint d'un quart par image la hauteur de sa proie, 80 unites au-dessus.</summary>
    private static void ClingValleyFlyer(byte* e, byte* chased, short animation)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x18) = animation;
            *(uint*)e |= 0x1000000;
            *(short*)(e + 0x1a) = 0;
            *(short*)(e + 0x12) = 1;
            e[0x43] = (byte)(CrtRand() % 10 + 10);
        }

        if (*(short*)(e + 0x12) != 1) return;

        int y = *(int*)(e + 0x20);
        *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y + 0x14000) >> 2) + y;
        e[0x43]--;
    }

    /// <summary>
    /// Ghidra: la posture 0x0c, <b>le pique</b>, en quatre phases dans <c>+0x12</c> :
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///   <item><term>1</term><description>monter de 6 unites par image en virant vers la proie ;
    ///     a plus de <paramref name="climb"/> au-dessus du sol, la phase 2 : animation
    ///     <paramref name="diveAnimation"/>, geste arme.</description></item>
    ///   <item><term>2</term><description>fondre : poussee <c>0x19000</c>, la hauteur rejoint
    ///     d'un quart celle de la proie, <c>+0x10</c> compte les images ; a
    ///     <paramref name="diveFrames"/>, le geste tombe et la phase 3.</description></item>
    ///   <item><term>3</term><description>la poussee perd un huitieme par image ; sous
    ///     <paramref name="slowFloor"/>, l'hesitation tombe.</description></item>
    ///   <item><term>0</term><description>la remise : geste leve, animation
    ///     <paramref name="restAnimation"/>, poussee a zero, phase 1.</description></item>
    /// </list>
    /// </remarks>
    private static void DiveValleyFlyer(
        byte* e, int boldness, int heading, byte* chased, int climb, short diveFrames,
        uint diveFlags, uint keepFlags, short diveAnimation, short clampFrame, int slowFloor, short restAnimation)
    {
        if (*(short*)(e + 0x12) == 1)
        {
            int y = *(int*)(e + 0x20) + 0x6000;
            *(int*)(e + 0x20) = y;

            if ((y - *(int*)(e + 0x60)) >> 0xc > climb)
            {
                uint flags = *(uint*)e;
                *(short*)(e + 0x12) = 2;
                *(short*)(e + 0x10) = 0;
                *(short*)(e + 0x18) = diveAnimation;
                *(short*)(e + 0x1a) = 0;
                *(uint*)e = (flags & keepFlags) | diveFlags;
            }

            TurnValleyFlyerTowardPrey(e, boldness, heading);
        }

        if (*(short*)(e + 0x12) == 2)
        {
            if (clampFrame >= 0 && *(short*)(e + 0x1a) > clampFrame) *(short*)(e + 0x1a) = clampFrame;

            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x5c) = 0x19000;
            *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y + 0x14000) >> 2) + y;
            (*(short*)(e + 0x10))++;
            short count = *(short*)(e + 0x10);

            TurnValleyFlyerTowardPrey(e, boldness, heading);

            if (count >= diveFrames)
            {
                *(short*)(e + 0x12) = 3;
                *(uint*)e &= 0xfffffffd;
            }
        }

        short phase = *(short*)(e + 0x12);

        if (phase == 3)
        {
            int push = *(int*)(e + 0x5c);
            push -= push >> 3;
            *(int*)(e + 0x5c) = push;
            if ((int)(push & 0xfffff000) < slowFloor) e[0x43] = 0;
        }

        if (phase != 0) return;

        uint rest = *(uint*)e;
        *(short*)(e + 0x12) = 1;
        *(short*)(e + 0x18) = restAnimation;
        *(short*)(e + 0x1a) = 0;
        *(int*)(e + 0x5c) = 0;
        *(uint*)e = (rest & 0xfffffffd) | 0x1000000;
    }

    /// <summary>Ghidra: le virage du pique, recopie deux fois — hors de ±<c>0x100</c>, a
    /// <c>2 * hardiesse + 0x28</c>.</summary>
    private static void TurnValleyFlyerTowardPrey(byte* e, int boldness, int heading)
    {
        if (((heading + 0x100) & 0xfff) <= 0x200) return;
        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x28 : -0x28 - boldness * 2);
    }

    /// <summary>
    /// Ghidra: la fin du premier temps — hors des postures d'attaque (<paramref name="skipA"/>,
    /// <paramref name="skipB"/>, <paramref name="skipC"/>), la bete ondule : sa phase
    /// <c>+0x14</c> avance de 200 a 299, et sa hauteur tend d'un huitieme vers l'ancre
    /// <c>+0x48</c> plus 60 fois le sinus, 140 unites au-dessus. Puis, si la proie a les pieds sur
    /// son plancher (<c>+0x20</c> et <c>+0x64</c> egaux au douzieme bit), l'ancre devient ce
    /// plancher.
    /// </summary>
    private static void BobValleyFlyer(byte* e, byte* chased, int skipA, int skipB, int skipC)
    {
        byte pose = e[0x42];
        if (pose != skipA && pose != skipB && pose != skipC)
        {
            *(short*)(e + 0x14) += (short)(CrtRand() % 100 + 200);
            if (*(short*)(e + 0x14) >= 0x1000) *(short*)(e + 0x14) += -0x1000;

            int y = *(int*)(e + 0x20);
            int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) * 60;   // Ghidra: FUN_00545a1c
            *(int*)(e + 0x20) = ((*(int*)(e + 0x48) + lift - y + 0x8c000) >> 3) + y;
        }

        int floor = *(int*)(chased + 0x64);
        if (((*(int*)(chased + 0x20) ^ floor) & unchecked((int)0xfffff000)) == 0) *(int*)(e + 0x48) = floor;
    }

    /// <summary>
    /// Ghidra: la queue commune — l'integration volante, la pose, le plancher (sauf contact
    /// <c>0x8000</c>), et <b>le heurt au masque <c>0x8882</c></b> : la marche droite (posture 7,
    /// 20 a 59 images, distance voulue <c>0x50</c>), le geste coupe, dix champs de placement rendus
    /// (pas la hauteur), et le contournement par <see cref="ProbeClearSide_004D5E60"/>. Puis
    /// l'etourdissement.
    /// </summary>
    /// <returns><c>false</c> quand le troisieme temps ne doit pas avoir lieu : immobile (le cap
    /// est alors rendu), ou encore hesitant.</returns>
    private static bool SettleValleyFlyer(byte* e, int speed, SceneEntityPlacement* keep, int boldness)
    {
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);             // recopiee en ligne
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        uint contact = *(uint*)(e + 0x90) & 0xdfffd8ff;
        *(uint*)(e + 0x90) = contact;

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (contact & 0x8000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e &= 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((contact & 0x8882) != 0)
        {
            if (e[0x42] != 7)
            {
                e[0x42] = 7;
                int roll = CrtRand();                               // Ghidra: FUN_00547140
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(roll % 0x28 + 0x14);
            }

            *(uint*)e &= 0xfffffffd;

            *(int*)(e + 0x24) = keep->Z;
            *(int*)(e + 0x1c) = keep->X;
            *(int*)(e + 0x7c) = keep->At7c;
            *(uint*)(e + 0x90) = keep->At90;                        // `and eax,0FFFFh` puis le dword
            *(int*)(e + 0x80) = keep->At80;
            *(int*)(e + 0x84) = keep->At84;
            *(int*)(e + 0x88) = keep->At88;
            *(int*)(e + 0x8c) = keep->At8c;
            *(int*)(e + 0x64) = keep->At64;
            *(int*)(e + 0x60) = keep->At60;

            int side = ProbeClearSide_004D5E60(e, 0x8882);
            if ((short)side == 0) side = SignedMask(CrtRand(), 1) * 2 - 1;   // Ghidra: FUN_00547140

            *(short*)(e + 0x32) += (short)((boldness + 0x40) * side);
        }

        ResetSceneEntityOnStun_004C3810(e);

        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep->Heading;
            return false;
        }

        return e[0x43] == 0;
    }
}
