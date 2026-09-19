Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00433850  section .text  offset fichier 0x32c50  176 octets

  00433850  56                    push      esi
  00433851  8b742408              mov       esi,[esp+8]
  00433855  8b466c                mov       eax,[esi+6Ch]
  00433858  85c0                  test      eax,eax
  0043385a  7505                  jne       short 00433861h
  0043385c  b858e55d00            mov       eax,5DE558h

LAB_00433861:
  00433861  8b481c                mov       ecx,[eax+1Ch]
  00433864  66837e1000            cmp       word [esi+10h],0
  00433869  894e1c                mov       [esi+1Ch],ecx
  0043386c  8b5020                mov       edx,[eax+20h]
  0043386f  895620                mov       [esi+20h],edx
  00433872  8b4024                mov       eax,[eax+24h]
  00433875  894624                mov       [esi+24h],eax
  00433878  b801000000            mov       eax,1
  0043387d  751d                  jne       short 0043389Ch
  0043387f  66894610              mov       [esi+10h],ax
  00433883  c7463c580d5500        mov       dword [esi+3Ch],550D58h
  0043388a  c6464000              mov       byte [esi+40h],0
  0043388e  c6460a09              mov       byte [esi+0Ah],9
  00433892  c6460b00              mov       byte [esi+0Bh],0
  00433896  66c746120a00          mov       word [esi+12h],0Ah

LAB_0043389c:
  0043389c  66394610              cmp       [esi+10h],ax
  004338a0  7559                  jne       short 004338FBh
  004338a2  66ff4e12              dec       word [esi+12h]
  004338a6  e895381100            call      00547140h
  004338ab  99                    cdq
  004338ac  33c2                  xor       eax,edx
  004338ae  2bc2                  sub       eax,edx
  004338b0  25ff070000            and       eax,7FFh
  004338b5  33c2                  xor       eax,edx
  004338b7  2bc2                  sub       eax,edx
  004338b9  0500100000            add       eax,1000h
  004338be  66894628              mov       [esi+28h],ax
  004338c2  e879381100            call      00547140h
  004338c7  99                    cdq
  004338c8  33c2                  xor       eax,edx
  004338ca  2bc2                  sub       eax,edx
  004338cc  25ff070000            and       eax,7FFh
  004338d1  33c2                  xor       eax,edx
  004338d3  2bc2                  sub       eax,edx
  004338d5  0500100000            add       eax,1000h
  004338da  6689462c              mov       [esi+2Ch],ax
  004338de  e85d381100            call      00547140h
  004338e3  99                    cdq
  004338e4  b903000000            mov       ecx,3
  004338e9  f7f9                  idiv      ecx
  004338eb  66837e1200            cmp       word [esi+12h],0
  004338f0  885640                mov       [esi+40h],dl
  004338f3  7f06                  jg        short 004338FBh
  004338f5  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004338fb:
  004338fb  5e                    pop       esi
  004338fc  c3                    ret
  004338fd  90                    nop
  004338fe  90                    nop
  004338ff  90                    nop

3 cible(s) de saut interne : 0x00433861, 0x0043389c, 0x004338fb
