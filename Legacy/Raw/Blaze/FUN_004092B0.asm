Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004092b0  section .text  offset fichier 0x86b0  112 octets

  004092b0  8b442404              mov       eax,[esp+4]
  004092b4  33d2                  xor       edx,edx
  004092b6  56                    push      esi
  004092b7  8b486c                mov       ecx,[eax+6Ch]
  004092ba  3bca                  cmp       ecx,edx
  004092bc  745e                  je        short 0040931Ch
  004092be  8b711c                mov       esi,[ecx+1Ch]
  004092c1  89701c                mov       [eax+1Ch],esi
  004092c4  8b7164                mov       esi,[ecx+64h]
  004092c7  897020                mov       [eax+20h],esi
  004092ca  8b7124                mov       esi,[ecx+24h]
  004092cd  897024                mov       [eax+24h],esi
  004092d0  8b7020                mov       esi,[eax+20h]
  004092d3  81fe000006ff          cmp       esi,0FF060000h
  004092d9  7d06                  jge       short 004092E1h
  004092db  8120ffffff7f          and       dword [eax],7FFFFFFFh

LAB_004092e1:
  004092e1  6683791002            cmp       word [ecx+10h],2
  004092e6  7506                  jne       short 004092EEh
  004092e8  8120ffffff7f          and       dword [eax],7FFFFFFFh

LAB_004092ee:
  004092ee  66395010              cmp       [eax+10h],dx
  004092f2  7528                  jne       short 0040931Ch
  004092f4  b132                  mov       cl,32h
  004092f6  66c740100100          mov       word [eax+10h],1
  004092fc  c7403c18f45400        mov       dword [eax+3Ch],54F418h
  00409303  885040                mov       [eax+40h],dl
  00409306  c6400a03              mov       byte [eax+0Ah],3
  0040930a  88500b                mov       [eax+0Bh],dl
  0040930d  66c7402ef6ff          mov       word [eax+2Eh],0FFF6h
  00409313  884838                mov       [eax+38h],cl
  00409316  884839                mov       [eax+39h],cl
  00409319  88483a                mov       [eax+3Ah],cl

LAB_0040931c:
  0040931c  5e                    pop       esi
  0040931d  c3                    ret
  0040931e  90                    nop
  0040931f  90                    nop

3 cible(s) de saut interne : 0x004092e1, 0x004092ee, 0x0040931c
