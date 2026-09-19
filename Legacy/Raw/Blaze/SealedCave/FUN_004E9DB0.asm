Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e9db0  section .text  offset fichier 0xe91b0  1664 octets

  004e9db0  83ec2c                sub       esp,2Ch
  004e9db3  53                    push      ebx
  004e9db4  55                    push      ebp
  004e9db5  56                    push      esi
  004e9db6  8b74243c              mov       esi,[esp+3Ch]
  004e9dba  8d442424              lea       eax,[esp+24h]
  004e9dbe  57                    push      edi
  004e9dbf  8d4c242c              lea       ecx,[esp+2Ch]
  004e9dc3  50                    push      eax
  004e9dc4  8d542424              lea       edx,[esp+24h]
  004e9dc8  51                    push      ecx
  004e9dc9  33db                  xor       ebx,ebx
  004e9dcb  52                    push      edx
  004e9dcc  56                    push      esi
  004e9dcd  66895c2440            mov       [esp+40h],bx
  004e9dd2  66c7442442c4ff        mov       word [esp+42h],0FFC4h
  004e9dd9  66c74424443c00        mov       word [esp+44h],3Ch
  004e9de0  66c74424469600        mov       word [esp+46h],96h
  004e9de7  66895c2448            mov       [esp+48h],bx
  004e9dec  66c744244a7800        mov       word [esp+4Ah],78h
  004e9df3  e888eb0400            call      00538980h
  004e9df8  83c410                add       esp,10h
  004e9dfb  8d442424              lea       eax,[esp+24h]
  004e9dff  8d4c241c              lea       ecx,[esp+1Ch]
  004e9e03  8d542414              lea       edx,[esp+14h]
  004e9e07  50                    push      eax
  004e9e08  51                    push      ecx
  004e9e09  8d442418              lea       eax,[esp+18h]
  004e9e0d  52                    push      edx
  004e9e0e  8d4c244c              lea       ecx,[esp+4Ch]
  004e9e12  50                    push      eax
  004e9e13  51                    push      ecx
  004e9e14  56                    push      esi
  004e9e15  e806c10400            call      00535F20h
  004e9e1a  8b4c2458              mov       ecx,[esp+58h]
  004e9e1e  b840000000            mov       eax,40h
  004e9e23  2bc1                  sub       eax,ecx
  004e9e25  83c418                add       esp,18h
  004e9e28  89442418              mov       [esp+18h],eax
  004e9e2c  8b44241c              mov       eax,[esp+1Ch]
  004e9e30  3bc3                  cmp       eax,ebx
  004e9e32  0f8415030000          je        near 004EA14Dh
  004e9e38  56                    push      esi
  004e9e39  e86217ffff            call      004DB5A0h
  004e9e3e  8b06                  mov       eax,[esi]
  004e9e40  83c404                add       esp,4
  004e9e43  a802                  test      al,2
  004e9e45  0f841f010000          je        near 004E9F6Ah
  004e9e4b  f644242410            test      byte [esp+24h],10h
  004e9e50  740c                  je        short 004E9E5Eh
  004e9e52  24fd                  and       al,0FDh
  004e9e54  885e43                mov       [esi+43h],bl
  004e9e57  8906                  mov       [esi],eax
  004e9e59  e9ef020000            jmp       004EA14Dh

LAB_004e9e5e:
  004e9e5e  8d542430              lea       edx,[esp+30h]
  004e9e62  53                    push      ebx
  004e9e63  8d461c                lea       eax,[esi+1Ch]
  004e9e66  52                    push      edx
  004e9e67  50                    push      eax
  004e9e68  56                    push      esi
  004e9e69  e8c2fd0200            call      00519C30h
  004e9e6e  8bf8                  mov       edi,eax
  004e9e70  a1b8e05d00            mov       eax,[5DE0B8h]
  004e9e75  83c410                add       esp,10h
  004e9e78  3bc3                  cmp       eax,ebx
  004e9e7a  7432                  je        short 004E9EAEh
  004e9e7c  8b17                  mov       edx,[edi]
  004e9e7e  57                    push      edi
  004e9e7f  83ca40                or        edx,40h
  004e9e82  56                    push      esi
  004e9e83  66c787960000001000    mov       word [edi+96h],10h
  004e9e8c  8917                  mov       [edi],edx
  004e9e8e  e8edfc0400            call      00539B80h
  004e9e93  83c408                add       esp,8
  004e9e96  83c71c                add       edi,1Ch
  004e9e99  57                    push      edi
  004e9e9a  6800180000            push      1800h
  004e9e9f  6800030000            push      300h
  004e9ea4  6a03                  push      3
  004e9ea6  e8753c0500            call      0053DB20h
  004e9eab  83c410                add       esp,10h

LAB_004e9eae:
  004e9eae  8b4c2440              mov       ecx,[esp+40h]
  004e9eb2  8b465c                mov       eax,[esi+5Ch]
  004e9eb5  81c180020000          add       ecx,280h
  004e9ebb  c1e107                shl       ecx,7
  004e9ebe  2bc8                  sub       ecx,eax
  004e9ec0  c1f904                sar       ecx,4
  004e9ec3  03c8                  add       ecx,eax
  004e9ec5  668b4628              mov       ax,[esi+28h]
  004e9ec9  894e5c                mov       [esi+5Ch],ecx
  004e9ecc  8a4e43                mov       cl,[esi+43h]
  004e9ecf  80f908                cmp       cl,8
  004e9ed2  0fbfd0                movsx     edx,ax
  004e9ed5  7235                  jb        short 004E9F0Ch
  004e9ed7  bf00300000            mov       edi,3000h
  004e9edc  2bfa                  sub       edi,edx
  004e9ede  c1ff02                sar       edi,2
  004e9ee1  03f8                  add       edi,eax
  004e9ee3  668b462a              mov       ax,[esi+2Ah]
  004e9ee7  0fbfd0                movsx     edx,ax
  004e9eea  66897e28              mov       [esi+28h],di
  004e9eee  bf00300000            mov       edi,3000h
  004e9ef3  2bfa                  sub       edi,edx
  004e9ef5  c1ff02                sar       edi,2
  004e9ef8  03f8                  add       edi,eax
  004e9efa  668b462c              mov       ax,[esi+2Ch]
  004e9efe  66897e2a              mov       [esi+2Ah],di
  004e9f02  bf00300000            mov       edi,3000h
  004e9f07  0fbfd0                movsx     edx,ax
  004e9f0a  eb33                  jmp       short 004E9F3Fh

LAB_004e9f0c:
  004e9f0c  bf00100000            mov       edi,1000h
  004e9f11  2bfa                  sub       edi,edx
  004e9f13  c1ff02                sar       edi,2
  004e9f16  03f8                  add       edi,eax
  004e9f18  668b462a              mov       ax,[esi+2Ah]
  004e9f1c  0fbfd0                movsx     edx,ax
  004e9f1f  66897e28              mov       [esi+28h],di
  004e9f23  bf00100000            mov       edi,1000h
  004e9f28  2bfa                  sub       edi,edx
  004e9f2a  c1ff02                sar       edi,2
  004e9f2d  03f8                  add       edi,eax
  004e9f2f  668b462c              mov       ax,[esi+2Ch]
  004e9f33  66897e2a              mov       [esi+2Ah],di
  004e9f37  bf00100000            mov       edi,1000h
  004e9f3c  0fbfd0                movsx     edx,ax

LAB_004e9f3f:
  004e9f3f  2bfa                  sub       edi,edx
  004e9f41  c1ff02                sar       edi,2
  004e9f44  03f8                  add       edi,eax
  004e9f46  84c9                  test      cl,cl
  004e9f48  66897e2c              mov       [esi+2Ch],di
  004e9f4c  0f84fb010000          je        near 004EA14Dh
  004e9f52  8ac1                  mov       al,cl
  004e9f54  fec8                  dec       al
  004e9f56  884643                mov       [esi+43h],al
  004e9f59  0f85ee010000          jne       near 004EA14Dh
  004e9f5f  8b06                  mov       eax,[esi]
  004e9f61  24fd                  and       al,0FDh
  004e9f63  8906                  mov       [esi],eax
  004e9f65  e9e3010000            jmp       004EA14Dh

LAB_004e9f6a:
  004e9f6a  a810                  test      al,10h
  004e9f6c  0f85db010000          jne       near 004EA14Dh
  004e9f72  a904080000            test      eax,804h
  004e9f77  0f85d0010000          jne       near 004EA14Dh
  004e9f7d  33c0                  xor       eax,eax
  004e9f7f  8a4642                mov       al,[esi+42h]
  004e9f82  83f803                cmp       eax,3
  004e9f85  0f8776010000          ja        near 004EA101h
  004e9f8b  ff248514a44e00        jmp       dword [eax*4+4EA414h]
  004e9f92  817c24102c010000      cmp       dword [esp+10h],12Ch
  004e9f9a  7d09                  jge       short 004E9FA5h
  004e9f9c  c6464300              mov       byte [esi+43h],0
  004e9fa0  e95c010000            jmp       004EA101h

LAB_004e9fa5:
  004e9fa5  8a4643                mov       al,[esi+43h]
  004e9fa8  84c0                  test      al,al
  004e9faa  7463                  je        short 004EA00Fh
  004e9fac  fec8                  dec       al
  004e9fae  884643                mov       [esi+43h],al
  004e9fb1  8b465c                mov       eax,[esi+5Ch]
  004e9fb4  8bc8                  mov       ecx,eax
  004e9fb6  c1f904                sar       ecx,4
  004e9fb9  2bc1                  sub       eax,ecx
  004e9fbb  89465c                mov       [esi+5Ch],eax
  004e9fbe  e93e010000            jmp       004EA101h
  004e9fc3  8b442440              mov       eax,[esp+40h]
  004e9fc7  8b4c2414              mov       ecx,[esp+14h]
  004e9fcb  c1f802                sar       eax,2
  004e9fce  83c038                add       eax,38h
  004e9fd1  8d1408                lea       edx,[eax+ecx]
  004e9fd4  8d3c00                lea       edi,[eax+eax]
  004e9fd7  81e2ff0f0000          and       edx,0FFFh
  004e9fdd  3bd7                  cmp       edx,edi
  004e9fdf  7f1a                  jg        short 004E9FFBh
  004e9fe1  8b465c                mov       eax,[esi+5Ch]
  004e9fe4  66014e32              add       [esi+32h],cx
  004e9fe8  8bc8                  mov       ecx,eax
  004e9fea  c6464300              mov       byte [esi+43h],0
  004e9fee  c1f904                sar       ecx,4
  004e9ff1  2bc1                  sub       eax,ecx
  004e9ff3  89465c                mov       [esi+5Ch],eax
  004e9ff6  e906010000            jmp       004EA101h

LAB_004e9ffb:
  004e9ffb  81e1ff0f0000          and       ecx,0FFFh
  004ea001  81f900080000          cmp       ecx,800h
  004ea007  7c02                  jl        short 004EA00Bh
  004ea009  f7d8                  neg       eax

LAB_004ea00b:
  004ea00b  66014632              add       [esi+32h],ax

LAB_004ea00f:
  004ea00f  8b465c                mov       eax,[esi+5Ch]
  004ea012  8bc8                  mov       ecx,eax
  004ea014  c1f904                sar       ecx,4
  004ea017  2bc1                  sub       eax,ecx
  004ea019  89465c                mov       [esi+5Ch],eax
  004ea01c  e9e0000000            jmp       004EA101h
  004ea021  817c24102c010000      cmp       dword [esp+10h],12Ch
  004ea029  7d09                  jge       short 004EA034h
  004ea02b  c6464300              mov       byte [esi+43h],0
  004ea02f  e9cd000000            jmp       004EA101h

LAB_004ea034:
  004ea034  8a4643                mov       al,[esi+43h]
  004ea037  84c0                  test      al,al
  004ea039  7405                  je        short 004EA040h
  004ea03b  fec8                  dec       al
  004ea03d  884643                mov       [esi+43h],al

LAB_004ea040:
  004ea040  8b542440              mov       edx,[esp+40h]
  004ea044  8b4c2414              mov       ecx,[esp+14h]
  004ea048  8bc2                  mov       eax,edx
  004ea04a  c1f802                sar       eax,2
  004ea04d  83c038                add       eax,38h
  004ea050  8d3c08                lea       edi,[eax+ecx]
  004ea053  8d1c00                lea       ebx,[eax+eax]
  004ea056  81e7ff0f0000          and       edi,0FFFh
  004ea05c  3bfb                  cmp       edi,ebx
  004ea05e  7f06                  jg        short 004EA066h
  004ea060  66014e32              add       [esi+32h],cx
  004ea064  eb14                  jmp       short 004EA07Ah

LAB_004ea066:
  004ea066  81e1ff0f0000          and       ecx,0FFFh
  004ea06c  81f900080000          cmp       ecx,800h
  004ea072  7c02                  jl        short 004EA076h
  004ea074  f7d8                  neg       eax

LAB_004ea076:
  004ea076  66014632              add       [esi+32h],ax

LAB_004ea07a:
  004ea07a  8b465c                mov       eax,[esi+5Ch]
  004ea07d  81c2a0010000          add       edx,1A0h
  004ea083  c1e207                shl       edx,7
  004ea086  2bd0                  sub       edx,eax
  004ea088  c1fa04                sar       edx,4
  004ea08b  03d0                  add       edx,eax
  004ea08d  89565c                mov       [esi+5Ch],edx
  004ea090  eb6f                  jmp       short 004EA101h
  004ea092  8a4643                mov       al,[esi+43h]
  004ea095  84c0                  test      al,al
  004ea097  7405                  je        short 004EA09Eh
  004ea099  fec8                  dec       al
  004ea09b  884643                mov       [esi+43h],al

LAB_004ea09e:
  004ea09e  8b4c2440              mov       ecx,[esp+40h]
  004ea0a2  8b465c                mov       eax,[esi+5Ch]
  004ea0a5  8d9140010000          lea       edx,[ecx+140h]
  004ea0ab  c1e207                shl       edx,7
  004ea0ae  2bd0                  sub       edx,eax
  004ea0b0  c1fa04                sar       edx,4
  004ea0b3  03d0                  add       edx,eax
  004ea0b5  668b4610              mov       ax,[esi+10h]
  004ea0b9  89565c                mov       [esi+5Ch],edx
  004ea0bc  668b5632              mov       dx,[esi+32h]
  004ea0c0  2bc2                  sub       eax,edx
  004ea0c2  25ff0f0000            and       eax,0FFFh
  004ea0c7  8db800010000          lea       edi,[eax+100h]
  004ea0cd  81e7ff0f0000          and       edi,0FFFh
  004ea0d3  81ff00020000          cmp       edi,200h
  004ea0d9  7e26                  jle       short 004EA101h
  004ea0db  25ff0f0000            and       eax,0FFFh
  004ea0e0  3d00080000            cmp       eax,800h
  004ea0e5  7d0a                  jge       short 004EA0F1h
  004ea0e7  c1f902                sar       ecx,2
  004ea0ea  83c138                add       ecx,38h
  004ea0ed  8bc1                  mov       eax,ecx
  004ea0ef  eb0a                  jmp       short 004EA0FBh

LAB_004ea0f1:
  004ea0f1  c1f902                sar       ecx,2
  004ea0f4  b8c8ffffff            mov       eax,0FFFFFFC8h
  004ea0f9  2bc1                  sub       eax,ecx

LAB_004ea0fb:
  004ea0fb  03d0                  add       edx,eax
  004ea0fd  66895632              mov       [esi+32h],dx

LAB_004ea101:
  004ea101  0fbf4614              movsx     eax,word [esi+14h]
  004ea105  50                    push      eax
  004ea106  e811b90500            call      00545A1Ch
  004ea10b  668b4e28              mov       cx,[esi+28h]
  004ea10f  c1f802                sar       eax,2
  004ea112  0500100000            add       eax,1000h
  004ea117  0fbfd1                movsx     edx,cx
  004ea11a  8bf8                  mov       edi,eax
  004ea11c  2bfa                  sub       edi,edx
  004ea11e  c1ff02                sar       edi,2
  004ea121  03f9                  add       edi,ecx
  004ea123  668b4e2a              mov       cx,[esi+2Ah]
  004ea127  0fbfd1                movsx     edx,cx
  004ea12a  66897e28              mov       [esi+28h],di
  004ea12e  8bf8                  mov       edi,eax
  004ea130  2bfa                  sub       edi,edx
  004ea132  c1ff02                sar       edi,2
  004ea135  03f9                  add       edi,ecx
  004ea137  668b4e2c              mov       cx,[esi+2Ch]
  004ea13b  0fbfd1                movsx     edx,cx
  004ea13e  2bc2                  sub       eax,edx
  004ea140  66897e2a              mov       [esi+2Ah],di
  004ea144  c1f802                sar       eax,2
  004ea147  03c1                  add       eax,ecx
  004ea149  6689462c              mov       [esi+2Ch],ax

LAB_004ea14d:
  004ea14d  8b44241c              mov       eax,[esp+1Ch]
  004ea151  50                    push      eax
  004ea152  56                    push      esi
  004ea153  e88807fbff            call      0049A8E0h
  004ea158  83c408                add       esp,8
  004ea15b  56                    push      esi
  004ea15c  e8df970400            call      00533940h
  004ea161  8b466c                mov       eax,[esi+6Ch]
  004ea164  b301                  mov       bl,1
  004ea166  83c404                add       esp,4
  004ea169  8418                  test      [eax],bl
  004ea16b  7506                  jne       short 004EA173h
  004ea16d  8b4860                mov       ecx,[eax+60h]
  004ea170  894e48                mov       [esi+48h],ecx

LAB_004ea173:
  004ea173  e8c8cf0500            call      00547140h
  004ea178  8b7e60                mov       edi,[esi+60h]
  004ea17b  25ff000000            and       eax,0FFh
  004ea180  0500010000            add       eax,100h
  004ea185  66014614              add       [esi+14h],ax
  004ea189  8b4648                mov       eax,[esi+48h]
  004ea18c  668b4e14              mov       cx,[esi+14h]
  004ea190  3bc7                  cmp       eax,edi
  004ea192  7c02                  jl        short 004EA196h
  004ea194  8bf8                  mov       edi,eax

LAB_004ea196:
  004ea196  f60602                test      byte [esi],2
  004ea199  7415                  je        short 004EA1B0h
  004ea19b  8b4620                mov       eax,[esi+20h]
  004ea19e  2bf8                  sub       edi,eax
  004ea1a0  81c700800200          add       edi,28000h
  004ea1a6  c1ff03                sar       edi,3
  004ea1a9  03f8                  add       edi,eax
  004ea1ab  897e20                mov       [esi+20h],edi
  004ea1ae  eb20                  jmp       short 004EA1D0h

LAB_004ea1b0:
  004ea1b0  8b6e20                mov       ebp,[esi+20h]
  004ea1b3  0fbfd1                movsx     edx,cx
  004ea1b6  52                    push      edx
  004ea1b7  e860b80500            call      00545A1Ch
  004ea1bc  0500320000            add       eax,3200h
  004ea1c1  c1e006                shl       eax,6
  004ea1c4  03c7                  add       eax,edi
  004ea1c6  2bc5                  sub       eax,ebp
  004ea1c8  c1f804                sar       eax,4
  004ea1cb  03c5                  add       eax,ebp
  004ea1cd  894620                mov       [esi+20h],eax

LAB_004ea1d0:
  004ea1d0  0fbf4614              movsx     eax,word [esi+14h]
  004ea1d4  50                    push      eax
  004ea1d5  e842b80500            call      00545A1Ch
  004ea1da  8bc8                  mov       ecx,eax
  004ea1dc  d1f9                  sar       ecx,1
  004ea1de  03c8                  add       ecx,eax
  004ea1e0  c1f906                sar       ecx,6
  004ea1e3  81c180000000          add       ecx,80h
  004ea1e9  81e1ff000000          and       ecx,0FFh
  004ea1ef  8bd1                  mov       edx,ecx
  004ea1f1  c1e208                shl       edx,8
  004ea1f4  0bd1                  or        edx,ecx
  004ea1f6  c1e208                shl       edx,8
  004ea1f9  0bd1                  or        edx,ecx
  004ea1fb  66837e160a            cmp       word [esi+16h],0Ah
  004ea200  895638                mov       [esi+38h],edx
  004ea203  7c0d                  jl        short 004EA212h
  004ea205  8b442420              mov       eax,[esp+20h]
  004ea209  50                    push      eax
  004ea20a  56                    push      esi
  004ea20b  e86095ffff            call      004E3770h
  004ea210  eb1d                  jmp       short 004EA22Fh

LAB_004ea212:
  004ea212  8bbe90000000          mov       edi,[esi+90h]
  004ea218  8b4c2420              mov       ecx,[esp+20h]
  004ea21c  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004ea222  51                    push      ecx
  004ea223  56                    push      esi
  004ea224  89be90000000          mov       [esi+90h],edi
  004ea22a  e8c17afbff            call      004A1CF0h

LAB_004ea22f:
  004ea22f  8b442424              mov       eax,[esp+24h]
  004ea233  83c408                add       esp,8
  004ea236  85c0                  test      eax,eax
  004ea238  0f84ce010000          je        near 004EA40Ch
  004ea23e  f70616080000          test      dword [esi],816h
  004ea244  0f85c2010000          jne       near 004EA40Ch
  004ea24a  8a4643                mov       al,[esi+43h]
  004ea24d  84c0                  test      al,al
  004ea24f  0f85b7010000          jne       near 004EA40Ch
  004ea255  8b466c                mov       eax,[esi+6Ch]
  004ea258  8b561c                mov       edx,[esi+1Ch]
  004ea25b  8b781c                mov       edi,[eax+1Ch]
  004ea25e  8b6824                mov       ebp,[eax+24h]
  004ea261  8b4624                mov       eax,[esi+24h]
  004ea264  2bfa                  sub       edi,edx
  004ea266  2be8                  sub       ebp,eax
  004ea268  c1ff0c                sar       edi,0Ch
  004ea26b  c1fd0c                sar       ebp,0Ch
  004ea26e  8bd7                  mov       edx,edi
  004ea270  8bc5                  mov       eax,ebp
  004ea272  0fafd7                imul      edx,edi
  004ea275  0fafc5                imul      eax,ebp
  004ea278  03d0                  add       edx,eax
  004ea27a  52                    push      edx
  004ea27b  e890b70500            call      00545A10h
  004ea280  55                    push      ebp
  004ea281  57                    push      edi
  004ea282  89442418              mov       [esp+18h],eax
  004ea286  e897b70500            call      00545A22h
  004ea28b  668b4e32              mov       cx,[esi+32h]
  004ea28f  2bc1                  sub       eax,ecx
  004ea291  25ff0f0000            and       eax,0FFFh
  004ea296  89442414              mov       [esp+14h],eax
  004ea29a  8b442410              mov       eax,[esp+10h]
  004ea29e  3d2c010000            cmp       eax,12Ch
  004ea2a3  0f8dbc000000          jge       near 004EA365h
  004ea2a9  e892ce0500            call      00547140h
  004ea2ae  99                    cdq
  004ea2af  b964000000            mov       ecx,64h
  004ea2b4  f7f9                  idiv      ecx
  004ea2b6  83fa3c                cmp       edx,3Ch
  004ea2b9  7d5c                  jge       short 004EA317h
  004ea2bb  8b542414              mov       edx,[esp+14h]
  004ea2bf  8b7c2418              mov       edi,[esp+18h]
  004ea2c3  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004ea2ca  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004ea2d1  25ff0f0000            and       eax,0FFFh
  004ea2d6  3bc1                  cmp       eax,ecx
  004ea2d8  0f8ff8000000          jg        near 004EA3D6h
  004ea2de  e85dce0500            call      00547140h
  004ea2e3  99                    cdq
  004ea2e4  8d4c3f01              lea       ecx,[edi+edi+1]
  004ea2e8  f7f9                  idiv      ecx
  004ea2ea  8b4c2410              mov       ecx,[esp+10h]
  004ea2ee  b8fa000000            mov       eax,0FAh
  004ea2f3  2bc2                  sub       eax,edx
  004ea2f5  3bc8                  cmp       ecx,eax
  004ea2f7  0f8dd9000000          jge       near 004EA3D6h
  004ea2fd  8b1e                  mov       ebx,[esi]
  004ea2ff  8bcf                  mov       ecx,edi
  004ea301  c1f903                sar       ecx,3
  004ea304  83cb02                or        ebx,2
  004ea307  80c110                add       cl,10h
  004ea30a  891e                  mov       [esi],ebx
  004ea30c  884e43                mov       [esi+43h],cl
  004ea30f  5f                    pop       edi
  004ea310  5e                    pop       esi
  004ea311  5d                    pop       ebp
  004ea312  5b                    pop       ebx
  004ea313  83c42c                add       esp,2Ch
  004ea316  c3                    ret

LAB_004ea317:
  004ea317  c6464203              mov       byte [esi+42h],3
  004ea31b  e820ce0500            call      00547140h
  004ea320  99                    cdq
  004ea321  b90a000000            mov       ecx,0Ah
  004ea326  f7f9                  idiv      ecx
  004ea328  8b442418              mov       eax,[esp+18h]
  004ea32c  c1f802                sar       eax,2
  004ea32f  02d0                  add       dl,al
  004ea331  02d1                  add       dl,cl
  004ea333  8b4c2440              mov       ecx,[esp+40h]
  004ea337  885643                mov       [esi+43h],dl
  004ea33a  8db980000000          lea       edi,[ecx+80h]
  004ea340  c1e704                shl       edi,4
  004ea343  e8f8cd0500            call      00547140h
  004ea348  99                    cdq
  004ea349  f7ff                  idiv      edi
  004ea34b  8b4c2414              mov       ecx,[esp+14h]
  004ea34f  d1ff                  sar       edi,1
  004ea351  2bd7                  sub       edx,edi
  004ea353  03d1                  add       edx,ecx
  004ea355  66035632              add       dx,[esi+32h]
  004ea359  66895610              mov       [esi+10h],dx
  004ea35d  5f                    pop       edi
  004ea35e  5e                    pop       esi
  004ea35f  5d                    pop       ebp
  004ea360  5b                    pop       ebx
  004ea361  83c42c                add       esp,2Ch
  004ea364  c3                    ret

LAB_004ea365:
  004ea365  3d58020000            cmp       eax,258h
  004ea36a  7d2c                  jge       short 004EA398h
  004ea36c  e8cfcd0500            call      00547140h
  004ea371  c6464202              mov       byte [esi+42h],2
  004ea375  e8c6cd0500            call      00547140h
  004ea37a  99                    cdq
  004ea37b  b90a000000            mov       ecx,0Ah
  004ea380  f7f9                  idiv      ecx
  004ea382  8b442418              mov       eax,[esp+18h]
  004ea386  c1f802                sar       eax,2
  004ea389  02d0                  add       dl,al
  004ea38b  02d1                  add       dl,cl
  004ea38d  885643                mov       [esi+43h],dl
  004ea390  5f                    pop       edi
  004ea391  5e                    pop       esi
  004ea392  5d                    pop       ebp
  004ea393  5b                    pop       ebx
  004ea394  83c42c                add       esp,2Ch
  004ea397  c3                    ret

LAB_004ea398:
  004ea398  e8a3cd0500            call      00547140h
  004ea39d  99                    cdq
  004ea39e  b964000000            mov       ecx,64h
  004ea3a3  f7f9                  idiv      ecx
  004ea3a5  83fa1e                cmp       edx,1Eh
  004ea3a8  7d27                  jge       short 004EA3D1h
  004ea3aa  c6464200              mov       byte [esi+42h],0
  004ea3ae  e88dcd0500            call      00547140h
  004ea3b3  99                    cdq
  004ea3b4  b90a000000            mov       ecx,0Ah
  004ea3b9  f7f9                  idiv      ecx
  004ea3bb  8b442418              mov       eax,[esp+18h]
  004ea3bf  c1f802                sar       eax,2
  004ea3c2  02d0                  add       dl,al
  004ea3c4  02d1                  add       dl,cl
  004ea3c6  885643                mov       [esi+43h],dl
  004ea3c9  5f                    pop       edi
  004ea3ca  5e                    pop       esi
  004ea3cb  5d                    pop       ebp
  004ea3cc  5b                    pop       ebx
  004ea3cd  83c42c                add       esp,2Ch
  004ea3d0  c3                    ret

LAB_004ea3d1:
  004ea3d1  83fa3c                cmp       edx,3Ch
  004ea3d4  7d0e                  jge       short 004EA3E4h

LAB_004ea3d6:
  004ea3d6  885e42                mov       [esi+42h],bl
  004ea3d9  885e43                mov       [esi+43h],bl
  004ea3dc  5f                    pop       edi
  004ea3dd  5e                    pop       esi
  004ea3de  5d                    pop       ebp
  004ea3df  5b                    pop       ebx
  004ea3e0  83c42c                add       esp,2Ch
  004ea3e3  c3                    ret

LAB_004ea3e4:
  004ea3e4  c6464203              mov       byte [esi+42h],3
  004ea3e8  e853cd0500            call      00547140h
  004ea3ed  99                    cdq
  004ea3ee  b90a000000            mov       ecx,0Ah
  004ea3f3  f7f9                  idiv      ecx
  004ea3f5  8b442418              mov       eax,[esp+18h]
  004ea3f9  c1f802                sar       eax,2
  004ea3fc  02d0                  add       dl,al
  004ea3fe  02d1                  add       dl,cl
  004ea400  885643                mov       [esi+43h],dl
  004ea403  e838cd0500            call      00547140h
  004ea408  66894610              mov       [esi+10h],ax

LAB_004ea40c:
  004ea40c  5f                    pop       edi
  004ea40d  5e                    pop       esi
  004ea40e  5d                    pop       ebp
  004ea40f  5b                    pop       ebx
  004ea410  83c42c                add       esp,2Ch
  004ea413  c3                    ret
  004ea414  92                    xchg      edx,eax
  004ea415  9f                    lahf
  004ea416  4e                    dec       esi
  004ea417  00c3                  add       bl,al
  004ea419  9f                    lahf
  004ea41a  4e                    dec       esi
  004ea41b  0021                  add       [ecx],ah
  004ea41d  a04e0092a0            mov       al,[0A092004Eh]
  004ea422  4e                    dec       esi
  004ea423  009090909090          add       [eax-6F6F6F70h],dl
  004ea429  90                    nop
  004ea42a  90                    nop
  004ea42b  90                    nop
  004ea42c  90                    nop
  004ea42d  90                    nop
  004ea42e  90                    nop
  004ea42f  90                    nop

32 cible(s) de saut interne : 0x004e9e5e, 0x004e9eae, 0x004e9f0c, 0x004e9f3f, 0x004e9f6a, 0x004e9fa5, 0x004e9ffb, 0x004ea00b, 0x004ea00f, 0x004ea034, 0x004ea040, 0x004ea066, 0x004ea076, 0x004ea07a, 0x004ea09e, 0x004ea0f1, 0x004ea0fb, 0x004ea101, 0x004ea14d, 0x004ea173, 0x004ea196, 0x004ea1b0, 0x004ea1d0, 0x004ea212, 0x004ea22f, 0x004ea317, 0x004ea365, 0x004ea398, 0x004ea3d1, 0x004ea3d6, 0x004ea3e4, 0x004ea40c
