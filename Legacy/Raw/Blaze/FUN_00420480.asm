
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00420480  section .text  offset fichier 0x1f880  864 octets

  00420480  51                    push      ecx
  00420481  53                    push      ebx
  00420482  55                    push      ebp
  00420483  56                    push      esi
  00420484  8b742414              mov       esi,[esp+14h]
  00420488  33db                  xor       ebx,ebx
  0042048a  57                    push      edi
  0042048b  8b466c                mov       eax,[esi+6Ch]
  0042048e  8b486c                mov       ecx,[eax+6Ch]
  00420491  8b465c                mov       eax,[esi+5Ch]
  00420494  3bc3                  cmp       eax,ebx
  00420496  894c2418              mov       [esp+18h],ecx
  0042049a  0f8593010000          jne       near 00420633h
  004204a0  66395e10              cmp       [esi+10h],bx
  004204a4  7559                  jne       short 004204FFh
  004204a6  66b81900              mov       ax,19h
  004204aa  bd01000000            mov       ebp,1
  004204af  662b4612              sub       ax,[esi+12h]
  004204b3  c6460407              mov       byte [esi+4],7
  004204b7  c6460a02              mov       byte [esi+0Ah],2
  004204bb  885e0b                mov       [esi+0Bh],bl
  004204be  c64638fa              mov       byte [esi+38h],0FAh
  004204c2  c64639fa              mov       byte [esi+39h],0FAh
  004204c6  8d0480                lea       eax,[eax+eax*4]
  004204c9  c6463afa              mov       byte [esi+3Ah],0FAh
  004204cd  66895e28              mov       [esi+28h],bx
  004204d1  66895e2c              mov       [esi+2Ch],bx
  004204d5  8d1480                lea       edx,[eax+eax*4]
  004204d8  668b4636              mov       ax,[esi+36h]
  004204dc  24fe                  and       al,0FEh
  004204de  66896e10              mov       [esi+10h],bp
  004204e2  c1e204                shl       edx,4
  004204e5  0c02                  or        al,2
  004204e7  6689562a              mov       [esi+2Ah],dx
  004204eb  66895e12              mov       [esi+12h],bx
  004204ef  66895e14              mov       [esi+14h],bx
  004204f3  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  004204f9  66894636              mov       [esi+36h],ax
  004204fd  eb05                  jmp       short 00420504h

LAB_004204ff:
  004204ff  bd01000000            mov       ebp,1

LAB_00420504:
  00420504  66396e10              cmp       [esi+10h],bp
  00420508  0f852a010000          jne       near 00420638h
  0042050e  6afb                  push      0FFFFFFFBh
  00420510  6af9                  push      0FFFFFFF9h
  00420512  6af9                  push      0FFFFFFF9h
  00420514  56                    push      esi
  00420515  e8c648feff            call      00404DE0h
  0042051a  668b4628              mov       ax,[esi+28h]
  0042051e  ba30750000            mov       edx,7530h
  00420523  0fbfc8                movsx     ecx,ax
  00420526  2bd1                  sub       edx,ecx
  00420528  bf30750000            mov       edi,7530h
  0042052d  c1fa03                sar       edx,3
  00420530  83c410                add       esp,10h
  00420533  8d0c02                lea       ecx,[edx+eax]
  00420536  668b462c              mov       ax,[esi+2Ch]
  0042053a  0fbfd0                movsx     edx,ax
  0042053d  2bfa                  sub       edi,edx
  0042053f  66894e28              mov       [esi+28h],cx
  00420543  c1ff03                sar       edi,3
  00420546  03f8                  add       edi,eax
  00420548  8a463a                mov       al,[esi+3Ah]
  0042054b  3ac3                  cmp       al,bl
  0042054d  66897e2c              mov       [esi+2Ch],di
  00420551  7506                  jne       short 00420559h
  00420553  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00420559:
  00420559  66395e16              cmp       [esi+16h],bx
  0042055d  0f85d5000000          jne       near 00420638h
  00420563  6681f97869            cmp       cx,6978h
  00420568  0f8dca000000          jge       near 00420638h
  0042056e  33ed                  xor       ebp,ebp
  00420570  bfc0935e00            mov       edi,5E93C0h
  00420575  c744241004000000      mov       dword [esp+10h],4

LAB_0042057d:
  0042057d  8b07                  mov       eax,[edi]
  0042057f  a900000080            test      eax,80000000h
  00420584  0f8493000000          je        near 0042061Dh
  0042058a  a900000050            test      eax,50000000h
  0042058f  0f8588000000          jne       near 0042061Dh
  00420595  8b471c                mov       eax,[edi+1Ch]
  00420598  8b561c                mov       edx,[esi+1Ch]
  0042059b  8b4f24                mov       ecx,[edi+24h]
  0042059e  2bc2                  sub       eax,edx
  004205a0  8b5624                mov       edx,[esi+24h]
  004205a3  c1f80c                sar       eax,0Ch
  004205a6  2bca                  sub       ecx,edx
  004205a8  8bd0                  mov       edx,eax
  004205aa  c1f90c                sar       ecx,0Ch
  004205ad  0fafd0                imul      edx,eax
  004205b0  8bc1                  mov       eax,ecx
  004205b2  0fafc1                imul      eax,ecx
  004205b5  03d0                  add       edx,eax
  004205b7  52                    push      edx
  004205b8  e853541200            call      00545A10h
  004205bd  0fbf5628              movsx     edx,word [esi+28h]
  004205c1  8bc8                  mov       ecx,eax
  004205c3  b81f85eb51            mov       eax,51EB851Fh
  004205c8  f7ea                  imul      edx
  004205ca  c1fa04                sar       edx,4
  004205cd  8bc2                  mov       eax,edx
  004205cf  c1e81f                shr       eax,1Fh
  004205d2  03d0                  add       edx,eax
  004205d4  3bca                  cmp       ecx,edx
  004205d6  7d45                  jge       short 0042061Dh
  004205d8  668b4614              mov       ax,[esi+14h]
  004205dc  ba01000000            mov       edx,1
  004205e1  8bcd                  mov       ecx,ebp
  004205e3  d3e2                  shl       edx,cl
  004205e5  0fbfc8                movsx     ecx,ax
  004205e8  85d1                  test      ecx,edx
  004205ea  7531                  jne       short 0042061Dh
  004205ec  ba01000000            mov       edx,1
  004205f1  8bcd                  mov       ecx,ebp
  004205f3  d3e2                  shl       edx,cl
  004205f5  53                    push      ebx
  004205f6  57                    push      edi
  004205f7  0bd0                  or        edx,eax
  004205f9  8b4644                mov       eax,[esi+44h]
  004205fc  3bc3                  cmp       eax,ebx
  004205fe  66895614              mov       [esi+14h],dx
  00420602  750c                  jne       short 00420610h
  00420604  8b442420              mov       eax,[esp+20h]
  00420608  50                    push      eax
  00420609  e8d2010000            call      004207E0h
  0042060e  eb0a                  jmp       short 0042061Ah

LAB_00420610:
  00420610  8b4c2420              mov       ecx,[esp+20h]
  00420614  51                    push      ecx
  00420615  e806020000            call      00420820h

LAB_0042061a:
  0042061a  83c40c                add       esp,0Ch

LAB_0042061d:
  0042061d  8b442410              mov       eax,[esp+10h]
  00420621  45                    inc       ebp
  00420622  81c79c000000          add       edi,9Ch
  00420628  48                    dec       eax
  00420629  89442410              mov       [esp+10h],eax
  0042062d  0f854affffff          jne       near 0042057Dh

LAB_00420633:
  00420633  bd01000000            mov       ebp,1

LAB_00420638:
  00420638  396e5c                cmp       [esi+5Ch],ebp
  0042063b  0f858c010000          jne       near 004207CDh
  00420641  66395e10              cmp       [esi+10h],bx
  00420645  0f859b000000          jne       near 004206E6h
  0042064b  c6460430              mov       byte [esi+4],30h
  0042064f  c6460a02              mov       byte [esi+0Ah],2
  00420653  885e0b                mov       [esi+0Bh],bl
  00420656  e8e56a1200            call      00547140h
  0042065b  99                    cdq
  0042065c  b9c8000000            mov       ecx,0C8h
  00420661  f7f9                  idiv      ecx
  00420663  03d1                  add       edx,ecx
  00420665  66895628              mov       [esi+28h],dx
  00420669  e8d26a1200            call      00547140h
  0042066e  99                    cdq
  0042066f  b9c8000000            mov       ecx,0C8h
  00420674  f7f9                  idiv      ecx
  00420676  03d1                  add       edx,ecx
  00420678  6689562a              mov       [esi+2Ah],dx
  0042067c  e8bf6a1200            call      00547140h
  00420681  99                    cdq
  00420682  b9c8000000            mov       ecx,0C8h
  00420687  f7f9                  idiv      ecx
  00420689  03d1                  add       edx,ecx
  0042068b  6689562c              mov       [esi+2Ch],dx
  0042068f  e8ac6a1200            call      00547140h
  00420694  99                    cdq
  00420695  33c2                  xor       eax,edx
  00420697  2bc2                  sub       eax,edx
  00420699  25ff0f0000            and       eax,0FFFh
  0042069e  33c2                  xor       eax,edx
  004206a0  2bc2                  sub       eax,edx
  004206a2  66894632              mov       [esi+32h],ax
  004206a6  e8956a1200            call      00547140h
  004206ab  99                    cdq
  004206ac  b9f4010000            mov       ecx,1F4h
  004206b1  885e38                mov       [esi+38h],bl
  004206b4  f7f9                  idiv      ecx
  004206b6  885e39                mov       [esi+39h],bl
  004206b9  885e3a                mov       [esi+3Ah],bl
  004206bc  66896e10              mov       [esi+10h],bp
  004206c0  66895e12              mov       [esi+12h],bx
  004206c4  66895630              mov       [esi+30h],dx
  004206c8  e8736a1200            call      00547140h
  004206cd  99                    cdq
  004206ce  b9c8000000            mov       ecx,0C8h
  004206d3  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  004206d9  f7f9                  idiv      ecx
  004206db  8b4620                mov       eax,[esi+20h]
  004206de  c1e20c                shl       edx,0Ch
  004206e1  03c2                  add       eax,edx
  004206e3  894620                mov       [esi+20h],eax

LAB_004206e6:
  004206e6  66396e10              cmp       [esi+10h],bp
  004206ea  b96affffff            mov       ecx,0FFFFFF6Ah
  004206ef  bf64000000            mov       edi,64h
  004206f4  7570                  jne       short 00420766h
  004206f6  8a4638                mov       al,[esi+38h]
  004206f9  bafa000000            mov       edx,0FAh
  004206fe  8be8                  mov       ebp,eax
  00420700  66014e32              add       [esi+32h],cx
  00420704  81e5ff000000          and       ebp,0FFh
  0042070a  2bd5                  sub       edx,ebp
  0042070c  d1fa                  sar       edx,1
  0042070e  02d0                  add       dl,al
  00420710  8a4639                mov       al,[esi+39h]
  00420713  8be8                  mov       ebp,eax
  00420715  885638                mov       [esi+38h],dl
  00420718  81e5ff000000          and       ebp,0FFh
  0042071e  bafa000000            mov       edx,0FAh
  00420723  2bd5                  sub       edx,ebp
  00420725  d1fa                  sar       edx,1
  00420727  02d0                  add       dl,al
  00420729  8a463a                mov       al,[esi+3Ah]
  0042072c  8be8                  mov       ebp,eax
  0042072e  885639                mov       [esi+39h],dl
  00420731  81e5ff000000          and       ebp,0FFh
  00420737  bafa000000            mov       edx,0FAh
  0042073c  2bd5                  sub       edx,ebp
  0042073e  d1fa                  sar       edx,1
  00420740  02d0                  add       dl,al
  00420742  66ff4612              inc       word [esi+12h]
  00420746  66837e1205            cmp       word [esi+12h],5
  0042074b  88563a                mov       [esi+3Ah],dl
  0042074e  7e0a                  jle       short 0042075Ah
  00420750  66c746100200          mov       word [esi+10h],2
  00420756  66895e12              mov       [esi+12h],bx

LAB_0042075a:
  0042075a  66017e28              add       [esi+28h],di
  0042075e  66017e2a              add       [esi+2Ah],di
  00420762  66017e2c              add       [esi+2Ch],di

LAB_00420766:
  00420766  66837e1002            cmp       word [esi+10h],2
  0042076b  7560                  jne       short 004207CDh
  0042076d  8a4638                mov       al,[esi+38h]
  00420770  66014e32              add       [esi+32h],cx
  00420774  8bd0                  mov       edx,eax
  00420776  81e2ff000000          and       edx,0FFh
  0042077c  f7da                  neg       edx
  0042077e  c1fa03                sar       edx,3
  00420781  02d0                  add       dl,al
  00420783  8a4639                mov       al,[esi+39h]
  00420786  885638                mov       [esi+38h],dl
  00420789  8bd0                  mov       edx,eax
  0042078b  81e2ff000000          and       edx,0FFh
  00420791  f7da                  neg       edx
  00420793  c1fa03                sar       edx,3
  00420796  02d0                  add       dl,al
  00420798  8a463a                mov       al,[esi+3Ah]
  0042079b  885639                mov       [esi+39h],dl
  0042079e  8bd0                  mov       edx,eax
  004207a0  81e2ff000000          and       edx,0FFh
  004207a6  f7da                  neg       edx
  004207a8  c1fa03                sar       edx,3
  004207ab  02d0                  add       dl,al
  004207ad  66ff4612              inc       word [esi+12h]
  004207b1  66837e1214            cmp       word [esi+12h],14h
  004207b6  88563a                mov       [esi+3Ah],dl
  004207b9  7e06                  jle       short 004207C1h
  004207bb  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004207c1:
  004207c1  66017e28              add       [esi+28h],di
  004207c5  66017e2a              add       [esi+2Ah],di
  004207c9  66017e2c              add       [esi+2Ch],di

LAB_004207cd:
  004207cd  5f                    pop       edi
  004207ce  5e                    pop       esi
  004207cf  5d                    pop       ebp
  004207d0  5b                    pop       ebx
  004207d1  59                    pop       ecx
  004207d2  c3                    ret

14 cible(s) de saut interne : 0x004204ff, 0x00420504, 0x00420559, 0x0042057d, 0x00420610, 0x0042061a, 0x0042061d, 0x00420633, 0x00420638, 0x004206e6, 0x0042075a, 0x00420766, 0x004207c1, 0x004207cd
