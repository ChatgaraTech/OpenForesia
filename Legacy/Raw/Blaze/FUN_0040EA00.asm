Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040ea00  section .text  offset fichier 0xde00  240 octets

  0040ea00  53                    push      ebx
  0040ea01  56                    push      esi
  0040ea02  8b74240c              mov       esi,[esp+0Ch]
  0040ea06  33db                  xor       ebx,ebx
  0040ea08  66395e10              cmp       [esi+10h],bx
  0040ea0c  0f85a8000000          jne       near 0040EABAh
  0040ea12  66c746100100          mov       word [esi+10h],1
  0040ea18  c7463cb8f65400        mov       dword [esi+3Ch],54F6B8h
  0040ea1f  c6460a09              mov       byte [esi+0Ah],9
  0040ea23  885e0b                mov       [esi+0Bh],bl
  0040ea26  e815871300            call      00547140h
  0040ea2b  99                    cdq
  0040ea2c  b928000000            mov       ecx,28h
  0040ea31  f7f9                  idiv      ecx
  0040ea33  8b461c                mov       eax,[esi+1Ch]
  0040ea36  83ea14                sub       edx,14h
  0040ea39  c1e20c                shl       edx,0Ch
  0040ea3c  03c2                  add       eax,edx
  0040ea3e  89461c                mov       [esi+1Ch],eax
  0040ea41  e8fa861300            call      00547140h
  0040ea46  99                    cdq
  0040ea47  b928000000            mov       ecx,28h
  0040ea4c  f7f9                  idiv      ecx
  0040ea4e  8b4e24                mov       ecx,[esi+24h]
  0040ea51  83ea14                sub       edx,14h
  0040ea54  c1e20c                shl       edx,0Ch
  0040ea57  03ca                  add       ecx,edx
  0040ea59  894e24                mov       [esi+24h],ecx
  0040ea5c  e8df861300            call      00547140h
  0040ea61  99                    cdq
  0040ea62  b928000000            mov       ecx,28h
  0040ea67  f7f9                  idiv      ecx
  0040ea69  8b4620                mov       eax,[esi+20h]
  0040ea6c  83ea14                sub       edx,14h
  0040ea6f  c1e20c                shl       edx,0Ch
  0040ea72  03c2                  add       eax,edx
  0040ea74  894620                mov       [esi+20h],eax
  0040ea77  e8c4861300            call      00547140h
  0040ea7c  99                    cdq
  0040ea7d  33c2                  xor       eax,edx
  0040ea7f  2bc2                  sub       eax,edx
  0040ea81  25ff070000            and       eax,7FFh
  0040ea86  33c2                  xor       eax,edx
  0040ea88  2bc2                  sub       eax,edx
  0040ea8a  ba00140000            mov       edx,1400h
  0040ea8f  2bd0                  sub       edx,eax
  0040ea91  66895628              mov       [esi+28h],dx
  0040ea95  e8a6861300            call      00547140h
  0040ea9a  99                    cdq
  0040ea9b  33c2                  xor       eax,edx
  0040ea9d  b900140000            mov       ecx,1400h
  0040eaa2  2bc2                  sub       eax,edx
  0040eaa4  66895e12              mov       [esi+12h],bx
  0040eaa8  25ff070000            and       eax,7FFh
  0040eaad  885e40                mov       [esi+40h],bl
  0040eab0  33c2                  xor       eax,edx
  0040eab2  2bc2                  sub       eax,edx
  0040eab4  2bc8                  sub       ecx,eax
  0040eab6  66894e2a              mov       [esi+2Ah],cx

LAB_0040eaba:
  0040eaba  66837e1001            cmp       word [esi+10h],1
  0040eabf  7523                  jne       short 0040EAE4h
  0040eac1  66ff4612              inc       word [esi+12h]
  0040eac5  66837e1202            cmp       word [esi+12h],2
  0040eaca  7518                  jne       short 0040EAE4h
  0040eacc  8a5640                mov       dl,[esi+40h]
  0040eacf  66895e12              mov       [esi+12h],bx
  0040ead3  fec2                  inc       dl
  0040ead5  8ac2                  mov       al,dl
  0040ead7  885640                mov       [esi+40h],dl
  0040eada  3c04                  cmp       al,4
  0040eadc  7506                  jne       short 0040EAE4h
  0040eade  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040eae4:
  0040eae4  5e                    pop       esi
  0040eae5  5b                    pop       ebx
  0040eae6  c3                    ret
  0040eae7  90                    nop
  0040eae8  90                    nop
  0040eae9  90                    nop
  0040eaea  90                    nop
  0040eaeb  90                    nop
  0040eaec  90                    nop
  0040eaed  90                    nop
  0040eaee  90                    nop
  0040eaef  90                    nop

2 cible(s) de saut interne : 0x0040eaba, 0x0040eae4
