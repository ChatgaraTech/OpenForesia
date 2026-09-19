Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00468850  section .text  offset fichier 0x67c50  256 octets

  00468850  53                    push      ebx
  00468851  56                    push      esi
  00468852  8b74240c              mov       esi,[esp+0Ch]
  00468856  bb01000000            mov       ebx,1
  0046885b  66837e1000            cmp       word [esi+10h],0
  00468860  7517                  jne       short 00468879h
  00468862  8b4620                mov       eax,[esi+20h]
  00468865  66895e10              mov       [esi+10h],bx
  00468869  c1f80c                sar       eax,0Ch
  0046886c  66894612              mov       [esi+12h],ax
  00468870  66c786980000000000    mov       word [esi+98h],0

LAB_00468879:
  00468879  66837e1002            cmp       word [esi+10h],2
  0046887e  757a                  jne       short 004688FAh
  00468880  6683be9800000000      cmp       word [esi+98h],0
  00468888  7532                  jne       short 004688BCh
  0046888a  8b0dcc975e00          mov       ecx,[5E97CCh]
  00468890  6a00                  push      0
  00468892  6a00                  push      0
  00468894  6800200000            push      2000h
  00468899  8b91a8000000          mov       edx,[ecx+0A8h]
  0046889f  6800020000            push      200h
  004688a4  8b02                  mov       eax,[edx]
  004688a6  660fb64825            movzx     cx,byte [eax+25h]
  004688ab  51                    push      ecx
  004688ac  56                    push      esi
  004688ad  e8cef7ffff            call      00468080h
  004688b2  83c418                add       esp,18h
  004688b5  66899e98000000        mov       [esi+98h],bx

LAB_004688bc:
  004688bc  0fbf5612              movsx     edx,word [esi+12h]
  004688c0  0fbf4614              movsx     eax,word [esi+14h]
  004688c4  2bd0                  sub       edx,eax
  004688c6  668b4616              mov       ax,[esi+16h]
  004688ca  c1e20c                shl       edx,0Ch
  004688cd  6685c0                test      ax,ax
  004688d0  895620                mov       [esi+20h],edx
  004688d3  66895e10              mov       [esi+10h],bx
  004688d7  7421                  je        short 004688FAh
  004688d9  0fbfc8                movsx     ecx,ax
  004688dc  8bc1                  mov       eax,ecx
  004688de  99                    cdq
  004688df  83e207                and       edx,7
  004688e2  03c2                  add       eax,edx
  004688e4  c1f803                sar       eax,3
  004688e7  8d14c500000000        lea       edx,[eax*8]
  004688ee  2bca                  sub       ecx,edx
  004688f0  8ad3                  mov       dl,bl
  004688f2  d2e2                  shl       dl,cl
  004688f4  0890e0965e00          or        [eax+5E96E0h],dl

LAB_004688fa:
  004688fa  66837e1003            cmp       word [esi+10h],3
  004688ff  7517                  jne       short 00468918h
  00468901  0fbf4612              movsx     eax,word [esi+12h]
  00468905  c1e00c                shl       eax,0Ch
  00468908  66c786980000000000    mov       word [esi+98h],0
  00468911  894620                mov       [esi+20h],eax
  00468914  66895e10              mov       [esi+10h],bx

LAB_00468918:
  00468918  6800000040            push      40000000h
  0046891d  6a10                  push      10h
  0046891f  56                    push      esi
  00468920  e8dbc60c00            call      00535000h
  00468925  83c40c                add       esp,0Ch
  00468928  85c0                  test      eax,eax
  0046892a  7410                  je        short 0046893Ch
  0046892c  66395e10              cmp       [esi+10h],bx
  00468930  7506                  jne       short 00468938h
  00468932  66c746100200          mov       word [esi+10h],2

LAB_00468938:
  00468938  85c0                  test      eax,eax
  0046893a  750c                  jne       short 00468948h

LAB_0046893c:
  0046893c  66395e10              cmp       [esi+10h],bx
  00468940  7506                  jne       short 00468948h
  00468942  66c746100300          mov       word [esi+10h],3

LAB_00468948:
  00468948  5e                    pop       esi
  00468949  5b                    pop       ebx
  0046894a  c3                    ret
  0046894b  90                    nop
  0046894c  90                    nop
  0046894d  90                    nop
  0046894e  90                    nop
  0046894f  90                    nop

7 cible(s) de saut interne : 0x00468879, 0x004688bc, 0x004688fa, 0x00468918, 0x00468938, 0x0046893c, 0x00468948
