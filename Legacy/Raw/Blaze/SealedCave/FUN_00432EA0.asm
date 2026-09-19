Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00432ea0  section .text  offset fichier 0x322a0  432 octets

  00432ea0  53                    push      ebx
  00432ea1  56                    push      esi
  00432ea2  8b74240c              mov       esi,[esp+0Ch]
  00432ea6  33db                  xor       ebx,ebx
  00432ea8  66395e10              cmp       [esi+10h],bx
  00432eac  0f8533010000          jne       near 00432FE5h
  00432eb2  c7463cc00c5500        mov       dword [esi+3Ch],550CC0h
  00432eb9  e882421100            call      00547140h
  00432ebe  99                    cdq
  00432ebf  33c2                  xor       eax,edx
  00432ec1  2bc2                  sub       eax,edx
  00432ec3  25ff030000            and       eax,3FFh
  00432ec8  33c2                  xor       eax,edx
  00432eca  2bc2                  sub       eax,edx
  00432ecc  05000c0000            add       eax,0C00h
  00432ed1  66894628              mov       [esi+28h],ax
  00432ed5  e866421100            call      00547140h
  00432eda  99                    cdq
  00432edb  33c2                  xor       eax,edx
  00432edd  2bc2                  sub       eax,edx
  00432edf  25ff030000            and       eax,3FFh
  00432ee4  33c2                  xor       eax,edx
  00432ee6  2bc2                  sub       eax,edx
  00432ee8  05000c0000            add       eax,0C00h
  00432eed  66837e1401            cmp       word [esi+14h],1
  00432ef2  6689462a              mov       [esi+2Ah],ax
  00432ef6  7506                  jne       short 00432EFEh
  00432ef8  c6460a08              mov       byte [esi+0Ah],8
  00432efc  eb04                  jmp       short 00432F02h

LAB_00432efe:
  00432efe  c6460a06              mov       byte [esi+0Ah],6

LAB_00432f02:
  00432f02  885e0b                mov       [esi+0Bh],bl
  00432f05  e836421100            call      00547140h
  00432f0a  99                    cdq
  00432f0b  b964000000            mov       ecx,64h
  00432f10  f7f9                  idiv      ecx
  00432f12  8b461c                mov       eax,[esi+1Ch]
  00432f15  83ea32                sub       edx,32h
  00432f18  c1e20c                shl       edx,0Ch
  00432f1b  03c2                  add       eax,edx
  00432f1d  89461c                mov       [esi+1Ch],eax
  00432f20  e81b421100            call      00547140h
  00432f25  99                    cdq
  00432f26  b932000000            mov       ecx,32h
  00432f2b  f7f9                  idiv      ecx
  00432f2d  8b4e20                mov       ecx,[esi+20h]
  00432f30  83ea19                sub       edx,19h
  00432f33  c1e20c                shl       edx,0Ch
  00432f36  03ca                  add       ecx,edx
  00432f38  894e20                mov       [esi+20h],ecx
  00432f3b  e800421100            call      00547140h
  00432f40  99                    cdq
  00432f41  b964000000            mov       ecx,64h
  00432f46  f7f9                  idiv      ecx
  00432f48  8b4624                mov       eax,[esi+24h]
  00432f4b  83ea32                sub       edx,32h
  00432f4e  c1e20c                shl       edx,0Ch
  00432f51  03c2                  add       eax,edx
  00432f53  894624                mov       [esi+24h],eax
  00432f56  e8e5411100            call      00547140h
  00432f5b  99                    cdq
  00432f5c  33c2                  xor       eax,edx
  00432f5e  2bc2                  sub       eax,edx
  00432f60  83e00f                and       eax,0Fh
  00432f63  33c2                  xor       eax,edx
  00432f65  2bc2                  sub       eax,edx
  00432f67  83e808                sub       eax,8
  00432f6a  c1e00c                shl       eax,0Ch
  00432f6d  894650                mov       [esi+50h],eax
  00432f70  e8cb411100            call      00547140h
  00432f75  99                    cdq
  00432f76  b90a000000            mov       ecx,0Ah
  00432f7b  f7f9                  idiv      ecx
  00432f7d  83c206                add       edx,6
  00432f80  c1e20c                shl       edx,0Ch
  00432f83  895654                mov       [esi+54h],edx
  00432f86  e8b5411100            call      00547140h
  00432f8b  99                    cdq
  00432f8c  33c2                  xor       eax,edx
  00432f8e  2bc2                  sub       eax,edx
  00432f90  83e00f                and       eax,0Fh
  00432f93  33c2                  xor       eax,edx
  00432f95  2bc2                  sub       eax,edx
  00432f97  83e808                sub       eax,8
  00432f9a  c1e00c                shl       eax,0Ch
  00432f9d  894658                mov       [esi+58h],eax
  00432fa0  e89b411100            call      00547140h
  00432fa5  99                    cdq
  00432fa6  33c2                  xor       eax,edx
  00432fa8  2bc2                  sub       eax,edx
  00432faa  25ff070000            and       eax,7FFh
  00432faf  33c2                  xor       eax,edx
  00432fb1  2bc2                  sub       eax,edx
  00432fb3  0500080000            add       eax,800h
  00432fb8  66894628              mov       [esi+28h],ax
  00432fbc  e87f411100            call      00547140h
  00432fc1  99                    cdq
  00432fc2  33c2                  xor       eax,edx
  00432fc4  66c746100100          mov       word [esi+10h],1
  00432fca  2bc2                  sub       eax,edx
  00432fcc  66895e12              mov       [esi+12h],bx
  00432fd0  25ff070000            and       eax,7FFh
  00432fd5  885e40                mov       [esi+40h],bl
  00432fd8  33c2                  xor       eax,edx
  00432fda  2bc2                  sub       eax,edx
  00432fdc  0500080000            add       eax,800h
  00432fe1  6689462a              mov       [esi+2Ah],ax

LAB_00432fe5:
  00432fe5  66837e1001            cmp       word [esi+10h],1
  00432fea  7553                  jne       short 0043303Fh
  00432fec  8b5650                mov       edx,[esi+50h]
  00432fef  8b4e1c                mov       ecx,[esi+1Ch]
  00432ff2  8b4620                mov       eax,[esi+20h]
  00432ff5  03ca                  add       ecx,edx
  00432ff7  8b5e24                mov       ebx,[esi+24h]
  00432ffa  894e1c                mov       [esi+1Ch],ecx
  00432ffd  8b4e54                mov       ecx,[esi+54h]
  00433000  03c1                  add       eax,ecx
  00433002  81c100f0ffff          add       ecx,0FFFFF000h
  00433008  894620                mov       [esi+20h],eax
  0043300b  8b4658                mov       eax,[esi+58h]
  0043300e  03d8                  add       ebx,eax
  00433010  66ff4612              inc       word [esi+12h]
  00433014  668b4612              mov       ax,[esi+12h]
  00433018  895e24                mov       [esi+24h],ebx
  0043301b  663d1800              cmp       ax,18h
  0043301f  894e54                mov       [esi+54h],ecx
  00433022  7506                  jne       short 0043302Ah
  00433024  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043302a:
  0043302a  663d0600              cmp       ax,6
  0043302e  740c                  je        short 0043303Ch
  00433030  663d0c00              cmp       ax,0Ch
  00433034  7406                  je        short 0043303Ch
  00433036  663d1200              cmp       ax,12h
  0043303a  7503                  jne       short 0043303Fh

LAB_0043303c:
  0043303c  fe4640                inc       byte [esi+40h]

LAB_0043303f:
  0043303f  5e                    pop       esi
  00433040  5b                    pop       ebx
  00433041  c3                    ret
  00433042  90                    nop
  00433043  90                    nop
  00433044  90                    nop
  00433045  90                    nop
  00433046  90                    nop
  00433047  90                    nop
  00433048  90                    nop
  00433049  90                    nop
  0043304a  90                    nop
  0043304b  90                    nop
  0043304c  90                    nop
  0043304d  90                    nop
  0043304e  90                    nop
  0043304f  90                    nop

6 cible(s) de saut interne : 0x00432efe, 0x00432f02, 0x00432fe5, 0x0043302a, 0x0043303c, 0x0043303f
