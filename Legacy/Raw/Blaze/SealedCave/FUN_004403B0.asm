  004403b0  53                    push      ebx
  004403b1  56                    push      esi
  004403b2  8b74240c              mov       esi,[esp+0Ch]
  004403b6  33db                  xor       ebx,ebx
  004403b8  57                    push      edi
  004403b9  bf01000000            mov       edi,1
  004403be  66395e10              cmp       [esi+10h],bx
  004403c2  0f85d1000000          jne       near 00440499h
  004403c8  c7463cf8145500        mov       dword [esi+3Ch],5514F8h
  004403cf  885e40                mov       [esi+40h],bl
  004403d2  c6460a0c              mov       byte [esi+0Ah],0Ch
  004403d6  885e0b                mov       [esi+0Bh],bl
  004403d9  66897e10              mov       [esi+10h],di
  004403dd  e85e6d1000            call      00547140h
  004403e2  99                    cdq
  004403e3  33c2                  xor       eax,edx
  004403e5  2bc2                  sub       eax,edx
  004403e7  25ff0f0000            and       eax,0FFFh
  004403ec  33c2                  xor       eax,edx
  004403ee  2bc2                  sub       eax,edx
  004403f0  66894612              mov       [esi+12h],ax
  004403f4  e8476d1000            call      00547140h
  004403f9  99                    cdq
  004403fa  33c2                  xor       eax,edx
  004403fc  2bc2                  sub       eax,edx
  004403fe  25ff030000            and       eax,3FFh
  00440403  33c2                  xor       eax,edx
  00440405  2bc2                  sub       eax,edx
  00440407  66894614              mov       [esi+14h],ax
  0044040b  e8306d1000            call      00547140h
  00440410  99                    cdq
  00440411  b964000000            mov       ecx,64h
  00440416  f7f9                  idiv      ecx
  00440418  81c290010000          add       edx,190h
  0044041e  6689969a000000        mov       [esi+9Ah],dx
  00440425  e8166d1000            call      00547140h
  0044042a  99                    cdq
  0044042b  b90f000000            mov       ecx,0Fh
  00440430  f7f9                  idiv      ecx
  00440432  8b4620                mov       eax,[esi+20h]
  00440435  8b4e24                mov       ecx,[esi+24h]
  00440438  894654                mov       [esi+54h],eax
  0044043b  894e58                mov       [esi+58h],ecx
  0044043e  83c20d                add       edx,0Dh
  00440441  89565c                mov       [esi+5Ch],edx
  00440444  8b561c                mov       edx,[esi+1Ch]
  00440447  895650                mov       [esi+50h],edx
  0044044a  e8f16c1000            call      00547140h
  0044044f  99                    cdq
  00440450  33c2                  xor       eax,edx
  00440452  885e38                mov       [esi+38h],bl
  00440455  2bc2                  sub       eax,edx
  00440457  885e39                mov       [esi+39h],bl
  0044045a  25ff010000            and       eax,1FFh
  0044045f  885e3a                mov       [esi+3Ah],bl
  00440462  33c2                  xor       eax,edx
  00440464  2bc2                  sub       eax,edx
  00440466  0500040000            add       eax,400h
  0044046b  66894628              mov       [esi+28h],ax
  0044046f  6689462a              mov       [esi+2Ah],ax
  00440473  e8c86c1000            call      00547140h
  00440478  99                    cdq
  00440479  b964000000            mov       ecx,64h
  0044047e  f7f9                  idiv      ecx
  00440480  895648                mov       [esi+48h],edx
  00440483  e8b86c1000            call      00547140h
  00440488  99                    cdq
  00440489  b964000000            mov       ecx,64h
  0044048e  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  00440494  f7f9                  idiv      ecx
  00440496  89564c                mov       [esi+4Ch],edx
  00440499  66397e10              cmp       [esi+10h],di
  0044049d  0f8513010000          jne       near 004405B6h
  004404a3  0fbf5614              movsx     edx,word [esi+14h]
  004404a7  52                    push      edx
  004404a8  e869551000            call      00545A16h
  004404ad  8bf8                  mov       edi,eax
  004404af  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004404b6  0fbf4e12              movsx     ecx,word [esi+12h]
  004404ba  0faff8                imul      edi,eax
  004404bd  51                    push      ecx
  004404be  c1ff0c                sar       edi,0Ch
  004404c1  e856551000            call      00545A1Ch
  004404c6  0faff8                imul      edi,eax
  004404c9  037e50                add       edi,[esi+50h]
  004404cc  0fbf5614              movsx     edx,word [esi+14h]
  004404d0  52                    push      edx
  004404d1  897e1c                mov       [esi+1Ch],edi
  004404d4  e83d551000            call      00545A16h
  004404d9  8bf8                  mov       edi,eax
  004404db  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004404e2  0fbf4e12              movsx     ecx,word [esi+12h]
  004404e6  0faff8                imul      edi,eax
  004404e9  51                    push      ecx
  004404ea  c1ff0c                sar       edi,0Ch
  004404ed  e824551000            call      00545A16h
  004404f2  0faff8                imul      edi,eax
  004404f5  8b4e58                mov       ecx,[esi+58h]
  004404f8  0fbf5614              movsx     edx,word [esi+14h]
  004404fc  03f9                  add       edi,ecx
  004404fe  52                    push      edx
  004404ff  897e24                mov       [esi+24h],edi
  00440502  668bbe9a000000        mov       di,[esi+9Ah]
  00440509  e80e551000            call      00545A1Ch
  0044050e  0fbfcf                movsx     ecx,di
  00440511  0fafc1                imul      eax,ecx
  00440514  8b4e54                mov       ecx,[esi+54h]
  00440517  668b5648              mov       dx,[esi+48h]
  0044051b  66015612              add       [esi+12h],dx
  0044051f  668b564c              mov       dx,[esi+4Ch]
  00440523  66015614              add       [esi+14h],dx
  00440527  03c1                  add       eax,ecx
  00440529  668b4e12              mov       cx,[esi+12h]
  0044052d  668b5614              mov       dx,[esi+14h]
  00440531  894620                mov       [esi+20h],eax
  00440534  668bc7                mov       ax,di
  00440537  662b465c              sub       ax,[esi+5Ch]
  0044053b  663d1801              cmp       ax,118h
  0044053f  6689869a000000        mov       [esi+9Ah],ax
  00440546  7d16                  jge       short 0044055Eh
  00440548  8b4648                mov       eax,[esi+48h]
  0044054b  d1f8                  sar       eax,1
  0044054d  2bc8                  sub       ecx,eax
  0044054f  66894e12              mov       [esi+12h],cx
  00440553  8b4e4c                mov       ecx,[esi+4Ch]
  00440556  d1f9                  sar       ecx,1
  00440558  2bd1                  sub       edx,ecx
  0044055a  66895614              mov       [esi+14h],dx
  0044055e  8b7e5c                mov       edi,[esi+5Ch]
  00440561  6a0a                  push      0Ah
  00440563  6a0a                  push      0Ah
  00440565  4f                    dec       edi
  00440566  6a0a                  push      0Ah
  00440568  56                    push      esi
  00440569  897e5c                mov       [esi+5Ch],edi
  0044056c  e86f48fcff            call      00404DE0h
  00440571  8b4e5c                mov       ecx,[esi+5Ch]
  00440574  b802000000            mov       eax,2
  00440579  83c410                add       esp,10h
  0044057c  3bc8                  cmp       ecx,eax
  0044057e  7f03                  jg        short 00440583h
  00440580  89465c                mov       [esi+5Ch],eax
  00440583  66837e163c            cmp       word [esi+16h],3Ch
  00440588  7e2c                  jle       short 004405B6h
  0044058a  66c746100300          mov       word [esi+10h],3
  00440590  e8ab6b1000            call      00547140h
  00440595  99                    cdq
  00440596  b914000000            mov       ecx,14h
  0044059b  f7f9                  idiv      ecx
  0044059d  83c228                add       edx,28h
  004405a0  89565c                mov       [esi+5Ch],edx
  004405a3  e8986b1000            call      00547140h
  004405a8  99                    cdq
  004405a9  b996000000            mov       ecx,96h
  004405ae  f7f9                  idiv      ecx
  004405b0  83c246                add       edx,46h
  004405b3  895644                mov       [esi+44h],edx
  004405b6  66837e1003            cmp       word [esi+10h],3
  004405bb  0f85da000000          jne       near 0044069Bh
  004405c1  0fbf5614              movsx     edx,word [esi+14h]
  004405c5  52                    push      edx
  004405c6  e84b541000            call      00545A16h
  004405cb  8bf8                  mov       edi,eax
  004405cd  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004405d4  0fbf4e12              movsx     ecx,word [esi+12h]
  004405d8  0faff8                imul      edi,eax
  004405db  51                    push      ecx
  004405dc  c1ff0c                sar       edi,0Ch
  004405df  e838541000            call      00545A1Ch
  004405e4  0faff8                imul      edi,eax
  004405e7  037e50                add       edi,[esi+50h]
  004405ea  0fbf5614              movsx     edx,word [esi+14h]
  004405ee  52                    push      edx
  004405ef  897e1c                mov       [esi+1Ch],edi
  004405f2  e81f541000            call      00545A16h
  004405f7  8bf8                  mov       edi,eax
  004405f9  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00440600  0fbf4e12              movsx     ecx,word [esi+12h]
  00440604  0faff8                imul      edi,eax
  00440607  51                    push      ecx
  00440608  c1ff0c                sar       edi,0Ch
  0044060b  e806541000            call      00545A16h
  00440610  0faff8                imul      edi,eax
  00440613  8b4e58                mov       ecx,[esi+58h]
  00440616  0fbf5614              movsx     edx,word [esi+14h]
  0044061a  03f9                  add       edi,ecx
  0044061c  52                    push      edx
  0044061d  897e24                mov       [esi+24h],edi
  00440620  668bbe9a000000        mov       di,[esi+9Ah]
  00440627  e8f0531000            call      00545A1Ch
  0044062c  0fbfcf                movsx     ecx,di
  0044062f  0fafc1                imul      eax,ecx
  00440632  8b4e54                mov       ecx,[esi+54h]
  00440635  668b565c              mov       dx,[esi+5Ch]
  00440639  6afb                  push      0FFFFFFFBh
  0044063b  6af9                  push      0FFFFFFF9h
  0044063d  03c1                  add       eax,ecx
  0044063f  6603d7                add       dx,di
  00440642  6af9                  push      0FFFFFFF9h
  00440644  56                    push      esi
  00440645  894620                mov       [esi+20h],eax
  00440648  6689969a000000        mov       [esi+9Ah],dx
  0044064f  e88c47fcff            call      00404DE0h
  00440654  668b4644              mov       ax,[esi+44h]
  00440658  ba00020000            mov       edx,200h
  0044065d  66d1e0                shl       ax,1
  00440660  66294628              sub       [esi+28h],ax
  00440664  668b4e28              mov       cx,[esi+28h]
  00440668  6629462a              sub       [esi+2Ah],ax
  0044066c  668b462a              mov       ax,[esi+2Ah]
  00440670  83c410                add       esp,10h
  00440673  663bca                cmp       cx,dx
  00440676  7d04                  jge       short 0044067Ch
  00440678  66895628              mov       [esi+28h],dx
  0044067c  663bc2                cmp       ax,dx
  0044067f  7d04                  jge       short 00440685h
  00440681  6689562a              mov       [esi+2Ah],dx
  00440685  8b4e44                mov       ecx,[esi+44h]
  00440688  8a463a                mov       al,[esi+3Ah]
  0044068b  83c1fb                add       ecx,0FFFFFFFBh
  0044068e  3ac3                  cmp       al,bl
  00440690  894e44                mov       [esi+44h],ecx
  00440693  7506                  jne       short 0044069Bh
  00440695  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0044069b  66ff4616              inc       word [esi+16h]
  0044069f  5f                    pop       edi
  004406a0  5e                    pop       esi
  004406a1  5b                    pop       ebx
  004406a2  c3                    ret
