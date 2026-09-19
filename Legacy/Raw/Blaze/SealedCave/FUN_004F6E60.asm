Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004f6e60  section .text  offset fichier 0xf6260  2848 octets

  004f6e60  81ec8c000000          sub       esp,8Ch
  004f6e66  53                    push      ebx
  004f6e67  55                    push      ebp
  004f6e68  56                    push      esi
  004f6e69  8bb4249c000000        mov       esi,[esp+9Ch]
  004f6e70  b864000000            mov       eax,64h
  004f6e75  33c9                  xor       ecx,ecx
  004f6e77  6689442436            mov       [esp+36h],ax
  004f6e7c  668944243a            mov       [esp+3Ah],ax
  004f6e81  8a4641                mov       al,[esi+41h]
  004f6e84  57                    push      edi
  004f6e85  84c0                  test      al,al
  004f6e87  66894c2434            mov       [esp+34h],cx
  004f6e8c  66c74424361e00        mov       word [esp+36h],1Eh
  004f6e93  66c74424383200        mov       word [esp+38h],32h
  004f6e9a  66894c243c            mov       [esp+3Ch],cx
  004f6e9f  7510                  jne       short 004F6EB1h
  004f6ea1  51                    push      ecx
  004f6ea2  6a01                  push      1
  004f6ea4  56                    push      esi
  004f6ea5  c6464101              mov       byte [esi+41h],1
  004f6ea9  e8d2f4ffff            call      004F6380h
  004f6eae  83c40c                add       esp,0Ch

LAB_004f6eb1:
  004f6eb1  8b461c                mov       eax,[esi+1Ch]
  004f6eb4  8b4e20                mov       ecx,[esi+20h]
  004f6eb7  8b5624                mov       edx,[esi+24h]
  004f6eba  8d5e1c                lea       ebx,[esi+1Ch]
  004f6ebd  89442428              mov       [esp+28h],eax
  004f6ec1  8b4664                mov       eax,[esi+64h]
  004f6ec4  894c242c              mov       [esp+2Ch],ecx
  004f6ec8  8b4e60                mov       ecx,[esi+60h]
  004f6ecb  89542430              mov       [esp+30h],edx
  004f6ecf  668b5632              mov       dx,[esi+32h]
  004f6ed3  8944244c              mov       [esp+4Ch],eax
  004f6ed7  8b467c                mov       eax,[esi+7Ch]
  004f6eda  894c2444              mov       [esp+44h],ecx
  004f6ede  668b8e90000000        mov       cx,[esi+90h]
  004f6ee5  668994248a000000      mov       [esp+8Ah],dx
  004f6eed  8b9680000000          mov       edx,[esi+80h]
  004f6ef3  89442450              mov       [esp+50h],eax
  004f6ef7  8b8684000000          mov       eax,[esi+84h]
  004f6efd  66894c2448            mov       [esp+48h],cx
  004f6f02  8b8e88000000          mov       ecx,[esi+88h]
  004f6f08  8b7e6c                mov       edi,[esi+6Ch]
  004f6f0b  89542440              mov       [esp+40h],edx
  004f6f0f  8b968c000000          mov       edx,[esi+8Ch]
  004f6f15  8944245c              mov       [esp+5Ch],eax
  004f6f19  894c2460              mov       [esp+60h],ecx
  004f6f1d  8d442468              lea       eax,[esp+68h]
  004f6f21  89542458              mov       [esp+58h],edx
  004f6f25  8d4c2470              lea       ecx,[esp+70h]
  004f6f29  50                    push      eax
  004f6f2a  8d542470              lea       edx,[esp+70h]
  004f6f2e  51                    push      ecx
  004f6f2f  52                    push      edx
  004f6f30  56                    push      esi
  004f6f31  e84a1a0400            call      00538980h
  004f6f36  83c410                add       esp,10h
  004f6f39  8d442464              lea       eax,[esp+64h]
  004f6f3d  8d4c241c              lea       ecx,[esp+1Ch]
  004f6f41  8d542414              lea       edx,[esp+14h]
  004f6f45  50                    push      eax
  004f6f46  51                    push      ecx
  004f6f47  8d442420              lea       eax,[esp+20h]
  004f6f4b  52                    push      edx
  004f6f4c  8d4c241c              lea       ecx,[esp+1Ch]
  004f6f50  50                    push      eax
  004f6f51  51                    push      ecx
  004f6f52  56                    push      esi
  004f6f53  e8c8ef0300            call      00535F20h
  004f6f58  8b542428              mov       edx,[esp+28h]
  004f6f5c  b840000000            mov       eax,40h
  004f6f61  83c418                add       esp,18h
  004f6f64  2bc2                  sub       eax,edx
  004f6f66  8b542464              mov       edx,[esp+64h]
  004f6f6a  89442424              mov       [esp+24h],eax
  004f6f6e  52                    push      edx
  004f6f6f  56                    push      esi
  004f6f70  e86beefdff            call      004D5DE0h
  004f6f75  8b03                  mov       eax,[ebx]
  004f6f77  8b571c                mov       edx,[edi+1Ch]
  004f6f7a  8b4e20                mov       ecx,[esi+20h]
  004f6f7d  8b6f20                mov       ebp,[edi+20h]
  004f6f80  2bc2                  sub       eax,edx
  004f6f82  8b5624                mov       edx,[esi+24h]
  004f6f85  c1f80c                sar       eax,0Ch
  004f6f88  2bcd                  sub       ecx,ebp
  004f6f8a  8b6f24                mov       ebp,[edi+24h]
  004f6f8d  8bf8                  mov       edi,eax
  004f6f8f  2bd5                  sub       edx,ebp
  004f6f91  c1f90c                sar       ecx,0Ch
  004f6f94  0faff8                imul      edi,eax
  004f6f97  8bc1                  mov       eax,ecx
  004f6f99  83c408                add       esp,8
  004f6f9c  c1fa0c                sar       edx,0Ch
  004f6f9f  0fafc1                imul      eax,ecx
  004f6fa2  8bca                  mov       ecx,edx
  004f6fa4  03f8                  add       edi,eax
  004f6fa6  0fafca                imul      ecx,edx
  004f6fa9  03f9                  add       edi,ecx
  004f6fab  57                    push      edi
  004f6fac  e85fea0400            call      00545A10h
  004f6fb1  8be8                  mov       ebp,eax
  004f6fb3  896c2418              mov       [esp+18h],ebp
  004f6fb7  8b44241c              mov       eax,[esp+1Ch]
  004f6fbb  85c0                  test      eax,eax
  004f6fbd  0f8434060000          je        near 004F75F7h
  004f6fc3  8b0e                  mov       ecx,[esi]
  004f6fc5  33d2                  xor       edx,edx
  004f6fc7  8a5604                mov       dl,[esi+4]
  004f6fca  81e1fffffffd          and       ecx,0FDFFFFFFh
  004f6fd0  0fbf4618              movsx     eax,word [esi+18h]
  004f6fd4  890e                  mov       [esi],ecx
  004f6fd6  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004f6fdd  f7c100000001          test      ecx,1000000h
  004f6fe3  8b1482                mov       edx,[edx+eax*4]
  004f6fe6  741d                  je        short 004F7005h
  004f6fe8  66ff461a              inc       word [esi+1Ah]
  004f6fec  668b461a              mov       ax,[esi+1Ah]
  004f6ff0  33c9                  xor       ecx,ecx
  004f6ff2  668b4a16              mov       cx,[edx+16h]
  004f6ff6  0fbfd0                movsx     edx,ax
  004f6ff9  3bd1                  cmp       edx,ecx
  004f6ffb  7c2a                  jl        short 004F7027h
  004f6ffd  66c7461a0000          mov       word [esi+1Ah],0
  004f7003  eb22                  jmp       short 004F7027h

LAB_004f7005:
  004f7005  66ff461a              inc       word [esi+1Ah]
  004f7009  668b7e1a              mov       di,[esi+1Ah]
  004f700d  33c0                  xor       eax,eax
  004f700f  668b4216              mov       ax,[edx+16h]
  004f7013  0fbfd7                movsx     edx,di
  004f7016  3bd0                  cmp       edx,eax
  004f7018  7c0d                  jl        short 004F7027h
  004f701a  4f                    dec       edi
  004f701b  81c900000002          or        ecx,2000000h
  004f7021  66897e1a              mov       [esi+1Ah],di
  004f7025  890e                  mov       [esi],ecx

LAB_004f7027:
  004f7027  8a4642                mov       al,[esi+42h]
  004f702a  8b0e                  mov       ecx,[esi]
  004f702c  3c05                  cmp       al,5
  004f702e  7505                  jne       short 004F7035h
  004f7030  83c910                or        ecx,10h
  004f7033  eb03                  jmp       short 004F7038h

LAB_004f7035:
  004f7035  83e1ef                and       ecx,0FFFFFFEFh

LAB_004f7038:
  004f7038  890e                  mov       [esi],ecx
  004f703a  8a0e                  mov       cl,[esi]
  004f703c  f6c102                test      cl,2
  004f703f  0f840e010000          je        near 004F7153h
  004f7045  3c08                  cmp       al,8
  004f7047  0f8506010000          jne       near 004F7153h
  004f704d  8d442434              lea       eax,[esp+34h]
  004f7051  6a00                  push      0
  004f7053  50                    push      eax
  004f7054  53                    push      ebx
  004f7055  56                    push      esi
  004f7056  e8d52b0200            call      00519C30h
  004f705b  8bf8                  mov       edi,eax
  004f705d  a1b8e05d00            mov       eax,[5DE0B8h]
  004f7062  83c410                add       esp,10h
  004f7065  85c0                  test      eax,eax
  004f7067  0f84e2000000          je        near 004F714Fh
  004f706d  8b17                  mov       edx,[edi]
  004f706f  57                    push      edi
  004f7070  83ca40                or        edx,40h
  004f7073  56                    push      esi
  004f7074  66c787960000001000    mov       word [edi+96h],10h
  004f707d  8917                  mov       [edi],edx
  004f707f  e8fc2a0400            call      00539B80h
  004f7084  83c408                add       esp,8
  004f7087  8d4f1c                lea       ecx,[edi+1Ch]
  004f708a  51                    push      ecx
  004f708b  6800180000            push      1800h
  004f7090  6800030000            push      300h
  004f7095  6a03                  push      3
  004f7097  e8846a0400            call      0053DB20h
  004f709c  8b07                  mov       eax,[edi]
  004f709e  83c410                add       esp,10h
  004f70a1  a900000040            test      eax,40000000h
  004f70a6  0f859d000000          jne       near 004F7149h
  004f70ac  0fbf4e32              movsx     ecx,word [esi+32h]
  004f70b0  8b5620                mov       edx,[esi+20h]
  004f70b3  8b4624                mov       eax,[esi+24h]
  004f70b6  8b2b                  mov       ebp,[ebx]
  004f70b8  51                    push      ecx
  004f70b9  89542458              mov       [esp+58h],edx
  004f70bd  89442424              mov       [esp+24h],eax
  004f70c1  e856e90400            call      00545A1Ch
  004f70c6  8d0480                lea       eax,[eax+eax*4]
  004f70c9  8d14c0                lea       edx,[eax+eax*8]
  004f70cc  8b03                  mov       eax,[ebx]
  004f70ce  c1e202                shl       edx,2
  004f70d1  03c2                  add       eax,edx
  004f70d3  8903                  mov       [ebx],eax
  004f70d5  0fbf4632              movsx     eax,word [esi+32h]
  004f70d9  50                    push      eax
  004f70da  e837e90400            call      00545A16h
  004f70df  8b5624                mov       edx,[esi+24h]
  004f70e2  8d0480                lea       eax,[eax+eax*4]
  004f70e5  6aff                  push      0FFFFFFFFh
  004f70e7  6a00                  push      0
  004f70e9  8d0cc0                lea       ecx,[eax+eax*8]
  004f70ec  6a39                  push      39h
  004f70ee  c1e102                shl       ecx,2
  004f70f1  03d1                  add       edx,ecx
  004f70f3  56                    push      esi
  004f70f4  895624                mov       [esi+24h],edx
  004f70f7  e8d438f8ff            call      0047A9D0h
  004f70fc  8b542464              mov       edx,[esp+64h]
  004f7100  8b442430              mov       eax,[esp+30h]
  004f7104  892b                  mov       [ebx],ebp
  004f7106  895620                mov       [esi+20h],edx
  004f7109  894624                mov       [esi+24h],eax
  004f710c  8b2f                  mov       ebp,[edi]
  004f710e  83cd01                or        ebp,1
  004f7111  83c410                add       esp,10h
  004f7114  892f                  mov       [edi],ebp
  004f7116  0fbf4e32              movsx     ecx,word [esi+32h]
  004f711a  51                    push      ecx
  004f711b  e8fce80400            call      00545A1Ch
  004f7120  8b565c                mov       edx,[esi+5Ch]
  004f7123  c1fa0b                sar       edx,0Bh
  004f7126  0fafc2                imul      eax,edx
  004f7129  894750                mov       [edi+50h],eax
  004f712c  0fbf4632              movsx     eax,word [esi+32h]
  004f7130  50                    push      eax
  004f7131  e8e0e80400            call      00545A16h
  004f7136  8b4e5c                mov       ecx,[esi+5Ch]
  004f7139  c7475400800100        mov       dword [edi+54h],18000h
  004f7140  c1f90b                sar       ecx,0Bh
  004f7143  0fafc1                imul      eax,ecx
  004f7146  894758                mov       [edi+58h],eax

LAB_004f7149:
  004f7149  8b06                  mov       eax,[esi]
  004f714b  24fd                  and       al,0FDh
  004f714d  8906                  mov       [esi],eax

LAB_004f714f:
  004f714f  8b6c2418              mov       ebp,[esp+18h]

LAB_004f7153:
  004f7153  33c0                  xor       eax,eax
  004f7155  8a4642                mov       al,[esi+42h]
  004f7158  83f80c                cmp       eax,0Ch
  004f715b  0f8796040000          ja        near 004F75F7h
  004f7161  ff248544794f00        jmp       dword [eax*4+4F7944h]
  004f7168  33c9                  xor       ecx,ecx
  004f716a  66394e12              cmp       [esi+12h],cx
  004f716e  751a                  jne       short 004F718Ah
  004f7170  8b06                  mov       eax,[esi]
  004f7172  66894e18              mov       [esi+18h],cx
  004f7176  0d00000001            or        eax,1000000h
  004f717b  66894e1a              mov       [esi+1Ah],cx
  004f717f  8906                  mov       [esi],eax
  004f7181  66c746120100          mov       word [esi+12h],1
  004f7187  894e5c                mov       [esi+5Ch],ecx

LAB_004f718a:
  004f718a  0fbf5610              movsx     edx,word [esi+10h]
  004f718e  3bea                  cmp       ebp,edx
  004f7190  7d09                  jge       short 004F719Bh
  004f7192  c6464300              mov       byte [esi+43h],0
  004f7196  e95c040000            jmp       004F75F7h

LAB_004f719b:
  004f719b  8a4643                mov       al,[esi+43h]
  004f719e  84c0                  test      al,al
  004f71a0  7405                  je        short 004F71A7h
  004f71a2  fec8                  dec       al
  004f71a4  884643                mov       [esi+43h],al

LAB_004f71a7:
  004f71a7  894e5c                mov       [esi+5Ch],ecx
  004f71aa  e948040000            jmp       004F75F7h
  004f71af  66837e1200            cmp       word [esi+12h],0
  004f71b4  7537                  jne       short 004F71EDh
  004f71b6  8b0e                  mov       ecx,[esi]
  004f71b8  b801000000            mov       eax,1
  004f71bd  81c900000001          or        ecx,1000000h
  004f71c3  66894618              mov       [esi+18h],ax
  004f71c7  66c7461a0000          mov       word [esi+1Ah],0
  004f71cd  890e                  mov       [esi],ecx
  004f71cf  66894612              mov       [esi+12h],ax
  004f71d3  c7465c00500000        mov       dword [esi+5Ch],5000h
  004f71da  e861ff0400            call      00547140h
  004f71df  99                    cdq
  004f71e0  b91e000000            mov       ecx,1Eh
  004f71e5  f7f9                  idiv      ecx
  004f71e7  80c214                add       dl,14h
  004f71ea  885643                mov       [esi+43h],dl

LAB_004f71ed:
  004f71ed  8a4643                mov       al,[esi+43h]
  004f71f0  fec8                  dec       al
  004f71f2  884643                mov       [esi+43h],al
  004f71f5  8b442414              mov       eax,[esp+14h]
  004f71f9  8d90c8000000          lea       edx,[eax+0C8h]
  004f71ff  81e2ff0f0000          and       edx,0FFFh
  004f7205  81fa90010000          cmp       edx,190h
  004f720b  0f8ee6030000          jle       near 004F75F7h
  004f7211  25ff0f0000            and       eax,0FFFh
  004f7216  3d00080000            cmp       eax,800h
  004f721b  0f8dc7030000          jge       near 004F75E8h
  004f7221  8b442410              mov       eax,[esp+10h]
  004f7225  83c032                add       eax,32h
  004f7228  e9c6030000            jmp       004F75F3h
  004f722d  8b4c2410              mov       ecx,[esp+10h]
  004f7231  33d2                  xor       edx,edx
  004f7233  66395612              cmp       [esi+12h],dx
  004f7237  b801000000            mov       eax,1
  004f723c  7525                  jne       short 004F7263h
  004f723e  8b3e                  mov       edi,[esi]
  004f7240  66894618              mov       [esi+18h],ax
  004f7244  81cf00000001          or        edi,1000000h
  004f724a  6689561a              mov       [esi+1Ah],dx
  004f724e  893e                  mov       [esi],edi
  004f7250  8d3cc9                lea       edi,[ecx+ecx*8]
  004f7253  c1e704                shl       edi,4
  004f7256  81c700500000          add       edi,5000h
  004f725c  66894612              mov       [esi+12h],ax
  004f7260  897e5c                mov       [esi+5Ch],edi

LAB_004f7263:
  004f7263  66394612              cmp       [esi+12h],ax
  004f7267  0f858a030000          jne       near 004F75F7h
  004f726d  0fbf4610              movsx     eax,word [esi+10h]
  004f7271  3be8                  cmp       ebp,eax
  004f7273  7d0c                  jge       short 004F7281h
  004f7275  c6464300              mov       byte [esi+43h],0
  004f7279  89565c                mov       [esi+5Ch],edx
  004f727c  e976030000            jmp       004F75F7h

LAB_004f7281:
  004f7281  8a4643                mov       al,[esi+43h]
  004f7284  84c0                  test      al,al
  004f7286  7405                  je        short 004F728Dh
  004f7288  fec8                  dec       al
  004f728a  884643                mov       [esi+43h],al

LAB_004f728d:
  004f728d  8b442414              mov       eax,[esp+14h]
  004f7291  8d90c8000000          lea       edx,[eax+0C8h]
  004f7297  81e2ff0f0000          and       edx,0FFFh
  004f729d  81fa90010000          cmp       edx,190h
  004f72a3  0f8e4e030000          jle       near 004F75F7h
  004f72a9  25ff0f0000            and       eax,0FFFh
  004f72ae  3d00080000            cmp       eax,800h
  004f72b3  7d08                  jge       short 004F72BDh
  004f72b5  8d4132                lea       eax,[ecx+32h]
  004f72b8  e936030000            jmp       004F75F3h

LAB_004f72bd:
  004f72bd  b8ceffffff            mov       eax,0FFFFFFCEh
  004f72c2  e92a030000            jmp       004F75F1h
  004f72c7  668b4e12              mov       cx,[esi+12h]
  004f72cb  ba01000000            mov       edx,1
  004f72d0  663bca                cmp       cx,dx
  004f72d3  751a                  jne       short 004F72EFh
  004f72d5  0fbf4610              movsx     eax,word [esi+10h]
  004f72d9  3be8                  cmp       ebp,eax
  004f72db  7d06                  jge       short 004F72E3h
  004f72dd  c6464300              mov       byte [esi+43h],0
  004f72e1  eb0c                  jmp       short 004F72EFh

LAB_004f72e3:
  004f72e3  8a4643                mov       al,[esi+43h]
  004f72e6  84c0                  test      al,al
  004f72e8  7405                  je        short 004F72EFh
  004f72ea  fec8                  dec       al
  004f72ec  884643                mov       [esi+43h],al

LAB_004f72ef:
  004f72ef  6685c9                test      cx,cx
  004f72f2  0f85ff020000          jne       near 004F75F7h
  004f72f8  8b442410              mov       eax,[esp+10h]
  004f72fc  66894e1a              mov       [esi+1Ah],cx
  004f7300  66895618              mov       [esi+18h],dx
  004f7304  66895612              mov       [esi+12h],dx
  004f7308  8b16                  mov       edx,[esi]
  004f730a  8d0cc0                lea       ecx,[eax+eax*8]
  004f730d  c1e104                shl       ecx,4
  004f7310  83e2fd                and       edx,0FFFFFFFDh
  004f7313  81c100500000          add       ecx,5000h
  004f7319  81ca00000001          or        edx,1000000h
  004f731f  894e5c                mov       [esi+5Ch],ecx
  004f7322  8916                  mov       [esi],edx
  004f7324  e9ce020000            jmp       004F75F7h
  004f7329  33ff                  xor       edi,edi
  004f732b  66397e12              cmp       [esi+12h],di
  004f732f  751a                  jne       short 004F734Bh
  004f7331  8b06                  mov       eax,[esi]
  004f7333  66897e18              mov       [esi+18h],di
  004f7337  0d00000001            or        eax,1000000h
  004f733c  66897e1a              mov       [esi+1Ah],di
  004f7340  8906                  mov       [esi],eax
  004f7342  66c746120100          mov       word [esi+12h],1
  004f7348  897e5c                mov       [esi+5Ch],edi

LAB_004f734b:
  004f734b  66837e1201            cmp       word [esi+12h],1
  004f7350  0f8584000000          jne       near 004F73DAh
  004f7356  8b4c2414              mov       ecx,[esp+14h]
  004f735a  8d8100010000          lea       eax,[ecx+100h]
  004f7360  25ff0f0000            and       eax,0FFFh
  004f7365  3d00020000            cmp       eax,200h
  004f736a  8b442410              mov       eax,[esp+10h]
  004f736e  7e1e                  jle       short 004F738Eh
  004f7370  81e1ff0f0000          and       ecx,0FFFh
  004f7376  81f900080000          cmp       ecx,800h
  004f737c  7d05                  jge       short 004F7383h
  004f737e  8d4864                lea       ecx,[eax+64h]
  004f7381  eb07                  jmp       short 004F738Ah

LAB_004f7383:
  004f7383  b99cffffff            mov       ecx,0FFFFFF9Ch
  004f7388  2bc8                  sub       ecx,eax

LAB_004f738a:
  004f738a  66014e32              add       [esi+32h],cx

LAB_004f738e:
  004f738e  8a4e43                mov       cl,[esi+43h]
  004f7391  84c9                  test      cl,cl
  004f7393  7445                  je        short 004F73DAh
  004f7395  fec9                  dec       cl
  004f7397  884e43                mov       [esi+43h],cl
  004f739a  753e                  jne       short 004F73DAh
  004f739c  8b16                  mov       edx,[esi]
  004f739e  24fc                  and       al,0FCh
  004f73a0  83c078                add       eax,78h
  004f73a3  81ca00000001          or        edx,1000000h
  004f73a9  c1e00a                shl       eax,0Ah
  004f73ac  66c746120200          mov       word [esi+12h],2
  004f73b2  89465c                mov       [esi+5Ch],eax
  004f73b5  66c746180400          mov       word [esi+18h],4
  004f73bb  66897e1a              mov       [esi+1Ah],di
  004f73bf  8916                  mov       [esi],edx
  004f73c1  e87afd0400            call      00547140h
  004f73c6  99                    cdq
  004f73c7  b90a000000            mov       ecx,0Ah
  004f73cc  f7f9                  idiv      ecx
  004f73ce  8b06                  mov       eax,[esi]
  004f73d0  80c21e                add       dl,1Eh
  004f73d3  0c02                  or        al,2
  004f73d5  885643                mov       [esi+43h],dl
  004f73d8  8906                  mov       [esi],eax

LAB_004f73da:
  004f73da  66837e1202            cmp       word [esi+12h],2
  004f73df  0f858c000000          jne       near 004F7471h
  004f73e5  6aff                  push      0FFFFFFFFh
  004f73e7  57                    push      edi
  004f73e8  68ee000000            push      0EEh
  004f73ed  57                    push      edi
  004f73ee  53                    push      ebx
  004f73ef  56                    push      esi
  004f73f0  e84b38f8ff            call      0047AC40h
  004f73f5  83c418                add       esp,18h
  004f73f8  e843fd0400            call      00547140h
  004f73fd  99                    cdq
  004f73fe  b903000000            mov       ecx,3
  004f7403  f7f9                  idiv      ecx
  004f7405  83fa01                cmp       edx,1
  004f7408  7513                  jne       short 004F741Dh
  004f740a  6aff                  push      0FFFFFFFFh
  004f740c  57                    push      edi
  004f740d  68ee000000            push      0EEh
  004f7412  57                    push      edi
  004f7413  53                    push      ebx
  004f7414  56                    push      esi
  004f7415  e82638f8ff            call      0047AC40h
  004f741a  83c418                add       esp,18h

LAB_004f741d:
  004f741d  8a4643                mov       al,[esi+43h]
  004f7420  3c0a                  cmp       al,0Ah
  004f7422  763e                  jbe       short 004F7462h
  004f7424  8b4c2414              mov       ecx,[esp+14h]
  004f7428  8d9100010000          lea       edx,[ecx+100h]
  004f742e  81e2ff0f0000          and       edx,0FFFh
  004f7434  81fa00020000          cmp       edx,200h
  004f743a  7e26                  jle       short 004F7462h
  004f743c  81e1ff0f0000          and       ecx,0FFFh
  004f7442  81f900080000          cmp       ecx,800h
  004f7448  7d09                  jge       short 004F7453h
  004f744a  8b4c2410              mov       ecx,[esp+10h]
  004f744e  83c132                add       ecx,32h
  004f7451  eb0b                  jmp       short 004F745Eh

LAB_004f7453:
  004f7453  8b542410              mov       edx,[esp+10h]
  004f7457  b9ceffffff            mov       ecx,0FFFFFFCEh
  004f745c  2bca                  sub       ecx,edx

LAB_004f745e:
  004f745e  66014e32              add       [esi+32h],cx

LAB_004f7462:
  004f7462  fec8                  dec       al
  004f7464  3c01                  cmp       al,1
  004f7466  884643                mov       [esi+43h],al
  004f7469  7506                  jne       short 004F7471h
  004f746b  66c746120300          mov       word [esi+12h],3

LAB_004f7471:
  004f7471  66837e1203            cmp       word [esi+12h],3
  004f7476  0f857b010000          jne       near 004F75F7h
  004f747c  8b465c                mov       eax,[esi+5Ch]
  004f747f  8bd0                  mov       edx,eax
  004f7481  f7da                  neg       edx
  004f7483  c1fa03                sar       edx,3
  004f7486  03c2                  add       eax,edx
  004f7488  3d00500000            cmp       eax,5000h
  004f748d  89465c                mov       [esi+5Ch],eax
  004f7490  0f8d61010000          jge       near 004F75F7h
  004f7496  c6464300              mov       byte [esi+43h],0
  004f749a  e958010000            jmp       004F75F7h
  004f749f  66837e1200            cmp       word [esi+12h],0
  004f74a4  7542                  jne       short 004F74E8h
  004f74a6  e895fc0400            call      00547140h
  004f74ab  99                    cdq
  004f74ac  b90a000000            mov       ecx,0Ah
  004f74b1  f7f9                  idiv      ecx
  004f74b3  83ea05                sub       edx,5
  004f74b6  8d1492                lea       edx,[edx+edx*4]
  004f74b9  d1e2                  shl       edx,1
  004f74bb  66895610              mov       [esi+10h],dx
  004f74bf  e87cfc0400            call      00547140h
  004f74c4  99                    cdq
  004f74c5  b90a000000            mov       ecx,0Ah
  004f74ca  66c746120100          mov       word [esi+12h],1
  004f74d0  f7f9                  idiv      ecx
  004f74d2  80c214                add       dl,14h
  004f74d5  885643                mov       [esi+43h],dl
  004f74d8  8b542410              mov       edx,[esp+10h]
  004f74dc  83e2fc                and       edx,0FFFFFFFCh
  004f74df  83c214                add       edx,14h
  004f74e2  c1e20a                shl       edx,0Ah
  004f74e5  89565c                mov       [esi+5Ch],edx

LAB_004f74e8:
  004f74e8  66837e1201            cmp       word [esi+12h],1
  004f74ed  0f8504010000          jne       near 004F75F7h
  004f74f3  668b4610              mov       ax,[esi+10h]
  004f74f7  66014632              add       [esi+32h],ax
  004f74fb  8a4643                mov       al,[esi+43h]
  004f74fe  fec8                  dec       al
  004f7500  884643                mov       [esi+43h],al
  004f7503  e9ef000000            jmp       004F75F7h
  004f7508  33c0                  xor       eax,eax
  004f750a  66394612              cmp       [esi+12h],ax
  004f750e  7520                  jne       short 004F7530h
  004f7510  8b0e                  mov       ecx,[esi]
  004f7512  66c746120100          mov       word [esi+12h],1
  004f7518  81e1fffffffe          and       ecx,0FEFFFFFFh
  004f751e  66c746180300          mov       word [esi+18h],3
  004f7524  83c902                or        ecx,2
  004f7527  6689461a              mov       [esi+1Ah],ax
  004f752b  89465c                mov       [esi+5Ch],eax
  004f752e  890e                  mov       [esi],ecx

LAB_004f7530:
  004f7530  66837e1201            cmp       word [esi+12h],1
  004f7535  0f85bc000000          jne       near 004F75F7h
  004f753b  8b06                  mov       eax,[esi]
  004f753d  a900000002            test      eax,2000000h
  004f7542  0f84af000000          je        near 004F75F7h
  004f7548  24fd                  and       al,0FDh
  004f754a  c6464300              mov       byte [esi+43h],0
  004f754e  8906                  mov       [esi],eax
  004f7550  e9a2000000            jmp       004F75F7h
  004f7555  66837e1200            cmp       word [esi+12h],0
  004f755a  7550                  jne       short 004F75ACh
  004f755c  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004f7563  6aff                  push      0FFFFFFFFh
  004f7565  6a00                  push      0
  004f7567  bd01000000            mov       ebp,1
  004f756c  68dc000000            push      0DCh
  004f7571  56                    push      esi
  004f7572  6689ae9a000000        mov       [esi+9Ah],bp
  004f7579  e85234f8ff            call      0047A9D0h
  004f757e  8b16                  mov       edx,[esi]
  004f7580  83c410                add       esp,10h
  004f7583  83e2fd                and       edx,0FFFFFFFDh
  004f7586  33c0                  xor       eax,eax
  004f7588  81ca00000001          or        edx,1000000h
  004f758e  6689be9a000000        mov       [esi+9Ah],di
  004f7595  66c746180200          mov       word [esi+18h],2
  004f759b  6689461a              mov       [esi+1Ah],ax
  004f759f  66896e12              mov       [esi+12h],bp
  004f75a3  c646430a              mov       byte [esi+43h],0Ah
  004f75a7  89465c                mov       [esi+5Ch],eax
  004f75aa  8916                  mov       [esi],edx

LAB_004f75ac:
  004f75ac  66837e1201            cmp       word [esi+12h],1
  004f75b1  7544                  jne       short 004F75F7h
  004f75b3  8a4643                mov       al,[esi+43h]
  004f75b6  fec8                  dec       al
  004f75b8  884643                mov       [esi+43h],al
  004f75bb  8b442414              mov       eax,[esp+14h]
  004f75bf  8d8800010000          lea       ecx,[eax+100h]
  004f75c5  81e1ff0f0000          and       ecx,0FFFh
  004f75cb  81f900020000          cmp       ecx,200h
  004f75d1  7e24                  jle       short 004F75F7h
  004f75d3  25ff0f0000            and       eax,0FFFh
  004f75d8  3d00080000            cmp       eax,800h
  004f75dd  7d09                  jge       short 004F75E8h
  004f75df  8b542410              mov       edx,[esp+10h]
  004f75e3  8d4232                lea       eax,[edx+32h]
  004f75e6  eb0b                  jmp       short 004F75F3h

LAB_004f75e8:
  004f75e8  8b4c2410              mov       ecx,[esp+10h]
  004f75ec  b8ceffffff            mov       eax,0FFFFFFCEh

LAB_004f75f1:
  004f75f1  2bc1                  sub       eax,ecx

LAB_004f75f3:
  004f75f3  66014632              add       [esi+32h],ax

LAB_004f75f7:
  004f75f7  0fbf4632              movsx     eax,word [esi+32h]
  004f75fb  50                    push      eax
  004f75fc  e81be40400            call      00545A1Ch
  004f7601  0fbf4e32              movsx     ecx,word [esi+32h]
  004f7605  51                    push      ecx
  004f7606  89442478              mov       [esp+78h],eax
  004f760a  e807e40400            call      00545A16h
  004f760f  8b5650                mov       edx,[esi+50h]
  004f7612  8944247c              mov       [esp+7Ch],eax
  004f7616  8bc2                  mov       eax,edx
  004f7618  8bfa                  mov       edi,edx
  004f761a  c1f808                sar       eax,8
  004f761d  2bf8                  sub       edi,eax
  004f761f  8b4c241c              mov       ecx,[esp+1Ch]
  004f7623  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004f762a  0faf4e5c              imul      ecx,[esi+5Ch]
  004f762e  c1fa03                sar       edx,3
  004f7631  2bfa                  sub       edi,edx
  004f7633  8b5654                mov       edx,[esi+54h]
  004f7636  8be8                  mov       ebp,eax
  004f7638  897e50                mov       [esi+50h],edi
  004f763b  c1fa08                sar       edx,8
  004f763e  c1fd08                sar       ebp,8
  004f7641  2bea                  sub       ebp,edx
  004f7643  8b5658                mov       edx,[esi+58h]
  004f7646  2be8                  sub       ebp,eax
  004f7648  8b4654                mov       eax,[esi+54h]
  004f764b  03c5                  add       eax,ebp
  004f764d  8bea                  mov       ebp,edx
  004f764f  89442420              mov       [esp+20h],eax
  004f7653  894654                mov       [esi+54h],eax
  004f7656  8bc2                  mov       eax,edx
  004f7658  56                    push      esi
  004f7659  c1f808                sar       eax,8
  004f765c  2be8                  sub       ebp,eax
  004f765e  8b442424              mov       eax,[esp+24h]
  004f7662  c1fa03                sar       edx,3
  004f7665  c1f90c                sar       ecx,0Ch
  004f7668  2bea                  sub       ebp,edx
  004f766a  8bd1                  mov       edx,ecx
  004f766c  0faf542478            imul      edx,[esp+78h]
  004f7671  0faf8c2480000000      imul      ecx,[esp+80h]
  004f7679  c1fa0c                sar       edx,0Ch
  004f767c  03d7                  add       edx,edi
  004f767e  8b3b                  mov       edi,[ebx]
  004f7680  03fa                  add       edi,edx
  004f7682  8b5620                mov       edx,[esi+20h]
  004f7685  c1f90c                sar       ecx,0Ch
  004f7688  03d0                  add       edx,eax
  004f768a  8b4624                mov       eax,[esi+24h]
  004f768d  03cd                  add       ecx,ebp
  004f768f  896e58                mov       [esi+58h],ebp
  004f7692  03c1                  add       eax,ecx
  004f7694  893b                  mov       [ebx],edi
  004f7696  895620                mov       [esi+20h],edx
  004f7699  894624                mov       [esi+24h],eax
  004f769c  e89fc20300            call      00533940h
  004f76a1  8b9690000000          mov       edx,[esi+90h]
  004f76a7  8b4660                mov       eax,[esi+60h]
  004f76aa  8b4e20                mov       ecx,[esi+20h]
  004f76ad  83c404                add       esp,4
  004f76b0  80e6fc                and       dh,0FCh
  004f76b3  3bc8                  cmp       ecx,eax
  004f76b5  899690000000          mov       [esi+90h],edx
  004f76bb  7f2a                  jg        short 004F76E7h
  004f76bd  f6c680                test      dh,80h
  004f76c0  7525                  jne       short 004F76E7h
  004f76c2  8b0e                  mov       ecx,[esi]
  004f76c4  894620                mov       [esi+20h],eax
  004f76c7  f6c101                test      cl,1
  004f76ca  740f                  je        short 004F76DBh
  004f76cc  83e1fe                and       ecx,0FFFFFFFEh
  004f76cf  33ff                  xor       edi,edi
  004f76d1  890e                  mov       [esi],ecx
  004f76d3  897e54                mov       [esi+54h],edi
  004f76d6  e92a010000            jmp       004F7805h

LAB_004f76db:
  004f76db  83e1fe                and       ecx,0FFFFFFFEh
  004f76de  33ff                  xor       edi,edi
  004f76e0  890e                  mov       [esi],ecx
  004f76e2  897e54                mov       [esi+54h],edi
  004f76e5  eb02                  jmp       short 004F76E9h

LAB_004f76e7:
  004f76e7  33ff                  xor       edi,edi

LAB_004f76e9:
  004f76e9  8b0e                  mov       ecx,[esi]
  004f76eb  f6c101                test      cl,1
  004f76ee  750a                  jne       short 004F76FAh
  004f76f0  394620                cmp       [esi+20h],eax
  004f76f3  7e05                  jle       short 004F76FAh
  004f76f5  83c901                or        ecx,1
  004f76f8  890e                  mov       [esi],ecx

LAB_004f76fa:
  004f76fa  f7c282ad0020          test      edx,2000AD82h
  004f7700  0f84ff000000          je        near 004F7805h
  004f7706  f60601                test      byte [esi],1
  004f7709  0f85ce000000          jne       near 004F77DDh
  004f770f  8a4e42                mov       cl,[esi+42h]
  004f7712  b007                  mov       al,7
  004f7714  3ac8                  cmp       cl,al
  004f7716  7426                  je        short 004F773Eh
  004f7718  884642                mov       [esi+42h],al
  004f771b  e820fa0400            call      00547140h
  004f7720  99                    cdq
  004f7721  b928000000            mov       ecx,28h
  004f7726  66897e12              mov       [esi+12h],di
  004f772a  f7f9                  idiv      ecx
  004f772c  8b06                  mov       eax,[esi]
  004f772e  66c74610c800          mov       word [esi+10h],0C8h
  004f7734  80c214                add       dl,14h
  004f7737  24fd                  and       al,0FDh
  004f7739  885643                mov       [esi+43h],dl
  004f773c  8906                  mov       [esi],eax

LAB_004f773e:
  004f773e  8b16                  mov       edx,[esi]
  004f7740  8b44242c              mov       eax,[esp+2Ch]
  004f7744  8b4c2430              mov       ecx,[esp+30h]
  004f7748  83e2fd                and       edx,0FFFFFFFDh
  004f774b  8916                  mov       [esi],edx
  004f774d  8b542428              mov       edx,[esp+28h]
  004f7751  894620                mov       [esi+20h],eax
  004f7754  8b442448              mov       eax,[esp+48h]
  004f7758  8913                  mov       [ebx],edx
  004f775a  8b542450              mov       edx,[esp+50h]
  004f775e  25ffff0000            and       eax,0FFFFh
  004f7763  894e24                mov       [esi+24h],ecx
  004f7766  8b4c245c              mov       ecx,[esp+5Ch]
  004f776a  89567c                mov       [esi+7Ch],edx
  004f776d  8b542460              mov       edx,[esp+60h]
  004f7771  898690000000          mov       [esi+90h],eax
  004f7777  8b442440              mov       eax,[esp+40h]
  004f777b  898e84000000          mov       [esi+84h],ecx
  004f7781  8b4c244c              mov       ecx,[esp+4Ch]
  004f7785  898680000000          mov       [esi+80h],eax
  004f778b  8b442458              mov       eax,[esp+58h]
  004f778f  899688000000          mov       [esi+88h],edx
  004f7795  8b542444              mov       edx,[esp+44h]
  004f7799  6882ad0020            push      2000AD82h
  004f779e  56                    push      esi
  004f779f  89868c000000          mov       [esi+8Ch],eax
  004f77a5  894e64                mov       [esi+64h],ecx
  004f77a8  895660                mov       [esi+60h],edx
  004f77ab  e8b0e6fdff            call      004D5E60h
  004f77b0  83c408                add       esp,8
  004f77b3  663bc7                cmp       ax,di
  004f77b6  7515                  jne       short 004F77CDh
  004f77b8  e883f90400            call      00547140h
  004f77bd  99                    cdq
  004f77be  33c2                  xor       eax,edx
  004f77c0  2bc2                  sub       eax,edx
  004f77c2  83e001                and       eax,1
  004f77c5  33c2                  xor       eax,edx
  004f77c7  2bc2                  sub       eax,edx
  004f77c9  8d4400ff              lea       eax,[eax+eax-1]

LAB_004f77cd:
  004f77cd  8b4c2410              mov       ecx,[esp+10h]
  004f77d1  83c140                add       ecx,40h
  004f77d4  0fafc8                imul      ecx,eax
  004f77d7  66014e32              add       [esi+32h],cx
  004f77db  eb28                  jmp       short 004F7805h

LAB_004f77dd:
  004f77dd  8b542428              mov       edx,[esp+28h]
  004f77e1  8b442430              mov       eax,[esp+30h]
  004f77e5  897e5c                mov       [esi+5Ch],edi
  004f77e8  897e50                mov       [esi+50h],edi
  004f77eb  897e58                mov       [esi+58h],edi
  004f77ee  8913                  mov       [ebx],edx
  004f77f0  894624                mov       [esi+24h],eax
  004f77f3  c6464200              mov       byte [esi+42h],0
  004f77f7  c6464328              mov       byte [esi+43h],28h
  004f77fb  66897e12              mov       [esi+12h],di
  004f77ff  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004f7805:
  004f7805  6aff                  push      0FFFFFFFFh
  004f7807  6aff                  push      0FFFFFFFFh
  004f7809  6aff                  push      0FFFFFFFFh
  004f780b  56                    push      esi
  004f780c  e8ffbffcff            call      004C3810h
  004f7811  8b44242c              mov       eax,[esp+2Ch]
  004f7815  83c410                add       esp,10h
  004f7818  3bc7                  cmp       eax,edi
  004f781a  752a                  jne       short 004F7846h
  004f781c  8a4642                mov       al,[esi+42h]
  004f781f  668b8c248a000000      mov       cx,[esp+8Ah]
  004f7827  3c0c                  cmp       al,0Ch
  004f7829  66894e32              mov       [esi+32h],cx
  004f782d  0f8506010000          jne       near 004F7939h
  004f7833  c6464200              mov       byte [esi+42h],0
  004f7837  c6464301              mov       byte [esi+43h],1
  004f783b  5f                    pop       edi
  004f783c  5e                    pop       esi
  004f783d  5d                    pop       ebp
  004f783e  5b                    pop       ebx
  004f783f  81c48c000000          add       esp,8Ch
  004f7845  c3                    ret

LAB_004f7846:
  004f7846  8a4643                mov       al,[esi+43h]
  004f7849  84c0                  test      al,al
  004f784b  0f85e8000000          jne       near 004F7939h
  004f7851  8b466c                mov       eax,[esi+6Ch]
  004f7854  8b2b                  mov       ebp,[ebx]
  004f7856  8b4e24                mov       ecx,[esi+24h]
  004f7859  8b781c                mov       edi,[eax+1Ch]
  004f785c  2bfd                  sub       edi,ebp
  004f785e  8b6824                mov       ebp,[eax+24h]
  004f7861  2be9                  sub       ebp,ecx
  004f7863  c1ff0c                sar       edi,0Ch
  004f7866  c1fd0c                sar       ebp,0Ch
  004f7869  8bd7                  mov       edx,edi
  004f786b  8bc5                  mov       eax,ebp
  004f786d  0fafd7                imul      edx,edi
  004f7870  0fafc5                imul      eax,ebp
  004f7873  03d0                  add       edx,eax
  004f7875  52                    push      edx
  004f7876  e895e10400            call      00545A10h
  004f787b  55                    push      ebp
  004f787c  57                    push      edi
  004f787d  89442420              mov       [esp+20h],eax
  004f7881  e89ce10400            call      00545A22h
  004f7886  668b4e32              mov       cx,[esi+32h]
  004f788a  66c746120000          mov       word [esi+12h],0
  004f7890  2bc1                  sub       eax,ecx
  004f7892  25ff0f0000            and       eax,0FFFh
  004f7897  89442414              mov       [esp+14h],eax
  004f789b  e8a0f80400            call      00547140h
  004f78a0  99                    cdq
  004f78a1  b964000000            mov       ecx,64h
  004f78a6  f7f9                  idiv      ecx
  004f78a8  8bfa                  mov       edi,edx
  004f78aa  83ff32                cmp       edi,32h
  004f78ad  7d27                  jge       short 004F78D6h
  004f78af  c6464208              mov       byte [esi+42h],8
  004f78b3  e888f80400            call      00547140h
  004f78b8  99                    cdq
  004f78b9  b90a000000            mov       ecx,0Ah
  004f78be  8b5c2424              mov       ebx,[esp+24h]
  004f78c2  f7f9                  idiv      ecx
  004f78c4  8bc3                  mov       eax,ebx
  004f78c6  c1f802                sar       eax,2
  004f78c9  02d0                  add       dl,al
  004f78cb  33ed                  xor       ebp,ebp
  004f78cd  885643                mov       [esi+43h],dl
  004f78d0  66896e12              mov       [esi+12h],bp
  004f78d4  eb06                  jmp       short 004F78DCh

LAB_004f78d6:
  004f78d6  8b5c2424              mov       ebx,[esp+24h]
  004f78da  33ed                  xor       ebp,ebp

LAB_004f78dc:
  004f78dc  83ff32                cmp       edi,32h
  004f78df  7c18                  jl        short 004F78F9h
  004f78e1  83ff50                cmp       edi,50h
  004f78e4  7d13                  jge       short 004F78F9h
  004f78e6  c6464201              mov       byte [esi+42h],1
  004f78ea  c6464301              mov       byte [esi+43h],1
  004f78ee  5f                    pop       edi
  004f78ef  5e                    pop       esi
  004f78f0  5d                    pop       ebp
  004f78f1  5b                    pop       ebx
  004f78f2  81c48c000000          add       esp,8Ch
  004f78f8  c3                    ret

LAB_004f78f9:
  004f78f9  817c241826020000      cmp       dword [esp+18h],226h
  004f7901  7d19                  jge       short 004F791Ch
  004f7903  807e420c              cmp       byte [esi+42h],0Ch
  004f7907  7413                  je        short 004F791Ch
  004f7909  c646420c              mov       byte [esi+42h],0Ch
  004f790d  c6464301              mov       byte [esi+43h],1
  004f7911  5f                    pop       edi
  004f7912  5e                    pop       esi
  004f7913  5d                    pop       ebp
  004f7914  5b                    pop       ebx
  004f7915  81c48c000000          add       esp,8Ch
  004f791b  c3                    ret

LAB_004f791c:
  004f791c  c6464208              mov       byte [esi+42h],8
  004f7920  e81bf80400            call      00547140h
  004f7925  99                    cdq
  004f7926  b90a000000            mov       ecx,0Ah
  004f792b  66896e12              mov       [esi+12h],bp
  004f792f  f7f9                  idiv      ecx
  004f7931  c1fb02                sar       ebx,2
  004f7934  02d3                  add       dl,bl
  004f7936  885643                mov       [esi+43h],dl

LAB_004f7939:
  004f7939  5f                    pop       edi
  004f793a  5e                    pop       esi
  004f793b  5d                    pop       ebp
  004f793c  5b                    pop       ebx
  004f793d  81c48c000000          add       esp,8Ch
  004f7943  c3                    ret
  004f7944  68714f00af            push      0AF004F71h
  004f7949  714f                  jno       short 004F799Ah
  004f794b  002d724f0096          add       [96004F72h],ch
  004f7951  744f                  je        short 004F79A2h
  004f7953  00f7                  add       bh,dh
  004f7955  754f                  jne       short 004F79A6h
  004f7957  00f7                  add       bh,dh
  004f7959  754f                  jne       short 004F79AAh
  004f795b  0096744f00c7          add       [esi-38FFB08Ch],dl
  004f7961  724f                  jb        short 004F79B2h
  004f7963  0029                  add       [ecx],ch
  004f7965  734f                  jae       short 004F79B6h
  004f7967  009f744f00f7          add       [edi-8FFB08Ch],bl
  004f796d  754f                  jne       short 004F79BEh
  004f796f  0008                  add       [eax],cl
  004f7971  754f                  jne       short 004F79C2h
  004f7973  005575                add       [ebp+75h],dl
  004f7976  4f                    dec       edi
  004f7977  009090909090          add       [eax-6F6F6F70h],dl
  004f797d  90                    nop
  004f797e  90                    nop
  004f797f  90                    nop

49 cible(s) de saut interne : 0x004f6eb1, 0x004f7005, 0x004f7027, 0x004f7035, 0x004f7038, 0x004f7149, 0x004f714f, 0x004f7153, 0x004f718a, 0x004f719b, 0x004f71a7, 0x004f71ed, 0x004f7263, 0x004f7281, 0x004f728d, 0x004f72bd, 0x004f72e3, 0x004f72ef, 0x004f734b, 0x004f7383, 0x004f738a, 0x004f738e, 0x004f73da, 0x004f741d, 0x004f7453, 0x004f745e, 0x004f7462, 0x004f7471, 0x004f74e8, 0x004f7530, 0x004f75ac, 0x004f75e8, 0x004f75f1, 0x004f75f3, 0x004f75f7, 0x004f76db, 0x004f76e7, 0x004f76e9, 0x004f76fa, 0x004f773e, 0x004f77cd, 0x004f77dd, 0x004f7805, 0x004f7846, 0x004f78d6, 0x004f78dc, 0x004f78f9, 0x004f791c, 0x004f7939
