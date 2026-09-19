Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004328e0  section .text  offset fichier 0x31ce0  240 octets

  004328e0  53                    push      ebx
  004328e1  56                    push      esi
  004328e2  8b74240c              mov       esi,[esp+0Ch]
  004328e6  33db                  xor       ebx,ebx
  004328e8  66395e10              cmp       [esi+10h],bx
  004328ec  0f85a8000000          jne       near 0043299Ah
  004328f2  66c746100100          mov       word [esi+10h],1
  004328f8  c7463c900c5500        mov       dword [esi+3Ch],550C90h
  004328ff  c6460a09              mov       byte [esi+0Ah],9
  00432903  885e0b                mov       [esi+0Bh],bl
  00432906  e835481100            call      00547140h
  0043290b  99                    cdq
  0043290c  b928000000            mov       ecx,28h
  00432911  f7f9                  idiv      ecx
  00432913  8b461c                mov       eax,[esi+1Ch]
  00432916  83ea14                sub       edx,14h
  00432919  c1e20c                shl       edx,0Ch
  0043291c  03c2                  add       eax,edx
  0043291e  89461c                mov       [esi+1Ch],eax
  00432921  e81a481100            call      00547140h
  00432926  99                    cdq
  00432927  b928000000            mov       ecx,28h
  0043292c  f7f9                  idiv      ecx
  0043292e  8b4e24                mov       ecx,[esi+24h]
  00432931  83ea14                sub       edx,14h
  00432934  c1e20c                shl       edx,0Ch
  00432937  03ca                  add       ecx,edx
  00432939  894e24                mov       [esi+24h],ecx
  0043293c  e8ff471100            call      00547140h
  00432941  99                    cdq
  00432942  b928000000            mov       ecx,28h
  00432947  f7f9                  idiv      ecx
  00432949  8b4620                mov       eax,[esi+20h]
  0043294c  83ea14                sub       edx,14h
  0043294f  c1e20c                shl       edx,0Ch
  00432952  03c2                  add       eax,edx
  00432954  894620                mov       [esi+20h],eax
  00432957  e8e4471100            call      00547140h
  0043295c  99                    cdq
  0043295d  33c2                  xor       eax,edx
  0043295f  2bc2                  sub       eax,edx
  00432961  25ff070000            and       eax,7FFh
  00432966  33c2                  xor       eax,edx
  00432968  2bc2                  sub       eax,edx
  0043296a  ba00140000            mov       edx,1400h
  0043296f  2bd0                  sub       edx,eax
  00432971  66895628              mov       [esi+28h],dx
  00432975  e8c6471100            call      00547140h
  0043297a  99                    cdq
  0043297b  33c2                  xor       eax,edx
  0043297d  b900140000            mov       ecx,1400h
  00432982  2bc2                  sub       eax,edx
  00432984  66895e12              mov       [esi+12h],bx
  00432988  25ff070000            and       eax,7FFh
  0043298d  885e40                mov       [esi+40h],bl
  00432990  33c2                  xor       eax,edx
  00432992  2bc2                  sub       eax,edx
  00432994  2bc8                  sub       ecx,eax
  00432996  66894e2a              mov       [esi+2Ah],cx

LAB_0043299a:
  0043299a  66837e1001            cmp       word [esi+10h],1
  0043299f  7523                  jne       short 004329C4h
  004329a1  66ff4612              inc       word [esi+12h]
  004329a5  66837e1202            cmp       word [esi+12h],2
  004329aa  7518                  jne       short 004329C4h
  004329ac  8a5640                mov       dl,[esi+40h]
  004329af  66895e12              mov       [esi+12h],bx
  004329b3  fec2                  inc       dl
  004329b5  8ac2                  mov       al,dl
  004329b7  885640                mov       [esi+40h],dl
  004329ba  3c04                  cmp       al,4
  004329bc  7506                  jne       short 004329C4h
  004329be  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004329c4:
  004329c4  5e                    pop       esi
  004329c5  5b                    pop       ebx
  004329c6  c3                    ret
  004329c7  90                    nop
  004329c8  90                    nop
  004329c9  90                    nop
  004329ca  90                    nop
  004329cb  90                    nop
  004329cc  90                    nop
  004329cd  90                    nop
  004329ce  90                    nop
  004329cf  90                    nop

2 cible(s) de saut interne : 0x0043299a, 0x004329c4
