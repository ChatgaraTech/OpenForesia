@echo off
REM ===========================================================================================
REM  guide-essais.bat - refait le guide des essais et l ouvre dans le navigateur.
REM
REM  Le guide (OpenForesia.Tools\Html\guide-gestionnaires.html) dit, lieu par lieu, ce qui n a
REM  encore jamais tourne en jeu, ou aller (QUEST / MAP du menu de debogage) et quoi y faire.
REM  Il relit Documentation\Etat\handlers-exerces.tsv a chaque lancement : jouer, fermer le jeu,
REM  relancer ce raccourci, et ce qui a tourne disparait de la liste.
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

"%NODE%" OpenForesia.Tools\Html\build-guide-gestionnaires.mjs
if errorlevel 1 (
    echo.
    echo La fabrication du guide a echoue.
    pause
    exit /b 1
)

"%NODE%" OpenForesia.Tools\Html\check-guide-gestionnaires.mjs
if errorlevel 1 (
    echo.
    echo Le controle du guide a echoue - la page est ouverte quand meme.
    pause
)

start "" "%CD%\OpenForesia.Tools\Html\guide-gestionnaires.html"
