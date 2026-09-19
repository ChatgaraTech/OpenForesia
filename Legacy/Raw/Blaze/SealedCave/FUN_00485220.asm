Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00485220  section .text  offset fichier 0x84620  272 octets

  00485220  83ec0c                sub       esp,0Ch
  00485223  56                    push      esi
  00485224  8b742414              mov       esi,[esp+14h]
  00485228  57                    push      edi
  00485229  668b4610              mov       ax,[esi+10h]
  0048522d  8b7e6c                mov       edi,[esi+6Ch]
  00485230  6685c0                test      ax,ax
  00485233  753b                  jne       short 00485270h
  00485235  8b4e44                mov       ecx,[esi+44h]
  00485238  8b564c                mov       edx,[esi+4Ch]
  0048523b  f7d9                  neg       ecx
  0048523d  894e44                mov       [esi+44h],ecx
  00485240  668b8e9a000000        mov       cx,[esi+9Ah]
  00485247  f7da                  neg       edx
  00485249  66014e32              add       [esi+32h],cx
  0048524d  897e74                mov       [esi+74h],edi
  00485250  897e70                mov       [esi+70h],edi
  00485253  66c746360400          mov       word [esi+36h],4
  00485259  c7462800080060        mov       dword [esi+28h],60000800h
  00485260  66c7462c0060          mov       word [esi+2Ch],6000h
  00485266  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0048526d  89564c                mov       [esi+4Ch],edx

LAB_00485270:
  00485270  a801                  test      al,1
  00485272  0f849f000000          je        near 00485317h
  00485278  8d542408              lea       edx,[esp+8]
  0048527c  8d4644                lea       eax,[esi+44h]
  0048527f  52                    push      edx
  00485280  8d4f30                lea       ecx,[edi+30h]
  00485283  50                    push      eax
  00485284  51                    push      ecx
  00485285  e8060e0b00            call      00536090h
  0048528a  8b571c                mov       edx,[edi+1Ch]
  0048528d  8b442414              mov       eax,[esp+14h]
  00485291  8b4c2418              mov       ecx,[esp+18h]
  00485295  03d0                  add       edx,eax
  00485297  89561c                mov       [esi+1Ch],edx
  0048529a  8b4720                mov       eax,[edi+20h]
  0048529d  8b54241c              mov       edx,[esp+1Ch]
  004852a1  03c1                  add       eax,ecx
  004852a3  894620                mov       [esi+20h],eax
  004852a6  8b4f24                mov       ecx,[edi+24h]
  004852a9  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004852b0  03ca                  add       ecx,edx
  004852b2  83c40c                add       esp,0Ch
  004852b5  0fbf5634              movsx     edx,word [esi+34h]
  004852b9  894e24                mov       [esi+24h],ecx
  004852bc  6a64                  push      64h
  004852be  0fbf4f32              movsx     ecx,word [edi+32h]
  004852c2  52                    push      edx
  004852c3  03c1                  add       eax,ecx
  004852c5  8d5650                lea       edx,[esi+50h]
  004852c8  50                    push      eax
  004852c9  52                    push      edx
  004852ca  e801d80a00            call      00532AD0h
  004852cf  668b4632              mov       ax,[esi+32h]
  004852d3  83c410                add       esp,10h
  004852d6  0fbff8                movsx     edi,ax
  004852d9  0500040000            add       eax,400h
  004852de  66894632              mov       [esi+32h],ax
  004852e2  e8591e0c00            call      00547140h
  004852e7  2403                  and       al,3
  004852e9  6a03                  push      3
  004852eb  0408                  add       al,8
  004852ed  6a00                  push      0
  004852ef  884604                mov       [esi+4],al
  004852f2  a1cc975e00            mov       eax,[5E97CCh]
  004852f7  8b88d0000000          mov       ecx,[eax+0D0h]
  004852fd  33c0                  xor       eax,eax
  004852ff  8b11                  mov       edx,[ecx]
  00485301  8a4211                mov       al,[edx+11h]
  00485304  05c0000000            add       eax,0C0h
  00485309  50                    push      eax
  0048530a  56                    push      esi
  0048530b  e8d057ffff            call      0047AAE0h
  00485310  83c410                add       esp,10h
  00485313  66897e32              mov       [esi+32h],di

LAB_00485317:
  00485317  66ff4610              inc       word [esi+10h]
  0048531b  66837e1014            cmp       word [esi+10h],14h
  00485320  7c06                  jl        short 00485328h
  00485322  c70600000000          mov       dword [esi],0

LAB_00485328:
  00485328  5f                    pop       edi
  00485329  5e                    pop       esi
  0048532a  83c40c                add       esp,0Ch
  0048532d  c3                    ret
  0048532e  90                    nop
  0048532f  90                    nop

3 cible(s) de saut interne : 0x00485270, 0x00485317, 0x00485328
