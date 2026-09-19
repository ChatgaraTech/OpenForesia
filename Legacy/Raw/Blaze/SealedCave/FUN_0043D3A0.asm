Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043d3a0  section .text  offset fichier 0x3c7a0  304 octets

  0043d3a0  56                    push      esi
  0043d3a1  8b742408              mov       esi,[esp+8]
  0043d3a5  33c0                  xor       eax,eax
  0043d3a7  66394610              cmp       [esi+10h],ax
  0043d3ab  0f85b9000000          jne       near 0043D46Ah
  0043d3b1  884640                mov       [esi+40h],al
  0043d3b4  88460b                mov       [esi+0Bh],al
  0043d3b7  b800080000            mov       eax,800h
  0043d3bc  66c746100100          mov       word [esi+10h],1
  0043d3c2  c7463cf0115500        mov       dword [esi+3Ch],5511F0h
  0043d3c9  c6460a09              mov       byte [esi+0Ah],9
  0043d3cd  66894628              mov       [esi+28h],ax
  0043d3d1  6689462a              mov       [esi+2Ah],ax
  0043d3d5  e8669d1000            call      00547140h
  0043d3da  99                    cdq
  0043d3db  b91e000000            mov       ecx,1Eh
  0043d3e0  f7f9                  idiv      ecx
  0043d3e2  8b4e1c                mov       ecx,[esi+1Ch]
  0043d3e5  83ea0f                sub       edx,0Fh
  0043d3e8  c1e20c                shl       edx,0Ch
  0043d3eb  03ca                  add       ecx,edx
  0043d3ed  894e1c                mov       [esi+1Ch],ecx
  0043d3f0  e84b9d1000            call      00547140h
  0043d3f5  99                    cdq
  0043d3f6  b91e000000            mov       ecx,1Eh
  0043d3fb  f7f9                  idiv      ecx
  0043d3fd  8b4624                mov       eax,[esi+24h]
  0043d400  8b4e20                mov       ecx,[esi+20h]
  0043d403  894e54                mov       [esi+54h],ecx
  0043d406  83ea0f                sub       edx,0Fh
  0043d409  c1e20c                shl       edx,0Ch
  0043d40c  03c2                  add       eax,edx
  0043d40e  8b561c                mov       edx,[esi+1Ch]
  0043d411  894624                mov       [esi+24h],eax
  0043d414  895650                mov       [esi+50h],edx
  0043d417  894658                mov       [esi+58h],eax
  0043d41a  e8219d1000            call      00547140h
  0043d41f  99                    cdq
  0043d420  33c2                  xor       eax,edx
  0043d422  2bc2                  sub       eax,edx
  0043d424  25ff0f0000            and       eax,0FFFh
  0043d429  33c2                  xor       eax,edx
  0043d42b  2bc2                  sub       eax,edx
  0043d42d  66894612              mov       [esi+12h],ax
  0043d431  e80a9d1000            call      00547140h
  0043d436  99                    cdq
  0043d437  b92d000000            mov       ecx,2Dh
  0043d43c  f7f9                  idiv      ecx
  0043d43e  83c21e                add       edx,1Eh
  0043d441  66895614              mov       [esi+14h],dx
  0043d445  e8f69c1000            call      00547140h
  0043d44a  99                    cdq
  0043d44b  b91e000000            mov       ecx,1Eh
  0043d450  f7f9                  idiv      ecx
  0043d452  03d1                  add       edx,ecx
  0043d454  66895616              mov       [esi+16h],dx
  0043d458  e8e39c1000            call      00547140h
  0043d45d  99                    cdq
  0043d45e  b91e000000            mov       ecx,1Eh
  0043d463  f7f9                  idiv      ecx
  0043d465  03d1                  add       edx,ecx
  0043d467  89565c                mov       [esi+5Ch],edx

LAB_0043d46a:
  0043d46a  66837e1001            cmp       word [esi+10h],1
  0043d46f  7555                  jne       short 0043D4C6h
  0043d471  0fbf5612              movsx     edx,word [esi+12h]
  0043d475  52                    push      edx
  0043d476  e8a1851000            call      00545A1Ch
  0043d47b  0fbf4e14              movsx     ecx,word [esi+14h]
  0043d47f  0fafc1                imul      eax,ecx
  0043d482  0fbf5612              movsx     edx,word [esi+12h]
  0043d486  8b4e50                mov       ecx,[esi+50h]
  0043d489  52                    push      edx
  0043d48a  03c1                  add       eax,ecx
  0043d48c  89461c                mov       [esi+1Ch],eax
  0043d48f  e882851000            call      00545A16h
  0043d494  0fbf4e14              movsx     ecx,word [esi+14h]
  0043d498  0fafc1                imul      eax,ecx
  0043d49b  8b5620                mov       edx,[esi+20h]
  0043d49e  8b4e58                mov       ecx,[esi+58h]
  0043d4a1  81c200800000          add       edx,8000h
  0043d4a7  03c1                  add       eax,ecx
  0043d4a9  895620                mov       [esi+20h],edx
  0043d4ac  668b5616              mov       dx,[esi+16h]
  0043d4b0  66015612              add       [esi+12h],dx
  0043d4b4  894624                mov       [esi+24h],eax
  0043d4b7  8b465c                mov       eax,[esi+5Ch]
  0043d4ba  48                    dec       eax
  0043d4bb  89465c                mov       [esi+5Ch],eax
  0043d4be  7906                  jns       short 0043D4C6h
  0043d4c0  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043d4c6:
  0043d4c6  5e                    pop       esi
  0043d4c7  c3                    ret
  0043d4c8  90                    nop
  0043d4c9  90                    nop
  0043d4ca  90                    nop
  0043d4cb  90                    nop
  0043d4cc  90                    nop
  0043d4cd  90                    nop
  0043d4ce  90                    nop
  0043d4cf  90                    nop

2 cible(s) de saut interne : 0x0043d46a, 0x0043d4c6
