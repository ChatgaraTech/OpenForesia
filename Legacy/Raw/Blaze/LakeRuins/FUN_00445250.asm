Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445250  section .text  offset fichier 0x44650  128 octets

  00445250  53                    push      ebx
  00445251  56                    push      esi
  00445252  8b35b4876000          mov       esi,[6087B4h]
  00445258  33d2                  xor       edx,edx
  0044525a  b308                  mov       bl,8

LAB_0044525c:
  0044525c  0fbfc2                movsx     eax,dx
  0044525f  8d0c80                lea       ecx,[eax+eax*4]
  00445262  c1e103                shl       ecx,3
  00445265  2bc8                  sub       ecx,eax
  00445267  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0044526e  f7c100000080          test      ecx,80000000h
  00445274  7416                  je        short 0044528Ch
  00445276  f7c100000050          test      ecx,50000000h
  0044527c  750e                  jne       short 0044528Ch
  0044527e  8b0e                  mov       ecx,[esi]
  00445280  c1e00d                shl       eax,0Dh
  00445283  849c0850010f00        test      [eax+ecx+0F0150h],bl
  0044528a  740c                  je        short 00445298h

LAB_0044528c:
  0044528c  42                    inc       edx
  0044528d  6683fa03              cmp       dx,3
  00445291  7ec9                  jle       short 0044525Ch
  00445293  33c0                  xor       eax,eax
  00445295  5e                    pop       esi
  00445296  5b                    pop       ebx
  00445297  c3                    ret

LAB_00445298:
  00445298  0fbfc2                movsx     eax,dx
  0044529b  8b0dcc975e00          mov       ecx,[5E97CCh]
  004452a1  6a00                  push      0
  004452a3  8d1480                lea       edx,[eax+eax*4]
  004452a6  68b0335600            push      5633B0h
  004452ab  c1e203                shl       edx,3
  004452ae  2bd0                  sub       edx,eax
  004452b0  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  004452b7  8b5170                mov       edx,[ecx+70h]
  004452ba  50                    push      eax
  004452bb  8b4210                mov       eax,[edx+10h]
  004452be  50                    push      eax
  004452bf  e88cf00f00            call      00544350h
  004452c4  83c410                add       esp,10h
  004452c7  33c0                  xor       eax,eax
  004452c9  5e                    pop       esi
  004452ca  5b                    pop       ebx
  004452cb  c3                    ret
  004452cc  90                    nop
  004452cd  90                    nop
  004452ce  90                    nop
  004452cf  90                    nop

3 cible(s) de saut interne : 0x0044525c, 0x0044528c, 0x00445298
