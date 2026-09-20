C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00440840  section .text  offset fichier 0x3fc40  2100 octets

  00440840  81eca0000000          sub       esp,0A0h
  00440846  53                    push      ebx
  00440847  55                    push      ebp
  00440848  56                    push      esi
  00440849  57                    push      edi
  0044084a  e801a7fdff            call      0041AF50h
  0044084f  e8ecddfcff            call      0040E640h
  00440854  8bb424b4000000        mov       esi,[esp+0B4h]
  0044085b  33db                  xor       ebx,ebx
  0044085d  8d6c2414              lea       ebp,[esp+14h]
  00440861  66395e10              cmp       [esi+10h],bx
  00440865  7513                  jne       short 0044087Ah
  00440867  66c7869a0000000b00    mov       word [esi+9Ah],0Bh
  00440870  895e5c                mov       [esi+5Ch],ebx
  00440873  66899e98000000        mov       [esi+98h],bx

LAB_0044087a:
  0044087a  b8a0ca5e00            mov       eax,5ECAA0h
  0044087f  bf20000000            mov       edi,20h

LAB_00440884:
  00440884  8a480c                mov       cl,[eax+0Ch]
  00440887  84c9                  test      cl,cl
  00440889  7544                  jne       short 004408CFh
  0044088b  f70000000080          test      dword [eax],80000000h
  00440891  743c                  je        short 004408CFh
  00440893  8b481c                mov       ecx,[eax+1Ch]
  00440896  8be8                  mov       ebp,eax
  00440898  894e1c                mov       [esi+1Ch],ecx
  0044089b  8b5020                mov       edx,[eax+20h]
  0044089e  895620                mov       [esi+20h],edx
  004408a1  8b4824                mov       ecx,[eax+24h]
  004408a4  894e24                mov       [esi+24h],ecx
  004408a7  668b8e9a000000        mov       cx,[esi+9Ah]
  004408ae  33d2                  xor       edx,edx
  004408b0  66894818              mov       [eax+18h],cx
  004408b4  8a5004                mov       dl,[eax+4]
  004408b7  0fbfc9                movsx     ecx,cx
  004408ba  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004408c1  8b0c8a                mov       ecx,[edx+ecx*4]
  004408c4  894c2410              mov       [esp+10h],ecx
  004408c8  8b08                  mov       ecx,[eax]
  004408ca  80cd10                or        ch,10h
  004408cd  8908                  mov       [eax],ecx

LAB_004408cf:
  004408cf  059c000000            add       eax,9Ch
  004408d4  4f                    dec       edi
  004408d5  75ad                  jne       short 00440884h
  004408d7  66395e10              cmp       [esi+10h],bx
  004408db  7549                  jne       short 00440926h
  004408dd  b801000000            mov       eax,1
  004408e2  66895e12              mov       [esi+12h],bx
  004408e6  66894610              mov       [esi+10h],ax
  004408ea  66895e14              mov       [esi+14h],bx
  004408ee  66895e16              mov       [esi+16h],bx
  004408f2  0fbf15b8925e00        movsx     edx,word [5E92B8h]
  004408f9  895650                mov       [esi+50h],edx
  004408fc  0fbf0dba925e00        movsx     ecx,word [5E92BAh]
  00440903  894e54                mov       [esi+54h],ecx
  00440906  0fbf15bc925e00        movsx     edx,word [5E92BCh]
  0044090d  895658                mov       [esi+58h],edx
  00440910  66895d1a              mov       [ebp+1Ah],bx
  00440914  c6450a00              mov       byte [ebp+0Ah],0
  00440918  c74538ffffff00        mov       dword [ebp+38h],0FFFFFFh
  0044091f  66894536              mov       [ebp+36h],ax
  00440923  895e74                mov       [esi+74h],ebx

LAB_00440926:
  00440926  8b5674                mov       edx,[esi+74h]
  00440929  42                    inc       edx
  0044092a  8bc2                  mov       eax,edx
  0044092c  895674                mov       [esi+74h],edx
  0044092f  83f866                cmp       eax,66h
  00440932  761e                  jbe       short 00440952h
  00440934  53                    push      ebx
  00440935  53                    push      ebx
  00440936  a801                  test      al,1
  00440938  68e7030000            push      3E7h
  0044093d  7404                  je        short 00440943h
  0044093f  6a08                  push      8
  00440941  eb01                  jmp       short 00440944h

LAB_00440943:
  00440943  53                    push      ebx

LAB_00440944:
  00440944  68e7030000            push      3E7h
  00440949  56                    push      esi
  0044094a  e811b5fdff            call      0041BE60h
  0044094f  83c418                add       esp,18h

LAB_00440952:
  00440952  837e7461              cmp       dword [esi+74h],61h
  00440956  7519                  jne       short 00440971h
  00440958  6800180000            push      1800h
  0044095d  6800180000            push      1800h
  00440962  68c2010000            push      1C2h
  00440967  6a0b                  push      0Bh
  00440969  e832c50f00            call      0053CEA0h
  0044096e  83c410                add       esp,10h

LAB_00440971:
  00440971  837e7462              cmp       dword [esi+74h],62h
  00440975  7519                  jne       short 00440990h
  00440977  6800180000            push      1800h
  0044097c  6800180000            push      1800h
  00440981  6800020000            push      200h
  00440986  6a20                  push      20h
  00440988  e813c50f00            call      0053CEA0h
  0044098d  83c410                add       esp,10h

LAB_00440990:
  00440990  668b4616              mov       ax,[esi+16h]
  00440994  ba0e000000            mov       edx,0Eh
  00440999  a801                  test      al,1
  0044099b  7459                  je        short 004409F6h
  0044099d  395e5c                cmp       [esi+5Ch],ebx
  004409a0  754f                  jne       short 004409F1h
  004409a2  66ff451a              inc       word [ebp+1Ah]
  004409a6  8b442410              mov       eax,[esp+10h]
  004409aa  0fbf4d1a              movsx     ecx,word [ebp+1Ah]
  004409ae  33ff                  xor       edi,edi
  004409b0  668b7816              mov       di,[eax+16h]
  004409b4  3bcf                  cmp       ecx,edi
  004409b6  7c3e                  jl        short 004409F6h
  004409b8  66895d1a              mov       [ebp+1Ah],bx
  004409bc  66ff869a000000        inc       word [esi+9Ah]
  004409c3  6683be9a0000000d      cmp       word [esi+9Ah],0Dh
  004409cb  7507                  jne       short 004409D4h
  004409cd  6689969a000000        mov       [esi+9Ah],dx

LAB_004409d4:
  004409d4  6683be9a0000000f      cmp       word [esi+9Ah],0Fh
  004409dc  7c18                  jl        short 004409F6h
  004409de  6689969a000000        mov       [esi+9Ah],dx
  004409e5  668b4016              mov       ax,[eax+16h]
  004409e9  6648                  dec       ax
  004409eb  6689451a              mov       [ebp+1Ah],ax
  004409ef  eb05                  jmp       short 004409F6h

LAB_004409f1:
  004409f1  48                    dec       eax
  004409f2  66894616              mov       [esi+16h],ax

LAB_004409f6:
  004409f6  8b465c                mov       eax,[esi+5Ch]
  004409f9  48                    dec       eax
  004409fa  89465c                mov       [esi+5Ch],eax
  004409fd  8bc8                  mov       ecx,eax
  004409ff  668b869a000000        mov       ax,[esi+9Ah]
  00440a06  663bc2                cmp       ax,dx
  00440a09  7513                  jne       short 00440A1Eh
  00440a0b  66837d1a1b            cmp       word [ebp+1Ah],1Bh
  00440a10  750c                  jne       short 00440A1Eh
  00440a12  83f9ff                cmp       ecx,0FFFFFFFFh
  00440a15  7507                  jne       short 00440A1Eh
  00440a17  c7465c0a000000        mov       dword [esi+5Ch],0Ah

LAB_00440a1e:
  00440a1e  395e5c                cmp       [esi+5Ch],ebx
  00440a21  7d03                  jge       short 00440A26h
  00440a23  895e5c                mov       [esi+5Ch],ebx

LAB_00440a26:
  00440a26  663bc2                cmp       ax,dx
  00440a29  0f85c2000000          jne       near 00440AF1h
  00440a2f  8b442410              mov       eax,[esp+10h]
  00440a33  33d2                  xor       edx,edx
  00440a35  0fbf4d1a              movsx     ecx,word [ebp+1Ah]
  00440a39  668b5016              mov       dx,[eax+16h]
  00440a3d  4a                    dec       edx
  00440a3e  3bca                  cmp       ecx,edx
  00440a40  0f8cab000000          jl        near 00440AF1h
  00440a46  66817e16be00          cmp       word [esi+16h],0BEh
  00440a4c  0f8d9f000000          jge       near 00440AF1h
  00440a52  6aff                  push      0FFFFFFFFh
  00440a54  53                    push      ebx
  00440a55  8d7e1c                lea       edi,[esi+1Ch]
  00440a58  68c5000000            push      0C5h
  00440a5d  53                    push      ebx
  00440a5e  57                    push      edi
  00440a5f  56                    push      esi
  00440a60  e8dba10300            call      0047AC40h
  00440a65  83c418                add       esp,18h
  00440a68  6aff                  push      0FFFFFFFFh
  00440a6a  53                    push      ebx
  00440a6b  68c5000000            push      0C5h
  00440a70  53                    push      ebx
  00440a71  57                    push      edi
  00440a72  56                    push      esi
  00440a73  e8c8a10300            call      0047AC40h
  00440a78  83c418                add       esp,18h
  00440a7b  6aff                  push      0FFFFFFFFh
  00440a7d  53                    push      ebx
  00440a7e  68c5000000            push      0C5h
  00440a83  53                    push      ebx
  00440a84  57                    push      edi
  00440a85  56                    push      esi
  00440a86  e8b5a10300            call      0047AC40h
  00440a8b  83c418                add       esp,18h
  00440a8e  6683be9800000001      cmp       word [esi+98h],1
  00440a96  7527                  jne       short 00440ABFh
  00440a98  6800200000            push      2000h
  00440a9d  6800200000            push      2000h
  00440aa2  6890010000            push      190h
  00440aa7  6a05                  push      5
  00440aa9  e8f2c30f00            call      0053CEA0h
  00440aae  bf02000000            mov       edi,2
  00440ab3  83c410                add       esp,10h
  00440ab6  6689be98000000        mov       [esi+98h],di
  00440abd  eb05                  jmp       short 00440AC4h

LAB_00440abf:
  00440abf  bf02000000            mov       edi,2

LAB_00440ac4:
  00440ac4  66399e98000000        cmp       [esi+98h],bx
  00440acb  7529                  jne       short 00440AF6h
  00440acd  6800140000            push      1400h
  00440ad2  6800140000            push      1400h
  00440ad7  6800010000            push      100h
  00440adc  6a05                  push      5
  00440ade  e8bdc30f00            call      0053CEA0h
  00440ae3  83c410                add       esp,10h
  00440ae6  66c786980000000100    mov       word [esi+98h],1
  00440aef  eb05                  jmp       short 00440AF6h

LAB_00440af1:
  00440af1  bf02000000            mov       edi,2

LAB_00440af6:
  00440af6  66817e16af00          cmp       word [esi+16h],0AFh
  00440afc  bbff000000            mov       ebx,0FFh
  00440b01  7522                  jne       short 00440B25h
  00440b03  66891d60915e00        mov       [5E9160h],bx
  00440b0a  66891d62915e00        mov       [5E9162h],bx
  00440b11  66891d64915e00        mov       [5E9164h],bx
  00440b18  66893d66915e00        mov       [5E9166h],di
  00440b1f  66c746120000          mov       word [esi+12h],0

LAB_00440b25:
  00440b25  668b4616              mov       ax,[esi+16h]
  00440b29  663daf00              cmp       ax,0AFh
  00440b2d  7e28                  jle       short 00440B57h
  00440b2f  663d2c01              cmp       ax,12Ch
  00440b33  7d22                  jge       short 00440B57h
  00440b35  66397e12              cmp       [esi+12h],di
  00440b39  7518                  jne       short 00440B53h
  00440b3b  6aff                  push      0FFFFFFFFh
  00440b3d  6a00                  push      0
  00440b3f  68cb000000            push      0CBh
  00440b44  56                    push      esi
  00440b45  66c746120000          mov       word [esi+12h],0
  00440b4b  e8909f0300            call      0047AAE0h
  00440b50  83c410                add       esp,10h

LAB_00440b53:
  00440b53  66ff4612              inc       word [esi+12h]

LAB_00440b57:
  00440b57  668b4616              mov       ax,[esi+16h]
  00440b5b  663df000              cmp       ax,0F0h
  00440b5f  7e49                  jle       short 00440BAAh
  00440b61  663d1801              cmp       ax,118h
  00440b65  7d43                  jge       short 00440BAAh
  00440b67  6639be98000000        cmp       [esi+98h],di
  00440b6e  7522                  jne       short 00440B92h
  00440b70  6800130000            push      1300h
  00440b75  6800130000            push      1300h
  00440b7a  6820030000            push      320h
  00440b7f  6a20                  push      20h
  00440b81  e81ac30f00            call      0053CEA0h
  00440b86  83c410                add       esp,10h
  00440b89  66c786980000000300    mov       word [esi+98h],3

LAB_00440b92:
  00440b92  6aff                  push      0FFFFFFFFh
  00440b94  6a00                  push      0
  00440b96  68c6000000            push      0C6h
  00440b9b  8d4e1c                lea       ecx,[esi+1Ch]
  00440b9e  6a00                  push      0
  00440ba0  51                    push      ecx
  00440ba1  56                    push      esi
  00440ba2  e899a00300            call      0047AC40h
  00440ba7  83c418                add       esp,18h

LAB_00440baa:
  00440baa  66817e161801          cmp       word [esi+16h],118h
  00440bb0  7550                  jne       short 00440C02h
  00440bb2  6800100000            push      1000h
  00440bb7  6800100000            push      1000h
  00440bbc  6820030000            push      320h
  00440bc1  6a05                  push      5
  00440bc3  e8d8c20f00            call      0053CEA0h
  00440bc8  83c410                add       esp,10h
  00440bcb  b896000000            mov       eax,96h
  00440bd0  66891d60915e00        mov       [5E9160h],bx
  00440bd7  66a362915e00          mov       [5E9162h],ax
  00440bdd  6aff                  push      0FFFFFFFFh
  00440bdf  6a00                  push      0
  00440be1  68cc000000            push      0CCh
  00440be6  66a364915e00          mov       [5E9164h],ax
  00440bec  66893d66915e00        mov       [5E9166h],di
  00440bf3  56                    push      esi
  00440bf4  66c746141000          mov       word [esi+14h],10h
  00440bfa  e8e19e0300            call      0047AAE0h
  00440bff  83c410                add       esp,10h

LAB_00440c02:
  00440c02  66817e161d01          cmp       word [esi+16h],11Dh
  00440c08  7518                  jne       short 00440C22h
  00440c0a  6aff                  push      0FFFFFFFFh
  00440c0c  6a00                  push      0
  00440c0e  68cc000000            push      0CCh
  00440c13  56                    push      esi
  00440c14  66c746140b00          mov       word [esi+14h],0Bh
  00440c1a  e8c19e0300            call      0047AAE0h
  00440c1f  83c410                add       esp,10h

LAB_00440c22:
  00440c22  66817e160e01          cmp       word [esi+16h],10Eh
  00440c28  751b                  jne       short 00440C45h
  00440c2a  6aff                  push      0FFFFFFFFh
  00440c2c  6a00                  push      0
  00440c2e  68cd000000            push      0CDh
  00440c33  56                    push      esi
  00440c34  66c786980000000000    mov       word [esi+98h],0
  00440c3d  e89e9e0300            call      0047AAE0h
  00440c42  83c410                add       esp,10h

LAB_00440c45:
  00440c45  668b4616              mov       ax,[esi+16h]
  00440c49  663d0e01              cmp       ax,10Eh
  00440c4d  7c43                  jl        short 00440C92h
  00440c4f  663d4001              cmp       ax,140h
  00440c53  7f3d                  jg        short 00440C92h
  00440c55  6683be9800000000      cmp       word [esi+98h],0
  00440c5d  7519                  jne       short 00440C78h
  00440c5f  6800100000            push      1000h
  00440c64  6800100000            push      1000h
  00440c69  6800010000            push      100h
  00440c6e  6a28                  push      28h
  00440c70  e82bc20f00            call      0053CEA0h
  00440c75  83c410                add       esp,10h

LAB_00440c78:
  00440c78  66ff8698000000        inc       word [esi+98h]
  00440c7f  6683be9800000007      cmp       word [esi+98h],7
  00440c87  7509                  jne       short 00440C92h
  00440c89  66c786980000000000    mov       word [esi+98h],0

LAB_00440c92:
  00440c92  668b4616              mov       ax,[esi+16h]
  00440c96  663d0e01              cmp       ax,10Eh
  00440c9a  7c30                  jl        short 00440CCCh
  00440c9c  663d1801              cmp       ax,118h
  00440ca0  7f2a                  jg        short 00440CCCh
  00440ca2  a801                  test      al,1
  00440ca4  7426                  je        short 00440CCCh
  00440ca6  8b7e20                mov       edi,[esi+20h]
  00440ca9  6aff                  push      0FFFFFFFFh
  00440cab  6a00                  push      0
  00440cad  68ce000000            push      0CEh
  00440cb2  8d9700600e00          lea       edx,[edi+0E6000h]
  00440cb8  56                    push      esi
  00440cb9  895620                mov       [esi+20h],edx
  00440cbc  e81f9e0300            call      0047AAE0h
  00440cc1  897e20                mov       [esi+20h],edi
  00440cc4  83c410                add       esp,10h
  00440cc7  bf02000000            mov       edi,2

LAB_00440ccc:
  00440ccc  668b4616              mov       ax,[esi+16h]
  00440cd0  663d0e01              cmp       ax,10Eh
  00440cd4  7c2a                  jl        short 00440D00h
  00440cd6  663d2c01              cmp       ax,12Ch
  00440cda  7f24                  jg        short 00440D00h
  00440cdc  6aff                  push      0FFFFFFFFh
  00440cde  6a00                  push      0
  00440ce0  68cf000000            push      0CFh
  00440ce5  56                    push      esi
  00440ce6  e8f59d0300            call      0047AAE0h
  00440ceb  83c410                add       esp,10h
  00440cee  6aff                  push      0FFFFFFFFh
  00440cf0  6a00                  push      0
  00440cf2  68cf000000            push      0CFh
  00440cf7  56                    push      esi
  00440cf8  e8e39d0300            call      0047AAE0h
  00440cfd  83c410                add       esp,10h

LAB_00440d00:
  00440d00  668b4616              mov       ax,[esi+16h]
  00440d04  663d4001              cmp       ax,140h
  00440d08  7c76                  jl        short 00440D80h
  00440d0a  751c                  jne       short 00440D28h
  00440d0c  c60527e05d0001        mov       byte [5DE027h],1
  00440d13  c60524e05d0000        mov       byte [5DE024h],0
  00440d1a  c60525e05d0000        mov       byte [5DE025h],0
  00440d21  c60526e05d0000        mov       byte [5DE026h],0

LAB_00440d28:
  00440d28  a024e05d00            mov       al,[5DE024h]
  00440d2d  3ac3                  cmp       al,bl
  00440d2f  7428                  je        short 00440D59h
  00440d31  8a0d25e05d00          mov       cl,[5DE025h]
  00440d37  8ad0                  mov       dl,al
  00440d39  a026e05d00            mov       al,[5DE026h]
  00440d3e  80c205                add       dl,5
  00440d41  80c105                add       cl,5
  00440d44  0405                  add       al,5
  00440d46  881524e05d00          mov       [5DE024h],dl
  00440d4c  880d25e05d00          mov       [5DE025h],cl
  00440d52  a226e05d00            mov       [5DE026h],al
  00440d57  eb27                  jmp       short 00440D80h

LAB_00440d59:
  00440d59  a12ce05d00            mov       eax,[5DE02Ch]
  00440d5e  c705f8455600fd000000  mov       dword [5645F8h],0FDh
  00440d68  0bc7                  or        eax,edi
  00440d6a  c70510e55d0007000000  mov       dword [5DE510h],7
  00440d74  a32ce05d00            mov       [5DE02Ch],eax
  00440d79  c60520e55d0003        mov       byte [5DE520h],3

LAB_00440d80:
  00440d80  66817e164001          cmp       word [esi+16h],140h
  00440d86  0f8c9b000000          jl        near 00440E27h
  00440d8c  6aff                  push      0FFFFFFFFh
  00440d8e  6a00                  push      0
  00440d90  8d7e1c                lea       edi,[esi+1Ch]
  00440d93  68c7000000            push      0C7h
  00440d98  6a00                  push      0
  00440d9a  57                    push      edi
  00440d9b  56                    push      esi
  00440d9c  e89f9e0300            call      0047AC40h
  00440da1  83c418                add       esp,18h
  00440da4  6aff                  push      0FFFFFFFFh
  00440da6  6a00                  push      0
  00440da8  68c7000000            push      0C7h
  00440dad  6a00                  push      0
  00440daf  57                    push      edi
  00440db0  56                    push      esi
  00440db1  e88a9e0300            call      0047AC40h
  00440db6  83c418                add       esp,18h
  00440db9  6aff                  push      0FFFFFFFFh
  00440dbb  6a00                  push      0
  00440dbd  68c7000000            push      0C7h
  00440dc2  6a00                  push      0
  00440dc4  57                    push      edi
  00440dc5  56                    push      esi
  00440dc6  e8759e0300            call      0047AC40h
  00440dcb  83c418                add       esp,18h
  00440dce  6aff                  push      0FFFFFFFFh
  00440dd0  6a00                  push      0
  00440dd2  68c7000000            push      0C7h
  00440dd7  6a00                  push      0
  00440dd9  57                    push      edi
  00440dda  56                    push      esi
  00440ddb  e8609e0300            call      0047AC40h
  00440de0  83c418                add       esp,18h
  00440de3  6aff                  push      0FFFFFFFFh
  00440de5  6a00                  push      0
  00440de7  68c7000000            push      0C7h
  00440dec  6a00                  push      0
  00440dee  57                    push      edi
  00440def  56                    push      esi
  00440df0  e84b9e0300            call      0047AC40h
  00440df5  83c418                add       esp,18h
  00440df8  6aff                  push      0FFFFFFFFh
  00440dfa  6a00                  push      0
  00440dfc  68c7000000            push      0C7h
  00440e01  6a00                  push      0
  00440e03  57                    push      edi
  00440e04  56                    push      esi
  00440e05  e8369e0300            call      0047AC40h
  00440e0a  83c418                add       esp,18h
  00440e0d  6aff                  push      0FFFFFFFFh
  00440e0f  6a00                  push      0
  00440e11  68c7000000            push      0C7h
  00440e16  6a00                  push      0
  00440e18  57                    push      edi
  00440e19  56                    push      esi
  00440e1a  e8219e0300            call      0047AC40h
  00440e1f  83c418                add       esp,18h
  00440e22  bf02000000            mov       edi,2

LAB_00440e27:
  00440e27  66ff4616              inc       word [esi+16h]
  00440e2b  66817e16f401          cmp       word [esi+16h],1F4h
  00440e31  0f8c80000000          jl        near 00440EB7h
  00440e37  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00440e3d  e86e290c00            call      005037B0h
  00440e42  33c0                  xor       eax,eax
  00440e44  6a00                  push      0
  00440e46  a038e55d00            mov       al,[5DE538h]
  00440e4b  8d0c80                lea       ecx,[eax+eax*4]
  00440e4e  c1e103                shl       ecx,3
  00440e51  2bc8                  sub       ecx,eax
  00440e53  a1cc975e00            mov       eax,[5E97CCh]
  00440e58  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00440e5f  52                    push      edx
  00440e60  50                    push      eax
  00440e61  e86a2f0d00            call      00513DD0h
  00440e66  8b4500                mov       eax,[ebp]
  00440e69  83c40c                add       esp,0Ch
  00440e6c  0d00400002            or        eax,2004000h
  00440e71  894500                mov       [ebp],eax
  00440e74  668b4e50              mov       cx,[esi+50h]
  00440e78  66890d60915e00        mov       [5E9160h],cx
  00440e7f  668b5654              mov       dx,[esi+54h]
  00440e83  66891562915e00        mov       [5E9162h],dx
  00440e8a  668b4658              mov       ax,[esi+58h]
  00440e8e  66a364915e00          mov       [5E9164h],ax
  00440e94  66893d66915e00        mov       [5E9166h],di
  00440e9b  c60527e05d0001        mov       byte [5DE027h],1
  00440ea2  c60524e05d0000        mov       byte [5DE024h],0
  00440ea9  c60525e05d0000        mov       byte [5DE025h],0
  00440eb0  c60526e05d0000        mov       byte [5DE026h],0

LAB_00440eb7:
  00440eb7  6683be9a00000012      cmp       word [esi+9Ah],12h
  00440ebf  7533                  jne       short 00440EF4h
  00440ec1  8b542410              mov       edx,[esp+10h]
  00440ec5  33c9                  xor       ecx,ecx
  00440ec7  0fbf451a              movsx     eax,word [ebp+1Ah]
  00440ecb  668b4a16              mov       cx,[edx+16h]
  00440ecf  3bc1                  cmp       eax,ecx
  00440ed1  7c21                  jl        short 00440EF4h
  00440ed3  6a00                  push      0
  00440ed5  6a00                  push      0
  00440ed7  6a01                  push      1
  00440ed9  e862621000            call      00547140h
  00440ede  99                    cdq
  00440edf  b905000000            mov       ecx,5
  00440ee4  f7f9                  idiv      ecx
  00440ee6  2bd7                  sub       edx,edi
  00440ee8  52                    push      edx
  00440ee9  6a00                  push      0
  00440eeb  56                    push      esi
  00440eec  e86faffdff            call      0041BE60h
  00440ef1  83c418                add       esp,18h

LAB_00440ef4:
  00440ef4  5f                    pop       edi
  00440ef5  5e                    pop       esi
  00440ef6  5d                    pop       ebp
  00440ef7  5b                    pop       ebx
  00440ef8  81c4a0000000          add       esp,0A0h
  00440efe  c3                    ret
  00440eff  90                    nop
  00440f00  53                    push      ebx
  00440f01  56                    push      esi
  00440f02  8b74240c              mov       esi,[esp+0Ch]
  00440f06  33db                  xor       ebx,ebx
  00440f08  57                    push      edi
  00440f09  bf01000000            mov       edi,1
  00440f0e  66395e10              cmp       [esi+10h],bx
  00440f12  0f8589000000          jne       near 00440FA1h
  00440f18  c7463cf0145500        mov       dword [esi+3Ch],5514F0h
  00440f1f  885e40                mov       [esi+40h],bl
  00440f22  e819621000            call      00547140h
  00440f27  99                    cdq
  00440f28  33c2                  xor       eax,edx
  00440f2a  66897e10              mov       [esi+10h],di
  00440f2e  2bc2                  sub       eax,edx
  00440f30  25ff030000            and       eax,3FFh
  00440f35  33c2                  xor       eax,edx
  00440f37  2bc2                  sub       eax,edx
  00440f39  0500060000            add       eax,600h
  00440f3e  66014632              add       [esi+32h],ax
  00440f42  e8f9611000            call      00547140h
  00440f47  99                    cdq
  00440f48  b91e000000            mov       ecx,1Eh
  00440f4d  66895e14              mov       [esi+14h],bx
  00440f51  f7f9                  idiv      ecx
  00440f53  83ea0f                sub       edx,0Fh
  00440f56  66895612              mov       [esi+12h],dx
  00440f5a  e8e1611000            call      00547140h
  00440f5f  99                    cdq
  00440f60  b905000000            mov       ecx,5
  00440f65  66899e9a000000        mov       [esi+9Ah],bx
  00440f6c  f7f9                  idiv      ecx
  00440f6e  8b4620                mov       eax,[esi+20h]
  00440f71  c6460a09              mov       byte [esi+0Ah],9
  00440f75  0500c01200            add       eax,12C000h
  00440f7a  885e0b                mov       [esi+0Bh],bl
  00440f7d  894620                mov       [esi+20h],eax
  00440f80  83c203                add       edx,3
  00440f83  89565c                mov       [esi+5Ch],edx
  00440f86  e8b5611000            call      00547140h
  00440f8b  99                    cdq
  00440f8c  b9dc050000            mov       ecx,5DCh
  00440f91  f7f9                  idiv      ecx
  00440f93  8d82d0070000          lea       eax,[edx+7D0h]
  00440f99  66894628              mov       [esi+28h],ax
  00440f9d  6689462a              mov       [esi+2Ah],ax

LAB_00440fa1:
  00440fa1  66397e10              cmp       [esi+10h],di
  00440fa5  0f8587000000          jne       near 00441032h
  00440fab  0fbf5632              movsx     edx,word [esi+32h]
  00440faf  52                    push      edx
  00440fb0  e8674a1000            call      00545A1Ch
  00440fb5  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00440fbc  0fafc1                imul      eax,ecx
  00440fbf  0fbf5632              movsx     edx,word [esi+32h]
  00440fc3  8b4e1c                mov       ecx,[esi+1Ch]
  00440fc6  668bbe9a000000        mov       di,[esi+9Ah]
  00440fcd  03c8                  add       ecx,eax
  00440fcf  52                    push      edx
  00440fd0  894e1c                mov       [esi+1Ch],ecx
  00440fd3  e83e4a1000            call      00545A16h
  00440fd8  0fbfcf                movsx     ecx,di
  00440fdb  0fafc1                imul      eax,ecx
  00440fde  8b5624                mov       edx,[esi+24h]
  00440fe1  03d0                  add       edx,eax
  00440fe3  8b4620                mov       eax,[esi+20h]
  00440fe6  895624                mov       [esi+24h],edx
  00440fe9  0fbf5612              movsx     edx,word [esi+12h]
  00440fed  c1e20c                shl       edx,0Ch
  00440ff0  03c2                  add       eax,edx
  00440ff2  894620                mov       [esi+20h],eax
  00440ff5  668b465c              mov       ax,[esi+5Ch]
  00440ff9  6603c7                add       ax,di
  00440ffc  6689869a000000        mov       [esi+9Ah],ax
  00441003  668b4614              mov       ax,[esi+14h]
  00441007  663d0400              cmp       ax,4
  0044100b  7c25                  jl        short 00441032h
  0044100d  663d0500              cmp       ax,5
  00441011  7f03                  jg        short 00441016h
  00441013  ff4e5c                dec       dword [esi+5Ch]

LAB_00441016:
  00441016  6af6                  push      0FFFFFFF6h
  00441018  6af6                  push      0FFFFFFF6h
  0044101a  6af6                  push      0FFFFFFF6h
  0044101c  56                    push      esi
  0044101d  e8be3dfcff            call      00404DE0h
  00441022  8a4638                mov       al,[esi+38h]
  00441025  83c410                add       esp,10h
  00441028  3ac3                  cmp       al,bl
  0044102a  7506                  jne       short 00441032h
  0044102c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00441032:
  00441032  66ff4616              inc       word [esi+16h]
  00441036  66ff4614              inc       word [esi+14h]
  0044103a  5f                    pop       edi
  0044103b  5e                    pop       esi
  0044103c  5b                    pop       ebx
  0044103d  c3                    ret
  0044103e  90                    nop
  0044103f  90                    nop
  00441040  56                    push      esi
  00441041  8b742408              mov       esi,[esp+8]
  00441045  66837e1000            cmp       word [esi+10h],0
  0044104a  0f8597000000          jne       near 004410E7h
  00441050  c7463cf0145500        mov       dword [esi+3Ch],5514F0h
  00441057  c6464000              mov       byte [esi+40h],0
  0044105b  e8e0601000            call      00547140h
  00441060  99                    cdq
  00441061  33c2                  xor       eax,edx
  00441063  2bc2                  sub       eax,edx
  00441065  25ff0f0000            and       eax,0FFFh
  0044106a  33c2                  xor       eax,edx
  0044106c  2bc2                  sub       eax,edx
  0044106e  66894632              mov       [esi+32h],ax
  00441072  e8c9                  (bad)
  (octet invalide — la plage demandee deborde probablement du code)
