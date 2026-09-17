Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004b0600  section .text  offset fichier 0xafa00  4128 octets

  004b0600  81ec94000000          sub       esp,94h
  004b0606  53                    push      ebx
  004b0607  55                    push      ebp
  004b0608  56                    push      esi
  004b0609  8bb424a4000000        mov       esi,[esp+0A4h]
  004b0610  b864000000            mov       eax,64h
  004b0615  57                    push      edi
  004b0616  8b4e20                mov       ecx,[esi+20h]
  004b0619  8b5624                mov       edx,[esi+24h]
  004b061c  8d6e1c                lea       ebp,[esi+1Ch]
  004b061f  6689442436            mov       [esp+36h],ax
  004b0624  668944243a            mov       [esp+3Ah],ax
  004b0629  6689442442            mov       [esp+42h],ax
  004b062e  6689442446            mov       [esp+46h],ax
  004b0633  8b4500                mov       eax,[ebp]
  004b0636  89442424              mov       [esp+24h],eax
  004b063a  8b4664                mov       eax,[esi+64h]
  004b063d  894c2428              mov       [esp+28h],ecx
  004b0641  8b4e60                mov       ecx,[esi+60h]
  004b0644  8954242c              mov       [esp+2Ch],edx
  004b0648  8b567c                mov       edx,[esi+7Ch]
  004b064b  8944246c              mov       [esp+6Ch],eax
  004b064f  668b8690000000        mov       ax,[esi+90h]
  004b0656  894c2454              mov       [esp+54h],ecx
  004b065a  8b8e80000000          mov       ecx,[esi+80h]
  004b0660  89542470              mov       [esp+70h],edx
  004b0664  8b9684000000          mov       edx,[esi+84h]
  004b066a  6689442464            mov       [esp+64h],ax
  004b066f  8b8688000000          mov       eax,[esi+88h]
  004b0675  894c2450              mov       [esp+50h],ecx
  004b0679  8b8e8c000000          mov       ecx,[esi+8Ch]
  004b067f  8b7e6c                mov       edi,[esi+6Ch]
  004b0682  89542448              mov       [esp+48h],edx
  004b0686  668b5632              mov       dx,[esi+32h]
  004b068a  89442460              mov       [esp+60h],eax
  004b068e  894c245c              mov       [esp+5Ch],ecx
  004b0692  8d442420              lea       eax,[esp+20h]
  004b0696  6689942492000000      mov       [esp+92h],dx
  004b069e  8d4c2474              lea       ecx,[esp+74h]
  004b06a2  50                    push      eax
  004b06a3  8d54247c              lea       edx,[esp+7Ch]
  004b06a7  51                    push      ecx
  004b06a8  33db                  xor       ebx,ebx
  004b06aa  52                    push      edx
  004b06ab  56                    push      esi
  004b06ac  66895c2440            mov       [esp+40h],bx
  004b06b1  66c74424421e00        mov       word [esp+42h],1Eh
  004b06b8  66c74424443200        mov       word [esp+44h],32h
  004b06bf  66895c2448            mov       [esp+48h],bx
  004b06c4  66895c244c            mov       [esp+4Ch],bx
  004b06c9  66895c244e            mov       [esp+4Eh],bx
  004b06ce  66c74424505000        mov       word [esp+50h],50h
  004b06d5  66895c2454            mov       [esp+54h],bx
  004b06da  e8a1820800            call      00538980h
  004b06df  83c410                add       esp,10h
  004b06e2  8d442458              lea       eax,[esp+58h]
  004b06e6  8d4c2418              lea       ecx,[esp+18h]
  004b06ea  8d54241c              lea       edx,[esp+1Ch]
  004b06ee  50                    push      eax
  004b06ef  51                    push      ecx
  004b06f0  8d44241c              lea       eax,[esp+1Ch]
  004b06f4  52                    push      edx
  004b06f5  8d4c241c              lea       ecx,[esp+1Ch]
  004b06f9  50                    push      eax
  004b06fa  51                    push      ecx
  004b06fb  56                    push      esi
  004b06fc  e81f580800            call      00535F20h
  004b0701  8b4c2428              mov       ecx,[esp+28h]
  004b0705  8b542470              mov       edx,[esp+70h]
  004b0709  83c418                add       esp,18h
  004b070c  b840000000            mov       eax,40h
  004b0711  2bc1                  sub       eax,ecx
  004b0713  52                    push      edx
  004b0714  56                    push      esi
  004b0715  89442454              mov       [esp+54h],eax
  004b0719  e8c2560200            call      004D5DE0h
  004b071e  8b4500                mov       eax,[ebp]
  004b0721  8b571c                mov       edx,[edi+1Ch]
  004b0724  8b4e20                mov       ecx,[esi+20h]
  004b0727  2bc2                  sub       eax,edx
  004b0729  83c408                add       esp,8
  004b072c  c1f80c                sar       eax,0Ch
  004b072f  8b5720                mov       edx,[edi+20h]
  004b0732  2bca                  sub       ecx,edx
  004b0734  8b5624                mov       edx,[esi+24h]
  004b0737  2b5724                sub       edx,[edi+24h]
  004b073a  8bf8                  mov       edi,eax
  004b073c  c1f90c                sar       ecx,0Ch
  004b073f  0faff8                imul      edi,eax
  004b0742  8bc1                  mov       eax,ecx
  004b0744  c1fa0c                sar       edx,0Ch
  004b0747  0fafc1                imul      eax,ecx
  004b074a  8bca                  mov       ecx,edx
  004b074c  03f8                  add       edi,eax
  004b074e  0fafca                imul      ecx,edx
  004b0751  03f9                  add       edi,ecx
  004b0753  57                    push      edi
  004b0754  e8b7520900            call      00545A10h
  004b0759  89442414              mov       [esp+14h],eax
  004b075d  8b442418              mov       eax,[esp+18h]
  004b0761  3bc3                  cmp       eax,ebx
  004b0763  0f8433080000          je        near 004B0F9Ch
  004b0769  8b3e                  mov       edi,[esi]
  004b076b  33d2                  xor       edx,edx
  004b076d  8a5604                mov       dl,[esi+4]
  004b0770  81e7fffffffd          and       edi,0FDFFFFFFh
  004b0776  0fbf4e18              movsx     ecx,word [esi+18h]
  004b077a  893e                  mov       [esi],edi
  004b077c  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004b0783  f7c700000001          test      edi,1000000h
  004b0789  8b0c8a                mov       ecx,[edx+ecx*4]
  004b078c  741b                  je        short 004B07A9h
  004b078e  66ff461a              inc       word [esi+1Ah]
  004b0792  668b561a              mov       dx,[esi+1Ah]
  004b0796  33ff                  xor       edi,edi
  004b0798  668b7916              mov       di,[ecx+16h]
  004b079c  0fbfca                movsx     ecx,dx
  004b079f  3bcf                  cmp       ecx,edi
  004b07a1  7c29                  jl        short 004B07CCh
  004b07a3  66895e1a              mov       [esi+1Ah],bx
  004b07a7  eb23                  jmp       short 004B07CCh

LAB_004b07a9:
  004b07a9  66ff461a              inc       word [esi+1Ah]
  004b07ad  33d2                  xor       edx,edx
  004b07af  668b5116              mov       dx,[ecx+16h]
  004b07b3  0fbf4e1a              movsx     ecx,word [esi+1Ah]
  004b07b7  3bca                  cmp       ecx,edx
  004b07b9  7c11                  jl        short 004B07CCh
  004b07bb  668b4e1a              mov       cx,[esi+1Ah]
  004b07bf  49                    dec       ecx
  004b07c0  81cf00000002          or        edi,2000000h
  004b07c6  66894e1a              mov       [esi+1Ah],cx
  004b07ca  893e                  mov       [esi],edi

LAB_004b07cc:
  004b07cc  8a4e42                mov       cl,[esi+42h]
  004b07cf  8b16                  mov       edx,[esi]
  004b07d1  80f905                cmp       cl,5
  004b07d4  7505                  jne       short 004B07DBh
  004b07d6  83ca10                or        edx,10h
  004b07d9  eb03                  jmp       short 004B07DEh

LAB_004b07db:
  004b07db  83e2ef                and       edx,0FFFFFFEFh

LAB_004b07de:
  004b07de  8916                  mov       [esi],edx
  004b07e0  8a16                  mov       dl,[esi]
  004b07e2  f6c202                test      dl,2
  004b07e5  0f84cd010000          je        near 004B09B8h
  004b07eb  80f90c                cmp       cl,0Ch
  004b07ee  0f8482000000          je        near 004B0876h
  004b07f4  80f90d                cmp       cl,0Dh
  004b07f7  747d                  je        short 004B0876h
  004b07f9  66837e1a08            cmp       word [esi+1Ah],8
  004b07fe  7558                  jne       short 004B0858h
  004b0800  8d542430              lea       edx,[esp+30h]
  004b0804  53                    push      ebx
  004b0805  52                    push      edx
  004b0806  55                    push      ebp
  004b0807  56                    push      esi
  004b0808  e823940600            call      00519C30h
  004b080d  8bf8                  mov       edi,eax
  004b080f  a1b8e05d00            mov       eax,[5DE0B8h]
  004b0814  83c410                add       esp,10h
  004b0817  3bc3                  cmp       eax,ebx
  004b0819  7439                  je        short 004B0854h
  004b081b  66837e1a08            cmp       word [esi+1Ah],8
  004b0820  7532                  jne       short 004B0854h
  004b0822  8b0f                  mov       ecx,[edi]
  004b0824  57                    push      edi
  004b0825  83c940                or        ecx,40h
  004b0828  56                    push      esi
  004b0829  66c787960000001000    mov       word [edi+96h],10h
  004b0832  890f                  mov       [edi],ecx
  004b0834  e847930800            call      00539B80h
  004b0839  83c408                add       esp,8
  004b083c  83c71c                add       edi,1Ch
  004b083f  57                    push      edi
  004b0840  6800180000            push      1800h
  004b0845  6800030000            push      300h
  004b084a  6a03                  push      3
  004b084c  e8cfd20800            call      0053DB20h
  004b0851  83c410                add       esp,10h

LAB_004b0854:
  004b0854  8b442418              mov       eax,[esp+18h]

LAB_004b0858:
  004b0858  8b0e                  mov       ecx,[esi]
  004b085a  f7c100000002          test      ecx,2000000h
  004b0860  7414                  je        short 004B0876h
  004b0862  f6c101                test      cl,1
  004b0865  750a                  jne       short 004B0871h
  004b0867  83e1fd                and       ecx,0FFFFFFFDh
  004b086a  885e43                mov       [esi+43h],bl
  004b086d  890e                  mov       [esi],ecx
  004b086f  eb05                  jmp       short 004B0876h

LAB_004b0871:
  004b0871  83e1fd                and       ecx,0FFFFFFFDh
  004b0874  890e                  mov       [esi],ecx

LAB_004b0876:
  004b0876  807e420c              cmp       byte [esi+42h],0Ch
  004b087a  756a                  jne       short 004B08E6h
  004b087c  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004b0881  7e63                  jle       short 004B08E6h
  004b0883  8d442430              lea       eax,[esp+30h]
  004b0887  53                    push      ebx
  004b0888  50                    push      eax
  004b0889  55                    push      ebp
  004b088a  56                    push      esi
  004b088b  e8a0930600            call      00519C30h
  004b0890  8bf8                  mov       edi,eax
  004b0892  a1b8e05d00            mov       eax,[5DE0B8h]
  004b0897  83c410                add       esp,10h
  004b089a  3bc3                  cmp       eax,ebx
  004b089c  7444                  je        short 004B08E2h
  004b089e  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004b08a3  7e3d                  jle       short 004B08E2h
  004b08a5  8b17                  mov       edx,[edi]
  004b08a7  57                    push      edi
  004b08a8  83ca40                or        edx,40h
  004b08ab  56                    push      esi
  004b08ac  66c787960000001000    mov       word [edi+96h],10h
  004b08b5  8917                  mov       [edi],edx
  004b08b7  e8c4920800            call      00539B80h
  004b08bc  83c408                add       esp,8
  004b08bf  53                    push      ebx
  004b08c0  57                    push      edi
  004b08c1  56                    push      esi
  004b08c2  e8590d0000            call      004B1620h
  004b08c7  83c40c                add       esp,0Ch
  004b08ca  83c71c                add       edi,1Ch
  004b08cd  57                    push      edi
  004b08ce  6800180000            push      1800h
  004b08d3  6800030000            push      300h
  004b08d8  6a03                  push      3
  004b08da  e841d20800            call      0053DB20h
  004b08df  83c410                add       esp,10h

LAB_004b08e2:
  004b08e2  8b442418              mov       eax,[esp+18h]

LAB_004b08e6:
  004b08e6  807e420d              cmp       byte [esi+42h],0Dh
  004b08ea  0f85c8000000          jne       near 004B09B8h
  004b08f0  8d4c243c              lea       ecx,[esp+3Ch]
  004b08f4  53                    push      ebx
  004b08f5  51                    push      ecx
  004b08f6  55                    push      ebp
  004b08f7  56                    push      esi
  004b08f8  e833930600            call      00519C30h
  004b08fd  8bf8                  mov       edi,eax
  004b08ff  a1b8e05d00            mov       eax,[5DE0B8h]
  004b0904  83c410                add       esp,10h
  004b0907  3bc3                  cmp       eax,ebx
  004b0909  0f84a5000000          je        near 004B09B4h
  004b090f  8b07                  mov       eax,[edi]
  004b0911  66c787960000001000    mov       word [edi+96h],10h
  004b091a  0c40                  or        al,40h
  004b091c  57                    push      edi
  004b091d  8907                  mov       [edi],eax
  004b091f  8b4c2424              mov       ecx,[esp+24h]
  004b0923  56                    push      esi
  004b0924  668b4130              mov       ax,[ecx+30h]
  004b0928  0fbfd0                movsx     edx,ax
  004b092b  66d1f8                sar       ax,1
  004b092e  89542470              mov       [esp+70h],edx
  004b0932  66894130              mov       [ecx+30h],ax
  004b0936  e845920800            call      00539B80h
  004b093b  8b442428              mov       eax,[esp+28h]
  004b093f  668b4c2470            mov       cx,[esp+70h]
  004b0944  83c408                add       esp,8
  004b0947  8d571c                lea       edx,[edi+1Ch]
  004b094a  66894830              mov       [eax+30h],cx
  004b094e  52                    push      edx
  004b094f  6800180000            push      1800h
  004b0954  6800030000            push      300h
  004b0959  6a03                  push      3
  004b095b  e8c0d10800            call      0053DB20h
  004b0960  8b07                  mov       eax,[edi]
  004b0962  83c410                add       esp,10h
  004b0965  a900000040            test      eax,40000000h
  004b096a  7548                  jne       short 004B09B4h
  004b096c  6aff                  push      0FFFFFFFFh
  004b096e  53                    push      ebx
  004b096f  6a39                  push      39h
  004b0971  56                    push      esi
  004b0972  e859a0fcff            call      0047A9D0h
  004b0977  8b17                  mov       edx,[edi]
  004b0979  83c410                add       esp,10h
  004b097c  83ca01                or        edx,1
  004b097f  8917                  mov       [edi],edx
  004b0981  0fbf4632              movsx     eax,word [esi+32h]
  004b0985  50                    push      eax
  004b0986  e891500900            call      00545A1Ch
  004b098b  8b4e5c                mov       ecx,[esi+5Ch]
  004b098e  c1f90d                sar       ecx,0Dh
  004b0991  0fafc1                imul      eax,ecx
  004b0994  894750                mov       [edi+50h],eax
  004b0997  0fbf5632              movsx     edx,word [esi+32h]
  004b099b  52                    push      edx
  004b099c  e875500900            call      00545A16h
  004b09a1  8b4e5c                mov       ecx,[esi+5Ch]
  004b09a4  c7475400800100        mov       dword [edi+54h],18000h
  004b09ab  c1f90d                sar       ecx,0Dh
  004b09ae  0fafc1                imul      eax,ecx
  004b09b1  894758                mov       [edi+58h],eax

LAB_004b09b4:
  004b09b4  8b442418              mov       eax,[esp+18h]

LAB_004b09b8:
  004b09b8  33c9                  xor       ecx,ecx
  004b09ba  8a4e42                mov       cl,[esi+42h]
  004b09bd  83f90e                cmp       ecx,0Eh
  004b09c0  0f87d6050000          ja        near 004B0F9Ch
  004b09c6  ff248de0154b00        jmp       dword [ecx*4+4B15E0h]
  004b09cd  66395e12              cmp       [esi+12h],bx
  004b09d1  751a                  jne       short 004B09EDh
  004b09d3  8b06                  mov       eax,[esi]
  004b09d5  66895e18              mov       [esi+18h],bx
  004b09d9  0d00000001            or        eax,1000000h
  004b09de  66895e1a              mov       [esi+1Ah],bx
  004b09e2  8906                  mov       [esi],eax
  004b09e4  66c746120100          mov       word [esi+12h],1
  004b09ea  895e5c                mov       [esi+5Ch],ebx

LAB_004b09ed:
  004b09ed  0fbf5610              movsx     edx,word [esi+10h]
  004b09f1  39542414              cmp       [esp+14h],edx
  004b09f5  7d08                  jge       short 004B09FFh
  004b09f7  885e43                mov       [esi+43h],bl
  004b09fa  e99d050000            jmp       004B0F9Ch

LAB_004b09ff:
  004b09ff  8a4643                mov       al,[esi+43h]
  004b0a02  3ac3                  cmp       al,bl
  004b0a04  0f848f050000          je        near 004B0F99h
  004b0a0a  fec8                  dec       al
  004b0a0c  884643                mov       [esi+43h],al
  004b0a0f  e985050000            jmp       004B0F99h
  004b0a14  66395e12              cmp       [esi+12h],bx
  004b0a18  751b                  jne       short 004B0A35h
  004b0a1a  8b0e                  mov       ecx,[esi]
  004b0a1c  b801000000            mov       eax,1
  004b0a21  81c900000001          or        ecx,1000000h
  004b0a27  66894618              mov       [esi+18h],ax
  004b0a2b  66895e1a              mov       [esi+1Ah],bx
  004b0a2f  890e                  mov       [esi],ecx
  004b0a31  66894612              mov       [esi+12h],ax

LAB_004b0a35:
  004b0a35  8b44241c              mov       eax,[esp+1Ch]
  004b0a39  8d88c8000000          lea       ecx,[eax+0C8h]
  004b0a3f  81e1ff0f0000          and       ecx,0FFFh
  004b0a45  81f990010000          cmp       ecx,190h
  004b0a4b  7f08                  jg        short 004B0A55h
  004b0a4d  885e43                mov       [esi+43h],bl
  004b0a50  e944050000            jmp       004B0F99h

LAB_004b0a55:
  004b0a55  25ff0f0000            and       eax,0FFFh
  004b0a5a  3d00080000            cmp       eax,800h
  004b0a5f  7d10                  jge       short 004B0A71h
  004b0a61  8b542410              mov       edx,[esp+10h]
  004b0a65  8d4250                lea       eax,[edx+50h]
  004b0a68  66014632              add       [esi+32h],ax
  004b0a6c  e928050000            jmp       004B0F99h

LAB_004b0a71:
  004b0a71  8b4c2410              mov       ecx,[esp+10h]
  004b0a75  b8b0ffffff            mov       eax,0FFFFFFB0h
  004b0a7a  2bc1                  sub       eax,ecx
  004b0a7c  66014632              add       [esi+32h],ax
  004b0a80  e914050000            jmp       004B0F99h
  004b0a85  66395e12              cmp       [esi+12h],bx
  004b0a89  8b4c2410              mov       ecx,[esp+10h]
  004b0a8d  b801000000            mov       eax,1
  004b0a92  7525                  jne       short 004B0AB9h
  004b0a94  8b16                  mov       edx,[esi]
  004b0a96  66894618              mov       [esi+18h],ax
  004b0a9a  81ca00000001          or        edx,1000000h
  004b0aa0  66895e1a              mov       [esi+1Ah],bx
  004b0aa4  8916                  mov       [esi],edx
  004b0aa6  8d14c9                lea       edx,[ecx+ecx*8]
  004b0aa9  c1e204                shl       edx,4
  004b0aac  81c200500000          add       edx,5000h
  004b0ab2  66894612              mov       [esi+12h],ax
  004b0ab6  89565c                mov       [esi+5Ch],edx

LAB_004b0ab9:
  004b0ab9  66394612              cmp       [esi+12h],ax
  004b0abd  0f85d9040000          jne       near 004B0F9Ch
  004b0ac3  0fbf4610              movsx     eax,word [esi+10h]
  004b0ac7  39442414              cmp       [esp+14h],eax
  004b0acb  7d08                  jge       short 004B0AD5h
  004b0acd  885e43                mov       [esi+43h],bl
  004b0ad0  e9c4040000            jmp       004B0F99h

LAB_004b0ad5:
  004b0ad5  8a4643                mov       al,[esi+43h]
  004b0ad8  3ac3                  cmp       al,bl
  004b0ada  7405                  je        short 004B0AE1h
  004b0adc  fec8                  dec       al
  004b0ade  884643                mov       [esi+43h],al

LAB_004b0ae1:
  004b0ae1  8b44241c              mov       eax,[esp+1Ch]
  004b0ae5  8d9000010000          lea       edx,[eax+100h]
  004b0aeb  81e2ff0f0000          and       edx,0FFFh
  004b0af1  81fa00020000          cmp       edx,200h
  004b0af7  0f8e9f040000          jle       near 004B0F9Ch
  004b0afd  25ff0f0000            and       eax,0FFFh
  004b0b02  3d00080000            cmp       eax,800h
  004b0b07  7d10                  jge       short 004B0B19h
  004b0b09  8d840980000000        lea       eax,[ecx+ecx+80h]
  004b0b10  66014632              add       [esi+32h],ax
  004b0b14  e983040000            jmp       004B0F9Ch

LAB_004b0b19:
  004b0b19  03c9                  add       ecx,ecx
  004b0b1b  b880ffffff            mov       eax,0FFFFFF80h
  004b0b20  2bc1                  sub       eax,ecx
  004b0b22  66014632              add       [esi+32h],ax
  004b0b26  e971040000            jmp       004B0F9Ch
  004b0b2b  66837e1201            cmp       word [esi+12h],1
  004b0b30  0f859a000000          jne       near 004B0BD0h
  004b0b36  8a4e43                mov       cl,[esi+43h]
  004b0b39  3acb                  cmp       cl,bl
  004b0b3b  7405                  je        short 004B0B42h
  004b0b3d  fec9                  dec       cl
  004b0b3f  884e43                mov       [esi+43h],cl

LAB_004b0b42:
  004b0b42  8b4c2410              mov       ecx,[esp+10h]
  004b0b46  8d14c9                lea       edx,[ecx+ecx*8]
  004b0b49  c1e204                shl       edx,4
  004b0b4c  8d8a00500000          lea       ecx,[edx+5000h]
  004b0b52  0fafc1                imul      eax,ecx
  004b0b55  c1f80c                sar       eax,0Ch
  004b0b58  8bf8                  mov       edi,eax
  004b0b5a  894e5c                mov       [esi+5Ch],ecx
  004b0b5d  0fbf4610              movsx     eax,word [esi+10h]
  004b0b61  50                    push      eax
  004b0b62  e8b54e0900            call      00545A1Ch
  004b0b67  0fafc7                imul      eax,edi
  004b0b6a  8b4d00                mov       ecx,[ebp]
  004b0b6d  c1f80c                sar       eax,0Ch
  004b0b70  03c8                  add       ecx,eax
  004b0b72  894d00                mov       [ebp],ecx
  004b0b75  0fbf4e10              movsx     ecx,word [esi+10h]
  004b0b79  51                    push      ecx
  004b0b7a  e8974e0900            call      00545A16h
  004b0b7f  0fafc7                imul      eax,edi
  004b0b82  8b6e24                mov       ebp,[esi+24h]
  004b0b85  895e5c                mov       [esi+5Ch],ebx
  004b0b88  c1f80c                sar       eax,0Ch
  004b0b8b  03e8                  add       ebp,eax
  004b0b8d  8b44241c              mov       eax,[esp+1Ch]
  004b0b91  896e24                mov       [esi+24h],ebp
  004b0b94  8d9000010000          lea       edx,[eax+100h]
  004b0b9a  81e2ff0f0000          and       edx,0FFFh
  004b0ba0  81fa00020000          cmp       edx,200h
  004b0ba6  7e28                  jle       short 004B0BD0h
  004b0ba8  25ff0f0000            and       eax,0FFFh
  004b0bad  3d00080000            cmp       eax,800h
  004b0bb2  7d0a                  jge       short 004B0BBEh
  004b0bb4  8b442410              mov       eax,[esp+10h]
  004b0bb8  8d440032              lea       eax,[eax+eax+32h]
  004b0bbc  eb0e                  jmp       short 004B0BCCh

LAB_004b0bbe:
  004b0bbe  8b4c2410              mov       ecx,[esp+10h]
  004b0bc2  b8ceffffff            mov       eax,0FFFFFFCEh
  004b0bc7  8d1409                lea       edx,[ecx+ecx]
  004b0bca  2bc2                  sub       eax,edx

LAB_004b0bcc:
  004b0bcc  66014632              add       [esi+32h],ax

LAB_004b0bd0:
  004b0bd0  66395e12              cmp       [esi+12h],bx
  004b0bd4  0f85c2030000          jne       near 004B0F9Ch
  004b0bda  8b0e                  mov       ecx,[esi]
  004b0bdc  b801000000            mov       eax,1
  004b0be1  81c900000001          or        ecx,1000000h
  004b0be7  66894618              mov       [esi+18h],ax
  004b0beb  66895e1a              mov       [esi+1Ah],bx
  004b0bef  890e                  mov       [esi],ecx
  004b0bf1  66894612              mov       [esi+12h],ax
  004b0bf5  e9a2030000            jmp       004B0F9Ch
  004b0bfa  668b4e12              mov       cx,[esi+12h]
  004b0bfe  6683f901              cmp       cx,1
  004b0c02  7514                  jne       short 004B0C18h
  004b0c04  8a4643                mov       al,[esi+43h]
  004b0c07  3ac3                  cmp       al,bl
  004b0c09  740d                  je        short 004B0C18h
  004b0c0b  f70600000002          test      dword [esi],2000000h
  004b0c11  7405                  je        short 004B0C18h
  004b0c13  fec8                  dec       al
  004b0c15  884643                mov       [esi+43h],al

LAB_004b0c18:
  004b0c18  663bcb                cmp       cx,bx
  004b0c1b  0f857b030000          jne       near 004B0F9Ch
  004b0c21  8b06                  mov       eax,[esi]
  004b0c23  66c746120100          mov       word [esi+12h],1
  004b0c29  25fffffffe            and       eax,0FEFFFFFFh
  004b0c2e  66c746180600          mov       word [esi+18h],6
  004b0c34  66895e1a              mov       [esi+1Ah],bx
  004b0c38  895e5c                mov       [esi+5Ch],ebx
  004b0c3b  8906                  mov       [esi],eax
  004b0c3d  e95a030000            jmp       004B0F9Ch
  004b0c42  66395e12              cmp       [esi+12h],bx
  004b0c46  757f                  jne       short 004B0CC7h
  004b0c48  8b0e                  mov       ecx,[esi]
  004b0c4a  f6c101                test      cl,1
  004b0c4d  7405                  je        short 004B0C54h
  004b0c4f  885e43                mov       [esi+43h],bl
  004b0c52  eb73                  jmp       short 004B0CC7h

LAB_004b0c54:
  004b0c54  81e1fffffffe          and       ecx,0FEFFFFFFh
  004b0c5a  bd01000000            mov       ebp,1
  004b0c5f  890e                  mov       [esi],ecx
  004b0c61  8b4c2410              mov       ecx,[esp+10h]
  004b0c65  81c100010000          add       ecx,100h
  004b0c6b  66896e18              mov       [esi+18h],bp
  004b0c6f  c1e106                shl       ecx,6
  004b0c72  0fafc1                imul      eax,ecx
  004b0c75  0fbf5610              movsx     edx,word [esi+10h]
  004b0c79  c1f80c                sar       eax,0Ch
  004b0c7c  52                    push      edx
  004b0c7d  66895e1a              mov       [esi+1Ah],bx
  004b0c81  894e5c                mov       [esi+5Ch],ecx
  004b0c84  8bf8                  mov       edi,eax
  004b0c86  e8914d0900            call      00545A1Ch
  004b0c8b  0fafc7                imul      eax,edi
  004b0c8e  8b5650                mov       edx,[esi+50h]
  004b0c91  c1f80c                sar       eax,0Ch
  004b0c94  03d0                  add       edx,eax
  004b0c96  0fbf4610              movsx     eax,word [esi+10h]
  004b0c9a  50                    push      eax
  004b0c9b  895650                mov       [esi+50h],edx
  004b0c9e  e8734d0900            call      00545A16h
  004b0ca3  0fafc7                imul      eax,edi
  004b0ca6  8b5658                mov       edx,[esi+58h]
  004b0ca9  8b4e54                mov       ecx,[esi+54h]
  004b0cac  c1f80c                sar       eax,0Ch
  004b0caf  03d0                  add       edx,eax
  004b0cb1  8b06                  mov       eax,[esi]
  004b0cb3  81c100e00100          add       ecx,1E000h
  004b0cb9  0bc5                  or        eax,ebp
  004b0cbb  895658                mov       [esi+58h],edx
  004b0cbe  894e54                mov       [esi+54h],ecx
  004b0cc1  8906                  mov       [esi],eax
  004b0cc3  66896e12              mov       [esi+12h],bp

LAB_004b0cc7:
  004b0cc7  f60601                test      byte [esi],1
  004b0cca  0f85cc020000          jne       near 004B0F9Ch
  004b0cd0  885e43                mov       [esi+43h],bl
  004b0cd3  e9c4020000            jmp       004B0F9Ch
  004b0cd8  668b4e12              mov       cx,[esi+12h]
  004b0cdc  ba01000000            mov       edx,1
  004b0ce1  663bca                cmp       cx,dx
  004b0ce4  751b                  jne       short 004B0D01h
  004b0ce6  0fbf4610              movsx     eax,word [esi+10h]
  004b0cea  39442414              cmp       [esp+14h],eax
  004b0cee  7d05                  jge       short 004B0CF5h
  004b0cf0  885e43                mov       [esi+43h],bl
  004b0cf3  eb0c                  jmp       short 004B0D01h

LAB_004b0cf5:
  004b0cf5  8a4643                mov       al,[esi+43h]
  004b0cf8  3ac3                  cmp       al,bl
  004b0cfa  7405                  je        short 004B0D01h
  004b0cfc  fec8                  dec       al
  004b0cfe  884643                mov       [esi+43h],al

LAB_004b0d01:
  004b0d01  663bcb                cmp       cx,bx
  004b0d04  0f8592020000          jne       near 004B0F9Ch
  004b0d0a  8b442410              mov       eax,[esp+10h]
  004b0d0e  8b3e                  mov       edi,[esi]
  004b0d10  81cf00000001          or        edi,1000000h
  004b0d16  66895618              mov       [esi+18h],dx
  004b0d1a  8d0cc0                lea       ecx,[eax+eax*8]
  004b0d1d  66895e1a              mov       [esi+1Ah],bx
  004b0d21  c1e104                shl       ecx,4
  004b0d24  81c100500000          add       ecx,5000h
  004b0d2a  893e                  mov       [esi],edi
  004b0d2c  66895612              mov       [esi+12h],dx
  004b0d30  894e5c                mov       [esi+5Ch],ecx
  004b0d33  e964020000            jmp       004B0F9Ch
  004b0d38  bf01000000            mov       edi,1
  004b0d3d  66397e12              cmp       [esi+12h],di
  004b0d41  7515                  jne       short 004B0D58h
  004b0d43  66837e1a08            cmp       word [esi+1Ah],8
  004b0d48  750e                  jne       short 004B0D58h
  004b0d4a  6aff                  push      0FFFFFFFFh
  004b0d4c  53                    push      ebx
  004b0d4d  6a36                  push      36h
  004b0d4f  56                    push      esi
  004b0d50  e87b9cfcff            call      0047A9D0h
  004b0d55  83c410                add       esp,10h

LAB_004b0d58:
  004b0d58  8b06                  mov       eax,[esi]
  004b0d5a  a900000002            test      eax,2000000h
  004b0d5f  7403                  je        short 004B0D64h
  004b0d61  885e43                mov       [esi+43h],bl

LAB_004b0d64:
  004b0d64  66395e12              cmp       [esi+12h],bx
  004b0d68  0f852e020000          jne       near 004B0F9Ch
  004b0d6e  25fffffffe            and       eax,0FEFFFFFFh
  004b0d73  66897e12              mov       [esi+12h],di
  004b0d77  8906                  mov       [esi],eax
  004b0d79  8b442410              mov       eax,[esp+10h]
  004b0d7d  83f81e                cmp       eax,1Eh
  004b0d80  66c746180500          mov       word [esi+18h],5
  004b0d86  66895e1a              mov       [esi+1Ah],bx
  004b0d8a  7d03                  jge       short 004B0D8Fh
  004b0d8c  895e5c                mov       [esi+5Ch],ebx

LAB_004b0d8f:
  004b0d8f  66395e16              cmp       [esi+16h],bx
  004b0d93  0f8503020000          jne       near 004B0F9Ch
  004b0d99  885e43                mov       [esi+43h],bl
  004b0d9c  e9fb010000            jmp       004B0F9Ch
  004b0da1  668b4e12              mov       cx,[esi+12h]
  004b0da5  6683f901              cmp       cx,1
  004b0da9  7510                  jne       short 004B0DBBh
  004b0dab  8b06                  mov       eax,[esi]
  004b0dad  a900000002            test      eax,2000000h
  004b0db2  7407                  je        short 004B0DBBh
  004b0db4  24fd                  and       al,0FDh
  004b0db6  885e43                mov       [esi+43h],bl
  004b0db9  8906                  mov       [esi],eax

LAB_004b0dbb:
  004b0dbb  663bcb                cmp       cx,bx
  004b0dbe  0f85d8010000          jne       near 004B0F9Ch
  004b0dc4  8b06                  mov       eax,[esi]
  004b0dc6  66c746120100          mov       word [esi+12h],1
  004b0dcc  25fffffffe            and       eax,0FEFFFFFFh
  004b0dd1  8906                  mov       [esi],eax
  004b0dd3  e9c4010000            jmp       004B0F9Ch
  004b0dd8  66395e12              cmp       [esi+12h],bx
  004b0ddc  7520                  jne       short 004B0DFEh
  004b0dde  8b16                  mov       edx,[esi]
  004b0de0  66c746120100          mov       word [esi+12h],1
  004b0de6  81e2fffffffe          and       edx,0FEFFFFFFh
  004b0dec  66c746180300          mov       word [esi+18h],3
  004b0df2  83ca02                or        edx,2
  004b0df5  66895e1a              mov       [esi+1Ah],bx
  004b0df9  8916                  mov       [esi],edx
  004b0dfb  895e5c                mov       [esi+5Ch],ebx

LAB_004b0dfe:
  004b0dfe  66837e1201            cmp       word [esi+12h],1
  004b0e03  0f8593010000          jne       near 004B0F9Ch
  004b0e09  8b06                  mov       eax,[esi]
  004b0e0b  a900000002            test      eax,2000000h
  004b0e10  0f8486010000          je        near 004B0F9Ch
  004b0e16  24fd                  and       al,0FDh
  004b0e18  885e43                mov       [esi+43h],bl
  004b0e1b  8906                  mov       [esi],eax
  004b0e1d  e97a010000            jmp       004B0F9Ch
  004b0e22  668b5612              mov       dx,[esi+12h]
  004b0e26  8b4c241c              mov       ecx,[esp+1Ch]
  004b0e2a  8b7c2410              mov       edi,[esp+10h]
  004b0e2e  6683fa02              cmp       dx,2
  004b0e32  7555                  jne       short 004B0E89h
  004b0e34  8b465c                mov       eax,[esi+5Ch]
  004b0e37  8be8                  mov       ebp,eax
  004b0e39  c1fd04                sar       ebp,4
  004b0e3c  2bc5                  sub       eax,ebp
  004b0e3e  3d00500000            cmp       eax,5000h
  004b0e43  89465c                mov       [esi+5Ch],eax
  004b0e46  7d0c                  jge       short 004B0E54h
  004b0e48  8b06                  mov       eax,[esi]
  004b0e4a  885e43                mov       [esi+43h],bl
  004b0e4d  24fd                  and       al,0FDh
  004b0e4f  895e5c                mov       [esi+5Ch],ebx
  004b0e52  8906                  mov       [esi],eax

LAB_004b0e54:
  004b0e54  83ff1e                cmp       edi,1Eh
  004b0e57  7e30                  jle       short 004B0E89h
  004b0e59  8d8100010000          lea       eax,[ecx+100h]
  004b0e5f  25ff0f0000            and       eax,0FFFh
  004b0e64  3d00020000            cmp       eax,200h
  004b0e69  7e1e                  jle       short 004B0E89h
  004b0e6b  8bc1                  mov       eax,ecx
  004b0e6d  25ff0f0000            and       eax,0FFFh
  004b0e72  3d00080000            cmp       eax,800h
  004b0e77  7d05                  jge       short 004B0E7Eh
  004b0e79  8d4732                lea       eax,[edi+32h]
  004b0e7c  eb07                  jmp       short 004B0E85h

LAB_004b0e7e:
  004b0e7e  b8ceffffff            mov       eax,0FFFFFFCEh
  004b0e83  2bc7                  sub       eax,edi

LAB_004b0e85:
  004b0e85  66014632              add       [esi+32h],ax

LAB_004b0e89:
  004b0e89  6683fa01              cmp       dx,1
  004b0e8d  7559                  jne       short 004B0EE8h
  004b0e8f  8d9100010000          lea       edx,[ecx+100h]
  004b0e95  81e2ff0f0000          and       edx,0FFFh
  004b0e9b  81fa00020000          cmp       edx,200h
  004b0ea1  7e22                  jle       short 004B0EC5h
  004b0ea3  81e1ff0f0000          and       ecx,0FFFh
  004b0ea9  81f900080000          cmp       ecx,800h
  004b0eaf  7d06                  jge       short 004B0EB7h
  004b0eb1  8d443f32              lea       eax,[edi+edi+32h]
  004b0eb5  eb0a                  jmp       short 004B0EC1h

LAB_004b0eb7:
  004b0eb7  8d0c3f                lea       ecx,[edi+edi]
  004b0eba  b8ceffffff            mov       eax,0FFFFFFCEh
  004b0ebf  2bc1                  sub       eax,ecx

LAB_004b0ec1:
  004b0ec1  66014632              add       [esi+32h],ax

LAB_004b0ec5:
  004b0ec5  66837e1a06            cmp       word [esi+1Ah],6
  004b0eca  7c1c                  jl        short 004B0EE8h
  004b0ecc  0fbf4616              movsx     eax,word [esi+16h]
  004b0ed0  83c006                add       eax,6
  004b0ed3  66c746120200          mov       word [esi+12h],2
  004b0ed9  8d1480                lea       edx,[eax+eax*4]
  004b0edc  8b06                  mov       eax,[esi]
  004b0ede  c1e20c                shl       edx,0Ch
  004b0ee1  0c02                  or        al,2
  004b0ee3  89565c                mov       [esi+5Ch],edx
  004b0ee6  8906                  mov       [esi],eax

LAB_004b0ee8:
  004b0ee8  66395e12              cmp       [esi+12h],bx
  004b0eec  0f85aa000000          jne       near 004B0F9Ch
  004b0ef2  8b06                  mov       eax,[esi]
  004b0ef4  66c746120100          mov       word [esi+12h],1
  004b0efa  25fffffffe            and       eax,0FEFFFFFFh
  004b0eff  66c746180400          mov       word [esi+18h],4
  004b0f05  66895e1a              mov       [esi+1Ah],bx
  004b0f09  8906                  mov       [esi],eax
  004b0f0b  e989000000            jmp       004B0F99h
  004b0f10  b801000000            mov       eax,1
  004b0f15  66394612              cmp       [esi+12h],ax
  004b0f19  7562                  jne       short 004B0F7Dh
  004b0f1b  8a4643                mov       al,[esi+43h]
  004b0f1e  3ac3                  cmp       al,bl
  004b0f20  7405                  je        short 004B0F27h
  004b0f22  fec8                  dec       al
  004b0f24  884643                mov       [esi+43h],al

LAB_004b0f27:
  004b0f27  0fbf4632              movsx     eax,word [esi+32h]
  004b0f2b  0500080000            add       eax,800h
  004b0f30  50                    push      eax
  004b0f31  e8e64a0900            call      00545A1Ch
  004b0f36  0fbf4e32              movsx     ecx,word [esi+32h]
  004b0f3a  81c100080000          add       ecx,800h
  004b0f40  8bf8                  mov       edi,eax
  004b0f42  51                    push      ecx
  004b0f43  e8ce4a0900            call      00545A16h
  004b0f48  8b542410              mov       edx,[esp+10h]
  004b0f4c  8d4a60                lea       ecx,[edx+60h]
  004b0f4f  0faf4c2418            imul      ecx,[esp+18h]
  004b0f54  c1e108                shl       ecx,8
  004b0f57  c1f90c                sar       ecx,0Ch
  004b0f5a  8bd1                  mov       edx,ecx
  004b0f5c  0fafc8                imul      ecx,eax
  004b0f5f  0fafd7                imul      edx,edi
  004b0f62  8b7d00                mov       edi,[ebp]
  004b0f65  8b4624                mov       eax,[esi+24h]
  004b0f68  c1fa0c                sar       edx,0Ch
  004b0f6b  c1f90c                sar       ecx,0Ch
  004b0f6e  03fa                  add       edi,edx
  004b0f70  03c1                  add       eax,ecx
  004b0f72  894624                mov       [esi+24h],eax
  004b0f75  897d00                mov       [ebp],edi
  004b0f78  b801000000            mov       eax,1

LAB_004b0f7d:
  004b0f7d  66395e12              cmp       [esi+12h],bx
  004b0f81  7519                  jne       short 004B0F9Ch
  004b0f83  8b0e                  mov       ecx,[esi]
  004b0f85  66894618              mov       [esi+18h],ax
  004b0f89  81c900000001          or        ecx,1000000h
  004b0f8f  66895e1a              mov       [esi+1Ah],bx
  004b0f93  890e                  mov       [esi],ecx
  004b0f95  66894612              mov       [esi+12h],ax

LAB_004b0f99:
  004b0f99  895e5c                mov       [esi+5Ch],ebx

LAB_004b0f9c:
  004b0f9c  0fbf4632              movsx     eax,word [esi+32h]
  004b0fa0  50                    push      eax
  004b0fa1  e8764a0900            call      00545A1Ch
  004b0fa6  0fbf4e32              movsx     ecx,word [esi+32h]
  004b0faa  51                    push      ecx
  004b0fab  89842480000000        mov       [esp+80h],eax
  004b0fb2  e85f4a0900            call      00545A16h
  004b0fb7  8b5650                mov       edx,[esi+50h]
  004b0fba  89842484000000        mov       [esp+84h],eax
  004b0fc1  8bc2                  mov       eax,edx
  004b0fc3  8b6e54                mov       ebp,[esi+54h]
  004b0fc6  c1f803                sar       eax,3
  004b0fc9  8bfa                  mov       edi,edx
  004b0fcb  8b4c2418              mov       ecx,[esp+18h]
  004b0fcf  2bf8                  sub       edi,eax
  004b0fd1  0faf4e5c              imul      ecx,[esi+5Ch]
  004b0fd5  c1fa08                sar       edx,8
  004b0fd8  2bfa                  sub       edi,edx
  004b0fda  8bc5                  mov       eax,ebp
  004b0fdc  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004b0fe3  8bda                  mov       ebx,edx
  004b0fe5  897e50                mov       [esi+50h],edi
  004b0fe8  c1f808                sar       eax,8
  004b0feb  c1fb08                sar       ebx,8
  004b0fee  2bd8                  sub       ebx,eax
  004b0ff0  56                    push      esi
  004b0ff1  2bda                  sub       ebx,edx
  004b0ff3  8b5658                mov       edx,[esi+58h]
  004b0ff6  c1f90c                sar       ecx,0Ch
  004b0ff9  8d042b                lea       eax,[ebx+ebp]
  004b0ffc  8bda                  mov       ebx,edx
  004b0ffe  c1fb08                sar       ebx,8
  004b1001  8bea                  mov       ebp,edx
  004b1003  894654                mov       [esi+54h],eax
  004b1006  2beb                  sub       ebp,ebx
  004b1008  8b5e1c                mov       ebx,[esi+1Ch]
  004b100b  c1fa03                sar       edx,3
  004b100e  2bea                  sub       ebp,edx
  004b1010  8bd1                  mov       edx,ecx
  004b1012  0faf942480000000      imul      edx,[esp+80h]
  004b101a  0faf8c2488000000      imul      ecx,[esp+88h]
  004b1022  c1fa0c                sar       edx,0Ch
  004b1025  c1f90c                sar       ecx,0Ch
  004b1028  896e58                mov       [esi+58h],ebp
  004b102b  03d7                  add       edx,edi
  004b102d  8b7e20                mov       edi,[esi+20h]
  004b1030  03cd                  add       ecx,ebp
  004b1032  8b6e24                mov       ebp,[esi+24h]
  004b1035  03da                  add       ebx,edx
  004b1037  03f8                  add       edi,eax
  004b1039  03e9                  add       ebp,ecx
  004b103b  895e1c                mov       [esi+1Ch],ebx
  004b103e  897e20                mov       [esi+20h],edi
  004b1041  896e24                mov       [esi+24h],ebp
  004b1044  e8f7280800            call      00533940h
  004b1049  8b9690000000          mov       edx,[esi+90h]
  004b104f  8b4660                mov       eax,[esi+60h]
  004b1052  8b4e20                mov       ecx,[esi+20h]
  004b1055  83c404                add       esp,4
  004b1058  80e6fc                and       dh,0FCh
  004b105b  3bc8                  cmp       ecx,eax
  004b105d  899690000000          mov       [esi+90h],edx
  004b1063  7f2a                  jg        short 004B108Fh
  004b1065  f6c680                test      dh,80h
  004b1068  7525                  jne       short 004B108Fh
  004b106a  8b0e                  mov       ecx,[esi]
  004b106c  894620                mov       [esi+20h],eax
  004b106f  f6c101                test      cl,1
  004b1072  740f                  je        short 004B1083h
  004b1074  83e1fe                and       ecx,0FFFFFFFEh
  004b1077  33ff                  xor       edi,edi
  004b1079  890e                  mov       [esi],ecx
  004b107b  897e54                mov       [esi+54h],edi
  004b107e  e926010000            jmp       004B11A9h

LAB_004b1083:
  004b1083  83e1fe                and       ecx,0FFFFFFFEh
  004b1086  33ff                  xor       edi,edi
  004b1088  890e                  mov       [esi],ecx
  004b108a  897e54                mov       [esi+54h],edi
  004b108d  eb02                  jmp       short 004B1091h

LAB_004b108f:
  004b108f  33ff                  xor       edi,edi

LAB_004b1091:
  004b1091  8b0e                  mov       ecx,[esi]
  004b1093  f6c101                test      cl,1
  004b1096  750a                  jne       short 004B10A2h
  004b1098  394620                cmp       [esi+20h],eax
  004b109b  7e05                  jle       short 004B10A2h
  004b109d  83c901                or        ecx,1
  004b10a0  890e                  mov       [esi],ecx

LAB_004b10a2:
  004b10a2  f7c282ad0020          test      edx,2000AD82h
  004b10a8  0f84fb000000          je        near 004B11A9h
  004b10ae  f60601                test      byte [esi],1
  004b10b1  0f85c9000000          jne       near 004B1180h
  004b10b7  8a4e42                mov       cl,[esi+42h]
  004b10ba  b007                  mov       al,7
  004b10bc  3ac8                  cmp       cl,al
  004b10be  7420                  je        short 004B10E0h
  004b10c0  884642                mov       [esi+42h],al
  004b10c3  e878600900            call      00547140h
  004b10c8  99                    cdq
  004b10c9  b928000000            mov       ecx,28h
  004b10ce  66897e12              mov       [esi+12h],di
  004b10d2  f7f9                  idiv      ecx
  004b10d4  66c746105000          mov       word [esi+10h],50h
  004b10da  80c214                add       dl,14h
  004b10dd  885643                mov       [esi+43h],dl

LAB_004b10e0:
  004b10e0  8b16                  mov       edx,[esi]
  004b10e2  8b442428              mov       eax,[esp+28h]
  004b10e6  8b4c242c              mov       ecx,[esp+2Ch]
  004b10ea  83e2fd                and       edx,0FFFFFFFDh
  004b10ed  8916                  mov       [esi],edx
  004b10ef  8b542424              mov       edx,[esp+24h]
  004b10f3  894620                mov       [esi+20h],eax
  004b10f6  8b442464              mov       eax,[esp+64h]
  004b10fa  89561c                mov       [esi+1Ch],edx
  004b10fd  8b542470              mov       edx,[esp+70h]
  004b1101  25ffff0000            and       eax,0FFFFh
  004b1106  894e24                mov       [esi+24h],ecx
  004b1109  8b4c2448              mov       ecx,[esp+48h]
  004b110d  89567c                mov       [esi+7Ch],edx
  004b1110  8b542460              mov       edx,[esp+60h]
  004b1114  898690000000          mov       [esi+90h],eax
  004b111a  8b442450              mov       eax,[esp+50h]
  004b111e  898e84000000          mov       [esi+84h],ecx
  004b1124  8b4c246c              mov       ecx,[esp+6Ch]
  004b1128  898680000000          mov       [esi+80h],eax
  004b112e  8b44245c              mov       eax,[esp+5Ch]
  004b1132  899688000000          mov       [esi+88h],edx
  004b1138  8b542454              mov       edx,[esp+54h]
  004b113c  6882ad0020            push      2000AD82h
  004b1141  56                    push      esi
  004b1142  89868c000000          mov       [esi+8Ch],eax
  004b1148  894e64                mov       [esi+64h],ecx
  004b114b  895660                mov       [esi+60h],edx
  004b114e  e80d4d0200            call      004D5E60h
  004b1153  83c408                add       esp,8
  004b1156  663bc7                cmp       ax,di
  004b1159  7515                  jne       short 004B1170h
  004b115b  e8e05f0900            call      00547140h
  004b1160  99                    cdq
  004b1161  33c2                  xor       eax,edx
  004b1163  2bc2                  sub       eax,edx
  004b1165  83e001                and       eax,1
  004b1168  33c2                  xor       eax,edx
  004b116a  2bc2                  sub       eax,edx
  004b116c  8d4400ff              lea       eax,[eax+eax-1]

LAB_004b1170:
  004b1170  8b4c2410              mov       ecx,[esp+10h]
  004b1174  83c140                add       ecx,40h
  004b1177  0fafc8                imul      ecx,eax
  004b117a  66014e32              add       [esi+32h],cx
  004b117e  eb29                  jmp       short 004B11A9h

LAB_004b1180:
  004b1180  8b542424              mov       edx,[esp+24h]
  004b1184  8b44242c              mov       eax,[esp+2Ch]
  004b1188  897e5c                mov       [esi+5Ch],edi
  004b118b  897e50                mov       [esi+50h],edi
  004b118e  897e58                mov       [esi+58h],edi
  004b1191  89561c                mov       [esi+1Ch],edx
  004b1194  894624                mov       [esi+24h],eax
  004b1197  c6464200              mov       byte [esi+42h],0
  004b119b  c6464328              mov       byte [esi+43h],28h
  004b119f  66897e12              mov       [esi+12h],di
  004b11a3  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004b11a9:
  004b11a9  6aff                  push      0FFFFFFFFh
  004b11ab  6aff                  push      0FFFFFFFFh
  004b11ad  6aff                  push      0FFFFFFFFh
  004b11af  56                    push      esi
  004b11b0  e85b260100            call      004C3810h
  004b11b5  8b442428              mov       eax,[esp+28h]
  004b11b9  83c410                add       esp,10h
  004b11bc  3bc7                  cmp       eax,edi
  004b11be  7517                  jne       short 004B11D7h
  004b11c0  668b8c2492000000      mov       cx,[esp+92h]
  004b11c8  66894e32              mov       [esi+32h],cx
  004b11cc  5f                    pop       edi
  004b11cd  5e                    pop       esi
  004b11ce  5d                    pop       ebp
  004b11cf  5b                    pop       ebx
  004b11d0  81c494000000          add       esp,94h
  004b11d6  c3                    ret

LAB_004b11d7:
  004b11d7  8a4643                mov       al,[esi+43h]
  004b11da  84c0                  test      al,al
  004b11dc  0f85f2030000          jne       near 004B15D4h
  004b11e2  8b5c244c              mov       ebx,[esp+4Ch]

LAB_004b11e6:
  004b11e6  8b466c                mov       eax,[esi+6Ch]
  004b11e9  8b561c                mov       edx,[esi+1Ch]
  004b11ec  8b781c                mov       edi,[eax+1Ch]
  004b11ef  8b6824                mov       ebp,[eax+24h]
  004b11f2  8b4624                mov       eax,[esi+24h]
  004b11f5  2bfa                  sub       edi,edx
  004b11f7  2be8                  sub       ebp,eax
  004b11f9  c1ff0c                sar       edi,0Ch
  004b11fc  c1fd0c                sar       ebp,0Ch
  004b11ff  8bd7                  mov       edx,edi
  004b1201  8bc5                  mov       eax,ebp
  004b1203  0fafd7                imul      edx,edi
  004b1206  0fafc5                imul      eax,ebp
  004b1209  03d0                  add       edx,eax
  004b120b  52                    push      edx
  004b120c  e8ff470900            call      00545A10h
  004b1211  55                    push      ebp
  004b1212  57                    push      edi
  004b1213  8944241c              mov       [esp+1Ch],eax
  004b1217  e806480900            call      00545A22h
  004b121c  668b4e32              mov       cx,[esi+32h]
  004b1220  66c746120000          mov       word [esi+12h],0
  004b1226  2bc1                  sub       eax,ecx
  004b1228  25ff0f0000            and       eax,0FFFh
  004b122d  8944241c              mov       [esp+1Ch],eax
  004b1231  8b442414              mov       eax,[esp+14h]
  004b1235  3dfa000000            cmp       eax,0FAh
  004b123a  0f8d64010000          jge       near 004B13A4h
  004b1240  e8fb5e0900            call      00547140h
  004b1245  99                    cdq
  004b1246  b964000000            mov       ecx,64h
  004b124b  f7f9                  idiv      ecx
  004b124d  8bfa                  mov       edi,edx
  004b124f  83ff32                cmp       edi,32h
  004b1252  0f8d80000000          jge       near 004B12D8h
  004b1258  8b54241c              mov       edx,[esp+1Ch]
  004b125c  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004b1263  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004b126a  25ff0f0000            and       eax,0FFFh
  004b126f  3bc1                  cmp       eax,ecx
  004b1271  7f58                  jg        short 004B12CBh
  004b1273  e8c85e0900            call      00547140h
  004b1278  99                    cdq
  004b1279  8d4c1b01              lea       ecx,[ebx+ebx+1]
  004b127d  f7f9                  idiv      ecx
  004b127f  8b442414              mov       eax,[esp+14h]
  004b1283  83c264                add       edx,64h
  004b1286  3bc2                  cmp       eax,edx
  004b1288  7d41                  jge       short 004B12CBh
  004b128a  83ff1e                cmp       edi,1Eh
  004b128d  7d29                  jge       short 004B12B8h
  004b128f  8b16                  mov       edx,[esi]
  004b1291  c646420b              mov       byte [esi+42h],0Bh
  004b1295  81e2fffffffe          and       edx,0FEFFFFFFh
  004b129b  c6464301              mov       byte [esi+43h],1
  004b129f  83ca02                or        edx,2
  004b12a2  33c0                  xor       eax,eax
  004b12a4  8916                  mov       [esi],edx
  004b12a6  89465c                mov       [esi+5Ch],eax
  004b12a9  66c746180500          mov       word [esi+18h],5
  004b12af  6689461a              mov       [esi+1Ah],ax
  004b12b3  e9ec000000            jmp       004B13A4h

LAB_004b12b8:
  004b12b8  c646420c              mov       byte [esi+42h],0Ch
  004b12bc  c6464301              mov       byte [esi+43h],1
  004b12c0  66c746120000          mov       word [esi+12h],0
  004b12c6  e9d9000000            jmp       004B13A4h

LAB_004b12cb:
  004b12cb  c6464201              mov       byte [esi+42h],1
  004b12cf  c6464301              mov       byte [esi+43h],1
  004b12d3  e9cc000000            jmp       004B13A4h

LAB_004b12d8:
  004b12d8  83ff3c                cmp       edi,3Ch
  004b12db  7d55                  jge       short 004B1332h
  004b12dd  8b442414              mov       eax,[esp+14h]
  004b12e1  bf50000000            mov       edi,50h
  004b12e6  3bc7                  cmp       eax,edi
  004b12e8  7d1b                  jge       short 004B1305h
  004b12ea  c646420e              mov       byte [esi+42h],0Eh
  004b12ee  e84d5e0900            call      00547140h
  004b12f3  99                    cdq
  004b12f4  b914000000            mov       ecx,14h
  004b12f9  f7f9                  idiv      ecx
  004b12fb  02d1                  add       dl,cl
  004b12fd  885643                mov       [esi+43h],dl
  004b1300  e99f000000            jmp       004B13A4h

LAB_004b1305:
  004b1305  807e420e              cmp       byte [esi+42h],0Eh
  004b1309  0f84cc000000          je        near 004B13DBh
  004b130f  c6464202              mov       byte [esi+42h],2
  004b1313  e8285e0900            call      00547140h
  004b1318  99                    cdq
  004b1319  b90a000000            mov       ecx,0Ah
  004b131e  66897e10              mov       [esi+10h],di
  004b1322  f7f9                  idiv      ecx
  004b1324  8bc3                  mov       eax,ebx
  004b1326  d1f8                  sar       eax,1
  004b1328  02d0                  add       dl,al
  004b132a  80c214                add       dl,14h
  004b132d  885643                mov       [esi+43h],dl
  004b1330  eb72                  jmp       short 004B13A4h

LAB_004b1332:
  004b1332  8b442414              mov       eax,[esp+14h]
  004b1336  83ff46                cmp       edi,46h
  004b1339  7d23                  jge       short 004B135Eh
  004b133b  8b4c2410              mov       ecx,[esp+10h]
  004b133f  83c164                add       ecx,64h
  004b1342  3bc1                  cmp       eax,ecx
  004b1344  7e18                  jle       short 004B135Eh
  004b1346  c646420e              mov       byte [esi+42h],0Eh
  004b134a  e8f15d0900            call      00547140h
  004b134f  99                    cdq
  004b1350  b914000000            mov       ecx,14h
  004b1355  f7f9                  idiv      ecx
  004b1357  02d1                  add       dl,cl
  004b1359  885643                mov       [esi+43h],dl
  004b135c  eb46                  jmp       short 004B13A4h

LAB_004b135e:
  004b135e  83f850                cmp       eax,50h
  004b1361  7d18                  jge       short 004B137Bh
  004b1363  c646420e              mov       byte [esi+42h],0Eh
  004b1367  e8d45d0900            call      00547140h
  004b136c  99                    cdq
  004b136d  b914000000            mov       ecx,14h
  004b1372  f7f9                  idiv      ecx
  004b1374  02d1                  add       dl,cl
  004b1376  885643                mov       [esi+43h],dl
  004b1379  eb29                  jmp       short 004B13A4h

LAB_004b137b:
  004b137b  807e420e              cmp       byte [esi+42h],0Eh
  004b137f  745a                  je        short 004B13DBh
  004b1381  c6464202              mov       byte [esi+42h],2
  004b1385  e8b65d0900            call      00547140h
  004b138a  99                    cdq
  004b138b  b90a000000            mov       ecx,0Ah
  004b1390  66c746105000          mov       word [esi+10h],50h
  004b1396  f7f9                  idiv      ecx
  004b1398  8bc3                  mov       eax,ebx
  004b139a  d1f8                  sar       eax,1
  004b139c  02d0                  add       dl,al
  004b139e  80c214                add       dl,14h
  004b13a1  885643                mov       [esi+43h],dl

LAB_004b13a4:
  004b13a4  8b442414              mov       eax,[esp+14h]
  004b13a8  3d58020000            cmp       eax,258h
  004b13ad  0f8dfd000000          jge       near 004B14B0h
  004b13b3  3dfa000000            cmp       eax,0FAh
  004b13b8  0f8cf2000000          jl        near 004B14B0h
  004b13be  e87d5d0900            call      00547140h
  004b13c3  99                    cdq
  004b13c4  b964000000            mov       ecx,64h
  004b13c9  f7f9                  idiv      ecx
  004b13cb  83fa14                cmp       edx,14h
  004b13ce  7c21                  jl        short 004B13F1h
  004b13d0  83fa1e                cmp       edx,1Eh
  004b13d3  7d6f                  jge       short 004B1444h
  004b13d5  807e420e              cmp       byte [esi+42h],0Eh
  004b13d9  7544                  jne       short 004B141Fh

LAB_004b13db:
  004b13db  8a4643                mov       al,[esi+43h]
  004b13de  84c0                  test      al,al
  004b13e0  0f8400feffff          je        near 004B11E6h
  004b13e6  5f                    pop       edi
  004b13e7  5e                    pop       esi
  004b13e8  5d                    pop       ebp
  004b13e9  5b                    pop       ebx
  004b13ea  81c494000000          add       esp,94h
  004b13f0  c3                    ret

LAB_004b13f1:
  004b13f1  8b54241c              mov       edx,[esp+1Ch]
  004b13f5  81c200040000          add       edx,400h
  004b13fb  81e2ff0f0000          and       edx,0FFFh
  004b1401  81fa00080000          cmp       edx,800h
  004b1407  7c0d                  jl        short 004B1416h
  004b1409  c6464201              mov       byte [esi+42h],1
  004b140d  c6464301              mov       byte [esi+43h],1
  004b1411  e99a000000            jmp       004B14B0h

LAB_004b1416:
  004b1416  c646420d              mov       byte [esi+42h],0Dh
  004b141a  e987000000            jmp       004B14A6h

LAB_004b141f:
  004b141f  c6464202              mov       byte [esi+42h],2
  004b1423  e8185d0900            call      00547140h
  004b1428  99                    cdq
  004b1429  b90a000000            mov       ecx,0Ah
  004b142e  66c746105000          mov       word [esi+10h],50h
  004b1434  f7f9                  idiv      ecx
  004b1436  8bc3                  mov       eax,ebx
  004b1438  d1f8                  sar       eax,1
  004b143a  02d0                  add       dl,al
  004b143c  80c214                add       dl,14h
  004b143f  885643                mov       [esi+43h],dl
  004b1442  eb6c                  jmp       short 004B14B0h

LAB_004b1444:
  004b1444  83fa32                cmp       edx,32h
  004b1447  7d18                  jge       short 004B1461h
  004b1449  c646420e              mov       byte [esi+42h],0Eh
  004b144d  e8ee5c0900            call      00547140h
  004b1452  99                    cdq
  004b1453  b914000000            mov       ecx,14h
  004b1458  f7f9                  idiv      ecx
  004b145a  02d1                  add       dl,cl
  004b145c  885643                mov       [esi+43h],dl
  004b145f  eb4f                  jmp       short 004B14B0h

LAB_004b1461:
  004b1461  83fa50                cmp       edx,50h
  004b1464  7d3c                  jge       short 004B14A2h
  004b1466  c6464203              mov       byte [esi+42h],3
  004b146a  66c746120000          mov       word [esi+12h],0
  004b1470  e8cb5c0900            call      00547140h
  004b1475  99                    cdq
  004b1476  b90a000000            mov       ecx,0Ah
  004b147b  f7f9                  idiv      ecx
  004b147d  8bc3                  mov       eax,ebx
  004b147f  d1f8                  sar       eax,1
  004b1481  02d0                  add       dl,al
  004b1483  80c214                add       dl,14h
  004b1486  885643                mov       [esi+43h],dl
  004b1489  e8b25c0900            call      00547140h
  004b148e  99                    cdq
  004b148f  33c2                  xor       eax,edx
  004b1491  2bc2                  sub       eax,edx
  004b1493  25ff0f0000            and       eax,0FFFh
  004b1498  33c2                  xor       eax,edx
  004b149a  2bc2                  sub       eax,edx
  004b149c  66894610              mov       [esi+10h],ax
  004b14a0  eb0e                  jmp       short 004B14B0h

LAB_004b14a2:
  004b14a2  c646420a              mov       byte [esi+42h],0Ah

LAB_004b14a6:
  004b14a6  c6464301              mov       byte [esi+43h],1
  004b14aa  66c746120000          mov       word [esi+12h],0

LAB_004b14b0:
  004b14b0  817c241458020000      cmp       dword [esp+14h],258h
  004b14b8  0f8c16010000          jl        near 004B15D4h
  004b14be  e87d5c0900            call      00547140h
  004b14c3  bf64000000            mov       edi,64h
  004b14c8  99                    cdq
  004b14c9  8bcf                  mov       ecx,edi
  004b14cb  f7f9                  idiv      ecx
  004b14cd  83fa1e                cmp       edx,1Eh
  004b14d0  7d3a                  jge       short 004B150Ch
  004b14d2  8b442410              mov       eax,[esp+10h]
  004b14d6  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004b14da  3bd1                  cmp       edx,ecx
  004b14dc  0f8dc7000000          jge       near 004B15A9h

LAB_004b14e2:
  004b14e2  c6464202              mov       byte [esi+42h],2
  004b14e6  e8555c0900            call      00547140h
  004b14eb  99                    cdq
  004b14ec  b90a000000            mov       ecx,0Ah
  004b14f1  66897e10              mov       [esi+10h],di
  004b14f5  f7f9                  idiv      ecx
  004b14f7  d1fb                  sar       ebx,1
  004b14f9  02d3                  add       dl,bl
  004b14fb  80c214                add       dl,14h
  004b14fe  885643                mov       [esi+43h],dl
  004b1501  5f                    pop       edi
  004b1502  5e                    pop       esi
  004b1503  5d                    pop       ebp
  004b1504  5b                    pop       ebx
  004b1505  81c494000000          add       esp,94h
  004b150b  c3                    ret

LAB_004b150c:
  004b150c  83fa28                cmp       edx,28h
  004b150f  7d43                  jge       short 004B1554h
  004b1511  c6464203              mov       byte [esi+42h],3
  004b1515  66c746120000          mov       word [esi+12h],0
  004b151b  e8205c0900            call      00547140h
  004b1520  99                    cdq
  004b1521  b90a000000            mov       ecx,0Ah
  004b1526  f7f9                  idiv      ecx
  004b1528  d1fb                  sar       ebx,1
  004b152a  02d3                  add       dl,bl
  004b152c  80c214                add       dl,14h
  004b152f  885643                mov       [esi+43h],dl
  004b1532  e8095c0900            call      00547140h
  004b1537  99                    cdq
  004b1538  33c2                  xor       eax,edx
  004b153a  2bc2                  sub       eax,edx
  004b153c  25ff0f0000            and       eax,0FFFh
  004b1541  33c2                  xor       eax,edx
  004b1543  2bc2                  sub       eax,edx
  004b1545  66894610              mov       [esi+10h],ax
  004b1549  5f                    pop       edi
  004b154a  5e                    pop       esi
  004b154b  5d                    pop       ebp
  004b154c  5b                    pop       ebx
  004b154d  81c494000000          add       esp,94h
  004b1553  c3                    ret

LAB_004b1554:
  004b1554  83fa3c                cmp       edx,3Ch
  004b1557  7d13                  jge       short 004B156Ch
  004b1559  c6464201              mov       byte [esi+42h],1
  004b155d  c6464301              mov       byte [esi+43h],1
  004b1561  5f                    pop       edi
  004b1562  5e                    pop       esi
  004b1563  5d                    pop       ebp
  004b1564  5b                    pop       ebx
  004b1565  81c494000000          add       esp,94h
  004b156b  c3                    ret

LAB_004b156c:
  004b156c  83fa50                cmp       edx,50h
  004b156f  7d38                  jge       short 004B15A9h
  004b1571  8b54241c              mov       edx,[esp+1Ch]
  004b1575  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004b157c  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004b1583  25ff0f0000            and       eax,0FFFh
  004b1588  3bc1                  cmp       eax,ecx
  004b158a  0f8f52ffffff          jg        near 004B14E2h
  004b1590  c646420a              mov       byte [esi+42h],0Ah
  004b1594  c6464301              mov       byte [esi+43h],1
  004b1598  66c746120000          mov       word [esi+12h],0
  004b159e  5f                    pop       edi
  004b159f  5e                    pop       esi
  004b15a0  5d                    pop       ebp
  004b15a1  5b                    pop       ebx
  004b15a2  81c494000000          add       esp,94h
  004b15a8  c3                    ret

LAB_004b15a9:
  004b15a9  c6464200              mov       byte [esi+42h],0
  004b15ad  e88e5b0900            call      00547140h
  004b15b2  99                    cdq
  004b15b3  b90a000000            mov       ecx,0Ah
  004b15b8  66c746120000          mov       word [esi+12h],0
  004b15be  f7f9                  idiv      ecx
  004b15c0  c1fb02                sar       ebx,2
  004b15c3  02d3                  add       dl,bl
  004b15c5  885643                mov       [esi+43h],dl
  004b15c8  8b542410              mov       edx,[esp+10h]
  004b15cc  8d441250              lea       eax,[edx+edx+50h]
  004b15d0  66894610              mov       [esi+10h],ax

LAB_004b15d4:
  004b15d4  5f                    pop       edi
  004b15d5  5e                    pop       esi
  004b15d6  5d                    pop       ebp
  004b15d7  5b                    pop       ebx
  004b15d8  81c494000000          add       esp,94h
  004b15de  c3                    ret
  004b15df  90                    nop
  004b15e0  cd09                  int       9
  004b15e2  4b                    dec       ebx
  004b15e3  00140a                add       [edx+ecx],dl
  004b15e6  4b                    dec       ebx
  004b15e7  00850a4b002b          add       [ebp+2B004B0Ah],al
  004b15ed  0b4b00                or        ecx,[ebx]
  004b15f0  9c                    pushf
  004b15f1  0f4b00                cmovnp    eax,[eax]
  004b15f4  fa                    cli
  004b15f5  0b4b00                or        ecx,[ebx]
  004b15f8  42                    inc       edx
  004b15f9  0c4b                  or        al,4Bh
  004b15fb  00d8                  add       al,bl
  004b15fd  0c4b                  or        al,4Bh
  004b15ff  009c0f4b009c0f        add       [edi+ecx+0F9C004Bh],bl
  004b1606  4b                    dec       ebx
  004b1607  0038                  add       [eax],bh
  004b1609  0d4b00a10d            or        eax,0DA1004Bh
  004b160e  4b                    dec       ebx
  004b160f  00d8                  add       al,bl
  004b1611  0d4b00220e            or        eax,0E22004Bh
  004b1616  4b                    dec       ebx
  004b1617  0010                  add       [eax],dl
  004b1619  0f4b00                cmovnp    eax,[eax]
  004b161c  90                    nop
  004b161d  90                    nop
  004b161e  90                    nop
  004b161f  90                    nop

80 cible(s) de saut interne : 0x004b07a9, 0x004b07cc, 0x004b07db, 0x004b07de, 0x004b0854, 0x004b0858, 0x004b0871, 0x004b0876, 0x004b08e2, 0x004b08e6, 0x004b09b4, 0x004b09b8, 0x004b09ed, 0x004b09ff, 0x004b0a35, 0x004b0a55, 0x004b0a71, 0x004b0ab9, 0x004b0ad5, 0x004b0ae1, 0x004b0b19, 0x004b0b42, 0x004b0bbe, 0x004b0bcc, 0x004b0bd0, 0x004b0c18, 0x004b0c54, 0x004b0cc7, 0x004b0cf5, 0x004b0d01, 0x004b0d58, 0x004b0d64, 0x004b0d8f, 0x004b0dbb, 0x004b0dfe, 0x004b0e54, 0x004b0e7e, 0x004b0e85, 0x004b0e89, 0x004b0eb7, 0x004b0ec1, 0x004b0ec5, 0x004b0ee8, 0x004b0f27, 0x004b0f7d, 0x004b0f99, 0x004b0f9c, 0x004b1083, 0x004b108f, 0x004b1091, 0x004b10a2, 0x004b10e0, 0x004b1170, 0x004b1180, 0x004b11a9, 0x004b11d7, 0x004b11e6, 0x004b12b8, 0x004b12cb, 0x004b12d8, 0x004b1305, 0x004b1332, 0x004b135e, 0x004b137b, 0x004b13a4, 0x004b13db, 0x004b13f1, 0x004b1416, 0x004b141f, 0x004b1444, 0x004b1461, 0x004b14a2, 0x004b14a6, 0x004b14b0, 0x004b14e2, 0x004b150c, 0x004b1554, 0x004b156c, 0x004b15a9, 0x004b15d4
