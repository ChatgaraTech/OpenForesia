Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00403e80  section .text  offset fichier 0x3280  380 octets

  00403e80  83ec0c                sub       esp,0Ch
  00403e83  56                    push      esi
  00403e84  8b742414              mov       esi,[esp+14h]
  00403e88  57                    push      edi
  00403e89  8b7e6c                mov       edi,[esi+6Ch]
  00403e8c  85ff                  test      edi,edi
  00403e8e  0f8462010000          je        near 00403FF6h
  00403e94  8b471c                mov       eax,[edi+1Ch]
  00403e97  55                    push      ebp
  00403e98  8d6e1c                lea       ebp,[esi+1Ch]
  00403e9b  894500                mov       [ebp],eax
  00403e9e  8b4f20                mov       ecx,[edi+20h]
  00403ea1  894e20                mov       [esi+20h],ecx
  00403ea4  8b5724                mov       edx,[edi+24h]
  00403ea7  33c0                  xor       eax,eax
  00403ea9  895624                mov       [esi+24h],edx
  00403eac  8a4704                mov       al,[edi+4]
  00403eaf  66c747180200          mov       word [edi+18h],2
  00403eb5  8b0c8540965e00        mov       ecx,[eax*4+5E9640h]
  00403ebc  8b5108                mov       edx,[ecx+8]
  00403ebf  8b0f                  mov       ecx,[edi]
  00403ec1  81c908100000          or        ecx,1008h
  00403ec7  8954240c              mov       [esp+0Ch],edx
  00403ecb  890f                  mov       [edi],ecx
  00403ecd  66837e1000            cmp       word [esi+10h],0
  00403ed2  7568                  jne       short 00403F3Ch
  00403ed4  66c746100100          mov       word [esi+10h],1
  00403eda  e861321400            call      00547140h
  00403edf  99                    cdq
  00403ee0  b905000000            mov       ecx,5
  00403ee5  f7f9                  idiv      ecx
  00403ee7  8d442410              lea       eax,[esp+10h]
  00403eeb  8d4c2414              lea       ecx,[esp+14h]
  00403eef  83c20f                add       edx,0Fh
  00403ef2  66895612              mov       [esi+12h],dx
  00403ef6  8d54241c              lea       edx,[esp+1Ch]
  00403efa  52                    push      edx
  00403efb  50                    push      eax
  00403efc  51                    push      ecx
  00403efd  57                    push      edi
  00403efe  c6470a00              mov       byte [edi+0Ah],0
  00403f02  66c7471a0000          mov       word [edi+1Ah],0
  00403f08  e8734a1300            call      00538980h
  00403f0d  8b44242c              mov       eax,[esp+2Ch]
  00403f11  83c410                add       esp,10h
  00403f14  0fbf4820              movsx     ecx,word [eax+20h]
  00403f18  0fbf501e              movsx     edx,word [eax+1Eh]
  00403f1c  0fbf401c              movsx     eax,word [eax+1Ch]
  00403f20  03ca                  add       ecx,edx
  00403f22  66c746160000          mov       word [esi+16h],0
  00403f28  03c8                  add       ecx,eax
  00403f2a  b856555555            mov       eax,55555556h
  00403f2f  f7e9                  imul      ecx
  00403f31  8bca                  mov       ecx,edx
  00403f33  c1e91f                shr       ecx,1Fh
  00403f36  03d1                  add       edx,ecx
  00403f38  66895614              mov       [esi+14h],dx

LAB_00403f3c:
  00403f3c  66837e1600            cmp       word [esi+16h],0
  00403f41  7519                  jne       short 00403F5Ch
  00403f43  6a00                  push      0
  00403f45  6a00                  push      0
  00403f47  6800100000            push      1000h
  00403f4c  6829030000            push      329h
  00403f51  6a21                  push      21h
  00403f53  56                    push      esi
  00403f54  e867f2ffff            call      004031C0h
  00403f59  83c418                add       esp,18h

LAB_00403f5c:
  00403f5c  66837e1601            cmp       word [esi+16h],1
  00403f61  7519                  jne       short 00403F7Ch
  00403f63  6a00                  push      0
  00403f65  6a00                  push      0
  00403f67  68000a0000            push      0A00h
  00403f6c  6800040000            push      400h
  00403f71  6a23                  push      23h
  00403f73  56                    push      esi
  00403f74  e847f2ffff            call      004031C0h
  00403f79  83c418                add       esp,18h

LAB_00403f7c:
  00403f7c  66837e1001            cmp       word [esi+10h],1
  00403f81  7534                  jne       short 00403FB7h
  00403f83  8a4616                mov       al,[esi+16h]
  00403f86  66ff4e12              dec       word [esi+12h]
  00403f8a  a801                  test      al,1
  00403f8c  7412                  je        short 00403FA0h
  00403f8e  6aff                  push      0FFFFFFFFh
  00403f90  6a00                  push      0
  00403f92  6a33                  push      33h
  00403f94  6a00                  push      0
  00403f96  55                    push      ebp
  00403f97  56                    push      esi
  00403f98  e8a36c0700            call      0047AC40h
  00403f9d  83c418                add       esp,18h

LAB_00403fa0:
  00403fa0  66837e1200            cmp       word [esi+12h],0
  00403fa5  7d10                  jge       short 00403FB7h
  00403fa7  8b0e                  mov       ecx,[esi]
  00403fa9  81e1ffffff7f          and       ecx,7FFFFFFFh
  00403faf  890e                  mov       [esi],ecx
  00403fb1  8b07                  mov       eax,[edi]
  00403fb3  24f7                  and       al,0F7h
  00403fb5  8907                  mov       [edi],eax

LAB_00403fb7:
  00403fb7  6af6                  push      0FFFFFFF6h
  00403fb9  6af6                  push      0FFFFFFF6h
  00403fbb  6af6                  push      0FFFFFFF6h
  00403fbd  57                    push      edi
  00403fbe  e81d0e0000            call      00404DE0h
  00403fc3  8a4616                mov       al,[esi+16h]
  00403fc6  83c410                add       esp,10h
  00403fc9  a801                  test      al,1
  00403fcb  7424                  je        short 00403FF1h
  00403fcd  8b54240c              mov       edx,[esp+0Ch]
  00403fd1  66ff471a              inc       word [edi+1Ah]
  00403fd5  668b4f1a              mov       cx,[edi+1Ah]
  00403fd9  668b4216              mov       ax,[edx+16h]
  00403fdd  8bd0                  mov       edx,eax
  00403fdf  0fbfc9                movsx     ecx,cx
  00403fe2  81e2ffff0000          and       edx,0FFFFh
  00403fe8  3bca                  cmp       ecx,edx
  00403fea  7c05                  jl        short 00403FF1h
  00403fec  48                    dec       eax
  00403fed  6689471a              mov       [edi+1Ah],ax

LAB_00403ff1:
  00403ff1  66ff4616              inc       word [esi+16h]
  00403ff5  5d                    pop       ebp

LAB_00403ff6:
  00403ff6  5f                    pop       edi
  00403ff7  5e                    pop       esi
  00403ff8  83c40c                add       esp,0Ch
  00403ffb  c3                    ret

7 cible(s) de saut interne : 0x00403f3c, 0x00403f5c, 0x00403f7c, 0x00403fa0, 0x00403fb7, 0x00403ff1, 0x00403ff6
