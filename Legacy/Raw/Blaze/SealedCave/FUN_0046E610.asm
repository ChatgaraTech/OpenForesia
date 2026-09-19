Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046e610  section .text  offset fichier 0x6da10  192 octets

  0046e610  53                    push      ebx
  0046e611  56                    push      esi
  0046e612  8b74240c              mov       esi,[esp+0Ch]
  0046e616  57                    push      edi
  0046e617  8a5e07                mov       bl,[esi+7]
  0046e61a  8b16                  mov       edx,[esi]
  0046e61c  80e3ef                and       bl,0EFh
  0046e61f  80e6df                and       dh,0DFh
  0046e622  885e07                mov       [esi+7],bl
  0046e625  8bc2                  mov       eax,edx
  0046e627  33db                  xor       ebx,ebx
  0046e629  8916                  mov       [esi],edx
  0046e62b  a810                  test      al,10h
  0046e62d  66c7462a7017          mov       word [esi+2Ah],1770h
  0046e633  750a                  jne       short 0046E63Fh
  0046e635  66395e10              cmp       [esi+10h],bx
  0046e639  0f8484000000          je        near 0046E6C3h

LAB_0046e63f:
  0046e63f  66395e10              cmp       [esi+10h],bx
  0046e643  b901000000            mov       ecx,1
  0046e648  7523                  jne       short 0046E66Dh
  0046e64a  0fbf4614              movsx     eax,word [esi+14h]
  0046e64e  66894e10              mov       [esi+10h],cx
  0046e652  66895e12              mov       [esi+12h],bx
  0046e656  8d1480                lea       edx,[eax+eax*4]
  0046e659  c1e203                shl       edx,3
  0046e65c  2bd0                  sub       edx,eax
  0046e65e  66894c9610            mov       [esi+edx*4+10h],cx
  0046e663  8b4620                mov       eax,[esi+20h]
  0046e666  c1f80c                sar       eax,0Ch
  0046e669  66894616              mov       [esi+16h],ax

LAB_0046e66d:
  0046e66d  66394e10              cmp       [esi+10h],cx
  0046e671  7550                  jne       short 0046E6C3h
  0046e673  0fbf4e12              movsx     ecx,word [esi+12h]
  0046e677  0fbf7e16              movsx     edi,word [esi+16h]
  0046e67b  51                    push      ecx
  0046e67c  e89b730d00            call      00545A1Ch
  0046e681  8bd7                  mov       edx,edi
  0046e683  668146120001          add       word [esi+12h],100h
  0046e689  c1e204                shl       edx,4
  0046e68c  03c2                  add       eax,edx
  0046e68e  c1e008                shl       eax,8
  0046e691  894620                mov       [esi+20h],eax
  0046e694  668b4612              mov       ax,[esi+12h]
  0046e698  663d0008              cmp       ax,800h
  0046e69c  7e0a                  jle       short 0046E6A8h
  0046e69e  c1e70c                shl       edi,0Ch
  0046e6a1  66895e10              mov       [esi+10h],bx
  0046e6a5  897e20                mov       [esi+20h],edi

LAB_0046e6a8:
  0046e6a8  663d0004              cmp       ax,400h
  0046e6ac  7515                  jne       short 0046E6C3h
  0046e6ae  8a4607                mov       al,[esi+7]
  0046e6b1  c7465400400100        mov       dword [esi+54h],14000h
  0046e6b8  0c10                  or        al,10h
  0046e6ba  895e50                mov       [esi+50h],ebx
  0046e6bd  884607                mov       [esi+7],al
  0046e6c0  895e58                mov       [esi+58h],ebx

LAB_0046e6c3:
  0046e6c3  5f                    pop       edi
  0046e6c4  5e                    pop       esi
  0046e6c5  5b                    pop       ebx
  0046e6c6  c3                    ret
  0046e6c7  90                    nop
  0046e6c8  90                    nop
  0046e6c9  90                    nop
  0046e6ca  90                    nop
  0046e6cb  90                    nop
  0046e6cc  90                    nop
  0046e6cd  90                    nop
  0046e6ce  90                    nop
  0046e6cf  90                    nop

4 cible(s) de saut interne : 0x0046e63f, 0x0046e66d, 0x0046e6a8, 0x0046e6c3
