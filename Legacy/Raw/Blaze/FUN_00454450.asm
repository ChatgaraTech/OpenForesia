Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00454450  section .text  offset fichier 0x53850  160 octets

  00454450  53                    push      ebx
  00454451  56                    push      esi
  00454452  8b74240c              mov       esi,[esp+0Ch]
  00454456  56                    push      esi
  00454457  e864cb0000            call      00460FC0h
  0045445c  83c404                add       esp,4
  0045445f  66837e1000            cmp       word [esi+10h],0
  00454464  751a                  jne       short 00454480h
  00454466  66c746100100          mov       word [esi+10h],1
  0045446c  e8cf2c0f00            call      00547140h
  00454471  99                    cdq
  00454472  b90f000000            mov       ecx,0Fh
  00454477  f7f9                  idiv      ecx
  00454479  c1e208                shl       edx,8
  0045447c  66895612              mov       [esi+12h],dx

LAB_00454480:
  00454480  66837e1001            cmp       word [esi+10h],1
  00454485  7554                  jne       short 004544DBh
  00454487  668146120001          add       word [esi+12h],100h
  0045448d  0fbf5612              movsx     edx,word [esi+12h]
  00454491  52                    push      edx
  00454492  e885150f00            call      00545A1Ch
  00454497  8bd8                  mov       ebx,eax
  00454499  c1fb07                sar       ebx,7
  0045449c  e89f2c0f00            call      00547140h
  004544a1  99                    cdq
  004544a2  33c2                  xor       eax,edx
  004544a4  2bc2                  sub       eax,edx
  004544a6  83e00f                and       eax,0Fh
  004544a9  33c2                  xor       eax,edx
  004544ab  2bc2                  sub       eax,edx
  004544ad  2ac3                  sub       al,bl
  004544af  0478                  add       al,78h
  004544b1  8844240c              mov       [esp+0Ch],al
  004544b5  8b44240c              mov       eax,[esp+0Ch]
  004544b9  25ff000000            and       eax,0FFh
  004544be  8bc8                  mov       ecx,eax
  004544c0  c1e108                shl       ecx,8
  004544c3  0bc8                  or        ecx,eax
  004544c5  c1e108                shl       ecx,8
  004544c8  0bc8                  or        ecx,eax
  004544ca  66817e120010          cmp       word [esi+12h],1000h
  004544d0  894e38                mov       [esi+38h],ecx
  004544d3  7c06                  jl        short 004544DBh
  004544d5  66c746120000          mov       word [esi+12h],0

LAB_004544db:
  004544db  5e                    pop       esi
  004544dc  5b                    pop       ebx
  004544dd  c3                    ret
