  00486720  83ec18                sub       esp,18h
  00486723  53                    push      ebx
  00486724  55                    push      ebp
  00486725  56                    push      esi
  00486726  8b742428              mov       esi,[esp+28h]
  0048672a  57                    push      edi
  0048672b  66c744241000fc        mov       word [esp+10h],0FC00h
  00486732  66837e1001            cmp       word [esi+10h],1
  00486737  66c7442412e2ff        mov       word [esp+12h],0FFE2h
  0048673e  66c74424141e00        mov       word [esp+14h],1Eh
  00486745  66c7442416c800        mov       word [esp+16h],0C8h
  0048674c  66c74424180000        mov       word [esp+18h],0
  00486753  66c744241aa000        mov       word [esp+1Ah],0A0h
  0048675a  0f85c1000000          jne       near 00486821h
  00486760  668b4612              mov       ax,[esi+12h]
  00486764  6685c0                test      ax,ax
  00486767  0f84b4000000          je        near 00486821h
  0048676d  0fbf7e32              movsx     edi,word [esi+32h]
  00486771  0fbf562a              movsx     edx,word [esi+2Ah]
  00486775  8b4e24                mov       ecx,[esi+24h]
  00486778  8b6e1c                mov       ebp,[esi+1Ch]
  0048677b  48                    dec       eax
  0048677c  8d9f00fcffff          lea       ebx,[edi-400h]
  00486782  66894612              mov       [esi+12h],ax
  00486786  8b4620                mov       eax,[esi+20h]
  00486789  53                    push      ebx
  0048678a  89442424              mov       [esp+24h],eax
  0048678e  894c2428              mov       [esp+28h],ecx
  00486792  89542430              mov       [esp+30h],edx
  00486796  e881f20b00            call      00545A1Ch
  0048679b  8b561c                mov       edx,[esi+1Ch]
  0048679e  8d0480                lea       eax,[eax+eax*4]
  004867a1  53                    push      ebx
  004867a2  8d0480                lea       eax,[eax+eax*4]
  004867a5  c1e002                shl       eax,2
  004867a8  03d0                  add       edx,eax
  004867aa  89561c                mov       [esi+1Ch],edx
  004867ad  e864f20b00            call      00545A16h
  004867b2  8b5e24                mov       ebx,[esi+24h]
  004867b5  8d0480                lea       eax,[eax+eax*4]
  004867b8  66c7462a0002          mov       word [esi+2Ah],200h
  004867be  8d0c80                lea       ecx,[eax+eax*4]
  004867c1  c1e102                shl       ecx,2
  004867c4  03d9                  add       ebx,ecx
  004867c6  895e24                mov       [esi+24h],ebx
  004867c9  e872090c00            call      00547140h
  004867ce  25ff030000            and       eax,3FFh
  004867d3  6a0d                  push      0Dh
  004867d5  2d00020000            sub       eax,200h
  004867da  6a01                  push      1
  004867dc  66014632              add       [esi+32h],ax
  004867e0  8b15cc975e00          mov       edx,[5E97CCh]
  004867e6  8b82d0000000          mov       eax,[edx+0D0h]
  004867ec  33d2                  xor       edx,edx
  004867ee  8b08                  mov       ecx,[eax]
  004867f0  8a5107                mov       dl,[ecx+7]
  004867f3  81c2c0000000          add       edx,0C0h
  004867f9  52                    push      edx
  004867fa  56                    push      esi
  004867fb  e8e042ffff            call      0047AAE0h
  00486800  8b442430              mov       eax,[esp+30h]
  00486804  8b4c2434              mov       ecx,[esp+34h]
  00486808  668b54243c            mov       dx,[esp+3Ch]
  0048680d  83c410                add       esp,10h
  00486810  896e1c                mov       [esi+1Ch],ebp
  00486813  894620                mov       [esi+20h],eax
  00486816  894e24                mov       [esi+24h],ecx
  00486819  66897e32              mov       [esi+32h],di
  0048681d  6689562a              mov       [esi+2Ah],dx
  00486821  66837e100a            cmp       word [esi+10h],0Ah
  00486826  8b4638                mov       eax,[esi+38h]
  00486829  0f8d84000000          jge       near 004868B3h
  0048682f  3d80404000            cmp       eax,404080h
  00486834  0f838b000000          jae       near 004868C5h
  0048683a  0508040400            add       eax,40408h
  0048683f  ba00200000            mov       edx,2000h
  00486844  894638                mov       [esi+38h],eax
  00486847  668b462a              mov       ax,[esi+2Ah]
  0048684b  0fbfc8                movsx     ecx,ax
  0048684e  2bd1                  sub       edx,ecx
  00486850  c1fa02                sar       edx,2
  00486853  03d0                  add       edx,eax
  00486855  8d442410              lea       eax,[esp+10h]
  00486859  50                    push      eax
  0048685a  56                    push      esi
  0048685b  6689562a              mov       [esi+2Ah],dx
  0048685f  e80ce4feff            call      00474C70h
  00486864  8bf8                  mov       edi,eax
  00486866  a1b8e05d00            mov       eax,[5DE0B8h]
  0048686b  83c408                add       esp,8
  0048686e  85c0                  test      eax,eax
  00486870  7453                  je        short 004868C5h
  00486872  8b4e74                mov       ecx,[esi+74h]
  00486875  57                    push      edi
  00486876  51                    push      ecx
  00486877  e804330b00            call      00539B80h
  0048687c  8b2f                  mov       ebp,[edi]
  0048687e  83c408                add       esp,8
  00486881  8d561c                lea       edx,[esi+1Ch]
  00486884  83cd40                or        ebp,40h
  00486887  52                    push      edx
  00486888  6800180000            push      1800h
  0048688d  6800040000            push      400h
  00486892  6a03                  push      3
  00486894  66c787960000000800    mov       word [edi+96h],8
  0048689d  892f                  mov       [edi],ebp
  0048689f  e87c720b00            call      0053DB20h
  004868a4  83c410                add       esp,10h
  004868a7  66ff4610              inc       word [esi+10h]
  004868ab  5f                    pop       edi
  004868ac  5e                    pop       esi
  004868ad  5d                    pop       ebp
  004868ae  5b                    pop       ebx
  004868af  83c418                add       esp,18h
  004868b2  c3                    ret
  004868b3  85c0                  test      eax,eax
  004868b5  740e                  je        short 004868C5h
  004868b7  05f8fbfbff            add       eax,0FFFBFBF8h
  004868bc  85c0                  test      eax,eax
  004868be  894638                mov       [esi+38h],eax
  004868c1  7502                  jne       short 004868C5h
  004868c3  8906                  mov       [esi],eax
  004868c5  66ff4610              inc       word [esi+10h]
  004868c9  5f                    pop       edi
  004868ca  5e                    pop       esi
  004868cb  5d                    pop       ebp
  004868cc  5b                    pop       ebx
  004868cd  83c418                add       esp,18h
  004868d0  c3                    ret
