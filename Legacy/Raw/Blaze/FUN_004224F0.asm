
Legacy/Binaries/Blaze.exe  base 0x00400000
0x004224f0  section .text  offset fichier 0x218f0  1328 octets

  004224f0  83ec20                sub       esp,20h
  004224f3  53                    push      ebx
  004224f4  55                    push      ebp
  004224f5  56                    push      esi
  004224f6  8b742430              mov       esi,[esp+30h]
  004224fa  33d2                  xor       edx,edx
  004224fc  57                    push      edi
  004224fd  8b4e74                mov       ecx,[esi+74h]
  00422500  b858e55d00            mov       eax,5DE558h
  00422505  3bca                  cmp       ecx,edx
  00422507  894c2434              mov       [esp+34h],ecx
  0042250b  7504                  jne       short 00422511h
  0042250d  89442434              mov       [esp+34h],eax

LAB_00422511:
  00422511  8b4e6c                mov       ecx,[esi+6Ch]
  00422514  3bca                  cmp       ecx,edx
  00422516  894c2418              mov       [esp+18h],ecx
  0042251a  7504                  jne       short 00422520h
  0042251c  89442418              mov       [esp+18h],eax

LAB_00422520:
  00422520  66395610              cmp       [esi+10h],dx
  00422524  756e                  jne       short 00422594h
  00422526  b8fa000000            mov       eax,0FAh
  0042252b  c6460400              mov       byte [esi+4],0
  0042252f  66894628              mov       [esi+28h],ax
  00422533  6689462c              mov       [esi+2Ch],ax
  00422537  668b869a000000        mov       ax,[esi+9Ah]
  0042253e  c6460a03              mov       byte [esi+0Ah],3
  00422542  663bc2                cmp       ax,dx
  00422545  c6460b01              mov       byte [esi+0Bh],1
  00422549  66c746100100          mov       word [esi+10h],1
  0042254f  6689562a              mov       [esi+2Ah],dx
  00422553  66c746120a00          mov       word [esi+12h],0Ah
  00422559  b196                  mov       cl,96h
  0042255b  750b                  jne       short 00422568h
  0042255d  c6463800              mov       byte [esi+38h],0
  00422561  c6463982              mov       byte [esi+39h],82h
  00422565  884e3a                mov       [esi+3Ah],cl

LAB_00422568:
  00422568  663d0100              cmp       ax,1
  0042256c  750b                  jne       short 00422579h
  0042256e  884e38                mov       [esi+38h],cl
  00422571  c6463900              mov       byte [esi+39h],0
  00422575  c6463a00              mov       byte [esi+3Ah],0

LAB_00422579:
  00422579  663d0200              cmp       ax,2
  0042257d  750a                  jne       short 00422589h
  0042257f  884e38                mov       [esi+38h],cl
  00422582  c6463900              mov       byte [esi+39h],0
  00422586  884e3a                mov       [esi+3Ah],cl

LAB_00422589:
  00422589  806636fe              and       byte [esi+36h],0FEh
  0042258d  66895616              mov       [esi+16h],dx
  00422591  895644                mov       [esi+44h],edx

LAB_00422594:
  00422594  66837e1001            cmp       word [esi+10h],1
  00422599  0f8526030000          jne       near 004228C5h
  0042259f  8b5c2434              mov       ebx,[esp+34h]
  004225a3  8b6c2418              mov       ebp,[esp+18h]
  004225a7  668b4332              mov       ax,[ebx+32h]
  004225ab  66c746340004          mov       word [esi+34h],400h
  004225b1  6605000c              add       ax,0C00h
  004225b5  66894632              mov       [esi+32h],ax
  004225b9  8b4b20                mov       ecx,[ebx+20h]
  004225bc  8b7d54                mov       edi,[ebp+54h]
  004225bf  0fbfd0                movsx     edx,ax
  004225c2  8b465c                mov       eax,[esi+5Ch]
  004225c5  03cf                  add       ecx,edi
  004225c7  894e20                mov       [esi+20h],ecx
  004225ca  8d7e1c                lea       edi,[esi+1Ch]
  004225cd  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  004225d4  51                    push      ecx
  004225d5  e842341200            call      00545A1Ch
  004225da  8b4d50                mov       ecx,[ebp+50h]
  004225dd  8d0440                lea       eax,[eax+eax*2]
  004225e0  8d1480                lea       edx,[eax+eax*4]
  004225e3  8b431c                mov       eax,[ebx+1Ch]
  004225e6  03d0                  add       edx,eax
  004225e8  0fbf4632              movsx     eax,word [esi+32h]
  004225ec  03d1                  add       edx,ecx
  004225ee  8b4e5c                mov       ecx,[esi+5Ch]
  004225f1  8917                  mov       [edi],edx
  004225f3  8d940100f4ffff        lea       edx,[ecx+eax-0C00h]
  004225fa  52                    push      edx
  004225fb  e816341200            call      00545A16h
  00422600  8b5324                mov       edx,[ebx+24h]
  00422603  8b4d58                mov       ecx,[ebp+58h]
  00422606  8d0440                lea       eax,[eax+eax*2]
  00422609  8d0480                lea       eax,[eax+eax*4]
  0042260c  03c2                  add       eax,edx
  0042260e  03c1                  add       eax,ecx
  00422610  66837e120a            cmp       word [esi+12h],0Ah
  00422615  894624                mov       [esi+24h],eax
  00422618  7515                  jne       short 0042262Fh
  0042261a  6aff                  push      0FFFFFFFFh
  0042261c  6a00                  push      0
  0042261e  68ee000000            push      0EEh
  00422623  6a00                  push      0
  00422625  57                    push      edi
  00422626  56                    push      esi
  00422627  e814860500            call      0047AC40h
  0042262c  83c418                add       esp,18h

LAB_0042262f:
  0042262f  8b0f                  mov       ecx,[edi]
  00422631  8b5620                mov       edx,[esi+20h]
  00422634  8b4624                mov       eax,[esi+24h]
  00422637  894c2424              mov       [esp+24h],ecx
  0042263b  0fbf4e32              movsx     ecx,word [esi+32h]
  0042263f  81e9000c0000          sub       ecx,0C00h
  00422645  89542428              mov       [esp+28h],edx
  00422649  51                    push      ecx
  0042264a  89442430              mov       [esp+30h],eax
  0042264e  e8c9331200            call      00545A1Ch
  00422653  0fbf5616              movsx     edx,word [esi+16h]
  00422657  0fafc2                imul      eax,edx
  0042265a  8b1f                  mov       ebx,[edi]
  0042265c  03d8                  add       ebx,eax
  0042265e  0fbf4632              movsx     eax,word [esi+32h]
  00422662  2d000c0000            sub       eax,0C00h
  00422667  891f                  mov       [edi],ebx
  00422669  50                    push      eax
  0042266a  e8a7331200            call      00545A16h
  0042266f  0fbf4e16              movsx     ecx,word [esi+16h]
  00422673  0fafc1                imul      eax,ecx
  00422676  8b4e24                mov       ecx,[esi+24h]
  00422679  c744241400000000      mov       dword [esp+14h],0
  00422681  03c8                  add       ecx,eax
  00422683  b8c0935e00            mov       eax,5E93C0h
  00422688  894e24                mov       [esi+24h],ecx
  0042268b  89442410              mov       [esp+10h],eax
  0042268f  c744241c04000000      mov       dword [esp+1Ch],4

LAB_00422697:
  00422697  8b08                  mov       ecx,[eax]
  00422699  f7c100000080          test      ecx,80000000h
  0042269f  0f8483010000          je        near 00422828h
  004226a5  f7c100000050          test      ecx,50000000h
  004226ab  0f8577010000          jne       near 00422828h
  004226b1  8b781c                mov       edi,[eax+1Ch]
  004226b4  8b6e1c                mov       ebp,[esi+1Ch]
  004226b7  8b5820                mov       ebx,[eax+20h]
  004226ba  8b5620                mov       edx,[esi+20h]
  004226bd  2bfd                  sub       edi,ebp
  004226bf  8b6824                mov       ebp,[eax+24h]
  004226c2  8b4624                mov       eax,[esi+24h]
  004226c5  2bda                  sub       ebx,edx
  004226c7  2be8                  sub       ebp,eax
  004226c9  c1fd0c                sar       ebp,0Ch
  004226cc  c1ff0c                sar       edi,0Ch
  004226cf  55                    push      ebp
  004226d0  57                    push      edi
  004226d1  c1fb0c                sar       ebx,0Ch
  004226d4  e849331200            call      00545A22h
  004226d9  8b542434              mov       edx,[esp+34h]
  004226dd  0fbf4a32              movsx     ecx,word [edx+32h]
  004226e1  2bc1                  sub       eax,ecx
  004226e3  8bd7                  mov       edx,edi
  004226e5  25ff0f0000            and       eax,0FFFh
  004226ea  0fafd7                imul      edx,edi
  004226ed  89442420              mov       [esp+20h],eax
  004226f1  8bc3                  mov       eax,ebx
  004226f3  0fafc3                imul      eax,ebx
  004226f6  8bcd                  mov       ecx,ebp
  004226f8  03d0                  add       edx,eax
  004226fa  0fafcd                imul      ecx,ebp
  004226fd  03d1                  add       edx,ecx
  004226ff  52                    push      edx
  00422700  e80b331200            call      00545A10h
  00422705  3dc8000000            cmp       eax,0C8h
  0042270a  0f8d14010000          jge       near 00422824h
  00422710  8b442420              mov       eax,[esp+20h]
  00422714  3d20030000            cmp       eax,320h
  00422719  7d04                  jge       short 0042271Fh
  0042271b  85c0                  test      eax,eax
  0042271d  7f16                  jg        short 00422735h

LAB_0042271f:
  0042271f  3d00100000            cmp       eax,1000h
  00422724  0f8dfa000000          jge       near 00422824h
  0042272a  3de00c0000            cmp       eax,0CE0h
  0042272f  0f8eef000000          jle       near 00422824h

LAB_00422735:
  00422735  8b4c2414              mov       ecx,[esp+14h]
  00422739  b801000000            mov       eax,1
  0042273e  d3e0                  shl       eax,cl
  00422740  8b4e44                mov       ecx,[esi+44h]
  00422743  85c1                  test      ecx,eax
  00422745  0f85d9000000          jne       near 00422824h
  0042274b  0bc1                  or        eax,ecx
  0042274d  6683be9a00000000      cmp       word [esi+9Ah],0
  00422755  894644                mov       [esi+44h],eax
  00422758  7516                  jne       short 00422770h
  0042275a  8b7c2410              mov       edi,[esp+10h]
  0042275e  8b5c2434              mov       ebx,[esp+34h]
  00422762  6a00                  push      0
  00422764  57                    push      edi
  00422765  53                    push      ebx
  00422766  e8b5020000            call      00422A20h
  0042276b  83c40c                add       esp,0Ch
  0042276e  eb08                  jmp       short 00422778h

LAB_00422770:
  00422770  8b5c2434              mov       ebx,[esp+34h]
  00422774  8b7c2410              mov       edi,[esp+10h]

LAB_00422778:
  00422778  6683be9a00000001      cmp       word [esi+9Ah],1
  00422780  0f8580000000          jne       near 00422806h
  00422786  e8b5491200            call      00547140h
  0042278b  99                    cdq
  0042278c  b909000000            mov       ecx,9
  00422791  f7f9                  idiv      ecx
  00422793  0fbfc2                movsx     eax,dx
  00422796  83f808                cmp       eax,8
  00422799  776b                  ja        short 00422806h
  0042279b  ff2485f0294200        jmp       dword [eax*4+4229F0h]
  004227a2  6a00                  push      0
  004227a4  57                    push      edi
  004227a5  53                    push      ebx
  004227a6  e835040000            call      00422BE0h
  004227ab  eb56                  jmp       short 00422803h
  004227ad  6a00                  push      0
  004227af  57                    push      edi
  004227b0  53                    push      ebx
  004227b1  e86a020000            call      00422A20h
  004227b6  eb4b                  jmp       short 00422803h
  004227b8  6a00                  push      0
  004227ba  57                    push      edi
  004227bb  53                    push      ebx
  004227bc  e8df030000            call      00422BA0h
  004227c1  eb40                  jmp       short 00422803h
  004227c3  6a00                  push      0
  004227c5  57                    push      edi
  004227c6  53                    push      ebx
  004227c7  e894020000            call      00422A60h
  004227cc  eb35                  jmp       short 00422803h
  004227ce  6a00                  push      0
  004227d0  57                    push      edi
  004227d1  53                    push      ebx
  004227d2  e809030000            call      00422AE0h
  004227d7  eb2a                  jmp       short 00422803h
  004227d9  6a00                  push      0
  004227db  57                    push      edi
  004227dc  53                    push      ebx
  004227dd  e8be020000            call      00422AA0h
  004227e2  eb1f                  jmp       short 00422803h
  004227e4  6a00                  push      0
  004227e6  57                    push      edi
  004227e7  53                    push      ebx
  004227e8  e833030000            call      00422B20h
  004227ed  eb14                  jmp       short 00422803h
  004227ef  6a00                  push      0
  004227f1  57                    push      edi
  004227f2  53                    push      ebx
  004227f3  e828040000            call      00422C20h
  004227f8  eb09                  jmp       short 00422803h
  004227fa  6a00                  push      0
  004227fc  57                    push      edi
  004227fd  53                    push      ebx
  004227fe  e85d030000            call      00422B60h

LAB_00422803:
  00422803  83c40c                add       esp,0Ch

LAB_00422806:
  00422806  6683be9a00000002      cmp       word [esi+9Ah],2
  0042280e  7514                  jne       short 00422824h
  00422810  8b542410              mov       edx,[esp+10h]
  00422814  8b442434              mov       eax,[esp+34h]
  00422818  6a00                  push      0
  0042281a  52                    push      edx
  0042281b  50                    push      eax
  0042281c  e83f020000            call      00422A60h
  00422821  83c40c                add       esp,0Ch

LAB_00422824:
  00422824  8b442410              mov       eax,[esp+10h]

LAB_00422828:
  00422828  8b7c2414              mov       edi,[esp+14h]
  0042282c  8b4c241c              mov       ecx,[esp+1Ch]
  00422830  47                    inc       edi
  00422831  059c000000            add       eax,9Ch
  00422836  49                    dec       ecx
  00422837  897c2414              mov       [esp+14h],edi
  0042283b  89442410              mov       [esp+10h],eax
  0042283f  894c241c              mov       [esp+1Ch],ecx
  00422843  0f854efeffff          jne       near 00422697h
  00422849  56                    push      esi
  0042284a  e8f1101100            call      00533940h
  0042284f  8b442430              mov       eax,[esp+30h]
  00422853  8b4c2428              mov       ecx,[esp+28h]
  00422857  8b54242c              mov       edx,[esp+2Ch]
  0042285b  894624                mov       [esi+24h],eax
  0042285e  668b462a              mov       ax,[esi+2Ah]
  00422862  894e1c                mov       [esi+1Ch],ecx
  00422865  0fbfc8                movsx     ecx,ax
  00422868  895620                mov       [esi+20h],edx
  0042286b  ba00400000            mov       edx,4000h
  00422870  2bd1                  sub       edx,ecx
  00422872  83c404                add       esp,4
  00422875  c1fa02                sar       edx,2
  00422878  03d0                  add       edx,eax
  0042287a  668b4616              mov       ax,[esi+16h]
  0042287e  0fbfc8                movsx     ecx,ax
  00422881  6689562a              mov       [esi+2Ah],dx
  00422885  ba58020000            mov       edx,258h
  0042288a  2bd1                  sub       edx,ecx
  0042288c  c1fa02                sar       edx,2
  0042288f  03d0                  add       edx,eax
  00422891  8b8690000000          mov       eax,[esi+90h]
  00422897  a902880000            test      eax,8802h
  0042289c  66895616              mov       [esi+16h],dx
  004228a0  740c                  je        short 004228AEh
  004228a2  66c746100200          mov       word [esi+10h],2
  004228a8  66c746120a00          mov       word [esi+12h],0Ah

LAB_004228ae:
  004228ae  66ff4e12              dec       word [esi+12h]
  004228b2  66837e1200            cmp       word [esi+12h],0
  004228b7  7f0c                  jg        short 004228C5h
  004228b9  66c746100200          mov       word [esi+10h],2
  004228bf  66c746120a00          mov       word [esi+12h],0Ah

LAB_004228c5:
  004228c5  66837e1002            cmp       word [esi+10h],2
  004228ca  0f8517010000          jne       near 004229E7h
  004228d0  8b7c2434              mov       edi,[esp+34h]
  004228d4  8b5c2418              mov       ebx,[esp+18h]
  004228d8  668b4732              mov       ax,[edi+32h]
  004228dc  66c746340004          mov       word [esi+34h],400h
  004228e2  6605000c              add       ax,0C00h
  004228e6  66894632              mov       [esi+32h],ax
  004228ea  8b4f20                mov       ecx,[edi+20h]
  004228ed  8b6b54                mov       ebp,[ebx+54h]
  004228f0  0fbfd0                movsx     edx,ax
  004228f3  8b465c                mov       eax,[esi+5Ch]
  004228f6  03cd                  add       ecx,ebp
  004228f8  894e20                mov       [esi+20h],ecx
  004228fb  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  00422902  51                    push      ecx
  00422903  e814311200            call      00545A1Ch
  00422908  8b4f1c                mov       ecx,[edi+1Ch]
  0042290b  8d0440                lea       eax,[eax+eax*2]
  0042290e  8d1480                lea       edx,[eax+eax*4]
  00422911  8b4350                mov       eax,[ebx+50h]
  00422914  03d1                  add       edx,ecx
  00422916  8b4e5c                mov       ecx,[esi+5Ch]
  00422919  03d0                  add       edx,eax
  0042291b  0fbf4632              movsx     eax,word [esi+32h]
  0042291f  89561c                mov       [esi+1Ch],edx
  00422922  8d940100f4ffff        lea       edx,[ecx+eax-0C00h]
  00422929  52                    push      edx
  0042292a  e8e7301200            call      00545A16h
  0042292f  8b5724                mov       edx,[edi+24h]
  00422932  8b4b58                mov       ecx,[ebx+58h]
  00422935  8d0440                lea       eax,[eax+eax*2]
  00422938  8d0480                lea       eax,[eax+eax*4]
  0042293b  03c2                  add       eax,edx
  0042293d  03c1                  add       eax,ecx
  0042293f  0fbf4e32              movsx     ecx,word [esi+32h]
  00422943  81e9000c0000          sub       ecx,0C00h
  00422949  894624                mov       [esi+24h],eax
  0042294c  51                    push      ecx
  0042294d  e8ca301200            call      00545A1Ch
  00422952  0fbf5616              movsx     edx,word [esi+16h]
  00422956  0fafc2                imul      eax,edx
  00422959  8b7e1c                mov       edi,[esi+1Ch]
  0042295c  03f8                  add       edi,eax
  0042295e  0fbf4632              movsx     eax,word [esi+32h]
  00422962  2d000c0000            sub       eax,0C00h
  00422967  897e1c                mov       [esi+1Ch],edi
  0042296a  50                    push      eax
  0042296b  e8a6301200            call      00545A16h
  00422970  0fbf4e16              movsx     ecx,word [esi+16h]
  00422974  0fafc1                imul      eax,ecx
  00422977  8b6e24                mov       ebp,[esi+24h]
  0042297a  66c74634000c          mov       word [esi+34h],0C00h
  00422980  03e8                  add       ebp,eax
  00422982  668b462a              mov       ax,[esi+2Ah]
  00422986  0fbfd0                movsx     edx,ax
  00422989  f7da                  neg       edx
  0042298b  c1fa02                sar       edx,2
  0042298e  03d0                  add       edx,eax
  00422990  8a4638                mov       al,[esi+38h]
  00422993  8bc8                  mov       ecx,eax
  00422995  6689562a              mov       [esi+2Ah],dx
  00422999  81e1ff000000          and       ecx,0FFh
  0042299f  896e24                mov       [esi+24h],ebp
  004229a2  f7d9                  neg       ecx
  004229a4  c1f903                sar       ecx,3
  004229a7  02c8                  add       cl,al
  004229a9  8a4639                mov       al,[esi+39h]
  004229ac  8bd0                  mov       edx,eax
  004229ae  884e38                mov       [esi+38h],cl
  004229b1  81e2ff000000          and       edx,0FFh
  004229b7  f7da                  neg       edx
  004229b9  c1fa03                sar       edx,3
  004229bc  02d0                  add       dl,al
  004229be  8a463a                mov       al,[esi+3Ah]
  004229c1  8bc8                  mov       ecx,eax
  004229c3  885639                mov       [esi+39h],dl
  004229c6  81e1ff000000          and       ecx,0FFh
  004229cc  f7d9                  neg       ecx
  004229ce  c1f903                sar       ecx,3
  004229d1  02c8                  add       cl,al
  004229d3  66ff4e12              dec       word [esi+12h]
  004229d7  884e3a                mov       [esi+3Ah],cl
  004229da  66837e1200            cmp       word [esi+12h],0
  004229df  7f06                  jg        short 004229E7h
  004229e1  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004229e7:
  004229e7  5f                    pop       edi
  004229e8  5e                    pop       esi
  004229e9  5d                    pop       ebp
  004229ea  5b                    pop       ebx
  004229eb  83c420                add       esp,20h
  004229ee  c3                    ret
  004229f0  a2274200ad            mov       [0AD004227h],al
  004229f5  27                    daa
  004229f6  42                    inc       edx
  004229f7  00b8274200c3          add       [eax-3CFFBDD9h],bh
  004229fd  27                    daa
  004229fe  42                    inc       edx
  004229ff  00ce                  add       dh,cl
  00422a01  27                    daa
  00422a02  42                    inc       edx
  00422a03  00d9                  add       cl,bl
  00422a05  27                    daa
  00422a06  42                    inc       edx
  00422a07  00e4                  add       ah,ah
  00422a09  27                    daa
  00422a0a  42                    inc       edx
  00422a0b  00ef                  add       bh,ch
  00422a0d  27                    daa
  00422a0e  42                    inc       edx
  00422a0f  00fa                  add       dl,bh
  00422a11  27                    daa
  00422a12  42                    inc       edx
  00422a13  009090909090          add       [eax-6F6F6F70h],dl

19 cible(s) de saut interne : 0x00422511, 0x00422520, 0x00422568, 0x00422579, 0x00422589, 0x00422594, 0x0042262f, 0x00422697, 0x0042271f, 0x00422735, 0x00422770, 0x00422778, 0x00422803, 0x00422806, 0x00422824, 0x00422828, 0x004228ae, 0x004228c5, 0x004229e7
