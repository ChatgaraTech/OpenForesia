
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00406680  section .text  offset fichier 0x5a80  400 octets

  00406680  53                    push      ebx
  00406681  56                    push      esi
  00406682  8b74240c              mov       esi,[esp+0Ch]
  00406686  33db                  xor       ebx,ebx
  00406688  66395e10              cmp       [esi+10h],bx
  0040668c  0f85c8000000          jne       near 0040675Ah
  00406692  66c746100100          mov       word [esi+10h],1
  00406698  c7463cb0f15400        mov       dword [esi+3Ch],54F1B0h
  0040669f  885e40                mov       [esi+40h],bl
  004066a2  e8990a1400            call      00547140h
  004066a7  99                    cdq
  004066a8  33c2                  xor       eax,edx
  004066aa  2bc2                  sub       eax,edx
  004066ac  25ff0f0000            and       eax,0FFFh
  004066b1  33c2                  xor       eax,edx
  004066b3  2bc2                  sub       eax,edx
  004066b5  66894632              mov       [esi+32h],ax
  004066b9  e8820a1400            call      00547140h
  004066be  99                    cdq
  004066bf  b9f8020000            mov       ecx,2F8h
  004066c4  f7f9                  idiv      ecx
  004066c6  8b4620                mov       eax,[esi+20h]
  004066c9  81ea7c010000          sub       edx,17Ch
  004066cf  c1e20c                shl       edx,0Ch
  004066d2  03c2                  add       eax,edx
  004066d4  8b561c                mov       edx,[esi+1Ch]
  004066d7  894620                mov       [esi+20h],eax
  004066da  894654                mov       [esi+54h],eax
  004066dd  8b4624                mov       eax,[esi+24h]
  004066e0  895650                mov       [esi+50h],edx
  004066e3  894658                mov       [esi+58h],eax
  004066e6  e8550a1400            call      00547140h
  004066eb  99                    cdq
  004066ec  b964000000            mov       ecx,64h
  004066f1  f7f9                  idiv      ecx
  004066f3  81c22c010000          add       edx,12Ch
  004066f9  6689969a000000        mov       [esi+9Ah],dx
  00406700  e83b0a1400            call      00547140h
  00406705  99                    cdq
  00406706  b90a000000            mov       ecx,0Ah
  0040670b  885e38                mov       [esi+38h],bl
  0040670e  f7f9                  idiv      ecx
  00406710  885e39                mov       [esi+39h],bl
  00406713  885e3a                mov       [esi+3Ah],bl
  00406716  83c205                add       edx,5
  00406719  89565c                mov       [esi+5Ch],edx
  0040671c  e81f0a1400            call      00547140h
  00406721  99                    cdq
  00406722  33c2                  xor       eax,edx
  00406724  c6460a09              mov       byte [esi+0Ah],9
  00406728  2bc2                  sub       eax,edx
  0040672a  885e0b                mov       [esi+0Bh],bl
  0040672d  25ff0f0000            and       eax,0FFFh
  00406732  33c2                  xor       eax,edx
  00406734  2bc2                  sub       eax,edx
  00406736  66894612              mov       [esi+12h],ax
  0040673a  e8010a1400            call      00547140h
  0040673f  99                    cdq
  00406740  33c2                  xor       eax,edx
  00406742  2bc2                  sub       eax,edx
  00406744  25ff030000            and       eax,3FFh
  00406749  33c2                  xor       eax,edx
  0040674b  2bc2                  sub       eax,edx
  0040674d  0500100000            add       eax,1000h
  00406752  66894628              mov       [esi+28h],ax
  00406756  6689462a              mov       [esi+2Ah],ax

LAB_0040675a:
  0040675a  66837e1001            cmp       word [esi+10h],1
  0040675f  0f859d000000          jne       near 00406802h
  00406765  6a14                  push      14h
  00406767  6a14                  push      14h
  00406769  6a14                  push      14h
  0040676b  56                    push      esi
  0040676c  e86fe6ffff            call      00404DE0h
  00406771  8a4e38                mov       cl,[esi+38h]
  00406774  b080                  mov       al,80h
  00406776  83c410                add       esp,10h
  00406779  3ac8                  cmp       cl,al
  0040677b  7209                  jb        short 00406786h
  0040677d  884638                mov       [esi+38h],al
  00406780  884639                mov       [esi+39h],al
  00406783  88463a                mov       [esi+3Ah],al

LAB_00406786:
  00406786  0fbf5632              movsx     edx,word [esi+32h]
  0040678a  52                    push      edx
  0040678b  e88cf21300            call      00545A1Ch
  00406790  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00406797  0fafc1                imul      eax,ecx
  0040679a  0fbf5632              movsx     edx,word [esi+32h]
  0040679e  8b4e50                mov       ecx,[esi+50h]
  004067a1  52                    push      edx
  004067a2  03c1                  add       eax,ecx
  004067a4  89461c                mov       [esi+1Ch],eax
  004067a7  e86af21300            call      00545A16h
  004067ac  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  004067b3  0fafc1                imul      eax,ecx
  004067b6  034658                add       eax,[esi+58h]
  004067b9  0fbf5612              movsx     edx,word [esi+12h]
  004067bd  894624                mov       [esi+24h],eax
  004067c0  0fbf4632              movsx     eax,word [esi+32h]
  004067c4  03d0                  add       edx,eax
  004067c6  52                    push      edx
  004067c7  e850f21300            call      00545A1Ch
  004067cc  8b5654                mov       edx,[esi+54h]
  004067cf  8d0480                lea       eax,[eax+eax*4]
  004067d2  8d0c80                lea       ecx,[eax+eax*4]
  004067d5  8d044a                lea       eax,[edx+ecx*2]
  004067d8  668b4e5c              mov       cx,[esi+5Ch]
  004067dc  666bc914              imul      cx,14h
  004067e0  668b565c              mov       dx,[esi+5Ch]
  004067e4  66014e32              add       [esi+32h],cx
  004067e8  6629969a000000        sub       [esi+9Ah],dx
  004067ef  6683be9a00000032      cmp       word [esi+9Ah],32h
  004067f7  894620                mov       [esi+20h],eax
  004067fa  7d06                  jge       short 00406802h
  004067fc  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00406802:
  00406802  5e                    pop       esi
  00406803  5b                    pop       ebx
  00406804  c3                    ret
  00406805  90                    nop
  00406806  90                    nop
  00406807  90                    nop
  00406808  90                    nop
  00406809  90                    nop
  0040680a  90                    nop
  0040680b  90                    nop
  0040680c  90                    nop
  0040680d  90                    nop
  0040680e  90                    nop
  0040680f  90                    nop

3 cible(s) de saut interne : 0x0040675a, 0x00406786, 0x00406802


