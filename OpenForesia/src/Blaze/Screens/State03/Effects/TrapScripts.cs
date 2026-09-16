// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PIEGES DE COFFRE — leurs scripts de son et leurs gerbes.
//
// Un coffre piege tire son effet de la MEME table 0x0056d4e0 qu'une potion ou un sort
// (`Combat/EffectTable.cs`) : ce qui distingue le piege, c'est ce qu'il fait naitre autour.
// Ce fichier porte ces naissances - la gerbe, la gerbe de degats, le script de son joue au
// declenchement - et les gestionnaires 0x004754C0 a 0x004757xx qui les animent.
//
// Les planches sont des plages relevees dans `.data`, indexees EN AVANT par leur consommateur :
// une plage trop courte rend du tas non initialise, sans erreur ni exception. Lancer
// `sh OpenForesia.Tools/check-descriptors.sh` apres toute retouche.
//
// Ce que le coffre contient, et le tirage de son contenu, sont dans `Handlers/Chests.cs`.
// Voir Documentation/Reference/DataSymbols.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551858</c> — <b>le script de sons du piege</b>, tel que
    /// <see cref="Audio.TickDelayedSounds"/> le lit : identifiant en <c>+0</c>, delai du lot suivant
    /// en <c>+1</c>, nombre de sons du lot en <c>+2</c>, hauteur en <c>+4</c>, gain en <c>+6</c>.
    ///
    /// <para>
    /// Deux enregistrements, donc <b>deux fois le son 3 a une image d'intervalle</b> : le premier
    /// lot annonce un delai de 1, le second un delai de 0 qui termine le script et libere
    /// l'emplacement. Seize octets — la suite (<c>0x00551868</c>) est un autre script, son
    /// identifiant change.
    /// </para>
    /// </summary>
    private static readonly byte[] s_TrapSoundScript_00551858 =
    [
        0x03, 0x01, 0x01, 0x00, 0x00, 0x01, 0x00, 0x12,   // 0x00551858
        0x03, 0x00, 0x01, 0x00, 0x00, 0x01, 0x00, 0x12,   // 0x00551860
    ];

    private static byte* s_TrapSoundScriptStorage;

    /// <summary>Le script en memoire native : la file des sons differes le garde par pointeur, donc
    /// il doit survivre a l'appel.</summary>
    private static byte* TrapSoundScript()
    {
        if (s_TrapSoundScriptStorage == null)
        {
            s_TrapSoundScriptStorage =
                (byte*)NativeMemory.Alloc((nuint)s_TrapSoundScript_00551858.Length);
            for (int i = 0; i < s_TrapSoundScript_00551858.Length; i++)
            {
                s_TrapSoundScriptStorage[i] = s_TrapSoundScript_00551858[i];
            }
        }

        return s_TrapSoundScriptStorage;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005519d0</c> — <b>les sept images de la gerbe de piege</b>, l'atlas que
    /// <see cref="EntityHandler_004755F0"/> passe a chacune des entites <c>0x3d</c> qu'il seme.
    ///
    /// <para>
    /// Sept, et la borne se lit dans le code comme pour <see cref="s_SplashDescriptors_00551A08"/> :
    /// <see cref="EntityHandler_00475730"/> pose <c>+0x40 = compteur / 2</c> et s'eteint des que
    /// cet indice atteint 7. La donnee le confirme d'une facon particulierement nette — la table
    /// s'arrete <b>exactement</b> a <c>0x00551a08</c>, la premiere image de l'eclaboussure.
    /// </para>
    /// <para>
    /// C'est encore la meme sequence de sept deplacements que <c>0x00551a08</c> et
    /// <see cref="s_Entity34Descriptors_0054F0E0"/> — <c>c0b8</c>, <c>d8b8</c>, <c>60d0</c>,
    /// <c>78d0</c>, <c>90d0</c>, <c>a8d0</c>, <c>c0d0</c> — a la moitie haute pres. Trois tables
    /// pour une seule animation, dans trois planches.
    /// </para>
    /// </summary>
    private static readonly byte[] s_TrapBurstDescriptors_005519D0 =
    [
        0xc0, 0xb8, 0x18, 0x18, 0x00, 0x02, 0x10, 0x00,   // 0x005519d0
        0xd8, 0xb8, 0x18, 0x18, 0x00, 0x02, 0x10, 0x00,   // 0x005519d8
        0x60, 0xd0, 0x18, 0x18, 0x00, 0x02, 0x10, 0x00,   // 0x005519e0
        0x78, 0xd0, 0x18, 0x18, 0x00, 0x02, 0x10, 0x00,   // 0x005519e8
        0x90, 0xd0, 0x18, 0x18, 0x00, 0x02, 0x10, 0x00,   // 0x005519f0
        0xa8, 0xd0, 0x18, 0x18, 0x00, 0x02, 0x10, 0x00,   // 0x005519f8
        0xc0, 0xd0, 0x18, 0x18, 0x00, 0x02, 0x10, 0x00,   // 0x00551a00
    ];

    private static byte* s_TrapBurstDescriptorStorage;

    /// <summary>Les sept descripteurs en memoire native.</summary>
    private static byte* TrapBurstDescriptors()
    {
        if (s_TrapBurstDescriptorStorage == null)
        {
            s_TrapBurstDescriptorStorage =
                (byte*)NativeMemory.Alloc((nuint)s_TrapBurstDescriptors_005519D0.Length);
            for (int i = 0; i < s_TrapBurstDescriptors_005519D0.Length; i++)
            {
                s_TrapBurstDescriptorStorage[i] = s_TrapBurstDescriptors_005519D0[i];
            }
        }

        return s_TrapBurstDescriptorStorage;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551ab8</c> — <b>les quatre images de la gerbe du piege a degats</b>,
    /// l'atlas que <see cref="EntityHandler_004754C0"/> passe a chacune des entites <c>0x3c</c>
    /// qu'il seme.
    ///
    /// <para>
    /// Quatre, et la borne se lit dans le corps du consommateur, comme pour
    /// <see cref="s_TrapBurstDescriptors_005519D0"/> : <see cref="EntityHandler_004755C0"/> pose
    /// <c>+0x40 = compteur / 2</c> et s'eteint des que cet indice depasse 3.
    /// </para>
    /// <para>
    /// Les quatre enregistrements reprennent les memes tuiles <c>0x20 x 0x20</c> que la plage
    /// <c>0x00551a98</c> qui les precede immediatement ; ce qui les en distingue est le dernier
    /// octet, nul dans toute la plage precedente et <c>02 02 04 0b</c> ici. C'est lui qui borne la
    /// table par le bas — la serie a dernier octet nul est une autre animation.
    /// </para>
    /// </summary>
    private static readonly byte[] s_TrapDamageBurstDescriptors_00551AB8 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x02,   // 0x00551ab8
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x02,   // 0x00551ac0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x04,   // 0x00551ac8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x0b,   // 0x00551ad0
    ];

    private static byte* s_TrapDamageBurstDescriptorStorage;

    /// <summary>Les quatre descripteurs en memoire native.</summary>
    private static byte* TrapDamageBurstDescriptors()
    {
        if (s_TrapDamageBurstDescriptorStorage == null)
        {
            s_TrapDamageBurstDescriptorStorage =
                (byte*)NativeMemory.Alloc((nuint)s_TrapDamageBurstDescriptors_00551AB8.Length);
            for (int i = 0; i < s_TrapDamageBurstDescriptors_00551AB8.Length; i++)
            {
                s_TrapDamageBurstDescriptorStorage[i] = s_TrapDamageBurstDescriptors_00551AB8[i];
            }
        }

        return s_TrapDamageBurstDescriptorStorage;
    }

    /// <summary>
    /// Ghidra: <c>0x004754c0</c> — <b>l'entree 180 (<c>0xb4</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 254 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_004754C0.asm</c>.
    ///
    /// <para>
    /// <b>C'est l'autre piege de coffre</b> — le voisin immediat de
    /// <see cref="EntityHandler_004755F0"/> dans la table comme dans le binaire, et celui qui
    /// plantait quand le tirage du coffre designait <c>0xb4</c> plutot que <c>0xb5</c>. Il vit
    /// <b>seize images</b> :
    /// </para>
    /// <list type="bullet">
    /// <item><b>image 0</b> — reglage : teinte <c>0xf0f0f0</c>, echelle <c>0x1000</c>, gravite
    /// <c>+0x9a</c> a <c>0x4000</c> ;</item>
    /// <item><b>image 1</b> — le son <c>0x0b</c> part a la position de l'entite ;</item>
    /// <item><b>chaque image</b> — une entite <c>0x3c</c> nait sur un cercle autour du PORTEUR
    /// (la victime, retenue en <c>+0x6c</c>), a un angle tire sur douze bits et un rayon tire
    /// entre 0 et 59 ; la hauteur est celle du porteur, sans dispersion ;</item>
    /// <item><b>image 16</b> — la victime perd 5 % de ses points de vie, puis l'entite s'efface.</item>
    /// </list>
    /// <para>
    /// <b>La vitesse de l'enfant se pose sur le pere.</b> Le piege ecrit son propre <c>+0x50</c> /
    /// <c>+0x54</c> / <c>+0x58</c> — le meme angle que la position, mis a l'echelle par huit, et
    /// <c>0x10000</c> de montee — juste avant l'appel a
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/>, qui recopie <c>0x27</c> mots du pere dans
    /// l'enfant. C'est le seul chemin par lequel la gerbe recoit sa direction, puisque
    /// <see cref="EntityHandler_004755C0"/> ne fait qu'integrer ce qu'elle trouve deja ecrit.
    /// </para>
    /// <para>
    /// <b>Il n'emprunte pas d'entite d'assaillant</b>, a la difference de <c>0xb5</c> : le degat
    /// passe par <see cref="ApplyPercentDamage_005397B0"/>, qui n'a pas besoin d'attaquant. Il n'y
    /// a donc ici ni copie en <c>0x005ede20</c>, ni jet de sauvegarde — le coup porte toujours.
    /// </para>
    /// </summary>
    private static void EntityHandler_004754C0(byte* e)
    {
        short phase = *(short*)(e + 0x10);
        byte* carrier = *(byte**)(e + 0x6c);

        if (phase == 0)
        {
            *(short*)(e + 0x0a) = 0;
            *(uint*)(e + 0x38) = 0xf0f0f0;
            *(uint*)(e + 0x28) = 0x10001000;
            e[0x40] = 0;
            *(short*)(e + 0x9a) = 0x4000;
        }
        else if (phase == 1)
        {
            // Ghidra: LAB_004754f7 — un « sinon », la ou 0x4755f0 enchaine deux tests independants :
            // le bloc d'image 0 saute par-dessus celui-ci (jmp LAB_00475515).
            Aud.PlayPositionalSound(0x0b, 0x400, 0x1200, (int*)(e + 0x1c));
        }

        // Ghidra: LAB_00475515 — a CHAQUE image, sans la parite de 0x4755f0.
        int angle = CrtRand() & 0xfff;
        int radius = CrtRand() % 0x3c;

        int* position = stackalloc int[3];
        position[0] = *(int*)(carrier + 0x1c) + (int)Winsys3d.GetSinTableEntry(angle) * radius;
        position[1] = *(int*)(carrier + 0x20);
        position[2] = *(int*)(carrier + 0x24) + Winsys3d.GetCosTableEntry(angle) * radius;

        // La vitesse que l'enfant recevra par la copie des 0x27 mots.
        *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(angle) << 3;
        *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(angle) << 3;
        *(int*)(e + 0x54) = 0x10000;

        SpawnStatusEffectEntity_0047AC40(
            e, position, TrapDamageBurstDescriptors(), 0x3c, e[0x0b], -1);

        // Ghidra: LAB_00475598
        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) < 0x10) return;

        *(uint*)e = 0;
        ApplyPercentDamage_005397B0((uint*)carrier, 5, 0);
    }

    /// <summary>
    /// Ghidra: FUN_004755c0 @ 0x004755c0 — <b>l'entree 60 (<c>0x3c</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>. Decompilee dans <c>Blaze.c</c>.
    /// C'est la gerbe que <see cref="EntityHandler_004754C0"/> seme a chaque image.
    ///
    /// <para>
    /// La sœur de <see cref="EntityHandler_00475730"/>, au frottement pres : celle-ci avance par
    /// <see cref="IntegrateWithDrag_00535880"/> au lieu de monter d'un pas fixe, prend pour image
    /// d'atlas <b>la moitie de son compteur</b> — deux images par dessin — et s'eteint des que cet
    /// indice depasse 3. Huit images de vie.
    /// </para>
    /// <para>
    /// <b>L'indice est ecrit avant le test</b>, y compris sur l'image ou elle meurt, et la
    /// comparaison est <b>non signee</b> sur l'octet bas. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_004755C0(byte* e)
    {
        IntegrateWithDrag_00535880(e);

        short counter = *(short*)(e + 0x10);

        byte frame = (byte)(short)(counter >> 1);
        e[0x40] = frame;
        if (frame > 3) *(uint*)e = 0;

        *(short*)(e + 0x10) = (short)(counter + 1);
    }

    /// <summary>
    /// Ghidra: <c>0x004755f0</c> — <b>l'entree 181 (<c>0xb5</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 313 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_004755F0.asm</c>.
    ///
    /// <para>
    /// <b>C'est le piege de coffre</b>, signale en jeu par Vincent le 2026-08-29. Il vit
    /// <b>huit images</b> exactement, et son deroule est entierement porte par le compteur
    /// <c>+0x10</c> :
    /// </para>
    /// <list type="bullet">
    /// <item><b>image 0</b> — reglage : teinte <c>0xf0f0f0</c>, echelle <c>0x1800</c>, gravite
    /// <c>+0x9a</c> a <c>0x4000</c> ;</item>
    /// <item><b>image 1</b> — le script de sons <see cref="s_TrapSoundScript_00551858"/> part dans
    /// la file differee ;</item>
    /// <item><b>images paires</b> — une gerbe : une entite <c>0x3d</c> nait sur un cercle autour du
    /// PORTEUR (le coffre, retenu en <c>+0x6c</c>), a un angle tire sur douze bits et un rayon tire
    /// entre 0 et 59. La hauteur est celle du porteur, sans dispersion ;</item>
    /// <item><b>image 8</b> — le coup part, puis l'entite s'efface.</item>
    /// </list>
    /// <para>
    /// <b>Le coup se porte par une entite d'emprunt</b>, et c'est le point interessant :
    /// <c>0x005ede20</c> est l'adresse qui suit <b>immediatement</b> les trente-deux emplacements de
    /// la table de scene <c>0x005ecaa0</c>. Le piege s'y recopie (<c>0x27</c> mots), y force le
    /// genre <c>0x20</c> en <c>+8</c>, lui fabrique un bloc de statistiques neuf par
    /// <see cref="ResetEntityStats"/>, et s'en sert comme assaillant.
    /// </para>
    /// <para>
    /// <b>Cette adjacence n'est pas un hasard et elle n'est pas facultative</b> :
    /// <see cref="ValidateCombatants_0053A860"/> accepte un assaillant de scene jusqu'a
    /// <c>0x005ecaa0 + 0x1380</c> <b>inclus</b>, ce qui est exactement <c>0x005ede20</c>. La borne
    /// haute « d'un emplacement de trop » relevee le 2026-08-26 n'etait donc pas une maladresse du
    /// binaire : c'est <b>par elle</b> que le piege est reconnu comme un combattant valide. La
    /// reconstruction la conserve parce que l'arene d'entites est une seule allocation, comme
    /// l'original.
    /// </para>
    /// </summary>
    private static void EntityHandler_004755F0(byte* e)
    {
        short phase = *(short*)(e + 0x10);
        byte* carrier = *(byte**)(e + 0x6c);

        if (phase == 0)
        {
            *(short*)(e + 0x0a) = 0;
            *(uint*)(e + 0x38) = 0xf0f0f0;
            *(uint*)(e + 0x28) = 0x18001800;
            e[0x40] = 0;
            *(short*)(e + 0x9a) = 0x4000;
        }

        // Ghidra: LAB_00475625 — le test porte sur la valeur d'ENTREE, pas sur celle que le bloc
        // precedent aurait pu ecrire ; il ne l'ecrit d'ailleurs pas.
        if (phase == 1)
        {
            Aud.QueueDelayedSoundScript(TrapSoundScript(), (int*)(e + 0x1c));
        }

        // Ghidra: LAB_0047563c — une image sur deux.
        if ((e[0x10] & 1) == 0)
        {
            int angle = CrtRand() & 0xfff;
            int radius = CrtRand() % 0x3c;

            int* position = stackalloc int[3];
            position[0] = *(int*)(carrier + 0x1c) + (int)Winsys3d.GetSinTableEntry(angle) * radius;
            position[1] = *(int*)(carrier + 0x20);
            position[2] = *(int*)(carrier + 0x24) + (int)Winsys3d.GetCosTableEntry(angle) * radius;

            SpawnStatusEffectEntity_0047AC40(e, position, TrapBurstDescriptors(), 0x3d, 0, 3);
        }

        // Ghidra: LAB_004756a2
        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) < 8) return;

        // L'entite d'emprunt, une place au-dela de la table de scene. Voir le commentaire de tete.
        byte* proxy = Mem.Entity(0x005ede20);

        *(uint*)e = 0x80400000;
        Mem.CopyDwords((uint*)e, (uint*)proxy, 0x27);      // Ghidra: FUN_005352e0

        proxy[8] = 0x20;
        ResolveEntityBlocks(proxy, out byte* stats, out byte* species, out byte* model);
        ResetEntityStats(stats, species, model);           // Ghidra: FUN_005389e0

        *(short*)(proxy + 6) = *(short*)(e + 6);

        ApplyTrapEffect_0053C420((uint*)proxy, *(uint**)(e + 0x6c), 0);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00475730</c> — <b>l'entree 61 (<c>0x3d</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 46 octets, <b>absente de <c>Blaze.c</c></b>.
    /// C'est la gerbe que <see cref="EntityHandler_004755F0"/> seme une image sur deux.
    ///
    /// <para>
    /// La plus courte de toute la famille, et sans branche d'initialisation : elle monte de
    /// <c>0xA000</c> par image, prend pour image d'atlas <b>la moitie de son compteur</b> — donc
    /// deux images par dessin — et s'eteint des que cet indice atteint 7. Quatorze images de vie.
    /// </para>
    /// <para>
    /// <b>L'indice est ecrit avant le test</b>, y compris sur l'image ou elle meurt ; et la
    /// comparaison est <b>non signee</b> sur l'octet bas. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475730(byte* e)
    {
        short counter = *(short*)(e + 0x10);

        *(int*)(e + 0x20) += 0xa000;

        byte frame = (byte)(short)(counter >> 1);
        e[0x40] = frame;
        if (frame >= 7) *(uint*)e = 0;

        *(short*)(e + 0x10) = (short)(counter + 1);
    }

    /// <summary>
    /// Ghidra: FUN_00475400 @ 0x00475400 — <b>l'entree 41 (<c>0x29</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>La marque du coup</b> : l'entite que toute la famille de resolution fait naitre sur la
    /// victime — <see cref="PrepareCombatPair_00538870"/> quand le coup est refuse,
    /// <see cref="ApplyTrapEffect_0053C420"/> quand il porte,
    /// <see cref="ValidateCombatants_0053A860"/> quand l'assaillant est douteux.
    /// </para>
    /// <para>
    /// Elle se colle a son porteur (<c>+0x6c</c>) a chaque image, decalee vers le haut de la
    /// hauteur de bulle d'etat de celui-ci — <see cref="StatusBubbleHeight_00538BC0"/>, mesuree une
    /// seule fois a la naissance et gardee en <c>+0x48</c>. <b>Elle enfle puis palit</b> : les douze
    /// premieres images son echelle suit un sinus (<c>angle = image &lt;&lt; 7</c>) jusqu'a
    /// <c>0x1200</c>, et a partir de la 32e sa teinte descend de <c>0xff</c> a <c>0x1f</c>. Elle
    /// meurt a la 40e.
    /// </para>
    /// </summary>
    private static void EntityHandler_00475400(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x0a) = 0;
            *(uint*)(e + 0x38) = 0x1ffffff;
            *(uint*)(e + 0x28) = 0;
            *(uint*)(e + 0x40) = 0;
            *(short*)(e + 0x2e) = unchecked((short)0xfff6);

            *(int*)(e + 0x48) = StatusBubbleHeight_00538BC0((uint*)carrier) << 0xc;
        }

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + *(int*)(e + 0x48);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        if (*(short*)(e + 0x10) < 0xc)
        {
            // Ordinal_451 : la table de sinus. L'angle est l'image multipliee par 0x80.
            int scale = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 7);
            short size = (short)(scale * 0x1200 >> 0xc);
            *(short*)(e + 0x28) = size;
            *(short*)(e + 0x2a) = size;
        }

        short frame = *(short*)(e + 0x10);

        if (frame > 0x1f)
        {
            // La teinte descend de 0xff a 0x1f sur les huit dernieres images, repliee sur les trois
            // canaux, l'octet de mode passant a 2.
            uint level = (uint)(frame * -0x20 + 0x4ff);
            *(uint*)(e + 0x38) = ((level | 0x200) << 8 | level) << 8 | level;
        }

        *(short*)(e + 0x10) = (short)(frame + 1);
        if ((short)(frame + 1) > 0x27) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00445950</c> — <b>l'entree 58 (<c>0x3a</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 142 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00445950.asm</c>.
    ///
    /// <para>
    /// La seconde entite que <see cref="ApplyTrapEffect_0053C420"/> fait naitre quand un effet
    /// prend. Elle se colle a son porteur, prend pour largeur l'etendue <c>+0x1e</c> de son bloc de
    /// modele, et seme une entite <c>0x99</c> une image sur deux pendant dix-huit images.
    /// </para>
    /// <para>
    /// <b>Elle a un porteur de repli</b> : si <c>+0x6c</c> est nul elle prend
    /// <see cref="Resources.s_EntityList_005de558"/>, l'entite de service que
    /// <c>Resources</c> tient a zero et qui se pointe elle-meme en <c>+0x6c</c>. Elle ne peut donc
    /// pas manquer de porteur — elle se colle a l'origine du monde plutot que de ne rien faire.
    /// C'est ecrit ainsi dans le binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00445950(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x14) = 0;

        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;   // Ghidra: DAT_005de558

        ResolveEntityBlocks(carrier, out _, out _, out byte* model);
        *(short*)(e + 0x14) = *(short*)(model + 0x1e);

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        if ((e[0x10] & 1) != 0)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x99, 0, -1);
        }

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 0x12) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005515b8</c> et <c>&amp;DAT_005515c0</c> — les deux atlas de
    /// <see cref="EntityHandler_004459E0"/>, un par moitie.
    ///
    /// <para>
    /// <b>Un seul enregistrement pour le premier</b> : la moitie <c>+0x44 == 0</c> pose
    /// <c>+0x40 = 0</c> et ne le touche plus jamais. Il est d'ailleurs seul de sa planche — la
    /// plage qui le precede (<c>0x005515a0</c>) est en <c>d0</c>, celle qui le suit en <c>78</c>.
    /// </para>
    /// <para>
    /// <b>Quatre pour le second</b>, et le corps le dit sans ambiguite : la moitie
    /// <c>+0x44 == 1</c> avance <c>+0x40</c> d'un cran toutes les six sous-etapes et le
    /// <b>plafonne a 3</b> (<c>si l'incrementation donne 4, remettre 3</c>). Les quatre tuiles
    /// <c>7078</c>, <c>8078</c>, <c>9078</c>, <c>a078</c> se suivent exactement.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_005515B8 =
    [
        0xa0, 0xa8, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515b8
    ];

    /// <summary><see cref="s_Descriptors_005515B8"/> porte le commentaire des deux.</summary>
    private static readonly byte[] s_Descriptors_005515C0 =
    [
        0x70, 0x78, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515c0
        0x80, 0x78, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515c8
        0x90, 0x78, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515d0
        0xa0, 0x78, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515d8
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005519b8</c> — <b>les trois images de la bouffee <c>0xbe</c></b> que
    /// <see cref="EntityHandler_00475D40"/> lache quatre fois.
    ///
    /// <para>
    /// Trois, et pour une fois la borne est <b>ecrite dans l'engendreur lui-meme</b> :
    /// <c>0x00475d40</c> pose <c>+0x1a = 3</c> avant cet appel, et
    /// <see cref="EntityHandler_00475FC0"/> parcourt <c>+0x1a</c> enregistrements. La donnee suit :
    /// les trois derniers de la plage <c>0x005519a0</c> (six tuiles <c>0x10 x 0x10</c>, dont
    /// <see cref="s_Descriptors_005519A0"/> prend les trois premiers), et <c>0x005519d0</c>
    /// derriere change de planche.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_005519B8 =
    [
        0x00, 0xd0, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x005519b8
        0x10, 0xd0, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x005519c0
        0x20, 0xd0, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x005519c8
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551868</c> — <b>le son du coup qui ne passe pas</b>, le script que
    /// <see cref="EntityHandler_00475D40"/> depose dans la file differee. Meme forme que
    /// <see cref="s_TrapSoundScript_00551858"/> mais <b>un seul enregistrement</b> : son delai de
    /// lot suivant est nul, ce qui termine le script des la premiere lecture.
    /// </summary>
    private static readonly byte[] s_ResistSoundScript_00551868 =
    [
        0x03, 0x00, 0x01, 0x00, 0x00, 0x01, 0x00, 0x12,   // 0x00551868
    ];

    private static byte* s_Descriptors005515B8Storage;
    private static byte* s_Descriptors005515C0Storage;
    private static byte* s_Descriptors005519B8Storage;
    private static byte* s_ResistSoundScriptStorage;

    private static byte* NativeCopy(byte[] source, ref byte* storage)
    {
        if (storage == null)
        {
            storage = (byte*)NativeMemory.Alloc((nuint)source.Length);
            for (int i = 0; i < source.Length; i++) storage[i] = source[i];
        }

        return storage;
    }

    private static byte* Descriptors005515B8()
        => NativeCopy(s_Descriptors_005515B8, ref s_Descriptors005515B8Storage);

    private static byte* Descriptors005515C0()
        => NativeCopy(s_Descriptors_005515C0, ref s_Descriptors005515C0Storage);

    private static byte* Descriptors005519B8()
        => NativeCopy(s_Descriptors_005519B8, ref s_Descriptors005519B8Storage);

    /// <summary>Le script en memoire native : la file des sons differes le garde par pointeur.</summary>
    private static byte* ResistSoundScript()
        => NativeCopy(s_ResistSoundScript_00551868, ref s_ResistSoundScriptStorage);
}
