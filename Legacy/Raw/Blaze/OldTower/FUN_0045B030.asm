C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045b030  section .text  offset fichier 0x5a430  608 octets

  0045b030  83ec18                sub       esp,18h
  0045b033  8b0d68e05d00          mov       ecx,[5DE068h]
  0045b039  c1f90c                sar       ecx,0Ch
  0045b03c  53                    push      ebx
  0045b03d  55                    push      ebp
  0045b03e  56                    push      esi
  0045b03f  8b742428              mov       esi,[esp+28h]
  0045b043  8bd1                  mov       edx,ecx
  0045b045  57                    push      edi
  0045b046  8b6e1c                mov       ebp,[esi+1Ch]
  0045b049  8bc5                  mov       eax,ebp
  0045b04b  c1f80c                sar       eax,0Ch
  0045b04e  2bd0                  sub       edx,eax
  0045b050  7806                  js        short 0045B058h
  0045b052  8954242c              mov       [esp+2Ch],edx
  0045b056  eb06                  jmp       short 0045B05Eh

LAB_0045b058:
  0045b058  2bc1                  sub       eax,ecx
  0045b05a  8944242c              mov       [esp+2Ch],eax

LAB_0045b05e:
  0045b05e  8b5e24                mov       ebx,[esi+24h]
  0045b061  8b0d70e05d00          mov       ecx,[5DE070h]
  0045b067  8bc3                  mov       eax,ebx
  0045b069  c1f90c                sar       ecx,0Ch
  0045b06c  c1f80c                sar       eax,0Ch
  0045b06f  8bd1                  mov       edx,ecx
  0045b071  2bd0                  sub       edx,eax
  0045b073  7806                  js        short 0045B07Bh
  0045b075  89542410              mov       [esp+10h],edx
  0045b079  eb06                  jmp       short 0045B081h

LAB_0045b07b:
  0045b07b  2bc1                  sub       eax,ecx
  0045b07d  89442410              mov       [esp+10h],eax

LAB_0045b081:
  0045b081  33ff                  xor       edi,edi
  0045b083  66397e10              cmp       [esi+10h],di
  0045b087  0f8593000000          jne       near 0045B120h
  0045b08d  0fbf4632              movsx     eax,word [esi+32h]
  0045b091  0500080000            add       eax,800h
  0045b096  66c746100100          mov       word [esi+10h],1
  0045b09c  50                    push      eax
  0045b09d  66897e12              mov       [esi+12h],di
  0045b0a1  e876a90e00            call      00545A1Ch
  0045b0a6  8b561c                mov       edx,[esi+1Ch]
  0045b0a9  8d0c80                lea       ecx,[eax+eax*4]
  0045b0ac  c1e104                shl       ecx,4
  0045b0af  03d1                  add       edx,ecx
  0045b0b1  89561c                mov       [esi+1Ch],edx
  0045b0b4  0fbf5632              movsx     edx,word [esi+32h]
  0045b0b8  81c200080000          add       edx,800h
  0045b0be  52                    push      edx
  0045b0bf  e852a90e00            call      00545A16h
  0045b0c4  8b4e24                mov       ecx,[esi+24h]
  0045b0c7  8d0480                lea       eax,[eax+eax*4]
  0045b0ca  c1e004                shl       eax,4
  0045b0cd  03c8                  add       ecx,eax
  0045b0cf  b800a00000            mov       eax,0A000h
  0045b0d4  66898682000000        mov       [esi+82h],ax
  0045b0db  66898686000000        mov       [esi+86h],ax
  0045b0e2  6689868a000000        mov       [esi+8Ah],ax
  0045b0e9  6689868e000000        mov       [esi+8Eh],ax
  0045b0f0  8b4620                mov       eax,[esi+20h]
  0045b0f3  894e24                mov       [esi+24h],ecx
  0045b0f6  894664                mov       [esi+64h],eax
  0045b0f9  894660                mov       [esi+60h],eax
  0045b0fc  8b0dc07b5f00          mov       ecx,[5F7BC0h]
  0045b102  6800800000            push      8000h
  0045b107  56                    push      esi
  0045b108  51                    push      ecx
  0045b109  e8c27f0d00            call      005330D0h
  0045b10e  83c40c                add       esp,0Ch
  0045b111  56                    push      esi
  0045b112  e829880d00            call      00533940h
  0045b117  83c404                add       esp,4
  0045b11a  896e1c                mov       [esi+1Ch],ebp
  0045b11d  895e24                mov       [esi+24h],ebx

LAB_0045b120:
  0045b120  668b4614              mov       ax,[esi+14h]
  0045b124  663944242c            cmp       [esp+2Ch],ax
  0045b129  7f07                  jg        short 0045B132h
  0045b12b  6639442410            cmp       [esp+10h],ax
  0045b130  7e09                  jle       short 0045B13Bh

LAB_0045b132:
  0045b132  663bc7                cmp       ax,di
  0045b135  0f8f47010000          jg        near 0045B282h

LAB_0045b13b:
  0045b13b  66ff4612              inc       word [esi+12h]
  0045b13f  e8fcbf0e00            call      00547140h
  0045b144  99                    cdq
  0045b145  b91e000000            mov       ecx,1Eh
  0045b14a  f7f9                  idiv      ecx
  0045b14c  6683fa01              cmp       dx,1
  0045b150  7511                  jne       short 0045B163h
  0045b152  0fbf4616              movsx     eax,word [esi+16h]
  0045b156  99                    cdq
  0045b157  2bc2                  sub       eax,edx
  0045b159  0fbf5612              movsx     edx,word [esi+12h]
  0045b15d  d1f8                  sar       eax,1
  0045b15f  3bd0                  cmp       edx,eax
  0045b161  7f0e                  jg        short 0045B171h

LAB_0045b163:
  0045b163  668b4612              mov       ax,[esi+12h]
  0045b167  663b4616              cmp       ax,[esi+16h]
  0045b16b  0f8c11010000          jl        near 0045B282h

LAB_0045b171:
  0045b171  8b4658                mov       eax,[esi+58h]
  0045b174  8b5654                mov       edx,[esi+54h]
  0045b177  8b4e50                mov       ecx,[esi+50h]
  0045b17a  89442424              mov       [esp+24h],eax
  0045b17e  668b4632              mov       ax,[esi+32h]
  0045b182  89542420              mov       [esp+20h],edx
  0045b186  8b5620                mov       edx,[esi+20h]
  0045b189  89442418              mov       [esp+18h],eax
  0045b18d  0500080000            add       eax,800h
  0045b192  894c241c              mov       [esp+1Ch],ecx
  0045b196  8b4e1c                mov       ecx,[esi+1Ch]
  0045b199  89542410              mov       [esp+10h],edx
  0045b19d  0fbfd0                movsx     edx,ax
  0045b1a0  894c242c              mov       [esp+2Ch],ecx
  0045b1a4  8b4e24                mov       ecx,[esi+24h]
  0045b1a7  52                    push      edx
  0045b1a8  66897e12              mov       [esi+12h],di
  0045b1ac  66894632              mov       [esi+32h],ax
  0045b1b0  894c2418              mov       [esp+18h],ecx
  0045b1b4  e863a80e00            call      00545A1Ch
  0045b1b9  8b4e1c                mov       ecx,[esi+1Ch]
  0045b1bc  8d0480                lea       eax,[eax+eax*4]
  0045b1bf  c1e004                shl       eax,4
  0045b1c2  03c8                  add       ecx,eax
  0045b1c4  894e1c                mov       [esi+1Ch],ecx
  0045b1c7  0fbf4e32              movsx     ecx,word [esi+32h]
  0045b1cb  51                    push      ecx
  0045b1cc  e845a80e00            call      00545A16h
  0045b1d1  8b4e24                mov       ecx,[esi+24h]
  0045b1d4  8d1480                lea       edx,[eax+eax*4]
  0045b1d7  c1e204                shl       edx,4
  0045b1da  03ca                  add       ecx,edx
  0045b1dc  66397e14              cmp       [esi+14h],di
  0045b1e0  894e24                mov       [esi+24h],ecx
  0045b1e3  7536                  jne       short 0045B21Bh

LAB_0045b1e5:
  0045b1e5  668b5e14              mov       bx,[esi+14h]
  0045b1e9  668b6e16              mov       bp,[esi+16h]
  0045b1ed  6a00                  push      0
  0045b1ef  6a00                  push      0
  0045b1f1  6aff                  push      0FFFFFFFFh
  0045b1f3  6a00                  push      0
  0045b1f5  6a39                  push      39h
  0045b1f7  56                    push      esi
  0045b1f8  66897e14              mov       [esi+14h],di
  0045b1fc  66c746160800          mov       word [esi+16h],8
  0045b202  e809cf0000            call      00468110h
  0045b207  83c418                add       esp,18h
  0045b20a  47                    inc       edi
  0045b20b  6683ff03              cmp       di,3
  0045b20f  66895e14              mov       [esi+14h],bx
  0045b213  66896e16              mov       [esi+16h],bp
  0045b217  7ecc                  jle       short 0045B1E5h
  0045b219  eb34                  jmp       short 0045B24Fh

LAB_0045b21b:
  0045b21b  668b5e14              mov       bx,[esi+14h]
  0045b21f  668b6e16              mov       bp,[esi+16h]
  0045b223  6a00                  push      0
  0045b225  6a00                  push      0
  0045b227  6aff                  push      0FFFFFFFFh
  0045b229  6a00                  push      0
  0045b22b  6a39                  push      39h
  0045b22d  56                    push      esi
  0045b22e  66897e14              mov       [esi+14h],di
  0045b232  66c746160800          mov       word [esi+16h],8
  0045b238  e8d3ce0000            call      00468110h
  0045b23d  83c418                add       esp,18h
  0045b240  47                    inc       edi
  0045b241  6683ff03              cmp       di,3
  0045b245  66895e14              mov       [esi+14h],bx
  0045b249  66896e16              mov       [esi+16h],bp
  0045b24d  7ecc                  jle       short 0045B21Bh

LAB_0045b24f:
  0045b24f  8b44242c              mov       eax,[esp+2Ch]
  0045b253  8b4c2410              mov       ecx,[esp+10h]
  0045b257  8b542414              mov       edx,[esp+14h]
  0045b25b  89461c                mov       [esi+1Ch],eax
  0045b25e  668b442418            mov       ax,[esp+18h]
  0045b263  894e20                mov       [esi+20h],ecx
  0045b266  8b4c241c              mov       ecx,[esp+1Ch]
  0045b26a  895624                mov       [esi+24h],edx
  0045b26d  8b542420              mov       edx,[esp+20h]
  0045b271  66894632              mov       [esi+32h],ax
  0045b275  8b442424              mov       eax,[esp+24h]
  0045b279  894e50                mov       [esi+50h],ecx
  0045b27c  895654                mov       [esi+54h],edx
  0045b27f  894658                mov       [esi+58h],eax

LAB_0045b282:
  0045b282  5f                    pop       edi
  0045b283  5e                    pop       esi
  0045b284  5d                    pop       ebp
  0045b285  5b                    pop       ebx
  0045b286  83c418                add       esp,18h
  0045b289  c3                    ret
  0045b28a  90                    nop
  0045b28b  90                    nop
  0045b28c  90                    nop
  0045b28d  90                    nop
  0045b28e  90                    nop
  0045b28f  90                    nop

13 cible(s) de saut interne : 0x0045b058, 0x0045b05e, 0x0045b07b, 0x0045b081, 0x0045b120, 0x0045b132, 0x0045b13b, 0x0045b163, 0x0045b171, 0x0045b1e5, 0x0045b21b, 0x0045b24f, 0x0045b282
