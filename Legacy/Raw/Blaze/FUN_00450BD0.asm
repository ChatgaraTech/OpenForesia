Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00450bd0  section .text  offset fichier 0x4ffd0  130 octets

  00450bd0  53                    push      ebx
  00450bd1  56                    push      esi
  00450bd2  8b74240c              mov       esi,[esp+0Ch]
  00450bd6  56                    push      esi
  00450bd7  e8e4030100            call      00460FC0h
  00450bdc  83c404                add       esp,4
  00450bdf  66837e1000            cmp       word [esi+10h],0
  00450be4  b801000000            mov       eax,1
  00450be9  750a                  jne       short 00450BF5h
  00450beb  66894610              mov       [esi+10h],ax
  00450bef  66c746120000          mov       word [esi+12h],0

LAB_00450bf5:
  00450bf5  66394610              cmp       [esi+10h],ax
  00450bf9  7554                  jne       short 00450C4Fh
  00450bfb  668146120001          add       word [esi+12h],100h
  00450c01  0fbf4612              movsx     eax,word [esi+12h]
  00450c05  50                    push      eax
  00450c06  e8114e0f00            call      00545A1Ch
  00450c0b  8bd8                  mov       ebx,eax
  00450c0d  c1fb08                sar       ebx,8
  00450c10  e82b650f00            call      00547140h
  00450c15  99                    cdq
  00450c16  33c2                  xor       eax,edx
  00450c18  2bc2                  sub       eax,edx
  00450c1a  83e00f                and       eax,0Fh
  00450c1d  33c2                  xor       eax,edx
  00450c1f  2bc2                  sub       eax,edx
  00450c21  2ac3                  sub       al,bl
  00450c23  0478                  add       al,78h
  00450c25  8844240c              mov       [esp+0Ch],al
  00450c29  8b44240c              mov       eax,[esp+0Ch]
  00450c2d  25ff000000            and       eax,0FFh
  00450c32  8bc8                  mov       ecx,eax
  00450c34  c1e108                shl       ecx,8
  00450c37  0bc8                  or        ecx,eax
  00450c39  c1e108                shl       ecx,8
  00450c3c  0bc8                  or        ecx,eax
  00450c3e  66817e120010          cmp       word [esi+12h],1000h
  00450c44  894e38                mov       [esi+38h],ecx
  00450c47  7506                  jne       short 00450C4Fh
  00450c49  66c746120000          mov       word [esi+12h],0

LAB_00450c4f:
  00450c4f  5e                    pop       esi
  00450c50  5b                    pop       ebx
  00450c51  c3                    ret

2 cible(s) de saut interne : 0x00450bf5, 0x00450c4f
