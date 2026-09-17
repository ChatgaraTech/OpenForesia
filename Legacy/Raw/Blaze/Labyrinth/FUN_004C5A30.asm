Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c5a30  section .text  offset fichier 0xc4e30  3280 octets

  004c5a30  81ec88000000          sub       esp,88h
  004c5a36  53                    push      ebx
  004c5a37  55                    push      ebp
  004c5a38  56                    push      esi
  004c5a39  8bb42498000000        mov       esi,[esp+98h]
  004c5a40  33db                  xor       ebx,ebx
  004c5a42  57                    push      edi
  004c5a43  8a4641                mov       al,[esi+41h]
  004c5a46  66895c2424            mov       [esp+24h],bx
  004c5a4b  3ac3                  cmp       al,bl
  004c5a4d  66c74424261e00        mov       word [esp+26h],1Eh
  004c5a54  66c74424283200        mov       word [esp+28h],32h
  004c5a5b  66c744242a6400        mov       word [esp+2Ah],64h
  004c5a62  66895c242c            mov       [esp+2Ch],bx
  004c5a67  66c744242e5000        mov       word [esp+2Eh],50h
  004c5a6e  7533                  jne       short 004C5AA3h
  004c5a70  53                    push      ebx
  004c5a71  6a01                  push      1
  004c5a73  56                    push      esi
  004c5a74  c6464101              mov       byte [esi+41h],1
  004c5a78  e8d3d9ffff            call      004C3450h
  004c5a7d  8b4620                mov       eax,[esi+20h]
  004c5a80  83c40c                add       esp,0Ch
  004c5a83  894648                mov       [esi+48h],eax
  004c5a86  668b4616              mov       ax,[esi+16h]
  004c5a8a  663d0a00              cmp       ax,0Ah
  004c5a8e  7c0d                  jl        short 004C5A9Dh
  004c5a90  83c0f6                add       eax,0FFFFFFF6h
  004c5a93  66894616              mov       [esi+16h],ax
  004c5a97  8b06                  mov       eax,[esi]
  004c5a99  24df                  and       al,0DFh
  004c5a9b  eb04                  jmp       short 004C5AA1h

LAB_004c5a9d:
  004c5a9d  8b06                  mov       eax,[esi]
  004c5a9f  0c20                  or        al,20h

LAB_004c5aa1:
  004c5aa1  8906                  mov       [esi],eax

LAB_004c5aa3:
  004c5aa3  8b4e6c                mov       ecx,[esi+6Ch]
  004c5aa6  8b561c                mov       edx,[esi+1Ch]
  004c5aa9  8b4620                mov       eax,[esi+20h]
  004c5aac  894c2458              mov       [esp+58h],ecx
  004c5ab0  8b0e                  mov       ecx,[esi]
  004c5ab2  8d7e1c                lea       edi,[esi+1Ch]
  004c5ab5  80cd10                or        ch,10h
  004c5ab8  89542448              mov       [esp+48h],edx
  004c5abc  8b5664                mov       edx,[esi+64h]
  004c5abf  890e                  mov       [esi],ecx
  004c5ac1  8b4e24                mov       ecx,[esi+24h]
  004c5ac4  8944244c              mov       [esp+4Ch],eax
  004c5ac8  8b4660                mov       eax,[esi+60h]
  004c5acb  894c2450              mov       [esp+50h],ecx
  004c5acf  668b4e32              mov       cx,[esi+32h]
  004c5ad3  8954243c              mov       [esp+3Ch],edx
  004c5ad7  8b567c                mov       edx,[esi+7Ch]
  004c5ada  89442434              mov       [esp+34h],eax
  004c5ade  668b8690000000        mov       ax,[esi+90h]
  004c5ae5  66898c2486000000      mov       [esp+86h],cx
  004c5aed  8b8e80000000          mov       ecx,[esi+80h]
  004c5af3  8954245c              mov       [esp+5Ch],edx
  004c5af7  8b9684000000          mov       edx,[esi+84h]
  004c5afd  6689442444            mov       [esp+44h],ax
  004c5b02  8b8688000000          mov       eax,[esi+88h]
  004c5b08  894c2454              mov       [esp+54h],ecx
  004c5b0c  8b8e8c000000          mov       ecx,[esi+8Ch]
  004c5b12  89542440              mov       [esp+40h],edx
  004c5b16  89442430              mov       [esp+30h],eax
  004c5b1a  8d542460              lea       edx,[esp+60h]
  004c5b1e  894c2438              mov       [esp+38h],ecx
  004c5b22  8d442464              lea       eax,[esp+64h]
  004c5b26  52                    push      edx
  004c5b27  8d4c246c              lea       ecx,[esp+6Ch]
  004c5b2b  50                    push      eax
  004c5b2c  51                    push      ecx
  004c5b2d  56                    push      esi
  004c5b2e  e84d2e0700            call      00538980h
  004c5b33  83c410                add       esp,10h
  004c5b36  8d54246c              lea       edx,[esp+6Ch]
  004c5b3a  8d442420              lea       eax,[esp+20h]
  004c5b3e  8d4c2414              lea       ecx,[esp+14h]
  004c5b42  52                    push      edx
  004c5b43  50                    push      eax
  004c5b44  8d542420              lea       edx,[esp+20h]
  004c5b48  51                    push      ecx
  004c5b49  8d44241c              lea       eax,[esp+1Ch]
  004c5b4d  52                    push      edx
  004c5b4e  50                    push      eax
  004c5b4f  56                    push      esi
  004c5b50  e8cb030700            call      00535F20h
  004c5b55  8b4c2428              mov       ecx,[esp+28h]
  004c5b59  b840000000            mov       eax,40h
  004c5b5e  2bc1                  sub       eax,ecx
  004c5b60  83c418                add       esp,18h
  004c5b63  8944241c              mov       [esp+1Ch],eax
  004c5b67  8b442420              mov       eax,[esp+20h]
  004c5b6b  3bc3                  cmp       eax,ebx
  004c5b6d  0f8407060000          je        near 004C617Ah
  004c5b73  8b2e                  mov       ebp,[esi]
  004c5b75  33d2                  xor       edx,edx
  004c5b77  8a5604                mov       dl,[esi+4]
  004c5b7a  81e5fffffffd          and       ebp,0FDFFFFFFh
  004c5b80  0fbf5e18              movsx     ebx,word [esi+18h]
  004c5b84  892e                  mov       [esi],ebp
  004c5b86  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004c5b8d  f7c500000001          test      ebp,1000000h
  004c5b93  8b149a                mov       edx,[edx+ebx*4]
  004c5b96  741d                  je        short 004C5BB5h
  004c5b98  66ff461a              inc       word [esi+1Ah]
  004c5b9c  668b5e1a              mov       bx,[esi+1Ah]
  004c5ba0  33ed                  xor       ebp,ebp
  004c5ba2  668b6a16              mov       bp,[edx+16h]
  004c5ba6  0fbfd3                movsx     edx,bx
  004c5ba9  3bd5                  cmp       edx,ebp
  004c5bab  7c2b                  jl        short 004C5BD8h
  004c5bad  33db                  xor       ebx,ebx
  004c5baf  66895e1a              mov       [esi+1Ah],bx
  004c5bb3  eb25                  jmp       short 004C5BDAh

LAB_004c5bb5:
  004c5bb5  66ff461a              inc       word [esi+1Ah]
  004c5bb9  33db                  xor       ebx,ebx
  004c5bbb  668b5a16              mov       bx,[edx+16h]
  004c5bbf  0fbf561a              movsx     edx,word [esi+1Ah]
  004c5bc3  3bd3                  cmp       edx,ebx
  004c5bc5  7c11                  jl        short 004C5BD8h
  004c5bc7  668b561a              mov       dx,[esi+1Ah]
  004c5bcb  4a                    dec       edx
  004c5bcc  81cd00000002          or        ebp,2000000h
  004c5bd2  6689561a              mov       [esi+1Ah],dx
  004c5bd6  892e                  mov       [esi],ebp

LAB_004c5bd8:
  004c5bd8  33db                  xor       ebx,ebx

LAB_004c5bda:
  004c5bda  f60602                test      byte [esi],2
  004c5bdd  7465                  je        short 004C5C44h
  004c5bdf  66837e1a04            cmp       word [esi+1Ah],4
  004c5be4  755e                  jne       short 004C5C44h
  004c5be6  8d442424              lea       eax,[esp+24h]
  004c5bea  53                    push      ebx
  004c5beb  50                    push      eax
  004c5bec  57                    push      edi
  004c5bed  56                    push      esi
  004c5bee  e83d400500            call      00519C30h
  004c5bf3  8be8                  mov       ebp,eax
  004c5bf5  a1b8e05d00            mov       eax,[5DE0B8h]
  004c5bfa  83c410                add       esp,10h
  004c5bfd  3bc3                  cmp       eax,ebx
  004c5bff  743b                  je        short 004C5C3Ch
  004c5c01  66837e1a04            cmp       word [esi+1Ah],4
  004c5c06  7534                  jne       short 004C5C3Ch
  004c5c08  8b4d00                mov       ecx,[ebp]
  004c5c0b  55                    push      ebp
  004c5c0c  83c940                or        ecx,40h
  004c5c0f  56                    push      esi
  004c5c10  66c785960000001000    mov       word [ebp+96h],10h
  004c5c19  894d00                mov       [ebp],ecx
  004c5c1c  e85f3f0700            call      00539B80h
  004c5c21  83c408                add       esp,8
  004c5c24  83c51c                add       ebp,1Ch
  004c5c27  55                    push      ebp
  004c5c28  6800180000            push      1800h
  004c5c2d  6800030000            push      300h
  004c5c32  6a03                  push      3
  004c5c34  e8e77e0700            call      0053DB20h
  004c5c39  83c410                add       esp,10h

LAB_004c5c3c:
  004c5c3c  8b442420              mov       eax,[esp+20h]
  004c5c40  8b4c2410              mov       ecx,[esp+10h]

LAB_004c5c44:
  004c5c44  33d2                  xor       edx,edx
  004c5c46  8a5642                mov       dl,[esi+42h]
  004c5c49  83fa0a                cmp       edx,0Ah
  004c5c4c  0f873f040000          ja        near 004C6091h
  004c5c52  ff2495c8664c00        jmp       dword [edx*4+4C66C8h]
  004c5c59  66395e12              cmp       [esi+12h],bx
  004c5c5d  751a                  jne       short 004C5C79h
  004c5c5f  8b06                  mov       eax,[esi]
  004c5c61  66895e18              mov       [esi+18h],bx
  004c5c65  0d00000001            or        eax,1000000h
  004c5c6a  66895e1a              mov       [esi+1Ah],bx
  004c5c6e  8906                  mov       [esi],eax
  004c5c70  66c746120100          mov       word [esi+12h],1
  004c5c76  895e5c                mov       [esi+5Ch],ebx

LAB_004c5c79:
  004c5c79  0fbf4e10              movsx     ecx,word [esi+10h]
  004c5c7d  394c2418              cmp       [esp+18h],ecx
  004c5c81  7d08                  jge       short 004C5C8Bh
  004c5c83  885e43                mov       [esi+43h],bl
  004c5c86  e906040000            jmp       004C6091h

LAB_004c5c8b:
  004c5c8b  8a4643                mov       al,[esi+43h]
  004c5c8e  3ac3                  cmp       al,bl
  004c5c90  0f84af020000          je        near 004C5F45h
  004c5c96  fec8                  dec       al
  004c5c98  895e5c                mov       [esi+5Ch],ebx
  004c5c9b  884643                mov       [esi+43h],al
  004c5c9e  e9ee030000            jmp       004C6091h
  004c5ca3  66395e12              cmp       [esi+12h],bx
  004c5ca7  7517                  jne       short 004C5CC0h
  004c5ca9  8b06                  mov       eax,[esi]
  004c5cab  66895e18              mov       [esi+18h],bx
  004c5caf  0d00000001            or        eax,1000000h
  004c5cb4  66895e1a              mov       [esi+1Ah],bx
  004c5cb8  8906                  mov       [esi],eax
  004c5cba  66c746120100          mov       word [esi+12h],1

LAB_004c5cc0:
  004c5cc0  8b442414              mov       eax,[esp+14h]
  004c5cc4  8d9090010000          lea       edx,[eax+190h]
  004c5cca  81e2ff0f0000          and       edx,0FFFh
  004c5cd0  81fa20030000          cmp       edx,320h
  004c5cd6  0f8e66020000          jle       near 004C5F42h
  004c5cdc  25ff0f0000            and       eax,0FFFh
  004c5ce1  3d00080000            cmp       eax,800h
  004c5ce6  7d0f                  jge       short 004C5CF7h
  004c5ce8  8d4132                lea       eax,[ecx+32h]
  004c5ceb  895e5c                mov       [esi+5Ch],ebx
  004c5cee  66014632              add       [esi+32h],ax
  004c5cf2  e99a030000            jmp       004C6091h

LAB_004c5cf7:
  004c5cf7  b8ceffffff            mov       eax,0FFFFFFCEh
  004c5cfc  895e5c                mov       [esi+5Ch],ebx
  004c5cff  2bc1                  sub       eax,ecx
  004c5d01  66014632              add       [esi+32h],ax
  004c5d05  e987030000            jmp       004C6091h
  004c5d0a  66395e12              cmp       [esi+12h],bx
  004c5d0e  7526                  jne       short 004C5D36h
  004c5d10  8b2e                  mov       ebp,[esi]
  004c5d12  8d04c9                lea       eax,[ecx+ecx*8]
  004c5d15  c1e004                shl       eax,4
  004c5d18  81cd00000001          or        ebp,1000000h
  004c5d1e  0500500000            add       eax,5000h
  004c5d23  66895e18              mov       [esi+18h],bx
  004c5d27  66895e1a              mov       [esi+1Ah],bx
  004c5d2b  892e                  mov       [esi],ebp
  004c5d2d  66c746120100          mov       word [esi+12h],1
  004c5d33  89465c                mov       [esi+5Ch],eax

LAB_004c5d36:
  004c5d36  66837e1201            cmp       word [esi+12h],1
  004c5d3b  0f8550030000          jne       near 004C6091h
  004c5d41  0fbf5610              movsx     edx,word [esi+10h]
  004c5d45  8b442418              mov       eax,[esp+18h]
  004c5d49  66895e18              mov       [esi+18h],bx
  004c5d4d  3bc2                  cmp       eax,edx
  004c5d4f  66895e1a              mov       [esi+1Ah],bx
  004c5d53  7d06                  jge       short 004C5D5Bh
  004c5d55  885e43                mov       [esi+43h],bl
  004c5d58  895e5c                mov       [esi+5Ch],ebx

LAB_004c5d5b:
  004c5d5b  8a4643                mov       al,[esi+43h]
  004c5d5e  3ac3                  cmp       al,bl
  004c5d60  7605                  jbe       short 004C5D67h
  004c5d62  fec8                  dec       al
  004c5d64  884643                mov       [esi+43h],al

LAB_004c5d67:
  004c5d67  8b442414              mov       eax,[esp+14h]
  004c5d6b  8d9000010000          lea       edx,[eax+100h]
  004c5d71  81e2ff0f0000          and       edx,0FFFh
  004c5d77  81fa00020000          cmp       edx,200h
  004c5d7d  0f8e0e030000          jle       near 004C6091h
  004c5d83  25ff0f0000            and       eax,0FFFh
  004c5d88  3d00080000            cmp       eax,800h
  004c5d8d  7d10                  jge       short 004C5D9Fh
  004c5d8f  8d8c0980000000        lea       ecx,[ecx+ecx+80h]
  004c5d96  66014e32              add       [esi+32h],cx
  004c5d9a  e9f2020000            jmp       004C6091h

LAB_004c5d9f:
  004c5d9f  8d0409                lea       eax,[ecx+ecx]
  004c5da2  b980ffffff            mov       ecx,0FFFFFF80h
  004c5da7  2bc8                  sub       ecx,eax
  004c5da9  66014e32              add       [esi+32h],cx
  004c5dad  e9df020000            jmp       004C6091h
  004c5db2  66837e1201            cmp       word [esi+12h],1
  004c5db7  0f85bb000000          jne       near 004C5E78h
  004c5dbd  8a5643                mov       dl,[esi+43h]
  004c5dc0  66895e18              mov       [esi+18h],bx
  004c5dc4  3ad3                  cmp       dl,bl
  004c5dc6  7405                  je        short 004C5DCDh
  004c5dc8  feca                  dec       dl
  004c5dca  885643                mov       [esi+43h],dl

LAB_004c5dcd:
  004c5dcd  83c170                add       ecx,70h
  004c5dd0  c1e108                shl       ecx,8
  004c5dd3  0fafc1                imul      eax,ecx
  004c5dd6  894e5c                mov       [esi+5Ch],ecx
  004c5dd9  0fbf4e10              movsx     ecx,word [esi+10h]
  004c5ddd  c1f80c                sar       eax,0Ch
  004c5de0  51                    push      ecx
  004c5de1  8be8                  mov       ebp,eax
  004c5de3  e834fc0700            call      00545A1Ch
  004c5de8  0fafc5                imul      eax,ebp
  004c5deb  0fbf5610              movsx     edx,word [esi+10h]
  004c5def  8b0f                  mov       ecx,[edi]
  004c5df1  52                    push      edx
  004c5df2  c1f80c                sar       eax,0Ch
  004c5df5  03c8                  add       ecx,eax
  004c5df7  890f                  mov       [edi],ecx
  004c5df9  e818fc0700            call      00545A16h
  004c5dfe  0fafc5                imul      eax,ebp
  004c5e01  8b6e24                mov       ebp,[esi+24h]
  004c5e04  895e5c                mov       [esi+5Ch],ebx
  004c5e07  c1f80c                sar       eax,0Ch
  004c5e0a  03e8                  add       ebp,eax
  004c5e0c  8b442414              mov       eax,[esp+14h]
  004c5e10  896e24                mov       [esi+24h],ebp
  004c5e13  8d8800010000          lea       ecx,[eax+100h]
  004c5e19  81e1ff0f0000          and       ecx,0FFFh
  004c5e1f  81f900020000          cmp       ecx,200h
  004c5e25  7e28                  jle       short 004C5E4Fh
  004c5e27  25ff0f0000            and       eax,0FFFh
  004c5e2c  3d00080000            cmp       eax,800h
  004c5e31  7d0a                  jge       short 004C5E3Dh
  004c5e33  8b542410              mov       edx,[esp+10h]
  004c5e37  8d441232              lea       eax,[edx+edx+32h]
  004c5e3b  eb0e                  jmp       short 004C5E4Bh

LAB_004c5e3d:
  004c5e3d  8b442410              mov       eax,[esp+10h]
  004c5e41  8d0c00                lea       ecx,[eax+eax]
  004c5e44  b8ceffffff            mov       eax,0FFFFFFCEh
  004c5e49  2bc1                  sub       eax,ecx

LAB_004c5e4b:
  004c5e4b  66014632              add       [esi+32h],ax

LAB_004c5e4f:
  004c5e4f  668b4614              mov       ax,[esi+14h]
  004c5e53  668bd0                mov       dx,ax
  004c5e56  66c1fa03              sar       dx,3
  004c5e5a  2bc2                  sub       eax,edx
  004c5e5c  663d0500              cmp       ax,5
  004c5e60  66894614              mov       [esi+14h],ax
  004c5e64  7d09                  jge       short 004C5E6Fh
  004c5e66  885e43                mov       [esi+43h],bl
  004c5e69  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004c5e6f:
  004c5e6f  0500100000            add       eax,1000h
  004c5e74  6689462c              mov       [esi+2Ch],ax

LAB_004c5e78:
  004c5e78  66395e12              cmp       [esi+12h],bx
  004c5e7c  0f850f020000          jne       near 004C6091h
  004c5e82  8b0e                  mov       ecx,[esi]
  004c5e84  66895e18              mov       [esi+18h],bx
  004c5e88  81c900000001          or        ecx,1000000h
  004c5e8e  66895e1a              mov       [esi+1Ah],bx
  004c5e92  890e                  mov       [esi],ecx
  004c5e94  66c746120100          mov       word [esi+12h],1
  004c5e9a  e8a1120800            call      00547140h
  004c5e9f  99                    cdq
  004c5ea0  b914000000            mov       ecx,14h
  004c5ea5  f7f9                  idiv      ecx
  004c5ea7  8b442410              mov       eax,[esp+10h]
  004c5eab  8d4c0205              lea       ecx,[edx+eax+5]
  004c5eaf  c1e104                shl       ecx,4
  004c5eb2  66894e14              mov       [esi+14h],cx
  004c5eb6  e9d6010000            jmp       004C6091h
  004c5ebb  668b5612              mov       dx,[esi+12h]
  004c5ebf  6683fa01              cmp       dx,1
  004c5ec3  751b                  jne       short 004C5EE0h
  004c5ec5  0fbf4610              movsx     eax,word [esi+10h]
  004c5ec9  39442418              cmp       [esp+18h],eax
  004c5ecd  7d05                  jge       short 004C5ED4h
  004c5ecf  885e43                mov       [esi+43h],bl
  004c5ed2  eb0c                  jmp       short 004C5EE0h

LAB_004c5ed4:
  004c5ed4  8a4643                mov       al,[esi+43h]
  004c5ed7  3ac3                  cmp       al,bl
  004c5ed9  7405                  je        short 004C5EE0h
  004c5edb  fec8                  dec       al
  004c5edd  884643                mov       [esi+43h],al

LAB_004c5ee0:
  004c5ee0  663bd3                cmp       dx,bx
  004c5ee3  0f85a8010000          jne       near 004C6091h
  004c5ee9  66895e18              mov       [esi+18h],bx
  004c5eed  66895e1a              mov       [esi+1Ah],bx
  004c5ef1  8b1e                  mov       ebx,[esi]
  004c5ef3  83e1f8                and       ecx,0FFFFFFF8h
  004c5ef6  83c120                add       ecx,20h
  004c5ef9  81cb00000001          or        ebx,1000000h
  004c5eff  c1e109                shl       ecx,9
  004c5f02  891e                  mov       [esi],ebx
  004c5f04  66c746120100          mov       word [esi+12h],1
  004c5f0a  894e5c                mov       [esi+5Ch],ecx
  004c5f0d  e97f010000            jmp       004C6091h
  004c5f12  668b4e32              mov       cx,[esi+32h]
  004c5f16  66895e18              mov       [esi+18h],bx
  004c5f1a  0fbf4610              movsx     eax,word [esi+10h]
  004c5f1e  0fbfd1                movsx     edx,cx
  004c5f21  8bf8                  mov       edi,eax
  004c5f23  2bfa                  sub       edi,edx
  004c5f25  8bd0                  mov       edx,eax
  004c5f27  c1ff03                sar       edi,3
  004c5f2a  03cf                  add       ecx,edi
  004c5f2c  66894e32              mov       [esi+32h],cx
  004c5f30  0fbfc9                movsx     ecx,cx
  004c5f33  2bd1                  sub       edx,ecx
  004c5f35  7804                  js        short 004C5F3Bh
  004c5f37  8bca                  mov       ecx,edx
  004c5f39  eb02                  jmp       short 004C5F3Dh

LAB_004c5f3b:
  004c5f3b  2bc8                  sub       ecx,eax

LAB_004c5f3d:
  004c5f3d  83f932                cmp       ecx,32h
  004c5f40  7d03                  jge       short 004C5F45h

LAB_004c5f42:
  004c5f42  885e43                mov       [esi+43h],bl

LAB_004c5f45:
  004c5f45  895e5c                mov       [esi+5Ch],ebx
  004c5f48  e944010000            jmp       004C6091h
  004c5f4d  66395e12              cmp       [esi+12h],bx
  004c5f51  0f85d5000000          jne       near 004C602Ch
  004c5f57  33ff                  xor       edi,edi
  004c5f59  b800f45f00            mov       eax,5FF400h

LAB_004c5f5e:
  004c5f5e  39706c                cmp       [eax+6Ch],esi
  004c5f61  750c                  jne       short 004C5F6Fh
  004c5f63  f70000000080          test      dword [eax],80000000h
  004c5f69  0f854f070000          jne       near 004C66BEh

LAB_004c5f6f:
  004c5f6f  059c000000            add       eax,9Ch
  004c5f74  47                    inc       edi
  004c5f75  3d80076000            cmp       eax,600780h
  004c5f7a  7ce2                  jl        short 004C5F5Eh
  004c5f7c  8b06                  mov       eax,[esi]
  004c5f7e  a904080000            test      eax,804h
  004c5f83  740d                  je        short 004C5F92h
  004c5f85  885e43                mov       [esi+43h],bl
  004c5f88  25fbf7ffff            and       eax,0FFFFF7FBh
  004c5f8d  e9fd000000            jmp       004C608Fh

LAB_004c5f92:
  004c5f92  6aff                  push      0FFFFFFFFh
  004c5f94  53                    push      ebx
  004c5f95  68bd000000            push      0BDh
  004c5f9a  56                    push      esi
  004c5f9b  e8404bfbff            call      0047AAE0h
  004c5fa0  83c410                add       esp,10h
  004c5fa3  66395e16              cmp       [esi+16h],bx
  004c5fa7  bd01000000            mov       ebp,1
  004c5fac  66896e12              mov       [esi+12h],bp
  004c5fb0  7512                  jne       short 004C5FC4h
  004c5fb2  e889110800            call      00547140h
  004c5fb7  99                    cdq
  004c5fb8  33c2                  xor       eax,edx
  004c5fba  2bc2                  sub       eax,edx
  004c5fbc  23c5                  and       eax,ebp
  004c5fbe  33c2                  xor       eax,edx
  004c5fc0  8bf8                  mov       edi,eax
  004c5fc2  2bfa                  sub       edi,edx

LAB_004c5fc4:
  004c5fc4  66396e16              cmp       [esi+16h],bp
  004c5fc8  750f                  jne       short 004C5FD9h
  004c5fca  e871110800            call      00547140h
  004c5fcf  99                    cdq
  004c5fd0  b911000000            mov       ecx,11h
  004c5fd5  f7f9                  idiv      ecx
  004c5fd7  8bfa                  mov       edi,edx

LAB_004c5fd9:
  004c5fd9  33c0                  xor       eax,eax
  004c5fdb  668b047db02b5500      mov       ax,[edi*2+552BB0h]
  004c5fe3  66894606              mov       [esi+6],ax
  004c5fe7  8b0dc4975e00          mov       ecx,[5E97C4h]
  004c5fed  8bd0                  mov       edx,eax
  004c5fef  25ff000000            and       eax,0FFh
  004c5ff4  8b899c000000          mov       ecx,[ecx+9Ch]
  004c5ffa  c1fa08                sar       edx,8
  004c5ffd  81e2ff000000          and       edx,0FFh
  004c6003  8d0440                lea       eax,[eax+eax*2]
  004c6006  c1e004                shl       eax,4
  004c6009  8b0c91                mov       ecx,[ecx+edx*4]
  004c600c  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  004c6012  8b06                  mov       eax,[esi]
  004c6014  66895610              mov       [esi+10h],dx
  004c6018  0d04080001            or        eax,1000804h
  004c601d  895e5c                mov       [esi+5Ch],ebx
  004c6020  66895e18              mov       [esi+18h],bx
  004c6024  66895e1a              mov       [esi+1Ah],bx
  004c6028  8906                  mov       [esi],eax
  004c602a  eb05                  jmp       short 004C6031h

LAB_004c602c:
  004c602c  bd01000000            mov       ebp,1

LAB_004c6031:
  004c6031  66396e12              cmp       [esi+12h],bp
  004c6035  7510                  jne       short 004C6047h
  004c6037  66ff4e10              dec       word [esi+10h]
  004c603b  66396e10              cmp       [esi+10h],bp
  004c603f  7d06                  jge       short 004C6047h
  004c6041  66c746120200          mov       word [esi+12h],2

LAB_004c6047:
  004c6047  66837e1202            cmp       word [esi+12h],2
  004c604c  7532                  jne       short 004C6080h
  004c604e  8b16                  mov       edx,[esi]
  004c6050  33c0                  xor       eax,eax
  004c6052  668b4606              mov       ax,[esi+6]
  004c6056  6aff                  push      0FFFFFFFFh
  004c6058  8bc8                  mov       ecx,eax
  004c605a  6aff                  push      0FFFFFFFFh
  004c605c  c1f808                sar       eax,8
  004c605f  81e1ff000000          and       ecx,0FFh
  004c6065  25ff000000            and       eax,0FFh
  004c606a  51                    push      ecx
  004c606b  83e2fb                and       edx,0FFFFFFFBh
  004c606e  50                    push      eax
  004c606f  56                    push      esi
  004c6070  8916                  mov       [esi],edx
  004c6072  e809490700            call      0053A980h
  004c6077  83c414                add       esp,14h
  004c607a  66c746120300          mov       word [esi+12h],3

LAB_004c6080:
  004c6080  66837e1203            cmp       word [esi+12h],3
  004c6085  750a                  jne       short 004C6091h
  004c6087  8b06                  mov       eax,[esi]
  004c6089  885e43                mov       [esi+43h],bl
  004c608c  80e4f7                and       ah,0F7h

LAB_004c608f:
  004c608f  8906                  mov       [esi],eax

LAB_004c6091:
  004c6091  8b0e                  mov       ecx,[esi]
  004c6093  8a4642                mov       al,[esi+42h]
  004c6096  f6c120                test      cl,20h
  004c6099  0f8488000000          je        near 004C6127h
  004c609f  3c0a                  cmp       al,0Ah
  004c60a1  7524                  jne       short 004C60C7h
  004c60a3  0fbf5614              movsx     edx,word [esi+14h]
  004c60a7  8b7e20                mov       edi,[esi+20h]
  004c60aa  52                    push      edx
  004c60ab  e86cf90700            call      00545A1Ch
  004c60b0  8b4e48                mov       ecx,[esi+48h]
  004c60b3  8d0440                lea       eax,[eax+eax*2]
  004c60b6  8d0480                lea       eax,[eax+eax*4]
  004c60b9  c1e002                shl       eax,2
  004c60bc  2bc7                  sub       eax,edi
  004c60be  8d940800c00800        lea       edx,[eax+ecx+8C000h]
  004c60c5  eb3f                  jmp       short 004C6106h

LAB_004c60c7:
  004c60c7  e874100800            call      00547140h
  004c60cc  99                    cdq
  004c60cd  b932000000            mov       ecx,32h
  004c60d2  8b7e20                mov       edi,[esi+20h]
  004c60d5  f7f9                  idiv      ecx
  004c60d7  66035614              add       dx,[esi+14h]
  004c60db  83c264                add       edx,64h
  004c60de  81e2ff0f0000          and       edx,0FFFh
  004c60e4  66895614              mov       [esi+14h],dx
  004c60e8  0fbfd2                movsx     edx,dx
  004c60eb  52                    push      edx
  004c60ec  e82bf90700            call      00545A1Ch
  004c60f1  8b4e48                mov       ecx,[esi+48h]
  004c60f4  8d0440                lea       eax,[eax+eax*2]
  004c60f7  8d0480                lea       eax,[eax+eax*4]
  004c60fa  c1e002                shl       eax,2
  004c60fd  2bc7                  sub       eax,edi
  004c60ff  8d940800000a00        lea       edx,[eax+ecx+0A0000h]

LAB_004c6106:
  004c6106  8b4c2458              mov       ecx,[esp+58h]
  004c610a  c1fa03                sar       edx,3
  004c610d  03d7                  add       edx,edi
  004c610f  895620                mov       [esi+20h],edx
  004c6112  8b4164                mov       eax,[ecx+64h]
  004c6115  8b4920                mov       ecx,[ecx+20h]
  004c6118  33c8                  xor       ecx,eax
  004c611a  f7c100f0ffff          test      ecx,0FFFFF000h
  004c6120  7558                  jne       short 004C617Ah
  004c6122  894648                mov       [esi+48h],eax
  004c6125  eb53                  jmp       short 004C617Ah

LAB_004c6127:
  004c6127  3c0c                  cmp       al,0Ch
  004c6129  7404                  je        short 004C612Fh
  004c612b  3c0a                  cmp       al,0Ah
  004c612d  7505                  jne       short 004C6134h

LAB_004c612f:
  004c612f  f6c102                test      cl,2
  004c6132  7446                  je        short 004C617Ah

LAB_004c6134:
  004c6134  e807100800            call      00547140h
  004c6139  99                    cdq
  004c613a  b932000000            mov       ecx,32h
  004c613f  8b7e20                mov       edi,[esi+20h]
  004c6142  f7f9                  idiv      ecx
  004c6144  66035614              add       dx,[esi+14h]
  004c6148  83c264                add       edx,64h
  004c614b  81e2ff0f0000          and       edx,0FFFh
  004c6151  66895614              mov       [esi+14h],dx
  004c6155  0fbfd2                movsx     edx,dx
  004c6158  52                    push      edx
  004c6159  e8bef80700            call      00545A1Ch
  004c615e  8b4e60                mov       ecx,[esi+60h]
  004c6161  8d0440                lea       eax,[eax+eax*2]
  004c6164  8d0480                lea       eax,[eax+eax*4]
  004c6167  8d1481                lea       edx,[ecx+eax*4]
  004c616a  2bd7                  sub       edx,edi
  004c616c  81c200c00800          add       edx,8C000h
  004c6172  c1fa03                sar       edx,3
  004c6175  03d7                  add       edx,edi
  004c6177  895620                mov       [esi+20h],edx

LAB_004c617a:
  004c617a  0fbf4614              movsx     eax,word [esi+14h]
  004c617e  50                    push      eax
  004c617f  e898f80700            call      00545A1Ch
  004c6184  0fbf4e14              movsx     ecx,word [esi+14h]
  004c6188  c1f806                sar       eax,6
  004c618b  0464                  add       al,64h
  004c618d  51                    push      ecx
  004c618e  884638                mov       [esi+38h],al
  004c6191  e886f80700            call      00545A1Ch
  004c6196  0fbf5614              movsx     edx,word [esi+14h]
  004c619a  c1f806                sar       eax,6
  004c619d  0464                  add       al,64h
  004c619f  52                    push      edx
  004c61a0  884639                mov       [esi+39h],al
  004c61a3  e874f80700            call      00545A1Ch
  004c61a8  c1f806                sar       eax,6
  004c61ab  0464                  add       al,64h
  004c61ad  88463a                mov       [esi+3Ah],al
  004c61b0  0fbf4632              movsx     eax,word [esi+32h]
  004c61b4  50                    push      eax
  004c61b5  e862f80700            call      00545A1Ch
  004c61ba  0fbf4e32              movsx     ecx,word [esi+32h]
  004c61be  51                    push      ecx
  004c61bf  89442474              mov       [esp+74h],eax
  004c61c3  e84ef80700            call      00545A16h
  004c61c8  8b5650                mov       edx,[esi+50h]
  004c61cb  89442478              mov       [esp+78h],eax
  004c61cf  8bc2                  mov       eax,edx
  004c61d1  8b4e5c                mov       ecx,[esi+5Ch]
  004c61d4  c1f808                sar       eax,8
  004c61d7  0faf4c2420            imul      ecx,[esp+20h]
  004c61dc  8bfa                  mov       edi,edx
  004c61de  56                    push      esi
  004c61df  2bf8                  sub       edi,eax
  004c61e1  c1fa03                sar       edx,3
  004c61e4  2bfa                  sub       edi,edx
  004c61e6  8b5654                mov       edx,[esi+54h]
  004c61e9  8bc2                  mov       eax,edx
  004c61eb  8bea                  mov       ebp,edx
  004c61ed  c1f803                sar       eax,3
  004c61f0  2be8                  sub       ebp,eax
  004c61f2  897e50                mov       [esi+50h],edi
  004c61f5  c1fa08                sar       edx,8
  004c61f8  2bea                  sub       ebp,edx
  004c61fa  8b5658                mov       edx,[esi+58h]
  004c61fd  8bda                  mov       ebx,edx
  004c61ff  8bc2                  mov       eax,edx
  004c6201  c1fb03                sar       ebx,3
  004c6204  2bc3                  sub       eax,ebx
  004c6206  8b5e20                mov       ebx,[esi+20h]
  004c6209  c1fa08                sar       edx,8
  004c620c  c1f90c                sar       ecx,0Ch
  004c620f  2bc2                  sub       eax,edx
  004c6211  8bd1                  mov       edx,ecx
  004c6213  0faf542474            imul      edx,[esp+74h]
  004c6218  0faf4c247c            imul      ecx,[esp+7Ch]
  004c621d  c1fa0c                sar       edx,0Ch
  004c6220  c1f90c                sar       ecx,0Ch
  004c6223  896e54                mov       [esi+54h],ebp
  004c6226  03d7                  add       edx,edi
  004c6228  8b7e1c                mov       edi,[esi+1Ch]
  004c622b  03dd                  add       ebx,ebp
  004c622d  8b6e24                mov       ebp,[esi+24h]
  004c6230  03c8                  add       ecx,eax
  004c6232  03fa                  add       edi,edx
  004c6234  03e9                  add       ebp,ecx
  004c6236  894658                mov       [esi+58h],eax
  004c6239  897e1c                mov       [esi+1Ch],edi
  004c623c  895e20                mov       [esi+20h],ebx
  004c623f  896e24                mov       [esi+24h],ebp
  004c6242  e8f9d60600            call      00533940h
  004c6247  8b9690000000          mov       edx,[esi+90h]
  004c624d  8a0e                  mov       cl,[esi]
  004c624f  83c404                add       esp,4
  004c6252  80e6f8                and       dh,0F8h
  004c6255  f6c120                test      cl,20h
  004c6258  899690000000          mov       [esi+90h],edx
  004c625e  8bc2                  mov       eax,edx
  004c6260  740b                  je        short 004C626Dh
  004c6262  25ffdfffdf            and       eax,0DFFFDFFFh
  004c6267  898690000000          mov       [esi+90h],eax

LAB_004c626d:
  004c626d  8b8690000000          mov       eax,[esi+90h]
  004c6273  33db                  xor       ebx,ebx
  004c6275  a982a80020            test      eax,2000A882h
  004c627a  895e54                mov       [esi+54h],ebx
  004c627d  0f84cd000000          je        near 004C6350h
  004c6283  8a4e42                mov       cl,[esi+42h]
  004c6286  b007                  mov       al,7
  004c6288  3ac8                  cmp       cl,al
  004c628a  66c7462c0010          mov       word [esi+2Ch],1000h
  004c6290  7420                  je        short 004C62B2h
  004c6292  884642                mov       [esi+42h],al
  004c6295  e8a60e0800            call      00547140h
  004c629a  99                    cdq
  004c629b  b928000000            mov       ecx,28h
  004c62a0  66895e12              mov       [esi+12h],bx
  004c62a4  f7f9                  idiv      ecx
  004c62a6  66c746105000          mov       word [esi+10h],50h
  004c62ac  80c214                add       dl,14h
  004c62af  885643                mov       [esi+43h],dl

LAB_004c62b2:
  004c62b2  8b16                  mov       edx,[esi]
  004c62b4  8b44244c              mov       eax,[esp+4Ch]
  004c62b8  8b4c2450              mov       ecx,[esp+50h]
  004c62bc  83e2fd                and       edx,0FFFFFFFDh
  004c62bf  8916                  mov       [esi],edx
  004c62c1  8b542448              mov       edx,[esp+48h]
  004c62c5  894620                mov       [esi+20h],eax
  004c62c8  8b442444              mov       eax,[esp+44h]
  004c62cc  89561c                mov       [esi+1Ch],edx
  004c62cf  8b54245c              mov       edx,[esp+5Ch]
  004c62d3  25ffff0000            and       eax,0FFFFh
  004c62d8  894e24                mov       [esi+24h],ecx
  004c62db  8b4c2440              mov       ecx,[esp+40h]
  004c62df  89567c                mov       [esi+7Ch],edx
  004c62e2  8b542430              mov       edx,[esp+30h]
  004c62e6  898690000000          mov       [esi+90h],eax
  004c62ec  8b442454              mov       eax,[esp+54h]
  004c62f0  898e84000000          mov       [esi+84h],ecx
  004c62f6  8b4c243c              mov       ecx,[esp+3Ch]
  004c62fa  898680000000          mov       [esi+80h],eax
  004c6300  8b442438              mov       eax,[esp+38h]
  004c6304  899688000000          mov       [esi+88h],edx
  004c630a  8b542434              mov       edx,[esp+34h]
  004c630e  6880a80020            push      2000A880h
  004c6313  56                    push      esi
  004c6314  89868c000000          mov       [esi+8Ch],eax
  004c631a  894e64                mov       [esi+64h],ecx
  004c631d  895660                mov       [esi+60h],edx
  004c6320  e83bfb0000            call      004D5E60h
  004c6325  83c408                add       esp,8
  004c6328  663bc3                cmp       ax,bx
  004c632b  7515                  jne       short 004C6342h
  004c632d  e80e0e0800            call      00547140h
  004c6332  99                    cdq
  004c6333  33c2                  xor       eax,edx
  004c6335  2bc2                  sub       eax,edx
  004c6337  83e001                and       eax,1
  004c633a  33c2                  xor       eax,edx
  004c633c  2bc2                  sub       eax,edx
  004c633e  8d4400ff              lea       eax,[eax+eax-1]

LAB_004c6342:
  004c6342  8b4c2410              mov       ecx,[esp+10h]
  004c6346  83c140                add       ecx,40h
  004c6349  0fafc8                imul      ecx,eax
  004c634c  66014e32              add       [esi+32h],cx

LAB_004c6350:
  004c6350  6aff                  push      0FFFFFFFFh
  004c6352  6aff                  push      0FFFFFFFFh
  004c6354  6aff                  push      0FFFFFFFFh
  004c6356  56                    push      esi
  004c6357  e8b4d4ffff            call      004C3810h
  004c635c  8b442430              mov       eax,[esp+30h]
  004c6360  83c410                add       esp,10h
  004c6363  3bc3                  cmp       eax,ebx
  004c6365  7517                  jne       short 004C637Eh
  004c6367  668b942486000000      mov       dx,[esp+86h]
  004c636f  66895632              mov       [esi+32h],dx
  004c6373  5f                    pop       edi
  004c6374  5e                    pop       esi
  004c6375  5d                    pop       ebp
  004c6376  5b                    pop       ebx
  004c6377  81c488000000          add       esp,88h
  004c637d  c3                    ret

LAB_004c637e:
  004c637e  385e43                cmp       [esi+43h],bl
  004c6381  0f852c030000          jne       near 004C66B3h
  004c6387  8b466c                mov       eax,[esi+6Ch]
  004c638a  8b6e1c                mov       ebp,[esi+1Ch]
  004c638d  8b4e24                mov       ecx,[esi+24h]
  004c6390  8b781c                mov       edi,[eax+1Ch]
  004c6393  2bfd                  sub       edi,ebp
  004c6395  8b6824                mov       ebp,[eax+24h]
  004c6398  2be9                  sub       ebp,ecx
  004c639a  c1ff0c                sar       edi,0Ch
  004c639d  c1fd0c                sar       ebp,0Ch
  004c63a0  8bc7                  mov       eax,edi
  004c63a2  8bcd                  mov       ecx,ebp
  004c63a4  0fafc7                imul      eax,edi
  004c63a7  0fafcd                imul      ecx,ebp
  004c63aa  03c1                  add       eax,ecx
  004c63ac  50                    push      eax
  004c63ad  e85ef60700            call      00545A10h
  004c63b2  55                    push      ebp
  004c63b3  57                    push      edi
  004c63b4  89442420              mov       [esp+20h],eax
  004c63b8  e865f60700            call      00545A22h
  004c63bd  668b5632              mov       dx,[esi+32h]
  004c63c1  66895e12              mov       [esi+12h],bx
  004c63c5  2bc2                  sub       eax,edx
  004c63c7  25ff0f0000            and       eax,0FFFh
  004c63cc  89442414              mov       [esp+14h],eax
  004c63d0  8b442418              mov       eax,[esp+18h]
  004c63d4  3dfa000000            cmp       eax,0FAh
  004c63d9  0f8dc0000000          jge       near 004C649Fh
  004c63df  e85c0d0800            call      00547140h
  004c63e4  99                    cdq
  004c63e5  b964000000            mov       ecx,64h
  004c63ea  f7f9                  idiv      ecx
  004c63ec  83fa32                cmp       edx,32h
  004c63ef  7c3f                  jl        short 004C6430h
  004c63f1  83fa46                cmp       edx,46h
  004c63f4  7d26                  jge       short 004C641Ch
  004c63f6  8b6c241c              mov       ebp,[esp+1Ch]
  004c63fa  8b442410              mov       eax,[esp+10h]
  004c63fe  8bd5                  mov       edx,ebp
  004c6400  885e42                mov       [esi+42h],bl
  004c6403  d1fa                  sar       edx,1
  004c6405  80c20a                add       dl,0Ah
  004c6408  8d4c0050              lea       ecx,[eax+eax+50h]
  004c640c  885643                mov       [esi+43h],dl
  004c640f  66894e10              mov       [esi+10h],cx
  004c6413  66895e12              mov       [esi+12h],bx
  004c6417  e987000000            jmp       004C64A3h

LAB_004c641c:
  004c641c  83fa5a                cmp       edx,5Ah
  004c641f  7d5f                  jge       short 004C6480h
  004c6421  8b542410              mov       edx,[esp+10h]
  004c6425  8b442418              mov       eax,[esp+18h]
  004c6429  83c264                add       edx,64h
  004c642c  3bc2                  cmp       eax,edx
  004c642e  7e50                  jle       short 004C6480h

LAB_004c6430:
  004c6430  c6464203              mov       byte [esi+42h],3
  004c6434  e8070d0800            call      00547140h
  004c6439  99                    cdq
  004c643a  b90a000000            mov       ecx,0Ah
  004c643f  8b6c241c              mov       ebp,[esp+1Ch]
  004c6443  f7f9                  idiv      ecx
  004c6445  8b4c2410              mov       ecx,[esp+10h]
  004c6449  8bc5                  mov       eax,ebp
  004c644b  d1f8                  sar       eax,1
  004c644d  8db980000000          lea       edi,[ecx+80h]
  004c6453  c1e704                shl       edi,4
  004c6456  02d0                  add       dl,al
  004c6458  80c214                add       dl,14h
  004c645b  885643                mov       [esi+43h],dl
  004c645e  e8dd0c0800            call      00547140h
  004c6463  99                    cdq
  004c6464  f7ff                  idiv      edi
  004c6466  d1ff                  sar       edi,1
  004c6468  2bd7                  sub       edx,edi
  004c646a  8b7c2414              mov       edi,[esp+14h]
  004c646e  03d7                  add       edx,edi
  004c6470  66035632              add       dx,[esi+32h]
  004c6474  81c200080000          add       edx,800h
  004c647a  66895610              mov       [esi+10h],dx
  004c647e  eb23                  jmp       short 004C64A3h

LAB_004c6480:
  004c6480  c6464201              mov       byte [esi+42h],1
  004c6484  e8b70c0800            call      00547140h
  004c6489  99                    cdq
  004c648a  33c2                  xor       eax,edx
  004c648c  c6464301              mov       byte [esi+43h],1
  004c6490  2bc2                  sub       eax,edx
  004c6492  25ff0f0000            and       eax,0FFFh
  004c6497  33c2                  xor       eax,edx
  004c6499  2bc2                  sub       eax,edx
  004c649b  66894610              mov       [esi+10h],ax

LAB_004c649f:
  004c649f  8b6c241c              mov       ebp,[esp+1Ch]

LAB_004c64a3:
  004c64a3  8b442418              mov       eax,[esp+18h]
  004c64a7  3d58020000            cmp       eax,258h
  004c64ac  0f8df4000000          jge       near 004C65A6h
  004c64b2  3dfa000000            cmp       eax,0FAh
  004c64b7  0f8ce9000000          jl        near 004C65A6h
  004c64bd  e87e0c0800            call      00547140h
  004c64c2  99                    cdq
  004c64c3  b964000000            mov       ecx,64h
  004c64c8  f7f9                  idiv      ecx
  004c64ca  83fa14                cmp       edx,14h
  004c64cd  7d54                  jge       short 004C6523h
  004c64cf  8b542414              mov       edx,[esp+14h]
  004c64d3  81c200040000          add       edx,400h
  004c64d9  81e2ff0f0000          and       edx,0FFFh
  004c64df  81fa00080000          cmp       edx,800h
  004c64e5  7c0d                  jl        short 004C64F4h
  004c64e7  c6464201              mov       byte [esi+42h],1
  004c64eb  c6464301              mov       byte [esi+43h],1
  004c64ef  e9b2000000            jmp       004C65A6h

LAB_004c64f4:
  004c64f4  885e42                mov       [esi+42h],bl
  004c64f7  e8440c0800            call      00547140h
  004c64fc  99                    cdq
  004c64fd  b90a000000            mov       ecx,0Ah
  004c6502  66895e12              mov       [esi+12h],bx
  004c6506  f7f9                  idiv      ecx
  004c6508  8b4c2410              mov       ecx,[esp+10h]
  004c650c  8bc5                  mov       eax,ebp
  004c650e  c1f802                sar       eax,2
  004c6511  02d0                  add       dl,al
  004c6513  885643                mov       [esi+43h],dl
  004c6516  8d540950              lea       edx,[ecx+ecx+50h]
  004c651a  66895610              mov       [esi+10h],dx
  004c651e  e983000000            jmp       004C65A6h

LAB_004c6523:
  004c6523  83fa1e                cmp       edx,1Eh
  004c6526  7d25                  jge       short 004C654Dh
  004c6528  c6464202              mov       byte [esi+42h],2
  004c652c  e80f0c0800            call      00547140h
  004c6531  99                    cdq
  004c6532  b90a000000            mov       ecx,0Ah
  004c6537  66c746105000          mov       word [esi+10h],50h
  004c653d  f7f9                  idiv      ecx
  004c653f  8bc5                  mov       eax,ebp
  004c6541  d1f8                  sar       eax,1
  004c6543  02d0                  add       dl,al
  004c6545  80c214                add       dl,14h
  004c6548  885643                mov       [esi+43h],dl
  004c654b  eb59                  jmp       short 004C65A6h

LAB_004c654d:
  004c654d  83fa3c                cmp       edx,3Ch
  004c6550  7d0a                  jge       short 004C655Ch
  004c6552  c646420a              mov       byte [esi+42h],0Ah
  004c6556  c6464301              mov       byte [esi+43h],1
  004c655a  eb4a                  jmp       short 004C65A6h

LAB_004c655c:
  004c655c  c6464203              mov       byte [esi+42h],3
  004c6560  e8db0b0800            call      00547140h
  004c6565  99                    cdq
  004c6566  b90a000000            mov       ecx,0Ah
  004c656b  f7f9                  idiv      ecx
  004c656d  8b4c2410              mov       ecx,[esp+10h]
  004c6571  8bc5                  mov       eax,ebp
  004c6573  d1f8                  sar       eax,1
  004c6575  8db980000000          lea       edi,[ecx+80h]
  004c657b  c1e704                shl       edi,4
  004c657e  02d0                  add       dl,al
  004c6580  80c214                add       dl,14h
  004c6583  885643                mov       [esi+43h],dl
  004c6586  e8b50b0800            call      00547140h
  004c658b  99                    cdq
  004c658c  f7ff                  idiv      edi
  004c658e  d1ff                  sar       edi,1
  004c6590  2bd7                  sub       edx,edi
  004c6592  8b7c2414              mov       edi,[esp+14h]
  004c6596  03d7                  add       edx,edi
  004c6598  66035632              add       dx,[esi+32h]
  004c659c  81c200080000          add       edx,800h
  004c65a2  66895610              mov       [esi+10h],dx

LAB_004c65a6:
  004c65a6  817c241858020000      cmp       dword [esp+18h],258h
  004c65ae  0f8cff000000          jl        near 004C66B3h
  004c65b4  e8870b0800            call      00547140h
  004c65b9  99                    cdq
  004c65ba  b964000000            mov       ecx,64h
  004c65bf  f7f9                  idiv      ecx
  004c65c1  83fa32                cmp       edx,32h
  004c65c4  7d6f                  jge       short 004C6635h
  004c65c6  8b442410              mov       eax,[esp+10h]
  004c65ca  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004c65ce  3bd1                  cmp       edx,ecx
  004c65d0  7d2e                  jge       short 004C6600h
  004c65d2  c6464202              mov       byte [esi+42h],2
  004c65d6  e8650b0800            call      00547140h
  004c65db  99                    cdq
  004c65dc  b90a000000            mov       ecx,0Ah
  004c65e1  66c746106400          mov       word [esi+10h],64h
  004c65e7  f7f9                  idiv      ecx
  004c65e9  8bc5                  mov       eax,ebp
  004c65eb  d1f8                  sar       eax,1
  004c65ed  02d0                  add       dl,al
  004c65ef  80c214                add       dl,14h
  004c65f2  885643                mov       [esi+43h],dl
  004c65f5  5f                    pop       edi
  004c65f6  5e                    pop       esi
  004c65f7  5d                    pop       ebp
  004c65f8  5b                    pop       ebx
  004c65f9  81c488000000          add       esp,88h
  004c65ff  c3                    ret

LAB_004c6600:
  004c6600  885e42                mov       [esi+42h],bl
  004c6603  e8380b0800            call      00547140h
  004c6608  99                    cdq
  004c6609  b90a000000            mov       ecx,0Ah
  004c660e  66895e12              mov       [esi+12h],bx
  004c6612  f7f9                  idiv      ecx
  004c6614  8b4c2410              mov       ecx,[esp+10h]
  004c6618  8bc5                  mov       eax,ebp
  004c661a  c1f802                sar       eax,2
  004c661d  02d0                  add       dl,al
  004c661f  885643                mov       [esi+43h],dl
  004c6622  8d540950              lea       edx,[ecx+ecx+50h]
  004c6626  66895610              mov       [esi+10h],dx
  004c662a  5f                    pop       edi
  004c662b  5e                    pop       esi
  004c662c  5d                    pop       ebp
  004c662d  5b                    pop       ebx
  004c662e  81c488000000          add       esp,88h
  004c6634  c3                    ret

LAB_004c6635:
  004c6635  83fa46                cmp       edx,46h
  004c6638  7d55                  jge       short 004C668Fh
  004c663a  c6464203              mov       byte [esi+42h],3
  004c663e  e8fd0a0800            call      00547140h
  004c6643  99                    cdq
  004c6644  b90a000000            mov       ecx,0Ah
  004c6649  f7f9                  idiv      ecx
  004c664b  8b4c2410              mov       ecx,[esp+10h]
  004c664f  8bc5                  mov       eax,ebp
  004c6651  d1f8                  sar       eax,1
  004c6653  8db980000000          lea       edi,[ecx+80h]
  004c6659  c1e704                shl       edi,4
  004c665c  02d0                  add       dl,al
  004c665e  80c214                add       dl,14h
  004c6661  885643                mov       [esi+43h],dl
  004c6664  e8d70a0800            call      00547140h
  004c6669  99                    cdq
  004c666a  f7ff                  idiv      edi
  004c666c  d1ff                  sar       edi,1
  004c666e  2bd7                  sub       edx,edi
  004c6670  8b7c2414              mov       edi,[esp+14h]
  004c6674  03d7                  add       edx,edi
  004c6676  66035632              add       dx,[esi+32h]
  004c667a  81c200080000          add       edx,800h
  004c6680  66895610              mov       [esi+10h],dx
  004c6684  5f                    pop       edi
  004c6685  5e                    pop       esi
  004c6686  5d                    pop       ebp
  004c6687  5b                    pop       ebx
  004c6688  81c488000000          add       esp,88h
  004c668e  c3                    ret

LAB_004c668f:
  004c668f  83fa5a                cmp       edx,5Ah
  004c6692  c6464201              mov       byte [esi+42h],1
  004c6696  7c17                  jl        short 004C66AFh
  004c6698  e8a30a0800            call      00547140h
  004c669d  99                    cdq
  004c669e  33c2                  xor       eax,edx
  004c66a0  2bc2                  sub       eax,edx
  004c66a2  25ff0f0000            and       eax,0FFFh
  004c66a7  33c2                  xor       eax,edx
  004c66a9  2bc2                  sub       eax,edx
  004c66ab  66894610              mov       [esi+10h],ax

LAB_004c66af:
  004c66af  c6464301              mov       byte [esi+43h],1

LAB_004c66b3:
  004c66b3  5f                    pop       edi
  004c66b4  5e                    pop       esi
  004c66b5  5d                    pop       ebp
  004c66b6  5b                    pop       ebx
  004c66b7  81c488000000          add       esp,88h
  004c66bd  c3                    ret

LAB_004c66be:
  004c66be  885e43                mov       [esi+43h],bl
  004c66c1  e9cbf9ffff            jmp       004C6091h
  004c66c6  8bff                  mov       edi,edi
  004c66c8  59                    pop       ecx
  004c66c9  5c                    pop       esp
  004c66ca  4c                    dec       esp
  004c66cb  00a35c4c000a          add       [ebx+0A004C5Ch],ah
  004c66d1  5d                    pop       ebp
  004c66d2  4c                    dec       esp
  004c66d3  00b25d4c0091          add       [edx-6EFFB3A3h],dh
  004c66d9  60                    pusha
  004c66da  4c                    dec       esp
  004c66db  0091604c0091          add       [ecx-6EFFB3A0h],dl
  004c66e1  60                    pusha
  004c66e2  4c                    dec       esp
  004c66e3  00bb5e4c0091          add       [ebx-6EFFB3A2h],bh
  004c66e9  60                    pusha
  004c66ea  4c                    dec       esp
  004c66eb  0012                  add       [edx],dl
  004c66ed  5f                    pop       edi
  004c66ee  4c                    dec       esp
  004c66ef  004d5f                add       [ebp+5Fh],cl
  004c66f2  4c                    dec       esp
  004c66f3  009090909090          add       [eax-6F6F6F70h],dl
  004c66f9  90                    nop
  004c66fa  90                    nop
  004c66fb  90                    nop
  004c66fc  90                    nop
  004c66fd  90                    nop
  004c66fe  90                    nop
  004c66ff  90                    nop

66 cible(s) de saut interne : 0x004c5a9d, 0x004c5aa1, 0x004c5aa3, 0x004c5bb5, 0x004c5bd8, 0x004c5bda, 0x004c5c3c, 0x004c5c44, 0x004c5c79, 0x004c5c8b, 0x004c5cc0, 0x004c5cf7, 0x004c5d36, 0x004c5d5b, 0x004c5d67, 0x004c5d9f, 0x004c5dcd, 0x004c5e3d, 0x004c5e4b, 0x004c5e4f, 0x004c5e6f, 0x004c5e78, 0x004c5ed4, 0x004c5ee0, 0x004c5f3b, 0x004c5f3d, 0x004c5f42, 0x004c5f45, 0x004c5f5e, 0x004c5f6f, 0x004c5f92, 0x004c5fc4, 0x004c5fd9, 0x004c602c, 0x004c6031, 0x004c6047, 0x004c6080, 0x004c608f, 0x004c6091, 0x004c60c7, 0x004c6106, 0x004c6127, 0x004c612f, 0x004c6134, 0x004c617a, 0x004c626d, 0x004c62b2, 0x004c6342, 0x004c6350, 0x004c637e, 0x004c641c, 0x004c6430, 0x004c6480, 0x004c649f, 0x004c64a3, 0x004c64f4, 0x004c6523, 0x004c654d, 0x004c655c, 0x004c65a6, 0x004c6600, 0x004c6635, 0x004c668f, 0x004c66af, 0x004c66b3, 0x004c66be
