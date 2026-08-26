<#
.SYNOPSIS
    Assemble la page consultable du bestiaire a partir des CSV extraits du CD.

.DESCRIPTION
    Le verbe `bestiaire` d'OpenForesia.Tools ecrit sept fichiers dans
    Documentation/Reference/Bestiaire/. Ce script en fait la jointure et l'injecte dans
    bestiaire-page.template.html, a la place du marqueur /*__DATA__*/, pour produire
    bestiaire.html — une page autonome, ouvrable directement dans un navigateur.

    Il ne lit jamais le CD : regenerer les CSV d'abord si les donnees ont change.

        dotnet run --project OpenForesia.Tools -- bestiaire "E:\" --sortie Documentation\Reference\Bestiaire
        OpenForesia.Tools\build-bestiaire-page.ps1

    La zone 0 est ecartee partout : ses noms sont illisibles et ses sous-zones se repetent.
    Voir LISEZMOI.md.
#>
[CmdletBinding()]
param(
    [string] $DataDir = "Documentation\Reference\Bestiaire",
    [string] $Template = "Documentation\Reference\Bestiaire\bestiaire-page.template.html",
    [string] $Out = "Documentation\Reference\Bestiaire\bestiaire.html"
)

$ErrorActionPreference = 'Stop'

foreach ($f in @('modeles.csv', 'especes.csv', 'formations.csv', 'butin.csv', 'objets.csv', 'butin_par_objet.csv')) {
    $p = Join-Path $DataDir $f
    if (-not (Test-Path $p)) { throw "Manquant : $p — relancer le verbe bestiaire avec --sortie $DataDir" }
}

$mod = Import-Csv (Join-Path $DataDir 'modeles.csv')          | Where-Object zone -ne '0'
$sp  = Import-Csv (Join-Path $DataDir 'especes.csv')          | Where-Object zone -ne '0'
$fo  = Import-Csv (Join-Path $DataDir 'formations.csv')       | Where-Object zone -ne '0'
$bu  = Import-Csv (Join-Path $DataDir 'butin.csv')            | Where-Object zone -ne '0'
$ob  = Import-Csv (Join-Path $DataDir 'objets.csv')           | Where-Object zone -ne '0'
$dr  = Import-Csv (Join-Path $DataDir 'butin_par_objet.csv')  | Where-Object zone -ne '0'

# especes par (zone, sous-zone, modele) : une fiche peut servir a plusieurs especes
$spByModel = @{}
foreach ($s in $sp) {
    $k = "$($s.zone)/$($s.sous_zone)/$($s.modele)"
    if (-not $spByModel[$k]) { $spByModel[$k] = @() }
    $spByModel[$k] += $s
}

# placements par (zone, sous-zone, espece)
$spawnCount = @{}
foreach ($f in $fo) { $k = "$($f.zone)/$($f.sous_zone)/$($f.espece)"; $spawnCount[$k] = 1 + [int]$spawnCount[$k] }

# les huit rangs de butin, par (zone, sous-zone, classe)
$loot = @{}
foreach ($b in $bu) {
    $k = "$($b.zone)/$($b.sous_zone)/$($b.classe)"
    if (-not $loot[$k]) { $loot[$k] = New-Object string[] 8 }
    $loot[$k][[int]$b.rang] = $b.objet
}

$variants = foreach ($m in $mod) {
    $k = "$($m.zone)/$($m.sous_zone)/$($m.modele)"
    $species = $spByModel[$k]
    $ais = @(); $classes = @(); $spawns = 0
    foreach ($s in $species) {
        $ais += [int]$s.ia
        $classes += [int]$s.classe_butin
        $spawns += [int]$spawnCount["$($s.zone)/$($s.sous_zone)/$($s.espece)"]
    }
    [pscustomobject]@{
        nom = $m.nom; zone = [int]$m.zone; sz = [int]$m.sous_zone; mdl = [int]$m.modele
        rang = [int]$m.rang_0x10; pv = [int]$m.pv_0x14; pm = [int]$m.pm_0x16; butin = [int]$m.butin_0x22
        c = @([int]$m.c0, [int]$m.c1, [int]$m.c2, [int]$m.c3, [int]$m.c4, [int]$m.c5, [int]$m.c6, [int]$m.c7)
        # Les huit octets de `modele+0x28` : les affinites elementaires, dans le meme ordre que
        # les huit de `+0x18` d'un objet. Voir LISEZMOI.md.
        el = @(0..7 | ForEach-Object { [Convert]::ToInt32($m.octets.Substring((0x28 + $_) * 2, 2), 16) })
        ia = @($ais | Sort-Object -Unique); cls = @($classes | Sort-Object -Unique); spawns = $spawns
        lt = @(if ($classes.Count -gt 0) { $loot["$($m.zone)/$($m.sous_zone)/$($classes[0])"] } else { @() })
        hex = $m.octets
    }
}

# Un monstre, pas une fiche. Le meme Goblin a jusqu'a NEUF fiches — une par sous-zone ou il
# apparait, avec des valeurs qui montent d'une zone a l'autre. Les lister a plat remplissait le
# tableau de doublons ; le nom devient donc la ligne, et les fiches deviennent ses variantes.
$rows = foreach ($g in ($variants | Group-Object nom)) {
    $v = @($g.Group | Sort-Object rang, zone, sz)
    [pscustomobject]@{
        nom  = $g.Name
        z    = @($v | ForEach-Object { $_.zone } | Sort-Object -Unique)
        rmin = ($v | Measure-Object rang -Minimum).Minimum
        rmax = ($v | Measure-Object rang -Maximum).Maximum
        pvmin = ($v | Measure-Object pv -Minimum).Minimum
        pvmax = ($v | Measure-Object pv -Maximum).Maximum
        pmmin = ($v | Measure-Object pm -Minimum).Minimum
        pmmax = ($v | Measure-Object pm -Maximum).Maximum
        v    = $v
    }
}

# L'index inverse : un objet, et tout ce qui le lache.
#
# La virgule de tete de `,@(...)` n'est pas decorative : sans elle, ForEach-Object **deroule** le
# tableau interne dans le pipeline et `src` devient une liste plate de six fois N valeurs au lieu
# d'une liste de N sources. Le JSON reste valide, la page ne leve rien, et la colonne Zone affiche
# « Zundefined ». C'est le piege classique de PowerShell sur les tableaux imbriques.
#
# Les rangs se CUMULENT. Un meme objet occupe souvent plusieurs des huit rangs d'une classe —
# l'Abeille lache l'Aiguille medicinale aux rangs 0, 1 et 2 — et la chance reelle est alors la
# somme : 25 + 17 + 15 = 57 %, pas 25. Regrouper par (zone, sous-zone, monstre) est donc une
# correction, pas un confort d'affichage. Le CSV, lui, garde une ligne par rang : c'est la donnee
# fidele, et c'est ici que se fait la lecture.

# La fiche d'objet : description et bonus. Un meme objet figure au catalogue de plusieurs
# sous-zones, avec les memes octets ; la premiere occurrence suffit.
$itemSheet = @{}
foreach ($i in $ob) {
    if (-not $itemSheet.ContainsKey($i.nom)) {
        $itemSheet[$i.nom] = [pscustomobject]@{
            desc = $i.description
            att  = [int]$i.att_0x30
            def  = [int]$i.def_0x32
            cls  = [Convert]::ToInt32($i.elements_0x12, 16) -band 0xff
            b16  = @(if ($i.bonus_0x20) { $i.bonus_0x20 -split ';' | ForEach-Object { [int]$_ } } else { @() })
            b8   = @(if ($i.bonus_0x18) { $i.bonus_0x18 -split ';' | ForEach-Object { [int]$_ } } else { @() })
            b8a  = [int]$i.bonus_0x8a
        }
    }
}

$items = foreach ($g in ($dr | Group-Object objet | Sort-Object Name)) {
    $bySource = $g.Group | Group-Object zone, sous_zone, monstre
    $sheet = $itemSheet[$g.Name]
    [pscustomobject]@{
        nom  = $g.Name
        desc = if ($sheet) { $sheet.desc } else { '' }
        att  = if ($sheet) { $sheet.att } else { 0 }
        def  = if ($sheet) { $sheet.def } else { 0 }
        kls  = if ($sheet) { $sheet.cls } else { 0 }
        b16  = @(if ($sheet) { $sheet.b16 } else { @() })
        b8   = @(if ($sheet) { $sheet.b8 } else { @() })
        b8a  = if ($sheet) { $sheet.b8a } else { 0 }
        src = @($bySource | ForEach-Object {
            $f = $_.Group[0]
            $ranks = ($_.Group | ForEach-Object { [int]$_.rang } | Sort-Object) -join ','
            $total = ($_.Group | Measure-Object -Property pourcent_plage_100 -Sum).Sum
            , @([int]$f.zone, [int]$f.sous_zone, $f.monstre, $ranks,
                [math]::Round($total, 1), [int]$f.placements)
        })
    }
}

$payload = [pscustomobject]@{
    m = @($rows)
    i = @($items)
    stats = [pscustomobject]@{
        fiches     = @($variants).Count
        noms       = ($mod.nom | Sort-Object -Unique).Count
        objets     = ($ob.nom | Sort-Object -Unique).Count
        lachables  = ($dr.objet | Sort-Object -Unique).Count
        formations = ($fo | Group-Object zone, sous_zone, formation).Count
        placements = $fo.Count
    }
}

# Le garde-fou de l'aplatissement : une source est un tuple de six champs. Si le deroulement du
# pipeline recommence un jour, mieux vaut echouer ici que publier une page qui affiche
# « Zundefined » sans rien lever.
$firstSrc = @($items)[0].src
if ($firstSrc.Count -eq 0 -or @($firstSrc[0]).Count -ne 6) {
    throw "Les sources d'objet sont aplaties : src[0] devrait etre un tuple de 6 champs, il en a $(@($firstSrc[0]).Count)."
}

$json = $payload | ConvertTo-Json -Depth 6 -Compress
$html = Get-Content $Template -Raw -Encoding utf8

$marker = '/*__DATA__*/{ m: [], i: [], stats: {} }'
if ($html -notmatch [regex]::Escape($marker)) { throw "Marqueur introuvable dans $Template" }

$html = $html.Replace($marker, $json)
Set-Content -Path $Out -Value $html -Encoding utf8 -NoNewline

$ko = [math]::Round((Get-Item $Out).Length / 1KB)
Write-Host "$Out — $ko Ko, $($payload.stats.fiches) fiches, $($payload.stats.lachables) objets obtenables"
