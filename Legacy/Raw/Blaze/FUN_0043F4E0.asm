
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043f4e0  section .text  offset fichier 0x3e8e0  1328 octets

  0043f4e0  83ec20                sub       esp,20h
  0043f4e3  53                    push      ebx
  0043f4e4  55                    push      ebp
  0043f4e5  56                    push      esi
  0043f4e6  8b742430              mov       esi,[esp+30h]
  0043f4ea  33d2                  xor       edx,edx
  0043f4ec  57                    push      edi
  0043f4ed  8b4e74                mov       ecx,[esi+74h]
  0043f4f0  b858e55d00            mov       eax,5DE558h
  0043f4f5  3bca                  cmp       ecx,edx
  0043f4f7  894c2434              mov       [esp+34h],ecx
  0043f4fb  7504                  jne       short 0043F501h
  0043f4fd  89442434              mov       [esp+34h],eax

LAB_0043f501:
  0043f501  8b4e6c                mov       ecx,[esi+6Ch]
  0043f504  3bca                  cmp       ecx,edx
  0043f506  894c2418              mov       [esp+18h],ecx
  0043f50a  7504                  jne       short 0043F510h
  0043f50c  89442418              mov       [esp+18h],eax

LAB_0043f510:
  0043f510  66395610              cmp       [esi+10h],dx
  0043f514  756e                  jne       short 0043F584h
  0043f516  b8fa000000            mov       eax,0FAh
  0043f51b  c6460400              mov       byte [esi+4],0
  0043f51f  66894628              mov       [esi+28h],ax
  0043f523  6689462c              mov       [esi+2Ch],ax
  0043f527  668b869a000000        mov       ax,[esi+9Ah]
  0043f52e  c6460a03              mov       byte [esi+0Ah],3
  0043f532  663bc2                cmp       ax,dx
  0043f535  c6460b01              mov       byte [esi+0Bh],1
  0043f539  66c746100100          mov       word [esi+10h],1
  0043f53f  6689562a              mov       [esi+2Ah],dx
  0043f543  66c746120a00          mov       word [esi+12h],0Ah
  0043f549  b196                  mov       cl,96h
  0043f54b  750b                  jne       short 0043F558h
  0043f54d  c6463800              mov       byte [esi+38h],0
  0043f551  c6463982              mov       byte [esi+39h],82h
  0043f555  884e3a                mov       [esi+3Ah],cl

LAB_0043f558:
  0043f558  663d0100              cmp       ax,1
  0043f55c  750b                  jne       short 0043F569h
  0043f55e  884e38                mov       [esi+38h],cl
  0043f561  c6463900              mov       byte [esi+39h],0
  0043f565  c6463a00              mov       byte [esi+3Ah],0

LAB_0043f569:
  0043f569  663d0200              cmp       ax,2
  0043f56d  750a                  jne       short 0043F579h
  0043f56f  884e38                mov       [esi+38h],cl
  0043f572  c6463900              mov       byte [esi+39h],0
  0043f576  884e3a                mov       [esi+3Ah],cl

LAB_0043f579:
  0043f579  806636fe              and       byte [esi+36h],0FEh
  0043f57d  66895616              mov       [esi+16h],dx
  0043f581  895644                mov       [esi+44h],edx

LAB_0043f584:
  0043f584  66837e1001            cmp       word [esi+10h],1
  0043f589  0f8526030000          jne       near 0043F8B5h
  0043f58f  8b5c2434              mov       ebx,[esp+34h]
  0043f593  8b6c2418              mov       ebp,[esp+18h]
  0043f597  668b4332              mov       ax,[ebx+32h]
  0043f59b  66c746340004          mov       word [esi+34h],400h
  0043f5a1  6605000c              add       ax,0C00h
  0043f5a5  66894632              mov       [esi+32h],ax
  0043f5a9  8b4b20                mov       ecx,[ebx+20h]
  0043f5ac  8b7d54                mov       edi,[ebp+54h]
  0043f5af  0fbfd0                movsx     edx,ax
  0043f5b2  8b465c                mov       eax,[esi+5Ch]
  0043f5b5  03cf                  add       ecx,edi
  0043f5b7  894e20                mov       [esi+20h],ecx
  0043f5ba  8d7e1c                lea       edi,[esi+1Ch]
  0043f5bd  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  0043f5c4  51                    push      ecx
  0043f5c5  e852641000            call      00545A1Ch
  0043f5ca  8b4d50                mov       ecx,[ebp+50h]
  0043f5cd  8d0440                lea       eax,[eax+eax*2]
  0043f5d0  8d1480                lea       edx,[eax+eax*4]
  0043f5d3  8b431c                mov       eax,[ebx+1Ch]
  0043f5d6  03d0                  add       edx,eax
  0043f5d8  0fbf4632              movsx     eax,word [esi+32h]
  0043f5dc  03d1                  add       edx,ecx
  0043f5de  8b4e5c                mov       ecx,[esi+5Ch]
  0043f5e1  8917                  mov       [edi],edx
  0043f5e3  8d940100f4ffff        lea       edx,[ecx+eax-0C00h]
  0043f5ea  52                    push      edx
  0043f5eb  e826641000            call      00545A16h
  0043f5f0  8b5324                mov       edx,[ebx+24h]
  0043f5f3  8b4d58                mov       ecx,[ebp+58h]
  0043f5f6  8d0440                lea       eax,[eax+eax*2]
  0043f5f9  8d0480                lea       eax,[eax+eax*4]
  0043f5fc  03c2                  add       eax,edx
  0043f5fe  03c1                  add       eax,ecx
  0043f600  66837e120a            cmp       word [esi+12h],0Ah
  0043f605  894624                mov       [esi+24h],eax
  0043f608  7515                  jne       short 0043F61Fh
  0043f60a  6aff                  push      0FFFFFFFFh
  0043f60c  6a00                  push      0
  0043f60e  68df000000            push      0DFh
  0043f613  6a00                  push      0
  0043f615  57                    push      edi
  0043f616  56                    push      esi
  0043f617  e824b60300            call      0047AC40h
  0043f61c  83c418                add       esp,18h

LAB_0043f61f:
  0043f61f  8b0f                  mov       ecx,[edi]
  0043f621  8b5620                mov       edx,[esi+20h]
  0043f624  8b4624                mov       eax,[esi+24h]
  0043f627  894c2424              mov       [esp+24h],ecx
  0043f62b  0fbf4e32              movsx     ecx,word [esi+32h]
  0043f62f  81e9000c0000          sub       ecx,0C00h
  0043f635  89542428              mov       [esp+28h],edx
  0043f639  51                    push      ecx
  0043f63a  89442430              mov       [esp+30h],eax
  0043f63e  e8d9631000            call      00545A1Ch
  0043f643  0fbf5616              movsx     edx,word [esi+16h]
  0043f647  0fafc2                imul      eax,edx
  0043f64a  8b1f                  mov       ebx,[edi]
  0043f64c  03d8                  add       ebx,eax
  0043f64e  0fbf4632              movsx     eax,word [esi+32h]
  0043f652  2d000c0000            sub       eax,0C00h
  0043f657  891f                  mov       [edi],ebx
  0043f659  50                    push      eax
  0043f65a  e8b7631000            call      00545A16h
  0043f65f  0fbf4e16              movsx     ecx,word [esi+16h]
  0043f663  0fafc1                imul      eax,ecx
  0043f666  8b4e24                mov       ecx,[esi+24h]
  0043f669  c744241400000000      mov       dword [esp+14h],0
  0043f671  03c8                  add       ecx,eax
  0043f673  b8c0935e00            mov       eax,5E93C0h
  0043f678  894e24                mov       [esi+24h],ecx
  0043f67b  89442410              mov       [esp+10h],eax
  0043f67f  c744241c04000000      mov       dword [esp+1Ch],4

LAB_0043f687:
  0043f687  8b08                  mov       ecx,[eax]
  0043f689  f7c100000080          test      ecx,80000000h
  0043f68f  0f8483010000          je        near 0043F818h
  0043f695  f7c100000050          test      ecx,50000000h
  0043f69b  0f8577010000          jne       near 0043F818h
  0043f6a1  8b781c                mov       edi,[eax+1Ch]
  0043f6a4  8b6e1c                mov       ebp,[esi+1Ch]
  0043f6a7  8b5820                mov       ebx,[eax+20h]
  0043f6aa  8b5620                mov       edx,[esi+20h]
  0043f6ad  2bfd                  sub       edi,ebp
  0043f6af  8b6824                mov       ebp,[eax+24h]
  0043f6b2  8b4624                mov       eax,[esi+24h]
  0043f6b5  2bda                  sub       ebx,edx
  0043f6b7  2be8                  sub       ebp,eax
  0043f6b9  c1fd0c                sar       ebp,0Ch
  0043f6bc  c1ff0c                sar       edi,0Ch
  0043f6bf  55                    push      ebp
  0043f6c0  57                    push      edi
  0043f6c1  c1fb0c                sar       ebx,0Ch
  0043f6c4  e859631000            call      00545A22h
  0043f6c9  8b542434              mov       edx,[esp+34h]
  0043f6cd  0fbf4a32              movsx     ecx,word [edx+32h]
  0043f6d1  2bc1                  sub       eax,ecx
  0043f6d3  8bd7                  mov       edx,edi
  0043f6d5  25ff0f0000            and       eax,0FFFh
  0043f6da  0fafd7                imul      edx,edi
  0043f6dd  89442420              mov       [esp+20h],eax
  0043f6e1  8bc3                  mov       eax,ebx
  0043f6e3  0fafc3                imul      eax,ebx
  0043f6e6  8bcd                  mov       ecx,ebp
  0043f6e8  03d0                  add       edx,eax
  0043f6ea  0fafcd                imul      ecx,ebp
  0043f6ed  03d1                  add       edx,ecx
  0043f6ef  52                    push      edx
  0043f6f0  e81b631000            call      00545A10h
  0043f6f5  3dc8000000            cmp       eax,0C8h
  0043f6fa  0f8d14010000          jge       near 0043F814h
  0043f700  8b442420              mov       eax,[esp+20h]
  0043f704  3d20030000            cmp       eax,320h
  0043f709  7d04                  jge       short 0043F70Fh
  0043f70b  85c0                  test      eax,eax
  0043f70d  7f16                  jg        short 0043F725h

LAB_0043f70f:
  0043f70f  3d00100000            cmp       eax,1000h
  0043f714  0f8dfa000000          jge       near 0043F814h
  0043f71a  3de00c0000            cmp       eax,0CE0h
  0043f71f  0f8eef000000          jle       near 0043F814h

LAB_0043f725:
  0043f725  8b4c2414              mov       ecx,[esp+14h]
  0043f729  b801000000            mov       eax,1
  0043f72e  d3e0                  shl       eax,cl
  0043f730  8b4e44                mov       ecx,[esi+44h]
  0043f733  85c1                  test      ecx,eax
  0043f735  0f85d9000000          jne       near 0043F814h
  0043f73b  0bc1                  or        eax,ecx
  0043f73d  6683be9a00000000      cmp       word [esi+9Ah],0
  0043f745  894644                mov       [esi+44h],eax
  0043f748  7516                  jne       short 0043F760h
  0043f74a  8b7c2410              mov       edi,[esp+10h]
  0043f74e  8b5c2434              mov       ebx,[esp+34h]
  0043f752  6a00                  push      0
  0043f754  57                    push      edi
  0043f755  53                    push      ebx
  0043f756  e805f9ffff            call      0043F060h
  0043f75b  83c40c                add       esp,0Ch
  0043f75e  eb08                  jmp       short 0043F768h

LAB_0043f760:
  0043f760  8b5c2434              mov       ebx,[esp+34h]
  0043f764  8b7c2410              mov       edi,[esp+10h]

LAB_0043f768:
  0043f768  6683be9a00000001      cmp       word [esi+9Ah],1
  0043f770  0f8580000000          jne       near 0043F7F6h
  0043f776  e8c5791000            call      00547140h
  0043f77b  99                    cdq
  0043f77c  b909000000            mov       ecx,9
  0043f781  f7f9                  idiv      ecx
  0043f783  0fbfc2                movsx     eax,dx
  0043f786  83f808                cmp       eax,8
  0043f789  776b                  ja        short 0043F7F6h
  0043f78b  ff2485e0f94300        jmp       dword [eax*4+43F9E0h]
  0043f792  6a00                  push      0
  0043f794  57                    push      edi
  0043f795  53                    push      ebx
  0043f796  e845fbffff            call      0043F2E0h
  0043f79b  eb56                  jmp       short 0043F7F3h
  0043f79d  6a00                  push      0
  0043f79f  57                    push      edi
  0043f7a0  53                    push      ebx
  0043f7a1  e8baf8ffff            call      0043F060h
  0043f7a6  eb4b                  jmp       short 0043F7F3h
  0043f7a8  6a00                  push      0
  0043f7aa  57                    push      edi
  0043f7ab  53                    push      ebx
  0043f7ac  e86ffaffff            call      0043F220h
  0043f7b1  eb40                  jmp       short 0043F7F3h
  0043f7b3  6a00                  push      0
  0043f7b5  57                    push      edi
  0043f7b6  53                    push      ebx
  0043f7b7  e8e4f8ffff            call      0043F0A0h
  0043f7bc  eb35                  jmp       short 0043F7F3h
  0043f7be  6a00                  push      0
  0043f7c0  57                    push      edi
  0043f7c1  53                    push      ebx
  0043f7c2  e859f9ffff            call      0043F120h
  0043f7c7  eb2a                  jmp       short 0043F7F3h
  0043f7c9  6a00                  push      0
  0043f7cb  57                    push      edi
  0043f7cc  53                    push      ebx
  0043f7cd  e80ef9ffff            call      0043F0E0h
  0043f7d2  eb1f                  jmp       short 0043F7F3h
  0043f7d4  6a00                  push      0
  0043f7d6  57                    push      edi
  0043f7d7  53                    push      ebx
  0043f7d8  e883f9ffff            call      0043F160h
  0043f7dd  eb14                  jmp       short 0043F7F3h
  0043f7df  6a00                  push      0
  0043f7e1  57                    push      edi
  0043f7e2  53                    push      ebx
  0043f7e3  e868020000            call      0043FA50h
  0043f7e8  eb09                  jmp       short 0043F7F3h
  0043f7ea  6a00                  push      0
  0043f7ec  57                    push      edi
  0043f7ed  53                    push      ebx
  0043f7ee  e81d020000            call      0043FA10h

LAB_0043f7f3:
  0043f7f3  83c40c                add       esp,0Ch

LAB_0043f7f6:
  0043f7f6  6683be9a00000002      cmp       word [esi+9Ah],2
  0043f7fe  7514                  jne       short 0043F814h
  0043f800  8b542410              mov       edx,[esp+10h]
  0043f804  8b442434              mov       eax,[esp+34h]
  0043f808  6a00                  push      0
  0043f80a  52                    push      edx
  0043f80b  50                    push      eax
  0043f80c  e88ff8ffff            call      0043F0A0h
  0043f811  83c40c                add       esp,0Ch

LAB_0043f814:
  0043f814  8b442410              mov       eax,[esp+10h]

LAB_0043f818:
  0043f818  8b7c2414              mov       edi,[esp+14h]
  0043f81c  8b4c241c              mov       ecx,[esp+1Ch]
  0043f820  47                    inc       edi
  0043f821  059c000000            add       eax,9Ch
  0043f826  49                    dec       ecx
  0043f827  897c2414              mov       [esp+14h],edi
  0043f82b  89442410              mov       [esp+10h],eax
  0043f82f  894c241c              mov       [esp+1Ch],ecx
  0043f833  0f854efeffff          jne       near 0043F687h
  0043f839  56                    push      esi
  0043f83a  e801410f00            call      00533940h
  0043f83f  8b442430              mov       eax,[esp+30h]
  0043f843  8b4c2428              mov       ecx,[esp+28h]
  0043f847  8b54242c              mov       edx,[esp+2Ch]
  0043f84b  894624                mov       [esi+24h],eax
  0043f84e  668b462a              mov       ax,[esi+2Ah]
  0043f852  894e1c                mov       [esi+1Ch],ecx
  0043f855  0fbfc8                movsx     ecx,ax
  0043f858  895620                mov       [esi+20h],edx
  0043f85b  ba00400000            mov       edx,4000h
  0043f860  2bd1                  sub       edx,ecx
  0043f862  83c404                add       esp,4
  0043f865  c1fa02                sar       edx,2
  0043f868  03d0                  add       edx,eax
  0043f86a  668b4616              mov       ax,[esi+16h]
  0043f86e  0fbfc8                movsx     ecx,ax
  0043f871  6689562a              mov       [esi+2Ah],dx
  0043f875  ba58020000            mov       edx,258h
  0043f87a  2bd1                  sub       edx,ecx
  0043f87c  c1fa02                sar       edx,2
  0043f87f  03d0                  add       edx,eax
  0043f881  8b8690000000          mov       eax,[esi+90h]
  0043f887  a902880000            test      eax,8802h
  0043f88c  66895616              mov       [esi+16h],dx
  0043f890  740c                  je        short 0043F89Eh
  0043f892  66c746100200          mov       word [esi+10h],2
  0043f898  66c746120a00          mov       word [esi+12h],0Ah

LAB_0043f89e:
  0043f89e  66ff4e12              dec       word [esi+12h]
  0043f8a2  66837e1200            cmp       word [esi+12h],0
  0043f8a7  7f0c                  jg        short 0043F8B5h
  0043f8a9  66c746100200          mov       word [esi+10h],2
  0043f8af  66c746120a00          mov       word [esi+12h],0Ah

LAB_0043f8b5:
  0043f8b5  66837e1002            cmp       word [esi+10h],2
  0043f8ba  0f8517010000          jne       near 0043F9D7h
  0043f8c0  8b7c2434              mov       edi,[esp+34h]
  0043f8c4  8b5c2418              mov       ebx,[esp+18h]
  0043f8c8  668b4732              mov       ax,[edi+32h]
  0043f8cc  66c746340004          mov       word [esi+34h],400h
  0043f8d2  6605000c              add       ax,0C00h
  0043f8d6  66894632              mov       [esi+32h],ax
  0043f8da  8b4f20                mov       ecx,[edi+20h]
  0043f8dd  8b6b54                mov       ebp,[ebx+54h]
  0043f8e0  0fbfd0                movsx     edx,ax
  0043f8e3  8b465c                mov       eax,[esi+5Ch]
  0043f8e6  03cd                  add       ecx,ebp
  0043f8e8  894e20                mov       [esi+20h],ecx
  0043f8eb  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  0043f8f2  51                    push      ecx
  0043f8f3  e824611000            call      00545A1Ch
  0043f8f8  8b4f1c                mov       ecx,[edi+1Ch]
  0043f8fb  8d0440                lea       eax,[eax+eax*2]
  0043f8fe  8d1480                lea       edx,[eax+eax*4]
  0043f901  8b4350                mov       eax,[ebx+50h]
  0043f904  03d1                  add       edx,ecx
  0043f906  8b4e5c                mov       ecx,[esi+5Ch]
  0043f909  03d0                  add       edx,eax
  0043f90b  0fbf4632              movsx     eax,word [esi+32h]
  0043f90f  89561c                mov       [esi+1Ch],edx
  0043f912  8d940100f4ffff        lea       edx,[ecx+eax-0C00h]
  0043f919  52                    push      edx
  0043f91a  e8f7601000            call      00545A16h
  0043f91f  8b5724                mov       edx,[edi+24h]
  0043f922  8b4b58                mov       ecx,[ebx+58h]
  0043f925  8d0440                lea       eax,[eax+eax*2]
  0043f928  8d0480                lea       eax,[eax+eax*4]
  0043f92b  03c2                  add       eax,edx
  0043f92d  03c1                  add       eax,ecx
  0043f92f  0fbf4e32              movsx     ecx,word [esi+32h]
  0043f933  81e9000c0000          sub       ecx,0C00h
  0043f939  894624                mov       [esi+24h],eax
  0043f93c  51                    push      ecx
  0043f93d  e8da601000            call      00545A1Ch
  0043f942  0fbf5616              movsx     edx,word [esi+16h]
  0043f946  0fafc2                imul      eax,edx
  0043f949  8b7e1c                mov       edi,[esi+1Ch]
  0043f94c  03f8                  add       edi,eax
  0043f94e  0fbf4632              movsx     eax,word [esi+32h]
  0043f952  2d000c0000            sub       eax,0C00h
  0043f957  897e1c                mov       [esi+1Ch],edi
  0043f95a  50                    push      eax
  0043f95b  e8b6601000            call      00545A16h
  0043f960  0fbf4e16              movsx     ecx,word [esi+16h]
  0043f964  0fafc1                imul      eax,ecx
  0043f967  8b6e24                mov       ebp,[esi+24h]
  0043f96a  66c74634000c          mov       word [esi+34h],0C00h
  0043f970  03e8                  add       ebp,eax
  0043f972  668b462a              mov       ax,[esi+2Ah]
  0043f976  0fbfd0                movsx     edx,ax
  0043f979  f7da                  neg       edx
  0043f97b  c1fa02                sar       edx,2
  0043f97e  03d0                  add       edx,eax
  0043f980  8a4638                mov       al,[esi+38h]
  0043f983  8bc8                  mov       ecx,eax
  0043f985  6689562a              mov       [esi+2Ah],dx
  0043f989  81e1ff000000          and       ecx,0FFh
  0043f98f  896e24                mov       [esi+24h],ebp
  0043f992  f7d9                  neg       ecx
  0043f994  c1f903                sar       ecx,3
  0043f997  02c8                  add       cl,al
  0043f999  8a4639                mov       al,[esi+39h]
  0043f99c  8bd0                  mov       edx,eax
  0043f99e  884e38                mov       [esi+38h],cl
  0043f9a1  81e2ff000000          and       edx,0FFh
  0043f9a7  f7da                  neg       edx
  0043f9a9  c1fa03                sar       edx,3
  0043f9ac  02d0                  add       dl,al
  0043f9ae  8a463a                mov       al,[esi+3Ah]
  0043f9b1  8bc8                  mov       ecx,eax
  0043f9b3  885639                mov       [esi+39h],dl
  0043f9b6  81e1ff000000          and       ecx,0FFh
  0043f9bc  f7d9                  neg       ecx
  0043f9be  c1f903                sar       ecx,3
  0043f9c1  02c8                  add       cl,al
  0043f9c3  66ff4e12              dec       word [esi+12h]
  0043f9c7  884e3a                mov       [esi+3Ah],cl
  0043f9ca  66837e1200            cmp       word [esi+12h],0
  0043f9cf  7f06                  jg        short 0043F9D7h
  0043f9d1  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043f9d7:
  0043f9d7  5f                    pop       edi
  0043f9d8  5e                    pop       esi
  0043f9d9  5d                    pop       ebp
  0043f9da  5b                    pop       ebx
  0043f9db  83c420                add       esp,20h
  0043f9de  c3                    ret
  0043f9e0  92                    xchg      edx,eax
  0043f9e1  f743009df74300        test      dword [ebx],43F79Dh
  0043f9e8  a8f7                  test      al,0F7h
  0043f9ea  43                    inc       ebx
  0043f9eb  00b3f74300be          add       [ebx-41FFBC09h],dh
  0043f9f1  f74300c9f74300        test      dword [ebx],43F7C9h
  0043f9f8  d4f7                  aam       0F7h
  0043f9fa  43                    inc       ebx
  0043f9fb  00df                  add       bh,bl
  0043f9fd  f74300eaf74300        test      dword [ebx],43F7EAh

19 cible(s) de saut interne : 0x0043f501, 0x0043f510, 0x0043f558, 0x0043f569, 0x0043f579, 0x0043f584, 0x0043f61f, 0x0043f687, 0x0043f70f, 0x0043f725, 0x0043f760, 0x0043f768, 0x0043f7f3, 0x0043f7f6, 0x0043f814, 0x0043f818, 0x0043f89e, 0x0043f8b5, 0x0043f9d7
