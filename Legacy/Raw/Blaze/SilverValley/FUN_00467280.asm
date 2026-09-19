Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00467280  section .text  offset fichier 0x66680  448 octets

  00467280  53                    push      ebx
  00467281  55                    push      ebp
  00467282  56                    push      esi
  00467283  8b742410              mov       esi,[esp+10h]
  00467287  57                    push      edi
  00467288  0fbf4614              movsx     eax,word [esi+14h]
  0046728c  8d0c80                lea       ecx,[eax+eax*4]
  0046728f  c1e103                shl       ecx,3
  00467292  2bc8                  sub       ecx,eax
  00467294  668b548e10            mov       dx,[esi+ecx*4+10h]
  00467299  8d048e                lea       eax,[esi+ecx*4]
  0046729c  66895610              mov       [esi+10h],dx
  004672a0  8b4820                mov       ecx,[eax+20h]
  004672a3  894e20                mov       [esi+20h],ecx
  004672a6  668bca                mov       cx,dx
  004672a9  6683f901              cmp       cx,1
  004672ad  740a                  je        short 004672B9h
  004672af  6683f903              cmp       cx,3
  004672b3  0f85ae000000          jne       near 00467367h

LAB_004672b9:
  004672b9  8b501c                mov       edx,[eax+1Ch]
  004672bc  89561c                mov       [esi+1Ch],edx
  004672bf  8b4824                mov       ecx,[eax+24h]
  004672c2  894e24                mov       [esi+24h],ecx
  004672c5  668b5030              mov       dx,[eax+30h]
  004672c9  66895630              mov       [esi+30h],dx
  004672cd  668b4832              mov       cx,[eax+32h]
  004672d1  66894e32              mov       [esi+32h],cx
  004672d5  668b5034              mov       dx,[eax+34h]
  004672d9  66895634              mov       [esi+34h],dx
  004672dd  e85efe0d00            call      00547140h
  004672e2  99                    cdq
  004672e3  b958020000            mov       ecx,258h
  004672e8  f7f9                  idiv      ecx
  004672ea  81ea2c010000          sub       edx,12Ch
  004672f0  66895612              mov       [esi+12h],dx
  004672f4  e847fe0d00            call      00547140h
  004672f9  99                    cdq
  004672fa  b958020000            mov       ecx,258h
  004672ff  f7f9                  idiv      ecx
  00467301  81ea2c010000          sub       edx,12Ch
  00467307  6689969a000000        mov       [esi+9Ah],dx
  0046730e  e82dfe0d00            call      00547140h
  00467313  99                    cdq
  00467314  b958020000            mov       ecx,258h
  00467319  f7f9                  idiv      ecx
  0046731b  81ea2c010000          sub       edx,12Ch
  00467321  66895616              mov       [esi+16h],dx
  00467325  e816fe0d00            call      00547140h
  0046732a  99                    cdq
  0046732b  b91e000000            mov       ecx,1Eh
  00467330  f7f9                  idiv      ecx
  00467332  83ea0f                sub       edx,0Fh
  00467335  c1e20c                shl       edx,0Ch
  00467338  895650                mov       [esi+50h],edx
  0046733b  e800fe0d00            call      00547140h
  00467340  99                    cdq
  00467341  b91e000000            mov       ecx,1Eh
  00467346  f7f9                  idiv      ecx
  00467348  83ea0f                sub       edx,0Fh
  0046734b  c1e20c                shl       edx,0Ch
  0046734e  895654                mov       [esi+54h],edx
  00467351  e8eafd0d00            call      00547140h
  00467356  99                    cdq
  00467357  b91e000000            mov       ecx,1Eh
  0046735c  f7f9                  idiv      ecx
  0046735e  83ea0f                sub       edx,0Fh
  00467361  c1e20c                shl       edx,0Ch
  00467364  895658                mov       [esi+58h],edx

LAB_00467367:
  00467367  668b5610              mov       dx,[esi+10h]
  0046736b  33ff                  xor       edi,edi
  0046736d  6683fa02              cmp       dx,2
  00467371  b838ffffff            mov       eax,0FFFFFF38h
  00467376  7552                  jne       short 004673CAh
  00467378  668b5e12              mov       bx,[esi+12h]
  0046737c  8b6e1c                mov       ebp,[esi+1Ch]
  0046737f  66015e30              add       [esi+30h],bx
  00467383  668b9e9a000000        mov       bx,[esi+9Ah]
  0046738a  66015e32              add       [esi+32h],bx
  0046738e  668b5e16              mov       bx,[esi+16h]
  00467392  66015e34              add       [esi+34h],bx
  00467396  8b5e50                mov       ebx,[esi+50h]
  00467399  66014628              add       [esi+28h],ax
  0046739d  668b4e28              mov       cx,[esi+28h]
  004673a1  6601462a              add       [esi+2Ah],ax
  004673a5  6601462c              add       [esi+2Ch],ax
  004673a9  03eb                  add       ebp,ebx
  004673ab  8b5e58                mov       ebx,[esi+58h]
  004673ae  896e1c                mov       [esi+1Ch],ebp
  004673b1  8b6e24                mov       ebp,[esi+24h]
  004673b4  03eb                  add       ebp,ebx
  004673b6  663bcf                cmp       cx,di
  004673b9  896e24                mov       [esi+24h],ebp
  004673bc  7d0c                  jge       short 004673CAh
  004673be  66897e28              mov       [esi+28h],di
  004673c2  66897e2a              mov       [esi+2Ah],di
  004673c6  66897e2c              mov       [esi+2Ch],di

LAB_004673ca:
  004673ca  6683fa03              cmp       dx,3
  004673ce  7552                  jne       short 00467422h
  004673d0  66014628              add       [esi+28h],ax
  004673d4  6601462a              add       [esi+2Ah],ax
  004673d8  6601462c              add       [esi+2Ch],ax
  004673dc  668b4612              mov       ax,[esi+12h]
  004673e0  66014630              add       [esi+30h],ax
  004673e4  668b869a000000        mov       ax,[esi+9Ah]
  004673eb  66014632              add       [esi+32h],ax
  004673ef  668b4616              mov       ax,[esi+16h]
  004673f3  66014634              add       [esi+34h],ax
  004673f7  8b4650                mov       eax,[esi+50h]
  004673fa  8b6e1c                mov       ebp,[esi+1Ch]
  004673fd  8b5e24                mov       ebx,[esi+24h]
  00467400  668b4e28              mov       cx,[esi+28h]
  00467404  03e8                  add       ebp,eax
  00467406  8b4658                mov       eax,[esi+58h]
  00467409  896e1c                mov       [esi+1Ch],ebp
  0046740c  03d8                  add       ebx,eax
  0046740e  663bcf                cmp       cx,di
  00467411  895e24                mov       [esi+24h],ebx
  00467414  7d0c                  jge       short 00467422h
  00467416  66897e28              mov       [esi+28h],di
  0046741a  66897e2a              mov       [esi+2Ah],di
  0046741e  66897e2c              mov       [esi+2Ch],di

LAB_00467422:
  00467422  6683fa63              cmp       dx,63h
  00467426  7511                  jne       short 00467439h
  00467428  66ff4612              inc       word [esi+12h]
  0046742c  66837e120a            cmp       word [esi+12h],0Ah
  00467431  7e06                  jle       short 00467439h
  00467433  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00467439:
  00467439  5f                    pop       edi
  0046743a  5e                    pop       esi
  0046743b  5d                    pop       ebp
  0046743c  5b                    pop       ebx
  0046743d  c3                    ret
  0046743e  90                    nop
  0046743f  90                    nop

5 cible(s) de saut interne : 0x004672b9, 0x00467367, 0x004673ca, 0x00467422, 0x00467439
