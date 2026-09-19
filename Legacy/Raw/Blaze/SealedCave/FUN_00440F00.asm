Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00440f00  section .text  offset fichier 0x40300  320 octets

  00440f00  53                    push      ebx
  00440f01  56                    push      esi
  00440f02  8b74240c              mov       esi,[esp+0Ch]
  00440f06  33db                  xor       ebx,ebx
  00440f08  57                    push      edi
  00440f09  bf01000000            mov       edi,1
  00440f0e  66395e10              cmp       [esi+10h],bx
  00440f12  0f8589000000          jne       near 00440FA1h
  00440f18  c7463cf0145500        mov       dword [esi+3Ch],5514F0h
  00440f1f  885e40                mov       [esi+40h],bl
  00440f22  e819621000            call      00547140h
  00440f27  99                    cdq
  00440f28  33c2                  xor       eax,edx
  00440f2a  66897e10              mov       [esi+10h],di
  00440f2e  2bc2                  sub       eax,edx
  00440f30  25ff030000            and       eax,3FFh
  00440f35  33c2                  xor       eax,edx
  00440f37  2bc2                  sub       eax,edx
  00440f39  0500060000            add       eax,600h
  00440f3e  66014632              add       [esi+32h],ax
  00440f42  e8f9611000            call      00547140h
  00440f47  99                    cdq
  00440f48  b91e000000            mov       ecx,1Eh
  00440f4d  66895e14              mov       [esi+14h],bx
  00440f51  f7f9                  idiv      ecx
  00440f53  83ea0f                sub       edx,0Fh
  00440f56  66895612              mov       [esi+12h],dx
  00440f5a  e8e1611000            call      00547140h
  00440f5f  99                    cdq
  00440f60  b905000000            mov       ecx,5
  00440f65  66899e9a000000        mov       [esi+9Ah],bx
  00440f6c  f7f9                  idiv      ecx
  00440f6e  8b4620                mov       eax,[esi+20h]
  00440f71  c6460a09              mov       byte [esi+0Ah],9
  00440f75  0500c01200            add       eax,12C000h
  00440f7a  885e0b                mov       [esi+0Bh],bl
  00440f7d  894620                mov       [esi+20h],eax
  00440f80  83c203                add       edx,3
  00440f83  89565c                mov       [esi+5Ch],edx
  00440f86  e8b5611000            call      00547140h
  00440f8b  99                    cdq
  00440f8c  b9dc050000            mov       ecx,5DCh
  00440f91  f7f9                  idiv      ecx
  00440f93  8d82d0070000          lea       eax,[edx+7D0h]
  00440f99  66894628              mov       [esi+28h],ax
  00440f9d  6689462a              mov       [esi+2Ah],ax

LAB_00440fa1:
  00440fa1  66397e10              cmp       [esi+10h],di
  00440fa5  0f8587000000          jne       near 00441032h
  00440fab  0fbf5632              movsx     edx,word [esi+32h]
  00440faf  52                    push      edx
  00440fb0  e8674a1000            call      00545A1Ch
  00440fb5  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00440fbc  0fafc1                imul      eax,ecx
  00440fbf  0fbf5632              movsx     edx,word [esi+32h]
  00440fc3  8b4e1c                mov       ecx,[esi+1Ch]
  00440fc6  668bbe9a000000        mov       di,[esi+9Ah]
  00440fcd  03c8                  add       ecx,eax
  00440fcf  52                    push      edx
  00440fd0  894e1c                mov       [esi+1Ch],ecx
  00440fd3  e83e4a1000            call      00545A16h
  00440fd8  0fbfcf                movsx     ecx,di
  00440fdb  0fafc1                imul      eax,ecx
  00440fde  8b5624                mov       edx,[esi+24h]
  00440fe1  03d0                  add       edx,eax
  00440fe3  8b4620                mov       eax,[esi+20h]
  00440fe6  895624                mov       [esi+24h],edx
  00440fe9  0fbf5612              movsx     edx,word [esi+12h]
  00440fed  c1e20c                shl       edx,0Ch
  00440ff0  03c2                  add       eax,edx
  00440ff2  894620                mov       [esi+20h],eax
  00440ff5  668b465c              mov       ax,[esi+5Ch]
  00440ff9  6603c7                add       ax,di
  00440ffc  6689869a000000        mov       [esi+9Ah],ax
  00441003  668b4614              mov       ax,[esi+14h]
  00441007  663d0400              cmp       ax,4
  0044100b  7c25                  jl        short 00441032h
  0044100d  663d0500              cmp       ax,5
  00441011  7f03                  jg        short 00441016h
  00441013  ff4e5c                dec       dword [esi+5Ch]

LAB_00441016:
  00441016  6af6                  push      0FFFFFFF6h
  00441018  6af6                  push      0FFFFFFF6h
  0044101a  6af6                  push      0FFFFFFF6h
  0044101c  56                    push      esi
  0044101d  e8be3dfcff            call      00404DE0h
  00441022  8a4638                mov       al,[esi+38h]
  00441025  83c410                add       esp,10h
  00441028  3ac3                  cmp       al,bl
  0044102a  7506                  jne       short 00441032h
  0044102c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00441032:
  00441032  66ff4616              inc       word [esi+16h]
  00441036  66ff4614              inc       word [esi+14h]
  0044103a  5f                    pop       edi
  0044103b  5e                    pop       esi
  0044103c  5b                    pop       ebx
  0044103d  c3                    ret
  0044103e  90                    nop
  0044103f  90                    nop

3 cible(s) de saut interne : 0x00440fa1, 0x00441016, 0x00441032
