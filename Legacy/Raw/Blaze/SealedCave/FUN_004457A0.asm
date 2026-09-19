Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004457a0  section .text  offset fichier 0x44ba0  64 octets

  004457a0  e89b191000            call      00547140h
  004457a5  99                    cdq
  004457a6  b907000000            mov       ecx,7
  004457ab  f7f9                  idiv      ecx
  004457ad  6683fa01              cmp       dx,1
  004457b1  751c                  jne       short 004457CFh
  004457b3  8b442404              mov       eax,[esp+4]
  004457b7  6aff                  push      0FFFFFFFFh
  004457b9  6a00                  push      0
  004457bb  68eb000000            push      0EBh
  004457c0  8d501c                lea       edx,[eax+1Ch]
  004457c3  6a00                  push      0
  004457c5  52                    push      edx
  004457c6  50                    push      eax
  004457c7  e874540300            call      0047AC40h
  004457cc  83c418                add       esp,18h

LAB_004457cf:
  004457cf  33c0                  xor       eax,eax
  004457d1  c3                    ret
  004457d2  90                    nop
  004457d3  90                    nop
  004457d4  90                    nop
  004457d5  90                    nop
  004457d6  90                    nop
  004457d7  90                    nop
  004457d8  90                    nop
  004457d9  90                    nop
  004457da  90                    nop
  004457db  90                    nop
  004457dc  90                    nop
  004457dd  90                    nop
  004457de  90                    nop
  004457df  90                    nop

1 cible(s) de saut interne : 0x004457cf
