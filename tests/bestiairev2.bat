@echo off
REM ===========================================================================================
REM  bestiairev2.bat - refait le compendium des objets et l ouvre dans le navigateur.
REM
REM  Le compendium (OpenForesia.Tools\Html\bestiairev2.html) montre les 397 objets du jeu avec
REM  leur vignette, leur description, leurs bonus, leurs effets et ou les trouver. Il relit
REM  Documentation\Reference\Bestiaire\*.csv : regenerer ces CSV d abord si les donnees changent.
REM
REM  Ce fichier est en ASCII pur : cmd.exe ne lit pas l UTF-8 par defaut.
REM ===========================================================================================

setlocal

cd /d "%~dp0.."

set "NODE="
for /f "delims=" %%i in ('where node.exe 2^>nul') do (
    if not defined NODE set "NODE=%%i"
)
if not defined NODE if exist "D:\Logiciels\nodejs\node.exe" set "NODE=D:\Logiciels\nodejs\node.exe"
if not defined NODE if exist "%ProgramFiles%\nodejs\node.exe" set "NODE=%ProgramFiles%\nodejs\node.exe"

if not defined NODE (
    echo node.exe est introuvable. Ajoute son chemin en haut de ce fichier.
    pause
    exit /b 1
)

"%NODE%" OpenForesia.Tools\Html\build-bestiairev2.mjs
if errorlevel 1 (
    echo.
    echo La fabrication du compendium a echoue.
    pause
    exit /b 1
)

"%NODE%" OpenForesia.Tools\Html\check-bestiairev2.mjs
if errorlevel 1 (
    echo.
    echo Le controle du compendium a echoue - la page est ouverte quand meme.
    pause
)

start "" "%CD%\OpenForesia.Tools\Html\bestiairev2.html"
