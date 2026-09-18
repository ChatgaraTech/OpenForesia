Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004d8f70  section .text  offset fichier 0xd8370  3616 octets

  004d8f70  81ec84000000          sub       esp,84h
  004d8f76  53                    push      ebx
  004d8f77  55                    push      ebp
  004d8f78  56                    push      esi
  004d8f79  8bb42494000000        mov       esi,[esp+94h]
  004d8f80  b864000000            mov       eax,64h
  004d8f85  33db                  xor       ebx,ebx
  004d8f87  6689442432            mov       [esp+32h],ax
  004d8f8c  6689442436            mov       [esp+36h],ax
  004d8f91  8a4641                mov       al,[esi+41h]
  004d8f94  57                    push      edi
  004d8f95  3ac3                  cmp       al,bl
  004d8f97  66895c2430            mov       [esp+30h],bx
  004d8f9c  66c74424321e00        mov       word [esp+32h],1Eh
  004d8fa3  66c74424343200        mov       word [esp+34h],32h
  004d8faa  66895c2438            mov       [esp+38h],bx
  004d8faf  7510                  jne       short 004D8FC1h
  004d8fb1  53                    push      ebx
  004d8fb2  6a01                  push      1
  004d8fb4  56                    push      esi
  004d8fb5  c6464101              mov       byte [esi+41h],1
  004d8fb9  e862caffff            call      004D5A20h
  004d8fbe  83c40c                add       esp,0Ch

LAB_004d8fc1:
  004d8fc1  8b461c                mov       eax,[esi+1Ch]
  004d8fc4  8b4e20                mov       ecx,[esi+20h]
  004d8fc7  8b5624                mov       edx,[esi+24h]
  004d8fca  8d6e1c                lea       ebp,[esi+1Ch]
  004d8fcd  89442424              mov       [esp+24h],eax
  004d8fd1  668b4632              mov       ax,[esi+32h]
  004d8fd5  894c2428              mov       [esp+28h],ecx
  004d8fd9  8b4e64                mov       ecx,[esi+64h]
  004d8fdc  8954242c              mov       [esp+2Ch],edx
  004d8fe0  8b5660                mov       edx,[esi+60h]
  004d8fe3  6689842482000000      mov       [esp+82h],ax
  004d8feb  8b467c                mov       eax,[esi+7Ch]
  004d8fee  894c2444              mov       [esp+44h],ecx
  004d8ff2  668b8e90000000        mov       cx,[esi+90h]
  004d8ff9  89542440              mov       [esp+40h],edx
  004d8ffd  8b9680000000          mov       edx,[esi+80h]
  004d9003  8944245c              mov       [esp+5Ch],eax
  004d9007  8b8684000000          mov       eax,[esi+84h]
  004d900d  66894c2448            mov       [esp+48h],cx
  004d9012  8b8e88000000          mov       ecx,[esi+88h]
  004d9018  8b7e6c                mov       edi,[esi+6Ch]
  004d901b  8954244c              mov       [esp+4Ch],edx
  004d901f  8b968c000000          mov       edx,[esi+8Ch]
  004d9025  8944243c              mov       [esp+3Ch],eax
  004d9029  894c2458              mov       [esp+58h],ecx
  004d902d  8d442460              lea       eax,[esp+60h]
  004d9031  89542450              mov       [esp+50h],edx
  004d9035  8d4c2468              lea       ecx,[esp+68h]
  004d9039  50                    push      eax
  004d903a  8d542468              lea       edx,[esp+68h]
  004d903e  51                    push      ecx
  004d903f  52                    push      edx
  004d9040  56                    push      esi
  004d9041  e83af90500            call      00538980h
  004d9046  83c410                add       esp,10h
  004d9049  8d442454              lea       eax,[esp+54h]
  004d904d  8d4c241c              lea       ecx,[esp+1Ch]
  004d9051  8d542418              lea       edx,[esp+18h]
  004d9055  50                    push      eax
  004d9056  51                    push      ecx
  004d9057  8d44241c              lea       eax,[esp+1Ch]
  004d905b  52                    push      edx
  004d905c  8d4c241c              lea       ecx,[esp+1Ch]
  004d9060  50                    push      eax
  004d9061  51                    push      ecx
  004d9062  56                    push      esi
  004d9063  e8b8ce0500            call      00535F20h
  004d9068  8b4c2428              mov       ecx,[esp+28h]
  004d906c  8b54246c              mov       edx,[esp+6Ch]
  004d9070  83c418                add       esp,18h
  004d9073  b840000000            mov       eax,40h
  004d9078  2bc1                  sub       eax,ecx
  004d907a  52                    push      edx
  004d907b  56                    push      esi
  004d907c  89442428              mov       [esp+28h],eax
  004d9080  e85bcdffff            call      004D5DE0h
  004d9085  8b4500                mov       eax,[ebp]
  004d9088  8b571c                mov       edx,[edi+1Ch]
  004d908b  8b4e20                mov       ecx,[esi+20h]
  004d908e  2bc2                  sub       eax,edx
  004d9090  8b5720                mov       edx,[edi+20h]
  004d9093  83c408                add       esp,8
  004d9096  2bca                  sub       ecx,edx
  004d9098  8b5624                mov       edx,[esi+24h]
  004d909b  2b5724                sub       edx,[edi+24h]
  004d909e  c1f80c                sar       eax,0Ch
  004d90a1  8bf8                  mov       edi,eax
  004d90a3  c1f90c                sar       ecx,0Ch
  004d90a6  0faff8                imul      edi,eax
  004d90a9  8bc1                  mov       eax,ecx
  004d90ab  c1fa0c                sar       edx,0Ch
  004d90ae  0fafc1                imul      eax,ecx
  004d90b1  8bca                  mov       ecx,edx
  004d90b3  03f8                  add       edi,eax
  004d90b5  0fafca                imul      ecx,edx
  004d90b8  03f9                  add       edi,ecx
  004d90ba  57                    push      edi
  004d90bb  e850c90600            call      00545A10h
  004d90c0  8b4c241c              mov       ecx,[esp+1Ch]
  004d90c4  89442414              mov       [esp+14h],eax
  004d90c8  3bcb                  cmp       ecx,ebx
  004d90ca  0f84ce060000          je        near 004D979Eh
  004d90d0  8b16                  mov       edx,[esi]
  004d90d2  33c0                  xor       eax,eax
  004d90d4  8a4604                mov       al,[esi+4]
  004d90d7  81e2fffffffd          and       edx,0FDFFFFFFh
  004d90dd  0fbf7e18              movsx     edi,word [esi+18h]
  004d90e1  8916                  mov       [esi],edx
  004d90e3  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004d90ea  f7c200000001          test      edx,1000000h
  004d90f0  8b3cb8                mov       edi,[eax+edi*4]
  004d90f3  741b                  je        short 004D9110h
  004d90f5  66ff461a              inc       word [esi+1Ah]
  004d90f9  668b461a              mov       ax,[esi+1Ah]
  004d90fd  33d2                  xor       edx,edx
  004d90ff  668b5716              mov       dx,[edi+16h]
  004d9103  0fbfc0                movsx     eax,ax
  004d9106  3bc2                  cmp       eax,edx
  004d9108  7c2a                  jl        short 004D9134h
  004d910a  66895e1a              mov       [esi+1Ah],bx
  004d910e  eb24                  jmp       short 004D9134h

LAB_004d9110:
  004d9110  66ff461a              inc       word [esi+1Ah]
  004d9114  668b461a              mov       ax,[esi+1Ah]
  004d9118  33db                  xor       ebx,ebx
  004d911a  668b5f16              mov       bx,[edi+16h]
  004d911e  0fbff8                movsx     edi,ax
  004d9121  3bfb                  cmp       edi,ebx
  004d9123  7c0d                  jl        short 004D9132h
  004d9125  48                    dec       eax
  004d9126  81ca00000002          or        edx,2000000h
  004d912c  6689461a              mov       [esi+1Ah],ax
  004d9130  8916                  mov       [esi],edx

LAB_004d9132:
  004d9132  33db                  xor       ebx,ebx

LAB_004d9134:
  004d9134  8a4642                mov       al,[esi+42h]
  004d9137  3c05                  cmp       al,5
  004d9139  8b06                  mov       eax,[esi]
  004d913b  7504                  jne       short 004D9141h
  004d913d  0c10                  or        al,10h
  004d913f  eb02                  jmp       short 004D9143h

LAB_004d9141:
  004d9141  24ef                  and       al,0EFh

LAB_004d9143:
  004d9143  8906                  mov       [esi],eax
  004d9145  8a06                  mov       al,[esi]
  004d9147  a802                  test      al,2
  004d9149  747d                  je        short 004D91C8h
  004d914b  66837e1a09            cmp       word [esi+1Ah],9
  004d9150  7558                  jne       short 004D91AAh
  004d9152  8d4c2430              lea       ecx,[esp+30h]
  004d9156  53                    push      ebx
  004d9157  51                    push      ecx
  004d9158  55                    push      ebp
  004d9159  56                    push      esi
  004d915a  e8d10a0400            call      00519C30h
  004d915f  8bf8                  mov       edi,eax
  004d9161  a1b8e05d00            mov       eax,[5DE0B8h]
  004d9166  83c410                add       esp,10h
  004d9169  3bc3                  cmp       eax,ebx
  004d916b  7439                  je        short 004D91A6h
  004d916d  66837e1a09            cmp       word [esi+1Ah],9
  004d9172  7532                  jne       short 004D91A6h
  004d9174  8b0f                  mov       ecx,[edi]
  004d9176  57                    push      edi
  004d9177  83c940                or        ecx,40h
  004d917a  56                    push      esi
  004d917b  66c787960000001000    mov       word [edi+96h],10h
  004d9184  890f                  mov       [edi],ecx
  004d9186  e8f5090600            call      00539B80h
  004d918b  83c408                add       esp,8
  004d918e  83c71c                add       edi,1Ch
  004d9191  57                    push      edi
  004d9192  6800180000            push      1800h
  004d9197  6800030000            push      300h
  004d919c  6a03                  push      3
  004d919e  e87d490600            call      0053DB20h
  004d91a3  83c410                add       esp,10h

LAB_004d91a6:
  004d91a6  8b4c241c              mov       ecx,[esp+1Ch]

LAB_004d91aa:
  004d91aa  8b16                  mov       edx,[esi]
  004d91ac  f7c200000002          test      edx,2000000h
  004d91b2  7414                  je        short 004D91C8h
  004d91b4  f6c201                test      dl,1
  004d91b7  750a                  jne       short 004D91C3h
  004d91b9  83e2fd                and       edx,0FFFFFFFDh
  004d91bc  885e43                mov       [esi+43h],bl
  004d91bf  8916                  mov       [esi],edx
  004d91c1  eb05                  jmp       short 004D91C8h

LAB_004d91c3:
  004d91c3  83e2fd                and       edx,0FFFFFFFDh
  004d91c6  8916                  mov       [esi],edx

LAB_004d91c8:
  004d91c8  33c0                  xor       eax,eax
  004d91ca  8a4642                mov       al,[esi+42h]
  004d91cd  83f80c                cmp       eax,0Ch
  004d91d0  0f87c8050000          ja        near 004D979Eh
  004d91d6  ff2485509d4d00        jmp       dword [eax*4+4D9D50h]
  004d91dd  66395e12              cmp       [esi+12h],bx
  004d91e1  751a                  jne       short 004D91FDh
  004d91e3  8b06                  mov       eax,[esi]
  004d91e5  66895e18              mov       [esi+18h],bx
  004d91e9  0d00000001            or        eax,1000000h
  004d91ee  66895e1a              mov       [esi+1Ah],bx
  004d91f2  8906                  mov       [esi],eax
  004d91f4  66c746120100          mov       word [esi+12h],1
  004d91fa  895e5c                mov       [esi+5Ch],ebx

LAB_004d91fd:
  004d91fd  0fbf5610              movsx     edx,word [esi+10h]
  004d9201  39542414              cmp       [esp+14h],edx
  004d9205  0f8c90050000          jl        near 004D979Bh
  004d920b  8a4643                mov       al,[esi+43h]
  004d920e  3ac3                  cmp       al,bl
  004d9210  0f84ce000000          je        near 004D92E4h
  004d9216  fec8                  dec       al
  004d9218  895e5c                mov       [esi+5Ch],ebx
  004d921b  884643                mov       [esi+43h],al
  004d921e  e97b050000            jmp       004D979Eh
  004d9223  66395e12              cmp       [esi+12h],bx
  004d9227  751b                  jne       short 004D9244h
  004d9229  8b0e                  mov       ecx,[esi]
  004d922b  b801000000            mov       eax,1
  004d9230  81c900000001          or        ecx,1000000h
  004d9236  66894618              mov       [esi+18h],ax
  004d923a  66895e1a              mov       [esi+1Ah],bx
  004d923e  890e                  mov       [esi],ecx
  004d9240  66894612              mov       [esi+12h],ax

LAB_004d9244:
  004d9244  8b442418              mov       eax,[esp+18h]
  004d9248  8d8880000000          lea       ecx,[eax+80h]
  004d924e  81e1ff0f0000          and       ecx,0FFFh
  004d9254  81f990010000          cmp       ecx,190h
  004d925a  0f8e81000000          jle       near 004D92E1h
  004d9260  25ff0f0000            and       eax,0FFFh
  004d9265  3d00080000            cmp       eax,800h
  004d926a  7d16                  jge       short 004D9282h
  004d926c  8b542410              mov       edx,[esp+10h]
  004d9270  895e5c                mov       [esi+5Ch],ebx
  004d9273  8d8280000000          lea       eax,[edx+80h]
  004d9279  66014632              add       [esi+32h],ax
  004d927d  e91c050000            jmp       004D979Eh

LAB_004d9282:
  004d9282  8b4c2410              mov       ecx,[esp+10h]
  004d9286  b880ffffff            mov       eax,0FFFFFF80h
  004d928b  2bc1                  sub       eax,ecx
  004d928d  895e5c                mov       [esi+5Ch],ebx
  004d9290  66014632              add       [esi+32h],ax
  004d9294  e905050000            jmp       004D979Eh
  004d9299  66395e12              cmp       [esi+12h],bx
  004d929d  8b4c2410              mov       ecx,[esp+10h]
  004d92a1  b801000000            mov       eax,1
  004d92a6  7525                  jne       short 004D92CDh
  004d92a8  8b16                  mov       edx,[esi]
  004d92aa  66894618              mov       [esi+18h],ax
  004d92ae  81ca00000001          or        edx,1000000h
  004d92b4  66895e1a              mov       [esi+1Ah],bx
  004d92b8  8916                  mov       [esi],edx
  004d92ba  8d14c9                lea       edx,[ecx+ecx*8]
  004d92bd  c1e204                shl       edx,4
  004d92c0  81c200500000          add       edx,5000h
  004d92c6  66894612              mov       [esi+12h],ax
  004d92ca  89565c                mov       [esi+5Ch],edx

LAB_004d92cd:
  004d92cd  66394612              cmp       [esi+12h],ax
  004d92d1  0f85c7040000          jne       near 004D979Eh
  004d92d7  0fbf4610              movsx     eax,word [esi+10h]
  004d92db  39442414              cmp       [esp+14h],eax
  004d92df  7d0b                  jge       short 004D92ECh

LAB_004d92e1:
  004d92e1  885e43                mov       [esi+43h],bl

LAB_004d92e4:
  004d92e4  895e5c                mov       [esi+5Ch],ebx
  004d92e7  e9b2040000            jmp       004D979Eh

LAB_004d92ec:
  004d92ec  8a4643                mov       al,[esi+43h]
  004d92ef  3ac3                  cmp       al,bl
  004d92f1  7405                  je        short 004D92F8h
  004d92f3  fec8                  dec       al
  004d92f5  884643                mov       [esi+43h],al

LAB_004d92f8:
  004d92f8  8b442418              mov       eax,[esp+18h]
  004d92fc  8d9000010000          lea       edx,[eax+100h]
  004d9302  81e2ff0f0000          and       edx,0FFFh
  004d9308  81fa00020000          cmp       edx,200h
  004d930e  0f8e8a040000          jle       near 004D979Eh
  004d9314  25ff0f0000            and       eax,0FFFh
  004d9319  3d00080000            cmp       eax,800h
  004d931e  7d0f                  jge       short 004D932Fh
  004d9320  8d8180000000          lea       eax,[ecx+80h]
  004d9326  66014632              add       [esi+32h],ax
  004d932a  e96f040000            jmp       004D979Eh

LAB_004d932f:
  004d932f  b880ffffff            mov       eax,0FFFFFF80h
  004d9334  2bc1                  sub       eax,ecx
  004d9336  66014632              add       [esi+32h],ax
  004d933a  e95f040000            jmp       004D979Eh
  004d933f  66837e1201            cmp       word [esi+12h],1
  004d9344  0f8599000000          jne       near 004D93E3h
  004d934a  8a4643                mov       al,[esi+43h]
  004d934d  3ac3                  cmp       al,bl
  004d934f  7405                  je        short 004D9356h
  004d9351  fec8                  dec       al
  004d9353  884643                mov       [esi+43h],al

LAB_004d9356:
  004d9356  8b442410              mov       eax,[esp+10h]
  004d935a  8d04c0                lea       eax,[eax+eax*8]
  004d935d  c1e004                shl       eax,4
  004d9360  0500500000            add       eax,5000h
  004d9365  0fafc8                imul      ecx,eax
  004d9368  c1f90c                sar       ecx,0Ch
  004d936b  8bf9                  mov       edi,ecx
  004d936d  89465c                mov       [esi+5Ch],eax
  004d9370  0fbf4e10              movsx     ecx,word [esi+10h]
  004d9374  51                    push      ecx
  004d9375  e8a2c60600            call      00545A1Ch
  004d937a  0fafc7                imul      eax,edi
  004d937d  0fbf5610              movsx     edx,word [esi+10h]
  004d9381  8b4d00                mov       ecx,[ebp]
  004d9384  52                    push      edx
  004d9385  c1f80c                sar       eax,0Ch
  004d9388  03c8                  add       ecx,eax
  004d938a  894d00                mov       [ebp],ecx
  004d938d  e884c60600            call      00545A16h
  004d9392  0fafc7                imul      eax,edi
  004d9395  8b6e24                mov       ebp,[esi+24h]
  004d9398  895e5c                mov       [esi+5Ch],ebx
  004d939b  c1f80c                sar       eax,0Ch
  004d939e  03e8                  add       ebp,eax
  004d93a0  8b442418              mov       eax,[esp+18h]
  004d93a4  896e24                mov       [esi+24h],ebp
  004d93a7  8d8800010000          lea       ecx,[eax+100h]
  004d93ad  81e1ff0f0000          and       ecx,0FFFh
  004d93b3  81f900020000          cmp       ecx,200h
  004d93b9  7e28                  jle       short 004D93E3h
  004d93bb  25ff0f0000            and       eax,0FFFh
  004d93c0  3d00080000            cmp       eax,800h
  004d93c5  7d0a                  jge       short 004D93D1h
  004d93c7  8b542410              mov       edx,[esp+10h]
  004d93cb  8d441232              lea       eax,[edx+edx+32h]
  004d93cf  eb0e                  jmp       short 004D93DFh

LAB_004d93d1:
  004d93d1  8b442410              mov       eax,[esp+10h]
  004d93d5  8d0c00                lea       ecx,[eax+eax]
  004d93d8  b8ceffffff            mov       eax,0FFFFFFCEh
  004d93dd  2bc1                  sub       eax,ecx

LAB_004d93df:
  004d93df  66014632              add       [esi+32h],ax

LAB_004d93e3:
  004d93e3  66395e12              cmp       [esi+12h],bx
  004d93e7  0f85b1030000          jne       near 004D979Eh
  004d93ed  8b0e                  mov       ecx,[esi]
  004d93ef  b801000000            mov       eax,1
  004d93f4  81c900000001          or        ecx,1000000h
  004d93fa  66894618              mov       [esi+18h],ax
  004d93fe  66895e1a              mov       [esi+1Ah],bx
  004d9402  890e                  mov       [esi],ecx
  004d9404  66894612              mov       [esi+12h],ax
  004d9408  e991030000            jmp       004D979Eh
  004d940d  66395e12              cmp       [esi+12h],bx
  004d9411  757d                  jne       short 004D9490h
  004d9413  8b06                  mov       eax,[esi]
  004d9415  a801                  test      al,1
  004d9417  7405                  je        short 004D941Eh
  004d9419  885e43                mov       [esi+43h],bl
  004d941c  eb72                  jmp       short 004D9490h

LAB_004d941e:
  004d941e  8b542410              mov       edx,[esp+10h]
  004d9422  25fffffffe            and       eax,0FEFFFFFFh
  004d9427  8906                  mov       [esi],eax
  004d9429  bd01000000            mov       ebp,1
  004d942e  8d8200010000          lea       eax,[edx+100h]
  004d9434  66896e18              mov       [esi+18h],bp
  004d9438  c1e006                shl       eax,6
  004d943b  0fafc8                imul      ecx,eax
  004d943e  89465c                mov       [esi+5Ch],eax
  004d9441  66895e1a              mov       [esi+1Ah],bx
  004d9445  0fbf4610              movsx     eax,word [esi+10h]
  004d9449  c1f90c                sar       ecx,0Ch
  004d944c  50                    push      eax
  004d944d  8bf9                  mov       edi,ecx
  004d944f  e8c8c50600            call      00545A1Ch
  004d9454  0fafc7                imul      eax,edi
  004d9457  8b4e50                mov       ecx,[esi+50h]
  004d945a  c1f80c                sar       eax,0Ch
  004d945d  03c8                  add       ecx,eax
  004d945f  894e50                mov       [esi+50h],ecx
  004d9462  0fbf4e10              movsx     ecx,word [esi+10h]
  004d9466  51                    push      ecx
  004d9467  e8aac50600            call      00545A16h
  004d946c  0fafc7                imul      eax,edi
  004d946f  8b5658                mov       edx,[esi+58h]
  004d9472  8b4e54                mov       ecx,[esi+54h]
  004d9475  c1f80c                sar       eax,0Ch
  004d9478  03d0                  add       edx,eax
  004d947a  8b06                  mov       eax,[esi]
  004d947c  81c100e00100          add       ecx,1E000h
  004d9482  0bc5                  or        eax,ebp
  004d9484  895658                mov       [esi+58h],edx
  004d9487  894e54                mov       [esi+54h],ecx
  004d948a  8906                  mov       [esi],eax
  004d948c  66896e12              mov       [esi+12h],bp

LAB_004d9490:
  004d9490  f60601                test      byte [esi],1
  004d9493  0f8505030000          jne       near 004D979Eh
  004d9499  e9fd020000            jmp       004D979Bh
  004d949e  668b4e12              mov       cx,[esi+12h]
  004d94a2  ba01000000            mov       edx,1
  004d94a7  663bca                cmp       cx,dx
  004d94aa  751b                  jne       short 004D94C7h
  004d94ac  0fbf4610              movsx     eax,word [esi+10h]
  004d94b0  39442414              cmp       [esp+14h],eax
  004d94b4  7d05                  jge       short 004D94BBh
  004d94b6  885e43                mov       [esi+43h],bl
  004d94b9  eb0c                  jmp       short 004D94C7h

LAB_004d94bb:
  004d94bb  8a4643                mov       al,[esi+43h]
  004d94be  3ac3                  cmp       al,bl
  004d94c0  7405                  je        short 004D94C7h
  004d94c2  fec8                  dec       al
  004d94c4  884643                mov       [esi+43h],al

LAB_004d94c7:
  004d94c7  663bcb                cmp       cx,bx
  004d94ca  0f85ce020000          jne       near 004D979Eh
  004d94d0  8b442410              mov       eax,[esp+10h]
  004d94d4  8b3e                  mov       edi,[esi]
  004d94d6  81cf00000001          or        edi,1000000h
  004d94dc  66895618              mov       [esi+18h],dx
  004d94e0  8d0cc0                lea       ecx,[eax+eax*8]
  004d94e3  66895e1a              mov       [esi+1Ah],bx
  004d94e7  c1e104                shl       ecx,4
  004d94ea  81c100500000          add       ecx,5000h
  004d94f0  893e                  mov       [esi],edi
  004d94f2  66895612              mov       [esi+12h],dx
  004d94f6  894e5c                mov       [esi+5Ch],ecx
  004d94f9  e9a0020000            jmp       004D979Eh
  004d94fe  66395e12              cmp       [esi+12h],bx
  004d9502  0f85dc000000          jne       near 004D95E4h
  004d9508  33ff                  xor       edi,edi
  004d950a  b800f45f00            mov       eax,5FF400h

LAB_004d950f:
  004d950f  39706c                cmp       [eax+6Ch],esi
  004d9512  750c                  jne       short 004D9520h
  004d9514  f70000000080          test      dword [eax],80000000h
  004d951a  0f857b020000          jne       near 004D979Bh

LAB_004d9520:
  004d9520  059c000000            add       eax,9Ch
  004d9525  47                    inc       edi
  004d9526  3d80076000            cmp       eax,600780h
  004d952b  7ce2                  jl        short 004D950Fh
  004d952d  8b06                  mov       eax,[esi]
  004d952f  a904080000            test      eax,804h
  004d9534  740f                  je        short 004D9545h
  004d9536  25fbf7ffff            and       eax,0FFFFF7FBh
  004d953b  885e43                mov       [esi+43h],bl
  004d953e  8906                  mov       [esi],eax
  004d9540  e959020000            jmp       004D979Eh

LAB_004d9545:
  004d9545  6aff                  push      0FFFFFFFFh
  004d9547  53                    push      ebx
  004d9548  68bd000000            push      0BDh
  004d954d  56                    push      esi
  004d954e  e88d15faff            call      0047AAE0h
  004d9553  83c410                add       esp,10h
  004d9556  66395e16              cmp       [esi+16h],bx
  004d955a  bd01000000            mov       ebp,1
  004d955f  66896e12              mov       [esi+12h],bp
  004d9563  751b                  jne       short 004D9580h
  004d9565  e8d6db0600            call      00547140h
  004d956a  99                    cdq
  004d956b  33c2                  xor       eax,edx
  004d956d  2bc2                  sub       eax,edx
  004d956f  83e003                and       eax,3
  004d9572  33c2                  xor       eax,edx
  004d9574  2bc2                  sub       eax,edx
  004d9576  33ff                  xor       edi,edi
  004d9578  668b3c45882c5500      mov       di,[eax*2+552C88h]

LAB_004d9580:
  004d9580  66396e16              cmp       [esi+16h],bp
  004d9584  7517                  jne       short 004D959Dh
  004d9586  e8b5db0600            call      00547140h
  004d958b  99                    cdq
  004d958c  b905000000            mov       ecx,5
  004d9591  f7f9                  idiv      ecx
  004d9593  33ff                  xor       edi,edi
  004d9595  668b3c55902c5500      mov       di,[edx*2+552C90h]

LAB_004d959d:
  004d959d  66897e06              mov       [esi+6],di
  004d95a1  a1c4975e00            mov       eax,[5E97C4h]
  004d95a6  8bd7                  mov       edx,edi
  004d95a8  81e7ff000000          and       edi,0FFh
  004d95ae  8b889c000000          mov       ecx,[eax+9Ch]
  004d95b4  c1fa08                sar       edx,8
  004d95b7  81e2ff000000          and       edx,0FFh
  004d95bd  8d047f                lea       eax,[edi+edi*2]
  004d95c0  c1e004                shl       eax,4
  004d95c3  8b0c91                mov       ecx,[ecx+edx*4]
  004d95c6  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  004d95cc  8b06                  mov       eax,[esi]
  004d95ce  66895610              mov       [esi+10h],dx
  004d95d2  0d04080001            or        eax,1000804h
  004d95d7  895e5c                mov       [esi+5Ch],ebx
  004d95da  66895e18              mov       [esi+18h],bx
  004d95de  66895e1a              mov       [esi+1Ah],bx
  004d95e2  8906                  mov       [esi],eax

LAB_004d95e4:
  004d95e4  b801000000            mov       eax,1
  004d95e9  66394612              cmp       [esi+12h],ax
  004d95ed  7523                  jne       short 004D9612h
  004d95ef  66ff4e10              dec       word [esi+10h]
  004d95f3  66394610              cmp       [esi+10h],ax
  004d95f7  7d19                  jge       short 004D9612h
  004d95f9  8b06                  mov       eax,[esi]
  004d95fb  66c746120200          mov       word [esi+12h],2
  004d9601  25fbfffffe            and       eax,0FEFFFFFBh
  004d9606  66c746180300          mov       word [esi+18h],3
  004d960c  66895e1a              mov       [esi+1Ah],bx
  004d9610  8906                  mov       [esi],eax

LAB_004d9612:
  004d9612  66837e1202            cmp       word [esi+12h],2
  004d9617  7532                  jne       short 004D964Bh
  004d9619  66837e1a09            cmp       word [esi+1Ah],9
  004d961e  752b                  jne       short 004D964Bh
  004d9620  33c0                  xor       eax,eax
  004d9622  6aff                  push      0FFFFFFFFh
  004d9624  668b4606              mov       ax,[esi+6]
  004d9628  6aff                  push      0FFFFFFFFh
  004d962a  8bc8                  mov       ecx,eax
  004d962c  c1f808                sar       eax,8
  004d962f  81e1ff000000          and       ecx,0FFh
  004d9635  25ff000000            and       eax,0FFh
  004d963a  51                    push      ecx
  004d963b  50                    push      eax
  004d963c  56                    push      esi
  004d963d  e83e130600            call      0053A980h
  004d9642  83c414                add       esp,14h
  004d9645  66c746120300          mov       word [esi+12h],3

LAB_004d964b:
  004d964b  66837e1203            cmp       word [esi+12h],3
  004d9650  0f8548010000          jne       near 004D979Eh
  004d9656  8b06                  mov       eax,[esi]
  004d9658  a900000002            test      eax,2000000h
  004d965d  0f843b010000          je        near 004D979Eh
  004d9663  80e4f7                and       ah,0F7h
  004d9666  8906                  mov       [esi],eax
  004d9668  e92e010000            jmp       004D979Bh
  004d966d  668b4e12              mov       cx,[esi+12h]
  004d9671  6683f901              cmp       cx,1
  004d9675  7510                  jne       short 004D9687h
  004d9677  8b06                  mov       eax,[esi]
  004d9679  a900000002            test      eax,2000000h
  004d967e  7407                  je        short 004D9687h
  004d9680  24fd                  and       al,0FDh
  004d9682  885e43                mov       [esi+43h],bl
  004d9685  8906                  mov       [esi],eax

LAB_004d9687:
  004d9687  663bcb                cmp       cx,bx
  004d968a  0f850e010000          jne       near 004D979Eh
  004d9690  8b06                  mov       eax,[esi]
  004d9692  66c746120100          mov       word [esi+12h],1
  004d9698  25fffffffe            and       eax,0FEFFFFFFh
  004d969d  8906                  mov       [esi],eax
  004d969f  e9fa000000            jmp       004D979Eh
  004d96a4  66395e12              cmp       [esi+12h],bx
  004d96a8  7568                  jne       short 004D9712h
  004d96aa  b900f45f00            mov       ecx,5FF400h

LAB_004d96af:
  004d96af  39716c                cmp       [ecx+6Ch],esi
  004d96b2  750c                  jne       short 004D96C0h
  004d96b4  f70100000080          test      dword [ecx],80000000h
  004d96ba  0f85db000000          jne       near 004D979Bh

LAB_004d96c0:
  004d96c0  81c19c000000          add       ecx,9Ch
  004d96c6  81f980076000          cmp       ecx,600780h
  004d96cc  7ce1                  jl        short 004D96AFh
  004d96ce  33d2                  xor       edx,edx
  004d96d0  b9e2ca5e00            mov       ecx,5ECAE2h

LAB_004d96d5:
  004d96d5  80390c                cmp       byte [ecx],0Ch
  004d96d8  750d                  jne       short 004D96E7h
  004d96da  33c0                  xor       eax,eax
  004d96dc  8a4608                mov       al,[esi+8]
  004d96df  3bd0                  cmp       edx,eax
  004d96e1  0f85b4000000          jne       near 004D979Bh

LAB_004d96e7:
  004d96e7  81c19c000000          add       ecx,9Ch
  004d96ed  42                    inc       edx
  004d96ee  81f962de5e00          cmp       ecx,5EDE62h
  004d96f4  7cdf                  jl        short 004D96D5h
  004d96f6  8b06                  mov       eax,[esi]
  004d96f8  66c746180300          mov       word [esi+18h],3
  004d96fe  25fffffffc            and       eax,0FCFFFFFFh
  004d9703  66895e1a              mov       [esi+1Ah],bx
  004d9707  66c746120100          mov       word [esi+12h],1
  004d970d  895e5c                mov       [esi+5Ch],ebx
  004d9710  8906                  mov       [esi],eax

LAB_004d9712:
  004d9712  66837e1201            cmp       word [esi+12h],1
  004d9717  0f8581000000          jne       near 004D979Eh
  004d971d  817c2414e8030000      cmp       dword [esp+14h],3E8h
  004d9725  7f74                  jg        short 004D979Bh
  004d9727  66837e1a09            cmp       word [esi+1Ah],9
  004d972c  7565                  jne       short 004D9793h
  004d972e  66395e16              cmp       [esi+16h],bx
  004d9732  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004d9739  751a                  jne       short 004D9755h
  004d973b  e800da0600            call      00547140h
  004d9740  99                    cdq
  004d9741  33c2                  xor       eax,edx
  004d9743  2bc2                  sub       eax,edx
  004d9745  83e001                and       eax,1
  004d9748  33c2                  xor       eax,edx
  004d974a  2bc2                  sub       eax,edx
  004d974c  d1e0                  shl       eax,1
  004d974e  6689869a000000        mov       [esi+9Ah],ax

LAB_004d9755:
  004d9755  66837e1601            cmp       word [esi+16h],1
  004d975a  7519                  jne       short 004D9775h
  004d975c  e8dfd90600            call      00547140h
  004d9761  99                    cdq
  004d9762  33c2                  xor       eax,edx
  004d9764  2bc2                  sub       eax,edx
  004d9766  83e001                and       eax,1
  004d9769  33c2                  xor       eax,edx
  004d976b  2bc2                  sub       eax,edx
  004d976d  40                    inc       eax
  004d976e  6689869a000000        mov       [esi+9Ah],ax

LAB_004d9775:
  004d9775  8b4e6c                mov       ecx,[esi+6Ch]
  004d9778  6aff                  push      0FFFFFFFFh
  004d977a  53                    push      ebx
  004d977b  68df000000            push      0DFh
  004d9780  56                    push      esi
  004d9781  894e78                mov       [esi+78h],ecx
  004d9784  e84712faff            call      0047A9D0h
  004d9789  83c410                add       esp,10h
  004d978c  6689be9a000000        mov       [esi+9Ah],di

LAB_004d9793:
  004d9793  f70600000002          test      dword [esi],2000000h
  004d9799  7403                  je        short 004D979Eh

LAB_004d979b:
  004d979b  885e43                mov       [esi+43h],bl

LAB_004d979e:
  004d979e  0fbf5632              movsx     edx,word [esi+32h]
  004d97a2  52                    push      edx
  004d97a3  e874c20600            call      00545A1Ch
  004d97a8  8944246c              mov       [esp+6Ch],eax
  004d97ac  0fbf4632              movsx     eax,word [esi+32h]
  004d97b0  50                    push      eax
  004d97b1  e860c20600            call      00545A16h
  004d97b6  8b5650                mov       edx,[esi+50h]
  004d97b9  89442474              mov       [esp+74h],eax
  004d97bd  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  004d97c4  8bc2                  mov       eax,edx
  004d97c6  8bfa                  mov       edi,edx
  004d97c8  c1f808                sar       eax,8
  004d97cb  8b4c241c              mov       ecx,[esp+1Ch]
  004d97cf  2bf8                  sub       edi,eax
  004d97d1  c1fa03                sar       edx,3
  004d97d4  0faf4e5c              imul      ecx,[esi+5Ch]
  004d97d8  2bfa                  sub       edi,edx
  004d97da  8b5654                mov       edx,[esi+54h]
  004d97dd  8bc5                  mov       eax,ebp
  004d97df  8bda                  mov       ebx,edx
  004d97e1  c1f808                sar       eax,8
  004d97e4  c1fb08                sar       ebx,8
  004d97e7  2bc3                  sub       eax,ebx
  004d97e9  897e50                mov       [esi+50h],edi
  004d97ec  2bc5                  sub       eax,ebp
  004d97ee  56                    push      esi
  004d97ef  03c2                  add       eax,edx
  004d97f1  8b5658                mov       edx,[esi+58h]
  004d97f4  8bda                  mov       ebx,edx
  004d97f6  8bea                  mov       ebp,edx
  004d97f8  c1fb03                sar       ebx,3
  004d97fb  2beb                  sub       ebp,ebx
  004d97fd  8b5e1c                mov       ebx,[esi+1Ch]
  004d9800  c1fa08                sar       edx,8
  004d9803  c1f90c                sar       ecx,0Ch
  004d9806  2bea                  sub       ebp,edx
  004d9808  8bd1                  mov       edx,ecx
  004d980a  0faf542470            imul      edx,[esp+70h]
  004d980f  0faf4c2478            imul      ecx,[esp+78h]
  004d9814  c1fa0c                sar       edx,0Ch
  004d9817  c1f90c                sar       ecx,0Ch
  004d981a  896e58                mov       [esi+58h],ebp
  004d981d  03d7                  add       edx,edi
  004d981f  8b7e20                mov       edi,[esi+20h]
  004d9822  03cd                  add       ecx,ebp
  004d9824  8b6e24                mov       ebp,[esi+24h]
  004d9827  03da                  add       ebx,edx
  004d9829  03f8                  add       edi,eax
  004d982b  03e9                  add       ebp,ecx
  004d982d  894654                mov       [esi+54h],eax
  004d9830  895e1c                mov       [esi+1Ch],ebx
  004d9833  897e20                mov       [esi+20h],edi
  004d9836  896e24                mov       [esi+24h],ebp
  004d9839  e802a10500            call      00533940h
  004d983e  8b9690000000          mov       edx,[esi+90h]
  004d9844  8b4660                mov       eax,[esi+60h]
  004d9847  8b4e20                mov       ecx,[esi+20h]
  004d984a  83c404                add       esp,4
  004d984d  80e6fc                and       dh,0FCh
  004d9850  3bc8                  cmp       ecx,eax
  004d9852  899690000000          mov       [esi+90h],edx
  004d9858  7f2a                  jg        short 004D9884h
  004d985a  f6c680                test      dh,80h
  004d985d  7525                  jne       short 004D9884h
  004d985f  8b0e                  mov       ecx,[esi]
  004d9861  894620                mov       [esi+20h],eax
  004d9864  f6c101                test      cl,1
  004d9867  740f                  je        short 004D9878h
  004d9869  83e1fe                and       ecx,0FFFFFFFEh
  004d986c  33db                  xor       ebx,ebx
  004d986e  890e                  mov       [esi],ecx
  004d9870  895e54                mov       [esi+54h],ebx
  004d9873  e925010000            jmp       004D999Dh

LAB_004d9878:
  004d9878  83e1fe                and       ecx,0FFFFFFFEh
  004d987b  33db                  xor       ebx,ebx
  004d987d  890e                  mov       [esi],ecx
  004d987f  895e54                mov       [esi+54h],ebx
  004d9882  eb02                  jmp       short 004D9886h

LAB_004d9884:
  004d9884  33db                  xor       ebx,ebx

LAB_004d9886:
  004d9886  8b0e                  mov       ecx,[esi]
  004d9888  f6c101                test      cl,1
  004d988b  750a                  jne       short 004D9897h
  004d988d  394620                cmp       [esi+20h],eax
  004d9890  7e05                  jle       short 004D9897h
  004d9892  83c901                or        ecx,1
  004d9895  890e                  mov       [esi],ecx

LAB_004d9897:
  004d9897  f7c282ad0020          test      edx,2000AD82h
  004d989d  0f84fa000000          je        near 004D999Dh
  004d98a3  f60601                test      byte [esi],1
  004d98a6  0f85c9000000          jne       near 004D9975h
  004d98ac  8a4e42                mov       cl,[esi+42h]
  004d98af  b007                  mov       al,7
  004d98b1  3ac8                  cmp       cl,al
  004d98b3  7420                  je        short 004D98D5h
  004d98b5  884642                mov       [esi+42h],al
  004d98b8  e883d80600            call      00547140h
  004d98bd  99                    cdq
  004d98be  b928000000            mov       ecx,28h
  004d98c3  66895e12              mov       [esi+12h],bx
  004d98c7  f7f9                  idiv      ecx
  004d98c9  66c746105000          mov       word [esi+10h],50h
  004d98cf  80c214                add       dl,14h
  004d98d2  885643                mov       [esi+43h],dl

LAB_004d98d5:
  004d98d5  8b16                  mov       edx,[esi]
  004d98d7  8b442428              mov       eax,[esp+28h]
  004d98db  8b4c242c              mov       ecx,[esp+2Ch]
  004d98df  83e2fd                and       edx,0FFFFFFFDh
  004d98e2  8916                  mov       [esi],edx
  004d98e4  8b542424              mov       edx,[esp+24h]
  004d98e8  894620                mov       [esi+20h],eax
  004d98eb  8b442448              mov       eax,[esp+48h]
  004d98ef  89561c                mov       [esi+1Ch],edx
  004d98f2  8b54245c              mov       edx,[esp+5Ch]
  004d98f6  25ffff0000            and       eax,0FFFFh
  004d98fb  894e24                mov       [esi+24h],ecx
  004d98fe  8b4c243c              mov       ecx,[esp+3Ch]
  004d9902  89567c                mov       [esi+7Ch],edx
  004d9905  8b542458              mov       edx,[esp+58h]
  004d9909  898690000000          mov       [esi+90h],eax
  004d990f  8b44244c              mov       eax,[esp+4Ch]
  004d9913  898e84000000          mov       [esi+84h],ecx
  004d9919  8b4c2444              mov       ecx,[esp+44h]
  004d991d  898680000000          mov       [esi+80h],eax
  004d9923  8b442450              mov       eax,[esp+50h]
  004d9927  899688000000          mov       [esi+88h],edx
  004d992d  8b542440              mov       edx,[esp+40h]
  004d9931  6882ad0020            push      2000AD82h
  004d9936  56                    push      esi
  004d9937  89868c000000          mov       [esi+8Ch],eax
  004d993d  894e64                mov       [esi+64h],ecx
  004d9940  895660                mov       [esi+60h],edx
  004d9943  e818c5ffff            call      004D5E60h
  004d9948  83c408                add       esp,8
  004d994b  663bc3                cmp       ax,bx
  004d994e  7515                  jne       short 004D9965h
  004d9950  e8ebd70600            call      00547140h
  004d9955  99                    cdq
  004d9956  33c2                  xor       eax,edx
  004d9958  2bc2                  sub       eax,edx
  004d995a  83e001                and       eax,1
  004d995d  33c2                  xor       eax,edx
  004d995f  2bc2                  sub       eax,edx
  004d9961  8d4400ff              lea       eax,[eax+eax-1]

LAB_004d9965:
  004d9965  8b4c2410              mov       ecx,[esp+10h]
  004d9969  83c140                add       ecx,40h
  004d996c  0fafc8                imul      ecx,eax
  004d996f  66014e32              add       [esi+32h],cx
  004d9973  eb28                  jmp       short 004D999Dh

LAB_004d9975:
  004d9975  8b542424              mov       edx,[esp+24h]
  004d9979  8b44242c              mov       eax,[esp+2Ch]
  004d997d  895e5c                mov       [esi+5Ch],ebx
  004d9980  895e50                mov       [esi+50h],ebx
  004d9983  895e58                mov       [esi+58h],ebx
  004d9986  89561c                mov       [esi+1Ch],edx
  004d9989  894624                mov       [esi+24h],eax
  004d998c  885e42                mov       [esi+42h],bl
  004d998f  c6464328              mov       byte [esi+43h],28h
  004d9993  66895e12              mov       [esi+12h],bx
  004d9997  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004d999d:
  004d999d  6aff                  push      0FFFFFFFFh
  004d999f  6aff                  push      0FFFFFFFFh
  004d99a1  6aff                  push      0FFFFFFFFh
  004d99a3  56                    push      esi
  004d99a4  e8679efeff            call      004C3810h
  004d99a9  8b44242c              mov       eax,[esp+2Ch]
  004d99ad  83c410                add       esp,10h
  004d99b0  3bc3                  cmp       eax,ebx
  004d99b2  7517                  jne       short 004D99CBh
  004d99b4  668b8c2482000000      mov       cx,[esp+82h]
  004d99bc  66894e32              mov       [esi+32h],cx
  004d99c0  5f                    pop       edi
  004d99c1  5e                    pop       esi
  004d99c2  5d                    pop       ebp
  004d99c3  5b                    pop       ebx
  004d99c4  81c484000000          add       esp,84h
  004d99ca  c3                    ret

LAB_004d99cb:
  004d99cb  385e43                cmp       [esi+43h],bl
  004d99ce  0f852d030000          jne       near 004D9D01h
  004d99d4  8b466c                mov       eax,[esi+6Ch]
  004d99d7  8b561c                mov       edx,[esi+1Ch]
  004d99da  8b781c                mov       edi,[eax+1Ch]
  004d99dd  8b6824                mov       ebp,[eax+24h]
  004d99e0  8b4624                mov       eax,[esi+24h]
  004d99e3  2bfa                  sub       edi,edx
  004d99e5  2be8                  sub       ebp,eax
  004d99e7  c1ff0c                sar       edi,0Ch
  004d99ea  c1fd0c                sar       ebp,0Ch
  004d99ed  8bd7                  mov       edx,edi
  004d99ef  8bc5                  mov       eax,ebp
  004d99f1  0fafd7                imul      edx,edi
  004d99f4  0fafc5                imul      eax,ebp
  004d99f7  03d0                  add       edx,eax
  004d99f9  52                    push      edx
  004d99fa  e811c00600            call      00545A10h
  004d99ff  55                    push      ebp
  004d9a00  57                    push      edi
  004d9a01  8944241c              mov       [esp+1Ch],eax
  004d9a05  e818c00600            call      00545A22h
  004d9a0a  668b4e32              mov       cx,[esi+32h]
  004d9a0e  66895e12              mov       [esi+12h],bx
  004d9a12  2bc1                  sub       eax,ecx
  004d9a14  25ff0f0000            and       eax,0FFFh
  004d9a19  89442418              mov       [esp+18h],eax
  004d9a1d  8b442414              mov       eax,[esp+14h]
  004d9a21  3dfa000000            cmp       eax,0FAh
  004d9a26  0f8dd6000000          jge       near 004D9B02h
  004d9a2c  e80fd70600            call      00547140h
  004d9a31  99                    cdq
  004d9a32  b964000000            mov       ecx,64h
  004d9a37  f7f9                  idiv      ecx
  004d9a39  8bea                  mov       ebp,edx
  004d9a3b  83fd32                cmp       ebp,32h
  004d9a3e  0f8d8d000000          jge       near 004D9AD1h
  004d9a44  8b542418              mov       edx,[esp+18h]
  004d9a48  8b7c2420              mov       edi,[esp+20h]
  004d9a4c  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004d9a53  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004d9a5a  25ff0f0000            and       eax,0FFFh
  004d9a5f  3bc1                  cmp       eax,ecx
  004d9a61  7f64                  jg        short 004D9AC7h
  004d9a63  e8d8d60600            call      00547140h
  004d9a68  99                    cdq
  004d9a69  8d4c3f01              lea       ecx,[edi+edi+1]
  004d9a6d  f7f9                  idiv      ecx
  004d9a6f  8b442414              mov       eax,[esp+14h]
  004d9a73  83c264                add       edx,64h
  004d9a76  3bc2                  cmp       eax,edx
  004d9a78  7d4d                  jge       short 004D9AC7h
  004d9a7a  83fd1e                cmp       ebp,1Eh
  004d9a7d  7d24                  jge       short 004D9AA3h
  004d9a7f  8b16                  mov       edx,[esi]
  004d9a81  c646420b              mov       byte [esi+42h],0Bh
  004d9a85  81e2fffffffe          and       edx,0FEFFFFFFh
  004d9a8b  c6464301              mov       byte [esi+43h],1
  004d9a8f  83ca02                or        edx,2
  004d9a92  895e5c                mov       [esi+5Ch],ebx
  004d9a95  8916                  mov       [esi],edx
  004d9a97  66c746180300          mov       word [esi+18h],3
  004d9a9d  66895e1a              mov       [esi+1Ah],bx
  004d9aa1  eb63                  jmp       short 004D9B06h

LAB_004d9aa3:
  004d9aa3  885e42                mov       [esi+42h],bl
  004d9aa6  e895d60600            call      00547140h
  004d9aab  99                    cdq
  004d9aac  b928000000            mov       ecx,28h
  004d9ab1  66c746105000          mov       word [esi+10h],50h
  004d9ab7  f7f9                  idiv      ecx
  004d9ab9  8bc7                  mov       eax,edi
  004d9abb  d1f8                  sar       eax,1
  004d9abd  02d0                  add       dl,al
  004d9abf  80c214                add       dl,14h
  004d9ac2  885643                mov       [esi+43h],dl
  004d9ac5  eb3f                  jmp       short 004D9B06h

LAB_004d9ac7:
  004d9ac7  c6464201              mov       byte [esi+42h],1
  004d9acb  c6464301              mov       byte [esi+43h],1
  004d9acf  eb35                  jmp       short 004D9B06h

LAB_004d9ad1:
  004d9ad1  83fd3c                cmp       ebp,3Ch
  004d9ad4  0f8c32020000          jl        near 004D9D0Ch
  004d9ada  83fd46                cmp       ebp,46h
  004d9add  0f8d29020000          jge       near 004D9D0Ch
  004d9ae3  8b4c2410              mov       ecx,[esp+10h]
  004d9ae7  8b442414              mov       eax,[esp+14h]
  004d9aeb  83c164                add       ecx,64h
  004d9aee  3bc1                  cmp       eax,ecx
  004d9af0  0f8e16020000          jle       near 004D9D0Ch
  004d9af6  c646420c              mov       byte [esi+42h],0Ch
  004d9afa  c6464301              mov       byte [esi+43h],1
  004d9afe  66895e12              mov       [esi+12h],bx

LAB_004d9b02:
  004d9b02  8b7c2420              mov       edi,[esp+20h]

LAB_004d9b06:
  004d9b06  8b442414              mov       eax,[esp+14h]
  004d9b0a  3d58020000            cmp       eax,258h
  004d9b0f  0f8df3000000          jge       near 004D9C08h
  004d9b15  3dfa000000            cmp       eax,0FAh
  004d9b1a  0f8ce8000000          jl        near 004D9C08h
  004d9b20  e81bd60600            call      00547140h
  004d9b25  99                    cdq
  004d9b26  b964000000            mov       ecx,64h
  004d9b2b  f7f9                  idiv      ecx
  004d9b2d  83fa0f                cmp       edx,0Fh
  004d9b30  7d36                  jge       short 004D9B68h
  004d9b32  8b542418              mov       edx,[esp+18h]
  004d9b36  81c200040000          add       edx,400h
  004d9b3c  81e2ff0f0000          and       edx,0FFFh
  004d9b42  81fa00080000          cmp       edx,800h
  004d9b48  7c0d                  jl        short 004D9B57h
  004d9b4a  c6464201              mov       byte [esi+42h],1
  004d9b4e  c6464301              mov       byte [esi+43h],1
  004d9b52  e9b1000000            jmp       004D9C08h

LAB_004d9b57:
  004d9b57  c646420a              mov       byte [esi+42h],0Ah
  004d9b5b  c6464301              mov       byte [esi+43h],1
  004d9b5f  66895e12              mov       [esi+12h],bx
  004d9b63  e9a0000000            jmp       004D9C08h

LAB_004d9b68:
  004d9b68  83fa28                cmp       edx,28h
  004d9b6b  7d25                  jge       short 004D9B92h
  004d9b6d  c6464202              mov       byte [esi+42h],2
  004d9b71  e8cad50600            call      00547140h
  004d9b76  99                    cdq
  004d9b77  b90a000000            mov       ecx,0Ah
  004d9b7c  66c746105000          mov       word [esi+10h],50h
  004d9b82  f7f9                  idiv      ecx
  004d9b84  8bc7                  mov       eax,edi
  004d9b86  d1f8                  sar       eax,1
  004d9b88  02d0                  add       dl,al
  004d9b8a  80c214                add       dl,14h
  004d9b8d  885643                mov       [esi+43h],dl
  004d9b90  eb76                  jmp       short 004D9C08h

LAB_004d9b92:
  004d9b92  83fa46                cmp       edx,46h
  004d9b95  7d17                  jge       short 004D9BAEh
  004d9b97  c6464206              mov       byte [esi+42h],6
  004d9b9b  c6464301              mov       byte [esi+43h],1
  004d9b9f  e89cd50600            call      00547140h
  004d9ba4  66894610              mov       [esi+10h],ax
  004d9ba8  66895e12              mov       [esi+12h],bx
  004d9bac  eb5a                  jmp       short 004D9C08h

LAB_004d9bae:
  004d9bae  b964000000            mov       ecx,64h
  004d9bb3  2bca                  sub       ecx,edx
  004d9bb5  8b542410              mov       edx,[esp+10h]
  004d9bb9  d1fa                  sar       edx,1
  004d9bbb  83c20a                add       edx,0Ah
  004d9bbe  3bca                  cmp       ecx,edx
  004d9bc0  7d0e                  jge       short 004D9BD0h
  004d9bc2  c646420c              mov       byte [esi+42h],0Ch
  004d9bc6  c6464301              mov       byte [esi+43h],1
  004d9bca  66895e12              mov       [esi+12h],bx
  004d9bce  eb38                  jmp       short 004D9C08h

LAB_004d9bd0:
  004d9bd0  c6464203              mov       byte [esi+42h],3
  004d9bd4  66895e12              mov       [esi+12h],bx
  004d9bd8  e863d50600            call      00547140h
  004d9bdd  99                    cdq
  004d9bde  b90a000000            mov       ecx,0Ah
  004d9be3  f7f9                  idiv      ecx
  004d9be5  8bc7                  mov       eax,edi
  004d9be7  d1f8                  sar       eax,1
  004d9be9  02d0                  add       dl,al
  004d9beb  80c214                add       dl,14h
  004d9bee  885643                mov       [esi+43h],dl
  004d9bf1  e84ad50600            call      00547140h
  004d9bf6  99                    cdq
  004d9bf7  33c2                  xor       eax,edx
  004d9bf9  2bc2                  sub       eax,edx
  004d9bfb  25ff0f0000            and       eax,0FFFh
  004d9c00  33c2                  xor       eax,edx
  004d9c02  2bc2                  sub       eax,edx
  004d9c04  66894610              mov       [esi+10h],ax

LAB_004d9c08:
  004d9c08  817c241458020000      cmp       dword [esp+14h],258h
  004d9c10  0f8ceb000000          jl        near 004D9D01h
  004d9c16  e825d50600            call      00547140h
  004d9c1b  99                    cdq
  004d9c1c  b964000000            mov       ecx,64h
  004d9c21  f7f9                  idiv      ecx
  004d9c23  83fa1e                cmp       edx,1Eh
  004d9c26  7d35                  jge       short 004D9C5Dh
  004d9c28  885e42                mov       [esi+42h],bl
  004d9c2b  e810d50600            call      00547140h
  004d9c30  99                    cdq
  004d9c31  b90a000000            mov       ecx,0Ah
  004d9c36  66895e12              mov       [esi+12h],bx
  004d9c3a  f7f9                  idiv      ecx
  004d9c3c  8b4c2410              mov       ecx,[esp+10h]
  004d9c40  8bc7                  mov       eax,edi
  004d9c42  c1f802                sar       eax,2
  004d9c45  02d0                  add       dl,al
  004d9c47  885643                mov       [esi+43h],dl
  004d9c4a  8d540950              lea       edx,[ecx+ecx+50h]
  004d9c4e  66895610              mov       [esi+10h],dx
  004d9c52  5f                    pop       edi
  004d9c53  5e                    pop       esi
  004d9c54  5d                    pop       ebp
  004d9c55  5b                    pop       ebx
  004d9c56  81c484000000          add       esp,84h
  004d9c5c  c3                    ret

LAB_004d9c5d:
  004d9c5d  83fa28                cmp       edx,28h
  004d9c60  7d43                  jge       short 004D9CA5h
  004d9c62  c6464203              mov       byte [esi+42h],3
  004d9c66  66895e12              mov       [esi+12h],bx
  004d9c6a  e8d1d40600            call      00547140h
  004d9c6f  99                    cdq
  004d9c70  b90a000000            mov       ecx,0Ah
  004d9c75  f7f9                  idiv      ecx
  004d9c77  8bc7                  mov       eax,edi
  004d9c79  d1f8                  sar       eax,1
  004d9c7b  02d0                  add       dl,al
  004d9c7d  80c214                add       dl,14h
  004d9c80  885643                mov       [esi+43h],dl
  004d9c83  e8b8d40600            call      00547140h
  004d9c88  99                    cdq
  004d9c89  33c2                  xor       eax,edx
  004d9c8b  2bc2                  sub       eax,edx
  004d9c8d  25ff0f0000            and       eax,0FFFh
  004d9c92  33c2                  xor       eax,edx
  004d9c94  2bc2                  sub       eax,edx
  004d9c96  66894610              mov       [esi+10h],ax
  004d9c9a  5f                    pop       edi
  004d9c9b  5e                    pop       esi
  004d9c9c  5d                    pop       ebp
  004d9c9d  5b                    pop       ebx
  004d9c9e  81c484000000          add       esp,84h
  004d9ca4  c3                    ret

LAB_004d9ca5:
  004d9ca5  83fa3c                cmp       edx,3Ch
  004d9ca8  7d13                  jge       short 004D9CBDh
  004d9caa  c6464201              mov       byte [esi+42h],1
  004d9cae  c6464301              mov       byte [esi+43h],1
  004d9cb2  5f                    pop       edi
  004d9cb3  5e                    pop       esi
  004d9cb4  5d                    pop       ebp
  004d9cb5  5b                    pop       ebx
  004d9cb6  81c484000000          add       esp,84h
  004d9cbc  c3                    ret

LAB_004d9cbd:
  004d9cbd  83fa50                cmp       edx,50h
  004d9cc0  7d2e                  jge       short 004D9CF0h
  004d9cc2  c6464202              mov       byte [esi+42h],2
  004d9cc6  e875d40600            call      00547140h
  004d9ccb  99                    cdq
  004d9ccc  b90a000000            mov       ecx,0Ah
  004d9cd1  66c746106400          mov       word [esi+10h],64h
  004d9cd7  f7f9                  idiv      ecx
  004d9cd9  8bc7                  mov       eax,edi
  004d9cdb  d1f8                  sar       eax,1
  004d9cdd  02d0                  add       dl,al
  004d9cdf  80c214                add       dl,14h
  004d9ce2  885643                mov       [esi+43h],dl
  004d9ce5  5f                    pop       edi
  004d9ce6  5e                    pop       esi
  004d9ce7  5d                    pop       ebp
  004d9ce8  5b                    pop       ebx
  004d9ce9  81c484000000          add       esp,84h
  004d9cef  c3                    ret

LAB_004d9cf0:
  004d9cf0  c6464206              mov       byte [esi+42h],6
  004d9cf4  c6464301              mov       byte [esi+43h],1
  004d9cf8  e843d40600            call      00547140h
  004d9cfd  66894610              mov       [esi+10h],ax

LAB_004d9d01:
  004d9d01  5f                    pop       edi
  004d9d02  5e                    pop       esi
  004d9d03  5d                    pop       ebp
  004d9d04  5b                    pop       ebx
  004d9d05  81c484000000          add       esp,84h
  004d9d0b  c3                    ret

LAB_004d9d0c:
  004d9d0c  c6464203              mov       byte [esi+42h],3
  004d9d10  66895e12              mov       [esi+12h],bx
  004d9d14  e827d40600            call      00547140h
  004d9d19  99                    cdq
  004d9d1a  b90a000000            mov       ecx,0Ah
  004d9d1f  8b7c2420              mov       edi,[esp+20h]
  004d9d23  f7f9                  idiv      ecx
  004d9d25  8bc7                  mov       eax,edi
  004d9d27  d1f8                  sar       eax,1
  004d9d29  02d0                  add       dl,al
  004d9d2b  80c214                add       dl,14h
  004d9d2e  885643                mov       [esi+43h],dl
  004d9d31  e80ad40600            call      00547140h
  004d9d36  99                    cdq
  004d9d37  33c2                  xor       eax,edx
  004d9d39  2bc2                  sub       eax,edx
  004d9d3b  25ff0f0000            and       eax,0FFFh
  004d9d40  33c2                  xor       eax,edx
  004d9d42  2bc2                  sub       eax,edx
  004d9d44  66894610              mov       [esi+10h],ax
  004d9d48  e9b9fdffff            jmp       004D9B06h
  004d9d4d  90                    nop
  004d9d4e  8bff                  mov       edi,edi
  004d9d50  dd914d002392          fst       qword [ecx-6DDCFFB3h]
  004d9d56  4d                    dec       ebp
  004d9d57  0099924d003f          add       [ecx+3F004D92h],bl
  004d9d5d  93                    xchg      ebx,eax
  004d9d5e  4d                    dec       ebp
  004d9d5f  009e974d009e          add       [esi-61FFB269h],bl
  004d9d65  97                    xchg      edi,eax
  004d9d66  4d                    dec       ebp
  004d9d67  000d944d009e          add       [9E004D94h],cl
  004d9d6d  94                    xchg      esp,eax
  004d9d6e  4d                    dec       ebp
  004d9d6f  009e974d009e          add       [esi-61FFB269h],bl
  004d9d75  97                    xchg      edi,eax
  004d9d76  4d                    dec       ebp
  004d9d77  00fe                  add       dh,bh
  004d9d79  94                    xchg      esp,eax
  004d9d7a  4d                    dec       ebp
  004d9d7b  006d96                add       [ebp-6Ah],ch
  004d9d7e  4d                    dec       ebp
  004d9d7f  00a4964d009090        add       [esi+edx*4-6F6FFFB3h],ah
  004d9d86  90                    nop
  004d9d87  90                    nop
  004d9d88  90                    nop
  004d9d89  90                    nop
  004d9d8a  90                    nop
  004d9d8b  90                    nop
  004d9d8c  90                    nop
  004d9d8d  90                    nop
  004d9d8e  90                    nop
  004d9d8f  90                    nop

72 cible(s) de saut interne : 0x004d8fc1, 0x004d9110, 0x004d9132, 0x004d9134, 0x004d9141, 0x004d9143, 0x004d91a6, 0x004d91aa, 0x004d91c3, 0x004d91c8, 0x004d91fd, 0x004d9244, 0x004d9282, 0x004d92cd, 0x004d92e1, 0x004d92e4, 0x004d92ec, 0x004d92f8, 0x004d932f, 0x004d9356, 0x004d93d1, 0x004d93df, 0x004d93e3, 0x004d941e, 0x004d9490, 0x004d94bb, 0x004d94c7, 0x004d950f, 0x004d9520, 0x004d9545, 0x004d9580, 0x004d959d, 0x004d95e4, 0x004d9612, 0x004d964b, 0x004d9687, 0x004d96af, 0x004d96c0, 0x004d96d5, 0x004d96e7, 0x004d9712, 0x004d9755, 0x004d9775, 0x004d9793, 0x004d979b, 0x004d979e, 0x004d9878, 0x004d9884, 0x004d9886, 0x004d9897, 0x004d98d5, 0x004d9965, 0x004d9975, 0x004d999d, 0x004d99cb, 0x004d9aa3, 0x004d9ac7, 0x004d9ad1, 0x004d9b02, 0x004d9b06, 0x004d9b57, 0x004d9b68, 0x004d9b92, 0x004d9bae, 0x004d9bd0, 0x004d9c08, 0x004d9c5d, 0x004d9ca5, 0x004d9cbd, 0x004d9cf0, 0x004d9d01, 0x004d9d0c
