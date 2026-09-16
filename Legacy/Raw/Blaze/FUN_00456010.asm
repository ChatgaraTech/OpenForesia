Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00456010  section .text  offset fichier 0x55410  160 octets

  00456010  53                    push      ebx
  00456011  56                    push      esi
  00456012  8b74240c              mov       esi,[esp+0Ch]
  00456016  56                    push      esi
  00456017  e8a4af0000            call      00460FC0h
  0045601c  83c404                add       esp,4
  0045601f  66837e1000            cmp       word [esi+10h],0
  00456024  751a                  jne       short 00456040h
  00456026  66c746100100          mov       word [esi+10h],1
  0045602c  e80f110f00            call      00547140h
  00456031  99                    cdq
  00456032  b90f000000            mov       ecx,0Fh
  00456037  f7f9                  idiv      ecx
  00456039  c1e208                shl       edx,8
  0045603c  66895612              mov       [esi+12h],dx

LAB_00456040:
  00456040  66837e1001            cmp       word [esi+10h],1
  00456045  7554                  jne       short 0045609Bh
  00456047  668146120001          add       word [esi+12h],100h
  0045604d  0fbf5612              movsx     edx,word [esi+12h]
  00456051  52                    push      edx
  00456052  e8c5f90e00            call      00545A1Ch
  00456057  8bd8                  mov       ebx,eax
  00456059  c1fb07                sar       ebx,7
  0045605c  e8df100f00            call      00547140h
  00456061  99                    cdq
  00456062  33c2                  xor       eax,edx
  00456064  2bc2                  sub       eax,edx
  00456066  83e00f                and       eax,0Fh
  00456069  33c2                  xor       eax,edx
  0045606b  2bc2                  sub       eax,edx
  0045606d  2ac3                  sub       al,bl
  0045606f  0478                  add       al,78h
  00456071  8844240c              mov       [esp+0Ch],al
  00456075  8b44240c              mov       eax,[esp+0Ch]
  00456079  25ff000000            and       eax,0FFh
  0045607e  8bc8                  mov       ecx,eax
  00456080  c1e108                shl       ecx,8
  00456083  0bc8                  or        ecx,eax
  00456085  c1e108                shl       ecx,8
  00456088  0bc8                  or        ecx,eax
  0045608a  66817e120010          cmp       word [esi+12h],1000h
  00456090  894e38                mov       [esi+38h],ecx
  00456093  7c06                  jl        short 0045609Bh
  00456095  66c746120000          mov       word [esi+12h],0

LAB_0045609b:
  0045609b  5e                    pop       esi
  0045609c  5b                    pop       ebx
  0045609d  c3                    ret
