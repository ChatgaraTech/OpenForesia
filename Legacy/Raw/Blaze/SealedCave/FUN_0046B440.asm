Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046b440  section .text  offset fichier 0x6a840  304 octets

  0046b440  53                    push      ebx
  0046b441  55                    push      ebp
  0046b442  56                    push      esi
  0046b443  8b742410              mov       esi,[esp+10h]
  0046b447  57                    push      edi
  0046b448  8b16                  mov       edx,[esi]
  0046b44a  8a5e07                mov       bl,[esi+7]
  0046b44d  80e6df                and       dh,0DFh
  0046b450  80e3ef                and       bl,0EFh
  0046b453  8bc2                  mov       eax,edx
  0046b455  885e07                mov       [esi+7],bl
  0046b458  a810                  test      al,10h
  0046b45a  8916                  mov       [esi],edx
  0046b45c  66c7462a7017          mov       word [esi+2Ah],1770h
  0046b462  750b                  jne       short 0046B46Fh
  0046b464  66837e1000            cmp       word [esi+10h],0
  0046b469  0f84f3000000          je        near 0046B562h

LAB_0046b46f:
  0046b46f  6800000040            push      40000000h
  0046b474  6a10                  push      10h
  0046b476  56                    push      esi
  0046b477  e8849b0c00            call      00535000h
  0046b47c  83c40c                add       esp,0Ch
  0046b47f  8be8                  mov       ebp,eax
  0046b481  33db                  xor       ebx,ebx
  0046b483  bac0935e00            mov       edx,5E93C0h
  0046b488  bf04000000            mov       edi,4
  0046b48d  b80000ceff            mov       eax,0FFCE0000h

LAB_0046b492:
  0046b492  8b0a                  mov       ecx,[edx]
  0046b494  f7c100000080          test      ecx,80000000h
  0046b49a  740e                  je        short 0046B4AAh
  0046b49c  f7c100000050          test      ecx,50000000h
  0046b4a2  7506                  jne       short 0046B4AAh
  0046b4a4  394220                cmp       [edx+20h],eax
  0046b4a7  7d01                  jge       short 0046B4AAh
  0046b4a9  43                    inc       ebx

LAB_0046b4aa:
  0046b4aa  81c29c000000          add       edx,9Ch
  0046b4b0  4f                    dec       edi
  0046b4b1  75df                  jne       short 0046B492h
  0046b4b3  663beb                cmp       bp,bx
  0046b4b6  740e                  je        short 0046B4C6h
  0046b4b8  33db                  xor       ebx,ebx
  0046b4ba  66395e10              cmp       [esi+10h],bx
  0046b4be  0f849e000000          je        near 0046B562h
  0046b4c4  eb02                  jmp       short 0046B4C8h

LAB_0046b4c6:
  0046b4c6  33db                  xor       ebx,ebx

LAB_0046b4c8:
  0046b4c8  66395e10              cmp       [esi+10h],bx
  0046b4cc  b901000000            mov       ecx,1
  0046b4d1  7532                  jne       short 0046B505h
  0046b4d3  0fbf4614              movsx     eax,word [esi+14h]
  0046b4d7  66894e10              mov       [esi+10h],cx
  0046b4db  66895e12              mov       [esi+12h],bx
  0046b4df  8d1480                lea       edx,[eax+eax*4]
  0046b4e2  c1e203                shl       edx,3
  0046b4e5  2bd0                  sub       edx,eax
  0046b4e7  66894c9610            mov       [esi+edx*4+10h],cx
  0046b4ec  8b4620                mov       eax,[esi+20h]
  0046b4ef  c1f80c                sar       eax,0Ch
  0046b4f2  66395e16              cmp       [esi+16h],bx
  0046b4f6  6689869a000000        mov       [esi+9Ah],ax
  0046b4fd  7506                  jne       short 0046B505h
  0046b4ff  66c746161400          mov       word [esi+16h],14h

LAB_0046b505:
  0046b505  66394e10              cmp       [esi+10h],cx
  0046b509  7557                  jne       short 0046B562h
  0046b50b  0fbf4e12              movsx     ecx,word [esi+12h]
  0046b50f  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  0046b516  51                    push      ecx
  0046b517  e800a50d00            call      00545A1Ch
  0046b51c  8bd7                  mov       edx,edi
  0046b51e  668146120001          add       word [esi+12h],100h
  0046b524  c1e204                shl       edx,4
  0046b527  03c2                  add       eax,edx
  0046b529  c1e008                shl       eax,8
  0046b52c  894620                mov       [esi+20h],eax
  0046b52f  668b4612              mov       ax,[esi+12h]
  0046b533  663d0008              cmp       ax,800h
  0046b537  7e0a                  jle       short 0046B543h
  0046b539  c1e70c                shl       edi,0Ch
  0046b53c  66895e10              mov       [esi+10h],bx
  0046b540  897e20                mov       [esi+20h],edi

LAB_0046b543:
  0046b543  663d0004              cmp       ax,400h
  0046b547  7519                  jne       short 0046B562h
  0046b549  0fbf4616              movsx     eax,word [esi+16h]
  0046b54d  8a5607                mov       dl,[esi+7]
  0046b550  895e50                mov       [esi+50h],ebx
  0046b553  80ca10                or        dl,10h
  0046b556  895e58                mov       [esi+58h],ebx
  0046b559  c1e00c                shl       eax,0Ch
  0046b55c  885607                mov       [esi+7],dl
  0046b55f  894654                mov       [esi+54h],eax

LAB_0046b562:
  0046b562  5f                    pop       edi
  0046b563  5e                    pop       esi
  0046b564  5d                    pop       ebp
  0046b565  5b                    pop       ebx
  0046b566  c3                    ret
  0046b567  90                    nop
  0046b568  90                    nop
  0046b569  90                    nop
  0046b56a  90                    nop
  0046b56b  90                    nop
  0046b56c  90                    nop
  0046b56d  90                    nop
  0046b56e  90                    nop
  0046b56f  90                    nop

8 cible(s) de saut interne : 0x0046b46f, 0x0046b492, 0x0046b4aa, 0x0046b4c6, 0x0046b4c8, 0x0046b505, 0x0046b543, 0x0046b562
