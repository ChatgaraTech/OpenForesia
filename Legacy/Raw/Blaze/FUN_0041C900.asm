
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041c900  section .text  offset fichier 0x1bd00  1360 octets

  0041c900  81ecc0000000          sub       esp,0C0h
  0041c906  53                    push      ebx
  0041c907  55                    push      ebp
  0041c908  56                    push      esi
  0041c909  8bb424d0000000        mov       esi,[esp+0D0h]
  0041c910  57                    push      edi
  0041c911  33ed                  xor       ebp,ebp
  0041c913  b926000000            mov       ecx,26h
  0041c918  33c0                  xor       eax,eax
  0041c91a  8d7c2438              lea       edi,[esp+38h]
  0041c91e  896c2434              mov       [esp+34h],ebp
  0041c922  f3ab                  rep stosd
  0041c924  8d442434              lea       eax,[esp+34h]
  0041c928  8d4c2434              lea       ecx,[esp+34h]
  0041c92c  8944241c              mov       [esp+1Ch],eax
  0041c930  8b4674                mov       eax,[esi+74h]
  0041c933  3bc5                  cmp       eax,ebp
  0041c935  894c2410              mov       [esp+10h],ecx
  0041c939  7404                  je        short 0041C93Fh
  0041c93b  89442410              mov       [esp+10h],eax

LAB_0041c93f:
  0041c93f  8b466c                mov       eax,[esi+6Ch]
  0041c942  3bc5                  cmp       eax,ebp
  0041c944  7404                  je        short 0041C94Ah
  0041c946  8944241c              mov       [esp+1Ch],eax

LAB_0041c94a:
  0041c94a  66396e10              cmp       [esi+10h],bp
  0041c94e  756e                  jne       short 0041C9BEh
  0041c950  b8fa000000            mov       eax,0FAh
  0041c955  c6460400              mov       byte [esi+4],0
  0041c959  66894628              mov       [esi+28h],ax
  0041c95d  6689462c              mov       [esi+2Ch],ax
  0041c961  668b869a000000        mov       ax,[esi+9Ah]
  0041c968  c6460a03              mov       byte [esi+0Ah],3
  0041c96c  663bc5                cmp       ax,bp
  0041c96f  c6460b01              mov       byte [esi+0Bh],1
  0041c973  66c746100100          mov       word [esi+10h],1
  0041c979  66896e2a              mov       [esi+2Ah],bp
  0041c97d  66c746120a00          mov       word [esi+12h],0Ah
  0041c983  b196                  mov       cl,96h
  0041c985  750b                  jne       short 0041C992h
  0041c987  c6463800              mov       byte [esi+38h],0
  0041c98b  c6463982              mov       byte [esi+39h],82h
  0041c98f  884e3a                mov       [esi+3Ah],cl

LAB_0041c992:
  0041c992  663d0100              cmp       ax,1
  0041c996  750b                  jne       short 0041C9A3h
  0041c998  884e38                mov       [esi+38h],cl
  0041c99b  c6463900              mov       byte [esi+39h],0
  0041c99f  c6463a00              mov       byte [esi+3Ah],0

LAB_0041c9a3:
  0041c9a3  663d0200              cmp       ax,2
  0041c9a7  750a                  jne       short 0041C9B3h
  0041c9a9  884e38                mov       [esi+38h],cl
  0041c9ac  c6463900              mov       byte [esi+39h],0
  0041c9b0  884e3a                mov       [esi+3Ah],cl

LAB_0041c9b3:
  0041c9b3  806636fe              and       byte [esi+36h],0FEh
  0041c9b7  66896e16              mov       [esi+16h],bp
  0041c9bb  896e44                mov       [esi+44h],ebp

LAB_0041c9be:
  0041c9be  66837e1001            cmp       word [esi+10h],1
  0041c9c3  0f852a030000          jne       near 0041CCF3h
  0041c9c9  8b5c2410              mov       ebx,[esp+10h]
  0041c9cd  8b6c241c              mov       ebp,[esp+1Ch]
  0041c9d1  8d7e1c                lea       edi,[esi+1Ch]
  0041c9d4  668b5332              mov       dx,[ebx+32h]
  0041c9d8  66c746340004          mov       word [esi+34h],400h
  0041c9de  6681c2000c            add       dx,0C00h
  0041c9e3  66895632              mov       [esi+32h],dx
  0041c9e7  8b4320                mov       eax,[ebx+20h]
  0041c9ea  8b5554                mov       edx,[ebp+54h]
  0041c9ed  0fbf4e32              movsx     ecx,word [esi+32h]
  0041c9f1  03c2                  add       eax,edx
  0041c9f3  8b565c                mov       edx,[esi+5Ch]
  0041c9f6  894620                mov       [esi+20h],eax
  0041c9f9  8d840a00f4ffff        lea       eax,[edx+ecx-0C00h]
  0041ca00  50                    push      eax
  0041ca01  e816901200            call      00545A1Ch
  0041ca06  8b531c                mov       edx,[ebx+1Ch]
  0041ca09  8d0440                lea       eax,[eax+eax*2]
  0041ca0c  8d0c80                lea       ecx,[eax+eax*4]
  0041ca0f  8b4550                mov       eax,[ebp+50h]
  0041ca12  03ca                  add       ecx,edx
  0041ca14  0fbf5632              movsx     edx,word [esi+32h]
  0041ca18  03c8                  add       ecx,eax
  0041ca1a  8b465c                mov       eax,[esi+5Ch]
  0041ca1d  890f                  mov       [edi],ecx
  0041ca1f  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  0041ca26  51                    push      ecx
  0041ca27  e8ea8f1200            call      00545A16h
  0041ca2c  8b4d58                mov       ecx,[ebp+58h]
  0041ca2f  8d0440                lea       eax,[eax+eax*2]
  0041ca32  8d1480                lea       edx,[eax+eax*4]
  0041ca35  8b4324                mov       eax,[ebx+24h]
  0041ca38  03d0                  add       edx,eax
  0041ca3a  03d1                  add       edx,ecx
  0041ca3c  66837e120a            cmp       word [esi+12h],0Ah
  0041ca41  895624                mov       [esi+24h],edx
  0041ca44  7515                  jne       short 0041CA5Bh
  0041ca46  6aff                  push      0FFFFFFFFh
  0041ca48  6a00                  push      0
  0041ca4a  68ee000000            push      0EEh
  0041ca4f  6a00                  push      0
  0041ca51  57                    push      edi
  0041ca52  56                    push      esi
  0041ca53  e8e8e10500            call      0047AC40h
  0041ca58  83c418                add       esp,18h

LAB_0041ca5b:
  0041ca5b  8b07                  mov       eax,[edi]
  0041ca5d  8b4e20                mov       ecx,[esi+20h]
  0041ca60  8b5624                mov       edx,[esi+24h]
  0041ca63  89442430              mov       [esp+30h],eax
  0041ca67  0fbf4632              movsx     eax,word [esi+32h]
  0041ca6b  2d000c0000            sub       eax,0C00h
  0041ca70  894c2424              mov       [esp+24h],ecx
  0041ca74  50                    push      eax
  0041ca75  89542430              mov       [esp+30h],edx
  0041ca79  e89e8f1200            call      00545A1Ch
  0041ca7e  0fbf4e16              movsx     ecx,word [esi+16h]
  0041ca82  0fbf5632              movsx     edx,word [esi+32h]
  0041ca86  0fafc1                imul      eax,ecx
  0041ca89  8b2f                  mov       ebp,[edi]
  0041ca8b  81ea000c0000          sub       edx,0C00h
  0041ca91  03e8                  add       ebp,eax
  0041ca93  52                    push      edx
  0041ca94  892f                  mov       [edi],ebp
  0041ca96  e87b8f1200            call      00545A16h
  0041ca9b  0fbf4e16              movsx     ecx,word [esi+16h]
  0041ca9f  0fafc1                imul      eax,ecx
  0041caa2  8b4e24                mov       ecx,[esi+24h]
  0041caa5  c744241800000000      mov       dword [esp+18h],0
  0041caad  03c8                  add       ecx,eax
  0041caaf  b8c0935e00            mov       eax,5E93C0h
  0041cab4  894e24                mov       [esi+24h],ecx
  0041cab7  89442414              mov       [esp+14h],eax
  0041cabb  c744242004000000      mov       dword [esp+20h],4

LAB_0041cac3:
  0041cac3  8b08                  mov       ecx,[eax]
  0041cac5  f7c100000080          test      ecx,80000000h
  0041cacb  0f8483010000          je        near 0041CC54h
  0041cad1  f7c100000050          test      ecx,50000000h
  0041cad7  0f8577010000          jne       near 0041CC54h
  0041cadd  8b781c                mov       edi,[eax+1Ch]
  0041cae0  8b6e1c                mov       ebp,[esi+1Ch]
  0041cae3  8b5820                mov       ebx,[eax+20h]
  0041cae6  8b5620                mov       edx,[esi+20h]
  0041cae9  2bfd                  sub       edi,ebp
  0041caeb  8b6824                mov       ebp,[eax+24h]
  0041caee  8b4624                mov       eax,[esi+24h]
  0041caf1  2bda                  sub       ebx,edx
  0041caf3  2be8                  sub       ebp,eax
  0041caf5  c1fd0c                sar       ebp,0Ch
  0041caf8  c1ff0c                sar       edi,0Ch
  0041cafb  55                    push      ebp
  0041cafc  57                    push      edi
  0041cafd  c1fb0c                sar       ebx,0Ch
  0041cb00  e81d8f1200            call      00545A22h
  0041cb05  8b542410              mov       edx,[esp+10h]
  0041cb09  0fbf4a32              movsx     ecx,word [edx+32h]
  0041cb0d  2bc1                  sub       eax,ecx
  0041cb0f  8bd7                  mov       edx,edi
  0041cb11  25ff0f0000            and       eax,0FFFh
  0041cb16  0fafd7                imul      edx,edi
  0041cb19  89442428              mov       [esp+28h],eax
  0041cb1d  8bc3                  mov       eax,ebx
  0041cb1f  0fafc3                imul      eax,ebx
  0041cb22  8bcd                  mov       ecx,ebp
  0041cb24  03d0                  add       edx,eax
  0041cb26  0fafcd                imul      ecx,ebp
  0041cb29  03d1                  add       edx,ecx
  0041cb2b  52                    push      edx
  0041cb2c  e8df8e1200            call      00545A10h
  0041cb31  3dc8000000            cmp       eax,0C8h
  0041cb36  0f8d14010000          jge       near 0041CC50h
  0041cb3c  8b442428              mov       eax,[esp+28h]
  0041cb40  3d20030000            cmp       eax,320h
  0041cb45  7d04                  jge       short 0041CB4Bh
  0041cb47  85c0                  test      eax,eax
  0041cb49  7f16                  jg        short 0041CB61h

LAB_0041cb4b:
  0041cb4b  3d00100000            cmp       eax,1000h
  0041cb50  0f8dfa000000          jge       near 0041CC50h
  0041cb56  3de00c0000            cmp       eax,0CE0h
  0041cb5b  0f8eef000000          jle       near 0041CC50h

LAB_0041cb61:
  0041cb61  8b4c2418              mov       ecx,[esp+18h]
  0041cb65  b801000000            mov       eax,1
  0041cb6a  d3e0                  shl       eax,cl
  0041cb6c  8b4e44                mov       ecx,[esi+44h]
  0041cb6f  85c1                  test      ecx,eax
  0041cb71  0f85d9000000          jne       near 0041CC50h
  0041cb77  0bc1                  or        eax,ecx
  0041cb79  6683be9a00000000      cmp       word [esi+9Ah],0
  0041cb81  894644                mov       [esi+44h],eax
  0041cb84  7516                  jne       short 0041CB9Ch
  0041cb86  8b7c2414              mov       edi,[esp+14h]
  0041cb8a  8b5c2410              mov       ebx,[esp+10h]
  0041cb8e  6a00                  push      0
  0041cb90  57                    push      edi
  0041cb91  53                    push      ebx
  0041cb92  e8b9020000            call      0041CE50h
  0041cb97  83c40c                add       esp,0Ch
  0041cb9a  eb08                  jmp       short 0041CBA4h

LAB_0041cb9c:
  0041cb9c  8b5c2410              mov       ebx,[esp+10h]
  0041cba0  8b7c2414              mov       edi,[esp+14h]

LAB_0041cba4:
  0041cba4  6683be9a00000001      cmp       word [esi+9Ah],1
  0041cbac  0f8580000000          jne       near 0041CC32h
  0041cbb2  e889a51200            call      00547140h
  0041cbb7  99                    cdq
  0041cbb8  b909000000            mov       ecx,9
  0041cbbd  f7f9                  idiv      ecx
  0041cbbf  0fbfc2                movsx     eax,dx
  0041cbc2  83f808                cmp       eax,8
  0041cbc5  776b                  ja        short 0041CC32h
  0041cbc7  ff248520ce4100        jmp       dword [eax*4+41CE20h]
  0041cbce  6a00                  push      0
  0041cbd0  57                    push      edi
  0041cbd1  53                    push      ebx
  0041cbd2  e839040000            call      0041D010h
  0041cbd7  eb56                  jmp       short 0041CC2Fh
  0041cbd9  6a00                  push      0
  0041cbdb  57                    push      edi
  0041cbdc  53                    push      ebx
  0041cbdd  e86e020000            call      0041CE50h
  0041cbe2  eb4b                  jmp       short 0041CC2Fh
  0041cbe4  6a00                  push      0
  0041cbe6  57                    push      edi
  0041cbe7  53                    push      ebx
  0041cbe8  e8e3030000            call      0041CFD0h
  0041cbed  eb40                  jmp       short 0041CC2Fh
  0041cbef  6a00                  push      0
  0041cbf1  57                    push      edi
  0041cbf2  53                    push      ebx
  0041cbf3  e898020000            call      0041CE90h
  0041cbf8  eb35                  jmp       short 0041CC2Fh
  0041cbfa  6a00                  push      0
  0041cbfc  57                    push      edi
  0041cbfd  53                    push      ebx
  0041cbfe  e80d030000            call      0041CF10h
  0041cc03  eb2a                  jmp       short 0041CC2Fh
  0041cc05  6a00                  push      0
  0041cc07  57                    push      edi
  0041cc08  53                    push      ebx
  0041cc09  e8c2020000            call      0041CED0h
  0041cc0e  eb1f                  jmp       short 0041CC2Fh
  0041cc10  6a00                  push      0
  0041cc12  57                    push      edi
  0041cc13  53                    push      ebx
  0041cc14  e837030000            call      0041CF50h
  0041cc19  eb14                  jmp       short 0041CC2Fh
  0041cc1b  6a00                  push      0
  0041cc1d  57                    push      edi
  0041cc1e  53                    push      ebx
  0041cc1f  e82c040000            call      0041D050h
  0041cc24  eb09                  jmp       short 0041CC2Fh
  0041cc26  6a00                  push      0
  0041cc28  57                    push      edi
  0041cc29  53                    push      ebx
  0041cc2a  e861030000            call      0041CF90h

LAB_0041cc2f:
  0041cc2f  83c40c                add       esp,0Ch

LAB_0041cc32:
  0041cc32  6683be9a00000002      cmp       word [esi+9Ah],2
  0041cc3a  7514                  jne       short 0041CC50h
  0041cc3c  8b542414              mov       edx,[esp+14h]
  0041cc40  8b442410              mov       eax,[esp+10h]
  0041cc44  6a00                  push      0
  0041cc46  52                    push      edx
  0041cc47  50                    push      eax
  0041cc48  e843020000            call      0041CE90h
  0041cc4d  83c40c                add       esp,0Ch

LAB_0041cc50:
  0041cc50  8b442414              mov       eax,[esp+14h]

LAB_0041cc54:
  0041cc54  8b7c2418              mov       edi,[esp+18h]
  0041cc58  8b4c2420              mov       ecx,[esp+20h]
  0041cc5c  47                    inc       edi
  0041cc5d  059c000000            add       eax,9Ch
  0041cc62  49                    dec       ecx
  0041cc63  897c2418              mov       [esp+18h],edi
  0041cc67  89442414              mov       [esp+14h],eax
  0041cc6b  894c2420              mov       [esp+20h],ecx
  0041cc6f  0f854efeffff          jne       near 0041CAC3h
  0041cc75  56                    push      esi
  0041cc76  e8c56c1100            call      00533940h
  0041cc7b  8b442430              mov       eax,[esp+30h]
  0041cc7f  8b4c2434              mov       ecx,[esp+34h]
  0041cc83  8b542428              mov       edx,[esp+28h]
  0041cc87  894624                mov       [esi+24h],eax
  0041cc8a  668b462a              mov       ax,[esi+2Ah]
  0041cc8e  894e1c                mov       [esi+1Ch],ecx
  0041cc91  0fbfc8                movsx     ecx,ax
  0041cc94  895620                mov       [esi+20h],edx
  0041cc97  ba00400000            mov       edx,4000h
  0041cc9c  2bd1                  sub       edx,ecx
  0041cc9e  83c404                add       esp,4
  0041cca1  c1fa02                sar       edx,2
  0041cca4  03d0                  add       edx,eax
  0041cca6  668b4616              mov       ax,[esi+16h]
  0041ccaa  0fbfc8                movsx     ecx,ax
  0041ccad  6689562a              mov       [esi+2Ah],dx
  0041ccb1  ba58020000            mov       edx,258h
  0041ccb6  2bd1                  sub       edx,ecx
  0041ccb8  c1fa02                sar       edx,2
  0041ccbb  03d0                  add       edx,eax
  0041ccbd  8b8690000000          mov       eax,[esi+90h]
  0041ccc3  a902880000            test      eax,8802h
  0041ccc8  66895616              mov       [esi+16h],dx
  0041cccc  740c                  je        short 0041CCDAh
  0041ccce  66c746100200          mov       word [esi+10h],2
  0041ccd4  66c746120a00          mov       word [esi+12h],0Ah

LAB_0041ccda:
  0041ccda  66ff4e12              dec       word [esi+12h]
  0041ccde  66837e1200            cmp       word [esi+12h],0
  0041cce3  7f0c                  jg        short 0041CCF1h
  0041cce5  66c746100200          mov       word [esi+10h],2
  0041cceb  66c746120a00          mov       word [esi+12h],0Ah

LAB_0041ccf1:
  0041ccf1  33ed                  xor       ebp,ebp

LAB_0041ccf3:
  0041ccf3  66837e1002            cmp       word [esi+10h],2
  0041ccf8  0f8517010000          jne       near 0041CE15h
  0041ccfe  8b7c2410              mov       edi,[esp+10h]
  0041cd02  8b5c241c              mov       ebx,[esp+1Ch]
  0041cd06  668b4732              mov       ax,[edi+32h]
  0041cd0a  66c746340004          mov       word [esi+34h],400h
  0041cd10  6605000c              add       ax,0C00h
  0041cd14  66894632              mov       [esi+32h],ax
  0041cd18  8b4f20                mov       ecx,[edi+20h]
  0041cd1b  8b4354                mov       eax,[ebx+54h]
  0041cd1e  0fbf5632              movsx     edx,word [esi+32h]
  0041cd22  03c8                  add       ecx,eax
  0041cd24  8b465c                mov       eax,[esi+5Ch]
  0041cd27  894e20                mov       [esi+20h],ecx
  0041cd2a  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  0041cd31  51                    push      ecx
  0041cd32  e8e58c1200            call      00545A1Ch
  0041cd37  8b4b50                mov       ecx,[ebx+50h]
  0041cd3a  8d0440                lea       eax,[eax+eax*2]
  0041cd3d  8d1480                lea       edx,[eax+eax*4]
  0041cd40  8b471c                mov       eax,[edi+1Ch]
  0041cd43  03d0                  add       edx,eax
  0041cd45  0fbf4632              movsx     eax,word [esi+32h]
  0041cd49  03d1                  add       edx,ecx
  0041cd4b  8b4e5c                mov       ecx,[esi+5Ch]
  0041cd4e  89561c                mov       [esi+1Ch],edx
  0041cd51  8d940100f4ffff        lea       edx,[ecx+eax-0C00h]
  0041cd58  52                    push      edx
  0041cd59  e8b88c1200            call      00545A16h
  0041cd5e  8b4f24                mov       ecx,[edi+24h]
  0041cd61  8d0440                lea       eax,[eax+eax*2]
  0041cd64  8b7b58                mov       edi,[ebx+58h]
  0041cd67  8d0480                lea       eax,[eax+eax*4]
  0041cd6a  03c1                  add       eax,ecx
  0041cd6c  0fbf4e32              movsx     ecx,word [esi+32h]
  0041cd70  81e9000c0000          sub       ecx,0C00h
  0041cd76  03c7                  add       eax,edi
  0041cd78  51                    push      ecx
  0041cd79  894624                mov       [esi+24h],eax
  0041cd7c  e89b8c1200            call      00545A1Ch
  0041cd81  0fbf5616              movsx     edx,word [esi+16h]
  0041cd85  0fafc2                imul      eax,edx
  0041cd88  8b5e1c                mov       ebx,[esi+1Ch]
  0041cd8b  03d8                  add       ebx,eax
  0041cd8d  0fbf4632              movsx     eax,word [esi+32h]
  0041cd91  2d000c0000            sub       eax,0C00h
  0041cd96  895e1c                mov       [esi+1Ch],ebx
  0041cd99  50                    push      eax
  0041cd9a  e8778c1200            call      00545A16h
  0041cd9f  0fbf4e16              movsx     ecx,word [esi+16h]
  0041cda3  0fafc1                imul      eax,ecx
  0041cda6  8b4e24                mov       ecx,[esi+24h]
  0041cda9  66c74634000c          mov       word [esi+34h],0C00h
  0041cdaf  03c8                  add       ecx,eax
  0041cdb1  668b462a              mov       ax,[esi+2Ah]
  0041cdb5  0fbfd0                movsx     edx,ax
  0041cdb8  f7da                  neg       edx
  0041cdba  c1fa02                sar       edx,2
  0041cdbd  03d0                  add       edx,eax
  0041cdbf  8a4638                mov       al,[esi+38h]
  0041cdc2  894e24                mov       [esi+24h],ecx
  0041cdc5  8bc8                  mov       ecx,eax
  0041cdc7  81e1ff000000          and       ecx,0FFh
  0041cdcd  6689562a              mov       [esi+2Ah],dx
  0041cdd1  f7d9                  neg       ecx
  0041cdd3  c1f903                sar       ecx,3
  0041cdd6  02c8                  add       cl,al
  0041cdd8  8a4639                mov       al,[esi+39h]
  0041cddb  8bd0                  mov       edx,eax
  0041cddd  884e38                mov       [esi+38h],cl
  0041cde0  81e2ff000000          and       edx,0FFh
  0041cde6  f7da                  neg       edx
  0041cde8  c1fa03                sar       edx,3
  0041cdeb  02d0                  add       dl,al
  0041cded  8a463a                mov       al,[esi+3Ah]
  0041cdf0  8bc8                  mov       ecx,eax
  0041cdf2  885639                mov       [esi+39h],dl
  0041cdf5  81e1ff000000          and       ecx,0FFh
  0041cdfb  f7d9                  neg       ecx
  0041cdfd  c1f903                sar       ecx,3
  0041ce00  02c8                  add       cl,al
  0041ce02  66ff4e12              dec       word [esi+12h]
  0041ce06  884e3a                mov       [esi+3Ah],cl
  0041ce09  66396e12              cmp       [esi+12h],bp
  0041ce0d  7f06                  jg        short 0041CE15h
  0041ce0f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041ce15:
  0041ce15  5f                    pop       edi
  0041ce16  5e                    pop       esi
  0041ce17  5d                    pop       ebp
  0041ce18  5b                    pop       ebx
  0041ce19  81c4c0000000          add       esp,0C0h
  0041ce1f  c3                    ret
  0041ce20  ce                    into
  0041ce21  cb                    retf
  0041ce22  41                    inc       ecx
  0041ce23  00d9                  add       cl,bl
  0041ce25  cb                    retf
  0041ce26  41                    inc       ecx
  0041ce27  00e4                  add       ah,ah
  0041ce29  cb                    retf
  0041ce2a  41                    inc       ecx
  0041ce2b  00ef                  add       bh,ch
  0041ce2d  cb                    retf
  0041ce2e  41                    inc       ecx
  0041ce2f  00fa                  add       dl,bh
  0041ce31  cb                    retf
  0041ce32  41                    inc       ecx
  0041ce33  0005cc410010          add       [100041CCh],al
  0041ce39  cc                    int3
  0041ce3a  41                    inc       ecx
  0041ce3b  001b                  add       [ebx],bl
  0041ce3d  cc                    int3
  0041ce3e  41                    inc       ecx
  0041ce3f  0026                  add       [esi],ah
  0041ce41  cc                    int3
  0041ce42  41                    inc       ecx
  0041ce43  009090909090          add       [eax-6F6F6F70h],dl

20 cible(s) de saut interne : 0x0041c93f, 0x0041c94a, 0x0041c992, 0x0041c9a3, 0x0041c9b3, 0x0041c9be, 0x0041ca5b, 0x0041cac3, 0x0041cb4b, 0x0041cb61, 0x0041cb9c, 0x0041cba4, 0x0041cc2f, 0x0041cc32, 0x0041cc50, 0x0041cc54, 0x0041ccda, 0x0041ccf1, 0x0041ccf3, 0x0041ce15
