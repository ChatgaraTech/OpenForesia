Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00454ad0  section .text  offset fichier 0x53ed0  112 octets

  00454ad0  8b442404              mov       eax,[esp+4]
  00454ad4  33d2                  xor       edx,edx
  00454ad6  56                    push      esi
  00454ad7  b901000000            mov       ecx,1
  00454adc  66395010              cmp       [eax+10h],dx
  00454ae0  7512                  jne       short 00454AF4h
  00454ae2  8b30                  mov       esi,[eax]
  00454ae4  66894810              mov       [eax+10h],cx
  00454ae8  81ce00400000          or        esi,4000h
  00454aee  66895012              mov       [eax+12h],dx
  00454af2  8930                  mov       [eax],esi

LAB_00454af4:
  00454af4  66394810              cmp       [eax+10h],cx
  00454af8  753c                  jne       short 00454B36h
  00454afa  66ff4012              inc       word [eax+12h]
  00454afe  668378120a            cmp       word [eax+12h],0Ah
  00454b03  7531                  jne       short 00454B36h
  00454b05  66895012              mov       [eax+12h],dx
  00454b09  8b0dcc975e00          mov       ecx,[5E97CCh]
  00454b0f  52                    push      edx
  00454b10  68d0070000            push      7D0h
  00454b15  8b91a8000000          mov       edx,[ecx+0A8h]
  00454b1b  6800200000            push      2000h
  00454b20  6800040000            push      400h
  00454b25  8b0a                  mov       ecx,[edx]
  00454b27  660fb65116            movzx     dx,byte [ecx+16h]
  00454b2c  52                    push      edx
  00454b2d  50                    push      eax
  00454b2e  e8ad350000            call      004580E0h
  00454b33  83c418                add       esp,18h

LAB_00454b36:
  00454b36  5e                    pop       esi
  00454b37  c3                    ret
