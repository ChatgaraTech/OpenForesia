Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470be0  section .text  offset fichier 0x6ffe0  64 octets

  00470be0  66833df2a55f0002      cmp       word [5FA5F2h],2
  00470be8  8b442404              mov       eax,[esp+4]
  00470bec  56                    push      esi
  00470bed  7409                  je        short 00470BF8h
  00470bef  8b08                  mov       ecx,[eax]
  00470bf1  80cd60                or        ch,60h
  00470bf4  8908                  mov       [eax],ecx
  00470bf6  5e                    pop       esi
  00470bf7  c3                    ret

LAB_00470bf8:
  00470bf8  8b10                  mov       edx,[eax]
  00470bfa  8a4808                mov       cl,[eax+8]
  00470bfd  80e69f                and       dh,9Fh
  00470c00  80f925                cmp       cl,25h
  00470c03  8910                  mov       [eax],edx
  00470c05  7517                  jne       short 00470C1Eh
  00470c07  668b4834              mov       cx,[eax+34h]
  00470c0b  be14050000            mov       esi,514h
  00470c10  0fbfd1                movsx     edx,cx
  00470c13  2bf2                  sub       esi,edx
  00470c15  c1fe03                sar       esi,3
  00470c18  03f1                  add       esi,ecx
  00470c1a  66897034              mov       [eax+34h],si

LAB_00470c1e:
  00470c1e  5e                    pop       esi
  00470c1f  c3                    ret

2 cible(s) de saut interne : 0x00470bf8, 0x00470c1e
