
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041f760  section .text  offset fichier 0x1eb60  1344 octets

  0041f760  53                    push      ebx
  0041f761  55                    push      ebp
  0041f762  56                    push      esi
  0041f763  8b742410              mov       esi,[esp+10h]
  0041f767  33db                  xor       ebx,ebx
  0041f769  57                    push      edi
  0041f76a  8b4644                mov       eax,[esi+44h]
  0041f76d  8b7e78                mov       edi,[esi+78h]
  0041f770  3bc3                  cmp       eax,ebx
  0041f772  0f8530030000          jne       near 0041FAA8h
  0041f778  66395e10              cmp       [esi+10h],bx
  0041f77c  0f85af000000          jne       near 0041F831h
  0041f782  c7463c48025500        mov       dword [esi+3Ch],550248h
  0041f789  e8b2791200            call      00547140h
  0041f78e  99                    cdq
  0041f78f  b903000000            mov       ecx,3
  0041f794  c6460a0d              mov       byte [esi+0Ah],0Dh
  0041f798  f7f9                  idiv      ecx
  0041f79a  c6460b01              mov       byte [esi+0Bh],1
  0041f79e  66c746100100          mov       word [esi+10h],1
  0041f7a4  885640                mov       [esi+40h],dl
  0041f7a7  e894791200            call      00547140h
  0041f7ac  99                    cdq
  0041f7ad  b92c010000            mov       ecx,12Ch
  0041f7b2  f7f9                  idiv      ecx
  0041f7b4  8b461c                mov       eax,[esi+1Ch]
  0041f7b7  81ea96000000          sub       edx,96h
  0041f7bd  c1e20c                shl       edx,0Ch
  0041f7c0  03d0                  add       edx,eax
  0041f7c2  895650                mov       [esi+50h],edx
  0041f7c5  e876791200            call      00547140h
  0041f7ca  99                    cdq
  0041f7cb  b92c010000            mov       ecx,12Ch
  0041f7d0  f7f9                  idiv      ecx
  0041f7d2  8b4624                mov       eax,[esi+24h]
  0041f7d5  81ea96000000          sub       edx,96h
  0041f7db  c1e20c                shl       edx,0Ch
  0041f7de  03d0                  add       edx,eax
  0041f7e0  895658                mov       [esi+58h],edx
  0041f7e3  e858791200            call      00547140h
  0041f7e8  99                    cdq
  0041f7e9  b92c010000            mov       ecx,12Ch
  0041f7ee  66c746120a00          mov       word [esi+12h],0Ah
  0041f7f4  f7f9                  idiv      ecx
  0041f7f6  8b4620                mov       eax,[esi+20h]
  0041f7f9  83c250                add       edx,50h
  0041f7fc  c1e20c                shl       edx,0Ch
  0041f7ff  03d0                  add       edx,eax
  0041f801  895654                mov       [esi+54h],edx
  0041f804  e837791200            call      00547140h
  0041f809  99                    cdq
  0041f80a  33c2                  xor       eax,edx
  0041f80c  66895e28              mov       [esi+28h],bx
  0041f810  2bc2                  sub       eax,edx
  0041f812  66895e2a              mov       [esi+2Ah],bx
  0041f816  25ff070000            and       eax,7FFh
  0041f81b  885e38                mov       [esi+38h],bl
  0041f81e  33c2                  xor       eax,edx
  0041f820  885e39                mov       [esi+39h],bl
  0041f823  2bc2                  sub       eax,edx
  0041f825  885e3a                mov       [esi+3Ah],bl
  0041f828  0500100000            add       eax,1000h
  0041f82d  66894616              mov       [esi+16h],ax

LAB_0041f831:
  0041f831  66837e1001            cmp       word [esi+10h],1
  0041f836  757f                  jne       short 0041F8B7h
  0041f838  8b461c                mov       eax,[esi+1Ch]
  0041f83b  8b5650                mov       edx,[esi+50h]
  0041f83e  8b4e54                mov       ecx,[esi+54h]
  0041f841  2bd0                  sub       edx,eax
  0041f843  c1fa03                sar       edx,3
  0041f846  03d0                  add       edx,eax
  0041f848  8b4620                mov       eax,[esi+20h]
  0041f84b  2bc8                  sub       ecx,eax
  0041f84d  89561c                mov       [esi+1Ch],edx
  0041f850  8b5658                mov       edx,[esi+58h]
  0041f853  c1f903                sar       ecx,3
  0041f856  03c8                  add       ecx,eax
  0041f858  8b4624                mov       eax,[esi+24h]
  0041f85b  2bd0                  sub       edx,eax
  0041f85d  894e20                mov       [esi+20h],ecx
  0041f860  668b4e28              mov       cx,[esi+28h]
  0041f864  c1fa03                sar       edx,3
  0041f867  03d0                  add       edx,eax
  0041f869  0fbf4616              movsx     eax,word [esi+16h]
  0041f86d  895624                mov       [esi+24h],edx
  0041f870  8be8                  mov       ebp,eax
  0041f872  0fbfd1                movsx     edx,cx
  0041f875  2bea                  sub       ebp,edx
  0041f877  c1fd03                sar       ebp,3
  0041f87a  03e9                  add       ebp,ecx
  0041f87c  668b4e2a              mov       cx,[esi+2Ah]
  0041f880  0fbfd1                movsx     edx,cx
  0041f883  2bc2                  sub       eax,edx
  0041f885  66896e28              mov       [esi+28h],bp
  0041f889  c1f803                sar       eax,3
  0041f88c  03c1                  add       eax,ecx
  0041f88e  66ff4e12              dec       word [esi+12h]
  0041f892  66395e12              cmp       [esi+12h],bx
  0041f896  6689462a              mov       [esi+2Ah],ax
  0041f89a  7f0c                  jg        short 0041F8A8h
  0041f89c  66c746100200          mov       word [esi+10h],2
  0041f8a2  66c746121200          mov       word [esi+12h],12h

LAB_0041f8a8:
  0041f8a8  6a0a                  push      0Ah
  0041f8aa  6a0a                  push      0Ah
  0041f8ac  6a0a                  push      0Ah
  0041f8ae  56                    push      esi
  0041f8af  e82c55feff            call      00404DE0h
  0041f8b4  83c410                add       esp,10h

LAB_0041f8b7:
  0041f8b7  66837e1002            cmp       word [esi+10h],2
  0041f8bc  0f8541010000          jne       near 0041FA03h
  0041f8c2  8b471c                mov       eax,[edi+1Ch]
  0041f8c5  894650                mov       [esi+50h],eax
  0041f8c8  8b4f20                mov       ecx,[edi+20h]
  0041f8cb  8b461c                mov       eax,[esi+1Ch]
  0041f8ce  81c100c00300          add       ecx,3C000h
  0041f8d4  894e54                mov       [esi+54h],ecx
  0041f8d7  8b4e50                mov       ecx,[esi+50h]
  0041f8da  8b5724                mov       edx,[edi+24h]
  0041f8dd  2bc8                  sub       ecx,eax
  0041f8df  c1f903                sar       ecx,3
  0041f8e2  895658                mov       [esi+58h],edx
  0041f8e5  8b5654                mov       edx,[esi+54h]
  0041f8e8  03c8                  add       ecx,eax
  0041f8ea  8b4620                mov       eax,[esi+20h]
  0041f8ed  8b6e58                mov       ebp,[esi+58h]
  0041f8f0  2bd0                  sub       edx,eax
  0041f8f2  c1fa03                sar       edx,3
  0041f8f5  03c2                  add       eax,edx
  0041f8f7  8b5624                mov       edx,[esi+24h]
  0041f8fa  2bea                  sub       ebp,edx
  0041f8fc  894e1c                mov       [esi+1Ch],ecx
  0041f8ff  c1fd03                sar       ebp,3
  0041f902  03d5                  add       edx,ebp
  0041f904  66ff4e12              dec       word [esi+12h]
  0041f908  66395e12              cmp       [esi+12h],bx
  0041f90c  894620                mov       [esi+20h],eax
  0041f90f  895624                mov       [esi+24h],edx
  0041f912  0f8fdc000000          jg        near 0041F9F4h
  0041f918  8b6f1c                mov       ebp,[edi+1Ch]
  0041f91b  2bcd                  sub       ecx,ebp
  0041f91d  8b6f20                mov       ebp,[edi+20h]
  0041f920  2bc5                  sub       eax,ebp
  0041f922  8b6f24                mov       ebp,[edi+24h]
  0041f925  c1f90c                sar       ecx,0Ch
  0041f928  0500c00300            add       eax,3C000h
  0041f92d  2bd5                  sub       edx,ebp
  0041f92f  8be9                  mov       ebp,ecx
  0041f931  c1f80c                sar       eax,0Ch
  0041f934  0fafe9                imul      ebp,ecx
  0041f937  8bc8                  mov       ecx,eax
  0041f939  c1fa0c                sar       edx,0Ch
  0041f93c  0fafc8                imul      ecx,eax
  0041f93f  8bc2                  mov       eax,edx
  0041f941  03e9                  add       ebp,ecx
  0041f943  0fafc2                imul      eax,edx
  0041f946  03e8                  add       ebp,eax
  0041f948  55                    push      ebp
  0041f949  e8c2601200            call      00545A10h
  0041f94e  3d8c000000            cmp       eax,8Ch
  0041f953  0f8d8e000000          jge       near 0041F9E7h
  0041f959  8b4e6c                mov       ecx,[esi+6Ch]
  0041f95c  c7414401000000        mov       dword [ecx+44h],1
  0041f963  8b7e78                mov       edi,[esi+78h]
  0041f966  66c746100300          mov       word [esi+10h],3
  0041f96c  e8cf771200            call      00547140h
  0041f971  99                    cdq
  0041f972  b905000000            mov       ecx,5
  0041f977  f7f9                  idiv      ecx
  0041f979  83c203                add       edx,3
  0041f97c  66895612              mov       [esi+12h],dx
  0041f980  e8bb771200            call      00547140h
  0041f985  99                    cdq
  0041f986  33c2                  xor       eax,edx
  0041f988  2bc2                  sub       eax,edx
  0041f98a  25ff0f0000            and       eax,0FFFh
  0041f98f  33c2                  xor       eax,edx
  0041f991  2bc2                  sub       eax,edx
  0041f993  66894616              mov       [esi+16h],ax
  0041f997  e8a4771200            call      00547140h
  0041f99c  99                    cdq
  0041f99d  b91e000000            mov       ecx,1Eh
  0041f9a2  895e48                mov       [esi+48h],ebx
  0041f9a5  f7f9                  idiv      ecx
  0041f9a7  83c228                add       edx,28h
  0041f9aa  66895614              mov       [esi+14h],dx
  0041f9ae  e88d771200            call      00547140h
  0041f9b3  99                    cdq
  0041f9b4  b914000000            mov       ecx,14h
  0041f9b9  f7f9                  idiv      ecx
  0041f9bb  83c21e                add       edx,1Eh
  0041f9be  89565c                mov       [esi+5Ch],edx
  0041f9c1  8b571c                mov       edx,[edi+1Ch]
  0041f9c4  895650                mov       [esi+50h],edx
  0041f9c7  8b4724                mov       eax,[edi+24h]
  0041f9ca  894658                mov       [esi+58h],eax
  0041f9cd  8b4f20                mov       ecx,[edi+20h]
  0041f9d0  81c100e00100          add       ecx,1E000h
  0041f9d6  b064                  mov       al,64h
  0041f9d8  894e20                mov       [esi+20h],ecx
  0041f9db  884638                mov       [esi+38h],al
  0041f9de  c646393c              mov       byte [esi+39h],3Ch
  0041f9e2  88463a                mov       [esi+3Ah],al
  0041f9e5  eb0d                  jmp       short 0041F9F4h

LAB_0041f9e7:
  0041f9e7  66837e12f1            cmp       word [esi+12h],0FFF1h
  0041f9ec  7d06                  jge       short 0041F9F4h
  0041f9ee  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041f9f4:
  0041f9f4  6afd                  push      0FFFFFFFDh
  0041f9f6  6afc                  push      0FFFFFFFCh
  0041f9f8  6afd                  push      0FFFFFFFDh
  0041f9fa  56                    push      esi
  0041f9fb  e8e053feff            call      00404DE0h
  0041fa00  83c410                add       esp,10h

LAB_0041fa03:
  0041fa03  66837e1003            cmp       word [esi+10h],3
  0041fa08  7573                  jne       short 0041FA7Dh
  0041fa0a  8b7e78                mov       edi,[esi+78h]
  0041fa0d  0fbf4e16              movsx     ecx,word [esi+16h]
  0041fa11  8b571c                mov       edx,[edi+1Ch]
  0041fa14  51                    push      ecx
  0041fa15  895650                mov       [esi+50h],edx
  0041fa18  8b4724                mov       eax,[edi+24h]
  0041fa1b  894658                mov       [esi+58h],eax
  0041fa1e  e8f95f1200            call      00545A1Ch
  0041fa23  0faf4648              imul      eax,[esi+48h]
  0041fa27  0fbf5616              movsx     edx,word [esi+16h]
  0041fa2b  8b4e50                mov       ecx,[esi+50h]
  0041fa2e  8b6e48                mov       ebp,[esi+48h]
  0041fa31  03c1                  add       eax,ecx
  0041fa33  52                    push      edx
  0041fa34  89461c                mov       [esi+1Ch],eax
  0041fa37  e8da5f1200            call      00545A16h
  0041fa3c  0fafc5                imul      eax,ebp
  0041fa3f  8b4e58                mov       ecx,[esi+58h]
  0041fa42  668146169600          add       word [esi+16h],96h
  0041fa48  03c1                  add       eax,ecx
  0041fa4a  8b4e20                mov       ecx,[esi+20h]
  0041fa4d  894624                mov       [esi+24h],eax
  0041fa50  0fbf4612              movsx     eax,word [esi+12h]
  0041fa54  c1e00c                shl       eax,0Ch
  0041fa57  03c8                  add       ecx,eax
  0041fa59  894e20                mov       [esi+20h],ecx
  0041fa5c  0fbf4e14              movsx     ecx,word [esi+14h]
  0041fa60  2bcd                  sub       ecx,ebp
  0041fa62  c1f903                sar       ecx,3
  0041fa65  03cd                  add       ecx,ebp
  0041fa67  894e48                mov       [esi+48h],ecx
  0041fa6a  8b4e5c                mov       ecx,[esi+5Ch]
  0041fa6d  49                    dec       ecx
  0041fa6e  8bc1                  mov       eax,ecx
  0041fa70  894e5c                mov       [esi+5Ch],ecx
  0041fa73  3bc3                  cmp       eax,ebx
  0041fa75  7f06                  jg        short 0041FA7Dh
  0041fa77  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041fa7d:
  0041fa7d  8a4e40                mov       cl,[esi+40h]
  0041fa80  fec1                  inc       cl
  0041fa82  8ac1                  mov       al,cl
  0041fa84  884e40                mov       [esi+40h],cl
  0041fa87  3c03                  cmp       al,3
  0041fa89  7503                  jne       short 0041FA8Eh
  0041fa8b  885e40                mov       [esi+40h],bl

LAB_0041fa8e:
  0041fa8e  56                    push      esi
  0041fa8f  e8ac3e1100            call      00533940h
  0041fa94  8b8690000000          mov       eax,[esi+90h]
  0041fa9a  83c404                add       esp,4
  0041fa9d  f6c488                test      ah,88h
  0041faa0  7406                  je        short 0041FAA8h
  0041faa2  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041faa8:
  0041faa8  8b4644                mov       eax,[esi+44h]
  0041faab  bd01000000            mov       ebp,1
  0041fab0  3bc5                  cmp       eax,ebp
  0041fab2  0f85e2010000          jne       near 0041FC9Ah
  0041fab8  66395e10              cmp       [esi+10h],bx
  0041fabc  0f85aa000000          jne       near 0041FB6Ch
  0041fac2  c7463c48025500        mov       dword [esi+3Ch],550248h
  0041fac9  c6464003              mov       byte [esi+40h],3
  0041facd  c6460a09              mov       byte [esi+0Ah],9
  0041fad1  885e0b                mov       [esi+0Bh],bl
  0041fad4  66896e10              mov       [esi+10h],bp
  0041fad8  8b571c                mov       edx,[edi+1Ch]
  0041fadb  89561c                mov       [esi+1Ch],edx
  0041fade  8b4720                mov       eax,[edi+20h]
  0041fae1  894620                mov       [esi+20h],eax
  0041fae4  8b4f24                mov       ecx,[edi+24h]
  0041fae7  894e24                mov       [esi+24h],ecx
  0041faea  e851761200            call      00547140h
  0041faef  99                    cdq
  0041faf0  b9a0000000            mov       ecx,0A0h
  0041faf5  f7f9                  idiv      ecx
  0041faf7  8b4f1c                mov       ecx,[edi+1Ch]
  0041fafa  83ea50                sub       edx,50h
  0041fafd  c1e20c                shl       edx,0Ch
  0041fb00  03d1                  add       edx,ecx
  0041fb02  895650                mov       [esi+50h],edx
  0041fb05  e836761200            call      00547140h
  0041fb0a  99                    cdq
  0041fb0b  b9a0000000            mov       ecx,0A0h
  0041fb10  f7f9                  idiv      ecx
  0041fb12  8b4724                mov       eax,[edi+24h]
  0041fb15  83ea50                sub       edx,50h
  0041fb18  c1e20c                shl       edx,0Ch
  0041fb1b  03d0                  add       edx,eax
  0041fb1d  895658                mov       [esi+58h],edx
  0041fb20  e81b761200            call      00547140h
  0041fb25  99                    cdq
  0041fb26  b9c8000000            mov       ecx,0C8h
  0041fb2b  f7f9                  idiv      ecx
  0041fb2d  8b4f20                mov       ecx,[edi+20h]
  0041fb30  66c746120a00          mov       word [esi+12h],0Ah
  0041fb36  81c2b4000000          add       edx,0B4h
  0041fb3c  c1e20c                shl       edx,0Ch
  0041fb3f  03d1                  add       edx,ecx
  0041fb41  895654                mov       [esi+54h],edx
  0041fb44  e8f7751200            call      00547140h
  0041fb49  99                    cdq
  0041fb4a  b9e8030000            mov       ecx,3E8h
  0041fb4f  66895e28              mov       [esi+28h],bx
  0041fb53  f7f9                  idiv      ecx
  0041fb55  66895e2a              mov       [esi+2Ah],bx
  0041fb59  885e38                mov       [esi+38h],bl
  0041fb5c  885e39                mov       [esi+39h],bl
  0041fb5f  885e3a                mov       [esi+3Ah],bl
  0041fb62  81c2d0070000          add       edx,7D0h
  0041fb68  66895616              mov       [esi+16h],dx

LAB_0041fb6c:
  0041fb6c  66396e10              cmp       [esi+10h],bp
  0041fb70  757f                  jne       short 0041FBF1h
  0041fb72  8b461c                mov       eax,[esi+1Ch]
  0041fb75  8b5650                mov       edx,[esi+50h]
  0041fb78  8b4e54                mov       ecx,[esi+54h]
  0041fb7b  2bd0                  sub       edx,eax
  0041fb7d  c1fa03                sar       edx,3
  0041fb80  03d0                  add       edx,eax
  0041fb82  8b4620                mov       eax,[esi+20h]
  0041fb85  2bc8                  sub       ecx,eax
  0041fb87  89561c                mov       [esi+1Ch],edx
  0041fb8a  8b5658                mov       edx,[esi+58h]
  0041fb8d  c1f903                sar       ecx,3
  0041fb90  03c8                  add       ecx,eax
  0041fb92  8b4624                mov       eax,[esi+24h]
  0041fb95  2bd0                  sub       edx,eax
  0041fb97  894e20                mov       [esi+20h],ecx
  0041fb9a  668b4e28              mov       cx,[esi+28h]
  0041fb9e  c1fa03                sar       edx,3
  0041fba1  03d0                  add       edx,eax
  0041fba3  0fbf4616              movsx     eax,word [esi+16h]
  0041fba7  895624                mov       [esi+24h],edx
  0041fbaa  8be8                  mov       ebp,eax
  0041fbac  0fbfd1                movsx     edx,cx
  0041fbaf  2bea                  sub       ebp,edx
  0041fbb1  c1fd03                sar       ebp,3
  0041fbb4  03e9                  add       ebp,ecx
  0041fbb6  668b4e2a              mov       cx,[esi+2Ah]
  0041fbba  0fbfd1                movsx     edx,cx
  0041fbbd  2bc2                  sub       eax,edx
  0041fbbf  66896e28              mov       [esi+28h],bp
  0041fbc3  c1f803                sar       eax,3
  0041fbc6  03c1                  add       eax,ecx
  0041fbc8  66ff4e12              dec       word [esi+12h]
  0041fbcc  66395e12              cmp       [esi+12h],bx
  0041fbd0  6689462a              mov       [esi+2Ah],ax
  0041fbd4  7f0c                  jg        short 0041FBE2h
  0041fbd6  66c746100200          mov       word [esi+10h],2
  0041fbdc  66c746121400          mov       word [esi+12h],14h

LAB_0041fbe2:
  0041fbe2  6a0a                  push      0Ah
  0041fbe4  6a0a                  push      0Ah
  0041fbe6  6a0a                  push      0Ah
  0041fbe8  56                    push      esi
  0041fbe9  e8f251feff            call      00404DE0h
  0041fbee  83c410                add       esp,10h

LAB_0041fbf1:
  0041fbf1  66837e1002            cmp       word [esi+10h],2
  0041fbf6  0f859e000000          jne       near 0041FC9Ah
  0041fbfc  8b466c                mov       eax,[esi+6Ch]
  0041fbff  8b481c                mov       ecx,[eax+1Ch]
  0041fc02  894e50                mov       [esi+50h],ecx
  0041fc05  8b5020                mov       edx,[eax+20h]
  0041fc08  81c200c00300          add       edx,3C000h
  0041fc0e  895654                mov       [esi+54h],edx
  0041fc11  8b4024                mov       eax,[eax+24h]
  0041fc14  894658                mov       [esi+58h],eax
  0041fc17  8b461c                mov       eax,[esi+1Ch]
  0041fc1a  2bc8                  sub       ecx,eax
  0041fc1c  8b6e58                mov       ebp,[esi+58h]
  0041fc1f  c1f903                sar       ecx,3
  0041fc22  03c8                  add       ecx,eax
  0041fc24  8b4620                mov       eax,[esi+20h]
  0041fc27  2bd0                  sub       edx,eax
  0041fc29  894e1c                mov       [esi+1Ch],ecx
  0041fc2c  c1fa03                sar       edx,3
  0041fc2f  03c2                  add       eax,edx
  0041fc31  8b5624                mov       edx,[esi+24h]
  0041fc34  2bea                  sub       ebp,edx
  0041fc36  894620                mov       [esi+20h],eax
  0041fc39  c1fd03                sar       ebp,3
  0041fc3c  03d5                  add       edx,ebp
  0041fc3e  66ff4e12              dec       word [esi+12h]
  0041fc42  66395e12              cmp       [esi+12h],bx
  0041fc46  895624                mov       [esi+24h],edx
  0041fc49  7f40                  jg        short 0041FC8Bh
  0041fc4b  8b1e                  mov       ebx,[esi]
  0041fc4d  81e3ffffff7f          and       ebx,7FFFFFFFh
  0041fc53  891e                  mov       [esi],ebx
  0041fc55  8b6f1c                mov       ebp,[edi+1Ch]
  0041fc58  8b5f24                mov       ebx,[edi+24h]
  0041fc5b  2bcd                  sub       ecx,ebp
  0041fc5d  8b6f20                mov       ebp,[edi+20h]
  0041fc60  2bd3                  sub       edx,ebx
  0041fc62  2bc5                  sub       eax,ebp
  0041fc64  c1f90c                sar       ecx,0Ch
  0041fc67  0500c00300            add       eax,3C000h
  0041fc6c  8bf9                  mov       edi,ecx
  0041fc6e  c1f80c                sar       eax,0Ch
  0041fc71  0faff9                imul      edi,ecx
  0041fc74  8bc8                  mov       ecx,eax
  0041fc76  c1fa0c                sar       edx,0Ch
  0041fc79  0fafc8                imul      ecx,eax
  0041fc7c  8bc2                  mov       eax,edx
  0041fc7e  03f9                  add       edi,ecx
  0041fc80  0fafc2                imul      eax,edx
  0041fc83  03f8                  add       edi,eax
  0041fc85  57                    push      edi
  0041fc86  e8855d1200            call      00545A10h

LAB_0041fc8b:
  0041fc8b  6afa                  push      0FFFFFFFAh
  0041fc8d  6af6                  push      0FFFFFFF6h
  0041fc8f  6afa                  push      0FFFFFFFAh
  0041fc91  56                    push      esi
  0041fc92  e84951feff            call      00404DE0h
  0041fc97  83c410                add       esp,10h

LAB_0041fc9a:
  0041fc9a  5f                    pop       edi
  0041fc9b  5e                    pop       esi
  0041fc9c  5d                    pop       ebp
  0041fc9d  5b                    pop       ebx
  0041fc9e  c3                    ret

14 cible(s) de saut interne : 0x0041f831, 0x0041f8a8, 0x0041f8b7, 0x0041f9e7, 0x0041f9f4, 0x0041fa03, 0x0041fa7d, 0x0041fa8e, 0x0041faa8, 0x0041fb6c, 0x0041fbe2, 0x0041fbf1, 0x0041fc8b, 0x0041fc9a
