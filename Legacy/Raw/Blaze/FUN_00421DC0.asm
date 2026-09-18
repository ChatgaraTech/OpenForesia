
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00421dc0  section .text  offset fichier 0x211c0  1392 octets

  00421dc0  53                    push      ebx
  00421dc1  55                    push      ebp
  00421dc2  56                    push      esi
  00421dc3  8b742410              mov       esi,[esp+10h]
  00421dc7  57                    push      edi
  00421dc8  33db                  xor       ebx,ebx
  00421dca  8b7e78                mov       edi,[esi+78h]
  00421dcd  3bfb                  cmp       edi,ebx
  00421dcf  7505                  jne       short 00421DD6h
  00421dd1  bf58e55d00            mov       edi,5DE558h

LAB_00421dd6:
  00421dd6  395e44                cmp       [esi+44h],ebx
  00421dd9  0f854b030000          jne       near 0042212Ah
  00421ddf  66395e10              cmp       [esi+10h],bx
  00421de3  0f85af000000          jne       near 00421E98h
  00421de9  c7463c60045500        mov       dword [esi+3Ch],550460h
  00421df0  e84b531200            call      00547140h
  00421df5  99                    cdq
  00421df6  b903000000            mov       ecx,3
  00421dfb  c6460a0d              mov       byte [esi+0Ah],0Dh
  00421dff  f7f9                  idiv      ecx
  00421e01  c6460b01              mov       byte [esi+0Bh],1
  00421e05  66c746100100          mov       word [esi+10h],1
  00421e0b  885640                mov       [esi+40h],dl
  00421e0e  e82d531200            call      00547140h
  00421e13  99                    cdq
  00421e14  b92c010000            mov       ecx,12Ch
  00421e19  f7f9                  idiv      ecx
  00421e1b  8b461c                mov       eax,[esi+1Ch]
  00421e1e  81ea96000000          sub       edx,96h
  00421e24  c1e20c                shl       edx,0Ch
  00421e27  03d0                  add       edx,eax
  00421e29  895650                mov       [esi+50h],edx
  00421e2c  e80f531200            call      00547140h
  00421e31  99                    cdq
  00421e32  b92c010000            mov       ecx,12Ch
  00421e37  f7f9                  idiv      ecx
  00421e39  8b4624                mov       eax,[esi+24h]
  00421e3c  81ea96000000          sub       edx,96h
  00421e42  c1e20c                shl       edx,0Ch
  00421e45  03d0                  add       edx,eax
  00421e47  895658                mov       [esi+58h],edx
  00421e4a  e8f1521200            call      00547140h
  00421e4f  99                    cdq
  00421e50  b92c010000            mov       ecx,12Ch
  00421e55  66c746120a00          mov       word [esi+12h],0Ah
  00421e5b  f7f9                  idiv      ecx
  00421e5d  8b4620                mov       eax,[esi+20h]
  00421e60  83c250                add       edx,50h
  00421e63  c1e20c                shl       edx,0Ch
  00421e66  03d0                  add       edx,eax
  00421e68  895654                mov       [esi+54h],edx
  00421e6b  e8d0521200            call      00547140h
  00421e70  99                    cdq
  00421e71  33c2                  xor       eax,edx
  00421e73  66895e28              mov       [esi+28h],bx
  00421e77  2bc2                  sub       eax,edx
  00421e79  66895e2a              mov       [esi+2Ah],bx
  00421e7d  25ff070000            and       eax,7FFh
  00421e82  885e38                mov       [esi+38h],bl
  00421e85  33c2                  xor       eax,edx
  00421e87  885e39                mov       [esi+39h],bl
  00421e8a  2bc2                  sub       eax,edx
  00421e8c  885e3a                mov       [esi+3Ah],bl
  00421e8f  0500100000            add       eax,1000h
  00421e94  66894616              mov       [esi+16h],ax

LAB_00421e98:
  00421e98  66837e1001            cmp       word [esi+10h],1
  00421e9d  757f                  jne       short 00421F1Eh
  00421e9f  8b461c                mov       eax,[esi+1Ch]
  00421ea2  8b5650                mov       edx,[esi+50h]
  00421ea5  8b4e54                mov       ecx,[esi+54h]
  00421ea8  2bd0                  sub       edx,eax
  00421eaa  c1fa03                sar       edx,3
  00421ead  03d0                  add       edx,eax
  00421eaf  8b4620                mov       eax,[esi+20h]
  00421eb2  2bc8                  sub       ecx,eax
  00421eb4  89561c                mov       [esi+1Ch],edx
  00421eb7  8b5658                mov       edx,[esi+58h]
  00421eba  c1f903                sar       ecx,3
  00421ebd  03c8                  add       ecx,eax
  00421ebf  8b4624                mov       eax,[esi+24h]
  00421ec2  2bd0                  sub       edx,eax
  00421ec4  894e20                mov       [esi+20h],ecx
  00421ec7  668b4e28              mov       cx,[esi+28h]
  00421ecb  c1fa03                sar       edx,3
  00421ece  03d0                  add       edx,eax
  00421ed0  0fbf4616              movsx     eax,word [esi+16h]
  00421ed4  895624                mov       [esi+24h],edx
  00421ed7  8be8                  mov       ebp,eax
  00421ed9  0fbfd1                movsx     edx,cx
  00421edc  2bea                  sub       ebp,edx
  00421ede  c1fd03                sar       ebp,3
  00421ee1  03e9                  add       ebp,ecx
  00421ee3  668b4e2a              mov       cx,[esi+2Ah]
  00421ee7  0fbfd1                movsx     edx,cx
  00421eea  2bc2                  sub       eax,edx
  00421eec  66896e28              mov       [esi+28h],bp
  00421ef0  c1f803                sar       eax,3
  00421ef3  03c1                  add       eax,ecx
  00421ef5  66ff4e12              dec       word [esi+12h]
  00421ef9  66395e12              cmp       [esi+12h],bx
  00421efd  6689462a              mov       [esi+2Ah],ax
  00421f01  7f0c                  jg        short 00421F0Fh
  00421f03  66c746100200          mov       word [esi+10h],2
  00421f09  66c746121200          mov       word [esi+12h],12h

LAB_00421f0f:
  00421f0f  6a0a                  push      0Ah
  00421f11  6a0a                  push      0Ah
  00421f13  6a0a                  push      0Ah
  00421f15  56                    push      esi
  00421f16  e8c52efeff            call      00404DE0h
  00421f1b  83c410                add       esp,10h

LAB_00421f1e:
  00421f1e  66837e1002            cmp       word [esi+10h],2
  00421f23  0f8553010000          jne       near 0042207Ch
  00421f29  8b471c                mov       eax,[edi+1Ch]
  00421f2c  894650                mov       [esi+50h],eax
  00421f2f  8b4f20                mov       ecx,[edi+20h]
  00421f32  8b461c                mov       eax,[esi+1Ch]
  00421f35  81c100c00300          add       ecx,3C000h
  00421f3b  894e54                mov       [esi+54h],ecx
  00421f3e  8b4e50                mov       ecx,[esi+50h]
  00421f41  8b5724                mov       edx,[edi+24h]
  00421f44  2bc8                  sub       ecx,eax
  00421f46  c1f903                sar       ecx,3
  00421f49  895658                mov       [esi+58h],edx
  00421f4c  8b5654                mov       edx,[esi+54h]
  00421f4f  03c8                  add       ecx,eax
  00421f51  8b4620                mov       eax,[esi+20h]
  00421f54  8b6e58                mov       ebp,[esi+58h]
  00421f57  2bd0                  sub       edx,eax
  00421f59  c1fa03                sar       edx,3
  00421f5c  03c2                  add       eax,edx
  00421f5e  8b5624                mov       edx,[esi+24h]
  00421f61  2bea                  sub       ebp,edx
  00421f63  894e1c                mov       [esi+1Ch],ecx
  00421f66  c1fd03                sar       ebp,3
  00421f69  03d5                  add       edx,ebp
  00421f6b  66ff4e12              dec       word [esi+12h]
  00421f6f  66395e12              cmp       [esi+12h],bx
  00421f73  894620                mov       [esi+20h],eax
  00421f76  895624                mov       [esi+24h],edx
  00421f79  0f8fee000000          jg        near 0042206Dh
  00421f7f  8b6f1c                mov       ebp,[edi+1Ch]
  00421f82  2bcd                  sub       ecx,ebp
  00421f84  8b6f20                mov       ebp,[edi+20h]
  00421f87  2bc5                  sub       eax,ebp
  00421f89  8b6f24                mov       ebp,[edi+24h]
  00421f8c  c1f90c                sar       ecx,0Ch
  00421f8f  0500c00300            add       eax,3C000h
  00421f94  2bd5                  sub       edx,ebp
  00421f96  8be9                  mov       ebp,ecx
  00421f98  c1f80c                sar       eax,0Ch
  00421f9b  0fafe9                imul      ebp,ecx
  00421f9e  8bc8                  mov       ecx,eax
  00421fa0  c1fa0c                sar       edx,0Ch
  00421fa3  0fafc8                imul      ecx,eax
  00421fa6  8bc2                  mov       eax,edx
  00421fa8  03e9                  add       ebp,ecx
  00421faa  0fafc2                imul      eax,edx
  00421fad  03e8                  add       ebp,eax
  00421faf  55                    push      ebp
  00421fb0  e85b3a1200            call      00545A10h
  00421fb5  3d8c000000            cmp       eax,8Ch
  00421fba  0f8da0000000          jge       near 00422060h
  00421fc0  8b466c                mov       eax,[esi+6Ch]
  00421fc3  3bc3                  cmp       eax,ebx
  00421fc5  7505                  jne       short 00421FCCh
  00421fc7  b858e55d00            mov       eax,5DE558h

LAB_00421fcc:
  00421fcc  c7404401000000        mov       dword [eax+44h],1
  00421fd3  8b7e78                mov       edi,[esi+78h]
  00421fd6  3bfb                  cmp       edi,ebx
  00421fd8  7505                  jne       short 00421FDFh
  00421fda  bf58e55d00            mov       edi,5DE558h

LAB_00421fdf:
  00421fdf  66c746100300          mov       word [esi+10h],3
  00421fe5  e856511200            call      00547140h
  00421fea  99                    cdq
  00421feb  b905000000            mov       ecx,5
  00421ff0  f7f9                  idiv      ecx
  00421ff2  83c203                add       edx,3
  00421ff5  66895612              mov       [esi+12h],dx
  00421ff9  e842511200            call      00547140h
  00421ffe  99                    cdq
  00421fff  33c2                  xor       eax,edx
  00422001  2bc2                  sub       eax,edx
  00422003  25ff0f0000            and       eax,0FFFh
  00422008  33c2                  xor       eax,edx
  0042200a  2bc2                  sub       eax,edx
  0042200c  66894616              mov       [esi+16h],ax
  00422010  e82b511200            call      00547140h
  00422015  99                    cdq
  00422016  b91e000000            mov       ecx,1Eh
  0042201b  895e48                mov       [esi+48h],ebx
  0042201e  f7f9                  idiv      ecx
  00422020  83c228                add       edx,28h
  00422023  66895614              mov       [esi+14h],dx
  00422027  e814511200            call      00547140h
  0042202c  99                    cdq
  0042202d  b914000000            mov       ecx,14h
  00422032  f7f9                  idiv      ecx
  00422034  83c21e                add       edx,1Eh
  00422037  89565c                mov       [esi+5Ch],edx
  0042203a  8b571c                mov       edx,[edi+1Ch]
  0042203d  895650                mov       [esi+50h],edx
  00422040  8b4724                mov       eax,[edi+24h]
  00422043  894658                mov       [esi+58h],eax
  00422046  8b4f20                mov       ecx,[edi+20h]
  00422049  81c100e00100          add       ecx,1E000h
  0042204f  b064                  mov       al,64h
  00422051  894e20                mov       [esi+20h],ecx
  00422054  884638                mov       [esi+38h],al
  00422057  c646393c              mov       byte [esi+39h],3Ch
  0042205b  88463a                mov       [esi+3Ah],al
  0042205e  eb0d                  jmp       short 0042206Dh

LAB_00422060:
  00422060  66837e12f1            cmp       word [esi+12h],0FFF1h
  00422065  7d06                  jge       short 0042206Dh
  00422067  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042206d:
  0042206d  6afd                  push      0FFFFFFFDh
  0042206f  6afc                  push      0FFFFFFFCh
  00422071  6afd                  push      0FFFFFFFDh
  00422073  56                    push      esi
  00422074  e8672dfeff            call      00404DE0h
  00422079  83c410                add       esp,10h

LAB_0042207c:
  0042207c  66837e1003            cmp       word [esi+10h],3
  00422081  757c                  jne       short 004220FFh
  00422083  8b7e78                mov       edi,[esi+78h]
  00422086  3bfb                  cmp       edi,ebx
  00422088  7505                  jne       short 0042208Fh
  0042208a  bf58e55d00            mov       edi,5DE558h

LAB_0042208f:
  0042208f  8b571c                mov       edx,[edi+1Ch]
  00422092  0fbf4e16              movsx     ecx,word [esi+16h]
  00422096  895650                mov       [esi+50h],edx
  00422099  8b4724                mov       eax,[edi+24h]
  0042209c  51                    push      ecx
  0042209d  894658                mov       [esi+58h],eax
  004220a0  e877391200            call      00545A1Ch
  004220a5  0faf4648              imul      eax,[esi+48h]
  004220a9  0fbf5616              movsx     edx,word [esi+16h]
  004220ad  8b4e50                mov       ecx,[esi+50h]
  004220b0  8b6e48                mov       ebp,[esi+48h]
  004220b3  03c1                  add       eax,ecx
  004220b5  52                    push      edx
  004220b6  89461c                mov       [esi+1Ch],eax
  004220b9  e858391200            call      00545A16h
  004220be  0fafc5                imul      eax,ebp
  004220c1  8b4e58                mov       ecx,[esi+58h]
  004220c4  668146169600          add       word [esi+16h],96h
  004220ca  03c1                  add       eax,ecx
  004220cc  8b4e20                mov       ecx,[esi+20h]
  004220cf  894624                mov       [esi+24h],eax
  004220d2  0fbf4612              movsx     eax,word [esi+12h]
  004220d6  c1e00c                shl       eax,0Ch
  004220d9  03c8                  add       ecx,eax
  004220db  894e20                mov       [esi+20h],ecx
  004220de  0fbf4e14              movsx     ecx,word [esi+14h]
  004220e2  2bcd                  sub       ecx,ebp
  004220e4  c1f903                sar       ecx,3
  004220e7  03cd                  add       ecx,ebp
  004220e9  894e48                mov       [esi+48h],ecx
  004220ec  8b4e5c                mov       ecx,[esi+5Ch]
  004220ef  49                    dec       ecx
  004220f0  8bc1                  mov       eax,ecx
  004220f2  894e5c                mov       [esi+5Ch],ecx
  004220f5  3bc3                  cmp       eax,ebx
  004220f7  7f06                  jg        short 004220FFh
  004220f9  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004220ff:
  004220ff  8a4e40                mov       cl,[esi+40h]
  00422102  fec1                  inc       cl
  00422104  8ac1                  mov       al,cl
  00422106  884e40                mov       [esi+40h],cl
  00422109  3c03                  cmp       al,3
  0042210b  7503                  jne       short 00422110h
  0042210d  885e40                mov       [esi+40h],bl

LAB_00422110:
  00422110  56                    push      esi
  00422111  e82a181100            call      00533940h
  00422116  8b8690000000          mov       eax,[esi+90h]
  0042211c  83c404                add       esp,4
  0042211f  f6c488                test      ah,88h
  00422122  7406                  je        short 0042212Ah
  00422124  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042212a:
  0042212a  8b4644                mov       eax,[esi+44h]
  0042212d  bd01000000            mov       ebp,1
  00422132  3bc5                  cmp       eax,ebp
  00422134  0f85e2010000          jne       near 0042231Ch
  0042213a  66395e10              cmp       [esi+10h],bx
  0042213e  0f85aa000000          jne       near 004221EEh
  00422144  c7463c60045500        mov       dword [esi+3Ch],550460h
  0042214b  c6464003              mov       byte [esi+40h],3
  0042214f  c6460a09              mov       byte [esi+0Ah],9
  00422153  885e0b                mov       [esi+0Bh],bl
  00422156  66896e10              mov       [esi+10h],bp
  0042215a  8b571c                mov       edx,[edi+1Ch]
  0042215d  89561c                mov       [esi+1Ch],edx
  00422160  8b4720                mov       eax,[edi+20h]
  00422163  894620                mov       [esi+20h],eax
  00422166  8b4f24                mov       ecx,[edi+24h]
  00422169  894e24                mov       [esi+24h],ecx
  0042216c  e8cf4f1200            call      00547140h
  00422171  99                    cdq
  00422172  b9a0000000            mov       ecx,0A0h
  00422177  f7f9                  idiv      ecx
  00422179  8b4f1c                mov       ecx,[edi+1Ch]
  0042217c  83ea50                sub       edx,50h
  0042217f  c1e20c                shl       edx,0Ch
  00422182  03d1                  add       edx,ecx
  00422184  895650                mov       [esi+50h],edx
  00422187  e8b44f1200            call      00547140h
  0042218c  99                    cdq
  0042218d  b9a0000000            mov       ecx,0A0h
  00422192  f7f9                  idiv      ecx
  00422194  8b4724                mov       eax,[edi+24h]
  00422197  83ea50                sub       edx,50h
  0042219a  c1e20c                shl       edx,0Ch
  0042219d  03d0                  add       edx,eax
  0042219f  895658                mov       [esi+58h],edx
  004221a2  e8994f1200            call      00547140h
  004221a7  99                    cdq
  004221a8  b9c8000000            mov       ecx,0C8h
  004221ad  f7f9                  idiv      ecx
  004221af  8b4f20                mov       ecx,[edi+20h]
  004221b2  66c746120a00          mov       word [esi+12h],0Ah
  004221b8  81c2b4000000          add       edx,0B4h
  004221be  c1e20c                shl       edx,0Ch
  004221c1  03d1                  add       edx,ecx
  004221c3  895654                mov       [esi+54h],edx
  004221c6  e8754f1200            call      00547140h
  004221cb  99                    cdq
  004221cc  b9e8030000            mov       ecx,3E8h
  004221d1  66895e28              mov       [esi+28h],bx
  004221d5  f7f9                  idiv      ecx
  004221d7  66895e2a              mov       [esi+2Ah],bx
  004221db  885e38                mov       [esi+38h],bl
  004221de  885e39                mov       [esi+39h],bl
  004221e1  885e3a                mov       [esi+3Ah],bl
  004221e4  81c2d0070000          add       edx,7D0h
  004221ea  66895616              mov       [esi+16h],dx

LAB_004221ee:
  004221ee  66396e10              cmp       [esi+10h],bp
  004221f2  757f                  jne       short 00422273h
  004221f4  8b461c                mov       eax,[esi+1Ch]
  004221f7  8b5650                mov       edx,[esi+50h]
  004221fa  8b4e54                mov       ecx,[esi+54h]
  004221fd  2bd0                  sub       edx,eax
  004221ff  c1fa03                sar       edx,3
  00422202  03d0                  add       edx,eax
  00422204  8b4620                mov       eax,[esi+20h]
  00422207  2bc8                  sub       ecx,eax
  00422209  89561c                mov       [esi+1Ch],edx
  0042220c  8b5658                mov       edx,[esi+58h]
  0042220f  c1f903                sar       ecx,3
  00422212  03c8                  add       ecx,eax
  00422214  8b4624                mov       eax,[esi+24h]
  00422217  2bd0                  sub       edx,eax
  00422219  894e20                mov       [esi+20h],ecx
  0042221c  668b4e28              mov       cx,[esi+28h]
  00422220  c1fa03                sar       edx,3
  00422223  03d0                  add       edx,eax
  00422225  0fbf4616              movsx     eax,word [esi+16h]
  00422229  895624                mov       [esi+24h],edx
  0042222c  8be8                  mov       ebp,eax
  0042222e  0fbfd1                movsx     edx,cx
  00422231  2bea                  sub       ebp,edx
  00422233  c1fd03                sar       ebp,3
  00422236  03e9                  add       ebp,ecx
  00422238  668b4e2a              mov       cx,[esi+2Ah]
  0042223c  0fbfd1                movsx     edx,cx
  0042223f  2bc2                  sub       eax,edx
  00422241  66896e28              mov       [esi+28h],bp
  00422245  c1f803                sar       eax,3
  00422248  03c1                  add       eax,ecx
  0042224a  66ff4e12              dec       word [esi+12h]
  0042224e  66395e12              cmp       [esi+12h],bx
  00422252  6689462a              mov       [esi+2Ah],ax
  00422256  7f0c                  jg        short 00422264h
  00422258  66c746100200          mov       word [esi+10h],2
  0042225e  66c746121400          mov       word [esi+12h],14h

LAB_00422264:
  00422264  6a0a                  push      0Ah
  00422266  6a0a                  push      0Ah
  00422268  6a0a                  push      0Ah
  0042226a  56                    push      esi
  0042226b  e8702bfeff            call      00404DE0h
  00422270  83c410                add       esp,10h

LAB_00422273:
  00422273  66837e1002            cmp       word [esi+10h],2
  00422278  0f859e000000          jne       near 0042231Ch
  0042227e  8b466c                mov       eax,[esi+6Ch]
  00422281  8b481c                mov       ecx,[eax+1Ch]
  00422284  894e50                mov       [esi+50h],ecx
  00422287  8b5020                mov       edx,[eax+20h]
  0042228a  81c200c00300          add       edx,3C000h
  00422290  895654                mov       [esi+54h],edx
  00422293  8b4024                mov       eax,[eax+24h]
  00422296  894658                mov       [esi+58h],eax
  00422299  8b461c                mov       eax,[esi+1Ch]
  0042229c  2bc8                  sub       ecx,eax
  0042229e  8b6e58                mov       ebp,[esi+58h]
  004222a1  c1f903                sar       ecx,3
  004222a4  03c8                  add       ecx,eax
  004222a6  8b4620                mov       eax,[esi+20h]
  004222a9  2bd0                  sub       edx,eax
  004222ab  894e1c                mov       [esi+1Ch],ecx
  004222ae  c1fa03                sar       edx,3
  004222b1  03c2                  add       eax,edx
  004222b3  8b5624                mov       edx,[esi+24h]
  004222b6  2bea                  sub       ebp,edx
  004222b8  894620                mov       [esi+20h],eax
  004222bb  c1fd03                sar       ebp,3
  004222be  03d5                  add       edx,ebp
  004222c0  66ff4e12              dec       word [esi+12h]
  004222c4  66395e12              cmp       [esi+12h],bx
  004222c8  895624                mov       [esi+24h],edx
  004222cb  7f40                  jg        short 0042230Dh
  004222cd  8b1e                  mov       ebx,[esi]
  004222cf  81e3ffffff7f          and       ebx,7FFFFFFFh
  004222d5  891e                  mov       [esi],ebx
  004222d7  8b6f1c                mov       ebp,[edi+1Ch]
  004222da  8b5f24                mov       ebx,[edi+24h]
  004222dd  2bcd                  sub       ecx,ebp
  004222df  8b6f20                mov       ebp,[edi+20h]
  004222e2  2bd3                  sub       edx,ebx
  004222e4  2bc5                  sub       eax,ebp
  004222e6  c1f90c                sar       ecx,0Ch
  004222e9  0500c00300            add       eax,3C000h
  004222ee  8bf9                  mov       edi,ecx
  004222f0  c1f80c                sar       eax,0Ch
  004222f3  0faff9                imul      edi,ecx
  004222f6  8bc8                  mov       ecx,eax
  004222f8  c1fa0c                sar       edx,0Ch
  004222fb  0fafc8                imul      ecx,eax
  004222fe  8bc2                  mov       eax,edx
  00422300  03f9                  add       edi,ecx
  00422302  0fafc2                imul      eax,edx
  00422305  03f8                  add       edi,eax
  00422307  57                    push      edi
  00422308  e803371200            call      00545A10h

LAB_0042230d:
  0042230d  6afa                  push      0FFFFFFFAh
  0042230f  6af6                  push      0FFFFFFF6h
  00422311  6afa                  push      0FFFFFFFAh
  00422313  56                    push      esi
  00422314  e8c72afeff            call      00404DE0h
  00422319  83c410                add       esp,10h

LAB_0042231c:
  0042231c  5f                    pop       edi
  0042231d  5e                    pop       esi
  0042231e  5d                    pop       ebp
  0042231f  5b                    pop       ebx
  00422320  c3                    ret

18 cible(s) de saut interne : 0x00421dd6, 0x00421e98, 0x00421f0f, 0x00421f1e, 0x00421fcc, 0x00421fdf, 0x00422060, 0x0042206d, 0x0042207c, 0x0042208f, 0x004220ff, 0x00422110, 0x0042212a, 0x004221ee, 0x00422264, 0x00422273, 0x0042230d, 0x0042231c
