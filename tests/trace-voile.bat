@echo off
REM ===========================================================================================
REM  trace-voile.bat - lance le jeu en notant qui blanchit (ou teinte) l ecran.
REM
REM  A chaque changement d un des deux voiles plein cadre ou de la couleur d ambiance, une ligne
REM  [VOILE] dit quelle sous-zone, quel gestionnaire (ou "hors gestionnaire" : script, scene), et
REM  les valeurs avant / apres. Voir Documentation\Reference\legacy-bug.md LB-052.
REM
REM  Jouer jusqu au moment qui blanchit l ecran, puis fermer le jeu : les lignes s affichent.
REM  Le journal complet est dans OpenForesia\bin\Debug\net10.0\trace-voile.log.
REM
REM  Ce fichier est en ASCII pur : cmd.exe ne lit pas l UTF-8 par defaut.
REM ===========================================================================================

setlocal

cd /d "%~dp0..\OpenForesia\bin\Debug\net10.0"
if not exist OpenForesia.exe (
    echo Le jeu n est pas compile.
    pause
    exit /b 1
)

set "OPENFORESIA_TRACE_VOILE=1"
OpenForesia.exe 2> trace-voile.log

echo.
echo ================================ lignes [VOILE] ================================
findstr /l /c:"[VOILE]" trace-voile.log
echo =================================================================================
pause
