Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040f650  section .text  offset fichier 0xea50  400 octets

  0040f650  53                    push      ebx
  0040f651  55                    push      ebp
  0040f652  56                    push      esi
  0040f653  8b742410              mov       esi,[esp+10h]
  0040f657  57                    push      edi
  0040f658  66837e1000            cmp       word [esi+10h],0
  0040f65d  0f851e010000          jne       near 0040F781h
  0040f663  66c7462e1400          mov       word [esi+2Eh],14h
  0040f669  c7463c58f75400        mov       dword [esi+3Ch],54F758h
  0040f670  e8cb7a1300            call      00547140h
  0040f675  99                    cdq
  0040f676  b958020000            mov       ecx,258h
  0040f67b  f7f9                  idiv      ecx
  0040f67d  81c2581b0000          add       edx,1B58h
  0040f683  66895628              mov       [esi+28h],dx
  0040f687  e8b47a1300            call      00547140h
  0040f68c  99                    cdq
  0040f68d  b958020000            mov       ecx,258h
  0040f692  f7f9                  idiv      ecx
  0040f694  81c2581b0000          add       edx,1B58h
  0040f69a  6689562a              mov       [esi+2Ah],dx
  0040f69e  0fbf5632              movsx     edx,word [esi+32h]
  0040f6a2  81c200040000          add       edx,400h
  0040f6a8  52                    push      edx
  0040f6a9  e86e631300            call      00545A1Ch
  0040f6ae  8d1c80                lea       ebx,[eax+eax*4]
  0040f6b1  0fbf4632              movsx     eax,word [esi+32h]
  0040f6b5  0500040000            add       eax,400h
  0040f6ba  50                    push      eax
  0040f6bb  c1e305                shl       ebx,5
  0040f6be  e853631300            call      00545A16h
  0040f6c3  0fbf4e32              movsx     ecx,word [esi+32h]
  0040f6c7  8d0480                lea       eax,[eax+eax*4]
  0040f6ca  81c1000c0000          add       ecx,0C00h
  0040f6d0  c1e005                shl       eax,5
  0040f6d3  51                    push      ecx
  0040f6d4  89442418              mov       [esp+18h],eax
  0040f6d8  e83f631300            call      00545A1Ch
  0040f6dd  0fbf5632              movsx     edx,word [esi+32h]
  0040f6e1  81c2000c0000          add       edx,0C00h
  0040f6e7  8d3c80                lea       edi,[eax+eax*4]
  0040f6ea  52                    push      edx
  0040f6eb  c1e705                shl       edi,5
  0040f6ee  e823631300            call      00545A16h
  0040f6f3  8b5624                mov       edx,[esi+24h]
  0040f6f6  8d2c80                lea       ebp,[eax+eax*4]
  0040f6f9  8b461c                mov       eax,[esi+1Ch]
  0040f6fc  c1e505                shl       ebp,5
  0040f6ff  2bc7                  sub       eax,edi
  0040f701  2bd5                  sub       edx,ebp
  0040f703  89461c                mov       [esi+1Ch],eax
  0040f706  895624                mov       [esi+24h],edx
  0040f709  e8327a1300            call      00547140h
  0040f70e  99                    cdq
  0040f70f  33c2                  xor       eax,edx
  0040f711  c6460a09              mov       byte [esi+0Ah],9
  0040f715  2bc2                  sub       eax,edx
  0040f717  c6460b00              mov       byte [esi+0Bh],0
  0040f71b  83e007                and       eax,7
  0040f71e  66c746100100          mov       word [esi+10h],1
  0040f724  33c2                  xor       eax,edx
  0040f726  2bc2                  sub       eax,edx
  0040f728  0fbfc8                movsx     ecx,ax
  0040f72b  8bc3                  mov       eax,ebx
  0040f72d  2bc7                  sub       eax,edi
  0040f72f  8b7e1c                mov       edi,[esi+1Ch]
  0040f732  99                    cdq
  0040f733  83e207                and       edx,7
  0040f736  03c2                  add       eax,edx
  0040f738  c1f803                sar       eax,3
  0040f73b  f7d8                  neg       eax
  0040f73d  0fafc1                imul      eax,ecx
  0040f740  03f8                  add       edi,eax
  0040f742  8b442414              mov       eax,[esp+14h]
  0040f746  2bc5                  sub       eax,ebp
  0040f748  897e1c                mov       [esi+1Ch],edi
  0040f74b  99                    cdq
  0040f74c  83e207                and       edx,7
  0040f74f  03c2                  add       eax,edx
  0040f751  c1f803                sar       eax,3
  0040f754  f7d8                  neg       eax
  0040f756  0fafc1                imul      eax,ecx
  0040f759  014624                add       [esi+24h],eax
  0040f75c  e8df791300            call      00547140h
  0040f761  99                    cdq
  0040f762  b905000000            mov       ecx,5
  0040f767  f7f9                  idiv      ecx
  0040f769  33c0                  xor       eax,eax
  0040f76b  66894614              mov       [esi+14h],ax
  0040f76f  66894616              mov       [esi+16h],ax
  0040f773  884640                mov       [esi+40h],al
  0040f776  8b06                  mov       eax,[esi]
  0040f778  80cc40                or        ah,40h
  0040f77b  8906                  mov       [esi],eax
  0040f77d  66895612              mov       [esi+12h],dx

LAB_0040f781:
  0040f781  66837e1001            cmp       word [esi+10h],1
  0040f786  b802000000            mov       eax,2
  0040f78b  7516                  jne       short 0040F7A3h
  0040f78d  66ff4e12              dec       word [esi+12h]
  0040f791  66837e1200            cmp       word [esi+12h],0
  0040f796  7d0b                  jge       short 0040F7A3h
  0040f798  8b0e                  mov       ecx,[esi]
  0040f79a  66894610              mov       [esi+10h],ax
  0040f79e  80e5bf                and       ch,0BFh
  0040f7a1  890e                  mov       [esi],ecx

LAB_0040f7a3:
  0040f7a3  66394610              cmp       [esi+10h],ax
  0040f7a7  7537                  jne       short 0040F7E0h
  0040f7a9  66ff4614              inc       word [esi+14h]
  0040f7ad  e88e791300            call      00547140h
  0040f7b2  99                    cdq
  0040f7b3  33c2                  xor       eax,edx
  0040f7b5  2bc2                  sub       eax,edx
  0040f7b7  83e003                and       eax,3
  0040f7ba  33c2                  xor       eax,edx
  0040f7bc  2bc2                  sub       eax,edx
  0040f7be  884640                mov       [esi+40h],al
  0040f7c1  e87a791300            call      00547140h
  0040f7c6  99                    cdq
  0040f7c7  b914000000            mov       ecx,14h
  0040f7cc  f7f9                  idiv      ecx
  0040f7ce  66394e14              cmp       [esi+14h],cx
  0040f7d2  7406                  je        short 0040F7DAh
  0040f7d4  6683fa01              cmp       dx,1
  0040f7d8  7506                  jne       short 0040F7E0h

LAB_0040f7da:
  0040f7da  8126ffffff7f          and       dword [esi],7FFFFFFFh

3 cible(s) de saut interne : 0x0040f781, 0x0040f7a3, 0x0040f7da
