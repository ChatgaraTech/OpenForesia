C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00471dd0  section .text  offset fichier 0x711d0  2000 octets

  00471dd0  a120195700            mov       eax,[571920h]
  00471dd5  53                    push      ebx
  00471dd6  8b5c2410              mov       ebx,[esp+10h]
  00471dda  55                    push      ebp
  00471ddb  8b6c2410              mov       ebp,[esp+10h]
  00471ddf  56                    push      esi
  00471de0  8b742410              mov       esi,[esp+10h]
  00471de4  57                    push      edi
  00471de5  83fd01                cmp       ebp,1
  00471de8  8bf8                  mov       edi,eax
  00471dea  7545                  jne       short 00471E31h
  00471dec  8bc8                  mov       ecx,eax
  00471dee  8bd0                  mov       edx,eax
  00471df0  c1e906                shr       ecx,6
  00471df3  c1ea03                shr       edx,3
  00471df6  03c1                  add       eax,ecx
  00471df8  03d0                  add       edx,eax
  00471dfa  891544195700          mov       [571944h],edx
  00471e00  0fbf4632              movsx     eax,word [esi+32h]
  00471e04  0500040000            add       eax,400h
  00471e09  c7053c19570000000000  mov       dword [57193Ch],0
  00471e13  a338195700            mov       [571938h],eax
  00471e18  891d40195700          mov       [571940h],ebx
  00471e1e  e81d530d00            call      00547140h
  00471e23  99                    cdq
  00471e24  b909000000            mov       ecx,9
  00471e29  f7f9                  idiv      ecx
  00471e2b  891534195700          mov       [571934h],edx

LAB_00471e31:
  00471e31  83fdff                cmp       ebp,0FFFFFFFFh
  00471e34  7548                  jne       short 00471E7Eh
  00471e36  8bd7                  mov       edx,edi
  00471e38  8bc7                  mov       eax,edi
  00471e3a  c1ea03                shr       edx,3
  00471e3d  c1e806                shr       eax,6
  00471e40  03fa                  add       edi,edx
  00471e42  03c7                  add       eax,edi
  00471e44  f7d8                  neg       eax
  00471e46  a344195700            mov       [571944h],eax
  00471e4b  0fbf4e32              movsx     ecx,word [esi+32h]
  00471e4f  81e900040000          sub       ecx,400h
  00471e55  c7053c19570000000000  mov       dword [57193Ch],0
  00471e5f  890d38195700          mov       [571938h],ecx
  00471e65  891d40195700          mov       [571940h],ebx
  00471e6b  e8d0520d00            call      00547140h
  00471e70  99                    cdq
  00471e71  b909000000            mov       ecx,9
  00471e76  f7f9                  idiv      ecx
  00471e78  891534195700          mov       [571934h],edx

LAB_00471e7e:
  00471e7e  85ed                  test      ebp,ebp
  00471e80  0f85ef000000          jne       near 00471F75h
  00471e86  a13c195700            mov       eax,[57193Ch]
  00471e8b  85c0                  test      eax,eax
  00471e8d  0f85e2000000          jne       near 00471F75h
  00471e93  8b3d44195700          mov       edi,[571944h]
  00471e99  a134195700            mov       eax,[571934h]
  00471e9e  33d2                  xor       edx,edx
  00471ea0  85ff                  test      edi,edi
  00471ea2  0f9ec2                setle     dl
  00471ea5  48                    dec       eax
  00471ea6  8bfa                  mov       edi,edx
  00471ea8  83f8ff                cmp       eax,0FFFFFFFFh
  00471eab  a334195700            mov       [571934h],eax
  00471eb0  7533                  jne       short 00471EE5h
  00471eb2  e889520d00            call      00547140h
  00471eb7  99                    cdq
  00471eb8  33c2                  xor       eax,edx
  00471eba  2bc2                  sub       eax,edx
  00471ebc  83e007                and       eax,7
  00471ebf  33c2                  xor       eax,edx
  00471ec1  2bc2                  sub       eax,edx
  00471ec3  40                    inc       eax
  00471ec4  a334195700            mov       [571934h],eax
  00471ec9  8d443f01              lea       eax,[edi+edi+1]
  00471ecd  50                    push      eax
  00471ece  56                    push      esi
  00471ecf  e85c070000            call      00472630h
  00471ed4  83c408                add       esp,8
  00471ed7  8d4c3f02              lea       ecx,[edi+edi+2]
  00471edb  51                    push      ecx
  00471edc  56                    push      esi
  00471edd  e84e070000            call      00472630h
  00471ee2  83c408                add       esp,8

LAB_00471ee5:
  00471ee5  668b1544195700        mov       dx,[571944h]
  00471eec  66015632              add       [esi+32h],dx
  00471ef0  8b0d44195700          mov       ecx,[571944h]
  00471ef6  668b4632              mov       ax,[esi+32h]
  00471efa  8b1538195700          mov       edx,[571938h]
  00471f00  85c9                  test      ecx,ecx
  00471f02  7d07                  jge       short 00471F0Bh
  00471f04  0fbff8                movsx     edi,ax
  00471f07  3bfa                  cmp       edi,edx
  00471f09  7e0b                  jle       short 00471F16h

LAB_00471f0b:
  00471f0b  85c9                  test      ecx,ecx
  00471f0d  7e66                  jle       short 00471F75h
  00471f0f  0fbfc0                movsx     eax,ax
  00471f12  3bc2                  cmp       eax,edx
  00471f14  7c5f                  jl        short 00471F75h

LAB_00471f16:
  00471f16  66895632              mov       [esi+32h],dx
  00471f1a  c7053c19570001000000  mov       dword [57193Ch],1
  00471f24  668b4632              mov       ax,[esi+32h]
  00471f28  663d0010              cmp       ax,1000h
  00471f2c  7c09                  jl        short 00471F37h
  00471f2e  0500f0ffff            add       eax,0FFFFF000h
  00471f33  66894632              mov       [esi+32h],ax

LAB_00471f37:
  00471f37  668b4632              mov       ax,[esi+32h]
  00471f3b  6685c0                test      ax,ax
  00471f3e  7d09                  jge       short 00471F49h
  00471f40  0500100000            add       eax,1000h
  00471f45  66894632              mov       [esi+32h],ax

LAB_00471f49:
  00471f49  668b4632              mov       ax,[esi+32h]
  00471f4d  6685c0                test      ax,ax
  00471f50  7417                  je        short 00471F69h
  00471f52  663d0008              cmp       ax,800h
  00471f56  7411                  je        short 00471F69h
  00471f58  8b0d40195700          mov       ecx,[571940h]
  00471f5e  c1e10c                shl       ecx,0Ch
  00471f61  894e1c                mov       [esi+1Ch],ecx
  00471f64  5f                    pop       edi
  00471f65  5e                    pop       esi
  00471f66  5d                    pop       ebp
  00471f67  5b                    pop       ebx
  00471f68  c3                    ret

LAB_00471f69:
  00471f69  8b1540195700          mov       edx,[571940h]
  00471f6f  c1e20c                shl       edx,0Ch
  00471f72  895624                mov       [esi+24h],edx

LAB_00471f75:
  00471f75  5f                    pop       edi
  00471f76  5e                    pop       esi
  00471f77  5d                    pop       ebp
  00471f78  5b                    pop       ebx
  00471f79  c3                    ret
  00471f7a  90                    nop
  00471f7b  90                    nop
  00471f7c  90                    nop
  00471f7d  90                    nop
  00471f7e  90                    nop
  00471f7f  90                    nop
  00471f80  a174195700            mov       eax,[571974h]
  00471f85  53                    push      ebx
  00471f86  56                    push      esi
  00471f87  8b74240c              mov       esi,[esp+0Ch]
  00471f8b  3d6c160000            cmp       eax,166Ch
  00471f90  57                    push      edi
  00471f91  bb01000000            mov       ebx,1
  00471f96  722e                  jb        short 00471FC6h
  00471f98  3d70170000            cmp       eax,1770h
  00471f9d  7727                  ja        short 00471FC6h
  00471f9f  391d3c195700          cmp       [57193Ch],ebx
  00471fa5  751f                  jne       short 00471FC6h
  00471fa7  68ac0d0000            push      0DACh
  00471fac  6aff                  push      0FFFFFFFFh
  00471fae  56                    push      esi
  00471faf  e81cfeffff            call      00471DD0h
  00471fb4  a174195700            mov       eax,[571974h]
  00471fb9  83c40c                add       esp,0Ch
  00471fbc  c7053019570005000000  mov       dword [571930h],5

LAB_00471fc6:
  00471fc6  3d141e0000            cmp       eax,1E14h
  00471fcb  7224                  jb        short 00471FF1h
  00471fcd  3ddc1e0000            cmp       eax,1EDCh
  00471fd2  771d                  ja        short 00471FF1h
  00471fd4  391d3c195700          cmp       [57193Ch],ebx
  00471fda  7515                  jne       short 00471FF1h
  00471fdc  6854f2ffff            push      0FFFFF254h
  00471fe1  6aff                  push      0FFFFFFFFh
  00471fe3  56                    push      esi
  00471fe4  e8e7fdffff            call      00471DD0h
  00471fe9  a174195700            mov       eax,[571974h]
  00471fee  83c40c                add       esp,0Ch

LAB_00471ff1:
  00471ff1  3d18240000            cmp       eax,2418h
  00471ff6  7224                  jb        short 0047201Ch
  00471ff8  3db8240000            cmp       eax,24B8h
  00471ffd  771d                  ja        short 0047201Ch
  00471fff  391d3c195700          cmp       [57193Ch],ebx
  00472005  7515                  jne       short 0047201Ch
  00472007  68a4060000            push      6A4h
  0047200c  6aff                  push      0FFFFFFFFh
  0047200e  56                    push      esi
  0047200f  e8bcfdffff            call      00471DD0h
  00472014  a174195700            mov       eax,[571974h]
  00472019  83c40c                add       esp,0Ch

LAB_0047201c:
  0047201c  3df82a0000            cmp       eax,2AF8h
  00472021  7223                  jb        short 00472046h
  00472023  3dc02b0000            cmp       eax,2BC0h
  00472028  771c                  ja        short 00472046h
  0047202a  391d3c195700          cmp       [57193Ch],ebx
  00472030  7514                  jne       short 00472046h
  00472032  6824faffff            push      0FFFFFA24h
  00472037  53                    push      ebx
  00472038  56                    push      esi
  00472039  e892fdffff            call      00471DD0h
  0047203e  a174195700            mov       eax,[571974h]
  00472043  83c40c                add       esp,0Ch

LAB_00472046:
  00472046  3d2c330000            cmp       eax,332Ch
  0047204b  7224                  jb        short 00472071h
  0047204d  3df4330000            cmp       eax,33F4h
  00472052  771d                  ja        short 00472071h
  00472054  391d3c195700          cmp       [57193Ch],ebx
  0047205a  7515                  jne       short 00472071h
  0047205c  6844fdffff            push      0FFFFFD44h
  00472061  6aff                  push      0FFFFFFFFh
  00472063  56                    push      esi
  00472064  e867fdffff            call      00471DD0h
  00472069  a174195700            mov       eax,[571974h]
  0047206e  83c40c                add       esp,0Ch

LAB_00472071:
  00472071  3d6c390000            cmp       eax,396Ch
  00472076  7224                  jb        short 0047209Ch
  00472078  3d343a0000            cmp       eax,3A34h
  0047207d  771d                  ja        short 0047209Ch
  0047207f  391d3c195700          cmp       [57193Ch],ebx
  00472085  7515                  jne       short 0047209Ch
  00472087  682c010000            push      12Ch
  0047208c  6aff                  push      0FFFFFFFFh
  0047208e  56                    push      esi
  0047208f  e83cfdffff            call      00471DD0h
  00472094  a174195700            mov       eax,[571974h]
  00472099  83c40c                add       esp,0Ch

LAB_0047209c:
  0047209c  3d8c3c0000            cmp       eax,3C8Ch
  004720a1  7223                  jb        short 004720C6h
  004720a3  3d543d0000            cmp       eax,3D54h
  004720a8  771c                  ja        short 004720C6h
  004720aa  391d3c195700          cmp       [57193Ch],ebx
  004720b0  7514                  jne       short 004720C6h
  004720b2  68f4010000            push      1F4h
  004720b7  53                    push      ebx
  004720b8  56                    push      esi
  004720b9  e812fdffff            call      00471DD0h
  004720be  a174195700            mov       eax,[571974h]
  004720c3  83c40c                add       esp,0Ch

LAB_004720c6:
  004720c6  3d644b0000            cmp       eax,4B64h
  004720cb  7241                  jb        short 0047210Eh
  004720cd  3d2c4c0000            cmp       eax,4C2Ch
  004720d2  773a                  ja        short 0047210Eh
  004720d4  391d3c195700          cmp       [57193Ch],ebx
  004720da  7532                  jne       short 0047210Eh
  004720dc  a120195700            mov       eax,[571920h]
  004720e1  683c0f0000            push      0F3Ch
  004720e6  6aff                  push      0FFFFFFFFh
  004720e8  56                    push      esi
  004720e9  8d0c00                lea       ecx,[eax+eax]
  004720ec  890d20195700          mov       [571920h],ecx
  004720f2  e8d9fcffff            call      00471DD0h
  004720f7  a120195700            mov       eax,[571920h]
  004720fc  83c40c                add       esp,0Ch
  004720ff  99                    cdq
  00472100  2bc2                  sub       eax,edx
  00472102  d1f8                  sar       eax,1
  00472104  a320195700            mov       [571920h],eax
  00472109  a174195700            mov       eax,[571974h]

LAB_0047210e:
  0047210e  3d024e0000            cmp       eax,4E02h
  00472113  7241                  jb        short 00472156h
  00472115  3d844e0000            cmp       eax,4E84h
  0047211a  773a                  ja        short 00472156h
  0047211c  391d3c195700          cmp       [57193Ch],ebx
  00472122  7532                  jne       short 00472156h
  00472124  8b1520195700          mov       edx,[571920h]
  0047212a  6814050000            push      514h
  0047212f  6aff                  push      0FFFFFFFFh
  00472131  56                    push      esi
  00472132  8d0412                lea       eax,[edx+edx]
  00472135  a320195700            mov       [571920h],eax
  0047213a  e891fcffff            call      00471DD0h
  0047213f  a120195700            mov       eax,[571920h]
  00472144  83c40c                add       esp,0Ch
  00472147  99                    cdq
  00472148  2bc2                  sub       eax,edx
  0047214a  d1f8                  sar       eax,1
  0047214c  a320195700            mov       [571920h],eax
  00472151  a174195700            mov       eax,[571974h]

LAB_00472156:
  00472156  3d406a0000            cmp       eax,6A40h
  0047215b  7224                  jb        short 00472181h
  0047215d  3d086b0000            cmp       eax,6B08h
  00472162  771d                  ja        short 00472181h
  00472164  391d3c195700          cmp       [57193Ch],ebx
  0047216a  7515                  jne       short 00472181h
  0047216c  68c4f0ffff            push      0FFFFF0C4h
  00472171  6aff                  push      0FFFFFFFFh
  00472173  56                    push      esi
  00472174  e857fcffff            call      00471DD0h
  00472179  a174195700            mov       eax,[571974h]
  0047217e  83c40c                add       esp,0Ch

LAB_00472181:
  00472181  3d80700000            cmp       eax,7080h
  00472186  7224                  jb        short 004721ACh
  00472188  3d48710000            cmp       eax,7148h
  0047218d  771d                  ja        short 004721ACh
  0047218f  391d3c195700          cmp       [57193Ch],ebx
  00472195  7515                  jne       short 004721ACh
  00472197  680cfeffff            push      0FFFFFE0Ch
  0047219c  6aff                  push      0FFFFFFFFh
  0047219e  56                    push      esi
  0047219f  e82cfcffff            call      00471DD0h
  004721a4  a174195700            mov       eax,[571974h]
  004721a9  83c40c                add       esp,0Ch

LAB_004721ac:
  004721ac  3d707b0000            cmp       eax,7B70h
  004721b1  7224                  jb        short 004721D7h
  004721b3  3d387c0000            cmp       eax,7C38h
  004721b8  771d                  ja        short 004721D7h
  004721ba  391d3c195700          cmp       [57193Ch],ebx
  004721c0  7515                  jne       short 004721D7h
  004721c2  687cfcffff            push      0FFFFFC7Ch
  004721c7  6aff                  push      0FFFFFFFFh
  004721c9  56                    push      esi
  004721ca  e801fcffff            call      00471DD0h
  004721cf  a174195700            mov       eax,[571974h]
  004721d4  83c40c                add       esp,0Ch

LAB_004721d7:
  004721d7  3dc67f0000            cmp       eax,7FC6h
  004721dc  7223                  jb        short 00472201h
  004721de  3de8800000            cmp       eax,80E8h
  004721e3  771c                  ja        short 00472201h
  004721e5  391d3c195700          cmp       [57193Ch],ebx
  004721eb  7514                  jne       short 00472201h
  004721ed  6884030000            push      384h
  004721f2  53                    push      ebx
  004721f3  56                    push      esi
  004721f4  e8d7fbffff            call      00471DD0h
  004721f9  a174195700            mov       eax,[571974h]
  004721fe  83c40c                add       esp,0Ch

LAB_00472201:
  00472201  3d7c830000            cmp       eax,837Ch
  00472206  7224                  jb        short 0047222Ch
  00472208  3d08840000            cmp       eax,8408h
  0047220d  771d                  ja        short 0047222Ch
  0047220f  391d3c195700          cmp       [57193Ch],ebx
  00472215  7515                  jne       short 0047222Ch
  00472217  682c010000            push      12Ch
  0047221c  6aff                  push      0FFFFFFFFh
  0047221e  56                    push      esi
  0047221f  e8acfbffff            call      00471DD0h
  00472224  a174195700            mov       eax,[571974h]
  00472229  83c40c                add       esp,0Ch

LAB_0047222c:
  0047222c  3dc08f0000            cmp       eax,8FC0h
  00472231  7224                  jb        short 00472257h
  00472233  3d88900000            cmp       eax,9088h
  00472238  771d                  ja        short 00472257h
  0047223a  391d3c195700          cmp       [57193Ch],ebx
  00472240  7515                  jne       short 00472257h
  00472242  68cc100000            push      10CCh
  00472247  6aff                  push      0FFFFFFFFh
  00472249  56                    push      esi
  0047224a  e881fbffff            call      00471DD0h
  0047224f  a174195700            mov       eax,[571974h]
  00472254  83c40c                add       esp,0Ch

LAB_00472257:
  00472257  3d6c9d0000            cmp       eax,9D6Ch
  0047225c  7243                  jb        short 004722A1h
  0047225e  3d349e0000            cmp       eax,9E34h
  00472263  771c                  ja        short 00472281h
  00472265  391d3c195700          cmp       [57193Ch],ebx
  0047226b  7514                  jne       short 00472281h
  0047226d  688cf1ffff            push      0FFFFF18Ch
  00472272  53                    push      ebx
  00472273  56                    push      esi
  00472274  e857fbffff            call      00471DD0h
  00472279  a174195700            mov       eax,[571974h]
  0047227e  83c40c                add       esp,0Ch

LAB_00472281:
  00472281  3d6c9d0000            cmp       eax,9D6Ch
  00472286  7219                  jb        short 004722A1h
  00472288  8b4e1c                mov       ecx,[esi+1Ch]
  0047228b  81f900c018ff          cmp       ecx,0FF18C000h
  00472291  7c0e                  jl        short 004722A1h
  00472293  81c10050ffff          add       ecx,0FFFF5000h
  00472299  894e1c                mov       [esi+1Ch],ecx
  0047229c  a174195700            mov       eax,[571974h]

LAB_004722a1:
  004722a1  3d1ca20000            cmp       eax,0A21Ch
  004722a6  7248                  jb        short 004722F0h
  004722a8  a120195700            mov       eax,[571920h]
  004722ad  bf08000000            mov       edi,8
  004722b2  48                    dec       eax
  004722b3  a320195700            mov       [571920h],eax

LAB_004722b8:
  004722b8  e8834e0d00            call      00547140h
  004722bd  99                    cdq
  004722be  33c2                  xor       eax,edx
  004722c0  2bc2                  sub       eax,edx
  004722c2  83e003                and       eax,3
  004722c5  33c2                  xor       eax,edx
  004722c7  2bc2                  sub       eax,edx
  004722c9  40                    inc       eax
  004722ca  50                    push      eax
  004722cb  56                    push      esi
  004722cc  e85f030000            call      00472630h
  004722d1  83c408                add       esp,8
  004722d4  4f                    dec       edi
  004722d5  75e1                  jne       short 004722B8h
  004722d7  a120195700            mov       eax,[571920h]
  004722dc  85c0                  test      eax,eax
  004722de  7f10                  jg        short 004722F0h
  004722e0  c7052019570000000000  mov       dword [571920h],0
  004722ea  66c746120200          mov       word [esi+12h],2

LAB_004722f0:
  004722f0  668b4616              mov       ax,[esi+16h]
  004722f4  663bc3                cmp       ax,bx
  004722f7  7c06                  jl        short 004722FFh
  004722f9  663d0300              cmp       ax,3
  004722fd  7e0c                  jle       short 0047230Bh

LAB_004722ff:
  004722ff  663d0900              cmp       ax,9
  00472303  7406                  je        short 0047230Bh
  00472305  663d0a00              cmp       ax,0Ah
  00472309  752b                  jne       short 00472336h

LAB_0047230b:
  0047230b  0fbf4e10              movsx     ecx,word [esi+10h]
  0047230f  8bc1                  mov       eax,ecx
  00472311  99                    cdq
  00472312  2bc2                  sub       eax,edx
  00472314  d1f8                  sar       eax,1
  00472316  d1e0                  shl       eax,1
  00472318  3bc8                  cmp       ecx,eax
  0047231a  751a                  jne       short 00472336h
  0047231c  a120195700            mov       eax,[571920h]
  00472321  40                    inc       eax
  00472322  83f83c                cmp       eax,3Ch
  00472325  a320195700            mov       [571920h],eax
  0047232a  7c0a                  jl        short 00472336h
  0047232c  c705201957003c000000  mov       dword [571920h],3Ch

LAB_00472336:
  00472336  668b4616              mov       ax,[esi+16h]
  0047233a  663d3900              cmp       ax,39h
  0047233e  7c3f                  jl        short 0047237Fh
  00472340  663d3d00              cmp       ax,3Dh
  00472344  7f39                  jg        short 0047237Fh
  00472346  8b0d20195700          mov       ecx,[571920h]
  0047234c  83c103                add       ecx,3
  0047234f  890d20195700          mov       [571920h],ecx
  00472355  66837e163a            cmp       word [esi+16h],3Ah
  0047235a  750f                  jne       short 0047236Bh
  0047235c  6800feffff            push      0FFFFFE00h
  00472361  53                    push      ebx
  00472362  56                    push      esi
  00472363  e838020000            call      004725A0h
  00472368  83c40c                add       esp,0Ch

LAB_0047236b:
  0047236b  8b0d20195700          mov       ecx,[571920h]
  00472371  b86e000000            mov       eax,6Eh
  00472376  3bc8                  cmp       ecx,eax
  00472378  7c05                  jl        short 0047237Fh
  0047237a  a320195700            mov       [571920h],eax

LAB_0047237f:
  0047237f  668b4616              mov       ax,[esi+16h]
  00472383  bb2d000000            mov       ebx,2Dh
  00472388  663d2c00              cmp       ax,2Ch
  0047238c  7c36                  jl        short 004723C4h
  0047238e  663d2e00              cmp       ax,2Eh
  00472392  7f30                  jg        short 004723C4h
  00472394  a120195700            mov       eax,[571920h]
  00472399  48                    dec       eax
  0047239a  83f80a                cmp       eax,0Ah
  0047239d  a320195700            mov       [571920h],eax
  004723a2  7f0a                  jg        short 004723AEh
  004723a4  c705201957000a000000  mov       dword [571920h],0Ah

LAB_004723ae:
  004723ae  66395e16              cmp       [esi+16h],bx
  004723b2  7510                  jne       short 004723C4h
  004723b4  6800020000            push      200h
  004723b9  6a01                  push      1
  004723bb  56                    push      esi
  004723bc  e8df010000            call      004725A0h
  004723c1  83c40c                add       esp,0Ch

LAB_004723c4:
  004723c4  668b4616              mov       ax,[esi+16h]
  004723c8  663d4100              cmp       ax,41h
  004723cc  7509                  jne       short 004723D7h
  004723ce  833d3019570005        cmp       dword [571930h],5
  004723d5  7415                  je        short 004723ECh

LAB_004723d7:
  004723d7  663d4700              cmp       ax,47h
  004723db  7c42                  jl        short 0047241Fh
  004723dd  663d4a00              cmp       ax,4Ah
  004723e1  7f3c                  jg        short 0047241Fh
  004723e3  833d3019570005        cmp       dword [571930h],5
  004723ea  7533                  jne       short 0047241Fh

LAB_004723ec:
  004723ec  a120195700            mov       eax,[571920h]
  004723f1  83e803                sub       eax,3
  004723f4  83f80a                cmp       eax,0Ah
  004723f7  a320195700            mov       [571920h],eax
  004723fc  7f0a                  jg        short 00472408h
  004723fe  c705201957000a000000  mov       dword [571920h],0Ah

LAB_00472408:
  00472408  66837e1649            cmp       word [esi+16h],49h
  0047240d  7510                  jne       short 0047241Fh
  0047240f  6800020000            push      200h
  00472414  6a01                  push      1
  00472416  56                    push      esi
  00472417  e884010000            call      004725A0h
  0047241c  83c40c                add       esp,0Ch

LAB_0047241f:
  0047241f  668b4616              mov       ax,[esi+16h]
  00472423  663d5c00              cmp       ax,5Ch
  00472427  7c75                  jl        short 0047249Eh
  00472429  663d5d00              cmp       ax,5Dh
  0047242d  7f6f                  jg        short 0047249Eh
  0047242f  833d3019570005        cmp       dword [571930h],5
  00472436  7566                  jne       short 0047249Eh
  00472438  bf08000000            mov       edi,8

LAB_0047243d:
  0047243d  e8fe4c0d00            call      00547140h
  00472442  99                    cdq
  00472443  33c2                  xor       eax,edx
  00472445  2bc2                  sub       eax,edx
  00472447  83e003                and       eax,3
  0047244a  33c2                  xor       eax,edx
  0047244c  2bc2                  sub       eax,edx
  0047244e  40                    inc       eax
  0047244f  50                    push      eax
  00472450  56                    push      esi
  00472451  e8da010000            call      00472630h
  00472456  83c408                add       esp,8
  00472459  4f                    dec       edi
  0047245a  75e1                  jne       short 0047243Dh
  0047245c  0fbf4e10              movsx     ecx,word [esi+10h]
  00472460  8bc1                  mov       eax,ecx
  00472462  99                    cdq
  00472463  2bc2                  sub       eax,edx
  00472465  d1f8                  sar       eax,1
  00472467  d1e0                  shl       eax,1
  00472469  3bc8                  cmp       ecx,eax
  0047246b  751a                  jne       short 00472487h
  0047246d  a120195700            mov       eax,[571920h]
  00472472  48                    dec       eax
  00472473  83f80a                cmp       eax,0Ah
  00472476  a320195700            mov       [571920h],eax
  0047247b  7f0a                  jg        short 00472487h
  0047247d  c705201957000a000000  mov       dword [571920h],0Ah

LAB_00472487:
  00472487  66837e165c            cmp       word [esi+16h],5Ch
  0047248c  7510                  jne       short 0047249Eh
  0047248e  6800feffff            push      0FFFFFE00h
  00472493  6a01                  push      1
  00472495  56                    push      esi
  00472496  e805010000            call      004725A0h
  0047249b  83c40c                add       esp,0Ch

LAB_0047249e:
  0047249e  668b4616              mov       ax,[esi+16h]
  004724a2  663d5e00              cmp       ax,5Eh
  004724a6  0f8c92000000          jl        near 0047253Eh
  004724ac  663d6000              cmp       ax,60h
  004724b0  0f8f88000000          jg        near 0047253Eh
  004724b6  833d3019570005        cmp       dword [571930h],5
  004724bd  757f                  jne       short 0047253Eh
  004724bf  8b0d78195700          mov       ecx,[571978h]
  004724c5  b800020000            mov       eax,200h
  004724ca  85c8                  test      eax,ecx
  004724cc  750e                  jne       short 004724DCh
  004724ce  0bc8                  or        ecx,eax
  004724d0  890d78195700          mov       [571978h],ecx
  004724d6  66c746140a00          mov       word [esi+14h],0Ah

LAB_004724dc:
  004724dc  66834614fd            add       word [esi+14h],0FFFDh
  004724e1  668b4614              mov       ax,[esi+14h]
  004724e5  8b7e20                mov       edi,[esi+20h]
  004724e8  66834634ec            add       word [esi+34h],0FFECh
  004724ed  0fbfc8                movsx     ecx,ax
  004724f0  c1e10c                shl       ecx,0Ch
  004724f3  03f9                  add       edi,ecx
  004724f5  8bc7                  mov       eax,edi
  004724f7  897e20                mov       [esi+20h],edi
  004724fa  2500f0ffff            and       eax,0FFFFF000h
  004724ff  3d0000ceff            cmp       eax,0FFCE0000h
  00472504  7f38                  jg        short 0047253Eh
  00472506  c746200000ceff        mov       dword [esi+20h],0FFCE0000h
  0047250d  66c746340000          mov       word [esi+34h],0
  00472513  8b15cc975e00          mov       edx,[5E97CCh]
  00472519  6800150000            push      1500h
  0047251e  6800150000            push      1500h
  00472523  6800030000            push      300h
  00472528  8b82a8000000          mov       eax,[edx+0A8h]
  0047252e  33d2                  xor       edx,edx
  00472530  8b08                  mov       ecx,[eax]
  00472532  8a5134                mov       dl,[ecx+34h]
  00472535  52                    push      edx
  00472536  e865a90c00            call      0053CEA0h
  0047253b  83c410                add       esp,10h

LAB_0047253e:
  0047253e  66837e1661            cmp       word [esi+16h],61h
  00472543  7530                  jne       short 00472575h
  00472545  a178195700            mov       eax,[571978h]
  0047254a  f6c402                test      ah,2
  0047254d  7426                  je        short 00472575h
  0047254f  80e4fd                and       ah,0FDh
  00472552  bf0b000000            mov       edi,0Bh
  00472557  a378195700            mov       [571978h],eax

LAB_0047255c:
  0047255c  6a02                  push      2
  0047255e  56                    push      esi
  0047255f  e8cc000000            call      00472630h
  00472564  83c408                add       esp,8
  00472567  6a03                  push      3
  00472569  56                    push      esi
  0047256a  e8c1000000            call      00472630h
  0047256f  83c408                add       esp,8
  00472572  4f                    dec       edi
  00472573  75e7                  jne       short 0047255Ch

LAB_00472575:
  00472575  66837e1665            cmp       word [esi+16h],65h
  0047257a  751e                  jne       short 0047259Ah
  0047257c  833d3019570005        cmp       dword [571930h],5
  00472583  7515                  jne       short 0047259Ah
  00472585  a120195700            mov       eax,[571920h]
  0047258a  40                    inc       eax
  0047258b  3bc3                  cmp       eax,ebx
  0047258d  a320195700            mov       [571920h],eax
  00472592  7c06                  jl        short 0047259Ah
  00472594  891d20195700          mov       [571920h],ebx

LAB_0047259a:
  0047259a  5f                    pop       edi
  0047259b  5e                    pop       esi
  0047259c  5b                    pop       ebx
  0047259d  c3                    ret
  0047259e  90                    nop
  0047259f  90                    nop

47 cible(s) de saut interne : 0x00471e31, 0x00471e7e, 0x00471ee5, 0x00471f0b, 0x00471f16, 0x00471f37, 0x00471f49, 0x00471f69, 0x00471f75, 0x00471fc6, 0x00471ff1, 0x0047201c, 0x00472046, 0x00472071, 0x0047209c, 0x004720c6, 0x0047210e, 0x00472156, 0x00472181, 0x004721ac, 0x004721d7, 0x00472201, 0x0047222c, 0x00472257, 0x00472281, 0x004722a1, 0x004722b8, 0x004722f0, 0x004722ff, 0x0047230b, 0x00472336, 0x0047236b, 0x0047237f, 0x004723ae, 0x004723c4, 0x004723d7, 0x004723ec, 0x00472408, 0x0047241f, 0x0047243d, 0x00472487, 0x0047249e, 0x004724dc, 0x0047253e, 0x0047255c, 0x00472575, 0x0047259a
