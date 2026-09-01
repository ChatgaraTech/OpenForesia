Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044ba00  section .text  offset fichier 0x4ae00  226 octets

  0044ba00  53                    push      ebx
  0044ba01  56                    push      esi
  0044ba02  8b74240c              mov       esi,[esp+0Ch]
  0044ba06  33db                  xor       ebx,ebx
  0044ba08  8b4678                mov       eax,[esi+78h]
  0044ba0b  3bc3                  cmp       eax,ebx
  0044ba0d  7505                  jne       short 0044BA14h
  0044ba0f  b858e55d00            mov       eax,5DE558h

LAB_0044ba14:
  0044ba14  8b481c                mov       ecx,[eax+1Ch]
  0044ba17  66395e10              cmp       [esi+10h],bx
  0044ba1b  894e1c                mov       [esi+1Ch],ecx
  0044ba1e  8b5020                mov       edx,[eax+20h]
  0044ba21  895620                mov       [esi+20h],edx
  0044ba24  8b4024                mov       eax,[eax+24h]
  0044ba27  894624                mov       [esi+24h],eax
  0044ba2a  b801000000            mov       eax,1
  0044ba2f  7534                  jne       short 0044BA65h
  0044ba31  b920030000            mov       ecx,320h
  0044ba36  885e04                mov       [esi+4],bl
  0044ba39  c6460a02              mov       byte [esi+0Ah],2
  0044ba3d  88460b                mov       [esi+0Bh],al
  0044ba40  66894e28              mov       [esi+28h],cx
  0044ba44  66894e2c              mov       [esi+2Ch],cx
  0044ba48  66c7462a2c01          mov       word [esi+2Ah],12Ch
  0044ba4e  66c746340004          mov       word [esi+34h],400h
  0044ba54  66894610              mov       [esi+10h],ax
  0044ba58  66895e14              mov       [esi+14h],bx
  0044ba5c  885e38                mov       [esi+38h],bl
  0044ba5f  885e39                mov       [esi+39h],bl
  0044ba62  885e3a                mov       [esi+3Ah],bl

LAB_0044ba65:
  0044ba65  66394610              cmp       [esi+10h],ax
  0044ba69  7574                  jne       short 0044BADFh
  0044ba6b  6a14                  push      14h
  0044ba6d  6a14                  push      14h
  0044ba6f  6a14                  push      14h
  0044ba71  56                    push      esi
  0044ba72  e86993fbff            call      00404DE0h
  0044ba77  6683463264            add       word [esi+32h],64h
  0044ba7c  668b4632              mov       ax,[esi+32h]
  0044ba80  6681462ab400          add       word [esi+2Ah],0B4h
  0044ba86  66834634ec            add       word [esi+34h],0FFECh
  0044ba8b  668b562a              mov       dx,[esi+2Ah]
  0044ba8f  668b4e34              mov       cx,[esi+34h]
  0044ba93  83c410                add       esp,10h
  0044ba96  663d0010              cmp       ax,1000h
  0044ba9a  7c09                  jl        short 0044BAA5h
  0044ba9c  0500f0ffff            add       eax,0FFFFF000h
  0044baa1  66894632              mov       [esi+32h],ax

LAB_0044baa5:
  0044baa5  663bcb                cmp       cx,bx
  0044baa8  7d0d                  jge       short 0044BAB7h
  0044baaa  8b06                  mov       eax,[esi]
  0044baac  66895e34              mov       [esi+34h],bx
  0044bab0  25ffffff7f            and       eax,7FFFFFFFh
  0044bab5  8906                  mov       [esi],eax

LAB_0044bab7:
  0044bab7  66817e349600          cmp       word [esi+34h],96h
  0044babd  7d20                  jge       short 0044BADFh
  0044babf  66834628ec            add       word [esi+28h],0FFECh
  0044bac4  6683462cec            add       word [esi+2Ch],0FFECh
  0044bac9  6ace                  push      0FFFFFFCEh
  0044bacb  6ace                  push      0FFFFFFCEh
  0044bacd  83c232                add       edx,32h
  0044bad0  6ace                  push      0FFFFFFCEh
  0044bad2  56                    push      esi
  0044bad3  6689562a              mov       [esi+2Ah],dx
  0044bad7  e80493fbff            call      00404DE0h
  0044badc  83c410                add       esp,10h

LAB_0044badf:
  0044badf  5e                    pop       esi
  0044bae0  5b                    pop       ebx
  0044bae1  c3                    ret

5 cible(s) de saut interne : 0x0044ba14, 0x0044ba65, 0x0044baa5, 0x0044bab7, 0x0044badf
