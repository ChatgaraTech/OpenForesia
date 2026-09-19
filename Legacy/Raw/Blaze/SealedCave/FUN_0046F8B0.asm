Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046f8b0  section .text  offset fichier 0x6ecb0  288 octets

  0046f8b0  56                    push      esi
  0046f8b1  8b742408              mov       esi,[esp+8]
  0046f8b5  0fbf4e16              movsx     ecx,word [esi+16h]
  0046f8b9  8bc1                  mov       eax,ecx
  0046f8bb  99                    cdq
  0046f8bc  83e207                and       edx,7
  0046f8bf  03c2                  add       eax,edx
  0046f8c1  c1f803                sar       eax,3
  0046f8c4  8d14c500000000        lea       edx,[eax*8]
  0046f8cb  8a80e0965e00          mov       al,[eax+5E96E0h]
  0046f8d1  2bca                  sub       ecx,edx
  0046f8d3  ba01000000            mov       edx,1
  0046f8d8  d3e2                  shl       edx,cl
  0046f8da  84d0                  test      al,dl
  0046f8dc  0f84ec000000          je        near 0046F9CEh
  0046f8e2  66837e1000            cmp       word [esi+10h],0
  0046f8e7  7513                  jne       short 0046F8FCh
  0046f8e9  0fbf4e34              movsx     ecx,word [esi+34h]
  0046f8ed  66c746100100          mov       word [esi+10h],1
  0046f8f3  66c746120500          mov       word [esi+12h],5
  0046f8f9  894e5c                mov       [esi+5Ch],ecx

LAB_0046f8fc:
  0046f8fc  66837e1001            cmp       word [esi+10h],1
  0046f901  7565                  jne       short 0046F968h
  0046f903  668b4612              mov       ax,[esi+12h]
  0046f907  8b4e5c                mov       ecx,[esi+5Ch]
  0046f90a  66294634              sub       [esi+34h],ax
  0046f90e  83c002                add       eax,2
  0046f911  66894612              mov       [esi+12h],ax
  0046f915  668b4614              mov       ax,[esi+14h]
  0046f919  0fbfd0                movsx     edx,ax
  0046f91c  2bca                  sub       ecx,edx
  0046f91e  0fbf5634              movsx     edx,word [esi+34h]
  0046f922  3bd1                  cmp       edx,ecx
  0046f924  7d42                  jge       short 0046F968h
  0046f926  668b4e5c              mov       cx,[esi+5Ch]
  0046f92a  66c746100200          mov       word [esi+10h],2
  0046f930  662bc8                sub       cx,ax
  0046f933  66c746120800          mov       word [esi+12h],8
  0046f939  66894e34              mov       [esi+34h],cx
  0046f93d  8b15cc975e00          mov       edx,[5E97CCh]
  0046f943  6a00                  push      0
  0046f945  6a00                  push      0
  0046f947  8b82a8000000          mov       eax,[edx+0A8h]
  0046f94d  6800150000            push      1500h
  0046f952  6800040000            push      400h
  0046f957  8b08                  mov       ecx,[eax]
  0046f959  660fb65121            movzx     dx,byte [ecx+21h]
  0046f95e  52                    push      edx
  0046f95f  56                    push      esi
  0046f960  e81b87ffff            call      00468080h
  0046f965  83c418                add       esp,18h

LAB_0046f968:
  0046f968  66837e1002            cmp       word [esi+10h],2
  0046f96d  755f                  jne       short 0046F9CEh
  0046f96f  0fbf4612              movsx     eax,word [esi+12h]
  0046f973  6a00                  push      0
  0046f975  6a00                  push      0
  0046f977  68e7030000            push      3E7h
  0046f97c  50                    push      eax
  0046f97d  68e7030000            push      3E7h
  0046f982  56                    push      esi
  0046f983  e8d8c4faff            call      0041BE60h
  0046f988  668b4612              mov       ax,[esi+12h]
  0046f98c  83c418                add       esp,18h
  0046f98f  66f7d8                neg       ax
  0046f992  6685c0                test      ax,ax
  0046f995  66894612              mov       [esi+12h],ax
  0046f999  7e03                  jle       short 0046F99Eh
  0046f99b  48                    dec       eax
  0046f99c  eb01                  jmp       short 0046F99Fh

LAB_0046f99e:
  0046f99e  40                    inc       eax

LAB_0046f99f:
  0046f99f  663d0100              cmp       ax,1
  0046f9a3  66894612              mov       [esi+12h],ax
  0046f9a7  7406                  je        short 0046F9AFh
  0046f9a9  663dffff              cmp       ax,0FFFFh
  0046f9ad  751f                  jne       short 0046F9CEh

LAB_0046f9af:
  0046f9af  6a00                  push      0
  0046f9b1  6a00                  push      0
  0046f9b3  68e7030000            push      3E7h
  0046f9b8  6a00                  push      0
  0046f9ba  68e7030000            push      3E7h
  0046f9bf  56                    push      esi
  0046f9c0  e89bc4faff            call      0041BE60h
  0046f9c5  83c418                add       esp,18h
  0046f9c8  66c746100300          mov       word [esi+10h],3

LAB_0046f9ce:
  0046f9ce  5e                    pop       esi
  0046f9cf  c3                    ret

6 cible(s) de saut interne : 0x0046f8fc, 0x0046f968, 0x0046f99e, 0x0046f99f, 0x0046f9af, 0x0046f9ce
