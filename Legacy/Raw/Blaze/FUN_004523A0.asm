Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004523a0  section .text  offset fichier 0x517a0  112 octets

  004523a0  8b442404              mov       eax,[esp+4]
  004523a4  33d2                  xor       edx,edx
  004523a6  56                    push      esi
  004523a7  b901000000            mov       ecx,1
  004523ac  66395010              cmp       [eax+10h],dx
  004523b0  7512                  jne       short 004523C4h
  004523b2  8b30                  mov       esi,[eax]
  004523b4  66894810              mov       [eax+10h],cx
  004523b8  81ce00400000          or        esi,4000h
  004523be  66895012              mov       [eax+12h],dx
  004523c2  8930                  mov       [eax],esi

LAB_004523c4:
  004523c4  66394810              cmp       [eax+10h],cx
  004523c8  753c                  jne       short 00452406h
  004523ca  66ff4012              inc       word [eax+12h]
  004523ce  668378120f            cmp       word [eax+12h],0Fh
  004523d3  7531                  jne       short 00452406h
  004523d5  66895012              mov       [eax+12h],dx
  004523d9  8b0dcc975e00          mov       ecx,[5E97CCh]
  004523df  52                    push      edx
  004523e0  68dc050000            push      5DCh
  004523e5  8b91a8000000          mov       edx,[ecx+0A8h]
  004523eb  6800100000            push      1000h
  004523f0  6800080000            push      800h
  004523f5  8b0a                  mov       ecx,[edx]
  004523f7  660fb65113            movzx     dx,byte [ecx+13h]
  004523fc  52                    push      edx
  004523fd  50                    push      eax
  004523fe  e8dd5c0000            call      004580E0h
  00452403  83c418                add       esp,18h

LAB_00452406:
  00452406  5e                    pop       esi
  00452407  c3                    ret
