Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00409f40  section .text  offset fichier 0x9340  972 octets

  00409f40  83ec08                sub       esp,8
  00409f43  53                    push      ebx
  00409f44  55                    push      ebp
  00409f45  56                    push      esi
  00409f46  8b742418              mov       esi,[esp+18h]
  00409f4a  33db                  xor       ebx,ebx
  00409f4c  57                    push      edi
  00409f4d  8b466c                mov       eax,[esi+6Ch]
  00409f50  3bc3                  cmp       eax,ebx
  00409f52  89442414              mov       [esp+14h],eax
  00409f56  7405                  je        short 00409F5Dh
  00409f58  8b686c                mov       ebp,[eax+6Ch]
  00409f5b  eb04                  jmp       short 00409F61h

LAB_00409f5d:
  00409f5d  8b6c241c              mov       ebp,[esp+1Ch]

LAB_00409f61:
  00409f61  395e5c                cmp       [esi+5Ch],ebx
  00409f64  0f8504020000          jne       near 0040A16Eh
  00409f6a  66395e10              cmp       [esi+10h],bx
  00409f6e  754f                  jne       short 00409FBFh
  00409f70  66b81900              mov       ax,19h
  00409f74  668b4e36              mov       cx,[esi+36h]
  00409f78  662b4612              sub       ax,[esi+12h]
  00409f7c  83e1fe                and       ecx,0FFFFFFFEh
  00409f7f  c6460407              mov       byte [esi+4],7
  00409f83  c64638fa              mov       byte [esi+38h],0FAh
  00409f87  c64639fa              mov       byte [esi+39h],0FAh
  00409f8b  c6463afa              mov       byte [esi+3Ah],0FAh
  00409f8f  8d0480                lea       eax,[eax+eax*4]
  00409f92  66895e28              mov       [esi+28h],bx
  00409f96  66895e2c              mov       [esi+2Ch],bx
  00409f9a  66c746100100          mov       word [esi+10h],1
  00409fa0  8d0480                lea       eax,[eax+eax*4]
  00409fa3  66895e12              mov       [esi+12h],bx
  00409fa7  c1e004                shl       eax,4
  00409faa  83c902                or        ecx,2
  00409fad  6689462a              mov       [esi+2Ah],ax
  00409fb1  66895e14              mov       [esi+14h],bx
  00409fb5  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  00409fbb  66894e36              mov       [esi+36h],cx

LAB_00409fbf:
  00409fbf  66837e1001            cmp       word [esi+10h],1
  00409fc4  0f85a4010000          jne       near 0040A16Eh
  00409fca  6afb                  push      0FFFFFFFBh
  00409fcc  6af9                  push      0FFFFFFF9h
  00409fce  6af9                  push      0FFFFFFF9h
  00409fd0  56                    push      esi
  00409fd1  e80aaeffff            call      00404DE0h
  00409fd6  668b4628              mov       ax,[esi+28h]
  00409fda  8b4e48                mov       ecx,[esi+48h]
  00409fdd  0fbfd0                movsx     edx,ax
  00409fe0  8bf9                  mov       edi,ecx
  00409fe2  8bd9                  mov       ebx,ecx
  00409fe4  2bfa                  sub       edi,edx
  00409fe6  83c410                add       esp,10h
  00409fe9  c1ff03                sar       edi,3
  00409fec  8d1407                lea       edx,[edi+eax]
  00409fef  668b462c              mov       ax,[esi+2Ch]
  00409ff3  0fbff8                movsx     edi,ax
  00409ff6  2bdf                  sub       ebx,edi
  00409ff8  66895628              mov       [esi+28h],dx
  00409ffc  c1fb03                sar       ebx,3
  00409fff  03d8                  add       ebx,eax
  0040a001  8a463a                mov       al,[esi+3Ah]
  0040a004  66895e2c              mov       [esi+2Ch],bx
  0040a008  33db                  xor       ebx,ebx
  0040a00a  3ac3                  cmp       al,bl
  0040a00c  7506                  jne       short 0040A014h
  0040a00e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040a014:
  0040a014  66395e16              cmp       [esi+16h],bx
  0040a018  0f8550010000          jne       near 0040A16Eh
  0040a01e  0fbfc2                movsx     eax,dx
  0040a021  81c130f8ffff          add       ecx,0FFFFF830h
  0040a027  3bc1                  cmp       eax,ecx
  0040a029  0f8d3f010000          jge       near 0040A16Eh
  0040a02f  895c241c              mov       [esp+1Ch],ebx
  0040a033  bfc0935e00            mov       edi,5E93C0h
  0040a038  c744241004000000      mov       dword [esp+10h],4

LAB_0040a040:
  0040a040  8b07                  mov       eax,[edi]
  0040a042  a900000080            test      eax,80000000h
  0040a047  0f8403010000          je        near 0040A150h
  0040a04d  a900000050            test      eax,50000000h
  0040a052  0f85f8000000          jne       near 0040A150h
  0040a058  8b471c                mov       eax,[edi+1Ch]
  0040a05b  8b561c                mov       edx,[esi+1Ch]
  0040a05e  8b4f24                mov       ecx,[edi+24h]
  0040a061  2bc2                  sub       eax,edx
  0040a063  8b5624                mov       edx,[esi+24h]
  0040a066  c1f80c                sar       eax,0Ch
  0040a069  2bca                  sub       ecx,edx
  0040a06b  8bd0                  mov       edx,eax
  0040a06d  c1f90c                sar       ecx,0Ch
  0040a070  0fafd0                imul      edx,eax
  0040a073  8bc1                  mov       eax,ecx
  0040a075  0fafc1                imul      eax,ecx
  0040a078  03d0                  add       edx,eax
  0040a07a  52                    push      edx
  0040a07b  e890b91300            call      00545A10h
  0040a080  0fbf5628              movsx     edx,word [esi+28h]
  0040a084  8bc8                  mov       ecx,eax
  0040a086  b81f85eb51            mov       eax,51EB851Fh
  0040a08b  f7ea                  imul      edx
  0040a08d  c1fa04                sar       edx,4
  0040a090  8bc2                  mov       eax,edx
  0040a092  c1e81f                shr       eax,1Fh
  0040a095  03d0                  add       edx,eax
  0040a097  3bca                  cmp       ecx,edx
  0040a099  0f8db1000000          jge       near 0040A150h
  0040a09f  8b4c241c              mov       ecx,[esp+1Ch]
  0040a0a3  668b4614              mov       ax,[esi+14h]
  0040a0a7  ba01000000            mov       edx,1
  0040a0ac  d3e2                  shl       edx,cl
  0040a0ae  0fbfc8                movsx     ecx,ax
  0040a0b1  85d1                  test      ecx,edx
  0040a0b3  0f8597000000          jne       near 0040A150h
  0040a0b9  8b4c241c              mov       ecx,[esp+1Ch]
  0040a0bd  ba01000000            mov       edx,1
  0040a0c2  d3e2                  shl       edx,cl
  0040a0c4  0bd0                  or        edx,eax
  0040a0c6  8b442414              mov       eax,[esp+14h]
  0040a0ca  3bc3                  cmp       eax,ebx
  0040a0cc  66895614              mov       [esi+14h],dx
  0040a0d0  747e                  je        short 0040A150h
  0040a0d2  8b4644                mov       eax,[esi+44h]
  0040a0d5  83f80b                cmp       eax,0Bh
  0040a0d8  7776                  ja        short 0040A150h
  0040a0da  ff24850ca34000        jmp       dword [eax*4+40A30Ch]
  0040a0e1  53                    push      ebx
  0040a0e2  57                    push      edi
  0040a0e3  55                    push      ebp
  0040a0e4  e897030000            call      0040A480h
  0040a0e9  eb62                  jmp       short 0040A14Dh
  0040a0eb  53                    push      ebx
  0040a0ec  57                    push      edi
  0040a0ed  55                    push      ebp
  0040a0ee  e8cd030000            call      0040A4C0h
  0040a0f3  eb58                  jmp       short 0040A14Dh
  0040a0f5  53                    push      ebx
  0040a0f6  57                    push      edi
  0040a0f7  55                    push      ebp
  0040a0f8  e8c3040000            call      0040A5C0h
  0040a0fd  eb4e                  jmp       short 0040A14Dh
  0040a0ff  53                    push      ebx
  0040a100  57                    push      edi
  0040a101  55                    push      ebp
  0040a102  e839020000            call      0040A340h
  0040a107  eb44                  jmp       short 0040A14Dh
  0040a109  53                    push      ebx
  0040a10a  57                    push      edi
  0040a10b  55                    push      ebp
  0040a10c  e86f020000            call      0040A380h
  0040a111  eb3a                  jmp       short 0040A14Dh
  0040a113  53                    push      ebx
  0040a114  57                    push      edi
  0040a115  55                    push      ebp
  0040a116  e8e5020000            call      0040A400h
  0040a11b  eb30                  jmp       short 0040A14Dh
  0040a11d  53                    push      ebx
  0040a11e  57                    push      edi
  0040a11f  55                    push      ebp
  0040a120  e89b020000            call      0040A3C0h
  0040a125  eb26                  jmp       short 0040A14Dh
  0040a127  53                    push      ebx
  0040a128  57                    push      edi
  0040a129  55                    push      ebp
  0040a12a  e811030000            call      0040A440h
  0040a12f  eb1c                  jmp       short 0040A14Dh
  0040a131  53                    push      ebx
  0040a132  57                    push      edi
  0040a133  55                    push      ebp
  0040a134  e8c7030000            call      0040A500h
  0040a139  eb12                  jmp       short 0040A14Dh
  0040a13b  53                    push      ebx
  0040a13c  57                    push      edi
  0040a13d  55                    push      ebp
  0040a13e  e8fd030000            call      0040A540h
  0040a143  eb08                  jmp       short 0040A14Dh
  0040a145  53                    push      ebx
  0040a146  57                    push      edi
  0040a147  55                    push      ebp
  0040a148  e833040000            call      0040A580h

LAB_0040a14d:
  0040a14d  83c40c                add       esp,0Ch

LAB_0040a150:
  0040a150  8b54241c              mov       edx,[esp+1Ch]
  0040a154  8b442410              mov       eax,[esp+10h]
  0040a158  42                    inc       edx
  0040a159  81c79c000000          add       edi,9Ch
  0040a15f  48                    dec       eax
  0040a160  8954241c              mov       [esp+1Ch],edx
  0040a164  89442410              mov       [esp+10h],eax
  0040a168  0f85d2feffff          jne       near 0040A040h

LAB_0040a16e:
  0040a16e  8b465c                mov       eax,[esi+5Ch]
  0040a171  bf01000000            mov       edi,1
  0040a176  3bc7                  cmp       eax,edi
  0040a178  0f8586010000          jne       near 0040A304h
  0040a17e  66395e10              cmp       [esi+10h],bx
  0040a182  0f8594000000          jne       near 0040A21Ch
  0040a188  c6460430              mov       byte [esi+4],30h
  0040a18c  e8afcf1300            call      00547140h
  0040a191  99                    cdq
  0040a192  b9c8000000            mov       ecx,0C8h
  0040a197  f7f9                  idiv      ecx
  0040a199  03d1                  add       edx,ecx
  0040a19b  66895628              mov       [esi+28h],dx
  0040a19f  e89ccf1300            call      00547140h
  0040a1a4  99                    cdq
  0040a1a5  b9c8000000            mov       ecx,0C8h
  0040a1aa  f7f9                  idiv      ecx
  0040a1ac  03d1                  add       edx,ecx
  0040a1ae  6689562a              mov       [esi+2Ah],dx
  0040a1b2  e889cf1300            call      00547140h
  0040a1b7  99                    cdq
  0040a1b8  b9c8000000            mov       ecx,0C8h
  0040a1bd  f7f9                  idiv      ecx
  0040a1bf  03d1                  add       edx,ecx
  0040a1c1  6689562c              mov       [esi+2Ch],dx
  0040a1c5  e876cf1300            call      00547140h
  0040a1ca  99                    cdq
  0040a1cb  33c2                  xor       eax,edx
  0040a1cd  2bc2                  sub       eax,edx
  0040a1cf  25ff0f0000            and       eax,0FFFh
  0040a1d4  33c2                  xor       eax,edx
  0040a1d6  2bc2                  sub       eax,edx
  0040a1d8  66894632              mov       [esi+32h],ax
  0040a1dc  e85fcf1300            call      00547140h
  0040a1e1  99                    cdq
  0040a1e2  b9f4010000            mov       ecx,1F4h
  0040a1e7  885e38                mov       [esi+38h],bl
  0040a1ea  f7f9                  idiv      ecx
  0040a1ec  885e39                mov       [esi+39h],bl
  0040a1ef  885e3a                mov       [esi+3Ah],bl
  0040a1f2  66897e10              mov       [esi+10h],di
  0040a1f6  66895e12              mov       [esi+12h],bx
  0040a1fa  66895630              mov       [esi+30h],dx
  0040a1fe  e83dcf1300            call      00547140h
  0040a203  99                    cdq
  0040a204  b9c8000000            mov       ecx,0C8h
  0040a209  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  0040a20f  f7f9                  idiv      ecx
  0040a211  8b4620                mov       eax,[esi+20h]
  0040a214  c1e20c                shl       edx,0Ch
  0040a217  03c2                  add       eax,edx
  0040a219  894620                mov       [esi+20h],eax

LAB_0040a21c:
  0040a21c  66837e1001            cmp       word [esi+10h],1
  0040a221  b96affffff            mov       ecx,0FFFFFF6Ah
  0040a226  bf64000000            mov       edi,64h
  0040a22b  7570                  jne       short 0040A29Dh
  0040a22d  8a4638                mov       al,[esi+38h]
  0040a230  bafa000000            mov       edx,0FAh
  0040a235  8be8                  mov       ebp,eax
  0040a237  66014e32              add       [esi+32h],cx
  0040a23b  81e5ff000000          and       ebp,0FFh
  0040a241  2bd5                  sub       edx,ebp
  0040a243  d1fa                  sar       edx,1
  0040a245  02d0                  add       dl,al
  0040a247  8a4639                mov       al,[esi+39h]
  0040a24a  8be8                  mov       ebp,eax
  0040a24c  885638                mov       [esi+38h],dl
  0040a24f  81e5ff000000          and       ebp,0FFh
  0040a255  bafa000000            mov       edx,0FAh
  0040a25a  2bd5                  sub       edx,ebp
  0040a25c  d1fa                  sar       edx,1
  0040a25e  02d0                  add       dl,al
  0040a260  8a463a                mov       al,[esi+3Ah]
  0040a263  8be8                  mov       ebp,eax
  0040a265  885639                mov       [esi+39h],dl
  0040a268  81e5ff000000          and       ebp,0FFh
  0040a26e  bafa000000            mov       edx,0FAh
  0040a273  2bd5                  sub       edx,ebp
  0040a275  d1fa                  sar       edx,1
  0040a277  02d0                  add       dl,al
  0040a279  66ff4612              inc       word [esi+12h]
  0040a27d  66837e1205            cmp       word [esi+12h],5
  0040a282  88563a                mov       [esi+3Ah],dl
  0040a285  7e0a                  jle       short 0040A291h
  0040a287  66c746100200          mov       word [esi+10h],2
  0040a28d  66895e12              mov       [esi+12h],bx

LAB_0040a291:
  0040a291  66017e28              add       [esi+28h],di
  0040a295  66017e2a              add       [esi+2Ah],di
  0040a299  66017e2c              add       [esi+2Ch],di

LAB_0040a29d:
  0040a29d  66837e1002            cmp       word [esi+10h],2
  0040a2a2  7560                  jne       short 0040A304h
  0040a2a4  8a4638                mov       al,[esi+38h]
  0040a2a7  66014e32              add       [esi+32h],cx
  0040a2ab  8bd0                  mov       edx,eax
  0040a2ad  81e2ff000000          and       edx,0FFh
  0040a2b3  f7da                  neg       edx
  0040a2b5  c1fa03                sar       edx,3
  0040a2b8  02d0                  add       dl,al
  0040a2ba  8a4639                mov       al,[esi+39h]
  0040a2bd  885638                mov       [esi+38h],dl
  0040a2c0  8bd0                  mov       edx,eax
  0040a2c2  81e2ff000000          and       edx,0FFh
  0040a2c8  f7da                  neg       edx
  0040a2ca  c1fa03                sar       edx,3
  0040a2cd  02d0                  add       dl,al
  0040a2cf  8a463a                mov       al,[esi+3Ah]
  0040a2d2  885639                mov       [esi+39h],dl
  0040a2d5  8bd0                  mov       edx,eax
  0040a2d7  81e2ff000000          and       edx,0FFh
  0040a2dd  f7da                  neg       edx
  0040a2df  c1fa03                sar       edx,3
  0040a2e2  02d0                  add       dl,al
  0040a2e4  66ff4612              inc       word [esi+12h]
  0040a2e8  66837e1214            cmp       word [esi+12h],14h
  0040a2ed  88563a                mov       [esi+3Ah],dl
  0040a2f0  7e06                  jle       short 0040A2F8h
  0040a2f2  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040a2f8:
  0040a2f8  66017e28              add       [esi+28h],di
  0040a2fc  66017e2a              add       [esi+2Ah],di
  0040a300  66017e2c              add       [esi+2Ch],di

LAB_0040a304:
  0040a304  5f                    pop       edi
  0040a305  5e                    pop       esi
  0040a306  5d                    pop       ebp
  0040a307  5b                    pop       ebx
  0040a308  83c408                add       esp,8
  0040a30b  c3                    ret

13 cible(s) de saut interne : 0x00409f5d, 0x00409f61, 0x00409fbf, 0x0040a014, 0x0040a040, 0x0040a14d, 0x0040a150, 0x0040a16e, 0x0040a21c, 0x0040a291, 0x0040a29d, 0x0040a2f8, 0x0040a304
