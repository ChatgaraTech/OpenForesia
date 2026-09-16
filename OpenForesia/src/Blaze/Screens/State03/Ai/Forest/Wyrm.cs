// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE WYRM — 0x004A13E0, sous-type 12 du bloc B de la foret (zone 3, sous-zones 6 a 8).
//
// 2 416 octets, absents de `Blaze.c`. Cinquante-six etiquettes de saut.
//
// La charpente commune (postures, replanification, errance, hesitation) est dans
// `Ai/SceneEntityAiCommon.cs` ; la resolution du coup dans `Combat/HitResolution.cs` ;
// le ciblage dans `Entities/Targeting.cs`.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004A13E0 — LE WYRM, sous-type 12 du bloc B de la foret (sous-zones 6 a 8). Transcrite le
    // 2026-09-02 au desassembleur (Legacy/Raw/Blaze/FUN_004A13E0.asm, 2 416 octets, absente de
    // Blaze.c). Cinquante-six etiquettes, une table de sauts a quatre entrees a 0x004A1CD4.
    //
    // LE CRIBLE S'ETAIT TROMPE DE SŒUR, ET LA LISTE D'APPELS L'A CORRIGE EN UNE COMMANDE. Le relevé
    // du 2026-09-01 l'annoncait a 583 lignes du Crabe, une TERRESTRE. Or elle appelle
    // FUN_0049A8E0 (l'integration volante) et FUN_004A1CF0 (l'interruption volante), et pas
    // FUN_00488160 ni FUN_004DFAF0 : c'est une VOLANTE, et sa vraie sœur est Willie 0x004A35B0 —
    // meme queue, meme interruption conditionnelle sur +0x16, meme masque 0xdfffd8ff reserve a la
    // branche volante. Recompte apres coup : 584 lignes de Willie contre 594 du Crabe, c'est-a-dire
    // une egalite que le comptage ne peut pas trancher. LA LISTE D'APPELS, ELLE, TRANCHE.
    //
    // C'est la lecon du Varan appliquee le jour meme, et elle a servi deux fois de suite.
    //
    // CE QUI LUI EST PROPRE :
    //
    //   - ELLE A DEUX GESTES, ce qui la separe des quatre autres volantes : la Coccinelle,
    //     l'Abeille, l'Abeille tueuse et Willie n'en ont qu'un. Le coup (posture 0) et LA CHARGE
    //     (posture 1), lus dans +0x42 comme chez le Crabe, le Grizzly et le Geant. C'est le
    //     croisement des deux axes que Comportement.md decrit depuis l'Abeille : la queue est
    //     volante, la charpente est celle des terrestres a deux gestes.
    //
    //   - SA CHARGE A DEUX PHASES DANS +0x10, ET LA SECONDE DURE CE QU'ELLE VEUT. A 1, une seule
    //     image — la seizieme — verse (hardiesse + 0x500) << 7 d'un coup dans les vitesses, puis
    //     laisse (hardiesse + 0x60) << 7 de poussee tenue et passe a 0. A 0, elle balaie a chaque
    //     image TANT QUE SA VITESSE A PLAT DEPASSE 3, et ne se ferme que quand l'animation est finie
    //     ET que la vitesse est retombee sous hardiesse/8 + 1. C'est le geste du Trent, sur une
    //     volante.
    //
    //   - ET SA CHARGE PORTE LA PRISE. Le bit 0x20 est pose des que le balayage touche quelque
    //     chose, et il interdit tout second balayage jusqu'a la fin du geste : la charge ne blesse
    //     qu'UNE fois, si loin qu'elle aille. Sa victime part a 30 unites le long du cap VERS LA
    //     CIBLE (cap relatif compris, comme le Geant), a 0x18000 de haut, et l'effet 0x39 nait a la
    //     position de la victime, pas a celle du Wyrm.
    //
    //   - DEUX SONDES, ET LA PLUS HAUTE EST CELLE DU COUP. 0x005529b0 porte un decalage vertical de
    //     -0x100 — elle frappe SOUS elle, comme la Coccinelle —, 0x005529bc est plate et va deux
    //     fois plus loin devant (0x78 contre 0x64). Elle mord d'en haut et charge a l'horizontale.
    //
    //   - SES QUATRE POSTURES AMORTISSENT TOUTES LEUR POUSSEE D'UN SEIZIEME, ce qu'aucune terrestre
    //     ne fait : la poussee ne tombe jamais a zero d'un coup, elle glisse. Les postures 0 et 1
    //     la laissent decroitre, la 2 la ramene vers (hardiesse + 0x1a0) << 7, le geste du coup vers
    //     (hardiesse + 0x280) << 7. Une bete qui vole n'a pas de frein.
    //
    //   - UNE FOIS SUR CINQ, SA REPLANIFICATION NE CHANGE RIEN. Dans la bande du milieu, un tirage
    //     de 0x50 a 0x63 rend la main sans toucher ni la posture ni le rebours — donc le
    //     replanificateur retourne au meme point l'image suivante et retire. Ce n'est pas une
    //     branche vide oubliee : c'est un temps mort qui coute une image.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x005529b0</c> et <c>0x005529bc</c> — <b>les deux gabarits de portee du Wyrm</b>,
    /// releves dans <c>.data</c>.
    ///
    /// <para>
    /// <b>Ils ferment la region par le bas.</b> Entre la table des sorts du Trent
    /// (<see cref="s_TrentSpells_005529A8"/>, quatre mots) et le sort du Varan
    /// (<see cref="s_VaranSpell_005529C8"/>) il restait vingt-quatre octets sans emploi connu : ce
    /// sont exactement ces deux gabarits. La suite <c>0x00552978</c> → <c>0x005529f4</c> est
    /// desormais entierement attribuee.
    /// </para>
    /// <para>
    /// Le premier — celui du coup — porte le decalage vertical <c>-0x100</c> de la Coccinelle : la
    /// bete frappe <b>sous elle</b>. Le second — celui de la charge — est plat et porte plus loin,
    /// <c>0x78</c> contre <c>0x64</c>.
    /// </para>
    /// </summary>
    private static readonly short[] s_WyrmProbes_005529B0 =
    [
        0x0000, 0x001e, 0x0046, 0x0064, unchecked((short)0xff00), 0x005a,   // 0x005529b0 — le coup
        0x0000, 0x001e, 0x0032, 0x0078, 0x0000, 0x005a,                     // 0x005529bc — la charge
    ];

    private static short* s_WyrmProbeStorage;

    /// <summary>Une des deux entrees de <see cref="s_WyrmProbes_005529B0"/> en memoire native,
    /// puisque <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* WyrmProbe(int row)
    {
        if (s_WyrmProbeStorage == null)
        {
            s_WyrmProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_WyrmProbes_005529B0.Length, sizeof(short));

            for (int i = 0; i < s_WyrmProbes_005529B0.Length; i++)
            {
                s_WyrmProbeStorage[i] = s_WyrmProbes_005529B0[i];
            }
        }

        return s_WyrmProbeStorage + row * 6;
    }

    /// <summary>Ghidra: le rebours des trois sites d'errance et des deux repos du Wyrm —
    /// <c>hasard % 10 + timidite / 4 + 10</c>.
    ///
    /// <para><b>Ce n'est pas <see cref="SceneEntityDelay"/></b>, qui vaut
    /// <c>hasard % 10 + timidite / 2 + 0x14</c> : le Wyrm prend le quart de la timidite et non la
    /// moitie, et dix images de socle au lieu de vingt. Il se ravise deux fois plus vite que les
    /// terrestres.</para></summary>
    private static byte WyrmDelay(int timid) => (byte)(CrtRand() % 10 + (byte)(timid >> 2) + 10);

    /// <summary>
    /// Ghidra: <c>0x004a13e0</c> — <b>l'intelligence du Wyrm</b>, sous-type 12 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 6 a 8 de la foret. <b>Cinquieme bete volante
    /// du depot</b>, et la seule des cinq a porter deux gestes.
    ///
    /// <para>
    /// <b>Quatre postures</b>, table de sauts a <c>0x004A1CD4</c> : 0 attendre, 1 se tourner,
    /// 2 approcher, 3 filer vers un cap. <b>Deux gestes</b>, lus dans <c>+0x42</c> : posture 0 le
    /// coup, posture 1 la charge.
    /// </para>
    /// <para>
    /// <b>Son interruption est conditionnelle</b>, exactement comme celle de Willie :
    /// <c>+0x16</c> a <c>0x0a</c> ou plus lui donne l'interruption terrestre, en dessous la
    /// volante — et l'effacement des drapeaux de vue <c>0xdfffd8ff</c> n'accompagne que la seconde.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A13E0(byte* record)
    {
        byte* e = record;

        // Le bloc de modele sert a la charge, qui ampute l'attaque de moitie. Le coup, lui, porte
        // a pleine attaque.
        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideWyrmAction_004A13E0(e, model, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        HoverOnWings(e);

        // Ghidra: LAB_004a1a2a — le choix d'interruption, et le masque avec lui. Le meme couple que
        // chez Willie, au mot pres.
        if (*(short*)(e + 0x16) >= 0x0a)
        {
            InterruptSceneEntity_004E3770((uint*)e, stats);
        }
        else
        {
            *(uint*)(e + 0x90) &= 0xdfffd8ff;
            InterruptFlyingEntity_004A1CF0((uint*)e, stats);
        }

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanWyrmAction_004A13E0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a144a..0x004a19a7</c> — <b>le premier temps</b> du Wyrm.
    ///
    /// <para>
    /// <b>Les quatre postures amortissent leur poussee d'un seizieme</b>, chacune a sa facon : les
    /// deux premieres la laissent decroitre vers zero, la troisieme la ramene vers
    /// <c>(hardiesse + 0x1a0) &lt;&lt; 7</c>, la quatrieme la remet a zero apres avoir verse son
    /// impulsion. Aucune terrestre ne fait cela — chez elles <c>+0x5c</c> est pose ou efface d'un
    /// coup.
    /// </para>
    /// </summary>
    private static void DecideWyrmAction_004A13E0(
        byte* e, byte* model, int boldness, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
                return;
            }

            switch (e[0x42])
            {
                case 0: WyrmStrikeGesture_004A16D7(e, boldness, heading, flags); return;
                case 1: WyrmChargeGesture_004A1486(e, model, boldness, heading); return;
                default: return;
            }
        }

        // ================================================================ la posture tenue
        if ((flags & 0x10) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte held = e[0x42];
        if (held > 3) return;

        switch (held)
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x18) != 0)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 0;
                    *(uint*)e = flags;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                *(int*)(e + 0x5c) -= *(int*)(e + 0x5c) >> 4;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                // Elle garde l'animation 0 pour se tourner, la ou toute la famille passe a 1 : le
                // Wyrm ne marche pas, il derive.
                if (*(ushort*)(e + 0x18) != 0)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 0;
                    *(uint*)e = flags;
                }

                if (TurnSceneEntityTowardTarget(e, (boldness >> 2) + 0x38, heading)) e[0x43] = 0;

                *(int*)(e + 0x5c) -= *(int*)(e + 0x5c) >> 4;
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, (boldness >> 2) + 0x38, heading);

                // La poussee de croisiere, rejointe par seiziemes et jamais atteinte.
                *(int*)(e + 0x5c) += (((boldness + 0x1a0) << 7) - *(int*)(e + 0x5c)) >> 4;
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            default:
            {
                if (*(ushort*)(e + 0x18) != 0)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 0;
                    *(uint*)e = flags;
                }

                if (e[0x43] != 0) e[0x43]--;

                // Une impulsion versee A CHAQUE IMAGE dans les vitesses, le long du cap range en
                // +0x10, puis la poussee remise a zero : l'integration ne verra que ce que les
                // vitesses ont garde.
                int thrust = (boldness + 0x40) << 7;
                *(int*)(e + 0x5c) = thrust;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * thrust) >> 0xc;
                *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * thrust) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, (boldness >> 2) + 0x40, heading);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a16d7..0x004a17a6</c> — <b>le coup</b>, le geste de la posture 0.
    ///
    /// <para>
    /// <b>Aucun controle d'image</b> : le balayage a lieu a chaque image du geste, comme chez la
    /// Coccinelle, et la sonde <c>0x005529b0</c> est celle qui porte le decalage vertical
    /// <c>-0x100</c> — le Wyrm frappe sous lui.
    /// </para>
    /// <para>
    /// <b>Et il porte a pleine attaque</b> : c'est la charge, pas le coup, qui ampute le bloc de
    /// modele. Le geste finit sur le rebours <c>+0x43</c>, pas sur l'animation.
    /// </para>
    /// </summary>
    private static void WyrmStrikeGesture_004A16D7(byte* e, int boldness, int heading, uint flags)
    {
        if (*(ushort*)(e + 0x18) != 3)
        {
            *(uint*)e = flags & 0xfeffffff;
            *(int*)(e + 0x18) = 3;
        }

        TurnSceneEntityTowardTarget(e, (boldness >> 1) + 0x40, heading);

        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), WyrmProbe(0), 0);

        if (*Mem.Camera(0x005de0b8) != 0)
        {
            *(ushort*)(victim + 0x96) = 0x10;
            *(uint*)victim |= 0x40;

            ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
            Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
        }

        // Ghidra: LAB_004a176c — la poussee du coup, rejointe par seiziemes.
        *(int*)(e + 0x5c) += (((boldness + 0x280) << 7) - *(int*)(e + 0x5c)) >> 4;

        if (e[0x43] == 0) return;
        if (--e[0x43] != 0) return;

        *(uint*)e &= 0xfffffffd;
    }

    /// <summary>
    /// Ghidra: <c>0x004a1486..0x004a16d6</c> — <b>la charge</b>, le geste de la posture 1, en deux
    /// phases lues dans <c>+0x10</c>.
    ///
    /// <para>
    /// <b>Phase 1, l'elan</b> : une seule image — la seizieme — verse
    /// <c>(hardiesse + 0x500) &lt;&lt; 7</c> d'un coup dans les deux vitesses a plat, puis laisse
    /// <c>(hardiesse + 0x60) &lt;&lt; 7</c> de poussee tenue et passe en phase 0. Les autres images
    /// ne font que tourner, cone <c>hardiesse / 2 + 0x40</c>.
    /// </para>
    /// <para>
    /// <b>Phase 0, le balayage</b> : il n'a lieu que si la vitesse a plat depasse <b>3</b>, et le
    /// geste ne se ferme que quand l'animation est finie <b>et</b> que cette vitesse est retombee
    /// sous <c>hardiesse / 8 + 1</c>. C'est le geste du Trent, a ceci pres qu'il porte en plus la
    /// prise.
    /// </para>
    /// <para>
    /// <b>La prise, justement</b> : le bit <c>0x20</c> est pose des que le balayage rencontre
    /// quelqu'un, et il barre tout balayage ulterieur jusqu'a la fermeture du geste. <b>Une charge
    /// ne blesse qu'une fois</b>, si longue qu'elle soit. Les deux bits tombent ensemble
    /// (<c>0xffffffdd</c>) a la fermeture.
    /// </para>
    /// </summary>
    private static void WyrmChargeGesture_004A1486(
        byte* e, byte* model, int boldness, int heading)
    {
        short phase = *(short*)(e + 0x10);

        // -------------------------------------------------------------- 1 : l'elan
        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0;

            if (*(short*)(e + 0x1a) == 0x10)
            {
                int burst = (boldness + 0x500) << 7;
                *(int*)(e + 0x5c) = burst;

                int course = *(short*)(e + 0x32);
                *(int*)(e + 0x50) +=
                    ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x58) +=
                    (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

                *(int*)(e + 0x5c) = (boldness + 0x60) << 7;
                *(short*)(e + 0x10) -= 1;
            }

            TurnSceneEntityTowardTarget(e, (boldness >> 1) + 0x40, heading);
            return;
        }

        if (phase != 0) return;

        // -------------------------------------------------------------- 0 : le balayage
        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;
        int flat = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));

        uint flags = *(uint*)e;

        if ((flags & 0x2000000) != 0 && flat <= (boldness >> 3) + 1)
        {
            // Le geste ET la prise tombent ensemble.
            *(uint*)e = flags & 0xffffffdd;
        }

        if (flat < 3) return;
        if ((*(uint*)e & 0x20) != 0) return;

        TurnSceneEntityTowardTarget(e, (boldness >> 3) + 0x20, heading);

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), WyrmProbe(1), 0);
        if (prey != null) *(uint*)e |= 0x20;

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(prey + 0x96) = 0x10;
        *(uint*)prey |= 0x40;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

        if ((*(uint*)prey & 0x40000000) != 0) return;

        // La projection part vers la CIBLE, cap relatif compris — le geste du Geant.
        int impact = heading + *(short*)(e + 0x32);
        *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(impact) * 30;
        *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(impact) * 30;

        uint preyFlags = *(uint*)prey;
        if ((preyFlags & 1) == 0)
        {
            *(int*)(prey + 0x54) = 0x18000;
            *(uint*)prey = preyFlags | 1;
        }

        // Ghidra: LAB_004a1688 — l'effet nait a la position de la VICTIME, pas a celle du Wyrm.
        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) = *(int*)(prey + 0x1c);
        *(int*)(e + 0x20) = *(int*)(prey + 0x20);
        *(int*)(e + 0x24) = *(int*)(prey + 0x24);

        SpawnEffectEntity(e, 0x39, 0, -1);               // Ghidra: FUN_0047a9d0

        *(int*)(e + 0x1c) = savedX;
        *(int*)(e + 0x20) = savedY;
        *(int*)(e + 0x24) = savedZ;
    }

    /// <summary>
    /// Ghidra: <c>0x004a1a81..0x004a1cd2</c> — <b>le troisieme temps</b> du Wyrm.
    ///
    /// <para>
    /// Trois plages, comme la famille : sous <c>0xfa</c> le corps a corps, sous <c>0x1f4</c>
    /// l'approche, au-dela l'errance. Mais elles ne se ressemblent pas.
    /// </para>
    /// <para>
    /// <b>Le corps a corps n'a que deux issues</b> — mordre ou errer, six fois sur dix pour la
    /// premiere —, et l'approche en a trois dont <b>une qui ne fait rien</b> : un tirage de
    /// <c>0x50</c> a <c>0x63</c> rend la main sans toucher ni la posture ni le rebours, si bien que
    /// le replanificateur retire a l'image suivante. Une image perdue sur cinq, a cette distance.
    /// </para>
    /// <para>
    /// <b>Son seuil d'engagement se retranche</b> : <c>0x12c - hasard % (timidite * 2 + 1)</c>, la
    /// forme de l'Abeille tueuse, mais avec <c>0x12c</c> alors que la plage s'arrete a
    /// <c>0xfa</c>. Le seuil est donc <b>toujours au-dessus de la plage</b> : un Wyrm hardi mord
    /// sans condition de distance, et seul un Wyrm tres craintif peut se voir refuser un
    /// engagement.
    /// </para>
    /// </summary>
    private static void ReplanWyrmAction_004A13E0(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        // ================================================================ le corps a corps
        if (distance < 0xfa)
        {
            if (CrtRand() % 100 < 0x3c)                  // Ghidra: FUN_00547140
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) > timid * 8 + 0x200)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                if (distance >= 0x12c - CrtRand() % (timid * 2 + 1))
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Ghidra: 0x004a1b29 — armer LE COUP. Aucune animation n'est posee ici : le geste
                // s'en charge a sa premiere image.
                *(uint*)e |= 2;
                e[0x42] = 0;
                e[0x43] = (byte)((timid >> 3) + 0x10);
                return;
            }

            // Ghidra: LAB_004a1b47 — l'errance du corps a corps, et la seule des trois dont la
            // fourchette porte le socle 0x80 : (hardiesse + 0x80) * 16.
            e[0x42] = 3;
            e[0x43] = WyrmDelay(timid);
            SetWyrmWanderCourse(e, (boldness + 0x80) << 4, heading);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;                  // Ghidra: FUN_00547140

            if (roll < 0x3c)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200)
                {
                    // Ghidra: 0x004a1bb8 — armer LA CHARGE, en phase 1 et sur l'animation 4. Le
                    // rebours n'est PAS pose : c'est le bit de geste qui bloque la replanification.
                    uint flags = *(uint*)e;
                    *(int*)(e + 0x18) = 4;
                    e[0x42] = 1;
                    *(ushort*)(e + 0x10) = 1;
                    *(uint*)e = (flags & 0xfeffffff) | 2;
                    return;
                }

                // Ghidra: LAB_004a1bde — l'errance de l'approche. Sa fourchette est
                // hardiesse * 16 + 1, sans le socle 0x80 : le +1 est ce qui evite la division par
                // zero quand la hardiesse est nulle, et il manque au site du corps a corps.
                e[0x42] = 3;
                e[0x43] = WyrmDelay(timid);
                SetWyrmWanderCourse(e, (boldness << 4) + 1, heading);
                return;
            }

            if (roll < 0x50)
            {
                e[0x42] = 2;
                e[0x43] = WyrmDelay(timid);
                return;
            }

            // Ghidra: 0x004a1c28 — LA BRANCHE VIDE. Ni posture, ni rebours : la replanification
            // retournera ici a l'image suivante.
            return;
        }

        // ================================================================ le lointain
        int far = CrtRand() % 100;                       // Ghidra: FUN_00547140

        if (far < 0x1e)
        {
            e[0x42] = 0;
            e[0x43] = WyrmDelay(timid);
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        // Ghidra: LAB_004a1ca3 — l'errance du lointain, et la seule des trois qui prenne un cap
        // ABSOLU tire au hasard, sans rapport avec la cible.
        e[0x42] = 3;
        e[0x43] = WyrmDelay(timid);
        *(short*)(e + 0x10) = (short)CrtRand();          // Ghidra: FUN_00547140
    }

    /// <summary>Ghidra: <c>LAB_004a1c03</c> — <b>le cap d'errance</b> des deux premieres plages :
    /// celui de la cible, plus un ecart tire dans une fourchette centree de largeur
    /// <paramref name="spread"/>.
    ///
    /// <para>C'est <see cref="WanderSceneEntity"/> sans le biais de demi-tour — le Wyrm erre
    /// <b>vers</b> vous — et sans la posture ni le rebours, que les deux sites posent eux-memes
    /// parce qu'ils ne prennent pas la meme fourchette.</para></summary>
    private static void SetWyrmWanderCourse(byte* e, int spread, int heading)
    {
        int offset = CrtRand() % spread - (spread >> 1);   // Ghidra: FUN_00547140
        *(short*)(e + 0x10) = (short)(offset + heading + *(short*)(e + 0x32));
    }
}
