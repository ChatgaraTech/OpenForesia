Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00467e80  section .text  offset fichier 0x67280  512 octets

  00467e80  56                    push      esi
  00467e81  8b742408              mov       esi,[esp+8]
  00467e85  57                    push      edi
  00467e86  8b3e                  mov       edi,[esi]
  00467e88  f7c700000040          test      edi,40000000h
  00467e8e  743b                  je        short 00467ECBh
  00467e90  0fbf4e14              movsx     ecx,word [esi+14h]
  00467e94  8bc1                  mov       eax,ecx
  00467e96  99                    cdq
  00467e97  83e207                and       edx,7
  00467e9a  03c2                  add       eax,edx
  00467e9c  c1f803                sar       eax,3
  00467e9f  0fbfc0                movsx     eax,ax
  00467ea2  8d14c500000000        lea       edx,[eax*8]
  00467ea9  8a80e0965e00          mov       al,[eax+5E96E0h]
  00467eaf  2bca                  sub       ecx,edx
  00467eb1  ba01000000            mov       edx,1
  00467eb6  d3e2                  shl       edx,cl
  00467eb8  84d0                  test      al,dl
  00467eba  0f84b2010000          je        near 00468072h
  00467ec0  81e7ffffff7f          and       edi,7FFFFFFFh
  00467ec6  893e                  mov       [esi],edi
  00467ec8  5f                    pop       edi
  00467ec9  5e                    pop       esi
  00467eca  c3                    ret

LAB_00467ecb:
  00467ecb  66837e1000            cmp       word [esi+10h],0
  00467ed0  7546                  jne       short 00467F18h
  00467ed2  0fbf4e14              movsx     ecx,word [esi+14h]
  00467ed6  8bc1                  mov       eax,ecx
  00467ed8  66c746100100          mov       word [esi+10h],1
  00467ede  99                    cdq
  00467edf  83e207                and       edx,7
  00467ee2  66c746120000          mov       word [esi+12h],0
  00467ee8  03c2                  add       eax,edx
  00467eea  c7465c00000000        mov       dword [esi+5Ch],0
  00467ef1  c1f803                sar       eax,3
  00467ef4  0fbfc0                movsx     eax,ax
  00467ef7  8d14c500000000        lea       edx,[eax*8]
  00467efe  2bca                  sub       ecx,edx
  00467f00  b201                  mov       dl,1
  00467f02  d2e2                  shl       dl,cl
  00467f04  8a88e0965e00          mov       cl,[eax+5E96E0h]
  00467f0a  0aca                  or        cl,dl
  00467f0c  8888e0965e00          mov       [eax+5E96E0h],cl
  00467f12  66c746140000          mov       word [esi+14h],0

LAB_00467f18:
  00467f18  66837e1001            cmp       word [esi+10h],1
  00467f1d  0f85b7000000          jne       near 00467FDAh
  00467f23  e818f20d00            call      00547140h
  00467f28  99                    cdq
  00467f29  b914000000            mov       ecx,14h
  00467f2e  f7f9                  idiv      ecx
  00467f30  66ff4612              inc       word [esi+12h]
  00467f34  668b4612              mov       ax,[esi+12h]
  00467f38  83fa01                cmp       edx,1
  00467f3b  7506                  jne       short 00467F43h
  00467f3d  663d0a00              cmp       ax,0Ah
  00467f41  7f0a                  jg        short 00467F4Dh

LAB_00467f43:
  00467f43  663d1e00              cmp       ax,1Eh
  00467f47  0f858d000000          jne       near 00467FDAh

LAB_00467f4d:
  00467f4d  66c746100200          mov       word [esi+10h],2
  00467f53  66c746120500          mov       word [esi+12h],5
  00467f59  e8e2f10d00            call      00547140h
  00467f5e  99                    cdq
  00467f5f  b978000000            mov       ecx,78h
  00467f64  f7f9                  idiv      ecx
  00467f66  83ea3c                sub       edx,3Ch
  00467f69  66895614              mov       [esi+14h],dx
  00467f6d  e8cef10d00            call      00547140h
  00467f72  99                    cdq
  00467f73  b932000000            mov       ecx,32h
  00467f78  6a00                  push      0
  00467f7a  f7f9                  idiv      ecx
  00467f7c  6a00                  push      0
  00467f7e  6800180000            push      1800h
  00467f83  6800040000            push      400h
  00467f88  83ea19                sub       edx,19h
  00467f8b  66895616              mov       [esi+16h],dx
  00467f8f  8b15cc975e00          mov       edx,[5E97CCh]
  00467f95  8b82a8000000          mov       eax,[edx+0A8h]
  00467f9b  8b08                  mov       ecx,[eax]
  00467f9d  660fb65113            movzx     dx,byte [ecx+13h]
  00467fa2  52                    push      edx
  00467fa3  56                    push      esi
  00467fa4  e8d7000000            call      00468080h
  00467fa9  83c418                add       esp,18h
  00467fac  e88ff10d00            call      00547140h
  00467fb1  99                    cdq
  00467fb2  b905000000            mov       ecx,5
  00467fb7  f7f9                  idiv      ecx
  00467fb9  03d1                  add       edx,ecx
  00467fbb  6685d2                test      dx,dx
  00467fbe  7c1a                  jl        short 00467FDAh
  00467fc0  0fbffa                movsx     edi,dx
  00467fc3  47                    inc       edi

LAB_00467fc4:
  00467fc4  6a00                  push      0
  00467fc6  6a00                  push      0
  00467fc8  6aff                  push      0FFFFFFFFh
  00467fca  6a00                  push      0
  00467fcc  6a31                  push      31h
  00467fce  56                    push      esi
  00467fcf  e83c010000            call      00468110h
  00467fd4  83c418                add       esp,18h
  00467fd7  4f                    dec       edi
  00467fd8  75ea                  jne       short 00467FC4h

LAB_00467fda:
  00467fda  66837e1002            cmp       word [esi+10h],2
  00467fdf  0f858d000000          jne       near 00468072h
  00467fe5  668b4612              mov       ax,[esi+12h]
  00467fe9  8b7e20                mov       edi,[esi+20h]
  00467fec  668b4e14              mov       cx,[esi+14h]
  00467ff0  0fbfd0                movsx     edx,ax
  00467ff3  66014e34              add       [esi+34h],cx
  00467ff7  83c002                add       eax,2
  00467ffa  c1e20c                shl       edx,0Ch
  00467ffd  2bfa                  sub       edi,edx
  00467fff  668b5616              mov       dx,[esi+16h]
  00468003  66015632              add       [esi+32h],dx
  00468007  66894612              mov       [esi+12h],ax
  0046800b  8bc7                  mov       eax,edi
  0046800d  897e20                mov       [esi+20h],edi
  00468010  85c0                  test      eax,eax
  00468012  7d4f                  jge       short 00468063h
  00468014  8b0e                  mov       ecx,[esi]
  00468016  8b465c                mov       eax,[esi+5Ch]
  00468019  80cd20                or        ch,20h
  0046801c  c6463850              mov       byte [esi+38h],50h
  00468020  85c0                  test      eax,eax
  00468022  c6463914              mov       byte [esi+39h],14h
  00468026  c6463a96              mov       byte [esi+3Ah],96h
  0046802a  890e                  mov       [esi],ecx
  0046802c  7535                  jne       short 00468063h
  0046802e  c7465c01000000        mov       dword [esi+5Ch],1
  00468035  e806f10d00            call      00547140h
  0046803a  99                    cdq
  0046803b  b905000000            mov       ecx,5
  00468040  f7f9                  idiv      ecx
  00468042  03d1                  add       edx,ecx
  00468044  6685d2                test      dx,dx
  00468047  7c1a                  jl        short 00468063h
  00468049  0fbffa                movsx     edi,dx
  0046804c  47                    inc       edi

LAB_0046804d:
  0046804d  6a00                  push      0
  0046804f  6a00                  push      0
  00468051  6aff                  push      0FFFFFFFFh
  00468053  6a00                  push      0
  00468055  6a32                  push      32h
  00468057  56                    push      esi
  00468058  e8b3000000            call      00468110h
  0046805d  83c418                add       esp,18h
  00468060  4f                    dec       edi
  00468061  75ea                  jne       short 0046804Dh

LAB_00468063:
  00468063  817e2000c0e0ff        cmp       dword [esi+20h],0FFE0C000h
  0046806a  7d06                  jge       short 00468072h
  0046806c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00468072:
  00468072  5f                    pop       edi
  00468073  5e                    pop       esi
  00468074  c3                    ret
  00468075  90                    nop
  00468076  90                    nop
  00468077  90                    nop
  00468078  90                    nop
  00468079  90                    nop
  0046807a  90                    nop
  0046807b  90                    nop
  0046807c  90                    nop
  0046807d  90                    nop
  0046807e  90                    nop
  0046807f  90                    nop

9 cible(s) de saut interne : 0x00467ecb, 0x00467f18, 0x00467f43, 0x00467f4d, 0x00467fc4, 0x00467fda, 0x0046804d, 0x00468063, 0x00468072
