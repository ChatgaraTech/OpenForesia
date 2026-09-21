@echo off
REM ===========================================================================================
REM  update-DLLAtteignables.bat - regenere Documentation\Etat\DLLAtteignables.md.
REM
REM  Le quatrieme de la famille, apres update-GestionnaireEntite.bat (les zones),
REM  update-ObjetsAtteignable.bat (les objets) et update-SortsAtteignable.bat (la magie).
REM  Celui-ci couvre LES CINQ DLL D'AIDE DE 1998 : ce que la reconstruction en appelle
REM  vraiment, et donc ce qu'il faudrait transcrire pour s'en passer.
REM
REM  A lancer apres avoir transcrit un corps de DLL, ou apres avoir ajoute ou retire un
REM  [DllImport] dans OpenForesia\src\Blaze\Interop\.
REM
REM  Il croise quatre sources, dont aucune n'est ecrite a la main :
REM    - les tables d'exports lues DANS les binaires de Legacy\Binaries (ordinal -> RVA -> nom),
REM      ce qui resout les ordinaux que le commentaire de corps de Ghidra ne porte pas ;
REM    - les cinq decompilations de Legacy\Decompiled, pour les corps et leurs appels ;
REM    - les [DllImport] de Interop\, qui sont les racines de la cloture ;
REM    - les commentaires de tracabilite des sources C#, pour la colonne implemente.
REM
REM  Aucune partie n'est jouee : tout est hors ligne, tout est relu a chaque passage.
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
    echo     sh OpenForesia.Tools/census-dll-surface.sh ^> Documentation/Etat/DLLAtteignables.md
    echo.
    pause
    exit /b 1
)

echo == bash : %BASH%
echo == regeneration de Documentation\Etat\DLLAtteignables.md
echo.

"%BASH%" -c "sh OpenForesia.Tools/census-dll-surface.sh > Documentation/Etat/DLLAtteignables.md"

if errorlevel 1 (
    echo.
    echo La regeneration a echoue - le fichier n'a pas ete remplace.
    echo   Une base de chargement incoherente arrete le script volontairement : mieux vaut
    echo   ne rien ecrire qu'ecrire un recensement a zero.
    pause
    exit /b 1
)

REM --- le releve, qui est ce qu'on vient chercher ---------------------------------------------
echo ================================== le releve ====================================
"%BASH%" -c "sed -n '/^| DLL |/,/^$/p' Documentation/Etat/DLLAtteignables.md"
echo.

REM --- Les extractions visent le CHAMP, pas la ligne : un grep sur 'non' attraperait le texte
REM     des paragraphes. Le nombre de champs distingue les tableaux du document :
REM        8 champs -> un point d'entree  (| ordinal | nom | adresse | fonction | lignes | impl |)
REM        7 champs -> une fonction       (| adresse | fonction | lignes | appelants | impl |)
REM
REM     AUCUN '!' dans les commandes qui suivent. Avec enabledelayedexpansion, cmd.exe le mange
REM     avant que bash ne le voie.

REM --- la liste de courses de wsyskern : le premier lot ---------------------------------------
echo ================ wsyskern : les points d'entree, le premier lot =================
"%BASH%" -c "sed -n '/^## .wsyskern./,/^## .winsys3d./p' Documentation/Etat/DLLAtteignables.md | awk -F'|' 'NF==8 && $4 ~ /0x/'"
echo.

REM --- les plus gros corps restants, toutes DLL confondues ------------------------------------
echo ============== les dix plus gros corps a transcrire (tout DLL) ==================
"%BASH%" -c "awk -F'|' 'NF==7 && $2 ~ /0x/ && $6 ~ /non/ { gsub(/ /, \"\", $4); printf \"%%6d %%s %%s\\n\", $4, $2, $3 }' Documentation/Etat/DLLAtteignables.md | sort -rn | head -10"
echo.

REM --- la dette : ce qui reste, corps par corps -----------------------------------------------
echo ========================== ce qui reste a transcrire ============================
"%BASH%" -c "awk -F'|' 'NF==7 && $2 ~ /0x/ && $6 ~ /non/' Documentation/Etat/DLLAtteignables.md | wc -l" > "%TEMP%\ofd.txt"
set /p RESTE=<"%TEMP%\ofd.txt"
del "%TEMP%\ofd.txt" 2>nul
"%BASH%" -c "awk -F'|' 'NF==7 && $2 ~ /0x/ && $6 ~ /oui/' Documentation/Etat/DLLAtteignables.md | wc -l" > "%TEMP%\ofd.txt"
set /p FAIT=<"%TEMP%\ofd.txt"
del "%TEMP%\ofd.txt" 2>nul
echo %FAIT% corps ecrit(s), %RESTE% restant(s).
echo.
echo   La CRT liee statiquement n'est pas comptee la : elle se rebranche sur Platform\Crt.cs
echo   au lieu de se transcrire, et le document la range a part.
echo =================================================================================
echo.
pause
