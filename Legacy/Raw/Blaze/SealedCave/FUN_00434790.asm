Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00434790  section .text  offset fichier 0x33b90  432 octets

  00434790  53                    push      ebx
  00434791  56                    push      esi
  00434792  8b74240c              mov       esi,[esp+0Ch]
  00434796  33db                  xor       ebx,ebx
  00434798  66395e10              cmp       [esi+10h],bx
  0043479c  0f8533010000          jne       near 004348D5h
  004347a2  c7463cb00d5500        mov       dword [esi+3Ch],550DB0h
  004347a9  e892291100            call      00547140h
  004347ae  99                    cdq
  004347af  33c2                  xor       eax,edx
  004347b1  2bc2                  sub       eax,edx
  004347b3  25ff030000            and       eax,3FFh
  004347b8  33c2                  xor       eax,edx
  004347ba  2bc2                  sub       eax,edx
  004347bc  05000c0000            add       eax,0C00h
  004347c1  66894628              mov       [esi+28h],ax
  004347c5  e876291100            call      00547140h
  004347ca  99                    cdq
  004347cb  33c2                  xor       eax,edx
  004347cd  2bc2                  sub       eax,edx
  004347cf  25ff030000            and       eax,3FFh
  004347d4  33c2                  xor       eax,edx
  004347d6  2bc2                  sub       eax,edx
  004347d8  05000c0000            add       eax,0C00h
  004347dd  66837e1401            cmp       word [esi+14h],1
  004347e2  6689462a              mov       [esi+2Ah],ax
  004347e6  7506                  jne       short 004347EEh
  004347e8  c6460a08              mov       byte [esi+0Ah],8
  004347ec  eb04                  jmp       short 004347F2h

LAB_004347ee:
  004347ee  c6460a06              mov       byte [esi+0Ah],6

LAB_004347f2:
  004347f2  885e0b                mov       [esi+0Bh],bl
  004347f5  e846291100            call      00547140h
  004347fa  99                    cdq
  004347fb  b964000000            mov       ecx,64h
  00434800  f7f9                  idiv      ecx
  00434802  8b461c                mov       eax,[esi+1Ch]
  00434805  83ea32                sub       edx,32h
  00434808  c1e20c                shl       edx,0Ch
  0043480b  03c2                  add       eax,edx
  0043480d  89461c                mov       [esi+1Ch],eax
  00434810  e82b291100            call      00547140h
  00434815  99                    cdq
  00434816  b932000000            mov       ecx,32h
  0043481b  f7f9                  idiv      ecx
  0043481d  8b4e20                mov       ecx,[esi+20h]
  00434820  83ea19                sub       edx,19h
  00434823  c1e20c                shl       edx,0Ch
  00434826  03ca                  add       ecx,edx
  00434828  894e20                mov       [esi+20h],ecx
  0043482b  e810291100            call      00547140h
  00434830  99                    cdq
  00434831  b964000000            mov       ecx,64h
  00434836  f7f9                  idiv      ecx
  00434838  8b4624                mov       eax,[esi+24h]
  0043483b  83ea32                sub       edx,32h
  0043483e  c1e20c                shl       edx,0Ch
  00434841  03c2                  add       eax,edx
  00434843  894624                mov       [esi+24h],eax
  00434846  e8f5281100            call      00547140h
  0043484b  99                    cdq
  0043484c  33c2                  xor       eax,edx
  0043484e  2bc2                  sub       eax,edx
  00434850  83e00f                and       eax,0Fh
  00434853  33c2                  xor       eax,edx
  00434855  2bc2                  sub       eax,edx
  00434857  83e808                sub       eax,8
  0043485a  c1e00c                shl       eax,0Ch
  0043485d  894650                mov       [esi+50h],eax
  00434860  e8db281100            call      00547140h
  00434865  99                    cdq
  00434866  b90a000000            mov       ecx,0Ah
  0043486b  f7f9                  idiv      ecx
  0043486d  83c206                add       edx,6
  00434870  c1e20c                shl       edx,0Ch
  00434873  895654                mov       [esi+54h],edx
  00434876  e8c5281100            call      00547140h
  0043487b  99                    cdq
  0043487c  33c2                  xor       eax,edx
  0043487e  2bc2                  sub       eax,edx
  00434880  83e00f                and       eax,0Fh
  00434883  33c2                  xor       eax,edx
  00434885  2bc2                  sub       eax,edx
  00434887  83e808                sub       eax,8
  0043488a  c1e00c                shl       eax,0Ch
  0043488d  894658                mov       [esi+58h],eax
  00434890  e8ab281100            call      00547140h
  00434895  99                    cdq
  00434896  33c2                  xor       eax,edx
  00434898  2bc2                  sub       eax,edx
  0043489a  25ff070000            and       eax,7FFh
  0043489f  33c2                  xor       eax,edx
  004348a1  2bc2                  sub       eax,edx
  004348a3  0500080000            add       eax,800h
  004348a8  66894628              mov       [esi+28h],ax
  004348ac  e88f281100            call      00547140h
  004348b1  99                    cdq
  004348b2  33c2                  xor       eax,edx
  004348b4  66c746100100          mov       word [esi+10h],1
  004348ba  2bc2                  sub       eax,edx
  004348bc  66895e12              mov       [esi+12h],bx
  004348c0  25ff070000            and       eax,7FFh
  004348c5  885e40                mov       [esi+40h],bl
  004348c8  33c2                  xor       eax,edx
  004348ca  2bc2                  sub       eax,edx
  004348cc  0500080000            add       eax,800h
  004348d1  6689462a              mov       [esi+2Ah],ax

LAB_004348d5:
  004348d5  66837e1001            cmp       word [esi+10h],1
  004348da  7553                  jne       short 0043492Fh
  004348dc  8b5650                mov       edx,[esi+50h]
  004348df  8b4e1c                mov       ecx,[esi+1Ch]
  004348e2  8b4620                mov       eax,[esi+20h]
  004348e5  03ca                  add       ecx,edx
  004348e7  8b5e24                mov       ebx,[esi+24h]
  004348ea  894e1c                mov       [esi+1Ch],ecx
  004348ed  8b4e54                mov       ecx,[esi+54h]
  004348f0  03c1                  add       eax,ecx
  004348f2  81c100f0ffff          add       ecx,0FFFFF000h
  004348f8  894620                mov       [esi+20h],eax
  004348fb  8b4658                mov       eax,[esi+58h]
  004348fe  03d8                  add       ebx,eax
  00434900  66ff4612              inc       word [esi+12h]
  00434904  668b4612              mov       ax,[esi+12h]
  00434908  895e24                mov       [esi+24h],ebx
  0043490b  663d1800              cmp       ax,18h
  0043490f  894e54                mov       [esi+54h],ecx
  00434912  7506                  jne       short 0043491Ah
  00434914  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043491a:
  0043491a  663d0600              cmp       ax,6
  0043491e  740c                  je        short 0043492Ch
  00434920  663d0c00              cmp       ax,0Ch
  00434924  7406                  je        short 0043492Ch
  00434926  663d1200              cmp       ax,12h
  0043492a  7503                  jne       short 0043492Fh

LAB_0043492c:
  0043492c  fe4640                inc       byte [esi+40h]

LAB_0043492f:
  0043492f  5e                    pop       esi
  00434930  5b                    pop       ebx
  00434931  c3                    ret
  00434932  90                    nop
  00434933  90                    nop
  00434934  90                    nop
  00434935  90                    nop
  00434936  90                    nop
  00434937  90                    nop
  00434938  90                    nop
  00434939  90                    nop
  0043493a  90                    nop
  0043493b  90                    nop
  0043493c  90                    nop
  0043493d  90                    nop
  0043493e  90                    nop
  0043493f  90                    nop

6 cible(s) de saut interne : 0x004347ee, 0x004347f2, 0x004348d5, 0x0043491a, 0x0043492c, 0x0043492f
