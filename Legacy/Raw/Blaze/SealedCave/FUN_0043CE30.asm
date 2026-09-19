Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043ce30  section .text  offset fichier 0x3c230  176 octets

  0043ce30  56                    push      esi
  0043ce31  8b742408              mov       esi,[esp+8]
  0043ce35  b901000000            mov       ecx,1
  0043ce3a  66837e1000            cmp       word [esi+10h],0
  0043ce3f  7536                  jne       short 0043CE77h
  0043ce41  66894e10              mov       [esi+10h],cx
  0043ce45  a0c4de5e00            mov       al,[5EDEC4h]
  0043ce4a  884604                mov       [esi+4],al
  0043ce4d  b8b80b0000            mov       eax,0BB8h
  0043ce52  66894628              mov       [esi+28h],ax
  0043ce56  6689462a              mov       [esi+2Ah],ax
  0043ce5a  6689462c              mov       [esi+2Ch],ax
  0043ce5e  b0ff                  mov       al,0FFh
  0043ce60  c6460a03              mov       byte [esi+0Ah],3
  0043ce64  c6460b00              mov       byte [esi+0Bh],0
  0043ce68  66c746128403          mov       word [esi+12h],384h
  0043ce6e  884638                mov       [esi+38h],al
  0043ce71  884639                mov       [esi+39h],al
  0043ce74  88463a                mov       [esi+3Ah],al

LAB_0043ce77:
  0043ce77  66394e10              cmp       [esi+10h],cx
  0043ce7b  7552                  jne       short 0043CECFh
  0043ce7d  6af4                  push      0FFFFFFF4h
  0043ce7f  6af4                  push      0FFFFFFF4h
  0043ce81  6af4                  push      0FFFFFFF4h
  0043ce83  56                    push      esi
  0043ce84  e8577ffcff            call      00404DE0h
  0043ce89  668b4612              mov       ax,[esi+12h]
  0043ce8d  83c410                add       esp,10h
  0043ce90  66014628              add       [esi+28h],ax
  0043ce94  6601462a              add       [esi+2Ah],ax
  0043ce98  6601462c              add       [esi+2Ch],ax
  0043ce9c  83c028                add       eax,28h
  0043ce9f  663de803              cmp       ax,3E8h
  0043cea3  66894612              mov       [esi+12h],ax
  0043cea7  7c06                  jl        short 0043CEAFh
  0043cea9  66c74612e803          mov       word [esi+12h],3E8h

LAB_0043ceaf:
  0043ceaf  b830750000            mov       eax,7530h
  0043ceb4  66394628              cmp       [esi+28h],ax
  0043ceb8  7c15                  jl        short 0043CECFh
  0043ceba  66894628              mov       [esi+28h],ax
  0043cebe  6689462a              mov       [esi+2Ah],ax
  0043cec2  6689462c              mov       [esi+2Ch],ax
  0043cec6  8b06                  mov       eax,[esi]
  0043cec8  25ffffff7f            and       eax,7FFFFFFFh
  0043cecd  8906                  mov       [esi],eax

LAB_0043cecf:
  0043cecf  5e                    pop       esi
  0043ced0  c3                    ret
  0043ced1  90                    nop
  0043ced2  90                    nop
  0043ced3  90                    nop
  0043ced4  90                    nop
  0043ced5  90                    nop
  0043ced6  90                    nop
  0043ced7  90                    nop
  0043ced8  90                    nop
  0043ced9  90                    nop
  0043ceda  90                    nop
  0043cedb  90                    nop
  0043cedc  90                    nop
  0043cedd  90                    nop
  0043cede  90                    nop
  0043cedf  90                    nop

3 cible(s) de saut interne : 0x0043ce77, 0x0043ceaf, 0x0043cecf
