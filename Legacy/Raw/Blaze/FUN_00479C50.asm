Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00479c50  section .text  offset fichier 0x79050  400 octets

  00479c50  51                    push      ecx
  00479c51  53                    push      ebx
  00479c52  55                    push      ebp
  00479c53  56                    push      esi
  00479c54  8b742414              mov       esi,[esp+14h]
  00479c58  57                    push      edi
  00479c59  8b06                  mov       eax,[esi]
  00479c5b  a900000002            test      eax,2000000h
  00479c60  740c                  je        short 00479C6Eh
  00479c62  c70600000000          mov       dword [esi],0
  00479c68  5f                    pop       edi
  00479c69  5e                    pop       esi
  00479c6a  5d                    pop       ebp
  00479c6b  5b                    pop       ebx
  00479c6c  59                    pop       ecx
  00479c6d  c3                    ret

LAB_00479c6e:
  00479c6e  668b4e10              mov       cx,[esi+10h]
  00479c72  33ed                  xor       ebp,ebp
  00479c74  663bcd                cmp       cx,bp
  00479c77  0f85af000000          jne       near 00479D2Ch
  00479c7d  8b5674                mov       edx,[esi+74h]
  00479c80  8b461c                mov       eax,[esi+1Ch]
  00479c83  8b4e24                mov       ecx,[esi+24h]
  00479c86  52                    push      edx
  00479c87  0fbf7e32              movsx     edi,word [esi+32h]
  00479c8b  66c746100100          mov       word [esi+10h],1
  00479c91  8944241c              mov       [esp+1Ch],eax
  00479c95  894c2414              mov       [esp+14h],ecx
  00479c99  e822ef0b00            call      00538BC0h
  00479c9e  8b5660                mov       edx,[esi+60h]
  00479ca1  8bd8                  mov       ebx,eax
  00479ca3  83c404                add       esp,4
  00479ca6  66896e12              mov       [esi+12h],bp
  00479caa  8d431e                lea       eax,[ebx+1Eh]
  00479cad  c6460439              mov       byte [esi+4],39h
  00479cb1  c1e00c                shl       eax,0Ch
  00479cb4  03c2                  add       eax,edx
  00479cb6  bd03000000            mov       ebp,3
  00479cbb  d1fb                  sar       ebx,1
  00479cbd  668146320004          add       word [esi+32h],400h
  00479cc3  894620                mov       [esi+20h],eax

LAB_00479cc6:
  00479cc6  57                    push      edi
  00479cc7  e850bd0c00            call      00545A1Ch
  00479ccc  0fafc3                imul      eax,ebx
  00479ccf  8b4c2418              mov       ecx,[esp+18h]
  00479cd3  57                    push      edi
  00479cd4  03c1                  add       eax,ecx
  00479cd6  89461c                mov       [esi+1Ch],eax
  00479cd9  e838bd0c00            call      00545A16h
  00479cde  0fafc3                imul      eax,ebx
  00479ce1  8b542410              mov       edx,[esp+10h]
  00479ce5  33c9                  xor       ecx,ecx
  00479ce7  8a4e0b                mov       cl,[esi+0Bh]
  00479cea  6aff                  push      0FFFFFFFFh
  00479cec  51                    push      ecx
  00479ced  03c2                  add       eax,edx
  00479cef  6a7f                  push      7Fh
  00479cf1  56                    push      esi
  00479cf2  894624                mov       [esi+24h],eax
  00479cf5  e8e60d0000            call      0047AAE0h
  00479cfa  668146125505          add       word [esi+12h],555h
  00479d00  83c410                add       esp,10h
  00479d03  81c755050000          add       edi,555h
  00479d09  4d                    dec       ebp
  00479d0a  75ba                  jne       short 00479CC6h
  00479d0c  8d561c                lea       edx,[esi+1Ch]
  00479d0f  52                    push      edx
  00479d10  6840195500            push      551940h
  00479d15  e8c63e0c00            call      0053DBE0h
  00479d1a  8b7674                mov       esi,[esi+74h]
  00479d1d  83c408                add       esp,8
  00479d20  8b06                  mov       eax,[esi]
  00479d22  0c80                  or        al,80h
  00479d24  8906                  mov       [esi],eax
  00479d26  5f                    pop       edi
  00479d27  5e                    pop       esi
  00479d28  5d                    pop       ebp
  00479d29  5b                    pop       ebx
  00479d2a  59                    pop       ecx
  00479d2b  c3                    ret

LAB_00479d2c:
  00479d2c  a802                  test      al,2
  00479d2e  0f849e000000          je        near 00479DD2h
  00479d34  a900008000            test      eax,800000h
  00479d39  7477                  je        short 00479DB2h
  00479d3b  8b4e74                mov       ecx,[esi+74h]
  00479d3e  8b3db4876000          mov       edi,[6087B4h]
  00479d44  33d2                  xor       edx,edx
  00479d46  8a5108                mov       dl,[ecx+8]
  00479d49  8b1f                  mov       ebx,[edi]
  00479d4b  8d4a78                lea       ecx,[edx+78h]
  00479d4e  8d3cd2                lea       edi,[edx+edx*8]
  00479d51  c1e10d                shl       ecx,0Dh
  00479d54  03cb                  add       ecx,ebx
  00479d56  8d147a                lea       edx,[edx+edi*2]
  00479d59  33db                  xor       ebx,ebx
  00479d5b  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  00479d62  c1e304                shl       ebx,4
  00479d65  668b93204a6000        mov       dx,[ebx+604A20h]
  00479d6c  668591c41f0000        test      [ecx+1FC4h],dx
  00479d73  7507                  jne       short 00479D7Ch
  00479d75  0d00000002            or        eax,2000000h
  00479d7a  8906                  mov       [esi],eax

LAB_00479d7c:
  00479d7c  a1f02c5f00            mov       eax,[5F2CF0h]
  00479d81  33d2                  xor       edx,edx
  00479d83  bf05000000            mov       edi,5
  00479d88  f7f7                  div       edi
  00479d8a  85d2                  test      edx,edx
  00479d8c  7531                  jne       short 00479DBFh
  00479d8e  668b814e010000        mov       ax,[ecx+14Eh]
  00479d95  663d0200              cmp       ax,2
  00479d99  7c0c                  jl        short 00479DA7h
  00479d9b  83c0fe                add       eax,0FFFFFFFEh
  00479d9e  6689814e010000        mov       [ecx+14Eh],ax
  00479da5  eb18                  jmp       short 00479DBFh

LAB_00479da7:
  00479da7  6689a94e010000        mov       [ecx+14Eh],bp
  00479dae  8b06                  mov       eax,[esi]
  00479db0  eb06                  jmp       short 00479DB8h

LAB_00479db2:
  00479db2  6683f978              cmp       cx,78h
  00479db6  7c07                  jl        short 00479DBFh

LAB_00479db8:
  00479db8  0d00000002            or        eax,2000000h
  00479dbd  8906                  mov       [esi],eax

LAB_00479dbf:
  00479dbf  56                    push      esi
  00479dc0  e84bb3ffff            call      00475110h
  00479dc5  83c404                add       esp,4
  00479dc8  85c0                  test      eax,eax
  00479dca  7406                  je        short 00479DD2h
  00479dcc  810e00000002          or        dword [esi],2000000h

LAB_00479dd2:
  00479dd2  5f                    pop       edi
  00479dd3  5e                    pop       esi
  00479dd4  5d                    pop       ebp
  00479dd5  5b                    pop       ebx
  00479dd6  59                    pop       ecx
  00479dd7  c3                    ret
  00479dd8  90                    nop
  00479dd9  90                    nop
  00479dda  90                    nop
  00479ddb  90                    nop
  00479ddc  90                    nop
  00479ddd  90                    nop
  00479dde  90                    nop
  00479ddf  90                    nop

9 cible(s) de saut interne : 0x00479c6e, 0x00479cc6, 0x00479d2c, 0x00479d7c, 0x00479da7, 0x00479db2, 0x00479db8, 0x00479dbf, 0x00479dd2
