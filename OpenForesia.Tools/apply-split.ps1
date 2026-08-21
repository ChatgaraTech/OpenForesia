<#
.SYNOPSIS
    Extrait des fichiers de Blaze.cs, en rederivant toujours depuis la reference figee.

.DESCRIPTION
    Chaque appel reconstruit l'ETAT COMPLET du decoupage a partir de `blaze-baseline.txt` et du
    manifeste :

      - chaque fichier deja extrait (`-Done`) plus ceux demandes (`-File`) est reecrit depuis ses
        plages de la reference ;
      - `Blaze.cs` est reecrit comme le preambule, PLUS toutes les plages non encore extraites,
        PLUS l'accolade fermante.

    Rien n'est jamais edite en place. C'est ce qui permet d'extraire par etapes sans que la
    suppression de lignes ne decale la numerotation du manifeste : les plages restent exprimees
    dans les coordonnees de la reference, qui ne bouge pas.

    Tout passe par ISO-8859-1 (un octet <-> un caractere) et [IO.File]::ReadAllBytes /
    WriteAllBytes. `Get-Content` / `Set-Content` sont proscrits : le fichier porte une region a
    encodage double qu'ils aggravent.

.PARAMETER File
    Les fichiers cibles a extraire maintenant, chemins relatifs a -Root, separes par des virgules.

.PARAMETER Done
    Les fichiers deja extraits lors d'appels precedents. Ils sont reecrits a l'identique et
    restent hors de Blaze.cs.

.PARAMETER WhatIf
    N'ecrit rien ; affiche seulement ce qui serait fait.

.EXAMPLE
    OpenForesia.Tools\apply-split.ps1 -File Platform/Win32.cs
    OpenForesia.Tools\apply-split.ps1 -File Platform/Crt.cs -Done Platform/Win32.cs
#>
[CmdletBinding()]
param(
    [string[]] $File = @(),
    [string[]] $Done = @(),
    [string]   $Manifest = "OpenForesia.Tools\split-manifest.tsv",
    [string]   $Baseline = "OpenForesia.Tools\blaze-baseline.txt",
    [string]   $Root     = "OpenForesia\src\Blaze",
    [int]      $PreambleEnd = 16,
    [switch]   $WhatIf
)

$ErrorActionPreference = 'Stop'
$Latin1 = [System.Text.Encoding]::GetEncoding(28591)

function Read-LinesExact([string] $Path) {
    $bytes = [System.IO.File]::ReadAllBytes((Resolve-Path -LiteralPath $Path).Path)
    return $Latin1.GetString($bytes) -split "`r`n|`n", -1
}

function Write-LinesExact([string] $Path, [string[]] $Lines) {
    $dir = Split-Path -Parent $Path
    if ($dir -and -not (Test-Path -LiteralPath $dir)) { New-Item -ItemType Directory -Force $dir | Out-Null }
    [System.IO.File]::WriteAllBytes($Path, $Latin1.GetBytes(($Lines -join "`r`n")))
}

$orig = Read-LinesExact $Baseline

$ranges = @()
foreach ($row in (Read-LinesExact $Manifest)) {
    if ([string]::IsNullOrWhiteSpace($row)) { continue }
    $p = $row -split "`t"
    if ($p.Count -lt 3) { continue }
    $ranges += [pscustomobject]@{ File = $p[0]; Start = [int]$p[1]; End = [int]$p[2] }
}

# Normalise les separateurs pour que -File accepte / comme \.
$extracted = @($Done + $File) | ForEach-Object { $_ -replace '\\', '/' } | Sort-Object -Unique
$known = $ranges.File | Sort-Object -Unique
foreach ($f in $extracted) {
    if ($known -notcontains $f) { throw "Fichier absent du manifeste : $f" }
}

# --- le preambule commun, derive de la reference ---
# Lignes 1..PreambleEnd : entete, using, namespace, declaration de classe, accolade ouvrante.
$preamble = $orig[0..($PreambleEnd - 1)]

Write-Host ("Reference : {0} lignes ; manifeste : {1} plages" -f ($orig.Count - 1), $ranges.Count)
Write-Host ("Deja extraits : {0} ; a extraire : {1}" -f $Done.Count, $File.Count)
Write-Host ""

# --- 1. les fichiers cibles ---
foreach ($f in $extracted) {
    $mine = @($ranges | Where-Object { $_.File -eq $f })
    $body = New-Object System.Collections.Generic.List[string]
    foreach ($r in $mine) { for ($i = $r.Start; $i -le $r.End; $i++) { $body.Add($orig[$i - 1]) } }

    # La toute derniere plage de la reference inclut l'accolade fermante de la classe : on la
    # retire du corps, elle est reposee par le postambule.
    while ($body.Count -gt 0 -and ($body[$body.Count - 1] -eq '}' -or [string]::IsNullOrWhiteSpace($body[$body.Count - 1]))) {
        if ($body[$body.Count - 1] -eq '}') { $body.RemoveAt($body.Count - 1); break }
        $body.RemoveAt($body.Count - 1)
    }

    $out = @($preamble) + $body.ToArray() + @('}', '')
    $path = Join-Path $Root ($f -replace '/', '\')
    Write-Host ("  {0,-40} {1,6} lignes, {2} plage(s)" -f $f, $body.Count, $mine.Count)
    if (-not $WhatIf) { Write-LinesExact $path $out }
}

# --- 2. Blaze.cs : le preambule plus tout ce qui n'est pas encore parti ---
$rest = New-Object System.Collections.Generic.List[string]
foreach ($r in ($ranges | Sort-Object Start)) {
    if ($extracted -contains $r.File) { continue }
    for ($i = $r.Start; $i -le $r.End; $i++) { $rest.Add($orig[$i - 1]) }
}
while ($rest.Count -gt 0 -and ($rest[$rest.Count - 1] -eq '}' -or [string]::IsNullOrWhiteSpace($rest[$rest.Count - 1]))) {
    if ($rest[$rest.Count - 1] -eq '}') { $rest.RemoveAt($rest.Count - 1); break }
    $rest.RemoveAt($rest.Count - 1)
}
$blaze = @($preamble) + $rest.ToArray() + @('}', '')
Write-Host ""
Write-Host ("  {0,-40} {1,6} lignes restantes" -f "Blaze.cs", $rest.Count)
if (-not $WhatIf) { Write-LinesExact (Join-Path $Root "Blaze.cs") $blaze }

if ($WhatIf) { Write-Host ""; Write-Host "-WhatIf : rien ecrit." -ForegroundColor Yellow }
