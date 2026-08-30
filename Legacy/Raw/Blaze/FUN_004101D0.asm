; 0x004101d0 — la braise qui derive (138 instructions, absente de Blaze.c). Planche 0x0054f840, parcourt les enregistrements 1 a 3.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004101d0  section .text  offset fichier 0xf5d0  420 octets

  004101d0  55                    push      ebp
  004101d1  56                    push      esi
  004101d2  8b74240c              mov       esi,[esp+0Ch]
  004101d6  57                    push      edi
  004101d7  66837e1000            cmp       word [esi+10h],0
  004101dc  0f8520010000          jne       near 00410302h
  004101e2  66c746100100          mov       word [esi+10h],1
  004101e8  c7463c40f85400        mov       dword [esi+3Ch],54F840h
  004101ef  c6464001              mov       byte [esi+40h],1
  004101f3  c6460a0a              mov       byte [esi+0Ah],0Ah
  004101f7  c6460b00              mov       byte [esi+0Bh],0
  004101fb  e8406f1300            call      00547140h
  00410200  99                    cdq
  00410201  33c2                  xor       eax,edx
  00410203  2bc2                  sub       eax,edx
  00410205  25ff010000            and       eax,1FFh
  0041020a  33c2                  xor       eax,edx
  0041020c  2bc2                  sub       eax,edx
  0041020e  0500080000            add       eax,800h
  00410213  66894628              mov       [esi+28h],ax
  00410217  6689462a              mov       [esi+2Ah],ax
  0041021b  e8206f1300            call      00547140h
  00410220  99                    cdq
  00410221  b905000000            mov       ecx,5
  00410226  f7f9                  idiv      ecx
  00410228  81462000600400        add       dword [esi+20h],46000h
  0041022f  8bfa                  mov       edi,edx
  00410231  03f9                  add       edi,ecx
  00410233  e8086f1300            call      00547140h
  00410238  99                    cdq
  00410239  b928000000            mov       ecx,28h
  0041023e  f7f9                  idiv      ecx
  00410240  8b461c                mov       eax,[esi+1Ch]
  00410243  83ea14                sub       edx,14h
  00410246  c1e20c                shl       edx,0Ch
  00410249  03c2                  add       eax,edx
  0041024b  89461c                mov       [esi+1Ch],eax
  0041024e  e8ed6e1300            call      00547140h
  00410253  99                    cdq
  00410254  b928000000            mov       ecx,28h
  00410259  f7f9                  idiv      ecx
  0041025b  8b4624                mov       eax,[esi+24h]
  0041025e  0fbfff                movsx     edi,di
  00410261  83ea14                sub       edx,14h
  00410264  c1e20c                shl       edx,0Ch
  00410267  03c2                  add       eax,edx
  00410269  0fbf5632              movsx     edx,word [esi+32h]
  0041026d  52                    push      edx
  0041026e  894624                mov       [esi+24h],eax
  00410271  e8a0571300            call      00545A16h
  00410276  8be8                  mov       ebp,eax
  00410278  0fafef                imul      ebp,edi
  0041027b  e8c06e1300            call      00547140h
  00410280  99                    cdq
  00410281  33c2                  xor       eax,edx
  00410283  2bc2                  sub       eax,edx
  00410285  83e003                and       eax,3
  00410288  33c2                  xor       eax,edx
  0041028a  2bc2                  sub       eax,edx
  0041028c  83e802                sub       eax,2
  0041028f  c1e00c                shl       eax,0Ch
  00410292  03e8                  add       ebp,eax
  00410294  0fbf4632              movsx     eax,word [esi+32h]
  00410298  50                    push      eax
  00410299  896e58                mov       [esi+58h],ebp
  0041029c  e87b571300            call      00545A1Ch
  004102a1  8be8                  mov       ebp,eax
  004102a3  0fafef                imul      ebp,edi
  004102a6  e8956e1300            call      00547140h
  004102ab  99                    cdq
  004102ac  33c2                  xor       eax,edx
  004102ae  2bc2                  sub       eax,edx
  004102b0  83e003                and       eax,3
  004102b3  33c2                  xor       eax,edx
  004102b5  2bc2                  sub       eax,edx
  004102b7  83e802                sub       eax,2
  004102ba  c1e00c                shl       eax,0Ch
  004102bd  03e8                  add       ebp,eax
  004102bf  896e50                mov       [esi+50h],ebp
  004102c2  e8796e1300            call      00547140h
  004102c7  99                    cdq
  004102c8  b90a000000            mov       ecx,0Ah
  004102cd  f7f9                  idiv      ecx
  004102cf  83ea05                sub       edx,5
  004102d2  c1e20c                shl       edx,0Ch
  004102d5  895654                mov       [esi+54h],edx
  004102d8  66c746120000          mov       word [esi+12h],0
  004102de  e85d6e1300            call      00547140h
  004102e3  99                    cdq
  004102e4  33c2                  xor       eax,edx
  004102e6  c64638c8              mov       byte [esi+38h],0C8h
  004102ea  2bc2                  sub       eax,edx
  004102ec  83e003                and       eax,3
  004102ef  33c2                  xor       eax,edx
  004102f1  2bc2                  sub       eax,edx
  004102f3  83c003                add       eax,3
  004102f6  66894614              mov       [esi+14h],ax
  004102fa  b078                  mov       al,78h
  004102fc  884639                mov       [esi+39h],al
  004102ff  88463a                mov       [esi+3Ah],al

LAB_00410302:
  00410302  66837e1001            cmp       word [esi+10h],1
  00410307  7563                  jne       short 0041036Ch
  00410309  8b5650                mov       edx,[esi+50h]
  0041030c  8b461c                mov       eax,[esi+1Ch]
  0041030f  8b6e20                mov       ebp,[esi+20h]
  00410312  8b4e58                mov       ecx,[esi+58h]
  00410315  8b7e24                mov       edi,[esi+24h]
  00410318  03c2                  add       eax,edx
  0041031a  89461c                mov       [esi+1Ch],eax
  0041031d  8b4654                mov       eax,[esi+54h]
  00410320  03e8                  add       ebp,eax
  00410322  03f9                  add       edi,ecx
  00410324  66ff4612              inc       word [esi+12h]
  00410328  668b4612              mov       ax,[esi+12h]
  0041032c  663b4614              cmp       ax,[esi+14h]
  00410330  896e20                mov       [esi+20h],ebp
  00410333  897e24                mov       [esi+24h],edi
  00410336  7e25                  jle       short 0041035Dh
  00410338  8a4640                mov       al,[esi+40h]
  0041033b  b900010000            mov       ecx,100h
  00410340  66014e28              add       [esi+28h],cx
  00410344  66014e2a              add       [esi+2Ah],cx
  00410348  fec0                  inc       al
  0041034a  66c746120000          mov       word [esi+12h],0
  00410350  3c04                  cmp       al,4
  00410352  884640                mov       [esi+40h],al
  00410355  7206                  jb        short 0041035Dh
  00410357  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041035d:
  0041035d  6af6                  push      0FFFFFFF6h
  0041035f  6af6                  push      0FFFFFFF6h
  00410361  6af6                  push      0FFFFFFF6h
  00410363  56                    push      esi
  00410364  e8774affff            call      00404DE0h
  00410369  83c410                add       esp,10h

LAB_0041036c:
  0041036c  5f                    pop       edi
  0041036d  5e                    pop       esi
  0041036e  5d                    pop       ebp
  0041036f  c3                    ret
  00410370  8b442404              mov       eax,[esp+4]

3 cible(s) de saut interne : 0x00410302, 0x0041035d, 0x0041036c
