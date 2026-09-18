
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041ff40  section .text  offset fichier 0x1f340  1344 octets

  0041ff40  53                    push      ebx
  0041ff41  55                    push      ebp
  0041ff42  56                    push      esi
  0041ff43  8b742410              mov       esi,[esp+10h]
  0041ff47  33db                  xor       ebx,ebx
  0041ff49  57                    push      edi
  0041ff4a  8b4644                mov       eax,[esi+44h]
  0041ff4d  8b7e78                mov       edi,[esi+78h]
  0041ff50  3bc3                  cmp       eax,ebx
  0041ff52  0f8530030000          jne       near 00420288h
  0041ff58  66395e10              cmp       [esi+10h],bx
  0041ff5c  0f85af000000          jne       near 00420011h
  0041ff62  c7463c28025500        mov       dword [esi+3Ch],550228h
  0041ff69  e8d2711200            call      00547140h
  0041ff6e  99                    cdq
  0041ff6f  b903000000            mov       ecx,3
  0041ff74  c6460a0d              mov       byte [esi+0Ah],0Dh
  0041ff78  f7f9                  idiv      ecx
  0041ff7a  c6460b01              mov       byte [esi+0Bh],1
  0041ff7e  66c746100100          mov       word [esi+10h],1
  0041ff84  885640                mov       [esi+40h],dl
  0041ff87  e8b4711200            call      00547140h
  0041ff8c  99                    cdq
  0041ff8d  b92c010000            mov       ecx,12Ch
  0041ff92  f7f9                  idiv      ecx
  0041ff94  8b461c                mov       eax,[esi+1Ch]
  0041ff97  81ea96000000          sub       edx,96h
  0041ff9d  c1e20c                shl       edx,0Ch
  0041ffa0  03d0                  add       edx,eax
  0041ffa2  895650                mov       [esi+50h],edx
  0041ffa5  e896711200            call      00547140h
  0041ffaa  99                    cdq
  0041ffab  b92c010000            mov       ecx,12Ch
  0041ffb0  f7f9                  idiv      ecx
  0041ffb2  8b4624                mov       eax,[esi+24h]
  0041ffb5  81ea96000000          sub       edx,96h
  0041ffbb  c1e20c                shl       edx,0Ch
  0041ffbe  03d0                  add       edx,eax
  0041ffc0  895658                mov       [esi+58h],edx
  0041ffc3  e878711200            call      00547140h
  0041ffc8  99                    cdq
  0041ffc9  b92c010000            mov       ecx,12Ch
  0041ffce  66c746120a00          mov       word [esi+12h],0Ah
  0041ffd4  f7f9                  idiv      ecx
  0041ffd6  8b4620                mov       eax,[esi+20h]
  0041ffd9  83c250                add       edx,50h
  0041ffdc  c1e20c                shl       edx,0Ch
  0041ffdf  03d0                  add       edx,eax
  0041ffe1  895654                mov       [esi+54h],edx
  0041ffe4  e857711200            call      00547140h
  0041ffe9  99                    cdq
  0041ffea  33c2                  xor       eax,edx
  0041ffec  66895e28              mov       [esi+28h],bx
  0041fff0  2bc2                  sub       eax,edx
  0041fff2  66895e2a              mov       [esi+2Ah],bx
  0041fff6  25ff070000            and       eax,7FFh
  0041fffb  885e38                mov       [esi+38h],bl
  0041fffe  33c2                  xor       eax,edx
  00420000  885e39                mov       [esi+39h],bl
  00420003  2bc2                  sub       eax,edx
  00420005  885e3a                mov       [esi+3Ah],bl
  00420008  0500100000            add       eax,1000h
  0042000d  66894616              mov       [esi+16h],ax

LAB_00420011:
  00420011  66837e1001            cmp       word [esi+10h],1
  00420016  757f                  jne       short 00420097h
  00420018  8b461c                mov       eax,[esi+1Ch]
  0042001b  8b5650                mov       edx,[esi+50h]
  0042001e  8b4e54                mov       ecx,[esi+54h]
  00420021  2bd0                  sub       edx,eax
  00420023  c1fa03                sar       edx,3
  00420026  03d0                  add       edx,eax
  00420028  8b4620                mov       eax,[esi+20h]
  0042002b  2bc8                  sub       ecx,eax
  0042002d  89561c                mov       [esi+1Ch],edx
  00420030  8b5658                mov       edx,[esi+58h]
  00420033  c1f903                sar       ecx,3
  00420036  03c8                  add       ecx,eax
  00420038  8b4624                mov       eax,[esi+24h]
  0042003b  2bd0                  sub       edx,eax
  0042003d  894e20                mov       [esi+20h],ecx
  00420040  668b4e28              mov       cx,[esi+28h]
  00420044  c1fa03                sar       edx,3
  00420047  03d0                  add       edx,eax
  00420049  0fbf4616              movsx     eax,word [esi+16h]
  0042004d  895624                mov       [esi+24h],edx
  00420050  8be8                  mov       ebp,eax
  00420052  0fbfd1                movsx     edx,cx
  00420055  2bea                  sub       ebp,edx
  00420057  c1fd03                sar       ebp,3
  0042005a  03e9                  add       ebp,ecx
  0042005c  668b4e2a              mov       cx,[esi+2Ah]
  00420060  0fbfd1                movsx     edx,cx
  00420063  2bc2                  sub       eax,edx
  00420065  66896e28              mov       [esi+28h],bp
  00420069  c1f803                sar       eax,3
  0042006c  03c1                  add       eax,ecx
  0042006e  66ff4e12              dec       word [esi+12h]
  00420072  66395e12              cmp       [esi+12h],bx
  00420076  6689462a              mov       [esi+2Ah],ax
  0042007a  7f0c                  jg        short 00420088h
  0042007c  66c746100200          mov       word [esi+10h],2
  00420082  66c746121200          mov       word [esi+12h],12h

LAB_00420088:
  00420088  6a0a                  push      0Ah
  0042008a  6a0a                  push      0Ah
  0042008c  6a0a                  push      0Ah
  0042008e  56                    push      esi
  0042008f  e84c4dfeff            call      00404DE0h
  00420094  83c410                add       esp,10h

LAB_00420097:
  00420097  66837e1002            cmp       word [esi+10h],2
  0042009c  0f8541010000          jne       near 004201E3h
  004200a2  8b471c                mov       eax,[edi+1Ch]
  004200a5  894650                mov       [esi+50h],eax
  004200a8  8b4f20                mov       ecx,[edi+20h]
  004200ab  8b461c                mov       eax,[esi+1Ch]
  004200ae  81c100c00300          add       ecx,3C000h
  004200b4  894e54                mov       [esi+54h],ecx
  004200b7  8b4e50                mov       ecx,[esi+50h]
  004200ba  8b5724                mov       edx,[edi+24h]
  004200bd  2bc8                  sub       ecx,eax
  004200bf  c1f903                sar       ecx,3
  004200c2  895658                mov       [esi+58h],edx
  004200c5  8b5654                mov       edx,[esi+54h]
  004200c8  03c8                  add       ecx,eax
  004200ca  8b4620                mov       eax,[esi+20h]
  004200cd  8b6e58                mov       ebp,[esi+58h]
  004200d0  2bd0                  sub       edx,eax
  004200d2  c1fa03                sar       edx,3
  004200d5  03c2                  add       eax,edx
  004200d7  8b5624                mov       edx,[esi+24h]
  004200da  2bea                  sub       ebp,edx
  004200dc  894e1c                mov       [esi+1Ch],ecx
  004200df  c1fd03                sar       ebp,3
  004200e2  03d5                  add       edx,ebp
  004200e4  66ff4e12              dec       word [esi+12h]
  004200e8  66395e12              cmp       [esi+12h],bx
  004200ec  894620                mov       [esi+20h],eax
  004200ef  895624                mov       [esi+24h],edx
  004200f2  0f8fdc000000          jg        near 004201D4h
  004200f8  8b6f1c                mov       ebp,[edi+1Ch]
  004200fb  2bcd                  sub       ecx,ebp
  004200fd  8b6f20                mov       ebp,[edi+20h]
  00420100  2bc5                  sub       eax,ebp
  00420102  8b6f24                mov       ebp,[edi+24h]
  00420105  c1f90c                sar       ecx,0Ch
  00420108  0500c00300            add       eax,3C000h
  0042010d  2bd5                  sub       edx,ebp
  0042010f  8be9                  mov       ebp,ecx
  00420111  c1f80c                sar       eax,0Ch
  00420114  0fafe9                imul      ebp,ecx
  00420117  8bc8                  mov       ecx,eax
  00420119  c1fa0c                sar       edx,0Ch
  0042011c  0fafc8                imul      ecx,eax
  0042011f  8bc2                  mov       eax,edx
  00420121  03e9                  add       ebp,ecx
  00420123  0fafc2                imul      eax,edx
  00420126  03e8                  add       ebp,eax
  00420128  55                    push      ebp
  00420129  e8e2581200            call      00545A10h
  0042012e  3d8c000000            cmp       eax,8Ch
  00420133  0f8d8e000000          jge       near 004201C7h
  00420139  8b4e6c                mov       ecx,[esi+6Ch]
  0042013c  c7414401000000        mov       dword [ecx+44h],1
  00420143  8b7e78                mov       edi,[esi+78h]
  00420146  66c746100300          mov       word [esi+10h],3
  0042014c  e8ef6f1200            call      00547140h
  00420151  99                    cdq
  00420152  b905000000            mov       ecx,5
  00420157  f7f9                  idiv      ecx
  00420159  83c203                add       edx,3
  0042015c  66895612              mov       [esi+12h],dx
  00420160  e8db6f1200            call      00547140h
  00420165  99                    cdq
  00420166  33c2                  xor       eax,edx
  00420168  2bc2                  sub       eax,edx
  0042016a  25ff0f0000            and       eax,0FFFh
  0042016f  33c2                  xor       eax,edx
  00420171  2bc2                  sub       eax,edx
  00420173  66894616              mov       [esi+16h],ax
  00420177  e8c46f1200            call      00547140h
  0042017c  99                    cdq
  0042017d  b91e000000            mov       ecx,1Eh
  00420182  895e48                mov       [esi+48h],ebx
  00420185  f7f9                  idiv      ecx
  00420187  83c228                add       edx,28h
  0042018a  66895614              mov       [esi+14h],dx
  0042018e  e8ad6f1200            call      00547140h
  00420193  99                    cdq
  00420194  b914000000            mov       ecx,14h
  00420199  f7f9                  idiv      ecx
  0042019b  83c21e                add       edx,1Eh
  0042019e  89565c                mov       [esi+5Ch],edx
  004201a1  8b571c                mov       edx,[edi+1Ch]
  004201a4  895650                mov       [esi+50h],edx
  004201a7  8b4724                mov       eax,[edi+24h]
  004201aa  894658                mov       [esi+58h],eax
  004201ad  8b4f20                mov       ecx,[edi+20h]
  004201b0  81c100e00100          add       ecx,1E000h
  004201b6  b064                  mov       al,64h
  004201b8  894e20                mov       [esi+20h],ecx
  004201bb  884638                mov       [esi+38h],al
  004201be  c646393c              mov       byte [esi+39h],3Ch
  004201c2  88463a                mov       [esi+3Ah],al
  004201c5  eb0d                  jmp       short 004201D4h

LAB_004201c7:
  004201c7  66837e12f1            cmp       word [esi+12h],0FFF1h
  004201cc  7d06                  jge       short 004201D4h
  004201ce  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004201d4:
  004201d4  6afd                  push      0FFFFFFFDh
  004201d6  6afc                  push      0FFFFFFFCh
  004201d8  6afd                  push      0FFFFFFFDh
  004201da  56                    push      esi
  004201db  e8004cfeff            call      00404DE0h
  004201e0  83c410                add       esp,10h

LAB_004201e3:
  004201e3  66837e1003            cmp       word [esi+10h],3
  004201e8  7573                  jne       short 0042025Dh
  004201ea  8b7e78                mov       edi,[esi+78h]
  004201ed  0fbf4e16              movsx     ecx,word [esi+16h]
  004201f1  8b571c                mov       edx,[edi+1Ch]
  004201f4  51                    push      ecx
  004201f5  895650                mov       [esi+50h],edx
  004201f8  8b4724                mov       eax,[edi+24h]
  004201fb  894658                mov       [esi+58h],eax
  004201fe  e819581200            call      00545A1Ch
  00420203  0faf4648              imul      eax,[esi+48h]
  00420207  0fbf5616              movsx     edx,word [esi+16h]
  0042020b  8b4e50                mov       ecx,[esi+50h]
  0042020e  8b6e48                mov       ebp,[esi+48h]
  00420211  03c1                  add       eax,ecx
  00420213  52                    push      edx
  00420214  89461c                mov       [esi+1Ch],eax
  00420217  e8fa571200            call      00545A16h
  0042021c  0fafc5                imul      eax,ebp
  0042021f  8b4e58                mov       ecx,[esi+58h]
  00420222  668146169600          add       word [esi+16h],96h
  00420228  03c1                  add       eax,ecx
  0042022a  8b4e20                mov       ecx,[esi+20h]
  0042022d  894624                mov       [esi+24h],eax
  00420230  0fbf4612              movsx     eax,word [esi+12h]
  00420234  c1e00c                shl       eax,0Ch
  00420237  03c8                  add       ecx,eax
  00420239  894e20                mov       [esi+20h],ecx
  0042023c  0fbf4e14              movsx     ecx,word [esi+14h]
  00420240  2bcd                  sub       ecx,ebp
  00420242  c1f903                sar       ecx,3
  00420245  03cd                  add       ecx,ebp
  00420247  894e48                mov       [esi+48h],ecx
  0042024a  8b4e5c                mov       ecx,[esi+5Ch]
  0042024d  49                    dec       ecx
  0042024e  8bc1                  mov       eax,ecx
  00420250  894e5c                mov       [esi+5Ch],ecx
  00420253  3bc3                  cmp       eax,ebx
  00420255  7f06                  jg        short 0042025Dh
  00420257  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042025d:
  0042025d  8a4e40                mov       cl,[esi+40h]
  00420260  fec1                  inc       cl
  00420262  8ac1                  mov       al,cl
  00420264  884e40                mov       [esi+40h],cl
  00420267  3c03                  cmp       al,3
  00420269  7503                  jne       short 0042026Eh
  0042026b  885e40                mov       [esi+40h],bl

LAB_0042026e:
  0042026e  56                    push      esi
  0042026f  e8cc361100            call      00533940h
  00420274  8b8690000000          mov       eax,[esi+90h]
  0042027a  83c404                add       esp,4
  0042027d  f6c488                test      ah,88h
  00420280  7406                  je        short 00420288h
  00420282  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00420288:
  00420288  8b4644                mov       eax,[esi+44h]
  0042028b  bd01000000            mov       ebp,1
  00420290  3bc5                  cmp       eax,ebp
  00420292  0f85e2010000          jne       near 0042047Ah
  00420298  66395e10              cmp       [esi+10h],bx
  0042029c  0f85aa000000          jne       near 0042034Ch
  004202a2  c7463c28025500        mov       dword [esi+3Ch],550228h
  004202a9  c6464003              mov       byte [esi+40h],3
  004202ad  c6460a09              mov       byte [esi+0Ah],9
  004202b1  885e0b                mov       [esi+0Bh],bl
  004202b4  66896e10              mov       [esi+10h],bp
  004202b8  8b571c                mov       edx,[edi+1Ch]
  004202bb  89561c                mov       [esi+1Ch],edx
  004202be  8b4720                mov       eax,[edi+20h]
  004202c1  894620                mov       [esi+20h],eax
  004202c4  8b4f24                mov       ecx,[edi+24h]
  004202c7  894e24                mov       [esi+24h],ecx
  004202ca  e8716e1200            call      00547140h
  004202cf  99                    cdq
  004202d0  b9a0000000            mov       ecx,0A0h
  004202d5  f7f9                  idiv      ecx
  004202d7  8b4f1c                mov       ecx,[edi+1Ch]
  004202da  83ea50                sub       edx,50h
  004202dd  c1e20c                shl       edx,0Ch
  004202e0  03d1                  add       edx,ecx
  004202e2  895650                mov       [esi+50h],edx
  004202e5  e8566e1200            call      00547140h
  004202ea  99                    cdq
  004202eb  b9a0000000            mov       ecx,0A0h
  004202f0  f7f9                  idiv      ecx
  004202f2  8b4724                mov       eax,[edi+24h]
  004202f5  83ea50                sub       edx,50h
  004202f8  c1e20c                shl       edx,0Ch
  004202fb  03d0                  add       edx,eax
  004202fd  895658                mov       [esi+58h],edx
  00420300  e83b6e1200            call      00547140h
  00420305  99                    cdq
  00420306  b9c8000000            mov       ecx,0C8h
  0042030b  f7f9                  idiv      ecx
  0042030d  8b4f20                mov       ecx,[edi+20h]
  00420310  66c746120a00          mov       word [esi+12h],0Ah
  00420316  81c2b4000000          add       edx,0B4h
  0042031c  c1e20c                shl       edx,0Ch
  0042031f  03d1                  add       edx,ecx
  00420321  895654                mov       [esi+54h],edx
  00420324  e8176e1200            call      00547140h
  00420329  99                    cdq
  0042032a  b9e8030000            mov       ecx,3E8h
  0042032f  66895e28              mov       [esi+28h],bx
  00420333  f7f9                  idiv      ecx
  00420335  66895e2a              mov       [esi+2Ah],bx
  00420339  885e38                mov       [esi+38h],bl
  0042033c  885e39                mov       [esi+39h],bl
  0042033f  885e3a                mov       [esi+3Ah],bl
  00420342  81c2d0070000          add       edx,7D0h
  00420348  66895616              mov       [esi+16h],dx

LAB_0042034c:
  0042034c  66396e10              cmp       [esi+10h],bp
  00420350  757f                  jne       short 004203D1h
  00420352  8b461c                mov       eax,[esi+1Ch]
  00420355  8b5650                mov       edx,[esi+50h]
  00420358  8b4e54                mov       ecx,[esi+54h]
  0042035b  2bd0                  sub       edx,eax
  0042035d  c1fa03                sar       edx,3
  00420360  03d0                  add       edx,eax
  00420362  8b4620                mov       eax,[esi+20h]
  00420365  2bc8                  sub       ecx,eax
  00420367  89561c                mov       [esi+1Ch],edx
  0042036a  8b5658                mov       edx,[esi+58h]
  0042036d  c1f903                sar       ecx,3
  00420370  03c8                  add       ecx,eax
  00420372  8b4624                mov       eax,[esi+24h]
  00420375  2bd0                  sub       edx,eax
  00420377  894e20                mov       [esi+20h],ecx
  0042037a  668b4e28              mov       cx,[esi+28h]
  0042037e  c1fa03                sar       edx,3
  00420381  03d0                  add       edx,eax
  00420383  0fbf4616              movsx     eax,word [esi+16h]
  00420387  895624                mov       [esi+24h],edx
  0042038a  8be8                  mov       ebp,eax
  0042038c  0fbfd1                movsx     edx,cx
  0042038f  2bea                  sub       ebp,edx
  00420391  c1fd03                sar       ebp,3
  00420394  03e9                  add       ebp,ecx
  00420396  668b4e2a              mov       cx,[esi+2Ah]
  0042039a  0fbfd1                movsx     edx,cx
  0042039d  2bc2                  sub       eax,edx
  0042039f  66896e28              mov       [esi+28h],bp
  004203a3  c1f803                sar       eax,3
  004203a6  03c1                  add       eax,ecx
  004203a8  66ff4e12              dec       word [esi+12h]
  004203ac  66395e12              cmp       [esi+12h],bx
  004203b0  6689462a              mov       [esi+2Ah],ax
  004203b4  7f0c                  jg        short 004203C2h
  004203b6  66c746100200          mov       word [esi+10h],2
  004203bc  66c746121400          mov       word [esi+12h],14h

LAB_004203c2:
  004203c2  6a0a                  push      0Ah
  004203c4  6a0a                  push      0Ah
  004203c6  6a0a                  push      0Ah
  004203c8  56                    push      esi
  004203c9  e8124afeff            call      00404DE0h
  004203ce  83c410                add       esp,10h

LAB_004203d1:
  004203d1  66837e1002            cmp       word [esi+10h],2
  004203d6  0f859e000000          jne       near 0042047Ah
  004203dc  8b466c                mov       eax,[esi+6Ch]
  004203df  8b481c                mov       ecx,[eax+1Ch]
  004203e2  894e50                mov       [esi+50h],ecx
  004203e5  8b5020                mov       edx,[eax+20h]
  004203e8  81c200c00300          add       edx,3C000h
  004203ee  895654                mov       [esi+54h],edx
  004203f1  8b4024                mov       eax,[eax+24h]
  004203f4  894658                mov       [esi+58h],eax
  004203f7  8b461c                mov       eax,[esi+1Ch]
  004203fa  2bc8                  sub       ecx,eax
  004203fc  8b6e58                mov       ebp,[esi+58h]
  004203ff  c1f903                sar       ecx,3
  00420402  03c8                  add       ecx,eax
  00420404  8b4620                mov       eax,[esi+20h]
  00420407  2bd0                  sub       edx,eax
  00420409  894e1c                mov       [esi+1Ch],ecx
  0042040c  c1fa03                sar       edx,3
  0042040f  03c2                  add       eax,edx
  00420411  8b5624                mov       edx,[esi+24h]
  00420414  2bea                  sub       ebp,edx
  00420416  894620                mov       [esi+20h],eax
  00420419  c1fd03                sar       ebp,3
  0042041c  03d5                  add       edx,ebp
  0042041e  66ff4e12              dec       word [esi+12h]
  00420422  66395e12              cmp       [esi+12h],bx
  00420426  895624                mov       [esi+24h],edx
  00420429  7f40                  jg        short 0042046Bh
  0042042b  8b1e                  mov       ebx,[esi]
  0042042d  81e3ffffff7f          and       ebx,7FFFFFFFh
  00420433  891e                  mov       [esi],ebx
  00420435  8b6f1c                mov       ebp,[edi+1Ch]
  00420438  8b5f24                mov       ebx,[edi+24h]
  0042043b  2bcd                  sub       ecx,ebp
  0042043d  8b6f20                mov       ebp,[edi+20h]
  00420440  2bd3                  sub       edx,ebx
  00420442  2bc5                  sub       eax,ebp
  00420444  c1f90c                sar       ecx,0Ch
  00420447  0500c00300            add       eax,3C000h
  0042044c  8bf9                  mov       edi,ecx
  0042044e  c1f80c                sar       eax,0Ch
  00420451  0faff9                imul      edi,ecx
  00420454  8bc8                  mov       ecx,eax
  00420456  c1fa0c                sar       edx,0Ch
  00420459  0fafc8                imul      ecx,eax
  0042045c  8bc2                  mov       eax,edx
  0042045e  03f9                  add       edi,ecx
  00420460  0fafc2                imul      eax,edx
  00420463  03f8                  add       edi,eax
  00420465  57                    push      edi
  00420466  e8a5551200            call      00545A10h

LAB_0042046b:
  0042046b  6afa                  push      0FFFFFFFAh
  0042046d  6af6                  push      0FFFFFFF6h
  0042046f  6afa                  push      0FFFFFFFAh
  00420471  56                    push      esi
  00420472  e86949feff            call      00404DE0h
  00420477  83c410                add       esp,10h

LAB_0042047a:
  0042047a  5f                    pop       edi
  0042047b  5e                    pop       esi
  0042047c  5d                    pop       ebp
  0042047d  5b                    pop       ebx
  0042047e  c3                    ret

14 cible(s) de saut interne : 0x00420011, 0x00420088, 0x00420097, 0x004201c7, 0x004201d4, 0x004201e3, 0x0042025d, 0x0042026e, 0x00420288, 0x0042034c, 0x004203c2, 0x004203d1, 0x0042046b, 0x0042047a
