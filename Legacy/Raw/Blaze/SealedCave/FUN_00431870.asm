Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00431870  section .text  offset fichier 0x30c70  432 octets

  00431870  53                    push      ebx
  00431871  56                    push      esi
  00431872  8b74240c              mov       esi,[esp+0Ch]
  00431876  33db                  xor       ebx,ebx
  00431878  66395e10              cmp       [esi+10h],bx
  0043187c  0f8533010000          jne       near 004319B5h
  00431882  c7463cb00b5500        mov       dword [esi+3Ch],550BB0h
  00431889  e8b2581100            call      00547140h
  0043188e  99                    cdq
  0043188f  33c2                  xor       eax,edx
  00431891  2bc2                  sub       eax,edx
  00431893  25ff030000            and       eax,3FFh
  00431898  33c2                  xor       eax,edx
  0043189a  2bc2                  sub       eax,edx
  0043189c  05000c0000            add       eax,0C00h
  004318a1  66894628              mov       [esi+28h],ax
  004318a5  e896581100            call      00547140h
  004318aa  99                    cdq
  004318ab  33c2                  xor       eax,edx
  004318ad  2bc2                  sub       eax,edx
  004318af  25ff030000            and       eax,3FFh
  004318b4  33c2                  xor       eax,edx
  004318b6  2bc2                  sub       eax,edx
  004318b8  05000c0000            add       eax,0C00h
  004318bd  66837e1401            cmp       word [esi+14h],1
  004318c2  6689462a              mov       [esi+2Ah],ax
  004318c6  7506                  jne       short 004318CEh
  004318c8  c6460a08              mov       byte [esi+0Ah],8
  004318cc  eb04                  jmp       short 004318D2h

LAB_004318ce:
  004318ce  c6460a06              mov       byte [esi+0Ah],6

LAB_004318d2:
  004318d2  885e0b                mov       [esi+0Bh],bl
  004318d5  e866581100            call      00547140h
  004318da  99                    cdq
  004318db  b964000000            mov       ecx,64h
  004318e0  f7f9                  idiv      ecx
  004318e2  8b461c                mov       eax,[esi+1Ch]
  004318e5  83ea32                sub       edx,32h
  004318e8  c1e20c                shl       edx,0Ch
  004318eb  03c2                  add       eax,edx
  004318ed  89461c                mov       [esi+1Ch],eax
  004318f0  e84b581100            call      00547140h
  004318f5  99                    cdq
  004318f6  b932000000            mov       ecx,32h
  004318fb  f7f9                  idiv      ecx
  004318fd  8b4e20                mov       ecx,[esi+20h]
  00431900  83ea19                sub       edx,19h
  00431903  c1e20c                shl       edx,0Ch
  00431906  03ca                  add       ecx,edx
  00431908  894e20                mov       [esi+20h],ecx
  0043190b  e830581100            call      00547140h
  00431910  99                    cdq
  00431911  b964000000            mov       ecx,64h
  00431916  f7f9                  idiv      ecx
  00431918  8b4624                mov       eax,[esi+24h]
  0043191b  83ea32                sub       edx,32h
  0043191e  c1e20c                shl       edx,0Ch
  00431921  03c2                  add       eax,edx
  00431923  894624                mov       [esi+24h],eax
  00431926  e815581100            call      00547140h
  0043192b  99                    cdq
  0043192c  33c2                  xor       eax,edx
  0043192e  2bc2                  sub       eax,edx
  00431930  83e00f                and       eax,0Fh
  00431933  33c2                  xor       eax,edx
  00431935  2bc2                  sub       eax,edx
  00431937  83e808                sub       eax,8
  0043193a  c1e00c                shl       eax,0Ch
  0043193d  894650                mov       [esi+50h],eax
  00431940  e8fb571100            call      00547140h
  00431945  99                    cdq
  00431946  b90a000000            mov       ecx,0Ah
  0043194b  f7f9                  idiv      ecx
  0043194d  83c206                add       edx,6
  00431950  c1e20c                shl       edx,0Ch
  00431953  895654                mov       [esi+54h],edx
  00431956  e8e5571100            call      00547140h
  0043195b  99                    cdq
  0043195c  33c2                  xor       eax,edx
  0043195e  2bc2                  sub       eax,edx
  00431960  83e00f                and       eax,0Fh
  00431963  33c2                  xor       eax,edx
  00431965  2bc2                  sub       eax,edx
  00431967  83e808                sub       eax,8
  0043196a  c1e00c                shl       eax,0Ch
  0043196d  894658                mov       [esi+58h],eax
  00431970  e8cb571100            call      00547140h
  00431975  99                    cdq
  00431976  33c2                  xor       eax,edx
  00431978  2bc2                  sub       eax,edx
  0043197a  25ff070000            and       eax,7FFh
  0043197f  33c2                  xor       eax,edx
  00431981  2bc2                  sub       eax,edx
  00431983  0500080000            add       eax,800h
  00431988  66894628              mov       [esi+28h],ax
  0043198c  e8af571100            call      00547140h
  00431991  99                    cdq
  00431992  33c2                  xor       eax,edx
  00431994  66c746100100          mov       word [esi+10h],1
  0043199a  2bc2                  sub       eax,edx
  0043199c  66895e12              mov       [esi+12h],bx
  004319a0  25ff070000            and       eax,7FFh
  004319a5  885e40                mov       [esi+40h],bl
  004319a8  33c2                  xor       eax,edx
  004319aa  2bc2                  sub       eax,edx
  004319ac  0500080000            add       eax,800h
  004319b1  6689462a              mov       [esi+2Ah],ax

LAB_004319b5:
  004319b5  66837e1001            cmp       word [esi+10h],1
  004319ba  7553                  jne       short 00431A0Fh
  004319bc  8b5650                mov       edx,[esi+50h]
  004319bf  8b4e1c                mov       ecx,[esi+1Ch]
  004319c2  8b4620                mov       eax,[esi+20h]
  004319c5  03ca                  add       ecx,edx
  004319c7  8b5e24                mov       ebx,[esi+24h]
  004319ca  894e1c                mov       [esi+1Ch],ecx
  004319cd  8b4e54                mov       ecx,[esi+54h]
  004319d0  03c1                  add       eax,ecx
  004319d2  81c100f0ffff          add       ecx,0FFFFF000h
  004319d8  894620                mov       [esi+20h],eax
  004319db  8b4658                mov       eax,[esi+58h]
  004319de  03d8                  add       ebx,eax
  004319e0  66ff4612              inc       word [esi+12h]
  004319e4  668b4612              mov       ax,[esi+12h]
  004319e8  895e24                mov       [esi+24h],ebx
  004319eb  663d1800              cmp       ax,18h
  004319ef  894e54                mov       [esi+54h],ecx
  004319f2  7506                  jne       short 004319FAh
  004319f4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004319fa:
  004319fa  663d0600              cmp       ax,6
  004319fe  740c                  je        short 00431A0Ch
  00431a00  663d0c00              cmp       ax,0Ch
  00431a04  7406                  je        short 00431A0Ch
  00431a06  663d1200              cmp       ax,12h
  00431a0a  7503                  jne       short 00431A0Fh

LAB_00431a0c:
  00431a0c  fe4640                inc       byte [esi+40h]

LAB_00431a0f:
  00431a0f  5e                    pop       esi
  00431a10  5b                    pop       ebx
  00431a11  c3                    ret
  00431a12  90                    nop
  00431a13  90                    nop
  00431a14  90                    nop
  00431a15  90                    nop
  00431a16  90                    nop
  00431a17  90                    nop
  00431a18  90                    nop
  00431a19  90                    nop
  00431a1a  90                    nop
  00431a1b  90                    nop
  00431a1c  90                    nop
  00431a1d  90                    nop
  00431a1e  90                    nop
  00431a1f  90                    nop

6 cible(s) de saut interne : 0x004318ce, 0x004318d2, 0x004319b5, 0x004319fa, 0x00431a0c, 0x00431a0f
