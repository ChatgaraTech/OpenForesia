Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040ebe0  section .text  offset fichier 0xdfe0  528 octets

  0040ebe0  53                    push      ebx
  0040ebe1  56                    push      esi
  0040ebe2  8b74240c              mov       esi,[esp+0Ch]
  0040ebe6  33db                  xor       ebx,ebx
  0040ebe8  57                    push      edi
  0040ebe9  66395e10              cmp       [esi+10h],bx
  0040ebed  0f85bf000000          jne       near 0040ECB2h
  0040ebf3  8b4e20                mov       ecx,[esi+20h]
  0040ebf6  8b461c                mov       eax,[esi+1Ch]
  0040ebf9  8b5624                mov       edx,[esi+24h]
  0040ebfc  81c100200300          add       ecx,32000h
  0040ec02  66c746100100          mov       word [esi+10h],1
  0040ec08  c7463c60f65400        mov       dword [esi+3Ch],54F660h
  0040ec0f  894644                mov       [esi+44h],eax
  0040ec12  894e48                mov       [esi+48h],ecx
  0040ec15  89564c                mov       [esi+4Ch],edx
  0040ec18  e823851300            call      00547140h
  0040ec1d  99                    cdq
  0040ec1e  b9d0070000            mov       ecx,7D0h
  0040ec23  f7f9                  idiv      ecx
  0040ec25  81c2180c0000          add       edx,0C18h
  0040ec2b  66895628              mov       [esi+28h],dx
  0040ec2f  e80c851300            call      00547140h
  0040ec34  99                    cdq
  0040ec35  b9d0070000            mov       ecx,7D0h
  0040ec3a  f7f9                  idiv      ecx
  0040ec3c  81c2180c0000          add       edx,0C18h
  0040ec42  6689562a              mov       [esi+2Ah],dx
  0040ec46  e8f5841300            call      00547140h
  0040ec4b  99                    cdq
  0040ec4c  b918000000            mov       ecx,18h
  0040ec51  f7f9                  idiv      ecx
  0040ec53  83ea0c                sub       edx,0Ch
  0040ec56  c1e20c                shl       edx,0Ch
  0040ec59  895650                mov       [esi+50h],edx
  0040ec5c  e8df841300            call      00547140h
  0040ec61  99                    cdq
  0040ec62  33c2                  xor       eax,edx
  0040ec64  2bc2                  sub       eax,edx
  0040ec66  83e007                and       eax,7
  0040ec69  33c2                  xor       eax,edx
  0040ec6b  2bc2                  sub       eax,edx
  0040ec6d  83c006                add       eax,6
  0040ec70  c1e00c                shl       eax,0Ch
  0040ec73  894654                mov       [esi+54h],eax
  0040ec76  e8c5841300            call      00547140h
  0040ec7b  99                    cdq
  0040ec7c  b918000000            mov       ecx,18h
  0040ec81  c6460a04              mov       byte [esi+0Ah],4
  0040ec85  f7f9                  idiv      ecx
  0040ec87  885e0b                mov       [esi+0Bh],bl
  0040ec8a  66895e12              mov       [esi+12h],bx
  0040ec8e  83ea0c                sub       edx,0Ch
  0040ec91  c1e20c                shl       edx,0Ch
  0040ec94  895658                mov       [esi+58h],edx
  0040ec97  e8a4841300            call      00547140h
  0040ec9c  99                    cdq
  0040ec9d  b91e000000            mov       ecx,1Eh
  0040eca2  66895e16              mov       [esi+16h],bx
  0040eca6  f7f9                  idiv      ecx
  0040eca8  885e40                mov       [esi+40h],bl
  0040ecab  83c20a                add       edx,0Ah
  0040ecae  66895614              mov       [esi+14h],dx

LAB_0040ecb2:
  0040ecb2  8b4644                mov       eax,[esi+44h]
  0040ecb5  8b561c                mov       edx,[esi+1Ch]
  0040ecb8  8b4e48                mov       ecx,[esi+48h]
  0040ecbb  8b7e24                mov       edi,[esi+24h]
  0040ecbe  c1f80c                sar       eax,0Ch
  0040ecc1  c1fa0c                sar       edx,0Ch
  0040ecc4  2bc2                  sub       eax,edx
  0040ecc6  8b5620                mov       edx,[esi+20h]
  0040ecc9  c1f90c                sar       ecx,0Ch
  0040eccc  c1fa0c                sar       edx,0Ch
  0040eccf  2bca                  sub       ecx,edx
  0040ecd1  8b564c                mov       edx,[esi+4Ch]
  0040ecd4  c1fa0c                sar       edx,0Ch
  0040ecd7  c1ff0c                sar       edi,0Ch
  0040ecda  2bd7                  sub       edx,edi
  0040ecdc  8bf8                  mov       edi,eax
  0040ecde  0faff8                imul      edi,eax
  0040ece1  8bc1                  mov       eax,ecx
  0040ece3  0fafc1                imul      eax,ecx
  0040ece6  8bca                  mov       ecx,edx
  0040ece8  03f8                  add       edi,eax
  0040ecea  0fafca                imul      ecx,edx
  0040eced  03f9                  add       edi,ecx
  0040ecef  57                    push      edi
  0040ecf0  e81b6d1300            call      00545A10h
  0040ecf5  83f878                cmp       eax,78h
  0040ecf8  bf02000000            mov       edi,2
  0040ecfd  7e13                  jle       short 0040ED12h
  0040ecff  66837e1001            cmp       word [esi+10h],1
  0040ed04  0f8588000000          jne       near 0040ED92h
  0040ed0a  66897e10              mov       [esi+10h],di
  0040ed0e  66895e12              mov       [esi+12h],bx

LAB_0040ed12:
  0040ed12  66837e1001            cmp       word [esi+10h],1
  0040ed17  7579                  jne       short 0040ED92h
  0040ed19  e822841300            call      00547140h
  0040ed1e  99                    cdq
  0040ed1f  b90a000000            mov       ecx,0Ah
  0040ed24  f7f9                  idiv      ecx
  0040ed26  8b4650                mov       eax,[esi+50h]
  0040ed29  8d4c02fb              lea       ecx,[edx+eax-5]
  0040ed2d  8b561c                mov       edx,[esi+1Ch]
  0040ed30  03d1                  add       edx,ecx
  0040ed32  89561c                mov       [esi+1Ch],edx
  0040ed35  e806841300            call      00547140h
  0040ed3a  99                    cdq
  0040ed3b  b90a000000            mov       ecx,0Ah
  0040ed40  f7f9                  idiv      ecx
  0040ed42  8b4658                mov       eax,[esi+58h]
  0040ed45  8d4c02fb              lea       ecx,[edx+eax-5]
  0040ed49  8b4624                mov       eax,[esi+24h]
  0040ed4c  03c1                  add       eax,ecx
  0040ed4e  894624                mov       [esi+24h],eax
  0040ed51  e8ea831300            call      00547140h
  0040ed56  99                    cdq
  0040ed57  b905000000            mov       ecx,5
  0040ed5c  f7f9                  idiv      ecx
  0040ed5e  8b4654                mov       eax,[esi+54h]
  0040ed61  8d4c02fe              lea       ecx,[edx+eax-2]
  0040ed65  8b5620                mov       edx,[esi+20h]
  0040ed68  03d1                  add       edx,ecx
  0040ed6a  895620                mov       [esi+20h],edx
  0040ed6d  e8ce831300            call      00547140h
  0040ed72  99                    cdq
  0040ed73  b914000000            mov       ecx,14h
  0040ed78  f7f9                  idiv      ecx
  0040ed7a  668b4616              mov       ax,[esi+16h]
  0040ed7e  663b4614              cmp       ax,[esi+14h]
  0040ed82  7f06                  jg        short 0040ED8Ah
  0040ed84  6683fa01              cmp       dx,1
  0040ed88  7508                  jne       short 0040ED92h

LAB_0040ed8a:
  0040ed8a  66897e10              mov       [esi+10h],di
  0040ed8e  66895e12              mov       [esi+12h],bx

LAB_0040ed92:
  0040ed92  66397e10              cmp       [esi+10h],di
  0040ed96  752d                  jne       short 0040EDC5h
  0040ed98  66ff4612              inc       word [esi+12h]
  0040ed9c  e89f831300            call      00547140h
  0040eda1  99                    cdq
  0040eda2  b932000000            mov       ecx,32h
  0040eda7  f7f9                  idiv      ecx
  0040eda9  668b4612              mov       ax,[esi+12h]
  0040edad  663d5000              cmp       ax,50h
  0040edb1  7f0c                  jg        short 0040EDBFh
  0040edb3  663d1e00              cmp       ax,1Eh
  0040edb7  7e0c                  jle       short 0040EDC5h
  0040edb9  6683fa01              cmp       dx,1
  0040edbd  7506                  jne       short 0040EDC5h

LAB_0040edbf:
  0040edbf  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040edc5:
  0040edc5  66ff4616              inc       word [esi+16h]
  0040edc9  668b4616              mov       ax,[esi+16h]
  0040edcd  66d1f8                sar       ax,1
  0040edd0  3c04                  cmp       al,4
  0040edd2  884640                mov       [esi+40h],al
  0040edd5  7211                  jb        short 0040EDE8h
  0040edd7  b0fc                  mov       al,0FCh

LAB_0040edd9:
  0040edd9  8a5640                mov       dl,[esi+40h]
  0040eddc  02d0                  add       dl,al
  0040edde  8aca                  mov       cl,dl
  0040ede0  885640                mov       [esi+40h],dl
  0040ede3  80f904                cmp       cl,4
  0040ede6  73f1                  jae       short 0040EDD9h

LAB_0040ede8:
  0040ede8  5f                    pop       edi
  0040ede9  5e                    pop       esi
  0040edea  5b                    pop       ebx
  0040edeb  c3                    ret
  0040edec  90                    nop
  0040eded  90                    nop
  0040edee  90                    nop
  0040edef  90                    nop

8 cible(s) de saut interne : 0x0040ecb2, 0x0040ed12, 0x0040ed8a, 0x0040ed92, 0x0040edbf, 0x0040edc5, 0x0040edd9, 0x0040ede8
