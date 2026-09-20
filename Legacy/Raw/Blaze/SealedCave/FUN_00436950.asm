C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00436950  section .text  offset fichier 0x35d50  1900 octets

  00436950  83ec20                sub       esp,20h
  00436953  53                    push      ebx
  00436954  55                    push      ebp
  00436955  56                    push      esi
  00436956  8b742430              mov       esi,[esp+30h]
  0043695a  57                    push      edi
  0043695b  33d2                  xor       edx,edx
  0043695d  8b7e74                mov       edi,[esi+74h]
  00436960  3bfa                  cmp       edi,edx
  00436962  897c2434              mov       [esp+34h],edi
  00436966  7505                  jne       short 0043696Dh
  00436968  be58e55d00            mov       esi,5DE558h

LAB_0043696d:
  0043696d  8b5e6c                mov       ebx,[esi+6Ch]
  00436970  3bda                  cmp       ebx,edx
  00436972  895c2414              mov       [esp+14h],ebx
  00436976  750c                  jne       short 00436984h
  00436978  c744241458e55d00      mov       dword [esp+14h],5DE558h
  00436980  8b5c2414              mov       ebx,[esp+14h]

LAB_00436984:
  00436984  66395610              cmp       [esi+10h],dx
  00436988  7577                  jne       short 00436A01h
  0043698a  6639969a000000        cmp       [esi+9Ah],dx
  00436991  b8fa000000            mov       eax,0FAh
  00436996  b901000000            mov       ecx,1
  0043699b  66894628              mov       [esi+28h],ax
  0043699f  6689462c              mov       [esi+2Ch],ax
  004369a3  c6460400              mov       byte [esi+4],0
  004369a7  c6460a03              mov       byte [esi+0Ah],3
  004369ab  c6460b01              mov       byte [esi+0Bh],1
  004369af  66894e10              mov       [esi+10h],cx
  004369b3  6689562a              mov       [esi+2Ah],dx
  004369b7  66c746120a00          mov       word [esi+12h],0Ah
  004369bd  b096                  mov       al,96h
  004369bf  750b                  jne       short 004369CCh
  004369c1  c6463800              mov       byte [esi+38h],0
  004369c5  c6463982              mov       byte [esi+39h],82h
  004369c9  88463a                mov       [esi+3Ah],al

LAB_004369cc:
  004369cc  66398e9a000000        cmp       [esi+9Ah],cx
  004369d3  750b                  jne       short 004369E0h
  004369d5  884638                mov       [esi+38h],al
  004369d8  c6463900              mov       byte [esi+39h],0
  004369dc  c6463a00              mov       byte [esi+3Ah],0

LAB_004369e0:
  004369e0  6683be9a00000002      cmp       word [esi+9Ah],2
  004369e8  750a                  jne       short 004369F4h
  004369ea  884638                mov       [esi+38h],al
  004369ed  c6463900              mov       byte [esi+39h],0
  004369f1  88463a                mov       [esi+3Ah],al

LAB_004369f4:
  004369f4  806636fe              and       byte [esi+36h],0FEh
  004369f8  66895616              mov       [esi+16h],dx
  004369fc  895644                mov       [esi+44h],edx
  004369ff  eb05                  jmp       short 00436A06h

LAB_00436a01:
  00436a01  b901000000            mov       ecx,1

LAB_00436a06:
  00436a06  66394e10              cmp       [esi+10h],cx
  00436a0a  0f8575030000          jne       near 00436D85h
  00436a10  668b4732              mov       ax,[edi+32h]
  00436a14  66c746340004          mov       word [esi+34h],400h
  00436a1a  6605000c              add       ax,0C00h
  00436a1e  66894632              mov       [esi+32h],ax
  00436a22  8b4f20                mov       ecx,[edi+20h]
  00436a25  034b54                add       ecx,[ebx+54h]
  00436a28  894e20                mov       [esi+20h],ecx
  00436a2b  0fbf5732              movsx     edx,word [edi+32h]
  00436a2f  52                    push      edx
  00436a30  e8e1ef1000            call      00545A16h
  00436a35  8b4b50                mov       ecx,[ebx+50h]
  00436a38  8b6f1c                mov       ebp,[edi+1Ch]
  00436a3b  c1f90c                sar       ecx,0Ch
  00436a3e  0fbf5732              movsx     edx,word [edi+32h]
  00436a42  0fafc1                imul      eax,ecx
  00436a45  52                    push      edx
  00436a46  2be8                  sub       ebp,eax
  00436a48  e8cfef1000            call      00545A1Ch
  00436a4d  8b4b58                mov       ecx,[ebx+58h]
  00436a50  c1f90c                sar       ecx,0Ch
  00436a53  0fafc1                imul      eax,ecx
  00436a56  0fbf5632              movsx     edx,word [esi+32h]
  00436a5a  2be8                  sub       ebp,eax
  00436a5c  8b465c                mov       eax,[esi+5Ch]
  00436a5f  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  00436a66  51                    push      ecx
  00436a67  e8b0ef1000            call      00545A1Ch
  00436a6c  8d0440                lea       eax,[eax+eax*2]
  00436a6f  8d1480                lea       edx,[eax+eax*4]
  00436a72  03ea                  add       ebp,edx
  00436a74  896e1c                mov       [esi+1Ch],ebp
  00436a77  0fbf4732              movsx     eax,word [edi+32h]
  00436a7b  50                    push      eax
  00436a7c  e89bef1000            call      00545A1Ch
  00436a81  8b4b50                mov       ecx,[ebx+50h]
  00436a84  8be8                  mov       ebp,eax
  00436a86  c1f90c                sar       ecx,0Ch
  00436a89  0fbf5732              movsx     edx,word [edi+32h]
  00436a8d  0fafe9                imul      ebp,ecx
  00436a90  8b4f24                mov       ecx,[edi+24h]
  00436a93  52                    push      edx
  00436a94  03e9                  add       ebp,ecx
  00436a96  e87bef1000            call      00545A16h
  00436a9b  8b4b58                mov       ecx,[ebx+58h]
  00436a9e  c1f90c                sar       ecx,0Ch
  00436aa1  0fafc1                imul      eax,ecx
  00436aa4  0fbf5632              movsx     edx,word [esi+32h]
  00436aa8  2be8                  sub       ebp,eax
  00436aaa  8b465c                mov       eax,[esi+5Ch]
  00436aad  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  00436ab4  51                    push      ecx
  00436ab5  e85cef1000            call      00545A16h
  00436aba  8d0440                lea       eax,[eax+eax*2]
  00436abd  8d1480                lea       edx,[eax+eax*4]
  00436ac0  03ea                  add       ebp,edx
  00436ac2  66837e120a            cmp       word [esi+12h],0Ah
  00436ac7  896e24                mov       [esi+24h],ebp
  00436aca  7518                  jne       short 00436AE4h
  00436acc  6aff                  push      0FFFFFFFFh
  00436ace  6a00                  push      0
  00436ad0  68df000000            push      0DFh
  00436ad5  8d461c                lea       eax,[esi+1Ch]
  00436ad8  6a00                  push      0
  00436ada  50                    push      eax
  00436adb  56                    push      esi
  00436adc  e85f410400            call      0047AC40h
  00436ae1  83c418                add       esp,18h

LAB_00436ae4:
  00436ae4  8b461c                mov       eax,[esi+1Ch]
  00436ae7  8b4e20                mov       ecx,[esi+20h]
  00436aea  8b5624                mov       edx,[esi+24h]
  00436aed  89442424              mov       [esp+24h],eax
  00436af1  0fbf4632              movsx     eax,word [esi+32h]
  00436af5  2d000c0000            sub       eax,0C00h
  00436afa  894c2428              mov       [esp+28h],ecx
  00436afe  50                    push      eax
  00436aff  89542430              mov       [esp+30h],edx
  00436b03  e814ef1000            call      00545A1Ch
  00436b08  0fbf4e16              movsx     ecx,word [esi+16h]
  00436b0c  0fbf5632              movsx     edx,word [esi+32h]
  00436b10  0fafc1                imul      eax,ecx
  00436b13  8b4e1c                mov       ecx,[esi+1Ch]
  00436b16  81ea000c0000          sub       edx,0C00h
  00436b1c  03c8                  add       ecx,eax
  00436b1e  52                    push      edx
  00436b1f  894e1c                mov       [esi+1Ch],ecx
  00436b22  e8efee1000            call      00545A16h
  00436b27  0fbf4e16              movsx     ecx,word [esi+16h]
  00436b2b  0fafc1                imul      eax,ecx
  00436b2e  8b4e24                mov       ecx,[esi+24h]
  00436b31  c744241800000000      mov       dword [esp+18h],0
  00436b39  03c8                  add       ecx,eax
  00436b3b  b8c0935e00            mov       eax,5E93C0h
  00436b40  894e24                mov       [esi+24h],ecx
  00436b43  89442410              mov       [esp+10h],eax
  00436b47  c744241c04000000      mov       dword [esp+1Ch],4

LAB_00436b4f:
  00436b4f  8b08                  mov       ecx,[eax]
  00436b51  f7c100000080          test      ecx,80000000h
  00436b57  0f848b010000          je        near 00436CE8h
  00436b5d  f7c100000050          test      ecx,50000000h
  00436b63  0f857f010000          jne       near 00436CE8h
  00436b69  8b781c                mov       edi,[eax+1Ch]
  00436b6c  8b6e1c                mov       ebp,[esi+1Ch]
  00436b6f  8b5820                mov       ebx,[eax+20h]
  00436b72  8b5620                mov       edx,[esi+20h]
  00436b75  2bfd                  sub       edi,ebp
  00436b77  8b6824                mov       ebp,[eax+24h]
  00436b7a  8b4624                mov       eax,[esi+24h]
  00436b7d  2bda                  sub       ebx,edx
  00436b7f  2be8                  sub       ebp,eax
  00436b81  c1fd0c                sar       ebp,0Ch
  00436b84  c1ff0c                sar       edi,0Ch
  00436b87  55                    push      ebp
  00436b88  57                    push      edi
  00436b89  c1fb0c                sar       ebx,0Ch
  00436b8c  e891ee1000            call      00545A22h
  00436b91  8b542434              mov       edx,[esp+34h]
  00436b95  0fbf4a32              movsx     ecx,word [edx+32h]
  00436b99  2bc1                  sub       eax,ecx
  00436b9b  8bd7                  mov       edx,edi
  00436b9d  25ff0f0000            and       eax,0FFFh
  00436ba2  0fafd7                imul      edx,edi
  00436ba5  89442420              mov       [esp+20h],eax
  00436ba9  8bc3                  mov       eax,ebx
  00436bab  0fafc3                imul      eax,ebx
  00436bae  8bcd                  mov       ecx,ebp
  00436bb0  03d0                  add       edx,eax
  00436bb2  0fafcd                imul      ecx,ebp
  00436bb5  03d1                  add       edx,ecx
  00436bb7  52                    push      edx
  00436bb8  e853ee1000            call      00545A10h
  00436bbd  3dc8000000            cmp       eax,0C8h
  00436bc2  0f8d14010000          jge       near 00436CDCh
  00436bc8  8b442420              mov       eax,[esp+20h]
  00436bcc  3d20030000            cmp       eax,320h
  00436bd1  7d04                  jge       short 00436BD7h
  00436bd3  85c0                  test      eax,eax
  00436bd5  7f16                  jg        short 00436BEDh

LAB_00436bd7:
  00436bd7  3d00100000            cmp       eax,1000h
  00436bdc  0f8dfa000000          jge       near 00436CDCh
  00436be2  3de00c0000            cmp       eax,0CE0h
  00436be7  0f8eef000000          jle       near 00436CDCh

LAB_00436bed:
  00436bed  8b4c2418              mov       ecx,[esp+18h]
  00436bf1  b801000000            mov       eax,1
  00436bf6  d3e0                  shl       eax,cl
  00436bf8  8b4e44                mov       ecx,[esi+44h]
  00436bfb  85c1                  test      ecx,eax
  00436bfd  0f85d9000000          jne       near 00436CDCh
  00436c03  0bc8                  or        ecx,eax
  00436c05  6683be9a00000000      cmp       word [esi+9Ah],0
  00436c0d  894e44                mov       [esi+44h],ecx
  00436c10  7516                  jne       short 00436C28h
  00436c12  8b7c2410              mov       edi,[esp+10h]
  00436c16  8b5c2434              mov       ebx,[esp+34h]
  00436c1a  6a00                  push      0
  00436c1c  57                    push      edi
  00436c1d  53                    push      ebx
  00436c1e  e84df8ffff            call      00436470h
  00436c23  83c40c                add       esp,0Ch
  00436c26  eb08                  jmp       short 00436C30h

LAB_00436c28:
  00436c28  8b5c2434              mov       ebx,[esp+34h]
  00436c2c  8b7c2410              mov       edi,[esp+10h]

LAB_00436c30:
  00436c30  6683be9a00000001      cmp       word [esi+9Ah],1
  00436c38  0f8580000000          jne       near 00436CBEh
  00436c3e  e8fd041100            call      00547140h
  00436c43  99                    cdq
  00436c44  b909000000            mov       ecx,9
  00436c49  f7f9                  idiv      ecx
  00436c4b  0fbfc2                movsx     eax,dx
  00436c4e  83f808                cmp       eax,8
  00436c51  776b                  ja        short 00436CBEh
  00436c53  ff2485f06e4300        jmp       dword [eax*4+436EF0h]
  00436c5a  6a00                  push      0
  00436c5c  57                    push      edi
  00436c5d  53                    push      ebx
  00436c5e  e8edfaffff            call      00436750h
  00436c63  eb56                  jmp       short 00436CBBh
  00436c65  6a00                  push      0
  00436c67  57                    push      edi
  00436c68  53                    push      ebx
  00436c69  e802f8ffff            call      00436470h
  00436c6e  eb4b                  jmp       short 00436CBBh
  00436c70  6a00                  push      0
  00436c72  57                    push      edi
  00436c73  53                    push      ebx
  00436c74  e817faffff            call      00436690h
  00436c79  eb40                  jmp       short 00436CBBh
  00436c7b  6a00                  push      0
  00436c7d  57                    push      edi
  00436c7e  53                    push      ebx
  00436c7f  e88cf8ffff            call      00436510h
  00436c84  eb35                  jmp       short 00436CBBh
  00436c86  6a00                  push      0
  00436c88  57                    push      edi
  00436c89  53                    push      ebx
  00436c8a  e801f9ffff            call      00436590h
  00436c8f  eb2a                  jmp       short 00436CBBh
  00436c91  6a00                  push      0
  00436c93  57                    push      edi
  00436c94  53                    push      ebx
  00436c95  e8b6f8ffff            call      00436550h
  00436c9a  eb1f                  jmp       short 00436CBBh
  00436c9c  6a00                  push      0
  00436c9e  57                    push      edi
  00436c9f  53                    push      ebx
  00436ca0  e82bf9ffff            call      004365D0h
  00436ca5  eb14                  jmp       short 00436CBBh
  00436ca7  6a00                  push      0
  00436ca9  57                    push      edi
  00436caa  53                    push      ebx
  00436cab  e8b0020000            call      00436F60h
  00436cb0  eb09                  jmp       short 00436CBBh
  00436cb2  6a00                  push      0
  00436cb4  57                    push      edi
  00436cb5  53                    push      ebx
  00436cb6  e865020000            call      00436F20h

LAB_00436cbb:
  00436cbb  83c40c                add       esp,0Ch

LAB_00436cbe:
  00436cbe  6683be9a00000002      cmp       word [esi+9Ah],2
  00436cc6  7514                  jne       short 00436CDCh
  00436cc8  8b542410              mov       edx,[esp+10h]
  00436ccc  8b442434              mov       eax,[esp+34h]
  00436cd0  6a00                  push      0
  00436cd2  52                    push      edx
  00436cd3  50                    push      eax
  00436cd4  e837f8ffff            call      00436510h
  00436cd9  83c40c                add       esp,0Ch

LAB_00436cdc:
  00436cdc  8b7c2434              mov       edi,[esp+34h]
  00436ce0  8b442410              mov       eax,[esp+10h]
  00436ce4  8b5c2414              mov       ebx,[esp+14h]

LAB_00436ce8:
  00436ce8  8b6c2418              mov       ebp,[esp+18h]
  00436cec  8b4c241c              mov       ecx,[esp+1Ch]
  00436cf0  45                    inc       ebp
  00436cf1  059c000000            add       eax,9Ch
  00436cf6  49                    dec       ecx
  00436cf7  896c2418              mov       [esp+18h],ebp
  00436cfb  89442410              mov       [esp+10h],eax
  00436cff  894c241c              mov       [esp+1Ch],ecx
  00436d03  0f8546feffff          jne       near 00436B4Fh
  00436d09  56                    push      esi
  00436d0a  e831cc0f00            call      00533940h
  00436d0f  8b442430              mov       eax,[esp+30h]
  00436d13  8b4c2428              mov       ecx,[esp+28h]
  00436d17  8b54242c              mov       edx,[esp+2Ch]
  00436d1b  894624                mov       [esi+24h],eax
  00436d1e  668b462a              mov       ax,[esi+2Ah]
  00436d22  894e1c                mov       [esi+1Ch],ecx
  00436d25  0fbfc8                movsx     ecx,ax
  00436d28  895620                mov       [esi+20h],edx
  00436d2b  ba00400000            mov       edx,4000h
  00436d30  2bd1                  sub       edx,ecx
  00436d32  83c404                add       esp,4
  00436d35  c1fa02                sar       edx,2
  00436d38  03d0                  add       edx,eax
  00436d3a  668b4616              mov       ax,[esi+16h]
  00436d3e  0fbfc8                movsx     ecx,ax
  00436d41  6689562a              mov       [esi+2Ah],dx
  00436d45  ba58020000            mov       edx,258h
  00436d4a  2bd1                  sub       edx,ecx
  00436d4c  c1fa02                sar       edx,2
  00436d4f  03d0                  add       edx,eax
  00436d51  8b8690000000          mov       eax,[esi+90h]
  00436d57  a902880000            test      eax,8802h
  00436d5c  66895616              mov       [esi+16h],dx
  00436d60  740c                  je        short 00436D6Eh
  00436d62  66c746100200          mov       word [esi+10h],2
  00436d68  66c746120a00          mov       word [esi+12h],0Ah

LAB_00436d6e:
  00436d6e  66ff4e12              dec       word [esi+12h]
  00436d72  66837e1200            cmp       word [esi+12h],0
  00436d77  7f0c                  jg        short 00436D85h
  00436d79  66c746100200          mov       word [esi+10h],2
  00436d7f  66c746120a00          mov       word [esi+12h],0Ah

LAB_00436d85:
  00436d85  66837e1002            cmp       word [esi+10h],2
  00436d8a  0f8557010000          jne       near 00436EE7h
  00436d90  668b4732              mov       ax,[edi+32h]
  00436d94  66c746340004          mov       word [esi+34h],400h
  00436d9a  6605000c              add       ax,0C00h
  00436d9e  66894632              mov       [esi+32h],ax
  00436da2  8b4f20                mov       ecx,[edi+20h]
  00436da5  0fbf5632              movsx     edx,word [esi+32h]
  00436da9  8b4354                mov       eax,[ebx+54h]
  00436dac  52                    push      edx
  00436dad  03c8                  add       ecx,eax
  00436daf  894e20                mov       [esi+20h],ecx
  00436db2  e85fec1000            call      00545A16h
  00436db7  8b4b50                mov       ecx,[ebx+50h]
  00436dba  8b6f1c                mov       ebp,[edi+1Ch]
  00436dbd  c1f90c                sar       ecx,0Ch
  00436dc0  0fbf5732              movsx     edx,word [edi+32h]
  00436dc4  0fafc1                imul      eax,ecx
  00436dc7  52                    push      edx
  00436dc8  2be8                  sub       ebp,eax
  00436dca  e84dec1000            call      00545A1Ch
  00436dcf  8b4b58                mov       ecx,[ebx+58h]
  00436dd2  c1f90c                sar       ecx,0Ch
  00436dd5  0fafc1                imul      eax,ecx
  00436dd8  0fbf5632              movsx     edx,word [esi+32h]
  00436ddc  2be8                  sub       ebp,eax
  00436dde  8b465c                mov       eax,[esi+5Ch]
  00436de1  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  00436de8  51                    push      ecx
  00436de9  e82eec1000            call      00545A1Ch
  00436dee  8d0440                lea       eax,[eax+eax*2]
  00436df1  8d1480                lea       edx,[eax+eax*4]
  00436df4  0fbf4632              movsx     eax,word [esi+32h]
  00436df8  03ea                  add       ebp,edx
  00436dfa  50                    push      eax
  00436dfb  896e1c                mov       [esi+1Ch],ebp
  00436dfe  e819ec1000            call      00545A1Ch
  00436e03  8b4b50                mov       ecx,[ebx+50h]
  00436e06  8be8                  mov       ebp,eax
  00436e08  c1f90c                sar       ecx,0Ch
  00436e0b  0fbf5732              movsx     edx,word [edi+32h]
  00436e0f  0fafe9                imul      ebp,ecx
  00436e12  8b4f24                mov       ecx,[edi+24h]
  00436e15  52                    push      edx
  00436e16  03e9                  add       ebp,ecx
  00436e18  e8f9eb1000            call      00545A16h
  00436e1d  8b4b58                mov       ecx,[ebx+58h]
  00436e20  c1f90c                sar       ecx,0Ch
  00436e23  0fafc1                imul      eax,ecx
  00436e26  0fbf5632              movsx     edx,word [esi+32h]
  00436e2a  2be8                  sub       ebp,eax
  00436e2c  8b465c                mov       eax,[esi+5Ch]
  00436e2f  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  00436e36  51                    push      ecx
  00436e37  e8daeb1000            call      00545A16h
  00436e3c  8d0440                lea       eax,[eax+eax*2]
  00436e3f  8d1480                lea       edx,[eax+eax*4]
  00436e42  0fbf4632              movsx     eax,word [esi+32h]
  00436e46  2d000c0000            sub       eax,0C00h
  00436e4b  03ea                  add       ebp,edx
  00436e4d  50                    push      eax
  00436e4e  896e24                mov       [esi+24h],ebp
  00436e51  e8c6eb1000            call      00545A1Ch
  00436e56  0fbf4e16              movsx     ecx,word [esi+16h]
  00436e5a  0fafc1                imul      eax,ecx
  00436e5d  01461c                add       [esi+1Ch],eax
  00436e60  0fbf5632              movsx     edx,word [esi+32h]
  00436e64  81ea000c0000          sub       edx,0C00h
  00436e6a  52                    push      edx
  00436e6b  e8a6eb1000            call      00545A16h
  00436e70  0fbf4e16              movsx     ecx,word [esi+16h]
  00436e74  0fafc1                imul      eax,ecx
  00436e77  8b6e24                mov       ebp,[esi+24h]
  00436e7a  66c74634000c          mov       word [esi+34h],0C00h
  00436e80  03e8                  add       ebp,eax
  00436e82  668b462a              mov       ax,[esi+2Ah]
  00436e86  0fbfd0                movsx     edx,ax
  00436e89  f7da                  neg       edx
  00436e8b  c1fa02                sar       edx,2
  00436e8e  896e24                mov       [esi+24h],ebp
  00436e91  03d0                  add       edx,eax
  00436e93  8a4638                mov       al,[esi+38h]
  00436e96  6689562a              mov       [esi+2Ah],dx
  00436e9a  8bc8                  mov       ecx,eax
  00436e9c  81e1ff000000          and       ecx,0FFh
  00436ea2  f7d9                  neg       ecx
  00436ea4  c1f903                sar       ecx,3
  00436ea7  02c8                  add       cl,al
  00436ea9  8a4639                mov       al,[esi+39h]
  00436eac  8bd0                  mov       edx,eax
  00436eae  884e38                mov       [esi+38h],cl
  00436eb1  81e2ff000000          and       edx,0FFh
  00436eb7  f7da                  neg       edx
  00436eb9  c1fa03                sar       edx,3
  00436ebc  02d0                  add       dl,al
  00436ebe  8a463a                mov       al,[esi+3Ah]
  00436ec1  8bc8                  mov       ecx,eax
  00436ec3  885639                mov       [esi+39h],dl
  00436ec6  81e1ff000000          and       ecx,0FFh
  00436ecc  f7d9                  neg       ecx
  00436ece  c1f903                sar       ecx,3
  00436ed1  02c8                  add       cl,al
  00436ed3  66ff4e12              dec       word [esi+12h]
  00436ed7  66837e1200            cmp       word [esi+12h],0
  00436edc  884e3a                mov       [esi+3Ah],cl
  00436edf  7f06                  jg        short 00436EE7h
  00436ee1  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00436ee7:
  00436ee7  5f                    pop       edi
  00436ee8  5e                    pop       esi
  00436ee9  5d                    pop       ebp
  00436eea  5b                    pop       ebx
  00436eeb  83c420                add       esp,20h
  00436eee  c3                    ret
  00436eef  90                    nop
  00436ef0  5a                    pop       edx
  00436ef1  6c                    insb
  00436ef2  43                    inc       ebx
  00436ef3  00656c                add       [ebp+6Ch],ah
  00436ef6  43                    inc       ebx
  00436ef7  00706c                add       [eax+6Ch],dh
  00436efa  43                    inc       ebx
  00436efb  007b6c                add       [ebx+6Ch],bh
  00436efe  43                    inc       ebx
  00436eff  00866c430091          add       [esi-6EFFBC94h],al
  00436f05  6c                    insb
  00436f06  43                    inc       ebx
  00436f07  009c6c4300a76c        add       [esp+ebp*2+6CA70043h],bl
  00436f0e  43                    inc       ebx
  00436f0f  00b26c430090          add       [edx-6FFFBC94h],dh
  00436f15  90                    nop
  00436f16  90                    nop
  00436f17  90                    nop
  00436f18  90                    nop
  00436f19  90                    nop
  00436f1a  90                    nop
  00436f1b  90                    nop
  00436f1c  90                    nop
  00436f1d  90                    nop
  00436f1e  90                    nop
  00436f1f  90                    nop
  00436f20  56                    push      esi
  00436f21  8b74240c              mov       esi,[esp+0Ch]
  00436f25  57                    push      edi
  00436f26  8b7c240c              mov       edi,[esp+0Ch]
  00436f2a  56                    push      esi
  00436f2b  57                    push      edi
  00436f2c  e87ff5ffff            call      004364B0h
  00436f31  83c408                add       esp,8
  00436f34  6685c0                test      ax,ax
  00436f37  751d                  jne       short 00436F56h
  00436f39  8b442414              mov       eax,[esp+14h]
  00436f3d  50                    push      eax
  00436f3e  56                    push      esi
  00436f3f  57                    push      edi
  00436f40  e87b4a1000            call      0053B9C0h
  00436f45  83c40c                add       esp,0Ch
  00436f48  33c9                  xor       ecx,ecx
  00436f4a  6685c0                test      ax,ax
  00436f4d  0f95c1                setne     cl
  00436f50  668bc1                mov       ax,cx
  00436f53  5f                    pop       edi
  00436f54  5e                    pop       esi
  00436f55  c3                    ret

LAB_00436f56:
  00436f56  5f                    pop       edi
  00436f57  66b80100              mov       ax,1
  00436f5b  5e                    pop       esi
  00436f5c  c3                    ret
  00436f5d  90                    nop
  00436f5e  90                    nop
  00436f5f  90                    nop
  00436f60  56                    push      esi
  00436f61  8b74240c              mov       esi,[esp+0Ch]
  00436f65  57                    push      edi
  00436f66  8b7c240c              mov       edi,[esp+0Ch]
  00436f6a  56                    push      esi
  00436f6b  57                    push      edi
  00436f6c  e83ff5ffff            call      004364B0h
  00436f71  83c408                add       esp,8
  00436f74  6685c0                test      ax,ax
  00436f77  751d                  jne       short 00436F96h
  00436f79  8b442414              mov       eax,[esp+14h]
  00436f7d  50                    push      eax
  00436f7e  56                    push      esi
  00436f7f  57                    push      edi
  00436f80  e88b551000            call      0053C510h
  00436f85  83c40c                add       esp,0Ch
  00436f88  33c9                  xor       ecx,ecx
  00436f8a  6685c0                test      ax,ax
  00436f8d  0f95c1                setne     cl
  00436f90  668bc1                mov       ax,cx
  00436f93  5f                    pop       edi
  00436f94  5e                    pop       esi
  00436f95  c3                    ret

LAB_00436f96:
  00436f96  5f                    pop       edi
  00436f97  66b80100              mov       ax,1
  00436f9b  5e                    pop       esi
  00436f9c  c3                    ret
  00436f9d  90                    nop
  00436f9e  90                    nop
  00436f9f  90                    nop
  00436fa0  53                    push      ebx
  00436fa1  55                    push      ebp
  00436fa2  56                    push      esi
  00436fa3  8b742410              mov       esi,[esp+10h]
  00436fa7  57                    push      edi
  00436fa8  8b6e74                mov       ebp,[esi+74h]
  00436fab  85ed                  test      ebp,ebp
  00436fad  7505                  jne       short 00436FB4h
  00436faf  bd58e55d00            mov       ebp,5DE558h

LAB_00436fb4:
  00436fb4  8b7e6c                mov       edi,[esi+6Ch]
  00436fb7  85ff                  test      edi,edi
  00436fb9  7505                  jne       short 00436FC0h
  00436fbb  bf58e55d00            mov       edi,5DE558h

LAB_00436fc0:
  00436fc0  8b4520                mov       eax,[ebp+20h]
  00436fc3  8b4f54                mov       ecx,[edi+54h]
  00436fc6  03c1                  add       eax,ecx
  00436fc8  894620                mov       [esi+20h],eax
  00436fcb  0fbf4d32              movsx     ecx,word [ebp+32h]
  00436fcf  51                    push      ecx
  00436fd0  e847ea1000            call      00545A1Ch
  00436fd5  8b5758                mov       edx,[edi+58h]
  00436fd8  8b5d1c                mov       ebx,[ebp+1Ch]
  00436fdb  c1fa0c                sar       edx,0Ch
  00436fde  0fafc2                imul      eax,edx
  00436fe1  2bd8                  sub       ebx,eax
  00436fe3  0fbf4632              movsx     eax,word [esi+32h]
  00436fe7  50                    push      eax
  00436fe8  e829ea1000            call      00545A16h
  00436fed  8b4f50                mov       ecx,[edi+50h]
  00436ff0  c1f90c                sar       ecx,0Ch
  00436ff3  0fafc1                imul      eax,ecx
  00436ff6  0fbf5632              movsx     edx,word [esi+32h]
  00436ffa  2bd8                  sub       ebx,eax
  00436ffc  8b465c                mov       eax,[esi+5Ch]
  00436fff  8d8c0200f4ffff        lea       ecx,[edx+eax-0C00h]
  00437006  51                    push      ecx
  00437007  e810ea1000            call      00545A1Ch
  0043700c  8d0440                lea       eax,[eax+eax*2]
  0043700f  8d1480                lea       edx,[eax+eax*4]
  00437012  0fbf4632              movsx     eax,word [esi+32h]
  00437016  03da                  add       ebx,edx
  00437018  50                    push      eax
  00437019  895e1c                mov       [esi+1Ch],ebx
  0043701c  e8fbe91000            call      00545A1Ch
  00437021  8b4f50                mov       ecx,[edi+50h]
  00437024  8bd8                  mov       ebx,eax
  00437026  c1f90c                sar       ecx,0Ch
  00437029  0fbf5532              movsx     edx,word [ebp+32h]
  0043702d  0fafd9                imul      ebx,ecx
  00437030  8b4d24                mov       ecx,[ebp+24h]
  00437033  52                    push      edx
  00437034  03d9                  add       ebx,ecx
  00437036  e8dbe91000            call      00545A16h
  0043703b  8b4f58                mov       ecx,[edi+58h]
  0043703e  c1f90c                sar       ecx,0Ch
  00437041  0fafc1                imul      eax,ecx
  00437044  0fbf5632              movsx     edx,word [esi+32h]
  00437048  2bd8                  sub       ebx,eax
  0043704a  8b465c                mov       eax,[esi+5Ch]
  0043704d  8d8c0200f4ffff        lea       ecx,[edx+eax-0C00h]
  00437054  51                    push      ecx
  00437055  e8bce91000            call      00545A16h
  0043705a  8d0440                lea       eax,[eax+eax*2]
  0043705d  bf01000000            mov       edi,1
  00437062  8d1480                lea       edx,[eax+eax*4]
  00437065  03da                  add       ebx,edx
  00437067  895e24                mov       [esi+24h],ebx
  0043706a  33db                  xor       ebx,ebx
  0043706c  66395e10              cmp       [esi+10h],bx
  00437070  7535                  jne       short 004370A7h
  00437072  c7463cb80f5500        mov       dword [esi+3Ch],550FB8h
  00437079  e8c2001100            call      00547140h
  0043707e  99                    cdq
  0043707f  b903000000            mov       ecx,3
  00437084  c6460a02              mov       byte [esi+0Ah],2
  00437088  f7f9                  idiv      ecx
  0043708a  b8a00f0000            mov       eax,0FA0h
  0043708f  885e0b                mov       [esi+0Bh],bl
  00437092  66894628              mov       [esi+28h],ax
  00437096  6689462a              mov       [esi+2Ah],ax
  0043709a  66897e10              mov       [esi+10h],di
  0043709e  66c746120b00          mov       word [esi+12h],0Bh
  004370a4  885640                mov       [esi+40h],dl

LAB_004370a7:
  004370a7  66397e10              cmp       [esi+10h],di
  004370ab  7525                  jne       short 004370D2h
  004370ad  8a5640                mov       dl,[esi+40h]
  004370b0  66ff4e12              dec       word [esi+12h]
  004370b4  668b4612              mov       ax,[esi+12h]
  004370b8  fec2                  inc       dl
  004370ba  8aca                  mov       cl,dl

25 cible(s) de saut interne : 0x0043696d, 0x00436984, 0x004369cc, 0x004369e0, 0x004369f4, 0x00436a01, 0x00436a06, 0x00436ae4, 0x00436b4f, 0x00436bd7, 0x00436bed, 0x00436c28, 0x00436c30, 0x00436cbb, 0x00436cbe, 0x00436cdc, 0x00436ce8, 0x00436d6e, 0x00436d85, 0x00436ee7, 0x00436f56, 0x00436f96, 0x00436fb4, 0x00436fc0, 0x004370a7
