Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040e3a0  section .text  offset fichier 0xd7a0  848 octets

  0040e3a0  81eca0000000          sub       esp,0A0h
  0040e3a6  53                    push      ebx
  0040e3a7  56                    push      esi
  0040e3a8  57                    push      edi
  0040e3a9  b926000000            mov       ecx,26h
  0040e3ae  33c0                  xor       eax,eax
  0040e3b0  8d7c2414              lea       edi,[esp+14h]
  0040e3b4  c744241000000000      mov       dword [esp+10h],0
  0040e3bc  f3ab                  rep stosd
  0040e3be  e88dcb0000            call      0041AF50h
  0040e3c3  e878020000            call      0040E640h
  0040e3c8  8d7c2410              lea       edi,[esp+10h]
  0040e3cc  b8a0ca5e00            mov       eax,5ECAA0h
  0040e3d1  b920000000            mov       ecx,20h
  0040e3d6  bb01000000            mov       ebx,1

LAB_0040e3db:
  0040e3db  8a500c                mov       dl,[eax+0Ch]
  0040e3de  84d2                  test      dl,dl
  0040e3e0  7528                  jne       short 0040E40Ah
  0040e3e2  f70000000080          test      dword [eax],80000000h
  0040e3e8  7420                  je        short 0040E40Ah
  0040e3ea  33d2                  xor       edx,edx
  0040e3ec  66895818              mov       [eax+18h],bx
  0040e3f0  8a5004                mov       dl,[eax+4]
  0040e3f3  8bf8                  mov       edi,eax
  0040e3f5  8b149540965e00        mov       edx,[edx*4+5E9640h]
  0040e3fc  8b5204                mov       edx,[edx+4]
  0040e3ff  8954240c              mov       [esp+0Ch],edx
  0040e403  8b10                  mov       edx,[eax]
  0040e405  80ce10                or        dh,10h
  0040e408  8910                  mov       [eax],edx

LAB_0040e40a:
  0040e40a  059c000000            add       eax,9Ch
  0040e40f  49                    dec       ecx
  0040e410  75c9                  jne       short 0040E3DBh
  0040e412  8bb424b0000000        mov       esi,[esp+0B0h]
  0040e419  33c0                  xor       eax,eax
  0040e41b  66394610              cmp       [esi+10h],ax
  0040e41f  7558                  jne       short 0040E479h
  0040e421  66895e10              mov       [esi+10h],bx
  0040e425  66894612              mov       [esi+12h],ax
  0040e429  66894614              mov       [esi+14h],ax
  0040e42d  66894616              mov       [esi+16h],ax
  0040e431  0fbf0db8925e00        movsx     ecx,word [5E92B8h]
  0040e438  894e50                mov       [esi+50h],ecx
  0040e43b  0fbf15ba925e00        movsx     edx,word [5E92BAh]
  0040e442  895654                mov       [esi+54h],edx
  0040e445  0fbf0dbc925e00        movsx     ecx,word [5E92BCh]
  0040e44c  894e58                mov       [esi+58h],ecx
  0040e44f  6689471a              mov       [edi+1Ah],ax
  0040e453  88470a                mov       [edi+0Ah],al
  0040e456  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0040e45d  66895f36              mov       [edi+36h],bx
  0040e461  66c78698000000ffff    mov       word [esi+98h],0FFFFh
  0040e46a  66c74614000e          mov       word [esi+14h],0E00h
  0040e470  8a5709                mov       dl,[edi+9]
  0040e473  881518145700          mov       [571418h],dl

LAB_0040e479:
  0040e479  668b471a              mov       ax,[edi+1Ah]
  0040e47d  663d0f00              cmp       ax,0Fh
  0040e481  7406                  je        short 0040E489h
  0040e483  663d2200              cmp       ax,22h
  0040e487  7533                  jne       short 0040E4BCh

LAB_0040e489:
  0040e489  0fbf4614              movsx     eax,word [esi+14h]
  0040e48d  50                    push      eax
  0040e48e  50                    push      eax
  0040e48f  6800040000            push      400h
  0040e494  6a1f                  push      1Fh
  0040e496  e805ea1200            call      0053CEA0h
  0040e49b  0fbf4614              movsx     eax,word [esi+14h]
  0040e49f  83c410                add       esp,10h
  0040e4a2  50                    push      eax
  0040e4a3  50                    push      eax
  0040e4a4  686a010000            push      16Ah
  0040e4a9  6a21                  push      21h
  0040e4ab  e8f0e91200            call      0053CEA0h
  0040e4b0  83c410                add       esp,10h
  0040e4b3  66c786980000000200    mov       word [esi+98h],2

LAB_0040e4bc:
  0040e4bc  6683be9800000000      cmp       word [esi+98h],0
  0040e4c4  7529                  jne       short 0040E4EFh
  0040e4c6  0fbf4614              movsx     eax,word [esi+14h]
  0040e4ca  0500feffff            add       eax,0FFFFFE00h
  0040e4cf  66c78698000000ffff    mov       word [esi+98h],0FFFFh
  0040e4d8  50                    push      eax
  0040e4d9  50                    push      eax
  0040e4da  68d3030000            push      3D3h
  0040e4df  6a1f                  push      1Fh
  0040e4e1  e8bae91200            call      0053CEA0h
  0040e4e6  83c410                add       esp,10h
  0040e4e9  6681461400ff          add       word [esi+14h],0FF00h

LAB_0040e4ef:
  0040e4ef  66ff8e98000000        dec       word [esi+98h]
  0040e4f6  668b8698000000        mov       ax,[esi+98h]
  0040e4fd  663de2ff              cmp       ax,0FFE2h
  0040e501  7d09                  jge       short 0040E50Ch
  0040e503  66c78698000000e2ff    mov       word [esi+98h],0FFE2h

LAB_0040e50c:
  0040e50c  66395e10              cmp       [esi+10h],bx
  0040e510  7559                  jne       short 0040E56Bh
  0040e512  0fbf4732              movsx     eax,word [edi+32h]
  0040e516  50                    push      eax
  0040e517  e800751300            call      00545A1Ch
  0040e51c  8b4f1c                mov       ecx,[edi+1Ch]
  0040e51f  d1e0                  shl       eax,1
  0040e521  03c8                  add       ecx,eax
  0040e523  894f1c                mov       [edi+1Ch],ecx
  0040e526  0fbf4f32              movsx     ecx,word [edi+32h]
  0040e52a  51                    push      ecx
  0040e52b  e8e6741300            call      00545A16h
  0040e530  8b5724                mov       edx,[edi+24h]
  0040e533  8b4f20                mov       ecx,[edi+20h]
  0040e536  d1e0                  shl       eax,1
  0040e538  03d0                  add       edx,eax
  0040e53a  81c100500000          add       ecx,5000h
  0040e540  895724                mov       [edi+24h],edx
  0040e543  894f20                mov       [edi+20h],ecx
  0040e546  66837e1628            cmp       word [esi+16h],28h
  0040e54b  7e1e                  jle       short 0040E56Bh
  0040e54d  66c746100200          mov       word [esi+10h],2
  0040e553  8b571c                mov       edx,[edi+1Ch]
  0040e556  89561c                mov       [esi+1Ch],edx
  0040e559  8b4720                mov       eax,[edi+20h]
  0040e55c  894620                mov       [esi+20h],eax
  0040e55f  8b4f24                mov       ecx,[edi+24h]
  0040e562  894e24                mov       [esi+24h],ecx
  0040e565  66c746120000          mov       word [esi+12h],0

LAB_0040e56b:
  0040e56b  66837e1002            cmp       word [esi+10h],2
  0040e570  b803000000            mov       eax,3
  0040e575  7515                  jne       short 0040E58Ch
  0040e577  66ff4612              inc       word [esi+12h]
  0040e57b  66837e1214            cmp       word [esi+12h],14h
  0040e580  7e0a                  jle       short 0040E58Ch
  0040e582  66894610              mov       [esi+10h],ax
  0040e586  66c746120000          mov       word [esi+12h],0

LAB_0040e58c:
  0040e58c  66394610              cmp       [esi+10h],ax
  0040e590  7575                  jne       short 0040E607h
  0040e592  0fbf5732              movsx     edx,word [edi+32h]
  0040e596  52                    push      edx
  0040e597  e880741300            call      00545A1Ch
  0040e59c  0fbf4f32              movsx     ecx,word [edi+32h]
  0040e5a0  8b5f1c                mov       ebx,[edi+1Ch]
  0040e5a3  8d0480                lea       eax,[eax+eax*4]
  0040e5a6  d1e0                  shl       eax,1
  0040e5a8  03d8                  add       ebx,eax
  0040e5aa  51                    push      ecx
  0040e5ab  895f1c                mov       [edi+1Ch],ebx
  0040e5ae  e863741300            call      00545A16h
  0040e5b3  8b5f24                mov       ebx,[edi+24h]
  0040e5b6  8d1480                lea       edx,[eax+eax*4]
  0040e5b9  d1e2                  shl       edx,1
  0040e5bb  03da                  add       ebx,edx
  0040e5bd  8b5720                mov       edx,[edi+20h]
  0040e5c0  81c200300000          add       edx,3000h
  0040e5c6  895f24                mov       [edi+24h],ebx
  0040e5c9  895720                mov       [edi+20h],edx
  0040e5cc  66ff4612              inc       word [esi+12h]
  0040e5d0  66837e1264            cmp       word [esi+12h],64h
  0040e5d5  7e30                  jle       short 0040E607h
  0040e5d7  6aff                  push      0FFFFFFFFh
  0040e5d9  6a00                  push      0
  0040e5db  68ff000000            push      0FFh
  0040e5e0  56                    push      esi
  0040e5e1  e8eac30600            call      0047A9D0h
  0040e5e6  8b0e                  mov       ecx,[esi]
  0040e5e8  83c410                add       esp,10h
  0040e5eb  81e1ffffff7f          and       ecx,7FFFFFFFh
  0040e5f1  890e                  mov       [esi],ecx
  0040e5f3  8b07                  mov       eax,[edi]
  0040e5f5  0d00400002            or        eax,2004000h
  0040e5fa  8907                  mov       [edi],eax
  0040e5fc  b800803801            mov       eax,1388000h
  0040e601  89471c                mov       [edi+1Ch],eax
  0040e604  894724                mov       [edi+24h],eax

LAB_0040e607:
  0040e607  66ff471a              inc       word [edi+1Ah]
  0040e60b  668b471a              mov       ax,[edi+1Ah]
  0040e60f  8b54240c              mov       edx,[esp+0Ch]
  0040e613  33c9                  xor       ecx,ecx
  0040e615  0fbfc0                movsx     eax,ax
  0040e618  668b4a16              mov       cx,[edx+16h]
  0040e61c  3bc1                  cmp       eax,ecx
  0040e61e  7c06                  jl        short 0040E626h
  0040e620  66c7471a0000          mov       word [edi+1Ah],0

LAB_0040e626:
  0040e626  66ff4616              inc       word [esi+16h]
  0040e62a  5f                    pop       edi
  0040e62b  5e                    pop       esi
  0040e62c  5b                    pop       ebx
  0040e62d  81c4a0000000          add       esp,0A0h
  0040e633  c3                    ret
  0040e634  90                    nop
  0040e635  90                    nop
  0040e636  90                    nop
  0040e637  90                    nop
  0040e638  90                    nop
  0040e639  90                    nop
  0040e63a  90                    nop
  0040e63b  90                    nop
  0040e63c  90                    nop
  0040e63d  90                    nop
  0040e63e  90                    nop
  0040e63f  90                    nop
  0040e640  53                    push      ebx
  0040e641  56                    push      esi
  0040e642  b800f45f00            mov       eax,5FF400h
  0040e647  ba20000000            mov       edx,20h
  0040e64c  b317                  mov       bl,17h

LAB_0040e64e:
  0040e64e  8b08                  mov       ecx,[eax]
  0040e650  f7c100000010          test      ecx,10000000h
  0040e656  7508                  jne       short 0040E660h
  0040e658  81e1fffffffb          and       ecx,0FBFFFFFFh
  0040e65e  8908                  mov       [eax],ecx

LAB_0040e660:
  0040e660  8078051c              cmp       byte [eax+5],1Ch
  0040e664  7506                  jne       short 0040E66Ch
  0040e666  810800000040          or        dword [eax],40000000h

LAB_0040e66c:
  0040e66c  385805                cmp       [eax+5],bl
  0040e66f  7506                  jne       short 0040E677h
  0040e671  810800000040          or        dword [eax],40000000h

LAB_0040e677:
  0040e677  059c000000            add       eax,9Ch
  0040e67c  4a                    dec       edx
  0040e67d  75cf                  jne       short 0040E64Eh
  0040e67f  b9402d5f00            mov       ecx,5F2D40h
  0040e684  ba80000000            mov       edx,80h

LAB_0040e689:
  0040e689  8b01                  mov       eax,[ecx]
  0040e68b  a900000010            test      eax,10000000h
  0040e690  7507                  jne       short 0040E699h
  0040e692  25fffffffb            and       eax,0FBFFFFFFh
  0040e697  8901                  mov       [ecx],eax

LAB_0040e699:
  0040e699  81c19c000000          add       ecx,9Ch
  0040e69f  4a                    dec       edx
  0040e6a0  75e7                  jne       short 0040E689h
  0040e6a2  b9e0de5e00            mov       ecx,5EDEE0h
  0040e6a7  ba80000000            mov       edx,80h

LAB_0040e6ac:
  0040e6ac  8b01                  mov       eax,[ecx]
  0040e6ae  a900000010            test      eax,10000000h
  0040e6b3  7507                  jne       short 0040E6BCh
  0040e6b5  25fffffffb            and       eax,0FBFFFFFFh
  0040e6ba  8901                  mov       [ecx],eax

LAB_0040e6bc:
  0040e6bc  81c19c000000          add       ecx,9Ch
  0040e6c2  4a                    dec       edx
  0040e6c3  75e7                  jne       short 0040E6ACh
  0040e6c5  b8f9935e00            mov       eax,5E93F9h
  0040e6ca  b904000000            mov       ecx,4
  0040e6cf  b2ff                  mov       dl,0FFh
  0040e6d1  befbffffff            mov       esi,0FFFFFFFBh

LAB_0040e6d6:
  0040e6d6  8850ff                mov       [eax-1],dl
  0040e6d9  8810                  mov       [eax],dl
  0040e6db  885001                mov       [eax+1],dl
  0040e6de  662170fd              and       [eax-3],si
  0040e6e2  059c000000            add       eax,9Ch
  0040e6e7  49                    dec       ecx
  0040e6e8  75ec                  jne       short 0040E6D6h
  0040e6ea  5e                    pop       esi
  0040e6eb  5b                    pop       ebx
  0040e6ec  c3                    ret
  0040e6ed  90                    nop
  0040e6ee  90                    nop
  0040e6ef  90                    nop

20 cible(s) de saut interne : 0x0040e3db, 0x0040e40a, 0x0040e479, 0x0040e489, 0x0040e4bc, 0x0040e4ef, 0x0040e50c, 0x0040e56b, 0x0040e58c, 0x0040e607, 0x0040e626, 0x0040e64e, 0x0040e660, 0x0040e66c, 0x0040e677, 0x0040e689, 0x0040e699, 0x0040e6ac, 0x0040e6bc, 0x0040e6d6
