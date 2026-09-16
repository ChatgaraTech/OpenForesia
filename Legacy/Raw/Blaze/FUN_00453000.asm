Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00453000  section .text  offset fichier 0x52400  112 octets

  00453000  8b442404              mov       eax,[esp+4]
  00453004  33d2                  xor       edx,edx
  00453006  56                    push      esi
  00453007  b901000000            mov       ecx,1
  0045300c  66395010              cmp       [eax+10h],dx
  00453010  7512                  jne       short 00453024h
  00453012  8b30                  mov       esi,[eax]
  00453014  66894810              mov       [eax+10h],cx
  00453018  81ce00400000          or        esi,4000h
  0045301e  66895012              mov       [eax+12h],dx
  00453022  8930                  mov       [eax],esi

LAB_00453024:
  00453024  66394810              cmp       [eax+10h],cx
  00453028  753c                  jne       short 00453066h
  0045302a  66ff4012              inc       word [eax+12h]
  0045302e  668378120a            cmp       word [eax+12h],0Ah
  00453033  7531                  jne       short 00453066h
  00453035  66895012              mov       [eax+12h],dx
  00453039  8b0dcc975e00          mov       ecx,[5E97CCh]
  0045303f  52                    push      edx
  00453040  68d0070000            push      7D0h
  00453045  8b91a8000000          mov       edx,[ecx+0A8h]
  0045304b  6800200000            push      2000h
  00453050  6800040000            push      400h
  00453055  8b0a                  mov       ecx,[edx]
  00453057  660fb65116            movzx     dx,byte [ecx+16h]
  0045305c  52                    push      edx
  0045305d  50                    push      eax
  0045305e  e87d500000            call      004580E0h
  00453063  83c418                add       esp,18h

LAB_00453066:
  00453066  5e                    pop       esi
  00453067  c3                    ret
