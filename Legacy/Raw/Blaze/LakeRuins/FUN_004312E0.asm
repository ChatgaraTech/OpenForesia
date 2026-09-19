Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004312e0  section .text  offset fichier 0x306e0  480 octets

  004312e0  56                    push      esi
  004312e1  8b742408              mov       esi,[esp+8]
  004312e5  66837e1000            cmp       word [esi+10h],0
  004312ea  0f854c010000          jne       near 0043143Ch
  004312f0  c7463c600b5500        mov       dword [esi+3Ch],550B60h
  004312f7  e8445e1100            call      00547140h
  004312fc  99                    cdq
  004312fd  33c2                  xor       eax,edx
  004312ff  2bc2                  sub       eax,edx
  00431301  25ff030000            and       eax,3FFh
  00431306  33c2                  xor       eax,edx
  00431308  2bc2                  sub       eax,edx
  0043130a  05000c0000            add       eax,0C00h
  0043130f  66894628              mov       [esi+28h],ax
  00431313  e8285e1100            call      00547140h
  00431318  99                    cdq
  00431319  33c2                  xor       eax,edx
  0043131b  c6460a0d              mov       byte [esi+0Ah],0Dh
  0043131f  2bc2                  sub       eax,edx
  00431321  c6460b00              mov       byte [esi+0Bh],0
  00431325  25ff030000            and       eax,3FFh
  0043132a  33c2                  xor       eax,edx
  0043132c  2bc2                  sub       eax,edx
  0043132e  05000c0000            add       eax,0C00h
  00431333  6689462a              mov       [esi+2Ah],ax
  00431337  e8045e1100            call      00547140h
  0043133c  99                    cdq
  0043133d  b964000000            mov       ecx,64h
  00431342  f7f9                  idiv      ecx
  00431344  8b4e1c                mov       ecx,[esi+1Ch]
  00431347  83ea32                sub       edx,32h
  0043134a  c1e20c                shl       edx,0Ch
  0043134d  03ca                  add       ecx,edx
  0043134f  894e1c                mov       [esi+1Ch],ecx
  00431352  e8e95d1100            call      00547140h
  00431357  99                    cdq
  00431358  b932000000            mov       ecx,32h
  0043135d  f7f9                  idiv      ecx
  0043135f  8b4620                mov       eax,[esi+20h]
  00431362  83ea19                sub       edx,19h
  00431365  c1e20c                shl       edx,0Ch
  00431368  03c2                  add       eax,edx
  0043136a  894620                mov       [esi+20h],eax
  0043136d  e8ce5d1100            call      00547140h
  00431372  99                    cdq
  00431373  b9c8000000            mov       ecx,0C8h
  00431378  f7f9                  idiv      ecx
  0043137a  8b4e24                mov       ecx,[esi+24h]
  0043137d  83ea64                sub       edx,64h
  00431380  c1e20c                shl       edx,0Ch
  00431383  03ca                  add       ecx,edx
  00431385  0fbf5632              movsx     edx,word [esi+32h]
  00431389  52                    push      edx
  0043138a  894e24                mov       [esi+24h],ecx
  0043138d  e88a461100            call      00545A1Ch
  00431392  8b4e24                mov       ecx,[esi+24h]
  00431395  8d0480                lea       eax,[eax+eax*4]
  00431398  8d0480                lea       eax,[eax+eax*4]
  0043139b  d1e0                  shl       eax,1
  0043139d  03c8                  add       ecx,eax
  0043139f  894e24                mov       [esi+24h],ecx
  004313a2  0fbf4e32              movsx     ecx,word [esi+32h]
  004313a6  51                    push      ecx
  004313a7  e86a461100            call      00545A16h
  004313ac  8d0480                lea       eax,[eax+eax*4]
  004313af  8d1480                lea       edx,[eax+eax*4]
  004313b2  8b461c                mov       eax,[esi+1Ch]
  004313b5  d1e2                  shl       edx,1
  004313b7  03c2                  add       eax,edx
  004313b9  89461c                mov       [esi+1Ch],eax
  004313bc  e87f5d1100            call      00547140h
  004313c1  99                    cdq
  004313c2  b906000000            mov       ecx,6
  004313c7  f7f9                  idiv      ecx
  004313c9  83ea03                sub       edx,3
  004313cc  c1e20c                shl       edx,0Ch
  004313cf  895650                mov       [esi+50h],edx
  004313d2  e8695d1100            call      00547140h
  004313d7  99                    cdq
  004313d8  b90f000000            mov       ecx,0Fh
  004313dd  f7f9                  idiv      ecx
  004313df  83c20a                add       edx,0Ah
  004313e2  c1e20c                shl       edx,0Ch
  004313e5  895654                mov       [esi+54h],edx
  004313e8  e8535d1100            call      00547140h
  004313ed  99                    cdq
  004313ee  b906000000            mov       ecx,6
  004313f3  f7f9                  idiv      ecx
  004313f5  83ea03                sub       edx,3
  004313f8  c1e20c                shl       edx,0Ch
  004313fb  895658                mov       [esi+58h],edx
  004313fe  e83d5d1100            call      00547140h
  00431403  99                    cdq
  00431404  b9000c0000            mov       ecx,0C00h
  00431409  f7f9                  idiv      ecx
  0043140b  81c200100000          add       edx,1000h
  00431411  66895628              mov       [esi+28h],dx
  00431415  e8265d1100            call      00547140h
  0043141a  99                    cdq
  0043141b  b9000c0000            mov       ecx,0C00h
  00431420  66c746100100          mov       word [esi+10h],1
  00431426  f7f9                  idiv      ecx
  00431428  66c746120000          mov       word [esi+12h],0
  0043142e  c6464000              mov       byte [esi+40h],0
  00431432  81c200100000          add       edx,1000h
  00431438  6689562a              mov       [esi+2Ah],dx

LAB_0043143c:
  0043143c  66837e1001            cmp       word [esi+10h],1
  00431441  7570                  jne       short 004314B3h
  00431443  8b5650                mov       edx,[esi+50h]
  00431446  8b461c                mov       eax,[esi+1Ch]
  00431449  8b4e20                mov       ecx,[esi+20h]
  0043144c  03c2                  add       eax,edx
  0043144e  8b5624                mov       edx,[esi+24h]
  00431451  89461c                mov       [esi+1Ch],eax
  00431454  8b4654                mov       eax,[esi+54h]
  00431457  03c8                  add       ecx,eax
  00431459  0500e0ffff            add       eax,0FFFFE000h
  0043145e  894e20                mov       [esi+20h],ecx
  00431461  8b4e58                mov       ecx,[esi+58h]
  00431464  03d1                  add       edx,ecx
  00431466  b1fe                  mov       cl,0FEh
  00431468  895624                mov       [esi+24h],edx
  0043146b  8a5638                mov       dl,[esi+38h]
  0043146e  02d1                  add       dl,cl
  00431470  66ff4612              inc       word [esi+12h]
  00431474  885638                mov       [esi+38h],dl
  00431477  8a5639                mov       dl,[esi+39h]
  0043147a  02d1                  add       dl,cl
  0043147c  894654                mov       [esi+54h],eax
  0043147f  885639                mov       [esi+39h],dl
  00431482  8a563a                mov       dl,[esi+3Ah]
  00431485  02d1                  add       dl,cl
  00431487  88563a                mov       [esi+3Ah],dl
  0043148a  e8b15c1100            call      00547140h
  0043148f  99                    cdq
  00431490  b906000000            mov       ecx,6
  00431495  f7f9                  idiv      ecx
  00431497  668b4612              mov       ax,[esi+12h]
  0043149b  663d0a00              cmp       ax,0Ah
  0043149f  7e06                  jle       short 004314A7h
  004314a1  6683fa01              cmp       dx,1
  004314a5  7406                  je        short 004314ADh

LAB_004314a7:
  004314a7  663d1000              cmp       ax,10h
  004314ab  7506                  jne       short 004314B3h

LAB_004314ad:
  004314ad  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004314b3:
  004314b3  5e                    pop       esi
  004314b4  c3                    ret
  004314b5  90                    nop
  004314b6  90                    nop
  004314b7  90                    nop
  004314b8  90                    nop
  004314b9  90                    nop
  004314ba  90                    nop
  004314bb  90                    nop
  004314bc  90                    nop
  004314bd  90                    nop
  004314be  90                    nop
  004314bf  90                    nop

4 cible(s) de saut interne : 0x0043143c, 0x004314a7, 0x004314ad, 0x004314b3
