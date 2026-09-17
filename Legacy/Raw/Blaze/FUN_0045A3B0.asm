Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045a3b0  section .text  offset fichier 0x597b0  288 octets

  0045a3b0  53                    push      ebx
  0045a3b1  56                    push      esi
  0045a3b2  8b74240c              mov       esi,[esp+0Ch]
  0045a3b6  66837e1000            cmp       word [esi+10h],0
  0045a3bb  7515                  jne       short 0045A3D2h
  0045a3bd  66c746100100          mov       word [esi+10h],1
  0045a3c3  66c74612ff00          mov       word [esi+12h],0FFh
  0045a3c9  66c786980000000000    mov       word [esi+98h],0

LAB_0045a3d2:
  0045a3d2  6683be9800000000      cmp       word [esi+98h],0
  0045a3da  7519                  jne       short 0045A3F5h
  0045a3dc  6a00                  push      0
  0045a3de  6a00                  push      0
  0045a3e0  6800180000            push      1800h
  0045a3e5  6880000000            push      80h
  0045a3ea  6a2d                  push      2Dh
  0045a3ec  56                    push      esi
  0045a3ed  e88edc0000            call      00468080h
  0045a3f2  83c418                add       esp,18h

LAB_0045a3f5:
  0045a3f5  66ff8698000000        inc       word [esi+98h]
  0045a3fc  6683be980000003c      cmp       word [esi+98h],3Ch
  0045a404  7c09                  jl        short 0045A40Fh
  0045a406  66c786980000000000    mov       word [esi+98h],0

LAB_0045a40f:
  0045a40f  8a0d01975e00          mov       cl,[5E9701h]
  0045a415  33c0                  xor       eax,eax
  0045a417  f6c110                test      cl,10h
  0045a41a  7505                  jne       short 0045A421h
  0045a41c  b801000000            mov       eax,1

LAB_0045a421:
  0045a421  f6c120                test      cl,20h
  0045a424  7501                  jne       short 0045A427h
  0045a426  40                    inc       eax

LAB_0045a427:
  0045a427  f6c140                test      cl,40h
  0045a42a  7501                  jne       short 0045A42Dh
  0045a42c  40                    inc       eax

LAB_0045a42d:
  0045a42d  f6c180                test      cl,80h
  0045a430  7501                  jne       short 0045A433h
  0045a432  40                    inc       eax

LAB_0045a433:
  0045a433  6685c0                test      ax,ax
  0045a436  7517                  jne       short 0045A44Fh
  0045a438  0fbf4e14              movsx     ecx,word [esi+14h]
  0045a43c  8d1489                lea       edx,[ecx+ecx*4]
  0045a43f  c1e203                shl       edx,3
  0045a442  2bd1                  sub       edx,ecx
  0045a444  8d0c96                lea       ecx,[esi+edx*4]
  0045a447  8b1496                mov       edx,[esi+edx*4]
  0045a44a  80ce20                or        dh,20h
  0045a44d  8911                  mov       [ecx],edx

LAB_0045a44f:
  0045a44f  c1e006                shl       eax,6
  0045a452  663d0001              cmp       ax,100h
  0045a456  66894612              mov       [esi+12h],ax
  0045a45a  7506                  jne       short 0045A462h
  0045a45c  66c74612ff00          mov       word [esi+12h],0FFh

LAB_0045a462:
  0045a462  0fbf4612              movsx     eax,word [esi+12h]
  0045a466  8a4e38                mov       cl,[esi+38h]
  0045a469  8bd8                  mov       ebx,eax
  0045a46b  8bd1                  mov       edx,ecx
  0045a46d  668146328000          add       word [esi+32h],80h
  0045a473  81e2ff000000          and       edx,0FFh
  0045a479  2bda                  sub       ebx,edx
  0045a47b  c1fb03                sar       ebx,3
  0045a47e  02d9                  add       bl,cl
  0045a480  8a4e39                mov       cl,[esi+39h]
  0045a483  8bd1                  mov       edx,ecx
  0045a485  885e38                mov       [esi+38h],bl
  0045a488  81e2ff000000          and       edx,0FFh
  0045a48e  8bd8                  mov       ebx,eax
  0045a490  2bda                  sub       ebx,edx
  0045a492  c1fb03                sar       ebx,3
  0045a495  02d9                  add       bl,cl
  0045a497  8a4e3a                mov       cl,[esi+3Ah]
  0045a49a  8bd1                  mov       edx,ecx
  0045a49c  885e39                mov       [esi+39h],bl
  0045a49f  81e2ff000000          and       edx,0FFh
  0045a4a5  2bc2                  sub       eax,edx
  0045a4a7  c1f803                sar       eax,3
  0045a4aa  02c1                  add       al,cl
  0045a4ac  88463a                mov       [esi+3Ah],al
  0045a4af  668b4632              mov       ax,[esi+32h]
  0045a4b3  663d0010              cmp       ax,1000h
  0045a4b7  7c09                  jl        short 0045A4C2h
  0045a4b9  0500f0ffff            add       eax,0FFFFF000h
  0045a4be  66894632              mov       [esi+32h],ax

LAB_0045a4c2:
  0045a4c2  5e                    pop       esi
  0045a4c3  5b                    pop       ebx
  0045a4c4  c3                    ret
  0045a4c5  90                    nop
  0045a4c6  90                    nop
  0045a4c7  90                    nop
  0045a4c8  90                    nop
  0045a4c9  90                    nop
  0045a4ca  90                    nop
  0045a4cb  90                    nop
  0045a4cc  90                    nop
  0045a4cd  90                    nop
  0045a4ce  90                    nop
  0045a4cf  90                    nop

10 cible(s) de saut interne : 0x0045a3d2, 0x0045a3f5, 0x0045a40f, 0x0045a421, 0x0045a427, 0x0045a42d, 0x0045a433, 0x0045a44f, 0x0045a462, 0x0045a4c2
