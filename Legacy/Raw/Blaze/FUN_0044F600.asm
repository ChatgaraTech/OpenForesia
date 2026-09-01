Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044f600  section .text  offset fichier 0x4ea00  336 octets

  0044f600  53                    push      ebx
  0044f601  56                    push      esi
  0044f602  8b74240c              mov       esi,[esp+0Ch]
  0044f606  33db                  xor       ebx,ebx
  0044f608  8b466c                mov       eax,[esi+6Ch]
  0044f60b  3bc3                  cmp       eax,ebx
  0044f60d  7505                  jne       short 0044F614h
  0044f60f  b858e55d00            mov       eax,5DE558h

LAB_0044f614:
  0044f614  f70000000050          test      dword [eax],50000000h
  0044f61a  751c                  jne       short 0044F638h
  0044f61c  f70600000002          test      dword [esi],2000000h
  0044f622  7514                  jne       short 0044F638h
  0044f624  8b481c                mov       ecx,[eax+1Ch]
  0044f627  894e1c                mov       [esi+1Ch],ecx
  0044f62a  8b5060                mov       edx,[eax+60h]
  0044f62d  895620                mov       [esi+20h],edx
  0044f630  8b4024                mov       eax,[eax+24h]
  0044f633  894624                mov       [esi+24h],eax
  0044f636  eb06                  jmp       short 0044F63Eh

LAB_0044f638:
  0044f638  810e00000002          or        dword [esi],2000000h

LAB_0044f63e:
  0044f63e  8b4678                mov       eax,[esi+78h]
  0044f641  3bc3                  cmp       eax,ebx
  0044f643  7505                  jne       short 0044F64Ah
  0044f645  b858e55d00            mov       eax,5DE558h

LAB_0044f64a:
  0044f64a  50                    push      eax
  0044f64b  e870950e00            call      00538BC0h
  0044f650  8d0440                lea       eax,[eax+eax*2]
  0044f653  83c404                add       esp,4
  0044f656  8d0480                lea       eax,[eax+eax*4]
  0044f659  d1e0                  shl       eax,1
  0044f65b  3d00100000            cmp       eax,1000h
  0044f660  7d05                  jge       short 0044F667h
  0044f662  b800100000            mov       eax,1000h

LAB_0044f667:
  0044f667  3d30750000            cmp       eax,7530h
  0044f66c  7e05                  jle       short 0044F673h
  0044f66e  b830750000            mov       eax,7530h

LAB_0044f673:
  0044f673  0fbf4e14              movsx     ecx,word [esi+14h]
  0044f677  66395e10              cmp       [esi+10h],bx
  0044f67b  8d840800f0ffff        lea       eax,[eax+ecx-1000h]
  0044f682  b901000000            mov       ecx,1
  0044f687  752c                  jne       short 0044F6B5h
  0044f689  8a969a000000          mov       dl,[esi+9Ah]
  0044f68f  66894e10              mov       [esi+10h],cx
  0044f693  c6460405              mov       byte [esi+4],5
  0044f697  885e40                mov       [esi+40h],bl
  0044f69a  88560a                mov       [esi+0Ah],dl
  0044f69d  885e0b                mov       [esi+0Bh],bl
  0044f6a0  66894628              mov       [esi+28h],ax
  0044f6a4  6689462c              mov       [esi+2Ch],ax
  0044f6a8  66895e2a              mov       [esi+2Ah],bx
  0044f6ac  885e38                mov       [esi+38h],bl
  0044f6af  885e39                mov       [esi+39h],bl
  0044f6b2  885e3a                mov       [esi+3Ah],bl

LAB_0044f6b5:
  0044f6b5  66394e10              cmp       [esi+10h],cx
  0044f6b9  0f8585000000          jne       near 0044F744h
  0044f6bf  668b4632              mov       ax,[esi+32h]
  0044f6c3  663d0010              cmp       ax,1000h
  0044f6c7  7c09                  jl        short 0044F6D2h
  0044f6c9  0500f0ffff            add       eax,0FFFFF000h
  0044f6ce  66894632              mov       [esi+32h],ax

LAB_0044f6d2:
  0044f6d2  668b462a              mov       ax,[esi+2Ah]
  0044f6d6  baff7f0000            mov       edx,7FFFh
  0044f6db  0fbfc8                movsx     ecx,ax
  0044f6de  2bd1                  sub       edx,ecx
  0044f6e0  8b4e44                mov       ecx,[esi+44h]
  0044f6e3  c1fa03                sar       edx,3
  0044f6e6  03c2                  add       eax,edx
  0044f6e8  6689462a              mov       [esi+2Ah],ax
  0044f6ec  0fbfc0                movsx     eax,ax
  0044f6ef  3bc1                  cmp       eax,ecx
  0044f6f1  7e08                  jle       short 0044F6FBh
  0044f6f3  668b4e44              mov       cx,[esi+44h]
  0044f6f7  66894e2a              mov       [esi+2Ah],cx

LAB_0044f6fb:
  0044f6fb  8b4648                mov       eax,[esi+48h]
  0044f6fe  48                    dec       eax
  0044f6ff  894648                mov       [esi+48h],eax
  0044f702  791f                  jns       short 0044F723h
  0044f704  6af6                  push      0FFFFFFF6h
  0044f706  6af6                  push      0FFFFFFF6h
  0044f708  6af6                  push      0FFFFFFF6h
  0044f70a  56                    push      esi
  0044f70b  e8d056fbff            call      00404DE0h
  0044f710  8a4638                mov       al,[esi+38h]
  0044f713  83c410                add       esp,10h
  0044f716  3ac3                  cmp       al,bl
  0044f718  752a                  jne       short 0044F744h
  0044f71a  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0044f720  5e                    pop       esi
  0044f721  5b                    pop       ebx
  0044f722  c3                    ret

LAB_0044f723:
  0044f723  6a0a                  push      0Ah
  0044f725  6a0a                  push      0Ah
  0044f727  6a0a                  push      0Ah
  0044f729  56                    push      esi
  0044f72a  e8b156fbff            call      00404DE0h
  0044f72f  8a4e38                mov       cl,[esi+38h]
  0044f732  b0b4                  mov       al,0B4h
  0044f734  83c410                add       esp,10h
  0044f737  3ac8                  cmp       cl,al
  0044f739  7609                  jbe       short 0044F744h
  0044f73b  884638                mov       [esi+38h],al
  0044f73e  884639                mov       [esi+39h],al
  0044f741  88463a                mov       [esi+3Ah],al

LAB_0044f744:
  0044f744  5e                    pop       esi
  0044f745  5b                    pop       ebx
  0044f746  c3                    ret
  0044f747  90                    nop
  0044f748  90                    nop
  0044f749  90                    nop
  0044f74a  90                    nop
  0044f74b  90                    nop
  0044f74c  90                    nop
  0044f74d  90                    nop
  0044f74e  90                    nop
  0044f74f  90                    nop

11 cible(s) de saut interne : 0x0044f614, 0x0044f638, 0x0044f63e, 0x0044f64a, 0x0044f667, 0x0044f673, 0x0044f6b5, 0x0044f6d2, 0x0044f6fb, 0x0044f723, 0x0044f744
