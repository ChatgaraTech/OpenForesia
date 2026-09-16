Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00492e40  section .text  offset fichier 0x92240  1968 octets

  00492e40  83ec2c                sub       esp,2Ch
  00492e43  b832000000            mov       eax,32h
  00492e48  53                    push      ebx
  00492e49  55                    push      ebp
  00492e4a  668944242a            mov       [esp+2Ah],ax
  00492e4f  668944242e            mov       [esp+2Eh],ax
  00492e54  56                    push      esi
  00492e55  8b74243c              mov       esi,[esp+3Ch]
  00492e59  8d442420              lea       eax,[esp+20h]
  00492e5d  57                    push      edi
  00492e5e  8d4c2428              lea       ecx,[esp+28h]
  00492e62  50                    push      eax
  00492e63  8d542430              lea       edx,[esp+30h]
  00492e67  51                    push      ecx
  00492e68  33ff                  xor       edi,edi
  00492e6a  52                    push      edx
  00492e6b  56                    push      esi
  00492e6c  66897c2440            mov       [esp+40h],di
  00492e71  66c74424442300        mov       word [esp+44h],23h
  00492e78  66897c2448            mov       [esp+48h],di
  00492e7d  66c744244a5a00        mov       word [esp+4Ah],5Ah
  00492e84  e8f75a0a00            call      00538980h
  00492e89  83c410                add       esp,10h
  00492e8c  8d442420              lea       eax,[esp+20h]
  00492e90  8d4c241c              lea       ecx,[esp+1Ch]
  00492e94  8d542414              lea       edx,[esp+14h]
  00492e98  50                    push      eax
  00492e99  51                    push      ecx
  00492e9a  8d442420              lea       eax,[esp+20h]
  00492e9e  52                    push      edx
  00492e9f  8d4c244c              lea       ecx,[esp+4Ch]
  00492ea3  50                    push      eax
  00492ea4  51                    push      ecx
  00492ea5  56                    push      esi
  00492ea6  e875300a00            call      00535F20h
  00492eab  8b4c2458              mov       ecx,[esp+58h]
  00492eaf  b840000000            mov       eax,40h
  00492eb4  2bc1                  sub       eax,ecx
  00492eb6  83c418                add       esp,18h
  00492eb9  89442410              mov       [esp+10h],eax
  00492ebd  8b44241c              mov       eax,[esp+1Ch]
  00492ec1  3bc7                  cmp       eax,edi
  00492ec3  0f841f030000          je        near 004931E8h
  00492ec9  56                    push      esi
  00492eca  e8d1860400            call      004DB5A0h
  00492ecf  8b06                  mov       eax,[esi]
  00492ed1  83c404                add       esp,4
  00492ed4  a802                  test      al,2
  00492ed6  0f8449010000          je        near 00493025h
  00492edc  8a4c2420              mov       cl,[esp+20h]
  00492ee0  bb10000000            mov       ebx,10h
  00492ee5  84cb                  test      bl,cl
  00492ee7  7415                  je        short 00492EFEh
  00492ee9  24fd                  and       al,0FDh
  00492eeb  c6464300              mov       byte [esi+43h],0
  00492eef  8906                  mov       [esi],eax
  00492ef1  8a4640                mov       al,[esi+40h]
  00492ef4  24fe                  and       al,0FEh
  00492ef6  884640                mov       [esi+40h],al
  00492ef9  e9ea020000            jmp       004931E8h

LAB_00492efe:
  00492efe  8a4640                mov       al,[esi+40h]
  00492f01  33ff                  xor       edi,edi
  00492f03  a801                  test      al,1
  00492f05  7416                  je        short 00492F1Dh
  00492f07  66ff4e14              dec       word [esi+14h]
  00492f0b  66397e14              cmp       [esi+14h],di
  00492f0f  0f85aa000000          jne       near 00492FBFh
  00492f15  8b7e6c                mov       edi,[esi+6Ch]
  00492f18  e9a2000000            jmp       00492FBFh

LAB_00492f1d:
  00492f1d  66837e1a09            cmp       word [esi+1Ah],9
  00492f22  0f8c97000000          jl        near 00492FBFh
  00492f28  8d542430              lea       edx,[esp+30h]
  00492f2c  8d6e1c                lea       ebp,[esi+1Ch]
  00492f2f  6a00                  push      0
  00492f31  52                    push      edx
  00492f32  55                    push      ebp
  00492f33  56                    push      esi
  00492f34  e8f76c0800            call      00519C30h
  00492f39  8bf8                  mov       edi,eax
  00492f3b  83c410                add       esp,10h
  00492f3e  85ff                  test      edi,edi
  00492f40  0f84bf000000          je        near 00493005h
  00492f46  8b4500                mov       eax,[ebp]
  00492f49  8b4f1c                mov       ecx,[edi+1Ch]
  00492f4c  2bc1                  sub       eax,ecx
  00492f4e  8b4e20                mov       ecx,[esi+20h]
  00492f51  8b5624                mov       edx,[esi+24h]
  00492f54  894644                mov       [esi+44h],eax
  00492f57  2b4f20                sub       ecx,[edi+20h]
  00492f5a  894e48                mov       [esi+48h],ecx
  00492f5d  8b6f24                mov       ebp,[edi+24h]
  00492f60  2bd5                  sub       edx,ebp
  00492f62  89564c                mov       [esi+4Ch],edx
  00492f65  e8d6410b00            call      00547140h
  00492f6a  8b4c2440              mov       ecx,[esp+40h]
  00492f6e  83e007                and       eax,7
  00492f71  c1f902                sar       ecx,2
  00492f74  8d540808              lea       edx,[eax+ecx+8]
  00492f78  66895610              mov       [esi+10h],dx
  00492f7c  8a5640                mov       dl,[esi+40h]
  00492f7f  80ca01                or        dl,1
  00492f82  885640                mov       [esi+40h],dl
  00492f85  8b0f                  mov       ecx,[edi]
  00492f87  83c940                or        ecx,40h
  00492f8a  66899f96000000        mov       [edi+96h],bx
  00492f91  890f                  mov       [edi],ecx
  00492f93  66837e1600            cmp       word [esi+16h],0
  00492f98  897e6c                mov       [esi+6Ch],edi
  00492f9b  7422                  je        short 00492FBFh
  00492f9d  e89e410b00            call      00547140h
  00492fa2  99                    cdq
  00492fa3  b964000000            mov       ecx,64h
  00492fa8  f7f9                  idiv      ecx
  00492faa  83fa1e                cmp       edx,1Eh
  00492fad  7d10                  jge       short 00492FBFh
  00492faf  6a00                  push      0
  00492fb1  57                    push      edi
  00492fb2  56                    push      esi
  00492fb3  c6460707              mov       byte [esi+7],7
  00492fb7  e864940a00            call      0053C420h
  00492fbc  83c40c                add       esp,0Ch

LAB_00492fbf:
  00492fbf  85ff                  test      edi,edi
  00492fc1  7442                  je        short 00493005h
  00492fc3  57                    push      edi
  00492fc4  56                    push      esi
  00492fc5  e8b66b0a00            call      00539B80h
  00492fca  83c408                add       esp,8
  00492fcd  8d571c                lea       edx,[edi+1Ch]
  00492fd0  52                    push      edx
  00492fd1  6800180000            push      1800h
  00492fd6  6800030000            push      300h
  00492fdb  6a03                  push      3
  00492fdd  e83eab0a00            call      0053DB20h
  00492fe2  8b07                  mov       eax,[edi]
  00492fe4  83c410                add       esp,10h
  00492fe7  a900000040            test      eax,40000000h
  00492fec  7411                  je        short 00492FFFh
  00492fee  8a4e40                mov       cl,[esi+40h]
  00492ff1  8b06                  mov       eax,[esi]
  00492ff3  80e1fe                and       cl,0FEh
  00492ff6  24fd                  and       al,0FDh
  00492ff8  884e40                mov       [esi+40h],cl
  00492ffb  8906                  mov       [esi],eax
  00492ffd  eb06                  jmp       short 00493005h

LAB_00492fff:
  00492fff  66c746141e00          mov       word [esi+14h],1Eh

LAB_00493005:
  00493005  8b06                  mov       eax,[esi]
  00493007  a900000002            test      eax,2000000h
  0049300c  0f84d6010000          je        near 004931E8h
  00493012  f6464001              test      byte [esi+40h],1
  00493016  0f85cc010000          jne       near 004931E8h
  0049301c  24fd                  and       al,0FDh
  0049301e  8906                  mov       [esi],eax
  00493020  e9c3010000            jmp       004931E8h

LAB_00493025:
  00493025  a811                  test      al,11h
  00493027  0f85bb010000          jne       near 004931E8h
  0049302d  a904080000            test      eax,804h
  00493032  0f85b0010000          jne       near 004931E8h
  00493038  33c9                  xor       ecx,ecx
  0049303a  8a4e42                mov       cl,[esi+42h]
  0049303d  83f903                cmp       ecx,3
  00493040  0f87a2010000          ja        near 004931E8h
  00493046  ff248dd4354900        jmp       dword [ecx*4+4935D4h]
  0049304d  33c9                  xor       ecx,ecx
  0049304f  66394e18              cmp       [esi+18h],cx
  00493053  740a                  je        short 0049305Fh
  00493055  0d00000001            or        eax,1000000h
  0049305a  894e18                mov       [esi+18h],ecx
  0049305d  8906                  mov       [esi],eax

LAB_0049305f:
  0049305f  817c24182c010000      cmp       dword [esp+18h],12Ch
  00493067  7d09                  jge       short 00493072h
  00493069  c6464300              mov       byte [esi+43h],0
  0049306d  e976010000            jmp       004931E8h

LAB_00493072:
  00493072  8a4643                mov       al,[esi+43h]
  00493075  84c0                  test      al,al
  00493077  7405                  je        short 0049307Eh
  00493079  fec8                  dec       al
  0049307b  884643                mov       [esi+43h],al

LAB_0049307e:
  0049307e  894e5c                mov       [esi+5Ch],ecx
  00493081  e962010000            jmp       004931E8h
  00493086  b901000000            mov       ecx,1
  0049308b  66394e18              cmp       [esi+18h],cx
  0049308f  740a                  je        short 0049309Bh
  00493091  0d00000001            or        eax,1000000h
  00493096  894e18                mov       [esi+18h],ecx
  00493099  8906                  mov       [esi],eax

LAB_0049309b:
  0049309b  8b442440              mov       eax,[esp+40h]
  0049309f  8b4c2414              mov       ecx,[esp+14h]
  004930a3  83c020                add       eax,20h
  004930a6  8d1408                lea       edx,[eax+ecx]
  004930a9  8d3c00                lea       edi,[eax+eax]
  004930ac  81e2ff0f0000          and       edx,0FFFh
  004930b2  3bd7                  cmp       edx,edi
  004930b4  7f14                  jg        short 004930CAh
  004930b6  66014e32              add       [esi+32h],cx
  004930ba  c6464300              mov       byte [esi+43h],0
  004930be  c7465c00000000        mov       dword [esi+5Ch],0
  004930c5  e91e010000            jmp       004931E8h

LAB_004930ca:
  004930ca  81e1ff0f0000          and       ecx,0FFFh
  004930d0  81f900080000          cmp       ecx,800h
  004930d6  7c02                  jl        short 004930DAh
  004930d8  f7d8                  neg       eax

LAB_004930da:
  004930da  66014632              add       [esi+32h],ax
  004930de  c7465c00000000        mov       dword [esi+5Ch],0
  004930e5  e9fe000000            jmp       004931E8h
  004930ea  b901000000            mov       ecx,1
  004930ef  66394e18              cmp       [esi+18h],cx
  004930f3  740a                  je        short 004930FFh
  004930f5  0d00000001            or        eax,1000000h
  004930fa  894e18                mov       [esi+18h],ecx
  004930fd  8906                  mov       [esi],eax

LAB_004930ff:
  004930ff  0fbf4610              movsx     eax,word [esi+10h]
  00493103  39442418              cmp       [esp+18h],eax
  00493107  7d09                  jge       short 00493112h
  00493109  c6464300              mov       byte [esi+43h],0
  0049310d  e9d6000000            jmp       004931E8h

LAB_00493112:
  00493112  8a4643                mov       al,[esi+43h]
  00493115  84c0                  test      al,al
  00493117  7405                  je        short 0049311Eh
  00493119  fec8                  dec       al
  0049311b  884643                mov       [esi+43h],al

LAB_0049311e:
  0049311e  8b542440              mov       edx,[esp+40h]
  00493122  8b4c2414              mov       ecx,[esp+14h]
  00493126  8d4240                lea       eax,[edx+40h]
  00493129  8d3c08                lea       edi,[eax+ecx]
  0049312c  8d1c00                lea       ebx,[eax+eax]
  0049312f  81e7ff0f0000          and       edi,0FFFh
  00493135  3bfb                  cmp       edi,ebx
  00493137  7f15                  jg        short 0049314Eh
  00493139  66014e32              add       [esi+32h],cx
  0049313d  81c2e0000000          add       edx,0E0h
  00493143  c1e207                shl       edx,7
  00493146  89565c                mov       [esi+5Ch],edx
  00493149  e99a000000            jmp       004931E8h

LAB_0049314e:
  0049314e  81e1ff0f0000          and       ecx,0FFFh
  00493154  81f900080000          cmp       ecx,800h
  0049315a  7c02                  jl        short 0049315Eh
  0049315c  f7d8                  neg       eax

LAB_0049315e:
  0049315e  66014632              add       [esi+32h],ax
  00493162  81c2e0000000          add       edx,0E0h
  00493168  c1e207                shl       edx,7
  0049316b  89565c                mov       [esi+5Ch],edx
  0049316e  eb78                  jmp       short 004931E8h
  00493170  8a4e43                mov       cl,[esi+43h]
  00493173  84c9                  test      cl,cl
  00493175  7405                  je        short 0049317Ch
  00493177  fec9                  dec       cl
  00493179  884e43                mov       [esi+43h],cl

LAB_0049317c:
  0049317c  b901000000            mov       ecx,1
  00493181  66394e18              cmp       [esi+18h],cx
  00493185  740a                  je        short 00493191h
  00493187  0d00000001            or        eax,1000000h
  0049318c  894e18                mov       [esi+18h],ecx
  0049318f  8906                  mov       [esi],eax

LAB_00493191:
  00493191  8b4c2440              mov       ecx,[esp+40h]
  00493195  668b4610              mov       ax,[esi+10h]
  00493199  8d91e0000000          lea       edx,[ecx+0E0h]
  0049319f  c1e207                shl       edx,7
  004931a2  89565c                mov       [esi+5Ch],edx
  004931a5  668b5632              mov       dx,[esi+32h]
  004931a9  2bc2                  sub       eax,edx
  004931ab  25ff0f0000            and       eax,0FFFh
  004931b0  8db800010000          lea       edi,[eax+100h]
  004931b6  81e7ff0f0000          and       edi,0FFFh
  004931bc  81ff00020000          cmp       edi,200h
  004931c2  7e24                  jle       short 004931E8h
  004931c4  25ff0f0000            and       eax,0FFFh
  004931c9  3d00080000            cmp       eax,800h
  004931ce  7d09                  jge       short 004931D9h
  004931d0  d1f9                  sar       ecx,1
  004931d2  83c120                add       ecx,20h
  004931d5  8bc1                  mov       eax,ecx
  004931d7  eb09                  jmp       short 004931E2h

LAB_004931d9:
  004931d9  d1f9                  sar       ecx,1
  004931db  b8e0ffffff            mov       eax,0FFFFFFE0h
  004931e0  2bc1                  sub       eax,ecx

LAB_004931e2:
  004931e2  03d0                  add       edx,eax
  004931e4  66895632              mov       [esi+32h],dx

LAB_004931e8:
  004931e8  8a4e40                mov       cl,[esi+40h]
  004931eb  f6c101                test      cl,1
  004931ee  0f84b0000000          je        near 004932A4h
  004931f4  8b466c                mov       eax,[esi+6Ch]
  004931f7  8b6e44                mov       ebp,[esi+44h]
  004931fa  8b5e48                mov       ebx,[esi+48h]
  004931fd  8b7e4c                mov       edi,[esi+4Ch]
  00493200  8b501c                mov       edx,[eax+1Ch]
  00493203  03d5                  add       edx,ebp
  00493205  89561c                mov       [esi+1Ch],edx
  00493208  8b5020                mov       edx,[eax+20h]
  0049320b  03d3                  add       edx,ebx
  0049320d  895620                mov       [esi+20h],edx
  00493210  8b5024                mov       edx,[eax+24h]
  00493213  03d7                  add       edx,edi
  00493215  895624                mov       [esi+24h],edx
  00493218  8b10                  mov       edx,[eax]
  0049321a  f7c200000100          test      edx,10000h
  00493220  7409                  je        short 0049322Bh
  00493222  33c0                  xor       eax,eax
  00493224  a038e55d00            mov       al,[5DE538h]
  00493229  eb07                  jmp       short 00493232h

LAB_0049322b:
  0049322b  33d2                  xor       edx,edx
  0049322d  8a5008                mov       dl,[eax+8]
  00493230  8bc2                  mov       eax,edx

LAB_00493232:
  00493232  8d14c0                lea       edx,[eax+eax*8]
  00493235  8d0450                lea       eax,[eax+edx*2]
  00493238  33d2                  xor       edx,edx
  0049323a  8a148588915e00        mov       dl,[eax*4+5E9188h]
  00493241  8bc2                  mov       eax,edx
  00493243  c1e004                shl       eax,4
  00493246  05204a6000            add       eax,604A20h
  0049324b  f64002f0              test      byte [eax+2],0F0h
  0049324f  7461                  je        short 004932B2h
  00493251  66ff4e10              dec       word [esi+10h]
  00493255  33db                  xor       ebx,ebx
  00493257  66395e10              cmp       [esi+10h],bx
  0049325b  7557                  jne       short 004932B4h
  0049325d  0fbf4632              movsx     eax,word [esi+32h]
  00493261  8b16                  mov       edx,[esi]
  00493263  0500080000            add       eax,800h
  00493268  83e2fd                and       edx,0FFFFFFFDh
  0049326b  80e1fe                and       cl,0FEh
  0049326e  50                    push      eax
  0049326f  8916                  mov       [esi],edx
  00493271  885e43                mov       [esi+43h],bl
  00493274  884e40                mov       [esi+40h],cl
  00493277  e8a0270b00            call      00545A1Ch
  0049327c  8b4e50                mov       ecx,[esi+50h]
  0049327f  c1e005                shl       eax,5
  00493282  03c8                  add       ecx,eax
  00493284  894e50                mov       [esi+50h],ecx
  00493287  0fbf4e32              movsx     ecx,word [esi+32h]
  0049328b  81c100080000          add       ecx,800h
  00493291  51                    push      ecx
  00493292  e87f270b00            call      00545A16h
  00493297  8b4e58                mov       ecx,[esi+58h]
  0049329a  c1e005                shl       eax,5
  0049329d  03c8                  add       ecx,eax
  0049329f  894e58                mov       [esi+58h],ecx
  004932a2  eb10                  jmp       short 004932B4h

LAB_004932a4:
  004932a4  8b54241c              mov       edx,[esp+1Ch]
  004932a8  52                    push      edx
  004932a9  56                    push      esi
  004932aa  e8b14effff            call      00488160h
  004932af  83c408                add       esp,8

LAB_004932b2:
  004932b2  33db                  xor       ebx,ebx

LAB_004932b4:
  004932b4  56                    push      esi
  004932b5  e886060a00            call      00533940h
  004932ba  8b06                  mov       eax,[esi]
  004932bc  83c404                add       esp,4
  004932bf  8bc8                  mov       ecx,eax
  004932c1  83e101                and       ecx,1
  004932c4  740a                  je        short 004932D0h
  004932c6  8b5660                mov       edx,[esi+60h]
  004932c9  8b7e20                mov       edi,[esi+20h]
  004932cc  3bfa                  cmp       edi,edx
  004932ce  7e14                  jle       short 004932E4h

LAB_004932d0:
  004932d0  3bcb                  cmp       ecx,ebx
  004932d2  753f                  jne       short 00493313h
  004932d4  8b7e20                mov       edi,[esi+20h]
  004932d7  8b5660                mov       edx,[esi+60h]
  004932da  81ef00c00000          sub       edi,0C000h
  004932e0  3bfa                  cmp       edi,edx
  004932e2  7f27                  jg        short 0049330Bh

LAB_004932e4:
  004932e4  f7869000000000a00000  test      dword [esi+90h],0A000h
  004932ee  751b                  jne       short 0049330Bh
  004932f0  3bcb                  cmp       ecx,ebx
  004932f2  895620                mov       [esi+20h],edx
  004932f5  740b                  je        short 00493302h
  004932f7  8a4e43                mov       cl,[esi+43h]
  004932fa  84c9                  test      cl,cl
  004932fc  7404                  je        short 00493302h
  004932fe  c6464300              mov       byte [esi+43h],0

LAB_00493302:
  00493302  24fe                  and       al,0FEh
  00493304  895e54                mov       [esi+54h],ebx
  00493307  8906                  mov       [esi],eax
  00493309  eb08                  jmp       short 00493313h

LAB_0049330b:
  0049330b  3bcb                  cmp       ecx,ebx
  0049330d  7504                  jne       short 00493313h
  0049330f  0c01                  or        al,1
  00493311  8906                  mov       [esi],eax

LAB_00493313:
  00493313  8a4640                mov       al,[esi+40h]
  00493316  a801                  test      al,1
  00493318  7411                  je        short 0049332Bh
  0049331a  8b4e6c                mov       ecx,[esi+6Ch]
  0049331d  f70100200050          test      dword [ecx],50002000h
  00493323  7512                  jne       short 00493337h
  00493325  66395912              cmp       [ecx+12h],bx
  00493329  750c                  jne       short 00493337h

LAB_0049332b:
  0049332b  f7869000000082ad0020  test      dword [esi+90h],2000AD82h
  00493335  7417                  je        short 0049334Eh

LAB_00493337:
  00493337  8b16                  mov       edx,[esi]
  00493339  c6464201              mov       byte [esi+42h],1
  0049333d  83e2fd                and       edx,0FFFFFFFDh
  00493340  24fe                  and       al,0FEh
  00493342  8916                  mov       [esi],edx
  00493344  c6464301              mov       byte [esi+43h],1
  00493348  884640                mov       [esi+40h],al
  0049334b  895e5c                mov       [esi+5Ch],ebx

LAB_0049334e:
  0049334e  66399e96000000        cmp       [esi+96h],bx
  00493355  741b                  je        short 00493372h
  00493357  8b06                  mov       eax,[esi]
  00493359  a840                  test      al,40h
  0049335b  740a                  je        short 00493367h
  0049335d  a843                  test      al,43h
  0049335f  7511                  jne       short 00493372h
  00493361  c6464300              mov       byte [esi+43h],0
  00493365  eb0b                  jmp       short 00493372h

LAB_00493367:
  00493367  c6464200              mov       byte [esi+42h],0
  0049336b  c6464301              mov       byte [esi+43h],1
  0049336f  895e5c                mov       [esi+5Ch],ebx

LAB_00493372:
  00493372  395c241c              cmp       [esp+1Ch],ebx
  00493376  0f844e020000          je        near 004935CAh
  0049337c  f70617080000          test      dword [esi],817h
  00493382  0f8542020000          jne       near 004935CAh
  00493388  8a4643                mov       al,[esi+43h]
  0049338b  84c0                  test      al,al
  0049338d  0f8537020000          jne       near 004935CAh
  00493393  f6464001              test      byte [esi+40h],1
  00493397  0f852d020000          jne       near 004935CAh
  0049339d  8b466c                mov       eax,[esi+6Ch]
  004933a0  8b6e1c                mov       ebp,[esi+1Ch]
  004933a3  8b4e24                mov       ecx,[esi+24h]
  004933a6  8b781c                mov       edi,[eax+1Ch]
  004933a9  2bfd                  sub       edi,ebp
  004933ab  8b6824                mov       ebp,[eax+24h]
  004933ae  2be9                  sub       ebp,ecx
  004933b0  c1ff0c                sar       edi,0Ch
  004933b3  c1fd0c                sar       ebp,0Ch
  004933b6  8bc7                  mov       eax,edi
  004933b8  8bcd                  mov       ecx,ebp
  004933ba  0fafc7                imul      eax,edi
  004933bd  0fafcd                imul      ecx,ebp
  004933c0  03c1                  add       eax,ecx
  004933c2  50                    push      eax
  004933c3  e848260b00            call      00545A10h
  004933c8  55                    push      ebp
  004933c9  57                    push      edi
  004933ca  89442420              mov       [esp+20h],eax
  004933ce  e84f260b00            call      00545A22h
  004933d3  668b5632              mov       dx,[esi+32h]
  004933d7  2bc2                  sub       eax,edx
  004933d9  25ff0f0000            and       eax,0FFFh
  004933de  89442414              mov       [esp+14h],eax
  004933e2  8b442418              mov       eax,[esp+18h]
  004933e6  3dfa000000            cmp       eax,0FAh
  004933eb  0f8dc6000000          jge       near 004934B7h
  004933f1  e84a3d0b00            call      00547140h
  004933f6  99                    cdq
  004933f7  b964000000            mov       ecx,64h
  004933fc  f7f9                  idiv      ecx
  004933fe  83fa3c                cmp       edx,3Ch
  00493401  0f8da9000000          jge       near 004934B0h
  00493407  8b542414              mov       edx,[esp+14h]
  0049340b  8b7c2410              mov       edi,[esp+10h]
  0049340f  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00493416  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0049341d  25ff0f0000            and       eax,0FFFh
  00493422  3bc1                  cmp       eax,ecx
  00493424  7f3e                  jg        short 00493464h
  00493426  e8153d0b00            call      00547140h
  0049342b  99                    cdq
  0049342c  8d4c3f01              lea       ecx,[edi+edi+1]
  00493430  f7f9                  idiv      ecx
  00493432  8b442418              mov       eax,[esp+18h]
  00493436  83c264                add       edx,64h
  00493439  3bc2                  cmp       eax,edx
  0049343b  7d27                  jge       short 00493464h
  0049343d  8b16                  mov       edx,[esi]
  0049343f  8a4640                mov       al,[esi+40h]
  00493442  81e2fffffffe          and       edx,0FEFFFFFFh
  00493448  c7461803000000        mov       dword [esi+18h],3
  0049344f  83ca02                or        edx,2
  00493452  24fe                  and       al,0FEh
  00493454  8916                  mov       [esi],edx
  00493456  884640                mov       [esi+40h],al
  00493459  895e5c                mov       [esi+5Ch],ebx
  0049345c  5f                    pop       edi
  0049345d  5e                    pop       esi
  0049345e  5d                    pop       ebp
  0049345f  5b                    pop       ebx
  00493460  83c42c                add       esp,2Ch
  00493463  c3                    ret

LAB_00493464:
  00493464  c6464203              mov       byte [esi+42h],3
  00493468  e8d33c0b00            call      00547140h
  0049346d  99                    cdq
  0049346e  b90a000000            mov       ecx,0Ah
  00493473  f7f9                  idiv      ecx
  00493475  8b4c2440              mov       ecx,[esp+40h]
  00493479  8bc7                  mov       eax,edi
  0049347b  d1f8                  sar       eax,1
  0049347d  8db980000000          lea       edi,[ecx+80h]
  00493483  c1e704                shl       edi,4
  00493486  02d0                  add       dl,al
  00493488  80c214                add       dl,14h
  0049348b  885643                mov       [esi+43h],dl
  0049348e  e8ad3c0b00            call      00547140h
  00493493  99                    cdq
  00493494  f7ff                  idiv      edi
  00493496  8b4c2414              mov       ecx,[esp+14h]
  0049349a  d1ff                  sar       edi,1
  0049349c  2bd7                  sub       edx,edi
  0049349e  03d1                  add       edx,ecx
  004934a0  66035632              add       dx,[esi+32h]
  004934a4  66895610              mov       [esi+10h],dx
  004934a8  5f                    pop       edi
  004934a9  5e                    pop       esi
  004934aa  5d                    pop       ebp
  004934ab  5b                    pop       ebx
  004934ac  83c42c                add       esp,2Ch
  004934af  c3                    ret

LAB_004934b0:
  004934b0  83fa50                cmp       edx,50h
  004934b3  7d51                  jge       short 00493506h
  004934b5  eb1d                  jmp       short 004934D4h

LAB_004934b7:
  004934b7  3df4010000            cmp       eax,1F4h
  004934bc  0f8d92000000          jge       near 00493554h
  004934c2  e8793c0b00            call      00547140h
  004934c7  99                    cdq
  004934c8  b964000000            mov       ecx,64h
  004934cd  f7f9                  idiv      ecx
  004934cf  83fa32                cmp       edx,32h
  004934d2  7d2d                  jge       short 00493501h

LAB_004934d4:
  004934d4  c6464202              mov       byte [esi+42h],2
  004934d8  e8633c0b00            call      00547140h
  004934dd  99                    cdq
  004934de  b90a000000            mov       ecx,0Ah
  004934e3  66c746105a00          mov       word [esi+10h],5Ah
  004934e9  f7f9                  idiv      ecx
  004934eb  8b442410              mov       eax,[esp+10h]
  004934ef  d1f8                  sar       eax,1
  004934f1  02d0                  add       dl,al
  004934f3  80c214                add       dl,14h
  004934f6  885643                mov       [esi+43h],dl
  004934f9  5f                    pop       edi
  004934fa  5e                    pop       esi
  004934fb  5d                    pop       ebp
  004934fc  5b                    pop       ebx
  004934fd  83c42c                add       esp,2Ch
  00493500  c3                    ret

LAB_00493501:
  00493501  83fa50                cmp       edx,50h
  00493504  7d60                  jge       short 00493566h

LAB_00493506:
  00493506  c6464203              mov       byte [esi+42h],3
  0049350a  e8313c0b00            call      00547140h
  0049350f  99                    cdq
  00493510  b90a000000            mov       ecx,0Ah
  00493515  f7f9                  idiv      ecx
  00493517  8b442410              mov       eax,[esp+10h]
  0049351b  8b4c2440              mov       ecx,[esp+40h]
  0049351f  d1f8                  sar       eax,1
  00493521  8db980000000          lea       edi,[ecx+80h]
  00493527  c1e704                shl       edi,4
  0049352a  02d0                  add       dl,al
  0049352c  80c214                add       dl,14h
  0049352f  885643                mov       [esi+43h],dl
  00493532  e8093c0b00            call      00547140h
  00493537  99                    cdq
  00493538  f7ff                  idiv      edi
  0049353a  8b4c2414              mov       ecx,[esp+14h]
  0049353e  d1ff                  sar       edi,1
  00493540  2bd7                  sub       edx,edi
  00493542  03d1                  add       edx,ecx
  00493544  66035632              add       dx,[esi+32h]
  00493548  66895610              mov       [esi+10h],dx
  0049354c  5f                    pop       edi
  0049354d  5e                    pop       esi
  0049354e  5d                    pop       ebp
  0049354f  5b                    pop       ebx
  00493550  83c42c                add       esp,2Ch
  00493553  c3                    ret

LAB_00493554:
  00493554  e8e73b0b00            call      00547140h
  00493559  99                    cdq
  0049355a  b964000000            mov       ecx,64h
  0049355f  f7f9                  idiv      ecx
  00493561  83fa1e                cmp       edx,1Eh
  00493564  7d27                  jge       short 0049358Dh

LAB_00493566:
  00493566  c6464200              mov       byte [esi+42h],0
  0049356a  e8d13b0b00            call      00547140h
  0049356f  99                    cdq
  00493570  b90a000000            mov       ecx,0Ah
  00493575  f7f9                  idiv      ecx
  00493577  8b442410              mov       eax,[esp+10h]
  0049357b  d1f8                  sar       eax,1
  0049357d  02d0                  add       dl,al
  0049357f  80c214                add       dl,14h
  00493582  885643                mov       [esi+43h],dl
  00493585  5f                    pop       edi
  00493586  5e                    pop       esi
  00493587  5d                    pop       ebp
  00493588  5b                    pop       ebx
  00493589  83c42c                add       esp,2Ch
  0049358c  c3                    ret

LAB_0049358d:
  0049358d  83fa3c                cmp       edx,3Ch
  00493590  7d10                  jge       short 004935A2h
  00493592  c6464201              mov       byte [esi+42h],1
  00493596  c6464301              mov       byte [esi+43h],1
  0049359a  5f                    pop       edi
  0049359b  5e                    pop       esi
  0049359c  5d                    pop       ebp
  0049359d  5b                    pop       ebx
  0049359e  83c42c                add       esp,2Ch
  004935a1  c3                    ret

LAB_004935a2:
  004935a2  c6464203              mov       byte [esi+42h],3
  004935a6  e8953b0b00            call      00547140h
  004935ab  99                    cdq
  004935ac  b90a000000            mov       ecx,0Ah
  004935b1  f7f9                  idiv      ecx
  004935b3  8b442410              mov       eax,[esp+10h]
  004935b7  d1f8                  sar       eax,1
  004935b9  02d0                  add       dl,al
  004935bb  80c214                add       dl,14h
  004935be  885643                mov       [esi+43h],dl
  004935c1  e87a3b0b00            call      00547140h
  004935c6  66894610              mov       [esi+10h],ax

LAB_004935ca:
  004935ca  5f                    pop       edi
  004935cb  5e                    pop       esi
  004935cc  5d                    pop       ebp
  004935cd  5b                    pop       ebx
  004935ce  83c42c                add       esp,2Ch
  004935d1  c3                    ret
  004935d2  8bff                  mov       edi,edi
  004935d4  4d                    dec       ebp
  004935d5  304900                xor       [ecx],cl
  004935d8  8630                  xchg      dh,[eax]
  004935da  49                    dec       ecx
  004935db  00ea                  add       dl,ch
  004935dd  304900                xor       [ecx],cl
  004935e0  7031                  jo        short 00493613h
  004935e2  49                    dec       ecx
  004935e3  009090909090          add       [eax-6F6F6F70h],dl
  004935e9  90                    nop
  004935ea  90                    nop
  004935eb  90                    nop
  004935ec  90                    nop
  004935ed  90                    nop
  004935ee  90                    nop
  004935ef  90                    nop

48 cible(s) de saut interne : 0x00492efe, 0x00492f1d, 0x00492fbf, 0x00492fff, 0x00493005, 0x00493025, 0x0049305f, 0x00493072, 0x0049307e, 0x0049309b, 0x004930ca, 0x004930da, 0x004930ff, 0x00493112, 0x0049311e, 0x0049314e, 0x0049315e, 0x0049317c, 0x00493191, 0x004931d9, 0x004931e2, 0x004931e8, 0x0049322b, 0x00493232, 0x004932a4, 0x004932b2, 0x004932b4, 0x004932d0, 0x004932e4, 0x00493302, 0x0049330b, 0x00493313, 0x0049332b, 0x00493337, 0x0049334e, 0x00493367, 0x00493372, 0x00493464, 0x004934b0, 0x004934b7, 0x004934d4, 0x00493501, 0x00493506, 0x00493554, 0x00493566, 0x0049358d, 0x004935a2, 0x004935ca
