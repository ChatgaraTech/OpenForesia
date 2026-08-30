@echo off
REM ===========================================================================================
REM  objets.bat - regenere Documentation\Etat\ObjetsAtteignables.md.
REM
REM  Le pendant de feuille-de-route.bat, sur les OBJETS au lieu des zones. A lancer apres une
REM  seance de jeu, ou apres avoir transcrit un gestionnaire.
REM
REM  Il croise quatre sources, dont aucune n'est ecrite a la main :
REM    - les scripts des 2370 fiches d'objet du CD (Bestiaire\objets.csv, colonne octets),
REM      deroulus hors ligne : c'est ce qui donne la liste de courses SANS jouer ;
REM    - les tables de sauts de Blaze.exe, pour resoudre un sous-type en adresse ;
REM    - les `case` du repartiteur dans les sources C#, pour la colonne implemente ;
REM    - les deux sondes, handlers-exerces.tsv et objets-exerces.tsv, pour la colonne teste.
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
    echo     sh OpenForesia.Tools/census-item-effects.sh ^> Documentation/Etat/ObjetsAtteignables.md
    echo.
    pause
    exit /b 1
)

echo == bash : %BASH%
echo == regeneration de Documentation\Etat\ObjetsAtteignables.md
echo.

"%BASH%" -c "sh OpenForesia.Tools/census-item-effects.sh > Documentation/Etat/ObjetsAtteignables.md"

if errorlevel 1 (
    echo.
    echo La regeneration a echoue - le fichier n'a pas ete remplace.
    pause
    exit /b 1
)

REM --- le compte, qui est ce qu'on vient chercher --------------------------------------------
echo ================================== le compte ====================================
"%BASH%" -c "sed -n '/^## Le compte/,/^\*\*La derniere ligne/p' Documentation/Etat/ObjetsAtteignables.md | grep '^|'"
echo.
"%BASH%" -c "grep 'objets distincts' Documentation/Etat/ObjetsAtteignables.md"
echo.

REM --- Les extractions visent le CHAMP, pas la ligne. Un grep sur '**non**' attrapait aussi la
REM     colonne teste, et faisait figurer dans la liste a ecrire un gestionnaire deja ecrit.
REM     Les lignes de gestionnaire ont huit champs (implemente en 5, teste en 6), celles d'objet
REM     en ont six (implemente en 4, teste en 5) : le nombre de champs suffit a les distinguer.
REM
REM     AUCUN '!' dans les commandes qui suivent. Avec enabledelayedexpansion, cmd.exe le mange
REM     avant que bash ne le voie : le '!~' d'awk disparaissait et le compte sortait a zero.

REM --- la liste de courses : les gestionnaires non ecrits, le plus employe d'abord ------------
echo ===================== gestionnaires a ecrire (le plus employe en tete) ==========
"%BASH%" -c "awk -F'|' 'NF==8 && $2 ~ /0x/ && $5 ~ /non/' Documentation/Etat/ObjetsAtteignables.md | head -8"
echo.

REM --- la priorite : ce que le jeu a REELLEMENT atteint et qui n'est pas ecrit ----------------
echo ======================= a ecrire en priorite (vu en jeu) ========================
"%BASH%" -c "awk -F'|' '(NF==8 && $2 ~ /0x/ && $5 ~ /non/ && $6 ~ /oui/) || (NF==6 && $4 ~ /non/ && $5 ~ /oui/)' Documentation/Etat/ObjetsAtteignables.md" > "%TEMP%\ofo.txt"
for /f %%c in ('type "%TEMP%\ofo.txt" ^| find /c /v ""') do set "PRIO=%%c"
echo %PRIO% ligne(s) que le jeu a atteint reellement et qui ne sont pas ecrites :
type "%TEMP%\ofo.txt"
del "%TEMP%\ofo.txt" 2>nul
echo.

REM --- ce qui est ecrit mais jamais employe : la dette, cote objets ---------------------------
echo ============== objets implementes que personne n'a encore employes ==============
"%BASH%" -c "awk -F'|' 'NF==6 && $4 ~ /oui/ && $5 ~ /non/' Documentation/Etat/ObjetsAtteignables.md | wc -l" > "%TEMP%\ofo.txt"
set /p DETTE=<"%TEMP%\ofo.txt"
del "%TEMP%\ofo.txt" 2>nul
echo %DETTE% objet(s) - un corps qui compile n'a rien prouve ; s'en servir en jeu, si.
echo.

REM --- le controle croise : la sonde connait-elle des objets que le catalogue ignore ? --------
REM     C'est ainsi qu'on a decouvert que objets.csv ne porte PAS l'equipement de depart : il
REM     donne les catalogues par sous-zone. Une ligne ici veut dire que le releve statique a un
REM     angle mort, et que seule la sonde le voit.
echo ============ vus en jeu, absents du catalogue (angle mort du releve) ============
"%BASH%" -c "sed -n '/^### Vus en jeu, absents du catalogue/,/^---$/p' Documentation/Etat/ObjetsAtteignables.md | grep '^| .' | grep -v 'nom probable' | grep -v '^|---'"
echo =================================================================================
echo.
pause
