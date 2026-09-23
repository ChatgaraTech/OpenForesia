// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur pour `ClearOTagR`.
//
// ============================================================================================
// LA LISTE D'AFFICHAGE — CE QUI S'EN DETACHE, ET CE QUI N'EN DETACHE PAS
//
// `DrawOTag` (ordinal 701, `FUN_00a061b0`) est le cœur du chemin d'affichage, et il n'est PAS
// portable aujourd'hui. Mesure faite sur son corps : **treize globales de la DLL, huit appels
// internes, et quatre emplacements de la vtable du device de 3dhelp**. Le porter seul mettrait a
// jour notre copie des treize globales et laisserait celles de la DLL — que le reste du chemin
// natif relit — inchangees. C'est la regle 6 de `byebye-dll.md`, et c'est elle qui decide du
// decoupage de tout ce qui reste.
//
// Ce fichier ne porte donc que ce qui se detache vraiment : **quatre entrees qui ne touchent
// aucun etat partage**. Le releve, corps par corps, pour que le prochain lot n'ait pas a le
// refaire :
//
//     ordinal  corps           globales  appels  vtable   verdict
//     658      FUN_00a05ac0    0         0       0        PORTE
//     661      FUN_00a05b50    0         0       0        PORTE
//     688      FUN_00a06160    0         0       0        PORTE
//     660      FUN_00a05b30    1         0       0        PORTE — voir ci-dessous
//     677      FUN_00a05bb0    1         0       0        PORTE le 2026-09-22 dans `Tim.cs`
//     662      FUN_00a05bd0    1         0       0        PORTE le 2026-09-22 dans `Tim.cs`
//     665/674/676/684  ...     2         1       0        bloques
//     710      FUN_00a026e0    4         3       0        bloque
//     668      FUN_00a020a0    10        0       0        bloque
//     657      FUN_00a05a00    14        1       2        bloque
//     701      FUN_00a061b0    13        7       4        bloque — le cœur
//
// **CORRECTION DU 2026-09-21, par `census-dll-globals.sh`.** Les ordinaux 677 et 662 sont marques
// « bloques » ci-dessus dans la premiere version de ce fichier, sur le compte des REFERENCES a leur
// globale `DAT_011be790` — dix — pris pour un compte de lecteurs. La carte des globales montre que
// ces dix references se repartissent sur DEUX corps seulement, ces deux-la : une reference dans 677
// et neuf dans 662. La paire est donc une composante fermee, portable d'un bloc, exactement comme
// 659/660. Compter des references quand la question porte sur des corps : la carte existe pour que
// cette erreur-la ne se refasse pas a la main.
//
// **La paire est partie le 2026-09-22**, et pas ici : `OpenTIM`/`ReadTIM` lisent un conteneur
// d'image, ils ne touchent pas a la liste d'affichage. Ils ont pris leur fichier, `Tim.cs`, comme
// `src/Libraries/LISEZMOI.md` le demande des qu'un sujet est identifiable. La ligne reste dans ce
// releve parce que c'est ce releve qui avait pose la question.
//
// **CORRECTION DU 2026-09-23 — le casier de 660 n'etait PAS prive.** La premiere version de ce
// fichier portait 660 « malgre sa globale » : le bloc `DAT_011be6d8` (23 mots, un `DRAWENV`)
// n'avait que deux references dans `winsys3d.c`, 660 qui l'ecrit et 659 qui le relit, et le
// casier avait ete recopie dans un tableau C#. **C'etait faux** : `DrawOTag` lit l'octet +0x18 du
// bloc — `isbg`, « effacer le fond » — sous un AUTRE nom, `DAT_011be6f0`, pour choisir entre
// `Clear(TARGET)` et `Clear(ZBUFFER)`. Un symbole Ghidra au milieu d'un bloc echappe a un compte
// de references par nom. Avec la copie privee, `isbg` restait a 0 dans la DLL : l'image n'etait
// jamais effacee, et tout ce que la scene ne recouvrait pas gardait les images precedentes
// (l'ecran de chargement dans le noir de la mine, la trainee des fenetres de texte). Le casier est
// maintenant la memoire de la DLL elle-meme, `Winsys3dData.DrawEnv_011be6d8` — la regle 6 de
// `src/Libraries/LISEZMOI.md`.
// ============================================================================================

using System;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dListeAffichage
{
    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05ac0 @ 0x00a05ac0 (ordinal 658) — `SetDefDispEnv` : remplit
    /// l'environnement d'affichage a partir d'un rectangle.
    ///
    /// Les deux constantes de queue sont la definition PlayStation : <c>0x100</c> et <c>0xf0</c>,
    /// soit **256 x 240**. Elles sont ecrites en dur ici, quelle que soit la taille demandee.
    /// </summary>
    internal static void SetDefDispEnv(void* dispEnv, short x, short y, short w, short h)
    {
        ushort* e = (ushort*)dispEnv;
        e[0] = (ushort)x;
        e[1] = (ushort)y;
        e[2] = (ushort)w;
        e[3] = (ushort)h;
        e[4] = 0;
        e[5] = 0;
        e[6] = 0x100;
        e[7] = 0xf0;
        ((byte*)e)[0x10] = 0;
        ((byte*)e)[0x11] = 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05b50 @ 0x00a05b50 (ordinal 661) — `SetDefDrawEnv` : remplit
    /// l'environnement de dessin.
    ///
    /// **Les mots 4 et 5 reprennent x et y**, pas la largeur et la hauteur : ce sont le decalage
    /// de dessin, initialise au coin du rectangle. L'ordre d'ecriture du corps d'origine est
    /// conserve tel quel (0, 2, 1, 4, 5, 3, puis la suite) — il n'a pas d'effet observable, mais
    /// le garder rend la relecture contre `winsys3d.c` immediate.
    /// </summary>
    internal static void SetDefDrawEnv(void* drawEnv, short x, short y, short w, short h)
    {
        ushort* e = (ushort*)drawEnv;
        e[0] = (ushort)x;
        e[2] = (ushort)w;
        e[1] = (ushort)y;
        e[4] = (ushort)x;
        e[5] = (ushort)y;
        e[3] = (ushort)h;
        e[6] = 0;
        e[7] = 0;
        e[8] = 0;
        e[9] = 0;
        e[10] = 10;
        ((byte*)e)[0x16] = 1;
        ((byte*)e)[0x17] = 1;
        ((byte*)e)[0x18] = 0;
        ((byte*)e)[0x19] = 0;
        ((byte*)e)[0x1a] = 0;
        ((byte*)e)[0x1b] = 0;
    }

    /// <summary>Le `DRAWENV` courant : 23 mots, 0x5C octets.</summary>
    private const int MotsDrawEnv = 0x17;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05b30 @ 0x00a05b30 (ordinal 660) — range 23 mots dans le casier.
    /// Rend toujours zero.
    /// </summary>
    internal static int PutDrawEnv(void* env)
    {
        uint* source = (uint*)env;
        uint* casier = Winsys3dData.DrawEnv_011be6d8;
        for (int i = 0; i < MotsDrawEnv; i++) casier[i] = source[i];
        return 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05b10 @ 0x00a05b10 (ordinal 659) — ressort les 23 mots. **Pas
    /// importe par la reconstruction** ; ecrit ici parce que le casier n'a de sens qu'en paire, et
    /// que la paire doit basculer d'un bloc (regle 1).
    /// </summary>
    internal static void GetDrawEnv(void* env)
    {
        uint* destination = (uint*)env;
        uint* casier = Winsys3dData.DrawEnv_011be6d8;
        for (int i = 0; i < MotsDrawEnv; i++) destination[i] = casier[i];
    }

    // ============================================================================================
    // L'ENVIRONNEMENT D'AFFICHAGE — `PutDispEnv` (657) et `GetDispEnv` (656), 2026-09-23
    //
    // Le jumeau de la paire 660/659 : un `DISPENV` de cinq mots, range a `0x011be6b8`. Mais 657
    // ne se contente pas de ranger — c'est **la fin d'image** : il recentre l'image selon la
    // largeur affichee, attend le balayage vertical (device `+0xd8`), PRESENTE l'image (`+0xb8`,
    // le `Flip` ou le `BltFast` de `3dhelp`) et lit les manettes. Verifie au desassembleur :
    // `Legacy/Raw/Winsys3d/FUN_00a05a00.disasm.txt`.
    // ============================================================================================

    private const int Device_AttendreBalayage = 0xd8 / 4;
    private const int Device_Presenter = 0xb8 / 4;

    /// <summary>Le `DISPENV` : cinq mots.</summary>
    private const int MotsDispEnv = 5;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a059e0 @ 0x00a059e0 (ordinal 656) — `GetDispEnv` : ressort les
    /// cinq mots. **Pas importe par la reconstruction** ; ecrit pour la meme raison que 659.
    /// </summary>
    internal static void GetDispEnv(void* env)
    {
        uint* destination = (uint*)env;
        uint* casier = Winsys3dData.DispEnv_011be6b8;
        for (int i = 0; i < MotsDispEnv; i++) destination[i] = casier[i];
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05a00 @ 0x00a05a00 (ordinal 657) — `PutDispEnv`, `ret 4`, rend
    /// toujours 0. Une largeur affichee d'au plus 320 (comparee en `short` signe, `cmp word [eax+4],
    /// 140h ; jg`) centre l'image par <see cref="Winsys3dDecalageEcran.SetAbsShift"/> ; au-dela,
    /// zoom, decalages et decalage d'ecran reviennent a zero et les tables de conversion passent
    /// aux deux tables de la grande surface (X `0x00a34fb8`, Y `0x00a5c0b8`). Puis, sauf image
    /// sautee, attente du balayage et presentation ; enfin la lecture des manettes, tampons
    /// compris.
    /// </summary>
    internal static int PutDispEnv(void* env)
    {
        uint* source = (uint*)env;
        uint* casier = Winsys3dData.DispEnv_011be6b8;
        for (int i = 0; i < MotsDispEnv; i++) casier[i] = source[i];

        if (*(short*)((byte*)env + 4) <= 0x140)
        {
            // `movsx` des deux `short`, soustraction, `sar 1` : une division arrondie vers le bas.
            Winsys3dDecalageEcran.SetAbsShift(
                (0x140 - Winsys3dData.DispLargeur_011be6bc) >> 1,
                (0xf0 - Winsys3dData.DispHauteur_011be6be) >> 1);
        }
        else
        {
            Winsys3dData.Zoom_00a2c498 = 0;
            Winsys3dData.DecalX_011be778 = 0;
            Winsys3dData.DecalY_011be77c = 0;
            Winsys3dData.TableEcranX_00a2c4a0 = Winsys3dData.TableGrandeSurfaceX_00a34fb8;
            Winsys3dData.TableEcranY_00a2c4a4 = Winsys3dData.TableGrandeSurfaceY_00a5c0b8;
            Winsys3dData.DecalEcranX_011be770 = 0;
            Winsys3dData.DecalEcranY_011be774 = 0;
        }

        if (Winsys3dData.Garde_00dedc44 == 0 && Winsys3dData.VSyncDelayed_011be67c == 0)
        {
            ((delegate* unmanaged[Thiscall]<void*, int, int, void>)
                Winsys3dData.DeviceVtable[Device_AttendreBalayage])(Winsys3dData.Device_011be760, 1, 0);
            ((delegate* unmanaged[Thiscall]<void*, void>)
                Winsys3dData.DeviceVtable[Device_Presenter])(Winsys3dData.Device_011be760);
        }

        Winsys3dManettes.LireManettes(1);
        return 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a06160 @ 0x00a06160 (ordinal 688) — `ClearOTagR` : chaine la table
    /// d'ordonnancement a l'envers, chaque entree pointant sur la precedente, et rend sa tete.
    ///
    /// **LA LISTE EST ADRESSEE SUR VINGT-QUATRE BITS.** C'est la convention que `Documentation/
    /// DLL/winsys3d.md` documente et sur laquelle le depot a deja perdu une journee : l'octet de
    /// poids fort de chaque mot n'est pas une adresse, c'est la taille de la primitive. Ce corps
    /// l'efface en chainant ; c'est `DrawOTag` qui le relit.
    ///
    /// La pseudo-C rend l'affectation ainsi :
    ///
    ///     *p = ((uint)precedent ^ *p &amp; 0xffffff) &amp; 0xffffff ^ *p &amp; 0xffffff;
    ///
    /// **C'est `precedent &amp; 0xffffff`, et rien d'autre.** Avec <c>b = *p &amp; 0xffffff</c>, donc
    /// d'octet de poids fort nul : <c>((a ^ b) &amp; 0xffffff) ^ b = ((a &amp; 0xffffff) ^ b) ^ b
    /// = a &amp; 0xffffff</c>. Le desassemblage confirme que le compilateur a bien emis la danse
    /// entiere — `and ecx,0FFFFFFh ; xor ebx,ecx ; and ebx,0FFFFFFh ; xor ebx,ecx` —, ce qui est
    /// la forme qu'un MSVC de 1996 donnait a l'ecriture d'un champ de bits de 24 bits. La forme
    /// simplifiee est donc une reconstruction, pas un raccourci : les deux ecrivent les memes
    /// octets.
    /// </summary>
    internal static uint* ClearOTagR(uint* ot, int count)
    {
        uint* precedent = ot;
        for (int reste = count - 1; reste > 0; reste--)
        {
            uint* courant = precedent + 1;
            *courant = (uint)precedent & 0xffffff;
            precedent = courant;
        }

        // La sentinelle de fin de liste, que `DrawOTag` reconnait a l'arret de son parcours.
        *ot = 0xffffff;

        return ot + count - 1;
    }
}
