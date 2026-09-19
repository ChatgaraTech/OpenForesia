Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042fa10  section .text  offset fichier 0x2ee10  320 octets

  0042fa10  56                    push      esi
  0042fa11  8b742408              mov       esi,[esp+8]
  0042fa15  66837e1000            cmp       word [esi+10h],0
  0042fa1a  0f859c000000          jne       near 0042FABCh
  0042fa20  66c746100100          mov       word [esi+10h],1
  0042fa26  c7463c700a5500        mov       dword [esi+3Ch],550A70h
  0042fa2d  c6464005              mov       byte [esi+40h],5
  0042fa31  e80a771100            call      00547140h
  0042fa36  99                    cdq
  0042fa37  33c2                  xor       eax,edx
  0042fa39  c6460b00              mov       byte [esi+0Bh],0
  0042fa3d  2bc2                  sub       eax,edx
  0042fa3f  83e001                and       eax,1
  0042fa42  33c2                  xor       eax,edx
  0042fa44  2bc2                  sub       eax,edx
  0042fa46  040e                  add       al,0Eh
  0042fa48  88460a                mov       [esi+0Ah],al
  0042fa4b  b800080000            mov       eax,800h
  0042fa50  66894628              mov       [esi+28h],ax
  0042fa54  6689462a              mov       [esi+2Ah],ax
  0042fa58  e8e3761100            call      00547140h
  0042fa5d  99                    cdq
  0042fa5e  b906000000            mov       ecx,6
  0042fa63  f7f9                  idiv      ecx
  0042fa65  83ea03                sub       edx,3
  0042fa68  895650                mov       [esi+50h],edx
  0042fa6b  e8d0761100            call      00547140h
  0042fa70  99                    cdq
  0042fa71  b906000000            mov       ecx,6
  0042fa76  f7f9                  idiv      ecx
  0042fa78  83ea03                sub       edx,3
  0042fa7b  895658                mov       [esi+58h],edx
  0042fa7e  e8bd761100            call      00547140h
  0042fa83  99                    cdq
  0042fa84  b906000000            mov       ecx,6
  0042fa89  66c746120100          mov       word [esi+12h],1
  0042fa8f  f7f9                  idiv      ecx
  0042fa91  83c203                add       edx,3
  0042fa94  895654                mov       [esi+54h],edx
  0042fa97  e8a4761100            call      00547140h
  0042fa9c  99                    cdq
  0042fa9d  b91e000000            mov       ecx,1Eh
  0042faa2  66c746160000          mov       word [esi+16h],0
  0042faa8  f7f9                  idiv      ecx
  0042faaa  b080                  mov       al,80h
  0042faac  884638                mov       [esi+38h],al
  0042faaf  884639                mov       [esi+39h],al
  0042fab2  88463a                mov       [esi+3Ah],al
  0042fab5  83c214                add       edx,14h
  0042fab8  66895614              mov       [esi+14h],dx

LAB_0042fabc:
  0042fabc  66837e1001            cmp       word [esi+10h],1
  0042fac1  0f8582000000          jne       near 0042FB49h
  0042fac7  8b5650                mov       edx,[esi+50h]
  0042faca  8b461c                mov       eax,[esi+1Ch]
  0042facd  8d4e1c                lea       ecx,[esi+1Ch]
  0042fad0  c1e20c                shl       edx,0Ch
  0042fad3  03c2                  add       eax,edx
  0042fad5  8b5624                mov       edx,[esi+24h]
  0042fad8  8901                  mov       [ecx],eax
  0042fada  8b4658                mov       eax,[esi+58h]
  0042fadd  c1e00c                shl       eax,0Ch
  0042fae0  03d0                  add       edx,eax
  0042fae2  8b4620                mov       eax,[esi+20h]
  0042fae5  895624                mov       [esi+24h],edx
  0042fae8  8b5654                mov       edx,[esi+54h]
  0042faeb  c1e20c                shl       edx,0Ch
  0042faee  2bc2                  sub       eax,edx
  0042faf0  66ff4616              inc       word [esi+16h]
  0042faf4  66837e1602            cmp       word [esi+16h],2
  0042faf9  894620                mov       [esi+20h],eax
  0042fafc  7c26                  jl        short 0042FB24h
  0042fafe  8a4612                mov       al,[esi+12h]
  0042fb01  8a5640                mov       dl,[esi+40h]
  0042fb04  02d0                  add       dl,al
  0042fb06  66c746160000          mov       word [esi+16h],0
  0042fb0c  8ac2                  mov       al,dl
  0042fb0e  885640                mov       [esi+40h],dl
  0042fb11  3c08                  cmp       al,8
  0042fb13  7404                  je        short 0042FB19h
  0042fb15  84c0                  test      al,al
  0042fb17  750b                  jne       short 0042FB24h

LAB_0042fb19:
  0042fb19  668b5612              mov       dx,[esi+12h]
  0042fb1d  66f7da                neg       dx
  0042fb20  66895612              mov       [esi+12h],dx

LAB_0042fb24:
  0042fb24  66837e1400            cmp       word [esi+14h],0
  0042fb29  7f1e                  jg        short 0042FB49h
  0042fb2b  8b06                  mov       eax,[esi]
  0042fb2d  6aff                  push      0FFFFFFFFh
  0042fb2f  6a00                  push      0
  0042fb31  68c3000000            push      0C3h
  0042fb36  6a00                  push      0
  0042fb38  25ffffff7f            and       eax,7FFFFFFFh
  0042fb3d  51                    push      ecx
  0042fb3e  56                    push      esi
  0042fb3f  8906                  mov       [esi],eax
  0042fb41  e8fab00400            call      0047AC40h
  0042fb46  83c418                add       esp,18h

LAB_0042fb49:
  0042fb49  66ff4e14              dec       word [esi+14h]
  0042fb4d  5e                    pop       esi
  0042fb4e  c3                    ret
  0042fb4f  90                    nop

4 cible(s) de saut interne : 0x0042fabc, 0x0042fb19, 0x0042fb24, 0x0042fb49
