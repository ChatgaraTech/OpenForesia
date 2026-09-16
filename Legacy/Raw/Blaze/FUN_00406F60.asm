
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00406f60  section .text  offset fichier 0x6360  400 octets

  00406f60  53                    push      ebx
  00406f61  56                    push      esi
  00406f62  8b74240c              mov       esi,[esp+0Ch]
  00406f66  33db                  xor       ebx,ebx
  00406f68  66395e10              cmp       [esi+10h],bx
  00406f6c  0f8516010000          jne       near 00407088h
  00406f72  c6460a01              mov       byte [esi+0Ah],1
  00406f76  885e0b                mov       [esi+0Bh],bl
  00406f79  c7463c08f25400        mov       dword [esi+3Ch],54F208h
  00406f80  e8bb011400            call      00547140h
  00406f85  99                    cdq
  00406f86  b996000000            mov       ecx,96h
  00406f8b  f7f9                  idiv      ecx
  00406f8d  8b4e1c                mov       ecx,[esi+1Ch]
  00406f90  83ea4b                sub       edx,4Bh
  00406f93  c1e20c                shl       edx,0Ch
  00406f96  03ca                  add       ecx,edx
  00406f98  894e1c                mov       [esi+1Ch],ecx
  00406f9b  e8a0011400            call      00547140h
  00406fa0  99                    cdq
  00406fa1  b932000000            mov       ecx,32h
  00406fa6  f7f9                  idiv      ecx
  00406fa8  8b4620                mov       eax,[esi+20h]
  00406fab  83ea19                sub       edx,19h
  00406fae  c1e20c                shl       edx,0Ch
  00406fb1  03c2                  add       eax,edx
  00406fb3  894620                mov       [esi+20h],eax
  00406fb6  e885011400            call      00547140h
  00406fbb  99                    cdq
  00406fbc  b9c8000000            mov       ecx,0C8h
  00406fc1  f7f9                  idiv      ecx
  00406fc3  8b4e24                mov       ecx,[esi+24h]
  00406fc6  83ea64                sub       edx,64h
  00406fc9  c1e20c                shl       edx,0Ch
  00406fcc  03ca                  add       ecx,edx
  00406fce  0fbf5632              movsx     edx,word [esi+32h]
  00406fd2  52                    push      edx
  00406fd3  894e24                mov       [esi+24h],ecx
  00406fd6  e841ea1300            call      00545A1Ch
  00406fdb  8b4e24                mov       ecx,[esi+24h]
  00406fde  8d0480                lea       eax,[eax+eax*4]
  00406fe1  8d0480                lea       eax,[eax+eax*4]
  00406fe4  d1e0                  shl       eax,1
  00406fe6  03c8                  add       ecx,eax
  00406fe8  894e24                mov       [esi+24h],ecx
  00406feb  0fbf4e32              movsx     ecx,word [esi+32h]
  00406fef  51                    push      ecx
  00406ff0  e821ea1300            call      00545A16h
  00406ff5  8d0480                lea       eax,[eax+eax*4]
  00406ff8  8d1480                lea       edx,[eax+eax*4]
  00406ffb  8b461c                mov       eax,[esi+1Ch]
  00406ffe  d1e2                  shl       edx,1
  00407000  03c2                  add       eax,edx
  00407002  89461c                mov       [esi+1Ch],eax
  00407005  e836011400            call      00547140h
  0040700a  99                    cdq
  0040700b  b90c000000            mov       ecx,0Ch
  00407010  f7f9                  idiv      ecx
  00407012  83ea06                sub       edx,6
  00407015  c1e20c                shl       edx,0Ch
  00407018  895650                mov       [esi+50h],edx
  0040701b  e820011400            call      00547140h
  00407020  99                    cdq
  00407021  b914000000            mov       ecx,14h
  00407026  f7f9                  idiv      ecx
  00407028  83c205                add       edx,5
  0040702b  c1e20c                shl       edx,0Ch
  0040702e  895654                mov       [esi+54h],edx
  00407031  e80a011400            call      00547140h
  00407036  99                    cdq
  00407037  b90c000000            mov       ecx,0Ch
  0040703c  f7f9                  idiv      ecx
  0040703e  83ea06                sub       edx,6
  00407041  c1e20c                shl       edx,0Ch
  00407044  895658                mov       [esi+58h],edx
  00407047  e8f4001400            call      00547140h
  0040704c  99                    cdq
  0040704d  33c2                  xor       eax,edx
  0040704f  66c746100100          mov       word [esi+10h],1
  00407055  2bc2                  sub       eax,edx
  00407057  25ff070000            and       eax,7FFh
  0040705c  33c2                  xor       eax,edx
  0040705e  2bc2                  sub       eax,edx
  00407060  0500080000            add       eax,800h
  00407065  66894628              mov       [esi+28h],ax
  00407069  6689462a              mov       [esi+2Ah],ax
  0040706d  e8ce001400            call      00547140h
  00407072  99                    cdq
  00407073  b903000000            mov       ecx,3
  00407078  66895e14              mov       [esi+14h],bx
  0040707c  f7f9                  idiv      ecx
  0040707e  885e40                mov       [esi+40h],bl
  00407081  83c202                add       edx,2
  00407084  66895612              mov       [esi+12h],dx

LAB_00407088:
  00407088  66837e1001            cmp       word [esi+10h],1
  0040708d  755a                  jne       short 004070E9h
  0040708f  8b5650                mov       edx,[esi+50h]
  00407092  8b461c                mov       eax,[esi+1Ch]
  00407095  8b4e20                mov       ecx,[esi+20h]
  00407098  03c2                  add       eax,edx
  0040709a  8b5624                mov       edx,[esi+24h]
  0040709d  89461c                mov       [esi+1Ch],eax
  004070a0  8b4654                mov       eax,[esi+54h]
  004070a3  03c8                  add       ecx,eax
  004070a5  0500f0ffff            add       eax,0FFFFF000h
  004070aa  894e20                mov       [esi+20h],ecx
  004070ad  8b4e58                mov       ecx,[esi+58h]
  004070b0  03d1                  add       edx,ecx
  004070b2  66ff4614              inc       word [esi+14h]
  004070b6  668b4e14              mov       cx,[esi+14h]
  004070ba  895624                mov       [esi+24h],edx
  004070bd  b2dc                  mov       dl,0DCh
  004070bf  894654                mov       [esi+54h],eax
  004070c2  005638                add       [esi+38h],dl
  004070c5  005639                add       [esi+39h],dl
  004070c8  00563a                add       [esi+3Ah],dl
  004070cb  663b4e12              cmp       cx,[esi+12h]
  004070cf  7518                  jne       short 004070E9h
  004070d1  8a4e40                mov       cl,[esi+40h]
  004070d4  66895e14              mov       [esi+14h],bx
  004070d8  fec1                  inc       cl
  004070da  8ac1                  mov       al,cl
  004070dc  884e40                mov       [esi+40h],cl
  004070df  3c04                  cmp       al,4
  004070e1  7206                  jb        short 004070E9h
  004070e3  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004070e9:
  004070e9  5e                    pop       esi
  004070ea  5b                    pop       ebx
  004070eb  c3                    ret
  004070ec  90                    nop
  004070ed  90                    nop
  004070ee  90                    nop
  004070ef  90                    nop

2 cible(s) de saut interne : 0x00407088, 0x004070e9


