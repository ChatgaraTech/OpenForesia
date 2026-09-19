Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445360  section .text  offset fichier 0x44760  128 octets

  00445360  53                    push      ebx
  00445361  56                    push      esi
  00445362  8b35b4876000          mov       esi,[6087B4h]
  00445368  33d2                  xor       edx,edx
  0044536a  b308                  mov       bl,8

LAB_0044536c:
  0044536c  0fbfc2                movsx     eax,dx
  0044536f  8d0c80                lea       ecx,[eax+eax*4]
  00445372  c1e103                shl       ecx,3
  00445375  2bc8                  sub       ecx,eax
  00445377  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0044537e  f7c100000080          test      ecx,80000000h
  00445384  7416                  je        short 0044539Ch
  00445386  f7c100000050          test      ecx,50000000h
  0044538c  750e                  jne       short 0044539Ch
  0044538e  8b0e                  mov       ecx,[esi]
  00445390  c1e00d                shl       eax,0Dh
  00445393  849c0850010f00        test      [eax+ecx+0F0150h],bl
  0044539a  740c                  je        short 004453A8h

LAB_0044539c:
  0044539c  42                    inc       edx
  0044539d  6683fa03              cmp       dx,3
  004453a1  7ec9                  jle       short 0044536Ch
  004453a3  33c0                  xor       eax,eax
  004453a5  5e                    pop       esi
  004453a6  5b                    pop       ebx
  004453a7  c3                    ret

LAB_004453a8:
  004453a8  0fbfc2                movsx     eax,dx
  004453ab  8b0dcc975e00          mov       ecx,[5E97CCh]
  004453b1  6a00                  push      0
  004453b3  8d1480                lea       edx,[eax+eax*4]
  004453b6  68b0335600            push      5633B0h
  004453bb  c1e203                shl       edx,3
  004453be  2bd0                  sub       edx,eax
  004453c0  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  004453c7  8b5170                mov       edx,[ecx+70h]
  004453ca  50                    push      eax
  004453cb  8b422c                mov       eax,[edx+2Ch]
  004453ce  50                    push      eax
  004453cf  e87cef0f00            call      00544350h
  004453d4  83c410                add       esp,10h
  004453d7  33c0                  xor       eax,eax
  004453d9  5e                    pop       esi
  004453da  5b                    pop       ebx
  004453db  c3                    ret
  004453dc  90                    nop
  004453dd  90                    nop
  004453de  90                    nop
  004453df  90                    nop

3 cible(s) de saut interne : 0x0044536c, 0x0044539c, 0x004453a8
