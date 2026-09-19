Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00438260  section .text  offset fichier 0x37660  288 octets

  00438260  53                    push      ebx
  00438261  56                    push      esi
  00438262  8b74240c              mov       esi,[esp+0Ch]
  00438266  33db                  xor       ebx,ebx
  00438268  66395e10              cmp       [esi+10h],bx
  0043826c  0f85a9000000          jne       near 0043831Bh
  00438272  66c746100100          mov       word [esi+10h],1
  00438278  c7463c600f5500        mov       dword [esi+3Ch],550F60h
  0043827f  885e40                mov       [esi+40h],bl
  00438282  e8b9ee1000            call      00547140h
  00438287  99                    cdq
  00438288  b9e8070000            mov       ecx,7E8h
  0043828d  f7f9                  idiv      ecx
  0043828f  8d82e8030000          lea       eax,[edx+3E8h]
  00438295  66894628              mov       [esi+28h],ax
  00438299  6689462a              mov       [esi+2Ah],ax
  0043829d  e89eee1000            call      00547140h
  004382a2  99                    cdq
  004382a3  b90a000000            mov       ecx,0Ah
  004382a8  f7f9                  idiv      ecx
  004382aa  83c208                add       edx,8
  004382ad  66895612              mov       [esi+12h],dx
  004382b1  e88aee1000            call      00547140h
  004382b6  99                    cdq
  004382b7  b9f4010000            mov       ecx,1F4h
  004382bc  f7f9                  idiv      ecx
  004382be  8b461c                mov       eax,[esi+1Ch]
  004382c1  81eafa000000          sub       edx,0FAh
  004382c7  c1e20c                shl       edx,0Ch
  004382ca  03c2                  add       eax,edx
  004382cc  89461c                mov       [esi+1Ch],eax
  004382cf  e86cee1000            call      00547140h
  004382d4  99                    cdq
  004382d5  b9f4010000            mov       ecx,1F4h
  004382da  c6460a02              mov       byte [esi+0Ah],2
  004382de  f7f9                  idiv      ecx
  004382e0  8b4e24                mov       ecx,[esi+24h]
  004382e3  885e0b                mov       [esi+0Bh],bl
  004382e6  66895e14              mov       [esi+14h],bx
  004382ea  81eafa000000          sub       edx,0FAh
  004382f0  c1e20c                shl       edx,0Ch
  004382f3  03ca                  add       ecx,edx
  004382f5  894e24                mov       [esi+24h],ecx
  004382f8  e843ee1000            call      00547140h
  004382fd  99                    cdq
  004382fe  b90a000000            mov       ecx,0Ah
  00438303  c64638be              mov       byte [esi+38h],0BEh
  00438307  f7f9                  idiv      ecx
  00438309  b096                  mov       al,96h
  0043830b  884639                mov       [esi+39h],al
  0043830e  88463a                mov       [esi+3Ah],al
  00438311  83c20f                add       edx,0Fh
  00438314  6689969a000000        mov       [esi+9Ah],dx

LAB_0043831b:
  0043831b  66837e1001            cmp       word [esi+10h],1
  00438320  7556                  jne       short 00438378h
  00438322  0fbf5612              movsx     edx,word [esi+12h]
  00438326  8b4620                mov       eax,[esi+20h]
  00438329  c1e20c                shl       edx,0Ch
  0043832c  03c2                  add       eax,edx
  0043832e  66ff4614              inc       word [esi+14h]
  00438332  894620                mov       [esi+20h],eax
  00438335  668b4614              mov       ax,[esi+14h]
  00438339  663b869a000000        cmp       ax,[esi+9Ah]
  00438340  7e36                  jle       short 00438378h
  00438342  6af8                  push      0FFFFFFF8h
  00438344  6af8                  push      0FFFFFFF8h
  00438346  6af8                  push      0FFFFFFF8h
  00438348  56                    push      esi
  00438349  e892cafcff            call      00404DE0h
  0043834e  b99cffffff            mov       ecx,0FFFFFF9Ch
  00438353  83c410                add       esp,10h
  00438356  66014e28              add       [esi+28h],cx
  0043835a  668b4628              mov       ax,[esi+28h]
  0043835e  66014e2a              add       [esi+2Ah],cx
  00438362  663bc3                cmp       ax,bx
  00438365  7d11                  jge       short 00438378h
  00438367  8b06                  mov       eax,[esi]
  00438369  66895e28              mov       [esi+28h],bx
  0043836d  25ffffff7f            and       eax,7FFFFFFFh
  00438372  66895e2a              mov       [esi+2Ah],bx
  00438376  8906                  mov       [esi],eax

LAB_00438378:
  00438378  5e                    pop       esi
  00438379  5b                    pop       ebx
  0043837a  c3                    ret
  0043837b  90                    nop
  0043837c  90                    nop
  0043837d  90                    nop
  0043837e  90                    nop
  0043837f  90                    nop

2 cible(s) de saut interne : 0x0043831b, 0x00438378
