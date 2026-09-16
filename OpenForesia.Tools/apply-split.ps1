<#
.SYNOPSIS
    Extrait des fichiers d'un gros fichier source, en rederivant toujours depuis la reference figee.

.DESCRIPTION
    Ecrit pour `split-blaze-into-subsystems` (Blaze.cs, 2026-08-19), reutilise tel quel par
    `split-worldmaptick-by-subject` (WorldMapTick.cs, 2026-09-16) : la source, le manifeste, la
    reference, la racine, le reliquat et le preambule sont tous des parametres.

    Chaque appel reconstruit l'ETAT COMPLET du decoupage a partir de la reference et du manifeste :

      - chaque fichier deja extrait (`-Done`) plus ceux demandes (`-File`) est reecrit depuis ses
        plages de la reference ;
      - le RELIQUAT (`-Residual`, par defaut `Blaze.cs`) est reecrit comme le preambule, PLUS
        toutes les plages non encore extraites, PLUS l'accolade fermante.

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
    restent hors du reliquat.

.PARAMETER KeepDone
    Ne PAS reecrire les fichiers `-Done` : ils sont seulement retires du reliquat.

    A passer des qu'un lot precedent a recu une ecriture a la main - un bandeau d'en-tete, par
    exemple. Sans ce commutateur, la rederivation depuis la reference les EFFACE en silence : le
    fichier redevient conforme au manifeste, donc `verify-split.ps1` reste vert, et rien ne signale
    la perte. C'est le seul endroit de ce script ou « rederiver depuis la reference » est la
    mauvaise chose a faire.

.PARAMETER Residual
    Le fichier qui recoit le reliquat - tout ce qui n'est pas encore extrait -, relatif a -Root.
    Par defaut `Blaze.cs`, le decoupage de 2026-08-19. Le decoupage de `WorldMapTick.cs` passe
    `-Root OpenForesia\src\Blaze\Screens\State03 -Residual WorldMapTick.cs`.

    Quand le reliquat devient VIDE - tous les fichiers du manifeste sont extraits - le script
    n'ecrit rien et signale que le fichier est a supprimer a la main. C'est le cas normal du
    dernier lot, ou le reliquat est lui-meme devenu une entree du manifeste.

.PARAMETER PreambleRanges
    Les plages de lignes de la reference qui forment le preambule commun, separees par des
    virgules : `1-7,18-24`. Le preambule N'EST PAS TOUJOURS CONTIGU - dans `WorldMapTick.cs` les
    lignes 8..16 decrivent `FUN_00516fc0` en particulier, et les recopier en tete de 67 autres
    fichiers y ecrirait une contre-verite. Elles restent donc dans la plage du reliquat.

    Quand ce parametre est absent, le preambule est `1..PreambleEnd`, comportement d'origine.

.PARAMETER ResidualPreambleRanges
    Le preambule du RELIQUAT, quand il differe de celui des fichiers cibles. Meme format.

    `WorldMapTick.cs` passe `-PreambleRanges "1-7,18-24" -ResidualPreambleRanges "1-24"` : les
    lignes 8..16 decrivent `FUN_00516fc0` en particulier, elles n'ont rien a faire en tete des 67
    autres fichiers, mais elles doivent RESTER sur le reliquat tant qu'il porte cette fonction.
    Elles ne sont dans aucune plage du manifeste - sans ce parametre elles disparaitraient du depot
    pendant les sept premiers lots.

    Absent, il vaut -PreambleRanges.

.PARAMETER WhatIf
    N'ecrit rien ; affiche seulement ce qui serait fait.

.EXAMPLE
    OpenForesia.Tools\apply-split.ps1 -File Platform/Win32.cs
    OpenForesia.Tools\apply-split.ps1 -File Platform/Crt.cs -Done Platform/Win32.cs

.EXAMPLE
    OpenForesia.Tools\apply-split.ps1 -File Spells/School0Wizard.cs `
        -Manifest OpenForesia.Tools\worldmaptick-manifest.tsv `
        -Baseline OpenForesia.Tools\worldmaptick-baseline.txt `
        -Root OpenForesia\src\Blaze\Screens\State03 `
        -Residual WorldMapTick.cs -PreambleRanges "1-7,18-24"
#>
[CmdletBinding()]
param(
    [string[]] $File = @(),
    [string[]] $Done = @(),
    [string]   $Manifest = "OpenForesia.Tools\split-manifest.tsv",
    [string]   $Baseline = "OpenForesia.Tools\blaze-baseline.txt",
    [string]   $Root     = "OpenForesia\src\Blaze",
    [string]   $Residual = "Blaze.cs",
    [int]      $PreambleEnd = 16,
    [string]   $PreambleRanges = "",
    [string]   $ResidualPreambleRanges = "",
    [switch]   $KeepDone,
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

# --- les preambules, derives de la reference ---
# Entete, using, namespace, declaration de classe, accolade ouvrante. Contigu (1..PreambleEnd) ou
# non (-PreambleRanges "1-7,18-24") : voir l'aide des parametres.
function Build-Preamble([string] $Spec) {
    $out = New-Object System.Collections.Generic.List[string]
    if ([string]::IsNullOrWhiteSpace($Spec)) {
        for ($i = 1; $i -le $PreambleEnd; $i++) { $out.Add($orig[$i - 1]) }
        return ,@($out.ToArray())
    }
    foreach ($s in ($Spec -split ',')) {
        $s = $s.Trim()
        if ($s -eq '') { continue }
        if ($s -notmatch '^(\d+)-(\d+)$') { throw "Plage de preambule illisible : '$s' (attendu 'debut-fin')" }
        $a = [int]$Matches[1]; $b = [int]$Matches[2]
        if ($a -lt 1 -or $b -lt $a -or $b -ge $orig.Count) { throw "Plage de preambule hors reference : '$s'" }
        for ($i = $a; $i -le $b; $i++) { $out.Add($orig[$i - 1]) }
    }
    return ,@($out.ToArray())
}

$preamble      = Build-Preamble $PreambleRanges
$preambleLabel = if ([string]::IsNullOrWhiteSpace($PreambleRanges)) { "1..$PreambleEnd" } else { $PreambleRanges }

# Le reliquat peut avoir un preambule A LUI. C'est le cas de `WorldMapTick.cs` : ses lignes 8..16
# decrivent `FUN_00516fc0`, elles n'ont rien a faire en tete des 67 autres fichiers mais elles
# doivent RESTER sur le reliquat tant qu'il porte cette fonction - sans quoi elles disparaitraient
# du depot pendant les sept premiers lots, alors qu'elles ne sont dans aucune plage du manifeste.
$residualSpec  = if ([string]::IsNullOrWhiteSpace($ResidualPreambleRanges)) { $PreambleRanges } else { $ResidualPreambleRanges }
$residualPre   = Build-Preamble $residualSpec
$residualLabel = if ([string]::IsNullOrWhiteSpace($residualSpec)) { "1..$PreambleEnd" } else { $residualSpec }

Write-Host ("Reference : {0} lignes ; manifeste : {1} plages" -f ($orig.Count - 1), $ranges.Count)
Write-Host ("Preambule : lignes {0} ({1} lignes)" -f $preambleLabel, $preamble.Count)
Write-Host ("Reliquat  : {0} (preambule {1}, {2} lignes)" -f $Residual, $residualLabel, $residualPre.Count)
Write-Host ("Deja extraits : {0} ; a extraire : {1}" -f $Done.Count, $File.Count)
Write-Host ""

# --- 1. les fichiers cibles ---
$doneSet = @($Done | ForEach-Object { $_ -replace '\\', '/' })
foreach ($f in $extracted) {
    if ($KeepDone -and ($doneSet -contains $f)) {
        Write-Host ("  {0,-46} conserve en l'etat (-KeepDone)" -f $f) -ForegroundColor DarkGray
        continue
    }
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

# --- 2. le reliquat : le preambule plus tout ce qui n'est pas encore parti ---
$rest = New-Object System.Collections.Generic.List[string]
foreach ($r in ($ranges | Sort-Object Start)) {
    if ($extracted -contains $r.File) { continue }
    for ($i = $r.Start; $i -le $r.End; $i++) { $rest.Add($orig[$i - 1]) }
}
while ($rest.Count -gt 0 -and ($rest[$rest.Count - 1] -eq '}' -or [string]::IsNullOrWhiteSpace($rest[$rest.Count - 1]))) {
    if ($rest[$rest.Count - 1] -eq '}') { $rest.RemoveAt($rest.Count - 1); break }
    $rest.RemoveAt($rest.Count - 1)
}
$residualPath = Join-Path $Root ($Residual -replace '/', '\')
Write-Host ""
if ($rest.Count -eq 0) {
    # Tout le manifeste est extrait. Le reliquat n'a plus de contenu propre : ecrire une classe
    # partielle vide serait un fichier fantome. On signale, on laisse la suppression a la main.
    Write-Host ("  {0,-40} {1,6} lignes restantes -- RELIQUAT VIDE" -f $Residual, 0) -ForegroundColor Yellow
    if (Test-Path -LiteralPath $residualPath) {
        Write-Host ("  A SUPPRIMER A LA MAIN : {0}" -f $residualPath) -ForegroundColor Yellow
    }
} else {
    $residualOut = @($residualPre) + $rest.ToArray() + @('}', '')
    Write-Host ("  {0,-40} {1,6} lignes restantes" -f $Residual, $rest.Count)
    if (-not $WhatIf) { Write-LinesExact $residualPath $residualOut }
}

if ($WhatIf) { Write-Host ""; Write-Host "-WhatIf : rien ecrit." -ForegroundColor Yellow }
