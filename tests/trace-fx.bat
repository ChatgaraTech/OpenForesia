@echo off
REM ===========================================================================================
REM  trace-fx.bat - lance OpenForesia avec le RELEVE DE COULEUR DES EFFETS.
REM
REM  A quoi ca sert : l'onde de choc du Guerrier (attaquer + une direction) sort noire alors
REM  qu'elle doit etre blanche. Ce releve dit ou le blanc se perd.
REM
REM  Mode d'emploi : double-clic, charge une partie, va au contact d'un ennemi et lance
REM  l'attaque en course une fois ou deux. Ferme le jeu. Les lignes [FX] sont reaffichees.
REM
REM  Troisieme version (2026-08-30) : la couleur est desormais innocentee des deux cotes
REM  (teinte 0x787878 emise, diffuse 0xFFF0F0F0 recue). Ce tour-ci vide la TEXTURE en PNG
REM  dans scratchpad\, et capte les VINGT dessins teintes de la salve au lieu de quatre.
REM
REM  Ce qu'il faut lire :
REM    32x32 texel(1,31)=0xFFEEEEEE transparents=176 opaques<0x10=..  >=0xc0=..
REM        -> la clarte reelle des texels, par seuils et non par egalite a zero
REM    texture 0x........ ecrite dans ...\scratchpad\texture-........-32x32.png
REM        -> l'image elle-meme, a ouvrir : elle tranche ce que les nombres laissent ouvert
REM    salve precedente : N dessins, dont 20 a diffuse non blanche
REM        -> combien de dessins teintes l'image comptait
REM
REM  Ce fichier est en ASCII pur : cmd.exe ne lit pas l'UTF-8 par defaut.
REM ===========================================================================================

setlocal

cd /d "%~dp0..\OpenForesia"

if not exist "bin\Debug\net9.0\OpenForesia.exe" (
    echo Le jeu n'est pas construit.
    echo   dotnet build OpenForesia.sln
    pause
    exit /b 1
)

if not exist "..\scratchpad" mkdir "..\scratchpad"

for /f %%i in ('powershell -NoProfile -Command "Get-Date -Format yyMMdd-HHmmss"') do set "STAMP=%%i"
set "LOG=..\scratchpad\trace-fx-%STAMP%.log"

set "OPENFORESIA_TRACE_FX=1"

echo == releve de couleur des effets
echo == journal : %LOG%
echo.
echo Charge une partie, approche un ennemi, et lance l'attaque en course
echo (attaquer + une direction) une fois ou deux. Puis ferme le jeu.
echo.

"bin\Debug\net9.0\OpenForesia.exe" 2> "%LOG%"

echo.
echo ======================== ce que le releve a vu ========================
findstr /C:"[FX]" /C:"32x32" /C:"uv :" "%LOG%" 2>nul
if errorlevel 1 echo (aucune ligne : l'onde n'a pas ete dessinee - voir %LOG%)
echo ======================================================================
echo.
pause
