// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur**, corps par corps.
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LE TEXTE A L'ECRAN — ce que `DrawOTag` dessine autour du rejeu (2026-09-23)
//
// Trois mecanismes, tous alimentes par des exports que le jeu appelle ailleurs, et vides ici a
// chaque image :
//
//   1. **les fenetres de texte** (`DAT_00a2e8f0`, 0x24 octets chacune) : des caracteres en
//      attente, dessines un par un en `SPRT` 8 x 16 depuis une police en texture. Passe de
//      textures : `LierPoliceFenetres` ; passe de dessin : `DessinerFenetres`, qui vide la file ;
//   2. **la console** (`DAT_00a2eaa0`, trente lignes de 0x51 octets en anneau) : meme police,
//      mais en `POLY_FT4` ;
//   3. **les textes GDI** (`DAT_00a2ea90`) : une file que `WinDrawText` remplit, ecrite apres
//      `EndScene` par `TextOutA` sur le HDC de la surface, puis videe.
//
// La police est une planche de 32 caracteres par rangee, 8 x 16 pixels : pour le code `c`,
// `u = c << 3` (tronque a l'octet) et `v = (c >> 1) & 0x70`.
//
// --------------------------------------------------------------------------------------------
// CE QUE GHIDRA DIT MAL
//
// **`FUN_00a02820` passe le HDC, pas son adresse.** Ghidra ecrit `FUN_00a01f00(puVar2, ...)`
// avec `puVar2 = local_4`, l'adresse de la locale. Le binaire fait `mov ecx,[esp] ; push ecx`
// (0x00a02846) : c'est la VALEUR que `device[0xc4]` vient d'y deposer. Meme chose pour
// `device[0xc8]`, qui rend le HDC.
//
// **Les caracteres d'une fenetre sont poses a la position D'AVANT l'avancee** : le paquet `SPRT`
// recoit x et y (`mov [esp+24h],di ; mov [esp+26h],bx`, 0x00a02581) avant que le caractere ne
// deplace le curseur. Le retour a la ligne automatique ne concerne donc que le caractere suivant.
// ============================================================================================

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dTexteEcran
{
    private const int TailleFenetre = 0x24;
    private const int LignesConsole = 30;
    private const int OctetsLigneConsole = 0x51;
    private const int Device_ObtenirDc = 0xc4 / 4;
    private const int Device_RendreDc = 0xc8 / 4;

    // GDI — `winsys3d.dll` les importe (table a 0x01215268..0x0121527c). P/Invoke, comme le veut la
    // regle du projet pour les appels Win32 reconnus.
    [DllImport("gdi32.dll")] private static extern int SetBkMode(IntPtr hdc, int mode);
    [DllImport("gdi32.dll")] private static extern uint SetTextColor(IntPtr hdc, uint couleur);
    [DllImport("gdi32.dll")] private static extern IntPtr SelectObject(IntPtr hdc, IntPtr objet);
    [DllImport("gdi32.dll")] private static extern int TextOutA(IntPtr hdc, int x, int y, byte* texte, int longueur);

    // ============================================================================================
    // Passe de textures — appele par `DrawOTag` apres le premier parcours de l'OT
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a027a0 @ 0x00a027a0 — prepare la police pour chaque fenetre de
    /// texte non vide, puis pour la console. Le nombre de fenetres est relu a chaque tour.
    /// </summary>
    internal static void LierPoliceFenetres()
    {
        for (int i = 0; i < Winsys3dData.NombreFenetresTexte_00a2ea98; i++)
            if (*(int*)(Fenetre(i) + 0x18) != 0) LierPoliceFenetre(i);
        LierPoliceConsole();
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a024a0 @ 0x00a024a0 — prepare la page de la police avec la CLUT
    /// de la fenetre : <c>DAT_00a2ea38</c> pour le genre 1, <c>DAT_00a2ea34</c> sinon.
    /// </summary>
    private static void LierPoliceFenetre(int i)
    {
        int clut = *(int*)(Fenetre(i) + 0x10) == 1
            ? Winsys3dData.ClutPolice_00a2ea38
            : Winsys3dData.ClutPoliceAutre_00a2ea34;
        Winsys3dCacheTextures.EnregistrerDemande((uint)Winsys3dData.PagePolice_00a2c198, (uint)clut, 0, 0, 0xff, 0x7f, 0);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a02340 @ 0x00a02340 — prepare la police de la console, si elle a
    /// quelque chose a montrer (debut different de fin, ou drapeau pose).
    /// </summary>
    private static void LierPoliceConsole()
    {
        if (Winsys3dData.ConsoleDebut_00a2f424 != Winsys3dData.ConsoleFin_00a2f420
            || Winsys3dData.ConsoleDrapeau_00a2f428 != 0)
            Winsys3dCacheTextures.EnregistrerDemande((uint)Winsys3dData.PagePolice_00a2c198, (uint)Winsys3dData.ClutPolice_00a2ea38,
                                       0, 0, 0xff, 0x7f, 0);
    }

    // ============================================================================================
    // Passe de dessin — appele par `DrawOTag` apres le second parcours, avant `EndScene`
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a027e0 @ 0x00a027e0 — dessine chaque fenetre de texte non vide,
    /// puis la console.
    /// </summary>
    internal static void DessinerFenetres()
    {
        for (int i = 0; i < Winsys3dData.NombreFenetresTexte_00a2ea98; i++)
            if (*(int*)(Fenetre(i) + 0x18) != 0) DessinerFenetre(i);
        DessinerConsole();
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a024e0 @ 0x00a024e0 — dessine les caracteres en attente d'une
    /// fenetre en `SPRT` 8 x 16, puis vide sa file.
    ///
    /// Le curseur part du coin de la fenetre. 8 (retour arriere) recule d'un caractere, 10 (fin de
    /// ligne) revient en debut de ligne suivante ; tout autre caractere avance d'un cran, passe a
    /// la ligne si le curseur depasse la largeur, et n'est dessine que s'il est au-dela de l'espace
    /// ET que la ligne tient dans la hauteur. La page de texture courante (le demi-mot
    /// <c>DAT_011be6ec</c>, celui que lit <c>SPRT</c>) est remplacee par celle de la police le temps
    /// de la fenetre, puis restauree — deux ecritures de 16 bits dans le binaire.
    /// </summary>
    private static void DessinerFenetre(int i)
    {
        byte* f = Fenetre(i);
        ushort pageSauvee = Winsys3dData.Demi_011be6ec;

        // Le paquet `SPRT` construit sur la pile : couleur neutre 0x80, 8 x 16, CLUT selon le genre.
        byte* sprt = stackalloc byte[0x14];
        sprt[4] = 0x80; sprt[5] = 0x80; sprt[6] = 0x80;
        *(ushort*)(sprt + 0x10) = 8;
        *(ushort*)(sprt + 0x12) = 0x10;
        *(ushort*)(sprt + 0x0e) = (ushort)(*(int*)(f + 0x10) == 1
            ? Winsys3dData.ClutPolice_00a2ea38
            : Winsys3dData.ClutPoliceAutre_00a2ea34);

        int largeur = *(int*)(f + 0x08);
        int x = *(int*)(f + 0x00);
        int y = *(int*)(f + 0x04);
        int hauteur = *(int*)(f + 0x0c);
        Winsys3dData.Demi_011be6ec = (ushort)Winsys3dData.PagePolice_00a2c198;

        for (int n = 0; n < *(int*)(f + 0x18); n++)
        {
            byte c = (*(byte**)(f + 0x20))[n];
            *(short*)(sprt + 0x08) = (short)x;
            *(short*)(sprt + 0x0a) = (short)y;
            sprt[0x0c] = (byte)(c << 3);
            sprt[0x0d] = (byte)((c >> 1) & 0x70);

            if (c == 8)
                x -= 8;
            else if (c == 10)
            {
                x = *(int*)(f + 0x00);
                y += 0x10;
            }
            else
            {
                x += 8;
                if (x - *(int*)(f + 0x00) > largeur)
                {
                    y += 0x10;
                    x = *(int*)(f + 0x00);
                }
                if (y - *(int*)(f + 0x04) <= hauteur && c > 0x20)
                    Winsys3dPrimitives.Sprt(sprt);
            }
        }

        *(int*)(f + 0x18) = 0;
        *(int*)(f + 0x1c) = 0;
        Winsys3dData.Demi_011be6ec = pageSauvee;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a02380 @ 0x00a02380 — dessine la console : les lignes de l'anneau,
    /// de celle qui suit <c>DAT_00a2f424 - 1</c> jusqu'a <c>DAT_00a2f420</c> incluse, une ligne de
    /// 16 pixels chacune depuis le haut de l'ecran, chaque caractere en `POLY_FT4` 8 x 16.
    /// </summary>
    private static void DessinerConsole()
    {
        if (Winsys3dData.ConsoleDebut_00a2f424 == Winsys3dData.ConsoleFin_00a2f420
            && Winsys3dData.ConsoleDrapeau_00a2f428 == 0)
            return;

        // Le paquet `POLY_FT4` construit sur la pile : couleur neutre, CLUT et page de la police.
        byte* quad = stackalloc byte[0x28];
        quad[4] = 0x80; quad[5] = 0x80; quad[6] = 0x80;
        *(ushort*)(quad + 0x0e) = (ushort)Winsys3dData.ClutPolice_00a2ea38;
        *(ushort*)(quad + 0x16) = (ushort)Winsys3dData.PagePolice_00a2c198;

        short y = 0;
        int ligne = Winsys3dData.ConsoleDebut_00a2f424 - 1;
        do
        {
            short x = 0;
            ligne++;
            if (ligne > LignesConsole - 1) ligne = 0;

            byte* car = Winsys3dData.ConsoleLignes_00a2eaa0 + ligne * OctetsLigneConsole;
            if (*car != 0)
            {
                do
                {
                    byte c = *car++;
                    byte u = (byte)(c * 8);
                    byte v = (byte)((c >> 1) & 0x70);
                    *(short*)(quad + 0x08) = x;              *(short*)(quad + 0x0a) = y;
                    quad[0x0c] = u;                          quad[0x0d] = v;
                    *(short*)(quad + 0x10) = (short)(x + 8); *(short*)(quad + 0x12) = y;
                    quad[0x14] = (byte)(u + 8);              quad[0x15] = v;
                    *(short*)(quad + 0x18) = x;              *(short*)(quad + 0x1a) = (short)(y + 0x10);
                    quad[0x1c] = u;                          quad[0x1d] = (byte)(v + 0x10);
                    *(short*)(quad + 0x20) = (short)(x + 8); *(short*)(quad + 0x22) = (short)(y + 0x10);
                    quad[0x24] = (byte)(u + 8);              quad[0x25] = (byte)(v + 0x10);
                    Winsys3dPrimitives.PolyFT4(quad);
                    x += 8;
                } while (*car != 0);
            }
            y += 0x10;
        } while (ligne != Winsys3dData.ConsoleFin_00a2f420);
    }

    // ============================================================================================
    // Les textes GDI — appele par `DrawOTag` apres `EndScene`
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a02820 @ 0x00a02820 — ecrit la file de textes GDI sur le HDC de la
    /// surface (<c>device[0xc4]</c>, rendu par <c>device[0xc8]</c>), puis vide la file, que
    /// l'obtention du HDC ait reussi ou non.
    /// </summary>
    internal static void EcrireTextesGdi()
    {
        if (Winsys3dData.FileTextesGdi_00a2ea90 == null) return;

        IntPtr hdc;
        int hr = ((delegate* unmanaged[Thiscall]<void*, IntPtr*, int>)
            Winsys3dData.DeviceVtable[Device_ObtenirDc])(Winsys3dData.Device_011be760, &hdc);
        if (hr >= 0)
        {
            EcrireFile(hdc, Winsys3dData.FileTextesGdi_00a2ea90);
            ((delegate* unmanaged[Thiscall]<void*, IntPtr, void>)
                Winsys3dData.DeviceVtable[Device_RendreDc])(Winsys3dData.Device_011be760, hdc);
        }
        Winsys3dData.FileTextesGdi_00a2ea90 = null;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a01f00 @ 0x00a01f00 — `ret 8`. Fond transparent, puis pour chaque
    /// maillon dont la police existe : couleur, police, et le texte ligne par ligne. Une ligne
    /// s'arrete au premier octet <c>&lt;= 0x0d</c> ; la suivante commence apres lui, une hauteur de
    /// ligne plus bas, jusqu'au zero final. Une ligne vide part quand meme, longueur 0.
    /// </summary>
    private static void EcrireFile(IntPtr hdc, byte* maillon)
    {
        SetBkMode(hdc, 1);                                   // TRANSPARENT
        do
        {
            int police = *(int*)(maillon + 4);
            if (Winsys3dData.PolicesGdi_00a2ea40[police] != IntPtr.Zero)
            {
                SetTextColor(hdc, Winsys3dData.CouleursGdi_00a2c1a0[police]);
                SelectObject(hdc, Winsys3dData.PolicesGdi_00a2ea40[police]);
                int y = *(int*)(maillon + 0x0c);
                byte* curseur = maillon + 0x13;
                do
                {
                    byte* debut = curseur + 1;
                    int longueur = 0;
                    curseur = debut;
                    while (debut[longueur] > 0x0d) { longueur++; curseur++; }
                    TextOutA(hdc, *(int*)(maillon + 8), y, debut, longueur);
                    y += Winsys3dData.HauteursLigneGdi_00a2ea68[*(int*)(maillon + 4)];
                } while (*curseur != 0);
            }
            maillon = *(byte**)maillon;
        } while (maillon != null);
    }

    private static byte* Fenetre(int i) => Winsys3dData.FenetresTexte_00a2e8f0 + i * TailleFenetre;
}
