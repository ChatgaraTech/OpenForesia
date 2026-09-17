C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045e300  section .text  offset fichier 0x5d700  432 octets

  0045e300  8a0d05975e00          mov       cl,[5E9705h]
  0045e306  b002                  mov       al,2
  0045e308  53                    push      ebx
  0045e309  56                    push      esi
  0045e30a  8b74240c              mov       esi,[esp+0Ch]
  0045e30e  57                    push      edi
  0045e30f  84c8                  test      al,cl
  0045e311  754d                  jne       short 0045E360h
  0045e313  8ad9                  mov       bl,cl
  0045e315  0ad8                  or        bl,al
  0045e317  881d05975e00          mov       [5E9705h],bl
  0045e31d  8b461c                mov       eax,[esi+1Ch]
  0045e320  8b4e20                mov       ecx,[esi+20h]
  0045e323  8b5624                mov       edx,[esi+24h]
  0045e326  c1f80c                sar       eax,0Ch
  0045e329  c1f90c                sar       ecx,0Ch
  0045e32c  c1fa0c                sar       edx,0Ch
  0045e32f  8bd8                  mov       ebx,eax
  0045e331  a207975e00            mov       [5E9707h],al
  0045e336  8bc1                  mov       eax,ecx
  0045e338  880d09975e00          mov       [5E9709h],cl
  0045e33e  8bca                  mov       ecx,edx
  0045e340  88150b975e00          mov       [5E970Bh],dl
  0045e346  c1fb08                sar       ebx,8
  0045e349  c1f808                sar       eax,8
  0045e34c  c1f908                sar       ecx,8
  0045e34f  881d06975e00          mov       [5E9706h],bl
  0045e355  a208975e00            mov       [5E9708h],al
  0045e35a  880d0a975e00          mov       [5E970Ah],cl

LAB_0045e360:
  0045e360  33ff                  xor       edi,edi
  0045e362  66397e10              cmp       [esi+10h],di
  0045e366  7563                  jne       short 0045E3CBh
  0045e368  33d2                  xor       edx,edx
  0045e36a  6689be98000000        mov       [esi+98h],di
  0045e371  8a1506975e00          mov       dl,[5E9706h]
  0045e377  33c0                  xor       eax,eax
  0045e379  a007975e00            mov       al,[5E9707h]
  0045e37e  c1e208                shl       edx,8
  0045e381  0bd0                  or        edx,eax
  0045e383  33c0                  xor       eax,eax
  0045e385  c1e210                shl       edx,10h
  0045e388  c1fa04                sar       edx,4
  0045e38b  89561c                mov       [esi+1Ch],edx
  0045e38e  8b0d08975e00          mov       ecx,[5E9708h]
  0045e394  81e1ff000000          and       ecx,0FFh
  0045e39a  33d2                  xor       edx,edx
  0045e39c  8a1509975e00          mov       dl,[5E9709h]
  0045e3a2  c1e108                shl       ecx,8
  0045e3a5  0bca                  or        ecx,edx
  0045e3a7  c1e110                shl       ecx,10h
  0045e3aa  c1f904                sar       ecx,4
  0045e3ad  894e20                mov       [esi+20h],ecx
  0045e3b0  a00a975e00            mov       al,[5E970Ah]
  0045e3b5  33c9                  xor       ecx,ecx
  0045e3b7  8a0d0b975e00          mov       cl,[5E970Bh]
  0045e3bd  c1e008                shl       eax,8
  0045e3c0  0bc1                  or        eax,ecx
  0045e3c2  c1e010                shl       eax,10h
  0045e3c5  c1f804                sar       eax,4
  0045e3c8  894624                mov       [esi+24h],eax

LAB_0045e3cb:
  0045e3cb  6882880000            push      8882h
  0045e3d0  56                    push      esi
  0045e3d1  e85a77ffff            call      00455B30h
  0045e3d6  83c408                add       esp,8
  0045e3d9  56                    push      esi
  0045e3da  e8f1680d00            call      00534CD0h
  0045e3df  8b4650                mov       eax,[esi+50h]
  0045e3e2  83c404                add       esp,4
  0045e3e5  3bc7                  cmp       eax,edi
  0045e3e7  7d02                  jge       short 0045E3EBh
  0045e3e9  f7d8                  neg       eax

LAB_0045e3eb:
  0045e3eb  3d00200000            cmp       eax,2000h
  0045e3f0  7f10                  jg        short 0045E402h
  0045e3f2  8b4658                mov       eax,[esi+58h]
  0045e3f5  3bc7                  cmp       eax,edi
  0045e3f7  7d02                  jge       short 0045E3FBh
  0045e3f9  f7d8                  neg       eax

LAB_0045e3fb:
  0045e3fb  3d00200000            cmp       eax,2000h
  0045e400  7e3b                  jle       short 0045E43Dh

LAB_0045e402:
  0045e402  6639be98000000        cmp       [esi+98h],di
  0045e409  7532                  jne       short 0045E43Dh
  0045e40b  8b15cc975e00          mov       edx,[5E97CCh]
  0045e411  57                    push      edi
  0045e412  57                    push      edi
  0045e413  6800100000            push      1000h
  0045e418  8b82a8000000          mov       eax,[edx+0A8h]
  0045e41e  6800080000            push      800h
  0045e423  8b08                  mov       ecx,[eax]
  0045e425  660fb65116            movzx     dx,byte [ecx+16h]
  0045e42a  52                    push      edx
  0045e42b  56                    push      esi
  0045e42c  e84f9c0000            call      00468080h
  0045e431  83c418                add       esp,18h
  0045e434  66c786980000001400    mov       word [esi+98h],14h

LAB_0045e43d:
  0045e43d  66ff8e98000000        dec       word [esi+98h]
  0045e444  6639be98000000        cmp       [esi+98h],di
  0045e44b  7d07                  jge       short 0045E454h
  0045e44d  6689be98000000        mov       [esi+98h],di

LAB_0045e454:
  0045e454  8b461c                mov       eax,[esi+1Ch]
  0045e457  c1f814                sar       eax,14h
  0045e45a  a206975e00            mov       [5E9706h],al
  0045e45f  8b4e1c                mov       ecx,[esi+1Ch]
  0045e462  c1f90c                sar       ecx,0Ch
  0045e465  880d07975e00          mov       [5E9707h],cl
  0045e46b  8b5620                mov       edx,[esi+20h]
  0045e46e  c1fa14                sar       edx,14h
  0045e471  881508975e00          mov       [5E9708h],dl
  0045e477  8b4620                mov       eax,[esi+20h]
  0045e47a  c1f80c                sar       eax,0Ch
  0045e47d  a209975e00            mov       [5E9709h],al
  0045e482  8b4e24                mov       ecx,[esi+24h]
  0045e485  c1f914                sar       ecx,14h
  0045e488  880d0a975e00          mov       [5E970Ah],cl
  0045e48e  8b5624                mov       edx,[esi+24h]
  0045e491  c1fa0c                sar       edx,0Ch
  0045e494  88150b975e00          mov       [5E970Bh],dl
  0045e49a  897e50                mov       [esi+50h],edi
  0045e49d  897e54                mov       [esi+54h],edi
  0045e4a0  897e58                mov       [esi+58h],edi
  0045e4a3  5f                    pop       edi
  0045e4a4  5e                    pop       esi
  0045e4a5  5b                    pop       ebx
  0045e4a6  c3                    ret
  0045e4a7  90                    nop
  0045e4a8  90                    nop
  0045e4a9  90                    nop
  0045e4aa  90                    nop
  0045e4ab  90                    nop
  0045e4ac  90                    nop
  0045e4ad  90                    nop
  0045e4ae  90                    nop
  0045e4af  90                    nop

7 cible(s) de saut interne : 0x0045e360, 0x0045e3cb, 0x0045e3eb, 0x0045e3fb, 0x0045e402, 0x0045e43d, 0x0045e454
