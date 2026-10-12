<#
.SYNOPSIS
    Produit OpenForesia-Setup.exe, l'installeur à publier sur GitHub.

.DESCRIPTION
    Quatre temps (openspec/changes/archive/2026-09-26-install-from-iso, lot 5) :

      1. publie le jeu en autonome x86, élagué, en un seul OpenForesia.exe — le runtime .NET y est
         lié, réduit à ce que le jeu appelle : ni DLL ni prérequis chez le joueur ; et de même
         OpenForesiaLanceur.exe, le lanceur (seul, héberger, rejoindre), à côté ;
      2. le zippe dans OpenForesia.Installer\Jeu\jeu.zip, que l'installeur embarque ;
      3. publie l'installeur en un seul fichier autonome, et pose à côté le lanceur seul (pour
         l'ami qui veut seulement rejoindre) ;
      4. contrôle que rien de ce qui sort ne contient une donnée du jeu, et retire le zip — sans lui,
         un installeur de développement retombe sur la build du dépôt.

    Aucune donnée du jeu n'est embarquée : l'installeur les extrait de l'ISO du joueur.
    editions.json ne porte que des chemins, des tailles et des sommes.

        OpenForesia.Tools\publish-release.ps1
        OpenForesia.Tools\publish-release.ps1 -Version 0.2.0

    Sortie : OpenForesia.Installer\bin\Release\publie\OpenForesia-Setup.exe et OpenForesiaLanceur.exe
#>
[CmdletBinding()]
param(
    [string]$Version = '0.1.0'
)

$ErrorActionPreference = 'Stop'
$depot = Split-Path $PSScriptRoot -Parent
$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"

$temporaire = Join-Path $depot 'OpenForesia.Installer\bin\Release\jeu-publie'
$zip = Join-Path $depot 'OpenForesia.Installer\Jeu\jeu.zip'
$sortie = Join-Path $depot 'OpenForesia.Installer\bin\Release\publie'

function Etape([string]$texte) { Write-Host "`n== $texte" -ForegroundColor Cyan }

function Publier([string[]]$arguments) {
    # -clp:ErrorsOnly : les avertissements du jeu sont connus et noieraient la sortie.
    & dotnet publish @arguments -nologo -v quiet -clp:ErrorsOnly
    if ($LASTEXITCODE -ne 0) { throw "dotnet publish a échoué ($($arguments[0]))" }
}

# Ce qui trahirait une donnée du jeu dans une release : ses dossiers et ses extensions.
$dossiersInterdits = 'French', 'Snd', 'Music', 'save'
$extensionsInterdites = '.all', '.avi', '.wav', '.snd', '.pic', '.bmp', '.ini', '.iso'

function Controler-Contenu([string[]]$chemins, [string]$quoi) {
    $fautifs = $chemins | Where-Object {
        $premier = ($_ -split '[\/]')[0]
        ($dossiersInterdits -contains $premier) -or ($extensionsInterdites -contains [IO.Path]::GetExtension($_).ToLowerInvariant())
    }
    if ($fautifs) { throw "$quoi contient des données du jeu :`n  " + ($fautifs -join "`n  ") }
    Write-Host "   $quoi : $($chemins.Count) fichiers, aucune donnée du jeu"
}

try {
    Etape "1. Publication du jeu (autonome, x86)"
    if (Test-Path $temporaire) { Remove-Item $temporaire -Recurse -Force }
    # Élagué : le runtime et les liaisons Direct3D de Silk.NET sont réduits à ce que le jeu appelle
    # (108 Mo → 21 Mo sur le disque du joueur). Un avertissement d'élagage est une erreur : il
    # signale du code que l'élagage pourrait casser sans que rien ne le dise à la compilation.
    # En un seul fichier : le runtime est lié dans OpenForesia.exe, et le dossier du joueur ne porte
    # ni DLL ni createdump.exe (l'outil de vidage mémoire du runtime, inutile au joueur).
    Publier @((Join-Path $depot 'OpenForesia\OpenForesia.csproj'), '-c', 'Release', '-r', 'win-x86',
              '--self-contained', 'true', '-p:PublishTrimmed=true', '-p:TrimMode=full',
              '-p:ILLinkTreatWarningsAsErrors=true', '-p:PublishSingleFile=true',
              '-p:EnableCompressionInSingleFile=true', '-p:DebugType=none', "-p:Version=$Version", '-o', $temporaire)

    # Le lanceur (openspec/changes/add-multiplayer-streaming, renommé par add-launcher-solo-gamepad) :
    # son exe à côté de celui du jeu, publié de même — élagué, en un seul fichier —, dans le même zip.
    Publier @((Join-Path $depot 'OpenForesia.Lanceur\OpenForesia.Lanceur.csproj'), '-c', 'Release', '-r', 'win-x86',
              '--self-contained', 'true', '-p:PublishTrimmed=true', '-p:TrimMode=full',
              '-p:ILLinkTreatWarningsAsErrors=true', '-p:PublishSingleFile=true',
              '-p:EnableCompressionInSingleFile=true', '-p:DebugType=none', "-p:Version=$Version", '-o', $temporaire)

    $fichiersJeu = Get-ChildItem $temporaire -Recurse -File |
        ForEach-Object { $_.FullName.Substring($temporaire.Length + 1) } | Sort-Object
    Controler-Contenu $fichiersJeu 'le jeu publié'
    $attendusJeu = 'OpenForesia.exe', 'OpenForesiaLanceur.exe'
    if (Compare-Object @($fichiersJeu) $attendusJeu) {
        throw "le jeu publié doit tenir en $($attendusJeu -join ' et ') ; il contient : $($fichiersJeu -join ', ')"
    }

    # Les licences, installées avec le jeu dans Licences\ : la MIT du projet, et la LGPL du décodeur
    # Indeo 4 porté de FFmpeg (Host/Indeo4/), qu'OpenForesia.exe embarque — la LGPL demande que son
    # texte accompagne le programme distribué. Le texte de la LGPL est recopié tel quel.
    $licences = Join-Path $temporaire 'Licences'
    New-Item -ItemType Directory -Force $licences | Out-Null
    Copy-Item (Join-Path $depot 'LICENSE') (Join-Path $licences 'OpenForesia-MIT.txt')
    Copy-Item (Join-Path $depot 'OpenForesia\src\Host\Indeo4\LICENSE-LGPL.txt') (Join-Path $licences 'Indeo4-LGPL-2.1.txt')
    $lisezmoi = @"
OpenForesia - licences
======================

OpenForesia est distribue sous licence MIT : voir OpenForesia-MIT.txt.

Exception : OpenForesia.exe contient un decodeur Indeo 4, utilise pour les cinematiques du jeu.
C'est un portage en C# du decodeur Indeo 4 de FFmpeg (https://ffmpeg.org), libavcodec/indeo4.c,
ivi.c et ivi_dsp.c, Copyright (c) 2009-2011 Maxim Poliakovski, revision
b0d154591161153b0ec55d9527535bc776504899. Comme sa source, il est distribue sous la GNU Lesser
General Public License, version 2.1 ou (a votre choix) toute version ulterieure : voir
Indeo4-LGPL-2.1.txt.

Son code source, et celui de tout OpenForesia, est disponible sur
https://github.com/ChatgaraTech/OpenForesia (dossier OpenForesia/src/Host/Indeo4/). Chacun peut y
modifier le decodeur et reconstruire le jeu.

Blaze & Blade / Legend of Foresia appartient a ses ayants droit. OpenForesia ne contient aucune
donnee du jeu et n'a aucun lien avec eux.

Version $Version
"@
    [IO.File]::WriteAllText((Join-Path $licences 'LISEZMOI.txt'), $lisezmoi.Replace("`r`n", "`n").Replace("`n", "`r`n"), [Text.UTF8Encoding]::new($true))
    Write-Host "   les licences : Licences\OpenForesia-MIT.txt, Indeo4-LGPL-2.1.txt, LISEZMOI.txt"

    Etape "2. Zip du jeu pour l'installeur"
    New-Item -ItemType Directory -Force (Split-Path $zip) | Out-Null
    if (Test-Path $zip) { Remove-Item $zip -Force }
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    [IO.Compression.ZipFile]::CreateFromDirectory($temporaire, $zip, 'Optimal', $false)
    Write-Host ("   {0:N1} Mo" -f ((Get-Item $zip).Length / 1MB))

    Etape "3. Publication de l'installeur (un seul fichier)"
    if (Test-Path $sortie) { Remove-Item $sortie -Recurse -Force }
    Publier @((Join-Path $depot 'OpenForesia.Installer\OpenForesia.Installer.csproj'), '-c', 'Release',
              '-r', 'win-x86', '--self-contained', 'true', '-p:PublishSingleFile=true',
              '-p:PublishTrimmed=true', '-p:TrimMode=full', '-p:ILLinkTreatWarningsAsErrors=true',
              '-p:IncludeNativeLibrariesForSelfExtract=true', '-p:EnableCompressionInSingleFile=true',
              '-p:DebugType=none', "-p:Version=$Version",
              '-o', $sortie)

    # Le lanceur seul, à côté du Setup (add-launcher-solo-gamepad, design D8) : pour l'ami qui veut
    # seulement rejoindre, sans le jeu ni l'ISO. Le même exe que celui du zip.
    Copy-Item (Join-Path $temporaire 'OpenForesiaLanceur.exe') $sortie

    Etape "4. Contrôles"
    $setup = Join-Path $sortie 'OpenForesia-Setup.exe'
    if (-not (Test-Path $setup)) { throw "OpenForesia-Setup.exe n'a pas été produit" }
    $publies = 'OpenForesia-Setup.exe', 'OpenForesiaLanceur.exe'
    $autres = Get-ChildItem $sortie -File | Where-Object { $publies -notcontains $_.Name }
    if ($autres) { throw "la publication a laissé d'autres fichiers à côté de l'installeur : $($autres.Name -join ', ')" }
    Controler-Contenu @(Get-ChildItem $sortie -File | ForEach-Object Name) 'la publication'

    # editions.json : que des chemins, des tailles et des sommes.
    $editions = Get-Content (Join-Path $depot 'OpenForesia.Installer\Editions\editions.json') -Raw | ConvertFrom-Json
    $champs = $editions.editions.fichiers | ForEach-Object { $_.PSObject.Properties.Name } | Sort-Object -Unique
    $attendus = 'chemin', 'coupeRiff', 'iso', 'md5', 'taille'
    if (Compare-Object $champs $attendus) { throw "editions.json porte des champs inattendus : $($champs -join ', ')" }
    Write-Host "   editions.json : $(@($editions.editions.fichiers).Count) fichiers décrits, champs $($champs -join ', ')"

    Write-Host ""
    foreach ($nom in $publies) {
        $fichier = Join-Path $sortie $nom
        Write-Host ("{0} {1} : {2:N1} Mo`n{3}" -f $nom, $Version, ((Get-Item $fichier).Length / 1MB), $fichier) -ForegroundColor Green
    }
}
finally {
    # Le zip ne sert qu'à la publication : laissé en place, il ferait embarquer le jeu à chaque
    # build de développement de l'installeur.
    if (Test-Path $zip) { Remove-Item $zip -Force }
    if (Test-Path $temporaire) { Remove-Item $temporaire -Recurse -Force }
}
