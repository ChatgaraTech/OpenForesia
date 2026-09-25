@echo off
REM ===========================================================================================
REM  tournee-auto.bat - la tournee automatique du banc : exercer les gestionnaires d entite
REM  non testes, sous-zone par sous-zone, sans jouer.
REM
REM    tournee-auto.bat            40 secondes par sous-zone
REM    tournee-auto.bat 60         60 secondes par sous-zone
REM    tournee-auto.bat neuf       oublier l avancement et repartir du debut
REM
REM  Ce qu il faut faire : charger UNE FOIS un groupe solide (un personnage de test a 9000 PV,
REM  ou le groupe du mode debug), puis lacher le clavier. Des que le groupe est sur la carte, le
REM  jeu se teleporte de lui-meme dans chaque sous-zone de la liste, y fait naitre toutes les
REM  betes que ses especes portent vraiment, attend, puis passe a la suivante.
REM
REM  La liste vient de Documentation\Etat\GestionnairesEntites.md (OpenForesia.Tools\
REM  tournee-auto.sh) : seules les sous-zones qui portent encore un gestionnaire non teste.
REM  L avancement est garde dans tournee-faite.txt a cote du jeu : on peut fermer le jeu a tout
REM  moment, la tournee reprendra ou elle en etait. Une sous-zone qui fait tomber le jeu est notee
REM  "plante" et sautee au lancement suivant (la pile reste dans tournee.log ; "neuf" la retente).
REM
REM  Le filet (OPENFORESIA_AI_STUB=1) est pose : un gestionnaire manquant ne tue pas le jeu.
REM  A la fin : fermer le jeu, puis tests\update-GestionnaireEntite.bat.
REM
REM  Ce fichier est en ASCII pur : cmd.exe ne lit pas l UTF-8 par defaut.
REM ===========================================================================================

setlocal

cd /d "%~dp0.."
set "GAME=%CD%\OpenForesia\bin\Debug\net10.0"

if not exist "%GAME%\OpenForesia.exe" (
    echo Le jeu n est pas compile : %GAME%\OpenForesia.exe est introuvable.
    pause
    exit /b 1
)

set "DWELL=40"
if /i "%~1"=="neuf" (
    del /q "%GAME%\tournee-faite.txt" 2>nul
    echo == avancement efface, la tournee repart du debut
) else (
    if not "%~1"=="" set "DWELL=%~1"
)

REM --- trouver bash : le PATH d abord, puis les emplacements habituels de Git pour Windows ----
set "BASH="

for /f "delims=" %%i in ('where bash.exe 2^>nul') do (
    if not defined BASH set "BASH=%%i"
)

if not defined BASH (
    for %%p in (
        "D:\Logiciels\Git\bin\bash.exe"
        "%ProgramFiles%\Git\bin\bash.exe"
        "%ProgramFiles(x86)%\Git\bin\bash.exe"
        "%LOCALAPPDATA%\Programs\Git\bin\bash.exe"
        "D:\Logiciels\Git\usr\bin\bash.exe"
        "%ProgramFiles%\Git\usr\bin\bash.exe"
    ) do (
        if not defined BASH if exist %%p set "BASH=%%~p"
    )
)

if not defined BASH (
    echo bash.exe est introuvable - il vient avec Git pour Windows.
    pause
    exit /b 1
)

REM --- la liste des etapes -------------------------------------------------------------------
"%BASH%" -c "sh OpenForesia.Tools/tournee-auto.sh" > "%TEMP%\ofr-tournee.txt"
set "TOUR="
set /p TOUR=<"%TEMP%\ofr-tournee.txt"
del "%TEMP%\ofr-tournee.txt" 2>nul

if not defined TOUR (
    echo Aucune sous-zone ne porte de gestionnaire non teste : rien a visiter.
    pause
    exit /b 0
)

echo == etapes : %TOUR%
echo == %DWELL% secondes par sous-zone
if exist "%GAME%\tournee-faite.txt" echo == deja faites : voir %GAME%\tournee-faite.txt
echo.
echo Charger un groupe solide, puis ne plus toucher a rien.
echo Le journal va dans %GAME%\tournee.log
echo.

set "OPENFORESIA_AI_BENCH_TOUR=%TOUR%"
set "OPENFORESIA_AI_BENCH_DWELL=%DWELL%"
REM  Pas de greffe : un comportement pose sur le modele d une autre bete demande des gestes que
REM  ce modele n a pas, et le dessin tombe (DrawAnimatedModel, auberge, 2026-09-25). Comme la
REM  tournee passe par TOUTES les sous-zones, les vraies especes suffisent.
set "OPENFORESIA_AI_BENCH_ORPHANS=0"
set "OPENFORESIA_AI_STUB=1"

cd /d "%GAME%"
echo ===== %DATE% %TIME% ===== >> tournee.log
OpenForesia.exe 2>> tournee.log

echo.
echo ============================== ce que la tournee a vu ==============================
findstr /l /c:"Unhandled exception" /c:"[BANC] tournee" /c:"[BANC] TOURNEE" /c:"[BANC] LEVEE" /c:"[LACUNE]" tournee.log
echo ====================================================================================
echo.
echo Etape suivante : tests\update-GestionnaireEntite.bat
pause
