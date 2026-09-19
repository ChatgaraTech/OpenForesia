Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004355f0  section .text  offset fichier 0x349f0  272 octets

  004355f0  56                    push      esi
  004355f1  8b742408              mov       esi,[esp+8]
  004355f5  66837e1000            cmp       word [esi+10h],0
  004355fa  0f8581000000          jne       near 00435681h
  00435600  b800100000            mov       eax,1000h
  00435605  c7463c480e5500        mov       dword [esi+3Ch],550E48h
  0043560c  c6464000              mov       byte [esi+40h],0
  00435610  c6460a09              mov       byte [esi+0Ah],9
  00435614  c6460b00              mov       byte [esi+0Bh],0
  00435618  66c746100100          mov       word [esi+10h],1
  0043561e  66894628              mov       [esi+28h],ax
  00435622  6689462a              mov       [esi+2Ah],ax
  00435626  e8151b1100            call      00547140h
  0043562b  99                    cdq
  0043562c  b90a000000            mov       ecx,0Ah
  00435631  f7f9                  idiv      ecx
  00435633  83ea05                sub       edx,5
  00435636  895650                mov       [esi+50h],edx
  00435639  e8021b1100            call      00547140h
  0043563e  99                    cdq
  0043563f  b90a000000            mov       ecx,0Ah
  00435644  f7f9                  idiv      ecx
  00435646  83ea05                sub       edx,5
  00435649  895658                mov       [esi+58h],edx
  0043564c  e8ef1a1100            call      00547140h
  00435651  99                    cdq
  00435652  b905000000            mov       ecx,5
  00435657  f7f9                  idiv      ecx
  00435659  03d1                  add       edx,ecx
  0043565b  895654                mov       [esi+54h],edx
  0043565e  e8dd1a1100            call      00547140h
  00435663  99                    cdq
  00435664  b905000000            mov       ecx,5
  00435669  f7f9                  idiv      ecx
  0043566b  8d4202                lea       eax,[edx+2]
  0043566e  66894614              mov       [esi+14h],ax
  00435672  66894612              mov       [esi+12h],ax
  00435676  b040                  mov       al,40h
  00435678  884638                mov       [esi+38h],al
  0043567b  884639                mov       [esi+39h],al
  0043567e  88463a                mov       [esi+3Ah],al

LAB_00435681:
  00435681  66837e1001            cmp       word [esi+10h],1
  00435686  756d                  jne       short 004356F5h
  00435688  8b5650                mov       edx,[esi+50h]
  0043568b  8b461c                mov       eax,[esi+1Ch]
  0043568e  8b4e24                mov       ecx,[esi+24h]
  00435691  6afd                  push      0FFFFFFFDh
  00435693  c1e20c                shl       edx,0Ch
  00435696  03c2                  add       eax,edx
  00435698  6afd                  push      0FFFFFFFDh
  0043569a  89461c                mov       [esi+1Ch],eax
  0043569d  8b4658                mov       eax,[esi+58h]
  004356a0  c1e00c                shl       eax,0Ch
  004356a3  03c8                  add       ecx,eax
  004356a5  8b4620                mov       eax,[esi+20h]
  004356a8  894e24                mov       [esi+24h],ecx
  004356ab  8b4e54                mov       ecx,[esi+54h]
  004356ae  c1e10c                shl       ecx,0Ch
  004356b1  03c1                  add       eax,ecx
  004356b3  6afd                  push      0FFFFFFFDh
  004356b5  894620                mov       [esi+20h],eax
  004356b8  b864000000            mov       eax,64h
  004356bd  66014628              add       [esi+28h],ax
  004356c1  6601462a              add       [esi+2Ah],ax
  004356c5  56                    push      esi
  004356c6  e815f7fcff            call      00404DE0h
  004356cb  83c410                add       esp,10h
  004356ce  66ff4e12              dec       word [esi+12h]
  004356d2  66837e1200            cmp       word [esi+12h],0
  004356d7  7f1c                  jg        short 004356F5h
  004356d9  8a4e40                mov       cl,[esi+40h]
  004356dc  668b5614              mov       dx,[esi+14h]
  004356e0  fec1                  inc       cl
  004356e2  66895612              mov       [esi+12h],dx
  004356e6  8ac1                  mov       al,cl
  004356e8  884e40                mov       [esi+40h],cl
  004356eb  3c04                  cmp       al,4
  004356ed  7506                  jne       short 004356F5h
  004356ef  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004356f5:
  004356f5  5e                    pop       esi
  004356f6  c3                    ret
  004356f7  90                    nop
  004356f8  90                    nop
  004356f9  90                    nop
  004356fa  90                    nop
  004356fb  90                    nop
  004356fc  90                    nop
  004356fd  90                    nop
  004356fe  90                    nop
  004356ff  90                    nop

2 cible(s) de saut interne : 0x00435681, 0x004356f5
