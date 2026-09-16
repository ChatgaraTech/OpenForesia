// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MARQUES DE COUP ET DE RESISTANCE — la famille 0x00475xxx.
//
// Le chiffre de degats qui monte, l'eclair de resistance, le rebond sur une immunite : tout ce
// qui apparait a l'instant d'un coup et disparait seul. Aucun de ces corps ne DECIDE de quoi que
// ce soit - la decision est dans `Combat/HitResolution.cs`, ils n'en montrent que le resultat.
//
// TRENTE PLANCHES DANS CE SEUL FICHIER, dont les cinq scripts de son retarde 0x00552530 a
// 0x00552578. Meme regle que partout : une planche se releve ENTIERE, jamais au plus juste sur
// l'enregistrement qu'un corps atteint - deux gestionnaires se partagent souvent un bloc, chacun
// a son decalage. Lancer `sh OpenForesia.Tools/check-descriptors.sh` apres toute retouche.
//
// Voir Documentation/Reference/DataSymbols.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: <c>0x00475910</c> — <b>l'entree 63 (<c>0x3f</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 270 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00475910.asm</c>. C'est
    /// l'enfant que <see cref="EntityHandler_004758C0"/> depose, et il etait laisse ouvert depuis
    /// la suite 10 parce qu'il appelle la chaine d'application des degats.
    ///
    /// <para>
    /// <b>Trente-trois images de brouillard, puis un effet d'etat.</b> Chaque image il se replace
    /// au hasard autour de son porteur — <c>+0x78</c>, que le pere a recopie de <c>+0x6c</c> — et
    /// tire son image d'atlas parmi trois. Le tirage de position est <b>entierement positif</b> :
    /// <c>rand() &amp; 0xfff</c> multiplie par 50 en <c>X</c> et <c>Z</c>, par <b>150</b> en
    /// <c>Y</c>. Le nuage ne se centre donc pas sur le porteur, il monte et derive toujours dans
    /// le meme quart d'espace. C'est ecrit ainsi dans le binaire.
    /// </para>
    /// <para>
    /// A la 33e image il emprunte la meme entite d'assaillant que le piege <c>0xb5</c> —
    /// <c>0x005ede20</c>, l'emplacement qui suit la table de scene — et appelle
    /// <see cref="ApplyStatusEffect_0053B3C0"/>. Le commentaire de
    /// <see cref="EntityHandler_004755F0"/> explique pourquoi cette adresse est le mecanisme et non
    /// un debordement.
    /// </para>
    /// <para>
    /// <b>Le son part une image sur six</b> (<c>compteur % 6 == 1</c>), pas une sur deux : c'est le
    /// seul de la famille a compter ainsi.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475910(byte* e)
    {
        if (*(short*)(e + 0x10) % 6 == 1)
        {
            Aud.PlayPositionalSound(0x14, 0x800, 0x1200, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
        }

        // Ghidra: LAB_00475942 — le porteur est en +0x78, pas en +0x6c : le pere l'y a recopie.
        byte* carrier = *(byte**)(e + 0x78);

        e[0x40] = (byte)(CrtRand() % 3);

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c) + (CrtRand() & 0xfff) * 50;
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + (CrtRand() & 0xfff) * 150;
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24) + (CrtRand() & 0xfff) * 50;

        if (*(short*)(e + 0x10) >= 0x20)
        {
            // L'entite d'emprunt, une place au-dela de la table de scene. Voir
            // EntityHandler_004755F0 pour le detail de l'adjacence.
            byte* proxy = Mem.Entity(0x005ede20);

            *(uint*)e = 0x80400000;
            Mem.CopyDwords((uint*)e, (uint*)proxy, 0x27);      // Ghidra: FUN_005352e0

            proxy[8] = 0x20;
            ResolveEntityBlocks(proxy, out byte* stats, out byte* species, out byte* model);
            ResetEntityStats(stats, species, model);           // Ghidra: FUN_005389e0

            *(short*)(proxy + 6) = *(short*)(e + 6);

            ApplyStatusEffect_0053B3C0((uint*)proxy, *(uint**)(e + 0x78), 0);

            *(uint*)e = 0;
        }

        // Ghidra: LAB_00475a1e — le compteur monte AUSSI sur l'image ou l'entite s'eteint.
        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00475d40</c> — <b>l'entree 191 (<c>0xbf</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 347 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00475D40.asm</c>.
    ///
    /// <para>
    /// <b>C'est le retour visuel du coup qui ne passe pas</b> : la victime immunisee ou qui resiste
    /// au jet de sauvegarde. Toutes les variantes de la chaine des degats le font naitre — voir
    /// <see cref="ApplyStatusEffect_0053B3C0"/> et <see cref="ApplyTrapEffect_0053C420"/>. Il
    /// vit <b>une seule image</b> : tout est fait en une passe, puis <c>*e = 0</c>.
    /// </para>
    /// <list type="number">
    /// <item>une bouffee <c>0xbe</c> immobile, echelle <c>0x2000</c>, atlas
    /// <see cref="s_Descriptors_00551BA0"/> borne a <b>un</b> enregistrement (<c>+0x1a = 1</c>) ;</item>
    /// <item><b>quatre</b> autres <c>0xbe</c>, echelle <c>0x1000</c>, atlas
    /// <see cref="s_Descriptors_005519B8"/> a trois enregistrements, chacune lancee dans une
    /// direction tiree <b>sur une sphere</b> — <c>sin(azimut)·cos(elevation)</c>,
    /// <c>sin(elevation)</c>, <c>cos(azimut)·cos(elevation)</c> ;</item>
    /// <item>la marque <c>0x29</c>, posee sur le <b>porteur</b> et non sur lui-meme ;</item>
    /// <item>le son <see cref="s_ResistSoundScript_00551868"/> dans la file differee.</item>
    /// </list>
    /// <para>
    /// <b>Les deux mises a l'echelle des vitesses ne sont pas symetriques</b> : les composantes
    /// horizontales sont un produit de deux entrees de table ramene de six rangs puis
    /// <b>tronque a un multiple de 0x40</b> (<c>and al,0xc0</c> — les six bits bas de l'octet bas
    /// tombent, rien d'autre), tandis que la composante verticale est une seule entree de table
    /// <b>montee</b> de six rangs, sans troncature. Les deux arrivent a la meme echelle ; l'ecriture
    /// n'est pas la meme.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475D40(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);

        *(short*)(e + 0x0a) = 7;
        *(byte**)(e + 0x70) = carrier;
        *(byte**)(e + 0x74) = carrier;
        *(byte**)(e + 0x78) = carrier;
        *(uint*)(e + 0x38) = 0xf0f0f0;
        *(int*)(e + 0x44) = 0;

        // La hauteur de bulle d'etat du porteur, bit bas ote (`and al,0xfe`), montee de onze rangs.
        uint height = (uint)StatusBubbleHeight_00538BC0((uint*)carrier);
        *(int*)(e + 0x48) = (int)((height & 0xfffffffe) << 0xb);

        *(int*)(e + 0x4c) = 0;
        *(short*)(e + 0x12) = 0x1f;
        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
        *(uint*)(e + 0x28) = 0x20002000;
        *(short*)(e + 0x14) = 0;
        *(short*)(e + 0x16) = 3;
        *(short*)(e + 0x1a) = 1;

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551BA0(), 0xbe, e[0x0b], -1);

        *(uint*)(e + 0x28) = 0x10001000;
        *(short*)(e + 0x14) = 0x1000;
        *(short*)(e + 0x16) = 0x1f;
        *(short*)(e + 0x12) = 2;
        *(short*)(e + 0x1a) = 3;
        *(int*)(e + 0x70) = 0;

        // Ghidra: LAB_00475df0 — quatre tours, le compteur est un emplacement de pile.
        for (int i = 4; i != 0; i--)
        {
            int elevation = CrtRand();
            int azimuth = CrtRand();

            int ring = Winsys3d.GetCosTableEntry(elevation);

            *(int*)(e + 0x50) = ((int)Winsys3d.GetSinTableEntry(azimuth) * ring >> 6) & ~0x3f;
            *(int*)(e + 0x54) = (int)Winsys3d.GetSinTableEntry(elevation) << 6;
            *(int*)(e + 0x58) = (Winsys3d.GetCosTableEntry(azimuth) * ring >> 6) & ~0x3f;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519B8(), 0xbe, e[0x0b], -1);
        }

        // La marque se pose sur le PORTEUR : c'est lui qu'on voit encaisser le coup manque.
        SpawnStatusEffectEntity_0047AC40(
            carrier, (int*)(carrier + 0x1c), SpriteAtlas(0x00564808), 0x29, e[0x0b], -1);

        Aud.QueueDelayedSoundScript(ResistSoundScript(), (int*)(e + 0x1c));

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004459e0</c> — <b>l'entree 153 (<c>0x99</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 773 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_004459E0.asm</c>. C'est ce
    /// que <see cref="EntityHandler_00445950"/> seme une image sur deux, donc ce que tout effet
    /// applique finit par produire.
    ///
    /// <para>
    /// <b>Deux corps dans une seule fonction, choisis par <c>+0x44</c>.</b> La moitie haute ne
    /// s'execute que si <c>+0x44 == 0</c>, la moitie basse que si <c>+0x44 == 1</c> ; toute autre
    /// valeur ne fait rien du tout. Ce champ est celui que
    /// <see cref="ApplyStatusEffect_0053B3C0"/> pose a <b>1</b> et
    /// <see cref="ApplyTrapEffect_0053C420"/> a <b>0</b> le temps d'engendrer le <c>0x3a</c>
    /// parent — c'est donc <b>l'effet d'origine qui choisit l'aspect de la poussiere</b>, et c'est
    /// ce que le commentaire de <c>0x0053c420</c> laissait ouvert.
    /// </para>
    /// <para>Les deux moities partagent la meme forme et different par cinq valeurs :</para>
    /// <table>
    /// <tr><td/><td><c>+0x44 == 0</c></td><td><c>+0x44 == 1</c></td></tr>
    /// <tr><td>atlas</td><td><c>0x005515b8</c>, 1 image</td><td><c>0x005515c0</c>, 4 images</td></tr>
    /// <tr><td>teinte</td><td><c>b4 00 b4</c> (violet)</td><td><c>00 b4 c8</c> (cyan)</td></tr>
    /// <tr><td>echelle</td><td><c>0x400 + rand % 0x400</c></td><td><c>0x800 + rand % 0x400</c></td></tr>
    /// <tr><td>vitesse d'extinction</td><td><c>8 + rand % 0x10</c></td><td><c>6 + rand % 6</c></td></tr>
    /// <tr><td>image d'atlas</td><td>figee a 0</td><td>avance toutes les 6 etapes, <b>plafonnee a 3</b></td></tr>
    /// </table>
    /// <para>
    /// <b>La spirale</b> est la meme des deux cotes : <c>+0x9a</c> est l'angle, il avance de
    /// <c>0x64 + rand % 0x32</c> par image et se replie sur douze bits ; <c>+0x5c</c> est le rayon,
    /// tire une fois pour toutes ; <c>+0x12</c> est la montee, qui <b>croit de un par image</b>,
    /// donc l'ascension accelere. Le centre <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> est fige a la
    /// naissance, comme chez <see cref="EntityHandler_004034A0"/>.
    /// </para>
    /// <para>
    /// <b>Elle meurt d'etre eteinte</b> : chaque image <see cref="BrightenEntityTint_00404DE0"/>
    /// lui retire <c>+0x16</c> sur les trois canaux, et le bit de vie ne tombe que lorsque les
    /// <b>trois</b> sont a zero. La composante nulle des le depart (le vert du violet, le rouge du
    /// cyan) est deja au plancher ; ce sont les deux autres qui font l'horloge.
    /// </para>
    /// <para>
    /// <b>La naissance et la premiere image sont la meme passe.</b> Le bloc d'initialisation pose
    /// <c>+0x10 = 1</c> et le test qui suit accepte <c>1</c> : rien n'est saute. Transcrit tel quel,
    /// comme chez ses sœurs.
    /// </para>
    /// </summary>
    private static void EntityHandler_004459E0(byte* e)
    {
        if (*(int*)(e + 0x44) == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                // Ghidra: 0x004459f7 — la naissance du premier corps. Le germe est +0x14.
                short seed = *(short*)(e + 0x14);

                *(byte**)(e + 0x3c) = Descriptors005515B8();
                e[0x40] = 0;

                int x = *(int*)(e + 0x1c);
                int y = *(int*)(e + 0x20) + ((seed & ~1) << 0xb);
                *(int*)(e + 0x20) = y;
                int z = *(int*)(e + 0x24);

                e[0x0a] = 9;
                e[0x0b] = 0;
                e[0x38] = 0xb4;
                e[0x39] = 0;
                e[0x3a] = 0xb4;

                *(int*)(e + 0x50) = x;
                *(int*)(e + 0x54) = y;
                *(int*)(e + 0x58) = z;

                *(short*)(e + 0x9a) = (short)(CrtRand() % 0x1000);

                int radius = CrtRand();
                *(short*)(e + 0x10) = 1;
                *(int*)(e + 0x5c) = radius % ((seed >> 1) + 1);

                short scale = (short)(CrtRand() % 0x400 + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(short*)(e + 0x12) = (short)(CrtRand() % 3 + 3);
                *(short*)(e + 0x16) = (short)(CrtRand() % 0x10 + 8);
            }

            // Ghidra: LAB_00445aba — la premiere image passe ici aussi.
            if (*(short*)(e + 0x10) == 1)
            {
                short spin = *(short*)(e + 0x9a);
                int radius = *(int*)(e + 0x5c);

                *(int*)(e + 0x1c) =
                    (int)Winsys3d.GetSinTableEntry(spin) * radius + *(int*)(e + 0x50);
                *(int*)(e + 0x24) =
                    Winsys3d.GetCosTableEntry(spin) * radius + *(int*)(e + 0x58);

                int step = CrtRand() % 0x32;
                short climb = *(short*)(e + 0x12);
                int height = *(int*)(e + 0x20);

                *(short*)(e + 0x9a) =
                    (short)((((ushort)(step + *(short*)(e + 0x9a))) + 0x64) & 0xfff);

                *(int*)(e + 0x20) = height + (climb << 0xc);
                *(short*)(e + 0x12) = (short)(climb + 1);

                short fade = *(short*)(e + 0x16);
                BrightenEntityTint_00404DE0(e, (short)-fade, (short)-fade, (short)-fade);

                if (e[0x38] == 0 && e[0x39] == 0 && e[0x3a] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // Ghidra: LAB_00445b57 — le second corps, exclusif du premier.
        if (*(int*)(e + 0x44) == 1)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                short seed = *(short*)(e + 0x14);

                *(byte**)(e + 0x3c) = Descriptors005515C0();
                e[0x40] = 0;

                int x = *(int*)(e + 0x1c);
                int y = *(int*)(e + 0x20) + ((seed & ~1) << 0xb);
                *(int*)(e + 0x20) = y;
                int z = *(int*)(e + 0x24);

                e[0x0a] = 9;
                e[0x0b] = 0;
                e[0x38] = 0;
                e[0x39] = 0xb4;
                e[0x3a] = 0xc8;

                *(int*)(e + 0x50) = x;
                *(int*)(e + 0x54) = y;
                *(int*)(e + 0x58) = z;

                *(short*)(e + 0x9a) = (short)(CrtRand() % 0x1000);

                int radius = CrtRand();
                *(short*)(e + 0x10) = 1;
                *(int*)(e + 0x5c) = radius % ((seed >> 1) + 1);

                short scale = (short)(CrtRand() % 0x400 + 0x800);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(short*)(e + 0x12) = (short)(CrtRand() % 3 + 3);

                *(int*)(e + 0x48) = 0;                       // le compteur de sous-etapes
                *(short*)(e + 0x16) = (short)(CrtRand() % 6 + 6);
            }

            // Ghidra: LAB_00445c21
            if (*(short*)(e + 0x10) == 1)
            {
                short spin = *(short*)(e + 0x9a);
                int radius = *(int*)(e + 0x5c);

                *(int*)(e + 0x1c) =
                    (int)Winsys3d.GetSinTableEntry(spin) * radius + *(int*)(e + 0x50);
                *(int*)(e + 0x24) =
                    Winsys3d.GetCosTableEntry(spin) * radius + *(int*)(e + 0x58);

                int step = CrtRand() % 0x32;
                short climb = *(short*)(e + 0x12);
                int height = *(int*)(e + 0x20);
                int substep = *(int*)(e + 0x48);

                *(short*)(e + 0x9a) =
                    (short)((((ushort)(step + *(short*)(e + 0x9a))) + 0x64) & 0xfff);

                *(int*)(e + 0x20) = height + (climb << 0xc);
                *(short*)(e + 0x12) = (short)(climb + 1);

                substep++;
                *(int*)(e + 0x48) = substep;

                if (substep >= 6)
                {
                    *(int*)(e + 0x48) = 0;

                    // L'octet monte d'un cran, et s'il atteint 4 il redescend a 3 : la derniere
                    // image reste affichee au lieu de repartir a zero.
                    byte frame = (byte)(e[0x40] + 1);
                    e[0x40] = frame;
                    if (frame == 4) e[0x40] = 3;
                }

                short fade = *(short*)(e + 0x16);
                BrightenEntityTint_00404DE0(e, (short)-fade, (short)-fade, (short)-fade);

                if (e[0x38] == 0 && e[0x39] == 0 && e[0x3a] == 0) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005519a0</c> — <b>les trois images de l'eclat <c>0x2d</c></b>, celui que
    /// <see cref="EntityHandler_004751E0"/> seme par six. Borne par
    /// <see cref="EntityHandler_0047A880"/>, qui avance <c>+0x40</c> d'un cran par image et le
    /// remet a zero des qu'il atteint 3.
    ///
    /// <para>
    /// La plage physique va de <c>0x005519a0</c> a <c>0x005519d0</c> et compte six tuiles
    /// <c>0x10 x 0x10</c> ; <see cref="s_Descriptors_005519B8"/> en prend les trois dernieres.
    /// <b>Deux tables se partagent donc une seule plage contigue</b>, chacune bornee par son propre
    /// consommateur — c'est le cas ou relever un descripteur isole aurait ete le plus dangereux.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_005519A0 =
    [
        0x00, 0xc0, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x005519a0
        0x10, 0xc0, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x005519a8
        0x20, 0xc0, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x005519b0
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551a40</c> — <b>les quatre images du panache <c>0x3e</c></b>, l'atlas
    /// que <see cref="EntityHandler_00475760"/> passe a son enfant. Borne par
    /// <see cref="EntityHandler_004757A0"/>, qui pose <c>+0x40 = (compteur / 2) &amp; 3</c>.
    ///
    /// <para>
    /// La donnee le confirme deux fois : les quatre enregistrements se suivent en <c>b8</c>
    /// (<c>60</c>, <c>78</c>, <c>90</c>, <c>a8</c>) et portent <c>00 02 10 01</c> en queue, la ou
    /// <see cref="s_Descriptors_00551A60"/> qui les suit change de tuile <b>et</b> de queue
    /// (<c>01 02 10 00</c>).
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551A40 =
    [
        0x60, 0xb8, 0x18, 0x18, 0x00, 0x02, 0x10, 0x01,   // 0x00551a40
        0x78, 0xb8, 0x18, 0x18, 0x00, 0x02, 0x10, 0x01,   // 0x00551a48
        0x90, 0xb8, 0x18, 0x18, 0x00, 0x02, 0x10, 0x01,   // 0x00551a50
        0xa8, 0xb8, 0x18, 0x18, 0x00, 0x02, 0x10, 0x01,   // 0x00551a58
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551a78</c> — <b>les quatre images de l'anneau</b>, et le seul atlas de
    /// cette serie que <b>deux</b> engendreurs se partagent :
    /// <see cref="EntityHandler_00475A30"/> le donne a son enfant <c>0x40</c>, et
    /// <see cref="ApplyStatusEffect_0053B580"/> au <c>0xbb</c> qu'il fait naitre sur la victime.
    ///
    /// <para>
    /// Quatre, et les deux consommateurs le disent de la meme facon —
    /// <see cref="EntityHandler_00475A80"/> et <see cref="EntityHandler_004768A0"/> posent tous
    /// deux <c>+0x40 = rand() &amp; 3</c>. La donnee suit : quatre tuiles <c>0x10 x 0x10</c> en
    /// <c>00</c> (<c>98</c>, <c>a8</c>, <c>b8</c>, <c>c8</c>), puis <c>0x00551a98</c> passe en
    /// <c>0x20 x 0x20</c>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551A78 =
    [
        0x98, 0x00, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551a78
        0xa8, 0x00, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551a80
        0xb8, 0x00, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551a88
        0xc8, 0x00, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551a90
    ];

    private static byte* s_Descriptors005519A0Storage;
    private static byte* s_Descriptors00551A40Storage;
    private static byte* s_Descriptors00551A78Storage;

    private static byte* Descriptors005519A0()
        => NativeCopy(s_Descriptors_005519A0, ref s_Descriptors005519A0Storage);

    private static byte* Descriptors00551A40()
        => NativeCopy(s_Descriptors_00551A40, ref s_Descriptors00551A40Storage);

    private static byte* Descriptors00551A78()
        => NativeCopy(s_Descriptors_00551A78, ref s_Descriptors00551A78Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551a98</c> — <b>les quatre images de la poussiere de Rocher</b>, l'atlas
    /// que <see cref="TickRock_0047B850"/> passe a son enfant <c>0x4b</c>, une fois a la naissance
    /// et une fois a l'impact.
    ///
    /// <para>
    /// <b>Quatre, et le consommateur le dit</b> : <see cref="TickRockDust_0047BB10"/> pose
    /// <c>+0x40 = compteur &gt;&gt; 1</c> et s'eteint des que ce quotient atteint 4. La donnee suit
    /// — quatre tuiles <c>0x20 x 0x20</c> en <c>v = 0x88</c> (<c>u</c> = <c>0x40</c>, <c>0x60</c>,
    /// <c>0x80</c>, <c>0xa0</c>), puis <c>0x00551ab8</c> reprend la meme serie avec un
    /// <b>dernier octet different</b> (<c>02</c> au lieu de <c>00</c>) : c'est une seconde table,
    /// pas la suite de celle-ci.
    /// </para>
    /// <para>
    /// Elle commence ou <see cref="s_Descriptors_00551A78"/> finit, et les deux plages se suivent
    /// sans trou — la tuile passe de <c>0x10 x 0x10</c> a <c>0x20 x 0x20</c> exactement a la
    /// frontiere.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551A98 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551a98
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551aa0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551aa8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551ab0
    ];

    private static byte* s_Descriptors00551A98Storage;

    /// <summary>Les quatre descripteurs en memoire native.</summary>
    private static byte* Descriptors00551A98()
        => NativeCopy(s_Descriptors_00551A98, ref s_Descriptors00551A98Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551a08</c> — <b>les sept images de l'eclaboussure de tir</b>, relevees
    /// dans <c>Documentation/Reference/DataSymbols.md</c> le 2026-08-29 et transcrites ici le
    /// 2026-08-31 pour <see cref="TickMagicArrowShaft_0047CA50"/>, qui les passe a son
    /// <c>0xbd</c> quand la fleche meurt sur le decor.
    ///
    /// <para>
    /// <b>Sept, et c'est le code qui le dit</b> : <c>EntityHandler_00476320</c> compte
    /// <c>+0x40</c> jusqu'a 7 avant de s'eteindre, et
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> pose ce champ a zero. La donnee le confirme —
    /// la huitieme entree (<c>0x00551a40</c>) change de forme.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551A08 =
    [
        0xc0, 0xb8, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a08
        0xd8, 0xb8, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a10
        0x60, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a18
        0x78, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a20
        0x90, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a28
        0xa8, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a30
        0xc0, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a38
    ];

    private static byte* s_Descriptors00551A08Storage;

    /// <summary>Les sept descripteurs en memoire native.</summary>
    private static byte* Descriptors00551A08()
        => NativeCopy(s_Descriptors_00551A08, ref s_Descriptors00551A08Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551b40</c> — <b>les quatre images de la colonne de feu</b>, l'atlas que
    /// <see cref="TickFlameWall_0047CFF0"/> passe a son enfant <c>0x52</c>.
    ///
    /// <para>
    /// <b>Quatre, et c'est l'engendreur qui le dit</b> — pas le consommateur, pour une fois : le
    /// sort pose <c>+0x1a = 4</c> juste avant d'engendrer, et
    /// <see cref="TickFlameWallColumn_0047D2A0"/> fait tourner <c>+0x40</c> en anneau sur ce compte.
    /// La donnee suit : quatre tuiles <c>0x18 x 0x20</c> en <c>v = 0x48</c>, puis
    /// <c>0x00551b60</c> passe en <c>0x10 x 0x10</c>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551B40 =
    [
        0x00, 0x48, 0x18, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551b40
        0x18, 0x48, 0x18, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551b48
        0x30, 0x48, 0x18, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551b50
        0x48, 0x48, 0x18, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551b58
    ];

    private static byte* s_Descriptors00551B40Storage;

    /// <summary>Les quatre descripteurs en memoire native.</summary>
    private static byte* Descriptors00551B40()
        => NativeCopy(s_Descriptors_00551B40, ref s_Descriptors00551B40Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552530</c> — <b>le craquement de la glace</b>, et le <b>premier script
    /// de sons differes</b> que le depot rencontre. <see cref="TickIcePillar_0047D670"/> le remet a
    /// <c>QueueDelayedSoundScript</c> a l'instant ou la colonne finit de monter.
    ///
    /// <para>
    /// Le format est celui que <c>TickDelayedSounds</c> lit : huit octets par pas — le son en
    /// <c>[0]</c>, le <b>delai avant le pas suivant</b> en <c>[1]</c> (zero termine), le nombre de
    /// sons a jouer d'un coup en <c>[2]</c>, la hauteur en <c>[4]</c> et le gain en <c>[6]</c>.
    /// </para>
    /// <para>
    /// Trois pas, tous le meme son <c>0x23</c> a la meme hauteur, espaces de <b>vingt images</b> :
    /// la glace craque trois fois. La borne est le <c>[1] = 0</c> du troisieme ; le pas suivant
    /// (<c>0x00552548</c>) ouvre un autre script, avec un autre son et un autre delai.
    /// </para>
    /// <para>
    /// <b>La file des sons differes cessait donc d'etre morte</b> : jusqu'ici rien de transcrit ne
    /// la remplissait, et <c>TickDelayedSounds</c> tournait a vide. C'est ce sort qui l'alimente.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552530 =
    [
        0x23, 0x14, 0x01, 0x00, 0x51, 0x06, 0x00, 0x12,   // 0x00552530
        0x23, 0x14, 0x01, 0x00, 0x51, 0x06, 0x00, 0x12,   // 0x00552538
        0x23, 0x00, 0x01, 0x00, 0x51, 0x06, 0x00, 0x12,   // 0x00552540 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552530Storage;

    /// <summary>Les trois pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00552530()
        => NativeCopy(s_DelayedSoundScript_00552530, ref s_DelayedSoundScript00552530Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552548</c> — <b>le second script de sons differes</b>, celui que
    /// <see cref="TickThunderStrand_0047E6F0"/> met en file a chaque decharge. Deux pas, et le
    /// premier delai vaut <b>1</b> : le second son part a l'image suivante, ce qui donne le
    /// claquement en deux temps de la foudre.
    ///
    /// <para>
    /// Il suit immediatement <see cref="s_DelayedSoundScript_00552530"/> dans le binaire, et c'est
    /// son premier octet de delai nul qui bornait celui-la.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552548 =
    [
        0x0c, 0x01, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x00552548
        0x20, 0x00, 0x01, 0x00, 0x4e, 0x0f, 0x00, 0x12,   // 0x00552550 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552548Storage;

    /// <summary>Les deux pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00552548()
        => NativeCopy(s_DelayedSoundScript_00552548, ref s_DelayedSoundScript00552548Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552558</c> — <b>le troisieme script de sons differes</b>, celui que
    /// <see cref="TickExorcism_0047F070"/> met en file a sa naissance. Deux pas : le son
    /// <c>0x19</c>, deux images d'attente, puis le <c>0x1c</c> qui termine.
    ///
    /// <para>
    /// Il suit <see cref="s_DelayedSoundScript_00552548"/> sans trou, et son propre octet de delai
    /// nul borne les deux entrees suivantes (<c>0x00552568</c>), qui appartiennent a un quatrieme
    /// script non encore rencontre.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552558 =
    [
        0x19, 0x02, 0x01, 0x00, 0x00, 0x08, 0x00, 0x12,   // 0x00552558
        0x1c, 0x00, 0x01, 0x00, 0x00, 0x08, 0x00, 0x12,   // 0x00552560 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript00552558Storage;

    /// <summary>Les deux pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00552558()
        => NativeCopy(s_DelayedSoundScript_00552558, ref s_DelayedSoundScript00552558Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552568</c> — <b>le quatrieme script de sons differes</b>, celui de
    /// <see cref="TickGale_0047FB80"/>. Le son <c>0x14</c>, une image d'attente, puis le
    /// <c>0x2c</c> qui termine — le souffle avant la bourrasque.
    ///
    /// <para>
    /// Il est le quatrieme d'une file de scripts contigus depuis <c>0x00552530</c>, chacun borne
    /// par l'octet de delai nul du precedent. Le cinquieme (<c>0x00552578</c>) reste a rencontrer.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552568 =
    [
        0x14, 0x01, 0x01, 0x00, 0x00, 0x08, 0x00, 0x12,   // 0x00552568
        0x2c, 0x00, 0x01, 0x00, 0x00, 0x08, 0x00, 0x12,   // 0x00552570 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript00552568Storage;

    /// <summary>Les deux pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00552568()
        => NativeCopy(s_DelayedSoundScript_00552568, ref s_DelayedSoundScript00552568Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552578</c> — <b>le cinquieme script de sons differes</b>, celui de
    /// <see cref="TickPolarDemon_0047FE60"/>. Le son <c>0x23</c>, trois images d'attente, puis le
    /// <c>0x12</c> qui termine.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552578 =
    [
        0x23, 0x03, 0x01, 0x00, 0xfd, 0x06, 0x00, 0x12,   // 0x00552578
        0x12, 0x00, 0x01, 0x00, 0xbf, 0x01, 0x00, 0x12,   // 0x00552580 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript00552578Storage;

    /// <summary>Les deux pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00552578()
        => NativeCopy(s_DelayedSoundScript_00552578, ref s_DelayedSoundScript00552578Storage);

    /// <summary>
    /// Ghidra: FUN_00475a30 @ 0x00475a30 — <b>l'entree 183 (<c>0xb7</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Un piege de coffre reduit a sa naissance</b> : il retient sa victime en <c>+0x78</c>,
    /// pose une teinte grise et une echelle, lache un <c>0x40</c>
    /// (<see cref="EntityHandler_00475A80"/>) et s'efface dans la meme image. Toute la duree et
    /// tout l'effet sont dans l'enfant.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475A30(byte* e)
    {
        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);
        *(short*)(e + 0x0a) = 0;
        *(uint*)(e + 0x38) = 0x808080;
        *(uint*)(e + 0x28) = 0x18001800;
        e[0x40] = 0;

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551A78(), 0x40, 0, 9);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00475b90 @ 0x00475b90 — <b>l'entree 184 (<c>0xb8</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Meme forme que <see cref="EntityHandler_00475A30"/>, mais son enfant part par
    /// <see cref="SpawnCastEntity_0047AAE0"/> et non par
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> : il naitra donc dans la table
    /// <c>0x005edee0</c>, resolue par <see cref="s_StaticEntityHandlers_00551f30"/>, ou le
    /// sous-type <c>0x5f</c> designe <see cref="EntityHandler_00475BE0"/>. <b>C'est la seule
    /// difference qui compte</b> — le meme numero dans l'autre table designerait un tout autre
    /// corps.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475B90(byte* e)
    {
        *(short*)(e + 0x0a) = 0x0d;
        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);
        e[4] = 2;
        *(uint*)(e + 0x38) = 0xf0f0f0;
        *(uint*)(e + 0x28) = 0x10001000;
        *(short*)(e + 0x2c) = 0x1000;

        SpawnCastEntity_0047AAE0(e, 0x5f, e[0x0b], -1);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00475760 @ 0x00475760 — <b>l'entree 185 (<c>0xb9</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, celle qui a leve en jeu le 2026-08-29.
    /// Decompilee dans <c>Blaze.c</c>. La plus courte des naissances de la famille : elle lache un
    /// <c>0x3e</c> (<see cref="EntityHandler_004757A0"/>) avec
    /// <see cref="s_Descriptors_00551A40"/> et s'efface.
    /// </summary>
    private static void EntityHandler_00475760(byte* e)
    {
        *(short*)(e + 0x0a) = 0;
        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);
        e[0x40] = 0;

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551A40(), 0x3e, e[0x0b], -1);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00476730</c> — <b>l'entree 187 (<c>0xbb</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 45 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Ce n'est pas un piege, c'est la secousse de l'ecran</b> : la seule chose que cette entite
    /// touche est <c>DAT_005de0ac</c>, le decalage vertical que la camera ajoute a sa cible. Elle
    /// l'arme a la naissance avec son propre <c>+0x0a</c>, puis l'amortit a chaque image par
    /// <c>a &lt;- (a / 4) - a</c> : trois quarts d'amplitude en moins, <b>et le signe qui
    /// s'inverse</b>. C'est une oscillation amortie ecrite en une soustraction.
    /// </para>
    /// <para>
    /// Elle meurt quand la valeur absolue tombe a 1 ou moins. <b>La derniere valeur reste
    /// ecrite</b> : rien ne remet <c>DAT_005de0ac</c> a zero, la camera garde un decalage de un ou
    /// zero pour toujours. Transcrit tel quel — c'est ce que fait le binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476730(byte* e)
    {
        int* shake = Mem.Camera(0x005de0ac);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *shake = e[0x0a];
            return;
        }

        int amplitude = *shake;
        amplitude = (amplitude >> 2) - amplitude;
        *shake = amplitude;

        if (amplitude < 0) amplitude = -amplitude;
        if (amplitude <= 1) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00475170 @ 0x00475170 — <b>l'entree 188 (<c>0xbc</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>La seule naissance de la famille qui lache deux enfants</b>, et le meme sous-type deux
    /// fois : <c>0xbc</c> par <see cref="SpawnCastEntity_0047AAE0"/>, une fois avec
    /// <c>+0x04 = 0x18</c> et une fois avec <c>0x19</c>. Le champ de modele est le seul ecart entre
    /// les deux — deux moities d'une meme forme, dessinees par deux entites.
    /// </para>
    /// <para>
    /// <b>Trois numeros <c>0xbc</c> dans trois tables.</b> Celui-ci est l'entree de
    /// <c>0x00551c30</c> ; ses enfants naissent dans <c>0x005edee0</c>, ou
    /// <see cref="s_StaticEntityHandlers_00551f30"/> resout <c>0xbc</c> vers
    /// <see cref="EntityHandler_004751E0"/> ; lequel engendre a son tour un <c>0xbc</c> dans
    /// <c>0x005f2d40</c>, ou <see cref="s_StaticEntityHandlers_00552230"/> le resout vers
    /// <see cref="EntityHandler_004752F0"/>. Le meme octet designe trois corps differents selon la
    /// table de destination de l'engendreur.
    /// </para>
    /// <para>
    /// Il se place a la hauteur de bulle d'etat de sa victime, dont il garde la mesure dans
    /// <c>+0x12</c> pour que les enfants la retrouvent.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475170(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        *(byte**)(e + 0x74) = carrier;

        uint height = (uint)StatusBubbleHeight_00538BC0((uint*)carrier);
        *(short*)(e + 0x12) = (short)height;
        *(int*)(e + 0x20) += (int)((height & 0xfffffffe) * 0x800);

        *(uint*)(e + 0x28) = 0x181c181c;
        *(short*)(e + 0x2c) = 0x181c;

        e[4] = 0x18;
        SpawnCastEntity_0047AAE0(e, 0xbc, 1, 0x0d);
        e[4] = 0x19;
        SpawnCastEntity_0047AAE0(e, 0xbc, 1, 0x0d);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004753d0 @ 0x004753d0 — <b>l'entree 189 (<c>0xbd</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>. Deux
    /// lignes : elle pose une marque <c>0x29</c> sur sa victime et disparait.
    ///
    /// <para>
    /// <b>Son atlas est choisi par son propre <c>+0x0a</c></b> — <c>0x005647c0 + n * 8</c>, un
    /// descripteur pris en avant dans la grande plage. C'est le seul de la famille dont l'engendreur
    /// choisit l'apparence par un champ plutot que par une adresse, et la raison pour laquelle
    /// <see cref="SpriteAtlas"/> releve la plage entiere au lieu des descripteurs un par un.
    /// </para>
    /// </summary>
    private static void EntityHandler_004753D0(byte* e)
    {
        SpawnStatusEffectEntity_0047AC40(
            *(byte**)(e + 0x6c), (int*)(e + 0x1c), SpriteAtlas(0x005647c0) + e[0x0a] * 8, 0x29, 0, 0);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004757a0</c> — <b>l'entree 62 (<c>0x3e</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 288 octets, <b>absente de
    /// <c>Blaze.c</c></b>. L'enfant de <see cref="EntityHandler_00475760"/>, et l'un des quatre
    /// corps de meme forme que <see cref="EntityHandler_00475910"/>.
    ///
    /// <para>
    /// <b>Soixante-cinq images, puis l'effet 8</b> (<see cref="ApplyStatusEffect_0053B4B0"/>) porte
    /// par l'entite d'emprunt <c>0x005ede20</c>. Entre-temps il reste colle a sa victime, un peu
    /// au-dessus (<c>+0x50000</c>), et <b>pulse</b> : sa teinte et son echelle suivent toutes deux
    /// un sinus de l'age, la teinte a <c>255/4096</c> pres et l'echelle doublee.
    /// </para>
    /// <para>Son son part une image sur <b>quinze</b>.</para>
    /// </summary>
    private static void EntityHandler_004757A0(byte* e)
    {
        if (*(short*)(e + 0x10) % 15 == 1)
        {
            Aud.PlayPositionalSound(0x17, 0x100, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_004757d1
        short counter = *(short*)(e + 0x10);
        byte* carrier = *(byte**)(e + 0x78);

        e[0x40] = (byte)(((sbyte)counter >> 1) & 3);

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        int wave = (int)Winsys3d.GetSinTableEntry(counter << 5);
        int level = (wave << 8) - wave >> 0xc;
        *(uint*)(e + 0x38) = (uint)((((level << 8) | level) << 8) | level);

        int size = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 5) << 1;
        *(uint*)(e + 0x28) = (uint)((size << 0x10) | size);

        if (*(short*)(e + 0x10) >= 0x40)
        {
            byte* proxy = Mem.Entity(0x005ede20);

            *(uint*)e = 0x80400000;
            Mem.CopyDwords((uint*)e, (uint*)proxy, 0x27);      // Ghidra: FUN_005352e0

            proxy[8] = 0x20;
            ResolveEntityBlocks(proxy, out byte* stats, out byte* species, out byte* model);
            ResetEntityStats(stats, species, model);           // Ghidra: FUN_005389e0

            *(short*)(proxy + 6) = *(short*)(e + 6);

            ApplyStatusEffect_0053B4B0((uint*)proxy, *(uint**)(e + 0x78), 0);

            *(uint*)e = 0;
        }

        // Ghidra: LAB_004758b5
        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00475a80</c> — <b>l'entree 64 (<c>0x40</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 272 octets, <b>absente de
    /// <c>Blaze.c</c></b>. L'enfant de <see cref="EntityHandler_00475A30"/>.
    ///
    /// <para>
    /// <b>Soixante-cinq images, puis l'effet 9</b> (<see cref="ApplyStatusEffect_0053B580"/>). Il
    /// tourne autour de sa victime a soixante unites de rayon — l'angle <c>+0x32</c> avance de
    /// <c>0x80</c> par image, un tour complet en trente-deux — pendant que sa hauteur suit un sinus
    /// de l'age releve de <c>0x4b00</c>. Son image d'atlas est <b>tiree au hasard a chaque
    /// image</b> parmi quatre : il scintille au lieu de s'animer.
    /// </para>
    /// <para>
    /// <b><see cref="EntityHandler_004768A0"/> est le meme corps sans l'effet</b> — meme rayon,
    /// meme <c>0x4b00</c>, meme tirage. Celui-la est ce que l'effet 9 fait naitre sur la victime,
    /// celui-ci ce qui l'applique. Le compilateur n'a pas partage le code, et la reconstruction ne
    /// le partage pas non plus : ce sont deux fonctions du binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475A80(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            Aud.PlayPositionalSound(0x1a, 0x200, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00475aa8
        byte* carrier = *(byte**)(e + 0x78);

        int roll = CrtRand();
        *(short*)(e + 0x32) += 0x80;
        e[0x40] = (byte)(roll & 3);

        short spin = *(short*)(e + 0x32);

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c) + (int)Winsys3d.GetSinTableEntry(spin) * 0x3c;
        *(int*)(e + 0x20) =
            ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 8) + 0x4b00) * 0x20
            + *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24) + Winsys3d.GetCosTableEntry(spin) * 0x3c;

        if (*(short*)(e + 0x10) >= 0x40)
        {
            byte* proxy = Mem.Entity(0x005ede20);

            *(uint*)e = 0x80400000;
            Mem.CopyDwords((uint*)e, (uint*)proxy, 0x27);      // Ghidra: FUN_005352e0

            proxy[8] = 0x20;
            ResolveEntityBlocks(proxy, out byte* stats, out byte* species, out byte* model);
            ResetEntityStats(stats, species, model);           // Ghidra: FUN_005389e0

            *(short*)(proxy + 6) = *(short*)(e + 6);

            ApplyStatusEffect_0053B580((uint*)proxy, *(uint**)(e + 0x78), 0);

            *(uint*)e = 0;
        }

        // Ghidra: LAB_00475b85
        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00475be0</c> — <b>l'entree 95 (<c>0x5f</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>, 224 octets, <b>absente de
    /// <c>Blaze.c</c></b>. L'enfant de <see cref="EntityHandler_00475B90"/>.
    ///
    /// <para>
    /// <b>Sa duree n'est pas comptee, elle est calculee.</b> Il ne teste aucun compteur : sa teinte
    /// part de <c>0xf0f0f0</c> et perd <c>0x080808</c> par image, et l'effet 10
    /// (<see cref="ApplyStatusEffect_0053B660"/>) part <b>quand le mot de teinte atteint exactement
    /// zero</b>. <c>0xf0 / 0x08 = 30</c> : trente images, sans qu'aucune constante ne les nomme.
    /// C'est l'horloge la plus economique de la famille, et elle ne tient que parce que la division
    /// tombe juste.
    /// </para>
    /// <para>Il reste colle a sa victime, un peu au-dessus, et retrecit de <c>0x80</c> par image.</para>
    /// </summary>
    private static void EntityHandler_00475BE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            Aud.PlayPositionalSound(0x31, 0x100, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00475c07
        byte* carrier = *(byte**)(e + 0x78);

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        *(short*)(e + 0x2c) += unchecked((short)0xff80);
        *(uint*)(e + 0x28) += 0xff7fff80;

        uint tint = *(uint*)(e + 0x38) + 0xfff7f7f8;
        *(uint*)(e + 0x38) = tint;

        if (tint == 0)
        {
            byte* proxy = Mem.Entity(0x005ede20);

            *(uint*)e = 0x80400000;
            Mem.CopyDwords((uint*)e, (uint*)proxy, 0x27);      // Ghidra: FUN_005352e0

            proxy[8] = 0x20;
            ResolveEntityBlocks(proxy, out byte* stats, out byte* species, out byte* model);
            ResetEntityStats(stats, species, model);           // Ghidra: FUN_005389e0

            *(short*)(proxy + 6) = *(short*)(e + 6);

            ApplyStatusEffect_0053B660((uint*)proxy, *(uint**)(e + 0x78), 0);

            *(uint*)e = 0;
        }

        // Ghidra: LAB_00475cb4
        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_004751e0 @ 0x004751e0 — <b>l'entree 188 (<c>0xbc</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>. Decompilee dans <c>Blaze.c</c>. L'un des
    /// deux enfants de <see cref="EntityHandler_00475170"/> — voir la note sur les trois
    /// <c>0xbc</c> qui s'y trouve.
    ///
    /// <para>
    /// <b>Il tourne avec sa victime</b> : son angle est recopie de <c>+0x32</c> du porteur a chaque
    /// image, et son rayon est la demi-hauteur de bulle d'etat que le pere a laissee en
    /// <c>+0x12</c>. Trente-deux images, puis il s'efface.
    /// </para>
    /// <para>
    /// <b>A la 24e image exactement</b>, et seulement si son champ de modele n'est pas <c>0x18</c>
    /// — c'est-a-dire pour la <b>seconde</b> des deux entites que le pere a lachees —, il eclate :
    /// un <c>0xbc</c> de la troisieme table (<see cref="EntityHandler_004752F0"/>) prenant son
    /// propre atlas avance de <c>+0x40</c>, puis <b>six</b> eclats <c>0x2d</c>
    /// (<see cref="EntityHandler_0047A880"/>) lances a soixante degres d'ecart les uns des autres
    /// (<c>0x2aa</c> de pas sur douze bits) et vers le haut.
    /// </para>
    /// </summary>
    private static void EntityHandler_004751E0(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x74);

        if (e[4] != 0x18)
        {
            // Ghidra: `*(short *)(param_1 + 0xd)` sur un `undefined4 *` — donc +0x34, pas +0x1a.
            if (*(short*)(e + 0x34) < 0x800) *(short*)(e + 0x34) += 0x60;

            if (*(short*)(e + 0x10) == 0x18)
            {
                SpawnStatusEffectEntity_0047AC40(
                    e, (int*)(e + 0x1c), *(byte**)(e + 0x3c) + e[0x40] * 8, 0xbc, 0, 0);

                *(short*)(e + 0x9a) = 0x4000;

                for (int i = 6; i != 0; i--)
                {
                    short spin = *(short*)(e + 0x32);

                    *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(spin) << 4;
                    *(int*)(e + 0x54) = 0x10000;
                    *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(spin) << 4;

                    SpawnStatusEffectEntity_0047AC40(
                        e, (int*)(e + 0x1c), Descriptors005519A0(), 0x2d, e[0x0b], -1);

                    *(short*)(e + 0x32) += 0x2aa;
                }
            }
        }

        short angle = *(short*)(carrier + 0x32);
        *(short*)(e + 0x32) = angle;

        int radius = *(short*)(e + 0x12) >> 1;

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * radius + *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = radius * 0x1000 + *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * radius + *(int*)(carrier + 0x24);

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) > 0x1f) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004752f0 @ 0x004752f0 — <b>l'entree 188 (<c>0xbc</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, la troisieme du nom. Decompilee dans
    /// <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>C'est <see cref="EntityHandler_00475400"/> a deux details pres</b> : son ancre est
    /// <c>+0x74</c> et non <c>+0x6c</c>, son echelle monte a <c>0x1800</c> et non <c>0x1200</c>, et
    /// il se donne un saut initial de <c>0x32000</c>. Tout le reste — le sinus sur les douze
    /// premieres images, la teinte qui descend de <c>0xff</c> a <c>0x1f</c> a partir de la 32e, la
    /// mort a la 40e — est identique, terme pour terme.
    /// </para>
    /// </summary>
    private static void EntityHandler_004752F0(byte* e)
    {
        byte* anchor = *(byte**)(e + 0x74);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x0a) = 0;
            *(uint*)(e + 0x28) = 0;
            *(uint*)(e + 0x40) = 0;
            *(uint*)(e + 0x38) = 0x1ffffff;
            *(short*)(e + 0x2e) = unchecked((short)0xfff6);
            *(int*)(e + 0x20) += 0x32000;
        }

        short angle = *(short*)(anchor + 0x32);
        int radius = *(short*)(e + 0x12) >> 1;

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * radius + *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = radius * 0x1000 + *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * radius + *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) < 0xc)
        {
            int scale = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 7);
            short size = (short)(scale * 0x1800 >> 0xc);
            *(short*)(e + 0x28) = size;
            *(short*)(e + 0x2a) = size;
        }

        short frame = *(short*)(e + 0x10);

        if (frame > 0x1f)
        {
            uint level = (uint)(frame * -0x20 + 0x4ff);
            *(uint*)(e + 0x38) = ((level | 0x200) << 8 | level) << 8 | level;
        }

        *(short*)(e + 0x10) = (short)(frame + 1);
        if ((short)(frame + 1) > 0x27) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0047a880</c> — <b>l'entree 45 (<c>0x2d</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 197 octets, <b>absente de
    /// <c>Blaze.c</c></b>. Les six eclats que <see cref="EntityHandler_004751E0"/> lance.
    ///
    /// <para>
    /// <b>Un projectile a frottement et a pesanteur</b>, et le seul de la famille dont la pesanteur
    /// vienne d'un champ : <c>+0x9a</c> (que le pere pose a <c>0x4000</c>) est retire de la vitesse
    /// verticale a chaque image, au huitieme de rang pres. Les deux composantes horizontales
    /// perdent <c>1/8 + 1/256</c> — le meme frottement que
    /// <see cref="IntegrateWithDrag_00535880"/>, ecrit en clair.
    /// </para>
    /// <para>
    /// <b>Sa duree se lit dans sa teinte, comme chez <see cref="EntityHandler_00475BE0"/></b> : il
    /// part de <c>0xf0f0f0</c>, perd <c>0x101010</c> par image et meurt quand le mot atteint zero,
    /// soit quinze images. Le test est <b>avant</b> la soustraction, ce qui donne bien quinze
    /// images de dessin et non quatorze. Son echelle perd un huitieme par image en meme temps, donc
    /// il retrecit en s'eteignant.
    /// </para>
    /// </summary>
    private static void EntityHandler_0047A880(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(uint*)(e + 0x38) = 0xf0f0f0;
            *(uint*)(e + 0x28) = 0x10001000;
        }

        // Ghidra: LAB_0047a8a2
        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x24) += vz;
        *(int*)(e + 0x20) += vy;

        int gravity = *(short*)(e + 0x9a);

        *(int*)(e + 0x50) = vx - (vx >> 8) - (vx >> 3);
        *(int*)(e + 0x54) = (gravity >> 8) - (vy >> 8) + vy - gravity;
        *(int*)(e + 0x58) = vz - (vz >> 8) - (vz >> 3);

        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if (frame >= 3) e[0x40] = 0;

        // Ghidra: LAB_0047a912 — le test porte sur la teinte AVANT qu'elle ne descende.
        if (*(uint*)(e + 0x38) == 0)
        {
            *(uint*)e = 0;
            return;
        }

        *(uint*)(e + 0x38) += 0xffefeff0;

        short size = *(short*)(e + 0x28);
        size -= (short)(size >> 3);
        *(short*)(e + 0x28) = size;
        *(short*)(e + 0x2a) = size;
    }

    /// <summary>
    /// Ghidra: FUN_004768a0 @ 0x004768a0 — <b>l'entree 187 (<c>0xbb</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>. Decompilee dans <c>Blaze.c</c>. Ce que
    /// <see cref="ApplyStatusEffect_0053B580"/> fait naitre sur la victime quand l'effet prend.
    ///
    /// <para>
    /// <b>C'est <see cref="EntityHandler_00475A80"/> prive de sa conclusion</b> : meme anneau a
    /// soixante unites, meme pas d'angle de <c>0x80</c>, meme hauteur en sinus relevee de
    /// <c>0x4b00</c>, meme tirage d'image parmi quatre — mais il se contente de tourner soixante-quatre
    /// images et de s'eteindre. L'un applique l'effet, l'autre le signale.
    /// </para>
    /// <para>
    /// <b>Il se garde d'un porteur nul</b>, ce que son jumeau ne fait pas : sans porteur il ne
    /// bouge pas du tout au lieu de dereferencer zero. La difference vient de l'engendreur — celui-ci
    /// nait d'une victime dont <c>+0x78</c> peut etre vide.
    /// </para>
    /// </summary>
    private static void EntityHandler_004768A0(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            Aud.PlayPositionalSound(0x1a, 0x200, 0x1200, (int*)(e + 0x1c));
        }

        byte* carrier = *(byte**)(e + 0x78);
        if (carrier != null)
        {
            byte roll = (byte)CrtRand();
            *(short*)(e + 0x32) += 0x80;
            e[0x40] = (byte)(roll & 3);

            short spin = *(short*)(e + 0x32);

            *(int*)(e + 0x1c) =
                *(int*)(carrier + 0x1c) + (int)Winsys3d.GetSinTableEntry(spin) * 0x3c;
            *(int*)(e + 0x20) =
                ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 8) + 0x4b00) * 0x20
                + *(int*)(carrier + 0x20);
            *(int*)(e + 0x24) =
                *(int*)(carrier + 0x24) + Winsys3d.GetCosTableEntry(spin) * 0x3c;
        }

        if (*(short*)(e + 0x10) > 0x3f) *(uint*)e = 0;
        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00476950 @ 0x00476950 — <b>l'entree 98 (<c>0x62</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Onze octets, decompilee dans
    /// <c>Blaze.c</c>. <b>Un relais</b>, la plus courte naissance du depot avec sa jumelle
    /// <see cref="EntityHandler_00476970"/> : elle lache un enfant et s'efface dans la meme image.
    ///
    /// <para>
    /// <b>Le sous-type de l'enfant n'est pas ecrit dans le code : il est dans l'entite.</b> Elle
    /// passe son propre <c>+0x0b</c> comme sous-type a <see cref="SpawnCastEntity_0047AAE0"/> —
    /// le champ qui, partout ailleurs, sert de parametre de dessin. Un seul creneau de table de
    /// sauts ouvre donc sur <b>n'importe quel</b> gestionnaire de la table <c>0x00551f30</c>,
    /// selon ce que l'engendreur a range dans <c>+0x0b</c>.
    /// </para>
    /// <para>
    /// C'est ce qui la rend impossible a suivre statiquement : le recensement de
    /// <c>GestionnairesEntites.md</c> ne peut pas dire ce qu'elle atteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476950(byte* e)
    {
        SpawnCastEntity_0047AAE0(e, e[0x0b], 0, -1);
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00476970 @ 0x00476970 — <b>l'entree 99 (<c>0x63</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Vingt-quatre octets, decompilee dans
    /// <c>Blaze.c</c>. <b>La jumelle de <see cref="EntityHandler_00476950"/> sur l'autre table</b>.
    ///
    /// <para>
    /// Meme relais, meme sous-type pris dans <c>+0x0b</c>, mais par
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> : l'enfant nait donc dans
    /// <c>0x005f2d40</c> et se resout par <see cref="s_StaticEntityHandlers_00552230"/>. Elle
    /// transmet en plus sa <b>position</b> (<c>+0x1c</c>) et son <b>atlas</b> (<c>+0x3c</c>), que
    /// la version <c>0x00551f30</c> n'a pas a passer — la copie des 0x27 dwords les porte deja.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476970(byte* e)
    {
        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), *(byte**)(e + 0x3c), e[0x0b], 0, -1);
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0047a950</c> — <b>l'entree 42 (<c>0x2a</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 121 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_0047A950.asm</c>.
    ///
    /// <para>
    /// <b>La bulle d'etat au-dessus d'une tete</b>, et elle ne vit que <b>deux images</b> : la
    /// premiere l'installe, la seconde efface son mot d'etat d'un seul <c>mov dword [esi],0</c>.
    /// Elle est donc reengendree en continu tant que l'etat dure, et non maintenue.
    /// </para>
    /// <para>
    /// <b>Elle se colle a son porteur</b> (<c>+0x6c</c>) : <c>X</c> et <c>Z</c> recopies tels
    /// quels, et <c>Y</c> releve de ce que <see cref="StatusBubbleHeight_00538BC0"/> rend pour ce
    /// porteur — la meme hauteur de bulle que le reste du projet lit deja. Le porteur n'est jamais
    /// verifie : s'il a disparu, la lecture part dans le vide, comme dans le binaire.
    /// </para>
    /// <para>
    /// <b>Elle respire au rythme du fondu general.</b> Son echelle vaut
    /// <c>sin(s_FadeCounter &#215; 512) / 2 + 0x1000</c> — <see cref="s_FadeCounter_005e9960"/>,
    /// le compteur global, et non un champ a elle : <b>toutes les bulles a l'ecran pulsent donc en
    /// phase</b>. Le meme mot est ecrit d'un coup sur <c>+0x28</c> et <c>+0x2a</c> (<c>shl 16</c>
    /// puis <c>or</c>), la teinte est blanche (<c>0x00ffffff</c> sur le dword <c>+0x38</c>, ce qui
    /// remet <c>+0x3b</c> a zero au passage), et <c>+0x2e</c> vaut <c>-10</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0047A950(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);

        // Le compteur de fondu global, partage par toutes les bulles a l'ecran.
        int wave = (int)Winsys3d.GetSinTableEntry((int)(s_FadeCounter_005e9960 << 9));
        short scale = (short)((wave >> 1) + 0x1000);

        *(uint*)(e + 0x38) = 0x00ffffff;   // blanc, et +0x3b remis a zero
        *(uint*)(e + 0x40) = 0;

        // `shl ecx,16 ; or ecx,eax` : les deux echelles ecrites d'un seul mot.
        *(uint*)(e + 0x28) = (uint)(((ushort)scale << 0x10) | (ushort)scale);

        *(short*)(e + 0x2e) = -10;

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) =
            (StatusBubbleHeight_00538BC0((uint*)carrier) << 0xc) + *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        // Deux images : la seconde efface le mot d'etat en entier, pas seulement le bit de vie.
        short age = *(short*)(e + 0x10);
        if (age >= 1) *(uint*)e = 0;
        *(short*)(e + 0x10) = (short)(age + 1);
    }

    /// <summary>
    /// Ghidra: <c>0x00482fa0</c> — <b>l'entree 0 (<c>0x00</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 79 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00482FA0.asm</c>.
    ///
    /// <para>
    /// <b>Le sous-type zero</b> : ce qu'une entite de la table <c>0x005f2d40</c> fait quand
    /// personne ne lui a donne de sous-type. Ce n'est pourtant pas une souche — le creneau porte un
    /// vrai corps, la ou <c>0x0047adc0</c> tient les creneaux vraiment vides.
    /// </para>
    /// <para>
    /// <b>Un eclat qui retrecit et disparait</b>, et rien d'autre : pas de deplacement, pas de
    /// teinte qui bouge, pas d'atlas qui defile. Il nait a l'echelle <c>0x1000</c> avec la teinte
    /// <c>0xf0</c> sur les trois canaux et l'atlas <see cref="s_Descriptors_005519A0"/>, puis perd
    /// <b>un quart de sa taille par image</b> (<c>taille - taille &gt;&gt; 2</c>, un <c>sar</c> nu
    /// sur seize bits) jusqu'a passer sous <c>0x100</c> — <b>onze images</b>, quel que soit ce qui
    /// l'a fait naitre.
    /// </para>
    /// <para>
    /// <b>Il s'efface d'un <c>mov dword [eax],0</c></b>, le mot d'etat entier, et non par le seul
    /// bit de vie : meme sortie que les relais <see cref="EntityHandler_00476950"/> et
    /// <see cref="EntityHandler_0047A950"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00482FA0(byte* e)
    {
        short age = *(short*)(e + 0x10);

        if (age == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005519A0();   // Ghidra: &DAT_005519a0
            *(uint*)(e + 0x28) = 0x10001000;               // les deux echelles d'un seul mot
            *(uint*)(e + 0x38) = 0x00f0f0f0;               // et la teinte de meme
        }
        else
        {
            // `sar si,2` puis `sub` : le quart tronque vers moins l'infini, sur seize bits.
            short scale = *(short*)(e + 0x28);
            scale = (short)(scale - (scale >> 2));
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
        }

        if (*(short*)(e + 0x28) < 0x100) *(uint*)e = 0;

        *(short*)(e + 0x10) = (short)(age + 1);
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551ba0</c> — <b>une plage de descripteurs d'atlas</b>, pas une
    /// animation : les huit enregistrements relevés pointent des <b>planches differentes</b>
    /// (<c>0x2020</c>, <c>0x1818</c>, <c>0x1010</c>, <c>0x0808</c>) et deux d'entre eux n'ont pas
    /// les octets <c>01 02</c> des autres. Chaque engendreur y designe son point de depart et borne
    /// lui-meme le parcours par <c>+0x1a</c>.
    ///
    /// <para>
    /// Depuis <see cref="EntityHandler_00475EA0"/>, <c>+0x1a</c> vaut <b>1</b> : seul
    /// l'enregistrement 0 est atteint. <c>0x00475D40</c>, encore a transcrire, en passe 3. La
    /// <b>longueur reelle de la table n'est donc pas etablie</b> — huit enregistrements sont releves
    /// parce que huit sont lisibles, pas parce qu'une borne les designe.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551BA0 =
    [
        0x60, 0x48, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551ba0
        0x18, 0x00, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551ba8
        0x30, 0x00, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551bb0
        0x08, 0x08, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551bb8
        0xb0, 0xa8, 0x10, 0x10, 0x00, 0x00, 0x10, 0x00,   // 0x00551bc0
        0xa0, 0xa8, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551bc8
        0x50, 0x58, 0x08, 0x08, 0x00, 0x00, 0x30, 0x02,   // 0x00551bd0
        0xb8, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551bd8
    ];

    private static byte* s_Descriptors00551BA0Storage;

    /// <summary>Les huit descripteurs en memoire native.</summary>
    private static byte* Descriptors00551BA0()
    {
        if (s_Descriptors00551BA0Storage == null)
        {
            s_Descriptors00551BA0Storage =
                (byte*)NativeMemory.Alloc((nuint)s_Descriptors_00551BA0.Length);
            for (int i = 0; i < s_Descriptors_00551BA0.Length; i++)
            {
                s_Descriptors00551BA0Storage[i] = s_Descriptors_00551BA0[i];
            }
        }

        return s_Descriptors00551BA0Storage;
    }

    /// <summary>
    /// Ghidra: FUN_00475ea0 @ 0x00475ea0 — <b>l'entree 190 (<c>0xbe</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>. Signalee
    /// en jeu par Vincent le 2026-08-29, en frappant un scarabee.
    ///
    /// <para>
    /// Encore une <b>naissance qui s'efface</b>, et le meme piege que <c>0xba</c> : le pere et
    /// l'enfant portent le meme numero <c>0xbe</c>, mais l'enfant est
    /// <see cref="EntityHandler_00475FC0"/>, entree 190 de la table <b>0x00552230</b>, ou
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> depose. Deux corps, un numero.
    /// </para>
    /// <para>
    /// Elle range son porteur dans <b>trois</b> champs (<c>+0x70</c>, <c>+0x74</c>, <c>+0x78</c>) —
    /// ses trois enfants ne lisent pas le meme — puis engendre en deux temps : d'abord un
    /// <c>0xbe</c> avec une echelle de <c>0x1f</c>, ensuite deux <c>0x68</c> avec une echelle de
    /// <c>0x3000</c> et une cible d'echelle de <c>0x800</c>. <b>Les deux reglages sont ecrits entre
    /// les appels</b>, donc les trois enfants ne recoivent pas la meme copie.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475EA0(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);

        *(short*)(e + 0x0a) = 7;

        *(byte**)(e + 0x70) = carrier;
        *(byte**)(e + 0x74) = carrier;
        *(byte**)(e + 0x78) = carrier;

        *(uint*)(e + 0x38) = 0xf0f0f0;
        *(uint*)(e + 0x44) = 0;

        uint height = (uint)StatusBubbleHeight_00538BC0((uint*)carrier);
        *(int*)(e + 0x48) = (int)((height & 0xfffffffe) << 0xb);

        *(uint*)(e + 0x4c) = 0;
        *(short*)(e + 0x12) = 0x1f;
        *(uint*)(e + 0x50) = 0;
        *(uint*)(e + 0x54) = 0;
        *(uint*)(e + 0x58) = 0;
        *(uint*)(e + 0x28) = 0x20002000;
        *(short*)(e + 0x14) = 0;
        *(short*)(e + 0x16) = 3;
        *(short*)(e + 0x1a) = 1;

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551BA0(), 0xbe, e[0x0b], -1);

        // Le second reglage, entre les appels : les deux 0x68 partent plus gros et visent 0x800.
        *(short*)(e + 0x12) = 0x3000;
        *(short*)(e + 0x14) = 0x800;

        SpawnCastEntity_0047AAE0(e, 0x68, e[0x0b], -1);
        SpawnCastEntity_0047AAE0(e, 0x68, e[0x0b], -1);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00475fc0</c> — <b>l'entree 190 (<c>0xbe</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 256 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00475FC0.asm</c>. C'est
    /// l'enfant de <see cref="EntityHandler_00475EA0"/>. <b>Elle n'appelle rien</b> : une feuille
    /// franche.
    ///
    /// <para>
    /// <b>Elle vole en repere relatif.</b> <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c> sont un
    /// <b>decalage</b> par rapport a une ancre <c>+0x70</c>, et <c>+0x1c</c>/<c>+0x20</c>/<c>+0x24</c>
    /// sont recalcules a chaque image comme « ancre + decalage ». La vitesse
    /// (<c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>) perd <c>1 / 2^(+0x12)</c> par image et s'ajoute au
    /// decalage : un jet qui s'essouffle en suivant ce a quoi il est accroche.
    /// </para>
    /// <para>
    /// <b>Sans ancre elle s'ancre a elle-meme</b> : si <c>+0x70</c> est nul, la premiere image
    /// verse la position courante dans le decalage, met la position a zero et prend <c>e</c> pour
    /// ancre. Le calcul devient alors l'identite — c'est la facon dont l'original ecrit « repere
    /// absolu » sans dupliquer le code.
    /// </para>
    /// <para>
    /// L'echelle rejoint <c>+0x14</c> par pas de <c>1 / 2^(+0x16)</c>, l'image d'atlas
    /// (<c>+0x40</c>) tourne modulo <c>+0x1a</c>, et la teinte perd <c>0x101010</c> par image :
    /// partie du <c>0xf0f0f0</c> que le pere pose, cela fait <b>seize images</b> pile.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475FC0(byte* e)
    {
        byte* anchor = *(byte**)(e + 0x70);

        if (anchor == null)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                // La position courante devient le decalage, une seule fois.
                *(int*)(e + 0x44) += *(int*)(e + 0x1c);
                *(int*)(e + 0x48) += *(int*)(e + 0x20);
                *(int*)(e + 0x4c) += *(int*)(e + 0x24);
                *(short*)(e + 0x10) = 1;
            }

            *(int*)(e + 0x1c) = 0;
            *(int*)(e + 0x20) = 0;
            *(int*)(e + 0x24) = 0;
            anchor = e;
        }

        // Ghidra: LAB_00476009 — l'amortissement est un decalage, pas une division constante.
        int damping = *(short*)(e + 0x12);

        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        vx -= vx >> damping;
        vy -= vy >> damping;
        vz -= vz >> damping;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;

        *(int*)(e + 0x44) += vx;
        *(int*)(e + 0x48) += vy;
        *(int*)(e + 0x4c) += vz;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c) + *(int*)(e + 0x44);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + *(int*)(e + 0x48);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24) + *(int*)(e + 0x4c);

        short current = *(short*)(e + 0x28);
        short size = (short)(((*(short*)(e + 0x14) - current) >> e[0x16]) + current);
        *(short*)(e + 0x28) = size;
        *(short*)(e + 0x2a) = size;

        e[0x40]++;
        if ((short)(ushort)e[0x40] >= *(short*)(e + 0x1a)) e[0x40] = 0;

        // Ghidra: LAB_0047609c — le test precede la soustraction, d'ou seize images et non quinze.
        uint tint = *(uint*)(e + 0x38);
        if ((tint & 0xffffff) == 0)
        {
            *(uint*)e = 0;
            return;
        }

        *(uint*)(e + 0x38) = tint + 0xffefeff0;
    }

    /// <summary>
    /// Ghidra: <c>0x0047bd00</c> — <b>l'entree 104 (<c>0x68</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>, 239 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_0047BD00.asm</c>. C'est ce
    /// que <see cref="EntityHandler_00475EA0"/> lache <b>par deux</b>.
    ///
    /// <para>
    /// <b>Une bulle qui enfle et blanchit puis s'eteint en seize images.</b> A la naissance elle
    /// prend l'echelle <c>+0x12</c> sur les trois axes et <b>une orientation entierement tiree</b> —
    /// trois appels a <c>rand()</c> pour <c>+0x30</c>, <c>+0x32</c>, <c>+0x34</c> —, puis chaque
    /// image ses trois echelles rejoignent <c>+0x14</c> <b>par quarts</b>.
    /// </para>
    /// <para>
    /// <b>Sa teinte est un sinus, pas une rampe</b> : <c>sin(image &lt;&lt; 7) × 255 / 4096</c>,
    /// replie sur les trois canaux. Elle s'allume donc et s'eteint d'elle-meme sur la duree de vie,
    /// sans compteur separe.
    /// </para>
    /// <para>
    /// Elle suit son ancre <c>+0x78</c> en <c>X</c> et <c>Z</c>, mais sa hauteur est recalculee a
    /// chaque image : celle de l'ancre plus la bulle d'etat de <c>+0x74</c>, arrondie au pair. Et
    /// elle <b>tourne sur elle-meme</b>, <c>+0x32</c> de <c>0x60</c> et <c>+0x34</c> de <c>0x80</c>
    /// par image.
    /// </para>
    /// </summary>
    private static void EntityHandler_0047BD00(byte* e)
    {
        byte* anchor = *(byte**)(e + 0x78);

        if (*(short*)(e + 0x10) == 0)
        {
            int start = *(short*)(e + 0x12);

            *(short*)(e + 0x2c) = (short)start;
            *(uint*)(e + 0x28) = (uint)((start << 0x10) | (start & 0xffff));

            *(short*)(e + 0x30) = (short)CrtRand();
            *(short*)(e + 0x32) = (short)CrtRand();
            *(short*)(e + 0x34) = (short)CrtRand();

            e[0x04] = 0x1d;
        }
        else
        {
            // Ghidra: LAB_0047bd44 — les trois echelles rejoignent la cible par quarts.
            int target = *(short*)(e + 0x14);

            short s28 = *(short*)(e + 0x28);
            short s2a = *(short*)(e + 0x2a);
            short s2c = *(short*)(e + 0x2c);

            *(short*)(e + 0x28) = (short)(((target - s28) >> 2) + s28);
            *(short*)(e + 0x2a) = (short)(((target - s2a) >> 2) + s2a);
            *(short*)(e + 0x2c) = (short)(((target - s2c) >> 2) + s2c);
        }

        // Ghidra: LAB_0047bd82 — la teinte suit un sinus de l'age, repliee sur les trois canaux.
        int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 7);
        int level = (wave << 8) - wave >> 0xc;
        *(uint*)(e + 0x38) = (uint)((((level << 8) | level) << 8) | level);

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        uint height = (uint)StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));

        *(short*)(e + 0x32) += 0x60;
        *(short*)(e + 0x34) += 0x80;

        *(int*)(e + 0x20) = (int)((height & 0xfffffffe) << 0xb) + *(int*)(anchor + 0x20);

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 0x10) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004034a0 @ 0x004034a0 — <b>l'entree 90 de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>, celle de la passe
    /// <see cref="TickEntityTable_005edee0"/>. C'est l'<b>etincelle</b> que
    /// <see cref="EntityHandler_00402FC0"/> lache par six autour de sa victime — la levee signalee
    /// en jeu le 2026-08-29, une image apres celle du gestionnaire pere.
    ///
    /// <para>
    /// <b>Elle decrit une spirale sur une sphere.</b> <c>+0x14</c> est l'angle vertical,
    /// <c>+0x12</c> l'angle horizontal, <c>+0x16</c> le rayon ; la position est recalculee a chaque
    /// image a partir des trois, autour de l'ancre <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> figee a
    /// la naissance. Le rayon croit de <c>+0x5c</c> (5 a 9) par image et l'angle horizontal de
    /// <c>+0x48</c> : l'etincelle s'ecarte en tournant.
    /// </para>
    /// <para>
    /// <b>Elle meurt de retrecir.</b> Les trois echelles <c>+0x28</c>/<c>+0x2a</c>/<c>+0x2c</c>
    /// partent d'un tirage sur <c>0x400</c> biaise par le <i>germe</i> que le pere a laisse en
    /// <c>+0x14</c>, perdent 100 par image, et quand la premiere passe sous zero les trois sont
    /// remises a zero et le bit de vie <c>0x80000000</c> tombe. Aucune duree n'est comptee : c'est
    /// la taille qui fait l'horloge.
    /// </para>
    /// <para>
    /// <b>Le germe sert trois fois avant d'etre ecrase</b> — les trois echelles, l'elevation
    /// initiale <c>+0x20</c> et le rayon <c>+0x16</c> — puis <c>+0x14</c> devient l'angle vertical
    /// courant, tire entre 200 et 999. Deux roles pour un champ, comme <c>+0x10</c> chez les
    /// routines d'intelligence.
    /// </para>
    /// <para>
    /// La derniere ligne l'eclaircit de dix par canal a chaque image, par
    /// <see cref="BrightenEntityTint_00404DE0"/>, chaque etincelle ayant sa propre teinte de depart.
    /// </para>
    /// </summary>
    private static void EntityHandler_004034A0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[4] = 0x12;                                    // le modele de l'etincelle
            e[0x0a] = 3;
            e[0x0b] = 1;

            // Le germe pose par l'engendreur, lu quatre fois avant d'etre ecrase plus bas.
            short seed = *(short*)(e + 0x14);

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x400 + seed * 10);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x400 + seed * 10);
            *(short*)(e + 0x2c) = (short)(CrtRand() % 0x400 + seed * 10);

            // Ghidra: `and al,0FCh` sur le germe sign-etendu — les deux bits bas tombent, rien
            // d'autre : c'est un `& ~3` ecrit sur l'octet bas.
            *(int*)(e + 0x20) += (seed & ~3) << 0x0a;
            *(short*)(e + 0x16) = (short)(seed >> 1);

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);

            // L'ancre : la position de naissance, autour de laquelle la spirale se dessine.
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x14) = (short)(CrtRand() % 800 + 200);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            *(int*)(e + 0x5c) = CrtRand() % 5 + 5;

            *(int*)(e + 0x44) = CrtRand() % 600 - 300;
            *(int*)(e + 0x48) = CrtRand() % 600 - 300;
            *(int*)(e + 0x4c) = CrtRand() % 600 - 300;

            e[0x38] = (byte)(CrtRand() % 200);
            e[0x39] = (byte)(CrtRand() % 0x32);
            e[0x3a] = (byte)(CrtRand() % 0x32);
        }

        // Ghidra: LAB_00403601 — la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) != 1) return;

        short pitch = *(short*)(e + 0x14);
        short radius = *(short*)(e + 0x16);
        short yaw = *(short*)(e + 0x12);

        // Le produit du rayon par le cosinus est ramene de douze bits ; celui par le sinus ou le
        // cosinus horizontal ne l'est PAS. L'asymetrie est dans le binaire.
        int ring = (Winsys3d.GetCosTableEntry(pitch) * radius) >> 0xc;

        *(int*)(e + 0x1c) = ring * (int)Winsys3d.GetSinTableEntry(yaw) + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = ring * Winsys3d.GetCosTableEntry(yaw) + *(int*)(e + 0x58);
        *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(pitch) * radius + *(int*)(e + 0x54);

        *(short*)(e + 0x16) += *(short*)(e + 0x5c);
        *(ushort*)(e + 0x32) = (ushort)((*(short*)(e + 0x48) + *(short*)(e + 0x32)) & 0xfff);

        *(short*)(e + 0x28) -= 100;
        *(short*)(e + 0x2a) -= 100;
        *(short*)(e + 0x2c) -= 100;

        *(ushort*)(e + 0x30) = (ushort)((*(short*)(e + 0x44) + *(short*)(e + 0x30)) & 0xfff);
        *(ushort*)(e + 0x34) = (ushort)((*(short*)(e + 0x4c) + *(short*)(e + 0x34)) & 0xfff);

        if (*(short*)(e + 0x28) < 0)
        {
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(uint*)e &= 0x7fffffff;
        }

        BrightenEntityTint_00404DE0(e, 10, 10, 10);
    }

    /// <summary>
    /// Ghidra: FUN_00404de0 @ 0x00404de0 — <b>eclaircir la teinte d'une entite</b> : les trois
    /// octets <c>+0x38</c>/<c>+0x39</c>/<c>+0x3a</c> augmentent des trois arguments, bornes a
    /// <c>0..0xff</c>. La borne basse ne peut pas se declencher avec des arguments positifs ; elle
    /// est transcrite parce qu'elle est dans le corps.
    /// </summary>
    private static void BrightenEntityTint_00404DE0(byte* e, short r, short g, short b)
    {
        r = (short)(r + e[0x38]);
        g = (short)(g + e[0x39]);
        b = (short)(b + e[0x3a]);

        if (r < 0) r = 0;
        if (r > 0xff) r = 0xff;
        if (g < 0) g = 0;
        if (g > 0xff) g = 0xff;
        if (b < 0) b = 0;
        if (b > 0xff) b = 0xff;

        e[0x38] = (byte)r;
        e[0x39] = (byte)g;
        e[0x3a] = (byte)b;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f118</c> — <b>le descripteur d'atlas de la poussiere</b>, que
    /// <see cref="EntityHandler_00403250"/> pose en <c>+0x3c</c> de chaque grain. Meme forme
    /// d'enregistrement de huit octets que <see cref="s_LootDescriptors_005646E0"/>.
    ///
    /// <para>
    /// Un seul enregistrement : le grain ne fait jamais tourner son indice d'image — il pose
    /// <c>+0x40 = 0</c> a la naissance et n'y retouche plus, contrairement a
    /// <see cref="EntityHandler_004474C0"/> qui en a trois.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DustDescriptors_0054F118 =
    [
        0x00, 0xa8, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x0054f118
    ];

    private static byte* s_DustDescriptorStorage;

    /// <summary>Le descripteur en memoire native : le grain le garde par pointeur
    /// (<c>+0x3c</c>).</summary>
    private static byte* DustDescriptor()
    {
        if (s_DustDescriptorStorage == null)
        {
            s_DustDescriptorStorage = (byte*)NativeMemory.Alloc((nuint)s_DustDescriptors_0054F118.Length);
            for (int i = 0; i < s_DustDescriptors_0054F118.Length; i++)
            {
                s_DustDescriptorStorage[i] = s_DustDescriptors_0054F118[i];
            }
        }

        return s_DustDescriptorStorage;
    }

    /// <summary>
    /// Ghidra: FUN_00403250 @ 0x00403250 — <b>l'entree 56 de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, celle de la passe
    /// <see cref="TickEntityTable_005f2d40"/>. C'est le <b>grain de poussiere</b> que
    /// <see cref="EntityHandler_00402FC0"/> lache par quatre, puis par deux une image sur deux.
    ///
    /// <para>
    /// <b>C'est la sœur de <see cref="EntityHandler_004034A0"/></b>, l'etincelle : meme spirale sur
    /// une sphere, meme germe lu quatre fois en <c>+0x14</c> avant d'etre ecrase, meme ancre figee
    /// a la naissance. Cinq nombres et une sortie les separent.
    /// </para>
    /// <list type="bullet">
    /// <item>ses echelles partent de <c>0xc00</c> plus un tirage sur <c>0x200</c> (l'etincelle :
    /// un tirage sur <c>0x400</c> sans plancher) et perdent <b>300</b> par image au lieu de
    /// 100 ;</item>
    /// <item>son rayon croit de 6 a 10 par image au lieu de 5 a 9 ;</item>
    /// <item>elle <b>s'assombrit</b> de trente par canal a chaque image au lieu de s'eclaircir de
    /// dix, et sa teinte de depart est ecrite en octet <i>signe</i> negatif — soit 0xc8..0xe5 une
    /// fois relue non signee par <see cref="BrightenEntityTint_00404DE0"/> ;</item>
    /// <item><b>elle meurt d'etre eteinte</b>, quand les trois canaux tombent a zero, et non
    /// d'avoir retreci ;</item>
    /// <item>elle ne fait pas tourner ses trois angles, alors qu'elle en tire les vitesses : les
    /// champs <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c> sont remplis et jamais relus par ce
    /// corps.</item>
    /// </list>
    /// </summary>
    private static void EntityHandler_00403250(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = DustDescriptor();          // Ghidra: &DAT_0054f118
            e[0x40] = 0;
            *(short*)(e + 0x10) = 1;
            e[0x0a] = 0x0d;
            e[0x0b] = 1;

            short seed = *(short*)(e + 0x14);

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x200 + 0xc00 + seed * 10);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x200 + 0xc00 + seed * 10);
            *(short*)(e + 0x2c) = (short)(CrtRand() % 0x200 + 0xc00 + seed * 10);

            *(int*)(e + 0x20) += (seed & ~3) << 0x0a;
            *(short*)(e + 0x16) = (short)(seed >> 1);

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);

            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x14) = (short)(CrtRand() % 800 + 200);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            *(int*)(e + 0x5c) = CrtRand() % 5 + 6;

            // Remplis et jamais relus par ce corps — l'etincelle, elle, s'en sert.
            *(int*)(e + 0x44) = CrtRand() % 600 - 300;
            *(int*)(e + 0x48) = CrtRand() % 600 - 300;
            *(int*)(e + 0x4c) = CrtRand() % 600 - 300;

            // Ghidra: `(char)(rand % 0x1e) + -0x38` — un octet SIGNE negatif, que
            // BrightenEntityTint_00404DE0 relit non signe : la teinte de depart vaut donc
            // 0xc8..0xe5, et l'assombrissement la ramene a zero en sept ou huit images.
            e[0x38] = (byte)(CrtRand() % 0x1e - 0x38);
            e[0x39] = (byte)(CrtRand() % 0x1e - 0x38);
            e[0x3a] = (byte)(CrtRand() % 0x1e - 0x38);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short pitch = *(short*)(e + 0x14);
        short radius = *(short*)(e + 0x16);
        short yaw = *(short*)(e + 0x12);

        int ring = (Winsys3d.GetCosTableEntry(pitch) * radius) >> 0xc;

        *(int*)(e + 0x1c) = ring * (int)Winsys3d.GetSinTableEntry(yaw) + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = ring * Winsys3d.GetCosTableEntry(yaw) + *(int*)(e + 0x58);
        *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(pitch) * radius + *(int*)(e + 0x54);

        *(short*)(e + 0x28) -= 300;
        *(short*)(e + 0x2a) -= 300;
        *(short*)(e + 0x2c) -= 300;

        *(short*)(e + 0x16) += *(short*)(e + 0x5c);

        BrightenEntityTint_00404DE0(e, -0x1e, -0x1e, -0x1e);

        if (e[0x38] == 0 && e[0x39] == 0 && e[0x3a] == 0)
        {
            *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f7a8</c> — <b>les quatre images de l'eclaboussure</b>, huit octets
    /// chacune, relevees dans le binaire. Quatre tuiles de <c>8 x 8</c> aux quatre coins d'un carre
    /// de <c>0x10</c> de cote, a partir de <c>(8, 8)</c> sur la planche.
    ///
    /// <para>
    /// <b>Quatre, et le corps le dit</b> : <see cref="EntityHandler_0040F7F0"/> avance
    /// <c>+0x40</c> une image sur deux et eteint l'entite quand il atteint 4. La donnee borne la
    /// plage des deux cotes — celle qui precede (<c>0x0054f778</c>..<c>0x0054f7a8</c>, les six
    /// images de <c>FUN_0040f8e0</c>) est en <c>0x18 x 0x18</c>, et <c>DAT_0054f7c8</c>, symbole
    /// distinct dans <c>Blaze.c</c>, ouvre la suivante en <c>0x10 x 0x10</c>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_SplashDescriptors_0054F7A8 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f7a8
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f7b0
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f7b8
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f7c0
    ];

    private static byte* s_SplashDescriptors0054F7A8Storage;

    /// <summary>Les quatre images en memoire native : l'entite les garde par pointeur
    /// (<c>+0x3c</c>) et choisit la sienne par <c>+0x40</c>.</summary>
    private static byte* Descriptors0054F7A8()
        => NativeCopy(s_SplashDescriptors_0054F7A8, ref s_SplashDescriptors0054F7A8Storage);

    /// <summary>
    /// L'idiome x86 du <b>reste signe par une puissance de deux</b> :
    /// <c>cdq ; xor ; sub ; and &lt;masque&gt; ; xor ; sub</c>, c'est-a-dire
    /// <c>signe(v) * (abs(v) &amp; masque)</c>. Le compilateur de 1998 l'emet pour un <c>%</c> par
    /// une puissance de deux, qui doit garder le signe du dividende, et Ghidra le rend tel quel
    /// dans les corps voisins (<c>FUN_0040f650</c>, <c>FUN_0040f8e0</c>).
    ///
    /// <para>Les appelants le nourrissent de <c>rand()</c>, qui ne rend jamais de negatif : le
    /// masque nu suffirait. L'idiome est neanmoins conserve — c'est ce que le binaire calcule, et
    /// le reduire prêterait au corps une hypothese qu'il ne fait pas.</para>
    /// </summary>
    private static int SignedMask(int value, int mask)
    {
        int sign = value >> 0x1f;
        return ((((value ^ sign) - sign) & mask) ^ sign) - sign;
    }
}
