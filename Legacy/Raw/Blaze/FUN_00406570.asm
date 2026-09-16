
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00406570  section .text  offset fichier 0x5970  96 octets

  00406570  8b442404              mov       eax,[esp+4]
  00406574  8b486c                mov       ecx,[eax+6Ch]
  00406577  85c9                  test      ecx,ecx
  00406579  7412                  je        short 0040658Dh
  0040657b  8b511c                mov       edx,[ecx+1Ch]
  0040657e  89501c                mov       [eax+1Ch],edx
  00406581  8b5160                mov       edx,[ecx+60h]
  00406584  895020                mov       [eax+20h],edx
  00406587  8b4924                mov       ecx,[ecx+24h]
  0040658a  894824                mov       [eax+24h],ecx

LAB_0040658d:
  0040658d  6683781000            cmp       word [eax+10h],0
  00406592  ba01000000            mov       edx,1
  00406597  7528                  jne       short 004065C1h
  00406599  b120                  mov       cl,20h
  0040659b  66895010              mov       [eax+10h],dx
  0040659f  c7403cc0f25400        mov       dword [eax+3Ch],54F2C0h
  004065a6  c6404000              mov       byte [eax+40h],0
  004065aa  c6400a03              mov       byte [eax+0Ah],3
  004065ae  c6400b00              mov       byte [eax+0Bh],0
  004065b2  66c7402ef6ff          mov       word [eax+2Eh],0FFF6h
  004065b8  884838                mov       [eax+38h],cl
  004065bb  884839                mov       [eax+39h],cl
  004065be  88483a                mov       [eax+3Ah],cl

LAB_004065c1:
  004065c1  66395010              cmp       [eax+10h],dx
  004065c5  7506                  jne       short 004065CDh
  004065c7  8b5060                mov       edx,[eax+60h]
  004065ca  895020                mov       [eax+20h],edx

LAB_004065cd:
  004065cd  c3                    ret
  004065ce  90                    nop
  004065cf  90                    nop

3 cible(s) de saut interne : 0x0040658d, 0x004065c1, 0x004065cd


