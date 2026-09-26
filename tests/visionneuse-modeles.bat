@echo off
REM ===========================================================================================
REM  visionneuse-modeles.bat - ouvre l etat de jeu 8, la visionneuse de modeles 3D.
REM
REM  Un outil d atelier reste dans Blaze.exe, qu aucune instruction du binaire n atteint
REM  (Documentation\Etat\NOTE_DEVELOPPEUR.md, entree 8). OPENFORESIA_FORCE_STATE=8 y entre
REM  directement au lieu du menu principal.
REM
REM  Commandes (manette du chef) :
REM    CARRE tenu + gauche/droite  choisir le jeu de modeles (charge au relachement de CARRE)
REM    croix directionnelle        tourner la camera
REM    L1 / R1                     deplacer la camera en X
REM    TRIANGLE / CROIX            deplacer la camera en Z
REM    L2 / R2                     animation precedente / suivante
REM    SELECT                      bascule lecture continue / image par image
REM    ROND                        image suivante (en image par image)
REM    START                       camera remise a sa pose de depart
REM    SELECT + START              quitter vers le menu principal
REM
REM  Le journal est dans OpenForesia\bin\Debug\net10.0\visionneuse-modeles.log.
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

set "OPENFORESIA_FORCE_STATE=8"
OpenForesia.exe 2> visionneuse-modeles.log

echo.
echo ============================== fin du journal ==============================
powershell -NoProfile -Command "Get-Content visionneuse-modeles.log -Tail 25"
echo ============================================================================
pause
