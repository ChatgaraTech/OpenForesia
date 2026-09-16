
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00406c40  section .text  offset fichier 0x6040  432 octets

  00406c40  53                    push      ebx
  00406c41  56                    push      esi
  00406c42  8b74240c              mov       esi,[esp+0Ch]
  00406c46  33db                  xor       ebx,ebx
  00406c48  66395e10              cmp       [esi+10h],bx
  00406c4c  0f8533010000          jne       near 00406D85h
  00406c52  c7463c50f25400        mov       dword [esi+3Ch],54F250h
  00406c59  e8e2041400            call      00547140h
  00406c5e  99                    cdq
  00406c5f  33c2                  xor       eax,edx
  00406c61  2bc2                  sub       eax,edx
  00406c63  25ff030000            and       eax,3FFh
  00406c68  33c2                  xor       eax,edx
  00406c6a  2bc2                  sub       eax,edx
  00406c6c  05000c0000            add       eax,0C00h
  00406c71  66894628              mov       [esi+28h],ax
  00406c75  e8c6041400            call      00547140h
  00406c7a  99                    cdq
  00406c7b  33c2                  xor       eax,edx
  00406c7d  2bc2                  sub       eax,edx
  00406c7f  25ff030000            and       eax,3FFh
  00406c84  33c2                  xor       eax,edx
  00406c86  2bc2                  sub       eax,edx
  00406c88  05000c0000            add       eax,0C00h
  00406c8d  66837e1401            cmp       word [esi+14h],1
  00406c92  6689462a              mov       [esi+2Ah],ax
  00406c96  7506                  jne       short 00406C9Eh
  00406c98  c6460a08              mov       byte [esi+0Ah],8
  00406c9c  eb04                  jmp       short 00406CA2h

LAB_00406c9e:
  00406c9e  c6460a06              mov       byte [esi+0Ah],6

LAB_00406ca2:
  00406ca2  885e0b                mov       [esi+0Bh],bl
  00406ca5  e896041400            call      00547140h
  00406caa  99                    cdq
  00406cab  b964000000            mov       ecx,64h
  00406cb0  f7f9                  idiv      ecx
  00406cb2  8b461c                mov       eax,[esi+1Ch]
  00406cb5  83ea32                sub       edx,32h
  00406cb8  c1e20c                shl       edx,0Ch
  00406cbb  03c2                  add       eax,edx
  00406cbd  89461c                mov       [esi+1Ch],eax
  00406cc0  e87b041400            call      00547140h
  00406cc5  99                    cdq
  00406cc6  b932000000            mov       ecx,32h
  00406ccb  f7f9                  idiv      ecx
  00406ccd  8b4e20                mov       ecx,[esi+20h]
  00406cd0  83ea19                sub       edx,19h
  00406cd3  c1e20c                shl       edx,0Ch
  00406cd6  03ca                  add       ecx,edx
  00406cd8  894e20                mov       [esi+20h],ecx
  00406cdb  e860041400            call      00547140h
  00406ce0  99                    cdq
  00406ce1  b964000000            mov       ecx,64h
  00406ce6  f7f9                  idiv      ecx
  00406ce8  8b4624                mov       eax,[esi+24h]
  00406ceb  83ea32                sub       edx,32h
  00406cee  c1e20c                shl       edx,0Ch
  00406cf1  03c2                  add       eax,edx
  00406cf3  894624                mov       [esi+24h],eax
  00406cf6  e845041400            call      00547140h
  00406cfb  99                    cdq
  00406cfc  33c2                  xor       eax,edx
  00406cfe  2bc2                  sub       eax,edx
  00406d00  83e00f                and       eax,0Fh
  00406d03  33c2                  xor       eax,edx
  00406d05  2bc2                  sub       eax,edx
  00406d07  83e808                sub       eax,8
  00406d0a  c1e00c                shl       eax,0Ch
  00406d0d  894650                mov       [esi+50h],eax
  00406d10  e82b041400            call      00547140h
  00406d15  99                    cdq
  00406d16  b90a000000            mov       ecx,0Ah
  00406d1b  f7f9                  idiv      ecx
  00406d1d  83c206                add       edx,6
  00406d20  c1e20c                shl       edx,0Ch
  00406d23  895654                mov       [esi+54h],edx
  00406d26  e815041400            call      00547140h
  00406d2b  99                    cdq
  00406d2c  33c2                  xor       eax,edx
  00406d2e  2bc2                  sub       eax,edx
  00406d30  83e00f                and       eax,0Fh
  00406d33  33c2                  xor       eax,edx
  00406d35  2bc2                  sub       eax,edx
  00406d37  83e808                sub       eax,8
  00406d3a  c1e00c                shl       eax,0Ch
  00406d3d  894658                mov       [esi+58h],eax
  00406d40  e8fb031400            call      00547140h
  00406d45  99                    cdq
  00406d46  33c2                  xor       eax,edx
  00406d48  2bc2                  sub       eax,edx
  00406d4a  25ff070000            and       eax,7FFh
  00406d4f  33c2                  xor       eax,edx
  00406d51  2bc2                  sub       eax,edx
  00406d53  0500080000            add       eax,800h
  00406d58  66894628              mov       [esi+28h],ax
  00406d5c  e8df031400            call      00547140h
  00406d61  99                    cdq
  00406d62  33c2                  xor       eax,edx
  00406d64  66c746100100          mov       word [esi+10h],1
  00406d6a  2bc2                  sub       eax,edx
  00406d6c  66895e12              mov       [esi+12h],bx
  00406d70  25ff070000            and       eax,7FFh
  00406d75  885e40                mov       [esi+40h],bl
  00406d78  33c2                  xor       eax,edx
  00406d7a  2bc2                  sub       eax,edx
  00406d7c  0500080000            add       eax,800h
  00406d81  6689462a              mov       [esi+2Ah],ax

LAB_00406d85:
  00406d85  66837e1001            cmp       word [esi+10h],1
  00406d8a  7553                  jne       short 00406DDFh
  00406d8c  8b5650                mov       edx,[esi+50h]
  00406d8f  8b4e1c                mov       ecx,[esi+1Ch]
  00406d92  8b4620                mov       eax,[esi+20h]
  00406d95  03ca                  add       ecx,edx
  00406d97  8b5e24                mov       ebx,[esi+24h]
  00406d9a  894e1c                mov       [esi+1Ch],ecx
  00406d9d  8b4e54                mov       ecx,[esi+54h]
  00406da0  03c1                  add       eax,ecx
  00406da2  81c100f0ffff          add       ecx,0FFFFF000h
  00406da8  894620                mov       [esi+20h],eax
  00406dab  8b4658                mov       eax,[esi+58h]
  00406dae  03d8                  add       ebx,eax
  00406db0  66ff4612              inc       word [esi+12h]
  00406db4  668b4612              mov       ax,[esi+12h]
  00406db8  895e24                mov       [esi+24h],ebx
  00406dbb  663d1800              cmp       ax,18h
  00406dbf  894e54                mov       [esi+54h],ecx
  00406dc2  7506                  jne       short 00406DCAh
  00406dc4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00406dca:
  00406dca  663d0600              cmp       ax,6
  00406dce  740c                  je        short 00406DDCh
  00406dd0  663d0c00              cmp       ax,0Ch
  00406dd4  7406                  je        short 00406DDCh
  00406dd6  663d1200              cmp       ax,12h
  00406dda  7503                  jne       short 00406DDFh

LAB_00406ddc:
  00406ddc  fe4640                inc       byte [esi+40h]

LAB_00406ddf:
  00406ddf  5e                    pop       esi
  00406de0  5b                    pop       ebx
  00406de1  c3                    ret
  00406de2  90                    nop
  00406de3  90                    nop
  00406de4  90                    nop
  00406de5  90                    nop
  00406de6  90                    nop
  00406de7  90                    nop
  00406de8  90                    nop
  00406de9  90                    nop
  00406dea  90                    nop
  00406deb  90                    nop
  00406dec  90                    nop
  00406ded  90                    nop
  00406dee  90                    nop
  00406def  90                    nop

6 cible(s) de saut interne : 0x00406c9e, 0x00406ca2, 0x00406d85, 0x00406dca, 0x00406ddc, 0x00406ddf


