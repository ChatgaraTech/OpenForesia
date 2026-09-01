Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049e920  section .text  offset fichier 0x9dd20  1932 octets

  0049e920  83ec2c                sub       esp,2Ch
  0049e923  b832000000            mov       eax,32h
  0049e928  53                    push      ebx
  0049e929  55                    push      ebp
  0049e92a  668944242a            mov       [esp+2Ah],ax
  0049e92f  668944242e            mov       [esp+2Eh],ax
  0049e934  56                    push      esi
  0049e935  8b74243c              mov       esi,[esp+3Ch]
  0049e939  8d442420              lea       eax,[esp+20h]
  0049e93d  57                    push      edi
  0049e93e  8d4c2428              lea       ecx,[esp+28h]
  0049e942  50                    push      eax
  0049e943  8d542430              lea       edx,[esp+30h]
  0049e947  51                    push      ecx
  0049e948  33ff                  xor       edi,edi
  0049e94a  52                    push      edx
  0049e94b  56                    push      esi
  0049e94c  66897c2440            mov       [esp+40h],di
  0049e951  66c74424442300        mov       word [esp+44h],23h
  0049e958  66897c2448            mov       [esp+48h],di
  0049e95d  66c744244a5a00        mov       word [esp+4Ah],5Ah
  0049e964  e817a00900            call      00538980h
  0049e969  83c410                add       esp,10h
  0049e96c  8d442420              lea       eax,[esp+20h]
  0049e970  8d4c241c              lea       ecx,[esp+1Ch]
  0049e974  8d542414              lea       edx,[esp+14h]
  0049e978  50                    push      eax
  0049e979  51                    push      ecx
  0049e97a  8d442420              lea       eax,[esp+20h]
  0049e97e  52                    push      edx
  0049e97f  8d4c244c              lea       ecx,[esp+4Ch]
  0049e983  50                    push      eax
  0049e984  51                    push      ecx
  0049e985  56                    push      esi
  0049e986  e895750900            call      00535F20h
  0049e98b  8b4c2458              mov       ecx,[esp+58h]
  0049e98f  b840000000            mov       eax,40h
  0049e994  2bc1                  sub       eax,ecx
  0049e996  83c418                add       esp,18h
  0049e999  89442410              mov       [esp+10h],eax
  0049e99d  8b44241c              mov       eax,[esp+1Ch]
  0049e9a1  3bc7                  cmp       eax,edi
  0049e9a3  0f841f030000          je        near 0049ECC8h
  0049e9a9  56                    push      esi
  0049e9aa  e8f1cb0300            call      004DB5A0h
  0049e9af  8b06                  mov       eax,[esi]
  0049e9b1  83c404                add       esp,4
  0049e9b4  a802                  test      al,2
  0049e9b6  0f8449010000          je        near 0049EB05h
  0049e9bc  8a4c2420              mov       cl,[esp+20h]
  0049e9c0  bb10000000            mov       ebx,10h
  0049e9c5  84cb                  test      bl,cl
  0049e9c7  7415                  je        short 0049E9DEh
  0049e9c9  24fd                  and       al,0FDh
  0049e9cb  c6464300              mov       byte [esi+43h],0
  0049e9cf  8906                  mov       [esi],eax
  0049e9d1  8a4640                mov       al,[esi+40h]
  0049e9d4  24fe                  and       al,0FEh
  0049e9d6  884640                mov       [esi+40h],al
  0049e9d9  e9ea020000            jmp       0049ECC8h

LAB_0049e9de:
  0049e9de  8a4640                mov       al,[esi+40h]
  0049e9e1  33ff                  xor       edi,edi
  0049e9e3  a801                  test      al,1
  0049e9e5  7416                  je        short 0049E9FDh
  0049e9e7  66ff4e14              dec       word [esi+14h]
  0049e9eb  66397e14              cmp       [esi+14h],di
  0049e9ef  0f85aa000000          jne       near 0049EA9Fh
  0049e9f5  8b7e6c                mov       edi,[esi+6Ch]
  0049e9f8  e9a2000000            jmp       0049EA9Fh

LAB_0049e9fd:
  0049e9fd  66837e1a09            cmp       word [esi+1Ah],9
  0049ea02  0f8c97000000          jl        near 0049EA9Fh
  0049ea08  8d542430              lea       edx,[esp+30h]
  0049ea0c  8d6e1c                lea       ebp,[esi+1Ch]
  0049ea0f  6a00                  push      0
  0049ea11  52                    push      edx
  0049ea12  55                    push      ebp
  0049ea13  56                    push      esi
  0049ea14  e817b20700            call      00519C30h
  0049ea19  8bf8                  mov       edi,eax
  0049ea1b  83c410                add       esp,10h
  0049ea1e  85ff                  test      edi,edi
  0049ea20  0f84bf000000          je        near 0049EAE5h
  0049ea26  8b4500                mov       eax,[ebp]
  0049ea29  8b4f1c                mov       ecx,[edi+1Ch]
  0049ea2c  2bc1                  sub       eax,ecx
  0049ea2e  8b4e20                mov       ecx,[esi+20h]
  0049ea31  8b5624                mov       edx,[esi+24h]
  0049ea34  894644                mov       [esi+44h],eax
  0049ea37  2b4f20                sub       ecx,[edi+20h]
  0049ea3a  894e48                mov       [esi+48h],ecx
  0049ea3d  8b6f24                mov       ebp,[edi+24h]
  0049ea40  2bd5                  sub       edx,ebp
  0049ea42  89564c                mov       [esi+4Ch],edx
  0049ea45  e8f6860a00            call      00547140h
  0049ea4a  8b4c2440              mov       ecx,[esp+40h]
  0049ea4e  83e007                and       eax,7
  0049ea51  c1f902                sar       ecx,2
  0049ea54  8d540808              lea       edx,[eax+ecx+8]
  0049ea58  66895610              mov       [esi+10h],dx
  0049ea5c  8a5640                mov       dl,[esi+40h]
  0049ea5f  80ca01                or        dl,1
  0049ea62  885640                mov       [esi+40h],dl
  0049ea65  8b0f                  mov       ecx,[edi]
  0049ea67  83c940                or        ecx,40h
  0049ea6a  66899f96000000        mov       [edi+96h],bx
  0049ea71  890f                  mov       [edi],ecx
  0049ea73  66837e1600            cmp       word [esi+16h],0
  0049ea78  897e6c                mov       [esi+6Ch],edi
  0049ea7b  7422                  je        short 0049EA9Fh
  0049ea7d  e8be860a00            call      00547140h
  0049ea82  99                    cdq
  0049ea83  b964000000            mov       ecx,64h
  0049ea88  f7f9                  idiv      ecx
  0049ea8a  83fa1e                cmp       edx,1Eh
  0049ea8d  7d10                  jge       short 0049EA9Fh
  0049ea8f  6a00                  push      0
  0049ea91  57                    push      edi
  0049ea92  56                    push      esi
  0049ea93  c6460707              mov       byte [esi+7],7
  0049ea97  e884d90900            call      0053C420h
  0049ea9c  83c40c                add       esp,0Ch

LAB_0049ea9f:
  0049ea9f  85ff                  test      edi,edi
  0049eaa1  7442                  je        short 0049EAE5h
  0049eaa3  57                    push      edi
  0049eaa4  56                    push      esi
  0049eaa5  e8d6b00900            call      00539B80h
  0049eaaa  83c408                add       esp,8
  0049eaad  8d571c                lea       edx,[edi+1Ch]
  0049eab0  52                    push      edx
  0049eab1  6800180000            push      1800h
  0049eab6  6800030000            push      300h
  0049eabb  6a03                  push      3
  0049eabd  e85ef00900            call      0053DB20h
  0049eac2  8b07                  mov       eax,[edi]
  0049eac4  83c410                add       esp,10h
  0049eac7  a900000040            test      eax,40000000h
  0049eacc  7411                  je        short 0049EADFh
  0049eace  8a4e40                mov       cl,[esi+40h]
  0049ead1  8b06                  mov       eax,[esi]
  0049ead3  80e1fe                and       cl,0FEh
  0049ead6  24fd                  and       al,0FDh
  0049ead8  884e40                mov       [esi+40h],cl
  0049eadb  8906                  mov       [esi],eax
  0049eadd  eb06                  jmp       short 0049EAE5h

LAB_0049eadf:
  0049eadf  66c746141e00          mov       word [esi+14h],1Eh

LAB_0049eae5:
  0049eae5  8b06                  mov       eax,[esi]
  0049eae7  a900000002            test      eax,2000000h
  0049eaec  0f84d6010000          je        near 0049ECC8h
  0049eaf2  f6464001              test      byte [esi+40h],1
  0049eaf6  0f85cc010000          jne       near 0049ECC8h
  0049eafc  24fd                  and       al,0FDh
  0049eafe  8906                  mov       [esi],eax
  0049eb00  e9c3010000            jmp       0049ECC8h

LAB_0049eb05:
  0049eb05  a811                  test      al,11h
  0049eb07  0f85bb010000          jne       near 0049ECC8h
  0049eb0d  a904080000            test      eax,804h
  0049eb12  0f85b0010000          jne       near 0049ECC8h
  0049eb18  33c9                  xor       ecx,ecx
  0049eb1a  8a4e42                mov       cl,[esi+42h]
  0049eb1d  83f903                cmp       ecx,3
  0049eb20  0f87a2010000          ja        near 0049ECC8h
  0049eb26  ff248dacf04900        jmp       dword [ecx*4+49F0ACh]
  0049eb2d  33c9                  xor       ecx,ecx
  0049eb2f  66394e18              cmp       [esi+18h],cx
  0049eb33  740a                  je        short 0049EB3Fh
  0049eb35  0d00000001            or        eax,1000000h
  0049eb3a  894e18                mov       [esi+18h],ecx
  0049eb3d  8906                  mov       [esi],eax

LAB_0049eb3f:
  0049eb3f  817c24182c010000      cmp       dword [esp+18h],12Ch
  0049eb47  7d09                  jge       short 0049EB52h
  0049eb49  c6464300              mov       byte [esi+43h],0
  0049eb4d  e976010000            jmp       0049ECC8h

LAB_0049eb52:
  0049eb52  8a4643                mov       al,[esi+43h]
  0049eb55  84c0                  test      al,al
  0049eb57  7405                  je        short 0049EB5Eh
  0049eb59  fec8                  dec       al
  0049eb5b  884643                mov       [esi+43h],al

LAB_0049eb5e:
  0049eb5e  894e5c                mov       [esi+5Ch],ecx
  0049eb61  e962010000            jmp       0049ECC8h
  0049eb66  b901000000            mov       ecx,1
  0049eb6b  66394e18              cmp       [esi+18h],cx
  0049eb6f  740a                  je        short 0049EB7Bh
  0049eb71  0d00000001            or        eax,1000000h
  0049eb76  894e18                mov       [esi+18h],ecx
  0049eb79  8906                  mov       [esi],eax

LAB_0049eb7b:
  0049eb7b  8b442440              mov       eax,[esp+40h]
  0049eb7f  8b4c2414              mov       ecx,[esp+14h]
  0049eb83  83c020                add       eax,20h
  0049eb86  8d1408                lea       edx,[eax+ecx]
  0049eb89  8d3c00                lea       edi,[eax+eax]
  0049eb8c  81e2ff0f0000          and       edx,0FFFh
  0049eb92  3bd7                  cmp       edx,edi
  0049eb94  7f14                  jg        short 0049EBAAh
  0049eb96  66014e32              add       [esi+32h],cx
  0049eb9a  c6464300              mov       byte [esi+43h],0
  0049eb9e  c7465c00000000        mov       dword [esi+5Ch],0
  0049eba5  e91e010000            jmp       0049ECC8h

LAB_0049ebaa:
  0049ebaa  81e1ff0f0000          and       ecx,0FFFh
  0049ebb0  81f900080000          cmp       ecx,800h
  0049ebb6  7c02                  jl        short 0049EBBAh
  0049ebb8  f7d8                  neg       eax

LAB_0049ebba:
  0049ebba  66014632              add       [esi+32h],ax
  0049ebbe  c7465c00000000        mov       dword [esi+5Ch],0
  0049ebc5  e9fe000000            jmp       0049ECC8h
  0049ebca  b901000000            mov       ecx,1
  0049ebcf  66394e18              cmp       [esi+18h],cx
  0049ebd3  740a                  je        short 0049EBDFh
  0049ebd5  0d00000001            or        eax,1000000h
  0049ebda  894e18                mov       [esi+18h],ecx
  0049ebdd  8906                  mov       [esi],eax

LAB_0049ebdf:
  0049ebdf  0fbf4610              movsx     eax,word [esi+10h]
  0049ebe3  39442418              cmp       [esp+18h],eax
  0049ebe7  7d09                  jge       short 0049EBF2h
  0049ebe9  c6464300              mov       byte [esi+43h],0
  0049ebed  e9d6000000            jmp       0049ECC8h

LAB_0049ebf2:
  0049ebf2  8a4643                mov       al,[esi+43h]
  0049ebf5  84c0                  test      al,al
  0049ebf7  7405                  je        short 0049EBFEh
  0049ebf9  fec8                  dec       al
  0049ebfb  884643                mov       [esi+43h],al

LAB_0049ebfe:
  0049ebfe  8b542440              mov       edx,[esp+40h]
  0049ec02  8b4c2414              mov       ecx,[esp+14h]
  0049ec06  8d4240                lea       eax,[edx+40h]
  0049ec09  8d3c08                lea       edi,[eax+ecx]
  0049ec0c  8d1c00                lea       ebx,[eax+eax]
  0049ec0f  81e7ff0f0000          and       edi,0FFFh
  0049ec15  3bfb                  cmp       edi,ebx
  0049ec17  7f15                  jg        short 0049EC2Eh
  0049ec19  66014e32              add       [esi+32h],cx
  0049ec1d  81c2e0000000          add       edx,0E0h
  0049ec23  c1e207                shl       edx,7
  0049ec26  89565c                mov       [esi+5Ch],edx
  0049ec29  e99a000000            jmp       0049ECC8h

LAB_0049ec2e:
  0049ec2e  81e1ff0f0000          and       ecx,0FFFh
  0049ec34  81f900080000          cmp       ecx,800h
  0049ec3a  7c02                  jl        short 0049EC3Eh
  0049ec3c  f7d8                  neg       eax

LAB_0049ec3e:
  0049ec3e  66014632              add       [esi+32h],ax
  0049ec42  81c2e0000000          add       edx,0E0h
  0049ec48  c1e207                shl       edx,7
  0049ec4b  89565c                mov       [esi+5Ch],edx
  0049ec4e  eb78                  jmp       short 0049ECC8h
  0049ec50  8a4e43                mov       cl,[esi+43h]
  0049ec53  84c9                  test      cl,cl
  0049ec55  7405                  je        short 0049EC5Ch
  0049ec57  fec9                  dec       cl
  0049ec59  884e43                mov       [esi+43h],cl

LAB_0049ec5c:
  0049ec5c  b901000000            mov       ecx,1
  0049ec61  66394e18              cmp       [esi+18h],cx
  0049ec65  740a                  je        short 0049EC71h
  0049ec67  0d00000001            or        eax,1000000h
  0049ec6c  894e18                mov       [esi+18h],ecx
  0049ec6f  8906                  mov       [esi],eax

LAB_0049ec71:
  0049ec71  8b4c2440              mov       ecx,[esp+40h]
  0049ec75  668b4610              mov       ax,[esi+10h]
  0049ec79  8d91e0000000          lea       edx,[ecx+0E0h]
  0049ec7f  c1e207                shl       edx,7
  0049ec82  89565c                mov       [esi+5Ch],edx
  0049ec85  668b5632              mov       dx,[esi+32h]
  0049ec89  2bc2                  sub       eax,edx
  0049ec8b  25ff0f0000            and       eax,0FFFh
  0049ec90  8db800010000          lea       edi,[eax+100h]
  0049ec96  81e7ff0f0000          and       edi,0FFFh
  0049ec9c  81ff00020000          cmp       edi,200h
  0049eca2  7e24                  jle       short 0049ECC8h
  0049eca4  25ff0f0000            and       eax,0FFFh
  0049eca9  3d00080000            cmp       eax,800h
  0049ecae  7d09                  jge       short 0049ECB9h
  0049ecb0  d1f9                  sar       ecx,1
  0049ecb2  83c120                add       ecx,20h
  0049ecb5  8bc1                  mov       eax,ecx
  0049ecb7  eb09                  jmp       short 0049ECC2h

LAB_0049ecb9:
  0049ecb9  d1f9                  sar       ecx,1
  0049ecbb  b8e0ffffff            mov       eax,0FFFFFFE0h
  0049ecc0  2bc1                  sub       eax,ecx

LAB_0049ecc2:
  0049ecc2  03d0                  add       edx,eax
  0049ecc4  66895632              mov       [esi+32h],dx

LAB_0049ecc8:
  0049ecc8  8a4e40                mov       cl,[esi+40h]
  0049eccb  f6c101                test      cl,1
  0049ecce  0f84aa000000          je        near 0049ED7Eh
  0049ecd4  8b466c                mov       eax,[esi+6Ch]
  0049ecd7  8b5644                mov       edx,[esi+44h]
  0049ecda  03501c                add       edx,[eax+1Ch]
  0049ecdd  89561c                mov       [esi+1Ch],edx
  0049ece0  8b5648                mov       edx,[esi+48h]
  0049ece3  035020                add       edx,[eax+20h]
  0049ece6  895620                mov       [esi+20h],edx
  0049ece9  8b564c                mov       edx,[esi+4Ch]
  0049ecec  035024                add       edx,[eax+24h]
  0049ecef  895624                mov       [esi+24h],edx
  0049ecf2  8b10                  mov       edx,[eax]
  0049ecf4  f7c200000100          test      edx,10000h
  0049ecfa  7409                  je        short 0049ED05h
  0049ecfc  33c0                  xor       eax,eax
  0049ecfe  a038e55d00            mov       al,[5DE538h]
  0049ed03  eb07                  jmp       short 0049ED0Ch

LAB_0049ed05:
  0049ed05  33d2                  xor       edx,edx
  0049ed07  8a5008                mov       dl,[eax+8]
  0049ed0a  8bc2                  mov       eax,edx

LAB_0049ed0c:
  0049ed0c  8d14c0                lea       edx,[eax+eax*8]
  0049ed0f  8d0450                lea       eax,[eax+edx*2]
  0049ed12  33d2                  xor       edx,edx
  0049ed14  8a148588915e00        mov       dl,[eax*4+5E9188h]
  0049ed1b  8bc2                  mov       eax,edx
  0049ed1d  c1e004                shl       eax,4
  0049ed20  05204a6000            add       eax,604A20h
  0049ed25  f64002f0              test      byte [eax+2],0F0h
  0049ed29  7461                  je        short 0049ED8Ch
  0049ed2b  66ff4e10              dec       word [esi+10h]
  0049ed2f  33db                  xor       ebx,ebx
  0049ed31  66395e10              cmp       [esi+10h],bx
  0049ed35  7557                  jne       short 0049ED8Eh
  0049ed37  0fbf4632              movsx     eax,word [esi+32h]
  0049ed3b  8b16                  mov       edx,[esi]
  0049ed3d  0500080000            add       eax,800h
  0049ed42  83e2fd                and       edx,0FFFFFFFDh
  0049ed45  80e1fe                and       cl,0FEh
  0049ed48  50                    push      eax
  0049ed49  8916                  mov       [esi],edx
  0049ed4b  885e43                mov       [esi+43h],bl
  0049ed4e  884e40                mov       [esi+40h],cl
  0049ed51  e8c66c0a00            call      00545A1Ch
  0049ed56  8b4e50                mov       ecx,[esi+50h]
  0049ed59  c1e005                shl       eax,5
  0049ed5c  03c8                  add       ecx,eax
  0049ed5e  894e50                mov       [esi+50h],ecx
  0049ed61  0fbf4e32              movsx     ecx,word [esi+32h]
  0049ed65  81c100080000          add       ecx,800h
  0049ed6b  51                    push      ecx
  0049ed6c  e8a56c0a00            call      00545A16h
  0049ed71  8b4e58                mov       ecx,[esi+58h]
  0049ed74  c1e005                shl       eax,5
  0049ed77  03c8                  add       ecx,eax
  0049ed79  894e58                mov       [esi+58h],ecx
  0049ed7c  eb10                  jmp       short 0049ED8Eh

LAB_0049ed7e:
  0049ed7e  8b54241c              mov       edx,[esp+1Ch]
  0049ed82  52                    push      edx
  0049ed83  56                    push      esi
  0049ed84  e8d793feff            call      00488160h
  0049ed89  83c408                add       esp,8

LAB_0049ed8c:
  0049ed8c  33db                  xor       ebx,ebx

LAB_0049ed8e:
  0049ed8e  56                    push      esi
  0049ed8f  e8ac4b0900            call      00533940h
  0049ed94  8b06                  mov       eax,[esi]
  0049ed96  83c404                add       esp,4
  0049ed99  8bc8                  mov       ecx,eax
  0049ed9b  83e101                and       ecx,1
  0049ed9e  740a                  je        short 0049EDAAh
  0049eda0  8b5660                mov       edx,[esi+60h]
  0049eda3  8b7e20                mov       edi,[esi+20h]
  0049eda6  3bfa                  cmp       edi,edx
  0049eda8  7e14                  jle       short 0049EDBEh

LAB_0049edaa:
  0049edaa  3bcb                  cmp       ecx,ebx
  0049edac  753f                  jne       short 0049EDEDh
  0049edae  8b7e20                mov       edi,[esi+20h]
  0049edb1  8b5660                mov       edx,[esi+60h]
  0049edb4  81ef00c00000          sub       edi,0C000h
  0049edba  3bfa                  cmp       edi,edx
  0049edbc  7f27                  jg        short 0049EDE5h

LAB_0049edbe:
  0049edbe  f7869000000000a00000  test      dword [esi+90h],0A000h
  0049edc8  751b                  jne       short 0049EDE5h
  0049edca  3bcb                  cmp       ecx,ebx
  0049edcc  895620                mov       [esi+20h],edx
  0049edcf  740b                  je        short 0049EDDCh
  0049edd1  8a4e43                mov       cl,[esi+43h]
  0049edd4  84c9                  test      cl,cl
  0049edd6  7404                  je        short 0049EDDCh
  0049edd8  c6464300              mov       byte [esi+43h],0

LAB_0049eddc:
  0049eddc  24fe                  and       al,0FEh
  0049edde  895e54                mov       [esi+54h],ebx
  0049ede1  8906                  mov       [esi],eax
  0049ede3  eb08                  jmp       short 0049EDEDh

LAB_0049ede5:
  0049ede5  3bcb                  cmp       ecx,ebx
  0049ede7  7504                  jne       short 0049EDEDh
  0049ede9  0c01                  or        al,1
  0049edeb  8906                  mov       [esi],eax

LAB_0049eded:
  0049eded  8a4640                mov       al,[esi+40h]
  0049edf0  a801                  test      al,1
  0049edf2  7411                  je        short 0049EE05h
  0049edf4  8b4e6c                mov       ecx,[esi+6Ch]
  0049edf7  f70100200050          test      dword [ecx],50002000h
  0049edfd  7512                  jne       short 0049EE11h
  0049edff  66395912              cmp       [ecx+12h],bx
  0049ee03  750c                  jne       short 0049EE11h

LAB_0049ee05:
  0049ee05  f7869000000082ad0020  test      dword [esi+90h],2000AD82h
  0049ee0f  7417                  je        short 0049EE28h

LAB_0049ee11:
  0049ee11  8b16                  mov       edx,[esi]
  0049ee13  c6464201              mov       byte [esi+42h],1
  0049ee17  83e2fd                and       edx,0FFFFFFFDh
  0049ee1a  24fe                  and       al,0FEh
  0049ee1c  8916                  mov       [esi],edx
  0049ee1e  c6464301              mov       byte [esi+43h],1
  0049ee22  884640                mov       [esi+40h],al
  0049ee25  895e5c                mov       [esi+5Ch],ebx

LAB_0049ee28:
  0049ee28  66399e96000000        cmp       [esi+96h],bx
  0049ee2f  741b                  je        short 0049EE4Ch
  0049ee31  8b06                  mov       eax,[esi]
  0049ee33  a840                  test      al,40h
  0049ee35  740a                  je        short 0049EE41h
  0049ee37  a843                  test      al,43h
  0049ee39  7511                  jne       short 0049EE4Ch
  0049ee3b  c6464300              mov       byte [esi+43h],0
  0049ee3f  eb0b                  jmp       short 0049EE4Ch

LAB_0049ee41:
  0049ee41  c6464200              mov       byte [esi+42h],0
  0049ee45  c6464301              mov       byte [esi+43h],1
  0049ee49  895e5c                mov       [esi+5Ch],ebx

LAB_0049ee4c:
  0049ee4c  395c241c              cmp       [esp+1Ch],ebx
  0049ee50  0f844e020000          je        near 0049F0A4h
  0049ee56  f70617080000          test      dword [esi],817h
  0049ee5c  0f8542020000          jne       near 0049F0A4h
  0049ee62  8a4643                mov       al,[esi+43h]
  0049ee65  84c0                  test      al,al
  0049ee67  0f8537020000          jne       near 0049F0A4h
  0049ee6d  f6464001              test      byte [esi+40h],1
  0049ee71  0f852d020000          jne       near 0049F0A4h
  0049ee77  8b466c                mov       eax,[esi+6Ch]
  0049ee7a  8b6e1c                mov       ebp,[esi+1Ch]
  0049ee7d  8b4e24                mov       ecx,[esi+24h]
  0049ee80  8b781c                mov       edi,[eax+1Ch]
  0049ee83  2bfd                  sub       edi,ebp
  0049ee85  8b6824                mov       ebp,[eax+24h]
  0049ee88  2be9                  sub       ebp,ecx
  0049ee8a  c1ff0c                sar       edi,0Ch
  0049ee8d  c1fd0c                sar       ebp,0Ch
  0049ee90  8bc7                  mov       eax,edi
  0049ee92  8bcd                  mov       ecx,ebp
  0049ee94  0fafc7                imul      eax,edi
  0049ee97  0fafcd                imul      ecx,ebp
  0049ee9a  03c1                  add       eax,ecx
  0049ee9c  50                    push      eax
  0049ee9d  e86e6b0a00            call      00545A10h
  0049eea2  55                    push      ebp
  0049eea3  57                    push      edi
  0049eea4  89442420              mov       [esp+20h],eax
  0049eea8  e8756b0a00            call      00545A22h
  0049eead  668b5632              mov       dx,[esi+32h]
  0049eeb1  2bc2                  sub       eax,edx
  0049eeb3  25ff0f0000            and       eax,0FFFh
  0049eeb8  89442414              mov       [esp+14h],eax
  0049eebc  8b442418              mov       eax,[esp+18h]
  0049eec0  3dfa000000            cmp       eax,0FAh
  0049eec5  0f8dc6000000          jge       near 0049EF91h
  0049eecb  e870820a00            call      00547140h
  0049eed0  99                    cdq
  0049eed1  b964000000            mov       ecx,64h
  0049eed6  f7f9                  idiv      ecx
  0049eed8  83fa3c                cmp       edx,3Ch
  0049eedb  0f8da9000000          jge       near 0049EF8Ah
  0049eee1  8b542414              mov       edx,[esp+14h]
  0049eee5  8b7c2410              mov       edi,[esp+10h]
  0049eee9  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0049eef0  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049eef7  25ff0f0000            and       eax,0FFFh
  0049eefc  3bc1                  cmp       eax,ecx
  0049eefe  7f3e                  jg        short 0049EF3Eh
  0049ef00  e83b820a00            call      00547140h
  0049ef05  99                    cdq
  0049ef06  8d4c3f01              lea       ecx,[edi+edi+1]
  0049ef0a  f7f9                  idiv      ecx
  0049ef0c  8b442418              mov       eax,[esp+18h]
  0049ef10  83c264                add       edx,64h
  0049ef13  3bc2                  cmp       eax,edx
  0049ef15  7d27                  jge       short 0049EF3Eh
  0049ef17  8b16                  mov       edx,[esi]
  0049ef19  8a4640                mov       al,[esi+40h]
  0049ef1c  81e2fffffffe          and       edx,0FEFFFFFFh
  0049ef22  c7461803000000        mov       dword [esi+18h],3
  0049ef29  83ca02                or        edx,2
  0049ef2c  24fe                  and       al,0FEh
  0049ef2e  8916                  mov       [esi],edx
  0049ef30  884640                mov       [esi+40h],al
  0049ef33  895e5c                mov       [esi+5Ch],ebx
  0049ef36  5f                    pop       edi
  0049ef37  5e                    pop       esi
  0049ef38  5d                    pop       ebp
  0049ef39  5b                    pop       ebx
  0049ef3a  83c42c                add       esp,2Ch
  0049ef3d  c3                    ret

LAB_0049ef3e:
  0049ef3e  c6464203              mov       byte [esi+42h],3
  0049ef42  e8f9810a00            call      00547140h
  0049ef47  99                    cdq
  0049ef48  b90a000000            mov       ecx,0Ah
  0049ef4d  f7f9                  idiv      ecx
  0049ef4f  8b4c2440              mov       ecx,[esp+40h]
  0049ef53  8bc7                  mov       eax,edi
  0049ef55  d1f8                  sar       eax,1
  0049ef57  8db980000000          lea       edi,[ecx+80h]
  0049ef5d  c1e704                shl       edi,4
  0049ef60  02d0                  add       dl,al
  0049ef62  80c214                add       dl,14h
  0049ef65  885643                mov       [esi+43h],dl
  0049ef68  e8d3810a00            call      00547140h
  0049ef6d  99                    cdq
  0049ef6e  f7ff                  idiv      edi
  0049ef70  8b4c2414              mov       ecx,[esp+14h]
  0049ef74  d1ff                  sar       edi,1
  0049ef76  2bd7                  sub       edx,edi
  0049ef78  03d1                  add       edx,ecx
  0049ef7a  66035632              add       dx,[esi+32h]
  0049ef7e  66895610              mov       [esi+10h],dx
  0049ef82  5f                    pop       edi
  0049ef83  5e                    pop       esi
  0049ef84  5d                    pop       ebp
  0049ef85  5b                    pop       ebx
  0049ef86  83c42c                add       esp,2Ch
  0049ef89  c3                    ret

LAB_0049ef8a:
  0049ef8a  83fa50                cmp       edx,50h
  0049ef8d  7d51                  jge       short 0049EFE0h
  0049ef8f  eb1d                  jmp       short 0049EFAEh

LAB_0049ef91:
  0049ef91  3df4010000            cmp       eax,1F4h
  0049ef96  0f8d92000000          jge       near 0049F02Eh
  0049ef9c  e89f810a00            call      00547140h
  0049efa1  99                    cdq
  0049efa2  b964000000            mov       ecx,64h
  0049efa7  f7f9                  idiv      ecx
  0049efa9  83fa32                cmp       edx,32h
  0049efac  7d2d                  jge       short 0049EFDBh

LAB_0049efae:
  0049efae  c6464202              mov       byte [esi+42h],2
  0049efb2  e889810a00            call      00547140h
  0049efb7  99                    cdq
  0049efb8  b90a000000            mov       ecx,0Ah
  0049efbd  66c746105a00          mov       word [esi+10h],5Ah
  0049efc3  f7f9                  idiv      ecx
  0049efc5  8b442410              mov       eax,[esp+10h]
  0049efc9  d1f8                  sar       eax,1
  0049efcb  02d0                  add       dl,al
  0049efcd  80c214                add       dl,14h
  0049efd0  885643                mov       [esi+43h],dl
  0049efd3  5f                    pop       edi
  0049efd4  5e                    pop       esi
  0049efd5  5d                    pop       ebp
  0049efd6  5b                    pop       ebx
  0049efd7  83c42c                add       esp,2Ch
  0049efda  c3                    ret

LAB_0049efdb:
  0049efdb  83fa50                cmp       edx,50h
  0049efde  7d60                  jge       short 0049F040h

LAB_0049efe0:
  0049efe0  c6464203              mov       byte [esi+42h],3
  0049efe4  e857810a00            call      00547140h
  0049efe9  99                    cdq
  0049efea  b90a000000            mov       ecx,0Ah
  0049efef  f7f9                  idiv      ecx
  0049eff1  8b442410              mov       eax,[esp+10h]
  0049eff5  8b4c2440              mov       ecx,[esp+40h]
  0049eff9  d1f8                  sar       eax,1
  0049effb  8db980000000          lea       edi,[ecx+80h]
  0049f001  c1e704                shl       edi,4
  0049f004  02d0                  add       dl,al
  0049f006  80c214                add       dl,14h
  0049f009  885643                mov       [esi+43h],dl
  0049f00c  e82f810a00            call      00547140h
  0049f011  99                    cdq
  0049f012  f7ff                  idiv      edi
  0049f014  8b4c2414              mov       ecx,[esp+14h]
  0049f018  d1ff                  sar       edi,1
  0049f01a  2bd7                  sub       edx,edi
  0049f01c  03d1                  add       edx,ecx
  0049f01e  66035632              add       dx,[esi+32h]
  0049f022  66895610              mov       [esi+10h],dx
  0049f026  5f                    pop       edi
  0049f027  5e                    pop       esi
  0049f028  5d                    pop       ebp
  0049f029  5b                    pop       ebx
  0049f02a  83c42c                add       esp,2Ch
  0049f02d  c3                    ret

LAB_0049f02e:
  0049f02e  e80d810a00            call      00547140h
  0049f033  99                    cdq
  0049f034  b964000000            mov       ecx,64h
  0049f039  f7f9                  idiv      ecx
  0049f03b  83fa1e                cmp       edx,1Eh
  0049f03e  7d27                  jge       short 0049F067h

LAB_0049f040:
  0049f040  c6464200              mov       byte [esi+42h],0
  0049f044  e8f7800a00            call      00547140h
  0049f049  99                    cdq
  0049f04a  b90a000000            mov       ecx,0Ah
  0049f04f  f7f9                  idiv      ecx
  0049f051  8b442410              mov       eax,[esp+10h]
  0049f055  d1f8                  sar       eax,1
  0049f057  02d0                  add       dl,al
  0049f059  80c214                add       dl,14h
  0049f05c  885643                mov       [esi+43h],dl
  0049f05f  5f                    pop       edi
  0049f060  5e                    pop       esi
  0049f061  5d                    pop       ebp
  0049f062  5b                    pop       ebx
  0049f063  83c42c                add       esp,2Ch
  0049f066  c3                    ret

LAB_0049f067:
  0049f067  83fa3c                cmp       edx,3Ch
  0049f06a  7d10                  jge       short 0049F07Ch
  0049f06c  c6464201              mov       byte [esi+42h],1
  0049f070  c6464301              mov       byte [esi+43h],1
  0049f074  5f                    pop       edi
  0049f075  5e                    pop       esi
  0049f076  5d                    pop       ebp
  0049f077  5b                    pop       ebx
  0049f078  83c42c                add       esp,2Ch
  0049f07b  c3                    ret

LAB_0049f07c:
  0049f07c  c6464203              mov       byte [esi+42h],3
  0049f080  e8bb800a00            call      00547140h
  0049f085  99                    cdq
  0049f086  b90a000000            mov       ecx,0Ah
  0049f08b  f7f9                  idiv      ecx
  0049f08d  8b442410              mov       eax,[esp+10h]
  0049f091  d1f8                  sar       eax,1
  0049f093  02d0                  add       dl,al
  0049f095  80c214                add       dl,14h
  0049f098  885643                mov       [esi+43h],dl
  0049f09b  e8a0800a00            call      00547140h
  0049f0a0  66894610              mov       [esi+10h],ax

LAB_0049f0a4:
  0049f0a4  5f                    pop       edi
  0049f0a5  5e                    pop       esi
  0049f0a6  5d                    pop       ebp
  0049f0a7  5b                    pop       ebx
  0049f0a8  83c42c                add       esp,2Ch
  0049f0ab  c3                    ret

48 cible(s) de saut interne : 0x0049e9de, 0x0049e9fd, 0x0049ea9f, 0x0049eadf, 0x0049eae5, 0x0049eb05, 0x0049eb3f, 0x0049eb52, 0x0049eb5e, 0x0049eb7b, 0x0049ebaa, 0x0049ebba, 0x0049ebdf, 0x0049ebf2, 0x0049ebfe, 0x0049ec2e, 0x0049ec3e, 0x0049ec5c, 0x0049ec71, 0x0049ecb9, 0x0049ecc2, 0x0049ecc8, 0x0049ed05, 0x0049ed0c, 0x0049ed7e, 0x0049ed8c, 0x0049ed8e, 0x0049edaa, 0x0049edbe, 0x0049eddc, 0x0049ede5, 0x0049eded, 0x0049ee05, 0x0049ee11, 0x0049ee28, 0x0049ee41, 0x0049ee4c, 0x0049ef3e, 0x0049ef8a, 0x0049ef91, 0x0049efae, 0x0049efdb, 0x0049efe0, 0x0049f02e, 0x0049f040, 0x0049f067, 0x0049f07c, 0x0049f0a4
