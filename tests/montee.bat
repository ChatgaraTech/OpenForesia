@echo off
REM ===========================================================================================
REM  montee.bat - ouvre le SIMULATEUR DE MONTEE DE NIVEAU.
REM
REM  Double-clic : une fenetre ou l'on compose un personnage (classe, repartition des points
REM                comme a la creation, niveau vise), puis on regarde ses sept caracteristiques
REM                monter niveau par niveau - courbes et tableau des ecarts.
REM
REM  L'outil est autonome : il ne lit ni Blaze.exe ni le CD. Il rejoue FUN_005391f0, la vraie
REM  montee de niveau du jeu, sur un bloc de personnage de 0x2000 octets.
REM
REM  Une graine (srand) fixe le tirage : la meme graine rend exactement la meme partie.
REM
REM  Ce que le simulateur calcule : Documentation\Reference\MonteeDeNiveau.md
REM  Ce fichier est en ASCII pur : cmd.exe ne lit pas l'UTF-8 par defaut.
REM ===========================================================================================

setlocal

cd /d "%~dp0..\OpenForesia.Tools"

set "EXE=bin\Debug\net9.0-windows\OpenForesia.Tools.exe"

if not exist "%EXE%" (
    echo L'outil n'est pas construit.
    echo   dotnet build OpenForesia.Tools
    echo.
    pause
    exit /b 1
)

REM Lance sans `start` : la console reste derriere la fenetre le temps de la session, et c'est
REM la que partirait une trace d'exception. Elle se ferme avec le simulateur.
"%EXE%" montee
