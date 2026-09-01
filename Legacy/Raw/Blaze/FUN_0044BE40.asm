Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044be40  section .text  offset fichier 0x4b240  177 octets

  0044be40  53                    push      ebx
  0044be41  56                    push      esi
  0044be42  8b74240c              mov       esi,[esp+0Ch]
  0044be46  33db                  xor       ebx,ebx
  0044be48  8b4678                mov       eax,[esi+78h]
  0044be4b  3bc3                  cmp       eax,ebx
  0044be4d  7505                  jne       short 0044BE54h
  0044be4f  b858e55d00            mov       eax,5DE558h

LAB_0044be54:
  0044be54  8b481c                mov       ecx,[eax+1Ch]
  0044be57  894e1c                mov       [esi+1Ch],ecx
  0044be5a  8b5020                mov       edx,[eax+20h]
  0044be5d  81c200600400          add       edx,46000h
  0044be63  66395e10              cmp       [esi+10h],bx
  0044be67  895620                mov       [esi+20h],edx
  0044be6a  8b4024                mov       eax,[eax+24h]
  0044be6d  894624                mov       [esi+24h],eax
  0044be70  b801000000            mov       eax,1
  0044be75  752e                  jne       short 0044BEA5h
  0044be77  668146320003          add       word [esi+32h],300h
  0044be7d  c6460411              mov       byte [esi+4],11h
  0044be81  c6460a0f              mov       byte [esi+0Ah],0Fh
  0044be85  885e0b                mov       [esi+0Bh],bl
  0044be88  66c746340002          mov       word [esi+34h],200h
  0044be8e  66894610              mov       [esi+10h],ax
  0044be92  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  0044be98  66895e12              mov       [esi+12h],bx
  0044be9c  885e38                mov       [esi+38h],bl
  0044be9f  885e39                mov       [esi+39h],bl
  0044bea2  885e3a                mov       [esi+3Ah],bl

LAB_0044bea5:
  0044bea5  66394610              cmp       [esi+10h],ax
  0044bea9  7543                  jne       short 0044BEEEh
  0044beab  668146342c01          add       word [esi+34h],12Ch
  0044beb1  66837e1205            cmp       word [esi+12h],5
  0044beb6  7d16                  jge       short 0044BECEh
  0044beb8  6a46                  push      46h
  0044beba  6a46                  push      46h
  0044bebc  6a46                  push      46h
  0044bebe  56                    push      esi
  0044bebf  e81c8ffbff            call      00404DE0h
  0044bec4  83c410                add       esp,10h
  0044bec7  66ff4612              inc       word [esi+12h]
  0044becb  5e                    pop       esi
  0044becc  5b                    pop       ebx
  0044becd  c3                    ret

LAB_0044bece:
  0044bece  6ae2                  push      0FFFFFFE2h
  0044bed0  6ae2                  push      0FFFFFFE2h
  0044bed2  6ae2                  push      0FFFFFFE2h
  0044bed4  56                    push      esi
  0044bed5  e8068ffbff            call      00404DE0h
  0044beda  8a4638                mov       al,[esi+38h]
  0044bedd  83c410                add       esp,10h
  0044bee0  3ac3                  cmp       al,bl
  0044bee2  7506                  jne       short 0044BEEAh
  0044bee4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044beea:
  0044beea  66ff4612              inc       word [esi+12h]

LAB_0044beee:
  0044beee  5e                    pop       esi
  0044beef  5b                    pop       ebx
  0044bef0  c3                    ret

5 cible(s) de saut interne : 0x0044be54, 0x0044bea5, 0x0044bece, 0x0044beea, 0x0044beee
