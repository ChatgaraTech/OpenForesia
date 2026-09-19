Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00502cd0  section .text  offset fichier 0x1020d0  2288 octets

  00502cd0  83ec28                sub       esp,28h
  00502cd3  8d442408              lea       eax,[esp+8]
  00502cd7  8d4c2420              lea       ecx,[esp+20h]
  00502cdb  8d54241c              lea       edx,[esp+1Ch]
  00502cdf  53                    push      ebx
  00502ce0  55                    push      ebp
  00502ce1  56                    push      esi
  00502ce2  8b742438              mov       esi,[esp+38h]
  00502ce6  57                    push      edi
  00502ce7  50                    push      eax
  00502ce8  51                    push      ecx
  00502ce9  52                    push      edx
  00502cea  56                    push      esi
  00502ceb  e8905c0300            call      00538980h
  00502cf0  83c410                add       esp,10h
  00502cf3  8d44241c              lea       eax,[esp+1Ch]
  00502cf7  8d4c2410              lea       ecx,[esp+10h]
  00502cfb  8d54243c              lea       edx,[esp+3Ch]
  00502cff  50                    push      eax
  00502d00  51                    push      ecx
  00502d01  8d44241c              lea       eax,[esp+1Ch]
  00502d05  52                    push      edx
  00502d06  8d4c2440              lea       ecx,[esp+40h]
  00502d0a  50                    push      eax
  00502d0b  51                    push      ecx
  00502d0c  56                    push      esi
  00502d0d  e80e320300            call      00535F20h
  00502d12  8b442428              mov       eax,[esp+28h]
  00502d16  33ed                  xor       ebp,ebp
  00502d18  83c418                add       esp,18h
  00502d1b  3bc5                  cmp       eax,ebp
  00502d1d  bf01000000            mov       edi,1
  00502d22  bb09000000            mov       ebx,9
  00502d27  0f84ec060000          je        near 00503419h
  00502d2d  56                    push      esi
  00502d2e  e86d88fdff            call      004DB5A0h
  00502d33  8b06                  mov       eax,[esi]
  00502d35  83c404                add       esp,4
  00502d38  a802                  test      al,2
  00502d3a  0f8452040000          je        near 00503192h
  00502d40  f644241c10            test      byte [esp+1Ch],10h
  00502d45  740d                  je        short 00502D54h
  00502d47  24fd                  and       al,0FDh
  00502d49  c6464300              mov       byte [esi+43h],0
  00502d4d  8906                  mov       [esi],eax
  00502d4f  e9c5060000            jmp       00503419h

LAB_00502d54:
  00502d54  33c9                  xor       ecx,ecx
  00502d56  8a4e42                mov       cl,[esi+42h]
  00502d59  83f905                cmp       ecx,5
  00502d5c  0f87b7060000          ja        near 00503419h
  00502d62  ff248d94355000        jmp       dword [ecx*4+503594h]
  00502d69  bf0a000000            mov       edi,0Ah
  00502d6e  66397e1a              cmp       [esi+1Ah],di
  00502d72  751c                  jne       short 00502D90h
  00502d74  8d561c                lea       edx,[esi+1Ch]
  00502d77  52                    push      edx
  00502d78  6800180000            push      1800h
  00502d7d  6800040000            push      400h
  00502d82  6a3c                  push      3Ch
  00502d84  e897ad0300            call      0053DB20h
  00502d89  83c410                add       esp,10h
  00502d8c  66397e1a              cmp       [esi+1Ah],di

LAB_00502d90:
  00502d90  0f8c22010000          jl        near 00502EB8h
  00502d96  f60620                test      byte [esi],20h
  00502d99  0f8519010000          jne       near 00502EB8h
  00502d9f  8d5e1c                lea       ebx,[esi+1Ch]
  00502da2  55                    push      ebp
  00502da3  68f8315500            push      5531F8h
  00502da8  53                    push      ebx
  00502da9  56                    push      esi
  00502daa  e8816e0100            call      00519C30h
  00502daf  8bf8                  mov       edi,eax
  00502db1  83c410                add       esp,10h
  00502db4  3bfd                  cmp       edi,ebp
  00502db6  7406                  je        short 00502DBEh
  00502db8  8b06                  mov       eax,[esi]
  00502dba  0c20                  or        al,20h
  00502dbc  8906                  mov       [esi],eax

LAB_00502dbe:
  00502dbe  392db8e05d00          cmp       [5DE0B8h],ebp
  00502dc4  0f84ee000000          je        near 00502EB8h
  00502dca  8b2f                  mov       ebp,[edi]
  00502dcc  66c787960000001000    mov       word [edi+96h],10h
  00502dd5  83cd40                or        ebp,40h
  00502dd8  57                    push      edi
  00502dd9  892f                  mov       [edi],ebp
  00502ddb  8b4c241c              mov       ecx,[esp+1Ch]
  00502ddf  56                    push      esi
  00502de0  668b4130              mov       ax,[ecx+30h]
  00502de4  0fbfe8                movsx     ebp,ax
  00502de7  66d1f8                sar       ax,1
  00502dea  66894130              mov       [ecx+30h],ax
  00502dee  e88d6d0300            call      00539B80h
  00502df3  8b442420              mov       eax,[esp+20h]
  00502df7  83c408                add       esp,8
  00502dfa  6a00                  push      0
  00502dfc  57                    push      edi
  00502dfd  66896830              mov       [eax+30h],bp
  00502e01  56                    push      esi
  00502e02  c6460707              mov       byte [esi+7],7
  00502e06  e8058a0300            call      0053B810h
  00502e0b  83c40c                add       esp,0Ch
  00502e0e  8d6f1c                lea       ebp,[edi+1Ch]
  00502e11  55                    push      ebp
  00502e12  6800180000            push      1800h
  00502e17  6800030000            push      300h
  00502e1c  6a03                  push      3
  00502e1e  e8fdac0300            call      0053DB20h
  00502e23  8b07                  mov       eax,[edi]
  00502e25  83c410                add       esp,10h
  00502e28  a900000040            test      eax,40000000h
  00502e2d  0f8583000000          jne       near 00502EB6h
  00502e33  0fbf4e32              movsx     ecx,word [esi+32h]
  00502e37  51                    push      ecx
  00502e38  e8df2b0400            call      00545A1Ch
  00502e3d  8d0440                lea       eax,[eax+eax*2]
  00502e40  8d1480                lea       edx,[eax+eax*4]
  00502e43  d1e2                  shl       edx,1
  00502e45  895750                mov       [edi+50h],edx
  00502e48  0fbf4632              movsx     eax,word [esi+32h]
  00502e4c  50                    push      eax
  00502e4d  e8c42b0400            call      00545A16h
  00502e52  8d0440                lea       eax,[eax+eax*2]
  00502e55  8d0c80                lea       ecx,[eax+eax*4]
  00502e58  8b07                  mov       eax,[edi]
  00502e5a  d1e1                  shl       ecx,1
  00502e5c  a801                  test      al,1
  00502e5e  894f58                mov       [edi+58h],ecx
  00502e61  750b                  jne       short 00502E6Eh
  00502e63  0c01                  or        al,1
  00502e65  c7475400800100        mov       dword [edi+54h],18000h
  00502e6c  8907                  mov       [edi],eax

LAB_00502e6e:
  00502e6e  8b13                  mov       edx,[ebx]
  00502e70  8b4620                mov       eax,[esi+20h]
  00502e73  8b4e24                mov       ecx,[esi+24h]
  00502e76  89542420              mov       [esp+20h],edx
  00502e7a  8b5500                mov       edx,[ebp]
  00502e7d  89442424              mov       [esp+24h],eax
  00502e81  8913                  mov       [ebx],edx
  00502e83  8b4720                mov       eax,[edi+20h]
  00502e86  6aff                  push      0FFFFFFFFh
  00502e88  894c242c              mov       [esp+2Ch],ecx
  00502e8c  894620                mov       [esi+20h],eax
  00502e8f  8b4f24                mov       ecx,[edi+24h]
  00502e92  6a00                  push      0
  00502e94  6a39                  push      39h
  00502e96  56                    push      esi
  00502e97  894e24                mov       [esi+24h],ecx
  00502e9a  e8317bf7ff            call      0047A9D0h
  00502e9f  8b542430              mov       edx,[esp+30h]
  00502ea3  8b442434              mov       eax,[esp+34h]
  00502ea7  8b4c2438              mov       ecx,[esp+38h]
  00502eab  83c410                add       esp,10h
  00502eae  8913                  mov       [ebx],edx
  00502eb0  894620                mov       [esi+20h],eax
  00502eb3  894e24                mov       [esi+24h],ecx

LAB_00502eb6:
  00502eb6  33ed                  xor       ebp,ebp

LAB_00502eb8:
  00502eb8  8b06                  mov       eax,[esi]
  00502eba  a900000002            test      eax,2000000h
  00502ebf  7404                  je        short 00502EC5h
  00502ec1  24dd                  and       al,0DDh
  00502ec3  8906                  mov       [esi],eax

LAB_00502ec5:
  00502ec5  896e5c                mov       [esi+5Ch],ebp
  00502ec8  bb09000000            mov       ebx,9
  00502ecd  e947050000            jmp       00503419h
  00502ed2  66837e1a08            cmp       word [esi+1Ah],8
  00502ed7  757f                  jne       short 00502F58h
  00502ed9  8b15cc975e00          mov       edx,[5E97CCh]
  00502edf  6aff                  push      0FFFFFFFFh
  00502ee1  6aff                  push      0FFFFFFFFh
  00502ee3  8b82cc000000          mov       eax,[edx+0CCh]
  00502ee9  33d2                  xor       edx,edx
  00502eeb  8b08                  mov       ecx,[eax]
  00502eed  8a5103                mov       dl,[ecx+3]
  00502ef0  81c2c0000000          add       edx,0C0h
  00502ef6  52                    push      edx
  00502ef7  56                    push      esi
  00502ef8  e8d37af7ff            call      0047A9D0h
  00502efd  83c410                add       esp,10h
  00502f00  8d461c                lea       eax,[esi+1Ch]
  00502f03  50                    push      eax
  00502f04  6800180000            push      1800h
  00502f09  68fd060000            push      6FDh
  00502f0e  6a25                  push      25h
  00502f10  e80bac0300            call      0053DB20h
  00502f15  83c410                add       esp,10h
  00502f18  eb3e                  jmp       short 00502F58h
  00502f1a  66395e1a              cmp       [esi+1Ah],bx
  00502f1e  7538                  jne       short 00502F58h
  00502f20  8b0dcc975e00          mov       ecx,[5E97CCh]
  00502f26  6aff                  push      0FFFFFFFFh
  00502f28  6aff                  push      0FFFFFFFFh
  00502f2a  8b91cc000000          mov       edx,[ecx+0CCh]
  00502f30  33c9                  xor       ecx,ecx
  00502f32  8b02                  mov       eax,[edx]
  00502f34  8a4804                mov       cl,[eax+4]
  00502f37  81c1c0000000          add       ecx,0C0h
  00502f3d  51                    push      ecx
  00502f3e  56                    push      esi
  00502f3f  e88c7af7ff            call      0047A9D0h
  00502f44  83c410                add       esp,10h
  00502f47  8d561c                lea       edx,[esi+1Ch]
  00502f4a  52                    push      edx
  00502f4b  6878325500            push      553278h
  00502f50  e88bac0300            call      0053DBE0h
  00502f55  83c408                add       esp,8

LAB_00502f58:
  00502f58  8b06                  mov       eax,[esi]
  00502f5a  a900000002            test      eax,2000000h
  00502f5f  0f84b4040000          je        near 00503419h
  00502f65  24fd                  and       al,0FDh
  00502f67  8906                  mov       [esi],eax
  00502f69  e9ab040000            jmp       00503419h
  00502f6e  8a5643                mov       dl,[esi+43h]
  00502f71  8bca                  mov       ecx,edx
  00502f73  81e1ff000000          and       ecx,0FFh
  00502f79  2bcd                  sub       ecx,ebp
  00502f7b  0f8408010000          je        near 00503089h
  00502f81  49                    dec       ecx
  00502f82  0f8480000000          je        near 00503008h
  00502f88  49                    dec       ecx
  00502f89  0f858a040000          jne       near 00503419h
  00502f8f  a801                  test      al,1
  00502f91  7445                  je        short 00502FD8h
  00502f93  0fbf4628              movsx     eax,word [esi+28h]
  00502f97  8bc8                  mov       ecx,eax
  00502f99  c1f902                sar       ecx,2
  00502f9c  2bc1                  sub       eax,ecx
  00502f9e  83f820                cmp       eax,20h
  00502fa1  66894628              mov       [esi+28h],ax
  00502fa5  6689462c              mov       [esi+2Ch],ax
  00502fa9  0f8d6a040000          jge       near 00503419h
  00502faf  8b4660                mov       eax,[esi+60h]
  00502fb2  66896e28              mov       [esi+28h],bp
  00502fb6  0500003200            add       eax,320000h
  00502fbb  feca                  dec       dl
  00502fbd  66896e2c              mov       [esi+2Ch],bp
  00502fc1  894620                mov       [esi+20h],eax
  00502fc4  896e54                mov       [esi+54h],ebp
  00502fc7  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  00502fd0  885643                mov       [esi+43h],dl
  00502fd3  e941040000            jmp       00503419h

LAB_00502fd8:
  00502fd8  8b4e54                mov       ecx,[esi+54h]
  00502fdb  0d01200000            or        eax,2001h
  00502fe0  81c100800100          add       ecx,18000h
  00502fe6  6689ae9a000000        mov       [esi+9Ah],bp
  00502fed  894e54                mov       [esi+54h],ecx
  00502ff0  8d4e1c                lea       ecx,[esi+1Ch]
  00502ff3  51                    push      ecx
  00502ff4  6810325500            push      553210h
  00502ff9  8906                  mov       [esi],eax
  00502ffb  e8e0ab0300            call      0053DBE0h
  00503000  83c408                add       esp,8
  00503003  e911040000            jmp       00503419h

LAB_00503008:
  00503008  0fbf4e28              movsx     ecx,word [esi+28h]
  0050300c  ba00100000            mov       edx,1000h
  00503011  2bd1                  sub       edx,ecx
  00503013  c1fa02                sar       edx,2
  00503016  03ca                  add       ecx,edx
  00503018  a801                  test      al,1
  0050301a  66894e28              mov       [esi+28h],cx
  0050301e  66894e2c              mov       [esi+2Ch],cx
  00503022  7418                  je        short 0050303Ch
  00503024  b80c000000            mov       eax,0Ch
  00503029  6639461a              cmp       [esi+1Ah],ax
  0050302d  0f8ee6030000          jle       near 00503419h
  00503033  6689461a              mov       [esi+1Ah],ax
  00503037  e9dd030000            jmp       00503419h

LAB_0050303c:
  0050303c  a1cc975e00            mov       eax,[5E97CCh]
  00503041  6aff                  push      0FFFFFFFFh
  00503043  6aff                  push      0FFFFFFFFh
  00503045  8b88cc000000          mov       ecx,[eax+0CCh]
  0050304b  33c0                  xor       eax,eax
  0050304d  8b11                  mov       edx,[ecx]
  0050304f  8a4205                mov       al,[edx+5]
  00503052  05c0000000            add       eax,0C0h
  00503057  50                    push      eax
  00503058  56                    push      esi
  00503059  e87279f7ff            call      0047A9D0h
  0050305e  8a4643                mov       al,[esi+43h]
  00503061  8b3e                  mov       edi,[esi]
  00503063  83c410                add       esp,10h
  00503066  8d4e1c                lea       ecx,[esi+1Ch]
  00503069  fec8                  dec       al
  0050306b  81e7ffdfffff          and       edi,0FFFFDFFFh
  00503071  51                    push      ecx
  00503072  6830325500            push      553230h
  00503077  884643                mov       [esi+43h],al
  0050307a  893e                  mov       [esi],edi
  0050307c  e85fab0300            call      0053DBE0h
  00503081  83c408                add       esp,8
  00503084  e990030000            jmp       00503419h

LAB_00503089:
  00503089  a900000002            test      eax,2000000h
  0050308e  0f8485030000          je        near 00503419h
  00503094  24fd                  and       al,0FDh
  00503096  8906                  mov       [esi],eax
  00503098  e97c030000            jmp       00503419h
  0050309d  8a5643                mov       dl,[esi+43h]
  005030a0  8bca                  mov       ecx,edx
  005030a2  81e1ff000000          and       ecx,0FFh
  005030a8  2bcd                  sub       ecx,ebp
  005030aa  0f8469030000          je        near 00503419h
  005030b0  49                    dec       ecx
  005030b1  0f84ba000000          je        near 00503171h
  005030b7  49                    dec       ecx
  005030b8  0f855b030000          jne       near 00503419h
  005030be  66395e1a              cmp       [esi+1Ah],bx
  005030c2  7527                  jne       short 005030EBh
  005030c4  8b15cc975e00          mov       edx,[5E97CCh]
  005030ca  6aff                  push      0FFFFFFFFh
  005030cc  6aff                  push      0FFFFFFFFh
  005030ce  8b82cc000000          mov       eax,[edx+0CCh]
  005030d4  33d2                  xor       edx,edx
  005030d6  8b08                  mov       ecx,[eax]
  005030d8  8a5106                mov       dl,[ecx+6]
  005030db  81c2c0000000          add       edx,0C0h
  005030e1  52                    push      edx
  005030e2  56                    push      esi
  005030e3  e8e878f7ff            call      0047A9D0h
  005030e8  83c410                add       esp,10h

LAB_005030eb:
  005030eb  f70600000002          test      dword [esi],2000000h
  005030f1  0f8422030000          je        near 00503419h
  005030f7  8a4643                mov       al,[esi+43h]
  005030fa  66c746101400          mov       word [esi+10h],14h
  00503100  fec8                  dec       al
  00503102  884643                mov       [esi+43h],al
  00503105  e90f030000            jmp       00503419h
  0050310a  8a5643                mov       dl,[esi+43h]
  0050310d  8bca                  mov       ecx,edx
  0050310f  81e1ff000000          and       ecx,0FFh
  00503115  2bcd                  sub       ecx,ebp
  00503117  0f84fc020000          je        near 00503419h
  0050311d  49                    dec       ecx
  0050311e  7451                  je        short 00503171h
  00503120  49                    dec       ecx
  00503121  0f85f2020000          jne       near 00503419h
  00503127  66395e1a              cmp       [esi+1Ah],bx
  0050312b  7525                  jne       short 00503152h
  0050312d  a1cc975e00            mov       eax,[5E97CCh]
  00503132  6aff                  push      0FFFFFFFFh
  00503134  6aff                  push      0FFFFFFFFh
  00503136  8b88cc000000          mov       ecx,[eax+0CCh]
  0050313c  33c0                  xor       eax,eax
  0050313e  8b11                  mov       edx,[ecx]
  00503140  8a4207                mov       al,[edx+7]
  00503143  05c0000000            add       eax,0C0h
  00503148  50                    push      eax
  00503149  56                    push      esi
  0050314a  e88178f7ff            call      0047A9D0h
  0050314f  83c410                add       esp,10h

LAB_00503152:
  00503152  f70600000002          test      dword [esi],2000000h
  00503158  0f84bb020000          je        near 00503419h
  0050315e  8a4643                mov       al,[esi+43h]
  00503161  66c746102a00          mov       word [esi+10h],2Ah
  00503167  fec8                  dec       al
  00503169  884643                mov       [esi+43h],al
  0050316c  e9a8020000            jmp       00503419h

LAB_00503171:
  00503171  668b4e10              mov       cx,[esi+10h]
  00503175  663bcd                cmp       cx,bp
  00503178  740a                  je        short 00503184h
  0050317a  49                    dec       ecx
  0050317b  66894e10              mov       [esi+10h],cx
  0050317f  e995020000            jmp       00503419h

LAB_00503184:
  00503184  24fd                  and       al,0FDh
  00503186  feca                  dec       dl
  00503188  8906                  mov       [esi],eax
  0050318a  885643                mov       [esi+43h],dl
  0050318d  e987020000            jmp       00503419h

LAB_00503192:
  00503192  a810                  test      al,10h
  00503194  0f857f020000          jne       near 00503419h
  0050319a  a904080000            test      eax,804h
  0050319f  0f8453010000          je        near 005032F8h
  005031a5  f744241ce8030040      test      dword [esp+1Ch],400003E8h
  005031ad  740b                  je        short 005031BAh
  005031af  25fbf7ffff            and       eax,0FFFFF7FBh
  005031b4  c6464300              mov       byte [esi+43h],0
  005031b8  8906                  mov       [esi],eax

LAB_005031ba:
  005031ba  668b4610              mov       ax,[esi+10h]
  005031be  0fbfc8                movsx     ecx,ax
  005031c1  83f903                cmp       ecx,3
  005031c4  0f874f020000          ja        near 00503419h
  005031ca  ff248dac355000        jmp       dword [ecx*4+5035ACh]
  005031d1  8b06                  mov       eax,[esi]
  005031d3  a900000002            test      eax,2000000h
  005031d8  0f843b020000          je        near 00503419h
  005031de  80e4f7                and       ah,0F7h
  005031e1  8906                  mov       [esi],eax
  005031e3  e931020000            jmp       00503419h
  005031e8  b80a000000            mov       eax,0Ah
  005031ed  66394618              cmp       [esi+18h],ax
  005031f1  7411                  je        short 00503204h
  005031f3  894618                mov       [esi+18h],eax
  005031f6  8b06                  mov       eax,[esi]
  005031f8  25fbfffffe            and       eax,0FEFFFFFBh
  005031fd  8906                  mov       [esi],eax
  005031ff  e915020000            jmp       00503419h

LAB_00503204:
  00503204  66837e1a12            cmp       word [esi+1Ah],12h
  00503209  0f850a020000          jne       near 00503419h
  0050320f  33c0                  xor       eax,eax
  00503211  6aff                  push      0FFFFFFFFh
  00503213  668b4606              mov       ax,[esi+6]
  00503217  6aff                  push      0FFFFFFFFh
  00503219  8bc8                  mov       ecx,eax
  0050321b  c1f808                sar       eax,8
  0050321e  81e1ff000000          and       ecx,0FFh
  00503224  25ff000000            and       eax,0FFh
  00503229  51                    push      ecx
  0050322a  50                    push      eax
  0050322b  56                    push      esi
  0050322c  e84f770300            call      0053A980h
  00503231  83c414                add       esp,14h
  00503234  66ff4e10              dec       word [esi+10h]
  00503238  e9dc010000            jmp       00503419h
  0050323d  8a4e43                mov       cl,[esi+43h]
  00503240  fec9                  dec       cl
  00503242  884e43                mov       [esi+43h],cl
  00503245  7505                  jne       short 0050324Ch
  00503247  48                    dec       eax
  00503248  66894610              mov       [esi+10h],ax

LAB_0050324c:
  0050324c  8b44243c              mov       eax,[esp+3Ch]
  00503250  8d9080000000          lea       edx,[eax+80h]
  00503256  81e2ff0f0000          and       edx,0FFFh
  0050325c  81fa00010000          cmp       edx,100h
  00503262  7f09                  jg        short 0050326Dh
  00503264  66014632              add       [esi+32h],ax
  00503268  e9ac010000            jmp       00503419h

LAB_0050326d:
  0050326d  25ff0f0000            and       eax,0FFFh
  00503272  33c9                  xor       ecx,ecx
  00503274  3d00080000            cmp       eax,800h
  00503279  0f9dc1                setge     cl
  0050327c  49                    dec       ecx
  0050327d  81e100010000          and       ecx,100h
  00503283  83c180                add       ecx,0FFFFFF80h
  00503286  8bc1                  mov       eax,ecx
  00503288  66014632              add       [esi+32h],ax
  0050328c  e988010000            jmp       00503419h
  00503291  55                    push      ebp
  00503292  55                    push      ebp
  00503293  68bd000000            push      0BDh
  00503298  56                    push      esi
  00503299  e84278f7ff            call      0047AAE0h
  0050329e  83c410                add       esp,10h
  005032a1  e89a3e0400            call      00547140h
  005032a6  99                    cdq
  005032a7  33c2                  xor       eax,edx
  005032a9  33c9                  xor       ecx,ecx
  005032ab  2bc2                  sub       eax,edx
  005032ad  83e003                and       eax,3
  005032b0  33c2                  xor       eax,edx
  005032b2  2bc2                  sub       eax,edx
  005032b4  668b0c4508325500      mov       cx,[eax*2+553208h]
  005032bc  66894e06              mov       [esi+6],cx
  005032c0  a1c4975e00            mov       eax,[5E97C4h]
  005032c5  8bd1                  mov       edx,ecx
  005032c7  81e1ff000000          and       ecx,0FFh
  005032cd  8b809c000000          mov       eax,[eax+9Ch]
  005032d3  c1fa08                sar       edx,8
  005032d6  81e2ff000000          and       edx,0FFh
  005032dc  8d0c49                lea       ecx,[ecx+ecx*2]
  005032df  c1e104                shl       ecx,4
  005032e2  8b1490                mov       edx,[eax+edx*4]
  005032e5  8a440a13              mov       al,[edx+ecx+13h]
  005032e9  66ff4e10              dec       word [esi+10h]
  005032ed  884643                mov       [esi+43h],al
  005032f0  896e5c                mov       [esi+5Ch],ebp
  005032f3  e921010000            jmp       00503419h

LAB_005032f8:
  005032f8  33c9                  xor       ecx,ecx
  005032fa  8a4e42                mov       cl,[esi+42h]
  005032fd  2bcd                  sub       ecx,ebp
  005032ff  0f84e5000000          je        near 005033EAh
  00503305  49                    dec       ecx
  00503306  0f848a000000          je        near 00503396h
  0050330c  49                    dec       ecx
  0050330d  0f8506010000          jne       near 00503419h
  00503313  66397e18              cmp       [esi+18h],di
  00503317  740a                  je        short 00503323h
  00503319  0d00000001            or        eax,1000000h
  0050331e  897e18                mov       [esi+18h],edi
  00503321  8906                  mov       [esi],eax

LAB_00503323:
  00503323  0fbf4e10              movsx     ecx,word [esi+10h]
  00503327  394c2414              cmp       [esp+14h],ecx
  0050332b  7d09                  jge       short 00503336h
  0050332d  c6464300              mov       byte [esi+43h],0
  00503331  e9e3000000            jmp       00503419h

LAB_00503336:
  00503336  8a4643                mov       al,[esi+43h]
  00503339  84c0                  test      al,al
  0050333b  7405                  je        short 00503342h
  0050333d  fec8                  dec       al
  0050333f  884643                mov       [esi+43h],al

LAB_00503342:
  00503342  8b44243c              mov       eax,[esp+3Ch]
  00503346  8d9000010000          lea       edx,[eax+100h]
  0050334c  81e2ff0f0000          and       edx,0FFFh
  00503352  81fa00020000          cmp       edx,200h
  00503358  7f10                  jg        short 0050336Ah
  0050335a  66014632              add       [esi+32h],ax
  0050335e  c7465c00400100        mov       dword [esi+5Ch],14000h
  00503365  e9af000000            jmp       00503419h

LAB_0050336a:
  0050336a  25ff0f0000            and       eax,0FFFh
  0050336f  33c9                  xor       ecx,ecx
  00503371  3d00080000            cmp       eax,800h
  00503376  c7465c00400100        mov       dword [esi+5Ch],14000h
  0050337d  0f9dc1                setge     cl
  00503380  49                    dec       ecx
  00503381  81e100020000          and       ecx,200h
  00503387  81c100ffffff          add       ecx,0FFFFFF00h
  0050338d  66014e32              add       [esi+32h],cx
  00503391  e983000000            jmp       00503419h

LAB_00503396:
  00503396  66397e18              cmp       [esi+18h],di
  0050339a  740a                  je        short 005033A6h
  0050339c  0d00000001            or        eax,1000000h
  005033a1  897e18                mov       [esi+18h],edi
  005033a4  8906                  mov       [esi],eax

LAB_005033a6:
  005033a6  8b44243c              mov       eax,[esp+3Ch]
  005033aa  8d9000010000          lea       edx,[eax+100h]
  005033b0  81e2ff0f0000          and       edx,0FFFh
  005033b6  81fa00020000          cmp       edx,200h
  005033bc  7f0a                  jg        short 005033C8h
  005033be  66014632              add       [esi+32h],ax
  005033c2  c6464300              mov       byte [esi+43h],0
  005033c6  eb51                  jmp       short 00503419h

LAB_005033c8:
  005033c8  25ff0f0000            and       eax,0FFFh
  005033cd  33c9                  xor       ecx,ecx
  005033cf  3d00080000            cmp       eax,800h
  005033d4  0f9dc1                setge     cl
  005033d7  49                    dec       ecx
  005033d8  81e100020000          and       ecx,200h
  005033de  81c100ffffff          add       ecx,0FFFFFF00h
  005033e4  66014e32              add       [esi+32h],cx
  005033e8  eb2f                  jmp       short 00503419h

LAB_005033ea:
  005033ea  66397e18              cmp       [esi+18h],di
  005033ee  740a                  je        short 005033FAh
  005033f0  0d00000001            or        eax,1000000h
  005033f5  897e18                mov       [esi+18h],edi
  005033f8  8906                  mov       [esi],eax

LAB_005033fa:
  005033fa  817c24142c010000      cmp       dword [esp+14h],12Ch
  00503402  7d06                  jge       short 0050340Ah
  00503404  c6464300              mov       byte [esi+43h],0
  00503408  eb0f                  jmp       short 00503419h

LAB_0050340a:
  0050340a  8a4643                mov       al,[esi+43h]
  0050340d  84c0                  test      al,al
  0050340f  7405                  je        short 00503416h
  00503411  fec8                  dec       al
  00503413  884643                mov       [esi+43h],al

LAB_00503416:
  00503416  896e5c                mov       [esi+5Ch],ebp

LAB_00503419:
  00503419  8b542410              mov       edx,[esp+10h]
  0050341d  52                    push      edx
  0050341e  56                    push      esi
  0050341f  e83c4df8ff            call      00488160h
  00503424  83c408                add       esp,8
  00503427  56                    push      esi
  00503428  e813050300            call      00533940h
  0050342d  83c404                add       esp,4
  00503430  56                    push      esi
  00503431  e8bac6fdff            call      004DFAF0h
  00503436  8b442430              mov       eax,[esp+30h]
  0050343a  83c404                add       esp,4
  0050343d  50                    push      eax
  0050343e  56                    push      esi
  0050343f  e82c03feff            call      004E3770h
  00503444  8b442418              mov       eax,[esp+18h]
  00503448  83c408                add       esp,8
  0050344b  3bc5                  cmp       eax,ebp
  0050344d  0f8436010000          je        near 00503589h
  00503453  f70617080000          test      dword [esi],817h
  00503459  0f852a010000          jne       near 00503589h
  0050345f  8a4643                mov       al,[esi+43h]
  00503462  84c0                  test      al,al
  00503464  0f851f010000          jne       near 00503589h
  0050346a  e8d13c0400            call      00547140h
  0050346f  99                    cdq
  00503470  b964000000            mov       ecx,64h
  00503475  bf90010000            mov       edi,190h
  0050347a  f7f9                  idiv      ecx
  0050347c  8b442414              mov       eax,[esp+14h]
  00503480  3bc7                  cmp       eax,edi
  00503482  0f8d94000000          jge       near 0050351Ch
  00503488  83fa3c                cmp       edx,3Ch
  0050348b  7d7a                  jge       short 00503507h
  0050348d  83fa0a                cmp       edx,0Ah
  00503490  7d0d                  jge       short 0050349Fh
  00503492  c6464200              mov       byte [esi+42h],0
  00503496  c7461803000000        mov       dword [esi+18h],3
  0050349d  eb50                  jmp       short 005034EFh

LAB_0050349f:
  0050349f  83fa14                cmp       edx,14h
  005034a2  7d0d                  jge       short 005034B1h
  005034a4  c6464201              mov       byte [esi+42h],1
  005034a8  c7461804000000        mov       dword [esi+18h],4
  005034af  eb3e                  jmp       short 005034EFh

LAB_005034b1:
  005034b1  83fa1e                cmp       edx,1Eh
  005034b4  7d0d                  jge       short 005034C3h
  005034b6  c6464202              mov       byte [esi+42h],2
  005034ba  c7461805000000        mov       dword [esi+18h],5
  005034c1  eb2c                  jmp       short 005034EFh

LAB_005034c3:
  005034c3  83fa28                cmp       edx,28h
  005034c6  7d0d                  jge       short 005034D5h
  005034c8  c6464203              mov       byte [esi+42h],3
  005034cc  c7461808000000        mov       dword [esi+18h],8
  005034d3  eb16                  jmp       short 005034EBh

LAB_005034d5:
  005034d5  83fa32                cmp       edx,32h
  005034d8  7d06                  jge       short 005034E0h
  005034da  c6464204              mov       byte [esi+42h],4
  005034de  eb04                  jmp       short 005034E4h

LAB_005034e0:
  005034e0  c6464205              mov       byte [esi+42h],5

LAB_005034e4:
  005034e4  c7461806000000        mov       dword [esi+18h],6

LAB_005034eb:
  005034eb  c6464302              mov       byte [esi+43h],2

LAB_005034ef:
  005034ef  8b16                  mov       edx,[esi]
  005034f1  896e5c                mov       [esi+5Ch],ebp
  005034f4  81e2fffffffe          and       edx,0FEFFFFFFh
  005034fa  83ca02                or        edx,2
  005034fd  8916                  mov       [esi],edx
  005034ff  5f                    pop       edi
  00503500  5e                    pop       esi
  00503501  5d                    pop       ebp
  00503502  5b                    pop       ebx
  00503503  83c428                add       esp,28h
  00503506  c3                    ret

LAB_00503507:
  00503507  83fa46                cmp       edx,46h
  0050350a  7d5b                  jge       short 00503567h

LAB_0050350c:
  0050350c  c6464201              mov       byte [esi+42h],1
  00503510  c6464301              mov       byte [esi+43h],1
  00503514  5f                    pop       edi
  00503515  5e                    pop       esi
  00503516  5d                    pop       ebp
  00503517  5b                    pop       ebx
  00503518  83c428                add       esp,28h
  0050351b  c3                    ret

LAB_0050351c:
  0050351c  3d20030000            cmp       eax,320h
  00503521  7d58                  jge       short 0050357Bh
  00503523  e8183c0400            call      00547140h
  00503528  99                    cdq
  00503529  b964000000            mov       ecx,64h
  0050352e  f7f9                  idiv      ecx
  00503530  83fa2d                cmp       edx,2Dh
  00503533  7d32                  jge       short 00503567h
  00503535  8b54243c              mov       edx,[esp+3Ch]
  00503539  81c200010000          add       edx,100h
  0050353f  81e2ff0f0000          and       edx,0FFFh
  00503545  81fa00020000          cmp       edx,200h
  0050354b  7fbf                  jg        short 0050350Ch
  0050354d  8b06                  mov       eax,[esi]
  0050354f  895e18                mov       [esi+18h],ebx
  00503552  0d04080001            or        eax,1000804h
  00503557  66c746100300          mov       word [esi+10h],3
  0050355d  8906                  mov       [esi],eax
  0050355f  5f                    pop       edi
  00503560  5e                    pop       esi
  00503561  5d                    pop       ebp
  00503562  5b                    pop       ebx
  00503563  83c428                add       esp,28h
  00503566  c3                    ret

LAB_00503567:
  00503567  c6464202              mov       byte [esi+42h],2
  0050356b  c646431e              mov       byte [esi+43h],1Eh
  0050356f  66897e10              mov       [esi+10h],di
  00503573  5f                    pop       edi
  00503574  5e                    pop       esi
  00503575  5d                    pop       ebp
  00503576  5b                    pop       ebx
  00503577  83c428                add       esp,28h
  0050357a  c3                    ret

LAB_0050357b:
  0050357b  c6464202              mov       byte [esi+42h],2
  0050357f  c646431e              mov       byte [esi+43h],1Eh
  00503583  66c74610c800          mov       word [esi+10h],0C8h

LAB_00503589:
  00503589  5f                    pop       edi
  0050358a  5e                    pop       esi
  0050358b  5d                    pop       ebp
  0050358c  5b                    pop       ebx
  0050358d  83c428                add       esp,28h
  00503590  c3                    ret
  00503591  90                    nop
  00503592  8bff                  mov       edi,edi
  00503594  692d5000d22e50001a2f  imul      ebp,[2ED20050h],2F1A0050h
  0050359e  50                    push      eax
  0050359f  006e2f                add       [esi+2Fh],ch
  005035a2  50                    push      eax
  005035a3  009d3050000a          add       [ebp+0A005030h],bl
  005035a9  315000                xor       [eax],edx
  005035ac  d131                  sal       dword [ecx],1
  005035ae  50                    push      eax
  005035af  00e8                  add       al,ch
  005035b1  315000                xor       [eax],edx
  005035b4  3d32500091            cmp       eax,91005032h
  005035b9  325000                xor       dl,[eax]
  005035bc  90                    nop
  005035bd  90                    nop
  005035be  90                    nop
  005035bf  90                    nop

48 cible(s) de saut interne : 0x00502d54, 0x00502d90, 0x00502dbe, 0x00502e6e, 0x00502eb6, 0x00502eb8, 0x00502ec5, 0x00502f58, 0x00502fd8, 0x00503008, 0x0050303c, 0x00503089, 0x005030eb, 0x00503152, 0x00503171, 0x00503184, 0x00503192, 0x005031ba, 0x00503204, 0x0050324c, 0x0050326d, 0x005032f8, 0x00503323, 0x00503336, 0x00503342, 0x0050336a, 0x00503396, 0x005033a6, 0x005033c8, 0x005033ea, 0x005033fa, 0x0050340a, 0x00503416, 0x00503419, 0x0050349f, 0x005034b1, 0x005034c3, 0x005034d5, 0x005034e0, 0x005034e4, 0x005034eb, 0x005034ef, 0x00503507, 0x0050350c, 0x0050351c, 0x00503567, 0x0050357b, 0x00503589
