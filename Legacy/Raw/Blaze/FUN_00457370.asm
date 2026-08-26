Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00457370  section .text  offset fichier 0x56770  1023 octets

  00457370  53                    push      ebx
  00457371  55                    push      ebp
  00457372  56                    push      esi
  00457373  8b742410              mov       esi,[esp+10h]
  00457377  33ed                  xor       ebp,ebp
  00457379  57                    push      edi
  0045737a  66396e16              cmp       [esi+16h],bp
  0045737e  7511                  jne       short 00457391h
  00457380  0fbf4614              movsx     eax,word [esi+14h]
  00457384  894644                mov       [esi+44h],eax
  00457387  66896e14              mov       [esi+14h],bp
  0045738b  66c746160100          mov       word [esi+16h],1

LAB_00457391:
  00457391  66396e10              cmp       [esi+10h],bp
  00457395  750a                  jne       short 004573A1h
  00457397  6689ae98000000        mov       [esi+98h],bp
  0045739e  896e5c                mov       [esi+5Ch],ebp

LAB_004573a1:
  004573a1  896c2414              mov       [esp+14h],ebp
  004573a5  bfe0935e00            mov       edi,5E93E0h
  004573aa  bb04000000            mov       ebx,4

LAB_004573af:
  004573af  8b47e0                mov       eax,[edi-20h]
  004573b2  a900000080            test      eax,80000000h
  004573b7  0f849f000000          je        near 0045745Ch
  004573bd  a900000050            test      eax,50000000h
  004573c2  0f8594000000          jne       near 0045745Ch
  004573c8  8b4e1c                mov       ecx,[esi+1Ch]
  004573cb  8b47fc                mov       eax,[edi-4]
  004573ce  8b5620                mov       edx,[esi+20h]
  004573d1  8b6e24                mov       ebp,[esi+24h]
  004573d4  c1f90c                sar       ecx,0Ch
  004573d7  c1f80c                sar       eax,0Ch
  004573da  2bc1                  sub       eax,ecx
  004573dc  8b0f                  mov       ecx,[edi]
  004573de  c1f90c                sar       ecx,0Ch
  004573e1  c1fa0c                sar       edx,0Ch
  004573e4  2bca                  sub       ecx,edx
  004573e6  8b5704                mov       edx,[edi+4]
  004573e9  c1fd0c                sar       ebp,0Ch
  004573ec  c1fa0c                sar       edx,0Ch
  004573ef  2bd5                  sub       edx,ebp
  004573f1  8be8                  mov       ebp,eax
  004573f3  0fafe8                imul      ebp,eax
  004573f6  8bc1                  mov       eax,ecx
  004573f8  0fafc1                imul      eax,ecx
  004573fb  8bca                  mov       ecx,edx
  004573fd  03e8                  add       ebp,eax
  004573ff  0fafca                imul      ecx,edx
  00457402  03e9                  add       ebp,ecx
  00457404  55                    push      ebp
  00457405  e806e60e00            call      00545A10h
  0045740a  3db0040000            cmp       eax,4B0h
  0045740f  7d49                  jge       short 0045745Ah
  00457411  8b4644                mov       eax,[esi+44h]
  00457414  33ed                  xor       ebp,ebp
  00457416  3bc5                  cmp       eax,ebp
  00457418  8b07                  mov       eax,[edi]
  0045741a  7518                  jne       short 00457434h
  0045741c  3d00802500            cmp       eax,258000h
  00457421  7c04                  jl        short 00457427h
  00457423  ff442414              inc       dword [esp+14h]

LAB_00457427:
  00457427  8b5620                mov       edx,[esi+20h]
  0045742a  81ea00800200          sub       edx,28000h
  00457430  3bc2                  cmp       eax,edx
  00457432  eb16                  jmp       short 0045744Ah

LAB_00457434:
  00457434  3d00c00d00            cmp       eax,0DC000h
  00457439  7c04                  jl        short 0045743Fh
  0045743b  ff442414              inc       dword [esp+14h]

LAB_0045743f:
  0045743f  8b4e20                mov       ecx,[esi+20h]
  00457442  81e900800200          sub       ecx,28000h
  00457448  3bc1                  cmp       eax,ecx

LAB_0045744a:
  0045744a  7d10                  jge       short 0045745Ch
  0045744c  66396e10              cmp       [esi+10h],bp
  00457450  750a                  jne       short 0045745Ch
  00457452  66c746106400          mov       word [esi+10h],64h
  00457458  eb02                  jmp       short 0045745Ch

LAB_0045745a:
  0045745a  33ed                  xor       ebp,ebp

LAB_0045745c:
  0045745c  81c79c000000          add       edi,9Ch
  00457462  4b                    dec       ebx
  00457463  0f8546ffffff          jne       near 004573AFh
  00457469  6800000040            push      40000000h
  0045746e  6a20                  push      20h
  00457470  56                    push      esi
  00457471  e88adb0d00            call      00535000h
  00457476  83c40c                add       esp,0Ch
  00457479  66396e14              cmp       [esi+14h],bp
  0045747d  8bd8                  mov       ebx,eax
  0045747f  7415                  je        short 00457496h
  00457481  663bdd                cmp       bx,bp
  00457484  750a                  jne       short 00457490h
  00457486  66396e10              cmp       [esi+10h],bp
  0045748a  7504                  jne       short 00457490h
  0045748c  66896e14              mov       [esi+14h],bp

LAB_00457490:
  00457490  66396e14              cmp       [esi+14h],bp
  00457494  7537                  jne       short 004574CDh

LAB_00457496:
  00457496  66396e10              cmp       [esi+10h],bp
  0045749a  7531                  jne       short 004574CDh
  0045749c  0fbf7c2414            movsx     edi,word [esp+14h]
  004574a1  e8caf80000            call      00466D70h
  004574a6  0fbfd0                movsx     edx,ax
  004574a9  0fbfc3                movsx     eax,bx
  004574ac  2bd7                  sub       edx,edi
  004574ae  3bc2                  cmp       eax,edx
  004574b0  751b                  jne       short 004574CDh
  004574b2  e8b9f80000            call      00466D70h
  004574b7  0fbfc8                movsx     ecx,ax
  004574ba  2bcf                  sub       ecx,edi
  004574bc  740f                  je        short 004574CDh
  004574be  bf01000000            mov       edi,1
  004574c3  66897e10              mov       [esi+10h],di
  004574c7  66897e14              mov       [esi+14h],di
  004574cb  eb05                  jmp       short 004574D2h

LAB_004574cd:
  004574cd  bf01000000            mov       edi,1

LAB_004574d2:
  004574d2  396e44                cmp       [esi+44h],ebp
  004574d5  753d                  jne       short 00457514h
  004574d7  66396e14              cmp       [esi+14h],bp
  004574db  7523                  jne       short 00457500h
  004574dd  66396e10              cmp       [esi+10h],bp
  004574e1  751d                  jne       short 00457500h
  004574e3  e888f80000            call      00466D70h
  004574e8  663bd8                cmp       bx,ax
  004574eb  7513                  jne       short 00457500h
  004574ed  817e2000802500        cmp       dword [esi+20h],258000h
  004574f4  750a                  jne       short 00457500h
  004574f6  66c746106400          mov       word [esi+10h],64h
  004574fc  66897e14              mov       [esi+14h],di

LAB_00457500:
  00457500  813d6ce05d0000802500  cmp       dword [5DE06Ch],258000h
  0045750a  7c47                  jl        short 00457553h
  0045750c  66396e10              cmp       [esi+10h],bp
  00457510  7541                  jne       short 00457553h
  00457512  eb3b                  jmp       short 0045754Fh

LAB_00457514:
  00457514  66396e14              cmp       [esi+14h],bp
  00457518  7523                  jne       short 0045753Dh
  0045751a  66396e10              cmp       [esi+10h],bp
  0045751e  751d                  jne       short 0045753Dh
  00457520  e84bf80000            call      00466D70h
  00457525  663bd8                cmp       bx,ax
  00457528  7513                  jne       short 0045753Dh
  0045752a  817e2000c00d00        cmp       dword [esi+20h],0DC000h
  00457531  750a                  jne       short 0045753Dh
  00457533  66c746106400          mov       word [esi+10h],64h
  00457539  66897e14              mov       [esi+14h],di

LAB_0045753d:
  0045753d  813d6ce05d0000c00d00  cmp       dword [5DE06Ch],0DC000h
  00457547  7c0a                  jl        short 00457553h
  00457549  66396e10              cmp       [esi+10h],bp
  0045754d  7504                  jne       short 00457553h

LAB_0045754f:
  0045754f  66897e10              mov       [esi+10h],di

LAB_00457553:
  00457553  66397e10              cmp       [esi+10h],di
  00457557  b802000000            mov       eax,2
  0045755c  750b                  jne       short 00457569h
  0045755e  66894610              mov       [esi+10h],ax
  00457562  66896e12              mov       [esi+12h],bp
  00457566  896e5c                mov       [esi+5Ch],ebp

LAB_00457569:
  00457569  66394610              cmp       [esi+10h],ax
  0045756d  0f85e1000000          jne       near 00457654h
  00457573  66ff8698000000        inc       word [esi+98h]
  0045757a  6683be9800000005      cmp       word [esi+98h],5
  00457582  7533                  jne       short 004575B7h
  00457584  6689ae98000000        mov       [esi+98h],bp
  0045758b  8b15cc975e00          mov       edx,[5E97CCh]
  00457591  55                    push      ebp
  00457592  55                    push      ebp
  00457593  8b82a8000000          mov       eax,[edx+0A8h]
  00457599  6800100000            push      1000h
  0045759e  6800040000            push      400h
  004575a3  8b08                  mov       ecx,[eax]
  004575a5  660fb65117            movzx     dx,byte [ecx+17h]
  004575aa  52                    push      edx
  004575ab  56                    push      esi
  004575ac  e8cf0a0100            call      00468080h
  004575b1  83c418                add       esp,18h
  004575b4  897e5c                mov       [esi+5Ch],edi

LAB_004575b7:
  004575b7  8b5620                mov       edx,[esi+20h]
  004575ba  8b4e44                mov       ecx,[esi+44h]
  004575bd  66ff4612              inc       word [esi+12h]
  004575c1  81c200700000          add       edx,7000h
  004575c7  3bcd                  cmp       ecx,ebp
  004575c9  895620                mov       [esi+20h],edx
  004575cc  8bc2                  mov       eax,edx
  004575ce  753c                  jne       short 0045760Ch
  004575d0  3d00802500            cmp       eax,258000h
  004575d5  7e7d                  jle       short 00457654h
  004575d7  8b465c                mov       eax,[esi+5Ch]
  004575da  c7462000802500        mov       dword [esi+20h],258000h
  004575e1  3bc7                  cmp       eax,edi
  004575e3  756b                  jne       short 00457650h
  004575e5  66396e10              cmp       [esi+10h],bp
  004575e9  7465                  je        short 00457650h
  004575eb  a1cc975e00            mov       eax,[5E97CCh]
  004575f0  55                    push      ebp
  004575f1  55                    push      ebp
  004575f2  6800100000            push      1000h
  004575f7  8b88a8000000          mov       ecx,[eax+0A8h]
  004575fd  6800020000            push      200h
  00457602  8b11                  mov       edx,[ecx]
  00457604  660fb64217            movzx     ax,byte [edx+17h]
  00457609  50                    push      eax
  0045760a  eb3b                  jmp       short 00457647h

LAB_0045760c:
  0045760c  3d00c00d00            cmp       eax,0DC000h
  00457611  7e41                  jle       short 00457654h
  00457613  8b465c                mov       eax,[esi+5Ch]
  00457616  c7462000c00d00        mov       dword [esi+20h],0DC000h
  0045761d  3bc7                  cmp       eax,edi
  0045761f  752f                  jne       short 00457650h
  00457621  66396e10              cmp       [esi+10h],bp
  00457625  7429                  je        short 00457650h
  00457627  8b0dcc975e00          mov       ecx,[5E97CCh]
  0045762d  55                    push      ebp
  0045762e  55                    push      ebp
  0045762f  6800100000            push      1000h
  00457634  8b91a8000000          mov       edx,[ecx+0A8h]
  0045763a  6800020000            push      200h
  0045763f  8b02                  mov       eax,[edx]
  00457641  660fb64817            movzx     cx,byte [eax+17h]
  00457646  51                    push      ecx

LAB_00457647:
  00457647  56                    push      esi
  00457648  e8330a0100            call      00468080h
  0045764d  83c418                add       esp,18h

LAB_00457650:
  00457650  66896e10              mov       [esi+10h],bp

LAB_00457654:
  00457654  66837e1064            cmp       word [esi+10h],64h
  00457659  b865000000            mov       eax,65h
  0045765e  750b                  jne       short 0045766Bh
  00457660  66894610              mov       [esi+10h],ax
  00457664  66896e12              mov       [esi+12h],bp
  00457668  896e5c                mov       [esi+5Ch],ebp

LAB_0045766b:
  0045766b  66394610              cmp       [esi+10h],ax
  0045766f  0f85f5000000          jne       near 0045776Ah
  00457675  66ff8698000000        inc       word [esi+98h]
  0045767c  6683be9800000005      cmp       word [esi+98h],5
  00457684  7533                  jne       short 004576B9h
  00457686  6689ae98000000        mov       [esi+98h],bp
  0045768d  8b15cc975e00          mov       edx,[5E97CCh]
  00457693  55                    push      ebp
  00457694  55                    push      ebp
  00457695  8b82a8000000          mov       eax,[edx+0A8h]
  0045769b  6800100000            push      1000h
  004576a0  6800040000            push      400h
  004576a5  8b08                  mov       ecx,[eax]
  004576a7  660fb65117            movzx     dx,byte [ecx+17h]
  004576ac  52                    push      edx
  004576ad  56                    push      esi
  004576ae  e8cd090100            call      00468080h
  004576b3  83c418                add       esp,18h
  004576b6  897e5c                mov       [esi+5Ch],edi

LAB_004576b9:
  004576b9  8b5620                mov       edx,[esi+20h]
  004576bc  8b4e44                mov       ecx,[esi+44h]
  004576bf  66ff4612              inc       word [esi+12h]
  004576c3  81c20090ffff          add       edx,0FFFF9000h
  004576c9  3bcd                  cmp       ecx,ebp
  004576cb  895620                mov       [esi+20h],edx
  004576ce  8bc2                  mov       eax,edx
  004576d0  7550                  jne       short 00457722h
  004576d2  3d00400100            cmp       eax,14000h
  004576d7  0f8d8d000000          jge       near 0045776Ah
  004576dd  8b465c                mov       eax,[esi+5Ch]
  004576e0  c7462000400100        mov       dword [esi+20h],14000h
  004576e7  3bc7                  cmp       eax,edi
  004576e9  757b                  jne       short 00457766h
  004576eb  66396e10              cmp       [esi+10h],bp
  004576ef  7475                  je        short 00457766h
  004576f1  a1cc975e00            mov       eax,[5E97CCh]
  004576f6  55                    push      ebp
  004576f7  55                    push      ebp
  004576f8  6800100000            push      1000h
  004576fd  8b88a8000000          mov       ecx,[eax+0A8h]
  00457703  6800020000            push      200h
  00457708  8b11                  mov       edx,[ecx]
  0045770a  660fb64217            movzx     ax,byte [edx+17h]
  0045770f  50                    push      eax
  00457710  56                    push      esi
  00457711  e86a090100            call      00468080h
  00457716  83c418                add       esp,18h
  00457719  66896e10              mov       [esi+10h],bp
  0045771d  5f                    pop       edi
  0045771e  5e                    pop       esi
  0045771f  5d                    pop       ebp
  00457720  5b                    pop       ebx
  00457721  c3                    ret

LAB_00457722:
  00457722  3d0000f6ff            cmp       eax,0FFF60000h
  00457727  7d41                  jge       short 0045776Ah
  00457729  8b465c                mov       eax,[esi+5Ch]
  0045772c  c746200000f6ff        mov       dword [esi+20h],0FFF60000h
  00457733  3bc7                  cmp       eax,edi
  00457735  752f                  jne       short 00457766h
  00457737  66396e10              cmp       [esi+10h],bp
  0045773b  7429                  je        short 00457766h
  0045773d  8b0dcc975e00          mov       ecx,[5E97CCh]
  00457743  55                    push      ebp
  00457744  55                    push      ebp
  00457745  6800100000            push      1000h
  0045774a  8b91a8000000          mov       edx,[ecx+0A8h]
  00457750  6800020000            push      200h
  00457755  8b02                  mov       eax,[edx]
  00457757  660fb64817            movzx     cx,byte [eax+17h]
  0045775c  51                    push      ecx
  0045775d  56                    push      esi
  0045775e  e81d090100            call      00468080h
  00457763  83c418                add       esp,18h

LAB_00457766:
  00457766  66896e10              mov       [esi+10h],bp

LAB_0045776a:
  0045776a  5f                    pop       edi
  0045776b  5e                    pop       esi
  0045776c  5d                    pop       ebp
  0045776d  5b                    pop       ebx
  0045776e  c3                    ret

29 cible(s) de saut interne : 0x00457391, 0x004573a1, 0x004573af, 0x00457427, 0x00457434, 0x0045743f, 0x0045744a, 0x0045745a, 0x0045745c, 0x00457490, 0x00457496, 0x004574cd, 0x004574d2, 0x00457500, 0x00457514, 0x0045753d, 0x0045754f, 0x00457553, 0x00457569, 0x004575b7, 0x0045760c, 0x00457647, 0x00457650, 0x00457654, 0x0045766b, 0x004576b9, 0x00457722, 0x00457766, 0x0045776a
