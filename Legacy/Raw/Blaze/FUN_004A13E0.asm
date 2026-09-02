C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a13e0  section .text  offset fichier 0xa07e0  2416 octets

  004a13e0  83ec2c                sub       esp,2Ch
  004a13e3  53                    push      ebx
  004a13e4  55                    push      ebp
  004a13e5  56                    push      esi
  004a13e6  8b74243c              mov       esi,[esp+3Ch]
  004a13ea  8d44241c              lea       eax,[esp+1Ch]
  004a13ee  57                    push      edi
  004a13ef  8d4c2438              lea       ecx,[esp+38h]
  004a13f3  50                    push      eax
  004a13f4  8d542428              lea       edx,[esp+28h]
  004a13f8  51                    push      ecx
  004a13f9  52                    push      edx
  004a13fa  56                    push      esi
  004a13fb  e880750900            call      00538980h
  004a1400  83c410                add       esp,10h
  004a1403  8d442428              lea       eax,[esp+28h]
  004a1407  8d4c241c              lea       ecx,[esp+1Ch]
  004a140b  8d542410              lea       edx,[esp+10h]
  004a140f  50                    push      eax
  004a1410  51                    push      ecx
  004a1411  8d442420              lea       eax,[esp+20h]
  004a1415  52                    push      edx
  004a1416  8d4c244c              lea       ecx,[esp+4Ch]
  004a141a  50                    push      eax
  004a141b  51                    push      ecx
  004a141c  56                    push      esi
  004a141d  e8fe4a0900            call      00535F20h
  004a1422  8b4c2458              mov       ecx,[esp+58h]
  004a1426  b840000000            mov       eax,40h
  004a142b  2bc1                  sub       eax,ecx
  004a142d  83c418                add       esp,18h
  004a1430  89442414              mov       [esp+14h],eax
  004a1434  8b44241c              mov       eax,[esp+1Ch]
  004a1438  85c0                  test      eax,eax
  004a143a  bb03000000            mov       ebx,3
  004a143f  bd10000000            mov       ebp,10h
  004a1444  0f845e050000          je        near 004A19A8h
  004a144a  56                    push      esi
  004a144b  e850a10300            call      004DB5A0h
  004a1450  8b06                  mov       eax,[esi]
  004a1452  83c404                add       esp,4
  004a1455  a802                  test      al,2
  004a1457  0f844a030000          je        near 004A17A7h
  004a145d  f644242810            test      byte [esp+28h],10h
  004a1462  740d                  je        short 004A1471h
  004a1464  24fd                  and       al,0FDh
  004a1466  c6464300              mov       byte [esi+43h],0
  004a146a  8906                  mov       [esi],eax
  004a146c  e937050000            jmp       004A19A8h

LAB_004a1471:
  004a1471  33c9                  xor       ecx,ecx
  004a1473  8a4e42                mov       cl,[esi+42h]
  004a1476  83e900                sub       ecx,0
  004a1479  0f8458020000          je        near 004A16D7h
  004a147f  49                    dec       ecx
  004a1480  0f8522050000          jne       near 004A19A8h
  004a1486  0fbf4610              movsx     eax,word [esi+10h]
  004a148a  83e800                sub       eax,0
  004a148d  0f84a6000000          je        near 004A1539h
  004a1493  48                    dec       eax
  004a1494  0f850e050000          jne       near 004A19A8h
  004a149a  66396e1a              cmp       [esi+1Ah],bp
  004a149e  c7465c00000000        mov       dword [esi+5Ch],0
  004a14a5  7555                  jne       short 004A14FCh
  004a14a7  8b542440              mov       edx,[esp+40h]
  004a14ab  0fbf4632              movsx     eax,word [esi+32h]
  004a14af  81c200050000          add       edx,500h
  004a14b5  50                    push      eax
  004a14b6  c1e207                shl       edx,7
  004a14b9  89565c                mov       [esi+5Ch],edx
  004a14bc  e85b450a00            call      00545A1Ch
  004a14c1  0faf465c              imul      eax,[esi+5Ch]
  004a14c5  8b4e50                mov       ecx,[esi+50h]
  004a14c8  c1f80c                sar       eax,0Ch
  004a14cb  03c8                  add       ecx,eax
  004a14cd  894e50                mov       [esi+50h],ecx
  004a14d0  0fbf4e32              movsx     ecx,word [esi+32h]
  004a14d4  51                    push      ecx
  004a14d5  e83c450a00            call      00545A16h
  004a14da  0faf465c              imul      eax,[esi+5Ch]
  004a14de  8b5e58                mov       ebx,[esi+58h]
  004a14e1  c1f80c                sar       eax,0Ch
  004a14e4  03d8                  add       ebx,eax
  004a14e6  8b442440              mov       eax,[esp+40h]
  004a14ea  895e58                mov       [esi+58h],ebx
  004a14ed  8d5060                lea       edx,[eax+60h]
  004a14f0  c1e207                shl       edx,7
  004a14f3  66ff4e10              dec       word [esi+10h]
  004a14f7  89565c                mov       [esi+5Ch],edx
  004a14fa  eb04                  jmp       short 004A1500h

LAB_004a14fc:
  004a14fc  8b442440              mov       eax,[esp+40h]

LAB_004a1500:
  004a1500  8b4c2410              mov       ecx,[esp+10h]
  004a1504  d1f8                  sar       eax,1
  004a1506  83c040                add       eax,40h
  004a1509  8d1408                lea       edx,[eax+ecx]
  004a150c  8d3c00                lea       edi,[eax+eax]
  004a150f  81e2ff0f0000          and       edx,0FFFh
  004a1515  3bd7                  cmp       edx,edi
  004a1517  7f09                  jg        short 004A1522h
  004a1519  66014e32              add       [esi+32h],cx
  004a151d  e986040000            jmp       004A19A8h

LAB_004a1522:
  004a1522  81e1ff0f0000          and       ecx,0FFFh
  004a1528  81f900080000          cmp       ecx,800h
  004a152e  0f8c70040000          jl        near 004A19A4h
  004a1534  e969040000            jmp       004A19A2h

LAB_004a1539:
  004a1539  8b4650                mov       eax,[esi+50h]
  004a153c  8b4e58                mov       ecx,[esi+58h]
  004a153f  c1f80c                sar       eax,0Ch
  004a1542  8bd0                  mov       edx,eax
  004a1544  c1f90c                sar       ecx,0Ch
  004a1547  0fafd0                imul      edx,eax
  004a154a  8bc1                  mov       eax,ecx
  004a154c  0fafc1                imul      eax,ecx
  004a154f  03d0                  add       edx,eax
  004a1551  52                    push      edx
  004a1552  e8b9440a00            call      00545A10h
  004a1557  8b16                  mov       edx,[esi]
  004a1559  8b4c2440              mov       ecx,[esp+40h]
  004a155d  f7c200000002          test      edx,2000000h
  004a1563  740f                  je        short 004A1574h
  004a1565  8bf9                  mov       edi,ecx
  004a1567  c1ff03                sar       edi,3
  004a156a  47                    inc       edi
  004a156b  3bc7                  cmp       eax,edi
  004a156d  7f05                  jg        short 004A1574h
  004a156f  83e2dd                and       edx,0FFFFFFDDh
  004a1572  8916                  mov       [esi],edx

LAB_004a1574:
  004a1574  3bc3                  cmp       eax,ebx
  004a1576  0f8c2c040000          jl        near 004A19A8h
  004a157c  f60620                test      byte [esi],20h
  004a157f  0f8523040000          jne       near 004A19A8h
  004a1585  c1f903                sar       ecx,3
  004a1588  83c120                add       ecx,20h
  004a158b  8bc1                  mov       eax,ecx
  004a158d  8b4c2410              mov       ecx,[esp+10h]
  004a1591  8d1408                lea       edx,[eax+ecx]
  004a1594  8d3c00                lea       edi,[eax+eax]
  004a1597  81e2ff0f0000          and       edx,0FFFh
  004a159d  3bd7                  cmp       edx,edi
  004a159f  7f06                  jg        short 004A15A7h
  004a15a1  66014e32              add       [esi+32h],cx
  004a15a5  eb14                  jmp       short 004A15BBh

LAB_004a15a7:
  004a15a7  81e1ff0f0000          and       ecx,0FFFh
  004a15ad  81f900080000          cmp       ecx,800h
  004a15b3  7c02                  jl        short 004A15B7h
  004a15b5  f7d8                  neg       eax

LAB_004a15b7:
  004a15b7  66014632              add       [esi+32h],ax

LAB_004a15bb:
  004a15bb  8d6e1c                lea       ebp,[esi+1Ch]
  004a15be  6a00                  push      0
  004a15c0  68bc295500            push      5529BCh
  004a15c5  55                    push      ebp
  004a15c6  56                    push      esi
  004a15c7  e864860700            call      00519C30h
  004a15cc  8bf8                  mov       edi,eax
  004a15ce  83c410                add       esp,10h
  004a15d1  85ff                  test      edi,edi
  004a15d3  7406                  je        short 004A15DBh
  004a15d5  8b06                  mov       eax,[esi]
  004a15d7  0c20                  or        al,20h
  004a15d9  8906                  mov       [esi],eax

LAB_004a15db:
  004a15db  a1b8e05d00            mov       eax,[5DE0B8h]
  004a15e0  85c0                  test      eax,eax
  004a15e2  0f84c0030000          je        near 004A19A8h
  004a15e8  8b1f                  mov       ebx,[edi]
  004a15ea  66c787960000001000    mov       word [edi+96h],10h
  004a15f3  83cb40                or        ebx,40h
  004a15f6  57                    push      edi
  004a15f7  891f                  mov       [edi],ebx
  004a15f9  8b4c2424              mov       ecx,[esp+24h]
  004a15fd  56                    push      esi
  004a15fe  668b4130              mov       ax,[ecx+30h]
  004a1602  0fbfd8                movsx     ebx,ax
  004a1605  66d1f8                sar       ax,1
  004a1608  66894130              mov       [ecx+30h],ax
  004a160c  e86f850900            call      00539B80h
  004a1611  8b442428              mov       eax,[esp+28h]
  004a1615  83c408                add       esp,8
  004a1618  66895830              mov       [eax+30h],bx
  004a161c  8d5f1c                lea       ebx,[edi+1Ch]
  004a161f  53                    push      ebx
  004a1620  6800180000            push      1800h
  004a1625  6800030000            push      300h
  004a162a  6a03                  push      3
  004a162c  e8efc40900            call      0053DB20h
  004a1631  8b07                  mov       eax,[edi]
  004a1633  83c410                add       esp,10h
  004a1636  a900000040            test      eax,40000000h
  004a163b  0f8567030000          jne       near 004A19A8h
  004a1641  0fbf4e32              movsx     ecx,word [esi+32h]
  004a1645  8b542410              mov       edx,[esp+10h]
  004a1649  03d1                  add       edx,ecx
  004a164b  52                    push      edx
  004a164c  e8cb430a00            call      00545A1Ch
  004a1651  8d0440                lea       eax,[eax+eax*2]
  004a1654  8d0480                lea       eax,[eax+eax*4]
  004a1657  d1e0                  shl       eax,1
  004a1659  894750                mov       [edi+50h],eax
  004a165c  8b542410              mov       edx,[esp+10h]
  004a1660  0fbf4e32              movsx     ecx,word [esi+32h]
  004a1664  03d1                  add       edx,ecx
  004a1666  52                    push      edx
  004a1667  e8aa430a00            call      00545A16h
  004a166c  8d0440                lea       eax,[eax+eax*2]
  004a166f  8d0480                lea       eax,[eax+eax*4]
  004a1672  d1e0                  shl       eax,1
  004a1674  894758                mov       [edi+58h],eax
  004a1677  8b07                  mov       eax,[edi]
  004a1679  a801                  test      al,1
  004a167b  750b                  jne       short 004A1688h
  004a167d  0c01                  or        al,1
  004a167f  c7475400800100        mov       dword [edi+54h],18000h
  004a1686  8907                  mov       [edi],eax

LAB_004a1688:
  004a1688  8b4d00                mov       ecx,[ebp]
  004a168b  8b5620                mov       edx,[esi+20h]
  004a168e  8b4624                mov       eax,[esi+24h]
  004a1691  894c242c              mov       [esp+2Ch],ecx
  004a1695  8b0b                  mov       ecx,[ebx]
  004a1697  89542430              mov       [esp+30h],edx
  004a169b  894d00                mov       [ebp],ecx
  004a169e  8b5720                mov       edx,[edi+20h]
  004a16a1  6aff                  push      0FFFFFFFFh
  004a16a3  89442438              mov       [esp+38h],eax
  004a16a7  895620                mov       [esi+20h],edx
  004a16aa  8b4724                mov       eax,[edi+24h]
  004a16ad  6a00                  push      0
  004a16af  6a39                  push      39h
  004a16b1  56                    push      esi
  004a16b2  894624                mov       [esi+24h],eax
  004a16b5  e81693fdff            call      0047A9D0h
  004a16ba  8b4c243c              mov       ecx,[esp+3Ch]
  004a16be  8b542440              mov       edx,[esp+40h]
  004a16c2  8b442444              mov       eax,[esp+44h]
  004a16c6  83c410                add       esp,10h
  004a16c9  894d00                mov       [ebp],ecx
  004a16cc  895620                mov       [esi+20h],edx
  004a16cf  894624                mov       [esi+24h],eax
  004a16d2  e9d1020000            jmp       004A19A8h

LAB_004a16d7:
  004a16d7  66395e18              cmp       [esi+18h],bx
  004a16db  740a                  je        short 004A16E7h
  004a16dd  25fffffffe            and       eax,0FEFFFFFFh
  004a16e2  895e18                mov       [esi+18h],ebx
  004a16e5  8906                  mov       [esi],eax

LAB_004a16e7:
  004a16e7  8b442440              mov       eax,[esp+40h]
  004a16eb  8b4c2410              mov       ecx,[esp+10h]
  004a16ef  d1f8                  sar       eax,1
  004a16f1  83c040                add       eax,40h
  004a16f4  8d1408                lea       edx,[eax+ecx]
  004a16f7  8d3c00                lea       edi,[eax+eax]
  004a16fa  81e2ff0f0000          and       edx,0FFFh
  004a1700  3bd7                  cmp       edx,edi
  004a1702  7f06                  jg        short 004A170Ah
  004a1704  66014e32              add       [esi+32h],cx
  004a1708  eb14                  jmp       short 004A171Eh

LAB_004a170a:
  004a170a  81e1ff0f0000          and       ecx,0FFFh
  004a1710  81f900080000          cmp       ecx,800h
  004a1716  7c02                  jl        short 004A171Ah
  004a1718  f7d8                  neg       eax

LAB_004a171a:
  004a171a  66014632              add       [esi+32h],ax

LAB_004a171e:
  004a171e  6a00                  push      0
  004a1720  8d461c                lea       eax,[esi+1Ch]
  004a1723  68b0295500            push      5529B0h
  004a1728  50                    push      eax
  004a1729  56                    push      esi
  004a172a  e801850700            call      00519C30h
  004a172f  8bf8                  mov       edi,eax
  004a1731  a1b8e05d00            mov       eax,[5DE0B8h]
  004a1736  83c410                add       esp,10h
  004a1739  85c0                  test      eax,eax
  004a173b  742f                  je        short 004A176Ch
  004a173d  6689af96000000        mov       [edi+96h],bp
  004a1744  8b2f                  mov       ebp,[edi]
  004a1746  83cd40                or        ebp,40h
  004a1749  57                    push      edi
  004a174a  56                    push      esi
  004a174b  892f                  mov       [edi],ebp
  004a174d  e82e840900            call      00539B80h
  004a1752  83c408                add       esp,8
  004a1755  83c71c                add       edi,1Ch
  004a1758  57                    push      edi
  004a1759  6800180000            push      1800h
  004a175e  6800030000            push      300h
  004a1763  53                    push      ebx
  004a1764  e8b7c30900            call      0053DB20h
  004a1769  83c410                add       esp,10h

LAB_004a176c:
  004a176c  8b4c2440              mov       ecx,[esp+40h]
  004a1770  8b465c                mov       eax,[esi+5Ch]
  004a1773  81c180020000          add       ecx,280h
  004a1779  c1e107                shl       ecx,7
  004a177c  2bc8                  sub       ecx,eax
  004a177e  c1f904                sar       ecx,4
  004a1781  03c8                  add       ecx,eax
  004a1783  8a4643                mov       al,[esi+43h]
  004a1786  84c0                  test      al,al
  004a1788  894e5c                mov       [esi+5Ch],ecx
  004a178b  0f8417020000          je        near 004A19A8h
  004a1791  fec8                  dec       al
  004a1793  884643                mov       [esi+43h],al
  004a1796  0f850c020000          jne       near 004A19A8h
  004a179c  8b06                  mov       eax,[esi]
  004a179e  24fd                  and       al,0FDh
  004a17a0  8906                  mov       [esi],eax
  004a17a2  e901020000            jmp       004A19A8h

LAB_004a17a7:
  004a17a7  a810                  test      al,10h
  004a17a9  0f85f9010000          jne       near 004A19A8h
  004a17af  a904080000            test      eax,804h
  004a17b4  0f85ee010000          jne       near 004A19A8h
  004a17ba  33c9                  xor       ecx,ecx
  004a17bc  8a4e42                mov       cl,[esi+42h]
  004a17bf  3bcb                  cmp       ecx,ebx
  004a17c1  0f87e1010000          ja        near 004A19A8h
  004a17c7  ff248dd41c4a00        jmp       dword [ecx*4+4A1CD4h]
  004a17ce  66837e1800            cmp       word [esi+18h],0
  004a17d3  740e                  je        short 004A17E3h
  004a17d5  0d00000001            or        eax,1000000h
  004a17da  c7461800000000        mov       dword [esi+18h],0
  004a17e1  8906                  mov       [esi],eax

LAB_004a17e3:
  004a17e3  817c24182c010000      cmp       dword [esp+18h],12Ch
  004a17eb  7d09                  jge       short 004A17F6h
  004a17ed  c6464300              mov       byte [esi+43h],0
  004a17f1  e9b2010000            jmp       004A19A8h

LAB_004a17f6:
  004a17f6  8a4643                mov       al,[esi+43h]
  004a17f9  84c0                  test      al,al
  004a17fb  7405                  je        short 004A1802h
  004a17fd  fec8                  dec       al
  004a17ff  884643                mov       [esi+43h],al

LAB_004a1802:
  004a1802  8b465c                mov       eax,[esi+5Ch]
  004a1805  8bd0                  mov       edx,eax
  004a1807  c1fa04                sar       edx,4
  004a180a  2bc2                  sub       eax,edx
  004a180c  89465c                mov       [esi+5Ch],eax
  004a180f  e994010000            jmp       004A19A8h
  004a1814  66837e1800            cmp       word [esi+18h],0
  004a1819  740e                  je        short 004A1829h
  004a181b  0d00000001            or        eax,1000000h
  004a1820  c7461800000000        mov       dword [esi+18h],0
  004a1827  8906                  mov       [esi],eax

LAB_004a1829:
  004a1829  8b442440              mov       eax,[esp+40h]
  004a182d  8b4c2410              mov       ecx,[esp+10h]
  004a1831  c1f802                sar       eax,2
  004a1834  83c038                add       eax,38h
  004a1837  8d1408                lea       edx,[eax+ecx]
  004a183a  8d3c00                lea       edi,[eax+eax]
  004a183d  81e2ff0f0000          and       edx,0FFFh
  004a1843  3bd7                  cmp       edx,edi
  004a1845  7f1a                  jg        short 004A1861h
  004a1847  8b465c                mov       eax,[esi+5Ch]
  004a184a  66014e32              add       [esi+32h],cx
  004a184e  8bc8                  mov       ecx,eax
  004a1850  c6464300              mov       byte [esi+43h],0
  004a1854  c1f904                sar       ecx,4
  004a1857  2bc1                  sub       eax,ecx
  004a1859  89465c                mov       [esi+5Ch],eax
  004a185c  e947010000            jmp       004A19A8h

LAB_004a1861:
  004a1861  81e1ff0f0000          and       ecx,0FFFh
  004a1867  81f900080000          cmp       ecx,800h
  004a186d  7c02                  jl        short 004A1871h
  004a186f  f7d8                  neg       eax

LAB_004a1871:
  004a1871  66014632              add       [esi+32h],ax
  004a1875  8b465c                mov       eax,[esi+5Ch]
  004a1878  8bc8                  mov       ecx,eax
  004a187a  c1f904                sar       ecx,4
  004a187d  2bc1                  sub       eax,ecx
  004a187f  89465c                mov       [esi+5Ch],eax
  004a1882  e921010000            jmp       004A19A8h
  004a1887  b901000000            mov       ecx,1
  004a188c  66394e18              cmp       [esi+18h],cx
  004a1890  740a                  je        short 004A189Ch
  004a1892  0d00000001            or        eax,1000000h
  004a1897  894e18                mov       [esi+18h],ecx
  004a189a  8906                  mov       [esi],eax

LAB_004a189c:
  004a189c  817c24182c010000      cmp       dword [esp+18h],12Ch
  004a18a4  7d09                  jge       short 004A18AFh
  004a18a6  c6464300              mov       byte [esi+43h],0
  004a18aa  e9f9000000            jmp       004A19A8h

LAB_004a18af:
  004a18af  8a4643                mov       al,[esi+43h]
  004a18b2  84c0                  test      al,al
  004a18b4  7405                  je        short 004A18BBh
  004a18b6  fec8                  dec       al
  004a18b8  884643                mov       [esi+43h],al

LAB_004a18bb:
  004a18bb  8b542440              mov       edx,[esp+40h]
  004a18bf  8b4c2410              mov       ecx,[esp+10h]
  004a18c3  8bc2                  mov       eax,edx
  004a18c5  c1f802                sar       eax,2
  004a18c8  83c038                add       eax,38h
  004a18cb  8d3c08                lea       edi,[eax+ecx]
  004a18ce  8d1c00                lea       ebx,[eax+eax]
  004a18d1  81e7ff0f0000          and       edi,0FFFh
  004a18d7  3bfb                  cmp       edi,ebx
  004a18d9  7f06                  jg        short 004A18E1h
  004a18db  66014e32              add       [esi+32h],cx
  004a18df  eb14                  jmp       short 004A18F5h

LAB_004a18e1:
  004a18e1  81e1ff0f0000          and       ecx,0FFFh
  004a18e7  81f900080000          cmp       ecx,800h
  004a18ed  7c02                  jl        short 004A18F1h
  004a18ef  f7d8                  neg       eax

LAB_004a18f1:
  004a18f1  66014632              add       [esi+32h],ax

LAB_004a18f5:
  004a18f5  8b465c                mov       eax,[esi+5Ch]
  004a18f8  81c2a0010000          add       edx,1A0h
  004a18fe  c1e207                shl       edx,7
  004a1901  2bd0                  sub       edx,eax
  004a1903  c1fa04                sar       edx,4
  004a1906  03d0                  add       edx,eax
  004a1908  89565c                mov       [esi+5Ch],edx
  004a190b  e998000000            jmp       004A19A8h
  004a1910  33ff                  xor       edi,edi
  004a1912  66397e18              cmp       [esi+18h],di
  004a1916  740a                  je        short 004A1922h
  004a1918  0d00000001            or        eax,1000000h
  004a191d  897e18                mov       [esi+18h],edi
  004a1920  8906                  mov       [esi],eax

LAB_004a1922:
  004a1922  8a4643                mov       al,[esi+43h]
  004a1925  84c0                  test      al,al
  004a1927  7405                  je        short 004A192Eh
  004a1929  fec8                  dec       al
  004a192b  884643                mov       [esi+43h],al

LAB_004a192e:
  004a192e  8b542440              mov       edx,[esp+40h]
  004a1932  0fbf4610              movsx     eax,word [esi+10h]
  004a1936  83c240                add       edx,40h
  004a1939  50                    push      eax
  004a193a  c1e207                shl       edx,7
  004a193d  89565c                mov       [esi+5Ch],edx
  004a1940  e8d7400a00            call      00545A1Ch
  004a1945  0faf465c              imul      eax,[esi+5Ch]
  004a1949  8b4e50                mov       ecx,[esi+50h]
  004a194c  c1f80c                sar       eax,0Ch
  004a194f  03c8                  add       ecx,eax
  004a1951  894e50                mov       [esi+50h],ecx
  004a1954  0fbf4e10              movsx     ecx,word [esi+10h]
  004a1958  51                    push      ecx
  004a1959  e8b8400a00            call      00545A16h
  004a195e  0faf465c              imul      eax,[esi+5Ch]
  004a1962  8b5e58                mov       ebx,[esi+58h]
  004a1965  8b4c2410              mov       ecx,[esp+10h]
  004a1969  c1f80c                sar       eax,0Ch
  004a196c  03d8                  add       ebx,eax
  004a196e  8b442440              mov       eax,[esp+40h]
  004a1972  c1f802                sar       eax,2
  004a1975  83c040                add       eax,40h
  004a1978  897e5c                mov       [esi+5Ch],edi
  004a197b  895e58                mov       [esi+58h],ebx
  004a197e  8d1408                lea       edx,[eax+ecx]
  004a1981  8d3c00                lea       edi,[eax+eax]
  004a1984  81e2ff0f0000          and       edx,0FFFh
  004a198a  3bd7                  cmp       edx,edi
  004a198c  7f06                  jg        short 004A1994h
  004a198e  66014e32              add       [esi+32h],cx
  004a1992  eb14                  jmp       short 004A19A8h

LAB_004a1994:
  004a1994  81e1ff0f0000          and       ecx,0FFFh
  004a199a  81f900080000          cmp       ecx,800h
  004a19a0  7c02                  jl        short 004A19A4h

LAB_004a19a2:
  004a19a2  f7d8                  neg       eax

LAB_004a19a4:
  004a19a4  66014632              add       [esi+32h],ax

LAB_004a19a8:
  004a19a8  8b44241c              mov       eax,[esp+1Ch]
  004a19ac  50                    push      eax
  004a19ad  56                    push      esi
  004a19ae  e82d8fffff            call      0049A8E0h
  004a19b3  83c408                add       esp,8
  004a19b6  56                    push      esi
  004a19b7  e8841f0900            call      00533940h
  004a19bc  8b466c                mov       eax,[esi+6Ch]
  004a19bf  83c404                add       esp,4
  004a19c2  f60001                test      byte [eax],1
  004a19c5  7506                  jne       short 004A19CDh
  004a19c7  8b4860                mov       ecx,[eax+60h]
  004a19ca  894e48                mov       [esi+48h],ecx

LAB_004a19cd:
  004a19cd  e86e570a00            call      00547140h
  004a19d2  8b7e60                mov       edi,[esi+60h]
  004a19d5  25ff000000            and       eax,0FFh
  004a19da  0500010000            add       eax,100h
  004a19df  66014614              add       [esi+14h],ax
  004a19e3  8b4648                mov       eax,[esi+48h]
  004a19e6  668b4e14              mov       cx,[esi+14h]
  004a19ea  3bc7                  cmp       eax,edi
  004a19ec  7c02                  jl        short 004A19F0h
  004a19ee  8bf8                  mov       edi,eax

LAB_004a19f0:
  004a19f0  f60602                test      byte [esi],2
  004a19f3  7415                  je        short 004A1A0Ah
  004a19f5  8b4620                mov       eax,[esi+20h]
  004a19f8  2bf8                  sub       edi,eax
  004a19fa  81c700800200          add       edi,28000h
  004a1a00  c1ff03                sar       edi,3
  004a1a03  03f8                  add       edi,eax
  004a1a05  897e20                mov       [esi+20h],edi
  004a1a08  eb20                  jmp       short 004A1A2Ah

LAB_004a1a0a:
  004a1a0a  8b6e20                mov       ebp,[esi+20h]
  004a1a0d  0fbfd1                movsx     edx,cx
  004a1a10  52                    push      edx
  004a1a11  e806400a00            call      00545A1Ch
  004a1a16  0500320000            add       eax,3200h
  004a1a1b  c1e006                shl       eax,6
  004a1a1e  03c7                  add       eax,edi
  004a1a20  2bc5                  sub       eax,ebp
  004a1a22  c1f804                sar       eax,4
  004a1a25  03c5                  add       eax,ebp
  004a1a27  894620                mov       [esi+20h],eax

LAB_004a1a2a:
  004a1a2a  66837e160a            cmp       word [esi+16h],0Ah
  004a1a2f  7c0d                  jl        short 004A1A3Eh
  004a1a31  8b442424              mov       eax,[esp+24h]
  004a1a35  50                    push      eax
  004a1a36  56                    push      esi
  004a1a37  e8341d0400            call      004E3770h
  004a1a3c  eb1d                  jmp       short 004A1A5Bh

LAB_004a1a3e:
  004a1a3e  8bbe90000000          mov       edi,[esi+90h]
  004a1a44  8b4c2424              mov       ecx,[esp+24h]
  004a1a48  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004a1a4e  51                    push      ecx
  004a1a4f  56                    push      esi
  004a1a50  89be90000000          mov       [esi+90h],edi
  004a1a56  e895020000            call      004A1CF0h

LAB_004a1a5b:
  004a1a5b  8b442424              mov       eax,[esp+24h]
  004a1a5f  83c408                add       esp,8
  004a1a62  85c0                  test      eax,eax
  004a1a64  0f8461020000          je        near 004A1CCBh
  004a1a6a  f70616080000          test      dword [esi],816h
  004a1a70  0f8555020000          jne       near 004A1CCBh
  004a1a76  8a4643                mov       al,[esi+43h]
  004a1a79  84c0                  test      al,al
  004a1a7b  0f854a020000          jne       near 004A1CCBh
  004a1a81  8b466c                mov       eax,[esi+6Ch]
  004a1a84  8b5e1c                mov       ebx,[esi+1Ch]
  004a1a87  8b4e24                mov       ecx,[esi+24h]
  004a1a8a  8b781c                mov       edi,[eax+1Ch]
  004a1a8d  8b6824                mov       ebp,[eax+24h]
  004a1a90  2bfb                  sub       edi,ebx
  004a1a92  2be9                  sub       ebp,ecx
  004a1a94  c1ff0c                sar       edi,0Ch
  004a1a97  c1fd0c                sar       ebp,0Ch
  004a1a9a  8bd7                  mov       edx,edi
  004a1a9c  8bc5                  mov       eax,ebp
  004a1a9e  0fafd7                imul      edx,edi
  004a1aa1  0fafc5                imul      eax,ebp
  004a1aa4  03d0                  add       edx,eax
  004a1aa6  52                    push      edx
  004a1aa7  e8643f0a00            call      00545A10h
  004a1aac  55                    push      ebp
  004a1aad  57                    push      edi
  004a1aae  89442420              mov       [esp+20h],eax
  004a1ab2  e86b3f0a00            call      00545A22h
  004a1ab7  668b4e32              mov       cx,[esi+32h]
  004a1abb  2bc1                  sub       eax,ecx
  004a1abd  25ff0f0000            and       eax,0FFFh
  004a1ac2  89442410              mov       [esp+10h],eax
  004a1ac6  8b442418              mov       eax,[esp+18h]
  004a1aca  3dfa000000            cmp       eax,0FAh
  004a1acf  0f8da3000000          jge       near 004A1B78h
  004a1ad5  e866560a00            call      00547140h
  004a1ada  99                    cdq
  004a1adb  b964000000            mov       ecx,64h
  004a1ae0  f7f9                  idiv      ecx
  004a1ae2  83fa3c                cmp       edx,3Ch
  004a1ae5  7d60                  jge       short 004A1B47h
  004a1ae7  8b542410              mov       edx,[esp+10h]
  004a1aeb  8b7c2414              mov       edi,[esp+14h]
  004a1aef  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a1af6  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a1afd  25ff0f0000            and       eax,0FFFh
  004a1b02  3bc1                  cmp       eax,ecx
  004a1b04  0f8f89010000          jg        near 004A1C93h
  004a1b0a  e831560a00            call      00547140h
  004a1b0f  99                    cdq
  004a1b10  8d4c3f01              lea       ecx,[edi+edi+1]
  004a1b14  f7f9                  idiv      ecx
  004a1b16  8b4c2418              mov       ecx,[esp+18h]
  004a1b1a  b82c010000            mov       eax,12Ch
  004a1b1f  2bc2                  sub       eax,edx
  004a1b21  3bc8                  cmp       ecx,eax
  004a1b23  0f8d6a010000          jge       near 004A1C93h
  004a1b29  8b1e                  mov       ebx,[esi]
  004a1b2b  8bcf                  mov       ecx,edi
  004a1b2d  c1f903                sar       ecx,3
  004a1b30  83cb02                or        ebx,2
  004a1b33  80c110                add       cl,10h
  004a1b36  891e                  mov       [esi],ebx
  004a1b38  c6464200              mov       byte [esi+42h],0
  004a1b3c  884e43                mov       [esi+43h],cl
  004a1b3f  5f                    pop       edi
  004a1b40  5e                    pop       esi
  004a1b41  5d                    pop       ebp
  004a1b42  5b                    pop       ebx
  004a1b43  83c42c                add       esp,2Ch
  004a1b46  c3                    ret

LAB_004a1b47:
  004a1b47  c6464203              mov       byte [esi+42h],3
  004a1b4b  e8f0550a00            call      00547140h
  004a1b50  99                    cdq
  004a1b51  b90a000000            mov       ecx,0Ah
  004a1b56  f7f9                  idiv      ecx
  004a1b58  8b442414              mov       eax,[esp+14h]
  004a1b5c  c1f802                sar       eax,2
  004a1b5f  02d0                  add       dl,al
  004a1b61  02d1                  add       dl,cl
  004a1b63  8b4c2440              mov       ecx,[esp+40h]
  004a1b67  885643                mov       [esi+43h],dl
  004a1b6a  8db980000000          lea       edi,[ecx+80h]
  004a1b70  c1e704                shl       edi,4
  004a1b73  e98b000000            jmp       004A1C03h

LAB_004a1b78:
  004a1b78  3df4010000            cmp       eax,1F4h
  004a1b7d  0f8dd2000000          jge       near 004A1C55h
  004a1b83  e8b8550a00            call      00547140h
  004a1b88  99                    cdq
  004a1b89  b964000000            mov       ecx,64h
  004a1b8e  f7f9                  idiv      ecx
  004a1b90  83fa3c                cmp       edx,3Ch
  004a1b93  0f8d8c000000          jge       near 004A1C25h
  004a1b99  8b542410              mov       edx,[esp+10h]
  004a1b9d  8b7c2414              mov       edi,[esp+14h]
  004a1ba1  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a1ba8  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a1baf  25ff0f0000            and       eax,0FFFh
  004a1bb4  3bc1                  cmp       eax,ecx
  004a1bb6  7f26                  jg        short 004A1BDEh
  004a1bb8  8b16                  mov       edx,[esi]
  004a1bba  c7461804000000        mov       dword [esi+18h],4
  004a1bc1  81e2fffffffe          and       edx,0FEFFFFFFh
  004a1bc7  c6464201              mov       byte [esi+42h],1
  004a1bcb  83ca02                or        edx,2
  004a1bce  66c746100100          mov       word [esi+10h],1
  004a1bd4  8916                  mov       [esi],edx
  004a1bd6  5f                    pop       edi
  004a1bd7  5e                    pop       esi
  004a1bd8  5d                    pop       ebp
  004a1bd9  5b                    pop       ebx
  004a1bda  83c42c                add       esp,2Ch
  004a1bdd  c3                    ret

LAB_004a1bde:
  004a1bde  c6464203              mov       byte [esi+42h],3
  004a1be2  e859550a00            call      00547140h
  004a1be7  99                    cdq
  004a1be8  b90a000000            mov       ecx,0Ah
  004a1bed  f7f9                  idiv      ecx
  004a1bef  8bc7                  mov       eax,edi
  004a1bf1  8b7c2440              mov       edi,[esp+40h]
  004a1bf5  c1f802                sar       eax,2
  004a1bf8  c1e704                shl       edi,4
  004a1bfb  02d0                  add       dl,al
  004a1bfd  02d1                  add       dl,cl
  004a1bff  47                    inc       edi
  004a1c00  885643                mov       [esi+43h],dl

LAB_004a1c03:
  004a1c03  e838550a00            call      00547140h
  004a1c08  99                    cdq
  004a1c09  f7ff                  idiv      edi
  004a1c0b  8b4c2410              mov       ecx,[esp+10h]
  004a1c0f  d1ff                  sar       edi,1
  004a1c11  2bd7                  sub       edx,edi
  004a1c13  03d1                  add       edx,ecx
  004a1c15  66035632              add       dx,[esi+32h]
  004a1c19  66895610              mov       [esi+10h],dx
  004a1c1d  5f                    pop       edi
  004a1c1e  5e                    pop       esi
  004a1c1f  5d                    pop       ebp
  004a1c20  5b                    pop       ebx
  004a1c21  83c42c                add       esp,2Ch
  004a1c24  c3                    ret

LAB_004a1c25:
  004a1c25  83fa50                cmp       edx,50h
  004a1c28  0f8d9d000000          jge       near 004A1CCBh
  004a1c2e  c6464202              mov       byte [esi+42h],2
  004a1c32  e809550a00            call      00547140h
  004a1c37  99                    cdq
  004a1c38  b90a000000            mov       ecx,0Ah
  004a1c3d  f7f9                  idiv      ecx
  004a1c3f  8b442414              mov       eax,[esp+14h]
  004a1c43  c1f802                sar       eax,2
  004a1c46  02d0                  add       dl,al
  004a1c48  02d1                  add       dl,cl
  004a1c4a  885643                mov       [esi+43h],dl
  004a1c4d  5f                    pop       edi
  004a1c4e  5e                    pop       esi
  004a1c4f  5d                    pop       ebp
  004a1c50  5b                    pop       ebx
  004a1c51  83c42c                add       esp,2Ch
  004a1c54  c3                    ret

LAB_004a1c55:
  004a1c55  e8e6540a00            call      00547140h
  004a1c5a  99                    cdq
  004a1c5b  b964000000            mov       ecx,64h
  004a1c60  f7f9                  idiv      ecx
  004a1c62  83fa1e                cmp       edx,1Eh
  004a1c65  7d27                  jge       short 004A1C8Eh
  004a1c67  c6464200              mov       byte [esi+42h],0
  004a1c6b  e8d0540a00            call      00547140h
  004a1c70  99                    cdq
  004a1c71  b90a000000            mov       ecx,0Ah
  004a1c76  f7f9                  idiv      ecx
  004a1c78  8b442414              mov       eax,[esp+14h]
  004a1c7c  c1f802                sar       eax,2
  004a1c7f  02d0                  add       dl,al
  004a1c81  02d1                  add       dl,cl
  004a1c83  885643                mov       [esi+43h],dl
  004a1c86  5f                    pop       edi
  004a1c87  5e                    pop       esi
  004a1c88  5d                    pop       ebp
  004a1c89  5b                    pop       ebx
  004a1c8a  83c42c                add       esp,2Ch
  004a1c8d  c3                    ret

LAB_004a1c8e:
  004a1c8e  83fa3c                cmp       edx,3Ch
  004a1c91  7d10                  jge       short 004A1CA3h

LAB_004a1c93:
  004a1c93  c6464201              mov       byte [esi+42h],1
  004a1c97  c6464301              mov       byte [esi+43h],1
  004a1c9b  5f                    pop       edi
  004a1c9c  5e                    pop       esi
  004a1c9d  5d                    pop       ebp
  004a1c9e  5b                    pop       ebx
  004a1c9f  83c42c                add       esp,2Ch
  004a1ca2  c3                    ret

LAB_004a1ca3:
  004a1ca3  c6464203              mov       byte [esi+42h],3
  004a1ca7  e894540a00            call      00547140h
  004a1cac  99                    cdq
  004a1cad  b90a000000            mov       ecx,0Ah
  004a1cb2  f7f9                  idiv      ecx
  004a1cb4  8b442414              mov       eax,[esp+14h]
  004a1cb8  c1f802                sar       eax,2
  004a1cbb  02d0                  add       dl,al
  004a1cbd  02d1                  add       dl,cl
  004a1cbf  885643                mov       [esi+43h],dl
  004a1cc2  e879540a00            call      00547140h
  004a1cc7  66894610              mov       [esi+10h],ax

LAB_004a1ccb:
  004a1ccb  5f                    pop       edi
  004a1ccc  5e                    pop       esi
  004a1ccd  5d                    pop       ebp
  004a1cce  5b                    pop       ebx
  004a1ccf  83c42c                add       esp,2Ch
  004a1cd2  c3                    ret
  004a1cd3  90                    nop
  004a1cd4  ce                    into
  004a1cd5  17                    pop       ss
  004a1cd6  4a                    dec       edx
  004a1cd7  001418                add       [eax+ebx],dl
  004a1cda  4a                    dec       edx
  004a1cdb  0087184a0010          add       [edi+10004A18h],al
  004a1ce1  194a00                sbb       [edx],ecx
  004a1ce4  90                    nop
  004a1ce5  90                    nop
  004a1ce6  90                    nop
  004a1ce7  90                    nop
  004a1ce8  90                    nop
  004a1ce9  90                    nop
  004a1cea  90                    nop
  004a1ceb  90                    nop
  004a1cec  90                    nop
  004a1ced  90                    nop
  004a1cee  90                    nop
  004a1cef  90                    nop
  004a1cf0  8b442404              mov       eax,[esp+4]
  004a1cf4  33d2                  xor       edx,edx
  004a1cf6  f7809000000082880000  test      dword [eax+90h],8882h
  004a1d00  7413                  je        short 004A1D15h
  004a1d02  f70016080000          test      dword [eax],816h
  004a1d08  7508                  jne       short 004A1D12h
  004a1d0a  c6404201              mov       byte [eax+42h],1
  004a1d0e  c6404301              mov       byte [eax+43h],1

LAB_004a1d12:
  004a1d12  89505c                mov       [eax+5Ch],edx

LAB_004a1d15:
  004a1d15  66399096000000        cmp       [eax+96h],dx
  004a1d1c  742a                  je        short 004A1D48h
  004a1d1e  8b4c2408              mov       ecx,[esp+8]
  004a1d22  f7411400000080        test      dword [ecx+14h],80000000h
  004a1d29  751d                  jne       short 004A1D48h
  004a1d2b  8b08                  mov       ecx,[eax]
  004a1d2d  f7c116080000          test      ecx,816h
  004a1d33  7513                  jne       short 004A1D48h
  004a1d35  f6c140                test      cl,40h
  004a1d38  7404                  je        short 004A1D3Eh
  004a1d3a  885043                mov       [eax+43h],dl
  004a1d3d  c3                    ret

LAB_004a1d3e:
  004a1d3e  885042                mov       [eax+42h],dl
  004a1d41  c6404301              mov       byte [eax+43h],1
  004a1d45  89505c                mov       [eax+5Ch],edx

LAB_004a1d48:
  004a1d48  c3                    ret
  004a1d49  90                    nop
  004a1d4a  90                    nop
  004a1d4b  90                    nop
  004a1d4c  90                    nop
  004a1d4d  90                    nop
  004a1d4e  90                    nop
  004a1d4f  90                    nop

56 cible(s) de saut interne : 0x004a1471, 0x004a14fc, 0x004a1500, 0x004a1522, 0x004a1539, 0x004a1574, 0x004a15a7, 0x004a15b7, 0x004a15bb, 0x004a15db, 0x004a1688, 0x004a16d7, 0x004a16e7, 0x004a170a, 0x004a171a, 0x004a171e, 0x004a176c, 0x004a17a7, 0x004a17e3, 0x004a17f6, 0x004a1802, 0x004a1829, 0x004a1861, 0x004a1871, 0x004a189c, 0x004a18af, 0x004a18bb, 0x004a18e1, 0x004a18f1, 0x004a18f5, 0x004a1922, 0x004a192e, 0x004a1994, 0x004a19a2, 0x004a19a4, 0x004a19a8, 0x004a19cd, 0x004a19f0, 0x004a1a0a, 0x004a1a2a, 0x004a1a3e, 0x004a1a5b, 0x004a1b47, 0x004a1b78, 0x004a1bde, 0x004a1c03, 0x004a1c25, 0x004a1c55, 0x004a1c8e, 0x004a1c93, 0x004a1ca3, 0x004a1ccb, 0x004a1d12, 0x004a1d15, 0x004a1d3e, 0x004a1d48
