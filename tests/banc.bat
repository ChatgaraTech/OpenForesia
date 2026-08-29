@echo off
REM ===========================================================================================
REM  banc.bat - lance OpenForesia avec la SALLE D'ESSAI, le banc des comportements d'ennemi.
REM
REM  Double-clic  : peuple la sous-zone ou le groupe se trouve, et de nouveau a chaque
REM                 changement de lieu. C'est la forme a utiliser quand on conduit soi-meme.
REM  banc.bat 3:0 : se transfere d'abord en zone 3 sous-zone 0, puis peuple.
REM  banc.bat 1 0 : sans greffe (seulement les especes que le lieu porte vraiment).
REM
REM  Le journal part dans scratchpad\banc-<horodatage>.log, et les lignes [BANC] / [LACUNE]
REM  sont reaffichees a la fermeture du jeu.
REM
REM  Mode d'emploi complet : Documentation\Tests\test-protocol.md, section "La salle d'essai".
REM  Ce fichier est en ASCII pur : cmd.exe ne lit pas l'UTF-8 par defaut.
REM ===========================================================================================

setlocal

set "BENCH=%~1"
if "%BENCH%"=="" set "BENCH=1"

set "ORPHANS=%~2"
if "%ORPHANS%"=="" set "ORPHANS=1"

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
set "LOG=..\scratchpad\banc-%STAMP%.log"

set "OPENFORESIA_AI_BENCH=%BENCH%"
set "OPENFORESIA_AI_BENCH_ORPHANS=%ORPHANS%"

echo == banc : AI_BENCH=%BENCH%  ORPHANS=%ORPHANS%
echo == journal : %LOG%
echo.
echo Charge une partie, va ou tu veux, et reste une trentaine de secondes par salle.
echo Ce sont de VRAIS monstres : ils attaquent.
echo.

"bin\Debug\net9.0\OpenForesia.exe" 2> "%LOG%"

echo.
echo ===================== ce que le banc a releve =====================
findstr /C:"[BANC]" /C:"[LACUNE]" "%LOG%" 2>nul
if errorlevel 1 echo (aucune ligne : le banc ne s'est pas declenche - voir %LOG%)
echo ==================================================================
echo.
pause
