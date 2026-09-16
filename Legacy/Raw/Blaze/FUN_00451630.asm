Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00451630  section .text  offset fichier 0x50a30  352 octets

  00451630  53                    push      ebx
  00451631  56                    push      esi
  00451632  8b74240c              mov       esi,[esp+0Ch]
  00451636  57                    push      edi
  00451637  33ff                  xor       edi,edi
  00451639  ba06000000            mov       edx,6
  0045163e  668b4610              mov       ax,[esi+10h]
  00451642  663d0100              cmp       ax,1
  00451646  7406                  je        short 0045164Eh
  00451648  663d0200              cmp       ax,2
  0045164c  7531                  jne       short 0045167Fh

LAB_0045164e:
  0045164e  0fbf4614              movsx     eax,word [esi+14h]
  00451652  8d0c80                lea       ecx,[eax+eax*4]
  00451655  c1e103                shl       ecx,3
  00451658  2bc8                  sub       ecx,eax
  0045165a  8d3c8dc0935e00        lea       edi,[ecx*4+5E93C0h]
  00451661  85ff                  test      edi,edi
  00451663  7505                  jne       short 0045166Ah
  00451665  bf58e55d00            mov       edi,5DE558h

LAB_0045166a:
  0045166a  33c0                  xor       eax,eax
  0045166c  66895718              mov       [edi+18h],dx
  00451670  8a4704                mov       al,[edi+4]
  00451673  8b0c8580965e00        mov       ecx,[eax*4+5E9680h]
  0045167a  8b4918                mov       ecx,[ecx+18h]
  0045167d  eb04                  jmp       short 00451683h

LAB_0045167f:
  0045167f  8b4c2410              mov       ecx,[esp+10h]

LAB_00451683:
  00451683  668b4610              mov       ax,[esi+10h]
  00451687  663d0100              cmp       ax,1
  0045168b  7541                  jne       short 004516CEh
  0045168d  8b06                  mov       eax,[esi]
  0045168f  66c746100200          mov       word [esi+10h],2
  00451695  80cc20                or        ah,20h
  00451698  6a00                  push      0
  0045169a  8906                  mov       [esi],eax
  0045169c  0fbf4614              movsx     eax,word [esi+14h]
  004516a0  8d0c80                lea       ecx,[eax+eax*4]
  004516a3  c1e103                shl       ecx,3
  004516a6  2bc8                  sub       ecx,eax
  004516a8  8d048dc0935e00        lea       eax,[ecx*4+5E93C0h]
  004516af  66895018              mov       [eax+18h],dx
  004516b3  66c7401a0000          mov       word [eax+1Ah],0
  004516b9  e8423c0e00            call      00535300h
  004516be  8b06                  mov       eax,[esi]
  004516c0  83c404                add       esp,4
  004516c3  25fffffffb            and       eax,0FBFFFFFFh
  004516c8  8906                  mov       [esi],eax
  004516ca  5f                    pop       edi
  004516cb  5e                    pop       esi
  004516cc  5b                    pop       ebx
  004516cd  c3                    ret

LAB_004516ce:
  004516ce  663d0200              cmp       ax,2
  004516d2  bb03000000            mov       ebx,3
  004516d7  754a                  jne       short 00451723h
  004516d9  668b471a              mov       ax,[edi+1Ah]
  004516dd  33d2                  xor       edx,edx
  004516df  668b5116              mov       dx,[ecx+16h]
  004516e3  0fbfc8                movsx     ecx,ax
  004516e6  41                    inc       ecx
  004516e7  3bca                  cmp       ecx,edx
  004516e9  7d07                  jge       short 004516F2h
  004516eb  40                    inc       eax
  004516ec  6689471a              mov       [edi+1Ah],ax
  004516f0  eb31                  jmp       short 00451723h

LAB_004516f2:
  004516f2  6a00                  push      0
  004516f4  6a00                  push      0
  004516f6  6800100000            push      1000h
  004516fb  6800040000            push      400h
  00451700  6a08                  push      8
  00451702  56                    push      esi
  00451703  e878690100            call      00468080h
  00451708  8b07                  mov       eax,[edi]
  0045170a  83c418                add       esp,18h
  0045170d  0d00000002            or        eax,2000000h
  00451712  8907                  mov       [edi],eax
  00451714  e8e73c0e00            call      00535400h
  00451719  66895e10              mov       [esi+10h],bx
  0045171d  66c746120000          mov       word [esi+12h],0

LAB_00451723:
  00451723  66395e10              cmp       [esi+10h],bx
  00451727  7555                  jne       short 0045177Eh
  00451729  8b5e20                mov       ebx,[esi+20h]
  0045172c  8b0e                  mov       ecx,[esi]
  0045172e  80cd40                or        ch,40h
  00451731  66ff4612              inc       word [esi+12h]
  00451735  8d9300c00300          lea       edx,[ebx+3C000h]
  0045173b  890e                  mov       [esi],ecx
  0045173d  895620                mov       [esi+20h],edx
  00451740  bf05000000            mov       edi,5

LAB_00451745:
  00451745  6a00                  push      0
  00451747  6a00                  push      0
  00451749  6aff                  push      0FFFFFFFFh
  0045174b  6a00                  push      0
  0045174d  6a36                  push      36h
  0045174f  56                    push      esi
  00451750  e8bb690100            call      00468110h
  00451755  83c418                add       esp,18h
  00451758  4f                    dec       edi
  00451759  75ea                  jne       short 00451745h
  0045175b  6a00                  push      0
  0045175d  6a00                  push      0
  0045175f  6aff                  push      0FFFFFFFFh
  00451761  6a00                  push      0
  00451763  6a31                  push      31h
  00451765  56                    push      esi
  00451766  e8a5690100            call      00468110h
  0045176b  83c418                add       esp,18h
  0045176e  66837e1205            cmp       word [esi+12h],5
  00451773  7506                  jne       short 0045177Bh
  00451775  66c746106300          mov       word [esi+10h],63h

LAB_0045177b:
  0045177b  895e20                mov       [esi+20h],ebx

LAB_0045177e:
  0045177e  66837e1063            cmp       word [esi+10h],63h
  00451783  7507                  jne       short 0045178Ch
  00451785  8b06                  mov       eax,[esi]
  00451787  80cc40                or        ah,40h
  0045178a  8906                  mov       [esi],eax

LAB_0045178c:
  0045178c  5f                    pop       edi
  0045178d  5e                    pop       esi
  0045178e  5b                    pop       ebx
  0045178f  c3                    ret

11 cible(s) de saut interne : 0x0045164e, 0x0045166a, 0x0045167f, 0x00451683, 0x004516ce, 0x004516f2, 0x00451723, 0x00451745, 0x0045177b, 0x0045177e, 0x0045178c
