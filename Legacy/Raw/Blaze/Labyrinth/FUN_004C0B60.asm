Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c0b60  section .text  offset fichier 0xbff60  4112 octets

  004c0b60  81ec84000000          sub       esp,84h
  004c0b66  53                    push      ebx
  004c0b67  55                    push      ebp
  004c0b68  56                    push      esi
  004c0b69  8bb42494000000        mov       esi,[esp+94h]
  004c0b70  33db                  xor       ebx,ebx
  004c0b72  57                    push      edi
  004c0b73  8a4641                mov       al,[esi+41h]
  004c0b76  66895c2430            mov       [esp+30h],bx
  004c0b7b  3ac3                  cmp       al,bl
  004c0b7d  66895c2432            mov       [esp+32h],bx
  004c0b82  66c74424343200        mov       word [esp+34h],32h
  004c0b89  66c74424366400        mov       word [esp+36h],64h
  004c0b90  66895c2438            mov       [esp+38h],bx
  004c0b95  66c744243a8200        mov       word [esp+3Ah],82h
  004c0b9c  751a                  jne       short 004C0BB8h
  004c0b9e  c6464101              mov       byte [esi+41h],1
  004c0ba2  66395e16              cmp       [esi+16h],bx
  004c0ba6  53                    push      ebx
  004c0ba7  7504                  jne       short 004C0BADh
  004c0ba9  6a02                  push      2
  004c0bab  eb02                  jmp       short 004C0BAFh

LAB_004c0bad:
  004c0bad  6a03                  push      3

LAB_004c0baf:
  004c0baf  56                    push      esi
  004c0bb0  e88bd2ffff            call      004BDE40h
  004c0bb5  83c40c                add       esp,0Ch

LAB_004c0bb8:
  004c0bb8  8b461c                mov       eax,[esi+1Ch]
  004c0bbb  8b4e20                mov       ecx,[esi+20h]
  004c0bbe  8b5624                mov       edx,[esi+24h]
  004c0bc1  89442424              mov       [esp+24h],eax
  004c0bc5  8b4664                mov       eax,[esi+64h]
  004c0bc8  894c2428              mov       [esp+28h],ecx
  004c0bcc  8b4e60                mov       ecx,[esi+60h]
  004c0bcf  8954242c              mov       [esp+2Ch],edx
  004c0bd3  668b5632              mov       dx,[esi+32h]
  004c0bd7  66837e1603            cmp       word [esi+16h],3
  004c0bdc  89442444              mov       [esp+44h],eax
  004c0be0  8b467c                mov       eax,[esi+7Ch]
  004c0be3  894c2440              mov       [esp+40h],ecx
  004c0be7  668b8e90000000        mov       cx,[esi+90h]
  004c0bee  6689942482000000      mov       [esp+82h],dx
  004c0bf6  8b9680000000          mov       edx,[esi+80h]
  004c0bfc  8944245c              mov       [esp+5Ch],eax
  004c0c00  8b8684000000          mov       eax,[esi+84h]
  004c0c06  66894c2448            mov       [esp+48h],cx
  004c0c0b  8b8e88000000          mov       ecx,[esi+88h]
  004c0c11  8954244c              mov       [esp+4Ch],edx
  004c0c15  8b968c000000          mov       edx,[esi+8Ch]
  004c0c1b  8944243c              mov       [esp+3Ch],eax
  004c0c1f  894c2458              mov       [esp+58h],ecx
  004c0c23  89542450              mov       [esp+50h],edx
  004c0c27  7523                  jne       short 004C0C4Ch
  004c0c29  b888130000            mov       eax,1388h
  004c0c2e  66895e16              mov       [esi+16h],bx
  004c0c32  66894628              mov       [esi+28h],ax
  004c0c36  6689462c              mov       [esi+2Ch],ax
  004c0c3a  b0dc                  mov       al,0DCh
  004c0c3c  66c7462a6810          mov       word [esi+2Ah],1068h
  004c0c42  884638                mov       [esi+38h],al
  004c0c45  884639                mov       [esi+39h],al
  004c0c48  c6463aaa              mov       byte [esi+3Ah],0AAh

LAB_004c0c4c:
  004c0c4c  66ff4614              inc       word [esi+14h]
  004c0c50  668b4614              mov       ax,[esi+14h]
  004c0c54  663d3075              cmp       ax,7530h
  004c0c58  7e06                  jle       short 004C0C60h
  004c0c5a  66c74614e803          mov       word [esi+14h],3E8h

LAB_004c0c60:
  004c0c60  8d442460              lea       eax,[esp+60h]
  004c0c64  8d4c2468              lea       ecx,[esp+68h]
  004c0c68  50                    push      eax
  004c0c69  8d542468              lea       edx,[esp+68h]
  004c0c6d  51                    push      ecx
  004c0c6e  52                    push      edx
  004c0c6f  56                    push      esi
  004c0c70  e80b7d0700            call      00538980h
  004c0c75  83c410                add       esp,10h
  004c0c78  8d442454              lea       eax,[esp+54h]
  004c0c7c  8d4c2420              lea       ecx,[esp+20h]
  004c0c80  8d542418              lea       edx,[esp+18h]
  004c0c84  50                    push      eax
  004c0c85  51                    push      ecx
  004c0c86  8d44241c              lea       eax,[esp+1Ch]
  004c0c8a  52                    push      edx
  004c0c8b  8d4c241c              lea       ecx,[esp+1Ch]
  004c0c8f  50                    push      eax
  004c0c90  51                    push      ecx
  004c0c91  56                    push      esi
  004c0c92  e889520700            call      00535F20h
  004c0c97  8b4c2428              mov       ecx,[esp+28h]
  004c0c9b  8b54246c              mov       edx,[esp+6Ch]
  004c0c9f  83c418                add       esp,18h
  004c0ca2  b840000000            mov       eax,40h
  004c0ca7  2bc1                  sub       eax,ecx
  004c0ca9  52                    push      edx
  004c0caa  56                    push      esi
  004c0cab  89442424              mov       [esp+24h],eax
  004c0caf  e82c510100            call      004D5DE0h
  004c0cb4  8b442428              mov       eax,[esp+28h]
  004c0cb8  83c408                add       esp,8
  004c0cbb  3bc3                  cmp       eax,ebx
  004c0cbd  0f84fe070000          je        near 004C14C1h
  004c0cc3  8b2e                  mov       ebp,[esi]
  004c0cc5  33d2                  xor       edx,edx
  004c0cc7  8a5604                mov       dl,[esi+4]
  004c0cca  81e5fffffffd          and       ebp,0FDFFFFFFh
  004c0cd0  0fbf7e18              movsx     edi,word [esi+18h]
  004c0cd4  892e                  mov       [esi],ebp
  004c0cd6  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004c0cdd  8bcd                  mov       ecx,ebp
  004c0cdf  8b14ba                mov       edx,[edx+edi*4]
  004c0ce2  f7c100000001          test      ecx,1000000h
  004c0ce8  741b                  je        short 004C0D05h
  004c0cea  66ff461a              inc       word [esi+1Ah]
  004c0cee  668b4e1a              mov       cx,[esi+1Ah]
  004c0cf2  33ff                  xor       edi,edi
  004c0cf4  668b7a16              mov       di,[edx+16h]
  004c0cf8  0fbfc9                movsx     ecx,cx
  004c0cfb  3bcf                  cmp       ecx,edi
  004c0cfd  7c28                  jl        short 004C0D27h
  004c0cff  66895e1a              mov       [esi+1Ah],bx
  004c0d03  eb22                  jmp       short 004C0D27h

LAB_004c0d05:
  004c0d05  66ff461a              inc       word [esi+1Ah]
  004c0d09  668b7e1a              mov       di,[esi+1Ah]
  004c0d0d  33ed                  xor       ebp,ebp
  004c0d0f  668b6a16              mov       bp,[edx+16h]
  004c0d13  0fbfd7                movsx     edx,di
  004c0d16  3bd5                  cmp       edx,ebp
  004c0d18  7c0d                  jl        short 004C0D27h
  004c0d1a  4f                    dec       edi
  004c0d1b  81c900000002          or        ecx,2000000h
  004c0d21  66897e1a              mov       [esi+1Ah],di
  004c0d25  890e                  mov       [esi],ecx

LAB_004c0d27:
  004c0d27  8b0e                  mov       ecx,[esi]
  004c0d29  f6c102                test      cl,2
  004c0d2c  0f84b20d0000          je        near 004C1AE4h
  004c0d32  bd0d000000            mov       ebp,0Dh
  004c0d37  66396e1a              cmp       [esi+1Ah],bp
  004c0d3b  0f85e9000000          jne       near 004C0E2Ah
  004c0d41  8d442430              lea       eax,[esp+30h]
  004c0d45  53                    push      ebx
  004c0d46  50                    push      eax
  004c0d47  8d461c                lea       eax,[esi+1Ch]
  004c0d4a  50                    push      eax
  004c0d4b  56                    push      esi
  004c0d4c  e8df8e0500            call      00519C30h
  004c0d51  8bf8                  mov       edi,eax
  004c0d53  a1b8e05d00            mov       eax,[5DE0B8h]
  004c0d58  83c410                add       esp,10h
  004c0d5b  3bc3                  cmp       eax,ebx
  004c0d5d  0f84c3000000          je        near 004C0E26h
  004c0d63  66396e1a              cmp       [esi+1Ah],bp
  004c0d67  0f85b9000000          jne       near 004C0E26h
  004c0d6d  8b17                  mov       edx,[edi]
  004c0d6f  57                    push      edi
  004c0d70  83ca40                or        edx,40h
  004c0d73  56                    push      esi
  004c0d74  66c787960000001000    mov       word [edi+96h],10h
  004c0d7d  8917                  mov       [edi],edx
  004c0d7f  e8fc8d0700            call      00539B80h
  004c0d84  83c408                add       esp,8
  004c0d87  8d4f1c                lea       ecx,[edi+1Ch]
  004c0d8a  51                    push      ecx
  004c0d8b  6800180000            push      1800h
  004c0d90  6800030000            push      300h
  004c0d95  6a03                  push      3
  004c0d97  e884cd0700            call      0053DB20h
  004c0d9c  83c410                add       esp,10h
  004c0d9f  33ed                  xor       ebp,ebp
  004c0da1  e89a630800            call      00547140h
  004c0da6  99                    cdq
  004c0da7  b914000000            mov       ecx,14h
  004c0dac  f7f9                  idiv      ecx
  004c0dae  83fa01                cmp       edx,1
  004c0db1  7516                  jne       short 004C0DC9h
  004c0db3  66395e16              cmp       [esi+16h],bx
  004c0db7  7410                  je        short 004C0DC9h
  004c0db9  53                    push      ebx
  004c0dba  57                    push      edi
  004c0dbb  56                    push      esi
  004c0dbc  e8ef0d0000            call      004C1BB0h
  004c0dc1  83c40c                add       esp,0Ch
  004c0dc4  bd01000000            mov       ebp,1

LAB_004c0dc9:
  004c0dc9  e872630800            call      00547140h
  004c0dce  99                    cdq
  004c0dcf  b914000000            mov       ecx,14h
  004c0dd4  f7f9                  idiv      ecx
  004c0dd6  83fa01                cmp       edx,1
  004c0dd9  751a                  jne       short 004C0DF5h
  004c0ddb  66395e16              cmp       [esi+16h],bx
  004c0ddf  7514                  jne       short 004C0DF5h
  004c0de1  3beb                  cmp       ebp,ebx
  004c0de3  7510                  jne       short 004C0DF5h
  004c0de5  53                    push      ebx
  004c0de6  57                    push      edi
  004c0de7  56                    push      esi
  004c0de8  e8030e0000            call      004C1BF0h
  004c0ded  83c40c                add       esp,0Ch
  004c0df0  bd01000000            mov       ebp,1

LAB_004c0df5:
  004c0df5  e846630800            call      00547140h
  004c0dfa  99                    cdq
  004c0dfb  b914000000            mov       ecx,14h
  004c0e00  f7f9                  idiv      ecx
  004c0e02  83fa01                cmp       edx,1
  004c0e05  751f                  jne       short 004C0E26h
  004c0e07  668b4616              mov       ax,[esi+16h]
  004c0e0b  663d0200              cmp       ax,2
  004c0e0f  7406                  je        short 004C0E17h
  004c0e11  663d0300              cmp       ax,3
  004c0e15  750f                  jne       short 004C0E26h

LAB_004c0e17:
  004c0e17  3beb                  cmp       ebp,ebx
  004c0e19  750b                  jne       short 004C0E26h
  004c0e1b  53                    push      ebx
  004c0e1c  57                    push      edi
  004c0e1d  56                    push      esi
  004c0e1e  e84d0d0000            call      004C1B70h
  004c0e23  83c40c                add       esp,0Ch

LAB_004c0e26:
  004c0e26  8b442420              mov       eax,[esp+20h]

LAB_004c0e2a:
  004c0e2a  8b0e                  mov       ecx,[esi]
  004c0e2c  f7c100000002          test      ecx,2000000h
  004c0e32  7411                  je        short 004C0E45h
  004c0e34  f6c101                test      cl,1
  004c0e37  0f859d0c0000          jne       near 004C1ADAh
  004c0e3d  83e1fd                and       ecx,0FFFFFFFDh
  004c0e40  885e43                mov       [esi+43h],bl
  004c0e43  890e                  mov       [esi],ecx

LAB_004c0e45:
  004c0e45  8b7c2410              mov       edi,[esp+10h]

LAB_004c0e49:
  004c0e49  33c9                  xor       ecx,ecx
  004c0e4b  8a4e42                mov       cl,[esi+42h]
  004c0e4e  83f90d                cmp       ecx,0Dh
  004c0e51  0f876a060000          ja        near 004C14C1h
  004c0e57  ff248d381b4c00        jmp       dword [ecx*4+4C1B38h]
  004c0e5e  66395e12              cmp       [esi+12h],bx
  004c0e62  751a                  jne       short 004C0E7Eh
  004c0e64  8b06                  mov       eax,[esi]
  004c0e66  66895e18              mov       [esi+18h],bx
  004c0e6a  0d00000001            or        eax,1000000h
  004c0e6f  66895e1a              mov       [esi+1Ah],bx
  004c0e73  8906                  mov       [esi],eax
  004c0e75  66c746120100          mov       word [esi+12h],1
  004c0e7b  895e5c                mov       [esi+5Ch],ebx

LAB_004c0e7e:
  004c0e7e  0fbf4610              movsx     eax,word [esi+10h]
  004c0e82  39442414              cmp       [esp+14h],eax
  004c0e86  7d08                  jge       short 004C0E90h
  004c0e88  885e43                mov       [esi+43h],bl
  004c0e8b  e931060000            jmp       004C14C1h

LAB_004c0e90:
  004c0e90  8a4643                mov       al,[esi+43h]
  004c0e93  3ac3                  cmp       al,bl
  004c0e95  0f84bb000000          je        near 004C0F56h
  004c0e9b  fec8                  dec       al
  004c0e9d  895e5c                mov       [esi+5Ch],ebx
  004c0ea0  884643                mov       [esi+43h],al
  004c0ea3  e919060000            jmp       004C14C1h
  004c0ea8  66395e12              cmp       [esi+12h],bx
  004c0eac  751b                  jne       short 004C0EC9h
  004c0eae  8b0e                  mov       ecx,[esi]
  004c0eb0  b801000000            mov       eax,1
  004c0eb5  81c900000001          or        ecx,1000000h
  004c0ebb  66894618              mov       [esi+18h],ax
  004c0ebf  66895e1a              mov       [esi+1Ah],bx
  004c0ec3  890e                  mov       [esi],ecx
  004c0ec5  66894612              mov       [esi+12h],ax

LAB_004c0ec9:
  004c0ec9  8b442418              mov       eax,[esp+18h]
  004c0ecd  8d88c8000000          lea       ecx,[eax+0C8h]
  004c0ed3  81e1ff0f0000          and       ecx,0FFFh
  004c0ed9  81f990010000          cmp       ecx,190h
  004c0edf  7e72                  jle       short 004C0F53h
  004c0ee1  25ff0f0000            and       eax,0FFFh
  004c0ee6  3d00080000            cmp       eax,800h
  004c0eeb  7d12                  jge       short 004C0EFFh
  004c0eed  8d8780000000          lea       eax,[edi+80h]
  004c0ef3  895e5c                mov       [esi+5Ch],ebx
  004c0ef6  66014632              add       [esi+32h],ax
  004c0efa  e9c2050000            jmp       004C14C1h

LAB_004c0eff:
  004c0eff  b880ffffff            mov       eax,0FFFFFF80h
  004c0f04  895e5c                mov       [esi+5Ch],ebx
  004c0f07  2bc7                  sub       eax,edi
  004c0f09  66014632              add       [esi+32h],ax
  004c0f0d  e9af050000            jmp       004C14C1h
  004c0f12  66395e12              cmp       [esi+12h],bx
  004c0f16  b801000000            mov       eax,1
  004c0f1b  7522                  jne       short 004C0F3Fh
  004c0f1d  8b16                  mov       edx,[esi]
  004c0f1f  81c780000000          add       edi,80h
  004c0f25  81ca00000001          or        edx,1000000h
  004c0f2b  66894618              mov       [esi+18h],ax
  004c0f2f  c1e707                shl       edi,7
  004c0f32  66895e1a              mov       [esi+1Ah],bx
  004c0f36  8916                  mov       [esi],edx
  004c0f38  66894612              mov       [esi+12h],ax
  004c0f3c  897e5c                mov       [esi+5Ch],edi

LAB_004c0f3f:
  004c0f3f  66394612              cmp       [esi+12h],ax
  004c0f43  0f8578050000          jne       near 004C14C1h
  004c0f49  0fbf5610              movsx     edx,word [esi+10h]
  004c0f4d  39542414              cmp       [esp+14h],edx
  004c0f51  7d0b                  jge       short 004C0F5Eh

LAB_004c0f53:
  004c0f53  885e43                mov       [esi+43h],bl

LAB_004c0f56:
  004c0f56  895e5c                mov       [esi+5Ch],ebx
  004c0f59  e963050000            jmp       004C14C1h

LAB_004c0f5e:
  004c0f5e  8a4643                mov       al,[esi+43h]
  004c0f61  3ac3                  cmp       al,bl
  004c0f63  7405                  je        short 004C0F6Ah
  004c0f65  fec8                  dec       al
  004c0f67  884643                mov       [esi+43h],al

LAB_004c0f6a:
  004c0f6a  8b442418              mov       eax,[esp+18h]
  004c0f6e  8d8800010000          lea       ecx,[eax+100h]
  004c0f74  81e1ff0f0000          and       ecx,0FFFh
  004c0f7a  81f900020000          cmp       ecx,200h
  004c0f80  0f8e3b050000          jle       near 004C14C1h
  004c0f86  25ff0f0000            and       eax,0FFFh
  004c0f8b  33d2                  xor       edx,edx
  004c0f8d  3d00080000            cmp       eax,800h
  004c0f92  0f9dc2                setge     dl
  004c0f95  4a                    dec       edx
  004c0f96  81e2a0000000          and       edx,0A0h
  004c0f9c  83c2b0                add       edx,0FFFFFFB0h
  004c0f9f  66015632              add       [esi+32h],dx
  004c0fa3  e919050000            jmp       004C14C1h
  004c0fa8  bd01000000            mov       ebp,1
  004c0fad  66396e12              cmp       [esi+12h],bp
  004c0fb1  0f8581000000          jne       near 004C1038h
  004c0fb7  8a4e43                mov       cl,[esi+43h]
  004c0fba  3acb                  cmp       cl,bl
  004c0fbc  7405                  je        short 004C0FC3h
  004c0fbe  fec9                  dec       cl
  004c0fc0  884e43                mov       [esi+43h],cl

LAB_004c0fc3:
  004c0fc3  8d8f00010000          lea       ecx,[edi+100h]
  004c0fc9  c1e106                shl       ecx,6
  004c0fcc  0fafc1                imul      eax,ecx
  004c0fcf  c1f80c                sar       eax,0Ch
  004c0fd2  8bf8                  mov       edi,eax
  004c0fd4  894e5c                mov       [esi+5Ch],ecx
  004c0fd7  0fbf4632              movsx     eax,word [esi+32h]
  004c0fdb  50                    push      eax
  004c0fdc  e83b4a0800            call      00545A1Ch
  004c0fe1  0fafc7                imul      eax,edi
  004c0fe4  8b4e1c                mov       ecx,[esi+1Ch]
  004c0fe7  c1f80c                sar       eax,0Ch
  004c0fea  03c8                  add       ecx,eax
  004c0fec  894e1c                mov       [esi+1Ch],ecx
  004c0fef  0fbf4e32              movsx     ecx,word [esi+32h]
  004c0ff3  51                    push      ecx
  004c0ff4  e81d4a0800            call      00545A16h
  004c0ff9  0fafc7                imul      eax,edi
  004c0ffc  8b5624                mov       edx,[esi+24h]
  004c0fff  895e5c                mov       [esi+5Ch],ebx
  004c1002  c1f80c                sar       eax,0Ch
  004c1005  03d0                  add       edx,eax
  004c1007  895624                mov       [esi+24h],edx
  004c100a  8b542418              mov       edx,[esp+18h]
  004c100e  81c200010000          add       edx,100h
  004c1014  81e2ff0f0000          and       edx,0FFFh
  004c101a  81fa00020000          cmp       edx,200h
  004c1020  7e16                  jle       short 004C1038h
  004c1022  668b4632              mov       ax,[esi+32h]
  004c1026  0fbf4e10              movsx     ecx,word [esi+10h]
  004c102a  0fbfd0                movsx     edx,ax
  004c102d  2bca                  sub       ecx,edx
  004c102f  c1f903                sar       ecx,3
  004c1032  03c8                  add       ecx,eax
  004c1034  66894e32              mov       [esi+32h],cx

LAB_004c1038:
  004c1038  66395e12              cmp       [esi+12h],bx
  004c103c  0f857f040000          jne       near 004C14C1h
  004c1042  8b06                  mov       eax,[esi]
  004c1044  66896e18              mov       [esi+18h],bp
  004c1048  0d00000001            or        eax,1000000h
  004c104d  66895e1a              mov       [esi+1Ah],bx
  004c1051  8906                  mov       [esi],eax
  004c1053  66896e12              mov       [esi+12h],bp
  004c1057  e965040000            jmp       004C14C1h
  004c105c  66395e12              cmp       [esi+12h],bx
  004c1060  757b                  jne       short 004C10DDh
  004c1062  8b0e                  mov       ecx,[esi]
  004c1064  f6c101                test      cl,1
  004c1067  7405                  je        short 004C106Eh
  004c1069  885e43                mov       [esi+43h],bl
  004c106c  eb6f                  jmp       short 004C10DDh

LAB_004c106e:
  004c106e  81e1fffffffe          and       ecx,0FEFFFFFFh
  004c1074  bd01000000            mov       ebp,1
  004c1079  890e                  mov       [esi],ecx
  004c107b  8d8fc0000000          lea       ecx,[edi+0C0h]
  004c1081  c1e106                shl       ecx,6
  004c1084  0fafc1                imul      eax,ecx
  004c1087  c1f80c                sar       eax,0Ch
  004c108a  8bf8                  mov       edi,eax
  004c108c  66896e18              mov       [esi+18h],bp
  004c1090  0fbf4610              movsx     eax,word [esi+10h]
  004c1094  50                    push      eax
  004c1095  66895e1a              mov       [esi+1Ah],bx
  004c1099  894e5c                mov       [esi+5Ch],ecx
  004c109c  e87b490800            call      00545A1Ch
  004c10a1  0fafc7                imul      eax,edi
  004c10a4  8b4e50                mov       ecx,[esi+50h]
  004c10a7  c1f80c                sar       eax,0Ch
  004c10aa  03c8                  add       ecx,eax
  004c10ac  894e50                mov       [esi+50h],ecx
  004c10af  0fbf4e10              movsx     ecx,word [esi+10h]
  004c10b3  51                    push      ecx
  004c10b4  e85d490800            call      00545A16h
  004c10b9  0fafc7                imul      eax,edi
  004c10bc  8b5658                mov       edx,[esi+58h]
  004c10bf  8b4e54                mov       ecx,[esi+54h]
  004c10c2  c1f80c                sar       eax,0Ch
  004c10c5  03d0                  add       edx,eax
  004c10c7  8b06                  mov       eax,[esi]
  004c10c9  81c100400100          add       ecx,14000h
  004c10cf  0bc5                  or        eax,ebp
  004c10d1  895658                mov       [esi+58h],edx
  004c10d4  894e54                mov       [esi+54h],ecx
  004c10d7  8906                  mov       [esi],eax
  004c10d9  66896e12              mov       [esi+12h],bp

LAB_004c10dd:
  004c10dd  f60601                test      byte [esi],1
  004c10e0  0f85db030000          jne       near 004C14C1h
  004c10e6  885e43                mov       [esi+43h],bl
  004c10e9  e9d3030000            jmp       004C14C1h
  004c10ee  668b4e12              mov       cx,[esi+12h]
  004c10f2  ba01000000            mov       edx,1
  004c10f7  663bca                cmp       cx,dx
  004c10fa  751b                  jne       short 004C1117h
  004c10fc  0fbf4610              movsx     eax,word [esi+10h]
  004c1100  39442414              cmp       [esp+14h],eax
  004c1104  7d05                  jge       short 004C110Bh
  004c1106  885e43                mov       [esi+43h],bl
  004c1109  eb0c                  jmp       short 004C1117h

LAB_004c110b:
  004c110b  8a4643                mov       al,[esi+43h]
  004c110e  3ac3                  cmp       al,bl
  004c1110  7405                  je        short 004C1117h
  004c1112  fec8                  dec       al
  004c1114  884643                mov       [esi+43h],al

LAB_004c1117:
  004c1117  663bcb                cmp       cx,bx
  004c111a  0f85a1030000          jne       near 004C14C1h
  004c1120  8b2e                  mov       ebp,[esi]
  004c1122  66895618              mov       [esi+18h],dx
  004c1126  d1ff                  sar       edi,1
  004c1128  81cd00000001          or        ebp,1000000h
  004c112e  81c700400000          add       edi,4000h
  004c1134  66895e1a              mov       [esi+1Ah],bx
  004c1138  892e                  mov       [esi],ebp
  004c113a  66895612              mov       [esi+12h],dx
  004c113e  897e5c                mov       [esi+5Ch],edi
  004c1141  e97b030000            jmp       004C14C1h
  004c1146  8b4c2418              mov       ecx,[esp+18h]
  004c114a  81e1ff0f0000          and       ecx,0FFFh
  004c1150  81f900080000          cmp       ecx,800h
  004c1156  7d08                  jge       short 004C1160h
  004c1158  8d8780000000          lea       eax,[edi+80h]
  004c115e  eb07                  jmp       short 004C1167h

LAB_004c1160:
  004c1160  b880ffffff            mov       eax,0FFFFFF80h
  004c1165  2bc7                  sub       eax,edi

LAB_004c1167:
  004c1167  66014632              add       [esi+32h],ax
  004c116b  bf01000000            mov       edi,1
  004c1170  66397e12              cmp       [esi+12h],di
  004c1174  7515                  jne       short 004C118Bh
  004c1176  66837e1a11            cmp       word [esi+1Ah],11h
  004c117b  750e                  jne       short 004C118Bh
  004c117d  6aff                  push      0FFFFFFFFh
  004c117f  53                    push      ebx
  004c1180  6a36                  push      36h
  004c1182  56                    push      esi
  004c1183  e84898fbff            call      0047A9D0h
  004c1188  83c410                add       esp,10h

LAB_004c118b:
  004c118b  8b06                  mov       eax,[esi]
  004c118d  a900000002            test      eax,2000000h
  004c1192  7403                  je        short 004C1197h
  004c1194  885e43                mov       [esi+43h],bl

LAB_004c1197:
  004c1197  66395e12              cmp       [esi+12h],bx
  004c119b  0f8520030000          jne       near 004C14C1h
  004c11a1  25fffffffe            and       eax,0FEFFFFFFh
  004c11a6  66817e14c800          cmp       word [esi+14h],0C8h
  004c11ac  66897e12              mov       [esi+12h],di
  004c11b0  66c746180400          mov       word [esi+18h],4
  004c11b6  66895e1a              mov       [esi+1Ah],bx
  004c11ba  8906                  mov       [esi],eax
  004c11bc  895e5c                mov       [esi+5Ch],ebx
  004c11bf  7d2e                  jge       short 004C11EFh
  004c11c1  885e42                mov       [esi+42h],bl
  004c11c4  e8775f0800            call      00547140h
  004c11c9  99                    cdq
  004c11ca  b90a000000            mov       ecx,0Ah
  004c11cf  66895e12              mov       [esi+12h],bx
  004c11d3  f7f9                  idiv      ecx
  004c11d5  8b44241c              mov       eax,[esp+1Ch]
  004c11d9  8b4c2410              mov       ecx,[esp+10h]
  004c11dd  c1f802                sar       eax,2
  004c11e0  02d0                  add       dl,al
  004c11e2  885643                mov       [esi+43h],dl
  004c11e5  8d540950              lea       edx,[ecx+ecx+50h]
  004c11e9  66895610              mov       [esi+10h],dx
  004c11ed  eb04                  jmp       short 004C11F3h

LAB_004c11ef:
  004c11ef  66895e14              mov       [esi+14h],bx

LAB_004c11f3:
  004c11f3  668b4616              mov       ax,[esi+16h]
  004c11f7  663bc3                cmp       ax,bx
  004c11fa  0f8459010000          je        near 004C1359h
  004c1200  663bc7                cmp       ax,di
  004c1203  0f85b8020000          jne       near 004C14C1h
  004c1209  885e42                mov       [esi+42h],bl
  004c120c  e82f5f0800            call      00547140h
  004c1211  99                    cdq
  004c1212  b90a000000            mov       ecx,0Ah
  004c1217  f7f9                  idiv      ecx
  004c1219  8b44241c              mov       eax,[esp+1Ch]
  004c121d  8b4c2410              mov       ecx,[esp+10h]
  004c1221  c1f802                sar       eax,2
  004c1224  02d0                  add       dl,al
  004c1226  885643                mov       [esi+43h],dl
  004c1229  8d540950              lea       edx,[ecx+ecx+50h]
  004c122d  e987020000            jmp       004C14B9h
  004c1232  668b4e12              mov       cx,[esi+12h]
  004c1236  6683f901              cmp       cx,1
  004c123a  7510                  jne       short 004C124Ch
  004c123c  8b06                  mov       eax,[esi]
  004c123e  a900000002            test      eax,2000000h
  004c1243  7407                  je        short 004C124Ch
  004c1245  24fd                  and       al,0FDh
  004c1247  885e43                mov       [esi+43h],bl
  004c124a  8906                  mov       [esi],eax

LAB_004c124c:
  004c124c  663bcb                cmp       cx,bx
  004c124f  0f856c020000          jne       near 004C14C1h
  004c1255  8b06                  mov       eax,[esi]
  004c1257  66c746120100          mov       word [esi+12h],1
  004c125d  25fffffffe            and       eax,0FEFFFFFFh
  004c1262  8906                  mov       [esi],eax
  004c1264  e958020000            jmp       004C14C1h
  004c1269  8b442418              mov       eax,[esp+18h]
  004c126d  25ff0f0000            and       eax,0FFFh
  004c1272  3d00080000            cmp       eax,800h
  004c1277  7d08                  jge       short 004C1281h
  004c1279  8d8780000000          lea       eax,[edi+80h]
  004c127f  eb07                  jmp       short 004C1288h

LAB_004c1281:
  004c1281  b880ffffff            mov       eax,0FFFFFF80h
  004c1286  2bc7                  sub       eax,edi

LAB_004c1288:
  004c1288  66014632              add       [esi+32h],ax
  004c128c  bd01000000            mov       ebp,1
  004c1291  66396e12              cmp       [esi+12h],bp
  004c1295  7547                  jne       short 004C12DEh
  004c1297  817c2414e8030000      cmp       dword [esp+14h],3E8h
  004c129f  7e08                  jle       short 004C12A9h
  004c12a1  885e43                mov       [esi+43h],bl
  004c12a4  e918020000            jmp       004C14C1h

LAB_004c12a9:
  004c12a9  66837e1a11            cmp       word [esi+1Ah],11h
  004c12ae  752e                  jne       short 004C12DEh
  004c12b0  8b4e6c                mov       ecx,[esi+6Ch]
  004c12b3  6aff                  push      0FFFFFFFFh
  004c12b5  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004c12bc  53                    push      ebx
  004c12bd  68df000000            push      0DFh
  004c12c2  56                    push      esi
  004c12c3  66c7869a0000000200    mov       word [esi+9Ah],2
  004c12cc  894e78                mov       [esi+78h],ecx
  004c12cf  e8fc96fbff            call      0047A9D0h
  004c12d4  83c410                add       esp,10h
  004c12d7  6689be9a000000        mov       [esi+9Ah],di

LAB_004c12de:
  004c12de  8b06                  mov       eax,[esi]
  004c12e0  a900000002            test      eax,2000000h
  004c12e5  7403                  je        short 004C12EAh
  004c12e7  885e43                mov       [esi+43h],bl

LAB_004c12ea:
  004c12ea  66395e12              cmp       [esi+12h],bx
  004c12ee  0f85cd010000          jne       near 004C14C1h
  004c12f4  25fffffffe            and       eax,0FEFFFFFFh
  004c12f9  66817e14c800          cmp       word [esi+14h],0C8h
  004c12ff  66896e12              mov       [esi+12h],bp
  004c1303  66c746180400          mov       word [esi+18h],4
  004c1309  66895e1a              mov       [esi+1Ah],bx
  004c130d  8906                  mov       [esi],eax
  004c130f  895e5c                mov       [esi+5Ch],ebx
  004c1312  7d2e                  jge       short 004C1342h
  004c1314  885e42                mov       [esi+42h],bl
  004c1317  e8245e0800            call      00547140h
  004c131c  99                    cdq
  004c131d  b90a000000            mov       ecx,0Ah
  004c1322  66895e12              mov       [esi+12h],bx
  004c1326  f7f9                  idiv      ecx
  004c1328  8b44241c              mov       eax,[esp+1Ch]
  004c132c  8b4c2410              mov       ecx,[esp+10h]
  004c1330  c1f802                sar       eax,2
  004c1333  02d0                  add       dl,al
  004c1335  885643                mov       [esi+43h],dl
  004c1338  8d540950              lea       edx,[ecx+ecx+50h]
  004c133c  66895610              mov       [esi+10h],dx
  004c1340  eb04                  jmp       short 004C1346h

LAB_004c1342:
  004c1342  66895e14              mov       [esi+14h],bx

LAB_004c1346:
  004c1346  668b4616              mov       ax,[esi+16h]
  004c134a  663bc3                cmp       ax,bx
  004c134d  740a                  je        short 004C1359h
  004c134f  663d0200              cmp       ax,2
  004c1353  0f8568010000          jne       near 004C14C1h

LAB_004c1359:
  004c1359  885e42                mov       [esi+42h],bl
  004c135c  e8df5d0800            call      00547140h
  004c1361  99                    cdq
  004c1362  b90a000000            mov       ecx,0Ah
  004c1367  f7f9                  idiv      ecx
  004c1369  8b44241c              mov       eax,[esp+1Ch]
  004c136d  8b4c2410              mov       ecx,[esp+10h]
  004c1371  c1f802                sar       eax,2
  004c1374  02d0                  add       dl,al
  004c1376  885643                mov       [esi+43h],dl
  004c1379  8d540950              lea       edx,[ecx+ecx+50h]
  004c137d  e937010000            jmp       004C14B9h
  004c1382  8b442418              mov       eax,[esp+18h]
  004c1386  25ff0f0000            and       eax,0FFFh
  004c138b  3d00080000            cmp       eax,800h
  004c1390  7d08                  jge       short 004C139Ah
  004c1392  8d8780000000          lea       eax,[edi+80h]
  004c1398  eb07                  jmp       short 004C13A1h

LAB_004c139a:
  004c139a  b880ffffff            mov       eax,0FFFFFF80h
  004c139f  2bc7                  sub       eax,edi

LAB_004c13a1:
  004c13a1  66014632              add       [esi+32h],ax
  004c13a5  66837e1201            cmp       word [esi+12h],1
  004c13aa  7549                  jne       short 004C13F5h
  004c13ac  817c2414e8030000      cmp       dword [esp+14h],3E8h
  004c13b4  7e08                  jle       short 004C13BEh
  004c13b6  885e43                mov       [esi+43h],bl
  004c13b9  e903010000            jmp       004C14C1h

LAB_004c13be:
  004c13be  66837e1a11            cmp       word [esi+1Ah],11h
  004c13c3  7530                  jne       short 004C13F5h
  004c13c5  8b4e6c                mov       ecx,[esi+6Ch]
  004c13c8  6aff                  push      0FFFFFFFFh
  004c13ca  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004c13d1  53                    push      ebx
  004c13d2  68df000000            push      0DFh
  004c13d7  56                    push      esi
  004c13d8  66899e9a000000        mov       [esi+9Ah],bx
  004c13df  894e78                mov       [esi+78h],ecx
  004c13e2  e8e995fbff            call      0047A9D0h
  004c13e7  6689be9a000000        mov       [esi+9Ah],di
  004c13ee  8b7c2420              mov       edi,[esp+20h]
  004c13f2  83c410                add       esp,10h

LAB_004c13f5:
  004c13f5  8b2e                  mov       ebp,[esi]
  004c13f7  f7c500000002          test      ebp,2000000h
  004c13fd  7403                  je        short 004C1402h
  004c13ff  885e43                mov       [esi+43h],bl

LAB_004c1402:
  004c1402  66395e12              cmp       [esi+12h],bx
  004c1406  0f85b5000000          jne       near 004C14C1h
  004c140c  b800f45f00            mov       eax,5FF400h

LAB_004c1411:
  004c1411  39706c                cmp       [eax+6Ch],esi
  004c1414  7508                  jne       short 004C141Eh
  004c1416  f70000000080          test      dword [eax],80000000h
  004c141c  7530                  jne       short 004C144Eh

LAB_004c141e:
  004c141e  059c000000            add       eax,9Ch
  004c1423  3d80076000            cmp       eax,600780h
  004c1428  7ce7                  jl        short 004C1411h
  004c142a  33c9                  xor       ecx,ecx
  004c142c  b8e2ca5e00            mov       eax,5ECAE2h

LAB_004c1431:
  004c1431  80380d                cmp       byte [eax],0Dh
  004c1434  7509                  jne       short 004C143Fh
  004c1436  33d2                  xor       edx,edx
  004c1438  8a5608                mov       dl,[esi+8]
  004c143b  3bca                  cmp       ecx,edx
  004c143d  750f                  jne       short 004C144Eh

LAB_004c143f:
  004c143f  059c000000            add       eax,9Ch
  004c1444  41                    inc       ecx
  004c1445  3d62de5e00            cmp       eax,5EDE62h
  004c144a  7ce5                  jl        short 004C1431h
  004c144c  eb05                  jmp       short 004C1453h

LAB_004c144e:
  004c144e  885e43                mov       [esi+43h],bl
  004c1451  eb6e                  jmp       short 004C14C1h

LAB_004c1453:
  004c1453  81e5fffffffe          and       ebp,0FEFFFFFFh
  004c1459  66817e14c800          cmp       word [esi+14h],0C8h
  004c145f  66c746120100          mov       word [esi+12h],1
  004c1465  66c746180400          mov       word [esi+18h],4
  004c146b  66895e1a              mov       [esi+1Ah],bx
  004c146f  892e                  mov       [esi],ebp
  004c1471  895e5c                mov       [esi+5Ch],ebx
  004c1474  7d2e                  jge       short 004C14A4h
  004c1476  885e42                mov       [esi+42h],bl
  004c1479  e8c25c0800            call      00547140h
  004c147e  99                    cdq
  004c147f  b90a000000            mov       ecx,0Ah
  004c1484  8b7c2410              mov       edi,[esp+10h]
  004c1488  f7f9                  idiv      ecx
  004c148a  8b44241c              mov       eax,[esp+1Ch]
  004c148e  8d4c3f50              lea       ecx,[edi+edi+50h]
  004c1492  c1f802                sar       eax,2
  004c1495  66894e10              mov       [esi+10h],cx
  004c1499  66895e12              mov       [esi+12h],bx
  004c149d  02d0                  add       dl,al
  004c149f  885643                mov       [esi+43h],dl
  004c14a2  eb04                  jmp       short 004C14A8h

LAB_004c14a4:
  004c14a4  66895e14              mov       [esi+14h],bx

LAB_004c14a8:
  004c14a8  66837e1602            cmp       word [esi+16h],2
  004c14ad  7512                  jne       short 004C14C1h
  004c14af  885e42                mov       [esi+42h],bl
  004c14b2  885e43                mov       [esi+43h],bl
  004c14b5  8d543f50              lea       edx,[edi+edi+50h]

LAB_004c14b9:
  004c14b9  66895610              mov       [esi+10h],dx
  004c14bd  66895e12              mov       [esi+12h],bx

LAB_004c14c1:
  004c14c1  0fbf4632              movsx     eax,word [esi+32h]
  004c14c5  50                    push      eax
  004c14c6  e851450800            call      00545A1Ch
  004c14cb  0fbf4e32              movsx     ecx,word [esi+32h]
  004c14cf  51                    push      ecx
  004c14d0  8bf8                  mov       edi,eax
  004c14d2  e83f450800            call      00545A16h
  004c14d7  8b4c2420              mov       ecx,[esp+20h]
  004c14db  8b6e1c                mov       ebp,[esi+1Ch]
  004c14de  0faf4e5c              imul      ecx,[esi+5Ch]
  004c14e2  c1f90c                sar       ecx,0Ch
  004c14e5  8bd1                  mov       edx,ecx
  004c14e7  0fafc8                imul      ecx,eax
  004c14ea  0fafd7                imul      edx,edi
  004c14ed  c1fa0c                sar       edx,0Ch
  004c14f0  8b4650                mov       eax,[esi+50h]
  004c14f3  03ea                  add       ebp,edx
  004c14f5  896e1c                mov       [esi+1Ch],ebp
  004c14f8  8b5624                mov       edx,[esi+24h]
  004c14fb  c1f90c                sar       ecx,0Ch
  004c14fe  03d1                  add       edx,ecx
  004c1500  8b7e54                mov       edi,[esi+54h]
  004c1503  895624                mov       [esi+24h],edx
  004c1506  8bd0                  mov       edx,eax
  004c1508  c1fa03                sar       edx,3
  004c150b  8bc8                  mov       ecx,eax
  004c150d  8bef                  mov       ebp,edi
  004c150f  2bca                  sub       ecx,edx
  004c1511  56                    push      esi
  004c1512  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004c1519  c1f808                sar       eax,8
  004c151c  2bc8                  sub       ecx,eax
  004c151e  8bc2                  mov       eax,edx
  004c1520  c1fd08                sar       ebp,8
  004c1523  c1f808                sar       eax,8
  004c1526  2bc5                  sub       eax,ebp
  004c1528  894e50                mov       [esi+50h],ecx
  004c152b  03c7                  add       eax,edi
  004c152d  2bc2                  sub       eax,edx
  004c152f  8b5658                mov       edx,[esi+58h]
  004c1532  8bea                  mov       ebp,edx
  004c1534  8bfa                  mov       edi,edx
  004c1536  c1fd08                sar       ebp,8
  004c1539  2bfd                  sub       edi,ebp
  004c153b  894654                mov       [esi+54h],eax
  004c153e  c1fa03                sar       edx,3
  004c1541  2bfa                  sub       edi,edx
  004c1543  8b561c                mov       edx,[esi+1Ch]
  004c1546  03d1                  add       edx,ecx
  004c1548  897e58                mov       [esi+58h],edi
  004c154b  89561c                mov       [esi+1Ch],edx
  004c154e  8b4e20                mov       ecx,[esi+20h]
  004c1551  03c8                  add       ecx,eax
  004c1553  894e20                mov       [esi+20h],ecx
  004c1556  8b4624                mov       eax,[esi+24h]
  004c1559  03c7                  add       eax,edi
  004c155b  894624                mov       [esi+24h],eax
  004c155e  e8dd230700            call      00533940h
  004c1563  8b9690000000          mov       edx,[esi+90h]
  004c1569  8b4660                mov       eax,[esi+60h]
  004c156c  8b4e20                mov       ecx,[esi+20h]
  004c156f  83c404                add       esp,4
  004c1572  80e6fc                and       dh,0FCh
  004c1575  3bc8                  cmp       ecx,eax
  004c1577  899690000000          mov       [esi+90h],edx
  004c157d  7f24                  jg        short 004C15A3h
  004c157f  f6c680                test      dh,80h
  004c1582  751f                  jne       short 004C15A3h
  004c1584  8b0e                  mov       ecx,[esi]
  004c1586  894620                mov       [esi+20h],eax
  004c1589  f6c101                test      cl,1
  004c158c  740d                  je        short 004C159Bh
  004c158e  83e1fe                and       ecx,0FFFFFFFEh
  004c1591  895e54                mov       [esi+54h],ebx
  004c1594  890e                  mov       [esi],ecx
  004c1596  e92f010000            jmp       004C16CAh

LAB_004c159b:
  004c159b  83e1fe                and       ecx,0FFFFFFFEh
  004c159e  895e54                mov       [esi+54h],ebx
  004c15a1  890e                  mov       [esi],ecx

LAB_004c15a3:
  004c15a3  8b0e                  mov       ecx,[esi]
  004c15a5  f6c101                test      cl,1
  004c15a8  750a                  jne       short 004C15B4h
  004c15aa  394620                cmp       [esi+20h],eax
  004c15ad  7e05                  jle       short 004C15B4h
  004c15af  83c901                or        ecx,1
  004c15b2  890e                  mov       [esi],ecx

LAB_004c15b4:
  004c15b4  f7c282ad0020          test      edx,2000AD82h
  004c15ba  0f840a010000          je        near 004C16CAh
  004c15c0  f60601                test      byte [esi],1
  004c15c3  0f85d9000000          jne       near 004C16A2h
  004c15c9  8a4e42                mov       cl,[esi+42h]
  004c15cc  b007                  mov       al,7
  004c15ce  3ac8                  cmp       cl,al
  004c15d0  7420                  je        short 004C15F2h
  004c15d2  884642                mov       [esi+42h],al
  004c15d5  e8665b0800            call      00547140h
  004c15da  99                    cdq
  004c15db  b928000000            mov       ecx,28h
  004c15e0  66895e12              mov       [esi+12h],bx
  004c15e4  f7f9                  idiv      ecx
  004c15e6  66c746105000          mov       word [esi+10h],50h
  004c15ec  80c214                add       dl,14h
  004c15ef  885643                mov       [esi+43h],dl

LAB_004c15f2:
  004c15f2  8b16                  mov       edx,[esi]
  004c15f4  8b442428              mov       eax,[esp+28h]
  004c15f8  83e2fd                and       edx,0FFFFFFFDh
  004c15fb  8b4c242c              mov       ecx,[esp+2Ch]
  004c15ff  8916                  mov       [esi],edx
  004c1601  8b542424              mov       edx,[esp+24h]
  004c1605  89561c                mov       [esi+1Ch],edx
  004c1608  8b54245c              mov       edx,[esp+5Ch]
  004c160c  894620                mov       [esi+20h],eax
  004c160f  8b442448              mov       eax,[esp+48h]
  004c1613  25ffff0000            and       eax,0FFFFh
  004c1618  894e24                mov       [esi+24h],ecx
  004c161b  8b4c243c              mov       ecx,[esp+3Ch]
  004c161f  89567c                mov       [esi+7Ch],edx
  004c1622  8b542458              mov       edx,[esp+58h]
  004c1626  898690000000          mov       [esi+90h],eax
  004c162c  8b44244c              mov       eax,[esp+4Ch]
  004c1630  898e84000000          mov       [esi+84h],ecx
  004c1636  8b4c2444              mov       ecx,[esp+44h]
  004c163a  898680000000          mov       [esi+80h],eax
  004c1640  8b442450              mov       eax,[esp+50h]
  004c1644  899688000000          mov       [esi+88h],edx
  004c164a  8b542440              mov       edx,[esp+40h]
  004c164e  6882ad0020            push      2000AD82h
  004c1653  56                    push      esi
  004c1654  89868c000000          mov       [esi+8Ch],eax
  004c165a  894e64                mov       [esi+64h],ecx
  004c165d  895660                mov       [esi+60h],edx
  004c1660  e8fb470100            call      004D5E60h
  004c1665  83c408                add       esp,8
  004c1668  663bc3                cmp       ax,bx
  004c166b  7525                  jne       short 004C1692h
  004c166d  e8ce5a0800            call      00547140h
  004c1672  99                    cdq
  004c1673  33c2                  xor       eax,edx
  004c1675  8b4c2410              mov       ecx,[esp+10h]
  004c1679  2bc2                  sub       eax,edx
  004c167b  83c140                add       ecx,40h
  004c167e  83e001                and       eax,1
  004c1681  33c2                  xor       eax,edx
  004c1683  2bc2                  sub       eax,edx
  004c1685  8d4400ff              lea       eax,[eax+eax-1]
  004c1689  0fafc8                imul      ecx,eax
  004c168c  66014e32              add       [esi+32h],cx
  004c1690  eb38                  jmp       short 004C16CAh

LAB_004c1692:
  004c1692  8b542410              mov       edx,[esp+10h]
  004c1696  83c240                add       edx,40h
  004c1699  0fafd0                imul      edx,eax
  004c169c  66015632              add       [esi+32h],dx
  004c16a0  eb28                  jmp       short 004C16CAh

LAB_004c16a2:
  004c16a2  8b442424              mov       eax,[esp+24h]
  004c16a6  8b4c242c              mov       ecx,[esp+2Ch]
  004c16aa  89461c                mov       [esi+1Ch],eax
  004c16ad  895e5c                mov       [esi+5Ch],ebx
  004c16b0  895e50                mov       [esi+50h],ebx
  004c16b3  895e58                mov       [esi+58h],ebx
  004c16b6  894e24                mov       [esi+24h],ecx
  004c16b9  885e42                mov       [esi+42h],bl
  004c16bc  c6464328              mov       byte [esi+43h],28h
  004c16c0  66895e12              mov       [esi+12h],bx
  004c16c4  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004c16ca:
  004c16ca  6aff                  push      0FFFFFFFFh
  004c16cc  6aff                  push      0FFFFFFFFh
  004c16ce  6aff                  push      0FFFFFFFFh
  004c16d0  56                    push      esi
  004c16d1  e83a210000            call      004C3810h
  004c16d6  8b442430              mov       eax,[esp+30h]
  004c16da  83c410                add       esp,10h
  004c16dd  3bc3                  cmp       eax,ebx
  004c16df  7517                  jne       short 004C16F8h
  004c16e1  668b942482000000      mov       dx,[esp+82h]
  004c16e9  66895632              mov       [esi+32h],dx
  004c16ed  5f                    pop       edi
  004c16ee  5e                    pop       esi
  004c16ef  5d                    pop       ebp
  004c16f0  5b                    pop       ebx
  004c16f1  81c484000000          add       esp,84h
  004c16f7  c3                    ret

LAB_004c16f8:
  004c16f8  385e43                cmp       [esi+43h],bl
  004c16fb  0f85ce030000          jne       near 004C1ACFh
  004c1701  8b466c                mov       eax,[esi+6Ch]
  004c1704  8b6e1c                mov       ebp,[esi+1Ch]
  004c1707  8b4e24                mov       ecx,[esi+24h]
  004c170a  8b781c                mov       edi,[eax+1Ch]
  004c170d  2bfd                  sub       edi,ebp
  004c170f  8b6824                mov       ebp,[eax+24h]
  004c1712  2be9                  sub       ebp,ecx
  004c1714  c1ff0c                sar       edi,0Ch
  004c1717  c1fd0c                sar       ebp,0Ch
  004c171a  8bc7                  mov       eax,edi
  004c171c  8bcd                  mov       ecx,ebp
  004c171e  0fafc7                imul      eax,edi
  004c1721  0fafcd                imul      ecx,ebp
  004c1724  03c1                  add       eax,ecx
  004c1726  50                    push      eax
  004c1727  e8e4420800            call      00545A10h
  004c172c  55                    push      ebp
  004c172d  57                    push      edi
  004c172e  8944241c              mov       [esp+1Ch],eax
  004c1732  e8eb420800            call      00545A22h
  004c1737  668b5632              mov       dx,[esi+32h]
  004c173b  66895e12              mov       [esi+12h],bx
  004c173f  2bc2                  sub       eax,edx
  004c1741  25ff0f0000            and       eax,0FFFh
  004c1746  89442418              mov       [esp+18h],eax
  004c174a  8b442414              mov       eax,[esp+14h]
  004c174e  3d96000000            cmp       eax,96h
  004c1753  0f8dcf000000          jge       near 004C1828h
  004c1759  e8e2590800            call      00547140h
  004c175e  99                    cdq
  004c175f  b964000000            mov       ecx,64h
  004c1764  f7f9                  idiv      ecx
  004c1766  83fa32                cmp       edx,32h
  004c1769  7d64                  jge       short 004C17CFh
  004c176b  8b542418              mov       edx,[esp+18h]
  004c176f  8b7c241c              mov       edi,[esp+1Ch]
  004c1773  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004c177a  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004c1781  25ff0f0000            and       eax,0FFFh
  004c1786  3bc1                  cmp       eax,ecx
  004c1788  7f3b                  jg        short 004C17C5h
  004c178a  e8b1590800            call      00547140h
  004c178f  99                    cdq
  004c1790  8d4c3f01              lea       ecx,[edi+edi+1]
  004c1794  f7f9                  idiv      ecx
  004c1796  8b442414              mov       eax,[esp+14h]
  004c179a  83c264                add       edx,64h
  004c179d  3bc2                  cmp       eax,edx
  004c179f  7d24                  jge       short 004C17C5h
  004c17a1  8b16                  mov       edx,[esi]
  004c17a3  c646420b              mov       byte [esi+42h],0Bh
  004c17a7  81e2fffffffe          and       edx,0FEFFFFFFh
  004c17ad  c6464301              mov       byte [esi+43h],1
  004c17b1  83ca02                or        edx,2
  004c17b4  895e5c                mov       [esi+5Ch],ebx
  004c17b7  8916                  mov       [esi],edx
  004c17b9  66c746180300          mov       word [esi+18h],3
  004c17bf  66895e1a              mov       [esi+1Ah],bx
  004c17c3  eb67                  jmp       short 004C182Ch

LAB_004c17c5:
  004c17c5  c6464201              mov       byte [esi+42h],1
  004c17c9  c6464301              mov       byte [esi+43h],1
  004c17cd  eb5d                  jmp       short 004C182Ch

LAB_004c17cf:
  004c17cf  83fa3c                cmp       edx,3Ch
  004c17d2  7c2b                  jl        short 004C17FFh
  004c17d4  83fa46                cmp       edx,46h
  004c17d7  7d26                  jge       short 004C17FFh
  004c17d9  8b4c2410              mov       ecx,[esp+10h]
  004c17dd  8b442414              mov       eax,[esp+14h]
  004c17e1  83c164                add       ecx,64h
  004c17e4  3bc1                  cmp       eax,ecx
  004c17e6  7e17                  jle       short 004C17FFh
  004c17e8  c6464206              mov       byte [esi+42h],6
  004c17ec  c6464301              mov       byte [esi+43h],1
  004c17f0  e84b590800            call      00547140h
  004c17f5  66894610              mov       [esi+10h],ax
  004c17f9  66895e12              mov       [esi+12h],bx
  004c17fd  eb29                  jmp       short 004C1828h

LAB_004c17ff:
  004c17ff  c6464202              mov       byte [esi+42h],2
  004c1803  e838590800            call      00547140h
  004c1808  99                    cdq
  004c1809  b90a000000            mov       ecx,0Ah
  004c180e  8b7c241c              mov       edi,[esp+1Ch]
  004c1812  f7f9                  idiv      ecx
  004c1814  8bc7                  mov       eax,edi
  004c1816  66c746105000          mov       word [esi+10h],50h
  004c181c  d1f8                  sar       eax,1
  004c181e  02d0                  add       dl,al
  004c1820  80c214                add       dl,14h
  004c1823  885643                mov       [esi+43h],dl
  004c1826  eb04                  jmp       short 004C182Ch

LAB_004c1828:
  004c1828  8b7c241c              mov       edi,[esp+1Ch]

LAB_004c182c:
  004c182c  8b442414              mov       eax,[esp+14h]
  004c1830  3dbc020000            cmp       eax,2BCh
  004c1835  0f8d48010000          jge       near 004C1983h
  004c183b  3d96000000            cmp       eax,96h
  004c1840  0f8c3d010000          jl        near 004C1983h
  004c1846  e8f5580800            call      00547140h
  004c184b  99                    cdq
  004c184c  b964000000            mov       ecx,64h
  004c1851  f7f9                  idiv      ecx
  004c1853  83fa14                cmp       edx,14h
  004c1856  7d43                  jge       short 004C189Bh
  004c1858  8b542418              mov       edx,[esp+18h]
  004c185c  81c200040000          add       edx,400h
  004c1862  81e2ff0f0000          and       edx,0FFFh
  004c1868  81fa00080000          cmp       edx,800h
  004c186e  7d30                  jge       short 004C18A0h
  004c1870  885e42                mov       [esi+42h],bl
  004c1873  e8c8580800            call      00547140h
  004c1878  99                    cdq
  004c1879  b90a000000            mov       ecx,0Ah
  004c187e  f7f9                  idiv      ecx
  004c1880  8b4c2410              mov       ecx,[esp+10h]
  004c1884  8bc7                  mov       eax,edi
  004c1886  c1f802                sar       eax,2
  004c1889  02d0                  add       dl,al
  004c188b  885643                mov       [esi+43h],dl
  004c188e  8d540950              lea       edx,[ecx+ecx+50h]
  004c1892  66895610              mov       [esi+10h],dx
  004c1896  e9e4000000            jmp       004C197Fh

LAB_004c189b:
  004c189b  83fa28                cmp       edx,28h
  004c189e  7d28                  jge       short 004C18C8h

LAB_004c18a0:
  004c18a0  c6464202              mov       byte [esi+42h],2
  004c18a4  e897580800            call      00547140h
  004c18a9  99                    cdq
  004c18aa  b90a000000            mov       ecx,0Ah
  004c18af  66c746105000          mov       word [esi+10h],50h
  004c18b5  f7f9                  idiv      ecx
  004c18b7  8bc7                  mov       eax,edi
  004c18b9  d1f8                  sar       eax,1
  004c18bb  02d0                  add       dl,al
  004c18bd  80c214                add       dl,14h
  004c18c0  885643                mov       [esi+43h],dl
  004c18c3  e9bb000000            jmp       004C1983h

LAB_004c18c8:
  004c18c8  83fa3c                cmp       edx,3Ch
  004c18cb  7d16                  jge       short 004C18E3h
  004c18cd  c6464206              mov       byte [esi+42h],6
  004c18d1  c6464301              mov       byte [esi+43h],1
  004c18d5  e866580800            call      00547140h
  004c18da  66894610              mov       [esi+10h],ax
  004c18de  e99c000000            jmp       004C197Fh

LAB_004c18e3:
  004c18e3  83fa46                cmp       edx,46h
  004c18e6  7d3a                  jge       short 004C1922h
  004c18e8  c6464203              mov       byte [esi+42h],3
  004c18ec  66895e12              mov       [esi+12h],bx
  004c18f0  e84b580800            call      00547140h
  004c18f5  99                    cdq
  004c18f6  b90a000000            mov       ecx,0Ah
  004c18fb  f7f9                  idiv      ecx
  004c18fd  8bc7                  mov       eax,edi
  004c18ff  d1f8                  sar       eax,1
  004c1901  02d0                  add       dl,al
  004c1903  80c214                add       dl,14h
  004c1906  885643                mov       [esi+43h],dl
  004c1909  e832580800            call      00547140h
  004c190e  99                    cdq
  004c190f  33c2                  xor       eax,edx
  004c1911  2bc2                  sub       eax,edx
  004c1913  25ff0f0000            and       eax,0FFFh
  004c1918  33c2                  xor       eax,edx
  004c191a  2bc2                  sub       eax,edx
  004c191c  66894610              mov       [esi+10h],ax
  004c1920  eb61                  jmp       short 004C1983h

LAB_004c1922:
  004c1922  668b4616              mov       ax,[esi+16h]
  004c1926  663bc3                cmp       ax,bx
  004c1929  750c                  jne       short 004C1937h
  004c192b  c646420d              mov       byte [esi+42h],0Dh
  004c192f  c6464301              mov       byte [esi+43h],1
  004c1933  66895e12              mov       [esi+12h],bx

LAB_004c1937:
  004c1937  663d0100              cmp       ax,1
  004c193b  b10c                  mov       cl,0Ch
  004c193d  7516                  jne       short 004C1955h
  004c193f  83fa55                cmp       edx,55h
  004c1942  7d05                  jge       short 004C1949h
  004c1944  884e42                mov       [esi+42h],cl
  004c1947  eb04                  jmp       short 004C194Dh

LAB_004c1949:
  004c1949  c646420d              mov       byte [esi+42h],0Dh

LAB_004c194d:
  004c194d  c6464301              mov       byte [esi+43h],1
  004c1951  66895e12              mov       [esi+12h],bx

LAB_004c1955:
  004c1955  663d0200              cmp       ax,2
  004c1959  750c                  jne       short 004C1967h
  004c195b  c646420a              mov       byte [esi+42h],0Ah
  004c195f  c6464301              mov       byte [esi+43h],1
  004c1963  66895e12              mov       [esi+12h],bx

LAB_004c1967:
  004c1967  663d0300              cmp       ax,3
  004c196b  7516                  jne       short 004C1983h
  004c196d  83fa55                cmp       edx,55h
  004c1970  7d06                  jge       short 004C1978h
  004c1972  c646420a              mov       byte [esi+42h],0Ah
  004c1976  eb03                  jmp       short 004C197Bh

LAB_004c1978:
  004c1978  884e42                mov       [esi+42h],cl

LAB_004c197b:
  004c197b  c6464301              mov       byte [esi+43h],1

LAB_004c197f:
  004c197f  66895e12              mov       [esi+12h],bx

LAB_004c1983:
  004c1983  817c2414bc020000      cmp       dword [esp+14h],2BCh
  004c198b  0f8c3e010000          jl        near 004C1ACFh
  004c1991  e8aa570800            call      00547140h
  004c1996  bd64000000            mov       ebp,64h
  004c199b  99                    cdq
  004c199c  8bcd                  mov       ecx,ebp
  004c199e  f7f9                  idiv      ecx
  004c19a0  83fa1e                cmp       edx,1Eh
  004c19a3  7d55                  jge       short 004C19FAh
  004c19a5  8b442410              mov       eax,[esp+10h]
  004c19a9  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004c19ad  3bd1                  cmp       edx,ecx
  004c19af  7d14                  jge       short 004C19C5h
  004c19b1  c6464202              mov       byte [esi+42h],2
  004c19b5  e886570800            call      00547140h
  004c19ba  99                    cdq
  004c19bb  b90a000000            mov       ecx,0Ah
  004c19c0  e9dc000000            jmp       004C1AA1h

LAB_004c19c5:
  004c19c5  885e42                mov       [esi+42h],bl
  004c19c8  e873570800            call      00547140h
  004c19cd  99                    cdq
  004c19ce  b90a000000            mov       ecx,0Ah
  004c19d3  66895e12              mov       [esi+12h],bx
  004c19d7  f7f9                  idiv      ecx
  004c19d9  8b4c2410              mov       ecx,[esp+10h]
  004c19dd  8bc7                  mov       eax,edi
  004c19df  c1f802                sar       eax,2
  004c19e2  02d0                  add       dl,al
  004c19e4  885643                mov       [esi+43h],dl
  004c19e7  8d540950              lea       edx,[ecx+ecx+50h]
  004c19eb  66895610              mov       [esi+10h],dx
  004c19ef  5f                    pop       edi
  004c19f0  5e                    pop       esi
  004c19f1  5d                    pop       ebp
  004c19f2  5b                    pop       ebx
  004c19f3  81c484000000          add       esp,84h
  004c19f9  c3                    ret

LAB_004c19fa:
  004c19fa  83fa28                cmp       edx,28h
  004c19fd  7d43                  jge       short 004C1A42h
  004c19ff  c6464203              mov       byte [esi+42h],3
  004c1a03  66895e12              mov       [esi+12h],bx
  004c1a07  e834570800            call      00547140h
  004c1a0c  99                    cdq
  004c1a0d  b90a000000            mov       ecx,0Ah
  004c1a12  f7f9                  idiv      ecx
  004c1a14  8bc7                  mov       eax,edi
  004c1a16  d1f8                  sar       eax,1
  004c1a18  02d0                  add       dl,al
  004c1a1a  80c214                add       dl,14h
  004c1a1d  885643                mov       [esi+43h],dl
  004c1a20  e81b570800            call      00547140h
  004c1a25  99                    cdq
  004c1a26  33c2                  xor       eax,edx
  004c1a28  2bc2                  sub       eax,edx
  004c1a2a  25ff0f0000            and       eax,0FFFh
  004c1a2f  33c2                  xor       eax,edx
  004c1a31  2bc2                  sub       eax,edx
  004c1a33  66894610              mov       [esi+10h],ax
  004c1a37  5f                    pop       edi
  004c1a38  5e                    pop       esi
  004c1a39  5d                    pop       ebp
  004c1a3a  5b                    pop       ebx
  004c1a3b  81c484000000          add       esp,84h
  004c1a41  c3                    ret

LAB_004c1a42:
  004c1a42  83fa50                cmp       edx,50h
  004c1a45  7d13                  jge       short 004C1A5Ah
  004c1a47  c6464201              mov       byte [esi+42h],1
  004c1a4b  c6464301              mov       byte [esi+43h],1
  004c1a4f  5f                    pop       edi
  004c1a50  5e                    pop       esi
  004c1a51  5d                    pop       ebp
  004c1a52  5b                    pop       ebx
  004c1a53  81c484000000          add       esp,84h
  004c1a59  c3                    ret

LAB_004c1a5a:
  004c1a5a  83fa5a                cmp       edx,5Ah
  004c1a5d  7d5f                  jge       short 004C1ABEh
  004c1a5f  8b4c2418              mov       ecx,[esp+18h]
  004c1a63  8d04fd00020000        lea       eax,[edi*8+200h]
  004c1a6a  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004c1a71  81e2ff0f0000          and       edx,0FFFh
  004c1a77  3bd0                  cmp       edx,eax
  004c1a79  7f17                  jg        short 004C1A92h
  004c1a7b  c646420a              mov       byte [esi+42h],0Ah
  004c1a7f  c6464301              mov       byte [esi+43h],1
  004c1a83  66895e12              mov       [esi+12h],bx
  004c1a87  5f                    pop       edi
  004c1a88  5e                    pop       esi
  004c1a89  5d                    pop       ebp
  004c1a8a  5b                    pop       ebx
  004c1a8b  81c484000000          add       esp,84h
  004c1a91  c3                    ret

LAB_004c1a92:
  004c1a92  c6464202              mov       byte [esi+42h],2
  004c1a96  e8a5560800            call      00547140h
  004c1a9b  99                    cdq
  004c1a9c  b914000000            mov       ecx,14h

LAB_004c1aa1:
  004c1aa1  f7f9                  idiv      ecx
  004c1aa3  8bc7                  mov       eax,edi
  004c1aa5  66896e10              mov       [esi+10h],bp
  004c1aa9  d1f8                  sar       eax,1
  004c1aab  02d0                  add       dl,al
  004c1aad  80c214                add       dl,14h
  004c1ab0  885643                mov       [esi+43h],dl
  004c1ab3  5f                    pop       edi
  004c1ab4  5e                    pop       esi
  004c1ab5  5d                    pop       ebp
  004c1ab6  5b                    pop       ebx
  004c1ab7  81c484000000          add       esp,84h
  004c1abd  c3                    ret

LAB_004c1abe:
  004c1abe  c6464206              mov       byte [esi+42h],6
  004c1ac2  c6464301              mov       byte [esi+43h],1
  004c1ac6  e875560800            call      00547140h
  004c1acb  66894610              mov       [esi+10h],ax

LAB_004c1acf:
  004c1acf  5f                    pop       edi
  004c1ad0  5e                    pop       esi
  004c1ad1  5d                    pop       ebp
  004c1ad2  5b                    pop       ebx
  004c1ad3  81c484000000          add       esp,84h
  004c1ad9  c3                    ret

LAB_004c1ada:
  004c1ada  83e1fd                and       ecx,0FFFFFFFDh
  004c1add  890e                  mov       [esi],ecx
  004c1adf  e961f3ffff            jmp       004C0E45h

LAB_004c1ae4:
  004c1ae4  f6c101                test      cl,1
  004c1ae7  0f8458f3ffff          je        near 004C0E45h
  004c1aed  bd03000000            mov       ebp,3
  004c1af2  66396e1a              cmp       [esi+1Ah],bp
  004c1af6  0f8d49f3ffff          jge       near 004C0E45h
  004c1afc  e83f560800            call      00547140h
  004c1b01  99                    cdq
  004c1b02  b928000000            mov       ecx,28h
  004c1b07  8b7c2410              mov       edi,[esp+10h]
  004c1b0b  f7f9                  idiv      ecx
  004c1b0d  3bd7                  cmp       edx,edi
  004c1b0f  7d1e                  jge       short 004C1B2Fh
  004c1b11  8b16                  mov       edx,[esi]
  004c1b13  8b442420              mov       eax,[esp+20h]
  004c1b17  81e2fffffffe          and       edx,0FEFFFFFFh
  004c1b1d  66896e18              mov       [esi+18h],bp
  004c1b21  83ca02                or        edx,2
  004c1b24  66895e1a              mov       [esi+1Ah],bx
  004c1b28  8916                  mov       [esi],edx
  004c1b2a  e91af3ffff            jmp       004C0E49h

LAB_004c1b2f:
  004c1b2f  8b442420              mov       eax,[esp+20h]
  004c1b33  e911f3ffff            jmp       004C0E49h
  004c1b38  5e                    pop       esi
  004c1b39  0e                    push      cs
  004c1b3a  4c                    dec       esp
  004c1b3b  00a80e4c0012          add       [eax+12004C0Eh],ch
  004c1b41  0f4c00                cmovl     eax,[eax]
  004c1b44  a80f                  test      al,0Fh
  004c1b46  4c                    dec       esp
  004c1b47  00c1                  add       cl,al
  004c1b49  144c                  adc       al,4Ch
  004c1b4b  00c1                  add       cl,al
  004c1b4d  144c                  adc       al,4Ch
  004c1b4f  005c104c              add       [eax+edx+4Ch],bl
  004c1b53  00ee                  add       dh,ch
  004c1b55  104c00c1              adc       [eax+eax-3Fh],cl
  004c1b59  144c                  adc       al,4Ch
  004c1b5b  00c1                  add       cl,al
  004c1b5d  144c                  adc       al,4Ch
  004c1b5f  004611                add       [esi+11h],al
  004c1b62  4c                    dec       esp
  004c1b63  0032                  add       [edx],dh
  004c1b65  124c0069              adc       cl,[eax+eax+69h]
  004c1b69  124c0082              adc       cl,[eax+eax-7Eh]
  004c1b6d  134c00                (bad)
  (octet invalide - la plage demandee deborde probablement du code)
