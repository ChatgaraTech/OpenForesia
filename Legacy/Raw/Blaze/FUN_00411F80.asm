; 0x00411f80 — l'invocation qui emporte un membre (230 instructions, absente de Blaze.c).
; Trois copies, deux parametres croises :
;   0x004153f0 memoire 0x00571478 ; 0x00412460 rythme vif ; 0x00415990 les deux.

C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00411f80  section .text  offset fichier 0x11380  700 octets

  00411f80  81eca0000000          sub       esp,0A0h
  00411f86  53                    push      ebx
  00411f87  55                    push      ebp
  00411f88  56                    push      esi
  00411f89  57                    push      edi
  00411f8a  b926000000            mov       ecx,26h
  00411f8f  33c0                  xor       eax,eax
  00411f91  8d7c2418              lea       edi,[esp+18h]
  00411f95  c744241400000000      mov       dword [esp+14h],0
  00411f9d  f3ab                  rep stosd
  00411f9f  e8ac8f0000            call      0041AF50h
  00411fa4  e897c6ffff            call      0040E640h
  00411fa9  8bb424b4000000        mov       esi,[esp+0B4h]
  00411fb0  8b6c2410              mov       ebp,[esp+10h]
  00411fb4  8d7c2414              lea       edi,[esp+14h]
  00411fb8  b8a0ca5e00            mov       eax,5ECAA0h
  00411fbd  b920000000            mov       ecx,20h

LAB_00411fc2:
  00411fc2  8a500c                mov       dl,[eax+0Ch]
  00411fc5  84d2                  test      dl,dl
  00411fc7  7538                  jne       short 00412001h
  00411fc9  f70000000080          test      dword [eax],80000000h
  00411fcf  7430                  je        short 00412001h
  00411fd1  8b501c                mov       edx,[eax+1Ch]
  00411fd4  8bf8                  mov       edi,eax
  00411fd6  89561c                mov       [esi+1Ch],edx
  00411fd9  8b5020                mov       edx,[eax+20h]
  00411fdc  895620                mov       [esi+20h],edx
  00411fdf  8b5024                mov       edx,[eax+24h]
  00411fe2  895624                mov       [esi+24h],edx
  00411fe5  33d2                  xor       edx,edx
  00411fe7  8a5004                mov       dl,[eax+4]
  00411fea  66c740180200          mov       word [eax+18h],2
  00411ff0  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00411ff7  8b6a08                mov       ebp,[edx+8]
  00411ffa  8b10                  mov       edx,[eax]
  00411ffc  80ce10                or        dh,10h
  00411fff  8910                  mov       [eax],edx

LAB_00412001:
  00412001  059c000000            add       eax,9Ch
  00412006  49                    dec       ecx
  00412007  75b9                  jne       short 00411FC2h
  00412009  33c9                  xor       ecx,ecx
  0041200b  bb01000000            mov       ebx,1
  00412010  66394e10              cmp       [esi+10h],cx
  00412014  7531                  jne       short 00412047h
  00412016  66895e10              mov       [esi+10h],bx
  0041201a  66894e12              mov       [esi+12h],cx
  0041201e  66894e14              mov       [esi+14h],cx
  00412022  66894e16              mov       [esi+16h],cx
  00412026  8a4709                mov       al,[edi+9]
  00412029  66894f1a              mov       [edi+1Ah],cx
  0041202d  884f0a                mov       [edi+0Ah],cl
  00412030  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  00412037  66895f36              mov       [edi+36h],bx
  0041203b  a248145700            mov       [571448h],al
  00412040  66898e98000000        mov       [esi+98h],cx

LAB_00412047:
  00412047  66395e10              cmp       [esi+10h],bx
  0041204b  0f8590000000          jne       near 004120E1h
  00412051  66ff8698000000        inc       word [esi+98h]
  00412058  e8e3501300            call      00547140h
  0041205d  99                    cdq
  0041205e  b903000000            mov       ecx,3
  00412063  f7f9                  idiv      ecx
  00412065  663bd3                cmp       dx,bx
  00412068  740a                  je        short 00412074h
  0041206a  6683be9800000014      cmp       word [esi+98h],14h
  00412072  7c22                  jl        short 00412096h

LAB_00412074:
  00412074  6800080000            push      800h
  00412079  6800080000            push      800h
  0041207e  68e8030000            push      3E8h
  00412083  6a0b                  push      0Bh
  00412085  66c786980000000000    mov       word [esi+98h],0
  0041208e  e80dae1200            call      0053CEA0h
  00412093  83c410                add       esp,10h

LAB_00412096:
  00412096  6aff                  push      0FFFFFFFFh
  00412098  6a00                  push      0
  0041209a  68f4000000            push      0F4h
  0041209f  8d561c                lea       edx,[esi+1Ch]
  004120a2  6a00                  push      0
  004120a4  52                    push      edx
  004120a5  56                    push      esi
  004120a6  e8958b0600            call      0047AC40h
  004120ab  83c418                add       esp,18h
  004120ae  66ff4614              inc       word [esi+14h]
  004120b2  66837e1404            cmp       word [esi+14h],4
  004120b7  7c26                  jl        short 004120DFh
  004120b9  66c746140000          mov       word [esi+14h],0
  004120bf  66ff471a              inc       word [edi+1Ah]
  004120c3  668b4f1a              mov       cx,[edi+1Ah]
  004120c7  668b4516              mov       ax,[ebp+16h]
  004120cb  0fbfc9                movsx     ecx,cx
  004120ce  8bd0                  mov       edx,eax
  004120d0  81e2ffff0000          and       edx,0FFFFh
  004120d6  3bca                  cmp       ecx,edx
  004120d8  7c05                  jl        short 004120DFh
  004120da  48                    dec       eax
  004120db  6689471a              mov       [edi+1Ah],ax

LAB_004120df:
  004120df  33c9                  xor       ecx,ecx

LAB_004120e1:
  004120e1  66837e1650            cmp       word [esi+16h],50h
  004120e6  7550                  jne       short 00412138h
  004120e8  668b6e14              mov       bp,[esi+14h]
  004120ec  6aff                  push      0FFFFFFFFh
  004120ee  51                    push      ecx
  004120ef  68c1000000            push      0C1h
  004120f4  56                    push      esi
  004120f5  66c746100200          mov       word [esi+10h],2
  004120fb  66c746140700          mov       word [esi+14h],7
  00412101  e8da890600            call      0047AAE0h
  00412106  83c410                add       esp,10h
  00412109  66895e14              mov       [esi+14h],bx
  0041210d  6aff                  push      0FFFFFFFFh
  0041210f  6a00                  push      0
  00412111  68c1000000            push      0C1h
  00412116  56                    push      esi
  00412117  e8c4890600            call      0047AAE0h
  0041211c  66896e14              mov       [esi+14h],bp
  00412120  8b07                  mov       eax,[edi]
  00412122  80cc40                or        ah,40h
  00412125  83c410                add       esp,10h
  00412128  8907                  mov       [edi],eax
  0041212a  c6473800              mov       byte [edi+38h],0
  0041212e  c6473900              mov       byte [edi+39h],0
  00412132  c6473a00              mov       byte [edi+3Ah],0
  00412136  33c9                  xor       ecx,ecx

LAB_00412138:
  00412138  668b4616              mov       ax,[esi+16h]
  0041213c  663d5000              cmp       ax,50h
  00412140  7c46                  jl        short 00412188h
  00412142  663d5500              cmp       ax,55h
  00412146  7f40                  jg        short 00412188h
  00412148  6aff                  push      0FFFFFFFFh
  0041214a  51                    push      ecx
  0041214b  8d6e1c                lea       ebp,[esi+1Ch]
  0041214e  68f5000000            push      0F5h
  00412153  51                    push      ecx
  00412154  55                    push      ebp
  00412155  56                    push      esi
  00412156  e8e58a0600            call      0047AC40h
  0041215b  83c418                add       esp,18h
  0041215e  6aff                  push      0FFFFFFFFh
  00412160  6a00                  push      0
  00412162  68f5000000            push      0F5h
  00412167  6a00                  push      0
  00412169  55                    push      ebp
  0041216a  56                    push      esi
  0041216b  e8d08a0600            call      0047AC40h
  00412170  83c418                add       esp,18h
  00412173  6aff                  push      0FFFFFFFFh
  00412175  6a00                  push      0
  00412177  68f5000000            push      0F5h
  0041217c  6a00                  push      0
  0041217e  55                    push      ebp
  0041217f  56                    push      esi
  00412180  e8bb8a0600            call      0047AC40h
  00412185  83c418                add       esp,18h

LAB_00412188:
  00412188  66837e1650            cmp       word [esi+16h],50h
  0041218d  7519                  jne       short 004121A8h
  0041218f  6800100000            push      1000h
  00412194  6800100000            push      1000h
  00412199  68bf010000            push      1BFh
  0041219e  6a2e                  push      2Eh
  004121a0  e8fbac1200            call      0053CEA0h
  004121a5  83c410                add       esp,10h

LAB_004121a8:
  004121a8  66837e1651            cmp       word [esi+16h],51h
  004121ad  7519                  jne       short 004121C8h
  004121af  6800100000            push      1000h
  004121b4  6800100000            push      1000h
  004121b9  68e9010000            push      1E9h
  004121be  6a2e                  push      2Eh
  004121c0  e8dbac1200            call      0053CEA0h
  004121c5  83c410                add       esp,10h

LAB_004121c8:
  004121c8  66837e1652            cmp       word [esi+16h],52h
  004121cd  7519                  jne       short 004121E8h
  004121cf  6800100000            push      1000h
  004121d4  6800100000            push      1000h
  004121d9  6800020000            push      200h
  004121de  6a2e                  push      2Eh
  004121e0  e8bbac1200            call      0053CEA0h
  004121e5  83c410                add       esp,10h

LAB_004121e8:
  004121e8  66817e168200          cmp       word [esi+16h],82h
  004121ee  7c3e                  jl        short 0041222Eh
  004121f0  8126ffffff7f          and       dword [esi],7FFFFFFFh
  004121f6  e8b5150f00            call      005037B0h
  004121fb  a148145700            mov       eax,[571448h]
  00412200  6a00                  push      0
  00412202  25ff000000            and       eax,0FFh
  00412207  8d0c80                lea       ecx,[eax+eax*4]
  0041220a  c1e103                shl       ecx,3
  0041220d  2bc8                  sub       ecx,eax
  0041220f  a1cc975e00            mov       eax,[5E97CCh]
  00412214  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0041221b  52                    push      edx
  0041221c  50                    push      eax
  0041221d  e8ae1b1000            call      00513DD0h
  00412222  8b07                  mov       eax,[edi]
  00412224  83c40c                add       esp,0Ch
  00412227  0d00400002            or        eax,2004000h
  0041222c  8907                  mov       [edi],eax

LAB_0041222e:
  0041222e  66ff4616              inc       word [esi+16h]
  00412232  5f                    pop       edi
  00412233  5e                    pop       esi
  00412234  5d                    pop       ebp
  00412235  5b                    pop       ebx
  00412236  81c4a0000000          add       esp,0A0h

13 cible(s) de saut interne : 0x00411fc2, 0x00412001, 0x00412047, 0x00412074, 0x00412096, 0x004120df, 0x004120e1, 0x00412138, 0x00412188, 0x004121a8, 0x004121c8, 0x004121e8, 0x0041222e
