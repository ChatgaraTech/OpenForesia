Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044ea60  section .text  offset fichier 0x4de60  1216 octets

  0044ea60  83ec30                sub       esp,30h
  0044ea63  b8baffffff            mov       eax,0FFFFFFBAh
  0044ea68  53                    push      ebx
  0044ea69  6689442428            mov       [esp+28h],ax
  0044ea6e  668944242a            mov       [esp+2Ah],ax
  0044ea73  b824000000            mov       eax,24h
  0044ea78  55                    push      ebp
  0044ea79  6689442430            mov       [esp+30h],ax
  0044ea7e  6689442432            mov       [esp+32h],ax
  0044ea83  b88f000000            mov       eax,8Fh
  0044ea88  56                    push      esi
  0044ea89  8b742440              mov       esi,[esp+40h]
  0044ea8d  6689442438            mov       [esp+38h],ax
  0044ea92  668944243a            mov       [esp+3Ah],ax
  0044ea97  b83e000000            mov       eax,3Eh
  0044ea9c  6689442424            mov       [esp+24h],ax
  0044eaa1  6689442426            mov       [esp+26h],ax
  0044eaa6  b847000000            mov       eax,47h
  0044eaab  57                    push      edi
  0044eaac  8b7e6c                mov       edi,[esi+6Ch]
  0044eaaf  668944242c            mov       [esp+2Ch],ax
  0044eab4  668944242e            mov       [esp+2Eh],ax
  0044eab9  b854000000            mov       eax,54h
  0044eabe  85ff                  test      edi,edi
  0044eac0  66c744241ca0ff        mov       word [esp+1Ch],0FFA0h
  0044eac7  66c744241e6000        mov       word [esp+1Eh],60h
  0044eace  66c74424206aff        mov       word [esp+20h],0FF6Ah
  0044ead5  66c74424229600        mov       word [esp+22h],96h
  0044eadc  66c744242497ff        mov       word [esp+24h],0FF97h
  0044eae3  66c74424266900        mov       word [esp+26h],69h
  0044eaea  6689442430            mov       [esp+30h],ax
  0044eaef  6689442432            mov       [esp+32h],ax
  0044eaf4  7505                  jne       short 0044EAFBh
  0044eaf6  bf58e55d00            mov       edi,5DE558h

LAB_0044eafb:
  0044eafb  8b4674                mov       eax,[esi+74h]
  0044eafe  85c0                  test      eax,eax
  0044eb00  89442410              mov       [esp+10h],eax
  0044eb04  7508                  jne       short 0044EB0Eh
  0044eb06  c744241058e55d00      mov       dword [esp+10h],5DE558h

LAB_0044eb0e:
  0044eb0e  8b4f1c                mov       ecx,[edi+1Ch]
  0044eb11  668b4614              mov       ax,[esi+14h]
  0044eb15  894e1c                mov       [esi+1Ch],ecx
  0044eb18  8b5724                mov       edx,[edi+24h]
  0044eb1b  0fbfd8                movsx     ebx,ax
  0044eb1e  895624                mov       [esi+24h],edx
  0044eb21  8b4f20                mov       ecx,[edi+20h]
  0044eb24  d1e3                  shl       ebx,1
  0044eb26  894e20                mov       [esi+20h],ecx
  0044eb29  0fbf4732              movsx     eax,word [edi+32h]
  0044eb2d  0fbf541c1c            movsx     edx,word [esp+ebx+1Ch]
  0044eb32  0fbf6c1c28            movsx     ebp,word [esp+ebx+28h]
  0044eb37  50                    push      eax
  0044eb38  89542448              mov       [esp+48h],edx
  0044eb3c  896c241c              mov       [esp+1Ch],ebp
  0044eb40  e8d76e0f00            call      00545A1Ch
  0044eb45  8bd0                  mov       edx,eax
  0044eb47  0fbf4f32              movsx     ecx,word [edi+32h]
  0044eb4b  0fafd5                imul      edx,ebp
  0044eb4e  51                    push      ecx
  0044eb4f  89542418              mov       [esp+18h],edx
  0044eb53  e8be6e0f00            call      00545A16h
  0044eb58  0faf442444            imul      eax,[esp+44h]
  0044eb5d  8b542414              mov       edx,[esp+14h]
  0044eb61  8b4e1c                mov       ecx,[esi+1Ch]
  0044eb64  2bc2                  sub       eax,edx
  0044eb66  03c8                  add       ecx,eax
  0044eb68  894e1c                mov       [esi+1Ch],ecx
  0044eb6b  0fbf4732              movsx     eax,word [edi+32h]
  0044eb6f  50                    push      eax
  0044eb70  e8a16e0f00            call      00545A16h
  0044eb75  0fbf4f32              movsx     ecx,word [edi+32h]
  0044eb79  8be8                  mov       ebp,eax
  0044eb7b  51                    push      ecx
  0044eb7c  0faf6c241c            imul      ebp,[esp+1Ch]
  0044eb81  e8966e0f00            call      00545A1Ch
  0044eb86  0faf442444            imul      eax,[esp+44h]
  0044eb8b  0fbf541c34            movsx     edx,word [esp+ebx+34h]
  0044eb90  8b4e20                mov       ecx,[esi+20h]
  0044eb93  03e8                  add       ebp,eax
  0044eb95  8b4624                mov       eax,[esi+24h]
  0044eb98  c1e20c                shl       edx,0Ch
  0044eb9b  2bc5                  sub       eax,ebp
  0044eb9d  03ca                  add       ecx,edx
  0044eb9f  894624                mov       [esi+24h],eax
  0044eba2  894e20                mov       [esi+20h],ecx
  0044eba5  f70700000080          test      dword [edi],80000000h
  0044ebab  7407                  je        short 0044EBB4h
  0044ebad  66837f1003            cmp       word [edi+10h],3
  0044ebb2  7506                  jne       short 0044EBBAh

LAB_0044ebb4:
  0044ebb4  66c746100200          mov       word [esi+10h],2

LAB_0044ebba:
  0044ebba  0fbf4614              movsx     eax,word [esi+14h]
  0044ebbe  83f805                cmp       eax,5
  0044ebc1  7740                  ja        short 0044EC03h
  0044ebc3  ff248524ef4400        jmp       dword [eax*4+44EF24h]
  0044ebca  8b7f44                mov       edi,[edi+44h]
  0044ebcd  81e7ffff0000          and       edi,0FFFFh
  0044ebd3  eb32                  jmp       short 0044EC07h
  0044ebd5  8b7f44                mov       edi,[edi+44h]
  0044ebd8  c1ff10                sar       edi,10h
  0044ebdb  eb2a                  jmp       short 0044EC07h
  0044ebdd  8b7f48                mov       edi,[edi+48h]
  0044ebe0  81e7ffff0000          and       edi,0FFFFh
  0044ebe6  eb1f                  jmp       short 0044EC07h
  0044ebe8  8b7f48                mov       edi,[edi+48h]
  0044ebeb  c1ff10                sar       edi,10h
  0044ebee  eb17                  jmp       short 0044EC07h
  0044ebf0  8b7f4c                mov       edi,[edi+4Ch]
  0044ebf3  81e7ffff0000          and       edi,0FFFFh
  0044ebf9  eb0c                  jmp       short 0044EC07h
  0044ebfb  8b7f4c                mov       edi,[edi+4Ch]
  0044ebfe  c1ff10                sar       edi,10h
  0044ec01  eb04                  jmp       short 0044EC07h

LAB_0044ec03:
  0044ec03  8b7c2444              mov       edi,[esp+44h]

LAB_0044ec07:
  0044ec07  33c9                  xor       ecx,ecx
  0044ec09  3bf9                  cmp       edi,ecx
  0044ec0b  7434                  je        short 0044EC41h
  0044ec0d  8b442410              mov       eax,[esp+10h]
  0044ec11  f70000008000          test      dword [eax],800000h
  0044ec17  7414                  je        short 0044EC2Dh
  0044ec19  8d14bf                lea       edx,[edi+edi*4]
  0044ec1c  c1e203                shl       edx,3
  0044ec1f  2bd7                  sub       edx,edi
  0044ec21  8d3c9504ca5e00        lea       edi,[edx*4+5ECA04h]
  0044ec28  897e78                mov       [esi+78h],edi
  0044ec2b  eb19                  jmp       short 0044EC46h

LAB_0044ec2d:
  0044ec2d  8d04bf                lea       eax,[edi+edi*4]
  0044ec30  c1e003                shl       eax,3
  0044ec33  2bc7                  sub       eax,edi
  0044ec35  8d3c8524935e00        lea       edi,[eax*4+5E9324h]
  0044ec3c  897e78                mov       [esi+78h],edi
  0044ec3f  eb05                  jmp       short 0044EC46h

LAB_0044ec41:
  0044ec41  894e78                mov       [esi+78h],ecx
  0044ec44  8bfe                  mov       edi,esi

LAB_0044ec46:
  0044ec46  66394e10              cmp       [esi+10h],cx
  0044ec4a  7558                  jne       short 0044ECA4h
  0044ec4c  b82c010000            mov       eax,12Ch
  0044ec51  c6460400              mov       byte [esi+4],0
  0044ec55  c6460a09              mov       byte [esi+0Ah],9
  0044ec59  c6460b00              mov       byte [esi+0Bh],0
  0044ec5d  66894628              mov       [esi+28h],ax
  0044ec61  6689462c              mov       [esi+2Ch],ax
  0044ec65  66c7462a401f          mov       word [esi+2Ah],1F40h
  0044ec6b  66c746340004          mov       word [esi+34h],400h
  0044ec71  66c746100100          mov       word [esi+10h],1
  0044ec77  c6463800              mov       byte [esi+38h],0
  0044ec7b  c6463900              mov       byte [esi+39h],0
  0044ec7f  c6463a00              mov       byte [esi+3Ah],0
  0044ec83  66894e16              mov       [esi+16h],cx
  0044ec87  8b571c                mov       edx,[edi+1Ch]
  0044ec8a  895650                mov       [esi+50h],edx
  0044ec8d  8b4720                mov       eax,[edi+20h]
  0044ec90  894654                mov       [esi+54h],eax
  0044ec93  8b5724                mov       edx,[edi+24h]
  0044ec96  806636fe              and       byte [esi+36h],0FEh
  0044ec9a  895658                mov       [esi+58h],edx
  0044ec9d  66898e98000000        mov       [esi+98h],cx

LAB_0044eca4:
  0044eca4  66837e1001            cmp       word [esi+10h],1
  0044eca9  0f8523020000          jne       near 0044EED2h
  0044ecaf  6a0a                  push      0Ah
  0044ecb1  6a0a                  push      0Ah
  0044ecb3  6a0a                  push      0Ah
  0044ecb5  56                    push      esi
  0044ecb6  e82561fbff            call      00404DE0h
  0044ecbb  8b4650                mov       eax,[esi+50h]
  0044ecbe  8b4f1c                mov       ecx,[edi+1Ch]
  0044ecc1  2bc8                  sub       ecx,eax
  0044ecc3  8b5e20                mov       ebx,[esi+20h]
  0044ecc6  c1f902                sar       ecx,2
  0044ecc9  03c8                  add       ecx,eax
  0044eccb  8b4658                mov       eax,[esi+58h]
  0044ecce  894e50                mov       [esi+50h],ecx
  0044ecd1  8b5724                mov       edx,[edi+24h]
  0044ecd4  2bd0                  sub       edx,eax
  0044ecd6  8b6e24                mov       ebp,[esi+24h]
  0044ecd9  c1fa02                sar       edx,2
  0044ecdc  03d0                  add       edx,eax
  0044ecde  8b4654                mov       eax,[esi+54h]
  0044ece1  895658                mov       [esi+58h],edx
  0044ece4  8b4f20                mov       ecx,[edi+20h]
  0044ece7  2bc8                  sub       ecx,eax
  0044ece9  8b5650                mov       edx,[esi+50h]
  0044ecec  c1f902                sar       ecx,2
  0044ecef  8b7e1c                mov       edi,[esi+1Ch]
  0044ecf2  03c1                  add       eax,ecx
  0044ecf4  894654                mov       [esi+54h],eax
  0044ecf7  83c410                add       esp,10h
  0044ecfa  c1f80c                sar       eax,0Ch
  0044ecfd  c1fb0c                sar       ebx,0Ch
  0044ed00  2bd8                  sub       ebx,eax
  0044ed02  8b4658                mov       eax,[esi+58h]
  0044ed05  c1f80c                sar       eax,0Ch
  0044ed08  c1fd0c                sar       ebp,0Ch
  0044ed0b  c1fa0c                sar       edx,0Ch
  0044ed0e  c1ff0c                sar       edi,0Ch
  0044ed11  2be8                  sub       ebp,eax
  0044ed13  2bfa                  sub       edi,edx
  0044ed15  8bcd                  mov       ecx,ebp
  0044ed17  8bc7                  mov       eax,edi
  0044ed19  0fafcd                imul      ecx,ebp
  0044ed1c  0fafc7                imul      eax,edi
  0044ed1f  894c2444              mov       [esp+44h],ecx
  0044ed23  03c8                  add       ecx,eax
  0044ed25  51                    push      ecx
  0044ed26  8944241c              mov       [esp+1Ch],eax
  0044ed2a  e8e16c0f00            call      00545A10h
  0044ed2f  50                    push      eax
  0044ed30  53                    push      ebx
  0044ed31  e8ec6c0f00            call      00545A22h
  0044ed36  0500040000            add       eax,400h
  0044ed3b  b900080000            mov       ecx,800h
  0044ed40  663bc1                cmp       ax,cx
  0044ed43  66894634              mov       [esi+34h],ax
  0044ed47  7c0f                  jl        short 0044ED58h
  0044ed49  b800f8ffff            mov       eax,0FFFFF800h

LAB_0044ed4e:
  0044ed4e  66014634              add       [esi+34h],ax
  0044ed52  66394e34              cmp       [esi+34h],cx
  0044ed56  7df6                  jge       short 0044ED4Eh

LAB_0044ed58:
  0044ed58  66837e3400            cmp       word [esi+34h],0
  0044ed5d  7d0b                  jge       short 0044ED6Ah

LAB_0044ed5f:
  0044ed5f  66014e34              add       [esi+34h],cx
  0044ed63  66837e3400            cmp       word [esi+34h],0
  0044ed68  7cf5                  jl        short 0044ED5Fh

LAB_0044ed6a:
  0044ed6a  55                    push      ebp
  0044ed6b  57                    push      edi
  0044ed6c  e8b16c0f00            call      00545A22h
  0044ed71  8bcb                  mov       ecx,ebx
  0044ed73  8b7c2418              mov       edi,[esp+18h]
  0044ed77  0fafcb                imul      ecx,ebx
  0044ed7a  8b5c2444              mov       ebx,[esp+44h]
  0044ed7e  0500040000            add       eax,400h
  0044ed83  03cb                  add       ecx,ebx
  0044ed85  66894632              mov       [esi+32h],ax
  0044ed89  03cf                  add       ecx,edi
  0044ed8b  51                    push      ecx
  0044ed8c  e87f6c0f00            call      00545A10h
  0044ed91  c1e00c                shl       eax,0Ch
  0044ed94  8bc8                  mov       ecx,eax
  0044ed96  b8eba00eea            mov       eax,0EA0EA0EBh
  0044ed9b  f7e9                  imul      ecx
  0044ed9d  03d1                  add       edx,ecx
  0044ed9f  33ff                  xor       edi,edi
  0044eda1  c1fa07                sar       edx,7
  0044eda4  8bc2                  mov       eax,edx
  0044eda6  66c7869a0000000900    mov       word [esi+9Ah],9
  0044edaf  c1e81f                shr       eax,1Fh
  0044edb2  03d0                  add       edx,eax
  0044edb4  8b4678                mov       eax,[esi+78h]
  0044edb7  3bc7                  cmp       eax,edi
  0044edb9  6689562a              mov       [esi+2Ah],dx
  0044edbd  0f84db000000          je        near 0044EE9Eh
  0044edc3  6639be98000000        cmp       [esi+98h],di
  0044edca  7519                  jne       short 0044EDE5h
  0044edcc  6800100000            push      1000h
  0044edd1  6800100000            push      1000h
  0044edd6  6800080000            push      800h
  0044eddb  6a0f                  push      0Fh
  0044eddd  e8bee00e00            call      0053CEA0h
  0044ede2  83c410                add       esp,10h

LAB_0044ede5:
  0044ede5  66ff8698000000        inc       word [esi+98h]
  0044edec  6683be9800000005      cmp       word [esi+98h],5
  0044edf4  7507                  jne       short 0044EDFDh
  0044edf6  6689be98000000        mov       [esi+98h],di

LAB_0044edfd:
  0044edfd  8b0e                  mov       ecx,[esi]
  0044edff  8a4616                mov       al,[esi+16h]
  0044ee02  80e5bf                and       ch,0BFh
  0044ee05  a801                  test      al,1
  0044ee07  890e                  mov       [esi],ecx
  0044ee09  7454                  je        short 0044EE5Fh
  0044ee0b  8b4e24                mov       ecx,[esi+24h]
  0044ee0e  8b5650                mov       edx,[esi+50h]
  0044ee11  8b4654                mov       eax,[esi+54h]
  0044ee14  8b5e1c                mov       ebx,[esi+1Ch]
  0044ee17  8b6e20                mov       ebp,[esi+20h]
  0044ee1a  8d7e1c                lea       edi,[esi+1Ch]
  0044ee1d  894c2444              mov       [esp+44h],ecx
  0044ee21  8b4e58                mov       ecx,[esi+58h]
  0044ee24  8917                  mov       [edi],edx
  0044ee26  894620                mov       [esi+20h],eax
  0044ee29  894e24                mov       [esi+24h],ecx
  0044ee2c  e80f830f00            call      00547140h
  0044ee31  99                    cdq
  0044ee32  b903000000            mov       ecx,3
  0044ee37  f7f9                  idiv      ecx
  0044ee39  6685d2                test      dx,dx
  0044ee3c  7515                  jne       short 0044EE53h
  0044ee3e  6aff                  push      0FFFFFFFFh
  0044ee40  6a00                  push      0
  0044ee42  6895000000            push      95h
  0044ee47  6a00                  push      0
  0044ee49  57                    push      edi
  0044ee4a  56                    push      esi
  0044ee4b  e8f0bd0200            call      0047AC40h
  0044ee50  83c418                add       esp,18h

LAB_0044ee53:
  0044ee53  8b542444              mov       edx,[esp+44h]
  0044ee57  891f                  mov       [edi],ebx
  0044ee59  896e20                mov       [esi+20h],ebp
  0044ee5c  895624                mov       [esi+24h],edx

LAB_0044ee5f:
  0044ee5f  66837e1614            cmp       word [esi+16h],14h
  0044ee64  7e6c                  jle       short 0044EED2h
  0044ee66  8b5678                mov       edx,[esi+78h]
  0044ee69  33c0                  xor       eax,eax
  0044ee6b  8a4606                mov       al,[esi+6]
  0044ee6e  33c9                  xor       ecx,ecx
  0044ee70  8a4e07                mov       cl,[esi+7]
  0044ee73  6a00                  push      0
  0044ee75  50                    push      eax
  0044ee76  8b4674                mov       eax,[esi+74h]
  0044ee79  51                    push      ecx
  0044ee7a  52                    push      edx
  0044ee7b  50                    push      eax
  0044ee7c  e82fb40e00            call      0053A2B0h
  0044ee81  8b4678                mov       eax,[esi+78h]
  0044ee84  83c414                add       esp,14h
  0044ee87  8b08                  mov       ecx,[eax]
  0044ee89  66c780960000001000    mov       word [eax+96h],10h
  0044ee92  83c940                or        ecx,40h
  0044ee95  8908                  mov       [eax],ecx
  0044ee97  66834616ec            add       word [esi+16h],0FFECh
  0044ee9c  eb34                  jmp       short 0044EED2h

LAB_0044ee9e:
  0044ee9e  6aec                  push      0FFFFFFECh
  0044eea0  6aec                  push      0FFFFFFECh
  0044eea2  6aec                  push      0FFFFFFECh
  0044eea4  56                    push      esi
  0044eea5  6689be98000000        mov       [esi+98h],di
  0044eeac  e82f5ffbff            call      00404DE0h
  0044eeb1  8a4638                mov       al,[esi+38h]
  0044eeb4  83c410                add       esp,10h
  0044eeb7  84c0                  test      al,al
  0044eeb9  66897e16              mov       [esi+16h],di
  0044eebd  7513                  jne       short 0044EED2h
  0044eebf  8b06                  mov       eax,[esi]
  0044eec1  c6463800              mov       byte [esi+38h],0
  0044eec5  80cc40                or        ah,40h
  0044eec8  c6463900              mov       byte [esi+39h],0
  0044eecc  8906                  mov       [esi],eax
  0044eece  c6463a00              mov       byte [esi+3Ah],0

LAB_0044eed2:
  0044eed2  66837e1002            cmp       word [esi+10h],2
  0044eed7  752e                  jne       short 0044EF07h
  0044eed9  6aec                  push      0FFFFFFECh
  0044eedb  6aec                  push      0FFFFFFECh
  0044eedd  6aec                  push      0FFFFFFECh
  0044eedf  56                    push      esi
  0044eee0  e8fb5efbff            call      00404DE0h
  0044eee5  8a4638                mov       al,[esi+38h]
  0044eee8  83c410                add       esp,10h
  0044eeeb  84c0                  test      al,al
  0044eeed  7518                  jne       short 0044EF07h
  0044eeef  8b16                  mov       edx,[esi]
  0044eef1  8b4678                mov       eax,[esi+78h]
  0044eef4  81e2ffffff7f          and       edx,7FFFFFFFh
  0044eefa  85c0                  test      eax,eax
  0044eefc  8916                  mov       [esi],edx
  0044eefe  7407                  je        short 0044EF07h
  0044ef00  c7407400000000        mov       dword [eax+74h],0

LAB_0044ef07:
  0044ef07  66ff4616              inc       word [esi+16h]
  0044ef0b  66817e16b80b          cmp       word [esi+16h],0BB8h
  0044ef11  7c06                  jl        short 0044EF19h
  0044ef13  66c746161400          mov       word [esi+16h],14h

LAB_0044ef19:
  0044ef19  5f                    pop       edi
  0044ef1a  5e                    pop       esi
  0044ef1b  5d                    pop       ebp
  0044ef1c  5b                    pop       ebx
  0044ef1d  83c430                add       esp,30h

22 cible(s) de saut interne : 0x0044eafb, 0x0044eb0e, 0x0044ebb4, 0x0044ebba, 0x0044ec03, 0x0044ec07, 0x0044ec2d, 0x0044ec41, 0x0044ec46, 0x0044eca4, 0x0044ed4e, 0x0044ed58, 0x0044ed5f, 0x0044ed6a, 0x0044ede5, 0x0044edfd, 0x0044ee53, 0x0044ee5f, 0x0044ee9e, 0x0044eed2, 0x0044ef07, 0x0044ef19
