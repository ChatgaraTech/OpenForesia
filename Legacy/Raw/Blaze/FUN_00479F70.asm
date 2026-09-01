Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00479f70  section .text  offset fichier 0x79370  392 octets

  00479f70  51                    push      ecx
  00479f71  53                    push      ebx
  00479f72  55                    push      ebp
  00479f73  56                    push      esi
  00479f74  8b742414              mov       esi,[esp+14h]
  00479f78  57                    push      edi
  00479f79  8b06                  mov       eax,[esi]
  00479f7b  a900000002            test      eax,2000000h
  00479f80  740c                  je        short 00479F8Eh
  00479f82  c70600000000          mov       dword [esi],0
  00479f88  5f                    pop       edi
  00479f89  5e                    pop       esi
  00479f8a  5d                    pop       ebp
  00479f8b  5b                    pop       ebx
  00479f8c  59                    pop       ecx
  00479f8d  c3                    ret

LAB_00479f8e:
  00479f8e  668b4e10              mov       cx,[esi+10h]
  00479f92  33ed                  xor       ebp,ebp
  00479f94  663bcd                cmp       cx,bp
  00479f97  0f85af000000          jne       near 0047A04Ch
  00479f9d  8b5674                mov       edx,[esi+74h]
  00479fa0  8b461c                mov       eax,[esi+1Ch]
  00479fa3  8b4e24                mov       ecx,[esi+24h]
  00479fa6  52                    push      edx
  00479fa7  0fbf7e32              movsx     edi,word [esi+32h]
  00479fab  66c746100100          mov       word [esi+10h],1
  00479fb1  8944241c              mov       [esp+1Ch],eax
  00479fb5  894c2414              mov       [esp+14h],ecx
  00479fb9  e802ec0b00            call      00538BC0h
  00479fbe  8b5660                mov       edx,[esi+60h]
  00479fc1  8bd8                  mov       ebx,eax
  00479fc3  83c404                add       esp,4
  00479fc6  66896e12              mov       [esi+12h],bp
  00479fca  8d431e                lea       eax,[ebx+1Eh]
  00479fcd  c6460439              mov       byte [esi+4],39h
  00479fd1  c1e00c                shl       eax,0Ch
  00479fd4  03c2                  add       eax,edx
  00479fd6  bd03000000            mov       ebp,3
  00479fdb  d1fb                  sar       ebx,1
  00479fdd  668146320004          add       word [esi+32h],400h
  00479fe3  894620                mov       [esi+20h],eax

LAB_00479fe6:
  00479fe6  57                    push      edi
  00479fe7  e830ba0c00            call      00545A1Ch
  00479fec  0fafc3                imul      eax,ebx
  00479fef  8b4c2418              mov       ecx,[esp+18h]
  00479ff3  57                    push      edi
  00479ff4  03c1                  add       eax,ecx
  00479ff6  89461c                mov       [esi+1Ch],eax
  00479ff9  e818ba0c00            call      00545A16h
  00479ffe  0fafc3                imul      eax,ebx
  0047a001  8b542410              mov       edx,[esp+10h]
  0047a005  33c9                  xor       ecx,ecx
  0047a007  8a4e0b                mov       cl,[esi+0Bh]
  0047a00a  6aff                  push      0FFFFFFFFh
  0047a00c  51                    push      ecx
  0047a00d  03c2                  add       eax,edx
  0047a00f  6a7f                  push      7Fh
  0047a011  56                    push      esi
  0047a012  894624                mov       [esi+24h],eax
  0047a015  e8c60a0000            call      0047AAE0h
  0047a01a  668146125505          add       word [esi+12h],555h
  0047a020  83c410                add       esp,10h
  0047a023  81c755050000          add       edi,555h
  0047a029  4d                    dec       ebp
  0047a02a  75ba                  jne       short 00479FE6h
  0047a02c  8d561c                lea       edx,[esi+1Ch]
  0047a02f  52                    push      edx
  0047a030  6878195500            push      551978h
  0047a035  e8a63b0c00            call      0053DBE0h
  0047a03a  8b7674                mov       esi,[esi+74h]
  0047a03d  83c408                add       esp,8
  0047a040  8b06                  mov       eax,[esi]
  0047a042  0c80                  or        al,80h
  0047a044  8906                  mov       [esi],eax
  0047a046  5f                    pop       edi
  0047a047  5e                    pop       esi
  0047a048  5d                    pop       ebp
  0047a049  5b                    pop       ebx
  0047a04a  59                    pop       ecx
  0047a04b  c3                    ret

LAB_0047a04c:
  0047a04c  a802                  test      al,2
  0047a04e  0f849e000000          je        near 0047A0F2h
  0047a054  a900008000            test      eax,800000h
  0047a059  7477                  je        short 0047A0D2h
  0047a05b  8b4e74                mov       ecx,[esi+74h]
  0047a05e  8b3db4876000          mov       edi,[6087B4h]
  0047a064  33d2                  xor       edx,edx
  0047a066  8a5108                mov       dl,[ecx+8]
  0047a069  8b1f                  mov       ebx,[edi]
  0047a06b  8d4a78                lea       ecx,[edx+78h]
  0047a06e  8d3cd2                lea       edi,[edx+edx*8]
  0047a071  c1e10d                shl       ecx,0Dh
  0047a074  03cb                  add       ecx,ebx
  0047a076  8d147a                lea       edx,[edx+edi*2]
  0047a079  33db                  xor       ebx,ebx
  0047a07b  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0047a082  c1e304                shl       ebx,4
  0047a085  668b93204a6000        mov       dx,[ebx+604A20h]
  0047a08c  668591c41f0000        test      [ecx+1FC4h],dx
  0047a093  7507                  jne       short 0047A09Ch
  0047a095  0d00000002            or        eax,2000000h
  0047a09a  8906                  mov       [esi],eax

LAB_0047a09c:
  0047a09c  a1f02c5f00            mov       eax,[5F2CF0h]
  0047a0a1  33d2                  xor       edx,edx
  0047a0a3  bf05000000            mov       edi,5
  0047a0a8  f7f7                  div       edi
  0047a0aa  85d2                  test      edx,edx
  0047a0ac  7531                  jne       short 0047A0DFh
  0047a0ae  668b814e010000        mov       ax,[ecx+14Eh]
  0047a0b5  663d0200              cmp       ax,2
  0047a0b9  7c0c                  jl        short 0047A0C7h
  0047a0bb  83c0fe                add       eax,0FFFFFFFEh
  0047a0be  6689814e010000        mov       [ecx+14Eh],ax
  0047a0c5  eb18                  jmp       short 0047A0DFh

LAB_0047a0c7:
  0047a0c7  6689a94e010000        mov       [ecx+14Eh],bp
  0047a0ce  8b06                  mov       eax,[esi]
  0047a0d0  eb06                  jmp       short 0047A0D8h

LAB_0047a0d2:
  0047a0d2  6683f978              cmp       cx,78h
  0047a0d6  7c07                  jl        short 0047A0DFh

LAB_0047a0d8:
  0047a0d8  0d00000002            or        eax,2000000h
  0047a0dd  8906                  mov       [esi],eax

LAB_0047a0df:
  0047a0df  56                    push      esi
  0047a0e0  e82bb0ffff            call      00475110h
  0047a0e5  83c404                add       esp,4
  0047a0e8  85c0                  test      eax,eax
  0047a0ea  7406                  je        short 0047A0F2h
  0047a0ec  810e00000002          or        dword [esi],2000000h

LAB_0047a0f2:
  0047a0f2  5f                    pop       edi
  0047a0f3  5e                    pop       esi
  0047a0f4  5d                    pop       ebp
  0047a0f5  5b                    pop       ebx
  0047a0f6  59                    pop       ecx
  0047a0f7  c3                    ret

9 cible(s) de saut interne : 0x00479f8e, 0x00479fe6, 0x0047a04c, 0x0047a09c, 0x0047a0c7, 0x0047a0d2, 0x0047a0d8, 0x0047a0df, 0x0047a0f2
