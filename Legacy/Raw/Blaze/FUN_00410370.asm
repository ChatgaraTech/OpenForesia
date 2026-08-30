; 0x00410370 — l'amorce du flash de sortie de zone (19 instructions, absente de Blaze.c).
; Elle recopie la pose de DAT_005fa5e0+0 puis saute dans FUN_004183d0, qui est decompilee.
; Copie identique au dernier octet pres : 0x004137e0.

C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00410370  section .text  offset fichier 0xf770  84 octets

  00410370  8b442404              mov       eax,[esp+4]
  00410374  50                    push      eax
  00410375  c7405c3a000000        mov       dword [eax+5Ch],3Ah
  0041037c  8b0dfca55f00          mov       ecx,[5FA5FCh]
  00410382  89481c                mov       [eax+1Ch],ecx
  00410385  8b1500a65f00          mov       edx,[5FA600h]
  0041038b  895020                mov       [eax+20h],edx
  0041038e  8b0d04a65f00          mov       ecx,[5FA604h]
  00410394  894824                mov       [eax+24h],ecx
  00410397  668b1510a65f00        mov       dx,[5FA610h]
  0041039e  66895030              mov       [eax+30h],dx
  004103a2  668b0d12a65f00        mov       cx,[5FA612h]
  004103a9  66894832              mov       [eax+32h],cx
  004103ad  668b1514a65f00        mov       dx,[5FA614h]
  004103b4  66895034              mov       [eax+34h],dx
  004103b8  e813800000            call      004183D0h
  004103bd  83c404                add       esp,4
  004103c0  c3                    ret
  004103c1  90                    nop
  004103c2  90                    nop
  004103c3  90                    nop
