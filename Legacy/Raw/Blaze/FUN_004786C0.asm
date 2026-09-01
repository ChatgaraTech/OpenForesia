Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004786c0  section .text  offset fichier 0x77ac0  1364 octets

  004786c0  83ec28                sub       esp,28h
  004786c3  53                    push      ebx
  004786c4  55                    push      ebp
  004786c5  56                    push      esi
  004786c6  8b742438              mov       esi,[esp+38h]
  004786ca  57                    push      edi
  004786cb  f70600000002          test      dword [esi],2000000h
  004786d1  7431                  je        short 00478704h
  004786d3  668b4628              mov       ax,[esi+28h]
  004786d7  668bc8                mov       cx,ax
  004786da  66c1f902              sar       cx,2
  004786de  2bc1                  sub       eax,ecx
  004786e0  663d0001              cmp       ax,100h
  004786e4  66894628              mov       [esi+28h],ax
  004786e8  6689462a              mov       [esi+2Ah],ax
  004786ec  6689462c              mov       [esi+2Ch],ax
  004786f0  0f8da8040000          jge       near 00478B9Eh
  004786f6  c70600000000          mov       dword [esi],0
  004786fc  5f                    pop       edi
  004786fd  5e                    pop       esi
  004786fe  5d                    pop       ebp
  004786ff  5b                    pop       ebx
  00478700  83c428                add       esp,28h
  00478703  c3                    ret

LAB_00478704:
  00478704  33ff                  xor       edi,edi
  00478706  66397e10              cmp       [esi+10h],di
  0047870a  756f                  jne       short 0047877Bh
  0047870c  8b4674                mov       eax,[esi+74h]
  0047870f  57                    push      edi
  00478710  56                    push      esi
  00478711  8b10                  mov       edx,[eax]
  00478713  80ca80                or        dl,80h
  00478716  8910                  mov       [eax],edx
  00478718  804e3606              or        byte [esi+36h],6
  0047871c  66c7462eecff          mov       word [esi+2Eh],0FFECh
  00478722  e879c4ffff            call      00474BA0h
  00478727  83c408                add       esp,8
  0047872a  8bd8                  mov       ebx,eax
  0047872c  895e78                mov       [esi+78h],ebx
  0047872f  895e70                mov       [esi+70h],ebx
  00478732  53                    push      ebx
  00478733  e888040c00            call      00538BC0h
  00478738  83c404                add       esp,4
  0047873b  8d561c                lea       edx,[esi+1Ch]
  0047873e  66894612              mov       [esi+12h],ax
  00478742  0fbf4332              movsx     eax,word [ebx+32h]
  00478746  0fbf4b34              movsx     ecx,word [ebx+34h]
  0047874a  52                    push      edx
  0047874b  6800120000            push      1200h
  00478750  6895010000            push      195h
  00478755  897e28                mov       [esi+28h],edi
  00478758  6a31                  push      31h
  0047875a  66894614              mov       [esi+14h],ax
  0047875e  66894e16              mov       [esi+16h],cx
  00478762  66894632              mov       [esi+32h],ax
  00478766  66894e34              mov       [esi+34h],cx
  0047876a  66897e28              mov       [esi+28h],di
  0047876e  897e38                mov       [esi+38h],edi
  00478771  e8aa530c00            call      0053DB20h
  00478776  83c410                add       esp,10h
  00478779  eb03                  jmp       short 0047877Eh

LAB_0047877b:
  0047877b  8b5e78                mov       ebx,[esi+78h]

LAB_0047877e:
  0047877e  0fbf4612              movsx     eax,word [esi+12h]
  00478782  8b5320                mov       edx,[ebx+20h]
  00478785  24fe                  and       al,0FEh
  00478787  c1e00b                shl       eax,0Bh
  0047878a  03c2                  add       eax,edx
  0047878c  894620                mov       [esi+20h],eax
  0047878f  668b4610              mov       ax,[esi+10h]
  00478793  663d1000              cmp       ax,10h
  00478797  7d4b                  jge       short 004787E4h
  00478799  663d0c00              cmp       ax,0Ch
  0047879d  7518                  jne       short 004787B7h
  0047879f  8d4e1c                lea       ecx,[esi+1Ch]
  004787a2  51                    push      ecx
  004787a3  6800120000            push      1200h
  004787a8  6853060000            push      653h
  004787ad  6a16                  push      16h
  004787af  e86c530c00            call      0053DB20h
  004787b4  83c410                add       esp,10h

LAB_004787b7:
  004787b7  66814628c000          add       word [esi+28h],0C0h
  004787bd  668b4628              mov       ax,[esi+28h]
  004787c1  6689462a              mov       [esi+2Ah],ax
  004787c5  6689462c              mov       [esi+2Ch],ax
  004787c9  8b4638                mov       eax,[esi+38h]
  004787cc  3d80808000            cmp       eax,808080h
  004787d1  0f83db000000          jae       near 004788B2h
  004787d7  0508080800            add       eax,80808h
  004787dc  894638                mov       [esi+38h],eax
  004787df  e9ce000000            jmp       004788B2h

LAB_004787e4:
  004787e4  8b4674                mov       eax,[esi+74h]
  004787e7  8b10                  mov       edx,[eax]
  004787e9  80e27f                and       dl,7Fh
  004787ec  8910                  mov       [eax],edx
  004787ee  0fbf5610              movsx     edx,word [esi+10h]
  004787f2  c1e208                shl       edx,8
  004787f5  52                    push      edx
  004787f6  e81bd20c00            call      00545A16h
  004787fb  8bc8                  mov       ecx,eax
  004787fd  b8000c0000            mov       eax,0C00h
  00478802  c1f903                sar       ecx,3
  00478805  2bc1                  sub       eax,ecx
  00478807  66894628              mov       [esi+28h],ax
  0047880b  6689462a              mov       [esi+2Ah],ax
  0047880f  6689462c              mov       [esi+2Ch],ax
  00478813  8b06                  mov       eax,[esi]
  00478815  a900008000            test      eax,800000h
  0047881a  7475                  je        short 00478891h
  0047881c  8b5674                mov       edx,[esi+74h]
  0047881f  33c9                  xor       ecx,ecx
  00478821  8a4a08                mov       cl,[edx+8]
  00478824  8b15b4876000          mov       edx,[6087B4h]
  0047882a  8d6978                lea       ebp,[ecx+78h]
  0047882d  c1e50d                shl       ebp,0Dh
  00478830  032a                  add       ebp,[edx]
  00478832  8d14c9                lea       edx,[ecx+ecx*8]
  00478835  8d0c51                lea       ecx,[ecx+edx*2]
  00478838  33d2                  xor       edx,edx
  0047883a  8a148d88915e00        mov       dl,[ecx*4+5E9188h]
  00478841  c1e204                shl       edx,4
  00478844  668b8a204a6000        mov       cx,[edx+604A20h]
  0047884b  66858dc41f0000        test      [ebp+1FC4h],cx
  00478852  7507                  jne       short 0047885Bh
  00478854  0d00000002            or        eax,2000000h
  00478859  8906                  mov       [esi],eax

LAB_0047885b:
  0047885b  a1f02c5f00            mov       eax,[5F2CF0h]
  00478860  33d2                  xor       edx,edx
  00478862  b914000000            mov       ecx,14h
  00478867  f7f1                  div       ecx
  00478869  85d2                  test      edx,edx
  0047886b  7532                  jne       short 0047889Fh
  0047886d  668b854e010000        mov       ax,[ebp+14Eh]
  00478874  663d0400              cmp       ax,4
  00478878  7c0c                  jl        short 00478886h
  0047887a  83c0fc                add       eax,0FFFFFFFCh
  0047887d  6689854e010000        mov       [ebp+14Eh],ax
  00478884  eb19                  jmp       short 0047889Fh

LAB_00478886:
  00478886  6689bd4e010000        mov       [ebp+14Eh],di
  0047888d  8b06                  mov       eax,[esi]
  0047888f  eb07                  jmp       short 00478898h

LAB_00478891:
  00478891  66837e1078            cmp       word [esi+10h],78h
  00478896  7c07                  jl        short 0047889Fh

LAB_00478898:
  00478898  0d00000002            or        eax,2000000h
  0047889d  8906                  mov       [esi],eax

LAB_0047889f:
  0047889f  56                    push      esi
  004788a0  e86bc8ffff            call      00475110h
  004788a5  83c404                add       esp,4
  004788a8  85c0                  test      eax,eax
  004788aa  7406                  je        short 004788B2h
  004788ac  810e00000002          or        dword [esi],2000000h

LAB_004788b2:
  004788b2  bd90010000            mov       ebp,190h
  004788b7  b803000000            mov       eax,3
  004788bc  897c2410              mov       [esp+10h],edi
  004788c0  8b7c243c              mov       edi,[esp+3Ch]
  004788c4  896c241c              mov       [esp+1Ch],ebp
  004788c8  89442424              mov       [esp+24h],eax

LAB_004788cc:
  004788cc  48                    dec       eax
  004788cd  7426                  je        short 004788F5h
  004788cf  48                    dec       eax
  004788d0  7413                  je        short 004788E5h
  004788d2  48                    dec       eax
  004788d3  7530                  jne       short 00478905h
  004788d5  b820000000            mov       eax,20h
  004788da  bf00f45f00            mov       edi,5FF400h
  004788df  8944243c              mov       [esp+3Ch],eax
  004788e3  eb24                  jmp       short 00478909h

LAB_004788e5:
  004788e5  b880000000            mov       eax,80h
  004788ea  bfe0de5e00            mov       edi,5EDEE0h
  004788ef  8944243c              mov       [esp+3Ch],eax
  004788f3  eb14                  jmp       short 00478909h

LAB_004788f5:
  004788f5  b880000000            mov       eax,80h
  004788fa  bf402d5f00            mov       edi,5F2D40h
  004788ff  8944243c              mov       [esp+3Ch],eax
  00478903  eb04                  jmp       short 00478909h

LAB_00478905:
  00478905  8b44243c              mov       eax,[esp+3Ch]

LAB_00478909:
  00478909  85c0                  test      eax,eax
  0047890b  0f84a8000000          je        near 004789B9h
  00478911  89442420              mov       [esp+20h],eax

LAB_00478915:
  00478915  8b07                  mov       eax,[edi]
  00478917  a900000080            test      eax,80000000h
  0047891c  747d                  je        short 0047899Bh
  0047891e  8bd0                  mov       edx,eax
  00478920  81e2800e0000          and       edx,0E80h
  00478926  81fa80060000          cmp       edx,680h
  0047892c  756d                  jne       short 0047899Bh
  0047892e  a900000040            test      eax,40000000h
  00478933  7566                  jne       short 0047899Bh
  00478935  8b471c                mov       eax,[edi+1Ch]
  00478938  8b531c                mov       edx,[ebx+1Ch]
  0047893b  8b4f20                mov       ecx,[edi+20h]
  0047893e  2bc2                  sub       eax,edx
  00478940  c1f80c                sar       eax,0Ch
  00478943  8944242c              mov       [esp+2Ch],eax
  00478947  8b6b20                mov       ebp,[ebx+20h]
  0047894a  8b5724                mov       edx,[edi+24h]
  0047894d  2bcd                  sub       ecx,ebp
  0047894f  c1f90c                sar       ecx,0Ch
  00478952  894c2430              mov       [esp+30h],ecx
  00478956  8b6b24                mov       ebp,[ebx+24h]
  00478959  2bd5                  sub       edx,ebp
  0047895b  89442428              mov       [esp+28h],eax
  0047895f  c1fa0c                sar       edx,0Ch
  00478962  0fafc0                imul      eax,eax
  00478965  894c2414              mov       [esp+14h],ecx
  00478969  0fafc9                imul      ecx,ecx
  0047896c  89542418              mov       [esp+18h],edx
  00478970  0fafd2                imul      edx,edx
  00478973  8944242c              mov       [esp+2Ch],eax
  00478977  03c1                  add       eax,ecx
  00478979  03c2                  add       eax,edx
  0047897b  894c2430              mov       [esp+30h],ecx
  0047897f  50                    push      eax
  00478980  89542438              mov       [esp+38h],edx
  00478984  e887d00c00            call      00545A10h
  00478989  8b6c241c              mov       ebp,[esp+1Ch]
  0047898d  3be8                  cmp       ebp,eax
  0047898f  7e0a                  jle       short 0047899Bh
  00478991  8be8                  mov       ebp,eax
  00478993  897c2410              mov       [esp+10h],edi
  00478997  896c241c              mov       [esp+1Ch],ebp

LAB_0047899b:
  0047899b  8b54243c              mov       edx,[esp+3Ch]
  0047899f  8b442420              mov       eax,[esp+20h]
  004789a3  4a                    dec       edx
  004789a4  81c79c000000          add       edi,9Ch
  004789aa  48                    dec       eax
  004789ab  8954243c              mov       [esp+3Ch],edx
  004789af  89442420              mov       [esp+20h],eax
  004789b3  0f855cffffff          jne       near 00478915h

LAB_004789b9:
  004789b9  8b442424              mov       eax,[esp+24h]
  004789bd  48                    dec       eax
  004789be  89442424              mov       [esp+24h],eax
  004789c2  0f8504ffffff          jne       near 004788CCh
  004789c8  8b442410              mov       eax,[esp+10h]
  004789cc  85c0                  test      eax,eax
  004789ce  7443                  je        short 00478A13h
  004789d0  8b7c2428              mov       edi,[esp+28h]
  004789d4  8b5c2418              mov       ebx,[esp+18h]
  004789d8  8bc7                  mov       eax,edi
  004789da  8bcb                  mov       ecx,ebx
  004789dc  0fafc7                imul      eax,edi
  004789df  0fafcb                imul      ecx,ebx
  004789e2  03c1                  add       eax,ecx
  004789e4  50                    push      eax
  004789e5  e826d00c00            call      00545A10h
  004789ea  53                    push      ebx
  004789eb  57                    push      edi
  004789ec  89442444              mov       [esp+44h],eax
  004789f0  e82dd00c00            call      00545A22h
  004789f5  8b54243c              mov       edx,[esp+3Ch]
  004789f9  0500040000            add       eax,400h
  004789fe  66894614              mov       [esi+14h],ax
  00478a02  8b442414              mov       eax,[esp+14h]
  00478a06  52                    push      edx
  00478a07  50                    push      eax
  00478a08  e815d00c00            call      00545A22h
  00478a0d  66894616              mov       [esi+16h],ax
  00478a11  eb15                  jmp       short 00478A28h

LAB_00478a13:
  00478a13  668b4b32              mov       cx,[ebx+32h]
  00478a17  6681c10004            add       cx,400h
  00478a1c  66894e14              mov       [esi+14h],cx
  00478a20  668b5334              mov       dx,[ebx+34h]
  00478a24  66895616              mov       [esi+16h],dx

LAB_00478a28:
  00478a28  668b7e32              mov       di,[esi+32h]
  00478a2c  668b4614              mov       ax,[esi+14h]
  00478a30  668b5e34              mov       bx,[esi+34h]
  00478a34  668b4e16              mov       cx,[esi+16h]
  00478a38  2bc7                  sub       eax,edi
  00478a3a  2bcb                  sub       ecx,ebx
  00478a3c  25ff0f0000            and       eax,0FFFh
  00478a41  81e1ff0f0000          and       ecx,0FFFh
  00478a47  3d00080000            cmp       eax,800h
  00478a4c  7d07                  jge       short 00478A55h
  00478a4e  8bd0                  mov       edx,eax
  00478a50  c1fa02                sar       edx,2
  00478a53  eb0c                  jmp       short 00478A61h

LAB_00478a55:
  00478a55  ba00100000            mov       edx,1000h
  00478a5a  2bd0                  sub       edx,eax
  00478a5c  c1fa02                sar       edx,2
  00478a5f  f7da                  neg       edx

LAB_00478a61:
  00478a61  03fa                  add       edi,edx
  00478a63  81f900080000          cmp       ecx,800h
  00478a69  66897e32              mov       [esi+32h],di
  00478a6d  7d07                  jge       short 00478A76h
  00478a6f  8bd1                  mov       edx,ecx
  00478a71  c1fa02                sar       edx,2
  00478a74  eb0c                  jmp       short 00478A82h

LAB_00478a76:
  00478a76  ba00100000            mov       edx,1000h
  00478a7b  2bd1                  sub       edx,ecx
  00478a7d  c1fa02                sar       edx,2
  00478a80  f7da                  neg       edx

LAB_00478a82:
  00478a82  8b7c2410              mov       edi,[esp+10h]
  00478a86  03da                  add       ebx,edx
  00478a88  85ff                  test      edi,edi
  00478a8a  66895e34              mov       [esi+34h],bx
  00478a8e  0f840a010000          je        near 00478B9Eh
  00478a94  0fbf5612              movsx     edx,word [esi+12h]
  00478a98  8bda                  mov       ebx,edx
  00478a9a  c1fb02                sar       ebx,2
  00478a9d  2bd3                  sub       edx,ebx
  00478a9f  3bea                  cmp       ebp,edx
  00478aa1  0f8df7000000          jge       near 00478B9Eh
  00478aa7  0500020000            add       eax,200h
  00478aac  25ff0f0000            and       eax,0FFFh
  00478ab1  3d00040000            cmp       eax,400h
  00478ab6  0f8de2000000          jge       near 00478B9Eh
  00478abc  81c100020000          add       ecx,200h
  00478ac2  81e1ff0f0000          and       ecx,0FFFh
  00478ac8  81f900040000          cmp       ecx,400h
  00478ace  0f8dca000000          jge       near 00478B9Eh
  00478ad4  8b4628                mov       eax,[esi+28h]
  00478ad7  8b4e2c                mov       ecx,[esi+2Ch]
  00478ada  8b5638                mov       edx,[esi+38h]
  00478add  83c71c                add       edi,1Ch
  00478ae0  66c7461a0100          mov       word [esi+1Ah],1
  00478ae6  66c7869a0000000020    mov       word [esi+9Ah],2000h
  00478aef  89442414              mov       [esp+14h],eax
  00478af3  894c2418              mov       [esp+18h],ecx
  00478af7  8954243c              mov       [esp+3Ch],edx
  00478afb  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00478b02  c7462800100010        mov       dword [esi+28h],10001000h
  00478b09  66c7462c1f00          mov       word [esi+2Ch],1Fh
  00478b0f  897c2428              mov       [esp+28h],edi
  00478b13  bd04000000            mov       ebp,4

LAB_00478b18:
  00478b18  e823e60c00            call      00547140h
  00478b1d  0fbf4e32              movsx     ecx,word [esi+32h]
  00478b21  25ff010000            and       eax,1FFh
  00478b26  8dbc0800fbffff        lea       edi,[eax+ecx-500h]
  00478b2d  e80ee60c00            call      00547140h
  00478b32  0fbf5634              movsx     edx,word [esi+34h]
  00478b36  25ff010000            and       eax,1FFh
  00478b3b  8d9c1000ffffff        lea       ebx,[eax+edx-100h]
  00478b42  e8f9e50c00            call      00547140h
  00478b47  83e00f                and       eax,0Fh
  00478b4a  83c014                add       eax,14h
  00478b4d  50                    push      eax
  00478b4e  53                    push      ebx
  00478b4f  8d4650                lea       eax,[esi+50h]
  00478b52  57                    push      edi
  00478b53  50                    push      eax
  00478b54  e8779f0b00            call      00532AD0h
  00478b59  8b4c2438              mov       ecx,[esp+38h]
  00478b5d  83c410                add       esp,10h
  00478b60  33c0                  xor       eax,eax
  00478b62  8a460b                mov       al,[esi+0Bh]
  00478b65  6aff                  push      0FFFFFFFFh
  00478b67  50                    push      eax
  00478b68  68bf000000            push      0BFh
  00478b6d  68a81b5500            push      551BA8h
  00478b72  51                    push      ecx
  00478b73  56                    push      esi
  00478b74  e8c7200000            call      0047AC40h
  00478b79  83c418                add       esp,18h
  00478b7c  4d                    dec       ebp
  00478b7d  7599                  jne       short 00478B18h
  00478b7f  8b442418              mov       eax,[esp+18h]
  00478b83  8b542414              mov       edx,[esp+14h]
  00478b87  8b4c243c              mov       ecx,[esp+3Ch]
  00478b8b  89462c                mov       [esi+2Ch],eax
  00478b8e  8b442410              mov       eax,[esp+10h]
  00478b92  895628                mov       [esi+28h],edx
  00478b95  894e38                mov       [esi+38h],ecx
  00478b98  810800000040          or        dword [eax],40000000h

LAB_00478b9e:
  00478b9e  0fbf4612              movsx     eax,word [esi+12h]
  00478ba2  0fbf4e32              movsx     ecx,word [esi+32h]
  00478ba6  8bd0                  mov       edx,eax
  00478ba8  8b7e78                mov       edi,[esi+78h]
  00478bab  c1fa02                sar       edx,2
  00478bae  2bc2                  sub       eax,edx
  00478bb0  81e900040000          sub       ecx,400h
  00478bb6  50                    push      eax
  00478bb7  8d542430              lea       edx,[esp+30h]
  00478bbb  0fbf4634              movsx     eax,word [esi+34h]
  00478bbf  50                    push      eax
  00478bc0  51                    push      ecx
  00478bc1  52                    push      edx
  00478bc2  e8099f0b00            call      00532AD0h
  00478bc7  8b471c                mov       eax,[edi+1Ch]
  00478bca  8b5c243c              mov       ebx,[esp+3Ch]
  00478bce  8b4c2440              mov       ecx,[esp+40h]
  00478bd2  8b5620                mov       edx,[esi+20h]
  00478bd5  03c3                  add       eax,ebx
  00478bd7  03d1                  add       edx,ecx
  00478bd9  89461c                mov       [esi+1Ch],eax
  00478bdc  8b442444              mov       eax,[esp+44h]
  00478be0  895620                mov       [esi+20h],edx
  00478be3  8b5724                mov       edx,[edi+24h]
  00478be6  03d0                  add       edx,eax
  00478be8  83c410                add       esp,10h
  00478beb  33c0                  xor       eax,eax
  00478bed  895624                mov       [esi+24h],edx
  00478bf0  8a460b                mov       al,[esi+0Bh]
  00478bf3  6aff                  push      0FFFFFFFFh
  00478bf5  50                    push      eax
  00478bf6  6884000000            push      84h
  00478bfb  56                    push      esi
  00478bfc  c646040e              mov       byte [esi+4],0Eh
  00478c00  e8db1e0000            call      0047AAE0h
  00478c05  83c410                add       esp,10h
  00478c08  66ff4610              inc       word [esi+10h]
  00478c0c  5f                    pop       edi
  00478c0d  5e                    pop       esi
  00478c0e  5d                    pop       ebp
  00478c0f  5b                    pop       ebx
  00478c10  83c428                add       esp,28h
  00478c13  c3                    ret

27 cible(s) de saut interne : 0x00478704, 0x0047877b, 0x0047877e, 0x004787b7, 0x004787e4, 0x0047885b, 0x00478886, 0x00478891, 0x00478898, 0x0047889f, 0x004788b2, 0x004788cc, 0x004788e5, 0x004788f5, 0x00478905, 0x00478909, 0x00478915, 0x0047899b, 0x004789b9, 0x00478a13, 0x00478a28, 0x00478a55, 0x00478a61, 0x00478a76, 0x00478a82, 0x00478b18, 0x00478b9e
