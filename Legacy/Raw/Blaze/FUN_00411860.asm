; 0x00411860 — l'amorce de la gerbe aux cinq phases (15 instructions, absente de Blaze.c).
; Elle pose +0x98=0x39, +0x96=6, la planche 0x0054f918 si la phase vaut 2 et l'etat 0,
; puis saute dans FUN_004118a0, qui est decompilee (561 instructions).
; Copie : 0x00414cd0, planche 0x0054fa10 et corps FUN_00414d10.

C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00411860  section .text  offset fichier 0x10c60  56 octets

  00411860  8b442404              mov       eax,[esp+4]
  00411864  6683781402            cmp       word [eax+14h],2
  00411869  66c780980000003900    mov       word [eax+98h],39h
  00411872  66c780960000000600    mov       word [eax+96h],6
  0041187b  750e                  jne       short 0041188Bh
  0041187d  6683781000            cmp       word [eax+10h],0
  00411882  7507                  jne       short 0041188Bh
  00411884  c7403c18f95400        mov       dword [eax+3Ch],54F918h

LAB_0041188b:
  0041188b  50                    push      eax
  0041188c  e80f000000            call      004118A0h
  00411891  83c404                add       esp,4
  00411894  c3                    ret
  00411895  90                    nop
  00411896  90                    nop
  00411897  90                    nop

1 cible(s) de saut interne : 0x0041188b
