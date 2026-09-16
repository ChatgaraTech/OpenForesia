Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004511f0  section .text  offset fichier 0x505f0  96 octets

  004511f0  8b442404              mov       eax,[esp+4]
  004511f4  33c9                  xor       ecx,ecx
  004511f6  66394810              cmp       [eax+10h],cx
  004511fa  7511                  jne       short 0045120Dh
  004511fc  8b10                  mov       edx,[eax]
  004511fe  66c740100100          mov       word [eax+10h],1
  00451204  80ce40                or        dh,40h
  00451207  66894812              mov       [eax+12h],cx
  0045120b  8910                  mov       [eax],edx

LAB_0045120d:
  0045120d  66ff4012              inc       word [eax+12h]
  00451211  668378120f            cmp       word [eax+12h],0Fh
  00451216  7531                  jne       short 00451249h
  00451218  668b5016              mov       dx,[eax+16h]
  0045121c  66894812              mov       [eax+12h],cx
  00451220  51                    push      ecx
  00451221  668b4814              mov       cx,[eax+14h]
  00451225  68dc050000            push      5DCh
  0045122a  51                    push      ecx
  0045122b  8b0dcc975e00          mov       ecx,[5E97CCh]
  00451231  52                    push      edx
  00451232  8b91a8000000          mov       edx,[ecx+0A8h]
  00451238  8b0a                  mov       ecx,[edx]
  0045123a  660fb65113            movzx     dx,byte [ecx+13h]
  0045123f  52                    push      edx
  00451240  50                    push      eax
  00451241  e89a6e0000            call      004580E0h
  00451246  83c418                add       esp,18h

LAB_00451249:
  00451249  c3                    ret
