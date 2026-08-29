C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00402fc0  section .text  offset fichier 0x23c0  500 octets

  00402fc0  83ec0c                sub       esp,0Ch
  00402fc3  56                    push      esi
  00402fc4  8b742414              mov       esi,[esp+14h]
  00402fc8  57                    push      edi
  00402fc9  8b7e6c                mov       edi,[esi+6Ch]
  00402fcc  85ff                  test      edi,edi
  00402fce  0f84da010000          je        near 004031AEh
  00402fd4  8b471c                mov       eax,[edi+1Ch]
  00402fd7  55                    push      ebp
  00402fd8  8d6e1c                lea       ebp,[esi+1Ch]
  00402fdb  894500                mov       [ebp],eax
  00402fde  8b4f20                mov       ecx,[edi+20h]
  00402fe1  894e20                mov       [esi+20h],ecx
  00402fe4  8b5724                mov       edx,[edi+24h]
  00402fe7  33c0                  xor       eax,eax
  00402fe9  895624                mov       [esi+24h],edx
  00402fec  8a4704                mov       al,[edi+4]
  00402fef  66c747180200          mov       word [edi+18h],2
  00402ff5  8b0c8540965e00        mov       ecx,[eax*4+5E9640h]
  00402ffc  8b5108                mov       edx,[ecx+8]
  00402fff  8b0f                  mov       ecx,[edi]
  00403001  81c908100000          or        ecx,1008h
  00403007  8954240c              mov       [esp+0Ch],edx
  0040300b  890f                  mov       [edi],ecx
  0040300d  66837e1000            cmp       word [esi+10h],0
  00403012  0f851a010000          jne       near 00403132h
  00403018  66c746100100          mov       word [esi+10h],1
  0040301e  e81d411400            call      00547140h
  00403023  99                    cdq
  00403024  b905000000            mov       ecx,5
  00403029  f7f9                  idiv      ecx
  0040302b  8d442410              lea       eax,[esp+10h]
  0040302f  8d4c2414              lea       ecx,[esp+14h]
  00403033  83c20f                add       edx,0Fh
  00403036  66895612              mov       [esi+12h],dx
  0040303a  8d54241c              lea       edx,[esp+1Ch]
  0040303e  52                    push      edx
  0040303f  50                    push      eax
  00403040  51                    push      ecx
  00403041  57                    push      edi
  00403042  c6470a00              mov       byte [edi+0Ah],0
  00403046  66c7471a0000          mov       word [edi+1Ah],0
  0040304c  e82f591300            call      00538980h
  00403051  8b4c242c              mov       ecx,[esp+2Ch]
  00403055  83c410                add       esp,10h
  00403058  0fbf411e              movsx     eax,word [ecx+1Eh]
  0040305c  0fbf511c              movsx     edx,word [ecx+1Ch]
  00403060  0fbf4920              movsx     ecx,word [ecx+20h]
  00403064  03c2                  add       eax,edx
  00403066  6aff                  push      0FFFFFFFFh
  00403068  03c1                  add       eax,ecx
  0040306a  6a00                  push      0
  0040306c  99                    cdq
  0040306d  2bc2                  sub       eax,edx
  0040306f  6a38                  push      38h
  00403071  6a00                  push      0
  00403073  55                    push      ebp
  00403074  d1f8                  sar       eax,1
  00403076  56                    push      esi
  00403077  66894614              mov       [esi+14h],ax
  0040307b  66c746160000          mov       word [esi+16h],0
  00403081  e8ba7b0700            call      0047AC40h
  00403086  83c418                add       esp,18h
  00403089  6aff                  push      0FFFFFFFFh
  0040308b  6a00                  push      0
  0040308d  6a38                  push      38h
  0040308f  6a00                  push      0
  00403091  55                    push      ebp
  00403092  56                    push      esi
  00403093  e8a87b0700            call      0047AC40h
  00403098  83c418                add       esp,18h
  0040309b  6aff                  push      0FFFFFFFFh
  0040309d  6a00                  push      0
  0040309f  6a38                  push      38h
  004030a1  6a00                  push      0
  004030a3  55                    push      ebp
  004030a4  56                    push      esi
  004030a5  e8967b0700            call      0047AC40h
  004030aa  83c418                add       esp,18h
  004030ad  6aff                  push      0FFFFFFFFh
  004030af  6a00                  push      0
  004030b1  6a38                  push      38h
  004030b3  6a00                  push      0
  004030b5  55                    push      ebp
  004030b6  56                    push      esi
  004030b7  e8847b0700            call      0047AC40h
  004030bc  83c418                add       esp,18h
  004030bf  6aff                  push      0FFFFFFFFh
  004030c1  6a00                  push      0
  004030c3  6a5a                  push      5Ah
  004030c5  56                    push      esi
  004030c6  e8157a0700            call      0047AAE0h
  004030cb  83c410                add       esp,10h
  004030ce  6aff                  push      0FFFFFFFFh
  004030d0  6a00                  push      0
  004030d2  6a5a                  push      5Ah
  004030d4  56                    push      esi
  004030d5  e8067a0700            call      0047AAE0h
  004030da  83c410                add       esp,10h
  004030dd  6aff                  push      0FFFFFFFFh
  004030df  6a00                  push      0
  004030e1  6a5a                  push      5Ah
  004030e3  56                    push      esi
  004030e4  e8f7790700            call      0047AAE0h
  004030e9  83c410                add       esp,10h
  004030ec  6aff                  push      0FFFFFFFFh
  004030ee  6a00                  push      0
  004030f0  6a5a                  push      5Ah
  004030f2  56                    push      esi
  004030f3  e8e8790700            call      0047AAE0h
  004030f8  83c410                add       esp,10h
  004030fb  6aff                  push      0FFFFFFFFh
  004030fd  6a00                  push      0
  004030ff  6a5a                  push      5Ah
  00403101  56                    push      esi
  00403102  e8d9790700            call      0047AAE0h
  00403107  83c410                add       esp,10h
  0040310a  6aff                  push      0FFFFFFFFh
  0040310c  6a00                  push      0
  0040310e  6a5a                  push      5Ah
  00403110  56                    push      esi
  00403111  e8ca790700            call      0047AAE0h
  00403116  83c410                add       esp,10h
  00403119  6a00                  push      0
  0040311b  6a00                  push      0
  0040311d  68000a0000            push      0A00h
  00403122  6800040000            push      400h
  00403127  6a25                  push      25h
  00403129  56                    push      esi
  0040312a  e891000000            call      004031C0h
  0040312f  83c418                add       esp,18h

LAB_00403132:
  00403132  66837e1001            cmp       word [esi+10h],1
  00403137  7546                  jne       short 0040317Fh
  00403139  8a4616                mov       al,[esi+16h]
  0040313c  66ff4e12              dec       word [esi+12h]
  00403140  a801                  test      al,1
  00403142  7424                  je        short 00403168h
  00403144  6aff                  push      0FFFFFFFFh
  00403146  6a00                  push      0
  00403148  6a38                  push      38h
  0040314a  6a00                  push      0
  0040314c  55                    push      ebp
  0040314d  56                    push      esi
  0040314e  e8ed7a0700            call      0047AC40h
  00403153  83c418                add       esp,18h
  00403156  6aff                  push      0FFFFFFFFh
  00403158  6a00                  push      0
  0040315a  6a38                  push      38h
  0040315c  6a00                  push      0
  0040315e  55                    push      ebp
  0040315f  56                    push      esi
  00403160  e8db7a0700            call      0047AC40h
  00403165  83c418                add       esp,18h

LAB_00403168:
  00403168  66837e1200            cmp       word [esi+12h],0
  0040316d  7d10                  jge       short 0040317Fh
  0040316f  8b0e                  mov       ecx,[esi]
  00403171  81e1ffffff7f          and       ecx,7FFFFFFFh
  00403177  890e                  mov       [esi],ecx
  00403179  8b07                  mov       eax,[edi]
  0040317b  24f7                  and       al,0F7h
  0040317d  8907                  mov       [edi],eax

LAB_0040317f:
  0040317f  f6461601              test      byte [esi+16h],1
  00403183  7424                  je        short 004031A9h
  00403185  8b54240c              mov       edx,[esp+0Ch]
  00403189  66ff471a              inc       word [edi+1Ah]
  0040318d  668b4f1a              mov       cx,[edi+1Ah]
  00403191  668b4216              mov       ax,[edx+16h]
  00403195  0fbfc9                movsx     ecx,cx
  00403198  8bd0                  mov       edx,eax
  0040319a  81e2ffff0000          and       edx,0FFFFh
  004031a0  3bca                  cmp       ecx,edx
  004031a2  7c05                  jl        short 004031A9h
  004031a4  48                    dec       eax
  004031a5  6689471a              mov       [edi+1Ah],ax

LAB_004031a9:
  004031a9  66ff4616              inc       word [esi+16h]
  004031ad  5d                    pop       ebp

LAB_004031ae:
  004031ae  5f                    pop       edi
  004031af  5e                    pop       esi
  004031b0  83c40c                add       esp,0Ch
  004031b3  c3                    ret

5 cible(s) de saut interne : 0x00403132, 0x00403168, 0x0040317f, 0x004031a9, 0x004031ae
