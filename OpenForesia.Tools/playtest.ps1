<#
.SYNOPSIS
    Lance OpenForesia, navigue jusqu'a un ecran donne, observe, et rend un verdict.

.DESCRIPTION
    Le jeu ne s'observe qu'en tournant, et son clavier passe par DirectInput : aucun test ne se
    fait sans piloter la fenetre. Ce script encapsule tout ce qui a ete appris a ce sujet, pour
    que chaque session reparte d'ici plutot que de le redecouvrir.

    Voir test-protocol.md a la racine du depot pour le mode d'emploi et le raisonnement.

.EXAMPLE
    .\playtest.ps1 -Entry 1 -Seconds 25 -Shot
    Selection des personnages, 25 s d'observation, capture PNG.

.EXAMPLE
    .\playtest.ps1 -Entry -1 -Seconds 15 -Shot
    Reste sur le menu principal, sans rien valider.
#>
[CmdletBinding()]
param(
    # Entree du menu principal a valider. 0 = Debut du jeu, 1 = Selection des personnages,
    # 2 = Menu objets, 3 = Options, 4 = Quitter. -1 = ne rien valider, rester sur le menu.
    [int]$Entry = 1,

    # Duree d'observation apres validation, en secondes.
    [int]$Seconds = 25,

    # Capture la fenetre en PNG a la fin de l'observation.
    [switch]$Shot,

    # Variables OPENFORESIA_* supplementaires a poser, sans le prefixe.
    # Exemple : -Trace GLYPHS,VP  ->  OPENFORESIA_TRACE_GLYPHS=1, OPENFORESIA_TRACE_VP=1
    [string[]]$Trace = @(),

    # Touches a jouer une fois l'ecran atteint, dans l'ordre. Jetons reconnus :
    # ENTER ESC UP DOWN LEFT RIGHT PGUP PGDN, et WAIT<n> pour patienter n secondes.
    # Exemple : -Then DOWN,WAIT2,ENTER
    [string[]]$Then = @(),

    # Reconstruit la solution avant de lancer.
    [switch]$Build,

    # Laisse le jeu ouvert au lieu de le tuer.
    [switch]$KeepOpen,

    # Secondes d'attente avant que le menu principal soit interactif.
    [int]$MenuDelay = 12,

    # Duree d'un appui, en millisecondes. Voir test-protocol.md : trop court = perdu,
    # trop long = le curseur s'emballe.
    [int]$TapMs = 150,

    [int]$MaxTaps = 30,

    # Dossier de travail du processus lance. Par defaut celui de l'exe (comme un double-clic
    # Explorer) -- utile pour reproduire un lancement dont le CWD differe (ex. Rider "Run", ou
    # "dotnet run", mettent par defaut le dossier du .csproj), ce qui compte pour tout chemin
    # relatif que le jeu ouvre (ex. "save\...").
    [string]$WorkingDir = '',
    # Touche maintenue enfoncee de la prise de focus jusqu'a la fin du run, y compris pendant la
    # navigation du menu et sa validation. Sert aux conditions que le jeu lit sur l'etat *tenu* a un
    # instant precoce -- typiquement SELECT a l'entree de l'etat de jeu 1, que la sequence -Then ne
    # peut pas atteindre puisqu'elle ne demarre qu'apres la validation.
    # Jetons : SELECT START LCTRL LALT ENTER ESC UP DOWN LEFT RIGHT PGUP PGDN
    [string]$Hold = ''
)

$ErrorActionPreference = 'Stop'

$repo = Split-Path (Split-Path $PSCommandPath -Parent) -Parent
$exe  = Join-Path $repo 'OpenForesia\bin\Debug\net9.0\OpenForesia.exe'
$runs = Join-Path $repo 'scratchpad'          # ignore par git
if (-not (Test-Path $runs)) { New-Item -ItemType Directory $runs | Out-Null }

# Un journal par execution. Un nom fixe s'est fait reecrire sans etre tronque, et le verdict a
# alors melange deux essais -- une mesure d'un run precedent lue comme celle du run courant. Le
# risque ne vaut pas l'economie d'un fichier.
$stamp = Get-Date -Format 'MMdd-HHmmss'
$err   = Join-Path $runs "playtest-$stamp.log"
$out   = Join-Path $runs "playtest-$stamp-stdout.log"
$png   = Join-Path $runs "playtest-$stamp.png"

# On garde les vingt derniers, le reste part.
Get-ChildItem $runs -Filter 'playtest-*' -ErrorAction SilentlyContinue |
    Sort-Object LastWriteTime -Descending | Select-Object -Skip 20 |
    ForEach-Object { Remove-Item $_.FullName -ErrorAction SilentlyContinue }

# --------------------------------------------------------------------------------------------
# Construction optionnelle. Sur certaines machines le dotnet du PATH n'a que le runtime (le SDK
# est alors ailleurs, chez l'utilisateur) ; sur d'autres le PATH porte deja un SDK x86 utilisable
# tel quel. On detecte le cas plutot que de suivre en dur le chemin d'une machine passee.
# --------------------------------------------------------------------------------------------
if ($Build) {
    Write-Host '== construction =='
    $pathHasSdk = $false
    try { $pathHasSdk = [bool](& dotnet --list-sdks 2>$null) } catch {}

    if ($pathHasSdk) {
        & dotnet build (Join-Path $repo 'OpenForesia.sln') | Select-String -Pattern 'Erreur\(s\)|error ' | ForEach-Object { Write-Host "   $_" }
        $failed = ($LASTEXITCODE -ne 0)
    } else {
        # DOTNET_ROOT ne vaut QUE pour la construction, et doit etre retire ensuite : le SDK connu
        # ici est x64, le jeu est x86, et un processus x86 qui herite de ce DOTNET_ROOT meurt sur
        # "Failed to load hostfxr.dll ... 0x800700C1" avant meme d'ouvrir sa fenetre.
        # Plusieurs replis connus, essayes dans l'ordre. Le premier est le SDK x86 installe sur la
        # machine courante : etant x86 comme le jeu, il ne pose meme pas le probleme hostfxr
        # ci-dessus. Ajouter un chemin ici plutot que d'en coder un ailleurs.
        $fallbackCandidates = @(
            'C:\Program Files (x86)\dotnet',
            'C:\Program Files\dotnet',
            "$env:USERPROFILE\.dotnet",
            'C:\Users\Sagara\.dotnet'
        )
        $fallbackSdk = $fallbackCandidates |
            Where-Object { Test-Path (Join-Path $_ 'sdk') } |
            Select-Object -First 1
        if (-not $fallbackSdk) {
            Write-Host '!! aucun SDK sur le PATH et aucun des replis connus ne porte de dossier sdk :'
            $fallbackCandidates | ForEach-Object { Write-Host "     $_" }
            Write-Host '!! ajouter le chemin du SDK .NET local a $fallbackCandidates dans playtest.ps1'
            return
        }
        Write-Host "   SDK de repli : $fallbackSdk"
        $savedRoot = $env:DOTNET_ROOT
        $savedPath = $env:PATH
        try {
            $env:DOTNET_ROOT = $fallbackSdk
            $env:PATH = "$fallbackSdk;$env:PATH"
            & dotnet build (Join-Path $repo 'OpenForesia.sln') | Select-String -Pattern 'Erreur\(s\)|error ' | ForEach-Object { Write-Host "   $_" }
            $failed = ($LASTEXITCODE -ne 0)
        } finally {
            if ($null -eq $savedRoot) { Remove-Item Env:DOTNET_ROOT -ErrorAction SilentlyContinue }
            else { $env:DOTNET_ROOT = $savedRoot }
            $env:PATH = $savedPath
        }
    }
    if ($failed) { Write-Host '!! construction en echec, arret'; return }
}

if (-not (Test-Path $exe)) { Write-Host "!! introuvable : $exe  (lancer avec -Build)"; return }

# --------------------------------------------------------------------------------------------
# Injection clavier. SendKeys n'existe pas pour DirectInput : il faut SendInput en scancodes.
# --------------------------------------------------------------------------------------------
if (-not ('PlayTestInput' -as [type])) {
Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class PlayTestInput {
    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT { public ushort wVk; public ushort wScan; public uint dwFlags; public uint time; public IntPtr dwExtraInfo; }
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT { public uint type; public KEYBDINPUT ki; public int pad1; public int pad2; }
    [DllImport("user32.dll")] static extern uint SendInput(uint n, INPUT[] p, int cb);
    [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr h);
    [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr h, int n);
    const uint EXT = 0x0001, UP = 0x0002, UNICODE = 0x0004, SCAN = 0x0008;

    static void Send(ushort scan, uint flags) {
        INPUT[] i = new INPUT[1];
        i[0].type = 1; i[0].ki.wScan = scan; i[0].ki.dwFlags = flags;
        SendInput(1, i, Marshal.SizeOf(typeof(INPUT)));
    }

    // Le relachement d'une touche etendue se perd parfois et DirectInput la garde enfoncee :
    // on le renvoie avec puis sans le prefixe E0.
    public static void Tap(ushort scan, bool ext, int hold) {
        uint f = SCAN | (ext ? EXT : 0);
        Send(scan, f);
        System.Threading.Thread.Sleep(hold);
        Send(scan, f | UP);
        System.Threading.Thread.Sleep(40);
        Send(scan, SCAN | UP);
    }

    // Frappe de TEXTE, par KEYEVENTF_UNICODE (0x0004) et non par scancode. Deux raisons :
    //  - l'ecran de saisie du nom ne lit pas DirectInput mais la file WM_CHAR de winsys3d, donc il
    //    faut un vrai caractere, pas une touche physique ;
    //  - un scancode depend de la disposition clavier (sur AZERTY, le scancode de 'a' QWERTY donne
    //    'q'), alors qu'UNICODE injecte le caractere voulu quelle que soit la disposition.
    // Le systeme synthetise WM_KEYDOWN/VK_PACKET, et le TranslateMessage de D3D11Window en fait le
    // WM_CHAR que le jeu consomme.
    public static void TypeText(string s, int perChar) {
        foreach (char c in s) {
            INPUT[] i = new INPUT[2];
            i[0].type = 1; i[0].ki.wScan = (ushort)c; i[0].ki.dwFlags = UNICODE;
            i[1].type = 1; i[1].ki.wScan = (ushort)c; i[1].ki.dwFlags = UNICODE | UP;
            SendInput(2, i, Marshal.SizeOf(typeof(INPUT)));
            System.Threading.Thread.Sleep(perChar);
        }
    }

    // Maintien d'une touche a travers plusieurs images. Necessaire pour les conditions que le jeu
    // lit sur l'etat *tenu* de la manette (mot [0x604a20], actif haut) et non sur un front --
    // par exemple SELECT maintenu a l'entree de l'etat de jeu 1.
    public static void Hold(ushort scan, bool ext) { Send(scan, SCAN | (ext ? EXT : 0)); }
    public static void Free(ushort scan, bool ext) {
        Send(scan, SCAN | (ext ? EXT : 0) | UP);
        System.Threading.Thread.Sleep(40);
        Send(scan, SCAN | UP);
    }

    public static void Focus(IntPtr h) { ShowWindow(h, 9); SetForegroundWindow(h); }
}
"@
}

if ($Shot -and -not ('PlayTestShot' -as [type])) {
Add-Type -ReferencedAssemblies System.Drawing -TypeDefinition @"
using System; using System.Runtime.InteropServices; using System.Drawing;
public static class PlayTestShot {
  [DllImport("user32.dll")] static extern bool PrintWindow(IntPtr h, IntPtr hdc, uint flags);
  [DllImport("user32.dll")] static extern bool GetWindowRect(IntPtr h, out RECT r);
  [StructLayout(LayoutKind.Sequential)] public struct RECT { public int L,T,R,B; }
  public static void Grab(IntPtr h, string path) {
    RECT r; GetWindowRect(h, out r);
    using (var bmp = new Bitmap(r.R - r.L, r.B - r.T))
    using (var g = Graphics.FromImage(bmp)) {
      IntPtr hdc = g.GetHdc();
      PrintWindow(h, hdc, 2);       // 2 = PW_RENDERFULLCONTENT
      g.ReleaseHdc(hdc);
      bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
    }
  }
}
"@
}

# Lecture d'un fichier que le jeu tient encore ouvert en ecriture.
function Read-Live([string]$path) {
    if (-not (Test-Path $path)) { return '' }
    try {
        $fs = [System.IO.File]::Open($path, 'Open', 'Read', 'ReadWrite')
        $sr = New-Object System.IO.StreamReader($fs)
        $t = $sr.ReadToEnd(); $sr.Close(); $fs.Close(); return $t
    } catch { return '' }
}

# Position du curseur du menu, lue dans la trace. -1 = jamais deplace, donc entree 0.
function Get-Cursor([string]$path) {
    $m = [regex]::Matches((Read-Live $path), '\+0x8C4:[0-9A-F]{2}->([0-9A-F]{2})')
    if ($m.Count -eq 0) { return -1 }
    return [Convert]::ToInt32($m[$m.Count - 1].Groups[1].Value, 16)
}

# Nombre de fois que le jeu a vu le bouton de validation. C'est lui qui nous dit si une frappe
# est passee : rien d'autre ne le fait, et le premier appui apres la prise de focus est avale.
function Get-ValidateCount([string]$path) {
    # Le bit qui compte est 0x8000 (carre) ; les autres bits du mot peuvent etre poses en meme temps
    # -- avec -Hold SELECT la trace dit pad=0x8001, et une comparaison exacte ratait la validation.
    $n = 0
    foreach ($m in [regex]::Matches((Read-Live $path), 'pad=0x([0-9a-f]{4})')) {
        if (([Convert]::ToInt32($m.Groups[1].Value, 16) -band 0x8000) -ne 0) { $n++ }
    }
    return $n
}

# --------------------------------------------------------------------------------------------
# Lancement
# --------------------------------------------------------------------------------------------
$env:OPENFORESIA_TRACE_MENU = '1'          # indispensable : c'est la boucle de retour
foreach ($t in $Trace) { Set-Item -Path "Env:OPENFORESIA_TRACE_$t" -Value '1' }

Remove-Item $err, $out -ErrorAction SilentlyContinue

$cwd = if ($WorkingDir) { $WorkingDir } else { Split-Path $exe }
$p = Start-Process -FilePath $exe -WorkingDirectory $cwd -PassThru `
        -RedirectStandardError $err -RedirectStandardOutput $out
Write-Host "== PID $($p.Id) ; attente $MenuDelay s pour le menu"
Start-Sleep -Seconds $MenuDelay

# Le jeu se **relance en processus fils** quand il perd la course pour la memoire basse (environ
# une fois sur quatre, voir EnsureDisplayListArena). Le parent attend alors son fils et n'a pas de
# fenetre : piloter $p directement echoue silencieusement. On prend donc le processus OpenForesia
# qui a reellement une fenetre.
# Le titre compte : un processus relance porte aussi une fenetre de console, dont le titre est le
# chemin de l'exe. La prendre pour la fenetre du jeu envoie toutes les frappes dans le vide --
# mesure, la capture montrait la console et la navigation ne bougeait pas.
# Attend la fenetre plutot que de la supposer prete : avec -Build, ou apres une relance, elle peut
# arriver plusieurs secondes apres MenuDelay. Sans cette attente le focus echoue en silence et la
# navigation part dans le vide -- mesure : 11 appuis au lieu de 2.
function Get-GameWindow($parent, [int]$WaitSeconds = 20) {
    for ($t = 0; $t -lt $WaitSeconds * 2; $t++) {
        $parent.Refresh()
        if ($parent.MainWindowHandle -ne [IntPtr]::Zero -and $parent.MainWindowTitle -eq 'OpenForesia') {
            return $parent
        }
        $kids = @(Get-Process -Name 'OpenForesia' -ErrorAction SilentlyContinue |
                  Where-Object { $_.MainWindowHandle -ne [IntPtr]::Zero -and $_.MainWindowTitle -eq 'OpenForesia' })
        if ($kids.Count -gt 0) {
            if ($kids[0].Id -ne $parent.Id) {
                Write-Host "   (relance detectee : fenetre portee par le PID $($kids[0].Id))"
            }
            if ($t -gt 0) { Write-Host "   (fenetre apparue apres $([math]::Round($t / 2.0, 1)) s d'attente)" }
            return $kids[0]
        }
        if ($parent.HasExited) { break }
        Start-Sleep -Milliseconds 500
    }
    Write-Host "!! aucune fenetre intitulee 'OpenForesia' apres $WaitSeconds s"
    return $parent
}

$hwnd = [IntPtr]::Zero
if ($p.HasExited) {
    Write-Host "!! sorti avant toute frappe (code $($p.ExitCode))"
} else {
    $game = Get-GameWindow $p
    $hwnd = $game.MainWindowHandle
    if ($hwnd -ne [IntPtr]::Zero) { [PlayTestInput]::Focus($hwnd) }
    Start-Sleep -Milliseconds 1200

    $holdScan = $null
    if ($Hold -ne '') {
        $holdMap = @{ 'START'=@(0x3C,$false); 'SELECT'=@(0x3B,$false); 'LCTRL'=@(0x1D,$false);
                      'LALT'=@(0x38,$false);  'ENTER'=@(0x1C,$false); 'ESC'=@(0x01,$false);
                      'UP'=@(0x48,$true); 'DOWN'=@(0x50,$true); 'LEFT'=@(0x4B,$true); 'RIGHT'=@(0x4D,$true) }
        $hk = $Hold.ToUpper()
        if ($holdMap.ContainsKey($hk)) {
            $holdScan = @([uint16]$holdMap[$hk][0], [bool]$holdMap[$hk][1])
            # Le premier appui apres SetForegroundWindow est toujours avale (voir test-protocol.md).
            # Ici c'en est un : on le depense volontairement, puis on enfonce pour de bon.
            [PlayTestInput]::Hold($holdScan[0], $holdScan[1])
            Start-Sleep -Milliseconds 300
            [PlayTestInput]::Free($holdScan[0], $holdScan[1])
            Start-Sleep -Milliseconds 300
            [PlayTestInput]::Hold($holdScan[0], $holdScan[1])
            Write-Host "== $hk maintenu pour toute la duree du run"
        } else {
            Write-Host "!! -Hold : jeton inconnu ($Hold)"
        }
    }

    if ($Entry -lt 0) {
        Write-Host "== pas de validation demandee, observation du menu pendant $Seconds s"
        Start-Sleep -Seconds $Seconds
    } else {
        # Appui d'echauffement : le premier appui apres la prise de focus est toujours avale.
        # La boucle ci-dessous relit la position reelle, donc un deplacement parasite se corrige
        # tout seul -- alors qu'une validation avalee, elle, ne se rattrape pas.
        [PlayTestInput]::Tap(0x50, $true, $TapMs)
        Start-Sleep -Milliseconds 500

        # Boucle fermee : taper, relire la position, recommencer. La cadence du jeu (~6 images/s,
        # un sondage par image) rend tout chronometrage illusoire.
        $cursor = Get-Cursor $err
        if ($cursor -lt 0) { $cursor = 0 }
        Write-Host "== curseur sur $cursor, cible $Entry"

        # Bidirectionnel : le curseur du menu **butte** sur la premiere et la derniere entree, il
        # ne boucle pas. Un depassement serait donc definitif si on ne tapait que vers le bas.
        for ($i = 0; $i -lt $MaxTaps; $i++) {
            if ($cursor -eq $Entry) { break }
            if ($cursor -lt $Entry) { [PlayTestInput]::Tap(0x50, $true, $TapMs) }   # Bas
            else                    { [PlayTestInput]::Tap(0x48, $true, $TapMs) }   # Haut
            Start-Sleep -Milliseconds 800      # ~5 images a la cadence du jeu : la trace a le temps
            $c = Get-Cursor $err
            if ($c -lt 0) { $c = 0 }
            if ($c -ne $cursor) { Write-Host "   appui $($i+1) : curseur -> $c" }
            $cursor = $c
            if ($p.HasExited) { break }
        }

        if ($cursor -ne $Entry) {
            Write-Host "!! curseur reste sur $cursor apres $MaxTaps appuis, validation non envoyee"
        } elseif ($p.HasExited) {
            Write-Host '!! le jeu est sorti pendant la navigation'
        } else {
            # Validation confirmee : on ne suppose pas que la frappe est passee, on attend que le
            # jeu dise l'avoir vue (pad=0x8000 dans sa propre trace).
            $seen = Get-ValidateCount $err
            $ok = $false
            for ($k = 0; $k -lt 4; $k++) {
                [PlayTestInput]::Tap(0x1C, $false, 400)    # Entree
                Start-Sleep -Milliseconds 1500
                if ($p.HasExited) { $ok = $true; break }
                if ((Get-ValidateCount $err) -gt $seen) { $ok = $true; break }
                Write-Host "   validation non vue, nouvel essai ($($k+2)/4)"
            }
            if ($ok) { Write-Host "== validation vue, observation $Seconds s" }
            else { Write-Host '!! validation jamais vue par le jeu' }

            # Sequence libre une fois l'ecran atteint. Laisser l'ecran s'installer d'abord : le
            # chargement des ressources prend plusieurs secondes et rien ne repond pendant.
            if ($Then.Count -gt 0) {
                Start-Sleep -Seconds 6
                foreach ($k in $Then) {
                    if ($p.HasExited) { Write-Host "   (sorti avant '$k')"; break }
                    if ($k -match '^WAIT(\d+)$') {
                        Start-Sleep -Seconds ([int]$Matches[1])
                        continue
                    }
                    # TYPE:xxx -- frappe du texte litteral dans la file WM_CHAR. Sert a l'ecran de
                    # saisie du nom, seul endroit du jeu qui lit de vrais caracteres.
                    # HOLD:xxx / FREE:xxx -- enfonce ou relache sans relacher tout de suite, pour les
                    # conditions que le jeu lit sur l'etat tenu et non sur un front.
                    if ($k -match '^(HOLD|FREE):(.+)$') {
                        $act = $Matches[1]; $key = $Matches[2].ToUpper()
                        $map = @{ 'START'=@(0x3C,$false); 'SELECT'=@(0x3B,$false); 'LCTRL'=@(0x1D,$false);
                                  'LALT'=@(0x38,$false);  'ENTER'=@(0x1C,$false); 'ESC'=@(0x01,$false);
                                  'UP'=@(0x48,$true); 'DOWN'=@(0x50,$true); 'LEFT'=@(0x4B,$true); 'RIGHT'=@(0x4D,$true);
                                  'PGUP'=@(0x49,$true); 'PGDN'=@(0x51,$true) }
                        if (-not $map.ContainsKey($key)) { Write-Host "!! jeton inconnu : $k"; continue }
                        $sc = [uint16]$map[$key][0]; $ex = [bool]$map[$key][1]
                        if ($act -eq 'HOLD') { [PlayTestInput]::Hold($sc, $ex) } else { [PlayTestInput]::Free($sc, $ex) }
                        Write-Host "   -> $k"
                        continue
                    }
                    # KBENTER -- le caractere 0x0d dans la file WM_CHAR. L'ecran de saisie du nom le
                    # traite comme la case "fin" : il confirme et passe au sous-etat suivant. A ne
                    # pas confondre avec ENTER, qui est le bouton carre de la manette.
                    if ($k -eq 'KBENTER') {
                        [PlayTestInput]::TypeText([string][char]13, 250)
                        Write-Host '   -> KBENTER'
                        Start-Sleep -Milliseconds 900
                        continue
                    }
                    if ($k -match '^TYPE:(.+)$') {
                        [PlayTestInput]::TypeText($Matches[1], 250)
                        Write-Host "   -> TYPE '$($Matches[1])'"
                        Start-Sleep -Milliseconds 900
                        continue
                    }
                    switch ($k.ToUpper()) {
                        'ENTER' { [PlayTestInput]::Tap(0x1C, $false, 400) }
                        'ESC'   { [PlayTestInput]::Tap(0x01, $false, 400) }
                        'UP'    { [PlayTestInput]::Tap(0x48, $true,  $TapMs) }
                        'DOWN'  { [PlayTestInput]::Tap(0x50, $true,  $TapMs) }
                        'LEFT'  { [PlayTestInput]::Tap(0x4B, $true,  $TapMs) }
                        'RIGHT' { [PlayTestInput]::Tap(0x4D, $true,  $TapMs) }
                        # DIK_LCONTROL (0x1D) -- action 7 ("carre", masque pad 0x8000) dans
                        # OpenForesia.cfg K0007=29. C'est le bouton que DrawUiPanel teste pour son
                        # "curseur de confirmation" (voir REVERSE_STATUS.md, code de controle 0x0f).
                        'LCTRL' { [PlayTestInput]::Tap(0x1D, $false, 400) }
                        # Les trois boutons restants, lus dans s_PadButtonMasks_00564f18 croisee
                        # avec la section [Controls] d'OpenForesia.cfg : action 11 = START (masque
                        # 0x0008) -> K000b=60 = F2 ; action 8 = SELECT (0x0001) -> K0008=59 = F1 ;
                        # action 6 = CROIX (0x4000) -> K0006=56 = Alt gauche. Ajoutes pour piloter
                        # RunDebugPartySelect, qui valide sur START et retire un membre sur CROIX.
                        'START'  { [PlayTestInput]::Tap(0x3C, $false, 400) }
                        'SELECT' { [PlayTestInput]::Tap(0x3B, $false, 400) }
                        'LALT'   { [PlayTestInput]::Tap(0x38, $false, 400) }
                        # Les deux gachettes hautes, ajoutees pour changer de page dans le menu de
                        # jeu (FUN_0052a850 lit 0x0400 pour reculer et 0x0800 pour avancer). Leur
                        # liaison secondaire par defaut est PageDown (DIK 0xd1) pour L1 et PageUp
                        # (DIK 0xc9) pour R1 -- donc PGUP avance d'une page et PGDN recule, ce qui
                        # est l'inverse de ce que les noms suggerent. Envoyees comme les fleches :
                        # le scancode de base avec le drapeau etendu.
                        'PGUP'   { [PlayTestInput]::Tap(0x49, $true,  400) }
                        'PGDN'   { [PlayTestInput]::Tap(0x51, $true,  400) }
                        default { Write-Host "!! jeton inconnu : $k"; continue }
                    }
                    Write-Host "   -> $k"
                    Start-Sleep -Milliseconds 900
                }
            }

            Start-Sleep -Seconds $Seconds
        }
    }
}

if ($null -ne $holdScan) { [PlayTestInput]::Free($holdScan[0], $holdScan[1]) }

if ($Shot -and -not $p.HasExited -and $hwnd -ne [IntPtr]::Zero) {
    try { [PlayTestShot]::Grab($hwnd, $png); Write-Host "== capture -> $png" }
    catch { Write-Host "!! capture en echec : $_" }
}

$p.Refresh()
if (-not $p.HasExited) {
    if ($KeepOpen) { Write-Host "== laisse ouvert (PID $($p.Id))" }
    else {
        # Tout l'arbre : en cas de relance le parent attend son fils, et le tuer seul laisserait
        # le jeu ouvert derriere.
        Get-Process -Name 'OpenForesia' -ErrorAction SilentlyContinue |
            ForEach-Object { try { $_.Kill() } catch {} }
        Start-Sleep -Seconds 2
    }
}

# --------------------------------------------------------------------------------------------
# Verdict
# --------------------------------------------------------------------------------------------
$log = Read-Live $err
Write-Host "`n===== VERDICT ====="

$fault = [regex]::Match($log, '(?m)^(Unhandled exception.*|Fatal error.*)$')
if ($fault.Success) {
    Write-Host 'PLANTAGE :'
    $lines = $log -split "`r?`n"
    $start = 0
    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -match '^(Unhandled exception|Fatal error)') { $start = $i; break }
    }
    $lines[$start..([Math]::Min($start + 16, $lines.Count - 1))] | ForEach-Object { Write-Host "   $_" }
} else {
    Write-Host 'aucun plantage'
}

# Progression du rendu : deux derniers bilans du shim.
$bilans = [regex]::Matches($log, 'BeginScene=(\d+).*?DrawPrimitive=(\d+)')
if ($bilans.Count -ge 2) {
    $a = $bilans[$bilans.Count - 2]; $b = $bilans[$bilans.Count - 1]
    $df = [int]$b.Groups[1].Value - [int]$a.Groups[1].Value
    $dp = [int]$b.Groups[2].Value - [int]$a.Groups[2].Value
    $par = 0
    if ($df -gt 0) { $par = [Math]::Round($dp / $df, 1) }
    Write-Host "images $($b.Groups[1].Value) ; DrawPrimitive $($b.Groups[2].Value) (+$dp sur $df images = $par par image)"
    if ($dp -eq 0) { Write-Host '   ATTENTION : rien de dessine sur le dernier intervalle' }
} else {
    Write-Host 'pas assez de bilans [SHIM] pour mesurer la progression'
}

Write-Host "journal complet : $err"
