C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00486320  section .text  offset fichier 0x85720  1200 octets

  00486320  83ec30                sub       esp,30h
  00486323  53                    push      ebx
  00486324  55                    push      ebp
  00486325  56                    push      esi
  00486326  8b742440              mov       esi,[esp+40h]
  0048632a  33c0                  xor       eax,eax
  0048632c  57                    push      edi
  0048632d  66394610              cmp       [esi+10h],ax
  00486331  66c744242800fc        mov       word [esp+28h],0FC00h
  00486338  66c744242a80ff        mov       word [esp+2Ah],0FF80h
  0048633f  66c744242c3200        mov       word [esp+2Ch],32h
  00486346  66c744242ec800        mov       word [esp+2Eh],0C8h
  0048634d  6689442430            mov       [esp+30h],ax
  00486352  66c74424328000        mov       word [esp+32h],80h
  00486359  7511                  jne       short 0048636Ch
  0048635b  8b16                  mov       edx,[esi]
  0048635d  66c746100100          mov       word [esi+10h],1
  00486363  80ca80                or        dl,80h
  00486366  804e3606              or        byte [esi+36h],6
  0048636a  8916                  mov       [esi],edx

LAB_0048636c:
  0048636c  8b4e1c                mov       ecx,[esi+1Ch]
  0048636f  8b6e78                mov       ebp,[esi+78h]
  00486372  8b5620                mov       edx,[esi+20h]
  00486375  894c2434              mov       [esp+34h],ecx
  00486379  8b4e24                mov       ecx,[esi+24h]
  0048637c  3be8                  cmp       ebp,eax
  0048637e  89542438              mov       [esp+38h],edx
  00486382  894c243c              mov       [esp+3Ch],ecx
  00486386  896c2444              mov       [esp+44h],ebp
  0048638a  0f848e000000          je        near 0048641Eh
  00486390  f60610                test      byte [esi],10h
  00486393  0f8585000000          jne       near 0048641Eh
  00486399  f7450000000050        test      dword [ebp],50000000h
  004863a0  7417                  je        short 004863B9h
  004863a2  89442444              mov       [esp+44h],eax
  004863a6  894574                mov       [ebp+74h],eax
  004863a9  8b7c2444              mov       edi,[esp+44h]
  004863ad  8b6c2444              mov       ebp,[esp+44h]
  004863b1  894678                mov       [esi+78h],eax
  004863b4  e984000000            jmp       0048643Dh

LAB_004863b9:
  004863b9  8b7d1c                mov       edi,[ebp+1Ch]
  004863bc  897574                mov       [ebp+74h],esi
  004863bf  8b561c                mov       edx,[esi+1Ch]
  004863c2  55                    push      ebp
  004863c3  2bfa                  sub       edi,edx
  004863c5  c1ff0c                sar       edi,0Ch
  004863c8  e8f3270b00            call      00538BC0h
  004863cd  8bd8                  mov       ebx,eax
  004863cf  8b4520                mov       eax,[ebp+20h]
  004863d2  8b6d24                mov       ebp,[ebp+24h]
  004863d5  83e3fe                and       ebx,0FFFFFFFEh
  004863d8  8b5620                mov       edx,[esi+20h]
  004863db  83c404                add       esp,4
  004863de  c1e30b                shl       ebx,0Bh
  004863e1  03d8                  add       ebx,eax
  004863e3  8b4624                mov       eax,[esi+24h]
  004863e6  2be8                  sub       ebp,eax
  004863e8  2bda                  sub       ebx,edx
  004863ea  c1fd0c                sar       ebp,0Ch
  004863ed  8bd7                  mov       edx,edi
  004863ef  8bc5                  mov       eax,ebp
  004863f1  0fafd7                imul      edx,edi
  004863f4  0fafc5                imul      eax,ebp
  004863f7  03d0                  add       edx,eax
  004863f9  52                    push      edx
  004863fa  c1fb0c                sar       ebx,0Ch
  004863fd  e80ef60b00            call      00545A10h
  00486402  55                    push      ebp
  00486403  57                    push      edi
  00486404  89442418              mov       [esp+18h],eax
  00486408  e815f60b00            call      00545A22h
  0048640d  8b4c2410              mov       ecx,[esp+10h]
  00486411  8bf8                  mov       edi,eax
  00486413  51                    push      ecx
  00486414  53                    push      ebx
  00486415  e808f60b00            call      00545A22h
  0048641a  8be8                  mov       ebp,eax
  0048641c  eb1f                  jmp       short 0048643Dh

LAB_0048641e:
  0048641e  66ff4610              inc       word [esi+10h]
  00486422  66837e1014            cmp       word [esi+10h],14h
  00486427  7c06                  jl        short 0048642Fh
  00486429  810e00000040          or        dword [esi],40000000h

LAB_0048642f:
  0048642f  0fbf7e32              movsx     edi,word [esi+32h]
  00486433  0fbf6e34              movsx     ebp,word [esi+34h]
  00486437  81ef00040000          sub       edi,400h

LAB_0048643d:
  0048643d  6a0c                  push      0Ch
  0048643f  55                    push      ebp
  00486440  8d542424              lea       edx,[esp+24h]
  00486444  57                    push      edi
  00486445  52                    push      edx
  00486446  e885c60a00            call      00532AD0h
  0048644b  8b4650                mov       eax,[esi+50h]
  0048644e  8b4c242c              mov       ecx,[esp+2Ch]
  00486452  8b5654                mov       edx,[esi+54h]
  00486455  8b5c2434              mov       ebx,[esp+34h]
  00486459  03c1                  add       eax,ecx
  0048645b  8b4c2430              mov       ecx,[esp+30h]
  0048645f  03ca                  add       ecx,edx
  00486461  8b5658                mov       edx,[esi+58h]
  00486464  03d3                  add       edx,ebx
  00486466  8bd8                  mov       ebx,eax
  00486468  c1fb03                sar       ebx,3
  0048646b  2bc3                  sub       eax,ebx
  0048646d  8bd9                  mov       ebx,ecx
  0048646f  c1fb03                sar       ebx,3
  00486472  2bcb                  sub       ecx,ebx
  00486474  8bda                  mov       ebx,edx
  00486476  c1fb03                sar       ebx,3
  00486479  2bd3                  sub       edx,ebx
  0048647b  8b5e1c                mov       ebx,[esi+1Ch]
  0048647e  03d8                  add       ebx,eax
  00486480  894650                mov       [esi+50h],eax
  00486483  895e1c                mov       [esi+1Ch],ebx
  00486486  8bc3                  mov       eax,ebx
  00486488  8b5e20                mov       ebx,[esi+20h]
  0048648b  894e54                mov       [esi+54h],ecx
  0048648e  03d9                  add       ebx,ecx
  00486490  895658                mov       [esi+58h],edx
  00486493  895e20                mov       [esi+20h],ebx
  00486496  8bcb                  mov       ecx,ebx
  00486498  8b5e24                mov       ebx,[esi+24h]
  0048649b  83c410                add       esp,10h
  0048649e  03da                  add       ebx,edx
  004864a0  895e24                mov       [esi+24h],ebx
  004864a3  8bd3                  mov       edx,ebx
  004864a5  8b5c2434              mov       ebx,[esp+34h]
  004864a9  2bc3                  sub       eax,ebx
  004864ab  8b5c2438              mov       ebx,[esp+38h]
  004864af  2bcb                  sub       ecx,ebx
  004864b1  8b5c243c              mov       ebx,[esp+3Ch]
  004864b5  2bd3                  sub       edx,ebx
  004864b7  c1f80c                sar       eax,0Ch
  004864ba  c1fa0c                sar       edx,0Ch
  004864bd  89442414              mov       [esp+14h],eax
  004864c1  89542410              mov       [esp+10h],edx
  004864c5  c1f90c                sar       ecx,0Ch
  004864c8  0fafc0                imul      eax,eax
  004864cb  0fafd2                imul      edx,edx
  004864ce  8bd9                  mov       ebx,ecx
  004864d0  8944241c              mov       [esp+1Ch],eax
  004864d4  0fafd9                imul      ebx,ecx
  004864d7  03c2                  add       eax,edx
  004864d9  894c2418              mov       [esp+18h],ecx
  004864dd  50                    push      eax
  004864de  895c2424              mov       [esp+24h],ebx
  004864e2  89542428              mov       [esp+28h],edx
  004864e6  e825f50b00            call      00545A10h
  004864eb  8b4c2414              mov       ecx,[esp+14h]
  004864ef  8bd8                  mov       ebx,eax
  004864f1  8b442410              mov       eax,[esp+10h]
  004864f5  50                    push      eax
  004864f6  51                    push      ecx
  004864f7  e826f50b00            call      00545A22h
  004864fc  8b542418              mov       edx,[esp+18h]
  00486500  0500040000            add       eax,400h
  00486505  53                    push      ebx
  00486506  52                    push      edx
  00486507  66894632              mov       [esi+32h],ax
  0048650b  e812f50b00            call      00545A22h
  00486510  0fbf4e32              movsx     ecx,word [esi+32h]
  00486514  0fbfd0                movsx     edx,ax
  00486517  2bf9                  sub       edi,ecx
  00486519  66894634              mov       [esi+34h],ax
  0048651d  8b44241c              mov       eax,[esp+1Ch]
  00486521  81c700040000          add       edi,400h
  00486527  2bea                  sub       ebp,edx
  00486529  8b4c2420              mov       ecx,[esp+20h]
  0048652d  03c1                  add       eax,ecx
  0048652f  8b4c2424              mov       ecx,[esp+24h]
  00486533  03c1                  add       eax,ecx
  00486535  50                    push      eax
  00486536  e8d5f40b00            call      00545A10h
  0048653b  8bd8                  mov       ebx,eax
  0048653d  b889888888            mov       eax,88888889h
  00486542  8bcb                  mov       ecx,ebx
  00486544  81c700010000          add       edi,100h
  0048654a  c1e10c                shl       ecx,0Ch
  0048654d  f7e9                  imul      ecx
  0048654f  03d1                  add       edx,ecx
  00486551  81e7ff0f0000          and       edi,0FFFh
  00486557  c1fa04                sar       edx,4
  0048655a  8bc2                  mov       eax,edx
  0048655c  c1e81f                shr       eax,1Fh
  0048655f  03d0                  add       edx,eax
  00486561  81ff00020000          cmp       edi,200h
  00486567  66895628              mov       [esi+28h],dx
  0048656b  7d22                  jge       short 0048658Fh
  0048656d  81c500010000          add       ebp,100h
  00486573  81e5ff0f0000          and       ebp,0FFFh
  00486579  81fd00020000          cmp       ebp,200h
  0048657f  7d0e                  jge       short 0048658Fh
  00486581  81fbc8000000          cmp       ebx,0C8h
  00486587  7d06                  jge       short 0048658Fh
  00486589  8b06                  mov       eax,[esi]
  0048658b  0c10                  or        al,10h
  0048658d  8906                  mov       [esi],eax

LAB_0048658f:
  0048658f  56                    push      esi
  00486590  e8abd30a00            call      00533940h
  00486595  8b4660                mov       eax,[esi+60h]
  00486598  8b4e20                mov       ecx,[esi+20h]
  0048659b  83c404                add       esp,4
  0048659e  3bc8                  cmp       ecx,eax
  004865a0  7f0d                  jg        short 004865AFh
  004865a2  894620                mov       [esi+20h],eax
  004865a5  c7869000000000080000  mov       dword [esi+90h],800h

LAB_004865af:
  004865af  f70600000040          test      dword [esi],40000000h
  004865b5  7554                  jne       short 0048660Bh
  004865b7  8b8690000000          mov       eax,[esi+90h]
  004865bd  f6c4a0                test      ah,0A0h
  004865c0  7507                  jne       short 004865C9h
  004865c2  a902080000            test      eax,802h
  004865c7  7542                  jne       short 0048660Bh

LAB_004865c9:
  004865c9  8d4c2428              lea       ecx,[esp+28h]
  004865cd  51                    push      ecx
  004865ce  56                    push      esi
  004865cf  e8cce6feff            call      00474CA0h
  004865d4  8bf8                  mov       edi,eax
  004865d6  83c408                add       esp,8
  004865d9  85ff                  test      edi,edi
  004865db  0f8490000000          je        near 00486671h
  004865e1  8b5674                mov       edx,[esi+74h]
  004865e4  57                    push      edi
  004865e5  52                    push      edx
  004865e6  e895350b00            call      00539B80h
  004865eb  8b07                  mov       eax,[edi]
  004865ed  83c408                add       esp,8
  004865f0  0c40                  or        al,40h
  004865f2  66c787960000000800    mov       word [edi+96h],8
  004865fb  8907                  mov       [edi],eax
  004865fd  c70600000000          mov       dword [esi],0
  00486603  5f                    pop       edi
  00486604  5e                    pop       esi
  00486605  5d                    pop       ebp
  00486606  5b                    pop       ebx
  00486607  83c430                add       esp,30h
  0048660a  c3                    ret

LAB_0048660b:
  0048660b  6a07                  push      7
  0048660d  6a01                  push      1
  0048660f  33ff                  xor       edi,edi
  00486611  bb00180000            mov       ebx,1800h
  00486616  6a6c                  push      6Ch
  00486618  56                    push      esi
  00486619  897e70                mov       [esi+70h],edi
  0048661c  897e44                mov       [esi+44h],edi
  0048661f  897e48                mov       [esi+48h],edi
  00486622  897e4c                mov       [esi+4Ch],edi
  00486625  895e50                mov       [esi+50h],ebx
  00486628  c7465400140000        mov       dword [esi+54h],1400h
  0048662f  895e58                mov       [esi+58h],ebx
  00486632  c7463880808000        mov       dword [esi+38h],808080h
  00486639  897e28                mov       [esi+28h],edi
  0048663c  66897e2c              mov       [esi+2Ch],di
  00486640  66897e34              mov       [esi+34h],di
  00486644  c6460401              mov       byte [esi+4],1
  00486648  e89344ffff            call      0047AAE0h
  0048664d  83c410                add       esp,10h
  00486650  8d461c                lea       eax,[esi+1Ch]
  00486653  50                    push      eax
  00486654  53                    push      ebx
  00486655  68520b0000            push      0B52h
  0048665a  6a2e                  push      2Eh
  0048665c  e8bf740b00            call      0053DB20h
  00486661  8b442454              mov       eax,[esp+54h]
  00486665  83c410                add       esp,10h
  00486668  3bc7                  cmp       eax,edi
  0048666a  7403                  je        short 0048666Fh
  0048666c  897874                mov       [eax+74h],edi

LAB_0048666f:
  0048666f  893e                  mov       [esi],edi

LAB_00486671:
  00486671  5f                    pop       edi
  00486672  5e                    pop       esi
  00486673  5d                    pop       ebp
  00486674  5b                    pop       ebx
  00486675  83c430                add       esp,30h
  00486678  c3                    ret
  00486679  90                    nop
  0048667a  90                    nop
  0048667b  90                    nop
  0048667c  90                    nop
  0048667d  90                    nop
  0048667e  90                    nop
  0048667f  90                    nop
  00486680  56                    push      esi
  00486681  8b742408              mov       esi,[esp+8]
  00486685  66837e1000            cmp       word [esi+10h],0
  0048668a  0f8584000000          jne       near 00486714h
  00486690  8b466c                mov       eax,[esi+6Ch]
  00486693  57                    push      edi
  00486694  0fbf7e32              movsx     edi,word [esi+32h]
  00486698  53                    push      ebx
  00486699  bb04000000            mov       ebx,4
  0048669e  894674                mov       [esi+74h],eax
  004866a1  c6460438              mov       byte [esi+4],38h
  004866a5  c7463800000000        mov       dword [esi+38h],0
  004866ac  66895e36              mov       [esi+36h],bx
  004866b0  66c7462ef0ff          mov       word [esi+2Eh],0FFF0h
  004866b6  c7462800100002        mov       dword [esi+28h],2001000h
  004866bd  66c7462c0000          mov       word [esi+2Ch],0
  004866c3  66c746120800          mov       word [esi+12h],8
  004866c9  81ef80010000          sub       edi,180h

LAB_004866cf:
  004866cf  e86c0a0c00            call      00547140h
  004866d4  83e03f                and       eax,3Fh
  004866d7  6a04                  push      4
  004866d9  6a00                  push      0
  004866db  8d8c38e0030000        lea       ecx,[eax+edi+3E0h]
  004866e2  66894e32              mov       [esi+32h],cx
  004866e6  8b15cc975e00          mov       edx,[5E97CCh]
  004866ec  8b82d0000000          mov       eax,[edx+0D0h]
  004866f2  33d2                  xor       edx,edx
  004866f4  8b08                  mov       ecx,[eax]
  004866f6  8a5107                mov       dl,[ecx+7]
  004866f9  81c2c0000000          add       edx,0C0h
  004866ff  52                    push      edx
  00486700  56                    push      esi
  00486701  e8da43ffff            call      0047AAE0h
  00486706  83c410                add       esp,10h
  00486709  81c700010000          add       edi,100h
  0048670f  4b                    dec       ebx
  00486710  75bd                  jne       short 004866CFh
  00486712  5b                    pop       ebx
  00486713  5f                    pop       edi

LAB_00486714:
  00486714  c70600000000          mov       dword [esi],0
  0048671a  5e                    pop       esi
  0048671b  c3                    ret
  0048671c  90                    nop
  0048671d  90                    nop
  0048671e  90                    nop
  0048671f  90                    nop
  00486720  83ec18                sub       esp,18h
  00486723  53                    push      ebx
  00486724  55                    push      ebp
  00486725  56                    push      esi
  00486726  8b742428              mov       esi,[esp+28h]
  0048672a  57                    push      edi
  0048672b  66c744241000fc        mov       word [esp+10h],0FC00h
  00486732  66837e1001            cmp       word [esi+10h],1
  00486737  66c7442412e2ff        mov       word [esp+12h],0FFE2h
  0048673e  66c74424141e00        mov       word [esp+14h],1Eh
  00486745  66c7442416c800        mov       word [esp+16h],0C8h
  0048674c  66c74424180000        mov       word [esp+18h],0
  00486753  66c744241aa000        mov       word [esp+1Ah],0A0h
  0048675a  0f85c1000000          jne       near 00486821h
  00486760  668b4612              mov       ax,[esi+12h]
  00486764  6685c0                test      ax,ax
  00486767  0f84b4000000          je        near 00486821h
  0048676d  0fbf7e32              movsx     edi,word [esi+32h]
  00486771  0fbf562a              movsx     edx,word [esi+2Ah]
  00486775  8b4e24                mov       ecx,[esi+24h]
  00486778  8b6e1c                mov       ebp,[esi+1Ch]
  0048677b  48                    dec       eax
  0048677c  8d9f00fcffff          lea       ebx,[edi-400h]
  00486782  66894612              mov       [esi+12h],ax
  00486786  8b4620                mov       eax,[esi+20h]
  00486789  53                    push      ebx
  0048678a  89442424              mov       [esp+24h],eax
  0048678e  894c2428              mov       [esp+28h],ecx
  00486792  89542430              mov       [esp+30h],edx
  00486796  e881f20b00            call      00545A1Ch
  0048679b  8b561c                mov       edx,[esi+1Ch]
  0048679e  8d0480                lea       eax,[eax+eax*4]
  004867a1  53                    push      ebx
  004867a2  8d0480                lea       eax,[eax+eax*4]
  004867a5  c1e002                shl       eax,2
  004867a8  03d0                  add       edx,eax
  004867aa  89561c                mov       [esi+1Ch],edx
  004867ad  e864f20b00            call      00545A16h
  004867b2  8b5e24                mov       ebx,[esi+24h]
  004867b5  8d0480                lea       eax,[eax+eax*4]
  004867b8  66c7462a0002          mov       word [esi+2Ah],200h
  004867be  8d0c80                lea       ecx,[eax+eax*4]
  004867c1  c1e102                shl       ecx,2
  004867c4  03d9                  add       ebx,ecx
  004867c6  895e24                mov       [esi+24h],ebx
  004867c9  e872090c00            call      00547140h
  004867ce  25ff                  (bad)
  (octet invalide — la plage demandee deborde probablement du code)
