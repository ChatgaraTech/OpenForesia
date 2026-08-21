<#
.SYNOPSIS
    Prouve qu'un refactor a change la FORME d'une methode, pas sa LOGIQUE.

.DESCRIPTION
    `verify-split.ps1` prouvait l'egalite octet a octet. Il cesse de s'appliquer des qu'on
    transforme du statique en instance : les octets changent par construction.

    Cet outil compare le CORPS de chaque methode a une reference figee, en NORMALISANT ce que le
    refactor a le droit de changer :

        retire    static, les modificateurs d'acces
        retire    les qualifications ajoutees par l'injection (mem., gfx., win32., ...)
        retire    les commentaires et les lignes vides
        reduit    les suites d'espaces a un seul

    Ce qui reste est la suite des instructions. Si elle change, la logique a change.

    La cle d'une methode est son NOM SEUL, pas fichier+nom : une methode qui demenage vers une
    nouvelle classe est le geste normal de ce refactor, et ne doit pas etre signalee comme une
    difference. Les apparitions et disparitions sont rapportees a part, pour information.

    Delimitation des corps : par l'INDENTATION (`    {` .. `    }`), pas par comptage d'accolades.
    66 chaines du depot contiennent des accolades, un comptage s'y perdrait.

    Tout passe par ISO-8859-1 et [IO.File]::ReadAllBytes / WriteAllBytes. `Get-Content` /
    `Set-Content` sont proscrits : ils cassent sur la region a encodage double.

.PARAMETER Snapshot
    Fige la reference. A lancer UNE FOIS, avant la premiere transformation.

.PARAMETER Root
    La racine a analyser. Par defaut `OpenForesia\src`.

.PARAMETER Baseline
    La reference figee. Par defaut `OpenForesia.Tools\bodies-baseline.txt`.

.PARAMETER Injected
    Les noms de dependances injectees dont la qualification doit etre ignoree.

.PARAMETER SelfTest
    Verifie l'outil lui-meme sur des cas fabriques, sans rien lire du depot.

.EXAMPLE
    OpenForesia.Tools\compare-bodies.ps1 -SelfTest
    OpenForesia.Tools\compare-bodies.ps1 -Snapshot
    OpenForesia.Tools\compare-bodies.ps1
#>
[CmdletBinding()]
param(
    [switch]   $Snapshot,
    [switch]   $SelfTest,
    [string]   $Root     = "OpenForesia\src",
    [string]   $Baseline = "OpenForesia.Tools\bodies-baseline.txt",
    # Les noms qui QUALIFIENT UNE DEPENDANCE, et rien d'autre.
    #
    # Cette liste n'est pas une liste de suppression : elle nomme les accesseurs par lesquels un
    # sous-systeme atteint ce dont il depend. `Ui.RunTextEngine(x)` et `RunTextEngine(x)` appellent
    # la meme logique ; seule la facon de l'atteindre a change, et c'est justement ce qu'un refactor
    # a le droit de faire.
    #
    # N'y ajouter un nom QUE s'il designe une dependance injectee ou un point d'entree de
    # sous-systeme. Y ajouter un nom pour faire taire un signalement genant vide l'outil de sa
    # valeur - c'est la pente classique des fichiers de suppression.
    # `aud` (ajoute le 2026-08-21 pour la conversion du son) : verifie AVANT de l'inscrire, comme
    # la note de TODO.md l'exige - `grep -rn --include=*.cs -iE "\baud\b" OpenForesia/src` rend zero
    # occurrence hors commentaire. Il ne peut donc masquer aucune ligne de logique existante.
    [string[]] $Injected = @('mem','memory','gameMemory','gfx','win32','winsys3d','wsyskern',
                             'ui','uiText','uiWindow','inp','aud','audio','input','resources','video',
                             'model3d','primitives2d','prims','vid','saveData','saves','host','display')
)

$ErrorActionPreference = 'Stop'
$Latin1 = [System.Text.Encoding]::GetEncoding(28591)

function Read-LinesExact([string] $Path) {
    $bytes = [System.IO.File]::ReadAllBytes($Path)
    return $Latin1.GetString($bytes) -split "`r`n|`n", -1
}

function Write-LinesExact([string] $Path, [string[]] $Lines) {
    [System.IO.File]::WriteAllBytes($Path, $Latin1.GetBytes(($Lines -join "`r`n")))
}

# --- normalisation : ce que le refactor a le droit de changer ---
function Normalize-Line([string] $line, [string[]] $inj) {
    $s = $line
    $s = $s -replace '//.*$', ''                       # commentaire de fin de ligne
    if ($s -match '^\s*$') { return '' }
    $s = $s -replace '\bstatic\b', ''
    $s = $s -replace '\b(private|internal|public|protected)\b', ''
    foreach ($d in $inj) { $s = $s -replace ("(?<![A-Za-z0-9_.])" + [regex]::Escape($d) + "\."), '' }
    $s = $s -replace '\s+', ' '
    return $s.Trim()
}

# --- extraction des corps d'un fichier ---
function Get-Bodies([string[]] $lines, [string] $file, [string[]] $inj) {
    $out = @{}
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $decl = $lines[$i]
        if ($decl -match '^    \[.*\]\s*(private|internal|public|protected)\s') {
            $decl = $decl -replace '^    \[.*\]\s*', '    '
        }
        if ($decl -notmatch '^    (private|internal|public|protected)\s') { continue }

        # le nom : le token juste avant la premiere parenthese
        $head = $decl
        if ($head -match '=>') { $head = $head.Substring(0, $head.IndexOf('=>')) }
        if ($head -notmatch '\(') { continue }                       # champ, pas methode
        $sig = $head.Substring(0, $head.IndexOf('('))
        $parts = $sig -split '[^A-Za-z0-9_]+' | Where-Object { $_ -ne '' }
        if ($parts.Count -eq 0) { continue }
        $name = $parts[$parts.Count - 1]

        $body = New-Object System.Collections.Generic.List[string]

        if ($lines[$i] -match '=>') {
            # corps-expression : de `=>` jusqu'au `;`, sur une ou plusieurs lignes
            $j = $i
            $frag = $lines[$j].Substring($lines[$j].IndexOf('=>') + 2)
            while ($true) {
                $n = Normalize-Line $frag $inj
                if ($n -ne '') { $body.Add($n) }
                if ($frag -match ';\s*$') { break }
                $j++
                if ($j -ge $lines.Count) { break }
                $frag = $lines[$j]
            }
            $i = $j
        }
        else {
            # corps en bloc : la ligne suivante non vide doit etre exactement `    {`
            $j = $i + 1
            while ($j -lt $lines.Count -and $lines[$j] -match '^\s*$') { $j++ }
            if ($j -ge $lines.Count -or $lines[$j] -ne '    {') { continue }   # extern, ou declaration nue
            $j++
            while ($j -lt $lines.Count -and $lines[$j] -ne '    }') {
                $n = Normalize-Line $lines[$j] $inj
                if ($n -ne '') { $body.Add($n) }
                $j++
            }
            $i = $j
        }

        if ($body.Count -eq 0) { continue }
        if ($out.ContainsKey($name)) { $name = "$name#$file" }        # homonyme : desambiguise
        $out[$name] = $body.ToArray()
    }
    return $out
}

function Get-TreeBodies([string] $root, [string[]] $inj) {
    $all = @{}
    foreach ($f in (Get-ChildItem -LiteralPath $root -Recurse -Filter *.cs)) {
        $rel = $f.FullName.Substring((Resolve-Path -LiteralPath $root).Path.Length + 1) -replace '\\','/'
        $b = Get-Bodies (Read-LinesExact $f.FullName) $rel $inj
        foreach ($k in $b.Keys) { $all[$k] = $b[$k] }
    }
    return $all
}

function Format-Snapshot($bodies) {
    $lines = New-Object System.Collections.Generic.List[string]
    foreach ($k in ($bodies.Keys | Sort-Object)) {
        $lines.Add("## $k")
        foreach ($l in $bodies[$k]) { $lines.Add($l) }
    }
    return $lines.ToArray()
}

function Parse-Snapshot([string[]] $lines) {
    $out = @{}; $cur = $null; $buf = $null
    foreach ($l in $lines) {
        if ($l -like '## *') {
            if ($cur -ne $null) { $out[$cur] = $buf.ToArray() }
            $cur = $l.Substring(3); $buf = New-Object System.Collections.Generic.List[string]
        }
        elseif ($cur -ne $null -and $l -ne '') { $buf.Add($l) }
    }
    if ($cur -ne $null) { $out[$cur] = $buf.ToArray() }
    return $out
}

# ============================ auto-verification ============================
if ($SelfTest) {
    Write-Host "Auto-verification de l'outil"
    $ok = 0; $ko = 0
    function Check([string] $label, [bool] $expected, [string[]] $a, [string[]] $b) {
        $ba = Get-Bodies $a 'x.cs' @('mem','gfx')
        $bb = Get-Bodies $b 'x.cs' @('mem','gfx')
        $same = $true
        foreach ($k in $ba.Keys) {
            if (-not $bb.ContainsKey($k)) { $same = $false; break }
            if (($ba[$k] -join '|') -ne ($bb[$k] -join '|')) { $same = $false; break }
        }
        if ($same -eq $expected) { Write-Host ("  OK    " + $label) -ForegroundColor Green; $script:ok++ }
        else { Write-Host ("  ECHEC " + $label + " (attendu identique=$expected)") -ForegroundColor Red; $script:ko++ }
    }
    $base = @('    private static int F(int a)', '    {', '        int x = Table[a];', '        return x + 1;', '    }')

    Check "fichier inchange -> identique" $true $base $base
    Check "`static` retire -> identique" $true $base `
        @('    private int F(int a)', '    {', '        int x = Table[a];', '        return x + 1;', '    }')
    Check "qualification ajoutee -> identique" $true $base `
        @('    private int F(int a)', '    {', '        int x = mem.Table[a];', '        return x + 1;', '    }')
    Check "commentaire ajoute -> identique" $true $base `
        @('    private static int F(int a)', '    {', '        // une explication', '        int x = Table[a];', '        return x + 1;', '    }')
    Check "indentation changee -> identique" $true $base `
        @('    private static int F(int a)', '    {', '            int x  =  Table[a];', '        return x + 1;', '    }')
    Check "INSTRUCTION changee -> DIFFERENT" $false $base `
        @('    private static int F(int a)', '    {', '        int x = Table[a];', '        return x + 2;', '    }')
    Check "instruction SUPPRIMEE -> DIFFERENT" $false $base `
        @('    private static int F(int a)', '    {', '        return x + 1;', '    }')
    Check "corps-expression inchange -> identique" $true `
        @('    private static int G() => Table[0] + 1;') @('    private int G() => mem.Table[0] + 1;')
    Check "corps-expression change -> DIFFERENT" $false `
        @('    private static int G() => Table[0] + 1;') @('    private int G() => mem.Table[0] + 2;')

    Write-Host ""
    Write-Host ("  {0} reussis, {1} en echec" -f $ok, $ko)
    if ($ko -gt 0) { exit 1 }
    Write-Host "L'outil detecte bien un changement de logique, et ignore bien un changement de forme." -ForegroundColor Green
    exit 0
}

# ============================ figer la reference ============================
if ($Snapshot) {
    $bodies = Get-TreeBodies $Root $Injected
    Write-LinesExact (Join-Path (Get-Location) $Baseline) (Format-Snapshot $bodies)
    Write-Host ("Reference figee : {0}" -f $Baseline)
    Write-Host ("  {0} corps de methode" -f $bodies.Count)
    exit 0
}

# ============================ comparer ============================
if (-not (Test-Path -LiteralPath $Baseline)) {
    Write-Host "Aucune reference. Lancer d'abord : compare-bodies.ps1 -Snapshot" -ForegroundColor Yellow
    exit 2
}

$ref = Parse-Snapshot (Read-LinesExact (Resolve-Path -LiteralPath $Baseline).Path)
$now = Get-TreeBodies $Root $Injected

Write-Host ("Reference : {0} corps ; arbre courant : {1} corps" -f $ref.Count, $now.Count)
Write-Host ""

$changed = 0; $gone = 0; $added = 0
foreach ($k in ($ref.Keys | Sort-Object)) {
    if (-not $now.ContainsKey($k)) { $gone++; continue }
    $a = $ref[$k]; $b = $now[$k]
    $diff = -1
    $max = [Math]::Min($a.Count, $b.Count)
    for ($i = 0; $i -lt $max; $i++) { if ($a[$i] -cne $b[$i]) { $diff = $i; break } }
    if ($diff -lt 0 -and $a.Count -ne $b.Count) { $diff = $max }
    if ($diff -ge 0) {
        $changed++
        Write-Host ("  LOGIQUE CHANGEE : {0}  (ligne de corps {1})" -f $k, ($diff + 1)) -ForegroundColor Red
        if ($diff -lt $a.Count) { Write-Host ("      reference : {0}" -f $a[$diff]) }
        if ($diff -lt $b.Count) { Write-Host ("      courant   : {0}" -f $b[$diff]) }
    }
}
foreach ($k in $now.Keys) { if (-not $ref.ContainsKey($k)) { $added++ } }

Write-Host ""
if ($gone  -gt 0) { Write-Host ("  {0} methode(s) de la reference absentes de l'arbre courant" -f $gone) -ForegroundColor Yellow }
if ($added -gt 0) { Write-Host ("  {0} methode(s) nouvelles (attendu : code neuf du refactor)" -f $added) -ForegroundColor Yellow }

if ($changed -gt 0) {
    Write-Host ("  {0} methode(s) dont la LOGIQUE a change." -f $changed) -ForegroundColor Red
    exit 1
}
Write-Host "Aucune logique modifiee." -ForegroundColor Green
exit 0
