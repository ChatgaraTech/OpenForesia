Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004444a0  section .text  offset fichier 0x438a0  384 octets

  004444a0  a058e05d00            mov       al,[5DE058h]
  004444a5  83ec08                sub       esp,8
  004444a8  84c0                  test      al,al
  004444aa  53                    push      ebx
  004444ab  55                    push      ebp
  004444ac  56                    push      esi
  004444ad  57                    push      edi
  004444ae  0f8559010000          jne       near 0044460Dh
  004444b4  f60500975e0020        test      byte [5E9700h],20h
  004444bb  0f854c010000          jne       near 0044460Dh
  004444c1  33ed                  xor       ebp,ebp
  004444c3  bef8935e00            mov       esi,5E93F8h
  004444c8  896c2410              mov       [esp+10h],ebp
  004444cc  c744241404000000      mov       dword [esp+14h],4
  004444d4  b3ff                  mov       bl,0FFh
  004444d6  bffbffffff            mov       edi,0FFFFFFFBh

LAB_004444db:
  004444db  8b56c8                mov       edx,[esi-38h]
  004444de  8d46c8                lea       eax,[esi-38h]
  004444e1  f7c200000080          test      edx,80000000h
  004444e7  7460                  je        short 00444549h
  004444e9  f7c200000150          test      edx,50010000h
  004444ef  7558                  jne       short 00444549h
  004444f1  8b4c2410              mov       ecx,[esp+10h]
  004444f5  41                    inc       ecx
  004444f6  817ee400c05d00        cmp       dword [esi-1Ch],5DC000h
  004444fd  894c2410              mov       [esp+10h],ecx
  00444501  8d4ee4                lea       ecx,[esi-1Ch]
  00444504  7e43                  jle       short 00444549h
  00444506  817ee800c0e5ff        cmp       dword [esi-18h],0FFE5C000h
  0044450d  7e3a                  jle       short 00444549h
  0044450f  45                    inc       ebp
  00444510  f7c201000004          test      edx,4000001h
  00444516  7531                  jne       short 00444549h
  00444518  881e                  mov       [esi],bl
  0044451a  885e01                mov       [esi+1],bl
  0044451d  885e02                mov       [esi+2],bl
  00444520  66217efe              and       [esi-2],di
  00444524  6aff                  push      0FFFFFFFFh
  00444526  66c7465e0000          mov       word [esi+5Eh],0
  0044452c  8b10                  mov       edx,[eax]
  0044452e  6a00                  push      0
  00444530  689d000000            push      9Dh
  00444535  6a00                  push      0
  00444537  81ca00000004          or        edx,4000000h
  0044453d  51                    push      ecx
  0044453e  50                    push      eax
  0044453f  8910                  mov       [eax],edx
  00444541  e8fa660300            call      0047AC40h
  00444546  83c418                add       esp,18h

LAB_00444549:
  00444549  8b442414              mov       eax,[esp+14h]
  0044454d  81c69c000000          add       esi,9Ch
  00444553  48                    dec       eax
  00444554  89442414              mov       [esp+14h],eax
  00444558  7581                  jne       short 004444DBh
  0044455a  8b442410              mov       eax,[esp+10h]
  0044455e  663bc5                cmp       ax,bp
  00444561  0f85a6000000          jne       near 0044460Dh
  00444567  6685c0                test      ax,ax
  0044456a  0f849d000000          je        near 0044460Dh
  00444570  6685ed                test      bp,bp
  00444573  0f8494000000          je        near 0044460Dh
  00444579  33c0                  xor       eax,eax
  0044457b  a038e55d00            mov       al,[5DE538h]
  00444580  8d0c80                lea       ecx,[eax+eax*4]
  00444583  c1e103                shl       ecx,3
  00444586  2bc8                  sub       ecx,eax
  00444588  8d048dc0935e00        lea       eax,[ecx*4+5E93C0h]
  0044458f  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  00444596  f7c1000000d0          test      ecx,0D0000000h
  0044459c  751f                  jne       short 004445BDh
  0044459e  0fbf542420            movsx     edx,word [esp+20h]
  004445a3  52                    push      edx
  004445a4  50                    push      eax
  004445a5  a1cc975e00            mov       eax,[5E97CCh]
  004445aa  50                    push      eax
  004445ab  e820f80c00            call      00513DD0h
  004445b0  83c40c                add       esp,0Ch
  004445b3  33c0                  xor       eax,eax
  004445b5  5f                    pop       edi
  004445b6  5e                    pop       esi
  004445b7  5d                    pop       ebp
  004445b8  5b                    pop       ebx
  004445b9  83c408                add       esp,8
  004445bc  c3                    ret

LAB_004445bd:
  004445bd  668b742420            mov       si,[esp+20h]
  004445c2  33c9                  xor       ecx,ecx

LAB_004445c4:
  004445c4  0fbfc1                movsx     eax,cx
  004445c7  8d1480                lea       edx,[eax+eax*4]
  004445ca  c1e203                shl       edx,3
  004445cd  2bd0                  sub       edx,eax
  004445cf  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  004445d6  8b1495c0935e00        mov       edx,[edx*4+5E93C0h]
  004445dd  f7c200000080          test      edx,80000000h
  004445e3  7421                  je        short 00444606h
  004445e5  f7c200000150          test      edx,50010000h
  004445eb  7519                  jne       short 00444606h
  004445ed  8b15cc975e00          mov       edx,[5E97CCh]
  004445f3  0fbfce                movsx     ecx,si
  004445f6  51                    push      ecx
  004445f7  50                    push      eax
  004445f8  52                    push      edx
  004445f9  e8d2f70c00            call      00513DD0h
  004445fe  83c40c                add       esp,0Ch
  00444601  b963000000            mov       ecx,63h

LAB_00444606:
  00444606  41                    inc       ecx
  00444607  6683f903              cmp       cx,3
  0044460b  7eb7                  jle       short 004445C4h

LAB_0044460d:
  0044460d  5f                    pop       edi
  0044460e  5e                    pop       esi
  0044460f  5d                    pop       ebp
  00444610  33c0                  xor       eax,eax
  00444612  5b                    pop       ebx
  00444613  83c408                add       esp,8
  00444616  c3                    ret
  00444617  90                    nop
  00444618  90                    nop
  00444619  90                    nop
  0044461a  90                    nop
  0044461b  90                    nop
  0044461c  90                    nop
  0044461d  90                    nop
  0044461e  90                    nop
  0044461f  90                    nop

6 cible(s) de saut interne : 0x004444db, 0x00444549, 0x004445bd, 0x004445c4, 0x00444606, 0x0044460d
