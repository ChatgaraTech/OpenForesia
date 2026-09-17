C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00416c40  section .text  offset fichier 0x16040  416 octets

  00416c40  53                    push      ebx
  00416c41  56                    push      esi
  00416c42  8b74240c              mov       esi,[esp+0Ch]
  00416c46  bb01000000            mov       ebx,1
  00416c4b  66837e1000            cmp       word [esi+10h],0
  00416c50  0f859f000000          jne       near 00416CF5h
  00416c56  c7463ce0fa5400        mov       dword [esi+3Ch],54FAE0h
  00416c5d  e8de041300            call      00547140h
  00416c62  99                    cdq
  00416c63  b903000000            mov       ecx,3
  00416c68  885e0a                mov       [esi+0Ah],bl
  00416c6b  f7f9                  idiv      ecx
  00416c6d  8b4e24                mov       ecx,[esi+24h]
  00416c70  b0a0                  mov       al,0A0h
  00416c72  884638                mov       [esi+38h],al
  00416c75  884639                mov       [esi+39h],al
  00416c78  88463a                mov       [esi+3Ah],al
  00416c7b  b8f4010000            mov       eax,1F4h
  00416c80  66894628              mov       [esi+28h],ax
  00416c84  6689462a              mov       [esi+2Ah],ax
  00416c88  8b4620                mov       eax,[esi+20h]
  00416c8b  894e58                mov       [esi+58h],ecx
  00416c8e  b900a00000            mov       ecx,0A000h
  00416c93  c6460b00              mov       byte [esi+0Bh],0
  00416c97  894654                mov       [esi+54h],eax
  00416c9a  66895e10              mov       [esi+10h],bx
  00416c9e  66c746120000          mov       word [esi+12h],0
  00416ca4  66c746140500          mov       word [esi+14h],5
  00416caa  66898e82000000        mov       [esi+82h],cx
  00416cb1  66898e86000000        mov       [esi+86h],cx
  00416cb8  66898e8a000000        mov       [esi+8Ah],cx
  00416cbf  66898e8e000000        mov       [esi+8Eh],cx
  00416cc6  894664                mov       [esi+64h],eax
  00416cc9  894660                mov       [esi+60h],eax
  00416ccc  6800800000            push      8000h
  00416cd1  56                    push      esi
  00416cd2  fec2                  inc       dl
  00416cd4  885640                mov       [esi+40h],dl
  00416cd7  8b561c                mov       edx,[esi+1Ch]
  00416cda  895650                mov       [esi+50h],edx
  00416cdd  8b15c07b5f00          mov       edx,[5F7BC0h]
  00416ce3  52                    push      edx
  00416ce4  e8e7c31100            call      005330D0h
  00416ce9  83c40c                add       esp,0Ch
  00416cec  56                    push      esi
  00416ced  e84ecc1100            call      00533940h
  00416cf2  83c404                add       esp,4

LAB_00416cf5:
  00416cf5  66395e10              cmp       [esi+10h],bx
  00416cf9  0f85c8000000          jne       near 00416DC7h
  00416cff  0fbf4632              movsx     eax,word [esi+32h]
  00416d03  50                    push      eax
  00416d04  e813ed1200            call      00545A1Ch
  00416d09  0fbf4e12              movsx     ecx,word [esi+12h]
  00416d0d  0fafc1                imul      eax,ecx
  00416d10  0fbf5632              movsx     edx,word [esi+32h]
  00416d14  8b4e50                mov       ecx,[esi+50h]
  00416d17  52                    push      edx
  00416d18  03c1                  add       eax,ecx
  00416d1a  89461c                mov       [esi+1Ch],eax
  00416d1d  e8f4ec1200            call      00545A16h
  00416d22  0fbf4e12              movsx     ecx,word [esi+12h]
  00416d26  0fafc1                imul      eax,ecx
  00416d29  8b4e58                mov       ecx,[esi+58h]
  00416d2c  8b5620                mov       edx,[esi+20h]
  00416d2f  03c1                  add       eax,ecx
  00416d31  6afd                  push      0FFFFFFFDh
  00416d33  894624                mov       [esi+24h],eax
  00416d36  6afd                  push      0FFFFFFFDh
  00416d38  81c200e0ffff          add       edx,0FFFFE000h
  00416d3e  b864000000            mov       eax,64h
  00416d43  66014628              add       [esi+28h],ax
  00416d47  6601462a              add       [esi+2Ah],ax
  00416d4b  6afd                  push      0FFFFFFFDh
  00416d4d  56                    push      esi
  00416d4e  895620                mov       [esi+20h],edx
  00416d51  e88ae0feff            call      00404DE0h
  00416d56  83c410                add       esp,10h
  00416d59  56                    push      esi
  00416d5a  e8e1cb1100            call      00533940h
  00416d5f  8b8690000000          mov       eax,[esi+90h]
  00416d65  83c404                add       esp,4
  00416d68  a902880000            test      eax,8802h
  00416d6d  7406                  je        short 00416D75h
  00416d6f  66c746100200          mov       word [esi+10h],2

LAB_00416d75:
  00416d75  8b4e20                mov       ecx,[esi+20h]
  00416d78  8b4660                mov       eax,[esi+60h]
  00416d7b  3bc1                  cmp       eax,ecx
  00416d7d  7c06                  jl        short 00416D85h
  00416d7f  66c746100200          mov       word [esi+10h],2

LAB_00416d85:
  00416d85  66ff4e14              dec       word [esi+14h]
  00416d89  668b4614              mov       ax,[esi+14h]
  00416d8d  6683461214            add       word [esi+12h],14h
  00416d92  6685c0                test      ax,ax
  00416d95  7f15                  jg        short 00416DACh
  00416d97  8a4640                mov       al,[esi+40h]
  00416d9a  66c746140500          mov       word [esi+14h],5
  00416da0  fec0                  inc       al
  00416da2  3c04                  cmp       al,4
  00416da4  884640                mov       [esi+40h],al
  00416da7  7503                  jne       short 00416DACh
  00416da9  885e40                mov       [esi+40h],bl

LAB_00416dac:
  00416dac  8b5624                mov       edx,[esi+24h]
  00416daf  8b461c                mov       eax,[esi+1Ch]
  00416db2  6a00                  push      0
  00416db4  6a00                  push      0
  00416db6  6a03                  push      3
  00416db8  6a03                  push      3
  00416dba  6a32                  push      32h
  00416dbc  52                    push      edx
  00416dbd  51                    push      ecx
  00416dbe  50                    push      eax
  00416dbf  e88c210000            call      00418F50h
  00416dc4  83c420                add       esp,20h

LAB_00416dc7:
  00416dc7  66837e1002            cmp       word [esi+10h],2
  00416dcc  7506                  jne       short 00416DD4h
  00416dce  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00416dd4:
  00416dd4  5e                    pop       esi
  00416dd5  5b                    pop       ebx
  00416dd6  c3                    ret
  00416dd7  90                    nop
  00416dd8  90                    nop
  00416dd9  90                    nop
  00416dda  90                    nop
  00416ddb  90                    nop
  00416ddc  90                    nop
  00416ddd  90                    nop
  00416dde  90                    nop
  00416ddf  90                    nop

6 cible(s) de saut interne : 0x00416cf5, 0x00416d75, 0x00416d85, 0x00416dac, 0x00416dc7, 0x00416dd4
