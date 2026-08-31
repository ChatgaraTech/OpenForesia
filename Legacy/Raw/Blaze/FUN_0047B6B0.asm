
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b6b0  section .text  offset fichier 0x7aab0  148 octets

  0047b6b0  56                    push      esi
  0047b6b1  8b742408              mov       esi,[esp+8]
  0047b6b5  57                    push      edi
  0047b6b6  66837e1000            cmp       word [esi+10h],0
  0047b6bb  751a                  jne       short 0047B6D7h
  0047b6bd  b800180000            mov       eax,1800h
  0047b6c2  c7463880808000        mov       dword [esi+38h],808080h
  0047b6c9  66894628              mov       [esi+28h],ax
  0047b6cd  6689462c              mov       [esi+2Ch],ax
  0047b6d1  c646041e              mov       byte [esi+4],1Eh
  0047b6d5  eb07                  jmp       short 0047B6DEh

LAB_0047b6d7:
  0047b6d7  814638f0efefff        add       dword [esi+38h],0FFEFEFF0h

LAB_0047b6de:
  0047b6de  0fbf4612              movsx     eax,word [esi+12h]
  0047b6e2  6681462880fc          add       word [esi+28h],0FC80h
  0047b6e8  c1e008                shl       eax,8
  0047b6eb  50                    push      eax
  0047b6ec  e82ba30c00            call      00545A1Ch
  0047b6f1  85c0                  test      eax,eax
  0047b6f3  7c0f                  jl        short 0047B704h
  0047b6f5  0fbf4e12              movsx     ecx,word [esi+12h]
  0047b6f9  c1e108                shl       ecx,8
  0047b6fc  51                    push      ecx
  0047b6fd  e81aa30c00            call      00545A1Ch
  0047b702  eb0f                  jmp       short 0047B713h

LAB_0047b704:
  0047b704  0fbf5612              movsx     edx,word [esi+12h]
  0047b708  c1e208                shl       edx,8
  0047b70b  52                    push      edx
  0047b70c  e80ba30c00            call      00545A1Ch
  0047b711  f7d8                  neg       eax

LAB_0047b713:
  0047b713  668b4e10              mov       cx,[esi+10h]
  0047b717  bf00100000            mov       edi,1000h
  0047b71c  0fbfd1                movsx     edx,cx
  0047b71f  c1e209                shl       edx,9
  0047b722  2bfa                  sub       edi,edx
  0047b724  0faff8                imul      edi,eax
  0047b727  8d4101                lea       eax,[ecx+1]
  0047b72a  c1ff0b                sar       edi,0Bh
  0047b72d  663d0800              cmp       ax,8
  0047b731  66897e2a              mov       [esi+2Ah],di
  0047b735  66894610              mov       [esi+10h],ax
  0047b739  7c06                  jl        short 0047B741h
  0047b73b  c70600000000          mov       dword [esi],0

LAB_0047b741:
  0047b741  5f                    pop       edi
  0047b742  5e                    pop       esi
  0047b743  c3                    ret

5 cible(s) de saut interne : 0x0047b6d7, 0x0047b6de, 0x0047b704, 0x0047b713, 0x0047b741
