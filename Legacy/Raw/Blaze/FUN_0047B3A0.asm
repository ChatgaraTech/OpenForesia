
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b3a0  section .text  offset fichier 0x7a7a0  191 octets

  0047b3a0  56                    push      esi
  0047b3a1  8b742408              mov       esi,[esp+8]
  0047b3a5  57                    push      edi
  0047b3a6  66837e1000            cmp       word [esi+10h],0
  0047b3ab  8b7e78                mov       edi,[esi+78h]
  0047b3ae  7536                  jne       short 0047B3E6h
  0047b3b0  f70700001000          test      dword [edi],100000h
  0047b3b6  752e                  jne       short 0047B3E6h
  0047b3b8  57                    push      edi
  0047b3b9  e802d80b00            call      00538BC0h
  0047b3be  0fbfc8                movsx     ecx,ax
  0047b3c1  66894612              mov       [esi+12h],ax
  0047b3c5  b8b5814e1b            mov       eax,1B4E81B5h
  0047b3ca  c1e10c                shl       ecx,0Ch
  0047b3cd  f7e9                  imul      ecx
  0047b3cf  c1fa04                sar       edx,4
  0047b3d2  8bc2                  mov       eax,edx
  0047b3d4  83c404                add       esp,4
  0047b3d7  c1e81f                shr       eax,1Fh
  0047b3da  03d0                  add       edx,eax
  0047b3dc  8bca                  mov       ecx,edx
  0047b3de  c1e110                shl       ecx,10h
  0047b3e1  0bca                  or        ecx,edx
  0047b3e3  894e28                mov       [esi+28h],ecx

LAB_0047b3e6:
  0047b3e6  e855bd0c00            call      00547140h
  0047b3eb  8b4f1c                mov       ecx,[edi+1Ch]
  0047b3ee  83e03f                and       eax,3Fh
  0047b3f1  83e820                sub       eax,20h
  0047b3f4  c1e00c                shl       eax,0Ch
  0047b3f7  03c1                  add       eax,ecx
  0047b3f9  89461c                mov       [esi+1Ch],eax
  0047b3fc  e83fbd0c00            call      00547140h
  0047b401  0fbf5612              movsx     edx,word [esi+12h]
  0047b405  83e03f                and       eax,3Fh
  0047b408  83e2fe                and       edx,0FFFFFFFEh
  0047b40b  8d4442c0              lea       eax,[edx+eax*2-40h]
  0047b40f  8b5720                mov       edx,[edi+20h]
  0047b412  c1e00b                shl       eax,0Bh
  0047b415  03c2                  add       eax,edx
  0047b417  894620                mov       [esi+20h],eax
  0047b41a  e821bd0c00            call      00547140h
  0047b41f  8b5724                mov       edx,[edi+24h]
  0047b422  83e03f                and       eax,3Fh
  0047b425  83e820                sub       eax,20h
  0047b428  c1e00c                shl       eax,0Ch
  0047b42b  03c2                  add       eax,edx
  0047b42d  66ff4610              inc       word [esi+10h]
  0047b431  66837e1014            cmp       word [esi+10h],14h
  0047b436  894624                mov       [esi+24h],eax
  0047b439  7d1b                  jge       short 0047B456h
  0047b43b  f70700000080          test      dword [edi],80000000h
  0047b441  7413                  je        short 0047B456h
  0047b443  e8f8bc0c00            call      00547140h
  0047b448  99                    cdq
  0047b449  b903000000            mov       ecx,3
  0047b44e  f7f9                  idiv      ecx
  0047b450  885640                mov       [esi+40h],dl
  0047b453  5f                    pop       edi
  0047b454  5e                    pop       esi
  0047b455  c3                    ret

LAB_0047b456:
  0047b456  c70600000000          mov       dword [esi],0
  0047b45c  5f                    pop       edi
  0047b45d  5e                    pop       esi
  0047b45e  c3                    ret

2 cible(s) de saut interne : 0x0047b3e6, 0x0047b456
