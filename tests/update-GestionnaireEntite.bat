@echo off
REM ===========================================================================================
REM  feuille-de-route.bat - regenere Documentation\Etat\GestionnairesEntites.md.
REM
REM  A lancer apres une seance de jeu. Le jeu, lui, n'ecrit que handlers-exerces.tsv (la preuve
REM  brute) ; ce fichier-ci croise trois sources - les tables de Blaze.exe, les `case` du
REM  repartiteur dans les sources C#, et ce releve - et reconstruit la feuille de route.
REM
REM  Il change donc pour deux raisons : parce que tu as joue, ou parce qu'un gestionnaire a ete
REM  implemente. C'est pour ca qu'il n'est pas ecrit par le jeu.
REM
REM  Ce fichier est en ASCII pur : cmd.exe ne lit pas l'UTF-8 par defaut.
REM ===========================================================================================

setlocal enabledelayedexpansion

cd /d "%~dp0.."

REM --- trouver bash : le PATH d'abord, puis les emplacements habituels de Git pour Windows ----
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
    echo bash.exe est introuvable.
    echo.
    echo   Il vient avec Git pour Windows. Si Git est installe ailleurs, ajoute son chemin
    echo   dans la liste en haut de ce fichier, ou lance la commande a la main :
    echo.
    echo     sh OpenForesia.Tools/census-entity-handlers.sh ^> Documentation/Etat/GestionnairesEntites.md
    echo.
    pause
    exit /b 1
)

echo == bash : %BASH%
echo == regeneration de Documentation\Etat\GestionnairesEntites.md
echo.

"%BASH%" -c "sh OpenForesia.Tools/census-entity-handlers.sh > Documentation/Etat/GestionnairesEntites.md"

if errorlevel 1 (
    echo.
    echo La regeneration a echoue - le fichier n'a pas ete remplace.
    pause
    exit /b 1
)

REM --- reafficher l'avancement, qui est ce qu'on vient chercher ------------------------------
echo ============================== avancement par zone ==============================
"%BASH%" -c "sed -n '/^## L.avancement par zone/,/^Un gestionnaire employe/p' Documentation/Etat/GestionnairesEntites.md | grep '^|'"
echo.
echo ======================= a ecrire en priorite (vu en jeu) ========================
"%BASH%" -c "grep -c 'non\*\* | oui |' Documentation/Etat/GestionnairesEntites.md" > "%TEMP%\ofr.txt"
set /p PRIO=<"%TEMP%\ofr.txt"
del "%TEMP%\ofr.txt" 2>nul
echo %PRIO% gestionnaire(s) que le jeu atteint reellement et qui ne sont pas ecrits :
"%BASH%" -c "grep 'non\*\* | oui |' Documentation/Etat/GestionnairesEntites.md | sort -u"
echo =================================================================================
echo.
pause
