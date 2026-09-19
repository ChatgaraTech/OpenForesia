Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043bf50  section .text  offset fichier 0x3b350  192 octets

  0043bf50  53                    push      ebx
  0043bf51  56                    push      esi
  0043bf52  8b74240c              mov       esi,[esp+0Ch]
  0043bf56  33db                  xor       ebx,ebx
  0043bf58  b901000000            mov       ecx,1
  0043bf5d  66395e10              cmp       [esi+10h],bx
  0043bf61  7552                  jne       short 0043BFB5h
  0043bf63  668b4650              mov       ax,[esi+50h]
  0043bf67  806636fe              and       byte [esi+36h],0FEh
  0043bf6b  66814632000c          add       word [esi+32h],0C00h
  0043bf71  66894634              mov       [esi+34h],ax
  0043bf75  668b5654              mov       dx,[esi+54h]
  0043bf79  b870170000            mov       eax,1770h
  0043bf7e  6689462c              mov       [esi+2Ch],ax
  0043bf82  6689462a              mov       [esi+2Ah],ax
  0043bf86  8b4620                mov       eax,[esi+20h]
  0043bf89  66894e10              mov       [esi+10h],cx
  0043bf8d  0500000a00            add       eax,0A0000h
  0043bf92  c6460408              mov       byte [esi+4],8
  0043bf96  894620                mov       [esi+20h],eax
  0043bf99  668b4658              mov       ax,[esi+58h]
  0043bf9d  c6460a02              mov       byte [esi+0Ah],2
  0043bfa1  885e0b                mov       [esi+0Bh],bl
  0043bfa4  66895628              mov       [esi+28h],dx
  0043bfa8  66894616              mov       [esi+16h],ax
  0043bfac  885e38                mov       [esi+38h],bl
  0043bfaf  885e39                mov       [esi+39h],bl
  0043bfb2  885e3a                mov       [esi+3Ah],bl

LAB_0043bfb5:
  0043bfb5  66394e10              cmp       [esi+10h],cx
  0043bfb9  754e                  jne       short 0043C009h
  0043bfbb  e880b11000            call      00547140h
  0043bfc0  99                    cdq
  0043bfc1  33c2                  xor       eax,edx
  0043bfc3  2bc2                  sub       eax,edx
  0043bfc5  83e003                and       eax,3
  0043bfc8  33c2                  xor       eax,edx
  0043bfca  2bc2                  sub       eax,edx
  0043bfcc  0408                  add       al,8
  0043bfce  66ff4e16              dec       word [esi+16h]
  0043bfd2  66395e16              cmp       [esi+16h],bx
  0043bfd6  884604                mov       [esi+4],al
  0043bfd9  7d1f                  jge       short 0043BFFAh
  0043bfdb  6ae2                  push      0FFFFFFE2h
  0043bfdd  6ae2                  push      0FFFFFFE2h
  0043bfdf  6ae2                  push      0FFFFFFE2h
  0043bfe1  56                    push      esi
  0043bfe2  e8f98dfcff            call      00404DE0h
  0043bfe7  8a4638                mov       al,[esi+38h]
  0043bfea  83c410                add       esp,10h
  0043bfed  3ac3                  cmp       al,bl
  0043bfef  7518                  jne       short 0043C009h
  0043bff1  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043bff7  5e                    pop       esi
  0043bff8  5b                    pop       ebx
  0043bff9  c3                    ret

LAB_0043bffa:
  0043bffa  6a32                  push      32h
  0043bffc  6a32                  push      32h
  0043bffe  6a32                  push      32h
  0043c000  56                    push      esi
  0043c001  e8da8dfcff            call      00404DE0h
  0043c006  83c410                add       esp,10h

LAB_0043c009:
  0043c009  5e                    pop       esi
  0043c00a  5b                    pop       ebx
  0043c00b  c3                    ret
  0043c00c  90                    nop
  0043c00d  90                    nop
  0043c00e  90                    nop
  0043c00f  90                    nop

3 cible(s) de saut interne : 0x0043bfb5, 0x0043bffa, 0x0043c009
