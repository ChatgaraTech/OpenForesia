Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004f6380  section .text  offset fichier 0xf5780  924 octets

  004f6380  83ec10                sub       esp,10h
  004f6383  b002                  mov       al,2
  004f6385  53                    push      ebx
  004f6386  88442406              mov       [esp+6],al
  004f638a  88442409              mov       [esp+9],al
  004f638e  8844240c              mov       [esp+0Ch],al
  004f6392  88442413              mov       [esp+13h],al
  004f6396  8b442418              mov       eax,[esp+18h]
  004f639a  55                    push      ebp
  004f639b  56                    push      esi
  004f639c  33db                  xor       ebx,ebx
  004f639e  57                    push      edi
  004f639f  8b786c                mov       edi,[eax+6Ch]
  004f63a2  885c2410              mov       [esp+10h],bl
  004f63a6  c644241101            mov       byte [esp+11h],1
  004f63ab  c644241303            mov       byte [esp+13h],3
  004f63b0  c644241401            mov       byte [esp+14h],1
  004f63b5  c644241603            mov       byte [esp+16h],3
  004f63ba  885c2417              mov       [esp+17h],bl
  004f63be  c644241903            mov       byte [esp+19h],3
  004f63c3  885c241a              mov       [esp+1Ah],bl
  004f63c7  c644241b01            mov       byte [esp+1Bh],1
  004f63cc  c644241c03            mov       byte [esp+1Ch],3
  004f63d1  885c241d              mov       [esp+1Dh],bl
  004f63d5  c644241e01            mov       byte [esp+1Eh],1
  004f63da  e8610d0500            call      00547140h
  004f63df  99                    cdq
  004f63e0  33c2                  xor       eax,edx
  004f63e2  2bc2                  sub       eax,edx
  004f63e4  660fb66f08            movzx     bp,byte [edi+8]
  004f63e9  83e003                and       eax,3
  004f63ec  33c2                  xor       eax,edx
  004f63ee  8bf0                  mov       esi,eax
  004f63f0  2bf2                  sub       esi,edx
  004f63f2  e8490d0500            call      00547140h
  004f63f7  99                    cdq
  004f63f8  b90a000000            mov       ecx,0Ah
  004f63fd  f7f9                  idiv      ecx
  004f63ff  6683fa05              cmp       dx,5
  004f6403  0f8c0b030000          jl        near 004F6714h
  004f6409  e8320d0500            call      00547140h
  004f640e  99                    cdq
  004f640f  b90a000000            mov       ecx,0Ah
  004f6414  f7f9                  idiv      ecx
  004f6416  6683fa05              cmp       dx,5
  004f641a  7f05                  jg        short 004F6421h
  004f641c  83c8ff                or        eax,0FFFFFFFFh
  004f641f  eb04                  jmp       short 004F6425h

LAB_004f6421:
  004f6421  8b442428              mov       eax,[esp+28h]

LAB_004f6425:
  004f6425  0fbfc0                movsx     eax,ax
  004f6428  40                    inc       eax
  004f6429  83f808                cmp       eax,8
  004f642c  0f87c9020000          ja        near 004F66FBh
  004f6432  ff24851c674f00        jmp       dword [eax*4+4F671Ch]
  004f6439  33f6                  xor       esi,esi

LAB_004f643b:
  004f643b  e8000d0500            call      00547140h
  004f6440  99                    cdq
  004f6441  33c2                  xor       eax,edx
  004f6443  2bc2                  sub       eax,edx
  004f6445  83e003                and       eax,3
  004f6448  33c2                  xor       eax,edx
  004f644a  2bc2                  sub       eax,edx
  004f644c  8d1480                lea       edx,[eax+eax*4]
  004f644f  c1e203                shl       edx,3
  004f6452  2bd0                  sub       edx,eax
  004f6454  8b0c95c0935e00        mov       ecx,[edx*4+5E93C0h]
  004f645b  f7c100000080          test      ecx,80000000h
  004f6461  740f                  je        short 004F6472h
  004f6463  f7c100000050          test      ecx,50000000h
  004f6469  7507                  jne       short 004F6472h
  004f646b  be01000000            mov       esi,1
  004f6470  8be8                  mov       ebp,eax

LAB_004f6472:
  004f6472  663bf3                cmp       si,bx
  004f6475  74c4                  je        short 004F643Bh
  004f6477  e97f020000            jmp       004F66FBh
  004f647c  8b3db4876000          mov       edi,[6087B4h]
  004f6482  33ed                  xor       ebp,ebp
  004f6484  0fbfc6                movsx     eax,si
  004f6487  c744242804000000      mov       dword [esp+28h],4
  004f648f  8d748410              lea       esi,[esp+eax*4+10h]

LAB_004f6493:
  004f6493  8a1e                  mov       bl,[esi]
  004f6495  0fbec3                movsx     eax,bl
  004f6498  8d0c80                lea       ecx,[eax+eax*4]
  004f649b  c1e103                shl       ecx,3
  004f649e  2bc8                  sub       ecx,eax
  004f64a0  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004f64a7  f7c100000080          test      ecx,80000000h
  004f64ad  742e                  je        short 004F64DDh
  004f64af  f7c100000050          test      ecx,50000000h
  004f64b5  7526                  jne       short 004F64DDh
  004f64b7  8d4878                lea       ecx,[eax+78h]
  004f64ba  8b07                  mov       eax,[edi]
  004f64bc  c1e10d                shl       ecx,0Dh
  004f64bf  03c8                  add       ecx,eax
  004f64c1  0fbf8148010000        movsx     eax,word [ecx+148h]
  004f64c8  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  004f64cf  c1e00c                shl       eax,0Ch
  004f64d2  99                    cdq
  004f64d3  f7f9                  idiv      ecx
  004f64d5  85c0                  test      eax,eax
  004f64d7  7e04                  jle       short 004F64DDh
  004f64d9  660fbeeb              movsx     bp,bl

LAB_004f64dd:
  004f64dd  8b442428              mov       eax,[esp+28h]
  004f64e1  46                    inc       esi
  004f64e2  48                    dec       eax
  004f64e3  89442428              mov       [esp+28h],eax
  004f64e7  75aa                  jne       short 004F6493h
  004f64e9  e90d020000            jmp       004F66FBh
  004f64ee  8b3db4876000          mov       edi,[6087B4h]
  004f64f4  33ed                  xor       ebp,ebp
  004f64f6  0fbfd6                movsx     edx,si
  004f64f9  c744242804000000      mov       dword [esp+28h],4
  004f6501  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004f6505:
  004f6505  8a1e                  mov       bl,[esi]
  004f6507  0fbec3                movsx     eax,bl
  004f650a  8d0c80                lea       ecx,[eax+eax*4]
  004f650d  c1e103                shl       ecx,3
  004f6510  2bc8                  sub       ecx,eax
  004f6512  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004f6519  f7c100000080          test      ecx,80000000h
  004f651f  7431                  je        short 004F6552h
  004f6521  f7c100000050          test      ecx,50000000h
  004f6527  7529                  jne       short 004F6552h
  004f6529  8d4878                lea       ecx,[eax+78h]
  004f652c  8b07                  mov       eax,[edi]
  004f652e  c1e10d                shl       ecx,0Dh
  004f6531  03c8                  add       ecx,eax
  004f6533  0fbf8148010000        movsx     eax,word [ecx+148h]
  004f653a  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  004f6541  c1e00c                shl       eax,0Ch
  004f6544  99                    cdq
  004f6545  f7f9                  idiv      ecx
  004f6547  3d10270000            cmp       eax,2710h
  004f654c  7d04                  jge       short 004F6552h
  004f654e  660fbeeb              movsx     bp,bl

LAB_004f6552:
  004f6552  8b442428              mov       eax,[esp+28h]
  004f6556  46                    inc       esi
  004f6557  48                    dec       eax
  004f6558  89442428              mov       [esp+28h],eax
  004f655c  75a7                  jne       short 004F6505h
  004f655e  e998010000            jmp       004F66FBh
  004f6563  8b3db4876000          mov       edi,[6087B4h]
  004f6569  bb04000000            mov       ebx,4
  004f656e  0fbfd6                movsx     edx,si
  004f6571  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004f6575:
  004f6575  8a16                  mov       dl,[esi]
  004f6577  0fbec2                movsx     eax,dl
  004f657a  8d0c80                lea       ecx,[eax+eax*4]
  004f657d  c1e103                shl       ecx,3
  004f6580  2bc8                  sub       ecx,eax
  004f6582  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004f6589  f7c100000080          test      ecx,80000000h
  004f658f  741b                  je        short 004F65ACh
  004f6591  f7c100000050          test      ecx,50000000h
  004f6597  7513                  jne       short 004F65ACh
  004f6599  8b0f                  mov       ecx,[edi]
  004f659b  c1e00d                shl       eax,0Dh
  004f659e  f6840850010f0007      test      byte [eax+ecx+0F0150h],7
  004f65a6  7504                  jne       short 004F65ACh
  004f65a8  660fbeea              movsx     bp,dl

LAB_004f65ac:
  004f65ac  46                    inc       esi
  004f65ad  4b                    dec       ebx
  004f65ae  75c5                  jne       short 004F6575h
  004f65b0  e946010000            jmp       004F66FBh
  004f65b5  8b3db4876000          mov       edi,[6087B4h]
  004f65bb  bb04000000            mov       ebx,4
  004f65c0  0fbfd6                movsx     edx,si
  004f65c3  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004f65c7:
  004f65c7  8a16                  mov       dl,[esi]
  004f65c9  0fbec2                movsx     eax,dl
  004f65cc  8d0c80                lea       ecx,[eax+eax*4]
  004f65cf  c1e103                shl       ecx,3
  004f65d2  2bc8                  sub       ecx,eax
  004f65d4  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004f65db  f7c100000080          test      ecx,80000000h
  004f65e1  741e                  je        short 004F6601h
  004f65e3  f7c100000050          test      ecx,50000000h
  004f65e9  7516                  jne       short 004F6601h
  004f65eb  8b0f                  mov       ecx,[edi]
  004f65ed  c1e00d                shl       eax,0Dh
  004f65f0  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  004f65f7  2407                  and       al,7
  004f65f9  3c01                  cmp       al,1
  004f65fb  7504                  jne       short 004F6601h
  004f65fd  660fbeea              movsx     bp,dl

LAB_004f6601:
  004f6601  46                    inc       esi
  004f6602  4b                    dec       ebx
  004f6603  75c2                  jne       short 004F65C7h
  004f6605  e9f1000000            jmp       004F66FBh
  004f660a  8b3db4876000          mov       edi,[6087B4h]
  004f6610  bb04000000            mov       ebx,4
  004f6615  0fbfce                movsx     ecx,si
  004f6618  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_004f661c:
  004f661c  8a16                  mov       dl,[esi]
  004f661e  0fbec2                movsx     eax,dl
  004f6621  8d0c80                lea       ecx,[eax+eax*4]
  004f6624  c1e103                shl       ecx,3
  004f6627  2bc8                  sub       ecx,eax
  004f6629  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004f6630  f7c100000080          test      ecx,80000000h
  004f6636  741e                  je        short 004F6656h
  004f6638  f7c100000050          test      ecx,50000000h
  004f663e  7516                  jne       short 004F6656h
  004f6640  8b0f                  mov       ecx,[edi]
  004f6642  c1e00d                shl       eax,0Dh
  004f6645  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  004f664c  2407                  and       al,7
  004f664e  3c03                  cmp       al,3
  004f6650  7504                  jne       short 004F6656h
  004f6652  660fbeea              movsx     bp,dl

LAB_004f6656:
  004f6656  46                    inc       esi
  004f6657  4b                    dec       ebx
  004f6658  75c2                  jne       short 004F661Ch
  004f665a  e99c000000            jmp       004F66FBh
  004f665f  8b3db4876000          mov       edi,[6087B4h]
  004f6665  bb04000000            mov       ebx,4
  004f666a  0fbfce                movsx     ecx,si
  004f666d  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_004f6671:
  004f6671  8a16                  mov       dl,[esi]
  004f6673  0fbec2                movsx     eax,dl
  004f6676  8d0c80                lea       ecx,[eax+eax*4]
  004f6679  c1e103                shl       ecx,3
  004f667c  2bc8                  sub       ecx,eax
  004f667e  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004f6685  f7c100000080          test      ecx,80000000h
  004f668b  741b                  je        short 004F66A8h
  004f668d  f7c100000050          test      ecx,50000000h
  004f6693  7513                  jne       short 004F66A8h
  004f6695  8b0f                  mov       ecx,[edi]
  004f6697  c1e00d                shl       eax,0Dh
  004f669a  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  004f66a2  7504                  jne       short 004F66A8h
  004f66a4  660fbeea              movsx     bp,dl

LAB_004f66a8:
  004f66a8  46                    inc       esi
  004f66a9  4b                    dec       ebx
  004f66aa  75c5                  jne       short 004F6671h
  004f66ac  eb4d                  jmp       short 004F66FBh
  004f66ae  8b3db4876000          mov       edi,[6087B4h]
  004f66b4  bb04000000            mov       ebx,4
  004f66b9  0fbfd6                movsx     edx,si
  004f66bc  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004f66c0:
  004f66c0  8a16                  mov       dl,[esi]
  004f66c2  0fbec2                movsx     eax,dl
  004f66c5  8d0c80                lea       ecx,[eax+eax*4]
  004f66c8  c1e103                shl       ecx,3
  004f66cb  2bc8                  sub       ecx,eax
  004f66cd  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004f66d4  f7c100000080          test      ecx,80000000h
  004f66da  741b                  je        short 004F66F7h
  004f66dc  f7c100000050          test      ecx,50000000h
  004f66e2  7513                  jne       short 004F66F7h
  004f66e4  8b0f                  mov       ecx,[edi]
  004f66e6  c1e00d                shl       eax,0Dh
  004f66e9  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  004f66f1  7404                  je        short 004F66F7h
  004f66f3  660fbeea              movsx     bp,dl

LAB_004f66f7:
  004f66f7  46                    inc       esi
  004f66f8  4b                    dec       ebx
  004f66f9  75c5                  jne       short 004F66C0h

LAB_004f66fb:
  004f66fb  0fbfc5                movsx     eax,bp
  004f66fe  8b4c2424              mov       ecx,[esp+24h]
  004f6702  8d1480                lea       edx,[eax+eax*4]
  004f6705  c1e203                shl       edx,3
  004f6708  2bd0                  sub       edx,eax
  004f670a  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  004f6711  89416c                mov       [ecx+6Ch],eax

LAB_004f6714:
  004f6714  5f                    pop       edi
  004f6715  5e                    pop       esi
  004f6716  5d                    pop       ebp
  004f6717  5b                    pop       ebx
  004f6718  83c410                add       esp,10h
  004f671b  c3                    ret

20 cible(s) de saut interne : 0x004f6421, 0x004f6425, 0x004f643b, 0x004f6472, 0x004f6493, 0x004f64dd, 0x004f6505, 0x004f6552, 0x004f6575, 0x004f65ac, 0x004f65c7, 0x004f6601, 0x004f661c, 0x004f6656, 0x004f6671, 0x004f66a8, 0x004f66c0, 0x004f66f7, 0x004f66fb, 0x004f6714
