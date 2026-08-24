Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00513660  section .text  offset fichier 0x112a60  336 octets

  00513660  83ec08                sub       esp,8
  00513663  53                    push      ebx
  00513664  8b1dc4856000          mov       ebx,[6085C4h]
  0051366a  55                    push      ebp
  0051366b  56                    push      esi
  0051366c  57                    push      edi
  0051366d  33ff                  xor       edi,edi

LAB_0051366f:
  0051366f  6aff                  push      0FFFFFFFFh
  00513671  57                    push      edi
  00513672  6838b85d00            push      5DB838h
  00513677  6a09                  push      9
  00513679  ffd3                  call      ebx
  0051367b  8bf0                  mov       esi,eax
  0051367d  83fe08                cmp       esi,8
  00513680  7507                  jne       short 00513689h
  00513682  57                    push      edi
  00513683  ff15b4856000          call      dword [6085B4h]

LAB_00513689:
  00513689  393d9cb85d00          cmp       [5DB89Ch],edi
  0051368f  740a                  je        short 0051369Bh
  00513691  3bf7                  cmp       esi,edi
  00513693  893d9cb85d00          mov       [5DB89Ch],edi
  00513699  74d4                  je        short 0051366Fh

LAB_0051369b:
  0051369b  3bf7                  cmp       esi,edi
  0051369d  7cd0                  jl        short 0051366Fh
  0051369f  83fe08                cmp       esi,8
  005136a2  7dcb                  jge       short 0051366Fh
  005136a4  3bf7                  cmp       esi,edi
  005136a6  7507                  jne       short 005136AFh
  005136a8  be07000000            mov       esi,7
  005136ad  eb01                  jmp       short 005136B0h

LAB_005136af:
  005136af  4e                    dec       esi

LAB_005136b0:
  005136b0  57                    push      edi
  005136b1  a100b95d00            mov       eax,[5DB900h]
  005136b6  57                    push      edi
  005136b7  8d54241c              lea       edx,[esp+1Ch]
  005136bb  57                    push      edi
  005136bc  52                    push      edx
  005136bd  8b08                  mov       ecx,[eax]
  005136bf  8d542420              lea       edx,[esp+20h]
  005136c3  52                    push      edx
  005136c4  8b15a8b85d00          mov       edx,[5DB8A8h]
  005136ca  52                    push      edx
  005136cb  8b14f5a0b85d00        mov       edx,[esi*8+5DB8A0h]
  005136d2  52                    push      edx
  005136d3  50                    push      eax
  005136d4  ff512c                call      dword [ecx+2Ch]
  005136d7  a160b85d00            mov       eax,[5DB860h]
  005136dc  8b0d50b75d00          mov       ecx,[5DB750h]
  005136e2  8d500a                lea       edx,[eax+0Ah]
  005136e5  3bd1                  cmp       edx,ecx
  005136e7  7d37                  jge       short 00513720h
  005136e9  8b15f4b85d00          mov       edx,[5DB8F4h]
  005136ef  33c9                  xor       ecx,ecx
  005136f1  8b2df8b85d00          mov       ebp,[5DB8F8h]
  005136f7  668b4a0c              mov       cx,[edx+0Ch]
  005136fb  0fafc1                imul      eax,ecx
  005136fe  8d3489                lea       esi,[ecx+ecx*4]
  00513701  03c5                  add       eax,ebp
  00513703  d1e6                  shl       esi,1
  00513705  56                    push      esi
  00513706  8b742414              mov       esi,[esp+14h]
  0051370a  56                    push      esi
  0051370b  6820b85d00            push      5DB820h
  00513710  50                    push      eax
  00513711  52                    push      edx
  00513712  e899e40100            call      00531BB0h
  00513717  830560b85d000a        add       dword [5DB860h],0Ah
  0051371e  eb65                  jmp       short 00513785h

LAB_00513720:
  00513720  8b15f4b85d00          mov       edx,[5DB8F4h]
  00513726  2bc8                  sub       ecx,eax
  00513728  8bf1                  mov       esi,ecx
  0051372a  33c9                  xor       ecx,ecx
  0051372c  668b4a0c              mov       cx,[edx+0Ch]
  00513730  8bee                  mov       ebp,esi
  00513732  0fafe9                imul      ebp,ecx
  00513735  0fafc1                imul      eax,ecx
  00513738  55                    push      ebp
  00513739  8b6c2414              mov       ebp,[esp+14h]
  0051373d  55                    push      ebp
  0051373e  8b2df8b85d00          mov       ebp,[5DB8F8h]
  00513744  03c5                  add       eax,ebp
  00513746  6820b85d00            push      5DB820h
  0051374b  50                    push      eax
  0051374c  52                    push      edx
  0051374d  e85ee40100            call      00531BB0h
  00513752  8b0df4b85d00          mov       ecx,[5DB8F4h]
  00513758  b80a000000            mov       eax,0Ah
  0051375d  2bc6                  sub       eax,esi
  0051375f  33d2                  xor       edx,edx
  00513761  a360b85d00            mov       [5DB860h],eax
  00513766  668b510c              mov       dx,[ecx+0Ch]
  0051376a  0fafd0                imul      edx,eax
  0051376d  8b442410              mov       eax,[esp+10h]
  00513771  52                    push      edx
  00513772  8b15f8b85d00          mov       edx,[5DB8F8h]
  00513778  50                    push      eax
  00513779  6820b85d00            push      5DB820h
  0051377e  52                    push      edx
  0051377f  51                    push      ecx
  00513780  e82be40100            call      00531BB0h

LAB_00513785:
  00513785  8b542414              mov       edx,[esp+14h]
  00513789  a100b95d00            mov       eax,[5DB900h]
  0051378e  57                    push      edi
  0051378f  57                    push      edi
  00513790  8b08                  mov       ecx,[eax]
  00513792  52                    push      edx
  00513793  8b54241c              mov       edx,[esp+1Ch]
  00513797  52                    push      edx
  00513798  50                    push      eax
  00513799  ff514c                call      dword [ecx+4Ch]
  0051379c  e9cefeffff            jmp       0051366Fh
  005137a1  90                    nop
  005137a2  90                    nop
  005137a3  90                    nop
  005137a4  90                    nop
  005137a5  90                    nop
  005137a6  90                    nop
  005137a7  90                    nop
  005137a8  90                    nop
  005137a9  90                    nop
  005137aa  90                    nop
  005137ab  90                    nop
  005137ac  90                    nop
  005137ad  90                    nop
  005137ae  90                    nop
  005137af  90                    nop

7 cible(s) de saut interne : 0x0051366f, 0x00513689, 0x0051369b, 0x005136af, 0x005136b0, 0x00513720, 0x00513785
