Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042ec30  section .text  offset fichier 0x2e030  240 octets

  0042ec30  56                    push      esi
  0042ec31  8b742408              mov       esi,[esp+8]
  0042ec35  66837e1000            cmp       word [esi+10h],0
  0042ec3a  0f85a0000000          jne       near 0042ECE0h
  0042ec40  c7463c80095500        mov       dword [esi+3Ch],550980h
  0042ec47  c6464000              mov       byte [esi+40h],0
  0042ec4b  c6460a0b              mov       byte [esi+0Ah],0Bh
  0042ec4f  c6460b00              mov       byte [esi+0Bh],0
  0042ec53  66c746100100          mov       word [esi+10h],1
  0042ec59  e8e2841100            call      00547140h
  0042ec5e  99                    cdq
  0042ec5f  b90a000000            mov       ecx,0Ah
  0042ec64  f7f9                  idiv      ecx
  0042ec66  83c205                add       edx,5
  0042ec69  66895612              mov       [esi+12h],dx
  0042ec6d  e8ce841100            call      00547140h
  0042ec72  99                    cdq
  0042ec73  33c2                  xor       eax,edx
  0042ec75  2bc2                  sub       eax,edx
  0042ec77  25ff030000            and       eax,3FFh
  0042ec7c  33c2                  xor       eax,edx
  0042ec7e  2bc2                  sub       eax,edx
  0042ec80  0500080000            add       eax,800h
  0042ec85  66894628              mov       [esi+28h],ax
  0042ec89  6689462a              mov       [esi+2Ah],ax
  0042ec8d  b0b4                  mov       al,0B4h
  0042ec8f  884638                mov       [esi+38h],al
  0042ec92  884639                mov       [esi+39h],al
  0042ec95  88463a                mov       [esi+3Ah],al
  0042ec98  e8a3841100            call      00547140h
  0042ec9d  99                    cdq
  0042ec9e  b92c010000            mov       ecx,12Ch
  0042eca3  f7f9                  idiv      ecx
  0042eca5  8b461c                mov       eax,[esi+1Ch]
  0042eca8  81ea96000000          sub       edx,96h
  0042ecae  c1e20c                shl       edx,0Ch
  0042ecb1  03c2                  add       eax,edx
  0042ecb3  89461c                mov       [esi+1Ch],eax
  0042ecb6  e885841100            call      00547140h
  0042ecbb  99                    cdq
  0042ecbc  b92c010000            mov       ecx,12Ch
  0042ecc1  f7f9                  idiv      ecx
  0042ecc3  8b4e24                mov       ecx,[esi+24h]
  0042ecc6  81ea96000000          sub       edx,96h
  0042eccc  c1e20c                shl       edx,0Ch
  0042eccf  03ca                  add       ecx,edx
  0042ecd1  8b5664                mov       edx,[esi+64h]
  0042ecd4  81c200200300          add       edx,32000h
  0042ecda  894e24                mov       [esi+24h],ecx
  0042ecdd  895620                mov       [esi+20h],edx

LAB_0042ece0:
  0042ece0  66837e1001            cmp       word [esi+10h],1
  0042ece5  7533                  jne       short 0042ED1Ah
  0042ece7  668b4612              mov       ax,[esi+12h]
  0042eceb  8b5620                mov       edx,[esi+20h]
  0042ecee  0fbfc8                movsx     ecx,ax
  0042ecf1  c1e10c                shl       ecx,0Ch
  0042ecf4  6afd                  push      0FFFFFFFDh
  0042ecf6  03d1                  add       edx,ecx
  0042ecf8  6afd                  push      0FFFFFFFDh
  0042ecfa  40                    inc       eax
  0042ecfb  6afd                  push      0FFFFFFFDh
  0042ecfd  56                    push      esi
  0042ecfe  895620                mov       [esi+20h],edx
  0042ed01  66894612              mov       [esi+12h],ax
  0042ed05  e8d660fdff            call      00404DE0h
  0042ed0a  8a4638                mov       al,[esi+38h]
  0042ed0d  83c410                add       esp,10h
  0042ed10  84c0                  test      al,al
  0042ed12  7506                  jne       short 0042ED1Ah
  0042ed14  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042ed1a:
  0042ed1a  5e                    pop       esi
  0042ed1b  c3                    ret
  0042ed1c  90                    nop
  0042ed1d  90                    nop
  0042ed1e  90                    nop
  0042ed1f  90                    nop

2 cible(s) de saut interne : 0x0042ece0, 0x0042ed1a
