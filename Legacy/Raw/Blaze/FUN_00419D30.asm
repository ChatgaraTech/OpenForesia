Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00419d30  section .text  offset fichier 0x19130  304 octets

  00419d30  53                    push      ebx
  00419d31  56                    push      esi
  00419d32  8b74240c              mov       esi,[esp+0Ch]
  00419d36  33db                  xor       ebx,ebx
  00419d38  57                    push      edi
  00419d39  66395e10              cmp       [esi+10h],bx
  00419d3d  7578                  jne       short 00419DB7h
  00419d3f  b800080000            mov       eax,800h
  00419d44  66c746100100          mov       word [esi+10h],1
  00419d4a  c7463cb8fb5400        mov       dword [esi+3Ch],54FBB8h
  00419d51  66894628              mov       [esi+28h],ax
  00419d55  6689462a              mov       [esi+2Ah],ax
  00419d59  c6460a02              mov       byte [esi+0Ah],2
  00419d5d  885e0b                mov       [esi+0Bh],bl
  00419d60  e8dbd31200            call      00547140h
  00419d65  99                    cdq
  00419d66  33c2                  xor       eax,edx
  00419d68  66895e12              mov       [esi+12h],bx
  00419d6c  2bc2                  sub       eax,edx
  00419d6e  885e40                mov       [esi+40h],bl
  00419d71  25ff0f0000            and       eax,0FFFh
  00419d76  bf0b000000            mov       edi,0Bh
  00419d7b  33c2                  xor       eax,edx
  00419d7d  2bc2                  sub       eax,edx
  00419d7f  66014632              add       [esi+32h],ax
  00419d83  8b06                  mov       eax,[esi]
  00419d85  80cc40                or        ah,40h
  00419d88  8906                  mov       [esi],eax

LAB_00419d8a:
  00419d8a  0fbf4632              movsx     eax,word [esi+32h]
  00419d8e  50                    push      eax
  00419d8f  e888bc1200            call      00545A1Ch
  00419d94  8b4e1c                mov       ecx,[esi+1Ch]
  00419d97  c1e003                shl       eax,3
  00419d9a  03c8                  add       ecx,eax
  00419d9c  894e1c                mov       [esi+1Ch],ecx
  00419d9f  0fbf4e32              movsx     ecx,word [esi+32h]
  00419da3  51                    push      ecx
  00419da4  e86dbc1200            call      00545A16h
  00419da9  8b4e24                mov       ecx,[esi+24h]
  00419dac  c1e003                shl       eax,3
  00419daf  03c8                  add       ecx,eax
  00419db1  4f                    dec       edi
  00419db2  894e24                mov       [esi+24h],ecx
  00419db5  75d3                  jne       short 00419D8Ah

LAB_00419db7:
  00419db7  66837e1001            cmp       word [esi+10h],1
  00419dbc  752f                  jne       short 00419DEDh
  00419dbe  66ff4612              inc       word [esi+12h]
  00419dc2  e879d31200            call      00547140h
  00419dc7  99                    cdq
  00419dc8  b905000000            mov       ecx,5
  00419dcd  f7f9                  idiv      ecx
  00419dcf  6683fa01              cmp       dx,1
  00419dd3  7407                  je        short 00419DDCh
  00419dd5  66837e1203            cmp       word [esi+12h],3
  00419dda  7511                  jne       short 00419DEDh

LAB_00419ddc:
  00419ddc  8b06                  mov       eax,[esi]
  00419dde  66c746100200          mov       word [esi+10h],2
  00419de4  80e4bf                and       ah,0BFh
  00419de7  8906                  mov       [esi],eax
  00419de9  5f                    pop       edi
  00419dea  5e                    pop       esi
  00419deb  5b                    pop       ebx
  00419dec  c3                    ret

LAB_00419ded:
  00419ded  66837e1002            cmp       word [esi+10h],2
  00419df2  7559                  jne       short 00419E4Dh
  00419df4  0fbf5632              movsx     edx,word [esi+32h]
  00419df8  52                    push      edx
  00419df9  e81ebc1200            call      00545A1Ch
  00419dfe  8b7e1c                mov       edi,[esi+1Ch]
  00419e01  c1e003                shl       eax,3
  00419e04  03f8                  add       edi,eax
  00419e06  0fbf4632              movsx     eax,word [esi+32h]
  00419e0a  50                    push      eax
  00419e0b  897e1c                mov       [esi+1Ch],edi
  00419e0e  e803bc1200            call      00545A16h
  00419e13  8b7e24                mov       edi,[esi+24h]
  00419e16  8b5620                mov       edx,[esi+20h]
  00419e19  c1e003                shl       eax,3
  00419e1c  03f8                  add       edi,eax
  00419e1e  81c200300000          add       edx,3000h
  00419e24  66ff4612              inc       word [esi+12h]
  00419e28  66837e120a            cmp       word [esi+12h],0Ah
  00419e2d  897e24                mov       [esi+24h],edi
  00419e30  895620                mov       [esi+20h],edx
  00419e33  7c18                  jl        short 00419E4Dh
  00419e35  8a5640                mov       dl,[esi+40h]
  00419e38  66895e12              mov       [esi+12h],bx
  00419e3c  fec2                  inc       dl
  00419e3e  8ac2                  mov       al,dl
  00419e40  885640                mov       [esi+40h],dl
  00419e43  3c02                  cmp       al,2
  00419e45  7506                  jne       short 00419E4Dh
  00419e47  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00419e4d:
  00419e4d  5f                    pop       edi
  00419e4e  5e                    pop       esi
  00419e4f  5b                    pop       ebx
  00419e50  c3                    ret
  00419e51  90                    nop
  00419e52  90                    nop
  00419e53  90                    nop
  00419e54  90                    nop
  00419e55  90                    nop
  00419e56  90                    nop
  00419e57  90                    nop
  00419e58  90                    nop
  00419e59  90                    nop
  00419e5a  90                    nop
  00419e5b  90                    nop
  00419e5c  90                    nop
  00419e5d  90                    nop
  00419e5e  90                    nop
  00419e5f  90                    nop

5 cible(s) de saut interne : 0x00419d8a, 0x00419db7, 0x00419ddc, 0x00419ded, 0x00419e4d
