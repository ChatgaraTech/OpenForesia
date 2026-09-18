// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE VARAN — 0x004A1D50, sous-type 13 du bloc B de la foret (zone 3, sous-zones 6 a 8).
//
// LANCEUR DE SORTS. 3 904 octets, absents de `Blaze.c`, une table de sauts a TREIZE
// entrees. Il porte la prise et le jet (`VaranSeizeAndThrow`).
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
    // 0x004A1D50 — LE VARAN, sous-type 13 du bloc B de la foret (sous-zones 6 a 8). Transcrite le
    // 2026-09-02 au desassembleur (Legacy/Raw/Blaze/FUN_004A1D50.asm, 3 904 octets, absente de
    // Blaze.c). Cinquante-huit etiquettes, une table de sauts a TREIZE entrees a 0x004A2BB8.
    //
    // LE CRIBLE L'ANNONCAIT A 1 171 LIGNES DE LA FOURMI GEANTE — L'ECART LE PLUS GRAND DU LOT —, ET
    // C'ETAIT LA BONNE LECTURE : ce n'est pas une variante de la charpente familiale, c'est une
    // AUTRE CHARPENTE. Les treize routines d'intelligence deja ecrites appellent toutes les memes
    // quatre feuilles — FUN_00488160 l'integration, FUN_004DB5A0 l'animation, FUN_004DFAF0 la fin
    // de geste, FUN_004E3770 l'interruption. Le Varan n'en appelle AUCUNE : il inline la premiere
    // et la deuxieme, et il remplace les deux dernieres par un bloc a lui.
    //
    // TROIS DIFFERENCES DE FORME, ET ELLES SE TIENNENT :
    //
    //   - LA MACHINE A POSTURES A TREIZE CASES, pas quatre. La table 0x004A2BB8 en compte treize
    //     (0 a 0x0c), dont trois vides — 4, 5 et 0x0a retombent sur la sortie. La posture 5 est
    //     donc « figee » au sens strict : elle ne fait rien ET elle pose le bit 0x10, qui est le
    //     bit que toute la famille lit comme « pas de decision ».
    //
    //   - LE GESTE N'INTERROMPT PAS LA POSTURE. Chez ses treize sœurs, `flags & 2` ouvre un bloc de
    //     geste qui rend la main ; ici il n'ouvre qu'un PRELUDE — la sonde de contact des postures
    //     0x0b et 8 — et la case de la posture s'execute ensuite dans tous les cas. C'est la seule
    //     routine du depot ou geste et posture tournent la meme image.
    //
    //   - LE CONE QUI DECIDE N'EST PAS LE PAS QUI TOURNE. Partout ailleurs, `TurnSceneEntityToward`
    //     teste l'ecart contre la largeur dont il va tourner. Ici les deux nombres sont
    //     independants : la posture 2 juge sur 0x100 et tourne de `hardiesse * 2 + 0x80`, la
    //     posture 1 juge sur 0xc8 et tourne de `hardiesse + 0x32`, l'incantation juge sur 0xc8 et
    //     tourne de `hardiesse / 2 + 0x32`. Le corps ne rattrape jamais son cap d'un coup.
    //
    // CE QU'IL FAIT :
    //
    //   - IL CHOISIT SA PROIE A LA PREMIERE IMAGE, ET IL LA CHOISIT PAR CLASSE. L'octet +0x41 sert
    //     de drapeau « deja ne » ; a zero il appelle FUN_004A1020 avec le mode 5, celui qui cherche
    //     un membre du groupe dont l'octet de classe (MainMem + 0xf0150) vaut 3. Aucune des treize
    //     autres ne touche a +0x6c : elles heritent de la cible que la naissance leur a donnee.
    //     Et le choix n'est ferme qu'a moitie — le chercheur tire une piece a chaque appel, et
    //     ignore le mode demande six fois sur dix pour prendre un vivant au hasard.
    //
    //   - IL SE HEURTE AUX MURS, ET IL EN TIENT COMPTE. Apres l'integration il relit le mot de
    //     contact +0x90 : s'il porte 0x2000ad82, tout le placement de l'image est ANNULE — les
    //     onze champs sont remis a ce qu'ils etaient avant — et le corps demande a FUN_004D5E60
    //     de quel cote passer. Cette fonction plante deux sondes, un quart de tour a gauche et un
    //     quart de tour a droite, 0x3c unites devant et 0x14000 au-dessus, et rend -1, 0 ou +1.
    //     Le Varan tourne alors de `(hardiesse + 0x40)` de ce cote-la, et tire a pile ou face
    //     quand les deux cotes se valent. C'est le premier corps du depot qui CONTOURNE.
    //
    //   - IL EMPOIGNE ET IL JETTE, mais son coup ne s'arme jamais. La posture 8 est une charge
    //     amortie dont la sonde de contact projette la victime a `+0x18000` de haut et a
    //     `[+0x5c] >> 11` d'elan horizontal, avec l'attaque du bloc de modele DIVISEE PAR QUATRE —
    //     le quart, la ou le Crabe, le Grizzly, le Geant et le Behemoth prennent la moitie. Mais
    //     ce prelude est garde par `flags & 2`, et rien dans le corps ne pose ce bit pour la
    //     posture 8 : seules la posture 0x0b (la morsure) et sa replanification le posent. LA
    //     PRISE EST DONC INATTEIGNABLE TELLE QUE LE BINAIRE EST ECRIT. Transcrite quand meme, et
    //     signalee ici : c'est un defaut de 1998, pas un trou de lecture.
    //
    //   - IL LANCE UN SORT, ET C'EST LE TROISIEME DU DEPOT A LE FAIRE. La posture 0x0c lit le mot
    //     0x005529c8 — famille 0, indice 0x13 —, le range en +0x06 et le passe a CastSpell. Une
    //     SEULE fiche, la ou le Behemoth et le Trent en tirent une sur trois ; et c'est la fiche
    //     que ces deux-la ont en commun. Les trois lanceurs de la foret partagent donc exactement
    //     un sort.
    //
    //   - IL A CINQ FACONS DE SE DEPLACER, ce qui est le record de la famille : marcher (2 et 7),
    //     louvoyer en ligne droite le long d'un cap range en +0x10 (3), BONDIR d'une impulsion
    //     unique de `(hardiesse + 0x100) << 6` plus 0x1e000 de haut (6), et TOURNER SUR PLACE d'un
    //     pas fixe tire dans ±50 pendant qu'il fonce (9). La posture 9 est la seule du depot ou
    //     +0x10 porte une VITESSE ANGULAIRE et non un cap.
    //
    //   - ET SA REPLANIFICATION A UNE ZONE DE RECOUVREMENT. Trois plages — moins de 0xfa, de 0xfa
    //     a 0x2bb, au-dela de 0x258 — mais les deux dernieres se CHEVAUCHENT de 0x258 a 0x2bb :
    //     entre 600 et 700 unites le corps choisit une posture dans la bande du milieu, puis en
    //     rechoisit une dans celle du lointain qui l'ecrase. Le premier tirage n'est pas mort pour
    //     autant : il laisse derriere lui +0x10 et +0x12. Transcrit en trois `if` successifs, pas
    //     en `else if`, pour que le recouvrement reste lisible.
    // =============================================================================================

    /// <summary>
    /// <b>Le placement d'une entite de scene</b> : les onze champs que
    /// <see cref="FinalizeEntityPlacement2"/> ecrit et que deux corps de la foret sauvegardent pour
    /// pouvoir annuler une image entiere — <see cref="SettleSceneEntityOnGroundAndWalls"/> quand le heurt
    /// est avere, <see cref="ProbeClearSide_004D5E60"/> a chaque sonde.
    ///
    /// <para>Le cap <c>+0x32</c> est a part : le premier ne le rend pas, le second si.</para>
    /// </summary>
    private struct SceneEntityPlacement
    {
        public int X;            // +0x1c
        public int Y;            // +0x20
        public int Z;            // +0x24
        public int At60;         // le plancher que le maillage a rendu
        public int At64;
        public int At7c;
        public int At80;
        public int At84;
        public int At88;
        public int At8c;
        public ushort At90;      // le mot de contact
        public short Heading;    // +0x32
    }

    private static void SaveSceneEntityPlacement(byte* e, SceneEntityPlacement* p)
    {
        p->X = *(int*)(e + 0x1c);
        p->Y = *(int*)(e + 0x20);
        p->Z = *(int*)(e + 0x24);
        p->At60 = *(int*)(e + 0x60);
        p->At64 = *(int*)(e + 0x64);
        p->At7c = *(int*)(e + 0x7c);
        p->At80 = *(int*)(e + 0x80);
        p->At84 = *(int*)(e + 0x84);
        p->At88 = *(int*)(e + 0x88);
        p->At8c = *(int*)(e + 0x8c);
        p->At90 = *(ushort*)(e + 0x90);
        p->Heading = *(short*)(e + 0x32);
    }

    /// <summary>Les onze champs, sans le cap — c'est ce que les deux sites restaurent.</summary>
    private static void RestoreSceneEntityPlacement(byte* e, SceneEntityPlacement* p)
    {
        *(int*)(e + 0x1c) = p->X;
        *(int*)(e + 0x20) = p->Y;
        *(int*)(e + 0x24) = p->Z;
        *(int*)(e + 0x60) = p->At60;
        *(int*)(e + 0x64) = p->At64;
        *(int*)(e + 0x7c) = p->At7c;
        *(int*)(e + 0x80) = p->At80;
        *(int*)(e + 0x84) = p->At84;
        *(int*)(e + 0x88) = p->At88;
        *(int*)(e + 0x8c) = p->At8c;

        // Le mot de contact est sauvegarde en `ushort` et rendu en `dword` etendu de zeros : le
        // binaire ecrit `and eax,0xffff` avant le `mov`, donc les seize bits hauts partent.
        *(uint*)(e + 0x90) = p->At90;
    }

    /// <summary>
    /// Ghidra: FUN_004d5de0 @ 0x004d5de0 — <b>ce qui annule un geste en cours</b>, selon les traits
    /// de l'espece. Le trait <c>0x10</c> coupe le geste (bit <c>2</c>), le trait <c>0x20</c> coupe
    /// la prise (bit <c>4</c>), et dans les deux cas le compte a rebours <c>+0x43</c> tombe a zero.
    ///
    /// <para>
    /// Les treize sœurs du Varan portent le premier des deux cas <b>en ligne</b>, dans leur bloc de
    /// geste (<c>if ((traits &amp; 0x10) != 0) { e[0x43] = 0; flags &amp;= ~2; }</c>). Le Varan est
    /// le seul de la foret a appeler la fonction, et il l'appelle <b>avant</b> tout le reste, pas
    /// dans un bloc de geste.
    /// </para>
    /// <para>
    /// <b>Le tirage ne decide de rien.</b> Les deux branches tirent <c>hasard % 100</c> et
    /// n'agissent que si le reste est positif ou nul — ce qu'il est toujours, <c>CrtRand</c> etant
    /// positif. Le jet est donc mort ; il consomme neanmoins un tirage, ce qui deplace le flux du
    /// generateur, et c'est pour cela qu'il est transcrit.
    /// </para>
    /// </summary>
    private static void CancelGestureOnTrait_004D5DE0(byte* e, uint traits)
    {
        if ((traits & 0x10) != 0 && (*(uint*)e & 2) != 0
            && (*(short*)(e + 0x1a) == 0 || *(short*)(e + 0x12) == 0))
        {
            if (CrtRand() % 100 >= 0)                      // Ghidra: FUN_00547140 — toujours vrai
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffd;
            }
        }

        if ((traits & 0x20) != 0 && (*(uint*)e & 4) != 0
            && (*(short*)(e + 0x1a) == 0 || *(short*)(e + 0x12) == 0))
        {
            if (CrtRand() % 100 >= 0)                      // Ghidra: FUN_00547140 — toujours vrai
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffb;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_004c3810 @ 0x004c3810 — <b>l'assommement</b> : quand le clignotement de degat
    /// <c>+0x96</c> vaut exactement 10, l'entite est remise a l'attente et perd ses bits de geste,
    /// de prise et de vol (<c>0xfffff7f9</c>).
    ///
    /// <para>
    /// <b>Le site d'appel lui passe quatre arguments</b> (<c>e, -1, -1, -1</c>) et en nettoie
    /// seize octets ; Ghidra n'en a recupere qu'un, et le corps n'en lit effectivement qu'un. Les
    /// trois autres sont ignores — note ici plutot que devinee.
    /// </para>
    /// </summary>
    private static void ResetSceneEntityOnStun_004C3810(byte* e)
    {
        if (*(short*)(e + 0x96) != 10) return;

        e[0x42] = 0;
        e[0x43] = 0;
        *(uint*)(e + 0x5c) = 0;
        *(uint*)e &= 0xfffff7f9;
    }

    /// <summary>
    /// Ghidra: FUN_004a2c30 @ 0x004a2c30 — <b>le filtre des effets sur autrui</b> : rend vrai quand
    /// la victime ne doit rien recevoir. Cinq enveloppes du binaire le partagent
    /// (<c>0x004a2bf0</c>, <c>0x004c4210</c>, <c>0x004c4250</c>, <c>0x004d8f30</c>,
    /// <c>0x004dac80</c>), chacune avec son effet a appliquer derriere.
    ///
    /// <para>Effet de bord : si le lanceur porte <c>0x400000</c>, son octet <c>+7</c> passe a 7
    /// avant tout examen.</para>
    /// </summary>
    private static bool RejectStatusTarget_004A2C30(uint* caster, uint* victim)
    {
        if ((*caster & 0x400000) != 0) ((byte*)caster)[7] = 7;

        bool reject = false;
        uint flags = *victim;

        if ((flags & 0x50002000) != 0 || (flags & 0x80000000) == 0) reject = true;
        if (*(short*)((byte*)victim + 0x12) != 0) reject = true;
        if ((flags & 0x80000000) == 0 || (*caster & 0x80000000) == 0) reject = true;

        return reject;
    }

    /// <summary>
    /// Ghidra: FUN_004a2bf0 @ 0x004a2bf0 — <b>l'effet de piege, s'il est permis</b> : le filtre de
    /// <see cref="RejectStatusTarget_004A2C30"/>, puis <see cref="ApplyTrapEffect_0053C420"/>.
    /// </summary>
    /// <returns><c>1</c> si la victime a ete refusee, <c>0</c> si l'effet est parti.</returns>
    private static uint ApplyTrapEffectIfAllowed_004A2BF0(uint* caster, uint* victim, int keep)
    {
        if (RejectStatusTarget_004A2C30(caster, victim)) return 1;

        ApplyTrapEffect_0053C420(caster, victim, keep);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_004d5e60 @ 0x004d5e60 — <b>de quel cote peut-on passer ?</b> Deux sondes, un
    /// quart de tour de part et d'autre du cap, <c>0x3c</c> unites devant et <c>0x14000</c>
    /// au-dessus : le corps est deplace pour de vrai, <see cref="FinalizeEntityPlacement2"/> est
    /// rappele, le mot de contact <c>+0x90</c> est releve, puis <b>les onze champs de placement
    /// sont remis exactement comme ils etaient</b>. Deux fois de suite, et le cap avec, la seconde
    /// fois.
    ///
    /// <para>
    /// <b>Ghidra rend ce corps illisible</b> — quatre <c>unaff_</c>, un seul parametre recupere sur
    /// deux, et les onze champs restaures dans le desordre. Le relevé qui compte est
    /// <c>Legacy/Raw/Blaze/FUN_004D5E60.asm</c>.
    /// </para>
    /// <para>
    /// Le chargement mort de la queue (<c>mov eax,[esp+38h]</c> a <c>0x004d604d</c>, qui relit
    /// l'emplacement du premier argument reecrit en scratch) n'atteint aucune sortie : les quatre
    /// chemins reecrivent tous <c>eax</c> avant le <c>ret</c>. Il n'est donc pas transcrit.
    /// </para>
    /// </summary>
    /// <returns><c>-1</c> si seul le cote <c>+0x400</c> est barre, <c>+1</c> si seul le cote
    /// <c>-0x400</c> l'est, <c>0</c> si les deux le sont ou aucun.</returns>
    private static int ProbeClearSide_004D5E60(byte* e, uint mask)
    {
        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        // ------------------------------------------------------------- la sonde d'un cote
        int right = *(short*)(e + 0x32) + 0x400;
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(right) * 0x3c;
        *(int*)(e + 0x20) += 0x14000;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(right) * 0x3c;

        FinalizeEntityPlacement2((uint*)e);

        uint rightContact = *(ushort*)(e + 0x90);
        RestoreSceneEntityPlacement(e, &keep);

        // ------------------------------------------------------------- la sonde de l'autre
        int left = *(short*)(e + 0x32) - 0x400;
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(left) * 0x3c;
        *(int*)(e + 0x20) += 0x14000;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(left) * 0x3c;

        FinalizeEntityPlacement2((uint*)e);

        uint leftContact = *(ushort*)(e + 0x90);
        RestoreSceneEntityPlacement(e, &keep);
        *(short*)(e + 0x32) = keep.Heading;

        // ------------------------------------------------------------- le verdict
        bool rightBlocked = (rightContact & mask) != 0;
        bool leftBlocked = (leftContact & mask) != 0;

        if (rightBlocked) return leftBlocked ? 0 : -1;
        return leftBlocked ? 1 : 0;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005e93c0</c> — <b>la permutation de depart du chercheur de proie</b> :
    /// quatre rotations de <c>{0,1,2,3}</c>, une par ligne. <see cref="PickChasedPartyMember_004A1020"/>
    /// en tire une au hasard et parcourt les quatre emplacements dans cet ordre ; le DERNIER qui
    /// convient l'emporte, ce qui fait de l'ordre une vraie source de variete.
    /// </summary>
    private static readonly byte[] s_PartyScanOrder_004A1020 =
    [
        0, 1, 2, 3,
        1, 2, 3, 0,
        2, 3, 0, 1,
        3, 0, 1, 2,
    ];

    /// <summary>
    /// Ghidra: FUN_004a1020 @ 0x004a1020 — <b>choisir la proie</b> et la ranger en <c>+0x6c</c>.
    /// Le binaire emet ce corps plusieurs fois (<c>0x004a8150</c>, <c>0x004c8d80</c>…), une copie
    /// par zone ; seule celle de la foret est transcrite.
    ///
    /// <para>
    /// <b>Deux pieces avant tout choix.</b> La premiere decide si l'on rechoisit du tout : sur
    /// <c>hasard % 10</c>, cinq valeurs sur dix rendent la main sans rien toucher. La seconde
    /// decide si le mode demande compte : <c>hasard % 10 &lt; 6</c> le remplace par <c>0xffff</c>,
    /// le mode « n'importe qui de vivant ». <b>Un mode n'est donc respecte que deux fois sur
    /// dix.</b>
    /// </para>
    /// <para>
    /// Les modes lisent tous d'abord les deux memes bits de l'emplacement (<c>0x80000000</c> pose,
    /// <c>0x50000000</c> absent : vivant et disponible), puis se departagent — <b>1</b> et
    /// <b>2</b> sur le rapport de points de vie <c>(+0x148 &lt;&lt; 12) / +0x14c</c>, <b>3</b> a
    /// <b>7</b> sur l'octet de classe <c>MainMem + 0xf0150 + n * 0x2000</c>. Le Varan demande le
    /// mode <b>5</b> : la classe <c>3</c>.
    /// </para>
    /// <para>
    /// <b>Ce que la fonction rend quand elle ne choisit pas est <c>hasard / 10</c></b>, c'est-a-dire
    /// un entier sans rapport laisse dans <c>EAX</c>. Aucun des appelants ne le lit ; il est
    /// reproduit pour que la lecture du binaire reste verifiable.
    /// </para>
    /// </summary>
    /// <param name="unused">Le site d'appel du Varan pousse un troisieme argument nul et nettoie
    /// douze octets ; le corps ne le lit jamais.</param>
    private static byte* PickChasedPartyMember_004A1020(byte* e, int mode, int unused)
    {
        byte* slots = Inp.s_SlotFlags_005e93c0;                  // Ghidra: &DAT_005e93c0
        byte* mainMem = GetMainMemPtr();

        byte* chased = *(byte**)(e + 0x6c);
        uint keep = chased[8];
        ushort best = chased[8];

        int start = CrtRand() % 4;                               // Ghidra: FUN_00547140

        int coin = CrtRand();                                    // Ghidra: FUN_00547140
        if ((short)(coin % 10) <= 4) return (byte*)(nint)(coin / 10);

        if ((short)(CrtRand() % 10) < 6) mode = 0xffff;          // Ghidra: FUN_00547140

        fixed (byte* order = s_PartyScanOrder_004A1020)
        {
            byte* scan = order + start * 4;

            switch (mode)
            {
                // ------------------------------------------------ 1 : le plus entier
                case 1:
                    best = 0;
                    for (int n = 4; n != 0; n--, scan++)
                    {
                        int slot = *scan;
                        if ((*(uint*)(slots + slot * 0x9c) & 0x80000000) == 0) continue;
                        if ((*(uint*)(slots + slot * 0x9c) & 0x50000000) != 0) continue;

                        byte* rec = mainMem + (slot + 0x78) * 0x2000;
                        if ((*(short*)(rec + 0x148) << 0xc) / *(short*)(rec + 0x14c) > 0)
                        {
                            best = (ushort)slot;
                        }
                    }
                    break;

                // ------------------------------------------------ 2 : le plus entame
                case 2:
                    best = 0;
                    for (int n = 4; n != 0; n--, scan++)
                    {
                        int slot = *scan;
                        if ((*(uint*)(slots + slot * 0x9c) & 0x80000000) == 0) continue;
                        if ((*(uint*)(slots + slot * 0x9c) & 0x50000000) != 0) continue;

                        byte* rec = mainMem + (slot + 0x78) * 0x2000;
                        if ((*(short*)(rec + 0x148) << 0xc) / *(short*)(rec + 0x14c) < 10000)
                        {
                            best = (ushort)slot;
                        }
                    }
                    break;

                // ------------------------------------------------ 3 a 7 : par la classe
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    for (int n = 4; n != 0; n--, scan++)
                    {
                        int slot = *scan;
                        if ((*(uint*)(slots + slot * 0x9c) & 0x80000000) != 0
                            && (*(uint*)(slots + slot * 0x9c) & 0x50000000) == 0)
                        {
                            byte kind = mainMem[slot * 0x2000 + 0xf0150];

                            bool wanted = mode switch
                            {
                                3 => (kind & 7) == 0,
                                4 => (kind & 7) == 1,
                                5 => (kind & 7) == 3,
                                6 => (kind & 8) == 0,
                                _ => (kind & 8) != 0,
                            };

                            if (wanted) keep = (uint)(ushort)(short)slot;
                        }

                        best = (ushort)keep;
                    }
                    break;

                // ------------------------------------------------ 0xffff : n'importe qui
                case 0xffff:
                {
                    bool found = false;
                    do
                    {
                        uint slot = (uint)(CrtRand() % 4);       // Ghidra: FUN_00547140
                        if ((*(uint*)(slots + slot * 0x9c) & 0x80000000) != 0
                            && (*(uint*)(slots + slot * 0x9c) & 0x50000000) == 0)
                        {
                            found = true;
                            keep = slot;
                        }

                        best = (ushort)keep;
                    }
                    while (!found);
                    break;
                }
            }
        }

        byte* picked = slots + (short)best * 0x9c;
        *(byte**)(e + 0x6c) = picked;
        return picked;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005529c8</c> — <b>le sort du Varan</b>, un mot et un seul :
    /// <c>0x0013</c>, c'est-a-dire la <b>famille 0</b>, indice <b>19</b>.
    ///
    /// <para>
    /// <b>Les trois lanceurs de la foret partagent exactement cette fiche.</b> Le Behemoth tire 13,
    /// 19 ou 23 dans <see cref="s_BehemothSpells_00552A30"/>, le Trent 3, 13 ou 19 dans
    /// <see cref="s_TrentSpells_005529A8"/>, et le Varan n'en a qu'une : la 19. Elle est donc la
    /// seule des cinq fiches employees par des betes a l'etre par les trois.
    /// </para>
    /// <para>
    /// Un mot, pas une table : le corps ne tire rien et n'indexe rien — il lit l'adresse, range le
    /// mot en <c>+0x06</c>, puis passe l'octet haut comme famille et l'octet bas comme indice.
    /// </para>
    /// </summary>
    private const ushort s_VaranSpell_005529C8 = 0x0013;

    /// <summary>
    /// Ghidra: <c>0x004a1d50</c> — <b>l'intelligence du Varan</b>, sous-type 13 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 6 a 8 de la foret. Quatorzieme routine
    /// d'intelligence d'entite de scene, et <b>la premiere batie sur une autre charpente</b> : elle
    /// n'appelle aucune des quatre feuilles que les treize precedentes partagent.
    ///
    /// <para>
    /// Le decoupage reste lisible en trois temps — decider, appliquer, replanifier — mais le
    /// deuxieme est bien plus long qu'ailleurs : integration, placement, <b>pose au sol</b>,
    /// <b>heurt des murs</b> et assommement, tout en ligne dans le corps.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A1D50(byte* record)
    {
        byte* e = record;

        // Ghidra: 0x004a1d71 — la naissance choisit la proie, une fois pour toutes.
        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 5, 0);
        }

        // Ghidra: LAB_004a1da1 — le placement d'avant l'image, garde pour pouvoir l'annuler.
        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004a1e65 — la distance est RECALCULEE, a trois dimensions comme celle que
        // FUN_00535f20 vient de rendre, mais depuis l'entite VERS la cible et non l'inverse. Le
        // carre efface la difference ; le calcul est refait tel quel.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideVaranAction_004A1D50(e, model, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_004a260f — FUN_00488160 recopie instruction pour instruction, jusqu'aux
        // amortissements d'un huitieme et d'un deux-cent-cinquante-sixieme.
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        // Ghidra: 0x004a281f — a l'arret le cap de l'image est RENDU, ce qu'aucune sœur ne fait :
        // une entite figee ne garde meme pas le virage que son heurt lui a impose.
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanVaranAction_004A1D50(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a1eb0..0x004a260e</c> — <b>le premier temps</b> du Varan : l'animation, le
    /// prelude de contact, puis la case de la posture.
    ///
    /// <para>
    /// <b>Le prelude n'est pas un bloc de geste</b> : il ne rend pas la main. La case de
    /// <c>+0x42</c> s'execute la meme image, geste ou pas — c'est la difference de charpente qui
    /// separe le Varan de ses treize sœurs.
    /// </para>
    /// <para>
    /// <b>La posture 5 pose le bit <c>0x10</c></b>, celui que toute la famille lit comme « pas de
    /// decision », et sa case de la table de sauts est vide. C'est la posture d'immobilisation.
    /// </para>
    /// </summary>
    private static void DecideVaranAction_004A1D50(
        byte* e, byte* model, int boldness, int distance, int heading, int speed)
    {
        // Ghidra: 0x004a1eb0..0x004a1f12 — FUN_004db5a0 recopie en ligne, au mot pres.
        AdvanceEntityAnimation_004DB5A0((uint*)e);

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le prelude de contact
        if ((flags & 2) != 0)
        {
            if (e[0x42] == 0x0b && *(short*)(e + 0x1a) == 0x0a) VaranBite_004A1F42(e);
            if (e[0x42] == 8) VaranSeizeAndThrow_004A1FC7(e, model);
        }

        // ================================================================ la posture
        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = 0;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = 0x5000;
                    e[0x43] = (byte)(CrtRand() % 0x1e + 0x14);   // Ghidra: FUN_00547140
                }

                // Le rebours descend AVANT le test, donc il descend meme l'image ou le cap est
                // pris ; il est aussitot ecrase par zero dans ce cas.
                e[0x43]--;

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    return;
                }

                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
                return;

            // ---------------------------------------------------------- 2 : marcher a portee
            case 2:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x80 : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(ushort*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    // Il AVANCE sa position lui-meme, le long du cap range en +0x10, puis remet sa
                    // poussee a zero : l'integration ne verra rien. Le meme decouplage que chez le
                    // Geant et le Behemoth, mais avec l'echelle de vitesse dans le produit.
                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) +=
                        ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) +=
                        (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    // ... et il regarde la cible pendant ce temps.
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(ushort*)(e + 0x32) +=
                            (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
                    }
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(ushort*)(e + 0x18) = 1;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 6 : bondir
            case 6:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    uint f = *(uint*)e;

                    if ((f & 1) != 0)
                    {
                        // Deja en l'air : le bond est refuse et la posture rendue tout de suite.
                        e[0x43] = 0;
                    }
                    else
                    {
                        *(uint*)e = f & 0xfeffffff;
                        *(ushort*)(e + 0x18) = 1;
                        *(ushort*)(e + 0x1a) = 0;

                        int thrust = (boldness + 0x100) << 6;
                        *(int*)(e + 0x5c) = thrust;
                        int step = (speed * thrust) >> 0xc;

                        short course = *(short*)(e + 0x10);
                        *(int*)(e + 0x50) +=
                            ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x58) +=
                            (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 1;
                        *(ushort*)(e + 0x12) = 1;
                    }
                }

                // Le bond dure tant que l'entite n'a pas touche terre.
                if ((*(uint*)e & 1) != 0) return;
                e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 7 : marcher droit
            case 7:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 1)
                {
                    if (distance < *(short*)(e + 0x10)) e[0x43] = 0;
                    else if (e[0x43] != 0) e[0x43]--;
                }

                if (phase != 0) return;

                *(ushort*)(e + 0x18) = 1;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 8 : la charge amortie
            case 8:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = ((boldness & ~3) + 0x50) << 0xa;
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    e[0x43] = (byte)(CrtRand() % 10 + 0x1e);     // Ghidra: FUN_00547140
                }

                if (*(ushort*)(e + 0x12) == 1)
                {
                    byte left = e[0x43];

                    // Elle ne corrige son cap que pendant les premieres images ; passe 0x14 elle
                    // fonce tout droit.
                    if (left > 0x14 && ((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(ushort*)(e + 0x32) +=
                            (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
                    }

                    left--;
                    e[0x43] = left;
                    if (left == 1) *(ushort*)(e + 0x12) = 2;
                }

                if (*(ushort*)(e + 0x12) != 2) return;

                // Ghidra: 0x004a2491 — l'amortissement d'un huitieme, plancher a 0x5000.
                int v = *(int*)(e + 0x5c);
                v += -v >> 3;
                *(int*)(e + 0x5c) = v;
                if (v < 0x5000) e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 9 : tourner en foncant
            case 9:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    // +0x10 porte ici une VITESSE ANGULAIRE, pas un cap : -50 a +40, par pas de 10.
                    *(short*)(e + 0x10) = (short)((CrtRand() % 10 - 5) * 10);
                    *(ushort*)(e + 0x12) = 1;
                    e[0x43] = (byte)(CrtRand() % 10 + 0x14);     // Ghidra: FUN_00547140
                    *(int*)(e + 0x5c) = ((boldness & ~3) + 0x14) << 0xa;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                *(ushort*)(e + 0x32) += *(ushort*)(e + 0x10);
                e[0x43]--;
                return;

            // ---------------------------------------------------------- 0x0b : la morsure
            case 0x0b:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    uint f = *(uint*)e;
                    *(ushort*)(e + 0x12) = 1;
                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (f & 0xfeffffff) | 2;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                // Le geste finit avec l'ANIMATION, pas sur un rebours.
                uint g = *(uint*)e;
                if ((g & 0x2000000) == 0) return;

                e[0x43] = 0;
                *(uint*)e = g & 0xfffffffd;
                return;

            // ---------------------------------------------------------- 0x0c : l'incantation
            case 0x0c:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 6) = s_VaranSpell_005529C8;

                    *(int*)(e + 0x5c) = 0;
                    *(ushort*)(e + 0x18) = 4;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e &= 0xfeffffff;
                    *(ushort*)(e + 0x12) = 1;

                    CastSpell_0053A980(
                        (uint*)e,
                        (s_VaranSpell_005529C8 >> 8) & 0xff,
                        s_VaranSpell_005529C8 & 0xff,
                        -1, -1);
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;

                if (((heading + 0xc8) & 0xfff) <= 0x190) return;

                // Le seul site ou le pas de virage prend la MOITIE de la hardiesse.
                *(ushort*)(e + 0x32) += (ushort)((heading & 0xfff) < 0x800
                    ? (boldness >> 1) + 0x32
                    : -0x32 - (boldness >> 1));
                return;

            // ---------------------------------------------------------- 4, 5, 0x0a : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a1f42..0x004a1fb8</c> — <b>la morsure</b>, seizieme image du geste de la
    /// posture <c>0x0b</c>, une image et une seule.
    ///
    /// <para>
    /// La sonde est batie sur la pile — <c>{0, 0x1e, 0x32, 0x64, 0, 0x64}</c>, la meme forme a six
    /// mots que celle du Trent —, et le releve de <c>+0x1a</c> est refait APRES le balayage : si le
    /// balayage a fait avancer l'animation, le coup ne porte pas.
    /// </para>
    /// <para>
    /// <b>Une morsure sur quinze empoisonne</b>, et seulement si <c>+0x16</c> vaut 1 : c'est
    /// <see cref="ApplyTrapEffectIfAllowed_004A2BF0"/>, l'effet de piege, applique par une bete.
    /// Aucune des treize routines precedentes n'appelle cet effet.
    /// </para>
    /// </summary>
    private static void VaranBite_004A1F42(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x64;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;
        if (*(short*)(e + 0x1a) != 0x0a) return;

        *(ushort*)(prey + 0x96) = 0x10;
        *(uint*)prey |= 0x40;

        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

        if (*(short*)(e + 0x16) != 1) return;
        if (CrtRand() % 15 != 1) return;                 // Ghidra: FUN_00547140

        ApplyTrapEffectIfAllowed_004A2BF0((uint*)e, (uint*)prey, 0);
    }

    /// <summary>
    /// Ghidra: <c>0x004a1fc7..0x004a2091</c> — <b>la prise et la projection</b> de la posture 8.
    ///
    /// <para>
    /// <b>L'attaque du bloc de modele est divisee par QUATRE</b> le temps de la resolution, puis
    /// rendue : le Crabe, le Grizzly, le Geant et le Behemoth prennent la moitie, le Varan le
    /// quart. Il jette plus qu'il ne blesse.
    /// </para>
    /// <para>
    /// La victime part a <c>0x18000</c> de haut et a <c>[+0x5c] &gt;&gt; 11</c> d'elan horizontal
    /// <b>dans le cap du Varan</b>, donc dans le prolongement de sa charge — ni vers elle ni a
    /// l'oppose, contrairement au Geant qui vise sa cible et au Mechant ours qui lit l'attaque du
    /// modele pour choisir un sens.
    /// </para>
    /// <para>
    /// <b>ET CE CORPS EST INATTEIGNABLE.</b> Le prelude qui l'appelle est garde par le bit
    /// <c>2</c> du mot de drapeaux, et rien dans la routine ne pose ce bit pour la posture 8 :
    /// <see cref="ReplanVaranAction_004A1D50"/> l'arme sans lui (<c>0x004a2b26</c>), et le seul
    /// site qui pose le bit est celui de la morsure, qui l'efface en finissant. Un Varan charge
    /// donc, mais n'empoigne jamais. Defaut de 1998, transcrit tel quel.
    /// </para>
    /// </summary>
    private static void VaranSeizeAndThrow_004A1FC7(byte* e, byte* model)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x64;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(prey + 0x96) = 0x10;
        *(uint*)prey |= 0x40;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 2);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

        if ((*(uint*)prey & 0x40000000) != 0) return;

        SpawnEffectEntity(e, 0x39, 0, -1);               // Ghidra: FUN_0047a9d0
        *(uint*)prey |= 1;

        int course = *(short*)(e + 0x32);
        int push = *(int*)(e + 0x5c) >> 0xb;

        *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * push;
        *(int*)(prey + 0x54) = 0x18000;
        *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(course) * push;
    }

    /// <summary>
    /// Ghidra: <c>0x004a26af..0x004a280b</c> (le Varan) et <c>0x004a0afa..0x004a0c57</c> (la Fleur
    /// du mal) — <b>la pose au sol et le heurt des murs</b>, ce que la charpente du Varan met a la
    /// place de <c>FUN_004dfaf0</c> et <c>FUN_004e3770</c>.
    ///
    /// <para>
    /// <b>Ce n'est ni l'une ni l'autre.</b> La borne de sol est <c>+0x60</c> et non <c>+0x64</c>,
    /// le masque de contact n'est pas <c>0x2000a882</c>, et le mot de contact perd d'abord ses bits
    /// <c>0x100</c> et <c>0x200</c>. Trois constantes voisines de celles des deux feuilles
    /// partagees, et trois constantes differentes.
    /// </para>
    /// <para>
    /// <b>Les deux emissions ne different que par le masque</b>, et il apparait deux fois : au test
    /// d'entree et dans la sonde de contournement. <c>0x2000ad82</c> pour le Varan,
    /// <c>0x2000af82</c> pour la Fleur du mal — un bit de plus, <c>0x200</c>. Tout le reste, y
    /// compris les onze champs restaures et les deux sorties, coincide instruction pour
    /// instruction ; c'est pourquoi le masque est un parametre plutot que deux corps.
    /// </para>
    /// <para>
    /// <b>L'image de l'atterrissage ne heurte rien</b> : si l'entite etait en l'air et vient de se
    /// poser, le corps rend la main avant meme de regarder le mot de contact. C'est ce qui empeche
    /// un bond de se conclure sur un mur.
    /// </para>
    /// <para>
    /// <b>Et le heurt annule l'image.</b> Les onze champs de placement sont remis a ce qu'ils
    /// etaient avant l'integration, le geste est coupe, la posture passe a 7 si elle n'y etait pas,
    /// et <see cref="ProbeClearSide_004D5E60"/> dit de quel cote contourner.
    /// </para>
    /// </summary>
    /// <param name="dropAttackOnAirborneHit">Le Sanguinaire (<c>0x004b8540</c>, <c>LAB_004b8d86</c>)
    /// ajoute un <c>and al,0FDh</c> a la sortie du heurt en l'air : le bit d'attaque tombe avec la
    /// chute. Une instruction, et lui seul de la famille la porte — d'ou le parametre plutot qu'un
    /// second corps.</param>
    /// <param name="keepWalkingSpecies">Le Loup (<c>0x004cc650</c>, <c>0x004ccd93</c>) ne garde sa
    /// marche en cours — la posture 7 deja prise — que si l'octet d'espece <c>+0x0c</c> vaut ce
    /// nombre, 11 : <c>cmp cl,7 ; jne ; cmp byte [esi+0Ch],0Bh ; je</c>. Toute autre espece sur ce
    /// corps retire sa marche a chaque heurt. <c>-1</c> (le defaut) : le test d'espece n'existe
    /// pas.</param>
    private static void SettleSceneEntityOnGroundAndWalls(
        byte* e, int boldness, uint contactMask, SceneEntityPlacement* keep,
        bool dropAttackOnAirborneHit = false, int keepWalkingSpecies = -1)
    {
        uint contact = *(uint*)(e + 0x90) & 0xfffffcff;
        *(uint*)(e + 0x90) = contact;

        int floor = *(int*)(e + 0x60);

        if (*(int*)(e + 0x20) <= floor && (contact & 0x8000) == 0)
        {
            uint landing = *(uint*)e;

            *(int*)(e + 0x20) = floor;
            *(uint*)e = landing & 0xfffffffe;
            *(int*)(e + 0x54) = 0;

            // Ghidra: 0x004a26e4 — le seul chemin qui saute par-dessus tout le reste.
            if ((landing & 1) != 0) return;
        }

        uint flags = *(uint*)e;
        if ((flags & 1) == 0 && *(int*)(e + 0x20) > floor) *(uint*)e = flags | 1;

        if ((contact & contactMask) == 0) return;

        // ================================================================ heurtee
        if ((*(uint*)e & 1) != 0)
        {
            // Ghidra: LAB_004a27e4 — heurtee EN L'AIR : arret net, retour au sol, et 0x28 images
            // d'attente. C'est la seule sortie qui reecrit +0x2c.
            if (dropAttackOnAirborneHit) *(uint*)e &= 0xfffffffd;   // Ghidra: LAB_004b8d86

            *(int*)(e + 0x5c) = 0;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x58) = 0;
            *(int*)(e + 0x1c) = keep->X;
            *(int*)(e + 0x24) = keep->Z;
            e[0x42] = 0;
            e[0x43] = 0x28;
            *(ushort*)(e + 0x12) = 0;
            *(ushort*)(e + 0x2c) = 0x1000;
            return;
        }

        bool keepWalking = e[0x42] == 7 && (keepWalkingSpecies < 0 || e[0x0c] == keepWalkingSpecies);

        if (!keepWalking)
        {
            e[0x42] = 7;
            *(ushort*)(e + 0x12) = 0;
            *(ushort*)(e + 0x10) = 0x50;
            e[0x43] = (byte)(CrtRand() % 0x28 + 0x14);   // Ghidra: FUN_00547140
        }

        *(uint*)e &= 0xfffffffd;

        RestoreSceneEntityPlacement(e, keep);

        int side = ProbeClearSide_004D5E60(e, contactMask);
        if (side == 0) side = (CrtRand() % 2) * 2 - 1;   // Ghidra: FUN_00547140

        *(ushort*)(e + 0x32) += (ushort)((boldness + 0x40) * side);
    }

    /// <summary>
    /// Ghidra: <c>0x004a283a..0x004a2bb7</c> — <b>le troisieme temps</b> du Varan.
    ///
    /// <para>
    /// La distance et le cap sont recalcules ici <b>a deux dimensions</b>, comme chez toutes ses
    /// sœurs : la hauteur ne fait pas renoncer a une poursuite.
    /// </para>
    /// <para>
    /// <b>Trois plages, dont deux se recouvrent.</b> Sous <c>0xfa</c> le corps a corps ; de
    /// <c>0xfa</c> a <c>0x2bb</c> l'approche ; au-dela de <c>0x258</c> le lointain. Entre
    /// <c>0x258</c> et <c>0x2bb</c> les deux dernieres s'executent l'une apres l'autre, et la
    /// seconde ecrase la posture que la premiere venait de choisir — mais pas <c>+0x10</c> ni
    /// <c>+0x12</c>, qu'elle laisse derriere elle. Trois <c>if</c> successifs, donc, et non une
    /// cascade.
    /// </para>
    /// <para>
    /// <b>Le cone du corps a corps s'ouvre avec la TIMIDITE</b> : <c>timidite * 8 + 0x200</c>,
    /// centre sur <c>timidite * 4</c>. Un Varan hardi doit etre presque en face pour mordre, un
    /// Varan craintif mord de biais. Et la portee elle-meme est tiree —
    /// <c>0x64 + hasard % (timidite * 2 + 1)</c> —, ce qui fait de la distance de morsure une
    /// grandeur aleatoire, seule du depot dans ce cas.
    /// </para>
    /// </summary>
    private static void ReplanVaranAction_004A1D50(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        *(ushort*)(e + 0x12) = 0;

        // ================================================================ le corps a corps
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;                  // Ghidra: FUN_00547140

            if (roll < 0x1e)
            {
                bool inCone = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;
                bool inReach = inCone && distance < CrtRand() % (timid * 2 + 1) + 0x64;

                if (!inReach)
                {
                    HesitateSceneEntity(e);
                }
                else if (roll < 0x14)
                {
                    // Ghidra: LAB_004a28ea — armer LA MORSURE.
                    uint flags = *(uint*)e;
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (flags & 0xfeffffff) | 2;
                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                }
                else
                {
                    // Ghidra: LAB_004a290e — tourner sur place en fonçant.
                    e[0x42] = 9;
                    e[0x43] = 1;
                    *(ushort*)(e + 0x12) = 0;
                }
            }
            else if (roll < 0x23)
            {
                // Ghidra: 0x004a292f — l'incantation, une fois sur vingt au corps a corps.
                e[0x42] = 0x0c;
                e[0x43] = 1;
                *(ushort*)(e + 0x12) = 0;
            }
            else if (roll >= 0x3c && roll < 0x46 && distance > boldness + 0x64)
            {
                // Ghidra: LAB_004a2b59 — le bond, et la seule sortie du corps a corps qui exige une
                // distance MINIMALE : il faut du recul pour sauter.
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();   // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
            }
            else
            {
                // Ghidra: LAB_004a2b8c — se rapprocher.
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = SceneEntityDelay(timid);
            }
        }

        // ================================================================ l'approche
        if (distance >= 0xfa && distance < 0x2bc)
        {
            int roll = CrtRand() % 100;                  // Ghidra: FUN_00547140

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    HesitateSceneEntity(e);
                }
                else
                {
                    e[0x42] = 7;
                    *(ushort*)(e + 0x12) = 0;
                    e[0x43] = (byte)(CrtRand() % 0x14 + 0x14);   // Ghidra: FUN_00547140
                }
            }
            else if (roll >= 0x28 && roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();  // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
            }
            else if (roll >= 0x3c && roll < 0x50)
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);   // Ghidra: FUN_00547140
            }
            else
            {
                // Ghidra: LAB_004a2a0f — les deux plages 0x14..0x27 et 0x50..0x63 y tombent.
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = SceneEntityDelay(timid);
            }
        }

        // ================================================================ le lointain
        if (distance < 0x258) return;

        int far = CrtRand() % 100;                       // Ghidra: FUN_00547140

        if (far < 0x1e)
        {
            // La hardiesse partage cette plage : un Varan hardi se rapproche, un Varan timide
            // s'arrete et attend.
            if (far < boldness * 2 + 0x0a)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x64;
                e[0x43] = SceneEntityDelay(timid);
                return;
            }

            // Ghidra: LAB_004a2a8c — le repos, avec une portee d'attente qui suit la hardiesse et
            // un rebours PLUS COURT que partout ailleurs : timidite / 4, sans les 0x14.
            e[0x42] = 0;
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + (byte)(timid >> 2));   // Ghidra: FUN_00547140
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x28)
        {
            e[0x42] = 3;
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = SceneEntityDelay(timid);
            *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);       // Ghidra: FUN_00547140
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        if (far < 0x50)
        {
            // Ghidra: LAB_004a2b21 — LA CHARGE, et le seul site qui l'arme. Il ne pose pas le bit
            // de geste : voir VaranSeizeAndThrow_004A1FC7.
            e[0x42] = 8;
            e[0x43] = 1;
            *(ushort*)(e + 0x12) = 0;
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();                      // Ghidra: FUN_00547140
    }
}
