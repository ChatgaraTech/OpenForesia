; 0x004103d0 — l'amorce de la fumee violette (6 instructions, absente de Blaze.c).
; Pur relais vers FUN_004103e0, qui est decompilee (335 instructions, trois roles).
; Copie : 0x00413840 -> FUN_00413850, planche 0x0054f920.

C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004103d0  section .text  offset fichier 0xf7d0  16 octets

  004103d0  8b442404              mov       eax,[esp+4]
  004103d4  50                    push      eax
  004103d5  e806000000            call      004103E0h
  004103da  83c404                add       esp,4
  004103dd  c3                    ret
  004103de  90                    nop
  004103df  90                    nop
