# Lecture d'un vidage memoire (.dmp) sans outil de debogage installe.
#
# Windows depose un vidage complet dans %LOCALAPPDATA%\CrashDumps a chaque mort
# du processus. Ce script en lit le format MINIDUMP directement : date de
# demarrage du processus (donc la duree de vie de l'execution), code d'exception,
# module fautif, liste des threads, et un balayage de pile qui rend les adresses
# de retour tombant dans un module connu.
#
# Ce n'est PAS un debogueur : pas de deroulement de pile reel, pas de symboles,
# et le code managed (JIT) n'appartient a aucun module donc n'apparait pas. Ce
# qu'on voit, ce sont les DLL natives - winsys3d, ddraw, dsound, dinput, ntdll -
# ce qui suffit a savoir OU en etait le jeu.
#
#   .\dump-info.ps1 -Path "$env:LOCALAPPDATA\CrashDumps\OpenForesia.exe.4124.dmp"
#   .\dump-info.ps1 -Path <dmp> -Threads          # tous les threads du jeu
#   .\dump-info.ps1 -Path <dmp> -Stack 10172      # une pile en detail

param(
    [Parameter(Mandatory = $true)][string]$Path,
    [switch]$Threads,
    [int]$Stack = 0
)

$fs = [System.IO.File]::OpenRead($Path)
$br = New-Object System.IO.BinaryReader($fs)
function Seek([long]$p) { $script:fs.Seek($p, 'Begin') | Out-Null }

$sig = $br.ReadUInt32(); $null = $br.ReadUInt32()
if ($sig -ne 0x504D444D) { throw "signature MINIDUMP absente : 0x{0:X8}" -f $sig }
$nStreams = $br.ReadUInt32(); $dirRva = $br.ReadUInt32()
$dir = @{}
Seek $dirRva
for ($i = 0; $i -lt $nStreams; $i++) {
    $t = $br.ReadUInt32(); $null = $br.ReadUInt32(); $rva = $br.ReadUInt32()
    $dir[[int]$t] = $rva
}

# --- MODULE_LIST (4). MINIDUMP_MODULE fait 108 octets : 24 d'entete, puis
#     VS_FIXEDFILEINFO (52), deux descripteurs (16) et deux reserves (16).
Seek $dir[4]
$nMod = [int]$br.ReadUInt32()
$mB = New-Object 'uint64[]' $nMod; $mE = New-Object 'uint64[]' $nMod
$mR = New-Object 'uint32[]' $nMod; $mN = New-Object 'string[]' $nMod
for ($i = 0; $i -lt $nMod; $i++) {
    $b = $br.ReadUInt64(); $s = $br.ReadUInt32()
    $null = $br.ReadUInt32(); $null = $br.ReadUInt32()
    $mR[$i] = $br.ReadUInt32()
    $null = $br.ReadBytes(84)
    $mB[$i] = $b; $mE[$i] = $b + $s
}
for ($i = 0; $i -lt $nMod; $i++) {
    Seek $mR[$i]
    $len = [int]$br.ReadUInt32()
    if ($len -gt 0 -and $len -lt 2000) {
        $mN[$i] = [System.IO.Path]::GetFileName([System.Text.Encoding]::Unicode.GetString($br.ReadBytes($len)))
    } else { $mN[$i] = '?' }
}
function ModuleOf([uint64]$a) {
    for ($k = 0; $k -lt $script:nMod; $k++) {
        if ($a -ge $script:mB[$k] -and $a -lt $script:mE[$k]) {
            return ("{0}+0x{1:X}" -f $script:mN[$k], ($a - $script:mB[$k]))
        }
    }
    return $null
}
# Les modules qui disent ou en etait le jeu. Le balayage de pile ne teste que
# ceux-la : tout comparer coute une minute de PowerShell par thread.
$jeu = @()
for ($i = 0; $i -lt $nMod; $i++) {
    if ($mN[$i] -match 'winsys3d|wsyskern|playst|3dhelp|gldhelp|OpenForesia|^d3d|ddraw|dinput|dsound|dxgi') { $jeu += $i }
}

# --- MISC_INFO (15) : quand le processus a demarre, donc combien de temps il a vecu
if ($dir.ContainsKey(15)) {
    Seek $dir[15]
    $null = $br.ReadUInt32(); $null = $br.ReadUInt32()
    $procId = $br.ReadUInt32(); $ctime = $br.ReadUInt32()
    $utime = $br.ReadUInt32(); $ktime = $br.ReadUInt32()
    $depart = [datetimeoffset]::FromUnixTimeSeconds($ctime).LocalDateTime
    $mort = (Get-Item $Path).LastWriteTime
    "== PROCESSUS =="
    "  pid            : $procId"
    "  demarre a      : $depart"
    "  vidage ecrit a : $mort"
    "  a vecu         : {0:N0} s   (utilisateur {1} s, noyau {2} s)" -f ($mort - $depart).TotalSeconds, $utime, $ktime
}

# --- EXCEPTION (6)
if ($dir.ContainsKey(6)) {
    Seek $dir[6]
    $tid = $br.ReadUInt32(); $null = $br.ReadUInt32()
    $code = $br.ReadUInt32(); $null = $br.ReadUInt32()
    $null = $br.ReadUInt64(); $exAddr = $br.ReadUInt64()
    "`n== EXCEPTION =="
    "  code           : 0x{0:X8}" -f $code
    "  thread fautif  : $tid"
    "  adresse        : 0x{0:X}  ({1})" -f $exAddr, (ModuleOf $exAddr)
    # 0xC0000374 deborde int32 : PowerShell lit le litteral hexa comme un entier signe.
    if ($code -eq [uint32]3221226356) {
        "  NB : 0xC0000374 est detecte par ntdll au prochain alloc/free. Le thread"
        "       fautif est celui qui allouait, PAS celui qui a corrompu le tas."
    }
}

"`n== MODULES DU JEU =="
foreach ($i in $jeu) { "  0x{0:X8}..0x{1:X8}  {2}" -f $mB[$i], $mE[$i], $mN[$i] }

# --- THREAD_LIST (3)
Seek $dir[3]
$nT = [int]$br.ReadUInt32()
$th = @()
for ($i = 0; $i -lt $nT; $i++) {
    $tid2 = $br.ReadUInt32(); $null = $br.ReadBytes(12); $null = $br.ReadUInt64()
    $st = $br.ReadUInt64(); $ss = $br.ReadUInt32(); $sr = $br.ReadUInt32()
    $null = $br.ReadUInt32(); $cr = $br.ReadUInt32()
    $th += New-Object psobject -Property @{ Tid = $tid2; Start = $st; Size = $ss; Rva = $sr; Ctx = $cr }
}
"`n== $nT THREADS =="

# CONTEXT x86 : Ebp @0xB4, Eip @0xB8, Esp @0xC4.
function Contexte($t) {
    Seek ($t.Ctx + 0xB8); $eip = $br.ReadUInt32()
    Seek ($t.Ctx + 0xC4); $esp = $br.ReadUInt32()
    return @{ Eip = $eip; Esp = $esp }
}

if ($Threads) {
    foreach ($t in $th) {
        $c = Contexte $t
        $off = [uint64]$c.Esp - $t.Start
        if ($off -ge $t.Size) { continue }
        $len = [Math]::Min(0x3000, $t.Size - $off)
        Seek ($t.Rva + $off)
        $bytes = $br.ReadBytes([int]$len)
        $hits = @()
        for ($i = 0; $i + 4 -le $bytes.Length; $i += 4) {
            $v = [BitConverter]::ToUInt32($bytes, $i)
            if ($v -lt 0x400000) { continue }
            foreach ($k in $jeu) {
                if ($v -ge $mB[$k] -and $v -lt $mE[$k]) { $hits += ("{0}+0x{1:X}" -f $mN[$k], ($v - $mB[$k])); break }
            }
            if ($hits.Count -ge 14) { break }
        }
        if ($hits.Count -gt 0) {
            "`n  thread {0}  EIP 0x{1:X8}  ESP 0x{2:X8}" -f $t.Tid, $c.Eip, $c.Esp
            foreach ($h in $hits) { "      $h" }
        }
    }
}

if ($Stack -ne 0) {
    $t = $th | Where-Object { $_.Tid -eq $Stack }
    if (-not $t) { "thread $Stack absent du vidage"; $br.Close(); $fs.Close(); return }
    $c = Contexte $t
    "`n== PILE DU THREAD $Stack =="
    "  EIP 0x{0:X8} ({1})   ESP 0x{2:X8}" -f $c.Eip, (ModuleOf $c.Eip), $c.Esp
    $off = [uint64]$c.Esp - $t.Start
    $len = [Math]::Min(0x4000, $t.Size - $off)
    Seek ($t.Rva + $off)
    $bytes = $br.ReadBytes([int]$len)
    for ($i = 0; $i + 4 -le $bytes.Length; $i += 4) {
        $v = [BitConverter]::ToUInt32($bytes, $i)
        if ($v -lt 0x400000) { continue }
        $m = ModuleOf $v
        if ($m) { "    esp+0x{0:X4}  0x{1:X8}  {2}" -f $i, $v, $m }
    }
}

$br.Close(); $fs.Close()
