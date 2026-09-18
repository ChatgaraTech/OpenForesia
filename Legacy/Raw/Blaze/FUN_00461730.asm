C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00461730  section .text  offset fichier 0x60b30  320 octets

  00461730  53                    push      ebx
  00461731  55                    push      ebp
  00461732  56                    push      esi
  00461733  8b742410              mov       esi,[esp+10h]
  00461737  33c0                  xor       eax,eax
  00461739  57                    push      edi
  0046173a  66394610              cmp       [esi+10h],ax
  0046173e  7519                  jne       short 00461759h
  00461740  8b4e1c                mov       ecx,[esi+1Ch]
  00461743  8b5624                mov       edx,[esi+24h]
  00461746  66c746100100          mov       word [esi+10h],1
  0046174c  66894612              mov       [esi+12h],ax
  00461750  894e50                mov       [esi+50h],ecx
  00461753  895658                mov       [esi+58h],edx
  00461756  89465c                mov       [esi+5Ch],eax

LAB_00461759:
  00461759  bfc0935e00            mov       edi,5E93C0h
  0046175e  bd04000000            mov       ebp,4
  00461763  bb02000000            mov       ebx,2

LAB_00461768:
  00461768  8b07                  mov       eax,[edi]
  0046176a  a900000080            test      eax,80000000h
  0046176f  746c                  je        short 004617DDh
  00461771  a900000050            test      eax,50000000h
  00461776  7565                  jne       short 004617DDh
  00461778  8b471c                mov       eax,[edi+1Ch]
  0046177b  8b561c                mov       edx,[esi+1Ch]
  0046177e  8b4f24                mov       ecx,[edi+24h]
  00461781  2bc2                  sub       eax,edx
  00461783  8b5624                mov       edx,[esi+24h]
  00461786  c1f80c                sar       eax,0Ch
  00461789  2bca                  sub       ecx,edx
  0046178b  8bd0                  mov       edx,eax
  0046178d  c1f90c                sar       ecx,0Ch
  00461790  0fafd0                imul      edx,eax
  00461793  8bc1                  mov       eax,ecx
  00461795  0fafc1                imul      eax,ecx
  00461798  03d0                  add       edx,eax
  0046179a  52                    push      edx
  0046179b  e870420e00            call      00545A10h
  004617a0  3dc8000000            cmp       eax,0C8h
  004617a5  7d36                  jge       short 004617DDh
  004617a7  66837e1001            cmp       word [esi+10h],1
  004617ac  752f                  jne       short 004617DDh
  004617ae  66895e10              mov       [esi+10h],bx
  004617b2  8b0dcc975e00          mov       ecx,[5E97CCh]
  004617b8  6a00                  push      0
  004617ba  6a00                  push      0
  004617bc  8b91a8000000          mov       edx,[ecx+0A8h]
  004617c2  6800280000            push      2800h
  004617c7  6800030000            push      300h
  004617cc  8b02                  mov       eax,[edx]
  004617ce  660fb64826            movzx     cx,byte [eax+26h]
  004617d3  51                    push      ecx
  004617d4  56                    push      esi
  004617d5  e8a6680000            call      00468080h
  004617da  83c418                add       esp,18h

LAB_004617dd:
  004617dd  81c79c000000          add       edi,9Ch
  004617e3  4d                    dec       ebp
  004617e4  7582                  jne       short 00461768h
  004617e6  66395e10              cmp       [esi+10h],bx
  004617ea  0f85b0000000          jne       near 004618A0h
  004617f0  66837e122d            cmp       word [esi+12h],2Dh
  004617f5  7e27                  jle       short 0046181Eh
  004617f7  8b465c                mov       eax,[esi+5Ch]
  004617fa  85c0                  test      eax,eax
  004617fc  7520                  jne       short 0046181Eh
  004617fe  0fbf5614              movsx     edx,word [esi+14h]
  00461802  a1cc975e00            mov       eax,[5E97CCh]
  00461807  52                    push      edx
  00461808  56                    push      esi
  00461809  50                    push      eax
  0046180a  e8c1250b00            call      00513DD0h
  0046180f  83c40c                add       esp,0Ch
  00461812  c7465c01000000        mov       dword [esi+5Ch],1
  00461819  5f                    pop       edi
  0046181a  5e                    pop       esi
  0046181b  5d                    pop       ebp
  0046181c  5b                    pop       ebx
  0046181d  c3                    ret

LAB_0046181e:
  0046181e  6a00                  push      0
  00461820  6a00                  push      0
  00461822  6aff                  push      0FFFFFFFFh
  00461824  6a00                  push      0
  00461826  6a37                  push      37h
  00461828  56                    push      esi
  00461829  e8e2680000            call      00468110h
  0046182e  83c418                add       esp,18h
  00461831  e80a590e00            call      00547140h
  00461836  99                    cdq
  00461837  33c2                  xor       eax,edx
  00461839  2bc2                  sub       eax,edx
  0046183b  83e001                and       eax,1
  0046183e  33c2                  xor       eax,edx
  00461840  2bc2                  sub       eax,edx
  00461842  6685c0                test      ax,ax
  00461845  7513                  jne       short 0046185Ah
  00461847  6a00                  push      0
  00461849  6a00                  push      0
  0046184b  6aff                  push      0FFFFFFFFh
  0046184d  6a00                  push      0
  0046184f  6a37                  push      37h
  00461851  56                    push      esi
  00461852  e8b9680000            call      00468110h
  00461857  83c418                add       esp,18h

LAB_0046185a:
  0046185a  0fbf4e32              movsx     ecx,word [esi+32h]
  0046185e  51                    push      ecx
  0046185f  e8b8410e00            call      00545A1Ch
  00461864  0fbf5612              movsx     edx,word [esi+12h]
  00461868  0fafc2                imul      eax,edx
  0046186b  8b6e58                mov       ebp,[esi+58h]
  0046186e  668b                  (bad)
  (octet invalide — la plage demandee deborde probablement du code)
