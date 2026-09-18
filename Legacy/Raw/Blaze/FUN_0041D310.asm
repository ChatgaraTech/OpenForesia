
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041d310  section .text  offset fichier 0x1c710  1360 octets

  0041d310  53                    push      ebx
  0041d311  55                    push      ebp
  0041d312  56                    push      esi
  0041d313  8b742410              mov       esi,[esp+10h]
  0041d317  57                    push      edi
  0041d318  33db                  xor       ebx,ebx
  0041d31a  8b7e78                mov       edi,[esi+78h]
  0041d31d  3bfb                  cmp       edi,ebx
  0041d31f  0f842b050000          je        near 0041D850h
  0041d325  395e44                cmp       [esi+44h],ebx
  0041d328  0f8530030000          jne       near 0041D65Eh
  0041d32e  66395e10              cmp       [esi+10h],bx
  0041d332  0f85af000000          jne       near 0041D3E7h
  0041d338  c7463c40005500        mov       dword [esi+3Ch],550040h
  0041d33f  e8fc9d1200            call      00547140h
  0041d344  99                    cdq
  0041d345  b903000000            mov       ecx,3
  0041d34a  c6460a0d              mov       byte [esi+0Ah],0Dh
  0041d34e  f7f9                  idiv      ecx
  0041d350  c6460b01              mov       byte [esi+0Bh],1
  0041d354  66c746100100          mov       word [esi+10h],1
  0041d35a  885640                mov       [esi+40h],dl
  0041d35d  e8de9d1200            call      00547140h
  0041d362  99                    cdq
  0041d363  b92c010000            mov       ecx,12Ch
  0041d368  f7f9                  idiv      ecx
  0041d36a  8b461c                mov       eax,[esi+1Ch]
  0041d36d  81ea96000000          sub       edx,96h
  0041d373  c1e20c                shl       edx,0Ch
  0041d376  03d0                  add       edx,eax
  0041d378  895650                mov       [esi+50h],edx
  0041d37b  e8c09d1200            call      00547140h
  0041d380  99                    cdq
  0041d381  b92c010000            mov       ecx,12Ch
  0041d386  f7f9                  idiv      ecx
  0041d388  8b4624                mov       eax,[esi+24h]
  0041d38b  81ea96000000          sub       edx,96h
  0041d391  c1e20c                shl       edx,0Ch
  0041d394  03d0                  add       edx,eax
  0041d396  895658                mov       [esi+58h],edx
  0041d399  e8a29d1200            call      00547140h
  0041d39e  99                    cdq
  0041d39f  b92c010000            mov       ecx,12Ch
  0041d3a4  66c746120a00          mov       word [esi+12h],0Ah
  0041d3aa  f7f9                  idiv      ecx
  0041d3ac  8b4620                mov       eax,[esi+20h]
  0041d3af  83c250                add       edx,50h
  0041d3b2  c1e20c                shl       edx,0Ch
  0041d3b5  03d0                  add       edx,eax
  0041d3b7  895654                mov       [esi+54h],edx
  0041d3ba  e8819d1200            call      00547140h
  0041d3bf  99                    cdq
  0041d3c0  33c2                  xor       eax,edx
  0041d3c2  66895e28              mov       [esi+28h],bx
  0041d3c6  2bc2                  sub       eax,edx
  0041d3c8  66895e2a              mov       [esi+2Ah],bx
  0041d3cc  25ff070000            and       eax,7FFh
  0041d3d1  885e38                mov       [esi+38h],bl
  0041d3d4  33c2                  xor       eax,edx
  0041d3d6  885e39                mov       [esi+39h],bl
  0041d3d9  2bc2                  sub       eax,edx
  0041d3db  885e3a                mov       [esi+3Ah],bl
  0041d3de  0500100000            add       eax,1000h
  0041d3e3  66894616              mov       [esi+16h],ax

LAB_0041d3e7:
  0041d3e7  66837e1001            cmp       word [esi+10h],1
  0041d3ec  757f                  jne       short 0041D46Dh
  0041d3ee  8b461c                mov       eax,[esi+1Ch]
  0041d3f1  8b5650                mov       edx,[esi+50h]
  0041d3f4  8b4e54                mov       ecx,[esi+54h]
  0041d3f7  2bd0                  sub       edx,eax
  0041d3f9  c1fa03                sar       edx,3
  0041d3fc  03d0                  add       edx,eax
  0041d3fe  8b4620                mov       eax,[esi+20h]
  0041d401  2bc8                  sub       ecx,eax
  0041d403  89561c                mov       [esi+1Ch],edx
  0041d406  8b5658                mov       edx,[esi+58h]
  0041d409  c1f903                sar       ecx,3
  0041d40c  03c8                  add       ecx,eax
  0041d40e  8b4624                mov       eax,[esi+24h]
  0041d411  2bd0                  sub       edx,eax
  0041d413  894e20                mov       [esi+20h],ecx
  0041d416  668b4e28              mov       cx,[esi+28h]
  0041d41a  c1fa03                sar       edx,3
  0041d41d  03d0                  add       edx,eax
  0041d41f  0fbf4616              movsx     eax,word [esi+16h]
  0041d423  895624                mov       [esi+24h],edx
  0041d426  8be8                  mov       ebp,eax
  0041d428  0fbfd1                movsx     edx,cx
  0041d42b  2bea                  sub       ebp,edx
  0041d42d  c1fd03                sar       ebp,3
  0041d430  03e9                  add       ebp,ecx
  0041d432  668b4e2a              mov       cx,[esi+2Ah]
  0041d436  0fbfd1                movsx     edx,cx
  0041d439  2bc2                  sub       eax,edx
  0041d43b  66896e28              mov       [esi+28h],bp
  0041d43f  c1f803                sar       eax,3
  0041d442  03c1                  add       eax,ecx
  0041d444  66ff4e12              dec       word [esi+12h]
  0041d448  66395e12              cmp       [esi+12h],bx
  0041d44c  6689462a              mov       [esi+2Ah],ax
  0041d450  7f0c                  jg        short 0041D45Eh
  0041d452  66c746100200          mov       word [esi+10h],2
  0041d458  66c746121200          mov       word [esi+12h],12h

LAB_0041d45e:
  0041d45e  6a0a                  push      0Ah
  0041d460  6a0a                  push      0Ah
  0041d462  6a0a                  push      0Ah
  0041d464  56                    push      esi
  0041d465  e87679feff            call      00404DE0h
  0041d46a  83c410                add       esp,10h

LAB_0041d46d:
  0041d46d  66837e1002            cmp       word [esi+10h],2
  0041d472  0f8541010000          jne       near 0041D5B9h
  0041d478  8b471c                mov       eax,[edi+1Ch]
  0041d47b  894650                mov       [esi+50h],eax
  0041d47e  8b4f20                mov       ecx,[edi+20h]
  0041d481  8b461c                mov       eax,[esi+1Ch]
  0041d484  81c100c00300          add       ecx,3C000h
  0041d48a  894e54                mov       [esi+54h],ecx
  0041d48d  8b4e50                mov       ecx,[esi+50h]
  0041d490  8b5724                mov       edx,[edi+24h]
  0041d493  2bc8                  sub       ecx,eax
  0041d495  c1f903                sar       ecx,3
  0041d498  895658                mov       [esi+58h],edx
  0041d49b  8b5654                mov       edx,[esi+54h]
  0041d49e  03c8                  add       ecx,eax
  0041d4a0  8b4620                mov       eax,[esi+20h]
  0041d4a3  8b6e58                mov       ebp,[esi+58h]
  0041d4a6  2bd0                  sub       edx,eax
  0041d4a8  c1fa03                sar       edx,3
  0041d4ab  03c2                  add       eax,edx
  0041d4ad  8b5624                mov       edx,[esi+24h]
  0041d4b0  2bea                  sub       ebp,edx
  0041d4b2  894e1c                mov       [esi+1Ch],ecx
  0041d4b5  c1fd03                sar       ebp,3
  0041d4b8  03d5                  add       edx,ebp
  0041d4ba  66ff4e12              dec       word [esi+12h]
  0041d4be  66395e12              cmp       [esi+12h],bx
  0041d4c2  894620                mov       [esi+20h],eax
  0041d4c5  895624                mov       [esi+24h],edx
  0041d4c8  0f8fdc000000          jg        near 0041D5AAh
  0041d4ce  8b6f1c                mov       ebp,[edi+1Ch]
  0041d4d1  2bcd                  sub       ecx,ebp
  0041d4d3  8b6f20                mov       ebp,[edi+20h]
  0041d4d6  2bc5                  sub       eax,ebp
  0041d4d8  8b6f24                mov       ebp,[edi+24h]
  0041d4db  c1f90c                sar       ecx,0Ch
  0041d4de  0500c00300            add       eax,3C000h
  0041d4e3  2bd5                  sub       edx,ebp
  0041d4e5  8be9                  mov       ebp,ecx
  0041d4e7  c1f80c                sar       eax,0Ch
  0041d4ea  0fafe9                imul      ebp,ecx
  0041d4ed  8bc8                  mov       ecx,eax
  0041d4ef  c1fa0c                sar       edx,0Ch
  0041d4f2  0fafc8                imul      ecx,eax
  0041d4f5  8bc2                  mov       eax,edx
  0041d4f7  03e9                  add       ebp,ecx
  0041d4f9  0fafc2                imul      eax,edx
  0041d4fc  03e8                  add       ebp,eax
  0041d4fe  55                    push      ebp
  0041d4ff  e80c851200            call      00545A10h
  0041d504  3d8c000000            cmp       eax,8Ch
  0041d509  0f8d8e000000          jge       near 0041D59Dh
  0041d50f  8b4e6c                mov       ecx,[esi+6Ch]
  0041d512  c7414401000000        mov       dword [ecx+44h],1
  0041d519  8b7e78                mov       edi,[esi+78h]
  0041d51c  66c746100300          mov       word [esi+10h],3
  0041d522  e8199c1200            call      00547140h
  0041d527  99                    cdq
  0041d528  b905000000            mov       ecx,5
  0041d52d  f7f9                  idiv      ecx
  0041d52f  83c203                add       edx,3
  0041d532  66895612              mov       [esi+12h],dx
  0041d536  e8059c1200            call      00547140h
  0041d53b  99                    cdq
  0041d53c  33c2                  xor       eax,edx
  0041d53e  2bc2                  sub       eax,edx
  0041d540  25ff0f0000            and       eax,0FFFh
  0041d545  33c2                  xor       eax,edx
  0041d547  2bc2                  sub       eax,edx
  0041d549  66894616              mov       [esi+16h],ax
  0041d54d  e8ee9b1200            call      00547140h
  0041d552  99                    cdq
  0041d553  b91e000000            mov       ecx,1Eh
  0041d558  895e48                mov       [esi+48h],ebx
  0041d55b  f7f9                  idiv      ecx
  0041d55d  83c228                add       edx,28h
  0041d560  66895614              mov       [esi+14h],dx
  0041d564  e8d79b1200            call      00547140h
  0041d569  99                    cdq
  0041d56a  b914000000            mov       ecx,14h
  0041d56f  f7f9                  idiv      ecx
  0041d571  83c21e                add       edx,1Eh
  0041d574  89565c                mov       [esi+5Ch],edx
  0041d577  8b571c                mov       edx,[edi+1Ch]
  0041d57a  895650                mov       [esi+50h],edx
  0041d57d  8b4724                mov       eax,[edi+24h]
  0041d580  894658                mov       [esi+58h],eax
  0041d583  8b4f20                mov       ecx,[edi+20h]
  0041d586  81c100e00100          add       ecx,1E000h
  0041d58c  b064                  mov       al,64h
  0041d58e  894e20                mov       [esi+20h],ecx
  0041d591  884638                mov       [esi+38h],al
  0041d594  c646393c              mov       byte [esi+39h],3Ch
  0041d598  88463a                mov       [esi+3Ah],al
  0041d59b  eb0d                  jmp       short 0041D5AAh

LAB_0041d59d:
  0041d59d  66837e12f1            cmp       word [esi+12h],0FFF1h
  0041d5a2  7d06                  jge       short 0041D5AAh
  0041d5a4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041d5aa:
  0041d5aa  6afd                  push      0FFFFFFFDh
  0041d5ac  6afc                  push      0FFFFFFFCh
  0041d5ae  6afd                  push      0FFFFFFFDh
  0041d5b0  56                    push      esi
  0041d5b1  e82a78feff            call      00404DE0h
  0041d5b6  83c410                add       esp,10h

LAB_0041d5b9:
  0041d5b9  66837e1003            cmp       word [esi+10h],3
  0041d5be  7573                  jne       short 0041D633h
  0041d5c0  8b7e78                mov       edi,[esi+78h]
  0041d5c3  0fbf4e16              movsx     ecx,word [esi+16h]
  0041d5c7  8b571c                mov       edx,[edi+1Ch]
  0041d5ca  51                    push      ecx
  0041d5cb  895650                mov       [esi+50h],edx
  0041d5ce  8b4724                mov       eax,[edi+24h]
  0041d5d1  894658                mov       [esi+58h],eax
  0041d5d4  e843841200            call      00545A1Ch
  0041d5d9  0faf4648              imul      eax,[esi+48h]
  0041d5dd  0fbf5616              movsx     edx,word [esi+16h]
  0041d5e1  8b4e50                mov       ecx,[esi+50h]
  0041d5e4  8b6e48                mov       ebp,[esi+48h]
  0041d5e7  03c1                  add       eax,ecx
  0041d5e9  52                    push      edx
  0041d5ea  89461c                mov       [esi+1Ch],eax
  0041d5ed  e824841200            call      00545A16h
  0041d5f2  0fafc5                imul      eax,ebp
  0041d5f5  8b4e58                mov       ecx,[esi+58h]
  0041d5f8  668146169600          add       word [esi+16h],96h
  0041d5fe  03c1                  add       eax,ecx
  0041d600  8b4e20                mov       ecx,[esi+20h]
  0041d603  894624                mov       [esi+24h],eax
  0041d606  0fbf4612              movsx     eax,word [esi+12h]
  0041d60a  c1e00c                shl       eax,0Ch
  0041d60d  03c8                  add       ecx,eax
  0041d60f  894e20                mov       [esi+20h],ecx
  0041d612  0fbf4e14              movsx     ecx,word [esi+14h]
  0041d616  2bcd                  sub       ecx,ebp
  0041d618  c1f903                sar       ecx,3
  0041d61b  03cd                  add       ecx,ebp
  0041d61d  894e48                mov       [esi+48h],ecx
  0041d620  8b4e5c                mov       ecx,[esi+5Ch]
  0041d623  49                    dec       ecx
  0041d624  8bc1                  mov       eax,ecx
  0041d626  894e5c                mov       [esi+5Ch],ecx
  0041d629  3bc3                  cmp       eax,ebx
  0041d62b  7f06                  jg        short 0041D633h
  0041d62d  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041d633:
  0041d633  8a4e40                mov       cl,[esi+40h]
  0041d636  fec1                  inc       cl
  0041d638  8ac1                  mov       al,cl
  0041d63a  884e40                mov       [esi+40h],cl
  0041d63d  3c03                  cmp       al,3
  0041d63f  7503                  jne       short 0041D644h
  0041d641  885e40                mov       [esi+40h],bl

LAB_0041d644:
  0041d644  56                    push      esi
  0041d645  e8f6621100            call      00533940h
  0041d64a  8b8690000000          mov       eax,[esi+90h]
  0041d650  83c404                add       esp,4
  0041d653  f6c488                test      ah,88h
  0041d656  7406                  je        short 0041D65Eh
  0041d658  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041d65e:
  0041d65e  8b4644                mov       eax,[esi+44h]
  0041d661  bd01000000            mov       ebp,1
  0041d666  3bc5                  cmp       eax,ebp
  0041d668  0f85e2010000          jne       near 0041D850h
  0041d66e  66395e10              cmp       [esi+10h],bx
  0041d672  0f85aa000000          jne       near 0041D722h
  0041d678  c7463c40005500        mov       dword [esi+3Ch],550040h
  0041d67f  c6464003              mov       byte [esi+40h],3
  0041d683  c6460a09              mov       byte [esi+0Ah],9
  0041d687  885e0b                mov       [esi+0Bh],bl
  0041d68a  66896e10              mov       [esi+10h],bp
  0041d68e  8b571c                mov       edx,[edi+1Ch]
  0041d691  89561c                mov       [esi+1Ch],edx
  0041d694  8b4720                mov       eax,[edi+20h]
  0041d697  894620                mov       [esi+20h],eax
  0041d69a  8b4f24                mov       ecx,[edi+24h]
  0041d69d  894e24                mov       [esi+24h],ecx
  0041d6a0  e89b9a1200            call      00547140h
  0041d6a5  99                    cdq
  0041d6a6  b9a0000000            mov       ecx,0A0h
  0041d6ab  f7f9                  idiv      ecx
  0041d6ad  8b4f1c                mov       ecx,[edi+1Ch]
  0041d6b0  83ea50                sub       edx,50h
  0041d6b3  c1e20c                shl       edx,0Ch
  0041d6b6  03d1                  add       edx,ecx
  0041d6b8  895650                mov       [esi+50h],edx
  0041d6bb  e8809a1200            call      00547140h
  0041d6c0  99                    cdq
  0041d6c1  b9a0000000            mov       ecx,0A0h
  0041d6c6  f7f9                  idiv      ecx
  0041d6c8  8b4724                mov       eax,[edi+24h]
  0041d6cb  83ea50                sub       edx,50h
  0041d6ce  c1e20c                shl       edx,0Ch
  0041d6d1  03d0                  add       edx,eax
  0041d6d3  895658                mov       [esi+58h],edx
  0041d6d6  e8659a1200            call      00547140h
  0041d6db  99                    cdq
  0041d6dc  b9c8000000            mov       ecx,0C8h
  0041d6e1  f7f9                  idiv      ecx
  0041d6e3  8b4f20                mov       ecx,[edi+20h]
  0041d6e6  66c746120a00          mov       word [esi+12h],0Ah
  0041d6ec  81c2b4000000          add       edx,0B4h
  0041d6f2  c1e20c                shl       edx,0Ch
  0041d6f5  03d1                  add       edx,ecx
  0041d6f7  895654                mov       [esi+54h],edx
  0041d6fa  e8419a1200            call      00547140h
  0041d6ff  99                    cdq
  0041d700  b9e8030000            mov       ecx,3E8h
  0041d705  66895e28              mov       [esi+28h],bx
  0041d709  f7f9                  idiv      ecx
  0041d70b  66895e2a              mov       [esi+2Ah],bx
  0041d70f  885e38                mov       [esi+38h],bl
  0041d712  885e39                mov       [esi+39h],bl
  0041d715  885e3a                mov       [esi+3Ah],bl
  0041d718  81c2d0070000          add       edx,7D0h
  0041d71e  66895616              mov       [esi+16h],dx

LAB_0041d722:
  0041d722  66396e10              cmp       [esi+10h],bp
  0041d726  757f                  jne       short 0041D7A7h
  0041d728  8b461c                mov       eax,[esi+1Ch]
  0041d72b  8b5650                mov       edx,[esi+50h]
  0041d72e  8b4e54                mov       ecx,[esi+54h]
  0041d731  2bd0                  sub       edx,eax
  0041d733  c1fa03                sar       edx,3
  0041d736  03d0                  add       edx,eax
  0041d738  8b4620                mov       eax,[esi+20h]
  0041d73b  2bc8                  sub       ecx,eax
  0041d73d  89561c                mov       [esi+1Ch],edx
  0041d740  8b5658                mov       edx,[esi+58h]
  0041d743  c1f903                sar       ecx,3
  0041d746  03c8                  add       ecx,eax
  0041d748  8b4624                mov       eax,[esi+24h]
  0041d74b  2bd0                  sub       edx,eax
  0041d74d  894e20                mov       [esi+20h],ecx
  0041d750  668b4e28              mov       cx,[esi+28h]
  0041d754  c1fa03                sar       edx,3
  0041d757  03d0                  add       edx,eax
  0041d759  0fbf4616              movsx     eax,word [esi+16h]
  0041d75d  895624                mov       [esi+24h],edx
  0041d760  8be8                  mov       ebp,eax
  0041d762  0fbfd1                movsx     edx,cx
  0041d765  2bea                  sub       ebp,edx
  0041d767  c1fd03                sar       ebp,3
  0041d76a  03e9                  add       ebp,ecx
  0041d76c  668b4e2a              mov       cx,[esi+2Ah]
  0041d770  0fbfd1                movsx     edx,cx
  0041d773  2bc2                  sub       eax,edx
  0041d775  66896e28              mov       [esi+28h],bp
  0041d779  c1f803                sar       eax,3
  0041d77c  03c1                  add       eax,ecx
  0041d77e  66ff4e12              dec       word [esi+12h]
  0041d782  66395e12              cmp       [esi+12h],bx
  0041d786  6689462a              mov       [esi+2Ah],ax
  0041d78a  7f0c                  jg        short 0041D798h
  0041d78c  66c746100200          mov       word [esi+10h],2
  0041d792  66c746121400          mov       word [esi+12h],14h

LAB_0041d798:
  0041d798  6a0a                  push      0Ah
  0041d79a  6a0a                  push      0Ah
  0041d79c  6a0a                  push      0Ah
  0041d79e  56                    push      esi
  0041d79f  e83c76feff            call      00404DE0h
  0041d7a4  83c410                add       esp,10h

LAB_0041d7a7:
  0041d7a7  66837e1002            cmp       word [esi+10h],2
  0041d7ac  0f859e000000          jne       near 0041D850h
  0041d7b2  8b466c                mov       eax,[esi+6Ch]
  0041d7b5  8b481c                mov       ecx,[eax+1Ch]
  0041d7b8  894e50                mov       [esi+50h],ecx
  0041d7bb  8b5020                mov       edx,[eax+20h]
  0041d7be  81c200c00300          add       edx,3C000h
  0041d7c4  895654                mov       [esi+54h],edx
  0041d7c7  8b4024                mov       eax,[eax+24h]
  0041d7ca  894658                mov       [esi+58h],eax
  0041d7cd  8b461c                mov       eax,[esi+1Ch]
  0041d7d0  2bc8                  sub       ecx,eax
  0041d7d2  8b6e58                mov       ebp,[esi+58h]
  0041d7d5  c1f903                sar       ecx,3
  0041d7d8  03c8                  add       ecx,eax
  0041d7da  8b4620                mov       eax,[esi+20h]
  0041d7dd  2bd0                  sub       edx,eax
  0041d7df  894e1c                mov       [esi+1Ch],ecx
  0041d7e2  c1fa03                sar       edx,3
  0041d7e5  03c2                  add       eax,edx
  0041d7e7  8b5624                mov       edx,[esi+24h]
  0041d7ea  2bea                  sub       ebp,edx
  0041d7ec  894620                mov       [esi+20h],eax
  0041d7ef  c1fd03                sar       ebp,3
  0041d7f2  03d5                  add       edx,ebp
  0041d7f4  66ff4e12              dec       word [esi+12h]
  0041d7f8  66395e12              cmp       [esi+12h],bx
  0041d7fc  895624                mov       [esi+24h],edx
  0041d7ff  7f40                  jg        short 0041D841h
  0041d801  8b1e                  mov       ebx,[esi]
  0041d803  81e3ffffff7f          and       ebx,7FFFFFFFh
  0041d809  891e                  mov       [esi],ebx
  0041d80b  8b6f1c                mov       ebp,[edi+1Ch]
  0041d80e  8b5f24                mov       ebx,[edi+24h]
  0041d811  2bcd                  sub       ecx,ebp
  0041d813  8b6f20                mov       ebp,[edi+20h]
  0041d816  2bd3                  sub       edx,ebx
  0041d818  2bc5                  sub       eax,ebp
  0041d81a  c1f90c                sar       ecx,0Ch
  0041d81d  0500c00300            add       eax,3C000h
  0041d822  8bf9                  mov       edi,ecx
  0041d824  c1f80c                sar       eax,0Ch
  0041d827  0faff9                imul      edi,ecx
  0041d82a  8bc8                  mov       ecx,eax
  0041d82c  c1fa0c                sar       edx,0Ch
  0041d82f  0fafc8                imul      ecx,eax
  0041d832  8bc2                  mov       eax,edx
  0041d834  03f9                  add       edi,ecx
  0041d836  0fafc2                imul      eax,edx
  0041d839  03f8                  add       edi,eax
  0041d83b  57                    push      edi
  0041d83c  e8cf811200            call      00545A10h

LAB_0041d841:
  0041d841  6afa                  push      0FFFFFFFAh
  0041d843  6af6                  push      0FFFFFFF6h
  0041d845  6afa                  push      0FFFFFFFAh
  0041d847  56                    push      esi
  0041d848  e89375feff            call      00404DE0h
  0041d84d  83c410                add       esp,10h

LAB_0041d850:
  0041d850  5f                    pop       edi
  0041d851  5e                    pop       esi
  0041d852  5d                    pop       ebp
  0041d853  5b                    pop       ebx
  0041d854  c3                    ret

14 cible(s) de saut interne : 0x0041d3e7, 0x0041d45e, 0x0041d46d, 0x0041d59d, 0x0041d5aa, 0x0041d5b9, 0x0041d633, 0x0041d644, 0x0041d65e, 0x0041d722, 0x0041d798, 0x0041d7a7, 0x0041d841, 0x0041d850
