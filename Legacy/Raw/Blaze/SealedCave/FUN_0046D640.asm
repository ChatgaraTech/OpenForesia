Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046d640  section .text  offset fichier 0x6ca40  288 octets

  0046d640  56                    push      esi
  0046d641  8b742408              mov       esi,[esp+8]
  0046d645  0fbf4e16              movsx     ecx,word [esi+16h]
  0046d649  8bc1                  mov       eax,ecx
  0046d64b  99                    cdq
  0046d64c  83e207                and       edx,7
  0046d64f  03c2                  add       eax,edx
  0046d651  c1f803                sar       eax,3
  0046d654  8d14c500000000        lea       edx,[eax*8]
  0046d65b  8a80e0965e00          mov       al,[eax+5E96E0h]
  0046d661  2bca                  sub       ecx,edx
  0046d663  ba01000000            mov       edx,1
  0046d668  d3e2                  shl       edx,cl
  0046d66a  84d0                  test      al,dl
  0046d66c  0f84ec000000          je        near 0046D75Eh
  0046d672  66837e1000            cmp       word [esi+10h],0
  0046d677  7513                  jne       short 0046D68Ch
  0046d679  0fbf4e34              movsx     ecx,word [esi+34h]
  0046d67d  66c746100100          mov       word [esi+10h],1
  0046d683  66c746120500          mov       word [esi+12h],5
  0046d689  894e5c                mov       [esi+5Ch],ecx

LAB_0046d68c:
  0046d68c  66837e1001            cmp       word [esi+10h],1
  0046d691  7565                  jne       short 0046D6F8h
  0046d693  668b4612              mov       ax,[esi+12h]
  0046d697  8b4e5c                mov       ecx,[esi+5Ch]
  0046d69a  66294634              sub       [esi+34h],ax
  0046d69e  83c002                add       eax,2
  0046d6a1  66894612              mov       [esi+12h],ax
  0046d6a5  668b4614              mov       ax,[esi+14h]
  0046d6a9  0fbfd0                movsx     edx,ax
  0046d6ac  2bca                  sub       ecx,edx
  0046d6ae  0fbf5634              movsx     edx,word [esi+34h]
  0046d6b2  3bd1                  cmp       edx,ecx
  0046d6b4  7d42                  jge       short 0046D6F8h
  0046d6b6  668b4e5c              mov       cx,[esi+5Ch]
  0046d6ba  66c746100200          mov       word [esi+10h],2
  0046d6c0  662bc8                sub       cx,ax
  0046d6c3  66c746120800          mov       word [esi+12h],8
  0046d6c9  66894e34              mov       [esi+34h],cx
  0046d6cd  8b15cc975e00          mov       edx,[5E97CCh]
  0046d6d3  6a00                  push      0
  0046d6d5  6a00                  push      0
  0046d6d7  8b82a8000000          mov       eax,[edx+0A8h]
  0046d6dd  6800100000            push      1000h
  0046d6e2  6800040000            push      400h
  0046d6e7  8b08                  mov       ecx,[eax]
  0046d6e9  660fb65121            movzx     dx,byte [ecx+21h]
  0046d6ee  52                    push      edx
  0046d6ef  56                    push      esi
  0046d6f0  e88ba9ffff            call      00468080h
  0046d6f5  83c418                add       esp,18h

LAB_0046d6f8:
  0046d6f8  66837e1002            cmp       word [esi+10h],2
  0046d6fd  755f                  jne       short 0046D75Eh
  0046d6ff  0fbf4612              movsx     eax,word [esi+12h]
  0046d703  6a00                  push      0
  0046d705  6a00                  push      0
  0046d707  68e7030000            push      3E7h
  0046d70c  50                    push      eax
  0046d70d  68e7030000            push      3E7h
  0046d712  56                    push      esi
  0046d713  e848e7faff            call      0041BE60h
  0046d718  668b4612              mov       ax,[esi+12h]
  0046d71c  83c418                add       esp,18h
  0046d71f  66f7d8                neg       ax
  0046d722  6685c0                test      ax,ax
  0046d725  66894612              mov       [esi+12h],ax
  0046d729  7e03                  jle       short 0046D72Eh
  0046d72b  48                    dec       eax
  0046d72c  eb01                  jmp       short 0046D72Fh

LAB_0046d72e:
  0046d72e  40                    inc       eax

LAB_0046d72f:
  0046d72f  663d0100              cmp       ax,1
  0046d733  66894612              mov       [esi+12h],ax
  0046d737  7406                  je        short 0046D73Fh
  0046d739  663dffff              cmp       ax,0FFFFh
  0046d73d  751f                  jne       short 0046D75Eh

LAB_0046d73f:
  0046d73f  6a00                  push      0
  0046d741  6a00                  push      0
  0046d743  68e7030000            push      3E7h
  0046d748  6a00                  push      0
  0046d74a  68e7030000            push      3E7h
  0046d74f  56                    push      esi
  0046d750  e80be7faff            call      0041BE60h
  0046d755  83c418                add       esp,18h
  0046d758  66c746100300          mov       word [esi+10h],3

LAB_0046d75e:
  0046d75e  5e                    pop       esi
  0046d75f  c3                    ret

6 cible(s) de saut interne : 0x0046d68c, 0x0046d6f8, 0x0046d72e, 0x0046d72f, 0x0046d73f, 0x0046d75e
