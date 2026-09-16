// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES GESTIONNAIRES D'ENTITE « COMMUNS » — la famille 0x004734B0..0x00473CB0, et ce qui les sert.
//
// Ce sont les comportements qu'une entite peut avoir DANS N'IMPORTE QUELLE ZONE : le saut
// (0x004734B0), la vitesse amortie (0x004738A0), la glissade (0x00473CB0). Aucun n'appartient a
// une carte ; ils sont installes partout.
//
// Autour d'eux, ce que la carte du monde emploie sans que ce soit ni de l'IA ni de la magie :
// le creneau d'equipe actif, la queue d'animation d'entite, la dispersion du groupe, la camera
// de coupe, la posture du sujet, le son de zone, et les trois remises a zero des bits d'eveil.
//
// Ce que ces corps NE font pas : decider d'un degat (`Combat/`), choisir une action (`Ai/`),
// deplacer une entite (`Physics/`). Ils posent un etat et rendent la main.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // La famille 0x004734B0..0x00473CB0 - les gestionnaires d'entite « communs » de la table
    // 0x0056adc8, celle que FUN_0050e5e0 lit pour la table d'entites de zone 0x005fa5e0.
    //
    // Ce sont eux que le jeu reclamait : sur les 0x40 sous-types de cette table, 47 pointent sur
    // 0x004734B0 seul (les indices 0, 1 et 19..63), et quatre autres sur ses freres. Les quatre
    // partagent la meme ossature : appeler l'automate commun FUN_004727d0, et s'il ne rend pas 1,
    // faire avancer leur propre machine a etats sur les champs +0x12 (compteur), +0x14 (etat) et
    // +0x16 (evenement).
    //
    // Les etats vont par paires symetriques : 1/2, 0xb/0xc, 0x15/0x16 et 0xc9/0xca sont la meme
    // sequence rejouee dans des contextes differents. Ces corps paraissent repetitifs parce qu'ils
    // le sont dans le binaire.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00472770 @ 0x00472770 - rend l'indice du membre de groupe « actif » : celui de
    /// <c>DAT_005de538</c> s'il est vivant et non masque, sinon le premier des quatre qui l'est.
    ///
    /// <para>
    /// Le masque <c>0x50010000</c> est teste en bloc ; ses bits ne sont pas separement etablis.
    /// La boucle rend <b>4</b> quand aucun emplacement ne convient - un indice hors des quatre,
    /// que l'appelant utilise tel quel. Transcrit sans garde : c'est le comportement d'origine.
    /// </para>
    /// <para>
    /// <b><c>DAT_005de538</c> est lu ici sur UN OCTET</b> — <c>mov cl,[5DE538h]</c> puis
    /// <c>xor eax,eax ; mov al,cl</c>, verifie au desassembleur le 2026-08-24. Le meme global est
    /// lu en double mot puis masque a d'autres sites (d'ou les <c>&amp; 0xff</c> disperses dans
    /// <c>Engine/Input.cs</c>, <c>Engine/Resources.cs</c> et <c>Screens/State02/Destination.cs</c>),
    /// et en double mot nu a deux sites d'<c>Engine/UiWindow.cs</c>. Les trois formes se
    /// transcrivent telles quelles plutot que de s'unifier derriere un champ <c>byte</c> : c'est
    /// le binaire qui varie, pas la reconstruction.
    /// </para>
    /// </summary>
    private static short ActivePartySlot_00472770()
    {
        byte* slots = Inp.s_SlotFlags_005e93c0;
        int current = (byte)Mem.s_Unknown_005de538;

        uint flags = *(uint*)(slots + current * 0x9c);

        // Ghidra rend ce retour par `CONCAT31((int3)(uVar1 >> 8), DAT_005de538) & 0xffff00ff`, ce
        // qui masque ce qu'il fait vraiment : le binaire fait `xor ax,ax ; mov al,cl`, donc AX vaut
        // l'octet de DAT_005de538 et les seize bits hauts d'EAX gardent les drapeaux lus juste
        // avant. L'appelant ne lit qu'un `short` ; le reste de l'expression est du bruit de registre.
        if ((flags & 0x80000000) != 0 && (flags & 0x50010000) == 0) return (short)current;

        short i = 0;
        while ((*(uint*)(slots + i * 0x9c) & 0x80000000) == 0 ||
               (*(uint*)(slots + i * 0x9c) & 0x50010000) != 0)
        {
            i++;
            if (i > 3) return i;
        }

        return i;
    }

    /// <summary>
    /// Ghidra: FUN_00466d70 @ 0x00466d70 - <b>combien d'emplacements du groupe sont actifs</b> :
    /// les quatre fiches de <c>s_SlotFlags_005e93c0</c> dont le bit de signe est pose et dont le
    /// masque <c>0x50000000</c> est vide.
    ///
    /// <para>
    /// Le masque n'est pas celui de <see cref="ActivePartySlot_00472770"/>, qui exclut en plus
    /// <c>0x00010000</c>. Les deux comptes cohabitent dans <see cref="EntityAnimTail_00472E80"/>
    /// et ne donnent pas le meme resultat ; c'est voulu.
    /// </para>
    /// <para>
    /// Rend <b>-1</b> quand aucun emplacement ne convient, ce qui en fait une valeur qu'aucune
    /// comparaison de comptage ne peut satisfaire.
    /// </para>
    /// </summary>
    private static int ActivePartySlotCount_00466D70()
    {
        byte* slots = Inp.s_SlotFlags_005e93c0;
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            uint flags = *(uint*)(slots + i * 0x9c);
            if ((flags & 0x80000000) != 0 && (flags & 0x50000000) == 0) count++;
        }

        return count == 0 ? -1 : count;
    }

    /// <summary>
    /// Ghidra: FUN_00472e80 @ 0x00472e80 - <b>la queue de l'automate commun</b>, prise quand l'etat
    /// n'est aucun de ceux que <see cref="EntityAnimTick_004727D0"/> traite lui-meme. Elle couvre
    /// cinq etats : 200, 0xc9, 0xcd, 0xce, 0xd2 et 0xd3.
    ///
    /// <para>
    /// <b>Ghidra perd sa signature, et le desassemblage dit pourquoi.</b> Sa pseudo-C declare deux
    /// parametres alors que le site d'appel en empile trois, puis reaffecte <c>param_1</c> en
    /// compteur de boucle. Le binaire l'explique en une ligne : la branche de l'etat 0xcd
    /// <b>reutilise les emplacements d'arguments <c>[esp+14h]</c> et <c>[esp+18h]</c> comme
    /// variables locales</b>, ecrasant l'entite et le premier identifiant de son une fois qu'ils
    /// ont ete recopies dans des registres. Le troisieme argument, lui, n'est <b>jamais lu</b> :
    /// <c>[esp+1ch]</c> n'apparait nulle part dans le corps. <c>soundB</c> est donc mort, et
    /// garde ici pour que le site d'appel reste comparable a <c>Blaze.c</c>.
    /// </para>
    /// <para>
    /// <b>Le retour est un <c>short</c>, pas un <c>uint</c>.</b> Tous les
    /// <c>CONCAT22</c>, <c>&amp; 0xffff0000</c> et <c>&lt;&lt; 0x10</c> de Ghidra sont du bruit de
    /// registre : chaque sortie du binaire est soit <c>mov ax,1</c> soit <c>xor ax,ax</c>. Sauf
    /// une — voir l'etat 0xd2 et LB-012.
    /// </para>
    /// <para>
    /// Releve dans <c>Legacy/Raw/Blaze/FUN_00472E80.asm</c>.
    /// </para>
    /// </summary>
    private static short EntityAnimTail_00472E80(uint* entity, short soundA, short soundB)
    {
        byte* e = (byte*)entity;
        short state = *(short*)(e + 0x14);

        // --- 200 : annonce la phrase de +0x4c, puis passe la main a 0xc9 --------------------
        if (state == 200)
        {
            *s_CameraResetArmed_00604b80 = 0;

            uint line = *(uint*)(e + 0x4c);
            if (*(short*)(e + 0x10) < 0xf && line != 0)
            {
                byte* slots = Inp.s_SlotFlags_005e93c0;
                BindMenuLabels(
                    *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + line * 4),
                    // Ghidra: `xor eax,eax ; mov al,[5DE538h]` — l'indice est lu sur un octet ici
                    // aussi, comme dans FUN_00472770.
                    (uint)(nint)(slots + (byte)Mem.s_Unknown_005de538 * 0x9c),
                    s_WorldMapDescriptor_005633b0, 0);                // Ghidra: FUN_00544350
                *(uint*)Mem.Arena(0x005e99c8) = 0x40;
            }

            uint word = *(uint*)e;
            *(short*)(e + 0x16) = -1;
            *(short*)(e + 0x14) = 0xc9;
            *(uint*)e = word | 0x2000;
            *(uint*)(e + 0x5c) = 0;
            return 1;
        }

        // --- 0xc9 : joue le son une seule fois, en passant de -1 a -2 -----------------------
        if (state == 0xc9)
        {
            if (*(short*)(e + 0x16) == -1)
            {
                *(short*)(e + 0x16) = -2;
                PlayZoneSound(entity, soundA);
            }

            return 0;
        }

        // --- 0xcd : le groupe s'est-il assez ecarte ? --------------------------------------
        // Ce bloc ne rend jamais la main lui-meme : il arme au plus l'etat 0xce, que la cascade
        // ci-dessous relit. Ghidra le montre en relisant +0x14 juste apres (LAB_00473154).
        if (state == 0xcd) EvaluatePartyDispersal(e);

        state = *(short*)(e + 0x14);

        // --- 0xce : bascule sur la camera de scenette -------------------------------------
        if (state == 0xce)
        {
            *s_CameraResetArmed_00604b80 = 1;

            short kind = *(short*)(e + 0x98);
            *(short*)(e + 0x14) = 10;
            *(short*)(e + 0x16) = 1;

            if (kind == 1 || kind == 2) ArmCutawayCamera(e);
            return 1;
        }

        // --- 0xd2 : met le sujet en posture 6 et annonce le son d'ouverture ---------------
        if (state == 0xd2)
        {
            uint word = *(uint*)e;
            byte* subject = *(byte**)(e + 0x6c);
            *(uint*)e = word | 0x2000;
            if (subject == null) subject = Res.s_EntityList_005de558;

            *(short*)(subject + 0x18) = 6;
            *(short*)(e + 0x14) = 0xd3;
            *(short*)(e + 0x16) = 0;
            *(short*)(subject + 0x1a) = 0;

            FreezeAllEntities();                                      // Ghidra: FUN_00535300(0)
            *(uint*)e &= 0xfbffffff;

            byte kindBits = GetMainMemPtr()[(subject[8] << 0xd) + 0xf0150];
            if ((kindBits & 7) == 2) Aud.PlaySoundEffect(6, 0x800, 0xa00, 0xa00);
            else Aud.PlaySoundEffect(7, 0x400, 0xa00, 0xa00);

            // LB-012 : ces deux sorties ne posent PAS AX. Le binaire rend ce que FUN_0053cea0 a
            // laisse dans EAX, c'est-a-dire ce que la derniere fonction appelee par elle y a
            // laisse — une valeur indefinie, que l'appelant compare pourtant a 1. Toutes les
            // autres sorties du corps font `mov ax,1` ou `xor ax,ax`. On rend 0, la valeur des
            // sorties voisines ; c'est le seul choix qui ne soit pas un tirage.
            return 0;
        }

        // --- 0xd3 : fait defiler la posture du sujet jusqu'a sa derniere image -------------
        if (state == 0xd3) StepSubjectPosture(e);

        return 0;
    }

    /// <summary>
    /// L'etat 0xcd : trois comptages sur les quatre fiches d'emplacement, une distance a
    /// <c>DAT_005e9a30</c>, et deux conditions concurrentes qui menent toutes deux a 0xce.
    ///
    /// <para>
    /// <b>Les trois comptages n'emploient pas le meme masque</b>, et c'est la tout le sens du
    /// bloc : <c>near</c> compte les membres assez eloignes de l'entite (rayon 500, ou 300 quand
    /// +0x98 vaut 1), <c>active</c> les emplacements vivants au sens de <c>0x50010000</c>, et
    /// <c>flagged</c> ceux d'entre eux qui portent deja le bit <c>0x04000000</c> pose par la
    /// premiere boucle. Confondre les trois effacerait les deux issues.
    /// </para>
    /// <para>
    /// La premiere boucle a un effet de bord : quand +0x98 vaut 2, chaque membre assez loin
    /// recoit le bit <c>0x04000000</c> et voit naitre l'effet d'etat 0x9d sur lui.
    /// </para>
    /// <para>
    /// <see cref="ActivePartySlotCount_00466D70"/> est appelee <b>inconditionnellement</b> : le
    /// binaire l'appelle avant de tester quoi que ce soit, et son resultat est compare a
    /// <c>near</c>. Ghidra le rend par <c>extraout_AX</c>, ce qui masque completement d'ou vient
    /// la valeur.
    /// </para>
    /// </summary>
    private static void EvaluatePartyDispersal(byte* e)
    {
        byte* slots = Inp.s_SlotFlags_005e93c0;
        short kind = *(short*)(e + 0x98);
        short near = 0;

        for (int i = 0; i < 4; i++)
        {
            uint* slot = (uint*)(slots + i * 0x9c);
            if ((*slot & 0x80000000) == 0 || (*slot & 0x50000001) != 0) continue;

            byte* position = (byte*)slot + 0x1c;
            int dx = (*(int*)(e + 0x1c) >> 0xc) - (*(int*)position >> 0xc);
            int dy = (*(int*)(e + 0x20) >> 0xc) - (*(int*)(position + 4) >> 0xc);
            int dz = (*(int*)(e + 0x24) >> 0xc) - (*(int*)(position + 8) >> 0xc);

            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
            if (distance <= 500 && !(distance > 300 && kind == 1)) continue;

            near++;
            if (kind != 2 || (*slot & 0x4000000) != 0) continue;

            *slot |= 0x4000000;
            SpawnStatusEffectEntity_0047AC40(
                (byte*)slot, (int*)position, null, 0x9d, 0, -1);      // Ghidra: FUN_0047ac40
        }

        short active = 0;
        for (int i = 0; i < 4; i++)
        {
            uint flags = *(uint*)(slots + i * 0x9c);
            if ((flags & 0x80000000) != 0 && (flags & 0x50010000) == 0) active++;
        }

        short flagged = 0;
        for (int i = 0; i < 4; i++)
        {
            uint flags = *(uint*)(slots + i * 0x9c);
            if ((flags & 0x80000000) != 0 && (flags & 0x50010000) == 0 &&
                (flags & 0x4000000) != 0) flagged++;
        }

        // La distance au point memorise en 0x005e9a30, la ou la premiere boucle mesurait la
        // distance a chaque membre.
        int* anchor = (int*)Mem.Arena(0x005e9a30);
        int ax = (*(int*)(e + 0x1c) >> 0xc) - (anchor[0] >> 0xc);
        int ay = (*(int*)(e + 0x20) >> 0xc) - (anchor[1] >> 0xc);
        int az = (*(int*)(e + 0x24) >> 0xc) - (anchor[2] >> 0xc);
        short far = (int)Winsys3d.Ordinal_442((uint)(ax * ax + ay * ay + az * az)) > 300
            ? (short)1 : (short)0;

        if (flagged != 0 && active != 0 && flagged == active && kind == 2)
        {
            *s_CameraResetArmed_00604b80 = 1;
            *(short*)(e + 0x14) = 0xce;
        }

        short counted = (short)ActivePartySlotCount_00466D70();       // Ghidra: FUN_00466d70
        if (near == counted && far == 1 && kind != 2)
        {
            *s_CameraResetArmed_00604b80 = 1;
            *(short*)(e + 0x14) = 0xce;
        }

        if (*s_CameraResetArmed_00604b80 == 1) *(short*)(e + 0x14) = 0xce;
    }

    /// <summary>
    /// L'etat 0xce, moitie active : <b>le miroir exact</b> de
    /// <see cref="ResetCameraAndWakeSceneEntities"/>. Celui-la rebranche les trois globales de
    /// camera sur les tables ordinaires ; celui-ci les detourne vers un jeu de valeurs range dans
    /// <c>0x00571980..0x005719a0</c>, apres y avoir recopie ce que les tables portaient.
    ///
    /// <para>
    /// La cible visee est l'entite elle-meme, avec un jeu de cap et d'elevation bruite : cap
    /// <c>rand() % 200 + entite[0x30] + 400</c>, elevation <c>rand() % 200 + entite[0x32] + 300</c>,
    /// distance <c>rand() % 200 + 1000</c>. <c>DAT_00571990</c> est ecrit <b>deux fois</b> — d'abord
    /// avec la distance courante, puis avec le tirage — et la premiere ecriture est morte. C'est ce
    /// que fait le binaire.
    /// </para>
    /// <para>
    /// <b>L'entite est perdue en route</b> : le binaire fait <c>xor esi,esi</c> au milieu du bloc
    /// pour reutiliser le registre en compteur de bits. Rien ne la relit apres, et la branche rend
    /// 1 immediatement — c'est cette destruction qui rend le corps illisible dans <c>Blaze.c</c>,
    /// ou <c>puVar9</c> change de sens en cours de route.
    /// </para>
    /// </summary>
    private static void ArmCutawayCamera(byte* e)
    {
        int* target = Mem.EntityState(0x00571998);
        target[0] = *(int*)(e + 0x1c);
        target[1] = *(int*)(e + 0x20);
        target[2] = *(int*)(e + 0x24);

        short* view = (short*)Mem.EntityStateBytes(0x00571980);
        view[0] = (short)(CrtRand() % 200 + *(short*)(e + 0x30) + 400);   // Ghidra: FUN_00547140
        view[1] = (short)(CrtRand() % 200 + *(short*)(e + 0x32) + 300);
        view[2] = *(short*)(e + 0x34);

        // Ce que les tables portaient est mis de cote dans les trois mots voisins.
        int* live = *(int**)Mem.Entity(0x005eca94);
        int* saved = Mem.EntityState(0x00571988);
        saved[0] = live[0];
        saved[1] = live[1];
        saved[2] = live[2];

        int roll = CrtRand();

        *(int**)Mem.Entity(0x005eca94) = Mem.EntityState(0x00571988);
        *(byte**)Mem.Arena(0x005e9ae0) = Mem.EntityStateBytes(0x00571980);
        *(int**)Mem.Entity(0x005edebc) = Mem.EntityState(0x00571998);

        s_CameraResetMask_00604b84[0] = 0;
        s_CameraResetMask_00604b84[1] = 0;
        s_CameraResetMask_00604b84[2] = 0;
        s_CameraResetMask_00604b84[3] = 0;

        // La seconde ecriture de DAT_00571990 : elle ecrase celle de `saved[2]` ci-dessus.
        *Mem.EntityState(0x00571990) = roll % 200 + 1000;

        Saves.s_SavedViewMode_00604b7c = s_Unknown_005de54c;
        s_Unknown_005de54c = 3;

        // Le masque que ResetCameraAndWakeSceneEntities relira : un bit par entree de scene deja
        // reveillee, l'indice de bit tournant sur huit.
        byte* scene = Mem.Entity(0x005ecaa0);
        short bit = 0;
        for (int i = 0; i < 0x20; i++)
        {
            if ((*(uint*)(scene + i * 0x9c) & 0x4000000) != 0)
            {
                s_CameraResetMask_00604b84[i >> 3] |= (byte)(1 << (bit & 0x1f));
            }

            bit++;
            if (bit == 8) bit = 0;
        }

        FreezeAllEntities();                                          // Ghidra: FUN_00535300(0)

        // Et les entites de zone dont le sous-type est 1..7 perdent leur bit d'eveil.
        byte* zone = Mem.Entity(0x005fa5e0);
        for (int i = 0; i < 0x80; i++)
        {
            byte* record = zone + i * 0x9c;
            if (record[5] >= 1 && record[5] <= 7) *(uint*)record &= 0xfbffffff;
        }
    }

    /// <summary>
    /// L'etat 0xd3 : le sujet tenu en +0x6c avance d'une image de posture par pas, jusqu'a une
    /// borne qui depend de ce qu'il est.
    ///
    /// <para>
    /// L'octet de classe est lu dans le bloc de 0x2000 octets du sujet, a
    /// <c>(sujet[8] + 0x78) * 0x2000 + 0x150</c>. Sa valeur <c>&amp; 7</c> vaut 2 pour la famille
    /// qui a ses propres bornes en dur — 5 images, ou 11 si le bit 8 est pose ; toute autre valeur
    /// prend la borne dans la fiche d'animation, <c>0x005e9680[sujet[4]]</c>.
    /// </para>
    /// <para>
    /// <b>Les deux liberations ne sont pas identiques</b> : celle de la famille 2 remet aussi
    /// +0x16 a zero, celle de l'autre non. Ghidra le montre par un <c>goto LAB_00473454</c> d'un
    /// cote et une chute de l'autre ; c'est le genre d'asymetrie qu'une factorisation effacerait.
    /// </para>
    /// </summary>
    private static void StepSubjectPosture(byte* e)
    {
        byte* subject = *(byte**)(e + 0x6c);
        if (subject == null) subject = Res.s_EntityList_005de558;

        *(short*)(subject + 0x18) = 6;

        // Le binaire charge la fiche d'animation AVANT de tester la classe, donc sans savoir s'il
        // s'en servira. Transcrit dans le meme ordre : une fiche invalide fauterait au meme
        // endroit qu'en 1998.
        byte* animation = *(byte**)(((byte**)Mem.Arena(0x005e9680))[subject[4]] + 0x18);

        byte kindBits = GetMainMemPtr()[((subject[8] + 0x78) << 0xd) + 0x150];

        if ((kindBits & 7) == 2)
        {
            short frame = *(short*)(subject + 0x1a);

            if ((kindBits & 8) == 0)
            {
                if (frame + 1 < 5)
                {
                    *(short*)(subject + 0x1a) = (short)(frame + 1);
                    return;
                }

                if (*(short*)(e + 0x16) != 0)
                {
                    ReleaseSubjectPosture(e, subject, clearEvent: true);
                    return;
                }

                *(short*)(e + 0x16) = 1;
                *(short*)(subject + 0x1a) = 2;
            }
            else
            {
                if (frame + 1 < 0xb)
                {
                    *(short*)(subject + 0x1a) = (short)(frame + 1);
                    return;
                }

                if (*(short*)(e + 0x16) != 0)
                {
                    ReleaseSubjectPosture(e, subject, clearEvent: true);
                    return;
                }

                *(short*)(e + 0x16) = 1;
                *(short*)(subject + 0x1a) = 3;
            }

            Aud.PlaySoundEffect(6, 0x800, 0xa00, 0xa00);              // Ghidra: FUN_0053cea0
            return;
        }

        int bound = *(ushort*)(animation + 0x16);
        if (*(short*)(subject + 0x1a) + 1 < bound)
        {
            *(short*)(subject + 0x1a) += 1;
            return;
        }

        ReleaseSubjectPosture(e, subject, clearEvent: false);
    }

    /// <summary>
    /// La fin de l'etat 0xd3 : le sujet reprend la main, les entites degelent, l'automate revient
    /// en 0x14.
    /// </summary>
    /// <param name="clearEvent">Vrai sur la sortie de la famille 2 seulement — c'est la seule des
    /// deux qui remet +0x16 a zero (Ghidra: <c>LAB_00473454</c> contre la chute en
    /// <c>LAB_00473490</c>).</param>
    private static void ReleaseSubjectPosture(byte* e, byte* subject, bool clearEvent)
    {
        *(uint*)subject |= 0x2000000;
        UnfreezeAllEntities();                                        // Ghidra: FUN_00535400
        *(short*)(e + 0x14) = 0x14;
        if (clearEvent) *(short*)(e + 0x16) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_005037e0 @ 0x005037e0 - rebranche les trois globales de camera selon le mode de
    /// vue courant. <c>FUN_005037b0</c>, son unique appelant, n'est qu'une enveloppe sans
    /// argument ; Ghidra lui prete deux arguments qu'elle n'a pas, et son retour est jete.
    ///
    /// <para>
    /// Les modes 2 et 3 ne sont pas des modes : ils sont rabattus sur celui que la sauvegarde
    /// porte (<c>DAT_00604b7c</c>) avant tout le reste. Le mode 0 est la vue de monde, le mode 1
    /// une vue qui emprunte deux de ses trois blocs au bloc de vue de 0x006067c0.
    /// </para>
    /// <para>
    /// <b>Aucun mode par defaut</b> : si la valeur n'est ni 0 ni 1 apres le rabattement, les trois
    /// globales gardent ce qu'elles avaient. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static int CameraModeApply_005037E0(int counter)
    {
        if (s_Unknown_005de54c == 2 || s_Unknown_005de54c == 3)
        {
            s_Unknown_005de54c = Saves.s_SavedViewMode_00604b7c;
        }

        if (s_Unknown_005de54c == 0)
        {
            *(byte**)Mem.Arena(0x005e9ae0) = s_WorldMapCamera_00564650;
            *(int**)Mem.Entity(0x005edebc) = Mem.Camera(0x005de068);
            *(int**)Mem.Entity(0x005eca94) = Mem.Camera(0x005de078);
        }
        else if (s_Unknown_005de54c == 1)
        {
            *(int**)Mem.Arena(0x005e9ae0) = Mem.View(0x006067e8);
            *(int**)Mem.Entity(0x005edebc) = Mem.Camera(0x005de068);
            *(int**)Mem.Entity(0x005eca94) = Mem.View(0x006067d0);
        }

        return counter + 1;
    }

    // Ghidra: DAT_00604b80 et &DAT_00604b84 - le drapeau qui arme le rebranchement des tables de
    // camera, et les quatre octets de masque que la boucle de reveil lit par groupes de huit. Les
    // deux sont dans la queue NON INITIALISEE de .data (verifie dans Legacy/Binaries/Blaze.exe),
    // donc nuls au chargement : la branche qui les lit est morte tant que rien ne les pose.
    // Declares ici plutot qu'en memoire partagee parce qu'un seul appelant les touche.
    private static readonly byte* s_CameraResetArmed_00604b80 = (byte*)NativeMemory.AllocZeroed(4);
    private static readonly byte* s_CameraResetMask_00604b84 = (byte*)NativeMemory.AllocZeroed(4);

    /// <summary>
    /// Ghidra: FUN_004727d0 @ 0x004727d0 - <b>l'automate commun</b> des gestionnaires d'entite de
    /// la table 0x0056adc8. Il fait vieillir l'entite d'un pas, decide si elle doit encore agir, et
    /// rend <b>1</b> quand l'appelant ne doit rien faire de plus ce pas-ci.
    ///
    /// <para>
    /// Les deux derniers arguments sont des <b>indices de son</b> que Ghidra a laisses en
    /// <c>undefined4</c> : chaque frere les passe differents (0/1, 2/3, 6/7), et ils ne servent
    /// qu'a indexer la table de sons de la zone avant <c>TryPlaySoundNearEntity</c>.
    /// </para>
    /// <para>
    /// <b>Le champ +0x9a est un compte a rebours d'attente</b> : tant qu'il est positif la fonction
    /// rend 1 sans rien faire avancer. Il est decremente puis borne a zero, donc une valeur
    /// negative est ramenee a zero sans que le pas soit consomme.
    /// </para>
    /// </summary>
    private static short EntityAnimTick_004727D0(uint* entity, short soundA, short soundB)
    {
        byte* e = (byte*)entity;
        byte* slots = Inp.s_SlotFlags_005e93c0;

        short age = *(short*)(e + 0x10);
        if (age == 0)
        {
            short packed = *(short*)(e + 0x30);
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x30) = 0;
            *(uint*)(e + 0x44) = (uint)(packed & 0xff);
            *(uint*)(e + 0x4c) = (uint)(packed >> 8);
            *(uint*)(e + 0x48) = *(short*)(e + 0x14) == 200 ? 1u : 0u;

            short evt = *(short*)(e + 0x16);
            if (evt == 1) { *(short*)(e + 0x16) = 0; *(short*)(e + 0x98) = 1; }
            else if (evt == 2) { *(short*)(e + 0x16) = 0; *(short*)(e + 0x98) = 2; }
            else *(short*)(e + 0x98) = 0;
        }

        // Une entite qui a passe l'age 10 et n'attend aucun evenement rend la main au premier
        // membre du groupe que son masque +0x9 designe et qui porte 0x8000000 a +0x90.
        if (*(short*)(e + 0x16) == 0 && age > 10)
        {
            for (short slot = 0; slot < 4; slot++)
            {
                if ((e[9] & (1 << (slot & 0x1f))) != 0 &&
                    (*(uint*)(slots + slot * 0x9c + 0x90) & 0x8000000) != 0)
                {
                    *(uint*)(slots + slot * 0x9c) |= 0x20000000;
                    *(uint*)(slots + slot * 0x9c + 0x90) |= 0x8000;
                    return 0;
                }
            }
        }

        *(short*)(e + 0x9a) -= 1;
        short wait = *(short*)(e + 0x9a);
        if (wait < 0) *(short*)(e + 0x9a) = 0;
        if (wait > 0) return 1;

        age++;
        *(short*)(e + 0x10) = age;
        if (age > 10 && *(short*)(e + 0x14) == 200) *(short*)(e + 0x14) = 99;
        if (age > 200) *(short*)(e + 0x10) = 0x10;

        // Ghidra: `if (sVar3 != 99) { LAB_004729b4: ... }`. Le bloc de contact ci-dessous n'est donc
        // atteint QUE dans l'etat 99 ; tout autre etat part directement dans la suite commune, que
        // Ghidra atteint par un `goto LAB_004729b4`. Reconstruit en appel : la suite ne relit aucun
        // etat local d'ici.
        if (*(short*)(e + 0x14) != 99) return EntityAnimCommon(e, entity, soundA, soundB, slots);

        bool contactEarly =
            *(short*)(e + 0x16) == 0 && (*(uint*)e & 0x40) != 0 && *(uint*)(e + 0x48) == 1 &&
            (**(uint**)(e + 0x6c) & 0x10000) != 0;
        bool contactLate =
            *(short*)(e + 0x16) == 0 && (*(uint*)e & 0x40) != 0 &&
            (sbyte)e[0xd] == -1 && (**(uint**)(e + 0x6c) & 0x10000) == 0;

        if (!contactEarly && !contactLate) return EntityAnimCommon(e, entity, soundA, soundB, slots);

        Aud.PlaySoundEffect(0x1d, 0x300, 0xc00, 0xc00);   // Ghidra: FUN_0053cea0
        BindMenuLabels(
            *(byte**)(*(int*)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x488),
            *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
        *(uint*)Mem.Arena(0x005e99c8) = 0x40;
        *(short*)(e + 0x9a) = 5;
        return 1;
    }

    /// <summary>
    /// La suite commune de <see cref="EntityAnimTick_004727D0"/> - ce que Ghidra atteint par
    /// <c>LAB_004729b4</c> depuis deux endroits. Extraite parce que c'est une unite reellement
    /// independante : elle ne relit aucun etat local de l'appelant.
    /// </summary>
    private static short EntityAnimCommon(byte* e, uint* entity, short soundA, short soundB, byte* slots)
    {
        if ((e[9] & 0xf) != 0) *(uint*)e &= 0xbfffffff;

        uint word = *(uint*)e;
        if ((word & 0x2000) != 0) e[9] &= 0xf0;

        short state = *(short*)(e + 0x14);
        if (state == 99 || ((word & 0x40000000) != 0 && (state == 0 || state == 200))) return 1;

        if (state == 0)
        {
            if (*(short*)(e + 0x10) < 0xf && *(uint*)(e + 0x4c) != 0)
            {
                BindMenuLabels(
                    *(byte**)(*(int*)(s_Unknown_005e97cc + 0x70) + *(uint*)(e + 0x4c) * 4),
                    // Ghidra: `xor eax,eax ; mov al,[5DE538h]` — troisieme site de ce fichier a
                    // lire l'indice sur un octet. Voir ActivePartySlot_00472770.
                    (uint)(nint)(slots + (byte)Mem.s_Unknown_005de538 * 0x9c),
                    s_WorldMapDescriptor_005633b0, 0);
                *(uint*)Mem.Arena(0x005e99c8) = 0x40;
            }

            *(short*)(e + 0x16) = -1;
            *(short*)(e + 0x14) = 1;
            *(uint*)e |= 0x2000;
            return 1;
        }

        if (state == 1)
        {
            if (*(short*)(e + 0x16) == -1)
            {
                *(short*)(e + 0x16) = -2;
                PlayZoneSound(entity, soundA);
                return 0;
            }

            return 0;
        }

        if (state == 5)
        {
            *(short*)(e + 0x14) = 99;
            *(uint*)e = word | 0x40000000;
            *(short*)(e + 0x16) = 1;
            return 1;
        }

        if (state == 10 && *(short*)(e + 0x16) == 0) *(short*)(e + 0x14) = 99;

        if (*(short*)(e + 0x14) == 10 &&
            (*(short*)(e + 0x16) == 1 || *(short*)(e + 0x16) == 2))
        {
            PlayZoneSound(entity, soundB);
            *(short*)(e + 0x14) = 0xb;
            *(uint*)e &= 0xffffdfff;
            *(short*)(e + 0x16) = -1;
        }

        state = *(short*)(e + 0x14);
        if (state == 0xb)
        {
            if (*(short*)(e + 0x16) == -1)
            {
                *(short*)(e + 0x16) = -2;
                PlayZoneSound(entity, soundB);
                return 0;
            }

            return 0;
        }

        if (state == 0xf)
        {
            ushort pending = *(ushort*)(e + 0x98);
            *(short*)(e + 0x14) = 99;
            word = *(uint*)e;
            *(short*)(e + 0x16) = 0;
            *(uint*)e = word | 0x40000000;

            if (pending != 1 && pending != 2)
            {
                if (*(uint*)(e + 0x44) == 0) return 1;

                short slot = ActivePartySlot_00472770();
                BindMenuLabels(
                    *(byte**)(*(int*)(s_Unknown_005e97cc + 0x70) + *(uint*)(e + 0x44) * 4),
                    (uint)(nint)(slots + slot * 0x9c), s_WorldMapDescriptor_005633b0, 0);
                *(uint*)Mem.Arena(0x005e99c8) = 0x40;
                return 1;
            }

            *(ushort*)(e + 0x98) = (ushort)(pending | 0xf00);
            *(short*)(e + 0x14) = 0x10;
            *(short*)(e + 0x16) = -1;
            *(uint*)e = word & 0xbfffffff;
            return 1;
        }

        if (state == 0x10)
        {
            *(short*)(e + 0x98) -= 0x100;
            if (*(short*)(e + 0x98) == 1 || *(short*)(e + 0x98) == 2)
            {
                if (*s_CameraResetArmed_00604b80 == 1) ResetCameraAndWakeSceneEntities();

                *(short*)(e + 0x14) = 99;
                *(short*)(e + 0x16) = 0;
                *(uint*)e |= 0x40000000;

                if (*(uint*)(e + 0x44) != 0)
                {
                    short slot = ActivePartySlot_00472770();
                    BindMenuLabels(
                        *(byte**)(*(int*)(s_Unknown_005e97cc + 0x70) + *(uint*)(e + 0x44) * 4),
                        (uint)(nint)(slots + slot * 0x9c), s_WorldMapDescriptor_005633b0, 0);
                    *(uint*)Mem.Arena(0x005e99c8) = 0x40;
                }
            }
        }

        if (*(short*)(e + 0x14) == 0x14 &&
            (*(short*)(e + 0x16) == 1 || *(short*)(e + 0x16) == 2))
        {
            *(short*)(e + 0x14) = 99;
        }

        if (*(short*)(e + 0x14) == 0x14 &&
            (*(short*)(e + 0x16) == -1 || *(short*)(e + 0x16) == -2))
        {
            *(short*)(e + 0x16) = 0;
        }

        if (*(short*)(e + 0x14) == 0x14 && *(short*)(e + 0x16) == 0)
        {
            PlayZoneSound(entity, soundA);
            *(short*)(e + 0x14) = 0x15;
            *(short*)(e + 0x16) = -1;
            *(uint*)e |= 0x2000;
        }

        if (*(short*)(e + 0x14) != 0x15)
        {
            if (*(short*)(e + 0x14) != 0x19) return EntityAnimTail_00472E80(entity, soundA, soundB);

            *(short*)(e + 0x14) = 99;
            *(short*)(e + 0x16) = 1;
            *(uint*)e |= 0x40000000;
            return 1;
        }

        if (*(short*)(e + 0x16) == -1)
        {
            *(short*)(e + 0x16) = -2;
            PlayZoneSound(entity, soundA);
        }

        return 0;
    }

    /// <summary>
    /// Le bloc arme par <c>DAT_00604b80</c> dans l'etat 0x10 : rebrancher les trois globales de
    /// camera, degeler les entites, puis reveiller celles des 0x20 entrees de scene que le masque
    /// de <c>DAT_00604b84</c> designe. Extrait de <see cref="EntityAnimCommon"/> pour la seule
    /// raison qu'il est mort tant que le drapeau vaut zero : le garder inline y noyait la machine
    /// a etats.
    /// </summary>
    private static void ResetCameraAndWakeSceneEntities()
    {
        *(int**)Mem.Entity(0x005eca94) = Mem.Camera(0x005de078);
        *(byte**)Mem.Arena(0x005e9ae0) = s_WorldMapCamera_00564650;
        *(int**)Mem.Entity(0x005edebc) = Mem.Camera(0x005de068);
        CameraModeApply_005037E0(0);         // Ghidra: FUN_005037b0 -> FUN_005037e0
        UnfreezeAllEntities();               // Ghidra: FUN_00535400

        // Ghidra: les 0x20 entrees de DAT_005ecaa0, reveillees selon un masque lu par groupes de
        // huit - l'indice de bit tourne sur 8, l'indice d'octet avance toutes les huit entrees.
        byte* scene = Mem.Entity(0x005ecaa0);
        short bit = 0;
        for (int i = 0; i < 0x20; i++)
        {
            if ((s_CameraResetMask_00604b84[i >> 3] & (byte)(1 << (bit & 0x1f))) != 0)
            {
                *(uint*)(scene + i * 0x9c) |= 0x4000000;
            }

            bit++;
            if (bit == 8) bit = 0;
        }
    }

    /// <summary>
    /// Le motif d'appel sonore que l'automate repete six fois : l'identifiant est lu dans la table
    /// de sons de la zone (<c>DAT_005e97cc + 0xa8</c>), indexee par l'argument, puis joue a la
    /// position de l'entite avec une hauteur de 0x400 et un gain de 0x1000.
    /// </summary>
    private static void PlayZoneSound(uint* entity, short index)
    {
        byte* table = *(byte**)(*(byte***)(s_Unknown_005e97cc + 0xa8));
        TryPlaySoundNearEntity(entity, (short)table[index], 0x400, 0x1000, 0, 0);
    }

    /// <summary>
    /// Ghidra: FUN_004734b0 @ 0x004734b0 - le gestionnaire par defaut de la table 0x0056adc8 :
    /// <b>47 des 64 sous-types y pointent</b>. Un <b>saut sur place</b> : l'entite monte pendant
    /// treize pas d'amplitude croissante puis redescend en autant, l'amplitude etant le compteur
    /// +0x12 lui-meme. La hauteur va dans +0x20, la distance cumulee dans +0x5c.
    ///
    /// <para>
    /// Quatre groupes, dont trois sont le meme saut rejoue : 1/2/3 sort en 5 avec l'evenement 1,
    /// 0xc9/0xca/0xcb sort en 0xcd avec l'evenement 2, 0x15/0x16/0x17 sort en 0x19 avec
    /// l'evenement 1.
    /// </para>
    /// <para>
    /// <b>Le groupe 0xb/0xc est different</b> : c'est une <b>chute</b>, pas un saut. La hauteur
    /// descend (<c>step * -0x1000</c>) et la sortie ne se fait pas sur l'amplitude mais sur la
    /// distance cumulee passant 0xc0 — apres quoi l'entite est <b>replacee exactement</b> a la
    /// hauteur de la butee (<c>+= (distance - 0xc1) * 0x1000</c>). Une butee, pas un arret.
    /// </para>
    /// </summary>
    private static void EntityHandler_004734B0(uint* entity)
    {
        byte* e = (byte*)entity;
        if (EntityAnimTick_004727D0(entity, 0, 1) == 1) return;

        if (*(short*)(e + 0x14) == 1) StartHop(e, 2);
        if (*(short*)(e + 0x14) == 2 && StepHopUp(e)) *(short*)(e + 0x14) = 3;
        if (*(short*)(e + 0x14) == 3 && StepHopDown(e)) { HopDone(e, 5, 1); return; }

        if (*(short*)(e + 0x14) == 0xc9) StartHop(e, 0xca);
        if (*(short*)(e + 0x14) == 0xca && StepHopUp(e)) *(short*)(e + 0x14) = 0xcb;
        if (*(short*)(e + 0x14) == 0xcb && StepHopDown(e)) { HopDone(e, 0xcd, 2); return; }

        if (*(short*)(e + 0x14) == 0xb) StartHop(e, 0xc);
        if (*(short*)(e + 0x14) == 0xc)
        {
            short step = *(short*)(e + 0x12);
            *(int*)(e + 0x20) += step * -0x1000;
            int travelled = *(int*)(e + 0x5c) + step;
            *(short*)(e + 0x12) = (short)(step + 1);
            *(int*)(e + 0x5c) = travelled;

            if (travelled > 0xc0)
            {
                *(short*)(e + 0x14) = 0xf;
                *(int*)(e + 0x20) += (travelled - 0xc1) * 0x1000;
                *(short*)(e + 0x16) = 0;
                return;
            }
        }

        if (*(short*)(e + 0x14) == 0x15) StartHop(e, 0x16);
        if (*(short*)(e + 0x14) == 0x16 && StepHopUp(e)) *(short*)(e + 0x14) = 0x17;
        if (*(short*)(e + 0x14) == 0x17 && StepHopDown(e)) HopDone(e, 0x19, 1);
    }

    /// <summary>L'amorce d'un saut : amplitude 3, distance a zero, etat suivant.</summary>
    private static void StartHop(byte* e, short nextState)
    {
        *(short*)(e + 0x12) = 3;
        *(uint*)(e + 0x5c) = 0;
        *(short*)(e + 0x14) = nextState;
    }

    /// <summary>La montee : la hauteur gagne l'amplitude courante, qui croit. Rend vrai quand
    /// l'amplitude depasse 0xd, c'est-a-dire au sommet.</summary>
    private static bool StepHopUp(byte* e)
    {
        short step = *(short*)(e + 0x12);
        *(int*)(e + 0x20) += step * 0x1000;
        *(int*)(e + 0x5c) += step;
        *(short*)(e + 0x12) = (short)(step + 1);
        return step + 1 > 0xd;
    }

    /// <summary>La descente. <b>La hauteur est ajoutee, pas retranchee</b> — c'est bien ce que fait
    /// le binaire : les deux moities du saut montent, seule l'amplitude decroit. Rend vrai quand
    /// l'amplitude retombe sous 1.</summary>
    private static bool StepHopDown(byte* e)
    {
        short step = *(short*)(e + 0x12);
        *(int*)(e + 0x20) += step * 0x1000;
        *(int*)(e + 0x5c) += step;
        *(short*)(e + 0x12) = (short)(step - 1);
        return step - 1 < 1;
    }

    /// <summary>La sortie d'un groupe de saut : etat suivant et evenement pose.</summary>
    private static void HopDone(byte* e, short nextState, short evt)
    {
        *(short*)(e + 0x14) = nextState;
        *(short*)(e + 0x16) = evt;
    }

    /// <summary>
    /// Ghidra: FUN_004738a0 @ 0x004738a0 - le sous-type 3. Une <b>chute avec rebond</b> : +0x17
    /// porte une vitesse qui croit d'un huitieme pendant douze pas puis decroit d'autant, et
    /// l'entite recule d'autant sur +0x32.
    ///
    /// <para>La branche 0xb/0xc est differente des trois autres : la vitesse s'y accumule par
    /// paliers croissants jusqu'a depasser 0x372, puis l'entite est <b>replacee exactement</b> a
    /// 0x373 - une butee, pas un arret.</para>
    /// </summary>
    private static void EntityHandler_004738A0(uint* entity)
    {
        byte* e = (byte*)entity;
        if (EntityAnimTick_004727D0(entity, 2, 3) == 1) return;

        if (*(short*)(e + 0x14) == 1)
        {
            *(short*)(e + 0x12) = 0;
            *(uint*)(e + 0x5c) = 0xf;
            *(short*)(e + 0x14) = 2;
        }

        if (*(short*)(e + 0x14) == 2)
        {
            if (StepDampedSpeed(e) == 0x28)
            {
                *(short*)(e + 0x16) = 1;
                *(short*)(e + 0x14) = 5;
                *(uint*)e |= 0x40000000;
                return;
            }

            *(short*)(e + 0x32) -= (short)*(uint*)(e + 0x5c);
        }

        if (*(short*)(e + 0x14) == 0xc9)
        {
            *(short*)(e + 0x12) = 0;
            *(uint*)(e + 0x5c) = 0xf;
            *(short*)(e + 0x14) = 0xca;
        }

        if (*(short*)(e + 0x14) == 0xca)
        {
            if (StepDampedSpeed(e) == 0x28)
            {
                *(short*)(e + 0x14) = 0xcd;
                *(short*)(e + 0x16) = 2;
                *(uint*)e |= 0x40000000;
            }

            *(short*)(e + 0x32) -= (short)*(uint*)(e + 0x5c);
        }

        if (*(short*)(e + 0x14) == 0xb)
        {
            *(short*)(e + 0x12) = 5;
            *(uint*)(e + 0x5c) = 0;
            *(short*)(e + 0x14) = 0xc;
        }

        if (*(short*)(e + 0x14) == 0xc)
        {
            short step = *(short*)(e + 0x12);
            uint speed = *(uint*)(e + 0x5c);
            *(short*)(e + 0x32) += step;
            *(short*)(e + 0x12) = (short)(step + 1);
            *(uint*)(e + 0x5c) = (uint)((int)speed + step);

            if ((int)(speed + (uint)step) > 0x372)
            {
                *(short*)(e + 0x14) = 0xf;
                *(short*)(e + 0x16) = 0;
                *(short*)(e + 0x32) += (short)(0x373 - (short)*(uint*)(e + 0x5c));
                return;
            }
        }

        if (*(short*)(e + 0x14) == 0x15)
        {
            *(short*)(e + 0x12) = 0;
            *(uint*)(e + 0x5c) = 0xf;
            *(short*)(e + 0x14) = 0x16;
        }

        if (*(short*)(e + 0x14) == 0x16)
        {
            if (StepDampedSpeed(e) == 0x28)
            {
                *(short*)(e + 0x16) = 1;
                *(short*)(e + 0x14) = 0x19;
                *(uint*)e |= 0x40000000;
                return;
            }

            *(short*)(e + 0x32) -= (short)*(uint*)(e + 0x5c);
        }
    }

    /// <summary>
    /// Les douze pas croissants puis douze decroissants que <see cref="EntityHandler_004738A0"/>
    /// repete a l'identique dans trois de ses quatre branches : la vitesse de +0x5c gagne un
    /// huitieme d'elle-meme tant que le compteur est sous 0xd, puis en perd autant. Rend le
    /// compteur apres incrementation, que les trois branches comparent a 0x28.
    /// </summary>
    private static short StepDampedSpeed(byte* e)
    {
        int speed = (int)*(uint*)(e + 0x5c);
        *(uint*)(e + 0x5c) = (uint)(*(short*)(e + 0x12) < 0xd ? (speed >> 3) + speed : speed - (speed >> 3));

        short next = (short)(*(short*)(e + 0x12) + 1);
        *(short*)(e + 0x12) = next;
        return next;
    }

    /// <summary>
    /// Ghidra: FUN_00473a90 @ 0x00473a90 - le sous-type 4. Un <b>glissement le long du cap</b> :
    /// pendant 0x17 pas, l'entite avance sur x (+0x1c) et z (+0x24) d'une fraction de
    /// <c>sin</c>/<c>cos</c> du cap (+0x32), l'amplitude montant sur les seize premiers pas puis
    /// redescendant.
    ///
    /// <para>Sa sœur <see cref="EntityHandler_00473CB0"/> est le meme corps avec les signes
    /// inverses ; les deux ne sont pas fusionnees parce que leurs quatre branches ne s'accordent
    /// pas sur les memes signes, et une fusion aurait demande un parametre par branche.</para>
    /// </summary>
    private static void EntityHandler_00473A90(uint* entity)
    {
        byte* e = (byte*)entity;
        if (EntityAnimTick_004727D0(entity, 6, 7) == 1) return;

        if (*(short*)(e + 0x14) == 1) StartSlide(e, 2);
        if (*(short*)(e + 0x14) == 2 && StepSlide(e, forward: true)) { SlideDone(e, 5, 1); return; }

        if (*(short*)(e + 0x14) == 0xb) StartSlide(e, 0xc);
        if (*(short*)(e + 0x14) == 0xc && StepSlide(e, forward: false)) { SlideDone(e, 0xf, 1); return; }

        if (*(short*)(e + 0x14) == 0x15) StartSlide(e, 0x16);
        if (*(short*)(e + 0x14) == 0x16 && StepSlide(e, forward: true)) { SlideDone(e, 0x19, 1); return; }

        if (*(short*)(e + 0x14) == 0xc9) StartSlide(e, 0xca);
        if (*(short*)(e + 0x14) == 0xca && StepSlide(e, forward: true)) SlideDone(e, 0xcd, 1);
    }

    /// <summary>
    /// Ghidra: FUN_00473cb0 @ 0x00473cb0 - le sous-type 5, le miroir du precedent. Seuls les
    /// signes des quatre branches changent : reculer, avancer, reculer, reculer.
    /// </summary>
    private static void EntityHandler_00473CB0(uint* entity)
    {
        byte* e = (byte*)entity;
        if (EntityAnimTick_004727D0(entity, 6, 7) == 1) return;

        if (*(short*)(e + 0x14) == 1) StartSlide(e, 2);
        if (*(short*)(e + 0x14) == 2 && StepSlide(e, forward: false)) { SlideDone(e, 5, 1); return; }

        if (*(short*)(e + 0x14) == 0xb) StartSlide(e, 0xc);
        if (*(short*)(e + 0x14) == 0xc && StepSlide(e, forward: true)) { SlideDone(e, 0xf, 1); return; }

        if (*(short*)(e + 0x14) == 0x15) StartSlide(e, 0x16);
        if (*(short*)(e + 0x14) == 0x16 && StepSlide(e, forward: false)) { SlideDone(e, 0x19, 1); return; }

        if (*(short*)(e + 0x14) == 0xc9) StartSlide(e, 0xca);
        if (*(short*)(e + 0x14) == 0xca && StepSlide(e, forward: false)) SlideDone(e, 0xcd, 1);
    }

    /// <summary>L'amorce d'une branche de glissement : compteur et distance a zero, etat suivant.</summary>
    private static void StartSlide(byte* e, short nextState)
    {
        *(short*)(e + 0x12) = 0;
        *(short*)(e + 0x14) = nextState;
        *(uint*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Un pas de glissement. L'amplitude de +0x12 croit tant que la distance parcourue est sous
    /// 0x10 et decroit ensuite, ce qui donne un profil en cloche sur les 0x17 pas de la branche.
    /// Rend vrai au dernier pas.
    /// </summary>
    private static bool StepSlide(byte* e, bool forward)
    {
        if ((int)*(uint*)(e + 0x5c) < 0x10) *(short*)(e + 0x12) += 1;
        else *(short*)(e + 0x12) -= 1;

        short amplitude = *(short*)(e + 0x12);
        short heading = *(short*)(e + 0x32);

        int sin = (int)Winsys3d.GetSinTableEntry(heading);   // Ghidra: Ordinal_451
        int cos = (int)Winsys3d.GetCosTableEntry(heading);   // Ghidra: Ordinal_450

        if (forward)
        {
            *(int*)(e + 0x24) += sin * amplitude;
            *(int*)(e + 0x1c) += cos * amplitude;
        }
        else
        {
            *(int*)(e + 0x24) -= sin * amplitude;
            *(int*)(e + 0x1c) -= cos * amplitude;
        }

        uint travelled = *(uint*)(e + 0x5c) + 1;
        *(uint*)(e + 0x5c) = travelled;
        return travelled == 0x17;
    }

    /// <summary>La sortie d'une branche de glissement : etat suivant et evenement pose.</summary>
    private static void SlideDone(byte* e, short nextState, short evt)
    {
        *(short*)(e + 0x14) = nextState;
        *(short*)(e + 0x16) = evt;
    }

    /// <summary>
    /// Ghidra: FUN_00460fc0 @ 0x00460fc0 - remet a zero les trois dwords de +0x50 a +0x58 de
    /// l'entite. Onze lignes, aucune dependance. Son unique appelant reconstruit est
    /// <see cref="EntityHandler_00450C60"/>, qui la lance a chaque pas avant toute chose.
    /// </summary>
    private static void ClearEntityVelocity_00460FC0(byte* e)
    {
        *(uint*)(e + 0x50) = 0;
        *(uint*)(e + 0x54) = 0;
        *(uint*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00450c60 @ 0x00450c60 - le sous-type <b>12</b> de la table 0x0056adc8. Une
    /// entite qui <b>palpite</b> : les trois facteurs d'echelle (+0x28, +0x2a, +0x2c) oscillent
    /// autour de l'unite (0x1000) au rythme d'une phase qui avance de 0x100 par pas et reboucle a
    /// 0x1000.
    ///
    /// <para>
    /// <b>L'axe de +0x2a bat deux fois plus fort</b> que les deux autres : <c>sin &gt;&gt; 4</c>
    /// contre <c>sin &gt;&gt; 5</c>. Ce n'est pas une coquille de transcription, les deux appels a
    /// la table des sinus sont distincts dans le binaire et leurs decalages different.
    /// </para>
    /// <para>
    /// Le son d'ambiance part une fois tous les 0x3c pas, le compteur +0x98 servant de minuterie
    /// circulaire. Il est joue <b>avant</b> l'incrementation, donc au premier pas de chaque cycle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00450C60(uint* entity)
    {
        byte* e = (byte*)entity;
        ClearEntityVelocity_00460FC0(e);       // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        if (*(short*)(e + 0x98) == 0)
        {
            byte* sounds = *(byte**)(*(byte***)(s_Unknown_005e97cc + 0xa8));
            TryPlaySoundNearEntity(entity, (short)sounds[0x11], 0x300, 0x800, 0, 0);   // Ghidra: FUN_00468080
        }

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) > 0x3b) *(short*)(e + 0x98) = 0;

        *(short*)(e + 0x12) += 0x100;

        short phase = *(short*)(e + 0x12);
        *(short*)(e + 0x2a) = (short)(((int)Winsys3d.GetSinTableEntry(phase) >> 4) + 0x1000);

        short flat = (short)(((int)Winsys3d.GetSinTableEntry(phase) >> 5) + 0x1000);
        *(short*)(e + 0x2c) = flat;
        *(short*)(e + 0x28) = flat;

        if (*(short*)(e + 0x12) > 0xfff) *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00450760 @ 0x00450760 - le sous-type <b>16</b> de la table 0x0056adc8. Une
    /// entite qui <b>declenche un script sur contact</b>, avec une descente et une remontee de
    /// trente pas de part et d'autre.
    ///
    /// <para>
    /// L'etat 1 est la veille : quand le bit de contact 0x40 est pose, l'entite visee est celle de
    /// +0x6c, ou <c>DAT_005de558</c> a defaut - la liste d'entites de secours que chaque
    /// chargement de zone remet a zero. Le choix du script se joue sur <b>l'enregistrement de
    /// 0x2000 octets</b> de la cible dans l'arene : vide, c'est le script 3 et l'entite passe
    /// directement en 0x65 ; non vide et sans le bit 0x10000, c'est le script 0.
    /// </para>
    /// <para>
    /// <b>L'appel indirect de l'etat 4 est un no-op verifie</b> : <c>PTR_FUN_0056afac</c> vaut
    /// <c>0x00401FA0</c> dans <c>Legacy/Binaries/Blaze.exe</c>, la souche <c>ret</c> nu que la
    /// table de sauts d'entites utilise partout ailleurs. Il est donc transcrit comme rien du tout,
    /// et non comme une lacune.
    /// </para>
    /// <para>
    /// L'etat 0x65 attend que les quatre bits bas de +0x9 retombent avant de rendre l'entite a son
    /// etat 0. C'est la seule sortie du cycle.
    /// </para>
    /// </summary>

    /// <summary>
    /// L'appel indirect de <c>PTR_FUN_0056afac</c> — l'entree <b>57</b> de la table de verbes de
    /// zone <c>0x0056aec8</c>, atteinte <b>hors de l'opcode 0</b> : c'est un decor qui appelle un
    /// verbe directement, pas un script.
    ///
    /// <para>Meme discipline que <see cref="CallEntityHook_0056aff0"/> : le dispatch se fait sur la
    /// valeur d'adresse, un cas par fonction transcrite, et tout le reste leve en se nommant. La
    /// table etant installee par zone, l'adresse qui s'y trouve depend de l'endroit ou l'on est.</para>
    /// </summary>
    private static void CallAreaVerbSlot57(byte* entity)
    {
        uint target = Res.s_Vt_0056aec8[57];

        switch (target)
        {
            // Souche `ret` nu : rien a faire. C'est ce que la reconstruction supposait pour TOUTES
            // les zones jusqu'au 2026-08-25, ce qui rendait le coffre muet.
            case 0x00401FA0: return;

            case 0x0053DEA0: AreaVerb_0053DEA0(entity); return;

            default:
                throw new NotImplementedException(
                    $"FUN_00450760, etat 4 : l'entree 57 de la table 0x0056aec8 vaut 0x{target:X6}, " +
                    "pas encore reconstruite. C'est le verbe que le coffre appelle pour ouvrir son " +
                    "panneau, atteint par PTR_FUN_0056afac et non par l'opcode 0 du script.");
        }
    }

    private static void EntityHandler_00450760(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            // Ghidra ecrit les deux octets separement ; c'est le short de +0x12 remis a zero.
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1 && (*(uint*)e & 0x40) != 0)
        {
            byte* target = *(byte**)(e + 0x6c);
            if (target == null) target = Res.s_EntityList_005de558;

            uint targetWord = *(uint*)target;

            // Ghidra: `*(int *)(((byte)puVar2[2] + 0x78) * 0x2000 + *(int *)MainMem_exref) == 0`
            // - l'enregistrement de 0x2000 octets que l'identifiant de +0x8 designe dans l'arene.
            bool recordEmpty =
                *(int*)(GetMainMemPtr() + (*(byte*)(target + 8) + 0x78) * 0x2000) == 0;

            if (recordEmpty)
            {
                RunEntityScript(s_Unknown_005e97cc, target, 3);   // Ghidra: FUN_00513dd0
                *(short*)(e + 0x10) = 0x65;
            }

            if ((targetWord & 0x10000) == 0 && !recordEmpty)
            {
                RunEntityScript(s_Unknown_005e97cc, target, 0);
            }
        }

        if (*(short*)(e + 0x10) == 4)
        {
            FreezeAllEntities();                                  // Ghidra: FUN_00535300(0)
            *(short*)(e + 0x34) -= 0x23;
            *(short*)(e + 0x12) += 1;
            *(uint*)e = *(uint*)e & 0xfbffffff | 0x2000;

            if (*(short*)(e + 0x12) > 0x1e)
            {
                // Ghidra: `(*(code *)PTR_FUN_0056afac)(param_1[0x1b], 0)`.
                //
                // **Cet appel etait supprime, sur la foi d'une souche `ret` nu.** Il ne l'est pas :
                // `0x0056afac` est l'entree **57** de la table 0x0056aec8 (l'ecart au debut vaut
                // 0xe4, soit 57 dwords), et pour la zone 0 cette entree vaut `0x0053DEA0` — le
                // verbe 57, celui qui ouvre le panneau. C'est ici, et nulle part ailleurs, que le
                // coffre de la chambre demandait son interface.
                //
                // Trouve le 2026-08-25 en suivant a la trace un appui sur le coffre : la boite
                // « Oui / Non » s'ouvrait, la reponse etait bien latchee, le script de reponse
                // posait l'etat 4 — et l'etat 4 arrivait ici pour ne rien faire.
                CallAreaVerbSlot57(*(byte**)(e + 0x6c));

                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
                UnfreezeAllEntities();                            // Ghidra: FUN_00535400
            }
        }

        if (*(short*)(e + 0x10) == 5)
        {
            uint word = *(uint*)e;
            *(uint*)e = word & 0xffffdfff;
            if ((e[9] & 0xf) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(uint*)e = word & 0xffffdfff;
            }
        }

        if (*(short*)(e + 0x10) == 100)
        {
            *(short*)(e + 0x34) += 0x23;
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) > 0x1e)
            {
                *(short*)(e + 0x10) = 0x65;
                *(short*)(e + 0x14) = 10;
                *(uint*)e &= 0xffffdfff;
            }
        }

        if (*(short*)(e + 0x10) == 0x65 && (e[9] & 0xf) == 0)
        {
            *(short*)(e + 0x10) = 0;
            *(short*)(e + 0x14) = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_004742b0 @ 0x004742b0 - <b>retire le bit 0x4000000</b> de toute entite qui ne
    /// porte pas 0x10000000, sur les trois tables d'entites animees : 0x005ff400 (0x20 entrees),
    /// 0x005f2d40 (0x80) et 0x005edee0 (0x80). Trente-huit lignes, aucune dependance.
    ///
    /// <para>
    /// Le bit 0x10000000 protege donc de l'effacement ; 0x4000000 est celui que
    /// <c>ResetCameraAndWakeSceneEntities</c> pose. La fonction est le pendant de ce reveil : elle
    /// est appelee a chaque pas du gestionnaire de coffre tant qu'il n'est pas dans l'etat 300.
    /// </para>
    /// </summary>
    private static void ClearWakeBitOnUnpinnedEntities_004742B0()
    {
        ClearWakeBitOnTable(Mem.Entity(0x005ff400), 0x20);
        ClearWakeBitOnTable(Mem.Entity(0x005f2d40), 0x80);
        ClearWakeBitOnTable(Mem.Entity(0x005edee0), 0x80);
    }

    /// <summary>Une des trois passes de <see cref="ClearWakeBitOnUnpinnedEntities_004742B0"/> ; le
    /// pas est celui de tout enregistrement d'entite, 0x27 dwords.</summary>
    private static void ClearWakeBitOnTable(byte* table, int count)
    {
        for (int i = 0; i < count; i++)
        {
            uint* word = (uint*)(table + i * 0x9c);
            if ((*word & 0x10000000) == 0) *word &= 0xfbffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0040e640 @ 0x0040e640 — <b>le grand frere</b> de
    /// <see cref="ClearWakeBitOnUnpinnedEntities_004742B0"/> : memes trois passes sur les memes
    /// trois tables d'entites, plus deux ajouts. C'est cette variante-la, et non la courte, que le
    /// sous-type 17 appelle en fin de pas.
    ///
    /// <para>
    /// Sur la <b>premiere</b> table seulement, les sous-types <c>0x1c</c> et <c>0x17</c> recoivent
    /// en plus le bit <c>0x40000000</c>. C'est pourquoi cette passe ne peut pas reutiliser
    /// <see cref="ClearWakeBitOnTable"/> et est ecrite en toutes lettres : l'octet lu est +5, donc
    /// dans le second mot de l'enregistrement, sans interference avec le mot de drapeaux de +0.
    /// </para>
    /// <para>
    /// La <b>quatrieme</b> boucle ne touche plus d'entites de scene mais les quatre fiches
    /// d'emplacement du groupe — <c>s_SlotFlags_005e93c0</c>, meme pas de 0x9c : elle remet le mot
    /// de couleur de +0x38 a blanc (trois octets a 0xff) et efface le bit 2 de +0x36. Ghidra
    /// l'ecrit en decalages negatifs autour de <c>&amp;DAT_005e93f9</c>, ce qui masque la base.
    /// </para>
    /// </summary>
    private static void ResetEntityWakeBits_0040E640()
    {
        ResetSceneWakeBits_00444E60();

        byte* slots = Mem.Arena(0x005e93c0);
        for (int i = 0; i < 4; i++)
        {
            byte* record = slots + i * 0x9c;
            record[0x38] = 0xff;
            record[0x39] = 0xff;
            record[0x3a] = 0xff;
            *(ushort*)(record + 0x36) &= 0xfffb;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00444e60</c> — <b>les trois premieres boucles de
    /// <see cref="ResetEntityWakeBits_0040E640"/>, et rien d'autre</b>. Absente de <c>Blaze.c</c>,
    /// relevee au desassembleur, 57 instructions.
    ///
    /// <para>
    /// Le binaire a emis ce bloc <b>deux fois</b> : une fois au sein de <c>FUN_0040e640</c>, qui y
    /// ajoute la quatrieme boucle sur les fiches d'emplacement, et une fois seul a
    /// <c>0x00444e60</c>, ou il sert de <b>verbe de zone</b> (<see cref="AreaVerb_00444E60"/>). Les
    /// deux relevés ne different que par l'allocation des registres. Le corps est donc ecrit une
    /// fois ici et appele des deux endroits.
    /// </para>
    /// </summary>
    private static void ResetSceneWakeBits_00444E60()
    {
        byte* scene = Mem.Entity(0x005ff400);
        for (int i = 0; i < 0x20; i++)
        {
            uint* word = (uint*)(scene + i * 0x9c);
            if ((*word & 0x10000000) == 0) *word &= 0xfbffffff;

            sbyte subtype = *(sbyte*)((byte*)word + 5);
            if (subtype == 0x1c) *word |= 0x40000000;
            if (subtype == 0x17) *word |= 0x40000000;
        }

        ClearWakeBitOnTable(Mem.Entity(0x005f2d40), 0x80);
        ClearWakeBitOnTable(Mem.Entity(0x005edee0), 0x80);
    }
}
