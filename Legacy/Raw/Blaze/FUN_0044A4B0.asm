Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044a4b0  section .text  offset fichier 0x498b0  419 octets

  0044a4b0  53                    push      ebx
  0044a4b1  56                    push      esi
  0044a4b2  8b74240c              mov       esi,[esp+0Ch]
  0044a4b6  57                    push      edi
  0044a4b7  6a00                  push      0
  0044a4b9  6a00                  push      0
  0044a4bb  56                    push      esi
  0044a4bc  e87fdbffff            call      00448040h
  0044a4c1  83c40c                add       esp,0Ch
  0044a4c4  66837e1000            cmp       word [esi+10h],0
  0044a4c9  750d                  jne       short 0044A4D8h
  0044a4cb  6a00                  push      0
  0044a4cd  6a00                  push      0
  0044a4cf  56                    push      esi
  0044a4d0  e8dbd7ffff            call      00447CB0h
  0044a4d5  83c40c                add       esp,0Ch

LAB_0044a4d8:
  0044a4d8  8b4678                mov       eax,[esi+78h]
  0044a4db  85c0                  test      eax,eax
  0044a4dd  7505                  jne       short 0044A4E4h
  0044a4df  b858e55d00            mov       eax,5DE558h

LAB_0044a4e4:
  0044a4e4  8b481c                mov       ecx,[eax+1Ch]
  0044a4e7  8d7e1c                lea       edi,[esi+1Ch]
  0044a4ea  66837e1000            cmp       word [esi+10h],0
  0044a4ef  bb01000000            mov       ebx,1
  0044a4f4  890f                  mov       [edi],ecx
  0044a4f6  8b5020                mov       edx,[eax+20h]
  0044a4f9  895620                mov       [esi+20h],edx
  0044a4fc  8b4024                mov       eax,[eax+24h]
  0044a4ff  894624                mov       [esi+24h],eax
  0044a502  0f85a4000000          jne       near 0044A5ACh
  0044a508  6aff                  push      0FFFFFFFFh
  0044a50a  6a00                  push      0
  0044a50c  688d000000            push      8Dh
  0044a511  56                    push      esi
  0044a512  66895e10              mov       [esi+10h],bx
  0044a516  66c746160000          mov       word [esi+16h],0
  0044a51c  66c746145000          mov       word [esi+14h],50h
  0044a522  66899e9a000000        mov       [esi+9Ah],bx
  0044a529  e8b2050300            call      0047AAE0h
  0044a52e  83c410                add       esp,10h
  0044a531  6aff                  push      0FFFFFFFFh
  0044a533  6a00                  push      0
  0044a535  68a9000000            push      0A9h
  0044a53a  6a00                  push      0
  0044a53c  57                    push      edi
  0044a53d  56                    push      esi
  0044a53e  e8fd060300            call      0047AC40h
  0044a543  83c418                add       esp,18h
  0044a546  6aff                  push      0FFFFFFFFh
  0044a548  6a00                  push      0
  0044a54a  68a9000000            push      0A9h
  0044a54f  6a00                  push      0
  0044a551  57                    push      edi
  0044a552  56                    push      esi
  0044a553  e8e8060300            call      0047AC40h
  0044a558  83c418                add       esp,18h
  0044a55b  6aff                  push      0FFFFFFFFh
  0044a55d  6a00                  push      0
  0044a55f  68a9000000            push      0A9h
  0044a564  6a00                  push      0
  0044a566  57                    push      edi
  0044a567  56                    push      esi
  0044a568  e8d3060300            call      0047AC40h
  0044a56d  83c418                add       esp,18h
  0044a570  6aff                  push      0FFFFFFFFh
  0044a572  6a00                  push      0
  0044a574  68a9000000            push      0A9h
  0044a579  6a00                  push      0
  0044a57b  57                    push      edi
  0044a57c  56                    push      esi
  0044a57d  e8be060300            call      0047AC40h
  0044a582  83c418                add       esp,18h
  0044a585  6aff                  push      0FFFFFFFFh
  0044a587  6a00                  push      0
  0044a589  68a9000000            push      0A9h
  0044a58e  6a00                  push      0
  0044a590  57                    push      edi
  0044a591  56                    push      esi
  0044a592  e8a9060300            call      0047AC40h
  0044a597  8b4e78                mov       ecx,[esi+78h]
  0044a59a  8b5674                mov       edx,[esi+74h]
  0044a59d  83c418                add       esp,18h
  0044a5a0  6a00                  push      0
  0044a5a2  51                    push      ecx
  0044a5a3  52                    push      edx
  0044a5a4  e8b7000000            call      0044A660h
  0044a5a9  83c40c                add       esp,0Ch

LAB_0044a5ac:
  0044a5ac  66837e1600            cmp       word [esi+16h],0
  0044a5b1  7519                  jne       short 0044A5CCh
  0044a5b3  6800100000            push      1000h
  0044a5b8  6800100000            push      1000h
  0044a5bd  68bf010000            push      1BFh
  0044a5c2  6a2c                  push      2Ch
  0044a5c4  e8d7280f00            call      0053CEA0h
  0044a5c9  83c410                add       esp,10h

LAB_0044a5cc:
  0044a5cc  66395e16              cmp       [esi+16h],bx
  0044a5d0  7519                  jne       short 0044A5EBh
  0044a5d2  6800100000            push      1000h
  0044a5d7  6800100000            push      1000h
  0044a5dc  6800020000            push      200h
  0044a5e1  6a30                  push      30h
  0044a5e3  e8b8280f00            call      0053CEA0h
  0044a5e8  83c410                add       esp,10h

LAB_0044a5eb:
  0044a5eb  66837e1603            cmp       word [esi+16h],3
  0044a5f0  7519                  jne       short 0044A60Bh
  0044a5f2  6800100000            push      1000h
  0044a5f7  6800100000            push      1000h
  0044a5fc  6853060000            push      653h
  0044a601  6a2f                  push      2Fh
  0044a603  e898280f00            call      0053CEA0h
  0044a608  83c410                add       esp,10h

LAB_0044a60b:
  0044a60b  845e16                test      [esi+16h],bl
  0044a60e  7415                  je        short 0044A625h
  0044a610  6aff                  push      0FFFFFFFFh
  0044a612  6a00                  push      0
  0044a614  68a9000000            push      0A9h
  0044a619  6a00                  push      0
  0044a61b  57                    push      edi
  0044a61c  56                    push      esi
  0044a61d  e81e060300            call      0047AC40h
  0044a622  83c418                add       esp,18h

LAB_0044a625:
  0044a625  66ff4616              inc       word [esi+16h]
  0044a629  66837e1646            cmp       word [esi+16h],46h
  0044a62e  7c1f                  jl        short 0044A64Fh
  0044a630  8b4674                mov       eax,[esi+74h]
  0044a633  8a4e06                mov       cl,[esi+6]
  0044a636  6a00                  push      0
  0044a638  6a00                  push      0
  0044a63a  884806                mov       [eax+6],cl
  0044a63d  8b5674                mov       edx,[esi+74h]
  0044a640  8a4607                mov       al,[esi+7]
  0044a643  56                    push      esi
  0044a644  884207                mov       [edx+7],al
  0044a647  e8d4d9ffff            call      00448020h
  0044a64c  83c40c                add       esp,0Ch

LAB_0044a64f:
  0044a64f  5f                    pop       edi
  0044a650  5e                    pop       esi
  0044a651  5b                    pop       ebx
  0044a652  c3                    ret

8 cible(s) de saut interne : 0x0044a4d8, 0x0044a4e4, 0x0044a5ac, 0x0044a5cc, 0x0044a5eb, 0x0044a60b, 0x0044a625, 0x0044a64f
