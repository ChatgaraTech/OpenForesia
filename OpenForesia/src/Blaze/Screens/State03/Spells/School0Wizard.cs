// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ECOLE 0 — LE SORCIER. Les 29 fiches, une par une, de Balle de feu a Fusion.
//
// Indices 0 a 28, sous-types 0x00 a 0x1c. Le compte de 29 est confirme par `DAT_00564600` dans le
// binaire, ce qui est aussi ce qui valide la lecture des longueurs de liste (chaque liste finit ou
// commence la suivante).
//
// Chaque fiche a son bandeau `// ====` : c'est le sommaire de ce fichier. Un sort n'est pas une
// fonction mais une famille de corps - l'entite de lancement, ses enfants, ses images -, et
// plusieurs fiches voisines partagent un corps a une planche pres, ce qui est signale sur place.
//
// Le repartiteur commun, le ciblage et la naissance de l'entite de lancement sont dans
// `SpellCasting.cs`. Ce fichier ne contient que des feuilles : rien ici n'est appele ailleurs que
// par le repartiteur d'entite.
//
// Vue d'ensemble : Documentation/Reference/Sorts.md ; feuille de route : Documentation/Etat/SortsAtteignables.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // BALLE DE FEU — le premier sort, ecole 0 indice 0, sous-type 0x00
    //
    // Deux corps, et c'est la forme generale d'un sort de projectile :
    //
    //   0x0047ADD0   LA GRAINE. Sous-type 0 de la table statique 0x00551c30, celle que la passe
    //                FUN_0047aa70 emprunte pour la table des effets 0x005ff400 — c'est-a-dire
    //                exactement ce que CastSpell_0053A980 fait naitre. Elle ne vit qu'UNE image :
    //                elle se pose devant le lanceur, se donne un modele et une taille, lache le
    //                projectile, joue le son, et s'eteint.
    //   0x0047AE80   LE PROJECTILE. Sous-type 0x64 de la table statique 0x00551f30, celle de la
    //                table d'incantation 0x005edee0 ou SpawnCastEntity_0047AAE0 depose. Il avance,
    //                traine des etincelles, cherche une cible douze images durant, et au contact
    //                appelle ResolveSpellHit_0053A2B0.
    //
    // Les deux sont ABSENTES de Blaze.c — Ghidra ne les a pas decompilees, elles n'existent que
    // comme entrees de table. Transcrites au desassembleur ; les releves sont gardes sous
    // Legacy/Raw/Blaze/FUN_0047add0.asm et FUN_0047AE80.asm.
    //
    // LE QUART DE TOUR. La graine avance de 80 unites dans la direction +0x32 heritee du lanceur,
    // puis AJOUTE 0x400 a cette direction avant de lacher le projectile ; le projectile, lui,
    // RETRANCHE 0x400 avant d'avancer. Les deux se compensent exactement, et le projectile part
    // donc bien droit devant. Le detour sert au dessin, qui lit +0x32 tel quel.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047add0 @ 0x0047add0 — <b>la graine de Balle de feu</b> : le sous-type
    /// <c>0x00</c>, creneau 0 de <see cref="s_StaticEntityHandlers_00551c30"/>. 161 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Elle ne tourne qu'une image. Le mot d'etat est mis a zero en sortie, ce qui rend le creneau
    /// a la passe suivante — la meme extinction que <see cref="DespawnEntity_0047ADC0"/>, mais
    /// apres avoir fait son travail.
    /// </para>
    /// <para>
    /// <b>Elle se place a hauteur de tete du LANCEUR</b>, pas au sol :
    /// <see cref="StatusBubbleHeight_00538BC0"/> mesure <c>+0x74</c> — que
    /// <see cref="CastSpell_0053A980"/> a pointe sur le lanceur lui-meme juste avant la naissance —
    /// et la hauteur, arrondie au pair, monte en <c>+0x20</c> decalee de 11 bits. C'est la meme
    /// mesure et le meme arrondi que <see cref="TickCastAura_004765D0"/>.
    /// </para>
    /// <para>
    /// Les quatre champs poses avant la naissance du projectile sont recopies dedans par
    /// <see cref="SpawnCastEntity_0047AAE0"/>, qui duplique les <c>0x27</c> dwords de la source :
    /// le modele <c>0x39</c>, l'echelle <c>0x640</c> sur les trois axes, la teinte pleine
    /// <c>0xffffff</c>. La graine s'habille pour son enfant.
    /// </para>
    /// </summary>
    private static void SpawnFireball_0047ADD0(byte* record)
    {
        byte* e = record;

        e[0x36] |= 6;

        int heading = *(short*)(e + 0x32);

        // 80 unites devant le lanceur, dans le plan. `lea eax,[eax+eax*4] ; shl eax,4` = *80.
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 80;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 80;

        // La hauteur de tete du lanceur, arrondie au pair : `and al,0FEh` n'efface que le bit 0.
        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
        *(int*)(e + 0x20) += (height & ~1) << 0xb;

        *(ushort*)(e + 0x32) += 0x400;

        *(uint*)(e + 0x28) = 0x06400640;              // echelle X et Y
        *(ushort*)(e + 0x2c) = 0x0640;                // echelle Z
        *(uint*)(e + 0x38) = 0x00ffffff;              // teinte pleine
        e[0x04] = 0x39;                               // le modele

        SpawnCastEntity_0047AAE0(e, 0x64, e[0x0b], -1);

        Aud.PlayPositionalSound(0x1f, 0x6fd, 0x1200, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20

        *(uint*)e = 0;                                // la graine s'eteint des cette image
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551ad8</c> — l'atlas des etincelles que le projectile de Balle de feu
    /// traine derriere lui. <b>Quatre enregistrements</b> : quatre carreaux de 16x16 alignes en
    /// <c>u = 0x60, 0x70, 0x80, 0x90</c> sur la meme ligne <c>v = 0xa8</c>.
    ///
    /// <para>
    /// <b>La borne se lit dans le corps du consommateur</b>, comme pour
    /// <see cref="s_Descriptors_00551A60"/> et non a l'aveugle :
    /// <see cref="TickSpark_0047B050"/> pose <c>+0x40 = compteur &gt;&gt; 1</c> et s'eteint des que
    /// ce quotient atteint <b>4</b>. Il n'existe donc que quatre images. La donnee le confirme —
    /// <c>0x00551af8</c> change de planche (<c>v</c> passe a <c>0x68</c>, les attributs de
    /// <c>01 02</c> a <c>00 00</c>).
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551AD8 =
    [
        0x60, 0xa8, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551ad8
        0x70, 0xa8, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551ae0
        0x80, 0xa8, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551ae8
        0x90, 0xa8, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551af0
    ];

    private static byte* s_Descriptors00551AD8Storage;

    /// <summary>Les quatre descripteurs en memoire native.</summary>
    private static byte* Descriptors00551AD8()
        => NativeCopy(s_Descriptors_00551AD8, ref s_Descriptors00551AD8Storage);

    /// <summary>
    /// <c>&amp;DAT_00551ba8</c>, l'atlas de la <b>Boule de lumiere</b> — le <b>deuxieme</b>
    /// enregistrement de la plage <see cref="s_Descriptors_00551BA0"/>, pas une table a lui.
    ///
    /// <para>
    /// <b>Il avait ete releve a part le 2026-08-31, et c'etait la faute que le depot s'etait deja
    /// faite une fois</b> : les deux tuiles de <c>0x00551ba8</c> et <c>0x00551bb0</c> etaient
    /// recopiees dans un tableau neuf alors que <c>s_Descriptors_00551BA0</c> les portait deja.
    /// Deux relevés du meme octet finissent par diverger, et surtout l'indexation en avant — la
    /// convention des fonctions de dessin — n'aurait plus rien trouve au-dela du second.
    /// Adresser par decalage dans la plage est la seule forme juste.
    /// </para>
    /// <para>
    /// <see cref="TickLightBall_0047B150"/> ne touche pas a son propre <c>+0x40</c>, que
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> a mis a zero : la boule garde donc la tuile
    /// <c>0x18 x 0x18</c> de <c>0x00551ba8</c> toute sa vie.
    /// </para>
    /// </summary>
    private static byte* Descriptors00551BA8() => Descriptors00551BA0() + 8;

    /// <summary>
    /// <c>&amp;DAT_00551bd8</c>, l'atlas de <b>la boule des sorts de monstre</b> — le
    /// <b>huitieme</b> et dernier enregistrement releve de la plage
    /// <see cref="s_Descriptors_00551BA0"/>, adresse par decalage comme
    /// <see cref="Descriptors00551BA8"/> et <see cref="Descriptors00551BC0"/>.
    ///
    /// <para>
    /// Son seul emploi est <see cref="TickMonsterOrbCast_004769A0"/>. C'est aussi <b>le premier
    /// appelant connu du huitieme enregistrement</b> : la note portee sur la plage disait que huit
    /// etaient relevés « parce que huit sont lisibles, pas parce qu'une borne les designe », et
    /// celui-ci en avait donc un.
    /// </para>
    /// </summary>
    private static byte* Descriptors00551BD8() => Descriptors00551BA0() + 0x38;

    /// <summary>
    /// <c>&amp;DAT_00551bb8</c>, l'atlas de l'<b>eclaboussure</b> de Pieds mouilles — le
    /// <b>quatrieme</b> enregistrement de la meme plage
    /// <see cref="s_Descriptors_00551BA0"/>. <see cref="TickWetFeetSplash_0047B750"/> ne touche pas
    /// non plus a son <c>+0x40</c> : une seule tuile, <c>0x10 x 0x10</c>.
    /// </summary>
    private static byte* Descriptors00551BB8() => Descriptors00551BA0() + 0x18;

    /// <summary>
    /// Ghidra: FUN_0047ae80 @ 0x0047ae80 — <b>le projectile de Balle de feu</b> : le sous-type
    /// <c>0x64</c>, creneau 100 de <see cref="s_StaticEntityHandlers_00551f30"/>. 455 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Chaque image, dans cet ordre</b> : il avance de 40 unites sur son cap (moins le quart de
    /// tour que la graine avait ajoute) ; il lache une etincelle <c>0x46</c> <b>derriere lui</b> —
    /// le cap plus un demi-tour, a 60 unites, plus un bruit de +/-32 unites de 4096 sur les trois
    /// axes, trois tirages consecutifs ; il repose son placement par
    /// <see cref="FinalizeEntityPlacement2"/> ; il cherche une cible.
    /// </para>
    /// <para>
    /// <b>La hauteur est sauvee et rendue autour du placement.</b>
    /// <c>FinalizeEntityPlacement2</c> pose l'entite <i>sur le sol</i>, ce qui ecraserait
    /// <c>+0x20</c> ; le binaire garde la valeur dans un registre et la remet juste apres. La
    /// balle vole donc a la hauteur ou la graine l'a lachee, et le placement ne sert qu'a la
    /// detection.
    /// </para>
    /// <para>
    /// <b>Trois fins</b>. Le decor : le mot d'etat porte <c>0x40000000</c>, ou <c>+0x90</c> porte
    /// l'un des bits <c>0x8802</c> — la balle s'eteint sans rien faire. Le temps : douze images
    /// sans cible, comptees dans <c>+0x10</c>. La cible : <c>ProbeMeleeTarget_00474CA0</c> rend une
    /// victime, et alors <see cref="ResolveSpellHit_0053A2B0"/> resout le coup avec la paire
    /// famille/indice que <see cref="CastSpell_0053A980"/> avait rangee en <c>+0x06</c>, la victime
    /// est <b>poussee</b> de 16 unites sur le cap de la balle, marquee <c>0x40</c> et
    /// <c>+0x96 = 0x10</c>, et un second son part.
    /// </para>
    /// <para>
    /// La sonde de portee est la meme forme que celle de <see cref="PickTargetBySearch_00474AD0"/>
    /// et de la melee : six mots — decalage de cap, avance, demi-largeur, tolerance en hauteur,
    /// ouverture, profondeur.
    /// </para>
    /// </summary>
    private static void TickFireball_0047AE80(byte* record)
    {
        byte* e = record;

        // Ghidra: les six mots de la sonde, empiles sur place. -1024 de decalage de cap, -100
        // d'avance, 50 de demi-largeur, 100 de tolerance en hauteur, aucune ouverture, 100 de
        // profondeur. Les trois ints qui suivent recoivent la position de l'etincelle : le binaire
        // n'a qu'un seul bloc de 0x18 octets sur sa pile, et il s'en sert des deux façons.
        short* probe = stackalloc short[6] { -0x0400, -0x0064, 0x0032, 0x0064, 0x0000, 0x0064 };
        int* sparkAt = stackalloc int[3];

        if (*(ushort*)(e + 0x10) == 0) *(uint*)e |= 0x80;   // la premiere image

        // Le cap, moins le quart de tour que la graine avait ajoute.
        int heading = *(short*)(e + 0x32) - 0x400;

        // 40 unites d'avance : `lea eax,[eax+eax*4] ; shl eax,3`.
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 40;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 40;

        // L'etincelle part DERRIERE, a 60 unites (`*3` puis `*5` puis `*4`), et chaque axe recoit
        // un bruit de (rand & 0x3f) - 0x20 quarts d'unite. L'ordre des trois tirages est celui du
        // binaire — ils partagent une seule suite pseudo-aleatoire.
        int behind = heading + 0x800;

        int back = (int)Winsys3d.GetSinTableEntry(behind) * 60;
        sparkAt[0] = *(int*)(e + 0x1c) + back + (((CrtRand() & 0x3f) - 0x20) << 0xc);

        sparkAt[1] = *(int*)(e + 0x20) + (((CrtRand() & 0x3f) - 0x20) << 0xc);

        back = Winsys3d.GetCosTableEntry(behind) * 60;
        sparkAt[2] = *(int*)(e + 0x24) + back + (((CrtRand() & 0x3f) - 0x20) << 0xc);

        SpawnStatusEffectEntity_0047AC40(e, sparkAt, Descriptors00551AD8(), 0x46, e[0x0b], -1);

        // Le placement ecraserait la hauteur : elle est mise de cote et rendue tout de suite apres.
        int keptHeight = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);               // Ghidra: FUN_00533940
        uint state = *(uint*)e;
        *(int*)(e + 0x20) = keptHeight;

        if ((state & 0x40000000) != 0 || (*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            *(uint*)e = 0;                                // le decor : elle s'eteint sur place
            return;
        }

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim == null)
        {
            *(ushort*)(e + 0x10) += 1;
            if (*(short*)(e + 0x10) < 0x0c) return;       // douze images de vol

            *(uint*)e = 0;
            return;
        }

        // ------------------------------------------------------------------ le coup
        ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

        // La poussee : 16 unites sur le cap de la balle, celui-la NON corrige du quart de tour.
        int push = *(short*)(e + 0x32);
        *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
        *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;

        *(uint*)victim |= 0x40;
        *(ushort*)(victim + 0x96) = 0x10;

        Aud.PlayPositionalSound(0x13, 0x653, 0x1200, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047b050 @ 0x0047b050 — <b>l'etincelle</b> que le projectile de Balle de feu
    /// laisse derriere lui : le sous-type <c>0x46</c> de
    /// <see cref="s_StaticEntityHandlers_00552230"/>, la table de la passe <c>FUN_0047acf0</c> sur
    /// <c>0x005f2d40</c>. 91 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le corps le plus court de la chaine, et le seul qui compte des images.</b> Le compteur
    /// <c>+0x10</c> monte d'une unite par image ; l'image d'atlas <c>+0x40</c> est sa <b>moitie</b>,
    /// si bien que chacun des quatre carreaux de <see cref="s_Descriptors_00551AD8"/> tient deux
    /// images. A la huitieme, le quotient atteint 4 et le mot d'etat passe a zero : l'etincelle
    /// vit <b>huit images</b>, ni plus ni moins.
    /// </para>
    /// <para>
    /// Pendant ce temps elle <b>monte</b> de <c>0xa000</c> par image, <b>retrecit</b> de
    /// <c>0x100</c> sur X et Y, et <b>palit</b> de <c>0x18</c> sur les trois canaux. Les deux
    /// dernieres sont des additions de <b>dword</b> dans l'original (<c>0xfeffff00</c> et
    /// <c>0xffe7e7e8</c>, soit <c>-0x01000100</c> et <c>-0x00181818</c>) : deux shorts et trois
    /// octets traites d'un seul coup, retenue comprise. C'est transcrit tel quel — la retenue fait
    /// partie du comportement, pas de l'astuce d'ecriture.
    /// </para>
    /// <para>
    /// L'echelle et la teinte ne sont posees qu'a la <b>premiere</b> image (<c>+0x10 == 0</c>) :
    /// <c>0x1800</c> sur X et Y, blanc plein. <see cref="SpawnStatusEffectEntity_0047AC40"/> a mis
    /// <c>+0x10</c> et <c>+0x40</c> a zero a la naissance, donc la branche est prise une fois.
    /// </para>
    /// </summary>
    private static void TickSpark_0047B050(byte* record)
    {
        byte* e = record;

        ushort counter = *(ushort*)(e + 0x10);

        if (counter == 0)
        {
            *(uint*)(e + 0x28) = 0x18001800;          // echelle X et Y
            *(uint*)(e + 0x38) = 0x00ffffff;          // teinte pleine
        }
        else
        {
            // Deux shorts et trois octets, en une addition de dword chacune.
            unchecked
            {
                *(uint*)(e + 0x28) += 0xfeffff00;     // -0x100 sur X et sur Y
                *(uint*)(e + 0x38) += 0xffe7e7e8;     // -0x18 sur les trois canaux
            }
        }

        *(int*)(e + 0x20) += 0xa000;                  // elle monte

        // `mov cx,dx ; sar cx,1` : la moitie, en SIGNE et sur seize bits. `cmp cl,4 ; jb` compare
        // ensuite l'octet BAS, non signe.
        short half = (short)((short)counter >> 1);
        e[0x40] = (byte)half;

        if ((byte)half >= 4) *(uint*)e = 0;

        *(ushort*)(e + 0x10) = (ushort)(counter + 1);
    }

    // =====================================================================
    // BOULE DE LUMIERE — le deuxieme sort, ecole 0 indice 1, sous-type 0x01
    //
    // Quatre corps, tous absents de Blaze.c. La forme est celle de Balle de feu, avec UNE
    // difference de fond : la boule ne meurt pas au contact.
    //
    //   0x0047B0B0   LA GRAINE. Sous-type 1 de 0x00551c30. Une image, comme celle du feu, mais
    //                elle avance de 40 et non de 80, ne se donne pas de modele, et fait naitre son
    //                enfant dans la table 0x005f2d40 par SpawnStatusEffectEntity_0047AC40 — pas
    //                dans celle des incantations. Il n'y a donc pas de projectile au sens du feu.
    //   0x0047B150   LA BOULE. Sous-type 0x47 de 0x00552230. Elle avance, seme deux sortes
    //                d'entites, cherche une cible — et CONTINUE apres l'avoir touchee. Douze
    //                images de vie quoi qu'il arrive.
    //   0x0047B340   L'ETINCELLE semee a chaque image (sous-type 0x48). Six images, trois tuiles
    //                en anneau.
    //   0x0047B3A0   LE HALO accroche a l'ancre (sous-type 0x49). Vingt images, il se recolle a
    //                chaque image sur l'entite de +0x78 et tire son image au hasard sur trois.
    //
    // CE QUI DISTINGUE CE SORT DU PRECEDENT. Balle de feu s'eteint des qu'elle touche
    // (`mov dword [esi],0` dans la branche du coup) ; ici la branche du coup RETOMBE sur le
    // vieillissement commun. Elle traverse donc, et peut frapper a chaque image de ses douze.
    // C'est ecrit tel quel : le binaire n'a aucune garde contre le coup repete.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047b0b0 @ 0x0047b0b0 — <b>la graine de Boule de lumiere</b> : le sous-type
    /// <c>0x01</c>, creneau 1 de <see cref="s_StaticEntityHandlers_00551c30"/>. 159 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Meme geste que <see cref="SpawnFireball_0047ADD0"/> — une image, se placer devant le lanceur
    /// a hauteur de tete, lacher l'enfant, jouer le son, s'eteindre — a quatre details pres :
    /// l'avance est de <b>40</b> et non 80, il n'y a <b>ni modele ni quart de tour</b>, l'echelle
    /// est <c>0x1400</c>, et l'enfant nait par
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> dans <c>0x005f2d40</c> et non par
    /// <see cref="SpawnCastEntity_0047AAE0"/> dans <c>0x005edee0</c>.
    /// </para>
    /// <para>
    /// <c>+0x36</c> est traite en <b>mot</b> (<c>&amp; 0xfffb | 2</c>) la ou la graine du feu se
    /// contentait d'un <c>|= 6</c> sur l'octet : ici le bit <c>4</c> est explicitement <b>efface</b>.
    /// </para>
    /// </summary>
    private static void SpawnLightBall_0047B0B0(byte* record)
    {
        byte* e = record;

        // `mov ax,[esi+36h] ; and al,0FBh ; or al,2 ; mov [esi+36h],ax` — l'octet haut est garde.
        *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & 0xfffb) | 2);

        int heading = *(short*)(e + 0x32);

        // 40 unites devant le lanceur : `lea eax,[eax+eax*4] ; shl ecx,3`.
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 40;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 40;

        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
        *(int*)(e + 0x20) += (height & ~1) << 0xb;

        *(uint*)(e + 0x28) = 0x14001400;              // echelle X et Y
        *(uint*)(e + 0x38) = 0x00ffffff;              // teinte pleine

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551BA8(), 0x47, e[0x0b], -1);

        Aud.PlayPositionalSound(0x14, 0x653, 0x1200, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20

        *(uint*)e = 0;                                // la graine s'eteint des cette image
    }

    /// <summary>
    /// Ghidra: FUN_0047b150 @ 0x0047b150 — <b>la Boule de lumiere</b> elle-meme : le sous-type
    /// <c>0x47</c>, creneau 71 de <see cref="s_StaticEntityHandlers_00552230"/>. 492 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>A la premiere image</b> elle pose <c>0x80</c> sur son mot d'etat, se designe elle-meme en
    /// <c>+0x78</c> — le champ « cible trouvee », qui sert ici d'<b>ancre</b> pour le halo —, prend
    /// l'echelle <c>0x1000</c> et le blanc plein, et lache un premier halo <c>0x49</c>.
    /// </para>
    /// <para>
    /// <b>A chaque image</b> : 40 unites d'avance sur <c>+0x32</c> — <b>sans</b> la correction de
    /// quart de tour du projectile de feu, sa graine n'en ayant pas ajoute —, une etincelle
    /// <c>0x48</c> semee a la position bruitee de +/-32 unites de 4096 sur les trois axes, le
    /// replacement, puis la recherche de cible.
    /// </para>
    /// <para>
    /// <b>Le coup ne l'arrete pas.</b> La branche du contact resout les degats, <b>rattache le halo
    /// a la victime</b> (<c>+0x78</c> change d'ancre, et un second halo nait), pousse et marque la
    /// victime — puis <b>retombe sur le vieillissement commun</b>. Seules les douze images, le
    /// decor (<c>0x40000000</c>) ou <c>+0x90</c> sur le masque <c>0x8802</c> la tuent.
    /// </para>
    /// <para>
    /// <b>La poussee emploie <c>+0x32 - 0x400</c></b> alors que l'avance emploie <c>+0x32</c> nu.
    /// L'asymetrie est dans le binaire ; elle n'est pas expliquee ici, elle est transcrite.
    /// </para>
    /// </summary>
    private static void TickLightBall_0047B150(byte* record)
    {
        byte* e = record;

        // Meme forme que le projectile de feu : six mots de sonde, puis trois ints de position
        // dans le meme bloc de 0x18 octets. Aucun decalage de cap ici, et la moitie de l'avance.
        short* probe = stackalloc short[6] { 0x0000, -0x0032, 0x0032, 0x0064, 0x0000, 0x0064 };
        int* sparkAt = stackalloc int[3];

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x80;

            // Elle est sa propre ancre tant qu'elle n'a rien touche.
            *(byte**)(e + 0x78) = e;

            *(uint*)(e + 0x28) = 0x10001000;
            *(uint*)(e + 0x38) = 0x00ffffff;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551A60(), 0x49, e[0x0b], -1);
        }

        // Ghidra: LAB_0047b1c1 — le cap, pris tel quel.
        int heading = *(short*)(e + 0x32);

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 40;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 40;

        // L'etincelle nait sur place, seulement bruitee. L'ordre des trois tirages est celui du
        // binaire — ils partagent une seule suite pseudo-aleatoire.
        sparkAt[0] = *(int*)(e + 0x1c) + (((CrtRand() & 0x3f) - 0x20) << 0xc);
        sparkAt[1] = *(int*)(e + 0x20) + (((CrtRand() & 0x3f) - 0x20) << 0xc);
        sparkAt[2] = *(int*)(e + 0x24) + (((CrtRand() & 0x3f) - 0x20) << 0xc);

        SpawnStatusEffectEntity_0047AC40(e, sparkAt, Descriptors005519A0(), 0x48, e[0x0b], -1);

        // Le placement ecraserait la hauteur : elle est mise de cote et rendue tout de suite apres.
        int keptHeight = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);               // Ghidra: FUN_00533940
        uint state = *(uint*)e;
        *(int*)(e + 0x20) = keptHeight;

        if ((state & 0x40000000) != 0 || (*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            *(uint*)e = 0;                                // le decor : elle s'eteint sur place
            return;
        }

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            // Le halo change d'ancre : il suivra desormais la victime.
            *(byte**)(e + 0x78) = victim;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551A60(), 0x49, e[0x0b], -1);

            int push = *(short*)(e + 0x32) - 0x400;
            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;

            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;

            Aud.PlayPositionalSound(0x14, 0x800, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047b319 — le vieillissement, commun aux deux branches. C'est LA difference
        // avec Balle de feu : le coup n'eteint pas la boule, elle traverse.
        *(ushort*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) < 0x0c) return;

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047b340 @ 0x0047b340 — <b>l'etincelle de Boule de lumiere</b> : le sous-type
    /// <c>0x48</c>, creneau 72 de <see cref="s_StaticEntityHandlers_00552230"/>. 88 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// La sœur de <see cref="TickSpark_0047B050"/>, a trois differences : elle vit <b>six</b>
    /// images et non huit, elle palit deux fois plus vite (<c>-0x20</c> par canal contre
    /// <c>-0x18</c>), et son image d'atlas <b>tourne en anneau</b> sur les trois tuiles de
    /// <see cref="s_Descriptors_005519A0"/> au lieu d'etre la moitie du compteur. C'est ce
    /// bouclage a 3 qui borne cet atlas, et il le borne deja pour un autre consommateur.
    /// </para>
    /// </summary>
    private static void TickLightSparkle_0047B340(byte* record)
    {
        byte* e = record;

        ushort counter = *(ushort*)(e + 0x10);

        if (counter == 0)
        {
            *(uint*)(e + 0x28) = 0x10001000;          // echelle X et Y
            *(uint*)(e + 0x38) = 0x00ffffff;          // teinte pleine
        }
        else
        {
            // Deux shorts et trois octets, en une addition de dword chacune.
            unchecked
            {
                *(uint*)(e + 0x28) += 0xfeffff00;     // -0x100 sur X et sur Y
                *(uint*)(e + 0x38) += 0xffdfdfe0;     // -0x20 sur les trois canaux
            }
        }

        // `inc dl ; cmp dl,3 ; mov [eax+40h],dl ; jb ; mov byte [eax+40h],0` — l'ecriture precede
        // la remise a zero, ce qui revient a un anneau de trois.
        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame < 3 ? frame : (byte)0;

        counter = (ushort)(counter + 1);
        *(ushort*)(e + 0x10) = counter;

        if ((short)counter >= 6) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047b3a0 @ 0x0047b3a0 — <b>le halo de Boule de lumiere</b> : le sous-type
    /// <c>0x49</c>, creneau 73 de <see cref="s_StaticEntityHandlers_00552230"/>. 191 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne se deplace pas, il se recolle.</b> A chaque image il reprend la position de l'entite
    /// que <c>+0x78</c> designe — la boule d'abord, la victime ensuite — bruitee de +/-32 unites de
    /// 4096 sur X et Z, et decalee en hauteur de la tete de l'ancre. Son image d'atlas est
    /// <b>tiree au hasard sur trois</b> a chaque image, ce qui borne
    /// <see cref="s_Descriptors_00551A60"/> exactement comme <c>FUN_00475910</c> le borne deja.
    /// </para>
    /// <para>
    /// <b>La taille est celle de l'ancre</b>, mesuree une seule fois : <c>(hauteur &lt;&lt; 12) /
    /// 150</c>, la meme division par multiplication magique <c>0x1B4E81B5</c> que
    /// <see cref="TickCastAura_004765D0"/> emploie. Le calcul est saute quand l'ancre porte
    /// <c>0x100000</c> — le bit du modele de paquet —, auquel cas <c>+0x12</c> reste nul et le halo
    /// colle au sol.
    /// </para>
    /// <para>
    /// <b>Deux fins</b> : vingt images, ou l'ancre qui s'eteint (son bit <c>0x80000000</c> tombe).
    /// La seconde est ce qui fait disparaitre le halo avec la boule.
    /// </para>
    /// </summary>
    private static void TickLightHalo_0047B3A0(byte* record)
    {
        byte* e = record;
        byte* anchor = *(byte**)(e + 0x78);

        if (*(ushort*)(e + 0x10) == 0 && (*(uint*)anchor & 0x100000) == 0)
        {
            short height = (short)StatusBubbleHeight_00538BC0((uint*)anchor);
            *(short*)(e + 0x12) = height;

            // `imul 0x1B4E81B5 ; sar edx,4` et la correction de signe : une division par 150.
            int scale = (height << 0xc) / 150;
            *(uint*)(e + 0x28) = ((uint)scale << 0x10) | (uint)scale;
        }

        // Ghidra: LAB_0047b3e6 — le recollage, hors de toute branche d'etat.
        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c) + (((CrtRand() & 0x3f) - 0x20) << 0xc);

        // La hauteur n'est pas bruitee comme les deux autres axes : `lea eax,[edx+eax*2-40h]` la
        // melange au tirage AVANT le decalage de 11 bits, l'arrondi au pair compris.
        int lift = (*(short*)(e + 0x12) & ~1) + (CrtRand() & 0x3f) * 2 - 0x40;
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + (lift << 0xb);

        *(int*)(e + 0x24) = *(int*)(anchor + 0x24) + (((CrtRand() & 0x3f) - 0x20) << 0xc);

        *(ushort*)(e + 0x10) += 1;

        if (*(short*)(e + 0x10) >= 0x14 || (*(uint*)anchor & 0x80000000) == 0)
        {
            *(uint*)e = 0;
            return;
        }

        e[0x40] = (byte)(CrtRand() % 3);              // `cdq ; idiv 3`, le reste
    }

    // =====================================================================
    // PIEDS MOUILLES — le troisieme sort, ecole 0 indice 2, sous-type 0x02
    //
    // Trois corps, et une forme NOUVELLE : il n'y a pas de graine. Le sous-type 2 de la table des
    // effets EST le sort — il se deplace, rebondit et frappe lui-meme, la ou les deux premiers
    // sorts naissaient d'une graine d'une seule image.
    //
    //   0x0047B460   LA VAGUE. Sous-type 2 de 0x00551c30. Vingt images. Elle avance de 20 par
    //                image, TOMBE (une vitesse verticale en +0x54 que la gravite ronge), colle au
    //                sol quand elle le touche, seme deux enfants a chaque image et frappe.
    //   0x0047B6B0   LA CRETE, sous-type 0x66 de 0x00551f30, semee par SpawnCastEntity_0047AAE0.
    //                Modele 0x1e, huit images, et sa hauteur suit une SINUSOIDE amortie.
    //   0x0047B750   L'ECLABOUSSURE, sous-type 0x4a de 0x00552230. Elle vole par
    //                IntegrateWithDrag_00535880 et s'eteint quand sa teinte atteint zero.
    //
    // LE PRET DES TROIS CHAMPS DE VITESSE. Avant de lacher l'eclaboussure, le corps ECRASE
    // +0x50/+0x54/+0x58 avec une impulsion tiree au hasard, appelle SpawnStatusEffectEntity_0047AC40
    // — qui recopie les 0x27 dwords, donc emporte l'impulsion — puis les REND. C'est le meme geste
    // que CastSpell_0053A980 fait sur +0x74/+0x78 : passer un argument par un champ de l'entite,
    // faute de parametre.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047b460 @ 0x0047b460 — <b>Pieds mouilles</b> : le sous-type <c>0x02</c>,
    /// creneau 2 de <see cref="s_StaticEntityHandlers_00551c30"/>. 583 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le premier sort qui n'a pas de graine.</b> Les deux precedents naissaient d'un corps
    /// d'une seule image qui lachait le vrai projectile ; ici le sous-type que
    /// <see cref="CastSpell_0053A980"/> fait naitre est deja la vague, et il vit vingt images.
    /// </para>
    /// <para>
    /// <b>Et le premier qui tombe.</b> <c>+0x54</c> porte une vitesse verticale, <c>+0x9a</c> une
    /// constante de <c>0x6000</c> reecrite a chaque image, et la mise a jour est
    /// <c>v' = v + (c &gt;&gt; 8) - (v &gt;&gt; 8) - c</c> : un frottement au 256e et une chute
    /// constante, dans la meme expression. Le sol vient de <c>+0x60</c>, que
    /// <see cref="FinalizeEntityPlacement2"/> vient de renseigner.
    /// </para>
    /// <para>
    /// <b>Le contact avec le sol se decide sur la distance parcourue</b>, pas sur la seule hauteur :
    /// si la hauteur d'avant le placement, majoree de <c>0xa000</c>, depasse encore la hauteur
    /// d'apres, la vague <b>colle au sol</b> ; sinon elle marque <c>0x800</c> dans <c>+0x90</c> —
    /// le drapeau qui la tuera a l'image suivante par le masque <c>0x802</c>. Dans les deux cas la
    /// vitesse verticale est remise a zero.
    /// </para>
    /// <para>
    /// La poussee de la victime emploie <c>+0x32</c> nu et marque <c>+0x96 = 4</c>, non
    /// <c>0x10</c> : un clignotement plus court que celui des deux premiers sorts. Il n'y a
    /// <b>pas de son au contact</b> — le seul son est celui de la premiere image.
    /// </para>
    /// </summary>
    private static void TickWetFeet_0047B460(byte* record)
    {
        byte* e = record;

        // Six mots de sonde : aucun decalage de cap, 10 en arriere, 60 de demi-largeur — deux fois
        // plus large que les deux sorts precedents, c'est une vague.
        short* probe = stackalloc short[6] { 0x0000, -0x000a, 0x003c, 0x0064, 0x0000, 0x0064 };

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x80;
            *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & 0xfffb) | 2);
            *(short*)(e + 0x2e) = -0x14;
            *(int*)(e + 0x54) = 0;                    // la vitesse verticale part de zero

            Aud.PlayPositionalSound(0x18, 0x6fd, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047b4d3 — tout ce qui suit tourne a chaque image.
        int heading = *(short*)(e + 0x32);
        *(short*)(e + 0x9a) = 0x6000;                 // reecrite a chaque image, jamais lue ailleurs

        // 20 unites d'avance : `lea eax,[eax+eax*4] ; shl eax,2`.
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 20;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 20;

        // La chute. L'ordre compte : la hauteur prend l'ANCIENNE vitesse.
        int gravity = *(short*)(e + 0x9a);
        int velocity = *(int*)(e + 0x54);
        *(int*)(e + 0x20) += velocity;
        *(int*)(e + 0x54) = velocity + (gravity >> 8) - (velocity >> 8) - gravity;

        SpawnCastEntity_0047AAE0(e, 0x66, e[0x0b], -1);

        // ---------------------------------------------------------------- l'eclaboussure
        // Les trois champs de vitesse sont PRETES a l'enfant : il en herite par la recopie des
        // 0x27 dwords, puis ils sont rendus intacts.
        int keptX = *(int*)(e + 0x50);
        int keptY = *(int*)(e + 0x54);
        int keptZ = *(int*)(e + 0x58);

        // Un cap disperse sur un quart de tour, decale d'un tiers et demi : `rand & 0x3ff`,
        // plus le cap, plus 0x600.
        int spray = (CrtRand() & 0x3ff) + *(short*)(e + 0x32) + 0x600;

        *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(spray) * 20;
        *(int*)(e + 0x54) = ((CrtRand() & 0xf) + 0x14) << 0xc;
        *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(spray) * 20;

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551BB8(), 0x4a, e[0x0b], -1);

        int beforePlacement = *(int*)(e + 0x20);

        *(int*)(e + 0x50) = keptX;
        *(int*)(e + 0x54) = keptY;
        *(int*)(e + 0x58) = keptZ;

        FinalizeEntityPlacement2((uint*)e);           // Ghidra: FUN_00533940

        // ---------------------------------------------------------------- le sol
        int height = *(int*)(e + 0x20);
        int ground = *(int*)(e + 0x60);

        if (height >= ground && (*(uint*)(e + 0x90) & 0x2200) != 0x2000)
        {
            if (beforePlacement + 0xa000 > height) *(int*)(e + 0x20) = ground;
            else *(uint*)(e + 0x90) |= 0x800;         // `or dh,8`

            *(int*)(e + 0x54) = 0;
        }

        // Ghidra: LAB_0047b60b — le masque est 0x802 ici, pas 0x8802 : le bit 0x8000 ne la tue pas.
        if ((*(uint*)e & 0x40000000) != 0 || (*(uint*)(e + 0x90) & 0x802) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            int push = *(short*)(e + 0x32);
            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
            *(ushort*)(victim + 0x96) = 4;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;
            *(uint*)victim |= 0x40;
        }

        // Ghidra: LAB_0047b687 — deux compteurs avancent, un seul borne la vie. `+0x12` est celui
        // que la crete lit pour sa sinusoide.
        *(ushort*)(e + 0x10) += 1;
        ushort age = *(ushort*)(e + 0x10);
        *(ushort*)(e + 0x12) += 1;

        if ((short)age >= 0x14) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047b6b0 @ 0x0047b6b0 — <b>la crete de Pieds mouilles</b> : le sous-type
    /// <c>0x66</c>, creneau 102 de <see cref="s_StaticEntityHandlers_00551f30"/>. 148 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Sa hauteur est une sinusoide amortie</b>, et c'est ce qui donne la vague : l'echelle Y
    /// (<c>+0x2a</c>) vaut <c>|sin(+0x12 &lt;&lt; 8)| * (0x1000 - age * 0x200) &gt;&gt; 11</c>.
    /// Le <c>+0x12</c> vient de la vague qui l'a semee — chaque crete nait donc un cran plus loin
    /// dans la sinusoide que la precedente, et l'ensemble ondule.
    /// </para>
    /// <para>
    /// <b>La valeur absolue est calculee en appelant la table DEUX FOIS.</b> Le binaire teste le
    /// signe du premier appel puis rappelle <c>Ordinal_451</c> avec le meme angle, pour nier ou non.
    /// La table est une lecture pure, donc le second appel rend exactement le premier : c'est
    /// ecrit ici comme la valeur absolue qu'il calcule, et non comme deux appels.
    /// </para>
    /// <para>
    /// Pendant ce temps l'echelle X perd <c>0x380</c> par image et la teinte <c>0x101010</c> — le
    /// blanc plein <c>0x808080</c> de la convention winsys3d, pas <c>0xffffff</c>. Huit images.
    /// </para>
    /// </summary>
    private static void TickWetFeetWave_0047B6B0(byte* record)
    {
        byte* e = record;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(uint*)(e + 0x38) = 0x00808080;          // le blanc PLEIN de winsys3d
            *(ushort*)(e + 0x28) = 0x1800;
            *(ushort*)(e + 0x2c) = 0x1800;
            e[0x04] = 0x1e;                           // le modele
        }
        else
        {
            unchecked { *(uint*)(e + 0x38) += 0xffefeff0; }   // -0x101010
        }

        // Ghidra: LAB_0047b6de — l'aplatissement et l'onde, communs aux deux branches.
        unchecked { *(ushort*)(e + 0x28) += 0xfc80; }         // -0x380

        int angle = *(short*)(e + 0x12) << 8;
        int wave = (int)Winsys3d.GetSinTableEntry(angle);
        if (wave < 0) wave = -wave;                   // le binaire rappelle la table pour nier

        ushort age = *(ushort*)(e + 0x10);
        int decay = 0x1000 - ((short)age << 9);
        *(short*)(e + 0x2a) = (short)(decay * wave >> 0xb);

        age = (ushort)(age + 1);
        *(ushort*)(e + 0x10) = age;

        if ((short)age >= 8) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047b750 @ 0x0047b750 — <b>l'eclaboussure de Pieds mouilles</b> : le sous-type
    /// <c>0x4a</c>, creneau 74 de <see cref="s_StaticEntityHandlers_00552230"/>. 66 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Le seul corps de la chaine qui ne compte pas les images : <b>c'est sa teinte qui lui tient
    /// lieu d'horloge</b>. Elle part de <c>0xf0f0f0</c> et perd <c>0x101010</c> par image ; a la
    /// seizieme le dword tombe pile a zero et l'entite s'eteint. Le test est
    /// <c>e[0x38] == 0</c> — une egalite exacte, qui ne tient que parce que les trois canaux
    /// descendent du meme pas et partent de la meme valeur.
    /// </para>
    /// <para>
    /// Son mouvement est celui que la vague lui a prete dans <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>,
    /// integre par <see cref="IntegrateWithDrag_00535880"/>.
    /// </para>
    /// </summary>
    private static void TickWetFeetSplash_0047B750(byte* record)
    {
        byte* e = record;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(uint*)(e + 0x28) = 0x20002000;
        }

        IntegrateWithDrag_00535880(e);                // Ghidra: FUN_00535880

        uint tint = *(uint*)(e + 0x38);

        if (tint == 0)
        {
            *(uint*)e = 0;
            return;
        }

        unchecked { *(uint*)(e + 0x38) = tint + 0xffefeff0; }   // -0x101010
    }

    // =====================================================================
    // ROCHER — le quatrieme sort, ecole 0 indice 3, sous-type 0x03
    //
    // Trois corps, et une forme encore nouvelle : le sort ne va nulle part, il TOURNE AUTOUR DU
    // LANCEUR et compte ses enfants.
    //
    //   0x0047B7A0   LE CERCLE. Sous-type 3 de 0x00551c30. Il se colle au lanceur, ne bouge pas,
    //                et lache un rocher toutes les quatre images pendant seize — quatre rochers.
    //                Puis il ATTEND : il ne s'eteint que lorsque ses deux compteurs tombent a zero.
    //   0x0047B850   LE ROCHER, sous-type 0x69 de 0x00551f30. Il orbite autour du lanceur, son
    //                orbite ralentit d'un huitieme par image, puis il est PROJETE et vole librement
    //                jusqu'au sol ou jusqu'a une cible.
    //   0x0047BB10   LA POUSSIERE, sous-type 0x4b de 0x00552230. Huit images, quatre tuiles.
    //
    // LES DEUX COMPTEURS SONT UN COMPTE DE REFERENCES, le premier du depot. Le cercle pose
    // +0x14 = +0x16 = 4 ; chaque rocher DECREMENTE +0x14 du cercle (par son +0x6c) quand il quitte
    // l'orbite, et +0x16 quand il meurt. Le cercle relache la marque 0x80 du lanceur quand +0x14
    // atteint zero — les quatre rochers sont partis — et ne s'eteint lui-meme que quand +0x16
    // atteint zero — les quatre sont retombes. C'est ce qui tient le lanceur immobile le temps du
    // sort.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047b7a0 @ 0x0047b7a0 — <b>le cercle de Rocher</b> : le sous-type <c>0x03</c>,
    /// creneau 3 de <see cref="s_StaticEntityHandlers_00551c30"/>. 176 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Il ne se deplace pas : a chaque image il <b>recopie la position du lanceur</b> et se pose a
    /// <c>+0x60</c> — le <b>sol</b> sous lui, pas sa hauteur. Toutes les quatre images
    /// (<c>compteur &amp; 3</c>) il lache un rocher et joue un son ; seize images, donc quatre
    /// rochers.
    /// </para>
    /// <para>
    /// <b>Ensuite il attend.</b> Passe la seizieme image il ne fait plus que surveiller ses deux
    /// compteurs : <c>+0x14</c> a zero relache la marque <c>0x80</c> qu'il avait posee sur le
    /// lanceur, <c>+0x16</c> a zero l'eteint lui-meme. Ce sont les rochers qui les decrementent —
    /// voir <see cref="TickRock_0047B850"/>.
    /// </para>
    /// </summary>
    private static void TickRockCircle_0047B7A0(byte* record)
    {
        byte* e = record;
        ushort age = *(ushort*)(e + 0x10);

        if ((short)age >= 0x10)
        {
            // Ghidra: LAB_0047b82f — l'attente. Les deux compteurs sont tenus par les rochers.
            if (*(ushort*)(e + 0x14) == 0) *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
            if (*(ushort*)(e + 0x16) == 0) *(uint*)e = 0;
            return;
        }

        byte* caster = *(byte**)(e + 0x74);

        if (age == 0)
        {
            *(uint*)caster |= 0x80;                   // le lanceur est immobilise
            *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & 0xfffb) | 2);
            *(uint*)(e + 0x38) = 0x01000000;
            e[0x40] = 0;
            *(ushort*)(e + 0x14) = 4;                 // quatre rochers a lacher
            *(ushort*)(e + 0x16) = 4;                 // quatre rochers a voir mourir
        }

        // Ghidra: LAB_0047b7e5 — il se recolle au lanceur a chaque image.
        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
        *(int*)(e + 0x24) = *(int*)(caster + 0x24);
        *(int*)(e + 0x20) = *(int*)(caster + 0x60);   // le SOL sous le lanceur

        if ((e[0x10] & 3) == 0)
        {
            SpawnCastEntity_0047AAE0(e, 0x69, e[0x0b], -1);
            Aud.PlayPositionalSound(0x16, 0x329, 0x1200, (int*)(e + 0x1c));
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047b850 @ 0x0047b850 — <b>le rocher</b> : le sous-type <c>0x69</c>, creneau 105
    /// de <see cref="s_StaticEntityHandlers_00551f30"/>. 700 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Deux vies en une.</b> Tant que <c>+0x12</c> — la vitesse angulaire — vaut <c>0x10</c> ou
    /// plus, le rocher <b>orbite</b> : sa position est recalculee a 90 unites du lanceur sur le cap
    /// <c>+0x32</c>, il monte de <c>0x7000</c> par image, et la vitesse angulaire perd un huitieme
    /// d'elle-meme a chaque tour. Des qu'elle passe sous <c>0x10</c> il est <b>projete</b> : une
    /// impulsion tiree au hasard part dans <c>+0x50</c>/<c>+0x58</c>, le bit <c>0x2000000</c> le
    /// marque comme lance, et il vole ensuite par
    /// <see cref="IntegrateWithDrag_00535880"/>.
    /// </para>
    /// <para>
    /// <b>Il tient les comptes de son semeur</b>, atteint par <c>+0x6c</c> : <c>-1</c> sur
    /// <c>+0x14</c> quand il quitte l'orbite, <c>-1</c> sur <c>+0x16</c> quand il meurt. Le bit
    /// <c>0x2000000</c> sert de garde — un rocher qui meurt avant d'avoir ete lance decremente
    /// <b>les deux</b>, pour que le compte reste juste.
    /// </para>
    /// <para>
    /// <b>Le test de sol est inverse par rapport a Pieds mouilles</b>, et c'est dans le binaire :
    /// <see cref="TickWetFeet_0047B460"/> traite le sol quand <c>+0x20 &gt;= +0x60</c>, celui-ci
    /// quand <c>+0x20 &lt;= +0x60</c>. Les deux sont transcrits tels quels ;
    /// <c>TODO(REVERSE)</c> : etablir lequel des deux lit le champ dans le bon sens, ou si les deux
    /// chemins ne se recouvrent jamais.
    /// </para>
    /// </summary>
    private static void TickRock_0047B850(byte* record)
    {
        byte* e = record;
        byte* caster = *(byte**)(e + 0x74);

        short* probe = stackalloc short[6] { 0x0000, -0x0032, 0x0032, 0x0064, 0x0000, 0x0064 };

        if (*(ushort*)(e + 0x10) == 0)
        {
            // ------------------------------------------------------------ la naissance
            *(uint*)e |= 0x80;

            // Le binaire garde le tirage ENTIER dans un registre et n'en range que les seize bits
            // bas ; la table de trigonometrie ne lit que douze bits, donc les deux sont le meme
            // angle. Ecrit comme le binaire le fait.
            int spin = CrtRand();
            *(short*)(e + 0x32) = (short)spin;
            *(short*)(e + 0x34) = (short)CrtRand();

            *(int*)(e + 0x20) = *(int*)(caster + 0x60) + 1;

            // 90 unites : `lea *5 ; lea *9 ; lea [edx+ecx*2]`.
            *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + (int)Winsys3d.GetSinTableEntry(spin) * 90;
            *(int*)(e + 0x24) = *(int*)(caster + 0x24) + Winsys3d.GetCosTableEntry(spin) * 90;

            uint scale = (uint)((CrtRand() & 0x1ff) + 0x600);
            *(uint*)(e + 0x28) = (scale << 0x10) | scale;
            *(ushort*)(e + 0x2c) = (ushort)scale;
            *(uint*)(e + 0x38) = 0x02ffffff;
            e[0x04] = 0x1b;                           // le modele

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551A98(), 0x4b, e[0x0b], -1);

            *(short*)(e + 0x12) = 0x200;              // la vitesse angulaire de depart
        }
        else if (*(short*)(e + 0x12) >= 0x10)
        {
            // ------------------------------------------------------------ l'orbite
            short speed = *(short*)(e + 0x12);
            speed -= (short)(speed >> 3);             // elle perd un huitieme par image
            *(short*)(e + 0x32) += speed;
            *(short*)(e + 0x12) = speed;

            int heading = *(short*)(e + 0x32);
            *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + (int)Winsys3d.GetSinTableEntry(heading) * 90;
            *(int*)(e + 0x24) = *(int*)(caster + 0x24) + Winsys3d.GetCosTableEntry(heading) * 90;
            *(int*)(e + 0x20) += 0x7000;

            if (*(short*)(e + 0x12) < 0x10)
            {
                // ------------------------------------------------------------ la projection
                *(short*)(e + 0x9a) = 0x6000;

                // Le cap du LANCEUR, disperse sur un huitieme de tour et recentre de 0x100.
                int throwAngle = (CrtRand() & 0x1ff) + *(short*)(caster + 0x32) - 0x100;

                int sin = (int)Winsys3d.GetSinTableEntry(throwAngle);
                *(int*)(e + 0x50) = sin * ((CrtRand() & 0x1f) + 0x32);

                int cos = Winsys3d.GetCosTableEntry(throwAngle);
                *(int*)(e + 0x58) = cos * ((CrtRand() & 0x1f) + 0x32);

                (*(ushort*)(*(byte**)(e + 0x6c) + 0x14))--;   // un rocher de moins en orbite
                *(uint*)e |= 0x2000000;
            }
        }
        else
        {
            // Ghidra: LAB_0047b9ee — le vol libre, une fois l'orbite quittee.
            IntegrateWithDrag_00535880(e);            // Ghidra: FUN_00535880
        }

        // Ghidra: LAB_0047b9f7 — le placement, commun aux trois branches.
        FinalizeEntityPlacement2((uint*)e);           // Ghidra: FUN_00533940

        uint contact = *(uint*)(e + 0x90);
        bool airborne = (contact & 0xa000) != 0;      // `test ah,0A0h`

        if (!airborne && (contact & 0x802) == 0 && *(int*)(e + 0x20) > *(int*)(e + 0x60))
        {
            airborne = true;
        }

        if (!airborne)
        {
            // ------------------------------------------------------------ l'impact au sol
            *(int*)(e + 0x20) = *(int*)(e + 0x60);

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551A98(), 0x4b, e[0x0b], -1);

            byte* circle = *(byte**)(e + 0x6c);
            if ((*(uint*)e & 0x2000000) == 0) (*(ushort*)(circle + 0x14))--;
            (*(ushort*)(circle + 0x16))--;

            *(uint*)e = 0;

            Aud.PlayPositionalSound(4, 0x4aa, 0x1200, (int*)(e + 0x1c));
            return;
        }

        // Ghidra: LAB_0047ba78 — en l'air : il cherche une cible.
        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            int push = *(short*)(e + 0x32);
            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
            *(ushort*)(victim + 0x96) = 0x10;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;
            *(uint*)victim |= 0x40;
        }

        // Ghidra: LAB_0047badc — le mot d'etat est relu APRES le coup.
        uint state = *(uint*)e;
        *(ushort*)(e + 0x10) += 1;

        if ((state & 0x60000000) != 0)
        {
            byte* circle = *(byte**)(e + 0x6c);
            if ((state & 0x2000000) == 0) (*(ushort*)(circle + 0x14))--;
            (*(ushort*)(circle + 0x16))--;

            *(uint*)e = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0047bb10 @ 0x0047bb10 — <b>la poussiere de Rocher</b> : le sous-type <c>0x4b</c>,
    /// creneau 75 de <see cref="s_StaticEntityHandlers_00552230"/>. 60 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// La jumelle de <see cref="TickSpark_0047B050"/> : huit images, l'image d'atlas est la moitie
    /// du compteur, quatre tuiles. Deux differences, toutes deux dans le binaire — le decalage est
    /// <b>non signe</b> (<c>shr</c> et non <c>sar</c>) et la comparaison porte sur le <b>mot</b>
    /// entier (<c>cmp cx,4</c>) et non sur son octet bas. Ni echelle ni teinte ne bougent apres la
    /// premiere image : elle ne fait que derouler ses quatre images.
    /// </para>
    /// </summary>
    private static void TickRockDust_0047BB10(byte* record)
    {
        byte* e = record;

        ushort counter = *(ushort*)(e + 0x10);
        ushort frame = (ushort)(counter >> 1);        // `shr cx,1` — NON signe

        if (counter == 0)
        {
            *(uint*)(e + 0x28) = 0x10001000;
            *(uint*)(e + 0x38) = 0x01000000;
            *(short*)(e + 0x2e) = -0x14;
        }

        if (frame >= 4) *(uint*)e = 0;

        e[0x40] = (byte)frame;
        *(ushort*)(e + 0x10) = (ushort)(counter + 1);
    }

    // =====================================================================
    // FEU D'ENFER — le cinquieme sort, ecole 0 indice 4, sous-type 0x04
    //
    // Trois corps a ecrire, et un quatrieme qui existait deja.
    //
    //   0x0047BB50   LE SORT. Sous-type 4 de 0x00551c30. Il CHOISIT UNE CIBLE des la premiere
    //                image, lache six flammes et un embrasement autour d'elle, puis attend
    //                soixante images avant de poser l'effet d'etat et de s'eteindre.
    //   0x0047BC40   LA FLAMME, sous-type 0x67 de 0x00551f30. Six exemplaires, en deux anneaux de
    //                trois inclines en sens contraires.
    //   0x0047BDF0   L'EMBRASEMENT, sous-type 0x4c de 0x00552230. Il grossit, puis a la 32e image
    //                lache DEUX braises 0x68 et se resorbe.
    //   0x0047BD00   LA BRAISE, sous-type 0x68 — deja ecrite et deja exercee en foret. Rien a
    //                faire : ce sort est le premier a reutiliser un corps que le depot avait.
    //
    // LE PREMIER SORT QUI VISE. Les quatre precedents partaient devant le lanceur ou tournaient
    // autour de lui ; celui-ci appelle FindTargetAround_00474BA0 des sa naissance, range la cible
    // en +0x78, et TOUS ses enfants la suivent — la recopie des 0x27 dwords la leur transmet. Les
    // flammes et l'embrasement se recollent sur elle a chaque image.
    //
    // ET LE PREMIER A DIFFERER SON EFFET. A la soixantieme image il recopie sa paire
    // famille/indice sur le lanceur et appelle ApplyStatusEffect_0053BA90 — l'entree 15 de la table
    // des 48. Il n'y a pas de recherche de cible a ce moment-la : c'est celle d'il y a soixante
    // images qui brule.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047bb50 @ 0x0047bb50 — <b>Feu d'enfer</b> : le sous-type <c>0x04</c>, creneau 4
    /// de <see cref="s_StaticEntityHandlers_00551c30"/>. 240 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>A la naissance</b> il choisit sa cible par
    /// <see cref="FindTargetAround_00474BA0"/> et la range en <c>+0x78</c>, prend le modele
    /// <c>0x0f</c>, puis lache <b>six flammes en deux anneaux</b> : trois a l'inclinaison
    /// <c>+0x155</c>, trois a <c>-0x155</c>, chacune decalee d'un tiers de tour
    /// (<c>0x555</c>) et les deux anneaux decales entre eux de <c>0x2aa</c> — un sixieme de tour.
    /// Puis l'embrasement et le son.
    /// </para>
    /// <para>
    /// <b>Ensuite il ne fait plus que compter</b>, jusqu'a la soixantieme image. La il recopie sa
    /// paire famille/indice dans le <c>+0x06</c> du <b>lanceur</b> — pour que
    /// <see cref="ResolveSpellHit_0053A2B0"/> lise la bonne fiche s'il y arrive — et appelle
    /// <see cref="ApplyStatusEffect_0053BA90"/>, l'entree 15 de la table des 48, sur le couple
    /// (lanceur, cible). Puis il s'eteint.
    /// </para>
    /// </summary>
    private static void TickHellfire_0047BB50(byte* record)
    {
        byte* e = record;
        ushort age = *(ushort*)(e + 0x10);

        if (age != 0)
        {
            if ((short)age < 0x3c)
            {
                *(ushort*)(e + 0x10) += 1;            // Ghidra: LAB_0047bc38
                return;
            }

            // La soixantieme image : l'effet d'etat, enfin.
            byte* caster = *(byte**)(e + 0x74);
            *(ushort*)(caster + 6) = *(ushort*)(e + 6);

            ApplyStatusEffect_0053BA90((uint*)caster, *(uint**)(e + 0x78), 0);

            *(ushort*)(e + 0x10) += 1;
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_0047bb95 — la naissance.
        e[0x36] |= 6;

        *(byte**)(e + 0x78) = FindTargetAround_00474BA0(e, 0);
        e[0x04] = 0x0f;                               // le modele
        *(short*)(e + 0x34) = 0x155;                  // l'inclinaison du premier anneau

        // Trois flammes, un tiers de tour d'ecart. 0x555 est le tiers de 0x1000 arrondi en bas.
        for (int i = 0; i < 3; i++)
        {
            SpawnCastEntity_0047AAE0(e, 0x67, e[0x0b], -1);
            *(short*)(e + 0x32) += 0x555;
        }

        // Le second anneau : un sixieme de tour plus loin, et l'inclinaison inversee.
        *(short*)(e + 0x32) = (short)(*(short*)(e + 0x32) + 0x2aa);
        *(short*)(e + 0x34) = -0x155;

        for (int i = 0; i < 3; i++)
        {
            SpawnCastEntity_0047AAE0(e, 0x67, e[0x0b], -1);
            *(short*)(e + 0x32) += 0x555;
        }

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4c, e[0x0b], -1);

        Aud.PlayPositionalSound(0x10, 0x400, 0x1200, (int*)(e + 0x1c));

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047bc40 @ 0x0047bc40 — <b>la flamme de Feu d'enfer</b> : le sous-type
    /// <c>0x67</c>, creneau 103 de <see cref="s_StaticEntityHandlers_00551f30"/>. 181 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Elle ne se deplace pas d'elle-meme : a chaque image elle <b>se recolle sur la cible</b>
    /// (<c>+0x78</c>, heritee du sort) et tourne de <c>-0xc0</c> sur <c>+0x32</c>. Six exemplaires
    /// tournant ensemble font la couronne.
    /// </para>
    /// <para>
    /// <b>Trois ages.</b> A la naissance elle prend l'echelle <c>0x1800</c> sur les trois axes et
    /// une teinte <b>nulle</b> — invisible. Jusqu'a la 32e image elle <b>s'eclaircit</b> de
    /// <c>0x101010</c> par image jusqu'a <c>0xf0f0f0</c>, et retrecit de <c>0x80</c> tant que son
    /// echelle depasse <c>0xc00</c>. Au-dela elle ne fait plus que retrecir, sans plancher — et
    /// c'est <b>l'echelle nulle qui l'eteint</b>, pas un compteur.
    /// </para>
    /// <para>
    /// <b>La hauteur est celle de la tete du LANCEUR</b> (<c>+0x74</c>), pas de la cible : la
    /// position vient de l'une et l'altitude de l'autre. C'est dans le binaire, et c'est ce qui
    /// pose la couronne a hauteur d'homme quelle que soit la taille du monstre vise.
    /// </para>
    /// </summary>
    private static void TickHellfireFlame_0047BC40(byte* record)
    {
        byte* e = record;
        byte* target = *(byte**)(e + 0x78);
        ushort age = *(ushort*)(e + 0x10);

        if (age == 0)
        {
            *(uint*)(e + 0x28) = 0x18001800;
            *(ushort*)(e + 0x2c) = 0x1800;
            *(uint*)(e + 0x38) = 0;                   // invisible a la naissance
        }
        else if ((short)age < 0x20)
        {
            if (*(short*)(e + 0x28) > 0x0c00)
            {
                unchecked
                {
                    *(uint*)(e + 0x28) += 0xff7fff80;  // -0x80 sur X et sur Y
                    *(ushort*)(e + 0x2c) += 0xff80;    // -0x80 sur Z
                }
            }

            uint tint = *(uint*)(e + 0x38);
            if (tint < 0x00f0f0f0) *(uint*)(e + 0x38) = tint + 0x00101010;
        }
        else
        {
            // Ghidra: LAB_0047bc9b — passe la 32e image elle ne fait plus que retrecir.
            unchecked
            {
                *(uint*)(e + 0x28) += 0xff7fff80;
                *(ushort*)(e + 0x2c) += 0xff80;
            }
        }

        // Ghidra: LAB_0047bcac — le recollage, commun aux trois ages.
        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
        unchecked { *(ushort*)(e + 0x32) += 0xff40; }  // -0xc0 de rotation par image
        *(int*)(e + 0x20) = *(int*)(target + 0x20) + ((height & ~1) << 0xb);

        // C'est l'echelle a zero qui l'eteint — les deux shorts d'un coup.
        if (age != 0 && *(uint*)(e + 0x28) == 0) *(uint*)e = 0;

        *(ushort*)(e + 0x10) = (ushort)(age + 1);
    }

    /// <summary>
    /// Ghidra: FUN_0047bdf0 @ 0x0047bdf0 — <b>l'embrasement de Feu d'enfer</b> : le sous-type
    /// <c>0x4c</c>, creneau 76 de <see cref="s_StaticEntityHandlers_00552230"/>. 270 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Meme forme que la flamme — il se recolle sur la cible a chaque image, a la hauteur de tete
    /// du lanceur, et c'est sa <b>teinte a zero</b> qui l'eteint. Il naît invisible et sans taille,
    /// grossit de <c>0x100</c> par image jusqu'a <c>0x1000</c> et s'eclaircit de <c>0x080808</c>
    /// jusqu'a <c>0xd0d0d0</c>.
    /// </para>
    /// <para>
    /// <b>A la 32e image exactement</b> — pas avant, pas apres : le test est une egalite — il lache
    /// <b>deux braises</b> <c>0x68</c>, qui sont
    /// <see cref="EntityHandler_0047BD00"/>, deja transcrite et deja exercee en foret. Ensuite il se
    /// resorbe : la teinte redescend de <c>0x080808</c> et l'echelle <b>approche</b> <c>0x2000</c>
    /// d'un quart de la distance restante a chaque image — elle grandit donc encore en s'effacant.
    /// </para>
    /// </summary>
    private static void TickHellfireBlaze_0047BDF0(byte* record)
    {
        byte* e = record;
        byte* target = *(byte**)(e + 0x78);
        ushort age = *(ushort*)(e + 0x10);

        if (age == 0)
        {
            *(uint*)(e + 0x28) = 0;
            *(uint*)(e + 0x38) = 0;
            *(ushort*)(e + 0x0a) = 7;
            *(short*)(e + 0x2e) = -0x14;
        }
        else if ((short)age < 0x20)
        {
            // La croissance : le dword porte les deux shorts, le test ne regarde que le bas.
            if (*(short*)(e + 0x28) < 0x1000) *(uint*)(e + 0x28) += 0x01000100;

            uint tint = *(uint*)(e + 0x38);
            if (tint < 0x00d0d0d0) *(uint*)(e + 0x38) = tint + 0x00080808;
        }
        else
        {
            // Ghidra: LAB_0047be4a — les deux braises, a la 32e image et a elle seule.
            if (age == 0x20)
            {
                *(short*)(e + 0x12) = 0;
                *(ushort*)(e + 0x14) = 0x2000;
                SpawnCastEntity_0047AAE0(e, 0x68, e[0x0b], -1);
                SpawnCastEntity_0047AAE0(e, 0x68, e[0x0b], -1);
            }

            // Ghidra: LAB_0047be7e — la resorption.
            uint tint = *(uint*)(e + 0x38);
            if (tint != 0) unchecked { *(uint*)(e + 0x38) = tint + 0xfff7f7f8; }   // -0x080808

            // L'echelle approche 0x2000 d'un quart de ce qui reste, sur X puis sur Y.
            short x = *(short*)(e + 0x28);
            *(short*)(e + 0x28) = (short)(x + ((0x2000 - x) >> 2));

            short y = *(short*)(e + 0x2a);
            *(short*)(e + 0x2a) = (short)(y + ((0x2000 - y) >> 2));
        }

        // Ghidra: LAB_0047bebb — le recollage, commun aux trois ages.
        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
        *(int*)(e + 0x20) = *(int*)(target + 0x20) + ((height & ~1) << 0xb);

        if (age != 0 && *(uint*)(e + 0x38) == 0) *(uint*)e = 0;

        *(ushort*)(e + 0x10) = (ushort)(age + 1);
    }

    // =====================================================================
    // WONDER — le sixieme sort, ecole 0 indice 5, sous-type 0x05
    //
    // Trois corps, et la forme la plus riche jusqu'ici : c'est le premier sort ENTRETENU.
    //
    //   0x0047BF00   LE SORT. Sous-type 5 de 0x00551c30, 724 octets. Il immobilise le lanceur,
    //                s'envole tout droit avec frottement, frappe, et TIENT tant que le joueur
    //                garde le bouton d'incantation enfonce — en lui prenant 2 PM toutes les vingt
    //                images.
    //   0x0047C1E0   L'AURA, sous-type 0x6a de 0x00551f30. Deux exemplaires, colles au sort.
    //   0x0047C2C0   L'ANNEAU, sous-type 0x4d de 0x00552230. Colle au sort lui aussi.
    //
    // LE BIT 0x2000000 EST LE SIGNAL DE FIN, ET IL SE PROPAGE. Le sort se le pose quand il doit
    // s'arreter ; ses deux enfants LISENT le mot d'etat de leur pere par +0x6c et en heritent
    // (`and 3000000h ; or`). Ils basculent alors en resorption et s'eteignent d'eux-memes. C'est le
    // pendant du compte de references de Rocher : ici le pere ne compte pas ses fils, il leur fait
    // signe.
    //
    // ET IL PEINT LE FOND DE L'ECRAN. La naissance ecrit 0x005e9160..0x005e9166 — trois canaux a
    // 0xc4 et le mode 2 —, la fin remet le mode a zero. C'est la couleur de fond de la scene, celle
    // que WorldMap.cs fait converger a chaque image : le sort blanchit le decor tant qu'il dure.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047bf00 @ 0x0047bf00 — <b>Wonder</b> : le sous-type <c>0x05</c>, creneau 5 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 724 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le premier sort entretenu du depot.</b> Passe la seizieme image, si le lanceur est un
    /// membre du groupe, il relit <b>la manette de ce joueur</b> — l'emplacement vient de
    /// <c>s_PlayerSlots_005e9188[indice * 0x4c]</c>, l'etat de
    /// <c>s_InputStateBase_00604a20 + emplacement * 0x10</c>, et le masque compare est
    /// <c>+0x1fc4</c> du bloc du personnage, celui-la meme qui ouvre la posture d'incantation. Le
    /// bouton relache, le sort se marque <c>0x2000000</c> et tout s'arrete.
    /// </para>
    /// <para>
    /// <b>Il coute 2 PM toutes les vingt images</b>, cadence sur le compteur global
    /// <c>0x005f2cf0</c> — donc sur l'horloge du monde, pas sur son propre age. A court de PM il
    /// les met a zero et se coupe. Un lanceur qui n'est pas du groupe n'a ni manette ni PM : il
    /// tient <b>120 images</b> et s'arrete.
    /// </para>
    /// <para>
    /// <b>Le mouvement est une ligne droite qui s'essouffle</b> : la vitesse posee a la naissance
    /// (40 unites sur le cap) perd un huitieme <b>et</b> un 256e d'elle-meme a chaque image. Le
    /// placement ne sert qu'a detecter le decor — la hauteur est sauvee et rendue autour, comme
    /// pour les autres projectiles —, et un contact sur <c>0x8802</c> annule la vitesse sans tuer
    /// le sort : il reste sur place jusqu'a ce que le joueur lache.
    /// </para>
    /// </summary>
    private static void TickWonder_0047BF00(byte* record)
    {
        byte* e = record;
        uint state = *(uint*)e;

        // Une sonde plus large et plus profonde que celles des sorts precedents : 70 de
        // demi-largeur, 140 de profondeur.
        short* probe = stackalloc short[6] { 0x0000, -0x0046, 0x0046, 0x0064, 0x0000, 0x008c };

        if ((state & 0x2000000) != 0)
        {
            // ------------------------------------------------------------ la fin
            *(uint*)(*(uint**)(e + 0x74)) &= 0xffffff7f;   // le lanceur est relache
            *(uint*)e = 0;
            *(ushort*)Mem.Arena(0x005e9166) = 0;           // le fond reprend sa couleur
            return;
        }

        ushort age = *(ushort*)(e + 0x10);

        if (age == 0)
        {
            // ------------------------------------------------------------ la naissance
            byte* caster = *(byte**)(e + 0x74);
            *(uint*)caster |= 0x80;                        // le lanceur est immobilise
            *(uint*)e |= 0x80;
            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -0x14;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4d, e[0x0b], -1);

            *(ushort*)(e + 0x12) = 0x1000;
            SpawnCastEntity_0047AAE0(e, 0x6a, e[0x0b], -1);
            SpawnCastEntity_0047AAE0(e, 0x6a, e[0x0b], -1);

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
            int heading = *(short*)(e + 0x32);
            *(int*)(e + 0x20) += (height & ~1) << 0xb;

            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(heading) * 40;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(heading) * 40;

            // La couleur de fond de la scene : trois canaux a 0xc4, mode 2.
            *(uint*)Mem.Arena(0x005e9160) = 0x00c400c4;
            *(uint*)Mem.Arena(0x005e9164) = 0x000200c4;

            Aud.PlayPositionalSound(0x2d, 0x400, 0x1200, (int*)(e + 0x1c));
        }
        else if ((short)age >= 0x10)
        {
            // ------------------------------------------------------------ l'entretien
            if ((state & 0x800000) != 0)
            {
                byte* caster = *(byte**)(e + 0x74);
                uint index = ((uint*)caster)[2] & 0xff;
                byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

                // La manette du joueur qui tient ce personnage. Le pas est 0x4c dans la table des
                // emplacements et 0x10 dans celle des etats — le meme piege que PollInputAndAudio
                // documente : la pseudo-C ecrit `* 4` parce que le symbole est typé `undefined4`.
                int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
                ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

                if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0)
                {
                    state |= 0x2000000;                    // le bouton est relache
                    *(uint*)e = state;
                }

                // Ghidra: LAB_0047c07f — la ponction, cadencee sur l'horloge du monde.
                if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
                {
                    short mana = *(short*)(perChar + 0x14e);

                    if (mana >= 2)
                    {
                        *(short*)(perChar + 0x14e) = (short)(mana - 2);
                    }
                    else
                    {
                        *(ushort*)(perChar + 0x14e) = 0;
                        *(uint*)e = *(uint*)e | 0x2000000;
                    }
                }
            }
            else if ((short)age >= 0x78)
            {
                // Un lanceur hors groupe n'a ni manette ni PM : 120 images, et c'est tout.
                *(uint*)e |= 0x2000000;
            }

            // Ghidra: LAB_0047c0c2 — et dans tous les cas, le lanceur doit tenir debout.
            if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_0047c0d5 — le vol, commun aux trois branches.
        int vx = *(int*)(e + 0x50);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x24) += vz;

        // Le frottement prend un huitieme ET un 256e : la vitesse s'eteint en une trentaine
        // d'images.
        *(int*)(e + 0x50) = vx - (vx >> 3) - (vx >> 8);
        *(int*)(e + 0x58) = vz - (vz >> 8) - (vz >> 3);

        int keptHeight = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);                // Ghidra: FUN_00533940
        uint contact = *(uint*)(e + 0x90);
        *(int*)(e + 0x20) = keptHeight;

        if ((contact & 0x8802) != 0)
        {
            // Le decor l'arrete, mais ne le tue pas : il reste la tant qu'on le tient.
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x58) = 0;
        }

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            int push = *(short*)(e + 0x32);
            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;

            Aud.PlayPositionalSound(0x0f, 0xaa8, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047c1ad — le sort meurt avec son lanceur.
        uint now = *(uint*)e;

        if ((now & 0x40000000) != 0 || (*(uint*)(*(uint**)(e + 0x74)) & 0x40000000) != 0)
        {
            *(uint*)e = now | 0x2000000;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047c1e0 @ 0x0047c1e0 — <b>l'aura de Wonder</b> : le sous-type <c>0x6a</c>,
    /// creneau 106 de <see cref="s_StaticEntityHandlers_00551f30"/>. 216 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle herite du mot d'etat de son pere</b> a chaque image : les deux bits <c>0x3000000</c>
    /// du sort (<c>+0x6c</c>) sont recopies dans le sien. C'est ainsi que la fin se propage — le
    /// sort se marque, l'aura le voit a l'image suivante et bascule en resorption sans que
    /// personne ne la previenne autrement.
    /// </para>
    /// <para>
    /// Tant que le pere tient, elle se colle a lui, tourne de <c>0x30</c> et <c>0x40</c> sur ses
    /// deux axes, et <b>approche d'un quart</b> deux cibles : l'echelle vers <c>+0x12</c> (que le
    /// sort a pose a <c>0x1000</c>) et la teinte vers <c>0xff</c>. Une fois marquee elle perd un
    /// quart de chaque par image et s'eteint sous <c>0x100</c> d'echelle.
    /// </para>
    /// <para>
    /// <b>Un seul canal est lu, trois sont ecrits</b> : le binaire prend l'octet bas de
    /// <c>+0x38</c> et le replique sur les trois. L'aura est donc toujours grise.
    /// </para>
    /// </summary>
    private static void TickWonderAura_0047C1E0(byte* record)
    {
        byte* e = record;
        uint state = *(uint*)e;

        int scale;
        int channel;

        if ((state & 0x3000000) != 0)
        {
            // ------------------------------------------------------------ la resorption
            scale = *(short*)(e + 0x28);
            scale -= scale >> 2;

            channel = e[0x38];
            channel -= channel >> 2;

            if (scale < 0x100) *(uint*)e = 0;
        }
        else
        {
            // Ghidra: LAB_0047c219 — l'heritage des deux bits du pere.
            byte* parent = *(byte**)(e + 0x6c);
            *(uint*)e = (*(uint*)parent & 0x3000000) | state;

            if (*(ushort*)(e + 0x10) == 0)
            {
                *(ushort*)(e + 0x10) = 1;
                *(int*)(e + 0x28) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x30) = (short)CrtRand();
                *(short*)(e + 0x32) = (short)CrtRand();
                *(short*)(e + 0x34) = (short)CrtRand();
                e[0x04] = 0x1d;                            // le modele
            }

            // Ghidra: LAB_0047c25c — les deux approches, chacune d'un quart.
            scale = *(short*)(e + 0x28);
            scale += (*(short*)(e + 0x12) - scale) >> 2;

            channel = e[0x38];
            channel += (0xff - channel) >> 2;

            *(int*)(e + 0x1c) = *(int*)(parent + 0x1c);
            *(int*)(e + 0x20) = *(int*)(parent + 0x20);
            *(int*)(e + 0x24) = *(int*)(parent + 0x24);

            *(ushort*)(e + 0x32) += 0x30;
            *(ushort*)(e + 0x34) += 0x40;
        }

        // Ghidra: LAB_0047c298 — l'echelle et la teinte, communes aux deux branches.
        *(short*)(e + 0x2c) = (short)scale;
        // `mov edx,eax ; shl edx,10h ; or edx,eax` porte sur le registre ENTIER : une echelle
        // negative garderait ses bits de signe dans la moitie haute. Ecrit tel quel.
        *(uint*)(e + 0x28) = ((uint)scale << 0x10) | (uint)scale;
        *(uint*)(e + 0x38) = (uint)(((channel << 8 | channel) << 8) | channel);
    }

    /// <summary>
    /// Ghidra: FUN_0047c2c0 @ 0x0047c2c0 — <b>l'anneau de Wonder</b> : le sous-type <c>0x4d</c>,
    /// creneau 77 de <see cref="s_StaticEntityHandlers_00552230"/>. 219 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Meme heritage du mot d'etat que <see cref="TickWonderAura_0047C1E0"/>, meme resorption sous
    /// <c>0x100</c>. Deux differences : son echelle approche <c>0x1800</c> <b>de moitie</b> et non
    /// d'un quart, et sa teinte <b>bat</b> — elle suit
    /// <c>(sin(age &lt;&lt; 9) &gt;&gt; 7) + 0x80</c>, approchee d'un quart. L'anneau pulse donc
    /// pendant que l'aura ne fait que monter.
    /// </para>
    /// <para>
    /// Le binaire garde le canal dans <b>l'emplacement de son propre argument</b> sur la pile, et y
    /// travaille en octet. Ecrit ici avec une variable locale : c'est la meme valeur, sans le
    /// detour.
    /// </para>
    /// </summary>
    private static void TickWonderRing_0047C2C0(byte* record)
    {
        byte* e = record;
        uint state = *(uint*)e;

        int scale;
        int channel;

        if ((state & 0x3000000) != 0)
        {
            // ------------------------------------------------------------ la resorption
            int current = *(short*)(e + 0x28);
            scale = current - (current >> 2) * 2;

            channel = e[0x38];
            channel = (byte)(channel - (channel >> 2));

            if (scale < 0x100) *(uint*)e = 0;
        }
        else
        {
            // Ghidra: LAB_0047c305 — l'heritage des deux bits du pere.
            byte* parent = *(byte**)(e + 0x6c);
            ushort age = *(ushort*)(e + 0x10);
            *(uint*)e = (*(uint*)parent & 0x3000000) | state;

            if (age == 0) *(int*)(e + 0x28) = 0;

            // Ghidra: LAB_0047c324 — l'echelle approche 0x1800 de moitie.
            int current = *(short*)(e + 0x28);
            scale = current + ((0x1800 - current) >> 2) * 2;

            channel = e[0x38];

            // La teinte bat : elle approche d'un quart la cible (sin(age << 9) >> 7) + 0x80.
            int beat = (int)Winsys3d.GetSinTableEntry((short)age << 9);
            channel = (byte)(channel + (((beat >> 7) - channel + 0x80) >> 2));

            *(int*)(e + 0x1c) = *(int*)(parent + 0x1c);
            *(int*)(e + 0x20) = *(int*)(parent + 0x20);
            *(int*)(e + 0x24) = *(int*)(parent + 0x24);
        }

        // Ghidra: LAB_0047c375 — l'age avance dans les deux branches, l'echelle et la teinte aussi.
        *(ushort*)(e + 0x10) += 1;
        // Meme forme que dans l aura : le OU porte sur le registre entier.
        *(uint*)(e + 0x28) = ((uint)scale << 0x10) | (uint)scale;
        *(uint*)(e + 0x38) = (uint)(((channel << 8 | channel) << 8) | channel);
    }

    // =====================================================================
    // TENEBRES — le septieme sort, ecole 0 indice 6, sous-type 0x06
    //
    // Deux corps seulement, mais l'ombre est le premier corps du depot qui ANNULE SON PROPRE
    // DEPLACEMENT.
    //
    //   0x0047C3A0   LE SORT. Sous-type 6 de 0x00551c30, 173 octets. Une seule image : il lache
    //                CINQ ombres en eventail sur un quart de tour, assombrit le fond de l'ecran et
    //                s'eteint.
    //   0x0047C450   L'OMBRE, sous-type 0x6b de 0x00551f30, 520 octets. Elle glisse, grossit, et
    //                REBONDIT sur le decor.
    //
    // LE REBOND EST UN RETOUR EN ARRIERE. L'ombre met de cote sa position et les CINQ dwords de
    // +0x80 a +0x90 — l'etat de collision que FinalizeEntityPlacement2 va ecrire —, avance, puis
    // appelle le placement. Si le contact a mordu (masque 0x802), elle remet tout en place, fait
    // demi-tour sur +0x32 et INVERSE ses deux vitesses. Rien de ce que le placement a ecrit ne
    // subsiste : c'est une annulation, pas une correction.
    //
    // ET SA PORTEE GROSSIT AVEC ELLE. Les mots 2 et 3 de la sonde — demi-largeur et tolerance en
    // hauteur — sont recalcules a chaque image depuis l'echelle. Une ombre qui a grandi frappe plus
    // large, ce qu'aucun des six sorts precedents ne faisait.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047c3a0 @ 0x0047c3a0 — <b>Tenebres</b> : le sous-type <c>0x06</c>, creneau 6 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 173 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Une seule image, comme les graines de Balle de feu et de Boule de lumiere — mais elle lache
    /// <b>cinq</b> enfants et non un. Le cap recule d'abord de <c>0x200</c>, puis chaque ombre part
    /// <c>0x100</c> plus loin que la precedente : l'eventail est centre sur le cap du lanceur et
    /// couvre <b>un quart de tour</b>. La vitesse posee est de 22 unites sur le cap de chacune.
    /// </para>
    /// <para>
    /// <b>Il assombrit la scene</b> : les trois canaux de fond passent a <c>0x60</c> avec le mode 2,
    /// la ou <see cref="TickWonder_0047BF00"/> les monte a <c>0xc4</c>. Il ne remet pas le mode a
    /// zero — ce sont ses ombres qui le font en mourant.
    /// </para>
    /// </summary>
    private static void TickDarkness_0047C3A0(byte* record)
    {
        byte* e = record;

        e[0x36] |= 6;
        unchecked { *(ushort*)(e + 0x32) += 0xfe00; }   // -0x200 : le bord de l'eventail
        *(int*)(e + 0x28) = 0;
        *(short*)(e + 0x2c) = 0;
        *(uint*)(e + 0x38) = 0xc0e0e0e0;
        *(short*)(e + 0x2e) = -0x14;
        e[0x04] = 0x1e;                                // le modele

        for (int i = 0; i < 5; i++)
        {
            int heading = *(short*)(e + 0x32);

            // 22 unites : `lea *5 ; lea [eax+ecx*2]` donne *11, puis `shl 1`.
            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(heading) * 22;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(heading) * 22;

            SpawnCastEntity_0047AAE0(e, 0x6b, e[0x0b], -1);

            *(ushort*)(e + 0x32) += 0x100;
        }

        // Le fond de la scene, assombri : trois canaux a 0x60, mode 2.
        *(uint*)Mem.Arena(0x005e9160) = 0x00600060;
        *(uint*)Mem.Arena(0x005e9164) = 0x00020060;

        Aud.PlayPositionalSound(0x1a, 0x400, 0x1200, (int*)(e + 0x1c));

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047c450 @ 0x0047c450 — <b>l'ombre de Tenebres</b> : le sous-type <c>0x6b</c>,
    /// creneau 107 de <see cref="s_StaticEntityHandlers_00551f30"/>. 520 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle rebondit, et le rebond est une annulation.</b> Avant d'avancer elle met de cote sa
    /// position et les <b>cinq dwords de <c>+0x80</c> a <c>+0x90</c></b> — l'etat de collision que
    /// <see cref="FinalizeEntityPlacement2"/> s'apprete a ecrire. Si le contact a mordu sur le
    /// masque <c>0x802</c>, elle remet les sept valeurs en place, ajoute <c>0x800</c> a son cap et
    /// <b>nie ses deux vitesses</b>. L'image ou elle touche est donc exactement l'image d'avant,
    /// vue a l'envers.
    /// </para>
    /// <para>
    /// <b>Sa portee grossit avec elle.</b> Les mots 2 et 3 de la sonde — demi-largeur et tolerance
    /// en hauteur — valent <c>(echelle &gt;&gt; 7) + 0x1e</c> et <c>(echelle &gt;&gt; 6) + 0x1e</c>,
    /// recalcules a chaque image. L'echelle, elle, approche <c>0x3000</c> d'un huitieme : l'ombre
    /// enfle et frappe de plus en plus large. Aucun des six sorts precedents ne faisait varier sa
    /// sonde.
    /// </para>
    /// <para>
    /// <b>Sa poussee est deux fois celle des autres</b> — <c>&lt;&lt; 5</c> et non <c>&lt;&lt; 4</c>.
    /// </para>
    /// <para>
    /// Elle n'a pas de compteur de vie : la teinte part de <c>0xe0e0e0</c>, perd <c>0x080808</c> par
    /// image, et l'ombre s'eteint quand les vingt-quatre bits bas atteignent zero — vingt-huit
    /// images. En mourant elle <b>rend au fond sa couleur</b>, ce que le sort lui-meme ne fait pas.
    /// </para>
    /// </summary>
    private static void TickDarknessShade_0047C450(byte* record)
    {
        byte* e = record;

        // {decalage de cap, avance, demi-largeur, tolerance en hauteur, ouverture, profondeur}.
        // Les mots 2 et 3 sont reecrits plus bas a chaque image ; le binaire les initialise quand
        // meme, et c'est reproduit tel quel.
        short* probe = stackalloc short[6] { 0x0000, -0x0028, 0x0000, 0x0064, 0x0000, 0x0050 };

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(uint*)e |= 0x80;
        }

        // Ghidra: LAB_0047c493 — tout est mis de cote AVANT le placement.
        uint kept80 = *(uint*)(e + 0x80);
        uint kept84 = *(uint*)(e + 0x84);
        uint kept88 = *(uint*)(e + 0x88);
        uint kept8c = *(uint*)(e + 0x8c);
        uint kept90 = *(uint*)(e + 0x90);
        int keptX = *(int*)(e + 0x1c);
        int keptZ = *(int*)(e + 0x24);

        int vx = *(int*)(e + 0x50);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) = keptX + vx;
        *(int*)(e + 0x24) = keptZ + vz;

        // Le frottement prend un 256e ET un 32e : plus mordant que celui de Wonder.
        *(int*)(e + 0x50) = vx - (vx >> 8) - (vx >> 5);
        *(int*)(e + 0x58) = vz - (vz >> 5) - (vz >> 8);

        int keptY = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);            // Ghidra: FUN_00533940
        uint contact = *(uint*)(e + 0x90);
        *(int*)(e + 0x20) = keptY;

        if ((contact & 0x802) != 0)
        {
            // Le rebond : l'image est annulee, et le cap fait demi-tour.
            *(ushort*)(e + 0x32) += 0x800;

            *(uint*)(e + 0x80) = kept80;
            *(uint*)(e + 0x84) = kept84;
            *(uint*)(e + 0x8c) = kept8c;
            *(uint*)(e + 0x90) = kept90;
            *(uint*)(e + 0x88) = kept88;

            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x24) = keptZ;
            *(int*)(e + 0x50) = -*(int*)(e + 0x50);
            *(int*)(e + 0x58) = -*(int*)(e + 0x58);
        }

        // Ghidra: LAB_0047c571 — l'enflement et l'effacement.
        int scale = *(short*)(e + 0x28);
        scale += (int)((uint)(0x3000 - scale) >> 3);   // `shr`, non signe

        unchecked { *(uint*)(e + 0x38) += 0xfff7f7f8; }   // -0x080808

        *(uint*)(e + 0x28) = ((uint)scale << 0x10) | (uint)scale;
        *(short*)(e + 0x2c) = (short)((uint)scale >> 1);

        // La sonde suit l'echelle : plus l'ombre est grande, plus elle mord large.
        probe[2] = (short)(((uint)scale >> 7) + 0x1e);
        probe[3] = (short)(((uint)scale >> 6) + 0x1e);

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            int push = *(short*)(e + 0x32);
            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 5;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 5;
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;

            Aud.PlayPositionalSound(0x1a, 0xea4, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047c630 — la teinte epuisee, ou le drapeau de fin.
        if ((*(uint*)(e + 0x38) & 0xffffff) == 0 || (*(uint*)e & 0x40000000) != 0)
        {
            *(uint*)e = 0;
            *(ushort*)Mem.Arena(0x005e9166) = 0;       // le fond reprend sa couleur
        }
    }

    // =====================================================================
    // ONDE DE CHOC — le huitieme sort, ecole 0 indice 7, sous-type 0x07
    //
    // Deux corps, et deux premieres.
    //
    //   0x0047C660   LE SORT. Sous-type 7 de 0x00551c30, 450 octets. Il ne va nulle part : il reste
    //                colle au lanceur treize images, tourne sur lui-meme et frappe tout ce qui
    //                passe a portee. Il lache DEUX anneaux, l'un au sol, l'autre a hauteur de tete.
    //   0x0047C830   L'ANNEAU, sous-type 0x6c de 0x00551f30, 210 octets.
    //
    // LA POUSSEE EST RADIALE, ET C'EST LA PREMIERE. Les sept sorts precedents poussaient leur
    // victime sur le cap du projectile ; celui-ci calcule l'angle qui va de l'onde VERS la victime
    // (Ordinal_457, le ratan2 de libgte, sur les deux ecarts) et y ajoute un demi-tour. Chaque
    // victime part donc dans une direction qui lui est propre — mais VERS le centre, pas au loin :
    // le demi-tour renverse le sens. C'est ce que le binaire fait, et ce n'est pas commente ici
    // autrement que pour le signaler.
    //
    // L'ANNEAU S'ACCROCHE PAR UN DECALAGE, PAS PAR UNE COPIE. Ses champs +0x44/+0x48/+0x4c portent
    // un ecart au lanceur, et sa position est recalculee a chaque image comme ancre + ecart. C'est
    // ce qui permet aux deux anneaux de partager un corps : le premier a un ecart nul (il est au
    // sol), le second a la hauteur de tete du lanceur.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047c660 @ 0x0047c660 — <b>Onde de choc</b> : le sous-type <c>0x07</c>, creneau 7
    /// de <see cref="s_StaticEntityHandlers_00551c30"/>. 450 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il immobilise le lanceur et reste sur lui.</b> Treize images durant, il recopie la
    /// position du lanceur, tourne de <c>0xcc</c> sur <c>+0x32</c>, et frappe par la meme sonde
    /// large que Wonder (70 de demi-largeur, 140 de profondeur). Puis il relache la marque
    /// <c>0x80</c> et s'eteint.
    /// </para>
    /// <para>
    /// <b>Ses deux anneaux se distinguent par trois champs, pas par leur sous-type.</b> Il pose
    /// <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c> — l'ecart a l'ancre — et
    /// <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> — les trois echelles visees — puis engendre, deux fois
    /// de suite, le meme <c>0x6c</c>. Le premier part au sol avec le modele <c>7</c> et vise
    /// <c>0x1800</c>/<c>0x1c00</c>/<c>0x1800</c> ; le second a hauteur de tete avec le modele
    /// <c>2</c> et vise <c>0x1000</c> partout. <b>Le premier recoit aussi <c>param4 = 4</c></b>,
    /// que <see cref="SpawnCastEntity_0047AAE0"/> range en <c>+0x0a</c>.
    /// </para>
    /// </summary>
    private static void TickShockwave_0047C660(byte* record)
    {
        byte* e = record;
        byte* caster = *(byte**)(e + 0x74);

        short* probe = stackalloc short[6] { 0x0000, -0x0046, 0x0046, 0x0064, 0x0000, 0x008c };

        if (*(ushort*)(e + 0x10) == 0)
        {
            // ------------------------------------------------------------ la naissance
            *(uint*)caster |= 0x80;                    // le lanceur est immobilise
            e[0x36] |= 6;
            *(byte**)(e + 0x70) = caster;              // l'ancre des anneaux
            *(int*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            *(uint*)(e + 0x38) = 0x00808080;           // le blanc plein de winsys3d
            *(short*)(e + 0x2e) = -0x14;

            // L'anneau du sol : aucun ecart, et une echelle plus haute que large.
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;
            *(int*)(e + 0x50) = 0x1800;
            *(int*)(e + 0x54) = 0x1c00;
            *(int*)(e + 0x58) = 0x1800;
            e[0x04] = 7;

            SpawnCastEntity_0047AAE0(e, 0x6c, 0, 4);

            // L'anneau de tete : l'ecart vertical vaut la hauteur du lanceur, arrondie au pair.
            *(int*)(e + 0x44) = 0;
            int height = StatusBubbleHeight_00538BC0((uint*)caster);
            *(int*)(e + 0x48) = (height & ~1) << 0xb;
            *(int*)(e + 0x50) = 0x1000;
            *(int*)(e + 0x54) = 0x1000;
            *(int*)(e + 0x58) = 0x1000;
            *(int*)(e + 0x4c) = 0;
            e[0x04] = 2;

            SpawnCastEntity_0047AAE0(e, 0x6c, e[0x0b], -1);

            Aud.PlayPositionalSound(0x23, 0x194a, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047c743 — l'onde ne quitte pas son lanceur.
        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
        *(int*)(e + 0x20) = *(int*)(caster + 0x20);
        *(int*)(e + 0x24) = *(int*)(caster + 0x24);
        *(ushort*)(e + 0x32) += 0xcc;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            // LA POUSSEE RADIALE : l'angle va de l'onde vers la victime, plus un demi-tour. Les
            // deux ecarts sont ramenes en unites entieres (>> 12) avant le ratan2, comme partout.
            int dz = (*(int*)(victim + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            int dx = (*(int*)(victim + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int away = Winsys3d.Ordinal_457(dx, dz) + 0x800;   // Ghidra: FUN_00545a22, ratan2

            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(away) << 4;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(away) << 4;
            *(ushort*)(victim + 0x96) = 0x10;
            *(uint*)victim |= 0x40;

            Aud.PlayPositionalSound(0x1e, 0x800, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047c7fb — treize images, ou le drapeau de fin.
        if (*(short*)(e + 0x10) >= 0x0d || (*(uint*)e & 0x40000000) != 0)
        {
            *(uint*)caster &= 0xffffff7f;              // le lanceur est relache
            *(uint*)e = 0;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047c830 @ 0x0047c830 — <b>l'anneau d'Onde de choc</b> : le sous-type
    /// <c>0x6c</c>, creneau 108 de <see cref="s_StaticEntityHandlers_00551f30"/>. 210 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il se place par un ecart, pas par une copie</b> : sa position vaut a chaque image
    /// <c>ancre + (+0x44, +0x48, +0x4c)</c>, l'ancre etant l'entite de <c>+0x70</c> — le lanceur,
    /// que le sort y a range avant d'engendrer. C'est ce qui laisse un seul corps servir les deux
    /// anneaux : celui du sol a un ecart nul, celui de tete un ecart vertical.
    /// </para>
    /// <para>
    /// <b>Sans ancre, il devient sa propre ancre</b> : la branche <c>+0x70 == 0</c> replie la
    /// position courante dans l'ecart, met la position a zero et prend son propre enregistrement
    /// comme reference. Aucun appelant transcrit n'y passe — le sort pose toujours
    /// <c>+0x70</c> — mais elle est ecrite parce qu'elle est la.
    /// </para>
    /// <para>
    /// Les trois echelles approchent d'un quart les cibles que le sort a posees en
    /// <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, et la teinte perd <c>0x080808</c> par image : c'est
    /// elle, une fois nulle, qui eteint l'anneau.
    /// </para>
    /// <para>
    /// <b>Un detail du binaire qui ne se voit pas ici</b> : les trois approches se font dans un
    /// registre dont les seize bits hauts portent encore le pointeur d'ancre. Seule la moitie basse
    /// est rangee, donc le resultat est juste — mais c'est pourquoi la pseudo-C de ce genre de corps
    /// est illisible.
    /// </para>
    /// </summary>
    private static void TickShockwaveRing_0047C830(byte* record)
    {
        byte* e = record;
        byte* anchor = *(byte**)(e + 0x70);

        if (anchor == null)
        {
            if (*(ushort*)(e + 0x10) == 0)
            {
                // La position courante devient l'ecart, une fois pour toutes.
                *(int*)(e + 0x44) += *(int*)(e + 0x1c);
                *(int*)(e + 0x48) += *(int*)(e + 0x20);
                *(int*)(e + 0x4c) += *(int*)(e + 0x24);
                *(ushort*)(e + 0x10) = 1;
            }

            // Ghidra: LAB_0047c869
            *(int*)(e + 0x1c) = 0;
            *(int*)(e + 0x20) = 0;
            *(int*)(e + 0x24) = 0;
            anchor = e;
        }

        // Ghidra: LAB_0047c880 — ancre plus ecart.
        *(int*)(e + 0x1c) = *(int*)(e + 0x44) + *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(e + 0x48) + *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c) + *(int*)(anchor + 0x24);

        // Les trois echelles, chacune approchant sa cible d'un quart.
        short x = *(short*)(e + 0x28);
        *(short*)(e + 0x28) = (short)(((*(int*)(e + 0x50) - x) >> 2) + x);

        short y = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)(((*(int*)(e + 0x54) - y) >> 2) + y);

        short z = *(short*)(e + 0x2c);
        *(short*)(e + 0x2c) = (short)(((*(int*)(e + 0x58) - z) >> 2) + z);

        uint tint;
        unchecked { tint = *(uint*)(e + 0x38) + 0xfff7f7f8; }   // -0x080808
        *(uint*)(e + 0x38) = tint;

        ushort age = *(ushort*)(e + 0x10);
        if (age != 0 && tint == 0) *(uint*)e = 0;

        *(ushort*)(e + 0x10) = (ushort)(age + 1);
    }

    // =====================================================================
    // FLECHES MAGIQUES — le neuvieme sort, ecole 0 indice 8, sous-type 0x08
    //
    // Trois corps, et le premier projectile A TETE CHERCHEUSE du depot.
    //
    //   0x0047C910   LE SORT. Sous-type 8 de 0x00551c30, 309 octets. Une image : il CHERCHE
    //                l'adversaire le plus proche, calcule le cap et le tangage qui y menent, pose
    //                la vitesse et lache la fleche.
    //   0x0047CA50   LA FLECHE, sous-type 0x6d de 0x00551f30. 1007 octets — le plus gros corps de
    //                sort transcrit. Elle recorrige son cap vers la cible a chaque image.
    //   0x00475F60   L'ETINCELLE de sa trainee, sous-type 0xbf de 0x00552230. 87 octets.
    //
    // LA CIBLE EST TENUE DES DEUX COTES. La fleche range son adversaire en +0x78 et s'ecrit
    // elle-meme dans le +0x74 de celui-ci — un lien reciproque, que la mort de l'un rompt pour
    // l'autre. C'est la premiere fois qu'un sort marque sa victime AVANT de la toucher.
    //
    // LES SIX CHAMPS PRETES. Comme Pieds mouilles, la fleche ecrase +0x50/+0x54/+0x58, +0x28,
    // +0x2c et +0x38 pour parametrer l'etincelle qu'elle seme, puis les rend. Six champs cette
    // fois, et le passage par la pile est le meme.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047c910 @ 0x0047c910 — <b>Fleches magiques</b> : le sous-type <c>0x08</c>,
    /// creneau 8 de <see cref="s_StaticEntityHandlers_00551c30"/>. 309 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Une seule image. Il se hisse a hauteur de tete du lanceur, puis demande a
    /// <see cref="FindNearestOpponent_00474EB0"/> l'adversaire le plus proche — <b>pas</b> la sonde
    /// conique des sorts precedents, une vraie recherche du plus proche.
    /// </para>
    /// <para>
    /// <b>Avec une cible</b>, il calcule les trois ecarts, en tire la distance horizontale par
    /// <c>Ordinal_442</c> (le <c>SquareRoot0</c> de libgte), puis deux angles par deux
    /// <c>ratan2</c> : le cap, <b>disperse d'un tirage sur douze bits</b> et decale de
    /// <c>0xa00</c>, et le tangage decale de <c>0x100</c>. La vitesse sort de
    /// <see cref="SetVectorFromAngles_00532AD0"/> a <c>0x20</c>.
    /// </para>
    /// <para>
    /// <b>Sans cible</b>, la fleche part droit devant a <c>rsin/rcos &lt;&lt; 6</c>, sans
    /// composante verticale, et le cap avance de <c>0x400</c> — de quoi eventer les tirs suivants.
    /// </para>
    /// </summary>
    private static void TickMagicArrow_0047C910(byte* record)
    {
        byte* e = record;
        byte* caster = *(byte**)(e + 0x74);

        int height = StatusBubbleHeight_00538BC0((uint*)caster);
        *(int*)(e + 0x20) += (height & ~1) << 0xb;

        byte* target = FindNearestOpponent_00474EB0((uint*)caster, 0xffffffff, 1);

        *(uint*)(e + 0x38) = 0x00f0f0f0;
        *(uint*)(e + 0x28) = 0x08000c00;
        *(ushort*)(e + 0x2c) = 0x0800;

        if (target != null)
        {
            *(byte**)(e + 0x78) = target;

            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            // La visee monte a la tete de la cible, pas a ses pieds.
            int targetHead = StatusBubbleHeight_00538BC0((uint*)target);
            int dy = (((targetHead & ~1) << 0xb) + *(int*)(target + 0x20) - *(int*)(e + 0x20)) >> 0xc;
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            // Ghidra: FUN_00545a10 — winsys3d Ordinal_442, le SquareRoot0 de libgte.
            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            // Le cap, disperse : le tirage sur douze bits couvre un tour entier, et le 0xa00 le
            // recentre. C'est ce qui fait qu'une volee de fleches ne part pas en faisceau.
            int yaw = Winsys3d.Ordinal_457(dx, dz) + (CrtRand() & 0xfff) + 0xa00;
            *(short*)(e + 0x32) = (short)yaw;

            int pitch = Winsys3d.Ordinal_457(dy, flat) + 0x100;
            *(short*)(e + 0x34) = (short)pitch;

            SetVectorFromAngles_00532AD0(
                (int*)(e + 0x50), *(short*)(e + 0x32) - 0x400, (short)pitch, 0x20);
        }
        else
        {
            // Ghidra: LAB_0047c9fb — droit devant, a plat.
            int heading = *(short*)(e + 0x32);
            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(heading) << 6;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(heading) << 6;
            *(ushort*)(e + 0x32) += 0x400;
        }

        // Ghidra: LAB_0047ca24
        e[0x04] = 0x14;                                // le modele
        SpawnCastEntity_0047AAE0(e, 0x6d, e[0x0b], -1);
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047ca50 @ 0x0047ca50 — <b>la fleche magique</b> : le sous-type <c>0x6d</c>,
    /// creneau 109 de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>1007 octets</b>, le plus
    /// gros corps de sort du depot, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle poursuit.</b> Tant qu'elle a une cible en <c>+0x78</c>, elle recalcule a chaque image
    /// les deux angles qui y menent et <b>ajoute</b> le vecteur correspondant a sa vitesse, laquelle
    /// perd ensuite un huitieme sur chaque axe. Ce n'est donc pas une correction de cap mais une
    /// <b>poussee continue</b> vers la cible : la fleche decrit une courbe et peut depasser.
    /// </para>
    /// <para>
    /// <b>Le lien est reciproque.</b> Elle ecrit son propre enregistrement dans le <c>+0x74</c> de
    /// sa cible, et le remet a zero en mourant — au contact comme sur le decor. Une cible qui porte
    /// <c>0x50000000</c> (morte ou mourante) est relachee et la fleche redevient libre.
    /// </para>
    /// <para>
    /// <b>Son orientation vient du deplacement reel</b>, pas du cap vise : apres avoir bouge, elle
    /// compare sa position a celle d'avant et en tire <c>+0x32</c> et <c>+0x34</c> par deux
    /// <c>ratan2</c>. Le modele pointe donc toujours dans la direction ou elle va vraiment.
    /// </para>
    /// <para>
    /// <b>La trainee.</b> Quand l'indice du sort vaut 8 — c'est-a-dire pour Fleches magiques et pas
    /// pour un autre appelant du meme sous-type — elle seme a chaque image un <c>0xbf</c> dont la
    /// vitesse part en arriere, dispersee de deux tirages sur huit bits. Six champs lui sont pretes
    /// puis rendus.
    /// </para>
    /// <para>
    /// Douze images sans cible et elle s'eteint ; sur le decor elle lache un <c>0xbd</c> —
    /// l'eclaboussure de <see cref="s_Descriptors_00551A08"/>.
    /// </para>
    /// </summary>
    private static void TickMagicArrowShaft_0047CA50(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { -0x0400, -0x0080, 0x0032, 0x00c8, 0x0000, 0x0080 };
        int* step = stackalloc int[3];

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(uint*)e |= 0x80;
            Aud.PlayPositionalSound(0x12, 0x400, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047caaf
        e[0x36] |= 6;

        int wasX = *(int*)(e + 0x1c);
        int wasY = *(int*)(e + 0x20);
        int wasZ = *(int*)(e + 0x24);

        byte* target = *(byte**)(e + 0x78);
        byte* keptTarget = target;

        int yaw;
        int pitch;

        if (target == null)
        {
            // Ghidra: LAB_0047cb67 — sans cible, elle vieillit et finit par s'eteindre.
            *(ushort*)(e + 0x10) += 1;

            if (*(short*)(e + 0x10) >= 0x0c)
            {
                *(uint*)e = 0;
                return;
            }

            // Ghidra: LAB_0047cb7c — elle garde son cap.
            yaw = *(short*)(e + 0x32) - 0x400;
            pitch = *(short*)(e + 0x34);
        }
        else if ((*(uint*)target & 0x50000000) != 0)
        {
            // La cible est morte : le lien est rompu des deux cotes.
            *(uint*)(target + 0x74) = 0;
            *(byte**)(e + 0x78) = null;
            keptTarget = null;

            // DEFAUT DE 1998, transcrit tel quel — voir Documentation/Reference/legacy-bug.md.
            // Le binaire lit ici DEUX FOIS un emplacement de pile qu'il n'a jamais ecrit : celui de
            // son propre argument, c'est-a-dire le POINTEUR de l'enregistrement. Le cap et le
            // tangage valent donc une adresse, masquee a douze bits par la table. La direction est
            // arbitraire mais deterministe — et jamais celle qu'on voulait.
            yaw = (int)(nint)record;
            pitch = (int)(nint)record;
        }
        else
        {
            // Ghidra: LAB_0047caf9 — la poursuite.
            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            *(byte**)(target + 0x74) = e;              // la cible retient la fleche

            int targetHead = StatusBubbleHeight_00538BC0((uint*)target);
            int dy = (((targetHead & ~1) << 0xb) + *(int*)(target + 0x20) - *(int*)(e + 0x20)) >> 0xc;
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            yaw = Winsys3d.Ordinal_457(dx, dz);
            pitch = Winsys3d.Ordinal_457(dy, flat);
        }

        // Ghidra: LAB_0047cb8a — la poussee vers la cible s'AJOUTE a la vitesse, elle ne la
        // remplace pas. Puis chaque axe perd un huitieme.
        SetVectorFromAngles_00532AD0(step, yaw, pitch, 0x10);

        int vx = *(int*)(e + 0x50) + step[0];
        int vy = *(int*)(e + 0x54) + step[1];
        int vz = *(int*)(e + 0x58) + step[2];

        vx -= vx >> 3;
        vy -= vy >> 3;
        vz -= vz >> 3;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        // L'orientation vient du deplacement REEL de cette image.
        int movedX = (*(int*)(e + 0x1c) - wasX) >> 0xc;
        int movedY = (*(int*)(e + 0x20) - wasY) >> 0xc;
        int movedZ = (*(int*)(e + 0x24) - wasZ) >> 0xc;

        int movedFlat = (int)Winsys3d.Ordinal_442((uint)(movedX * movedX + movedZ * movedZ));

        *(short*)(e + 0x32) = (short)(Winsys3d.Ordinal_457(movedX, movedZ) + 0x400);
        *(short*)(e + 0x34) = (short)Winsys3d.Ordinal_457(movedY, movedFlat);

        if (*(ushort*)(e + 6) == 8)
        {
            // ------------------------------------------------------------ la trainee
            // Six champs pretes a l'enfant, comme Pieds mouilles en prete trois.
            int keptVx = *(int*)(e + 0x50);
            int keptVy = *(int*)(e + 0x54);
            int keptVz = *(int*)(e + 0x58);
            uint keptScaleXY = *(uint*)(e + 0x28);
            uint keptScaleZ = *(uint*)(e + 0x2c);
            uint keptTint = *(uint*)(e + 0x38);

            *(int*)(e + 0x70) = 0;

            int sprayYaw = *(short*)(e + 0x32) - (CrtRand() & 0xff) - 0x380;
            int sprayPitch = *(short*)(e + 0x34) - (CrtRand() & 0xff) + 0x880;

            SetVectorFromAngles_00532AD0((int*)(e + 0x50), sprayYaw, sprayPitch, 0x20);

            *(uint*)(e + 0x28) = 0x0c000c00;
            *(int*)(e + 0x54) = *(int*)(e + 0x54) >> 3;
            *(ushort*)(e + 0x2c) = 3;                  // l'etincelle retrecit d'un huitieme
            *(uint*)(e + 0x38) = 0x00808080;
            *(ushort*)(e + 0x1a) = 1;                  // une seule image d'atlas

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA8(), 0xbf, e[0x0b], -1);

            *(int*)(e + 0x50) = keptVx;
            *(int*)(e + 0x54) = keptVy;
            *(int*)(e + 0x58) = keptVz;
            *(uint*)(e + 0x28) = keptScaleXY;
            *(uint*)(e + 0x2c) = keptScaleZ;
            *(uint*)(e + 0x38) = keptTint;
        }

        // Ghidra: LAB_0047cd30
        FinalizeEntityPlacement2((uint*)e);            // Ghidra: FUN_00533940

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60))
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)(e + 0x90) = 0x800;
        }

        // Ghidra: LAB_0047cd50 — le decor, ou le drapeau de fin.
        uint contact = *(uint*)(e + 0x90);
        bool spent = (*(uint*)e & 0x40000000) != 0
                     || ((contact & 0xa000) == 0 && (contact & 0x802) != 0);

        if (spent)
        {
            // Ghidra: LAB_0047ce0a — l'eclaboussure, et la cible relachee.
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551A08(), 0xbd, 0, 4);

            if (keptTarget != null) *(uint*)(keptTarget + 0x74) = 0;

            *(uint*)e = 0;
            return;
        }

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);
        if (victim == null) return;

        ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

        int push = *(short*)(e + 0x32) - 0x400;
        *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
        *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;
        *(uint*)victim |= 0x40;
        *(ushort*)(victim + 0x96) = 0x10;
        *(uint*)(victim + 0x74) = 0;                   // la victime est relachee

        Aud.PlayPositionalSound(0x0b, 0x800, 0x1200, (int*)(e + 0x1c));

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00475f60 @ 0x00475f60 — <b>l'etincelle de trainee</b> : le sous-type
    /// <c>0xbf</c> de <see cref="s_StaticEntityHandlers_00552230"/>. 87 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle est entierement parametree par son parent</b>, et c'est ce qui la rend reutilisable :
    /// <c>+0x1a</c> donne le nombre d'images d'atlas a parcourir en anneau, <c>+0x2c</c> le
    /// <b>decalage</b> par lequel son echelle retrecit a chaque image — un decalage
    /// <b>variable</b>, <c>sar ax,cl</c>, ce qui est rare — et <c>+0x38</c> sa teinte de depart.
    /// <see cref="TickMagicArrowShaft_0047CA50"/> pose <c>1</c>, <c>3</c> et <c>0x808080</c> : une
    /// seule image, un huitieme de moins par image, blanc plein.
    /// </para>
    /// <para>
    /// Elle vole par <see cref="IntegrateWithDrag_00535880"/> et meurt quand les vingt-quatre bits
    /// bas de sa teinte atteignent zero.
    /// </para>
    /// </summary>
    private static void TickTrailSpark_00475F60(byte* record)
    {
        byte* e = record;

        IntegrateWithDrag_00535880(e);                 // Ghidra: FUN_00535880

        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if ((short)frame >= *(short*)(e + 0x1a)) e[0x40] = 0;

        // Ghidra: LAB_00475f84
        uint tint = *(uint*)(e + 0x38);

        if ((tint & 0xffffff) == 0)
        {
            *(uint*)e = 0;
            return;
        }

        int shift = *(short*)(e + 0x2c);
        unchecked { *(uint*)(e + 0x38) = tint + 0xffefeff0; }   // -0x101010

        // `sar ax,cl` : le pas de retrecissement est un DECALAGE porte par l'entite.
        short scale = *(short*)(e + 0x28);
        scale -= (short)(scale >> (shift & 0x1f));
        *(short*)(e + 0x28) = scale;
        *(short*)(e + 0x2a) = scale;
    }

    // =====================================================================
    // ENCHANT — le dixieme sort, ecole 0 indice 9, sous-type 0x09
    //
    // Deux corps neufs seulement : le troisieme enfant, l'embrasement 0x4c, est celui de Feu
    // d'enfer, et l'effet pose est deja transcrit.
    //
    //   0x0047CE40   LE SORT. Sous-type 9 de 0x00551c30, 240 octets. Il choisit une cible, seme
    //                UNE bille par image pendant vingt-quatre images, puis attend la soixantieme
    //                pour poser l'effet et s'eteindre.
    //   0x0047CF30   LA BILLE, sous-type 0x70 de 0x00551f30, 183 octets. Elle tombe en spirale
    //                sur la cible.
    //
    // C'EST LE PREMIER SORT QUI NE FRAPPE PAS. Aucun des deux corps n'appelle
    // ResolveSpellHit_0053A2B0 ni ne cherche de victime : le sort se resout d'un seul coup a la fin
    // par ApplyStatusEffect_0053AFB0, l'entree 0 de la table des 48. Et celle-la n'a ni masque
    // d'immunite, ni jet de sauvegarde, ni marque visible — elle se pose TOUJOURS. Enchant ne
    // rate pas.
    //
    // LES VINGT-QUATRE BILLES NE TOURNENT PAS. Le sort pose leur vitesse angulaire +0x14 a zero :
    // chacune garde le cap tire au hasard a sa naissance et se contente de resserrer son rayon.
    // La spirale est collective, pas individuelle — vingt-quatre traits qui convergent.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047ce40 @ 0x0047ce40 — <b>Enchant</b> : le sous-type <c>0x09</c>, creneau 9 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 240 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Trois temps, tous gouvernes par le meme compteur.</b> A la naissance il choisit sa cible
    /// par <see cref="FindTargetAround_00474BA0"/> — qu'il range dans <c>+0x78</c> <b>et</b>
    /// <c>+0x70</c>, l'un pour lui, l'autre pour que ses billes l'heritent — et lache
    /// l'embrasement <c>0x4c</c>, celui de Feu d'enfer. Jusqu'a la <b>24e</b> image il seme une
    /// bille par image. A la <b>60e</b> il recopie sa paire famille/indice sur le lanceur, appelle
    /// <see cref="ApplyStatusEffect_0053AFB0"/> et s'eteint.
    /// </para>
    /// <para>
    /// <b>Chaque bille est parametree avant d'etre engendree</b> : cap et tangage tires au hasard,
    /// rayon de depart <c>0x1f4</c> en <c>+0x12</c>, vitesse angulaire <b>nulle</b> en
    /// <c>+0x14</c>, et un ecart vertical <c>+0x48</c> qui vaut la hauteur de tete de la
    /// <b>cible</b> — pas du lanceur. Elles convergent donc sur sa tete.
    /// </para>
    /// </summary>
    private static void TickEnchant_0047CE40(byte* record)
    {
        byte* e = record;

        if (*(ushort*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -0x40;

            byte* target = FindTargetAround_00474BA0(e, 0);
            *(byte**)(e + 0x78) = target;
            *(byte**)(e + 0x70) = target;              // l'ancre que les billes heriteront

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4c, e[0x0b], -1);

            Aud.PlayPositionalSound(0x10, 0x3d4, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047ce9a — une bille par image, vingt-quatre images durant.
        if (*(short*)(e + 0x10) < 0x18)
        {
            *(short*)(e + 0x32) = (short)(CrtRand() + 0x400);
            *(short*)(e + 0x34) = (short)CrtRand();

            *(uint*)(e + 0x28) = 0x00007fff;           // X plein, Y nul
            *(ushort*)(e + 0x2c) = 0x0400;
            *(uint*)(e + 0x38) = 0;                    // elle naît invisible
            *(short*)(e + 0x12) = 0x01f4;              // le rayon de depart
            *(ushort*)(e + 0x14) = 0;                  // et elle ne tourne pas
            *(int*)(e + 0x44) = 0;

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x78));
            *(int*)(e + 0x48) = (height & ~1) << 0xb;
            *(int*)(e + 0x4c) = 0;
            e[0x04] = 0x3f;                            // le modele

            SpawnCastEntity_0047AAE0(e, 0x70, e[0x0b], -1);
        }

        // Ghidra: LAB_0047cf03 — la soixantieme image : l'effet, enfin.
        if (*(short*)(e + 0x10) >= 0x3c)
        {
            byte* caster = *(byte**)(e + 0x74);
            *(ushort*)(caster + 6) = *(ushort*)(e + 6);

            ApplyStatusEffect_0053AFB0((uint*)caster, *(uint**)(e + 0x78), 0);

            *(uint*)e = 0;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047cf30 @ 0x0047cf30 — <b>la bille d'Enchant</b> : le sous-type <c>0x70</c>,
    /// creneau 112 de <see cref="s_StaticEntityHandlers_00551f30"/>. 183 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle est en coordonnees polaires.</b> Sa position n'est jamais integree : a chaque image
    /// elle vaut <c>ancre + ecart + vecteur(cap, tangage, rayon)</c>, ou l'ancre est l'entite de
    /// <c>+0x70</c> et le vecteur sort de <see cref="SetVectorFromAngles_00532AD0"/>. Le rayon
    /// <c>+0x12</c> perd un quart a chaque image : la bille tombe donc sur sa cible en s'accelerant,
    /// et disparait des qu'il passe sous <c>0x10</c> — environ dix-sept images.
    /// </para>
    /// <para>
    /// <b>Le cap avance de <c>+0x14</c></b>, que le sort laisse a zero : la bille garde donc la
    /// direction tiree a sa naissance. Le champ existe, et un autre appelant pourrait la faire
    /// spiraler ; celui-la ne le fait pas.
    /// </para>
    /// <para>
    /// Pendant ce temps elle <b>apparait</b> — la teinte monte de <c>0x101010</c> par image jusqu'a
    /// <c>0xf0f0f0</c> — et <b>s'affine</b> : son echelle X perd un quart tant qu'elle depasse
    /// <c>0x800</c>. Elle naît donc large et invisible, et finit fine et pleine.
    /// </para>
    /// </summary>
    private static void TickEnchantMote_0047CF30(byte* record)
    {
        byte* e = record;
        byte* anchor = *(byte**)(e + 0x70);

        int* offset = stackalloc int[3];

        *(short*)(e + 0x32) += *(short*)(e + 0x14);

        int yaw = *(short*)(e + 0x32) - 0x400;
        int pitch = *(short*)(e + 0x34);
        int radius = *(short*)(e + 0x12);

        SetVectorFromAngles_00532AD0(offset, yaw, pitch, radius);

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c) + *(int*)(e + 0x44) + offset[0];
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + *(int*)(e + 0x48) + offset[1];
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24) + *(int*)(e + 0x4c) + offset[2];

        uint tint = *(uint*)(e + 0x38);
        if (tint < 0x00f0f0f0) *(uint*)(e + 0x38) = tint + 0x00101010;

        // Ghidra: LAB_0047cfad — elle s'affine, avec un plancher.
        short scale = *(short*)(e + 0x28);
        if (scale > 0x0800) *(short*)(e + 0x28) = (short)(scale - (scale >> 2));

        // Ghidra: LAB_0047cfc4 — et le rayon se resserre, sans plancher : c'est lui qui la tue.
        short next = (short)(radius - (radius >> 2));
        *(short*)(e + 0x12) = next;

        if (next < 0x10) *(uint*)e = 0;
    }

    // =====================================================================
    // MUR DE FLAMMES — le onzieme sort, ecole 0 indice 10, sous-type 0x0a
    //
    // Trois corps neufs. C'est Pieds mouilles refait en feu — meme chute, meme gravite de 0x6000
    // en +0x9a, meme avance de 20 sur le cap — mais il seme deux enfants au lieu de deux, sur une
    // image sur deux, et il TEINTE LE DECOR EN ORANGE tant qu'il brule.
    //
    //   0x0047CFF0   LE SORT. Sous-type 0x0a de 0x00551c30, 626 octets. Quarante images.
    //   0x0047D270   LA BRAISE, sous-type 0x7b de 0x00551f30, 35 octets — le plus court corps de
    //                sort du depot.
    //   0x0047D2A0   LA COLONNE, sous-type 0x52 de 0x00552230, 143 octets. Elle monte, puis
    //                retombe.
    //
    // LA DIFFERENCE AVEC PIEDS MOUILLES tient en trois points : le decor vire a l'orange
    // (0xc0/0x80/0x60, reecrit A CHAQUE IMAGE et non a la naissance), le contact avec le decor ne
    // tue pas le sort mais FORCE son compteur a 0x28 — la fin par le chemin normal —, et les
    // enfants naissent a une position tiree au hasard autour de lui, jusqu'a 127 unites.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047cff0 @ 0x0047cff0 — <b>Mur de flammes</b> : le sous-type <c>0x0a</c>, creneau
    /// 10 de <see cref="s_StaticEntityHandlers_00551c30"/>. 626 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il avance et il tombe</b>, exactement comme <see cref="TickWetFeet_0047B460"/> : 20
    /// unites sur le cap, une vitesse verticale en <c>+0x54</c> et la meme gravite de <c>0x6000</c>
    /// reecrite en <c>+0x9a</c> a chaque image. Le sol le colle et lui coupe la vitesse.
    /// </para>
    /// <para>
    /// <b>Une image sur deux, il seme deux enfants</b> — le test est <c>+0x10 &amp; 1</c>, et le
    /// compteur part de 1. La position est <b>deplacee au hasard</b> jusqu'a 127 unites dans une
    /// direction quelconque le temps des deux naissances, puis rendue : la braise et la colonne
    /// naissent donc eparpillees autour du trajet, pas dessus.
    /// </para>
    /// <para>
    /// <b>Le decor vire a l'orange</b> — <c>0xc0</c>, <c>0x80</c>, <c>0x60</c> avec le mode 2 —
    /// et <b>a chaque image</b>, non a la naissance comme Wonder et Tenebres. Le mode revient a
    /// zero quand le sort s'eteint.
    /// </para>
    /// <para>
    /// <b>Le decor ne le tue pas, il l'acheve.</b> Un contact sur le masque <c>0x802</c> ne met pas
    /// le mot d'etat a zero : il force <c>+0x10</c> a <c>0x28</c>, ce qui fait passer le sort par
    /// sa fin normale a la fin de la meme image — donc en frappant une derniere fois et en rendant
    /// sa couleur au decor.
    /// </para>
    /// </summary>
    private static void TickFlameWall_0047CFF0(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { 0x0000, -0x0080, 0x0080, 0x00c8, 0x0000, 0x0100 };

        if (*(ushort*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(ushort*)(e + 0x10) = 1;
            *(uint*)e |= 0x80;
            *(short*)(e + 0x2e) = -0x40;
            *(uint*)(e + 0x28) = 0x30003000;
            *(ushort*)(e + 0x2c) = 0x3000;
            *(uint*)(e + 0x38) = 0x00d0d0d0;
            e[0x04] = 4;                               // le modele

            // Deux fois le meme son, a deux hauteurs : c'est ce qui lui donne son grondement.
            Aud.PlayPositionalSound(0x20, 0x400, 0x1200, (int*)(e + 0x1c));
            Aud.PlayPositionalSound(0x20, 0x455, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047d088 — une image sur deux.
        if ((e[0x10] & 1) != 0)
        {
            int keptX = *(int*)(e + 0x1c);
            int keptZ = *(int*)(e + 0x24);

            // Un ecart quelconque, jusqu'a 127 unites : les deux enfants naissent eparpilles.
            int spray = CrtRand();
            int reach = CrtRand() & 0x7f;

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(spray) * reach;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(spray) * reach;

            SpawnCastEntity_0047AAE0(e, 0x7b, e[0x0b], -1);

            *(ushort*)(e + 0x1a) = 4;                  // quatre images d'atlas pour la colonne
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551B40(), 0x52, e[0x0b], -1);

            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x24) = keptZ;
        }

        // Ghidra: LAB_0047d112 — l'avance et la chute, comme Pieds mouilles.
        int heading = *(short*)(e + 0x32);
        *(short*)(e + 0x9a) = 0x6000;

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 20;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 20;

        int gravity = *(short*)(e + 0x9a);
        int velocity = *(int*)(e + 0x54);
        *(int*)(e + 0x20) += velocity;
        *(int*)(e + 0x54) = (gravity >> 8) - (velocity >> 8) + velocity - gravity;

        FinalizeEntityPlacement2((uint*)e);            // Ghidra: FUN_00533940

        if (*(int*)(e + 0x20) < *(int*)(e + 0x60))
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)(e + 0x90) = 0x800;
        }

        // Ghidra: LAB_0047d18d — le decor n'eteint pas, il precipite la fin.
        uint contact = *(uint*)(e + 0x90);
        if ((contact & 0xa000) == 0 && (contact & 0x802) != 0) *(ushort*)(e + 0x10) = 0x28;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            int push = *(short*)(e + 0x32) - 0x400;
            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;

            Aud.PlayPositionalSound(0x13, 0x653, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047d224 — la teinte du decor, reecrite a chaque image.
        *(uint*)Mem.Arena(0x005e9160) = 0x008000c0;    // R 0xc0, V 0x80
        *(uint*)Mem.Arena(0x005e9164) = 0x00020060;    // B 0x60, mode 2

        if (*(short*)(e + 0x10) >= 0x28 || (*(uint*)e & 0x40000000) != 0)
        {
            *(ushort*)Mem.Arena(0x005e9166) = 0;       // le decor reprend sa couleur
            *(uint*)e = 0;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047d270 @ 0x0047d270 — <b>la braise de Mur de flammes</b> : le sous-type
    /// <c>0x7b</c>, creneau 123 de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>35 octets</b>,
    /// le plus court corps de sort du depot, <b>absent de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Elle ne fait rien d'autre que tourner de <c>-0x170</c> et palir de <c>0x101010</c> par
    /// image, jusqu'a extinction. <b>Le test de fin porte sur le resultat de l'addition</b>, pas sur
    /// le champ relu : le <c>jne</c> lit les indicateurs de l'<c>add</c>. C'est le meme sens, et
    /// c'est ecrit ainsi pour que le rapprochement avec le desassemblage reste immediat.
    /// </para>
    /// <para>
    /// Elle n'a ni position propre — elle garde celle de sa naissance — ni compteur de vie : la
    /// teinte lui tient lieu d'horloge, comme a l'eclaboussure de Pieds mouilles.
    /// </para>
    /// </summary>
    private static void TickFlameWallEmber_0047D270(byte* record)
    {
        byte* e = record;

        unchecked
        {
            *(ushort*)(e + 0x32) += 0xfe90;            // -0x170
            uint tint = *(uint*)(e + 0x38) + 0xffefeff0;   // -0x101010
            *(uint*)(e + 0x38) = tint;

            if (tint == 0) *(uint*)e = 0;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047d2a0 @ 0x0047d2a0 — <b>la colonne de Mur de flammes</b> : le sous-type
    /// <c>0x52</c>, creneau 82 de <see cref="s_StaticEntityHandlers_00552230"/>. 143 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Seize images, en deux moities.</b> Les huit premieres son echelle approche <c>0x3000</c>
    /// d'un quart ; les huit suivantes elle perd un quart d'elle-meme ; a la seizieme elle
    /// s'eteint. Elle monte donc et retombe sans que rien ne la deplace.
    /// </para>
    /// <para>
    /// <b>Sa hauteur EST son echelle.</b> A la naissance elle met sa hauteur de cote dans
    /// <c>+0x48</c>, et a chaque image <c>+0x20 = +0x48 + (echelle &lt;&lt; 6)</c> : la colonne
    /// s'eleve au-dessus du sol exactement dans la proportion ou elle grandit. C'est le seul corps
    /// transcrit qui lie ainsi la position a la taille.
    /// </para>
    /// <para>
    /// Son image d'atlas tourne en anneau sur <c>+0x1a</c>, que le sort a pose a 4 —
    /// <see cref="s_Descriptors_00551B40"/>.
    /// </para>
    /// </summary>
    private static void TickFlameWallColumn_0047D2A0(byte* record)
    {
        byte* e = record;
        ushort age = *(ushort*)(e + 0x10);

        if (age == 0)
        {
            *(uint*)(e + 0x28) = 0;
            *(int*)(e + 0x48) = *(int*)(e + 0x20);     // la hauteur du sol, mise de cote
        }

        if ((short)age < 8)
        {
            // La montee : l'echelle approche 0x3000 d'un quart.
            short scale = *(short*)(e + 0x28);
            scale += (short)((0x3000 - scale) >> 2);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
        }
        else if ((short)age < 0x10)
        {
            // La retombee : un quart de moins par image.
            short scale = *(short*)(e + 0x28);
            scale -= (short)(scale >> 2);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
        }
        else
        {
            *(uint*)e = 0;
        }

        // Ghidra: LAB_0047d302 — la hauteur suit l'echelle, decalee de six bits.
        *(int*)(e + 0x20) = (*(short*)(e + 0x28) << 6) + *(int*)(e + 0x48);

        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if ((short)frame >= *(short*)(e + 0x1a)) e[0x40] = 0;

        *(ushort*)(e + 0x10) = (ushort)(age + 1);
    }

    // =====================================================================
    // COUP DE JUS — le douzieme sort, ecole 0 indice 11, sous-type 0x0b
    //
    // Deux corps neufs. Le halo qu'il pose sur sa victime est celui de Boule de lumiere, deja
    // ecrit — troisieme sort d'affilee a reutiliser un corps du depot.
    //
    //   0x0047D330   L'ARC. Sous-type 0x0b de 0x00551c30, 310 octets. UNE SEULE IMAGE : il pose
    //                trois segments bout a bout, chacun devie au hasard, et s'eteint.
    //   0x0047D470   LE SEGMENT, sous-type 0x75 de 0x00551f30, 395 octets. Il ne bouge pas ; il
    //                clignote sur place et frappe.
    //
    // LA FOUDRE EST UNE POLYLIGNE. L'arc ne lance pas un projectile : il MARCHE. A chaque tour de
    // sa boucle de trois il tire une deviation, tire une longueur, engendre un segment, puis
    // AVANCE de cette longueur sur le nouveau cap. Les trois segments se touchent donc bout a bout
    // et forment une ligne brisee — c'est tout le dessin de l'eclair.
    //
    // ET LA PORTEE DU SEGMENT EST SA LONGUEUR. Son echelle X vaut `longueur << 12 / 250`, et sa
    // sonde s'en deduit : l'avance vaut -(echelle >> 5) et la profondeur echelle >> 4. Un segment
    // long frappe loin, un court frappe court. C'est la deuxieme fois qu'une sonde varie — la
    // premiere etait l'ombre de Tenebres, qui suivait sa propre croissance.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047d330 @ 0x0047d330 — <b>Coup de jus</b> : le sous-type <c>0x0b</c>, creneau 11
    /// de <see cref="s_StaticEntityHandlers_00551c30"/>. 310 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il vit une seule image et pose trois segments bout a bout.</b> A chaque tour : une
    /// deviation tiree sur <c>0x1c0</c>, <b>relative a la precedente</b> (le binaire retranche
    /// l'ecart cumule avant d'ajouter le nouveau, ce qui fait zigzaguer l'arc au lieu de le courber
    /// toujours du meme cote), une longueur de <c>0x100</c> a <c>0x1ff</c>, un modele tire parmi
    /// trois (8, 9 ou 10) et deux echelles au hasard. Puis il <b>avance</b> de la longueur sur le
    /// nouveau cap, et recommence.
    /// </para>
    /// <para>
    /// <b>L'echelle X du segment est sa longueur</b>, ramenee par une division par 250 —
    /// <c>imul 0x10624DD3</c> puis <c>sar edx,4</c>, l'idiome du compilateur. C'est ce qui fait
    /// que le trait dessine mesure exactement la distance parcourue.
    /// </para>
    /// </summary>
    private static void TickLightningArc_0047D330(byte* record)
    {
        byte* e = record;

        *(uint*)(e + 0x38) = 0x00f0f0f0;

        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
        *(int*)(e + 0x20) += (height & ~1) << 0xb;

        Aud.PlayPositionalSound(0x26, 0x653, 0x1200, (int*)(e + 0x1c));

        *(ushort*)(e + 0x32) += 0x400;

        int baseHeading = *(short*)(e + 0x32);
        int drift = 0;

        for (int segment = 0; segment < 3; segment++)
        {
            // La deviation est RELATIVE : l'ecart precedent est retranche du nouveau tirage, donc
            // l'arc zigzague au lieu de tourner toujours du meme cote.
            drift = (CrtRand() & 0x1c0) - drift - 0x100;
            *(short*)(e + 0x32) = (short)(baseHeading + drift);

            int length = (CrtRand() & 0xff) + 0x100;

            // `imul 0x10624DD3 ; sar edx,4` plus la correction de signe : une division par 250.
            *(short*)(e + 0x28) = (short)((length << 0xc) / 250);
            *(short*)(e + 0x2a) = (short)((CrtRand() & 0xfff) + 0x2000);
            *(short*)(e + 0x2c) = (short)((CrtRand() & 0xfff) + 0x2800);
            e[0x04] = (byte)(CrtRand() % 3 + 8);       // trois modeles d'eclair

            SpawnCastEntity_0047AAE0(e, 0x75, e[0x0b], -1);

            // Et il marche jusqu'au bout du segment qu'il vient de poser.
            int heading = *(short*)(e + 0x32) - 0x400;
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * length;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * length;
        }

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047d470 @ 0x0047d470 — <b>le segment de Coup de jus</b> : le sous-type
    /// <c>0x75</c>, creneau 117 de <see cref="s_StaticEntityHandlers_00551f30"/>. 395 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne bouge pas.</b> Il reste ou l'arc l'a pose et se contente de <b>clignoter</b> :
    /// modele et deux echelles retires au hasard <b>a chaque image</b>, ce qui donne le
    /// grouillement de l'eclair. La teinte perd <c>0x101010</c> par image et c'est elle, une fois
    /// nulle, qui l'eteint — seize images.
    /// </para>
    /// <para>
    /// <b>Sa sonde se deduit de sa taille, et de deux manieres.</b> A la naissance, l'avance et la
    /// profondeur viennent de l'echelle X que l'arc lui a donnee — donc de la longueur du segment.
    /// A chaque image, la demi-largeur et l'ouverture viennent des deux echelles fraichement
    /// tirees. La zone frappee suit donc exactement le trait dessine, image par image.
    /// </para>
    /// <para>
    /// <b>Il immobilise le lanceur</b> a sa naissance et le relache en mourant : les trois segments
    /// posent et relachent la meme marque, le dernier a s'eteindre libere.
    /// </para>
    /// <para>
    /// La marque posee sur la victime est le <c>0x49</c> de
    /// <see cref="TickLightHalo_0047B3A0"/> — celui de Boule de lumiere, avec son atlas
    /// <see cref="s_Descriptors_00551A60"/>. Le segment se range en <c>+0x78</c> avant de
    /// l'engendrer, pour que le halo s'accroche a la victime et non a lui.
    /// </para>
    /// </summary>
    private static void TickLightningBolt_0047D470(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { -0x0400, -0x0040, 0x0040, 0x0080, 0x0000, 0x0180 };

        if (*(ushort*)(e + 0x10) == 0)
        {
            byte* caster = *(byte**)(e + 0x74);
            *(uint*)caster |= 0x80;                    // le lanceur est immobilise

            *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & 0xfffe) | 4);
            *(uint*)e |= 0x80;
            *(ushort*)(e + 0x10) += 1;

            // L'avance et la profondeur viennent de la LONGUEUR du segment, une fois pour toutes.
            short scale = *(short*)(e + 0x28);
            probe[5] = (short)(scale >> 4);
            probe[1] = (short)-(scale >> 5);
        }

        // Ghidra: LAB_0047d4eb — le clignotement, retire a chaque image.
        e[0x04] = (byte)(CrtRand() % 3 + 8);
        *(short*)(e + 0x2a) = (short)((CrtRand() & 0xfff) + 0x2000);
        *(short*)(e + 0x2c) = (short)((CrtRand() & 0xfff) + 0x2800);

        // Et la demi-largeur et l'ouverture suivent les deux echelles de cette image.
        probe[2] = (short)(*(short*)(e + 0x2c) >> 7);
        probe[4] = (short)(*(short*)(e + 0x2a) >> 6);

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            // Le halo s'accroche a la VICTIME : c'est +0x78 qui le lui dit.
            *(byte**)(e + 0x78) = victim;
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551A60(), 0x49, e[0x0b], -1);

            int push = *(short*)(e + 0x32);
            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;

            Aud.PlayPositionalSound(0x14, 0x800, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047d5d6 — la teinte epuisee relache le lanceur et eteint le segment.
        uint tint;
        unchecked { tint = *(uint*)(e + 0x38) + 0xffefeff0; }   // -0x101010
        *(uint*)(e + 0x38) = tint;

        if (tint == 0)
        {
            *(uint*)(*(uint**)(e + 0x74)) &= 0xffffff7f;
            *(uint*)e = 0;
        }
    }

    // =====================================================================
    // CONGELO — le treizieme sort, ecole 0 indice 12, sous-type 0x0c
    //
    // Quatre corps neufs, et le plus long enchainement transcrit jusqu'ici.
    //
    //   0x0047D600   LE SORT. Sous-type 0x0c de 0x00551c30, 105 octets. Une image : il lache la
    //                colonne et s'eteint.
    //   0x0047D670   LA COLONNE, sous-type 0x71 de 0x00551f30, 730 octets. Elle MONTE sur place,
    //                puis BALAIE en spirale, puis ECLATE en quatre eclats.
    //   0x0047D950   LE BLOC, sous-type 0x72 de 0x00551f30, 241 octets. Il prend la victime dans
    //                la glace et fond.
    //   0x0047DA50   L'ECLAT, sous-type 0x4e de 0x00552230, 244 octets.
    //
    // DEUX PHASES, SEPAREES PAR LA TEINTE. Tant que +0x38 n'a pas atteint 0xf0f0f0, la colonne
    // grandit sur place et s'eclaircit d'un huitieme par image ; l'image ou elle y arrive, elle
    // RELACHE le lanceur et met en file le craquement. Ensuite elle balaie : le cap tourne de 0x60
    // par image et le rayon approche 0xa000 d'un quart, ce qui donne une spirale qui s'ouvre.
    //
    // ET C'EST LE PREMIER CORPS QUI ALIMENTE LA FILE DES SONS DIFFERES. Jusqu'ici rien de
    // transcrit n'ecrivait dans 0x005dfb90 et TickDelayedSounds tournait a vide ; le script
    // 0x00552530 lui donne enfin quelque chose a jouer.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047d600 @ 0x0047d600 — <b>Congelo</b> : le sous-type <c>0x0c</c>, creneau 12 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 105 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Une image, et rien de plus : il se hisse a hauteur de tete du lanceur, remet a zero teinte
    /// et echelles — la colonne les reconstruira —, prend le modele <c>0x1b</c>, engendre et
    /// s'eteint.
    /// </para>
    /// </summary>
    private static void TickFreeze_0047D600(byte* record)
    {
        byte* e = record;

        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
        *(int*)(e + 0x20) += (height & ~1) << 0xb;

        *(uint*)(e + 0x38) = 0;
        *(uint*)(e + 0x28) = 0;
        *(ushort*)(e + 0x2c) = 0;
        e[0x04] = 0x1b;                                // le modele

        SpawnCastEntity_0047AAE0(e, 0x71, e[0x0b], -1);
        Aud.PlayPositionalSound(0x1c, 0x400, 0x1200, (int*)(e + 0x1c));

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047d670 @ 0x0047d670 — <b>la colonne de glace</b> : le sous-type <c>0x71</c>,
    /// creneau 113 de <see cref="s_StaticEntityHandlers_00551f30"/>. 730 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Sa teinte est son horloge de phase</b>, pas un compteur. Tant que <c>+0x38</c> n'a pas
    /// atteint <c>0xf0f0f0</c> — elle l'approche d'un huitieme par image — la colonne <b>monte sur
    /// place</b> : son echelle approche <c>0x2000</c> d'un huitieme et sa hauteur suit. L'image
    /// exacte ou la teinte y arrive, elle <b>relache le lanceur</b> et met le craquement en file
    /// (<see cref="s_DelayedSoundScript_00552530"/>).
    /// </para>
    /// <para>
    /// <b>Ensuite elle balaie en spirale</b> : le cap tourne de <c>0x60</c> par image, mais le
    /// deplacement se fait sur le cap <b>de depart</b> garde en <c>+0x12</c>, et le rayon
    /// <c>+0x5c</c> approche <c>0xa000</c> d'un quart. Elle seme a chaque image un eclat
    /// <c>0x4e</c> dont l'atlas est tire parmi <b>cinq</b>, et prend dans la glace ce qu'elle
    /// touche — le bloc <c>0x72</c>.
    /// </para>
    /// <para>
    /// <b>Elle finit par eclater</b> : le decor ou le drapeau de fin la font semer <b>quatre</b>
    /// <c>0xbf</c> — l'etincelle de Fleches magiques — projetes au hasard avec une gravite, puis
    /// elle force son compteur a <c>0x50</c>, ce qui la tue par le chemin normal en rendant sa
    /// couleur au decor.
    /// </para>
    /// <para>
    /// Le decor vire au <b>bleu</b> (<c>0x40</c>, <c>0x40</c>, <c>0x80</c>) a chaque image tant
    /// qu'elle vit — le pendant froid de l'orange de Mur de flammes.
    /// </para>
    /// </summary>
    private static void TickIcePillar_0047D670(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { 0x0000, -0x0100, 0x0100, 0x0190, 0x0000, 0x0200 };

        // Les cinq atlas parmi lesquels l'eclat tire le sien. Le binaire les empile sur sa propre
        // pile a chaque image ; ils vivent tous dans deux plages deja relevees.
        byte** atlases = stackalloc byte*[5];
        atlases[0] = Descriptors00551B00() + 0x10;
        atlases[1] = Descriptors00551B00() + 0x18;
        atlases[2] = Descriptors00551B00() + 0x20;
        atlases[3] = Descriptors00551BB8();
        atlases[4] = Descriptors00551BA8();

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = *(short*)(e + 0x32);  // le cap de depart, mis de cote
            *(uint*)e |= 0x80;
            *(ushort*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0;                      // le rayon du balayage
            *(byte**)(e + 0x70) = e;
            *(uint*)(*(uint**)(e + 0x74)) |= 0x80;      // le lanceur est immobilise
        }

        // Ghidra: LAB_0047d6f6 — la teinte separe les deux phases.
        if (*(uint*)(e + 0x38) < 0x00f0f0f0)
        {
            // ------------------------------------------------------------ elle monte
            int channel = e[0x38];
            channel += (0xff - channel) >> 3;
            uint tint = (uint)(((channel << 8 | channel) << 8) | channel);
            *(uint*)(e + 0x38) = tint;

            if (tint == 0x00f0f0f0)
            {
                *(uint*)(*(uint**)(e + 0x74)) &= 0xffffff7f;   // le lanceur est relache
                Aud.QueueDelayedSoundScript(DelayedSoundScript00552530(), (int*)(e + 0x1c));
            }

            // Ghidra: LAB_0047d744 — l'echelle approche 0x2000 d'un huitieme, et la hauteur suit.
            int scale = *(short*)(e + 0x28);
            int rise = (0x2000 - scale) >> 3;
            *(int*)(e + 0x20) += rise;

            scale += rise * 2;
            *(short*)(e + 0x2c) = (short)scale;
            *(uint*)(e + 0x28) = ((uint)scale << 0x10) | (uint)scale;
        }
        else
        {
            // ------------------------------------------------------------ elle balaie
            // Le cap TOURNE, mais le deplacement se fait sur celui de depart : c'est ce qui donne
            // une spirale et non un cercle.
            int start = *(short*)(e + 0x12);
            *(ushort*)(e + 0x32) += 0x60;

            int radius = *(int*)(e + 0x5c);
            *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(start) * radius) >> 0xc;
            *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(start) * radius) >> 0xc;
            *(int*)(e + 0x5c) = radius + ((0xa000 - radius) >> 2);

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), atlases[CrtRand() % 5], 0x4e, e[0x0b], -1);

            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

            if (victim != null)
            {
                ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

                *(uint*)victim |= 0x40;
                *(ushort*)(victim + 0x96) = 0x1e;      // un clignotement plus long qu'ailleurs

                // Le bloc s'accroche a la victime par +0x78, comme le halo de Coup de jus.
                *(byte**)(e + 0x78) = victim;
                SpawnCastEntity_0047AAE0(e, 0x72, e[0x0b], -1);

                Aud.PlayPositionalSound(0x19, 0x6fd, 0x1200, (int*)(e + 0x1c));
            }

            *(ushort*)(e + 0x10) += 1;
        }

        // Ghidra: LAB_0047d849 — le placement, commun aux deux phases. La hauteur est rendue.
        int keptHeight = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);            // Ghidra: FUN_00533940
        int ground = *(int*)(e + 0x60);
        *(int*)(e + 0x20) = keptHeight;

        if (keptHeight <= ground)
        {
            *(int*)(e + 0x20) = ground;
            *(uint*)(e + 0x90) = 0x800;
        }

        // Ghidra: LAB_0047d86c — l'eclatement.
        if ((*(uint*)e & 0x40000000) != 0 || (*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            *(short*)(e + 0x9a) = 0x6000;

            for (int shard = 0; shard < 4; shard++)
            {
                int spray = CrtRand();
                *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(spray) << 4;
                *(int*)(e + 0x54) = ((CrtRand() & 0x1f) + 0x1e) << 0xc;
                *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(spray) << 4;

                *(uint*)(e + 0x38) = 0x01f0f0f0;
                *(uint*)(e + 0x28) = 0x01801800;
                *(ushort*)(e + 0x2c) = 0x1f;
                *(ushort*)(e + 0x1a) = 1;

                SpawnStatusEffectEntity_0047AC40(
                    e, (int*)(e + 0x1c), Descriptors00551B00() + 0x10 + CrtRand() % 3 * 8,
                    0xbf, e[0x0b], -1);
            }

            *(ushort*)(e + 0x10) = 0x50;               // et elle finit par le chemin normal
        }

        if (*(short*)(e + 0x10) >= 0x50)
        {
            *(ushort*)Mem.Arena(0x005e9166) = 0;       // le decor reprend sa couleur
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_0047d92e — le decor vire au bleu, a chaque image.
        *(uint*)Mem.Arena(0x005e9160) = 0x00400040;    // R 0x40, V 0x40
        *(uint*)Mem.Arena(0x005e9164) = 0x00020080;    // B 0x80, mode 2
    }

    /// <summary>
    /// Ghidra: FUN_0047d950 @ 0x0047d950 — <b>le bloc de glace</b> : le sous-type <c>0x72</c>,
    /// creneau 114 de <see cref="s_StaticEntityHandlers_00551f30"/>. 241 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Il enferme la victime que <c>+0x78</c> designe et la suit : sa position est recopiee de la
    /// sienne a chaque image, decalee de la hauteur de tete <b>mesuree une seule fois</b> a la
    /// naissance et gardee en <c>+0x12</c>.
    /// </para>
    /// <para>
    /// <b>Quarante images pour prendre, vingt pour fondre.</b> Pendant les quarante premieres son
    /// echelle approche <c>hauteur &lt;&lt; 12 / 100</c> d'un quart et sa teinte <c>0xff</c> d'un
    /// quart ; ensuite les deux perdent un quart d'elles-memes. A la soixantieme il disparait.
    /// </para>
    /// </summary>
    private static void TickIceBlock_0047D950(byte* record)
    {
        byte* e = record;
        byte* victim = *(byte**)(e + 0x78);
        ushort age = *(ushort*)(e + 0x10);

        if (age == 0)
        {
            *(uint*)(e + 0x38) = 0;
            *(short*)(e + 0x2e) = -0x14;
            *(uint*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(short*)(e + 0x32) = (short)CrtRand();
            *(short*)(e + 0x12) = (short)StatusBubbleHeight_00538BC0((uint*)victim);
        }
        else
        {
            int channel = e[0x38];
            short scale = *(short*)(e + 0x28);

            if ((short)age < 0x28)
            {
                // La prise : les deux approchent leur cible d'un quart.
                channel += (0xff - channel) >> 2;

                // `imul 0x51EB851F ; sar edx,5` plus la correction de signe : /100.
                int target = (*(short*)(e + 0x12) << 0xc) / 100;
                scale += (short)((target - scale) >> 2);
                *(short*)(e + 0x28) = scale;
            }
            else
            {
                // La fonte : un quart de moins chacun.
                channel -= channel >> 2;
                scale -= (short)(scale >> 2);
                *(short*)(e + 0x28) = scale;
            }

            // Ghidra: LAB_0047d9f3
            *(uint*)(e + 0x38) = (uint)(((channel << 8 | channel) << 8) | channel);
            *(short*)(e + 0x2a) = scale;
            *(short*)(e + 0x2c) = scale;
        }

        // Ghidra: LAB_0047da0e — il colle a sa victime, a hauteur de tete.
        *(int*)(e + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(e + 0x20) = *(int*)(victim + 0x20) + ((*(short*)(e + 0x12) & ~1) << 0xb);
        *(int*)(e + 0x24) = *(int*)(victim + 0x24);

        *(ushort*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 0x3c) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047da50 @ 0x0047da50 — <b>l'eclat de glace</b> : le sous-type <c>0x4e</c>,
    /// creneau 78 de <see cref="s_StaticEntityHandlers_00552230"/>. 244 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il tourne autour du point ou il est ne</b> — <c>+0x44</c> et <c>+0x4c</c> gardent ce
    /// point — sur un rayon <c>+0x12</c> qui part de zero et approche <b>300</b> d'un huitieme,
    /// tandis que le cap avance de <c>0x100</c> par image. C'est une spirale qui s'ouvre, comme
    /// celle de sa mere, mais dix fois plus serree.
    /// </para>
    /// <para>
    /// <b>Verticalement il retombe.</b> Sa vitesse <c>+0x54</c> part negative — entre
    /// <c>-0x14000</c> et <c>-0x23000</c> selon le tirage — et gagne <c>0x4000</c> par image : il
    /// monte d'abord, puis redescend. La teinte perd <c>0x080808</c> par image et l'eteint a la
    /// vingtieme.
    /// </para>
    /// </summary>
    private static void TickIceShard_0047DA50(byte* record)
    {
        byte* e = record;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(uint*)(e + 0x38) = 0x00a0a0a0;
            *(short*)(e + 0x2e) = -0x14;

            uint scale = (uint)((CrtRand() & 0xfff) + 0x1800);
            *(uint*)(e + 0x28) = (scale << 0x10) | scale;

            *(short*)(e + 0x32) = (short)CrtRand();
            *(int*)(e + 0x20) += 0x32000;

            *(int*)(e + 0x4c) = *(int*)(e + 0x24);     // le point de naissance, en Z
            *(int*)(e + 0x54) = unchecked((int)0xfffec000) - ((CrtRand() & 0xf) << 0xc);
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);     // et en X
            e[0x40] = 0;
            *(short*)(e + 0x12) = 0;                   // le rayon part de zero
        }
        else
        {
            unchecked { *(uint*)(e + 0x38) += 0xfff7f7f8; }   // -0x080808

            short radius = *(short*)(e + 0x12);
            *(short*)(e + 0x12) = (short)(((0x12c - radius) >> 3) + radius);
        }

        // Ghidra: LAB_0047dae8 — la spirale autour du point de naissance.
        *(ushort*)(e + 0x32) += 0x100;

        int heading = *(short*)(e + 0x32);
        int radiusNow = *(short*)(e + 0x12);

        *(int*)(e + 0x1c) = *(int*)(e + 0x44) + (int)Winsys3d.GetSinTableEntry(heading) * radiusNow;
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c) + Winsys3d.GetCosTableEntry(heading) * radiusNow;

        *(int*)(e + 0x54) += 0x4000;                   // la gravite le rattrape

        *(ushort*)(e + 0x10) += 1;

        if (*(uint*)(e + 0x38) == 0) *(uint*)e = 0;
    }

    // =====================================================================
    // ATCHOUM — le quatorzieme sort, ecole 0 indice 13, sous-type 0x0d
    //
    // Deux corps neufs. C'est le SECOND SORT ENTRETENU, apres Wonder — meme lecture de manette,
    // meme ponction cadencee sur l'horloge du monde, meme garde CanSustainChannel_00475110 — mais
    // il coute 4 PM par vingt images au lieu de 2, et il POURSUIT.
    //
    //   0x0047DB50   LE NUAGE. Sous-type 0x0d de 0x00551c30, 698 octets.
    //   0x0047DE10   LA BOUFFEE, sous-type 0x4f de 0x00552230, 346 octets. Huit exemplaires.
    //
    // TROIS AGES, ET ILS NE SE RESSEMBLENT PAS. Les huit premieres images le nuage se tient au
    // lanceur et laisse tomber une bouffee par image, chacune a une position tiree au hasard au
    // SOL autour de lui. De la huitieme a la seizieme il relache le lanceur. Ensuite il poursuit
    // l'adversaire le plus proche, en approchant d'un huitieme sur les trois axes.
    //
    // ET UNE SEULE BOUFFEE SUR HUIT PEUT FRAPPER. Le nuage ecrit son propre age dans le +0x12 de
    // chaque bouffee avant de l'engendrer, et la bouffee ne cherche de victime que si ce champ est
    // NUL. Seule la premiere — celle de l'image 0 — mord donc, et elle cesse des que la
    // resorption commence a incrementer le champ. Ce n'est pas une garde de securite : c'est
    // l'age qui sert de laissez-passer.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047db50 @ 0x0047db50 — <b>Atchoum</b> : le sous-type <c>0x0d</c>, creneau 13 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 698 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le second sort entretenu du depot.</b> Comme <see cref="TickWonder_0047BF00"/> il relit
    /// la manette du joueur qui tient le lanceur et s'arrete au relachement, et comme lui il
    /// ponctionne sur l'horloge du monde <c>0x005f2cf0</c> — mais <b>4 PM toutes les vingt
    /// images</b>, le double.
    /// </para>
    /// <para>
    /// <b>Trois ages.</b> Images 0 a 7 : il se tient a hauteur de tete du lanceur et laisse tomber
    /// une bouffee par image, chacune au <b>sol</b> (<c>+0x60</c> du lanceur) a un ecart tire sur
    /// 256 unites. Images 8 a 15 : il relache la marque <c>0x80</c> du lanceur et ne fait rien
    /// d'autre. A partir de 16 : il <b>poursuit</b> l'adversaire le plus proche, approchant d'un
    /// huitieme sur les trois axes ; faute de cible il revient se coller au lanceur.
    /// </para>
    /// <para>
    /// <b>Le lien avec la cible est reciproque</b>, comme celui de la fleche magique : le nuage
    /// s'ecrit dans le <c>+0x74</c> de sa proie et la relache si elle meurt.
    /// </para>
    /// <para>
    /// Un lanceur hors du groupe n'a ni manette ni PM : il tient <b>120 images</b>.
    /// </para>
    /// </summary>
    private static void TickSneeze_0047DB50(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        int* puffAt = stackalloc int[3];
        ushort age = *(ushort*)(e + 0x10);

        if (age == 0)
        {
            *(uint*)(*(uint**)(e + 0x74)) |= 0x80;     // le lanceur est immobilise
            *(uint*)e |= 0x80;
            *(int*)(e + 0x78) = 0;
            *(uint*)(e + 0x28) = 0x18001800;
            *(uint*)(e + 0x38) = 0x00808080;

            Aud.PlayPositionalSound(3, 0x100, 0x1200, (int*)(e + 0x1c));
        }

        byte* caster = *(byte**)(e + 0x74);

        if ((short)age < 8)
        {
            // ------------------------------------------------------------ la bouffee
            // L'age du nuage part avec la bouffee : c'est son laissez-passer pour frapper.
            *(short*)(e + 0x12) = (short)age;

            puffAt[0] = *(int*)(caster + 0x1c) + (((CrtRand() & 0xff) - 0x80) << 0xc);
            puffAt[1] = *(int*)(caster + 0x60);        // au SOL, pas a sa hauteur
            puffAt[2] = *(int*)(caster + 0x24) + (((CrtRand() & 0xff) - 0x80) << 0xc);

            SpawnStatusEffectEntity_0047AC40(
                e, puffAt, TrapBurstDescriptors(), 0x4f, e[0x0b], -1);

            *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
            *(int*)(e + 0x20) = *(int*)(caster + 0x20)
                                + ((StatusBubbleHeight_00538BC0((uint*)caster) + 0x64) << 0xc);
            *(int*)(e + 0x24) = *(int*)(caster + 0x24);

            *(ushort*)(e + 0x10) += 1;
            return;
        }

        if ((short)age < 0x10)
        {
            // Ghidra: LAB_0047dc4d — huit images de latence, le lanceur est rendu a lui-meme.
            *(uint*)(*(uint**)(e + 0x74)) &= 0xffffff7f;
        }
        else
        {
            // ------------------------------------------------------------ la poursuite
            byte* target = *(byte**)(e + 0x78);

            if (target != null)
            {
                if ((*(uint*)target & 0x50000000) != 0)
                {
                    *(uint*)(target + 0x74) = 0;
                    *(int*)(e + 0x78) = 0;
                    target = null;
                }
            }
            else
            {
                target = FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 1);
            }

            if (target != null)
            {
                *(byte**)(e + 0x78) = target;
                *(byte**)(target + 0x74) = e;          // le lien est reciproque

                *(int*)(e + 0x1c) += (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 3;

                int head = StatusBubbleHeight_00538BC0((uint*)target);
                *(int*)(e + 0x20) +=
                    (((head & ~1) << 0xb) + *(int*)(target + 0x20) - *(int*)(e + 0x20)) >> 3;

                *(int*)(e + 0x24) += (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 3;
            }
            else
            {
                // Ghidra: LAB_0047dcd9 — faute de proie, il revient sur le lanceur.
                *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
                *(int*)(e + 0x20) = *(int*)(caster + 0x20)
                                    + ((StatusBubbleHeight_00538BC0((uint*)caster) + 0x64) << 0xc);
                *(int*)(e + 0x24) = *(int*)(caster + 0x24);
            }

            // Ghidra: LAB_0047dcff — le placement, la hauteur rendue.
            int keptHeight = *(int*)(e + 0x20);
            FinalizeEntityPlacement2((uint*)e);        // Ghidra: FUN_00533940
            int ground = *(int*)(e + 0x60);
            *(int*)(e + 0x20) = keptHeight;

            if (keptHeight <= ground)
            {
                *(int*)(e + 0x20) = ground;
                *(uint*)(e + 0x90) = 0x800;
            }

            if ((*(uint*)e & 0x40000000) != 0
                || (*(uint*)(e + 0x90) & 0x8802) != 0
                || CanSustainChannel_00475110(e) != 0)
            {
                *(uint*)e |= 0x2000000;
            }
        }

        // Ghidra: LAB_0047dd51 — l'entretien, commun aux deux derniers ages.
        if ((*(uint*)e & 0x800000) != 0)
        {
            uint index = ((uint*)caster)[2] & 0xff;
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0) *(uint*)e |= 0x2000000;

            // Ghidra: LAB_0047dd9f — quatre PM toutes les vingt images, le double de Wonder.
            if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
            {
                short mana = *(short*)(perChar + 0x14e);

                if (mana >= 4)
                {
                    *(short*)(perChar + 0x14e) = (short)(mana - 4);
                }
                else
                {
                    *(ushort*)(perChar + 0x14e) = 0;
                    *(uint*)e |= 0x2000000;
                }
            }
        }
        else if (*(short*)(e + 0x10) >= 0x78)
        {
            // Ghidra: LAB_0047ddf0 — sans manette ni PM, cent-vingt images et c'est tout.
            *(uint*)e |= 0x2000000;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047de10 @ 0x0047de10 — <b>la bouffee d'Atchoum</b> : le sous-type <c>0x4f</c>,
    /// creneau 79 de <see cref="s_StaticEntityHandlers_00552230"/>. 346 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle flotte autour de son pere</b> sans jamais l'atteindre : a chaque image elle vise un
    /// point tire au hasard a moins de 64 unites du nuage, et n'en parcourt qu'<b>un quart</b>.
    /// Le resultat est un flottement, pas un suivi.
    /// </para>
    /// <para>
    /// <b>Elle herite du mot d'etat du nuage</b> par <c>+0x6c</c>, les deux bits <c>0x3000000</c>,
    /// exactement comme l'aura de Wonder : c'est ainsi que la fin se propage. Une fois marquee elle
    /// perd un quart de son echelle par image et disparait sous <c>0x100</c>.
    /// </para>
    /// <para>
    /// <b>Une seule bouffee sur huit frappe.</b> Le nuage lui a passe son propre age dans
    /// <c>+0x12</c>, et la recherche de victime est gardee par <c>+0x12 == 0</c> : seule celle de
    /// la premiere image mord. La resorption incremente ce champ, ce qui la fait taire aussi.
    /// </para>
    /// <para>
    /// Son image d'atlas tourne en anneau sur sept — la borne de
    /// <see cref="s_TrapBurstDescriptors_005519D0"/>, deja etablie par un autre consommateur.
    /// </para>
    /// </summary>
    private static void TickSneezePuff_0047DE10(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { 0x0000, -0x003c, 0x003c, 0x00c8, 0x0000, 0x0078 };

        if ((*(uint*)e & 0x2000000) != 0)
        {
            // La resorption — et l'increment de +0x12 la prive du droit de frapper.
            *(ushort*)(e + 0x12) += 1;

            short scale = *(short*)(e + 0x28);
            scale -= (short)(scale >> 2);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            if (scale < 0x100)
            {
                *(uint*)e = 0;
                return;
            }
        }

        // Ghidra: LAB_0047de75 — l'anneau des sept images.
        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if (frame >= 7) e[0x40] = 0;

        byte* cloud = *(byte**)(e + 0x6c);

        // Elle vise un point quelconque autour du nuage et n'en fait que le quart.
        int wantX = *(int*)(cloud + 0x1c) + (((CrtRand() & 0x7f) - 0x40) << 0xc);
        int wantY = *(int*)(cloud + 0x20) + (((CrtRand() & 0x7f) - 0x40) << 0xc);
        int wantZ = *(int*)(cloud + 0x24) + (((CrtRand() & 0x7f) - 0x40) << 0xc);

        *(int*)(e + 0x1c) += (wantX - *(int*)(e + 0x1c)) >> 2;
        *(int*)(e + 0x20) += (wantY - *(int*)(e + 0x20)) >> 2;
        *(int*)(e + 0x24) += (wantZ - *(int*)(e + 0x24)) >> 2;

        // L'heritage des deux bits du pere, comme l'aura de Wonder.
        *(uint*)e |= *(uint*)cloud & 0x3000000;

        if (*(ushort*)(e + 0x12) != 0) return;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);
        if (victim == null) return;

        ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

        *(uint*)victim |= 0x40;
        *(ushort*)(victim + 0x96) = 0x10;
        *(byte**)(e + 0x78) = victim;

        Aud.PlayPositionalSound(0x0e, 0x200, 0x1200, (int*)(e + 0x1c));
    }

    // =====================================================================
    // ASSAUT MAGIQUE — le quinzieme sort, ecole 0 indice 14, sous-type 0x0e
    //
    // Trois corps neufs. Comme Enchant, il NE FRAPPE PAS : aucun de ses corps n'appelle
    // ResolveSpellHit_0053A2B0. Tout se resout a la soixantieme image par
    // ApplyStatusEffect_0053BE70, l'entree 21 de la table des 48.
    //
    //   0x0047DF70   LE SORT. Sous-type 0x0e de 0x00551c30, 477 octets.
    //   0x0047E150   LA LAME, sous-type 0x76 de 0x00551f30, 169 octets. SIX exemplaires.
    //   0x0047E200   L'ANNEAU, sous-type 0x77 de 0x00551f30, 90 octets. Deux exemplaires.
    //
    // LES SIX LAMES SONT DEUX GROUPES DE TROIS. La boucle est double : trois lames espacees de
    // 0x555 — un tiers de tour —, puis un decalage de 0x2aa et trois autres. La meme figure que
    // les deux anneaux de Feu d'enfer, avec un corps qui bouge au lieu de tourner sur place.
    //
    // ET CE SONT LES LAMES QUI DECIDENT DE LA FIN. Quand l'echelle angulaire +0x34 d'une lame
    // tombe sous 8, elle pose 0x2000000 sur le mot d'etat de son PERE — l'inverse du signal de
    // Wonder, qui descendait du pere vers les fils. Le sort attend ce signal pour lacher ses deux
    // anneaux.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047df70 @ 0x0047df70 — <b>Assaut magique</b> : le sous-type <c>0x0e</c>, creneau
    /// 14 de <see cref="s_StaticEntityHandlers_00551c30"/>. 477 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Trois divisions par multiplication magique a la naissance</b>, toutes sur la hauteur de
    /// tete de la cible : l'echelle <c>+0x28</c> vaut <c>hauteur &lt;&lt; 12 / 60</c>
    /// (<c>0x88888889</c> avec la correction <c>add edx,edi</c> que reclame un multiplicateur
    /// au-dela de <c>2^31</c>), <c>+0x2c</c> en vaut le dixieme (<c>0x66666667</c>), et le rayon des
    /// anneaux <c>hauteur &lt;&lt; 12 / 75</c> (<c>0x1B4E81B5</c> avec un decalage de 3 — le meme
    /// multiplicateur que l'aureole d'incantation, qui l'emploie avec 4 pour diviser par 150).
    /// </para>
    /// <para>
    /// <b>Il attend le signal de ses lames.</b> Le corps ne compte rien : il regarde
    /// <c>0x2000000</c> sur son propre mot d'etat — que les lames y posent en mourant — et se
    /// marque alors <c>0x1000000</c> pour ne le faire qu'une fois. C'est a cet instant qu'il lache
    /// les deux anneaux, opposes d'un demi-tour.
    /// </para>
    /// <para>
    /// A la soixantieme image, il recopie sa paire famille/indice sur le lanceur et pose
    /// <see cref="ApplyStatusEffect_0053BE70"/> sur la cible.
    /// </para>
    /// </summary>
    private static void TickMagicAssault_0047DF70(byte* record)
    {
        byte* e = record;

        if (*(ushort*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -0x14;

            byte* target = FindTargetAround_00474BA0(e, 0);
            *(byte**)(e + 0x78) = target;
            *(byte**)(e + 0x70) = target;              // l'ancre des lames

            *(short*)(e + 0x12) = 0x190;               // la hauteur de depart des lames
            *(uint*)(e + 0x38) = 0;

            int height = StatusBubbleHeight_00538BC0((uint*)target);

            // `imul 0x88888889 ; add edx,edi ; sar edx,5` : une division par 60. Le `add` est ce
            // que reclame un multiplicateur qui ne tient pas dans un entier signe.
            int span = (height << 0xc) / 60;
            *(int*)(e + 0x28) = span;
            e[0x04] = 0x3f;                            // le modele des lames

            // `imul 0x66666667 ; sar edx,2` : le dixieme.
            *(short*)(e + 0x2c) = (short)(span / 10);

            int heading = *(short*)(e + 0x32);
            int radius = (height >> 1) - (height >> 4);

            // Deux groupes de trois, decales entre eux d'un sixieme de tour.
            for (int ring = 0; ring < 2; ring++)
            {
                for (int blade = 0; blade < 3; blade++)
                {
                    heading += 0x555;                  // un tiers de tour

                    *(ushort*)(e + 0x34) = 0x800;
                    *(short*)(e + 0x14) = (short)heading;         // l'angle vise
                    *(short*)(e + 0x32) = (short)(heading + 0x800);  // et l'oppose pour partir

                    *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(heading) * radius;
                    *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(heading) * radius;

                    SpawnCastEntity_0047AAE0(e, 0x76, e[0x0b], -1);
                }

                heading += 0x2aa;
            }

            Aud.PlayPositionalSound(0x2c, 0x37e, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047e075 — le signal des lames, traite une seule fois.
        uint state = *(uint*)e;

        if ((state & 0x1000000) == 0 && (state & 0x2000000) != 0)
        {
            *(uint*)e = state | 0x1000000;
            e[0x04] = 0x3e;                            // le modele des anneaux

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x78));

            // `imul 0x1B4E81B5 ; sar edx,3` : une division par 75.
            int radius = (height << 0xc) / 75;
            *(short*)(e + 0x2c) = (short)radius;
            *(uint*)(e + 0x28) = (uint)radius | 0x60000000;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(ushort*)(e + 0x34) = 0;

            SpawnCastEntity_0047AAE0(e, 0x77, e[0x0b], -1);
            *(ushort*)(e + 0x32) += 0x800;             // l'autre moitie du cercle
            SpawnCastEntity_0047AAE0(e, 0x77, e[0x0b], -1);

            Aud.PlayPositionalSound(0x0d, 0x800, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047e11a — l'effet, a la soixantieme image.
        if (*(short*)(e + 0x10) >= 0x3c)
        {
            byte* caster = *(byte**)(e + 0x74);
            *(ushort*)(caster + 6) = *(ushort*)(e + 6);

            ApplyStatusEffect_0053BE70((uint*)caster, *(uint**)(e + 0x78), 0);

            *(uint*)e = 0;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047e150 @ 0x0047e150 — <b>la lame d'Assaut magique</b> : le sous-type
    /// <c>0x76</c>, creneau 118 de <see cref="s_StaticEntityHandlers_00551f30"/>. 169 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Trois amortissements a un huitieme, et rien d'autre.</b> Son angle <c>+0x32</c> approche
    /// d'un huitieme la cible <c>+0x14</c> que le sort lui a donnee — elle part d'un demi-tour de
    /// la et vient s'y ranger ; son <c>+0x34</c> et son <c>+0x12</c> perdent chacun un huitieme
    /// d'eux-memes. Le premier est son horloge, le second sa hauteur.
    /// </para>
    /// <para>
    /// <b>Elle descend.</b> Sa hauteur vaut <c>+0x12 &lt;&lt; 12</c> au-dessus du <b>sol sous
    /// l'ancre</b> (<c>+0x60</c> de la cible, pas sa position), et <c>+0x12</c> part de 400 : la
    /// lame tombe donc en s'amortissant, ce qui donne un mouvement d'epee qu'on abaisse.
    /// </para>
    /// <para>
    /// <b>C'est elle qui declare la fin.</b> Des que <c>+0x34</c> tombe sous 8, elle pose
    /// <c>0x2000000</c> sur le mot d'etat de son <b>pere</b> (<c>+0x6c</c>) — le signal remonte, la
    /// ou celui de Wonder descendait. Ensuite elle palit de <c>0x101010</c> par image ; avant, elle
    /// s'eclaircissait du meme pas.
    /// </para>
    /// </summary>
    private static void TickAssaultBlade_0047E150(byte* record)
    {
        byte* e = record;
        byte* anchor = *(byte**)(e + 0x70);

        // L'angle approche sa cible d'un huitieme.
        short angle = *(short*)(e + 0x32);
        *(short*)(e + 0x32) = (short)(((*(short*)(e + 0x14) - angle) >> 3) + angle);

        // L'horloge et la hauteur perdent un huitieme chacune.
        short clock = *(short*)(e + 0x34);
        clock -= (short)(clock >> 3);
        *(short*)(e + 0x34) = clock;

        short lift = *(short*)(e + 0x12);
        lift -= (short)(lift >> 3);
        *(short*)(e + 0x12) = lift;

        *(int*)(e + 0x1c) = *(int*)(e + 0x44) + *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = (lift << 0xc) + *(int*)(anchor + 0x60);   // au-dessus du SOL
        *(int*)(e + 0x24) = *(int*)(e + 0x4c) + *(int*)(anchor + 0x24);

        if (clock < 8)
        {
            // Le signal REMONTE vers le pere : c'est la lame qui declare la fin.
            *(uint*)(*(uint**)(e + 0x6c)) |= 0x2000000;

            uint tint;
            unchecked { tint = *(uint*)(e + 0x38) + 0xffefeff0; }     // -0x101010
            *(uint*)(e + 0x38) = tint;

            if (tint == 0) *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_0047e1e1 — tant qu'elle vit, elle s'eclaircit.
        uint current = *(uint*)(e + 0x38);
        if (current < 0x00f0f0f0) *(uint*)(e + 0x38) = current + 0x00101010;
    }

    /// <summary>
    /// Ghidra: FUN_0047e200 @ 0x0047e200 — <b>l'anneau d'Assaut magique</b> : le sous-type
    /// <c>0x77</c>, creneau 119 de <see cref="s_StaticEntityHandlers_00551f30"/>. 90 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Il se pose sur un cercle autour de l'ancre, au <b>sol</b> sous elle, a un rayon qui vaut
    /// <c>15 * (+0x28) &gt;&gt; 12</c> — l'echelle que le sort a calculee. Son angle ne bouge pas :
    /// les deux exemplaires sont figes a un demi-tour l'un de l'autre, et c'est leur teinte qui les
    /// eteint, seize images plus tard.
    /// </para>
    /// </summary>
    private static void TickAssaultRing_0047E200(byte* record)
    {
        byte* e = record;
        byte* anchor = *(byte**)(e + 0x70);

        int heading = *(short*)(e + 0x32);
        int radius = (*(short*)(e + 0x28) * 15) >> 0xc;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c) + (int)Winsys3d.GetSinTableEntry(heading) * radius;
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60);    // au sol
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24) + Winsys3d.GetCosTableEntry(heading) * radius;

        uint tint;
        unchecked { tint = *(uint*)(e + 0x38) + 0xffefeff0; }         // -0x101010
        *(uint*)(e + 0x38) = tint;

        if (tint == 0) *(uint*)e = 0;
    }

    // =====================================================================
    // LUMIERE — le seizieme sort, ecole 0 indice 15, sous-type 0x0f
    //
    // Deux corps neufs, et le sort le plus economique du depot en travail : son projectile est
    // la FLECHE MAGIQUE, deja ecrite pour le sort 8.
    //
    //   0x0047E260   LE SORT. Sous-type 0x0f de 0x00551c30, 257 octets. Troisieme sort entretenu,
    //                a 6 PM par vingt images.
    //   0x0047E370   LE GLOBE, sous-type 0x6e de 0x00551f30, 531 octets. Il flotte pres du lanceur
    //                et TIRE UNE FLECHE PAR IMAGE sur l'adversaire le plus proche.
    //
    // LES TROIS SORTS ENTRETENUS FORMENT UNE ECHELLE. Wonder coute 2 PM par vingt images, Atchoum
    // 4, Lumiere 6 — meme code de manette, meme garde CanSustainChannel_00475110, meme cadence sur
    // l'horloge du monde 0x005f2cf0. Seul le prix change.
    //
    // ET LE GLOBE EST UN LANCE-FLECHES. Sa visee est mot pour mot celle de
    // TickMagicArrow_0047C910 — ratan2 sur les ecarts, SquareRoot0 pour la distance a plat,
    // dispersion du cap sur douze bits — mais il la refait A CHAQUE IMAGE au lieu d'une fois.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047e260 @ 0x0047e260 — <b>Lumiere</b> : le sous-type <c>0x0f</c>, creneau 15 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 257 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Le troisieme sort entretenu, et le plus cher : <b>6 PM toutes les vingt images</b> contre 4
    /// pour <see cref="TickSneeze_0047DB50"/> et 2 pour <see cref="TickWonder_0047BF00"/>. Tout le
    /// reste est identique — la manette du joueur relue par <c>s_PlayerSlots_005e9188</c>, la
    /// cadence sur l'horloge du monde, la garde <see cref="CanSustainChannel_00475110"/>, et les
    /// 120 images d'un lanceur hors du groupe.
    /// </para>
    /// <para>
    /// Il ne fait rien d'autre que tenir : c'est le globe qu'il engendre a la premiere image qui
    /// travaille.
    /// </para>
    /// </summary>
    private static void TickLight_0047E260(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)(*(uint**)(e + 0x74)) &= 0xffffff7f;   // le lanceur est relache
            *(uint*)e = 0;
            return;
        }

        byte* caster = *(byte**)(e + 0x74);

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(uint*)caster |= 0x80;                    // le lanceur est immobilise
            *(uint*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            e[0x04] = 2;                               // le modele
            SpawnCastEntity_0047AAE0(e, 0x6e, e[0x0b], -1);
        }

        // Ghidra: LAB_0047e2b6 — l'entretien, la meme forme que Wonder et Atchoum.
        if ((*(uint*)e & 0x800000) != 0)
        {
            uint index = ((uint*)caster)[2] & 0xff;
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0) *(uint*)e |= 0x2000000;

            if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
            {
                short mana = *(short*)(perChar + 0x14e);

                if (mana >= 6)
                {
                    *(short*)(perChar + 0x14e) = (short)(mana - 6);
                }
                else
                {
                    *(ushort*)(perChar + 0x14e) = 0;
                    *(uint*)e |= 0x2000000;
                }
            }
        }
        else if (*(short*)(e + 0x10) >= 0x78)
        {
            *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_0047e346
        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047e370 @ 0x0047e370 — <b>le globe de Lumiere</b> : le sous-type <c>0x6e</c>,
    /// creneau 110 de <see cref="s_StaticEntityHandlers_00551f30"/>. 531 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il flotte</b> : sa position, son echelle (vers <c>0x1000</c>) et sa teinte (vers
    /// <c>0x80</c>) approchent toutes leur cible d'un quart par image, la position visee etant le
    /// lanceur a hauteur de tete plus cent unites. Il herite du bit <c>0x2000000</c> de son pere
    /// par <c>+0x6c</c>, comme l'aura de Wonder, et se resorbe alors d'un quart par image.
    /// </para>
    /// <para>
    /// <b>Et il tire une fleche magique par image.</b> La visee est mot pour mot celle de
    /// <see cref="TickMagicArrow_0047C910"/> — <c>ratan2</c> sur les trois ecarts,
    /// <c>Ordinal_442</c> pour la distance a plat, cap disperse sur douze bits et recentre de
    /// <c>0xa00</c>, tangage decale de <c>0x100</c> — mais refaite <b>a chaque image</b> au lieu
    /// d'une seule fois. Le projectile est le <c>0x6d</c> de
    /// <see cref="TickMagicArrowShaft_0047CA50"/>, deja ecrit.
    /// </para>
    /// <para>
    /// <b>Trois dwords lui sont pretes puis rendus</b> — <c>+0x30</c>, <c>+0x34</c> et
    /// <c>+0x04</c> —, ce qui emporte au passage le cap <c>+0x32</c> et le modele. Le globe reprend
    /// ensuite son apparence : blanc plein et echelle <c>0x0c00</c>. Meme geste que le pret de
    /// vitesse de Pieds mouilles, sur d'autres champs.
    /// </para>
    /// </summary>
    private static void TickLightGlobe_0047E370(byte* record)
    {
        byte* e = record;
        uint state = *(uint*)e;

        e[0x36] |= 6;

        if ((state & 0x2000000) != 0)
        {
            // ------------------------------------------------------------ la resorption
            short scale = *(short*)(e + 0x28);
            scale -= (short)(scale >> 2);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
            *(short*)(e + 0x2c) = scale;

            if (scale < 0x80) *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_0047e3b9 — l'heritage du bit de fin, puis les trois approches.
        byte* father = *(byte**)(e + 0x6c);
        byte* caster = *(byte**)(e + 0x74);

        state |= 0x80;
        *(uint*)e = (*(uint*)father & 0x2000000) | state;

        short current = *(short*)(e + 0x28);
        current += (short)((0x1000 - current) >> 2);
        *(short*)(e + 0x28) = current;
        *(short*)(e + 0x2a) = current;
        *(short*)(e + 0x2c) = current;

        int channel = e[0x38];
        channel += (0x80 - channel) >> 2;
        *(uint*)(e + 0x38) = (uint)(((channel << 8 | channel) << 8) | channel);

        int wasY = *(int*)(e + 0x20);
        *(int*)(e + 0x1c) += (*(int*)(caster + 0x1c) - *(int*)(e + 0x1c)) >> 2;

        int casterHead = StatusBubbleHeight_00538BC0((uint*)caster);
        *(int*)(e + 0x20) =
            (((((casterHead + 0x64) << 0xc) + *(int*)(caster + 0x20)) - wasY) >> 2) + wasY;

        *(int*)(e + 0x24) += (*(int*)(caster + 0x24) - *(int*)(e + 0x24)) >> 2;

        byte* target = FindNearestOpponent_00474EB0((uint*)caster, 0xffffffff, 1);

        if (target != null)
        {
            // ------------------------------------------------------------ la fleche
            // Trois dwords pretes : +0x30 emporte le cap +0x32, +0x04 le modele.
            uint kept30 = *(uint*)(e + 0x30);
            uint kept34 = *(uint*)(e + 0x34);
            uint kept04 = *(uint*)(e + 0x04);

            *(byte**)(e + 0x78) = target;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(uint*)(e + 0x28) = 0x06000800;
            *(ushort*)(e + 0x2c) = 0x0600;

            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            int targetHead = StatusBubbleHeight_00538BC0((uint*)target);
            int dy = (((targetHead & ~1) << 0xb) + *(int*)(target + 0x20) - *(int*)(e + 0x20)) >> 0xc;
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            int yaw = Winsys3d.Ordinal_457(dx, dz) + (CrtRand() & 0xfff) + 0xa00;
            *(short*)(e + 0x32) = (short)yaw;

            int pitch = Winsys3d.Ordinal_457(dy, flat) + 0x100;
            *(short*)(e + 0x34) = (short)pitch;

            SetVectorFromAngles_00532AD0(
                (int*)(e + 0x50), *(short*)(e + 0x32) - 0x400, (short)pitch, 0x20);

            e[0x04] = 0x14;                            // le modele de la fleche
            SpawnCastEntity_0047AAE0(e, 0x6d, e[0x0b], -1);

            *(uint*)(e + 0x30) = kept30;
            *(uint*)(e + 0x34) = kept34;
            *(uint*)(e + 0x04) = kept04;

            // Et le globe reprend son apparence.
            *(uint*)(e + 0x38) = 0x00ffffff;
            *(uint*)(e + 0x28) = 0x0c000c00;
            *(ushort*)(e + 0x2c) = 0x0c00;
        }

        // Ghidra: LAB_0047e56b
        if ((*(uint*)e & 0x40000000) != 0) *(uint*)e |= 0x2000000;
    }

    // =====================================================================
    // ECLAIR — le dix-septieme sort, ecole 0 indice 16, sous-type 0x10
    //
    // Trois corps neufs, et le QUATRIEME sort entretenu — le plus vorace : 1 PM toutes les DIX
    // images, soit deux fois la cadence des trois autres.
    //
    //   0x0047E590   LE SORT. Sous-type 0x10 de 0x00551c30, 342 octets.
    //   0x0047E6F0   LE FILAMENT, sous-type 0x64 de 0x00551c30 — la table des EFFETS, pas celle
    //                des incantations. 901 octets. Quatre exemplaires.
    //   0x0047EA80   L'ETINCELLE, sous-type 0x6f de 0x00551f30, 83 octets.
    //
    // LE COMPTE DE REFERENCES EST FAIT SUR LES NAISSANCES REUSSIES. Le sort appelle quatre fois
    // SpawnEffectEntity et ADDITIONNE ses retours dans +0x16 — la fonction rend 1 quand un
    // emplacement etait libre, 0 sinon. Le compte est donc exact meme si le pool est plein, la ou
    // Rocher posait un 4 en dur. Chaque filament decremente ce compte en mourant, et le sort
    // s'eteint quand il retombe a zero.
    //
    // ET LE FILAMENT A DEUX VIES, SEPAREES PAR UN BIT. Tant que 0x40 n'est pas pose sur son mot
    // d'etat il VOLE — vers sa cible a la vitesse 0x10, ou en errant pres du lanceur a la
    // vitesse 8 avec un bruit de +/-256 sur les deux axes horizontaux. Des qu'il touche, il pose
    // 0x40 et bascule : il reste accroche et decharge une etincelle UNE IMAGE SUR HUIT.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047e590 @ 0x0047e590 — <b>Eclair</b> : le sous-type <c>0x10</c>, creneau 16 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 342 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Le quatrieme sort entretenu, et le plus gourmand : <b>1 PM toutes les dix images</b> — deux
    /// fois la cadence de Wonder, Atchoum et Lumiere, qui ponctionnent toutes les vingt. Le reste
    /// est leur code commun.
    /// </para>
    /// <para>
    /// <b>Il compte ses filaments sur leurs naissances reussies.</b> Les quatre appels a
    /// <see cref="SpawnEffectEntity"/> rendent 1 ou 0 selon qu'un emplacement etait libre, et le
    /// sort <b>additionne</b> ces retours dans <c>+0x16</c>. Il ne s'eteint que lorsque ce compte
    /// retombe a zero, chaque filament le decrementant en mourant. C'est le compte de references
    /// de <see cref="TickRockCircle_0047B7A0"/>, en plus juste : un pool plein ne le fait pas
    /// attendre un enfant qui n'existe pas.
    /// </para>
    /// <para>
    /// Le decor blanchit a <c>0xc4</c> sur les trois canaux, comme Wonder.
    /// </para>
    /// </summary>
    private static void TickThunder_0047E590(byte* record)
    {
        byte* e = record;

        if (*(ushort*)(e + 0x10) != 0)
        {
            if (*(ushort*)(e + 0x16) == 0)
            {
                // Les quatre filaments sont morts : le sort avec eux.
                *(uint*)e = 0;
                *(uint*)(*(uint**)(e + 0x74)) &= 0xffffff7f;
                *(ushort*)Mem.Arena(0x005e9166) = 0;
                return;
            }
        }
        else
        {
            // Ghidra: LAB_0047e5c1 — la naissance.
            byte* born = *(byte**)(e + 0x74);
            *(uint*)born |= 0x80;                      // le lanceur est immobilise
            *(int*)(e + 0x78) = 0;

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
            *(int*)(e + 0x20) += (height & ~1) << 0xb;

            *(ushort*)(e + 0x16) = 0;

            // Le compte se fait sur ce que la naissance rend, pas sur le nombre d'appels.
            for (int strand = 0; strand < 4; strand++)
            {
                *(ushort*)(e + 0x16) += (ushort)SpawnEffectEntity(e, 0x64, 0, -1);
            }

            *(uint*)Mem.Arena(0x005e9160) = 0x00c400c4;
            *(uint*)Mem.Arena(0x005e9164) = 0x000200c4;

            Aud.PlayPositionalSound(0x2c, 0x329, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047e631 — l'entretien, la meme forme que les trois autres.
        if ((*(uint*)e & 0x2000000) == 0)
        {
            byte* caster = *(byte**)(e + 0x74);

            if ((*(uint*)e & 0x800000) != 0)
            {
                uint index = ((uint*)caster)[2] & 0xff;
                byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

                int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
                ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

                if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0) *(uint*)e |= 0x2000000;

                // DIX images, pas vingt : c'est le seul a cette cadence.
                if (*(uint*)Mem.Entity(0x005f2cf0) % 10 == 0)
                {
                    short mana = *(short*)(perChar + 0x14e);

                    if (mana >= 1)
                    {
                        *(short*)(perChar + 0x14e) = (short)(mana - 1);
                    }
                    else
                    {
                        *(ushort*)(perChar + 0x14e) = 0;
                        *(uint*)e |= 0x2000000;
                    }
                }
            }
            else if (*(short*)(e + 0x10) >= 0x78)
            {
                *(uint*)e |= 0x2000000;
            }
        }

        // Ghidra: LAB_0047e6ca
        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047e6f0 @ 0x0047e6f0 — <b>le filament d'Eclair</b> : le sous-type <c>0x64</c>,
    /// creneau 100 de <see cref="s_StaticEntityHandlers_00551c30"/> — <b>la table des effets</b>,
    /// celle ou <see cref="SpawnEffectEntity"/> depose, et non celle des incantations ou le meme
    /// numero designe le projectile de Balle de feu. 901 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Deux vies, separees par le bit <c>0x40</c> de son propre mot d'etat.</b>
    /// </para>
    /// <list type="bullet">
    /// <item><b>Libre</b> — il vole. Vers sa cible s'il en a une, a la vitesse <c>0x10</c> ; sinon
    /// il erre autour du lanceur a la vitesse <b>8</b>, en visant un point bruite de +/-256 unites
    /// sur les deux axes horizontaux et cent au-dessus de sa tete. Dans les deux cas la poussee
    /// s'<b>ajoute</b> a la vitesse, qui perd ensuite un huitieme — la meme mecanique que la fleche
    /// magique.</item>
    /// <item><b>Accroche</b> — il ne bouge plus et <b>decharge une image sur huit</b>
    /// (<c>+0x10 &amp; 7 == 1</c>) : une etincelle <c>0x6f</c> a un point quelconque a moins de 79
    /// unites de la victime, les degats, et le claquement en deux temps de
    /// <see cref="s_DelayedSoundScript_00552548"/>.</item>
    /// </list>
    /// <para>
    /// <b>Il herite du bit de fin de son pere</b> (<c>+0x6c</c>, masque <c>0x2000000</c>) et
    /// <b>decremente son compte</b> <c>+0x16</c> en mourant : le signal descend, le compte remonte.
    /// C'est le seul corps du depot qui fasse les deux.
    /// </para>
    /// <para>
    /// Sur le decor il lache l'eclaboussure <c>0xbd</c> de
    /// <see cref="s_Descriptors_00551A08"/> et relache sa victime.
    /// </para>
    /// </summary>
    private static void TickThunderStrand_0047E6F0(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { 0x0000, -0x003c, 0x003c, 0x00c8, 0x0000, 0x0078 };
        int* step = stackalloc int[3];

        if (*(ushort*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(ushort*)(e + 0x10) = 1;
            *(uint*)e |= 0x80;
            *(short*)(e + 0x2e) = -0x14;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4d, e[0x0b], -1);

            *(ushort*)(e + 0x12) = 0x1000;
            SpawnCastEntity_0047AAE0(e, 0x6a, e[0x0b], -1);

            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = 0;
        }

        // Ghidra: LAB_0047e781 — la fin rend son du au pere.
        byte* father = *(byte**)(e + 0x6c);

        if ((*(uint*)e & 0x2000000) != 0)
        {
            (*(ushort*)(father + 0x16))--;
            *(uint*)e = 0;
            return;
        }

        // L'heritage du bit de fin, et le drapeau de mort propre.
        *(uint*)e |= *(uint*)father & 0x2000000;
        if ((*(uint*)e & 0x40000000) != 0) *(uint*)e |= 0x2000000;

        if ((*(uint*)e & 0x40) != 0)
        {
            // ------------------------------------------------------------ accroche
            byte* held = *(byte**)(e + 0x78);

            if ((*(uint*)held & 0x50000000) != 0)
            {
                *(uint*)e |= 0x2000000;
                return;
            }

            if ((e[0x10] & 7) == 1)
            {
                int spray = CrtRand();
                int reach = CrtRand() & 0x4f;

                *(int*)(e + 0x1c) = *(int*)(held + 0x1c)
                                    + (int)Winsys3d.GetSinTableEntry(spray) * reach;
                *(int*)(e + 0x20) = *(int*)(held + 0x20);
                *(int*)(e + 0x24) = *(int*)(held + 0x24)
                                    + Winsys3d.GetCosTableEntry(spray) * reach;

                e[0x04] = 0;
                SpawnCastEntity_0047AAE0(e, 0x6f, e[0x0b], -1);

                ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)held, e[7], e[6], 0);

                *(uint*)held |= 0x40;
                *(ushort*)(held + 0x96) = 0x10;

                Aud.QueueDelayedSoundScript(DelayedSoundScript00552548(), (int*)(e + 0x1c));
            }

            *(ushort*)(e + 0x10) += 1;
            return;
        }

        // ------------------------------------------------------------ libre
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 1);

        int dx, dy, dz, speed;

        if (target != null)
        {
            if ((*(uint*)target & 0x50000000) != 0)
            {
                *(uint*)(target + 0x74) = 0;
                *(uint*)e |= 0x2000000;
            }

            // Ghidra: LAB_0047e8b2
            *(byte**)(e + 0x78) = target;
            dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            *(byte**)(target + 0x74) = e;

            int head = StatusBubbleHeight_00538BC0((uint*)target);
            dy = (head >> 1) + ((*(int*)(target + 0x20) - *(int*)(e + 0x20)) >> 0xc);
            dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            speed = 0x10;
        }
        else
        {
            // Ghidra: LAB_0047e8f3 — l'errance autour du lanceur, bruitee.
            byte* caster = *(byte**)(e + 0x74);

            dx = (CrtRand() & 0x1ff)
                 + ((*(int*)(caster + 0x1c) - *(int*)(e + 0x1c)) >> 0xc) - 0x100;

            int head = StatusBubbleHeight_00538BC0((uint*)caster);
            dy = head + ((*(int*)(caster + 0x20) - *(int*)(e + 0x20)) >> 0xc) + 0x64;

            dz = (CrtRand() & 0x1ff)
                 + ((*(int*)(caster + 0x24) - *(int*)(e + 0x24)) >> 0xc) - 0x100;
            speed = 8;
        }

        // Ghidra: LAB_0047e951 — la poussee s'ajoute, puis la vitesse perd un huitieme.
        int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int pitch = Winsys3d.Ordinal_457(dy, flat);
        int yaw = Winsys3d.Ordinal_457(dx, dz);

        SetVectorFromAngles_00532AD0(step, yaw, pitch, speed);

        int vx = *(int*)(e + 0x50) + step[0];
        int vy = *(int*)(e + 0x54) + step[1];
        int vz = *(int*)(e + 0x58) + step[2];

        vx -= vx >> 3;
        vy -= vy >> 3;
        vz -= vz >> 3;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        FinalizeEntityPlacement2((uint*)e);            // Ghidra: FUN_00533940

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60))
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)(e + 0x90) = 0x800;
        }

        // Ghidra: LAB_0047e9f4 — le decor l'eteint.
        uint contact = *(uint*)(e + 0x90);

        if ((contact & 0xa000) == 0 && (contact & 0x802) != 0)
        {
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551A08(), 0xbd, 0, 4);

            byte* held = *(byte**)(e + 0x78);
            if (held != null) *(uint*)(held + 0x74) = 0;

            *(uint*)e |= 0x2000000;
            return;
        }

        // Ghidra: LAB_0047ea3a — le contact pose 0x40 et le fait basculer.
        if (*(byte**)(e + 0x78) == null) return;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);
        if (victim == null) return;

        *(uint*)e |= 0x1000040;
        *(uint*)victim |= 0x40;
        *(ushort*)(victim + 0x96) = 0x10;
    }

    /// <summary>
    /// Ghidra: FUN_0047ea80 @ 0x0047ea80 — <b>l'etincelle d'Eclair</b> : le sous-type <c>0x6f</c>,
    /// creneau 111 de <see cref="s_StaticEntityHandlers_00551f30"/>. 83 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Elle ne se deplace pas : elle nait large (<c>0x1000</c>, avec le mode <c>0x6000</c> dans les
    /// seize bits hauts de <c>+0x28</c>), tourne de <c>0x40</c> par image, perd un quart de sa
    /// taille et s'eteint sous <c>0x100</c> — une dizaine d'images.
    /// </para>
    /// </summary>
    private static void TickThunderSpark_0047EA80(byte* record)
    {
        byte* e = record;
        ushort age = *(ushort*)(e + 0x10);

        if (age == 0)
        {
            *(uint*)(e + 0x28) = 0x60001000;
            *(ushort*)(e + 0x2c) = 0x1000;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
        }
        else
        {
            short scale = *(short*)(e + 0x28);
            scale -= (short)(scale >> 2);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2c) = scale;
        }

        // Ghidra: LAB_0047eab9
        *(ushort*)(e + 0x32) += 0x40;
        if (*(short*)(e + 0x28) < 0x100) *(uint*)e = 0;

        *(ushort*)(e + 0x10) = (ushort)(age + 1);
    }

    // =====================================================================
    // TROUILLE — le dix-huitieme sort, ecole 0 indice 17, sous-type 0x11
    //
    // Trois corps neufs, et le CINQUIEME sort entretenu (3 PM par vingt images).
    //
    //   0x0047EAE0   LE SORT. Sous-type 0x11 de 0x00551c30, 323 octets.
    //   0x0047EC30   LE FANTOME, sous-type 0x89 de 0x00551f30, 808 octets. Un seul.
    //   0x0047EF60   LA LUCIOLE, sous-type 0x8a de 0x00551f30, 260 octets.
    //
    // LE BIT 0x02 EST UN SIGNAL QUI REMONTE, ET IL DEBLOQUE LE SORT. Le fantome nait gros et
    // retrecit ; quand son echelle Y passe sous 0x2040 il se pose 0x02, POSE LE MEME BIT SUR SON
    // PERE, et relache le lanceur. Tant que le sort ne l'a pas, il ne lit ni la manette ni les PM,
    // et surtout il refuse de mourir : la branche de fin commence par `test al,2` et rend la main
    // si le bit manque. Le sort attend donc que son fantome soit sorti avant d'avoir le droit de
    // s'arreter.
    //
    // ET LE FANTOME TOURNE PAR LE PLUS COURT CHEMIN. L'ecart d'angle est ramene sur douze bits,
    // puis compare a 0x800 : au-dela il vaut mieux tourner dans l'autre sens, et le binaire nie le
    // huitieme. C'est le seul corps transcrit qui fasse ce choix.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047eae0 @ 0x0047eae0 — <b>Trouille</b> : le sous-type <c>0x11</c>, creneau 17 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 323 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Le cinquieme sort entretenu — <b>3 PM toutes les vingt images</b> — avec une difference de
    /// fond : <b>rien ne se passe tant que le bit <c>0x02</c> n'est pas pose</b>. Ni la manette, ni
    /// les PM, ni meme la fin : la branche de sortie commence par le tester et rend la main s'il
    /// manque. C'est <see cref="TickFearGhost_0047EC30"/> qui le pose, une fois sorti.
    /// </para>
    /// <para>
    /// Une fois le bit la et <c>0x2000000</c> pose, le sort compte encore une image dans
    /// <c>+0x12</c> avant de s'eteindre — le temps que le fantome se resorbe.
    /// </para>
    /// </summary>
    private static void TickFear_0047EAE0(byte* record)
    {
        byte* e = record;
        uint state = *(uint*)e;

        if ((state & 0x2000000) != 0)
        {
            // Sans le signal du fantome, il ne meurt meme pas.
            if ((state & 2) == 0) return;

            ushort delay = *(ushort*)(e + 0x12);
            if (delay != 0) *(uint*)e = 0;
            *(ushort*)(e + 0x12) = (ushort)(delay + 1);
            return;
        }

        byte* caster = *(byte**)(e + 0x74);

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(uint*)(e + 0x38) = 0;
            *(uint*)(e + 0x28) = 0x30001400;
            *(ushort*)(e + 0x2c) = 0x1400;
            e[0x04] = 0x1f;                            // le modele

            SpawnCastEntity_0047AAE0(e, 0x89, e[0x0b], -1);

            *(int*)(e + 0x78) = 0;
            *(uint*)caster |= 0x80;                    // le lanceur est immobilise

            Aud.PlayPositionalSound(0x1a, 0x6fd, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047eb6e — l'entretien n'existe qu'une fois le fantome sorti.
        if ((*(uint*)e & 2) != 0)
        {
            if ((*(uint*)e & 0x800000) != 0)
            {
                uint index = ((uint*)caster)[2] & 0xff;
                byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

                int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
                ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

                if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0) *(uint*)e |= 0x2000000;

                if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
                {
                    short mana = *(short*)(perChar + 0x14e);

                    if (mana >= 3)
                    {
                        *(short*)(perChar + 0x14e) = (short)(mana - 3);
                    }
                    else
                    {
                        *(ushort*)(perChar + 0x14e) = 0;
                        *(uint*)e |= 0x2000000;
                    }
                }
            }
            else if (*(short*)(e + 0x10) >= 0x78)
            {
                *(uint*)e |= 0x2000000;
            }
        }

        // Ghidra: LAB_0047ec08
        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047ec30 @ 0x0047ec30 — <b>le fantome de Trouille</b> : le sous-type <c>0x89</c>,
    /// creneau 137 de <see cref="s_StaticEntityHandlers_00551f30"/>. 808 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il commence par sortir.</b> Ne son echelle Y a <c>0x3000</c>, il l'approche de
    /// <c>0x1800</c> d'un huitieme et s'eclaircit ; des qu'elle passe sous <c>0x2040</c> il se pose
    /// le bit <c>0x02</c>, <b>le pose sur son pere</b>, et relache le lanceur. Le sort ne peut ni
    /// entretenir ni mourir avant cet instant.
    /// </para>
    /// <para>
    /// <b>Ensuite il chasse.</b> Il colle au lanceur — position approchee d'un quart, hauteur d'un
    /// huitieme — sauf quand <c>+0x90</c> porte <c>0x8000</c>, et recopie <b>six dwords de
    /// <c>+0x7c</c></b> du lanceur a chaque image, ce que rien d'autre dans le depot ne fait. Il
    /// prend l'adversaire le plus proche, le garde <b>quarante images</b> au plus, puis en cherche
    /// un autre.
    /// </para>
    /// <para>
    /// <b>Il tourne par le plus court chemin</b> : l'ecart d'angle est ramene sur douze bits, et
    /// au-dela de <c>0x800</c> le huitieme est <b>nie</b> — tourner a gauche plutot que faire
    /// presque un tour a droite. C'est le seul corps transcrit qui fasse ce choix.
    /// </para>
    /// <para>
    /// <b>Et il crache une luciole par image</b> vers sa proie, a une vitesse proportionnelle a la
    /// distance (le huitieme de la norme 3D). Six champs lui sont pretes puis rendus — la position
    /// entiere, l'echelle et le modele.
    /// </para>
    /// </summary>
    private static void TickFearGhost_0047EC30(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            // ------------------------------------------------------------ la resorption
            short x = *(short*)(e + 0x28);
            *(short*)(e + 0x28) = (short)(x - (x >> 3));

            short z = *(short*)(e + 0x2c);
            *(short*)(e + 0x2c) = (short)(z - (z >> 3));

            short spin = *(short*)(e + 0x12);
            *(ushort*)(e + 0x32) += (ushort)(spin << 6);

            spin++;
            *(short*)(e + 0x12) = spin;

            if (spin >= 0x14)
            {
                *(uint*)e = 0;
                byte* held = *(byte**)(e + 0x78);
                if (held != null) *(uint*)(held + 0x74) = 0;
            }

            return;
        }

        // Ghidra: LAB_0047eca1 — six dwords recopies du lanceur, a chaque image.
        byte* caster = *(byte**)(e + 0x74);
        Mem.CopyDwords((uint*)(caster + 0x7c), (uint*)(e + 0x7c), 6);

        if ((*(uint*)(e + 0x90) & 0x8000) == 0)
        {
            *(int*)(e + 0x1c) += (*(int*)(caster + 0x1c) - *(int*)(e + 0x1c)) >> 2;

            int head = StatusBubbleHeight_00538BC0((uint*)caster);
            int wasY = *(int*)(e + 0x20);
            *(int*)(e + 0x20) =
                ((((head & ~1) << 0xb) + *(int*)(caster + 0x20) - wasY) >> 3) + wasY;

            *(int*)(e + 0x24) += (*(int*)(caster + 0x24) - *(int*)(e + 0x24)) >> 2;
        }

        // Ghidra: LAB_0047ed01
        if ((*(uint*)e & 2) == 0)
        {
            // ------------------------------------------------------------ il sort
            uint tint = *(uint*)(e + 0x38);
            if (tint < 0x00f0f0f0) *(uint*)(e + 0x38) = tint + 0x00101010;

            short height = *(short*)(e + 0x2a);
            height += (short)((0x1800 - height) >> 3);
            *(short*)(e + 0x2a) = height;

            if (height < 0x2040)
            {
                *(uint*)e |= 2;
                *(uint*)(*(uint**)(e + 0x6c)) |= 2;    // le signal remonte au sort
                *(uint*)caster &= 0xffffff7f;          // et le lanceur est rendu a lui-meme
            }

            return;
        }

        // ------------------------------------------------------------ il chasse
        *(uint*)e |= *(uint*)(*(byte**)(e + 0x6c)) & 0x2000000;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            Aud.PlayPositionalSound(0x31, 0x16a, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047ed39 — la proie, gardee quarante images au plus.
        byte* prey = *(byte**)(e + 0x78);

        if (prey != null)
        {
            *(ushort*)(e + 0x10) += 1;

            if ((*(uint*)prey & 0x50000000) != 0 || *(short*)(e + 0x10) >= 0x28)
            {
                *(uint*)(prey + 0x74) = 0;
                *(int*)(e + 0x78) = 0;
                *(ushort*)(e + 0x10) = 0;
                prey = null;
            }
        }
        else
        {
            prey = FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 1);
            if (prey != null) Aud.PlayPositionalSound(0x23, 0x1000, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047ed96 — le cap vise.
        int wanted;

        if (prey != null)
        {
            *(byte**)(e + 0x78) = prey;
            *(byte**)(prey + 0x74) = e;

            wanted = Winsys3d.Ordinal_457(
                (*(int*)(prey + 0x1c) - *(int*)(e + 0x1c)) >> 0xc,
                (*(int*)(prey + 0x24) - *(int*)(e + 0x24)) >> 0xc);
        }
        else
        {
            wanted = *(ushort*)(caster + 0x32);
        }

        // Ghidra: LAB_0047edc6 — LE PLUS COURT CHEMIN : au-dela d'un demi-tour, on nie.
        int gap = (wanted - *(short*)(e + 0x32)) & 0xfff;
        int turn = gap < 0x800 ? gap >> 3 : -((0x1000 - gap) >> 3);
        *(ushort*)(e + 0x32) += (ushort)turn;

        int heading = *(short*)(e + 0x32);
        if (prey == null) return;

        // ------------------------------------------------------------ la luciole
        int keptX = *(int*)(e + 0x1c);
        int keptY = *(int*)(e + 0x20);
        int keptZ = *(int*)(e + 0x24);

        // 30 unites d'avance (`lea *3 ; lea *5 ; shl 1`) et un bond vertical.
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 30;
        *(int*)(e + 0x20) += 0x104000;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 30;

        int dx = (*(int*)(prey + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dy = (*(int*)(prey + 0x20) - *(int*)(e + 0x20)) >> 0xc;
        int dz = (*(int*)(prey + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int pitch = Winsys3d.Ordinal_457(dy, flat);

        // La norme a TROIS dimensions : c'est elle qui donne la vitesse.
        int span = (int)Winsys3d.Ordinal_442((uint)(dy * dy + dx * dx + dz * dz));

        uint keptScale = *(uint*)(e + 0x28);
        ushort keptDepth = *(ushort*)(e + 0x2c);
        uint keptModel = *(uint*)(e + 0x04);

        *(uint*)(e + 0x28) = 0x08000400;
        *(ushort*)(e + 0x2c) = 0x0400;

        SetVectorFromAngles_00532AD0((int*)(e + 0x50), heading, pitch, span >> 3);

        e[0x04] = 0x1e;
        SpawnCastEntity_0047AAE0(e, 0x8a, 0, 4);

        *(uint*)(e + 0x28) = keptScale;
        *(ushort*)(e + 0x2c) = keptDepth;
        *(uint*)(e + 0x04) = keptModel;

        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x20) = keptY;
        *(int*)(e + 0x24) = keptZ;
    }

    /// <summary>
    /// Ghidra: FUN_0047ef60 @ 0x0047ef60 — <b>la luciole de Trouille</b> : le sous-type
    /// <c>0x8a</c>, creneau 138 de <see cref="s_StaticEntityHandlers_00551f30"/>. 260 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle enfle en volant.</b> Sa taille vaut <c>(age² + 8) &lt;&lt; 7</c> plus un bruit sur
    /// dix bits — donc quadratique : discrete au depart, large a la huitieme et derniere image.
    /// </para>
    /// <para>
    /// <b>Deux details du binaire ecrits tels quels</b> : l'echelle Y est <c>eax &lt;&lt; 17</c> et
    /// non <c>&lt;&lt; 16</c>, ce qui la fait valoir le double de l'echelle X ; et le sol est lu en
    /// <c>+0x64</c>, non en <c>+0x60</c> comme partout ailleurs.
    /// </para>
    /// </summary>
    private static void TickFearWisp_0047EF60(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { 0x0000, 0x0000, 0x003c, 0x00c8, 0x0000, 0x0078 };

        int age = *(short*)(e + 0x10);
        int size = (CrtRand() & 0x3ff) + ((age * age + 8) << 7);

        *(short*)(e + 0x2c) = (short)size;

        // `shl edx,11h` : DIX-SEPT bits, pas seize. L'echelle Y vaut donc le double de la X.
        *(uint*)(e + 0x28) = ((uint)size << 0x11) | (uint)size;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        int newY = *(int*)(e + 0x20) + *(int*)(e + 0x54);
        *(int*)(e + 0x20) = newY;
        *(int*)(e + 0x24) += *(int*)(e + 0x58);

        e[0x3b] = 0xc0;

        FinalizeEntityPlacement2((uint*)e);            // Ghidra: FUN_00533940

        // Le sol est lu en +0x64 ici, et non en +0x60 comme dans tous les autres corps.
        int ground = *(int*)(e + 0x64);
        *(int*)(e + 0x20) = newY;

        if (newY <= ground)
        {
            *(int*)(e + 0x20) = ground;
            *(uint*)(e + 0x90) = 0x800;
        }

        if ((*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;
        }

        *(ushort*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 8) *(uint*)e = 0;
    }

    // =====================================================================
    // EXORCISTE — le dix-neuvieme sort, ecole 0 indice 18, sous-type 0x12
    //
    // Deux corps neufs, dont le plus court du depot : trente octets.
    //
    //   0x0047F070   LE SORT. Sous-type 0x12 de 0x00551c30, 532 octets. Sixieme sort entretenu,
    //                a 4 PM par vingt images.
    //   0x0047F290   LE HALO, sous-type 0x84 de 0x00551f30, 30 octets. Un par image.
    //
    // IL NE FRAPPE PAS, IL NETTOIE. A chaque image il balaie autour de lui et appelle
    // ClearAllStatusEffects_0053C330 — l'entree 29 de la table des 48 — sur ce qu'il trouve. Faute
    // de cible, il se rabat sur le LANCEUR : le sort se nettoie donc lui-meme quand il ne trouve
    // personne, ce qui est visiblement l'usage voulu.
    //
    // ET SA SONDE ENFLE AVEC LUI. L'echelle bat au rythme d'un cosinus ; l'avance et la profondeur
    // de la sonde en sont tirees a chaque image — sept dixiemes en arriere, quatorze fois en avant.
    // C'est la troisieme sonde variable du depot, apres l'ombre de Tenebres et le segment de Coup
    // de jus, et la seule qui PULSE au lieu de croitre.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047f070 @ 0x0047f070 — <b>Exorciste</b> : le sous-type <c>0x12</c>, creneau 18
    /// de <see cref="s_StaticEntityHandlers_00551c30"/>. 532 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il bat.</b> Sa teinte suit <c>sin(age &lt;&lt; 9) &gt;&gt; 6 + 0xbf</c> et son echelle
    /// <c>cos(age &lt;&lt; 7) &gt;&gt; 1 + 0x3000</c> — deux sinusoides de periodes differentes, ce
    /// qui donne une pulsation qui ne se repete pas. La hauteur, elle, vaut la moitie de la
    /// largeur.
    /// </para>
    /// <para>
    /// <b>Le balayage est ecrit sur place</b>, pas via
    /// <see cref="ProbeMeleeTarget_00474CA0"/> : la sonde est particuliere — un demi-tour de
    /// decalage de cap, aucune ouverture, et une avance et une profondeur <b>tirees de l'echelle
    /// courante</b> — et l'acteur est le sort lui-meme et non son <c>+0x74</c>.
    /// </para>
    /// <para>
    /// <b>Faute de cible, il prend le lanceur.</b> C'est ce qui fait de lui un sort de soin autant
    /// que d'attaque : <see cref="ClearAllStatusEffects_0053C330"/> s'applique alors a celui qui le
    /// lance.
    /// </para>
    /// <para>
    /// Sixieme sort entretenu, a <b>4 PM toutes les vingt images</b> — le meme prix qu'Atchoum.
    /// </para>
    /// </summary>
    private static void TickExorcism_0047F070(byte* record)
    {
        byte* e = record;
        byte* caster = *(byte**)(e + 0x74);

        // Un demi-tour de decalage de cap, et une ouverture de 0x155. Les mots 1 et 5 sont
        // recalcules plus bas depuis l'echelle.
        short* probe = stackalloc short[6] { -0x0800, 0x0000, 0x0000, 0x0096, 0x0155, 0x0000 };

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & 0xfffe) | 6);
            *(short*)(e + 0x2e) = -0x40;
            e[0x04] = 0x0c;                            // le modele

            Aud.QueueDelayedSoundScript(DelayedSoundScript00552558(), (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047f0e3 — les deux battements, de periodes differentes.
        int age = *(short*)(e + 0x10);
        *(ushort*)(e + 0x32) += 0x80;

        int channel = ((int)Winsys3d.GetSinTableEntry(age << 9) >> 6) + 0xbf;
        *(uint*)(e + 0x38) = (uint)(((channel << 8 | channel) << 8) | channel);

        int scale = (Winsys3d.GetCosTableEntry(age << 7) >> 1) + 0x3000;
        *(short*)(e + 0x28) = (short)scale;
        *(short*)(e + 0x2a) = (short)(scale >> 1);     // la hauteur vaut la moitie
        *(short*)(e + 0x2c) = (short)scale;

        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
        *(int*)(e + 0x24) = *(int*)(caster + 0x24);

        SpawnCastEntity_0047AAE0(e, 0x84, e[0x0b], -1);

        // La sonde suit l'echelle : sept dixiemes en arriere, quatorze fois plus en avant.
        int reach = scale * 7;
        probe[1] = (short)((-reach * 10) >> 0xc);
        probe[5] = (short)((reach * 20) >> 0xc);

        // Le balayage, ecrit sur place : l'acteur est le SORT, pas son lanceur.
        byte* found = (*(uint*)e & 0x800000) != 0
            ? SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0x800)
            : SweepSceneEntitiesForMeleeHit_00519AF0(e, (int*)(e + 0x1c), probe, 0x800);

        // Faute de cible, c'est le lanceur qu'il nettoie.
        if (found == null) found = *(byte**)(e + 0x74);
        *(byte**)(e + 0x78) = found;

        *(ushort*)(caster + 6) = *(ushort*)(e + 6);
        ClearAllStatusEffects_0053C330((uint*)caster, (uint*)found, 0);

        // Ghidra: LAB_0047f1d7 — l'entretien, la meme forme que les cinq autres.
        if ((*(uint*)e & 0x800000) != 0)
        {
            uint index = ((uint*)caster)[2] & 0xff;
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0) *(uint*)e |= 0x2000000;

            if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
            {
                short mana = *(short*)(perChar + 0x14e);

                if (mana >= 4)
                {
                    *(short*)(perChar + 0x14e) = (short)(mana - 4);
                }
                else
                {
                    *(ushort*)(perChar + 0x14e) = 0;
                    *(uint*)e |= 0x2000000;
                }
            }
        }
        else if (*(short*)(e + 0x10) >= 0x78)
        {
            *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_0047f265
        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047f290 @ 0x0047f290 — <b>le halo d'Exorciste</b> : le sous-type <c>0x84</c>,
    /// creneau 132 de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>30 octets</b> — le plus
    /// court corps de sort du depot, <b>absent de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Il ne fait rien d'autre que s'effacer : il herite de tout du sort a sa naissance — position,
    /// echelle, teinte, modele — et perd <c>0x20</c> par canal a chaque image. Six images.
    /// </para>
    /// <para>
    /// <b>Le test porte sur le canal bas seul</b>, en non signe : sous <c>0x20</c> il s'eteint,
    /// avant comme apres la soustraction. Le double test evite qu'un canal deja bas ne repasse
    /// au-dessus par emprunt sur le canal voisin.
    /// </para>
    /// </summary>
    private static void TickExorcismHalo_0047F290(byte* record)
    {
        byte* e = record;
        uint tint = *(uint*)(e + 0x38);

        if ((byte)tint < 0x20)
        {
            *(uint*)e = 0;
            return;
        }

        tint -= 0x202020;
        *(uint*)(e + 0x38) = tint;

        if ((byte)tint < 0x20) *(uint*)e = 0;
    }

    // =====================================================================
    // DURDUR — le vingtieme sort, ecole 0 indice 19, sous-type 0x13
    //
    // Trois corps neufs. Il n'est PAS entretenu, malgre son apparence : c'est sa coquille qui lui
    // dit quand partir.
    //
    //   0x0047F2B0   LE SORT. Sous-type 0x13 de 0x00551c30, 336 octets.
    //   0x0047F400   LA COQUILLE, sous-type 0x85 de 0x00551f30, 98 octets. Elle enfle, et quand
    //                elle arrive a sa taille elle marque son pere.
    //   0x0047F470   L'ONDE, sous-type 0x86 de 0x00551f30, 246 octets. Elle porte l'effet.
    //
    // ET IL REEMPLOIE L'ANNEAU D'ONDE DE CHOC — le sous-type 0x6c de TickShockwaveRing_0047C830 —
    // en posant son ancre +0x70 A ZERO. C'est le premier appelant a emprunter la branche « sans
    // ancre » de ce corps, celle qui replie la position dans l'ecart et fait de l'anneau sa propre
    // reference. Elle etait ecrite « parce qu'elle est la » ; elle sert.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047f2b0 @ 0x0047f2b0 — <b>Durdur</b> : le sous-type <c>0x13</c>, creneau 19 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 336 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Deux images utiles, separees par une attente.</b> A la premiere il immobilise le lanceur,
    /// se place a mi-hauteur de sa tete — et <b>a la meme distance devant lui</b>, la demi-hauteur
    /// servant a la fois d'altitude et de portee —, puis lache la coquille <c>0x85</c>. Ensuite il
    /// ne fait plus que compter, jusqu'a ce que la coquille lui pose <c>0x2000000</c>.
    /// </para>
    /// <para>
    /// <b>A ce signal il lache deux enfants et s'eteint</b> : l'anneau <c>0x6c</c> d'Onde de choc,
    /// avec l'ancre <c>+0x70</c> <b>a zero</b> — la branche sans ancre de
    /// <see cref="TickShockwaveRing_0047C830"/>, jamais empruntee jusqu'ici — et l'onde
    /// <c>0x86</c>, qui portera l'effet.
    /// </para>
    /// <para>
    /// Les six champs <c>+0x44</c>..<c>+0x58</c> sont poses avant chaque naissance : ecart nul et
    /// trois echelles visees pour l'anneau, puis une seule hauteur (<c>0x369d</c>) pour l'onde.
    /// </para>
    /// </summary>
    private static void TickHardening_0047F2B0(byte* record)
    {
        byte* e = record;
        byte* caster = *(byte**)(e + 0x74);

        if ((*(uint*)e & 0x2000000) != 0)
        {
            // ------------------------------------------------------------ le signal recu
            unchecked { *(ushort*)(e + 0x32) += 0xfc00; }   // -0x400

            // L'anneau d'Onde de choc, SANS ancre : il deviendra sa propre reference.
            *(int*)(e + 0x70) = 0;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;
            *(int*)(e + 0x50) = 0x1000;
            *(int*)(e + 0x54) = 0x1800;
            *(int*)(e + 0x58) = 0x1000;
            *(uint*)(e + 0x38) = 0x00808080;
            *(int*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(ushort*)(e + 0x34) = 0x400;
            e[0x04] = 7;

            SpawnCastEntity_0047AAE0(e, 0x6c, e[0x0b], -1);

            // Puis l'onde, avec une seule des trois echelles.
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0x369d;
            *(int*)(e + 0x58) = 0;
            *(int*)(e + 0x28) = 0x1000;
            *(ushort*)(e + 0x2c) = 0x1000;
            e[0x04] = 5;

            SpawnCastEntity_0047AAE0(e, 0x86, e[0x0b], -1);

            *(ushort*)(e + 0x10) += 1;
            *(uint*)e = 0;
            return;
        }

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(uint*)caster |= 0x80;                    // le lanceur est immobilise

            // La demi-hauteur de tete sert a la fois de portee et d'altitude.
            int half = StatusBubbleHeight_00538BC0((uint*)caster) >> 1;
            int heading = *(short*)(e + 0x32);

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * half;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * half;
            *(int*)(e + 0x20) += half << 0xc;

            *(ushort*)(e + 0x14) = 2;                  // le decalage d'approche de la coquille
            e[0x04] = 2;
            *(uint*)(e + 0x38) = 0;
            *(int*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(ushort*)(e + 0x12) = 0x800;              // et sa taille visee

            SpawnCastEntity_0047AAE0(e, 0x85, e[0x0b], -1);

            Aud.PlayPositionalSound(0x10, 0x800, 0x1200, (int*)(e + 0x1c));
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047f400 @ 0x0047f400 — <b>la coquille de Durdur</b> : le sous-type <c>0x85</c>,
    /// creneau 133 de <see cref="s_StaticEntityHandlers_00551f30"/>. 98 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Elle enfle vers la taille <c>+0x12</c> que le sort lui a donnee, d'un <b>decalage</b> que
    /// <c>+0x14</c> porte — un <c>sar</c> a compte variable, comme celui de l'etincelle de trainee.
    /// Elle s'eclaircit du meme mouvement.
    /// </para>
    /// <para>
    /// <b>Des qu'elle arrive a moins de <c>0x10</c> de sa taille</b>, elle pose <c>0x2000000</c>
    /// sur le mot d'etat de son <b>pere</b> et s'eteint. C'est le troisieme signal remontant du
    /// depot, apres la lame d'Assaut magique et le fantome de Trouille — et le seul dont le
    /// declencheur soit une <b>taille atteinte</b> plutot qu'une horloge.
    /// </para>
    /// </summary>
    private static void TickHardeningShell_0047F400(byte* record)
    {
        byte* e = record;

        int wanted = *(short*)(e + 0x12);
        short current = *(short*)(e + 0x28);
        int shift = e[0x14];

        *(ushort*)(e + 0x32) += 0x40;

        short next = (short)(((wanted - current) >> (shift & 0x1f)) + current);
        *(short*)(e + 0x28) = next;
        *(short*)(e + 0x2a) = next;
        *(short*)(e + 0x2c) = next;

        uint tint = *(uint*)(e + 0x38);
        if (tint < 0x00f0f0f0) *(uint*)(e + 0x38) = tint + 0x00101010;

        // Ghidra: LAB_0047f442 — a moins de 0x10 du but, elle previent son pere.
        if (next >= wanted - 0x10)
        {
            *(uint*)(*(uint**)(e + 0x6c)) |= 0x2000000;
            *(uint*)e = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0047f470 @ 0x0047f470 — <b>l'onde de Durdur</b> : le sous-type <c>0x86</c>,
    /// creneau 134 de <see cref="s_StaticEntityHandlers_00551f30"/>. 246 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Ses trois echelles approchent trois cibles distinctes d'un quart</b>, rangees dans les
    /// champs de vitesse <c>+0x50</c>, <c>+0x54</c> et <c>+0x58</c> que le sort a detournes pour
    /// l'occasion. Seule la hauteur en a une non nulle : l'onde s'aplatit en montant.
    /// </para>
    /// <para>
    /// <b>Et sa profondeur de sonde suit cette hauteur</b> : <c>(hauteur * 150) &gt;&gt; 12</c>
    /// plus <c>0x28</c>, recalculee a chaque image. C'est la quatrieme sonde variable du depot.
    /// </para>
    /// <para>
    /// L'effet est <see cref="ApplyStatusEffect_0053B8F0"/>, l'entree 13 de la table des 48 — et
    /// <b>quand il prend, la victime est clouee sur place</b> : ses deux vitesses horizontales sont
    /// mises a zero. Le clignotement est de 8 images, la moitie de l'ordinaire.
    /// </para>
    /// <para>
    /// C'est elle qui relache le lanceur, quand sa teinte s'epuise.
    /// </para>
    /// </summary>
    private static void TickHardeningBurst_0047F470(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { 0x0400, -0x0028, 0x003c, 0x0064, 0x0000, 0x0000 };

        // Les trois echelles, chacune vers sa cible, d'un quart.
        short x = *(short*)(e + 0x28);
        *(short*)(e + 0x28) = (short)(((*(int*)(e + 0x50) - x) >> 2) + x);

        short y = *(short*)(e + 0x2a);
        short newY = (short)(((*(int*)(e + 0x54) - y) >> 2) + y);
        *(short*)(e + 0x2a) = newY;

        short z = *(short*)(e + 0x2c);
        *(short*)(e + 0x2c) = (short)(((*(int*)(e + 0x58) - z) >> 2) + z);

        // La profondeur suit la hauteur : `*3 *5 *5 *2` donne 150, puis `sar 12` et `+0x28`.
        probe[5] = (short)(((newY * 150) >> 0xc) + 0x28);

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            byte* caster = *(byte**)(e + 0x74);
            *(ushort*)(caster + 6) = *(ushort*)(e + 6);

            if (ApplyStatusEffect_0053B8F0((uint*)caster, (uint*)victim, 0) != 0)
            {
                // L'effet a pris : la victime est clouee sur place.
                *(int*)(victim + 0x50) = 0;
                *(int*)(victim + 0x58) = 0;
            }

            *(ushort*)(victim + 0x96) = 8;
            *(uint*)victim |= 0x40;
        }

        // Ghidra: LAB_0047f543 — et c'est elle qui rend le lanceur a lui-meme.
        uint tint;
        unchecked { tint = *(uint*)(e + 0x38) + 0xffefeff0; }   // -0x101010
        *(uint*)(e + 0x38) = tint;

        if (tint == 0)
        {
            *(uint*)(*(uint**)(e + 0x74)) &= 0xffffff7f;
            *(uint*)e = 0;
        }
    }

    // =====================================================================
    // BOULE DE FEU — le vingt-et-unieme sort, ecole 0 indice 20, sous-type 0x14
    //
    // Quatre corps, dont trois neufs, et le plus gros sort du depot : 1425 octets pour le seul
    // corps principal.
    //
    //   0x0047F570   LE SORT. Sous-type 0x14 de 0x00551c30. QUATRE ages successifs.
    //   0x0047FB10   LA COQUE, sous-type 0x51 de 0x00552230, 112 octets.
    //   0x00482EF0   LA TRAINEE, sous-type 0x50 de 0x00552230, 167 octets.
    //   0x00475FC0   L'ECLAT 0xbe — deja ecrit et deja exerce.
    //
    // IL PORTE DEUX SONDES ET CHOISIT ENTRE ELLES. Les deux blocs de six mots se suivent sur la
    // pile, et la fin du corps calcule `esp + eax*12 + 0x1c` : une sonde etroite (50 de portee)
    // tant que la boule vole, une large (512) pendant l'explosion. C'est le seul corps du depot a
    // CHOISIR sa sonde plutot qu'a la modifier.
    //
    // ET IL SECOUE L'ECRAN. L'explosion pose 8 dans DAT_005de0ac — le decalage vertical de camera
    // qu'EntityHandler_00476730 amortit — puis le sort le ronge lui-meme de trois quarts par image
    // pendant ses dix dernieres. Il peint aussi le fond en rouge, DIRECTEMENT dans
    // s_BackColour*_005e93a0, sans passer par le mode 2 des autres sorts.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047f570 @ 0x0047f570 — <b>Boule de feu</b> : le sous-type <c>0x14</c>, creneau
    /// 20 de <see cref="s_StaticEntityHandlers_00551c30"/>. <b>1425 octets</b>, le plus gros corps
    /// de sort du depot, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para><b>Quatre ages, dans l'ordre :</b></para>
    /// <list type="number">
    /// <item><b>0 a 9</b> — elle se forme. A la premiere image elle se place a mi-hauteur de tete
    /// du lanceur et a la meme distance devant lui, et lache la coque <c>0x51</c> ; a chaque image
    /// ensuite, une bille <c>0x70</c> — celle d'Enchant — sur un cap tire au hasard.</item>
    /// <item><b>13</b> — un anneau <c>0x6c</c>, celui d'Onde de choc, avec l'ancre a zero.</item>
    /// <item><b>20 et au-dela</b> — elle vole : 20 unites par image, une flammeche <c>0x50</c> une
    /// image sur deux, et le decor l'arrete. Le joueur peut la lacher en relachant le bouton —
    /// <b>mais elle ne coute rien de plus</b> : c'est le seul sort qui lise la manette sans
    /// ponctionner de PM.</item>
    /// <item><b>l'explosion</b> — deux anneaux, un eclat, le fond en rouge, la secousse de camera
    /// a 8, et le compteur remis a zero pour dix images d'eclats disperses.</item>
    /// </list>
    /// <para>
    /// <b>DEFAUT DE 1998 sur les images 10 a 19</b> : le cap de la poussee est lu dans un
    /// emplacement de pile jamais ecrit sur ce chemin. Voir <c>legacy-bug.md</c>, LB-018.
    /// </para>
    /// </summary>
    private static void TickFireOrb_0047F570(byte* record)
    {
        byte* e = record;

        // DEUX sondes, contigues sur la pile : l'etroite pour le vol, la large pour l'explosion.
        short* probes = stackalloc short[12]
        {
            0x0000, -0x0032, 0x0032, 0x0064, 0x0000, 0x0032,   // en vol
            0x0000, -0x0100, 0x0100, 0x0190, 0x0000, 0x0200,   // a l'explosion
        };

        int* burstAt = stackalloc int[3];
        uint state = *(uint*)e;
        int heading;
        int wide;

        if ((state & 0x2000000) != 0)
        {
            // ------------------------------------------------------------ l'explosion
            if (*(short*)(e + 0x10) >= 0x0a || (state & 0x40000000) != 0)
            {
                *(uint*)e = 0;
                *(uint*)(*(uint**)(e + 0x74)) &= 0xffffff7f;
                return;
            }

            *(ushort*)(e + 0x1a) = 7;
            *(int*)(e + 0x70) = 0;
            *(uint*)(e + 0x38) = 0x01606060;

            heading = CrtRand();
            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(heading) << 6;
            *(int*)(e + 0x54) = 0x40000;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(heading) << 6;

            *(ushort*)(e + 0x12) = 3;
            *(uint*)(e + 0x28) = 0x20002000;
            *(ushort*)(e + 0x14) = 0x5000;
            *(ushort*)(e + 0x16) = 2;

            burstAt[0] = *(int*)(e + 0x1c) + (((CrtRand() & 0x1ff) - 0x100) << 0xc);
            burstAt[1] = *(int*)(e + 0x20);
            burstAt[2] = *(int*)(e + 0x24) + (((CrtRand() & 0x1ff) - 0x100) << 0xc);

            SpawnStatusEffectEntity_0047AC40(e, burstAt, Descriptors00551A08(), 0xbe, 0, 2);

            // La secousse s'amortit : trois quarts en moins, et le signe s'inverse.
            int* shake = Mem.Camera(0x005de0ac);
            if (*shake != 0) *shake = (*shake >> 2) - *shake;

            wide = 1;                                  // la sonde large
        }
        else
        {
            wide = 0;
            byte* caster = *(byte**)(e + 0x74);

            if (*(ushort*)(e + 0x10) == 0)
            {
                // -------------------------------------------------------- la formation
                *(uint*)caster |= 0x80;
                *(uint*)e |= 0x80;
                *(byte**)(e + 0x70) = e;
                *(short*)(e + 0x2e) = -0x14;

                int half = StatusBubbleHeight_00538BC0((uint*)caster) >> 1;
                heading = *(short*)(e + 0x32);

                *(int*)(e + 0x20) += half << 0xc;
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * half;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * half;

                *(int*)(e + 0x28) = 0;
                *(uint*)(e + 0x38) = 0x00f0f0f0;
                *(ushort*)(e + 0x1a) = 3;

                SpawnStatusEffectEntity_0047AC40(
                    e, (int*)(e + 0x1c), Descriptors00551B00() + 0x28, 0x51, 0, 2);

                *(int*)(e + 0x28) = 0x7fff;
                *(ushort*)(e + 0x2c) = 0x400;
                *(uint*)(e + 0x38) = 0;
                *(ushort*)(e + 0x12) = 0x1f4;
                *(int*)(e + 0x44) = 0;
                *(int*)(e + 0x48) = 0;
                *(int*)(e + 0x4c) = 0;

                Aud.PlayPositionalSound(0x16, 0x155, 0x1200, (int*)(e + 0x1c));
            }
            else
            {
                // DEFAUT DE 1998 (LB-018) : sur ce chemin, le binaire lit l'emplacement de son
                // propre argument, jamais ecrit ici. Le cap vaut donc le POINTEUR de
                // l'enregistrement, et sert tel quel a la poussee des images 10 a 19.
                heading = (int)(nint)record;
            }

            // Ghidra: LAB_0047f792
            ushort age = *(ushort*)(e + 0x10);

            if ((short)age < 0x14)
            {
                if ((short)age < 0x0a)
                {
                    // Une bille par image, sur un cap quelconque. Le cap est rendu apres.
                    heading = *(short*)(e + 0x32);

                    *(short*)(e + 0x32) = (short)(CrtRand() + 0x400);
                    *(short*)(e + 0x34) = (short)CrtRand();
                    e[0x04] = 0x3f;

                    SpawnCastEntity_0047AAE0(e, 0x70, 0, 2);

                    *(short*)(e + 0x32) = (short)heading;
                }

                // Ghidra: LAB_0047f7d7 — la treizieme image, et elle seule.
                if (*(ushort*)(e + 0x10) == 0x13)
                {
                    short keptHeading = *(short*)(e + 0x32);
                    byte* keptAnchor = *(byte**)(e + 0x70);

                    *(int*)(e + 0x70) = 0;             // l'anneau sera sa propre ancre
                    *(int*)(e + 0x44) = 0;
                    *(int*)(e + 0x48) = 0;
                    *(int*)(e + 0x4c) = 0;
                    *(int*)(e + 0x50) = 0x1000;
                    *(int*)(e + 0x54) = 0x1000;
                    *(int*)(e + 0x58) = 0x1000;
                    *(uint*)(e + 0x38) = 0x00808080;
                    *(int*)(e + 0x28) = 0;
                    *(ushort*)(e + 0x2c) = 0;
                    *(short*)(e + 0x32) = (short)(keptHeading - 0x400);
                    *(ushort*)(e + 0x34) = 0x400;
                    e[0x04] = 7;

                    SpawnCastEntity_0047AAE0(e, 0x6c, 0, 4);

                    *(short*)(e + 0x32) = keptHeading;
                    *(byte**)(e + 0x70) = keptAnchor;
                }
            }
            else
            {
                // -------------------------------------------------------- le vol
                if ((age & 1) != 0)
                {
                    short keptHeading = *(short*)(e + 0x32);

                    *(uint*)(e + 0x28) = 0x20002000;
                    *(ushort*)(e + 0x12) = (ushort)((CrtRand() & 0x7f) + 0xc8);
                    *(uint*)(e + 0x38) = 0;
                    *(ushort*)(e + 0x1a) = 7;
                    *(short*)(e + 0x32) = (short)CrtRand();
                    *(short*)(e + 0x34) = (short)((CrtRand() & 0x1ff) - 0x100);

                    SpawnStatusEffectEntity_0047AC40(
                        e, (int*)(e + 0x1c), Descriptors00551A08(), 0x50, 0, 4);

                    *(short*)(e + 0x32) = keptHeading;
                }

                // Ghidra: LAB_0047f8a1
                heading = *(short*)(e + 0x32);
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 20;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 20;

                int keptY = *(int*)(e + 0x20);
                FinalizeEntityPlacement2((uint*)e);    // Ghidra: FUN_00533940
                int ground = *(int*)(e + 0x60);
                *(int*)(e + 0x20) = keptY;

                if (keptY <= ground)
                {
                    *(int*)(e + 0x20) = ground;
                    *(uint*)(e + 0x90) = 0x800;
                }

                if ((*(uint*)(e + 0x90) & 0x8802) != 0) *(uint*)e |= 0x2000000;
                if (*(short*)(e + 0x10) >= 0x3c) *(uint*)e |= 0x2000000;

                // Ghidra: LAB_0047f913 — la manette, SANS ponction : elle ne coute rien de plus.
                if ((*(uint*)e & 0x800000) != 0)
                {
                    uint index = ((uint*)caster)[2] & 0xff;
                    int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
                    ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);
                    byte* perChar = GetMainMemPtr() + index * 0x2000 + 0xf0000;

                    if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0) *(uint*)e |= 0x2000000;
                }
                else if (*(short*)(e + 0x10) >= 0x28)
                {
                    *(uint*)e |= 0x2000000;
                }

                if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;

                if ((*(uint*)e & 0x2000000) != 0)
                {
                    // ---------------------------------------------------- elle eclate
                    *(short*)(e + 0x2e) = -0x40;
                    *(ushort*)(e + 0x34) = 0;
                    *(int*)(e + 0x44) = 0;
                    *(int*)(e + 0x48) = 0;
                    *(int*)(e + 0x4c) = 0;
                    *(int*)(e + 0x50) = 0x4000;
                    *(int*)(e + 0x54) = 0x2000;
                    *(int*)(e + 0x58) = 0x4000;
                    *(uint*)(e + 0x38) = 0x00808080;
                    *(int*)(e + 0x28) = 0;
                    *(ushort*)(e + 0x2c) = 0;
                    e[0x04] = 7;

                    SpawnCastEntity_0047AAE0(e, 0x6c, 0, 3);

                    *(int*)(e + 0x50) = 0x2000;
                    *(int*)(e + 0x54) = 0x2000;
                    *(int*)(e + 0x58) = 0x2000;
                    e[0x04] = 1;

                    SpawnCastEntity_0047AAE0(e, 0x6c, 1, 0x0e);

                    byte* keptAnchor = *(byte**)(e + 0x70);
                    *(int*)(e + 0x70) = 0;
                    *(int*)(e + 0x50) = 0;
                    *(int*)(e + 0x54) = 0;
                    *(int*)(e + 0x58) = 0;
                    *(ushort*)(e + 0x12) = 0x1f;
                    *(int*)(e + 0x44) = 0;
                    *(int*)(e + 0x48) = 0;
                    *(int*)(e + 0x4c) = 0;
                    *(ushort*)(e + 0x14) = 0x7fff;
                    *(ushort*)(e + 0x16) = 2;
                    *(uint*)(e + 0x38) = 0x00f0f0f0;
                    *(ushort*)(e + 0x1a) = 1;

                    SpawnStatusEffectEntity_0047AC40(
                        e, (int*)(e + 0x1c), Descriptors00551BA0(), 0xbe, 0, 7);

                    *(byte**)(e + 0x70) = keptAnchor;

                    // Le fond de l'ecran, ecrit DIRECTEMENT — sans le mode 2 des autres sorts.
                    Mem.s_BackColourR_005e93a0 = 0x00ff;
                    Mem.s_BackColourG_005e93a2 = 0x0040;
                    Mem.s_BackColourB_005e93a4 = 0x0040;

                    *(ushort*)(e + 0x10) = 0;          // dix images d'eclats commencent
                    *Mem.Camera(0x005de0ac) = 8;       // et l'ecran tremble

                    Aud.PlayPositionalSound(0x0b, 0x329, 0x1200, (int*)(e + 0x1c));
                }
            }

            // Ghidra: LAB_0047fa7d
            if ((*(uint*)e & 0x40000000) != 0)
            {
                *(uint*)e |= 0x2000000;
                *(ushort*)(e + 0x10) = 0;
            }
        }

        // Ghidra: LAB_0047fa93 — `lea edx,[esp+ecx*4+1Ch]` : la sonde est CHOISIE, pas modifiee.
        byte* victim = ProbeMeleeTarget_00474CA0(e, probes + wide * 6);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(heading) << 5;
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(heading) << 5;
            *(ushort*)(victim + 0x96) = 0x10;
            *(uint*)victim |= 0x40;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047fb10 @ 0x0047fb10 — <b>la coque de Boule de feu</b> : le sous-type
    /// <c>0x51</c>, creneau 81 de <see cref="s_StaticEntityHandlers_00552230"/>. 112 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// Elle colle a la boule — l'ancre est <c>+0x70</c>, que le sort a pointe sur lui-meme —,
    /// approche <c>0x1000</c> d'un huitieme et fait tourner son image d'atlas sur <c>+0x1a</c>.
    /// <b>Elle herite du bit de fin</b> de son ancre et s'eteint avec elle, sans transition.
    /// </para>
    /// </summary>
    private static void TickFireOrbShell_0047FB10(byte* record)
    {
        byte* e = record;
        byte* anchor = *(byte**)(e + 0x70);

        uint state = *(uint*)e | (*(uint*)anchor & 0x2000000);
        *(uint*)e = state;

        if ((state & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_0047fb3a
        short scale = *(short*)(e + 0x28);
        scale += (short)((0x1000 - scale) >> 3);
        *(short*)(e + 0x28) = scale;
        *(short*)(e + 0x2a) = scale;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);

        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;

        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (frame >= *(ushort*)(e + 0x1a)) e[0x40] = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00482ef0 @ 0x00482ef0 — <b>la trainee de Boule de feu</b> : le sous-type
    /// <c>0x50</c>, creneau 80 de <see cref="s_StaticEntityHandlers_00552230"/>. 167 octets,
    /// <b>absente de <c>Blaze.c</c></b>. C'est le seul corps de ce sort qui vive loin des autres
    /// dans le binaire.
    ///
    /// <para>
    /// Elle tourne autour de sa mere en coordonnees <b>spheriques</b> : son rayon <c>+0x12</c> perd
    /// un huitieme par image et son cap <c>+0x32</c> recule de <c>0x80</c>, le tangage <c>+0x34</c>
    /// restant celui que la boule lui a tire. <b>Sa teinte MONTE</b> de <c>0x202020</c> jusqu'a
    /// <c>0xe0e0e0</c>, a l'envers de toutes les autres trainees du depot.
    /// </para>
    /// <para>
    /// <b>Deux fins</b> : son rayon sous <c>0x30</c>, ou sa mere marquee <c>0x40000000</c>.
    /// </para>
    /// </summary>
    private static void TickFireOrbTrail_00482EF0(byte* record)
    {
        byte* e = record;
        byte* mother = *(byte**)(e + 0x6c);

        int* offset = stackalloc int[3];

        unchecked { *(ushort*)(e + 0x32) += 0xff80; }  // -0x80

        short radius = *(short*)(e + 0x12);
        radius -= (short)(radius >> 3);
        *(short*)(e + 0x12) = radius;

        SetVectorFromAngles_00532AD0(
            offset, *(short*)(e + 0x32), *(short*)(e + 0x34), radius);

        *(int*)(e + 0x1c) = *(int*)(mother + 0x1c) + offset[0];
        *(int*)(e + 0x20) = *(int*)(mother + 0x20) + offset[1];
        *(int*)(e + 0x24) = *(int*)(mother + 0x24) + offset[2];

        uint tint = *(uint*)(e + 0x38);
        if (tint < 0x00e0e0e0) *(uint*)(e + 0x38) = tint + 0x00202020;

        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if (frame >= *(ushort*)(e + 0x1a)) e[0x40] = 0;

        if (*(short*)(e + 0x12) < 0x30 || (*(uint*)mother & 0x40000000) != 0) *(uint*)e = 0;
    }

    // =====================================================================
    // VENTILO — le vingt-deuxieme sort, ecole 0 indice 21, sous-type 0x15
    //
    // Deux corps neufs seulement : c'est COUP DE JUS TROIS FOIS. Il reemploie tel quel le segment
    // 0x75 de TickLightningBolt_0047D470, et l'eclat 0xbe deja ecrit.
    //
    //   0x0047FB80   LE SORT. Sous-type 0x15 de 0x00551c30, 696 octets.
    //   0x0047FE40   LA BOURRASQUE, sous-type 0x83 de 0x00551f30, 29 octets.
    //
    // TROIS POLYLIGNES AU LIEU D'UNE. La boucle est double : trois eclairs, chacun de trois
    // segments poses bout a bout comme le fait Coup de jus, et les trois partent du MEME point —
    // la position est mise de cote et rendue entre chaque. Les caps de depart sont espaces de
    // 0x200, ce qui ouvre l'eventail.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047fb80 @ 0x0047fb80 — <b>Ventilo</b> : le sous-type <c>0x15</c>, creneau 21 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 696 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>A la naissance</b> il se place a mi-hauteur de tete du lanceur et a la meme distance
    /// devant lui — le meme geste que Boule de feu et Durdur —, lache la bourrasque <c>0x83</c>
    /// avec un modele tire parmi quatre, un eclat <c>0xbe</c>, et met le souffle en file.
    /// </para>
    /// <para>
    /// <b>A la quatorzieme image il frappe</b> : un second eclat, le tonnerre, puis <b>trois
    /// eclairs de trois segments</b>. Chacun est exactement la polyligne de
    /// <see cref="TickLightningArc_0047D330"/> — deviation relative sur <c>0x1c0</c>, longueur de
    /// <c>0x100</c> a <c>0x1ff</c>, echelle <c>longueur &lt;&lt; 12 / 250</c>, modele parmi trois —
    /// et les trois repartent du <b>meme point</b>, la position etant mise de cote et rendue entre
    /// chaque. Les caps de depart sont espaces de <c>0x200</c>.
    /// </para>
    /// <para>
    /// Le sort s'eteint dans la foulee : les segments vivent leur vie sans lui.
    /// </para>
    /// </summary>
    private static void TickGale_0047FB80(byte* record)
    {
        byte* e = record;
        byte* caster = *(byte**)(e + 0x74);

        if (*(ushort*)(e + 0x10) == 0)
        {
            // ------------------------------------------------------------ la naissance
            *(uint*)caster |= 0x80;

            int heading = *(short*)(e + 0x32);
            int half = StatusBubbleHeight_00538BC0((uint*)caster) >> 1;

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * half;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * half;
            *(int*)(e + 0x20) += half << 0xc;

            *(ushort*)(e + 0x32) += 0x400;
            *(ushort*)(e + 0x34) += 0x400;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(uint*)(e + 0x28) = 0x20004000;
            *(ushort*)(e + 0x2c) = 0x2000;

            e[0x04] = (byte)((CrtRand() & 3) + 8);     // un modele parmi quatre

            SpawnCastEntity_0047AAE0(e, 0x83, e[0x0b], -1);

            unchecked { *(ushort*)(e + 0x34) += 0xfc00; }   // -0x400 : le tangage est rendu

            *(int*)(e + 0x70) = 0;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = 0;
            *(ushort*)(e + 0x12) = 0x1f;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;
            *(int*)(e + 0x28) = 0;
            *(ushort*)(e + 0x14) = 0x2000;
            *(ushort*)(e + 0x16) = 2;
            *(uint*)(e + 0x38) = 0x01f0f0f0;
            *(ushort*)(e + 0x1a) = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0() + 0x10, 0xbe, e[0x0b], -1);

            Aud.QueueDelayedSoundScript(DelayedSoundScript00552568(), (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047fc97 — treize images d'attente, puis tout d'un coup.
        if (*(short*)(e + 0x10) < 0x0e)
        {
            *(ushort*)(e + 0x10) += 1;
            return;
        }

        *(int*)(e + 0x28) = 0x1ffc;
        *(ushort*)(e + 0x14) = 0;
        *(ushort*)(e + 0x16) = 3;
        *(uint*)(e + 0x38) = 0x00f0f0f0;

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551BA0() + 0x10, 0xbe, e[0x0b], -1);

        Aud.PlayPositionalSound(0x26, 0x653, 0x1200, (int*)(e + 0x1c));

        int baseHeading = *(short*)(e + 0x32) - 0x200;

        // Trois eclairs, tous partis du meme point : la position est rendue entre chaque.
        for (int arc = 0; arc < 3; arc++)
        {
            int drift = 0;
            int keptX = *(int*)(e + 0x1c);
            int keptY = *(int*)(e + 0x20);
            int keptZ = *(int*)(e + 0x24);

            for (int segment = 0; segment < 3; segment++)
            {
                drift = (CrtRand() & 0x1c0) - drift - 0x100;
                *(short*)(e + 0x32) = (short)(baseHeading + drift);

                int length = (CrtRand() & 0xff) + 0x100;

                *(short*)(e + 0x28) = (short)((length << 0xc) / 250);
                *(short*)(e + 0x2a) = (short)((CrtRand() & 0xfff) + 0x2000);
                *(short*)(e + 0x2c) = (short)((CrtRand() & 0xfff) + 0x2800);
                e[0x04] = (byte)(CrtRand() % 3 + 8);

                SpawnCastEntity_0047AAE0(e, 0x75, e[0x0b], -1);

                int heading = *(short*)(e + 0x32) - 0x400;
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * length;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * length;
            }

            *(int*)(e + 0x20) = keptY;
            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x24) = keptZ;

            baseHeading += 0x200;                      // l'eventail s'ouvre
        }

        *(uint*)e = 0;
        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0047fe40 @ 0x0047fe40 — <b>la bourrasque de Ventilo</b> : le sous-type
    /// <c>0x83</c>, creneau 131 de <see cref="s_StaticEntityHandlers_00551f30"/>. 29 octets,
    /// <b>absente de <c>Blaze.c</c></b> — le deuxieme plus court corps de sort du depot.
    ///
    /// <para>
    /// Elle pose le bit <c>4</c> de <c>+0x36</c> a chaque image — le seul corps de sort a le faire
    /// dans son corps plutot qu'a la naissance — et perd <c>0x101010</c> de teinte. Seize images.
    /// </para>
    /// </summary>
    private static void TickGaleGust_0047FE40(byte* record)
    {
        byte* e = record;

        e[0x36] |= 4;

        uint tint;
        unchecked { tint = *(uint*)(e + 0x38) + 0xffefeff0; }   // -0x101010
        *(uint*)(e + 0x38) = tint;

        if (tint == 0) *(uint*)e = 0;
    }

    // =====================================================================
    // DEMON POLAIRE — le vingt-troisieme sort, ecole 0 indice 22, sous-type 0x16
    //
    // Trois corps neufs, et le premier sort qui VERIFIE SES RESSOURCES avant de partir.
    //
    //   0x0047FE60   LE SORT. Sous-type 0x16 de 0x00551c30, 532 octets. Septieme sort entretenu,
    //                a 5 PM par vingt images.
    //   0x00480080   LA TETE, sous-type 0x3c de 0x00551f30, 1026 octets. Trois exemplaires.
    //   0x00480490   LA QUEUE, sous-type 0x3d de 0x00551f30, 362 octets. Trois aussi.
    //
    // IL REFUSE DE PARTIR SI LA TABLE EST PLEINE. La premiere instruction utile compare le curseur
    // du pool 0x006007a0 a 0x76 : au-dela, le sort s'eteint sans rien faire. Aucun autre corps du
    // depot ne fait ce controle — celui-ci en a besoin, il lui faut six emplacements d'un coup.
    //
    // ET SES DEUX MOITIES SE RETROUVENT PAR LEUR NUMERO D'EMPLACEMENT. Pour chacun des trois
    // demons, le sort RESERVE deux emplacements par ReserveEntitySlots_00475090 — qui trie les deux
    // plus petits indices libres en tete SANS avancer le curseur —, les lit dans +0x16 et +0x17,
    // puis engendre la tete et la queue. La queue recalcule ensuite l'adresse de sa tete par
    // `0x005edee0 + (+0x16) * 0x9c`, sans qu'aucun pointeur ne circule. C'est la seule
    // communication par indice du depot.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047fe60 @ 0x0047fe60 — <b>Demon polaire</b> : le sous-type <c>0x16</c>, creneau
    /// 22 de <see cref="s_StaticEntityHandlers_00551c30"/>. 532 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il commence par verifier qu'il a la place.</b> Si le curseur du pool des entites
    /// d'incantation depasse <c>0x76</c> — moins de dix emplacements libres sur <c>0x80</c> — il
    /// s'eteint immediatement, sans immobiliser le lanceur ni jouer de son.
    /// </para>
    /// <para>
    /// <b>Trois demons, chacun en deux moities.</b> Pour chacun il reserve deux emplacements par
    /// <see cref="ReserveEntitySlots_00475090"/>, recopie les deux indices dans <c>+0x16</c> et
    /// <c>+0x17</c>, avance son cap d'un quart de tour et sa phase <c>+0x12</c> d'un tiers
    /// (<c>0x555</c>), puis engendre la tete <c>0x3c</c> et la queue <c>0x3d</c>.
    /// </para>
    /// <para>
    /// <b>Il attend ensuite le bit <c>2</c></b> que ses tetes lui posent quand elles ont fini de
    /// grandir — comme Trouille — avant de relacher le lanceur et de lire la manette. Septieme sort
    /// entretenu, a 5 PM toutes les vingt images ; le decor vire au bleu de Congelo, reecrit a
    /// chaque image.
    /// </para>
    /// </summary>
    private static void TickPolarDemon_0047FE60(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(ushort*)Mem.Arena(0x005e9166) = 0;       // le decor reprend sa couleur
            *(uint*)e = 0;
            return;
        }

        byte* caster = *(byte**)(e + 0x74);

        if (*(ushort*)(e + 0x10) == 0)
        {
            // ------------------------------------------------------------ le controle de place
            if (*Mem.Entity(0x006007a0) > 0x76)
            {
                *(uint*)e = 0;                         // pas assez d'emplacements : il renonce
                return;
            }

            *(uint*)caster |= 0x80;
            *(int*)(e + 0x78) = 0;

            int heading = *(short*)(e + 0x32);
            int half = StatusBubbleHeight_00538BC0((uint*)caster) >> 1;

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * half;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * half;
            *(ushort*)(e + 0x12) = 0;
            *(int*)(e + 0x20) += half << 0xc;

            for (int demon = 0; demon < 3; demon++)
            {
                // Les deux emplacements sont RESERVES, pas pris : les deux naissances qui suivent
                // les prendront dans cet ordre.
                byte* pool = Mem.Entity(0x006007a0);
                ReserveEntitySlots_00475090(pool, 2);

                e[0x16] = pool[*pool + 2];
                e[0x17] = pool[*pool + 3];

                int spin = *(short*)(e + 0x32);
                *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(spin) << 6;
                *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(spin) << 6;
                *(ushort*)(e + 0x32) += 0x400;

                e[0x04] = 0x24;
                SpawnCastEntity_0047AAE0(e, 0x3c, e[0x0b], -1);

                e[0x04] = 0x25;
                SpawnCastEntity_0047AAE0(e, 0x3d, e[0x0b], -1);

                *(ushort*)(e + 0x12) += 0x555;         // un tiers de tour de phase
            }

            Aud.QueueDelayedSoundScript(DelayedSoundScript00552578(), (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047ffa1 — rien ne bouge tant que les tetes n'ont pas pose le bit 2.
        if ((*(uint*)e & 2) != 0)
        {
            *(uint*)caster &= 0xffffff7f;              // le lanceur est rendu a lui-meme

            if ((*(uint*)e & 0x800000) != 0)
            {
                uint index = ((uint*)caster)[2] & 0xff;
                byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

                int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
                ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

                if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0) *(uint*)e |= 0x2000000;

                if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
                {
                    short mana = *(short*)(perChar + 0x14e);

                    if (mana >= 5)
                    {
                        *(short*)(perChar + 0x14e) = (short)(mana - 5);
                    }
                    else
                    {
                        *(ushort*)(perChar + 0x14e) = 0;
                        *(uint*)e |= 0x2000000;
                    }
                }
            }
            else if (*(short*)(e + 0x10) >= 0x78)
            {
                *(uint*)e |= 0x2000000;
            }

            if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_00480057 — le bleu de Congelo, reecrit a chaque image.
        *(uint*)Mem.Arena(0x005e9160) = 0x00400040;
        *(uint*)Mem.Arena(0x005e9164) = 0x00020080;

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00480080 @ 0x00480080 — <b>la tete du Demon polaire</b> : le sous-type
    /// <c>0x3c</c>, creneau 60 de <see cref="s_StaticEntityHandlers_00551f30"/>. 1026 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle grandit, puis elle chasse.</b> Son echelle approche <c>0x1800</c> d'un quart ; des
    /// qu'elle depasse <c>0x17e0</c> elle pose le bit <c>2</c> sur son <b>pere</b> — c'est ce signal
    /// qui debloque le sort. Elle prend ensuite l'adversaire le plus proche et le garde quarante
    /// images, avec le meme lien reciproque que la fleche magique ; faute de proie elle tourne
    /// autour du lanceur, a hauteur de sa tete et sur un cap qui avance de <c>0x80</c> par image.
    /// </para>
    /// <para>
    /// <b>Ses deux angles tournent par le plus court chemin</b> — comme le fantome de Trouille,
    /// mais tous les deux — et le tangage vise est decale de <c>(age &amp; 7) &lt;&lt; 7</c>. La
    /// meme quantite, decalee de cinq bits, donne sa vitesse : elle <b>bat des ailes sur huit
    /// images</b>, l'ondulation et la poussee venant du meme compteur.
    /// </para>
    /// <para>
    /// Une image sur quatre elle laisse une trainee <c>0xbf</c> derriere elle, six champs pretes
    /// puis rendus.
    /// </para>
    /// </summary>
    private static void TickPolarDemonHead_00480080(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6] { -0x0400, -0x0050, 0x003c, 0x0064, 0x0000, 0x00a0 };
        int* step = stackalloc int[3];
        int* trailAt = stackalloc int[3];

        uint state = *(uint*)e;

        if ((state & 0x2000000) != 0)
        {
            short shrinking = *(short*)(e + 0x28);
            shrinking -= (short)(shrinking >> 2);
            *(short*)(e + 0x28) = shrinking;
            *(short*)(e + 0x2a) = shrinking;
            *(short*)(e + 0x2c) = shrinking;

            if (shrinking < 0x100) { *(uint*)e = 0; return; }

            *(ushort*)(e + 0x10) += 1;
            return;
        }

        // Ghidra: LAB_004800eb
        byte* father = *(byte**)(e + 0x6c);
        *(uint*)e = (*(uint*)father & 0x2000000) | state;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(int*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = 0;
            *(ushort*)(e + 0x9a) = 0x1000;
        }

        // Ghidra: LAB_00480120 — elle grandit, et previent son pere quand elle y est.
        short grown = *(short*)(e + 0x28);
        grown += (short)((0x1800 - grown) >> 2);
        *(short*)(e + 0x28) = grown;
        *(short*)(e + 0x2a) = grown;
        *(short*)(e + 0x2c) = grown;

        if (grown > 0x17e0) *(uint*)father |= 2;

        // Ghidra: LAB_0048014b — la proie, gardee quarante images au plus.
        byte* prey = *(byte**)(e + 0x78);

        if (prey != null)
        {
            if ((*(uint*)prey & 0x50000000) != 0 || *(short*)(e + 0x10) >= 0x28)
            {
                *(uint*)(prey + 0x74) = 0;
                *(int*)(e + 0x78) = 0;
                *(ushort*)(e + 0x10) = 0;
                prey = null;
            }
        }
        else
        {
            prey = FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 1);
        }

        // Ghidra: LAB_00480181
        *(ushort*)(e + 0x12) += 0x80;
        int orbit = *(short*)(e + 0x12);

        int dx, dy, dz;

        if (prey != null)
        {
            *(byte**)(e + 0x78) = prey;
            *(byte**)(prey + 0x74) = e;

            dx = (*(int*)(prey + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            int head = StatusBubbleHeight_00538BC0((uint*)prey);
            dy = (((head & ~1) << 0xb) + *(int*)(prey + 0x20) - *(int*)(e + 0x20)) >> 0xc;
            dz = (*(int*)(prey + 0x24) - *(int*)(e + 0x24)) >> 0xc;
        }
        else
        {
            // Ghidra: LAB_004801c3 — elle tourne autour du lanceur, a hauteur de sa tete.
            byte* caster = *(byte**)(e + 0x74);
            int radius = StatusBubbleHeight_00538BC0((uint*)caster);

            dx = ((int)Winsys3d.GetSinTableEntry(orbit) * radius
                  + *(int*)(caster + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            dy = (*(int*)(caster + 0x20) + (radius << 0xc) - *(int*)(e + 0x20)) >> 0xc;
            dz = (Winsys3d.GetCosTableEntry(orbit) * radius
                  + *(int*)(caster + 0x24) - *(int*)(e + 0x24)) >> 0xc;
        }

        // Ghidra: LAB_0048021a — la poussee s'ajoute, puis tout perd un quart.
        int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int wantYaw = Winsys3d.Ordinal_457(dx, dz);
        int wantPitch = Winsys3d.Ordinal_457(dy, flat);

        SetVectorFromAngles_00532AD0(step, wantYaw, wantPitch, *(int*)(e + 0x5c));

        int vx = *(int*)(e + 0x50) + step[0];
        int vy = *(int*)(e + 0x54) + step[1];
        int vz = *(int*)(e + 0x58) + step[2];

        // L'ondulation : la phase sur huit images decale le tangage vise ET donne la vitesse.
        int flap = (e[0x10] & 7) << 7;

        short pitch = *(short*)(e + 0x34);
        short yaw = *(short*)(e + 0x32);

        int gapYaw = (wantYaw - yaw + 0x400) & 0xfff;
        int gapPitch = (wantPitch - flap - pitch + 0x200) & 0xfff;

        *(short*)(e + 0x32) =
            (short)(yaw + (gapYaw < 0x800 ? gapYaw >> 2 : -((0x1000 - gapYaw) >> 2)));
        *(short*)(e + 0x34) =
            (short)(pitch + (gapPitch < 0x800 ? gapPitch >> 2 : -((0x1000 - gapPitch) >> 2)));

        *(int*)(e + 0x5c) = flap >> 5;                 // soit (age & 7) * 4

        vx -= vx >> 2;
        vy -= vy >> 2;
        vz -= vz >> 2;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;

            Aud.PlayPositionalSound(0x22, 0xf4e, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0048039c — une image sur quatre, la trainee.
        if ((e[0x10] & 3) == 0)
        {
            uint keptScale = *(uint*)(e + 0x28);
            uint keptDepth = *(uint*)(e + 0x2c);
            uint keptTint = *(uint*)(e + 0x38);

            int behind = *(short*)(e + 0x32) + 0x400;

            trailAt[0] = *(int*)(e + 0x1c) + (int)Winsys3d.GetSinTableEntry(behind) * 60
                         + (((CrtRand() & 0x3f) - 0x20) << 0xc);
            trailAt[1] = *(int*)(e + 0x20) + (((CrtRand() & 0x3f) - 0x20) << 0xc);
            trailAt[2] = *(int*)(e + 0x24) + Winsys3d.GetCosTableEntry(behind) * 60
                         + (((CrtRand() & 0x3f) - 0x20) << 0xc);

            *(uint*)(e + 0x28) = 0x18001800;
            *(ushort*)(e + 0x2c) = 0x1f;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(ushort*)(e + 0x1a) = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, trailAt, Descriptors00551BB8(), 0xbf, e[0x0b], -1);

            *(uint*)(e + 0x28) = keptScale;
            *(uint*)(e + 0x2c) = keptDepth;
            *(uint*)(e + 0x38) = keptTint;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00480490 @ 0x00480490 — <b>la queue du Demon polaire</b> : le sous-type
    /// <c>0x3d</c>, creneau 61 de <see cref="s_StaticEntityHandlers_00551f30"/>. 362 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle retrouve sa tete par son NUMERO D'EMPLACEMENT</b>, pas par un pointeur : a sa
    /// premiere image elle calcule <c>0x005edee0 + (+0x16) * 0x9c</c> — le binaire l'ecrit
    /// <c>indice * 39 * 4</c> — et range le resultat dans <c>+0x70</c>. L'indice vient de la
    /// reservation faite avant les deux naissances, voir
    /// <see cref="ReserveEntitySlots_00475090"/>.
    /// </para>
    /// <para>
    /// Ensuite elle suit : ses deux angles rattrapent ceux de la tete <b>par le plus court
    /// chemin</b>, et sa position vaut celle de la tete plus un vecteur de longueur
    /// <c>echelle * 60 &gt;&gt; 12</c> dans le sens <b>oppose</b> a son regard — cap plus un quart
    /// de tour, tangage nie. Elle traine donc exactement derriere.
    /// </para>
    /// </summary>
    private static void TickPolarDemonTail_00480490(byte* record)
    {
        byte* e = record;
        uint state = *(uint*)e;

        int* offset = stackalloc int[3];

        if ((state & 0x2000000) != 0)
        {
            short shrinking = *(short*)(e + 0x28);
            shrinking -= (short)(shrinking >> 2);
            *(short*)(e + 0x28) = shrinking;
            *(short*)(e + 0x2a) = shrinking;
            *(short*)(e + 0x2c) = shrinking;

            if (shrinking < 0x100) *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_004804d5
        byte* father = *(byte**)(e + 0x6c);
        *(uint*)e = (*(uint*)father & 0x2000000) | state;

        byte* head;

        if (*(ushort*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x10) = 1;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(int*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;

            // `lea edx,[eax+eax*4] ; shl edx,3 ; sub edx,eax ; lea edi,[edx*4+5EDEE0h]` :
            // l'indice fois 39, fois 4 — soit 0x9c, le pas de la table des incantations.
            head = Mem.Entity(0x005edee0) + e[0x16] * 0x9c;
            *(byte**)(e + 0x70) = head;
        }
        else
        {
            head = *(byte**)(e + 0x70);
        }

        // Ghidra: LAB_0048051c
        short scaleNow = *(short*)(e + 0x28);
        short grown = (short)(((0x1800 - scaleNow) >> 2) + scaleNow);
        *(short*)(e + 0x28) = grown;
        *(short*)(e + 0x2a) = grown;
        *(short*)(e + 0x2c) = grown;

        // Les deux angles rattrapent ceux de la tete, chacun par le plus court chemin.
        short yaw = *(short*)(e + 0x32);
        short pitch = *(short*)(e + 0x34);

        int gapYaw = (*(ushort*)(head + 0x32) - yaw) & 0xfff;
        int gapPitch = (*(ushort*)(head + 0x34) - pitch) & 0xfff;

        *(short*)(e + 0x32) =
            (short)(yaw + (gapYaw < 0x800 ? gapYaw >> 2 : -((0x1000 - gapYaw) >> 2)));
        *(short*)(e + 0x34) =
            (short)(pitch + (gapPitch < 0x800 ? gapPitch >> 2 : -((0x1000 - gapPitch) >> 2)));

        // Et elle se pose derriere : le cap de la TETE plus un quart de tour, son tangage nie.
        int span = (grown * 15 * 4) >> 0xc;

        SetVectorFromAngles_00532AD0(
            offset,
            *(short*)(head + 0x32) + 0x400,
            -*(short*)(head + 0x34),
            span);

        *(int*)(e + 0x1c) = *(int*)(head + 0x1c) + offset[0];
        *(int*)(e + 0x20) = *(int*)(head + 0x20) + offset[1];
        *(int*)(e + 0x24) = *(int*)(head + 0x24) + offset[2];
    }


    // =====================================================================
    // STALAGTITE — le vingt-quatrieme sort, ecole 0 indice 23, sous-type 0x17
    //
    // Trois corps neufs, absents de Blaze.c.
    //
    //   0x00480600   LE SORT. Sous-type 0x17 de 0x00551c30, 1048 octets.
    //   0x00480A20   L'AIGUILLE, sous-type 0x73 de 0x00551f30, 79 octets. Six exemplaires.
    //   0x00480A70   LA PIERRE, sous-type 0x74 de 0x00551f30, 283 octets. Une image sur deux.
    //
    // LE PREMIER SORT QUI VIT DEUX VIES DANS UN SEUL CORPS. Son mot d'etat commence par tester
    // 0x2000000 et se branche sur DEUX corps complets, pas sur une fin de vie : tant que le bit est
    // absent la pointe RAMPE (vingt unites par image sur son cap, la hauteur rendue apres le
    // placement au sol) ; des qu'il est pose, LAB_004809d3 range la position dans l'ancre
    // +0x44/+0x48/+0x4c, remet l'age a zero, et le meme corps devient un eboulement de 0x50 images.
    // Tous les autres sorts du depot eteignent leur entite sur ce bit ; celui-ci s'en sert de
    // sablier. C'est ce qui explique le sens du test a l'entree, qui parait inverse.
    //
    // ET SES SIX AIGUILLES SONT UN CHAPELET, PAS UNE GERBE. A la premiere image il en engendre une
    // sans ecart (modele 0x15), puis cinq (modele 0x16) a 30, 60, 90, 120 et 150 unites sur le cap
    // DU LANCEUR, chacune 0x200 plus petite que la precedente sur les trois axes. Chaque aiguille
    // relit l'ancre de son pere a chaque image (0x00480A20) : la file entiere suit la pointe qui
    // rampe, sans qu'aucune ne calcule son propre deplacement.
    //
    // SON ATLAS DE POUSSIERE FIXE LE DEBUT D'UNE TABLE DEJA RELEVEE. Il tire rand % 3 dans
    // 0x00551AF8, dont les deux derniers enregistrements sont les DEUX PREMIERS de
    // s_Descriptors_00551B00, borne jusqu'ici par le seul changement de planche. Les deux
    // consommateurs lisent le meme bloc de .data a deux bases distantes de huit octets.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551af8</c> — l'atlas de la poussiere de <b>Stalagtite</b>.
    /// <b>Trois enregistrements</b>, et c'est le sort qui le dit : il pose <c>rand % 3</c> comme
    /// indice, exactement comme <see cref="s_Descriptors_00551A60"/>.
    ///
    /// <para>
    /// <b>Il chevauche <see cref="s_Descriptors_00551B00"/>.</b> Ses deuxieme et troisieme lignes
    /// sont les premiere et deuxieme de celui-la : le bloc de <c>.data</c> est unique, et les deux
    /// gestionnaires y entrent a deux bases distantes de huit octets. Cette table-ci donne donc la
    /// <b>vraie borne gauche</b> du groupe <c>0068 1010</c>, que l'autre releve n'avait pas.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551AF8 =
    [
        0x00, 0x68, 0x10, 0x10, 0x00, 0x00, 0x10, 0x00,   // 0x00551af8
        0x10, 0x68, 0x10, 0x10, 0x00, 0x00, 0x10, 0x00,   // 0x00551b00
        0x20, 0x68, 0x10, 0x10, 0x00, 0x00, 0x10, 0x00,   // 0x00551b08
    ];

    private static byte* s_Descriptors00551AF8Storage;

    /// <summary>Les trois descripteurs en memoire native.</summary>
    private static byte* Descriptors00551AF8()
        => NativeCopy(s_Descriptors_00551AF8, ref s_Descriptors00551AF8Storage);

    /// <summary>
    /// Ghidra: FUN_00480600 @ 0x00480600 — <b>Stalagtite</b> : le sous-type <c>0x17</c>, creneau 23
    /// de <see cref="s_StaticEntityHandlers_00551c30"/>. 1048 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Un corps, deux vies.</b> Le bit <c>0x2000000</c> ne l'eteint pas : il la fait passer de la
    /// <i>pointe qui rampe</i> a l'<i>eboulement</i>. La pointe avance de vingt unites par image sur
    /// son propre cap, se laisse poser au sol par <see cref="FinalizeEntityPlacement2"/> puis
    /// <b>reprend sa hauteur</b> — elle glisse a l'horizontale au lieu d'epouser le relief.
    /// </para>
    /// <para>
    /// <b>Trois choses la font basculer</b> : le decor (<c>0x40000000</c> ou <c>+0x90</c> sur le
    /// masque <c>0x8802</c>), quarante images, et le bouton relache — ou, pour un lanceur qui n'est
    /// pas un joueur (bit <c>0x800000</c> absent), vingt images tout court. <b>Aucune ponction de
    /// PM</b> : elle dure tant que le bouton tient, sans rien couter de plus.
    /// </para>
    /// <para>
    /// <b>L'eboulement dure 0x50 images.</b> A chacune il seme un eclat <c>0xbf</c> — celui de
    /// Flêches magiques, <see cref="TickTrailSpark_00475F60"/> — dans un rayon de 255 unites autour
    /// de l'ancre, avec une vitesse verticale de 40 a 71 ; une image sur deux il lache en plus une
    /// pierre <c>0x74</c> au cap tire au hasard, et son bruit.
    /// </para>
    /// </summary>
    private static void TickStalactite_00480600(byte* record)
    {
        byte* e = record;

        // Le binaire remplit ses six mots de sonde en tete de fonction, avant meme le branchement ;
        // seule la vie « pointe » s'en sert.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0x3c;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x12c;

        uint state = *(uint*)e;

        if ((state & 0x2000000) != 0)
        {
            // =================================================== la seconde vie : l'eboulement
            if (*(short*)(e + 0x10) >= 0x50) { *(uint*)e = 0; return; }
            if ((state & 0x40000000) != 0) { *(uint*)e = 0; return; }

            byte* fallen = *(byte**)(e + 0x74);
            if ((*(uint*)fallen & 0x40000000) != 0) { *(uint*)e = 0; return; }

            // Les deux mots d'echelle sont mis de cote : la poussiere naît a 0x2000 par recopie du
            // pere, et le sort reprend sa taille des la naissance faite.
            uint savedScaleXy = *(uint*)(e + 0x28);
            uint savedScaleZ = *(uint*)(e + 0x2c);

            int scatter = CrtRand();
            int reach = CrtRand() & 0xff;

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(scatter) * reach + *(int*)(e + 0x44);
            *(int*)(e + 0x20) = *(int*)(e + 0x48);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(scatter) * reach + *(int*)(e + 0x4c);

            int drift = CrtRand();
            int speed = (CrtRand() & 0x1f) + 0x0a;

            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(drift) * speed;
            *(int*)(e + 0x54) = ((CrtRand() & 0x1f) + 0x28) << 0xc;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(drift) * speed;

            *(uint*)(e + 0x38) = 0x01f0f0f0;
            *(uint*)(e + 0x28) = 0x20002000;
            *(ushort*)(e + 0x2c) = 0x1f;

            byte* atlas = Descriptors00551AF8() + CrtRand() % 3 * 8;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), atlas, 0xbf, e[0x0b], -1);

            *(uint*)(e + 0x28) = savedScaleXy;
            *(uint*)(e + 0x2c) = savedScaleZ;

            // Ghidra: `test al,1` — une image sur deux, et une seule pierre a la fois.
            if ((e[0x10] & 1) != 0)
            {
                *(uint*)(e + 0x38) = 0x00808080;
                *(ushort*)(e + 0x32) = (ushort)CrtRand();

                e[0x04] = 0x17;
                SpawnCastEntity_0047AAE0(e, 0x74, e[0x0b], -1);

                Aud.PlayPositionalSound(4, 0x400, 0x1200, (int*)(e + 0x1c));
            }

            *(ushort*)(e + 0x10) += 1;
            return;
        }

        // ======================================================= la premiere vie : la pointe
        if (*(short*)(e + 0x10) == 0)
        {
            byte* caster = *(byte**)(e + 0x74);

            e[0x36] |= 6;
            *(uint*)e = state | 0x80;                  // le lanceur est immobilise

            // Ghidra: `and al,0FEh` puis `shl eax,0Bh`. Seul le bit 0 est efface, donc c'est
            // (hauteur >> 1) << 12 — la demi-hauteur du Demon polaire, ecrite autrement.
            int height = StatusBubbleHeight_00538BC0((uint*)caster);
            *(int*)(e + 0x20) += (height & ~1) << 0xb;

            int casterHeading = *(short*)(caster + 0x32);

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(casterHeading) * 20;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(casterHeading) * 20;

            // La premiere aiguille n'a pas d'ecart : elle est la pointe elle-meme.
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;

            *(uint*)(e + 0x38) = 0x00808080;
            *(uint*)(e + 0x28) = 0x10001000;
            *(ushort*)(e + 0x2c) = 0x1000;

            e[0x04] = 0x15;
            SpawnCastEntity_0047AAE0(e, 0x73, e[0x0b], -1);

            // Les cinq suivantes : autre modele, de trente en trente unites derriere la premiere,
            // 0x200 plus petites a chaque pas et tournees d'un huitieme de tour tire au hasard.
            e[0x04] = 0x16;

            for (int span = 0x1e; span <= 0x96; span += 0x1e)
            {
                *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(casterHeading) * span;
                *(int*)(e + 0x48) = 0;
                *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(casterHeading) * span;

                *(ushort*)(e + 0x2c) += 0xfe00;         // -0x200 sur Z
                *(uint*)(e + 0x28) += 0xfdfffe00;       // -0x200 sur X et sur Y, en un seul dword

                *(ushort*)(e + 0x34) += (ushort)((CrtRand() & 0x1ff) + 0x300);

                SpawnCastEntity_0047AAE0(e, 0x73, e[0x0b], -1);
            }

            Aud.PlayPositionalSound(4, 0x400, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0048089c — vingt unites par image sur son propre cap.
        int heading = *(short*)(e + 0x32);

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 20;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 20;

        // La hauteur est relevee AVANT le placement et rendue apres : la pointe glisse a plat, le
        // sol ne la souleve pas.
        int keptHeight = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);
        *(int*)(e + 0x20) = keptHeight;

        state = *(uint*)e;

        if ((state & 0x40000000) != 0 || (*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            state |= 0x2000000;
            *(uint*)e = state;
        }

        *(ushort*)(e + 0x10) += 1;
        short age = *(short*)(e + 0x10);

        if (age >= 0x28) *(uint*)e |= 0x2000000;

        state = *(uint*)e;

        if ((state & 0x800000) != 0)
        {
            // Le meme code de manette que les sorts entretenus, mais sans la ponction de PM.
            byte* caster = *(byte**)(e + 0x74);
            uint index = caster[8];
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0)
            {
                state |= 0x2000000;
                *(uint*)e = state;
            }
        }
        else if (age >= 0x14)
        {
            state |= 0x2000000;
            *(uint*)e = state;
        }

        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

            // La poussee est prise un QUART DE TOUR avant le cap de la pointe : la victime part de
            // cote, pas devant.
            int push = *(short*)(e + 0x32) - 0x400;
            *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
            *(ushort*)(victim + 0x96) = 0x10;          // le clignotement de degat
            *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;
            *(uint*)victim |= 0x40;
        }

        // Ghidra: LAB_004809d3 — la bascule. Elle a lieu dans l'image meme ou le bit vient d'etre
        // pose, donc la seconde vie commence au pas suivant avec un age tout neuf.
        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x48) = *(int*)(e + 0x60);     // l'ancre se pose au SOL

            *(short*)(e + 0x10) = 0;
            *(uint*)(e + 0x28) = 0x1400;               // X a 0x1400, Y a zero : dword, comme ecrit
            *(ushort*)(e + 0x2c) = 0x1400;
            *(short*)(e + 0x34) = 0;
            *(short*)(e + 0x1a) = 1;
            *(ushort*)(e + 0x9a) = 0x7fff;             // la gravite, la plus forte du depot
        }
    }

    /// <summary>
    /// Ghidra: FUN_00480a20 @ 0x00480a20 — <b>une aiguille de Stalagtite</b> : le sous-type
    /// <c>0x73</c>, creneau 115 de <see cref="s_StaticEntityHandlers_00551f30"/>. 79 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle ne se deplace pas, elle est placee.</b> Sa position vaut a chaque image l'ancre de
    /// son pere plus son propre ecart <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>, fige a la naissance :
    /// les six aiguilles forment un chapelet rigide que la pointe traine derriere elle. Seul son
    /// tangage bouge, de <c>0x100</c> en arriere par image.
    /// </para>
    /// <para>
    /// <b>Elle meurt du bit de son pere.</b> Elle prend <c>0x2000000</c> dans le mot d'etat du pere,
    /// se le pose, et s'eteint dans la foulee — le sens de lecture inverse de celui d'Assaut
    /// magique, ou la lame posait le bit <i>sur</i> son pere.
    /// </para>
    /// </summary>
    private static void TickStalactiteSpike_00480A20(byte* record)
    {
        byte* e = record;
        byte* parent = *(byte**)(e + 0x6c);

        uint state = *(uint*)e | (*(uint*)parent & 0x2000000);
        *(uint*)e = state;

        if ((state & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        *(int*)(e + 0x1c) = *(int*)(e + 0x44) + *(int*)(parent + 0x1c);
        *(int*)(e + 0x20) = *(int*)(e + 0x48) + *(int*)(parent + 0x20);
        *(ushort*)(e + 0x34) += 0xff00;                // -0x100 de tangage
        *(int*)(e + 0x24) = *(int*)(e + 0x4c) + *(int*)(parent + 0x24);
    }

    /// <summary>
    /// Ghidra: FUN_00480a70 @ 0x00480a70 — <b>une pierre de l'eboulement</b> : le sous-type
    /// <c>0x74</c>, creneau 116 de <see cref="s_StaticEntityHandlers_00551f30"/>. 283 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle ne vit que par sa hauteur.</b> Son echelle Y <c>+0x2a</c> passe par trois regimes,
    /// tous par demi-ecart : elle rejoint <c>0x1900</c> pendant quatre images, redescend vers
    /// <c>0x1402</c> jusqu'a la douzieme, puis <b>se divise par deux</b> jusqu'a la seizieme, ou
    /// elle s'eteint. La pierre jaillit, tient, et s'affaisse.
    /// </para>
    /// <para>
    /// <b>Elle ne frappe que pendant sa montee et son plateau</b> — au-dela de la douzieme image la
    /// sonde n'est plus consultee. Celle-ci commence <b>quatre-vingts unites en arriere</b>
    /// (<c>-0x50</c> d'avance) et porte <c>0xc8</c> de profondeur sur <c>0xa0</c> d'ouverture :
    /// elle prend ce qui se tient au pied de la pierre, pas devant elle.
    /// </para>
    /// </summary>
    private static void TickStalactiteShard_00480A70(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = unchecked((short)0xffb0);
        probe[2] = 0x50;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0xa0;

        uint state = *(uint*)e;

        if ((state & 0x40000000) != 0)
        {
            *(uint*)e = 0;                             // le decor l'arrete net
            return;
        }

        short age = *(short*)(e + 0x10);

        if (age < 4)
        {
            *(uint*)e = state | 0x80;
            *(short*)(e + 0x2a) += (short)((0x1900 - *(short*)(e + 0x2a)) >> 1);
        }
        else if (age < 0x0c)
        {
            *(short*)(e + 0x2a) += (short)((0x1402 - *(short*)(e + 0x2a)) >> 1);
        }
        else if (age < 0x10)
        {
            *(short*)(e + 0x2a) -= (short)(*(short*)(e + 0x2a) >> 1);
        }
        else
        {
            *(uint*)e = 0;
        }

        if (age < 0x0c)
        {
            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

            if (victim != null)
            {
                ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

                // La meme poussee de cote que la pointe, au quart de tour pres.
                int push = *(short*)(e + 0x32) - 0x400;
                *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(push) << 4;
                *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(push) << 4;
                *(uint*)victim |= 0x40;
            }
        }

        // Elle est incrementee meme sur l'image qui l'eteint : le binaire ne rend la main qu'ici.
        *(short*)(e + 0x10) += 1;
    }

    // =====================================================================
    // SATANAS — le vingt-cinquieme sort, ecole 0 indice 24, sous-type 0x18
    //
    // Trois corps neufs, absents de Blaze.c.
    //
    //   0x00480B90   LE SORT. Sous-type 0x18 de 0x00551c30, 688 octets.
    //   0x00480E40   LE SCEAU, sous-type 0x87 de 0x00551f30, 1038 octets. DOUZE exemplaires.
    //   0x00481250   LE DEMON, sous-type 0x88 de 0x00551f30, 1050 octets. Un seul, et tard.
    //
    // LE SEUL SORT DU DEPOT QUI TUE SANS PASSER PAR LES DEGATS. Le demon n'appelle pas
    // ResolveSpellHit_0053A2B0 : il appelle SlayTarget_0053CDF0, l'entree 47 et derniere de la
    // table des 48 — la mort immediate, la seule qui exige DEUX jets de sauvegarde d'affilee. Les
    // vingt-quatre sorts precedents retiraient des points de vie ; celui-ci en demande zero.
    //
    // ET LE SEUL QUI SE DEROULE EN TROIS ACTES, CHACUN GARDE PAR UN BIT DIFFERENT.
    //
    //   1. LA RONDE. A la premiere image le sort pose douze sceaux en cercle autour du lanceur, a
    //      la hauteur de sa tete et au rayon de celle-ci, espaces de 0x155 (un douzieme de tour) et
    //      retardes chacun de deux images de plus que le precedent (+0x12 = 1, 3, 5 … 23). Ils
    //      grossissent sur place jusqu'a la teinte 0xF0 ; le premier a y arriver pose 0x1000000 sur
    //      le sort.
    //   2. LA CHASSE. Ce bit ouvre la recherche : le sort appelle FindNearestOpponent_00474EB0 a
    //      chaque image, et s'eteint au bout de 0x3c images s'il ne trouve rien. Des qu'il trouve,
    //      il se lie a la proie dans les deux sens (+0x78 chez lui, +0x74 chez elle), pose son bit
    //      2 — que les sceaux heritent avec la proie — et remet son age a zero.
    //   3. LA MISE A MORT. Chaque sceau vole vers la proie et s'y ACCROCHE au contact ; chaque
    //      accrochage incremente le +0x14 du sort. Quand les DOUZE tiennent, le sort fait naitre le
    //      demon 0x88 a la position du lanceur. Il approche 0x3c images, puis frappe.
    //
    // LE CONTROLE DE PLACE EST LE SECOND DU DEPOT. Comme Demon polaire, il compare le curseur du
    // pool 0x006007a0 a une borne avant de partir — 0x6c ici contre 0x76 la-bas, parce qu'il lui
    // faut douze emplacements d'un coup et non six.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551b88</c> — l'atlas de la gerbe d'eclats du demon de <b>Satanas</b>.
    /// <b>Trois enregistrements</b>, bornes par le corps consommateur : le demon pose
    /// <c>rand % 3</c> comme indice.
    ///
    /// <para>
    /// Comme <see cref="s_Descriptors_00551AF8"/>, il commence au milieu d'un groupe : la ligne qui
    /// le precede (<c>0x00551b80</c>, <c>d8 d0 0f 0f 00 00 10 00</c>) appartient a la meme planche
    /// et a la meme famille, et n'a pas encore de consommateur connu.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551B88 =
    [
        0xd8, 0xe0, 0x0f, 0x0f, 0x00, 0x00, 0x10, 0x00,   // 0x00551b88
        0xd8, 0xf0, 0x0f, 0x0f, 0x00, 0x00, 0x10, 0x00,   // 0x00551b90
        0xe8, 0xf0, 0x0f, 0x0f, 0x00, 0x00, 0x10, 0x00,   // 0x00551b98
    ];

    private static byte* s_Descriptors00551B88Storage;

    /// <summary>Les trois descripteurs en memoire native.</summary>
    private static byte* Descriptors00551B88()
        => NativeCopy(s_Descriptors_00551B88, ref s_Descriptors00551B88Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552588</c> — <b>le sixieme script de sons differes</b>, celui de
    /// <see cref="TickSatanas_00480B90"/>. Le son <c>0x31</c>, une image d'attente, puis le
    /// <c>0x12</c> qui termine — l'invocation, puis la ronde qui s'allume.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552588 =
    [
        0x31, 0x01, 0x01, 0x00, 0x94, 0x01, 0x00, 0x12,   // 0x00552588
        0x12, 0x00, 0x01, 0x00, 0x53, 0x06, 0x00, 0x0f,   // 0x00552590 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript00552588Storage;

    /// <summary>Les deux pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00552588()
        => NativeCopy(s_DelayedSoundScript_00552588, ref s_DelayedSoundScript00552588Storage);

    /// <summary>
    /// Ghidra: FUN_00480b90 @ 0x00480b90 — <b>Satanas</b> : le sous-type <c>0x18</c>, creneau 24 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 688 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le sort lui-meme ne touche jamais personne.</b> Il n'a ni sonde ni appel a
    /// <see cref="ResolveSpellHit_0053A2B0"/> : il pose douze sceaux, attend leur signal, cherche
    /// une proie, et quand les douze la tiennent il fait naitre le demon qui tue. Il est
    /// l'ordonnanceur des deux autres corps.
    /// </para>
    /// <para>
    /// <b>Il compte de trois facons a la fois.</b> <c>+0x16</c> est le nombre de sceaux encore en
    /// vie — a zero, le sort meurt ; <c>+0x14</c> le nombre de ceux accroches a la proie — a douze,
    /// le demon naît ; <c>+0x12</c> monte tant que le joueur tient un bouton de face, plafonne a
    /// <c>+0x14 &#215; 5</c>. Les trois sont remis a zero a la fin de la premiere image, apres avoir
    /// servi de compteur de boucle a la pose des sceaux.
    /// </para>
    /// <para>
    /// <b>Il lit la manette par la proie, pas par le lanceur.</b> L'indice de personnage vient de
    /// l'entite liee en <c>+0x78</c> — de <c>DAT_005de538</c> si elle porte <c>0x10000</c>, de son
    /// <c>+0x08</c> sinon — et le masque est <c>0xf0</c> sur l'octet <c>+2</c> de l'etat de manette,
    /// les quatre boutons de face. Aucun autre corps du depot ne s'adresse a la manette par ce
    /// chemin.
    /// </para>
    /// </summary>
    private static void TickSatanas_00480B90(byte* record)
    {
        byte* e = record;

        uint state = *(uint*)e;
        byte* caster = *(byte**)(e + 0x74);

        if ((state & 0x2000000) != 0)
        {
            *(uint*)caster &= 0xffffff7f;               // le lanceur est rendu a lui-meme
            byte* bound = *(byte**)(e + 0x78);
            if (bound != null) *(uint*)(bound + 0x74) = 0;   // la proie oublie son marqueur
            *(uint*)e = 0;
            return;
        }

        if ((state & 2) != 0)
        {
            // ================================================ la proie est designee
            short latched = *(short*)(e + 0x14);

            if (latched != 0)
            {
                if (*(short*)(e + 0x10) == 0 && latched == 0x0c)
                {
                    // Les douze sceaux tiennent : le demon naît a la place du lanceur.
                    *(short*)(e + 0x10) = 1;
                    *(uint*)(e + 0x28) = 0;
                    *(ushort*)(e + 0x2c) = 0;
                    *(uint*)(e + 0x38) = 0;

                    *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
                    *(int*)(e + 0x24) = *(int*)(caster + 0x24);
                    *(ushort*)(e + 0x32) = *(ushort*)(caster + 0x32);
                    *(ushort*)(e + 0x34) = 0x400;

                    SpawnCastEntity_0047AAE0(e, 0x88, e[0x0b], -1);
                }

                state = *(uint*)e;                     // Ghidra: LAB_00480c33, le mot est relu

                if ((state & 0x400000) != 0)
                {
                    byte* bound = *(byte**)(e + 0x78);

                    uint index = (*(uint*)bound & 0x10000) != 0
                        ? (uint)(byte)Mem.s_Unknown_005de538
                        : bound[8];

                    int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];

                    // Le masque 0xf0 sur l'octet +2 : les quatre boutons de face, tenus.
                    if ((Inp.s_InputStateBase_00604a20[slot * 0x10 + 2] & 0xf0) != 0
                        && *(short*)(e + 0x12) < *(short*)(e + 0x14) * 5)
                    {
                        *(short*)(e + 0x12) += 1;
                    }
                }
            }

            // Ghidra: LAB_00480c8a — la proie perdue ou le lanceur au decor posent le bit 4, que
            // les sceaux relisent pour se decrocher.
            byte* prey = *(byte**)(e + 0x78);

            if ((prey != null && (*(uint*)prey & 0x50000000) != 0)
                || (*(uint*)(*(byte**)(e + 0x74)) & 0x40000000) != 0)
            {
                state |= 4;
                *(uint*)e = state;
            }

            // Plus un seul sceau en vie : le sort n'a plus rien a ordonner.
            if (*(short*)(e + 0x16) == 0) *(uint*)e |= 0x2000000;
            return;
        }

        // ==================================================== pas encore de proie
        if (*(short*)(e + 0x10) == 0)
        {
            // Le controle de place — douze emplacements d'un coup.
            if (*Mem.Entity(0x006007a0) > 0x6c)
            {
                *(uint*)e = 0;
                return;
            }

            *(uint*)caster |= 0x80;                    // le lanceur est immobilise
            e[0x36] |= 6;
            *(ushort*)(e + 0x2e) = 0xffc0;
            *(byte**)(e + 0x78) = null;

            int height = StatusBubbleHeight_00538BC0((uint*)caster);

            int anchorX = *(int*)(e + 0x1c);
            int anchorZ = *(int*)(e + 0x24);

            *(ushort*)(e + 0x34) += 0xff00;            // le tangage descend d'un cran
            *(int*)(e + 0x20) += (height + 0x1e) << 0xc;
            *(ushort*)(e + 0x32) += 0x400;

            *(uint*)(e + 0x38) = 0;
            *(uint*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(short*)(e + 0x12) = 1;
            *(short*)(e + 0x14) = 1;

            int pitch = *(short*)(e + 0x34);
            e[0x04] = 0x14;

            // Douze sceaux, un douzieme de tour d'ecart, deux images de retard de plus a chaque
            // fois : +0x12 et +0x14 servent ici de compteurs et partent dans la copie de chaque
            // enfant, qui les relit comme son retard et son rang.
            for (int seal = 0; seal < 0x0c; seal++)
            {
                int yaw = *(short*)(e + 0x32) - 0x400;

                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * height + anchorX;
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * height + anchorZ;

                SetVectorFromAngles_00532AD0((int*)(e + 0x50), yaw, pitch, 0x80);
                SpawnCastEntity_0047AAE0(e, 0x87, e[0x0b], -1);

                *(ushort*)(e + 0x32) += 0x155;
                *(short*)(e + 0x12) += 2;
                *(short*)(e + 0x14) += 1;
            }

            *(short*)(e + 0x16) = 0x0c;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;

            Aud.QueueDelayedSoundScript(DelayedSoundScript00552588(), (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00480de6 — la chasse ne s'ouvre qu'au signal des sceaux.
        state = *(uint*)e;
        *(ushort*)(e + 0x10) += 1;

        if ((state & 0x1000000) == 0) return;

        byte* found = FindNearestOpponent_00474EB0((uint*)caster, 0x0cf66a00, 1);

        if (found != null)
        {
            // Le lien est reciproque, comme celui de la fleche magique.
            *(byte**)(e + 0x78) = found;
            *(byte**)(found + 0x74) = e;

            *(short*)(e + 0x10) = 0;
            *(uint*)e |= 2;
            return;
        }

        if (*(short*)(e + 0x10) >= 0x3c || (*(uint*)caster & 0x40000000) != 0)
        {
            *(uint*)e |= 0x2000000;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00480e40 @ 0x00480e40 — <b>un sceau de Satanas</b> : le sous-type <c>0x87</c>,
    /// creneau 135 de <see cref="s_StaticEntityHandlers_00551f30"/>. 1038 octets, <b>absente de
    /// <c>Blaze.c</c></b>. Douze exemplaires.
    ///
    /// <para>
    /// <b>Trois vies, dans cet ordre.</b> Sans le bit <c>2</c> il grandit sur place vers
    /// <c>0x800</c> et s'eclaircit de <c>0x10</c> par image ; arrive a <c>0xF0</c> il pose
    /// <c>0x1000000</c> sur le sort — c'est ce qui ouvre la chasse. Le bit <c>2</c> et la proie lui
    /// arrivent ensuite du sort, par heritage direct du mot d'etat. Il decompte alors son retard
    /// <c>+0x12</c>, joue son bruit a zero, et vole.
    /// </para>
    /// <para>
    /// <b>Son vol est une poursuite amortie.</b> A chaque image il recalcule les deux angles vers la
    /// tete de la proie, en tire un pas de <c>0x10</c> qu'il <b>ajoute</b> a sa vitesse, puis retire
    /// un huitieme de celle-ci : une poussee continue freinee, pas un cap. Son orientation vient
    /// ensuite de son deplacement reel.
    /// </para>
    /// <para>
    /// <b>Au contact il s'accroche.</b> Il frappe une fois, retient l'ecart a la victime dans
    /// <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>, se pose le bit <c>4</c> et ne fait plus que suivre —
    /// et il incremente le <c>+0x14</c> du sort. <b>Faute de proie il s'etale</b> : son echelle X
    /// enfle vers <c>0x5000</c> pendant que les deux autres fondent d'un quart, et il meurt quand
    /// son echelle Y passe sous 8.
    /// </para>
    /// </summary>
    private static void TickSatanasSigil_00480E40(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6];
        probe[0] = unchecked((short)0xfc00);
        probe[1] = unchecked((short)0xff80);
        probe[2] = 0x32;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x80;

        uint state = *(uint*)e;

        if ((state & 0x2000000) != 0)
        {
            short shrinking = (short)(*(short*)(e + 0x2a) - (*(short*)(e + 0x2a) >> 2));
            *(short*)(e + 0x2a) = shrinking;
            *(short*)(e + 0x2c) -= (short)(*(short*)(e + 0x2c) >> 2);

            if (shrinking < 8) *(uint*)e = 0;
            return;
        }

        byte* spell = *(byte**)(e + 0x6c);

        if ((state & 0x40000000) != 0)
        {
            // Le decor : il previent le sort et lui force son compteur de charge a 0x3c.
            *(uint*)spell |= 4;
            *(short*)(*(byte**)(e + 0x6c) + 0x12) = 0x3c;
        }

        byte* prey = *(byte**)(e + 0x78);

        // Ghidra: LAB_00480ece — deux facons de lacher la proie : le sort le demande (son bit 4),
        // ou la charge a depasse cinq fois le rang de ce sceau alors qu'il tenait deja.
        bool release = prey != null && (*(uint*)spell & 4) != 0;

        if (!release)
        {
            release = *(short*)(spell + 0x12) >= *(short*)(e + 0x14) * 5
                      && (*(uint*)e & 4) != 0;
        }

        if (release)
        {
            *(uint*)e &= 0xfffffffb;
            *(byte**)(e + 0x78) = null;
            *(short*)(spell + 0x16) -= 1;
            prey = null;
        }

        if (prey != null && (*(uint*)e & 4) != 0)
        {
            // Accroche : il ne se deplace plus, il est place.
            *(int*)(e + 0x1c) = *(int*)(prey + 0x1c) + *(int*)(e + 0x44);
            *(int*)(e + 0x20) = *(int*)(prey + 0x20) + *(int*)(e + 0x48);
            *(int*)(e + 0x24) = *(int*)(prey + 0x24) + *(int*)(e + 0x4c);
            return;
        }

        if ((*(uint*)e & 2) == 0)
        {
            // ============================================ Ghidra: LAB_004811de, le sceau qui monte
            short scale = *(short*)(e + 0x28);
            scale += (short)((0x800 - scale) >> 2);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
            *(short*)(e + 0x2c) = scale;

            // Il herite du sort ses deux bits : la fin, et la designation de la proie.
            *(uint*)e |= *(uint*)spell & 0x2000002;

            if ((*(uint*)e & 2) != 0) *(byte**)(e + 0x78) = *(byte**)(spell + 0x78);

            if ((*(uint*)e & 0x2000000) != 0) *(short*)(spell + 0x16) -= 1;

            uint tint = *(uint*)(e + 0x38);

            if (tint < 0xf0)
            {
                *(uint*)(e + 0x38) = tint + 0x10;
            }
            else
            {
                // Le signal qui ouvre la chasse du sort.
                *(uint*)spell |= 0x1000000;
            }

            return;
        }

        // ================================================ Ghidra: LAB_00480f3c, le sceau qui vole
        short delay = *(short*)(e + 0x12);

        if (delay != 0)
        {
            delay -= 1;
            *(short*)(e + 0x12) = delay;
            if (delay != 0) return;

            Aud.PlayPositionalSound(0x12, 0x7a7, 0x1200, (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(uint*)e |= 0x80;
        }

        // Ghidra: LAB_00480f88 — la position d'avant le pas, pour en deduire l'orientation apres.
        int keptX = *(int*)(e + 0x1c);
        int keptY = *(int*)(e + 0x20);
        int keptZ = *(int*)(e + 0x24);

        int yaw;
        int pitch;

        if (prey != null)
        {
            int dx = (*(int*)(prey + 0x1c) - keptX) >> 0xc;

            // La visee monte a la tete de la proie, pas a ses pieds.
            int preyHead = StatusBubbleHeight_00538BC0((uint*)prey);
            int dy = (((preyHead & ~1) << 0xb) + *(int*)(prey + 0x20) - *(int*)(e + 0x20)) >> 0xc;
            int dz = (*(int*)(prey + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            yaw = Winsys3d.Ordinal_457(dx, dz);
            pitch = Winsys3d.Ordinal_457(dy, flat);
        }
        else
        {
            // Ghidra: LAB_00481011 — sans proie il s'eteint et s'etale.
            uint tint = *(uint*)(e + 0x38);
            if (tint != 0) *(uint*)(e + 0x38) = tint - 0x10;

            short sx = *(short*)(e + 0x28);
            *(short*)(e + 0x28) = (short)(sx + ((0x5000 - sx) >> 2));

            short sy = *(short*)(e + 0x2a);
            sy -= (short)(sy >> 2);
            *(short*)(e + 0x2a) = sy;

            short sz = *(short*)(e + 0x2c);
            *(short*)(e + 0x2c) = (short)(sz - (sz >> 2));

            if (sy < 8)
            {
                *(uint*)e = 0;
                return;
            }

            yaw = *(short*)(e + 0x32) - 0x400;
            pitch = *(short*)(e + 0x34);
        }

        // Ghidra: LAB_00481075 — le pas s'AJOUTE a la vitesse, qui perd ensuite un huitieme.
        int* step = stackalloc int[3];
        SetVectorFromAngles_00532AD0(step, yaw, pitch, 0x10);

        int vx = *(int*)(e + 0x50) + step[0];
        int vy = *(int*)(e + 0x54) + step[1];
        int vz = *(int*)(e + 0x58) + step[2];

        vx -= vx >> 3;
        vy -= vy >> 3;
        vz -= vz >> 3;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;

        int movedX = keptX + vx;
        int movedY = keptY + vy;
        int movedZ = keptZ + vz;

        *(int*)(e + 0x1c) = movedX;
        *(int*)(e + 0x20) = movedY;
        *(int*)(e + 0x24) = movedZ;

        // L'orientation vient du deplacement REEL, pas du cap vise.
        int mx = (movedX - keptX) >> 0xc;
        int my = (movedY - keptY) >> 0xc;
        int mz = (movedZ - keptZ) >> 0xc;

        int travelled = (int)Winsys3d.Ordinal_442((uint)(mx * mx + mz * mz));

        *(short*)(e + 0x32) = (short)(Winsys3d.Ordinal_457(mx, mz) + 0x400);
        *(short*)(e + 0x34) = (short)Winsys3d.Ordinal_457(my, travelled);

        if (prey == null) return;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        // Il ne mord que SA proie : tout autre corps traverse.
        if (victim == null || victim != *(byte**)(e + 0x78)) return;

        ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

        *(ushort*)(victim + 0x96) = 1;             // le clignotement de degat
        *(uint*)victim |= 0x40;

        *(short*)(spell + 0x14) += 1;              // un sceau de plus qui tient

        *(int*)(e + 0x44) = *(int*)(e + 0x1c) - *(int*)(victim + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20) - *(int*)(victim + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24) - *(int*)(victim + 0x24);

        Mem.s_BackColourR_005e93a0 = 0xff;         // le fond vire au rouge, une image
        *(uint*)e |= 4;

        Aud.PlayPositionalSound(0x2e, 0xca6, 0x1200, (int*)(e + 0x1c));
    }

    /// <summary>
    /// Ghidra: FUN_00481250 @ 0x00481250 — <b>le demon de Satanas</b> : le sous-type <c>0x88</c>,
    /// creneau 136 de <see cref="s_StaticEntityHandlers_00551f30"/>. 1050 octets, <b>absente de
    /// <c>Blaze.c</c></b>. Un seul, et seulement quand les douze sceaux tiennent.
    ///
    /// <para>
    /// <b>C'est lui qui tue.</b> Au contact de la proie il appelle
    /// <see cref="SlayTarget_0053CDF0"/> — l'entree 47 de la table des 48, la mort immediate — et
    /// non <see cref="ResolveSpellHit_0053A2B0"/>. Aucun autre corps de sort du depot ne fait cela.
    /// Si les deux jets de sauvegarde passent, il n'en reste que le bruit ; sinon la proie s'eteint
    /// et six eclats <c>0xbf</c> partent en gerbe.
    /// </para>
    /// <para>
    /// <b>Il recopie sa fiche de sort sur le lanceur.</b> Juste avant de frapper il ecrit son
    /// <c>+0x06</c> — la paire famille/indice — dans le <c>+0x06</c> du lanceur. C'est la seule
    /// ecriture du depot d'un enfant vers ce champ de son invocateur, et elle est faite <b>avant</b>
    /// l'appel, donc <c>SlayTarget</c> la lit.
    /// </para>
    /// <para>
    /// <b>Deux allures, separees par <c>0x1000000</c>.</b> Pendant 0x3c images il approche en
    /// tournant ses deux angles <b>par le plus court chemin</b>, un huitieme de l'ecart par image,
    /// et grossit vers <c>0x2000</c>/<c>0x1000</c>/<c>0x1000</c>. Le bit pose, il fonce a
    /// <c>0x80</c> par image et frappe ; douze images plus tard il s'eteint, en prevenant le sort.
    /// </para>
    /// <para>
    /// <b>Sa garde d'abandon ne peut pas se declencher.</b> Pendant l'approche il s'eteint si le
    /// <c>+0x12</c> du sort divise par dix atteint 12, soit 120 ; or ce compteur est plafonne a
    /// <c>+0x14 &#215; 5</c>, et <c>+0x14</c> ne depasse pas les douze sceaux — donc 60. Le sceau
    /// qui touche le decor le force a <c>0x3c</c>, ce qui ne change rien. La branche est ecrite
    /// telle quelle : c'est une borne du binaire, pas une omission.
    /// </para>
    /// </summary>
    private static void TickSatanasReaper_00481250(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6];
        probe[0] = unchecked((short)0xfc00);
        probe[1] = unchecked((short)0xff80);
        probe[2] = 0x32;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x80;

        uint state = *(uint*)e;

        // Le seul corps de ce sort a tester les deux bits ensemble : la fin voulue et le decor.
        if ((state & 0x42000000) != 0)
        {
            short shrinking = (short)(*(short*)(e + 0x2a) - (*(short*)(e + 0x2a) >> 2));
            *(short*)(e + 0x2a) = shrinking;
            *(short*)(e + 0x2c) -= (short)(*(short*)(e + 0x2c) >> 2);

            if (shrinking < 8) *(uint*)e = 0;
            return;
        }

        byte* spell = *(byte**)(e + 0x6c);

        if ((state & 0x1000000) == 0)
        {
            // ============================================ Ghidra: LAB_00481507, l'approche
            byte* prey = *(byte**)(e + 0x78);

            *(uint*)e = state | 0x80;

            int dx = (*(int*)(prey + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            int preyHead = StatusBubbleHeight_00538BC0((uint*)prey);
            int dy = (((preyHead & ~1) << 0xb)
                      + (*(int*)(prey + 0x20) - *(int*)(e + 0x20))) >> 0xc;
            int dz = (*(int*)(prey + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            short heading = *(short*)(e + 0x32);
            short tilt = *(short*)(e + 0x34);

            int yawGap = (Winsys3d.Ordinal_457(dx, dz) + 0x400 - heading) & 0xfff;
            int tiltGap = (Winsys3d.Ordinal_457(dy, flat) - tilt) & 0xfff;

            // Le plus court chemin : au-dela d'un demi-tour, le huitieme est NIE — le meme choix
            // que le fantome de Trouille et que la tete du Demon polaire.
            *(short*)(e + 0x32) += (short)(yawGap < 0x800 ? yawGap >> 3 : -((0x1000 - yawGap) >> 3));
            *(short*)(e + 0x34) =
                (short)(tilt + (tiltGap < 0x800 ? tiltGap >> 3 : -((0x1000 - tiltGap) >> 3)));

            short sx = *(short*)(e + 0x28);
            *(short*)(e + 0x28) = (short)(sx + ((0x2000 - sx) >> 2));

            short sy = *(short*)(e + 0x2a);
            *(short*)(e + 0x2a) = (short)(sy + ((0x1000 - sy) >> 2));

            short sz = *(short*)(e + 0x2c);
            *(short*)(e + 0x2c) = (short)(sz + ((0x1000 - sz) >> 2));

            uint tint = *(uint*)(e + 0x38);
            if (tint < 0xf0) *(uint*)(e + 0x38) = tint + 0x10;

            *(ushort*)(e + 0x10) += 1;

            if (*(short*)(e + 0x10) >= 0x3c)
            {
                *(short*)(e + 0x10) = 0;
                *(uint*)e |= 0x1000000;                // il passe a la frappe
                return;
            }

            // Ghidra: `imul 66666667h` puis `sar edx,1` — une division par dix par multiplication
            // magique. Voir l'en-tete : le seuil de 12 est hors d'atteinte.
            if (*(short*)(spell + 0x12) / 10 >= 0x0c) *(uint*)e |= 0x2000000;
            return;
        }

        // ================================================ Ghidra: LAB_004812c8, la frappe
        int yaw;
        int pitch;

        if ((state & 1) != 0)
        {
            // Il a deja frappe : il continue tout droit, sans se reorienter.
            yaw = *(short*)(e + 0x32) - 0x400;
            pitch = *(short*)(e + 0x34);
        }
        else
        {
            byte* prey = *(byte**)(e + 0x78);

            int dx = (*(int*)(prey + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            int preyHead = StatusBubbleHeight_00538BC0((uint*)prey);
            int dy = (((preyHead & ~1) << 0xb)
                      + (*(int*)(prey + 0x20) - *(int*)(e + 0x20))) >> 0xc;
            int dz = (*(int*)(prey + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            yaw = Winsys3d.Ordinal_457(dx, dz);
            pitch = Winsys3d.Ordinal_457(dy, flat);

            *(short*)(e + 0x34) = (short)pitch;
            *(short*)(e + 0x32) = (short)(yaw + 0x400);
        }

        // Ghidra: LAB_00481356 — la vitesse est REMPLACEE, pas cumulee : il ne courbe pas.
        SetVectorFromAngles_00532AD0((int*)(e + 0x50), yaw, pitch, 0x80);

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null && victim == *(byte**)(e + 0x78))
        {
            *(uint*)e |= 1;

            // La fiche de sort du demon est recopiee sur le lanceur AVANT l'appel.
            *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

            if (SlayTarget_0053CDF0(*(uint**)(e + 0x74), (uint*)victim, 0) == 0)
            {
                // La mort a pris : six eclats partent, et le demon se prete a chacun d'eux sa
                // propre vitesse et sa propre echelle avant de les reprendre.
                int keptVx = *(int*)(e + 0x50);
                int keptVy = *(int*)(e + 0x54);
                int keptVz = *(int*)(e + 0x58);
                uint keptScaleXy = *(uint*)(e + 0x28);
                uint keptScaleZ = *(uint*)(e + 0x2c);
                uint keptTint = *(uint*)(e + 0x38);

                *(ushort*)(e + 0x9a) = 0x6000;         // la gravite des eclats

                for (int shard = 0; shard < 6; shard++)
                {
                    int spin = CrtRand();

                    *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(spin) << 5;
                    *(int*)(e + 0x54) = ((CrtRand() & 0x1f) + 0x1e) << 0xc;
                    *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(spin) << 5;

                    *(uint*)(e + 0x38) = 0x01f0f0f0;
                    *(uint*)(e + 0x28) = 0x02002000;
                    *(ushort*)(e + 0x2c) = 0x1f;
                    *(short*)(e + 0x1a) = 1;

                    byte* atlas = Descriptors00551B88() + CrtRand() % 3 * 8;
                    SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), atlas, 0xbf, 0, 0x0c);
                }

                *(int*)(e + 0x50) = keptVx;
                *(int*)(e + 0x54) = keptVy;
                *(int*)(e + 0x58) = keptVz;
                *(uint*)(e + 0x28) = keptScaleXy;
                *(uint*)(e + 0x2c) = keptScaleZ;
                *(uint*)(e + 0x38) = keptTint;

                *(ushort*)(victim + 0x96) = 1;         // le clignotement de degat
            }

            // Ghidra: LAB_004814c7 — le decor vire au rouge sombre, meme quand la mort est refusee.
            *(uint*)spell |= 4;

            Mem.s_BackColourR_005e93a0 = 0x80;         // le binaire ecrit R et V en un seul dword
            Mem.s_BackColourG_005e93a2 = 0;
            Mem.s_BackColourB_005e93a4 = 0;
        }

        // Ghidra: LAB_004814e4 — douze images, et il s'eteint en prevenant le sort.
        if (*(short*)(e + 0x10) >= 0x0c)
        {
            *(uint*)spell |= 4;
            *(uint*)e = 0;
        }

        *(ushort*)(e + 0x10) += 1;
    }

    // =====================================================================
    // AU SEC — le vingt-sixieme sort, ecole 0 indice 25, sous-type 0x19
    //
    //   0x00481670   LE SORT. Sous-type 0x19 de 0x00551c30, 169 octets. DECOMPILE dans Blaze.c.
    //   0x00481720   LA SPIRALE, sous-type 0x63 de 0x00551f30, 680 octets. Absente de Blaze.c.
    //                DEUX exemplaires par membre du groupe, soit huit au complet.
    //
    // LE SEUL SORT DU DEPOT QUI CHANGE D'ECRAN. A sa soixante-quinzieme image la spirale pose
    // s_GameStateId_005de510 = 2 et s_WorldMapStep_005645f8 = 0xfe : l'etat 2 est l'ecran de
    // destination (Screens/State02/), et 0xfe arrete la boucle de la carte du monde. « Au sec »
    // n'est donc pas un sort de combat du tout — c'est la sortie de donjon, et son nom le dit.
    //
    // ET LE PREMIER QUI S'ADRESSE AU GROUPE ENTIER. Le sort ne fait rien d'autre que balayer les
    // 0x9c octets par membre de Inp.s_SlotFlags_005e93c0 et engendrer DEUX spirales par membre
    // vivant — l'une au cap courant, l'autre a l'oppose (+0x800) —, puis s'eteindre dans la meme
    // image. Il n'a ni age, ni sonde, ni fin : son corps entier tient dans une seule passe.
    //
    // Hors d'un lanceur du groupe il ne fait RIEN. La premiere instruction teste 0x800000 sur son
    // propre mot d'etat et saute directement a l'extinction : un monstre qui lancerait cette fiche
    // n'obtiendrait pas meme une spirale.
    //
    // ET LA TRANSITION EST FAITE DEUX FOIS PAR MEMBRE. Les deux spirales d'un meme membre sont
    // identiques a leur cap pres — la seule difference que le sort leur donne, +0x16 a 0 puis a 1,
    // n'est JAMAIS relue par leur corps —, donc toutes deux atteignent l'age 0x30 et refont le bloc
    // en entier : le +0x12 du membre et le compteur 0x005de544 sont incrementes deux fois, l'anneau
    // 0x6c naît en double. Les ecritures d'etat, elles, sont idempotentes. C'est ecrit tel quel.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552598</c> — <b>le septieme script de sons differes</b>, celui de
    /// <see cref="TickRecallSpiral_00481720"/>, et <b>le plus long du depot</b> : cinq pas contre
    /// deux ou trois pour les six precedents.
    ///
    /// <para>
    /// Le son <c>0x30</c> tenu <c>0x20</c> images, le <c>0x31</c> tenu <c>0x10</c>, puis trois
    /// <c>0x12</c> a deux images d'ecart dont le dernier termine — la montee, le declic, et les
    /// trois notes de la disparition. Les trois derniers pas montent en hauteur — <c>0x0200</c>,
    /// <c>0x0329</c>, <c>0x0400</c> — la seule progression de ce genre parmi les sept scripts.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552598 =
    [
        0x30, 0x20, 0x01, 0x00, 0x29, 0x03, 0x00, 0x12,   // 0x00552598
        0x31, 0x10, 0x01, 0x00, 0x00, 0x01, 0x00, 0x12,   // 0x005525a0
        0x12, 0x02, 0x01, 0x00, 0x00, 0x02, 0x00, 0x12,   // 0x005525a8
        0x12, 0x02, 0x01, 0x00, 0x29, 0x03, 0x00, 0x12,   // 0x005525b0
        0x12, 0x00, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x005525b8 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript00552598Storage;

    /// <summary>Les cinq pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00552598()
        => NativeCopy(s_DelayedSoundScript_00552598, ref s_DelayedSoundScript00552598Storage);

    /// <summary>
    /// Ghidra: FUN_00481670 @ 0x00481670 — <b>Au sec</b> : le sous-type <c>0x19</c>, creneau 25 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 169 octets, <b>decompilee dans
    /// <c>Blaze.c</c></b> — la premiere fiche de sort de l'ecole 0 dans ce cas depuis Balle de feu.
    ///
    /// <para>
    /// <b>Il vit une seule image.</b> Il balaie le groupe, pose deux spirales par membre vivant, et
    /// remet son mot d'etat a zero avant de rendre la main. Toute la duree du sort appartient a ses
    /// enfants.
    /// </para>
    /// <para>
    /// <b>Il ne part que d'un membre du groupe</b> : le bit <c>0x800000</c> de son propre mot d'etat
    /// garde l'ensemble du corps. Le critere de selection d'un membre est le meme que partout —
    /// <c>0x80000000</c> pose (l'emplacement existe) et <c>0x50000000</c> absent (il n'est ni mort
    /// ni mourant).
    /// </para>
    /// </summary>
    private static void TickRecall_00481670(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x800000) != 0)
        {
            *(uint*)(e + 0x28) = 0x60000000;           // X a zero, Y a 0x6000 : un trait vertical
            *(uint*)(e + 0x38) = 0;
            *(ushort*)(e + 0x2c) = 0;
            e[0x04] = 0x3e;

            byte* member = Inp.s_SlotFlags_005e93c0;

            for (int remaining = s_PartySize_005e9020; remaining != 0; remaining--, member += 0x9c)
            {
                if ((*(uint*)member & 0x80000000) == 0) continue;
                if ((*(uint*)member & 0x50000000) != 0) continue;

                *(byte**)(e + 0x78) = member;

                // Les deux spirales ne different que par leur cap : leur corps ne relit jamais
                // le +0x16 que le sort leur distingue ici.
                *(ushort*)(e + 0x16) = 0;
                SpawnCastEntity_0047AAE0(e, 0x63, e[0x0b], -1);

                *(ushort*)(e + 0x32) += 0x800;
                *(ushort*)(e + 0x16) = 1;
                SpawnCastEntity_0047AAE0(e, 0x63, e[0x0b], -1);
            }
        }

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00481720 @ 0x00481720 — <b>une spirale d'Au sec</b> : le sous-type <c>0x63</c>,
    /// creneau 99 de <see cref="s_StaticEntityHandlers_00551f30"/>. 680 octets, <b>absente de
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle tourne autour de son membre, jamais autour d'elle-meme.</b> Sa position est
    /// recalculee a chaque image comme celle du membre plus un vecteur de rayon
    /// <c>echelle &#215; 15 &gt;&gt; 12</c> sur son propre cap ; sous l'age <c>0x30</c> sa hauteur
    /// est celle du <b>sol du membre</b> (<c>+0x60</c>). Le cap avance de <c>+0x12</c>, qui grandit
    /// lui-meme de 8 par image : la spirale accelere sans fin.
    /// </para>
    /// <para>
    /// <b>Quatre ages, et le dernier change d'ecran.</b> De 0 a 15 le rayon suit un sinus sur seize
    /// images (<c>sin &#215; hauteur / 75</c>) et la teinte monte de <c>0x80808</c> ; de 16 a 31 elle
    /// tourne sans rien faire ; de 32 a 47 le rayon suit le sinus <b>inverse</b> et la spirale
    /// <b>retrecit le membre lui-meme</b> en lui ecrivant <c>0x1000 - sin</c> dans ses deux echelles.
    /// A partir de 48 elle seme un eclat <c>0xbf</c> par image ; a l'image <c>0x30</c> exactement
    /// elle lache un anneau <c>0x6c</c> ancre au membre et <b>declenche la sortie</b> ; de 48 a 63
    /// elle emporte le membre vers le haut de <c>0x3c000</c> par image ; a 64 elle s'eteint.
    /// </para>
    /// <para>
    /// <b>La division par 75</b> est faite par le multiplicateur magique <c>0x1B4E81B5</c> avec un
    /// decalage de 3 — le meme que l'aureole d'incantation emploie avec 4 pour diviser par 150, et
    /// que la lame d'Assaut magique emploie deja tel quel.
    /// </para>
    /// </summary>
    private static void TickRecallSpiral_00481720(byte* record)
    {
        byte* e = record;
        byte* member = *(byte**)(e + 0x78);

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;     // le lanceur est immobilise
            e[0x36] |= 6;

            // Le decor vire au bleu-vert, et n'en revient qu'a l'extinction.
            *(uint*)Mem.Arena(0x005e9160) = 0x00400040;
            *(uint*)Mem.Arena(0x005e9164) = 0x00020040;

            *(ushort*)(e + 0x9a) = 0x5000;

            Aud.QueueDelayedSoundScript(DelayedSoundScript00552598(), (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00481772 — le pas angulaire grandit de 8 a chaque image.
        ushort spin = *(ushort*)(e + 0x12);
        *(ushort*)(e + 0x32) += spin;
        *(ushort*)(e + 0x12) = (ushort)(spin + 8);

        short age = *(short*)(e + 0x10);

        if (age < 0x10)
        {
            *(uint*)(e + 0x38) += 0x80808;

            int phase = (int)Winsys3d.GetSinTableEntry((age & 0x0f) << 6);
            int height = StatusBubbleHeight_00538BC0((uint*)member);

            // Ghidra: `imul 1B4E81B5h` puis `sar edx,3` — une division par 75.
            short scale = (short)(phase * height / 75);

            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2c) = scale;
        }
        else if (age >= 0x20 && age < 0x30)
        {
            *(uint*)(e + 0x38) += 0x70707;

            int shrink = 0x1000 - (int)Winsys3d.GetSinTableEntry((age & 0x0f) << 6);
            int height = StatusBubbleHeight_00538BC0((uint*)member);

            short scale = (short)(height * shrink / 75);

            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2c) = scale;

            // C'est ici que le membre lui-meme se met a retrecir.
            *(short*)(member + 0x28) = (short)shrink;
            *(short*)(member + 0x2c) = (short)shrink;
        }
        else if (age >= 0x30)
        {
            // ============================================ Ghidra: LAB_00481832, la disparition
            *(byte**)(e + 0x70) = null;

            int pitch = (CrtRand() & 0xff) - 0x380;
            int yaw = CrtRand();                       // le second tirage : l'ordre compte
            SetVectorFromAngles_00532AD0((int*)(e + 0x50), yaw, pitch, 0x40);

            uint keptScaleXy = *(uint*)(e + 0x28);
            *(int*)(e + 0x54) >>= 3;                   // la poussee verticale est divisee par huit
            uint keptScaleZ = *(uint*)(e + 0x2c);
            uint keptTint = *(uint*)(e + 0x38);

            // L'eclat est engendre a la taille et a la teinte qu'on lui prete ici, puis la spirale
            // reprend les siennes plus bas.
            *(uint*)(e + 0x28) = 0x10001000;
            *(ushort*)(e + 0x2c) = 3;
            *(uint*)(e + 0x38) = 0x00808080;
            *(short*)(e + 0x1a) = 3;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519B8(), 0xbf, e[0x0b], -1);

            if (*(short*)(e + 0x10) == 0x30)
            {
                // L'anneau de l'Onde de choc, ancre au membre par +0x70 — la branche « avec
                // ancre », l'inverse de celle que Durdur emploie.
                *(int*)(e + 0x44) = 0;
                *(int*)(e + 0x48) = 0;
                *(int*)(e + 0x4c) = 0;
                *(int*)(e + 0x50) = 0x1000;
                *(int*)(e + 0x54) = 0x0c00;
                *(int*)(e + 0x58) = 0x1000;
                *(uint*)(e + 0x28) = 0;
                *(ushort*)(e + 0x2c) = 0;
                *(byte**)(e + 0x70) = member;
                e[0x04] = 7;

                SpawnCastEntity_0047AAE0(e, 0x6c, 0, 4);

                uint flags = *(uint*)member;
                *(ushort*)(member + 0x98) = *(ushort*)(member + 0x32);
                *(short*)(member + 0x12) += 1;
                *(uint*)member = (flags & 0xf7fffdff) | 0x2000;

                // LA SORTIE. L'etat 2 est l'ecran de destination ; 0xfe arrete la boucle de la
                // carte du monde a la fin de l'image.
                s_Unknown_005de53c = (byte)Mem.s_Unknown_005de538;
                s_Unknown_005de544++;
                s_WorldMapStep_005645f8 = -2;          // 0xfe
                s_GameStateId_005de510 = 2;
            }

            // Ghidra: LAB_00481936 — la taille et la teinte pretees a l'eclat sont rendues.
            *(uint*)(e + 0x28) = keptScaleXy;
            *(uint*)(e + 0x2c) = keptScaleZ;
            *(uint*)(e + 0x38) = keptTint;

            if (*(short*)(e + 0x10) < 0x40)
            {
                *(int*)(e + 0x20) += 0x3c000;          // le membre monte
                *(uint*)member |= 0x5000;
            }
            else
            {
                *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
                *(ushort*)Mem.Arena(0x005e9166) = 0;   // le decor reprend sa couleur
                *(uint*)e = 0;
            }
        }

        // Ghidra: LAB_0048197a — la position, toujours prise sur celle du membre.
        int radius = (*(short*)(e + 0x28) * 15) >> 0xc;
        int heading = *(short*)(e + 0x32);

        *(int*)(e + 0x1c) =
            (int)Winsys3d.GetSinTableEntry(heading) * radius + *(int*)(member + 0x1c);
        *(int*)(e + 0x24) =
            Winsys3d.GetCosTableEntry(heading) * radius + *(int*)(member + 0x24);

        short now = *(short*)(e + 0x10);
        if (now < 0x30) *(int*)(e + 0x20) = *(int*)(member + 0x60);

        // Elle est incrementee meme sur l'image qui l'eteint.
        *(short*)(e + 0x10) = (short)(now + 1);
    }

    // =====================================================================
    // COLERE — le vingt-septieme sort, ecole 0 indice 26, sous-type 0x1a
    //
    // CINQ corps neufs, tous absents de Blaze.c — le sort le plus fourni du depot.
    //
    //   0x004819D0   LE SORT. Sous-type 0x1a de 0x00551c30, 883 octets.
    //   0x00481D50   LE HALO, sous-type 0x3e de 0x00551f30, 212 octets. QUATRE exemplaires.
    //   0x00481E30   LA TETE, sous-type 0x3f de 0x00551f30, 876 octets. Une seule.
    //   0x004821A0   L'ANNEAU, sous-type 0x40 de 0x00551f30, 348 octets. SEPT exemplaires.
    //   0x00482300   LA COLONNE, sous-type 0x41 de 0x00551f30, 107 octets. Une seule.
    //
    // UN SERPENT DE HUIT ANNEAUX, CHAINE PAR NUMERO D'EMPLACEMENT. Le sort reserve dix places par
    // ReserveEntitySlots_00475090, puis engendre la tete 0x3f et sept anneaux 0x40 a la suite. Avant
    // chaque naissance il lit l'indice libre courant du pool 0x006007a0, engendre, PUIS range cet
    // indice dans son +0x16 : l'anneau ne recoit donc pas son propre numero mais celui de son
    // PREDECESSEUR. Chaque anneau retrouve son devancier par 0x005edee0 + indice * 0x9c et se place
    // derriere lui — c'est la deuxieme communication par indice du depot, apres Demon polaire, et
    // la premiere qui forme une chaine et non une paire.
    //
    // ET LE SEUL APPEL DU DEPOT AVEC UNE VARIANTE NON NULLE. La tete passe 1 comme cinquieme
    // argument de ResolveSpellHit_0053A2B0, la ou les vingt-six sorts precedents passaient 0 : la
    // fiche est lue en +0x17 et +0x1a au lieu de +0x16 et +0x18. La documentation de cette fonction
    // disait « vaut 0 pour tous les appelants transcrits » ; ce n'est plus vrai.
    //
    // LE PLUS CHER A ENTRETENIR, DE LOIN : 2 PM toutes les CINQ images, contre 1 PM par dix pour
    // Eclair, le precedent record. Et 300 images de duree maximale hors joueur.
    //
    // La premiere image se joue en deux temps, separes par le bit 2 : quatre halos montent du sol et
    // decrementent le +0x16 du sort en arrivant a leur teinte de consigne ; a zero le sort se pose
    // le bit 2 et fait naitre la colonne, la tete et les anneaux.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005525c0</c> — <b>le huitieme script de sons differes</b>, celui de
    /// <see cref="TickWrath_004819D0"/>, et <b>de loin le plus long</b> : <b>huit pas</b>, contre
    /// cinq pour Au sec et deux ou trois pour les six premiers.
    ///
    /// <para>
    /// Il occupe <c>0x005525c0</c> a <c>0x005525ff</c>. Les huit scripts des sorts de l'ecole 0 se
    /// suivent desormais sans trou depuis <c>0x00552530</c>, chacun borne par l'octet de delai nul
    /// du precedent — mais la file continue : <c>0x00552600</c> en porte un neuvieme, sans
    /// consommateur connu.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_005525C0 =
    [
        0x1a, 0x01, 0x01, 0x00, 0x00, 0x02, 0x00, 0x12,   // 0x005525c0
        0x10, 0x1e, 0x01, 0x00, 0x29, 0x03, 0x00, 0x12,   // 0x005525c8
        0x2d, 0x01, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x005525d0
        0x13, 0x0a, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x005525d8
        0x15, 0x01, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x005525e0
        0x23, 0x28, 0x01, 0x00, 0xaa, 0x02, 0x00, 0x12,   // 0x005525e8
        0x23, 0x14, 0x01, 0x00, 0x53, 0x06, 0x00, 0x12,   // 0x005525f0
        0x23, 0x00, 0x01, 0x00, 0x52, 0x07, 0x00, 0x12,   // 0x005525f8 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript005525C0Storage;

    /// <summary>Les huit pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript005525C0()
        => NativeCopy(s_DelayedSoundScript_005525C0, ref s_DelayedSoundScript005525C0Storage);

    /// <summary>
    /// Ghidra: FUN_004819d0 @ 0x004819d0 — <b>Colere</b> : le sous-type <c>0x1a</c>, creneau 26 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 883 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Deux temps, separes par le bit 2.</b> A la premiere image il pose quatre halos
    /// <c>0x3e</c> au sol, chacun avec sa propre teinte de consigne et sa propre vitesse de rotation,
    /// et met son <c>+0x16</c> a 4. Chaque halo arrive a sa consigne decremente ce compteur ; a zero,
    /// <c>LAB_00481d31</c> pose le bit <c>2</c> — et le corps entier bascule dans le second temps.
    /// </para>
    /// <para>
    /// <b>Le second temps engendre le serpent.</b> Une colonne <c>0x41</c>, puis dix emplacements
    /// reserves d'un coup, puis la tete <c>0x3f</c> et sept anneaux <c>0x40</c> chaines par leur
    /// numero d'emplacement. Le septieme change de modele (<c>0x13</c> au lieu de <c>0x28</c>) :
    /// c'est la queue.
    /// </para>
    /// <para>
    /// <b>Le plus cher a entretenir du depot</b> : <b>2 PM toutes les cinq images</b>. Hors joueur il
    /// dure 300 images. Il frappe lui-meme, une fois par image, avec une sonde tres large
    /// (<c>0x190</c> de profondeur, <c>0x200</c> d'ouverture) dont le cap tourne de <c>0x190</c> par
    /// image — il balaie.
    /// </para>
    /// </summary>
    private static void TickWrath_004819D0(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = unchecked((short)0xff00);
        probe[2] = 0x100;
        probe[3] = 0x190;
        probe[4] = 0;
        probe[5] = 0x200;

        uint state = *(uint*)e;

        if ((state & 0x2000000) != 0)
        {
            *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
            *(ushort*)Mem.Arena(0x005e9166) = 0;       // le decor reprend sa couleur
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00481a27 — les quatre halos, une seule fois.
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            // Le controle de place — dix emplacements lui seront necessaires plus tard.
            if (*Mem.Entity(0x006007a0) > 0x6c)
            {
                *(uint*)e = 0;
                return;
            }

            byte* caster = *(byte**)(e + 0x74);
            *(uint*)caster |= 0x80;                    // le lanceur est immobilise

            e[0x36] = (byte)((e[0x36] & 0xfe) | 6);
            *(byte**)(e + 0x78) = null;
            *(ushort*)(e + 0x2e) = 0xffdd;
            *(int*)(e + 0x20) = *(int*)(e + 0x60);     // au sol

            // Le premier halo : teinte de depart 0x40000000, pas de 0x080808, consigne 0x808080.
            *(uint*)(e + 0x38) = 0x40000000;
            *(uint*)(e + 0x44) = 0x00080808;
            *(uint*)(e + 0x48) = 0x00808080;
            *(uint*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(ushort*)(e + 0x12) = 0x4000;             // echelle X visee
            *(ushort*)(e + 0x14) = 0x4000;             // echelle Y visee
            *(ushort*)(e + 0x16) = 0;                  // pas de rotation
            e[0x04] = 0x29;
            SpawnCastEntity_0047AAE0(e, 0x3e, 0, 0x0b);

            // Le deuxieme : plus sombre, plus lent, et il tourne d'un cran par image.
            *(ushort*)(e + 0x2e) = 0xffe2;
            *(uint*)(e + 0x38) = 0x80202020;
            *(uint*)(e + 0x44) = 0x00040404;
            *(uint*)(e + 0x48) = 0x00404040;
            SpawnCastEntity_0047AAE0(e, 0x3e, 1, 3);

            // Le troisieme : plus grand, presque noir, et il tourne de 0x20.
            *(ushort*)(e + 0x2e) = 0xffd8;
            *(uint*)(e + 0x38) = 0;
            *(uint*)(e + 0x44) = 8;
            *(uint*)(e + 0x48) = 0x40;
            *(uint*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(ushort*)(e + 0x12) = 0x5000;
            *(ushort*)(e + 0x14) = 0x5000;
            *(ushort*)(e + 0x16) = 0x20;
            e[0x04] = 0x22;
            SpawnCastEntity_0047AAE0(e, 0x3e, 1, 2);

            // Le quatrieme : le plus large, et il tourne a l'ENVERS.
            *(uint*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(ushort*)(e + 0x12) = 0x6000;
            *(ushort*)(e + 0x14) = 0x4000;
            *(ushort*)(e + 0x16) = 0xffe0;
            SpawnCastEntity_0047AAE0(e, 0x3e, 1, 2);

            Aud.QueueDelayedSoundScript(DelayedSoundScript005525C0(), (int*)(e + 0x1c));

            *(ushort*)(e + 0x16) = 4;                  // les quatre halos a attendre
        }

        // Ghidra: LAB_00481b59
        state = *(uint*)e;

        if ((state & 2) == 0)
        {
            // Les halos n'ont pas tous fini : rien d'autre a faire que compter.
            if (*(short*)(e + 0x16) == 0) *(uint*)e = state | 2;
            return;
        }

        *(uint*)Mem.Arena(0x005e9160) = 0x008000a0;
        *(uint*)Mem.Arena(0x005e9164) = 0x00020080;

        if (*(short*)(e + 0x10) == 1)
        {
            // ---------------------------------------------------------- la colonne
            *(uint*)(e + 0x38) = 0;
            *(uint*)(e + 0x28) = 0x7fff0000;
            *(ushort*)(e + 0x2c) = 0;
            e[0x04] = 0x36;
            SpawnCastEntity_0047AAE0(e, 0x41, 0, 0x0d);

            // ---------------------------------------------------------- le serpent
            // Dix emplacements sont RESERVES d'un coup : les huit naissances qui suivent les
            // prendront dans cet ordre, et c'est ce qui rend la chaine par indice previsible.
            ReserveEntitySlots_00475090(Mem.Entity(0x006007a0), 0x0a);

            e[0x36] &= 0xfb;
            *(ushort*)(e + 0x16) = 0x400;
            *(ushort*)(e + 0x32) = 0x400;
            *(ushort*)(e + 0x34) = 0x400;
            *(ushort*)(e + 0x14) = 0;
            *(uint*)(e + 0x38) = 0x02f0f0f0;
            *(uint*)(e + 0x28) = 0x20002000;
            *(ushort*)(e + 0x2c) = 0x2000;
            e[0x04] = 0x27;

            // L'indice libre courant est lu AVANT la naissance : la tete le porte, et c'est donc
            // son propre emplacement qu'elle emmene — les anneaux, eux, porteront celui de leur
            // devancier.
            byte* pool = Mem.Entity(0x006007a0);
            *(ushort*)(e + 0x16) = pool[*pool + 2];
            SpawnCastEntity_0047AAE0(e, 0x3f, 0, 0x0b);

            e[0x04] = 0x28;

            for (int link = 7; link != 0; link--)
            {
                if (link == 1) e[0x04] = 0x13;         // le septieme est la queue

                byte* slots = Mem.Entity(0x006007a0);
                ushort previous = slots[*slots + 2];

                SpawnCastEntity_0047AAE0(e, 0x40, 0, 0x0b);

                *(ushort*)(e + 0x16) = previous;
            }

            *(ushort*)(e + 0x34) = 0;
        }

        // Ghidra: LAB_00481c40 — l'entretien.
        state = *(uint*)e;

        if ((state & 0x800000) != 0)
        {
            byte* caster = *(byte**)(e + 0x74);
            uint index = caster[8];
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((*(ushort*)(perChar + 0x1fc4) & pad) == 0)
            {
                state |= 0x2000000;
                *(uint*)e = state;
            }

            // Ghidra: LAB_00481c8a — 2 PM toutes les CINQ images, le prix le plus lourd du depot.
            if (*(uint*)Mem.Entity(0x005f2cf0) % 5 == 0)
            {
                short mana = *(short*)(perChar + 0x14e);

                if (mana >= 2)
                {
                    *(short*)(perChar + 0x14e) = (short)(mana - 2);
                }
                else
                {
                    *(ushort*)(perChar + 0x14e) = 0;
                    *(uint*)e |= 0x2000000;
                }
            }
        }
        else if (*(short*)(e + 0x10) >= 0x12c)
        {
            *(uint*)e |= 0x2000000;
        }

        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;

        // Ghidra: LAB_00481ce2 — la sonde tourne de 0x190 par image : il balaie.
        *(ushort*)(e + 0x32) += 0x190;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;          // le clignotement de degat
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00481d50 @ 0x00481d50 — <b>un halo de Colere</b> : le sous-type <c>0x3e</c>,
    /// creneau 62 de <see cref="s_StaticEntityHandlers_00551f30"/>. 212 octets, <b>absente de
    /// <c>Blaze.c</c></b>. Quatre exemplaires, tous differents par les seuls champs que le sort leur
    /// prete.
    ///
    /// <para>
    /// <b>Il grandit, puis il s'allume, puis il fait signe.</b> Ses deux echelles rejoignent
    /// <c>+0x12</c> et <c>+0x14</c> par huitiemes ; tant que le pas d'echelle X depasse <c>0x20</c>
    /// il ne fait que grandir et tourner. Sous ce seuil il ajoute <c>+0x44</c> a sa teinte a chaque
    /// image, et quand celle-ci atteint <c>+0x48</c> il <b>decremente le <c>+0x16</c> de son
    /// pere</b> et se pose le bit <c>2</c> — apres quoi il ne fait plus que tourner.
    /// </para>
    /// <para>
    /// <b>Il meurt en s'eteignant</b>, pas d'un coup : sur le bit <c>0x2000000</c> herite du sort il
    /// <i>retire</i> <c>+0x44</c> par image et ne se coupe qu'une fois les vingt-quatre bits bas de
    /// sa teinte a zero. Seul corps du depot dont la fin est un fondu.
    /// </para>
    /// </summary>
    private static void TickWrathGlow_00481D50(byte* record)
    {
        byte* e = record;

        uint state = *(uint*)e;

        if ((state & 0x2000000) != 0)
        {
            uint tint = *(uint*)(e + 0x38) - *(uint*)(e + 0x44);
            *(uint*)(e + 0x38) = tint;

            if ((tint & 0xffffff) == 0)
            {
                *(uint*)e = 0;
                return;
            }

            *(ushort*)(e + 0x10) += 1;                 // Ghidra: LAB_00481e1c
            return;
        }

        if ((state & 2) != 0)
        {
            // Il a fait son signal : il ne fait plus que tourner, et attendre la fin du sort.
            uint inherited = state | (*(uint*)(*(byte**)(e + 0x6c)) & 0x2000000);

            *(ushort*)(e + 0x32) += *(ushort*)(e + 0x16);
            *(ushort*)(e + 0x10) += 1;
            *(uint*)e = inherited;
            return;
        }

        // Ghidra: LAB_00481da8 — les deux echelles rejoignent leur consigne par huitiemes.
        int stepX = (*(short*)(e + 0x12) - *(short*)(e + 0x28)) >> 3;
        short scaleY = *(short*)(e + 0x2a);
        int newY = ((*(short*)(e + 0x14) - scaleY) >> 3) + scaleY;

        *(short*)(e + 0x28) += (short)stepX;
        short newX = *(short*)(e + 0x28);

        *(short*)(e + 0x2a) = (short)newY;
        *(short*)(e + 0x2c) = newX;

        if (stepX < 0x20)
        {
            uint tint = *(uint*)(e + 0x38);

            if ((tint & 0xffffff) < *(uint*)(e + 0x48))
            {
                *(uint*)(e + 0x38) = tint + *(uint*)(e + 0x44);
                *(ushort*)(e + 0x32) += *(ushort*)(e + 0x16);
                *(ushort*)(e + 0x10) += 1;
                return;
            }

            // Ghidra: LAB_00481e06 — le signal qui fait avancer le sort.
            *(short*)(*(byte**)(e + 0x6c) + 0x16) -= 1;
            *(uint*)e |= 2;
        }

        // Ghidra: LAB_00481e14
        *(ushort*)(e + 0x32) += *(ushort*)(e + 0x16);
        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00481e30 @ 0x00481e30 — <b>la tete du serpent de Colere</b> : le sous-type
    /// <c>0x3f</c>, creneau 63 de <see cref="s_StaticEntityHandlers_00551f30"/>. 876 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle monte trente images, puis elle chasse.</b> Avant l'age <c>0x1e</c> son vecteur de
    /// visee est vertical pur (<c>dy = 0x190</c>, dx et dz nuls) : elle s'eleve. Ensuite elle prend
    /// l'adversaire le plus proche du <b>lanceur</b> — pas d'elle-meme — le retient dans
    /// <c>+0x78</c> avec le lien reciproque de la fleche magique, et le lache des qu'il porte
    /// <c>0x50000000</c>.
    /// </para>
    /// <para>
    /// <b>Faute de proie elle erre autour du lanceur</b>, et sa cible d'errance est retiree tous les
    /// 32 images seulement (<c>age &amp; 0x1f</c>), a plus ou moins 128 unites de lui ; sa hauteur
    /// visee y ajoute une ondulation de <c>sin &#215; 400 &gt;&gt; 12</c> sur une phase qui avance de
    /// <c>0x80</c> par image.
    /// </para>
    /// <para>
    /// <b>Sa visee passe par un limiteur avant d'arriver a ses angles</b> — deux etages, et c'est le
    /// seul corps du depot qui en ait deux. Les angles voulus sont d'abord ranges dans <c>+0x14</c>
    /// et <c>+0x16</c> en ne bougeant que de <c>0x100</c> par image quand l'ecart depasse
    /// <c>0x100</c> ; puis <c>+0x32</c> et <c>+0x34</c> rejoignent ceux-la d'un quart de l'ecart,
    /// par le plus court chemin. La poussee, elle, s'ajoute a la vitesse, qui perd ensuite un quart.
    /// </para>
    /// <para>
    /// <b>Et elle est le seul appelant du depot a passer une variante non nulle</b> a
    /// <see cref="ResolveSpellHit_0053A2B0"/> : <c>1</c>, ce qui lui fait lire <c>+0x17</c> et
    /// <c>+0x1a</c> de la fiche au lieu de <c>+0x16</c> et <c>+0x18</c>.
    /// </para>
    /// <para>
    /// <b>Sa fin est une derniere montee puis une chute.</b> Sur le bit <c>0x2000000</c> herite du
    /// sort, son age repart de zero : trente images vers le haut, soixante-dix vers le bas, puis
    /// elle se pose <c>0x20000000</c>, que les sept anneaux heritent l'un apres l'autre.
    /// </para>
    /// </summary>
    private static void TickWrathHead_00481E30(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6];
        probe[0] = unchecked((short)0xfc00);
        probe[1] = unchecked((short)0xff00);
        probe[2] = 0x64;
        probe[3] = 0x96;
        probe[4] = 0;
        probe[5] = 0x100;

        uint state = *(uint*)e;

        if ((state & 0x20000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        int dx = 0;
        int dy;
        int dz = 0;
        bool dying = false;                            // le chemin de fin saute l'heritage

        if ((state & 0x2000000) != 0)
        {
            // Ghidra: LAB_00481e78 — la fin : elle monte trente images, puis retombe.
            short fall = *(short*)(e + 0x10);

            if (fall < 0x1e)
            {
                dy = 0x190;
            }
            else if (fall < 0x64)
            {
                dy = -0x190;
            }
            else
            {
                *(uint*)e = state | 0x20000000;        // le signal que la chaine suivra
                return;
            }

            // Ce chemin saute l'heritage : il est deja dedans.
            dying = true;
        }
        else
        {
            byte* prey = *(byte**)(e + 0x78);

            if (prey != null)
            {
                if ((*(uint*)prey & 0x50000000) != 0)
                {
                    *(uint*)(prey + 0x74) = 0;
                    *(byte**)(e + 0x78) = null;
                    prey = null;
                }
            }
            else
            {
                // Ghidra: LAB_00481ecf — elle cherche l'adversaire du LANCEUR.
                prey = FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 1);
            }

            // Ghidra: LAB_00481ee1
            short age = *(short*)(e + 0x10);

            if (age < 0x1e)
            {
                dy = 0x190;                            // les trente premieres images : elle monte
            }
            else if (prey != null)
            {
                *(byte**)(e + 0x78) = prey;
                dx = (*(int*)(prey + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                *(byte**)(prey + 0x74) = e;

                int preyHead = StatusBubbleHeight_00538BC0((uint*)prey);
                dy = (((preyHead & ~1) << 0xb)
                      + (*(int*)(prey + 0x20) - *(int*)(e + 0x20))) >> 0xc;
                dz = (*(int*)(prey + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            }
            else
            {
                // Ghidra: LAB_00481f37 — l'errance, retiree une image sur 32 seulement.
                byte* caster = *(byte**)(e + 0x74);

                if ((age & 0x1f) == 0)
                {
                    *(int*)(e + 0x44) = (CrtRand() & 0xff)
                        + ((*(int*)(caster + 0x1c) - *(int*)(e + 0x1c)) >> 0xc) - 0x80;
                    *(int*)(e + 0x4c) = (CrtRand() & 0xff)
                        + ((*(int*)(caster + 0x24) - *(int*)(e + 0x24)) >> 0xc) - 0x80;
                }

                // Ghidra: LAB_00481f76
                *(int*)(e + 0x48) =
                    ((*(int*)(caster + 0x20) - *(int*)(e + 0x20)) >> 0xc) + 0x190;

                *(ushort*)(e + 0x12) += 0x80;

                dx = *(int*)(e + 0x44);
                dz = *(int*)(e + 0x4c);
                dy = (((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12)) * 400) >> 0xc)
                     + *(int*)(e + 0x48);
            }
        }

        if (!dying)
        {
            // Ghidra: LAB_00481fb3 — l'heritage de la fin, et l'age qui repart.
            *(uint*)e = state | (*(uint*)(*(byte**)(e + 0x6c)) & 0x2000000);
            if ((*(uint*)e & 0x2000000) != 0) *(short*)(e + 0x10) = 0;
        }

        // Ghidra: LAB_00481fd2
        int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int yaw = Winsys3d.Ordinal_457(dx, dz);
        int pitch = Winsys3d.Ordinal_457(dy, flat);

        int* step = stackalloc int[3];
        SetVectorFromAngles_00532AD0(step, yaw, pitch, 0x10);

        int vx = *(int*)(e + 0x50) + step[0];
        int vy = *(int*)(e + 0x54) + step[1];
        int vz = *(int*)(e + 0x58) + step[2];

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;

        // PREMIER ETAGE : l'angle range ne bouge que de 0x100 par image, et seulement si l'ecart
        // depasse 0x100. Le test est ecrit en (ecart + 0x100) & 0xfff <= 0x200.
        short wantedYaw = *(short*)(e + 0x14);
        int yawGap = (yaw - wantedYaw) & 0xfff;

        if (((yawGap + 0x100) & 0xfff) > 0x200)
        {
            *(short*)(e + 0x14) = (short)(wantedYaw + (yawGap >= 0x800 ? -0x100 : 0x100));
        }

        // Ghidra: LAB_0048207f — le meme limiteur, sur le tangage.
        short wantedPitch = *(short*)(e + 0x16);
        int pitchGap = (pitch - wantedPitch) & 0xfff;

        if (((pitchGap + 0x100) & 0xfff) > 0x200)
        {
            *(short*)(e + 0x16) = (short)(wantedPitch + (pitchGap >= 0x800 ? -0x100 : 0x100));
        }

        // SECOND ETAGE : Ghidra LAB_004820af — un quart de l'ecart, par le plus court chemin. Le
        // cap porte en plus le quart de tour de decalage du modele.
        short heading = *(short*)(e + 0x32);
        short tilt = *(short*)(e + 0x34);

        int turnGap = (*(short*)(e + 0x14) - heading + 0x400) & 0xfff;
        int riseGap = (*(short*)(e + 0x16) - tilt) & 0xfff;

        *(short*)(e + 0x32) =
            (short)(heading + (turnGap < 0x800 ? turnGap >> 2 : -((0x1000 - turnGap) >> 2)));
        *(short*)(e + 0x34) =
            (short)(tilt + (riseGap < 0x800 ? riseGap >> 2 : -((0x1000 - riseGap) >> 2)));

        // Ghidra: LAB_0048210e — la vitesse perd un quart, puis la position l'ajoute.
        vx -= vx >> 2;
        vy -= vy >> 2;
        vz -= vz >> 2;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

        if (victim != null)
        {
            // LA VARIANTE 1 : seule du depot. Elle deplace la lecture de la fiche de sort.
            ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 1);

            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;          // le clignotement de degat
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_004821a0 @ 0x004821a0 — <b>un anneau du serpent de Colere</b> : le sous-type
    /// <c>0x40</c>, creneau 64 de <see cref="s_StaticEntityHandlers_00551f30"/>. 348 octets,
    /// <b>absente de <c>Blaze.c</c></b>. Sept exemplaires, en file.
    ///
    /// <para>
    /// <b>Il retrouve son devancier par son numero d'emplacement.</b> A sa premiere image il calcule
    /// <c>0x005edee0 + (+0x16) * 0x9c</c> et range l'adresse dans <c>+0x70</c> — le meme mecanisme
    /// que la queue du Demon polaire, mais ici il forme une <b>chaine</b> : le sort a donne a chaque
    /// anneau le numero de celui qui le precede, et au premier celui de la tete.
    /// </para>
    /// <para>
    /// <b>Il ne se deplace pas, il se place.</b> Sa position vaut a chaque image celle de son
    /// devancier plus un vecteur de longueur <c>echelle X &#215; 80 &gt;&gt; 12</c>, pris sur le cap
    /// du devancier plus un quart de tour et sur son tangage <b>nie</b> — donc exactement derriere
    /// lui. Son orientation, elle, vient de son deplacement reel, rejointe d'un quart par image.
    /// </para>
    /// <para>
    /// <b>Il meurt par heritage.</b> Il prend <c>0x20000000</c> dans le mot d'etat de son devancier
    /// et s'eteint a l'image suivante : la fin descend la chaine anneau par anneau, un cran par
    /// image, ce qui fait disparaitre le serpent par la queue.
    /// </para>
    /// </summary>
    private static void TickWrathLink_004821A0(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x20000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* leader;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            leader = Mem.Entity(0x005edee0) + *(short*)(e + 0x16) * 0x9c;
            *(byte**)(e + 0x70) = leader;
        }
        else
        {
            leader = *(byte**)(e + 0x70);
        }

        // Ghidra: LAB_004821e6
        int span = (*(short*)(e + 0x28) * 5 << 4) >> 0xc;

        int keptX = *(int*)(e + 0x1c);
        int keptY = *(int*)(e + 0x20);
        int keptZ = *(int*)(e + 0x24);

        int* offset = stackalloc int[3];
        SetVectorFromAngles_00532AD0(
            offset, *(short*)(leader + 0x32) + 0x400, -*(short*)(leader + 0x34), span);

        int newX = *(int*)(leader + 0x1c) + offset[0];
        int newY = *(int*)(leader + 0x20) + offset[1];
        int newZ = *(int*)(leader + 0x24) + offset[2];

        *(int*)(e + 0x1c) = newX;
        *(int*)(e + 0x20) = newY;
        *(int*)(e + 0x24) = newZ;

        int mx = (newX - keptX) >> 0xc;
        int my = (newY - keptY) >> 0xc;
        int mz = (newZ - keptZ) >> 0xc;

        int travelled = (int)Winsys3d.Ordinal_442((uint)(mx * mx + mz * mz));

        short heading = *(short*)(e + 0x32);
        short tilt = *(short*)(e + 0x34);

        int turnGap = (Winsys3d.Ordinal_457(mx, mz) + 0x400 - heading) & 0xfff;
        int riseGap = (Winsys3d.Ordinal_457(my, travelled) - tilt) & 0xfff;

        *(short*)(e + 0x32) +=
            (short)(turnGap < 0x800 ? turnGap >> 2 : -((0x1000 - turnGap) >> 2));
        *(short*)(e + 0x34) =
            (short)(tilt + (riseGap < 0x800 ? riseGap >> 2 : -((0x1000 - riseGap) >> 2)));

        // La fin descend la chaine : il la prend chez son devancier.
        *(uint*)e |= *(uint*)(*(byte**)(e + 0x70)) & 0x20000000;
    }

    /// <summary>
    /// Ghidra: FUN_00482300 @ 0x00482300 — <b>la colonne de Colere</b> : le sous-type <c>0x41</c>,
    /// creneau 65 de <see cref="s_StaticEntityHandlers_00551f30"/>. 107 octets, <b>absente de
    /// <c>Blaze.c</c></b>. Une seule.
    ///
    /// <para>
    /// <b>Elle ne fait que grandir et tourner.</b> Son echelle rejoint <c>0x7fff</c> par huitiemes —
    /// la plus grande consigne du depot — et son cap avance de <c>0x80</c> par image, y compris
    /// pendant son extinction. Sa teinte monte de <c>0x080808</c> jusqu'a <c>0x808080</c>, puis
    /// redescend du meme pas quand le sort lui passe <c>0x2000000</c>, et c'est ce fondu qui la tue.
    /// </para>
    /// </summary>
    private static void TickWrathPillar_00482300(byte* record)
    {
        byte* e = record;

        uint state = *(uint*)e;
        uint tint = *(uint*)(e + 0x38);

        if ((state & 0x2000000) != 0)
        {
            tint += 0xfff7f7f8;                        // -0x080808
            *(uint*)(e + 0x38) = tint;

            if (tint == 0)
            {
                *(uint*)e = 0;
                return;
            }
        }
        else
        {
            // Ghidra: LAB_00482327
            if (tint < 0x808080) *(uint*)(e + 0x38) = tint + 0x00080808;

            short scale = *(short*)(e + 0x28);
            scale += (short)((0x7fff - scale) >> 3);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2c) = scale;

            *(uint*)e = state | (*(uint*)(*(byte**)(e + 0x6c)) & 0x2000000);
        }

        // Ghidra: LAB_00482362 — le cap tourne meme pendant le fondu.
        *(ushort*)(e + 0x32) += 0x80;
    }

    // =====================================================================
    // METEORES — le vingt-huitieme sort, ecole 0 indice 27, sous-type 0x1b
    //
    // Trois corps neufs, absents de Blaze.c.
    //
    //   0x00482370   LE SORT. Sous-type 0x1b de 0x00551c30, 381 octets.
    //   0x004824F0   LE METEORE, sous-type 0x65 de 0x00551c30, 954 octets. HUIT exemplaires.
    //   0x004828B0   LA TRAINE, sous-type 0x78 de 0x00551f30, 58 octets. Deux par meteore.
    //
    // LE PREMIER SORT QUI N'IMMOBILISE PAS SON LANCEUR. Aucun des 381 octets du sort n'ecrit dans le
    // mot d'etat du lanceur : il ne pose jamais 0x80. Les vingt-sept sorts precedents le faisaient
    // tous, sans exception. Il ne lit pas non plus la manette et ne ponctionne aucun PM — il dure
    // 0x50 images et s'arrete.
    //
    // ET LE PREMIER QUI CHERCHE UN SOL AVANT D'ENGENDRER. Une image sur huit, pendant les soixante
    // premieres, il se DEPLACE lui-meme a un point tire au hasard dans un carre de 256 unites autour
    // d'une ancre posee devant le lanceur, se recopie les six mots de contexte de collision du
    // lanceur (+0x7c), appelle FinalizeEntityPlacement2 TROIS FOIS de suite en accumulant le mot de
    // contact — et n'engendre le meteore que si 0x8800 est reste absent. Aucun autre corps transcrit
    // ne sonde le decor pour decider d'une naissance, ni n'appelle le placement plusieurs fois pour
    // le faire converger.
    //
    // ET C'EST L'ARBRE LE PLUS PROFOND DU DEPOT : le sort engendre le meteore, qui engendre a son
    // tour quatre especes. Il tombe en trainant deux 0x78 ; a l'impact il lache trois anneaux 0x6c
    // de tailles differentes et quatre eclats 0xbf ; puis, dix images durant, une eclaboussure 0xbe
    // par image et un coup porte.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00482370 @ 0x00482370 — <b>Meteores</b> : le sous-type <c>0x1b</c>, creneau 27 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/>. 381 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne touche personne et n'immobilise personne.</b> Ni sonde, ni appel de resolution, ni
    /// <c>0x80</c> sur le lanceur : il n'est qu'un semoir. Le lanceur reste libre de ses mouvements
    /// pendant les 0x50 images que dure la pluie — <b>aucun autre sort du depot ne fait cela</b>.
    /// </para>
    /// <para>
    /// <b>L'ancre est posee a la premiere image</b>, a 256 unites devant le lanceur sur son cap et a
    /// <c>0x32</c> unites au-dessus de son sol ; le sort s'y donne aussi une vitesse et une gravite
    /// dont il ne se sert pas — ce sont les champs que ses meteores heritent par recopie.
    /// </para>
    /// <para>
    /// <b>Une image sur huit</b> (<c>age &amp; 7</c>), et seulement pendant les soixante premieres,
    /// il se transporte a un point tire dans un carre de 256 unites autour de l'ancre, se recopie le
    /// contexte de collision du lanceur, laisse <see cref="FinalizeEntityPlacement2"/> converger en
    /// <b>trois passes</b>, et n'engendre le meteore que si le decor n'a rien signale sur le masque
    /// <c>0x8800</c>. Le bruit, lui, part dans tous les cas.
    /// </para>
    /// </summary>
    private static void TickMeteorStorm_00482370(byte* record)
    {
        byte* e = record;
        byte* caster = *(byte**)(e + 0x74);

        if (*(short*)(e + 0x10) == 0)
        {
            int heading = *(short*)(e + 0x32);

            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(heading) << 5;
            *(int*)(e + 0x54) = -0x14000;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(heading) << 5;

            *(ushort*)(e + 0x32) += 0x400;
            *(ushort*)(e + 0x9a) = 0x7fff;

            // Le quart de tour vient d'etre ajoute et se retire aussitot : l'ancre est bien sur le
            // cap d'origine. Le champ +0x32, lui, garde le decalage.
            int yaw = *(short*)(e + 0x32) - 0x400;
            *(ushort*)(e + 0x34) = 0xfd00;

            *(int*)(e + 0x44) = *(int*)(caster + 0x1c)
                                - ((int)Winsys3d.GetSinTableEntry(yaw) << 8);
            *(int*)(e + 0x48) = *(int*)(caster + 0x60) + 0x320000;
            *(int*)(e + 0x4c) = *(int*)(caster + 0x24)
                                - (Winsys3d.GetCosTableEntry(yaw) << 8);
        }

        // Ghidra: LAB_004823f4 — un meteore toutes les huit images, soixante images durant.
        short age = *(short*)(e + 0x10);

        if ((age & 7) == 0 && age < 0x3c)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x48);
            *(int*)(e + 0x1c) = (((CrtRand() & 0x1ff) - 0x100) << 0xc) + *(int*)(e + 0x44);
            *(int*)(e + 0x24) = (((CrtRand() & 0x1ff) - 0x100) << 0xc) + *(int*)(e + 0x4c);

            // Les six mots de contexte de collision du lanceur, recopies tels quels : sans eux le
            // placement ne saurait pas dans quelle piece il tombe.
            for (int word = 0; word < 6; word++)
            {
                *(int*)(e + 0x7c + word * 4) = *(int*)(caster + 0x7c + word * 4);
            }

            *(uint*)(e + 0x90) = 0;

            uint contact = 0;

            for (int pass = 3; pass != 0; pass--)
            {
                FinalizeEntityPlacement2((uint*)e);
                contact |= *(uint*)(e + 0x90);
            }

            // Le decor a repondu : pas de meteore ici. Le bruit part quand meme.
            if ((contact & 0x8800) == 0) SpawnEffectEntity(e, 0x65, 0, -1);

            Aud.PlaySoundEffect(0x20, 0x400, 0x1200, 0x1200);
        }

        // Ghidra: LAB_004824b0 — le ciel vire au rouge sombre, reecrit a chaque image.
        *(uint*)Mem.Arena(0x005e9160) = 0x00400080;
        *(uint*)Mem.Arena(0x005e9164) = 0x00020040;

        if (*(short*)(e + 0x10) >= 0x50 || (*(uint*)(*(byte**)(e + 0x74)) & 0x40000000) != 0)
        {
            *(uint*)e = 0;
            *(ushort*)Mem.Arena(0x005e9166) = 0;       // le decor reprend sa couleur
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_004824f0 @ 0x004824f0 — <b>un meteore</b> : le sous-type <c>0x65</c>, creneau 101
    /// de <see cref="s_StaticEntityHandlers_00551c30"/>. 954 octets, <b>absente de
    /// <c>Blaze.c</c></b>. Huit exemplaires, un toutes les huit images.
    ///
    /// <para>
    /// <b>Il est engendre par <see cref="SpawnEffectEntity"/></b>, pas par
    /// <see cref="SpawnCastEntity_0047AAE0"/> : il naît donc dans la table <c>0x005ff400</c>, et son
    /// sous-type est resolu par <see cref="s_StaticEntityHandlers_00551c30"/>, <b>la table des sorts
    /// eux-memes</b>. Tous les enfants de sort transcrits jusqu'ici vivaient dans une des deux autres
    /// tables (0x00551f30 par SpawnCastEntity, 0x00552230 par SpawnStatusEffectEntity) ; celui-ci est
    /// le premier a partager la table de son pere.
    /// </para>
    /// <para>
    /// <b>Sa chute est une derive, pas une trajectoire.</b> A chaque image il <i>ajoute</i>
    /// <c>sin/cos &lt;&lt; 3</c> de son cap a sa vitesse, laisse
    /// <see cref="IntegrateWithDrag_00535880"/> l'amortir, puis <see cref="FinalizeEntityPlacement2"/>
    /// le poser ; s'il est passe sous son sol il y est remonte de force et se signale
    /// <c>0x800</c> a lui-meme dans <c>+0x90</c>. Deux traines <c>0x78</c> le suivent depuis sa
    /// naissance.
    /// </para>
    /// <para>
    /// <b>L'impact tient en un bloc</b> : trois anneaux <c>0x6c</c> de tailles et de vitesses
    /// differentes, quatre eclats <c>0xbf</c> avec l'atlas de Stalagtite, un bruit, et
    /// <c>DAT_005de0ac</c> pose a 8 — la secousse d'ecran, la meme que Boule de feu.
    /// </para>
    /// <para>
    /// <b>Puis dix images de resolution</b>, une frappe par image : une eclaboussure <c>0xbe</c>
    /// jetee a l'ecart, une sonde large (<c>0x12c</c> de profondeur, <c>0x200</c> d'ouverture), une
    /// poussee au cap disperse, et la secousse rongee de trois quarts <b>en changeant de signe</b>
    /// a chaque image — l'oscillation amortie de <see cref="EntityHandler_00476730"/>, ecrite ici
    /// une deuxieme fois.
    /// </para>
    /// </summary>
    private static void TickMeteor_004824F0(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = unchecked((short)0xff00);
        probe[2] = 0x100;
        probe[3] = 0x12c;
        probe[4] = 0;
        probe[5] = 0x200;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(uint*)e |= 0x80;
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x70) = null;

            *(uint*)(e + 0x38) = 0x008080ff;
            *(uint*)(e + 0x28) = 0x18001800;
            *(ushort*)(e + 0x2c) = 0x1800;
            *(ushort*)(e + 0x9a) = 0x7fff;

            e[0x04] = 0x1c;
            SpawnCastEntity_0047AAE0(e, 0x78, 0, 8);

            e[0x04] = 0x39;
            SpawnCastEntity_0047AAE0(e, 0x78, 0, 2);
        }

        // Ghidra: LAB_0048257e
        uint state = *(uint*)e;

        if ((state & 0x2000000) != 0)
        {
            // ============================================ la resolution, dix images
            short beat = *(short*)(e + 0x10);

            if (beat >= 0x0a || (state & 0x40000000) != 0)
            {
                *(uint*)e = 0;
                return;
            }

            *(short*)(e + 0x10) = (short)(beat + 1);
            *(short*)(e + 0x1a) = 7;
            *(uint*)(e + 0x38) = 0x01606060;

            // Le cap est disperse sur 512 unites, et recule de 0x500 : la gerbe s'ouvre en avant.
            int spread = (CrtRand() & 0x1ff) + *(short*)(e + 0x32) - 0x500;

            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(spread) << 6;
            *(int*)(e + 0x54) = 0x40000;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(spread) << 6;

            *(short*)(e + 0x12) = 3;
            *(uint*)(e + 0x28) = 0x20002000;
            *(short*)(e + 0x14) = 0x5000;
            *(short*)(e + 0x16) = 2;

            int* splash = stackalloc int[3];
            splash[0] = (((CrtRand() & 0x1ff) - 0x100) << 0xc) + *(int*)(e + 0x1c);
            splash[1] = *(int*)(e + 0x20);
            splash[2] = (((CrtRand() & 0x1ff) - 0x100) << 0xc) + *(int*)(e + 0x24);

            SpawnStatusEffectEntity_0047AC40(e, splash, Descriptors00551A08(), 0xbe, 0, 2);

            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

            if (victim != null)
            {
                ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);

                *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(spread) << 5;
                *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(spread) << 5;
                *(uint*)victim |= 0x40;
            }

            // Ghidra: LAB_004826bc — la secousse s'amortit de trois quarts, signe compris.
            int* shake = Mem.Camera(0x005de0ac);
            if (*shake == 0) return;
            *shake = (*shake >> 2) - *shake;
            return;
        }

        // ================================================ Ghidra: LAB_004826e8, la chute
        int yaw = *(short*)(e + 0x32) - 0x400;

        *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(yaw) << 3;
        *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(yaw) << 3;

        IntegrateWithDrag_00535880(e);
        FinalizeEntityPlacement2((uint*)e);

        // Passe sous son sol : il y est remonte, et il se signale le contact lui-meme.
        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60))
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)(e + 0x90) |= 0x800;
        }

        // Ghidra: LAB_00482742
        if ((*(uint*)(e + 0x90) & 0x8802) != 0 || (*(uint*)e & 0x20000000) != 0)
        {
            // ============================================ Ghidra: LAB_0048275a, l'impact
            *(short*)(e + 0x34) = 0;
            *(ushort*)(e + 0x2e) = 0xffc0;

            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;

            *(int*)(e + 0x50) = 0x4000;
            *(int*)(e + 0x54) = 0x2000;
            *(int*)(e + 0x58) = 0x4000;
            *(uint*)(e + 0x38) = 0x00808080;
            *(uint*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;

            // Les trois anneaux d'Onde de choc, branche « sans ancre » : +0x44/+0x48/+0x4c a zero.
            e[0x04] = 7;
            SpawnCastEntity_0047AAE0(e, 0x6c, 0, 3);

            *(int*)(e + 0x50) = 0x2000;
            *(int*)(e + 0x54) = 0x2000;
            *(int*)(e + 0x58) = 0x2000;
            e[0x04] = 1;
            SpawnCastEntity_0047AAE0(e, 0x6c, 1, 0x0e);

            *(int*)(e + 0x50) = 0x2000;
            *(int*)(e + 0x54) = 0x1000;
            *(int*)(e + 0x58) = 0x2000;
            *(uint*)(e + 0x38) = 0x00808080;
            *(uint*)(e + 0x28) = 0x50007000;
            *(ushort*)(e + 0x2c) = 0x7000;
            e[0x04] = 0x26;
            SpawnCastEntity_0047AAE0(e, 0x6c, 1, 6);

            for (int shard = 4; shard != 0; shard--)
            {
                int spin = CrtRand();

                *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(spin) << 5;
                *(int*)(e + 0x54) = ((CrtRand() & 0x1f) + 0x32) << 0xc;
                *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(spin) << 5;

                *(uint*)(e + 0x38) = 0x01f0f0f0;
                *(uint*)(e + 0x28) = 0x02802800;
                *(ushort*)(e + 0x2c) = 0x1f;
                *(short*)(e + 0x1a) = 1;

                byte* atlas = Descriptors00551AF8() + CrtRand() % 3 * 8;
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), atlas, 0xbf, 0, 2);
            }

            *(uint*)e |= 0x2000000;

            Aud.PlayPositionalSound(0x2e, 0x400, 0x1200, (int*)(e + 0x1c));

            *Mem.Camera(0x005de0ac) = 8;               // la secousse d'ecran
        }

        // Ghidra: LAB_00482892
        if ((*(uint*)e & 0x40000000) != 0) *(uint*)e |= 0x2000000;
    }

    /// <summary>
    /// Ghidra: FUN_004828b0 @ 0x004828b0 — <b>une traine de meteore</b> : le sous-type <c>0x78</c>,
    /// creneau 120 de <see cref="s_StaticEntityHandlers_00551f30"/>. 58 octets, <b>absente de
    /// <c>Blaze.c</c></b>. Deux par meteore, de modeles differents.
    ///
    /// <para>
    /// <b>Elle recopie la position de son pere, sans decalage aucun.</b> Ni vitesse, ni age, ni
    /// horloge : elle est exactement la ou le meteore est, et ne differe de lui que par le modele et
    /// l'echelle que la naissance lui a laisses. Elle prend <c>0x2000000</c> chez lui et s'eteint
    /// dans la meme image — pas a la suivante, contrairement a l'aiguille de Stalagtite.
    /// </para>
    /// </summary>
    private static void TickMeteorTrail_004828B0(byte* record)
    {
        byte* e = record;
        byte* meteor = *(byte**)(e + 0x6c);

        uint state = *(uint*)e | (*(uint*)meteor & 0x2000000);
        *(uint*)e = state;

        *(int*)(e + 0x1c) = *(int*)(meteor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(meteor + 0x20);
        *(int*)(e + 0x24) = *(int*)(meteor + 0x24);

        if ((state & 0x2000000) != 0) *(uint*)e = 0;
    }

    // =====================================================================
    // FUSION — le vingt-neuvieme sort, ecole 0 indice 28, sous-type 0x1c
    //
    // LE DERNIER DE L'ECOLE 0. Deux corps neufs seulement, et deux enfants deja ecrits.
    //
    //   0x004828F0   LE SORT. Sous-type 0x1c de 0x00551c30, 1260 octets.
    //   0x00482DE0   LA BILLE, sous-type 0x79 de 0x00551f30, 234 octets. NEUF exemplaires.
    //                DECOMPILEE dans Blaze.c.
    //   0x00482ED0   L'ETINCELLE, sous-type 0x7a de 0x00551f30, 25 octets — LE PLUS COURT CORPS
    //                DE SORT DU DEPOT, devant les 30 du halo d'Exorciste.
    //
    // Ses deux autres enfants existaient deja : la trainee 0x50 est celle de Boule de feu
    // (TickFireOrbTrail_00482EF0) et la bourrasque 0x83 celle de Ventilo
    // (TickGaleGust_0047FE40). Le sort le plus cher de l'ecole 0 — 100 PM — est donc aussi celui
    // dont la moitie des enfants etait deja ecrite.
    //
    // ET SON COMPTEUR DE VITESSE EST SON COMPTEUR DE MISE A FEU. Le champ +0x16 est a la fois
    // l'avance en unites par image et le decompte avant la detonation : parti a 0x19 il perd un
    // par image, et le corps saute tout le bloc central tant qu'il n'est pas nul. La boule roule
    // donc en ralentissant sur vingt-cinq images, et c'est son immobilite qui l'allume. Un mur la
    // met a zero d'un coup (+0x90 sur 0x8802) : elle explose ou elle s'arrete.
    //
    // IL ASPIRE — OU IL BRASSE — TOUT CE QUI L'ENTOURE. A chaque image il balaie soit les 0x20
    // entites de scene (0x005ecaa0) si le lanceur n'est pas du groupe, soit les membres du groupe
    // (0x005e93c0) sinon, et ajoute a la vitesse de chaque corps a moins de 0x320 un vecteur pris
    // au cap qui va de la boule vers lui, MOINS 0x600. La correction usuelle etant 0x400, c'est le
    // rayon sortant tourne d'un huitieme de tour : un tourbillon, pas une poussee droite. Aucun
    // autre corps de sort transcrit ne balaie un camp entier pour en modifier les vitesses.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004828f0 @ 0x004828f0 — <b>Fusion</b> : le sous-type <c>0x1c</c>, creneau 28 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et <b>le dernier de l'ecole 0</b>. 1260 octets,
    /// <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Une boule qui roule, puis qui s'allume.</b> A la premiere image elle pose une bille
    /// <c>0x79</c> centrale et huit autres en couronne, chacune a son propre rayon, son propre cap
    /// et son propre tangage tires au hasard ; puis elle avance a <c>+0x16</c> unites par image, ce
    /// meme champ perdant un par image depuis <c>0x19</c>. Tant qu'il n'est pas nul le corps saute
    /// tout ce qui suit : <b>la detonation commence quand la boule s'arrete</b>, ou quand un mur
    /// l'arrete.
    /// </para>
    /// <para>
    /// <b>Ensuite trois cadences se superposent.</b> Une trainee <c>0x50</c> par image — celle de
    /// Boule de feu ; une etincelle <c>0x7a</c> une image sur seize pendant les 180 premieres, avec
    /// l'ecran qui vire au <b>blanc</b> ; une bourrasque <c>0x83</c> — celle de Ventilo — une image
    /// sur quatre pendant les 200 premieres, posee au sol a 256 unites au hasard autour d'elle. Et
    /// une paire de sons toutes les quatorze images.
    /// </para>
    /// <para>
    /// <b>Il ne meurt pas de lui-meme.</b> Passe <c>0xc8</c> images il se pose <c>0x2000000</c> et
    /// remet son age a zero, mais il attend ensuite le <c>0x1000000</c> que sa bille centrale lui
    /// pose en achevant son fondu. C'est le meme signal remontant qu'Assaut magique, et il est ici
    /// la <b>seule</b> condition de fin.
    /// </para>
    /// </summary>
    private static void TickFusion_004828F0(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = unchecked((short)0xff00);
        probe[2] = 0x100;
        probe[3] = 0x12c;
        probe[4] = 0;
        probe[5] = 0x200;

        uint state = *(uint*)e;
        byte* caster = *(byte**)(e + 0x74);

        if ((state & 0x2000000) != 0)
        {
            // Il attend le signal de sa bille centrale avant de rendre le lanceur.
            if ((state & 0x1000000) != 0)
            {
                *(uint*)e = 0;
                *(uint*)caster &= 0xffffff7f;
                return;
            }
        }
        else
        {
            // Ghidra: LAB_00482951
            if (*(short*)(e + 0x10) == 0)
            {
                *(uint*)caster |= 0x80;                // le lanceur est immobilise

                *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & 0xfffe) | 6);
                *(uint*)e = state | 0x80;
                *(ushort*)(e + 0x2e) = 0xffc0;

                int height = StatusBubbleHeight_00538BC0((uint*)caster);

                *(uint*)(e + 0x38) = 0;
                *(int*)(e + 0x20) += (height & ~1) << 0xb;
                *(uint*)(e + 0x28) = 0;
                *(ushort*)(e + 0x2c) = 0;

                // La bille centrale : rayon nul, elle reste sur la boule.
                *(ushort*)(e + 0x12) = 0x1800;
                *(ushort*)(e + 0x14) = 0;
                *(ushort*)(e + 0x16) = 0;
                e[0x04] = 2;
                SpawnCastEntity_0047AAE0(e, 0x79, 0, 7);

                // Les huit autres : meme echelle visee, mais un rayon et une orientation propres,
                // et un huitieme de tour d'ecart d'une a l'autre.
                *(ushort*)(e + 0x12) = 0x2000;
                *(ushort*)(e + 0x14) = 0;
                e[0x04] = 0x30;

                int spoke = 0;

                for (int orb = 8; orb != 0; orb--)
                {
                    *(ushort*)(e + 0x16) = (ushort)((CrtRand() & 0x1f) + 0x96);
                    *(ushort*)(e + 0x32) = (ushort)((CrtRand() & 0x3f) + spoke - 0x20);
                    *(ushort*)(e + 0x34) = (ushort)((CrtRand() & 0x1ff) - 0x100);

                    SpawnCastEntity_0047AAE0(e, 0x79, 0, 1);

                    spoke += 0x200;
                }

                // +0x16 redevient ce qu'il est vraiment : l'avance de la boule.
                *(ushort*)(e + 0x16) = 0x19;
                *(ushort*)(e + 0x32) = *(ushort*)(caster + 0x32);
            }

            // Ghidra: LAB_00482a32 — une trainee par image, empruntee a Boule de feu.
            int keptYaw = *(short*)(e + 0x32);
            int keptTilt = *(short*)(e + 0x34);

            *(uint*)(e + 0x28) = 0x30003000;
            *(ushort*)(e + 0x12) = (ushort)((CrtRand() & 0x7f) + 0x320);
            *(uint*)(e + 0x38) = 0;
            *(short*)(e + 0x1a) = 3;
            *(ushort*)(e + 0x32) = (ushort)CrtRand();
            *(ushort*)(e + 0x34) = (ushort)((CrtRand() & 0x1ff) - 0x100);

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519A0(), 0x50, 0, 7);

            ushort speed = *(ushort*)(e + 0x16);

            *(ushort*)(e + 0x32) = (ushort)keptYaw;
            *(ushort*)(e + 0x34) = (ushort)keptTilt;

            if (speed != 0)
            {
                // Elle roule encore : rien d'autre ne se declenche.
                *(ushort*)(e + 0x16) = (ushort)(speed - 1);
            }
            else
            {
                // Ghidra: LAB_00482aae — une etincelle une image sur seize, 180 images durant.
                if (*(short*)(e + 0x10) < 0xb4 && (CrtRand() & 0x0f) == 0)
                {
                    int sparkYaw = *(short*)(e + 0x32);
                    int sparkTilt = *(short*)(e + 0x34);

                    *(uint*)(e + 0x28) = 0x60006000;
                    *(ushort*)(e + 0x2c) = 0x6000;
                    *(uint*)(e + 0x38) = 0x00f0f0f0;
                    *(ushort*)(e + 0x32) = (ushort)CrtRand();
                    *(ushort*)(e + 0x34) = (ushort)CrtRand();

                    e[0x04] = 0x1d;
                    SpawnCastEntity_0047AAE0(e, 0x7a, 0, 7);

                    *(ushort*)(e + 0x32) = (ushort)sparkYaw;
                    *(ushort*)(e + 0x34) = (ushort)sparkTilt;

                    Aud.PlayPositionalSound(0x0f, 0x800, 0x1200, (int*)(e + 0x1c));

                    // L'ecran vire au BLANC — le binaire ecrit R et V en un dword, B a part.
                    Mem.s_BackColourR_005e93a0 = 0xff;
                    Mem.s_BackColourG_005e93a2 = 0xff;
                    Mem.s_BackColourB_005e93a4 = 0xff;
                }

                // Ghidra: LAB_00482b30 — une bourrasque une image sur quatre, 200 images durant.
                short age = *(short*)(e + 0x10);

                if (age < 0xc8 && (age & 3) == 0)
                {
                    int keptX = *(int*)(e + 0x1c);
                    int keptY = *(int*)(e + 0x20);
                    int keptZ = *(int*)(e + 0x24);
                    int gustYaw = *(short*)(e + 0x32);
                    int gustTilt = *(short*)(e + 0x34);

                    int scatter = CrtRand();
                    int reach = (CrtRand() & 0xff) + 0x100;

                    *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(scatter) * reach;
                    *(int*)(e + 0x20) = *(int*)(e + 0x60);         // au sol
                    *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(scatter) * reach;

                    *(uint*)(e + 0x38) = 0x00f0f0f0;
                    *(uint*)(e + 0x28) = 0x50005000;
                    *(ushort*)(e + 0x2c) = 0x5000;
                    *(ushort*)(e + 0x32) = (ushort)CrtRand();
                    *(ushort*)(e + 0x34) = 0x400;
                    e[0x04] = (byte)((CrtRand() & 3) + 8);

                    SpawnCastEntity_0047AAE0(e, 0x83, 0, 7);

                    *(ushort*)(e + 0x32) = (ushort)gustYaw;
                    *(ushort*)(e + 0x34) = (ushort)gustTilt;
                    *(int*)(e + 0x1c) = keptX;
                    *(int*)(e + 0x20) = keptY;
                    *(int*)(e + 0x24) = keptZ;
                }

                // Ghidra: LAB_00482c15 — elle ne frappe qu'une fois arretee.
                byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

                if (victim != null)
                {
                    ResolveSpellHit_0053A2B0(*(uint**)(e + 0x74), (uint*)victim, e[7], e[6], 0);
                    *(uint*)victim |= 0x40;
                    *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                }
            }

            // Ghidra: LAB_00482c53 — +0x16 est l'avance ; la hauteur est rendue apres le placement.
            int heading = *(short*)(e + 0x32);
            int advance = *(short*)(e + 0x16);

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * advance;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * advance;

            int keptHeight = *(int*)(e + 0x20);
            FinalizeEntityPlacement2((uint*)e);
            *(int*)(e + 0x20) = keptHeight;

            // Un mur ne la tue pas : il l'arrete, et l'arret est ce qui l'allume.
            if ((*(uint*)(e + 0x90) & 0x8802) != 0) *(ushort*)(e + 0x16) = 0;

            // Ghidra: LAB_00482c9f — LE TOURBILLON, sur tout le camp d'en face.
            uint remaining;
            byte* body;

            if ((*(uint*)e & 0x800000) != 0)
            {
                remaining = 0x20;
                body = Mem.Entity(0x005ecaa0);         // les entites de scene
            }
            else
            {
                remaining = (uint)s_PartySize_005e9020;
                body = Inp.s_SlotFlags_005e93c0;       // le groupe
            }

            for (; remaining != 0; remaining--, body += 0x9c)
            {
                if ((*(uint*)body & 0x80000000) == 0) continue;

                int dx = (*(int*)(body + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dz = (*(int*)(body + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) >= 0x320) continue;

                // La correction usuelle est 0x400 : 0x600 met le rayon sortant de biais.
                int swirl = Winsys3d.Ordinal_457(dx, dz) - 0x600;

                *(int*)(body + 0x50) += (int)Winsys3d.GetSinTableEntry(swirl) << 2;
                *(int*)(body + 0x58) += Winsys3d.GetCosTableEntry(swirl) << 2;
            }

            // Ghidra: LAB_00482d3f — la secousse est RETIREE a chaque image, pas amortie.
            int shake = (CrtRand() & 0x0f) - 8;

            *(uint*)Mem.Arena(0x005e9160) = 0x00500040;
            *(uint*)Mem.Arena(0x005e9164) = 0x00020060;
            *Mem.Camera(0x005de0ac) = shake;

            if (*(short*)(e + 0x10) >= 0xc8
                || (*(uint*)e & 0x40000000) != 0
                || (*(uint*)(*(byte**)(e + 0x74)) & 0x40000000) != 0)
            {
                *(uint*)e |= 0x2000000;
                *(short*)(e + 0x10) = 0;               // l'age repart pour l'extinction
                *(ushort*)Mem.Arena(0x005e9166) = 0;   // le decor reprend sa couleur
            }
        }

        // Ghidra: LAB_00482d93 — deux sons toutes les quatorze images, y compris pendant la fin.
        if (*(short*)(e + 0x10) % 14 == 0)
        {
            Aud.PlayPositionalSound(0x23, 0x400, 0x1200, (int*)(e + 0x1c));
            Aud.PlayPositionalSound(0x2d, 0x200, 0x1200, (int*)(e + 0x1c));
        }

        *(ushort*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00482de0 @ 0x00482de0 — <b>une bille de Fusion</b> : le sous-type <c>0x79</c>,
    /// creneau 121 de <see cref="s_StaticEntityHandlers_00551f30"/>. 234 octets, <b>decompilee dans
    /// <c>Blaze.c</c></b>. Neuf exemplaires : une au centre, huit en couronne.
    ///
    /// <para>
    /// <b>Elle est en coordonnees polaires, comme la bille d'Enchant</b> — sa position n'est jamais
    /// integree : elle vaut a chaque image celle du sort plus un vecteur de rayon <c>+0x14</c> sur
    /// son propre cap, et son cap recule de <c>0x80</c> par image. Son rayon et son echelle
    /// rejoignent leurs consignes <c>+0x16</c> et <c>+0x12</c> par huitiemes. La centrale a un rayon
    /// vise nul : elle reste sur la boule.
    /// </para>
    /// <para>
    /// <b>Et c'est elle qui autorise la mort du sort.</b> Sur <c>0x2000000</c> herite, son rayon et
    /// son echelle se resorbent et sa teinte perd <c>0x080808</c> par image ; a l'instant ou celle-ci
    /// atteint zero elle s'eteint <b>et pose <c>0x1000000</c> sur son pere</b>. Les neuf le font,
    /// mais le mot est idempotent : c'est la premiere arrivee qui compte.
    /// </para>
    /// </summary>
    private static void TickFusionOrb_00482DE0(byte* record)
    {
        byte* e = record;

        *(ushort*)(e + 0x32) += 0xff80;                // -0x80 de cap par image
        byte* orb = *(byte**)(e + 0x6c);

        if ((*(uint*)e & 0x2000000) == 0)
        {
            *(uint*)e |= *(uint*)orb & 0x2000000;

            short scale = *(short*)(e + 0x28);
            *(short*)(e + 0x28) = (short)(((*(short*)(e + 0x12) - scale) >> 3) + scale);

            short radius = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = (short)(((*(short*)(e + 0x16) - radius) >> 3) + radius);

            if (*(uint*)(e + 0x38) < 0xf0f0f0) *(uint*)(e + 0x38) += 0x00080808;
        }
        else
        {
            short scale = *(short*)(e + 0x28);
            *(short*)(e + 0x28) = (short)(scale - (scale >> 3));

            short radius = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = (short)(radius - (radius >> 3));

            uint tint = *(uint*)(e + 0x38) - 0x00080808;
            *(uint*)(e + 0x38) = tint;

            if (tint == 0)
            {
                *(uint*)e = 0;
                *(uint*)orb |= 0x1000000;              // le feu vert que le sort attend
            }
        }

        // Ghidra: LAB_00482e83 — la position, jamais integree.
        int yaw = *(short*)(e + 0x32) - 0x400;
        short span = *(short*)(e + 0x14);
        short size = *(short*)(e + 0x28);

        *(short*)(e + 0x2a) = size;
        *(short*)(e + 0x2c) = size;

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * span + *(int*)(orb + 0x1c);
        *(int*)(e + 0x20) = *(int*)(orb + 0x20);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * span + *(int*)(orb + 0x24);
    }

    /// <summary>
    /// Ghidra: FUN_00482ed0 @ 0x00482ed0 — <b>une etincelle de Fusion</b> : le sous-type
    /// <c>0x7a</c>, creneau 122 de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>25 octets — le
    /// plus court corps de sort du depot</b>, devant les 30 du halo d'Exorciste.
    ///
    /// <para>
    /// Elle perd <c>0x101010</c> de teinte par image et s'eteint a zero. Rien d'autre : ni
    /// deplacement, ni sonde, ni horloge. <b>C'est la bourrasque de Ventilo moins une
    /// instruction</b> — <see cref="TickGaleGust_0047FE40"/> fait exactement cela et pose en plus le
    /// bit <c>4</c> de <c>+0x36</c>, ce qui explique ses quatre octets de plus. Le compilateur a
    /// emis deux fois la meme idee a deux endroits, sans la partager.
    /// </para>
    /// </summary>
    private static void TickFusionSpark_00482ED0(byte* record)
    {
        byte* e = record;

        uint tint;
        unchecked { tint = *(uint*)(e + 0x38) + 0xffefeff0; }   // -0x101010
        *(uint*)(e + 0x38) = tint;

        if (tint == 0) *(uint*)e = 0;
    }
}
