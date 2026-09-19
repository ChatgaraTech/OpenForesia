Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046f070  section .text  offset fichier 0x6e470  272 octets

  0046f070  51                    push      ecx
  0046f071  56                    push      esi
  0046f072  8b74240c              mov       esi,[esp+0Ch]
  0046f076  8b0e                  mov       ecx,[esi]
  0046f078  80cd40                or        ch,40h
  0046f07b  890e                  mov       [esi],ecx
  0046f07d  e8be800d00            call      00547140h
  0046f082  99                    cdq
  0046f083  b978000000            mov       ecx,78h
  0046f088  f7f9                  idiv      ecx
  0046f08a  83fa01                cmp       edx,1
  0046f08d  0f85df000000          jne       near 0046F172h
  0046f093  668b5632              mov       dx,[esi+32h]
  0046f097  57                    push      edi
  0046f098  8b7e1c                mov       edi,[esi+1Ch]
  0046f09b  55                    push      ebp
  0046f09c  8b6e24                mov       ebp,[esi+24h]
  0046f09f  53                    push      ebx
  0046f0a0  8b5e20                mov       ebx,[esi+20h]
  0046f0a3  6689542410            mov       [esp+10h],dx
  0046f0a8  e893800d00            call      00547140h
  0046f0ad  e88e800d00            call      00547140h
  0046f0b2  99                    cdq
  0046f0b3  b958020000            mov       ecx,258h
  0046f0b8  f7f9                  idiv      ecx
  0046f0ba  8b461c                mov       eax,[esi+1Ch]
  0046f0bd  81ea2c010000          sub       edx,12Ch
  0046f0c3  c1e20c                shl       edx,0Ch
  0046f0c6  03c2                  add       eax,edx
  0046f0c8  89461c                mov       [esi+1Ch],eax
  0046f0cb  e870800d00            call      00547140h
  0046f0d0  99                    cdq
  0046f0d1  b958020000            mov       ecx,258h
  0046f0d6  f7f9                  idiv      ecx
  0046f0d8  8b4e24                mov       ecx,[esi+24h]
  0046f0db  81ea2c010000          sub       edx,12Ch
  0046f0e1  c1e20c                shl       edx,0Ch
  0046f0e4  03ca                  add       ecx,edx
  0046f0e6  894e24                mov       [esi+24h],ecx
  0046f0e9  e852800d00            call      00547140h
  0046f0ee  99                    cdq
  0046f0ef  b9f4010000            mov       ecx,1F4h
  0046f0f4  f7f9                  idiv      ecx
  0046f0f6  81c2380b0000          add       edx,0B38h
  0046f0fc  66895632              mov       [esi+32h],dx
  0046f100  e83b800d00            call      00547140h
  0046f105  99                    cdq
  0046f106  33c2                  xor       eax,edx
  0046f108  2bc2                  sub       eax,edx
  0046f10a  83e001                and       eax,1
  0046f10d  33c2                  xor       eax,edx
  0046f10f  2bc2                  sub       eax,edx
  0046f111  8d54401f              lea       edx,[eax+eax*2+1Fh]
  0046f115  89542418              mov       [esp+18h],edx
  0046f119  e822800d00            call      00547140h
  0046f11e  99                    cdq
  0046f11f  b914000000            mov       ecx,14h
  0046f124  6a00                  push      0
  0046f126  f7f9                  idiv      ecx
  0046f128  6a00                  push      0
  0046f12a  6800200000            push      2000h
  0046f12f  81c2f6000000          add       edx,0F6h
  0046f135  52                    push      edx
  0046f136  8b542428              mov       edx,[esp+28h]
  0046f13a  52                    push      edx
  0046f13b  56                    push      esi
  0046f13c  e83f8fffff            call      00468080h
  0046f141  83c418                add       esp,18h
  0046f144  66c746160300          mov       word [esi+16h],3
  0046f14a  6a00                  push      0
  0046f14c  6a00                  push      0
  0046f14e  6aff                  push      0FFFFFFFFh
  0046f150  6a00                  push      0
  0046f152  6a32                  push      32h
  0046f154  56                    push      esi
  0046f155  e8b68fffff            call      00468110h
  0046f15a  668b442428            mov       ax,[esp+28h]
  0046f15f  83c418                add       esp,18h
  0046f162  895e20                mov       [esi+20h],ebx
  0046f165  896e24                mov       [esi+24h],ebp
  0046f168  5b                    pop       ebx
  0046f169  897e1c                mov       [esi+1Ch],edi
  0046f16c  5d                    pop       ebp
  0046f16d  66894632              mov       [esi+32h],ax
  0046f171  5f                    pop       edi

LAB_0046f172:
  0046f172  5e                    pop       esi
  0046f173  59                    pop       ecx
  0046f174  c3                    ret
  0046f175  90                    nop
  0046f176  90                    nop
  0046f177  90                    nop
  0046f178  90                    nop
  0046f179  90                    nop
  0046f17a  90                    nop
  0046f17b  90                    nop
  0046f17c  90                    nop
  0046f17d  90                    nop
  0046f17e  90                    nop
  0046f17f  90                    nop

1 cible(s) de saut interne : 0x0046f172
