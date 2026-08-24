Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004736d0  section .text  offset fichier 0x72ad0  464 octets

  004736d0  55                    push      ebp
  004736d1  56                    push      esi
  004736d2  8b74240c              mov       esi,[esp+0Ch]
  004736d6  57                    push      edi
  004736d7  bf02000000            mov       edi,2
  004736dc  6a03                  push      3
  004736de  57                    push      edi
  004736df  56                    push      esi
  004736e0  e8ebf0ffff            call      004727D0h
  004736e5  83c40c                add       esp,0Ch
  004736e8  663d0100              cmp       ax,1
  004736ec  0f849c010000          je        near 0047388Eh
  004736f2  668b5614              mov       dx,[esi+14h]
  004736f6  6683fa01              cmp       dx,1
  004736fa  7515                  jne       short 00473711h
  004736fc  66c746120000          mov       word [esi+12h],0
  00473702  c7465c0f000000        mov       dword [esi+5Ch],0Fh
  00473709  66897e14              mov       [esi+14h],di
  0047370d  5f                    pop       edi
  0047370e  5e                    pop       esi
  0047370f  5d                    pop       ebp
  00473710  c3                    ret

LAB_00473711:
  00473711  663bd7                cmp       dx,di
  00473714  7546                  jne       short 0047375Ch
  00473716  668b4e12              mov       cx,[esi+12h]
  0047371a  8b465c                mov       eax,[esi+5Ch]
  0047371d  6683f90c              cmp       cx,0Ch
  00473721  8be8                  mov       ebp,eax
  00473723  7e0a                  jle       short 0047372Fh
  00473725  c1fd03                sar       ebp,3
  00473728  2bc5                  sub       eax,ebp
  0047372a  89465c                mov       [esi+5Ch],eax
  0047372d  eb08                  jmp       short 00473737h

LAB_0047372f:
  0047372f  c1fd03                sar       ebp,3
  00473732  03e8                  add       ebp,eax
  00473734  896e5c                mov       [esi+5Ch],ebp

LAB_00473737:
  00473737  8d4101                lea       eax,[ecx+1]
  0047373a  663d2800              cmp       ax,28h
  0047373e  66894612              mov       [esi+12h],ax
  00473742  7510                  jne       short 00473754h
  00473744  66c746160100          mov       word [esi+16h],1
  0047374a  66c746140500          mov       word [esi+14h],5
  00473750  5f                    pop       edi
  00473751  5e                    pop       esi
  00473752  5d                    pop       ebp
  00473753  c3                    ret

LAB_00473754:
  00473754  668b465c              mov       ax,[esi+5Ch]
  00473758  66014632              add       [esi+32h],ax

LAB_0047375c:
  0047375c  6681fac900            cmp       dx,0C9h
  00473761  7517                  jne       short 0047377Ah
  00473763  66c74614ca00          mov       word [esi+14h],0CAh
  00473769  66c746120000          mov       word [esi+12h],0
  0047376f  c7465c0f000000        mov       dword [esi+5Ch],0Fh
  00473776  5f                    pop       edi
  00473777  5e                    pop       esi
  00473778  5d                    pop       ebp
  00473779  c3                    ret

LAB_0047377a:
  0047377a  6681faca00            cmp       dx,0CAh
  0047377f  7540                  jne       short 004737C1h
  00473781  668b4e12              mov       cx,[esi+12h]
  00473785  8b465c                mov       eax,[esi+5Ch]
  00473788  6683f90c              cmp       cx,0Ch
  0047378c  8bd0                  mov       edx,eax
  0047378e  7e0a                  jle       short 0047379Ah
  00473790  c1fa03                sar       edx,3
  00473793  2bc2                  sub       eax,edx
  00473795  89465c                mov       [esi+5Ch],eax
  00473798  eb08                  jmp       short 004737A2h

LAB_0047379a:
  0047379a  c1fa03                sar       edx,3
  0047379d  03d0                  add       edx,eax
  0047379f  89565c                mov       [esi+5Ch],edx

LAB_004737a2:
  004737a2  8d4101                lea       eax,[ecx+1]
  004737a5  663d2800              cmp       ax,28h
  004737a9  66894612              mov       [esi+12h],ax
  004737ad  750a                  jne       short 004737B9h
  004737af  66c74614cd00          mov       word [esi+14h],0CDh
  004737b5  66897e16              mov       [esi+16h],di

LAB_004737b9:
  004737b9  668b465c              mov       ax,[esi+5Ch]
  004737bd  66014632              add       [esi+32h],ax

LAB_004737c1:
  004737c1  33d2                  xor       edx,edx
  004737c3  66837e140b            cmp       word [esi+14h],0Bh
  004737c8  750f                  jne       short 004737D9h
  004737ca  66c746140c00          mov       word [esi+14h],0Ch
  004737d0  66c746120500          mov       word [esi+12h],5
  004737d6  89565c                mov       [esi+5Ch],edx

LAB_004737d9:
  004737d9  668b4e14              mov       cx,[esi+14h]
  004737dd  6683f90c              cmp       cx,0Ch
  004737e1  753c                  jne       short 0047381Fh
  004737e3  668b4612              mov       ax,[esi+12h]
  004737e7  8b6e5c                mov       ebp,[esi+5Ch]
  004737ea  66294632              sub       [esi+32h],ax
  004737ee  0fbff8                movsx     edi,ax
  004737f1  03ef                  add       ebp,edi
  004737f3  40                    inc       eax
  004737f4  66894612              mov       [esi+12h],ax
  004737f8  8bc5                  mov       eax,ebp
  004737fa  3d73030000            cmp       eax,373h
  004737ff  896e5c                mov       [esi+5Ch],ebp
  00473802  7c1b                  jl        short 0047381Fh
  00473804  668b4e5c              mov       cx,[esi+5Ch]
  00473808  66c746140f00          mov       word [esi+14h],0Fh
  0047380e  6681e97303            sub       cx,373h
  00473813  66895616              mov       [esi+16h],dx
  00473817  66014e32              add       [esi+32h],cx
  0047381b  5f                    pop       edi
  0047381c  5e                    pop       esi
  0047381d  5d                    pop       ebp
  0047381e  c3                    ret

LAB_0047381f:
  0047381f  6683f915              cmp       cx,15h
  00473823  b816000000            mov       eax,16h
  00473828  750f                  jne       short 00473839h
  0047382a  66895612              mov       [esi+12h],dx
  0047382e  c7465c0f000000        mov       dword [esi+5Ch],0Fh
  00473835  66894614              mov       [esi+14h],ax

LAB_00473839:
  00473839  66394614              cmp       [esi+14h],ax
  0047383d  754f                  jne       short 0047388Eh
  0047383f  668b4e12              mov       cx,[esi+12h]
  00473843  8b465c                mov       eax,[esi+5Ch]
  00473846  6683f90c              cmp       cx,0Ch
  0047384a  8bd0                  mov       edx,eax
  0047384c  7e0a                  jle       short 00473858h
  0047384e  c1fa03                sar       edx,3
  00473851  2bc2                  sub       eax,edx
  00473853  89465c                mov       [esi+5Ch],eax
  00473856  eb08                  jmp       short 00473860h

LAB_00473858:
  00473858  c1fa03                sar       edx,3
  0047385b  03d0                  add       edx,eax
  0047385d  89565c                mov       [esi+5Ch],edx

LAB_00473860:
  00473860  8d4101                lea       eax,[ecx+1]
  00473863  663d2800              cmp       ax,28h
  00473867  66894612              mov       [esi+12h],ax
  0047386b  7519                  jne       short 00473886h
  0047386d  8b06                  mov       eax,[esi]
  0047386f  66c746160100          mov       word [esi+16h],1
  00473875  0d00000040            or        eax,40000000h
  0047387a  66c746141900          mov       word [esi+14h],19h
  00473880  8906                  mov       [esi],eax
  00473882  5f                    pop       edi
  00473883  5e                    pop       esi
  00473884  5d                    pop       ebp
  00473885  c3                    ret

LAB_00473886:
  00473886  668b465c              mov       ax,[esi+5Ch]
  0047388a  66014632              add       [esi+32h],ax

LAB_0047388e:
  0047388e  5f                    pop       edi
  0047388f  5e                    pop       esi
  00473890  5d                    pop       ebp
  00473891  c3                    ret
  00473892  90                    nop
  00473893  90                    nop
  00473894  90                    nop
  00473895  90                    nop
  00473896  90                    nop
  00473897  90                    nop
  00473898  90                    nop
  00473899  90                    nop
  0047389a  90                    nop
  0047389b  90                    nop
  0047389c  90                    nop
  0047389d  90                    nop
  0047389e  90                    nop
  0047389f  90                    nop

17 cible(s) de saut interne : 0x00473711, 0x0047372f, 0x00473737, 0x00473754, 0x0047375c, 0x0047377a, 0x0047379a, 0x004737a2, 0x004737b9, 0x004737c1, 0x004737d9, 0x0047381f, 0x00473839, 0x00473858, 0x00473860, 0x00473886, 0x0047388e
