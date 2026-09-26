<#
.SYNOPSIS
    Produit OpenForesia-Setup.exe, l'installeur à publier sur GitHub.

.DESCRIPTION
    Quatre temps (openspec/changes/install-from-iso, lot 5) :

      1. publie le jeu en autonome x86, élagué, en un seul OpenForesia.exe — le runtime .NET y est
         lié, réduit à ce que le jeu appelle : ni DLL ni prérequis chez le joueur ;
      2. le zippe dans OpenForesia.Installer\Jeu\jeu.zip, que l'installeur embarque ;
      3. publie l'installeur en un seul fichier autonome ;
      4. contrôle que rien de ce qui sort ne contient une donnée du jeu, et retire le zip — sans lui,
         un installeur de développement retombe sur la build du dépôt.

    Aucune donnée du jeu n'est embarquée : l'installeur les extrait de l'ISO du joueur.
    editions.json ne porte que des chemins, des tailles et des sommes.

        OpenForesia.Tools\publish-release.ps1
        OpenForesia.Tools\publish-release.ps1 -Version 0.2.0

    Sortie : OpenForesia.Installer\bin\Release\publie\OpenForesia-Setup.exe
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

    $fichiersJeu = Get-ChildItem $temporaire -Recurse -File |
        ForEach-Object { $_.FullName.Substring($temporaire.Length + 1) }
    Controler-Contenu $fichiersJeu 'le jeu publié'
    if (@($fichiersJeu).Count -ne 1 -or $fichiersJeu -ne 'OpenForesia.exe') {
        throw "le jeu publié doit tenir en OpenForesia.exe seul ; il contient : $($fichiersJeu -join ', ')"
    }

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

    Etape "4. Contrôles"
    $setup = Join-Path $sortie 'OpenForesia-Setup.exe'
    if (-not (Test-Path $setup)) { throw "OpenForesia-Setup.exe n'a pas été produit" }
    $autres = Get-ChildItem $sortie -File | Where-Object Name -ne 'OpenForesia-Setup.exe'
    if ($autres) { throw "la publication a laissé d'autres fichiers à côté de l'installeur : $($autres.Name -join ', ')" }

    # editions.json : que des chemins, des tailles et des sommes.
    $editions = Get-Content (Join-Path $depot 'OpenForesia.Installer\Editions\editions.json') -Raw | ConvertFrom-Json
    $champs = $editions.editions.fichiers | ForEach-Object { $_.PSObject.Properties.Name } | Sort-Object -Unique
    $attendus = 'chemin', 'coupeRiff', 'iso', 'md5', 'taille'
    if (Compare-Object $champs $attendus) { throw "editions.json porte des champs inattendus : $($champs -join ', ')" }
    Write-Host "   editions.json : $(@($editions.editions.fichiers).Count) fichiers décrits, champs $($champs -join ', ')"

    Write-Host ("`nOpenForesia-Setup.exe {0} : {1:N1} Mo`n{2}" -f $Version, ((Get-Item $setup).Length / 1MB), $setup) -ForegroundColor Green
}
finally {
    # Le zip ne sert qu'à la publication : laissé en place, il ferait embarquer le jeu à chaque
    # build de développement de l'installeur.
    if (Test-Path $zip) { Remove-Item $zip -Force }
    if (Test-Path $temporaire) { Remove-Item $temporaire -Recurse -Force }
}
