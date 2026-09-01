Legacy/Binaries/Blaze.exe  base 0x00400000
0x00446f30  section .text  offset fichier 0x46330  273 octets

  00446f30  83ec08                sub       esp,8
  00446f33  56                    push      esi
  00446f34  8b742410              mov       esi,[esp+10h]
  00446f38  85f6                  test      esi,esi
  00446f3a  57                    push      edi
  00446f3b  0f8400010000          je        near 00447041h
  00446f41  8b7e6c                mov       edi,[esi+6Ch]
  00446f44  85ff                  test      edi,edi
  00446f46  7505                  jne       short 00446F4Dh
  00446f48  bf58e55d00            mov       edi,5DE558h

LAB_00446f4d:
  00446f4d  f70700008000          test      dword [edi],800000h
  00446f53  740e                  je        short 00446F63h
  00446f55  66c746141e00          mov       word [esi+14h],1Eh
  00446f5b  66c746167017          mov       word [esi+16h],1770h
  00446f61  eb6a                  jmp       short 00446FCDh

LAB_00446f63:
  00446f63  8d442414              lea       eax,[esp+14h]
  00446f67  8d4c2408              lea       ecx,[esp+8]
  00446f6b  50                    push      eax
  00446f6c  8d542410              lea       edx,[esp+10h]
  00446f70  51                    push      ecx
  00446f71  52                    push      edx
  00446f72  57                    push      edi
  00446f73  e8081a0f00            call      00538980h
  00446f78  8b442424              mov       eax,[esp+24h]
  00446f7c  83c410                add       esp,10h
  00446f7f  0fbf481c              movsx     ecx,word [eax+1Ch]
  00446f83  0fbf5020              movsx     edx,word [eax+20h]
  00446f87  03ca                  add       ecx,edx
  00446f89  d1f9                  sar       ecx,1
  00446f8b  66d1f9                sar       cx,1
  00446f8e  66894e14              mov       [esi+14h],cx
  00446f92  0fbf481e              movsx     ecx,word [eax+1Eh]
  00446f96  0fbf501c              movsx     edx,word [eax+1Ch]
  00446f9a  0fbf4020              movsx     eax,word [eax+20h]
  00446f9e  03ca                  add       ecx,edx
  00446fa0  03c8                  add       ecx,eax
  00446fa2  b856555555            mov       eax,55555556h
  00446fa7  f7e9                  imul      ecx
  00446fa9  8bca                  mov       ecx,edx
  00446fab  c1e91f                shr       ecx,1Fh
  00446fae  03d1                  add       edx,ecx
  00446fb0  6681fa5802            cmp       dx,258h
  00446fb5  66895616              mov       [esi+16h],dx
  00446fb9  7e06                  jle       short 00446FC1h
  00446fbb  66c746165802          mov       word [esi+16h],258h

LAB_00446fc1:
  00446fc1  668b5616              mov       dx,[esi+16h]
  00446fc5  666bd232              imul      dx,32h
  00446fc9  66895616              mov       [esi+16h],dx

LAB_00446fcd:
  00446fcd  66837e1000            cmp       word [esi+10h],0
  00446fd2  7524                  jne       short 00446FF8h
  00446fd4  8d461c                lea       eax,[esi+1Ch]
  00446fd7  66c746100100          mov       word [esi+10h],1
  00446fdd  50                    push      eax
  00446fde  6800100000            push      1000h
  00446fe3  6880000000            push      80h
  00446fe8  6a03                  push      3
  00446fea  66c746120000          mov       word [esi+12h],0
  00446ff0  e82b6b0f00            call      0053DB20h
  00446ff5  83c410                add       esp,10h

LAB_00446ff8:
  00446ff8  66837e1001            cmp       word [esi+10h],1
  00446ffd  7542                  jne       short 00447041h
  00446fff  8b4f1c                mov       ecx,[edi+1Ch]
  00447002  8d461c                lea       eax,[esi+1Ch]
  00447005  8908                  mov       [eax],ecx
  00447007  8b5720                mov       edx,[edi+20h]
  0044700a  895620                mov       [esi+20h],edx
  0044700d  8b4f24                mov       ecx,[edi+24h]
  00447010  894e24                mov       [esi+24h],ecx
  00447013  33d2                  xor       edx,edx
  00447015  8a560a                mov       dl,[esi+0Ah]
  00447018  33c9                  xor       ecx,ecx
  0044701a  8a4e0b                mov       cl,[esi+0Bh]
  0044701d  52                    push      edx
  0044701e  51                    push      ecx
  0044701f  689a000000            push      9Ah
  00447024  6a00                  push      0
  00447026  50                    push      eax
  00447027  56                    push      esi
  00447028  e8133c0300            call      0047AC40h
  0044702d  83c418                add       esp,18h
  00447030  66ff4612              inc       word [esi+12h]
  00447034  66837e1205            cmp       word [esi+12h],5
  00447039  7c06                  jl        short 00447041h
  0044703b  8126ffffff7f          and       dword [esi],7FFFFFFFh

5 cible(s) de saut interne : 0x00446f4d, 0x00446f63, 0x00446fc1, 0x00446fcd, 0x00446ff8
