Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046f9d0  section .text  offset fichier 0x6edd0  256 octets

  0046f9d0  53                    push      ebx
  0046f9d1  56                    push      esi
  0046f9d2  8b74240c              mov       esi,[esp+0Ch]
  0046f9d6  bb01000000            mov       ebx,1
  0046f9db  66837e1000            cmp       word [esi+10h],0
  0046f9e0  7517                  jne       short 0046F9F9h
  0046f9e2  8b4620                mov       eax,[esi+20h]
  0046f9e5  66895e10              mov       [esi+10h],bx
  0046f9e9  c1f80c                sar       eax,0Ch
  0046f9ec  66894612              mov       [esi+12h],ax
  0046f9f0  66c786980000000000    mov       word [esi+98h],0

LAB_0046f9f9:
  0046f9f9  6800000040            push      40000000h
  0046f9fe  6a10                  push      10h
  0046fa00  56                    push      esi
  0046fa01  e8fa550c00            call      00535000h
  0046fa06  83c40c                add       esp,0Ch
  0046fa09  b902000000            mov       ecx,2
  0046fa0e  85c0                  test      eax,eax
  0046fa10  740e                  je        short 0046FA20h
  0046fa12  66395e10              cmp       [esi+10h],bx
  0046fa16  7504                  jne       short 0046FA1Ch
  0046fa18  66894e10              mov       [esi+10h],cx

LAB_0046fa1c:
  0046fa1c  85c0                  test      eax,eax
  0046fa1e  750c                  jne       short 0046FA2Ch

LAB_0046fa20:
  0046fa20  66395e10              cmp       [esi+10h],bx
  0046fa24  7506                  jne       short 0046FA2Ch
  0046fa26  66c746100300          mov       word [esi+10h],3

LAB_0046fa2c:
  0046fa2c  66394e10              cmp       [esi+10h],cx
  0046fa30  7577                  jne       short 0046FAA9h
  0046fa32  6683be9800000000      cmp       word [esi+98h],0
  0046fa3a  7532                  jne       short 0046FA6Eh
  0046fa3c  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046fa42  6a00                  push      0
  0046fa44  6a00                  push      0
  0046fa46  6800200000            push      2000h
  0046fa4b  8b91a8000000          mov       edx,[ecx+0A8h]
  0046fa51  6800020000            push      200h
  0046fa56  8b02                  mov       eax,[edx]
  0046fa58  660fb64825            movzx     cx,byte [eax+25h]
  0046fa5d  51                    push      ecx
  0046fa5e  56                    push      esi
  0046fa5f  e81c86ffff            call      00468080h
  0046fa64  83c418                add       esp,18h
  0046fa67  66899e98000000        mov       [esi+98h],bx

LAB_0046fa6e:
  0046fa6e  0fbf5614              movsx     edx,word [esi+14h]
  0046fa72  0fbf4612              movsx     eax,word [esi+12h]
  0046fa76  0fbf4e16              movsx     ecx,word [esi+16h]
  0046fa7a  2bc2                  sub       eax,edx
  0046fa7c  66895e10              mov       [esi+10h],bx
  0046fa80  c1e00c                shl       eax,0Ch
  0046fa83  894620                mov       [esi+20h],eax
  0046fa86  8bc1                  mov       eax,ecx
  0046fa88  99                    cdq
  0046fa89  83e207                and       edx,7
  0046fa8c  03c2                  add       eax,edx
  0046fa8e  c1f803                sar       eax,3
  0046fa91  83f81f                cmp       eax,1Fh
  0046fa94  7f13                  jg        short 0046FAA9h
  0046fa96  8d14c500000000        lea       edx,[eax*8]
  0046fa9d  2bca                  sub       ecx,edx
  0046fa9f  8ad3                  mov       dl,bl
  0046faa1  d2e2                  shl       dl,cl
  0046faa3  0890e0965e00          or        [eax+5E96E0h],dl

LAB_0046faa9:
  0046faa9  66837e1003            cmp       word [esi+10h],3
  0046faae  7517                  jne       short 0046FAC7h
  0046fab0  0fbf4612              movsx     eax,word [esi+12h]
  0046fab4  c1e00c                shl       eax,0Ch
  0046fab7  66c786980000000000    mov       word [esi+98h],0
  0046fac0  894620                mov       [esi+20h],eax
  0046fac3  66895e10              mov       [esi+10h],bx

LAB_0046fac7:
  0046fac7  5e                    pop       esi
  0046fac8  5b                    pop       ebx
  0046fac9  c3                    ret
  0046faca  90                    nop
  0046facb  90                    nop
  0046facc  90                    nop
  0046facd  90                    nop
  0046face  90                    nop
  0046facf  90                    nop

7 cible(s) de saut interne : 0x0046f9f9, 0x0046fa1c, 0x0046fa20, 0x0046fa2c, 0x0046fa6e, 0x0046faa9, 0x0046fac7
