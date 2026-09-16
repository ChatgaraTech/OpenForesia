Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00451460  section .text  offset fichier 0x50860  455 octets

  00451460  53                    push      ebx
  00451461  56                    push      esi
  00451462  8b74240c              mov       esi,[esp+0Ch]
  00451466  57                    push      edi
  00451467  33ff                  xor       edi,edi
  00451469  f60640                test      byte [esi],40h
  0045146c  7475                  je        short 004514E3h
  0045146e  66397e10              cmp       [esi+10h],di
  00451472  756f                  jne       short 004514E3h
  00451474  8b466c                mov       eax,[esi+6Ch]
  00451477  85c0                  test      eax,eax
  00451479  7505                  jne       short 00451480h
  0045147b  b858e55d00            mov       eax,5DE558h

LAB_00451480:
  00451480  8b15b4876000          mov       edx,[6087B4h]
  00451486  33c9                  xor       ecx,ecx
  00451488  8a4808                mov       cl,[eax+8]
  0045148b  8b12                  mov       edx,[edx]
  0045148d  c1e10d                shl       ecx,0Dh
  00451490  8a8c1150010f00        mov       cl,[ecx+edx+0F0150h]
  00451497  80e107                and       cl,7
  0045149a  80f906                cmp       cl,6
  0045149d  0f8580010000          jne       near 00451623h
  004514a3  f70000000100          test      dword [eax],10000h
  004514a9  0f8574010000          jne       near 00451623h
  004514af  8b15c4975e00          mov       edx,[5E97C4h]
  004514b5  6a00                  push      0
  004514b7  68b0335600            push      5633B0h
  004514bc  50                    push      eax
  004514bd  8b4270                mov       eax,[edx+70h]
  004514c0  8b88a8040000          mov       ecx,[eax+4A8h]
  004514c6  51                    push      ecx
  004514c7  e8842e0f00            call      00544350h
  004514cc  c705c8995e0040000000  mov       dword [5E99C8h],40h
  004514d6  83c410                add       esp,10h
  004514d9  66c746100100          mov       word [esi+10h],1
  004514df  5f                    pop       edi
  004514e0  5e                    pop       esi
  004514e1  5b                    pop       ebx
  004514e2  c3                    ret

LAB_004514e3:
  004514e3  668b4610              mov       ax,[esi+10h]
  004514e7  ba06000000            mov       edx,6
  004514ec  663d0100              cmp       ax,1
  004514f0  7406                  je        short 004514F8h
  004514f2  663d0200              cmp       ax,2
  004514f6  7518                  jne       short 00451510h

LAB_004514f8:
  004514f8  8b7e6c                mov       edi,[esi+6Ch]
  004514fb  33c0                  xor       eax,eax
  004514fd  8a4704                mov       al,[edi+4]
  00451500  66895718              mov       [edi+18h],dx
  00451504  8b0c8580965e00        mov       ecx,[eax*4+5E9680h]
  0045150b  8b4918                mov       ecx,[ecx+18h]
  0045150e  eb04                  jmp       short 00451514h

LAB_00451510:
  00451510  8b4c2410              mov       ecx,[esp+10h]

LAB_00451514:
  00451514  668b4610              mov       ax,[esi+10h]
  00451518  663d0100              cmp       ax,1
  0045151c  7534                  jne       short 00451552h
  0045151e  8b3e                  mov       edi,[esi]
  00451520  8b466c                mov       eax,[esi+6Ch]
  00451523  81cf00200000          or        edi,2000h
  00451529  66c746100200          mov       word [esi+10h],2
  0045152f  893e                  mov       [esi],edi
  00451531  6a00                  push      0
  00451533  66895018              mov       [eax+18h],dx
  00451537  66c7401a0000          mov       word [eax+1Ah],0
  0045153d  e8be3d0e00            call      00535300h
  00451542  8b06                  mov       eax,[esi]
  00451544  83c404                add       esp,4
  00451547  25fffffffb            and       eax,0FBFFFFFFh
  0045154c  8906                  mov       [esi],eax
  0045154e  5f                    pop       edi
  0045154f  5e                    pop       esi
  00451550  5b                    pop       ebx
  00451551  c3                    ret

LAB_00451552:
  00451552  663d0200              cmp       ax,2
  00451556  bb03000000            mov       ebx,3
  0045155b  754a                  jne       short 004515A7h
  0045155d  668b471a              mov       ax,[edi+1Ah]
  00451561  33d2                  xor       edx,edx
  00451563  668b5116              mov       dx,[ecx+16h]
  00451567  0fbfc8                movsx     ecx,ax
  0045156a  41                    inc       ecx
  0045156b  3bca                  cmp       ecx,edx
  0045156d  7d07                  jge       short 00451576h
  0045156f  40                    inc       eax
  00451570  6689471a              mov       [edi+1Ah],ax
  00451574  eb31                  jmp       short 004515A7h

LAB_00451576:
  00451576  6a00                  push      0
  00451578  6a00                  push      0
  0045157a  6800100000            push      1000h
  0045157f  6800030000            push      300h
  00451584  6a08                  push      8
  00451586  56                    push      esi
  00451587  e8f46a0100            call      00468080h
  0045158c  8b07                  mov       eax,[edi]
  0045158e  83c418                add       esp,18h
  00451591  0d00000002            or        eax,2000000h
  00451596  8907                  mov       [edi],eax
  00451598  66895e10              mov       [esi+10h],bx
  0045159c  66c746120000          mov       word [esi+12h],0
  004515a2  e8593e0e00            call      00535400h

LAB_004515a7:
  004515a7  66395e10              cmp       [esi+10h],bx
  004515ab  7568                  jne       short 00451615h
  004515ad  8b5e20                mov       ebx,[esi+20h]
  004515b0  8b0e                  mov       ecx,[esi]
  004515b2  80cd40                or        ch,40h
  004515b5  66ff4612              inc       word [esi+12h]
  004515b9  8d9300e00100          lea       edx,[ebx+1E000h]
  004515bf  890e                  mov       [esi],ecx
  004515c1  895620                mov       [esi+20h],edx
  004515c4  bf09000000            mov       edi,9

LAB_004515c9:
  004515c9  6a00                  push      0
  004515cb  6a00                  push      0
  004515cd  6aff                  push      0FFFFFFFFh
  004515cf  6a00                  push      0
  004515d1  6a36                  push      36h
  004515d3  56                    push      esi
  004515d4  e8376b0100            call      00468110h
  004515d9  83c418                add       esp,18h
  004515dc  4f                    dec       edi
  004515dd  75ea                  jne       short 004515C9h
  004515df  6a00                  push      0
  004515e1  6a00                  push      0
  004515e3  6aff                  push      0FFFFFFFFh
  004515e5  6a00                  push      0
  004515e7  6a31                  push      31h
  004515e9  56                    push      esi
  004515ea  e8216b0100            call      00468110h
  004515ef  83c418                add       esp,18h
  004515f2  6a00                  push      0
  004515f4  6a00                  push      0
  004515f6  6aff                  push      0FFFFFFFFh
  004515f8  6a00                  push      0
  004515fa  6a31                  push      31h
  004515fc  56                    push      esi
  004515fd  e80e6b0100            call      00468110h
  00451602  83c418                add       esp,18h
  00451605  66837e1205            cmp       word [esi+12h],5
  0045160a  7506                  jne       short 00451612h
  0045160c  66c746106300          mov       word [esi+10h],63h

LAB_00451612:
  00451612  895e20                mov       [esi+20h],ebx

LAB_00451615:
  00451615  66837e1063            cmp       word [esi+10h],63h
  0045161a  7507                  jne       short 00451623h
  0045161c  8b06                  mov       eax,[esi]
  0045161e  80cc60                or        ah,60h
  00451621  8906                  mov       [esi],eax

LAB_00451623:
  00451623  5f                    pop       edi
  00451624  5e                    pop       esi
  00451625  5b                    pop       ebx
  00451626  c3                    ret

12 cible(s) de saut interne : 0x00451480, 0x004514e3, 0x004514f8, 0x00451510, 0x00451514, 0x00451552, 0x00451576, 0x004515a7, 0x004515c9, 0x00451612, 0x00451615, 0x00451623
