Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042f620  section .text  offset fichier 0x2ea20  576 octets

  0042f620  53                    push      ebx
  0042f621  55                    push      ebp
  0042f622  56                    push      esi
  0042f623  57                    push      edi
  0042f624  33ff                  xor       edi,edi
  0042f626  e825b9feff            call      0041AF50h
  0042f62b  e810f0fdff            call      0040E640h
  0042f630  8b742414              mov       esi,[esp+14h]
  0042f634  b8a0ca5e00            mov       eax,5ECAA0h
  0042f639  ba20000000            mov       edx,20h
  0042f63e  b903000000            mov       ecx,3
  0042f643  bb02000000            mov       ebx,2

LAB_0042f648:
  0042f648  80780c01              cmp       byte [eax+0Ch],1
  0042f64c  752d                  jne       short 0042F67Bh
  0042f64e  f70000000080          test      dword [eax],80000000h
  0042f654  7425                  je        short 0042F67Bh
  0042f656  8b681c                mov       ebp,[eax+1Ch]
  0042f659  8bf8                  mov       edi,eax
  0042f65b  896e1c                mov       [esi+1Ch],ebp
  0042f65e  8b6820                mov       ebp,[eax+20h]
  0042f661  896e20                mov       [esi+20h],ebp
  0042f664  8b6824                mov       ebp,[eax+24h]
  0042f667  896e24                mov       [esi+24h],ebp
  0042f66a  8b28                  mov       ebp,[eax]
  0042f66c  08583b                or        [eax+3Bh],bl
  0042f66f  81cd00100000          or        ebp,1000h
  0042f675  66894818              mov       [eax+18h],cx
  0042f679  8928                  mov       [eax],ebp

LAB_0042f67b:
  0042f67b  059c000000            add       eax,9Ch
  0042f680  4a                    dec       edx
  0042f681  75c5                  jne       short 0042F648h
  0042f683  33c0                  xor       eax,eax
  0042f685  3bf8                  cmp       edi,eax
  0042f687  7505                  jne       short 0042F68Eh
  0042f689  bf58e55d00            mov       edi,5DE558h

LAB_0042f68e:
  0042f68e  66394610              cmp       [esi+10h],ax
  0042f692  752b                  jne       short 0042F6BFh
  0042f694  b901000000            mov       ecx,1
  0042f699  66895e12              mov       [esi+12h],bx
  0042f69d  66894e10              mov       [esi+10h],cx
  0042f6a1  66894614              mov       [esi+14h],ax
  0042f6a5  66894616              mov       [esi+16h],ax
  0042f6a9  c6470a00              mov       byte [edi+0Ah],0
  0042f6ad  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0042f6b4  66894f36              mov       [edi+36h],cx
  0042f6b8  66898698000000        mov       [esi+98h],ax

LAB_0042f6bf:
  0042f6bf  0fbf4732              movsx     eax,word [edi+32h]
  0042f6c3  50                    push      eax
  0042f6c4  e853631100            call      00545A1Ch
  0042f6c9  8b4f1c                mov       ecx,[edi+1Ch]
  0042f6cc  f7d8                  neg       eax
  0042f6ce  c1e002                shl       eax,2
  0042f6d1  03c8                  add       ecx,eax
  0042f6d3  894f1c                mov       [edi+1Ch],ecx
  0042f6d6  0fbf4f32              movsx     ecx,word [edi+32h]
  0042f6da  51                    push      ecx
  0042f6db  e836631100            call      00545A16h
  0042f6e0  8b4f24                mov       ecx,[edi+24h]
  0042f6e3  8b6f20                mov       ebp,[edi+20h]
  0042f6e6  f7d8                  neg       eax
  0042f6e8  c1e002                shl       eax,2
  0042f6eb  03c8                  add       ecx,eax
  0042f6ed  6af8                  push      0FFFFFFF8h
  0042f6ef  894f24                mov       [edi+24h],ecx
  0042f6f2  6af8                  push      0FFFFFFF8h
  0042f6f4  0fbf5612              movsx     edx,word [esi+12h]
  0042f6f8  c1e20c                shl       edx,0Ch
  0042f6fb  03ea                  add       ebp,edx
  0042f6fd  6aff                  push      0FFFFFFFFh
  0042f6ff  57                    push      edi
  0042f700  896f20                mov       [edi+20h],ebp
  0042f703  e8d856fdff            call      00404DE0h
  0042f708  668b4616              mov       ax,[esi+16h]
  0042f70c  83c410                add       esp,10h
  0042f70f  663d0f00              cmp       ax,0Fh
  0042f713  7412                  je        short 0042F727h
  0042f715  663d1900              cmp       ax,19h
  0042f719  740c                  je        short 0042F727h
  0042f71b  663d2300              cmp       ax,23h
  0042f71f  7406                  je        short 0042F727h
  0042f721  663d2d00              cmp       ax,2Dh
  0042f725  7504                  jne       short 0042F72Bh

LAB_0042f727:
  0042f727  66ff4e12              dec       word [esi+12h]

LAB_0042f72b:
  0042f72b  84c3                  test      bl,al
  0042f72d  7513                  jne       short 0042F742h
  0042f72f  66ff471a              inc       word [edi+1Ah]
  0042f733  b80b000000            mov       eax,0Bh
  0042f738  6639471a              cmp       [edi+1Ah],ax
  0042f73c  7c04                  jl        short 0042F742h
  0042f73e  6689471a              mov       [edi+1Ah],ax

LAB_0042f742:
  0042f742  bb3c000000            mov       ebx,3Ch
  0042f747  66395e16              cmp       [esi+16h],bx
  0042f74b  7d77                  jge       short 0042F7C4h
  0042f74d  6aff                  push      0FFFFFFFFh
  0042f74f  6a00                  push      0
  0042f751  8d6e1c                lea       ebp,[esi+1Ch]
  0042f754  68c2000000            push      0C2h
  0042f759  6a00                  push      0
  0042f75b  55                    push      ebp
  0042f75c  56                    push      esi
  0042f75d  e8deb40400            call      0047AC40h
  0042f762  83c418                add       esp,18h
  0042f765  6aff                  push      0FFFFFFFFh
  0042f767  6a00                  push      0
  0042f769  68c2000000            push      0C2h
  0042f76e  6a00                  push      0
  0042f770  55                    push      ebp
  0042f771  56                    push      esi
  0042f772  e8c9b40400            call      0047AC40h
  0042f777  83c418                add       esp,18h
  0042f77a  66ff8698000000        inc       word [esi+98h]
  0042f781  e8ba791100            call      00547140h
  0042f786  99                    cdq
  0042f787  b906000000            mov       ecx,6
  0042f78c  f7f9                  idiv      ecx
  0042f78e  6683be9800000005      cmp       word [esi+98h],5
  0042f796  7406                  je        short 0042F79Eh
  0042f798  6683fa01              cmp       dx,1
  0042f79c  7522                  jne       short 0042F7C0h

LAB_0042f79e:
  0042f79e  6800080000            push      800h
  0042f7a3  6800080000            push      800h
  0042f7a8  68f4010000            push      1F4h
  0042f7ad  6a0b                  push      0Bh
  0042f7af  66c786980000000000    mov       word [esi+98h],0
  0042f7b8  e8e3d61000            call      0053CEA0h
  0042f7bd  83c410                add       esp,10h

LAB_0042f7c0:
  0042f7c0  66395e16              cmp       [esi+16h],bx

LAB_0042f7c4:
  0042f7c4  754b                  jne       short 0042F811h
  0042f7c6  6aff                  push      0FFFFFFFFh
  0042f7c8  6a00                  push      0
  0042f7ca  68c1000000            push      0C1h
  0042f7cf  56                    push      esi
  0042f7d0  e80bb30400            call      0047AAE0h
  0042f7d5  8b07                  mov       eax,[edi]
  0042f7d7  83c410                add       esp,10h
  0042f7da  80cc40                or        ah,40h
  0042f7dd  6800100000            push      1000h
  0042f7e2  6800100000            push      1000h
  0042f7e7  6800020000            push      200h
  0042f7ec  6a2e                  push      2Eh
  0042f7ee  8907                  mov       [edi],eax
  0042f7f0  e8abd61000            call      0053CEA0h
  0042f7f5  83c410                add       esp,10h
  0042f7f8  6800100000            push      1000h
  0042f7fd  6800100000            push      1000h
  0042f802  682a020000            push      22Ah
  0042f807  6a2e                  push      2Eh
  0042f809  e892d61000            call      0053CEA0h
  0042f80e  83c410                add       esp,10h

LAB_0042f811:
  0042f811  66837e1678            cmp       word [esi+16h],78h
  0042f816  7c3c                  jl        short 0042F854h
  0042f818  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0042f81e  e88d3f0d00            call      005037B0h
  0042f823  33c0                  xor       eax,eax
  0042f825  8b0dcc975e00          mov       ecx,[5E97CCh]
  0042f82b  a038e55d00            mov       al,[5DE538h]
  0042f830  6a01                  push      1
  0042f832  8d1480                lea       edx,[eax+eax*4]
  0042f835  c1e203                shl       edx,3
  0042f838  2bd0                  sub       edx,eax
  0042f83a  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  0042f841  50                    push      eax
  0042f842  51                    push      ecx
  0042f843  e888450e00            call      00513DD0h
  0042f848  8b07                  mov       eax,[edi]
  0042f84a  83c40c                add       esp,0Ch
  0042f84d  0d00400002            or        eax,2004000h
  0042f852  8907                  mov       [edi],eax

LAB_0042f854:
  0042f854  66ff4616              inc       word [esi+16h]
  0042f858  5f                    pop       edi
  0042f859  5e                    pop       esi
  0042f85a  5d                    pop       ebp
  0042f85b  5b                    pop       ebx
  0042f85c  c3                    ret
  0042f85d  90                    nop
  0042f85e  90                    nop
  0042f85f  90                    nop

12 cible(s) de saut interne : 0x0042f648, 0x0042f67b, 0x0042f68e, 0x0042f6bf, 0x0042f727, 0x0042f72b, 0x0042f742, 0x0042f79e, 0x0042f7c0, 0x0042f7c4, 0x0042f811, 0x0042f854
