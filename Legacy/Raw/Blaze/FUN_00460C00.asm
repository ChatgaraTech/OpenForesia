C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460c00  section .text  offset fichier 0x60000  261 octets

  00460c00  56                    push      esi
  00460c01  8b742408              mov       esi,[esp+8]
  00460c05  57                    push      edi
  00460c06  668b4e10              mov       cx,[esi+10h]
  00460c0a  6685c9                test      cx,cx
  00460c0d  0f84ef000000          je        near 00460D02h
  00460c13  0fbf7e14              movsx     edi,word [esi+14h]
  00460c17  8bc7                  mov       eax,edi
  00460c19  99                    cdq
  00460c1a  83e207                and       edx,7
  00460c1d  03c2                  add       eax,edx
  00460c1f  c1f803                sar       eax,3
  00460c22  6683f901              cmp       cx,1
  00460c26  7537                  jne       short 00460C5Fh
  00460c28  0fbfc0                movsx     eax,ax
  00460c2b  ba01000000            mov       edx,1
  00460c30  8d0cc500000000        lea       ecx,[eax*8]
  00460c37  2bf9                  sub       edi,ecx
  00460c39  8bcf                  mov       ecx,edi
  00460c3b  d3e2                  shl       edx,cl
  00460c3d  8a88e0965e00          mov       cl,[eax+5E96E0h]
  00460c43  84d1                  test      cl,dl
  00460c45  7518                  jne       short 00460C5Fh
  00460c47  b201                  mov       dl,1
  00460c49  8bcf                  mov       ecx,edi
  00460c4b  d2e2                  shl       dl,cl
  00460c4d  66c746100200          mov       word [esi+10h],2
  00460c53  66c746120000          mov       word [esi+12h],0
  00460c59  0890e0965e00          or        [eax+5E96E0h],dl

LAB_00460c5f:
  00460c5f  66837e1002            cmp       word [esi+10h],2
  00460c64  bf03000000            mov       edi,3
  00460c69  755f                  jne       short 00460CCAh
  00460c6b  668b4612              mov       ax,[esi+12h]
  00460c6f  6683463220            add       word [esi+32h],20h
  00460c74  6685c0                test      ax,ax
  00460c77  7412                  je        short 00460C8Bh
  00460c79  663d0a00              cmp       ax,0Ah
  00460c7d  740c                  je        short 00460C8Bh
  00460c7f  663d1400              cmp       ax,14h
  00460c83  7406                  je        short 00460C8Bh
  00460c85  663d1e00              cmp       ax,1Eh
  00460c89  752a                  jne       short 00460CB5h

LAB_00460c8b:
  00460c8b  a1cc975e00            mov       eax,[5E97CCh]
  00460c90  6a00                  push      0
  00460c92  6a00                  push      0
  00460c94  6800100000            push      1000h
  00460c99  8b88a8000000          mov       ecx,[eax+0A8h]
  00460c9f  6820030000            push      320h
  00460ca4  8b11                  mov       edx,[ecx]
  00460ca6  660fb64212            movzx     ax,byte [edx+12h]
  00460cab  50                    push      eax
  00460cac  56                    push      esi
  00460cad  e8ce730000            call      00468080h
  00460cb2  83c418                add       esp,18h

LAB_00460cb5:
  00460cb5  66ff4612              inc       word [esi+12h]
  00460cb9  66837e1220            cmp       word [esi+12h],20h
  00460cbe  7c0a                  jl        short 00460CCAh
  00460cc0  66897e10              mov       [esi+10h],di
  00460cc4  66c746120000          mov       word [esi+12h],0

LAB_00460cca:
  00460cca  66397e10              cmp       [esi+10h],di
  00460cce  7532                  jne       short 00460D02h
  00460cd0  6a00                  push      0
  00460cd2  6a00                  push      0
  00460cd4  6a00                  push      0
  00460cd6  e865640e00            call      00547140h
  00460cdb  99                    cdq
  00460cdc  b906000000            mov       ecx,6
  00460ce1  f7f9                  idiv      ecx
  00460ce3  2bd7                  sub       edx,edi
  00460ce5  52                    push      edx
  00460ce6  6a00                  push      0
  00460ce8  56                    push      esi
  00460ce9  e872b1fbff            call      0041BE60h
  00460cee  83c418                add       esp,18h
  00460cf1  66ff4612              inc       word [esi+12h]
  00460cf5  66837e1228            cmp       word [esi+12h],28h
  00460cfa  7c06                  jl        short 00460D02h
  00460cfc  66c746100400          mov       word [esi+10h],4

LAB_00460d02:
  00460d02  5f                    pop       edi
  00460d03  5e                    pop       esi
  00460d04  c3                    ret

5 cible(s) de saut interne : 0x00460c5f, 0x00460c8b, 0x00460cb5, 0x00460cca, 0x00460d02
