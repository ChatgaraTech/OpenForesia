<#
.SYNOPSIS
    Assemble l'editeur de sauvegarde : le modele HTML plus le catalogue d'objets et de sorts du CD.

.DESCRIPTION
    Lit trois CSV que le verbe `bestiaire` d'OpenForesia.Tools a ecrits dans
    Documentation/Reference/Bestiaire/ — objets.csv, vignettes.csv et sorts.csv — et les injecte dans
    editeur-sauvegarde.template.html a la place du marqueur /*__DATA__*/. Le resultat,
    editeur-sauvegarde.html, est une page autonome : elle s'ouvre d'un double-clic, sans serveur.

    Il ne lit jamais le CD : regenerer les CSV d'abord si les donnees ont change.

        OpenForesia.Tools\Html\build-editeur-sauvegarde.ps1
        node OpenForesia.Tools\Html\check-editeur-sauvegarde.mjs

    Les fiches d'objet sont dedoublonnees sur leurs 0x140 octets : la meme fiche revient dans le
    catalogue de chaque sous-zone qui la vend ou la cache.

    Les fiches JAPONAISES sont ecartees. La zone 0 du CD porte 213 fiches jamais traduites, dont le
    nom est du Shift-JIS (katakana demi-largeur) que la lecture Latin-1 rend illisible. Toutes sont
    en zone 0 seulement ; la regle garde un nom quand il ne contient que des caracteres d'un nom
    francais, et elle ne rejette aucune fiche d'une autre zone (verifie le 2026-09-25).
#>
[CmdletBinding()]
param(
    [string] $DataDir = "Documentation\Reference\Bestiaire",
    [string] $Template = "OpenForesia.Tools\Html\editeur-sauvegarde.template.html",
    [string] $Out = "OpenForesia.Tools\Html\editeur-sauvegarde.html"
)

$ErrorActionPreference = 'Stop'

foreach ($f in @('objets.csv', 'vignettes.csv', 'sorts.csv')) {
    $p = Join-Path $DataDir $f
    if (-not (Test-Path $p)) { throw "Manquant : $p — relancer le verbe bestiaire avec --sortie $DataDir" }
}

$ob = Import-Csv (Join-Path $DataDir 'objets.csv') -Encoding UTF8
$so = Import-Csv (Join-Path $DataDir 'sorts.csv') -Encoding UTF8

# Les vignettes du CD : les 0x140 octets qui suivent chaque fiche dans le catalogue de la sous-zone
# (palette puis image 24x24), ceux que le jeu recopie dans l'emplacement d'inventaire.
$icons = @{}
foreach ($v in (Import-Csv (Join-Path $DataDir 'vignettes.csv'))) {
    $icons["$($v.zone)/$($v.sous_zone)/$($v.objet_id)"] = $v.vignette
}

# Les caracteres d'un nom d'objet francais. Tout autre caractere trahit une fiche japonaise.
# L'apostrophe typographique passe par [char]0x2019 : PowerShell 5.1 la prend pour un guillemet simple.
$frenchName = '^[A-Za-z0-9 àâäçéèêëîïôöùûüÀÂÄÇÉÈÊËÎÏÔÖÙÛÜœŒ''' + [char]0x2019 + '.,:!?()\-+«»/&]*$'

# ---------------------------------------------------------------- les objets, une fiche par contenu
$byHex = [ordered]@{}
$japanese = 0
foreach ($o in $ob) {
    if (-not $o.nom -or $o.octets.Length -lt 0x140 * 2) { continue }
    $h = $o.octets.Substring(0, 0x140 * 2)
    if ($o.nom -cnotmatch $frenchName) {
        if ($o.zone -ne '0') { throw "Nom illisible hors de la zone 0 : « $($o.nom) » (zone $($o.zone)) — revoir la regle" }
        $japanese++
        continue
    }
    if (-not $byHex.Contains($h)) {
        $byHex[$h] = [pscustomobject]@{ n = $o.nom; d = $o.description; h = $h; i = [string]$icons["$($o.zone)/$($o.sous_zone)/$($o.objet_id)"] }
    }
}
$items = @($byHex.Values | Sort-Object n)

# ---------------------------------------------------------------- les sorts, par ecole
$spells = @(@(), @(), @(), @())
foreach ($s in ($so | Sort-Object { [int]$_.famille }, { [int]$_.indice })) {
    $f = [int]$s.famille
    if ($f -gt 3) { continue }
    $spells[$f] += [pscustomobject]@{
        n = $s.nom; pm = [int]$s.cout_pm_0x14; a = [int]$s.cout_apprentissage_0x1d; c = $s.combinaison
    }
}

$payload = [pscustomobject]@{
    items = $items
    spells = $spells
    generated = (Get-Date -Format 'yyyy-MM-dd')
}
$json = $payload | ConvertTo-Json -Compress -Depth 8

$html = [IO.File]::ReadAllText((Resolve-Path $Template), [Text.Encoding]::UTF8)
$marker = '/*__DATA__*/{ items: [], spells: [[], [], [], []], generated: '''' }'
if (-not $html.Contains($marker)) { throw "Marqueur /*__DATA__*/ introuvable dans $Template" }
$html = $html.Replace($marker, "/*__DATA__*/$json")

$outPath = Join-Path (Get-Location) $Out
[IO.File]::WriteAllText($outPath, $html, (New-Object Text.UTF8Encoding $false))

$withIcon = @($items | Where-Object { $_.i }).Count
Write-Host ("{0} fiches d'objet dont {7} avec vignette ({1} lignes japonaises ecartees), sorts {2}/{3}/{4}/{5} -> {6}" -f `
    $items.Count, $japanese, $spells[0].Count, $spells[1].Count, $spells[2].Count, $spells[3].Count, $Out, $withIcon)
