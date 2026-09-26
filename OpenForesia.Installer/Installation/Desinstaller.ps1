# Désinstallation d'OpenForesia — déposé dans le dossier du jeu par OpenForesia-Setup.
#
# Lancé par l'entrée « Désinstaller OpenForesia » du menu Démarrer ou par « Applications
# installées » de Windows. Retire le jeu, ses données, ses raccourcis et son entrée dans la liste
# des applications ; les sauvegardes restent si le joueur le demande.
#
# Un script et non un .exe : PowerShell est présent sur tout Windows, et un script peut effacer son
# propre fichier (il est lu en entier avant de s'exécuter), ce qu'un exécutable en cours ne peut pas.

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Windows.Forms

$dossier = $PSScriptRoot
$titre = 'Désinstaller OpenForesia'
$cle = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\OpenForesia'
$raccourciBureau = Join-Path ([Environment]::GetFolderPath('Desktop')) 'OpenForesia.lnk'
$dossierMenu = Join-Path ([Environment]::GetFolderPath('Programs')) 'OpenForesia'

function Dire([string]$texte, [string]$boutons = 'OK', [string]$icone = 'Information') {
    [System.Windows.Forms.MessageBox]::Show($texte, $titre, $boutons, $icone)
}

# On ne travaille pas depuis le dossier qu'on va supprimer.
Set-Location $env:TEMP

$reponse = Dire "Désinstaller OpenForesia de ce dossier ?`n`n$dossier" 'OKCancel' 'Question'
if ($reponse -ne 'OK') { return }

# Le jeu doit être fermé : Windows refuse d'effacer un exécutable en cours.
$exe = Join-Path $dossier 'OpenForesia.exe'
while (Get-Process -Name 'OpenForesia' -ErrorAction SilentlyContinue |
       Where-Object { $_.Path -eq $exe }) {
    $reponse = Dire "OpenForesia est en cours d'exécution. Fermez le jeu, puis cliquez sur OK." 'OKCancel' 'Warning'
    if ($reponse -ne 'OK') { return }
}

$sauvegardes = Join-Path $dossier 'save'
$garder = $false
if ((Test-Path $sauvegardes) -and (Get-ChildItem $sauvegardes -Force | Select-Object -First 1)) {
    $reponse = Dire "Conserver vos sauvegardes ?`n`nElles resteront dans :`n$sauvegardes" 'YesNoCancel' 'Question'
    if ($reponse -eq 'Cancel') { return }
    $garder = ($reponse -eq 'Yes')
}

Remove-Item $raccourciBureau -Force -ErrorAction SilentlyContinue
Remove-Item $dossierMenu -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item $cle -Recurse -Force -ErrorAction SilentlyContinue

$echecs = @()
foreach ($element in Get-ChildItem $dossier -Force) {
    if ($garder -and $element.Name -eq 'save') { continue }
    try { Remove-Item $element.FullName -Recurse -Force }
    catch { $echecs += $element.Name }
}
if (-not (Get-ChildItem $dossier -Force | Select-Object -First 1)) {
    Remove-Item $dossier -Force -ErrorAction SilentlyContinue
}

if ($echecs.Count -gt 0) {
    Dire ("OpenForesia est désinstallé, mais ces éléments n'ont pas pu être supprimés :`n`n" +
          ($echecs -join "`n") + "`n`nDossier : $dossier") 'OK' 'Warning'
} elseif ($garder) {
    Dire "OpenForesia est désinstallé. Vos sauvegardes sont conservées dans :`n$sauvegardes"
} else {
    Dire 'OpenForesia est désinstallé.'
}
