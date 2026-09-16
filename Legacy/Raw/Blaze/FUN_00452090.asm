Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00452090  section .text  offset fichier 0x51490  769 octets

  00452090  53                    push      ebx
  00452091  56                    push      esi
  00452092  8b74240c              mov       esi,[esp+0Ch]
  00452096  57                    push      edi
  00452097  6800000040            push      40000000h
  0045209c  6a20                  push      20h
  0045209e  56                    push      esi
  0045209f  e85c2f0e00            call      00535000h
  004520a4  83c40c                add       esp,0Ch
  004520a7  6685c0                test      ax,ax
  004520aa  7461                  je        short 0045210Dh
  004520ac  8b8680000000          mov       eax,[esi+80h]
  004520b2  a3707d5f00            mov       [5F7D70h],eax
  004520b7  8b8e84000000          mov       ecx,[esi+84h]
  004520bd  890d747d5f00          mov       [5F7D74h],ecx
  004520c3  8b9688000000          mov       edx,[esi+88h]
  004520c9  8915787d5f00          mov       [5F7D78h],edx
  004520cf  8b868c000000          mov       eax,[esi+8Ch]
  004520d5  a37c7d5f00            mov       [5F7D7Ch],eax
  004520da  8b4e1c                mov       ecx,[esi+1Ch]
  004520dd  890d309a5e00          mov       [5E9A30h],ecx
  004520e3  8b5620                mov       edx,[esi+20h]
  004520e6  8915349a5e00          mov       [5E9A34h],edx
  004520ec  8b4624                mov       eax,[esi+24h]
  004520ef  a3389a5e00            mov       [5E9A38h],eax
  004520f4  668b8e90000000        mov       cx,[esi+90h]
  004520fb  66890d389b5e00        mov       [5E9B38h],cx
  00452102  8b567c                mov       edx,[esi+7Ch]
  00452105  8915487b5f00          mov       [5F7B48h],edx
  0045210b  eb11                  jmp       short 0045211Eh

LAB_0045210d:
  0045210d  a1cc975e00            mov       eax,[5E97CCh]
  00452112  6a01                  push      1
  00452114  56                    push      esi
  00452115  50                    push      eax
  00452116  e8b51c0c00            call      00513DD0h
  0045211b  83c40c                add       esp,0Ch

LAB_0045211e:
  0045211e  33ff                  xor       edi,edi
  00452120  b8c0935e00            mov       eax,5E93C0h
  00452125  ba04000000            mov       edx,4

LAB_0045212a:
  0045212a  8b08                  mov       ecx,[eax]
  0045212c  f7c100000080          test      ecx,80000000h
  00452132  742b                  je        short 0045215Fh
  00452134  f7c100000050          test      ecx,50000000h
  0045213a  7523                  jne       short 0045215Fh
  0045213c  8b481c                mov       ecx,[eax+1Ch]
  0045213f  81f900b07e00          cmp       ecx,7EB000h
  00452145  7e18                  jle       short 0045215Fh
  00452147  81f900308600          cmp       ecx,863000h
  0045214d  7d10                  jge       short 0045215Fh
  0045214f  8b4824                mov       ecx,[eax+24h]
  00452152  85c9                  test      ecx,ecx
  00452154  7e09                  jle       short 0045215Fh
  00452156  81f900400b00          cmp       ecx,0B4000h
  0045215c  7d01                  jge       short 0045215Fh
  0045215e  47                    inc       edi

LAB_0045215f:
  0045215f  059c000000            add       eax,9Ch
  00452164  4a                    dec       edx
  00452165  75c3                  jne       short 0045212Ah
  00452167  6685ff                test      di,di
  0045216a  7516                  jne       short 00452182h
  0045216c  e8ff4b0100            call      00466D70h
  00452171  6685c0                test      ax,ax
  00452174  740c                  je        short 00452182h
  00452176  66817e10fa00          cmp       word [esi+10h],0FAh
  0045217c  7504                  jne       short 00452182h
  0045217e  66897e10              mov       [esi+10h],di

LAB_00452182:
  00452182  6685ff                test      di,di
  00452185  b340                  mov       bl,40h
  00452187  0f84a1000000          je        near 0045222Eh
  0045218d  e8de4b0100            call      00466D70h
  00452192  663bf8                cmp       di,ax
  00452195  0f8593000000          jne       near 0045222Eh
  0045219b  66837e1000            cmp       word [esi+10h],0
  004521a0  0f8588000000          jne       near 0045222Eh
  004521a6  841dfc965e00          test      [5E96FCh],bl
  004521ac  0f8484000000          je        near 00452236h
  004521b2  8a0d38e55d00          mov       cl,[5DE538h]
  004521b8  33c0                  xor       eax,eax
  004521ba  8ac1                  mov       al,cl
  004521bc  8d1480                lea       edx,[eax+eax*4]
  004521bf  c1e203                shl       edx,3
  004521c2  2bd0                  sub       edx,eax
  004521c4  8b0495c0935e00        mov       eax,[edx*4+5E93C0h]
  004521cb  a900000080            test      eax,80000000h
  004521d0  740d                  je        short 004521DFh
  004521d2  a900000050            test      eax,50000000h
  004521d7  7506                  jne       short 004521DFh
  004521d9  660fb6f9              movzx     di,cl
  004521dd  eb2c                  jmp       short 0045220Bh

LAB_004521df:
  004521df  660fb6f9              movzx     di,cl
  004521e3  33c0                  xor       eax,eax
  004521e5  bac0935e00            mov       edx,5E93C0h

LAB_004521ea:
  004521ea  8b0a                  mov       ecx,[edx]
  004521ec  f7c100000080          test      ecx,80000000h
  004521f2  740a                  je        short 004521FEh
  004521f4  f7c100000050          test      ecx,50000000h
  004521fa  7502                  jne       short 004521FEh
  004521fc  8bf8                  mov       edi,eax

LAB_004521fe:
  004521fe  40                    inc       eax
  004521ff  81c29c000000          add       edx,9Ch
  00452205  663d0300              cmp       ax,3
  00452209  7edf                  jle       short 004521EAh

LAB_0045220b:
  0045220b  0fbfc7                movsx     eax,di
  0045220e  6a02                  push      2
  00452210  8d0c80                lea       ecx,[eax+eax*4]
  00452213  c1e103                shl       ecx,3
  00452216  2bc8                  sub       ecx,eax
  00452218  a1cc975e00            mov       eax,[5E97CCh]
  0045221d  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00452224  52                    push      edx
  00452225  50                    push      eax
  00452226  e8a51b0c00            call      00513DD0h
  0045222b  83c40c                add       esp,0Ch

LAB_0045222e:
  0045222e  841dfc965e00          test      [5E96FCh],bl
  00452234  751d                  jne       short 00452253h

LAB_00452236:
  00452236  8b06                  mov       eax,[esi]
  00452238  6a01                  push      1
  0045223a  80cc60                or        ah,60h
  0045223d  56                    push      esi
  0045223e  8906                  mov       [esi],eax
  00452240  8b0dcc975e00          mov       ecx,[5E97CCh]
  00452246  51                    push      ecx
  00452247  e8841b0c00            call      00513DD0h
  0045224c  83c40c                add       esp,0Ch
  0045224f  5f                    pop       edi
  00452250  5e                    pop       esi
  00452251  5b                    pop       ebx
  00452252  c3                    ret

LAB_00452253:
  00452253  668b4610              mov       ax,[esi+10h]
  00452257  bb00c0e5ff            mov       ebx,0FFE5C000h
  0045225c  6685c0                test      ax,ax
  0045225f  750a                  jne       short 0045226Bh
  00452261  8b0e                  mov       ecx,[esi]
  00452263  895e20                mov       [esi+20h],ebx
  00452266  80e59f                and       ch,9Fh
  00452269  890e                  mov       [esi],ecx

LAB_0045226b:
  0045226b  663d6500              cmp       ax,65h
  0045226f  bf66000000            mov       edi,66h
  00452274  7524                  jne       short 0045229Ah
  00452276  c746200040b6ff        mov       dword [esi+20h],0FFB64000h
  0045227d  66897e10              mov       [esi+10h],di
  00452281  8b15cc975e00          mov       edx,[5E97CCh]
  00452287  6a00                  push      0
  00452289  56                    push      esi
  0045228a  52                    push      edx
  0045228b  e8401b0c00            call      00513DD0h
  00452290  83c40c                add       esp,0Ch
  00452293  c7465c00000000        mov       dword [esi+5Ch],0

LAB_0045229a:
  0045229a  66397e10              cmp       [esi+10h],di
  0045229e  7567                  jne       short 00452307h
  004522a0  8b4e20                mov       ecx,[esi+20h]
  004522a3  81c100a00000          add       ecx,0A000h
  004522a9  8bc1                  mov       eax,ecx
  004522ab  894e20                mov       [esi+20h],ecx
  004522ae  3bc3                  cmp       eax,ebx
  004522b0  7e1a                  jle       short 004522CCh
  004522b2  895e20                mov       [esi+20h],ebx
  004522b5  66c74610fa00          mov       word [esi+10h],0FAh
  004522bb  a1cc975e00            mov       eax,[5E97CCh]
  004522c0  6a01                  push      1
  004522c2  56                    push      esi
  004522c3  50                    push      eax
  004522c4  e8071b0c00            call      00513DD0h
  004522c9  83c40c                add       esp,0Ch

LAB_004522cc:
  004522cc  837e5c0a              cmp       dword [esi+5Ch],0Ah
  004522d0  7532                  jne       short 00452304h
  004522d2  c7465c00000000        mov       dword [esi+5Ch],0
  004522d9  8b0dcc975e00          mov       ecx,[5E97CCh]
  004522df  6a00                  push      0
  004522e1  6a00                  push      0
  004522e3  8b91a8000000          mov       edx,[ecx+0A8h]
  004522e9  6800100000            push      1000h
  004522ee  6800020000            push      200h
  004522f3  8b02                  mov       eax,[edx]
  004522f5  660fb6481b            movzx     cx,byte [eax+1Bh]
  004522fa  51                    push      ecx
  004522fb  56                    push      esi
  004522fc  e87f5d0100            call      00468080h
  00452301  83c418                add       esp,18h

LAB_00452304:
  00452304  ff465c                inc       dword [esi+5Ch]

LAB_00452307:
  00452307  66837e1001            cmp       word [esi+10h],1
  0045230c  7522                  jne       short 00452330h
  0045230e  895e20                mov       [esi+20h],ebx
  00452311  66c746100200          mov       word [esi+10h],2
  00452317  8b15cc975e00          mov       edx,[5E97CCh]
  0045231d  6a00                  push      0
  0045231f  56                    push      esi
  00452320  52                    push      edx
  00452321  e8aa1a0c00            call      00513DD0h
  00452326  83c40c                add       esp,0Ch
  00452329  c7465c00000000        mov       dword [esi+5Ch],0

LAB_00452330:
  00452330  66837e1002            cmp       word [esi+10h],2
  00452335  7556                  jne       short 0045238Dh
  00452337  8b5620                mov       edx,[esi+20h]
  0045233a  81c20060ffff          add       edx,0FFFF6000h
  00452340  8bc2                  mov       eax,edx
  00452342  895620                mov       [esi+20h],edx
  00452345  3d00c0aeff            cmp       eax,0FFAEC000h
  0045234a  7d07                  jge       short 00452353h
  0045234c  c7462000c0aeff        mov       dword [esi+20h],0FFAEC000h

LAB_00452353:
  00452353  837e5c0a              cmp       dword [esi+5Ch],0Ah
  00452357  7531                  jne       short 0045238Ah
  00452359  c7465c00000000        mov       dword [esi+5Ch],0
  00452360  a1cc975e00            mov       eax,[5E97CCh]
  00452365  6a00                  push      0
  00452367  6a00                  push      0
  00452369  8b88a8000000          mov       ecx,[eax+0A8h]
  0045236f  6800100000            push      1000h
  00452374  6800020000            push      200h
  00452379  8b11                  mov       edx,[ecx]
  0045237b  660fb6421b            movzx     ax,byte [edx+1Bh]
  00452380  50                    push      eax
  00452381  56                    push      esi
  00452382  e8f95c0100            call      00468080h
  00452387  83c418                add       esp,18h

LAB_0045238a:
  0045238a  ff465c                inc       dword [esi+5Ch]

LAB_0045238d:
  0045238d  5f                    pop       edi
  0045238e  5e                    pop       esi
  0045238f  5b                    pop       ebx
  00452390  c3                    ret

21 cible(s) de saut interne : 0x0045210d, 0x0045211e, 0x0045212a, 0x0045215f, 0x00452182, 0x004521df, 0x004521ea, 0x004521fe, 0x0045220b, 0x0045222e, 0x00452236, 0x00452253, 0x0045226b, 0x0045229a, 0x004522cc, 0x00452304, 0x00452307, 0x00452330, 0x00452353, 0x0045238a, 0x0045238d
