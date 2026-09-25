@echo off
REM ===========================================================================================
REM  lot-suivant.bat - met le lot suivant de personnages de test dans save\ et ouvre sa fiche.
REM
REM  Les lots sont fabriques par :
REM    node OpenForesia.Tools\generer-personnages-test.mjs
REM  dans OpenForesia\bin\Debug\net10.0\save-tests\lot-01, lot-02... (24 personnages au plus :
REM  l ecran de constitution du groupe n en liste que 32).
REM
REM  Ce raccourci retire de save\ les personnages de test du lot precedent (Blaze-SavingTST*)
REM  SANS toucher aux autres sauvegardes, copie le lot suivant, et ouvre FICHE.txt.
REM  Le numero du lot en cours est garde dans save-tests\lot-courant.txt.
REM
REM  FERMER LE JEU avant de lancer ce raccourci.
REM ===========================================================================================

set "GAME=%~dp0..\OpenForesia\bin\Debug\net10.0"
set "TESTS=%GAME%\save-tests"
if not exist "%TESTS%\lot-01" (
    echo Aucun lot : lancer d abord  node OpenForesia.Tools\generer-personnages-test.mjs
    pause
    exit /b 1
)

set CUR=0
if exist "%TESTS%\lot-courant.txt" set /p CUR=<"%TESTS%\lot-courant.txt"
set /a NEXT=CUR+1
set "PAD=%NEXT%"
if %NEXT% LSS 10 set "PAD=0%NEXT%"

if not exist "%TESTS%\lot-%PAD%" (
    echo Plus de lot apres le lot %CUR%. Pour repartir de ce qui reste :
    echo   1. tests\update-ObjetsAtteignable.bat
    echo   2. node OpenForesia.Tools\generer-personnages-test.mjs
    del /q "%GAME%\save\Blaze-SavingTST*" 2>nul
    pause
    exit /b 0
)

del /q "%GAME%\save\Blaze-SavingTST*" 2>nul
copy /y "%TESTS%\lot-%PAD%\Blaze-Saving*" "%GAME%\save\" >nul
>"%TESTS%\lot-courant.txt" echo %NEXT%
echo Lot %PAD% en place dans save\.
start "" notepad "%TESTS%\lot-%PAD%\FICHE.txt"
