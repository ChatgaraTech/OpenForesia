Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044a6a0  section .text  offset fichier 0x49aa0  256 octets

  0044a6a0  56                    push      esi
  0044a6a1  8b742408              mov       esi,[esp+8]
  0044a6a5  8b4678                mov       eax,[esi+78h]
  0044a6a8  85c0                  test      eax,eax
  0044a6aa  7505                  jne       short 0044A6B1h
  0044a6ac  b858e55d00            mov       eax,5DE558h

LAB_0044a6b1:
  0044a6b1  8b481c                mov       ecx,[eax+1Ch]
  0044a6b4  66837e1000            cmp       word [esi+10h],0
  0044a6b9  894e1c                mov       [esi+1Ch],ecx
  0044a6bc  8b5060                mov       edx,[eax+60h]
  0044a6bf  895620                mov       [esi+20h],edx
  0044a6c2  8b4024                mov       eax,[eax+24h]
  0044a6c5  894624                mov       [esi+24h],eax
  0044a6c8  b801000000            mov       eax,1
  0044a6cd  7535                  jne       short 0044A704h
  0044a6cf  b9a00f0000            mov       ecx,0FA0h
  0044a6d4  c6460426              mov       byte [esi+4],26h
  0044a6d8  c6460a03              mov       byte [esi+0Ah],3
  0044a6dc  88460b                mov       [esi+0Bh],al
  0044a6df  66894610              mov       [esi+10h],ax
  0044a6e3  66894e28              mov       [esi+28h],cx
  0044a6e7  66894e2c              mov       [esi+2Ch],cx
  0044a6eb  66c7462ab80b          mov       word [esi+2Ah],0BB8h
  0044a6f1  c6463800              mov       byte [esi+38h],0
  0044a6f5  c6463900              mov       byte [esi+39h],0
  0044a6f9  c6463a00              mov       byte [esi+3Ah],0
  0044a6fd  c7465c64000000        mov       dword [esi+5Ch],64h

LAB_0044a704:
  0044a704  668b4e5c              mov       cx,[esi+5Ch]
  0044a708  66014e32              add       [esi+32h],cx
  0044a70c  66394610              cmp       [esi+10h],ax
  0044a710  751b                  jne       short 0044A72Dh
  0044a712  50                    push      eax
  0044a713  6a0f                  push      0Fh
  0044a715  6a00                  push      0
  0044a717  56                    push      esi
  0044a718  e8c3a6fbff            call      00404DE0h
  0044a71d  8a4639                mov       al,[esi+39h]
  0044a720  83c410                add       esp,10h
  0044a723  3cc8                  cmp       al,0C8h
  0044a725  7206                  jb        short 0044A72Dh
  0044a727  66c746100200          mov       word [esi+10h],2

LAB_0044a72d:
  0044a72d  66837e1002            cmp       word [esi+10h],2
  0044a732  751c                  jne       short 0044A750h
  0044a734  6a0a                  push      0Ah
  0044a736  6af6                  push      0FFFFFFF6h
  0044a738  6a00                  push      0
  0044a73a  56                    push      esi
  0044a73b  e8a0a6fbff            call      00404DE0h
  0044a740  8a463a                mov       al,[esi+3Ah]
  0044a743  83c410                add       esp,10h
  0044a746  3cff                  cmp       al,0FFh
  0044a748  7506                  jne       short 0044A750h
  0044a74a  66c746100300          mov       word [esi+10h],3

LAB_0044a750:
  0044a750  66837e1003            cmp       word [esi+10h],3
  0044a755  753c                  jne       short 0044A793h
  0044a757  6af8                  push      0FFFFFFF8h
  0044a759  6af8                  push      0FFFFFFF8h
  0044a75b  6af8                  push      0FFFFFFF8h
  0044a75d  56                    push      esi
  0044a75e  e87da6fbff            call      00404DE0h
  0044a763  6683462ad8            add       word [esi+2Ah],0FFD8h
  0044a768  b828000000            mov       eax,28h
  0044a76d  66014628              add       [esi+28h],ax
  0044a771  6601462c              add       [esi+2Ch],ax
  0044a775  8a4638                mov       al,[esi+38h]
  0044a778  83c410                add       esp,10h
  0044a77b  84c0                  test      al,al
  0044a77d  7514                  jne       short 0044A793h
  0044a77f  8a4639                mov       al,[esi+39h]
  0044a782  84c0                  test      al,al
  0044a784  750d                  jne       short 0044A793h
  0044a786  8a463a                mov       al,[esi+3Ah]
  0044a789  84c0                  test      al,al
  0044a78b  7506                  jne       short 0044A793h
  0044a78d  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044a793:
  0044a793  5e                    pop       esi
  0044a794  c3                    ret
  0044a795  90                    nop
  0044a796  90                    nop
  0044a797  90                    nop
  0044a798  90                    nop
  0044a799  90                    nop
  0044a79a  90                    nop
  0044a79b  90                    nop
  0044a79c  90                    nop
  0044a79d  90                    nop
  0044a79e  90                    nop
  0044a79f  90                    nop

5 cible(s) de saut interne : 0x0044a6b1, 0x0044a704, 0x0044a72d, 0x0044a750, 0x0044a793
