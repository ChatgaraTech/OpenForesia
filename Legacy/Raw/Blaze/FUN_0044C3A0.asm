Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044c3a0  section .text  offset fichier 0x4b7a0  837 octets

  0044c3a0  55                    push      ebp
  0044c3a1  56                    push      esi
  0044c3a2  8b74240c              mov       esi,[esp+0Ch]
  0044c3a6  57                    push      edi
  0044c3a7  33ff                  xor       edi,edi
  0044c3a9  57                    push      edi
  0044c3aa  57                    push      edi
  0044c3ab  56                    push      esi
  0044c3ac  e88fbcffff            call      00448040h
  0044c3b1  83c40c                add       esp,0Ch
  0044c3b4  66397e10              cmp       [esi+10h],di
  0044c3b8  750b                  jne       short 0044C3C5h
  0044c3ba  57                    push      edi
  0044c3bb  57                    push      edi
  0044c3bc  56                    push      esi
  0044c3bd  e8eeb8ffff            call      00447CB0h
  0044c3c2  83c40c                add       esp,0Ch

LAB_0044c3c5:
  0044c3c5  8b4678                mov       eax,[esi+78h]
  0044c3c8  3bc7                  cmp       eax,edi
  0044c3ca  7505                  jne       short 0044C3D1h
  0044c3cc  b858e55d00            mov       eax,5DE558h

LAB_0044c3d1:
  0044c3d1  8b481c                mov       ecx,[eax+1Ch]
  0044c3d4  66397e10              cmp       [esi+10h],di
  0044c3d8  894e1c                mov       [esi+1Ch],ecx
  0044c3db  8b5020                mov       edx,[eax+20h]
  0044c3de  895620                mov       [esi+20h],edx
  0044c3e1  8b4024                mov       eax,[eax+24h]
  0044c3e4  894624                mov       [esi+24h],eax
  0044c3e7  bd01000000            mov       ebp,1
  0044c3ec  7512                  jne       short 0044C400h
  0044c3ee  8b4674                mov       eax,[esi+74h]
  0044c3f1  66896e10              mov       [esi+10h],bp
  0044c3f5  66897e16              mov       [esi+16h],di
  0044c3f9  8b08                  mov       ecx,[eax]
  0044c3fb  80c980                or        cl,80h
  0044c3fe  8908                  mov       [eax],ecx

LAB_0044c400:
  0044c400  66397e16              cmp       [esi+16h],di
  0044c404  7519                  jne       short 0044C41Fh
  0044c406  6800100000            push      1000h
  0044c40b  6800100000            push      1000h
  0044c410  6800080000            push      800h
  0044c415  6a0a                  push      0Ah
  0044c417  e8840a0f00            call      0053CEA0h
  0044c41c  83c410                add       esp,10h

LAB_0044c41f:
  0044c41f  66396e16              cmp       [esi+16h],bp
  0044c423  7519                  jne       short 0044C43Eh
  0044c425  6800100000            push      1000h
  0044c42a  6800100000            push      1000h
  0044c42f  6800040000            push      400h
  0044c434  6a0f                  push      0Fh
  0044c436  e8650a0f00            call      0053CEA0h
  0044c43b  83c410                add       esp,10h

LAB_0044c43e:
  0044c43e  66837e1602            cmp       word [esi+16h],2
  0044c443  7519                  jne       short 0044C45Eh
  0044c445  6800100000            push      1000h
  0044c44a  6800100000            push      1000h
  0044c44f  68a7070000            push      7A7h
  0044c454  6a10                  push      10h
  0044c456  e8450a0f00            call      0053CEA0h
  0044c45b  83c410                add       esp,10h

LAB_0044c45e:
  0044c45e  66837e1615            cmp       word [esi+16h],15h
  0044c463  7519                  jne       short 0044C47Eh
  0044c465  6800100000            push      1000h
  0044c46a  6800100000            push      1000h
  0044c46f  68520b0000            push      0B52h
  0044c474  6a2f                  push      2Fh
  0044c476  e8250a0f00            call      0053CEA0h
  0044c47b  83c410                add       esp,10h

LAB_0044c47e:
  0044c47e  66837e1616            cmp       word [esi+16h],16h
  0044c483  7519                  jne       short 0044C49Eh
  0044c485  6800100000            push      1000h
  0044c48a  6800100000            push      1000h
  0044c48f  68fc0b0000            push      0BFCh
  0044c494  6a2f                  push      2Fh
  0044c496  e8050a0f00            call      0053CEA0h
  0044c49b  83c410                add       esp,10h

LAB_0044c49e:
  0044c49e  66837e1617            cmp       word [esi+16h],17h
  0044c4a3  7519                  jne       short 0044C4BEh
  0044c4a5  6800100000            push      1000h
  0044c4aa  6800100000            push      1000h
  0044c4af  6829030000            push      329h
  0044c4b4  6a30                  push      30h
  0044c4b6  e8e5090f00            call      0053CEA0h
  0044c4bb  83c410                add       esp,10h

LAB_0044c4be:
  0044c4be  66837e1618            cmp       word [esi+16h],18h
  0044c4c3  7519                  jne       short 0044C4DEh
  0044c4c5  6800100000            push      1000h
  0044c4ca  6800100000            push      1000h
  0044c4cf  6800020000            push      200h
  0044c4d4  6a2d                  push      2Dh
  0044c4d6  e8c5090f00            call      0053CEA0h
  0044c4db  83c410                add       esp,10h

LAB_0044c4de:
  0044c4de  66396e10              cmp       [esi+10h],bp
  0044c4e2  0f850a010000          jne       near 0044C5F2h
  0044c4e8  6aff                  push      0FFFFFFFFh
  0044c4ea  57                    push      edi
  0044c4eb  6894000000            push      94h
  0044c4f0  56                    push      esi
  0044c4f1  66c7869a000000b018    mov       word [esi+9Ah],18B0h
  0044c4fa  897e50                mov       [esi+50h],edi
  0044c4fd  897e58                mov       [esi+58h],edi
  0044c500  c7465400000a00        mov       dword [esi+54h],0A0000h
  0044c507  66c74634dc05          mov       word [esi+34h],5DCh
  0044c50d  66897e32              mov       [esi+32h],di
  0044c511  e8cae50200            call      0047AAE0h
  0044c516  83c410                add       esp,10h
  0044c519  66c746320004          mov       word [esi+32h],400h
  0044c51f  6aff                  push      0FFFFFFFFh
  0044c521  57                    push      edi
  0044c522  6894000000            push      94h
  0044c527  56                    push      esi
  0044c528  e8b3e50200            call      0047AAE0h
  0044c52d  83c410                add       esp,10h
  0044c530  66c746320008          mov       word [esi+32h],800h
  0044c536  6aff                  push      0FFFFFFFFh
  0044c538  57                    push      edi
  0044c539  6894000000            push      94h
  0044c53e  56                    push      esi
  0044c53f  e89ce50200            call      0047AAE0h
  0044c544  83c410                add       esp,10h
  0044c547  66c74632000c          mov       word [esi+32h],0C00h
  0044c54d  6aff                  push      0FFFFFFFFh
  0044c54f  57                    push      edi
  0044c550  6894000000            push      94h
  0044c555  56                    push      esi
  0044c556  e885e50200            call      0047AAE0h
  0044c55b  83c410                add       esp,10h
  0044c55e  bd00d00a00            mov       ebp,0AD000h
  0044c563  66c7869a000000401a    mov       word [esi+9Ah],1A40h
  0044c56c  c7465400a00000        mov       dword [esi+54h],0A000h
  0044c573  6aff                  push      0FFFFFFFFh
  0044c575  57                    push      edi
  0044c576  6894000000            push      94h
  0044c57b  56                    push      esi
  0044c57c  66c746340004          mov       word [esi+34h],400h
  0044c582  896e58                mov       [esi+58h],ebp
  0044c585  66c746320002          mov       word [esi+32h],200h
  0044c58b  e850e50200            call      0047AAE0h
  0044c590  83c410                add       esp,10h
  0044c593  c746580030f5ff        mov       dword [esi+58h],0FFF53000h
  0044c59a  66c7463200fa          mov       word [esi+32h],0FA00h
  0044c5a0  6aff                  push      0FFFFFFFFh
  0044c5a2  57                    push      edi
  0044c5a3  6894000000            push      94h
  0044c5a8  56                    push      esi
  0044c5a9  e832e50200            call      0047AAE0h
  0044c5ae  83c410                add       esp,10h
  0044c5b1  897e58                mov       [esi+58h],edi
  0044c5b4  896e50                mov       [esi+50h],ebp
  0044c5b7  66c746320006          mov       word [esi+32h],600h
  0044c5bd  6aff                  push      0FFFFFFFFh
  0044c5bf  57                    push      edi
  0044c5c0  6894000000            push      94h
  0044c5c5  56                    push      esi
  0044c5c6  e815e50200            call      0047AAE0h
  0044c5cb  83c410                add       esp,10h
  0044c5ce  c746500030f5ff        mov       dword [esi+50h],0FFF53000h
  0044c5d5  66c7463200fe          mov       word [esi+32h],0FE00h
  0044c5db  6aff                  push      0FFFFFFFFh
  0044c5dd  57                    push      edi
  0044c5de  6894000000            push      94h
  0044c5e3  56                    push      esi
  0044c5e4  e8f7e40200            call      0047AAE0h
  0044c5e9  83c410                add       esp,10h
  0044c5ec  66c746100200          mov       word [esi+10h],2

LAB_0044c5f2:
  0044c5f2  66837e1002            cmp       word [esi+10h],2
  0044c5f7  0f85e4000000          jne       near 0044C6E1h
  0044c5fd  66ff4616              inc       word [esi+16h]
  0044c601  66837e1614            cmp       word [esi+16h],14h
  0044c606  7532                  jne       short 0044C63Ah
  0044c608  6aff                  push      0FFFFFFFFh
  0044c60a  57                    push      edi
  0044c60b  b810130000            mov       eax,1310h
  0044c610  6895000000            push      95h
  0044c615  56                    push      esi
  0044c616  66894628              mov       [esi+28h],ax
  0044c61a  6689462c              mov       [esi+2Ch],ax
  0044c61e  66c7462a4010          mov       word [esi+2Ah],1040h
  0044c624  66897e30              mov       [esi+30h],di
  0044c628  66c746320002          mov       word [esi+32h],200h
  0044c62e  66897e34              mov       [esi+34h],di
  0044c632  e8a9e40200            call      0047AAE0h
  0044c637  83c410                add       esp,10h

LAB_0044c63a:
  0044c63a  66837e1615            cmp       word [esi+16h],15h
  0044c63f  7522                  jne       short 0044C663h
  0044c641  6aff                  push      0FFFFFFFFh
  0044c643  57                    push      edi
  0044c644  6893000000            push      93h
  0044c649  56                    push      esi
  0044c64a  e891e40200            call      0047AAE0h
  0044c64f  83c410                add       esp,10h
  0044c652  6aff                  push      0FFFFFFFFh
  0044c654  57                    push      edi
  0044c655  6893000000            push      93h
  0044c65a  56                    push      esi
  0044c65b  e880e40200            call      0047AAE0h
  0044c660  83c410                add       esp,10h

LAB_0044c663:
  0044c663  668b4616              mov       ax,[esi+16h]
  0044c667  663d1600              cmp       ax,16h
  0044c66b  7406                  je        short 0044C673h
  0044c66d  663d1a00              cmp       ax,1Ah
  0044c671  7530                  jne       short 0044C6A3h

LAB_0044c673:
  0044c673  6aff                  push      0FFFFFFFFh
  0044c675  57                    push      edi
  0044c676  b8b80b0000            mov       eax,0BB8h
  0044c67b  6892000000            push      92h
  0044c680  56                    push      esi
  0044c681  66894628              mov       [esi+28h],ax
  0044c685  6689462a              mov       [esi+2Ah],ax
  0044c689  6689462c              mov       [esi+2Ch],ax
  0044c68d  66897e30              mov       [esi+30h],di
  0044c691  66897e34              mov       [esi+34h],di
  0044c695  66c746320002          mov       word [esi+32h],200h
  0044c69b  e840e40200            call      0047AAE0h
  0044c6a0  83c410                add       esp,10h

LAB_0044c6a3:
  0044c6a3  66837e163c            cmp       word [esi+16h],3Ch
  0044c6a8  7537                  jne       short 0044C6E1h
  0044c6aa  8b4e74                mov       ecx,[esi+74h]
  0044c6ad  8a5606                mov       dl,[esi+6]
  0044c6b0  57                    push      edi
  0044c6b1  885106                mov       [ecx+6],dl
  0044c6b4  8b4674                mov       eax,[esi+74h]
  0044c6b7  8a4e07                mov       cl,[esi+7]
  0044c6ba  884807                mov       [eax+7],cl
  0044c6bd  8b5678                mov       edx,[esi+78h]
  0044c6c0  8b4674                mov       eax,[esi+74h]
  0044c6c3  52                    push      edx
  0044c6c4  50                    push      eax
  0044c6c5  e826000000            call      0044C6F0h
  0044c6ca  83c40c                add       esp,0Ch
  0044c6cd  57                    push      edi
  0044c6ce  57                    push      edi
  0044c6cf  56                    push      esi
  0044c6d0  e84bb9ffff            call      00448020h
  0044c6d5  8b7674                mov       esi,[esi+74h]
  0044c6d8  83c40c                add       esp,0Ch
  0044c6db  8b06                  mov       eax,[esi]
  0044c6dd  247f                  and       al,7Fh
  0044c6df  8906                  mov       [esi],eax

LAB_0044c6e1:
  0044c6e1  5f                    pop       edi
  0044c6e2  5e                    pop       esi
  0044c6e3  5d                    pop       ebp
  0044c6e4  c3                    ret

16 cible(s) de saut interne : 0x0044c3c5, 0x0044c3d1, 0x0044c400, 0x0044c41f, 0x0044c43e, 0x0044c45e, 0x0044c47e, 0x0044c49e, 0x0044c4be, 0x0044c4de, 0x0044c5f2, 0x0044c63a, 0x0044c663, 0x0044c673, 0x0044c6a3, 0x0044c6e1
