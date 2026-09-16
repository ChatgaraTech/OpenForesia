Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00451790  section .text  offset fichier 0x50b90  309 octets

  00451790  8b442404              mov       eax,[esp+4]
  00451794  53                    push      ebx
  00451795  56                    push      esi
  00451796  57                    push      edi
  00451797  33ff                  xor       edi,edi
  00451799  66397810              cmp       [eax+10h],di
  0045179d  750a                  jne       short 004517A9h
  0045179f  66897812              mov       [eax+12h],di
  004517a3  66c740106300          mov       word [eax+10h],63h

LAB_004517a9:
  004517a9  6683781001            cmp       word [eax+10h],1
  004517ae  be02000000            mov       esi,2
  004517b3  7550                  jne       short 00451805h
  004517b5  0fbf4816              movsx     ecx,word [eax+16h]
  004517b9  668340349c            add       word [eax+34h],0FF9Ch
  004517be  8d1489                lea       edx,[ecx+ecx*4]
  004517c1  c1e203                shl       edx,3
  004517c4  2bd1                  sub       edx,ecx
  004517c6  6683449034b5          add       word [eax+edx*4+34h],0FFB5h
  004517cc  8d4c9034              lea       ecx,[eax+edx*4+34h]
  004517d0  b938ffffff            mov       ecx,0FFFFFF38h
  004517d5  66394834              cmp       [eax+34h],cx
  004517d9  7d17                  jge       short 004517F2h
  004517db  66894834              mov       [eax+34h],cx
  004517df  0fbf4816              movsx     ecx,word [eax+16h]
  004517e3  8d1489                lea       edx,[ecx+ecx*4]
  004517e6  c1e203                shl       edx,3
  004517e9  2bd1                  sub       edx,ecx
  004517eb  66c74490343200        mov       word [eax+edx*4+34h],32h

LAB_004517f2:
  004517f2  66ff4012              inc       word [eax+12h]
  004517f6  668378120a            cmp       word [eax+12h],0Ah
  004517fb  7508                  jne       short 00451805h
  004517fd  66897010              mov       [eax+10h],si
  00451801  66897812              mov       [eax+12h],di

LAB_00451805:
  00451805  66397010              cmp       [eax+10h],si
  00451809  ba03000000            mov       edx,3
  0045180e  7536                  jne       short 00451846h
  00451810  0fbf4814              movsx     ecx,word [eax+14h]
  00451814  33db                  xor       ebx,ebx
  00451816  8d3489                lea       esi,[ecx+ecx*4]
  00451819  c1e603                shl       esi,3
  0045181c  2bf1                  sub       esi,ecx
  0045181e  33c9                  xor       ecx,ecx
  00451820  8a0cb5c8935e00        mov       cl,[esi*4+5E93C8h]
  00451827  6bc94c                imul      ecx,4Ch
  0045182a  8a9988915e00          mov       bl,[ecx+5E9188h]
  00451830  c1e304                shl       ebx,4
  00451833  8b8b204a6000          mov       ecx,[ebx+604A20h]
  00451839  f6c5a0                test      ch,0A0h
  0045183c  7508                  jne       short 00451846h
  0045183e  66895010              mov       [eax+10h],dx
  00451842  66897812              mov       [eax+12h],di

LAB_00451846:
  00451846  66395010              cmp       [eax+10h],dx
  0045184a  7530                  jne       short 0045187Ch
  0045184c  0fbf4816              movsx     ecx,word [eax+16h]
  00451850  6683403464            add       word [eax+34h],64h
  00451855  8d1489                lea       edx,[ecx+ecx*4]
  00451858  c1e203                shl       edx,3
  0045185b  2bd1                  sub       edx,ecx
  0045185d  66834490344b          add       word [eax+edx*4+34h],4Bh
  00451863  66ff4012              inc       word [eax+12h]
  00451867  668378120a            cmp       word [eax+12h],0Ah
  0045186c  8d4c9034              lea       ecx,[eax+edx*4+34h]
  00451870  750a                  jne       short 0045187Ch
  00451872  66c740106300          mov       word [eax+10h],63h
  00451878  66897812              mov       [eax+12h],di

LAB_0045187c:
  0045187c  6683781063            cmp       word [eax+10h],63h
  00451881  753e                  jne       short 004518C1h
  00451883  668b4834              mov       cx,[eax+34h]
  00451887  be20030000            mov       esi,320h
  0045188c  0fbfd1                movsx     edx,cx
  0045188f  2bf2                  sub       esi,edx
  00451891  c1fe02                sar       esi,2
  00451894  03f1                  add       esi,ecx
  00451896  0fbf4816              movsx     ecx,word [eax+16h]
  0045189a  66897034              mov       [eax+34h],si
  0045189e  be20030000            mov       esi,320h
  004518a3  8d1489                lea       edx,[ecx+ecx*4]
  004518a6  c1e203                shl       edx,3
  004518a9  2bd1                  sub       edx,ecx
  004518ab  668b4c9034            mov       cx,[eax+edx*4+34h]
  004518b0  8d449034              lea       eax,[eax+edx*4+34h]
  004518b4  0fbfd1                movsx     edx,cx
  004518b7  2bf2                  sub       esi,edx
  004518b9  c1fe02                sar       esi,2
  004518bc  03f1                  add       esi,ecx
  004518be  668930                mov       [eax],si

LAB_004518c1:
  004518c1  5f                    pop       edi
  004518c2  5e                    pop       esi
  004518c3  5b                    pop       ebx
  004518c4  c3                    ret

6 cible(s) de saut interne : 0x004517a9, 0x004517f2, 0x00451805, 0x00451846, 0x0045187c, 0x004518c1
