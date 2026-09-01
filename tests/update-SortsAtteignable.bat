@echo off
REM ===========================================================================================
REM  sorts.bat - regenere Documentation\Etat\SortsAtteignables.md.
REM
REM  Le troisieme de la famille, apres feuille-de-route.bat (les zones) et objets.bat (les
REM  objets). Celui-ci couvre la MAGIE. A lancer apres une seance de jeu, ou apres avoir
REM  transcrit un gestionnaire de sort.
REM
REM  Il croise cinq sources, dont aucune n'est ecrite a la main :
REM    - les 123 fiches de sort du CD (Bestiaire\sorts.csv), lues hors ligne par le verbe
REM      bestiaire : c'est ce qui donne la liste de courses SANS jouer ;
REM    - la table de sauts statique 0x00551c30 de Blaze.exe, pour resoudre un sous-type ;
REM    - DAT_00564600, DAT_0056d330 et DAT_00562ab8, pour les ecoles et leurs paliers ;
REM    - les `case` du repartiteur dans les sources C#, pour la colonne implemente ;
REM    - la sonde sorts-exerces.tsv, pour la colonne teste.
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
    echo     sh OpenForesia.Tools/census-spells.sh ^> Documentation/Etat/SortsAtteignables.md
    echo.
    pause
    exit /b 1
)

echo == bash : %BASH%
echo == regeneration de Documentation\Etat\SortsAtteignables.md
echo.

"%BASH%" -c "sh OpenForesia.Tools/census-spells.sh > Documentation/Etat/SortsAtteignables.md"

if errorlevel 1 (
    echo.
    echo La regeneration a echoue - le fichier n'a pas ete remplace.
    pause
    exit /b 1
)

REM --- le compte, qui est ce qu'on vient chercher --------------------------------------------
echo ================================== le compte ====================================
"%BASH%" -c "sed -n '/^## Le compte/,/^\*\*La derniere ligne/p' Documentation/Etat/SortsAtteignables.md | grep '^|'"
echo.
"%BASH%" -c "grep 'gestionnaires distincts' Documentation/Etat/SortsAtteignables.md"
echo.

REM --- Les extractions visent le CHAMP, pas la ligne. Un grep sur '**non**' attraperait aussi
REM     la colonne teste. Le nombre de champs distingue les trois tableaux du document :
REM       13 champs -> un sort d'ecole   (implemente en 11, teste en 12)
REM        7 champs -> un gestionnaire   (implemente en  4, teste en  5)
REM        9 champs -> une famille hors ecole
REM
REM     AUCUN '!' dans les commandes qui suivent. Avec enabledelayedexpansion, cmd.exe le mange
REM     avant que bash ne le voie : le '!~' d'awk disparaissait et le compte sortait a zero.

REM --- la liste de courses : les corps non ecrits, le plus employe d'abord --------------------
echo ====================== gestionnaires a ecrire (le plus employe en tete) =========
"%BASH%" -c "awk -F'|' 'NF==7 && $2 ~ /0x/ && $4 ~ /non/' Documentation/Etat/SortsAtteignables.md | head -10"
echo.

REM --- la priorite : ce que le jeu a REELLEMENT lance et qui n'est pas ecrit ------------------
echo ======================== a ecrire en priorite (lance en jeu) ====================
"%BASH%" -c "awk -F'|' 'NF==13 && $11 ~ /non/ && $12 ~ /oui/' Documentation/Etat/SortsAtteignables.md" > "%TEMP%\ofs.txt"
for /f %%c in ('type "%TEMP%\ofs.txt" ^| find /c /v ""') do set "PRIO=%%c"
echo %PRIO% sort(s) que le jeu a lance reellement et dont le corps manque :
type "%TEMP%\ofs.txt"
del "%TEMP%\ofs.txt" 2>nul
echo.

REM --- ce qui est ecrit mais jamais lance : la dette ------------------------------------------
echo ================= sorts implementes que personne n'a encore lances ==============
"%BASH%" -c "awk -F'|' 'NF==13 && $11 ~ /oui/ && $12 ~ /non/' Documentation/Etat/SortsAtteignables.md | wc -l" > "%TEMP%\ofs.txt"
set /p DETTE=<"%TEMP%\ofs.txt"
del "%TEMP%\ofs.txt" 2>nul
echo %DETTE% sort(s) - un corps qui compile n'a rien prouve ; le lancer en jeu, si.
echo =================================================================================
echo.
pause
