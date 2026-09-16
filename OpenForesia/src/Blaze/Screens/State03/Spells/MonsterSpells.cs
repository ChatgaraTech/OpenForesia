// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SORTS DE MONSTRE — les familles 4 et 6 du repertoire d'etat.
//
// Le repertoire en `+0x9c` porte HUIT familles, dont les quatre premieres seulement sont les
// ecoles. Les quatre au-dela ne sont atteintes par aucune classe jouable : ce sont
// vraisemblablement les sorts que les monstres lancent, et leur etendue n'est pas etablie - c'est
// une hypothese datee, pas un fait releve dans le binaire.
//
// Deux corps seulement manquaient au depot, et ils sont ici : le TRAIT (`004477D0` qui lance,
// `004478F0` qui vole) et l'ORBE (`004769A0` qui lance, `004769F0` qui vole). Meme forme que les
// sorts de joueur - une entite de lancement, puis un projectile -, meme entree par
// `CastSpell_0053A980`.
//
// Les quatre lanceurs de sorts du depot cote monstre sont le Behemoth, le Trent, le Varan et le
// sous-type 1 de l'auberge ; ils partagent le meme bloc d'incantation, recopie et non reecrit.
// Leurs corps sont dans `Ai/`.
//
// Vue d'ensemble : Documentation/Reference/Sorts.md ; feuille de route : Documentation/Etat/SortsAtteignables.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES SORTS DE MONSTRE — familles 4 et 6, les deux seuls corps qui manquaient
    //
    //   0x004477D0   LE SORT DE LA FAMILLE 4. Sous-type 0x4b de 0x00551c30. 288 octets, ABSENT de
    //                Blaze.c ; releve sous Legacy/Raw/Blaze/FUN_004477D0.asm.
    //   0x004478F0   SON TRAIT, sous-type 0x51 de 0x00551f30. 415 octets, ABSENT de Blaze.c ;
    //                releve sous Legacy/Raw/Blaze/FUN_004478F0.asm.
    //   0x004769A0   LE SORT DE LA FAMILLE 6. Sous-type 0x4a de 0x00551c30. DECOMPILE, sept lignes.
    //   0x004769F0   SA BOULE, sous-type 0x28 de 0x00552230. 635 octets, ABSENTE de Blaze.c ;
    //                releve sous Legacy/Raw/Blaze/FUN_004769F0.asm.
    //
    // LA TABLE +0x9c PORTE HUIT FAMILLES, ET LES QUATRE DERNIERES NE SONT DESIGNEES PAR PERSONNE —
    // ni par une classe (0x00562ab8 n'en nomme que 0 a 3), ni par un script d'objet (l'opcode 14
    // n'emploie que 0, 1 et 2). Le seul appelant restant de CastSpell_0053A980 est le mot +0x06
    // d'une entite de scene : ce sont les sorts que les monstres lancent. Leurs noms sont restes
    // dans le jeu de caracteres d'origine, jamais traduits.
    //
    // ET SUR LES 43 FICHES RELEVEES, QUATRE SEULEMENT DESIGNENT UN VRAI CORPS. Les 39 autres
    // tombent sur DespawnEntity_0047ADC0, le bouche-trou. Des quatre, la famille 5 emploie
    // 0x0047a100 et la famille 7 emploie le corps de Meteores (0x00482370) — tous deux deja
    // ecrits. Restaient ces deux-ci. Le detail qui compte : leurs sous-types sont 0x49, 0x4a et
    // 0x4b, TROIS CRENEAUX CONSECUTIFS sur la PREMIERE fiche des familles 5, 6 et 4. Trois
    // emplacements voisins reserves a trois sorts de monstre ; tout le reste des 43 est du
    // remplissage, et l'etendue reelle de ces familles n'est toujours pas etablie.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004477d0 @ 0x004477d0 — <b>le sort de la famille 4</b> : le sous-type
    /// <c>0x4b</c>, creneau 75 de <see cref="s_StaticEntityHandlers_00551c30"/>. 288 octets,
    /// <b>absent de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004477D0.asm</c>.
    ///
    /// <para>
    /// <b>C'est un depart de trait, et rien d'autre.</b> A sa premiere image il se fait haut et
    /// mince (<c>0x800</c> de large, <c>0x157c</c> de haut, <c>0xc00</c> de profond), se teinte de
    /// blanc plein, s'avance de <b>huit unites</b> dans son propre cap, se donne une vitesse de
    /// <b>neuf fois ce pas</b>, range le lanceur en <c>+0x78</c> et lache le trait
    /// <see cref="TickMonsterBolt_004478F0"/>. Puis il ne fait plus que <b>reprendre le meme eclat
    /// deux fois</b>, aux images 1 et 2, chaque fois plus sombre, et il s'eteint a la seizieme.
    /// </para>
    /// <para>
    /// <b>L'assombrissement se fait par debordement d'octet</b> : il ajoute <c>0xc0</c> aux trois
    /// composantes, donc <c>0xff</c> devient <c>0xbf</c> puis <c>0x7f</c>. C'est bien une addition
    /// dans le binaire, pas une soustraction, et le repli sur huit bits est ce qui la rend
    /// decroissante. Transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Il ne s'efface pas, il se desarme</b> : les deux sorties tombantes font
    /// <c>&amp;= 0x7fffffff</c> — elles enlevent le bit de vie et laissent le reste du mot debout —
    /// la ou la plupart des corps du depot ecrivent un zero franc. Son enfant fait de meme.
    /// </para>
    /// <para>
    /// Le bruit du depart n'est joue que si le bit <c>0x4000</c> de <c>+0x12</c> est absent.
    /// </para>
    /// </summary>
    private static void TickMonsterBoltCast_004477D0(byte* record)
    {
        byte* e = record;

        // Le porteur s'en va : on perd le bit de vie, et on continue quand meme cette image.
        if ((*(uint*)e & 0x40000000) != 0) *(uint*)e &= 0x7fffffff;

        short age = *(short*)(e + 0x10);

        if (age == 0)
        {
            e[0x36] |= 2;

            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;

            *(uint*)e |= 0x682;

            *(ushort*)(e + 0x28) = 0x800;
            *(ushort*)(e + 0x2c) = 0xc00;
            *(ushort*)(e + 0x2a) = 0x157c;

            short yaw = *(short*)(e + 0x32);

            int stepX = (int)Winsys3d.GetSinTableEntry(yaw) << 3;
            int stepZ = Winsys3d.GetCosTableEntry(yaw) << 3;

            *(int*)(e + 0x1c) += stepX;
            *(int*)(e + 0x24) += stepZ;

            // Neuf fois le pas d'avance : le trait part vite.
            *(int*)(e + 0x50) = stepX * 9;
            *(int*)(e + 0x58) = stepZ * 9;

            e[0x04] = 0x1e;

            // Le lanceur, prete au trait par la voie longue : lui le lira en +0x6c de son pere.
            *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);

            SpawnCastEntity_0047AAE0(e, 0x51, 0, 4);

            if ((*(ushort*)(e + 0x12) & 0x4000) == 0)
            {
                Aud.PlayPositionalSound(0x23, 0xca6, 0x1800, (int*)(e + 0x1c));
            }

            e[0x13] |= 0x40;
            return;
        }

        // Ghidra: LAB_0044789e — DEUX RAPPELS, aux images 1 et 2.
        if (age < 3)
        {
            // L'assombrissement par debordement : 0xff, 0xbf, 0x7f.
            e[0x38] += 0xc0;
            e[0x39] += 0xc0;
            e[0x3a] += 0xc0;

            *(uint*)e |= 0x2000;
            SpawnCastEntity_0047AAE0(e, 0x51, 0, 4);
            *(uint*)e &= 0xffffdfff;
        }

        // Ghidra: LAB_004478db
        *(short*)(e + 0x10) += 1;

        if (*(short*)(e + 0x10) >= 0x10) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_004478f0 @ 0x004478f0 — <b>le trait de la famille 4</b> : le sous-type
    /// <c>0x51</c>, creneau 81 de <see cref="s_StaticEntityHandlers_00551f30"/>. 415 octets,
    /// <b>absent de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004478F0.asm</c>.
    ///
    /// <para>
    /// <b>Il vole huit images et frappe qui il touche.</b> Sa vitesse est amortie de <b>trois
    /// huitiemes</b> par image (un quart plus un huitieme, dans cet ordre) sur X et Z, jamais sur
    /// Y ; sa largeur perd <c>0x70</c> par image et sa profondeur <c>0xf0</c>, sa hauteur ne bouge
    /// pas ; sa teinte perd <c>0x0f</c> sur les trois composantes tant qu'elle le peut.
    /// </para>
    /// <para>
    /// <b>Il retrouve le lanceur par la voie longue</b> : son <c>+0x6c</c> est le <b>sort</b>, et
    /// c'est le <c>+0x6c</c> du sort qui porte le lanceur. Il le recopie en <c>+0x74</c> a chaque
    /// image, et c'est ce <c>+0x74</c> qu'il donnera comme assaillant au coup.
    /// </para>
    /// <para>
    /// <b>Le decor l'arrete</b> : une fois pose par <see cref="FinalizeEntityPlacement2"/>, si son
    /// etat <c>+0x90</c> porte <c>0x802</c>, ou s'il est descendu plus bas que le sol moins
    /// <c>0x32000</c>, ou si son porteur est marque, alors <b>le sort et lui prennent tous deux le
    /// bit <c>0x8000</c> de <c>+0x12</c></b> et sa vitesse tombe a zero. Sinon il se cale sur le
    /// sol. Ce bit est ce qui, a l'image suivante, lui fait sauter la pose : il est fiche en
    /// terre.
    /// </para>
    /// <para>
    /// <b>La recherche de cible change de camp selon le sien</b> : <c>0x600000</c> pose, il balaye
    /// le groupe (<see cref="SweepPartyForMeleeHit_00519C30"/>) ; sinon les entites de scene
    /// (<see cref="SweepSceneEntitiesForMeleeHit_00519AF0"/>). <b>Ce second appel ne passe que
    /// trois arguments la ou la fonction en lit quatre</b> — voir LB-029 dans
    /// <c>Documentation/Reference/legacy-bug.md</c>. Ce qui est touche prend un coup d'arme
    /// (<see cref="ResolveMeleeHit_00539B80"/>), le bit <c>0x40</c> et <c>0x14</c> en
    /// <c>+0x96</c>.
    /// </para>
    /// <para>
    /// <b>A la huitieme image il force l'age de son sort a 8</b> avant de se desarmer : c'est le
    /// seul moyen qu'il ait de dire au sort qu'il est fini, et le sort, qui meurt de toute facon a
    /// seize, s'en trouve avance.
    /// </para>
    /// </summary>
    private static void TickMonsterBolt_004478F0(byte* record)
    {
        byte* e = record;

        // La boite de detection, reconstruite sur la pile a chaque image.
        short* probe = stackalloc short[6] { 0, -50, 0x32, 0x82, 0, 0x64 };

        byte* spell = *(byte**)(e + 0x6c);

        // Le lanceur, retrouve par la voie longue : le +0x6c du sort.
        *(byte**)(e + 0x74) = *(byte**)(spell + 0x6c);

        if ((*(uint*)spell & 0x40000000) != 0) *(uint*)e &= 0x7fffffff;

        // Ghidra: LAB_0044793c
        if (*(short*)(e + 0x10) == 0) *(ushort*)(e + 0x12) &= 0x7fff;

        // Ghidra: LAB_00447948 — l'avance, puis l'amortissement de trois huitiemes.
        int velX = *(int*)(e + 0x50);
        int velZ = *(int*)(e + 0x58);

        *(int*)(e + 0x24) += velZ;
        *(int*)(e + 0x1c) += velX;

        *(ushort*)(e + 0x28) -= 0x70;
        *(ushort*)(e + 0x2c) -= 0xf0;

        *(int*)(e + 0x50) = velX - (velX >> 3) - (velX >> 2);
        *(int*)(e + 0x58) = velZ - (velZ >> 2) - (velZ >> 3);

        if (e[0x38] >= 0x0f)
        {
            e[0x38] -= 0x0f;
            e[0x39] += 0xf1;                              // = -0x0f, ecrit ainsi dans le binaire
            e[0x3a] += 0xf1;
        }

        // Ghidra: LAB_004479ab — fiche en terre, il ne se pose plus.
        if ((*(ushort*)(e + 0x12) & 0x8000) == 0)
        {
            FinalizeEntityPlacement2((uint*)e);

            uint state = *(uint*)(e + 0x90);
            int ground = *(int*)(e + 0x60);
            int y = *(int*)(e + 0x20);
            int floor = ground - 0x32000;

            if ((state & 0x802) != 0
                || y < floor
                || (*(uint*)e & 0x40000000) != 0
                || (*(ushort*)(spell + 0x12) & 0x8000) != 0)
            {
                // Ghidra: LAB_004479f5 — le sort et le trait se fichent ensemble.
                *(ushort*)(spell + 0x12) |= 0x8000;
                *(ushort*)(e + 0x12) |= 0x8000;
                *(int*)(e + 0x50) = 0;
                *(int*)(e + 0x58) = 0;
            }
            else if (y <= ground && y > floor)
            {
                *(int*)(e + 0x20) = ground;               // il se cale sur le sol
            }
        }

        // Ghidra: LAB_00447a0a — le bit 0x4000 lui interdit de frapper.
        if ((*(ushort*)(e + 0x12) & 0x4000) == 0)
        {
            byte* hit = (*(uint*)e & 0x600000) != 0
                ? SweepPartyForMeleeHit_00519C30(
                      *(byte**)(e + 0x74), (int*)(e + 0x1c), probe, 0)
                // LB-029 : le binaire n'empile ici que TROIS arguments. Le quatrieme que lit la
                // fonction est l'edi sauve par le prologue, donc une valeur quelconque.
                : SweepSceneEntitiesForMeleeHit_00519AF0(
                      *(byte**)(e + 0x74), (int*)(e + 0x1c), probe, 0);

            if (hit != null)
            {
                ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)hit);
                *(uint*)hit |= 0x40;
                *(ushort*)(hit + 0x96) = 0x14;
            }
        }

        // Ghidra: LAB_00447a6a — huit images, et il avance l'age de son sort en partant.
        *(short*)(e + 0x10) += 1;

        if (*(short*)(e + 0x10) >= 8)
        {
            *(ushort*)(spell + 0x10) = 8;
            *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_004769a0 @ 0x004769a0 — <b>le sort de la famille 6</b> : le sous-type
    /// <c>0x4a</c>, creneau 74 de <see cref="s_StaticEntityHandlers_00551c30"/>. <b>Sept lignes</b>,
    /// et c'est le plus court corps de sort du depot.
    ///
    /// <para>
    /// <b>Il ne vit qu'une image</b> : il se donne une gravite de <c>-20</c>, lache la boule
    /// <see cref="TickMonsterOrb_004769F0"/> avec le huitieme descripteur de la plage
    /// <see cref="s_Descriptors_00551BA0"/>, joue son bruit et s'efface. Tout ce que fait le sort
    /// est dans son enfant.
    /// </para>
    /// </summary>
    private static void TickMonsterOrbCast_004769A0(byte* record)
    {
        byte* e = record;

        e[0x36] |= 2;
        *(short*)(e + 0x2e) = -20;

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551BD8(), 0x28, 0, 2);

        Aud.PlayPositionalSound(0x23, 0x1e9c, 0xe00, (int*)(e + 0x1c));

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004769f0 @ 0x004769f0 — <b>la boule de la famille 6</b> : le sous-type
    /// <c>0x28</c>, creneau 40 de <see cref="s_StaticEntityHandlers_00552230"/>. 635 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004769F0.asm</c>.
    ///
    /// <para>
    /// <b>Elle nait devant le lanceur, a mi-hauteur de sa tete</b> — le <c>(stature &amp; ~1) &lt;&lt;
    /// 11</c> qu'emploie aussi <see cref="TickSleepBubble_00476CF0"/> — et prend la vitesse du
    /// lanceur plus <b>quarante-huit unites</b> dans son propre cap. Ensuite elle ne fait plus que
    /// tomber : sa vitesse horizontale est amortie d'un quart par image, sa vitesse verticale est
    /// celle que le sort lui a donnee.
    /// </para>
    /// <para>
    /// <b>Sa teinte est son sablier</b> : elle perd <c>0x18</c> sur les trois composantes par
    /// image, et des qu'elle ne peut plus, elle <b>perd son bit de vie</b> — dix images, la teinte
    /// de depart <c>0xf0</c> etant exactement dix fois le pas. Et tant que cette teinte reste
    /// <b>au-dessus de <c>0x60</c></b>, elle seme a chaque image une etincelle <c>0xbe</c> dans une
    /// direction tiree au sort autour de son cap et de son tangage (<c>±0x100</c> sur l'un,
    /// <c>±0x100</c> autour de <c>0x900</c> sur l'autre) : <b>cinq images de trainee, puis plus
    /// rien</b>. Les six champs qu'elle emprunte pour engendrer — vitesse, echelle, teinte — lui
    /// sont rendus juste apres.
    /// </para>
    /// <para>
    /// <b>Trois fins, et deux seulement l'effacent.</b> Si son etat <c>+0x90</c> porte
    /// <c>0xa000</c>, ou si elle est encore en l'air, elle cherche un adversaire par
    /// <see cref="SweepOpposingSideForMeleeHit_00474C70"/> : ce qu'elle touche prend un coup d'arme
    /// et <b>la moitie de sa vitesse en poussee</b>, et elle disparait. Sinon elle a atteint le
    /// sol : elle s'y cale, y pose l'eclat <c>0xbd</c> et disparait. Et si elle ne touche rien en
    /// l'air, elle vieillit d'une image.
    /// </para>
    /// <para>
    /// <b>La perte du bit de vie ne l'arrete pas</b> : le corps continue jusqu'au bout de l'image,
    /// et peut encore frapper. C'est la passe de la table qui la retirera.
    /// </para>
    /// </summary>
    private static void TickMonsterOrb_004769F0(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { 0, -50, 0x32, 0x64, 0, 0x64 };

        if (*(short*)(e + 0x10) == 0)
        {
            byte* caster = *(byte**)(e + 0x74);

            *(ushort*)(e + 0x28) = 0xc00;
            *(ushort*)(e + 0x2a) = 0xc00;
            *(uint*)(e + 0x38) = 0x00f0f0f0;

            int height = StatusBubbleHeight_00538BC0((uint*)caster);

            // A MI-HAUTEUR DE TETE : la stature paire, decalee de onze et non de douze.
            *(int*)(e + 0x20) += (height & ~1) << 0xb;

            short yaw = *(short*)(e + 0x32);

            // La vitesse du lanceur, plus quarante-huit unites dans son propre cap.
            *(int*)(e + 0x50) = ((int)Winsys3d.GetSinTableEntry(yaw) * 3 << 4)
                                + *(int*)(caster + 0x50);
            *(int*)(e + 0x58) = (Winsys3d.GetCosTableEntry(yaw) * 3 << 4)
                                + *(int*)(caster + 0x58);
        }

        // Ghidra: LAB_00476a87 — LE SABLIER.
        if (e[0x38] >= 0x18)
        {
            e[0x38] -= 0x18;
            e[0x39] += 0xe8;                              // = -0x18, ecrit ainsi dans le binaire
            e[0x3a] += 0xe8;
        }
        else
        {
            // Ghidra: LAB_00476aa7 — elle perd son bit de vie et poursuit quand meme cette image.
            *(uint*)e &= 0x7fffffff;
        }

        // Ghidra: LAB_00476aad — LA TRAINEE, tant que la teinte est au-dessus de 0x60.
        if (e[0x38] > 0x60)
        {
            int savedVelX = *(int*)(e + 0x50);
            int savedVelY = *(int*)(e + 0x54);
            int savedVelZ = *(int*)(e + 0x58);
            int savedScale = *(int*)(e + 0x28);
            int savedDepth = *(int*)(e + 0x2c);
            int savedTint = *(int*)(e + 0x38);

            *(byte**)(e + 0x70) = null;

            int yaw = *(short*)(e + 0x32) - (CrtRand() & 0x1ff) + 0x100;
            int pitch = *(short*)(e + 0x34) - (CrtRand() & 0x1ff) + 0x900;

            // La direction de l'etincelle est ecrite PAR-DESSUS sa propre vitesse : c'est elle que
            // l'enfant heritera, et elle est rendue juste apres.
            SetVectorFromAngles_00532AD0((int*)(e + 0x50), yaw, pitch, 0x10);

            *(ushort*)(e + 0x12) = 3;
            *(uint*)(e + 0x28) = 0x06400640;
            *(ushort*)(e + 0x14) = 0x640;
            *(ushort*)(e + 0x16) = 0x1f;
            *(uint*)(e + 0x38) = 0x00808080;
            *(ushort*)(e + 0x1a) = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA8(), 0xbe, e[0x0b], -1);

            *(int*)(e + 0x54) = savedVelY;
            *(int*)(e + 0x58) = savedVelZ;
            *(int*)(e + 0x50) = savedVelX;
            *(int*)(e + 0x28) = savedScale;
            *(int*)(e + 0x2c) = savedDepth;
            *(int*)(e + 0x38) = savedTint;
        }

        // Ghidra: LAB_00476b8a — l'avance, et un quart d'amortissement horizontal.
        int velX = *(int*)(e + 0x50);
        int velZ = *(int*)(e + 0x58);

        *(int*)(e + 0x24) += velZ;
        *(int*)(e + 0x1c) += velX;

        *(int*)(e + 0x50) = velX - (velX >> 2);
        *(int*)(e + 0x58) = velZ - (velZ >> 2);

        // Y est mis de cote AVANT la pose et rendu apres : la pose le corrige, la boule non.
        int y = *(int*)(e + 0x20);

        FinalizeEntityPlacement2((uint*)e);

        uint state = *(uint*)(e + 0x90);
        *(int*)(e + 0x20) = y;

        int ground = *(int*)(e + 0x60);

        bool airborne = (state & 0xa000) != 0
                        || ((state & 0x802) == 0 && y > ground);

        if (!airborne)
        {
            // Ghidra: LAB_00476bdc — ELLE A TOUCHE LE SOL.
            if (y <= ground) *(int*)(e + 0x20) = ground;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551A08(), 0xbd, 0, 4);

            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00476c07 — ELLE CHERCHE QUELQU'UN.
        byte* hit = SweepOpposingSideForMeleeHit_00474C70(e, probe);

        if (hit != null)
        {
            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)hit);

            // LA MOITIE DE SA VITESSE, PASSEE EN POUSSEE a ce qu'elle vient de frapper.
            *(int*)(hit + 0x50) += *(int*)(e + 0x50) >> 1;
            *(int*)(hit + 0x58) += *(int*)(e + 0x58) >> 1;

            *(uint*)hit |= 0x40;
            *(ushort*)(hit + 0x96) = 0x14;

            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00476c5f
        *(short*)(e + 0x10) += 1;
    }
}
