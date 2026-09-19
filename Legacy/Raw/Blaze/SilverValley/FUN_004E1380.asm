Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e1380  section .text  offset fichier 0xe0780  3440 octets

  004e1380  81ec88000000          sub       esp,88h
  004e1386  53                    push      ebx
  004e1387  55                    push      ebp
  004e1388  56                    push      esi
  004e1389  8bb42498000000        mov       esi,[esp+98h]
  004e1390  b864000000            mov       eax,64h
  004e1395  33db                  xor       ebx,ebx
  004e1397  668944242e            mov       [esp+2Eh],ax
  004e139c  6689442432            mov       [esp+32h],ax
  004e13a1  8a4641                mov       al,[esi+41h]
  004e13a4  57                    push      edi
  004e13a5  84c0                  test      al,al
  004e13a7  66895c242c            mov       [esp+2Ch],bx
  004e13ac  66c744242e3c00        mov       word [esp+2Eh],3Ch
  004e13b3  66c74424305000        mov       word [esp+30h],50h
  004e13ba  66895c2434            mov       [esp+34h],bx
  004e13bf  7516                  jne       short 004E13D7h
  004e13c1  53                    push      ebx
  004e13c2  6a03                  push      3
  004e13c4  56                    push      esi
  004e13c5  c6464101              mov       byte [esi+41h],1
  004e13c9  e882f3ffff            call      004E0750h
  004e13ce  8b4620                mov       eax,[esi+20h]
  004e13d1  83c40c                add       esp,0Ch
  004e13d4  894648                mov       [esi+48h],eax

LAB_004e13d7:
  004e13d7  8b16                  mov       edx,[esi]
  004e13d9  8b4e1c                mov       ecx,[esi+1Ch]
  004e13dc  8b4664                mov       eax,[esi+64h]
  004e13df  8d6e1c                lea       ebp,[esi+1Ch]
  004e13e2  83e2fe                and       edx,0FFFFFFFEh
  004e13e5  894c245c              mov       [esp+5Ch],ecx
  004e13e9  8b4e60                mov       ecx,[esi+60h]
  004e13ec  8916                  mov       [esi],edx
  004e13ee  8b5624                mov       edx,[esi+24h]
  004e13f1  8944243c              mov       [esp+3Ch],eax
  004e13f5  8b467c                mov       eax,[esi+7Ch]
  004e13f8  89542464              mov       [esp+64h],edx
  004e13fc  668b5632              mov       dx,[esi+32h]
  004e1400  894c2440              mov       [esp+40h],ecx
  004e1404  668b8e90000000        mov       cx,[esi+90h]
  004e140b  6689942486000000      mov       [esp+86h],dx
  004e1413  8b9680000000          mov       edx,[esi+80h]
  004e1419  8b7e6c                mov       edi,[esi+6Ch]
  004e141c  89442458              mov       [esp+58h],eax
  004e1420  8b8684000000          mov       eax,[esi+84h]
  004e1426  66894c2448            mov       [esp+48h],cx
  004e142b  8b8e88000000          mov       ecx,[esi+88h]
  004e1431  89542438              mov       [esp+38h],edx
  004e1435  8b968c000000          mov       edx,[esi+8Ch]
  004e143b  8944244c              mov       [esp+4Ch],eax
  004e143f  894c2454              mov       [esp+54h],ecx
  004e1443  8d442424              lea       eax,[esp+24h]
  004e1447  89542450              mov       [esp+50h],edx
  004e144b  8d4c2468              lea       ecx,[esp+68h]
  004e144f  50                    push      eax
  004e1450  8d542470              lea       edx,[esp+70h]
  004e1454  51                    push      ecx
  004e1455  52                    push      edx
  004e1456  56                    push      esi
  004e1457  897c2438              mov       [esp+38h],edi
  004e145b  e820750500            call      00538980h
  004e1460  83c410                add       esp,10h
  004e1463  8d442444              lea       eax,[esp+44h]
  004e1467  8d4c2420              lea       ecx,[esp+20h]
  004e146b  8d542418              lea       edx,[esp+18h]
  004e146f  50                    push      eax
  004e1470  51                    push      ecx
  004e1471  8d44241c              lea       eax,[esp+1Ch]
  004e1475  52                    push      edx
  004e1476  8d4c241c              lea       ecx,[esp+1Ch]
  004e147a  50                    push      eax
  004e147b  51                    push      ecx
  004e147c  56                    push      esi
  004e147d  e89e4a0500            call      00535F20h
  004e1482  8b4c2428              mov       ecx,[esp+28h]
  004e1486  8b54245c              mov       edx,[esp+5Ch]
  004e148a  83c418                add       esp,18h
  004e148d  b840000000            mov       eax,40h
  004e1492  2bc1                  sub       eax,ecx
  004e1494  52                    push      edx
  004e1495  56                    push      esi
  004e1496  89442424              mov       [esp+24h],eax
  004e149a  e84149ffff            call      004D5DE0h
  004e149f  8b4500                mov       eax,[ebp]
  004e14a2  8b571c                mov       edx,[edi+1Ch]
  004e14a5  8b4e24                mov       ecx,[esi+24h]
  004e14a8  2bc2                  sub       eax,edx
  004e14aa  8b5724                mov       edx,[edi+24h]
  004e14ad  83c408                add       esp,8
  004e14b0  c1f80c                sar       eax,0Ch
  004e14b3  2bca                  sub       ecx,edx
  004e14b5  8bd0                  mov       edx,eax
  004e14b7  c1f90c                sar       ecx,0Ch
  004e14ba  0fafd0                imul      edx,eax
  004e14bd  8bc1                  mov       eax,ecx
  004e14bf  0fafc1                imul      eax,ecx
  004e14c2  03d0                  add       edx,eax
  004e14c4  52                    push      edx
  004e14c5  e846450600            call      00545A10h
  004e14ca  89442414              mov       [esp+14h],eax
  004e14ce  8b442420              mov       eax,[esp+20h]
  004e14d2  3bc3                  cmp       eax,ebx
  004e14d4  0f84fd060000          je        near 004E1BD7h
  004e14da  8b3e                  mov       edi,[esi]
  004e14dc  33c9                  xor       ecx,ecx
  004e14de  8a4e04                mov       cl,[esi+4]
  004e14e1  81e7fffffffd          and       edi,0FDFFFFFFh
  004e14e7  0fbf5618              movsx     edx,word [esi+18h]
  004e14eb  893e                  mov       [esi],edi
  004e14ed  8b0c8d40965e00        mov       ecx,[ecx*4+5E9640h]
  004e14f4  8bc7                  mov       eax,edi
  004e14f6  8b0c91                mov       ecx,[ecx+edx*4]
  004e14f9  a900000001            test      eax,1000000h
  004e14fe  741b                  je        short 004E151Bh
  004e1500  66ff461a              inc       word [esi+1Ah]
  004e1504  668b461a              mov       ax,[esi+1Ah]
  004e1508  33d2                  xor       edx,edx
  004e150a  668b5116              mov       dx,[ecx+16h]
  004e150e  0fbfc0                movsx     eax,ax
  004e1511  3bc2                  cmp       eax,edx
  004e1513  7c27                  jl        short 004E153Ch
  004e1515  66895e1a              mov       [esi+1Ah],bx
  004e1519  eb21                  jmp       short 004E153Ch

LAB_004e151b:
  004e151b  66ff461a              inc       word [esi+1Ah]
  004e151f  668b561a              mov       dx,[esi+1Ah]
  004e1523  33ff                  xor       edi,edi
  004e1525  668b7916              mov       di,[ecx+16h]
  004e1529  0fbfca                movsx     ecx,dx
  004e152c  3bcf                  cmp       ecx,edi
  004e152e  7c0c                  jl        short 004E153Ch
  004e1530  4a                    dec       edx
  004e1531  0d00000002            or        eax,2000000h
  004e1536  6689561a              mov       [esi+1Ah],dx
  004e153a  8906                  mov       [esi],eax

LAB_004e153c:
  004e153c  f60602                test      byte [esi],2
  004e153f  0f8445010000          je        near 004E168Ah
  004e1545  807e420b              cmp       byte [esi+42h],0Bh
  004e1549  7570                  jne       short 004E15BBh
  004e154b  f6461a01              test      byte [esi+1Ah],1
  004e154f  746a                  je        short 004E15BBh
  004e1551  8d54242c              lea       edx,[esp+2Ch]
  004e1555  53                    push      ebx
  004e1556  52                    push      edx
  004e1557  55                    push      ebp
  004e1558  56                    push      esi
  004e1559  e8d2860300            call      00519C30h
  004e155e  8bf8                  mov       edi,eax
  004e1560  a1b8e05d00            mov       eax,[5DE0B8h]
  004e1565  83c410                add       esp,10h
  004e1568  3bc3                  cmp       eax,ebx
  004e156a  744f                  je        short 004E15BBh
  004e156c  8b1f                  mov       ebx,[edi]
  004e156e  66c787960000000a00    mov       word [edi+96h],0Ah
  004e1577  83cb40                or        ebx,40h
  004e157a  57                    push      edi
  004e157b  891f                  mov       [edi],ebx
  004e157d  8b4c2428              mov       ecx,[esp+28h]
  004e1581  56                    push      esi
  004e1582  668b4130              mov       ax,[ecx+30h]
  004e1586  0fbfd8                movsx     ebx,ax
  004e1589  66c1f802              sar       ax,2
  004e158d  66894130              mov       [ecx+30h],ax
  004e1591  e8ea850500            call      00539B80h
  004e1596  8b44242c              mov       eax,[esp+2Ch]
  004e159a  83c408                add       esp,8
  004e159d  83c71c                add       edi,1Ch
  004e15a0  57                    push      edi
  004e15a1  6800180000            push      1800h
  004e15a6  6800030000            push      300h
  004e15ab  6a03                  push      3
  004e15ad  66895830              mov       [eax+30h],bx
  004e15b1  e86ac50500            call      0053DB20h
  004e15b6  83c410                add       esp,10h
  004e15b9  33db                  xor       ebx,ebx

LAB_004e15bb:
  004e15bb  807e420c              cmp       byte [esi+42h],0Ch
  004e15bf  0f85c5000000          jne       near 004E168Ah
  004e15c5  8d4c242c              lea       ecx,[esp+2Ch]
  004e15c9  53                    push      ebx
  004e15ca  51                    push      ecx
  004e15cb  55                    push      ebp
  004e15cc  56                    push      esi
  004e15cd  e85e860300            call      00519C30h
  004e15d2  8bf8                  mov       edi,eax
  004e15d4  a1b8e05d00            mov       eax,[5DE0B8h]
  004e15d9  83c410                add       esp,10h
  004e15dc  3bc3                  cmp       eax,ebx
  004e15de  0f84a6000000          je        near 004E168Ah
  004e15e4  8b1f                  mov       ebx,[edi]
  004e15e6  66c787960000001e00    mov       word [edi+96h],1Eh
  004e15ef  83cb40                or        ebx,40h
  004e15f2  57                    push      edi
  004e15f3  891f                  mov       [edi],ebx
  004e15f5  8b4c2428              mov       ecx,[esp+28h]
  004e15f9  56                    push      esi
  004e15fa  668b4130              mov       ax,[ecx+30h]
  004e15fe  0fbfd8                movsx     ebx,ax
  004e1601  66d1f8                sar       ax,1
  004e1604  66894130              mov       [ecx+30h],ax
  004e1608  e873850500            call      00539B80h
  004e160d  8b54242c              mov       edx,[esp+2Ch]
  004e1611  83c408                add       esp,8
  004e1614  8d471c                lea       eax,[edi+1Ch]
  004e1617  50                    push      eax
  004e1618  6800180000            push      1800h
  004e161d  6800030000            push      300h
  004e1622  6a03                  push      3
  004e1624  66895a30              mov       [edx+30h],bx
  004e1628  e8f3c40500            call      0053DB20h
  004e162d  8b07                  mov       eax,[edi]
  004e162f  83c410                add       esp,10h
  004e1632  a900000040            test      eax,40000000h
  004e1637  7549                  jne       short 004E1682h
  004e1639  6aff                  push      0FFFFFFFFh
  004e163b  6a00                  push      0
  004e163d  6a39                  push      39h
  004e163f  56                    push      esi
  004e1640  e88b93f9ff            call      0047A9D0h
  004e1645  8b1f                  mov       ebx,[edi]
  004e1647  83c410                add       esp,10h
  004e164a  83cb01                or        ebx,1
  004e164d  891f                  mov       [edi],ebx
  004e164f  0fbf4e32              movsx     ecx,word [esi+32h]
  004e1653  51                    push      ecx
  004e1654  e8c3430600            call      00545A1Ch
  004e1659  8b565c                mov       edx,[esi+5Ch]
  004e165c  c1fa0b                sar       edx,0Bh
  004e165f  0fafc2                imul      eax,edx
  004e1662  894750                mov       [edi+50h],eax
  004e1665  0fbf4632              movsx     eax,word [esi+32h]
  004e1669  50                    push      eax
  004e166a  e8a7430600            call      00545A16h
  004e166f  8b4e5c                mov       ecx,[esi+5Ch]
  004e1672  c7475400800100        mov       dword [edi+54h],18000h
  004e1679  c1f90b                sar       ecx,0Bh
  004e167c  0fafc1                imul      eax,ecx
  004e167f  894758                mov       [edi+58h],eax

LAB_004e1682:
  004e1682  8b06                  mov       eax,[esi]
  004e1684  24fd                  and       al,0FDh
  004e1686  33db                  xor       ebx,ebx
  004e1688  8906                  mov       [esi],eax

LAB_004e168a:
  004e168a  33c0                  xor       eax,eax
  004e168c  8a4642                mov       al,[esi+42h]
  004e168f  83f80c                cmp       eax,0Ch
  004e1692  0f87ca040000          ja        near 004E1B62h
  004e1698  ff2485b8204e00        jmp       dword [eax*4+4E20B8h]
  004e169f  66395e12              cmp       [esi+12h],bx
  004e16a3  751c                  jne       short 004E16C1h
  004e16a5  8b06                  mov       eax,[esi]
  004e16a7  66c746180400          mov       word [esi+18h],4
  004e16ad  0d00000001            or        eax,1000000h
  004e16b2  66895e1a              mov       [esi+1Ah],bx
  004e16b6  8906                  mov       [esi],eax
  004e16b8  66c746120100          mov       word [esi+12h],1
  004e16be  895e5c                mov       [esi+5Ch],ebx

LAB_004e16c1:
  004e16c1  0fbf5610              movsx     edx,word [esi+10h]
  004e16c5  39542414              cmp       [esp+14h],edx
  004e16c9  7d09                  jge       short 004E16D4h
  004e16cb  c6464300              mov       byte [esi+43h],0
  004e16cf  e98e040000            jmp       004E1B62h

LAB_004e16d4:
  004e16d4  8a4643                mov       al,[esi+43h]
  004e16d7  84c0                  test      al,al
  004e16d9  0f84d3000000          je        near 004E17B2h
  004e16df  fec8                  dec       al
  004e16e1  895e5c                mov       [esi+5Ch],ebx
  004e16e4  884643                mov       [esi+43h],al
  004e16e7  e976040000            jmp       004E1B62h
  004e16ec  66395e12              cmp       [esi+12h],bx
  004e16f0  7520                  jne       short 004E1712h
  004e16f2  8b06                  mov       eax,[esi]
  004e16f4  66c746180400          mov       word [esi+18h],4
  004e16fa  0d00000001            or        eax,1000000h
  004e16ff  66895e1a              mov       [esi+1Ah],bx
  004e1703  8906                  mov       [esi],eax
  004e1705  66c746120100          mov       word [esi+12h],1
  004e170b  c7465c00700000        mov       dword [esi+5Ch],7000h

LAB_004e1712:
  004e1712  8b442418              mov       eax,[esp+18h]
  004e1716  8d88c8000000          lea       ecx,[eax+0C8h]
  004e171c  81e1ff0f0000          and       ecx,0FFFh
  004e1722  81f990010000          cmp       ecx,190h
  004e1728  7f09                  jg        short 004E1733h
  004e172a  c6464300              mov       byte [esi+43h],0
  004e172e  e92f040000            jmp       004E1B62h

LAB_004e1733:
  004e1733  25ff0f0000            and       eax,0FFFh
  004e1738  3d00080000            cmp       eax,800h
  004e173d  7d13                  jge       short 004E1752h
  004e173f  8b542410              mov       edx,[esp+10h]
  004e1743  8d8280000000          lea       eax,[edx+80h]
  004e1749  66014632              add       [esi+32h],ax
  004e174d  e910040000            jmp       004E1B62h

LAB_004e1752:
  004e1752  8b4c2410              mov       ecx,[esp+10h]
  004e1756  b880ffffff            mov       eax,0FFFFFF80h
  004e175b  2bc1                  sub       eax,ecx
  004e175d  66014632              add       [esi+32h],ax
  004e1761  e9fc030000            jmp       004E1B62h
  004e1766  66395e12              cmp       [esi+12h],bx
  004e176a  8b4c2410              mov       ecx,[esp+10h]
  004e176e  b801000000            mov       eax,1
  004e1773  7525                  jne       short 004E179Ah
  004e1775  8b16                  mov       edx,[esi]
  004e1777  66894618              mov       [esi+18h],ax
  004e177b  81ca00000001          or        edx,1000000h
  004e1781  66895e1a              mov       [esi+1Ah],bx
  004e1785  8916                  mov       [esi],edx
  004e1787  8d14c9                lea       edx,[ecx+ecx*8]
  004e178a  c1e204                shl       edx,4
  004e178d  81c200a00000          add       edx,0A000h
  004e1793  66894612              mov       [esi+12h],ax
  004e1797  89565c                mov       [esi+5Ch],edx

LAB_004e179a:
  004e179a  66394612              cmp       [esi+12h],ax
  004e179e  0f85be030000          jne       near 004E1B62h
  004e17a4  0fbf4610              movsx     eax,word [esi+10h]
  004e17a8  39442414              cmp       [esp+14h],eax
  004e17ac  7d0c                  jge       short 004E17BAh
  004e17ae  c6464300              mov       byte [esi+43h],0

LAB_004e17b2:
  004e17b2  895e5c                mov       [esi+5Ch],ebx
  004e17b5  e9a8030000            jmp       004E1B62h

LAB_004e17ba:
  004e17ba  8a4643                mov       al,[esi+43h]
  004e17bd  84c0                  test      al,al
  004e17bf  7405                  je        short 004E17C6h
  004e17c1  fec8                  dec       al
  004e17c3  884643                mov       [esi+43h],al

LAB_004e17c6:
  004e17c6  8b442418              mov       eax,[esp+18h]
  004e17ca  8d9000010000          lea       edx,[eax+100h]
  004e17d0  81e2ff0f0000          and       edx,0FFFh
  004e17d6  81fa00020000          cmp       edx,200h
  004e17dc  0f8e80030000          jle       near 004E1B62h
  004e17e2  25ff0f0000            and       eax,0FFFh
  004e17e7  3d00080000            cmp       eax,800h
  004e17ec  7d0d                  jge       short 004E17FBh
  004e17ee  8d440928              lea       eax,[ecx+ecx+28h]
  004e17f2  66014632              add       [esi+32h],ax
  004e17f6  e967030000            jmp       004E1B62h

LAB_004e17fb:
  004e17fb  03c9                  add       ecx,ecx
  004e17fd  b8d8ffffff            mov       eax,0FFFFFFD8h
  004e1802  2bc1                  sub       eax,ecx
  004e1804  66014632              add       [esi+32h],ax
  004e1808  e955030000            jmp       004E1B62h
  004e180d  66837e1201            cmp       word [esi+12h],1
  004e1812  0f8598000000          jne       near 004E18B0h
  004e1818  8a4643                mov       al,[esi+43h]
  004e181b  84c0                  test      al,al
  004e181d  7405                  je        short 004E1824h
  004e181f  fec8                  dec       al
  004e1821  884643                mov       [esi+43h],al

LAB_004e1824:
  004e1824  8b442410              mov       eax,[esp+10h]
  004e1828  8b7c2420              mov       edi,[esp+20h]
  004e182c  8d14c0                lea       edx,[eax+eax*8]
  004e182f  c1e204                shl       edx,4
  004e1832  8d8200500000          lea       eax,[edx+5000h]
  004e1838  0faff8                imul      edi,eax
  004e183b  89465c                mov       [esi+5Ch],eax
  004e183e  0fbf4610              movsx     eax,word [esi+10h]
  004e1842  50                    push      eax
  004e1843  c1ff0c                sar       edi,0Ch
  004e1846  e8d1410600            call      00545A1Ch
  004e184b  0fafc7                imul      eax,edi
  004e184e  8b4d00                mov       ecx,[ebp]
  004e1851  c1f80c                sar       eax,0Ch
  004e1854  03c8                  add       ecx,eax
  004e1856  894d00                mov       [ebp],ecx
  004e1859  0fbf4e10              movsx     ecx,word [esi+10h]
  004e185d  51                    push      ecx
  004e185e  e8b3410600            call      00545A16h
  004e1863  0fafc7                imul      eax,edi
  004e1866  8b6e24                mov       ebp,[esi+24h]
  004e1869  895e5c                mov       [esi+5Ch],ebx
  004e186c  c1f80c                sar       eax,0Ch
  004e186f  03e8                  add       ebp,eax
  004e1871  8b442418              mov       eax,[esp+18h]
  004e1875  896e24                mov       [esi+24h],ebp
  004e1878  8d9000010000          lea       edx,[eax+100h]
  004e187e  81e2ff0f0000          and       edx,0FFFh
  004e1884  81fa00020000          cmp       edx,200h
  004e188a  7e24                  jle       short 004E18B0h
  004e188c  25ff0f0000            and       eax,0FFFh
  004e1891  3d00080000            cmp       eax,800h
  004e1896  7d09                  jge       short 004E18A1h
  004e1898  8b442410              mov       eax,[esp+10h]
  004e189c  83c032                add       eax,32h
  004e189f  eb0b                  jmp       short 004E18ACh

LAB_004e18a1:
  004e18a1  8b4c2410              mov       ecx,[esp+10h]
  004e18a5  b8ceffffff            mov       eax,0FFFFFFCEh
  004e18aa  2bc1                  sub       eax,ecx

LAB_004e18ac:
  004e18ac  66014632              add       [esi+32h],ax

LAB_004e18b0:
  004e18b0  66395e12              cmp       [esi+12h],bx
  004e18b4  0f85a8020000          jne       near 004E1B62h
  004e18ba  8b0e                  mov       ecx,[esi]
  004e18bc  b801000000            mov       eax,1
  004e18c1  81c900000001          or        ecx,1000000h
  004e18c7  66894618              mov       [esi+18h],ax
  004e18cb  66895e1a              mov       [esi+1Ah],bx
  004e18cf  890e                  mov       [esi],ecx
  004e18d1  66894612              mov       [esi+12h],ax
  004e18d5  e988020000            jmp       004E1B62h
  004e18da  668b4e12              mov       cx,[esi+12h]
  004e18de  6683f901              cmp       cx,1
  004e18e2  751c                  jne       short 004E1900h
  004e18e4  0fbf5610              movsx     edx,word [esi+10h]
  004e18e8  39542414              cmp       [esp+14h],edx
  004e18ec  7d06                  jge       short 004E18F4h
  004e18ee  c6464300              mov       byte [esi+43h],0
  004e18f2  eb0c                  jmp       short 004E1900h

LAB_004e18f4:
  004e18f4  8a4643                mov       al,[esi+43h]
  004e18f7  84c0                  test      al,al
  004e18f9  7405                  je        short 004E1900h
  004e18fb  fec8                  dec       al
  004e18fd  884643                mov       [esi+43h],al

LAB_004e1900:
  004e1900  663bcb                cmp       cx,bx
  004e1903  0f8559020000          jne       near 004E1B62h
  004e1909  8b3e                  mov       edi,[esi]
  004e190b  b801000000            mov       eax,1
  004e1910  66894618              mov       [esi+18h],ax
  004e1914  66894612              mov       [esi+12h],ax
  004e1918  8b442410              mov       eax,[esp+10h]
  004e191c  81cf00000001          or        edi,1000000h
  004e1922  66895e1a              mov       [esi+1Ah],bx
  004e1926  893e                  mov       [esi],edi
  004e1928  8d04c0                lea       eax,[eax+eax*8]
  004e192b  c1e004                shl       eax,4
  004e192e  0500600000            add       eax,6000h
  004e1933  89465c                mov       [esi+5Ch],eax
  004e1936  e927020000            jmp       004E1B62h
  004e193b  66395e12              cmp       [esi+12h],bx
  004e193f  7534                  jne       short 004E1975h
  004e1941  e8fa570600            call      00547140h
  004e1946  99                    cdq
  004e1947  33c2                  xor       eax,edx
  004e1949  66816632ff0f          and       word [esi+32h],0FFFh
  004e194f  2bc2                  sub       eax,edx
  004e1951  66c746120100          mov       word [esi+12h],1
  004e1957  25ff0f0000            and       eax,0FFFh
  004e195c  66895e18              mov       [esi+18h],bx
  004e1960  33c2                  xor       eax,edx
  004e1962  66895e1a              mov       [esi+1Ah],bx
  004e1966  2bc2                  sub       eax,edx
  004e1968  66894610              mov       [esi+10h],ax
  004e196c  8b06                  mov       eax,[esi]
  004e196e  0d00000001            or        eax,1000000h
  004e1973  8906                  mov       [esi],eax

LAB_004e1975:
  004e1975  66837e1201            cmp       word [esi+12h],1
  004e197a  0f85e2010000          jne       near 004E1B62h
  004e1980  668b4e32              mov       cx,[esi+32h]
  004e1984  0fbf4610              movsx     eax,word [esi+10h]
  004e1988  0fbfd1                movsx     edx,cx
  004e198b  8bf8                  mov       edi,eax
  004e198d  2bfa                  sub       edi,edx
  004e198f  c1ff03                sar       edi,3
  004e1992  03cf                  add       ecx,edi
  004e1994  66894e32              mov       [esi+32h],cx
  004e1998  0fbfc9                movsx     ecx,cx
  004e199b  2bc1                  sub       eax,ecx
  004e199d  83f814                cmp       eax,14h
  004e19a0  0f8dbc010000          jge       near 004E1B62h
  004e19a6  e895570600            call      00547140h
  004e19ab  99                    cdq
  004e19ac  b914000000            mov       ecx,14h
  004e19b1  c6464207              mov       byte [esi+42h],7
  004e19b5  f7f9                  idiv      ecx
  004e19b7  80c21e                add       dl,1Eh
  004e19ba  885643                mov       [esi+43h],dl
  004e19bd  e9a0010000            jmp       004E1B62h
  004e19c2  66395e12              cmp       [esi+12h],bx
  004e19c6  752c                  jne       short 004E19F4h
  004e19c8  8b0e                  mov       ecx,[esi]
  004e19ca  66c746180600          mov       word [esi+18h],6
  004e19d0  81c900000001          or        ecx,1000000h
  004e19d6  66895e1a              mov       [esi+1Ah],bx
  004e19da  890e                  mov       [esi],ecx
  004e19dc  66c746120100          mov       word [esi+12h],1
  004e19e2  e859570600            call      00547140h
  004e19e7  99                    cdq
  004e19e8  b90a000000            mov       ecx,0Ah
  004e19ed  f7f9                  idiv      ecx
  004e19ef  02d1                  add       dl,cl
  004e19f1  885643                mov       [esi+43h],dl

LAB_004e19f4:
  004e19f4  66837e1201            cmp       word [esi+12h],1
  004e19f9  0f8563010000          jne       near 004E1B62h
  004e19ff  8b542428              mov       edx,[esp+28h]
  004e1a03  8b4620                mov       eax,[esi+20h]
  004e1a06  8b4a20                mov       ecx,[edx+20h]
  004e1a09  2bc8                  sub       ecx,eax
  004e1a0b  81c100400100          add       ecx,14000h
  004e1a11  c1f902                sar       ecx,2
  004e1a14  03c8                  add       ecx,eax
  004e1a16  8a4643                mov       al,[esi+43h]
  004e1a19  fec8                  dec       al
  004e1a1b  894e20                mov       [esi+20h],ecx
  004e1a1e  884643                mov       [esi+43h],al
  004e1a21  e93c010000            jmp       004E1B62h
  004e1a26  8b7c2410              mov       edi,[esp+10h]
  004e1a2a  8b4c2418              mov       ecx,[esp+18h]
  004e1a2e  bd01000000            mov       ebp,1
  004e1a33  66396e12              cmp       [esi+12h],bp
  004e1a37  756d                  jne       short 004E1AA6h
  004e1a39  8b4620                mov       eax,[esi+20h]
  004e1a3c  8b5660                mov       edx,[esi+60h]
  004e1a3f  0500600000            add       eax,6000h
  004e1a44  894620                mov       [esi+20h],eax
  004e1a47  2bc2                  sub       eax,edx
  004e1a49  c1f80c                sar       eax,0Ch
  004e1a4c  3dc8000000            cmp       eax,0C8h
  004e1a51  7e1d                  jle       short 004E1A70h
  004e1a53  8b06                  mov       eax,[esi]
  004e1a55  66c746120200          mov       word [esi+12h],2
  004e1a5b  0d02000001            or        eax,1000002h
  004e1a60  66895e10              mov       [esi+10h],bx
  004e1a64  66c746180500          mov       word [esi+18h],5
  004e1a6a  66895e1a              mov       [esi+1Ah],bx
  004e1a6e  8906                  mov       [esi],eax

LAB_004e1a70:
  004e1a70  8d9100010000          lea       edx,[ecx+100h]
  004e1a76  81e2ff0f0000          and       edx,0FFFh
  004e1a7c  81fa00020000          cmp       edx,200h
  004e1a82  7e22                  jle       short 004E1AA6h
  004e1a84  8bc1                  mov       eax,ecx
  004e1a86  25ff0f0000            and       eax,0FFFh
  004e1a8b  3d00080000            cmp       eax,800h
  004e1a90  7d06                  jge       short 004E1A98h
  004e1a92  8d443f28              lea       eax,[edi+edi+28h]
  004e1a96  eb0a                  jmp       short 004E1AA2h

LAB_004e1a98:
  004e1a98  8d143f                lea       edx,[edi+edi]
  004e1a9b  b8d8ffffff            mov       eax,0FFFFFFD8h
  004e1aa0  2bc2                  sub       eax,edx

LAB_004e1aa2:
  004e1aa2  66014632              add       [esi+32h],ax

LAB_004e1aa6:
  004e1aa6  66837e1202            cmp       word [esi+12h],2
  004e1aab  756f                  jne       short 004E1B1Ch
  004e1aad  8b542428              mov       edx,[esp+28h]
  004e1ab1  8b4620                mov       eax,[esi+20h]
  004e1ab4  c7465c00900100        mov       dword [esi+5Ch],19000h
  004e1abb  8b5220                mov       edx,[edx+20h]
  004e1abe  2bd0                  sub       edx,eax
  004e1ac0  81c200400100          add       edx,14000h
  004e1ac6  c1fa02                sar       edx,2
  004e1ac9  03d0                  add       edx,eax
  004e1acb  66ff4610              inc       word [esi+10h]
  004e1acf  8d8100010000          lea       eax,[ecx+100h]
  004e1ad5  895620                mov       [esi+20h],edx
  004e1ad8  668b5610              mov       dx,[esi+10h]
  004e1adc  25ff0f0000            and       eax,0FFFh
  004e1ae1  3d00020000            cmp       eax,200h
  004e1ae6  7e22                  jle       short 004E1B0Ah
  004e1ae8  81e1ff0f0000          and       ecx,0FFFh
  004e1aee  81f900080000          cmp       ecx,800h
  004e1af4  7d06                  jge       short 004E1AFCh
  004e1af6  8d443f28              lea       eax,[edi+edi+28h]
  004e1afa  eb0a                  jmp       short 004E1B06h

LAB_004e1afc:
  004e1afc  8d0c3f                lea       ecx,[edi+edi]
  004e1aff  b8d8ffffff            mov       eax,0FFFFFFD8h
  004e1b04  2bc1                  sub       eax,ecx

LAB_004e1b06:
  004e1b06  66014632              add       [esi+32h],ax

LAB_004e1b0a:
  004e1b0a  6683fa21              cmp       dx,21h
  004e1b0e  7c0c                  jl        short 004E1B1Ch
  004e1b10  8b06                  mov       eax,[esi]
  004e1b12  66c746120300          mov       word [esi+12h],3
  004e1b18  24fd                  and       al,0FDh
  004e1b1a  8906                  mov       [esi],eax

LAB_004e1b1c:
  004e1b1c  668b4e12              mov       cx,[esi+12h]
  004e1b20  6683f903              cmp       cx,3
  004e1b24  751d                  jne       short 004E1B43h
  004e1b26  8b465c                mov       eax,[esi+5Ch]
  004e1b29  8bd0                  mov       edx,eax
  004e1b2b  c1fa03                sar       edx,3
  004e1b2e  2bc2                  sub       eax,edx
  004e1b30  89465c                mov       [esi+5Ch],eax
  004e1b33  2500f0ffff            and       eax,0FFFFF000h
  004e1b38  3d00a00000            cmp       eax,0A000h
  004e1b3d  7d04                  jge       short 004E1B43h
  004e1b3f  c6464300              mov       byte [esi+43h],0

LAB_004e1b43:
  004e1b43  663bcb                cmp       cx,bx
  004e1b46  751a                  jne       short 004E1B62h
  004e1b48  8b06                  mov       eax,[esi]
  004e1b4a  66896e12              mov       [esi+12h],bp
  004e1b4e  24fd                  and       al,0FDh
  004e1b50  66896e18              mov       [esi+18h],bp
  004e1b54  0d00000001            or        eax,1000000h
  004e1b59  66895e1a              mov       [esi+1Ah],bx
  004e1b5d  895e5c                mov       [esi+5Ch],ebx
  004e1b60  8906                  mov       [esi],eax

LAB_004e1b62:
  004e1b62  8a4642                mov       al,[esi+42h]
  004e1b65  3c0c                  cmp       al,0Ch
  004e1b67  7457                  je        short 004E1BC0h
  004e1b69  3c0b                  cmp       al,0Bh
  004e1b6b  7453                  je        short 004E1BC0h
  004e1b6d  e8ce550600            call      00547140h
  004e1b72  99                    cdq
  004e1b73  b964000000            mov       ecx,64h
  004e1b78  f7f9                  idiv      ecx
  004e1b7a  81c2c8000000          add       edx,0C8h
  004e1b80  66015614              add       [esi+14h],dx
  004e1b84  668b4614              mov       ax,[esi+14h]
  004e1b88  663d0010              cmp       ax,1000h
  004e1b8c  7c09                  jl        short 004E1B97h
  004e1b8e  0500f0ffff            add       eax,0FFFFF000h
  004e1b93  66894614              mov       [esi+14h],ax

LAB_004e1b97:
  004e1b97  0fbf5614              movsx     edx,word [esi+14h]
  004e1b9b  8b7e20                mov       edi,[esi+20h]
  004e1b9e  52                    push      edx
  004e1b9f  e8783e0600            call      00545A1Ch
  004e1ba4  8b4e48                mov       ecx,[esi+48h]
  004e1ba7  8d0440                lea       eax,[eax+eax*2]
  004e1baa  8d0480                lea       eax,[eax+eax*4]
  004e1bad  8d1481                lea       edx,[ecx+eax*4]
  004e1bb0  2bd7                  sub       edx,edi
  004e1bb2  81c200c00800          add       edx,8C000h
  004e1bb8  c1fa03                sar       edx,3
  004e1bbb  03d7                  add       edx,edi
  004e1bbd  895620                mov       [esi+20h],edx

LAB_004e1bc0:
  004e1bc0  8b4c2428              mov       ecx,[esp+28h]
  004e1bc4  8b4164                mov       eax,[ecx+64h]
  004e1bc7  8b4920                mov       ecx,[ecx+20h]
  004e1bca  33c8                  xor       ecx,eax
  004e1bcc  f7c100f0ffff          test      ecx,0FFFFF000h
  004e1bd2  7503                  jne       short 004E1BD7h
  004e1bd4  894648                mov       [esi+48h],eax

LAB_004e1bd7:
  004e1bd7  0fbf5632              movsx     edx,word [esi+32h]
  004e1bdb  52                    push      edx
  004e1bdc  e83b3e0600            call      00545A1Ch
  004e1be1  89442470              mov       [esp+70h],eax
  004e1be5  0fbf4632              movsx     eax,word [esi+32h]
  004e1be9  50                    push      eax
  004e1bea  e8273e0600            call      00545A16h
  004e1bef  8b5650                mov       edx,[esi+50h]
  004e1bf2  89442478              mov       [esp+78h],eax
  004e1bf6  8bc2                  mov       eax,edx
  004e1bf8  8b4c2420              mov       ecx,[esp+20h]
  004e1bfc  c1f803                sar       eax,3
  004e1bff  0faf4e5c              imul      ecx,[esi+5Ch]
  004e1c03  8bfa                  mov       edi,edx
  004e1c05  56                    push      esi
  004e1c06  2bf8                  sub       edi,eax
  004e1c08  c1fa08                sar       edx,8
  004e1c0b  2bfa                  sub       edi,edx
  004e1c0d  8b5654                mov       edx,[esi+54h]
  004e1c10  8bc2                  mov       eax,edx
  004e1c12  8bea                  mov       ebp,edx
  004e1c14  c1f803                sar       eax,3
  004e1c17  2be8                  sub       ebp,eax
  004e1c19  897e50                mov       [esi+50h],edi
  004e1c1c  c1fa08                sar       edx,8
  004e1c1f  2bea                  sub       ebp,edx
  004e1c21  8b5658                mov       edx,[esi+58h]
  004e1c24  8bda                  mov       ebx,edx
  004e1c26  8bc2                  mov       eax,edx
  004e1c28  c1fb03                sar       ebx,3
  004e1c2b  2bc3                  sub       eax,ebx
  004e1c2d  8b5e20                mov       ebx,[esi+20h]
  004e1c30  c1fa08                sar       edx,8
  004e1c33  c1f90c                sar       ecx,0Ch
  004e1c36  2bc2                  sub       eax,edx
  004e1c38  8bd1                  mov       edx,ecx
  004e1c3a  0faf542474            imul      edx,[esp+74h]
  004e1c3f  0faf4c247c            imul      ecx,[esp+7Ch]
  004e1c44  c1fa0c                sar       edx,0Ch
  004e1c47  c1f90c                sar       ecx,0Ch
  004e1c4a  894658                mov       [esi+58h],eax
  004e1c4d  03d7                  add       edx,edi
  004e1c4f  8b7e1c                mov       edi,[esi+1Ch]
  004e1c52  03c8                  add       ecx,eax
  004e1c54  8b4624                mov       eax,[esi+24h]
  004e1c57  03fa                  add       edi,edx
  004e1c59  03dd                  add       ebx,ebp
  004e1c5b  03c1                  add       eax,ecx
  004e1c5d  896e54                mov       [esi+54h],ebp
  004e1c60  897e1c                mov       [esi+1Ch],edi
  004e1c63  895e20                mov       [esi+20h],ebx
  004e1c66  894624                mov       [esi+24h],eax
  004e1c69  e8d21c0500            call      00533940h
  004e1c6e  8bbe90000000          mov       edi,[esi+90h]
  004e1c74  8b4660                mov       eax,[esi+60h]
  004e1c77  8b5620                mov       edx,[esi+20h]
  004e1c7a  83c404                add       esp,4
  004e1c7d  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004e1c83  3bd0                  cmp       edx,eax
  004e1c85  89be90000000          mov       [esi+90h],edi
  004e1c8b  8bcf                  mov       ecx,edi
  004e1c8d  7f15                  jg        short 004E1CA4h
  004e1c8f  f6c580                test      ch,80h
  004e1c92  7510                  jne       short 004E1CA4h
  004e1c94  894620                mov       [esi+20h],eax
  004e1c97  8b06                  mov       eax,[esi]
  004e1c99  24fe                  and       al,0FEh
  004e1c9b  33db                  xor       ebx,ebx
  004e1c9d  8906                  mov       [esi],eax
  004e1c9f  895e54                mov       [esi+54h],ebx
  004e1ca2  eb02                  jmp       short 004E1CA6h

LAB_004e1ca4:
  004e1ca4  33db                  xor       ebx,ebx

LAB_004e1ca6:
  004e1ca6  f7c182880000          test      ecx,8882h
  004e1cac  0f84c0000000          je        near 004E1D72h
  004e1cb2  8a4e42                mov       cl,[esi+42h]
  004e1cb5  b007                  mov       al,7
  004e1cb7  3ac8                  cmp       cl,al
  004e1cb9  7420                  je        short 004E1CDBh
  004e1cbb  884642                mov       [esi+42h],al
  004e1cbe  e87d540600            call      00547140h
  004e1cc3  99                    cdq
  004e1cc4  b928000000            mov       ecx,28h
  004e1cc9  66895e12              mov       [esi+12h],bx
  004e1ccd  f7f9                  idiv      ecx
  004e1ccf  66c746105000          mov       word [esi+10h],50h
  004e1cd5  80c214                add       dl,14h
  004e1cd8  885643                mov       [esi+43h],dl

LAB_004e1cdb:
  004e1cdb  8b16                  mov       edx,[esi]
  004e1cdd  8b442464              mov       eax,[esp+64h]
  004e1ce1  8b4c2458              mov       ecx,[esp+58h]
  004e1ce5  83e2fd                and       edx,0FFFFFFFDh
  004e1ce8  894624                mov       [esi+24h],eax
  004e1ceb  8b442448              mov       eax,[esp+48h]
  004e1cef  8916                  mov       [esi],edx
  004e1cf1  8b54245c              mov       edx,[esp+5Ch]
  004e1cf5  25ffff0000            and       eax,0FFFFh
  004e1cfa  89561c                mov       [esi+1Ch],edx
  004e1cfd  8b542438              mov       edx,[esp+38h]
  004e1d01  894e7c                mov       [esi+7Ch],ecx
  004e1d04  8b4c2454              mov       ecx,[esp+54h]
  004e1d08  898690000000          mov       [esi+90h],eax
  004e1d0e  8b44244c              mov       eax,[esp+4Ch]
  004e1d12  899680000000          mov       [esi+80h],edx
  004e1d18  8b542450              mov       edx,[esp+50h]
  004e1d1c  898684000000          mov       [esi+84h],eax
  004e1d22  8b44243c              mov       eax,[esp+3Ch]
  004e1d26  898e88000000          mov       [esi+88h],ecx
  004e1d2c  8b4c2440              mov       ecx,[esp+40h]
  004e1d30  6882880000            push      8882h
  004e1d35  56                    push      esi
  004e1d36  89968c000000          mov       [esi+8Ch],edx
  004e1d3c  894664                mov       [esi+64h],eax
  004e1d3f  894e60                mov       [esi+60h],ecx
  004e1d42  e81941ffff            call      004D5E60h
  004e1d47  83c408                add       esp,8
  004e1d4a  663bc3                cmp       ax,bx
  004e1d4d  7515                  jne       short 004E1D64h
  004e1d4f  e8ec530600            call      00547140h
  004e1d54  99                    cdq
  004e1d55  33c2                  xor       eax,edx
  004e1d57  2bc2                  sub       eax,edx
  004e1d59  83e001                and       eax,1
  004e1d5c  33c2                  xor       eax,edx
  004e1d5e  2bc2                  sub       eax,edx
  004e1d60  8d4400ff              lea       eax,[eax+eax-1]

LAB_004e1d64:
  004e1d64  8b542410              mov       edx,[esp+10h]
  004e1d68  83c240                add       edx,40h
  004e1d6b  0fafd0                imul      edx,eax
  004e1d6e  66015632              add       [esi+32h],dx

LAB_004e1d72:
  004e1d72  6aff                  push      0FFFFFFFFh
  004e1d74  6aff                  push      0FFFFFFFFh
  004e1d76  6aff                  push      0FFFFFFFFh
  004e1d78  56                    push      esi
  004e1d79  e8921afeff            call      004C3810h
  004e1d7e  8b442430              mov       eax,[esp+30h]
  004e1d82  83c410                add       esp,10h
  004e1d85  3bc3                  cmp       eax,ebx
  004e1d87  7517                  jne       short 004E1DA0h
  004e1d89  668b842486000000      mov       ax,[esp+86h]
  004e1d91  66894632              mov       [esi+32h],ax
  004e1d95  5f                    pop       edi
  004e1d96  5e                    pop       esi
  004e1d97  5d                    pop       ebp
  004e1d98  5b                    pop       ebx
  004e1d99  81c488000000          add       esp,88h
  004e1d9f  c3                    ret

LAB_004e1da0:
  004e1da0  385e43                cmp       [esi+43h],bl
  004e1da3  0f85c0020000          jne       near 004E2069h
  004e1da9  8b466c                mov       eax,[esi+6Ch]
  004e1dac  8b4e1c                mov       ecx,[esi+1Ch]
  004e1daf  8b5624                mov       edx,[esi+24h]
  004e1db2  8b781c                mov       edi,[eax+1Ch]
  004e1db5  8b6824                mov       ebp,[eax+24h]
  004e1db8  2bf9                  sub       edi,ecx
  004e1dba  2bea                  sub       ebp,edx
  004e1dbc  c1ff0c                sar       edi,0Ch
  004e1dbf  c1fd0c                sar       ebp,0Ch
  004e1dc2  8bcf                  mov       ecx,edi
  004e1dc4  8bd5                  mov       edx,ebp
  004e1dc6  0fafcf                imul      ecx,edi
  004e1dc9  0fafd5                imul      edx,ebp
  004e1dcc  03ca                  add       ecx,edx
  004e1dce  51                    push      ecx
  004e1dcf  e83c3c0600            call      00545A10h
  004e1dd4  55                    push      ebp
  004e1dd5  57                    push      edi
  004e1dd6  8944241c              mov       [esp+1Ch],eax
  004e1dda  e8433c0600            call      00545A22h
  004e1ddf  668b4e32              mov       cx,[esi+32h]
  004e1de3  66895e12              mov       [esi+12h],bx
  004e1de7  2bc1                  sub       eax,ecx
  004e1de9  25ff0f0000            and       eax,0FFFh
  004e1dee  89442418              mov       [esp+18h],eax
  004e1df2  8b442414              mov       eax,[esp+14h]
  004e1df6  3d96000000            cmp       eax,96h
  004e1dfb  0f8def000000          jge       near 004E1EF0h
  004e1e01  e83a530600            call      00547140h
  004e1e06  99                    cdq
  004e1e07  b964000000            mov       ecx,64h
  004e1e0c  f7f9                  idiv      ecx
  004e1e0e  8bea                  mov       ebp,edx
  004e1e10  83fd32                cmp       ebp,32h
  004e1e13  0f8dae000000          jge       near 004E1EC7h
  004e1e19  8b542418              mov       edx,[esp+18h]
  004e1e1d  8b7c241c              mov       edi,[esp+1Ch]
  004e1e21  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004e1e28  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004e1e2f  25ff0f0000            and       eax,0FFFh
  004e1e34  3bc1                  cmp       eax,ecx
  004e1e36  7f55                  jg        short 004E1E8Dh
  004e1e38  e803530600            call      00547140h
  004e1e3d  99                    cdq
  004e1e3e  8d4c3f01              lea       ecx,[edi+edi+1]
  004e1e42  f7f9                  idiv      ecx
  004e1e44  8b442414              mov       eax,[esp+14h]
  004e1e48  83c264                add       edx,64h
  004e1e4b  3bc2                  cmp       eax,edx
  004e1e4d  7d3e                  jge       short 004E1E8Dh
  004e1e4f  83fd1e                cmp       ebp,1Eh
  004e1e52  7d24                  jge       short 004E1E78h
  004e1e54  8b16                  mov       edx,[esi]
  004e1e56  c646420b              mov       byte [esi+42h],0Bh
  004e1e5a  81e2fffffffe          and       edx,0FEFFFFFFh
  004e1e60  c6464301              mov       byte [esi+43h],1
  004e1e64  83ca02                or        edx,2
  004e1e67  895e5c                mov       [esi+5Ch],ebx
  004e1e6a  8916                  mov       [esi],edx
  004e1e6c  66c746180100          mov       word [esi+18h],1
  004e1e72  66895e1a              mov       [esi+1Ah],bx
  004e1e76  eb7c                  jmp       short 004E1EF4h

LAB_004e1e78:
  004e1e78  885e42                mov       [esi+42h],bl
  004e1e7b  e8c0520600            call      00547140h
  004e1e80  99                    cdq
  004e1e81  b928000000            mov       ecx,28h
  004e1e86  f7f9                  idiv      ecx
  004e1e88  885643                mov       [esi+43h],dl
  004e1e8b  eb67                  jmp       short 004E1EF4h

LAB_004e1e8d:
  004e1e8d  c6464203              mov       byte [esi+42h],3
  004e1e91  66895e12              mov       [esi+12h],bx
  004e1e95  e8a6520600            call      00547140h
  004e1e9a  99                    cdq
  004e1e9b  b90a000000            mov       ecx,0Ah
  004e1ea0  f7f9                  idiv      ecx
  004e1ea2  8bc7                  mov       eax,edi
  004e1ea4  d1f8                  sar       eax,1
  004e1ea6  02d0                  add       dl,al
  004e1ea8  80c214                add       dl,14h
  004e1eab  885643                mov       [esi+43h],dl
  004e1eae  e88d520600            call      00547140h
  004e1eb3  99                    cdq
  004e1eb4  33c2                  xor       eax,edx
  004e1eb6  2bc2                  sub       eax,edx
  004e1eb8  25ff0f0000            and       eax,0FFFh
  004e1ebd  33c2                  xor       eax,edx
  004e1ebf  2bc2                  sub       eax,edx
  004e1ec1  66894610              mov       [esi+10h],ax
  004e1ec5  eb2d                  jmp       short 004E1EF4h

LAB_004e1ec7:
  004e1ec7  83fd46                cmp       ebp,46h
  004e1eca  0f8ca4010000          jl        near 004E2074h
  004e1ed0  8b4c2410              mov       ecx,[esp+10h]
  004e1ed4  ba64000000            mov       edx,64h
  004e1ed9  d1f9                  sar       ecx,1
  004e1edb  83c10a                add       ecx,0Ah
  004e1ede  2bd5                  sub       edx,ebp
  004e1ee0  3bd1                  cmp       edx,ecx
  004e1ee2  0f8d8c010000          jge       near 004E2074h
  004e1ee8  c646420c              mov       byte [esi+42h],0Ch
  004e1eec  c6464301              mov       byte [esi+43h],1

LAB_004e1ef0:
  004e1ef0  8b7c241c              mov       edi,[esp+1Ch]

LAB_004e1ef4:
  004e1ef4  8b442414              mov       eax,[esp+14h]
  004e1ef8  3d58020000            cmp       eax,258h
  004e1efd  0f8dc8000000          jge       near 004E1FCBh
  004e1f03  3d96000000            cmp       eax,96h
  004e1f08  0f8cbd000000          jl        near 004E1FCBh
  004e1f0e  e82d520600            call      00547140h
  004e1f13  99                    cdq
  004e1f14  b964000000            mov       ecx,64h
  004e1f19  f7f9                  idiv      ecx
  004e1f1b  83fa14                cmp       edx,14h
  004e1f1e  7d2f                  jge       short 004E1F4Fh
  004e1f20  8b542418              mov       edx,[esp+18h]
  004e1f24  81c200040000          add       edx,400h
  004e1f2a  81e2ff0f0000          and       edx,0FFFh
  004e1f30  81fa00080000          cmp       edx,800h
  004e1f36  7c0d                  jl        short 004E1F45h
  004e1f38  c6464201              mov       byte [esi+42h],1
  004e1f3c  c6464301              mov       byte [esi+43h],1
  004e1f40  e986000000            jmp       004E1FCBh

LAB_004e1f45:
  004e1f45  c646420c              mov       byte [esi+42h],0Ch
  004e1f49  c6464301              mov       byte [esi+43h],1
  004e1f4d  eb7c                  jmp       short 004E1FCBh

LAB_004e1f4f:
  004e1f4f  83fa3c                cmp       edx,3Ch
  004e1f52  7d25                  jge       short 004E1F79h
  004e1f54  c6464202              mov       byte [esi+42h],2
  004e1f58  e8e3510600            call      00547140h
  004e1f5d  99                    cdq
  004e1f5e  b90a000000            mov       ecx,0Ah
  004e1f63  66c746105000          mov       word [esi+10h],50h
  004e1f69  f7f9                  idiv      ecx
  004e1f6b  8bc7                  mov       eax,edi
  004e1f6d  d1f8                  sar       eax,1
  004e1f6f  02d0                  add       dl,al
  004e1f71  80c214                add       dl,14h
  004e1f74  885643                mov       [esi+43h],dl
  004e1f77  eb52                  jmp       short 004E1FCBh

LAB_004e1f79:
  004e1f79  83fa50                cmp       edx,50h
  004e1f7c  7d3a                  jge       short 004E1FB8h
  004e1f7e  c6464203              mov       byte [esi+42h],3
  004e1f82  66895e12              mov       [esi+12h],bx
  004e1f86  e8b5510600            call      00547140h
  004e1f8b  99                    cdq
  004e1f8c  b90a000000            mov       ecx,0Ah
  004e1f91  f7f9                  idiv      ecx
  004e1f93  8bc7                  mov       eax,edi
  004e1f95  d1f8                  sar       eax,1
  004e1f97  02d0                  add       dl,al
  004e1f99  80c214                add       dl,14h
  004e1f9c  885643                mov       [esi+43h],dl
  004e1f9f  e89c510600            call      00547140h
  004e1fa4  99                    cdq
  004e1fa5  33c2                  xor       eax,edx
  004e1fa7  2bc2                  sub       eax,edx
  004e1fa9  25ff0f0000            and       eax,0FFFh
  004e1fae  33c2                  xor       eax,edx
  004e1fb0  2bc2                  sub       eax,edx
  004e1fb2  66894610              mov       [esi+10h],ax
  004e1fb6  eb13                  jmp       short 004E1FCBh

LAB_004e1fb8:
  004e1fb8  885e42                mov       [esi+42h],bl
  004e1fbb  e880510600            call      00547140h
  004e1fc0  99                    cdq
  004e1fc1  b928000000            mov       ecx,28h
  004e1fc6  f7f9                  idiv      ecx
  004e1fc8  885643                mov       [esi+43h],dl

LAB_004e1fcb:
  004e1fcb  817c241458020000      cmp       dword [esp+14h],258h
  004e1fd3  0f8c90000000          jl        near 004E2069h
  004e1fd9  e862510600            call      00547140h
  004e1fde  99                    cdq
  004e1fdf  b964000000            mov       ecx,64h
  004e1fe4  f7f9                  idiv      ecx
  004e1fe6  83fa3c                cmp       edx,3Ch
  004e1fe9  7d2e                  jge       short 004E2019h
  004e1feb  c6464202              mov       byte [esi+42h],2
  004e1fef  e84c510600            call      00547140h
  004e1ff4  99                    cdq
  004e1ff5  b90a000000            mov       ecx,0Ah
  004e1ffa  66c746106400          mov       word [esi+10h],64h
  004e2000  f7f9                  idiv      ecx
  004e2002  8bc7                  mov       eax,edi
  004e2004  d1f8                  sar       eax,1
  004e2006  02d0                  add       dl,al
  004e2008  80c214                add       dl,14h
  004e200b  885643                mov       [esi+43h],dl
  004e200e  5f                    pop       edi
  004e200f  5e                    pop       esi
  004e2010  5d                    pop       ebp
  004e2011  5b                    pop       ebx
  004e2012  81c488000000          add       esp,88h
  004e2018  c3                    ret

LAB_004e2019:
  004e2019  83fa4b                cmp       edx,4Bh
  004e201c  7d43                  jge       short 004E2061h
  004e201e  c6464203              mov       byte [esi+42h],3
  004e2022  66895e12              mov       [esi+12h],bx
  004e2026  e815510600            call      00547140h
  004e202b  99                    cdq
  004e202c  b90a000000            mov       ecx,0Ah
  004e2031  f7f9                  idiv      ecx
  004e2033  8bc7                  mov       eax,edi
  004e2035  d1f8                  sar       eax,1
  004e2037  02d0                  add       dl,al
  004e2039  80c214                add       dl,14h
  004e203c  885643                mov       [esi+43h],dl
  004e203f  e8fc500600            call      00547140h
  004e2044  99                    cdq
  004e2045  33c2                  xor       eax,edx
  004e2047  2bc2                  sub       eax,edx
  004e2049  25ff0f0000            and       eax,0FFFh
  004e204e  33c2                  xor       eax,edx
  004e2050  2bc2                  sub       eax,edx
  004e2052  66894610              mov       [esi+10h],ax
  004e2056  5f                    pop       edi
  004e2057  5e                    pop       esi
  004e2058  5d                    pop       ebp
  004e2059  5b                    pop       ebx
  004e205a  81c488000000          add       esp,88h
  004e2060  c3                    ret

LAB_004e2061:
  004e2061  c6464201              mov       byte [esi+42h],1
  004e2065  c6464301              mov       byte [esi+43h],1

LAB_004e2069:
  004e2069  5f                    pop       edi
  004e206a  5e                    pop       esi
  004e206b  5d                    pop       ebp
  004e206c  5b                    pop       ebx
  004e206d  81c488000000          add       esp,88h
  004e2073  c3                    ret

LAB_004e2074:
  004e2074  c6464203              mov       byte [esi+42h],3
  004e2078  66895e12              mov       [esi+12h],bx
  004e207c  e8bf500600            call      00547140h
  004e2081  99                    cdq
  004e2082  b90a000000            mov       ecx,0Ah
  004e2087  8b7c241c              mov       edi,[esp+1Ch]
  004e208b  f7f9                  idiv      ecx
  004e208d  8bc7                  mov       eax,edi
  004e208f  d1f8                  sar       eax,1
  004e2091  02d0                  add       dl,al
  004e2093  80c214                add       dl,14h
  004e2096  885643                mov       [esi+43h],dl
  004e2099  e8a2500600            call      00547140h
  004e209e  99                    cdq
  004e209f  33c2                  xor       eax,edx
  004e20a1  2bc2                  sub       eax,edx
  004e20a3  25ff0f0000            and       eax,0FFFh
  004e20a8  33c2                  xor       eax,edx
  004e20aa  2bc2                  sub       eax,edx
  004e20ac  66894610              mov       [esi+10h],ax
  004e20b0  e93ffeffff            jmp       004E1EF4h
  004e20b5  90                    nop
  004e20b6  8bff                  mov       edi,edi
  004e20b8  9f                    lahf
  004e20b9  16                    push      ss
  004e20ba  4e                    dec       esi
  004e20bb  00ec                  add       ah,ch
  004e20bd  16                    push      ss
  004e20be  4e                    dec       esi
  004e20bf  006617                add       [esi+17h],ah
  004e20c2  4e                    dec       esi
  004e20c3  000d184e0062          add       [62004E18h],cl
  004e20c9  1b4e00                sbb       ecx,[esi]
  004e20cc  621b                  bound     ebx,[ebx]
  004e20ce  4e                    dec       esi
  004e20cf  00621b                add       [edx+1Bh],ah
  004e20d2  4e                    dec       esi
  004e20d3  00da                  add       dl,bl
  004e20d5  184e00                sbb       [esi],cl
  004e20d8  621b                  bound     ebx,[ebx]
  004e20da  4e                    dec       esi
  004e20db  003b                  add       [ebx],bh
  004e20dd  194e00                sbb       [esi],ecx
  004e20e0  621b                  bound     ebx,[ebx]
  004e20e2  4e                    dec       esi
  004e20e3  00c2                  add       dl,al
  004e20e5  194e00                sbb       [esi],ecx
  004e20e8  261a4e00              sbb       cl,[es:esi]
  004e20ec  90                    nop
  004e20ed  90                    nop
  004e20ee  90                    nop
  004e20ef  90                    nop

57 cible(s) de saut interne : 0x004e13d7, 0x004e151b, 0x004e153c, 0x004e15bb, 0x004e1682, 0x004e168a, 0x004e16c1, 0x004e16d4, 0x004e1712, 0x004e1733, 0x004e1752, 0x004e179a, 0x004e17b2, 0x004e17ba, 0x004e17c6, 0x004e17fb, 0x004e1824, 0x004e18a1, 0x004e18ac, 0x004e18b0, 0x004e18f4, 0x004e1900, 0x004e1975, 0x004e19f4, 0x004e1a70, 0x004e1a98, 0x004e1aa2, 0x004e1aa6, 0x004e1afc, 0x004e1b06, 0x004e1b0a, 0x004e1b1c, 0x004e1b43, 0x004e1b62, 0x004e1b97, 0x004e1bc0, 0x004e1bd7, 0x004e1ca4, 0x004e1ca6, 0x004e1cdb, 0x004e1d64, 0x004e1d72, 0x004e1da0, 0x004e1e78, 0x004e1e8d, 0x004e1ec7, 0x004e1ef0, 0x004e1ef4, 0x004e1f45, 0x004e1f4f, 0x004e1f79, 0x004e1fb8, 0x004e1fcb, 0x004e2019, 0x004e2061, 0x004e2069, 0x004e2074
