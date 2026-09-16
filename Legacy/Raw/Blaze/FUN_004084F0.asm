Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004084f0  section .text  offset fichier 0x78f0  400 octets

  004084f0  53                    push      ebx
  004084f1  56                    push      esi
  004084f2  8b74240c              mov       esi,[esp+0Ch]
  004084f6  33db                  xor       ebx,ebx
  004084f8  66395e10              cmp       [esi+10h],bx
  004084fc  0f85c8000000          jne       near 004085CAh
  00408502  66c746100100          mov       word [esi+10h],1
  00408508  c7463c08f35400        mov       dword [esi+3Ch],54F308h
  0040850f  885e40                mov       [esi+40h],bl
  00408512  e829ec1300            call      00547140h
  00408517  99                    cdq
  00408518  33c2                  xor       eax,edx
  0040851a  2bc2                  sub       eax,edx
  0040851c  25ff0f0000            and       eax,0FFFh
  00408521  33c2                  xor       eax,edx
  00408523  2bc2                  sub       eax,edx
  00408525  66894632              mov       [esi+32h],ax
  00408529  e812ec1300            call      00547140h
  0040852e  99                    cdq
  0040852f  b9f8020000            mov       ecx,2F8h
  00408534  f7f9                  idiv      ecx
  00408536  8b4620                mov       eax,[esi+20h]
  00408539  81ea7c010000          sub       edx,17Ch
  0040853f  c1e20c                shl       edx,0Ch
  00408542  03c2                  add       eax,edx
  00408544  8b561c                mov       edx,[esi+1Ch]
  00408547  894620                mov       [esi+20h],eax
  0040854a  894654                mov       [esi+54h],eax
  0040854d  8b4624                mov       eax,[esi+24h]
  00408550  895650                mov       [esi+50h],edx
  00408553  894658                mov       [esi+58h],eax
  00408556  e8e5eb1300            call      00547140h
  0040855b  99                    cdq
  0040855c  b964000000            mov       ecx,64h
  00408561  f7f9                  idiv      ecx
  00408563  81c22c010000          add       edx,12Ch
  00408569  6689969a000000        mov       [esi+9Ah],dx
  00408570  e8cbeb1300            call      00547140h
  00408575  99                    cdq
  00408576  b90a000000            mov       ecx,0Ah
  0040857b  885e38                mov       [esi+38h],bl
  0040857e  f7f9                  idiv      ecx
  00408580  885e39                mov       [esi+39h],bl
  00408583  885e3a                mov       [esi+3Ah],bl
  00408586  83c205                add       edx,5
  00408589  89565c                mov       [esi+5Ch],edx
  0040858c  e8afeb1300            call      00547140h
  00408591  99                    cdq
  00408592  33c2                  xor       eax,edx
  00408594  c6460a09              mov       byte [esi+0Ah],9
  00408598  2bc2                  sub       eax,edx
  0040859a  885e0b                mov       [esi+0Bh],bl
  0040859d  25ff0f0000            and       eax,0FFFh
  004085a2  33c2                  xor       eax,edx
  004085a4  2bc2                  sub       eax,edx
  004085a6  66894612              mov       [esi+12h],ax
  004085aa  e891eb1300            call      00547140h
  004085af  99                    cdq
  004085b0  33c2                  xor       eax,edx
  004085b2  2bc2                  sub       eax,edx
  004085b4  25ff030000            and       eax,3FFh
  004085b9  33c2                  xor       eax,edx
  004085bb  2bc2                  sub       eax,edx
  004085bd  0500100000            add       eax,1000h
  004085c2  66894628              mov       [esi+28h],ax
  004085c6  6689462a              mov       [esi+2Ah],ax

LAB_004085ca:
  004085ca  66837e1001            cmp       word [esi+10h],1
  004085cf  0f859d000000          jne       near 00408672h
  004085d5  6a14                  push      14h
  004085d7  6a14                  push      14h
  004085d9  6a14                  push      14h
  004085db  56                    push      esi
  004085dc  e8ffc7ffff            call      00404DE0h
  004085e1  8a4e38                mov       cl,[esi+38h]
  004085e4  b080                  mov       al,80h
  004085e6  83c410                add       esp,10h
  004085e9  3ac8                  cmp       cl,al
  004085eb  7209                  jb        short 004085F6h
  004085ed  884638                mov       [esi+38h],al
  004085f0  884639                mov       [esi+39h],al
  004085f3  88463a                mov       [esi+3Ah],al

LAB_004085f6:
  004085f6  0fbf5632              movsx     edx,word [esi+32h]
  004085fa  52                    push      edx
  004085fb  e81cd41300            call      00545A1Ch
  00408600  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00408607  0fafc1                imul      eax,ecx
  0040860a  0fbf5632              movsx     edx,word [esi+32h]
  0040860e  8b4e50                mov       ecx,[esi+50h]
  00408611  52                    push      edx
  00408612  03c1                  add       eax,ecx
  00408614  89461c                mov       [esi+1Ch],eax
  00408617  e8fad31300            call      00545A16h
  0040861c  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00408623  0fafc1                imul      eax,ecx
  00408626  034658                add       eax,[esi+58h]
  00408629  0fbf5612              movsx     edx,word [esi+12h]
  0040862d  894624                mov       [esi+24h],eax
  00408630  0fbf4632              movsx     eax,word [esi+32h]
  00408634  03d0                  add       edx,eax
  00408636  52                    push      edx
  00408637  e8e0d31300            call      00545A1Ch
  0040863c  8b5654                mov       edx,[esi+54h]
  0040863f  8d0480                lea       eax,[eax+eax*4]
  00408642  8d0c80                lea       ecx,[eax+eax*4]
  00408645  8d044a                lea       eax,[edx+ecx*2]
  00408648  668b4e5c              mov       cx,[esi+5Ch]
  0040864c  666bc914              imul      cx,14h
  00408650  668b565c              mov       dx,[esi+5Ch]
  00408654  66014e32              add       [esi+32h],cx
  00408658  6629969a000000        sub       [esi+9Ah],dx
  0040865f  6683be9a00000032      cmp       word [esi+9Ah],32h
  00408667  894620                mov       [esi+20h],eax
  0040866a  7d06                  jge       short 00408672h
  0040866c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00408672:
  00408672  5e                    pop       esi
  00408673  5b                    pop       ebx
  00408674  c3                    ret
  00408675  90                    nop
  00408676  90                    nop
  00408677  90                    nop
  00408678  90                    nop
  00408679  90                    nop
  0040867a  90                    nop
  0040867b  90                    nop
  0040867c  90                    nop
  0040867d  90                    nop
  0040867e  90                    nop
  0040867f  90                    nop

3 cible(s) de saut interne : 0x004085ca, 0x004085f6, 0x00408672
