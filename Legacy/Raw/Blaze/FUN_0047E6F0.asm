
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047e6f0  section .text  offset fichier 0x7daf0  901 octets

  0047e6f0  83ec18                sub       esp,18h
  0047e6f3  53                    push      ebx
  0047e6f4  55                    push      ebp
  0047e6f5  56                    push      esi
  0047e6f6  8b742428              mov       esi,[esp+28h]
  0047e6fa  57                    push      edi
  0047e6fb  33ff                  xor       edi,edi
  0047e6fd  66397e10              cmp       [esi+10h],di
  0047e701  66897c2410            mov       [esp+10h],di
  0047e706  66c7442412c4ff        mov       word [esp+12h],0FFC4h
  0047e70d  66c74424143c00        mov       word [esp+14h],3Ch
  0047e714  66c7442416c800        mov       word [esp+16h],0C8h
  0047e71b  66897c2418            mov       [esp+18h],di
  0047e720  66c744241a7800        mov       word [esp+1Ah],78h
  0047e727  7558                  jne       short 0047E781h
  0047e729  8b2e                  mov       ebp,[esi]
  0047e72b  33c0                  xor       eax,eax
  0047e72d  8a460b                mov       al,[esi+0Bh]
  0047e730  804e3606              or        byte [esi+36h],6
  0047e734  6aff                  push      0FFFFFFFFh
  0047e736  50                    push      eax
  0047e737  6a4d                  push      4Dh
  0047e739  8d4e1c                lea       ecx,[esi+1Ch]
  0047e73c  68a01b5500            push      551BA0h
  0047e741  81cd80000000          or        ebp,80h
  0047e747  51                    push      ecx
  0047e748  56                    push      esi
  0047e749  66c746100100          mov       word [esi+10h],1
  0047e74f  892e                  mov       [esi],ebp
  0047e751  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047e757  e8e4c4ffff            call      0047AC40h
  0047e75c  83c418                add       esp,18h
  0047e75f  33d2                  xor       edx,edx
  0047e761  8a560b                mov       dl,[esi+0Bh]
  0047e764  66c746120010          mov       word [esi+12h],1000h
  0047e76a  6aff                  push      0FFFFFFFFh
  0047e76c  52                    push      edx
  0047e76d  6a6a                  push      6Ah
  0047e76f  56                    push      esi
  0047e770  e86bc3ffff            call      0047AAE0h
  0047e775  83c410                add       esp,10h
  0047e778  897e50                mov       [esi+50h],edi
  0047e77b  897e54                mov       [esi+54h],edi
  0047e77e  897e58                mov       [esi+58h],edi

LAB_0047e781:
  0047e781  8b0e                  mov       ecx,[esi]
  0047e783  8b466c                mov       eax,[esi+6Ch]
  0047e786  f7c100000002          test      ecx,2000000h
  0047e78c  740e                  je        short 0047E79Ch
  0047e78e  66ff4816              dec       word [eax+16h]
  0047e792  893e                  mov       [esi],edi
  0047e794  5f                    pop       edi
  0047e795  5e                    pop       esi
  0047e796  5d                    pop       ebp
  0047e797  5b                    pop       ebx
  0047e798  83c418                add       esp,18h
  0047e79b  c3                    ret

LAB_0047e79c:
  0047e79c  8b00                  mov       eax,[eax]
  0047e79e  2500000002            and       eax,2000000h
  0047e7a3  0bc1                  or        eax,ecx
  0047e7a5  a900000040            test      eax,40000000h
  0047e7aa  8906                  mov       [esi],eax
  0047e7ac  7407                  je        short 0047E7B5h
  0047e7ae  0d00000002            or        eax,2000000h
  0047e7b3  8906                  mov       [esi],eax

LAB_0047e7b5:
  0047e7b5  8b06                  mov       eax,[esi]
  0047e7b7  a840                  test      al,40h
  0047e7b9  0f84c1000000          je        near 0047E880h
  0047e7bf  8b7e78                mov       edi,[esi+78h]
  0047e7c2  f70700000050          test      dword [edi],50000000h
  0047e7c8  740f                  je        short 0047E7D9h
  0047e7ca  0d00000002            or        eax,2000000h
  0047e7cf  8906                  mov       [esi],eax
  0047e7d1  5f                    pop       edi
  0047e7d2  5e                    pop       esi
  0047e7d3  5d                    pop       ebp
  0047e7d4  5b                    pop       ebx
  0047e7d5  83c418                add       esp,18h
  0047e7d8  c3                    ret

LAB_0047e7d9:
  0047e7d9  8a4e10                mov       cl,[esi+10h]
  0047e7dc  80e107                and       cl,7
  0047e7df  80f901                cmp       cl,1
  0047e7e2  0f858c000000          jne       near 0047E874h
  0047e7e8  e853890c00            call      00547140h
  0047e7ed  8bd8                  mov       ebx,eax
  0047e7ef  e84c890c00            call      00547140h
  0047e7f4  8be8                  mov       ebp,eax
  0047e7f6  53                    push      ebx
  0047e7f7  83e54f                and       ebp,4Fh
  0047e7fa  e81d720c00            call      00545A1Ch
  0047e7ff  0fafc5                imul      eax,ebp
  0047e802  8b4f1c                mov       ecx,[edi+1Ch]
  0047e805  53                    push      ebx
  0047e806  03c1                  add       eax,ecx
  0047e808  89461c                mov       [esi+1Ch],eax
  0047e80b  8b5720                mov       edx,[edi+20h]
  0047e80e  895620                mov       [esi+20h],edx
  0047e811  e800720c00            call      00545A16h
  0047e816  0fafc5                imul      eax,ebp
  0047e819  8b4f24                mov       ecx,[edi+24h]
  0047e81c  6aff                  push      0FFFFFFFFh
  0047e81e  03c1                  add       eax,ecx
  0047e820  c6460400              mov       byte [esi+4],0
  0047e824  894624                mov       [esi+24h],eax
  0047e827  33c0                  xor       eax,eax
  0047e829  8a460b                mov       al,[esi+0Bh]
  0047e82c  50                    push      eax
  0047e82d  6a6f                  push      6Fh
  0047e82f  56                    push      esi
  0047e830  e8abc2ffff            call      0047AAE0h
  0047e835  8b4674                mov       eax,[esi+74h]
  0047e838  83c410                add       esp,10h
  0047e83b  33c9                  xor       ecx,ecx
  0047e83d  33d2                  xor       edx,edx
  0047e83f  8a4e06                mov       cl,[esi+6]
  0047e842  8a5607                mov       dl,[esi+7]
  0047e845  6a00                  push      0
  0047e847  51                    push      ecx
  0047e848  52                    push      edx
  0047e849  57                    push      edi
  0047e84a  50                    push      eax
  0047e84b  e860ba0b00            call      0053A2B0h
  0047e850  8b1f                  mov       ebx,[edi]
  0047e852  83c414                add       esp,14h
  0047e855  8d461c                lea       eax,[esi+1Ch]
  0047e858  83cb40                or        ebx,40h
  0047e85b  50                    push      eax
  0047e85c  6848255500            push      552548h
  0047e861  66c787960000001000    mov       word [edi+96h],10h
  0047e86a  891f                  mov       [edi],ebx
  0047e86c  e86ff30b00            call      0053DBE0h
  0047e871  83c408                add       esp,8

LAB_0047e874:
  0047e874  66ff4610              inc       word [esi+10h]
  0047e878  5f                    pop       edi
  0047e879  5e                    pop       esi
  0047e87a  5d                    pop       ebp
  0047e87b  5b                    pop       ebx
  0047e87c  83c418                add       esp,18h
  0047e87f  c3                    ret

LAB_0047e880:
  0047e880  8b6e78                mov       ebp,[esi+78h]
  0047e883  3bef                  cmp       ebp,edi
  0047e885  7516                  jne       short 0047E89Dh
  0047e887  8b4e74                mov       ecx,[esi+74h]
  0047e88a  6a01                  push      1
  0047e88c  6aff                  push      0FFFFFFFFh
  0047e88e  51                    push      ecx
  0047e88f  e81c66ffff            call      00474EB0h
  0047e894  8be8                  mov       ebp,eax
  0047e896  83c40c                add       esp,0Ch
  0047e899  3bef                  cmp       ebp,edi
  0047e89b  7456                  je        short 0047E8F3h

LAB_0047e89d:
  0047e89d  f7450000000050        test      dword [ebp],50000000h
  0047e8a4  740c                  je        short 0047E8B2h
  0047e8a6  897d74                mov       [ebp+74h],edi
  0047e8a9  8b06                  mov       eax,[esi]
  0047e8ab  0d00000002            or        eax,2000000h
  0047e8b0  8906                  mov       [esi],eax

LAB_0047e8b2:
  0047e8b2  8d5e1c                lea       ebx,[esi+1Ch]
  0047e8b5  896e78                mov       [esi+78h],ebp
  0047e8b8  8b7d1c                mov       edi,[ebp+1Ch]
  0047e8bb  897574                mov       [ebp+74h],esi
  0047e8be  8b0b                  mov       ecx,[ebx]
  0047e8c0  55                    push      ebp
  0047e8c1  2bf9                  sub       edi,ecx
  0047e8c3  c1ff0c                sar       edi,0Ch
  0047e8c6  e8f5a20b00            call      00538BC0h
  0047e8cb  8b5520                mov       edx,[ebp+20h]
  0047e8ce  8b4e20                mov       ecx,[esi+20h]
  0047e8d1  8b6d24                mov       ebp,[ebp+24h]
  0047e8d4  2bd1                  sub       edx,ecx
  0047e8d6  8b4e24                mov       ecx,[esi+24h]
  0047e8d9  83c404                add       esp,4
  0047e8dc  d1f8                  sar       eax,1
  0047e8de  c1fa0c                sar       edx,0Ch
  0047e8e1  03c2                  add       eax,edx
  0047e8e3  2be9                  sub       ebp,ecx
  0047e8e5  8944242c              mov       [esp+2Ch],eax
  0047e8e9  b810000000            mov       eax,10h
  0047e8ee  c1fd0c                sar       ebp,0Ch
  0047e8f1  eb5e                  jmp       short 0047E951h

LAB_0047e8f3:
  0047e8f3  8b6e74                mov       ebp,[esi+74h]
  0047e8f6  8d5e1c                lea       ebx,[esi+1Ch]
  0047e8f9  e842880c00            call      00547140h
  0047e8fe  8b4d1c                mov       ecx,[ebp+1Ch]
  0047e901  8b3b                  mov       edi,[ebx]
  0047e903  2bcf                  sub       ecx,edi
  0047e905  25ff010000            and       eax,1FFh
  0047e90a  c1f90c                sar       ecx,0Ch
  0047e90d  55                    push      ebp
  0047e90e  8dbc0800ffffff        lea       edi,[eax+ecx-100h]
  0047e915  e8a6a20b00            call      00538BC0h
  0047e91a  8b5520                mov       edx,[ebp+20h]
  0047e91d  8b4e20                mov       ecx,[esi+20h]
  0047e920  2bd1                  sub       edx,ecx
  0047e922  83c404                add       esp,4
  0047e925  c1fa0c                sar       edx,0Ch
  0047e928  8d441064              lea       eax,[eax+edx+64h]
  0047e92c  8944242c              mov       [esp+2Ch],eax
  0047e930  e80b880c00            call      00547140h
  0047e935  8b4d24                mov       ecx,[ebp+24h]
  0047e938  8b6e24                mov       ebp,[esi+24h]
  0047e93b  2bcd                  sub       ecx,ebp
  0047e93d  25ff010000            and       eax,1FFh
  0047e942  c1f90c                sar       ecx,0Ch
  0047e945  8dac0800ffffff        lea       ebp,[eax+ecx-100h]
  0047e94c  b808000000            mov       eax,8

LAB_0047e951:
  0047e951  50                    push      eax
  0047e952  8bd7                  mov       edx,edi
  0047e954  8bc5                  mov       eax,ebp
  0047e956  0fafd7                imul      edx,edi
  0047e959  0fafc5                imul      eax,ebp
  0047e95c  03d0                  add       edx,eax
  0047e95e  52                    push      edx
  0047e95f  e8ac700c00            call      00545A10h
  0047e964  8b4c2430              mov       ecx,[esp+30h]
  0047e968  50                    push      eax
  0047e969  51                    push      ecx
  0047e96a  e8b3700c00            call      00545A22h
  0047e96f  50                    push      eax
  0047e970  55                    push      ebp
  0047e971  57                    push      edi
  0047e972  e8ab700c00            call      00545A22h
  0047e977  8d542424              lea       edx,[esp+24h]
  0047e97b  50                    push      eax
  0047e97c  52                    push      edx
  0047e97d  e84e410b00            call      00532AD0h
  0047e982  8b4650                mov       eax,[esi+50h]
  0047e985  8b4c242c              mov       ecx,[esp+2Ch]
  0047e989  8b5654                mov       edx,[esi+54h]
  0047e98c  8b7c2434              mov       edi,[esp+34h]
  0047e990  03c1                  add       eax,ecx
  0047e992  8b4c2430              mov       ecx,[esp+30h]
  0047e996  03ca                  add       ecx,edx
  0047e998  8b5658                mov       edx,[esi+58h]
  0047e99b  03d7                  add       edx,edi
  0047e99d  8bf8                  mov       edi,eax
  0047e99f  c1ff03                sar       edi,3
  0047e9a2  2bc7                  sub       eax,edi
  0047e9a4  8bf9                  mov       edi,ecx
  0047e9a6  c1ff03                sar       edi,3
  0047e9a9  2bcf                  sub       ecx,edi
  0047e9ab  8bfa                  mov       edi,edx
  0047e9ad  8b6e24                mov       ebp,[esi+24h]
  0047e9b0  894650                mov       [esi+50h],eax
  0047e9b3  c1ff03                sar       edi,3
  0047e9b6  2bd7                  sub       edx,edi
  0047e9b8  8b3b                  mov       edi,[ebx]
  0047e9ba  03f8                  add       edi,eax
  0047e9bc  8b4620                mov       eax,[esi+20h]
  0047e9bf  83c410                add       esp,10h
  0047e9c2  03c1                  add       eax,ecx
  0047e9c4  03ea                  add       ebp,edx
  0047e9c6  894e54                mov       [esi+54h],ecx
  0047e9c9  56                    push      esi
  0047e9ca  895658                mov       [esi+58h],edx
  0047e9cd  893b                  mov       [ebx],edi
  0047e9cf  894620                mov       [esi+20h],eax
  0047e9d2  896e24                mov       [esi+24h],ebp
  0047e9d5  e8664f0b00            call      00533940h
  0047e9da  8b4660                mov       eax,[esi+60h]
  0047e9dd  8b4e20                mov       ecx,[esi+20h]
  0047e9e0  83c404                add       esp,4
  0047e9e3  3bc8                  cmp       ecx,eax
  0047e9e5  7f0d                  jg        short 0047E9F4h
  0047e9e7  894620                mov       [esi+20h],eax
  0047e9ea  c7869000000000080000  mov       dword [esi+90h],800h

LAB_0047e9f4:
  0047e9f4  8b8690000000          mov       eax,[esi+90h]
  0047e9fa  f6c4a0                test      ah,0A0h
  0047e9fd  753b                  jne       short 0047EA3Ah
  0047e9ff  a902080000            test      eax,802h
  0047ea04  7434                  je        short 0047EA3Ah
  0047ea06  6a04                  push      4
  0047ea08  6a00                  push      0
  0047ea0a  68bd000000            push      0BDh
  0047ea0f  68081a5500            push      551A08h
  0047ea14  53                    push      ebx
  0047ea15  56                    push      esi
  0047ea16  e825c2ffff            call      0047AC40h
  0047ea1b  8b4678                mov       eax,[esi+78h]
  0047ea1e  83c418                add       esp,18h
  0047ea21  85c0                  test      eax,eax
  0047ea23  7407                  je        short 0047EA2Ch
  0047ea25  c7407400000000        mov       dword [eax+74h],0

LAB_0047ea2c:
  0047ea2c  810e00000002          or        dword [esi],2000000h
  0047ea32  5f                    pop       edi
  0047ea33  5e                    pop       esi
  0047ea34  5d                    pop       ebp
  0047ea35  5b                    pop       ebx
  0047ea36  83c418                add       esp,18h
  0047ea39  c3                    ret

LAB_0047ea3a:
  0047ea3a  8b4678                mov       eax,[esi+78h]
  0047ea3d  85c0                  test      eax,eax
  0047ea3f  742c                  je        short 0047EA6Dh
  0047ea41  8d442410              lea       eax,[esp+10h]
  0047ea45  50                    push      eax
  0047ea46  56                    push      esi
  0047ea47  e85462ffff            call      00474CA0h
  0047ea4c  83c408                add       esp,8
  0047ea4f  85c0                  test      eax,eax
  0047ea51  741a                  je        short 0047EA6Dh
  0047ea53  8b16                  mov       edx,[esi]
  0047ea55  81ca40000001          or        edx,1000040h
  0047ea5b  8916                  mov       [esi],edx
  0047ea5d  8b08                  mov       ecx,[eax]
  0047ea5f  83c940                or        ecx,40h
  0047ea62  66c780960000001000    mov       word [eax+96h],10h
  0047ea6b  8908                  mov       [eax],ecx

LAB_0047ea6d:
  0047ea6d  5f                    pop       edi
  0047ea6e  5e                    pop       esi
  0047ea6f  5d                    pop       ebp
  0047ea70  5b                    pop       ebx
  0047ea71  83c418                add       esp,18h
  0047ea74  c3                    ret

14 cible(s) de saut interne : 0x0047e781, 0x0047e79c, 0x0047e7b5, 0x0047e7d9, 0x0047e874, 0x0047e880, 0x0047e89d, 0x0047e8b2, 0x0047e8f3, 0x0047e951, 0x0047e9f4, 0x0047ea2c, 0x0047ea3a, 0x0047ea6d
