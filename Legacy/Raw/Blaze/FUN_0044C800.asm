Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044c800  section .text  offset fichier 0x4bc00  145 octets

  0044c800  53                    push      ebx
  0044c801  56                    push      esi
  0044c802  8b74240c              mov       esi,[esp+0Ch]
  0044c806  33db                  xor       ebx,ebx
  0044c808  8b4678                mov       eax,[esi+78h]
  0044c80b  3bc3                  cmp       eax,ebx
  0044c80d  7505                  jne       short 0044C814h
  0044c80f  b858e55d00            mov       eax,5DE558h

LAB_0044c814:
  0044c814  8b481c                mov       ecx,[eax+1Ch]
  0044c817  894e1c                mov       [esi+1Ch],ecx
  0044c81a  8b5060                mov       edx,[eax+60h]
  0044c81d  81c200a00000          add       edx,0A000h
  0044c823  66395e10              cmp       [esi+10h],bx
  0044c827  895620                mov       [esi+20h],edx
  0044c82a  8b4024                mov       eax,[eax+24h]
  0044c82d  894624                mov       [esi+24h],eax
  0044c830  b801000000            mov       eax,1
  0044c835  7522                  jne       short 0044C859h
  0044c837  c646040d              mov       byte [esi+4],0Dh
  0044c83b  c6460a09              mov       byte [esi+0Ah],9
  0044c83f  885e0b                mov       [esi+0Bh],bl
  0044c842  66894610              mov       [esi+10h],ax
  0044c846  885e38                mov       [esi+38h],bl
  0044c849  885e39                mov       [esi+39h],bl
  0044c84c  885e3a                mov       [esi+3Ah],bl
  0044c84f  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  0044c855  66895e16              mov       [esi+16h],bx

LAB_0044c859:
  0044c859  66394610              cmp       [esi+10h],ax
  0044c85d  752f                  jne       short 0044C88Eh
  0044c85f  66837e1604            cmp       word [esi+16h],4
  0044c864  7f08                  jg        short 0044C86Eh
  0044c866  6a3c                  push      3Ch
  0044c868  6a3c                  push      3Ch
  0044c86a  6a3c                  push      3Ch
  0044c86c  eb06                  jmp       short 0044C874h

LAB_0044c86e:
  0044c86e  6afa                  push      0FFFFFFFAh
  0044c870  6af8                  push      0FFFFFFF8h
  0044c872  6af8                  push      0FFFFFFF8h

LAB_0044c874:
  0044c874  56                    push      esi
  0044c875  e86685fbff            call      00404DE0h
  0044c87a  8a463a                mov       al,[esi+3Ah]
  0044c87d  83c410                add       esp,10h
  0044c880  3ac3                  cmp       al,bl
  0044c882  7506                  jne       short 0044C88Ah
  0044c884  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044c88a:
  0044c88a  66ff4616              inc       word [esi+16h]

LAB_0044c88e:
  0044c88e  5e                    pop       esi
  0044c88f  5b                    pop       ebx
  0044c890  c3                    ret

6 cible(s) de saut interne : 0x0044c814, 0x0044c859, 0x0044c86e, 0x0044c874, 0x0044c88a, 0x0044c88e
