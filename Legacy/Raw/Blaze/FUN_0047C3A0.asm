
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047c3a0  section .text  offset fichier 0x7b7a0  173 octets

  0047c3a0  53                    push      ebx
  0047c3a1  55                    push      ebp
  0047c3a2  56                    push      esi
  0047c3a3  8b742410              mov       esi,[esp+10h]
  0047c3a7  33c0                  xor       eax,eax
  0047c3a9  57                    push      edi
  0047c3aa  804e3606              or        byte [esi+36h],6
  0047c3ae  6681463200fe          add       word [esi+32h],0FE00h
  0047c3b4  894628                mov       [esi+28h],eax
  0047c3b7  6689462c              mov       [esi+2Ch],ax
  0047c3bb  c74638e0e0e0c0        mov       dword [esi+38h],0C0E0E0E0h
  0047c3c2  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047c3c8  c646041e              mov       byte [esi+4],1Eh
  0047c3cc  bb05000000            mov       ebx,5
  0047c3d1  bd00010000            mov       ebp,100h

LAB_0047c3d6:
  0047c3d6  0fbf7e32              movsx     edi,word [esi+32h]
  0047c3da  57                    push      edi
  0047c3db  e83c960c00            call      00545A1Ch
  0047c3e0  8d0c80                lea       ecx,[eax+eax*4]
  0047c3e3  57                    push      edi
  0047c3e4  8d1448                lea       edx,[eax+ecx*2]
  0047c3e7  d1e2                  shl       edx,1
  0047c3e9  895650                mov       [esi+50h],edx
  0047c3ec  e825960c00            call      00545A16h
  0047c3f1  8d0c80                lea       ecx,[eax+eax*4]
  0047c3f4  6aff                  push      0FFFFFFFFh
  0047c3f6  8d1448                lea       edx,[eax+ecx*2]
  0047c3f9  33c0                  xor       eax,eax
  0047c3fb  8a460b                mov       al,[esi+0Bh]
  0047c3fe  50                    push      eax
  0047c3ff  6a6b                  push      6Bh
  0047c401  d1e2                  shl       edx,1
  0047c403  56                    push      esi
  0047c404  895658                mov       [esi+58h],edx
  0047c407  e8d4e6ffff            call      0047AAE0h
  0047c40c  66016e32              add       [esi+32h],bp
  0047c410  83c410                add       esp,10h
  0047c413  4b                    dec       ebx
  0047c414  75c0                  jne       short 0047C3D6h
  0047c416  8d4e1c                lea       ecx,[esi+1Ch]
  0047c419  c70560915e0060006000  mov       dword [5E9160h],600060h
  0047c423  51                    push      ecx
  0047c424  6800120000            push      1200h
  0047c429  6800040000            push      400h
  0047c42e  6a1a                  push      1Ah
  0047c430  c70564915e0060000200  mov       dword [5E9164h],20060h
  0047c43a  e8e1160c00            call      0053DB20h
  0047c43f  83c410                add       esp,10h
  0047c442  c70600000000          mov       dword [esi],0
  0047c448  5f                    pop       edi
  0047c449  5e                    pop       esi
  0047c44a  5d                    pop       ebp
  0047c44b  5b                    pop       ebx
  0047c44c  c3                    ret

1 cible(s) de saut interne : 0x0047c3d6
