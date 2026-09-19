Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00485940  section .text  offset fichier 0x84d40  256 octets

  00485940  53                    push      ebx
  00485941  55                    push      ebp
  00485942  56                    push      esi
  00485943  8b742410              mov       esi,[esp+10h]
  00485947  33ed                  xor       ebp,ebp
  00485949  57                    push      edi
  0048594a  66396e10              cmp       [esi+10h],bp
  0048594e  0f85c1000000          jne       near 00485A15h
  00485954  8b466c                mov       eax,[esi+6Ch]
  00485957  c6460438              mov       byte [esi+4],38h
  0048595b  0fbf7e32              movsx     edi,word [esi+32h]
  0048595f  894674                mov       [esi+74h],eax
  00485962  896e38                mov       [esi+38h],ebp
  00485965  66c746360400          mov       word [esi+36h],4
  0048596b  66c7462ef0ff          mov       word [esi+2Eh],0FFF0h
  00485971  c7462800200004        mov       dword [esi+28h],4002000h
  00485978  66c7462c0020          mov       word [esi+2Ch],2000h
  0048597e  66c746120200          mov       word [esi+12h],2
  00485984  bb06000000            mov       ebx,6

LAB_00485989:
  00485989  e8b2170c00            call      00547140h
  0048598e  25ff000000            and       eax,0FFh
  00485993  6a01                  push      1
  00485995  6a01                  push      1
  00485997  8d8c3880030000        lea       ecx,[eax+edi+380h]
  0048599e  66894e32              mov       [esi+32h],cx
  004859a2  8b15cc975e00          mov       edx,[5E97CCh]
  004859a8  8b82d0000000          mov       eax,[edx+0D0h]
  004859ae  33d2                  xor       edx,edx
  004859b0  8b08                  mov       ecx,[eax]
  004859b2  8a5103                mov       dl,[ecx+3]
  004859b5  81c2c0000000          add       edx,0C0h
  004859bb  52                    push      edx
  004859bc  56                    push      esi
  004859bd  e81e51ffff            call      0047AAE0h
  004859c2  83c410                add       esp,10h
  004859c5  81c7aa020000          add       edi,2AAh
  004859cb  4b                    dec       ebx
  004859cc  75bb                  jne       short 00485989h
  004859ce  6a02                  push      2
  004859d0  6a01                  push      1
  004859d2  b800600000            mov       eax,6000h
  004859d7  6a6c                  push      6Ch
  004859d9  56                    push      esi
  004859da  896e70                mov       [esi+70h],ebp
  004859dd  896e44                mov       [esi+44h],ebp
  004859e0  896e48                mov       [esi+48h],ebp
  004859e3  896e4c                mov       [esi+4Ch],ebp
  004859e6  894650                mov       [esi+50h],eax
  004859e9  894654                mov       [esi+54h],eax
  004859ec  894658                mov       [esi+58h],eax
  004859ef  c7463880808000        mov       dword [esi+38h],808080h
  004859f6  896e28                mov       [esi+28h],ebp
  004859f9  66896e2c              mov       [esi+2Ch],bp
  004859fd  c6460403              mov       byte [esi+4],3
  00485a01  e8da50ffff            call      0047AAE0h
  00485a06  b80a000000            mov       eax,0Ah
  00485a0b  83c410                add       esp,10h
  00485a0e  a3ace05d00            mov       [5DE0ACh],eax
  00485a13  eb05                  jmp       short 00485A1Ah

LAB_00485a15:
  00485a15  a1ace05d00            mov       eax,[5DE0ACh]

LAB_00485a1a:
  00485a1a  3bc5                  cmp       eax,ebp
  00485a1c  740d                  je        short 00485A2Bh
  00485a1e  8bc8                  mov       ecx,eax
  00485a20  c1f902                sar       ecx,2
  00485a23  2bc8                  sub       ecx,eax
  00485a25  890dace05d00          mov       [5DE0ACh],ecx

LAB_00485a2b:
  00485a2b  66ff4610              inc       word [esi+10h]
  00485a2f  66837e100a            cmp       word [esi+10h],0Ah
  00485a34  7c02                  jl        short 00485A38h
  00485a36  892e                  mov       [esi],ebp

LAB_00485a38:
  00485a38  5f                    pop       edi
  00485a39  5e                    pop       esi
  00485a3a  5d                    pop       ebp
  00485a3b  5b                    pop       ebx
  00485a3c  c3                    ret
  00485a3d  90                    nop
  00485a3e  90                    nop
  00485a3f  90                    nop

5 cible(s) de saut interne : 0x00485989, 0x00485a15, 0x00485a1a, 0x00485a2b, 0x00485a38
