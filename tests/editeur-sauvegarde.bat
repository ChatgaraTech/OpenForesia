@echo off
REM ===========================================================================================
REM  editeur-sauvegarde.bat - ouvre l editeur de sauvegarde dans le navigateur par defaut.
REM
REM  La page est OpenForesia.Tools\Html\editeur-sauvegarde.html, un fichier autonome : aucun
REM  serveur, rien a installer. Pour reecrire les sauvegardes en place, utiliser Chrome ou Edge
REM  et le bouton "Ouvrir le dossier save", puis choisir OpenForesia\save.
REM
REM  FERMER LE JEU avant d enregistrer : il reecrit la sauvegarde avec ce qu il a en memoire.
REM
REM  Si le catalogue d objets est vide, regenerer la page :
REM    powershell -ExecutionPolicy Bypass -File OpenForesia.Tools\Html\build-editeur-sauvegarde.ps1
REM  Voir OpenForesia.Tools\Html\LISEZMOI.md.
REM ===========================================================================================

set "PAGE=%~dp0..\OpenForesia.Tools\Html\editeur-sauvegarde.html"
if not exist "%PAGE%" (
    echo Page introuvable : %PAGE%
    echo Lancer d abord OpenForesia.Tools\Html\build-editeur-sauvegarde.ps1
    pause
    exit /b 1
)
start "" "%PAGE%"
