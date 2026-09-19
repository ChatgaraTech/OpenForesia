Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043de50  section .text  offset fichier 0x3d250  272 octets

  0043de50  53                    push      ebx
  0043de51  56                    push      esi
  0043de52  8b74240c              mov       esi,[esp+0Ch]
  0043de56  33db                  xor       ebx,ebx
  0043de58  66395e10              cmp       [esi+10h],bx
  0043de5c  0f85c9000000          jne       near 0043DF2Bh
  0043de62  c7463c20125500        mov       dword [esi+3Ch],551220h
  0043de69  885e40                mov       [esi+40h],bl
  0043de6c  e8cf921000            call      00547140h
  0043de71  99                    cdq
  0043de72  33c2                  xor       eax,edx
  0043de74  2bc2                  sub       eax,edx
  0043de76  25ff070000            and       eax,7FFh
  0043de7b  33c2                  xor       eax,edx
  0043de7d  2bc2                  sub       eax,edx
  0043de7f  05000c0000            add       eax,0C00h
  0043de84  66894628              mov       [esi+28h],ax
  0043de88  6689462a              mov       [esi+2Ah],ax
  0043de8c  803d2ce55d0010        cmp       byte [5DE52Ch],10h
  0043de93  7531                  jne       short 0043DEC6h
  0043de95  c6460a09              mov       byte [esi+0Ah],9
  0043de99  e8a2921000            call      00547140h
  0043de9e  99                    cdq
  0043de9f  b996000000            mov       ecx,96h
  0043dea4  f7f9                  idiv      ecx
  0043dea6  8b4e1c                mov       ecx,[esi+1Ch]
  0043dea9  83ea4b                sub       edx,4Bh
  0043deac  c1e20c                shl       edx,0Ch
  0043deaf  03ca                  add       ecx,edx
  0043deb1  894e1c                mov       [esi+1Ch],ecx
  0043deb4  e887921000            call      00547140h
  0043deb9  99                    cdq
  0043deba  b996000000            mov       ecx,96h
  0043debf  f7f9                  idiv      ecx
  0043dec1  83ea4b                sub       edx,4Bh
  0043dec4  eb35                  jmp       short 0043DEFBh

LAB_0043dec6:
  0043dec6  c6460a06              mov       byte [esi+0Ah],6
  0043deca  e871921000            call      00547140h
  0043decf  99                    cdq
  0043ded0  b92c010000            mov       ecx,12Ch
  0043ded5  f7f9                  idiv      ecx
  0043ded7  8b4e1c                mov       ecx,[esi+1Ch]
  0043deda  81ea96000000          sub       edx,96h
  0043dee0  c1e20c                shl       edx,0Ch
  0043dee3  03ca                  add       ecx,edx
  0043dee5  894e1c                mov       [esi+1Ch],ecx
  0043dee8  e853921000            call      00547140h
  0043deed  99                    cdq
  0043deee  b92c010000            mov       ecx,12Ch
  0043def3  f7f9                  idiv      ecx
  0043def5  81ea96000000          sub       edx,96h

LAB_0043defb:
  0043defb  8b4624                mov       eax,[esi+24h]
  0043defe  c1e20c                shl       edx,0Ch
  0043df01  03c2                  add       eax,edx
  0043df03  894624                mov       [esi+24h],eax
  0043df06  e835921000            call      00547140h
  0043df0b  99                    cdq
  0043df0c  b996000000            mov       ecx,96h
  0043df11  885e0b                mov       [esi+0Bh],bl
  0043df14  f7f9                  idiv      ecx
  0043df16  8b4620                mov       eax,[esi+20h]
  0043df19  66c746100100          mov       word [esi+10h],1
  0043df1f  66895e12              mov       [esi+12h],bx
  0043df23  c1e20c                shl       edx,0Ch
  0043df26  03c2                  add       eax,edx
  0043df28  894620                mov       [esi+20h],eax

LAB_0043df2b:
  0043df2b  66837e1001            cmp       word [esi+10h],1
  0043df30  7523                  jne       short 0043DF55h
  0043df32  66ff4612              inc       word [esi+12h]
  0043df36  66837e1202            cmp       word [esi+12h],2
  0043df3b  7c18                  jl        short 0043DF55h
  0043df3d  8a4e40                mov       cl,[esi+40h]
  0043df40  66895e12              mov       [esi+12h],bx
  0043df44  fec1                  inc       cl
  0043df46  8ac1                  mov       al,cl
  0043df48  884e40                mov       [esi+40h],cl
  0043df4b  3c04                  cmp       al,4
  0043df4d  7206                  jb        short 0043DF55h
  0043df4f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043df55:
  0043df55  5e                    pop       esi
  0043df56  5b                    pop       ebx
  0043df57  c3                    ret
  0043df58  90                    nop
  0043df59  90                    nop
  0043df5a  90                    nop
  0043df5b  90                    nop
  0043df5c  90                    nop
  0043df5d  90                    nop
  0043df5e  90                    nop
  0043df5f  90                    nop

4 cible(s) de saut interne : 0x0043dec6, 0x0043defb, 0x0043df2b, 0x0043df55
