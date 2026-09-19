Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042fcc0  section .text  offset fichier 0x2f0c0  240 octets

  0042fcc0  53                    push      ebx
  0042fcc1  56                    push      esi
  0042fcc2  8b74240c              mov       esi,[esp+0Ch]
  0042fcc6  33db                  xor       ebx,ebx
  0042fcc8  66395e10              cmp       [esi+10h],bx
  0042fccc  0f85a8000000          jne       near 0042FD7Ah
  0042fcd2  66c746100100          mov       word [esi+10h],1
  0042fcd8  c7463cf0095500        mov       dword [esi+3Ch],5509F0h
  0042fcdf  c6460a09              mov       byte [esi+0Ah],9
  0042fce3  885e0b                mov       [esi+0Bh],bl
  0042fce6  e855741100            call      00547140h
  0042fceb  99                    cdq
  0042fcec  b928000000            mov       ecx,28h
  0042fcf1  f7f9                  idiv      ecx
  0042fcf3  8b461c                mov       eax,[esi+1Ch]
  0042fcf6  83ea14                sub       edx,14h
  0042fcf9  c1e20c                shl       edx,0Ch
  0042fcfc  03c2                  add       eax,edx
  0042fcfe  89461c                mov       [esi+1Ch],eax
  0042fd01  e83a741100            call      00547140h
  0042fd06  99                    cdq
  0042fd07  b928000000            mov       ecx,28h
  0042fd0c  f7f9                  idiv      ecx
  0042fd0e  8b4e24                mov       ecx,[esi+24h]
  0042fd11  83ea14                sub       edx,14h
  0042fd14  c1e20c                shl       edx,0Ch
  0042fd17  03ca                  add       ecx,edx
  0042fd19  894e24                mov       [esi+24h],ecx
  0042fd1c  e81f741100            call      00547140h
  0042fd21  99                    cdq
  0042fd22  b928000000            mov       ecx,28h
  0042fd27  f7f9                  idiv      ecx
  0042fd29  8b4620                mov       eax,[esi+20h]
  0042fd2c  83ea14                sub       edx,14h
  0042fd2f  c1e20c                shl       edx,0Ch
  0042fd32  03c2                  add       eax,edx
  0042fd34  894620                mov       [esi+20h],eax
  0042fd37  e804741100            call      00547140h
  0042fd3c  99                    cdq
  0042fd3d  33c2                  xor       eax,edx
  0042fd3f  2bc2                  sub       eax,edx
  0042fd41  25ff070000            and       eax,7FFh
  0042fd46  33c2                  xor       eax,edx
  0042fd48  2bc2                  sub       eax,edx
  0042fd4a  ba00140000            mov       edx,1400h
  0042fd4f  2bd0                  sub       edx,eax
  0042fd51  66895628              mov       [esi+28h],dx
  0042fd55  e8e6731100            call      00547140h
  0042fd5a  99                    cdq
  0042fd5b  33c2                  xor       eax,edx
  0042fd5d  b900140000            mov       ecx,1400h
  0042fd62  2bc2                  sub       eax,edx
  0042fd64  66895e12              mov       [esi+12h],bx
  0042fd68  25ff070000            and       eax,7FFh
  0042fd6d  885e40                mov       [esi+40h],bl
  0042fd70  33c2                  xor       eax,edx
  0042fd72  2bc2                  sub       eax,edx
  0042fd74  2bc8                  sub       ecx,eax
  0042fd76  66894e2a              mov       [esi+2Ah],cx

LAB_0042fd7a:
  0042fd7a  66837e1001            cmp       word [esi+10h],1
  0042fd7f  7523                  jne       short 0042FDA4h
  0042fd81  66ff4612              inc       word [esi+12h]
  0042fd85  66837e1202            cmp       word [esi+12h],2
  0042fd8a  7518                  jne       short 0042FDA4h
  0042fd8c  8a5640                mov       dl,[esi+40h]
  0042fd8f  66895e12              mov       [esi+12h],bx
  0042fd93  fec2                  inc       dl
  0042fd95  8ac2                  mov       al,dl
  0042fd97  885640                mov       [esi+40h],dl
  0042fd9a  3c04                  cmp       al,4
  0042fd9c  7506                  jne       short 0042FDA4h
  0042fd9e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042fda4:
  0042fda4  5e                    pop       esi
  0042fda5  5b                    pop       ebx
  0042fda6  c3                    ret
  0042fda7  90                    nop
  0042fda8  90                    nop
  0042fda9  90                    nop
  0042fdaa  90                    nop
  0042fdab  90                    nop
  0042fdac  90                    nop
  0042fdad  90                    nop
  0042fdae  90                    nop
  0042fdaf  90                    nop

2 cible(s) de saut interne : 0x0042fd7a, 0x0042fda4
