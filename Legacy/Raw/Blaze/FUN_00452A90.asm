Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00452a90  section .text  offset fichier 0x51e90  160 octets

  00452a90  53                    push      ebx
  00452a91  56                    push      esi
  00452a92  8b74240c              mov       esi,[esp+0Ch]
  00452a96  56                    push      esi
  00452a97  e824e50000            call      00460FC0h
  00452a9c  83c404                add       esp,4
  00452a9f  66837e1000            cmp       word [esi+10h],0
  00452aa4  751a                  jne       short 00452AC0h
  00452aa6  66c746100100          mov       word [esi+10h],1
  00452aac  e88f460f00            call      00547140h
  00452ab1  99                    cdq
  00452ab2  b90f000000            mov       ecx,0Fh
  00452ab7  f7f9                  idiv      ecx
  00452ab9  c1e208                shl       edx,8
  00452abc  66895612              mov       [esi+12h],dx

LAB_00452ac0:
  00452ac0  66837e1001            cmp       word [esi+10h],1
  00452ac5  7554                  jne       short 00452B1Bh
  00452ac7  668146120001          add       word [esi+12h],100h
  00452acd  0fbf5612              movsx     edx,word [esi+12h]
  00452ad1  52                    push      edx
  00452ad2  e8452f0f00            call      00545A1Ch
  00452ad7  8bd8                  mov       ebx,eax
  00452ad9  c1fb07                sar       ebx,7
  00452adc  e85f460f00            call      00547140h
  00452ae1  99                    cdq
  00452ae2  33c2                  xor       eax,edx
  00452ae4  2bc2                  sub       eax,edx
  00452ae6  83e00f                and       eax,0Fh
  00452ae9  33c2                  xor       eax,edx
  00452aeb  2bc2                  sub       eax,edx
  00452aed  2ac3                  sub       al,bl
  00452aef  0478                  add       al,78h
  00452af1  8844240c              mov       [esp+0Ch],al
  00452af5  8b44240c              mov       eax,[esp+0Ch]
  00452af9  25ff000000            and       eax,0FFh
  00452afe  8bc8                  mov       ecx,eax
  00452b00  c1e108                shl       ecx,8
  00452b03  0bc8                  or        ecx,eax
  00452b05  c1e108                shl       ecx,8
  00452b08  0bc8                  or        ecx,eax
  00452b0a  66817e120010          cmp       word [esi+12h],1000h
  00452b10  894e38                mov       [esi+38h],ecx
  00452b13  7c06                  jl        short 00452B1Bh
  00452b15  66c746120000          mov       word [esi+12h],0

LAB_00452b1b:
  00452b1b  5e                    pop       esi
  00452b1c  5b                    pop       ebx
  00452b1d  c3                    ret
