
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047bf00  section .text  offset fichier 0x7b300  724 octets

  0047bf00  83ec0c                sub       esp,0Ch
  0047bf03  56                    push      esi
  0047bf04  8b742414              mov       esi,[esp+14h]
  0047bf08  57                    push      edi
  0047bf09  33ff                  xor       edi,edi
  0047bf0b  8b06                  mov       eax,[esi]
  0047bf0d  66897c2408            mov       [esp+8],di
  0047bf12  a900000002            test      eax,2000000h
  0047bf17  66c744240abaff        mov       word [esp+0Ah],0FFBAh
  0047bf1e  66c744240c4600        mov       word [esp+0Ch],46h
  0047bf25  66c744240e6400        mov       word [esp+0Eh],64h
  0047bf2c  66897c2410            mov       [esp+10h],di
  0047bf31  66c74424128c00        mov       word [esp+12h],8Ch
  0047bf38  7419                  je        short 0047BF53h
  0047bf3a  8b4674                mov       eax,[esi+74h]
  0047bf3d  8b08                  mov       ecx,[eax]
  0047bf3f  80e17f                and       cl,7Fh
  0047bf42  8908                  mov       [eax],ecx
  0047bf44  893e                  mov       [esi],edi
  0047bf46  66893d66915e00        mov       [5E9166h],di
  0047bf4d  5f                    pop       edi
  0047bf4e  5e                    pop       esi
  0047bf4f  83c40c                add       esp,0Ch
  0047bf52  c3                    ret

LAB_0047bf53:
  0047bf53  668b4e10              mov       cx,[esi+10h]
  0047bf57  55                    push      ebp
  0047bf58  663bcf                cmp       cx,di
  0047bf5b  53                    push      ebx
  0047bf5c  0f85cb000000          jne       near 0047C02Dh
  0047bf62  8b4674                mov       eax,[esi+74h]
  0047bf65  6aff                  push      0FFFFFFFFh
  0047bf67  8d7e1c                lea       edi,[esi+1Ch]
  0047bf6a  8b08                  mov       ecx,[eax]
  0047bf6c  80c980                or        cl,80h
  0047bf6f  8908                  mov       [eax],ecx
  0047bf71  8b06                  mov       eax,[esi]
  0047bf73  0c80                  or        al,80h
  0047bf75  804e3606              or        byte [esi+36h],6
  0047bf79  8906                  mov       [esi],eax
  0047bf7b  33c0                  xor       eax,eax
  0047bf7d  8a460b                mov       al,[esi+0Bh]
  0047bf80  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047bf86  50                    push      eax
  0047bf87  6a4d                  push      4Dh
  0047bf89  68a01b5500            push      551BA0h
  0047bf8e  57                    push      edi
  0047bf8f  56                    push      esi
  0047bf90  e8abecffff            call      0047AC40h
  0047bf95  83c418                add       esp,18h
  0047bf98  33c9                  xor       ecx,ecx
  0047bf9a  8a4e0b                mov       cl,[esi+0Bh]
  0047bf9d  66c746120010          mov       word [esi+12h],1000h
  0047bfa3  6aff                  push      0FFFFFFFFh
  0047bfa5  51                    push      ecx
  0047bfa6  6a6a                  push      6Ah
  0047bfa8  56                    push      esi
  0047bfa9  e832ebffff            call      0047AAE0h
  0047bfae  83c410                add       esp,10h
  0047bfb1  33d2                  xor       edx,edx
  0047bfb3  8a560b                mov       dl,[esi+0Bh]
  0047bfb6  6aff                  push      0FFFFFFFFh
  0047bfb8  52                    push      edx
  0047bfb9  6a6a                  push      6Ah
  0047bfbb  56                    push      esi
  0047bfbc  e81febffff            call      0047AAE0h
  0047bfc1  8b4674                mov       eax,[esi+74h]
  0047bfc4  83c410                add       esp,10h
  0047bfc7  50                    push      eax
  0047bfc8  e8f3cb0b00            call      00538BC0h
  0047bfcd  0fbf5e32              movsx     ebx,word [esi+32h]
  0047bfd1  8b4e20                mov       ecx,[esi+20h]
  0047bfd4  24fe                  and       al,0FEh
  0047bfd6  83c404                add       esp,4
  0047bfd9  c1e00b                shl       eax,0Bh
  0047bfdc  03c8                  add       ecx,eax
  0047bfde  53                    push      ebx
  0047bfdf  894e20                mov       [esi+20h],ecx
  0047bfe2  e8359a0c00            call      00545A1Ch
  0047bfe7  8d0c80                lea       ecx,[eax+eax*4]
  0047bfea  53                    push      ebx
  0047bfeb  c1e103                shl       ecx,3
  0047bfee  894e50                mov       [esi+50h],ecx
  0047bff1  e8209a0c00            call      00545A16h
  0047bff6  8d1480                lea       edx,[eax+eax*4]
  0047bff9  57                    push      edi
  0047bffa  c1e203                shl       edx,3
  0047bffd  6800120000            push      1200h
  0047c002  6800040000            push      400h
  0047c007  895658                mov       [esi+58h],edx
  0047c00a  6a2d                  push      2Dh
  0047c00c  c70560915e00c400c400  mov       dword [5E9160h],0C400C4h
  0047c016  c70564915e00c4000200  mov       dword [5E9164h],200C4h
  0047c020  e8fb1a0c00            call      0053DB20h
  0047c025  83c410                add       esp,10h
  0047c028  e9a8000000            jmp       0047C0D5h

LAB_0047c02d:
  0047c02d  6683f910              cmp       cx,10h
  0047c031  0f8c9e000000          jl        near 0047C0D5h
  0047c037  a900008000            test      eax,800000h
  0047c03c  7477                  je        short 0047C0B5h
  0047c03e  8b4e74                mov       ecx,[esi+74h]
  0047c041  8b1db4876000          mov       ebx,[6087B4h]
  0047c047  33d2                  xor       edx,edx
  0047c049  8a5108                mov       dl,[ecx+8]
  0047c04c  8b2b                  mov       ebp,[ebx]
  0047c04e  8d1cd2                lea       ebx,[edx+edx*8]
  0047c051  8d4a78                lea       ecx,[edx+78h]
  0047c054  c1e10d                shl       ecx,0Dh
  0047c057  8d145a                lea       edx,[edx+ebx*2]
  0047c05a  33db                  xor       ebx,ebx
  0047c05c  03cd                  add       ecx,ebp
  0047c05e  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0047c065  c1e304                shl       ebx,4
  0047c068  668b93204a6000        mov       dx,[ebx+604A20h]
  0047c06f  668591c41f0000        test      [ecx+1FC4h],dx
  0047c076  7507                  jne       short 0047C07Fh
  0047c078  0d00000002            or        eax,2000000h
  0047c07d  8906                  mov       [esi],eax

LAB_0047c07f:
  0047c07f  a1f02c5f00            mov       eax,[5F2CF0h]
  0047c084  33d2                  xor       edx,edx
  0047c086  bb14000000            mov       ebx,14h
  0047c08b  f7f3                  div       ebx
  0047c08d  85d2                  test      edx,edx
  0047c08f  7531                  jne       short 0047C0C2h
  0047c091  668b814e010000        mov       ax,[ecx+14Eh]
  0047c098  663d0200              cmp       ax,2
  0047c09c  7c0c                  jl        short 0047C0AAh
  0047c09e  83c0fe                add       eax,0FFFFFFFEh
  0047c0a1  6689814e010000        mov       [ecx+14Eh],ax
  0047c0a8  eb18                  jmp       short 0047C0C2h

LAB_0047c0aa:
  0047c0aa  6689b94e010000        mov       [ecx+14Eh],di
  0047c0b1  8b06                  mov       eax,[esi]
  0047c0b3  eb06                  jmp       short 0047C0BBh

LAB_0047c0b5:
  0047c0b5  6683f978              cmp       cx,78h
  0047c0b9  7c07                  jl        short 0047C0C2h

LAB_0047c0bb:
  0047c0bb  0d00000002            or        eax,2000000h
  0047c0c0  8906                  mov       [esi],eax

LAB_0047c0c2:
  0047c0c2  56                    push      esi
  0047c0c3  e84890ffff            call      00475110h
  0047c0c8  83c404                add       esp,4
  0047c0cb  85c0                  test      eax,eax
  0047c0cd  7406                  je        short 0047C0D5h
  0047c0cf  810e00000002          or        dword [esi],2000000h

LAB_0047c0d5:
  0047c0d5  8b4650                mov       eax,[esi+50h]
  0047c0d8  8b4e1c                mov       ecx,[esi+1Ch]
  0047c0db  8d6e1c                lea       ebp,[esi+1Ch]
  0047c0de  8b5e24                mov       ebx,[esi+24h]
  0047c0e1  8bd0                  mov       edx,eax
  0047c0e3  03c8                  add       ecx,eax
  0047c0e5  c1fa03                sar       edx,3
  0047c0e8  8bf8                  mov       edi,eax
  0047c0ea  894d00                mov       [ebp],ecx
  0047c0ed  8b4e58                mov       ecx,[esi+58h]
  0047c0f0  2bfa                  sub       edi,edx
  0047c0f2  c1f808                sar       eax,8
  0047c0f5  2bf8                  sub       edi,eax
  0047c0f7  8bc1                  mov       eax,ecx
  0047c0f9  c1f808                sar       eax,8
  0047c0fc  8bd1                  mov       edx,ecx
  0047c0fe  03d9                  add       ebx,ecx
  0047c100  2bd0                  sub       edx,eax
  0047c102  897e50                mov       [esi+50h],edi
  0047c105  8b7e20                mov       edi,[esi+20h]
  0047c108  56                    push      esi
  0047c109  c1f903                sar       ecx,3
  0047c10c  2bd1                  sub       edx,ecx
  0047c10e  895e24                mov       [esi+24h],ebx
  0047c111  895658                mov       [esi+58h],edx
  0047c114  e827780b00            call      00533940h
  0047c119  8b8690000000          mov       eax,[esi+90h]
  0047c11f  83c404                add       esp,4
  0047c122  a902880000            test      eax,8802h
  0047c127  897e20                mov       [esi+20h],edi
  0047c12a  7408                  je        short 0047C134h
  0047c12c  33c0                  xor       eax,eax
  0047c12e  894650                mov       [esi+50h],eax
  0047c131  894658                mov       [esi+58h],eax

LAB_0047c134:
  0047c134  8d442410              lea       eax,[esp+10h]
  0047c138  50                    push      eax
  0047c139  56                    push      esi
  0047c13a  e8618bffff            call      00474CA0h
  0047c13f  8bf8                  mov       edi,eax
  0047c141  83c408                add       esp,8
  0047c144  85ff                  test      edi,edi
  0047c146  7465                  je        short 0047C1ADh
  0047c148  8b4674                mov       eax,[esi+74h]
  0047c14b  33c9                  xor       ecx,ecx
  0047c14d  8a4e06                mov       cl,[esi+6]
  0047c150  33d2                  xor       edx,edx
  0047c152  8a5607                mov       dl,[esi+7]
  0047c155  6a00                  push      0
  0047c157  51                    push      ecx
  0047c158  52                    push      edx
  0047c159  57                    push      edi
  0047c15a  50                    push      eax
  0047c15b  e850e10b00            call      0053A2B0h
  0047c160  0fbf5e32              movsx     ebx,word [esi+32h]
  0047c164  83c414                add       esp,14h
  0047c167  53                    push      ebx
  0047c168  e8af980c00            call      00545A1Ch
  0047c16d  8b4f50                mov       ecx,[edi+50h]
  0047c170  53                    push      ebx
  0047c171  c1e004                shl       eax,4
  0047c174  03c8                  add       ecx,eax
  0047c176  894f50                mov       [edi+50h],ecx
  0047c179  e898980c00            call      00545A16h
  0047c17e  8b4f58                mov       ecx,[edi+58h]
  0047c181  55                    push      ebp
  0047c182  c1e004                shl       eax,4
  0047c185  03c8                  add       ecx,eax
  0047c187  8b07                  mov       eax,[edi]
  0047c189  6800120000            push      1200h
  0047c18e  0c40                  or        al,40h
  0047c190  68a80a0000            push      0AA8h
  0047c195  6a0f                  push      0Fh
  0047c197  894f58                mov       [edi+58h],ecx
  0047c19a  66c787960000001000    mov       word [edi+96h],10h
  0047c1a3  8907                  mov       [edi],eax
  0047c1a5  e876190c00            call      0053DB20h
  0047c1aa  83c410                add       esp,10h

LAB_0047c1ad:
  0047c1ad  8b06                  mov       eax,[esi]
  0047c1af  a900000040            test      eax,40000000h
  0047c1b4  750b                  jne       short 0047C1C1h
  0047c1b6  8b4e74                mov       ecx,[esi+74h]
  0047c1b9  f70100000040          test      dword [ecx],40000000h
  0047c1bf  7407                  je        short 0047C1C8h

LAB_0047c1c1:
  0047c1c1  0d00000002            or        eax,2000000h
  0047c1c6  8906                  mov       [esi],eax

LAB_0047c1c8:
  0047c1c8  66ff4610              inc       word [esi+10h]
  0047c1cc  5b                    pop       ebx
  0047c1cd  5d                    pop       ebp
  0047c1ce  5f                    pop       edi
  0047c1cf  5e                    pop       esi
  0047c1d0  83c40c                add       esp,0Ch
  0047c1d3  c3                    ret

12 cible(s) de saut interne : 0x0047bf53, 0x0047c02d, 0x0047c07f, 0x0047c0aa, 0x0047c0b5, 0x0047c0bb, 0x0047c0c2, 0x0047c0d5, 0x0047c134, 0x0047c1ad, 0x0047c1c1, 0x0047c1c8
