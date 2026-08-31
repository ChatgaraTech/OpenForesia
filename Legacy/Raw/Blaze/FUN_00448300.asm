Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00448300  section .text  offset fichier 0x47700  418 octets

  00448300  56                    push      esi
  00448301  8b742408              mov       esi,[esp+8]
  00448305  57                    push      edi
  00448306  6a00                  push      0
  00448308  6a00                  push      0
  0044830a  56                    push      esi
  0044830b  e830fdffff            call      00448040h
  00448310  83c40c                add       esp,0Ch
  00448313  66837e1000            cmp       word [esi+10h],0
  00448318  750d                  jne       short 00448327h
  0044831a  6a00                  push      0
  0044831c  6a00                  push      0
  0044831e  56                    push      esi
  0044831f  e88cf9ffff            call      00447CB0h
  00448324  83c40c                add       esp,0Ch

LAB_00448327:
  00448327  8b4678                mov       eax,[esi+78h]
  0044832a  85c0                  test      eax,eax
  0044832c  7505                  jne       short 00448333h
  0044832e  b858e55d00            mov       eax,5DE558h

LAB_00448333:
  00448333  8b481c                mov       ecx,[eax+1Ch]
  00448336  8d7e1c                lea       edi,[esi+1Ch]
  00448339  890f                  mov       [edi],ecx
  0044833b  8b5020                mov       edx,[eax+20h]
  0044833e  81c200000500          add       edx,50000h
  00448344  66837e1000            cmp       word [esi+10h],0
  00448349  895620                mov       [esi+20h],edx
  0044834c  8b4024                mov       eax,[eax+24h]
  0044834f  894624                mov       [esi+24h],eax
  00448352  7529                  jne       short 0044837Dh
  00448354  66c746100100          mov       word [esi+10h],1
  0044835a  66c746160000          mov       word [esi+16h],0
  00448360  66c746142800          mov       word [esi+14h],28h
  00448366  c7464450000000        mov       dword [esi+44h],50h
  0044836d  c7465c96000000        mov       dword [esi+5Ch],96h
  00448374  66c7869a0000000200    mov       word [esi+9Ah],2

LAB_0044837d:
  0044837d  668b4616              mov       ax,[esi+16h]
  00448381  663d0100              cmp       ax,1
  00448385  7412                  je        short 00448399h
  00448387  663d0300              cmp       ax,3
  0044838b  740c                  je        short 00448399h
  0044838d  663d0600              cmp       ax,6
  00448391  7406                  je        short 00448399h
  00448393  663d0900              cmp       ax,9
  00448397  7538                  jne       short 004483D1h

LAB_00448399:
  00448399  6aff                  push      0FFFFFFFFh
  0044839b  6a00                  push      0
  0044839d  68a7000000            push      0A7h
  004483a2  6a00                  push      0
  004483a4  57                    push      edi
  004483a5  56                    push      esi
  004483a6  c7464800000000        mov       dword [esi+48h],0
  004483ad  e88e280300            call      0047AC40h
  004483b2  83c418                add       esp,18h
  004483b5  c7464800080000        mov       dword [esi+48h],800h
  004483bc  6aff                  push      0FFFFFFFFh
  004483be  6a00                  push      0
  004483c0  68a7000000            push      0A7h
  004483c5  6a00                  push      0
  004483c7  57                    push      edi
  004483c8  56                    push      esi
  004483c9  e872280300            call      0047AC40h
  004483ce  83c418                add       esp,18h

LAB_004483d1:
  004483d1  66837e1600            cmp       word [esi+16h],0
  004483d6  7519                  jne       short 004483F1h
  004483d8  6800120000            push      1200h
  004483dd  6800120000            push      1200h
  004483e2  6800010000            push      100h
  004483e7  6a16                  push      16h
  004483e9  e8b24a0f00            call      0053CEA0h
  004483ee  83c410                add       esp,10h

LAB_004483f1:
  004483f1  66837e1602            cmp       word [esi+16h],2
  004483f6  7519                  jne       short 00448411h
  004483f8  6800120000            push      1200h
  004483fd  6800120000            push      1200h
  00448402  682a010000            push      12Ah
  00448407  6a16                  push      16h
  00448409  e8924a0f00            call      0053CEA0h
  0044840e  83c410                add       esp,10h

LAB_00448411:
  00448411  66837e161e            cmp       word [esi+16h],1Eh
  00448416  752b                  jne       short 00448443h
  00448418  6aff                  push      0FFFFFFFFh
  0044841a  6a00                  push      0
  0044841c  68a0000000            push      0A0h
  00448421  56                    push      esi
  00448422  e8b9260300            call      0047AAE0h
  00448427  83c410                add       esp,10h
  0044842a  6800120000            push      1200h
  0044842f  6800120000            push      1200h
  00448434  68a9050000            push      5A9h
  00448439  6a0d                  push      0Dh
  0044843b  e8604a0f00            call      0053CEA0h
  00448440  83c410                add       esp,10h

LAB_00448443:
  00448443  66837e1620            cmp       word [esi+16h],20h
  00448448  7519                  jne       short 00448463h
  0044844a  6800100000            push      1000h
  0044844f  6800100000            push      1000h
  00448454  6800040000            push      400h
  00448459  6a2d                  push      2Dh
  0044845b  e8404a0f00            call      0053CEA0h
  00448460  83c410                add       esp,10h

LAB_00448463:
  00448463  66ff4616              inc       word [esi+16h]
  00448467  66837e163c            cmp       word [esi+16h],3Ch
  0044846c  7e31                  jle       short 0044849Fh
  0044846e  8b4e74                mov       ecx,[esi+74h]
  00448471  8a5606                mov       dl,[esi+6]
  00448474  6a00                  push      0
  00448476  885106                mov       [ecx+6],dl
  00448479  8b4674                mov       eax,[esi+74h]
  0044847c  8a4e07                mov       cl,[esi+7]
  0044847f  884807                mov       [eax+7],cl
  00448482  8b5678                mov       edx,[esi+78h]
  00448485  8b4674                mov       eax,[esi+74h]
  00448488  52                    push      edx
  00448489  50                    push      eax
  0044848a  e821000000            call      004484B0h
  0044848f  83c40c                add       esp,0Ch
  00448492  6a00                  push      0
  00448494  6a00                  push      0
  00448496  56                    push      esi
  00448497  e884fbffff            call      00448020h
  0044849c  83c40c                add       esp,0Ch

LAB_0044849f:
  0044849f  5f                    pop       edi
  004484a0  5e                    pop       esi
  004484a1  c3                    ret

10 cible(s) de saut interne : 0x00448327, 0x00448333, 0x0044837d, 0x00448399, 0x004483d1, 0x004483f1, 0x00448411, 0x00448443, 0x00448463, 0x0044849f
