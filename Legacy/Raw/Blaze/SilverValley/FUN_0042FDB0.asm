Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042fdb0  section .text  offset fichier 0x2f1b0  416 octets

  0042fdb0  56                    push      esi
  0042fdb1  8b742408              mov       esi,[esp+8]
  0042fdb5  57                    push      edi
  0042fdb6  8b7e44                mov       edi,[esi+44h]
  0042fdb9  8b461c                mov       eax,[esi+1Ch]
  0042fdbc  8b4e20                mov       ecx,[esi+20h]
  0042fdbf  8b5624                mov       edx,[esi+24h]
  0042fdc2  66837e1000            cmp       word [esi+10h],0
  0042fdc7  897e1c                mov       [esi+1Ch],edi
  0042fdca  8b7e48                mov       edi,[esi+48h]
  0042fdcd  894644                mov       [esi+44h],eax
  0042fdd0  897e20                mov       [esi+20h],edi
  0042fdd3  8b7e4c                mov       edi,[esi+4Ch]
  0042fdd6  897e24                mov       [esi+24h],edi
  0042fdd9  894e48                mov       [esi+48h],ecx
  0042fddc  89564c                mov       [esi+4Ch],edx
  0042fddf  bf01000000            mov       edi,1
  0042fde4  0f85fd000000          jne       near 0042FEE7h
  0042fdea  66897e10              mov       [esi+10h],di
  0042fdee  c7463ce8095500        mov       dword [esi+3Ch],5509E8h
  0042fdf5  e846731100            call      00547140h
  0042fdfa  99                    cdq
  0042fdfb  b9dc050000            mov       ecx,5DCh
  0042fe00  f7f9                  idiv      ecx
  0042fe02  a168e05d00            mov       eax,[5DE068h]
  0042fe07  81eaee020000          sub       edx,2EEh
  0042fe0d  c1e20c                shl       edx,0Ch
  0042fe10  03d0                  add       edx,eax
  0042fe12  89561c                mov       [esi+1Ch],edx
  0042fe15  e826731100            call      00547140h
  0042fe1a  99                    cdq
  0042fe1b  b9b0040000            mov       ecx,4B0h
  0042fe20  f7f9                  idiv      ecx
  0042fe22  8b0d6ce05d00          mov       ecx,[5DE06Ch]
  0042fe28  81c290010000          add       edx,190h
  0042fe2e  c1e20c                shl       edx,0Ch
  0042fe31  03d1                  add       edx,ecx
  0042fe33  895620                mov       [esi+20h],edx
  0042fe36  e805731100            call      00547140h
  0042fe3b  99                    cdq
  0042fe3c  b9dc050000            mov       ecx,5DCh
  0042fe41  f7f9                  idiv      ecx
  0042fe43  a170e05d00            mov       eax,[5DE070h]
  0042fe48  81eaee020000          sub       edx,2EEh
  0042fe4e  c1e20c                shl       edx,0Ch
  0042fe51  03d0                  add       edx,eax
  0042fe53  895624                mov       [esi+24h],edx
  0042fe56  e8e5721100            call      00547140h
  0042fe5b  99                    cdq
  0042fe5c  b9dc050000            mov       ecx,5DCh
  0042fe61  f7f9                  idiv      ecx
  0042fe63  8b0d68e05d00          mov       ecx,[5DE068h]
  0042fe69  81eaee020000          sub       edx,2EEh
  0042fe6f  c1e20c                shl       edx,0Ch
  0042fe72  03d1                  add       edx,ecx
  0042fe74  895644                mov       [esi+44h],edx
  0042fe77  e8c4721100            call      00547140h
  0042fe7c  99                    cdq
  0042fe7d  b990010000            mov       ecx,190h
  0042fe82  f7f9                  idiv      ecx
  0042fe84  a16ce05d00            mov       eax,[5DE06Ch]
  0042fe89  81c2b0040000          add       edx,4B0h
  0042fe8f  c1e20c                shl       edx,0Ch
  0042fe92  03d0                  add       edx,eax
  0042fe94  895648                mov       [esi+48h],edx
  0042fe97  e8a4721100            call      00547140h
  0042fe9c  99                    cdq
  0042fe9d  b9dc050000            mov       ecx,5DCh
  0042fea2  f7f9                  idiv      ecx
  0042fea4  8b0d70e05d00          mov       ecx,[5DE070h]
  0042feaa  b800040000            mov       eax,400h
  0042feaf  66894628              mov       [esi+28h],ax
  0042feb3  6689462a              mov       [esi+2Ah],ax
  0042feb7  81eaee020000          sub       edx,2EEh
  0042febd  c1e20c                shl       edx,0Ch
  0042fec0  03d1                  add       edx,ecx
  0042fec2  89564c                mov       [esi+4Ch],edx
  0042fec5  e876721100            call      00547140h
  0042feca  99                    cdq
  0042fecb  b91e000000            mov       ecx,1Eh
  0042fed0  66c746140000          mov       word [esi+14h],0
  0042fed6  f7f9                  idiv      ecx
  0042fed8  c6460a09              mov       byte [esi+0Ah],9
  0042fedc  c6460b00              mov       byte [esi+0Bh],0
  0042fee0  83c20a                add       edx,0Ah
  0042fee3  66895612              mov       [esi+12h],dx

LAB_0042fee7:
  0042fee7  66397e10              cmp       [esi+10h],di
  0042feeb  7558                  jne       short 0042FF45h
  0042feed  0fbf5612              movsx     edx,word [esi+12h]
  0042fef1  8b7e20                mov       edi,[esi+20h]
  0042fef4  c1e20c                shl       edx,0Ch
  0042fef7  2bfa                  sub       edi,edx
  0042fef9  897e20                mov       [esi+20h],edi
  0042fefc  e83f721100            call      00547140h
  0042ff01  99                    cdq
  0042ff02  b90a000000            mov       ecx,0Ah
  0042ff07  f7f9                  idiv      ecx
  0042ff09  8b461c                mov       eax,[esi+1Ch]
  0042ff0c  83ea05                sub       edx,5
  0042ff0f  03c2                  add       eax,edx
  0042ff11  89461c                mov       [esi+1Ch],eax
  0042ff14  e827721100            call      00547140h
  0042ff19  99                    cdq
  0042ff1a  b90a000000            mov       ecx,0Ah
  0042ff1f  f7f9                  idiv      ecx
  0042ff21  8b4e24                mov       ecx,[esi+24h]
  0042ff24  8b4620                mov       eax,[esi+20h]
  0042ff27  83ea05                sub       edx,5
  0042ff2a  03ca                  add       ecx,edx
  0042ff2c  894e24                mov       [esi+24h],ecx
  0042ff2f  8b156ce05d00          mov       edx,[5DE06Ch]
  0042ff35  81c20000e7ff          add       edx,0FFE70000h
  0042ff3b  3bc2                  cmp       eax,edx
  0042ff3d  7d06                  jge       short 0042FF45h
  0042ff3f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042ff45:
  0042ff45  5f                    pop       edi
  0042ff46  5e                    pop       esi
  0042ff47  c3                    ret
  0042ff48  90                    nop
  0042ff49  90                    nop
  0042ff4a  90                    nop
  0042ff4b  90                    nop
  0042ff4c  90                    nop
  0042ff4d  90                    nop
  0042ff4e  90                    nop
  0042ff4f  90                    nop

2 cible(s) de saut interne : 0x0042fee7, 0x0042ff45
