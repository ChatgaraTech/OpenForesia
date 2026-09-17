Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00491650  section .text  offset fichier 0x90a50  2896 octets

  00491650  81ec84000000          sub       esp,84h
  00491656  53                    push      ebx
  00491657  55                    push      ebp
  00491658  56                    push      esi
  00491659  8bb42494000000        mov       esi,[esp+94h]
  00491660  33c0                  xor       eax,eax
  00491662  57                    push      edi
  00491663  8a4e41                mov       cl,[esi+41h]
  00491666  6689442430            mov       [esp+30h],ax
  0049166b  84c9                  test      cl,cl
  0049166d  66c7442432ceff        mov       word [esp+32h],0FFCEh
  00491674  66c74424343200        mov       word [esp+34h],32h
  0049167b  66c74424366400        mov       word [esp+36h],64h
  00491682  6689442438            mov       [esp+38h],ax
  00491687  66c744243a8200        mov       word [esp+3Ah],82h
  0049168e  bb01000000            mov       ebx,1
  00491693  750e                  jne       short 004916A3h
  00491695  50                    push      eax
  00491696  50                    push      eax
  00491697  56                    push      esi
  00491698  885e41                mov       [esi+41h],bl
  0049169b  e8f0fbffff            call      00491290h
  004916a0  83c40c                add       esp,0Ch

LAB_004916a3:
  004916a3  8b461c                mov       eax,[esi+1Ch]
  004916a6  8b4e20                mov       ecx,[esi+20h]
  004916a9  8b5624                mov       edx,[esi+24h]
  004916ac  8d6e1c                lea       ebp,[esi+1Ch]
  004916af  89442424              mov       [esp+24h],eax
  004916b3  8b4664                mov       eax,[esi+64h]
  004916b6  894c2428              mov       [esp+28h],ecx
  004916ba  8b4e60                mov       ecx,[esi+60h]
  004916bd  8954242c              mov       [esp+2Ch],edx
  004916c1  668b5632              mov       dx,[esi+32h]
  004916c5  89442444              mov       [esp+44h],eax
  004916c9  8b467c                mov       eax,[esi+7Ch]
  004916cc  894c2440              mov       [esp+40h],ecx
  004916d0  668b8e90000000        mov       cx,[esi+90h]
  004916d7  6689942482000000      mov       [esp+82h],dx
  004916df  8b9680000000          mov       edx,[esi+80h]
  004916e5  8944245c              mov       [esp+5Ch],eax
  004916e9  8b8684000000          mov       eax,[esi+84h]
  004916ef  66894c2448            mov       [esp+48h],cx
  004916f4  8b8e88000000          mov       ecx,[esi+88h]
  004916fa  8b7e6c                mov       edi,[esi+6Ch]
  004916fd  8954244c              mov       [esp+4Ch],edx
  00491701  8b968c000000          mov       edx,[esi+8Ch]
  00491707  8944243c              mov       [esp+3Ch],eax
  0049170b  894c2458              mov       [esp+58h],ecx
  0049170f  8d442460              lea       eax,[esp+60h]
  00491713  89542450              mov       [esp+50h],edx
  00491717  8d4c2468              lea       ecx,[esp+68h]
  0049171b  50                    push      eax
  0049171c  8d542468              lea       edx,[esp+68h]
  00491720  51                    push      ecx
  00491721  52                    push      edx
  00491722  56                    push      esi
  00491723  e858720a00            call      00538980h
  00491728  83c410                add       esp,10h
  0049172b  8d442454              lea       eax,[esp+54h]
  0049172f  8d4c241c              lea       ecx,[esp+1Ch]
  00491733  8d542418              lea       edx,[esp+18h]
  00491737  50                    push      eax
  00491738  51                    push      ecx
  00491739  8d44241c              lea       eax,[esp+1Ch]
  0049173d  52                    push      edx
  0049173e  8d4c241c              lea       ecx,[esp+1Ch]
  00491742  50                    push      eax
  00491743  51                    push      ecx
  00491744  56                    push      esi
  00491745  e8d6470a00            call      00535F20h
  0049174a  8b4c2428              mov       ecx,[esp+28h]
  0049174e  8b54246c              mov       edx,[esp+6Ch]
  00491752  83c418                add       esp,18h
  00491755  b840000000            mov       eax,40h
  0049175a  2bc1                  sub       eax,ecx
  0049175c  52                    push      edx
  0049175d  56                    push      esi
  0049175e  89442428              mov       [esp+28h],eax
  00491762  e879460400            call      004D5DE0h
  00491767  8b4500                mov       eax,[ebp]
  0049176a  8b571c                mov       edx,[edi+1Ch]
  0049176d  8b4e20                mov       ecx,[esi+20h]
  00491770  2bc2                  sub       eax,edx
  00491772  8b5720                mov       edx,[edi+20h]
  00491775  83c408                add       esp,8
  00491778  2bca                  sub       ecx,edx
  0049177a  8b5624                mov       edx,[esi+24h]
  0049177d  2b5724                sub       edx,[edi+24h]
  00491780  c1f80c                sar       eax,0Ch
  00491783  8bf8                  mov       edi,eax
  00491785  c1f90c                sar       ecx,0Ch
  00491788  0faff8                imul      edi,eax
  0049178b  8bc1                  mov       eax,ecx
  0049178d  c1fa0c                sar       edx,0Ch
  00491790  0fafc1                imul      eax,ecx
  00491793  8bca                  mov       ecx,edx
  00491795  03f8                  add       edi,eax
  00491797  0fafca                imul      ecx,edx
  0049179a  03f9                  add       edi,ecx
  0049179c  57                    push      edi
  0049179d  e86e420b00            call      00545A10h
  004917a2  8b4c241c              mov       ecx,[esp+1Ch]
  004917a6  89442414              mov       [esp+14h],eax
  004917aa  85c9                  test      ecx,ecx
  004917ac  0f84db040000          je        near 00491C8Dh
  004917b2  8b16                  mov       edx,[esi]
  004917b4  33c0                  xor       eax,eax
  004917b6  8a4604                mov       al,[esi+4]
  004917b9  81e2fffffffd          and       edx,0FDFFFFFFh
  004917bf  0fbf7e18              movsx     edi,word [esi+18h]
  004917c3  8916                  mov       [esi],edx
  004917c5  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004917cc  f7c200000001          test      edx,1000000h
  004917d2  8b3cb8                mov       edi,[eax+edi*4]
  004917d5  741d                  je        short 004917F4h
  004917d7  66ff461a              inc       word [esi+1Ah]
  004917db  668b461a              mov       ax,[esi+1Ah]
  004917df  33d2                  xor       edx,edx
  004917e1  668b5716              mov       dx,[edi+16h]
  004917e5  0fbfc0                movsx     eax,ax
  004917e8  3bc2                  cmp       eax,edx
  004917ea  7c2f                  jl        short 0049181Bh
  004917ec  66c7461a0000          mov       word [esi+1Ah],0
  004917f2  eb27                  jmp       short 0049181Bh

LAB_004917f4:
  004917f4  66ff461a              inc       word [esi+1Ah]
  004917f8  668b461a              mov       ax,[esi+1Ah]
  004917fc  33db                  xor       ebx,ebx
  004917fe  668b5f16              mov       bx,[edi+16h]
  00491802  0fbff8                movsx     edi,ax
  00491805  3bfb                  cmp       edi,ebx
  00491807  7c0d                  jl        short 00491816h
  00491809  48                    dec       eax
  0049180a  81ca00000002          or        edx,2000000h
  00491810  6689461a              mov       [esi+1Ah],ax
  00491814  8916                  mov       [esi],edx

LAB_00491816:
  00491816  bb01000000            mov       ebx,1

LAB_0049181b:
  0049181b  8a4642                mov       al,[esi+42h]
  0049181e  3c05                  cmp       al,5
  00491820  8b06                  mov       eax,[esi]
  00491822  7504                  jne       short 00491828h
  00491824  0c10                  or        al,10h
  00491826  eb02                  jmp       short 0049182Ah

LAB_00491828:
  00491828  24ef                  and       al,0EFh

LAB_0049182a:
  0049182a  8906                  mov       [esi],eax
  0049182c  8a06                  mov       al,[esi]
  0049182e  a802                  test      al,2
  00491830  0f8488000000          je        near 004918BEh
  00491836  66837e1a0b            cmp       word [esi+1Ah],0Bh
  0049183b  7576                  jne       short 004918B3h
  0049183d  8d4c2430              lea       ecx,[esp+30h]
  00491841  6a00                  push      0
  00491843  51                    push      ecx
  00491844  55                    push      ebp
  00491845  56                    push      esi
  00491846  e8e5830800            call      00519C30h
  0049184b  8bf8                  mov       edi,eax
  0049184d  a1b8e05d00            mov       eax,[5DE0B8h]
  00491852  83c410                add       esp,10h
  00491855  85c0                  test      eax,eax
  00491857  7456                  je        short 004918AFh
  00491859  66837e1a0b            cmp       word [esi+1Ah],0Bh
  0049185e  754f                  jne       short 004918AFh
  00491860  8b0f                  mov       ecx,[edi]
  00491862  57                    push      edi
  00491863  83c940                or        ecx,40h
  00491866  56                    push      esi
  00491867  66c787960000001000    mov       word [edi+96h],10h
  00491870  890f                  mov       [edi],ecx
  00491872  e809830a00            call      00539B80h
  00491877  83c408                add       esp,8
  0049187a  8d571c                lea       edx,[edi+1Ch]
  0049187d  52                    push      edx
  0049187e  6800180000            push      1800h
  00491883  6800030000            push      300h
  00491888  6a03                  push      3
  0049188a  e891c20a00            call      0053DB20h
  0049188f  83c410                add       esp,10h
  00491892  e8a9580b00            call      00547140h
  00491897  99                    cdq
  00491898  b903000000            mov       ecx,3
  0049189d  f7f9                  idiv      ecx
  0049189f  3bd3                  cmp       edx,ebx
  004918a1  750c                  jne       short 004918AFh
  004918a3  6a00                  push      0
  004918a5  57                    push      edi
  004918a6  56                    push      esi
  004918a7  e8b4080000            call      00492160h
  004918ac  83c40c                add       esp,0Ch

LAB_004918af:
  004918af  8b4c241c              mov       ecx,[esp+1Ch]

LAB_004918b3:
  004918b3  8b16                  mov       edx,[esi]
  004918b5  84d3                  test      bl,dl
  004918b7  7505                  jne       short 004918BEh
  004918b9  83e2fd                and       edx,0FFFFFFFDh
  004918bc  8916                  mov       [esi],edx

LAB_004918be:
  004918be  33c0                  xor       eax,eax
  004918c0  8a4642                mov       al,[esi+42h]
  004918c3  83f80b                cmp       eax,0Bh
  004918c6  0f87c1030000          ja        near 00491C8Dh
  004918cc  ff248530214900        jmp       dword [eax*4+492130h]
  004918d3  33c9                  xor       ecx,ecx
  004918d5  66394e12              cmp       [esi+12h],cx
  004918d9  7518                  jne       short 004918F3h
  004918db  8b06                  mov       eax,[esi]
  004918dd  66894e18              mov       [esi+18h],cx
  004918e1  0d00000001            or        eax,1000000h
  004918e6  66894e1a              mov       [esi+1Ah],cx
  004918ea  8906                  mov       [esi],eax
  004918ec  66895e12              mov       [esi+12h],bx
  004918f0  894e5c                mov       [esi+5Ch],ecx

LAB_004918f3:
  004918f3  0fbf5610              movsx     edx,word [esi+10h]
  004918f7  39542414              cmp       [esp+14h],edx
  004918fb  7d09                  jge       short 00491906h
  004918fd  c6464300              mov       byte [esi+43h],0
  00491901  e987030000            jmp       00491C8Dh

LAB_00491906:
  00491906  8a4643                mov       al,[esi+43h]
  00491909  84c0                  test      al,al
  0049190b  0f84ac020000          je        near 00491BBDh
  00491911  fec8                  dec       al
  00491913  894e5c                mov       [esi+5Ch],ecx
  00491916  884643                mov       [esi+43h],al
  00491919  e96f030000            jmp       00491C8Dh
  0049191e  33c9                  xor       ecx,ecx
  00491920  66394e12              cmp       [esi+12h],cx
  00491924  7515                  jne       short 0049193Bh
  00491926  8b06                  mov       eax,[esi]
  00491928  66895e18              mov       [esi+18h],bx
  0049192c  0d00000001            or        eax,1000000h
  00491931  66894e1a              mov       [esi+1Ah],cx
  00491935  8906                  mov       [esi],eax
  00491937  66895e12              mov       [esi+12h],bx

LAB_0049193b:
  0049193b  8b442418              mov       eax,[esp+18h]
  0049193f  8d90c8000000          lea       edx,[eax+0C8h]
  00491945  81e2ff0f0000          and       edx,0FFFh
  0049194b  81fa90010000          cmp       edx,190h
  00491951  7f0c                  jg        short 0049195Fh
  00491953  c6464300              mov       byte [esi+43h],0
  00491957  894e5c                mov       [esi+5Ch],ecx
  0049195a  e92e030000            jmp       00491C8Dh

LAB_0049195f:
  0049195f  25ff0f0000            and       eax,0FFFh
  00491964  3d00080000            cmp       eax,800h
  00491969  7d15                  jge       short 00491980h
  0049196b  8b442410              mov       eax,[esp+10h]
  0049196f  894e5c                mov       [esi+5Ch],ecx
  00491972  0580000000            add       eax,80h
  00491977  66014632              add       [esi+32h],ax
  0049197b  e90d030000            jmp       00491C8Dh

LAB_00491980:
  00491980  8b542410              mov       edx,[esp+10h]
  00491984  b880ffffff            mov       eax,0FFFFFF80h
  00491989  2bc2                  sub       eax,edx
  0049198b  894e5c                mov       [esi+5Ch],ecx
  0049198e  66014632              add       [esi+32h],ax
  00491992  e9f6020000            jmp       00491C8Dh
  00491997  8b4c2410              mov       ecx,[esp+10h]
  0049199b  33c0                  xor       eax,eax
  0049199d  66394612              cmp       [esi+12h],ax
  004919a1  7525                  jne       short 004919C8h
  004919a3  8b16                  mov       edx,[esi]
  004919a5  66895e18              mov       [esi+18h],bx
  004919a9  81ca00000001          or        edx,1000000h
  004919af  6689461a              mov       [esi+1Ah],ax
  004919b3  8916                  mov       [esi],edx
  004919b5  8d14c9                lea       edx,[ecx+ecx*8]
  004919b8  c1e204                shl       edx,4
  004919bb  81c200500000          add       edx,5000h
  004919c1  66895e12              mov       [esi+12h],bx
  004919c5  89565c                mov       [esi+5Ch],edx

LAB_004919c8:
  004919c8  66395e12              cmp       [esi+12h],bx
  004919cc  0f85bb020000          jne       near 00491C8Dh
  004919d2  0fbf5610              movsx     edx,word [esi+10h]
  004919d6  39542414              cmp       [esp+14h],edx
  004919da  7d0c                  jge       short 004919E8h
  004919dc  c6464300              mov       byte [esi+43h],0
  004919e0  89465c                mov       [esi+5Ch],eax
  004919e3  e9a5020000            jmp       00491C8Dh

LAB_004919e8:
  004919e8  8a4643                mov       al,[esi+43h]
  004919eb  84c0                  test      al,al
  004919ed  7405                  je        short 004919F4h
  004919ef  fec8                  dec       al
  004919f1  884643                mov       [esi+43h],al

LAB_004919f4:
  004919f4  8b442418              mov       eax,[esp+18h]
  004919f8  8d9000010000          lea       edx,[eax+100h]
  004919fe  81e2ff0f0000          and       edx,0FFFh
  00491a04  81fa00020000          cmp       edx,200h
  00491a0a  0f8e7d020000          jle       near 00491C8Dh
  00491a10  25ff0f0000            and       eax,0FFFh
  00491a15  3d00080000            cmp       eax,800h
  00491a1a  7d10                  jge       short 00491A2Ch
  00491a1c  8d840980000000        lea       eax,[ecx+ecx+80h]
  00491a23  66014632              add       [esi+32h],ax
  00491a27  e961020000            jmp       00491C8Dh

LAB_00491a2c:
  00491a2c  03c9                  add       ecx,ecx
  00491a2e  b880ffffff            mov       eax,0FFFFFF80h
  00491a33  2bc1                  sub       eax,ecx
  00491a35  66014632              add       [esi+32h],ax
  00491a39  e94f020000            jmp       00491C8Dh
  00491a3e  66837e1200            cmp       word [esi+12h],0
  00491a43  7544                  jne       short 00491A89h
  00491a45  e8f6560b00            call      00547140h
  00491a4a  99                    cdq
  00491a4b  8b4c2410              mov       ecx,[esp+10h]
  00491a4f  33c2                  xor       eax,edx
  00491a51  2bc2                  sub       eax,edx
  00491a53  8b2e                  mov       ebp,[esi]
  00491a55  25ff0f0000            and       eax,0FFFh
  00491a5a  81cd00000001          or        ebp,1000000h
  00491a60  33c2                  xor       eax,edx
  00491a62  66895e18              mov       [esi+18h],bx
  00491a66  2bc2                  sub       eax,edx
  00491a68  8d14c9                lea       edx,[ecx+ecx*8]
  00491a6b  c1e204                shl       edx,4
  00491a6e  81c200500000          add       edx,5000h
  00491a74  66894610              mov       [esi+10h],ax
  00491a78  66c7461a0000          mov       word [esi+1Ah],0
  00491a7e  892e                  mov       [esi],ebp
  00491a80  66895e12              mov       [esi+12h],bx
  00491a84  89565c                mov       [esi+5Ch],edx
  00491a87  eb04                  jmp       short 00491A8Dh

LAB_00491a89:
  00491a89  8b4c2410              mov       ecx,[esp+10h]

LAB_00491a8d:
  00491a8d  66395e12              cmp       [esi+12h],bx
  00491a91  0f85f6010000          jne       near 00491C8Dh
  00491a97  668b4610              mov       ax,[esi+10h]
  00491a9b  663d0008              cmp       ax,800h
  00491a9f  7d06                  jge       short 00491AA7h
  00491aa1  8d4c0950              lea       ecx,[ecx+ecx+50h]
  00491aa5  eb0a                  jmp       short 00491AB1h

LAB_00491aa7:
  00491aa7  8d1409                lea       edx,[ecx+ecx]
  00491aaa  b9b0ffffff            mov       ecx,0FFFFFFB0h
  00491aaf  2bca                  sub       ecx,edx

LAB_00491ab1:
  00491ab1  66014e32              add       [esi+32h],cx
  00491ab5  8b5632                mov       edx,[esi+32h]
  00491ab8  2bc2                  sub       eax,edx
  00491aba  25ff0f0000            and       eax,0FFFh
  00491abf  7d02                  jge       short 00491AC3h
  00491ac1  f7d8                  neg       eax

LAB_00491ac3:
  00491ac3  3d58020000            cmp       eax,258h
  00491ac8  0f8dbf010000          jge       near 00491C8Dh
  00491ace  c6464300              mov       byte [esi+43h],0
  00491ad2  e9b6010000            jmp       00491C8Dh
  00491ad7  66837e1200            cmp       word [esi+12h],0
  00491adc  757a                  jne       short 00491B58h
  00491ade  8b06                  mov       eax,[esi]
  00491ae0  84c3                  test      bl,al
  00491ae2  7406                  je        short 00491AEAh
  00491ae4  c6464300              mov       byte [esi+43h],0
  00491ae8  eb6e                  jmp       short 00491B58h

LAB_00491aea:
  00491aea  25fffffffe            and       eax,0FEFFFFFFh
  00491aef  66895e18              mov       [esi+18h],bx
  00491af3  8906                  mov       [esi],eax
  00491af5  8b442410              mov       eax,[esp+10h]
  00491af9  0500010000            add       eax,100h
  00491afe  66c7461a0000          mov       word [esi+1Ah],0
  00491b04  c1e006                shl       eax,6
  00491b07  0fafc8                imul      ecx,eax
  00491b0a  c1f90c                sar       ecx,0Ch
  00491b0d  8bf9                  mov       edi,ecx
  00491b0f  89465c                mov       [esi+5Ch],eax
  00491b12  0fbf4e10              movsx     ecx,word [esi+10h]
  00491b16  51                    push      ecx
  00491b17  e8003f0b00            call      00545A1Ch
  00491b1c  0fafc7                imul      eax,edi
  00491b1f  0fbf5610              movsx     edx,word [esi+10h]
  00491b23  8b4e50                mov       ecx,[esi+50h]
  00491b26  52                    push      edx
  00491b27  c1f80c                sar       eax,0Ch
  00491b2a  03c8                  add       ecx,eax
  00491b2c  894e50                mov       [esi+50h],ecx
  00491b2f  e8e23e0b00            call      00545A16h
  00491b34  0fafc7                imul      eax,edi
  00491b37  8b5658                mov       edx,[esi+58h]
  00491b3a  8b4e54                mov       ecx,[esi+54h]
  00491b3d  c1f80c                sar       eax,0Ch
  00491b40  03d0                  add       edx,eax
  00491b42  8b06                  mov       eax,[esi]
  00491b44  81c100e00100          add       ecx,1E000h
  00491b4a  0bc3                  or        eax,ebx
  00491b4c  895658                mov       [esi+58h],edx
  00491b4f  894e54                mov       [esi+54h],ecx
  00491b52  8906                  mov       [esi],eax
  00491b54  66895e12              mov       [esi+12h],bx

LAB_00491b58:
  00491b58  841e                  test      [esi],bl
  00491b5a  0f852d010000          jne       near 00491C8Dh
  00491b60  c6464300              mov       byte [esi+43h],0
  00491b64  e924010000            jmp       00491C8Dh
  00491b69  668b4e12              mov       cx,[esi+12h]
  00491b6d  663bcb                cmp       cx,bx
  00491b70  751c                  jne       short 00491B8Eh
  00491b72  0fbf4610              movsx     eax,word [esi+10h]
  00491b76  39442414              cmp       [esp+14h],eax
  00491b7a  7d06                  jge       short 00491B82h
  00491b7c  c6464300              mov       byte [esi+43h],0
  00491b80  eb0c                  jmp       short 00491B8Eh

LAB_00491b82:
  00491b82  8a4643                mov       al,[esi+43h]
  00491b85  84c0                  test      al,al
  00491b87  7405                  je        short 00491B8Eh
  00491b89  fec8                  dec       al
  00491b8b  884643                mov       [esi+43h],al

LAB_00491b8e:
  00491b8e  6685c9                test      cx,cx
  00491b91  0f85f6000000          jne       near 00491C8Dh
  00491b97  8b442410              mov       eax,[esp+10h]
  00491b9b  8b3e                  mov       edi,[esi]
  00491b9d  66894e1a              mov       [esi+1Ah],cx
  00491ba1  81cf00000001          or        edi,1000000h
  00491ba7  8d0cc0                lea       ecx,[eax+eax*8]
  00491baa  66895e18              mov       [esi+18h],bx
  00491bae  c1e104                shl       ecx,4
  00491bb1  893e                  mov       [esi],edi
  00491bb3  66895e12              mov       [esi+12h],bx
  00491bb7  81c100500000          add       ecx,5000h

LAB_00491bbd:
  00491bbd  894e5c                mov       [esi+5Ch],ecx
  00491bc0  e9c8000000            jmp       00491C8Dh
  00491bc5  33c0                  xor       eax,eax
  00491bc7  66394612              cmp       [esi+12h],ax
  00491bcb  751f                  jne       short 00491BECh
  00491bcd  89465c                mov       [esi+5Ch],eax
  00491bd0  6689461a              mov       [esi+1Ah],ax
  00491bd4  8b06                  mov       eax,[esi]
  00491bd6  ba03000000            mov       edx,3
  00491bdb  25fffffffe            and       eax,0FEFFFFFFh
  00491be0  66895618              mov       [esi+18h],dx
  00491be4  8906                  mov       [esi],eax
  00491be6  66895e12              mov       [esi+12h],bx
  00491bea  eb05                  jmp       short 00491BF1h

LAB_00491bec:
  00491bec  ba03000000            mov       edx,3

LAB_00491bf1:
  00491bf1  66395e12              cmp       [esi+12h],bx
  00491bf5  8b4c2410              mov       ecx,[esp+10h]
  00491bf9  7548                  jne       short 00491C43h
  00491bfb  66837e1a08            cmp       word [esi+1Ah],8
  00491c00  7506                  jne       short 00491C08h
  00491c02  66c746120200          mov       word [esi+12h],2

LAB_00491c08:
  00491c08  8b442418              mov       eax,[esp+18h]
  00491c0c  8db800010000          lea       edi,[eax+100h]
  00491c12  81e7ff0f0000          and       edi,0FFFh
  00491c18  81ff00020000          cmp       edi,200h
  00491c1e  7e23                  jle       short 00491C43h
  00491c20  25ff0f0000            and       eax,0FFFh
  00491c25  3d00080000            cmp       eax,800h
  00491c2a  7d09                  jge       short 00491C35h
  00491c2c  8d840980000000        lea       eax,[ecx+ecx+80h]
  00491c33  eb0a                  jmp       short 00491C3Fh

LAB_00491c35:
  00491c35  8d3c09                lea       edi,[ecx+ecx]
  00491c38  b880ffffff            mov       eax,0FFFFFF80h
  00491c3d  2bc7                  sub       eax,edi

LAB_00491c3f:
  00491c3f  66014632              add       [esi+32h],ax

LAB_00491c43:
  00491c43  66837e1202            cmp       word [esi+12h],2
  00491c48  752f                  jne       short 00491C79h
  00491c4a  8b06                  mov       eax,[esi]
  00491c4c  84c3                  test      bl,al
  00491c4e  7406                  je        short 00491C56h
  00491c50  c6464300              mov       byte [esi+43h],0
  00491c54  eb37                  jmp       short 00491C8Dh

LAB_00491c56:
  00491c56  8b7e54                mov       edi,[esi+54h]
  00491c59  8d0cc9                lea       ecx,[ecx+ecx*8]
  00491c5c  c1e104                shl       ecx,4
  00491c5f  81c100e00000          add       ecx,0E000h
  00491c65  81c700e00100          add       edi,1E000h
  00491c6b  0bc2                  or        eax,edx
  00491c6d  894e5c                mov       [esi+5Ch],ecx
  00491c70  897e54                mov       [esi+54h],edi
  00491c73  8906                  mov       [esi],eax
  00491c75  66895612              mov       [esi+12h],dx

LAB_00491c79:
  00491c79  66395612              cmp       [esi+12h],dx
  00491c7d  750e                  jne       short 00491C8Dh
  00491c7f  8b06                  mov       eax,[esi]
  00491c81  84c3                  test      bl,al
  00491c83  7508                  jne       short 00491C8Dh
  00491c85  24fd                  and       al,0FDh
  00491c87  c6464300              mov       byte [esi+43h],0
  00491c8b  8906                  mov       [esi],eax

LAB_00491c8d:
  00491c8d  0fbf5632              movsx     edx,word [esi+32h]
  00491c91  52                    push      edx
  00491c92  e8853d0b00            call      00545A1Ch
  00491c97  8944246c              mov       [esp+6Ch],eax
  00491c9b  0fbf4632              movsx     eax,word [esi+32h]
  00491c9f  50                    push      eax
  00491ca0  e8713d0b00            call      00545A16h
  00491ca5  8b5650                mov       edx,[esi+50h]
  00491ca8  89442474              mov       [esp+74h],eax
  00491cac  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  00491cb3  8bc2                  mov       eax,edx
  00491cb5  8bfa                  mov       edi,edx
  00491cb7  c1f808                sar       eax,8
  00491cba  8b4c241c              mov       ecx,[esp+1Ch]
  00491cbe  2bf8                  sub       edi,eax
  00491cc0  c1fa03                sar       edx,3
  00491cc3  0faf4e5c              imul      ecx,[esi+5Ch]
  00491cc7  2bfa                  sub       edi,edx
  00491cc9  8b5654                mov       edx,[esi+54h]
  00491ccc  8bc5                  mov       eax,ebp
  00491cce  8bda                  mov       ebx,edx
  00491cd0  c1f808                sar       eax,8
  00491cd3  c1fb08                sar       ebx,8
  00491cd6  2bc3                  sub       eax,ebx
  00491cd8  897e50                mov       [esi+50h],edi
  00491cdb  2bc5                  sub       eax,ebp
  00491cdd  56                    push      esi
  00491cde  03c2                  add       eax,edx
  00491ce0  8b5658                mov       edx,[esi+58h]
  00491ce3  8bda                  mov       ebx,edx
  00491ce5  8bea                  mov       ebp,edx
  00491ce7  c1fb03                sar       ebx,3
  00491cea  2beb                  sub       ebp,ebx
  00491cec  8b5e1c                mov       ebx,[esi+1Ch]
  00491cef  c1fa08                sar       edx,8
  00491cf2  c1f90c                sar       ecx,0Ch
  00491cf5  2bea                  sub       ebp,edx
  00491cf7  8bd1                  mov       edx,ecx
  00491cf9  0faf542470            imul      edx,[esp+70h]
  00491cfe  0faf4c2478            imul      ecx,[esp+78h]
  00491d03  c1fa0c                sar       edx,0Ch
  00491d06  c1f90c                sar       ecx,0Ch
  00491d09  896e58                mov       [esi+58h],ebp
  00491d0c  03d7                  add       edx,edi
  00491d0e  8b7e20                mov       edi,[esi+20h]
  00491d11  03cd                  add       ecx,ebp
  00491d13  8b6e24                mov       ebp,[esi+24h]
  00491d16  03da                  add       ebx,edx
  00491d18  03f8                  add       edi,eax
  00491d1a  03e9                  add       ebp,ecx
  00491d1c  894654                mov       [esi+54h],eax
  00491d1f  895e1c                mov       [esi+1Ch],ebx
  00491d22  897e20                mov       [esi+20h],edi
  00491d25  896e24                mov       [esi+24h],ebp
  00491d28  e8131c0a00            call      00533940h
  00491d2d  8b9690000000          mov       edx,[esi+90h]
  00491d33  8b4660                mov       eax,[esi+60h]
  00491d36  8b4e20                mov       ecx,[esi+20h]
  00491d39  83c404                add       esp,4
  00491d3c  80e6fc                and       dh,0FCh
  00491d3f  3bc8                  cmp       ecx,eax
  00491d41  899690000000          mov       [esi+90h],edx
  00491d47  7f2a                  jg        short 00491D73h
  00491d49  f6c680                test      dh,80h
  00491d4c  7525                  jne       short 00491D73h
  00491d4e  8b0e                  mov       ecx,[esi]
  00491d50  894620                mov       [esi+20h],eax
  00491d53  f6c101                test      cl,1
  00491d56  740f                  je        short 00491D67h
  00491d58  83e1fe                and       ecx,0FFFFFFFEh
  00491d5b  33db                  xor       ebx,ebx
  00491d5d  890e                  mov       [esi],ecx
  00491d5f  895e54                mov       [esi+54h],ebx
  00491d62  e923010000            jmp       00491E8Ah

LAB_00491d67:
  00491d67  83e1fe                and       ecx,0FFFFFFFEh
  00491d6a  33db                  xor       ebx,ebx
  00491d6c  890e                  mov       [esi],ecx
  00491d6e  895e54                mov       [esi+54h],ebx
  00491d71  eb02                  jmp       short 00491D75h

LAB_00491d73:
  00491d73  33db                  xor       ebx,ebx

LAB_00491d75:
  00491d75  8b0e                  mov       ecx,[esi]
  00491d77  f6c101                test      cl,1
  00491d7a  750a                  jne       short 00491D86h
  00491d7c  394620                cmp       [esi+20h],eax
  00491d7f  7e05                  jle       short 00491D86h
  00491d81  83c901                or        ecx,1
  00491d84  890e                  mov       [esi],ecx

LAB_00491d86:
  00491d86  f7c282ad0020          test      edx,2000AD82h
  00491d8c  0f84f8000000          je        near 00491E8Ah
  00491d92  f60601                test      byte [esi],1
  00491d95  0f85c7000000          jne       near 00491E62h
  00491d9b  807e4207              cmp       byte [esi+42h],7
  00491d9f  7421                  je        short 00491DC2h
  00491da1  c6464207              mov       byte [esi+42h],7
  00491da5  e896530b00            call      00547140h
  00491daa  99                    cdq
  00491dab  b928000000            mov       ecx,28h
  00491db0  66895e12              mov       [esi+12h],bx
  00491db4  f7f9                  idiv      ecx
  00491db6  66c746105000          mov       word [esi+10h],50h
  00491dbc  80c214                add       dl,14h
  00491dbf  885643                mov       [esi+43h],dl

LAB_00491dc2:
  00491dc2  8b16                  mov       edx,[esi]
  00491dc4  8b442428              mov       eax,[esp+28h]
  00491dc8  8b4c242c              mov       ecx,[esp+2Ch]
  00491dcc  83e2fd                and       edx,0FFFFFFFDh
  00491dcf  8916                  mov       [esi],edx
  00491dd1  8b542424              mov       edx,[esp+24h]
  00491dd5  894620                mov       [esi+20h],eax
  00491dd8  8b442448              mov       eax,[esp+48h]
  00491ddc  89561c                mov       [esi+1Ch],edx
  00491ddf  8b54245c              mov       edx,[esp+5Ch]
  00491de3  25ffff0000            and       eax,0FFFFh
  00491de8  894e24                mov       [esi+24h],ecx
  00491deb  8b4c243c              mov       ecx,[esp+3Ch]
  00491def  89567c                mov       [esi+7Ch],edx
  00491df2  8b542458              mov       edx,[esp+58h]
  00491df6  898690000000          mov       [esi+90h],eax
  00491dfc  8b44244c              mov       eax,[esp+4Ch]
  00491e00  898e84000000          mov       [esi+84h],ecx
  00491e06  8b4c2444              mov       ecx,[esp+44h]
  00491e0a  898680000000          mov       [esi+80h],eax
  00491e10  8b442450              mov       eax,[esp+50h]
  00491e14  899688000000          mov       [esi+88h],edx
  00491e1a  8b542440              mov       edx,[esp+40h]
  00491e1e  6882ad0020            push      2000AD82h
  00491e23  56                    push      esi
  00491e24  89868c000000          mov       [esi+8Ch],eax
  00491e2a  894e64                mov       [esi+64h],ecx
  00491e2d  895660                mov       [esi+60h],edx
  00491e30  e82b400400            call      004D5E60h
  00491e35  83c408                add       esp,8
  00491e38  663bc3                cmp       ax,bx
  00491e3b  7515                  jne       short 00491E52h
  00491e3d  e8fe520b00            call      00547140h
  00491e42  99                    cdq
  00491e43  33c2                  xor       eax,edx
  00491e45  2bc2                  sub       eax,edx
  00491e47  83e001                and       eax,1
  00491e4a  33c2                  xor       eax,edx
  00491e4c  2bc2                  sub       eax,edx
  00491e4e  8d4400ff              lea       eax,[eax+eax-1]

LAB_00491e52:
  00491e52  8b4c2410              mov       ecx,[esp+10h]
  00491e56  83c140                add       ecx,40h
  00491e59  0fafc8                imul      ecx,eax
  00491e5c  66014e32              add       [esi+32h],cx
  00491e60  eb28                  jmp       short 00491E8Ah

LAB_00491e62:
  00491e62  8b542424              mov       edx,[esp+24h]
  00491e66  8b44242c              mov       eax,[esp+2Ch]
  00491e6a  895e5c                mov       [esi+5Ch],ebx
  00491e6d  895e50                mov       [esi+50h],ebx
  00491e70  895e58                mov       [esi+58h],ebx
  00491e73  89561c                mov       [esi+1Ch],edx
  00491e76  894624                mov       [esi+24h],eax
  00491e79  885e42                mov       [esi+42h],bl
  00491e7c  c6464328              mov       byte [esi+43h],28h
  00491e80  66895e12              mov       [esi+12h],bx
  00491e84  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_00491e8a:
  00491e8a  6aff                  push      0FFFFFFFFh
  00491e8c  6aff                  push      0FFFFFFFFh
  00491e8e  6aff                  push      0FFFFFFFFh
  00491e90  56                    push      esi
  00491e91  e87a190300            call      004C3810h
  00491e96  8b44242c              mov       eax,[esp+2Ch]
  00491e9a  83c410                add       esp,10h
  00491e9d  3bc3                  cmp       eax,ebx
  00491e9f  7517                  jne       short 00491EB8h
  00491ea1  668b8c2482000000      mov       cx,[esp+82h]
  00491ea9  66894e32              mov       [esi+32h],cx
  00491ead  5f                    pop       edi
  00491eae  5e                    pop       esi
  00491eaf  5d                    pop       ebp
  00491eb0  5b                    pop       ebx
  00491eb1  81c484000000          add       esp,84h
  00491eb7  c3                    ret

LAB_00491eb8:
  00491eb8  385e43                cmp       [esi+43h],bl
  00491ebb  0f8563020000          jne       near 00492124h
  00491ec1  8b466c                mov       eax,[esi+6Ch]
  00491ec4  8b561c                mov       edx,[esi+1Ch]
  00491ec7  8b781c                mov       edi,[eax+1Ch]
  00491eca  8b6824                mov       ebp,[eax+24h]
  00491ecd  8b4624                mov       eax,[esi+24h]
  00491ed0  2bfa                  sub       edi,edx
  00491ed2  2be8                  sub       ebp,eax
  00491ed4  c1ff0c                sar       edi,0Ch
  00491ed7  c1fd0c                sar       ebp,0Ch
  00491eda  8bd7                  mov       edx,edi
  00491edc  8bc5                  mov       eax,ebp
  00491ede  0fafd7                imul      edx,edi
  00491ee1  0fafc5                imul      eax,ebp
  00491ee4  03d0                  add       edx,eax
  00491ee6  52                    push      edx
  00491ee7  e8243b0b00            call      00545A10h
  00491eec  55                    push      ebp
  00491eed  57                    push      edi
  00491eee  8944241c              mov       [esp+1Ch],eax
  00491ef2  e82b3b0b00            call      00545A22h
  00491ef7  668b4e32              mov       cx,[esi+32h]
  00491efb  66895e12              mov       [esi+12h],bx
  00491eff  2bc1                  sub       eax,ecx
  00491f01  25ff0f0000            and       eax,0FFFh
  00491f06  89442418              mov       [esp+18h],eax
  00491f0a  8b442414              mov       eax,[esp+14h]
  00491f0e  3dfa000000            cmp       eax,0FAh
  00491f13  0f8da3000000          jge       near 00491FBCh
  00491f19  e822520b00            call      00547140h
  00491f1e  99                    cdq
  00491f1f  b964000000            mov       ecx,64h
  00491f24  f7f9                  idiv      ecx
  00491f26  83fa32                cmp       edx,32h
  00491f29  7d4e                  jge       short 00491F79h
  00491f2b  8b542418              mov       edx,[esp+18h]
  00491f2f  8b7c2420              mov       edi,[esp+20h]
  00491f33  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00491f3a  8d0cfd00020000        lea       ecx,[edi*8+200h]
  00491f41  25ff0f0000            and       eax,0FFFh
  00491f46  3bc1                  cmp       eax,ecx
  00491f48  7f25                  jg        short 00491F6Fh
  00491f4a  e8f1510b00            call      00547140h
  00491f4f  99                    cdq
  00491f50  8d4c3f01              lea       ecx,[edi+edi+1]
  00491f54  f7f9                  idiv      ecx
  00491f56  8b442414              mov       eax,[esp+14h]
  00491f5a  83c264                add       edx,64h
  00491f5d  3bc2                  cmp       eax,edx
  00491f5f  7d0e                  jge       short 00491F6Fh
  00491f61  c646420b              mov       byte [esi+42h],0Bh
  00491f65  c6464301              mov       byte [esi+43h],1
  00491f69  66895e12              mov       [esi+12h],bx
  00491f6d  eb51                  jmp       short 00491FC0h

LAB_00491f6f:
  00491f6f  c6464201              mov       byte [esi+42h],1
  00491f73  c6464301              mov       byte [esi+43h],1
  00491f77  eb47                  jmp       short 00491FC0h

LAB_00491f79:
  00491f79  83fa50                cmp       edx,50h
  00491f7c  7d29                  jge       short 00491FA7h
  00491f7e  c6464202              mov       byte [esi+42h],2
  00491f82  e8b9510b00            call      00547140h
  00491f87  99                    cdq
  00491f88  b90a000000            mov       ecx,0Ah
  00491f8d  8b7c2420              mov       edi,[esp+20h]
  00491f91  f7f9                  idiv      ecx
  00491f93  8bc7                  mov       eax,edi
  00491f95  66c746105000          mov       word [esi+10h],50h
  00491f9b  d1f8                  sar       eax,1
  00491f9d  02d0                  add       dl,al
  00491f9f  80c214                add       dl,14h
  00491fa2  885643                mov       [esi+43h],dl
  00491fa5  eb19                  jmp       short 00491FC0h

LAB_00491fa7:
  00491fa7  c6464206              mov       byte [esi+42h],6
  00491fab  c6464301              mov       byte [esi+43h],1
  00491faf  e88c510b00            call      00547140h
  00491fb4  66894610              mov       [esi+10h],ax
  00491fb8  66895e12              mov       [esi+12h],bx

LAB_00491fbc:
  00491fbc  8b7c2420              mov       edi,[esp+20h]

LAB_00491fc0:
  00491fc0  8b442414              mov       eax,[esp+14h]
  00491fc4  3d58020000            cmp       eax,258h
  00491fc9  0f8dab000000          jge       near 0049207Ah
  00491fcf  3dfa000000            cmp       eax,0FAh
  00491fd4  0f8ca0000000          jl        near 0049207Ah
  00491fda  e861510b00            call      00547140h
  00491fdf  99                    cdq
  00491fe0  b964000000            mov       ecx,64h
  00491fe5  f7f9                  idiv      ecx
  00491fe7  83fa14                cmp       edx,14h
  00491fea  7d22                  jge       short 0049200Eh
  00491fec  8b542418              mov       edx,[esp+18h]
  00491ff0  81c200040000          add       edx,400h
  00491ff6  81e2ff0f0000          and       edx,0FFFh
  00491ffc  81fa00080000          cmp       edx,800h
  00492002  7c76                  jl        short 0049207Ah
  00492004  c6464201              mov       byte [esi+42h],1
  00492008  c6464301              mov       byte [esi+43h],1
  0049200c  eb6c                  jmp       short 0049207Ah

LAB_0049200e:
  0049200e  83fa28                cmp       edx,28h
  00492011  7d25                  jge       short 00492038h
  00492013  c6464202              mov       byte [esi+42h],2
  00492017  e824510b00            call      00547140h
  0049201c  99                    cdq
  0049201d  b90a000000            mov       ecx,0Ah
  00492022  66c746105000          mov       word [esi+10h],50h
  00492028  f7f9                  idiv      ecx
  0049202a  8bc7                  mov       eax,edi
  0049202c  d1f8                  sar       eax,1
  0049202e  02d0                  add       dl,al
  00492030  80c214                add       dl,14h
  00492033  885643                mov       [esi+43h],dl
  00492036  eb42                  jmp       short 0049207Ah

LAB_00492038:
  00492038  83fa3c                cmp       edx,3Ch
  0049203b  7d13                  jge       short 00492050h
  0049203d  c6464206              mov       byte [esi+42h],6
  00492041  c6464301              mov       byte [esi+43h],1
  00492045  e8f6500b00            call      00547140h
  0049204a  66894610              mov       [esi+10h],ax
  0049204e  eb26                  jmp       short 00492076h

LAB_00492050:
  00492050  83fa50                cmp       edx,50h
  00492053  7d19                  jge       short 0049206Eh
  00492055  c6464207              mov       byte [esi+42h],7
  00492059  e8e2500b00            call      00547140h
  0049205e  99                    cdq
  0049205f  b914000000            mov       ecx,14h
  00492064  f7f9                  idiv      ecx
  00492066  80c21e                add       dl,1Eh
  00492069  885643                mov       [esi+43h],dl
  0049206c  eb08                  jmp       short 00492076h

LAB_0049206e:
  0049206e  c6464205              mov       byte [esi+42h],5
  00492072  c6464301              mov       byte [esi+43h],1

LAB_00492076:
  00492076  66895e12              mov       [esi+12h],bx

LAB_0049207a:
  0049207a  817c241458020000      cmp       dword [esp+14h],258h
  00492082  0f8c9c000000          jl        near 00492124h
  00492088  e8b3500b00            call      00547140h
  0049208d  99                    cdq
  0049208e  b964000000            mov       ecx,64h
  00492093  f7f9                  idiv      ecx
  00492095  83fa32                cmp       edx,32h
  00492098  7d2e                  jge       short 004920C8h
  0049209a  c6464202              mov       byte [esi+42h],2
  0049209e  e89d500b00            call      00547140h
  004920a3  99                    cdq
  004920a4  b90a000000            mov       ecx,0Ah
  004920a9  66c746106400          mov       word [esi+10h],64h
  004920af  f7f9                  idiv      ecx
  004920b1  8bc7                  mov       eax,edi
  004920b3  d1f8                  sar       eax,1
  004920b5  02d0                  add       dl,al
  004920b7  80c214                add       dl,14h
  004920ba  885643                mov       [esi+43h],dl
  004920bd  5f                    pop       edi
  004920be  5e                    pop       esi
  004920bf  5d                    pop       ebp
  004920c0  5b                    pop       ebx
  004920c1  81c484000000          add       esp,84h
  004920c7  c3                    ret

LAB_004920c8:
  004920c8  83fa3c                cmp       edx,3Ch
  004920cb  7d26                  jge       short 004920F3h
  004920cd  c6464207              mov       byte [esi+42h],7
  004920d1  e86a500b00            call      00547140h
  004920d6  99                    cdq
  004920d7  b914000000            mov       ecx,14h
  004920dc  66895e12              mov       [esi+12h],bx
  004920e0  f7f9                  idiv      ecx
  004920e2  80c21e                add       dl,1Eh
  004920e5  885643                mov       [esi+43h],dl
  004920e8  5f                    pop       edi
  004920e9  5e                    pop       esi
  004920ea  5d                    pop       ebp
  004920eb  5b                    pop       ebx
  004920ec  81c484000000          add       esp,84h
  004920f2  c3                    ret

LAB_004920f3:
  004920f3  83fa50                cmp       edx,50h
  004920f6  7d17                  jge       short 0049210Fh
  004920f8  c6464205              mov       byte [esi+42h],5
  004920fc  c6464301              mov       byte [esi+43h],1
  00492100  66895e12              mov       [esi+12h],bx
  00492104  5f                    pop       edi
  00492105  5e                    pop       esi
  00492106  5d                    pop       ebp
  00492107  5b                    pop       ebx
  00492108  81c484000000          add       esp,84h
  0049210e  c3                    ret

LAB_0049210f:
  0049210f  c6464206              mov       byte [esi+42h],6
  00492113  c6464301              mov       byte [esi+43h],1
  00492117  e824500b00            call      00547140h
  0049211c  66894610              mov       [esi+10h],ax
  00492120  66895e12              mov       [esi+12h],bx

LAB_00492124:
  00492124  5f                    pop       edi
  00492125  5e                    pop       esi
  00492126  5d                    pop       ebp
  00492127  5b                    pop       ebx
  00492128  81c484000000          add       esp,84h
  0049212e  c3                    ret
  0049212f  90                    nop
  00492130  d318                  rcr       dword [eax],cl
  00492132  49                    dec       ecx
  00492133  001e                  add       [esi],bl
  00492135  194900                sbb       [ecx],ecx
  00492138  97                    xchg      edi,eax
  00492139  194900                sbb       [ecx],ecx
  0049213c  8d1c49                lea       ebx,[ecx+ecx*2]
  0049213f  008d1c49003e          add       [ebp+3E00491Ch],cl
  00492145  1a4900                sbb       cl,[ecx]
  00492148  d7                    xlatb
  00492149  1a4900                sbb       cl,[ecx]
  0049214c  691b49008d1c          imul      ebx,[ebx],1C8D0049h
  00492152  49                    dec       ecx
  00492153  008d1c49008d          add       [ebp-72FFB6E4h],cl
  00492159  1c49                  sbb       al,49h
  0049215b  00c5                  add       ch,al
  0049215d  1b4900                sbb       ecx,[ecx]
  00492160  56                    push      esi
  00492161  8b74240c              mov       esi,[esp+0Ch]
  00492165  57                    push      edi
  00492166  8b7c240c              mov       edi,[esp+0Ch]
  0049216a  56                    push      esi
  0049216b  57                    push      edi
  0049216c  e8bf0a0100            call      004A2C30h
  00492171  83c408                add       esp,8
  00492174  6685c0                test      ax,ax
  00492177  7515                  jne       short 0049218Eh
  00492179  8b442414              mov       eax,[esp+14h]
  0049217d  50                    push      eax
  0049217e  56                    push      esi
  0049217f  57                    push      edi
  00492180  e89ba20a00            call      0053C420h
  00492185  83c40c                add       esp,0Ch
  00492188  6633c0                xor       ax,ax
  0049218b  5f                    pop       edi
  0049218c  5e                    pop       esi
  0049218d  c3                    ret

LAB_0049218e:
  0049218e  5f                    pop       edi
  0049218f  66b80100              mov       ax,1
  00492193  5e                    pop       esi
  00492194  c3                    ret
  00492195  90                    nop
  00492196  90                    nop
  00492197  90                    nop
  00492198  90                    nop
  00492199  90                    nop
  0049219a  90                    nop
  0049219b  90                    nop
  0049219c  90                    nop
  0049219d  90                    nop
  0049219e  90                    nop
  0049219f  90                    nop

62 cible(s) de saut interne : 0x004916a3, 0x004917f4, 0x00491816, 0x0049181b, 0x00491828, 0x0049182a, 0x004918af, 0x004918b3, 0x004918be, 0x004918f3, 0x00491906, 0x0049193b, 0x0049195f, 0x00491980, 0x004919c8, 0x004919e8, 0x004919f4, 0x00491a2c, 0x00491a89, 0x00491a8d, 0x00491aa7, 0x00491ab1, 0x00491ac3, 0x00491aea, 0x00491b58, 0x00491b82, 0x00491b8e, 0x00491bbd, 0x00491bec, 0x00491bf1, 0x00491c08, 0x00491c35, 0x00491c3f, 0x00491c43, 0x00491c56, 0x00491c79, 0x00491c8d, 0x00491d67, 0x00491d73, 0x00491d75, 0x00491d86, 0x00491dc2, 0x00491e52, 0x00491e62, 0x00491e8a, 0x00491eb8, 0x00491f6f, 0x00491f79, 0x00491fa7, 0x00491fbc, 0x00491fc0, 0x0049200e, 0x00492038, 0x00492050, 0x0049206e, 0x00492076, 0x0049207a, 0x004920c8, 0x004920f3, 0x0049210f, 0x00492124, 0x0049218e
