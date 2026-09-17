Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004b1660  section .text  offset fichier 0xb0a60  3184 octets

  004b1660  81ec88000000          sub       esp,88h
  004b1666  53                    push      ebx
  004b1667  55                    push      ebp
  004b1668  56                    push      esi
  004b1669  8bb42498000000        mov       esi,[esp+98h]
  004b1670  33ed                  xor       ebp,ebp
  004b1672  57                    push      edi
  004b1673  8a4641                mov       al,[esi+41h]
  004b1676  66896c2428            mov       [esp+28h],bp
  004b167b  84c0                  test      al,al
  004b167d  66896c242a            mov       [esp+2Ah],bp
  004b1682  66c744242c5000        mov       word [esp+2Ch],50h
  004b1689  66c744242e6400        mov       word [esp+2Eh],64h
  004b1690  66896c2430            mov       [esp+30h],bp
  004b1695  66c7442432a000        mov       word [esp+32h],0A0h
  004b169c  752d                  jne       short 004B16CBh
  004b169e  55                    push      ebp
  004b169f  6a01                  push      1
  004b16a1  56                    push      esi
  004b16a2  c6464101              mov       byte [esi+41h],1
  004b16a6  e8b5b1ffff            call      004AC860h
  004b16ab  668b4616              mov       ax,[esi+16h]
  004b16af  83c40c                add       esp,0Ch
  004b16b2  663d0a00              cmp       ax,0Ah
  004b16b6  7c0d                  jl        short 004B16C5h
  004b16b8  83c0f6                add       eax,0FFFFFFF6h
  004b16bb  66894616              mov       [esi+16h],ax
  004b16bf  8b06                  mov       eax,[esi]
  004b16c1  24df                  and       al,0DFh
  004b16c3  eb04                  jmp       short 004B16C9h

LAB_004b16c5:
  004b16c5  8b06                  mov       eax,[esi]
  004b16c7  0c20                  or        al,20h

LAB_004b16c9:
  004b16c9  8906                  mov       [esi],eax

LAB_004b16cb:
  004b16cb  8b0e                  mov       ecx,[esi]
  004b16cd  8b461c                mov       eax,[esi+1Ch]
  004b16d0  8b5664                mov       edx,[esi+64h]
  004b16d3  8d7e1c                lea       edi,[esi+1Ch]
  004b16d6  83e1fe                and       ecx,0FFFFFFFEh
  004b16d9  89442458              mov       [esp+58h],eax
  004b16dd  8b4660                mov       eax,[esi+60h]
  004b16e0  890e                  mov       [esi],ecx
  004b16e2  8b4e24                mov       ecx,[esi+24h]
  004b16e5  89542440              mov       [esp+40h],edx
  004b16e9  8b567c                mov       edx,[esi+7Ch]
  004b16ec  894c2460              mov       [esp+60h],ecx
  004b16f0  668b4e32              mov       cx,[esi+32h]
  004b16f4  89442438              mov       [esp+38h],eax
  004b16f8  668b8690000000        mov       ax,[esi+90h]
  004b16ff  66898c2486000000      mov       [esp+86h],cx
  004b1707  8b8e80000000          mov       ecx,[esi+80h]
  004b170d  8b5e6c                mov       ebx,[esi+6Ch]
  004b1710  89542448              mov       [esp+48h],edx
  004b1714  8b9684000000          mov       edx,[esi+84h]
  004b171a  668944243c            mov       [esp+3Ch],ax
  004b171f  8b8688000000          mov       eax,[esi+88h]
  004b1725  894c244c              mov       [esp+4Ch],ecx
  004b1729  8b8e8c000000          mov       ecx,[esi+8Ch]
  004b172f  89542434              mov       [esp+34h],edx
  004b1733  89442444              mov       [esp+44h],eax
  004b1737  8d542464              lea       edx,[esp+64h]
  004b173b  894c2450              mov       [esp+50h],ecx
  004b173f  8d44246c              lea       eax,[esp+6Ch]
  004b1743  52                    push      edx
  004b1744  8d4c246c              lea       ecx,[esp+6Ch]
  004b1748  50                    push      eax
  004b1749  51                    push      ecx
  004b174a  56                    push      esi
  004b174b  895c2434              mov       [esp+34h],ebx
  004b174f  e82c720800            call      00538980h
  004b1754  83c410                add       esp,10h
  004b1757  8d542454              lea       edx,[esp+54h]
  004b175b  8d44241c              lea       eax,[esp+1Ch]
  004b175f  8d4c2418              lea       ecx,[esp+18h]
  004b1763  52                    push      edx
  004b1764  50                    push      eax
  004b1765  8d54241c              lea       edx,[esp+1Ch]
  004b1769  51                    push      ecx
  004b176a  8d44241c              lea       eax,[esp+1Ch]
  004b176e  52                    push      edx
  004b176f  50                    push      eax
  004b1770  56                    push      esi
  004b1771  e8aa470800            call      00535F20h
  004b1776  8b4c2428              mov       ecx,[esp+28h]
  004b177a  b840000000            mov       eax,40h
  004b177f  83c418                add       esp,18h
  004b1782  2bc1                  sub       eax,ecx
  004b1784  8b4c2454              mov       ecx,[esp+54h]
  004b1788  89442420              mov       [esp+20h],eax
  004b178c  51                    push      ecx
  004b178d  56                    push      esi
  004b178e  e84d460200            call      004D5DE0h
  004b1793  8b07                  mov       eax,[edi]
  004b1795  8b531c                mov       edx,[ebx+1Ch]
  004b1798  8b4e24                mov       ecx,[esi+24h]
  004b179b  2bc2                  sub       eax,edx
  004b179d  8b5324                mov       edx,[ebx+24h]
  004b17a0  83c408                add       esp,8
  004b17a3  c1f80c                sar       eax,0Ch
  004b17a6  2bca                  sub       ecx,edx
  004b17a8  8bd0                  mov       edx,eax
  004b17aa  c1f90c                sar       ecx,0Ch
  004b17ad  0fafd0                imul      edx,eax
  004b17b0  8bc1                  mov       eax,ecx
  004b17b2  0fafc1                imul      eax,ecx
  004b17b5  03d0                  add       edx,eax
  004b17b7  52                    push      edx
  004b17b8  e853420900            call      00545A10h
  004b17bd  8b4c241c              mov       ecx,[esp+1Ch]
  004b17c1  89442414              mov       [esp+14h],eax
  004b17c5  3bcd                  cmp       ecx,ebp
  004b17c7  0f84ad050000          je        near 004B1D7Ah
  004b17cd  8b16                  mov       edx,[esi]
  004b17cf  33c0                  xor       eax,eax
  004b17d1  8a4604                mov       al,[esi+4]
  004b17d4  81e2fffffffd          and       edx,0FDFFFFFFh
  004b17da  0fbf5e18              movsx     ebx,word [esi+18h]
  004b17de  8916                  mov       [esi],edx
  004b17e0  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004b17e7  f7c200000001          test      edx,1000000h
  004b17ed  8b2c98                mov       ebp,[eax+ebx*4]
  004b17f0  741d                  je        short 004B180Fh
  004b17f2  66ff461a              inc       word [esi+1Ah]
  004b17f6  668b461a              mov       ax,[esi+1Ah]
  004b17fa  33d2                  xor       edx,edx
  004b17fc  668b5516              mov       dx,[ebp+16h]
  004b1800  0fbfc0                movsx     eax,ax
  004b1803  3bc2                  cmp       eax,edx
  004b1805  7c2a                  jl        short 004B1831h
  004b1807  66c7461a0000          mov       word [esi+1Ah],0
  004b180d  eb22                  jmp       short 004B1831h

LAB_004b180f:
  004b180f  66ff461a              inc       word [esi+1Ah]
  004b1813  668b461a              mov       ax,[esi+1Ah]
  004b1817  33db                  xor       ebx,ebx
  004b1819  668b5d16              mov       bx,[ebp+16h]
  004b181d  0fbfe8                movsx     ebp,ax
  004b1820  3beb                  cmp       ebp,ebx
  004b1822  7c0d                  jl        short 004B1831h
  004b1824  48                    dec       eax
  004b1825  81ca00000002          or        edx,2000000h
  004b182b  6689461a              mov       [esi+1Ah],ax
  004b182f  8916                  mov       [esi],edx

LAB_004b1831:
  004b1831  8a4642                mov       al,[esi+42h]
  004b1834  bb05000000            mov       ebx,5
  004b1839  3ac3                  cmp       al,bl
  004b183b  8b06                  mov       eax,[esi]
  004b183d  7504                  jne       short 004B1843h
  004b183f  0c10                  or        al,10h
  004b1841  eb02                  jmp       short 004B1845h

LAB_004b1843:
  004b1843  24ef                  and       al,0EFh

LAB_004b1845:
  004b1845  8906                  mov       [esi],eax
  004b1847  8a06                  mov       al,[esi]
  004b1849  a802                  test      al,2
  004b184b  7478                  je        short 004B18C5h
  004b184d  66395e1a              cmp       [esi+1Ah],bx
  004b1851  7559                  jne       short 004B18ACh
  004b1853  8d4c2428              lea       ecx,[esp+28h]
  004b1857  6a00                  push      0
  004b1859  51                    push      ecx
  004b185a  57                    push      edi
  004b185b  56                    push      esi
  004b185c  e8cf830600            call      00519C30h
  004b1861  8be8                  mov       ebp,eax
  004b1863  a1b8e05d00            mov       eax,[5DE0B8h]
  004b1868  83c410                add       esp,10h
  004b186b  85c0                  test      eax,eax
  004b186d  7439                  je        short 004B18A8h
  004b186f  66395e1a              cmp       [esi+1Ah],bx
  004b1873  7533                  jne       short 004B18A8h
  004b1875  8b4500                mov       eax,[ebp]
  004b1878  55                    push      ebp
  004b1879  0c40                  or        al,40h
  004b187b  56                    push      esi
  004b187c  66c785960000001000    mov       word [ebp+96h],10h
  004b1885  894500                mov       [ebp],eax
  004b1888  e8f3820800            call      00539B80h
  004b188d  83c408                add       esp,8
  004b1890  83c51c                add       ebp,1Ch
  004b1893  55                    push      ebp
  004b1894  6800180000            push      1800h
  004b1899  6800030000            push      300h
  004b189e  6a03                  push      3
  004b18a0  e87bc20800            call      0053DB20h
  004b18a5  83c410                add       esp,10h

LAB_004b18a8:
  004b18a8  8b4c241c              mov       ecx,[esp+1Ch]

LAB_004b18ac:
  004b18ac  8b16                  mov       edx,[esi]
  004b18ae  f7c200000002          test      edx,2000000h
  004b18b4  740f                  je        short 004B18C5h
  004b18b6  807e420a              cmp       byte [esi+42h],0Ah
  004b18ba  7409                  je        short 004B18C5h
  004b18bc  83e2fd                and       edx,0FFFFFFFDh
  004b18bf  c6464300              mov       byte [esi+43h],0
  004b18c3  8916                  mov       [esi],edx

LAB_004b18c5:
  004b18c5  33c0                  xor       eax,eax
  004b18c7  8a4642                mov       al,[esi+42h]
  004b18ca  83f80b                cmp       eax,0Bh
  004b18cd  0f8725040000          ja        near 004B1CF8h
  004b18d3  ff248594224b00        jmp       dword [eax*4+4B2294h]
  004b18da  33c9                  xor       ecx,ecx
  004b18dc  66394e12              cmp       [esi+12h],cx
  004b18e0  751a                  jne       short 004B18FCh
  004b18e2  8b06                  mov       eax,[esi]
  004b18e4  66894e18              mov       [esi+18h],cx
  004b18e8  0d00000001            or        eax,1000000h
  004b18ed  66894e1a              mov       [esi+1Ah],cx
  004b18f1  8906                  mov       [esi],eax
  004b18f3  66c746120100          mov       word [esi+12h],1
  004b18f9  894e5c                mov       [esi+5Ch],ecx

LAB_004b18fc:
  004b18fc  0fbf5610              movsx     edx,word [esi+10h]
  004b1900  39542414              cmp       [esp+14h],edx
  004b1904  7d09                  jge       short 004B190Fh
  004b1906  c6464300              mov       byte [esi+43h],0
  004b190a  e9e9030000            jmp       004B1CF8h

LAB_004b190f:
  004b190f  8a4643                mov       al,[esi+43h]
  004b1912  84c0                  test      al,al
  004b1914  0f8455020000          je        near 004B1B6Fh
  004b191a  fec8                  dec       al
  004b191c  894e5c                mov       [esi+5Ch],ecx
  004b191f  884643                mov       [esi+43h],al
  004b1922  e9d1030000            jmp       004B1CF8h
  004b1927  33c9                  xor       ecx,ecx
  004b1929  66394e12              cmp       [esi+12h],cx
  004b192d  7517                  jne       short 004B1946h
  004b192f  8b06                  mov       eax,[esi]
  004b1931  66894e18              mov       [esi+18h],cx
  004b1935  0d00000001            or        eax,1000000h
  004b193a  66894e1a              mov       [esi+1Ah],cx
  004b193e  8906                  mov       [esi],eax
  004b1940  66c746120100          mov       word [esi+12h],1

LAB_004b1946:
  004b1946  8b442418              mov       eax,[esp+18h]
  004b194a  8d90c8000000          lea       edx,[eax+0C8h]
  004b1950  81e2ff0f0000          and       edx,0FFFh
  004b1956  81fa90010000          cmp       edx,190h
  004b195c  7f0c                  jg        short 004B196Ah
  004b195e  c6464300              mov       byte [esi+43h],0
  004b1962  894e5c                mov       [esi+5Ch],ecx
  004b1965  e98e030000            jmp       004B1CF8h

LAB_004b196a:
  004b196a  25ff0f0000            and       eax,0FFFh
  004b196f  3d00080000            cmp       eax,800h
  004b1974  7d15                  jge       short 004B198Bh
  004b1976  8b442410              mov       eax,[esp+10h]
  004b197a  894e5c                mov       [esi+5Ch],ecx
  004b197d  0580000000            add       eax,80h
  004b1982  66014632              add       [esi+32h],ax
  004b1986  e96d030000            jmp       004B1CF8h

LAB_004b198b:
  004b198b  8b542410              mov       edx,[esp+10h]
  004b198f  b880ffffff            mov       eax,0FFFFFF80h
  004b1994  2bc2                  sub       eax,edx
  004b1996  894e5c                mov       [esi+5Ch],ecx
  004b1999  66014632              add       [esi+32h],ax
  004b199d  e956030000            jmp       004B1CF8h
  004b19a2  8b4c2410              mov       ecx,[esp+10h]
  004b19a6  33c0                  xor       eax,eax
  004b19a8  66394612              cmp       [esi+12h],ax
  004b19ac  7527                  jne       short 004B19D5h
  004b19ae  8b2e                  mov       ebp,[esi]
  004b19b0  8d14c9                lea       edx,[ecx+ecx*8]
  004b19b3  c1e204                shl       edx,4
  004b19b6  81cd00000001          or        ebp,1000000h
  004b19bc  81c200500000          add       edx,5000h
  004b19c2  66894618              mov       [esi+18h],ax
  004b19c6  6689461a              mov       [esi+1Ah],ax
  004b19ca  892e                  mov       [esi],ebp
  004b19cc  66c746120100          mov       word [esi+12h],1
  004b19d2  89565c                mov       [esi+5Ch],edx

LAB_004b19d5:
  004b19d5  66837e1201            cmp       word [esi+12h],1
  004b19da  0f8518030000          jne       near 004B1CF8h
  004b19e0  0fbf5610              movsx     edx,word [esi+10h]
  004b19e4  39542414              cmp       [esp+14h],edx
  004b19e8  7d0c                  jge       short 004B19F6h
  004b19ea  c6464300              mov       byte [esi+43h],0
  004b19ee  89465c                mov       [esi+5Ch],eax
  004b19f1  e902030000            jmp       004B1CF8h

LAB_004b19f6:
  004b19f6  8a4643                mov       al,[esi+43h]
  004b19f9  84c0                  test      al,al
  004b19fb  7405                  je        short 004B1A02h
  004b19fd  fec8                  dec       al
  004b19ff  884643                mov       [esi+43h],al

LAB_004b1a02:
  004b1a02  8b442418              mov       eax,[esp+18h]
  004b1a06  8d9000010000          lea       edx,[eax+100h]
  004b1a0c  81e2ff0f0000          and       edx,0FFFh
  004b1a12  81fa00020000          cmp       edx,200h
  004b1a18  0f8eda020000          jle       near 004B1CF8h
  004b1a1e  25ff0f0000            and       eax,0FFFh
  004b1a23  3d00080000            cmp       eax,800h
  004b1a28  7d0d                  jge       short 004B1A37h
  004b1a2a  8d44091e              lea       eax,[ecx+ecx+1Eh]
  004b1a2e  66014632              add       [esi+32h],ax
  004b1a32  e9c1020000            jmp       004B1CF8h

LAB_004b1a37:
  004b1a37  03c9                  add       ecx,ecx
  004b1a39  b8e2ffffff            mov       eax,0FFFFFFE2h
  004b1a3e  2bc1                  sub       eax,ecx
  004b1a40  66014632              add       [esi+32h],ax
  004b1a44  e9af020000            jmp       004B1CF8h
  004b1a49  66837e1201            cmp       word [esi+12h],1
  004b1a4e  0f859c000000          jne       near 004B1AF0h
  004b1a54  8a4643                mov       al,[esi+43h]
  004b1a57  84c0                  test      al,al
  004b1a59  7405                  je        short 004B1A60h
  004b1a5b  fec8                  dec       al
  004b1a5d  884643                mov       [esi+43h],al

LAB_004b1a60:
  004b1a60  8b442410              mov       eax,[esp+10h]
  004b1a64  8d14c0                lea       edx,[eax+eax*8]
  004b1a67  c1e204                shl       edx,4
  004b1a6a  8d8200500000          lea       eax,[edx+5000h]
  004b1a70  0fafc8                imul      ecx,eax
  004b1a73  89465c                mov       [esi+5Ch],eax
  004b1a76  0fbf4610              movsx     eax,word [esi+10h]
  004b1a7a  c1f90c                sar       ecx,0Ch
  004b1a7d  50                    push      eax
  004b1a7e  8be9                  mov       ebp,ecx
  004b1a80  e8973f0900            call      00545A1Ch
  004b1a85  0fafc5                imul      eax,ebp
  004b1a88  8b0f                  mov       ecx,[edi]
  004b1a8a  c1f80c                sar       eax,0Ch
  004b1a8d  03c8                  add       ecx,eax
  004b1a8f  890f                  mov       [edi],ecx
  004b1a91  0fbf4e10              movsx     ecx,word [esi+10h]
  004b1a95  51                    push      ecx
  004b1a96  e87b3f0900            call      00545A16h
  004b1a9b  0fafc5                imul      eax,ebp
  004b1a9e  8b5e24                mov       ebx,[esi+24h]
  004b1aa1  c7465c00000000        mov       dword [esi+5Ch],0
  004b1aa8  c1f80c                sar       eax,0Ch
  004b1aab  03d8                  add       ebx,eax
  004b1aad  8b442418              mov       eax,[esp+18h]
  004b1ab1  895e24                mov       [esi+24h],ebx
  004b1ab4  8d9000010000          lea       edx,[eax+100h]
  004b1aba  81e2ff0f0000          and       edx,0FFFh
  004b1ac0  81fa00020000          cmp       edx,200h
  004b1ac6  7e28                  jle       short 004B1AF0h
  004b1ac8  25ff0f0000            and       eax,0FFFh
  004b1acd  3d00080000            cmp       eax,800h
  004b1ad2  7d0a                  jge       short 004B1ADEh
  004b1ad4  8b442410              mov       eax,[esp+10h]
  004b1ad8  8d44001e              lea       eax,[eax+eax+1Eh]
  004b1adc  eb0e                  jmp       short 004B1AECh

LAB_004b1ade:
  004b1ade  8b4c2410              mov       ecx,[esp+10h]
  004b1ae2  b8e2ffffff            mov       eax,0FFFFFFE2h
  004b1ae7  8d1409                lea       edx,[ecx+ecx]
  004b1aea  2bc2                  sub       eax,edx

LAB_004b1aec:
  004b1aec  66014632              add       [esi+32h],ax

LAB_004b1af0:
  004b1af0  66837e1200            cmp       word [esi+12h],0
  004b1af5  0f85fd010000          jne       near 004B1CF8h
  004b1afb  33c0                  xor       eax,eax
  004b1afd  66c746120100          mov       word [esi+12h],1
  004b1b03  66894618              mov       [esi+18h],ax
  004b1b07  6689461a              mov       [esi+1Ah],ax
  004b1b0b  810e00000001          or        dword [esi],1000000h
  004b1b11  e9e2010000            jmp       004B1CF8h
  004b1b16  668b4e12              mov       cx,[esi+12h]
  004b1b1a  6683f901              cmp       cx,1
  004b1b1e  751c                  jne       short 004B1B3Ch
  004b1b20  0fbf4610              movsx     eax,word [esi+10h]
  004b1b24  39442414              cmp       [esp+14h],eax
  004b1b28  7d06                  jge       short 004B1B30h
  004b1b2a  c6464300              mov       byte [esi+43h],0
  004b1b2e  eb0c                  jmp       short 004B1B3Ch

LAB_004b1b30:
  004b1b30  8a4643                mov       al,[esi+43h]
  004b1b33  84c0                  test      al,al
  004b1b35  7405                  je        short 004B1B3Ch
  004b1b37  fec8                  dec       al
  004b1b39  884643                mov       [esi+43h],al

LAB_004b1b3c:
  004b1b3c  33c0                  xor       eax,eax
  004b1b3e  663bc8                cmp       cx,ax
  004b1b41  0f85b1010000          jne       near 004B1CF8h
  004b1b47  8b3e                  mov       edi,[esi]
  004b1b49  66894618              mov       [esi+18h],ax
  004b1b4d  6689461a              mov       [esi+1Ah],ax
  004b1b51  8b442410              mov       eax,[esp+10h]
  004b1b55  81cf00000001          or        edi,1000000h
  004b1b5b  66c746120100          mov       word [esi+12h],1
  004b1b61  8d0cc0                lea       ecx,[eax+eax*8]
  004b1b64  893e                  mov       [esi],edi
  004b1b66  c1e104                shl       ecx,4
  004b1b69  81c100500000          add       ecx,5000h

LAB_004b1b6f:
  004b1b6f  894e5c                mov       [esi+5Ch],ecx
  004b1b72  e981010000            jmp       004B1CF8h
  004b1b77  66837e1201            cmp       word [esi+12h],1
  004b1b7c  8b5c2410              mov       ebx,[esp+10h]
  004b1b80  8b4c2418              mov       ecx,[esp+18h]
  004b1b84  757b                  jne       short 004B1C01h
  004b1b86  8b4620                mov       eax,[esi+20h]
  004b1b89  8b6e60                mov       ebp,[esi+60h]
  004b1b8c  0500500000            add       eax,5000h
  004b1b91  894620                mov       [esi+20h],eax
  004b1b94  2bc5                  sub       eax,ebp
  004b1b96  c1f80c                sar       eax,0Ch
  004b1b99  3dc8000000            cmp       eax,0C8h
  004b1b9e  7e23                  jle       short 004B1BC3h
  004b1ba0  8b16                  mov       edx,[esi]
  004b1ba2  33ed                  xor       ebp,ebp
  004b1ba4  81e2fffffffe          and       edx,0FEFFFFFFh
  004b1baa  66c746120200          mov       word [esi+12h],2
  004b1bb0  83ca02                or        edx,2
  004b1bb3  66896e14              mov       [esi+14h],bp
  004b1bb7  66896e18              mov       [esi+18h],bp
  004b1bbb  66896e1a              mov       [esi+1Ah],bp
  004b1bbf  8916                  mov       [esi],edx
  004b1bc1  eb02                  jmp       short 004B1BC5h

LAB_004b1bc3:
  004b1bc3  33ed                  xor       ebp,ebp

LAB_004b1bc5:
  004b1bc5  8d8100010000          lea       eax,[ecx+100h]
  004b1bcb  25ff0f0000            and       eax,0FFFh
  004b1bd0  3d00020000            cmp       eax,200h
  004b1bd5  7e2c                  jle       short 004B1C03h
  004b1bd7  8bd1                  mov       edx,ecx
  004b1bd9  81e2ff0f0000          and       edx,0FFFh
  004b1bdf  81fa00080000          cmp       edx,800h
  004b1be5  7d0a                  jge       short 004B1BF1h
  004b1be7  8d441b1e              lea       eax,[ebx+ebx+1Eh]
  004b1beb  66014632              add       [esi+32h],ax
  004b1bef  eb12                  jmp       short 004B1C03h

LAB_004b1bf1:
  004b1bf1  8d141b                lea       edx,[ebx+ebx]
  004b1bf4  b8e2ffffff            mov       eax,0FFFFFFE2h
  004b1bf9  2bc2                  sub       eax,edx
  004b1bfb  66014632              add       [esi+32h],ax
  004b1bff  eb02                  jmp       short 004B1C03h

LAB_004b1c01:
  004b1c01  33ed                  xor       ebp,ebp

LAB_004b1c03:
  004b1c03  668b7e12              mov       di,[esi+12h]
  004b1c07  6683ff02              cmp       di,2
  004b1c0b  7573                  jne       short 004B1C80h
  004b1c0d  8b542424              mov       edx,[esp+24h]
  004b1c11  8b4620                mov       eax,[esi+20h]
  004b1c14  c7465c00e00100        mov       dword [esi+5Ch],1E000h
  004b1c1b  8b5220                mov       edx,[edx+20h]
  004b1c1e  2bd0                  sub       edx,eax
  004b1c20  81c200800200          add       edx,28000h
  004b1c26  c1fa02                sar       edx,2
  004b1c29  03d0                  add       edx,eax
  004b1c2b  66ff4614              inc       word [esi+14h]
  004b1c2f  895620                mov       [esi+20h],edx
  004b1c32  668b5614              mov       dx,[esi+14h]
  004b1c36  6683fa14              cmp       dx,14h
  004b1c3a  7d34                  jge       short 004B1C70h
  004b1c3c  8d8100010000          lea       eax,[ecx+100h]
  004b1c42  25ff0f0000            and       eax,0FFFh
  004b1c47  3d00020000            cmp       eax,200h
  004b1c4c  7e22                  jle       short 004B1C70h
  004b1c4e  81e1ff0f0000          and       ecx,0FFFh
  004b1c54  81f900080000          cmp       ecx,800h
  004b1c5a  7d06                  jge       short 004B1C62h
  004b1c5c  8d441b1e              lea       eax,[ebx+ebx+1Eh]
  004b1c60  eb0a                  jmp       short 004B1C6Ch

LAB_004b1c62:
  004b1c62  8d0c1b                lea       ecx,[ebx+ebx]
  004b1c65  b8e2ffffff            mov       eax,0FFFFFFE2h
  004b1c6a  2bc1                  sub       eax,ecx

LAB_004b1c6c:
  004b1c6c  66014632              add       [esi+32h],ax

LAB_004b1c70:
  004b1c70  6683fa21              cmp       dx,21h
  004b1c74  7c0a                  jl        short 004B1C80h
  004b1c76  8b06                  mov       eax,[esi]
  004b1c78  c6464300              mov       byte [esi+43h],0
  004b1c7c  24fd                  and       al,0FDh
  004b1c7e  8906                  mov       [esi],eax

LAB_004b1c80:
  004b1c80  663bfd                cmp       di,bp
  004b1c83  7573                  jne       short 004B1CF8h
  004b1c85  8b0e                  mov       ecx,[esi]
  004b1c87  b801000000            mov       eax,1
  004b1c8c  81c900000001          or        ecx,1000000h
  004b1c92  66396e16              cmp       [esi+16h],bp
  004b1c96  66894612              mov       [esi+12h],ax
  004b1c9a  66894618              mov       [esi+18h],ax
  004b1c9e  66896e1a              mov       [esi+1Ah],bp
  004b1ca2  896e5c                mov       [esi+5Ch],ebp
  004b1ca5  890e                  mov       [esi],ecx
  004b1ca7  754f                  jne       short 004B1CF8h
  004b1ca9  c6464300              mov       byte [esi+43h],0
  004b1cad  eb49                  jmp       short 004B1CF8h
  004b1caf  8b542424              mov       edx,[esp+24h]
  004b1cb3  8b4620                mov       eax,[esi+20h]
  004b1cb6  8b4a20                mov       ecx,[edx+20h]
  004b1cb9  2bc8                  sub       ecx,eax
  004b1cbb  81c100800200          add       ecx,28000h
  004b1cc1  c1f902                sar       ecx,2
  004b1cc4  03c8                  add       ecx,eax
  004b1cc6  894e20                mov       [esi+20h],ecx
  004b1cc9  668b4e12              mov       cx,[esi+12h]
  004b1ccd  6683f901              cmp       cx,1
  004b1cd1  7511                  jne       short 004B1CE4h
  004b1cd3  8b06                  mov       eax,[esi]
  004b1cd5  a900000002            test      eax,2000000h
  004b1cda  7408                  je        short 004B1CE4h
  004b1cdc  24fd                  and       al,0FDh
  004b1cde  c6464300              mov       byte [esi+43h],0
  004b1ce2  8906                  mov       [esi],eax

LAB_004b1ce4:
  004b1ce4  6685c9                test      cx,cx
  004b1ce7  750f                  jne       short 004B1CF8h
  004b1ce9  8b06                  mov       eax,[esi]
  004b1ceb  66c746120100          mov       word [esi+12h],1
  004b1cf1  25fffffffe            and       eax,0FEFFFFFFh
  004b1cf6  8906                  mov       [esi],eax

LAB_004b1cf8:
  004b1cf8  807e420a              cmp       byte [esi+42h],0Ah
  004b1cfc  7505                  jne       short 004B1D03h
  004b1cfe  f60602                test      byte [esi],2
  004b1d01  7477                  je        short 004B1D7Ah

LAB_004b1d03:
  004b1d03  66814616c800          add       word [esi+16h],0C8h
  004b1d09  668b4616              mov       ax,[esi+16h]
  004b1d0d  663d0010              cmp       ax,1000h
  004b1d11  7c09                  jl        short 004B1D1Ch
  004b1d13  0500f0ffff            add       eax,0FFFFF000h
  004b1d18  66894616              mov       [esi+16h],ax

LAB_004b1d1c:
  004b1d1c  8b5c2424              mov       ebx,[esp+24h]
  004b1d20  8b4660                mov       eax,[esi+60h]
  004b1d23  8b7e20                mov       edi,[esi+20h]
  004b1d26  8b5360                mov       edx,[ebx+60h]
  004b1d29  3bd0                  cmp       edx,eax
  004b1d2b  7e28                  jle       short 004B1D55h
  004b1d2d  0fbf4616              movsx     eax,word [esi+16h]
  004b1d31  50                    push      eax
  004b1d32  e8e53c0900            call      00545A1Ch
  004b1d37  0500600000            add       eax,6000h
  004b1d3c  8b6b60                mov       ebp,[ebx+60h]
  004b1d3f  8d0440                lea       eax,[eax+eax*2]
  004b1d42  8d0c80                lea       ecx,[eax+eax*4]
  004b1d45  d1e1                  shl       ecx,1
  004b1d47  2bcf                  sub       ecx,edi
  004b1d49  03cd                  add       ecx,ebp
  004b1d4b  c1f903                sar       ecx,3
  004b1d4e  03cf                  add       ecx,edi
  004b1d50  894e20                mov       [esi+20h],ecx
  004b1d53  eb25                  jmp       short 004B1D7Ah

LAB_004b1d55:
  004b1d55  0fbf5616              movsx     edx,word [esi+16h]
  004b1d59  52                    push      edx
  004b1d5a  e8bd3c0900            call      00545A1Ch
  004b1d5f  8b4e60                mov       ecx,[esi+60h]
  004b1d62  0500600000            add       eax,6000h
  004b1d67  8d0440                lea       eax,[eax+eax*2]
  004b1d6a  8d0480                lea       eax,[eax+eax*4]
  004b1d6d  8d1441                lea       edx,[ecx+eax*2]
  004b1d70  2bd7                  sub       edx,edi
  004b1d72  c1fa03                sar       edx,3
  004b1d75  03d7                  add       edx,edi
  004b1d77  895620                mov       [esi+20h],edx

LAB_004b1d7a:
  004b1d7a  0fbf4632              movsx     eax,word [esi+32h]
  004b1d7e  50                    push      eax
  004b1d7f  e8983c0900            call      00545A1Ch
  004b1d84  0fbf4e32              movsx     ecx,word [esi+32h]
  004b1d88  51                    push      ecx
  004b1d89  89442474              mov       [esp+74h],eax
  004b1d8d  e8843c0900            call      00545A16h
  004b1d92  8b5650                mov       edx,[esi+50h]
  004b1d95  89442478              mov       [esp+78h],eax
  004b1d99  8bc2                  mov       eax,edx
  004b1d9b  8b4c241c              mov       ecx,[esp+1Ch]
  004b1d9f  c1f808                sar       eax,8
  004b1da2  0faf4e5c              imul      ecx,[esi+5Ch]
  004b1da6  8bfa                  mov       edi,edx
  004b1da8  56                    push      esi
  004b1da9  2bf8                  sub       edi,eax
  004b1dab  c1fa03                sar       edx,3
  004b1dae  2bfa                  sub       edi,edx
  004b1db0  8b5654                mov       edx,[esi+54h]
  004b1db3  8bc2                  mov       eax,edx
  004b1db5  8bea                  mov       ebp,edx
  004b1db7  c1f803                sar       eax,3
  004b1dba  2be8                  sub       ebp,eax
  004b1dbc  897e50                mov       [esi+50h],edi
  004b1dbf  c1fa08                sar       edx,8
  004b1dc2  2bea                  sub       ebp,edx
  004b1dc4  8b5658                mov       edx,[esi+58h]
  004b1dc7  8bda                  mov       ebx,edx
  004b1dc9  8bc2                  mov       eax,edx
  004b1dcb  c1fb03                sar       ebx,3
  004b1dce  2bc3                  sub       eax,ebx
  004b1dd0  8b5e20                mov       ebx,[esi+20h]
  004b1dd3  c1fa08                sar       edx,8
  004b1dd6  c1f90c                sar       ecx,0Ch
  004b1dd9  2bc2                  sub       eax,edx
  004b1ddb  8bd1                  mov       edx,ecx
  004b1ddd  0faf542474            imul      edx,[esp+74h]
  004b1de2  0faf4c247c            imul      ecx,[esp+7Ch]
  004b1de7  c1fa0c                sar       edx,0Ch
  004b1dea  c1f90c                sar       ecx,0Ch
  004b1ded  894658                mov       [esi+58h],eax
  004b1df0  03d7                  add       edx,edi
  004b1df2  8b7e1c                mov       edi,[esi+1Ch]
  004b1df5  03c8                  add       ecx,eax
  004b1df7  8b4624                mov       eax,[esi+24h]
  004b1dfa  03fa                  add       edi,edx
  004b1dfc  03dd                  add       ebx,ebp
  004b1dfe  03c1                  add       eax,ecx
  004b1e00  896e54                mov       [esi+54h],ebp
  004b1e03  897e1c                mov       [esi+1Ch],edi
  004b1e06  895e20                mov       [esi+20h],ebx
  004b1e09  894624                mov       [esi+24h],eax
  004b1e0c  e82f1b0800            call      00533940h
  004b1e11  8bbe90000000          mov       edi,[esi+90h]
  004b1e17  8b06                  mov       eax,[esi]
  004b1e19  83c404                add       esp,4
  004b1e1c  81e7fff8ffff          and       edi,0FFFFF8FFh
  004b1e22  a820                  test      al,20h
  004b1e24  89be90000000          mov       [esi+90h],edi
  004b1e2a  8bcf                  mov       ecx,edi
  004b1e2c  740c                  je        short 004B1E3Ah
  004b1e2e  81e1ffdfffdf          and       ecx,0DFFFDFFFh
  004b1e34  898e90000000          mov       [esi+90h],ecx

LAB_004b1e3a:
  004b1e3a  8b4e60                mov       ecx,[esi+60h]
  004b1e3d  8b5620                mov       edx,[esi+20h]
  004b1e40  3bd1                  cmp       edx,ecx
  004b1e42  7f19                  jg        short 004B1E5Dh
  004b1e44  8b9690000000          mov       edx,[esi+90h]
  004b1e4a  f6c680                test      dh,80h
  004b1e4d  750e                  jne       short 004B1E5Dh
  004b1e4f  24fe                  and       al,0FEh
  004b1e51  33db                  xor       ebx,ebx
  004b1e53  894e20                mov       [esi+20h],ecx
  004b1e56  8906                  mov       [esi],eax
  004b1e58  895e54                mov       [esi+54h],ebx
  004b1e5b  eb02                  jmp       short 004B1E5Fh

LAB_004b1e5d:
  004b1e5d  33db                  xor       ebx,ebx

LAB_004b1e5f:
  004b1e5f  f7869000000082a80020  test      dword [esi+90h],2000A882h
  004b1e69  0f84c0000000          je        near 004B1F2Fh
  004b1e6f  8a4e42                mov       cl,[esi+42h]
  004b1e72  b007                  mov       al,7
  004b1e74  3ac8                  cmp       cl,al
  004b1e76  7420                  je        short 004B1E98h
  004b1e78  884642                mov       [esi+42h],al
  004b1e7b  e8c0520900            call      00547140h
  004b1e80  99                    cdq
  004b1e81  b928000000            mov       ecx,28h
  004b1e86  66895e12              mov       [esi+12h],bx
  004b1e8a  f7f9                  idiv      ecx
  004b1e8c  66c746105000          mov       word [esi+10h],50h
  004b1e92  80c214                add       dl,14h
  004b1e95  885643                mov       [esi+43h],dl

LAB_004b1e98:
  004b1e98  8b16                  mov       edx,[esi]
  004b1e9a  8b442460              mov       eax,[esp+60h]
  004b1e9e  8b4c2448              mov       ecx,[esp+48h]
  004b1ea2  83e2fd                and       edx,0FFFFFFFDh
  004b1ea5  894624                mov       [esi+24h],eax
  004b1ea8  8b44243c              mov       eax,[esp+3Ch]
  004b1eac  8916                  mov       [esi],edx
  004b1eae  8b542458              mov       edx,[esp+58h]
  004b1eb2  25ffff0000            and       eax,0FFFFh
  004b1eb7  89561c                mov       [esi+1Ch],edx
  004b1eba  8b54244c              mov       edx,[esp+4Ch]
  004b1ebe  894e7c                mov       [esi+7Ch],ecx
  004b1ec1  8b4c2444              mov       ecx,[esp+44h]
  004b1ec5  898690000000          mov       [esi+90h],eax
  004b1ecb  8b442434              mov       eax,[esp+34h]
  004b1ecf  899680000000          mov       [esi+80h],edx
  004b1ed5  8b542450              mov       edx,[esp+50h]
  004b1ed9  898684000000          mov       [esi+84h],eax
  004b1edf  8b442440              mov       eax,[esp+40h]
  004b1ee3  898e88000000          mov       [esi+88h],ecx
  004b1ee9  8b4c2438              mov       ecx,[esp+38h]
  004b1eed  6882a80020            push      2000A882h
  004b1ef2  56                    push      esi
  004b1ef3  89968c000000          mov       [esi+8Ch],edx
  004b1ef9  894664                mov       [esi+64h],eax
  004b1efc  894e60                mov       [esi+60h],ecx
  004b1eff  e85c3f0200            call      004D5E60h
  004b1f04  83c408                add       esp,8
  004b1f07  663bc3                cmp       ax,bx
  004b1f0a  7515                  jne       short 004B1F21h
  004b1f0c  e82f520900            call      00547140h
  004b1f11  99                    cdq
  004b1f12  33c2                  xor       eax,edx
  004b1f14  2bc2                  sub       eax,edx
  004b1f16  83e001                and       eax,1
  004b1f19  33c2                  xor       eax,edx
  004b1f1b  2bc2                  sub       eax,edx
  004b1f1d  8d4400ff              lea       eax,[eax+eax-1]

LAB_004b1f21:
  004b1f21  8b542410              mov       edx,[esp+10h]
  004b1f25  83c240                add       edx,40h
  004b1f28  0fafd0                imul      edx,eax
  004b1f2b  66015632              add       [esi+32h],dx

LAB_004b1f2f:
  004b1f2f  6aff                  push      0FFFFFFFFh
  004b1f31  6aff                  push      0FFFFFFFFh
  004b1f33  6aff                  push      0FFFFFFFFh
  004b1f35  56                    push      esi
  004b1f36  e8d5180100            call      004C3810h
  004b1f3b  8b44242c              mov       eax,[esp+2Ch]
  004b1f3f  83c410                add       esp,10h
  004b1f42  3bc3                  cmp       eax,ebx
  004b1f44  7517                  jne       short 004B1F5Dh
  004b1f46  668b842486000000      mov       ax,[esp+86h]
  004b1f4e  66894632              mov       [esi+32h],ax
  004b1f52  5f                    pop       edi
  004b1f53  5e                    pop       esi
  004b1f54  5d                    pop       ebp
  004b1f55  5b                    pop       ebx
  004b1f56  81c488000000          add       esp,88h
  004b1f5c  c3                    ret

LAB_004b1f5d:
  004b1f5d  385e43                cmp       [esi+43h],bl
  004b1f60  0f8523030000          jne       near 004B2289h
  004b1f66  8b466c                mov       eax,[esi+6Ch]
  004b1f69  8b4e1c                mov       ecx,[esi+1Ch]
  004b1f6c  8b5624                mov       edx,[esi+24h]
  004b1f6f  8b781c                mov       edi,[eax+1Ch]
  004b1f72  8b6824                mov       ebp,[eax+24h]
  004b1f75  2bf9                  sub       edi,ecx
  004b1f77  2bea                  sub       ebp,edx
  004b1f79  c1ff0c                sar       edi,0Ch
  004b1f7c  c1fd0c                sar       ebp,0Ch
  004b1f7f  8bcf                  mov       ecx,edi
  004b1f81  8bd5                  mov       edx,ebp
  004b1f83  0fafcf                imul      ecx,edi
  004b1f86  0fafd5                imul      edx,ebp
  004b1f89  03ca                  add       ecx,edx
  004b1f8b  51                    push      ecx
  004b1f8c  e87f3a0900            call      00545A10h
  004b1f91  55                    push      ebp
  004b1f92  57                    push      edi
  004b1f93  8944241c              mov       [esp+1Ch],eax
  004b1f97  e8863a0900            call      00545A22h
  004b1f9c  668b4e32              mov       cx,[esi+32h]
  004b1fa0  66895e12              mov       [esi+12h],bx
  004b1fa4  2bc1                  sub       eax,ecx
  004b1fa6  25ff0f0000            and       eax,0FFFh
  004b1fab  89442418              mov       [esp+18h],eax
  004b1faf  8b442414              mov       eax,[esp+14h]
  004b1fb3  3dfa000000            cmp       eax,0FAh
  004b1fb8  0f8d1b010000          jge       near 004B20D9h
  004b1fbe  e87d510900            call      00547140h
  004b1fc3  99                    cdq
  004b1fc4  b964000000            mov       ecx,64h
  004b1fc9  f7f9                  idiv      ecx
  004b1fcb  8bea                  mov       ebp,edx
  004b1fcd  83fd32                cmp       ebp,32h
  004b1fd0  0f8dda000000          jge       near 004B20B0h
  004b1fd6  8b542418              mov       edx,[esp+18h]
  004b1fda  8b7c2420              mov       edi,[esp+20h]
  004b1fde  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004b1fe5  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004b1fec  25ff0f0000            and       eax,0FFFh
  004b1ff1  3bc1                  cmp       eax,ecx
  004b1ff3  0f8fad000000          jg        near 004B20A6h
  004b1ff9  e842510900            call      00547140h
  004b1ffe  99                    cdq
  004b1fff  8d4c3f01              lea       ecx,[edi+edi+1]
  004b2003  f7f9                  idiv      ecx
  004b2005  8b442414              mov       eax,[esp+14h]
  004b2009  83c264                add       edx,64h
  004b200c  3bc2                  cmp       eax,edx
  004b200e  0f8d92000000          jge       near 004B20A6h
  004b2014  83fd1e                cmp       ebp,1Eh
  004b2017  7d78                  jge       short 004B2091h
  004b2019  8a4e42                mov       cl,[esi+42h]
  004b201c  b00b                  mov       al,0Bh
  004b201e  3ac8                  cmp       cl,al
  004b2020  7435                  je        short 004B2057h
  004b2022  8b16                  mov       edx,[esi]
  004b2024  884642                mov       [esi+42h],al
  004b2027  81e2fffffffe          and       edx,0FEFFFFFFh
  004b202d  c6464301              mov       byte [esi+43h],1
  004b2031  83ca02                or        edx,2
  004b2034  83fd0a                cmp       ebp,0Ah
  004b2037  8916                  mov       [esi],edx
  004b2039  895e5c                mov       [esi+5Ch],ebx
  004b203c  66c746180200          mov       word [esi+18h],2
  004b2042  66895e1a              mov       [esi+1Ah],bx
  004b2046  0f8d91000000          jge       near 004B20DDh
  004b204c  66c746180100          mov       word [esi+18h],1
  004b2052  e986000000            jmp       004B20DDh

LAB_004b2057:
  004b2057  c6464203              mov       byte [esi+42h],3
  004b205b  66895e12              mov       [esi+12h],bx
  004b205f  e8dc500900            call      00547140h
  004b2064  99                    cdq
  004b2065  b90a000000            mov       ecx,0Ah
  004b206a  f7f9                  idiv      ecx
  004b206c  8bc7                  mov       eax,edi
  004b206e  d1f8                  sar       eax,1
  004b2070  02d0                  add       dl,al
  004b2072  80c214                add       dl,14h
  004b2075  885643                mov       [esi+43h],dl
  004b2078  e8c3500900            call      00547140h
  004b207d  99                    cdq
  004b207e  33c2                  xor       eax,edx
  004b2080  2bc2                  sub       eax,edx
  004b2082  25ff0f0000            and       eax,0FFFh
  004b2087  33c2                  xor       eax,edx
  004b2089  2bc2                  sub       eax,edx
  004b208b  66894610              mov       [esi+10h],ax
  004b208f  eb4c                  jmp       short 004B20DDh

LAB_004b2091:
  004b2091  885e42                mov       [esi+42h],bl
  004b2094  e8a7500900            call      00547140h
  004b2099  99                    cdq
  004b209a  b928000000            mov       ecx,28h
  004b209f  f7f9                  idiv      ecx
  004b20a1  885643                mov       [esi+43h],dl
  004b20a4  eb37                  jmp       short 004B20DDh

LAB_004b20a6:
  004b20a6  c6464201              mov       byte [esi+42h],1
  004b20aa  c6464301              mov       byte [esi+43h],1
  004b20ae  eb2d                  jmp       short 004B20DDh

LAB_004b20b0:
  004b20b0  c6464202              mov       byte [esi+42h],2
  004b20b4  e887500900            call      00547140h
  004b20b9  99                    cdq
  004b20ba  b90a000000            mov       ecx,0Ah
  004b20bf  8b7c2420              mov       edi,[esp+20h]
  004b20c3  f7f9                  idiv      ecx
  004b20c5  8bc7                  mov       eax,edi
  004b20c7  66c746105000          mov       word [esi+10h],50h
  004b20cd  d1f8                  sar       eax,1
  004b20cf  02d0                  add       dl,al
  004b20d1  80c214                add       dl,14h
  004b20d4  885643                mov       [esi+43h],dl
  004b20d7  eb04                  jmp       short 004B20DDh

LAB_004b20d9:
  004b20d9  8b7c2420              mov       edi,[esp+20h]

LAB_004b20dd:
  004b20dd  8b442414              mov       eax,[esp+14h]
  004b20e1  3d58020000            cmp       eax,258h
  004b20e6  0f8db9000000          jge       near 004B21A5h
  004b20ec  3dfa000000            cmp       eax,0FAh
  004b20f1  0f8cae000000          jl        near 004B21A5h
  004b20f7  e844500900            call      00547140h
  004b20fc  99                    cdq
  004b20fd  b964000000            mov       ecx,64h
  004b2102  f7f9                  idiv      ecx
  004b2104  83fa14                cmp       edx,14h
  004b2107  7d22                  jge       short 004B212Bh
  004b2109  8b542418              mov       edx,[esp+18h]
  004b210d  81c200040000          add       edx,400h
  004b2113  81e2ff0f0000          and       edx,0FFFh
  004b2119  81fa00080000          cmp       edx,800h
  004b211f  7c78                  jl        short 004B2199h
  004b2121  c6464201              mov       byte [esi+42h],1
  004b2125  c6464301              mov       byte [esi+43h],1
  004b2129  eb7a                  jmp       short 004B21A5h

LAB_004b212b:
  004b212b  83fa32                cmp       edx,32h
  004b212e  7d25                  jge       short 004B2155h
  004b2130  c6464202              mov       byte [esi+42h],2
  004b2134  e807500900            call      00547140h
  004b2139  99                    cdq
  004b213a  b90a000000            mov       ecx,0Ah
  004b213f  66c746105000          mov       word [esi+10h],50h
  004b2145  f7f9                  idiv      ecx
  004b2147  8bc7                  mov       eax,edi
  004b2149  d1f8                  sar       eax,1
  004b214b  02d0                  add       dl,al
  004b214d  80c214                add       dl,14h
  004b2150  885643                mov       [esi+43h],dl
  004b2153  eb50                  jmp       short 004B21A5h

LAB_004b2155:
  004b2155  83fa3c                cmp       edx,3Ch
  004b2158  7c3f                  jl        short 004B2199h
  004b215a  83fa50                cmp       edx,50h
  004b215d  7d3a                  jge       short 004B2199h
  004b215f  c6464203              mov       byte [esi+42h],3
  004b2163  66895e12              mov       [esi+12h],bx
  004b2167  e8d44f0900            call      00547140h
  004b216c  99                    cdq
  004b216d  b90a000000            mov       ecx,0Ah
  004b2172  f7f9                  idiv      ecx
  004b2174  8bc7                  mov       eax,edi
  004b2176  d1f8                  sar       eax,1
  004b2178  02d0                  add       dl,al
  004b217a  80c214                add       dl,14h
  004b217d  885643                mov       [esi+43h],dl
  004b2180  e8bb4f0900            call      00547140h
  004b2185  99                    cdq
  004b2186  33c2                  xor       eax,edx
  004b2188  2bc2                  sub       eax,edx
  004b218a  25ff0f0000            and       eax,0FFFh
  004b218f  33c2                  xor       eax,edx
  004b2191  2bc2                  sub       eax,edx
  004b2193  66894610              mov       [esi+10h],ax
  004b2197  eb0c                  jmp       short 004B21A5h

LAB_004b2199:
  004b2199  c646420a              mov       byte [esi+42h],0Ah
  004b219d  c6464301              mov       byte [esi+43h],1
  004b21a1  66895e12              mov       [esi+12h],bx

LAB_004b21a5:
  004b21a5  817c241458020000      cmp       dword [esp+14h],258h
  004b21ad  0f8cd6000000          jl        near 004B2289h
  004b21b3  e8884f0900            call      00547140h
  004b21b8  99                    cdq
  004b21b9  b964000000            mov       ecx,64h
  004b21be  f7f9                  idiv      ecx
  004b21c0  83fa32                cmp       edx,32h
  004b21c3  7d6f                  jge       short 004B2234h
  004b21c5  8b442410              mov       eax,[esp+10h]
  004b21c9  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004b21cd  3bd1                  cmp       edx,ecx
  004b21cf  7d2e                  jge       short 004B21FFh
  004b21d1  c6464202              mov       byte [esi+42h],2
  004b21d5  e8664f0900            call      00547140h
  004b21da  99                    cdq
  004b21db  b90a000000            mov       ecx,0Ah
  004b21e0  66c746106400          mov       word [esi+10h],64h
  004b21e6  f7f9                  idiv      ecx
  004b21e8  8bc7                  mov       eax,edi
  004b21ea  d1f8                  sar       eax,1
  004b21ec  02d0                  add       dl,al
  004b21ee  80c214                add       dl,14h
  004b21f1  885643                mov       [esi+43h],dl
  004b21f4  5f                    pop       edi
  004b21f5  5e                    pop       esi
  004b21f6  5d                    pop       ebp
  004b21f7  5b                    pop       ebx
  004b21f8  81c488000000          add       esp,88h
  004b21fe  c3                    ret

LAB_004b21ff:
  004b21ff  885e42                mov       [esi+42h],bl
  004b2202  e8394f0900            call      00547140h
  004b2207  99                    cdq
  004b2208  b90a000000            mov       ecx,0Ah
  004b220d  66895e12              mov       [esi+12h],bx
  004b2211  f7f9                  idiv      ecx
  004b2213  8b4c2410              mov       ecx,[esp+10h]
  004b2217  8bc7                  mov       eax,edi
  004b2219  c1f802                sar       eax,2
  004b221c  02d0                  add       dl,al
  004b221e  885643                mov       [esi+43h],dl
  004b2221  8d540950              lea       edx,[ecx+ecx+50h]
  004b2225  66895610              mov       [esi+10h],dx
  004b2229  5f                    pop       edi
  004b222a  5e                    pop       esi
  004b222b  5d                    pop       ebp
  004b222c  5b                    pop       ebx
  004b222d  81c488000000          add       esp,88h
  004b2233  c3                    ret

LAB_004b2234:
  004b2234  83fa4b                cmp       edx,4Bh
  004b2237  7d43                  jge       short 004B227Ch
  004b2239  c6464203              mov       byte [esi+42h],3
  004b223d  66895e12              mov       [esi+12h],bx
  004b2241  e8fa4e0900            call      00547140h
  004b2246  99                    cdq
  004b2247  b90a000000            mov       ecx,0Ah
  004b224c  f7f9                  idiv      ecx
  004b224e  8bc7                  mov       eax,edi
  004b2250  d1f8                  sar       eax,1
  004b2252  02d0                  add       dl,al
  004b2254  80c214                add       dl,14h
  004b2257  885643                mov       [esi+43h],dl
  004b225a  e8e14e0900            call      00547140h
  004b225f  99                    cdq
  004b2260  33c2                  xor       eax,edx
  004b2262  2bc2                  sub       eax,edx
  004b2264  25ff0f0000            and       eax,0FFFh
  004b2269  33c2                  xor       eax,edx
  004b226b  2bc2                  sub       eax,edx
  004b226d  66894610              mov       [esi+10h],ax
  004b2271  5f                    pop       edi
  004b2272  5e                    pop       esi
  004b2273  5d                    pop       ebp
  004b2274  5b                    pop       ebx
  004b2275  81c488000000          add       esp,88h
  004b227b  c3                    ret

LAB_004b227c:
  004b227c  83fa3c                cmp       edx,3Ch
  004b227f  7d08                  jge       short 004B2289h
  004b2281  c6464201              mov       byte [esi+42h],1
  004b2285  c6464301              mov       byte [esi+43h],1

LAB_004b2289:
  004b2289  5f                    pop       edi
  004b228a  5e                    pop       esi
  004b228b  5d                    pop       ebp
  004b228c  5b                    pop       ebx
  004b228d  81c488000000          add       esp,88h
  004b2293  c3                    ret
  004b2294  da18                  ficomp    dword [eax]
  004b2296  4b                    dec       ebx
  004b2297  0027                  add       [edi],ah
  004b2299  194b00                sbb       [ebx],ecx
  004b229c  a2194b0049            mov       [49004B19h],al
  004b22a1  1a4b00                sbb       cl,[ebx]
  004b22a4  f8                    clc
  004b22a5  1c4b                  sbb       al,4Bh
  004b22a7  00f8                  add       al,bh
  004b22a9  1c4b                  sbb       al,4Bh
  004b22ab  00f8                  add       al,bh
  004b22ad  1c4b                  sbb       al,4Bh
  004b22af  0016                  add       [esi],dl
  004b22b1  1b4b00                sbb       ecx,[ebx]
  004b22b4  f8                    clc
  004b22b5  1c4b                  sbb       al,4Bh
  004b22b7  00f8                  add       al,bh
  004b22b9  1c4b                  sbb       al,4Bh
  004b22bb  00771b                add       [edi+1Bh],dh
  004b22be  4b                    dec       ebx
  004b22bf  00af1c4b0090          add       [edi-6FFFB4E4h],ch
  004b22c5  90                    nop
  004b22c6  90                    nop
  004b22c7  90                    nop
  004b22c8  90                    nop
  004b22c9  90                    nop
  004b22ca  90                    nop
  004b22cb  90                    nop
  004b22cc  90                    nop
  004b22cd  90                    nop
  004b22ce  90                    nop
  004b22cf  90                    nop

62 cible(s) de saut interne : 0x004b16c5, 0x004b16c9, 0x004b16cb, 0x004b180f, 0x004b1831, 0x004b1843, 0x004b1845, 0x004b18a8, 0x004b18ac, 0x004b18c5, 0x004b18fc, 0x004b190f, 0x004b1946, 0x004b196a, 0x004b198b, 0x004b19d5, 0x004b19f6, 0x004b1a02, 0x004b1a37, 0x004b1a60, 0x004b1ade, 0x004b1aec, 0x004b1af0, 0x004b1b30, 0x004b1b3c, 0x004b1b6f, 0x004b1bc3, 0x004b1bc5, 0x004b1bf1, 0x004b1c01, 0x004b1c03, 0x004b1c62, 0x004b1c6c, 0x004b1c70, 0x004b1c80, 0x004b1ce4, 0x004b1cf8, 0x004b1d03, 0x004b1d1c, 0x004b1d55, 0x004b1d7a, 0x004b1e3a, 0x004b1e5d, 0x004b1e5f, 0x004b1e98, 0x004b1f21, 0x004b1f2f, 0x004b1f5d, 0x004b2057, 0x004b2091, 0x004b20a6, 0x004b20b0, 0x004b20d9, 0x004b20dd, 0x004b212b, 0x004b2155, 0x004b2199, 0x004b21a5, 0x004b21ff, 0x004b2234, 0x004b227c, 0x004b2289
