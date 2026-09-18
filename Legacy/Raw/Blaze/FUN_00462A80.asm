
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00462a80  section .text  offset fichier 0x61e80  1744 octets

  00462a80  53                    push      ebx
  00462a81  55                    push      ebp
  00462a82  56                    push      esi
  00462a83  8b742410              mov       esi,[esp+10h]
  00462a87  57                    push      edi
  00462a88  33ff                  xor       edi,edi
  00462a8a  66397e10              cmp       [esi+10h],di
  00462a8e  7533                  jne       short 00462AC3h
  00462a90  0fbf4614              movsx     eax,word [esi+14h]
  00462a94  6a01                  push      1
  00462a96  57                    push      edi
  00462a97  8d0c80                lea       ecx,[eax+eax*4]
  00462a9a  c1e103                shl       ecx,3
  00462a9d  2bc8                  sub       ecx,eax
  00462a9f  8b2c8e                mov       ebp,[esi+ecx*4]
  00462aa2  8d048e                lea       eax,[esi+ecx*4]
  00462aa5  81cd00400000          or        ebp,4000h
  00462aab  8928                  mov       [eax],ebp
  00462aad  6689be98000000        mov       [esi+98h],di
  00462ab4  8b15cc975e00          mov       edx,[5E97CCh]
  00462aba  52                    push      edx
  00462abb  e810130b00            call      00513DD0h
  00462ac0  83c40c                add       esp,0Ch

LAB_00462ac3:
  00462ac3  66837e1001            cmp       word [esi+10h],1
  00462ac8  bb02000000            mov       ebx,2
  00462acd  0f85ce010000          jne       near 00462CA1h
  00462ad3  6639be98000000        cmp       [esi+98h],di
  00462ada  7528                  jne       short 00462B04h
  00462adc  a1cc975e00            mov       eax,[5E97CCh]
  00462ae1  57                    push      edi
  00462ae2  57                    push      edi
  00462ae3  68000b0000            push      0B00h
  00462ae8  8b88a8000000          mov       ecx,[eax+0A8h]
  00462aee  6800060000            push      600h
  00462af3  8b11                  mov       edx,[ecx]
  00462af5  660fb6421d            movzx     ax,byte [edx+1Dh]
  00462afa  50                    push      eax
  00462afb  56                    push      esi
  00462afc  e87f550000            call      00468080h
  00462b01  83c418                add       esp,18h

LAB_00462b04:
  00462b04  66ff8698000000        inc       word [esi+98h]
  00462b0b  6683be9800000008      cmp       word [esi+98h],8
  00462b13  7507                  jne       short 00462B1Ch
  00462b15  6689be98000000        mov       [esi+98h],di

LAB_00462b1c:
  00462b1c  6683463411            add       word [esi+34h],11h
  00462b21  66397e34              cmp       [esi+34h],di
  00462b25  7c37                  jl        short 00462B5Eh
  00462b27  66897e34              mov       [esi+34h],di
  00462b2b  66895e10              mov       [esi+10h],bx
  00462b2f  66c746120a00          mov       word [esi+12h],0Ah
  00462b35  8b0dcc975e00          mov       ecx,[5E97CCh]
  00462b3b  57                    push      edi
  00462b3c  57                    push      edi
  00462b3d  8b91a8000000          mov       edx,[ecx+0A8h]
  00462b43  6800100000            push      1000h
  00462b48  6800040000            push      400h
  00462b4d  8b02                  mov       eax,[edx]
  00462b4f  660fb64827            movzx     cx,byte [eax+27h]
  00462b54  51                    push      ecx
  00462b55  56                    push      esi
  00462b56  e825550000            call      00468080h
  00462b5b  83c418                add       esp,18h

LAB_00462b5e:
  00462b5e  66397e32              cmp       [esi+32h],di
  00462b62  7535                  jne       short 00462B99h
  00462b64  0fbf4614              movsx     eax,word [esi+14h]
  00462b68  8d1480                lea       edx,[eax+eax*4]
  00462b6b  c1e203                shl       edx,3
  00462b6e  2bd0                  sub       edx,eax
  00462b70  0fbf4634              movsx     eax,word [esi+34h]
  00462b74  50                    push      eax
  00462b75  8d7c961c              lea       edi,[esi+edx*4+1Ch]
  00462b79  e89e2e0e00            call      00545A1Ch
  00462b7e  0fbf4e34              movsx     ecx,word [esi+34h]
  00462b82  8be8                  mov       ebp,eax
  00462b84  51                    push      ecx
  00462b85  d1fd                  sar       ebp,1
  00462b87  e8902e0e00            call      00545A1Ch
  00462b8c  8b0f                  mov       ecx,[edi]
  00462b8e  8d1440                lea       edx,[eax+eax*2]
  00462b91  8d445500              lea       eax,[ebp+edx*2]
  00462b95  03c8                  add       ecx,eax
  00462b97  890f                  mov       [edi],ecx

LAB_00462b99:
  00462b99  66817e320004          cmp       word [esi+32h],400h
  00462b9f  753f                  jne       short 00462BE0h
  00462ba1  0fbf4e34              movsx     ecx,word [esi+34h]
  00462ba5  51                    push      ecx
  00462ba6  e8712e0e00            call      00545A1Ch
  00462bab  8d1440                lea       edx,[eax+eax*2]
  00462bae  0fbf4614              movsx     eax,word [esi+14h]
  00462bb2  d1e2                  shl       edx,1
  00462bb4  8d0c80                lea       ecx,[eax+eax*4]
  00462bb7  c1e103                shl       ecx,3
  00462bba  2bc8                  sub       ecx,eax
  00462bbc  8b7c8e24              mov       edi,[esi+ecx*4+24h]
  00462bc0  2bfa                  sub       edi,edx
  00462bc2  0fbf5634              movsx     edx,word [esi+34h]
  00462bc6  52                    push      edx
  00462bc7  e8502e0e00            call      00545A1Ch
  00462bcc  d1f8                  sar       eax,1
  00462bce  2bf8                  sub       edi,eax
  00462bd0  0fbf4614              movsx     eax,word [esi+14h]
  00462bd4  8d0c80                lea       ecx,[eax+eax*4]
  00462bd7  c1e103                shl       ecx,3
  00462bda  2bc8                  sub       ecx,eax
  00462bdc  897c8e24              mov       [esi+ecx*4+24h],edi

LAB_00462be0:
  00462be0  66817e320008          cmp       word [esi+32h],800h
  00462be6  7541                  jne       short 00462C29h
  00462be8  0fbf5634              movsx     edx,word [esi+34h]
  00462bec  52                    push      edx
  00462bed  e82a2e0e00            call      00545A1Ch
  00462bf2  8d0440                lea       eax,[eax+eax*2]
  00462bf5  d1e0                  shl       eax,1
  00462bf7  8bc8                  mov       ecx,eax
  00462bf9  0fbf4614              movsx     eax,word [esi+14h]
  00462bfd  8d1480                lea       edx,[eax+eax*4]
  00462c00  c1e203                shl       edx,3
  00462c03  2bd0                  sub       edx,eax
  00462c05  0fbf4634              movsx     eax,word [esi+34h]
  00462c09  8b7c961c              mov       edi,[esi+edx*4+1Ch]
  00462c0d  50                    push      eax
  00462c0e  2bf9                  sub       edi,ecx
  00462c10  e8072e0e00            call      00545A1Ch
  00462c15  d1f8                  sar       eax,1
  00462c17  2bf8                  sub       edi,eax
  00462c19  0fbf4614              movsx     eax,word [esi+14h]
  00462c1d  8d0c80                lea       ecx,[eax+eax*4]
  00462c20  c1e103                shl       ecx,3
  00462c23  2bc8                  sub       ecx,eax
  00462c25  897c8e1c              mov       [esi+ecx*4+1Ch],edi

LAB_00462c29:
  00462c29  66817e32000c          cmp       word [esi+32h],0C00h
  00462c2f  7535                  jne       short 00462C66h
  00462c31  0fbf4614              movsx     eax,word [esi+14h]
  00462c35  8d1480                lea       edx,[eax+eax*4]
  00462c38  c1e203                shl       edx,3
  00462c3b  2bd0                  sub       edx,eax
  00462c3d  0fbf4634              movsx     eax,word [esi+34h]
  00462c41  50                    push      eax
  00462c42  8d7c9624              lea       edi,[esi+edx*4+24h]
  00462c46  e8d12d0e00            call      00545A1Ch
  00462c4b  0fbf4e34              movsx     ecx,word [esi+34h]
  00462c4f  8be8                  mov       ebp,eax
  00462c51  51                    push      ecx
  00462c52  d1fd                  sar       ebp,1
  00462c54  e8c32d0e00            call      00545A1Ch
  00462c59  8b0f                  mov       ecx,[edi]
  00462c5b  8d1440                lea       edx,[eax+eax*2]
  00462c5e  8d445500              lea       eax,[ebp+edx*2]
  00462c62  03c8                  add       ecx,eax
  00462c64  890f                  mov       [edi],ecx

LAB_00462c66:
  00462c66  0fbf4614              movsx     eax,word [esi+14h]
  00462c6a  8d0c80                lea       ecx,[eax+eax*4]
  00462c6d  c1e103                shl       ecx,3
  00462c70  2bc8                  sub       ecx,eax
  00462c72  6683448e3405          add       word [esi+ecx*4+34h],5
  00462c78  8d448e34              lea       eax,[esi+ecx*4+34h]
  00462c7c  0fbf4614              movsx     eax,word [esi+14h]
  00462c80  8d1480                lea       edx,[eax+eax*4]
  00462c83  c1e203                shl       edx,3
  00462c86  2bd0                  sub       edx,eax
  00462c88  0fbf4634              movsx     eax,word [esi+34h]
  00462c8c  50                    push      eax
  00462c8d  8d7c9620              lea       edi,[esi+edx*4+20h]
  00462c91  e8802d0e00            call      00545A16h
  00462c96  8d0c40                lea       ecx,[eax+eax*2]
  00462c99  8b07                  mov       eax,[edi]
  00462c9b  d1e1                  shl       ecx,1
  00462c9d  03c1                  add       eax,ecx
  00462c9f  8907                  mov       [edi],eax

LAB_00462ca1:
  00462ca1  66395e10              cmp       [esi+10h],bx
  00462ca5  756a                  jne       short 00462D11h
  00462ca7  8b15cc975e00          mov       edx,[5E97CCh]
  00462cad  53                    push      ebx
  00462cae  6a00                  push      0
  00462cb0  52                    push      edx
  00462cb1  e81a110b00            call      00513DD0h
  00462cb6  a1cc975e00            mov       eax,[5E97CCh]
  00462cbb  83c40c                add       esp,0Ch
  00462cbe  6a04                  push      4
  00462cc0  6a00                  push      0
  00462cc2  50                    push      eax
  00462cc3  e808110b00            call      00513DD0h
  00462cc8  668b4612              mov       ax,[esi+12h]
  00462ccc  83c40c                add       esp,0Ch
  00462ccf  66f7d8                neg       ax
  00462cd2  6685c0                test      ax,ax
  00462cd5  66894612              mov       [esi+12h],ax
  00462cd9  7e03                  jle       short 00462CDEh
  00462cdb  48                    dec       eax
  00462cdc  eb01                  jmp       short 00462CDFh

LAB_00462cde:
  00462cde  40                    inc       eax

LAB_00462cdf:
  00462cdf  0fbfc8                movsx     ecx,ax
  00462ce2  6a00                  push      0
  00462ce4  6a00                  push      0
  00462ce6  68e7030000            push      3E7h
  00462ceb  51                    push      ecx
  00462cec  68e7030000            push      3E7h
  00462cf1  56                    push      esi
  00462cf2  66894612              mov       [esi+12h],ax
  00462cf6  e86591fbff            call      0041BE60h
  00462cfb  83c418                add       esp,18h
  00462cfe  66837e1200            cmp       word [esi+12h],0
  00462d03  750c                  jne       short 00462D11h
  00462d05  66c746100300          mov       word [esi+10h],3
  00462d0b  66c746120000          mov       word [esi+12h],0

LAB_00462d11:
  00462d11  66837e1063            cmp       word [esi+10h],63h
  00462d16  0f85d4010000          jne       near 00462EF0h
  00462d1c  0fbf4614              movsx     eax,word [esi+14h]
  00462d20  53                    push      ebx
  00462d21  6a00                  push      0
  00462d23  8d1480                lea       edx,[eax+eax*4]
  00462d26  c1e203                shl       edx,3
  00462d29  2bd0                  sub       edx,eax
  00462d2b  8b2c96                mov       ebp,[esi+edx*4]
  00462d2e  8d0496                lea       eax,[esi+edx*4]
  00462d31  81e5ffbfffff          and       ebp,0FFFFBFFFh
  00462d37  8928                  mov       [eax],ebp
  00462d39  0fbf4614              movsx     eax,word [esi+14h]
  00462d3d  8d0c80                lea       ecx,[eax+eax*4]
  00462d40  c1e103                shl       ecx,3
  00462d43  2bc8                  sub       ecx,eax
  00462d45  c7448e200030e6ff      mov       dword [esi+ecx*4+20h],0FFE63000h
  00462d4d  8b15cc975e00          mov       edx,[5E97CCh]
  00462d53  52                    push      edx
  00462d54  e877100b00            call      00513DD0h
  00462d59  a1cc975e00            mov       eax,[5E97CCh]
  00462d5e  83c40c                add       esp,0Ch
  00462d61  6a04                  push      4
  00462d63  6a00                  push      0
  00462d65  50                    push      eax
  00462d66  e865100b00            call      00513DD0h
  00462d6b  83c40c                add       esp,0Ch
  00462d6e  c74424143c000000      mov       dword [esp+14h],3Ch

LAB_00462d76:
  00462d76  6683463411            add       word [esi+34h],11h
  00462d7b  66837e3200            cmp       word [esi+32h],0
  00462d80  668b5e34              mov       bx,[esi+34h]
  00462d84  7542                  jne       short 00462DC8h
  00462d86  0fbf4614              movsx     eax,word [esi+14h]
  00462d8a  0fbfd3                movsx     edx,bx
  00462d8d  8d0c80                lea       ecx,[eax+eax*4]
  00462d90  52                    push      edx
  00462d91  c1e103                shl       ecx,3
  00462d94  2bc8                  sub       ecx,eax
  00462d96  8d7c8e1c              lea       edi,[esi+ecx*4+1Ch]
  00462d9a  e87d2c0e00            call      00545A1Ch
  00462d9f  8be8                  mov       ebp,eax
  00462da1  0fbf4634              movsx     eax,word [esi+34h]
  00462da5  50                    push      eax
  00462da6  c1fd02                sar       ebp,2
  00462da9  e86e2c0e00            call      00545A1Ch
  00462dae  0fbfd3                movsx     edx,bx
  00462db1  8d0c40                lea       ecx,[eax+eax*2]
  00462db4  52                    push      edx
  00462db5  8d6c4d00              lea       ebp,[ebp+ecx*2]
  00462db9  e85e2c0e00            call      00545A1Ch
  00462dbe  d1f8                  sar       eax,1
  00462dc0  03e8                  add       ebp,eax
  00462dc2  8b07                  mov       eax,[edi]
  00462dc4  03c5                  add       eax,ebp
  00462dc6  8907                  mov       [edi],eax

LAB_00462dc8:
  00462dc8  66817e320004          cmp       word [esi+32h],400h
  00462dce  754e                  jne       short 00462E1Eh
  00462dd0  0fbf4634              movsx     eax,word [esi+34h]
  00462dd4  50                    push      eax
  00462dd5  e8422c0e00            call      00545A1Ch
  00462dda  8d0c40                lea       ecx,[eax+eax*2]
  00462ddd  0fbf4614              movsx     eax,word [esi+14h]
  00462de1  d1e1                  shl       ecx,1
  00462de3  8d1480                lea       edx,[eax+eax*4]
  00462de6  c1e203                shl       edx,3
  00462de9  2bd0                  sub       edx,eax
  00462deb  0fbf4634              movsx     eax,word [esi+34h]
  00462def  8b7c9624              mov       edi,[esi+edx*4+24h]
  00462df3  50                    push      eax
  00462df4  2bf9                  sub       edi,ecx
  00462df6  e8212c0e00            call      00545A1Ch
  00462dfb  0fbf4e34              movsx     ecx,word [esi+34h]
  00462dff  c1f802                sar       eax,2
  00462e02  51                    push      ecx
  00462e03  2bf8                  sub       edi,eax
  00462e05  e8122c0e00            call      00545A1Ch
  00462e0a  d1f8                  sar       eax,1
  00462e0c  2bf8                  sub       edi,eax
  00462e0e  0fbf4614              movsx     eax,word [esi+14h]
  00462e12  8d1480                lea       edx,[eax+eax*4]
  00462e15  c1e203                shl       edx,3
  00462e18  2bd0                  sub       edx,eax
  00462e1a  897c9624              mov       [esi+edx*4+24h],edi

LAB_00462e1e:
  00462e1e  66817e320008          cmp       word [esi+32h],800h
  00462e24  754e                  jne       short 00462E74h
  00462e26  0fbf4634              movsx     eax,word [esi+34h]
  00462e2a  50                    push      eax
  00462e2b  e8ec2b0e00            call      00545A1Ch
  00462e30  8d0c40                lea       ecx,[eax+eax*2]
  00462e33  0fbf4614              movsx     eax,word [esi+14h]
  00462e37  d1e1                  shl       ecx,1
  00462e39  8d1480                lea       edx,[eax+eax*4]
  00462e3c  c1e203                shl       edx,3
  00462e3f  2bd0                  sub       edx,eax
  00462e41  0fbf4634              movsx     eax,word [esi+34h]
  00462e45  8b7c961c              mov       edi,[esi+edx*4+1Ch]
  00462e49  50                    push      eax
  00462e4a  2bf9                  sub       edi,ecx
  00462e4c  e8cb2b0e00            call      00545A1Ch
  00462e51  0fbf4e34              movsx     ecx,word [esi+34h]
  00462e55  c1f802                sar       eax,2
  00462e58  51                    push      ecx
  00462e59  2bf8                  sub       edi,eax
  00462e5b  e8bc2b0e00            call      00545A1Ch
  00462e60  d1f8                  sar       eax,1
  00462e62  2bf8                  sub       edi,eax
  00462e64  0fbf4614              movsx     eax,word [esi+14h]
  00462e68  8d1480                lea       edx,[eax+eax*4]
  00462e6b  c1e203                shl       edx,3
  00462e6e  2bd0                  sub       edx,eax
  00462e70  897c961c              mov       [esi+edx*4+1Ch],edi

LAB_00462e74:
  00462e74  66817e32000c          cmp       word [esi+32h],0C00h
  00462e7a  7543                  jne       short 00462EBFh
  00462e7c  0fbf4614              movsx     eax,word [esi+14h]
  00462e80  0fbf5634              movsx     edx,word [esi+34h]
  00462e84  8d0c80                lea       ecx,[eax+eax*4]
  00462e87  52                    push      edx
  00462e88  c1e103                shl       ecx,3
  00462e8b  2bc8                  sub       ecx,eax
  00462e8d  8d7c8e24              lea       edi,[esi+ecx*4+24h]
  00462e91  e8862b0e00            call      00545A1Ch
  00462e96  8bd8                  mov       ebx,eax
  00462e98  0fbf4634              movsx     eax,word [esi+34h]
  00462e9c  50                    push      eax
  00462e9d  d1fb                  sar       ebx,1
  00462e9f  e8782b0e00            call      00545A1Ch
  00462ea4  0fbf4e34              movsx     ecx,word [esi+34h]
  00462ea8  c1f802                sar       eax,2
  00462eab  51                    push      ecx
  00462eac  03d8                  add       ebx,eax
  00462eae  e8692b0e00            call      00545A1Ch
  00462eb3  8b0f                  mov       ecx,[edi]
  00462eb5  8d1440                lea       edx,[eax+eax*2]
  00462eb8  8d0453                lea       eax,[ebx+edx*2]
  00462ebb  03c8                  add       ecx,eax
  00462ebd  890f                  mov       [edi],ecx

LAB_00462ebf:
  00462ebf  0fbf4614              movsx     eax,word [esi+14h]
  00462ec3  8d0c80                lea       ecx,[eax+eax*4]
  00462ec6  c1e103                shl       ecx,3
  00462ec9  2bc8                  sub       ecx,eax
  00462ecb  6683448e3405          add       word [esi+ecx*4+34h],5
  00462ed1  8d448e34              lea       eax,[esi+ecx*4+34h]
  00462ed5  8b442414              mov       eax,[esp+14h]
  00462ed9  48                    dec       eax
  00462eda  89442414              mov       [esp+14h],eax
  00462ede  0f8592feffff          jne       near 00462D76h
  00462ee4  66c746340000          mov       word [esi+34h],0
  00462eea  66c746106400          mov       word [esi+10h],64h

LAB_00462ef0:
  00462ef0  66817e10c800          cmp       word [esi+10h],0C8h
  00462ef6  bbc9000000            mov       ebx,0C9h
  00462efb  0f85fd010000          jne       near 004630FEh
  00462f01  8b15cc975e00          mov       edx,[5E97CCh]
  00462f07  6a01                  push      1
  00462f09  6a00                  push      0
  00462f0b  52                    push      edx
  00462f0c  e8bf0e0b00            call      00513DD0h
  00462f11  a1cc975e00            mov       eax,[5E97CCh]
  00462f16  83c40c                add       esp,0Ch
  00462f19  6a03                  push      3
  00462f1b  6a00                  push      0
  00462f1d  50                    push      eax
  00462f1e  e8ad0e0b00            call      00513DD0h
  00462f23  83c40c                add       esp,0Ch
  00462f26  6683be9800000000      cmp       word [esi+98h],0
  00462f2e  752b                  jne       short 00462F5Bh
  00462f30  8b0dcc975e00          mov       ecx,[5E97CCh]
  00462f36  6a00                  push      0
  00462f38  6a00                  push      0
  00462f3a  68000b0000            push      0B00h
  00462f3f  8b91a8000000          mov       edx,[ecx+0A8h]
  00462f45  6800060000            push      600h
  00462f4a  8b02                  mov       eax,[edx]
  00462f4c  660fb6481d            movzx     cx,byte [eax+1Dh]
  00462f51  51                    push      ecx
  00462f52  56                    push      esi
  00462f53  e828510000            call      00468080h
  00462f58  83c418                add       esp,18h

LAB_00462f5b:
  00462f5b  66ff8698000000        inc       word [esi+98h]
  00462f62  6683be9800000003      cmp       word [esi+98h],3
  00462f6a  7c09                  jl        short 00462F75h
  00462f6c  66c786980000000000    mov       word [esi+98h],0

LAB_00462f75:
  00462f75  66834634de            add       word [esi+34h],0FFDEh
  00462f7a  668b4634              mov       ax,[esi+34h]
  00462f7e  663d00fc              cmp       ax,0FC00h
  00462f82  7f3b                  jg        short 00462FBFh
  00462f84  66c7463400fc          mov       word [esi+34h],0FC00h
  00462f8a  66895e10              mov       [esi+10h],bx
  00462f8e  66c746121400          mov       word [esi+12h],14h
  00462f94  8b15cc975e00          mov       edx,[5E97CCh]
  00462f9a  6a00                  push      0
  00462f9c  6a00                  push      0
  00462f9e  8b82a8000000          mov       eax,[edx+0A8h]
  00462fa4  6800180000            push      1800h
  00462fa9  6800050000            push      500h
  00462fae  8b08                  mov       ecx,[eax]
  00462fb0  660fb65127            movzx     dx,byte [ecx+27h]
  00462fb5  52                    push      edx
  00462fb6  56                    push      esi
  00462fb7  e8c4500000            call      00468080h
  00462fbc  83c418                add       esp,18h

LAB_00462fbf:
  00462fbf  66837e3200            cmp       word [esi+32h],0
  00462fc4  753e                  jne       short 00463004h
  00462fc6  0fbf4634              movsx     eax,word [esi+34h]
  00462fca  50                    push      eax
  00462fcb  e84c2a0e00            call      00545A1Ch
  00462fd0  8d0c40                lea       ecx,[eax+eax*2]
  00462fd3  0fbf4614              movsx     eax,word [esi+14h]
  00462fd7  c1e102                shl       ecx,2
  00462fda  8d1480                lea       edx,[eax+eax*4]
  00462fdd  c1e203                shl       edx,3
  00462fe0  2bd0                  sub       edx,eax
  00462fe2  0fbf4634              movsx     eax,word [esi+34h]
  00462fe6  8b7c961c              mov       edi,[esi+edx*4+1Ch]
  00462fea  50                    push      eax
  00462feb  2bf9                  sub       edi,ecx
  00462fed  e82a2a0e00            call      00545A1Ch
  00462ff2  2bf8                  sub       edi,eax
  00462ff4  0fbf4614              movsx     eax,word [esi+14h]
  00462ff8  8d0c80                lea       ecx,[eax+eax*4]
  00462ffb  c1e103                shl       ecx,3
  00462ffe  2bc8                  sub       ecx,eax
  00463000  897c8e1c              mov       [esi+ecx*4+1Ch],edi

LAB_00463004:
  00463004  66817e320004          cmp       word [esi+32h],400h
  0046300a  7530                  jne       short 0046303Ch
  0046300c  0fbf4614              movsx     eax,word [esi+14h]
  00463010  8d1480                lea       edx,[eax+eax*4]
  00463013  c1e203                shl       edx,3
  00463016  2bd0                  sub       edx,eax
  00463018  0fbf4634              movsx     eax,word [esi+34h]
  0046301c  50                    push      eax
  0046301d  8d7c9624              lea       edi,[esi+edx*4+24h]
  00463021  e8f6290e00            call      00545A1Ch
  00463026  0fbf4e34              movsx     ecx,word [esi+34h]
  0046302a  51                    push      ecx
  0046302b  8d2c40                lea       ebp,[eax+eax*2]
  0046302e  e8e9290e00            call      00545A1Ch
  00463033  8d14a8                lea       edx,[eax+ebp*4]
  00463036  8b07                  mov       eax,[edi]
  00463038  03c2                  add       eax,edx
  0046303a  8907                  mov       [edi],eax

LAB_0046303c:
  0046303c  66817e320008          cmp       word [esi+32h],800h
  00463042  7530                  jne       short 00463074h
  00463044  0fbf4614              movsx     eax,word [esi+14h]
  00463048  0fbf5634              movsx     edx,word [esi+34h]
  0046304c  8d0c80                lea       ecx,[eax+eax*4]
  0046304f  52                    push      edx
  00463050  c1e103                shl       ecx,3
  00463053  2bc8                  sub       ecx,eax
  00463055  8d7c8e1c              lea       edi,[esi+ecx*4+1Ch]
  00463059  e8be290e00            call      00545A1Ch
  0046305e  8d2c40                lea       ebp,[eax+eax*2]
  00463061  0fbf4634              movsx     eax,word [esi+34h]
  00463065  50                    push      eax
  00463066  e8b1290e00            call      00545A1Ch
  0046306b  8d0ca8                lea       ecx,[eax+ebp*4]
  0046306e  8b07                  mov       eax,[edi]
  00463070  03c1                  add       eax,ecx
  00463072  8907                  mov       [edi],eax

LAB_00463074:
  00463074  66817e32000c          cmp       word [esi+32h],0C00h
  0046307a  7540                  jne       short 004630BCh
  0046307c  0fbf5634              movsx     edx,word [esi+34h]
  00463080  52                    push      edx
  00463081  e896290e00            call      00545A1Ch
  00463086  8d0440                lea       eax,[eax+eax*2]
  00463089  c1e002                shl       eax,2
  0046308c  8bc8                  mov       ecx,eax
  0046308e  0fbf4614              movsx     eax,word [esi+14h]
  00463092  8d1480                lea       edx,[eax+eax*4]
  00463095  c1e203                shl       edx,3
  00463098  2bd0                  sub       edx,eax
  0046309a  0fbf4634              movsx     eax,word [esi+34h]
  0046309e  8b7c9624              mov       edi,[esi+edx*4+24h]
  004630a2  50                    push      eax
  004630a3  2bf9                  sub       edi,ecx
  004630a5  e872290e00            call      00545A1Ch
  004630aa  2bf8                  sub       edi,eax
  004630ac  0fbf4614              movsx     eax,word [esi+14h]
  004630b0  8d0c80                lea       ecx,[eax+eax*4]
  004630b3  c1e103                shl       ecx,3
  004630b6  2bc8                  sub       ecx,eax
  004630b8  897c8e24              mov       [esi+ecx*4+24h],edi

LAB_004630bc:
  004630bc  0fbf4614              movsx     eax,word [esi+14h]
  004630c0  8d1480                lea       edx,[eax+eax*4]
  004630c3  c1e203                shl       edx,3
  004630c6  2bd0                  sub       edx,eax
  004630c8  6683449634f6          add       word [esi+edx*4+34h],0FFF6h
  004630ce  8d449634              lea       eax,[esi+edx*4+34h]
  004630d2  0fbf4614              movsx     eax,word [esi+14h]
  004630d6  0fbf5634              movsx     edx,word [esi+34h]
  004630da  8d0c80                lea       ecx,[eax+eax*4]
  004630dd  52                    push      edx
  004630de  c1e103                shl       ecx,3
  004630e1  2bc8                  sub       ecx,eax
  004630e3  8d7c8e20              lea       edi,[esi+ecx*4+20h]
  004630e7  e82a290e00            call      00545A16h
  004630ec  8bc8                  mov       ecx,eax
  004630ee  c1e102                shl       ecx,2
  004630f1  2bc8                  sub       ecx,eax
  004630f3  8b07                  mov       eax,[edi]
  004630f5  f7d9                  neg       ecx
  004630f7  c1e102                shl       ecx,2
  004630fa  03c1                  add       eax,ecx
  004630fc  8907                  mov       [edi],eax

LAB_004630fe:
  004630fe  66395e10              cmp       [esi+10h],bx
  00463102  7546                  jne       short 0046314Ah
  00463104  668b4612              mov       ax,[esi+12h]
  00463108  66f7d8                neg       ax
  0046310b  6685c0                test      ax,ax
  0046310e  66894612              mov       [esi+12h],ax
  00463112  7e03                  jle       short 00463117h
  00463114  48                    dec       eax
  00463115  eb01                  jmp       short 00463118h

LAB_00463117:
  00463117  40                    inc       eax

LAB_00463118:
  00463118  0fbfd0                movsx     edx,ax
  0046311b  6a00                  push      0
  0046311d  6a00                  push      0
  0046311f  68e7030000            push      3E7h
  00463124  52                    push      edx
  00463125  68e7030000            push      3E7h
  0046312a  56                    push      esi
  0046312b  66894612              mov       [esi+12h],ax
  0046312f  e82c8dfbff            call      0041BE60h
  00463134  83c418                add       esp,18h
  00463137  66837e1200            cmp       word [esi+12h],0
  0046313c  750c                  jne       short 0046314Ah
  0046313e  66c74610ca00          mov       word [esi+10h],0CAh
  00463144  66c746120000          mov       word [esi+12h],0

LAB_0046314a:
  0046314a  5f                    pop       edi
  0046314b  5e                    pop       esi
  0046314c  5d                    pop       ebp
  0046314d  5b                    pop       ebx
  0046314e  c3                    ret

29 cible(s) de saut interne : 0x00462ac3, 0x00462b04, 0x00462b1c, 0x00462b5e, 0x00462b99, 0x00462be0, 0x00462c29, 0x00462c66, 0x00462ca1, 0x00462cde, 0x00462cdf, 0x00462d11, 0x00462d76, 0x00462dc8, 0x00462e1e, 0x00462e74, 0x00462ebf, 0x00462ef0, 0x00462f5b, 0x00462f75, 0x00462fbf, 0x00463004, 0x0046303c, 0x00463074, 0x004630bc, 0x004630fe, 0x00463117, 0x00463118, 0x0046314a
