Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00499de0  section .text  offset fichier 0x991e0  1377 octets

  00499de0  83ec2c                sub       esp,2Ch
  00499de3  53                    push      ebx
  00499de4  55                    push      ebp
  00499de5  56                    push      esi
  00499de6  8b74243c              mov       esi,[esp+3Ch]
  00499dea  8d442424              lea       eax,[esp+24h]
  00499dee  57                    push      edi
  00499def  8d4c242c              lea       ecx,[esp+2Ch]
  00499df3  50                    push      eax
  00499df4  8d542428              lea       edx,[esp+28h]
  00499df8  51                    push      ecx
  00499df9  33ed                  xor       ebp,ebp
  00499dfb  52                    push      edx
  00499dfc  56                    push      esi
  00499dfd  66896c2440            mov       [esp+40h],bp
  00499e02  66c74424421e00        mov       word [esp+42h],1Eh
  00499e09  66c74424443200        mov       word [esp+44h],32h
  00499e10  66c74424465000        mov       word [esp+46h],50h
  00499e17  66896c2448            mov       [esp+48h],bp
  00499e1c  66c744244a3c00        mov       word [esp+4Ah],3Ch
  00499e23  e858eb0900            call      00538980h
  00499e28  83c410                add       esp,10h
  00499e2b  8d442420              lea       eax,[esp+20h]
  00499e2f  8d4c241c              lea       ecx,[esp+1Ch]
  00499e33  8d542414              lea       edx,[esp+14h]
  00499e37  50                    push      eax
  00499e38  51                    push      ecx
  00499e39  8d442420              lea       eax,[esp+20h]
  00499e3d  52                    push      edx
  00499e3e  8d4c241c              lea       ecx,[esp+1Ch]
  00499e42  50                    push      eax
  00499e43  51                    push      ecx
  00499e44  56                    push      esi
  00499e45  e8d6c00900            call      00535F20h
  00499e4a  8b4c2428              mov       ecx,[esp+28h]
  00499e4e  b840000000            mov       eax,40h
  00499e53  2bc1                  sub       eax,ecx
  00499e55  83c418                add       esp,18h
  00499e58  89442440              mov       [esp+40h],eax
  00499e5c  8b44241c              mov       eax,[esp+1Ch]
  00499e60  3bc5                  cmp       eax,ebp
  00499e62  0f8464020000          je        near 0049A0CCh
  00499e68  56                    push      esi
  00499e69  e832170400            call      004DB5A0h
  00499e6e  8b06                  mov       eax,[esi]
  00499e70  83c404                add       esp,4
  00499e73  a802                  test      al,2
  00499e75  0f8498000000          je        near 00499F13h
  00499e7b  8a4c2420              mov       cl,[esp+20h]
  00499e7f  bb10000000            mov       ebx,10h
  00499e84  84cb                  test      bl,cl
  00499e86  740d                  je        short 00499E95h
  00499e88  24fd                  and       al,0FDh
  00499e8a  c6464300              mov       byte [esi+43h],0
  00499e8e  8906                  mov       [esi],eax
  00499e90  e937020000            jmp       0049A0CCh

LAB_00499e95:
  00499e95  66837e1a0c            cmp       word [esi+1Ah],0Ch
  00499e9a  7558                  jne       short 00499EF4h
  00499e9c  8d542430              lea       edx,[esp+30h]
  00499ea0  55                    push      ebp
  00499ea1  8d461c                lea       eax,[esi+1Ch]
  00499ea4  52                    push      edx
  00499ea5  50                    push      eax
  00499ea6  56                    push      esi
  00499ea7  e884fd0700            call      00519C30h
  00499eac  8bf8                  mov       edi,eax
  00499eae  a1b8e05d00            mov       eax,[5DE0B8h]
  00499eb3  83c410                add       esp,10h
  00499eb6  3bc5                  cmp       eax,ebp
  00499eb8  7430                  je        short 00499EEAh
  00499eba  66899f96000000        mov       [edi+96h],bx
  00499ec1  8b1f                  mov       ebx,[edi]
  00499ec3  83cb40                or        ebx,40h
  00499ec6  57                    push      edi
  00499ec7  56                    push      esi
  00499ec8  891f                  mov       [edi],ebx
  00499eca  e8b1fc0900            call      00539B80h
  00499ecf  83c408                add       esp,8
  00499ed2  83c71c                add       edi,1Ch
  00499ed5  57                    push      edi
  00499ed6  6800180000            push      1800h
  00499edb  6800030000            push      300h
  00499ee0  6a03                  push      3
  00499ee2  e8393c0a00            call      0053DB20h
  00499ee7  83c410                add       esp,10h

LAB_00499eea:
  00499eea  8b06                  mov       eax,[esi]
  00499eec  a801                  test      al,1
  00499eee  740c                  je        short 00499EFCh
  00499ef0  66ff4e1a              dec       word [esi+1Ah]

LAB_00499ef4:
  00499ef4  a801                  test      al,1
  00499ef6  0f85d0010000          jne       near 0049A0CCh

LAB_00499efc:
  00499efc  a900000002            test      eax,2000000h
  00499f01  0f84bf000000          je        near 00499FC6h
  00499f07  24fd                  and       al,0FDh
  00499f09  896e5c                mov       [esi+5Ch],ebp
  00499f0c  8906                  mov       [esi],eax
  00499f0e  e9b9010000            jmp       0049A0CCh

LAB_00499f13:
  00499f13  a811                  test      al,11h
  00499f15  0f85b1010000          jne       near 0049A0CCh
  00499f1b  a904080000            test      eax,804h
  00499f20  0f85a6010000          jne       near 0049A0CCh
  00499f26  33c9                  xor       ecx,ecx
  00499f28  8a4e42                mov       cl,[esi+42h]
  00499f2b  83f903                cmp       ecx,3
  00499f2e  0f8798010000          ja        near 0049A0CCh
  00499f34  ff248d44a34900        jmp       dword [ecx*4+49A344h]
  00499f3b  66396e18              cmp       [esi+18h],bp
  00499f3f  740a                  je        short 00499F4Bh
  00499f41  0d00000001            or        eax,1000000h
  00499f46  896e18                mov       [esi+18h],ebp
  00499f49  8906                  mov       [esi],eax

LAB_00499f4b:
  00499f4b  817c24182c010000      cmp       dword [esp+18h],12Ch
  00499f53  7d09                  jge       short 00499F5Eh
  00499f55  c6464300              mov       byte [esi+43h],0
  00499f59  e96e010000            jmp       0049A0CCh

LAB_00499f5e:
  00499f5e  8a4643                mov       al,[esi+43h]
  00499f61  84c0                  test      al,al
  00499f63  7461                  je        short 00499FC6h
  00499f65  fec8                  dec       al
  00499f67  896e5c                mov       [esi+5Ch],ebp
  00499f6a  884643                mov       [esi+43h],al
  00499f6d  e95a010000            jmp       0049A0CCh
  00499f72  b901000000            mov       ecx,1
  00499f77  66394e18              cmp       [esi+18h],cx
  00499f7b  740a                  je        short 00499F87h
  00499f7d  0d00000001            or        eax,1000000h
  00499f82  894e18                mov       [esi+18h],ecx
  00499f85  8906                  mov       [esi],eax

LAB_00499f87:
  00499f87  8b4c2410              mov       ecx,[esp+10h]
  00499f8b  8d4120                lea       eax,[ecx+20h]
  00499f8e  8b4c2414              mov       ecx,[esp+14h]
  00499f92  8d1408                lea       edx,[eax+ecx]
  00499f95  8d3c00                lea       edi,[eax+eax]
  00499f98  81e2ff0f0000          and       edx,0FFFh
  00499f9e  3bd7                  cmp       edx,edi
  00499fa0  7f10                  jg        short 00499FB2h
  00499fa2  66014e32              add       [esi+32h],cx
  00499fa6  c6464300              mov       byte [esi+43h],0
  00499faa  896e5c                mov       [esi+5Ch],ebp
  00499fad  e91a010000            jmp       0049A0CCh

LAB_00499fb2:
  00499fb2  81e1ff0f0000          and       ecx,0FFFh
  00499fb8  81f900080000          cmp       ecx,800h
  00499fbe  7c02                  jl        short 00499FC2h
  00499fc0  f7d8                  neg       eax

LAB_00499fc2:
  00499fc2  66014632              add       [esi+32h],ax

LAB_00499fc6:
  00499fc6  896e5c                mov       [esi+5Ch],ebp
  00499fc9  e9fe000000            jmp       0049A0CCh
  00499fce  b901000000            mov       ecx,1
  00499fd3  66394e18              cmp       [esi+18h],cx
  00499fd7  740a                  je        short 00499FE3h
  00499fd9  0d00000001            or        eax,1000000h
  00499fde  894e18                mov       [esi+18h],ecx
  00499fe1  8906                  mov       [esi],eax

LAB_00499fe3:
  00499fe3  0fbf4610              movsx     eax,word [esi+10h]
  00499fe7  39442418              cmp       [esp+18h],eax
  00499feb  7d09                  jge       short 00499FF6h
  00499fed  c6464300              mov       byte [esi+43h],0
  00499ff1  e9d6000000            jmp       0049A0CCh

LAB_00499ff6:
  00499ff6  8a4643                mov       al,[esi+43h]
  00499ff9  84c0                  test      al,al
  00499ffb  7405                  je        short 0049A002h
  00499ffd  fec8                  dec       al
  00499fff  884643                mov       [esi+43h],al

LAB_0049a002:
  0049a002  8b542410              mov       edx,[esp+10h]
  0049a006  8b4c2414              mov       ecx,[esp+14h]
  0049a00a  8d4240                lea       eax,[edx+40h]
  0049a00d  8d3c08                lea       edi,[eax+ecx]
  0049a010  8d1c00                lea       ebx,[eax+eax]
  0049a013  81e7ff0f0000          and       edi,0FFFh
  0049a019  3bfb                  cmp       edi,ebx
  0049a01b  7f15                  jg        short 0049A032h
  0049a01d  66014e32              add       [esi+32h],cx
  0049a021  81c2a0000000          add       edx,0A0h
  0049a027  c1e207                shl       edx,7
  0049a02a  89565c                mov       [esi+5Ch],edx
  0049a02d  e99a000000            jmp       0049A0CCh

LAB_0049a032:
  0049a032  81e1ff0f0000          and       ecx,0FFFh
  0049a038  81f900080000          cmp       ecx,800h
  0049a03e  7c02                  jl        short 0049A042h
  0049a040  f7d8                  neg       eax

LAB_0049a042:
  0049a042  66014632              add       [esi+32h],ax
  0049a046  81c2a0000000          add       edx,0A0h
  0049a04c  c1e207                shl       edx,7
  0049a04f  89565c                mov       [esi+5Ch],edx
  0049a052  eb78                  jmp       short 0049A0CCh
  0049a054  8a4e43                mov       cl,[esi+43h]
  0049a057  84c9                  test      cl,cl
  0049a059  7405                  je        short 0049A060h
  0049a05b  fec9                  dec       cl
  0049a05d  884e43                mov       [esi+43h],cl

LAB_0049a060:
  0049a060  b901000000            mov       ecx,1
  0049a065  66394e18              cmp       [esi+18h],cx
  0049a069  740a                  je        short 0049A075h
  0049a06b  0d00000001            or        eax,1000000h
  0049a070  894e18                mov       [esi+18h],ecx
  0049a073  8906                  mov       [esi],eax

LAB_0049a075:
  0049a075  8b4c2410              mov       ecx,[esp+10h]
  0049a079  668b4610              mov       ax,[esi+10h]
  0049a07d  8d91a0000000          lea       edx,[ecx+0A0h]
  0049a083  c1e207                shl       edx,7
  0049a086  89565c                mov       [esi+5Ch],edx
  0049a089  668b5632              mov       dx,[esi+32h]
  0049a08d  2bc2                  sub       eax,edx
  0049a08f  25ff0f0000            and       eax,0FFFh
  0049a094  8db800010000          lea       edi,[eax+100h]
  0049a09a  81e7ff0f0000          and       edi,0FFFh
  0049a0a0  81ff00020000          cmp       edi,200h
  0049a0a6  7e24                  jle       short 0049A0CCh
  0049a0a8  25ff0f0000            and       eax,0FFFh
  0049a0ad  3d00080000            cmp       eax,800h
  0049a0b2  7d09                  jge       short 0049A0BDh
  0049a0b4  d1f9                  sar       ecx,1
  0049a0b6  83c120                add       ecx,20h
  0049a0b9  8bc1                  mov       eax,ecx
  0049a0bb  eb09                  jmp       short 0049A0C6h

LAB_0049a0bd:
  0049a0bd  d1f9                  sar       ecx,1
  0049a0bf  b8e0ffffff            mov       eax,0FFFFFFE0h
  0049a0c4  2bc1                  sub       eax,ecx

LAB_0049a0c6:
  0049a0c6  03d0                  add       edx,eax
  0049a0c8  66895632              mov       [esi+32h],dx

LAB_0049a0cc:
  0049a0cc  8b44241c              mov       eax,[esp+1Ch]
  0049a0d0  50                    push      eax
  0049a0d1  56                    push      esi
  0049a0d2  e889e0feff            call      00488160h
  0049a0d7  83c408                add       esp,8
  0049a0da  56                    push      esi
  0049a0db  e860980900            call      00533940h
  0049a0e0  83c404                add       esp,4
  0049a0e3  56                    push      esi
  0049a0e4  e8075a0400            call      004DFAF0h
  0049a0e9  8b4c2428              mov       ecx,[esp+28h]
  0049a0ed  83c404                add       esp,4
  0049a0f0  51                    push      ecx
  0049a0f1  56                    push      esi
  0049a0f2  e879960400            call      004E3770h
  0049a0f7  8b442424              mov       eax,[esp+24h]
  0049a0fb  83c408                add       esp,8
  0049a0fe  3bc5                  cmp       eax,ebp
  0049a100  0f8433020000          je        near 0049A339h
  0049a106  f70617080000          test      dword [esi],817h
  0049a10c  0f8527020000          jne       near 0049A339h
  0049a112  8a4643                mov       al,[esi+43h]
  0049a115  84c0                  test      al,al
  0049a117  0f851c020000          jne       near 0049A339h
  0049a11d  8b466c                mov       eax,[esi+6Ch]
  0049a120  8b5e1c                mov       ebx,[esi+1Ch]
  0049a123  8b4e24                mov       ecx,[esi+24h]
  0049a126  8b781c                mov       edi,[eax+1Ch]
  0049a129  8b6824                mov       ebp,[eax+24h]
  0049a12c  2bfb                  sub       edi,ebx
  0049a12e  2be9                  sub       ebp,ecx
  0049a130  c1ff0c                sar       edi,0Ch
  0049a133  c1fd0c                sar       ebp,0Ch
  0049a136  8bd7                  mov       edx,edi
  0049a138  8bc5                  mov       eax,ebp
  0049a13a  0fafd7                imul      edx,edi
  0049a13d  0fafc5                imul      eax,ebp
  0049a140  03d0                  add       edx,eax
  0049a142  52                    push      edx
  0049a143  e8c8b80a00            call      00545A10h
  0049a148  55                    push      ebp
  0049a149  57                    push      edi
  0049a14a  89442420              mov       [esp+20h],eax
  0049a14e  e8cfb80a00            call      00545A22h
  0049a153  668b4e32              mov       cx,[esi+32h]
  0049a157  2bc1                  sub       eax,ecx
  0049a159  25ff0f0000            and       eax,0FFFh
  0049a15e  89442414              mov       [esp+14h],eax
  0049a162  8b442418              mov       eax,[esp+18h]
  0049a166  3dfa000000            cmp       eax,0FAh
  0049a16b  0f8db5000000          jge       near 0049A226h
  0049a171  e8cacf0a00            call      00547140h
  0049a176  99                    cdq
  0049a177  b964000000            mov       ecx,64h
  0049a17c  f7f9                  idiv      ecx
  0049a17e  83fa28                cmp       edx,28h
  0049a181  7d71                  jge       short 0049A1F4h
  0049a183  8b542414              mov       edx,[esp+14h]
  0049a187  8b7c2440              mov       edi,[esp+40h]
  0049a18b  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0049a192  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049a199  25ff0f0000            and       eax,0FFFh
  0049a19e  3bc1                  cmp       eax,ecx
  0049a1a0  7f3a                  jg        short 0049A1DCh
  0049a1a2  e899cf0a00            call      00547140h
  0049a1a7  99                    cdq
  0049a1a8  8d4c3f01              lea       ecx,[edi+edi+1]
  0049a1ac  f7f9                  idiv      ecx
  0049a1ae  8b442418              mov       eax,[esp+18h]
  0049a1b2  83c264                add       edx,64h
  0049a1b5  3bc2                  cmp       eax,edx
  0049a1b7  7d23                  jge       short 0049A1DCh
  0049a1b9  8b16                  mov       edx,[esi]
  0049a1bb  c7461803000000        mov       dword [esi+18h],3
  0049a1c2  81e2fffffffe          and       edx,0FEFFFFFFh
  0049a1c8  c7465c00000000        mov       dword [esi+5Ch],0
  0049a1cf  83ca02                or        edx,2
  0049a1d2  8916                  mov       [esi],edx
  0049a1d4  5f                    pop       edi
  0049a1d5  5e                    pop       esi
  0049a1d6  5d                    pop       ebp
  0049a1d7  5b                    pop       ebx
  0049a1d8  83c42c                add       esp,2Ch
  0049a1db  c3                    ret

LAB_0049a1dc:
  0049a1dc  c6464203              mov       byte [esi+42h],3
  0049a1e0  e85bcf0a00            call      00547140h
  0049a1e5  99                    cdq
  0049a1e6  b90a000000            mov       ecx,0Ah
  0049a1eb  f7f9                  idiv      ecx
  0049a1ed  8bc7                  mov       eax,edi
  0049a1ef  e996000000            jmp       0049A28Ah

LAB_0049a1f4:
  0049a1f4  83fa50                cmp       edx,50h
  0049a1f7  7d7c                  jge       short 0049A275h
  0049a1f9  c6464202              mov       byte [esi+42h],2
  0049a1fd  e83ecf0a00            call      00547140h
  0049a202  99                    cdq
  0049a203  b90a000000            mov       ecx,0Ah
  0049a208  66c746105a00          mov       word [esi+10h],5Ah
  0049a20e  f7f9                  idiv      ecx
  0049a210  8b442440              mov       eax,[esp+40h]
  0049a214  d1f8                  sar       eax,1
  0049a216  02d0                  add       dl,al
  0049a218  80c214                add       dl,14h
  0049a21b  885643                mov       [esi+43h],dl
  0049a21e  5f                    pop       edi
  0049a21f  5e                    pop       esi
  0049a220  5d                    pop       ebp
  0049a221  5b                    pop       ebx
  0049a222  83c42c                add       esp,2Ch
  0049a225  c3                    ret

LAB_0049a226:
  0049a226  3df4010000            cmp       eax,1F4h
  0049a22b  0f8d92000000          jge       near 0049A2C3h
  0049a231  e80acf0a00            call      00547140h
  0049a236  99                    cdq
  0049a237  b964000000            mov       ecx,64h
  0049a23c  f7f9                  idiv      ecx
  0049a23e  83fa32                cmp       edx,32h
  0049a241  7d2d                  jge       short 0049A270h
  0049a243  c6464202              mov       byte [esi+42h],2
  0049a247  e8f4ce0a00            call      00547140h
  0049a24c  99                    cdq
  0049a24d  b90a000000            mov       ecx,0Ah
  0049a252  66c746105a00          mov       word [esi+10h],5Ah
  0049a258  f7f9                  idiv      ecx
  0049a25a  8b442440              mov       eax,[esp+40h]
  0049a25e  d1f8                  sar       eax,1
  0049a260  02d0                  add       dl,al
  0049a262  80c214                add       dl,14h
  0049a265  885643                mov       [esi+43h],dl
  0049a268  5f                    pop       edi
  0049a269  5e                    pop       esi
  0049a26a  5d                    pop       ebp
  0049a26b  5b                    pop       ebx
  0049a26c  83c42c                add       esp,2Ch
  0049a26f  c3                    ret

LAB_0049a270:
  0049a270  83fa50                cmp       edx,50h
  0049a273  7d60                  jge       short 0049A2D5h

LAB_0049a275:
  0049a275  c6464203              mov       byte [esi+42h],3
  0049a279  e8c2ce0a00            call      00547140h
  0049a27e  99                    cdq
  0049a27f  b90a000000            mov       ecx,0Ah
  0049a284  f7f9                  idiv      ecx
  0049a286  8b442440              mov       eax,[esp+40h]

LAB_0049a28a:
  0049a28a  8b4c2410              mov       ecx,[esp+10h]
  0049a28e  d1f8                  sar       eax,1
  0049a290  02d0                  add       dl,al
  0049a292  8db980000000          lea       edi,[ecx+80h]
  0049a298  80c214                add       dl,14h
  0049a29b  885643                mov       [esi+43h],dl
  0049a29e  c1e704                shl       edi,4
  0049a2a1  e89ace0a00            call      00547140h
  0049a2a6  99                    cdq
  0049a2a7  f7ff                  idiv      edi
  0049a2a9  8b4c2414              mov       ecx,[esp+14h]
  0049a2ad  d1ff                  sar       edi,1
  0049a2af  2bd7                  sub       edx,edi
  0049a2b1  03d1                  add       edx,ecx
  0049a2b3  66035632              add       dx,[esi+32h]
  0049a2b7  66895610              mov       [esi+10h],dx
  0049a2bb  5f                    pop       edi
  0049a2bc  5e                    pop       esi
  0049a2bd  5d                    pop       ebp
  0049a2be  5b                    pop       ebx
  0049a2bf  83c42c                add       esp,2Ch
  0049a2c2  c3                    ret

LAB_0049a2c3:
  0049a2c3  e878ce0a00            call      00547140h
  0049a2c8  99                    cdq
  0049a2c9  b964000000            mov       ecx,64h
  0049a2ce  f7f9                  idiv      ecx
  0049a2d0  83fa1e                cmp       edx,1Eh
  0049a2d3  7d27                  jge       short 0049A2FCh

LAB_0049a2d5:
  0049a2d5  c6464200              mov       byte [esi+42h],0
  0049a2d9  e862ce0a00            call      00547140h
  0049a2de  99                    cdq
  0049a2df  b90a000000            mov       ecx,0Ah
  0049a2e4  f7f9                  idiv      ecx
  0049a2e6  8b442440              mov       eax,[esp+40h]
  0049a2ea  d1f8                  sar       eax,1
  0049a2ec  02d0                  add       dl,al
  0049a2ee  80c214                add       dl,14h
  0049a2f1  885643                mov       [esi+43h],dl
  0049a2f4  5f                    pop       edi
  0049a2f5  5e                    pop       esi
  0049a2f6  5d                    pop       ebp
  0049a2f7  5b                    pop       ebx
  0049a2f8  83c42c                add       esp,2Ch
  0049a2fb  c3                    ret

LAB_0049a2fc:
  0049a2fc  83fa3c                cmp       edx,3Ch
  0049a2ff  7d10                  jge       short 0049A311h
  0049a301  c6464201              mov       byte [esi+42h],1
  0049a305  c6464301              mov       byte [esi+43h],1
  0049a309  5f                    pop       edi
  0049a30a  5e                    pop       esi
  0049a30b  5d                    pop       ebp
  0049a30c  5b                    pop       ebx
  0049a30d  83c42c                add       esp,2Ch
  0049a310  c3                    ret

LAB_0049a311:
  0049a311  c6464203              mov       byte [esi+42h],3
  0049a315  e826ce0a00            call      00547140h
  0049a31a  99                    cdq
  0049a31b  b90a000000            mov       ecx,0Ah
  0049a320  f7f9                  idiv      ecx
  0049a322  8b442440              mov       eax,[esp+40h]
  0049a326  d1f8                  sar       eax,1
  0049a328  02d0                  add       dl,al
  0049a32a  80c214                add       dl,14h
  0049a32d  885643                mov       [esi+43h],dl
  0049a330  e80bce0a00            call      00547140h
  0049a335  66894610              mov       [esi+10h],ax

LAB_0049a339:
  0049a339  5f                    pop       edi
  0049a33a  5e                    pop       esi
  0049a33b  5d                    pop       ebp
  0049a33c  5b                    pop       ebx
  0049a33d  83c42c                add       esp,2Ch
  0049a340  c3                    ret

32 cible(s) de saut interne : 0x00499e95, 0x00499eea, 0x00499ef4, 0x00499efc, 0x00499f13, 0x00499f4b, 0x00499f5e, 0x00499f87, 0x00499fb2, 0x00499fc2, 0x00499fc6, 0x00499fe3, 0x00499ff6, 0x0049a002, 0x0049a032, 0x0049a042, 0x0049a060, 0x0049a075, 0x0049a0bd, 0x0049a0c6, 0x0049a0cc, 0x0049a1dc, 0x0049a1f4, 0x0049a226, 0x0049a270, 0x0049a275, 0x0049a28a, 0x0049a2c3, 0x0049a2d5, 0x0049a2fc, 0x0049a311, 0x0049a339
