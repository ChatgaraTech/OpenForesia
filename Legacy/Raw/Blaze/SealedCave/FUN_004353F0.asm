Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004353f0  section .text  offset fichier 0x347f0  176 octets

  004353f0  56                    push      esi
  004353f1  8b742408              mov       esi,[esp+8]
  004353f5  8b466c                mov       eax,[esi+6Ch]
  004353f8  85c0                  test      eax,eax
  004353fa  7505                  jne       short 00435401h
  004353fc  b858e55d00            mov       eax,5DE558h

LAB_00435401:
  00435401  8b481c                mov       ecx,[eax+1Ch]
  00435404  66837e1000            cmp       word [esi+10h],0
  00435409  894e1c                mov       [esi+1Ch],ecx
  0043540c  8b5020                mov       edx,[eax+20h]
  0043540f  895620                mov       [esi+20h],edx
  00435412  8b4024                mov       eax,[eax+24h]
  00435415  894624                mov       [esi+24h],eax
  00435418  b801000000            mov       eax,1
  0043541d  751d                  jne       short 0043543Ch
  0043541f  66894610              mov       [esi+10h],ax
  00435423  c7463c680e5500        mov       dword [esi+3Ch],550E68h
  0043542a  c6464000              mov       byte [esi+40h],0
  0043542e  c6460a09              mov       byte [esi+0Ah],9
  00435432  c6460b00              mov       byte [esi+0Bh],0
  00435436  66c746120a00          mov       word [esi+12h],0Ah

LAB_0043543c:
  0043543c  66394610              cmp       [esi+10h],ax
  00435440  7559                  jne       short 0043549Bh
  00435442  66ff4e12              dec       word [esi+12h]
  00435446  e8f51c1100            call      00547140h
  0043544b  99                    cdq
  0043544c  33c2                  xor       eax,edx
  0043544e  2bc2                  sub       eax,edx
  00435450  25ff070000            and       eax,7FFh
  00435455  33c2                  xor       eax,edx
  00435457  2bc2                  sub       eax,edx
  00435459  0500100000            add       eax,1000h
  0043545e  66894628              mov       [esi+28h],ax
  00435462  e8d91c1100            call      00547140h
  00435467  99                    cdq
  00435468  33c2                  xor       eax,edx
  0043546a  2bc2                  sub       eax,edx
  0043546c  25ff070000            and       eax,7FFh
  00435471  33c2                  xor       eax,edx
  00435473  2bc2                  sub       eax,edx
  00435475  0500100000            add       eax,1000h
  0043547a  6689462c              mov       [esi+2Ch],ax
  0043547e  e8bd1c1100            call      00547140h
  00435483  99                    cdq
  00435484  b903000000            mov       ecx,3
  00435489  f7f9                  idiv      ecx
  0043548b  66837e1200            cmp       word [esi+12h],0
  00435490  885640                mov       [esi+40h],dl
  00435493  7f06                  jg        short 0043549Bh
  00435495  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043549b:
  0043549b  5e                    pop       esi
  0043549c  c3                    ret
  0043549d  90                    nop
  0043549e  90                    nop
  0043549f  90                    nop

3 cible(s) de saut interne : 0x00435401, 0x0043543c, 0x0043549b
