Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00480e40  section .text  offset fichier 0x80240  1038 octets

  00480e40  83ec30                sub       esp,30h
  00480e43  53                    push      ebx
  00480e44  55                    push      ebp
  00480e45  56                    push      esi
  00480e46  8b742440              mov       esi,[esp+40h]
  00480e4a  33d2                  xor       edx,edx
  00480e4c  57                    push      edi
  00480e4d  8b06                  mov       eax,[esi]
  00480e4f  66c744241c00fc        mov       word [esp+1Ch],0FC00h
  00480e56  a900000002            test      eax,2000000h
  00480e5b  66c744241e80ff        mov       word [esp+1Eh],0FF80h
  00480e62  66c74424203200        mov       word [esp+20h],32h
  00480e69  66c7442422c800        mov       word [esp+22h],0C8h
  00480e70  6689542424            mov       [esp+24h],dx
  00480e75  66c74424268000        mov       word [esp+26h],80h
  00480e7c  7436                  je        short 00480EB4h
  00480e7e  668b4e2a              mov       cx,[esi+2Ah]
  00480e82  668bc1                mov       ax,cx
  00480e85  66c1f802              sar       ax,2
  00480e89  2bc8                  sub       ecx,eax
  00480e8b  668b462c              mov       ax,[esi+2Ch]
  00480e8f  668bf8                mov       di,ax
  00480e92  66894e2a              mov       [esi+2Ah],cx
  00480e96  66c1ff02              sar       di,2
  00480e9a  2bc7                  sub       eax,edi
  00480e9c  6683f908              cmp       cx,8
  00480ea0  6689462c              mov       [esi+2Ch],ax
  00480ea4  0f8d9c030000          jge       near 00481246h
  00480eaa  8916                  mov       [esi],edx
  00480eac  5f                    pop       edi
  00480ead  5e                    pop       esi
  00480eae  5d                    pop       ebp
  00480eaf  5b                    pop       ebx
  00480eb0  83c430                add       esp,30h
  00480eb3  c3                    ret

LAB_00480eb4:
  00480eb4  a900000040            test      eax,40000000h
  00480eb9  7413                  je        short 00480ECEh
  00480ebb  8b466c                mov       eax,[esi+6Ch]
  00480ebe  8b08                  mov       ecx,[eax]
  00480ec0  83c904                or        ecx,4
  00480ec3  8908                  mov       [eax],ecx
  00480ec5  8b4e6c                mov       ecx,[esi+6Ch]
  00480ec8  66c741123c00          mov       word [ecx+12h],3Ch

LAB_00480ece:
  00480ece  8b5e78                mov       ebx,[esi+78h]
  00480ed1  3bda                  cmp       ebx,edx
  00480ed3  895c2410              mov       [esp+10h],ebx
  00480ed7  7408                  je        short 00480EE1h
  00480ed9  8b466c                mov       eax,[esi+6Ch]
  00480edc  f60004                test      byte [eax],4
  00480edf  7517                  jne       short 00480EF8h

LAB_00480ee1:
  00480ee1  8b466c                mov       eax,[esi+6Ch]
  00480ee4  0fbf4e14              movsx     ecx,word [esi+14h]
  00480ee8  0fbf7812              movsx     edi,word [eax+12h]
  00480eec  8d0c89                lea       ecx,[ecx+ecx*4]
  00480eef  3bf9                  cmp       edi,ecx
  00480ef1  7c19                  jl        short 00480F0Ch
  00480ef3  f60604                test      byte [esi],4
  00480ef6  7414                  je        short 00480F0Ch

LAB_00480ef8:
  00480ef8  8b3e                  mov       edi,[esi]
  00480efa  895678                mov       [esi+78h],edx
  00480efd  83e7fb                and       edi,0FFFFFFFBh
  00480f00  89542410              mov       [esp+10h],edx
  00480f04  893e                  mov       [esi],edi
  00480f06  66ff4816              dec       word [eax+16h]
  00480f0a  8bda                  mov       ebx,edx

LAB_00480f0c:
  00480f0c  3bda                  cmp       ebx,edx
  00480f0e  742c                  je        short 00480F3Ch
  00480f10  f60604                test      byte [esi],4
  00480f13  7427                  je        short 00480F3Ch
  00480f15  8b531c                mov       edx,[ebx+1Ch]
  00480f18  8b7e44                mov       edi,[esi+44h]
  00480f1b  8b4e48                mov       ecx,[esi+48h]
  00480f1e  03d7                  add       edx,edi
  00480f20  89561c                mov       [esi+1Ch],edx
  00480f23  8b4320                mov       eax,[ebx+20h]
  00480f26  03c1                  add       eax,ecx
  00480f28  894620                mov       [esi+20h],eax
  00480f2b  8b4b24                mov       ecx,[ebx+24h]
  00480f2e  034e4c                add       ecx,[esi+4Ch]
  00480f31  894e24                mov       [esi+24h],ecx
  00480f34  5f                    pop       edi
  00480f35  5e                    pop       esi
  00480f36  5d                    pop       ebp
  00480f37  5b                    pop       ebx
  00480f38  83c430                add       esp,30h
  00480f3b  c3                    ret

LAB_00480f3c:
  00480f3c  f60602                test      byte [esi],2
  00480f3f  0f8499020000          je        near 004811DEh
  00480f45  668b4612              mov       ax,[esi+12h]
  00480f49  663bc2                cmp       ax,dx
  00480f4c  7428                  je        short 00480F76h
  00480f4e  48                    dec       eax
  00480f4f  663bc2                cmp       ax,dx
  00480f52  66894612              mov       [esi+12h],ax
  00480f56  0f85ea020000          jne       near 00481246h
  00480f5c  8d561c                lea       edx,[esi+1Ch]
  00480f5f  52                    push      edx
  00480f60  6800120000            push      1200h
  00480f65  68a7070000            push      7A7h
  00480f6a  6a12                  push      12h
  00480f6c  e8afcb0b00            call      0053DB20h
  00480f71  83c410                add       esp,10h
  00480f74  33d2                  xor       edx,edx

LAB_00480f76:
  00480f76  66395610              cmp       [esi+10h],dx
  00480f7a  750c                  jne       short 00480F88h
  00480f7c  8b06                  mov       eax,[esi]
  00480f7e  66c746100100          mov       word [esi+10h],1
  00480f84  0c80                  or        al,80h
  00480f86  8906                  mov       [esi],eax

LAB_00480f88:
  00480f88  8b7e1c                mov       edi,[esi+1Ch]
  00480f8b  8b4620                mov       eax,[esi+20h]
  00480f8e  8b4e24                mov       ecx,[esi+24h]
  00480f91  8d6e1c                lea       ebp,[esi+1Ch]
  00480f94  3bda                  cmp       ebx,edx
  00480f96  897c2418              mov       [esp+18h],edi
  00480f9a  89442438              mov       [esp+38h],eax
  00480f9e  894c243c              mov       [esp+3Ch],ecx
  00480fa2  746d                  je        short 00481011h
  00480fa4  8b431c                mov       eax,[ebx+1Ch]
  00480fa7  53                    push      ebx
  00480fa8  2bc7                  sub       eax,edi
  00480faa  c1f80c                sar       eax,0Ch
  00480fad  89442418              mov       [esp+18h],eax
  00480fb1  e80a7c0b00            call      00538BC0h
  00480fb6  8bf8                  mov       edi,eax
  00480fb8  8b4320                mov       eax,[ebx+20h]
  00480fbb  8b5b24                mov       ebx,[ebx+24h]
  00480fbe  8b5620                mov       edx,[esi+20h]
  00480fc1  83e7fe                and       edi,0FFFFFFFEh
  00480fc4  83c404                add       esp,4
  00480fc7  c1e70b                shl       edi,0Bh
  00480fca  03f8                  add       edi,eax
  00480fcc  8b4624                mov       eax,[esi+24h]
  00480fcf  2bd8                  sub       ebx,eax
  00480fd1  8b442414              mov       eax,[esp+14h]
  00480fd5  2bfa                  sub       edi,edx
  00480fd7  8bd0                  mov       edx,eax
  00480fd9  c1fb0c                sar       ebx,0Ch
  00480fdc  0fafd0                imul      edx,eax
  00480fdf  8bc3                  mov       eax,ebx
  00480fe1  0fafc3                imul      eax,ebx
  00480fe4  03d0                  add       edx,eax
  00480fe6  52                    push      edx
  00480fe7  c1ff0c                sar       edi,0Ch
  00480fea  e8214a0c00            call      00545A10h
  00480fef  8b4c2414              mov       ecx,[esp+14h]
  00480ff3  53                    push      ebx
  00480ff4  51                    push      ecx
  00480ff5  8944244c              mov       [esp+4Ch],eax
  00480ff9  e8244a0c00            call      00545A22h
  00480ffe  8b542444              mov       edx,[esp+44h]
  00481002  8bd8                  mov       ebx,eax
  00481004  52                    push      edx
  00481005  57                    push      edi
  00481006  e8174a0c00            call      00545A22h
  0048100b  8b7c2418              mov       edi,[esp+18h]
  0048100f  eb64                  jmp       short 00481075h

LAB_00481011:
  00481011  8b4638                mov       eax,[esi+38h]
  00481014  3bc2                  cmp       eax,edx
  00481016  7406                  je        short 0048101Eh
  00481018  83c0f0                add       eax,0FFFFFFF0h
  0048101b  894638                mov       [esi+38h],eax

LAB_0048101e:
  0048101e  668b4628              mov       ax,[esi+28h]
  00481022  bb00500000            mov       ebx,5000h
  00481027  0fbfc8                movsx     ecx,ax
  0048102a  2bd9                  sub       ebx,ecx
  0048102c  668b4e2a              mov       cx,[esi+2Ah]
  00481030  c1fb02                sar       ebx,2
  00481033  03d8                  add       ebx,eax
  00481035  668bc1                mov       ax,cx
  00481038  66c1f802              sar       ax,2
  0048103c  2bc8                  sub       ecx,eax
  0048103e  668b462c              mov       ax,[esi+2Ch]
  00481042  66895e28              mov       [esi+28h],bx
  00481046  668bd8                mov       bx,ax
  00481049  66c1fb02              sar       bx,2
  0048104d  2bc3                  sub       eax,ebx
  0048104f  6683f908              cmp       cx,8
  00481053  66894e2a              mov       [esi+2Ah],cx
  00481057  6689462c              mov       [esi+2Ch],ax
  0048105b  7d0a                  jge       short 00481067h
  0048105d  8916                  mov       [esi],edx
  0048105f  5f                    pop       edi
  00481060  5e                    pop       esi
  00481061  5d                    pop       ebp
  00481062  5b                    pop       ebx
  00481063  83c430                add       esp,30h
  00481066  c3                    ret

LAB_00481067:
  00481067  0fbf5e32              movsx     ebx,word [esi+32h]
  0048106b  0fbf4634              movsx     eax,word [esi+34h]
  0048106f  81eb00040000          sub       ebx,400h

LAB_00481075:
  00481075  6a10                  push      10h
  00481077  50                    push      eax
  00481078  8d4c2430              lea       ecx,[esp+30h]
  0048107c  53                    push      ebx
  0048107d  51                    push      ecx
  0048107e  e84d1a0b00            call      00532AD0h
  00481083  8b5650                mov       edx,[esi+50h]
  00481086  8b442438              mov       eax,[esp+38h]
  0048108a  8b4e54                mov       ecx,[esi+54h]
  0048108d  8b5c2440              mov       ebx,[esp+40h]
  00481091  03c2                  add       eax,edx
  00481093  8b54243c              mov       edx,[esp+3Ch]
  00481097  03ca                  add       ecx,edx
  00481099  8b5658                mov       edx,[esi+58h]
  0048109c  03d3                  add       edx,ebx
  0048109e  8bd8                  mov       ebx,eax
  004810a0  c1fb03                sar       ebx,3
  004810a3  2bc3                  sub       eax,ebx
  004810a5  8bd9                  mov       ebx,ecx
  004810a7  c1fb03                sar       ebx,3
  004810aa  2bcb                  sub       ecx,ebx
  004810ac  8bda                  mov       ebx,edx
  004810ae  c1fb03                sar       ebx,3
  004810b1  2bd3                  sub       edx,ebx
  004810b3  8b5d00                mov       ebx,[ebp]
  004810b6  03d8                  add       ebx,eax
  004810b8  894650                mov       [esi+50h],eax
  004810bb  895d00                mov       [ebp],ebx
  004810be  8bc3                  mov       eax,ebx
  004810c0  8b5e20                mov       ebx,[esi+20h]
  004810c3  2bc7                  sub       eax,edi
  004810c5  03d9                  add       ebx,ecx
  004810c7  894e54                mov       [esi+54h],ecx
  004810ca  c1f80c                sar       eax,0Ch
  004810cd  895e20                mov       [esi+20h],ebx
  004810d0  8bcb                  mov       ecx,ebx
  004810d2  8b5e24                mov       ebx,[esi+24h]
  004810d5  8bf8                  mov       edi,eax
  004810d7  8b442448              mov       eax,[esp+48h]
  004810db  03da                  add       ebx,edx
  004810dd  895658                mov       [esi+58h],edx
  004810e0  2bc8                  sub       ecx,eax
  004810e2  8b44244c              mov       eax,[esp+4Ch]
  004810e6  8bd3                  mov       edx,ebx
  004810e8  2bd0                  sub       edx,eax
  004810ea  895e24                mov       [esi+24h],ebx
  004810ed  c1fa0c                sar       edx,0Ch
  004810f0  c1f90c                sar       ecx,0Ch
  004810f3  8bda                  mov       ebx,edx
  004810f5  894c2428              mov       [esp+28h],ecx
  004810f9  8bc7                  mov       eax,edi
  004810fb  8bcb                  mov       ecx,ebx
  004810fd  0fafc7                imul      eax,edi
  00481100  0fafcb                imul      ecx,ebx
  00481103  83c410                add       esp,10h
  00481106  03c1                  add       eax,ecx
  00481108  50                    push      eax
  00481109  e802490c00            call      00545A10h
  0048110e  53                    push      ebx
  0048110f  57                    push      edi
  00481110  8944244c              mov       [esp+4Ch],eax
  00481114  e809490c00            call      00545A22h
  00481119  8b542444              mov       edx,[esp+44h]
  0048111d  0500040000            add       eax,400h
  00481122  66894632              mov       [esi+32h],ax
  00481126  8b442418              mov       eax,[esp+18h]
  0048112a  52                    push      edx
  0048112b  50                    push      eax
  0048112c  e8f1480c00            call      00545A22h
  00481131  66894634              mov       [esi+34h],ax
  00481135  8b442410              mov       eax,[esp+10h]
  00481139  85c0                  test      eax,eax
  0048113b  0f8405010000          je        near 00481246h
  00481141  8d4c241c              lea       ecx,[esp+1Ch]
  00481145  51                    push      ecx
  00481146  56                    push      esi
  00481147  e8543bffff            call      00474CA0h
  0048114c  8bf8                  mov       edi,eax
  0048114e  83c408                add       esp,8
  00481151  85ff                  test      edi,edi
  00481153  0f84ed000000          je        near 00481246h
  00481159  3b7e78                cmp       edi,[esi+78h]
  0048115c  0f85e4000000          jne       near 00481246h
  00481162  8b4e74                mov       ecx,[esi+74h]
  00481165  33d2                  xor       edx,edx
  00481167  8a5606                mov       dl,[esi+6]
  0048116a  33c0                  xor       eax,eax
  0048116c  8a4607                mov       al,[esi+7]
  0048116f  6a00                  push      0
  00481171  52                    push      edx
  00481172  50                    push      eax
  00481173  57                    push      edi
  00481174  51                    push      ecx
  00481175  e836910b00            call      0053A2B0h
  0048117a  8b17                  mov       edx,[edi]
  0048117c  66c787960000000100    mov       word [edi+96h],1
  00481185  83ca40                or        edx,40h
  00481188  83c414                add       esp,14h
  0048118b  8917                  mov       [edi],edx
  0048118d  8b466c                mov       eax,[esi+6Ch]
  00481190  55                    push      ebp
  00481191  6800120000            push      1200h
  00481196  66ff4014              inc       word [eax+14h]
  0048119a  8b5500                mov       edx,[ebp]
  0048119d  8b5f1c                mov       ebx,[edi+1Ch]
  004811a0  8b4620                mov       eax,[esi+20h]
  004811a3  2bd3                  sub       edx,ebx
  004811a5  68a60c0000            push      0CA6h
  004811aa  895644                mov       [esi+44h],edx
  004811ad  8b4f20                mov       ecx,[edi+20h]
  004811b0  2bc1                  sub       eax,ecx
  004811b2  8b4e24                mov       ecx,[esi+24h]
  004811b5  894648                mov       [esi+48h],eax
  004811b8  8b4724                mov       eax,[edi+24h]
  004811bb  2bc8                  sub       ecx,eax
  004811bd  6a2e                  push      2Eh
  004811bf  894e4c                mov       [esi+4Ch],ecx
  004811c2  66c705a0935e00ff00    mov       word [5E93A0h],0FFh
  004811cb  830e04                or        dword [esi],4
  004811ce  e84dc90b00            call      0053DB20h
  004811d3  83c410                add       esp,10h
  004811d6  5f                    pop       edi
  004811d7  5e                    pop       esi
  004811d8  5d                    pop       ebp
  004811d9  5b                    pop       ebx
  004811da  83c430                add       esp,30h
  004811dd  c3                    ret

LAB_004811de:
  004811de  668b4628              mov       ax,[esi+28h]
  004811e2  b900080000            mov       ecx,800h
  004811e7  0fbfd0                movsx     edx,ax
  004811ea  2bca                  sub       ecx,edx
  004811ec  8b3e                  mov       edi,[esi]
  004811ee  c1f902                sar       ecx,2
  004811f1  03c1                  add       eax,ecx
  004811f3  66894628              mov       [esi+28h],ax
  004811f7  6689462a              mov       [esi+2Ah],ax
  004811fb  6689462c              mov       [esi+2Ch],ax
  004811ff  8b466c                mov       eax,[esi+6Ch]
  00481202  8b10                  mov       edx,[eax]
  00481204  81e202000002          and       edx,2000002h
  0048120a  0bfa                  or        edi,edx
  0048120c  893e                  mov       [esi],edi
  0048120e  8a0e                  mov       cl,[esi]
  00481210  f6c102                test      cl,2
  00481213  7406                  je        short 0048121Bh
  00481215  8b4878                mov       ecx,[eax+78h]
  00481218  894e78                mov       [esi+78h],ecx

LAB_0048121b:
  0048121b  f70600000002          test      dword [esi],2000000h
  00481221  7404                  je        short 00481227h
  00481223  66ff4816              dec       word [eax+16h]

LAB_00481227:
  00481227  8b4e38                mov       ecx,[esi+38h]
  0048122a  81f9f0000000          cmp       ecx,0F0h
  00481230  730e                  jae       short 00481240h
  00481232  83c110                add       ecx,10h
  00481235  894e38                mov       [esi+38h],ecx
  00481238  5f                    pop       edi
  00481239  5e                    pop       esi
  0048123a  5d                    pop       ebp
  0048123b  5b                    pop       ebx
  0048123c  83c430                add       esp,30h
  0048123f  c3                    ret

LAB_00481240:
  00481240  810800000001          or        dword [eax],1000000h

LAB_00481246:
  00481246  5f                    pop       edi
  00481247  5e                    pop       esi
  00481248  5d                    pop       ebp
  00481249  5b                    pop       ebx
  0048124a  83c430                add       esp,30h
  0048124d  c3                    ret

17 cible(s) de saut interne : 0x00480eb4, 0x00480ece, 0x00480ee1, 0x00480ef8, 0x00480f0c, 0x00480f3c, 0x00480f76, 0x00480f88, 0x00481011, 0x0048101e, 0x00481067, 0x00481075, 0x004811de, 0x0048121b, 0x00481227, 0x00481240, 0x00481246
