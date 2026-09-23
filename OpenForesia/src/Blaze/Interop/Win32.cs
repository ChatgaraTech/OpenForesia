// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La surface P/Invoke du systeme : kernel32, user32, gdi32, winmm, ole32, ddraw, msvcrt,
// dinput.
//
// Une seule classe pour huit DLL, parce que la frontiere qui compte pour un lecteur est
// "c'est le systeme", pas "c'est user32 plutot que gdi32". Aucun second implementeur : pas
// d'interface.
//
// `static` par NECESSITE et non par paresse : [DllImport] exige `extern static`, et il n'y a
// aucun etat a porter. Une classe statique est ici la forme correcte.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

internal static unsafe class Win32
{
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern IntPtr LoadLibraryA(byte* lpLibFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern IntPtr GetProcAddress(IntPtr hModule, byte* lpProcName);

    [DllImport("dinput.dll", ExactSpelling = true)]
    internal static extern int DirectInputCreateA(IntPtr hinst, uint version, void** ppDI, void* unkOuter);

    // Ghidra: memmove â€” FUN_00547a00 is 420 lines of unrolled MSVC block copy. Per the project's
    // rule, a recognised CRT routine is called rather than transcribed.
    [DllImport("msvcrt.dll", EntryPoint = "memmove", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void* CrtMemMove(void* dest, void* src, nuint count);

    [DllImport("kernel32.dll")] internal static extern uint GetVersion();

    [DllImport("kernel32.dll")] internal static extern byte* GetCommandLineA();

    [DllImport("kernel32.dll")] internal static extern void GetStartupInfoA(Blaze.STARTUPINFOA* lpStartupInfo);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern IntPtr GetModuleHandleA(byte* lpModuleName);

    // Appele par `SetTimingMode` de winsys3d (Libraries/winsys3d/Cadence.cs).
    [DllImport("kernel32.dll")] internal static extern int QueryPerformanceCounter(long* lpPerformanceCount);

    // La pompe de messages de winsys3d, FUN_00a01d70 (Libraries/winsys3d/Cadence.cs).
    [DllImport("user32.dll")] internal static extern int TranslateMessage(Blaze.MSG* lpMsg);
    [DllImport("user32.dll")] internal static extern int WaitMessage();
    [DllImport("kernel32.dll")] internal static extern IntPtr SetUnhandledExceptionFilter(IntPtr lpTopLevelExceptionFilter);

    // L'initialisation du device de winsys3d, FUN_00a06420 / FUN_00a03110 (Libraries/winsys3d/Device.cs).
    [DllImport("winmm.dll")] internal static extern uint timeSetEvent(uint uDelay, uint uResolution, IntPtr lpTimeProc, nuint dwUser, uint fuEvent);
    [DllImport("winmm.dll")] internal static extern uint timeKillEvent(uint uTimerID);
    [DllImport("kernel32.dll")] internal static extern int QueryPerformanceFrequency(long* lpFrequency);
    [DllImport("kernel32.dll")] internal static extern int FreeLibrary(IntPtr hLibModule);
    [DllImport("user32.dll")] internal static extern IntPtr SetWindowLongA(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    // Le dialogue de choix du pilote de winsys3d, Setup3DDevice / FUN_00a010d0 (Libraries/winsys3d/Device.cs).
    // L'image de winsys3d (Libraries/winsys3d/Image.cs) : ses ressources, lues dans le fichier comme des donnees.
    [DllImport("kernel32.dll")] internal static extern IntPtr LoadLibraryExA(byte* lpLibFileName, IntPtr hFile, uint dwFlags);
    [DllImport("kernel32.dll")] internal static extern byte* lstrcatA(byte* lpString1, byte* lpString2);
    [DllImport("kernel32.dll")] internal static extern uint GetPrivateProfileStringA(byte* lpAppName, byte* lpKeyName, byte* lpDefault, byte* lpReturnedString, uint nSize, byte* lpFileName);
    // La procedure du dialogue de choix du pilote, FUN_00a012f0 (Libraries/winsys3d/Device.cs).
    [DllImport("user32.dll")] internal static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);
    [DllImport("user32.dll")] internal static extern nint SendMessageA(IntPtr hWnd, uint Msg, nuint wParam, nint lParam);
    [DllImport("user32.dll")] internal static extern nint SendDlgItemMessageA(IntPtr hDlg, int nIDDlgItem, uint Msg, nuint wParam, nint lParam);
    [DllImport("user32.dll")] internal static extern int SetDlgItemTextA(IntPtr hDlg, int nIDDlgItem, byte* lpString);
    [DllImport("user32.dll")] internal static extern int EndDialog(IntPtr hDlg, nint nResult);
    [DllImport("gdi32.dll")] internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    [DllImport("gdi32.dll")] internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);
    [DllImport("gdi32.dll")] internal static extern int SetStretchBltMode(IntPtr hdc, int mode);
    [DllImport("gdi32.dll")] internal static extern int StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, uint rop);
    [DllImport("gdi32.dll")] internal static extern int PatBlt(IntPtr hdc, int x, int y, int w, int h, uint rop);
    [DllImport("gdi32.dll")] internal static extern int DeleteDC(IntPtr hdc);
    [DllImport("user32.dll")] internal static extern nint DialogBoxIndirectParamA(IntPtr hInstance, byte* hDialogTemplate, IntPtr hWndParent, IntPtr lpDialogFunc, nint dwInitParam);
    [DllImport("user32.dll")] internal static extern IntPtr CreateIconFromResourceEx(byte* presbits, uint dwResSize, int fIcon, uint dwVer, int cxDesired, int cyDesired, uint Flags);
    [DllImport("user32.dll")] internal static extern nint CallWindowProcA(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, nuint wParam, nint lParam);
    [DllImport("user32.dll")] internal static extern nint DialogBoxParamA(IntPtr hInstance, byte* lpTemplateName, IntPtr hWndParent, IntPtr lpDialogFunc, nint dwInitParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern uint GetModuleFileNameA(IntPtr hModule, byte* lpFilename, uint nSize);

    [DllImport("kernel32.dll")] internal static extern IntPtr HeapCreate(uint flOptions, nuint dwInitialSize, nuint dwMaximumSize);

    [DllImport("kernel32.dll")] internal static extern IntPtr GetCurrentProcess();

    [DllImport("kernel32.dll")] internal static extern int TerminateProcess(IntPtr hProcess, uint uExitCode);

    [DllImport("kernel32.dll")] internal static extern void ExitProcess(uint uExitCode);

    [DllImport("kernel32.dll")] internal static extern char* GetEnvironmentStringsW();

    [DllImport("kernel32.dll")] internal static extern int FreeEnvironmentStringsW(char* lpszEnvironmentBlock);

    [DllImport("kernel32.dll")] internal static extern int WideCharToMultiByte(uint CodePage, uint dwFlags, char* lpWideCharStr, int cchWideChar, byte* lpMultiByteStr, int cbMultiByte, byte* lpDefaultChar, int* lpUsedDefaultChar);

    [DllImport("kernel32.dll")] internal static extern int IsDBCSLeadByte(byte TestChar);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern uint GetDriveTypeA(byte* lpRootPathName);

    [DllImport("kernel32.dll")] internal static extern uint GetFileSize(IntPtr hFile, uint* lpFileSizeHigh);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    internal static extern IntPtr CreateFileA(byte* lpFileName, uint dwDesiredAccess, uint dwShareMode, void* lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

    [DllImport("kernel32.dll")] internal static extern int CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern uint GetPrivateProfileIntA(byte* lpAppName, byte* lpKeyName, int nDefault, byte* lpFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern int WritePrivateProfileStringA(byte* lpAppName, byte* lpKeyName, byte* lpString, byte* lpFileName);

    // Ghidra: FUN_005129e0 -> DirectDrawCreate(NULL, &lpDD, NULL).
    [DllImport("ddraw.dll")] internal static extern int DirectDrawCreate(void* lpGUID, void** lplpDD, void* pUnkOuter);

    [DllImport("ole32.dll")] internal static extern int CoInitialize(void* pvReserved);

    [DllImport("ole32.dll")] internal static extern void CoUninitialize();

    [DllImport("user32.dll", CharSet = CharSet.Ansi)] internal static extern int MessageBoxA(IntPtr hWnd, byte* lpText, byte* lpCaption, uint uType);

    [DllImport("user32.dll")] internal static extern int DestroyWindow(IntPtr hWnd);

    [DllImport("gdi32.dll")] internal static extern int DeleteObject(IntPtr hObject);

    [DllImport("winmm.dll")] internal static extern uint waveOutGetNumDevs();

    [DllImport("winmm.dll")] internal static extern int waveOutGetVolume(IntPtr hwo, uint* pdwVolume);

    [DllImport("winmm.dll")] internal static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

    // Win32 APIs needed by RunMainLoop's own dependency graph (movie/intro-video playback window,
    // resource-file font loading, and the joystick/critical-section plumbing in FUN_00504460).
    [DllImport("user32.dll", CharSet = CharSet.Ansi)] internal static extern ushort RegisterClassA(Blaze.WNDCLASSA* lpWndClass);

    [DllImport("user32.dll")] internal static extern int UnregisterClassA(byte* lpClassName, IntPtr hInstance);

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    internal static extern IntPtr CreateWindowExA(uint dwExStyle, byte* lpClassName, byte* lpWindowName, uint dwStyle,
    int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, void* lpParam);

    [DllImport("user32.dll")] internal static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")] internal static extern int MoveWindow(IntPtr hWnd, int x, int y, int w, int h, int bRepaint);

    [DllImport("user32.dll", CharSet = CharSet.Ansi)] internal static extern IntPtr LoadCursorA(IntPtr hInstance, IntPtr lpCursorName);

    [DllImport("gdi32.dll")] internal static extern IntPtr GetStockObject(int fnObject);

    [DllImport("user32.dll")] internal static extern int PeekMessageA(Blaze.MSG* lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

    [DllImport("user32.dll")] internal static extern int DispatchMessageA(Blaze.MSG* lpMsg);

    [DllImport("user32.dll", CharSet = CharSet.Ansi)] internal static extern nint DefWindowProcA(IntPtr hWnd, uint msg, nuint wParam, nint lParam);

    [DllImport("user32.dll")] internal static extern void PostQuitMessage(int nExitCode);

    [DllImport("user32.dll")] internal static extern IntPtr SetFocus(IntPtr hWnd);

    [DllImport("user32.dll")] internal static extern short GetAsyncKeyState(int vKey);

    [DllImport("kernel32.dll")] internal static extern void Sleep(uint milliseconds);

    [DllImport("winmm.dll")] internal static extern uint timeGetTime();

    [DllImport("user32.dll")] internal static extern IntPtr SetTimer(IntPtr hWnd, nuint nIDEvent, uint uElapse, IntPtr lpTimerFunc);

    [DllImport("user32.dll")] internal static extern int KillTimer(IntPtr hWnd, nuint uIDEvent);

    [DllImport("user32.dll")] internal static extern int PostMessageA(IntPtr hWnd, uint msg, nuint wParam, nint lParam);

    [DllImport("kernel32.dll")] internal static extern void RaiseException(uint dwExceptionCode, uint dwExceptionFlags, uint nNumberOfArguments, nint* lpArguments);

    [DllImport("winmm.dll")] internal static extern uint joySetCapture(IntPtr hWnd, uint uJoyID, uint uPeriod, int fChanged);

    [DllImport("winmm.dll")] internal static extern uint joyReleaseCapture(uint uJoyID);

    // `JOYINFO` = quatre UINT : wXpos, wYpos, wZpos, wButtons. Rend 0 (JOYERR_NOERROR) si lu.
    [DllImport("winmm.dll")] internal static extern uint joyGetPos(uint uJoyID, uint* pji);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern byte* lstrcpyA(byte* lpString1, byte* lpString2);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern int lstrcmpiA(byte* lpString1, byte* lpString2);

    [DllImport("gdi32.dll")] internal static extern int StretchDIBits(IntPtr hdc, int xDest, int yDest, int destW, int destH,
    int xSrc, int ySrc, int srcW, int srcH, void* lpBits, void* lpBitsInfo, uint iUsage, uint rop);

    [DllImport("user32.dll")] internal static extern int SetRect(int* lprc, int left, int top, int right, int bottom);

    [DllImport("ddraw.dll")] internal static extern int DirectDrawCreate(void* lpGuid, out IntPtr lplpDD, void* pUnkOuter);

    // Ghidra: DirectSoundCreate @ 0x00545c68 (IAT 0x006084b8). Son unique site d'appel dans tout
    // le binaire est 0x00512f20, au coeur du corps que Ghidra n'a pas decompile (LAB_00512ef0) :
    // c'est pourquoi REVERSE_STATUS #26 a longtemps conclu qu'il n'en existait aucun.
    [DllImport("dsound.dll")] internal static extern int DirectSoundCreate(void* lpcGuidDevice, void** ppDS, void* pUnkOuter);

    // Ghidra: SetWindowTextA, appele par LAB_00512ef0 avec le titre s_Blaze___Blade_005631d8.
    [DllImport("user32.dll", CharSet = CharSet.Ansi)] internal static extern int SetWindowTextA(IntPtr hWnd, byte* lpString);

    [DllImport("kernel32.dll")] internal static extern uint SetFilePointer(IntPtr hFile, int lDistanceToMove, void* lpDistanceToMoveHigh, uint dwMoveMethod);

    [DllImport("kernel32.dll")] internal static extern int ReadFile(IntPtr hFile, void* lpBuffer, uint nNumberOfBytesToRead, uint* lpNumberOfBytesRead, void* lpOverlapped);

    // Used by the music-streaming subsystem (Ghidra: FUN_005131d0 / FUN_005135d0).
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern IntPtr CreateEventA(void* lpEventAttributes, int bManualReset, int bInitialState, byte* lpName);

    [DllImport("kernel32.dll")] internal static extern int SetEvent(IntPtr hEvent);

    [DllImport("kernel32.dll")] internal static extern int ResetEvent(IntPtr hEvent);

    [DllImport("kernel32.dll")] internal static extern uint WaitForMultipleObjects(uint nCount, IntPtr* lpHandles, int bWaitAll, uint dwMilliseconds);

    [DllImport("kernel32.dll")] internal static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

    [DllImport("kernel32.dll")] internal static extern IntPtr CreateThread(void* lpThreadAttributes, nuint dwStackSize, IntPtr lpStartAddress, void* lpParameter, uint dwCreationFlags, uint* lpThreadId);

    [DllImport("kernel32.dll")] internal static extern void* VirtualAlloc(void* lpAddress, nuint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")] internal static extern nuint VirtualQuery(void* lpAddress, out Blaze.MEMORY_BASIC_INFORMATION lpBuffer, nuint dwLength);

    // -----------------------------------------------------------------------------------------
    // Ce que les DLL d'aide de 1998 appellent, et que `src/Libraries/` reprend en les transcrivant.
    // Ces declarations ne servent a aucun corps de `Blaze.c` : elles sont ici parce que c'est le
    // seul endroit du depot ou vit la surface P/Invoke du systeme, et qu'une DLL reconstruite
    // appelle Win32 exactement comme le jeu le fait.
    // -----------------------------------------------------------------------------------------

    // `wvsprintfA` et non `sprintf` : c'est ce que wsyskern appelle, et son jeu de conversions est
    // plus pauvre que celui de la CRT (%s %d %u %x %c %%, pas de flottant). Transcrire les appels
    // vers la CRT changerait le rendu des messages d'erreur sur les formats que wvsprintf refuse.
    [DllImport("user32.dll", CharSet = CharSet.Ansi)] internal static extern int wvsprintfA(byte* lpOutput, byte* lpFormat, void* arglist);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern IntPtr FindResourceA(IntPtr hModule, byte* lpName, byte* lpType);
    [DllImport("kernel32.dll")] internal static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);
    [DllImport("kernel32.dll")] internal static extern void* LockResource(IntPtr hResData);
    [DllImport("kernel32.dll")] internal static extern int FreeResource(IntPtr hResData);

    [DllImport("user32.dll")] internal static extern IntPtr GetDC(IntPtr hWnd);
    [DllImport("user32.dll")] internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")] internal static extern IntPtr CreatePalette(void* lplgpl);
    [DllImport("gdi32.dll")] internal static extern IntPtr SelectPalette(IntPtr hdc, IntPtr hpal, int bForceBackground);
    [DllImport("gdi32.dll")] internal static extern uint RealizePalette(IntPtr hdc);
    [DllImport("gdi32.dll")] internal static extern IntPtr CreateDIBitmap(IntPtr hdc, void* lpbmih, uint fdwInit, void* lpbInit, void* lpbmi, uint fuUsage);

    // La section critique de `winsys3d` (ordinaux 40 et 41, `&DAT_00b6d6d8`). Ce sont les VRAIES
    // fonctions Win32 et non un verrou .NET : `LeaveCriticalSection` sans possession ne dit rien,
    // la ou `Monitor.Exit` leve. Le jeu relache une fois de trop a l'extinction — voir
    // `Libraries/winsys3d/SectionCritique.cs`.
    [DllImport("kernel32.dll")] internal static extern void InitializeCriticalSection(void* lpCriticalSection);
    [DllImport("kernel32.dll")] internal static extern void EnterCriticalSection(void* lpCriticalSection);
    [DllImport("kernel32.dll")] internal static extern void LeaveCriticalSection(void* lpCriticalSection);

    // La famille de fichiers de la carte memoire (`winsys3d`, ordinaux 18 a 25, 29 et 30). Voir
    // `Libraries/winsys3d/CarteMemoire.cs` : ce sont des enveloppes d'une ligne autour de ces
    // appels-ci, plus la resolution du descripteur en chemin.
    //
    // `lpFindFileData` est un `void*` et non une structure : les deux corps qui l'emploient
    // lisent `WIN32_FIND_DATAA` par offsets (nFileSizeLow a 0x20, cFileName a 0x2c), exactement
    // comme le cadre de pile de l'original.
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern IntPtr FindFirstFileA(byte* lpFileName, void* lpFindFileData);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern int FindNextFileA(IntPtr hFindFile, void* lpFindFileData);
    [DllImport("kernel32.dll")] internal static extern int FindClose(IntPtr hFindFile);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern int DeleteFileA(byte* lpFileName);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern int MoveFileA(byte* lpExistingFileName, byte* lpNewFileName);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern int SetCurrentDirectoryA(byte* lpPathName);
    [DllImport("kernel32.dll")] internal static extern int WriteFile(IntPtr hFile, void* lpBuffer, uint nNumberOfBytesToWrite, uint* lpNumberOfBytesWritten, void* lpOverlapped);

}
