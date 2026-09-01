Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004780b0  section .text  offset fichier 0x774b0  1333 octets

  004780b0  83ec28                sub       esp,28h
  004780b3  53                    push      ebx
  004780b4  55                    push      ebp
  004780b5  56                    push      esi
  004780b6  8b742438              mov       esi,[esp+38h]
  004780ba  57                    push      edi
  004780bb  f70600000002          test      dword [esi],2000000h
  004780c1  7431                  je        short 004780F4h
  004780c3  668b4628              mov       ax,[esi+28h]
  004780c7  668bc8                mov       cx,ax
  004780ca  66c1f902              sar       cx,2
  004780ce  2bc1                  sub       eax,ecx
  004780d0  663d0001              cmp       ax,100h
  004780d4  66894628              mov       [esi+28h],ax
  004780d8  6689462a              mov       [esi+2Ah],ax
  004780dc  6689462c              mov       [esi+2Ch],ax
  004780e0  0f8d89040000          jge       near 0047856Fh
  004780e6  c70600000000          mov       dword [esi],0
  004780ec  5f                    pop       edi
  004780ed  5e                    pop       esi
  004780ee  5d                    pop       ebp
  004780ef  5b                    pop       ebx
  004780f0  83c428                add       esp,28h
  004780f3  c3                    ret

LAB_004780f4:
  004780f4  33ff                  xor       edi,edi
  004780f6  66397e10              cmp       [esi+10h],di
  004780fa  756f                  jne       short 0047816Bh
  004780fc  8b4674                mov       eax,[esi+74h]
  004780ff  57                    push      edi
  00478100  56                    push      esi
  00478101  8b10                  mov       edx,[eax]
  00478103  80ca80                or        dl,80h
  00478106  8910                  mov       [eax],edx
  00478108  804e3606              or        byte [esi+36h],6
  0047810c  66c7462eecff          mov       word [esi+2Eh],0FFECh
  00478112  e889caffff            call      00474BA0h
  00478117  83c408                add       esp,8
  0047811a  8bd8                  mov       ebx,eax
  0047811c  895e78                mov       [esi+78h],ebx
  0047811f  895e70                mov       [esi+70h],ebx
  00478122  53                    push      ebx
  00478123  e8980a0c00            call      00538BC0h
  00478128  83c404                add       esp,4
  0047812b  8d561c                lea       edx,[esi+1Ch]
  0047812e  66894612              mov       [esi+12h],ax
  00478132  0fbf4332              movsx     eax,word [ebx+32h]
  00478136  0fbf4b34              movsx     ecx,word [ebx+34h]
  0047813a  52                    push      edx
  0047813b  6800120000            push      1200h
  00478140  6800080000            push      800h
  00478145  897e28                mov       [esi+28h],edi
  00478148  6a0d                  push      0Dh
  0047814a  66894614              mov       [esi+14h],ax
  0047814e  66894e16              mov       [esi+16h],cx
  00478152  66894632              mov       [esi+32h],ax
  00478156  66894e34              mov       [esi+34h],cx
  0047815a  66897e28              mov       [esi+28h],di
  0047815e  897e38                mov       [esi+38h],edi
  00478161  e8ba590c00            call      0053DB20h
  00478166  83c410                add       esp,10h
  00478169  eb03                  jmp       short 0047816Eh

LAB_0047816b:
  0047816b  8b5e78                mov       ebx,[esi+78h]

LAB_0047816e:
  0047816e  0fbf4612              movsx     eax,word [esi+12h]
  00478172  8b4b20                mov       ecx,[ebx+20h]
  00478175  24fe                  and       al,0FEh
  00478177  c1e00b                shl       eax,0Bh
  0047817a  03c1                  add       eax,ecx
  0047817c  66837e1010            cmp       word [esi+10h],10h
  00478181  894620                mov       [esi+20h],eax
  00478184  7d2d                  jge       short 004781B3h
  00478186  66814628c000          add       word [esi+28h],0C0h
  0047818c  668b4628              mov       ax,[esi+28h]
  00478190  6689462a              mov       [esi+2Ah],ax
  00478194  6689462c              mov       [esi+2Ch],ax
  00478198  8b4638                mov       eax,[esi+38h]
  0047819b  3d80808000            cmp       eax,808080h
  004781a0  0f83dd000000          jae       near 00478283h
  004781a6  0508080800            add       eax,80808h
  004781ab  894638                mov       [esi+38h],eax
  004781ae  e9d0000000            jmp       00478283h

LAB_004781b3:
  004781b3  8b4674                mov       eax,[esi+74h]
  004781b6  8b08                  mov       ecx,[eax]
  004781b8  80e17f                and       cl,7Fh
  004781bb  8908                  mov       [eax],ecx
  004781bd  0fbf4e10              movsx     ecx,word [esi+10h]
  004781c1  c1e108                shl       ecx,8
  004781c4  51                    push      ecx
  004781c5  e84cd80c00            call      00545A16h
  004781ca  8bd0                  mov       edx,eax
  004781cc  b8000c0000            mov       eax,0C00h
  004781d1  c1fa03                sar       edx,3
  004781d4  2bc2                  sub       eax,edx
  004781d6  66894628              mov       [esi+28h],ax
  004781da  6689462a              mov       [esi+2Ah],ax
  004781de  6689462c              mov       [esi+2Ch],ax
  004781e2  8b06                  mov       eax,[esi]
  004781e4  a900008000            test      eax,800000h
  004781e9  7477                  je        short 00478262h
  004781eb  8b4e74                mov       ecx,[esi+74h]
  004781ee  33d2                  xor       edx,edx
  004781f0  8a5108                mov       dl,[ecx+8]
  004781f3  8bca                  mov       ecx,edx
  004781f5  8b15b4876000          mov       edx,[6087B4h]
  004781fb  8d6978                lea       ebp,[ecx+78h]
  004781fe  c1e50d                shl       ebp,0Dh
  00478201  032a                  add       ebp,[edx]
  00478203  8d14c9                lea       edx,[ecx+ecx*8]
  00478206  8d0c51                lea       ecx,[ecx+edx*2]
  00478209  33d2                  xor       edx,edx
  0047820b  8a148d88915e00        mov       dl,[ecx*4+5E9188h]
  00478212  c1e204                shl       edx,4
  00478215  668b8a204a6000        mov       cx,[edx+604A20h]
  0047821c  66858dc41f0000        test      [ebp+1FC4h],cx
  00478223  7507                  jne       short 0047822Ch
  00478225  0d00000002            or        eax,2000000h
  0047822a  8906                  mov       [esi],eax

LAB_0047822c:
  0047822c  a1f02c5f00            mov       eax,[5F2CF0h]
  00478231  33d2                  xor       edx,edx
  00478233  b914000000            mov       ecx,14h
  00478238  f7f1                  div       ecx
  0047823a  85d2                  test      edx,edx
  0047823c  7532                  jne       short 00478270h
  0047823e  668b854e010000        mov       ax,[ebp+14Eh]
  00478245  663d0400              cmp       ax,4
  00478249  7c0c                  jl        short 00478257h
  0047824b  83c0fc                add       eax,0FFFFFFFCh
  0047824e  6689854e010000        mov       [ebp+14Eh],ax
  00478255  eb19                  jmp       short 00478270h

LAB_00478257:
  00478257  6689bd4e010000        mov       [ebp+14Eh],di
  0047825e  8b06                  mov       eax,[esi]
  00478260  eb07                  jmp       short 00478269h

LAB_00478262:
  00478262  66837e1078            cmp       word [esi+10h],78h
  00478267  7c07                  jl        short 00478270h

LAB_00478269:
  00478269  0d00000002            or        eax,2000000h
  0047826e  8906                  mov       [esi],eax

LAB_00478270:
  00478270  56                    push      esi
  00478271  e89aceffff            call      00475110h
  00478276  83c404                add       esp,4
  00478279  85c0                  test      eax,eax
  0047827b  7406                  je        short 00478283h
  0047827d  810e00000002          or        dword [esi],2000000h

LAB_00478283:
  00478283  bd90010000            mov       ebp,190h
  00478288  b803000000            mov       eax,3
  0047828d  897c2410              mov       [esp+10h],edi
  00478291  8b7c243c              mov       edi,[esp+3Ch]
  00478295  896c241c              mov       [esp+1Ch],ebp
  00478299  89442424              mov       [esp+24h],eax

LAB_0047829d:
  0047829d  48                    dec       eax
  0047829e  7426                  je        short 004782C6h
  004782a0  48                    dec       eax
  004782a1  7413                  je        short 004782B6h
  004782a3  48                    dec       eax
  004782a4  7530                  jne       short 004782D6h
  004782a6  b820000000            mov       eax,20h
  004782ab  bf00f45f00            mov       edi,5FF400h
  004782b0  8944243c              mov       [esp+3Ch],eax
  004782b4  eb24                  jmp       short 004782DAh

LAB_004782b6:
  004782b6  b880000000            mov       eax,80h
  004782bb  bfe0de5e00            mov       edi,5EDEE0h
  004782c0  8944243c              mov       [esp+3Ch],eax
  004782c4  eb14                  jmp       short 004782DAh

LAB_004782c6:
  004782c6  b880000000            mov       eax,80h
  004782cb  bf402d5f00            mov       edi,5F2D40h
  004782d0  8944243c              mov       [esp+3Ch],eax
  004782d4  eb04                  jmp       short 004782DAh

LAB_004782d6:
  004782d6  8b44243c              mov       eax,[esp+3Ch]

LAB_004782da:
  004782da  85c0                  test      eax,eax
  004782dc  0f84a8000000          je        near 0047838Ah
  004782e2  89442420              mov       [esp+20h],eax

LAB_004782e6:
  004782e6  8b07                  mov       eax,[edi]
  004782e8  a900000080            test      eax,80000000h
  004782ed  747d                  je        short 0047836Ch
  004782ef  8bd0                  mov       edx,eax
  004782f1  81e280080000          and       edx,880h
  004782f7  81fa80080000          cmp       edx,880h
  004782fd  756d                  jne       short 0047836Ch
  004782ff  a900000040            test      eax,40000000h
  00478304  7566                  jne       short 0047836Ch
  00478306  8b471c                mov       eax,[edi+1Ch]
  00478309  8b531c                mov       edx,[ebx+1Ch]
  0047830c  8b4f20                mov       ecx,[edi+20h]
  0047830f  2bc2                  sub       eax,edx
  00478311  c1f80c                sar       eax,0Ch
  00478314  8944242c              mov       [esp+2Ch],eax
  00478318  8b6b20                mov       ebp,[ebx+20h]
  0047831b  8b5724                mov       edx,[edi+24h]
  0047831e  2bcd                  sub       ecx,ebp
  00478320  c1f90c                sar       ecx,0Ch
  00478323  894c2430              mov       [esp+30h],ecx
  00478327  8b6b24                mov       ebp,[ebx+24h]
  0047832a  2bd5                  sub       edx,ebp
  0047832c  89442428              mov       [esp+28h],eax
  00478330  c1fa0c                sar       edx,0Ch
  00478333  0fafc0                imul      eax,eax
  00478336  894c2414              mov       [esp+14h],ecx
  0047833a  0fafc9                imul      ecx,ecx
  0047833d  89542418              mov       [esp+18h],edx
  00478341  0fafd2                imul      edx,edx
  00478344  8944242c              mov       [esp+2Ch],eax
  00478348  03c1                  add       eax,ecx
  0047834a  03c2                  add       eax,edx
  0047834c  894c2430              mov       [esp+30h],ecx
  00478350  50                    push      eax
  00478351  89542438              mov       [esp+38h],edx
  00478355  e8b6d60c00            call      00545A10h
  0047835a  8b6c241c              mov       ebp,[esp+1Ch]
  0047835e  3be8                  cmp       ebp,eax
  00478360  7e0a                  jle       short 0047836Ch
  00478362  8be8                  mov       ebp,eax
  00478364  897c2410              mov       [esp+10h],edi
  00478368  896c241c              mov       [esp+1Ch],ebp

LAB_0047836c:
  0047836c  8b54243c              mov       edx,[esp+3Ch]
  00478370  8b442420              mov       eax,[esp+20h]
  00478374  4a                    dec       edx
  00478375  81c79c000000          add       edi,9Ch
  0047837b  48                    dec       eax
  0047837c  8954243c              mov       [esp+3Ch],edx
  00478380  89442420              mov       [esp+20h],eax
  00478384  0f855cffffff          jne       near 004782E6h

LAB_0047838a:
  0047838a  8b442424              mov       eax,[esp+24h]
  0047838e  48                    dec       eax
  0047838f  89442424              mov       [esp+24h],eax
  00478393  0f8504ffffff          jne       near 0047829Dh
  00478399  8b442410              mov       eax,[esp+10h]
  0047839d  85c0                  test      eax,eax
  0047839f  7443                  je        short 004783E4h
  004783a1  8b7c2428              mov       edi,[esp+28h]
  004783a5  8b5c2418              mov       ebx,[esp+18h]
  004783a9  8bc7                  mov       eax,edi
  004783ab  8bcb                  mov       ecx,ebx
  004783ad  0fafc7                imul      eax,edi
  004783b0  0fafcb                imul      ecx,ebx
  004783b3  03c1                  add       eax,ecx
  004783b5  50                    push      eax
  004783b6  e855d60c00            call      00545A10h
  004783bb  53                    push      ebx
  004783bc  57                    push      edi
  004783bd  89442444              mov       [esp+44h],eax
  004783c1  e85cd60c00            call      00545A22h
  004783c6  8b54243c              mov       edx,[esp+3Ch]
  004783ca  0500040000            add       eax,400h
  004783cf  66894614              mov       [esi+14h],ax
  004783d3  8b442414              mov       eax,[esp+14h]
  004783d7  52                    push      edx
  004783d8  50                    push      eax
  004783d9  e844d60c00            call      00545A22h
  004783de  66894616              mov       [esi+16h],ax
  004783e2  eb15                  jmp       short 004783F9h

LAB_004783e4:
  004783e4  668b4b32              mov       cx,[ebx+32h]
  004783e8  6681c10004            add       cx,400h
  004783ed  66894e14              mov       [esi+14h],cx
  004783f1  668b5334              mov       dx,[ebx+34h]
  004783f5  66895616              mov       [esi+16h],dx

LAB_004783f9:
  004783f9  668b7e32              mov       di,[esi+32h]
  004783fd  668b4614              mov       ax,[esi+14h]
  00478401  668b5e34              mov       bx,[esi+34h]
  00478405  668b4e16              mov       cx,[esi+16h]
  00478409  2bc7                  sub       eax,edi
  0047840b  2bcb                  sub       ecx,ebx
  0047840d  25ff0f0000            and       eax,0FFFh
  00478412  81e1ff0f0000          and       ecx,0FFFh
  00478418  3d00080000            cmp       eax,800h
  0047841d  7d07                  jge       short 00478426h
  0047841f  8bd0                  mov       edx,eax
  00478421  c1fa02                sar       edx,2
  00478424  eb0c                  jmp       short 00478432h

LAB_00478426:
  00478426  ba00100000            mov       edx,1000h
  0047842b  2bd0                  sub       edx,eax
  0047842d  c1fa02                sar       edx,2
  00478430  f7da                  neg       edx

LAB_00478432:
  00478432  03fa                  add       edi,edx
  00478434  81f900080000          cmp       ecx,800h
  0047843a  66897e32              mov       [esi+32h],di
  0047843e  7d07                  jge       short 00478447h
  00478440  8bd1                  mov       edx,ecx
  00478442  c1fa02                sar       edx,2
  00478445  eb0c                  jmp       short 00478453h

LAB_00478447:
  00478447  ba00100000            mov       edx,1000h
  0047844c  2bd1                  sub       edx,ecx
  0047844e  c1fa02                sar       edx,2
  00478451  f7da                  neg       edx

LAB_00478453:
  00478453  8b7c2410              mov       edi,[esp+10h]
  00478457  03da                  add       ebx,edx
  00478459  85ff                  test      edi,edi
  0047845b  66895e34              mov       [esi+34h],bx
  0047845f  0f840a010000          je        near 0047856Fh
  00478465  0fbf5612              movsx     edx,word [esi+12h]
  00478469  8bda                  mov       ebx,edx
  0047846b  c1fb02                sar       ebx,2
  0047846e  2bd3                  sub       edx,ebx
  00478470  3bea                  cmp       ebp,edx
  00478472  0f8df7000000          jge       near 0047856Fh
  00478478  0500020000            add       eax,200h
  0047847d  25ff0f0000            and       eax,0FFFh
  00478482  3d00040000            cmp       eax,400h
  00478487  0f8de2000000          jge       near 0047856Fh
  0047848d  81c100020000          add       ecx,200h
  00478493  81e1ff0f0000          and       ecx,0FFFh
  00478499  81f900040000          cmp       ecx,400h
  0047849f  0f8dca000000          jge       near 0047856Fh
  004784a5  8b4628                mov       eax,[esi+28h]
  004784a8  8b4e2c                mov       ecx,[esi+2Ch]
  004784ab  8b5638                mov       edx,[esi+38h]
  004784ae  83c71c                add       edi,1Ch
  004784b1  66c7461a0100          mov       word [esi+1Ah],1
  004784b7  66c7869a0000000020    mov       word [esi+9Ah],2000h
  004784c0  89442414              mov       [esp+14h],eax
  004784c4  894c2418              mov       [esp+18h],ecx
  004784c8  8954243c              mov       [esp+3Ch],edx
  004784cc  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  004784d3  c7462800100010        mov       dword [esi+28h],10001000h
  004784da  66c7462c1f00          mov       word [esi+2Ch],1Fh
  004784e0  897c2428              mov       [esp+28h],edi
  004784e4  bd04000000            mov       ebp,4

LAB_004784e9:
  004784e9  e852ec0c00            call      00547140h
  004784ee  0fbf4e32              movsx     ecx,word [esi+32h]
  004784f2  25ff010000            and       eax,1FFh
  004784f7  8dbc0800fbffff        lea       edi,[eax+ecx-500h]
  004784fe  e83dec0c00            call      00547140h
  00478503  0fbf5634              movsx     edx,word [esi+34h]
  00478507  25ff010000            and       eax,1FFh
  0047850c  8d9c1000ffffff        lea       ebx,[eax+edx-100h]
  00478513  e828ec0c00            call      00547140h
  00478518  83e00f                and       eax,0Fh
  0047851b  83c014                add       eax,14h
  0047851e  50                    push      eax
  0047851f  53                    push      ebx
  00478520  8d4650                lea       eax,[esi+50h]
  00478523  57                    push      edi
  00478524  50                    push      eax
  00478525  e8a6a50b00            call      00532AD0h
  0047852a  8b4c2438              mov       ecx,[esp+38h]
  0047852e  83c410                add       esp,10h
  00478531  33c0                  xor       eax,eax
  00478533  8a460b                mov       al,[esi+0Bh]
  00478536  6aff                  push      0FFFFFFFFh
  00478538  50                    push      eax
  00478539  68bf000000            push      0BFh
  0047853e  68a81b5500            push      551BA8h
  00478543  51                    push      ecx
  00478544  56                    push      esi
  00478545  e8f6260000            call      0047AC40h
  0047854a  83c418                add       esp,18h
  0047854d  4d                    dec       ebp
  0047854e  7599                  jne       short 004784E9h
  00478550  8b442418              mov       eax,[esp+18h]
  00478554  8b542414              mov       edx,[esp+14h]
  00478558  8b4c243c              mov       ecx,[esp+3Ch]
  0047855c  89462c                mov       [esi+2Ch],eax
  0047855f  8b442410              mov       eax,[esp+10h]
  00478563  895628                mov       [esi+28h],edx
  00478566  894e38                mov       [esi+38h],ecx
  00478569  810800000040          or        dword [eax],40000000h

LAB_0047856f:
  0047856f  0fbf4612              movsx     eax,word [esi+12h]
  00478573  0fbf4e32              movsx     ecx,word [esi+32h]
  00478577  8bd0                  mov       edx,eax
  00478579  8b7e78                mov       edi,[esi+78h]
  0047857c  c1fa02                sar       edx,2
  0047857f  2bc2                  sub       eax,edx
  00478581  81e900040000          sub       ecx,400h
  00478587  50                    push      eax
  00478588  8d542430              lea       edx,[esp+30h]
  0047858c  0fbf4634              movsx     eax,word [esi+34h]
  00478590  50                    push      eax
  00478591  51                    push      ecx
  00478592  52                    push      edx
  00478593  e838a50b00            call      00532AD0h
  00478598  8b471c                mov       eax,[edi+1Ch]
  0047859b  8b5c243c              mov       ebx,[esp+3Ch]
  0047859f  8b4c2440              mov       ecx,[esp+40h]
  004785a3  8b5620                mov       edx,[esi+20h]
  004785a6  03c3                  add       eax,ebx
  004785a8  03d1                  add       edx,ecx
  004785aa  89461c                mov       [esi+1Ch],eax
  004785ad  8b442444              mov       eax,[esp+44h]
  004785b1  895620                mov       [esi+20h],edx
  004785b4  8b5724                mov       edx,[edi+24h]
  004785b7  03d0                  add       edx,eax
  004785b9  83c410                add       esp,10h
  004785bc  33c0                  xor       eax,eax
  004785be  895624                mov       [esi+24h],edx
  004785c1  8a460b                mov       al,[esi+0Bh]
  004785c4  6aff                  push      0FFFFFFFFh
  004785c6  50                    push      eax
  004785c7  6884000000            push      84h
  004785cc  56                    push      esi
  004785cd  c646040e              mov       byte [esi+4],0Eh
  004785d1  e80a250000            call      0047AAE0h
  004785d6  83c410                add       esp,10h
  004785d9  66ff4610              inc       word [esi+10h]
  004785dd  5f                    pop       edi
  004785de  5e                    pop       esi
  004785df  5d                    pop       ebp
  004785e0  5b                    pop       ebx
  004785e1  83c428                add       esp,28h
  004785e4  c3                    ret

26 cible(s) de saut interne : 0x004780f4, 0x0047816b, 0x0047816e, 0x004781b3, 0x0047822c, 0x00478257, 0x00478262, 0x00478269, 0x00478270, 0x00478283, 0x0047829d, 0x004782b6, 0x004782c6, 0x004782d6, 0x004782da, 0x004782e6, 0x0047836c, 0x0047838a, 0x004783e4, 0x004783f9, 0x00478426, 0x00478432, 0x00478447, 0x00478453, 0x004784e9, 0x0047856f
