Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043e030  section .text  offset fichier 0x3d430  320 octets

  0043e030  53                    push      ebx
  0043e031  56                    push      esi
  0043e032  8b74240c              mov       esi,[esp+0Ch]
  0043e036  33db                  xor       ebx,ebx
  0043e038  57                    push      edi
  0043e039  bf01000000            mov       edi,1
  0043e03e  66395e10              cmp       [esi+10h],bx
  0043e042  0f8596000000          jne       near 0043E0DEh
  0043e048  66897e10              mov       [esi+10h],di
  0043e04c  a02ce55d00            mov       al,[5DE52Ch]
  0043e051  3c10                  cmp       al,10h
  0043e053  7506                  jne       short 0043E05Bh
  0043e055  c6460a09              mov       byte [esi+0Ah],9
  0043e059  eb04                  jmp       short 0043E05Fh

LAB_0043e05b:
  0043e05b  c6460a06              mov       byte [esi+0Ah],6

LAB_0043e05f:
  0043e05f  b800200000            mov       eax,2000h
  0043e064  885e0b                mov       [esi+0Bh],bl
  0043e067  c7463c20125500        mov       dword [esi+3Ch],551220h
  0043e06e  885e40                mov       [esi+40h],bl
  0043e071  66894628              mov       [esi+28h],ax
  0043e075  6689462a              mov       [esi+2Ah],ax
  0043e079  e8c2901000            call      00547140h
  0043e07e  99                    cdq
  0043e07f  33c2                  xor       eax,edx
  0043e081  2bc2                  sub       eax,edx
  0043e083  25ff0f0000            and       eax,0FFFh
  0043e088  33c2                  xor       eax,edx
  0043e08a  2bc2                  sub       eax,edx
  0043e08c  66894612              mov       [esi+12h],ax
  0043e090  e8ab901000            call      00547140h
  0043e095  99                    cdq
  0043e096  b90a000000            mov       ecx,0Ah
  0043e09b  f7f9                  idiv      ecx
  0043e09d  03d1                  add       edx,ecx
  0043e09f  66895614              mov       [esi+14h],dx
  0043e0a3  e898901000            call      00547140h
  0043e0a8  99                    cdq
  0043e0a9  b905000000            mov       ecx,5
  0043e0ae  895e5c                mov       [esi+5Ch],ebx
  0043e0b1  f7f9                  idiv      ecx
  0043e0b3  8b4620                mov       eax,[esi+20h]
  0043e0b6  8b4e24                mov       ecx,[esi+24h]
  0043e0b9  894654                mov       [esi+54h],eax
  0043e0bc  b080                  mov       al,80h
  0043e0be  894e58                mov       [esi+58h],ecx
  0043e0c1  66899e9a000000        mov       [esi+9Ah],bx
  0043e0c8  884638                mov       [esi+38h],al
  0043e0cb  884639                mov       [esi+39h],al
  0043e0ce  88463a                mov       [esi+3Ah],al
  0043e0d1  83c203                add       edx,3
  0043e0d4  66895616              mov       [esi+16h],dx
  0043e0d8  8b561c                mov       edx,[esi+1Ch]
  0043e0db  895650                mov       [esi+50h],edx

LAB_0043e0de:
  0043e0de  66397e10              cmp       [esi+10h],di
  0043e0e2  0f8581000000          jne       near 0043E169h
  0043e0e8  0fbf5612              movsx     edx,word [esi+12h]
  0043e0ec  52                    push      edx
  0043e0ed  e82a791000            call      00545A1Ch
  0043e0f2  0faf465c              imul      eax,[esi+5Ch]
  0043e0f6  8b7e50                mov       edi,[esi+50h]
  0043e0f9  03c7                  add       eax,edi
  0043e0fb  8b7e5c                mov       edi,[esi+5Ch]
  0043e0fe  89461c                mov       [esi+1Ch],eax
  0043e101  0fbf4612              movsx     eax,word [esi+12h]
  0043e105  50                    push      eax
  0043e106  e80b791000            call      00545A16h
  0043e10b  0fafc7                imul      eax,edi
  0043e10e  8b4e58                mov       ecx,[esi+58h]
  0043e111  8b5620                mov       edx,[esi+20h]
  0043e114  03c1                  add       eax,ecx
  0043e116  6afb                  push      0FFFFFFFBh
  0043e118  0fbf4e16              movsx     ecx,word [esi+16h]
  0043e11c  c1e10c                shl       ecx,0Ch
  0043e11f  03d1                  add       edx,ecx
  0043e121  6afb                  push      0FFFFFFFBh
  0043e123  895620                mov       [esi+20h],edx
  0043e126  6afb                  push      0FFFFFFFBh
  0043e128  0fbf5614              movsx     edx,word [esi+14h]
  0043e12c  03d7                  add       edx,edi
  0043e12e  66ff869a000000        inc       word [esi+9Ah]
  0043e135  56                    push      esi
  0043e136  894624                mov       [esi+24h],eax
  0043e139  89565c                mov       [esi+5Ch],edx
  0043e13c  e89f6cfcff            call      00404DE0h
  0043e141  83c410                add       esp,10h
  0043e144  6683be9a00000003      cmp       word [esi+9Ah],3
  0043e14c  7e1b                  jle       short 0043E169h
  0043e14e  8a4e40                mov       cl,[esi+40h]
  0043e151  66899e9a000000        mov       [esi+9Ah],bx
  0043e158  fec1                  inc       cl
  0043e15a  8ac1                  mov       al,cl
  0043e15c  884e40                mov       [esi+40h],cl
  0043e15f  3c04                  cmp       al,4
  0043e161  7506                  jne       short 0043E169h
  0043e163  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043e169:
  0043e169  5f                    pop       edi
  0043e16a  5e                    pop       esi
  0043e16b  5b                    pop       ebx
  0043e16c  c3                    ret
  0043e16d  90                    nop
  0043e16e  90                    nop
  0043e16f  90                    nop

4 cible(s) de saut interne : 0x0043e05b, 0x0043e05f, 0x0043e0de, 0x0043e169
