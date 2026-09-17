Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00417000  section .text  offset fichier 0x16400  272 octets

  00417000  53                    push      ebx
  00417001  56                    push      esi
  00417002  8b74240c              mov       esi,[esp+0Ch]
  00417006  33db                  xor       ebx,ebx
  00417008  66395e10              cmp       [esi+10h],bx
  0041700c  0f85bb000000          jne       near 004170CDh
  00417012  c7463c50fa5400        mov       dword [esi+3Ch],54FA50h
  00417019  c60555fa540002        mov       byte [54FA55h],2
  00417020  885e40                mov       [esi+40h],bl
  00417023  c6460a09              mov       byte [esi+0Ah],9
  00417027  885e0b                mov       [esi+0Bh],bl
  0041702a  66895e10              mov       [esi+10h],bx
  0041702e  e80d011300            call      00547140h
  00417033  99                    cdq
  00417034  b950000000            mov       ecx,50h
  00417039  f7f9                  idiv      ecx
  0041703b  8b461c                mov       eax,[esi+1Ch]
  0041703e  83ea28                sub       edx,28h
  00417041  c1e20c                shl       edx,0Ch
  00417044  03c2                  add       eax,edx
  00417046  89461c                mov       [esi+1Ch],eax
  00417049  e8f2001300            call      00547140h
  0041704e  99                    cdq
  0041704f  b950000000            mov       ecx,50h
  00417054  f7f9                  idiv      ecx
  00417056  8b4e24                mov       ecx,[esi+24h]
  00417059  83ea28                sub       edx,28h
  0041705c  c1e20c                shl       edx,0Ch
  0041705f  03ca                  add       ecx,edx
  00417061  894e24                mov       [esi+24h],ecx
  00417064  e8d7001300            call      00547140h
  00417069  99                    cdq
  0041706a  b928000000            mov       ecx,28h
  0041706f  66895e28              mov       [esi+28h],bx
  00417073  f7f9                  idiv      ecx
  00417075  8b4620                mov       eax,[esi+20h]
  00417078  66895e2a              mov       [esi+2Ah],bx
  0041707c  83c250                add       edx,50h
  0041707f  c1e20c                shl       edx,0Ch
  00417082  03c2                  add       eax,edx
  00417084  894620                mov       [esi+20h],eax
  00417087  e8b4001300            call      00547140h
  0041708c  99                    cdq
  0041708d  33c2                  xor       eax,edx
  0041708f  66c746100100          mov       word [esi+10h],1
  00417095  2bc2                  sub       eax,edx
  00417097  83e003                and       eax,3
  0041709a  33c2                  xor       eax,edx
  0041709c  2bc2                  sub       eax,edx
  0041709e  83c002                add       eax,2
  004170a1  8d0480                lea       eax,[eax+eax*4]
  004170a4  8d1480                lea       edx,[eax+eax*4]
  004170a7  c1e202                shl       edx,2
  004170aa  66895612              mov       [esi+12h],dx
  004170ae  e88d001300            call      00547140h
  004170b3  99                    cdq
  004170b4  b905000000            mov       ecx,5
  004170b9  f7f9                  idiv      ecx
  004170bb  b080                  mov       al,80h
  004170bd  884638                mov       [esi+38h],al
  004170c0  884639                mov       [esi+39h],al
  004170c3  88463a                mov       [esi+3Ah],al
  004170c6  83c203                add       edx,3
  004170c9  66895614              mov       [esi+14h],dx

LAB_004170cd:
  004170cd  66837e1001            cmp       word [esi+10h],1
  004170d2  7537                  jne       short 0041710Bh
  004170d4  0fbf5614              movsx     edx,word [esi+14h]
  004170d8  8b4620                mov       eax,[esi+20h]
  004170db  6afa                  push      0FFFFFFFAh
  004170dd  c1e20c                shl       edx,0Ch
  004170e0  03c2                  add       eax,edx
  004170e2  6afa                  push      0FFFFFFFAh
  004170e4  894620                mov       [esi+20h],eax
  004170e7  668b4612              mov       ax,[esi+12h]
  004170eb  66014628              add       [esi+28h],ax
  004170ef  6601462a              add       [esi+2Ah],ax
  004170f3  6afa                  push      0FFFFFFFAh
  004170f5  56                    push      esi
  004170f6  e8e5dcfeff            call      00404DE0h
  004170fb  8a4638                mov       al,[esi+38h]
  004170fe  83c410                add       esp,10h
  00417101  3ac3                  cmp       al,bl
  00417103  7506                  jne       short 0041710Bh
  00417105  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041710b:
  0041710b  5e                    pop       esi
  0041710c  5b                    pop       ebx
  0041710d  c3                    ret
  0041710e  90                    nop
  0041710f  90                    nop

2 cible(s) de saut interne : 0x004170cd, 0x0041710b
