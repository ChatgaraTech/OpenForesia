; 0x0040ef40 — le geyser et sa partition (158 instructions, absente de Blaze.c). Planche : sixieme enregistrement de 0x0054f7c8.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040ef40  section .text  offset fichier 0xe340  600 octets

  0040ef40  53                    push      ebx
  0040ef41  56                    push      esi
  0040ef42  8b74240c              mov       esi,[esp+0Ch]
  0040ef46  33db                  xor       ebx,ebx
  0040ef48  66395e10              cmp       [esi+10h],bx
  0040ef4c  754b                  jne       short 0040EF99h
  0040ef4e  8b4e1c                mov       ecx,[esi+1Ch]
  0040ef51  8d461c                lea       eax,[esi+1Ch]
  0040ef54  6aff                  push      0FFFFFFFFh
  0040ef56  53                    push      ebx
  0040ef57  68fc000000            push      0FCh
  0040ef5c  53                    push      ebx
  0040ef5d  81c100800200          add       ecx,28000h
  0040ef63  50                    push      eax
  0040ef64  56                    push      esi
  0040ef65  66c746100100          mov       word [esi+10h],1
  0040ef6b  c7463cf0f75400        mov       dword [esi+3Ch],54F7F0h
  0040ef72  885e40                mov       [esi+40h],bl
  0040ef75  c6460a0a              mov       byte [esi+0Ah],0Ah
  0040ef79  885e0b                mov       [esi+0Bh],bl
  0040ef7c  66895e28              mov       [esi+28h],bx
  0040ef80  66895e2a              mov       [esi+2Ah],bx
  0040ef84  66895e12              mov       [esi+12h],bx
  0040ef88  8908                  mov       [eax],ecx
  0040ef8a  e8b1bc0600            call      0047AC40h
  0040ef8f  83c418                add       esp,18h
  0040ef92  66899e98000000        mov       [esi+98h],bx

LAB_0040ef99:
  0040ef99  66837e1001            cmp       word [esi+10h],1
  0040ef9e  7568                  jne       short 0040F008h
  0040efa0  6aff                  push      0FFFFFFFFh
  0040efa2  53                    push      ebx
  0040efa3  68fb000000            push      0FBh
  0040efa8  8d461c                lea       eax,[esi+1Ch]
  0040efab  53                    push      ebx
  0040efac  50                    push      eax
  0040efad  56                    push      esi
  0040efae  e88dbc0600            call      0047AC40h
  0040efb3  8b4e20                mov       ecx,[esi+20h]
  0040efb6  83c418                add       esp,18h
  0040efb9  81c100200000          add       ecx,2000h
  0040efbf  894e20                mov       [esi+20h],ecx
  0040efc2  b964000000            mov       ecx,64h
  0040efc7  66014e28              add       [esi+28h],cx
  0040efcb  668b4628              mov       ax,[esi+28h]
  0040efcf  66014e2a              add       [esi+2Ah],cx
  0040efd3  668b4e2a              mov       cx,[esi+2Ah]
  0040efd7  663d0008              cmp       ax,800h
  0040efdb  7c06                  jl        short 0040EFE3h
  0040efdd  66c746280008          mov       word [esi+28h],800h

LAB_0040efe3:
  0040efe3  6681f9c409            cmp       cx,9C4h
  0040efe8  7c06                  jl        short 0040EFF0h
  0040efea  66c7462ac409          mov       word [esi+2Ah],9C4h

LAB_0040eff0:
  0040eff0  66ff4612              inc       word [esi+12h]
  0040eff4  66837e123c            cmp       word [esi+12h],3Ch
  0040eff9  7c0d                  jl        short 0040F008h
  0040effb  66c746100200          mov       word [esi+10h],2
  0040f001  66895e12              mov       [esi+12h],bx
  0040f005  5e                    pop       esi
  0040f006  5b                    pop       ebx
  0040f007  c3                    ret

LAB_0040f008:
  0040f008  66837e1002            cmp       word [esi+10h],2
  0040f00d  753d                  jne       short 0040F04Ch
  0040f00f  6aff                  push      0FFFFFFFFh
  0040f011  53                    push      ebx
  0040f012  68fb000000            push      0FBh
  0040f017  8d4e1c                lea       ecx,[esi+1Ch]
  0040f01a  53                    push      ebx
  0040f01b  51                    push      ecx
  0040f01c  56                    push      esi
  0040f01d  e81ebc0600            call      0047AC40h
  0040f022  0fbf5612              movsx     edx,word [esi+12h]
  0040f026  83c418                add       esp,18h
  0040f029  52                    push      edx
  0040f02a  e8ed691300            call      00545A1Ch
  0040f02f  8b5620                mov       edx,[esi+20h]
  0040f032  6683461250            add       word [esi+12h],50h
  0040f037  d1e0                  shl       eax,1
  0040f039  03d0                  add       edx,eax
  0040f03b  66817e121027          cmp       word [esi+12h],2710h
  0040f041  895620                mov       [esi+20h],edx
  0040f044  7e06                  jle       short 0040F04Ch
  0040f046  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040f04c:
  0040f04c  66399e98000000        cmp       [esi+98h],bx
  0040f053  7519                  jne       short 0040F06Eh
  0040f055  6800100000            push      1000h
  0040f05a  6800100000            push      1000h
  0040f05f  6800020000            push      200h
  0040f064  6a27                  push      27h
  0040f066  e835de1200            call      0053CEA0h
  0040f06b  83c410                add       esp,10h

LAB_0040f06e:
  0040f06e  6683be9800000001      cmp       word [esi+98h],1
  0040f076  7519                  jne       short 0040F091h
  0040f078  6800100000            push      1000h
  0040f07d  6800100000            push      1000h
  0040f082  687f020000            push      27Fh
  0040f087  6a27                  push      27h
  0040f089  e812de1200            call      0053CEA0h
  0040f08e  83c410                add       esp,10h

LAB_0040f091:
  0040f091  6683be9800000002      cmp       word [esi+98h],2
  0040f099  7519                  jne       short 0040F0B4h
  0040f09b  6800100000            push      1000h
  0040f0a0  6800100000            push      1000h
  0040f0a5  6829030000            push      329h
  0040f0aa  6a27                  push      27h
  0040f0ac  e8efdd1200            call      0053CEA0h
  0040f0b1  83c410                add       esp,10h

LAB_0040f0b4:
  0040f0b4  6683be9800000003      cmp       word [esi+98h],3
  0040f0bc  7519                  jne       short 0040F0D7h
  0040f0be  6800100000            push      1000h
  0040f0c3  6800100000            push      1000h
  0040f0c8  6800020000            push      200h
  0040f0cd  6a30                  push      30h
  0040f0cf  e8ccdd1200            call      0053CEA0h
  0040f0d4  83c410                add       esp,10h

LAB_0040f0d7:
  0040f0d7  6683be9800000028      cmp       word [esi+98h],28h
  0040f0df  7519                  jne       short 0040F0FAh
  0040f0e1  6800100000            push      1000h
  0040f0e6  6800100000            push      1000h
  0040f0eb  6800020000            push      200h
  0040f0f0  6a27                  push      27h
  0040f0f2  e8a9dd1200            call      0053CEA0h
  0040f0f7  83c410                add       esp,10h

LAB_0040f0fa:
  0040f0fa  6683be9800000029      cmp       word [esi+98h],29h
  0040f102  7519                  jne       short 0040F11Dh
  0040f104  6800100000            push      1000h
  0040f109  6800100000            push      1000h
  0040f10e  68d4020000            push      2D4h
  0040f113  6a27                  push      27h
  0040f115  e886dd1200            call      0053CEA0h
  0040f11a  83c410                add       esp,10h

LAB_0040f11d:
  0040f11d  6683be980000002a      cmp       word [esi+98h],2Ah
  0040f125  7519                  jne       short 0040F140h
  0040f127  6800100000            push      1000h
  0040f12c  6800100000            push      1000h
  0040f131  687e030000            push      37Eh
  0040f136  6a27                  push      27h
  0040f138  e863dd1200            call      0053CEA0h
  0040f13d  83c410                add       esp,10h

LAB_0040f140:
  0040f140  6683be980000002b      cmp       word [esi+98h],2Bh
  0040f148  7519                  jne       short 0040F163h
  0040f14a  6800100000            push      1000h
  0040f14f  6800100000            push      1000h
  0040f154  68bf010000            push      1BFh
  0040f159  6a30                  push      30h
  0040f15b  e840dd1200            call      0053CEA0h
  0040f160  83c410                add       esp,10h

LAB_0040f163:
  0040f163  6683be9800000050      cmp       word [esi+98h],50h
  0040f16b  7519                  jne       short 0040F186h
  0040f16d  6800100000            push      1000h
  0040f172  6800100000            push      1000h
  0040f177  687f020000            push      27Fh
  0040f17c  6a30                  push      30h
  0040f17e  e81ddd1200            call      0053CEA0h
  0040f183  83c410                add       esp,10h

LAB_0040f186:
  0040f186  66ff8698000000        inc       word [esi+98h]
  0040f18d  5e                    pop       esi
  0040f18e  5b                    pop       ebx
  0040f18f  c3                    ret
  0040f190  56                    push      esi
  0040f191  8b742408              mov       esi,[esp+8]
  0040f195  66837e                (bad)
  (octet invalide - la plage demandee deborde probablement du code)
