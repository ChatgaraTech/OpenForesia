Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449e30  section .text  offset fichier 0x49230  341 octets

  00449e30  53                    push      ebx
  00449e31  56                    push      esi
  00449e32  8b74240c              mov       esi,[esp+0Ch]
  00449e36  33db                  xor       ebx,ebx
  00449e38  57                    push      edi
  00449e39  53                    push      ebx
  00449e3a  53                    push      ebx
  00449e3b  56                    push      esi
  00449e3c  e8ffe1ffff            call      00448040h
  00449e41  8b4678                mov       eax,[esi+78h]
  00449e44  83c40c                add       esp,0Ch
  00449e47  3bc3                  cmp       eax,ebx
  00449e49  7505                  jne       short 00449E50h
  00449e4b  b858e55d00            mov       eax,5DE558h

LAB_00449e50:
  00449e50  8b481c                mov       ecx,[eax+1Ch]
  00449e53  894e50                mov       [esi+50h],ecx
  00449e56  8b5020                mov       edx,[eax+20h]
  00449e59  81c200c00300          add       edx,3C000h
  00449e5f  66395e10              cmp       [esi+10h],bx
  00449e63  895654                mov       [esi+54h],edx
  00449e66  8b4024                mov       eax,[eax+24h]
  00449e69  894658                mov       [esi+58h],eax
  00449e6c  b901000000            mov       ecx,1
  00449e71  7538                  jne       short 00449EABh
  00449e73  8b4620                mov       eax,[esi+20h]
  00449e76  8a969a000000          mov       dl,[esi+9Ah]
  00449e7c  0500a00f00            add       eax,0FA000h
  00449e81  66894e10              mov       [esi+10h],cx
  00449e85  894620                mov       [esi+20h],eax
  00449e88  b0c8                  mov       al,0C8h
  00449e8a  c7463c50155500        mov       dword [esi+3Ch],551550h
  00449e91  885e40                mov       [esi+40h],bl
  00449e94  88560a                mov       [esi+0Ah],dl
  00449e97  885e0b                mov       [esi+0Bh],bl
  00449e9a  66895e28              mov       [esi+28h],bx
  00449e9e  66895e2a              mov       [esi+2Ah],bx
  00449ea2  884638                mov       [esi+38h],al
  00449ea5  884639                mov       [esi+39h],al
  00449ea8  88463a                mov       [esi+3Ah],al

LAB_00449eab:
  00449eab  66394e10              cmp       [esi+10h],cx
  00449eaf  0f85cc000000          jne       near 00449F81h
  00449eb5  8b7e20                mov       edi,[esi+20h]
  00449eb8  8b4654                mov       eax,[esi+54h]
  00449ebb  81c70060ffff          add       edi,0FFFF6000h
  00449ec1  8bcf                  mov       ecx,edi
  00449ec3  897e20                mov       [esi+20h],edi
  00449ec6  3bc8                  cmp       ecx,eax
  00449ec8  7d03                  jge       short 00449ECDh
  00449eca  894620                mov       [esi+20h],eax

LAB_00449ecd:
  00449ecd  8b4648                mov       eax,[esi+48h]
  00449ed0  50                    push      eax
  00449ed1  e846bb0f00            call      00545A1Ch
  00449ed6  0faf4644              imul      eax,[esi+44h]
  00449eda  8b4e50                mov       ecx,[esi+50h]
  00449edd  8b7e44                mov       edi,[esi+44h]
  00449ee0  03c1                  add       eax,ecx
  00449ee2  8b4e48                mov       ecx,[esi+48h]
  00449ee5  51                    push      ecx
  00449ee6  89461c                mov       [esi+1Ch],eax
  00449ee9  e828bb0f00            call      00545A16h
  00449eee  0fafc7                imul      eax,edi
  00449ef1  034658                add       eax,[esi+58h]
  00449ef4  894624                mov       [esi+24h],eax
  00449ef7  8d47ff                lea       eax,[edi-1]
  00449efa  83f828                cmp       eax,28h
  00449efd  894644                mov       [esi+44h],eax
  00449f00  7d07                  jge       short 00449F09h
  00449f02  c7464428000000        mov       dword [esi+44h],28h

LAB_00449f09:
  00449f09  8b565c                mov       edx,[esi+5Ch]
  00449f0c  8b7e48                mov       edi,[esi+48h]
  00449f0f  03fa                  add       edi,edx
  00449f11  8bc7                  mov       eax,edi
  00449f13  897e48                mov       [esi+48h],edi
  00449f16  3d00100000            cmp       eax,1000h
  00449f1b  7c08                  jl        short 00449F25h
  00449f1d  0500f0ffff            add       eax,0FFFFF000h
  00449f22  894648                mov       [esi+48h],eax

LAB_00449f25:
  00449f25  8b4648                mov       eax,[esi+48h]
  00449f28  3bc3                  cmp       eax,ebx
  00449f2a  7d08                  jge       short 00449F34h
  00449f2c  0500100000            add       eax,1000h
  00449f31  894648                mov       [esi+48h],eax

LAB_00449f34:
  00449f34  66ff4e14              dec       word [esi+14h]
  00449f38  66395e14              cmp       [esi+14h],bx
  00449f3c  7d20                  jge       short 00449F5Eh
  00449f3e  b9b0ffffff            mov       ecx,0FFFFFFB0h
  00449f43  66014e28              add       [esi+28h],cx
  00449f47  668b4628              mov       ax,[esi+28h]
  00449f4b  66014e2a              add       [esi+2Ah],cx
  00449f4f  663bc3                cmp       ax,bx
  00449f52  7d2d                  jge       short 00449F81h
  00449f54  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00449f5a  5f                    pop       edi
  00449f5b  5e                    pop       esi
  00449f5c  5b                    pop       ebx
  00449f5d  c3                    ret

LAB_00449f5e:
  00449f5e  b950000000            mov       ecx,50h
  00449f63  66014e28              add       [esi+28h],cx
  00449f67  66014e2a              add       [esi+2Ah],cx
  00449f6b  668b4628              mov       ax,[esi+28h]
  00449f6f  b9dc050000            mov       ecx,5DCh
  00449f74  663bc1                cmp       ax,cx
  00449f77  7c08                  jl        short 00449F81h
  00449f79  66894e28              mov       [esi+28h],cx
  00449f7d  66894e2a              mov       [esi+2Ah],cx

LAB_00449f81:
  00449f81  5f                    pop       edi
  00449f82  5e                    pop       esi
  00449f83  5b                    pop       ebx
  00449f84  c3                    ret

8 cible(s) de saut interne : 0x00449e50, 0x00449eab, 0x00449ecd, 0x00449f09, 0x00449f25, 0x00449f34, 0x00449f5e, 0x00449f81
