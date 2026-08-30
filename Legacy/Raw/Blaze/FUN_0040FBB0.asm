; 0x0040fbb0 — l'eclat pose sur une corde d'arc (520 instructions, absente de Blaze.c).
; Deux copies : 0x00412d10 (planche 0x0054f878), 0x00416300 (planche 0x0054f970).
; Une seule ligne d'ecart entre les trois.

C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040fbb0  section .text  offset fichier 0xefb0  1557 octets

  0040fbb0  51                    push      ecx
  0040fbb1  53                    push      ebx
  0040fbb2  55                    push      ebp
  0040fbb3  56                    push      esi
  0040fbb4  8b742414              mov       esi,[esp+14h]
  0040fbb8  57                    push      edi
  0040fbb9  66837e1000            cmp       word [esi+10h],0
  0040fbbe  0f8533050000          jne       near 004100F7h
  0040fbc4  66c7462e1400          mov       word [esi+2Eh],14h
  0040fbca  c7463c58f75400        mov       dword [esi+3Ch],54F758h
  0040fbd1  e86a751300            call      00547140h
  0040fbd6  99                    cdq
  0040fbd7  b958020000            mov       ecx,258h
  0040fbdc  f7f9                  idiv      ecx
  0040fbde  81c270170000          add       edx,1770h
  0040fbe4  66895628              mov       [esi+28h],dx
  0040fbe8  e853751300            call      00547140h
  0040fbed  99                    cdq
  0040fbee  b958020000            mov       ecx,258h
  0040fbf3  f7f9                  idiv      ecx
  0040fbf5  81c270170000          add       edx,1770h
  0040fbfb  66837e1202            cmp       word [esi+12h],2
  0040fc00  6689562a              mov       [esi+2Ah],dx
  0040fc04  7411                  je        short 0040FC17h
  0040fc06  e835751300            call      00547140h
  0040fc0b  99                    cdq
  0040fc0c  b906000000            mov       ecx,6
  0040fc11  f7f9                  idiv      ecx
  0040fc13  8bca                  mov       ecx,edx
  0040fc15  eb13                  jmp       short 0040FC2Ah

LAB_0040fc17:
  0040fc17  e824751300            call      00547140h
  0040fc1c  99                    cdq
  0040fc1d  33c2                  xor       eax,edx
  0040fc1f  2bc2                  sub       eax,edx
  0040fc21  83e007                and       eax,7
  0040fc24  33c2                  xor       eax,edx
  0040fc26  8bc8                  mov       ecx,eax
  0040fc28  2bca                  sub       ecx,edx

LAB_0040fc2a:
  0040fc2a  6685c9                test      cx,cx
  0040fc2d  894c2418              mov       [esp+18h],ecx
  0040fc31  740a                  je        short 0040FC3Dh
  0040fc33  6683f904              cmp       cx,4
  0040fc37  0f85db000000          jne       near 0040FD18h

LAB_0040fc3d:
  0040fc3d  0fbf5632              movsx     edx,word [esi+32h]
  0040fc41  81c2bc020000          add       edx,2BCh
  0040fc47  52                    push      edx
  0040fc48  e8cf5d1300            call      00545A1Ch
  0040fc4d  8d0480                lea       eax,[eax+eax*4]
  0040fc50  8d0480                lea       eax,[eax+eax*4]
  0040fc53  8d3c80                lea       edi,[eax+eax*4]
  0040fc56  e8e5741300            call      00547140h
  0040fc5b  99                    cdq
  0040fc5c  b914000000            mov       ecx,14h
  0040fc61  f7f9                  idiv      ecx
  0040fc63  83ea0a                sub       edx,0Ah
  0040fc66  c1e20c                shl       edx,0Ch
  0040fc69  8d3c7a                lea       edi,[edx+edi*2]
  0040fc6c  0fbf5632              movsx     edx,word [esi+32h]
  0040fc70  81c2bc020000          add       edx,2BCh
  0040fc76  52                    push      edx
  0040fc77  e89a5d1300            call      00545A16h
  0040fc7c  8d0480                lea       eax,[eax+eax*4]
  0040fc7f  8d0480                lea       eax,[eax+eax*4]
  0040fc82  8d1c80                lea       ebx,[eax+eax*4]
  0040fc85  e8b6741300            call      00547140h
  0040fc8a  99                    cdq
  0040fc8b  b914000000            mov       ecx,14h
  0040fc90  f7f9                  idiv      ecx
  0040fc92  83ea0a                sub       edx,0Ah
  0040fc95  c1e20c                shl       edx,0Ch
  0040fc98  8d2c5a                lea       ebp,[edx+ebx*2]
  0040fc9b  0fbf5632              movsx     edx,word [esi+32h]
  0040fc9f  81eabc020000          sub       edx,2BCh
  0040fca5  52                    push      edx
  0040fca6  e8715d1300            call      00545A1Ch
  0040fcab  8d0480                lea       eax,[eax+eax*4]
  0040fcae  8d0480                lea       eax,[eax+eax*4]
  0040fcb1  8d1c80                lea       ebx,[eax+eax*4]
  0040fcb4  e887741300            call      00547140h
  0040fcb9  99                    cdq
  0040fcba  b914000000            mov       ecx,14h
  0040fcbf  f7f9                  idiv      ecx
  0040fcc1  0fbf4632              movsx     eax,word [esi+32h]
  0040fcc5  2dbc020000            sub       eax,2BCh
  0040fcca  50                    push      eax
  0040fccb  83ea0a                sub       edx,0Ah
  0040fcce  c1e20c                shl       edx,0Ch
  0040fcd1  8d145a                lea       edx,[edx+ebx*2]
  0040fcd4  89542414              mov       [esp+14h],edx
  0040fcd8  e8395d1300            call      00545A16h
  0040fcdd  8d0480                lea       eax,[eax+eax*4]
  0040fce0  8d0480                lea       eax,[eax+eax*4]
  0040fce3  8d1c80                lea       ebx,[eax+eax*4]
  0040fce6  e855741300            call      00547140h
  0040fceb  99                    cdq
  0040fcec  b914000000            mov       ecx,14h
  0040fcf1  f7f9                  idiv      ecx
  0040fcf3  83ea0a                sub       edx,0Ah
  0040fcf6  c1e20c                shl       edx,0Ch
  0040fcf9  8d1c5a                lea       ebx,[edx+ebx*2]
  0040fcfc  e83f741300            call      00547140h
  0040fd01  99                    cdq
  0040fd02  33c2                  xor       eax,edx
  0040fd04  8b4c2418              mov       ecx,[esp+18h]
  0040fd08  2bc2                  sub       eax,edx
  0040fd0a  83e007                and       eax,7
  0040fd0d  33c2                  xor       eax,edx
  0040fd0f  2bc2                  sub       eax,edx
  0040fd11  ba08000000            mov       edx,8
  0040fd16  eb14                  jmp       short 0040FD2Ch

LAB_0040fd18:
  0040fd18  8b7c2418              mov       edi,[esp+18h]
  0040fd1c  8b6c2418              mov       ebp,[esp+18h]
  0040fd20  8b5c2418              mov       ebx,[esp+18h]
  0040fd24  8b542418              mov       edx,[esp+18h]
  0040fd28  8b442418              mov       eax,[esp+18h]

LAB_0040fd2c:
  0040fd2c  6683f901              cmp       cx,1
  0040fd30  7415                  je        short 0040FD47h
  0040fd32  66837e1202            cmp       word [esi+12h],2
  0040fd37  0f85e3000000          jne       near 0040FE20h
  0040fd3d  6683f906              cmp       cx,6
  0040fd41  0f8cd9000000          jl        near 0040FE20h

LAB_0040fd47:
  0040fd47  0fbf5632              movsx     edx,word [esi+32h]
  0040fd4b  81eabc020000          sub       edx,2BCh
  0040fd51  52                    push      edx
  0040fd52  e8c55c1300            call      00545A1Ch
  0040fd57  8d0480                lea       eax,[eax+eax*4]
  0040fd5a  8d0480                lea       eax,[eax+eax*4]
  0040fd5d  8d3c80                lea       edi,[eax+eax*4]
  0040fd60  e8db731300            call      00547140h
  0040fd65  99                    cdq
  0040fd66  b914000000            mov       ecx,14h
  0040fd6b  f7f9                  idiv      ecx
  0040fd6d  83ea0a                sub       edx,0Ah
  0040fd70  c1e20c                shl       edx,0Ch
  0040fd73  8d3c7a                lea       edi,[edx+edi*2]
  0040fd76  0fbf5632              movsx     edx,word [esi+32h]
  0040fd7a  81eabc020000          sub       edx,2BCh
  0040fd80  52                    push      edx
  0040fd81  e8905c1300            call      00545A16h
  0040fd86  8d0480                lea       eax,[eax+eax*4]
  0040fd89  8d0480                lea       eax,[eax+eax*4]
  0040fd8c  8d1c80                lea       ebx,[eax+eax*4]
  0040fd8f  e8ac731300            call      00547140h
  0040fd94  99                    cdq
  0040fd95  b914000000            mov       ecx,14h
  0040fd9a  f7f9                  idiv      ecx
  0040fd9c  83ea0a                sub       edx,0Ah
  0040fd9f  c1e20c                shl       edx,0Ch
  0040fda2  8d2c5a                lea       ebp,[edx+ebx*2]
  0040fda5  0fbf5632              movsx     edx,word [esi+32h]
  0040fda9  81ea14050000          sub       edx,514h
  0040fdaf  52                    push      edx
  0040fdb0  e8675c1300            call      00545A1Ch
  0040fdb5  8d0480                lea       eax,[eax+eax*4]
  0040fdb8  8d0480                lea       eax,[eax+eax*4]
  0040fdbb  8d1c80                lea       ebx,[eax+eax*4]
  0040fdbe  e87d731300            call      00547140h
  0040fdc3  99                    cdq
  0040fdc4  b914000000            mov       ecx,14h
  0040fdc9  f7f9                  idiv      ecx
  0040fdcb  0fbf4632              movsx     eax,word [esi+32h]
  0040fdcf  2d14050000            sub       eax,514h
  0040fdd4  50                    push      eax
  0040fdd5  83ea0a                sub       edx,0Ah
  0040fdd8  c1e20c                shl       edx,0Ch
  0040fddb  8d145a                lea       edx,[edx+ebx*2]
  0040fdde  89542414              mov       [esp+14h],edx
  0040fde2  e82f5c1300            call      00545A16h
  0040fde7  8d0480                lea       eax,[eax+eax*4]
  0040fdea  8d0480                lea       eax,[eax+eax*4]
  0040fded  8d1c80                lea       ebx,[eax+eax*4]
  0040fdf0  e84b731300            call      00547140h
  0040fdf5  99                    cdq
  0040fdf6  b914000000            mov       ecx,14h
  0040fdfb  f7f9                  idiv      ecx
  0040fdfd  83ea0a                sub       edx,0Ah
  0040fe00  c1e20c                shl       edx,0Ch
  0040fe03  8d1c5a                lea       ebx,[edx+ebx*2]
  0040fe06  e835731300            call      00547140h
  0040fe0b  99                    cdq
  0040fe0c  33c2                  xor       eax,edx
  0040fe0e  8b4c2418              mov       ecx,[esp+18h]
  0040fe12  2bc2                  sub       eax,edx
  0040fe14  83e003                and       eax,3
  0040fe17  33c2                  xor       eax,edx
  0040fe19  2bc2                  sub       eax,edx
  0040fe1b  ba04000000            mov       edx,4

LAB_0040fe20:
  0040fe20  6683f902              cmp       cx,2
  0040fe24  740a                  je        short 0040FE30h
  0040fe26  6683f905              cmp       cx,5
  0040fe2a  0f85d9000000          jne       near 0040FF09h

LAB_0040fe30:
  0040fe30  0fbf5632              movsx     edx,word [esi+32h]
  0040fe34  81ea14050000          sub       edx,514h
  0040fe3a  52                    push      edx
  0040fe3b  e8dc5b1300            call      00545A1Ch
  0040fe40  8d0480                lea       eax,[eax+eax*4]
  0040fe43  8d0480                lea       eax,[eax+eax*4]
  0040fe46  8d3c80                lea       edi,[eax+eax*4]
  0040fe49  e8f2721300            call      00547140h
  0040fe4e  99                    cdq
  0040fe4f  b914000000            mov       ecx,14h
  0040fe54  f7f9                  idiv      ecx
  0040fe56  83ea0a                sub       edx,0Ah
  0040fe59  c1e20c                shl       edx,0Ch
  0040fe5c  8d3c7a                lea       edi,[edx+edi*2]
  0040fe5f  0fbf5632              movsx     edx,word [esi+32h]
  0040fe63  81ea14050000          sub       edx,514h
  0040fe69  52                    push      edx
  0040fe6a  e8a75b1300            call      00545A16h
  0040fe6f  8d0480                lea       eax,[eax+eax*4]
  0040fe72  8d0480                lea       eax,[eax+eax*4]
  0040fe75  8d1c80                lea       ebx,[eax+eax*4]
  0040fe78  e8c3721300            call      00547140h
  0040fe7d  99                    cdq
  0040fe7e  b914000000            mov       ecx,14h
  0040fe83  f7f9                  idiv      ecx
  0040fe85  83ea0a                sub       edx,0Ah
  0040fe88  c1e20c                shl       edx,0Ch
  0040fe8b  8d2c5a                lea       ebp,[edx+ebx*2]
  0040fe8e  0fbf5632              movsx     edx,word [esi+32h]
  0040fe92  81c214050000          add       edx,514h
  0040fe98  52                    push      edx
  0040fe99  e87e5b1300            call      00545A1Ch
  0040fe9e  8d0480                lea       eax,[eax+eax*4]
  0040fea1  8d0480                lea       eax,[eax+eax*4]
  0040fea4  8d1c80                lea       ebx,[eax+eax*4]
  0040fea7  e894721300            call      00547140h
  0040feac  99                    cdq
  0040fead  b914000000            mov       ecx,14h
  0040feb2  f7f9                  idiv      ecx
  0040feb4  0fbf4632              movsx     eax,word [esi+32h]
  0040feb8  0514050000            add       eax,514h
  0040febd  50                    push      eax
  0040febe  83ea0a                sub       edx,0Ah
  0040fec1  c1e20c                shl       edx,0Ch
  0040fec4  8d145a                lea       edx,[edx+ebx*2]
  0040fec7  89542414              mov       [esp+14h],edx
  0040fecb  e8465b1300            call      00545A16h
  0040fed0  8d0480                lea       eax,[eax+eax*4]
  0040fed3  8d0480                lea       eax,[eax+eax*4]
  0040fed6  8d1c80                lea       ebx,[eax+eax*4]
  0040fed9  e862721300            call      00547140h
  0040fede  99                    cdq
  0040fedf  b914000000            mov       ecx,14h
  0040fee4  f7f9                  idiv      ecx
  0040fee6  83ea0a                sub       edx,0Ah
  0040fee9  c1e20c                shl       edx,0Ch
  0040feec  8d1c5a                lea       ebx,[edx+ebx*2]
  0040feef  e84c721300            call      00547140h
  0040fef4  99                    cdq
  0040fef5  33c2                  xor       eax,edx
  0040fef7  8b4c2418              mov       ecx,[esp+18h]
  0040fefb  2bc2                  sub       eax,edx
  0040fefd  83e007                and       eax,7
  0040ff00  33c2                  xor       eax,edx
  0040ff02  2bc2                  sub       eax,edx
  0040ff04  ba08000000            mov       edx,8

LAB_0040ff09:
  0040ff09  6683f903              cmp       cx,3
  0040ff0d  0f85d5000000          jne       near 0040FFE8h
  0040ff13  0fbf5632              movsx     edx,word [esi+32h]
  0040ff17  81c214050000          add       edx,514h
  0040ff1d  52                    push      edx
  0040ff1e  e8f95a1300            call      00545A1Ch
  0040ff23  8d0480                lea       eax,[eax+eax*4]
  0040ff26  8d0480                lea       eax,[eax+eax*4]
  0040ff29  8d3c80                lea       edi,[eax+eax*4]
  0040ff2c  e80f721300            call      00547140h
  0040ff31  99                    cdq
  0040ff32  b914000000            mov       ecx,14h
  0040ff37  f7f9                  idiv      ecx
  0040ff39  83ea0a                sub       edx,0Ah
  0040ff3c  c1e20c                shl       edx,0Ch
  0040ff3f  8d3c7a                lea       edi,[edx+edi*2]
  0040ff42  0fbf5632              movsx     edx,word [esi+32h]
  0040ff46  81c214050000          add       edx,514h
  0040ff4c  52                    push      edx
  0040ff4d  e8c45a1300            call      00545A16h
  0040ff52  8d0480                lea       eax,[eax+eax*4]
  0040ff55  8d0480                lea       eax,[eax+eax*4]
  0040ff58  8d1c80                lea       ebx,[eax+eax*4]
  0040ff5b  e8e0711300            call      00547140h
  0040ff60  99                    cdq
  0040ff61  b914000000            mov       ecx,14h
  0040ff66  f7f9                  idiv      ecx
  0040ff68  83ea0a                sub       edx,0Ah
  0040ff6b  c1e20c                shl       edx,0Ch
  0040ff6e  8d2c5a                lea       ebp,[edx+ebx*2]
  0040ff71  0fbf5632              movsx     edx,word [esi+32h]
  0040ff75  81c2bc020000          add       edx,2BCh
  0040ff7b  52                    push      edx
  0040ff7c  e89b5a1300            call      00545A1Ch
  0040ff81  8d0480                lea       eax,[eax+eax*4]
  0040ff84  8d0480                lea       eax,[eax+eax*4]
  0040ff87  8d1c80                lea       ebx,[eax+eax*4]
  0040ff8a  e8b1711300            call      00547140h
  0040ff8f  99                    cdq
  0040ff90  b914000000            mov       ecx,14h
  0040ff95  f7f9                  idiv      ecx
  0040ff97  0fbf4632              movsx     eax,word [esi+32h]
  0040ff9b  05bc020000            add       eax,2BCh
  0040ffa0  50                    push      eax
  0040ffa1  83ea0a                sub       edx,0Ah
  0040ffa4  c1e20c                shl       edx,0Ch
  0040ffa7  8d145a                lea       edx,[edx+ebx*2]
  0040ffaa  89542414              mov       [esp+14h],edx
  0040ffae  e8635a1300            call      00545A16h
  0040ffb3  8d0480                lea       eax,[eax+eax*4]
  0040ffb6  8d0480                lea       eax,[eax+eax*4]
  0040ffb9  8d1c80                lea       ebx,[eax+eax*4]
  0040ffbc  e87f711300            call      00547140h
  0040ffc1  99                    cdq
  0040ffc2  b914000000            mov       ecx,14h
  0040ffc7  f7f9                  idiv      ecx
  0040ffc9  83ea0a                sub       edx,0Ah
  0040ffcc  c1e20c                shl       edx,0Ch
  0040ffcf  8d1c5a                lea       ebx,[edx+ebx*2]
  0040ffd2  e869711300            call      00547140h
  0040ffd7  99                    cdq
  0040ffd8  33c2                  xor       eax,edx
  0040ffda  2bc2                  sub       eax,edx
  0040ffdc  83e003                and       eax,3
  0040ffdf  33c2                  xor       eax,edx
  0040ffe1  2bc2                  sub       eax,edx
  0040ffe3  ba04000000            mov       edx,4

LAB_0040ffe8:
  0040ffe8  0fbfc0                movsx     eax,ax
  0040ffeb  c1ff0c                sar       edi,0Ch
  0040ffee  c1fd0c                sar       ebp,0Ch
  0040fff1  40                    inc       eax
  0040fff2  0fbfca                movsx     ecx,dx
  0040fff5  8b542410              mov       edx,[esp+10h]
  0040fff9  89442418              mov       [esp+18h],eax
  0040fffd  c1fa0c                sar       edx,0Ch
  00410000  8bc7                  mov       eax,edi
  00410002  2bc2                  sub       eax,edx
  00410004  99                    cdq
  00410005  f7f9                  idiv      ecx
  00410007  8b561c                mov       edx,[esi+1Ch]
  0041000a  c1fb0c                sar       ebx,0Ch
  0041000d  0faf442418            imul      eax,[esp+18h]
  00410012  2bf8                  sub       edi,eax
  00410014  8bc5                  mov       eax,ebp
  00410016  c1e70c                shl       edi,0Ch
  00410019  03d7                  add       edx,edi
  0041001b  2bc3                  sub       eax,ebx
  0041001d  89561c                mov       [esi+1Ch],edx
  00410020  99                    cdq
  00410021  f7f9                  idiv      ecx
  00410023  0faf442418            imul      eax,[esp+18h]
  00410028  2be8                  sub       ebp,eax
  0041002a  8b4624                mov       eax,[esi+24h]
  0041002d  c1e50c                shl       ebp,0Ch
  00410030  03c5                  add       eax,ebp
  00410032  894624                mov       [esi+24h],eax
  00410035  e806711300            call      00547140h
  0041003a  99                    cdq
  0041003b  b914000000            mov       ecx,14h
  00410040  33ed                  xor       ebp,ebp
  00410042  f7f9                  idiv      ecx
  00410044  b80060ffff            mov       eax,0FFFF6000h
  00410049  8b4e1c                mov       ecx,[esi+1Ch]
  0041004c  bb01000000            mov       ebx,1
  00410051  894e50                mov       [esi+50h],ecx
  00410054  c6460a09              mov       byte [esi+0Ah],9
  00410058  c6460b00              mov       byte [esi+0Bh],0
  0041005c  66895e10              mov       [esi+10h],bx
  00410060  c1e20c                shl       edx,0Ch
  00410063  2bc2                  sub       eax,edx
  00410065  8b5620                mov       edx,[esi+20h]
  00410068  03d0                  add       edx,eax
  0041006a  8bc2                  mov       eax,edx
  0041006c  895620                mov       [esi+20h],edx
  0041006f  8b5624                mov       edx,[esi+24h]
  00410072  894654                mov       [esi+54h],eax
  00410075  668b4612              mov       ax,[esi+12h]
  00410079  895658                mov       [esi+58h],edx
  0041007c  663bc5                cmp       ax,bp
  0041007f  7507                  jne       short 00410088h
  00410081  6689ae9a000000        mov       [esi+9Ah],bp

LAB_00410088:
  00410088  663bc3                cmp       ax,bx
  0041008b  7514                  jne       short 004100A1h
  0041008d  b900040000            mov       ecx,400h
  00410092  66899e9a000000        mov       [esi+9Ah],bx
  00410099  66014e28              add       [esi+28h],cx
  0041009d  66014e2a              add       [esi+2Ah],cx

LAB_004100a1:
  004100a1  bf02000000            mov       edi,2
  004100a6  663bc7                cmp       ax,di
  004100a9  7514                  jne       short 004100BFh
  004100ab  b8e0fcffff            mov       eax,0FFFFFCE0h
  004100b0  6689be9a000000        mov       [esi+9Ah],di
  004100b7  66014628              add       [esi+28h],ax
  004100bb  6601462a              add       [esi+2Ah],ax

LAB_004100bf:
  004100bf  668b869a000000        mov       ax,[esi+9Ah]
  004100c6  663bc3                cmp       ax,bx
  004100c9  7405                  je        short 004100D0h
  004100cb  663bc7                cmp       ax,di
  004100ce  7502                  jne       short 004100D2h

LAB_004100d0:
  004100d0  8bfb                  mov       edi,ebx

LAB_004100d2:
  004100d2  e869701300            call      00547140h
  004100d7  99                    cdq
  004100d8  b905000000            mov       ecx,5
  004100dd  66896e14              mov       [esi+14h],bp
  004100e1  f7f9                  idiv      ecx
  004100e3  66896e16              mov       [esi+16h],bp
  004100e7  c6464000              mov       byte [esi+40h],0
  004100eb  83c208                add       edx,8
  004100ee  0fafd7                imul      edx,edi
  004100f1  66895612              mov       [esi+12h],dx
  004100f5  eb05                  jmp       short 004100FCh

LAB_004100f7:
  004100f7  bb01000000            mov       ebx,1

LAB_004100fc:
  004100fc  66395e10              cmp       [esi+10h],bx
  00410100  7567                  jne       short 00410169h
  00410102  8b2e                  mov       ebp,[esi]
  00410104  81cd00400000          or        ebp,4000h
  0041010a  66ff4614              inc       word [esi+14h]
  0041010e  668b4e14              mov       cx,[esi+14h]
  00410112  892e                  mov       [esi],ebp
  00410114  6683f903              cmp       cx,3
  00410118  8bc5                  mov       eax,ebp
  0041011a  7e16                  jle       short 00410132h
  0041011c  668b8e9a000000        mov       cx,[esi+9Ah]
  00410123  6685c9                test      cx,cx
  00410126  7405                  je        short 0041012Dh
  00410128  663bcb                cmp       cx,bx
  0041012b  7505                  jne       short 00410132h

LAB_0041012d:
  0041012d  80e4bf                and       ah,0BFh
  00410130  8906                  mov       [esi],eax

LAB_00410132:
  00410132  e809701300            call      00547140h
  00410137  99                    cdq
  00410138  b90a000000            mov       ecx,0Ah
  0041013d  f7f9                  idiv      ecx
  0041013f  66837e1408            cmp       word [esi+14h],8
  00410144  7f05                  jg        short 0041014Bh
  00410146  6685d2                test      dx,dx
  00410149  751e                  jne       short 00410169h

LAB_0041014b:
  0041014b  8b0e                  mov       ecx,[esi]
  0041014d  8b4620                mov       eax,[esi+20h]
  00410150  80e5bf                and       ch,0BFh
  00410153  050010ffff            add       eax,0FFFF1000h
  00410158  66c746100200          mov       word [esi+10h],2
  0041015e  66c746140000          mov       word [esi+14h],0
  00410164  890e                  mov       [esi],ecx
  00410166  894620                mov       [esi+20h],eax

LAB_00410169:
  00410169  66837e1002            cmp       word [esi+10h],2
  0041016e  754f                  jne       short 004101BFh
  00410170  e8cb6f1300            call      00547140h
  00410175  99                    cdq
  00410176  b914000000            mov       ecx,14h
  0041017b  f7f9                  idiv      ecx
  0041017d  8b4e1c                mov       ecx,[esi+1Ch]
  00410180  83ea0a                sub       edx,0Ah
  00410183  c1e20c                shl       edx,0Ch
  00410186  03ca                  add       ecx,edx
  00410188  894e1c                mov       [esi+1Ch],ecx
  0041018b  e8b06f1300            call      00547140h
  00410190  99                    cdq
  00410191  b914000000            mov       ecx,14h
  00410196  8b5e24                mov       ebx,[esi+24h]
  00410199  f7f9                  idiv      ecx
  0041019b  83ea0a                sub       edx,0Ah
  0041019e  c1e20c                shl       edx,0Ch
  004101a1  03da                  add       ebx,edx
  004101a3  66ff4616              inc       word [esi+16h]
  004101a7  668b4616              mov       ax,[esi+16h]
  004101ab  895e24                mov       [esi+24h],ebx
  004101ae  66c1f802              sar       ax,2
  004101b2  3c04                  cmp       al,4
  004101b4  884640                mov       [esi+40h],al
  004101b7  7506                  jne       short 004101BFh
  004101b9  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004101bf:
  004101bf  5f                    pop       edi
  004101c0  5e                    pop       esi
  004101c1  5d                    pop       ebp
  004101c2  5b                    pop       ebx
  004101c3  59                    pop       ecx
  004101c4  c3                    ret

22 cible(s) de saut interne : 0x0040fc17, 0x0040fc2a, 0x0040fc3d, 0x0040fd18, 0x0040fd2c, 0x0040fd47, 0x0040fe20, 0x0040fe30, 0x0040ff09, 0x0040ffe8, 0x00410088, 0x004100a1, 0x004100bf, 0x004100d0, 0x004100d2, 0x004100f7, 0x004100fc, 0x0041012d, 0x00410132, 0x0041014b, 0x00410169, 0x004101bf
