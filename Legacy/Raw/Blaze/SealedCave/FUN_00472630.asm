C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00472630  section .text  offset fichier 0x71a30  1024 octets

  00472630  8b442408              mov       eax,[esp+8]
  00472634  53                    push      ebx
  00472635  48                    dec       eax
  00472636  55                    push      ebp
  00472637  56                    push      esi
  00472638  83f803                cmp       eax,3
  0047263b  57                    push      edi
  0047263c  7723                  ja        short 00472661h
  0047263e  ff24855c274700        jmp       dword [eax*4+47275Ch]
  00472645  bf00060000            mov       edi,600h
  0047264a  eb19                  jmp       short 00472665h
  0047264c  bf000a0000            mov       edi,0A00h
  00472651  eb12                  jmp       short 00472665h
  00472653  bf000e0000            mov       edi,0E00h
  00472658  eb0b                  jmp       short 00472665h
  0047265a  bf00120000            mov       edi,1200h
  0047265f  eb04                  jmp       short 00472665h

LAB_00472661:
  00472661  8b7c2418              mov       edi,[esp+18h]

LAB_00472665:
  00472665  8b742414              mov       esi,[esp+14h]
  00472669  668b5e32              mov       bx,[esi+32h]
  0047266d  8b4e20                mov       ecx,[esi+20h]
  00472670  8b5624                mov       edx,[esi+24h]
  00472673  8b6e1c                mov       ebp,[esi+1Ch]
  00472676  0fbfc3                movsx     eax,bx
  00472679  894c2418              mov       [esp+18h],ecx
  0047267d  8bcf                  mov       ecx,edi
  0047267f  03c8                  add       ecx,eax
  00472681  89542414              mov       [esp+14h],edx
  00472685  51                    push      ecx
  00472686  e891330d00            call      00545A1Ch
  0047268b  8d0480                lea       eax,[eax+eax*4]
  0047268e  8d1480                lea       edx,[eax+eax*4]
  00472691  8b461c                mov       eax,[esi+1Ch]
  00472694  c1e202                shl       edx,2
  00472697  03c2                  add       eax,edx
  00472699  89461c                mov       [esi+1Ch],eax
  0047269c  0fbf4632              movsx     eax,word [esi+32h]
  004726a0  03f8                  add       edi,eax
  004726a2  57                    push      edi
  004726a3  e86e330d00            call      00545A16h
  004726a8  8d0480                lea       eax,[eax+eax*4]
  004726ab  8d0c80                lea       ecx,[eax+eax*4]
  004726ae  8b4624                mov       eax,[esi+24h]
  004726b1  c1e102                shl       ecx,2
  004726b4  03c1                  add       eax,ecx
  004726b6  894624                mov       [esi+24h],eax
  004726b9  e8824a0d00            call      00547140h
  004726be  99                    cdq
  004726bf  33c2                  xor       eax,edx
  004726c1  bf07000000            mov       edi,7
  004726c6  2bc2                  sub       eax,edx
  004726c8  25ff000000            and       eax,0FFh
  004726cd  33c2                  xor       eax,edx
  004726cf  2bc2                  sub       eax,edx
  004726d1  0580030000            add       eax,380h
  004726d6  66014632              add       [esi+32h],ax

LAB_004726da:
  004726da  6a00                  push      0
  004726dc  6a00                  push      0
  004726de  6aff                  push      0FFFFFFFFh
  004726e0  6a00                  push      0
  004726e2  6a32                  push      32h
  004726e4  56                    push      esi
  004726e5  e8265affff            call      00468110h
  004726ea  83c418                add       esp,18h
  004726ed  4f                    dec       edi
  004726ee  75ea                  jne       short 004726DAh
  004726f0  e84b4a0d00            call      00547140h
  004726f5  99                    cdq
  004726f6  b90a000000            mov       ecx,0Ah
  004726fb  f7f9                  idiv      ecx
  004726fd  83fa01                cmp       edx,1
  00472700  753d                  jne       short 0047273Fh
  00472702  e8394a0d00            call      00547140h
  00472707  99                    cdq
  00472708  b964000000            mov       ecx,64h
  0047270d  6a00                  push      0
  0047270f  f7f9                  idiv      ecx
  00472711  6a00                  push      0
  00472713  6800080000            push      800h
  00472718  83ea32                sub       edx,32h
  0047271b  81c200040000          add       edx,400h
  00472721  52                    push      edx
  00472722  8b15cc975e00          mov       edx,[5E97CCh]
  00472728  8b82a8000000          mov       eax,[edx+0A8h]
  0047272e  8b08                  mov       ecx,[eax]
  00472730  660fb65136            movzx     dx,byte [ecx+36h]
  00472735  52                    push      edx
  00472736  56                    push      esi
  00472737  e84459ffff            call      00468080h
  0047273c  83c418                add       esp,18h

LAB_0047273f:
  0047273f  8b442418              mov       eax,[esp+18h]
  00472743  8b4c2414              mov       ecx,[esp+14h]
  00472747  896e1c                mov       [esi+1Ch],ebp
  0047274a  894620                mov       [esi+20h],eax
  0047274d  894e24                mov       [esi+24h],ecx
  00472750  66895e32              mov       [esi+32h],bx
  00472754  5f                    pop       edi
  00472755  5e                    pop       esi
  00472756  5d                    pop       ebp
  00472757  5b                    pop       ebx
  00472758  c3                    ret
  00472759  90                    nop
  0047275a  8bff                  mov       edi,edi
  0047275c  45                    inc       ebp
  0047275d  2647                  inc       edi
  0047275f  004c2647              add       [esi+47h],cl
  00472763  005326                add       [ebx+26h],dl
  00472766  47                    inc       edi
  00472767  005a26                add       [edx+26h],bl
  0047276a  47                    inc       edi
  0047276b  00909090908a          add       [eax-756F6F70h],dl
  00472771  0d38e55d00            or        eax,5DE538h
  00472776  33c0                  xor       eax,eax
  00472778  8ac1                  mov       al,cl
  0047277a  8d1480                lea       edx,[eax+eax*4]
  0047277d  c1e203                shl       edx,3
  00472780  2bd0                  sub       edx,eax
  00472782  8b0495c0935e00        mov       eax,[edx*4+5E93C0h]
  00472789  a900000080            test      eax,80000000h
  0047278e  740d                  je        short 0047279Dh
  00472790  a900000150            test      eax,50010000h
  00472795  7506                  jne       short 0047279Dh
  00472797  6633c0                xor       ax,ax
  0047279a  8ac1                  mov       al,cl
  0047279c  c3                    ret

LAB_0047279d:
  0047279d  33c0                  xor       eax,eax

LAB_0047279f:
  0047279f  0fbfc8                movsx     ecx,ax
  004727a2  8d1489                lea       edx,[ecx+ecx*4]
  004727a5  c1e203                shl       edx,3
  004727a8  2bd1                  sub       edx,ecx
  004727aa  8b0c95c0935e00        mov       ecx,[edx*4+5E93C0h]
  004727b1  f7c100000080          test      ecx,80000000h
  004727b7  7408                  je        short 004727C1h
  004727b9  f7c100000150          test      ecx,50010000h
  004727bf  7407                  je        short 004727C8h

LAB_004727c1:
  004727c1  40                    inc       eax
  004727c2  663d0300              cmp       ax,3
  004727c6  7ed7                  jle       short 0047279Fh

LAB_004727c8:
  004727c8  c3                    ret
  004727c9  90                    nop
  004727ca  90                    nop
  004727cb  90                    nop
  004727cc  90                    nop
  004727cd  90                    nop
  004727ce  90                    nop
  004727cf  90                    nop
  004727d0  53                    push      ebx
  004727d1  55                    push      ebp
  004727d2  56                    push      esi
  004727d3  8b742410              mov       esi,[esp+10h]
  004727d7  33db                  xor       ebx,ebx
  004727d9  57                    push      edi
  004727da  668b6e10              mov       bp,[esi+10h]
  004727de  ba01000000            mov       edx,1
  004727e3  663beb                cmp       bp,bx
  004727e6  7560                  jne       short 00472848h
  004727e8  0fbf4630              movsx     eax,word [esi+30h]
  004727ec  8bc8                  mov       ecx,eax
  004727ee  66899e9a000000        mov       [esi+9Ah],bx
  004727f5  81e1ff000000          and       ecx,0FFh
  004727fb  66895e30              mov       [esi+30h],bx
  004727ff  c1f808                sar       eax,8
  00472802  66817e14c800          cmp       word [esi+14h],0C8h
  00472808  894e44                mov       [esi+44h],ecx
  0047280b  89464c                mov       [esi+4Ch],eax
  0047280e  7505                  jne       short 00472815h
  00472810  895648                mov       [esi+48h],edx
  00472813  eb03                  jmp       short 00472818h

LAB_00472815:
  00472815  895e48                mov       [esi+48h],ebx

LAB_00472818:
  00472818  668b4616              mov       ax,[esi+16h]
  0047281c  663bc2                cmp       ax,dx
  0047281f  750d                  jne       short 0047282Eh
  00472821  66895e16              mov       [esi+16h],bx
  00472825  66899698000000        mov       [esi+98h],dx
  0047282c  eb1a                  jmp       short 00472848h

LAB_0047282e:
  0047282e  663d0200              cmp       ax,2
  00472832  750d                  jne       short 00472841h
  00472834  66895e16              mov       [esi+16h],bx
  00472838  66898698000000        mov       [esi+98h],ax
  0047283f  eb07                  jmp       short 00472848h

LAB_00472841:
  00472841  66899e98000000        mov       [esi+98h],bx

LAB_00472848:
  00472848  668b4616              mov       ax,[esi+16h]
  0047284c  663bc3                cmp       ax,bx
  0047284f  7543                  jne       short 00472894h
  00472851  6683fd0a              cmp       bp,0Ah
  00472855  7e3d                  jle       short 00472894h
  00472857  33c0                  xor       eax,eax
  00472859  33d2                  xor       edx,edx
  0047285b  8a5609                mov       dl,[esi+9]
  0047285e  bf00000008            mov       edi,8000000h

LAB_00472863:
  00472863  0fbfc8                movsx     ecx,ax
  00472866  bb01000000            mov       ebx,1
  0047286b  d3e3                  shl       ebx,cl
  0047286d  85da                  test      edx,ebx
  0047286f  7415                  je        short 00472886h
  00472871  8d1c89                lea       ebx,[ecx+ecx*4]
  00472874  c1e303                shl       ebx,3
  00472877  2bd9                  sub       ebx,ecx
  00472879  853c9d50945e00        test      [ebx*4+5E9450h],edi
  00472880  0f85b7050000          jne       near 00472E3Dh

LAB_00472886:
  00472886  40                    inc       eax
  00472887  663d0300              cmp       ax,3
  0047288b  7ed6                  jle       short 00472863h
  0047288d  33db                  xor       ebx,ebx
  0047288f  ba01000000            mov       edx,1

LAB_00472894:
  00472894  66ff8e9a000000        dec       word [esi+9Ah]
  0047289b  66399e9a000000        cmp       [esi+9Ah],bx
  004728a2  7d0a                  jge       short 004728AEh
  004728a4  66899e9a000000        mov       [esi+9Ah],bx
  004728ab  663bdb                cmp       bx,bx

LAB_004728ae:
  004728ae  7408                  je        short 004728B8h
  004728b0  668bc2                mov       ax,dx
  004728b3  5f                    pop       edi
  004728b4  5e                    pop       esi
  004728b5  5d                    pop       ebp
  004728b6  5b                    pop       ebx
  004728b7  c3                    ret

LAB_004728b8:
  004728b8  8d4501                lea       eax,[ebp+1]
  004728bb  bf63000000            mov       edi,63h
  004728c0  663d0a00              cmp       ax,0Ah
  004728c4  66894610              mov       [esi+10h],ax
  004728c8  7e0c                  jle       short 004728D6h
  004728ca  66817e14c800          cmp       word [esi+14h],0C8h
  004728d0  7504                  jne       short 004728D6h
  004728d2  66897e14              mov       [esi+14h],di

LAB_004728d6:
  004728d6  663dc800              cmp       ax,0C8h
  004728da  7e06                  jle       short 004728E2h
  004728dc  66c746101000          mov       word [esi+10h],10h

LAB_004728e2:
  004728e2  668b4e14              mov       cx,[esi+14h]
  004728e6  8b466c                mov       eax,[esi+6Ch]
  004728e9  663bcf                cmp       cx,di
  004728ec  bd40000000            mov       ebp,40h
  004728f1  0f85bd000000          jne       near 004729B4h
  004728f7  66395e16              cmp       [esi+16h],bx
  004728fb  7546                  jne       short 00472943h
  004728fd  f60640                test      byte [esi],40h
  00472900  7441                  je        short 00472943h
  00472902  395648                cmp       [esi+48h],edx
  00472905  753c                  jne       short 00472943h
  00472907  f70000000100          test      dword [eax],10000h
  0047290d  7434                  je        short 00472943h
  0047290f  68000c0000            push      0C00h
  00472914  68000c0000            push      0C00h
  00472919  6800030000            push      300h
  0047291e  6a1d                  push      1Dh
  00472920  e87ba50c00            call      0053CEA0h
  00472925  a1c4975e00            mov       eax,[5E97C4h]
  0047292a  8b566c                mov       edx,[esi+6Ch]
  0047292d  83c410                add       esp,10h
  00472930  8b4870                mov       ecx,[eax+70h]
  00472933  53                    push      ebx
  00472934  68b0335600            push      5633B0h
  00472939  52                    push      edx
  0047293a  8b9188040000          mov       edx,[ecx+488h]
  00472940  52                    push      edx
  00472941  eb51                  jmp       short 00472994h

LAB_00472943:
  00472943  663bcf                cmp       cx,di
  00472946  756c                  jne       short 004729B4h
  00472948  66395e16              cmp       [esi+16h],bx
  0047294c  7566                  jne       short 004729B4h
  0047294e  f60640                test      byte [esi],40h
  00472951  7461                  je        short 004729B4h
  00472953  807e0dff              cmp       byte [esi+0Dh],0FFh
  00472957  755b                  jne       short 004729B4h
  00472959  f70000000100          test      dword [eax],10000h
  0047295f  7553                  jne       short 004729B4h
  00472961  68000c0000            push      0C00h
  00472966  68000c0000            push      0C00h
  0047296b  6800030000            push      300h
  00472970  6a1d                  push      1Dh
  00472972  e829a50c00            call      0053CEA0h
  00472977  8b0dc4975e00          mov       ecx,[5E97C4h]
  0047297d  8b466c                mov       eax,[esi+6Ch]
  00472980  83c410                add       esp,10h
  00472983  8b5170                mov       edx,[ecx+70h]
  00472986  53                    push      ebx
  00472987  68b0335600            push      5633B0h
  0047298c  50                    push      eax
  0047298d  8b8288040000          mov       eax,[edx+488h]
  00472993  50                    push      eax

LAB_00472994:
  00472994  e8b7190d00            call      00544350h
  00472999  892dc8995e00          mov       [5E99C8h],ebp
  0047299f  83c410                add       esp,10h
  004729a2  66c7869a0000000500    mov       word [esi+9Ah],5
  004729ab  66b80100              mov       ax,1
  004729af  5f                    pop       edi
  004729b0  5e                    pop       esi
  004729b1  5d                    pop       ebp
  004729b2  5b                    pop       ebx
  004729b3  c3                    ret

LAB_004729b4:
  004729b4  8a5609                mov       dl,[esi+9]
  004729b7  f6c20f                test      dl,0Fh
  004729ba  7406                  je        short 004729C2h
  004729bc  8126ffffffbf          and       dword [esi],0BFFFFFFFh

LAB_004729c2:
  004729c2  8b06                  mov       eax,[esi]
  004729c4  f6c420                test      ah,20h
  004729c7  7406                  je        short 004729CFh
  004729c9  80e2f0                and       dl,0F0h
  004729cc  885609                mov       [esi+9],dl

LAB_004729cf:
  004729cf  663bcf                cmp       cx,di
  004729d2  0f845c040000          je        near 00472E34h
  004729d8  a900000040            test      eax,40000000h
  004729dd  7414                  je        short 004729F3h
  004729df  663bcb                cmp       cx,bx
  004729e2  0f844c040000          je        near 00472E34h
  004729e8  6681f9c800            cmp       cx,0C8h
  004729ed  0f8441040000          je        near 00472E34h

LAB_004729f3:
  004729f3  663bcb                cmp       cx,bx
  004729f6  7561                  jne       short 00472A59h
  004729f8  66837e100f            cmp       word [esi+10h],0Fh
  004729fd  7d3f                  jge       short 00472A3Eh
  004729ff  8b4e4c                mov       ecx,[esi+4Ch]
  00472a02  3bcb                  cmp       ecx,ebx
  00472a04  7438                  je        short 00472A3Eh
  00472a06  33c0                  xor       eax,eax
  00472a08  53                    push      ebx
  00472a09  a038e55d00            mov       al,[5DE538h]
  00472a0e  68b0335600            push      5633B0h
  00472a13  8d1480                lea       edx,[eax+eax*4]
  00472a16  c1e203                shl       edx,3
  00472a19  2bd0                  sub       edx,eax
  00472a1b  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00472a22  8b15cc975e00          mov       edx,[5E97CCh]
  00472a28  50                    push      eax
  00472a29  8b4270                mov       eax,[edx+70h]
  00472a2c  8b0c88                mov       ecx,[eax+ecx*4]
  00472a2f  51                    push      ecx

26 cible(s) de saut interne : 0x00472661, 0x00472665, 0x004726da, 0x0047273f, 0x0047279d, 0x0047279f, 0x004727c1, 0x004727c8, 0x00472815, 0x00472818, 0x0047282e, 0x00472841, 0x00472848, 0x00472863, 0x00472886, 0x00472894, 0x004728ae, 0x004728b8, 0x004728d6, 0x004728e2, 0x00472943, 0x00472994, 0x004729b4, 0x004729c2, 0x004729cf, 0x004729f3
