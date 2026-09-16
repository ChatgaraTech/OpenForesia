Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00454b40  section .text  offset fichier 0x53f40  112 octets

  00454b40  8b442404              mov       eax,[esp+4]
  00454b44  33d2                  xor       edx,edx
  00454b46  56                    push      esi
  00454b47  b901000000            mov       ecx,1
  00454b4c  66395010              cmp       [eax+10h],dx
  00454b50  7512                  jne       short 00454B64h
  00454b52  8b30                  mov       esi,[eax]
  00454b54  66894810              mov       [eax+10h],cx
  00454b58  81ce00400000          or        esi,4000h
  00454b5e  66895012              mov       [eax+12h],dx
  00454b62  8930                  mov       [eax],esi

LAB_00454b64:
  00454b64  66394810              cmp       [eax+10h],cx
  00454b68  753c                  jne       short 00454BA6h
  00454b6a  66ff4012              inc       word [eax+12h]
  00454b6e  668378120f            cmp       word [eax+12h],0Fh
  00454b73  7531                  jne       short 00454BA6h
  00454b75  66895012              mov       [eax+12h],dx
  00454b79  8b0dcc975e00          mov       ecx,[5E97CCh]
  00454b7f  52                    push      edx
  00454b80  68dc050000            push      5DCh
  00454b85  8b91a8000000          mov       edx,[ecx+0A8h]
  00454b8b  6800100000            push      1000h
  00454b90  6800080000            push      800h
  00454b95  8b0a                  mov       ecx,[edx]
  00454b97  660fb65113            movzx     dx,byte [ecx+13h]
  00454b9c  52                    push      edx
  00454b9d  50                    push      eax
  00454b9e  e83d350000            call      004580E0h
  00454ba3  83c418                add       esp,18h

LAB_00454ba6:
  00454ba6  5e                    pop       esi
  00454ba7  c3                    ret
