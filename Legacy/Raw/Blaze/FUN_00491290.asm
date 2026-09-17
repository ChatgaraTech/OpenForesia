Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00491290  section .text  offset fichier 0x90690  1200 octets

  00491290  83ec10                sub       esp,10h
  00491293  b002                  mov       al,2
  00491295  53                    push      ebx
  00491296  88442406              mov       [esp+6],al
  0049129a  88442409              mov       [esp+9],al
  0049129e  8844240c              mov       [esp+0Ch],al
  004912a2  88442413              mov       [esp+13h],al
  004912a6  8b442418              mov       eax,[esp+18h]
  004912aa  55                    push      ebp
  004912ab  56                    push      esi
  004912ac  33db                  xor       ebx,ebx
  004912ae  57                    push      edi
  004912af  8b786c                mov       edi,[eax+6Ch]
  004912b2  885c2410              mov       [esp+10h],bl
  004912b6  c644241101            mov       byte [esp+11h],1
  004912bb  c644241303            mov       byte [esp+13h],3
  004912c0  c644241401            mov       byte [esp+14h],1
  004912c5  c644241603            mov       byte [esp+16h],3
  004912ca  885c2417              mov       [esp+17h],bl
  004912ce  c644241903            mov       byte [esp+19h],3
  004912d3  885c241a              mov       [esp+1Ah],bl
  004912d7  c644241b01            mov       byte [esp+1Bh],1
  004912dc  c644241c03            mov       byte [esp+1Ch],3
  004912e1  885c241d              mov       [esp+1Dh],bl
  004912e5  c644241e01            mov       byte [esp+1Eh],1
  004912ea  e8515e0b00            call      00547140h
  004912ef  99                    cdq
  004912f0  33c2                  xor       eax,edx
  004912f2  2bc2                  sub       eax,edx
  004912f4  660fb66f08            movzx     bp,byte [edi+8]
  004912f9  83e003                and       eax,3
  004912fc  33c2                  xor       eax,edx
  004912fe  8bf0                  mov       esi,eax
  00491300  2bf2                  sub       esi,edx
  00491302  e8395e0b00            call      00547140h
  00491307  99                    cdq
  00491308  b90a000000            mov       ecx,0Ah
  0049130d  f7f9                  idiv      ecx
  0049130f  6683fa05              cmp       dx,5
  00491313  0f8c0b030000          jl        near 00491624h
  00491319  e8225e0b00            call      00547140h
  0049131e  99                    cdq
  0049131f  b90a000000            mov       ecx,0Ah
  00491324  f7f9                  idiv      ecx
  00491326  6683fa05              cmp       dx,5
  0049132a  7f05                  jg        short 00491331h
  0049132c  83c8ff                or        eax,0FFFFFFFFh
  0049132f  eb04                  jmp       short 00491335h

LAB_00491331:
  00491331  8b442428              mov       eax,[esp+28h]

LAB_00491335:
  00491335  0fbfc0                movsx     eax,ax
  00491338  40                    inc       eax
  00491339  83f808                cmp       eax,8
  0049133c  0f87c9020000          ja        near 0049160Bh
  00491342  ff24852c164900        jmp       dword [eax*4+49162Ch]
  00491349  33f6                  xor       esi,esi

LAB_0049134b:
  0049134b  e8f05d0b00            call      00547140h
  00491350  99                    cdq
  00491351  33c2                  xor       eax,edx
  00491353  2bc2                  sub       eax,edx
  00491355  83e003                and       eax,3
  00491358  33c2                  xor       eax,edx
  0049135a  2bc2                  sub       eax,edx
  0049135c  8d1480                lea       edx,[eax+eax*4]
  0049135f  c1e203                shl       edx,3
  00491362  2bd0                  sub       edx,eax
  00491364  8b0c95c0935e00        mov       ecx,[edx*4+5E93C0h]
  0049136b  f7c100000080          test      ecx,80000000h
  00491371  740f                  je        short 00491382h
  00491373  f7c100000050          test      ecx,50000000h
  00491379  7507                  jne       short 00491382h
  0049137b  be01000000            mov       esi,1
  00491380  8be8                  mov       ebp,eax

LAB_00491382:
  00491382  663bf3                cmp       si,bx
  00491385  74c4                  je        short 0049134Bh
  00491387  e97f020000            jmp       0049160Bh
  0049138c  8b3db4876000          mov       edi,[6087B4h]
  00491392  33ed                  xor       ebp,ebp
  00491394  0fbfc6                movsx     eax,si
  00491397  c744242804000000      mov       dword [esp+28h],4
  0049139f  8d748410              lea       esi,[esp+eax*4+10h]

LAB_004913a3:
  004913a3  8a1e                  mov       bl,[esi]
  004913a5  0fbec3                movsx     eax,bl
  004913a8  8d0c80                lea       ecx,[eax+eax*4]
  004913ab  c1e103                shl       ecx,3
  004913ae  2bc8                  sub       ecx,eax
  004913b0  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004913b7  f7c100000080          test      ecx,80000000h
  004913bd  742e                  je        short 004913EDh
  004913bf  f7c100000050          test      ecx,50000000h
  004913c5  7526                  jne       short 004913EDh
  004913c7  8d4878                lea       ecx,[eax+78h]
  004913ca  8b07                  mov       eax,[edi]
  004913cc  c1e10d                shl       ecx,0Dh
  004913cf  03c8                  add       ecx,eax
  004913d1  0fbf8148010000        movsx     eax,word [ecx+148h]
  004913d8  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  004913df  c1e00c                shl       eax,0Ch
  004913e2  99                    cdq
  004913e3  f7f9                  idiv      ecx
  004913e5  85c0                  test      eax,eax
  004913e7  7e04                  jle       short 004913EDh
  004913e9  660fbeeb              movsx     bp,bl

LAB_004913ed:
  004913ed  8b442428              mov       eax,[esp+28h]
  004913f1  46                    inc       esi
  004913f2  48                    dec       eax
  004913f3  89442428              mov       [esp+28h],eax
  004913f7  75aa                  jne       short 004913A3h
  004913f9  e90d020000            jmp       0049160Bh
  004913fe  8b3db4876000          mov       edi,[6087B4h]
  00491404  33ed                  xor       ebp,ebp
  00491406  0fbfd6                movsx     edx,si
  00491409  c744242804000000      mov       dword [esp+28h],4
  00491411  8d749410              lea       esi,[esp+edx*4+10h]

LAB_00491415:
  00491415  8a1e                  mov       bl,[esi]
  00491417  0fbec3                movsx     eax,bl
  0049141a  8d0c80                lea       ecx,[eax+eax*4]
  0049141d  c1e103                shl       ecx,3
  00491420  2bc8                  sub       ecx,eax
  00491422  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  00491429  f7c100000080          test      ecx,80000000h
  0049142f  7431                  je        short 00491462h
  00491431  f7c100000050          test      ecx,50000000h
  00491437  7529                  jne       short 00491462h
  00491439  8d4878                lea       ecx,[eax+78h]
  0049143c  8b07                  mov       eax,[edi]
  0049143e  c1e10d                shl       ecx,0Dh
  00491441  03c8                  add       ecx,eax
  00491443  0fbf8148010000        movsx     eax,word [ecx+148h]
  0049144a  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  00491451  c1e00c                shl       eax,0Ch
  00491454  99                    cdq
  00491455  f7f9                  idiv      ecx
  00491457  3d10270000            cmp       eax,2710h
  0049145c  7d04                  jge       short 00491462h
  0049145e  660fbeeb              movsx     bp,bl

LAB_00491462:
  00491462  8b442428              mov       eax,[esp+28h]
  00491466  46                    inc       esi
  00491467  48                    dec       eax
  00491468  89442428              mov       [esp+28h],eax
  0049146c  75a7                  jne       short 00491415h
  0049146e  e998010000            jmp       0049160Bh
  00491473  8b3db4876000          mov       edi,[6087B4h]
  00491479  bb04000000            mov       ebx,4
  0049147e  0fbfd6                movsx     edx,si
  00491481  8d749410              lea       esi,[esp+edx*4+10h]

LAB_00491485:
  00491485  8a16                  mov       dl,[esi]
  00491487  0fbec2                movsx     eax,dl
  0049148a  8d0c80                lea       ecx,[eax+eax*4]
  0049148d  c1e103                shl       ecx,3
  00491490  2bc8                  sub       ecx,eax
  00491492  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  00491499  f7c100000080          test      ecx,80000000h
  0049149f  741b                  je        short 004914BCh
  004914a1  f7c100000050          test      ecx,50000000h
  004914a7  7513                  jne       short 004914BCh
  004914a9  8b0f                  mov       ecx,[edi]
  004914ab  c1e00d                shl       eax,0Dh
  004914ae  f6840850010f0007      test      byte [eax+ecx+0F0150h],7
  004914b6  7504                  jne       short 004914BCh
  004914b8  660fbeea              movsx     bp,dl

LAB_004914bc:
  004914bc  46                    inc       esi
  004914bd  4b                    dec       ebx
  004914be  75c5                  jne       short 00491485h
  004914c0  e946010000            jmp       0049160Bh
  004914c5  8b3db4876000          mov       edi,[6087B4h]
  004914cb  bb04000000            mov       ebx,4
  004914d0  0fbfd6                movsx     edx,si
  004914d3  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004914d7:
  004914d7  8a16                  mov       dl,[esi]
  004914d9  0fbec2                movsx     eax,dl
  004914dc  8d0c80                lea       ecx,[eax+eax*4]
  004914df  c1e103                shl       ecx,3
  004914e2  2bc8                  sub       ecx,eax
  004914e4  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004914eb  f7c100000080          test      ecx,80000000h
  004914f1  741e                  je        short 00491511h
  004914f3  f7c100000050          test      ecx,50000000h
  004914f9  7516                  jne       short 00491511h
  004914fb  8b0f                  mov       ecx,[edi]
  004914fd  c1e00d                shl       eax,0Dh
  00491500  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  00491507  2407                  and       al,7
  00491509  3c01                  cmp       al,1
  0049150b  7504                  jne       short 00491511h
  0049150d  660fbeea              movsx     bp,dl

LAB_00491511:
  00491511  46                    inc       esi
  00491512  4b                    dec       ebx
  00491513  75c2                  jne       short 004914D7h
  00491515  e9f1000000            jmp       0049160Bh
  0049151a  8b3db4876000          mov       edi,[6087B4h]
  00491520  bb04000000            mov       ebx,4
  00491525  0fbfce                movsx     ecx,si
  00491528  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_0049152c:
  0049152c  8a16                  mov       dl,[esi]
  0049152e  0fbec2                movsx     eax,dl
  00491531  8d0c80                lea       ecx,[eax+eax*4]
  00491534  c1e103                shl       ecx,3
  00491537  2bc8                  sub       ecx,eax
  00491539  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  00491540  f7c100000080          test      ecx,80000000h
  00491546  741e                  je        short 00491566h
  00491548  f7c100000050          test      ecx,50000000h
  0049154e  7516                  jne       short 00491566h
  00491550  8b0f                  mov       ecx,[edi]
  00491552  c1e00d                shl       eax,0Dh
  00491555  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  0049155c  2407                  and       al,7
  0049155e  3c03                  cmp       al,3
  00491560  7504                  jne       short 00491566h
  00491562  660fbeea              movsx     bp,dl

LAB_00491566:
  00491566  46                    inc       esi
  00491567  4b                    dec       ebx
  00491568  75c2                  jne       short 0049152Ch
  0049156a  e99c000000            jmp       0049160Bh
  0049156f  8b3db4876000          mov       edi,[6087B4h]
  00491575  bb04000000            mov       ebx,4
  0049157a  0fbfce                movsx     ecx,si
  0049157d  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_00491581:
  00491581  8a16                  mov       dl,[esi]
  00491583  0fbec2                movsx     eax,dl
  00491586  8d0c80                lea       ecx,[eax+eax*4]
  00491589  c1e103                shl       ecx,3
  0049158c  2bc8                  sub       ecx,eax
  0049158e  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  00491595  f7c100000080          test      ecx,80000000h
  0049159b  741b                  je        short 004915B8h
  0049159d  f7c100000050          test      ecx,50000000h
  004915a3  7513                  jne       short 004915B8h
  004915a5  8b0f                  mov       ecx,[edi]
  004915a7  c1e00d                shl       eax,0Dh
  004915aa  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  004915b2  7504                  jne       short 004915B8h
  004915b4  660fbeea              movsx     bp,dl

LAB_004915b8:
  004915b8  46                    inc       esi
  004915b9  4b                    dec       ebx
  004915ba  75c5                  jne       short 00491581h
  004915bc  eb4d                  jmp       short 0049160Bh
  004915be  8b3db4876000          mov       edi,[6087B4h]
  004915c4  bb04000000            mov       ebx,4
  004915c9  0fbfd6                movsx     edx,si
  004915cc  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004915d0:
  004915d0  8a16                  mov       dl,[esi]
  004915d2  0fbec2                movsx     eax,dl
  004915d5  8d0c80                lea       ecx,[eax+eax*4]
  004915d8  c1e103                shl       ecx,3
  004915db  2bc8                  sub       ecx,eax
  004915dd  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004915e4  f7c100000080          test      ecx,80000000h
  004915ea  741b                  je        short 00491607h
  004915ec  f7c100000050          test      ecx,50000000h
  004915f2  7513                  jne       short 00491607h
  004915f4  8b0f                  mov       ecx,[edi]
  004915f6  c1e00d                shl       eax,0Dh
  004915f9  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  00491601  7404                  je        short 00491607h
  00491603  660fbeea              movsx     bp,dl

LAB_00491607:
  00491607  46                    inc       esi
  00491608  4b                    dec       ebx
  00491609  75c5                  jne       short 004915D0h

LAB_0049160b:
  0049160b  0fbfc5                movsx     eax,bp
  0049160e  8b4c2424              mov       ecx,[esp+24h]
  00491612  8d1480                lea       edx,[eax+eax*4]
  00491615  c1e203                shl       edx,3
  00491618  2bd0                  sub       edx,eax
  0049161a  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00491621  89416c                mov       [ecx+6Ch],eax

LAB_00491624:
  00491624  5f                    pop       edi
  00491625  5e                    pop       esi
  00491626  5d                    pop       ebp
  00491627  5b                    pop       ebx
  00491628  83c410                add       esp,10h
  0049162b  c3                    ret
  0049162c  49                    dec       ecx
  0049162d  134900                adc       ecx,[ecx]
  00491630  0b16                  or        edx,[esi]
  00491632  49                    dec       ecx
  00491633  008c134900fe13        add       [ebx+edx+13FE0049h],cl
  0049163a  49                    dec       ecx
  0049163b  007314                add       [ebx+14h],dh
  0049163e  49                    dec       ecx
  0049163f  00c5                  add       ch,al
  00491641  1449                  adc       al,49h
  00491643  001a                  add       [edx],bl
  00491645  1549006f15            adc       eax,156F0049h
  0049164a  49                    dec       ecx
  0049164b  00be15490081          add       [esi-7EFFB6EBh],bh
  00491651  ec                    in        al,dx
  00491652  8400                  test      [eax],al
  00491654  0000                  add       [eax],al
  00491656  53                    push      ebx
  00491657  55                    push      ebp
  00491658  56                    push      esi
  00491659  8bb42494000000        mov       esi,[esp+94h]
  00491660  33c0                  xor       eax,eax
  00491662  57                    push      edi
  00491663  8a4e41                mov       cl,[esi+41h]
  00491666  6689442430            mov       [esp+30h],ax
  0049166b  84c9                  test      cl,cl
  0049166d  66c7442432ceff        mov       word [esp+32h],0FFCEh
  00491674  66c74424343200        mov       word [esp+34h],32h
  0049167b  66c74424366400        mov       word [esp+36h],64h
  00491682  6689442438            mov       [esp+38h],ax
  00491687  66c744243a8200        mov       word [esp+3Ah],82h
  0049168e  bb01000000            mov       ebx,1
  00491693  750e                  jne       short 004916A3h
  00491695  50                    push      eax
  00491696  50                    push      eax
  00491697  56                    push      esi
  00491698  885e41                mov       [esi+41h],bl
  0049169b  e8f0fbffff            call      00491290h
  004916a0  83c40c                add       esp,0Ch

LAB_004916a3:
  004916a3  8b461c                mov       eax,[esi+1Ch]
  004916a6  8b4e20                mov       ecx,[esi+20h]
  004916a9  8b5624                mov       edx,[esi+24h]
  004916ac  8d6e1c                lea       ebp,[esi+1Ch]
  004916af  89442424              mov       [esp+24h],eax
  004916b3  8b4664                mov       eax,[esi+64h]
  004916b6  894c2428              mov       [esp+28h],ecx
  004916ba  8b4e60                mov       ecx,[esi+60h]
  004916bd  8954242c              mov       [esp+2Ch],edx
  004916c1  668b5632              mov       dx,[esi+32h]
  004916c5  89442444              mov       [esp+44h],eax
  004916c9  8b467c                mov       eax,[esi+7Ch]
  004916cc  894c2440              mov       [esp+40h],ecx
  004916d0  668b8e90000000        mov       cx,[esi+90h]
  004916d7  6689942482000000      mov       [esp+82h],dx
  004916df  8b9680000000          mov       edx,[esi+80h]
  004916e5  8944245c              mov       [esp+5Ch],eax
  004916e9  8b8684000000          mov       eax,[esi+84h]
  004916ef  66894c2448            mov       [esp+48h],cx
  004916f4  8b8e88000000          mov       ecx,[esi+88h]
  004916fa  8b7e6c                mov       edi,[esi+6Ch]
  004916fd  8954244c              mov       [esp+4Ch],edx
  00491701  8b968c000000          mov       edx,[esi+8Ch]
  00491707  8944243c              mov       [esp+3Ch],eax
  0049170b  894c2458              mov       [esp+58h],ecx
  0049170f  8d442460              lea       eax,[esp+60h]
  00491713  89542450              mov       [esp+50h],edx
  00491717  8d4c2468              lea       ecx,[esp+68h]
  0049171b  50                    push      eax
  0049171c  8d542468              lea       edx,[esp+68h]
  00491720  51                    push      ecx
  00491721  52                    push      edx
  00491722  56                    push      esi
  00491723  e858720a00            call      00538980h
  00491728  83c410                add       esp,10h
  0049172b  8d442454              lea       eax,[esp+54h]
  0049172f  8d4c241c              lea       ecx,[esp+1Ch]
  00491733  8d542418              lea       edx,[esp+18h]
  00491737  50                    push      eax
  00491738  51                    push      ecx
  00491739  8d44241c              lea       eax,[esp+1Ch]
  0049173d  52                    push      edx
  0049173e  8d4c                  (bad)
  (octet invalide — la plage demandee deborde probablement du code)
