@echo off
REM ===========================================================================================
REM  trace-modeles.bat - lance le jeu en notant ce que la grille d entites dessine, et qui l a pose.
REM
REM  Une ligne [MODELE] a la premiere apparition de chaque combinaison : gestionnaire, chemin de
REM  dessin (modele, particule), numero de modele, decalage de texture, planche, teinte, echelles.
REM  Sert a nommer l entite qui dessine le rectangle arc-en-ciel a la mort des boss de la foret.
REM
REM  Aller pres du boss, le tuer, attendre la fin de l explosion, puis fermer le jeu.
REM  Le journal complet est dans OpenForesia\bin\Debug\net10.0\trace-modeles.log.
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

set "OPENFORESIA_TRACE_MODELES=1"
OpenForesia.exe 2> trace-modeles.log

echo.
echo ============================ lignes [MODELE] de la foret ============================
findstr /l /c:"[MODELE] zone 3:" trace-modeles.log
echo ======================================================================================
pause
