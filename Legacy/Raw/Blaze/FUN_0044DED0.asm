Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044ded0  section .text  offset fichier 0x4d2d0  2919 octets

  0044ded0  81ecb4000000          sub       esp,0B4h
  0044ded6  b8baffffff            mov       eax,0FFFFFFBAh
  0044dedb  53                    push      ebx
  0044dedc  6689442460            mov       [esp+60h],ax
  0044dee1  6689442462            mov       [esp+62h],ax
  0044dee6  b824000000            mov       eax,24h
  0044deeb  55                    push      ebp
  0044deec  6689442468            mov       [esp+68h],ax
  0044def1  668944246a            mov       [esp+6Ah],ax
  0044def6  b88f000000            mov       eax,8Fh
  0044defb  56                    push      esi
  0044defc  6689442470            mov       [esp+70h],ax
  0044df01  6689442472            mov       [esp+72h],ax
  0044df06  b83e000000            mov       eax,3Eh
  0044df0b  8bb424c4000000        mov       esi,[esp+0C4h]
  0044df12  6689442450            mov       [esp+50h],ax
  0044df17  6689442452            mov       [esp+52h],ax
  0044df1c  b847000000            mov       eax,47h
  0044df21  57                    push      edi
  0044df22  6689442458            mov       [esp+58h],ax
  0044df27  668944245a            mov       [esp+5Ah],ax
  0044df2c  b854000000            mov       eax,54h
  0044df31  b910000000            mov       ecx,10h
  0044df36  668944245c            mov       [esp+5Ch],ax
  0044df3b  668944245e            mov       [esp+5Eh],ax
  0044df40  83c8ff                or        eax,0FFFFFFFFh
  0044df43  8d7c247c              lea       edi,[esp+7Ch]
  0044df47  f3ab                  rep stosd
  0044df49  8b466c                mov       eax,[esi+6Ch]
  0044df4c  33d2                  xor       edx,edx
  0044df4e  3bc2                  cmp       eax,edx
  0044df50  66c7442460a0ff        mov       word [esp+60h],0FFA0h
  0044df57  66c74424626000        mov       word [esp+62h],60h
  0044df5e  66c74424646aff        mov       word [esp+64h],0FF6Ah
  0044df65  66c74424669600        mov       word [esp+66h],96h
  0044df6c  66c744246897ff        mov       word [esp+68h],0FF97h
  0044df73  66c744246a6900        mov       word [esp+6Ah],69h
  0044df7a  7505                  jne       short 0044DF81h
  0044df7c  b858e55d00            mov       eax,5DE558h

LAB_0044df81:
  0044df81  8b6e74                mov       ebp,[esi+74h]
  0044df84  3bea                  cmp       ebp,edx
  0044df86  896c2430              mov       [esp+30h],ebp
  0044df8a  7509                  jne       short 0044DF95h
  0044df8c  bd58e55d00            mov       ebp,5DE558h
  0044df91  896c2430              mov       [esp+30h],ebp

LAB_0044df95:
  0044df95  8b4e1c                mov       ecx,[esi+1Ch]
  0044df98  8b7d1c                mov       edi,[ebp+1Ch]
  0044df9b  2bf9                  sub       edi,ecx
  0044df9d  c1ff02                sar       edi,2
  0044dfa0  03f9                  add       edi,ecx
  0044dfa2  8b4e24                mov       ecx,[esi+24h]
  0044dfa5  897e1c                mov       [esi+1Ch],edi
  0044dfa8  8b7d24                mov       edi,[ebp+24h]
  0044dfab  2bf9                  sub       edi,ecx
  0044dfad  c1ff02                sar       edi,2
  0044dfb0  03f9                  add       edi,ecx
  0044dfb2  8b4e20                mov       ecx,[esi+20h]
  0044dfb5  897e24                mov       [esi+24h],edi
  0044dfb8  8b7d20                mov       edi,[ebp+20h]
  0044dfbb  2bf9                  sub       edi,ecx
  0044dfbd  81c700a01400          add       edi,14A000h
  0044dfc3  c1ff02                sar       edi,2
  0044dfc6  03f9                  add       edi,ecx
  0044dfc8  897e20                mov       [esi+20h],edi
  0044dfcb  668b4d32              mov       cx,[ebp+32h]
  0044dfcf  66894e32              mov       [esi+32h],cx
  0044dfd3  b903000000            mov       ecx,3
  0044dfd8  66394810              cmp       [eax+10h],cx
  0044dfdc  7504                  jne       short 0044DFE2h
  0044dfde  66894e10              mov       [esi+10h],cx

LAB_0044dfe2:
  0044dfe2  66395610              cmp       [esi+10h],dx
  0044dfe6  bb01000000            mov       ebx,1
  0044dfeb  0f8574010000          jne       near 0044E165h
  0044dff1  6639969a000000        cmp       [esi+9Ah],dx
  0044dff8  752b                  jne       short 0044E025h
  0044dffa  b0fa                  mov       al,0FAh
  0044dffc  c646042e              mov       byte [esi+4],2Eh
  0044e000  c6460a02              mov       byte [esi+0Ah],2
  0044e004  885e0b                mov       [esi+0Bh],bl
  0044e007  66895628              mov       [esi+28h],dx
  0044e00b  6689562c              mov       [esi+2Ch],dx
  0044e00f  66c7462aa00f          mov       word [esi+2Ah],0FA0h
  0044e015  66899e9a000000        mov       [esi+9Ah],bx
  0044e01c  884638                mov       [esi+38h],al
  0044e01f  884639                mov       [esi+39h],al
  0044e022  88463a                mov       [esi+3Ah],al

LAB_0044e025:
  0044e025  66399e9a000000        cmp       [esi+9Ah],bx
  0044e02c  754c                  jne       short 0044E07Ah
  0044e02e  668b4628              mov       ax,[esi+28h]
  0044e032  8b4e5c                mov       ecx,[esi+5Ch]
  0044e035  0fbfd0                movsx     edx,ax
  0044e038  bf3c0f0000            mov       edi,0F3Ch
  0044e03d  bb3c0f0000            mov       ebx,0F3Ch
  0044e042  2bfa                  sub       edi,edx
  0044e044  668b562c              mov       dx,[esi+2Ch]
  0044e048  d3ff                  sar       edi,cl
  0044e04a  03c7                  add       eax,edi
  0044e04c  0fbffa                movsx     edi,dx
  0044e04f  2bdf                  sub       ebx,edi
  0044e051  66894628              mov       [esi+28h],ax
  0044e055  d3fb                  sar       ebx,cl
  0044e057  b9d80e0000            mov       ecx,0ED8h
  0044e05c  03da                  add       ebx,edx
  0044e05e  663bc1                cmp       ax,cx
  0044e061  66895e2c              mov       [esi+2Ch],bx
  0044e065  7c0e                  jl        short 0044E075h
  0044e067  66894e28              mov       [esi+28h],cx
  0044e06b  66894e2c              mov       [esi+2Ch],cx
  0044e06f  66c746106300          mov       word [esi+10h],63h

LAB_0044e075:
  0044e075  bb01000000            mov       ebx,1

LAB_0044e07a:
  0044e07a  6683be9a0000000a      cmp       word [esi+9Ah],0Ah
  0044e082  b90b000000            mov       ecx,0Bh
  0044e087  752f                  jne       short 0044E0B8h
  0044e089  b800100000            mov       eax,1000h
  0044e08e  c646042e              mov       byte [esi+4],2Eh
  0044e092  c6460a02              mov       byte [esi+0Ah],2
  0044e096  885e0b                mov       [esi+0Bh],bl
  0044e099  66894628              mov       [esi+28h],ax
  0044e09d  6689462c              mov       [esi+2Ch],ax
  0044e0a1  6689462a              mov       [esi+2Ah],ax
  0044e0a5  66898e9a000000        mov       [esi+9Ah],cx
  0044e0ac  c6463800              mov       byte [esi+38h],0
  0044e0b0  c6463900              mov       byte [esi+39h],0
  0044e0b4  c6463a00              mov       byte [esi+3Ah],0

LAB_0044e0b8:
  0044e0b8  66398e9a000000        cmp       [esi+9Ah],cx
  0044e0bf  751a                  jne       short 0044E0DBh
  0044e0c1  6a3c                  push      3Ch
  0044e0c3  6a1e                  push      1Eh
  0044e0c5  6a1e                  push      1Eh
  0044e0c7  56                    push      esi
  0044e0c8  e8136dfbff            call      00404DE0h
  0044e0cd  8a463a                mov       al,[esi+3Ah]
  0044e0d0  83c410                add       esp,10h
  0044e0d3  3cff                  cmp       al,0FFh
  0044e0d5  7504                  jne       short 0044E0DBh
  0044e0d7  66895e10              mov       [esi+10h],bx

LAB_0044e0db:
  0044e0db  6683be9a00000014      cmp       word [esi+9Ah],14h
  0044e0e3  b915000000            mov       ecx,15h
  0044e0e8  752f                  jne       short 0044E119h
  0044e0ea  b8401f0000            mov       eax,1F40h
  0044e0ef  c646042e              mov       byte [esi+4],2Eh
  0044e0f3  c6460a02              mov       byte [esi+0Ah],2
  0044e0f7  885e0b                mov       [esi+0Bh],bl
  0044e0fa  66894628              mov       [esi+28h],ax
  0044e0fe  6689462c              mov       [esi+2Ch],ax
  0044e102  6689462a              mov       [esi+2Ah],ax
  0044e106  66898e9a000000        mov       [esi+9Ah],cx
  0044e10d  c6463800              mov       byte [esi+38h],0
  0044e111  c6463900              mov       byte [esi+39h],0
  0044e115  c6463a00              mov       byte [esi+3Ah],0

LAB_0044e119:
  0044e119  66398e9a000000        cmp       [esi+9Ah],cx
  0044e120  7543                  jne       short 0044E165h
  0044e122  6a3c                  push      3Ch
  0044e124  6a1e                  push      1Eh
  0044e126  b833ffffff            mov       eax,0FFFFFF33h
  0044e12b  6a1e                  push      1Eh
  0044e12d  66014628              add       [esi+28h],ax
  0044e131  6601462a              add       [esi+2Ah],ax
  0044e135  6601462c              add       [esi+2Ch],ax
  0044e139  56                    push      esi
  0044e13a  e8a16cfbff            call      00404DE0h
  0044e13f  b868100000            mov       eax,1068h
  0044e144  83c410                add       esp,10h
  0044e147  66394628              cmp       [esi+28h],ax
  0044e14b  7f18                  jg        short 0044E165h
  0044e14d  807e38ff              cmp       byte [esi+38h],0FFh
  0044e151  7512                  jne       short 0044E165h
  0044e153  66894628              mov       [esi+28h],ax
  0044e157  6689462a              mov       [esi+2Ah],ax
  0044e15b  6689462c              mov       [esi+2Ch],ax
  0044e15f  66c746106300          mov       word [esi+10h],63h

LAB_0044e165:
  0044e165  66395e10              cmp       [esi+10h],bx
  0044e169  0f85e9000000          jne       near 0044E258h
  0044e16f  33ff                  xor       edi,edi
  0044e171  33db                  xor       ebx,ebx
  0044e173  897e44                mov       [esi+44h],edi
  0044e176  897e48                mov       [esi+48h],edi
  0044e179  897e4c                mov       [esi+4Ch],edi

LAB_0044e17c:
  0044e17c  8b461c                mov       eax,[esi+1Ch]
  0044e17f  8b4e20                mov       ecx,[esi+20h]
  0044e182  8b5624                mov       edx,[esi+24h]
  0044e185  8944243c              mov       [esp+3Ch],eax
  0044e189  0fbf4632              movsx     eax,word [esi+32h]
  0044e18d  0fbf6c3c54            movsx     ebp,word [esp+edi+54h]
  0044e192  50                    push      eax
  0044e193  894c2438              mov       [esp+38h],ecx
  0044e197  8954243c              mov       [esp+3Ch],edx
  0044e19b  e87c780f00            call      00545A1Ch
  0044e1a0  8bd0                  mov       edx,eax
  0044e1a2  0fbf4e32              movsx     ecx,word [esi+32h]
  0044e1a6  0fafd5                imul      edx,ebp
  0044e1a9  51                    push      ecx
  0044e1aa  89542444              mov       [esp+44h],edx
  0044e1ae  e863780f00            call      00545A16h
  0044e1b3  8bd0                  mov       edx,eax
  0044e1b5  8b6e1c                mov       ebp,[esi+1Ch]
  0044e1b8  0fbf443c60            movsx     eax,word [esp+edi+60h]
  0044e1bd  0fafd0                imul      edx,eax
  0044e1c0  8b442440              mov       eax,[esp+40h]
  0044e1c4  0fbf4e32              movsx     ecx,word [esi+32h]
  0044e1c8  2bd0                  sub       edx,eax
  0044e1ca  51                    push      ecx
  0044e1cb  03ea                  add       ebp,edx
  0044e1cd  896e1c                mov       [esi+1Ch],ebp
  0044e1d0  e841780f00            call      00545A16h
  0044e1d5  0fbf5632              movsx     edx,word [esi+32h]
  0044e1d9  8be8                  mov       ebp,eax
  0044e1db  52                    push      edx
  0044e1dc  0fbf443c58            movsx     eax,word [esp+edi+58h]
  0044e1e1  0fafe8                imul      ebp,eax
  0044e1e4  e833780f00            call      00545A1Ch
  0044e1e9  8bc8                  mov       ecx,eax
  0044e1eb  6aff                  push      0FFFFFFFFh
  0044e1ed  0fbf443c64            movsx     eax,word [esp+edi+64h]
  0044e1f2  0fafc8                imul      ecx,eax
  0044e1f5  0fbf543c70            movsx     edx,word [esp+edi+70h]
  0044e1fa  03e9                  add       ebp,ecx
  0044e1fc  8b4e24                mov       ecx,[esi+24h]
  0044e1ff  2bcd                  sub       ecx,ebp
  0044e201  8b6e20                mov       ebp,[esi+20h]
  0044e204  c1e20c                shl       edx,0Ch
  0044e207  03ea                  add       ebp,edx
  0044e209  6a00                  push      0
  0044e20b  896e20                mov       [esi+20h],ebp
  0044e20e  668b6e14              mov       bp,[esi+14h]
  0044e212  689d000000            push      9Dh
  0044e217  56                    push      esi
  0044e218  894e24                mov       [esi+24h],ecx
  0044e21b  66895e14              mov       [esi+14h],bx
  0044e21f  e8bcc80200            call      0047AAE0h
  0044e224  8b44244c              mov       eax,[esp+4Ch]
  0044e228  8b4c2444              mov       ecx,[esp+44h]
  0044e22c  8b542448              mov       edx,[esp+48h]
  0044e230  83c410                add       esp,10h
  0044e233  43                    inc       ebx
  0044e234  83c702                add       edi,2
  0044e237  6683fb05              cmp       bx,5
  0044e23b  66896e14              mov       [esi+14h],bp
  0044e23f  89461c                mov       [esi+1Ch],eax
  0044e242  894e20                mov       [esi+20h],ecx
  0044e245  895624                mov       [esi+24h],edx
  0044e248  0f8e2effffff          jle       near 0044E17Ch
  0044e24e  8b6c2430              mov       ebp,[esp+30h]
  0044e252  66c746100200          mov       word [esi+10h],2

LAB_0044e258:
  0044e258  66837e1002            cmp       word [esi+10h],2
  0044e25d  0f85a6070000          jne       near 0044EA09h
  0044e263  8b5e20                mov       ebx,[esi+20h]
  0044e266  6aff                  push      0FFFFFFFFh
  0044e268  8bfb                  mov       edi,ebx
  0044e26a  6a00                  push      0
  0044e26c  81c70000f6ff          add       edi,0FFF60000h
  0044e272  68a9000000            push      0A9h
  0044e277  897e20                mov       [esi+20h],edi
  0044e27a  8d7e1c                lea       edi,[esi+1Ch]
  0044e27d  6a00                  push      0
  0044e27f  57                    push      edi
  0044e280  56                    push      esi
  0044e281  66c7869a0000000900    mov       word [esi+9Ah],9
  0044e28a  66c746146400          mov       word [esi+14h],64h
  0044e290  895c244c              mov       [esp+4Ch],ebx
  0044e294  e8a7c90200            call      0047AC40h
  0044e299  895e20                mov       [esi+20h],ebx
  0044e29c  8b4d00                mov       ecx,[ebp]
  0044e29f  83c418                add       esp,18h
  0044e2a2  33c0                  xor       eax,eax
  0044e2a4  f7c100008000          test      ecx,800000h
  0044e2aa  89442414              mov       [esp+14h],eax
  0044e2ae  89442418              mov       [esp+18h],eax
  0044e2b2  0f841f030000          je        near 0044E5D7h
  0044e2b8  bdc0ca5e00            mov       ebp,5ECAC0h

LAB_0044e2bd:
  0044e2bd  8b467c                mov       eax,[esi+7Ch]
  0044e2c0  668b8e90000000        mov       cx,[esi+90h]
  0044e2c7  8944244c              mov       [esp+4Ch],eax
  0044e2cb  8b4664                mov       eax,[esi+64h]
  0044e2ce  8b5660                mov       edx,[esi+60h]
  0044e2d1  89442450              mov       [esp+50h],eax
  0044e2d5  8b8680000000          mov       eax,[esi+80h]
  0044e2db  66894c2448            mov       [esp+48h],cx
  0044e2e0  8b0f                  mov       ecx,[edi]
  0044e2e2  89442438              mov       [esp+38h],eax
  0044e2e6  8b8684000000          mov       eax,[esi+84h]
  0044e2ec  89542444              mov       [esp+44h],edx
  0044e2f0  8b5624                mov       edx,[esi+24h]
  0044e2f3  8944243c              mov       [esp+3Ch],eax
  0044e2f7  8b8688000000          mov       eax,[esi+88h]
  0044e2fd  894e44                mov       [esi+44h],ecx
  0044e300  89442440              mov       [esp+40h],eax
  0044e304  8b868c000000          mov       eax,[esi+8Ch]
  0044e30a  895e48                mov       [esi+48h],ebx
  0044e30d  89564c                mov       [esi+4Ch],edx
  0044e310  89442430              mov       [esp+30h],eax
  0044e314  8b45fc                mov       eax,[ebp-4]
  0044e317  894c2424              mov       [esp+24h],ecx
  0044e31b  2bc1                  sub       eax,ecx
  0044e31d  8b4d00                mov       ecx,[ebp]
  0044e320  8944241c              mov       [esp+1Ch],eax
  0044e324  2bcb                  sub       ecx,ebx
  0044e326  8b5d04                mov       ebx,[ebp+4]
  0044e329  2bda                  sub       ebx,edx
  0044e32b  8954242c              mov       [esp+2Ch],edx
  0044e32f  c1f80c                sar       eax,0Ch
  0044e332  895c2420              mov       [esp+20h],ebx
  0044e336  8bd3                  mov       edx,ebx
  0044e338  894c2428              mov       [esp+28h],ecx
  0044e33c  8bd8                  mov       ebx,eax
  0044e33e  c1f90c                sar       ecx,0Ch
  0044e341  0fafd8                imul      ebx,eax
  0044e344  8bc1                  mov       eax,ecx
  0044e346  c1fa0c                sar       edx,0Ch
  0044e349  0fafc1                imul      eax,ecx
  0044e34c  8bca                  mov       ecx,edx
  0044e34e  03d8                  add       ebx,eax
  0044e350  0fafca                imul      ecx,edx
  0044e353  03d9                  add       ebx,ecx
  0044e355  53                    push      ebx
  0044e356  e8b5760f00            call      00545A10h
  0044e35b  3d20030000            cmp       eax,320h
  0044e360  0f8df5010000          jge       near 0044E55Bh
  0044e366  8b54241c              mov       edx,[esp+1Ch]
  0044e36a  8b4c2428              mov       ecx,[esp+28h]
  0044e36e  8b442420              mov       eax,[esp+20h]
  0044e372  8d7e1c                lea       edi,[esi+1Ch]
  0044e375  c1fa02                sar       edx,2
  0044e378  c1f902                sar       ecx,2
  0044e37b  c1f802                sar       eax,2
  0044e37e  33db                  xor       ebx,ebx
  0044e380  8954241c              mov       [esp+1Ch],edx
  0044e384  894c2428              mov       [esp+28h],ecx
  0044e388  89442420              mov       [esp+20h],eax
  0044e38c  895c2410              mov       [esp+10h],ebx

LAB_0044e390:
  0044e390  8b54241c              mov       edx,[esp+1Ch]
  0044e394  8b07                  mov       eax,[edi]
  0044e396  8b4e20                mov       ecx,[esi+20h]
  0044e399  03c2                  add       eax,edx
  0044e39b  8907                  mov       [edi],eax
  0044e39d  8b442428              mov       eax,[esp+28h]
  0044e3a1  03c8                  add       ecx,eax
  0044e3a3  8b4624                mov       eax,[esi+24h]
  0044e3a6  894e20                mov       [esi+20h],ecx
  0044e3a9  8b4c2420              mov       ecx,[esp+20h]
  0044e3ad  03c1                  add       eax,ecx
  0044e3af  56                    push      esi
  0044e3b0  894624                mov       [esi+24h],eax
  0044e3b3  e888550e00            call      00533940h
  0044e3b8  8b8690000000          mov       eax,[esi+90h]
  0044e3be  83c404                add       esp,4
  0044e3c1  f6c488                test      ah,88h
  0044e3c4  740d                  je        short 0044E3D3h
  0044e3c6  bb63000000            mov       ebx,63h
  0044e3cb  c744241001000000      mov       dword [esp+10h],1

LAB_0044e3d3:
  0044e3d3  43                    inc       ebx
  0044e3d4  6683fb03              cmp       bx,3
  0044e3d8  7eb6                  jle       short 0044E390h
  0044e3da  66837c241001          cmp       word [esp+10h],1
  0044e3e0  0f8475010000          je        near 0044E55Bh
  0044e3e6  8d9424bc000000        lea       edx,[esp+0BCh]
  0044e3ed  8d8424c0000000        lea       eax,[esp+0C0h]
  0044e3f4  52                    push      edx
  0044e3f5  8d4c247c              lea       ecx,[esp+7Ch]
  0044e3f9  8d5de0                lea       ebx,[ebp-20h]
  0044e3fc  50                    push      eax
  0044e3fd  51                    push      ecx
  0044e3fe  53                    push      ebx
  0044e3ff  e87ca50e00            call      00538980h
  0044e404  8b1b                  mov       ebx,[ebx]
  0044e406  83c410                add       esp,10h
  0044e409  f7c300000080          test      ebx,80000000h
  0044e40f  0f8446010000          je        near 0044E55Bh
  0044e415  f7c300200040          test      ebx,40002000h
  0044e41b  0f853a010000          jne       near 0044E55Bh
  0044e421  8b542478              mov       edx,[esp+78h]
  0044e425  f7421480000001        test      dword [edx+14h],1000080h
  0044e42c  0f8429010000          je        near 0044E55Bh
  0044e432  8b466c                mov       eax,[esi+6Ch]
  0044e435  894554                mov       [ebp+54h],eax
  0044e438  8b442414              mov       eax,[esp+14h]
  0044e43c  40                    inc       eax
  0044e43d  663d0700              cmp       ax,7
  0044e441  89442414              mov       [esp+14h],eax
  0044e445  0f8503010000          jne       near 0044E54Eh
  0044e44b  8b442424              mov       eax,[esp+24h]
  0044e44f  8b4dfc                mov       ecx,[ebp-4]
  0044e452  c1f80c                sar       eax,0Ch
  0044e455  c1f90c                sar       ecx,0Ch
  0044e458  2bc1                  sub       eax,ecx
  0044e45a  8b4d00                mov       ecx,[ebp]
  0044e45d  8b7d04                mov       edi,[ebp+4]
  0044e460  8bd0                  mov       edx,eax
  0044e462  8b442434              mov       eax,[esp+34h]
  0044e466  c1f80c                sar       eax,0Ch
  0044e469  c1f90c                sar       ecx,0Ch
  0044e46c  2bc1                  sub       eax,ecx
  0044e46e  8b4c242c              mov       ecx,[esp+2Ch]
  0044e472  c1f90c                sar       ecx,0Ch
  0044e475  c1ff0c                sar       edi,0Ch
  0044e478  2bcf                  sub       ecx,edi
  0044e47a  8bfa                  mov       edi,edx
  0044e47c  0faffa                imul      edi,edx
  0044e47f  8bd0                  mov       edx,eax
  0044e481  0fafd0                imul      edx,eax
  0044e484  8bc1                  mov       eax,ecx
  0044e486  03fa                  add       edi,edx
  0044e488  0fafc1                imul      eax,ecx
  0044e48b  03f8                  add       edi,eax
  0044e48d  57                    push      edi
  0044e48e  e87d750f00            call      00545A10h
  0044e493  8944242c              mov       [esp+2Ch],eax
  0044e497  33c0                  xor       eax,eax
  0044e499  89442410              mov       [esp+10h],eax
  0044e49d  8d7e1c                lea       edi,[esi+1Ch]

LAB_0044e4a0:
  0044e4a0  0fbfc8                movsx     ecx,ax
  0044e4a3  8d444c7c              lea       eax,[esp+ecx*2+7Ch]
  0044e4a7  89442414              mov       [esp+14h],eax
  0044e4ab  0fbf00                movsx     eax,word [eax]
  0044e4ae  8d1480                lea       edx,[eax+eax*4]
  0044e4b1  c1e203                shl       edx,3
  0044e4b4  2bd0                  sub       edx,eax
  0044e4b6  8b07                  mov       eax,[edi]
  0044e4b8  c1f80c                sar       eax,0Ch
  0044e4bb  8b0c9520ca5e00        mov       ecx,[edx*4+5ECA20h]
  0044e4c2  8d149504ca5e00        lea       edx,[edx*4+5ECA04h]
  0044e4c9  c1f90c                sar       ecx,0Ch
  0044e4cc  8b5a20                mov       ebx,[edx+20h]
  0044e4cf  2bc1                  sub       eax,ecx
  0044e4d1  8b4e20                mov       ecx,[esi+20h]
  0044e4d4  89542424              mov       [esp+24h],edx
  0044e4d8  c1fb0c                sar       ebx,0Ch
  0044e4db  c1f90c                sar       ecx,0Ch
  0044e4de  2bcb                  sub       ecx,ebx
  0044e4e0  8b5a24                mov       ebx,[edx+24h]
  0044e4e3  8b5624                mov       edx,[esi+24h]
  0044e4e6  c1fb0c                sar       ebx,0Ch
  0044e4e9  c1fa0c                sar       edx,0Ch
  0044e4ec  2bd3                  sub       edx,ebx
  0044e4ee  8bd8                  mov       ebx,eax
  0044e4f0  0fafd8                imul      ebx,eax
  0044e4f3  8bc1                  mov       eax,ecx
  0044e4f5  0fafc1                imul      eax,ecx
  0044e4f8  8bca                  mov       ecx,edx
  0044e4fa  03d8                  add       ebx,eax
  0044e4fc  0fafca                imul      ecx,edx
  0044e4ff  03d9                  add       ebx,ecx
  0044e501  53                    push      ebx
  0044e502  e809750f00            call      00545A10h
  0044e507  3944242c              cmp       [esp+2Ch],eax
  0044e50b  7d24                  jge       short 0044E531h
  0044e50d  8b442424              mov       eax,[esp+24h]
  0044e511  8b4c2418              mov       ecx,[esp+18h]
  0044e515  c7455400000000        mov       dword [ebp+54h],0
  0044e51c  8b566c                mov       edx,[esi+6Ch]
  0044e51f  895074                mov       [eax+74h],edx
  0044e522  8b542414              mov       edx,[esp+14h]
  0044e526  41                    inc       ecx
  0044e527  b806000000            mov       eax,6
  0044e52c  66890a                mov       [edx],cx
  0044e52f  eb04                  jmp       short 0044E535h

LAB_0044e531:
  0044e531  8b442410              mov       eax,[esp+10h]

LAB_0044e535:
  0044e535  40                    inc       eax
  0044e536  663d0500              cmp       ax,5
  0044e53a  89442410              mov       [esp+10h],eax
  0044e53e  0f8e5cffffff          jle       near 0044E4A0h
  0044e544  c744241406000000      mov       dword [esp+14h],6
  0044e54c  eb0d                  jmp       short 0044E55Bh

LAB_0044e54e:
  0044e54e  8b4c2418              mov       ecx,[esp+18h]
  0044e552  0fbfd0                movsx     edx,ax
  0044e555  41                    inc       ecx
  0044e556  66894c547a            mov       [esp+edx*2+7Ah],cx

LAB_0044e55b:
  0044e55b  8b44244c              mov       eax,[esp+4Ch]
  0044e55f  8b4c2444              mov       ecx,[esp+44h]
  0044e563  89467c                mov       [esi+7Ch],eax
  0044e566  8b442448              mov       eax,[esp+48h]
  0044e56a  8b542450              mov       edx,[esp+50h]
  0044e56e  25ffff0000            and       eax,0FFFFh
  0044e573  898690000000          mov       [esi+90h],eax
  0044e579  8b4644                mov       eax,[esi+44h]
  0044e57c  8b5c2434              mov       ebx,[esp+34h]
  0044e580  8907                  mov       [edi],eax
  0044e582  8b44243c              mov       eax,[esp+3Ch]
  0044e586  894e60                mov       [esi+60h],ecx
  0044e589  8b4e4c                mov       ecx,[esi+4Ch]
  0044e58c  895664                mov       [esi+64h],edx
  0044e58f  8b542438              mov       edx,[esp+38h]
  0044e593  898684000000          mov       [esi+84h],eax
  0044e599  8b442418              mov       eax,[esp+18h]
  0044e59d  81c59c000000          add       ebp,9Ch
  0044e5a3  894e24                mov       [esi+24h],ecx
  0044e5a6  8b4c2440              mov       ecx,[esp+40h]
  0044e5aa  899680000000          mov       [esi+80h],edx
  0044e5b0  8b542430              mov       edx,[esp+30h]
  0044e5b4  40                    inc       eax
  0044e5b5  895e20                mov       [esi+20h],ebx
  0044e5b8  663d2000              cmp       ax,20h
  0044e5bc  898e88000000          mov       [esi+88h],ecx
  0044e5c2  89968c000000          mov       [esi+8Ch],edx
  0044e5c8  89442418              mov       [esp+18h],eax
  0044e5cc  0f8cebfcffff          jl        near 0044E2BDh
  0044e5d2  e9f2020000            jmp       0044E8C9h

LAB_0044e5d7:
  0044e5d7  bddc935e00            mov       ebp,5E93DCh

LAB_0044e5dc:
  0044e5dc  8b467c                mov       eax,[esi+7Ch]
  0044e5df  668b8e90000000        mov       cx,[esi+90h]
  0044e5e6  8944244c              mov       [esp+4Ch],eax
  0044e5ea  8b4664                mov       eax,[esi+64h]
  0044e5ed  8b5660                mov       edx,[esi+60h]
  0044e5f0  89442450              mov       [esp+50h],eax
  0044e5f4  8b8680000000          mov       eax,[esi+80h]
  0044e5fa  66894c2448            mov       [esp+48h],cx
  0044e5ff  8b0f                  mov       ecx,[edi]
  0044e601  89442438              mov       [esp+38h],eax
  0044e605  8b8684000000          mov       eax,[esi+84h]
  0044e60b  89542444              mov       [esp+44h],edx
  0044e60f  8b5624                mov       edx,[esi+24h]
  0044e612  8944243c              mov       [esp+3Ch],eax
  0044e616  8b8688000000          mov       eax,[esi+88h]
  0044e61c  894e44                mov       [esi+44h],ecx
  0044e61f  89442440              mov       [esp+40h],eax
  0044e623  8b868c000000          mov       eax,[esi+8Ch]
  0044e629  895e48                mov       [esi+48h],ebx
  0044e62c  89564c                mov       [esi+4Ch],edx
  0044e62f  89442430              mov       [esp+30h],eax
  0044e633  8b4500                mov       eax,[ebp]
  0044e636  894c2424              mov       [esp+24h],ecx
  0044e63a  2bc1                  sub       eax,ecx
  0044e63c  8b4d04                mov       ecx,[ebp+4]
  0044e63f  8944241c              mov       [esp+1Ch],eax
  0044e643  2bcb                  sub       ecx,ebx
  0044e645  8b5d08                mov       ebx,[ebp+8]
  0044e648  2bda                  sub       ebx,edx
  0044e64a  8954242c              mov       [esp+2Ch],edx
  0044e64e  c1f80c                sar       eax,0Ch
  0044e651  895c2420              mov       [esp+20h],ebx
  0044e655  8bd3                  mov       edx,ebx
  0044e657  894c2428              mov       [esp+28h],ecx
  0044e65b  8bd8                  mov       ebx,eax
  0044e65d  c1f90c                sar       ecx,0Ch
  0044e660  0fafd8                imul      ebx,eax
  0044e663  8bc1                  mov       eax,ecx
  0044e665  c1fa0c                sar       edx,0Ch
  0044e668  0fafc1                imul      eax,ecx
  0044e66b  8bca                  mov       ecx,edx
  0044e66d  03d8                  add       ebx,eax
  0044e66f  0fafca                imul      ecx,edx
  0044e672  03d9                  add       ebx,ecx
  0044e674  53                    push      ebx
  0044e675  e896730f00            call      00545A10h
  0044e67a  3d20030000            cmp       eax,320h
  0044e67f  0f8dcd010000          jge       near 0044E852h
  0044e685  8b54241c              mov       edx,[esp+1Ch]
  0044e689  8b4c2428              mov       ecx,[esp+28h]
  0044e68d  8b442420              mov       eax,[esp+20h]
  0044e691  8d7e1c                lea       edi,[esi+1Ch]
  0044e694  c1fa02                sar       edx,2
  0044e697  c1f902                sar       ecx,2
  0044e69a  c1f802                sar       eax,2
  0044e69d  33db                  xor       ebx,ebx
  0044e69f  8954241c              mov       [esp+1Ch],edx
  0044e6a3  894c2428              mov       [esp+28h],ecx
  0044e6a7  89442420              mov       [esp+20h],eax
  0044e6ab  895c2410              mov       [esp+10h],ebx

LAB_0044e6af:
  0044e6af  8b54241c              mov       edx,[esp+1Ch]
  0044e6b3  8b07                  mov       eax,[edi]
  0044e6b5  8b4e20                mov       ecx,[esi+20h]
  0044e6b8  03c2                  add       eax,edx
  0044e6ba  8907                  mov       [edi],eax
  0044e6bc  8b442428              mov       eax,[esp+28h]
  0044e6c0  03c8                  add       ecx,eax
  0044e6c2  8b4624                mov       eax,[esi+24h]
  0044e6c5  894e20                mov       [esi+20h],ecx
  0044e6c8  8b4c2420              mov       ecx,[esp+20h]
  0044e6cc  03c1                  add       eax,ecx
  0044e6ce  56                    push      esi
  0044e6cf  894624                mov       [esi+24h],eax
  0044e6d2  e869520e00            call      00533940h
  0044e6d7  8b8690000000          mov       eax,[esi+90h]
  0044e6dd  83c404                add       esp,4
  0044e6e0  f6c488                test      ah,88h
  0044e6e3  740d                  je        short 0044E6F2h
  0044e6e5  bb63000000            mov       ebx,63h
  0044e6ea  c744241001000000      mov       dword [esp+10h],1

LAB_0044e6f2:
  0044e6f2  43                    inc       ebx
  0044e6f3  6683fb03              cmp       bx,3
  0044e6f7  7eb6                  jle       short 0044E6AFh
  0044e6f9  66837c241001          cmp       word [esp+10h],1
  0044e6ff  0f844d010000          je        near 0044E852h
  0044e705  8b45e4                mov       eax,[ebp-1Ch]
  0044e708  a900000080            test      eax,80000000h
  0044e70d  0f843f010000          je        near 0044E852h
  0044e713  a900200040            test      eax,40002000h
  0044e718  0f8534010000          jne       near 0044E852h
  0044e71e  66837df600            cmp       word [ebp-0Ah],0
  0044e723  0f8529010000          jne       near 0044E852h
  0044e729  8b442414              mov       eax,[esp+14h]
  0044e72d  8b566c                mov       edx,[esi+6Ch]
  0044e730  40                    inc       eax
  0044e731  895558                mov       [ebp+58h],edx
  0044e734  663d0700              cmp       ax,7
  0044e738  89442414              mov       [esp+14h],eax
  0044e73c  0f8503010000          jne       near 0044E845h
  0044e742  8b442424              mov       eax,[esp+24h]
  0044e746  8b4d00                mov       ecx,[ebp]
  0044e749  c1f80c                sar       eax,0Ch
  0044e74c  c1f90c                sar       ecx,0Ch
  0044e74f  2bc1                  sub       eax,ecx
  0044e751  8b4d04                mov       ecx,[ebp+4]
  0044e754  8b7d08                mov       edi,[ebp+8]
  0044e757  8bd0                  mov       edx,eax
  0044e759  8b442434              mov       eax,[esp+34h]
  0044e75d  c1f80c                sar       eax,0Ch
  0044e760  c1f90c                sar       ecx,0Ch
  0044e763  2bc1                  sub       eax,ecx
  0044e765  8b4c242c              mov       ecx,[esp+2Ch]
  0044e769  c1f90c                sar       ecx,0Ch
  0044e76c  c1ff0c                sar       edi,0Ch
  0044e76f  2bcf                  sub       ecx,edi
  0044e771  8bfa                  mov       edi,edx
  0044e773  0faffa                imul      edi,edx
  0044e776  8bd0                  mov       edx,eax
  0044e778  0fafd0                imul      edx,eax
  0044e77b  8bc1                  mov       eax,ecx
  0044e77d  03fa                  add       edi,edx
  0044e77f  0fafc1                imul      eax,ecx
  0044e782  03f8                  add       edi,eax
  0044e784  57                    push      edi
  0044e785  e886720f00            call      00545A10h
  0044e78a  8944242c              mov       [esp+2Ch],eax
  0044e78e  33c0                  xor       eax,eax
  0044e790  89442410              mov       [esp+10h],eax
  0044e794  8d7e1c                lea       edi,[esi+1Ch]

LAB_0044e797:
  0044e797  0fbfc8                movsx     ecx,ax
  0044e79a  8d444c7c              lea       eax,[esp+ecx*2+7Ch]
  0044e79e  89442414              mov       [esp+14h],eax
  0044e7a2  0fbf00                movsx     eax,word [eax]
  0044e7a5  8d1480                lea       edx,[eax+eax*4]
  0044e7a8  c1e203                shl       edx,3
  0044e7ab  2bd0                  sub       edx,eax
  0044e7ad  8b07                  mov       eax,[edi]
  0044e7af  c1f80c                sar       eax,0Ch
  0044e7b2  8b0c9520ca5e00        mov       ecx,[edx*4+5ECA20h]
  0044e7b9  8d149504ca5e00        lea       edx,[edx*4+5ECA04h]
  0044e7c0  c1f90c                sar       ecx,0Ch
  0044e7c3  8b5a20                mov       ebx,[edx+20h]
  0044e7c6  2bc1                  sub       eax,ecx
  0044e7c8  8b4e20                mov       ecx,[esi+20h]
  0044e7cb  89542424              mov       [esp+24h],edx
  0044e7cf  c1fb0c                sar       ebx,0Ch
  0044e7d2  c1f90c                sar       ecx,0Ch
  0044e7d5  2bcb                  sub       ecx,ebx
  0044e7d7  8b5a24                mov       ebx,[edx+24h]
  0044e7da  8b5624                mov       edx,[esi+24h]
  0044e7dd  c1fb0c                sar       ebx,0Ch
  0044e7e0  c1fa0c                sar       edx,0Ch
  0044e7e3  2bd3                  sub       edx,ebx
  0044e7e5  8bd8                  mov       ebx,eax
  0044e7e7  0fafd8                imul      ebx,eax
  0044e7ea  8bc1                  mov       eax,ecx
  0044e7ec  0fafc1                imul      eax,ecx
  0044e7ef  8bca                  mov       ecx,edx
  0044e7f1  03d8                  add       ebx,eax
  0044e7f3  0fafca                imul      ecx,edx
  0044e7f6  03d9                  add       ebx,ecx
  0044e7f8  53                    push      ebx
  0044e7f9  e812720f00            call      00545A10h
  0044e7fe  3944242c              cmp       [esp+2Ch],eax
  0044e802  7d24                  jge       short 0044E828h
  0044e804  8b442424              mov       eax,[esp+24h]
  0044e808  8b4c2418              mov       ecx,[esp+18h]
  0044e80c  c7455800000000        mov       dword [ebp+58h],0
  0044e813  8b566c                mov       edx,[esi+6Ch]
  0044e816  895074                mov       [eax+74h],edx
  0044e819  8b542414              mov       edx,[esp+14h]
  0044e81d  41                    inc       ecx
  0044e81e  b806000000            mov       eax,6
  0044e823  66890a                mov       [edx],cx
  0044e826  eb04                  jmp       short 0044E82Ch

LAB_0044e828:
  0044e828  8b442410              mov       eax,[esp+10h]

LAB_0044e82c:
  0044e82c  40                    inc       eax
  0044e82d  663d0500              cmp       ax,5
  0044e831  89442410              mov       [esp+10h],eax
  0044e835  0f8e5cffffff          jle       near 0044E797h
  0044e83b  c744241406000000      mov       dword [esp+14h],6
  0044e843  eb0d                  jmp       short 0044E852h

LAB_0044e845:
  0044e845  8b4c2418              mov       ecx,[esp+18h]
  0044e849  0fbfd0                movsx     edx,ax
  0044e84c  41                    inc       ecx
  0044e84d  66894c547a            mov       [esp+edx*2+7Ah],cx

LAB_0044e852:
  0044e852  8b44244c              mov       eax,[esp+4Ch]
  0044e856  8b4c2444              mov       ecx,[esp+44h]
  0044e85a  89467c                mov       [esi+7Ch],eax
  0044e85d  8b442448              mov       eax,[esp+48h]
  0044e861  8b542450              mov       edx,[esp+50h]
  0044e865  25ffff0000            and       eax,0FFFFh
  0044e86a  898690000000          mov       [esi+90h],eax
  0044e870  8b4644                mov       eax,[esi+44h]
  0044e873  8b5c2434              mov       ebx,[esp+34h]
  0044e877  8907                  mov       [edi],eax
  0044e879  8b44243c              mov       eax,[esp+3Ch]
  0044e87d  894e60                mov       [esi+60h],ecx
  0044e880  8b4e4c                mov       ecx,[esi+4Ch]
  0044e883  895664                mov       [esi+64h],edx
  0044e886  8b542438              mov       edx,[esp+38h]
  0044e88a  898684000000          mov       [esi+84h],eax
  0044e890  8b442418              mov       eax,[esp+18h]
  0044e894  81c59c000000          add       ebp,9Ch
  0044e89a  894e24                mov       [esi+24h],ecx
  0044e89d  8b4c2440              mov       ecx,[esp+40h]
  0044e8a1  899680000000          mov       [esi+80h],edx
  0044e8a7  8b542430              mov       edx,[esp+30h]
  0044e8ab  40                    inc       eax
  0044e8ac  895e20                mov       [esi+20h],ebx
  0044e8af  663d0300              cmp       ax,3
  0044e8b3  898e88000000          mov       [esi+88h],ecx
  0044e8b9  89968c000000          mov       [esi+8Ch],edx
  0044e8bf  89442418              mov       [esp+18h],eax
  0044e8c3  0f8c13fdffff          jl        near 0044E5DCh

LAB_0044e8c9:
  0044e8c9  0fbf442414            movsx     eax,word [esp+14h]
  0044e8ce  83f806                cmp       eax,6
  0044e8d1  0f8732010000          ja        near 0044EA09h
  0044e8d7  ff248538ea4400        jmp       dword [eax*4+44EA38h]
  0044e8de  33c0                  xor       eax,eax
  0044e8e0  894644                mov       [esi+44h],eax
  0044e8e3  894648                mov       [esi+48h],eax
  0044e8e6  89464c                mov       [esi+4Ch],eax
  0044e8e9  e91b010000            jmp       0044EA09h
  0044e8ee  0fbf4c247c            movsx     ecx,word [esp+7Ch]
  0044e8f3  8bc1                  mov       eax,ecx
  0044e8f5  c1e010                shl       eax,10h
  0044e8f8  0bc1                  or        eax,ecx
  0044e8fa  894644                mov       [esi+44h],eax
  0044e8fd  894648                mov       [esi+48h],eax
  0044e900  89464c                mov       [esi+4Ch],eax
  0044e903  e901010000            jmp       0044EA09h
  0044e908  0fbf54247e            movsx     edx,word [esp+7Eh]
  0044e90d  0fbf44247c            movsx     eax,word [esp+7Ch]
  0044e912  8bca                  mov       ecx,edx
  0044e914  c1e110                shl       ecx,10h
  0044e917  0bc8                  or        ecx,eax
  0044e919  c1e010                shl       eax,10h
  0044e91c  0bc2                  or        eax,edx
  0044e91e  894e44                mov       [esi+44h],ecx
  0044e921  894648                mov       [esi+48h],eax
  0044e924  894e4c                mov       [esi+4Ch],ecx
  0044e927  e9dd000000            jmp       0044EA09h
  0044e92c  0fbf4c247c            movsx     ecx,word [esp+7Ch]
  0044e931  0fbf44247e            movsx     eax,word [esp+7Eh]
  0044e936  8bd1                  mov       edx,ecx
  0044e938  c1e210                shl       edx,10h
  0044e93b  0bd0                  or        edx,eax
  0044e93d  895644                mov       [esi+44h],edx
  0044e940  0fbf942480000000      movsx     edx,word [esp+80h]
  0044e948  8bfa                  mov       edi,edx
  0044e94a  c1e710                shl       edi,10h
  0044e94d  c1e010                shl       eax,10h
  0044e950  0bf9                  or        edi,ecx
  0044e952  0bc2                  or        eax,edx
  0044e954  897e48                mov       [esi+48h],edi
  0044e957  89464c                mov       [esi+4Ch],eax
  0044e95a  e9aa000000            jmp       0044EA09h
  0044e95f  0fbf44247c            movsx     eax,word [esp+7Ch]
  0044e964  0fbf4c247e            movsx     ecx,word [esp+7Eh]
  0044e969  0fbf942480000000      movsx     edx,word [esp+80h]
  0044e971  c1e010                shl       eax,10h
  0044e974  0bc1                  or        eax,ecx
  0044e976  0fbf8c2482000000      movsx     ecx,word [esp+82h]
  0044e97e  c1e210                shl       edx,10h
  0044e981  0bd1                  or        edx,ecx
  0044e983  894644                mov       [esi+44h],eax
  0044e986  895648                mov       [esi+48h],edx
  0044e989  89464c                mov       [esi+4Ch],eax
  0044e98c  eb7b                  jmp       short 0044EA09h
  0044e98e  0fbf44247c            movsx     eax,word [esp+7Ch]
  0044e993  0fbf8c2480000000      movsx     ecx,word [esp+80h]
  0044e99b  8bd0                  mov       edx,eax
  0044e99d  c1e210                shl       edx,10h
  0044e9a0  0bd0                  or        edx,eax
  0044e9a2  0fbf44247e            movsx     eax,word [esp+7Eh]
  0044e9a7  895644                mov       [esi+44h],edx
  0044e9aa  0fbf942482000000      movsx     edx,word [esp+82h]
  0044e9b2  c1e010                shl       eax,10h
  0044e9b5  0bc1                  or        eax,ecx
  0044e9b7  894648                mov       [esi+48h],eax
  0044e9ba  0fbf842484000000      movsx     eax,word [esp+84h]
  0044e9c2  c1e210                shl       edx,10h
  0044e9c5  eb3d                  jmp       short 0044EA04h
  0044e9c7  0fbf4c247c            movsx     ecx,word [esp+7Ch]
  0044e9cc  0fbf54247e            movsx     edx,word [esp+7Eh]
  0044e9d1  0fbf842480000000      movsx     eax,word [esp+80h]
  0044e9d9  c1e110                shl       ecx,10h
  0044e9dc  0bca                  or        ecx,edx
  0044e9de  0fbf942484000000      movsx     edx,word [esp+84h]
  0044e9e6  894e44                mov       [esi+44h],ecx
  0044e9e9  0fbf8c2482000000      movsx     ecx,word [esp+82h]
  0044e9f1  c1e010                shl       eax,10h
  0044e9f4  0bc1                  or        eax,ecx
  0044e9f6  894648                mov       [esi+48h],eax
  0044e9f9  0fbf842486000000      movsx     eax,word [esp+86h]
  0044ea01  c1e210                shl       edx,10h

LAB_0044ea04:
  0044ea04  0bd0                  or        edx,eax
  0044ea06  89564c                mov       [esi+4Ch],edx

LAB_0044ea09:
  0044ea09  66837e1003            cmp       word [esi+10h],3
  0044ea0e  751c                  jne       short 0044EA2Ch
  0044ea10  6afb                  push      0FFFFFFFBh
  0044ea12  6afb                  push      0FFFFFFFBh
  0044ea14  6afb                  push      0FFFFFFFBh
  0044ea16  56                    push      esi
  0044ea17  e8c463fbff            call      00404DE0h
  0044ea1c  8a4638                mov       al,[esi+38h]
  0044ea1f  83c410                add       esp,10h
  0044ea22  84c0                  test      al,al
  0044ea24  7506                  jne       short 0044EA2Ch
  0044ea26  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044ea2c:
  0044ea2c  5f                    pop       edi
  0044ea2d  5e                    pop       esi
  0044ea2e  5d                    pop       ebp
  0044ea2f  5b                    pop       ebx
  0044ea30  81c4b4000000          add       esp,0B4h
  0044ea36  c3                    ret

33 cible(s) de saut interne : 0x0044df81, 0x0044df95, 0x0044dfe2, 0x0044e025, 0x0044e075, 0x0044e07a, 0x0044e0b8, 0x0044e0db, 0x0044e119, 0x0044e165, 0x0044e17c, 0x0044e258, 0x0044e2bd, 0x0044e390, 0x0044e3d3, 0x0044e4a0, 0x0044e531, 0x0044e535, 0x0044e54e, 0x0044e55b, 0x0044e5d7, 0x0044e5dc, 0x0044e6af, 0x0044e6f2, 0x0044e797, 0x0044e828, 0x0044e82c, 0x0044e845, 0x0044e852, 0x0044e8c9, 0x0044ea04, 0x0044ea09, 0x0044ea2c
