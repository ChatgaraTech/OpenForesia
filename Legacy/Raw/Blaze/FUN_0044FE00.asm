Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044fe00  section .text  offset fichier 0x4f200  315 octets

  0044fe00  53                    push      ebx
  0044fe01  33db                  xor       ebx,ebx
  0044fe03  56                    push      esi
  0044fe04  8b74240c              mov       esi,[esp+0Ch]
  0044fe08  53                    push      ebx
  0044fe09  53                    push      ebx
  0044fe0a  56                    push      esi
  0044fe0b  e83082ffff            call      00448040h
  0044fe10  8b4678                mov       eax,[esi+78h]
  0044fe13  83c40c                add       esp,0Ch
  0044fe16  3bc3                  cmp       eax,ebx
  0044fe18  7505                  jne       short 0044FE1Fh
  0044fe1a  b858e55d00            mov       eax,5DE558h

LAB_0044fe1f:
  0044fe1f  8b481c                mov       ecx,[eax+1Ch]
  0044fe22  894e50                mov       [esi+50h],ecx
  0044fe25  8b5020                mov       edx,[eax+20h]
  0044fe28  81c200c00300          add       edx,3C000h
  0044fe2e  66395e10              cmp       [esi+10h],bx
  0044fe32  895654                mov       [esi+54h],edx
  0044fe35  8b4024                mov       eax,[eax+24h]
  0044fe38  894658                mov       [esi+58h],eax
  0044fe3b  b801000000            mov       eax,1
  0044fe40  753a                  jne       short 0044FE7Ch
  0044fe42  8a8e9a000000          mov       cl,[esi+9Ah]
  0044fe48  66894610              mov       [esi+10h],ax
  0044fe4c  884e0a                mov       [esi+0Ah],cl
  0044fe4f  8b4e20                mov       ecx,[esi+20h]
  0044fe52  81c100a00f00          add       ecx,0FA000h
  0044fe58  c7463c50155500        mov       dword [esi+3Ch],551550h
  0044fe5f  885e40                mov       [esi+40h],bl
  0044fe62  885e0b                mov       [esi+0Bh],bl
  0044fe65  894e20                mov       [esi+20h],ecx
  0044fe68  66895e28              mov       [esi+28h],bx
  0044fe6c  66895e2a              mov       [esi+2Ah],bx
  0044fe70  c64638c8              mov       byte [esi+38h],0C8h
  0044fe74  c64639c8              mov       byte [esi+39h],0C8h
  0044fe78  c6463ac8              mov       byte [esi+3Ah],0C8h

LAB_0044fe7c:
  0044fe7c  66394610              cmp       [esi+10h],ax
  0044fe80  0f85b2000000          jne       near 0044FF38h
  0044fe86  8b4620                mov       eax,[esi+20h]
  0044fe89  050060ffff            add       eax,0FFFF6000h
  0044fe8e  894620                mov       [esi+20h],eax
  0044fe91  8bc8                  mov       ecx,eax
  0044fe93  8b4654                mov       eax,[esi+54h]
  0044fe96  3bc8                  cmp       ecx,eax
  0044fe98  7d03                  jge       short 0044FE9Dh
  0044fe9a  894620                mov       [esi+20h],eax

LAB_0044fe9d:
  0044fe9d  8b5648                mov       edx,[esi+48h]
  0044fea0  52                    push      edx
  0044fea1  e8765b0f00            call      00545A1Ch
  0044fea6  0faf4644              imul      eax,[esi+44h]
  0044feaa  034650                add       eax,[esi+50h]
  0044fead  89461c                mov       [esi+1Ch],eax
  0044feb0  8b4648                mov       eax,[esi+48h]
  0044feb3  50                    push      eax
  0044feb4  e85d5b0f00            call      00545A16h
  0044feb9  0faf4644              imul      eax,[esi+44h]
  0044febd  8b4e58                mov       ecx,[esi+58h]
  0044fec0  8b5648                mov       edx,[esi+48h]
  0044fec3  03c1                  add       eax,ecx
  0044fec5  8b4e5c                mov       ecx,[esi+5Ch]
  0044fec8  03d1                  add       edx,ecx
  0044feca  894624                mov       [esi+24h],eax
  0044fecd  8bc2                  mov       eax,edx
  0044fecf  895648                mov       [esi+48h],edx
  0044fed2  3d00100000            cmp       eax,1000h
  0044fed7  7c08                  jl        short 0044FEE1h
  0044fed9  0500f0ffff            add       eax,0FFFFF000h
  0044fede  894648                mov       [esi+48h],eax

LAB_0044fee1:
  0044fee1  8b4648                mov       eax,[esi+48h]
  0044fee4  3bc3                  cmp       eax,ebx
  0044fee6  7d08                  jge       short 0044FEF0h
  0044fee8  0500100000            add       eax,1000h
  0044feed  894648                mov       [esi+48h],eax

LAB_0044fef0:
  0044fef0  66ff4e14              dec       word [esi+14h]
  0044fef4  66395e14              cmp       [esi+14h],bx
  0044fef8  7d1d                  jge       short 0044FF17h
  0044fefa  668346289c            add       word [esi+28h],0FF9Ch
  0044feff  668b4628              mov       ax,[esi+28h]
  0044ff03  6681462a38ff          add       word [esi+2Ah],0FF38h
  0044ff09  663bc3                cmp       ax,bx
  0044ff0c  7d2a                  jge       short 0044FF38h
  0044ff0e  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0044ff14  5e                    pop       esi
  0044ff15  5b                    pop       ebx
  0044ff16  c3                    ret

LAB_0044ff17:
  0044ff17  6683462864            add       word [esi+28h],64h
  0044ff1c  668b4628              mov       ax,[esi+28h]
  0044ff20  6681462ac800          add       word [esi+2Ah],0C8h
  0044ff26  663d0008              cmp       ax,800h
  0044ff2a  7c0c                  jl        short 0044FF38h
  0044ff2c  66c746280008          mov       word [esi+28h],800h
  0044ff32  66c7462a0010          mov       word [esi+2Ah],1000h

LAB_0044ff38:
  0044ff38  5e                    pop       esi
  0044ff39  5b                    pop       ebx
  0044ff3a  c3                    ret

7 cible(s) de saut interne : 0x0044fe1f, 0x0044fe7c, 0x0044fe9d, 0x0044fee1, 0x0044fef0, 0x0044ff17, 0x0044ff38
