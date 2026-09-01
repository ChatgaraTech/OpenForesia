Legacy/Binaries/Blaze.exe  base 0x00400000
0x00446410  section .text  offset fichier 0x45810  515 octets

  00446410  53                    push      ebx
  00446411  56                    push      esi
  00446412  8b74240c              mov       esi,[esp+0Ch]
  00446416  33db                  xor       ebx,ebx
  00446418  3bf3                  cmp       esi,ebx
  0044641a  0f84f0010000          je        near 00446610h
  00446420  8b466c                mov       eax,[esi+6Ch]
  00446423  66395e10              cmp       [esi+10h],bx
  00446427  57                    push      edi
  00446428  894678                mov       [esi+78h],eax
  0044642b  bf00100000            mov       edi,1000h
  00446430  0f85e5000000          jne       near 0044651Bh
  00446436  6a02                  push      2
  00446438  53                    push      ebx
  00446439  b8581b0000            mov       eax,1B58h
  0044643e  68a7000000            push      0A7h
  00446443  56                    push      esi
  00446444  c6460a02              mov       byte [esi+0Ah],2
  00446448  885e0b                mov       [esi+0Bh],bl
  0044644b  66897e28              mov       [esi+28h],di
  0044644f  66897e2a              mov       [esi+2Ah],di
  00446453  66897e2c              mov       [esi+2Ch],di
  00446457  894644                mov       [esi+44h],eax
  0044645a  c7464870170000        mov       dword [esi+48h],1770h
  00446461  89464c                mov       [esi+4Ch],eax
  00446464  66895e30              mov       [esi+30h],bx
  00446468  66895e32              mov       [esi+32h],bx
  0044646c  66895e34              mov       [esi+34h],bx
  00446470  e86b460300            call      0047AAE0h
  00446475  33c9                  xor       ecx,ecx
  00446477  83c410                add       esp,10h
  0044647a  8a4e0a                mov       cl,[esi+0Ah]
  0044647d  33d2                  xor       edx,edx
  0044647f  8a560b                mov       dl,[esi+0Bh]
  00446482  51                    push      ecx
  00446483  52                    push      edx
  00446484  68a7000000            push      0A7h
  00446489  56                    push      esi
  0044648a  66895e30              mov       [esi+30h],bx
  0044648e  66895e32              mov       [esi+32h],bx
  00446492  66c746340008          mov       word [esi+34h],800h
  00446498  e843460300            call      0047AAE0h
  0044649d  b8b80b0000            mov       eax,0BB8h
  004464a2  83c410                add       esp,10h
  004464a5  66894628              mov       [esi+28h],ax
  004464a9  6689462c              mov       [esi+2Ch],ax
  004464ad  894648                mov       [esi+48h],eax
  004464b0  33c0                  xor       eax,eax
  004464b2  8a460a                mov       al,[esi+0Ah]
  004464b5  33c9                  xor       ecx,ecx
  004464b7  8a4e0b                mov       cl,[esi+0Bh]
  004464ba  50                    push      eax
  004464bb  51                    push      ecx
  004464bc  68a7000000            push      0A7h
  004464c1  56                    push      esi
  004464c2  66897e2a              mov       [esi+2Ah],di
  004464c6  895e44                mov       [esi+44h],ebx
  004464c9  895e4c                mov       [esi+4Ch],ebx
  004464cc  66895e30              mov       [esi+30h],bx
  004464d0  66895e32              mov       [esi+32h],bx
  004464d4  66895e34              mov       [esi+34h],bx
  004464d8  e803460300            call      0047AAE0h
  004464dd  33d2                  xor       edx,edx
  004464df  83c410                add       esp,10h
  004464e2  8a560a                mov       dl,[esi+0Ah]
  004464e5  33c0                  xor       eax,eax
  004464e7  8a460b                mov       al,[esi+0Bh]
  004464ea  52                    push      edx
  004464eb  50                    push      eax
  004464ec  68a7000000            push      0A7h
  004464f1  56                    push      esi
  004464f2  66895e30              mov       [esi+30h],bx
  004464f6  66895e32              mov       [esi+32h],bx
  004464fa  66c746340008          mov       word [esi+34h],800h
  00446500  e8db450300            call      0047AAE0h
  00446505  83c410                add       esp,10h
  00446508  66c746100100          mov       word [esi+10h],1
  0044650e  66c746161400          mov       word [esi+16h],14h
  00446514  66899e98000000        mov       [esi+98h],bx

LAB_0044651b:
  0044651b  66837e1001            cmp       word [esi+10h],1
  00446520  0f8589000000          jne       near 004465AFh
  00446526  8b4e78                mov       ecx,[esi+78h]
  00446529  c646043f              mov       byte [esi+4],3Fh
  0044652d  c6460a02              mov       byte [esi+0Ah],2
  00446531  885e0b                mov       [esi+0Bh],bl
  00446534  894e70                mov       [esi+70h],ecx
  00446537  895e44                mov       [esi+44h],ebx
  0044653a  c7464800000500        mov       dword [esi+48h],50000h
  00446541  895e4c                mov       [esi+4Ch],ebx
  00446544  e8f70b1000            call      00547140h
  00446549  99                    cdq
  0044654a  33c2                  xor       eax,edx
  0044654c  2bc2                  sub       eax,edx
  0044654e  25ff030000            and       eax,3FFh
  00446553  33c2                  xor       eax,edx
  00446555  2bc2                  sub       eax,edx
  00446557  0500040000            add       eax,400h
  0044655c  66894632              mov       [esi+32h],ax
  00446560  e8db0b1000            call      00547140h
  00446565  99                    cdq
  00446566  33c2                  xor       eax,edx
  00446568  6aff                  push      0FFFFFFFFh
  0044656a  2bc2                  sub       eax,edx
  0044656c  53                    push      ebx
  0044656d  25ff0f0000            and       eax,0FFFh
  00446572  6a70                  push      70h
  00446574  33c2                  xor       eax,edx
  00446576  56                    push      esi
  00446577  2bc2                  sub       eax,edx
  00446579  66895e14              mov       [esi+14h],bx
  0044657d  66894634              mov       [esi+34h],ax
  00446581  66c74612c800          mov       word [esi+12h],0C8h
  00446587  66c74628ff1f          mov       word [esi+28h],1FFFh
  0044658d  66895e2a              mov       [esi+2Ah],bx
  00446591  66c7462c0001          mov       word [esi+2Ch],100h
  00446597  e844450300            call      0047AAE0h
  0044659c  83c410                add       esp,10h
  0044659f  66ff4e16              dec       word [esi+16h]
  004465a3  66395e16              cmp       [esi+16h],bx
  004465a7  7d06                  jge       short 004465AFh
  004465a9  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004465af:
  004465af  66399e98000000        cmp       [esi+98h],bx
  004465b6  7514                  jne       short 004465CCh
  004465b8  8d561c                lea       edx,[esi+1Ch]
  004465bb  52                    push      edx
  004465bc  57                    push      edi
  004465bd  6800010000            push      100h
  004465c2  6a1e                  push      1Eh
  004465c4  e857750f00            call      0053DB20h
  004465c9  83c410                add       esp,10h

LAB_004465cc:
  004465cc  6683be9800000001      cmp       word [esi+98h],1
  004465d4  7514                  jne       short 004465EAh
  004465d6  8d461c                lea       eax,[esi+1Ch]
  004465d9  50                    push      eax
  004465da  57                    push      edi
  004465db  6800040000            push      400h
  004465e0  6a1f                  push      1Fh
  004465e2  e839750f00            call      0053DB20h
  004465e7  83c410                add       esp,10h

LAB_004465ea:
  004465ea  6683be9800000002      cmp       word [esi+98h],2
  004465f2  7514                  jne       short 00446608h
  004465f4  8d4e1c                lea       ecx,[esi+1Ch]
  004465f7  51                    push      ecx
  004465f8  57                    push      edi
  004465f9  68a9050000            push      5A9h
  004465fe  6a23                  push      23h
  00446600  e81b750f00            call      0053DB20h
  00446605  83c410                add       esp,10h

LAB_00446608:
  00446608  66ff8698000000        inc       word [esi+98h]
  0044660f  5f                    pop       edi

LAB_00446610:
  00446610  5e                    pop       esi
  00446611  5b                    pop       ebx
  00446612  c3                    ret

6 cible(s) de saut interne : 0x0044651b, 0x004465af, 0x004465cc, 0x004465ea, 0x00446608, 0x00446610
