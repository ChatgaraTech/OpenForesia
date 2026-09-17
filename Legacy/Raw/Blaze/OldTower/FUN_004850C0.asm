C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004850c0  section .text  offset fichier 0x844c0  352 octets

  004850c0  83ec0c                sub       esp,0Ch
  004850c3  53                    push      ebx
  004850c4  56                    push      esi
  004850c5  8b742418              mov       esi,[esp+18h]
  004850c9  57                    push      edi
  004850ca  66c744240c0000        mov       word [esp+0Ch],0
  004850d1  66c744240e0000        mov       word [esp+0Eh],0
  004850d8  66837e1000            cmp       word [esi+10h],0
  004850dd  66c74424103c00        mov       word [esp+10h],3Ch
  004850e4  66c7442412c800        mov       word [esp+12h],0C8h
  004850eb  66c74424140000        mov       word [esp+14h],0
  004850f2  66c74424167800        mov       word [esp+16h],78h
  004850f9  7512                  jne       short 0048510Dh
  004850fb  8b461c                mov       eax,[esi+1Ch]
  004850fe  8b4e20                mov       ecx,[esi+20h]
  00485101  8b5624                mov       edx,[esi+24h]
  00485104  894644                mov       [esi+44h],eax
  00485107  894e48                mov       [esi+48h],ecx
  0048510a  89564c                mov       [esi+4Ch],edx

LAB_0048510d:
  0048510d  0fbf7e10              movsx     edi,word [esi+10h]
  00485111  e82a200c00            call      00547140h
  00485116  8bcf                  mov       ecx,edi
  00485118  25ff030000            and       eax,3FFh
  0048511d  0fafcf                imul      ecx,edi
  00485120  83c108                add       ecx,8
  00485123  8b5e44                mov       ebx,[esi+44h]
  00485126  c1e107                shl       ecx,7
  00485129  03c1                  add       eax,ecx
  0048512b  8b7e48                mov       edi,[esi+48h]
  0048512e  8bc8                  mov       ecx,eax
  00485130  56                    push      esi
  00485131  8d1440                lea       edx,[eax+eax*2]
  00485134  c1e112                shl       ecx,12h
  00485137  0bd1                  or        edx,ecx
  00485139  895628                mov       [esi+28h],edx
  0048513c  8b5650                mov       edx,[esi+50h]
  0048513f  03da                  add       ebx,edx
  00485141  8b5654                mov       edx,[esi+54h]
  00485144  03fa                  add       edi,edx
  00485146  895e44                mov       [esi+44h],ebx
  00485149  8bcb                  mov       ecx,ebx
  0048514b  8b5e4c                mov       ebx,[esi+4Ch]
  0048514e  897e48                mov       [esi+48h],edi
  00485151  8bd7                  mov       edx,edi
  00485153  8b7e58                mov       edi,[esi+58h]
  00485156  894e1c                mov       [esi+1Ch],ecx
  00485159  c1e007                shl       eax,7
  0048515c  03df                  add       ebx,edi
  0048515e  8d3c10                lea       edi,[eax+edx]
  00485161  895e4c                mov       [esi+4Ch],ebx
  00485164  897e20                mov       [esi+20h],edi
  00485167  895e24                mov       [esi+24h],ebx
  0048516a  e8d1e70a00            call      00533940h
  0048516f  8b4664                mov       eax,[esi+64h]
  00485172  83c404                add       esp,4
  00485175  3bf8                  cmp       edi,eax
  00485177  897e20                mov       [esi+20h],edi
  0048517a  7f0d                  jg        short 00485189h
  0048517c  894620                mov       [esi+20h],eax
  0048517f  c7869000000000080000  mov       dword [esi+90h],800h

LAB_00485189:
  00485189  f7869000000002880000  test      dword [esi+90h],8802h
  00485193  756f                  jne       short 00485204h
  00485195  8d44240c              lea       eax,[esp+0Ch]
  00485199  50                    push      eax
  0048519a  56                    push      esi
  0048519b  e8d0fafeff            call      00474C70h
  004851a0  8bf8                  mov       edi,eax
  004851a2  83c408                add       esp,8
  004851a5  85ff                  test      edi,edi
  004851a7  7444                  je        short 004851EDh
  004851a9  a1b8e05d00            mov       eax,[5DE0B8h]
  004851ae  85c0                  test      eax,eax
  004851b0  7452                  je        short 00485204h
  004851b2  8b4e74                mov       ecx,[esi+74h]
  004851b5  57                    push      edi
  004851b6  51                    push      ecx
  004851b7  e8c4490b00            call      00539B80h
  004851bc  8b0f                  mov       ecx,[edi]
  004851be  83c408                add       esp,8
  004851c1  83c940                or        ecx,40h
  004851c4  33db                  xor       ebx,ebx
  004851c6  6a01                  push      1
  004851c8  6a00                  push      0
  004851ca  66c787960000001000    mov       word [edi+96h],10h
  004851d3  890f                  mov       [edi],ecx
  004851d5  8a5e04                mov       bl,[esi+4]
  004851d8  6a72                  push      72h
  004851da  56                    push      esi
  004851db  c646041b              mov       byte [esi+4],1Bh
  004851df  897e78                mov       [esi+78h],edi
  004851e2  e8f958ffff            call      0047AAE0h
  004851e7  83c410                add       esp,10h
  004851ea  885e04                mov       [esi+4],bl

LAB_004851ed:
  004851ed  8b5638                mov       edx,[esi+38h]
  004851f0  81c2e8e7e7ff          add       edx,0FFE7E7E8h
  004851f6  66ff4610              inc       word [esi+10h]
  004851fa  66837e1008            cmp       word [esi+10h],8
  004851ff  895638                mov       [esi+38h],edx
  00485202  7c06                  jl        short 0048520Ah

LAB_00485204:
  00485204  c70600000000          mov       dword [esi],0

LAB_0048520a:
  0048520a  5f                    pop       edi
  0048520b  5e                    pop       esi
  0048520c  5b                    pop       ebx
  0048520d  83c40c                add       esp,0Ch
  00485210  c3                    ret
  00485211  90                    nop
  00485212  90                    nop
  00485213  90                    nop
  00485214  90                    nop
  00485215  90                    nop
  00485216  90                    nop
  00485217  90                    nop
  00485218  90                    nop
  00485219  90                    nop
  0048521a  90                    nop
  0048521b  90                    nop
  0048521c  90                    nop
  0048521d  90                    nop
  0048521e  90                    nop
  0048521f  90                    nop

5 cible(s) de saut interne : 0x0048510d, 0x00485189, 0x004851ed, 0x00485204, 0x0048520a
