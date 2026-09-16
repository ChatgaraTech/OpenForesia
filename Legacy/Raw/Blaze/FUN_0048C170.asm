Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048c170  section .text  offset fichier 0x8b570  960 octets

  0048c170  83ec10                sub       esp,10h
  0048c173  b002                  mov       al,2
  0048c175  53                    push      ebx
  0048c176  88442406              mov       [esp+6],al
  0048c17a  88442409              mov       [esp+9],al
  0048c17e  8844240c              mov       [esp+0Ch],al
  0048c182  88442413              mov       [esp+13h],al
  0048c186  8b442418              mov       eax,[esp+18h]
  0048c18a  55                    push      ebp
  0048c18b  56                    push      esi
  0048c18c  33db                  xor       ebx,ebx
  0048c18e  57                    push      edi
  0048c18f  8b786c                mov       edi,[eax+6Ch]
  0048c192  885c2410              mov       [esp+10h],bl
  0048c196  c644241101            mov       byte [esp+11h],1
  0048c19b  c644241303            mov       byte [esp+13h],3
  0048c1a0  c644241401            mov       byte [esp+14h],1
  0048c1a5  c644241603            mov       byte [esp+16h],3
  0048c1aa  885c2417              mov       [esp+17h],bl
  0048c1ae  c644241903            mov       byte [esp+19h],3
  0048c1b3  885c241a              mov       [esp+1Ah],bl
  0048c1b7  c644241b01            mov       byte [esp+1Bh],1
  0048c1bc  c644241c03            mov       byte [esp+1Ch],3
  0048c1c1  885c241d              mov       [esp+1Dh],bl
  0048c1c5  c644241e01            mov       byte [esp+1Eh],1
  0048c1ca  e871af0b00            call      00547140h
  0048c1cf  99                    cdq
  0048c1d0  33c2                  xor       eax,edx
  0048c1d2  2bc2                  sub       eax,edx
  0048c1d4  660fb66f08            movzx     bp,byte [edi+8]
  0048c1d9  83e003                and       eax,3
  0048c1dc  33c2                  xor       eax,edx
  0048c1de  8bf0                  mov       esi,eax
  0048c1e0  2bf2                  sub       esi,edx
  0048c1e2  e859af0b00            call      00547140h
  0048c1e7  99                    cdq
  0048c1e8  b90a000000            mov       ecx,0Ah
  0048c1ed  f7f9                  idiv      ecx
  0048c1ef  6683fa05              cmp       dx,5
  0048c1f3  0f8c0b030000          jl        near 0048C504h
  0048c1f9  e842af0b00            call      00547140h
  0048c1fe  99                    cdq
  0048c1ff  b90a000000            mov       ecx,0Ah
  0048c204  f7f9                  idiv      ecx
  0048c206  6683fa05              cmp       dx,5
  0048c20a  7f05                  jg        short 0048C211h
  0048c20c  83c8ff                or        eax,0FFFFFFFFh
  0048c20f  eb04                  jmp       short 0048C215h

LAB_0048c211:
  0048c211  8b442428              mov       eax,[esp+28h]

LAB_0048c215:
  0048c215  0fbfc0                movsx     eax,ax
  0048c218  40                    inc       eax
  0048c219  83f808                cmp       eax,8
  0048c21c  0f87c9020000          ja        near 0048C4EBh
  0048c222  ff24850cc54800        jmp       dword [eax*4+48C50Ch]
  0048c229  33f6                  xor       esi,esi

LAB_0048c22b:
  0048c22b  e810af0b00            call      00547140h
  0048c230  99                    cdq
  0048c231  33c2                  xor       eax,edx
  0048c233  2bc2                  sub       eax,edx
  0048c235  83e003                and       eax,3
  0048c238  33c2                  xor       eax,edx
  0048c23a  2bc2                  sub       eax,edx
  0048c23c  8d1480                lea       edx,[eax+eax*4]
  0048c23f  c1e203                shl       edx,3
  0048c242  2bd0                  sub       edx,eax
  0048c244  8b0c95c0935e00        mov       ecx,[edx*4+5E93C0h]
  0048c24b  f7c100000080          test      ecx,80000000h
  0048c251  740f                  je        short 0048C262h
  0048c253  f7c100000050          test      ecx,50000000h
  0048c259  7507                  jne       short 0048C262h
  0048c25b  be01000000            mov       esi,1
  0048c260  8be8                  mov       ebp,eax

LAB_0048c262:
  0048c262  663bf3                cmp       si,bx
  0048c265  74c4                  je        short 0048C22Bh
  0048c267  e97f020000            jmp       0048C4EBh
  0048c26c  8b3db4876000          mov       edi,[6087B4h]
  0048c272  33ed                  xor       ebp,ebp
  0048c274  0fbfc6                movsx     eax,si
  0048c277  c744242804000000      mov       dword [esp+28h],4
  0048c27f  8d748410              lea       esi,[esp+eax*4+10h]

LAB_0048c283:
  0048c283  8a1e                  mov       bl,[esi]
  0048c285  0fbec3                movsx     eax,bl
  0048c288  8d0c80                lea       ecx,[eax+eax*4]
  0048c28b  c1e103                shl       ecx,3
  0048c28e  2bc8                  sub       ecx,eax
  0048c290  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0048c297  f7c100000080          test      ecx,80000000h
  0048c29d  742e                  je        short 0048C2CDh
  0048c29f  f7c100000050          test      ecx,50000000h
  0048c2a5  7526                  jne       short 0048C2CDh
  0048c2a7  8d4878                lea       ecx,[eax+78h]
  0048c2aa  8b07                  mov       eax,[edi]
  0048c2ac  c1e10d                shl       ecx,0Dh
  0048c2af  03c8                  add       ecx,eax
  0048c2b1  0fbf8148010000        movsx     eax,word [ecx+148h]
  0048c2b8  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  0048c2bf  c1e00c                shl       eax,0Ch
  0048c2c2  99                    cdq
  0048c2c3  f7f9                  idiv      ecx
  0048c2c5  85c0                  test      eax,eax
  0048c2c7  7e04                  jle       short 0048C2CDh
  0048c2c9  660fbeeb              movsx     bp,bl

LAB_0048c2cd:
  0048c2cd  8b442428              mov       eax,[esp+28h]
  0048c2d1  46                    inc       esi
  0048c2d2  48                    dec       eax
  0048c2d3  89442428              mov       [esp+28h],eax
  0048c2d7  75aa                  jne       short 0048C283h
  0048c2d9  e90d020000            jmp       0048C4EBh
  0048c2de  8b3db4876000          mov       edi,[6087B4h]
  0048c2e4  33ed                  xor       ebp,ebp
  0048c2e6  0fbfd6                movsx     edx,si
  0048c2e9  c744242804000000      mov       dword [esp+28h],4
  0048c2f1  8d749410              lea       esi,[esp+edx*4+10h]

LAB_0048c2f5:
  0048c2f5  8a1e                  mov       bl,[esi]
  0048c2f7  0fbec3                movsx     eax,bl
  0048c2fa  8d0c80                lea       ecx,[eax+eax*4]
  0048c2fd  c1e103                shl       ecx,3
  0048c300  2bc8                  sub       ecx,eax
  0048c302  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0048c309  f7c100000080          test      ecx,80000000h
  0048c30f  7431                  je        short 0048C342h
  0048c311  f7c100000050          test      ecx,50000000h
  0048c317  7529                  jne       short 0048C342h
  0048c319  8d4878                lea       ecx,[eax+78h]
  0048c31c  8b07                  mov       eax,[edi]
  0048c31e  c1e10d                shl       ecx,0Dh
  0048c321  03c8                  add       ecx,eax
  0048c323  0fbf8148010000        movsx     eax,word [ecx+148h]
  0048c32a  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  0048c331  c1e00c                shl       eax,0Ch
  0048c334  99                    cdq
  0048c335  f7f9                  idiv      ecx
  0048c337  3d10270000            cmp       eax,2710h
  0048c33c  7d04                  jge       short 0048C342h
  0048c33e  660fbeeb              movsx     bp,bl

LAB_0048c342:
  0048c342  8b442428              mov       eax,[esp+28h]
  0048c346  46                    inc       esi
  0048c347  48                    dec       eax
  0048c348  89442428              mov       [esp+28h],eax
  0048c34c  75a7                  jne       short 0048C2F5h
  0048c34e  e998010000            jmp       0048C4EBh
  0048c353  8b3db4876000          mov       edi,[6087B4h]
  0048c359  bb04000000            mov       ebx,4
  0048c35e  0fbfd6                movsx     edx,si
  0048c361  8d749410              lea       esi,[esp+edx*4+10h]

LAB_0048c365:
  0048c365  8a16                  mov       dl,[esi]
  0048c367  0fbec2                movsx     eax,dl
  0048c36a  8d0c80                lea       ecx,[eax+eax*4]
  0048c36d  c1e103                shl       ecx,3
  0048c370  2bc8                  sub       ecx,eax
  0048c372  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0048c379  f7c100000080          test      ecx,80000000h
  0048c37f  741b                  je        short 0048C39Ch
  0048c381  f7c100000050          test      ecx,50000000h
  0048c387  7513                  jne       short 0048C39Ch
  0048c389  8b0f                  mov       ecx,[edi]
  0048c38b  c1e00d                shl       eax,0Dh
  0048c38e  f6840850010f0007      test      byte [eax+ecx+0F0150h],7
  0048c396  7504                  jne       short 0048C39Ch
  0048c398  660fbeea              movsx     bp,dl

LAB_0048c39c:
  0048c39c  46                    inc       esi
  0048c39d  4b                    dec       ebx
  0048c39e  75c5                  jne       short 0048C365h
  0048c3a0  e946010000            jmp       0048C4EBh
  0048c3a5  8b3db4876000          mov       edi,[6087B4h]
  0048c3ab  bb04000000            mov       ebx,4
  0048c3b0  0fbfd6                movsx     edx,si
  0048c3b3  8d749410              lea       esi,[esp+edx*4+10h]

LAB_0048c3b7:
  0048c3b7  8a16                  mov       dl,[esi]
  0048c3b9  0fbec2                movsx     eax,dl
  0048c3bc  8d0c80                lea       ecx,[eax+eax*4]
  0048c3bf  c1e103                shl       ecx,3
  0048c3c2  2bc8                  sub       ecx,eax
  0048c3c4  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0048c3cb  f7c100000080          test      ecx,80000000h
  0048c3d1  741e                  je        short 0048C3F1h
  0048c3d3  f7c100000050          test      ecx,50000000h
  0048c3d9  7516                  jne       short 0048C3F1h
  0048c3db  8b0f                  mov       ecx,[edi]
  0048c3dd  c1e00d                shl       eax,0Dh
  0048c3e0  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  0048c3e7  2407                  and       al,7
  0048c3e9  3c01                  cmp       al,1
  0048c3eb  7504                  jne       short 0048C3F1h
  0048c3ed  660fbeea              movsx     bp,dl

LAB_0048c3f1:
  0048c3f1  46                    inc       esi
  0048c3f2  4b                    dec       ebx
  0048c3f3  75c2                  jne       short 0048C3B7h
  0048c3f5  e9f1000000            jmp       0048C4EBh
  0048c3fa  8b3db4876000          mov       edi,[6087B4h]
  0048c400  bb04000000            mov       ebx,4
  0048c405  0fbfce                movsx     ecx,si
  0048c408  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_0048c40c:
  0048c40c  8a16                  mov       dl,[esi]
  0048c40e  0fbec2                movsx     eax,dl
  0048c411  8d0c80                lea       ecx,[eax+eax*4]
  0048c414  c1e103                shl       ecx,3
  0048c417  2bc8                  sub       ecx,eax
  0048c419  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0048c420  f7c100000080          test      ecx,80000000h
  0048c426  741e                  je        short 0048C446h
  0048c428  f7c100000050          test      ecx,50000000h
  0048c42e  7516                  jne       short 0048C446h
  0048c430  8b0f                  mov       ecx,[edi]
  0048c432  c1e00d                shl       eax,0Dh
  0048c435  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  0048c43c  2407                  and       al,7
  0048c43e  3c03                  cmp       al,3
  0048c440  7504                  jne       short 0048C446h
  0048c442  660fbeea              movsx     bp,dl

LAB_0048c446:
  0048c446  46                    inc       esi
  0048c447  4b                    dec       ebx
  0048c448  75c2                  jne       short 0048C40Ch
  0048c44a  e99c000000            jmp       0048C4EBh
  0048c44f  8b3db4876000          mov       edi,[6087B4h]
  0048c455  bb04000000            mov       ebx,4
  0048c45a  0fbfce                movsx     ecx,si
  0048c45d  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_0048c461:
  0048c461  8a16                  mov       dl,[esi]
  0048c463  0fbec2                movsx     eax,dl
  0048c466  8d0c80                lea       ecx,[eax+eax*4]
  0048c469  c1e103                shl       ecx,3
  0048c46c  2bc8                  sub       ecx,eax
  0048c46e  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0048c475  f7c100000080          test      ecx,80000000h
  0048c47b  741b                  je        short 0048C498h
  0048c47d  f7c100000050          test      ecx,50000000h
  0048c483  7513                  jne       short 0048C498h
  0048c485  8b0f                  mov       ecx,[edi]
  0048c487  c1e00d                shl       eax,0Dh
  0048c48a  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  0048c492  7504                  jne       short 0048C498h
  0048c494  660fbeea              movsx     bp,dl

LAB_0048c498:
  0048c498  46                    inc       esi
  0048c499  4b                    dec       ebx
  0048c49a  75c5                  jne       short 0048C461h
  0048c49c  eb4d                  jmp       short 0048C4EBh
  0048c49e  8b3db4876000          mov       edi,[6087B4h]
  0048c4a4  bb04000000            mov       ebx,4
  0048c4a9  0fbfd6                movsx     edx,si
  0048c4ac  8d749410              lea       esi,[esp+edx*4+10h]

LAB_0048c4b0:
  0048c4b0  8a16                  mov       dl,[esi]
  0048c4b2  0fbec2                movsx     eax,dl
  0048c4b5  8d0c80                lea       ecx,[eax+eax*4]
  0048c4b8  c1e103                shl       ecx,3
  0048c4bb  2bc8                  sub       ecx,eax
  0048c4bd  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  0048c4c4  f7c100000080          test      ecx,80000000h
  0048c4ca  741b                  je        short 0048C4E7h
  0048c4cc  f7c100000050          test      ecx,50000000h
  0048c4d2  7513                  jne       short 0048C4E7h
  0048c4d4  8b0f                  mov       ecx,[edi]
  0048c4d6  c1e00d                shl       eax,0Dh
  0048c4d9  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  0048c4e1  7404                  je        short 0048C4E7h
  0048c4e3  660fbeea              movsx     bp,dl

LAB_0048c4e7:
  0048c4e7  46                    inc       esi
  0048c4e8  4b                    dec       ebx
  0048c4e9  75c5                  jne       short 0048C4B0h

LAB_0048c4eb:
  0048c4eb  0fbfc5                movsx     eax,bp
  0048c4ee  8b4c2424              mov       ecx,[esp+24h]
  0048c4f2  8d1480                lea       edx,[eax+eax*4]
  0048c4f5  c1e203                shl       edx,3
  0048c4f8  2bd0                  sub       edx,eax
  0048c4fa  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  0048c501  89416c                mov       [ecx+6Ch],eax

LAB_0048c504:
  0048c504  5f                    pop       edi
  0048c505  5e                    pop       esi
  0048c506  5d                    pop       ebp
  0048c507  5b                    pop       ebx
  0048c508  83c410                add       esp,10h
  0048c50b  c3                    ret
  0048c50c  29c2                  sub       edx,eax
  0048c50e  48                    dec       eax
  0048c50f  00eb                  add       bl,ch
  0048c511  c44800                les       ecx,[eax]
  0048c514  6c                    insb
  0048c515  c24800                ret       48h
  0048c518  dec2                  faddp     st2
  0048c51a  48                    dec       eax
  0048c51b  0053c3                add       [ebx-3Dh],dl
  0048c51e  48                    dec       eax
  0048c51f  00a5c34800fa          add       [ebp-5FFB73Dh],ah
  0048c525  c3                    ret
  0048c526  48                    dec       eax
  0048c527  004fc4                add       [edi-3Ch],cl
  0048c52a  48                    dec       eax
  0048c52b  009ec44800            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
