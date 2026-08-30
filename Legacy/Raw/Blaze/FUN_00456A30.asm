Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00456a30  section .text  offset fichier 0x55e30  692 octets

  00456a30  51                    push      ecx
  00456a31  53                    push      ebx
  00456a32  55                    push      ebp
  00456a33  56                    push      esi
  00456a34  8b742414              mov       esi,[esp+14h]
  00456a38  57                    push      edi
  00456a39  bd9f860100            mov       ebp,1869Fh
  00456a3e  c744241000000000      mov       dword [esp+10h],0
  00456a46  bfc0935e00            mov       edi,5E93C0h

LAB_00456a4b:
  00456a4b  8b07                  mov       eax,[edi]
  00456a4d  a900000080            test      eax,80000000h
  00456a52  7451                  je        short 00456AA5h
  00456a54  a900000050            test      eax,50000000h
  00456a59  754a                  jne       short 00456AA5h
  00456a5b  8b461c                mov       eax,[esi+1Ch]
  00456a5e  8b571c                mov       edx,[edi+1Ch]
  00456a61  8b4e20                mov       ecx,[esi+20h]
  00456a64  8b5f20                mov       ebx,[edi+20h]
  00456a67  2bc2                  sub       eax,edx
  00456a69  8b5624                mov       edx,[esi+24h]
  00456a6c  2bcb                  sub       ecx,ebx
  00456a6e  8b5f24                mov       ebx,[edi+24h]
  00456a71  c1f80c                sar       eax,0Ch
  00456a74  2bd3                  sub       edx,ebx
  00456a76  8bd8                  mov       ebx,eax
  00456a78  c1f90c                sar       ecx,0Ch
  00456a7b  0fafd8                imul      ebx,eax
  00456a7e  8bc1                  mov       eax,ecx
  00456a80  c1fa0c                sar       edx,0Ch
  00456a83  0fafc1                imul      eax,ecx
  00456a86  8bca                  mov       ecx,edx
  00456a88  03d8                  add       ebx,eax
  00456a8a  0fafca                imul      ecx,edx
  00456a8d  03d9                  add       ebx,ecx
  00456a8f  53                    push      ebx
  00456a90  e87bef0e00            call      00545A10h
  00456a95  3bc5                  cmp       eax,ebp
  00456a97  7d0c                  jge       short 00456AA5h
  00456a99  8a542410              mov       dl,[esp+10h]
  00456a9d  8be8                  mov       ebp,eax
  00456a9f  881562905e00          mov       [5E9062h],dl

LAB_00456aa5:
  00456aa5  8b442410              mov       eax,[esp+10h]
  00456aa9  81c79c000000          add       edi,9Ch
  00456aaf  40                    inc       eax
  00456ab0  663d0300              cmp       ax,3
  00456ab4  89442410              mov       [esp+10h],eax
  00456ab8  7e91                  jle       short 00456A4Bh
  00456aba  a00e975e00            mov       al,[5E970Eh]
  00456abf  a802                  test      al,2
  00456ac1  7531                  jne       short 00456AF4h
  00456ac3  0c02                  or        al,2
  00456ac5  c60508975e00fe        mov       byte [5E9708h],0FEh
  00456acc  a20e975e00            mov       [5E970Eh],al
  00456ad1  c60509975e00d4        mov       byte [5E9709h],0D4h
  00456ad8  c6050a975e0000        mov       byte [5E970Ah],0
  00456adf  c6050b975e00c8        mov       byte [5E970Bh],0C8h
  00456ae6  c6050c975e0010        mov       byte [5E970Ch],10h
  00456aed  c6050d975e00cc        mov       byte [5E970Dh],0CCh

LAB_00456af4:
  00456af4  66837e1000            cmp       word [esi+10h],0
  00456af9  7564                  jne       short 00456B5Fh
  00456afb  66c786980000000000    mov       word [esi+98h],0
  00456b04  a108975e00            mov       eax,[5E9708h]
  00456b09  25ff000000            and       eax,0FFh
  00456b0e  33c9                  xor       ecx,ecx
  00456b10  8a0d09975e00          mov       cl,[5E9709h]
  00456b16  33d2                  xor       edx,edx
  00456b18  c1e008                shl       eax,8
  00456b1b  0bc1                  or        eax,ecx
  00456b1d  33c9                  xor       ecx,ecx
  00456b1f  c1e010                shl       eax,10h
  00456b22  c1f804                sar       eax,4
  00456b25  89461c                mov       [esi+1Ch],eax
  00456b28  8a150a975e00          mov       dl,[5E970Ah]
  00456b2e  33c0                  xor       eax,eax
  00456b30  a00b975e00            mov       al,[5E970Bh]
  00456b35  c1e208                shl       edx,8
  00456b38  0bd0                  or        edx,eax
  00456b3a  c1e210                shl       edx,10h
  00456b3d  c1fa04                sar       edx,4
  00456b40  895620                mov       [esi+20h],edx
  00456b43  8a0d0c975e00          mov       cl,[5E970Ch]
  00456b49  33d2                  xor       edx,edx
  00456b4b  8a150d975e00          mov       dl,[5E970Dh]
  00456b51  c1e108                shl       ecx,8
  00456b54  0bca                  or        ecx,edx
  00456b56  c1e110                shl       ecx,10h
  00456b59  c1f904                sar       ecx,4
  00456b5c  894e24                mov       [esi+24h],ecx

LAB_00456b5f:
  00456b5f  6882880000            push      8882h
  00456b64  56                    push      esi
  00456b65  e8c6efffff            call      00455B30h
  00456b6a  83c408                add       esp,8
  00456b6d  8bd8                  mov       ebx,eax
  00456b6f  56                    push      esi
  00456b70  e85be10d00            call      00534CD0h
  00456b75  8b4650                mov       eax,[esi+50h]
  00456b78  33ed                  xor       ebp,ebp
  00456b7a  83c404                add       esp,4
  00456b7d  3bc5                  cmp       eax,ebp
  00456b7f  7d02                  jge       short 00456B83h
  00456b81  f7d8                  neg       eax

LAB_00456b83:
  00456b83  3d00200000            cmp       eax,2000h
  00456b88  bf0f000000            mov       edi,0Fh
  00456b8d  7f10                  jg        short 00456B9Fh
  00456b8f  8b4658                mov       eax,[esi+58h]
  00456b92  3bc5                  cmp       eax,ebp
  00456b94  7d02                  jge       short 00456B98h
  00456b96  f7d8                  neg       eax

LAB_00456b98:
  00456b98  3d00200000            cmp       eax,2000h
  00456b9d  7e27                  jle       short 00456BC6h

LAB_00456b9f:
  00456b9f  6639ae98000000        cmp       [esi+98h],bp
  00456ba6  751e                  jne       short 00456BC6h
  00456ba8  55                    push      ebp
  00456ba9  55                    push      ebp
  00456baa  6800100000            push      1000h
  00456baf  68bc020000            push      2BCh
  00456bb4  6a02                  push      2
  00456bb6  56                    push      esi
  00456bb7  e8c4140100            call      00468080h
  00456bbc  83c418                add       esp,18h
  00456bbf  6689be98000000        mov       [esi+98h],di

LAB_00456bc6:
  00456bc6  8b4650                mov       eax,[esi+50h]
  00456bc9  3bc5                  cmp       eax,ebp
  00456bcb  7d02                  jge       short 00456BCFh
  00456bcd  f7d8                  neg       eax

LAB_00456bcf:
  00456bcf  3d00200000            cmp       eax,2000h
  00456bd4  7f10                  jg        short 00456BE6h
  00456bd6  8b4658                mov       eax,[esi+58h]
  00456bd9  3bc5                  cmp       eax,ebp
  00456bdb  7d02                  jge       short 00456BDFh
  00456bdd  f7d8                  neg       eax

LAB_00456bdf:
  00456bdf  3d00200000            cmp       eax,2000h
  00456be4  7e10                  jle       short 00456BF6h

LAB_00456be6:
  00456be6  55                    push      ebp
  00456be7  55                    push      ebp
  00456be8  6aff                  push      0FFFFFFFFh
  00456bea  55                    push      ebp
  00456beb  6a3d                  push      3Dh
  00456bed  56                    push      esi
  00456bee  e81d150100            call      00468110h
  00456bf3  83c418                add       esp,18h

LAB_00456bf6:
  00456bf6  66ff8e98000000        dec       word [esi+98h]
  00456bfd  6639ae98000000        cmp       [esi+98h],bp
  00456c04  7d07                  jge       short 00456C0Dh
  00456c06  6689be98000000        mov       [esi+98h],di

LAB_00456c0d:
  00456c0d  817e2000c0e0ff        cmp       dword [esi+20h],0FFE0C000h
  00456c14  7c06                  jl        short 00456C1Ch
  00456c16  6683fb01              cmp       bx,1
  00456c1a  7506                  jne       short 00456C22h

LAB_00456c1c:
  00456c1c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00456c22:
  00456c22  8b461c                mov       eax,[esi+1Ch]
  00456c25  c1f814                sar       eax,14h
  00456c28  a208975e00            mov       [5E9708h],al
  00456c2d  8b4e1c                mov       ecx,[esi+1Ch]
  00456c30  c1f90c                sar       ecx,0Ch
  00456c33  880d09975e00          mov       [5E9709h],cl
  00456c39  8b5620                mov       edx,[esi+20h]
  00456c3c  c1fa14                sar       edx,14h
  00456c3f  88150a975e00          mov       [5E970Ah],dl
  00456c45  8b4620                mov       eax,[esi+20h]
  00456c48  c1f80c                sar       eax,0Ch
  00456c4b  a20b975e00            mov       [5E970Bh],al
  00456c50  8b4e24                mov       ecx,[esi+24h]
  00456c53  c1f914                sar       ecx,14h
  00456c56  880d0c975e00          mov       [5E970Ch],cl
  00456c5c  8b5624                mov       edx,[esi+24h]
  00456c5f  c1fa0c                sar       edx,0Ch
  00456c62  88150d975e00          mov       [5E970Dh],dl
  00456c68  8b461c                mov       eax,[esi+1Ch]
  00456c6b  8b4e20                mov       ecx,[esi+20h]
  00456c6e  8b5624                mov       edx,[esi+24h]
  00456c71  c1f80c                sar       eax,0Ch
  00456c74  052c010000            add       eax,12Ch
  00456c79  c1f90c                sar       ecx,0Ch
  00456c7c  8bf8                  mov       edi,eax
  00456c7e  81e9c8000000          sub       ecx,0C8h
  00456c84  c1fa0c                sar       edx,0Ch
  00456c87  0faff8                imul      edi,eax
  00456c8a  8bc1                  mov       eax,ecx
  00456c8c  81eacc100000          sub       edx,10CCh
  00456c92  0fafc1                imul      eax,ecx
  00456c95  8bca                  mov       ecx,edx
  00456c97  03f8                  add       edi,eax
  00456c99  0fafca                imul      ecx,edx
  00456c9c  03f9                  add       edi,ecx
  00456c9e  57                    push      edi
  00456c9f  e86ced0e00            call      00545A10h
  00456ca4  3dc8000000            cmp       eax,0C8h
  00456ca9  a00e975e00            mov       al,[5E970Eh]
  00456cae  7e1a                  jle       short 00456CCAh
  00456cb0  0c01                  or        al,1
  00456cb2  a20e975e00            mov       [5E970Eh],al
  00456cb7  c6460a04              mov       byte [esi+0Ah],4
  00456cbb  896e50                mov       [esi+50h],ebp
  00456cbe  896e54                mov       [esi+54h],ebp
  00456cc1  896e58                mov       [esi+58h],ebp
  00456cc4  5f                    pop       edi
  00456cc5  5e                    pop       esi
  00456cc6  5d                    pop       ebp
  00456cc7  5b                    pop       ebx
  00456cc8  59                    pop       ecx
  00456cc9  c3                    ret

LAB_00456cca:
  00456cca  24fe                  and       al,0FEh
  00456ccc  5f                    pop       edi
  00456ccd  a20e975e00            mov       [5E970Eh],al
  00456cd2  c6460a05              mov       byte [esi+0Ah],5
  00456cd6  896e50                mov       [esi+50h],ebp
  00456cd9  896e54                mov       [esi+54h],ebp
  00456cdc  896e58                mov       [esi+58h],ebp
  00456cdf  5e                    pop       esi
  00456ce0  5d                    pop       ebp
  00456ce1  5b                    pop       ebx
  00456ce2  59                    pop       ecx
  00456ce3  c3                    ret

16 cible(s) de saut interne : 0x00456a4b, 0x00456aa5, 0x00456af4, 0x00456b5f, 0x00456b83, 0x00456b98, 0x00456b9f, 0x00456bc6, 0x00456bcf, 0x00456bdf, 0x00456be6, 0x00456bf6, 0x00456c0d, 0x00456c1c, 0x00456c22, 0x00456cca
