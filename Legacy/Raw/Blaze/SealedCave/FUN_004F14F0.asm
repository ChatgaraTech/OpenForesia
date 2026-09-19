Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004f14f0  section .text  offset fichier 0xf08f0  3744 octets

  004f14f0  81ec84000000          sub       esp,84h
  004f14f6  53                    push      ebx
  004f14f7  55                    push      ebp
  004f14f8  56                    push      esi
  004f14f9  8bb42494000000        mov       esi,[esp+94h]
  004f1500  b864000000            mov       eax,64h
  004f1505  33db                  xor       ebx,ebx
  004f1507  6689442436            mov       [esp+36h],ax
  004f150c  668944243a            mov       [esp+3Ah],ax
  004f1511  8a4641                mov       al,[esi+41h]
  004f1514  57                    push      edi
  004f1515  84c0                  test      al,al
  004f1517  66895c2434            mov       [esp+34h],bx
  004f151c  66c74424361e00        mov       word [esp+36h],1Eh
  004f1523  66c74424383200        mov       word [esp+38h],32h
  004f152a  66895c243c            mov       [esp+3Ch],bx
  004f152f  7510                  jne       short 004F1541h
  004f1531  53                    push      ebx
  004f1532  6a05                  push      5
  004f1534  56                    push      esi
  004f1535  c6464101              mov       byte [esi+41h],1
  004f1539  e822e3ffff            call      004EF860h
  004f153e  83c40c                add       esp,0Ch

LAB_004f1541:
  004f1541  8b461c                mov       eax,[esi+1Ch]
  004f1544  8b4e20                mov       ecx,[esi+20h]
  004f1547  8b5624                mov       edx,[esi+24h]
  004f154a  8d6e1c                lea       ebp,[esi+1Ch]
  004f154d  89442428              mov       [esp+28h],eax
  004f1551  8b4664                mov       eax,[esi+64h]
  004f1554  894c242c              mov       [esp+2Ch],ecx
  004f1558  8b4e60                mov       ecx,[esi+60h]
  004f155b  89542430              mov       [esp+30h],edx
  004f155f  668b5632              mov       dx,[esi+32h]
  004f1563  8944244c              mov       [esp+4Ch],eax
  004f1567  8b467c                mov       eax,[esi+7Ch]
  004f156a  894c2444              mov       [esp+44h],ecx
  004f156e  668b8e90000000        mov       cx,[esi+90h]
  004f1575  6689942482000000      mov       [esp+82h],dx
  004f157d  8b9680000000          mov       edx,[esi+80h]
  004f1583  89442460              mov       [esp+60h],eax
  004f1587  8b8684000000          mov       eax,[esi+84h]
  004f158d  66894c245c            mov       [esp+5Ch],cx
  004f1592  8b8e88000000          mov       ecx,[esi+88h]
  004f1598  8b7e6c                mov       edi,[esi+6Ch]
  004f159b  89542440              mov       [esp+40h],edx
  004f159f  8b968c000000          mov       edx,[esi+8Ch]
  004f15a5  89442450              mov       [esp+50h],eax
  004f15a9  894c2458              mov       [esp+58h],ecx
  004f15ad  8d442424              lea       eax,[esp+24h]
  004f15b1  89542454              mov       [esp+54h],edx
  004f15b5  8d4c2464              lea       ecx,[esp+64h]
  004f15b9  50                    push      eax
  004f15ba  8d54246c              lea       edx,[esp+6Ch]
  004f15be  51                    push      ecx
  004f15bf  52                    push      edx
  004f15c0  56                    push      esi
  004f15c1  e8ba730400            call      00538980h
  004f15c6  83c410                add       esp,10h
  004f15c9  8d442448              lea       eax,[esp+48h]
  004f15cd  8d4c241c              lea       ecx,[esp+1Ch]
  004f15d1  8d542418              lea       edx,[esp+18h]
  004f15d5  50                    push      eax
  004f15d6  51                    push      ecx
  004f15d7  8d44241c              lea       eax,[esp+1Ch]
  004f15db  52                    push      edx
  004f15dc  8d4c241c              lea       ecx,[esp+1Ch]
  004f15e0  50                    push      eax
  004f15e1  51                    push      ecx
  004f15e2  56                    push      esi
  004f15e3  e838490400            call      00535F20h
  004f15e8  8b4c2428              mov       ecx,[esp+28h]
  004f15ec  8b542460              mov       edx,[esp+60h]
  004f15f0  83c418                add       esp,18h
  004f15f3  b840000000            mov       eax,40h
  004f15f8  2bc1                  sub       eax,ecx
  004f15fa  52                    push      edx
  004f15fb  56                    push      esi
  004f15fc  89442428              mov       [esp+28h],eax
  004f1600  e8db47feff            call      004D5DE0h
  004f1605  8b4500                mov       eax,[ebp]
  004f1608  8b571c                mov       edx,[edi+1Ch]
  004f160b  8b4e20                mov       ecx,[esi+20h]
  004f160e  2bc2                  sub       eax,edx
  004f1610  8b5720                mov       edx,[edi+20h]
  004f1613  83c408                add       esp,8
  004f1616  2bca                  sub       ecx,edx
  004f1618  8b5624                mov       edx,[esi+24h]
  004f161b  2b5724                sub       edx,[edi+24h]
  004f161e  c1f80c                sar       eax,0Ch
  004f1621  8bf8                  mov       edi,eax
  004f1623  c1f90c                sar       ecx,0Ch
  004f1626  0faff8                imul      edi,eax
  004f1629  8bc1                  mov       eax,ecx
  004f162b  c1fa0c                sar       edx,0Ch
  004f162e  0fafc1                imul      eax,ecx
  004f1631  8bca                  mov       ecx,edx
  004f1633  03f8                  add       edi,eax
  004f1635  0fafca                imul      ecx,edx
  004f1638  03f9                  add       edi,ecx
  004f163a  57                    push      edi
  004f163b  e8d0430500            call      00545A10h
  004f1640  89442414              mov       [esp+14h],eax
  004f1644  8b44241c              mov       eax,[esp+1Ch]
  004f1648  3bc3                  cmp       eax,ebx
  004f164a  0f845f070000          je        near 004F1DAFh
  004f1650  8b0e                  mov       ecx,[esi]
  004f1652  33d2                  xor       edx,edx
  004f1654  8a5604                mov       dl,[esi+4]
  004f1657  81e1fffffffd          and       ecx,0FDFFFFFFh
  004f165d  0fbf7e18              movsx     edi,word [esi+18h]
  004f1661  890e                  mov       [esi],ecx
  004f1663  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004f166a  f7c100000001          test      ecx,1000000h
  004f1670  8b14ba                mov       edx,[edx+edi*4]
  004f1673  741b                  je        short 004F1690h
  004f1675  66ff461a              inc       word [esi+1Ah]
  004f1679  668b4e1a              mov       cx,[esi+1Ah]
  004f167d  33ff                  xor       edi,edi
  004f167f  668b7a16              mov       di,[edx+16h]
  004f1683  0fbfc9                movsx     ecx,cx
  004f1686  3bcf                  cmp       ecx,edi
  004f1688  7c29                  jl        short 004F16B3h
  004f168a  66895e1a              mov       [esi+1Ah],bx
  004f168e  eb23                  jmp       short 004F16B3h

LAB_004f1690:
  004f1690  66ff461a              inc       word [esi+1Ah]
  004f1694  33ff                  xor       edi,edi
  004f1696  668b7a16              mov       di,[edx+16h]
  004f169a  0fbf561a              movsx     edx,word [esi+1Ah]
  004f169e  3bd7                  cmp       edx,edi
  004f16a0  7c11                  jl        short 004F16B3h
  004f16a2  668b561a              mov       dx,[esi+1Ah]
  004f16a6  4a                    dec       edx
  004f16a7  81c900000002          or        ecx,2000000h
  004f16ad  6689561a              mov       [esi+1Ah],dx
  004f16b1  890e                  mov       [esi],ecx

LAB_004f16b3:
  004f16b3  8a4e42                mov       cl,[esi+42h]
  004f16b6  8b16                  mov       edx,[esi]
  004f16b8  80f905                cmp       cl,5
  004f16bb  7505                  jne       short 004F16C2h
  004f16bd  83ca10                or        edx,10h
  004f16c0  eb03                  jmp       short 004F16C5h

LAB_004f16c2:
  004f16c2  83e2ef                and       edx,0FFFFFFEFh

LAB_004f16c5:
  004f16c5  8916                  mov       [esi],edx
  004f16c7  8a16                  mov       dl,[esi]
  004f16c9  f6c202                test      dl,2
  004f16cc  0f8460010000          je        near 004F1832h
  004f16d2  80f90b                cmp       cl,0Bh
  004f16d5  0f8582000000          jne       near 004F175Dh
  004f16db  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004f16e0  757b                  jne       short 004F175Dh
  004f16e2  8d442434              lea       eax,[esp+34h]
  004f16e6  53                    push      ebx
  004f16e7  50                    push      eax
  004f16e8  55                    push      ebp
  004f16e9  56                    push      esi
  004f16ea  e841850200            call      00519C30h
  004f16ef  8bf8                  mov       edi,eax
  004f16f1  a1b8e05d00            mov       eax,[5DE0B8h]
  004f16f6  83c410                add       esp,10h
  004f16f9  3bc3                  cmp       eax,ebx
  004f16fb  745c                  je        short 004F1759h
  004f16fd  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004f1702  7555                  jne       short 004F1759h
  004f1704  8b07                  mov       eax,[edi]
  004f1706  57                    push      edi
  004f1707  0c40                  or        al,40h
  004f1709  56                    push      esi
  004f170a  66c787960000001000    mov       word [edi+96h],10h
  004f1713  8907                  mov       [edi],eax
  004f1715  e866840400            call      00539B80h
  004f171a  83c408                add       esp,8
  004f171d  8d4f1c                lea       ecx,[edi+1Ch]
  004f1720  51                    push      ecx
  004f1721  6800180000            push      1800h
  004f1726  6800030000            push      300h
  004f172b  6a03                  push      3
  004f172d  e8eec30400            call      0053DB20h
  004f1732  83c410                add       esp,10h
  004f1735  66837e1601            cmp       word [esi+16h],1
  004f173a  751d                  jne       short 004F1759h
  004f173c  e8ff590500            call      00547140h
  004f1741  99                    cdq
  004f1742  b90f000000            mov       ecx,0Fh
  004f1747  f7f9                  idiv      ecx
  004f1749  83fa01                cmp       edx,1
  004f174c  750b                  jne       short 004F1759h
  004f174e  53                    push      ebx
  004f174f  57                    push      edi
  004f1750  56                    push      esi
  004f1751  e83a0c0000            call      004F2390h
  004f1756  83c40c                add       esp,0Ch

LAB_004f1759:
  004f1759  8b44241c              mov       eax,[esp+1Ch]

LAB_004f175d:
  004f175d  807e4208              cmp       byte [esi+42h],8
  004f1761  0f85cb000000          jne       near 004F1832h
  004f1767  8d542434              lea       edx,[esp+34h]
  004f176b  53                    push      ebx
  004f176c  52                    push      edx
  004f176d  55                    push      ebp
  004f176e  56                    push      esi
  004f176f  e8bc840200            call      00519C30h
  004f1774  8bf8                  mov       edi,eax
  004f1776  a1b8e05d00            mov       eax,[5DE0B8h]
  004f177b  83c410                add       esp,10h
  004f177e  3bc3                  cmp       eax,ebx
  004f1780  0f84a8000000          je        near 004F182Eh
  004f1786  8b1f                  mov       ebx,[edi]
  004f1788  66c787960000001000    mov       word [edi+96h],10h
  004f1791  83cb40                or        ebx,40h
  004f1794  57                    push      edi
  004f1795  891f                  mov       [edi],ebx
  004f1797  8b4c2428              mov       ecx,[esp+28h]
  004f179b  56                    push      esi
  004f179c  668b4130              mov       ax,[ecx+30h]
  004f17a0  0fbfd8                movsx     ebx,ax
  004f17a3  66c1f802              sar       ax,2
  004f17a7  66894130              mov       [ecx+30h],ax
  004f17ab  e8d0830400            call      00539B80h
  004f17b0  8b44242c              mov       eax,[esp+2Ch]
  004f17b4  83c408                add       esp,8
  004f17b7  8d4f1c                lea       ecx,[edi+1Ch]
  004f17ba  51                    push      ecx
  004f17bb  6800180000            push      1800h
  004f17c0  6800030000            push      300h
  004f17c5  6a03                  push      3
  004f17c7  66895830              mov       [eax+30h],bx
  004f17cb  e850c30400            call      0053DB20h
  004f17d0  8b07                  mov       eax,[edi]
  004f17d2  83c410                add       esp,10h
  004f17d5  a900000040            test      eax,40000000h
  004f17da  7550                  jne       short 004F182Ch
  004f17dc  6aff                  push      0FFFFFFFFh
  004f17de  6a00                  push      0
  004f17e0  6a39                  push      39h
  004f17e2  56                    push      esi
  004f17e3  e8e891f8ff            call      0047A9D0h
  004f17e8  8b07                  mov       eax,[edi]
  004f17ea  83c410                add       esp,10h
  004f17ed  0c01                  or        al,1
  004f17ef  8907                  mov       [edi],eax
  004f17f1  0fbf5632              movsx     edx,word [esi+32h]
  004f17f5  52                    push      edx
  004f17f6  e821420500            call      00545A1Ch
  004f17fb  8b4e5c                mov       ecx,[esi+5Ch]
  004f17fe  c1f90b                sar       ecx,0Bh
  004f1801  0fafc1                imul      eax,ecx
  004f1804  894750                mov       [edi+50h],eax
  004f1807  0fbf5632              movsx     edx,word [esi+32h]
  004f180b  52                    push      edx
  004f180c  e805420500            call      00545A16h
  004f1811  8b4e5c                mov       ecx,[esi+5Ch]
  004f1814  c7475400800100        mov       dword [edi+54h],18000h
  004f181b  c1f90b                sar       ecx,0Bh
  004f181e  0fafc1                imul      eax,ecx
  004f1821  894758                mov       [edi+58h],eax
  004f1824  8b44241c              mov       eax,[esp+1Ch]
  004f1828  33db                  xor       ebx,ebx
  004f182a  eb06                  jmp       short 004F1832h

LAB_004f182c:
  004f182c  33db                  xor       ebx,ebx

LAB_004f182e:
  004f182e  8b44241c              mov       eax,[esp+1Ch]

LAB_004f1832:
  004f1832  33c9                  xor       ecx,ecx
  004f1834  8a4e42                mov       cl,[esi+42h]
  004f1837  83f90c                cmp       ecx,0Ch
  004f183a  0f876f050000          ja        near 004F1DAFh
  004f1840  ff248d58234f00        jmp       dword [ecx*4+4F2358h]
  004f1847  66395e12              cmp       [esi+12h],bx
  004f184b  751a                  jne       short 004F1867h
  004f184d  8b06                  mov       eax,[esi]
  004f184f  66895e18              mov       [esi+18h],bx
  004f1853  0d00000001            or        eax,1000000h
  004f1858  66895e1a              mov       [esi+1Ah],bx
  004f185c  8906                  mov       [esi],eax
  004f185e  66c746120100          mov       word [esi+12h],1
  004f1864  895e5c                mov       [esi+5Ch],ebx

LAB_004f1867:
  004f1867  0fbf5610              movsx     edx,word [esi+10h]
  004f186b  39542414              cmp       [esp+14h],edx
  004f186f  7d09                  jge       short 004F187Ah
  004f1871  c6464300              mov       byte [esi+43h],0
  004f1875  e935050000            jmp       004F1DAFh

LAB_004f187a:
  004f187a  8a4643                mov       al,[esi+43h]
  004f187d  84c0                  test      al,al
  004f187f  0f84e5000000          je        near 004F196Ah
  004f1885  fec8                  dec       al
  004f1887  895e5c                mov       [esi+5Ch],ebx
  004f188a  884643                mov       [esi+43h],al
  004f188d  e91d050000            jmp       004F1DAFh
  004f1892  66395e12              cmp       [esi+12h],bx
  004f1896  7535                  jne       short 004F18CDh
  004f1898  8b0e                  mov       ecx,[esi]
  004f189a  b801000000            mov       eax,1
  004f189f  81c900000001          or        ecx,1000000h
  004f18a5  66894618              mov       [esi+18h],ax
  004f18a9  66895e1a              mov       [esi+1Ah],bx
  004f18ad  890e                  mov       [esi],ecx
  004f18af  66894612              mov       [esi+12h],ax
  004f18b3  c7465c00500000        mov       dword [esi+5Ch],5000h
  004f18ba  e881580500            call      00547140h
  004f18bf  99                    cdq
  004f18c0  b91e000000            mov       ecx,1Eh
  004f18c5  f7f9                  idiv      ecx
  004f18c7  80c214                add       dl,14h
  004f18ca  885643                mov       [esi+43h],dl

LAB_004f18cd:
  004f18cd  8b442418              mov       eax,[esp+18h]
  004f18d1  8a5e43                mov       bl,[esi+43h]
  004f18d4  fecb                  dec       bl
  004f18d6  8d90c8000000          lea       edx,[eax+0C8h]
  004f18dc  885e43                mov       [esi+43h],bl
  004f18df  81e2ff0f0000          and       edx,0FFFh
  004f18e5  81fa90010000          cmp       edx,190h
  004f18eb  7f09                  jg        short 004F18F6h
  004f18ed  c6464300              mov       byte [esi+43h],0
  004f18f1  e9b9040000            jmp       004F1DAFh

LAB_004f18f6:
  004f18f6  25ff0f0000            and       eax,0FFFh
  004f18fb  3d00080000            cmp       eax,800h
  004f1900  7d0c                  jge       short 004F190Eh
  004f1902  8b442410              mov       eax,[esp+10h]
  004f1906  83c032                add       eax,32h
  004f1909  e99d040000            jmp       004F1DABh

LAB_004f190e:
  004f190e  8b4c2410              mov       ecx,[esp+10h]
  004f1912  b8ceffffff            mov       eax,0FFFFFFCEh
  004f1917  2bc1                  sub       eax,ecx
  004f1919  e98d040000            jmp       004F1DABh
  004f191e  66395e12              cmp       [esi+12h],bx
  004f1922  8b4c2410              mov       ecx,[esp+10h]
  004f1926  b801000000            mov       eax,1
  004f192b  7525                  jne       short 004F1952h
  004f192d  8b16                  mov       edx,[esi]
  004f192f  66894618              mov       [esi+18h],ax
  004f1933  81ca00000001          or        edx,1000000h
  004f1939  66895e1a              mov       [esi+1Ah],bx
  004f193d  8916                  mov       [esi],edx
  004f193f  8d14c9                lea       edx,[ecx+ecx*8]
  004f1942  c1e204                shl       edx,4
  004f1945  81c200500000          add       edx,5000h
  004f194b  66894612              mov       [esi+12h],ax
  004f194f  89565c                mov       [esi+5Ch],edx

LAB_004f1952:
  004f1952  66394612              cmp       [esi+12h],ax
  004f1956  0f8553040000          jne       near 004F1DAFh
  004f195c  0fbf4610              movsx     eax,word [esi+10h]
  004f1960  39442414              cmp       [esp+14h],eax
  004f1964  7d0c                  jge       short 004F1972h
  004f1966  c6464300              mov       byte [esi+43h],0

LAB_004f196a:
  004f196a  895e5c                mov       [esi+5Ch],ebx
  004f196d  e93d040000            jmp       004F1DAFh

LAB_004f1972:
  004f1972  8a4643                mov       al,[esi+43h]
  004f1975  84c0                  test      al,al
  004f1977  7405                  je        short 004F197Eh
  004f1979  fec8                  dec       al
  004f197b  884643                mov       [esi+43h],al

LAB_004f197e:
  004f197e  8b442418              mov       eax,[esp+18h]
  004f1982  8d9000010000          lea       edx,[eax+100h]
  004f1988  81e2ff0f0000          and       edx,0FFFh
  004f198e  81fa00020000          cmp       edx,200h
  004f1994  0f8e15040000          jle       near 004F1DAFh
  004f199a  25ff0f0000            and       eax,0FFFh
  004f199f  3d00080000            cmp       eax,800h
  004f19a4  7d0c                  jge       short 004F19B2h
  004f19a6  8d840980000000        lea       eax,[ecx+ecx+80h]
  004f19ad  e9f9030000            jmp       004F1DABh

LAB_004f19b2:
  004f19b2  03c9                  add       ecx,ecx
  004f19b4  b880ffffff            mov       eax,0FFFFFF80h
  004f19b9  2bc1                  sub       eax,ecx
  004f19bb  e9eb030000            jmp       004F1DABh
  004f19c0  66837e1201            cmp       word [esi+12h],1
  004f19c5  0f8596000000          jne       near 004F1A61h
  004f19cb  8a4e43                mov       cl,[esi+43h]
  004f19ce  84c9                  test      cl,cl
  004f19d0  7405                  je        short 004F19D7h
  004f19d2  fec9                  dec       cl
  004f19d4  884e43                mov       [esi+43h],cl

LAB_004f19d7:
  004f19d7  8b4c2410              mov       ecx,[esp+10h]
  004f19db  8d14c9                lea       edx,[ecx+ecx*8]
  004f19de  c1e204                shl       edx,4
  004f19e1  8d8a00500000          lea       ecx,[edx+5000h]
  004f19e7  0fafc1                imul      eax,ecx
  004f19ea  c1f80c                sar       eax,0Ch
  004f19ed  8bf8                  mov       edi,eax
  004f19ef  894e5c                mov       [esi+5Ch],ecx
  004f19f2  0fbf4610              movsx     eax,word [esi+10h]
  004f19f6  50                    push      eax
  004f19f7  e820400500            call      00545A1Ch
  004f19fc  0fafc7                imul      eax,edi
  004f19ff  8b4d00                mov       ecx,[ebp]
  004f1a02  c1f80c                sar       eax,0Ch
  004f1a05  03c8                  add       ecx,eax
  004f1a07  894d00                mov       [ebp],ecx
  004f1a0a  0fbf4e10              movsx     ecx,word [esi+10h]
  004f1a0e  51                    push      ecx
  004f1a0f  e802400500            call      00545A16h
  004f1a14  0fafc7                imul      eax,edi
  004f1a17  8b6e24                mov       ebp,[esi+24h]
  004f1a1a  895e5c                mov       [esi+5Ch],ebx
  004f1a1d  c1f80c                sar       eax,0Ch
  004f1a20  03e8                  add       ebp,eax
  004f1a22  8b442418              mov       eax,[esp+18h]
  004f1a26  896e24                mov       [esi+24h],ebp
  004f1a29  8d9000010000          lea       edx,[eax+100h]
  004f1a2f  81e2ff0f0000          and       edx,0FFFh
  004f1a35  81fa00020000          cmp       edx,200h
  004f1a3b  7e24                  jle       short 004F1A61h
  004f1a3d  25ff0f0000            and       eax,0FFFh
  004f1a42  3d00080000            cmp       eax,800h
  004f1a47  7d09                  jge       short 004F1A52h
  004f1a49  8b442410              mov       eax,[esp+10h]
  004f1a4d  83c032                add       eax,32h
  004f1a50  eb0b                  jmp       short 004F1A5Dh

LAB_004f1a52:
  004f1a52  8b4c2410              mov       ecx,[esp+10h]
  004f1a56  b8ceffffff            mov       eax,0FFFFFFCEh
  004f1a5b  2bc1                  sub       eax,ecx

LAB_004f1a5d:
  004f1a5d  66014632              add       [esi+32h],ax

LAB_004f1a61:
  004f1a61  66395e12              cmp       [esi+12h],bx
  004f1a65  0f8544030000          jne       near 004F1DAFh
  004f1a6b  8b0e                  mov       ecx,[esi]
  004f1a6d  b801000000            mov       eax,1
  004f1a72  81c900000001          or        ecx,1000000h
  004f1a78  66894618              mov       [esi+18h],ax
  004f1a7c  66895e1a              mov       [esi+1Ah],bx
  004f1a80  890e                  mov       [esi],ecx
  004f1a82  66894612              mov       [esi+12h],ax
  004f1a86  e924030000            jmp       004F1DAFh
  004f1a8b  66395e12              cmp       [esi+12h],bx
  004f1a8f  0f8580000000          jne       near 004F1B15h
  004f1a95  8b0e                  mov       ecx,[esi]
  004f1a97  f6c101                test      cl,1
  004f1a9a  7406                  je        short 004F1AA2h
  004f1a9c  c6464300              mov       byte [esi+43h],0
  004f1aa0  eb73                  jmp       short 004F1B15h

LAB_004f1aa2:
  004f1aa2  81e1fffffffe          and       ecx,0FEFFFFFFh
  004f1aa8  bd01000000            mov       ebp,1
  004f1aad  890e                  mov       [esi],ecx
  004f1aaf  8b4c2410              mov       ecx,[esp+10h]
  004f1ab3  81c100010000          add       ecx,100h
  004f1ab9  66896e18              mov       [esi+18h],bp
  004f1abd  c1e106                shl       ecx,6
  004f1ac0  0fafc1                imul      eax,ecx
  004f1ac3  0fbf5610              movsx     edx,word [esi+10h]
  004f1ac7  c1f80c                sar       eax,0Ch
  004f1aca  52                    push      edx
  004f1acb  66895e1a              mov       [esi+1Ah],bx
  004f1acf  894e5c                mov       [esi+5Ch],ecx
  004f1ad2  8bf8                  mov       edi,eax
  004f1ad4  e8433f0500            call      00545A1Ch
  004f1ad9  0fafc7                imul      eax,edi
  004f1adc  8b4e50                mov       ecx,[esi+50h]
  004f1adf  c1f80c                sar       eax,0Ch
  004f1ae2  03c8                  add       ecx,eax
  004f1ae4  0fbf4610              movsx     eax,word [esi+10h]
  004f1ae8  50                    push      eax
  004f1ae9  894e50                mov       [esi+50h],ecx
  004f1aec  e8253f0500            call      00545A16h
  004f1af1  0fafc7                imul      eax,edi
  004f1af4  8b5658                mov       edx,[esi+58h]
  004f1af7  8b4e54                mov       ecx,[esi+54h]
  004f1afa  c1f80c                sar       eax,0Ch
  004f1afd  03d0                  add       edx,eax
  004f1aff  8b06                  mov       eax,[esi]
  004f1b01  81c100e00100          add       ecx,1E000h
  004f1b07  0bc5                  or        eax,ebp
  004f1b09  895658                mov       [esi+58h],edx
  004f1b0c  894e54                mov       [esi+54h],ecx
  004f1b0f  8906                  mov       [esi],eax
  004f1b11  66896e12              mov       [esi+12h],bp

LAB_004f1b15:
  004f1b15  f60601                test      byte [esi],1
  004f1b18  0f8591020000          jne       near 004F1DAFh
  004f1b1e  c6464300              mov       byte [esi+43h],0
  004f1b22  e988020000            jmp       004F1DAFh
  004f1b27  668b4e12              mov       cx,[esi+12h]
  004f1b2b  ba01000000            mov       edx,1
  004f1b30  663bca                cmp       cx,dx
  004f1b33  751c                  jne       short 004F1B51h
  004f1b35  0fbf4610              movsx     eax,word [esi+10h]
  004f1b39  39442414              cmp       [esp+14h],eax
  004f1b3d  7d06                  jge       short 004F1B45h
  004f1b3f  c6464300              mov       byte [esi+43h],0
  004f1b43  eb0c                  jmp       short 004F1B51h

LAB_004f1b45:
  004f1b45  8a4643                mov       al,[esi+43h]
  004f1b48  84c0                  test      al,al
  004f1b4a  7405                  je        short 004F1B51h
  004f1b4c  fec8                  dec       al
  004f1b4e  884643                mov       [esi+43h],al

LAB_004f1b51:
  004f1b51  663bcb                cmp       cx,bx
  004f1b54  0f8555020000          jne       near 004F1DAFh
  004f1b5a  8b442410              mov       eax,[esp+10h]
  004f1b5e  8b3e                  mov       edi,[esi]
  004f1b60  81cf00000001          or        edi,1000000h
  004f1b66  66895618              mov       [esi+18h],dx
  004f1b6a  8d0cc0                lea       ecx,[eax+eax*8]
  004f1b6d  66895e1a              mov       [esi+1Ah],bx
  004f1b71  c1e104                shl       ecx,4
  004f1b74  81c100500000          add       ecx,5000h
  004f1b7a  893e                  mov       [esi],edi
  004f1b7c  66895612              mov       [esi+12h],dx
  004f1b80  894e5c                mov       [esi+5Ch],ecx
  004f1b83  e927020000            jmp       004F1DAFh
  004f1b88  66395e12              cmp       [esi+12h],bx
  004f1b8c  bf01000000            mov       edi,1
  004f1b91  7539                  jne       short 004F1BCCh
  004f1b93  8b542410              mov       edx,[esp+10h]
  004f1b97  8b0e                  mov       ecx,[esi]
  004f1b99  83e2fc                and       edx,0FFFFFFFCh
  004f1b9c  81c900000001          or        ecx,1000000h
  004f1ba2  83c250                add       edx,50h
  004f1ba5  66897e12              mov       [esi+12h],di
  004f1ba9  c1e20a                shl       edx,0Ah
  004f1bac  89565c                mov       [esi+5Ch],edx
  004f1baf  66897e18              mov       [esi+18h],di
  004f1bb3  66895e1a              mov       [esi+1Ah],bx
  004f1bb7  890e                  mov       [esi],ecx
  004f1bb9  e882550500            call      00547140h
  004f1bbe  99                    cdq
  004f1bbf  b90a000000            mov       ecx,0Ah
  004f1bc4  f7f9                  idiv      ecx
  004f1bc6  80c21e                add       dl,1Eh
  004f1bc9  885643                mov       [esi+43h],dl

LAB_004f1bcc:
  004f1bcc  66397e12              cmp       [esi+12h],di
  004f1bd0  7554                  jne       short 004F1C26h
  004f1bd2  8a4643                mov       al,[esi+43h]
  004f1bd5  3c14                  cmp       al,14h
  004f1bd7  763e                  jbe       short 004F1C17h
  004f1bd9  8b4c2418              mov       ecx,[esp+18h]
  004f1bdd  8d9100010000          lea       edx,[ecx+100h]
  004f1be3  81e2ff0f0000          and       edx,0FFFh
  004f1be9  81fa00020000          cmp       edx,200h
  004f1bef  7e26                  jle       short 004F1C17h
  004f1bf1  81e1ff0f0000          and       ecx,0FFFh
  004f1bf7  81f900080000          cmp       ecx,800h
  004f1bfd  7d09                  jge       short 004F1C08h
  004f1bff  8b4c2410              mov       ecx,[esp+10h]
  004f1c03  83c132                add       ecx,32h
  004f1c06  eb0b                  jmp       short 004F1C13h

LAB_004f1c08:
  004f1c08  8b542410              mov       edx,[esp+10h]
  004f1c0c  b9ceffffff            mov       ecx,0FFFFFFCEh
  004f1c11  2bca                  sub       ecx,edx

LAB_004f1c13:
  004f1c13  66014e32              add       [esi+32h],cx

LAB_004f1c17:
  004f1c17  fec8                  dec       al
  004f1c19  3c01                  cmp       al,1
  004f1c1b  884643                mov       [esi+43h],al
  004f1c1e  7506                  jne       short 004F1C26h
  004f1c20  66c746120200          mov       word [esi+12h],2

LAB_004f1c26:
  004f1c26  66837e1202            cmp       word [esi+12h],2
  004f1c2b  0f857e010000          jne       near 004F1DAFh
  004f1c31  8b465c                mov       eax,[esi+5Ch]
  004f1c34  8bd0                  mov       edx,eax
  004f1c36  f7da                  neg       edx
  004f1c38  c1fa03                sar       edx,3
  004f1c3b  03c2                  add       eax,edx
  004f1c3d  3d00500000            cmp       eax,5000h
  004f1c42  89465c                mov       [esi+5Ch],eax
  004f1c45  0f8d64010000          jge       near 004F1DAFh
  004f1c4b  c6464300              mov       byte [esi+43h],0
  004f1c4f  e95b010000            jmp       004F1DAFh
  004f1c54  66395e12              cmp       [esi+12h],bx
  004f1c58  7542                  jne       short 004F1C9Ch
  004f1c5a  e8e1540500            call      00547140h
  004f1c5f  99                    cdq
  004f1c60  b90a000000            mov       ecx,0Ah
  004f1c65  f7f9                  idiv      ecx
  004f1c67  83ea05                sub       edx,5
  004f1c6a  8d1492                lea       edx,[edx+edx*4]
  004f1c6d  d1e2                  shl       edx,1
  004f1c6f  66895610              mov       [esi+10h],dx
  004f1c73  e8c8540500            call      00547140h
  004f1c78  99                    cdq
  004f1c79  b90a000000            mov       ecx,0Ah
  004f1c7e  66c746120100          mov       word [esi+12h],1
  004f1c84  f7f9                  idiv      ecx
  004f1c86  80c214                add       dl,14h
  004f1c89  885643                mov       [esi+43h],dl
  004f1c8c  8b542410              mov       edx,[esp+10h]
  004f1c90  83e2fc                and       edx,0FFFFFFFCh
  004f1c93  83c214                add       edx,14h
  004f1c96  c1e20a                shl       edx,0Ah
  004f1c99  89565c                mov       [esi+5Ch],edx

LAB_004f1c9c:
  004f1c9c  66837e1201            cmp       word [esi+12h],1
  004f1ca1  0f8508010000          jne       near 004F1DAFh
  004f1ca7  668b4610              mov       ax,[esi+10h]
  004f1cab  66014632              add       [esi+32h],ax
  004f1caf  8a4643                mov       al,[esi+43h]
  004f1cb2  fec8                  dec       al
  004f1cb4  884643                mov       [esi+43h],al
  004f1cb7  e9f3000000            jmp       004F1DAFh
  004f1cbc  66395e12              cmp       [esi+12h],bx
  004f1cc0  7520                  jne       short 004F1CE2h
  004f1cc2  8b0e                  mov       ecx,[esi]
  004f1cc4  66c746120100          mov       word [esi+12h],1
  004f1cca  81e1fffffffe          and       ecx,0FEFFFFFFh
  004f1cd0  66c746180300          mov       word [esi+18h],3
  004f1cd6  83c902                or        ecx,2
  004f1cd9  66895e1a              mov       [esi+1Ah],bx
  004f1cdd  895e5c                mov       [esi+5Ch],ebx
  004f1ce0  890e                  mov       [esi],ecx

LAB_004f1ce2:
  004f1ce2  66837e1201            cmp       word [esi+12h],1
  004f1ce7  0f85c2000000          jne       near 004F1DAFh
  004f1ced  8b06                  mov       eax,[esi]
  004f1cef  a900000002            test      eax,2000000h
  004f1cf4  0f84b5000000          je        near 004F1DAFh
  004f1cfa  24fd                  and       al,0FDh
  004f1cfc  c6464300              mov       byte [esi+43h],0
  004f1d00  8906                  mov       [esi],eax
  004f1d02  e9a8000000            jmp       004F1DAFh
  004f1d07  66395e12              cmp       [esi+12h],bx
  004f1d0b  754f                  jne       short 004F1D5Ch
  004f1d0d  a13c2f5500            mov       eax,[552F3Ch]
  004f1d12  8b2e                  mov       ebp,[esi]
  004f1d14  25ffff0000            and       eax,0FFFFh
  004f1d19  6aff                  push      0FFFFFFFFh
  004f1d1b  66894606              mov       [esi+6],ax
  004f1d1f  25ffff0000            and       eax,0FFFFh
  004f1d24  8bd0                  mov       edx,eax
  004f1d26  6aff                  push      0FFFFFFFFh
  004f1d28  c1f808                sar       eax,8
  004f1d2b  81e2ff000000          and       edx,0FFh
  004f1d31  25ff000000            and       eax,0FFh
  004f1d36  52                    push      edx
  004f1d37  81e5fffffffe          and       ebp,0FEFFFFFFh
  004f1d3d  50                    push      eax
  004f1d3e  56                    push      esi
  004f1d3f  895e5c                mov       [esi+5Ch],ebx
  004f1d42  66c746180400          mov       word [esi+18h],4
  004f1d48  66895e1a              mov       [esi+1Ah],bx
  004f1d4c  892e                  mov       [esi],ebp
  004f1d4e  66c746120100          mov       word [esi+12h],1
  004f1d54  e8278c0400            call      0053A980h
  004f1d59  83c414                add       esp,14h

LAB_004f1d5c:
  004f1d5c  66837e1201            cmp       word [esi+12h],1
  004f1d61  754c                  jne       short 004F1DAFh
  004f1d63  f70600000002          test      dword [esi],2000000h
  004f1d69  7404                  je        short 004F1D6Fh
  004f1d6b  c6464300              mov       byte [esi+43h],0

LAB_004f1d6f:
  004f1d6f  8b442418              mov       eax,[esp+18h]
  004f1d73  8d88c8000000          lea       ecx,[eax+0C8h]
  004f1d79  81e1ff0f0000          and       ecx,0FFFh
  004f1d7f  81f990010000          cmp       ecx,190h
  004f1d85  7e28                  jle       short 004F1DAFh
  004f1d87  25ff0f0000            and       eax,0FFFh
  004f1d8c  3d00080000            cmp       eax,800h
  004f1d91  7d0b                  jge       short 004F1D9Eh
  004f1d93  8b442410              mov       eax,[esp+10h]
  004f1d97  d1f8                  sar       eax,1
  004f1d99  83c032                add       eax,32h
  004f1d9c  eb0d                  jmp       short 004F1DABh

LAB_004f1d9e:
  004f1d9e  8b542410              mov       edx,[esp+10h]
  004f1da2  b8ceffffff            mov       eax,0FFFFFFCEh
  004f1da7  d1fa                  sar       edx,1
  004f1da9  2bc2                  sub       eax,edx

LAB_004f1dab:
  004f1dab  66014632              add       [esi+32h],ax

LAB_004f1daf:
  004f1daf  0fbf4632              movsx     eax,word [esi+32h]
  004f1db3  50                    push      eax
  004f1db4  e8633c0500            call      00545A1Ch
  004f1db9  0fbf4e32              movsx     ecx,word [esi+32h]
  004f1dbd  51                    push      ecx
  004f1dbe  89442470              mov       [esp+70h],eax
  004f1dc2  e84f3c0500            call      00545A16h
  004f1dc7  8b5650                mov       edx,[esi+50h]
  004f1dca  89442474              mov       [esp+74h],eax
  004f1dce  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  004f1dd5  8bc2                  mov       eax,edx
  004f1dd7  8bfa                  mov       edi,edx
  004f1dd9  c1f808                sar       eax,8
  004f1ddc  8b4c241c              mov       ecx,[esp+1Ch]
  004f1de0  2bf8                  sub       edi,eax
  004f1de2  c1fa03                sar       edx,3
  004f1de5  0faf4e5c              imul      ecx,[esi+5Ch]
  004f1de9  2bfa                  sub       edi,edx
  004f1deb  8b5654                mov       edx,[esi+54h]
  004f1dee  8bc5                  mov       eax,ebp
  004f1df0  8bda                  mov       ebx,edx
  004f1df2  c1f808                sar       eax,8
  004f1df5  c1fb08                sar       ebx,8
  004f1df8  2bc3                  sub       eax,ebx
  004f1dfa  897e50                mov       [esi+50h],edi
  004f1dfd  2bc5                  sub       eax,ebp
  004f1dff  56                    push      esi
  004f1e00  03c2                  add       eax,edx
  004f1e02  8b5658                mov       edx,[esi+58h]
  004f1e05  8bda                  mov       ebx,edx
  004f1e07  8bea                  mov       ebp,edx
  004f1e09  c1fb03                sar       ebx,3
  004f1e0c  2beb                  sub       ebp,ebx
  004f1e0e  8b5e1c                mov       ebx,[esi+1Ch]
  004f1e11  c1fa08                sar       edx,8
  004f1e14  c1f90c                sar       ecx,0Ch
  004f1e17  2bea                  sub       ebp,edx
  004f1e19  8bd1                  mov       edx,ecx
  004f1e1b  0faf542470            imul      edx,[esp+70h]
  004f1e20  0faf4c2478            imul      ecx,[esp+78h]
  004f1e25  c1fa0c                sar       edx,0Ch
  004f1e28  c1f90c                sar       ecx,0Ch
  004f1e2b  896e58                mov       [esi+58h],ebp
  004f1e2e  03d7                  add       edx,edi
  004f1e30  8b7e20                mov       edi,[esi+20h]
  004f1e33  03cd                  add       ecx,ebp
  004f1e35  8b6e24                mov       ebp,[esi+24h]
  004f1e38  03da                  add       ebx,edx
  004f1e3a  03f8                  add       edi,eax
  004f1e3c  03e9                  add       ebp,ecx
  004f1e3e  894654                mov       [esi+54h],eax
  004f1e41  895e1c                mov       [esi+1Ch],ebx
  004f1e44  897e20                mov       [esi+20h],edi
  004f1e47  896e24                mov       [esi+24h],ebp
  004f1e4a  e8f11a0400            call      00533940h
  004f1e4f  8b9690000000          mov       edx,[esi+90h]
  004f1e55  8b4660                mov       eax,[esi+60h]
  004f1e58  8b4e20                mov       ecx,[esi+20h]
  004f1e5b  83c404                add       esp,4
  004f1e5e  80e6fc                and       dh,0FCh
  004f1e61  3bc8                  cmp       ecx,eax
  004f1e63  899690000000          mov       [esi+90h],edx
  004f1e69  7f2a                  jg        short 004F1E95h
  004f1e6b  f6c680                test      dh,80h
  004f1e6e  7525                  jne       short 004F1E95h
  004f1e70  8b0e                  mov       ecx,[esi]
  004f1e72  894620                mov       [esi+20h],eax
  004f1e75  f6c101                test      cl,1
  004f1e78  740f                  je        short 004F1E89h
  004f1e7a  83e1fe                and       ecx,0FFFFFFFEh
  004f1e7d  33db                  xor       ebx,ebx
  004f1e7f  890e                  mov       [esi],ecx
  004f1e81  895e54                mov       [esi+54h],ebx
  004f1e84  e923010000            jmp       004F1FACh

LAB_004f1e89:
  004f1e89  83e1fe                and       ecx,0FFFFFFFEh
  004f1e8c  33db                  xor       ebx,ebx
  004f1e8e  890e                  mov       [esi],ecx
  004f1e90  895e54                mov       [esi+54h],ebx
  004f1e93  eb02                  jmp       short 004F1E97h

LAB_004f1e95:
  004f1e95  33db                  xor       ebx,ebx

LAB_004f1e97:
  004f1e97  8b0e                  mov       ecx,[esi]
  004f1e99  f6c101                test      cl,1
  004f1e9c  750a                  jne       short 004F1EA8h
  004f1e9e  394620                cmp       [esi+20h],eax
  004f1ea1  7e05                  jle       short 004F1EA8h
  004f1ea3  83c901                or        ecx,1
  004f1ea6  890e                  mov       [esi],ecx

LAB_004f1ea8:
  004f1ea8  f7c282ad0020          test      edx,2000AD82h
  004f1eae  0f84f8000000          je        near 004F1FACh
  004f1eb4  f60601                test      byte [esi],1
  004f1eb7  0f85c7000000          jne       near 004F1F84h
  004f1ebd  807e4207              cmp       byte [esi+42h],7
  004f1ec1  7421                  je        short 004F1EE4h
  004f1ec3  c6464207              mov       byte [esi+42h],7
  004f1ec7  e874520500            call      00547140h
  004f1ecc  99                    cdq
  004f1ecd  b928000000            mov       ecx,28h
  004f1ed2  66895e12              mov       [esi+12h],bx
  004f1ed6  f7f9                  idiv      ecx
  004f1ed8  66c746105000          mov       word [esi+10h],50h
  004f1ede  80c214                add       dl,14h
  004f1ee1  885643                mov       [esi+43h],dl

LAB_004f1ee4:
  004f1ee4  8b16                  mov       edx,[esi]
  004f1ee6  8b44242c              mov       eax,[esp+2Ch]
  004f1eea  8b4c2430              mov       ecx,[esp+30h]
  004f1eee  83e2fd                and       edx,0FFFFFFFDh
  004f1ef1  8916                  mov       [esi],edx
  004f1ef3  8b542428              mov       edx,[esp+28h]
  004f1ef7  894620                mov       [esi+20h],eax
  004f1efa  8b44245c              mov       eax,[esp+5Ch]
  004f1efe  89561c                mov       [esi+1Ch],edx
  004f1f01  8b542460              mov       edx,[esp+60h]
  004f1f05  25ffff0000            and       eax,0FFFFh
  004f1f0a  894e24                mov       [esi+24h],ecx
  004f1f0d  8b4c2450              mov       ecx,[esp+50h]
  004f1f11  89567c                mov       [esi+7Ch],edx
  004f1f14  8b542458              mov       edx,[esp+58h]
  004f1f18  898690000000          mov       [esi+90h],eax
  004f1f1e  8b442440              mov       eax,[esp+40h]
  004f1f22  898e84000000          mov       [esi+84h],ecx
  004f1f28  8b4c244c              mov       ecx,[esp+4Ch]
  004f1f2c  898680000000          mov       [esi+80h],eax
  004f1f32  8b442454              mov       eax,[esp+54h]
  004f1f36  899688000000          mov       [esi+88h],edx
  004f1f3c  8b542444              mov       edx,[esp+44h]
  004f1f40  6882ad0020            push      2000AD82h
  004f1f45  56                    push      esi
  004f1f46  89868c000000          mov       [esi+8Ch],eax
  004f1f4c  894e64                mov       [esi+64h],ecx
  004f1f4f  895660                mov       [esi+60h],edx
  004f1f52  e8093ffeff            call      004D5E60h
  004f1f57  83c408                add       esp,8
  004f1f5a  663bc3                cmp       ax,bx
  004f1f5d  7515                  jne       short 004F1F74h
  004f1f5f  e8dc510500            call      00547140h
  004f1f64  99                    cdq
  004f1f65  33c2                  xor       eax,edx
  004f1f67  2bc2                  sub       eax,edx
  004f1f69  83e001                and       eax,1
  004f1f6c  33c2                  xor       eax,edx
  004f1f6e  2bc2                  sub       eax,edx
  004f1f70  8d4400ff              lea       eax,[eax+eax-1]

LAB_004f1f74:
  004f1f74  8b4c2410              mov       ecx,[esp+10h]
  004f1f78  83c140                add       ecx,40h
  004f1f7b  0fafc8                imul      ecx,eax
  004f1f7e  66014e32              add       [esi+32h],cx
  004f1f82  eb28                  jmp       short 004F1FACh

LAB_004f1f84:
  004f1f84  8b542428              mov       edx,[esp+28h]
  004f1f88  8b442430              mov       eax,[esp+30h]
  004f1f8c  895e5c                mov       [esi+5Ch],ebx
  004f1f8f  895e50                mov       [esi+50h],ebx
  004f1f92  895e58                mov       [esi+58h],ebx
  004f1f95  89561c                mov       [esi+1Ch],edx
  004f1f98  894624                mov       [esi+24h],eax
  004f1f9b  885e42                mov       [esi+42h],bl
  004f1f9e  c6464328              mov       byte [esi+43h],28h
  004f1fa2  66895e12              mov       [esi+12h],bx
  004f1fa6  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004f1fac:
  004f1fac  6aff                  push      0FFFFFFFFh
  004f1fae  6aff                  push      0FFFFFFFFh
  004f1fb0  6aff                  push      0FFFFFFFFh
  004f1fb2  56                    push      esi
  004f1fb3  e85818fdff            call      004C3810h
  004f1fb8  8b44242c              mov       eax,[esp+2Ch]
  004f1fbc  83c410                add       esp,10h
  004f1fbf  3bc3                  cmp       eax,ebx
  004f1fc1  7517                  jne       short 004F1FDAh
  004f1fc3  668b8c2482000000      mov       cx,[esp+82h]
  004f1fcb  66894e32              mov       [esi+32h],cx
  004f1fcf  5f                    pop       edi
  004f1fd0  5e                    pop       esi
  004f1fd1  5d                    pop       ebp
  004f1fd2  5b                    pop       ebx
  004f1fd3  81c484000000          add       esp,84h
  004f1fd9  c3                    ret

LAB_004f1fda:
  004f1fda  385e43                cmp       [esi+43h],bl
  004f1fdd  0f850b030000          jne       near 004F22EEh
  004f1fe3  8b466c                mov       eax,[esi+6Ch]
  004f1fe6  8b561c                mov       edx,[esi+1Ch]
  004f1fe9  8b781c                mov       edi,[eax+1Ch]
  004f1fec  8b6824                mov       ebp,[eax+24h]
  004f1fef  8b4624                mov       eax,[esi+24h]
  004f1ff2  2bfa                  sub       edi,edx
  004f1ff4  2be8                  sub       ebp,eax
  004f1ff6  c1ff0c                sar       edi,0Ch
  004f1ff9  c1fd0c                sar       ebp,0Ch
  004f1ffc  8bd7                  mov       edx,edi
  004f1ffe  8bc5                  mov       eax,ebp
  004f2000  0fafd7                imul      edx,edi
  004f2003  0fafc5                imul      eax,ebp
  004f2006  03d0                  add       edx,eax
  004f2008  52                    push      edx
  004f2009  e8023a0500            call      00545A10h
  004f200e  55                    push      ebp
  004f200f  57                    push      edi
  004f2010  8944241c              mov       [esp+1Ch],eax
  004f2014  e8093a0500            call      00545A22h
  004f2019  668b4e32              mov       cx,[esi+32h]
  004f201d  66895e12              mov       [esi+12h],bx
  004f2021  2bc1                  sub       eax,ecx
  004f2023  25ff0f0000            and       eax,0FFFh
  004f2028  89442418              mov       [esp+18h],eax
  004f202c  8b442414              mov       eax,[esp+14h]
  004f2030  3dfa000000            cmp       eax,0FAh
  004f2035  0f8da0000000          jge       near 004F20DBh
  004f203b  e800510500            call      00547140h
  004f2040  99                    cdq
  004f2041  b964000000            mov       ecx,64h
  004f2046  f7f9                  idiv      ecx
  004f2048  8bea                  mov       ebp,edx
  004f204a  83fd1e                cmp       ebp,1Eh
  004f204d  7d77                  jge       short 004F20C6h
  004f204f  8b542418              mov       edx,[esp+18h]
  004f2053  8b7c2420              mov       edi,[esp+20h]
  004f2057  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004f205e  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004f2065  25ff0f0000            and       eax,0FFFh
  004f206a  3bc1                  cmp       eax,ecx
  004f206c  7f4e                  jg        short 004F20BCh
  004f206e  e8cd500500            call      00547140h
  004f2073  99                    cdq
  004f2074  8d4c3f01              lea       ecx,[edi+edi+1]
  004f2078  f7f9                  idiv      ecx
  004f207a  8b442414              mov       eax,[esp+14h]
  004f207e  83c264                add       edx,64h
  004f2081  3bc2                  cmp       eax,edx
  004f2083  7d37                  jge       short 004F20BCh
  004f2085  83fd14                cmp       ebp,14h
  004f2088  7d24                  jge       short 004F20AEh
  004f208a  8b16                  mov       edx,[esi]
  004f208c  c646420b              mov       byte [esi+42h],0Bh
  004f2090  81e2fffffffe          and       edx,0FEFFFFFFh
  004f2096  c6464301              mov       byte [esi+43h],1
  004f209a  83ca02                or        edx,2
  004f209d  895e5c                mov       [esi+5Ch],ebx
  004f20a0  8916                  mov       [esi],edx
  004f20a2  66c746180300          mov       word [esi+18h],3
  004f20a8  66895e1a              mov       [esi+1Ah],bx
  004f20ac  eb31                  jmp       short 004F20DFh

LAB_004f20ae:
  004f20ae  c6464209              mov       byte [esi+42h],9
  004f20b2  c6464301              mov       byte [esi+43h],1
  004f20b6  66895e12              mov       [esi+12h],bx
  004f20ba  eb23                  jmp       short 004F20DFh

LAB_004f20bc:
  004f20bc  c6464201              mov       byte [esi+42h],1
  004f20c0  c6464301              mov       byte [esi+43h],1
  004f20c4  eb19                  jmp       short 004F20DFh

LAB_004f20c6:
  004f20c6  83fd23                cmp       ebp,23h
  004f20c9  0f8d2a020000          jge       near 004F22F9h
  004f20cf  c646420c              mov       byte [esi+42h],0Ch
  004f20d3  c6464301              mov       byte [esi+43h],1
  004f20d7  66895e12              mov       [esi+12h],bx

LAB_004f20db:
  004f20db  8b7c2420              mov       edi,[esp+20h]

LAB_004f20df:
  004f20df  8b442414              mov       eax,[esp+14h]
  004f20e3  3dbc020000            cmp       eax,2BCh
  004f20e8  0f8de4000000          jge       near 004F21D2h
  004f20ee  3dfa000000            cmp       eax,0FAh
  004f20f3  0f8cd9000000          jl        near 004F21D2h
  004f20f9  e842500500            call      00547140h
  004f20fe  99                    cdq
  004f20ff  b964000000            mov       ecx,64h
  004f2104  f7f9                  idiv      ecx
  004f2106  83fa14                cmp       edx,14h
  004f2109  7d44                  jge       short 004F214Fh
  004f210b  8b542418              mov       edx,[esp+18h]
  004f210f  81c200040000          add       edx,400h
  004f2115  81e2ff0f0000          and       edx,0FFFh
  004f211b  81fa00080000          cmp       edx,800h
  004f2121  7c0d                  jl        short 004F2130h
  004f2123  c6464201              mov       byte [esi+42h],1
  004f2127  c6464301              mov       byte [esi+43h],1
  004f212b  e9a2000000            jmp       004F21D2h

LAB_004f2130:
  004f2130  c6464207              mov       byte [esi+42h],7
  004f2134  e807500500            call      00547140h
  004f2139  99                    cdq
  004f213a  b914000000            mov       ecx,14h
  004f213f  66895e12              mov       [esi+12h],bx
  004f2143  f7f9                  idiv      ecx
  004f2145  02d1                  add       dl,cl
  004f2147  885643                mov       [esi+43h],dl
  004f214a  e983000000            jmp       004F21D2h

LAB_004f214f:
  004f214f  83fa28                cmp       edx,28h
  004f2152  7c5b                  jl        short 004F21AFh
  004f2154  83fa3c                cmp       edx,3Ch
  004f2157  7d17                  jge       short 004F2170h
  004f2159  c6464206              mov       byte [esi+42h],6
  004f215d  c6464301              mov       byte [esi+43h],1
  004f2161  e8da4f0500            call      00547140h
  004f2166  66894610              mov       [esi+10h],ax
  004f216a  66895e12              mov       [esi+12h],bx
  004f216e  eb62                  jmp       short 004F21D2h

LAB_004f2170:
  004f2170  83fa50                cmp       edx,50h
  004f2173  7d3a                  jge       short 004F21AFh
  004f2175  c6464203              mov       byte [esi+42h],3
  004f2179  66895e12              mov       [esi+12h],bx
  004f217d  e8be4f0500            call      00547140h
  004f2182  99                    cdq
  004f2183  b90a000000            mov       ecx,0Ah
  004f2188  f7f9                  idiv      ecx
  004f218a  8bc7                  mov       eax,edi
  004f218c  d1f8                  sar       eax,1
  004f218e  02d0                  add       dl,al
  004f2190  80c214                add       dl,14h
  004f2193  885643                mov       [esi+43h],dl
  004f2196  e8a54f0500            call      00547140h
  004f219b  99                    cdq
  004f219c  33c2                  xor       eax,edx
  004f219e  2bc2                  sub       eax,edx
  004f21a0  25ff0f0000            and       eax,0FFFh
  004f21a5  33c2                  xor       eax,edx
  004f21a7  2bc2                  sub       eax,edx
  004f21a9  66894610              mov       [esi+10h],ax
  004f21ad  eb23                  jmp       short 004F21D2h

LAB_004f21af:
  004f21af  c6464202              mov       byte [esi+42h],2
  004f21b3  e8884f0500            call      00547140h
  004f21b8  99                    cdq
  004f21b9  b90a000000            mov       ecx,0Ah
  004f21be  66c746105000          mov       word [esi+10h],50h
  004f21c4  f7f9                  idiv      ecx
  004f21c6  8bc7                  mov       eax,edi
  004f21c8  d1f8                  sar       eax,1
  004f21ca  02d0                  add       dl,al
  004f21cc  80c214                add       dl,14h
  004f21cf  885643                mov       [esi+43h],dl

LAB_004f21d2:
  004f21d2  817c241458020000      cmp       dword [esp+14h],258h
  004f21da  0f8c0e010000          jl        near 004F22EEh
  004f21e0  e85b4f0500            call      00547140h
  004f21e5  99                    cdq
  004f21e6  b964000000            mov       ecx,64h
  004f21eb  f7f9                  idiv      ecx
  004f21ed  83fa1e                cmp       edx,1Eh
  004f21f0  7d6f                  jge       short 004F2261h
  004f21f2  8b442410              mov       eax,[esp+10h]
  004f21f6  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004f21fa  3bd1                  cmp       edx,ecx
  004f21fc  7d2e                  jge       short 004F222Ch
  004f21fe  c6464202              mov       byte [esi+42h],2
  004f2202  e8394f0500            call      00547140h
  004f2207  99                    cdq
  004f2208  b90a000000            mov       ecx,0Ah
  004f220d  66c746106400          mov       word [esi+10h],64h
  004f2213  f7f9                  idiv      ecx
  004f2215  8bc7                  mov       eax,edi
  004f2217  d1f8                  sar       eax,1
  004f2219  02d0                  add       dl,al
  004f221b  80c214                add       dl,14h
  004f221e  885643                mov       [esi+43h],dl
  004f2221  5f                    pop       edi
  004f2222  5e                    pop       esi
  004f2223  5d                    pop       ebp
  004f2224  5b                    pop       ebx
  004f2225  81c484000000          add       esp,84h
  004f222b  c3                    ret

LAB_004f222c:
  004f222c  885e42                mov       [esi+42h],bl
  004f222f  e80c4f0500            call      00547140h
  004f2234  99                    cdq
  004f2235  b90a000000            mov       ecx,0Ah
  004f223a  66895e12              mov       [esi+12h],bx
  004f223e  f7f9                  idiv      ecx
  004f2240  8b4c2410              mov       ecx,[esp+10h]
  004f2244  8bc7                  mov       eax,edi
  004f2246  c1f802                sar       eax,2
  004f2249  02d0                  add       dl,al
  004f224b  885643                mov       [esi+43h],dl
  004f224e  8d540950              lea       edx,[ecx+ecx+50h]
  004f2252  66895610              mov       [esi+10h],dx
  004f2256  5f                    pop       edi
  004f2257  5e                    pop       esi
  004f2258  5d                    pop       ebp
  004f2259  5b                    pop       ebx
  004f225a  81c484000000          add       esp,84h
  004f2260  c3                    ret

LAB_004f2261:
  004f2261  83fa28                cmp       edx,28h
  004f2264  7d43                  jge       short 004F22A9h
  004f2266  c6464203              mov       byte [esi+42h],3
  004f226a  66895e12              mov       [esi+12h],bx
  004f226e  e8cd4e0500            call      00547140h
  004f2273  99                    cdq
  004f2274  b90a000000            mov       ecx,0Ah
  004f2279  f7f9                  idiv      ecx
  004f227b  8bc7                  mov       eax,edi
  004f227d  d1f8                  sar       eax,1
  004f227f  02d0                  add       dl,al
  004f2281  80c214                add       dl,14h
  004f2284  885643                mov       [esi+43h],dl
  004f2287  e8b44e0500            call      00547140h
  004f228c  99                    cdq
  004f228d  33c2                  xor       eax,edx
  004f228f  2bc2                  sub       eax,edx
  004f2291  25ff0f0000            and       eax,0FFFh
  004f2296  33c2                  xor       eax,edx
  004f2298  2bc2                  sub       eax,edx
  004f229a  66894610              mov       [esi+10h],ax
  004f229e  5f                    pop       edi
  004f229f  5e                    pop       esi
  004f22a0  5d                    pop       ebp
  004f22a1  5b                    pop       ebx
  004f22a2  81c484000000          add       esp,84h
  004f22a8  c3                    ret

LAB_004f22a9:
  004f22a9  83fa3c                cmp       edx,3Ch
  004f22ac  7d13                  jge       short 004F22C1h
  004f22ae  c6464201              mov       byte [esi+42h],1
  004f22b2  c6464301              mov       byte [esi+43h],1
  004f22b6  5f                    pop       edi
  004f22b7  5e                    pop       esi
  004f22b8  5d                    pop       ebp
  004f22b9  5b                    pop       ebx
  004f22ba  81c484000000          add       esp,84h
  004f22c0  c3                    ret

LAB_004f22c1:
  004f22c1  83fa50                cmp       edx,50h
  004f22c4  7d17                  jge       short 004F22DDh
  004f22c6  c6464208              mov       byte [esi+42h],8
  004f22ca  c6464301              mov       byte [esi+43h],1
  004f22ce  66895e12              mov       [esi+12h],bx
  004f22d2  5f                    pop       edi
  004f22d3  5e                    pop       esi
  004f22d4  5d                    pop       ebp
  004f22d5  5b                    pop       ebx
  004f22d6  81c484000000          add       esp,84h
  004f22dc  c3                    ret

LAB_004f22dd:
  004f22dd  c6464206              mov       byte [esi+42h],6
  004f22e1  c6464301              mov       byte [esi+43h],1
  004f22e5  e8564e0500            call      00547140h
  004f22ea  66894610              mov       [esi+10h],ax

LAB_004f22ee:
  004f22ee  5f                    pop       edi
  004f22ef  5e                    pop       esi
  004f22f0  5d                    pop       ebp
  004f22f1  5b                    pop       ebx
  004f22f2  81c484000000          add       esp,84h
  004f22f8  c3                    ret

LAB_004f22f9:
  004f22f9  83fd3c                cmp       ebp,3Ch
  004f22fc  7c2e                  jl        short 004F232Ch
  004f22fe  83fd46                cmp       ebp,46h
  004f2301  7d29                  jge       short 004F232Ch
  004f2303  8b4c2410              mov       ecx,[esp+10h]
  004f2307  8b442414              mov       eax,[esp+14h]
  004f230b  83c164                add       ecx,64h
  004f230e  3bc1                  cmp       eax,ecx
  004f2310  7e1a                  jle       short 004F232Ch
  004f2312  c6464206              mov       byte [esi+42h],6
  004f2316  c6464301              mov       byte [esi+43h],1
  004f231a  e8214e0500            call      00547140h
  004f231f  66894610              mov       [esi+10h],ax
  004f2323  66895e12              mov       [esi+12h],bx
  004f2327  e9affdffff            jmp       004F20DBh

LAB_004f232c:
  004f232c  c6464202              mov       byte [esi+42h],2
  004f2330  e80b4e0500            call      00547140h
  004f2335  99                    cdq
  004f2336  b90a000000            mov       ecx,0Ah
  004f233b  8b7c2420              mov       edi,[esp+20h]
  004f233f  f7f9                  idiv      ecx
  004f2341  8bc7                  mov       eax,edi
  004f2343  66c746105000          mov       word [esi+10h],50h
  004f2349  d1f8                  sar       eax,1
  004f234b  02d0                  add       dl,al
  004f234d  80c214                add       dl,14h
  004f2350  885643                mov       [esi+43h],dl
  004f2353  e987fdffff            jmp       004F20DFh
  004f2358  47                    inc       edi
  004f2359  184f00                sbb       [edi],cl
  004f235c  92                    xchg      edx,eax
  004f235d  184f00                sbb       [edi],cl
  004f2360  1e                    push      ds
  004f2361  194f00                sbb       [edi],ecx
  004f2364  c0194f                rcr       byte [ecx],4Fh
  004f2367  00af1d4f00af          add       [edi-50FFB0E3h],ch
  004f236d  1d4f008b1a            sbb       eax,1A8B004Fh
  004f2372  4f                    dec       edi
  004f2373  0027                  add       [edi],ah
  004f2375  1b4f00                sbb       ecx,[edi]
  004f2378  881b                  mov       [ebx],bl
  004f237a  4f                    dec       edi
  004f237b  00541c4f              add       [esp+ebx+4Fh],dl
  004f237f  00af1d4f00bc          add       [edi-43FFB0E3h],ch
  004f2385  1c4f                  sbb       al,4Fh
  004f2387  0007                  add       [edi],al
  004f2389  1d4f009090            sbb       eax,9090004Fh
  004f238e  90                    nop
  004f238f  90                    nop

67 cible(s) de saut interne : 0x004f1541, 0x004f1690, 0x004f16b3, 0x004f16c2, 0x004f16c5, 0x004f1759, 0x004f175d, 0x004f182c, 0x004f182e, 0x004f1832, 0x004f1867, 0x004f187a, 0x004f18cd, 0x004f18f6, 0x004f190e, 0x004f1952, 0x004f196a, 0x004f1972, 0x004f197e, 0x004f19b2, 0x004f19d7, 0x004f1a52, 0x004f1a5d, 0x004f1a61, 0x004f1aa2, 0x004f1b15, 0x004f1b45, 0x004f1b51, 0x004f1bcc, 0x004f1c08, 0x004f1c13, 0x004f1c17, 0x004f1c26, 0x004f1c9c, 0x004f1ce2, 0x004f1d5c, 0x004f1d6f, 0x004f1d9e, 0x004f1dab, 0x004f1daf, 0x004f1e89, 0x004f1e95, 0x004f1e97, 0x004f1ea8, 0x004f1ee4, 0x004f1f74, 0x004f1f84, 0x004f1fac, 0x004f1fda, 0x004f20ae, 0x004f20bc, 0x004f20c6, 0x004f20db, 0x004f20df, 0x004f2130, 0x004f214f, 0x004f2170, 0x004f21af, 0x004f21d2, 0x004f222c, 0x004f2261, 0x004f22a9, 0x004f22c1, 0x004f22dd, 0x004f22ee, 0x004f22f9, 0x004f232c
