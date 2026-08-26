Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00458170  section .text  offset fichier 0x57570  133 octets

  00458170  56                    push      esi
  00458171  8b742408              mov       esi,[esp+8]
  00458175  66837e1000            cmp       word [esi+10h],0
  0045817a  754a                  jne       short 004581C6h
  0045817c  f646090f              test      byte [esi+9],0Fh
  00458180  7444                  je        short 004581C6h
  00458182  8b4670                mov       eax,[esi+70h]
  00458185  85c0                  test      eax,eax
  00458187  7505                  jne       short 0045818Eh
  00458189  b858e55d00            mov       eax,5DE558h

LAB_0045818e:
  0045818e  f70000000100          test      dword [eax],10000h
  00458194  7524                  jne       short 004581BAh
  00458196  8b466c                mov       eax,[esi+6Ch]
  00458199  8b15cc975e00          mov       edx,[5E97CCh]
  0045819f  0fbf4e16              movsx     ecx,word [esi+16h]
  004581a3  6a00                  push      0
  004581a5  68b0335600            push      5633B0h
  004581aa  50                    push      eax
  004581ab  8b4270                mov       eax,[edx+70h]
  004581ae  8b0c88                mov       ecx,[eax+ecx*4]
  004581b1  51                    push      ecx
  004581b2  e899c10e00            call      00544350h
  004581b7  83c410                add       esp,10h

LAB_004581ba:
  004581ba  66c746100200          mov       word [esi+10h],2
  004581c0  66c746120300          mov       word [esi+12h],3

LAB_004581c6:
  004581c6  b801000000            mov       eax,1
  004581cb  66394610              cmp       [esi+10h],ax
  004581cf  750c                  jne       short 004581DDh
  004581d1  f646090f              test      byte [esi+9],0Fh
  004581d5  7506                  jne       short 004581DDh
  004581d7  66c746100000          mov       word [esi+10h],0

LAB_004581dd:
  004581dd  66837e1002            cmp       word [esi+10h],2
  004581e2  750f                  jne       short 004581F3h
  004581e4  66ff4e12              dec       word [esi+12h]
  004581e8  66837e1200            cmp       word [esi+12h],0
  004581ed  7f04                  jg        short 004581F3h
  004581ef  66894610              mov       [esi+10h],ax

LAB_004581f3:
  004581f3  5e                    pop       esi
  004581f4  c3                    ret

5 cible(s) de saut interne : 0x0045818e, 0x004581ba, 0x004581c6, 0x004581dd, 0x004581f3
