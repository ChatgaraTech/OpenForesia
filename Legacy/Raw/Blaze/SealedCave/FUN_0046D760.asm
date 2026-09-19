Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046d760  section .text  offset fichier 0x6cb60  256 octets

  0046d760  53                    push      ebx
  0046d761  56                    push      esi
  0046d762  8b74240c              mov       esi,[esp+0Ch]
  0046d766  bb01000000            mov       ebx,1
  0046d76b  66837e1000            cmp       word [esi+10h],0
  0046d770  7517                  jne       short 0046D789h
  0046d772  8b4620                mov       eax,[esi+20h]
  0046d775  66895e10              mov       [esi+10h],bx
  0046d779  c1f80c                sar       eax,0Ch
  0046d77c  66894612              mov       [esi+12h],ax
  0046d780  66c786980000000000    mov       word [esi+98h],0

LAB_0046d789:
  0046d789  66837e1002            cmp       word [esi+10h],2
  0046d78e  757a                  jne       short 0046D80Ah
  0046d790  6683be9800000000      cmp       word [esi+98h],0
  0046d798  7532                  jne       short 0046D7CCh
  0046d79a  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046d7a0  6a00                  push      0
  0046d7a2  6a00                  push      0
  0046d7a4  6800200000            push      2000h
  0046d7a9  8b91a8000000          mov       edx,[ecx+0A8h]
  0046d7af  6800020000            push      200h
  0046d7b4  8b02                  mov       eax,[edx]
  0046d7b6  660fb64825            movzx     cx,byte [eax+25h]
  0046d7bb  51                    push      ecx
  0046d7bc  56                    push      esi
  0046d7bd  e8bea8ffff            call      00468080h
  0046d7c2  83c418                add       esp,18h
  0046d7c5  66899e98000000        mov       [esi+98h],bx

LAB_0046d7cc:
  0046d7cc  0fbf5612              movsx     edx,word [esi+12h]
  0046d7d0  0fbf4614              movsx     eax,word [esi+14h]
  0046d7d4  2bd0                  sub       edx,eax
  0046d7d6  668b4616              mov       ax,[esi+16h]
  0046d7da  c1e20c                shl       edx,0Ch
  0046d7dd  6685c0                test      ax,ax
  0046d7e0  895620                mov       [esi+20h],edx
  0046d7e3  66895e10              mov       [esi+10h],bx
  0046d7e7  7421                  je        short 0046D80Ah
  0046d7e9  0fbfc8                movsx     ecx,ax
  0046d7ec  8bc1                  mov       eax,ecx
  0046d7ee  99                    cdq
  0046d7ef  83e207                and       edx,7
  0046d7f2  03c2                  add       eax,edx
  0046d7f4  c1f803                sar       eax,3
  0046d7f7  8d14c500000000        lea       edx,[eax*8]
  0046d7fe  2bca                  sub       ecx,edx
  0046d800  8ad3                  mov       dl,bl
  0046d802  d2e2                  shl       dl,cl
  0046d804  0890e0965e00          or        [eax+5E96E0h],dl

LAB_0046d80a:
  0046d80a  66837e1003            cmp       word [esi+10h],3
  0046d80f  7517                  jne       short 0046D828h
  0046d811  0fbf4612              movsx     eax,word [esi+12h]
  0046d815  c1e00c                shl       eax,0Ch
  0046d818  66c786980000000000    mov       word [esi+98h],0
  0046d821  894620                mov       [esi+20h],eax
  0046d824  66895e10              mov       [esi+10h],bx

LAB_0046d828:
  0046d828  6800000040            push      40000000h
  0046d82d  6a10                  push      10h
  0046d82f  56                    push      esi
  0046d830  e8cb770c00            call      00535000h
  0046d835  83c40c                add       esp,0Ch
  0046d838  85c0                  test      eax,eax
  0046d83a  7410                  je        short 0046D84Ch
  0046d83c  66395e10              cmp       [esi+10h],bx
  0046d840  7506                  jne       short 0046D848h
  0046d842  66c746100200          mov       word [esi+10h],2

LAB_0046d848:
  0046d848  85c0                  test      eax,eax
  0046d84a  750c                  jne       short 0046D858h

LAB_0046d84c:
  0046d84c  66395e10              cmp       [esi+10h],bx
  0046d850  7506                  jne       short 0046D858h
  0046d852  66c746100300          mov       word [esi+10h],3

LAB_0046d858:
  0046d858  5e                    pop       esi
  0046d859  5b                    pop       ebx
  0046d85a  c3                    ret
  0046d85b  90                    nop
  0046d85c  90                    nop
  0046d85d  90                    nop
  0046d85e  90                    nop
  0046d85f  90                    nop

7 cible(s) de saut interne : 0x0046d789, 0x0046d7cc, 0x0046d80a, 0x0046d828, 0x0046d848, 0x0046d84c, 0x0046d858
