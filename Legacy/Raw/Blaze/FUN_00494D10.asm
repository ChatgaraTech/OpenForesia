Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00494d10  section .text  offset fichier 0x94110  3968 octets

  00494d10  81ec90000000          sub       esp,90h
  00494d16  53                    push      ebx
  00494d17  55                    push      ebp
  00494d18  56                    push      esi
  00494d19  8bb424a0000000        mov       esi,[esp+0A0h]
  00494d20  b864000000            mov       eax,64h
  00494d25  33db                  xor       ebx,ebx
  00494d27  6689442436            mov       [esp+36h],ax
  00494d2c  668944243a            mov       [esp+3Ah],ax
  00494d31  6689442442            mov       [esp+42h],ax
  00494d36  8a4641                mov       al,[esi+41h]
  00494d39  b9c8000000            mov       ecx,0C8h
  00494d3e  57                    push      edi
  00494d3f  84c0                  test      al,al
  00494d41  66895c2434            mov       [esp+34h],bx
  00494d46  66c74424361e00        mov       word [esp+36h],1Eh
  00494d4d  66c74424383200        mov       word [esp+38h],32h
  00494d54  66895c243c            mov       [esp+3Ch],bx
  00494d59  66895c2440            mov       [esp+40h],bx
  00494d5e  66c74424429cff        mov       word [esp+42h],0FF9Ch
  00494d65  66894c2444            mov       [esp+44h],cx
  00494d6a  66895c2448            mov       [esp+48h],bx
  00494d6f  66894c244a            mov       [esp+4Ah],cx
  00494d74  7510                  jne       short 00494D86h
  00494d76  53                    push      ebx
  00494d77  6a03                  push      3
  00494d79  56                    push      esi
  00494d7a  c6464101              mov       byte [esi+41h],1
  00494d7e  e80dc5ffff            call      00491290h
  00494d83  83c40c                add       esp,0Ch

LAB_00494d86:
  00494d86  8b461c                mov       eax,[esi+1Ch]
  00494d89  8b4e20                mov       ecx,[esi+20h]
  00494d8c  8b5624                mov       edx,[esi+24h]
  00494d8f  8d6e1c                lea       ebp,[esi+1Ch]
  00494d92  89442428              mov       [esp+28h],eax
  00494d96  8b4664                mov       eax,[esi+64h]
  00494d99  894c242c              mov       [esp+2Ch],ecx
  00494d9d  8b4e60                mov       ecx,[esi+60h]
  00494da0  89542430              mov       [esp+30h],edx
  00494da4  668b5632              mov       dx,[esi+32h]
  00494da8  89442450              mov       [esp+50h],eax
  00494dac  8b467c                mov       eax,[esi+7Ch]
  00494daf  894c2454              mov       [esp+54h],ecx
  00494db3  668b8e90000000        mov       cx,[esi+90h]
  00494dba  668994248e000000      mov       [esp+8Eh],dx
  00494dc2  8b9680000000          mov       edx,[esi+80h]
  00494dc8  89442464              mov       [esp+64h],eax
  00494dcc  8b8684000000          mov       eax,[esi+84h]
  00494dd2  66894c246c            mov       [esp+6Ch],cx
  00494dd7  8b8e88000000          mov       ecx,[esi+88h]
  00494ddd  8b7e6c                mov       edi,[esi+6Ch]
  00494de0  89542460              mov       [esp+60h],edx
  00494de4  8b968c000000          mov       edx,[esi+8Ch]
  00494dea  8944244c              mov       [esp+4Ch],eax
  00494dee  894c2458              mov       [esp+58h],ecx
  00494df2  8d442424              lea       eax,[esp+24h]
  00494df6  89542468              mov       [esp+68h],edx
  00494dfa  8d4c2470              lea       ecx,[esp+70h]
  00494dfe  50                    push      eax
  00494dff  8d542478              lea       edx,[esp+78h]
  00494e03  51                    push      ecx
  00494e04  52                    push      edx
  00494e05  56                    push      esi
  00494e06  e8753b0a00            call      00538980h
  00494e0b  83c410                add       esp,10h
  00494e0e  8d44245c              lea       eax,[esp+5Ch]
  00494e12  8d4c2420              lea       ecx,[esp+20h]
  00494e16  8d542418              lea       edx,[esp+18h]
  00494e1a  50                    push      eax
  00494e1b  51                    push      ecx
  00494e1c  8d44241c              lea       eax,[esp+1Ch]
  00494e20  52                    push      edx
  00494e21  8d4c241c              lea       ecx,[esp+1Ch]
  00494e25  50                    push      eax
  00494e26  51                    push      ecx
  00494e27  56                    push      esi
  00494e28  e8f3100a00            call      00535F20h
  00494e2d  8b4c2428              mov       ecx,[esp+28h]
  00494e31  8b542474              mov       edx,[esp+74h]
  00494e35  83c418                add       esp,18h
  00494e38  b840000000            mov       eax,40h
  00494e3d  2bc1                  sub       eax,ecx
  00494e3f  52                    push      edx
  00494e40  56                    push      esi
  00494e41  89442424              mov       [esp+24h],eax
  00494e45  e8960f0400            call      004D5DE0h
  00494e4a  8b4500                mov       eax,[ebp]
  00494e4d  8b571c                mov       edx,[edi+1Ch]
  00494e50  8b4e20                mov       ecx,[esi+20h]
  00494e53  2bc2                  sub       eax,edx
  00494e55  8b5720                mov       edx,[edi+20h]
  00494e58  83c408                add       esp,8
  00494e5b  2bca                  sub       ecx,edx
  00494e5d  8b5624                mov       edx,[esi+24h]
  00494e60  2b5724                sub       edx,[edi+24h]
  00494e63  c1f80c                sar       eax,0Ch
  00494e66  8bf8                  mov       edi,eax
  00494e68  c1f90c                sar       ecx,0Ch
  00494e6b  0faff8                imul      edi,eax
  00494e6e  8bc1                  mov       eax,ecx
  00494e70  c1fa0c                sar       edx,0Ch
  00494e73  0fafc1                imul      eax,ecx
  00494e76  8bca                  mov       ecx,edx
  00494e78  03f8                  add       edi,eax
  00494e7a  0fafca                imul      ecx,edx
  00494e7d  03f9                  add       edi,ecx
  00494e7f  57                    push      edi
  00494e80  e88b0b0b00            call      00545A10h
  00494e85  89442414              mov       [esp+14h],eax
  00494e89  8b442420              mov       eax,[esp+20h]
  00494e8d  3bc3                  cmp       eax,ebx
  00494e8f  0f84cd070000          je        near 00495662h
  00494e95  8b3e                  mov       edi,[esi]
  00494e97  33d2                  xor       edx,edx
  00494e99  8a5604                mov       dl,[esi+4]
  00494e9c  81e7fffffffd          and       edi,0FDFFFFFFh
  00494ea2  0fbf4e18              movsx     ecx,word [esi+18h]
  00494ea6  893e                  mov       [esi],edi
  00494ea8  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00494eaf  8bc7                  mov       eax,edi
  00494eb1  8b0c8a                mov       ecx,[edx+ecx*4]
  00494eb4  a900000001            test      eax,1000000h
  00494eb9  741b                  je        short 00494ED6h
  00494ebb  66ff461a              inc       word [esi+1Ah]
  00494ebf  668b461a              mov       ax,[esi+1Ah]
  00494ec3  33d2                  xor       edx,edx
  00494ec5  668b5116              mov       dx,[ecx+16h]
  00494ec9  0fbfc0                movsx     eax,ax
  00494ecc  3bc2                  cmp       eax,edx
  00494ece  7c27                  jl        short 00494EF7h
  00494ed0  66895e1a              mov       [esi+1Ah],bx
  00494ed4  eb21                  jmp       short 00494EF7h

LAB_00494ed6:
  00494ed6  66ff461a              inc       word [esi+1Ah]
  00494eda  668b561a              mov       dx,[esi+1Ah]
  00494ede  33ff                  xor       edi,edi
  00494ee0  668b7916              mov       di,[ecx+16h]
  00494ee4  0fbfca                movsx     ecx,dx
  00494ee7  3bcf                  cmp       ecx,edi
  00494ee9  7c0c                  jl        short 00494EF7h
  00494eeb  4a                    dec       edx
  00494eec  0d00000002            or        eax,2000000h
  00494ef1  6689561a              mov       [esi+1Ah],dx
  00494ef5  8906                  mov       [esi],eax

LAB_00494ef7:
  00494ef7  8a4642                mov       al,[esi+42h]
  00494efa  8b0e                  mov       ecx,[esi]
  00494efc  3c05                  cmp       al,5
  00494efe  7505                  jne       short 00494F05h
  00494f00  83c910                or        ecx,10h
  00494f03  eb03                  jmp       short 00494F08h

LAB_00494f05:
  00494f05  83e1ef                and       ecx,0FFFFFFEFh

LAB_00494f08:
  00494f08  f6c102                test      cl,2
  00494f0b  890e                  mov       [esi],ecx
  00494f0d  0f841b020000          je        near 0049512Eh
  00494f13  3c0c                  cmp       al,0Ch
  00494f15  bb08000000            mov       ebx,8
  00494f1a  7479                  je        short 00494F95h
  00494f1c  3c0d                  cmp       al,0Dh
  00494f1e  7475                  je        short 00494F95h
  00494f20  66395e1a              cmp       [esi+1Ah],bx
  00494f24  7554                  jne       short 00494F7Ah
  00494f26  8d542434              lea       edx,[esp+34h]
  00494f2a  6a00                  push      0
  00494f2c  52                    push      edx
  00494f2d  55                    push      ebp
  00494f2e  56                    push      esi
  00494f2f  e8fc4c0800            call      00519C30h
  00494f34  8bf8                  mov       edi,eax
  00494f36  a1b8e05d00            mov       eax,[5DE0B8h]
  00494f3b  83c410                add       esp,10h
  00494f3e  85c0                  test      eax,eax
  00494f40  7438                  je        short 00494F7Ah
  00494f42  66395e1a              cmp       [esi+1Ah],bx
  00494f46  7532                  jne       short 00494F7Ah
  00494f48  8b0f                  mov       ecx,[edi]
  00494f4a  57                    push      edi
  00494f4b  83c940                or        ecx,40h
  00494f4e  56                    push      esi
  00494f4f  66c787960000001000    mov       word [edi+96h],10h
  00494f58  890f                  mov       [edi],ecx
  00494f5a  e8214c0a00            call      00539B80h
  00494f5f  83c408                add       esp,8
  00494f62  83c71c                add       edi,1Ch
  00494f65  57                    push      edi
  00494f66  6800180000            push      1800h
  00494f6b  6800030000            push      300h
  00494f70  6a03                  push      3
  00494f72  e8a98b0a00            call      0053DB20h
  00494f77  83c410                add       esp,10h

LAB_00494f7a:
  00494f7a  8b06                  mov       eax,[esi]
  00494f7c  a900000002            test      eax,2000000h
  00494f81  7412                  je        short 00494F95h
  00494f83  a801                  test      al,1
  00494f85  750a                  jne       short 00494F91h
  00494f87  24fd                  and       al,0FDh
  00494f89  c6464300              mov       byte [esi+43h],0
  00494f8d  8906                  mov       [esi],eax
  00494f8f  eb04                  jmp       short 00494F95h

LAB_00494f91:
  00494f91  24fd                  and       al,0FDh
  00494f93  8906                  mov       [esi],eax

LAB_00494f95:
  00494f95  807e420c              cmp       byte [esi+42h],0Ch
  00494f99  0f85c5000000          jne       near 00495064h
  00494f9f  668b461a              mov       ax,[esi+1Ah]
  00494fa3  663bc3                cmp       ax,bx
  00494fa6  7507                  jne       short 00494FAFh
  00494fa8  66837e1803            cmp       word [esi+18h],3
  00494fad  7415                  je        short 00494FC4h

LAB_00494faf:
  00494faf  663d0c00              cmp       ax,0Ch
  00494fb3  0f85ab000000          jne       near 00495064h
  00494fb9  66837e1806            cmp       word [esi+18h],6
  00494fbe  0f85a0000000          jne       near 00495064h

LAB_00494fc4:
  00494fc4  8d442434              lea       eax,[esp+34h]
  00494fc8  6a00                  push      0
  00494fca  50                    push      eax
  00494fcb  55                    push      ebp
  00494fcc  56                    push      esi
  00494fcd  e85e4c0800            call      00519C30h
  00494fd2  8bf8                  mov       edi,eax
  00494fd4  a1b8e05d00            mov       eax,[5DE0B8h]
  00494fd9  83c410                add       esp,10h
  00494fdc  85c0                  test      eax,eax
  00494fde  0f8480000000          je        near 00495064h
  00494fe4  668b461a              mov       ax,[esi+1Ah]
  00494fe8  663bc3                cmp       ax,bx
  00494feb  7507                  jne       short 00494FF4h
  00494fed  66837e1803            cmp       word [esi+18h],3
  00494ff2  740d                  je        short 00495001h

LAB_00494ff4:
  00494ff4  663d0c00              cmp       ax,0Ch
  00494ff8  756a                  jne       short 00495064h
  00494ffa  66837e1806            cmp       word [esi+18h],6
  00494fff  7563                  jne       short 00495064h

LAB_00495001:
  00495001  66837e1201            cmp       word [esi+12h],1
  00495006  750b                  jne       short 00495013h
  00495008  66c787960000000000    mov       word [edi+96h],0
  00495011  eb0f                  jmp       short 00495022h

LAB_00495013:
  00495013  8b07                  mov       eax,[edi]
  00495015  66c787960000001000    mov       word [edi+96h],10h
  0049501e  0c40                  or        al,40h
  00495020  8907                  mov       [edi],eax

LAB_00495022:
  00495022  8b4c2424              mov       ecx,[esp+24h]
  00495026  57                    push      edi
  00495027  56                    push      esi
  00495028  668b4130              mov       ax,[ecx+30h]
  0049502c  668bd0                mov       dx,ax
  0049502f  66c1fa02              sar       dx,2
  00495033  0fbfd8                movsx     ebx,ax
  00495036  2bc2                  sub       eax,edx
  00495038  66894130              mov       [ecx+30h],ax
  0049503c  e83f4b0a00            call      00539B80h
  00495041  8b44242c              mov       eax,[esp+2Ch]
  00495045  83c408                add       esp,8
  00495048  83c71c                add       edi,1Ch
  0049504b  57                    push      edi
  0049504c  6800180000            push      1800h
  00495051  6800030000            push      300h
  00495056  6a03                  push      3
  00495058  66895830              mov       [eax+30h],bx
  0049505c  e8bf8a0a00            call      0053DB20h
  00495061  83c410                add       esp,10h

LAB_00495064:
  00495064  807e420d              cmp       byte [esi+42h],0Dh
  00495068  0f85fe000000          jne       near 0049516Ch
  0049506e  33db                  xor       ebx,ebx
  00495070  8d4c2440              lea       ecx,[esp+40h]
  00495074  53                    push      ebx
  00495075  51                    push      ecx
  00495076  55                    push      ebp
  00495077  56                    push      esi
  00495078  e8b34b0800            call      00519C30h
  0049507d  8bf8                  mov       edi,eax
  0049507f  a1b8e05d00            mov       eax,[5DE0B8h]
  00495084  83c410                add       esp,10h
  00495087  3bc3                  cmp       eax,ebx
  00495089  0f84e5000000          je        near 00495174h
  0049508f  8b1f                  mov       ebx,[edi]
  00495091  66c787960000001000    mov       word [edi+96h],10h
  0049509a  83cb40                or        ebx,40h
  0049509d  57                    push      edi
  0049509e  891f                  mov       [edi],ebx
  004950a0  8b4c2428              mov       ecx,[esp+28h]
  004950a4  56                    push      esi
  004950a5  668b4130              mov       ax,[ecx+30h]
  004950a9  0fbfd8                movsx     ebx,ax
  004950ac  66d1f8                sar       ax,1
  004950af  66894130              mov       [ecx+30h],ax
  004950b3  e8c84a0a00            call      00539B80h
  004950b8  8b54242c              mov       edx,[esp+2Ch]
  004950bc  83c408                add       esp,8
  004950bf  66895a30              mov       [edx+30h],bx
  004950c3  8d5f1c                lea       ebx,[edi+1Ch]
  004950c6  53                    push      ebx
  004950c7  6800180000            push      1800h
  004950cc  6800030000            push      300h
  004950d1  6a03                  push      3
  004950d3  e8488a0a00            call      0053DB20h
  004950d8  8b07                  mov       eax,[edi]
  004950da  83c410                add       esp,10h
  004950dd  a900000040            test      eax,40000000h
  004950e2  753c                  jne       short 00495120h
  004950e4  6aff                  push      0FFFFFFFFh
  004950e6  6a00                  push      0
  004950e8  6a39                  push      39h
  004950ea  56                    push      esi
  004950eb  e8e058feff            call      0047A9D0h
  004950f0  8b17                  mov       edx,[edi]
  004950f2  8b0b                  mov       ecx,[ebx]
  004950f4  83ca01                or        edx,1
  004950f7  8b5f24                mov       ebx,[edi+24h]
  004950fa  8917                  mov       [edi],edx
  004950fc  8b4500                mov       eax,[ebp]
  004950ff  2bc1                  sub       eax,ecx
  00495101  83c410                add       esp,10h
  00495104  c1f803                sar       eax,3
  00495107  f7d8                  neg       eax
  00495109  894750                mov       [edi+50h],eax
  0049510c  8b4e24                mov       ecx,[esi+24h]
  0049510f  2bcb                  sub       ecx,ebx
  00495111  c7475400800100        mov       dword [edi+54h],18000h
  00495118  c1f903                sar       ecx,3
  0049511b  f7d9                  neg       ecx
  0049511d  894f58                mov       [edi+58h],ecx

LAB_00495120:
  00495120  8b06                  mov       eax,[esi]
  00495122  8b4c2410              mov       ecx,[esp+10h]
  00495126  24fd                  and       al,0FDh
  00495128  33db                  xor       ebx,ebx
  0049512a  8906                  mov       [esi],eax
  0049512c  eb4a                  jmp       short 00495178h

LAB_0049512e:
  0049512e  f6c101                test      cl,1
  00495131  7439                  je        short 0049516Ch
  00495133  66837e1a03            cmp       word [esi+1Ah],3
  00495138  7d32                  jge       short 0049516Ch
  0049513a  e801200b00            call      00547140h
  0049513f  99                    cdq
  00495140  b93c000000            mov       ecx,3Ch
  00495145  f7f9                  idiv      ecx
  00495147  8b4c2410              mov       ecx,[esp+10h]
  0049514b  3bd1                  cmp       edx,ecx
  0049514d  7d21                  jge       short 00495170h
  0049514f  8b16                  mov       edx,[esi]
  00495151  66c746180300          mov       word [esi+18h],3
  00495157  81e2fffffffe          and       edx,0FEFFFFFFh
  0049515d  66c7461a0000          mov       word [esi+1Ah],0
  00495163  83ca02                or        edx,2
  00495166  33db                  xor       ebx,ebx
  00495168  8916                  mov       [esi],edx
  0049516a  eb0c                  jmp       short 00495178h

LAB_0049516c:
  0049516c  8b4c2410              mov       ecx,[esp+10h]

LAB_00495170:
  00495170  33db                  xor       ebx,ebx
  00495172  eb04                  jmp       short 00495178h

LAB_00495174:
  00495174  8b4c2410              mov       ecx,[esp+10h]

LAB_00495178:
  00495178  33c0                  xor       eax,eax
  0049517a  8a4642                mov       al,[esi+42h]
  0049517d  83f80d                cmp       eax,0Dh
  00495180  0f87dc040000          ja        near 00495662h
  00495186  ff2485545c4900        jmp       dword [eax*4+495C54h]
  0049518d  66395e12              cmp       [esi+12h],bx
  00495191  751a                  jne       short 004951ADh
  00495193  8b06                  mov       eax,[esi]
  00495195  66895e18              mov       [esi+18h],bx
  00495199  0d00000001            or        eax,1000000h
  0049519e  66895e1a              mov       [esi+1Ah],bx
  004951a2  8906                  mov       [esi],eax
  004951a4  66c746120100          mov       word [esi+12h],1
  004951aa  895e5c                mov       [esi+5Ch],ebx

LAB_004951ad:
  004951ad  0fbf4610              movsx     eax,word [esi+10h]
  004951b1  39442414              cmp       [esp+14h],eax
  004951b5  7d09                  jge       short 004951C0h
  004951b7  c6464300              mov       byte [esi+43h],0
  004951bb  e9a2040000            jmp       00495662h

LAB_004951c0:
  004951c0  8a4643                mov       al,[esi+43h]
  004951c3  84c0                  test      al,al
  004951c5  0f8494040000          je        near 0049565Fh
  004951cb  fec8                  dec       al
  004951cd  884643                mov       [esi+43h],al
  004951d0  e98a040000            jmp       0049565Fh
  004951d5  66395e12              cmp       [esi+12h],bx
  004951d9  751b                  jne       short 004951F6h
  004951db  8b16                  mov       edx,[esi]
  004951dd  b801000000            mov       eax,1
  004951e2  81ca00000001          or        edx,1000000h
  004951e8  66894618              mov       [esi+18h],ax
  004951ec  66895e1a              mov       [esi+1Ah],bx
  004951f0  8916                  mov       [esi],edx
  004951f2  66894612              mov       [esi+12h],ax

LAB_004951f6:
  004951f6  8b442418              mov       eax,[esp+18h]
  004951fa  8d90c8000000          lea       edx,[eax+0C8h]
  00495200  81e2ff0f0000          and       edx,0FFFh
  00495206  81fa90010000          cmp       edx,190h
  0049520c  7f09                  jg        short 00495217h
  0049520e  c6464300              mov       byte [esi+43h],0
  00495212  e948040000            jmp       0049565Fh

LAB_00495217:
  00495217  25ff0f0000            and       eax,0FFFh
  0049521c  3d00080000            cmp       eax,800h
  00495221  7d0c                  jge       short 0049522Fh
  00495223  8d413c                lea       eax,[ecx+3Ch]
  00495226  66014632              add       [esi+32h],ax
  0049522a  e930040000            jmp       0049565Fh

LAB_0049522f:
  0049522f  b8c4ffffff            mov       eax,0FFFFFFC4h
  00495234  2bc1                  sub       eax,ecx
  00495236  66014632              add       [esi+32h],ax
  0049523a  e920040000            jmp       0049565Fh
  0049523f  66395e12              cmp       [esi+12h],bx
  00495243  b801000000            mov       eax,1
  00495248  7525                  jne       short 0049526Fh
  0049524a  8b16                  mov       edx,[esi]
  0049524c  66894618              mov       [esi+18h],ax
  00495250  81ca00000001          or        edx,1000000h
  00495256  66895e1a              mov       [esi+1Ah],bx
  0049525a  8916                  mov       [esi],edx
  0049525c  8d14c9                lea       edx,[ecx+ecx*8]
  0049525f  c1e204                shl       edx,4
  00495262  81c200600000          add       edx,6000h
  00495268  66894612              mov       [esi+12h],ax
  0049526c  89565c                mov       [esi+5Ch],edx

LAB_0049526f:
  0049526f  66394612              cmp       [esi+12h],ax
  00495273  0f85e9030000          jne       near 00495662h
  00495279  0fbf4610              movsx     eax,word [esi+10h]
  0049527d  39442414              cmp       [esp+14h],eax
  00495281  7d09                  jge       short 0049528Ch
  00495283  c6464300              mov       byte [esi+43h],0
  00495287  e9d3030000            jmp       0049565Fh

LAB_0049528c:
  0049528c  8a4643                mov       al,[esi+43h]
  0049528f  84c0                  test      al,al
  00495291  7405                  je        short 00495298h
  00495293  fec8                  dec       al
  00495295  884643                mov       [esi+43h],al

LAB_00495298:
  00495298  8b442418              mov       eax,[esp+18h]
  0049529c  8d9000010000          lea       edx,[eax+100h]
  004952a2  81e2ff0f0000          and       edx,0FFFh
  004952a8  81fa00020000          cmp       edx,200h
  004952ae  0f8eae030000          jle       near 00495662h
  004952b4  25ff0f0000            and       eax,0FFFh
  004952b9  3d00080000            cmp       eax,800h
  004952be  7d0c                  jge       short 004952CCh
  004952c0  8d413c                lea       eax,[ecx+3Ch]
  004952c3  66014632              add       [esi+32h],ax
  004952c7  e996030000            jmp       00495662h

LAB_004952cc:
  004952cc  b8c4ffffff            mov       eax,0FFFFFFC4h
  004952d1  2bc1                  sub       eax,ecx
  004952d3  66014632              add       [esi+32h],ax
  004952d7  e986030000            jmp       00495662h
  004952dc  bb01000000            mov       ebx,1
  004952e1  66395e12              cmp       [esi+12h],bx
  004952e5  0f859b000000          jne       near 00495386h
  004952eb  8a4643                mov       al,[esi+43h]
  004952ee  84c0                  test      al,al
  004952f0  7405                  je        short 004952F7h
  004952f2  fec8                  dec       al
  004952f4  884643                mov       [esi+43h],al

LAB_004952f7:
  004952f7  8b7c2420              mov       edi,[esp+20h]
  004952fb  8d04c9                lea       eax,[ecx+ecx*8]
  004952fe  c1e004                shl       eax,4
  00495301  0500500000            add       eax,5000h
  00495306  0fbf4e10              movsx     ecx,word [esi+10h]
  0049530a  0faff8                imul      edi,eax
  0049530d  51                    push      ecx
  0049530e  89465c                mov       [esi+5Ch],eax
  00495311  c1ff0c                sar       edi,0Ch
  00495314  e803070b00            call      00545A1Ch
  00495319  0fafc7                imul      eax,edi
  0049531c  0fbf5610              movsx     edx,word [esi+10h]
  00495320  8b4d00                mov       ecx,[ebp]
  00495323  52                    push      edx
  00495324  c1f80c                sar       eax,0Ch
  00495327  03c8                  add       ecx,eax
  00495329  894d00                mov       [ebp],ecx
  0049532c  e8e5060b00            call      00545A16h
  00495331  0fafc7                imul      eax,edi
  00495334  8b6e24                mov       ebp,[esi+24h]
  00495337  c7465c00000000        mov       dword [esi+5Ch],0
  0049533e  c1f80c                sar       eax,0Ch
  00495341  03e8                  add       ebp,eax
  00495343  8b442418              mov       eax,[esp+18h]
  00495347  896e24                mov       [esi+24h],ebp
  0049534a  8d8800010000          lea       ecx,[eax+100h]
  00495350  81e1ff0f0000          and       ecx,0FFFh
  00495356  81f900020000          cmp       ecx,200h
  0049535c  7e28                  jle       short 00495386h
  0049535e  25ff0f0000            and       eax,0FFFh
  00495363  3d00080000            cmp       eax,800h
  00495368  7d0a                  jge       short 00495374h
  0049536a  8b542410              mov       edx,[esp+10h]
  0049536e  8d441232              lea       eax,[edx+edx+32h]
  00495372  eb0e                  jmp       short 00495382h

LAB_00495374:
  00495374  8b442410              mov       eax,[esp+10h]
  00495378  8d0c00                lea       ecx,[eax+eax]
  0049537b  b8ceffffff            mov       eax,0FFFFFFCEh
  00495380  2bc1                  sub       eax,ecx

LAB_00495382:
  00495382  66014632              add       [esi+32h],ax

LAB_00495386:
  00495386  66837e1200            cmp       word [esi+12h],0
  0049538b  0f85d1020000          jne       near 00495662h
  00495391  8b06                  mov       eax,[esi]
  00495393  66895e18              mov       [esi+18h],bx
  00495397  0d00000001            or        eax,1000000h
  0049539c  66c7461a0000          mov       word [esi+1Ah],0
  004953a2  8906                  mov       [esi],eax
  004953a4  66895e12              mov       [esi+12h],bx
  004953a8  e9b5020000            jmp       00495662h
  004953ad  66395e12              cmp       [esi+12h],bx
  004953b1  757b                  jne       short 0049542Eh
  004953b3  8b06                  mov       eax,[esi]
  004953b5  a801                  test      al,1
  004953b7  7406                  je        short 004953BFh
  004953b9  c6464300              mov       byte [esi+43h],0
  004953bd  eb6f                  jmp       short 0049542Eh

LAB_004953bf:
  004953bf  8b7c2420              mov       edi,[esp+20h]
  004953c3  25fffffffe            and       eax,0FEFFFFFFh
  004953c8  8906                  mov       [esi],eax
  004953ca  8d8100010000          lea       eax,[ecx+100h]
  004953d0  c1e006                shl       eax,6
  004953d3  0fbf5610              movsx     edx,word [esi+10h]
  004953d7  0faff8                imul      edi,eax
  004953da  52                    push      edx
  004953db  66c746180400          mov       word [esi+18h],4
  004953e1  66895e1a              mov       [esi+1Ah],bx
  004953e5  89465c                mov       [esi+5Ch],eax
  004953e8  c1ff0c                sar       edi,0Ch
  004953eb  e82c060b00            call      00545A1Ch
  004953f0  0fafc7                imul      eax,edi
  004953f3  8b6e50                mov       ebp,[esi+50h]
  004953f6  c1f80c                sar       eax,0Ch
  004953f9  03e8                  add       ebp,eax
  004953fb  0fbf4610              movsx     eax,word [esi+10h]
  004953ff  50                    push      eax
  00495400  896e50                mov       [esi+50h],ebp
  00495403  e80e060b00            call      00545A16h
  00495408  0fafc7                imul      eax,edi
  0049540b  8b5658                mov       edx,[esi+58h]
  0049540e  8b4e54                mov       ecx,[esi+54h]
  00495411  c1f80c                sar       eax,0Ch
  00495414  03d0                  add       edx,eax
  00495416  8b06                  mov       eax,[esi]
  00495418  81c100e00100          add       ecx,1E000h
  0049541e  0c01                  or        al,1
  00495420  895658                mov       [esi+58h],edx
  00495423  894e54                mov       [esi+54h],ecx
  00495426  8906                  mov       [esi],eax
  00495428  66c746120100          mov       word [esi+12h],1

LAB_0049542e:
  0049542e  f60601                test      byte [esi],1
  00495431  0f852b020000          jne       near 00495662h
  00495437  c6464300              mov       byte [esi+43h],0
  0049543b  e922020000            jmp       00495662h
  00495440  668b5612              mov       dx,[esi+12h]
  00495444  bf01000000            mov       edi,1
  00495449  663bd7                cmp       dx,di
  0049544c  751c                  jne       short 0049546Ah
  0049544e  0fbf4610              movsx     eax,word [esi+10h]
  00495452  39442414              cmp       [esp+14h],eax
  00495456  7d06                  jge       short 0049545Eh
  00495458  c6464300              mov       byte [esi+43h],0
  0049545c  eb0c                  jmp       short 0049546Ah

LAB_0049545e:
  0049545e  8a4643                mov       al,[esi+43h]
  00495461  84c0                  test      al,al
  00495463  7405                  je        short 0049546Ah
  00495465  fec8                  dec       al
  00495467  884643                mov       [esi+43h],al

LAB_0049546a:
  0049546a  663bd3                cmp       dx,bx
  0049546d  0f85ef010000          jne       near 00495662h
  00495473  66895e1a              mov       [esi+1Ah],bx
  00495477  8b1e                  mov       ebx,[esi]
  00495479  8d0cc9                lea       ecx,[ecx+ecx*8]
  0049547c  81cb00000001          or        ebx,1000000h
  00495482  c1e104                shl       ecx,4
  00495485  81c100500000          add       ecx,5000h
  0049548b  66897e18              mov       [esi+18h],di
  0049548f  891e                  mov       [esi],ebx
  00495491  66897e12              mov       [esi+12h],di
  00495495  894e5c                mov       [esi+5Ch],ecx
  00495498  e9c5010000            jmp       00495662h
  0049549d  bf01000000            mov       edi,1
  004954a2  66397e12              cmp       [esi+12h],di
  004954a6  751f                  jne       short 004954C7h
  004954a8  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004954ad  7518                  jne       short 004954C7h
  004954af  66397e16              cmp       [esi+16h],di
  004954b3  7512                  jne       short 004954C7h
  004954b5  6aff                  push      0FFFFFFFFh
  004954b7  53                    push      ebx
  004954b8  6a36                  push      36h
  004954ba  56                    push      esi
  004954bb  e81055feff            call      0047A9D0h
  004954c0  8b4c2420              mov       ecx,[esp+20h]
  004954c4  83c410                add       esp,10h

LAB_004954c7:
  004954c7  8b06                  mov       eax,[esi]
  004954c9  a900000002            test      eax,2000000h
  004954ce  7404                  je        short 004954D4h
  004954d0  c6464300              mov       byte [esi+43h],0

LAB_004954d4:
  004954d4  66395e12              cmp       [esi+12h],bx
  004954d8  0f8584010000          jne       near 00495662h
  004954de  25fffffffe            and       eax,0FEFFFFFFh
  004954e3  83f91e                cmp       ecx,1Eh
  004954e6  66897e12              mov       [esi+12h],di
  004954ea  66c746180600          mov       word [esi+18h],6
  004954f0  66895e1a              mov       [esi+1Ah],bx
  004954f4  8906                  mov       [esi],eax
  004954f6  7d03                  jge       short 004954FBh
  004954f8  895e5c                mov       [esi+5Ch],ebx

LAB_004954fb:
  004954fb  66395e16              cmp       [esi+16h],bx
  004954ff  0f855d010000          jne       near 00495662h
  00495505  c6464300              mov       byte [esi+43h],0
  00495509  e954010000            jmp       00495662h
  0049550e  668b4e12              mov       cx,[esi+12h]
  00495512  6683f901              cmp       cx,1
  00495516  7511                  jne       short 00495529h
  00495518  8b06                  mov       eax,[esi]
  0049551a  a900000002            test      eax,2000000h
  0049551f  7408                  je        short 00495529h
  00495521  24fd                  and       al,0FDh
  00495523  c6464300              mov       byte [esi+43h],0
  00495527  8906                  mov       [esi],eax

LAB_00495529:
  00495529  663bcb                cmp       cx,bx
  0049552c  0f8530010000          jne       near 00495662h
  00495532  8b06                  mov       eax,[esi]
  00495534  66c746180300          mov       word [esi+18h],3
  0049553a  25fffffffc            and       eax,0FCFFFFFFh
  0049553f  66895e1a              mov       [esi+1Ah],bx
  00495543  8906                  mov       [esi],eax
  00495545  66c746120100          mov       word [esi+12h],1
  0049554b  e912010000            jmp       00495662h
  00495550  668b4e12              mov       cx,[esi+12h]
  00495554  6683f902              cmp       cx,2
  00495558  7511                  jne       short 0049556Bh
  0049555a  8b06                  mov       eax,[esi]
  0049555c  a900000002            test      eax,2000000h
  00495561  7408                  je        short 0049556Bh
  00495563  24fd                  and       al,0FDh
  00495565  c6464300              mov       byte [esi+43h],0
  00495569  8906                  mov       [esi],eax

LAB_0049556b:
  0049556b  6683f901              cmp       cx,1
  0049556f  7522                  jne       short 00495593h
  00495571  8b06                  mov       eax,[esi]
  00495573  a900000002            test      eax,2000000h
  00495578  7419                  je        short 00495593h
  0049557a  25fffffffe            and       eax,0FEFFFFFFh
  0049557f  66c746120200          mov       word [esi+12h],2
  00495585  8906                  mov       [esi],eax
  00495587  66c746180600          mov       word [esi+18h],6
  0049558d  66c7461a0700          mov       word [esi+1Ah],7

LAB_00495593:
  00495593  66395e12              cmp       [esi+12h],bx
  00495597  0f85c5000000          jne       near 00495662h
  0049559d  8b16                  mov       edx,[esi]
  0049559f  66c746120100          mov       word [esi+12h],1
  004955a5  81e2fffffffe          and       edx,0FEFFFFFFh
  004955ab  66c746180300          mov       word [esi+18h],3
  004955b1  83ca02                or        edx,2
  004955b4  66895e1a              mov       [esi+1Ah],bx
  004955b8  8916                  mov       [esi],edx
  004955ba  e9a0000000            jmp       0049565Fh
  004955bf  668b5612              mov       dx,[esi+12h]
  004955c3  6683fa02              cmp       dx,2
  004955c7  751a                  jne       short 004955E3h
  004955c9  8b465c                mov       eax,[esi+5Ch]
  004955cc  8bf8                  mov       edi,eax
  004955ce  c1ff04                sar       edi,4
  004955d1  2bc7                  sub       eax,edi
  004955d3  89465c                mov       [esi+5Ch],eax
  004955d6  8b06                  mov       eax,[esi]
  004955d8  a900000002            test      eax,2000000h
  004955dd  7404                  je        short 004955E3h
  004955df  c6464300              mov       byte [esi+43h],0

LAB_004955e3:
  004955e3  6683fa01              cmp       dx,1
  004955e7  7557                  jne       short 00495640h
  004955e9  8b442418              mov       eax,[esp+18h]
  004955ed  8d9000010000          lea       edx,[eax+100h]
  004955f3  81e2ff0f0000          and       edx,0FFFh
  004955f9  81fa00020000          cmp       edx,200h
  004955ff  7e20                  jle       short 00495621h
  00495601  25ff0f0000            and       eax,0FFFh
  00495606  3d00080000            cmp       eax,800h
  0049560b  7d06                  jge       short 00495613h
  0049560d  8d4c0932              lea       ecx,[ecx+ecx+32h]
  00495611  eb0a                  jmp       short 0049561Dh

LAB_00495613:
  00495613  8d0409                lea       eax,[ecx+ecx]
  00495616  b9ceffffff            mov       ecx,0FFFFFFCEh
  0049561b  2bc8                  sub       ecx,eax

LAB_0049561d:
  0049561d  66014e32              add       [esi+32h],cx

LAB_00495621:
  00495621  66837e1a0a            cmp       word [esi+1Ah],0Ah
  00495626  7c18                  jl        short 00495640h
  00495628  8b4e54                mov       ecx,[esi+54h]
  0049562b  8b06                  mov       eax,[esi]
  0049562d  81c100e00100          add       ecx,1E000h
  00495633  0c03                  or        al,3
  00495635  66c746120200          mov       word [esi+12h],2
  0049563b  894e54                mov       [esi+54h],ecx
  0049563e  8906                  mov       [esi],eax

LAB_00495640:
  00495640  66395e12              cmp       [esi+12h],bx
  00495644  751c                  jne       short 00495662h
  00495646  8b06                  mov       eax,[esi]
  00495648  66c746120100          mov       word [esi+12h],1
  0049564e  25fffffffe            and       eax,0FEFFFFFFh
  00495653  66c746180500          mov       word [esi+18h],5
  00495659  66895e1a              mov       [esi+1Ah],bx
  0049565d  8906                  mov       [esi],eax

LAB_0049565f:
  0049565f  895e5c                mov       [esi+5Ch],ebx

LAB_00495662:
  00495662  0fbf4e32              movsx     ecx,word [esi+32h]
  00495666  51                    push      ecx
  00495667  e8b0030b00            call      00545A1Ch
  0049566c  0fbf5632              movsx     edx,word [esi+32h]
  00495670  52                    push      edx
  00495671  8944247c              mov       [esp+7Ch],eax
  00495675  e89c030b00            call      00545A16h
  0049567a  8b5650                mov       edx,[esi+50h]
  0049567d  89842480000000        mov       [esp+80h],eax
  00495684  8bc2                  mov       eax,edx
  00495686  8b6e54                mov       ebp,[esi+54h]
  00495689  c1f803                sar       eax,3
  0049568c  8bfa                  mov       edi,edx
  0049568e  8b4c2420              mov       ecx,[esp+20h]
  00495692  2bf8                  sub       edi,eax
  00495694  0faf4e5c              imul      ecx,[esi+5Ch]
  00495698  c1fa08                sar       edx,8
  0049569b  2bfa                  sub       edi,edx
  0049569d  8bdd                  mov       ebx,ebp
  0049569f  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004956a6  8bc2                  mov       eax,edx
  004956a8  897e50                mov       [esi+50h],edi
  004956ab  c1f808                sar       eax,8
  004956ae  c1fb08                sar       ebx,8
  004956b1  2bc3                  sub       eax,ebx
  004956b3  56                    push      esi
  004956b4  2bc2                  sub       eax,edx
  004956b6  8b5658                mov       edx,[esi+58h]
  004956b9  8bda                  mov       ebx,edx
  004956bb  03c5                  add       eax,ebp
  004956bd  c1fb08                sar       ebx,8
  004956c0  8bea                  mov       ebp,edx
  004956c2  894654                mov       [esi+54h],eax
  004956c5  2beb                  sub       ebp,ebx
  004956c7  8b5e1c                mov       ebx,[esi+1Ch]
  004956ca  c1fa03                sar       edx,3
  004956cd  c1f90c                sar       ecx,0Ch
  004956d0  2bea                  sub       ebp,edx
  004956d2  8bd1                  mov       edx,ecx
  004956d4  0faf54247c            imul      edx,[esp+7Ch]
  004956d9  0faf8c2484000000      imul      ecx,[esp+84h]
  004956e1  c1fa0c                sar       edx,0Ch
  004956e4  c1f90c                sar       ecx,0Ch
  004956e7  896e58                mov       [esi+58h],ebp
  004956ea  03d7                  add       edx,edi
  004956ec  8b7e20                mov       edi,[esi+20h]
  004956ef  03cd                  add       ecx,ebp
  004956f1  8b6e24                mov       ebp,[esi+24h]
  004956f4  03da                  add       ebx,edx
  004956f6  03f8                  add       edi,eax
  004956f8  03e9                  add       ebp,ecx
  004956fa  895e1c                mov       [esi+1Ch],ebx
  004956fd  897e20                mov       [esi+20h],edi
  00495700  896e24                mov       [esi+24h],ebp
  00495703  e838e20900            call      00533940h
  00495708  8b9690000000          mov       edx,[esi+90h]
  0049570e  8b4660                mov       eax,[esi+60h]
  00495711  8b4e20                mov       ecx,[esi+20h]
  00495714  83c404                add       esp,4
  00495717  80e6fc                and       dh,0FCh
  0049571a  3bc8                  cmp       ecx,eax
  0049571c  899690000000          mov       [esi+90h],edx
  00495722  7f2a                  jg        short 0049574Eh
  00495724  f6c680                test      dh,80h
  00495727  7525                  jne       short 0049574Eh
  00495729  8b0e                  mov       ecx,[esi]
  0049572b  894620                mov       [esi+20h],eax
  0049572e  f6c101                test      cl,1
  00495731  740f                  je        short 00495742h
  00495733  83e1fe                and       ecx,0FFFFFFFEh
  00495736  33db                  xor       ebx,ebx
  00495738  890e                  mov       [esi],ecx
  0049573a  895e54                mov       [esi+54h],ebx
  0049573d  e923010000            jmp       00495865h

LAB_00495742:
  00495742  83e1fe                and       ecx,0FFFFFFFEh
  00495745  33db                  xor       ebx,ebx
  00495747  890e                  mov       [esi],ecx
  00495749  895e54                mov       [esi+54h],ebx
  0049574c  eb02                  jmp       short 00495750h

LAB_0049574e:
  0049574e  33db                  xor       ebx,ebx

LAB_00495750:
  00495750  8b0e                  mov       ecx,[esi]
  00495752  f6c101                test      cl,1
  00495755  750a                  jne       short 00495761h
  00495757  394620                cmp       [esi+20h],eax
  0049575a  7e05                  jle       short 00495761h
  0049575c  83c901                or        ecx,1
  0049575f  890e                  mov       [esi],ecx

LAB_00495761:
  00495761  f7c282ad0020          test      edx,2000AD82h
  00495767  0f84f8000000          je        near 00495865h
  0049576d  f60601                test      byte [esi],1
  00495770  0f85c7000000          jne       near 0049583Dh
  00495776  807e4207              cmp       byte [esi+42h],7
  0049577a  7421                  je        short 0049579Dh
  0049577c  c6464207              mov       byte [esi+42h],7
  00495780  e8bb190b00            call      00547140h
  00495785  99                    cdq
  00495786  b928000000            mov       ecx,28h
  0049578b  66895e12              mov       [esi+12h],bx
  0049578f  f7f9                  idiv      ecx
  00495791  66c746105000          mov       word [esi+10h],50h
  00495797  80c214                add       dl,14h
  0049579a  885643                mov       [esi+43h],dl

LAB_0049579d:
  0049579d  8b16                  mov       edx,[esi]
  0049579f  8b44242c              mov       eax,[esp+2Ch]
  004957a3  8b4c2430              mov       ecx,[esp+30h]
  004957a7  83e2fd                and       edx,0FFFFFFFDh
  004957aa  8916                  mov       [esi],edx
  004957ac  8b542428              mov       edx,[esp+28h]
  004957b0  894620                mov       [esi+20h],eax
  004957b3  8b44246c              mov       eax,[esp+6Ch]
  004957b7  89561c                mov       [esi+1Ch],edx
  004957ba  8b542464              mov       edx,[esp+64h]
  004957be  25ffff0000            and       eax,0FFFFh
  004957c3  894e24                mov       [esi+24h],ecx
  004957c6  8b4c244c              mov       ecx,[esp+4Ch]
  004957ca  89567c                mov       [esi+7Ch],edx
  004957cd  8b542458              mov       edx,[esp+58h]
  004957d1  898690000000          mov       [esi+90h],eax
  004957d7  8b442460              mov       eax,[esp+60h]
  004957db  898e84000000          mov       [esi+84h],ecx
  004957e1  8b4c2450              mov       ecx,[esp+50h]
  004957e5  898680000000          mov       [esi+80h],eax
  004957eb  8b442468              mov       eax,[esp+68h]
  004957ef  899688000000          mov       [esi+88h],edx
  004957f5  8b542454              mov       edx,[esp+54h]
  004957f9  6882ad0020            push      2000AD82h
  004957fe  56                    push      esi
  004957ff  89868c000000          mov       [esi+8Ch],eax
  00495805  894e64                mov       [esi+64h],ecx
  00495808  895660                mov       [esi+60h],edx
  0049580b  e850060400            call      004D5E60h
  00495810  83c408                add       esp,8
  00495813  663bc3                cmp       ax,bx
  00495816  7515                  jne       short 0049582Dh
  00495818  e823190b00            call      00547140h
  0049581d  99                    cdq
  0049581e  33c2                  xor       eax,edx
  00495820  2bc2                  sub       eax,edx
  00495822  83e001                and       eax,1
  00495825  33c2                  xor       eax,edx
  00495827  2bc2                  sub       eax,edx
  00495829  8d4400ff              lea       eax,[eax+eax-1]

LAB_0049582d:
  0049582d  8b4c2410              mov       ecx,[esp+10h]
  00495831  83c140                add       ecx,40h
  00495834  0fafc8                imul      ecx,eax
  00495837  66014e32              add       [esi+32h],cx
  0049583b  eb28                  jmp       short 00495865h

LAB_0049583d:
  0049583d  8b542428              mov       edx,[esp+28h]
  00495841  8b442430              mov       eax,[esp+30h]
  00495845  895e5c                mov       [esi+5Ch],ebx
  00495848  895e50                mov       [esi+50h],ebx
  0049584b  895e58                mov       [esi+58h],ebx
  0049584e  89561c                mov       [esi+1Ch],edx
  00495851  894624                mov       [esi+24h],eax
  00495854  885e42                mov       [esi+42h],bl
  00495857  c6464328              mov       byte [esi+43h],28h
  0049585b  66895e12              mov       [esi+12h],bx
  0049585f  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_00495865:
  00495865  6aff                  push      0FFFFFFFFh
  00495867  6aff                  push      0FFFFFFFFh
  00495869  6aff                  push      0FFFFFFFFh
  0049586b  56                    push      esi
  0049586c  e89fdf0200            call      004C3810h
  00495871  8b442430              mov       eax,[esp+30h]
  00495875  83c410                add       esp,10h
  00495878  3bc3                  cmp       eax,ebx
  0049587a  7517                  jne       short 00495893h
  0049587c  668b8c248e000000      mov       cx,[esp+8Eh]
  00495884  66894e32              mov       [esi+32h],cx
  00495888  5f                    pop       edi
  00495889  5e                    pop       esi
  0049588a  5d                    pop       ebp
  0049588b  5b                    pop       ebx
  0049588c  81c490000000          add       esp,90h
  00495892  c3                    ret

LAB_00495893:
  00495893  385e43                cmp       [esi+43h],bl
  00495896  0f856a030000          jne       near 00495C06h
  0049589c  8b466c                mov       eax,[esi+6Ch]
  0049589f  8b561c                mov       edx,[esi+1Ch]
  004958a2  8b781c                mov       edi,[eax+1Ch]
  004958a5  8b6824                mov       ebp,[eax+24h]
  004958a8  8b4624                mov       eax,[esi+24h]
  004958ab  2bfa                  sub       edi,edx
  004958ad  2be8                  sub       ebp,eax
  004958af  c1ff0c                sar       edi,0Ch
  004958b2  c1fd0c                sar       ebp,0Ch
  004958b5  8bd7                  mov       edx,edi
  004958b7  8bc5                  mov       eax,ebp
  004958b9  0fafd7                imul      edx,edi
  004958bc  0fafc5                imul      eax,ebp
  004958bf  03d0                  add       edx,eax
  004958c1  52                    push      edx
  004958c2  e849010b00            call      00545A10h
  004958c7  55                    push      ebp
  004958c8  57                    push      edi
  004958c9  8944241c              mov       [esp+1Ch],eax
  004958cd  e850010b00            call      00545A22h
  004958d2  668b4e32              mov       cx,[esi+32h]
  004958d6  66895e12              mov       [esi+12h],bx
  004958da  2bc1                  sub       eax,ecx
  004958dc  25ff0f0000            and       eax,0FFFh
  004958e1  89442418              mov       [esp+18h],eax
  004958e5  8b442414              mov       eax,[esp+14h]
  004958e9  3dfa000000            cmp       eax,0FAh
  004958ee  0f8dd9000000          jge       near 004959CDh
  004958f4  e847180b00            call      00547140h
  004958f9  99                    cdq
  004958fa  b964000000            mov       ecx,64h
  004958ff  f7f9                  idiv      ecx
  00495901  8bea                  mov       ebp,edx
  00495903  83fd32                cmp       ebp,32h
  00495906  7d77                  jge       short 0049597Fh
  00495908  8b542418              mov       edx,[esp+18h]
  0049590c  8b7c241c              mov       edi,[esp+1Ch]
  00495910  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00495917  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049591e  25ff0f0000            and       eax,0FFFh
  00495923  3bc1                  cmp       eax,ecx
  00495925  7f4e                  jg        short 00495975h
  00495927  e814180b00            call      00547140h
  0049592c  99                    cdq
  0049592d  8d4c3f01              lea       ecx,[edi+edi+1]
  00495931  f7f9                  idiv      ecx
  00495933  8b442414              mov       eax,[esp+14h]
  00495937  83c264                add       edx,64h
  0049593a  3bc2                  cmp       eax,edx
  0049593c  7d37                  jge       short 00495975h
  0049593e  83fd1e                cmp       ebp,1Eh
  00495941  7d24                  jge       short 00495967h
  00495943  8b16                  mov       edx,[esi]
  00495945  c646420b              mov       byte [esi+42h],0Bh
  00495949  81e2fffffffe          and       edx,0FEFFFFFFh
  0049594f  c6464301              mov       byte [esi+43h],1
  00495953  83ca02                or        edx,2
  00495956  895e5c                mov       [esi+5Ch],ebx
  00495959  8916                  mov       [esi],edx
  0049595b  66c746180300          mov       word [esi+18h],3
  00495961  66895e1a              mov       [esi+1Ah],bx
  00495965  eb6a                  jmp       short 004959D1h

LAB_00495967:
  00495967  c646420c              mov       byte [esi+42h],0Ch
  0049596b  c6464301              mov       byte [esi+43h],1
  0049596f  66895e12              mov       [esi+12h],bx
  00495973  eb5c                  jmp       short 004959D1h

LAB_00495975:
  00495975  c6464201              mov       byte [esi+42h],1
  00495979  c6464301              mov       byte [esi+43h],1
  0049597d  eb52                  jmp       short 004959D1h

LAB_0049597f:
  0049597f  83fd3c                cmp       ebp,3Ch
  00495982  7d29                  jge       short 004959ADh
  00495984  c6464202              mov       byte [esi+42h],2
  00495988  e8b3170b00            call      00547140h
  0049598d  99                    cdq
  0049598e  b90a000000            mov       ecx,0Ah
  00495993  8b7c241c              mov       edi,[esp+1Ch]
  00495997  f7f9                  idiv      ecx
  00495999  8bc7                  mov       eax,edi
  0049599b  66c746105000          mov       word [esi+10h],50h
  004959a1  d1f8                  sar       eax,1
  004959a3  02d0                  add       dl,al
  004959a5  80c214                add       dl,14h
  004959a8  885643                mov       [esi+43h],dl
  004959ab  eb24                  jmp       short 004959D1h

LAB_004959ad:
  004959ad  83fd5a                cmp       ebp,5Ah
  004959b0  0f8d5b020000          jge       near 00495C11h
  004959b6  837c241464            cmp       dword [esp+14h],64h
  004959bb  0f8d50020000          jge       near 00495C11h
  004959c1  c646420d              mov       byte [esi+42h],0Dh
  004959c5  c6464301              mov       byte [esi+43h],1
  004959c9  66895e12              mov       [esi+12h],bx

LAB_004959cd:
  004959cd  8b7c241c              mov       edi,[esp+1Ch]

LAB_004959d1:
  004959d1  8b442414              mov       eax,[esp+14h]
  004959d5  3d58020000            cmp       eax,258h
  004959da  0f8dea000000          jge       near 00495ACAh
  004959e0  3dfa000000            cmp       eax,0FAh
  004959e5  0f8cdf000000          jl        near 00495ACAh
  004959eb  e850170b00            call      00547140h
  004959f0  99                    cdq
  004959f1  b964000000            mov       ecx,64h
  004959f6  f7f9                  idiv      ecx
  004959f8  83fa14                cmp       edx,14h
  004959fb  7d40                  jge       short 00495A3Dh
  004959fd  8b542418              mov       edx,[esp+18h]
  00495a01  81c200040000          add       edx,400h
  00495a07  81e2ff0f0000          and       edx,0FFFh
  00495a0d  81fa00080000          cmp       edx,800h
  00495a13  7c0d                  jl        short 00495A22h
  00495a15  c6464201              mov       byte [esi+42h],1
  00495a19  c6464301              mov       byte [esi+43h],1
  00495a1d  e9a8000000            jmp       00495ACAh

LAB_00495a22:
  00495a22  c6464207              mov       byte [esi+42h],7
  00495a26  e815170b00            call      00547140h
  00495a2b  99                    cdq
  00495a2c  b914000000            mov       ecx,14h
  00495a31  f7f9                  idiv      ecx
  00495a33  02d1                  add       dl,cl
  00495a35  885643                mov       [esi+43h],dl
  00495a38  e989000000            jmp       00495AC6h

LAB_00495a3d:
  00495a3d  83fa28                cmp       edx,28h
  00495a40  7d25                  jge       short 00495A67h
  00495a42  c6464202              mov       byte [esi+42h],2
  00495a46  e8f5160b00            call      00547140h
  00495a4b  99                    cdq
  00495a4c  b90a000000            mov       ecx,0Ah
  00495a51  66c746105000          mov       word [esi+10h],50h
  00495a57  f7f9                  idiv      ecx
  00495a59  8bc7                  mov       eax,edi
  00495a5b  d1f8                  sar       eax,1
  00495a5d  02d0                  add       dl,al
  00495a5f  80c214                add       dl,14h
  00495a62  885643                mov       [esi+43h],dl
  00495a65  eb63                  jmp       short 00495ACAh

LAB_00495a67:
  00495a67  83fa3c                cmp       edx,3Ch
  00495a6a  7d13                  jge       short 00495A7Fh
  00495a6c  c6464206              mov       byte [esi+42h],6
  00495a70  c6464301              mov       byte [esi+43h],1
  00495a74  e8c7160b00            call      00547140h
  00495a79  66894610              mov       [esi+10h],ax
  00495a7d  eb47                  jmp       short 00495AC6h

LAB_00495a7f:
  00495a7f  83fa50                cmp       edx,50h
  00495a82  7d3a                  jge       short 00495ABEh
  00495a84  c6464203              mov       byte [esi+42h],3
  00495a88  66895e12              mov       [esi+12h],bx
  00495a8c  e8af160b00            call      00547140h
  00495a91  99                    cdq
  00495a92  b90a000000            mov       ecx,0Ah
  00495a97  f7f9                  idiv      ecx
  00495a99  8bc7                  mov       eax,edi
  00495a9b  d1f8                  sar       eax,1
  00495a9d  02d0                  add       dl,al
  00495a9f  80c214                add       dl,14h
  00495aa2  885643                mov       [esi+43h],dl
  00495aa5  e896160b00            call      00547140h
  00495aaa  99                    cdq
  00495aab  33c2                  xor       eax,edx
  00495aad  2bc2                  sub       eax,edx
  00495aaf  25ff0f0000            and       eax,0FFFh
  00495ab4  33c2                  xor       eax,edx
  00495ab6  2bc2                  sub       eax,edx
  00495ab8  66894610              mov       [esi+10h],ax
  00495abc  eb0c                  jmp       short 00495ACAh

LAB_00495abe:
  00495abe  c646420a              mov       byte [esi+42h],0Ah
  00495ac2  c6464301              mov       byte [esi+43h],1

LAB_00495ac6:
  00495ac6  66895e12              mov       [esi+12h],bx

LAB_00495aca:
  00495aca  817c241458020000      cmp       dword [esp+14h],258h
  00495ad2  0f8c2e010000          jl        near 00495C06h
  00495ad8  e863160b00            call      00547140h
  00495add  bd64000000            mov       ebp,64h
  00495ae2  99                    cdq
  00495ae3  8bcd                  mov       ecx,ebp
  00495ae5  f7f9                  idiv      ecx
  00495ae7  83fa1e                cmp       edx,1Eh
  00495aea  7d45                  jge       short 00495B31h
  00495aec  8b442410              mov       eax,[esp+10h]
  00495af0  8d4c000a              lea       ecx,[eax+eax+0Ah]
  00495af4  3bd1                  cmp       edx,ecx
  00495af6  0f8ccd000000          jl        near 00495BC9h
  00495afc  885e42                mov       [esi+42h],bl
  00495aff  e83c160b00            call      00547140h
  00495b04  99                    cdq
  00495b05  b90a000000            mov       ecx,0Ah
  00495b0a  66895e12              mov       [esi+12h],bx
  00495b0e  f7f9                  idiv      ecx
  00495b10  8b4c2410              mov       ecx,[esp+10h]
  00495b14  8bc7                  mov       eax,edi
  00495b16  c1f802                sar       eax,2
  00495b19  02d0                  add       dl,al
  00495b1b  885643                mov       [esi+43h],dl
  00495b1e  8d540950              lea       edx,[ecx+ecx+50h]
  00495b22  66895610              mov       [esi+10h],dx
  00495b26  5f                    pop       edi
  00495b27  5e                    pop       esi
  00495b28  5d                    pop       ebp
  00495b29  5b                    pop       ebx
  00495b2a  81c490000000          add       esp,90h
  00495b30  c3                    ret

LAB_00495b31:
  00495b31  83fa28                cmp       edx,28h
  00495b34  7d43                  jge       short 00495B79h
  00495b36  c6464203              mov       byte [esi+42h],3
  00495b3a  66895e12              mov       [esi+12h],bx
  00495b3e  e8fd150b00            call      00547140h
  00495b43  99                    cdq
  00495b44  b90a000000            mov       ecx,0Ah
  00495b49  f7f9                  idiv      ecx
  00495b4b  8bc7                  mov       eax,edi
  00495b4d  d1f8                  sar       eax,1
  00495b4f  02d0                  add       dl,al
  00495b51  80c214                add       dl,14h
  00495b54  885643                mov       [esi+43h],dl
  00495b57  e8e4150b00            call      00547140h
  00495b5c  99                    cdq
  00495b5d  33c2                  xor       eax,edx
  00495b5f  2bc2                  sub       eax,edx
  00495b61  25ff0f0000            and       eax,0FFFh
  00495b66  33c2                  xor       eax,edx
  00495b68  2bc2                  sub       eax,edx
  00495b6a  66894610              mov       [esi+10h],ax
  00495b6e  5f                    pop       edi
  00495b6f  5e                    pop       esi
  00495b70  5d                    pop       ebp
  00495b71  5b                    pop       ebx
  00495b72  81c490000000          add       esp,90h
  00495b78  c3                    ret

LAB_00495b79:
  00495b79  83fa3c                cmp       edx,3Ch
  00495b7c  7d13                  jge       short 00495B91h
  00495b7e  c6464201              mov       byte [esi+42h],1
  00495b82  c6464301              mov       byte [esi+43h],1
  00495b86  5f                    pop       edi
  00495b87  5e                    pop       esi
  00495b88  5d                    pop       ebp
  00495b89  5b                    pop       ebx
  00495b8a  81c490000000          add       esp,90h
  00495b90  c3                    ret

LAB_00495b91:
  00495b91  83fa50                cmp       edx,50h
  00495b94  7d5f                  jge       short 00495BF5h
  00495b96  8b4c2418              mov       ecx,[esp+18h]
  00495b9a  8d04fd00020000        lea       eax,[edi*8+200h]
  00495ba1  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  00495ba8  81e2ff0f0000          and       edx,0FFFh
  00495bae  3bd0                  cmp       edx,eax
  00495bb0  7f17                  jg        short 00495BC9h
  00495bb2  c646420a              mov       byte [esi+42h],0Ah
  00495bb6  c6464301              mov       byte [esi+43h],1
  00495bba  66895e12              mov       [esi+12h],bx
  00495bbe  5f                    pop       edi
  00495bbf  5e                    pop       esi
  00495bc0  5d                    pop       ebp
  00495bc1  5b                    pop       ebx
  00495bc2  81c490000000          add       esp,90h
  00495bc8  c3                    ret

LAB_00495bc9:
  00495bc9  c6464202              mov       byte [esi+42h],2
  00495bcd  e86e150b00            call      00547140h
  00495bd2  99                    cdq
  00495bd3  b90a000000            mov       ecx,0Ah
  00495bd8  66896e10              mov       [esi+10h],bp
  00495bdc  f7f9                  idiv      ecx
  00495bde  8bc7                  mov       eax,edi
  00495be0  d1f8                  sar       eax,1
  00495be2  02d0                  add       dl,al
  00495be4  80c214                add       dl,14h
  00495be7  885643                mov       [esi+43h],dl
  00495bea  5f                    pop       edi
  00495beb  5e                    pop       esi
  00495bec  5d                    pop       ebp
  00495bed  5b                    pop       ebx
  00495bee  81c490000000          add       esp,90h
  00495bf4  c3                    ret

LAB_00495bf5:
  00495bf5  c6464206              mov       byte [esi+42h],6
  00495bf9  c6464301              mov       byte [esi+43h],1
  00495bfd  e83e150b00            call      00547140h
  00495c02  66894610              mov       [esi+10h],ax

LAB_00495c06:
  00495c06  5f                    pop       edi
  00495c07  5e                    pop       esi
  00495c08  5d                    pop       ebp
  00495c09  5b                    pop       ebx
  00495c0a  81c490000000          add       esp,90h
  00495c10  c3                    ret

LAB_00495c11:
  00495c11  c6464203              mov       byte [esi+42h],3
  00495c15  66895e12              mov       [esi+12h],bx
  00495c19  e822150b00            call      00547140h
  00495c1e  99                    cdq
  00495c1f  b90a000000            mov       ecx,0Ah
  00495c24  8b7c241c              mov       edi,[esp+1Ch]
  00495c28  f7f9                  idiv      ecx
  00495c2a  8bc7                  mov       eax,edi
  00495c2c  d1f8                  sar       eax,1
  00495c2e  02d0                  add       dl,al
  00495c30  80c214                add       dl,14h
  00495c33  885643                mov       [esi+43h],dl
  00495c36  e805150b00            call      00547140h
  00495c3b  99                    cdq
  00495c3c  33c2                  xor       eax,edx
  00495c3e  2bc2                  sub       eax,edx
  00495c40  25ff0f0000            and       eax,0FFFh
  00495c45  33c2                  xor       eax,edx
  00495c47  2bc2                  sub       eax,edx
  00495c49  66894610              mov       [esi+10h],ax
  00495c4d  e97ffdffff            jmp       004959D1h
  00495c52  8bff                  mov       edi,edi
  00495c54  8d5149                lea       edx,[ecx+49h]
  00495c57  00d5                  add       ch,dl
  00495c59  51                    push      ecx
  00495c5a  49                    dec       ecx
  00495c5b  003f                  add       [edi],bh
  00495c5d  52                    push      edx
  00495c5e  49                    dec       ecx
  00495c5f  00dc                  add       ah,bl
  00495c61  52                    push      edx
  00495c62  49                    dec       ecx
  00495c63  006256                add       [edx+56h],ah
  00495c66  49                    dec       ecx
  00495c67  0005554900ad          add       [0AD004955h],al
  00495c6d  53                    push      ebx
  00495c6e  49                    dec       ecx
  00495c6f  004054                add       [eax+54h],al
  00495c72  49                    dec       ecx
  00495c73  006256                add       [edx+56h],ah
  00495c76  49                    dec       ecx
  00495c77  006256                add       [edx+56h],ah
  00495c7a  49                    dec       ecx
  00495c7b  009d5449000e          add       [ebp+0E004954h],bl
  00495c81  55                    push      ebp
  00495c82  49                    dec       ecx
  00495c83  005055                add       [eax+55h],dl
  00495c86  49                    dec       ecx
  00495c87  00bf55490090          add       [edi-6FFFB6ABh],bh
  00495c8d  90                    nop
  00495c8e  90                    nop
  00495c8f  90                    nop

80 cible(s) de saut interne : 0x00494d86, 0x00494ed6, 0x00494ef7, 0x00494f05, 0x00494f08, 0x00494f7a, 0x00494f91, 0x00494f95, 0x00494faf, 0x00494fc4, 0x00494ff4, 0x00495001, 0x00495013, 0x00495022, 0x00495064, 0x00495120, 0x0049512e, 0x0049516c, 0x00495170, 0x00495174, 0x00495178, 0x004951ad, 0x004951c0, 0x004951f6, 0x00495217, 0x0049522f, 0x0049526f, 0x0049528c, 0x00495298, 0x004952cc, 0x004952f7, 0x00495374, 0x00495382, 0x00495386, 0x004953bf, 0x0049542e, 0x0049545e, 0x0049546a, 0x004954c7, 0x004954d4, 0x004954fb, 0x00495529, 0x0049556b, 0x00495593, 0x004955e3, 0x00495613, 0x0049561d, 0x00495621, 0x00495640, 0x0049565f, 0x00495662, 0x00495742, 0x0049574e, 0x00495750, 0x00495761, 0x0049579d, 0x0049582d, 0x0049583d, 0x00495865, 0x00495893, 0x00495967, 0x00495975, 0x0049597f, 0x004959ad, 0x004959cd, 0x004959d1, 0x00495a22, 0x00495a3d, 0x00495a67, 0x00495a7f, 0x00495abe, 0x00495ac6, 0x00495aca, 0x00495b31, 0x00495b79, 0x00495b91, 0x00495bc9, 0x00495bf5, 0x00495c06, 0x00495c11
