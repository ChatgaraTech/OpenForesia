; 0x00459fa0 — le pilier tournant a quatre faces (177 instructions, absente de Blaze.c). Orientation sur deux bits de la banque d'etat 0, octet 0x1d.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00459fa0  section .text  offset fichier 0x593a0  640 octets

  00459fa0  55                    push      ebp
  00459fa1  56                    push      esi
  00459fa2  8b74240c              mov       esi,[esp+0Ch]
  00459fa6  bd000a0000            mov       ebp,0A00h
  00459fab  668b5610              mov       dx,[esi+10h]
  00459faf  6685d2                test      dx,dx
  00459fb2  0f85c1000000          jne       near 0045A079h
  00459fb8  668b4e14              mov       cx,[esi+14h]
  00459fbc  a0fd965e00            mov       al,[5E96FDh]
  00459fc1  6685c9                test      cx,cx
  00459fc4  7526                  jne       short 00459FECh
  00459fc6  2403                  and       al,3
  00459fc8  7504                  jne       short 00459FCEh
  00459fca  66896e32              mov       [esi+32h],bp

LAB_00459fce:
  00459fce  3c01                  cmp       al,1
  00459fd0  7506                  jne       short 00459FD8h
  00459fd2  66c746320006          mov       word [esi+32h],600h

LAB_00459fd8:
  00459fd8  3c02                  cmp       al,2
  00459fda  7506                  jne       short 00459FE2h
  00459fdc  66c746320002          mov       word [esi+32h],200h

LAB_00459fe2:
  00459fe2  3c03                  cmp       al,3
  00459fe4  7506                  jne       short 00459FECh
  00459fe6  66c74632000e          mov       word [esi+32h],0E00h

LAB_00459fec:
  00459fec  6683f901              cmp       cx,1
  00459ff0  7526                  jne       short 0045A018h
  00459ff2  240c                  and       al,0Ch
  00459ff4  7504                  jne       short 00459FFAh
  00459ff6  66896e32              mov       [esi+32h],bp

LAB_00459ffa:
  00459ffa  3c04                  cmp       al,4
  00459ffc  7506                  jne       short 0045A004h
  00459ffe  66c746320006          mov       word [esi+32h],600h

LAB_0045a004:
  0045a004  3c08                  cmp       al,8
  0045a006  7506                  jne       short 0045A00Eh
  0045a008  66c746320002          mov       word [esi+32h],200h

LAB_0045a00e:
  0045a00e  3c0c                  cmp       al,0Ch
  0045a010  7506                  jne       short 0045A018h
  0045a012  66c74632000e          mov       word [esi+32h],0E00h

LAB_0045a018:
  0045a018  6683f902              cmp       cx,2
  0045a01c  7526                  jne       short 0045A044h
  0045a01e  2430                  and       al,30h
  0045a020  7504                  jne       short 0045A026h
  0045a022  66896e32              mov       [esi+32h],bp

LAB_0045a026:
  0045a026  3c10                  cmp       al,10h
  0045a028  7506                  jne       short 0045A030h
  0045a02a  66c746320006          mov       word [esi+32h],600h

LAB_0045a030:
  0045a030  3c20                  cmp       al,20h
  0045a032  7506                  jne       short 0045A03Ah
  0045a034  66c746320002          mov       word [esi+32h],200h

LAB_0045a03a:
  0045a03a  3c30                  cmp       al,30h
  0045a03c  7506                  jne       short 0045A044h
  0045a03e  66c74632000e          mov       word [esi+32h],0E00h

LAB_0045a044:
  0045a044  6683f903              cmp       cx,3
  0045a048  7526                  jne       short 0045A070h
  0045a04a  24c0                  and       al,0C0h
  0045a04c  7504                  jne       short 0045A052h
  0045a04e  66896e32              mov       [esi+32h],bp

LAB_0045a052:
  0045a052  3c40                  cmp       al,40h
  0045a054  7506                  jne       short 0045A05Ch
  0045a056  66c746320006          mov       word [esi+32h],600h

LAB_0045a05c:
  0045a05c  3c80                  cmp       al,80h
  0045a05e  7506                  jne       short 0045A066h
  0045a060  66c746320002          mov       word [esi+32h],200h

LAB_0045a066:
  0045a066  3cc0                  cmp       al,0C0h
  0045a068  7506                  jne       short 0045A070h
  0045a06a  66c74632000e          mov       word [esi+32h],0E00h

LAB_0045a070:
  0045a070  6685d2                test      dx,dx
  0045a073  0f849a010000          je        near 0045A213h

LAB_0045a079:
  0045a079  6683fa01              cmp       dx,1
  0045a07d  751d                  jne       short 0045A09Ch
  0045a07f  668b4632              mov       ax,[esi+32h]
  0045a083  66c746122000          mov       word [esi+12h],20h
  0045a089  66894616              mov       [esi+16h],ax
  0045a08d  66c746100200          mov       word [esi+10h],2
  0045a093  66c786980000000000    mov       word [esi+98h],0

LAB_0045a09c:
  0045a09c  66837e1002            cmp       word [esi+10h],2
  0045a0a1  0f856c010000          jne       near 0045A213h
  0045a0a7  66ff8698000000        inc       word [esi+98h]
  0045a0ae  668b8698000000        mov       ax,[esi+98h]
  0045a0b5  663d0400              cmp       ax,4
  0045a0b9  7534                  jne       short 0045A0EFh
  0045a0bb  8b0dcc975e00          mov       ecx,[5E97CCh]
  0045a0c1  6a00                  push      0
  0045a0c3  6a00                  push      0
  0045a0c5  6800100000            push      1000h
  0045a0ca  8b91a8000000          mov       edx,[ecx+0A8h]
  0045a0d0  6800040000            push      400h
  0045a0d5  8b02                  mov       eax,[edx]
  0045a0d7  660fb6481e            movzx     cx,byte [eax+1Eh]
  0045a0dc  51                    push      ecx
  0045a0dd  56                    push      esi
  0045a0de  e89ddf0000            call      00468080h
  0045a0e3  83c418                add       esp,18h
  0045a0e6  66c786980000000000    mov       word [esi+98h],0

LAB_0045a0ef:
  0045a0ef  668b5612              mov       dx,[esi+12h]
  0045a0f3  66015632              add       [esi+32h],dx
  0045a0f7  668b4632              mov       ax,[esi+32h]
  0045a0fb  0fbf4e16              movsx     ecx,word [esi+16h]
  0045a0ff  0fbfd0                movsx     edx,ax
  0045a102  81c100040000          add       ecx,400h
  0045a108  3bd1                  cmp       edx,ecx
  0045a10a  0f8e03010000          jle       near 0045A213h
  0045a110  a0fb965e00            mov       al,[5E96FBh]
  0045a115  24fd                  and       al,0FDh
  0045a117  a2fb965e00            mov       [5E96FBh],al
  0045a11c  668b4616              mov       ax,[esi+16h]
  0045a120  66050004              add       ax,400h
  0045a124  66c746100000          mov       word [esi+10h],0
  0045a12a  663d0010              cmp       ax,1000h
  0045a12e  66894632              mov       [esi+32h],ax
  0045a132  7c09                  jl        short 0045A13Dh
  0045a134  0500f0ffff            add       eax,0FFFFF000h
  0045a139  66894632              mov       [esi+32h],ax

LAB_0045a13d:
  0045a13d  66396e32              cmp       [esi+32h],bp
  0045a141  7536                  jne       short 0045A179h
  0045a143  0fbf4e14              movsx     ecx,word [esi+14h]
  0045a147  d1e1                  shl       ecx,1
  0045a149  b001                  mov       al,1
  0045a14b  d2e0                  shl       al,cl
  0045a14d  8a0dfd965e00          mov       cl,[5E96FDh]
  0045a153  f6d0                  not       al
  0045a155  22c8                  and       cl,al
  0045a157  880dfd965e00          mov       [5E96FDh],cl
  0045a15d  0fbf4614              movsx     eax,word [esi+14h]
  0045a161  8d4c0001              lea       ecx,[eax+eax+1]
  0045a165  b001                  mov       al,1
  0045a167  d2e0                  shl       al,cl
  0045a169  8a0dfd965e00          mov       cl,[5E96FDh]
  0045a16f  f6d0                  not       al
  0045a171  22c8                  and       cl,al
  0045a173  880dfd965e00          mov       [5E96FDh],cl

LAB_0045a179:
  0045a179  66817e320006          cmp       word [esi+32h],600h
  0045a17f  7534                  jne       short 0045A1B5h
  0045a181  0fbf4e14              movsx     ecx,word [esi+14h]
  0045a185  d1e1                  shl       ecx,1
  0045a187  b001                  mov       al,1
  0045a189  d2e0                  shl       al,cl
  0045a18b  8a0dfd965e00          mov       cl,[5E96FDh]
  0045a191  0ac8                  or        cl,al
  0045a193  b001                  mov       al,1
  0045a195  880dfd965e00          mov       [5E96FDh],cl
  0045a19b  0fbf4e14              movsx     ecx,word [esi+14h]
  0045a19f  8d4c0901              lea       ecx,[ecx+ecx+1]
  0045a1a3  d2e0                  shl       al,cl
  0045a1a5  8a0dfd965e00          mov       cl,[5E96FDh]
  0045a1ab  f6d0                  not       al
  0045a1ad  22c8                  and       cl,al
  0045a1af  880dfd965e00          mov       [5E96FDh],cl

LAB_0045a1b5:
  0045a1b5  66817e320002          cmp       word [esi+32h],200h
  0045a1bb  752c                  jne       short 0045A1E9h
  0045a1bd  0fbf4e14              movsx     ecx,word [esi+14h]
  0045a1c1  d1e1                  shl       ecx,1
  0045a1c3  b001                  mov       al,1
  0045a1c5  d2e0                  shl       al,cl
  0045a1c7  8a0dfd965e00          mov       cl,[5E96FDh]
  0045a1cd  f6d0                  not       al
  0045a1cf  22c8                  and       cl,al
  0045a1d1  b001                  mov       al,1
  0045a1d3  880dfd965e00          mov       [5E96FDh],cl
  0045a1d9  0fbf5614              movsx     edx,word [esi+14h]
  0045a1dd  8d4c1201              lea       ecx,[edx+edx+1]
  0045a1e1  d2e0                  shl       al,cl
  0045a1e3  0805fd965e00          or        [5E96FDh],al

LAB_0045a1e9:
  0045a1e9  66817e32000e          cmp       word [esi+32h],0E00h
  0045a1ef  7522                  jne       short 0045A213h
  0045a1f1  0fbf4e14              movsx     ecx,word [esi+14h]
  0045a1f5  d1e1                  shl       ecx,1
  0045a1f7  b001                  mov       al,1
  0045a1f9  d2e0                  shl       al,cl
  0045a1fb  0805fd965e00          or        [5E96FDh],al
  0045a201  0fbf4614              movsx     eax,word [esi+14h]
  0045a205  8d4c0001              lea       ecx,[eax+eax+1]
  0045a209  b001                  mov       al,1
  0045a20b  d2e0                  shl       al,cl
  0045a20d  0805fd965e00          or        [5E96FDh],al

LAB_0045a213:
  0045a213  5e                    pop       esi
  0045a214  5d                    pop       ebp
  0045a215  c3                    ret
  0045a216  90                    nop
  0045a217  90                    nop
  0045a218  90                    nop
  0045a219  90                    nop
  0045a21a  90                    nop
  0045a21b  90                    nop
  0045a21c  90                    nop
  0045a21d  90                    nop
  0045a21e  90                    nop
  0045a21f  90                    nop

24 cible(s) de saut interne : 0x00459fce, 0x00459fd8, 0x00459fe2, 0x00459fec, 0x00459ffa, 0x0045a004, 0x0045a00e, 0x0045a018, 0x0045a026, 0x0045a030, 0x0045a03a, 0x0045a044, 0x0045a052, 0x0045a05c, 0x0045a066, 0x0045a070, 0x0045a079, 0x0045a09c, 0x0045a0ef, 0x0045a13d, 0x0045a179, 0x0045a1b5, 0x0045a1e9, 0x0045a213
