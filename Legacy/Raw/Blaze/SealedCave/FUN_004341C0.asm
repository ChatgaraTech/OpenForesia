Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004341c0  section .text  offset fichier 0x335c0  240 octets

  004341c0  53                    push      ebx
  004341c1  56                    push      esi
  004341c2  8b74240c              mov       esi,[esp+0Ch]
  004341c6  33db                  xor       ebx,ebx
  004341c8  66395e10              cmp       [esi+10h],bx
  004341cc  0f8589000000          jne       near 0043425Bh
  004341d2  66c746100100          mov       word [esi+10h],1
  004341d8  c7463c700d5500        mov       dword [esi+3Ch],550D70h
  004341df  c6460a06              mov       byte [esi+0Ah],6
  004341e3  885e0b                mov       [esi+0Bh],bl
  004341e6  e8552f1100            call      00547140h
  004341eb  99                    cdq
  004341ec  b9a0000000            mov       ecx,0A0h
  004341f1  f7f9                  idiv      ecx
  004341f3  8b461c                mov       eax,[esi+1Ch]
  004341f6  83ea50                sub       edx,50h
  004341f9  c1e20c                shl       edx,0Ch
  004341fc  03c2                  add       eax,edx
  004341fe  89461c                mov       [esi+1Ch],eax
  00434201  e83a2f1100            call      00547140h
  00434206  99                    cdq
  00434207  b9a0000000            mov       ecx,0A0h
  0043420c  f7f9                  idiv      ecx
  0043420e  8b4e24                mov       ecx,[esi+24h]
  00434211  83ea50                sub       edx,50h
  00434214  c1e20c                shl       edx,0Ch
  00434217  03ca                  add       ecx,edx
  00434219  894e24                mov       [esi+24h],ecx
  0043421c  e81f2f1100            call      00547140h
  00434221  99                    cdq
  00434222  b914000000            mov       ecx,14h
  00434227  66895e14              mov       [esi+14h],bx
  0043422b  f7f9                  idiv      ecx
  0043422d  8b4e20                mov       ecx,[esi+20h]
  00434230  b8c4090000            mov       eax,9C4h
  00434235  66894628              mov       [esi+28h],ax
  00434239  6689462a              mov       [esi+2Ah],ax
  0043423d  c1e20c                shl       edx,0Ch
  00434240  03ca                  add       ecx,edx
  00434242  894e20                mov       [esi+20h],ecx
  00434245  e8f62e1100            call      00547140h
  0043424a  99                    cdq
  0043424b  b90a000000            mov       ecx,0Ah
  00434250  885e40                mov       [esi+40h],bl
  00434253  f7f9                  idiv      ecx
  00434255  03d1                  add       edx,ecx
  00434257  66895612              mov       [esi+12h],dx

LAB_0043425b:
  0043425b  66837e1001            cmp       word [esi+10h],1
  00434260  7549                  jne       short 004342ABh
  00434262  668b4612              mov       ax,[esi+12h]
  00434266  8b4e20                mov       ecx,[esi+20h]
  00434269  0fbfd0                movsx     edx,ax
  0043426c  c1e20c                shl       edx,0Ch
  0043426f  03ca                  add       ecx,edx
  00434271  66ff4614              inc       word [esi+14h]
  00434275  66837e1402            cmp       word [esi+14h],2
  0043427a  894e20                mov       [esi+20h],ecx
  0043427d  7509                  jne       short 00434288h
  0043427f  48                    dec       eax
  00434280  66895e14              mov       [esi+14h],bx
  00434284  66894612              mov       [esi+12h],ax

LAB_00434288:
  00434288  668b4612              mov       ax,[esi+12h]
  0043428c  663d0700              cmp       ax,7
  00434290  7d0d                  jge       short 0043429Fh
  00434292  b938ffffff            mov       ecx,0FFFFFF38h
  00434297  66014e28              add       [esi+28h],cx
  0043429b  66014e2a              add       [esi+2Ah],cx

LAB_0043429f:
  0043429f  663d0300              cmp       ax,3
  004342a3  7d06                  jge       short 004342ABh
  004342a5  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004342ab:
  004342ab  5e                    pop       esi
  004342ac  5b                    pop       ebx
  004342ad  c3                    ret
  004342ae  90                    nop
  004342af  90                    nop

4 cible(s) de saut interne : 0x0043425b, 0x00434288, 0x0043429f, 0x004342ab
