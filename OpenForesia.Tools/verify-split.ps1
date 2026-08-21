<#
.SYNOPSIS
    Verifie qu'un decoupage de Blaze.cs n'a modifie aucune ligne - comparaison octet a octet.

.DESCRIPTION
    Le changement `split-blaze-into-subsystems` deplace 23 647 lignes de `Blaze.cs` vers 28
    fichiers. Le seul controle qui prouve qu'aucune ligne n'a ete perdue, dupliquee ou modifiee au
    passage est une comparaison octet a octet contre une reference figee avant le premier
    deplacement.

    Le fichier contient une region a **encodage double** (871 lignes, commentaires et journaux
    seulement). Tout ce script lit et ecrit donc en **ISO-8859-1**, qui fait correspondre un octet a
    un caractere et un seul : le decoupage en lignes et la comparaison restent exacts a l'octet,
    quel que soit l'encodage reel du contenu. `Get-Content` / `Set-Content` sont proscrits - ils ont
    deja aggrave le probleme une fois.

.PARAMETER Snapshot
    Fige la reference. A lancer UNE FOIS, avant le premier deplacement.

.PARAMETER Manifest
    Le fichier de plages `fichier<TAB>debut<TAB>fin`, genere depuis l'inventaire (tache 1.1).

.PARAMETER Baseline
    La reference figee. Par defaut `OpenForesia.Tools/blaze-baseline.txt`.

.PARAMETER Root
    La racine des fichiers cibles. Par defaut `OpenForesia/src/Blaze`.

.EXAMPLE
    OpenForesia.Tools\verify-split.ps1 -Snapshot
    OpenForesia.Tools\verify-split.ps1
#>
[CmdletBinding()]
param(
    [switch] $Snapshot,
    [string] $Manifest = "OpenForesia.Tools\split-manifest.tsv",
    [string] $Baseline = "OpenForesia.Tools\blaze-baseline.txt",
    [string] $Root     = "OpenForesia\src\Blaze",
    [string] $Source   = "OpenForesia\src\Blaze\Blaze.cs"
)

$ErrorActionPreference = 'Stop'
# ISO-8859-1 : un octet <-> un caractere. Le seul encodage sur lequel une comparaison de lignes
# reste une comparaison d'octets quand le contenu est a encodage douteux.
$Latin1 = [System.Text.Encoding]::GetEncoding(28591)

function Read-LinesExact([string] $Path) {
    $bytes = [System.IO.File]::ReadAllBytes((Resolve-Path -LiteralPath $Path).Path)
    $text  = $Latin1.GetString($bytes)
    # -1 : conserve la derniere ligne vide si le fichier finit par un saut de ligne.
    return $text -split "`r`n|`n", -1
}

if ($Snapshot) {
    $bytes = [System.IO.File]::ReadAllBytes((Resolve-Path -LiteralPath $Source).Path)
    [System.IO.File]::WriteAllBytes((Join-Path (Get-Location) $Baseline), $bytes)
    $lines = Read-LinesExact $Baseline
    Write-Host "Reference figee : $Baseline"
    Write-Host ("  {0} octets, {1} lignes" -f $bytes.Length, ($lines.Count - 1))
    exit 0
}

if (-not (Test-Path -LiteralPath $Baseline)) {
    Write-Host "Aucune reference. Lancer d'abord : verify-split.ps1 -Snapshot" -ForegroundColor Yellow
    exit 2
}

$orig = Read-LinesExact $Baseline
Write-Host ("Reference : {0} lignes" -f ($orig.Count - 1))

# --- le manifeste ---
$ranges = @()
foreach ($row in (Read-LinesExact $Manifest)) {
    if ([string]::IsNullOrWhiteSpace($row)) { continue }
    $p = $row -split "`t"
    if ($p.Count -lt 3) { continue }
    $ranges += [pscustomobject]@{ File = $p[0]; Start = [int]$p[1]; End = [int]$p[2] }
}
Write-Host ("Manifeste : {0} plages, {1} fichiers" -f $ranges.Count, ($ranges.File | Sort-Object -Unique).Count)

# --- controle 1 : chaque ligne source est revendiquee exactement une fois ---
$claim = @{}
$dup = 0
foreach ($r in $ranges) {
    for ($i = $r.Start; $i -le $r.End; $i++) {
        if ($claim.ContainsKey($i)) { $dup++; if ($dup -le 5) { Write-Host ("  ligne {0} revendiquee deux fois : {1} et {2}" -f $i, $claim[$i], $r.File) -ForegroundColor Red } }
        $claim[$i] = $r.File
    }
}
$first = [int]($ranges | Measure-Object -Property Start -Minimum).Minimum
$last  = [int]($ranges | Measure-Object -Property End   -Maximum).Maximum
$holes = @()
for ($i = $first; $i -le $last; $i++) { if (-not $claim.ContainsKey($i)) { $holes += $i } }

Write-Host ""
Write-Host "[1] Couverture du manifeste"
Write-Host ("    plage revendiquee : {0}..{1}  ({2} lignes)" -f $first, $last, $claim.Count)
if ($dup -gt 0)          { Write-Host ("    ECHEC : {0} lignes revendiquees plusieurs fois" -f $dup) -ForegroundColor Red }
if ($holes.Count -gt 0)  { Write-Host ("    ECHEC : {0} lignes non revendiquees, la premiere en {1}" -f $holes.Count, $holes[0]) -ForegroundColor Red }
if ($dup -eq 0 -and $holes.Count -eq 0) { Write-Host "    OK : aucune ligne perdue ni dupliquee" -ForegroundColor Green }

# --- controle 2 : le corps de chaque fichier cible est exactement ses plages ---
Write-Host ""
Write-Host "[2] Contenu des fichiers cibles"
$checked = 0; $failed = 0; $absent = 0

foreach ($file in ($ranges.File | Sort-Object -Unique)) {
    $path = Join-Path $Root ($file -replace '/', '\')
    if (-not (Test-Path -LiteralPath $path)) { $absent++; continue }
    $checked++

    $actual = Read-LinesExact $path
    # Le corps : de l'accolade ouvrante de la classe a l'accolade fermante finale.
    $open = -1
    for ($i = 0; $i -lt $actual.Count; $i++) { if ($actual[$i] -eq '{') { $open = $i; break } }
    $close = -1
    for ($i = $actual.Count - 1; $i -ge 0; $i--) { if ($actual[$i] -eq '}') { $close = $i; break } }
    if ($open -lt 0 -or $close -le $open) {
        Write-Host ("    {0} : corps introuvable (accolades de classe absentes)" -f $file) -ForegroundColor Red
        $failed++; continue
    }
    $body = $actual[($open + 1)..($close - 1)]

    $expected = New-Object System.Collections.Generic.List[string]
    foreach ($r in ($ranges | Where-Object { $_.File -eq $file })) {
        for ($i = $r.Start; $i -le $r.End; $i++) { $expected.Add($orig[$i - 1]) }
    }

    # On tolere un saut de ligne en tete et en queue de corps, rien d'autre.
    $b = $body
    while ($b.Count -gt 0 -and [string]::IsNullOrWhiteSpace($b[0]))              { $b = $b[1..($b.Count - 1)] }
    while ($b.Count -gt 0 -and [string]::IsNullOrWhiteSpace($b[$b.Count - 1]))   { $b = $b[0..($b.Count - 2)] }
    $e = $expected.ToArray()
    # La toute derniere plage de la reference se termine par l'accolade fermante DE LA CLASSE.
    # Le corps extrait plus haut s'arrete avant elle (il va de la premiere '{' a la derniere '}'),
    # donc il faut la retirer ici aussi, sans quoi le fichier qui porte la fin du fichier d'origine
    # parait toujours une ligne trop court.
    while ($e.Count -gt 0 -and [string]::IsNullOrWhiteSpace($e[$e.Count - 1]))   { $e = $e[0..($e.Count - 2)] }
    if ($e.Count -gt 0 -and $e[$e.Count - 1] -eq '}')                            { $e = $e[0..($e.Count - 2)] }
    while ($e.Count -gt 0 -and [string]::IsNullOrWhiteSpace($e[0]))              { $e = $e[1..($e.Count - 1)] }
    while ($e.Count -gt 0 -and [string]::IsNullOrWhiteSpace($e[$e.Count - 1]))   { $e = $e[0..($e.Count - 2)] }

    if ($b.Count -ne $e.Count) {
        Write-Host ("    {0} : ECHEC - {1} lignes de corps, {2} attendues" -f $file, $b.Count, $e.Count) -ForegroundColor Red
        $failed++; continue
    }
    $diff = -1
    for ($i = 0; $i -lt $e.Count; $i++) { if ($b[$i] -cne $e[$i]) { $diff = $i; break } }
    if ($diff -ge 0) {
        Write-Host ("    {0} : ECHEC a la ligne de corps {1}" -f $file, ($diff + 1)) -ForegroundColor Red
        Write-Host ("        attendu : {0}" -f $e[$diff])
        Write-Host ("        trouve  : {0}" -f $b[$diff])
        $failed++; continue
    }
    Write-Host ("    {0} : OK ({1} lignes)" -f $file, $e.Count) -ForegroundColor Green
}

Write-Host ""
Write-Host ("    {0} verifies, {1} en echec, {2} pas encore crees" -f $checked, $failed, $absent)

if ($failed -gt 0 -or $dup -gt 0 -or $holes.Count -gt 0) { exit 1 }
Write-Host ""
Write-Host "Aucune ligne modifiee." -ForegroundColor Green
exit 0
