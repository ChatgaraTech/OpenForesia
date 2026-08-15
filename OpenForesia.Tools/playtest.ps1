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
    # ENTER ESC UP DOWN LEFT RIGHT, et WAIT<n> pour patienter n secondes.
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

    [int]$MaxTaps = 30
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
# Construction optionnelle. Le dotnet du PATH n'a que le runtime : le SDK est chez l'utilisateur.
# --------------------------------------------------------------------------------------------
if ($Build) {
    # DOTNET_ROOT ne vaut QUE pour la construction, et doit etre retire ensuite : le SDK est x64,
    # le jeu est x86, et un processus x86 qui herite de ce DOTNET_ROOT meurt sur
    # "Failed to load hostfxr.dll ... 0x800700C1" avant meme d'ouvrir sa fenetre.
    $savedRoot = $env:DOTNET_ROOT
    $savedPath = $env:PATH
    try {
        $env:DOTNET_ROOT = 'C:\Users\Sagara\.dotnet'
        $env:PATH = "C:\Users\Sagara\.dotnet;$env:PATH"
        Write-Host '== construction =='
        & dotnet build (Join-Path $repo 'OpenForesia.sln') | Select-String -Pattern 'Erreur\(s\)|error ' | ForEach-Object { Write-Host "   $_" }
        $failed = ($LASTEXITCODE -ne 0)
    } finally {
        if ($null -eq $savedRoot) { Remove-Item Env:DOTNET_ROOT -ErrorAction SilentlyContinue }
        else { $env:DOTNET_ROOT = $savedRoot }
        $env:PATH = $savedPath
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
    const uint EXT = 0x0001, UP = 0x0002, SCAN = 0x0008;

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
    return ([regex]::Matches((Read-Live $path), 'pad=0x8000')).Count
}

# --------------------------------------------------------------------------------------------
# Lancement
# --------------------------------------------------------------------------------------------
$env:OPENFORESIA_TRACE_MENU = '1'          # indispensable : c'est la boucle de retour
foreach ($t in $Trace) { Set-Item -Path "Env:OPENFORESIA_TRACE_$t" -Value '1' }

Remove-Item $err, $out -ErrorAction SilentlyContinue

$p = Start-Process -FilePath $exe -WorkingDirectory (Split-Path $exe) -PassThru `
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
function Get-GameWindow($parent) {
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
        return $kids[0]
    }
    Write-Host "!! aucune fenetre intitulee 'OpenForesia' trouvee"
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
                    switch ($k.ToUpper()) {
                        'ENTER' { [PlayTestInput]::Tap(0x1C, $false, 400) }
                        'ESC'   { [PlayTestInput]::Tap(0x01, $false, 400) }
                        'UP'    { [PlayTestInput]::Tap(0x48, $true,  $TapMs) }
                        'DOWN'  { [PlayTestInput]::Tap(0x50, $true,  $TapMs) }
                        'LEFT'  { [PlayTestInput]::Tap(0x4B, $true,  $TapMs) }
                        'RIGHT' { [PlayTestInput]::Tap(0x4D, $true,  $TapMs) }
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
