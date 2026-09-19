Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004354a0  section .text  offset fichier 0x348a0  336 octets

  004354a0  56                    push      esi
  004354a1  8b742408              mov       esi,[esp+8]
  004354a5  57                    push      edi
  004354a6  bf01000000            mov       edi,1
  004354ab  66837e1000            cmp       word [esi+10h],0
  004354b0  0f85c2000000          jne       near 00435578h
  004354b6  66897e10              mov       [esi+10h],di
  004354ba  c7463cb80e5500        mov       dword [esi+3Ch],550EB8h
  004354c1  c6464000              mov       byte [esi+40h],0
  004354c5  c6460a02              mov       byte [esi+0Ah],2
  004354c9  c6460b00              mov       byte [esi+0Bh],0
  004354cd  e86e1c1100            call      00547140h
  004354d2  99                    cdq
  004354d3  b9e8030000            mov       ecx,3E8h
  004354d8  f7f9                  idiv      ecx
  004354da  8d8220030000          lea       eax,[edx+320h]
  004354e0  66894628              mov       [esi+28h],ax
  004354e4  6689462a              mov       [esi+2Ah],ax
  004354e8  e8531c1100            call      00547140h
  004354ed  99                    cdq
  004354ee  b920030000            mov       ecx,320h
  004354f3  f7f9                  idiv      ecx
  004354f5  81ea90010000          sub       edx,190h
  004354fb  66015632              add       [esi+32h],dx
  004354ff  e83c1c1100            call      00547140h
  00435504  99                    cdq
  00435505  33c2                  xor       eax,edx
  00435507  2bc2                  sub       eax,edx
  00435509  83e007                and       eax,7
  0043550c  33c2                  xor       eax,edx
  0043550e  2bc2                  sub       eax,edx
  00435510  83c008                add       eax,8
  00435513  66894614              mov       [esi+14h],ax
  00435517  e8241c1100            call      00547140h
  0043551c  99                    cdq
  0043551d  b906000000            mov       ecx,6
  00435522  f7f9                  idiv      ecx
  00435524  03d1                  add       edx,ecx
  00435526  66895616              mov       [esi+16h],dx
  0043552a  e8111c1100            call      00547140h
  0043552f  99                    cdq
  00435530  b93c000000            mov       ecx,3Ch
  00435535  f7f9                  idiv      ecx
  00435537  8b4e1c                mov       ecx,[esi+1Ch]
  0043553a  83ea1e                sub       edx,1Eh
  0043553d  c1e20c                shl       edx,0Ch
  00435540  03ca                  add       ecx,edx
  00435542  894e1c                mov       [esi+1Ch],ecx
  00435545  e8f61b1100            call      00547140h
  0043554a  99                    cdq
  0043554b  b93c000000            mov       ecx,3Ch
  00435550  f7f9                  idiv      ecx
  00435552  8b4624                mov       eax,[esi+24h]
  00435555  83ea1e                sub       edx,1Eh
  00435558  c1e20c                shl       edx,0Ch
  0043555b  03c2                  add       eax,edx
  0043555d  894624                mov       [esi+24h],eax
  00435560  e8db1b1100            call      00547140h
  00435565  99                    cdq
  00435566  b914000000            mov       ecx,14h
  0043556b  f7f9                  idiv      ecx
  0043556d  8b4620                mov       eax,[esi+20h]
  00435570  c1e20c                shl       edx,0Ch
  00435573  03c2                  add       eax,edx
  00435575  894620                mov       [esi+20h],eax

LAB_00435578:
  00435578  66397e10              cmp       [esi+10h],di
  0043557c  7567                  jne       short 004355E5h
  0043557e  0fbf5632              movsx     edx,word [esi+32h]
  00435582  52                    push      edx
  00435583  e894041100            call      00545A1Ch
  00435588  0fbf4e14              movsx     ecx,word [esi+14h]
  0043558c  0fafc1                imul      eax,ecx
  0043558f  0fbf5632              movsx     edx,word [esi+32h]
  00435593  8b4e1c                mov       ecx,[esi+1Ch]
  00435596  668b7e14              mov       di,[esi+14h]
  0043559a  03c8                  add       ecx,eax
  0043559c  52                    push      edx
  0043559d  894e1c                mov       [esi+1Ch],ecx
  004355a0  e871041100            call      00545A16h
  004355a5  0fbfcf                movsx     ecx,di
  004355a8  0fafc1                imul      eax,ecx
  004355ab  8b5624                mov       edx,[esi+24h]
  004355ae  6ae2                  push      0FFFFFFE2h
  004355b0  03d0                  add       edx,eax
  004355b2  8b4620                mov       eax,[esi+20h]
  004355b5  895624                mov       [esi+24h],edx
  004355b8  6ae2                  push      0FFFFFFE2h
  004355ba  0fbf5616              movsx     edx,word [esi+16h]
  004355be  c1e20c                shl       edx,0Ch
  004355c1  03c2                  add       eax,edx
  004355c3  83c7fe                add       edi,0FFFFFFFEh
  004355c6  6ae2                  push      0FFFFFFE2h
  004355c8  56                    push      esi
  004355c9  894620                mov       [esi+20h],eax
  004355cc  66897e14              mov       [esi+14h],di
  004355d0  e80bf8fcff            call      00404DE0h
  004355d5  83c410                add       esp,10h
  004355d8  66837e1400            cmp       word [esi+14h],0
  004355dd  7f06                  jg        short 004355E5h
  004355df  c706ffffff7f          mov       dword [esi],7FFFFFFFh

LAB_004355e5:
  004355e5  5f                    pop       edi
  004355e6  5e                    pop       esi
  004355e7  c3                    ret
  004355e8  90                    nop
  004355e9  90                    nop
  004355ea  90                    nop
  004355eb  90                    nop
  004355ec  90                    nop
  004355ed  90                    nop
  004355ee  90                    nop
  004355ef  90                    nop

2 cible(s) de saut interne : 0x00435578, 0x004355e5
