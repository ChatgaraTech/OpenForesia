Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00409120  section .text  offset fichier 0x8520  400 octets

  00409120  53                    push      ebx
  00409121  56                    push      esi
  00409122  8b74240c              mov       esi,[esp+0Ch]
  00409126  33db                  xor       ebx,ebx
  00409128  66395e10              cmp       [esi+10h],bx
  0040912c  0f8516010000          jne       near 00409248h
  00409132  c6460a01              mov       byte [esi+0Ah],1
  00409136  885e0b                mov       [esi+0Bh],bl
  00409139  c7463c60f35400        mov       dword [esi+3Ch],54F360h
  00409140  e8fbdf1300            call      00547140h
  00409145  99                    cdq
  00409146  b996000000            mov       ecx,96h
  0040914b  f7f9                  idiv      ecx
  0040914d  8b4e1c                mov       ecx,[esi+1Ch]
  00409150  83ea4b                sub       edx,4Bh
  00409153  c1e20c                shl       edx,0Ch
  00409156  03ca                  add       ecx,edx
  00409158  894e1c                mov       [esi+1Ch],ecx
  0040915b  e8e0df1300            call      00547140h
  00409160  99                    cdq
  00409161  b932000000            mov       ecx,32h
  00409166  f7f9                  idiv      ecx
  00409168  8b4620                mov       eax,[esi+20h]
  0040916b  83ea19                sub       edx,19h
  0040916e  c1e20c                shl       edx,0Ch
  00409171  03c2                  add       eax,edx
  00409173  894620                mov       [esi+20h],eax
  00409176  e8c5df1300            call      00547140h
  0040917b  99                    cdq
  0040917c  b9c8000000            mov       ecx,0C8h
  00409181  f7f9                  idiv      ecx
  00409183  8b4e24                mov       ecx,[esi+24h]
  00409186  83ea64                sub       edx,64h
  00409189  c1e20c                shl       edx,0Ch
  0040918c  03ca                  add       ecx,edx
  0040918e  0fbf5632              movsx     edx,word [esi+32h]
  00409192  52                    push      edx
  00409193  894e24                mov       [esi+24h],ecx
  00409196  e881c81300            call      00545A1Ch
  0040919b  8b4e24                mov       ecx,[esi+24h]
  0040919e  8d0480                lea       eax,[eax+eax*4]
  004091a1  8d0480                lea       eax,[eax+eax*4]
  004091a4  d1e0                  shl       eax,1
  004091a6  03c8                  add       ecx,eax
  004091a8  894e24                mov       [esi+24h],ecx
  004091ab  0fbf4e32              movsx     ecx,word [esi+32h]
  004091af  51                    push      ecx
  004091b0  e861c81300            call      00545A16h
  004091b5  8d0480                lea       eax,[eax+eax*4]
  004091b8  8d1480                lea       edx,[eax+eax*4]
  004091bb  8b461c                mov       eax,[esi+1Ch]
  004091be  d1e2                  shl       edx,1
  004091c0  03c2                  add       eax,edx
  004091c2  89461c                mov       [esi+1Ch],eax
  004091c5  e876df1300            call      00547140h
  004091ca  99                    cdq
  004091cb  b90c000000            mov       ecx,0Ch
  004091d0  f7f9                  idiv      ecx
  004091d2  83ea06                sub       edx,6
  004091d5  c1e20c                shl       edx,0Ch
  004091d8  895650                mov       [esi+50h],edx
  004091db  e860df1300            call      00547140h
  004091e0  99                    cdq
  004091e1  b914000000            mov       ecx,14h
  004091e6  f7f9                  idiv      ecx
  004091e8  83c205                add       edx,5
  004091eb  c1e20c                shl       edx,0Ch
  004091ee  895654                mov       [esi+54h],edx
  004091f1  e84adf1300            call      00547140h
  004091f6  99                    cdq
  004091f7  b90c000000            mov       ecx,0Ch
  004091fc  f7f9                  idiv      ecx
  004091fe  83ea06                sub       edx,6
  00409201  c1e20c                shl       edx,0Ch
  00409204  895658                mov       [esi+58h],edx
  00409207  e834df1300            call      00547140h
  0040920c  99                    cdq
  0040920d  33c2                  xor       eax,edx
  0040920f  66c746100100          mov       word [esi+10h],1
  00409215  2bc2                  sub       eax,edx
  00409217  25ff070000            and       eax,7FFh
  0040921c  33c2                  xor       eax,edx
  0040921e  2bc2                  sub       eax,edx
  00409220  0500080000            add       eax,800h
  00409225  66894628              mov       [esi+28h],ax
  00409229  6689462a              mov       [esi+2Ah],ax
  0040922d  e80edf1300            call      00547140h
  00409232  99                    cdq
  00409233  b903000000            mov       ecx,3
  00409238  66895e14              mov       [esi+14h],bx
  0040923c  f7f9                  idiv      ecx
  0040923e  885e40                mov       [esi+40h],bl
  00409241  83c202                add       edx,2
  00409244  66895612              mov       [esi+12h],dx

LAB_00409248:
  00409248  66837e1001            cmp       word [esi+10h],1
  0040924d  755a                  jne       short 004092A9h
  0040924f  8b5650                mov       edx,[esi+50h]
  00409252  8b461c                mov       eax,[esi+1Ch]
  00409255  8b4e20                mov       ecx,[esi+20h]
  00409258  03c2                  add       eax,edx
  0040925a  8b5624                mov       edx,[esi+24h]
  0040925d  89461c                mov       [esi+1Ch],eax
  00409260  8b4654                mov       eax,[esi+54h]
  00409263  03c8                  add       ecx,eax
  00409265  0500f0ffff            add       eax,0FFFFF000h
  0040926a  894e20                mov       [esi+20h],ecx
  0040926d  8b4e58                mov       ecx,[esi+58h]
  00409270  03d1                  add       edx,ecx
  00409272  66ff4614              inc       word [esi+14h]
  00409276  668b4e14              mov       cx,[esi+14h]
  0040927a  895624                mov       [esi+24h],edx
  0040927d  b2dc                  mov       dl,0DCh
  0040927f  894654                mov       [esi+54h],eax
  00409282  005638                add       [esi+38h],dl
  00409285  005639                add       [esi+39h],dl
  00409288  00563a                add       [esi+3Ah],dl
  0040928b  663b4e12              cmp       cx,[esi+12h]
  0040928f  7518                  jne       short 004092A9h
  00409291  8a4e40                mov       cl,[esi+40h]
  00409294  66895e14              mov       [esi+14h],bx
  00409298  fec1                  inc       cl
  0040929a  8ac1                  mov       al,cl
  0040929c  884e40                mov       [esi+40h],cl
  0040929f  3c04                  cmp       al,4
  004092a1  7206                  jb        short 004092A9h
  004092a3  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004092a9:
  004092a9  5e                    pop       esi
  004092aa  5b                    pop       ebx
  004092ab  c3                    ret
  004092ac  90                    nop
  004092ad  90                    nop
  004092ae  90                    nop
  004092af  90                    nop

2 cible(s) de saut interne : 0x00409248, 0x004092a9
