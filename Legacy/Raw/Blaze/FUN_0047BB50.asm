
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047bb50  section .text  offset fichier 0x7af50  240 octets

  0047bb50  53                    push      ebx
  0047bb51  56                    push      esi
  0047bb52  8b74240c              mov       esi,[esp+0Ch]
  0047bb56  57                    push      edi
  0047bb57  668b4610              mov       ax,[esi+10h]
  0047bb5b  6685c0                test      ax,ax
  0047bb5e  7435                  je        short 0047BB95h
  0047bb60  663d3c00              cmp       ax,3Ch
  0047bb64  0f8cce000000          jl        near 0047BC38h
  0047bb6a  8b4674                mov       eax,[esi+74h]
  0047bb6d  668b4e06              mov       cx,[esi+6]
  0047bb71  6a00                  push      0
  0047bb73  66894806              mov       [eax+6],cx
  0047bb77  8b5678                mov       edx,[esi+78h]
  0047bb7a  8b4674                mov       eax,[esi+74h]
  0047bb7d  52                    push      edx
  0047bb7e  50                    push      eax
  0047bb7f  e80cff0b00            call      0053BA90h
  0047bb84  83c40c                add       esp,0Ch
  0047bb87  66ff4610              inc       word [esi+10h]
  0047bb8b  c70600000000          mov       dword [esi],0
  0047bb91  5f                    pop       edi
  0047bb92  5e                    pop       esi
  0047bb93  5b                    pop       ebx
  0047bb94  c3                    ret

LAB_0047bb95:
  0047bb95  804e3606              or        byte [esi+36h],6
  0047bb99  6a00                  push      0
  0047bb9b  56                    push      esi
  0047bb9c  e8ff8fffff            call      00474BA0h
  0047bba1  83c408                add       esp,8
  0047bba4  894678                mov       [esi+78h],eax
  0047bba7  c646040f              mov       byte [esi+4],0Fh
  0047bbab  66c746345501          mov       word [esi+34h],155h
  0047bbb1  bf03000000            mov       edi,3
  0047bbb6  bb55050000            mov       ebx,555h

LAB_0047bbbb:
  0047bbbb  33c9                  xor       ecx,ecx
  0047bbbd  6aff                  push      0FFFFFFFFh
  0047bbbf  8a4e0b                mov       cl,[esi+0Bh]
  0047bbc2  51                    push      ecx
  0047bbc3  6a67                  push      67h
  0047bbc5  56                    push      esi
  0047bbc6  e815efffff            call      0047AAE0h
  0047bbcb  66015e32              add       [esi+32h],bx
  0047bbcf  668b4632              mov       ax,[esi+32h]
  0047bbd3  83c410                add       esp,10h
  0047bbd6  4f                    dec       edi
  0047bbd7  75e2                  jne       short 0047BBBBh
  0047bbd9  05aa020000            add       eax,2AAh
  0047bbde  66c74634abfe          mov       word [esi+34h],0FEABh
  0047bbe4  66894632              mov       [esi+32h],ax
  0047bbe8  bf03000000            mov       edi,3

LAB_0047bbed:
  0047bbed  33d2                  xor       edx,edx
  0047bbef  6aff                  push      0FFFFFFFFh
  0047bbf1  8a560b                mov       dl,[esi+0Bh]
  0047bbf4  52                    push      edx
  0047bbf5  6a67                  push      67h
  0047bbf7  56                    push      esi
  0047bbf8  e8e3eeffff            call      0047AAE0h
  0047bbfd  66015e32              add       [esi+32h],bx
  0047bc01  83c410                add       esp,10h
  0047bc04  4f                    dec       edi
  0047bc05  75e6                  jne       short 0047BBEDh
  0047bc07  33c0                  xor       eax,eax
  0047bc09  6aff                  push      0FFFFFFFFh
  0047bc0b  8a460b                mov       al,[esi+0Bh]
  0047bc0e  8d7e1c                lea       edi,[esi+1Ch]
  0047bc11  50                    push      eax
  0047bc12  6a4c                  push      4Ch
  0047bc14  68a01b5500            push      551BA0h
  0047bc19  57                    push      edi
  0047bc1a  56                    push      esi
  0047bc1b  e820f0ffff            call      0047AC40h
  0047bc20  83c418                add       esp,18h
  0047bc23  57                    push      edi
  0047bc24  6800120000            push      1200h
  0047bc29  6800040000            push      400h
  0047bc2e  6a10                  push      10h
  0047bc30  e8eb1e0c00            call      0053DB20h
  0047bc35  83c410                add       esp,10h

LAB_0047bc38:
  0047bc38  66ff4610              inc       word [esi+10h]
  0047bc3c  5f                    pop       edi
  0047bc3d  5e                    pop       esi
  0047bc3e  5b                    pop       ebx
  0047bc3f  c3                    ret

4 cible(s) de saut interne : 0x0047bb95, 0x0047bbbb, 0x0047bbed, 0x0047bc38
