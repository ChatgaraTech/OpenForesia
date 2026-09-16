Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00453de0  section .text  offset fichier 0x531e0  128 octets

  00453de0  8b442404              mov       eax,[esp+4]
  00453de4  b901000000            mov       ecx,1
  00453de9  6683781000            cmp       word [eax+10h],0
  00453dee  7510                  jne       short 00453E00h
  00453df0  66894810              mov       [eax+10h],cx
  00453df4  66c740121800          mov       word [eax+12h],18h
  00453dfa  66c74014ffff          mov       word [eax+14h],0FFFFh

LAB_00453e00:
  00453e00  66394810              cmp       [eax+10h],cx
  00453e04  754d                  jne       short 00453E53h
  00453e06  668b4814              mov       cx,[eax+14h]
  00453e0a  66014812              add       [eax+12h],cx
  00453e0e  668b5012              mov       dx,[eax+12h]
  00453e12  66015034              add       [eax+34h],dx
  00453e16  6683fae8              cmp       dx,0FFE8h
  00453e1a  7406                  je        short 00453E22h
  00453e1c  6683fa18              cmp       dx,18h
  00453e20  7531                  jne       short 00453E53h

LAB_00453e22:
  00453e22  f7d9                  neg       ecx
  00453e24  66894814              mov       [eax+14h],cx
  00453e28  8b0dcc975e00          mov       ecx,[5E97CCh]
  00453e2e  6a00                  push      0
  00453e30  6a00                  push      0
  00453e32  8b91a8000000          mov       edx,[ecx+0A8h]
  00453e38  6800100000            push      1000h
  00453e3d  6800040000            push      400h
  00453e42  8b0a                  mov       ecx,[edx]
  00453e44  660fb65122            movzx     dx,byte [ecx+22h]
  00453e49  52                    push      edx
  00453e4a  50                    push      eax
  00453e4b  e830420100            call      00468080h
  00453e50  83c418                add       esp,18h

LAB_00453e53:
  00453e53  c3                    ret
