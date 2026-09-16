Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00453f60  section .text  offset fichier 0x53360  448 octets

  00453f60  55                    push      ebp
  00453f61  56                    push      esi
  00453f62  8b74240c              mov       esi,[esp+0Ch]
  00453f66  57                    push      edi
  00453f67  bf01000000            mov       edi,1
  00453f6c  66837e1000            cmp       word [esi+10h],0
  00453f71  751e                  jne       short 00453F91h
  00453f73  66897e10              mov       [esi+10h],di
  00453f77  e8c4310f00            call      00547140h
  00453f7c  99                    cdq
  00453f7d  b91f000000            mov       ecx,1Fh
  00453f82  66c746140a00          mov       word [esi+14h],0Ah
  00453f88  f7f9                  idiv      ecx
  00453f8a  c1e207                shl       edx,7
  00453f8d  66895612              mov       [esi+12h],dx

LAB_00453f91:
  00453f91  66397e10              cmp       [esi+10h],di
  00453f95  7544                  jne       short 00453FDBh
  00453f97  0fbf5612              movsx     edx,word [esi+12h]
  00453f9b  52                    push      edx
  00453f9c  e87b1a0f00            call      00545A1Ch
  00453fa1  8b4e20                mov       ecx,[esi+20h]
  00453fa4  6a00                  push      0
  00453fa6  c1e003                shl       eax,3
  00453fa9  03c8                  add       ecx,eax
  00453fab  0fbf4612              movsx     eax,word [esi+12h]
  00453faf  50                    push      eax
  00453fb0  894e20                mov       [esi+20h],ecx
  00453fb3  e8641a0f00            call      00545A1Ch
  00453fb8  c1e003                shl       eax,3
  00453fbb  50                    push      eax
  00453fbc  6a00                  push      0
  00453fbe  56                    push      esi
  00453fbf  e8bc050000            call      00454580h
  00453fc4  668146128000          add       word [esi+12h],80h
  00453fca  83c410                add       esp,10h
  00453fcd  66817e120010          cmp       word [esi+12h],1000h
  00453fd3  7c06                  jl        short 00453FDBh
  00453fd5  66c746120000          mov       word [esi+12h],0

LAB_00453fdb:
  00453fdb  66837e1002            cmp       word [esi+10h],2
  00453fe0  0f85ed000000          jne       near 004540D3h
  00453fe6  668b4614              mov       ax,[esi+14h]
  00453fea  8b5620                mov       edx,[esi+20h]
  00453fed  0fbfc8                movsx     ecx,ax
  00453ff0  c1e10c                shl       ecx,0Ch
  00453ff3  83c003                add       eax,3
  00453ff6  2bd1                  sub       edx,ecx
  00453ff8  66894614              mov       [esi+14h],ax
  00453ffc  b80080f3ff            mov       eax,0FFF38000h
  00454001  8bca                  mov       ecx,edx
  00454003  895620                mov       [esi+20h],edx
  00454006  3bc8                  cmp       ecx,eax
  00454008  0f8fc5000000          jg        near 004540D3h
  0045400e  894620                mov       [esi+20h],eax
  00454011  8b06                  mov       eax,[esi]
  00454013  0d00000040            or        eax,40000000h
  00454018  66c746100300          mov       word [esi+10h],3
  0045401e  66c7869a0000000000    mov       word [esi+9Ah],0
  00454027  8906                  mov       [esi],eax
  00454029  8b15cc975e00          mov       edx,[5E97CCh]
  0045402f  6a00                  push      0
  00454031  6a00                  push      0
  00454033  6800100000            push      1000h
  00454038  8b82a8000000          mov       eax,[edx+0A8h]
  0045403e  6800080000            push      800h
  00454043  8b08                  mov       ecx,[eax]
  00454045  660fb65123            movzx     dx,byte [ecx+23h]
  0045404a  52                    push      edx
  0045404b  56                    push      esi
  0045404c  e82f400100            call      00468080h
  00454051  83c418                add       esp,18h
  00454054  6a00                  push      0
  00454056  6a00                  push      0
  00454058  68e7030000            push      3E7h
  0045405d  6a28                  push      28h
  0045405f  68e7030000            push      3E7h
  00454064  56                    push      esi
  00454065  e8f67dfcff            call      0041BE60h
  0045406a  8a5609                mov       dl,[esi+9]
  0045406d  83c418                add       esp,18h
  00454070  80e20f                and       dl,0Fh
  00454073  33c9                  xor       ecx,ecx
  00454075  88542410              mov       [esp+10h],dl
  00454079  bfc0935e00            mov       edi,5E93C0h
  0045407e  bd00800000            mov       ebp,8000h
  00454083  eb04                  jmp       short 00454089h

LAB_00454085:
  00454085  8a542410              mov       dl,[esp+10h]

LAB_00454089:
  00454089  8b07                  mov       eax,[edi]
  0045408b  a900000080            test      eax,80000000h
  00454090  7423                  je        short 004540B5h
  00454092  a900000050            test      eax,50000000h
  00454097  751c                  jne       short 004540B5h
  00454099  d2ea                  shr       dl,cl
  0045409b  f6c201                test      dl,1
  0045409e  7415                  je        short 004540B5h
  004540a0  0d00000020            or        eax,20000000h
  004540a5  8907                  mov       [edi],eax
  004540a7  8b8790000000          mov       eax,[edi+90h]
  004540ad  0bc5                  or        eax,ebp
  004540af  898790000000          mov       [edi+90h],eax

LAB_004540b5:
  004540b5  41                    inc       ecx
  004540b6  81c79c000000          add       edi,9Ch
  004540bc  6683f903              cmp       cx,3
  004540c0  7ec3                  jle       short 00454085h
  004540c2  6a00                  push      0
  004540c4  6a00                  push      0
  004540c6  6a00                  push      0
  004540c8  6a05                  push      5
  004540ca  56                    push      esi
  004540cb  e870990000            call      0045DA40h
  004540d0  83c414                add       esp,14h

LAB_004540d3:
  004540d3  668b4610              mov       ax,[esi+10h]
  004540d7  663d0400              cmp       ax,4
  004540db  7518                  jne       short 004540F5h
  004540dd  66ff869a000000        inc       word [esi+9Ah]
  004540e4  6683be9a00000000      cmp       word [esi+9Ah],0
  004540ec  7e07                  jle       short 004540F5h
  004540ee  8b0e                  mov       ecx,[esi]
  004540f0  80e5df                and       ch,0DFh
  004540f3  890e                  mov       [esi],ecx

LAB_004540f5:
  004540f5  663d0500              cmp       ax,5
  004540f9  7522                  jne       short 0045411Dh
  004540fb  8b5620                mov       edx,[esi+20h]
  004540fe  81c200a00000          add       edx,0A000h
  00454104  8bc2                  mov       eax,edx
  00454106  895620                mov       [esi+20h],edx
  00454109  3d00001900            cmp       eax,190000h
  0045410e  7c0d                  jl        short 0045411Dh
  00454110  c7462000001900        mov       dword [esi+20h],190000h
  00454117  66c746100000          mov       word [esi+10h],0

LAB_0045411d:
  0045411d  5f                    pop       edi
  0045411e  5e                    pop       esi
  0045411f  5d                    pop       ebp

8 cible(s) de saut interne : 0x00453f91, 0x00453fdb, 0x00454085, 0x00454089, 0x004540b5, 0x004540d3, 0x004540f5, 0x0045411d
