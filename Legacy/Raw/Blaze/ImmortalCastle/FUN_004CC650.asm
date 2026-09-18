Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004cc650  section .text  offset fichier 0xcba50  3088 octets

  004cc650  83ec7c                sub       esp,7Ch
  004cc653  53                    push      ebx
  004cc654  55                    push      ebp
  004cc655  56                    push      esi
  004cc656  8bb4248c000000        mov       esi,[esp+8Ch]
  004cc65d  b864000000            mov       eax,64h
  004cc662  33db                  xor       ebx,ebx
  004cc664  668944245a            mov       [esp+5Ah],ax
  004cc669  668944245e            mov       [esp+5Eh],ax
  004cc66e  8a4641                mov       al,[esi+41h]
  004cc671  57                    push      edi
  004cc672  3ac3                  cmp       al,bl
  004cc674  66895c2458            mov       [esp+58h],bx
  004cc679  66c744245a1e00        mov       word [esp+5Ah],1Eh
  004cc680  66c744245c3200        mov       word [esp+5Ch],32h
  004cc687  66895c2460            mov       [esp+60h],bx
  004cc68c  7510                  jne       short 004CC69Eh
  004cc68e  53                    push      ebx
  004cc68f  6a03                  push      3
  004cc691  56                    push      esi
  004cc692  c6464101              mov       byte [esi+41h],1
  004cc696  e8e5c6ffff            call      004C8D80h
  004cc69b  83c40c                add       esp,0Ch

LAB_004cc69e:
  004cc69e  8b461c                mov       eax,[esi+1Ch]
  004cc6a1  8b4e20                mov       ecx,[esi+20h]
  004cc6a4  8b5624                mov       edx,[esi+24h]
  004cc6a7  8d6e1c                lea       ebp,[esi+1Ch]
  004cc6aa  8944244c              mov       [esp+4Ch],eax
  004cc6ae  668b4632              mov       ax,[esi+32h]
  004cc6b2  894c2450              mov       [esp+50h],ecx
  004cc6b6  8b4e7c                mov       ecx,[esi+7Ch]
  004cc6b9  89542454              mov       [esp+54h],edx
  004cc6bd  668b9690000000        mov       dx,[esi+90h]
  004cc6c4  668944247a            mov       [esp+7Ah],ax
  004cc6c9  8b8680000000          mov       eax,[esi+80h]
  004cc6cf  894c2428              mov       [esp+28h],ecx
  004cc6d3  8b8e84000000          mov       ecx,[esi+84h]
  004cc6d9  668954242c            mov       [esp+2Ch],dx
  004cc6de  8b9688000000          mov       edx,[esi+88h]
  004cc6e4  8b7e6c                mov       edi,[esi+6Ch]
  004cc6e7  89442430              mov       [esp+30h],eax
  004cc6eb  8b868c000000          mov       eax,[esi+8Ch]
  004cc6f1  894c2434              mov       [esp+34h],ecx
  004cc6f5  89542438              mov       [esp+38h],edx
  004cc6f9  8d4c2440              lea       ecx,[esp+40h]
  004cc6fd  8944243c              mov       [esp+3Ch],eax
  004cc701  8d542444              lea       edx,[esp+44h]
  004cc705  51                    push      ecx
  004cc706  8d44244c              lea       eax,[esp+4Ch]
  004cc70a  52                    push      edx
  004cc70b  50                    push      eax
  004cc70c  56                    push      esi
  004cc70d  e86ec20600            call      00538980h
  004cc712  83c410                add       esp,10h
  004cc715  8d4c2424              lea       ecx,[esp+24h]
  004cc719  8d54241c              lea       edx,[esp+1Ch]
  004cc71d  8d442418              lea       eax,[esp+18h]
  004cc721  51                    push      ecx
  004cc722  52                    push      edx
  004cc723  8d4c241c              lea       ecx,[esp+1Ch]
  004cc727  50                    push      eax
  004cc728  8d54241c              lea       edx,[esp+1Ch]
  004cc72c  51                    push      ecx
  004cc72d  52                    push      edx
  004cc72e  56                    push      esi
  004cc72f  e8ec970600            call      00535F20h
  004cc734  8b4c2428              mov       ecx,[esp+28h]
  004cc738  b840000000            mov       eax,40h
  004cc73d  2bc1                  sub       eax,ecx
  004cc73f  83c418                add       esp,18h
  004cc742  89442420              mov       [esp+20h],eax
  004cc746  8b442424              mov       eax,[esp+24h]
  004cc74a  50                    push      eax
  004cc74b  56                    push      esi
  004cc74c  e88f960000            call      004D5DE0h
  004cc751  8b4500                mov       eax,[ebp]
  004cc754  8b571c                mov       edx,[edi+1Ch]
  004cc757  8b4e20                mov       ecx,[esi+20h]
  004cc75a  2bc2                  sub       eax,edx
  004cc75c  8b5720                mov       edx,[edi+20h]
  004cc75f  83c408                add       esp,8
  004cc762  2bca                  sub       ecx,edx
  004cc764  8b5624                mov       edx,[esi+24h]
  004cc767  2b5724                sub       edx,[edi+24h]
  004cc76a  c1f80c                sar       eax,0Ch
  004cc76d  8bf8                  mov       edi,eax
  004cc76f  c1f90c                sar       ecx,0Ch
  004cc772  0faff8                imul      edi,eax
  004cc775  8bc1                  mov       eax,ecx
  004cc777  c1fa0c                sar       edx,0Ch
  004cc77a  0fafc1                imul      eax,ecx
  004cc77d  8bca                  mov       ecx,edx
  004cc77f  03f8                  add       edi,eax
  004cc781  0fafca                imul      ecx,edx
  004cc784  03f9                  add       edi,ecx
  004cc786  57                    push      edi
  004cc787  e884920700            call      00545A10h
  004cc78c  8b4c241c              mov       ecx,[esp+1Ch]
  004cc790  8bf8                  mov       edi,eax
  004cc792  3bcb                  cmp       ecx,ebx
  004cc794  897c2414              mov       [esp+14h],edi
  004cc798  0f84dd040000          je        near 004CCC7Bh
  004cc79e  8b06                  mov       eax,[esi]
  004cc7a0  0fbf5e18              movsx     ebx,word [esi+18h]
  004cc7a4  25fffffffd            and       eax,0FDFFFFFFh
  004cc7a9  8906                  mov       [esi],eax
  004cc7ab  8bd0                  mov       edx,eax
  004cc7ad  33c0                  xor       eax,eax
  004cc7af  8a4604                mov       al,[esi+4]
  004cc7b2  f7c200000001          test      edx,1000000h
  004cc7b8  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004cc7bf  8b0498                mov       eax,[eax+ebx*4]
  004cc7c2  741d                  je        short 004CC7E1h
  004cc7c4  66ff461a              inc       word [esi+1Ah]
  004cc7c8  668b561a              mov       dx,[esi+1Ah]
  004cc7cc  33db                  xor       ebx,ebx
  004cc7ce  668b5816              mov       bx,[eax+16h]
  004cc7d2  0fbfd2                movsx     edx,dx
  004cc7d5  3bd3                  cmp       edx,ebx
  004cc7d7  7c2b                  jl        short 004CC804h
  004cc7d9  66c7461a0000          mov       word [esi+1Ah],0
  004cc7df  eb23                  jmp       short 004CC804h

LAB_004cc7e1:
  004cc7e1  66ff461a              inc       word [esi+1Ah]
  004cc7e5  33db                  xor       ebx,ebx
  004cc7e7  668b5816              mov       bx,[eax+16h]
  004cc7eb  0fbf461a              movsx     eax,word [esi+1Ah]
  004cc7ef  3bc3                  cmp       eax,ebx
  004cc7f1  7c11                  jl        short 004CC804h
  004cc7f3  668b461a              mov       ax,[esi+1Ah]
  004cc7f7  48                    dec       eax
  004cc7f8  81ca00000002          or        edx,2000000h
  004cc7fe  6689461a              mov       [esi+1Ah],ax
  004cc802  8916                  mov       [esi],edx

LAB_004cc804:
  004cc804  8a4642                mov       al,[esi+42h]
  004cc807  33d2                  xor       edx,edx
  004cc809  3c05                  cmp       al,5
  004cc80b  8b06                  mov       eax,[esi]
  004cc80d  7504                  jne       short 004CC813h
  004cc80f  0c10                  or        al,10h
  004cc811  eb02                  jmp       short 004CC815h

LAB_004cc813:
  004cc813  24ef                  and       al,0EFh

LAB_004cc815:
  004cc815  8906                  mov       [esi],eax
  004cc817  8a06                  mov       al,[esi]
  004cc819  a802                  test      al,2
  004cc81b  bb0a000000            mov       ebx,0Ah
  004cc820  7464                  je        short 004CC886h
  004cc822  66395e1a              cmp       [esi+1Ah],bx
  004cc826  7c5e                  jl        short 004CC886h
  004cc828  8d4c2458              lea       ecx,[esp+58h]
  004cc82c  6a00                  push      0
  004cc82e  51                    push      ecx
  004cc82f  55                    push      ebp
  004cc830  56                    push      esi
  004cc831  e8fad30400            call      00519C30h
  004cc836  8bf8                  mov       edi,eax
  004cc838  a1b8e05d00            mov       eax,[5DE0B8h]
  004cc83d  83c410                add       esp,10h
  004cc840  85c0                  test      eax,eax
  004cc842  7438                  je        short 004CC87Ch
  004cc844  66395e1a              cmp       [esi+1Ah],bx
  004cc848  7532                  jne       short 004CC87Ch
  004cc84a  8b0f                  mov       ecx,[edi]
  004cc84c  57                    push      edi
  004cc84d  83c940                or        ecx,40h
  004cc850  56                    push      esi
  004cc851  66c787960000001000    mov       word [edi+96h],10h
  004cc85a  890f                  mov       [edi],ecx
  004cc85c  e81fd30600            call      00539B80h
  004cc861  83c408                add       esp,8
  004cc864  83c71c                add       edi,1Ch
  004cc867  57                    push      edi
  004cc868  6800180000            push      1800h
  004cc86d  6800030000            push      300h
  004cc872  6a03                  push      3
  004cc874  e8a7120700            call      0053DB20h
  004cc879  83c410                add       esp,10h

LAB_004cc87c:
  004cc87c  8b4c241c              mov       ecx,[esp+1Ch]
  004cc880  8b7c2414              mov       edi,[esp+14h]
  004cc884  33d2                  xor       edx,edx

LAB_004cc886:
  004cc886  33c0                  xor       eax,eax
  004cc888  8a4642                mov       al,[esi+42h]
  004cc88b  83f80b                cmp       eax,0Bh
  004cc88e  0f87e7030000          ja        near 004CCC7Bh
  004cc894  ff248530d24c00        jmp       dword [eax*4+4CD230h]
  004cc89b  66395612              cmp       [esi+12h],dx
  004cc89f  751a                  jne       short 004CC8BBh
  004cc8a1  8b06                  mov       eax,[esi]
  004cc8a3  66895618              mov       [esi+18h],dx
  004cc8a7  0d00000001            or        eax,1000000h
  004cc8ac  6689561a              mov       [esi+1Ah],dx
  004cc8b0  8906                  mov       [esi],eax
  004cc8b2  66c746120100          mov       word [esi+12h],1
  004cc8b8  89565c                mov       [esi+5Ch],edx

LAB_004cc8bb:
  004cc8bb  0fbf4610              movsx     eax,word [esi+10h]
  004cc8bf  3bf8                  cmp       edi,eax
  004cc8c1  0f8cb0030000          jl        near 004CCC77h
  004cc8c7  8a4643                mov       al,[esi+43h]
  004cc8ca  84c0                  test      al,al
  004cc8cc  7405                  je        short 004CC8D3h
  004cc8ce  fec8                  dec       al
  004cc8d0  884643                mov       [esi+43h],al

LAB_004cc8d3:
  004cc8d3  89565c                mov       [esi+5Ch],edx
  004cc8d6  e9a0030000            jmp       004CCC7Bh
  004cc8db  66395612              cmp       [esi+12h],dx
  004cc8df  751b                  jne       short 004CC8FCh
  004cc8e1  8b0e                  mov       ecx,[esi]
  004cc8e3  b801000000            mov       eax,1
  004cc8e8  81c900000001          or        ecx,1000000h
  004cc8ee  66894618              mov       [esi+18h],ax
  004cc8f2  6689561a              mov       [esi+1Ah],dx
  004cc8f6  890e                  mov       [esi],ecx
  004cc8f8  66894612              mov       [esi+12h],ax

LAB_004cc8fc:
  004cc8fc  8b442418              mov       eax,[esp+18h]
  004cc900  8d88c8000000          lea       ecx,[eax+0C8h]
  004cc906  81e1ff0f0000          and       ecx,0FFFh
  004cc90c  81f990010000          cmp       ecx,190h
  004cc912  7f10                  jg        short 004CC924h
  004cc914  c6464300              mov       byte [esi+43h],0
  004cc918  c7465c00500000        mov       dword [esi+5Ch],5000h
  004cc91f  e957030000            jmp       004CCC7Bh

LAB_004cc924:
  004cc924  25ff0f0000            and       eax,0FFFh
  004cc929  3d00080000            cmp       eax,800h
  004cc92e  7d17                  jge       short 004CC947h
  004cc930  8b542410              mov       edx,[esp+10h]
  004cc934  c7465c00500000        mov       dword [esi+5Ch],5000h
  004cc93b  8d4232                lea       eax,[edx+32h]
  004cc93e  66014632              add       [esi+32h],ax
  004cc942  e934030000            jmp       004CCC7Bh

LAB_004cc947:
  004cc947  8b4c2410              mov       ecx,[esp+10h]
  004cc94b  b8ceffffff            mov       eax,0FFFFFFCEh
  004cc950  2bc1                  sub       eax,ecx
  004cc952  c7465c00500000        mov       dword [esi+5Ch],5000h
  004cc959  66014632              add       [esi+32h],ax
  004cc95d  e919030000            jmp       004CCC7Bh
  004cc962  668b5612              mov       dx,[esi+12h]
  004cc966  8b4c2410              mov       ecx,[esp+10h]
  004cc96a  bd01000000            mov       ebp,1
  004cc96f  663bd5                cmp       dx,bp
  004cc972  7552                  jne       short 004CC9C6h
  004cc974  0fbf4610              movsx     eax,word [esi+10h]
  004cc978  3bf8                  cmp       edi,eax
  004cc97a  7d06                  jge       short 004CC982h
  004cc97c  c6464300              mov       byte [esi+43h],0
  004cc980  eb44                  jmp       short 004CC9C6h

LAB_004cc982:
  004cc982  8a4643                mov       al,[esi+43h]
  004cc985  84c0                  test      al,al
  004cc987  7405                  je        short 004CC98Eh
  004cc989  fec8                  dec       al
  004cc98b  884643                mov       [esi+43h],al

LAB_004cc98e:
  004cc98e  8b442418              mov       eax,[esp+18h]
  004cc992  8db800010000          lea       edi,[eax+100h]
  004cc998  81e7ff0f0000          and       edi,0FFFh
  004cc99e  81ff00020000          cmp       edi,200h
  004cc9a4  7e20                  jle       short 004CC9C6h
  004cc9a6  25ff0f0000            and       eax,0FFFh
  004cc9ab  3d00080000            cmp       eax,800h
  004cc9b0  7d06                  jge       short 004CC9B8h
  004cc9b2  8d440932              lea       eax,[ecx+ecx+32h]
  004cc9b6  eb0a                  jmp       short 004CC9C2h

LAB_004cc9b8:
  004cc9b8  8d3c09                lea       edi,[ecx+ecx]
  004cc9bb  b8ceffffff            mov       eax,0FFFFFFCEh
  004cc9c0  2bc7                  sub       eax,edi

LAB_004cc9c2:
  004cc9c2  66014632              add       [esi+32h],ax

LAB_004cc9c6:
  004cc9c6  6685d2                test      dx,dx
  004cc9c9  0f85ac020000          jne       near 004CCC7Bh
  004cc9cf  8b3e                  mov       edi,[esi]
  004cc9d1  8d0cc9                lea       ecx,[ecx+ecx*8]
  004cc9d4  c1e104                shl       ecx,4
  004cc9d7  81cf00000001          or        edi,1000000h
  004cc9dd  81c100c00000          add       ecx,0C000h
  004cc9e3  66896e18              mov       [esi+18h],bp
  004cc9e7  6689561a              mov       [esi+1Ah],dx
  004cc9eb  893e                  mov       [esi],edi
  004cc9ed  66896e12              mov       [esi+12h],bp
  004cc9f1  894e5c                mov       [esi+5Ch],ecx
  004cc9f4  e982020000            jmp       004CCC7Bh
  004cc9f9  bb01000000            mov       ebx,1
  004cc9fe  66395e12              cmp       [esi+12h],bx
  004cca02  0f859a000000          jne       near 004CCAA2h
  004cca08  8a4643                mov       al,[esi+43h]
  004cca0b  84c0                  test      al,al
  004cca0d  7405                  je        short 004CCA14h
  004cca0f  fec8                  dec       al
  004cca11  884643                mov       [esi+43h],al

LAB_004cca14:
  004cca14  8b442410              mov       eax,[esp+10h]
  004cca18  8d14c0                lea       edx,[eax+eax*8]
  004cca1b  c1e204                shl       edx,4
  004cca1e  8d8200600000          lea       eax,[edx+6000h]
  004cca24  0fafc8                imul      ecx,eax
  004cca27  89465c                mov       [esi+5Ch],eax
  004cca2a  0fbf4610              movsx     eax,word [esi+10h]
  004cca2e  c1f90c                sar       ecx,0Ch
  004cca31  50                    push      eax
  004cca32  8bf9                  mov       edi,ecx
  004cca34  e8e38f0700            call      00545A1Ch
  004cca39  0fafc7                imul      eax,edi
  004cca3c  8b4d00                mov       ecx,[ebp]
  004cca3f  c1f80c                sar       eax,0Ch
  004cca42  03c8                  add       ecx,eax
  004cca44  894d00                mov       [ebp],ecx
  004cca47  0fbf4e10              movsx     ecx,word [esi+10h]
  004cca4b  51                    push      ecx
  004cca4c  e8c58f0700            call      00545A16h
  004cca51  0fafc7                imul      eax,edi
  004cca54  8b6e24                mov       ebp,[esi+24h]
  004cca57  c7465c00000000        mov       dword [esi+5Ch],0
  004cca5e  c1f80c                sar       eax,0Ch
  004cca61  03e8                  add       ebp,eax
  004cca63  8b442418              mov       eax,[esp+18h]
  004cca67  896e24                mov       [esi+24h],ebp
  004cca6a  8d9000010000          lea       edx,[eax+100h]
  004cca70  81e2ff0f0000          and       edx,0FFFh
  004cca76  81fa00020000          cmp       edx,200h
  004cca7c  7e24                  jle       short 004CCAA2h
  004cca7e  25ff0f0000            and       eax,0FFFh
  004cca83  3d00080000            cmp       eax,800h
  004cca88  7d09                  jge       short 004CCA93h
  004cca8a  8b442410              mov       eax,[esp+10h]
  004cca8e  83c032                add       eax,32h
  004cca91  eb0b                  jmp       short 004CCA9Eh

LAB_004cca93:
  004cca93  8b4c2410              mov       ecx,[esp+10h]
  004cca97  b8ceffffff            mov       eax,0FFFFFFCEh
  004cca9c  2bc1                  sub       eax,ecx

LAB_004cca9e:
  004cca9e  66014632              add       [esi+32h],ax

LAB_004ccaa2:
  004ccaa2  66837e1200            cmp       word [esi+12h],0
  004ccaa7  0f85ce010000          jne       near 004CCC7Bh
  004ccaad  8b06                  mov       eax,[esi]
  004ccaaf  66895e18              mov       [esi+18h],bx
  004ccab3  0d00000001            or        eax,1000000h
  004ccab8  66c7461a0000          mov       word [esi+1Ah],0
  004ccabe  8906                  mov       [esi],eax
  004ccac0  66895e12              mov       [esi+12h],bx
  004ccac4  e9b2010000            jmp       004CCC7Bh
  004ccac9  66395612              cmp       [esi+12h],dx
  004ccacd  7523                  jne       short 004CCAF2h
  004ccacf  8b06                  mov       eax,[esi]
  004ccad1  a801                  test      al,1
  004ccad3  7406                  je        short 004CCADBh
  004ccad5  c6464300              mov       byte [esi+43h],0
  004ccad9  eb17                  jmp       short 004CCAF2h

LAB_004ccadb:
  004ccadb  25fffffffe            and       eax,0FEFFFFFFh
  004ccae0  66c746180300          mov       word [esi+18h],3
  004ccae6  8906                  mov       [esi],eax
  004ccae8  6689561a              mov       [esi+1Ah],dx
  004ccaec  66c746120100          mov       word [esi+12h],1

LAB_004ccaf2:
  004ccaf2  66837e1201            cmp       word [esi+12h],1
  004ccaf7  7564                  jne       short 004CCB5Dh
  004ccaf9  66395e1a              cmp       [esi+1Ah],bx
  004ccafd  7c5e                  jl        short 004CCB5Dh
  004ccaff  8b542410              mov       edx,[esp+10h]
  004ccb03  8b5e54                mov       ebx,[esi+54h]
  004ccb06  81c300400100          add       ebx,14000h
  004ccb0c  66c746120200          mov       word [esi+12h],2
  004ccb12  8d04d500400100        lea       eax,[edx*8+14000h]
  004ccb19  895e54                mov       [esi+54h],ebx
  004ccb1c  0fafc8                imul      ecx,eax
  004ccb1f  89465c                mov       [esi+5Ch],eax
  004ccb22  0fbf4610              movsx     eax,word [esi+10h]
  004ccb26  c1f90c                sar       ecx,0Ch
  004ccb29  50                    push      eax
  004ccb2a  8bf9                  mov       edi,ecx
  004ccb2c  e8eb8e0700            call      00545A1Ch
  004ccb31  0fafc7                imul      eax,edi
  004ccb34  0fbf4e10              movsx     ecx,word [esi+10h]
  004ccb38  8b6e50                mov       ebp,[esi+50h]
  004ccb3b  51                    push      ecx
  004ccb3c  c1f80c                sar       eax,0Ch
  004ccb3f  03e8                  add       ebp,eax
  004ccb41  896e50                mov       [esi+50h],ebp
  004ccb44  e8cd8e0700            call      00545A16h
  004ccb49  0fafc7                imul      eax,edi
  004ccb4c  8b4e58                mov       ecx,[esi+58h]
  004ccb4f  c1f80c                sar       eax,0Ch
  004ccb52  03c8                  add       ecx,eax
  004ccb54  8b06                  mov       eax,[esi]
  004ccb56  0c03                  or        al,3
  004ccb58  894e58                mov       [esi+58h],ecx
  004ccb5b  8906                  mov       [esi],eax

LAB_004ccb5d:
  004ccb5d  66837e1202            cmp       word [esi+12h],2
  004ccb62  0f8513010000          jne       near 004CCC7Bh
  004ccb68  8b06                  mov       eax,[esi]
  004ccb6a  a801                  test      al,1
  004ccb6c  0f8509010000          jne       near 004CCC7Bh
  004ccb72  24fd                  and       al,0FDh
  004ccb74  c6464300              mov       byte [esi+43h],0
  004ccb78  8906                  mov       [esi],eax
  004ccb7a  e9fc000000            jmp       004CCC7Bh
  004ccb7f  668b4e12              mov       cx,[esi+12h]
  004ccb83  ba01000000            mov       edx,1
  004ccb88  663bca                cmp       cx,dx
  004ccb8b  751a                  jne       short 004CCBA7h
  004ccb8d  0fbf4610              movsx     eax,word [esi+10h]
  004ccb91  3bf8                  cmp       edi,eax
  004ccb93  7d06                  jge       short 004CCB9Bh
  004ccb95  c6464300              mov       byte [esi+43h],0
  004ccb99  eb0c                  jmp       short 004CCBA7h

LAB_004ccb9b:
  004ccb9b  8a4643                mov       al,[esi+43h]
  004ccb9e  84c0                  test      al,al
  004ccba0  7405                  je        short 004CCBA7h
  004ccba2  fec8                  dec       al
  004ccba4  884643                mov       [esi+43h],al

LAB_004ccba7:
  004ccba7  6685c9                test      cx,cx
  004ccbaa  0f85cb000000          jne       near 004CCC7Bh
  004ccbb0  8b442410              mov       eax,[esp+10h]
  004ccbb4  8b3e                  mov       edi,[esi]
  004ccbb6  66894e1a              mov       [esi+1Ah],cx
  004ccbba  81cf00000001          or        edi,1000000h
  004ccbc0  8d0cc0                lea       ecx,[eax+eax*8]
  004ccbc3  66895618              mov       [esi+18h],dx
  004ccbc7  c1e104                shl       ecx,4
  004ccbca  81c100c00000          add       ecx,0C000h
  004ccbd0  893e                  mov       [esi],edi
  004ccbd2  66895612              mov       [esi+12h],dx
  004ccbd6  894e5c                mov       [esi+5Ch],ecx
  004ccbd9  e99d000000            jmp       004CCC7Bh
  004ccbde  66395612              cmp       [esi+12h],dx
  004ccbe2  8b4c2410              mov       ecx,[esp+10h]
  004ccbe6  7523                  jne       short 004CCC0Bh
  004ccbe8  6689561a              mov       [esi+1Ah],dx
  004ccbec  8b16                  mov       edx,[esi]
  004ccbee  81e2fffffffe          and       edx,0FEFFFFFFh
  004ccbf4  83f932                cmp       ecx,32h
  004ccbf7  66c746120100          mov       word [esi+12h],1
  004ccbfd  66c746180300          mov       word [esi+18h],3
  004ccc03  8916                  mov       [esi],edx
  004ccc05  7e04                  jle       short 004CCC0Bh
  004ccc07  66895e1a              mov       [esi+1Ah],bx

LAB_004ccc0b:
  004ccc0b  66837e1201            cmp       word [esi+12h],1
  004ccc10  7551                  jne       short 004CCC63h
  004ccc12  66395e1a              cmp       [esi+1Ah],bx
  004ccc16  7c4b                  jl        short 004CCC63h
  004ccc18  8b442418              mov       eax,[esp+18h]
  004ccc1c  8b2e                  mov       ebp,[esi]
  004ccc1e  83cd02                or        ebp,2
  004ccc21  66c746120200          mov       word [esi+12h],2
  004ccc27  8d9000010000          lea       edx,[eax+100h]
  004ccc2d  c7465c00e00100        mov       dword [esi+5Ch],1E000h
  004ccc34  81e2ff0f0000          and       edx,0FFFh
  004ccc3a  892e                  mov       [esi],ebp
  004ccc3c  81fa00020000          cmp       edx,200h
  004ccc42  7e1f                  jle       short 004CCC63h
  004ccc44  25ff0f0000            and       eax,0FFFh
  004ccc49  3d00080000            cmp       eax,800h
  004ccc4e  7d06                  jge       short 004CCC56h
  004ccc50  8d440932              lea       eax,[ecx+ecx+32h]
  004ccc54  eb09                  jmp       short 004CCC5Fh

LAB_004ccc56:
  004ccc56  03c9                  add       ecx,ecx
  004ccc58  b8ceffffff            mov       eax,0FFFFFFCEh
  004ccc5d  2bc1                  sub       eax,ecx

LAB_004ccc5f:
  004ccc5f  66014632              add       [esi+32h],ax

LAB_004ccc63:
  004ccc63  66837e1202            cmp       word [esi+12h],2
  004ccc68  7511                  jne       short 004CCC7Bh
  004ccc6a  8b06                  mov       eax,[esi]
  004ccc6c  a900000002            test      eax,2000000h
  004ccc71  7408                  je        short 004CCC7Bh
  004ccc73  24fd                  and       al,0FDh
  004ccc75  8906                  mov       [esi],eax

LAB_004ccc77:
  004ccc77  c6464300              mov       byte [esi+43h],0

LAB_004ccc7b:
  004ccc7b  0fbf5632              movsx     edx,word [esi+32h]
  004ccc7f  52                    push      edx
  004ccc80  e8978d0700            call      00545A1Ch
  004ccc85  89442464              mov       [esp+64h],eax
  004ccc89  0fbf4632              movsx     eax,word [esi+32h]
  004ccc8d  50                    push      eax
  004ccc8e  e8838d0700            call      00545A16h
  004ccc93  8b5650                mov       edx,[esi+50h]
  004ccc96  8b4c241c              mov       ecx,[esp+1Ch]
  004ccc9a  8944246c              mov       [esp+6Ch],eax
  004ccc9e  8b5e54                mov       ebx,[esi+54h]
  004ccca1  0faf4e5c              imul      ecx,[esi+5Ch]
  004ccca5  8bc2                  mov       eax,edx
  004ccca7  8bfa                  mov       edi,edx
  004ccca9  c1f803                sar       eax,3
  004cccac  2bf8                  sub       edi,eax
  004cccae  8beb                  mov       ebp,ebx
  004cccb0  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004cccb7  c1fa08                sar       edx,8
  004cccba  2bfa                  sub       edi,edx
  004cccbc  8bd0                  mov       edx,eax
  004cccbe  c1fd08                sar       ebp,8
  004cccc1  c1fa08                sar       edx,8
  004cccc4  2bd5                  sub       edx,ebp
  004cccc6  8b6e58                mov       ebp,[esi+58h]
  004cccc9  03d3                  add       edx,ebx
  004ccccb  8bdd                  mov       ebx,ebp
  004ccccd  2bd0                  sub       edx,eax
  004ccccf  8bc5                  mov       eax,ebp
  004cccd1  c1fb08                sar       ebx,8
  004cccd4  c1f90c                sar       ecx,0Ch
  004cccd7  2bc3                  sub       eax,ebx
  004cccd9  8bd9                  mov       ebx,ecx
  004cccdb  0faf5c2464            imul      ebx,[esp+64h]
  004ccce0  0faf4c246c            imul      ecx,[esp+6Ch]
  004ccce5  c1fd03                sar       ebp,3
  004ccce8  c1fb0c                sar       ebx,0Ch
  004ccceb  2bc5                  sub       eax,ebp
  004ccced  8b6e1c                mov       ebp,[esi+1Ch]
  004cccf0  03df                  add       ebx,edi
  004cccf2  56                    push      esi
  004cccf3  03eb                  add       ebp,ebx
  004cccf5  8b5e20                mov       ebx,[esi+20h]
  004cccf8  c1f90c                sar       ecx,0Ch
  004cccfb  896e1c                mov       [esi+1Ch],ebp
  004cccfe  8b6e24                mov       ebp,[esi+24h]
  004ccd01  03c8                  add       ecx,eax
  004ccd03  03da                  add       ebx,edx
  004ccd05  03e9                  add       ebp,ecx
  004ccd07  897e50                mov       [esi+50h],edi
  004ccd0a  895654                mov       [esi+54h],edx
  004ccd0d  894658                mov       [esi+58h],eax
  004ccd10  895e20                mov       [esi+20h],ebx
  004ccd13  896e24                mov       [esi+24h],ebp
  004ccd16  e8256c0600            call      00533940h
  004ccd1b  8b9690000000          mov       edx,[esi+90h]
  004ccd21  8b4660                mov       eax,[esi+60h]
  004ccd24  8b4e20                mov       ecx,[esi+20h]
  004ccd27  83c404                add       esp,4
  004ccd2a  80e6fc                and       dh,0FCh
  004ccd2d  3bc8                  cmp       ecx,eax
  004ccd2f  899690000000          mov       [esi+90h],edx
  004ccd35  7f2a                  jg        short 004CCD61h
  004ccd37  f6c680                test      dh,80h
  004ccd3a  7525                  jne       short 004CCD61h
  004ccd3c  8b0e                  mov       ecx,[esi]
  004ccd3e  894620                mov       [esi+20h],eax
  004ccd41  f6c101                test      cl,1
  004ccd44  740f                  je        short 004CCD55h
  004ccd46  83e1fe                and       ecx,0FFFFFFFEh
  004ccd49  33db                  xor       ebx,ebx
  004ccd4b  890e                  mov       [esi],ecx
  004ccd4d  895e54                mov       [esi+54h],ebx
  004ccd50  e928010000            jmp       004CCE7Dh

LAB_004ccd55:
  004ccd55  83e1fe                and       ecx,0FFFFFFFEh
  004ccd58  33db                  xor       ebx,ebx
  004ccd5a  890e                  mov       [esi],ecx
  004ccd5c  895e54                mov       [esi+54h],ebx
  004ccd5f  eb02                  jmp       short 004CCD63h

LAB_004ccd61:
  004ccd61  33db                  xor       ebx,ebx

LAB_004ccd63:
  004ccd63  8b0e                  mov       ecx,[esi]
  004ccd65  f6c101                test      cl,1
  004ccd68  750a                  jne       short 004CCD74h
  004ccd6a  394620                cmp       [esi+20h],eax
  004ccd6d  7e05                  jle       short 004CCD74h
  004ccd6f  83c901                or        ecx,1
  004ccd72  890e                  mov       [esi],ecx

LAB_004ccd74:
  004ccd74  f7c282ad0020          test      edx,2000AD82h
  004ccd7a  0f84fd000000          je        near 004CCE7Dh
  004ccd80  8b06                  mov       eax,[esi]
  004ccd82  a801                  test      al,1
  004ccd84  0f85c7000000          jne       near 004CCE51h
  004ccd8a  8a4e42                mov       cl,[esi+42h]
  004ccd8d  b007                  mov       al,7
  004ccd8f  3ac8                  cmp       cl,al
  004ccd91  7506                  jne       short 004CCD99h
  004ccd93  807e0c0b              cmp       byte [esi+0Ch],0Bh
  004ccd97  7426                  je        short 004CCDBFh

LAB_004ccd99:
  004ccd99  884642                mov       [esi+42h],al
  004ccd9c  e89fa30700            call      00547140h
  004ccda1  99                    cdq
  004ccda2  b928000000            mov       ecx,28h
  004ccda7  66895e12              mov       [esi+12h],bx
  004ccdab  f7f9                  idiv      ecx
  004ccdad  8b06                  mov       eax,[esi]
  004ccdaf  66c746105000          mov       word [esi+10h],50h
  004ccdb5  80c214                add       dl,14h
  004ccdb8  24fd                  and       al,0FDh
  004ccdba  885643                mov       [esi+43h],dl
  004ccdbd  8906                  mov       [esi],eax

LAB_004ccdbf:
  004ccdbf  8b16                  mov       edx,[esi]
  004ccdc1  8b442450              mov       eax,[esp+50h]
  004ccdc5  8b4c2454              mov       ecx,[esp+54h]
  004ccdc9  83e2fd                and       edx,0FFFFFFFDh
  004ccdcc  894620                mov       [esi+20h],eax
  004ccdcf  8b44242c              mov       eax,[esp+2Ch]
  004ccdd3  8916                  mov       [esi],edx
  004ccdd5  8b54244c              mov       edx,[esp+4Ch]
  004ccdd9  25ffff0000            and       eax,0FFFFh
  004ccdde  89561c                mov       [esi+1Ch],edx
  004ccde1  8b542428              mov       edx,[esp+28h]
  004ccde5  898690000000          mov       [esi+90h],eax
  004ccdeb  8b442430              mov       eax,[esp+30h]
  004ccdef  894e24                mov       [esi+24h],ecx
  004ccdf2  8b4c2434              mov       ecx,[esp+34h]
  004ccdf6  89567c                mov       [esi+7Ch],edx
  004ccdf9  8b542438              mov       edx,[esp+38h]
  004ccdfd  898680000000          mov       [esi+80h],eax
  004cce03  8b44243c              mov       eax,[esp+3Ch]
  004cce07  6882ad0020            push      2000AD82h
  004cce0c  56                    push      esi
  004cce0d  898e84000000          mov       [esi+84h],ecx
  004cce13  899688000000          mov       [esi+88h],edx
  004cce19  89868c000000          mov       [esi+8Ch],eax
  004cce1f  e83c900000            call      004D5E60h
  004cce24  83c408                add       esp,8
  004cce27  663bc3                cmp       ax,bx
  004cce2a  7515                  jne       short 004CCE41h
  004cce2c  e80fa30700            call      00547140h
  004cce31  99                    cdq
  004cce32  33c2                  xor       eax,edx
  004cce34  2bc2                  sub       eax,edx
  004cce36  83e001                and       eax,1
  004cce39  33c2                  xor       eax,edx
  004cce3b  2bc2                  sub       eax,edx
  004cce3d  8d4400ff              lea       eax,[eax+eax-1]

LAB_004cce41:
  004cce41  8b4c2410              mov       ecx,[esp+10h]
  004cce45  83c140                add       ecx,40h
  004cce48  0fafc8                imul      ecx,eax
  004cce4b  66014e32              add       [esi+32h],cx
  004cce4f  eb2c                  jmp       short 004CCE7Dh

LAB_004cce51:
  004cce51  8b54244c              mov       edx,[esp+4Ch]
  004cce55  24fd                  and       al,0FDh
  004cce57  8906                  mov       [esi],eax
  004cce59  8b442454              mov       eax,[esp+54h]
  004cce5d  895e5c                mov       [esi+5Ch],ebx
  004cce60  895e50                mov       [esi+50h],ebx
  004cce63  895e58                mov       [esi+58h],ebx
  004cce66  89561c                mov       [esi+1Ch],edx
  004cce69  894624                mov       [esi+24h],eax
  004cce6c  885e42                mov       [esi+42h],bl
  004cce6f  c6464328              mov       byte [esi+43h],28h
  004cce73  66895e12              mov       [esi+12h],bx
  004cce77  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004cce7d:
  004cce7d  6aff                  push      0FFFFFFFFh
  004cce7f  6aff                  push      0FFFFFFFFh
  004cce81  6aff                  push      0FFFFFFFFh
  004cce83  56                    push      esi
  004cce84  e88769ffff            call      004C3810h
  004cce89  8b44242c              mov       eax,[esp+2Ch]
  004cce8d  83c410                add       esp,10h
  004cce90  3bc3                  cmp       eax,ebx
  004cce92  7511                  jne       short 004CCEA5h
  004cce94  668b4c247a            mov       cx,[esp+7Ah]
  004cce99  66894e32              mov       [esi+32h],cx
  004cce9d  5f                    pop       edi
  004cce9e  5e                    pop       esi
  004cce9f  5d                    pop       ebp
  004ccea0  5b                    pop       ebx
  004ccea1  83c47c                add       esp,7Ch
  004ccea4  c3                    ret

LAB_004ccea5:
  004ccea5  385e43                cmp       [esi+43h],bl
  004ccea8  0f857a030000          jne       near 004CD228h
  004cceae  8b466c                mov       eax,[esi+6Ch]
  004cceb1  8b561c                mov       edx,[esi+1Ch]
  004cceb4  8b781c                mov       edi,[eax+1Ch]
  004cceb7  8b6824                mov       ebp,[eax+24h]
  004cceba  8b4624                mov       eax,[esi+24h]
  004ccebd  2bfa                  sub       edi,edx
  004ccebf  2be8                  sub       ebp,eax
  004ccec1  c1ff0c                sar       edi,0Ch
  004ccec4  c1fd0c                sar       ebp,0Ch
  004ccec7  8bd7                  mov       edx,edi
  004ccec9  8bc5                  mov       eax,ebp
  004ccecb  0fafd7                imul      edx,edi
  004ccece  0fafc5                imul      eax,ebp
  004cced1  03d0                  add       edx,eax
  004cced3  52                    push      edx
  004cced4  e8378b0700            call      00545A10h
  004cced9  55                    push      ebp
  004cceda  57                    push      edi
  004ccedb  8944241c              mov       [esp+1Ch],eax
  004ccedf  e83e8b0700            call      00545A22h
  004ccee4  668b4e32              mov       cx,[esi+32h]
  004ccee8  66895e12              mov       [esi+12h],bx
  004cceec  2bc1                  sub       eax,ecx
  004cceee  8b0e                  mov       ecx,[esi]
  004ccef0  25ff0f0000            and       eax,0FFFh
  004ccef5  83e1fd                and       ecx,0FFFFFFFDh
  004ccef8  89442418              mov       [esp+18h],eax
  004ccefc  8b442414              mov       eax,[esp+14h]
  004ccf00  3de6000000            cmp       eax,0E6h
  004ccf05  890e                  mov       [esi],ecx
  004ccf07  0f8d51010000          jge       near 004CD05Eh
  004ccf0d  e82ea20700            call      00547140h
  004ccf12  99                    cdq
  004ccf13  b964000000            mov       ecx,64h
  004ccf18  f7f9                  idiv      ecx
  004ccf1a  8bea                  mov       ebp,edx
  004ccf1c  83fd32                cmp       ebp,32h
  004ccf1f  0f8d98000000          jge       near 004CCFBDh
  004ccf25  8b542418              mov       edx,[esp+18h]
  004ccf29  8b7c2420              mov       edi,[esp+20h]
  004ccf2d  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004ccf34  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004ccf3b  25ff0f0000            and       eax,0FFFh
  004ccf40  3bc1                  cmp       eax,ecx
  004ccf42  7f69                  jg        short 004CCFADh
  004ccf44  e8f7a10700            call      00547140h
  004ccf49  99                    cdq
  004ccf4a  8d4c3f01              lea       ecx,[edi+edi+1]
  004ccf4e  f7f9                  idiv      ecx
  004ccf50  8b442414              mov       eax,[esp+14h]
  004ccf54  83c264                add       edx,64h
  004ccf57  3bc2                  cmp       eax,edx
  004ccf59  7d52                  jge       short 004CCFADh
  004ccf5b  83fd1e                cmp       ebp,1Eh
  004ccf5e  7d27                  jge       short 004CCF87h
  004ccf60  8b16                  mov       edx,[esi]
  004ccf62  c646420b              mov       byte [esi+42h],0Bh
  004ccf66  81e2fffffffe          and       edx,0FEFFFFFFh
  004ccf6c  c6464301              mov       byte [esi+43h],1
  004ccf70  83ca02                or        edx,2
  004ccf73  895e5c                mov       [esi+5Ch],ebx
  004ccf76  8916                  mov       [esi],edx
  004ccf78  66c746180300          mov       word [esi+18h],3
  004ccf7e  66895e1a              mov       [esi+1Ah],bx
  004ccf82  e99c000000            jmp       004CD023h

LAB_004ccf87:
  004ccf87  885e42                mov       [esi+42h],bl
  004ccf8a  e8b1a10700            call      00547140h
  004ccf8f  99                    cdq
  004ccf90  b90a000000            mov       ecx,0Ah
  004ccf95  66c746105000          mov       word [esi+10h],50h
  004ccf9b  f7f9                  idiv      ecx
  004ccf9d  8bc7                  mov       eax,edi
  004ccf9f  66895e12              mov       [esi+12h],bx
  004ccfa3  c1f802                sar       eax,2
  004ccfa6  02d0                  add       dl,al
  004ccfa8  885643                mov       [esi+43h],dl
  004ccfab  eb76                  jmp       short 004CD023h

LAB_004ccfad:
  004ccfad  807e4201              cmp       byte [esi+42h],1
  004ccfb1  7470                  je        short 004CD023h
  004ccfb3  c6464201              mov       byte [esi+42h],1
  004ccfb7  c6464301              mov       byte [esi+43h],1
  004ccfbb  eb66                  jmp       short 004CD023h

LAB_004ccfbd:
  004ccfbd  83fd3c                cmp       ebp,3Ch
  004ccfc0  7c2b                  jl        short 004CCFEDh
  004ccfc2  83fd46                cmp       ebp,46h
  004ccfc5  7d26                  jge       short 004CCFEDh
  004ccfc7  8b542410              mov       edx,[esp+10h]
  004ccfcb  8b442414              mov       eax,[esp+14h]
  004ccfcf  83c264                add       edx,64h
  004ccfd2  3bc2                  cmp       eax,edx
  004ccfd4  7e17                  jle       short 004CCFEDh
  004ccfd6  c6464206              mov       byte [esi+42h],6
  004ccfda  c6464301              mov       byte [esi+43h],1
  004ccfde  e85da10700            call      00547140h
  004ccfe3  66894610              mov       [esi+10h],ax
  004ccfe7  66895e12              mov       [esi+12h],bx
  004ccfeb  eb32                  jmp       short 004CD01Fh

LAB_004ccfed:
  004ccfed  c6464203              mov       byte [esi+42h],3
  004ccff1  66895e12              mov       [esi+12h],bx
  004ccff5  e846a10700            call      00547140h
  004ccffa  99                    cdq
  004ccffb  b905000000            mov       ecx,5
  004cd000  f7f9                  idiv      ecx
  004cd002  80c20a                add       dl,0Ah
  004cd005  885643                mov       [esi+43h],dl
  004cd008  e833a10700            call      00547140h
  004cd00d  99                    cdq
  004cd00e  33c2                  xor       eax,edx
  004cd010  2bc2                  sub       eax,edx
  004cd012  25ff0f0000            and       eax,0FFFh
  004cd017  33c2                  xor       eax,edx
  004cd019  2bc2                  sub       eax,edx
  004cd01b  66894610              mov       [esi+10h],ax

LAB_004cd01f:
  004cd01f  8b7c2420              mov       edi,[esp+20h]

LAB_004cd023:
  004cd023  837c241464            cmp       dword [esp+14h],64h
  004cd028  7d38                  jge       short 004CD062h
  004cd02a  c6464203              mov       byte [esi+42h],3
  004cd02e  66895e12              mov       [esi+12h],bx
  004cd032  e809a10700            call      00547140h
  004cd037  99                    cdq
  004cd038  b905000000            mov       ecx,5
  004cd03d  f7f9                  idiv      ecx
  004cd03f  80c20a                add       dl,0Ah
  004cd042  885643                mov       [esi+43h],dl
  004cd045  e8f6a00700            call      00547140h
  004cd04a  99                    cdq
  004cd04b  33c2                  xor       eax,edx
  004cd04d  2bc2                  sub       eax,edx
  004cd04f  25ff0f0000            and       eax,0FFFh
  004cd054  33c2                  xor       eax,edx
  004cd056  2bc2                  sub       eax,edx
  004cd058  66894610              mov       [esi+10h],ax
  004cd05c  eb04                  jmp       short 004CD062h

LAB_004cd05e:
  004cd05e  8b7c2420              mov       edi,[esp+20h]

LAB_004cd062:
  004cd062  8b442414              mov       eax,[esp+14h]
  004cd066  3d58020000            cmp       eax,258h
  004cd06b  0f8dba000000          jge       near 004CD12Bh
  004cd071  3de6000000            cmp       eax,0E6h
  004cd076  0f8caf000000          jl        near 004CD12Bh
  004cd07c  e8bfa00700            call      00547140h
  004cd081  99                    cdq
  004cd082  b964000000            mov       ecx,64h
  004cd087  f7f9                  idiv      ecx
  004cd089  83fa14                cmp       edx,14h
  004cd08c  7d0d                  jge       short 004CD09Bh
  004cd08e  c6464201              mov       byte [esi+42h],1
  004cd092  c6464301              mov       byte [esi+43h],1
  004cd096  e990000000            jmp       004CD12Bh

LAB_004cd09b:
  004cd09b  83fa28                cmp       edx,28h
  004cd09e  7d1f                  jge       short 004CD0BFh
  004cd0a0  c6464202              mov       byte [esi+42h],2
  004cd0a4  e897a00700            call      00547140h
  004cd0a9  99                    cdq
  004cd0aa  b90a000000            mov       ecx,0Ah
  004cd0af  f7f9                  idiv      ecx
  004cd0b1  8bc7                  mov       eax,edi
  004cd0b3  d1f8                  sar       eax,1
  004cd0b5  02d0                  add       dl,al
  004cd0b7  80c214                add       dl,14h
  004cd0ba  885643                mov       [esi+43h],dl
  004cd0bd  eb66                  jmp       short 004CD125h

LAB_004cd0bf:
  004cd0bf  83fa3c                cmp       edx,3Ch
  004cd0c2  7d17                  jge       short 004CD0DBh
  004cd0c4  c6464206              mov       byte [esi+42h],6
  004cd0c8  c6464301              mov       byte [esi+43h],1
  004cd0cc  e86fa00700            call      00547140h
  004cd0d1  66894610              mov       [esi+10h],ax
  004cd0d5  66895e12              mov       [esi+12h],bx
  004cd0d9  eb50                  jmp       short 004CD12Bh

LAB_004cd0db:
  004cd0db  83fa50                cmp       edx,50h
  004cd0de  7d3a                  jge       short 004CD11Ah
  004cd0e0  c6464203              mov       byte [esi+42h],3
  004cd0e4  66895e12              mov       [esi+12h],bx
  004cd0e8  e853a00700            call      00547140h
  004cd0ed  99                    cdq
  004cd0ee  b90a000000            mov       ecx,0Ah
  004cd0f3  f7f9                  idiv      ecx
  004cd0f5  8bc7                  mov       eax,edi
  004cd0f7  d1f8                  sar       eax,1
  004cd0f9  02d0                  add       dl,al
  004cd0fb  80c214                add       dl,14h
  004cd0fe  885643                mov       [esi+43h],dl
  004cd101  e83aa00700            call      00547140h
  004cd106  99                    cdq
  004cd107  33c2                  xor       eax,edx
  004cd109  2bc2                  sub       eax,edx
  004cd10b  25ff0f0000            and       eax,0FFFh
  004cd110  33c2                  xor       eax,edx
  004cd112  2bc2                  sub       eax,edx
  004cd114  66894610              mov       [esi+10h],ax
  004cd118  eb11                  jmp       short 004CD12Bh

LAB_004cd11a:
  004cd11a  885e42                mov       [esi+42h],bl
  004cd11d  c6464301              mov       byte [esi+43h],1
  004cd121  66895e12              mov       [esi+12h],bx

LAB_004cd125:
  004cd125  66c746105000          mov       word [esi+10h],50h

LAB_004cd12b:
  004cd12b  817c241458020000      cmp       dword [esp+14h],258h
  004cd133  0f8cef000000          jl        near 004CD228h
  004cd139  e802a00700            call      00547140h
  004cd13e  bd64000000            mov       ebp,64h
  004cd143  99                    cdq
  004cd144  8bcd                  mov       ecx,ebp
  004cd146  f7f9                  idiv      ecx
  004cd148  83fa1e                cmp       edx,1Eh
  004cd14b  7d42                  jge       short 004CD18Fh
  004cd14d  8b442410              mov       eax,[esp+10h]
  004cd151  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004cd155  3bd1                  cmp       edx,ecx
  004cd157  0f8c91000000          jl        near 004CD1EEh
  004cd15d  885e42                mov       [esi+42h],bl
  004cd160  e8db9f0700            call      00547140h
  004cd165  99                    cdq
  004cd166  b90a000000            mov       ecx,0Ah
  004cd16b  66895e12              mov       [esi+12h],bx
  004cd16f  f7f9                  idiv      ecx
  004cd171  8b4c2410              mov       ecx,[esp+10h]
  004cd175  8bc7                  mov       eax,edi
  004cd177  c1f802                sar       eax,2
  004cd17a  02d0                  add       dl,al
  004cd17c  885643                mov       [esi+43h],dl
  004cd17f  8d540950              lea       edx,[ecx+ecx+50h]
  004cd183  66895610              mov       [esi+10h],dx
  004cd187  5f                    pop       edi
  004cd188  5e                    pop       esi
  004cd189  5d                    pop       ebp
  004cd18a  5b                    pop       ebx
  004cd18b  83c47c                add       esp,7Ch
  004cd18e  c3                    ret

LAB_004cd18f:
  004cd18f  83fa28                cmp       edx,28h
  004cd192  7d40                  jge       short 004CD1D4h
  004cd194  c6464203              mov       byte [esi+42h],3
  004cd198  66895e12              mov       [esi+12h],bx
  004cd19c  e89f9f0700            call      00547140h
  004cd1a1  99                    cdq
  004cd1a2  b90a000000            mov       ecx,0Ah
  004cd1a7  f7f9                  idiv      ecx
  004cd1a9  8bc7                  mov       eax,edi
  004cd1ab  d1f8                  sar       eax,1
  004cd1ad  02d0                  add       dl,al
  004cd1af  80c214                add       dl,14h
  004cd1b2  885643                mov       [esi+43h],dl
  004cd1b5  e8869f0700            call      00547140h
  004cd1ba  99                    cdq
  004cd1bb  33c2                  xor       eax,edx
  004cd1bd  2bc2                  sub       eax,edx
  004cd1bf  25ff0f0000            and       eax,0FFFh
  004cd1c4  33c2                  xor       eax,edx
  004cd1c6  2bc2                  sub       eax,edx
  004cd1c8  66894610              mov       [esi+10h],ax
  004cd1cc  5f                    pop       edi
  004cd1cd  5e                    pop       esi
  004cd1ce  5d                    pop       ebp
  004cd1cf  5b                    pop       ebx
  004cd1d0  83c47c                add       esp,7Ch
  004cd1d3  c3                    ret

LAB_004cd1d4:
  004cd1d4  83fa3c                cmp       edx,3Ch
  004cd1d7  7d10                  jge       short 004CD1E9h
  004cd1d9  c6464201              mov       byte [esi+42h],1
  004cd1dd  c6464301              mov       byte [esi+43h],1
  004cd1e1  5f                    pop       edi
  004cd1e2  5e                    pop       esi
  004cd1e3  5d                    pop       ebp
  004cd1e4  5b                    pop       ebx
  004cd1e5  83c47c                add       esp,7Ch
  004cd1e8  c3                    ret

LAB_004cd1e9:
  004cd1e9  83fa50                cmp       edx,50h
  004cd1ec  7d29                  jge       short 004CD217h

LAB_004cd1ee:
  004cd1ee  c6464202              mov       byte [esi+42h],2
  004cd1f2  e8499f0700            call      00547140h
  004cd1f7  99                    cdq
  004cd1f8  b90a000000            mov       ecx,0Ah
  004cd1fd  66896e10              mov       [esi+10h],bp
  004cd201  f7f9                  idiv      ecx
  004cd203  8bc7                  mov       eax,edi
  004cd205  d1f8                  sar       eax,1
  004cd207  02d0                  add       dl,al
  004cd209  80c214                add       dl,14h
  004cd20c  885643                mov       [esi+43h],dl
  004cd20f  5f                    pop       edi
  004cd210  5e                    pop       esi
  004cd211  5d                    pop       ebp
  004cd212  5b                    pop       ebx
  004cd213  83c47c                add       esp,7Ch
  004cd216  c3                    ret

LAB_004cd217:
  004cd217  c6464206              mov       byte [esi+42h],6
  004cd21b  c6464301              mov       byte [esi+43h],1
  004cd21f  e81c9f0700            call      00547140h
  004cd224  66894610              mov       [esi+10h],ax

LAB_004cd228:
  004cd228  5f                    pop       edi
  004cd229  5e                    pop       esi
  004cd22a  5d                    pop       ebp
  004cd22b  5b                    pop       ebx
  004cd22c  83c47c                add       esp,7Ch
  004cd22f  c3                    ret
  004cd230  9b                    wait
  004cd231  c84c00db              enter     4Ch,0DBh
  004cd235  c84c0062              enter     4Ch,62h
  004cd239  c9                    leave
  004cd23a  4c                    dec       esp
  004cd23b  00f9                  add       cl,bh
  004cd23d  c9                    leave
  004cd23e  4c                    dec       esp
  004cd23f  007bcc                add       [ebx-34h],bh
  004cd242  4c                    dec       esp
  004cd243  007bcc                add       [ebx-34h],bh
  004cd246  4c                    dec       esp
  004cd247  00c9                  add       cl,cl
  004cd249  ca4c00                retf      4Ch
  004cd24c  7fcb                  jg        short 004CD219h
  004cd24e  4c                    dec       esp
  004cd24f  007bcc                add       [ebx-34h],bh
  004cd252  4c                    dec       esp
  004cd253  007bcc                add       [ebx-34h],bh
  004cd256  4c                    dec       esp
  004cd257  007bcc                add       [ebx-34h],bh
  004cd25a  4c                    dec       esp
  004cd25b  00de                  add       dh,bl
  004cd25d  cb                    retf
  004cd25e  4c                    dec       esp
  004cd25f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
