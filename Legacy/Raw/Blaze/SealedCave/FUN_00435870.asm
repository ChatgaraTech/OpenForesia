Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00435870  section .text  offset fichier 0x34c70  320 octets

  00435870  56                    push      esi
  00435871  8b742408              mov       esi,[esp+8]
  00435875  66837e1000            cmp       word [esi+10h],0
  0043587a  7573                  jne       short 004358EFh
  0043587c  8a5616                mov       dl,[esi+16h]
  0043587f  b003                  mov       al,3
  00435881  2ac2                  sub       al,dl
  00435883  66c746100100          mov       word [esi+10h],1
  00435889  c7463ce80d5500        mov       dword [esi+3Ch],550DE8h
  00435890  884640                mov       [esi+40h],al
  00435893  c6460a02              mov       byte [esi+0Ah],2
  00435897  c6460b00              mov       byte [esi+0Bh],0
  0043589b  66c746120200          mov       word [esi+12h],2
  004358a1  e89a181100            call      00547140h
  004358a6  99                    cdq
  004358a7  33c2                  xor       eax,edx
  004358a9  8b4e1c                mov       ecx,[esi+1Ch]
  004358ac  2bc2                  sub       eax,edx
  004358ae  894e50                mov       [esi+50h],ecx
  004358b1  25ff030000            and       eax,3FFh
  004358b6  66c7869a0000000000    mov       word [esi+9Ah],0
  004358bf  33c2                  xor       eax,edx
  004358c1  2bc2                  sub       eax,edx
  004358c3  8b5620                mov       edx,[esi+20h]
  004358c6  05000c0000            add       eax,0C00h
  004358cb  895654                mov       [esi+54h],edx
  004358ce  66894628              mov       [esi+28h],ax
  004358d2  6689462a              mov       [esi+2Ah],ax
  004358d6  8b4624                mov       eax,[esi+24h]
  004358d9  894658                mov       [esi+58h],eax
  004358dc  e85f181100            call      00547140h
  004358e1  99                    cdq
  004358e2  b932000000            mov       ecx,32h
  004358e7  f7f9                  idiv      ecx
  004358e9  83c250                add       edx,50h
  004358ec  89565c                mov       [esi+5Ch],edx

LAB_004358ef:
  004358ef  66837e1001            cmp       word [esi+10h],1
  004358f4  755c                  jne       short 00435952h
  004358f6  66ff4e12              dec       word [esi+12h]
  004358fa  66837e1200            cmp       word [esi+12h],0
  004358ff  7d51                  jge       short 00435952h
  00435901  66837e1603            cmp       word [esi+16h],3
  00435906  7519                  jne       short 00435921h
  00435908  6a00                  push      0
  0043590a  6a00                  push      0
  0043590c  6800180000            push      1800h
  00435911  6800020000            push      200h
  00435916  6a1e                  push      1Eh
  00435918  56                    push      esi
  00435919  e862270300            call      00468080h
  0043591e  83c418                add       esp,18h

LAB_00435921:
  00435921  66ff4e16              dec       word [esi+16h]
  00435925  66837e1600            cmp       word [esi+16h],0
  0043592a  66c746100200          mov       word [esi+10h],2
  00435930  7c1a                  jl        short 0043594Ch
  00435932  6a00                  push      0
  00435934  6a00                  push      0
  00435936  6aff                  push      0FFFFFFFFh
  00435938  6a00                  push      0
  0043593a  6a32                  push      32h
  0043593c  56                    push      esi
  0043593d  e8ce270300            call      00468110h
  00435942  83c418                add       esp,18h
  00435945  66837e1600            cmp       word [esi+16h],0
  0043594a  7d06                  jge       short 00435952h

LAB_0043594c:
  0043594c  66c746160000          mov       word [esi+16h],0

LAB_00435952:
  00435952  66837e1002            cmp       word [esi+10h],2
  00435957  7549                  jne       short 004359A2h
  00435959  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00435960  52                    push      edx
  00435961  e8b0001100            call      00545A16h
  00435966  0faf465c              imul      eax,[esi+5Ch]
  0043596a  034650                add       eax,[esi+50h]
  0043596d  89461c                mov       [esi+1Ch],eax
  00435970  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00435977  50                    push      eax
  00435978  e89f001100            call      00545A1Ch
  0043597d  0faf465c              imul      eax,[esi+5Ch]
  00435981  8b5654                mov       edx,[esi+54h]
  00435984  6683869a00000064      add       word [esi+9Ah],64h
  0043598c  03c2                  add       eax,edx
  0043598e  6681be9a0000000008    cmp       word [esi+9Ah],800h
  00435997  894620                mov       [esi+20h],eax
  0043599a  7e06                  jle       short 004359A2h
  0043599c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004359a2:
  004359a2  5e                    pop       esi
  004359a3  c3                    ret
  004359a4  90                    nop
  004359a5  90                    nop
  004359a6  90                    nop
  004359a7  90                    nop
  004359a8  90                    nop
  004359a9  90                    nop
  004359aa  90                    nop
  004359ab  90                    nop
  004359ac  90                    nop
  004359ad  90                    nop
  004359ae  90                    nop
  004359af  90                    nop

5 cible(s) de saut interne : 0x004358ef, 0x00435921, 0x0043594c, 0x00435952, 0x004359a2
