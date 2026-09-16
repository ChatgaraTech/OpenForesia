
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00408490  section .text  offset fichier 0x7890  96 octets

  00408490  8b442404              mov       eax,[esp+4]
  00408494  8b486c                mov       ecx,[eax+6Ch]
  00408497  85c9                  test      ecx,ecx
  00408499  7412                  je        short 004084ADh
  0040849b  8b511c                mov       edx,[ecx+1Ch]
  0040849e  89501c                mov       [eax+1Ch],edx
  004084a1  8b5160                mov       edx,[ecx+60h]
  004084a4  895020                mov       [eax+20h],edx
  004084a7  8b4924                mov       ecx,[ecx+24h]
  004084aa  894824                mov       [eax+24h],ecx

LAB_004084ad:
  004084ad  6683781000            cmp       word [eax+10h],0
  004084b2  ba01000000            mov       edx,1
  004084b7  7528                  jne       short 004084E1h
  004084b9  b196                  mov       cl,96h
  004084bb  66895010              mov       [eax+10h],dx
  004084bf  c7403c18f45400        mov       dword [eax+3Ch],54F418h
  004084c6  c6404000              mov       byte [eax+40h],0
  004084ca  c6400a03              mov       byte [eax+0Ah],3
  004084ce  c6400b00              mov       byte [eax+0Bh],0
  004084d2  66c7402ef6ff          mov       word [eax+2Eh],0FFF6h
  004084d8  884838                mov       [eax+38h],cl
  004084db  884839                mov       [eax+39h],cl
