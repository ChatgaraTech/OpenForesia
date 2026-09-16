Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00454e10  section .text  offset fichier 0x54210  188 octets

  00454e10  a070155700            mov       al,[571570h]
  00454e15  56                    push      esi
  00454e16  8b742408              mov       esi,[esp+8]
  00454e1a  a804                  test      al,4
  00454e1c  750b                  jne       short 00454E29h
  00454e1e  66837e1000            cmp       word [esi+10h],0
  00454e23  0f84a1000000          je        near 00454ECAh

LAB_00454e29:
  00454e29  66837e1000            cmp       word [esi+10h],0
  00454e2e  7545                  jne       short 00454E75h
  00454e30  8b0d70155700          mov       ecx,[571570h]
  00454e36  6a00                  push      0
  00454e38  83e1fb                and       ecx,0FFFFFFFBh
  00454e3b  6a00                  push      0
  00454e3d  890d70155700          mov       [571570h],ecx
  00454e43  66c746100100          mov       word [esi+10h],1
  00454e49  66c746120001          mov       word [esi+12h],100h
  00454e4f  a1cc975e00            mov       eax,[5E97CCh]
  00454e54  6800100000            push      1000h
  00454e59  6800040000            push      400h
  00454e5e  8b88a8000000          mov       ecx,[eax+0A8h]
  00454e64  8b11                  mov       edx,[ecx]
  00454e66  660fb6421a            movzx     ax,byte [edx+1Ah]
  00454e6b  50                    push      eax
  00454e6c  56                    push      esi
  00454e6d  e80e320100            call      00468080h
  00454e72  83c418                add       esp,18h

LAB_00454e75:
  00454e75  66837e1001            cmp       word [esi+10h],1
  00454e7a  ba02000000            mov       edx,2
  00454e7f  b940010000            mov       ecx,140h
  00454e84  751f                  jne       short 00454EA5h
  00454e86  668b4612              mov       ax,[esi+12h]
  00454e8a  66294634              sub       [esi+34h],ax
  00454e8e  83c0e0                add       eax,0FFFFFFE0h
  00454e91  66394e34              cmp       [esi+34h],cx
  00454e95  66894612              mov       [esi+12h],ax
  00454e99  7c0a                  jl        short 00454EA5h
  00454e9b  66895610              mov       [esi+10h],dx
  00454e9f  66c74612baff          mov       word [esi+12h],0FFBAh

LAB_00454ea5:
  00454ea5  66395610              cmp       [esi+10h],dx
  00454ea9  751f                  jne       short 00454ECAh
  00454eab  668b4612              mov       ax,[esi+12h]
  00454eaf  66294634              sub       [esi+34h],ax
  00454eb3  83c010                add       eax,10h
  00454eb6  66394e34              cmp       [esi+34h],cx
  00454eba  66894612              mov       [esi+12h],ax
  00454ebe  7f0a                  jg        short 00454ECAh
  00454ec0  66894e34              mov       [esi+34h],cx
  00454ec4  66c746100300          mov       word [esi+10h],3

LAB_00454eca:
  00454eca  5e                    pop       esi
  00454ecb  c3                    ret

4 cible(s) de saut interne : 0x00454e29, 0x00454e75, 0x00454ea5, 0x00454eca
