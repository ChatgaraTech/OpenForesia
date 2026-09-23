// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`ret` et `ret 4`, EAX = l'ancienne valeur).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// UN CASIER, UN LECTEUR, UN ECRIVAIN — ordinaux 135 et 136
//
// Deux corps de trois instructions autour d'une seule globale :
//
//     ordinal 135   FUN_00a05760   `mov eax,[11BE6A4h] ; ret`          la lit — PAS IMPORTE
//     ordinal 136   FUN_00a05770   l'echange et rend l'ancienne        importe, un seul site
//
// `DAT_011be6a4` a **trois references dans les 27 000 lignes de `winsys3d.c`**, et ce sont ces
// trois-la. Aucune autre fonction de la DLL ne la consulte : ce n'est pas de l'etat dont la DLL
// se sert, c'est un casier qu'elle prete au jeu. Exactement la situation de `DAT_011be6d8` avec
// les ordinaux 659/660 (voir `ListeAffichage.cs`), et c'est ce qui rend la paire portable.
//
// L'ordinal 135 n'est importe par personne. Il part quand meme : porter l'ecrivain sans le
// lecteur laisserait 135, natif, relire une globale que plus rien n'ecrit. Une composante se
// porte entiere.
//
// ============================================================================================
// LE ROLE N'EST PAS ETABLI, ET LES DEUX CORPS GARDENT DONC UN NOM D'ADRESSE
//
// Ce qu'on sait :
//
//   - le jeu appelle l'ordinal 136 **une seule fois, avec 0**, au demarrage — `Blaze.c` ligne
//     74470, entre l'ordinal 45 et `srand(1)`. Il jette la valeur rendue ;
//   - l'ordinal 136 est un echange qui rend l'ancienne valeur, forme typique d'un reglage
//     global qu'on veut pouvoir restaurer ;
//   - il vit dans le bloc d'ordinaux 129-138, ou l'ordinal 132 est la temporisation d'image et
//     l'ordinal 138 une souche vide.
//
// Cela suggere un niveau de trace ou un reglage de cadence — `Documentation/DLL/winsys3d.md`
// note « frame time offset ? » depuis le premier relevé, avec le point d'interrogation. Un
// echange rendant l'ancienne valeur, appele une fois avec 0, ressemble beaucoup au
// `SetGraphDebug` de la `libgpu`. **Rien ne le prouve**, et le depot ne renomme que ce qui est
// etabli : les deux corps s'appellent donc `Unknown_00a057…`, la globale `s_Unknown_011be6a4`,
// et le nom de ce FICHIER dit le voisinage d'ordinaux, pas le role.
//
// TODO(REVERSE): trancher entre « niveau de trace » et « reglage de cadence ». La DLL ne peut
// pas repondre — rien d'autre n'y lit la globale. Ce qui repondrait : le nom PSY-Q de l'ordinal
// 136, que `playst.dll` porte peut-etre (elle a les 758 noms de Sony) ; ou un appel natif a
// l'ordinal 135 apres avoir pose une valeur, pour voir si quoi que ce soit change a l'ecran.
// ============================================================================================

using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dCadence
{
    /// <summary>
    /// DAT_011be6a4 — le casier. Zero au chargement, comme le `.data` de la DLL, et le jeu y
    /// remet zero au demarrage. Role non etabli : voir le pave de tete.
    /// </summary>
    private static uint s_Unknown_011be6a4;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05760 @ 0x00a05760 (ordinal 135) — rend la valeur du casier,
    /// sans argument.
    ///
    /// **Aucun appelant en C#** : la reconstruction n'importe pas cet ordinal. Il est ici parce
    /// que le laisser natif lui ferait relire une globale que plus rien n'ecrirait.
    /// </summary>
    internal static uint Unknown_00a05760() => s_Unknown_011be6a4;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05770 @ 0x00a05770 (ordinal 136) — pose une valeur dans le
    /// casier et rend **l'ancienne**.
    ///
    /// Un seul site dans tout le jeu, au demarrage, avec 0, et la valeur rendue est jetee.
    /// </summary>
    internal static uint Unknown_00a05770(uint valeur)
    {
        uint precedente = s_Unknown_011be6a4;
        s_Unknown_011be6a4 = valeur;
        return precedente;
    }

    // ============================================================================================
    // LE MODE DE CADENCE — ordinal 822, `SetTimingMode` (2026-09-23), verifie au desassembleur
    // (`Legacy/Raw/Winsys3d/FUN_00a05260.disasm.txt`). Ses globales, elles, sont ALIASEES (voir
    // `Data.cs`) : l'attente d'image (132) les relit, encore native.
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c SetTimingMode @ 0x00a05260 (ordinal 822) — echange le mode de cadence
    /// et rend l'ancien ; puis recale l'horloge d'images sur le compteur de performance :
    /// `VSyncCounter = compteur / tops` (division SIGNEE 64 bits, `__alldiv`, dont on garde le mot
    /// bas), l'image attendue a `VSyncCounter + 3`, et l'echeance a `compteur + 3 . tops`, le
    /// produit fait sur 32 bits (`lea eax,[esi+esi*2]`) puis sign-etendu (`cdq`). `ret 4`.
    ///
    /// Le retour de `QueryPerformanceCounter` n'est pas teste, comme dans l'original.
    /// </summary>
    internal static int SetTimingMode(int mode)
    {
        int precedent = Winsys3dData.ModeCadence_00a2c3b0;
        Winsys3dData.ModeCadence_00a2c3b0 = mode;
        Win32.QueryPerformanceCounter(Winsys3dData.CompteurPerf_00dedd70);

        int tops = Winsys3dData.TopsParImage_011be684;
        long compteur = *Winsys3dData.CompteurPerf_00dedd70;
        Winsys3dData.VSyncCounter_00dedbf4 = (int)(compteur / tops);
        Winsys3dData.ImageAttendue_011be680 = Winsys3dData.VSyncCounter_00dedbf4 + 3;
        *Winsys3dData.EcheancePerf_011be690 = compteur + (tops * 3);
        return precedent;
    }

    // ============================================================================================
    // L'ATTENTE D'IMAGE — ordinal 132, le `VSync(n)` de la libgpu (2026-09-23), transcrit sur le
    // desassemblage (`Legacy/Raw/Winsys3d/FUN_00a053e0.disasm.txt`) : Ghidra en rendait les
    // comparaisons 64 bits sous forme de boucles imbriquees, et l'ordre des lectures compte (voir
    // LB-047). Table de sauts relue a 0x00a05730 : mode 0 -> 05646, 1 -> 054bf, 2 -> 05667,
    // 3 -> 0562b ; un mode hors de 0..3 (non signe) rend 1.
    //
    // Les notations : `c` le compteur de performance lu, `e` l'echeance (64 bits, 0x011be690),
    // `tops` les tops par image. Chaque attente tourne `PollMessages` puis relit le compteur tant
    // que `e > c`.
    // ============================================================================================

    /// <summary>
    /// Relit le compteur de performance et recale `VSyncCounter = c / tops` (`__alldiv`, signe) ;
    /// rend `c` et `tops`, que le corps garde en registres (`edi:esi`, `ebp`).
    /// </summary>
    private static long RelireCompteur(out int tops)
    {
        Win32.QueryPerformanceCounter(Winsys3dData.CompteurPerf_00dedd70);
        tops = Winsys3dData.TopsParImage_011be684;
        long c = *Winsys3dData.CompteurPerf_00dedd70;
        Winsys3dData.VSyncCounter_00dedbf4 = (int)(c / tops);
        return c;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a053e0 @ 0x00a053e0 (ordinal 132) — `VSync(n)` : attend la fin de
    /// l'image selon le mode de cadence (<see cref="SetTimingMode"/>). `n` negatif : rend le
    /// compteur d'images sans rien attendre ni journaliser. `n` nul compte pour une image.
    /// Rend 1, ou 0xEF quand `n` vaut 1 (et, en mode 0 ou 3-sans-attente, quand `n` est non nul).
    /// `ret 4`.
    /// </summary>
    internal static int VSync(int n)
    {
        int nombre = n == 0 ? 1 : n;
        long c = RelireCompteur(out int tops);
        if (n < 0) return Winsys3dData.VSyncCounter_00dedbf4;

        // Le journal : toutes les valeurs sont lues avant la premiere ecriture, puis ecrites dans
        // l'ordre du binaire — a l'indice 100 000, l'enregistrement recouvre ces globales-la.
        int indice = ++Winsys3dData.IndiceJournal_011be69c;
        int image = Winsys3dData.VSyncCounter_00dedbf4;
        int attendue = Winsys3dData.ImageAttendue_011be680;
        long e = *Winsys3dData.EcheancePerf_011be690;
        int rattrapage = Winsys3dData.Rattrapage_011be688;    // lu avant rec[2] (`0546d`)
        int* rec = (int*)(Winsys3dData.JournalImages_00dedd78 + indice * 0x28);
        rec[0] = image;
        rec[1] = attendue;
        rec[2] = (int)c;
        rec[3] = (int)(c >> 32);
        rec[4] = (int)e;
        rec[5] = (int)(e >> 32);
        rec[6] = rattrapage;
        rec[8] = 0;
        rec[7] = 0;
        rec[9] = nombre;

        switch ((uint)Winsys3dData.ModeCadence_00a2c3b0)
        {
            case 0:
                return n != 0 ? 0xef : 1;

            case 1:
            {
                if (Winsys3dData.Rattrapage_011be688 == 0 && e > c)
                {
                    rec[7] = (int)((uint)e - (uint)c);        // l'attente, mot bas seulement
                    do
                    {
                        PollMessages();
                        c = RelireCompteur(out tops);
                        e = *Winsys3dData.EcheancePerf_011be690;
                    } while (e > c);
                }

                Winsys3dData.ImageAttendue_011be680 += nombre;
                int produit = nombre * tops;
                *Winsys3dData.EcheancePerf_011be690 = e + produit;
                if (Winsys3dData.ImageAttendue_011be680 + 0x1e < Winsys3dData.VSyncCounter_00dedbf4)
                {
                    // Plus de 30 images de retard : on recale sur maintenant.
                    Winsys3dData.ImageAttendue_011be680 = nombre + Winsys3dData.VSyncCounter_00dedbf4;
                    *Winsys3dData.EcheancePerf_011be690 = c + produit;
                }
                if (n == 1) return 0xef;

                PollMessages();
                if (Winsys3dData.ImageAttendue_011be680 >= Winsys3dData.VSyncCounter_00dedbf4)
                {
                    if (Winsys3dData.Rattrapage_011be688 != 0) Winsys3dData.Rattrapage_011be688--;
                    Winsys3dData.VSyncDelayed_011be67c = 0;
                    Winsys3dData.DerniereImage_011be678 = Winsys3dData.VSyncCounter_00dedbf4;
                    return 1;
                }
                int retard = Winsys3dData.VSyncCounter_00dedbf4 - Winsys3dData.ImageAttendue_011be680;
                int dernier = Winsys3dData.IndiceJournal_011be69c;
                Winsys3dData.Rattrapage_011be688 = 3;
                Winsys3dData.VSyncDelayed_011be67c = 1;
                Winsys3dData.DerniereImage_011be678 = Winsys3dData.VSyncCounter_00dedbf4;
                ((int*)(Winsys3dData.JournalImages_00dedd78 + dernier * 0x28))[8] = retard;
                return 1;
            }

            case 3:
            {
                int bascule = Winsys3dData.BasculeMode3_011be698 == 0 ? 1 : 0;
                Winsys3dData.BasculeMode3_011be698 = bascule;
                Winsys3dData.VSyncDelayed_011be67c = bascule;
                if (bascule != 0) return n != 0 ? 0xef : 1;   // le code du mode 0
                nombre *= 2;
                goto case 2;
            }

            case 2:
            {
                if (Winsys3dData.Rattrapage_011be688 == 0 && e > c)
                {
                    do
                    {
                        PollMessages();
                        c = RelireCompteur(out tops);
                        e = *Winsys3dData.EcheancePerf_011be690;
                    } while (e > c);
                }

                Winsys3dData.ImageAttendue_011be680 += nombre;
                int produit = nombre * tops;
                e += produit;
                *Winsys3dData.EcheancePerf_011be690 = e;
                if (e > c)
                {
                    *Winsys3dData.EcheancePerf_011be690 = c + produit;
                    Winsys3dData.ImageAttendue_011be680 = nombre + Winsys3dData.VSyncCounter_00dedbf4;
                }
                return n == 1 ? 0xef : 1;
            }

            default:
                return 1;
        }
    }

    // ============================================================================================
    // LE MINUTEUR DE 17 MS — `LAB_00a06360` (2026-09-23), transcrit sur le desassemblage
    // (`Legacy/Raw/Winsys3d/LAB_00a06360.disasm.txt`). Ghidra ne l'a pas decompile : il n'est
    // atteint que par adresse (`push 0A06360h`, trois `timeSetEvent`). Il tourne sur le fil du
    // minuteur multimedia de Windows, d'ou `[UnmanagedCallersOnly]` : Windows l'appelle en `stdcall`
    // a cinq arguments (`ret 14h`), qu'il n'utilise pas.
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c LAB_00a06360 @ 0x00a06360 — le tick de 17 ms, **sous la section critique**
    /// des ordinaux 40/41 : avance `VSyncCounter` ; si le compteur racine 3 est demarre en mode
    /// interruption (`0x1000`), livre l'evenement `(0xF2000003, 2)` ; appelle le rappel de
    /// l'ordinal 133 s'il y en a un. Puis, hors section, **se reprogramme** : le minuteur est a
    /// coup unique (`fuEvent = 0`), chaque tick arme le suivant.
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
    internal static void TickMinuteur(uint id, uint message, nuint utilisateur, nuint reserve1, nuint reserve2)
    {
        Winsys3dSectionCritique.Entrer();
        Winsys3dData.VSyncCounter_00dedbf4 = Winsys3dData.VSyncCounter_00dedbf4 + 1;
        if (Winsys3dData.CompteurRacineActif_00dedc14 != 0
            && (Winsys3dData.ModeCompteurRacine_00dedc08 & 0xff00) == 0x1000)
            Winsys3dEvenements.DeliverEvent(0xf2000003, 2);
        void* rappel = Winsys3dData.RappelTick_011be6a0;
        if (rappel != null) ((delegate* unmanaged[Stdcall]<void>)rappel)();
        Winsys3dSectionCritique.Sortir();
        Winsys3dData.Minuteur_011be6d0 = Win32.timeSetEvent(0x11, 0, AdresseTickMinuteur, 0, 0);
    }

    /// <summary>L'adresse de <see cref="TickMinuteur"/>, a la place de `0x00a06360` dans les trois `timeSetEvent`.</summary>
    internal static System.IntPtr AdresseTickMinuteur
        => (System.IntPtr)(delegate* unmanaged[Stdcall]<uint, uint, nuint, nuint, nuint, void>)&TickMinuteur;

    // ============================================================================================
    // LA POMPE DE MESSAGES — `PollMessages` (784) et `FUN_00a01d70`, verifies au desassembleur
    // (`Legacy/Raw/Winsys3d/FUN_00a01d60.disasm.txt`). Ici parce que l'attente d'image la fait
    // tourner a chaque tour de boucle ; elle lit aussi les manettes (`Manettes.cs`).
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a01d60 @ 0x00a01d60 — le filtre d'exceptions que la pompe installe
    /// avant son `RaiseException` : `or eax,-1 ; ret 4`, soit `EXCEPTION_CONTINUE_EXECUTION` pour
    /// toute exception non geree. Porte avec l'image de la DLL, qui n'a plus de code a designer.
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
    private static int FiltreExceptions(void* informations) => -1;

    /// <summary>La case `+0x3c` de la vtable du device : non nul s'il faut continuer sans attendre de message.</summary>
    private const int Device_Actif = 0x3c / 4;

    /// <summary>
    /// Ghidra: winsys3d.c PollMessages @ 0x00a01e20 (ordinal 784) — vide la file de messages, puis
    /// lit les manettes.
    /// </summary>
    internal static void PollMessages()
    {
        ViderMessages();
        Winsys3dManettes.LireManettes(1);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a01d70 @ 0x00a01d70 — `PeekMessageA(PM_REMOVE)` en boucle. File
    /// vide : si le device (`+0x3c`) rend 0, `WaitMessage` et on reboucle ; sinon on sort.
    ///
    /// Sur `WM_QUIT` : `DAT_00dedbf0 = 1`, l'extinction `FUN_00a069b0(-2)`, puis le filtre
    /// d'exceptions `FUN_00a01d60` et `RaiseException(0xE0000001)`. Comme partout dans le depot, cette
    /// exception est representee par l'exception managee de <c>Blaze.RaiseQuitException</c>, que le
    /// demarrage rattrape la ou l'original reprend la main ; les trois lignes qui la suivent — la
    /// restauration du filtre et `ExitProcess(1)` — ne s'executent que si l'exception est reprise,
    /// et sont gardees telles quelles.
    /// </summary>
    private static void ViderMessages()
    {
        Blaze.Blaze.MSG message;
        int encore;
        do
        {
            encore = Win32.PeekMessageA(&message, System.IntPtr.Zero, 0, 0, 1);
            if (encore == 0)
            {
                int actif = ((delegate* unmanaged[Thiscall]<void*, int>)Winsys3dData.DeviceVtable[Device_Actif])(
                    Winsys3dData.Device_011be760);
                if (actif == 0)
                {
                    Win32.WaitMessage();
                    encore = 1;
                }
            }
            else
            {
                if (message.message == 0x12)   // WM_QUIT
                {
                    Winsys3dData.QuitteRecu_00dedbf0 = 1;
                    Winsys3dDevice.Unknown_00a069b0(-2);
                    System.IntPtr precedent = Win32.SetUnhandledExceptionFilter(
                        (System.IntPtr)(delegate* unmanaged[Stdcall]<void*, int>)&FiltreExceptions);
                    Blaze.Blaze.RaiseQuitException();
                    Win32.SetUnhandledExceptionFilter(precedent);
                    Win32.ExitProcess(1);
                }
                Win32.TranslateMessage(&message);
                Win32.DispatchMessageA(&message);
            }
        } while (encore != 0);
    }
}
