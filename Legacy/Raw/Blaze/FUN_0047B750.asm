
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b750  section .text  offset fichier 0x7ab50  66 octets

  0047b750  56                    push      esi
  0047b751  8b742408              mov       esi,[esp+8]
  0047b755  66837e1000            cmp       word [esi+10h],0
  0047b75a  7514                  jne       short 0047B770h
  0047b75c  66c746100100          mov       word [esi+10h],1
  0047b762  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047b769  c7462800200020        mov       dword [esi+28h],20002000h

LAB_0047b770:
  0047b770  56                    push      esi
  0047b771  e80aa10b00            call      00535880h
  0047b776  8b4638                mov       eax,[esi+38h]
  0047b779  83c404                add       esp,4
  0047b77c  85c0                  test      eax,eax
  0047b77e  740a                  je        short 0047B78Ah
  0047b780  05f0efefff            add       eax,0FFEFEFF0h
  0047b785  894638                mov       [esi+38h],eax
  0047b788  5e                    pop       esi
  0047b789  c3                    ret

LAB_0047b78a:
  0047b78a  c70600000000          mov       dword [esi],0
  0047b790  5e                    pop       esi
  0047b791  c3                    ret

2 cible(s) de saut interne : 0x0047b770, 0x0047b78a
