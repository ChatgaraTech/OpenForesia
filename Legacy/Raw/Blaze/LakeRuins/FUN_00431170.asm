Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00431170  section .text  offset fichier 0x30570  368 octets

  00431170  53                    push      ebx
  00431171  56                    push      esi
  00431172  8b74240c              mov       esi,[esp+0Ch]
  00431176  33db                  xor       ebx,ebx
  00431178  66395e10              cmp       [esi+10h],bx
  0043117c  0f85b8000000          jne       near 0043123Ah
  00431182  66c746100100          mov       word [esi+10h],1
  00431188  c7463c200b5500        mov       dword [esi+3Ch],550B20h
  0043118f  c6460a02              mov       byte [esi+0Ah],2
  00431193  885e0b                mov       [esi+0Bh],bl
  00431196  e8a55f1100            call      00547140h
  0043119b  99                    cdq
  0043119c  b92c010000            mov       ecx,12Ch
  004311a1  f7f9                  idiv      ecx
  004311a3  8b4e1c                mov       ecx,[esi+1Ch]
  004311a6  81ea96000000          sub       edx,96h
  004311ac  c1e20c                shl       edx,0Ch
  004311af  03ca                  add       ecx,edx
  004311b1  894e1c                mov       [esi+1Ch],ecx
  004311b4  e8875f1100            call      00547140h
  004311b9  99                    cdq
  004311ba  b92c010000            mov       ecx,12Ch
  004311bf  f7f9                  idiv      ecx
  004311c1  8b4624                mov       eax,[esi+24h]
  004311c4  81ea96000000          sub       edx,96h
  004311ca  c1e20c                shl       edx,0Ch
  004311cd  03c2                  add       eax,edx
  004311cf  894624                mov       [esi+24h],eax
  004311d2  e8695f1100            call      00547140h
  004311d7  99                    cdq
  004311d8  b90a000000            mov       ecx,0Ah
  004311dd  f7f9                  idiv      ecx
  004311df  8b4e20                mov       ecx,[esi+20h]
  004311e2  83ea05                sub       edx,5
  004311e5  c1e20c                shl       edx,0Ch
  004311e8  03ca                  add       ecx,edx
  004311ea  894e20                mov       [esi+20h],ecx
  004311ed  e84e5f1100            call      00547140h
  004311f2  99                    cdq
  004311f3  b9d0070000            mov       ecx,7D0h
  004311f8  f7f9                  idiv      ecx
  004311fa  81c2dc050000          add       edx,5DCh
  00431200  66895628              mov       [esi+28h],dx
  00431204  e8375f1100            call      00547140h
  00431209  99                    cdq
  0043120a  b9d0070000            mov       ecx,7D0h
  0043120f  66895e14              mov       [esi+14h],bx
  00431213  f7f9                  idiv      ecx
  00431215  81c2dc050000          add       edx,5DCh
  0043121b  6689562a              mov       [esi+2Ah],dx
  0043121f  e81c5f1100            call      00547140h
  00431224  99                    cdq
  00431225  b905000000            mov       ecx,5
  0043122a  885e40                mov       [esi+40h],bl
  0043122d  f7f9                  idiv      ecx
  0043122f  8b06                  mov       eax,[esi]
  00431231  80cc40                or        ah,40h
  00431234  8906                  mov       [esi],eax
  00431236  66895612              mov       [esi+12h],dx

LAB_0043123a:
  0043123a  66837e1001            cmp       word [esi+10h],1
  0043123f  751b                  jne       short 0043125Ch
  00431241  66ff4e12              dec       word [esi+12h]
  00431245  66395e12              cmp       [esi+12h],bx
  00431249  7d11                  jge       short 0043125Ch
  0043124b  8b06                  mov       eax,[esi]
  0043124d  66c746100200          mov       word [esi+10h],2
  00431253  80e4bf                and       ah,0BFh
  00431256  66895e12              mov       [esi+12h],bx
  0043125a  8906                  mov       [esi],eax

LAB_0043125c:
  0043125c  66837e1002            cmp       word [esi+10h],2
  00431261  7571                  jne       short 004312D4h
  00431263  e8d85e1100            call      00547140h
  00431268  99                    cdq
  00431269  b906000000            mov       ecx,6
  0043126e  f7f9                  idiv      ecx
  00431270  8b4e1c                mov       ecx,[esi+1Ch]
  00431273  83ea03                sub       edx,3
  00431276  c1e20c                shl       edx,0Ch
  00431279  03ca                  add       ecx,edx
  0043127b  894e1c                mov       [esi+1Ch],ecx
  0043127e  e8bd5e1100            call      00547140h
  00431283  99                    cdq
  00431284  b906000000            mov       ecx,6
  00431289  f7f9                  idiv      ecx
  0043128b  8b4624                mov       eax,[esi+24h]
  0043128e  83ea03                sub       edx,3
  00431291  c1e20c                shl       edx,0Ch
  00431294  03c2                  add       eax,edx
  00431296  894624                mov       [esi+24h],eax
  00431299  e8a25e1100            call      00547140h
  0043129e  99                    cdq
  0043129f  b903000000            mov       ecx,3
  004312a4  f7f9                  idiv      ecx
  004312a6  8b4620                mov       eax,[esi+20h]
  004312a9  c1e20c                shl       edx,0Ch
  004312ac  03c2                  add       eax,edx
  004312ae  66ff4612              inc       word [esi+12h]
  004312b2  66837e1202            cmp       word [esi+12h],2
  004312b7  894620                mov       [esi+20h],eax
  004312ba  7c18                  jl        short 004312D4h
  004312bc  8a4e40                mov       cl,[esi+40h]
  004312bf  66895e12              mov       [esi+12h],bx
  004312c3  fec1                  inc       cl
  004312c5  8ac1                  mov       al,cl
  004312c7  884e40                mov       [esi+40h],cl
  004312ca  3c04                  cmp       al,4
  004312cc  7506                  jne       short 004312D4h
  004312ce  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004312d4:
  004312d4  5e                    pop       esi
  004312d5  5b                    pop       ebx
  004312d6  c3                    ret
  004312d7  90                    nop
  004312d8  90                    nop
  004312d9  90                    nop
  004312da  90                    nop
  004312db  90                    nop
  004312dc  90                    nop
  004312dd  90                    nop
  004312de  90                    nop
  004312df  90                    nop

3 cible(s) de saut interne : 0x0043123a, 0x0043125c, 0x004312d4
