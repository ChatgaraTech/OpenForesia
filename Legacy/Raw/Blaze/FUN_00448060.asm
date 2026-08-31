Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00448060  section .text  offset fichier 0x47460  425 octets

  00448060  56                    push      esi
  00448061  8b742408              mov       esi,[esp+8]
  00448065  57                    push      edi
  00448066  6a00                  push      0
  00448068  6a00                  push      0
  0044806a  56                    push      esi
  0044806b  e8d0ffffff            call      00448040h
  00448070  83c40c                add       esp,0Ch
  00448073  66837e1000            cmp       word [esi+10h],0
  00448078  750d                  jne       short 00448087h
  0044807a  6a00                  push      0
  0044807c  6a00                  push      0
  0044807e  56                    push      esi
  0044807f  e82cfcffff            call      00447CB0h
  00448084  83c40c                add       esp,0Ch

LAB_00448087:
  00448087  8b4678                mov       eax,[esi+78h]
  0044808a  85c0                  test      eax,eax
  0044808c  7505                  jne       short 00448093h
  0044808e  b858e55d00            mov       eax,5DE558h

LAB_00448093:
  00448093  8b481c                mov       ecx,[eax+1Ch]
  00448096  8d7e1c                lea       edi,[esi+1Ch]
  00448099  890f                  mov       [edi],ecx
  0044809b  8b5020                mov       edx,[eax+20h]
  0044809e  81c200000500          add       edx,50000h
  004480a4  66837e1000            cmp       word [esi+10h],0
  004480a9  895620                mov       [esi+20h],edx
  004480ac  8b4024                mov       eax,[eax+24h]
  004480af  894624                mov       [esi+24h],eax
  004480b2  7529                  jne       short 004480DDh
  004480b4  66c746100100          mov       word [esi+10h],1
  004480ba  66c746160000          mov       word [esi+16h],0
  004480c0  66c746142800          mov       word [esi+14h],28h
  004480c6  c7464450000000        mov       dword [esi+44h],50h
  004480cd  c7465c96000000        mov       dword [esi+5Ch],96h
  004480d4  66c7869a0000000600    mov       word [esi+9Ah],6

LAB_004480dd:
  004480dd  668b4616              mov       ax,[esi+16h]
  004480e1  663d0100              cmp       ax,1
  004480e5  7412                  je        short 004480F9h
  004480e7  663d0300              cmp       ax,3
  004480eb  740c                  je        short 004480F9h
  004480ed  663d0600              cmp       ax,6
  004480f1  7406                  je        short 004480F9h
  004480f3  663d0900              cmp       ax,9
  004480f7  7538                  jne       short 00448131h

LAB_004480f9:
  004480f9  6aff                  push      0FFFFFFFFh
  004480fb  6a00                  push      0
  004480fd  68a7000000            push      0A7h
  00448102  6a00                  push      0
  00448104  57                    push      edi
  00448105  56                    push      esi
  00448106  c7464800000000        mov       dword [esi+48h],0
  0044810d  e82e2b0300            call      0047AC40h
  00448112  83c418                add       esp,18h
  00448115  c7464800080000        mov       dword [esi+48h],800h
  0044811c  6aff                  push      0FFFFFFFFh
  0044811e  6a00                  push      0
  00448120  68a7000000            push      0A7h
  00448125  6a00                  push      0
  00448127  57                    push      edi
  00448128  56                    push      esi
  00448129  e8122b0300            call      0047AC40h
  0044812e  83c418                add       esp,18h

LAB_00448131:
  00448131  66837e1600            cmp       word [esi+16h],0
  00448136  7519                  jne       short 00448151h
  00448138  6800120000            push      1200h
  0044813d  6800120000            push      1200h
  00448142  6800010000            push      100h
  00448147  6a16                  push      16h
  00448149  e8524d0f00            call      0053CEA0h
  0044814e  83c410                add       esp,10h

LAB_00448151:
  00448151  66837e1602            cmp       word [esi+16h],2
  00448156  7519                  jne       short 00448171h
  00448158  6800120000            push      1200h
  0044815d  6800120000            push      1200h
  00448162  682a010000            push      12Ah
  00448167  6a16                  push      16h
  00448169  e8324d0f00            call      0053CEA0h
  0044816e  83c410                add       esp,10h

LAB_00448171:
  00448171  66837e161e            cmp       word [esi+16h],1Eh
  00448176  7512                  jne       short 0044818Ah
  00448178  6aff                  push      0FFFFFFFFh
  0044817a  6a00                  push      0
  0044817c  68a0000000            push      0A0h
  00448181  56                    push      esi
  00448182  e859290300            call      0047AAE0h
  00448187  83c410                add       esp,10h

LAB_0044818a:
  0044818a  66837e1623            cmp       word [esi+16h],23h
  0044818f  7519                  jne       short 004481AAh
  00448191  6800120000            push      1200h
  00448196  6800120000            push      1200h
  0044819b  6800080000            push      800h
  004481a0  6a19                  push      19h
  004481a2  e8f94c0f00            call      0053CEA0h
  004481a7  83c410                add       esp,10h

LAB_004481aa:
  004481aa  66837e1625            cmp       word [esi+16h],25h
  004481af  7519                  jne       short 004481CAh
  004481b1  6800100000            push      1000h
  004481b6  6800100000            push      1000h
  004481bb  6800040000            push      400h
  004481c0  6a1c                  push      1Ch
  004481c2  e8d94c0f00            call      0053CEA0h
  004481c7  83c410                add       esp,10h

LAB_004481ca:
  004481ca  66ff4616              inc       word [esi+16h]
  004481ce  66837e163c            cmp       word [esi+16h],3Ch
  004481d3  7e31                  jle       short 00448206h
  004481d5  8b4e74                mov       ecx,[esi+74h]
  004481d8  8a5606                mov       dl,[esi+6]
  004481db  6a00                  push      0
  004481dd  885106                mov       [ecx+6],dl
  004481e0  8b4674                mov       eax,[esi+74h]
  004481e3  8a4e07                mov       cl,[esi+7]
  004481e6  884807                mov       [eax+7],cl
  004481e9  8b5678                mov       edx,[esi+78h]
  004481ec  8b4674                mov       eax,[esi+74h]
  004481ef  52                    push      edx
  004481f0  50                    push      eax
  004481f1  e81a000000            call      00448210h
  004481f6  83c40c                add       esp,0Ch
  004481f9  6a00                  push      0
  004481fb  6a00                  push      0
  004481fd  56                    push      esi
  004481fe  e81dfeffff            call      00448020h
  00448203  83c40c                add       esp,0Ch

LAB_00448206:
  00448206  5f                    pop       edi
  00448207  5e                    pop       esi
  00448208  c3                    ret

11 cible(s) de saut interne : 0x00448087, 0x00448093, 0x004480dd, 0x004480f9, 0x00448131, 0x00448151, 0x00448171, 0x0044818a, 0x004481aa, 0x004481ca, 0x00448206
