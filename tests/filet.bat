@echo off
REM ===========================================================================================
REM  filet.bat - lance OpenForesia avec LE FILET (OPENFORESIA_AI_STUB=1).
REM
REM  Le jeu normal, rien de force : aucun monstre greffe, aucun transfert. La seule difference
REM  est qu'un gestionnaire d'entite non transcrit NE TUE PLUS LE PROCESSUS - il se journalise
REM  une fois en [LACUNE] et rend la main. La creature reste immobile, la partie continue.
REM
REM  C'est la forme a utiliser pour "je joue normalement, ca plante quand je frappe, je veux
REM  savoir QUI manque". Joue, frappe, va ou tu veux, puis FERME LA FENETRE NORMALEMENT :
REM  le recapitulatif trie par nombre d'appels ne s'imprime qu'a la sortie propre du processus.
REM
REM  Le journal part dans scratchpad\filet-<horodatage>.log, et les lignes [LACUNE] sont
REM  reaffichees a la fermeture.
REM
REM  Pour exercer des comportements sans jouer jusqu'a eux, c'est banc.bat qu'il faut.
REM  Ce fichier est en ASCII pur : cmd.exe ne lit pas l'UTF-8 par defaut.
REM ===========================================================================================

setlocal

cd /d "%~dp0..\OpenForesia"

if not exist "bin\Debug\net9.0\OpenForesia.exe" (
    echo Le jeu n'est pas construit.
    echo   dotnet build OpenForesia.sln
    pause
    exit /b 1
)

if not exist "save\*" (
    echo Aucune sauvegarde dans OpenForesia\save\ : il n'y aura rien a charger.
    echo.
)

if not exist "..\scratchpad" mkdir "..\scratchpad"

for /f %%i in ('powershell -NoProfile -Command "Get-Date -Format yyMMdd-HHmmss"') do set "STAMP=%%i"
set "LOG=..\scratchpad\filet-%STAMP%.log"

set "OPENFORESIA_AI_STUB=1"

echo == filet : AI_STUB=1 - une lacune se journalise au lieu de tuer le jeu
echo == journal : %LOG%
echo.
echo Ferme la fenetre normalement : le recapitulatif s'imprime a la sortie propre.
echo.

"bin\Debug\net9.0\OpenForesia.exe" 2> "%LOG%"

echo.
echo ===================== les lacunes rencontrees =====================
findstr /C:"[LACUNE]" "%LOG%" 2>nul
if errorlevel 1 echo (aucune ligne : aucun gestionnaire manquant n'a ete rencontre)
echo ==================================================================
echo.
pause
