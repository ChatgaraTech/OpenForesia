
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047da50  section .text  offset fichier 0x7ce50  244 octets

  0047da50  55                    push      ebp
  0047da51  56                    push      esi
  0047da52  8b74240c              mov       esi,[esp+0Ch]
  0047da56  57                    push      edi
  0047da57  66837e1000            cmp       word [esi+10h],0
  0047da5c  7568                  jne       short 0047DAC6h
  0047da5e  c74638a0a0a000        mov       dword [esi+38h],0A0A0A0h
  0047da65  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047da6b  e8d0960c00            call      00547140h
  0047da70  25ff0f0000            and       eax,0FFFh
  0047da75  0500180000            add       eax,1800h
  0047da7a  8bc8                  mov       ecx,eax
  0047da7c  c1e110                shl       ecx,10h
  0047da7f  0bc8                  or        ecx,eax
  0047da81  894e28                mov       [esi+28h],ecx
  0047da84  e8b7960c00            call      00547140h
  0047da89  8b6e20                mov       ebp,[esi+20h]
  0047da8c  66894632              mov       [esi+32h],ax
  0047da90  81c500200300          add       ebp,32000h
  0047da96  896e20                mov       [esi+20h],ebp
  0047da99  e8a2960c00            call      00547140h
  0047da9e  8b4e24                mov       ecx,[esi+24h]
  0047daa1  83e00f                and       eax,0Fh
  0047daa4  c1e00c                shl       eax,0Ch
  0047daa7  ba00c0feff            mov       edx,0FFFEC000h
  0047daac  894e4c                mov       [esi+4Ch],ecx
  0047daaf  2bd0                  sub       edx,eax
  0047dab1  8b461c                mov       eax,[esi+1Ch]
  0047dab4  895654                mov       [esi+54h],edx
  0047dab7  894644                mov       [esi+44h],eax
  0047daba  c6464000              mov       byte [esi+40h],0
  0047dabe  66c746120000          mov       word [esi+12h],0
  0047dac4  eb22                  jmp       short 0047DAE8h

LAB_0047dac6:
  0047dac6  8b4638                mov       eax,[esi+38h]
  0047dac9  b92c010000            mov       ecx,12Ch
  0047dace  05f8f7f7ff            add       eax,0FFF7F7F8h
  0047dad3  894638                mov       [esi+38h],eax
  0047dad6  668b4612              mov       ax,[esi+12h]
  0047dada  0fbfd0                movsx     edx,ax
  0047dadd  2bca                  sub       ecx,edx
  0047dadf  c1f903                sar       ecx,3
  0047dae2  03c8                  add       ecx,eax
  0047dae4  66894e12              mov       [esi+12h],cx

LAB_0047dae8:
  0047dae8  668146320001          add       word [esi+32h],100h
  0047daee  0fbf7e32              movsx     edi,word [esi+32h]
  0047daf2  0fbf6e12              movsx     ebp,word [esi+12h]
  0047daf6  57                    push      edi
  0047daf7  e8207f0c00            call      00545A1Ch
  0047dafc  0fafc5                imul      eax,ebp
  0047daff  8b5644                mov       edx,[esi+44h]
  0047db02  8b4e20                mov       ecx,[esi+20h]
  0047db05  03c2                  add       eax,edx
  0047db07  8b5654                mov       edx,[esi+54h]
  0047db0a  03ca                  add       ecx,edx
  0047db0c  57                    push      edi
  0047db0d  89461c                mov       [esi+1Ch],eax
  0047db10  894e20                mov       [esi+20h],ecx
  0047db13  e8fe7e0c00            call      00545A16h
  0047db18  0fafc5                imul      eax,ebp
  0047db1b  8b7e4c                mov       edi,[esi+4Ch]
  0047db1e  8b5654                mov       edx,[esi+54h]
  0047db21  03c7                  add       eax,edi
  0047db23  81c200400000          add       edx,4000h
  0047db29  66ff4610              inc       word [esi+10h]
  0047db2d  894624                mov       [esi+24h],eax
  0047db30  8b4638                mov       eax,[esi+38h]
  0047db33  895654                mov       [esi+54h],edx
  0047db36  85c0                  test      eax,eax
  0047db38  7506                  jne       short 0047DB40h
  0047db3a  c70600000000          mov       dword [esi],0

LAB_0047db40:
  0047db40  5f                    pop       edi
  0047db41  5e                    pop       esi
  0047db42  5d                    pop       ebp
  0047db43  c3                    ret

3 cible(s) de saut interne : 0x0047dac6, 0x0047dae8, 0x0047db40
