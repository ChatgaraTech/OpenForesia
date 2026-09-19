Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046b340  section .text  offset fichier 0x6a740  256 octets

  0046b340  53                    push      ebx
  0046b341  56                    push      esi
  0046b342  8b74240c              mov       esi,[esp+0Ch]
  0046b346  bb01000000            mov       ebx,1
  0046b34b  66837e1000            cmp       word [esi+10h],0
  0046b350  7517                  jne       short 0046B369h
  0046b352  8b4620                mov       eax,[esi+20h]
  0046b355  66895e10              mov       [esi+10h],bx
  0046b359  c1f80c                sar       eax,0Ch
  0046b35c  66894612              mov       [esi+12h],ax
  0046b360  66c786980000000000    mov       word [esi+98h],0

LAB_0046b369:
  0046b369  66837e1002            cmp       word [esi+10h],2
  0046b36e  757a                  jne       short 0046B3EAh
  0046b370  6683be9800000000      cmp       word [esi+98h],0
  0046b378  7532                  jne       short 0046B3ACh
  0046b37a  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046b380  6a00                  push      0
  0046b382  6a00                  push      0
  0046b384  6800200000            push      2000h
  0046b389  8b91a8000000          mov       edx,[ecx+0A8h]
  0046b38f  6800020000            push      200h
  0046b394  8b02                  mov       eax,[edx]
  0046b396  660fb64825            movzx     cx,byte [eax+25h]
  0046b39b  51                    push      ecx
  0046b39c  56                    push      esi
  0046b39d  e8deccffff            call      00468080h
  0046b3a2  83c418                add       esp,18h
  0046b3a5  66899e98000000        mov       [esi+98h],bx

LAB_0046b3ac:
  0046b3ac  0fbf5612              movsx     edx,word [esi+12h]
  0046b3b0  0fbf4614              movsx     eax,word [esi+14h]
  0046b3b4  2bd0                  sub       edx,eax
  0046b3b6  668b4616              mov       ax,[esi+16h]
  0046b3ba  c1e20c                shl       edx,0Ch
  0046b3bd  6685c0                test      ax,ax
  0046b3c0  895620                mov       [esi+20h],edx
  0046b3c3  66895e10              mov       [esi+10h],bx
  0046b3c7  7421                  je        short 0046B3EAh
  0046b3c9  0fbfc8                movsx     ecx,ax
  0046b3cc  8bc1                  mov       eax,ecx
  0046b3ce  99                    cdq
  0046b3cf  83e207                and       edx,7
  0046b3d2  03c2                  add       eax,edx
  0046b3d4  c1f803                sar       eax,3
  0046b3d7  8d14c500000000        lea       edx,[eax*8]
  0046b3de  2bca                  sub       ecx,edx
  0046b3e0  8ad3                  mov       dl,bl
  0046b3e2  d2e2                  shl       dl,cl
  0046b3e4  0890e0965e00          or        [eax+5E96E0h],dl

LAB_0046b3ea:
  0046b3ea  66837e1003            cmp       word [esi+10h],3
  0046b3ef  7517                  jne       short 0046B408h
  0046b3f1  0fbf4612              movsx     eax,word [esi+12h]
  0046b3f5  c1e00c                shl       eax,0Ch
  0046b3f8  66c786980000000000    mov       word [esi+98h],0
  0046b401  894620                mov       [esi+20h],eax
  0046b404  66895e10              mov       [esi+10h],bx

LAB_0046b408:
  0046b408  6800000040            push      40000000h
  0046b40d  6a10                  push      10h
  0046b40f  56                    push      esi
  0046b410  e8eb9b0c00            call      00535000h
  0046b415  83c40c                add       esp,0Ch
  0046b418  85c0                  test      eax,eax
  0046b41a  7410                  je        short 0046B42Ch
  0046b41c  66395e10              cmp       [esi+10h],bx
  0046b420  7506                  jne       short 0046B428h
  0046b422  66c746100200          mov       word [esi+10h],2

LAB_0046b428:
  0046b428  85c0                  test      eax,eax
  0046b42a  750c                  jne       short 0046B438h

LAB_0046b42c:
  0046b42c  66395e10              cmp       [esi+10h],bx
  0046b430  7506                  jne       short 0046B438h
  0046b432  66c746100300          mov       word [esi+10h],3

LAB_0046b438:
  0046b438  5e                    pop       esi
  0046b439  5b                    pop       ebx
  0046b43a  c3                    ret
  0046b43b  90                    nop
  0046b43c  90                    nop
  0046b43d  90                    nop
  0046b43e  90                    nop
  0046b43f  90                    nop

7 cible(s) de saut interne : 0x0046b369, 0x0046b3ac, 0x0046b3ea, 0x0046b408, 0x0046b428, 0x0046b42c, 0x0046b438
