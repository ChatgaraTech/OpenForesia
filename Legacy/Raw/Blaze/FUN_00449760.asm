Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449760  section .text  offset fichier 0x48b60  335 octets

  00449760  53                    push      ebx
  00449761  56                    push      esi
  00449762  8b74240c              mov       esi,[esp+0Ch]
  00449766  33db                  xor       ebx,ebx
  00449768  8b466c                mov       eax,[esi+6Ch]
  0044976b  3bc3                  cmp       eax,ebx
  0044976d  7505                  jne       short 00449774h
  0044976f  b858e55d00            mov       eax,5DE558h

LAB_00449774:
  00449774  8b481c                mov       ecx,[eax+1Ch]
  00449777  66395e10              cmp       [esi+10h],bx
  0044977b  894e50                mov       [esi+50h],ecx
  0044977e  8b5060                mov       edx,[eax+60h]
  00449781  895654                mov       [esi+54h],edx
  00449784  8b4824                mov       ecx,[eax+24h]
  00449787  894e58                mov       [esi+58h],ecx
  0044978a  668b5032              mov       dx,[eax+32h]
  0044978e  66895612              mov       [esi+12h],dx
  00449792  7560                  jne       short 004497F4h
  00449794  c7463c40155500        mov       dword [esi+3Ch],551540h
  0044979b  885e40                mov       [esi+40h],bl
  0044979e  c6460a06              mov       byte [esi+0Ah],6
  004497a2  885e0b                mov       [esi+0Bh],bl
  004497a5  66c746100100          mov       word [esi+10h],1
  004497ab  66895e14              mov       [esi+14h],bx
  004497af  e88cd90f00            call      00547140h
  004497b4  99                    cdq
  004497b5  b91e000000            mov       ecx,1Eh
  004497ba  f7f9                  idiv      ecx
  004497bc  83c228                add       edx,28h
  004497bf  89565c                mov       [esi+5Ch],edx
  004497c2  e879d90f00            call      00547140h
  004497c7  99                    cdq
  004497c8  33c2                  xor       eax,edx
  004497ca  2bc2                  sub       eax,edx
  004497cc  25ff0f0000            and       eax,0FFFh
  004497d1  33c2                  xor       eax,edx
  004497d3  2bc2                  sub       eax,edx
  004497d5  6689869a000000        mov       [esi+9Ah],ax
  004497dc  e85fd90f00            call      00547140h
  004497e1  99                    cdq
  004497e2  b978000000            mov       ecx,78h
  004497e7  66895e28              mov       [esi+28h],bx
  004497eb  f7f9                  idiv      ecx
  004497ed  66895e2a              mov       [esi+2Ah],bx
  004497f1  895644                mov       [esi+44h],edx

LAB_004497f4:
  004497f4  66837e1001            cmp       word [esi+10h],1
  004497f9  0f85ad000000          jne       near 004498ACh
  004497ff  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00449806  0fbf4612              movsx     eax,word [esi+12h]
  0044980a  03d0                  add       edx,eax
  0044980c  52                    push      edx
  0044980d  e80ac20f00            call      00545A1Ch
  00449812  0faf465c              imul      eax,[esi+5Ch]
  00449816  8b4e50                mov       ecx,[esi+50h]
  00449819  0fbf5612              movsx     edx,word [esi+12h]
  0044981d  03c1                  add       eax,ecx
  0044981f  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00449826  03ca                  add       ecx,edx
  00449828  89461c                mov       [esi+1Ch],eax
  0044982b  51                    push      ecx
  0044982c  e8e5c10f00            call      00545A16h
  00449831  0faf465c              imul      eax,[esi+5Ch]
  00449835  8b4e58                mov       ecx,[esi+58h]
  00449838  8b5644                mov       edx,[esi+44h]
  0044983b  03c1                  add       eax,ecx
  0044983d  894624                mov       [esi+24h],eax
  00449840  668b4614              mov       ax,[esi+14h]
  00449844  0fbfc8                movsx     ecx,ax
  00449847  03d1                  add       edx,ecx
  00449849  8b4e54                mov       ecx,[esi+54h]
  0044984c  c1e20c                shl       edx,0Ch
  0044984f  03d1                  add       edx,ecx
  00449851  40                    inc       eax
  00449852  663d1400              cmp       ax,14h
  00449856  895620                mov       [esi+20h],edx
  00449859  66894614              mov       [esi+14h],ax
  0044985d  7e2a                  jle       short 00449889h
  0044985f  b96affffff            mov       ecx,0FFFFFF6Ah
  00449864  66014e28              add       [esi+28h],cx
  00449868  668b4628              mov       ax,[esi+28h]
  0044986c  66014e2a              add       [esi+2Ah],cx
  00449870  663bc3                cmp       ax,bx
  00449873  7d37                  jge       short 004498ACh
  00449875  8b06                  mov       eax,[esi]
  00449877  66895e28              mov       [esi+28h],bx
  0044987b  25ffffff7f            and       eax,7FFFFFFFh
  00449880  66895e2a              mov       [esi+2Ah],bx
  00449884  8906                  mov       [esi],eax
  00449886  5e                    pop       esi
  00449887  5b                    pop       ebx
  00449888  c3                    ret

LAB_00449889:
  00449889  b996000000            mov       ecx,96h
  0044988e  66014e28              add       [esi+28h],cx
  00449892  66014e2a              add       [esi+2Ah],cx
  00449896  668b4628              mov       ax,[esi+28h]
  0044989a  b900080000            mov       ecx,800h
  0044989f  663bc1                cmp       ax,cx
  004498a2  7e08                  jle       short 004498ACh
  004498a4  66894e28              mov       [esi+28h],cx
  004498a8  66894e2a              mov       [esi+2Ah],cx

LAB_004498ac:
  004498ac  5e                    pop       esi
  004498ad  5b                    pop       ebx
  004498ae  c3                    ret

4 cible(s) de saut interne : 0x00449774, 0x004497f4, 0x00449889, 0x004498ac
