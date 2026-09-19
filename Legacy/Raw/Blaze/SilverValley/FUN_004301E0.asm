Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004301e0  section .text  offset fichier 0x2f5e0  384 octets

  004301e0  55                    push      ebp
  004301e1  56                    push      esi
  004301e2  8b74240c              mov       esi,[esp+0Ch]
  004301e6  57                    push      edi
  004301e7  bf01000000            mov       edi,1
  004301ec  66837e1000            cmp       word [esi+10h],0
  004301f1  0f85fc000000          jne       near 004302F3h
  004301f7  66897e10              mov       [esi+10h],di
  004301fb  c7463cc8095500        mov       dword [esi+3Ch],5509C8h
  00430202  e8396f1100            call      00547140h
  00430207  99                    cdq
  00430208  b958020000            mov       ecx,258h
  0043020d  f7f9                  idiv      ecx
  0043020f  81c2dc050000          add       edx,5DCh
  00430215  66895628              mov       [esi+28h],dx
  00430219  e8226f1100            call      00547140h
  0043021e  99                    cdq
  0043021f  b958020000            mov       ecx,258h
  00430224  f7f9                  idiv      ecx
  00430226  81c2dc050000          add       edx,5DCh
  0043022c  6689562a              mov       [esi+2Ah],dx
  00430230  e80b6f1100            call      00547140h
  00430235  99                    cdq
  00430236  33c2                  xor       eax,edx
  00430238  2bc2                  sub       eax,edx
  0043023a  25ff070000            and       eax,7FFh
  0043023f  33c2                  xor       eax,edx
  00430241  2bc2                  sub       eax,edx
  00430243  2d00040000            sub       eax,400h
  00430248  66014632              add       [esi+32h],ax
  0043024c  0fbf5632              movsx     edx,word [esi+32h]
  00430250  52                    push      edx
  00430251  e8c6571100            call      00545A1Ch
  00430256  8d2c80                lea       ebp,[eax+eax*4]
  00430259  e8e26e1100            call      00547140h
  0043025e  99                    cdq
  0043025f  b914000000            mov       ecx,14h
  00430264  f7f9                  idiv      ecx
  00430266  8b461c                mov       eax,[esi+1Ch]
  00430269  83ea0a                sub       edx,0Ah
  0043026c  c1e20c                shl       edx,0Ch
  0043026f  03ea                  add       ebp,edx
  00430271  0fbf5632              movsx     edx,word [esi+32h]
  00430275  03c5                  add       eax,ebp
  00430277  52                    push      edx
  00430278  89461c                mov       [esi+1Ch],eax
  0043027b  e896571100            call      00545A16h
  00430280  8d2c80                lea       ebp,[eax+eax*4]
  00430283  e8b86e1100            call      00547140h
  00430288  99                    cdq
  00430289  b914000000            mov       ecx,14h
  0043028e  f7f9                  idiv      ecx
  00430290  8b4624                mov       eax,[esi+24h]
  00430293  83ea0a                sub       edx,0Ah
  00430296  c1e20c                shl       edx,0Ch
  00430299  03ea                  add       ebp,edx
  0043029b  03c5                  add       eax,ebp
  0043029d  894624                mov       [esi+24h],eax
  004302a0  e89b6e1100            call      00547140h
  004302a5  99                    cdq
  004302a6  b914000000            mov       ecx,14h
  004302ab  c6460a06              mov       byte [esi+0Ah],6
  004302af  f7f9                  idiv      ecx
  004302b1  8b4e20                mov       ecx,[esi+20h]
  004302b4  c6460b00              mov       byte [esi+0Bh],0
  004302b8  c1e20c                shl       edx,0Ch
  004302bb  2bca                  sub       ecx,edx
  004302bd  8b561c                mov       edx,[esi+1Ch]
  004302c0  8bc1                  mov       eax,ecx
  004302c2  894e20                mov       [esi+20h],ecx
  004302c5  894654                mov       [esi+54h],eax
  004302c8  8b4624                mov       eax,[esi+24h]
  004302cb  895650                mov       [esi+50h],edx
  004302ce  894658                mov       [esi+58h],eax
  004302d1  e86a6e1100            call      00547140h
  004302d6  99                    cdq
  004302d7  b905000000            mov       ecx,5
  004302dc  66c746160000          mov       word [esi+16h],0
  004302e2  f7f9                  idiv      ecx
  004302e4  66894e14              mov       [esi+14h],cx
  004302e8  c6464000              mov       byte [esi+40h],0
  004302ec  83c208                add       edx,8
  004302ef  66895612              mov       [esi+12h],dx

LAB_004302f3:
  004302f3  66397e10              cmp       [esi+10h],di
  004302f7  755c                  jne       short 00430355h
  004302f9  0fbf5632              movsx     edx,word [esi+32h]
  004302fd  52                    push      edx
  004302fe  e819571100            call      00545A1Ch
  00430303  0fbf4e14              movsx     ecx,word [esi+14h]
  00430307  0fafc1                imul      eax,ecx
  0043030a  0fbf5632              movsx     edx,word [esi+32h]
  0043030e  8b4e50                mov       ecx,[esi+50h]
  00430311  668b7e14              mov       di,[esi+14h]
  00430315  03c1                  add       eax,ecx
  00430317  52                    push      edx
  00430318  89461c                mov       [esi+1Ch],eax
  0043031b  e8f6561100            call      00545A16h
  00430320  0fbfcf                movsx     ecx,di
  00430323  0fafc1                imul      eax,ecx
  00430326  8b4e58                mov       ecx,[esi+58h]
  00430329  662b7e12              sub       di,[esi+12h]
  0043032d  03c1                  add       eax,ecx
  0043032f  894624                mov       [esi+24h],eax
  00430332  8b4620                mov       eax,[esi+20h]
  00430335  0500d00000            add       eax,0D000h
  0043033a  66ff4616              inc       word [esi+16h]
  0043033e  894620                mov       [esi+20h],eax
  00430341  8a4616                mov       al,[esi+16h]
  00430344  3c04                  cmp       al,4
  00430346  66897e14              mov       [esi+14h],di
  0043034a  884640                mov       [esi+40h],al
  0043034d  7506                  jne       short 00430355h
  0043034f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00430355:
  00430355  5f                    pop       edi
  00430356  5e                    pop       esi
  00430357  5d                    pop       ebp
  00430358  c3                    ret
  00430359  90                    nop
  0043035a  90                    nop
  0043035b  90                    nop
  0043035c  90                    nop
  0043035d  90                    nop
  0043035e  90                    nop
  0043035f  90                    nop

2 cible(s) de saut interne : 0x004302f3, 0x00430355
