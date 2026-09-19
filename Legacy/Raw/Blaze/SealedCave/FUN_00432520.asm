Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00432520  section .text  offset fichier 0x31920  272 octets

  00432520  56                    push      esi
  00432521  8b742408              mov       esi,[esp+8]
  00432525  66837e1000            cmp       word [esi+10h],0
  0043252a  0f8581000000          jne       near 004325B1h
  00432530  b800100000            mov       eax,1000h
  00432535  c7463c480c5500        mov       dword [esi+3Ch],550C48h
  0043253c  c6464000              mov       byte [esi+40h],0
  00432540  c6460a09              mov       byte [esi+0Ah],9
  00432544  c6460b00              mov       byte [esi+0Bh],0
  00432548  66c746100100          mov       word [esi+10h],1
  0043254e  66894628              mov       [esi+28h],ax
  00432552  6689462a              mov       [esi+2Ah],ax
  00432556  e8e54b1100            call      00547140h
  0043255b  99                    cdq
  0043255c  b90a000000            mov       ecx,0Ah
  00432561  f7f9                  idiv      ecx
  00432563  83ea05                sub       edx,5
  00432566  895650                mov       [esi+50h],edx
  00432569  e8d24b1100            call      00547140h
  0043256e  99                    cdq
  0043256f  b90a000000            mov       ecx,0Ah
  00432574  f7f9                  idiv      ecx
  00432576  83ea05                sub       edx,5
  00432579  895658                mov       [esi+58h],edx
  0043257c  e8bf4b1100            call      00547140h
  00432581  99                    cdq
  00432582  b905000000            mov       ecx,5
  00432587  f7f9                  idiv      ecx
  00432589  03d1                  add       edx,ecx
  0043258b  895654                mov       [esi+54h],edx
  0043258e  e8ad4b1100            call      00547140h
  00432593  99                    cdq
  00432594  b905000000            mov       ecx,5
  00432599  f7f9                  idiv      ecx
  0043259b  8d4202                lea       eax,[edx+2]
  0043259e  66894614              mov       [esi+14h],ax
  004325a2  66894612              mov       [esi+12h],ax
  004325a6  b040                  mov       al,40h
  004325a8  884638                mov       [esi+38h],al
  004325ab  884639                mov       [esi+39h],al
  004325ae  88463a                mov       [esi+3Ah],al

LAB_004325b1:
  004325b1  66837e1001            cmp       word [esi+10h],1
  004325b6  756d                  jne       short 00432625h
  004325b8  8b5650                mov       edx,[esi+50h]
  004325bb  8b461c                mov       eax,[esi+1Ch]
  004325be  8b4e24                mov       ecx,[esi+24h]
  004325c1  6afd                  push      0FFFFFFFDh
  004325c3  c1e20c                shl       edx,0Ch
  004325c6  03c2                  add       eax,edx
  004325c8  6afd                  push      0FFFFFFFDh
  004325ca  89461c                mov       [esi+1Ch],eax
  004325cd  8b4658                mov       eax,[esi+58h]
  004325d0  c1e00c                shl       eax,0Ch
  004325d3  03c8                  add       ecx,eax
  004325d5  8b4620                mov       eax,[esi+20h]
  004325d8  894e24                mov       [esi+24h],ecx
  004325db  8b4e54                mov       ecx,[esi+54h]
  004325de  c1e10c                shl       ecx,0Ch
  004325e1  03c1                  add       eax,ecx
  004325e3  6afd                  push      0FFFFFFFDh
  004325e5  894620                mov       [esi+20h],eax
  004325e8  b864000000            mov       eax,64h
  004325ed  66014628              add       [esi+28h],ax
  004325f1  6601462a              add       [esi+2Ah],ax
  004325f5  56                    push      esi
  004325f6  e8e527fdff            call      00404DE0h
  004325fb  83c410                add       esp,10h
  004325fe  66ff4e12              dec       word [esi+12h]
  00432602  66837e1200            cmp       word [esi+12h],0
  00432607  7f1c                  jg        short 00432625h
  00432609  8a4e40                mov       cl,[esi+40h]
  0043260c  668b5614              mov       dx,[esi+14h]
  00432610  fec1                  inc       cl
  00432612  66895612              mov       [esi+12h],dx
  00432616  8ac1                  mov       al,cl
  00432618  884e40                mov       [esi+40h],cl
  0043261b  3c04                  cmp       al,4
  0043261d  7506                  jne       short 00432625h
  0043261f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00432625:
  00432625  5e                    pop       esi
  00432626  c3                    ret
  00432627  90                    nop
  00432628  90                    nop
  00432629  90                    nop
  0043262a  90                    nop
  0043262b  90                    nop
  0043262c  90                    nop
  0043262d  90                    nop
  0043262e  90                    nop
  0043262f  90                    nop

2 cible(s) de saut interne : 0x004325b1, 0x00432625
