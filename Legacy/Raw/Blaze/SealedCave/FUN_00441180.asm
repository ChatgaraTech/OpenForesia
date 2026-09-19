Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00441180  section .text  offset fichier 0x40580  336 octets

  00441180  53                    push      ebx
  00441181  56                    push      esi
  00441182  8b74240c              mov       esi,[esp+0Ch]
  00441186  33db                  xor       ebx,ebx
  00441188  57                    push      edi
  00441189  66395e10              cmp       [esi+10h],bx
  0044118d  0f8589000000          jne       near 0044121Ch
  00441193  c7463cf0145500        mov       dword [esi+3Ch],5514F0h
  0044119a  885e40                mov       [esi+40h],bl
  0044119d  c6460a01              mov       byte [esi+0Ah],1
  004411a1  885e0b                mov       [esi+0Bh],bl
  004411a4  c6463864              mov       byte [esi+38h],64h
  004411a8  c6463982              mov       byte [esi+39h],82h
  004411ac  c6463a32              mov       byte [esi+3Ah],32h
  004411b0  e88b5f1000            call      00547140h
  004411b5  99                    cdq
  004411b6  33c2                  xor       eax,edx
  004411b8  2bc2                  sub       eax,edx
  004411ba  25ff0f0000            and       eax,0FFFh
  004411bf  33c2                  xor       eax,edx
  004411c1  2bc2                  sub       eax,edx
  004411c3  66894612              mov       [esi+12h],ax
  004411c7  e8745f1000            call      00547140h
  004411cc  99                    cdq
  004411cd  33c2                  xor       eax,edx
  004411cf  66899e9a000000        mov       [esi+9Ah],bx
  004411d6  2bc2                  sub       eax,edx
  004411d8  25ff030000            and       eax,3FFh
  004411dd  33c2                  xor       eax,edx
  004411df  2bc2                  sub       eax,edx
  004411e1  66894614              mov       [esi+14h],ax
  004411e5  e8565f1000            call      00547140h
  004411ea  99                    cdq
  004411eb  b914000000            mov       ecx,14h
  004411f0  66895e16              mov       [esi+16h],bx
  004411f4  f7f9                  idiv      ecx
  004411f6  8b4620                mov       eax,[esi+20h]
  004411f9  66c746100100          mov       word [esi+10h],1
  004411ff  83c20a                add       edx,0Ah
  00441202  0500600e00            add       eax,0E6000h
  00441207  89565c                mov       [esi+5Ch],edx
  0044120a  8b561c                mov       edx,[esi+1Ch]
  0044120d  894620                mov       [esi+20h],eax
  00441210  894654                mov       [esi+54h],eax
  00441213  8b4624                mov       eax,[esi+24h]
  00441216  895650                mov       [esi+50h],edx
  00441219  894658                mov       [esi+58h],eax

LAB_0044121c:
  0044121c  66837e1001            cmp       word [esi+10h],1
  00441221  0f8599000000          jne       near 004412C0h
  00441227  0fbf4e14              movsx     ecx,word [esi+14h]
  0044122b  66ff4616              inc       word [esi+16h]
  0044122f  51                    push      ecx
  00441230  e8e1471000            call      00545A16h
  00441235  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0044123c  8bf8                  mov       edi,eax
  0044123e  0fbf4612              movsx     eax,word [esi+12h]
  00441242  0faffa                imul      edi,edx
  00441245  50                    push      eax
  00441246  c1ff0c                sar       edi,0Ch
  00441249  e8ce471000            call      00545A1Ch
  0044124e  0faff8                imul      edi,eax
  00441251  0fbf4e14              movsx     ecx,word [esi+14h]
  00441255  8b5e50                mov       ebx,[esi+50h]
  00441258  51                    push      ecx
  00441259  03fb                  add       edi,ebx
  0044125b  897e1c                mov       [esi+1Ch],edi
  0044125e  e8b3471000            call      00545A16h
  00441263  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0044126a  8bf8                  mov       edi,eax
  0044126c  0fbf4612              movsx     eax,word [esi+12h]
  00441270  0faffa                imul      edi,edx
  00441273  50                    push      eax
  00441274  c1ff0c                sar       edi,0Ch
  00441277  e89a471000            call      00545A16h
  0044127c  0faff8                imul      edi,eax
  0044127f  8b4658                mov       eax,[esi+58h]
  00441282  0fbf4e14              movsx     ecx,word [esi+14h]
  00441286  03f8                  add       edi,eax
  00441288  51                    push      ecx
  00441289  897e24                mov       [esi+24h],edi
  0044128c  668bbe9a000000        mov       di,[esi+9Ah]
  00441293  e884471000            call      00545A1Ch
  00441298  0fbfd7                movsx     edx,di
  0044129b  0fafc2                imul      eax,edx
  0044129e  034654                add       eax,[esi+54h]
  004412a1  894620                mov       [esi+20h],eax
  004412a4  668b465c              mov       ax,[esi+5Ch]
  004412a8  6603c7                add       ax,di
  004412ab  66817e16c800          cmp       word [esi+16h],0C8h
  004412b1  6689869a000000        mov       [esi+9Ah],ax
  004412b8  7506                  jne       short 004412C0h
  004412ba  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004412c0:
  004412c0  5f                    pop       edi
  004412c1  5e                    pop       esi
  004412c2  5b                    pop       ebx
  004412c3  c3                    ret
  004412c4  90                    nop
  004412c5  90                    nop
  004412c6  90                    nop
  004412c7  90                    nop
  004412c8  90                    nop
  004412c9  90                    nop
  004412ca  90                    nop
  004412cb  90                    nop
  004412cc  90                    nop
  004412cd  90                    nop
  004412ce  90                    nop
  004412cf  90                    nop

2 cible(s) de saut interne : 0x0044121c, 0x004412c0
