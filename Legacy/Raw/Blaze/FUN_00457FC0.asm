Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00457fc0  section .text  offset fichier 0x573c0  172 octets

  00457fc0  56                    push      esi
  00457fc1  8b742408              mov       esi,[esp+8]
  00457fc5  b801000000            mov       eax,1
  00457fca  66837e1000            cmp       word [esi+10h],0
  00457fcf  751c                  jne       short 00457FEDh
  00457fd1  8b4e1c                mov       ecx,[esi+1Ch]
  00457fd4  8b5620                mov       edx,[esi+20h]
  00457fd7  894e44                mov       [esi+44h],ecx
  00457fda  8b4e24                mov       ecx,[esi+24h]
  00457fdd  895648                mov       [esi+48h],edx
  00457fe0  894e4c                mov       [esi+4Ch],ecx
  00457fe3  66894610              mov       [esi+10h],ax
  00457fe7  66c746120000          mov       word [esi+12h],0

LAB_00457fed:
  00457fed  66394610              cmp       [esi+10h],ax
  00457ff1  7577                  jne       short 0045806Ah
  00457ff3  8b5644                mov       edx,[esi+44h]
  00457ff6  8b4648                mov       eax,[esi+48h]
  00457ff9  8b4e4c                mov       ecx,[esi+4Ch]
  00457ffc  89561c                mov       [esi+1Ch],edx
  00457fff  894620                mov       [esi+20h],eax
  00458002  894e24                mov       [esi+24h],ecx
  00458005  e836f10e00            call      00547140h
  0045800a  99                    cdq
  0045800b  b932000000            mov       ecx,32h
  00458010  f7f9                  idiv      ecx
  00458012  8b461c                mov       eax,[esi+1Ch]
  00458015  83ea19                sub       edx,19h
  00458018  c1e20c                shl       edx,0Ch
  0045801b  03c2                  add       eax,edx
  0045801d  89461c                mov       [esi+1Ch],eax
  00458020  e81bf10e00            call      00547140h
  00458025  99                    cdq
  00458026  b932000000            mov       ecx,32h
  0045802b  f7f9                  idiv      ecx
  0045802d  8b4e20                mov       ecx,[esi+20h]
  00458030  83ea19                sub       edx,19h
  00458033  c1e20c                shl       edx,0Ch
  00458036  03ca                  add       ecx,edx
  00458038  894e20                mov       [esi+20h],ecx
  0045803b  e800f10e00            call      00547140h
  00458040  99                    cdq
  00458041  b932000000            mov       ecx,32h
  00458046  f7f9                  idiv      ecx
  00458048  8b4624                mov       eax,[esi+24h]
  0045804b  83ea19                sub       edx,19h
  0045804e  c1e20c                shl       edx,0Ch
  00458051  03c2                  add       eax,edx
  00458053  894624                mov       [esi+24h],eax
  00458056  e8e5f00e00            call      00547140h
  0045805b  99                    cdq
  0045805c  33c2                  xor       eax,edx
  0045805e  2bc2                  sub       eax,edx
  00458060  83e003                and       eax,3
  00458063  33c2                  xor       eax,edx
  00458065  2bc2                  sub       eax,edx
  00458067  884640                mov       [esi+40h],al

LAB_0045806a:
  0045806a  5e                    pop       esi
  0045806b  c3                    ret

2 cible(s) de saut interne : 0x00457fed, 0x0045806a
