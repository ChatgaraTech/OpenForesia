Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00472e80  section .text  offset fichier 0x72280  1584 octets

  00472e80  53                    push      ebx
  00472e81  55                    push      ebp
  00472e82  56                    push      esi
  00472e83  8b742410              mov       esi,[esp+10h]
  00472e87  57                    push      edi
  00472e88  668b4614              mov       ax,[esi+14h]
  00472e8c  663dc800              cmp       ax,0C8h
  00472e90  7571                  jne       short 00472F03h
  00472e92  33db                  xor       ebx,ebx
  00472e94  881d804b6000          mov       [604B80h],bl
  00472e9a  66837e100f            cmp       word [esi+10h],0Fh
  00472e9f  7d43                  jge       short 00472EE4h
  00472ea1  8b4e4c                mov       ecx,[esi+4Ch]
  00472ea4  3bcb                  cmp       ecx,ebx
  00472ea6  743c                  je        short 00472EE4h
  00472ea8  33c0                  xor       eax,eax
  00472eaa  53                    push      ebx
  00472eab  a038e55d00            mov       al,[5DE538h]
  00472eb0  68b0335600            push      5633B0h
  00472eb5  8d1480                lea       edx,[eax+eax*4]
  00472eb8  c1e203                shl       edx,3
  00472ebb  2bd0                  sub       edx,eax
  00472ebd  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00472ec4  8b15cc975e00          mov       edx,[5E97CCh]
  00472eca  50                    push      eax
  00472ecb  8b4270                mov       eax,[edx+70h]
  00472ece  8b0c88                mov       ecx,[eax+ecx*4]
  00472ed1  51                    push      ecx
  00472ed2  e879140d00            call      00544350h
  00472ed7  83c410                add       esp,10h
  00472eda  c705c8995e0040000000  mov       dword [5E99C8h],40h

LAB_00472ee4:
  00472ee4  8b06                  mov       eax,[esi]
  00472ee6  66c74616ffff          mov       word [esi+16h],0FFFFh
  00472eec  80cc20                or        ah,20h
  00472eef  66c74614c900          mov       word [esi+14h],0C9h
  00472ef5  8906                  mov       [esi],eax
  00472ef7  895e5c                mov       [esi+5Ch],ebx
  00472efa  66b80100              mov       ax,1
  00472efe  5f                    pop       edi
  00472eff  5e                    pop       esi
  00472f00  5d                    pop       ebp
  00472f01  5b                    pop       ebx
  00472f02  c3                    ret

LAB_00472f03:
  00472f03  663dc900              cmp       ax,0C9h
  00472f07  7549                  jne       short 00472F52h
  00472f09  66837e16ff            cmp       word [esi+16h],0FFFFh
  00472f0e  0f858d050000          jne       near 004734A1h
  00472f14  0fbf4c2418            movsx     ecx,word [esp+18h]
  00472f19  66c74616feff          mov       word [esi+16h],0FFFEh
  00472f1f  8b15cc975e00          mov       edx,[5E97CCh]
  00472f25  6a00                  push      0
  00472f27  6a00                  push      0
  00472f29  8b82a8000000          mov       eax,[edx+0A8h]
  00472f2f  6800100000            push      1000h
  00472f34  6800040000            push      400h
  00472f39  8b10                  mov       edx,[eax]
  00472f3b  660fb6040a            movzx     ax,byte [edx+ecx]
  00472f40  50                    push      eax
  00472f41  56                    push      esi
  00472f42  e83951ffff            call      00468080h
  00472f47  83c418                add       esp,18h
  00472f4a  6633c0                xor       ax,ax
  00472f4d  5f                    pop       edi
  00472f4e  5e                    pop       esi
  00472f4f  5d                    pop       ebp
  00472f50  5b                    pop       ebx
  00472f51  c3                    ret

LAB_00472f52:
  00472f52  663dcd00              cmp       ax,0CDh
  00472f56  0f85f6010000          jne       near 00473152h
  00472f5c  c744241800000000      mov       dword [esp+18h],0
  00472f64  bdc0935e00            mov       ebp,5E93C0h
  00472f69  c744241404000000      mov       dword [esp+14h],4

LAB_00472f71:
  00472f71  8b4500                mov       eax,[ebp]
  00472f74  a900000080            test      eax,80000000h
  00472f79  0f84a3000000          je        near 00473022h
  00472f7f  a901000050            test      eax,50000001h
  00472f84  0f8598000000          jne       near 00473022h
  00472f8a  8b461c                mov       eax,[esi+1Ch]
  00472f8d  8b4d1c                mov       ecx,[ebp+1Ch]
  00472f90  8d7d1c                lea       edi,[ebp+1Ch]
  00472f93  c1f80c                sar       eax,0Ch
  00472f96  8b5704                mov       edx,[edi+4]
  00472f99  8b5f08                mov       ebx,[edi+8]
  00472f9c  c1f90c                sar       ecx,0Ch
  00472f9f  2bc1                  sub       eax,ecx
  00472fa1  8b4e20                mov       ecx,[esi+20h]
  00472fa4  c1f90c                sar       ecx,0Ch
  00472fa7  c1fa0c                sar       edx,0Ch
  00472faa  2bca                  sub       ecx,edx
  00472fac  8b5624                mov       edx,[esi+24h]
  00472faf  c1fa0c                sar       edx,0Ch
  00472fb2  c1fb0c                sar       ebx,0Ch
  00472fb5  2bd3                  sub       edx,ebx
  00472fb7  8bd8                  mov       ebx,eax
  00472fb9  0fafd8                imul      ebx,eax
  00472fbc  8bc1                  mov       eax,ecx
  00472fbe  0fafc1                imul      eax,ecx
  00472fc1  8bca                  mov       ecx,edx
  00472fc3  03d8                  add       ebx,eax
  00472fc5  0fafca                imul      ecx,edx
  00472fc8  03d9                  add       ebx,ecx
  00472fca  53                    push      ebx
  00472fcb  e8402a0d00            call      00545A10h
  00472fd0  3df4010000            cmp       eax,1F4h
  00472fd5  7f11                  jg        short 00472FE8h
  00472fd7  3d2c010000            cmp       eax,12Ch
  00472fdc  7e44                  jle       short 00473022h
  00472fde  6683be9800000001      cmp       word [esi+98h],1
  00472fe6  753a                  jne       short 00473022h

LAB_00472fe8:
  00472fe8  8b4c2418              mov       ecx,[esp+18h]
  00472fec  41                    inc       ecx
  00472fed  6683be9800000002      cmp       word [esi+98h],2
  00472ff5  894c2418              mov       [esp+18h],ecx
  00472ff9  7527                  jne       short 00473022h
  00472ffb  8b4500                mov       eax,[ebp]
  00472ffe  a900000004            test      eax,4000000h
  00473003  751d                  jne       short 00473022h
  00473005  6aff                  push      0FFFFFFFFh
  00473007  6a00                  push      0
  00473009  689d000000            push      9Dh
  0047300e  6a00                  push      0
  00473010  0d00000004            or        eax,4000000h
  00473015  57                    push      edi
  00473016  55                    push      ebp
  00473017  894500                mov       [ebp],eax
  0047301a  e8217c0000            call      0047AC40h
  0047301f  83c418                add       esp,18h

LAB_00473022:
  00473022  8b442414              mov       eax,[esp+14h]
  00473026  81c59c000000          add       ebp,9Ch
  0047302c  48                    dec       eax
  0047302d  89442414              mov       [esp+14h],eax
  00473031  0f853affffff          jne       near 00472F71h
  00473037  33ff                  xor       edi,edi
  00473039  b9c0935e00            mov       ecx,5E93C0h
  0047303e  897c2414              mov       [esp+14h],edi
  00473042  ba04000000            mov       edx,4

LAB_00473047:
  00473047  8b01                  mov       eax,[ecx]
  00473049  a900000080            test      eax,80000000h
  0047304e  7408                  je        short 00473058h
  00473050  a900000150            test      eax,50010000h
  00473055  7501                  jne       short 00473058h
  00473057  47                    inc       edi

LAB_00473058:
  00473058  81c19c000000          add       ecx,9Ch
  0047305e  4a                    dec       edx
  0047305f  75e6                  jne       short 00473047h
  00473061  897c2414              mov       [esp+14h],edi
  00473065  33db                  xor       ebx,ebx
  00473067  b9c0935e00            mov       ecx,5E93C0h
  0047306c  ba04000000            mov       edx,4

LAB_00473071:
  00473071  8b01                  mov       eax,[ecx]
  00473073  a900000080            test      eax,80000000h
  00473078  740f                  je        short 00473089h
  0047307a  a900000150            test      eax,50010000h
  0047307f  7508                  jne       short 00473089h
  00473081  a900000004            test      eax,4000000h
  00473086  7401                  je        short 00473089h
  00473088  43                    inc       ebx

LAB_00473089:
  00473089  81c19c000000          add       ecx,9Ch
  0047308f  4a                    dec       edx
  00473090  75df                  jne       short 00473071h
  00473092  8b461c                mov       eax,[esi+1Ch]
  00473095  8b15309a5e00          mov       edx,[5E9A30h]
  0047309b  8b4e20                mov       ecx,[esi+20h]
  0047309e  8b2d389a5e00          mov       ebp,[5E9A38h]
  004730a4  c1f80c                sar       eax,0Ch
  004730a7  c1fa0c                sar       edx,0Ch
  004730aa  2bc2                  sub       eax,edx
  004730ac  8b15349a5e00          mov       edx,[5E9A34h]
  004730b2  c1f90c                sar       ecx,0Ch
  004730b5  c1fa0c                sar       edx,0Ch
  004730b8  2bca                  sub       ecx,edx
  004730ba  8b5624                mov       edx,[esi+24h]
  004730bd  c1fa0c                sar       edx,0Ch
  004730c0  c1fd0c                sar       ebp,0Ch
  004730c3  2bd5                  sub       edx,ebp
  004730c5  8be8                  mov       ebp,eax
  004730c7  0fafe8                imul      ebp,eax
  004730ca  8bc1                  mov       eax,ecx
  004730cc  33ff                  xor       edi,edi
  004730ce  0fafc1                imul      eax,ecx
  004730d1  8bca                  mov       ecx,edx
  004730d3  03e8                  add       ebp,eax
  004730d5  0fafca                imul      ecx,edx
  004730d8  03e9                  add       ebp,ecx
  004730da  55                    push      ebp
  004730db  e830290d00            call      00545A10h
  004730e0  3d2c010000            cmp       eax,12Ch
  004730e5  7e05                  jle       short 004730ECh
  004730e7  bf01000000            mov       edi,1

LAB_004730ec:
  004730ec  33ed                  xor       ebp,ebp
  004730ee  663bdd                cmp       bx,bp
  004730f1  7425                  je        short 00473118h
  004730f3  8b442414              mov       eax,[esp+14h]
  004730f7  663bc5                cmp       ax,bp
  004730fa  741c                  je        short 00473118h
  004730fc  663bd8                cmp       bx,ax
  004730ff  7517                  jne       short 00473118h
  00473101  6683be9800000002      cmp       word [esi+98h],2
  00473109  750d                  jne       short 00473118h
  0047310b  c605804b600001        mov       byte [604B80h],1
  00473112  66c74614ce00          mov       word [esi+14h],0CEh

LAB_00473118:
  00473118  e8533cffff            call      00466D70h
  0047311d  6639442418            cmp       [esp+18h],ax
  00473122  751d                  jne       short 00473141h
  00473124  6683ff01              cmp       di,1
  00473128  7517                  jne       short 00473141h
  0047312a  6683be9800000002      cmp       word [esi+98h],2
  00473132  740d                  je        short 00473141h
  00473134  c605804b600001        mov       byte [604B80h],1
  0047313b  66c74614ce00          mov       word [esi+14h],0CEh

LAB_00473141:
  00473141  803d804b600001        cmp       byte [604B80h],1
  00473148  750a                  jne       short 00473154h
  0047314a  66c74614ce00          mov       word [esi+14h],0CEh
  00473150  eb02                  jmp       short 00473154h

LAB_00473152:
  00473152  33ed                  xor       ebp,ebp

LAB_00473154:
  00473154  668b4614              mov       ax,[esi+14h]
  00473158  663dce00              cmp       ax,0CEh
  0047315c  0f8590010000          jne       near 004732F2h
  00473162  c605804b600001        mov       byte [604B80h],1
  00473169  668b8698000000        mov       ax,[esi+98h]
  00473170  663d0100              cmp       ax,1
  00473174  66c746140a00          mov       word [esi+14h],0Ah
  0047317a  66c746160100          mov       word [esi+16h],1
  00473180  740a                  je        short 0047318Ch
  00473182  663d0200              cmp       ax,2
  00473186  0f855d010000          jne       near 004732E9h

LAB_0047318c:
  0047318c  8b561c                mov       edx,[esi+1Ch]
  0047318f  891598195700          mov       [571998h],edx
  00473195  8b4620                mov       eax,[esi+20h]
  00473198  a39c195700            mov       [57199Ch],eax
  0047319d  8b4e24                mov       ecx,[esi+24h]
  004731a0  890da0195700          mov       [5719A0h],ecx
  004731a6  e8953f0d00            call      00547140h
  004731ab  99                    cdq
  004731ac  b9c8000000            mov       ecx,0C8h
  004731b1  f7f9                  idiv      ecx
  004731b3  66035630              add       dx,[esi+30h]
  004731b7  81c290010000          add       edx,190h
  004731bd  66891580195700        mov       [571980h],dx
  004731c4  e8773f0d00            call      00547140h
  004731c9  99                    cdq
  004731ca  b9c8000000            mov       ecx,0C8h
  004731cf  f7f9                  idiv      ecx
  004731d1  a194ca5e00            mov       eax,[5ECA94h]
  004731d6  66035632              add       dx,[esi+32h]
  004731da  81c22c010000          add       edx,12Ch
  004731e0  66891582195700        mov       [571982h],dx
  004731e7  668b5634              mov       dx,[esi+34h]
  004731eb  66891584195700        mov       [571984h],dx
  004731f2  8b08                  mov       ecx,[eax]
  004731f4  890d88195700          mov       [571988h],ecx
  004731fa  8b5004                mov       edx,[eax+4]
  004731fd  89158c195700          mov       [57198Ch],edx
  00473203  8b4008                mov       eax,[eax+8]
  00473206  a390195700            mov       [571990h],eax
  0047320b  e8303f0d00            call      00547140h
  00473210  99                    cdq
  00473211  b9c8000000            mov       ecx,0C8h
  00473216  33f6                  xor       esi,esi
  00473218  f7f9                  idiv      ecx
  0047321a  c70594ca5e0088195700  mov       dword [5ECA94h],571988h
  00473224  c705e09a5e0080195700  mov       dword [5E9AE0h],571980h
  0047322e  c705bcde5e0098195700  mov       dword [5EDEBCh],571998h
  00473238  c605844b600000        mov       byte [604B84h],0
  0047323f  c605854b600000        mov       byte [604B85h],0
  00473246  c605864b600000        mov       byte [604B86h],0
  0047324d  c605874b600000        mov       byte [604B87h],0
  00473254  bda0ca5e00            mov       ebp,5ECAA0h
  00473259  81c2e8030000          add       edx,3E8h
  0047325f  33ff                  xor       edi,edi
  00473261  891590195700          mov       [571990h],edx
  00473267  8b154ce55d00          mov       edx,[5DE54Ch]
  0047326d  89157c4b6000          mov       [604B7Ch],edx
  00473273  c7054ce55d0003000000  mov       dword [5DE54Ch],3
  0047327d  ba20000000            mov       edx,20h

LAB_00473282:
  00473282  f7450000000004        test      dword [ebp],4000000h
  00473289  741c                  je        short 004732A7h
  0047328b  8bc7                  mov       eax,edi
  0047328d  b301                  mov       bl,1
  0047328f  0fbfce                movsx     ecx,si
  00473292  c1f803                sar       eax,3
  00473295  d2e3                  shl       bl,cl
  00473297  8a88844b6000          mov       cl,[eax+604B84h]
  0047329d  8d80844b6000          lea       eax,[eax+604B84h]
  004732a3  0acb                  or        cl,bl
  004732a5  8808                  mov       [eax],cl

LAB_004732a7:
  004732a7  46                    inc       esi
  004732a8  6683fe08              cmp       si,8
  004732ac  7502                  jne       short 004732B0h
  004732ae  33f6                  xor       esi,esi

LAB_004732b0:
  004732b0  47                    inc       edi
  004732b1  81c59c000000          add       ebp,9Ch
  004732b7  4a                    dec       edx
  004732b8  75c8                  jne       short 00473282h
  004732ba  6a00                  push      0
  004732bc  e83f200c00            call      00535300h
  004732c1  83c404                add       esp,4
  004732c4  b8e0a55f00            mov       eax,5FA5E0h
  004732c9  ba80000000            mov       edx,80h

LAB_004732ce:
  004732ce  8a4805                mov       cl,[eax+5]
  004732d1  80f901                cmp       cl,1
  004732d4  720b                  jb        short 004732E1h
  004732d6  80f907                cmp       cl,7
  004732d9  7706                  ja        short 004732E1h
  004732db  8120fffffffb          and       dword [eax],0FBFFFFFFh

LAB_004732e1:
  004732e1  059c000000            add       eax,9Ch
  004732e6  4a                    dec       edx
  004732e7  75e5                  jne       short 004732CEh

LAB_004732e9:
  004732e9  66b80100              mov       ax,1
  004732ed  5f                    pop       edi
  004732ee  5e                    pop       esi
  004732ef  5d                    pop       ebp
  004732f0  5b                    pop       ebx
  004732f1  c3                    ret

LAB_004732f2:
  004732f2  663dd200              cmp       ax,0D2h
  004732f6  0f858b000000          jne       near 00473387h
  004732fc  8b0e                  mov       ecx,[esi]
  004732fe  8b7e6c                mov       edi,[esi+6Ch]
  00473301  80cd20                or        ch,20h
  00473304  3bfd                  cmp       edi,ebp
  00473306  890e                  mov       [esi],ecx
  00473308  7505                  jne       short 0047330Fh
  0047330a  bf58e55d00            mov       edi,5DE558h

LAB_0047330f:
  0047330f  66c747180600          mov       word [edi+18h],6
  00473315  66c74614d300          mov       word [esi+14h],0D3h
  0047331b  66896e16              mov       [esi+16h],bp
  0047331f  55                    push      ebp
  00473320  66896f1a              mov       [edi+1Ah],bp
  00473324  e8d71f0c00            call      00535300h
  00473329  8b1e                  mov       ebx,[esi]
  0047332b  33c9                  xor       ecx,ecx
  0047332d  81e3fffffffb          and       ebx,0FBFFFFFFh
  00473333  83c404                add       esp,4
  00473336  891e                  mov       [esi],ebx
  00473338  8b15b4876000          mov       edx,[6087B4h]
  0047333e  8a4f08                mov       cl,[edi+8]
  00473341  68000a0000            push      0A00h
  00473346  8b02                  mov       eax,[edx]
  00473348  68000a0000            push      0A00h
  0047334d  c1e10d                shl       ecx,0Dh
  00473350  8a8c0150010f00        mov       cl,[ecx+eax+0F0150h]
  00473357  80e107                and       cl,7
  0047335a  80f902                cmp       cl,2
  0047335d  7514                  jne       short 00473373h
  0047335f  6800080000            push      800h
  00473364  6a06                  push      6
  00473366  e8359b0c00            call      0053CEA0h
  0047336b  83c410                add       esp,10h
  0047336e  5f                    pop       edi
  0047336f  5e                    pop       esi
  00473370  5d                    pop       ebp
  00473371  5b                    pop       ebx
  00473372  c3                    ret

LAB_00473373:
  00473373  6800040000            push      400h
  00473378  6a07                  push      7
  0047337a  e8219b0c00            call      0053CEA0h
  0047337f  83c410                add       esp,10h
  00473382  5f                    pop       edi
  00473383  5e                    pop       esi
  00473384  5d                    pop       ebp
  00473385  5b                    pop       ebx
  00473386  c3                    ret

LAB_00473387:
  00473387  663dd300              cmp       ax,0D3h
  0047338b  0f8510010000          jne       near 004734A1h
  00473391  8b466c                mov       eax,[esi+6Ch]
  00473394  3bc5                  cmp       eax,ebp
  00473396  7505                  jne       short 0047339Dh
  00473398  b858e55d00            mov       eax,5DE558h

LAB_0047339d:
  0047339d  33d2                  xor       edx,edx
  0047339f  66c740180600          mov       word [eax+18h],6
  004733a5  8a5004                mov       dl,[eax+4]
  004733a8  8b0c9580965e00        mov       ecx,[edx*4+5E9680h]
  004733af  8b15b4876000          mov       edx,[6087B4h]
  004733b5  8b7918                mov       edi,[ecx+18h]
  004733b8  8b1a                  mov       ebx,[edx]
  004733ba  33c9                  xor       ecx,ecx
  004733bc  8a4808                mov       cl,[eax+8]
  004733bf  83c178                add       ecx,78h
  004733c2  c1e10d                shl       ecx,0Dh
  004733c5  03cb                  add       ecx,ebx
  004733c7  8a8950010000          mov       cl,[ecx+150h]
  004733cd  8ad1                  mov       dl,cl
  004733cf  80e207                and       dl,7
  004733d2  80fa02                cmp       dl,2
  004733d5  0f8596000000          jne       near 00473471h
  004733db  f6c108                test      cl,8
  004733de  668b481a              mov       cx,[eax+1Ah]
  004733e2  0fbfd1                movsx     edx,cx
  004733e5  7527                  jne       short 0047340Eh
  004733e7  42                    inc       edx
  004733e8  83fa05                cmp       edx,5
  004733eb  7d0d                  jge       short 004733FAh
  004733ed  41                    inc       ecx
  004733ee  6689481a              mov       [eax+1Ah],cx
  004733f2  6633c0                xor       ax,ax
  004733f5  5f                    pop       edi
  004733f6  5e                    pop       esi
  004733f7  5d                    pop       ebp
  004733f8  5b                    pop       ebx
  004733f9  c3                    ret

LAB_004733fa:
  004733fa  66396e16              cmp       [esi+16h],bp
  004733fe  7554                  jne       short 00473454h
  00473400  66c746160100          mov       word [esi+16h],1
  00473406  66c7401a0200          mov       word [eax+1Ah],2
  0047340c  eb25                  jmp       short 00473433h

LAB_0047340e:
  0047340e  42                    inc       edx
  0047340f  83fa0b                cmp       edx,0Bh
  00473412  7d0d                  jge       short 00473421h
  00473414  41                    inc       ecx
  00473415  6689481a              mov       [eax+1Ah],cx
  00473419  6633c0                xor       ax,ax
  0047341c  5f                    pop       edi
  0047341d  5e                    pop       esi
  0047341e  5d                    pop       ebp
  0047341f  5b                    pop       ebx
  00473420  c3                    ret

LAB_00473421:
  00473421  66396e16              cmp       [esi+16h],bp
  00473425  752d                  jne       short 00473454h
  00473427  66c746160100          mov       word [esi+16h],1
  0047342d  66c7401a0300          mov       word [eax+1Ah],3

LAB_00473433:
  00473433  68000a0000            push      0A00h
  00473438  68000a0000            push      0A00h
  0047343d  6800080000            push      800h
  00473442  6a06                  push      6
  00473444  e8579a0c00            call      0053CEA0h
  00473449  83c410                add       esp,10h
  0047344c  6633c0                xor       ax,ax
  0047344f  5f                    pop       edi
  00473450  5e                    pop       esi
  00473451  5d                    pop       ebp
  00473452  5b                    pop       ebx
  00473453  c3                    ret

LAB_00473454:
  00473454  810800000002          or        dword [eax],2000000h
  0047345a  e8a11f0c00            call      00535400h
  0047345f  66c746141400          mov       word [esi+14h],14h
  00473465  66896e16              mov       [esi+16h],bp
  00473469  6633c0                xor       ax,ax
  0047346c  5f                    pop       edi
  0047346d  5e                    pop       esi
  0047346e  5d                    pop       ebp
  0047346f  5b                    pop       ebx
  00473470  c3                    ret

LAB_00473471:
  00473471  668b481a              mov       cx,[eax+1Ah]
  00473475  33d2                  xor       edx,edx
  00473477  668b5716              mov       dx,[edi+16h]
  0047347b  0fbff9                movsx     edi,cx
  0047347e  47                    inc       edi
  0047347f  3bfa                  cmp       edi,edx
  00473481  7d0d                  jge       short 00473490h
  00473483  41                    inc       ecx
  00473484  6689481a              mov       [eax+1Ah],cx
  00473488  6633c0                xor       ax,ax
  0047348b  5f                    pop       edi
  0047348c  5e                    pop       esi
  0047348d  5d                    pop       ebp
  0047348e  5b                    pop       ebx
  0047348f  c3                    ret

LAB_00473490:
  00473490  810800000002          or        dword [eax],2000000h
  00473496  e8651f0c00            call      00535400h
  0047349b  66c746141400          mov       word [esi+14h],14h

LAB_004734a1:
  004734a1  5f                    pop       edi
  004734a2  5e                    pop       esi
  004734a3  5d                    pop       ebp
  004734a4  6633c0                xor       ax,ax
  004734a7  5b                    pop       ebx
  004734a8  c3                    ret
  004734a9  90                    nop
  004734aa  90                    nop
  004734ab  90                    nop
  004734ac  90                    nop
  004734ad  90                    nop
  004734ae  90                    nop
  004734af  90                    nop

35 cible(s) de saut interne : 0x00472ee4, 0x00472f03, 0x00472f52, 0x00472f71, 0x00472fe8, 0x00473022, 0x00473047, 0x00473058, 0x00473071, 0x00473089, 0x004730ec, 0x00473118, 0x00473141, 0x00473152, 0x00473154, 0x0047318c, 0x00473282, 0x004732a7, 0x004732b0, 0x004732ce, 0x004732e1, 0x004732e9, 0x004732f2, 0x0047330f, 0x00473373, 0x00473387, 0x0047339d, 0x004733fa, 0x0047340e, 0x00473421, 0x00473433, 0x00473454, 0x00473471, 0x00473490, 0x004734a1
