Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00433a50  section .text  offset fichier 0x32e50  272 octets

  00433a50  56                    push      esi
  00433a51  8b742408              mov       esi,[esp+8]
  00433a55  66837e1000            cmp       word [esi+10h],0
  00433a5a  0f8581000000          jne       near 00433AE1h
  00433a60  b800100000            mov       eax,1000h
  00433a65  c7463c380d5500        mov       dword [esi+3Ch],550D38h
  00433a6c  c6464000              mov       byte [esi+40h],0
  00433a70  c6460a09              mov       byte [esi+0Ah],9
  00433a74  c6460b00              mov       byte [esi+0Bh],0
  00433a78  66c746100100          mov       word [esi+10h],1
  00433a7e  66894628              mov       [esi+28h],ax
  00433a82  6689462a              mov       [esi+2Ah],ax
  00433a86  e8b5361100            call      00547140h
  00433a8b  99                    cdq
  00433a8c  b90a000000            mov       ecx,0Ah
  00433a91  f7f9                  idiv      ecx
  00433a93  83ea05                sub       edx,5
  00433a96  895650                mov       [esi+50h],edx
  00433a99  e8a2361100            call      00547140h
  00433a9e  99                    cdq
  00433a9f  b90a000000            mov       ecx,0Ah
  00433aa4  f7f9                  idiv      ecx
  00433aa6  83ea05                sub       edx,5
  00433aa9  895658                mov       [esi+58h],edx
  00433aac  e88f361100            call      00547140h
  00433ab1  99                    cdq
  00433ab2  b905000000            mov       ecx,5
  00433ab7  f7f9                  idiv      ecx
  00433ab9  03d1                  add       edx,ecx
  00433abb  895654                mov       [esi+54h],edx
  00433abe  e87d361100            call      00547140h
  00433ac3  99                    cdq
  00433ac4  b905000000            mov       ecx,5
  00433ac9  f7f9                  idiv      ecx
  00433acb  8d4202                lea       eax,[edx+2]
  00433ace  66894614              mov       [esi+14h],ax
  00433ad2  66894612              mov       [esi+12h],ax
  00433ad6  b040                  mov       al,40h
  00433ad8  884638                mov       [esi+38h],al
  00433adb  884639                mov       [esi+39h],al
  00433ade  88463a                mov       [esi+3Ah],al

LAB_00433ae1:
  00433ae1  66837e1001            cmp       word [esi+10h],1
  00433ae6  756d                  jne       short 00433B55h
  00433ae8  8b5650                mov       edx,[esi+50h]
  00433aeb  8b461c                mov       eax,[esi+1Ch]
  00433aee  8b4e24                mov       ecx,[esi+24h]
  00433af1  6afd                  push      0FFFFFFFDh
  00433af3  c1e20c                shl       edx,0Ch
  00433af6  03c2                  add       eax,edx
  00433af8  6afd                  push      0FFFFFFFDh
  00433afa  89461c                mov       [esi+1Ch],eax
  00433afd  8b4658                mov       eax,[esi+58h]
  00433b00  c1e00c                shl       eax,0Ch
  00433b03  03c8                  add       ecx,eax
  00433b05  8b4620                mov       eax,[esi+20h]
  00433b08  894e24                mov       [esi+24h],ecx
  00433b0b  8b4e54                mov       ecx,[esi+54h]
  00433b0e  c1e10c                shl       ecx,0Ch
  00433b11  03c1                  add       eax,ecx
  00433b13  6afd                  push      0FFFFFFFDh
  00433b15  894620                mov       [esi+20h],eax
  00433b18  b864000000            mov       eax,64h
  00433b1d  66014628              add       [esi+28h],ax
  00433b21  6601462a              add       [esi+2Ah],ax
  00433b25  56                    push      esi
  00433b26  e8b512fdff            call      00404DE0h
  00433b2b  83c410                add       esp,10h
  00433b2e  66ff4e12              dec       word [esi+12h]
  00433b32  66837e1200            cmp       word [esi+12h],0
  00433b37  7f1c                  jg        short 00433B55h
  00433b39  8a4e40                mov       cl,[esi+40h]
  00433b3c  668b5614              mov       dx,[esi+14h]
  00433b40  fec1                  inc       cl
  00433b42  66895612              mov       [esi+12h],dx
  00433b46  8ac1                  mov       al,cl
  00433b48  884e40                mov       [esi+40h],cl
  00433b4b  3c04                  cmp       al,4
  00433b4d  7506                  jne       short 00433B55h
  00433b4f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00433b55:
  00433b55  5e                    pop       esi
  00433b56  c3                    ret
  00433b57  90                    nop
  00433b58  90                    nop
  00433b59  90                    nop
  00433b5a  90                    nop
  00433b5b  90                    nop
  00433b5c  90                    nop
  00433b5d  90                    nop
  00433b5e  90                    nop
  00433b5f  90                    nop

2 cible(s) de saut interne : 0x00433ae1, 0x00433b55
