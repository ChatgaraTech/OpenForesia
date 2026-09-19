Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004dfb40  section .text  offset fichier 0xdef40  3088 octets

  004dfb40  83ec7c                sub       esp,7Ch
  004dfb43  53                    push      ebx
  004dfb44  55                    push      ebp
  004dfb45  56                    push      esi
  004dfb46  8bb4248c000000        mov       esi,[esp+8Ch]
  004dfb4d  b864000000            mov       eax,64h
  004dfb52  33db                  xor       ebx,ebx
  004dfb54  668944245a            mov       [esp+5Ah],ax
  004dfb59  668944245e            mov       [esp+5Eh],ax
  004dfb5e  8a4641                mov       al,[esi+41h]
  004dfb61  57                    push      edi
  004dfb62  3ac3                  cmp       al,bl
  004dfb64  66895c2458            mov       [esp+58h],bx
  004dfb69  66c744245a1e00        mov       word [esp+5Ah],1Eh
  004dfb70  66c744245c3200        mov       word [esp+5Ch],32h
  004dfb77  66895c2460            mov       [esp+60h],bx
  004dfb7c  7510                  jne       short 004DFB8Eh
  004dfb7e  53                    push      ebx
  004dfb7f  6a03                  push      3
  004dfb81  56                    push      esi
  004dfb82  c6464101              mov       byte [esi+41h],1
  004dfb86  e8c50b0000            call      004E0750h
  004dfb8b  83c40c                add       esp,0Ch

LAB_004dfb8e:
  004dfb8e  8b461c                mov       eax,[esi+1Ch]
  004dfb91  8b4e20                mov       ecx,[esi+20h]
  004dfb94  8b5624                mov       edx,[esi+24h]
  004dfb97  8d6e1c                lea       ebp,[esi+1Ch]
  004dfb9a  8944244c              mov       [esp+4Ch],eax
  004dfb9e  668b4632              mov       ax,[esi+32h]
  004dfba2  894c2450              mov       [esp+50h],ecx
  004dfba6  8b4e7c                mov       ecx,[esi+7Ch]
  004dfba9  89542454              mov       [esp+54h],edx
  004dfbad  668b9690000000        mov       dx,[esi+90h]
  004dfbb4  668944247a            mov       [esp+7Ah],ax
  004dfbb9  8b8680000000          mov       eax,[esi+80h]
  004dfbbf  894c2428              mov       [esp+28h],ecx
  004dfbc3  8b8e84000000          mov       ecx,[esi+84h]
  004dfbc9  668954242c            mov       [esp+2Ch],dx
  004dfbce  8b9688000000          mov       edx,[esi+88h]
  004dfbd4  8b7e6c                mov       edi,[esi+6Ch]
  004dfbd7  89442430              mov       [esp+30h],eax
  004dfbdb  8b868c000000          mov       eax,[esi+8Ch]
  004dfbe1  894c2434              mov       [esp+34h],ecx
  004dfbe5  89542438              mov       [esp+38h],edx
  004dfbe9  8d4c2440              lea       ecx,[esp+40h]
  004dfbed  8944243c              mov       [esp+3Ch],eax
  004dfbf1  8d542444              lea       edx,[esp+44h]
  004dfbf5  51                    push      ecx
  004dfbf6  8d44244c              lea       eax,[esp+4Ch]
  004dfbfa  52                    push      edx
  004dfbfb  50                    push      eax
  004dfbfc  56                    push      esi
  004dfbfd  e87e8d0500            call      00538980h
  004dfc02  83c410                add       esp,10h
  004dfc05  8d4c2424              lea       ecx,[esp+24h]
  004dfc09  8d54241c              lea       edx,[esp+1Ch]
  004dfc0d  8d442418              lea       eax,[esp+18h]
  004dfc11  51                    push      ecx
  004dfc12  52                    push      edx
  004dfc13  8d4c241c              lea       ecx,[esp+1Ch]
  004dfc17  50                    push      eax
  004dfc18  8d54241c              lea       edx,[esp+1Ch]
  004dfc1c  51                    push      ecx
  004dfc1d  52                    push      edx
  004dfc1e  56                    push      esi
  004dfc1f  e8fc620500            call      00535F20h
  004dfc24  8b4c2428              mov       ecx,[esp+28h]
  004dfc28  b840000000            mov       eax,40h
  004dfc2d  2bc1                  sub       eax,ecx
  004dfc2f  83c418                add       esp,18h
  004dfc32  89442420              mov       [esp+20h],eax
  004dfc36  8b442424              mov       eax,[esp+24h]
  004dfc3a  50                    push      eax
  004dfc3b  56                    push      esi
  004dfc3c  e89f61ffff            call      004D5DE0h
  004dfc41  8b4500                mov       eax,[ebp]
  004dfc44  8b571c                mov       edx,[edi+1Ch]
  004dfc47  8b4e20                mov       ecx,[esi+20h]
  004dfc4a  2bc2                  sub       eax,edx
  004dfc4c  8b5720                mov       edx,[edi+20h]
  004dfc4f  83c408                add       esp,8
  004dfc52  2bca                  sub       ecx,edx
  004dfc54  8b5624                mov       edx,[esi+24h]
  004dfc57  2b5724                sub       edx,[edi+24h]
  004dfc5a  c1f80c                sar       eax,0Ch
  004dfc5d  8bf8                  mov       edi,eax
  004dfc5f  c1f90c                sar       ecx,0Ch
  004dfc62  0faff8                imul      edi,eax
  004dfc65  8bc1                  mov       eax,ecx
  004dfc67  c1fa0c                sar       edx,0Ch
  004dfc6a  0fafc1                imul      eax,ecx
  004dfc6d  8bca                  mov       ecx,edx
  004dfc6f  03f8                  add       edi,eax
  004dfc71  0fafca                imul      ecx,edx
  004dfc74  03f9                  add       edi,ecx
  004dfc76  57                    push      edi
  004dfc77  e8945d0600            call      00545A10h
  004dfc7c  8b4c241c              mov       ecx,[esp+1Ch]
  004dfc80  8bf8                  mov       edi,eax
  004dfc82  3bcb                  cmp       ecx,ebx
  004dfc84  897c2414              mov       [esp+14h],edi
  004dfc88  0f84dd040000          je        near 004E016Bh
  004dfc8e  8b06                  mov       eax,[esi]
  004dfc90  0fbf5e18              movsx     ebx,word [esi+18h]
  004dfc94  25fffffffd            and       eax,0FDFFFFFFh
  004dfc99  8906                  mov       [esi],eax
  004dfc9b  8bd0                  mov       edx,eax
  004dfc9d  33c0                  xor       eax,eax
  004dfc9f  8a4604                mov       al,[esi+4]
  004dfca2  f7c200000001          test      edx,1000000h
  004dfca8  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004dfcaf  8b0498                mov       eax,[eax+ebx*4]
  004dfcb2  741d                  je        short 004DFCD1h
  004dfcb4  66ff461a              inc       word [esi+1Ah]
  004dfcb8  668b561a              mov       dx,[esi+1Ah]
  004dfcbc  33db                  xor       ebx,ebx
  004dfcbe  668b5816              mov       bx,[eax+16h]
  004dfcc2  0fbfd2                movsx     edx,dx
  004dfcc5  3bd3                  cmp       edx,ebx
  004dfcc7  7c2b                  jl        short 004DFCF4h
  004dfcc9  66c7461a0000          mov       word [esi+1Ah],0
  004dfccf  eb23                  jmp       short 004DFCF4h

LAB_004dfcd1:
  004dfcd1  66ff461a              inc       word [esi+1Ah]
  004dfcd5  33db                  xor       ebx,ebx
  004dfcd7  668b5816              mov       bx,[eax+16h]
  004dfcdb  0fbf461a              movsx     eax,word [esi+1Ah]
  004dfcdf  3bc3                  cmp       eax,ebx
  004dfce1  7c11                  jl        short 004DFCF4h
  004dfce3  668b461a              mov       ax,[esi+1Ah]
  004dfce7  48                    dec       eax
  004dfce8  81ca00000002          or        edx,2000000h
  004dfcee  6689461a              mov       [esi+1Ah],ax
  004dfcf2  8916                  mov       [esi],edx

LAB_004dfcf4:
  004dfcf4  8a4642                mov       al,[esi+42h]
  004dfcf7  33d2                  xor       edx,edx
  004dfcf9  3c05                  cmp       al,5
  004dfcfb  8b06                  mov       eax,[esi]
  004dfcfd  7504                  jne       short 004DFD03h
  004dfcff  0c10                  or        al,10h
  004dfd01  eb02                  jmp       short 004DFD05h

LAB_004dfd03:
  004dfd03  24ef                  and       al,0EFh

LAB_004dfd05:
  004dfd05  8906                  mov       [esi],eax
  004dfd07  8a06                  mov       al,[esi]
  004dfd09  a802                  test      al,2
  004dfd0b  bb0a000000            mov       ebx,0Ah
  004dfd10  7464                  je        short 004DFD76h
  004dfd12  66395e1a              cmp       [esi+1Ah],bx
  004dfd16  7c5e                  jl        short 004DFD76h
  004dfd18  8d4c2458              lea       ecx,[esp+58h]
  004dfd1c  6a00                  push      0
  004dfd1e  51                    push      ecx
  004dfd1f  55                    push      ebp
  004dfd20  56                    push      esi
  004dfd21  e80a9f0300            call      00519C30h
  004dfd26  8bf8                  mov       edi,eax
  004dfd28  a1b8e05d00            mov       eax,[5DE0B8h]
  004dfd2d  83c410                add       esp,10h
  004dfd30  85c0                  test      eax,eax
  004dfd32  7438                  je        short 004DFD6Ch
  004dfd34  66395e1a              cmp       [esi+1Ah],bx
  004dfd38  7532                  jne       short 004DFD6Ch
  004dfd3a  8b0f                  mov       ecx,[edi]
  004dfd3c  57                    push      edi
  004dfd3d  83c940                or        ecx,40h
  004dfd40  56                    push      esi
  004dfd41  66c787960000001000    mov       word [edi+96h],10h
  004dfd4a  890f                  mov       [edi],ecx
  004dfd4c  e82f9e0500            call      00539B80h
  004dfd51  83c408                add       esp,8
  004dfd54  83c71c                add       edi,1Ch
  004dfd57  57                    push      edi
  004dfd58  6800180000            push      1800h
  004dfd5d  6800030000            push      300h
  004dfd62  6a03                  push      3
  004dfd64  e8b7dd0500            call      0053DB20h
  004dfd69  83c410                add       esp,10h

LAB_004dfd6c:
  004dfd6c  8b4c241c              mov       ecx,[esp+1Ch]
  004dfd70  8b7c2414              mov       edi,[esp+14h]
  004dfd74  33d2                  xor       edx,edx

LAB_004dfd76:
  004dfd76  33c0                  xor       eax,eax
  004dfd78  8a4642                mov       al,[esi+42h]
  004dfd7b  83f80b                cmp       eax,0Bh
  004dfd7e  0f87e7030000          ja        near 004E016Bh
  004dfd84  ff248520074e00        jmp       dword [eax*4+4E0720h]
  004dfd8b  66395612              cmp       [esi+12h],dx
  004dfd8f  751a                  jne       short 004DFDABh
  004dfd91  8b06                  mov       eax,[esi]
  004dfd93  66895618              mov       [esi+18h],dx
  004dfd97  0d00000001            or        eax,1000000h
  004dfd9c  6689561a              mov       [esi+1Ah],dx
  004dfda0  8906                  mov       [esi],eax
  004dfda2  66c746120100          mov       word [esi+12h],1
  004dfda8  89565c                mov       [esi+5Ch],edx

LAB_004dfdab:
  004dfdab  0fbf4610              movsx     eax,word [esi+10h]
  004dfdaf  3bf8                  cmp       edi,eax
  004dfdb1  0f8cb0030000          jl        near 004E0167h
  004dfdb7  8a4643                mov       al,[esi+43h]
  004dfdba  84c0                  test      al,al
  004dfdbc  7405                  je        short 004DFDC3h
  004dfdbe  fec8                  dec       al
  004dfdc0  884643                mov       [esi+43h],al

LAB_004dfdc3:
  004dfdc3  89565c                mov       [esi+5Ch],edx
  004dfdc6  e9a0030000            jmp       004E016Bh
  004dfdcb  66395612              cmp       [esi+12h],dx
  004dfdcf  751b                  jne       short 004DFDECh
  004dfdd1  8b0e                  mov       ecx,[esi]
  004dfdd3  b801000000            mov       eax,1
  004dfdd8  81c900000001          or        ecx,1000000h
  004dfdde  66894618              mov       [esi+18h],ax
  004dfde2  6689561a              mov       [esi+1Ah],dx
  004dfde6  890e                  mov       [esi],ecx
  004dfde8  66894612              mov       [esi+12h],ax

LAB_004dfdec:
  004dfdec  8b442418              mov       eax,[esp+18h]
  004dfdf0  8d88c8000000          lea       ecx,[eax+0C8h]
  004dfdf6  81e1ff0f0000          and       ecx,0FFFh
  004dfdfc  81f990010000          cmp       ecx,190h
  004dfe02  7f10                  jg        short 004DFE14h
  004dfe04  c6464300              mov       byte [esi+43h],0
  004dfe08  c7465c00500000        mov       dword [esi+5Ch],5000h
  004dfe0f  e957030000            jmp       004E016Bh

LAB_004dfe14:
  004dfe14  25ff0f0000            and       eax,0FFFh
  004dfe19  3d00080000            cmp       eax,800h
  004dfe1e  7d17                  jge       short 004DFE37h
  004dfe20  8b542410              mov       edx,[esp+10h]
  004dfe24  c7465c00500000        mov       dword [esi+5Ch],5000h
  004dfe2b  8d4232                lea       eax,[edx+32h]
  004dfe2e  66014632              add       [esi+32h],ax
  004dfe32  e934030000            jmp       004E016Bh

LAB_004dfe37:
  004dfe37  8b4c2410              mov       ecx,[esp+10h]
  004dfe3b  b8ceffffff            mov       eax,0FFFFFFCEh
  004dfe40  2bc1                  sub       eax,ecx
  004dfe42  c7465c00500000        mov       dword [esi+5Ch],5000h
  004dfe49  66014632              add       [esi+32h],ax
  004dfe4d  e919030000            jmp       004E016Bh
  004dfe52  668b5612              mov       dx,[esi+12h]
  004dfe56  8b4c2410              mov       ecx,[esp+10h]
  004dfe5a  bd01000000            mov       ebp,1
  004dfe5f  663bd5                cmp       dx,bp
  004dfe62  7552                  jne       short 004DFEB6h
  004dfe64  0fbf4610              movsx     eax,word [esi+10h]
  004dfe68  3bf8                  cmp       edi,eax
  004dfe6a  7d06                  jge       short 004DFE72h
  004dfe6c  c6464300              mov       byte [esi+43h],0
  004dfe70  eb44                  jmp       short 004DFEB6h

LAB_004dfe72:
  004dfe72  8a4643                mov       al,[esi+43h]
  004dfe75  84c0                  test      al,al
  004dfe77  7405                  je        short 004DFE7Eh
  004dfe79  fec8                  dec       al
  004dfe7b  884643                mov       [esi+43h],al

LAB_004dfe7e:
  004dfe7e  8b442418              mov       eax,[esp+18h]
  004dfe82  8db800010000          lea       edi,[eax+100h]
  004dfe88  81e7ff0f0000          and       edi,0FFFh
  004dfe8e  81ff00020000          cmp       edi,200h
  004dfe94  7e20                  jle       short 004DFEB6h
  004dfe96  25ff0f0000            and       eax,0FFFh
  004dfe9b  3d00080000            cmp       eax,800h
  004dfea0  7d06                  jge       short 004DFEA8h
  004dfea2  8d440932              lea       eax,[ecx+ecx+32h]
  004dfea6  eb0a                  jmp       short 004DFEB2h

LAB_004dfea8:
  004dfea8  8d3c09                lea       edi,[ecx+ecx]
  004dfeab  b8ceffffff            mov       eax,0FFFFFFCEh
  004dfeb0  2bc7                  sub       eax,edi

LAB_004dfeb2:
  004dfeb2  66014632              add       [esi+32h],ax

LAB_004dfeb6:
  004dfeb6  6685d2                test      dx,dx
  004dfeb9  0f85ac020000          jne       near 004E016Bh
  004dfebf  8b3e                  mov       edi,[esi]
  004dfec1  8d0cc9                lea       ecx,[ecx+ecx*8]
  004dfec4  c1e104                shl       ecx,4
  004dfec7  81cf00000001          or        edi,1000000h
  004dfecd  81c100c00000          add       ecx,0C000h
  004dfed3  66896e18              mov       [esi+18h],bp
  004dfed7  6689561a              mov       [esi+1Ah],dx
  004dfedb  893e                  mov       [esi],edi
  004dfedd  66896e12              mov       [esi+12h],bp
  004dfee1  894e5c                mov       [esi+5Ch],ecx
  004dfee4  e982020000            jmp       004E016Bh
  004dfee9  bb01000000            mov       ebx,1
  004dfeee  66395e12              cmp       [esi+12h],bx
  004dfef2  0f859a000000          jne       near 004DFF92h
  004dfef8  8a4643                mov       al,[esi+43h]
  004dfefb  84c0                  test      al,al
  004dfefd  7405                  je        short 004DFF04h
  004dfeff  fec8                  dec       al
  004dff01  884643                mov       [esi+43h],al

LAB_004dff04:
  004dff04  8b442410              mov       eax,[esp+10h]
  004dff08  8d14c0                lea       edx,[eax+eax*8]
  004dff0b  c1e204                shl       edx,4
  004dff0e  8d8200600000          lea       eax,[edx+6000h]
  004dff14  0fafc8                imul      ecx,eax
  004dff17  89465c                mov       [esi+5Ch],eax
  004dff1a  0fbf4610              movsx     eax,word [esi+10h]
  004dff1e  c1f90c                sar       ecx,0Ch
  004dff21  50                    push      eax
  004dff22  8bf9                  mov       edi,ecx
  004dff24  e8f35a0600            call      00545A1Ch
  004dff29  0fafc7                imul      eax,edi
  004dff2c  8b4d00                mov       ecx,[ebp]
  004dff2f  c1f80c                sar       eax,0Ch
  004dff32  03c8                  add       ecx,eax
  004dff34  894d00                mov       [ebp],ecx
  004dff37  0fbf4e10              movsx     ecx,word [esi+10h]
  004dff3b  51                    push      ecx
  004dff3c  e8d55a0600            call      00545A16h
  004dff41  0fafc7                imul      eax,edi
  004dff44  8b6e24                mov       ebp,[esi+24h]
  004dff47  c7465c00000000        mov       dword [esi+5Ch],0
  004dff4e  c1f80c                sar       eax,0Ch
  004dff51  03e8                  add       ebp,eax
  004dff53  8b442418              mov       eax,[esp+18h]
  004dff57  896e24                mov       [esi+24h],ebp
  004dff5a  8d9000010000          lea       edx,[eax+100h]
  004dff60  81e2ff0f0000          and       edx,0FFFh
  004dff66  81fa00020000          cmp       edx,200h
  004dff6c  7e24                  jle       short 004DFF92h
  004dff6e  25ff0f0000            and       eax,0FFFh
  004dff73  3d00080000            cmp       eax,800h
  004dff78  7d09                  jge       short 004DFF83h
  004dff7a  8b442410              mov       eax,[esp+10h]
  004dff7e  83c032                add       eax,32h
  004dff81  eb0b                  jmp       short 004DFF8Eh

LAB_004dff83:
  004dff83  8b4c2410              mov       ecx,[esp+10h]
  004dff87  b8ceffffff            mov       eax,0FFFFFFCEh
  004dff8c  2bc1                  sub       eax,ecx

LAB_004dff8e:
  004dff8e  66014632              add       [esi+32h],ax

LAB_004dff92:
  004dff92  66837e1200            cmp       word [esi+12h],0
  004dff97  0f85ce010000          jne       near 004E016Bh
  004dff9d  8b06                  mov       eax,[esi]
  004dff9f  66895e18              mov       [esi+18h],bx
  004dffa3  0d00000001            or        eax,1000000h
  004dffa8  66c7461a0000          mov       word [esi+1Ah],0
  004dffae  8906                  mov       [esi],eax
  004dffb0  66895e12              mov       [esi+12h],bx
  004dffb4  e9b2010000            jmp       004E016Bh
  004dffb9  66395612              cmp       [esi+12h],dx
  004dffbd  7523                  jne       short 004DFFE2h
  004dffbf  8b06                  mov       eax,[esi]
  004dffc1  a801                  test      al,1
  004dffc3  7406                  je        short 004DFFCBh
  004dffc5  c6464300              mov       byte [esi+43h],0
  004dffc9  eb17                  jmp       short 004DFFE2h

LAB_004dffcb:
  004dffcb  25fffffffe            and       eax,0FEFFFFFFh
  004dffd0  66c746180300          mov       word [esi+18h],3
  004dffd6  8906                  mov       [esi],eax
  004dffd8  6689561a              mov       [esi+1Ah],dx
  004dffdc  66c746120100          mov       word [esi+12h],1

LAB_004dffe2:
  004dffe2  66837e1201            cmp       word [esi+12h],1
  004dffe7  7564                  jne       short 004E004Dh
  004dffe9  66395e1a              cmp       [esi+1Ah],bx
  004dffed  7c5e                  jl        short 004E004Dh
  004dffef  8b542410              mov       edx,[esp+10h]
  004dfff3  8b5e54                mov       ebx,[esi+54h]
  004dfff6  81c300400100          add       ebx,14000h
  004dfffc  66c746120200          mov       word [esi+12h],2
  004e0002  8d04d500400100        lea       eax,[edx*8+14000h]
  004e0009  895e54                mov       [esi+54h],ebx
  004e000c  0fafc8                imul      ecx,eax
  004e000f  89465c                mov       [esi+5Ch],eax
  004e0012  0fbf4610              movsx     eax,word [esi+10h]
  004e0016  c1f90c                sar       ecx,0Ch
  004e0019  50                    push      eax
  004e001a  8bf9                  mov       edi,ecx
  004e001c  e8fb590600            call      00545A1Ch
  004e0021  0fafc7                imul      eax,edi
  004e0024  0fbf4e10              movsx     ecx,word [esi+10h]
  004e0028  8b6e50                mov       ebp,[esi+50h]
  004e002b  51                    push      ecx
  004e002c  c1f80c                sar       eax,0Ch
  004e002f  03e8                  add       ebp,eax
  004e0031  896e50                mov       [esi+50h],ebp
  004e0034  e8dd590600            call      00545A16h
  004e0039  0fafc7                imul      eax,edi
  004e003c  8b4e58                mov       ecx,[esi+58h]
  004e003f  c1f80c                sar       eax,0Ch
  004e0042  03c8                  add       ecx,eax
  004e0044  8b06                  mov       eax,[esi]
  004e0046  0c03                  or        al,3
  004e0048  894e58                mov       [esi+58h],ecx
  004e004b  8906                  mov       [esi],eax

LAB_004e004d:
  004e004d  66837e1202            cmp       word [esi+12h],2
  004e0052  0f8513010000          jne       near 004E016Bh
  004e0058  8b06                  mov       eax,[esi]
  004e005a  a801                  test      al,1
  004e005c  0f8509010000          jne       near 004E016Bh
  004e0062  24fd                  and       al,0FDh
  004e0064  c6464300              mov       byte [esi+43h],0
  004e0068  8906                  mov       [esi],eax
  004e006a  e9fc000000            jmp       004E016Bh
  004e006f  668b4e12              mov       cx,[esi+12h]
  004e0073  ba01000000            mov       edx,1
  004e0078  663bca                cmp       cx,dx
  004e007b  751a                  jne       short 004E0097h
  004e007d  0fbf4610              movsx     eax,word [esi+10h]
  004e0081  3bf8                  cmp       edi,eax
  004e0083  7d06                  jge       short 004E008Bh
  004e0085  c6464300              mov       byte [esi+43h],0
  004e0089  eb0c                  jmp       short 004E0097h

LAB_004e008b:
  004e008b  8a4643                mov       al,[esi+43h]
  004e008e  84c0                  test      al,al
  004e0090  7405                  je        short 004E0097h
  004e0092  fec8                  dec       al
  004e0094  884643                mov       [esi+43h],al

LAB_004e0097:
  004e0097  6685c9                test      cx,cx
  004e009a  0f85cb000000          jne       near 004E016Bh
  004e00a0  8b442410              mov       eax,[esp+10h]
  004e00a4  8b3e                  mov       edi,[esi]
  004e00a6  66894e1a              mov       [esi+1Ah],cx
  004e00aa  81cf00000001          or        edi,1000000h
  004e00b0  8d0cc0                lea       ecx,[eax+eax*8]
  004e00b3  66895618              mov       [esi+18h],dx
  004e00b7  c1e104                shl       ecx,4
  004e00ba  81c100c00000          add       ecx,0C000h
  004e00c0  893e                  mov       [esi],edi
  004e00c2  66895612              mov       [esi+12h],dx
  004e00c6  894e5c                mov       [esi+5Ch],ecx
  004e00c9  e99d000000            jmp       004E016Bh
  004e00ce  66395612              cmp       [esi+12h],dx
  004e00d2  8b4c2410              mov       ecx,[esp+10h]
  004e00d6  7523                  jne       short 004E00FBh
  004e00d8  6689561a              mov       [esi+1Ah],dx
  004e00dc  8b16                  mov       edx,[esi]
  004e00de  81e2fffffffe          and       edx,0FEFFFFFFh
  004e00e4  83f932                cmp       ecx,32h
  004e00e7  66c746120100          mov       word [esi+12h],1
  004e00ed  66c746180300          mov       word [esi+18h],3
  004e00f3  8916                  mov       [esi],edx
  004e00f5  7e04                  jle       short 004E00FBh
  004e00f7  66895e1a              mov       [esi+1Ah],bx

LAB_004e00fb:
  004e00fb  66837e1201            cmp       word [esi+12h],1
  004e0100  7551                  jne       short 004E0153h
  004e0102  66395e1a              cmp       [esi+1Ah],bx
  004e0106  7c4b                  jl        short 004E0153h
  004e0108  8b442418              mov       eax,[esp+18h]
  004e010c  8b2e                  mov       ebp,[esi]
  004e010e  83cd02                or        ebp,2
  004e0111  66c746120200          mov       word [esi+12h],2
  004e0117  8d9000010000          lea       edx,[eax+100h]
  004e011d  c7465c00e00100        mov       dword [esi+5Ch],1E000h
  004e0124  81e2ff0f0000          and       edx,0FFFh
  004e012a  892e                  mov       [esi],ebp
  004e012c  81fa00020000          cmp       edx,200h
  004e0132  7e1f                  jle       short 004E0153h
  004e0134  25ff0f0000            and       eax,0FFFh
  004e0139  3d00080000            cmp       eax,800h
  004e013e  7d06                  jge       short 004E0146h
  004e0140  8d440932              lea       eax,[ecx+ecx+32h]
  004e0144  eb09                  jmp       short 004E014Fh

LAB_004e0146:
  004e0146  03c9                  add       ecx,ecx
  004e0148  b8ceffffff            mov       eax,0FFFFFFCEh
  004e014d  2bc1                  sub       eax,ecx

LAB_004e014f:
  004e014f  66014632              add       [esi+32h],ax

LAB_004e0153:
  004e0153  66837e1202            cmp       word [esi+12h],2
  004e0158  7511                  jne       short 004E016Bh
  004e015a  8b06                  mov       eax,[esi]
  004e015c  a900000002            test      eax,2000000h
  004e0161  7408                  je        short 004E016Bh
  004e0163  24fd                  and       al,0FDh
  004e0165  8906                  mov       [esi],eax

LAB_004e0167:
  004e0167  c6464300              mov       byte [esi+43h],0

LAB_004e016b:
  004e016b  0fbf5632              movsx     edx,word [esi+32h]
  004e016f  52                    push      edx
  004e0170  e8a7580600            call      00545A1Ch
  004e0175  89442464              mov       [esp+64h],eax
  004e0179  0fbf4632              movsx     eax,word [esi+32h]
  004e017d  50                    push      eax
  004e017e  e893580600            call      00545A16h
  004e0183  8b5650                mov       edx,[esi+50h]
  004e0186  8b4c241c              mov       ecx,[esp+1Ch]
  004e018a  0fbf9e9a000000        movsx     ebx,word [esi+9Ah]
  004e0191  0faf4e5c              imul      ecx,[esi+5Ch]
  004e0195  8944246c              mov       [esp+6Ch],eax
  004e0199  8bc2                  mov       eax,edx
  004e019b  c1f803                sar       eax,3
  004e019e  8bfa                  mov       edi,edx
  004e01a0  56                    push      esi
  004e01a1  2bf8                  sub       edi,eax
  004e01a3  8b4654                mov       eax,[esi+54h]
  004e01a6  c1fa08                sar       edx,8
  004e01a9  2bfa                  sub       edi,edx
  004e01ab  8be8                  mov       ebp,eax
  004e01ad  8bd3                  mov       edx,ebx
  004e01af  897e50                mov       [esi+50h],edi
  004e01b2  c1fd08                sar       ebp,8
  004e01b5  c1fa08                sar       edx,8
  004e01b8  2bd5                  sub       edx,ebp
  004e01ba  8b6e58                mov       ebp,[esi+58h]
  004e01bd  03d0                  add       edx,eax
  004e01bf  8bc5                  mov       eax,ebp
  004e01c1  2bd3                  sub       edx,ebx
  004e01c3  8bdd                  mov       ebx,ebp
  004e01c5  c1fb08                sar       ebx,8
  004e01c8  c1f90c                sar       ecx,0Ch
  004e01cb  2bc3                  sub       eax,ebx
  004e01cd  8bd9                  mov       ebx,ecx
  004e01cf  0faf5c2468            imul      ebx,[esp+68h]
  004e01d4  0faf4c2470            imul      ecx,[esp+70h]
  004e01d9  c1fd03                sar       ebp,3
  004e01dc  c1fb0c                sar       ebx,0Ch
  004e01df  2bc5                  sub       eax,ebp
  004e01e1  8b6e1c                mov       ebp,[esi+1Ch]
  004e01e4  03df                  add       ebx,edi
  004e01e6  895654                mov       [esi+54h],edx
  004e01e9  03eb                  add       ebp,ebx
  004e01eb  8b5e20                mov       ebx,[esi+20h]
  004e01ee  c1f90c                sar       ecx,0Ch
  004e01f1  896e1c                mov       [esi+1Ch],ebp
  004e01f4  8b6e24                mov       ebp,[esi+24h]
  004e01f7  03c8                  add       ecx,eax
  004e01f9  03da                  add       ebx,edx
  004e01fb  03e9                  add       ebp,ecx
  004e01fd  894658                mov       [esi+58h],eax
  004e0200  895e20                mov       [esi+20h],ebx
  004e0203  896e24                mov       [esi+24h],ebp
  004e0206  e835370500            call      00533940h
  004e020b  8b9690000000          mov       edx,[esi+90h]
  004e0211  8b4660                mov       eax,[esi+60h]
  004e0214  8b4e20                mov       ecx,[esi+20h]
  004e0217  83c404                add       esp,4
  004e021a  80e6fc                and       dh,0FCh
  004e021d  3bc8                  cmp       ecx,eax
  004e021f  899690000000          mov       [esi+90h],edx
  004e0225  7f2a                  jg        short 004E0251h
  004e0227  f6c680                test      dh,80h
  004e022a  7525                  jne       short 004E0251h
  004e022c  8b0e                  mov       ecx,[esi]
  004e022e  894620                mov       [esi+20h],eax
  004e0231  f6c101                test      cl,1
  004e0234  740f                  je        short 004E0245h
  004e0236  83e1fe                and       ecx,0FFFFFFFEh
  004e0239  33db                  xor       ebx,ebx
  004e023b  890e                  mov       [esi],ecx
  004e023d  895e54                mov       [esi+54h],ebx
  004e0240  e928010000            jmp       004E036Dh

LAB_004e0245:
  004e0245  83e1fe                and       ecx,0FFFFFFFEh
  004e0248  33db                  xor       ebx,ebx
  004e024a  890e                  mov       [esi],ecx
  004e024c  895e54                mov       [esi+54h],ebx
  004e024f  eb02                  jmp       short 004E0253h

LAB_004e0251:
  004e0251  33db                  xor       ebx,ebx

LAB_004e0253:
  004e0253  8b0e                  mov       ecx,[esi]
  004e0255  f6c101                test      cl,1
  004e0258  750a                  jne       short 004E0264h
  004e025a  394620                cmp       [esi+20h],eax
  004e025d  7e05                  jle       short 004E0264h
  004e025f  83c901                or        ecx,1
  004e0262  890e                  mov       [esi],ecx

LAB_004e0264:
  004e0264  f7c282ad0020          test      edx,2000AD82h
  004e026a  0f84fd000000          je        near 004E036Dh
  004e0270  8b06                  mov       eax,[esi]
  004e0272  a801                  test      al,1
  004e0274  0f85c7000000          jne       near 004E0341h
  004e027a  8a4e42                mov       cl,[esi+42h]
  004e027d  b007                  mov       al,7
  004e027f  3ac8                  cmp       cl,al
  004e0281  7506                  jne       short 004E0289h
  004e0283  807e0c0b              cmp       byte [esi+0Ch],0Bh
  004e0287  7426                  je        short 004E02AFh

LAB_004e0289:
  004e0289  884642                mov       [esi+42h],al
  004e028c  e8af6e0600            call      00547140h
  004e0291  99                    cdq
  004e0292  b928000000            mov       ecx,28h
  004e0297  66895e12              mov       [esi+12h],bx
  004e029b  f7f9                  idiv      ecx
  004e029d  8b06                  mov       eax,[esi]
  004e029f  66c746105000          mov       word [esi+10h],50h
  004e02a5  80c214                add       dl,14h
  004e02a8  24fd                  and       al,0FDh
  004e02aa  885643                mov       [esi+43h],dl
  004e02ad  8906                  mov       [esi],eax

LAB_004e02af:
  004e02af  8b16                  mov       edx,[esi]
  004e02b1  8b442450              mov       eax,[esp+50h]
  004e02b5  8b4c2454              mov       ecx,[esp+54h]
  004e02b9  83e2fd                and       edx,0FFFFFFFDh
  004e02bc  894620                mov       [esi+20h],eax
  004e02bf  8b44242c              mov       eax,[esp+2Ch]
  004e02c3  8916                  mov       [esi],edx
  004e02c5  8b54244c              mov       edx,[esp+4Ch]
  004e02c9  25ffff0000            and       eax,0FFFFh
  004e02ce  89561c                mov       [esi+1Ch],edx
  004e02d1  8b542428              mov       edx,[esp+28h]
  004e02d5  898690000000          mov       [esi+90h],eax
  004e02db  8b442430              mov       eax,[esp+30h]
  004e02df  894e24                mov       [esi+24h],ecx
  004e02e2  8b4c2434              mov       ecx,[esp+34h]
  004e02e6  89567c                mov       [esi+7Ch],edx
  004e02e9  8b542438              mov       edx,[esp+38h]
  004e02ed  898680000000          mov       [esi+80h],eax
  004e02f3  8b44243c              mov       eax,[esp+3Ch]
  004e02f7  6882ad0020            push      2000AD82h
  004e02fc  56                    push      esi
  004e02fd  898e84000000          mov       [esi+84h],ecx
  004e0303  899688000000          mov       [esi+88h],edx
  004e0309  89868c000000          mov       [esi+8Ch],eax
  004e030f  e84c5bffff            call      004D5E60h
  004e0314  83c408                add       esp,8
  004e0317  663bc3                cmp       ax,bx
  004e031a  7515                  jne       short 004E0331h
  004e031c  e81f6e0600            call      00547140h
  004e0321  99                    cdq
  004e0322  33c2                  xor       eax,edx
  004e0324  2bc2                  sub       eax,edx
  004e0326  83e001                and       eax,1
  004e0329  33c2                  xor       eax,edx
  004e032b  2bc2                  sub       eax,edx
  004e032d  8d4400ff              lea       eax,[eax+eax-1]

LAB_004e0331:
  004e0331  8b4c2410              mov       ecx,[esp+10h]
  004e0335  83c140                add       ecx,40h
  004e0338  0fafc8                imul      ecx,eax
  004e033b  66014e32              add       [esi+32h],cx
  004e033f  eb2c                  jmp       short 004E036Dh

LAB_004e0341:
  004e0341  8b54244c              mov       edx,[esp+4Ch]
  004e0345  24fd                  and       al,0FDh
  004e0347  8906                  mov       [esi],eax
  004e0349  8b442454              mov       eax,[esp+54h]
  004e034d  895e5c                mov       [esi+5Ch],ebx
  004e0350  895e50                mov       [esi+50h],ebx
  004e0353  895e58                mov       [esi+58h],ebx
  004e0356  89561c                mov       [esi+1Ch],edx
  004e0359  894624                mov       [esi+24h],eax
  004e035c  885e42                mov       [esi+42h],bl
  004e035f  c6464328              mov       byte [esi+43h],28h
  004e0363  66895e12              mov       [esi+12h],bx
  004e0367  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004e036d:
  004e036d  6aff                  push      0FFFFFFFFh
  004e036f  6aff                  push      0FFFFFFFFh
  004e0371  6aff                  push      0FFFFFFFFh
  004e0373  56                    push      esi
  004e0374  e89734feff            call      004C3810h
  004e0379  8b44242c              mov       eax,[esp+2Ch]
  004e037d  83c410                add       esp,10h
  004e0380  3bc3                  cmp       eax,ebx
  004e0382  7511                  jne       short 004E0395h
  004e0384  668b4c247a            mov       cx,[esp+7Ah]
  004e0389  66894e32              mov       [esi+32h],cx
  004e038d  5f                    pop       edi
  004e038e  5e                    pop       esi
  004e038f  5d                    pop       ebp
  004e0390  5b                    pop       ebx
  004e0391  83c47c                add       esp,7Ch
  004e0394  c3                    ret

LAB_004e0395:
  004e0395  385e43                cmp       [esi+43h],bl
  004e0398  0f857a030000          jne       near 004E0718h
  004e039e  8b466c                mov       eax,[esi+6Ch]
  004e03a1  8b561c                mov       edx,[esi+1Ch]
  004e03a4  8b781c                mov       edi,[eax+1Ch]
  004e03a7  8b6824                mov       ebp,[eax+24h]
  004e03aa  8b4624                mov       eax,[esi+24h]
  004e03ad  2bfa                  sub       edi,edx
  004e03af  2be8                  sub       ebp,eax
  004e03b1  c1ff0c                sar       edi,0Ch
  004e03b4  c1fd0c                sar       ebp,0Ch
  004e03b7  8bd7                  mov       edx,edi
  004e03b9  8bc5                  mov       eax,ebp
  004e03bb  0fafd7                imul      edx,edi
  004e03be  0fafc5                imul      eax,ebp
  004e03c1  03d0                  add       edx,eax
  004e03c3  52                    push      edx
  004e03c4  e847560600            call      00545A10h
  004e03c9  55                    push      ebp
  004e03ca  57                    push      edi
  004e03cb  8944241c              mov       [esp+1Ch],eax
  004e03cf  e84e560600            call      00545A22h
  004e03d4  668b4e32              mov       cx,[esi+32h]
  004e03d8  66895e12              mov       [esi+12h],bx
  004e03dc  2bc1                  sub       eax,ecx
  004e03de  8b0e                  mov       ecx,[esi]
  004e03e0  25ff0f0000            and       eax,0FFFh
  004e03e5  83e1fd                and       ecx,0FFFFFFFDh
  004e03e8  89442418              mov       [esp+18h],eax
  004e03ec  8b442414              mov       eax,[esp+14h]
  004e03f0  3de6000000            cmp       eax,0E6h
  004e03f5  890e                  mov       [esi],ecx
  004e03f7  0f8d51010000          jge       near 004E054Eh
  004e03fd  e83e6d0600            call      00547140h
  004e0402  99                    cdq
  004e0403  b964000000            mov       ecx,64h
  004e0408  f7f9                  idiv      ecx
  004e040a  8bea                  mov       ebp,edx
  004e040c  83fd32                cmp       ebp,32h
  004e040f  0f8d98000000          jge       near 004E04ADh
  004e0415  8b542418              mov       edx,[esp+18h]
  004e0419  8b7c2420              mov       edi,[esp+20h]
  004e041d  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004e0424  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004e042b  25ff0f0000            and       eax,0FFFh
  004e0430  3bc1                  cmp       eax,ecx
  004e0432  7f69                  jg        short 004E049Dh
  004e0434  e8076d0600            call      00547140h
  004e0439  99                    cdq
  004e043a  8d4c3f01              lea       ecx,[edi+edi+1]
  004e043e  f7f9                  idiv      ecx
  004e0440  8b442414              mov       eax,[esp+14h]
  004e0444  83c264                add       edx,64h
  004e0447  3bc2                  cmp       eax,edx
  004e0449  7d52                  jge       short 004E049Dh
  004e044b  83fd1e                cmp       ebp,1Eh
  004e044e  7d27                  jge       short 004E0477h
  004e0450  8b16                  mov       edx,[esi]
  004e0452  c646420b              mov       byte [esi+42h],0Bh
  004e0456  81e2fffffffe          and       edx,0FEFFFFFFh
  004e045c  c6464301              mov       byte [esi+43h],1
  004e0460  83ca02                or        edx,2
  004e0463  895e5c                mov       [esi+5Ch],ebx
  004e0466  8916                  mov       [esi],edx
  004e0468  66c746180300          mov       word [esi+18h],3
  004e046e  66895e1a              mov       [esi+1Ah],bx
  004e0472  e99c000000            jmp       004E0513h

LAB_004e0477:
  004e0477  885e42                mov       [esi+42h],bl
  004e047a  e8c16c0600            call      00547140h
  004e047f  99                    cdq
  004e0480  b90a000000            mov       ecx,0Ah
  004e0485  66c746105000          mov       word [esi+10h],50h
  004e048b  f7f9                  idiv      ecx
  004e048d  8bc7                  mov       eax,edi
  004e048f  66895e12              mov       [esi+12h],bx
  004e0493  c1f802                sar       eax,2
  004e0496  02d0                  add       dl,al
  004e0498  885643                mov       [esi+43h],dl
  004e049b  eb76                  jmp       short 004E0513h

LAB_004e049d:
  004e049d  807e4201              cmp       byte [esi+42h],1
  004e04a1  7470                  je        short 004E0513h
  004e04a3  c6464201              mov       byte [esi+42h],1
  004e04a7  c6464301              mov       byte [esi+43h],1
  004e04ab  eb66                  jmp       short 004E0513h

LAB_004e04ad:
  004e04ad  83fd3c                cmp       ebp,3Ch
  004e04b0  7c2b                  jl        short 004E04DDh
  004e04b2  83fd46                cmp       ebp,46h
  004e04b5  7d26                  jge       short 004E04DDh
  004e04b7  8b542410              mov       edx,[esp+10h]
  004e04bb  8b442414              mov       eax,[esp+14h]
  004e04bf  83c264                add       edx,64h
  004e04c2  3bc2                  cmp       eax,edx
  004e04c4  7e17                  jle       short 004E04DDh
  004e04c6  c6464206              mov       byte [esi+42h],6
  004e04ca  c6464301              mov       byte [esi+43h],1
  004e04ce  e86d6c0600            call      00547140h
  004e04d3  66894610              mov       [esi+10h],ax
  004e04d7  66895e12              mov       [esi+12h],bx
  004e04db  eb32                  jmp       short 004E050Fh

LAB_004e04dd:
  004e04dd  c6464203              mov       byte [esi+42h],3
  004e04e1  66895e12              mov       [esi+12h],bx
  004e04e5  e8566c0600            call      00547140h
  004e04ea  99                    cdq
  004e04eb  b905000000            mov       ecx,5
  004e04f0  f7f9                  idiv      ecx
  004e04f2  80c20a                add       dl,0Ah
  004e04f5  885643                mov       [esi+43h],dl
  004e04f8  e8436c0600            call      00547140h
  004e04fd  99                    cdq
  004e04fe  33c2                  xor       eax,edx
  004e0500  2bc2                  sub       eax,edx
  004e0502  25ff0f0000            and       eax,0FFFh
  004e0507  33c2                  xor       eax,edx
  004e0509  2bc2                  sub       eax,edx
  004e050b  66894610              mov       [esi+10h],ax

LAB_004e050f:
  004e050f  8b7c2420              mov       edi,[esp+20h]

LAB_004e0513:
  004e0513  837c241464            cmp       dword [esp+14h],64h
  004e0518  7d38                  jge       short 004E0552h
  004e051a  c6464203              mov       byte [esi+42h],3
  004e051e  66895e12              mov       [esi+12h],bx
  004e0522  e8196c0600            call      00547140h
  004e0527  99                    cdq
  004e0528  b905000000            mov       ecx,5
  004e052d  f7f9                  idiv      ecx
  004e052f  80c20a                add       dl,0Ah
  004e0532  885643                mov       [esi+43h],dl
  004e0535  e8066c0600            call      00547140h
  004e053a  99                    cdq
  004e053b  33c2                  xor       eax,edx
  004e053d  2bc2                  sub       eax,edx
  004e053f  25ff0f0000            and       eax,0FFFh
  004e0544  33c2                  xor       eax,edx
  004e0546  2bc2                  sub       eax,edx
  004e0548  66894610              mov       [esi+10h],ax
  004e054c  eb04                  jmp       short 004E0552h

LAB_004e054e:
  004e054e  8b7c2420              mov       edi,[esp+20h]

LAB_004e0552:
  004e0552  8b442414              mov       eax,[esp+14h]
  004e0556  3d58020000            cmp       eax,258h
  004e055b  0f8dba000000          jge       near 004E061Bh
  004e0561  3de6000000            cmp       eax,0E6h
  004e0566  0f8caf000000          jl        near 004E061Bh
  004e056c  e8cf6b0600            call      00547140h
  004e0571  99                    cdq
  004e0572  b964000000            mov       ecx,64h
  004e0577  f7f9                  idiv      ecx
  004e0579  83fa14                cmp       edx,14h
  004e057c  7d0d                  jge       short 004E058Bh
  004e057e  c6464201              mov       byte [esi+42h],1
  004e0582  c6464301              mov       byte [esi+43h],1
  004e0586  e990000000            jmp       004E061Bh

LAB_004e058b:
  004e058b  83fa28                cmp       edx,28h
  004e058e  7d1f                  jge       short 004E05AFh
  004e0590  c6464202              mov       byte [esi+42h],2
  004e0594  e8a76b0600            call      00547140h
  004e0599  99                    cdq
  004e059a  b90a000000            mov       ecx,0Ah
  004e059f  f7f9                  idiv      ecx
  004e05a1  8bc7                  mov       eax,edi
  004e05a3  d1f8                  sar       eax,1
  004e05a5  02d0                  add       dl,al
  004e05a7  80c214                add       dl,14h
  004e05aa  885643                mov       [esi+43h],dl
  004e05ad  eb66                  jmp       short 004E0615h

LAB_004e05af:
  004e05af  83fa3c                cmp       edx,3Ch
  004e05b2  7d17                  jge       short 004E05CBh
  004e05b4  c6464206              mov       byte [esi+42h],6
  004e05b8  c6464301              mov       byte [esi+43h],1
  004e05bc  e87f6b0600            call      00547140h
  004e05c1  66894610              mov       [esi+10h],ax
  004e05c5  66895e12              mov       [esi+12h],bx
  004e05c9  eb50                  jmp       short 004E061Bh

LAB_004e05cb:
  004e05cb  83fa50                cmp       edx,50h
  004e05ce  7d3a                  jge       short 004E060Ah
  004e05d0  c6464203              mov       byte [esi+42h],3
  004e05d4  66895e12              mov       [esi+12h],bx
  004e05d8  e8636b0600            call      00547140h
  004e05dd  99                    cdq
  004e05de  b90a000000            mov       ecx,0Ah
  004e05e3  f7f9                  idiv      ecx
  004e05e5  8bc7                  mov       eax,edi
  004e05e7  d1f8                  sar       eax,1
  004e05e9  02d0                  add       dl,al
  004e05eb  80c214                add       dl,14h
  004e05ee  885643                mov       [esi+43h],dl
  004e05f1  e84a6b0600            call      00547140h
  004e05f6  99                    cdq
  004e05f7  33c2                  xor       eax,edx
  004e05f9  2bc2                  sub       eax,edx
  004e05fb  25ff0f0000            and       eax,0FFFh
  004e0600  33c2                  xor       eax,edx
  004e0602  2bc2                  sub       eax,edx
  004e0604  66894610              mov       [esi+10h],ax
  004e0608  eb11                  jmp       short 004E061Bh

LAB_004e060a:
  004e060a  885e42                mov       [esi+42h],bl
  004e060d  c6464301              mov       byte [esi+43h],1
  004e0611  66895e12              mov       [esi+12h],bx

LAB_004e0615:
  004e0615  66c746105000          mov       word [esi+10h],50h

LAB_004e061b:
  004e061b  817c241458020000      cmp       dword [esp+14h],258h
  004e0623  0f8cef000000          jl        near 004E0718h
  004e0629  e8126b0600            call      00547140h
  004e062e  bd64000000            mov       ebp,64h
  004e0633  99                    cdq
  004e0634  8bcd                  mov       ecx,ebp
  004e0636  f7f9                  idiv      ecx
  004e0638  83fa1e                cmp       edx,1Eh
  004e063b  7d42                  jge       short 004E067Fh
  004e063d  8b442410              mov       eax,[esp+10h]
  004e0641  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004e0645  3bd1                  cmp       edx,ecx
  004e0647  0f8c91000000          jl        near 004E06DEh
  004e064d  885e42                mov       [esi+42h],bl
  004e0650  e8eb6a0600            call      00547140h
  004e0655  99                    cdq
  004e0656  b90a000000            mov       ecx,0Ah
  004e065b  66895e12              mov       [esi+12h],bx
  004e065f  f7f9                  idiv      ecx
  004e0661  8b4c2410              mov       ecx,[esp+10h]
  004e0665  8bc7                  mov       eax,edi
  004e0667  c1f802                sar       eax,2
  004e066a  02d0                  add       dl,al
  004e066c  885643                mov       [esi+43h],dl
  004e066f  8d540950              lea       edx,[ecx+ecx+50h]
  004e0673  66895610              mov       [esi+10h],dx
  004e0677  5f                    pop       edi
  004e0678  5e                    pop       esi
  004e0679  5d                    pop       ebp
  004e067a  5b                    pop       ebx
  004e067b  83c47c                add       esp,7Ch
  004e067e  c3                    ret

LAB_004e067f:
  004e067f  83fa28                cmp       edx,28h
  004e0682  7d40                  jge       short 004E06C4h
  004e0684  c6464203              mov       byte [esi+42h],3
  004e0688  66895e12              mov       [esi+12h],bx
  004e068c  e8af6a0600            call      00547140h
  004e0691  99                    cdq
  004e0692  b90a000000            mov       ecx,0Ah
  004e0697  f7f9                  idiv      ecx
  004e0699  8bc7                  mov       eax,edi
  004e069b  d1f8                  sar       eax,1
  004e069d  02d0                  add       dl,al
  004e069f  80c214                add       dl,14h
  004e06a2  885643                mov       [esi+43h],dl
  004e06a5  e8966a0600            call      00547140h
  004e06aa  99                    cdq
  004e06ab  33c2                  xor       eax,edx
  004e06ad  2bc2                  sub       eax,edx
  004e06af  25ff0f0000            and       eax,0FFFh
  004e06b4  33c2                  xor       eax,edx
  004e06b6  2bc2                  sub       eax,edx
  004e06b8  66894610              mov       [esi+10h],ax
  004e06bc  5f                    pop       edi
  004e06bd  5e                    pop       esi
  004e06be  5d                    pop       ebp
  004e06bf  5b                    pop       ebx
  004e06c0  83c47c                add       esp,7Ch
  004e06c3  c3                    ret

LAB_004e06c4:
  004e06c4  83fa3c                cmp       edx,3Ch
  004e06c7  7d10                  jge       short 004E06D9h
  004e06c9  c6464201              mov       byte [esi+42h],1
  004e06cd  c6464301              mov       byte [esi+43h],1
  004e06d1  5f                    pop       edi
  004e06d2  5e                    pop       esi
  004e06d3  5d                    pop       ebp
  004e06d4  5b                    pop       ebx
  004e06d5  83c47c                add       esp,7Ch
  004e06d8  c3                    ret

LAB_004e06d9:
  004e06d9  83fa50                cmp       edx,50h
  004e06dc  7d29                  jge       short 004E0707h

LAB_004e06de:
  004e06de  c6464202              mov       byte [esi+42h],2
  004e06e2  e8596a0600            call      00547140h
  004e06e7  99                    cdq
  004e06e8  b90a000000            mov       ecx,0Ah
  004e06ed  66896e10              mov       [esi+10h],bp
  004e06f1  f7f9                  idiv      ecx
  004e06f3  8bc7                  mov       eax,edi
  004e06f5  d1f8                  sar       eax,1
  004e06f7  02d0                  add       dl,al
  004e06f9  80c214                add       dl,14h
  004e06fc  885643                mov       [esi+43h],dl
  004e06ff  5f                    pop       edi
  004e0700  5e                    pop       esi
  004e0701  5d                    pop       ebp
  004e0702  5b                    pop       ebx
  004e0703  83c47c                add       esp,7Ch
  004e0706  c3                    ret

LAB_004e0707:
  004e0707  c6464206              mov       byte [esi+42h],6
  004e070b  c6464301              mov       byte [esi+43h],1
  004e070f  e82c6a0600            call      00547140h
  004e0714  66894610              mov       [esi+10h],ax

LAB_004e0718:
  004e0718  5f                    pop       edi
  004e0719  5e                    pop       esi
  004e071a  5d                    pop       ebp
  004e071b  5b                    pop       ebx
  004e071c  83c47c                add       esp,7Ch
  004e071f  c3                    ret
  004e0720  8bfd                  mov       edi,ebp
  004e0722  4d                    dec       ebp
  004e0723  00cb                  add       bl,cl
  004e0725  fd                    std
  004e0726  4d                    dec       ebp
  004e0727  0052fe                add       [edx-2],dl
  004e072a  4d                    dec       ebp
  004e072b  00e9                  add       cl,ch
  004e072d  fe4d00                dec       byte [ebp]
  004e0730  6b014e                imul      eax,[ecx],4Eh
  004e0733  006b01                add       [ebx+1],ch
  004e0736  4e                    dec       esi
  004e0737  00b9ff4d006f          add       [ecx+6F004DFFh],bh
  004e073d  004e00                add       [esi],cl
  004e0740  6b014e                imul      eax,[ecx],4Eh
  004e0743  006b01                add       [ebx+1],ch
  004e0746  4e                    dec       esi
  004e0747  006b01                add       [ebx+1],ch
  004e074a  4e                    dec       esi
  004e074b  00ce                  add       dh,cl
  004e074d  004e00                add       [esi],cl

62 cible(s) de saut interne : 0x004dfb8e, 0x004dfcd1, 0x004dfcf4, 0x004dfd03, 0x004dfd05, 0x004dfd6c, 0x004dfd76, 0x004dfdab, 0x004dfdc3, 0x004dfdec, 0x004dfe14, 0x004dfe37, 0x004dfe72, 0x004dfe7e, 0x004dfea8, 0x004dfeb2, 0x004dfeb6, 0x004dff04, 0x004dff83, 0x004dff8e, 0x004dff92, 0x004dffcb, 0x004dffe2, 0x004e004d, 0x004e008b, 0x004e0097, 0x004e00fb, 0x004e0146, 0x004e014f, 0x004e0153, 0x004e0167, 0x004e016b, 0x004e0245, 0x004e0251, 0x004e0253, 0x004e0264, 0x004e0289, 0x004e02af, 0x004e0331, 0x004e0341, 0x004e036d, 0x004e0395, 0x004e0477, 0x004e049d, 0x004e04ad, 0x004e04dd, 0x004e050f, 0x004e0513, 0x004e054e, 0x004e0552, 0x004e058b, 0x004e05af, 0x004e05cb, 0x004e060a, 0x004e0615, 0x004e061b, 0x004e067f, 0x004e06c4, 0x004e06d9, 0x004e06de, 0x004e0707, 0x004e0718
