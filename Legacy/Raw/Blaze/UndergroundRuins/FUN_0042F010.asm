Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042f010  section .text  offset fichier 0x2e410  300 octets

  0042f010  53                    push      ebx
  0042f011  56                    push      esi
  0042f012  8b74240c              mov       esi,[esp+0Ch]
  0042f016  33db                  xor       ebx,ebx
  0042f018  3bf3                  cmp       esi,ebx
  0042f01a  7505                  jne       short 0042F021h
  0042f01c  be58e55d00            mov       esi,5DE558h

LAB_0042f021:
  0042f021  8b4648                mov       eax,[esi+48h]
  0042f024  83f80a                cmp       eax,0Ah
  0042f027  7c0c                  jl        short 0042F035h
  0042f029  48                    dec       eax
  0042f02a  83f809                cmp       eax,9
  0042f02d  894648                mov       [esi+48h],eax
  0042f030  7f03                  jg        short 0042F035h
  0042f032  895e48                mov       [esi+48h],ebx

LAB_0042f035:
  0042f035  395e48                cmp       [esi+48h],ebx
  0042f038  7516                  jne       short 0042F050h
  0042f03a  668b442410            mov       ax,[esp+10h]
  0042f03f  c7464801000000        mov       dword [esi+48h],1
  0042f046  66c7461affff          mov       word [esi+1Ah],0FFFFh
  0042f04c  66894618              mov       [esi+18h],ax

LAB_0042f050:
  0042f050  837e4801              cmp       dword [esi+48h],1
  0042f054  0f85c4000000          jne       near 0042F11Eh
  0042f05a  0fbf5618              movsx     edx,word [esi+18h]
  0042f05e  33c9                  xor       ecx,ecx
  0042f060  8a4e04                mov       cl,[esi+4]
  0042f063  8b048d40965e00        mov       eax,[ecx*4+5E9640h]
  0042f06a  8b0490                mov       eax,[eax+edx*4]
  0042f06d  66ff461a              inc       word [esi+1Ah]
  0042f071  668b4e1a              mov       cx,[esi+1Ah]
  0042f075  668b4016              mov       ax,[eax+16h]
  0042f079  8bd0                  mov       edx,eax
  0042f07b  81e2ffff0000          and       edx,0FFFFh
  0042f081  0fbfc9                movsx     ecx,cx
  0042f084  4a                    dec       edx
  0042f085  3bca                  cmp       ecx,edx
  0042f087  0f8c91000000          jl        near 0042F11Eh
  0042f08d  395e4c                cmp       [esi+4Ch],ebx
  0042f090  7542                  jne       short 0042F0D4h
  0042f092  8b4e5c                mov       ecx,[esi+5Ch]
  0042f095  8b4644                mov       eax,[esi+44h]
  0042f098  81c900000080          or        ecx,80000000h
  0042f09e  3bc3                  cmp       eax,ebx
  0042f0a0  66895e1a              mov       [esi+1Ah],bx
  0042f0a4  894e5c                mov       [esi+5Ch],ecx
  0042f0a7  7475                  je        short 0042F11Eh
  0042f0a9  8b0dcc975e00          mov       ecx,[5E97CCh]
  0042f0af  8bd0                  mov       edx,eax
  0042f0b1  8b4670                mov       eax,[esi+70h]
  0042f0b4  52                    push      edx
  0042f0b5  50                    push      eax
  0042f0b6  51                    push      ecx
  0042f0b7  e8144d0e00            call      00513DD0h
  0042f0bc  8b465c                mov       eax,[esi+5Ch]
  0042f0bf  83c40c                add       esp,0Ch
  0042f0c2  0d00000080            or        eax,80000000h
  0042f0c7  895e44                mov       [esi+44h],ebx
  0042f0ca  89465c                mov       [esi+5Ch],eax
  0042f0cd  66b80100              mov       ax,1
  0042f0d1  5e                    pop       esi
  0042f0d2  5b                    pop       ebx
  0042f0d3  c3                    ret

LAB_0042f0d4:
  0042f0d4  8b4e5c                mov       ecx,[esi+5Ch]
  0042f0d7  48                    dec       eax
  0042f0d8  6689461a              mov       [esi+1Ah],ax
  0042f0dc  8b4644                mov       eax,[esi+44h]
  0042f0df  81c900000080          or        ecx,80000000h
  0042f0e5  3bc3                  cmp       eax,ebx
  0042f0e7  894e5c                mov       [esi+5Ch],ecx
  0042f0ea  742b                  je        short 0042F117h
  0042f0ec  8b0dcc975e00          mov       ecx,[5E97CCh]
  0042f0f2  8bd0                  mov       edx,eax
  0042f0f4  8b4670                mov       eax,[esi+70h]
  0042f0f7  52                    push      edx
  0042f0f8  50                    push      eax
  0042f0f9  51                    push      ecx
  0042f0fa  e8d14c0e00            call      00513DD0h
  0042f0ff  8b465c                mov       eax,[esi+5Ch]
  0042f102  83c40c                add       esp,0Ch
  0042f105  0d00000080            or        eax,80000000h
  0042f10a  895e44                mov       [esi+44h],ebx
  0042f10d  89465c                mov       [esi+5Ch],eax
  0042f110  66b80100              mov       ax,1
  0042f114  5e                    pop       esi
  0042f115  5b                    pop       ebx
  0042f116  c3                    ret

LAB_0042f117:
  0042f117  c7464802000000        mov       dword [esi+48h],2

LAB_0042f11e:
  0042f11e  5e                    pop       esi
  0042f11f  6633c0                xor       ax,ax
  0042f122  5b                    pop       ebx
  0042f123  c3                    ret
  0042f124  90                    nop
  0042f125  90                    nop
  0042f126  90                    nop
  0042f127  90                    nop
  0042f128  90                    nop
  0042f129  90                    nop
  0042f12a  90                    nop
  0042f12b  90                    nop
  0042f12c  90                    nop
  0042f12d  90                    nop
  0042f12e  90                    nop
  0042f12f  90                    nop
  0042f130  56                    push      esi
  0042f131  8b742408              mov       esi,[esp+8]
  0042f135  85f6                  test      esi,esi
  0042f137  7505                  jne       short 0042F13Eh
  0042f139  be58e5                (bad)
  (octet invalide - la plage demandee deborde probablement du code)
