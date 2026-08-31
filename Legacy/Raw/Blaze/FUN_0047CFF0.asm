
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047cff0  section .text  offset fichier 0x7c3f0  626 octets

  0047cff0  83ec18                sub       esp,18h
  0047cff3  53                    push      ebx
  0047cff4  55                    push      ebp
  0047cff5  56                    push      esi
  0047cff6  8b742428              mov       esi,[esp+28h]
  0047cffa  33c0                  xor       eax,eax
  0047cffc  57                    push      edi
  0047cffd  66394610              cmp       [esi+10h],ax
  0047d001  6689442410            mov       [esp+10h],ax
  0047d006  66c744241280ff        mov       word [esp+12h],0FF80h
  0047d00d  66c74424148000        mov       word [esp+14h],80h
  0047d014  66c7442416c800        mov       word [esp+16h],0C8h
  0047d01b  6689442418            mov       [esp+18h],ax
  0047d020  66c744241a0001        mov       word [esp+1Ah],100h
  0047d027  bb01000000            mov       ebx,1
  0047d02c  755a                  jne       short 0047D088h
  0047d02e  8b16                  mov       edx,[esi]
  0047d030  804e3606              or        byte [esi+36h],6
  0047d034  8d7e1c                lea       edi,[esi+1Ch]
  0047d037  80ca80                or        dl,80h
  0047d03a  57                    push      edi
  0047d03b  6800120000            push      1200h
  0047d040  6800040000            push      400h
  0047d045  6a20                  push      20h
  0047d047  66895e10              mov       [esi+10h],bx
  0047d04b  8916                  mov       [esi],edx
  0047d04d  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  0047d053  c7462800300030        mov       dword [esi+28h],30003000h
  0047d05a  66c7462c0030          mov       word [esi+2Ch],3000h
  0047d060  c74638d0d0d000        mov       dword [esi+38h],0D0D0D0h
  0047d067  c6460404              mov       byte [esi+4],4
  0047d06b  e8b00a0c00            call      0053DB20h
  0047d070  83c410                add       esp,10h
  0047d073  57                    push      edi
  0047d074  6800120000            push      1200h
  0047d079  6855040000            push      455h
  0047d07e  6a20                  push      20h
  0047d080  e89b0a0c00            call      0053DB20h
  0047d085  83c410                add       esp,10h

LAB_0047d088:
  0047d088  845e10                test      [esi+10h],bl
  0047d08b  0f8481000000          je        near 0047D112h
  0047d091  8b461c                mov       eax,[esi+1Ch]
  0047d094  8b4e24                mov       ecx,[esi+24h]
  0047d097  8d7e1c                lea       edi,[esi+1Ch]
  0047d09a  8944241c              mov       [esp+1Ch],eax
  0047d09e  894c2424              mov       [esp+24h],ecx
  0047d0a2  e899a00c00            call      00547140h
  0047d0a7  8bd8                  mov       ebx,eax
  0047d0a9  e892a00c00            call      00547140h
  0047d0ae  8be8                  mov       ebp,eax
  0047d0b0  53                    push      ebx
  0047d0b1  83e57f                and       ebp,7Fh
  0047d0b4  e863890c00            call      00545A1Ch
  0047d0b9  0fafc5                imul      eax,ebp
  0047d0bc  8b0f                  mov       ecx,[edi]
  0047d0be  53                    push      ebx
  0047d0bf  03c8                  add       ecx,eax
  0047d0c1  890f                  mov       [edi],ecx
  0047d0c3  e84e890c00            call      00545A16h
  0047d0c8  0fafc5                imul      eax,ebp
  0047d0cb  8b5e24                mov       ebx,[esi+24h]
  0047d0ce  33d2                  xor       edx,edx
  0047d0d0  8a560b                mov       dl,[esi+0Bh]
  0047d0d3  6aff                  push      0FFFFFFFFh
  0047d0d5  52                    push      edx
  0047d0d6  03d8                  add       ebx,eax
  0047d0d8  6a7b                  push      7Bh
  0047d0da  56                    push      esi
  0047d0db  895e24                mov       [esi+24h],ebx
  0047d0de  e8fdd9ffff            call      0047AAE0h
  0047d0e3  83c410                add       esp,10h
  0047d0e6  33c0                  xor       eax,eax
  0047d0e8  8a460b                mov       al,[esi+0Bh]
  0047d0eb  66c7461a0400          mov       word [esi+1Ah],4
  0047d0f1  6aff                  push      0FFFFFFFFh
  0047d0f3  50                    push      eax
  0047d0f4  6a52                  push      52h
  0047d0f6  68401b5500            push      551B40h
  0047d0fb  57                    push      edi
  0047d0fc  56                    push      esi
  0047d0fd  e83edbffff            call      0047AC40h
  0047d102  8b4c2434              mov       ecx,[esp+34h]
  0047d106  8b54243c              mov       edx,[esp+3Ch]
  0047d10a  83c418                add       esp,18h
  0047d10d  890f                  mov       [edi],ecx
  0047d10f  895624                mov       [esi+24h],edx

LAB_0047d112:
  0047d112  0fbf7e32              movsx     edi,word [esi+32h]
  0047d116  57                    push      edi
  0047d117  66c7869a0000000060    mov       word [esi+9Ah],6000h
  0047d120  8d5e1c                lea       ebx,[esi+1Ch]
  0047d123  e8f4880c00            call      00545A1Ch
  0047d128  8b0b                  mov       ecx,[ebx]
  0047d12a  8d0480                lea       eax,[eax+eax*4]
  0047d12d  c1e002                shl       eax,2
  0047d130  03c8                  add       ecx,eax
  0047d132  57                    push      edi
  0047d133  890b                  mov       [ebx],ecx
  0047d135  e8dc880c00            call      00545A16h
  0047d13a  8d0c80                lea       ecx,[eax+eax*4]
  0047d13d  8b4624                mov       eax,[esi+24h]
  0047d140  c1e102                shl       ecx,2
  0047d143  03c1                  add       eax,ecx
  0047d145  8b6e20                mov       ebp,[esi+20h]
  0047d148  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0047d14f  894624                mov       [esi+24h],eax
  0047d152  8b4654                mov       eax,[esi+54h]
  0047d155  8bd0                  mov       edx,eax
  0047d157  8bf9                  mov       edi,ecx
  0047d159  c1fa08                sar       edx,8
  0047d15c  c1ff08                sar       edi,8
  0047d15f  2bfa                  sub       edi,edx
  0047d161  03e8                  add       ebp,eax
  0047d163  03f8                  add       edi,eax
  0047d165  56                    push      esi
  0047d166  2bf9                  sub       edi,ecx
  0047d168  896e20                mov       [esi+20h],ebp
  0047d16b  897e54                mov       [esi+54h],edi
  0047d16e  e8cd670b00            call      00533940h
  0047d173  8b4660                mov       eax,[esi+60h]
  0047d176  8b4e20                mov       ecx,[esi+20h]
  0047d179  83c404                add       esp,4
  0047d17c  3bc8                  cmp       ecx,eax
  0047d17e  7d0d                  jge       short 0047D18Dh
  0047d180  894620                mov       [esi+20h],eax
  0047d183  c7869000000000080000  mov       dword [esi+90h],800h

LAB_0047d18d:
  0047d18d  8b8690000000          mov       eax,[esi+90h]
  0047d193  f6c4a0                test      ah,0A0h
  0047d196  750d                  jne       short 0047D1A5h
  0047d198  a902080000            test      eax,802h
  0047d19d  7406                  je        short 0047D1A5h
  0047d19f  66c746102800          mov       word [esi+10h],28h

LAB_0047d1a5:
  0047d1a5  8d442410              lea       eax,[esp+10h]
  0047d1a9  50                    push      eax
  0047d1aa  56                    push      esi
  0047d1ab  e8f07affff            call      00474CA0h
  0047d1b0  8bf8                  mov       edi,eax
  0047d1b2  83c408                add       esp,8
  0047d1b5  85ff                  test      edi,edi
  0047d1b7  746b                  je        short 0047D224h
  0047d1b9  8b4674                mov       eax,[esi+74h]
  0047d1bc  33c9                  xor       ecx,ecx
  0047d1be  8a4e06                mov       cl,[esi+6]
  0047d1c1  33d2                  xor       edx,edx
  0047d1c3  8a5607                mov       dl,[esi+7]
  0047d1c6  6a00                  push      0
  0047d1c8  51                    push      ecx
  0047d1c9  52                    push      edx
  0047d1ca  57                    push      edi
  0047d1cb  50                    push      eax
  0047d1cc  e8dfd00b00            call      0053A2B0h
  0047d1d1  0fbf6e32              movsx     ebp,word [esi+32h]
  0047d1d5  83c414                add       esp,14h
  0047d1d8  81ed00040000          sub       ebp,400h
  0047d1de  55                    push      ebp
  0047d1df  e838880c00            call      00545A1Ch
  0047d1e4  8b4f50                mov       ecx,[edi+50h]
  0047d1e7  55                    push      ebp
  0047d1e8  c1e004                shl       eax,4
  0047d1eb  03c8                  add       ecx,eax
  0047d1ed  894f50                mov       [edi+50h],ecx
  0047d1f0  e821880c00            call      00545A16h
  0047d1f5  8b4f58                mov       ecx,[edi+58h]
  0047d1f8  53                    push      ebx
  0047d1f9  c1e004                shl       eax,4
  0047d1fc  03c8                  add       ecx,eax
  0047d1fe  8b07                  mov       eax,[edi]
  0047d200  6800120000            push      1200h
  0047d205  0c40                  or        al,40h
  0047d207  6853060000            push      653h
  0047d20c  6a13                  push      13h
  0047d20e  894f58                mov       [edi+58h],ecx
  0047d211  66c787960000001000    mov       word [edi+96h],10h
  0047d21a  8907                  mov       [edi],eax
  0047d21c  e8ff080c00            call      0053DB20h
  0047d221  83c410                add       esp,10h

LAB_0047d224:
  0047d224  c70560915e00c0008000  mov       dword [5E9160h],8000C0h
  0047d22e  c70564915e0060000200  mov       dword [5E9164h],20060h
  0047d238  66837e1028            cmp       word [esi+10h],28h
  0047d23d  7d08                  jge       short 0047D247h
  0047d23f  f70600000040          test      dword [esi],40000000h
  0047d245  740f                  je        short 0047D256h

LAB_0047d247:
  0047d247  66c70566915e000000    mov       word [5E9166h],0
  0047d250  c70600000000          mov       dword [esi],0

LAB_0047d256:
  0047d256  66ff4610              inc       word [esi+10h]
  0047d25a  5f                    pop       edi
  0047d25b  5e                    pop       esi
  0047d25c  5d                    pop       ebp
  0047d25d  5b                    pop       ebx
  0047d25e  83c418                add       esp,18h
  0047d261  c3                    ret

7 cible(s) de saut interne : 0x0047d088, 0x0047d112, 0x0047d18d, 0x0047d1a5, 0x0047d224, 0x0047d247, 0x0047d256
