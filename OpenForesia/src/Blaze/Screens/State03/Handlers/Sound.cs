// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES EFFETS DE PROXIMITE — la famille 0x0045xxxx qui se declenche selon la distance.
//
// Quatre gestionnaires, deux mecanismes : publier un effet de CAMERA pres d'une entite
// (`TryPublishCameraEffectNearEntity`) et jouer un SON pres d'une entite
// (`TryPlaySoundNearEntity`). Dans les deux cas le corps ne fait que mesurer une distance et
// decider s'il declenche - le travail est ailleurs.
//
// Ce sont des gestionnaires de DECOR : ils sont poses sur la carte par le relevé de placement,
// pas engendres par une action du joueur.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_004508D0 @ 0x004508d0 — <b>absente de <c>Blaze.c</c></b>, transcrite depuis le
    /// desassemblage (197 octets, 0x004508d0..0x00450994). C'est l'entree <b>8</b> de la table
    /// 0x0056adc8, le premier gestionnaire d'entite que la carte du monde atteint reellement.
    ///
    /// <para>
    /// Une <b>animation de 0x28 images</b>, comptee dans le <c>short</c> de <c>+0x10</c>. Au
    /// premier tour (compteur nul) elle pose le bit <c>0x2000</c> de l'etat, amorce a 15 la valeur
    /// de <c>+0x54</c>, et allume un bit de <c>DAT_00571540</c> choisi par le <c>short</c> de
    /// <c>+0x14</c> : c'est une <b>table de sauts a cinq entrees</b> (0x00450998 dans le binaire),
    /// dont les cinq cibles ne different que par la constante OU-ee — 1, 2, 4, 8, 0x10. Elle se
    /// resume donc a <c>1 &lt;&lt; (kind - 1)</c>, et c'est ecrit ainsi plutot qu'en cinq branches
    /// identiques. <c>DAT_00571540</c> est justement le mot que <c>Init_004502d0</c> remet a zero
    /// au chargement de zone.
    /// </para>
    /// <para>
    /// Ensuite la valeur de <c>+0x54</c> <b>enfle puis retombe</b> : elle gagne un huitieme d'
    /// elle-meme tant que le compteur est sous 12, et en perd un huitieme au-dela. Elle est ajoutee
    /// a chaque image au <c>short</c> de <c>+0x32</c> — un angle qui accelere puis ralentit.
    /// </para>
    /// <para>
    /// A la 0x28e image, l'entite pose <c>0x40002000</c> et rend la main <b>sans</b> demander l'
    /// effet de camera : c'est sa fin.
    /// </para>
    /// </summary>
    private static void EntityHandler_004508D0(uint* entity)
    {
        if ((entity[0] & 0x40000000) != 0) return;

        byte* e = (byte*)entity;

        if (*(short*)(e + 0x10) == 0)
        {
            // Ghidra rend `or ch,20h` : ch est l'octet 1 d'ECX, donc le bit 0x2000 du mot d'etat.
            entity[0] |= 0x2000;
            *(int*)(e + 0x54) = 0x0f;

            int kind = *(short*)(e + 0x14) - 1;
            if ((uint)kind <= 4)
            {
                *Mem.EntityState(0x00571540) |= 1 << kind;
            }
        }

        short frame = *(short*)(e + 0x10);
        int rate = *(int*)(e + 0x54);
        *(int*)(e + 0x54) = frame > 0xc ? rate - (rate >> 3) : (rate >> 3) + rate;

        short next = (short)(frame + 1);
        *(short*)(e + 0x10) = next;
        if (next == 0x28)
        {
            entity[0] |= 0x40002000;
            return;
        }

        *(short*)(e + 0x32) += (short)*(int*)(e + 0x54);
        TryPublishCameraEffectNearEntity(entity, 1, 1, 1, 0, 0);
    }

    /// <summary>
    /// Ghidra: FUN_0041be60 @ 0x0041be60 — publie jusqu'a trois valeurs dans le bloc
    /// <c>DAT_005de0a8</c> / <c>ac</c> / <c>b0</c>, mais <b>seulement si l'entite est a moins de
    /// 1500 unites de la camera</b>. Les trois mots sont remis a zero en tete de chaque image par
    /// le prologue de <see cref="RunWorldMapTick"/> : c'est donc une requete valable une image.
    /// La valeur sentinelle <c>999</c> veut dire « ne touche pas a celui-la ».
    ///
    /// <para>
    /// <b>Ghidra s'est trompe de signature, et le desassembleur tranche.</b> La pseudo-C montre
    /// trois parametres et se sert de <c>param_1</c> a la fois comme entite (<c>param_1 + 0x1c</c>)
    /// et comme premiere valeur publiee — ce qui ne peut pas etre. Le site d'appel empile
    /// <b>six</b> arguments (<c>add esp,18h</c>), et le corps lit <c>[esp+10h]</c>,
    /// <c>[esp+14h]</c> et <c>[esp+18h]</c> apres deux <c>push</c>, soit les arguments 2, 3 et 4.
    /// Les arguments 5 et 6 ne sont lus <b>nulle part</b> dans le corps ; ils sont conserves dans
    /// la signature parce que l'appelant les empile.
    /// </para>
    /// <para>
    /// <b>Ce decompte d'offsets ne tient que parce que <c>Ordinal_442</c> est stdcall</b> : la
    /// somme des carres empilee juste avant l'appel est depilee par la callee, sans
    /// <c>add esp,4</c>. En cdecl, <c>[esp+10h]</c> designerait l'argument 1. Verifie au
    /// desassembleur le 2026-08-24 sur les trois sœurs — voir
    /// <c>Documentation/DLL/winsys3d.md</c>. Ici les trois valeurs sont lues en <b>double mot</b>
    /// (<c>mov eax,[esp+10h]</c>), la ou <see cref="TryPlaySoundNearEntity"/> les lit en
    /// <c>movsx word</c> : les deux sœurs ne partagent pas les largeurs de leurs arguments.
    /// </para>
    /// </summary>
    /// <returns>1 si l'entite etait assez proche pour que la requete porte, 0 sinon.</returns>
    private static ushort TryPublishCameraEffectNearEntity(
        uint* entity, int first, int second, int third, int unused5, int unused6)
    {
        int* camera = *(int**)Mem.Entity(0x005edebc);
        byte* e = (byte*)entity;

        int dx = (camera[0] >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x5dc) return 0;

        if (first != 999) *Mem.Camera(0x005de0a8) = first;
        if (second != 999) *Mem.Camera(0x005de0ac) = second;
        if (third != 999) *Mem.Camera(0x005de0b0) = third;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_004509b0 @ 0x004509b0 — l'entree <b>9</b> de la table 0x0056adc8, et la
    /// <b>jumelle</b> de <see cref="EntityHandler_004508D0"/> : meme animation de 0x28 images, meme
    /// enflure de la valeur de <c>+0x54</c>, meme table de sauts a cinq entrees sur
    /// <c>DAT_00571540</c>. Deux differences, et elles vont ensemble :
    ///
    /// <list type="bullet">
    /// <item>l'angle de <c>+0x32</c> est <b>retranche</b> au lieu d'etre ajoute — les deux tournent
    /// en sens contraire ;</item>
    /// <item>elle joue un son au premier tour au lieu de demander l'effet de camera a chaque
    /// tour.</item>
    /// </list>
    ///
    /// <para>
    /// <b>L'identifiant du son ne se lit pas dans la pseudo-C.</b> Ghidra rend
    /// <c>CONCAT22((short)(DAT_005e97cc &gt;&gt; 0x10), …)</c>, ce qui n'a aucun sens comme valeur : c'est
    /// sa facon de modeliser un <c>movzx ax, byte […]</c> qui laisse la moitie haute d'EAX
    /// perimee. Le desassemblage (0x004509d5..0x004509f4) donne la vraie lecture — une <b>double
    /// indirection</b> depuis <c>ctx + 0xa8</c>, dont on prend le premier octet — et confirme que
    /// l'appele ne lit que la moitie basse.
    /// </para>
    /// </summary>
    private static void EntityHandler_004509B0(uint* entity)
    {
        if ((entity[0] & 0x40000000) != 0) return;

        byte* e = (byte*)entity;

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x54) = 0x0f;
            entity[0] |= 0x2000;

            ushort soundId = **(byte**)*(byte**)(s_Unknown_005e97cc + 0xa8);
            TryPlaySoundNearEntity(entity, (short)soundId, 0x200, 0x1600, 0, 0);

            // La meme table de sauts a cinq entrees que EntityHandler_004508D0 (ici en 0x00450a80),
            // dont les cibles ne different que par la constante OU-ee.
            int kind = *(short*)(e + 0x14) - 1;
            if ((uint)kind <= 4)
            {
                *Mem.EntityState(0x00571540) |= 1 << kind;
            }
        }

        int rate = *(int*)(e + 0x54);
        *(int*)(e + 0x54) = *(short*)(e + 0x10) < 0xd ? (rate >> 3) + rate : rate - (rate >> 3);

        short next = (short)(*(short*)(e + 0x10) + 1);
        *(short*)(e + 0x10) = next;
        if (next == 0x28)
        {
            entity[0] |= 0x40002000;
            return;
        }

        *(short*)(e + 0x32) -= (short)*(int*)(e + 0x54);
    }

    /// <summary>
    /// Ghidra: FUN_00468080 @ 0x00468080 — <b>le jumeau sonore</b> de
    /// <see cref="TryPublishCameraEffectNearEntity"/> : meme calcul de distance a la camera, meme
    /// signature perdue par Ghidra, mais un rayon de 2000 au lieu de 1500 et un son a la place des
    /// trois mots publies.
    ///
    /// <para>
    /// Comme sa jumelle, <b>la pseudo-C se sert de <c>param_1</c> a la fois comme entite et comme
    /// identifiant de son</b>, ce qui est impossible. Le desassemblage donne les vrais arguments :
    /// le site d'appel en empile <b>six</b> (<c>add esp,18h</c>) et le corps lit
    /// <c>[esp+14h]</c>, <c>[esp+18h]</c> et <c>[esp+1ch]</c> apres trois <c>push</c>, soit les
    /// arguments 2, 3 et 4, tous trois en <c>movsx word</c>. Les arguments 5 et 6 ne sont lus
    /// nulle part.
    /// </para>
    /// <para>
    /// <b>Ce qui rend ces offsets lisibles est la convention de <c>Ordinal_442</c></b>, verifiee au
    /// desassembleur le 2026-08-24 : elle est <b>stdcall</b>, aucun de ses sites d'appel ne fait
    /// <c>add esp,4</c>. La somme des carres empilee est donc depilee par la callee, et
    /// <c>[esp+14h]</c> designe l'argument 2. En cdecl, les memes offsets designeraient les
    /// arguments 1, 2 et 3 — un cran d'ecart sur toute la famille. Voir
    /// <c>Documentation/DLL/winsys3d.md</c>.
    /// </para>
    /// <para>
    /// Le gain est <b>double</b> avant d'etre passe (<c>shl edx,1</c>), et la position sonore est
    /// celle de l'entite (<c>+0x1c</c>).
    /// </para>
    /// </summary>
    /// <returns>1 si l'entite etait assez proche pour que le son parte, 0 sinon.</returns>
    private static ushort TryPlaySoundNearEntity(
        uint* entity, short soundId, short pitch, short gain, int unused5, int unused6)
    {
        int* camera = *(int**)Mem.Entity(0x005edebc);
        byte* e = (byte*)entity;

        int dx = (camera[0] >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 2000) return 0;

        Aud.PlayPositionalSound(soundId, pitch, gain << 1, (int*)(e + 0x1c));
        return 1;
    }

    /// <summary>
    /// Ghidra: <c>DAT_00571540</c> — <b>le registre des types d'effet de zone actifs</b>, cinq bits
    /// dans l'octet bas d'un dword de l'arene d'etat d'entite. <c>Init_004502d0</c>
    /// (<c>Engine/Resources.cs</c>) le remet a zero a chaque chargement de zone.
    ///
    /// <para>
    /// Trois gestionnaires s'en servent et le cycle est maintenant complet :
    /// <see cref="EntityHandler_004508D0"/> et <see cref="EntityHandler_004509B0"/> <b>allument</b>
    /// le bit de leur type au premier tour, <see cref="EntityHandler_00450AA0"/> l'<b>eteint</b>
    /// quand son effet s'acheve — et se sert du meme bit, tant qu'il est allume, comme feu vert
    /// pour avancer.
    /// </para>
    /// <para>
    /// Le binaire n'ecrit jamais ce registre autrement que par les cinq branches d'une table de
    /// sauts dont les cibles ne different que par la constante (1, 2, 4, 8, 0x10) : c'est
    /// <c>1 &lt;&lt; (type - 1)</c>, ecrit ainsi plutot qu'en cinq branches identiques.
    /// </para>
    /// </summary>
    private static byte* AreaEffectKindBits => Mem.EntityStateBytes(0x00571540);

    /// <summary>
    /// Ghidra: FUN_00450AA0 @ 0x00450aa0 — l'entree <b>10</b> de la table 0x0056adc8, <b>absente de
    /// <c>Blaze.c</c></b> et transcrite depuis le desassemblage (0x00450aa0..0x00450bb1, plus la
    /// table de sauts de 0x00450bb4).
    ///
    /// <para>
    /// Une machine a quatre phases dans le <c>short</c> de <c>+0x10</c>, qui fait <b>monter puis
    /// tenir</b> un niveau range a <c>+0x28</c> :
    /// </para>
    /// <list type="number">
    /// <item><b>0</b> — amorcage : le niveau part de zero et la vitesse de montee est recopiee de
    /// <c>+0x16</c> dans <c>+0x12</c> ;</item>
    /// <item><b>1</b> — montee : le niveau gagne la vitesse a chaque image, jusqu'a saturer a
    /// <c>0x1000</c> ; <c>+0x12</c> est alors remis a zero et sert de compteur ;</item>
    /// <item><b>2</b> — maintien pendant 30 images ;</item>
    /// <item><b>3</b> — fini : l'entite pose <c>0x40000000</c> et <b>rend son bit de type</b>.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Les cinq gardes du binaire se resument a une seule ligne.</b> Ghidra n'a pas decompile ce
    /// corps, mais le desassemblage montre cinq blocs <c>cmp cx,N ; jne ; test dl,M ; je</c> — un
    /// par type — ou <c>M</c> vaut successivement 1, 2, 4, 8, 0x10. Le deuxieme s'ecrit
    /// <c>test cl,dl</c> et non <c>test dl,2</c> : le compilateur a reutilise <c>CL</c>, qui porte
    /// deja le 2 de la comparaison. C'est le meme test que les quatre autres, pas un cas
    /// particulier. Tout se ramene donc a <c>bits &amp; (1 &lt;&lt; (type - 1))</c> — et un type hors de
    /// 1..5 ne rencontre <b>aucune</b> garde et passe.
    /// </para>
    /// <para>
    /// La garde est <b>avant</b> l'avance de la machine : tant que le bit de type est eteint,
    /// l'effet est fige plutot qu'annule.
    /// </para>
    /// </summary>
    private static void EntityHandler_00450AA0(uint* entity)
    {
        byte* e = (byte*)entity;

        if (*(short*)(e + 0x10) == 0)
        {
            short speed = *(short*)(e + 0x16);
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = speed;
        }

        short kind = *(short*)(e + 0x14);
        byte bits = *AreaEffectKindBits;

        if (kind >= 1 && kind <= 5 && (bits & (1 << (kind - 1))) == 0) return;

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x28) += *(short*)(e + 0x12);
            if (*(short*)(e + 0x28) >= 0x1000)
            {
                *(short*)(e + 0x28) = 0x1000;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) < 0x1e) return;

        *(short*)(e + 0x10) = 3;
        entity[0] |= 0x40000000;

        // La table de sauts de 0x00450bb4 : cinq cibles qui ne different que par le
        // `and al,0FEh` / `0FDh` / `0FBh` / `0F7h` / `0EFh` final.
        int index = kind - 1;
        if ((uint)index <= 4)
        {
            *AreaEffectKindBits &= (byte)~(1 << index);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00450D30 @ 0x00450d30 — l'entree <b>18</b> de la table 0x0056adc8, <b>absente de
    /// <c>Blaze.c</c></b> et transcrite depuis le desassemblage (1 087 octets,
    /// 0x00450d30..0x0045116e). C'est <b>le coffre</b> : un objet du decor qui s'ouvre quand un
    /// membre du groupe le touche, verifie une clef, remet un objet, puis se referme.
    ///
    /// <para>
    /// Une machine a etats dans le <c>short</c> de <c>+0x10</c>, dont les valeurs ne sont pas des
    /// numeros consecutifs mais des jalons espaces — <c>0</c> (amorcage), <c>0xFFFF</c> et
    /// <c>0xFFFE</c> (les deux repos, selon le sens d'ouverture), <c>1</c> et <c>2</c>
    /// (l'ouverture), <c>0x14</c>, <c>0x28</c>, <c>0x64</c>, <c>0xC8</c>, <c>0xC9</c>, <c>0xCA</c>
    /// (la fermeture), <c>0x12C</c> (refus) et <c>0x3E7</c> (echec). Chaque etat est un <c>if</c>
    /// separe qui <b>tombe dans le suivant</b> : plusieurs transitions s'enchainent dans la meme
    /// image, ce qui est voulu et transcrit tel quel plutot que rendu en <c>switch</c>.
    /// </para>
    /// <para>
    /// <b>Le piege du corps est <c>EDI</c>.</b> Il vaut zero a l'entree et sert de « zero » a
    /// plusieurs comparaisons, mais <c>0x00451097</c> le recharge a <c>0xC8</c> — et cette
    /// instruction est sur <b>toutes</b> les routes qui menent a l'etat <c>0x12C</c>. Le
    /// <c>mov [esi+10h],di</c> de <c>0x0045115d</c> pose donc <c>0xC8</c>, pas zero : apres le
    /// message de refus, le coffre repart par la sequence de fermeture. Une lecture naive du
    /// registre aurait mis zero et fait boucler l'objet sur son amorcage.
    /// </para>
    /// <para>
    /// Meme famille de piege pour <c>EBX</c>, qui vaut 1 en tete et <c>0x28</c> a partir de
    /// <c>0x00450e79</c> : les <c>mov [esi+10h],bx</c> des deux cotes ne posent pas la meme valeur.
    /// </para>
    /// <para>
    /// <b>Un defaut de 1998 est conserve</b> : dans l'etat <c>0x28</c>, le pointeur d'entite liee
    /// de <c>+0x6c</c> est remplace par <c>s_EntityList_005de558</c> quand il est nul — mais
    /// seulement dans <c>EAX</c>. Le <c>mov dword [ebx+6Ch],0</c> qui suit ecrit toujours a travers
    /// <c>EBX</c>, resté nul. Voir <c>Documentation/Reference/legacy-bug.md</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00450D30(uint* entity)
    {
        byte* e = (byte*)entity;

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;

        if (*(short*)(e + 0x10) == unchecked((short)0xFF01)) *(short*)(e + 0x10) = 1;
        if (*(short*)(e + 0x10) == unchecked((short)0xFFFE)) return;

        if (*(short*)(e + 0x10) == 0)
        {
            // L'amorcage lit l'angle d'ouverture de +0x30 : son SIGNE choisit lequel des deux repos
            // sera le sien, et la valeur est rangee en positif dans +0x9a.
            short opening = *(short*)(e + 0x30);
            *(short*)(e + 0x10) = unchecked((short)0xFFFF);
            *(short*)(e + 0x9a) = opening;
            *(short*)(e + 0x98) = 0;

            if (opening < 0)
            {
                *(short*)(e + 0x10) = unchecked((short)0xFFFE);
                *(short*)(e + 0x9a) = (short)-opening;
                *(short*)(e + 0x98) = 1;
            }

            int packed = *(short*)(e + 0x34);
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x34) = 0;
            *(int*)(e + 0x44) = packed & 0xff;
            *(int*)(e + 0x48) = packed >> 8;
        }

        if (*(short*)(e + 0x10) == unchecked((short)0xFFFF) && (entity[0] & 0x40) != 0)
        {
            // Le declenchement : un membre du groupe est en contact. L'entite liee de +0x6c
            // retombe sur s_EntityList_005de558 quand elle est nulle.
            uint* toucher = *(uint**)(e + 0x6c);
            if (toucher == null) toucher = (uint*)Res.s_EntityList_005de558;

            if ((*toucher & 0x10000) == 0)
            {
                entity[0] |= 0x2000;
                *(short*)(e + 0x10) = 1;
            }
        }

        short state = *(short*)(e + 0x10);
        if (state != unchecked((short)0xFF01) && state != unchecked((short)0xFFFE) && state != 0
            && state != unchecked((short)0xFFFF) && state != 0xCA)
        {
            FreezeAllEntities();
            entity[0] &= 0xFBFFFFFF;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x5c) = CrtRand() % 0x12C + 0x1F4;
            FreezeAllEntities();

            // Le son d'ouverture : meme double indirection que EntityHandler_004509B0, a +8 pres.
            byte soundId = (*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8)))[8];
            TryPlaySoundNearEntity(entity, soundId, 0x146, 0x3000, 0, 0);

            entity[0] &= 0xFBFFFFFF;
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            // La distance de vue glisse d'un quart vers la valeur tiree en entrant dans l'etat 1,
            // et le couvercle tourne de 35 par image.
            int target = *(int*)(e + 0x5c);
            int current = *Mem.EntityState(0x00571528);
            *Mem.EntityState(0x00571528) = current + ((target - current) >> 2);

            *(short*)(e + 0x34) += unchecked((short)0xFFDD);
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) >= 0x1F)
            {
                // Le coffre a-t-il deja ete ouvert ? Le drapeau vit dans la banque 0, un bit par
                // identifiant, et l'identifiant est celui range en +0x9a a l'amorcage.
                int flagBit = *(short*)(e + 0x9a);
                if ((Mem.s_StateBank0_005e96e0[flagBit >> 3] & (1 << (flagBit & 7))) != 0)
                {
                    *(short*)(e + 0x10) = 0x12C;
                    *(short*)(e + 0x12) = 0;
                    return;
                }

                int contents = *(int*)(e + 0x48);
                short rest;

                if (*(short*)(e + 0x14) == -1)
                {
                    if (contents == 0)
                    {
                        // Vide et sans etiquette : on repart directement sur la fermeture.
                        *(short*)(e + 0x10) = 0x28;
                        *(short*)(e + 0x12) = 0;
                        goto tail;
                    }

                    rest = *(short*)(e + 0x98);
                    *(short*)(e + 0x98) = 0x28;
                }
                else
                {
                    if (contents == 0)
                    {
                        *(short*)(e + 0x10) = 0x14;
                        *(short*)(e + 0x12) = 0;
                        goto tail;
                    }

                    rest = *(short*)(e + 0x98);
                    *(short*)(e + 0x98) = 0x14;
                }

                // L'objet sort du coffre : une entite d'effet, dont le type est le contenu decale
                // dans la plage 0xC0.
                SpawnEffectEntity(e, (byte)(contents + 0xC0), 0, -1);
                *(short*)(e + 0x98) = rest;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 0x3E7;
            }
        }

    tail:
        state = *(short*)(e + 0x10);

        if (state == 0x14)
        {
            // L'etiquette : le bit de la banque 3 est pose, puis la boite de dialogue est liee.
            int label = *(short*)(e + 0x14);
            Mem.s_StateBank3_005e9840[label >> 3] |= (byte)(1 << (label & 7));

            int entry = *(short*)(e + 0x16);
            byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + entry * 4);
            BindMenuLabels(labels, *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);

            *(short*)(e + 0x10) = 0x64;
            return;
        }

        if (state == 0x28)
        {
            uint* linked = *(uint**)(e + 0x6c);
            uint* holder = linked;
            if (holder == null) holder = (uint*)Res.s_EntityList_005de558;

            // L'inventaire du porteur : dix rangees de 0x280 octets dans son bloc de 0x2000.
            byte* inventory = GetMainMemPtr() + ((*((byte*)holder + 8) + 0x78) << 0xd);

            for (int row = 0; row <= 9; row++)
            {
                byte* slot = inventory + row * 0x280;
                if (*(short*)(slot + 0x386) == 0x2F1F
                    && slot[0x300] != 0
                    && *(ushort*)(slot + 0x310) >= 2)
                {
                    *(short*)(e + 0x10) = 0x12C;
                    return;
                }
            }

            // Ghidra: `mov dword [ebx+6Ch],0` — EBX, pas EAX. Le remplacement par
            // s_EntityList_005de558 ci-dessus n'a touche qu'EAX, donc un +0x6c nul se dereference
            // ici. Defaut de 1998, conserve.
            *(uint*)((byte*)linked + 0x6c) = 0;

            uint* subject = *(uint**)(e + 0x6c);
            *((byte*)subject + 0x41) = e[8];

            byte* dropTable = Mem.Arena(0x005e9860);
            byte already = dropTable[*(int*)(e + 0x44)];

            if (already == 0)
            {
                int amount = PickWeightedTableEntry(*(short*)(e + 0x16), 0x64);
                ushort granted = (ushort)GrantChestContents_00530F20(*(uint**)(e + 0x6c), amount, 0x00450700);

                if (granted == 0)
                {
                    *(short*)(e + 0x10) = 0x3E7;
                    return;
                }

                dropTable[*(int*)(e + 0x44)] = (byte)granted;
                *(short*)(e + 0x10) = 0xC8;
                return;
            }

            ushort again = (ushort)GrantChestContents_00530F20(*(uint**)(e + 0x6c), already - 1, 0x00450700);

            // Ghidra: `neg ax ; sbb eax,eax ; and eax,0FFFFFCE1h ; add eax,3E7h` — la forme sans
            // branchement de « non nul ? 0xC8 : 0x3E7 ».
            *(short*)(e + 0x10) = (short)(again != 0 ? 0xC8 : 0x3E7);
            return;
        }

        // A partir d'ici, EDI vaut 0xC8 dans le binaire — voir le resume de la methode.
        if (state == 0x64)
        {
            int flagBit = *(short*)(e + 0x9a);
            Mem.s_StateBank0_005e96e0[flagBit >> 3] |= (byte)(1 << (flagBit & 7));
            *(short*)(e + 0x10) = 0xC8;
        }

        if (*(short*)(e + 0x10) == 0xC8)
        {
            FreezeAllEntities();
            entity[0] &= 0xFBFFFFFF;
            *(short*)(e + 0x10) = 0xC9;
        }

        if (*(short*)(e + 0x10) == 0xC9)
        {
            // La refermeture : le couvercle revient de 0x46 par image jusqu'a repasser par zero.
            *(short*)(e + 0x34) += 0x46;
            if (*(short*)(e + 0x34) >= 0)
            {
                *(short*)(e + 0x34) = 0;
                *(short*)(e + 0x10) = 0xCA;
                UnfreezeAllEntities();
                entity[0] &= 0xFFFFDFFF;      // Ghidra: `and ah,0DFh`
                return;
            }
        }

        if (*(short*)(e + 0x10) == 0xCA && (entity[0] & 0x40) == 0)
        {
            // Retour au repos, du cote choisi a l'amorcage.
            *(short*)(e + 0x10) = *(short*)(e + 0x98) != 0
                ? unchecked((short)0xFFFE)
                : unchecked((short)0xFFFF);
        }

        if (*(short*)(e + 0x10) == 0x12C)
        {
            // Le refus : une etiquette fixe (+0x118) prise dans l'AUTRE repertoire, DAT_005e97c8.
            byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70) + 0x118);
            BindMenuLabels(labels, *(uint*)(e + 0x6c), s_WorldMapDescriptor_005633b0, 0);

            // `mov [esi+10h],di`, et EDI vaut 0xC8 ici — pas zero.
            *(short*)(e + 0x10) = 0xC8;
        }
    }
}
