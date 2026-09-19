Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042e150  section .text  offset fichier 0x2d550  1200 octets

  0042e150  a0e2965e00            mov       al,[5E96E2h]
  0042e155  53                    push      ebx
  0042e156  55                    push      ebp
  0042e157  56                    push      esi
  0042e158  84c0                  test      al,al
  0042e15a  57                    push      edi
  0042e15b  750a                  jne       short 0042E167h
  0042e15d  e8eecdfeff            call      0041AF50h
  0042e162  e8d904feff            call      0040E640h

LAB_0042e167:
  0042e167  8b742414              mov       esi,[esp+14h]
  0042e16b  33db                  xor       ebx,ebx
  0042e16d  8b6e6c                mov       ebp,[esi+6Ch]
  0042e170  3beb                  cmp       ebp,ebx
  0042e172  7505                  jne       short 0042E179h
  0042e174  bd58e55d00            mov       ebp,5DE558h

LAB_0042e179:
  0042e179  33c0                  xor       eax,eax
  0042e17b  66c745180700          mov       word [ebp+18h],7
  0042e181  8a4504                mov       al,[ebp+4]
  0042e184  8b0c8540965e00        mov       ecx,[eax*4+5E9640h]
  0042e18b  8b511c                mov       edx,[ecx+1Ch]
  0042e18e  8b4d00                mov       ecx,[ebp]
  0042e191  80cd10                or        ch,10h
  0042e194  89542414              mov       [esp+14h],edx
  0042e198  894d00                mov       [ebp],ecx
  0042e19b  66395e10              cmp       [esi+10h],bx
  0042e19f  7549                  jne       short 0042E1EAh
  0042e1a1  b801000000            mov       eax,1
  0042e1a6  66895e12              mov       [esi+12h],bx
  0042e1aa  66894610              mov       [esi+10h],ax
  0042e1ae  66895e16              mov       [esi+16h],bx
  0042e1b2  0fbf0db8925e00        movsx     ecx,word [5E92B8h]
  0042e1b9  894e50                mov       [esi+50h],ecx
  0042e1bc  0fbf15ba925e00        movsx     edx,word [5E92BAh]
  0042e1c3  895654                mov       [esi+54h],edx
  0042e1c6  0fbf0dbc925e00        movsx     ecx,word [5E92BCh]
  0042e1cd  894e58                mov       [esi+58h],ecx
  0042e1d0  66895d1a              mov       [ebp+1Ah],bx
  0042e1d4  c6450a00              mov       byte [ebp+0Ah],0
  0042e1d8  c74538ffffff00        mov       dword [ebp+38h],0FFFFFFh
  0042e1df  66894536              mov       [ebp+36h],ax
  0042e1e3  66899e98000000        mov       [esi+98h],bx

LAB_0042e1ea:
  0042e1ea  66399e98000000        cmp       [esi+98h],bx
  0042e1f1  7519                  jne       short 0042E20Ch
  0042e1f3  6800120000            push      1200h
  0042e1f8  6800120000            push      1200h
  0042e1fd  6800020000            push      200h
  0042e202  6a20                  push      20h
  0042e204  e897ec1000            call      0053CEA0h
  0042e209  83c410                add       esp,10h

LAB_0042e20c:
  0042e20c  6683be9800000001      cmp       word [esi+98h],1
  0042e214  7519                  jne       short 0042E22Fh
  0042e216  6800120000            push      1200h
  0042e21b  6800120000            push      1200h
  0042e220  6800020000            push      200h
  0042e225  6a20                  push      20h
  0042e227  e874ec1000            call      0053CEA0h
  0042e22c  83c410                add       esp,10h

LAB_0042e22f:
  0042e22f  6683be9800000002      cmp       word [esi+98h],2
  0042e237  7519                  jne       short 0042E252h
  0042e239  6800120000            push      1200h
  0042e23e  6800120000            push      1200h
  0042e243  6829030000            push      329h
  0042e248  6a20                  push      20h
  0042e24a  e851ec1000            call      0053CEA0h
  0042e24f  83c410                add       esp,10h

LAB_0042e252:
  0042e252  6683be9800000003      cmp       word [esi+98h],3
  0042e25a  7519                  jne       short 0042E275h
  0042e25c  6800120000            push      1200h
  0042e261  6800120000            push      1200h
  0042e266  6800040000            push      400h
  0042e26b  6a20                  push      20h
  0042e26d  e82eec1000            call      0053CEA0h
  0042e272  83c410                add       esp,10h

LAB_0042e275:
  0042e275  6683be9800000004      cmp       word [esi+98h],4
  0042e27d  7519                  jne       short 0042E298h
  0042e27f  6800120000            push      1200h
  0042e284  6800120000            push      1200h
  0042e289  6853060000            push      653h
  0042e28e  6a20                  push      20h
  0042e290  e80bec1000            call      0053CEA0h
  0042e295  83c410                add       esp,10h

LAB_0042e298:
  0042e298  66ff8698000000        inc       word [esi+98h]
  0042e29f  6683be9800000013      cmp       word [esi+98h],13h
  0042e2a7  7507                  jne       short 0042E2B0h
  0042e2a9  66899e98000000        mov       [esi+98h],bx

LAB_0042e2b0:
  0042e2b0  66837e1605            cmp       word [esi+16h],5
  0042e2b5  0f8d9b000000          jge       near 0042E356h
  0042e2bb  6aff                  push      0FFFFFFFFh
  0042e2bd  53                    push      ebx
  0042e2be  8d7e1c                lea       edi,[esi+1Ch]
  0042e2c1  68fd000000            push      0FDh
  0042e2c6  53                    push      ebx
  0042e2c7  57                    push      edi
  0042e2c8  56                    push      esi
  0042e2c9  e872c90400            call      0047AC40h
  0042e2ce  83c418                add       esp,18h
  0042e2d1  6aff                  push      0FFFFFFFFh
  0042e2d3  53                    push      ebx
  0042e2d4  68fd000000            push      0FDh
  0042e2d9  53                    push      ebx
  0042e2da  57                    push      edi
  0042e2db  56                    push      esi
  0042e2dc  e85fc90400            call      0047AC40h
  0042e2e1  83c418                add       esp,18h
  0042e2e4  6aff                  push      0FFFFFFFFh
  0042e2e6  53                    push      ebx
  0042e2e7  68fd000000            push      0FDh
  0042e2ec  53                    push      ebx
  0042e2ed  57                    push      edi
  0042e2ee  56                    push      esi
  0042e2ef  e84cc90400            call      0047AC40h
  0042e2f4  83c418                add       esp,18h
  0042e2f7  6aff                  push      0FFFFFFFFh
  0042e2f9  53                    push      ebx
  0042e2fa  68fd000000            push      0FDh
  0042e2ff  53                    push      ebx
  0042e300  57                    push      edi
  0042e301  56                    push      esi
  0042e302  e839c90400            call      0047AC40h
  0042e307  83c418                add       esp,18h
  0042e30a  6aff                  push      0FFFFFFFFh
  0042e30c  53                    push      ebx
  0042e30d  68fd000000            push      0FDh
  0042e312  53                    push      ebx
  0042e313  57                    push      edi
  0042e314  56                    push      esi
  0042e315  e826c90400            call      0047AC40h
  0042e31a  83c418                add       esp,18h
  0042e31d  6aff                  push      0FFFFFFFFh
  0042e31f  53                    push      ebx
  0042e320  68fd000000            push      0FDh
  0042e325  53                    push      ebx
  0042e326  57                    push      edi
  0042e327  56                    push      esi
  0042e328  e813c90400            call      0047AC40h
  0042e32d  83c418                add       esp,18h
  0042e330  6aff                  push      0FFFFFFFFh
  0042e332  53                    push      ebx
  0042e333  68fd000000            push      0FDh
  0042e338  53                    push      ebx
  0042e339  57                    push      edi
  0042e33a  56                    push      esi
  0042e33b  e800c90400            call      0047AC40h
  0042e340  83c418                add       esp,18h
  0042e343  6aff                  push      0FFFFFFFFh
  0042e345  53                    push      ebx
  0042e346  68fd000000            push      0FDh
  0042e34b  53                    push      ebx
  0042e34c  57                    push      edi
  0042e34d  56                    push      esi
  0042e34e  e8edc80400            call      0047AC40h
  0042e353  83c418                add       esp,18h

LAB_0042e356:
  0042e356  668b4616              mov       ax,[esi+16h]
  0042e35a  663d0300              cmp       ax,3
  0042e35e  7e42                  jle       short 0042E3A2h
  0042e360  663d0600              cmp       ax,6
  0042e364  7d3c                  jge       short 0042E3A2h
  0042e366  6aff                  push      0FFFFFFFFh
  0042e368  53                    push      ebx
  0042e369  8d7e1c                lea       edi,[esi+1Ch]
  0042e36c  68fc000000            push      0FCh
  0042e371  53                    push      ebx
  0042e372  57                    push      edi
  0042e373  56                    push      esi
  0042e374  e8c7c80400            call      0047AC40h
  0042e379  83c418                add       esp,18h
  0042e37c  6aff                  push      0FFFFFFFFh
  0042e37e  53                    push      ebx
  0042e37f  68fc000000            push      0FCh
  0042e384  53                    push      ebx
  0042e385  57                    push      edi
  0042e386  56                    push      esi
  0042e387  e8b4c80400            call      0047AC40h
  0042e38c  83c418                add       esp,18h
  0042e38f  6aff                  push      0FFFFFFFFh
  0042e391  53                    push      ebx
  0042e392  68fc000000            push      0FCh
  0042e397  53                    push      ebx
  0042e398  57                    push      edi
  0042e399  56                    push      esi
  0042e39a  e8a1c80400            call      0047AC40h
  0042e39f  83c418                add       esp,18h

LAB_0042e3a2:
  0042e3a2  66837e1639            cmp       word [esi+16h],39h
  0042e3a7  7d23                  jge       short 0042E3CCh
  0042e3a9  b8feffffff            mov       eax,0FFFFFFFEh
  0042e3ae  66c70566915e000200    mov       word [5E9166h],2
  0042e3b7  66010560915e00        add       [5E9160h],ax
  0042e3be  66010562915e00        add       [5E9162h],ax
  0042e3c5  66010564915e00        add       [5E9164h],ax

LAB_0042e3cc:
  0042e3cc  668b4616              mov       ax,[esi+16h]
  0042e3d0  663d3a00              cmp       ax,3Ah
  0042e3d4  7c53                  jl        short 0042E429h
  0042e3d6  663d3c00              cmp       ax,3Ch
  0042e3da  7f4d                  jg        short 0042E429h
  0042e3dc  66830564915e003c      add       word [5E9164h],3Ch
  0042e3e4  6aff                  push      0FFFFFFFFh
  0042e3e6  53                    push      ebx
  0042e3e7  8d7e1c                lea       edi,[esi+1Ch]
  0042e3ea  68fb000000            push      0FBh
  0042e3ef  53                    push      ebx
  0042e3f0  b81e000000            mov       eax,1Eh
  0042e3f5  57                    push      edi
  0042e3f6  66010560915e00        add       [5E9160h],ax
  0042e3fd  66010562915e00        add       [5E9162h],ax
  0042e404  56                    push      esi
  0042e405  66c70566915e000100    mov       word [5E9166h],1
  0042e40e  e82dc80400            call      0047AC40h
  0042e413  83c418                add       esp,18h
  0042e416  6aff                  push      0FFFFFFFFh
  0042e418  53                    push      ebx
  0042e419  68fb000000            push      0FBh
  0042e41e  53                    push      ebx
  0042e41f  57                    push      edi
  0042e420  56                    push      esi
  0042e421  e81ac80400            call      0047AC40h
  0042e426  83c418                add       esp,18h

LAB_0042e429:
  0042e429  66837e1641            cmp       word [esi+16h],41h
  0042e42e  7e24                  jle       short 0042E454h
  0042e430  b8fcffffff            mov       eax,0FFFFFFFCh
  0042e435  66c70566915e000200    mov       word [5E9166h],2
  0042e43e  66010560915e00        add       [5E9160h],ax
  0042e445  66010562915e00        add       [5E9162h],ax
  0042e44c  66830564915e00f9      add       word [5E9164h],0FFF9h

LAB_0042e454:
  0042e454  66837e163d            cmp       word [esi+16h],3Dh
  0042e459  7530                  jne       short 0042E48Bh
  0042e45b  6800160000            push      1600h
  0042e460  6800160000            push      1600h
  0042e465  6829030000            push      329h
  0042e46a  6a0b                  push      0Bh
  0042e46c  66c746120a00          mov       word [esi+12h],0Ah
  0042e472  e829ea1000            call      0053CEA0h
  0042e477  83c410                add       esp,10h
  0042e47a  6aff                  push      0FFFFFFFFh
  0042e47c  53                    push      ebx
  0042e47d  68ff000000            push      0FFh
  0042e482  56                    push      esi
  0042e483  e858c60400            call      0047AAE0h
  0042e488  83c410                add       esp,10h

LAB_0042e48b:
  0042e48b  66837e163e            cmp       word [esi+16h],3Eh
  0042e490  7519                  jne       short 0042E4ABh
  0042e492  6800160000            push      1600h
  0042e497  6800160000            push      1600h
  0042e49c  6800010000            push      100h
  0042e4a1  6a0b                  push      0Bh
  0042e4a3  e8f8e91000            call      0053CEA0h
  0042e4a8  83c410                add       esp,10h

LAB_0042e4ab:
  0042e4ab  668b4612              mov       ax,[esi+12h]
  0042e4af  663bc3                cmp       ax,bx
  0042e4b2  7442                  je        short 0042E4F6h
  0042e4b4  7e05                  jle       short 0042E4BBh
  0042e4b6  48                    dec       eax
  0042e4b7  66894612              mov       [esi+12h],ax

LAB_0042e4bb:
  0042e4bb  668b4612              mov       ax,[esi+12h]
  0042e4bf  663bc3                cmp       ax,bx
  0042e4c2  7d05                  jge       short 0042E4C9h
  0042e4c4  40                    inc       eax
  0042e4c5  66894612              mov       [esi+12h],ax

LAB_0042e4c9:
  0042e4c9  668b4612              mov       ax,[esi+12h]
  0042e4cd  53                    push      ebx
  0042e4ce  66f7d8                neg       ax
  0042e4d1  0fbfd0                movsx     edx,ax
  0042e4d4  53                    push      ebx
  0042e4d5  68e7030000            push      3E7h
  0042e4da  52                    push      edx
  0042e4db  68e7030000            push      3E7h
  0042e4e0  56                    push      esi
  0042e4e1  66894612              mov       [esi+12h],ax
  0042e4e5  e876d9feff            call      0041BE60h
  0042e4ea  8b4500                mov       eax,[ebp]
  0042e4ed  83c418                add       esp,18h
  0042e4f0  80cc40                or        ah,40h
  0042e4f3  894500                mov       [ebp],eax

LAB_0042e4f6:
  0042e4f6  f6461601              test      byte [esi+16h],1
  0042e4fa  7424                  je        short 0042E520h
  0042e4fc  8b442414              mov       eax,[esp+14h]
  0042e500  66ff451a              inc       word [ebp+1Ah]
  0042e504  668b4d1a              mov       cx,[ebp+1Ah]
  0042e508  668b4016              mov       ax,[eax+16h]
  0042e50c  0fbfc9                movsx     ecx,cx
  0042e50f  8bd0                  mov       edx,eax
  0042e511  81e2ffff0000          and       edx,0FFFFh
  0042e517  3bca                  cmp       ecx,edx
  0042e519  7c05                  jl        short 0042E520h
  0042e51b  48                    dec       eax
  0042e51c  6689451a              mov       [ebp+1Ah],ax

LAB_0042e520:
  0042e520  66ff4616              inc       word [esi+16h]
  0042e524  66837e164b            cmp       word [esi+16h],4Bh
  0042e529  0f8cc5000000          jl        near 0042E5F4h
  0042e52f  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0042e535  e876520d00            call      005037B0h
  0042e53a  a0e2965e00            mov       al,[5E96E2h]
  0042e53f  84c0                  test      al,al
  0042e541  7426                  je        short 0042E569h
  0042e543  33c0                  xor       eax,eax
  0042e545  53                    push      ebx
  0042e546  8a4509                mov       al,[ebp+9]
  0042e549  8d0c80                lea       ecx,[eax+eax*4]
  0042e54c  c1e103                shl       ecx,3
  0042e54f  2bc8                  sub       ecx,eax
  0042e551  a1cc975e00            mov       eax,[5E97CCh]
  0042e556  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0042e55d  52                    push      edx
  0042e55e  50                    push      eax
  0042e55f  e86c580e00            call      00513DD0h
  0042e564  83c40c                add       esp,0Ch
  0042e567  eb57                  jmp       short 0042E5C0h

LAB_0042e569:
  0042e569  33c0                  xor       eax,eax
  0042e56b  6a01                  push      1
  0042e56d  8a4509                mov       al,[ebp+9]
  0042e570  8d0c80                lea       ecx,[eax+eax*4]
  0042e573  c1e103                shl       ecx,3
  0042e576  2bc8                  sub       ecx,eax
  0042e578  a1cc975e00            mov       eax,[5E97CCh]
  0042e57d  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0042e584  52                    push      edx
  0042e585  50                    push      eax
  0042e586  e845580e00            call      00513DD0h
  0042e58b  83c40c                add       esp,0Ch
  0042e58e  bfc0935e00            mov       edi,5E93C0h
  0042e593  bb04000000            mov       ebx,4

LAB_0042e598:
  0042e598  8b07                  mov       eax,[edi]
  0042e59a  a900000080            test      eax,80000000h
  0042e59f  7416                  je        short 0042E5B7h
  0042e5a1  a900000050            test      eax,50000000h
  0042e5a6  750f                  jne       short 0042E5B7h
  0042e5a8  6aff                  push      0FFFFFFFFh
  0042e5aa  6a00                  push      0
  0042e5ac  6a59                  push      59h
  0042e5ae  57                    push      edi
  0042e5af  e81cc40400            call      0047A9D0h
  0042e5b4  83c410                add       esp,10h

LAB_0042e5b7:
  0042e5b7  81c79c000000          add       edi,9Ch
  0042e5bd  4b                    dec       ebx
  0042e5be  75d8                  jne       short 0042E598h

LAB_0042e5c0:
  0042e5c0  8b4500                mov       eax,[ebp]
  0042e5c3  0d00400002            or        eax,2004000h
  0042e5c8  894500                mov       [ebp],eax
  0042e5cb  668b4e50              mov       cx,[esi+50h]
  0042e5cf  66890d60915e00        mov       [5E9160h],cx
  0042e5d6  668b5654              mov       dx,[esi+54h]
  0042e5da  66891562915e00        mov       [5E9162h],dx
  0042e5e1  668b4658              mov       ax,[esi+58h]
  0042e5e5  66a364915e00          mov       [5E9164h],ax
  0042e5eb  66c70566915e000200    mov       word [5E9166h],2

LAB_0042e5f4:
  0042e5f4  5f                    pop       edi
  0042e5f5  5e                    pop       esi
  0042e5f6  5d                    pop       ebp
  0042e5f7  5b                    pop       ebx
  0042e5f8  c3                    ret
  0042e5f9  90                    nop
  0042e5fa  90                    nop
  0042e5fb  90                    nop
  0042e5fc  90                    nop
  0042e5fd  90                    nop
  0042e5fe  90                    nop
  0042e5ff  90                    nop

25 cible(s) de saut interne : 0x0042e167, 0x0042e179, 0x0042e1ea, 0x0042e20c, 0x0042e22f, 0x0042e252, 0x0042e275, 0x0042e298, 0x0042e2b0, 0x0042e356, 0x0042e3a2, 0x0042e3cc, 0x0042e429, 0x0042e454, 0x0042e48b, 0x0042e4ab, 0x0042e4bb, 0x0042e4c9, 0x0042e4f6, 0x0042e520, 0x0042e569, 0x0042e598, 0x0042e5b7, 0x0042e5c0, 0x0042e5f4
