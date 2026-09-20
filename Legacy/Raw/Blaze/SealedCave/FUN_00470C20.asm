C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470c20  section .text  offset fichier 0x70020  1328 octets

  00470c20  8b0d78195700          mov       ecx,[571978h]
  00470c26  83ec1c                sub       esp,1Ch
  00470c29  b800100000            mov       eax,1000h
  00470c2e  53                    push      ebx
  00470c2f  55                    push      ebp
  00470c30  56                    push      esi
  00470c31  8b74242c              mov       esi,[esp+2Ch]
  00470c35  85c8                  test      eax,ecx
  00470c37  57                    push      edi
  00470c38  bd00200000            mov       ebp,2000h
  00470c3d  7502                  jne       short 00470C41h
  00470c3f  092e                  or        [esi],ebp

LAB_00470c41:
  00470c41  850578195700          test      [571978h],eax
  00470c47  7407                  je        short 00470C50h
  00470c49  8b06                  mov       eax,[esi]
  00470c4b  80e4df                and       ah,0DFh
  00470c4e  8906                  mov       [esi],eax

LAB_00470c50:
  00470c50  852d78195700          test      [571978h],ebp
  00470c56  8b06                  mov       eax,[esi]
  00470c58  750d                  jne       short 00470C67h
  00470c5a  80cc40                or        ah,40h
  00470c5d  8906                  mov       [esi],eax
  00470c5f  5f                    pop       edi
  00470c60  5e                    pop       esi
  00470c61  5d                    pop       ebp
  00470c62  5b                    pop       ebx
  00470c63  83c41c                add       esp,1Ch
  00470c66  c3                    ret

LAB_00470c67:
  00470c67  80e49f                and       ah,9Fh
  00470c6a  66ff8698000000        inc       word [esi+98h]
  00470c71  668b8e98000000        mov       cx,[esi+98h]
  00470c78  8906                  mov       [esi],eax
  00470c7a  8b3d20195700          mov       edi,[571920h]
  00470c80  b83c000000            mov       eax,3Ch
  00470c85  2bc7                  sub       eax,edi
  00470c87  83f803                cmp       eax,3
  00470c8a  7d05                  jge       short 00470C91h
  00470c8c  b803000000            mov       eax,3

LAB_00470c91:
  00470c91  0fbfc9                movsx     ecx,cx
  00470c94  3bc8                  cmp       ecx,eax
  00470c96  7e4f                  jle       short 00470CE7h
  00470c98  a178195700            mov       eax,[571978h]
  00470c9d  f6c402                test      ah,2
  00470ca0  7545                  jne       short 00470CE7h
  00470ca2  33ff                  xor       edi,edi
  00470ca4  6800120000            push      1200h
  00470ca9  6800120000            push      1200h
  00470cae  6689be98000000        mov       [esi+98h],di
  00470cb5  e886640d00            call      00547140h
  00470cba  99                    cdq
  00470cbb  b964000000            mov       ecx,64h
  00470cc0  f7f9                  idiv      ecx
  00470cc2  81c2ce020000          add       edx,2CEh
  00470cc8  52                    push      edx
  00470cc9  8b15cc975e00          mov       edx,[5E97CCh]
  00470ccf  8b82a8000000          mov       eax,[edx+0A8h]
  00470cd5  33d2                  xor       edx,edx
  00470cd7  8b08                  mov       ecx,[eax]
  00470cd9  8a5114                mov       dl,[ecx+14h]
  00470cdc  52                    push      edx
  00470cdd  e8bec10c00            call      0053CEA0h
  00470ce2  83c410                add       esp,10h
  00470ce5  eb02                  jmp       short 00470CE9h

LAB_00470ce7:
  00470ce7  33ff                  xor       edi,edi

LAB_00470ce9:
  00470ce9  83c8ff                or        eax,0FFFFFFFFh
  00470cec  66397e10              cmp       [esi+10h],di
  00470cf0  751f                  jne       short 00470D11h
  00470cf2  66894612              mov       [esi+12h],ax
  00470cf6  c705201957001e000000  mov       dword [571920h],1Eh
  00470d00  bb01000000            mov       ebx,1
  00470d05  66897e16              mov       [esi+16h],di
  00470d09  891d3c195700          mov       [57193Ch],ebx
  00470d0f  eb05                  jmp       short 00470D16h

LAB_00470d11:
  00470d11  bb01000000            mov       ebx,1

LAB_00470d16:
  00470d16  8b4e7c                mov       ecx,[esi+7Ch]
  00470d19  668b9690000000        mov       dx,[esi+90h]
  00470d20  894c2430              mov       [esp+30h],ecx
  00470d24  8b4e60                mov       ecx,[esi+60h]
  00470d27  6689542410            mov       [esp+10h],dx
  00470d2c  8b5664                mov       edx,[esi+64h]
  00470d2f  894c2414              mov       [esp+14h],ecx
  00470d33  8b4e1c                mov       ecx,[esi+1Ch]
  00470d36  66ff4610              inc       word [esi+10h]
  00470d3a  89542418              mov       [esp+18h],edx
  00470d3e  8b5620                mov       edx,[esi+20h]
  00470d41  894e44                mov       [esi+44h],ecx
  00470d44  8b4e24                mov       ecx,[esi+24h]
  00470d47  66817e103075          cmp       word [esi+10h],7530h
  00470d4d  895648                mov       [esi+48h],edx
  00470d50  8b9680000000          mov       edx,[esi+80h]
  00470d56  894e4c                mov       [esi+4Ch],ecx
  00470d59  8b8e84000000          mov       ecx,[esi+84h]
  00470d5f  8954241c              mov       [esp+1Ch],edx
  00470d63  8b9688000000          mov       edx,[esi+88h]
  00470d69  894c2420              mov       [esp+20h],ecx
  00470d6d  8b8e8c000000          mov       ecx,[esi+8Ch]
  00470d73  89542424              mov       [esp+24h],edx
  00470d77  894c2428              mov       [esp+28h],ecx
  00470d7b  7c06                  jl        short 00470D83h
  00470d7d  66c74610204e          mov       word [esi+10h],4E20h

LAB_00470d83:
  00470d83  66394612              cmp       [esi+12h],ax
  00470d87  0f859d000000          jne       near 00470E2Ah
  00470d8d  6800000040            push      40000000h
  00470d92  6a10                  push      10h
  00470d94  56                    push      esi
  00470d95  e866420c00            call      00535000h
  00470d9a  83c40c                add       esp,0Ch
  00470d9d  8be8                  mov       ebp,eax
  00470d9f  e8cc5fffff            call      00466D70h
  00470da4  0fbfd0                movsx     edx,ax
  00470da7  3bd5                  cmp       edx,ebp
  00470da9  757a                  jne       short 00470E25h
  00470dab  668b4612              mov       ax,[esi+12h]
  00470daf  66895e10              mov       [esi+10h],bx
  00470db3  66f7d8                neg       ax
  00470db6  66894612              mov       [esi+12h],ax
  00470dba  c7052019570028000000  mov       dword [571920h],28h
  00470dc4  893d74195700          mov       [571974h],edi
  00470dca  66897e16              mov       [esi+16h],di
  00470dce  893d30195700          mov       [571930h],edi
  00470dd4  66897e14              mov       [esi+14h],di
  00470dd8  8b2de0a55f00          mov       ebp,[5FA5E0h]
  00470dde  8b1550a85f00          mov       edx,[5FA850h]
  00470de4  8b0deca85f00          mov       ecx,[5FA8ECh]
  00470dea  a188a95f00            mov       eax,[5FA988h]
  00470def  81e5ffffff7f          and       ebp,7FFFFFFFh
  00470df5  893d6c195700          mov       [57196Ch],edi
  00470dfb  892de0a55f00          mov       [5FA5E0h],ebp
  00470e01  bd00200000            mov       ebp,2000h
  00470e06  0bd5                  or        edx,ebp
  00470e08  0bcd                  or        ecx,ebp
  00470e0a  0bc5                  or        eax,ebp
  00470e0c  893d70195700          mov       [571970h],edi
  00470e12  891550a85f00          mov       [5FA850h],edx
  00470e18  890deca85f00          mov       [5FA8ECh],ecx
  00470e1e  a388a95f00            mov       [5FA988h],eax
  00470e23  eb05                  jmp       short 00470E2Ah

LAB_00470e25:
  00470e25  bd00200000            mov       ebp,2000h

LAB_00470e2a:
  00470e2a  66395e12              cmp       [esi+12h],bx
  00470e2e  0f8514030000          jne       near 00471148h
  00470e34  a134bb5f00            mov       eax,[5FBB34h]
  00470e39  8b0d20195700          mov       ecx,[571920h]
  00470e3f  8b1574195700          mov       edx,[571974h]
  00470e45  57                    push      edi
  00470e46  0bc5                  or        eax,ebp
  00470e48  03d1                  add       edx,ecx
  00470e4a  57                    push      edi
  00470e4b  56                    push      esi
  00470e4c  a334bb5f00            mov       [5FBB34h],eax
  00470e51  891574195700          mov       [571974h],edx
  00470e57  e8740f0000            call      00471DD0h
  00470e5c  83c40c                add       esp,0Ch
  00470e5f  57                    push      edi
  00470e60  57                    push      edi
  00470e61  56                    push      esi
  00470e62  e839170000            call      004725A0h
  00470e67  83c40c                add       esp,0Ch
  00470e6a  66395e16              cmp       [esi+16h],bx
  00470e6e  7535                  jne       short 00470EA5h
  00470e70  0fbf4e10              movsx     ecx,word [esi+10h]
  00470e74  8bc1                  mov       eax,ecx
  00470e76  99                    cdq
  00470e77  83e203                and       edx,3
  00470e7a  03c2                  add       eax,edx
  00470e7c  c1f802                sar       eax,2
  00470e7f  c1e002                shl       eax,2
  00470e82  3bc8                  cmp       ecx,eax
  00470e84  751f                  jne       short 00470EA5h
  00470e86  8b3d20195700          mov       edi,[571920h]
  00470e8c  4f                    dec       edi
  00470e8d  83ff05                cmp       edi,5
  00470e90  893d20195700          mov       [571920h],edi
  00470e96  7f13                  jg        short 00470EABh
  00470e98  bf05000000            mov       edi,5
  00470e9d  893d20195700          mov       [571920h],edi
  00470ea3  eb06                  jmp       short 00470EABh

LAB_00470ea5:
  00470ea5  8b3d20195700          mov       edi,[571920h]

LAB_00470eab:
  00470eab  668b4616              mov       ax,[esi+16h]
  00470eaf  663d0300              cmp       ax,3
  00470eb3  7c06                  jl        short 00470EBBh
  00470eb5  663d0600              cmp       ax,6
  00470eb9  7e12                  jle       short 00470ECDh

LAB_00470ebb:
  00470ebb  663d0b00              cmp       ax,0Bh
  00470ebf  740c                  je        short 00470ECDh
  00470ec1  663d1400              cmp       ax,14h
  00470ec5  7406                  je        short 00470ECDh
  00470ec7  663d1800              cmp       ax,18h
  00470ecb  7528                  jne       short 00470EF5h

LAB_00470ecd:
  00470ecd  0fbf4e10              movsx     ecx,word [esi+10h]
  00470ed1  8bc1                  mov       eax,ecx
  00470ed3  99                    cdq
  00470ed4  2bc2                  sub       eax,edx
  00470ed6  d1f8                  sar       eax,1
  00470ed8  d1e0                  shl       eax,1
  00470eda  3bc8                  cmp       ecx,eax
  00470edc  7517                  jne       short 00470EF5h
  00470ede  47                    inc       edi
  00470edf  83ff28                cmp       edi,28h
  00470ee2  893d20195700          mov       [571920h],edi
  00470ee8  7c0b                  jl        short 00470EF5h
  00470eea  bf28000000            mov       edi,28h
  00470eef  893d20195700          mov       [571920h],edi

LAB_00470ef5:
  00470ef5  668b4616              mov       ax,[esi+16h]
  00470ef9  663d0800              cmp       ax,8
  00470efd  7c06                  jl        short 00470F05h
  00470eff  663d0a00              cmp       ax,0Ah
  00470f03  7e12                  jle       short 00470F17h

LAB_00470f05:
  00470f05  663d1200              cmp       ax,12h
  00470f09  740c                  je        short 00470F17h
  00470f0b  663d1600              cmp       ax,16h
  00470f0f  7406                  je        short 00470F17h
  00470f11  663d1a00              cmp       ax,1Ah
  00470f15  752f                  jne       short 00470F46h

LAB_00470f17:
  00470f17  0fbf4e10              movsx     ecx,word [esi+10h]
  00470f1b  b856555555            mov       eax,55555556h
  00470f20  f7e9                  imul      ecx
  00470f22  8bc2                  mov       eax,edx
  00470f24  c1e81f                shr       eax,1Fh
  00470f27  03d0                  add       edx,eax
  00470f29  8d1452                lea       edx,[edx+edx*2]
  00470f2c  3bca                  cmp       ecx,edx
  00470f2e  7516                  jne       short 00470F46h
  00470f30  4f                    dec       edi
  00470f31  83ff05                cmp       edi,5
  00470f34  893d20195700          mov       [571920h],edi
  00470f3a  7f0a                  jg        short 00470F46h
  00470f3c  c7052019570005000000  mov       dword [571920h],5

LAB_00470f46:
  00470f46  0fbf4632              movsx     eax,word [esi+32h]
  00470f4a  50                    push      eax
  00470f4b  e8cc4a0d00            call      00545A1Ch
  00470f50  0fbf4e32              movsx     ecx,word [esi+32h]
  00470f54  8bf8                  mov       edi,eax
  00470f56  51                    push      ecx
  00470f57  0faf3d20195700        imul      edi,[571920h]
  00470f5e  e8b34a0d00            call      00545A16h
  00470f63  0faf0520195700        imul      eax,[571920h]
  00470f6a  8b5624                mov       edx,[esi+24h]
  00470f6d  8b4e1c                mov       ecx,[esi+1Ch]
  00470f70  03d7                  add       edx,edi
  00470f72  8b7e20                mov       edi,[esi+20h]
  00470f75  895624                mov       [esi+24h],edx
  00470f78  8b567c                mov       edx,[esi+7Ch]
  00470f7b  2bc8                  sub       ecx,eax
  00470f7d  33c0                  xor       eax,eax
  00470f7f  894e1c                mov       [esi+1Ch],ecx
  00470f82  8a420e                mov       al,[edx+0Eh]
  00470f85  8d8f00800c00          lea       ecx,[edi+0C8000h]
  00470f8b  56                    push      esi
  00470f8c  668bac8680000000      mov       bp,[esi+eax*4+80h]
  00470f94  894e20                mov       [esi+20h],ecx
  00470f97  e8a4290c00            call      00533940h
  00470f9c  8b5660                mov       edx,[esi+60h]
  00470f9f  83c404                add       esp,4
  00470fa2  895620                mov       [esi+20h],edx
  00470fa5  a120195700            mov       eax,[571920h]
  00470faa  a9fcffffff            test      eax,0FFFFFFFCh
  00470faf  7405                  je        short 00470FB6h
  00470fb1  c1f802                sar       eax,2
  00470fb4  8bd8                  mov       ebx,eax

LAB_00470fb6:
  00470fb6  6a00                  push      0
  00470fb8  6a00                  push      0
  00470fba  68e7030000            push      3E7h
  00470fbf  e87c610d00            call      00547140h
  00470fc4  99                    cdq
  00470fc5  f7fb                  idiv      ebx
  00470fc7  52                    push      edx
  00470fc8  68e7030000            push      3E7h
  00470fcd  56                    push      esi
  00470fce  e88daefaff            call      0041BE60h
  00470fd3  83c418                add       esp,18h
  00470fd6  e865610d00            call      00547140h
  00470fdb  99                    cdq
  00470fdc  b91e000000            mov       ecx,1Eh
  00470fe1  f7f9                  idiv      ecx
  00470fe3  83fa01                cmp       edx,1
  00470fe6  751c                  jne       short 00471004h
  00470fe8  e853610d00            call      00547140h
  00470fed  99                    cdq
  00470fee  33c2                  xor       eax,edx
  00470ff0  2bc2                  sub       eax,edx
  00470ff2  83e003                and       eax,3
  00470ff5  33c2                  xor       eax,edx
  00470ff7  2bc2                  sub       eax,edx
  00470ff9  40                    inc       eax
  00470ffa  50                    push      eax
  00470ffb  56                    push      esi
  00470ffc  e82f160000            call      00472630h
  00471001  83c408                add       esp,8

LAB_00471004:
  00471004  c1ff0c                sar       edi,0Ch
  00471007  8bdf                  mov       ebx,edi
  00471009  8b7e20                mov       edi,[esi+20h]
  0047100c  c1ff0c                sar       edi,0Ch
  0047100f  3bdf                  cmp       ebx,edi
  00471011  7d12                  jge       short 00471025h
  00471013  6800010000            push      100h
  00471018  6a01                  push      1
  0047101a  56                    push      esi
  0047101b  e880150000            call      004725A0h
  00471020  83c40c                add       esp,0Ch
  00471023  3bdf                  cmp       ebx,edi

LAB_00471025:
  00471025  7e19                  jle       short 00471040h
  00471027  66837e1604            cmp       word [esi+16h],4
  0047102c  7410                  je        short 0047103Eh
  0047102e  6800ffffff            push      0FFFFFF00h
  00471033  6a01                  push      1
  00471035  56                    push      esi
  00471036  e865150000            call      004725A0h
  0047103b  83c40c                add       esp,0Ch

LAB_0047103e:
  0047103e  3bdf                  cmp       ebx,edi

LAB_00471040:
  00471040  750d                  jne       short 0047104Fh
  00471042  6a00                  push      0
  00471044  6a01                  push      1
  00471046  56                    push      esi
  00471047  e854150000            call      004725A0h
  0047104c  83c40c                add       esp,0Ch

LAB_0047104f:
  0047104f  8b567c                mov       edx,[esi+7Ch]
  00471052  33c0                  xor       eax,eax
  00471054  8a420e                mov       al,[edx+0Eh]
  00471057  6639ac8680000000      cmp       [esi+eax*4+80h],bp
  0047105f  741b                  je        short 0047107Ch
  00471061  66ff4616              inc       word [esi+16h]
  00471065  66837e1604            cmp       word [esi+16h],4
  0047106a  7510                  jne       short 0047107Ch
  0047106c  6800feffff            push      0FFFFFE00h
  00471071  6a01                  push      1
  00471073  56                    push      esi
  00471074  e827150000            call      004725A0h
  00471079  83c40c                add       esp,0Ch

LAB_0047107c:
  0047107c  8b8690000000          mov       eax,[esi+90h]
  00471082  f6c408                test      ah,8
  00471085  7458                  je        short 004710DFh
  00471087  8b442410              mov       eax,[esp+10h]
  0047108b  8b4c2430              mov       ecx,[esp+30h]
  0047108f  8b542414              mov       edx,[esp+14h]
  00471093  25ffff0000            and       eax,0FFFFh
  00471098  898690000000          mov       [esi+90h],eax
  0047109e  8b442418              mov       eax,[esp+18h]
  004710a2  894e7c                mov       [esi+7Ch],ecx
  004710a5  8b4e44                mov       ecx,[esi+44h]
  004710a8  895660                mov       [esi+60h],edx
  004710ab  8b564c                mov       edx,[esi+4Ch]
  004710ae  894664                mov       [esi+64h],eax
  004710b1  8b44241c              mov       eax,[esp+1Ch]
  004710b5  894e1c                mov       [esi+1Ch],ecx
  004710b8  8b4c2420              mov       ecx,[esp+20h]
  004710bc  895624                mov       [esi+24h],edx
  004710bf  8b542424              mov       edx,[esp+24h]
  004710c3  898680000000          mov       [esi+80h],eax
  004710c9  8b442428              mov       eax,[esp+28h]
  004710cd  898e84000000          mov       [esi+84h],ecx
  004710d3  899688000000          mov       [esi+88h],edx
  004710d9  89868c000000          mov       [esi+8Ch],eax

LAB_004710df:
  004710df  813d74195700282d0000  cmp       dword [571974h],2D28h
  004710e9  765d                  jbe       short 00471148h
  004710eb  66c746120200          mov       word [esi+12h],2
  004710f1  8b0dcc975e00          mov       ecx,[5E97CCh]
  004710f7  6a00                  push      0
  004710f9  68c0935e00            push      5E93C0h
  004710fe  51                    push      ecx
  004710ff  e8cc2c0a00            call      00513DD0h
  00471104  8b15cc975e00          mov       edx,[5E97CCh]
  0047110a  83c40c                add       esp,0Ch
  0047110d  6a00                  push      0
  0047110f  685c945e00            push      5E945Ch
  00471114  52                    push      edx
  00471115  e8b62c0a00            call      00513DD0h
  0047111a  a1cc975e00            mov       eax,[5E97CCh]
  0047111f  83c40c                add       esp,0Ch
  00471122  6a00                  push      0
  00471124  68f8945e00            push      5E94F8h
  00471129  50                    push      eax
  0047112a  e8a12c0a00            call      00513DD0h
  0047112f  8b0dcc975e00          mov       ecx,[5E97CCh]
  00471135  83c40c                add       esp,0Ch
  00471138  6a00                  push      0
  0047113a  6894955e00            push      5E9594h
  0047113f  51                    push      ecx
  00471140  e88b2c0a00            call      00513DD0h
  00471145  83c40c                add       esp,0Ch

LAB_00471148:
  00471148  5f                    pop       edi
  00471149  5e                    pop       esi
  0047114a  5d                    pop       ebp
  0047114b  5b                    pop       ebx
  0047114c  83c41c                add       esp,1Ch
  0047114f  c3                    ret

28 cible(s) de saut interne : 0x00470c41, 0x00470c50, 0x00470c67, 0x00470c91, 0x00470ce7, 0x00470ce9, 0x00470d11, 0x00470d16, 0x00470d83, 0x00470e25, 0x00470e2a, 0x00470ea5, 0x00470eab, 0x00470ebb, 0x00470ecd, 0x00470ef5, 0x00470f05, 0x00470f17, 0x00470f46, 0x00470fb6, 0x00471004, 0x00471025, 0x0047103e, 0x00471040, 0x0047104f, 0x0047107c, 0x004710df, 0x00471148
