Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042eea0  section .text  offset fichier 0x2e2a0  240 octets

  0042eea0  56                    push      esi
  0042eea1  8b742408              mov       esi,[esp+8]
  0042eea5  66837e1000            cmp       word [esi+10h],0
  0042eeaa  0f8589000000          jne       near 0042EF39h
  0042eeb0  66c746100100          mov       word [esi+10h],1
  0042eeb6  c7463c58095500        mov       dword [esi+3Ch],550958h
  0042eebd  c6460a06              mov       byte [esi+0Ah],6
  0042eec1  c6460b00              mov       byte [esi+0Bh],0
  0042eec5  e876821100            call      00547140h
  0042eeca  99                    cdq
  0042eecb  b9a0000000            mov       ecx,0A0h
  0042eed0  f7f9                  idiv      ecx
  0042eed2  8b461c                mov       eax,[esi+1Ch]
  0042eed5  83ea50                sub       edx,50h
  0042eed8  c1e20c                shl       edx,0Ch
  0042eedb  03c2                  add       eax,edx
  0042eedd  89461c                mov       [esi+1Ch],eax
  0042eee0  e85b821100            call      00547140h
  0042eee5  99                    cdq
  0042eee6  b9a0000000            mov       ecx,0A0h
  0042eeeb  f7f9                  idiv      ecx
  0042eeed  8b4e24                mov       ecx,[esi+24h]
  0042eef0  83ea50                sub       edx,50h
  0042eef3  c1e20c                shl       edx,0Ch
  0042eef6  03ca                  add       ecx,edx
  0042eef8  894e24                mov       [esi+24h],ecx
  0042eefb  e840821100            call      00547140h
  0042ef00  99                    cdq
  0042ef01  b914000000            mov       ecx,14h
  0042ef06  66c746140000          mov       word [esi+14h],0
  0042ef0c  f7f9                  idiv      ecx
  0042ef0e  8b4e20                mov       ecx,[esi+20h]
  0042ef11  b8c4090000            mov       eax,9C4h
  0042ef16  66894628              mov       [esi+28h],ax
  0042ef1a  6689462a              mov       [esi+2Ah],ax
  0042ef1e  c1e20c                shl       edx,0Ch
  0042ef21  03ca                  add       ecx,edx
  0042ef23  894e20                mov       [esi+20h],ecx
  0042ef26  e815821100            call      00547140h
  0042ef2b  99                    cdq
  0042ef2c  b90a000000            mov       ecx,0Ah
  0042ef31  f7f9                  idiv      ecx
  0042ef33  03d1                  add       edx,ecx
  0042ef35  66895612              mov       [esi+12h],dx

LAB_0042ef39:
  0042ef39  66837e1001            cmp       word [esi+10h],1
  0042ef3e  754b                  jne       short 0042EF8Bh
  0042ef40  668b4612              mov       ax,[esi+12h]
  0042ef44  8b4e20                mov       ecx,[esi+20h]
  0042ef47  0fbfd0                movsx     edx,ax
  0042ef4a  c1e20c                shl       edx,0Ch
  0042ef4d  03ca                  add       ecx,edx
  0042ef4f  66ff4614              inc       word [esi+14h]
  0042ef53  66837e1402            cmp       word [esi+14h],2
  0042ef58  894e20                mov       [esi+20h],ecx
  0042ef5b  750b                  jne       short 0042EF68h
  0042ef5d  48                    dec       eax
  0042ef5e  66c746140000          mov       word [esi+14h],0
  0042ef64  66894612              mov       [esi+12h],ax

LAB_0042ef68:
  0042ef68  668b4612              mov       ax,[esi+12h]
  0042ef6c  663d0700              cmp       ax,7
  0042ef70  7d0d                  jge       short 0042EF7Fh
  0042ef72  b938ffffff            mov       ecx,0FFFFFF38h
  0042ef77  66014e28              add       [esi+28h],cx
  0042ef7b  66014e2a              add       [esi+2Ah],cx

LAB_0042ef7f:
  0042ef7f  663d0300              cmp       ax,3
  0042ef83  7d06                  jge       short 0042EF8Bh
  0042ef85  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042ef8b:
  0042ef8b  5e                    pop       esi
  0042ef8c  c3                    ret
  0042ef8d  90                    nop
  0042ef8e  90                    nop
  0042ef8f  90                    nop

4 cible(s) de saut interne : 0x0042ef39, 0x0042ef68, 0x0042ef7f, 0x0042ef8b
