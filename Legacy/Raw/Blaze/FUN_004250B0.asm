
Legacy/Binaries/Blaze.exe  base 0x00400000
0x004250b0  section .text  offset fichier 0x244b0  448 octets

  004250b0  53                    push      ebx
  004250b1  56                    push      esi
  004250b2  8b74240c              mov       esi,[esp+0Ch]
  004250b6  33db                  xor       ebx,ebx
  004250b8  57                    push      edi
  004250b9  bf01000000            mov       edi,1
  004250be  66395e10              cmp       [esi+10h],bx
  004250c2  0f85dd000000          jne       near 004251A5h
  004250c8  66897e10              mov       [esi+10h],di
  004250cc  c7463c88035500        mov       dword [esi+3Ch],550388h
  004250d3  885e40                mov       [esi+40h],bl
  004250d6  c6460a02              mov       byte [esi+0Ah],2
  004250da  885e0b                mov       [esi+0Bh],bl
  004250dd  e85e201200            call      00547140h
  004250e2  99                    cdq
  004250e3  33c2                  xor       eax,edx
  004250e5  66895e14              mov       [esi+14h],bx
  004250e9  2bc2                  sub       eax,edx
  004250eb  25ff0f0000            and       eax,0FFFh
  004250f0  33c2                  xor       eax,edx
  004250f2  2bc2                  sub       eax,edx
  004250f4  66894612              mov       [esi+12h],ax
  004250f8  e843201200            call      00547140h
  004250fd  99                    cdq
  004250fe  33c2                  xor       eax,edx
  00425100  2bc2                  sub       eax,edx
  00425102  25ff030000            and       eax,3FFh
  00425107  33c2                  xor       eax,edx
  00425109  2bc2                  sub       eax,edx
  0042510b  0500080000            add       eax,800h
  00425110  66894628              mov       [esi+28h],ax
  00425114  6689462a              mov       [esi+2Ah],ax
  00425118  e823201200            call      00547140h
  0042511d  99                    cdq
  0042511e  b90a000000            mov       ecx,0Ah
  00425123  f7f9                  idiv      ecx
  00425125  83ea05                sub       edx,5
  00425128  895650                mov       [esi+50h],edx
  0042512b  e810201200            call      00547140h
  00425130  99                    cdq
  00425131  b90a000000            mov       ecx,0Ah
  00425136  f7f9                  idiv      ecx
  00425138  83ea05                sub       edx,5
  0042513b  895658                mov       [esi+58h],edx
  0042513e  e8fd1f1200            call      00547140h
  00425143  99                    cdq
  00425144  b903000000            mov       ecx,3
  00425149  f7f9                  idiv      ecx
  0042514b  83c202                add       edx,2
  0042514e  895654                mov       [esi+54h],edx
  00425151  e8ea1f1200            call      00547140h
  00425156  99                    cdq
  00425157  b903000000            mov       ecx,3
  0042515c  66899e9a000000        mov       [esi+9Ah],bx
  00425163  f7f9                  idiv      ecx
  00425165  83c202                add       edx,2
  00425168  66895616              mov       [esi+16h],dx
  0042516c  e8cf1f1200            call      00547140h
  00425171  99                    cdq
  00425172  b932000000            mov       ecx,32h
  00425177  f7f9                  idiv      ecx
  00425179  80c280                add       dl,80h
  0042517c  885638                mov       [esi+38h],dl
  0042517f  e8bc1f1200            call      00547140h
  00425184  99                    cdq
  00425185  b932000000            mov       ecx,32h
  0042518a  f7f9                  idiv      ecx
  0042518c  80c280                add       dl,80h
  0042518f  885639                mov       [esi+39h],dl
  00425192  e8a91f1200            call      00547140h
  00425197  99                    cdq
  00425198  b932000000            mov       ecx,32h
  0042519d  f7f9                  idiv      ecx
  0042519f  80c280                add       dl,80h
  004251a2  88563a                mov       [esi+3Ah],dl

LAB_004251a5:
  004251a5  66397e10              cmp       [esi+10h],di
  004251a9  0f85b7000000          jne       near 00425266h
  004251af  8b4658                mov       eax,[esi+58h]
  004251b2  8b4e24                mov       ecx,[esi+24h]
  004251b5  8b5650                mov       edx,[esi+50h]
  004251b8  8b7e1c                mov       edi,[esi+1Ch]
  004251bb  c1e00c                shl       eax,0Ch
  004251be  03c8                  add       ecx,eax
  004251c0  8b4654                mov       eax,[esi+54h]
  004251c3  c1e20c                shl       edx,0Ch
  004251c6  894e24                mov       [esi+24h],ecx
  004251c9  8bc8                  mov       ecx,eax
  004251cb  03fa                  add       edi,edx
  004251cd  8b5620                mov       edx,[esi+20h]
  004251d0  c1e10c                shl       ecx,0Ch
  004251d3  03d1                  add       edx,ecx
  004251d5  40                    inc       eax
  004251d6  895620                mov       [esi+20h],edx
  004251d9  897e1c                mov       [esi+1Ch],edi
  004251dc  0fbf5612              movsx     edx,word [esi+12h]
  004251e0  52                    push      edx
  004251e1  894654                mov       [esi+54h],eax
  004251e4  e833081200            call      00545A1Ch
  004251e9  0fbf4e14              movsx     ecx,word [esi+14h]
  004251ed  0fafc1                imul      eax,ecx
  004251f0  0fbf5612              movsx     edx,word [esi+12h]
  004251f4  8b4e1c                mov       ecx,[esi+1Ch]
  004251f7  668b7e14              mov       di,[esi+14h]
  004251fb  03c8                  add       ecx,eax
  004251fd  52                    push      edx
  004251fe  894e1c                mov       [esi+1Ch],ecx
  00425201  e810081200            call      00545A16h
  00425206  0fbfcf                movsx     ecx,di
  00425209  0fafc1                imul      eax,ecx
  0042520c  8b5624                mov       edx,[esi+24h]
  0042520f  6683461264            add       word [esi+12h],64h
  00425214  6683462a32            add       word [esi+2Ah],32h
  00425219  668b4e2a              mov       cx,[esi+2Ah]
  0042521d  03d0                  add       edx,eax
  0042521f  47                    inc       edi
  00425220  66ff869a000000        inc       word [esi+9Ah]
  00425227  668b869a000000        mov       ax,[esi+9Ah]
  0042522e  663b4616              cmp       ax,[esi+16h]
  00425232  895624                mov       [esi+24h],edx
  00425235  66897e14              mov       [esi+14h],di
  00425239  7c2b                  jl        short 00425266h
  0042523b  668146282c01          add       word [esi+28h],12Ch
  00425241  66899e9a000000        mov       [esi+9Ah],bx
  00425248  8a5e40                mov       bl,[esi+40h]
  0042524b  81c12c010000          add       ecx,12Ch
  00425251  fec3                  inc       bl
  00425253  66894e2a              mov       [esi+2Ah],cx
  00425257  8ac3                  mov       al,bl
  00425259  885e40                mov       [esi+40h],bl
  0042525c  3c08                  cmp       al,8
  0042525e  7506                  jne       short 00425266h
  00425260  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00425266:
  00425266  5f                    pop       edi
  00425267  5e                    pop       esi
  00425268  5b                    pop       ebx
  00425269  c3                    ret

2 cible(s) de saut interne : 0x004251a5, 0x00425266
