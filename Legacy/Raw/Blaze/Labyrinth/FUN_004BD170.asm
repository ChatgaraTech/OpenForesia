Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004bd170  section .text  offset fichier 0xbc570  3280 octets

  004bd170  81ec88000000          sub       esp,88h
  004bd176  53                    push      ebx
  004bd177  55                    push      ebp
  004bd178  56                    push      esi
  004bd179  8bb42498000000        mov       esi,[esp+98h]
  004bd180  33db                  xor       ebx,ebx
  004bd182  57                    push      edi
  004bd183  8a4641                mov       al,[esi+41h]
  004bd186  66895c2428            mov       [esp+28h],bx
  004bd18b  3ac3                  cmp       al,bl
  004bd18d  66c744242a1e00        mov       word [esp+2Ah],1Eh
  004bd194  66c744242c3200        mov       word [esp+2Ch],32h
  004bd19b  66c744242e6400        mov       word [esp+2Eh],64h
  004bd1a2  66895c2430            mov       [esp+30h],bx
  004bd1a7  66c74424325000        mov       word [esp+32h],50h
  004bd1ae  7533                  jne       short 004BD1E3h
  004bd1b0  53                    push      ebx
  004bd1b1  6a03                  push      3
  004bd1b3  56                    push      esi
  004bd1b4  c6464101              mov       byte [esi+41h],1
  004bd1b8  e8830c0000            call      004BDE40h
  004bd1bd  8b4620                mov       eax,[esi+20h]
  004bd1c0  83c40c                add       esp,0Ch
  004bd1c3  894648                mov       [esi+48h],eax
  004bd1c6  668b4616              mov       ax,[esi+16h]
  004bd1ca  663d0a00              cmp       ax,0Ah
  004bd1ce  7c0d                  jl        short 004BD1DDh
  004bd1d0  83c0f6                add       eax,0FFFFFFF6h
  004bd1d3  66894616              mov       [esi+16h],ax
  004bd1d7  8b06                  mov       eax,[esi]
  004bd1d9  24df                  and       al,0DFh
  004bd1db  eb04                  jmp       short 004BD1E1h

LAB_004bd1dd:
  004bd1dd  8b06                  mov       eax,[esi]
  004bd1df  0c20                  or        al,20h

LAB_004bd1e1:
  004bd1e1  8906                  mov       [esi],eax

LAB_004bd1e3:
  004bd1e3  8b4e6c                mov       ecx,[esi+6Ch]
  004bd1e6  8b561c                mov       edx,[esi+1Ch]
  004bd1e9  8b4624                mov       eax,[esi+24h]
  004bd1ec  8d7e1c                lea       edi,[esi+1Ch]
  004bd1ef  894c2424              mov       [esp+24h],ecx
  004bd1f3  8b4e64                mov       ecx,[esi+64h]
  004bd1f6  89542454              mov       [esp+54h],edx
  004bd1fa  8b5660                mov       edx,[esi+60h]
  004bd1fd  8944245c              mov       [esp+5Ch],eax
  004bd201  668b4632              mov       ax,[esi+32h]
  004bd205  894c243c              mov       [esp+3Ch],ecx
  004bd209  8b4e7c                mov       ecx,[esi+7Ch]
  004bd20c  89542444              mov       [esp+44h],edx
  004bd210  668b9690000000        mov       dx,[esi+90h]
  004bd217  6689842486000000      mov       [esp+86h],ax
  004bd21f  8b8680000000          mov       eax,[esi+80h]
  004bd225  894c2448              mov       [esp+48h],ecx
  004bd229  8b8e84000000          mov       ecx,[esi+84h]
  004bd22f  668954244c            mov       [esp+4Ch],dx
  004bd234  8b9688000000          mov       edx,[esi+88h]
  004bd23a  89442450              mov       [esp+50h],eax
  004bd23e  8b868c000000          mov       eax,[esi+8Ch]
  004bd244  894c2440              mov       [esp+40h],ecx
  004bd248  89542434              mov       [esp+34h],edx
  004bd24c  8d4c2460              lea       ecx,[esp+60h]
  004bd250  89442438              mov       [esp+38h],eax
  004bd254  8d542464              lea       edx,[esp+64h]
  004bd258  51                    push      ecx
  004bd259  8d44246c              lea       eax,[esp+6Ch]
  004bd25d  52                    push      edx
  004bd25e  50                    push      eax
  004bd25f  56                    push      esi
  004bd260  e81bb70700            call      00538980h
  004bd265  83c410                add       esp,10h
  004bd268  8d4c246c              lea       ecx,[esp+6Ch]
  004bd26c  8d54241c              lea       edx,[esp+1Ch]
  004bd270  8d442414              lea       eax,[esp+14h]
  004bd274  51                    push      ecx
  004bd275  52                    push      edx
  004bd276  8d4c2420              lea       ecx,[esp+20h]
  004bd27a  50                    push      eax
  004bd27b  8d54241c              lea       edx,[esp+1Ch]
  004bd27f  51                    push      ecx
  004bd280  52                    push      edx
  004bd281  56                    push      esi
  004bd282  e8998c0700            call      00535F20h
  004bd287  8b4c2428              mov       ecx,[esp+28h]
  004bd28b  b840000000            mov       eax,40h
  004bd290  2bc1                  sub       eax,ecx
  004bd292  83c418                add       esp,18h
  004bd295  89442420              mov       [esp+20h],eax
  004bd299  8b44241c              mov       eax,[esp+1Ch]
  004bd29d  3bc3                  cmp       eax,ebx
  004bd29f  0f847f050000          je        near 004BD824h
  004bd2a5  8b2e                  mov       ebp,[esi]
  004bd2a7  33d2                  xor       edx,edx
  004bd2a9  8a5604                mov       dl,[esi+4]
  004bd2ac  81e5fffffffd          and       ebp,0FDFFFFFFh
  004bd2b2  0fbf5e18              movsx     ebx,word [esi+18h]
  004bd2b6  892e                  mov       [esi],ebp
  004bd2b8  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004bd2bf  f7c500000001          test      ebp,1000000h
  004bd2c5  8b149a                mov       edx,[edx+ebx*4]
  004bd2c8  741d                  je        short 004BD2E7h
  004bd2ca  66ff461a              inc       word [esi+1Ah]
  004bd2ce  668b5e1a              mov       bx,[esi+1Ah]
  004bd2d2  33ed                  xor       ebp,ebp
  004bd2d4  668b6a16              mov       bp,[edx+16h]
  004bd2d8  0fbfd3                movsx     edx,bx
  004bd2db  3bd5                  cmp       edx,ebp
  004bd2dd  7c2b                  jl        short 004BD30Ah
  004bd2df  33db                  xor       ebx,ebx
  004bd2e1  66895e1a              mov       [esi+1Ah],bx
  004bd2e5  eb25                  jmp       short 004BD30Ch

LAB_004bd2e7:
  004bd2e7  66ff461a              inc       word [esi+1Ah]
  004bd2eb  33db                  xor       ebx,ebx
  004bd2ed  668b5a16              mov       bx,[edx+16h]
  004bd2f1  0fbf561a              movsx     edx,word [esi+1Ah]
  004bd2f5  3bd3                  cmp       edx,ebx
  004bd2f7  7c11                  jl        short 004BD30Ah
  004bd2f9  668b561a              mov       dx,[esi+1Ah]
  004bd2fd  4a                    dec       edx
  004bd2fe  81cd00000002          or        ebp,2000000h
  004bd304  6689561a              mov       [esi+1Ah],dx
  004bd308  892e                  mov       [esi],ebp

LAB_004bd30a:
  004bd30a  33db                  xor       ebx,ebx

LAB_004bd30c:
  004bd30c  f60602                test      byte [esi],2
  004bd30f  7457                  je        short 004BD368h
  004bd311  8d442428              lea       eax,[esp+28h]
  004bd315  53                    push      ebx
  004bd316  50                    push      eax
  004bd317  57                    push      edi
  004bd318  56                    push      esi
  004bd319  e812c90500            call      00519C30h
  004bd31e  8be8                  mov       ebp,eax
  004bd320  a1b8e05d00            mov       eax,[5DE0B8h]
  004bd325  83c410                add       esp,10h
  004bd328  3bc3                  cmp       eax,ebx
  004bd32a  7434                  je        short 004BD360h
  004bd32c  8b4d00                mov       ecx,[ebp]
  004bd32f  55                    push      ebp
  004bd330  83c940                or        ecx,40h
  004bd333  56                    push      esi
  004bd334  66c785960000001000    mov       word [ebp+96h],10h
  004bd33d  894d00                mov       [ebp],ecx
  004bd340  e83bc80700            call      00539B80h
  004bd345  83c408                add       esp,8
  004bd348  83c51c                add       ebp,1Ch
  004bd34b  55                    push      ebp
  004bd34c  6800180000            push      1800h
  004bd351  6800030000            push      300h
  004bd356  6a03                  push      3
  004bd358  e8c3070800            call      0053DB20h
  004bd35d  83c410                add       esp,10h

LAB_004bd360:
  004bd360  8b44241c              mov       eax,[esp+1Ch]
  004bd364  8b4c2410              mov       ecx,[esp+10h]

LAB_004bd368:
  004bd368  33d2                  xor       edx,edx
  004bd36a  8a5642                mov       dl,[esi+42h]
  004bd36d  83fa09                cmp       edx,9
  004bd370  0f87c2030000          ja        near 004BD738h
  004bd376  ff249518de4b00        jmp       dword [edx*4+4BDE18h]
  004bd37d  66395e12              cmp       [esi+12h],bx
  004bd381  751e                  jne       short 004BD3A1h
  004bd383  8b0e                  mov       ecx,[esi]
  004bd385  b801000000            mov       eax,1
  004bd38a  81c900000001          or        ecx,1000000h
  004bd390  66894618              mov       [esi+18h],ax
  004bd394  66895e1a              mov       [esi+1Ah],bx
  004bd398  890e                  mov       [esi],ecx
  004bd39a  66894612              mov       [esi+12h],ax
  004bd39e  895e5c                mov       [esi+5Ch],ebx

LAB_004bd3a1:
  004bd3a1  0fbf4e10              movsx     ecx,word [esi+10h]
  004bd3a5  394c2418              cmp       [esp+18h],ecx
  004bd3a9  7d08                  jge       short 004BD3B3h
  004bd3ab  885e43                mov       [esi+43h],bl
  004bd3ae  e985030000            jmp       004BD738h

LAB_004bd3b3:
  004bd3b3  8a4643                mov       al,[esi+43h]
  004bd3b6  3ac3                  cmp       al,bl
  004bd3b8  0f8477030000          je        near 004BD735h
  004bd3be  fec8                  dec       al
  004bd3c0  884643                mov       [esi+43h],al
  004bd3c3  e96d030000            jmp       004BD735h
  004bd3c8  66395e12              cmp       [esi+12h],bx
  004bd3cc  751b                  jne       short 004BD3E9h
  004bd3ce  8b16                  mov       edx,[esi]
  004bd3d0  b801000000            mov       eax,1
  004bd3d5  81ca00000001          or        edx,1000000h
  004bd3db  66894618              mov       [esi+18h],ax
  004bd3df  66895e1a              mov       [esi+1Ah],bx
  004bd3e3  8916                  mov       [esi],edx
  004bd3e5  66894612              mov       [esi+12h],ax

LAB_004bd3e9:
  004bd3e9  8b442414              mov       eax,[esp+14h]
  004bd3ed  8d90c8000000          lea       edx,[eax+0C8h]
  004bd3f3  81e2ff0f0000          and       edx,0FFFh
  004bd3f9  81fa90010000          cmp       edx,190h
  004bd3ff  0f8e2d030000          jle       near 004BD732h
  004bd405  25ff0f0000            and       eax,0FFFh
  004bd40a  3d00080000            cmp       eax,800h
  004bd40f  7d0c                  jge       short 004BD41Dh
  004bd411  8d4132                lea       eax,[ecx+32h]
  004bd414  66014632              add       [esi+32h],ax
  004bd418  e918030000            jmp       004BD735h

LAB_004bd41d:
  004bd41d  b8ceffffff            mov       eax,0FFFFFFCEh
  004bd422  2bc1                  sub       eax,ecx
  004bd424  66014632              add       [esi+32h],ax
  004bd428  e908030000            jmp       004BD735h
  004bd42d  66395e12              cmp       [esi+12h],bx
  004bd431  752e                  jne       short 004BD461h
  004bd433  8b16                  mov       edx,[esi]
  004bd435  b801000000            mov       eax,1
  004bd43a  81ca00000001          or        edx,1000000h
  004bd440  66c746180200          mov       word [esi+18h],2
  004bd446  8916                  mov       [esi],edx
  004bd448  8d14c9                lea       edx,[ecx+ecx*8]
  004bd44b  c1e204                shl       edx,4
  004bd44e  81c200a00000          add       edx,0A000h
  004bd454  6689461a              mov       [esi+1Ah],ax
  004bd458  66894612              mov       [esi+12h],ax
  004bd45c  89565c                mov       [esi+5Ch],edx
  004bd45f  eb05                  jmp       short 004BD466h

LAB_004bd461:
  004bd461  b801000000            mov       eax,1

LAB_004bd466:
  004bd466  66394612              cmp       [esi+12h],ax
  004bd46a  0f85c8020000          jne       near 004BD738h
  004bd470  8b542418              mov       edx,[esp+18h]
  004bd474  6689461a              mov       [esi+1Ah],ax
  004bd478  0fbf4610              movsx     eax,word [esi+10h]
  004bd47c  3bd0                  cmp       edx,eax
  004bd47e  66c746180200          mov       word [esi+18h],2
  004bd484  7d06                  jge       short 004BD48Ch
  004bd486  885e43                mov       [esi+43h],bl
  004bd489  895e5c                mov       [esi+5Ch],ebx

LAB_004bd48c:
  004bd48c  8a4643                mov       al,[esi+43h]
  004bd48f  3ac3                  cmp       al,bl
  004bd491  7605                  jbe       short 004BD498h
  004bd493  fec8                  dec       al
  004bd495  884643                mov       [esi+43h],al

LAB_004bd498:
  004bd498  8b442414              mov       eax,[esp+14h]
  004bd49c  8d9000010000          lea       edx,[eax+100h]
  004bd4a2  81e2ff0f0000          and       edx,0FFFh
  004bd4a8  81fa00020000          cmp       edx,200h
  004bd4ae  0f8e84020000          jle       near 004BD738h
  004bd4b4  25ff0f0000            and       eax,0FFFh
  004bd4b9  3d00080000            cmp       eax,800h
  004bd4be  7d10                  jge       short 004BD4D0h
  004bd4c0  8d8c0980000000        lea       ecx,[ecx+ecx+80h]
  004bd4c7  66014e32              add       [esi+32h],cx
  004bd4cb  e968020000            jmp       004BD738h

LAB_004bd4d0:
  004bd4d0  8d0409                lea       eax,[ecx+ecx]
  004bd4d3  b980ffffff            mov       ecx,0FFFFFF80h
  004bd4d8  2bc8                  sub       ecx,eax
  004bd4da  66014e32              add       [esi+32h],cx
  004bd4de  e955020000            jmp       004BD738h
  004bd4e3  ba01000000            mov       edx,1
  004bd4e8  66395612              cmp       [esi+12h],dx
  004bd4ec  0f85c0000000          jne       near 004BD5B2h
  004bd4f2  66895618              mov       [esi+18h],dx
  004bd4f6  8a5643                mov       dl,[esi+43h]
  004bd4f9  3ad3                  cmp       dl,bl
  004bd4fb  7405                  je        short 004BD502h
  004bd4fd  feca                  dec       dl
  004bd4ff  885643                mov       [esi+43h],dl

LAB_004bd502:
  004bd502  83c170                add       ecx,70h
  004bd505  c1e108                shl       ecx,8
  004bd508  0fafc1                imul      eax,ecx
  004bd50b  894e5c                mov       [esi+5Ch],ecx
  004bd50e  0fbf4e10              movsx     ecx,word [esi+10h]
  004bd512  c1f80c                sar       eax,0Ch
  004bd515  51                    push      ecx
  004bd516  8be8                  mov       ebp,eax
  004bd518  e8ff840800            call      00545A1Ch
  004bd51d  0fafc5                imul      eax,ebp
  004bd520  0fbf5610              movsx     edx,word [esi+10h]
  004bd524  8b0f                  mov       ecx,[edi]
  004bd526  52                    push      edx
  004bd527  c1f80c                sar       eax,0Ch
  004bd52a  03c8                  add       ecx,eax
  004bd52c  890f                  mov       [edi],ecx
  004bd52e  e8e3840800            call      00545A16h
  004bd533  0fafc5                imul      eax,ebp
  004bd536  8b6e24                mov       ebp,[esi+24h]
  004bd539  895e5c                mov       [esi+5Ch],ebx
  004bd53c  c1f80c                sar       eax,0Ch
  004bd53f  03e8                  add       ebp,eax
  004bd541  8b442414              mov       eax,[esp+14h]
  004bd545  896e24                mov       [esi+24h],ebp
  004bd548  8d8800010000          lea       ecx,[eax+100h]
  004bd54e  81e1ff0f0000          and       ecx,0FFFh
  004bd554  81f900020000          cmp       ecx,200h
  004bd55a  7e28                  jle       short 004BD584h
  004bd55c  25ff0f0000            and       eax,0FFFh
  004bd561  3d00080000            cmp       eax,800h
  004bd566  7d0a                  jge       short 004BD572h
  004bd568  8b542410              mov       edx,[esp+10h]
  004bd56c  8d441232              lea       eax,[edx+edx+32h]
  004bd570  eb0e                  jmp       short 004BD580h

LAB_004bd572:
  004bd572  8b442410              mov       eax,[esp+10h]
  004bd576  8d0c00                lea       ecx,[eax+eax]
  004bd579  b8ceffffff            mov       eax,0FFFFFFCEh
  004bd57e  2bc1                  sub       eax,ecx

LAB_004bd580:
  004bd580  66014632              add       [esi+32h],ax

LAB_004bd584:
  004bd584  668b4614              mov       ax,[esi+14h]
  004bd588  668bd0                mov       dx,ax
  004bd58b  66c1fa03              sar       dx,3
  004bd58f  2bc2                  sub       eax,edx
  004bd591  663d0500              cmp       ax,5
  004bd595  66894614              mov       [esi+14h],ax
  004bd599  7d09                  jge       short 004BD5A4h
  004bd59b  885e43                mov       [esi+43h],bl
  004bd59e  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004bd5a4:
  004bd5a4  0500100000            add       eax,1000h
  004bd5a9  ba01000000            mov       edx,1
  004bd5ae  6689462c              mov       [esi+2Ch],ax

LAB_004bd5b2:
  004bd5b2  66395e12              cmp       [esi+12h],bx
  004bd5b6  0f857c010000          jne       near 004BD738h
  004bd5bc  8b0e                  mov       ecx,[esi]
  004bd5be  66895618              mov       [esi+18h],dx
  004bd5c2  81c900000001          or        ecx,1000000h
  004bd5c8  66895e1a              mov       [esi+1Ah],bx
  004bd5cc  890e                  mov       [esi],ecx
  004bd5ce  66895612              mov       [esi+12h],dx
  004bd5d2  e8699b0800            call      00547140h
  004bd5d7  99                    cdq
  004bd5d8  b914000000            mov       ecx,14h
  004bd5dd  f7f9                  idiv      ecx
  004bd5df  8b442410              mov       eax,[esp+10h]
  004bd5e3  8d4c020a              lea       ecx,[edx+eax+0Ah]
  004bd5e7  c1e104                shl       ecx,4
  004bd5ea  66894e14              mov       [esi+14h],cx
  004bd5ee  e945010000            jmp       004BD738h
  004bd5f3  668b5612              mov       dx,[esi+12h]
  004bd5f7  bf01000000            mov       edi,1
  004bd5fc  663bd7                cmp       dx,di
  004bd5ff  751b                  jne       short 004BD61Ch
  004bd601  0fbf4610              movsx     eax,word [esi+10h]
  004bd605  39442418              cmp       [esp+18h],eax
  004bd609  7d05                  jge       short 004BD610h
  004bd60b  885e43                mov       [esi+43h],bl
  004bd60e  eb0c                  jmp       short 004BD61Ch

LAB_004bd610:
  004bd610  8a4643                mov       al,[esi+43h]
  004bd613  3ac3                  cmp       al,bl
  004bd615  7405                  je        short 004BD61Ch
  004bd617  fec8                  dec       al
  004bd619  884643                mov       [esi+43h],al

LAB_004bd61c:
  004bd61c  663bd3                cmp       dx,bx
  004bd61f  0f8513010000          jne       near 004BD738h
  004bd625  66895e1a              mov       [esi+1Ah],bx
  004bd629  8b1e                  mov       ebx,[esi]
  004bd62b  d1f9                  sar       ecx,1
  004bd62d  81cb00000001          or        ebx,1000000h
  004bd633  81c100400000          add       ecx,4000h
  004bd639  66897e18              mov       [esi+18h],di
  004bd63d  891e                  mov       [esi],ebx
  004bd63f  66897e12              mov       [esi+12h],di
  004bd643  894e5c                mov       [esi+5Ch],ecx
  004bd646  e9ed000000            jmp       004BD738h
  004bd64b  668b5612              mov       dx,[esi+12h]
  004bd64f  6683fa01              cmp       dx,1
  004bd653  7575                  jne       short 004BD6CAh
  004bd655  8b465c                mov       eax,[esi+5Ch]
  004bd658  66895e18              mov       [esi+18h],bx
  004bd65c  8bf8                  mov       edi,eax
  004bd65e  c1ff04                sar       edi,4
  004bd661  2bc7                  sub       eax,edi
  004bd663  3d00700000            cmp       eax,7000h
  004bd668  89465c                mov       [esi+5Ch],eax
  004bd66b  7d12                  jge       short 004BD67Fh
  004bd66d  8b06                  mov       eax,[esi]
  004bd66f  885e43                mov       [esi+43h],bl
  004bd672  24fd                  and       al,0FDh
  004bd674  895e5c                mov       [esi+5Ch],ebx
  004bd677  66c7462c0010          mov       word [esi+2Ch],1000h
  004bd67d  8906                  mov       [esi],eax

LAB_004bd67f:
  004bd67f  8b442414              mov       eax,[esp+14h]
  004bd683  8db800010000          lea       edi,[eax+100h]
  004bd689  81e7ff0f0000          and       edi,0FFFh
  004bd68f  81ff00020000          cmp       edi,200h
  004bd695  7e23                  jle       short 004BD6BAh
  004bd697  25ff0f0000            and       eax,0FFFh
  004bd69c  3d00080000            cmp       eax,800h
  004bd6a1  7d09                  jge       short 004BD6ACh
  004bd6a3  8d8c0980000000        lea       ecx,[ecx+ecx+80h]
  004bd6aa  eb0a                  jmp       short 004BD6B6h

LAB_004bd6ac:
  004bd6ac  8d0409                lea       eax,[ecx+ecx]
  004bd6af  b980ffffff            mov       ecx,0FFFFFF80h
  004bd6b4  2bc8                  sub       ecx,eax

LAB_004bd6b6:
  004bd6b6  66014e32              add       [esi+32h],cx

LAB_004bd6ba:
  004bd6ba  8b4e5c                mov       ecx,[esi+5Ch]
  004bd6bd  c1f905                sar       ecx,5
  004bd6c0  81c100100000          add       ecx,1000h
  004bd6c6  66894e2c              mov       [esi+2Ch],cx

LAB_004bd6ca:
  004bd6ca  663bd3                cmp       dx,bx
  004bd6cd  7569                  jne       short 004BD738h
  004bd6cf  8b16                  mov       edx,[esi]
  004bd6d1  66895e18              mov       [esi+18h],bx
  004bd6d5  83ca02                or        edx,2
  004bd6d8  66895e1a              mov       [esi+1Ah],bx
  004bd6dc  8916                  mov       [esi],edx
  004bd6de  e85d9a0800            call      00547140h
  004bd6e3  99                    cdq
  004bd6e4  b932000000            mov       ecx,32h
  004bd6e9  66c746120100          mov       word [esi+12h],1
  004bd6ef  f7f9                  idiv      ecx
  004bd6f1  8b442410              mov       eax,[esp+10h]
  004bd6f5  24fc                  and       al,0FCh
  004bd6f7  8d0c90                lea       ecx,[eax+edx*4]
  004bd6fa  c1e10a                shl       ecx,0Ah
  004bd6fd  894e5c                mov       [esi+5Ch],ecx
  004bd700  eb36                  jmp       short 004BD738h
  004bd702  668b4e32              mov       cx,[esi+32h]
  004bd706  66895e18              mov       [esi+18h],bx
  004bd70a  0fbf4610              movsx     eax,word [esi+10h]
  004bd70e  0fbfd1                movsx     edx,cx
  004bd711  8bf8                  mov       edi,eax
  004bd713  2bfa                  sub       edi,edx
  004bd715  8bd0                  mov       edx,eax
  004bd717  c1ff03                sar       edi,3
  004bd71a  03cf                  add       ecx,edi
  004bd71c  66894e32              mov       [esi+32h],cx
  004bd720  0fbfc9                movsx     ecx,cx
  004bd723  2bd1                  sub       edx,ecx
  004bd725  7804                  js        short 004BD72Bh
  004bd727  8bca                  mov       ecx,edx
  004bd729  eb02                  jmp       short 004BD72Dh

LAB_004bd72b:
  004bd72b  2bc8                  sub       ecx,eax

LAB_004bd72d:
  004bd72d  83f932                cmp       ecx,32h
  004bd730  7d03                  jge       short 004BD735h

LAB_004bd732:
  004bd732  885e43                mov       [esi+43h],bl

LAB_004bd735:
  004bd735  895e5c                mov       [esi+5Ch],ebx

LAB_004bd738:
  004bd738  8b06                  mov       eax,[esi]
  004bd73a  8a4e42                mov       cl,[esi+42h]
  004bd73d  a820                  test      al,20h
  004bd73f  0f8490000000          je        near 004BD7D5h
  004bd745  80f908                cmp       cl,8
  004bd748  752d                  jne       short 004BD777h
  004bd74a  a802                  test      al,2
  004bd74c  7529                  jne       short 004BD777h
  004bd74e  8b4620                mov       eax,[esi+20h]
  004bd751  8b4c2424              mov       ecx,[esp+24h]
  004bd755  8b7920                mov       edi,[ecx+20h]
  004bd758  8d9000c00800          lea       edx,[eax+8C000h]
  004bd75e  3bd7                  cmp       edx,edi
  004bd760  7e5e                  jle       short 004BD7C0h
  004bd762  8b5160                mov       edx,[ecx+60h]
  004bd765  2bd0                  sub       edx,eax
  004bd767  81c200800200          add       edx,28000h
  004bd76d  c1fa03                sar       edx,3
  004bd770  03d0                  add       edx,eax
  004bd772  895620                mov       [esi+20h],edx
  004bd775  eb49                  jmp       short 004BD7C0h

LAB_004bd777:
  004bd777  e8c4990800            call      00547140h
  004bd77c  99                    cdq
  004bd77d  b964000000            mov       ecx,64h
  004bd782  8b7e20                mov       edi,[esi+20h]
  004bd785  f7f9                  idiv      ecx
  004bd787  66035614              add       dx,[esi+14h]
  004bd78b  03d1                  add       edx,ecx
  004bd78d  81e2ff0f0000          and       edx,0FFFh
  004bd793  66895614              mov       [esi+14h],dx
  004bd797  0fbfd2                movsx     edx,dx
  004bd79a  52                    push      edx
  004bd79b  e87c820800            call      00545A1Ch
  004bd7a0  8b4e48                mov       ecx,[esi+48h]
  004bd7a3  8d0440                lea       eax,[eax+eax*2]
  004bd7a6  8d0480                lea       eax,[eax+eax*4]
  004bd7a9  8d1481                lea       edx,[ecx+eax*4]
  004bd7ac  8b4c2424              mov       ecx,[esp+24h]
  004bd7b0  2bd7                  sub       edx,edi
  004bd7b2  81c200c00800          add       edx,8C000h
  004bd7b8  c1fa03                sar       edx,3
  004bd7bb  03d7                  add       edx,edi
  004bd7bd  895620                mov       [esi+20h],edx

LAB_004bd7c0:
  004bd7c0  8b4164                mov       eax,[ecx+64h]
  004bd7c3  8b4920                mov       ecx,[ecx+20h]
  004bd7c6  33c8                  xor       ecx,eax
  004bd7c8  f7c100f0ffff          test      ecx,0FFFFF000h
  004bd7ce  7554                  jne       short 004BD824h
  004bd7d0  894648                mov       [esi+48h],eax
  004bd7d3  eb4f                  jmp       short 004BD824h

LAB_004bd7d5:
  004bd7d5  80f908                cmp       cl,8
  004bd7d8  7504                  jne       short 004BD7DEh
  004bd7da  a802                  test      al,2
  004bd7dc  7446                  je        short 004BD824h

LAB_004bd7de:
  004bd7de  e85d990800            call      00547140h
  004bd7e3  99                    cdq
  004bd7e4  b932000000            mov       ecx,32h
  004bd7e9  8b7e20                mov       edi,[esi+20h]
  004bd7ec  f7f9                  idiv      ecx
  004bd7ee  66035614              add       dx,[esi+14h]
  004bd7f2  03d1                  add       edx,ecx
  004bd7f4  81e2ff0f0000          and       edx,0FFFh
  004bd7fa  66895614              mov       [esi+14h],dx
  004bd7fe  0fbfd2                movsx     edx,dx
  004bd801  52                    push      edx
  004bd802  e815820800            call      00545A1Ch
  004bd807  8b4e60                mov       ecx,[esi+60h]
  004bd80a  8d0440                lea       eax,[eax+eax*2]
  004bd80d  8d0480                lea       eax,[eax+eax*4]
  004bd810  c1e002                shl       eax,2
  004bd813  2bc7                  sub       eax,edi
  004bd815  8d940800c00800        lea       edx,[eax+ecx+8C000h]
  004bd81c  c1fa03                sar       edx,3
  004bd81f  03d7                  add       edx,edi
  004bd821  895620                mov       [esi+20h],edx

LAB_004bd824:
  004bd824  0fbf4632              movsx     eax,word [esi+32h]
  004bd828  50                    push      eax
  004bd829  e8ee810800            call      00545A1Ch
  004bd82e  0fbf4e32              movsx     ecx,word [esi+32h]
  004bd832  51                    push      ecx
  004bd833  89442474              mov       [esp+74h],eax
  004bd837  e8da810800            call      00545A16h
  004bd83c  8b5650                mov       edx,[esi+50h]
  004bd83f  89442478              mov       [esp+78h],eax
  004bd843  8bc2                  mov       eax,edx
  004bd845  8b4e5c                mov       ecx,[esi+5Ch]
  004bd848  c1f808                sar       eax,8
  004bd84b  0faf4c241c            imul      ecx,[esp+1Ch]
  004bd850  8bfa                  mov       edi,edx
  004bd852  56                    push      esi
  004bd853  2bf8                  sub       edi,eax
  004bd855  c1fa03                sar       edx,3
  004bd858  2bfa                  sub       edi,edx
  004bd85a  8b5654                mov       edx,[esi+54h]
  004bd85d  8bc2                  mov       eax,edx
  004bd85f  8bea                  mov       ebp,edx
  004bd861  c1f803                sar       eax,3
  004bd864  2be8                  sub       ebp,eax
  004bd866  897e50                mov       [esi+50h],edi
  004bd869  c1fa08                sar       edx,8
  004bd86c  2bea                  sub       ebp,edx
  004bd86e  8b5658                mov       edx,[esi+58h]
  004bd871  8bda                  mov       ebx,edx
  004bd873  8bc2                  mov       eax,edx
  004bd875  c1fb03                sar       ebx,3
  004bd878  2bc3                  sub       eax,ebx
  004bd87a  8b5e20                mov       ebx,[esi+20h]
  004bd87d  c1fa08                sar       edx,8
  004bd880  c1f90c                sar       ecx,0Ch
  004bd883  2bc2                  sub       eax,edx
  004bd885  8bd1                  mov       edx,ecx
  004bd887  0faf542474            imul      edx,[esp+74h]
  004bd88c  0faf4c247c            imul      ecx,[esp+7Ch]
  004bd891  c1fa0c                sar       edx,0Ch
  004bd894  c1f90c                sar       ecx,0Ch
  004bd897  894658                mov       [esi+58h],eax
  004bd89a  03d7                  add       edx,edi
  004bd89c  8b7e1c                mov       edi,[esi+1Ch]
  004bd89f  03c8                  add       ecx,eax
  004bd8a1  8b4624                mov       eax,[esi+24h]
  004bd8a4  03fa                  add       edi,edx
  004bd8a6  03dd                  add       ebx,ebp
  004bd8a8  03c1                  add       eax,ecx
  004bd8aa  896e54                mov       [esi+54h],ebp
  004bd8ad  897e1c                mov       [esi+1Ch],edi
  004bd8b0  895e20                mov       [esi+20h],ebx
  004bd8b3  894624                mov       [esi+24h],eax
  004bd8b6  e885600700            call      00533940h
  004bd8bb  8bbe90000000          mov       edi,[esi+90h]
  004bd8c1  8b06                  mov       eax,[esi]
  004bd8c3  83c404                add       esp,4
  004bd8c6  81e7fff8ffff          and       edi,0FFFFF8FFh
  004bd8cc  a820                  test      al,20h
  004bd8ce  89be90000000          mov       [esi+90h],edi
  004bd8d4  8bcf                  mov       ecx,edi
  004bd8d6  740c                  je        short 004BD8E4h
  004bd8d8  81e1ffdfffdf          and       ecx,0DFFFDFFFh
  004bd8de  898e90000000          mov       [esi+90h],ecx

LAB_004bd8e4:
  004bd8e4  8b4e60                mov       ecx,[esi+60h]
  004bd8e7  8b5620                mov       edx,[esi+20h]
  004bd8ea  3bd1                  cmp       edx,ecx
  004bd8ec  7f19                  jg        short 004BD907h
  004bd8ee  8b9690000000          mov       edx,[esi+90h]
  004bd8f4  f6c680                test      dh,80h
  004bd8f7  750e                  jne       short 004BD907h
  004bd8f9  24fe                  and       al,0FEh
  004bd8fb  894e20                mov       [esi+20h],ecx
  004bd8fe  8906                  mov       [esi],eax
  004bd900  c7465400000000        mov       dword [esi+54h],0

LAB_004bd907:
  004bd907  8b8690000000          mov       eax,[esi+90h]
  004bd90d  33db                  xor       ebx,ebx
  004bd90f  a982a80020            test      eax,2000A882h
  004bd914  0f84c3000000          je        near 004BD9DDh
  004bd91a  8a4642                mov       al,[esi+42h]
  004bd91d  3c07                  cmp       al,7
  004bd91f  7425                  je        short 004BD946h
  004bd921  3c0a                  cmp       al,0Ah
  004bd923  7421                  je        short 004BD946h
  004bd925  c6464207              mov       byte [esi+42h],7
  004bd929  e812980800            call      00547140h
  004bd92e  99                    cdq
  004bd92f  b928000000            mov       ecx,28h
  004bd934  66895e12              mov       [esi+12h],bx
  004bd938  f7f9                  idiv      ecx
  004bd93a  66c746105000          mov       word [esi+10h],50h
  004bd940  80c214                add       dl,14h
  004bd943  885643                mov       [esi+43h],dl

LAB_004bd946:
  004bd946  8b16                  mov       edx,[esi]
  004bd948  8b44245c              mov       eax,[esp+5Ch]
  004bd94c  8b4c2448              mov       ecx,[esp+48h]
  004bd950  83e2fd                and       edx,0FFFFFFFDh
  004bd953  894624                mov       [esi+24h],eax
  004bd956  8b44244c              mov       eax,[esp+4Ch]
  004bd95a  8916                  mov       [esi],edx
  004bd95c  8b542454              mov       edx,[esp+54h]
  004bd960  25ffff0000            and       eax,0FFFFh
  004bd965  89561c                mov       [esi+1Ch],edx
  004bd968  8b542450              mov       edx,[esp+50h]
  004bd96c  894e7c                mov       [esi+7Ch],ecx
  004bd96f  8b4c2434              mov       ecx,[esp+34h]
  004bd973  898690000000          mov       [esi+90h],eax
  004bd979  8b442440              mov       eax,[esp+40h]
  004bd97d  899680000000          mov       [esi+80h],edx
  004bd983  8b542438              mov       edx,[esp+38h]
  004bd987  898684000000          mov       [esi+84h],eax
  004bd98d  8b44243c              mov       eax,[esp+3Ch]
  004bd991  898e88000000          mov       [esi+88h],ecx
  004bd997  8b4c2444              mov       ecx,[esp+44h]
  004bd99b  6882a80020            push      2000A882h
  004bd9a0  56                    push      esi
  004bd9a1  89968c000000          mov       [esi+8Ch],edx
  004bd9a7  894664                mov       [esi+64h],eax
  004bd9aa  894e60                mov       [esi+60h],ecx
  004bd9ad  e8ae840100            call      004D5E60h
  004bd9b2  83c408                add       esp,8
  004bd9b5  663bc3                cmp       ax,bx
  004bd9b8  7515                  jne       short 004BD9CFh
  004bd9ba  e881970800            call      00547140h
  004bd9bf  99                    cdq
  004bd9c0  33c2                  xor       eax,edx
  004bd9c2  2bc2                  sub       eax,edx
  004bd9c4  83e001                and       eax,1
  004bd9c7  33c2                  xor       eax,edx
  004bd9c9  2bc2                  sub       eax,edx
  004bd9cb  8d4400ff              lea       eax,[eax+eax-1]

LAB_004bd9cf:
  004bd9cf  8b542410              mov       edx,[esp+10h]
  004bd9d3  83c240                add       edx,40h
  004bd9d6  0fafd0                imul      edx,eax
  004bd9d9  66015632              add       [esi+32h],dx

LAB_004bd9dd:
  004bd9dd  6aff                  push      0FFFFFFFFh
  004bd9df  6aff                  push      0FFFFFFFFh
  004bd9e1  6aff                  push      0FFFFFFFFh
  004bd9e3  56                    push      esi
  004bd9e4  e8275e0000            call      004C3810h
  004bd9e9  8b44242c              mov       eax,[esp+2Ch]
  004bd9ed  83c410                add       esp,10h
  004bd9f0  3bc3                  cmp       eax,ebx
  004bd9f2  7517                  jne       short 004BDA0Bh
  004bd9f4  668b842486000000      mov       ax,[esp+86h]
  004bd9fc  66894632              mov       [esi+32h],ax
  004bda00  5f                    pop       edi
  004bda01  5e                    pop       esi
  004bda02  5d                    pop       ebp
  004bda03  5b                    pop       ebx
  004bda04  81c488000000          add       esp,88h
  004bda0a  c3                    ret

LAB_004bda0b:
  004bda0b  8a4643                mov       al,[esi+43h]
  004bda0e  84c0                  test      al,al
  004bda10  0f85f6030000          jne       near 004BDE0Ch
  004bda16  8b466c                mov       eax,[esi+6Ch]
  004bda19  8b4e1c                mov       ecx,[esi+1Ch]
  004bda1c  8b5624                mov       edx,[esi+24h]
  004bda1f  8b781c                mov       edi,[eax+1Ch]
  004bda22  8b6824                mov       ebp,[eax+24h]
  004bda25  2bf9                  sub       edi,ecx
  004bda27  2bea                  sub       ebp,edx
  004bda29  c1ff0c                sar       edi,0Ch
  004bda2c  c1fd0c                sar       ebp,0Ch
  004bda2f  8bcf                  mov       ecx,edi
  004bda31  8bd5                  mov       edx,ebp
  004bda33  0fafcf                imul      ecx,edi
  004bda36  0fafd5                imul      edx,ebp
  004bda39  03ca                  add       ecx,edx
  004bda3b  51                    push      ecx
  004bda3c  e8cf7f0800            call      00545A10h
  004bda41  55                    push      ebp
  004bda42  57                    push      edi
  004bda43  89442420              mov       [esp+20h],eax
  004bda47  e8d67f0800            call      00545A22h
  004bda4c  668b4e32              mov       cx,[esi+32h]
  004bda50  66895e12              mov       [esi+12h],bx
  004bda54  2bc1                  sub       eax,ecx
  004bda56  25ff0f0000            and       eax,0FFFh
  004bda5b  89442414              mov       [esp+14h],eax
  004bda5f  b800100000            mov       eax,1000h
  004bda64  66894628              mov       [esi+28h],ax
  004bda68  6689462a              mov       [esi+2Ah],ax
  004bda6c  6689462c              mov       [esi+2Ch],ax
  004bda70  8b442418              mov       eax,[esp+18h]
  004bda74  3dfa000000            cmp       eax,0FAh
  004bda79  0f8dc4000000          jge       near 004BDB43h
  004bda7f  e8bc960800            call      00547140h
  004bda84  99                    cdq
  004bda85  b964000000            mov       ecx,64h
  004bda8a  f7f9                  idiv      ecx
  004bda8c  83fa32                cmp       edx,32h
  004bda8f  7d50                  jge       short 004BDAE1h
  004bda91  c6464203              mov       byte [esi+42h],3
  004bda95  e8a6960800            call      00547140h
  004bda9a  99                    cdq
  004bda9b  b90a000000            mov       ecx,0Ah
  004bdaa0  8b6c2420              mov       ebp,[esp+20h]
  004bdaa4  f7f9                  idiv      ecx
  004bdaa6  8b4c2410              mov       ecx,[esp+10h]
  004bdaaa  8bc5                  mov       eax,ebp
  004bdaac  d1f8                  sar       eax,1
  004bdaae  8db980000000          lea       edi,[ecx+80h]
  004bdab4  c1e704                shl       edi,4
  004bdab7  02d0                  add       dl,al
  004bdab9  80c214                add       dl,14h
  004bdabc  885643                mov       [esi+43h],dl
  004bdabf  e87c960800            call      00547140h
  004bdac4  99                    cdq
  004bdac5  f7ff                  idiv      edi
  004bdac7  d1ff                  sar       edi,1
  004bdac9  2bd7                  sub       edx,edi
  004bdacb  8b7c2414              mov       edi,[esp+14h]
  004bdacf  03d7                  add       edx,edi
  004bdad1  66035632              add       dx,[esi+32h]
  004bdad5  81c200080000          add       edx,800h
  004bdadb  66895610              mov       [esi+10h],dx
  004bdadf  eb66                  jmp       short 004BDB47h

LAB_004bdae1:
  004bdae1  83fa46                cmp       edx,46h
  004bdae4  7d24                  jge       short 004BDB0Ah
  004bdae6  8b6c2420              mov       ebp,[esp+20h]
  004bdaea  8b442410              mov       eax,[esp+10h]
  004bdaee  8bd5                  mov       edx,ebp
  004bdaf0  c6464200              mov       byte [esi+42h],0
  004bdaf4  d1fa                  sar       edx,1
  004bdaf6  80c20a                add       dl,0Ah
  004bdaf9  8d4c0050              lea       ecx,[eax+eax+50h]
  004bdafd  885643                mov       [esi+43h],dl
  004bdb00  66894e10              mov       [esi+10h],cx
  004bdb04  66895e12              mov       [esi+12h],bx
  004bdb08  eb3d                  jmp       short 004BDB47h

LAB_004bdb0a:
  004bdb0a  83fa5a                cmp       edx,5Ah
  004bdb0d  7d15                  jge       short 004BDB24h
  004bdb0f  8b542410              mov       edx,[esp+10h]
  004bdb13  8b442418              mov       eax,[esp+18h]
  004bdb17  83c264                add       edx,64h
  004bdb1a  3bc2                  cmp       eax,edx
  004bdb1c  7e06                  jle       short 004BDB24h
  004bdb1e  c6464208              mov       byte [esi+42h],8
  004bdb22  eb1b                  jmp       short 004BDB3Fh

LAB_004bdb24:
  004bdb24  c6464201              mov       byte [esi+42h],1
  004bdb28  e813960800            call      00547140h
  004bdb2d  99                    cdq
  004bdb2e  33c2                  xor       eax,edx
  004bdb30  2bc2                  sub       eax,edx
  004bdb32  25ff0f0000            and       eax,0FFFh
  004bdb37  33c2                  xor       eax,edx
  004bdb39  2bc2                  sub       eax,edx
  004bdb3b  66894610              mov       [esi+10h],ax

LAB_004bdb3f:
  004bdb3f  c6464301              mov       byte [esi+43h],1

LAB_004bdb43:
  004bdb43  8b6c2420              mov       ebp,[esp+20h]

LAB_004bdb47:
  004bdb47  8b442418              mov       eax,[esp+18h]
  004bdb4b  3d58020000            cmp       eax,258h
  004bdb50  0f8da8010000          jge       near 004BDCFEh
  004bdb56  3dfa000000            cmp       eax,0FAh
  004bdb5b  0f8c9d010000          jl        near 004BDCFEh
  004bdb61  e8da950800            call      00547140h
  004bdb66  99                    cdq
  004bdb67  b964000000            mov       ecx,64h
  004bdb6c  f7f9                  idiv      ecx
  004bdb6e  8bfa                  mov       edi,edx
  004bdb70  83ff14                cmp       edi,14h
  004bdb73  7d55                  jge       short 004BDBCAh
  004bdb75  8b542414              mov       edx,[esp+14h]
  004bdb79  81c200040000          add       edx,400h
  004bdb7f  81e2ff0f0000          and       edx,0FFFh
  004bdb85  81fa00080000          cmp       edx,800h
  004bdb8b  7c0d                  jl        short 004BDB9Ah
  004bdb8d  c6464201              mov       byte [esi+42h],1
  004bdb91  c6464301              mov       byte [esi+43h],1
  004bdb95  e964010000            jmp       004BDCFEh

LAB_004bdb9a:
  004bdb9a  c6464200              mov       byte [esi+42h],0
  004bdb9e  e89d950800            call      00547140h
  004bdba3  99                    cdq
  004bdba4  b90a000000            mov       ecx,0Ah
  004bdba9  66895e12              mov       [esi+12h],bx
  004bdbad  f7f9                  idiv      ecx
  004bdbaf  8b4c2410              mov       ecx,[esp+10h]
  004bdbb3  8bc5                  mov       eax,ebp
  004bdbb5  c1f802                sar       eax,2
  004bdbb8  02d0                  add       dl,al
  004bdbba  885643                mov       [esi+43h],dl
  004bdbbd  8d540950              lea       edx,[ecx+ecx+50h]
  004bdbc1  66895610              mov       [esi+10h],dx
  004bdbc5  e934010000            jmp       004BDCFEh

LAB_004bdbca:
  004bdbca  83ff23                cmp       edi,23h
  004bdbcd  0f8dac000000          jge       near 004BDC7Fh
  004bdbd3  c6464202              mov       byte [esi+42h],2
  004bdbd7  e864950800            call      00547140h
  004bdbdc  99                    cdq
  004bdbdd  b90a000000            mov       ecx,0Ah
  004bdbe2  66c746105000          mov       word [esi+10h],50h
  004bdbe8  f7f9                  idiv      ecx
  004bdbea  8bc5                  mov       eax,ebp
  004bdbec  d1f8                  sar       eax,1
  004bdbee  02d0                  add       dl,al
  004bdbf0  80c214                add       dl,14h
  004bdbf3  83ff19                cmp       edi,19h
  004bdbf6  885643                mov       [esi+43h],dl
  004bdbf9  7d59                  jge       short 004BDC54h
  004bdbfb  8b4e54                mov       ecx,[esi+54h]
  004bdbfe  8b5658                mov       edx,[esi+58h]
  004bdc01  8b5e50                mov       ebx,[esi+50h]
  004bdc04  33c0                  xor       eax,eax
  004bdc06  6aff                  push      0FFFFFFFFh
  004bdc08  50                    push      eax
  004bdc09  68de000000            push      0DEh
  004bdc0e  56                    push      esi
  004bdc0f  898c2484000000        mov       [esp+84h],ecx
  004bdc16  89942488000000        mov       [esp+88h],edx
  004bdc1d  894650                mov       [esi+50h],eax
  004bdc20  c7465400c00300        mov       dword [esi+54h],3C000h
  004bdc27  894658                mov       [esi+58h],eax
  004bdc2a  66c7869a0000000200    mov       word [esi+9Ah],2
  004bdc33  e898cdfbff            call      0047A9D0h
  004bdc38  8b842484000000        mov       eax,[esp+84h]
  004bdc3f  8b8c2488000000        mov       ecx,[esp+88h]
  004bdc46  83c410                add       esp,10h
  004bdc49  895e50                mov       [esi+50h],ebx
  004bdc4c  894654                mov       [esi+54h],eax
  004bdc4f  894e58                mov       [esi+58h],ecx
  004bdc52  33db                  xor       ebx,ebx

LAB_004bdc54:
  004bdc54  83ff1a                cmp       edi,1Ah
  004bdc57  0f8ea1000000          jle       near 004BDCFEh
  004bdc5d  83ff1c                cmp       edi,1Ch
  004bdc60  0f8d98000000          jge       near 004BDCFEh
  004bdc66  8b566c                mov       edx,[esi+6Ch]
  004bdc69  6aff                  push      0FFFFFFFFh
  004bdc6b  53                    push      ebx
  004bdc6c  68dd000000            push      0DDh
  004bdc71  56                    push      esi
  004bdc72  895678                mov       [esi+78h],edx
  004bdc75  e856cdfbff            call      0047A9D0h
  004bdc7a  83c410                add       esp,10h
  004bdc7d  eb7f                  jmp       short 004BDCFEh

LAB_004bdc7f:
  004bdc7f  83ff3c                cmp       edi,3Ch
  004bdc82  7d21                  jge       short 004BDCA5h
  004bdc84  c6464201              mov       byte [esi+42h],1
  004bdc88  e8b3940800            call      00547140h
  004bdc8d  99                    cdq
  004bdc8e  33c2                  xor       eax,edx
  004bdc90  c6464301              mov       byte [esi+43h],1
  004bdc94  2bc2                  sub       eax,edx
  004bdc96  25ff0f0000            and       eax,0FFFh
  004bdc9b  33c2                  xor       eax,edx
  004bdc9d  2bc2                  sub       eax,edx
  004bdc9f  66894610              mov       [esi+10h],ax
  004bdca3  eb59                  jmp       short 004BDCFEh

LAB_004bdca5:
  004bdca5  83ff5a                cmp       edi,5Ah
  004bdca8  7d0a                  jge       short 004BDCB4h
  004bdcaa  c6464208              mov       byte [esi+42h],8
  004bdcae  c6464301              mov       byte [esi+43h],1
  004bdcb2  eb4a                  jmp       short 004BDCFEh

LAB_004bdcb4:
  004bdcb4  c6464203              mov       byte [esi+42h],3
  004bdcb8  e883940800            call      00547140h
  004bdcbd  99                    cdq
  004bdcbe  b90a000000            mov       ecx,0Ah
  004bdcc3  f7f9                  idiv      ecx
  004bdcc5  8b4c2410              mov       ecx,[esp+10h]
  004bdcc9  8bc5                  mov       eax,ebp
  004bdccb  d1f8                  sar       eax,1
  004bdccd  8db980000000          lea       edi,[ecx+80h]
  004bdcd3  c1e704                shl       edi,4
  004bdcd6  02d0                  add       dl,al
  004bdcd8  80c214                add       dl,14h
  004bdcdb  885643                mov       [esi+43h],dl
  004bdcde  e85d940800            call      00547140h
  004bdce3  99                    cdq
  004bdce4  f7ff                  idiv      edi
  004bdce6  d1ff                  sar       edi,1
  004bdce8  2bd7                  sub       edx,edi
  004bdcea  8b7c2414              mov       edi,[esp+14h]
  004bdcee  03d7                  add       edx,edi
  004bdcf0  66035632              add       dx,[esi+32h]
  004bdcf4  81c200080000          add       edx,800h
  004bdcfa  66895610              mov       [esi+10h],dx

LAB_004bdcfe:
  004bdcfe  817c241858020000      cmp       dword [esp+18h],258h
  004bdd06  0f8c00010000          jl        near 004BDE0Ch
  004bdd0c  e82f940800            call      00547140h
  004bdd11  99                    cdq
  004bdd12  b964000000            mov       ecx,64h
  004bdd17  f7f9                  idiv      ecx
  004bdd19  83fa32                cmp       edx,32h
  004bdd1c  7d70                  jge       short 004BDD8Eh
  004bdd1e  8b442410              mov       eax,[esp+10h]
  004bdd22  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004bdd26  3bd1                  cmp       edx,ecx
  004bdd28  7d2e                  jge       short 004BDD58h
  004bdd2a  c6464202              mov       byte [esi+42h],2
  004bdd2e  e80d940800            call      00547140h
  004bdd33  99                    cdq
  004bdd34  b90a000000            mov       ecx,0Ah
  004bdd39  66c746106400          mov       word [esi+10h],64h
  004bdd3f  f7f9                  idiv      ecx
  004bdd41  8bc5                  mov       eax,ebp
  004bdd43  d1f8                  sar       eax,1
  004bdd45  02d0                  add       dl,al
  004bdd47  80c214                add       dl,14h
  004bdd4a  885643                mov       [esi+43h],dl
  004bdd4d  5f                    pop       edi
  004bdd4e  5e                    pop       esi
  004bdd4f  5d                    pop       ebp
  004bdd50  5b                    pop       ebx
  004bdd51  81c488000000          add       esp,88h
  004bdd57  c3                    ret

LAB_004bdd58:
  004bdd58  c6464200              mov       byte [esi+42h],0
  004bdd5c  e8df930800            call      00547140h
  004bdd61  99                    cdq
  004bdd62  b90a000000            mov       ecx,0Ah
  004bdd67  66895e12              mov       [esi+12h],bx
  004bdd6b  f7f9                  idiv      ecx
  004bdd6d  8b4c2410              mov       ecx,[esp+10h]
  004bdd71  8bc5                  mov       eax,ebp
  004bdd73  c1f802                sar       eax,2
  004bdd76  02d0                  add       dl,al
  004bdd78  885643                mov       [esi+43h],dl
  004bdd7b  8d540950              lea       edx,[ecx+ecx+50h]
  004bdd7f  66895610              mov       [esi+10h],dx
  004bdd83  5f                    pop       edi
  004bdd84  5e                    pop       esi
  004bdd85  5d                    pop       ebp
  004bdd86  5b                    pop       ebx
  004bdd87  81c488000000          add       esp,88h
  004bdd8d  c3                    ret

LAB_004bdd8e:
  004bdd8e  83fa46                cmp       edx,46h
  004bdd91  7d55                  jge       short 004BDDE8h
  004bdd93  c6464203              mov       byte [esi+42h],3
  004bdd97  e8a4930800            call      00547140h
  004bdd9c  99                    cdq
  004bdd9d  b90a000000            mov       ecx,0Ah
  004bdda2  f7f9                  idiv      ecx
  004bdda4  8b4c2410              mov       ecx,[esp+10h]
  004bdda8  8bc5                  mov       eax,ebp
  004bddaa  d1f8                  sar       eax,1
  004bddac  8db980000000          lea       edi,[ecx+80h]
  004bddb2  c1e704                shl       edi,4
  004bddb5  02d0                  add       dl,al
  004bddb7  80c214                add       dl,14h
  004bddba  885643                mov       [esi+43h],dl
  004bddbd  e87e930800            call      00547140h
  004bddc2  99                    cdq
  004bddc3  f7ff                  idiv      edi
  004bddc5  d1ff                  sar       edi,1
  004bddc7  2bd7                  sub       edx,edi
  004bddc9  8b7c2414              mov       edi,[esp+14h]
  004bddcd  03d7                  add       edx,edi
  004bddcf  66035632              add       dx,[esi+32h]
  004bddd3  81c200080000          add       edx,800h
  004bddd9  66895610              mov       [esi+10h],dx
  004bdddd  5f                    pop       edi
  004bddde  5e                    pop       esi
  004bdddf  5d                    pop       ebp
  004bdde0  5b                    pop       ebx
  004bdde1  81c488000000          add       esp,88h
  004bdde7  c3                    ret

LAB_004bdde8:
  004bdde8  83fa5a                cmp       edx,5Ah
  004bddeb  c6464201              mov       byte [esi+42h],1
  004bddef  7c17                  jl        short 004BDE08h
  004bddf1  e84a930800            call      00547140h
  004bddf6  99                    cdq
  004bddf7  33c2                  xor       eax,edx
  004bddf9  2bc2                  sub       eax,edx
  004bddfb  25ff0f0000            and       eax,0FFFh
  004bde00  33c2                  xor       eax,edx
  004bde02  2bc2                  sub       eax,edx
  004bde04  66894610              mov       [esi+10h],ax

LAB_004bde08:
  004bde08  c6464301              mov       byte [esi+43h],1

LAB_004bde0c:
  004bde0c  5f                    pop       edi
  004bde0d  5e                    pop       esi
  004bde0e  5d                    pop       ebp
  004bde0f  5b                    pop       ebx
  004bde10  81c488000000          add       esp,88h
  004bde16  c3                    ret
  004bde17  90                    nop
  004bde18  7dd3                  jge       short 004BDDEDh
  004bde1a  4b                    dec       ebx
  004bde1b  00c8                  add       al,cl
  004bde1d  d34b00                ror       dword [ebx],cl
  004bde20  2dd44b00e3            sub       eax,0E3004BD4h
  004bde25  d44b                  aam       4Bh
  004bde27  0038                  add       [eax],bh
  004bde29  d7                    xlatb
  004bde2a  4b                    dec       ebx
  004bde2b  0038                  add       [eax],bh
  004bde2d  d7                    xlatb
  004bde2e  4b                    dec       ebx
  004bde2f  0038                  add       [eax],bh
  004bde31  d7                    xlatb
  004bde32  4b                    dec       ebx
  004bde33  00f3                  add       bl,dh
  004bde35  d54b                  aad       4Bh
  004bde37  004bd6                add       [ebx-2Ah],cl
  004bde3a  4b                    dec       ebx
  004bde3b  0002                  add       [edx],al
  004bde3d  d7                    xlatb
  004bde3e  4b                    dec       ebx
  004bde3f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
