Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040bf50  section .text  offset fichier 0xb350  1296 octets

  0040bf50  81eca0000000          sub       esp,0A0h
  0040bf56  53                    push      ebx
  0040bf57  55                    push      ebp
  0040bf58  56                    push      esi
  0040bf59  57                    push      edi
  0040bf5a  33ed                  xor       ebp,ebp
  0040bf5c  b926000000            mov       ecx,26h
  0040bf61  33c0                  xor       eax,eax
  0040bf63  8d7c2418              lea       edi,[esp+18h]
  0040bf67  896c2414              mov       [esp+14h],ebp
  0040bf6b  f3ab                  rep stosd
  0040bf6d  e8deef0000            call      0041AF50h
  0040bf72  e8c9260000            call      0040E640h
  0040bf77  8bb424b4000000        mov       esi,[esp+0B4h]
  0040bf7e  8d7c2414              lea       edi,[esp+14h]
  0040bf82  b8a4ca5e00            mov       eax,5ECAA4h
  0040bf87  ba20000000            mov       edx,20h

LAB_0040bf8c:
  0040bf8c  80780803              cmp       byte [eax+8],3
  0040bf90  753a                  jne       short 0040BFCCh
  0040bf92  8b58fc                mov       ebx,[eax-4]
  0040bf95  8d48fc                lea       ecx,[eax-4]
  0040bf98  f7c300000080          test      ebx,80000000h
  0040bf9e  742c                  je        short 0040BFCCh
  0040bfa0  8bf9                  mov       edi,ecx
  0040bfa2  8b4818                mov       ecx,[eax+18h]
  0040bfa5  894e1c                mov       [esi+1Ch],ecx
  0040bfa8  8b481c                mov       ecx,[eax+1Ch]
  0040bfab  894e20                mov       [esi+20h],ecx
  0040bfae  8b4820                mov       ecx,[eax+20h]
  0040bfb1  894e24                mov       [esi+24h],ecx
  0040bfb4  33c9                  xor       ecx,ecx
  0040bfb6  8a08                  mov       cl,[eax]
  0040bfb8  66c740140200          mov       word [eax+14h],2
  0040bfbe  8b0c8d40965e00        mov       ecx,[ecx*4+5E9640h]
  0040bfc5  8b4908                mov       ecx,[ecx+8]
  0040bfc8  894c2410              mov       [esp+10h],ecx

LAB_0040bfcc:
  0040bfcc  059c000000            add       eax,9Ch
  0040bfd1  4a                    dec       edx
  0040bfd2  75b8                  jne       short 0040BF8Ch
  0040bfd4  66396e10              cmp       [esi+10h],bp
  0040bfd8  bb01000000            mov       ebx,1
  0040bfdd  7551                  jne       short 0040C030h
  0040bfdf  66895e10              mov       [esi+10h],bx
  0040bfe3  66896e12              mov       [esi+12h],bp
  0040bfe7  66896e14              mov       [esi+14h],bp
  0040bfeb  66896e16              mov       [esi+16h],bp
  0040bfef  0fbf15b8925e00        movsx     edx,word [5E92B8h]
  0040bff6  895650                mov       [esi+50h],edx
  0040bff9  0fbf05ba925e00        movsx     eax,word [5E92BAh]
  0040c000  894654                mov       [esi+54h],eax
  0040c003  0fbf0dbc925e00        movsx     ecx,word [5E92BCh]
  0040c00a  894e58                mov       [esi+58h],ecx
  0040c00d  8a5709                mov       dl,[edi+9]
  0040c010  8815e8135700          mov       [5713E8h],dl
  0040c016  66896f1a              mov       [edi+1Ah],bp
  0040c01a  c6470a00              mov       byte [edi+0Ah],0
  0040c01e  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0040c025  66895f36              mov       [edi+36h],bx
  0040c029  6689ae98000000        mov       [esi+98h],bp

LAB_0040c030:
  0040c030  66395e10              cmp       [esi+10h],bx
  0040c034  0f85ff000000          jne       near 0040C139h
  0040c03a  6aff                  push      0FFFFFFFFh
  0040c03c  55                    push      ebp
  0040c03d  68fb000000            push      0FBh
  0040c042  8d461c                lea       eax,[esi+1Ch]
  0040c045  55                    push      ebp
  0040c046  50                    push      eax
  0040c047  56                    push      esi
  0040c048  66896e14              mov       [esi+14h],bp
  0040c04c  e8efeb0600            call      0047AC40h
  0040c051  83c418                add       esp,18h
  0040c054  e8e7b01300            call      00547140h
  0040c059  99                    cdq
  0040c05a  b90c000000            mov       ecx,0Ch
  0040c05f  f7f9                  idiv      ecx
  0040c061  663bd3                cmp       dx,bx
  0040c064  750c                  jne       short 0040C072h
  0040c066  6689ae98000000        mov       [esi+98h],bp
  0040c06d  e9d4030000            jmp       0040C446h

LAB_0040c072:
  0040c072  6683fa02              cmp       dx,2
  0040c076  0f859d030000          jne       near 0040C419h
  0040c07c  6689ae98000000        mov       [esi+98h],bp

LAB_0040c083:
  0040c083  6800080000            push      800h
  0040c088  6800080000            push      800h
  0040c08d  6853060000            push      653h
  0040c092  6a22                  push      22h

LAB_0040c094:
  0040c094  e8070e1300            call      0053CEA0h
  0040c099  83c410                add       esp,10h
  0040c09c  6800040000            push      400h
  0040c0a1  6800040000            push      400h
  0040c0a6  6853060000            push      653h
  0040c0ab  6a25                  push      25h
  0040c0ad  e8ee0d1300            call      0053CEA0h
  0040c0b2  83c410                add       esp,10h

LAB_0040c0b5:
  0040c0b5  66ff8698000000        inc       word [esi+98h]
  0040c0bc  6683be9800000008      cmp       word [esi+98h],8
  0040c0c4  7507                  jne       short 0040C0CDh
  0040c0c6  6689ae98000000        mov       [esi+98h],bp

LAB_0040c0cd:
  0040c0cd  66837e1203            cmp       word [esi+12h],3
  0040c0d2  7f26                  jg        short 0040C0FAh
  0040c0d4  66ff471a              inc       word [edi+1Ah]
  0040c0d8  8b4c2410              mov       ecx,[esp+10h]
  0040c0dc  668b471a              mov       ax,[edi+1Ah]
  0040c0e0  33d2                  xor       edx,edx
  0040c0e2  668b5116              mov       dx,[ecx+16h]
  0040c0e6  0fbfc0                movsx     eax,ax
  0040c0e9  83ea0a                sub       edx,0Ah
  0040c0ec  3bc2                  cmp       eax,edx
  0040c0ee  7c49                  jl        short 0040C139h
  0040c0f0  66896f1a              mov       [edi+1Ah],bp
  0040c0f4  66ff4612              inc       word [esi+12h]
  0040c0f8  eb3f                  jmp       short 0040C139h

LAB_0040c0fa:
  0040c0fa  845e16                test      [esi+16h],bl
  0040c0fd  743a                  je        short 0040C139h
  0040c0ff  8b542410              mov       edx,[esp+10h]
  0040c103  66ff471a              inc       word [edi+1Ah]
  0040c107  668b4f1a              mov       cx,[edi+1Ah]
  0040c10b  668b4216              mov       ax,[edx+16h]
  0040c10f  8bd0                  mov       edx,eax
  0040c111  81e2ffff0000          and       edx,0FFFFh
  0040c117  0fbfc9                movsx     ecx,cx
  0040c11a  83ea04                sub       edx,4
  0040c11d  3bca                  cmp       ecx,edx
  0040c11f  7c18                  jl        short 0040C139h
  0040c121  83c0fc                add       eax,0FFFFFFFCh
  0040c124  6689471a              mov       [edi+1Ah],ax
  0040c128  66c746100200          mov       word [esi+10h],2
  0040c12e  6689ae98000000        mov       [esi+98h],bp
  0040c135  66896e12              mov       [esi+12h],bp

LAB_0040c139:
  0040c139  66837e1002            cmp       word [esi+10h],2
  0040c13e  0f8558020000          jne       near 0040C39Ch
  0040c144  66ff4612              inc       word [esi+12h]
  0040c148  66837e1203            cmp       word [esi+12h],3
  0040c14d  7f11                  jg        short 0040C160h
  0040c14f  b82c010000            mov       eax,12Ch
  0040c154  66014728              add       [edi+28h],ax
  0040c158  6601472a              add       [edi+2Ah],ax
  0040c15c  6601472c              add       [edi+2Ch],ax

LAB_0040c160:
  0040c160  66837e120c            cmp       word [esi+12h],0Ch
  0040c165  0f8c31020000          jl        near 0040C39Ch
  0040c16b  8b07                  mov       eax,[edi]
  0040c16d  8d5e1c                lea       ebx,[esi+1Ch]
  0040c170  80cc10                or        ah,10h
  0040c173  bd06000000            mov       ebp,6
  0040c178  8907                  mov       [edi],eax

LAB_0040c17a:
  0040c17a  668b572a              mov       dx,[edi+2Ah]
  0040c17e  6aff                  push      0FFFFFFFFh
  0040c180  6a00                  push      0
  0040c182  68fb000000            push      0FBh
  0040c187  6a00                  push      0
  0040c189  53                    push      ebx
  0040c18a  56                    push      esi
  0040c18b  66895614              mov       [esi+14h],dx
  0040c18f  e8acea0600            call      0047AC40h
  0040c194  83c418                add       esp,18h
  0040c197  4d                    dec       ebp
  0040c198  75e0                  jne       short 0040C17Ah
  0040c19a  6683be9800000000      cmp       word [esi+98h],0
  0040c1a2  7564                  jne       short 0040C208h
  0040c1a4  6800080000            push      800h
  0040c1a9  6800080000            push      800h
  0040c1ae  6853060000            push      653h
  0040c1b3  6a1f                  push      1Fh
  0040c1b5  e8e60c1300            call      0053CEA0h
  0040c1ba  83c410                add       esp,10h
  0040c1bd  6800080000            push      800h
  0040c1c2  6800080000            push      800h
  0040c1c7  68ea010000            push      1EAh
  0040c1cc  6a03                  push      3
  0040c1ce  e8cd0c1300            call      0053CEA0h
  0040c1d3  83c410                add       esp,10h
  0040c1d6  6800080000            push      800h
  0040c1db  6800080000            push      800h
  0040c1e0  6800020000            push      200h
  0040c1e5  6a03                  push      3
  0040c1e7  e8b40c1300            call      0053CEA0h
  0040c1ec  83c410                add       esp,10h
  0040c1ef  6800040000            push      400h
  0040c1f4  6800040000            push      400h
  0040c1f9  6800020000            push      200h
  0040c1fe  6a25                  push      25h
  0040c200  e89b0c1300            call      0053CEA0h
  0040c205  83c410                add       esp,10h

LAB_0040c208:
  0040c208  6683be9800000005      cmp       word [esi+98h],5
  0040c210  7564                  jne       short 0040C276h
  0040c212  6800080000            push      800h
  0040c217  6800080000            push      800h
  0040c21c  6800040000            push      400h
  0040c221  6a1f                  push      1Fh
  0040c223  e8780c1300            call      0053CEA0h
  0040c228  83c410                add       esp,10h
  0040c22b  6800080000            push      800h
  0040c230  6800080000            push      800h
  0040c235  68ea010000            push      1EAh
  0040c23a  6a03                  push      3
  0040c23c  e85f0c1300            call      0053CEA0h
  0040c241  83c410                add       esp,10h
  0040c244  6800080000            push      800h
  0040c249  6800080000            push      800h
  0040c24e  6800020000            push      200h
  0040c253  6a03                  push      3
  0040c255  e8460c1300            call      0053CEA0h
  0040c25a  83c410                add       esp,10h
  0040c25d  6800040000            push      400h
  0040c262  6800040000            push      400h
  0040c267  6800020000            push      200h
  0040c26c  6a25                  push      25h
  0040c26e  e82d0c1300            call      0053CEA0h
  0040c273  83c410                add       esp,10h

LAB_0040c276:
  0040c276  6683be980000000a      cmp       word [esi+98h],0Ah
  0040c27e  7564                  jne       short 0040C2E4h
  0040c280  6800080000            push      800h
  0040c285  6800080000            push      800h
  0040c28a  6829030000            push      329h
  0040c28f  6a1f                  push      1Fh
  0040c291  e80a0c1300            call      0053CEA0h
  0040c296  83c410                add       esp,10h
  0040c299  6800080000            push      800h
  0040c29e  6800080000            push      800h
  0040c2a3  68ea010000            push      1EAh
  0040c2a8  6a03                  push      3
  0040c2aa  e8f10b1300            call      0053CEA0h
  0040c2af  83c410                add       esp,10h
  0040c2b2  6800080000            push      800h
  0040c2b7  6800080000            push      800h
  0040c2bc  6800020000            push      200h
  0040c2c1  6a03                  push      3
  0040c2c3  e8d80b1300            call      0053CEA0h
  0040c2c8  83c410                add       esp,10h
  0040c2cb  6800040000            push      400h
  0040c2d0  6800040000            push      400h
  0040c2d5  6800020000            push      200h
  0040c2da  6a25                  push      25h
  0040c2dc  e8bf0b1300            call      0053CEA0h
  0040c2e1  83c410                add       esp,10h

LAB_0040c2e4:
  0040c2e4  6683be980000000f      cmp       word [esi+98h],0Fh
  0040c2ec  7564                  jne       short 0040C352h
  0040c2ee  6800080000            push      800h
  0040c2f3  6800080000            push      800h
  0040c2f8  6800020000            push      200h
  0040c2fd  6a1f                  push      1Fh
  0040c2ff  e89c0b1300            call      0053CEA0h
  0040c304  83c410                add       esp,10h
  0040c307  6800080000            push      800h
  0040c30c  6800080000            push      800h
  0040c311  68ea010000            push      1EAh
  0040c316  6a03                  push      3
  0040c318  e8830b1300            call      0053CEA0h
  0040c31d  83c410                add       esp,10h
  0040c320  6800080000            push      800h
  0040c325  6800080000            push      800h
  0040c32a  6800020000            push      200h
  0040c32f  6a03                  push      3
  0040c331  e86a0b1300            call      0053CEA0h
  0040c336  83c410                add       esp,10h
  0040c339  6800040000            push      400h
  0040c33e  6800040000            push      400h
  0040c343  6800020000            push      200h
  0040c348  6a25                  push      25h
  0040c34a  e8510b1300            call      0053CEA0h
  0040c34f  83c410                add       esp,10h

LAB_0040c352:
  0040c352  66ff8698000000        inc       word [esi+98h]
  0040c359  6683be9800000014      cmp       word [esi+98h],14h
  0040c361  7509                  jne       short 0040C36Ch
  0040c363  66c786980000000000    mov       word [esi+98h],0

LAB_0040c36c:
  0040c36c  b938ffffff            mov       ecx,0FFFFFF38h
  0040c371  66014f28              add       [edi+28h],cx
  0040c375  668b4728              mov       ax,[edi+28h]
  0040c379  66014f2a              add       [edi+2Ah],cx
  0040c37d  66014f2c              add       [edi+2Ch],cx
  0040c381  6685c0                test      ax,ax
  0040c384  7d14                  jge       short 0040C39Ah
  0040c386  33c0                  xor       eax,eax
  0040c388  66894728              mov       [edi+28h],ax
  0040c38c  6689472a              mov       [edi+2Ah],ax
  0040c390  6689472c              mov       [edi+2Ch],ax
  0040c394  66c746100300          mov       word [esi+10h],3

LAB_0040c39a:
  0040c39a  33ed                  xor       ebp,ebp

LAB_0040c39c:
  0040c39c  66817e168c00          cmp       word [esi+16h],8Ch
  0040c3a2  7c66                  jl        short 0040C40Ah
  0040c3a4  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0040c3aa  e801740f00            call      005037B0h
  0040c3af  a1e8135700            mov       eax,[5713E8h]
  0040c3b4  55                    push      ebp
  0040c3b5  25ff000000            and       eax,0FFh
  0040c3ba  8d0c80                lea       ecx,[eax+eax*4]
  0040c3bd  c1e103                shl       ecx,3
  0040c3c0  2bc8                  sub       ecx,eax
  0040c3c2  a1cc975e00            mov       eax,[5E97CCh]
  0040c3c7  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0040c3ce  52                    push      edx
  0040c3cf  50                    push      eax
  0040c3d0  e8fb791000            call      00513DD0h
  0040c3d5  8b07                  mov       eax,[edi]
  0040c3d7  83c40c                add       esp,0Ch
  0040c3da  0d00400002            or        eax,2004000h
  0040c3df  8907                  mov       [edi],eax
  0040c3e1  668b4e50              mov       cx,[esi+50h]
  0040c3e5  66890d60915e00        mov       [5E9160h],cx
  0040c3ec  668b5654              mov       dx,[esi+54h]
  0040c3f0  66891562915e00        mov       [5E9162h],dx
  0040c3f7  668b4658              mov       ax,[esi+58h]
  0040c3fb  66a364915e00          mov       [5E9164h],ax
  0040c401  66c70566915e000200    mov       word [5E9166h],2

LAB_0040c40a:
  0040c40a  66ff4616              inc       word [esi+16h]
  0040c40e  5f                    pop       edi
  0040c40f  5e                    pop       esi
  0040c410  5d                    pop       ebp
  0040c411  5b                    pop       ebx
  0040c412  81c4a0000000          add       esp,0A0h
  0040c418  c3                    ret

LAB_0040c419:
  0040c419  6639ae98000000        cmp       [esi+98h],bp
  0040c420  0f858ffcffff          jne       near 0040C0B5h
  0040c426  6689ae98000000        mov       [esi+98h],bp
  0040c42d  e80ead1300            call      00547140h
  0040c432  99                    cdq
  0040c433  33c2                  xor       eax,edx
  0040c435  2bc2                  sub       eax,edx
  0040c437  23c3                  and       eax,ebx
  0040c439  33c2                  xor       eax,edx
  0040c43b  2bc2                  sub       eax,edx
  0040c43d  663bc3                cmp       ax,bx
  0040c440  0f853dfcffff          jne       near 0040C083h

LAB_0040c446:
  0040c446  6800080000            push      800h
  0040c44b  6800080000            push      800h
  0040c450  6800020000            push      200h
  0040c455  6a03                  push      3
  0040c457  e938fcffff            jmp       0040C094h
  0040c45c  90                    nop
  0040c45d  90                    nop
  0040c45e  90                    nop
  0040c45f  90                    nop

22 cible(s) de saut interne : 0x0040bf8c, 0x0040bfcc, 0x0040c030, 0x0040c072, 0x0040c083, 0x0040c094, 0x0040c0b5, 0x0040c0cd, 0x0040c0fa, 0x0040c139, 0x0040c160, 0x0040c17a, 0x0040c208, 0x0040c276, 0x0040c2e4, 0x0040c352, 0x0040c36c, 0x0040c39a, 0x0040c39c, 0x0040c40a, 0x0040c419, 0x0040c446
