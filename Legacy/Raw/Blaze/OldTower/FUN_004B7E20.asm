Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004b7e20  section .text  offset fichier 0xb7220  1824 octets

  004b7e20  83ec1c                sub       esp,1Ch
  004b7e23  8d442410              lea       eax,[esp+10h]
  004b7e27  8d4c2414              lea       ecx,[esp+14h]
  004b7e2b  8d54240c              lea       edx,[esp+0Ch]
  004b7e2f  55                    push      ebp
  004b7e30  56                    push      esi
  004b7e31  8b742428              mov       esi,[esp+28h]
  004b7e35  57                    push      edi
  004b7e36  50                    push      eax
  004b7e37  51                    push      ecx
  004b7e38  52                    push      edx
  004b7e39  56                    push      esi
  004b7e3a  e8410b0800            call      00538980h
  004b7e3f  83c410                add       esp,10h
  004b7e42  8d442414              lea       eax,[esp+14h]
  004b7e46  8d4c2410              lea       ecx,[esp+10h]
  004b7e4a  8d54242c              lea       edx,[esp+2Ch]
  004b7e4e  50                    push      eax
  004b7e4f  51                    push      ecx
  004b7e50  8d442414              lea       eax,[esp+14h]
  004b7e54  52                    push      edx
  004b7e55  8d4c2430              lea       ecx,[esp+30h]
  004b7e59  50                    push      eax
  004b7e5a  51                    push      ecx
  004b7e5b  56                    push      esi
  004b7e5c  e8bfe00700            call      00535F20h
  004b7e61  8b442428              mov       eax,[esp+28h]
  004b7e65  33ed                  xor       ebp,ebp
  004b7e67  83c418                add       esp,18h
  004b7e6a  3bc5                  cmp       eax,ebp
  004b7e6c  0f849a040000          je        near 004B830Ch
  004b7e72  56                    push      esi
  004b7e73  e828370200            call      004DB5A0h
  004b7e78  8b06                  mov       eax,[esi]
  004b7e7a  83c404                add       esp,4
  004b7e7d  a802                  test      al,2
  004b7e7f  0f8454010000          je        near 004B7FD9h
  004b7e85  f644241410            test      byte [esp+14h],10h
  004b7e8a  740d                  je        short 004B7E99h
  004b7e8c  24fd                  and       al,0FDh
  004b7e8e  c6464300              mov       byte [esi+43h],0
  004b7e92  8906                  mov       [esi],eax
  004b7e94  e973040000            jmp       004B830Ch

LAB_004b7e99:
  004b7e99  0fbf4610              movsx     eax,word [esi+10h]
  004b7e9d  2bc5                  sub       eax,ebp
  004b7e9f  7476                  je        short 004B7F17h
  004b7ea1  48                    dec       eax
  004b7ea2  0f8564040000          jne       near 004B830Ch
  004b7ea8  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004b7ead  896e5c                mov       [esi+5Ch],ebp
  004b7eb0  7544                  jne       short 004B7EF6h
  004b7eb2  0fbf5632              movsx     edx,word [esi+32h]
  004b7eb6  52                    push      edx
  004b7eb7  c7465c00200200        mov       dword [esi+5Ch],22000h
  004b7ebe  e859db0800            call      00545A1Ch
  004b7ec3  0faf465c              imul      eax,[esi+5Ch]
  004b7ec7  8b4e50                mov       ecx,[esi+50h]
  004b7eca  c1f80c                sar       eax,0Ch
  004b7ecd  03c8                  add       ecx,eax
  004b7ecf  0fbf4632              movsx     eax,word [esi+32h]
  004b7ed3  50                    push      eax
  004b7ed4  894e50                mov       [esi+50h],ecx
  004b7ed7  e83adb0800            call      00545A16h
  004b7edc  0faf465c              imul      eax,[esi+5Ch]
  004b7ee0  8b4e58                mov       ecx,[esi+58h]
  004b7ee3  c7465c00500000        mov       dword [esi+5Ch],5000h
  004b7eea  c1f80c                sar       eax,0Ch
  004b7eed  03c8                  add       ecx,eax
  004b7eef  66ff4e10              dec       word [esi+10h]
  004b7ef3  894e58                mov       [esi+58h],ecx

LAB_004b7ef6:
  004b7ef6  8b44242c              mov       eax,[esp+2Ch]
  004b7efa  8d8880000000          lea       ecx,[eax+80h]
  004b7f00  81e1ff0f0000          and       ecx,0FFFh
  004b7f06  81f900010000          cmp       ecx,100h
  004b7f0c  0f8ef6030000          jle       near 004B8308h
  004b7f12  e9d6030000            jmp       004B82EDh

LAB_004b7f17:
  004b7f17  8b4650                mov       eax,[esi+50h]
  004b7f1a  8b4e58                mov       ecx,[esi+58h]
  004b7f1d  c1f80c                sar       eax,0Ch
  004b7f20  8bd0                  mov       edx,eax
  004b7f22  c1f90c                sar       ecx,0Ch
  004b7f25  0fafd0                imul      edx,eax
  004b7f28  8bc1                  mov       eax,ecx
  004b7f2a  0fafc1                imul      eax,ecx
  004b7f2d  03d0                  add       edx,eax
  004b7f2f  52                    push      edx
  004b7f30  e8dbda0800            call      00545A10h
  004b7f35  8b0e                  mov       ecx,[esi]
  004b7f37  f7c100000002          test      ecx,2000000h
  004b7f3d  740a                  je        short 004B7F49h
  004b7f3f  83f808                cmp       eax,8
  004b7f42  7f05                  jg        short 004B7F49h
  004b7f44  83e1fd                and       ecx,0FFFFFFFDh
  004b7f47  890e                  mov       [esi],ecx

LAB_004b7f49:
  004b7f49  83f803                cmp       eax,3
  004b7f4c  0f8cba030000          jl        near 004B830Ch
  004b7f52  8b44242c              mov       eax,[esp+2Ch]
  004b7f56  8d4828                lea       ecx,[eax+28h]
  004b7f59  81e1ff0f0000          and       ecx,0FFFh
  004b7f5f  83f950                cmp       ecx,50h
  004b7f62  7e18                  jle       short 004B7F7Ch
  004b7f64  25ff0f0000            and       eax,0FFFh
  004b7f69  33d2                  xor       edx,edx
  004b7f6b  3d00080000            cmp       eax,800h
  004b7f70  0f9dc2                setge     dl
  004b7f73  4a                    dec       edx
  004b7f74  83e250                and       edx,50h
  004b7f77  83c2d8                add       edx,0FFFFFFD8h
  004b7f7a  8bc2                  mov       eax,edx

LAB_004b7f7c:
  004b7f7c  66014632              add       [esi+32h],ax
  004b7f80  55                    push      ebp
  004b7f81  8d461c                lea       eax,[esi+1Ch]
  004b7f84  68382b5500            push      552B38h
  004b7f89  50                    push      eax
  004b7f8a  56                    push      esi
  004b7f8b  e8a01c0600            call      00519C30h
  004b7f90  8bf8                  mov       edi,eax
  004b7f92  a1b8e05d00            mov       eax,[5DE0B8h]
  004b7f97  83c410                add       esp,10h
  004b7f9a  3bc5                  cmp       eax,ebp
  004b7f9c  0f846a030000          je        near 004B830Ch
  004b7fa2  8b0f                  mov       ecx,[edi]
  004b7fa4  57                    push      edi
  004b7fa5  83c940                or        ecx,40h
  004b7fa8  56                    push      esi
  004b7fa9  66c787960000001000    mov       word [edi+96h],10h
  004b7fb2  890f                  mov       [edi],ecx
  004b7fb4  e8c71b0800            call      00539B80h
  004b7fb9  83c408                add       esp,8
  004b7fbc  83c71c                add       edi,1Ch
  004b7fbf  57                    push      edi
  004b7fc0  6800180000            push      1800h
  004b7fc5  6800030000            push      300h
  004b7fca  6a03                  push      3
  004b7fcc  e84f5b0800            call      0053DB20h
  004b7fd1  83c410                add       esp,10h
  004b7fd4  e933030000            jmp       004B830Ch

LAB_004b7fd9:
  004b7fd9  a810                  test      al,10h
  004b7fdb  0f852b030000          jne       near 004B830Ch
  004b7fe1  a904080000            test      eax,804h
  004b7fe6  0f8446010000          je        near 004B8132h
  004b7fec  f7442414e8030040      test      dword [esp+14h],400003E8h
  004b7ff4  740b                  je        short 004B8001h
  004b7ff6  25fbf7ffff            and       eax,0FFFFF7FBh
  004b7ffb  c6464300              mov       byte [esi+43h],0
  004b7fff  8906                  mov       [esi],eax

LAB_004b8001:
  004b8001  668b4610              mov       ax,[esi+10h]
  004b8005  0fbfc8                movsx     ecx,ax
  004b8008  83f903                cmp       ecx,3
  004b800b  0f87fb020000          ja        near 004B830Ch
  004b8011  ff248d1c854b00        jmp       dword [ecx*4+4B851Ch]
  004b8018  8b06                  mov       eax,[esi]
  004b801a  a900000002            test      eax,2000000h
  004b801f  0f84e7020000          je        near 004B830Ch
  004b8025  80e4f7                and       ah,0F7h
  004b8028  8906                  mov       [esi],eax
  004b802a  e9dd020000            jmp       004B830Ch
  004b802f  b805000000            mov       eax,5
  004b8034  66394618              cmp       [esi+18h],ax
  004b8038  7411                  je        short 004B804Bh
  004b803a  894618                mov       [esi+18h],eax
  004b803d  8b06                  mov       eax,[esi]
  004b803f  25fbfffffe            and       eax,0FEFFFFFBh
  004b8044  8906                  mov       [esi],eax
  004b8046  e9c1020000            jmp       004B830Ch

LAB_004b804b:
  004b804b  66837e1a18            cmp       word [esi+1Ah],18h
  004b8050  0f85b6020000          jne       near 004B830Ch
  004b8056  33c0                  xor       eax,eax
  004b8058  6aff                  push      0FFFFFFFFh
  004b805a  668b4606              mov       ax,[esi+6]
  004b805e  6aff                  push      0FFFFFFFFh
  004b8060  8bc8                  mov       ecx,eax
  004b8062  c1f808                sar       eax,8
  004b8065  81e1ff000000          and       ecx,0FFh
  004b806b  25ff000000            and       eax,0FFh
  004b8070  51                    push      ecx
  004b8071  50                    push      eax
  004b8072  56                    push      esi
  004b8073  e808290800            call      0053A980h
  004b8078  83c414                add       esp,14h
  004b807b  66ff4e10              dec       word [esi+10h]
  004b807f  e988020000            jmp       004B830Ch
  004b8084  8a4e43                mov       cl,[esi+43h]
  004b8087  fec9                  dec       cl
  004b8089  884e43                mov       [esi+43h],cl
  004b808c  7505                  jne       short 004B8093h
  004b808e  48                    dec       eax
  004b808f  66894610              mov       [esi+10h],ax

LAB_004b8093:
  004b8093  8b44242c              mov       eax,[esp+2Ch]
  004b8097  8d9080000000          lea       edx,[eax+80h]
  004b809d  81e2ff0f0000          and       edx,0FFFh
  004b80a3  81fa00010000          cmp       edx,100h
  004b80a9  0f8e59020000          jle       near 004B8308h
  004b80af  25ff0f0000            and       eax,0FFFh
  004b80b4  33c9                  xor       ecx,ecx
  004b80b6  3d00080000            cmp       eax,800h
  004b80bb  0f9dc1                setge     cl
  004b80be  49                    dec       ecx
  004b80bf  81e100010000          and       ecx,100h
  004b80c5  83c180                add       ecx,0FFFFFF80h
  004b80c8  8bc1                  mov       eax,ecx
  004b80ca  e939020000            jmp       004B8308h
  004b80cf  55                    push      ebp
  004b80d0  55                    push      ebp
  004b80d1  68bd000000            push      0BDh
  004b80d6  56                    push      esi
  004b80d7  e8042afcff            call      0047AAE0h
  004b80dc  83c410                add       esp,10h
  004b80df  e85cf00800            call      00547140h
  004b80e4  99                    cdq
  004b80e5  b909000000            mov       ecx,9
  004b80ea  f7f9                  idiv      ecx
  004b80ec  33c0                  xor       eax,eax
  004b80ee  668b0455482b5500      mov       ax,[edx*2+552B48h]
  004b80f6  66894606              mov       [esi+6],ax
  004b80fa  8b0dc4975e00          mov       ecx,[5E97C4h]
  004b8100  8bd0                  mov       edx,eax
  004b8102  25ff000000            and       eax,0FFh
  004b8107  8b899c000000          mov       ecx,[ecx+9Ch]
  004b810d  c1fa08                sar       edx,8
  004b8110  81e2ff000000          and       edx,0FFh
  004b8116  8d0440                lea       eax,[eax+eax*2]
  004b8119  c1e004                shl       eax,4
  004b811c  8b0c91                mov       ecx,[ecx+edx*4]
  004b811f  8a540113              mov       dl,[ecx+eax+13h]
  004b8123  66ff4e10              dec       word [esi+10h]
  004b8127  885643                mov       [esi+43h],dl
  004b812a  896e5c                mov       [esi+5Ch],ebp
  004b812d  e9da010000            jmp       004B830Ch

LAB_004b8132:
  004b8132  33c9                  xor       ecx,ecx
  004b8134  8a4e42                mov       cl,[esi+42h]
  004b8137  83f903                cmp       ecx,3
  004b813a  0f87cc010000          ja        near 004B830Ch
  004b8140  ff248d2c854b00        jmp       dword [ecx*4+4B852Ch]
  004b8147  66396e18              cmp       [esi+18h],bp
  004b814b  740a                  je        short 004B8157h
  004b814d  0d00000001            or        eax,1000000h
  004b8152  896e18                mov       [esi+18h],ebp
  004b8155  8906                  mov       [esi],eax

LAB_004b8157:
  004b8157  817c240c2c010000      cmp       dword [esp+0Ch],12Ch
  004b815f  7d09                  jge       short 004B816Ah
  004b8161  c6464300              mov       byte [esi+43h],0
  004b8165  e9a2010000            jmp       004B830Ch

LAB_004b816a:
  004b816a  8a4643                mov       al,[esi+43h]
  004b816d  84c0                  test      al,al
  004b816f  7405                  je        short 004B8176h
  004b8171  fec8                  dec       al
  004b8173  884643                mov       [esi+43h],al

LAB_004b8176:
  004b8176  8b465c                mov       eax,[esi+5Ch]
  004b8179  8bc8                  mov       ecx,eax
  004b817b  c1f904                sar       ecx,4
  004b817e  2bc1                  sub       eax,ecx
  004b8180  89465c                mov       [esi+5Ch],eax
  004b8183  e984010000            jmp       004B830Ch
  004b8188  66396e18              cmp       [esi+18h],bp
  004b818c  740a                  je        short 004B8198h
  004b818e  0d00000001            or        eax,1000000h
  004b8193  896e18                mov       [esi+18h],ebp
  004b8196  8906                  mov       [esi],eax

LAB_004b8198:
  004b8198  8b44242c              mov       eax,[esp+2Ch]
  004b819c  8d9080000000          lea       edx,[eax+80h]
  004b81a2  81e2ff0f0000          and       edx,0FFFh
  004b81a8  81fa00010000          cmp       edx,100h
  004b81ae  7f1a                  jg        short 004B81CAh
  004b81b0  66014632              add       [esi+32h],ax
  004b81b4  8b465c                mov       eax,[esi+5Ch]
  004b81b7  8bd0                  mov       edx,eax
  004b81b9  c6464300              mov       byte [esi+43h],0
  004b81bd  c1fa04                sar       edx,4
  004b81c0  2bc2                  sub       eax,edx
  004b81c2  89465c                mov       [esi+5Ch],eax
  004b81c5  e942010000            jmp       004B830Ch

LAB_004b81ca:
  004b81ca  25ff0f0000            and       eax,0FFFh
  004b81cf  33c9                  xor       ecx,ecx
  004b81d1  3d00080000            cmp       eax,800h
  004b81d6  0f9dc1                setge     cl
  004b81d9  49                    dec       ecx
  004b81da  81e100010000          and       ecx,100h
  004b81e0  83c180                add       ecx,0FFFFFF80h
  004b81e3  8bc1                  mov       eax,ecx
  004b81e5  66014632              add       [esi+32h],ax
  004b81e9  8b465c                mov       eax,[esi+5Ch]
  004b81ec  8bd0                  mov       edx,eax
  004b81ee  c1fa04                sar       edx,4
  004b81f1  2bc2                  sub       eax,edx
  004b81f3  89465c                mov       [esi+5Ch],eax
  004b81f6  e911010000            jmp       004B830Ch
  004b81fb  b901000000            mov       ecx,1
  004b8200  66394e18              cmp       [esi+18h],cx
  004b8204  740a                  je        short 004B8210h
  004b8206  0d00000001            or        eax,1000000h
  004b820b  894e18                mov       [esi+18h],ecx
  004b820e  8906                  mov       [esi],eax

LAB_004b8210:
  004b8210  817c240c2c010000      cmp       dword [esp+0Ch],12Ch
  004b8218  7d09                  jge       short 004B8223h
  004b821a  c6464300              mov       byte [esi+43h],0
  004b821e  e9e9000000            jmp       004B830Ch

LAB_004b8223:
  004b8223  8a4643                mov       al,[esi+43h]
  004b8226  84c0                  test      al,al
  004b8228  7405                  je        short 004B822Fh
  004b822a  fec8                  dec       al
  004b822c  884643                mov       [esi+43h],al

LAB_004b822f:
  004b822f  8b44242c              mov       eax,[esp+2Ch]
  004b8233  8d8880000000          lea       ecx,[eax+80h]
  004b8239  81e1ff0f0000          and       ecx,0FFFh
  004b823f  81f900010000          cmp       ecx,100h
  004b8245  7e1b                  jle       short 004B8262h
  004b8247  25ff0f0000            and       eax,0FFFh
  004b824c  33d2                  xor       edx,edx
  004b824e  3d00080000            cmp       eax,800h
  004b8253  0f9dc2                setge     dl
  004b8256  4a                    dec       edx
  004b8257  81e200010000          and       edx,100h
  004b825d  83c280                add       edx,0FFFFFF80h
  004b8260  8bc2                  mov       eax,edx

LAB_004b8262:
  004b8262  66014632              add       [esi+32h],ax
  004b8266  8b465c                mov       eax,[esi+5Ch]
  004b8269  b900000100            mov       ecx,10000h
  004b826e  2bc8                  sub       ecx,eax
  004b8270  c1f904                sar       ecx,4
  004b8273  03c8                  add       ecx,eax
  004b8275  894e5c                mov       [esi+5Ch],ecx
  004b8278  e98f000000            jmp       004B830Ch
  004b827d  66396e18              cmp       [esi+18h],bp
  004b8281  740a                  je        short 004B828Dh
  004b8283  0d00000001            or        eax,1000000h
  004b8288  896e18                mov       [esi+18h],ebp
  004b828b  8906                  mov       [esi],eax

LAB_004b828d:
  004b828d  8a4643                mov       al,[esi+43h]
  004b8290  84c0                  test      al,al
  004b8292  7405                  je        short 004B8299h
  004b8294  fec8                  dec       al
  004b8296  884643                mov       [esi+43h],al

LAB_004b8299:
  004b8299  0fbf5610              movsx     edx,word [esi+10h]
  004b829d  52                    push      edx
  004b829e  c7465c00400000        mov       dword [esi+5Ch],4000h
  004b82a5  e872d70800            call      00545A1Ch
  004b82aa  0faf465c              imul      eax,[esi+5Ch]
  004b82ae  8b4e50                mov       ecx,[esi+50h]
  004b82b1  c1f80c                sar       eax,0Ch
  004b82b4  03c8                  add       ecx,eax
  004b82b6  0fbf4610              movsx     eax,word [esi+10h]
  004b82ba  50                    push      eax
  004b82bb  894e50                mov       [esi+50h],ecx
  004b82be  e853d70800            call      00545A16h
  004b82c3  0faf465c              imul      eax,[esi+5Ch]
  004b82c7  8b4e58                mov       ecx,[esi+58h]
  004b82ca  896e5c                mov       [esi+5Ch],ebp
  004b82cd  c1f80c                sar       eax,0Ch
  004b82d0  03c8                  add       ecx,eax
  004b82d2  8b44242c              mov       eax,[esp+2Ch]
  004b82d6  894e58                mov       [esi+58h],ecx
  004b82d9  8d8880000000          lea       ecx,[eax+80h]
  004b82df  81e1ff0f0000          and       ecx,0FFFh
  004b82e5  81f900010000          cmp       ecx,100h
  004b82eb  7e1b                  jle       short 004B8308h

LAB_004b82ed:
  004b82ed  25ff0f0000            and       eax,0FFFh
  004b82f2  33d2                  xor       edx,edx
  004b82f4  3d00080000            cmp       eax,800h
  004b82f9  0f9dc2                setge     dl
  004b82fc  4a                    dec       edx
  004b82fd  81e200010000          and       edx,100h
  004b8303  83c280                add       edx,0FFFFFF80h
  004b8306  8bc2                  mov       eax,edx

LAB_004b8308:
  004b8308  66014632              add       [esi+32h],ax

LAB_004b830c:
  004b830c  8b442410              mov       eax,[esp+10h]
  004b8310  50                    push      eax
  004b8311  56                    push      esi
  004b8312  e8c925feff            call      0049A8E0h
  004b8317  83c408                add       esp,8
  004b831a  56                    push      esi
  004b831b  e820b60700            call      00533940h
  004b8320  8b9690000000          mov       edx,[esi+90h]
  004b8326  8b466c                mov       eax,[esi+6Ch]
  004b8329  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  004b832f  83c404                add       esp,4
  004b8332  899690000000          mov       [esi+90h],edx
  004b8338  8a08                  mov       cl,[eax]
  004b833a  f6c101                test      cl,1
  004b833d  7506                  jne       short 004B8345h
  004b833f  8b4860                mov       ecx,[eax+60h]
  004b8342  894e48                mov       [esi+48h],ecx

LAB_004b8345:
  004b8345  e8f6ed0800            call      00547140h
  004b834a  8b7e60                mov       edi,[esi+60h]
  004b834d  25ff000000            and       eax,0FFh
  004b8352  0500010000            add       eax,100h
  004b8357  66014614              add       [esi+14h],ax
  004b835b  8b4648                mov       eax,[esi+48h]
  004b835e  668b4e14              mov       cx,[esi+14h]
  004b8362  3bc7                  cmp       eax,edi
  004b8364  7c02                  jl        short 004B8368h
  004b8366  8bf8                  mov       edi,eax

LAB_004b8368:
  004b8368  f60602                test      byte [esi],2
  004b836b  7415                  je        short 004B8382h
  004b836d  8b4620                mov       eax,[esi+20h]
  004b8370  2bf8                  sub       edi,eax
  004b8372  81c700800200          add       edi,28000h
  004b8378  c1ff03                sar       edi,3
  004b837b  03f8                  add       edi,eax
  004b837d  897e20                mov       [esi+20h],edi
  004b8380  eb22                  jmp       short 004B83A4h

LAB_004b8382:
  004b8382  8b6e20                mov       ebp,[esi+20h]
  004b8385  0fbfd1                movsx     edx,cx
  004b8388  52                    push      edx
  004b8389  e88ed60800            call      00545A1Ch
  004b838e  0500410000            add       eax,4100h
  004b8393  c1e005                shl       eax,5
  004b8396  03c7                  add       eax,edi
  004b8398  2bc5                  sub       eax,ebp
  004b839a  c1f804                sar       eax,4
  004b839d  03c5                  add       eax,ebp
  004b839f  33ed                  xor       ebp,ebp
  004b83a1  894620                mov       [esi+20h],eax

LAB_004b83a4:
  004b83a4  8b442418              mov       eax,[esp+18h]
  004b83a8  50                    push      eax
  004b83a9  56                    push      esi
  004b83aa  e84199feff            call      004A1CF0h
  004b83af  8b442418              mov       eax,[esp+18h]
  004b83b3  83c408                add       esp,8
  004b83b6  3bc5                  cmp       eax,ebp
  004b83b8  0f8456010000          je        near 004B8514h
  004b83be  f70616080000          test      dword [esi],816h
  004b83c4  0f854a010000          jne       near 004B8514h
  004b83ca  8a4643                mov       al,[esi+43h]
  004b83cd  84c0                  test      al,al
  004b83cf  0f853f010000          jne       near 004B8514h
  004b83d5  8b466c                mov       eax,[esi+6Ch]
  004b83d8  8b4e1c                mov       ecx,[esi+1Ch]
  004b83db  8b5624                mov       edx,[esi+24h]
  004b83de  8b781c                mov       edi,[eax+1Ch]
  004b83e1  8b6824                mov       ebp,[eax+24h]
  004b83e4  2bf9                  sub       edi,ecx
  004b83e6  2bea                  sub       ebp,edx
  004b83e8  c1ff0c                sar       edi,0Ch
  004b83eb  c1fd0c                sar       ebp,0Ch
  004b83ee  8bcf                  mov       ecx,edi
  004b83f0  8bd5                  mov       edx,ebp
  004b83f2  0fafcf                imul      ecx,edi
  004b83f5  0fafd5                imul      edx,ebp
  004b83f8  03ca                  add       ecx,edx
  004b83fa  51                    push      ecx
  004b83fb  e810d60800            call      00545A10h
  004b8400  55                    push      ebp
  004b8401  57                    push      edi
  004b8402  89442414              mov       [esp+14h],eax
  004b8406  e817d60800            call      00545A22h
  004b840b  668b4e32              mov       cx,[esi+32h]
  004b840f  2bc1                  sub       eax,ecx
  004b8411  25ff0f0000            and       eax,0FFFh
  004b8416  8944242c              mov       [esp+2Ch],eax
  004b841a  e821ed0800            call      00547140h
  004b841f  99                    cdq
  004b8420  b964000000            mov       ecx,64h
  004b8425  f7f9                  idiv      ecx
  004b8427  817c240c2c010000      cmp       dword [esp+0Ch],12Ch
  004b842f  7d40                  jge       short 004B8471h
  004b8431  83fa3c                cmp       edx,3Ch
  004b8434  0f8da3000000          jge       near 004B84DDh
  004b843a  8b54242c              mov       edx,[esp+2Ch]
  004b843e  81c200010000          add       edx,100h
  004b8444  81e2ff0f0000          and       edx,0FFFh
  004b844a  81fa00020000          cmp       edx,200h
  004b8450  7f59                  jg        short 004B84ABh
  004b8452  8b06                  mov       eax,[esi]
  004b8454  c7461803000000        mov       dword [esi+18h],3
  004b845b  25fffffffe            and       eax,0FEFFFFFFh
  004b8460  66c746100100          mov       word [esi+10h],1
  004b8466  0c02                  or        al,2
  004b8468  8906                  mov       [esi],eax
  004b846a  5f                    pop       edi
  004b846b  5e                    pop       esi
  004b846c  5d                    pop       ebp
  004b846d  83c41c                add       esp,1Ch
  004b8470  c3                    ret

LAB_004b8471:
  004b8471  83fa1e                cmp       edx,1Eh
  004b8474  7d44                  jge       short 004B84BAh
  004b8476  8b54242c              mov       edx,[esp+2Ch]
  004b847a  81c200010000          add       edx,100h
  004b8480  81e2ff0f0000          and       edx,0FFFh
  004b8486  81fa00020000          cmp       edx,200h
  004b848c  7f1d                  jg        short 004B84ABh
  004b848e  8b06                  mov       eax,[esi]
  004b8490  c7461804000000        mov       dword [esi+18h],4
  004b8497  0d04080001            or        eax,1000804h
  004b849c  66c746100300          mov       word [esi+10h],3
  004b84a2  8906                  mov       [esi],eax
  004b84a4  5f                    pop       edi
  004b84a5  5e                    pop       esi
  004b84a6  5d                    pop       ebp
  004b84a7  83c41c                add       esp,1Ch
  004b84aa  c3                    ret

LAB_004b84ab:
  004b84ab  c6464201              mov       byte [esi+42h],1
  004b84af  c6464301              mov       byte [esi+43h],1
  004b84b3  5f                    pop       edi
  004b84b4  5e                    pop       esi
  004b84b5  5d                    pop       ebp
  004b84b6  83c41c                add       esp,1Ch
  004b84b9  c3                    ret

LAB_004b84ba:
  004b84ba  83fa5a                cmp       edx,5Ah
  004b84bd  7d1e                  jge       short 004B84DDh
  004b84bf  c6464202              mov       byte [esi+42h],2
  004b84c3  e878ec0800            call      00547140h
  004b84c8  99                    cdq
  004b84c9  b90a000000            mov       ecx,0Ah
  004b84ce  f7f9                  idiv      ecx
  004b84d0  80c214                add       dl,14h
  004b84d3  885643                mov       [esi+43h],dl
  004b84d6  5f                    pop       edi
  004b84d7  5e                    pop       esi
  004b84d8  5d                    pop       ebp
  004b84d9  83c41c                add       esp,1Ch
  004b84dc  c3                    ret

LAB_004b84dd:
  004b84dd  c6464203              mov       byte [esi+42h],3
  004b84e1  e85aec0800            call      00547140h
  004b84e6  99                    cdq
  004b84e7  b90a000000            mov       ecx,0Ah
  004b84ec  f7f9                  idiv      ecx
  004b84ee  02d1                  add       dl,cl
  004b84f0  885643                mov       [esi+43h],dl
  004b84f3  e848ec0800            call      00547140h
  004b84f8  99                    cdq
  004b84f9  b9000c0000            mov       ecx,0C00h
  004b84fe  8b7c242c              mov       edi,[esp+2Ch]
  004b8502  f7f9                  idiv      ecx
  004b8504  03d7                  add       edx,edi
  004b8506  66035632              add       dx,[esi+32h]
  004b850a  81ea00060000          sub       edx,600h
  004b8510  66895610              mov       [esi+10h],dx

LAB_004b8514:
  004b8514  5f                    pop       edi
  004b8515  5e                    pop       esi
  004b8516  5d                    pop       ebp
  004b8517  83c41c                add       esp,1Ch
  004b851a  c3                    ret
  004b851b  90                    nop
  004b851c  18804b002f80          sbb       [eax-7FD0FFB5h],al
  004b8522  4b                    dec       ebx
  004b8523  0084804b00cf80        add       [eax+eax*4-7F30FFB5h],al
  004b852a  4b                    dec       ebx
  004b852b  004781                add       [edi-7Fh],al
  004b852e  4b                    dec       ebx
  004b852f  0088814b00fb          add       [eax-4FFB47Fh],cl
  004b8535  814b007d824b00        or        dword [ebx],4B827Dh
  004b853c  90                    nop
  004b853d  90                    nop
  004b853e  90                    nop
  004b853f  90                    nop

33 cible(s) de saut interne : 0x004b7e99, 0x004b7ef6, 0x004b7f17, 0x004b7f49, 0x004b7f7c, 0x004b7fd9, 0x004b8001, 0x004b804b, 0x004b8093, 0x004b8132, 0x004b8157, 0x004b816a, 0x004b8176, 0x004b8198, 0x004b81ca, 0x004b8210, 0x004b8223, 0x004b822f, 0x004b8262, 0x004b828d, 0x004b8299, 0x004b82ed, 0x004b8308, 0x004b830c, 0x004b8345, 0x004b8368, 0x004b8382, 0x004b83a4, 0x004b8471, 0x004b84ab, 0x004b84ba, 0x004b84dd, 0x004b8514
