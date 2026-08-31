Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00481670  section .text  offset fichier 0x80a70  169 octets

  00481670  53                    push      ebx
  00481671  55                    push      ebp
  00481672  56                    push      esi
  00481673  8b742410              mov       esi,[esp+10h]
  00481677  57                    push      edi
  00481678  f70600008000          test      dword [esi],800000h
  0048167e  0f848a000000          je        near 0048170Eh
  00481684  33c9                  xor       ecx,ecx
  00481686  c7462800000060        mov       dword [esi+28h],60000000h
  0048168d  894e38                mov       [esi+38h],ecx
  00481690  66894e2c              mov       [esi+2Ch],cx
  00481694  c646043e              mov       byte [esi+4],3Eh
  00481698  a120905e00            mov       eax,[5E9020h]
  0048169d  3bc1                  cmp       eax,ecx
  0048169f  bfc0935e00            mov       edi,5E93C0h
  004816a4  7468                  je        short 0048170Eh
  004816a6  89442414              mov       [esp+14h],eax
  004816aa  bd00080000            mov       ebp,800h
  004816af  bb01000000            mov       ebx,1
  004816b4  eb02                  jmp       short 004816B8h

LAB_004816b6:
  004816b6  33c9                  xor       ecx,ecx

LAB_004816b8:
  004816b8  8b07                  mov       eax,[edi]
  004816ba  a900000080            test      eax,80000000h
  004816bf  743c                  je        short 004816FDh
  004816c1  a900000050            test      eax,50000000h
  004816c6  7535                  jne       short 004816FDh
  004816c8  33c0                  xor       eax,eax
  004816ca  6aff                  push      0FFFFFFFFh
  004816cc  8a460b                mov       al,[esi+0Bh]
  004816cf  897e78                mov       [esi+78h],edi
  004816d2  50                    push      eax
  004816d3  6a63                  push      63h
  004816d5  56                    push      esi
  004816d6  66894e16              mov       [esi+16h],cx
  004816da  e80194ffff            call      0047AAE0h
  004816df  66016e32              add       [esi+32h],bp
  004816e3  83c410                add       esp,10h
  004816e6  33c9                  xor       ecx,ecx
  004816e8  66895e16              mov       [esi+16h],bx
  004816ec  8a4e0b                mov       cl,[esi+0Bh]
  004816ef  6aff                  push      0FFFFFFFFh
  004816f1  51                    push      ecx
  004816f2  6a63                  push      63h
  004816f4  56                    push      esi
  004816f5  e8e693ffff            call      0047AAE0h
  004816fa  83c410                add       esp,10h

LAB_004816fd:
  004816fd  8b442414              mov       eax,[esp+14h]
  00481701  81c79c000000          add       edi,9Ch
  00481707  48                    dec       eax
  00481708  89442414              mov       [esp+14h],eax
  0048170c  75a8                  jne       short 004816B6h

LAB_0048170e:
  0048170e  c70600000000          mov       dword [esi],0
  00481714  5f                    pop       edi
  00481715  5e                    pop       esi
  00481716  5d                    pop       ebp
  00481717  5b                    pop       ebx
  00481718  c3                    ret

4 cible(s) de saut interne : 0x004816b6, 0x004816b8, 0x004816fd, 0x0048170e
