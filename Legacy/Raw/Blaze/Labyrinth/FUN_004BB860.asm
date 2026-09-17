Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004bb860  section .text  offset fichier 0xbac60  3280 octets

  004bb860  81ec88000000          sub       esp,88h
  004bb866  53                    push      ebx
  004bb867  55                    push      ebp
  004bb868  56                    push      esi
  004bb869  8bb42498000000        mov       esi,[esp+98h]
  004bb870  33db                  xor       ebx,ebx
  004bb872  57                    push      edi
  004bb873  8a4641                mov       al,[esi+41h]
  004bb876  66895c2428            mov       [esp+28h],bx
  004bb87b  3ac3                  cmp       al,bl
  004bb87d  66c744242a1e00        mov       word [esp+2Ah],1Eh
  004bb884  66c744242c3200        mov       word [esp+2Ch],32h
  004bb88b  66c744242e6400        mov       word [esp+2Eh],64h
  004bb892  66895c2430            mov       [esp+30h],bx
  004bb897  66c74424325000        mov       word [esp+32h],50h
  004bb89e  7533                  jne       short 004BB8D3h
  004bb8a0  53                    push      ebx
  004bb8a1  6a03                  push      3
  004bb8a3  56                    push      esi
  004bb8a4  c6464101              mov       byte [esi+41h],1
  004bb8a8  e873d9ffff            call      004B9220h
  004bb8ad  8b4620                mov       eax,[esi+20h]
  004bb8b0  83c40c                add       esp,0Ch
  004bb8b3  894648                mov       [esi+48h],eax
  004bb8b6  668b4616              mov       ax,[esi+16h]
  004bb8ba  663d0a00              cmp       ax,0Ah
  004bb8be  7c0d                  jl        short 004BB8CDh
  004bb8c0  83c0f6                add       eax,0FFFFFFF6h
  004bb8c3  66894616              mov       [esi+16h],ax
  004bb8c7  8b06                  mov       eax,[esi]
  004bb8c9  24df                  and       al,0DFh
  004bb8cb  eb04                  jmp       short 004BB8D1h

LAB_004bb8cd:
  004bb8cd  8b06                  mov       eax,[esi]
  004bb8cf  0c20                  or        al,20h

LAB_004bb8d1:
  004bb8d1  8906                  mov       [esi],eax

LAB_004bb8d3:
  004bb8d3  8b4e6c                mov       ecx,[esi+6Ch]
  004bb8d6  8b561c                mov       edx,[esi+1Ch]
  004bb8d9  8b4624                mov       eax,[esi+24h]
  004bb8dc  8d7e1c                lea       edi,[esi+1Ch]
  004bb8df  894c2424              mov       [esp+24h],ecx
  004bb8e3  8b4e64                mov       ecx,[esi+64h]
  004bb8e6  89542454              mov       [esp+54h],edx
  004bb8ea  8b5660                mov       edx,[esi+60h]
  004bb8ed  8944245c              mov       [esp+5Ch],eax
  004bb8f1  668b4632              mov       ax,[esi+32h]
  004bb8f5  894c243c              mov       [esp+3Ch],ecx
  004bb8f9  8b4e7c                mov       ecx,[esi+7Ch]
  004bb8fc  89542444              mov       [esp+44h],edx
  004bb900  668b9690000000        mov       dx,[esi+90h]
  004bb907  6689842486000000      mov       [esp+86h],ax
  004bb90f  8b8680000000          mov       eax,[esi+80h]
  004bb915  894c2448              mov       [esp+48h],ecx
  004bb919  8b8e84000000          mov       ecx,[esi+84h]
  004bb91f  668954244c            mov       [esp+4Ch],dx
  004bb924  8b9688000000          mov       edx,[esi+88h]
  004bb92a  89442450              mov       [esp+50h],eax
  004bb92e  8b868c000000          mov       eax,[esi+8Ch]
  004bb934  894c2440              mov       [esp+40h],ecx
  004bb938  89542434              mov       [esp+34h],edx
  004bb93c  8d4c2460              lea       ecx,[esp+60h]
  004bb940  89442438              mov       [esp+38h],eax
  004bb944  8d542464              lea       edx,[esp+64h]
  004bb948  51                    push      ecx
  004bb949  8d44246c              lea       eax,[esp+6Ch]
  004bb94d  52                    push      edx
  004bb94e  50                    push      eax
  004bb94f  56                    push      esi
  004bb950  e82bd00700            call      00538980h
  004bb955  83c410                add       esp,10h
  004bb958  8d4c246c              lea       ecx,[esp+6Ch]
  004bb95c  8d54241c              lea       edx,[esp+1Ch]
  004bb960  8d442414              lea       eax,[esp+14h]
  004bb964  51                    push      ecx
  004bb965  52                    push      edx
  004bb966  8d4c2420              lea       ecx,[esp+20h]
  004bb96a  50                    push      eax
  004bb96b  8d54241c              lea       edx,[esp+1Ch]
  004bb96f  51                    push      ecx
  004bb970  52                    push      edx
  004bb971  56                    push      esi
  004bb972  e8a9a50700            call      00535F20h
  004bb977  8b4c2428              mov       ecx,[esp+28h]
  004bb97b  b840000000            mov       eax,40h
  004bb980  2bc1                  sub       eax,ecx
  004bb982  83c418                add       esp,18h
  004bb985  89442420              mov       [esp+20h],eax
  004bb989  8b44241c              mov       eax,[esp+1Ch]
  004bb98d  3bc3                  cmp       eax,ebx
  004bb98f  0f847f050000          je        near 004BBF14h
  004bb995  8b2e                  mov       ebp,[esi]
  004bb997  33d2                  xor       edx,edx
  004bb999  8a5604                mov       dl,[esi+4]
  004bb99c  81e5fffffffd          and       ebp,0FDFFFFFFh
  004bb9a2  0fbf5e18              movsx     ebx,word [esi+18h]
  004bb9a6  892e                  mov       [esi],ebp
  004bb9a8  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004bb9af  f7c500000001          test      ebp,1000000h
  004bb9b5  8b149a                mov       edx,[edx+ebx*4]
  004bb9b8  741d                  je        short 004BB9D7h
  004bb9ba  66ff461a              inc       word [esi+1Ah]
  004bb9be  668b5e1a              mov       bx,[esi+1Ah]
  004bb9c2  33ed                  xor       ebp,ebp
  004bb9c4  668b6a16              mov       bp,[edx+16h]
  004bb9c8  0fbfd3                movsx     edx,bx
  004bb9cb  3bd5                  cmp       edx,ebp
  004bb9cd  7c2b                  jl        short 004BB9FAh
  004bb9cf  33db                  xor       ebx,ebx
  004bb9d1  66895e1a              mov       [esi+1Ah],bx
  004bb9d5  eb25                  jmp       short 004BB9FCh

LAB_004bb9d7:
  004bb9d7  66ff461a              inc       word [esi+1Ah]
  004bb9db  33db                  xor       ebx,ebx
  004bb9dd  668b5a16              mov       bx,[edx+16h]
  004bb9e1  0fbf561a              movsx     edx,word [esi+1Ah]
  004bb9e5  3bd3                  cmp       edx,ebx
  004bb9e7  7c11                  jl        short 004BB9FAh
  004bb9e9  668b561a              mov       dx,[esi+1Ah]
  004bb9ed  4a                    dec       edx
  004bb9ee  81cd00000002          or        ebp,2000000h
  004bb9f4  6689561a              mov       [esi+1Ah],dx
  004bb9f8  892e                  mov       [esi],ebp

LAB_004bb9fa:
  004bb9fa  33db                  xor       ebx,ebx

LAB_004bb9fc:
  004bb9fc  f60602                test      byte [esi],2
  004bb9ff  7457                  je        short 004BBA58h
  004bba01  8d442428              lea       eax,[esp+28h]
  004bba05  53                    push      ebx
  004bba06  50                    push      eax
  004bba07  57                    push      edi
  004bba08  56                    push      esi
  004bba09  e822e20500            call      00519C30h
  004bba0e  8be8                  mov       ebp,eax
  004bba10  a1b8e05d00            mov       eax,[5DE0B8h]
  004bba15  83c410                add       esp,10h
  004bba18  3bc3                  cmp       eax,ebx
  004bba1a  7434                  je        short 004BBA50h
  004bba1c  8b4d00                mov       ecx,[ebp]
  004bba1f  55                    push      ebp
  004bba20  83c940                or        ecx,40h
  004bba23  56                    push      esi
  004bba24  66c785960000001000    mov       word [ebp+96h],10h
  004bba2d  894d00                mov       [ebp],ecx
  004bba30  e84be10700            call      00539B80h
  004bba35  83c408                add       esp,8
  004bba38  83c51c                add       ebp,1Ch
  004bba3b  55                    push      ebp
  004bba3c  6800180000            push      1800h
  004bba41  6800030000            push      300h
  004bba46  6a03                  push      3
  004bba48  e8d3200800            call      0053DB20h
  004bba4d  83c410                add       esp,10h

LAB_004bba50:
  004bba50  8b44241c              mov       eax,[esp+1Ch]
  004bba54  8b4c2410              mov       ecx,[esp+10h]

LAB_004bba58:
  004bba58  33d2                  xor       edx,edx
  004bba5a  8a5642                mov       dl,[esi+42h]
  004bba5d  83fa09                cmp       edx,9
  004bba60  0f87c2030000          ja        near 004BBE28h
  004bba66  ff249508c54b00        jmp       dword [edx*4+4BC508h]
  004bba6d  66395e12              cmp       [esi+12h],bx
  004bba71  751e                  jne       short 004BBA91h
  004bba73  8b0e                  mov       ecx,[esi]
  004bba75  b801000000            mov       eax,1
  004bba7a  81c900000001          or        ecx,1000000h
  004bba80  66894618              mov       [esi+18h],ax
  004bba84  66895e1a              mov       [esi+1Ah],bx
  004bba88  890e                  mov       [esi],ecx
  004bba8a  66894612              mov       [esi+12h],ax
  004bba8e  895e5c                mov       [esi+5Ch],ebx

LAB_004bba91:
  004bba91  0fbf4e10              movsx     ecx,word [esi+10h]
  004bba95  394c2418              cmp       [esp+18h],ecx
  004bba99  7d08                  jge       short 004BBAA3h
  004bba9b  885e43                mov       [esi+43h],bl
  004bba9e  e985030000            jmp       004BBE28h

LAB_004bbaa3:
  004bbaa3  8a4643                mov       al,[esi+43h]
  004bbaa6  3ac3                  cmp       al,bl
  004bbaa8  0f8477030000          je        near 004BBE25h
  004bbaae  fec8                  dec       al
  004bbab0  884643                mov       [esi+43h],al
  004bbab3  e96d030000            jmp       004BBE25h
  004bbab8  66395e12              cmp       [esi+12h],bx
  004bbabc  751b                  jne       short 004BBAD9h
  004bbabe  8b16                  mov       edx,[esi]
  004bbac0  b801000000            mov       eax,1
  004bbac5  81ca00000001          or        edx,1000000h
  004bbacb  66894618              mov       [esi+18h],ax
  004bbacf  66895e1a              mov       [esi+1Ah],bx
  004bbad3  8916                  mov       [esi],edx
  004bbad5  66894612              mov       [esi+12h],ax

LAB_004bbad9:
  004bbad9  8b442414              mov       eax,[esp+14h]
  004bbadd  8d90c8000000          lea       edx,[eax+0C8h]
  004bbae3  81e2ff0f0000          and       edx,0FFFh
  004bbae9  81fa90010000          cmp       edx,190h
  004bbaef  0f8e2d030000          jle       near 004BBE22h
  004bbaf5  25ff0f0000            and       eax,0FFFh
  004bbafa  3d00080000            cmp       eax,800h
  004bbaff  7d0c                  jge       short 004BBB0Dh
  004bbb01  8d4132                lea       eax,[ecx+32h]
  004bbb04  66014632              add       [esi+32h],ax
  004bbb08  e918030000            jmp       004BBE25h

LAB_004bbb0d:
  004bbb0d  b8ceffffff            mov       eax,0FFFFFFCEh
  004bbb12  2bc1                  sub       eax,ecx
  004bbb14  66014632              add       [esi+32h],ax
  004bbb18  e908030000            jmp       004BBE25h
  004bbb1d  66395e12              cmp       [esi+12h],bx
  004bbb21  752e                  jne       short 004BBB51h
  004bbb23  8b16                  mov       edx,[esi]
  004bbb25  b801000000            mov       eax,1
  004bbb2a  81ca00000001          or        edx,1000000h
  004bbb30  66c746180200          mov       word [esi+18h],2
  004bbb36  8916                  mov       [esi],edx
  004bbb38  8d14c9                lea       edx,[ecx+ecx*8]
  004bbb3b  c1e204                shl       edx,4
  004bbb3e  81c200a00000          add       edx,0A000h
  004bbb44  6689461a              mov       [esi+1Ah],ax
  004bbb48  66894612              mov       [esi+12h],ax
  004bbb4c  89565c                mov       [esi+5Ch],edx
  004bbb4f  eb05                  jmp       short 004BBB56h

LAB_004bbb51:
  004bbb51  b801000000            mov       eax,1

LAB_004bbb56:
  004bbb56  66394612              cmp       [esi+12h],ax
  004bbb5a  0f85c8020000          jne       near 004BBE28h
  004bbb60  8b542418              mov       edx,[esp+18h]
  004bbb64  6689461a              mov       [esi+1Ah],ax
  004bbb68  0fbf4610              movsx     eax,word [esi+10h]
  004bbb6c  3bd0                  cmp       edx,eax
  004bbb6e  66c746180200          mov       word [esi+18h],2
  004bbb74  7d06                  jge       short 004BBB7Ch
  004bbb76  885e43                mov       [esi+43h],bl
  004bbb79  895e5c                mov       [esi+5Ch],ebx

LAB_004bbb7c:
  004bbb7c  8a4643                mov       al,[esi+43h]
  004bbb7f  3ac3                  cmp       al,bl
  004bbb81  7605                  jbe       short 004BBB88h
  004bbb83  fec8                  dec       al
  004bbb85  884643                mov       [esi+43h],al

LAB_004bbb88:
  004bbb88  8b442414              mov       eax,[esp+14h]
  004bbb8c  8d9000010000          lea       edx,[eax+100h]
  004bbb92  81e2ff0f0000          and       edx,0FFFh
  004bbb98  81fa00020000          cmp       edx,200h
  004bbb9e  0f8e84020000          jle       near 004BBE28h
  004bbba4  25ff0f0000            and       eax,0FFFh
  004bbba9  3d00080000            cmp       eax,800h
  004bbbae  7d10                  jge       short 004BBBC0h
  004bbbb0  8d8c0980000000        lea       ecx,[ecx+ecx+80h]
  004bbbb7  66014e32              add       [esi+32h],cx
  004bbbbb  e968020000            jmp       004BBE28h

LAB_004bbbc0:
  004bbbc0  8d0409                lea       eax,[ecx+ecx]
  004bbbc3  b980ffffff            mov       ecx,0FFFFFF80h
  004bbbc8  2bc8                  sub       ecx,eax
  004bbbca  66014e32              add       [esi+32h],cx
  004bbbce  e955020000            jmp       004BBE28h
  004bbbd3  ba01000000            mov       edx,1
  004bbbd8  66395612              cmp       [esi+12h],dx
  004bbbdc  0f85c0000000          jne       near 004BBCA2h
  004bbbe2  66895618              mov       [esi+18h],dx
  004bbbe6  8a5643                mov       dl,[esi+43h]
  004bbbe9  3ad3                  cmp       dl,bl
  004bbbeb  7405                  je        short 004BBBF2h
  004bbbed  feca                  dec       dl
  004bbbef  885643                mov       [esi+43h],dl

LAB_004bbbf2:
  004bbbf2  83c170                add       ecx,70h
  004bbbf5  c1e108                shl       ecx,8
  004bbbf8  0fafc1                imul      eax,ecx
  004bbbfb  894e5c                mov       [esi+5Ch],ecx
  004bbbfe  0fbf4e10              movsx     ecx,word [esi+10h]
  004bbc02  c1f80c                sar       eax,0Ch
  004bbc05  51                    push      ecx
  004bbc06  8be8                  mov       ebp,eax
  004bbc08  e80f9e0800            call      00545A1Ch
  004bbc0d  0fafc5                imul      eax,ebp
  004bbc10  0fbf5610              movsx     edx,word [esi+10h]
  004bbc14  8b0f                  mov       ecx,[edi]
  004bbc16  52                    push      edx
  004bbc17  c1f80c                sar       eax,0Ch
  004bbc1a  03c8                  add       ecx,eax
  004bbc1c  890f                  mov       [edi],ecx
  004bbc1e  e8f39d0800            call      00545A16h
  004bbc23  0fafc5                imul      eax,ebp
  004bbc26  8b6e24                mov       ebp,[esi+24h]
  004bbc29  895e5c                mov       [esi+5Ch],ebx
  004bbc2c  c1f80c                sar       eax,0Ch
  004bbc2f  03e8                  add       ebp,eax
  004bbc31  8b442414              mov       eax,[esp+14h]
  004bbc35  896e24                mov       [esi+24h],ebp
  004bbc38  8d8800010000          lea       ecx,[eax+100h]
  004bbc3e  81e1ff0f0000          and       ecx,0FFFh
  004bbc44  81f900020000          cmp       ecx,200h
  004bbc4a  7e28                  jle       short 004BBC74h
  004bbc4c  25ff0f0000            and       eax,0FFFh
  004bbc51  3d00080000            cmp       eax,800h
  004bbc56  7d0a                  jge       short 004BBC62h
  004bbc58  8b542410              mov       edx,[esp+10h]
  004bbc5c  8d441232              lea       eax,[edx+edx+32h]
  004bbc60  eb0e                  jmp       short 004BBC70h

LAB_004bbc62:
  004bbc62  8b442410              mov       eax,[esp+10h]
  004bbc66  8d0c00                lea       ecx,[eax+eax]
  004bbc69  b8ceffffff            mov       eax,0FFFFFFCEh
  004bbc6e  2bc1                  sub       eax,ecx

LAB_004bbc70:
  004bbc70  66014632              add       [esi+32h],ax

LAB_004bbc74:
  004bbc74  668b4614              mov       ax,[esi+14h]
  004bbc78  668bd0                mov       dx,ax
  004bbc7b  66c1fa03              sar       dx,3
  004bbc7f  2bc2                  sub       eax,edx
  004bbc81  663d0500              cmp       ax,5
  004bbc85  66894614              mov       [esi+14h],ax
  004bbc89  7d09                  jge       short 004BBC94h
  004bbc8b  885e43                mov       [esi+43h],bl
  004bbc8e  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004bbc94:
  004bbc94  0500100000            add       eax,1000h
  004bbc99  ba01000000            mov       edx,1
  004bbc9e  6689462c              mov       [esi+2Ch],ax

LAB_004bbca2:
  004bbca2  66395e12              cmp       [esi+12h],bx
  004bbca6  0f857c010000          jne       near 004BBE28h
  004bbcac  8b0e                  mov       ecx,[esi]
  004bbcae  66895618              mov       [esi+18h],dx
  004bbcb2  81c900000001          or        ecx,1000000h
  004bbcb8  66895e1a              mov       [esi+1Ah],bx
  004bbcbc  890e                  mov       [esi],ecx
  004bbcbe  66895612              mov       [esi+12h],dx
  004bbcc2  e879b40800            call      00547140h
  004bbcc7  99                    cdq
  004bbcc8  b914000000            mov       ecx,14h
  004bbccd  f7f9                  idiv      ecx
  004bbccf  8b442410              mov       eax,[esp+10h]
  004bbcd3  8d4c020a              lea       ecx,[edx+eax+0Ah]
  004bbcd7  c1e104                shl       ecx,4
  004bbcda  66894e14              mov       [esi+14h],cx
  004bbcde  e945010000            jmp       004BBE28h
  004bbce3  668b5612              mov       dx,[esi+12h]
  004bbce7  bf01000000            mov       edi,1
  004bbcec  663bd7                cmp       dx,di
  004bbcef  751b                  jne       short 004BBD0Ch
  004bbcf1  0fbf4610              movsx     eax,word [esi+10h]
  004bbcf5  39442418              cmp       [esp+18h],eax
  004bbcf9  7d05                  jge       short 004BBD00h
  004bbcfb  885e43                mov       [esi+43h],bl
  004bbcfe  eb0c                  jmp       short 004BBD0Ch

LAB_004bbd00:
  004bbd00  8a4643                mov       al,[esi+43h]
  004bbd03  3ac3                  cmp       al,bl
  004bbd05  7405                  je        short 004BBD0Ch
  004bbd07  fec8                  dec       al
  004bbd09  884643                mov       [esi+43h],al

LAB_004bbd0c:
  004bbd0c  663bd3                cmp       dx,bx
  004bbd0f  0f8513010000          jne       near 004BBE28h
  004bbd15  66895e1a              mov       [esi+1Ah],bx
  004bbd19  8b1e                  mov       ebx,[esi]
  004bbd1b  d1f9                  sar       ecx,1
  004bbd1d  81cb00000001          or        ebx,1000000h
  004bbd23  81c100400000          add       ecx,4000h
  004bbd29  66897e18              mov       [esi+18h],di
  004bbd2d  891e                  mov       [esi],ebx
  004bbd2f  66897e12              mov       [esi+12h],di
  004bbd33  894e5c                mov       [esi+5Ch],ecx
  004bbd36  e9ed000000            jmp       004BBE28h
  004bbd3b  668b5612              mov       dx,[esi+12h]
  004bbd3f  6683fa01              cmp       dx,1
  004bbd43  7575                  jne       short 004BBDBAh
  004bbd45  8b465c                mov       eax,[esi+5Ch]
  004bbd48  66895e18              mov       [esi+18h],bx
  004bbd4c  8bf8                  mov       edi,eax
  004bbd4e  c1ff04                sar       edi,4
  004bbd51  2bc7                  sub       eax,edi
  004bbd53  3d00700000            cmp       eax,7000h
  004bbd58  89465c                mov       [esi+5Ch],eax
  004bbd5b  7d12                  jge       short 004BBD6Fh
  004bbd5d  8b06                  mov       eax,[esi]
  004bbd5f  885e43                mov       [esi+43h],bl
  004bbd62  24fd                  and       al,0FDh
  004bbd64  895e5c                mov       [esi+5Ch],ebx
  004bbd67  66c7462c0010          mov       word [esi+2Ch],1000h
  004bbd6d  8906                  mov       [esi],eax

LAB_004bbd6f:
  004bbd6f  8b442414              mov       eax,[esp+14h]
  004bbd73  8db800010000          lea       edi,[eax+100h]
  004bbd79  81e7ff0f0000          and       edi,0FFFh
  004bbd7f  81ff00020000          cmp       edi,200h
  004bbd85  7e23                  jle       short 004BBDAAh
  004bbd87  25ff0f0000            and       eax,0FFFh
  004bbd8c  3d00080000            cmp       eax,800h
  004bbd91  7d09                  jge       short 004BBD9Ch
  004bbd93  8d8c0980000000        lea       ecx,[ecx+ecx+80h]
  004bbd9a  eb0a                  jmp       short 004BBDA6h

LAB_004bbd9c:
  004bbd9c  8d0409                lea       eax,[ecx+ecx]
  004bbd9f  b980ffffff            mov       ecx,0FFFFFF80h
  004bbda4  2bc8                  sub       ecx,eax

LAB_004bbda6:
  004bbda6  66014e32              add       [esi+32h],cx

LAB_004bbdaa:
  004bbdaa  8b4e5c                mov       ecx,[esi+5Ch]
  004bbdad  c1f905                sar       ecx,5
  004bbdb0  81c100100000          add       ecx,1000h
  004bbdb6  66894e2c              mov       [esi+2Ch],cx

LAB_004bbdba:
  004bbdba  663bd3                cmp       dx,bx
  004bbdbd  7569                  jne       short 004BBE28h
  004bbdbf  8b16                  mov       edx,[esi]
  004bbdc1  66895e18              mov       [esi+18h],bx
  004bbdc5  83ca02                or        edx,2
  004bbdc8  66895e1a              mov       [esi+1Ah],bx
  004bbdcc  8916                  mov       [esi],edx
  004bbdce  e86db30800            call      00547140h
  004bbdd3  99                    cdq
  004bbdd4  b932000000            mov       ecx,32h
  004bbdd9  66c746120100          mov       word [esi+12h],1
  004bbddf  f7f9                  idiv      ecx
  004bbde1  8b442410              mov       eax,[esp+10h]
  004bbde5  24fc                  and       al,0FCh
  004bbde7  8d0c90                lea       ecx,[eax+edx*4]
  004bbdea  c1e10a                shl       ecx,0Ah
  004bbded  894e5c                mov       [esi+5Ch],ecx
  004bbdf0  eb36                  jmp       short 004BBE28h
  004bbdf2  668b4e32              mov       cx,[esi+32h]
  004bbdf6  66895e18              mov       [esi+18h],bx
  004bbdfa  0fbf4610              movsx     eax,word [esi+10h]
  004bbdfe  0fbfd1                movsx     edx,cx
  004bbe01  8bf8                  mov       edi,eax
  004bbe03  2bfa                  sub       edi,edx
  004bbe05  8bd0                  mov       edx,eax
  004bbe07  c1ff03                sar       edi,3
  004bbe0a  03cf                  add       ecx,edi
  004bbe0c  66894e32              mov       [esi+32h],cx
  004bbe10  0fbfc9                movsx     ecx,cx
  004bbe13  2bd1                  sub       edx,ecx
  004bbe15  7804                  js        short 004BBE1Bh
  004bbe17  8bca                  mov       ecx,edx
  004bbe19  eb02                  jmp       short 004BBE1Dh

LAB_004bbe1b:
  004bbe1b  2bc8                  sub       ecx,eax

LAB_004bbe1d:
  004bbe1d  83f932                cmp       ecx,32h
  004bbe20  7d03                  jge       short 004BBE25h

LAB_004bbe22:
  004bbe22  885e43                mov       [esi+43h],bl

LAB_004bbe25:
  004bbe25  895e5c                mov       [esi+5Ch],ebx

LAB_004bbe28:
  004bbe28  8b06                  mov       eax,[esi]
  004bbe2a  8a4e42                mov       cl,[esi+42h]
  004bbe2d  a820                  test      al,20h
  004bbe2f  0f8490000000          je        near 004BBEC5h
  004bbe35  80f908                cmp       cl,8
  004bbe38  752d                  jne       short 004BBE67h
  004bbe3a  a802                  test      al,2
  004bbe3c  7529                  jne       short 004BBE67h
  004bbe3e  8b4620                mov       eax,[esi+20h]
  004bbe41  8b4c2424              mov       ecx,[esp+24h]
  004bbe45  8b7920                mov       edi,[ecx+20h]
  004bbe48  8d9000c00800          lea       edx,[eax+8C000h]
  004bbe4e  3bd7                  cmp       edx,edi
  004bbe50  7e5e                  jle       short 004BBEB0h
  004bbe52  8b5160                mov       edx,[ecx+60h]
  004bbe55  2bd0                  sub       edx,eax
  004bbe57  81c200800200          add       edx,28000h
  004bbe5d  c1fa03                sar       edx,3
  004bbe60  03d0                  add       edx,eax
  004bbe62  895620                mov       [esi+20h],edx
  004bbe65  eb49                  jmp       short 004BBEB0h

LAB_004bbe67:
  004bbe67  e8d4b20800            call      00547140h
  004bbe6c  99                    cdq
  004bbe6d  b964000000            mov       ecx,64h
  004bbe72  8b7e20                mov       edi,[esi+20h]
  004bbe75  f7f9                  idiv      ecx
  004bbe77  66035614              add       dx,[esi+14h]
  004bbe7b  03d1                  add       edx,ecx
  004bbe7d  81e2ff0f0000          and       edx,0FFFh
  004bbe83  66895614              mov       [esi+14h],dx
  004bbe87  0fbfd2                movsx     edx,dx
  004bbe8a  52                    push      edx
  004bbe8b  e88c9b0800            call      00545A1Ch
  004bbe90  8b4e48                mov       ecx,[esi+48h]
  004bbe93  8d0440                lea       eax,[eax+eax*2]
  004bbe96  8d0480                lea       eax,[eax+eax*4]
  004bbe99  8d1481                lea       edx,[ecx+eax*4]
  004bbe9c  8b4c2424              mov       ecx,[esp+24h]
  004bbea0  2bd7                  sub       edx,edi
  004bbea2  81c200c00800          add       edx,8C000h
  004bbea8  c1fa03                sar       edx,3
  004bbeab  03d7                  add       edx,edi
  004bbead  895620                mov       [esi+20h],edx

LAB_004bbeb0:
  004bbeb0  8b4164                mov       eax,[ecx+64h]
  004bbeb3  8b4920                mov       ecx,[ecx+20h]
  004bbeb6  33c8                  xor       ecx,eax
  004bbeb8  f7c100f0ffff          test      ecx,0FFFFF000h
  004bbebe  7554                  jne       short 004BBF14h
  004bbec0  894648                mov       [esi+48h],eax
  004bbec3  eb4f                  jmp       short 004BBF14h

LAB_004bbec5:
  004bbec5  80f908                cmp       cl,8
  004bbec8  7504                  jne       short 004BBECEh
  004bbeca  a802                  test      al,2
  004bbecc  7446                  je        short 004BBF14h

LAB_004bbece:
  004bbece  e86db20800            call      00547140h
  004bbed3  99                    cdq
  004bbed4  b932000000            mov       ecx,32h
  004bbed9  8b7e20                mov       edi,[esi+20h]
  004bbedc  f7f9                  idiv      ecx
  004bbede  66035614              add       dx,[esi+14h]
  004bbee2  03d1                  add       edx,ecx
  004bbee4  81e2ff0f0000          and       edx,0FFFh
  004bbeea  66895614              mov       [esi+14h],dx
  004bbeee  0fbfd2                movsx     edx,dx
  004bbef1  52                    push      edx
  004bbef2  e8259b0800            call      00545A1Ch
  004bbef7  8b4e60                mov       ecx,[esi+60h]
  004bbefa  8d0440                lea       eax,[eax+eax*2]
  004bbefd  8d0480                lea       eax,[eax+eax*4]
  004bbf00  c1e002                shl       eax,2
  004bbf03  2bc7                  sub       eax,edi
  004bbf05  8d940800c00800        lea       edx,[eax+ecx+8C000h]
  004bbf0c  c1fa03                sar       edx,3
  004bbf0f  03d7                  add       edx,edi
  004bbf11  895620                mov       [esi+20h],edx

LAB_004bbf14:
  004bbf14  0fbf4632              movsx     eax,word [esi+32h]
  004bbf18  50                    push      eax
  004bbf19  e8fe9a0800            call      00545A1Ch
  004bbf1e  0fbf4e32              movsx     ecx,word [esi+32h]
  004bbf22  51                    push      ecx
  004bbf23  89442474              mov       [esp+74h],eax
  004bbf27  e8ea9a0800            call      00545A16h
  004bbf2c  8b5650                mov       edx,[esi+50h]
  004bbf2f  89442478              mov       [esp+78h],eax
  004bbf33  8bc2                  mov       eax,edx
  004bbf35  8b4e5c                mov       ecx,[esi+5Ch]
  004bbf38  c1f803                sar       eax,3
  004bbf3b  0faf4c241c            imul      ecx,[esp+1Ch]
  004bbf40  8bfa                  mov       edi,edx
  004bbf42  56                    push      esi
  004bbf43  2bf8                  sub       edi,eax
  004bbf45  c1fa08                sar       edx,8
  004bbf48  2bfa                  sub       edi,edx
  004bbf4a  8b5654                mov       edx,[esi+54h]
  004bbf4d  8bc2                  mov       eax,edx
  004bbf4f  8bea                  mov       ebp,edx
  004bbf51  c1f803                sar       eax,3
  004bbf54  2be8                  sub       ebp,eax
  004bbf56  897e50                mov       [esi+50h],edi
  004bbf59  c1fa08                sar       edx,8
  004bbf5c  2bea                  sub       ebp,edx
  004bbf5e  8b5658                mov       edx,[esi+58h]
  004bbf61  8bda                  mov       ebx,edx
  004bbf63  8bc2                  mov       eax,edx
  004bbf65  c1fb03                sar       ebx,3
  004bbf68  2bc3                  sub       eax,ebx
  004bbf6a  8b5e20                mov       ebx,[esi+20h]
  004bbf6d  c1fa08                sar       edx,8
  004bbf70  c1f90c                sar       ecx,0Ch
  004bbf73  2bc2                  sub       eax,edx
  004bbf75  8bd1                  mov       edx,ecx
  004bbf77  0faf542474            imul      edx,[esp+74h]
  004bbf7c  0faf4c247c            imul      ecx,[esp+7Ch]
  004bbf81  c1fa0c                sar       edx,0Ch
  004bbf84  c1f90c                sar       ecx,0Ch
  004bbf87  894658                mov       [esi+58h],eax
  004bbf8a  03d7                  add       edx,edi
  004bbf8c  8b7e1c                mov       edi,[esi+1Ch]
  004bbf8f  03c8                  add       ecx,eax
  004bbf91  8b4624                mov       eax,[esi+24h]
  004bbf94  03fa                  add       edi,edx
  004bbf96  03dd                  add       ebx,ebp
  004bbf98  03c1                  add       eax,ecx
  004bbf9a  896e54                mov       [esi+54h],ebp
  004bbf9d  897e1c                mov       [esi+1Ch],edi
  004bbfa0  895e20                mov       [esi+20h],ebx
  004bbfa3  894624                mov       [esi+24h],eax
  004bbfa6  e895790700            call      00533940h
  004bbfab  8bbe90000000          mov       edi,[esi+90h]
  004bbfb1  8b06                  mov       eax,[esi]
  004bbfb3  83c404                add       esp,4
  004bbfb6  81e7fff8ffff          and       edi,0FFFFF8FFh
  004bbfbc  a820                  test      al,20h
  004bbfbe  89be90000000          mov       [esi+90h],edi
  004bbfc4  8bcf                  mov       ecx,edi
  004bbfc6  740c                  je        short 004BBFD4h
  004bbfc8  81e1ffdfffdf          and       ecx,0DFFFDFFFh
  004bbfce  898e90000000          mov       [esi+90h],ecx

LAB_004bbfd4:
  004bbfd4  8b4e60                mov       ecx,[esi+60h]
  004bbfd7  8b5620                mov       edx,[esi+20h]
  004bbfda  3bd1                  cmp       edx,ecx
  004bbfdc  7f19                  jg        short 004BBFF7h
  004bbfde  8b9690000000          mov       edx,[esi+90h]
  004bbfe4  f6c680                test      dh,80h
  004bbfe7  750e                  jne       short 004BBFF7h
  004bbfe9  24fe                  and       al,0FEh
  004bbfeb  894e20                mov       [esi+20h],ecx
  004bbfee  8906                  mov       [esi],eax
  004bbff0  c7465400000000        mov       dword [esi+54h],0

LAB_004bbff7:
  004bbff7  8b8690000000          mov       eax,[esi+90h]
  004bbffd  33db                  xor       ebx,ebx
  004bbfff  a982a80020            test      eax,2000A882h
  004bc004  0f84c3000000          je        near 004BC0CDh
  004bc00a  8a4642                mov       al,[esi+42h]
  004bc00d  3c07                  cmp       al,7
  004bc00f  7425                  je        short 004BC036h
  004bc011  3c0a                  cmp       al,0Ah
  004bc013  7421                  je        short 004BC036h
  004bc015  c6464207              mov       byte [esi+42h],7
  004bc019  e822b10800            call      00547140h
  004bc01e  99                    cdq
  004bc01f  b928000000            mov       ecx,28h
  004bc024  66895e12              mov       [esi+12h],bx
  004bc028  f7f9                  idiv      ecx
  004bc02a  66c746105000          mov       word [esi+10h],50h
  004bc030  80c214                add       dl,14h
  004bc033  885643                mov       [esi+43h],dl

LAB_004bc036:
  004bc036  8b16                  mov       edx,[esi]
  004bc038  8b44245c              mov       eax,[esp+5Ch]
  004bc03c  8b4c2448              mov       ecx,[esp+48h]
  004bc040  83e2fd                and       edx,0FFFFFFFDh
  004bc043  894624                mov       [esi+24h],eax
  004bc046  8b44244c              mov       eax,[esp+4Ch]
  004bc04a  8916                  mov       [esi],edx
  004bc04c  8b542454              mov       edx,[esp+54h]
  004bc050  25ffff0000            and       eax,0FFFFh
  004bc055  89561c                mov       [esi+1Ch],edx
  004bc058  8b542450              mov       edx,[esp+50h]
  004bc05c  894e7c                mov       [esi+7Ch],ecx
  004bc05f  8b4c2434              mov       ecx,[esp+34h]
  004bc063  898690000000          mov       [esi+90h],eax
  004bc069  8b442440              mov       eax,[esp+40h]
  004bc06d  899680000000          mov       [esi+80h],edx
  004bc073  8b542438              mov       edx,[esp+38h]
  004bc077  898684000000          mov       [esi+84h],eax
  004bc07d  8b44243c              mov       eax,[esp+3Ch]
  004bc081  898e88000000          mov       [esi+88h],ecx
  004bc087  8b4c2444              mov       ecx,[esp+44h]
  004bc08b  6882a80020            push      2000A882h
  004bc090  56                    push      esi
  004bc091  89968c000000          mov       [esi+8Ch],edx
  004bc097  894664                mov       [esi+64h],eax
  004bc09a  894e60                mov       [esi+60h],ecx
  004bc09d  e8be9d0100            call      004D5E60h
  004bc0a2  83c408                add       esp,8
  004bc0a5  663bc3                cmp       ax,bx
  004bc0a8  7515                  jne       short 004BC0BFh
  004bc0aa  e891b00800            call      00547140h
  004bc0af  99                    cdq
  004bc0b0  33c2                  xor       eax,edx
  004bc0b2  2bc2                  sub       eax,edx
  004bc0b4  83e001                and       eax,1
  004bc0b7  33c2                  xor       eax,edx
  004bc0b9  2bc2                  sub       eax,edx
  004bc0bb  8d4400ff              lea       eax,[eax+eax-1]

LAB_004bc0bf:
  004bc0bf  8b542410              mov       edx,[esp+10h]
  004bc0c3  83c240                add       edx,40h
  004bc0c6  0fafd0                imul      edx,eax
  004bc0c9  66015632              add       [esi+32h],dx

LAB_004bc0cd:
  004bc0cd  6aff                  push      0FFFFFFFFh
  004bc0cf  6aff                  push      0FFFFFFFFh
  004bc0d1  6aff                  push      0FFFFFFFFh
  004bc0d3  56                    push      esi
  004bc0d4  e837770000            call      004C3810h
  004bc0d9  8b44242c              mov       eax,[esp+2Ch]
  004bc0dd  83c410                add       esp,10h
  004bc0e0  3bc3                  cmp       eax,ebx
  004bc0e2  7517                  jne       short 004BC0FBh
  004bc0e4  668b842486000000      mov       ax,[esp+86h]
  004bc0ec  66894632              mov       [esi+32h],ax
  004bc0f0  5f                    pop       edi
  004bc0f1  5e                    pop       esi
  004bc0f2  5d                    pop       ebp
  004bc0f3  5b                    pop       ebx
  004bc0f4  81c488000000          add       esp,88h
  004bc0fa  c3                    ret

LAB_004bc0fb:
  004bc0fb  8a4643                mov       al,[esi+43h]
  004bc0fe  84c0                  test      al,al
  004bc100  0f85f6030000          jne       near 004BC4FCh
  004bc106  8b466c                mov       eax,[esi+6Ch]
  004bc109  8b4e1c                mov       ecx,[esi+1Ch]
  004bc10c  8b5624                mov       edx,[esi+24h]
  004bc10f  8b781c                mov       edi,[eax+1Ch]
  004bc112  8b6824                mov       ebp,[eax+24h]
  004bc115  2bf9                  sub       edi,ecx
  004bc117  2bea                  sub       ebp,edx
  004bc119  c1ff0c                sar       edi,0Ch
  004bc11c  c1fd0c                sar       ebp,0Ch
  004bc11f  8bcf                  mov       ecx,edi
  004bc121  8bd5                  mov       edx,ebp
  004bc123  0fafcf                imul      ecx,edi
  004bc126  0fafd5                imul      edx,ebp
  004bc129  03ca                  add       ecx,edx
  004bc12b  51                    push      ecx
  004bc12c  e8df980800            call      00545A10h
  004bc131  55                    push      ebp
  004bc132  57                    push      edi
  004bc133  89442420              mov       [esp+20h],eax
  004bc137  e8e6980800            call      00545A22h
  004bc13c  668b4e32              mov       cx,[esi+32h]
  004bc140  66895e12              mov       [esi+12h],bx
  004bc144  2bc1                  sub       eax,ecx
  004bc146  25ff0f0000            and       eax,0FFFh
  004bc14b  89442414              mov       [esp+14h],eax
  004bc14f  b800100000            mov       eax,1000h
  004bc154  66894628              mov       [esi+28h],ax
  004bc158  6689462a              mov       [esi+2Ah],ax
  004bc15c  6689462c              mov       [esi+2Ch],ax
  004bc160  8b442418              mov       eax,[esp+18h]
  004bc164  3dfa000000            cmp       eax,0FAh
  004bc169  0f8dc4000000          jge       near 004BC233h
  004bc16f  e8ccaf0800            call      00547140h
  004bc174  99                    cdq
  004bc175  b964000000            mov       ecx,64h
  004bc17a  f7f9                  idiv      ecx
  004bc17c  83fa32                cmp       edx,32h
  004bc17f  7d50                  jge       short 004BC1D1h
  004bc181  c6464203              mov       byte [esi+42h],3
  004bc185  e8b6af0800            call      00547140h
  004bc18a  99                    cdq
  004bc18b  b90a000000            mov       ecx,0Ah
  004bc190  8b6c2420              mov       ebp,[esp+20h]
  004bc194  f7f9                  idiv      ecx
  004bc196  8b4c2410              mov       ecx,[esp+10h]
  004bc19a  8bc5                  mov       eax,ebp
  004bc19c  d1f8                  sar       eax,1
  004bc19e  8db980000000          lea       edi,[ecx+80h]
  004bc1a4  c1e704                shl       edi,4
  004bc1a7  02d0                  add       dl,al
  004bc1a9  80c214                add       dl,14h
  004bc1ac  885643                mov       [esi+43h],dl
  004bc1af  e88caf0800            call      00547140h
  004bc1b4  99                    cdq
  004bc1b5  f7ff                  idiv      edi
  004bc1b7  d1ff                  sar       edi,1
  004bc1b9  2bd7                  sub       edx,edi
  004bc1bb  8b7c2414              mov       edi,[esp+14h]
  004bc1bf  03d7                  add       edx,edi
  004bc1c1  66035632              add       dx,[esi+32h]
  004bc1c5  81c200080000          add       edx,800h
  004bc1cb  66895610              mov       [esi+10h],dx
  004bc1cf  eb66                  jmp       short 004BC237h

LAB_004bc1d1:
  004bc1d1  83fa46                cmp       edx,46h
  004bc1d4  7d24                  jge       short 004BC1FAh
  004bc1d6  8b6c2420              mov       ebp,[esp+20h]
  004bc1da  8b442410              mov       eax,[esp+10h]
  004bc1de  8bd5                  mov       edx,ebp
  004bc1e0  c6464200              mov       byte [esi+42h],0
  004bc1e4  d1fa                  sar       edx,1
  004bc1e6  80c20a                add       dl,0Ah
  004bc1e9  8d4c0050              lea       ecx,[eax+eax+50h]
  004bc1ed  885643                mov       [esi+43h],dl
  004bc1f0  66894e10              mov       [esi+10h],cx
  004bc1f4  66895e12              mov       [esi+12h],bx
  004bc1f8  eb3d                  jmp       short 004BC237h

LAB_004bc1fa:
  004bc1fa  83fa5a                cmp       edx,5Ah
  004bc1fd  7d15                  jge       short 004BC214h
  004bc1ff  8b542410              mov       edx,[esp+10h]
  004bc203  8b442418              mov       eax,[esp+18h]
  004bc207  83c264                add       edx,64h
  004bc20a  3bc2                  cmp       eax,edx
  004bc20c  7e06                  jle       short 004BC214h
  004bc20e  c6464208              mov       byte [esi+42h],8
  004bc212  eb1b                  jmp       short 004BC22Fh

LAB_004bc214:
  004bc214  c6464201              mov       byte [esi+42h],1
  004bc218  e823af0800            call      00547140h
  004bc21d  99                    cdq
  004bc21e  33c2                  xor       eax,edx
  004bc220  2bc2                  sub       eax,edx
  004bc222  25ff0f0000            and       eax,0FFFh
  004bc227  33c2                  xor       eax,edx
  004bc229  2bc2                  sub       eax,edx
  004bc22b  66894610              mov       [esi+10h],ax

LAB_004bc22f:
  004bc22f  c6464301              mov       byte [esi+43h],1

LAB_004bc233:
  004bc233  8b6c2420              mov       ebp,[esp+20h]

LAB_004bc237:
  004bc237  8b442418              mov       eax,[esp+18h]
  004bc23b  3d58020000            cmp       eax,258h
  004bc240  0f8da8010000          jge       near 004BC3EEh
  004bc246  3dfa000000            cmp       eax,0FAh
  004bc24b  0f8c9d010000          jl        near 004BC3EEh
  004bc251  e8eaae0800            call      00547140h
  004bc256  99                    cdq
  004bc257  b964000000            mov       ecx,64h
  004bc25c  f7f9                  idiv      ecx
  004bc25e  8bfa                  mov       edi,edx
  004bc260  83ff14                cmp       edi,14h
  004bc263  7d55                  jge       short 004BC2BAh
  004bc265  8b542414              mov       edx,[esp+14h]
  004bc269  81c200040000          add       edx,400h
  004bc26f  81e2ff0f0000          and       edx,0FFFh
  004bc275  81fa00080000          cmp       edx,800h
  004bc27b  7c0d                  jl        short 004BC28Ah
  004bc27d  c6464201              mov       byte [esi+42h],1
  004bc281  c6464301              mov       byte [esi+43h],1
  004bc285  e964010000            jmp       004BC3EEh

LAB_004bc28a:
  004bc28a  c6464200              mov       byte [esi+42h],0
  004bc28e  e8adae0800            call      00547140h
  004bc293  99                    cdq
  004bc294  b90a000000            mov       ecx,0Ah
  004bc299  66895e12              mov       [esi+12h],bx
  004bc29d  f7f9                  idiv      ecx
  004bc29f  8b4c2410              mov       ecx,[esp+10h]
  004bc2a3  8bc5                  mov       eax,ebp
  004bc2a5  c1f802                sar       eax,2
  004bc2a8  02d0                  add       dl,al
  004bc2aa  885643                mov       [esi+43h],dl
  004bc2ad  8d540950              lea       edx,[ecx+ecx+50h]
  004bc2b1  66895610              mov       [esi+10h],dx
  004bc2b5  e934010000            jmp       004BC3EEh

LAB_004bc2ba:
  004bc2ba  83ff23                cmp       edi,23h
  004bc2bd  0f8dac000000          jge       near 004BC36Fh
  004bc2c3  c6464202              mov       byte [esi+42h],2
  004bc2c7  e874ae0800            call      00547140h
  004bc2cc  99                    cdq
  004bc2cd  b90a000000            mov       ecx,0Ah
  004bc2d2  66c746105000          mov       word [esi+10h],50h
  004bc2d8  f7f9                  idiv      ecx
  004bc2da  8bc5                  mov       eax,ebp
  004bc2dc  d1f8                  sar       eax,1
  004bc2de  02d0                  add       dl,al
  004bc2e0  80c214                add       dl,14h
  004bc2e3  83ff19                cmp       edi,19h
  004bc2e6  885643                mov       [esi+43h],dl
  004bc2e9  7d59                  jge       short 004BC344h
  004bc2eb  8b4e54                mov       ecx,[esi+54h]
  004bc2ee  8b5658                mov       edx,[esi+58h]
  004bc2f1  8b5e50                mov       ebx,[esi+50h]
  004bc2f4  33c0                  xor       eax,eax
  004bc2f6  6aff                  push      0FFFFFFFFh
  004bc2f8  50                    push      eax
  004bc2f9  68de000000            push      0DEh
  004bc2fe  56                    push      esi
  004bc2ff  898c2484000000        mov       [esp+84h],ecx
  004bc306  89942488000000        mov       [esp+88h],edx
  004bc30d  894650                mov       [esi+50h],eax
  004bc310  c7465400c00300        mov       dword [esi+54h],3C000h
  004bc317  894658                mov       [esi+58h],eax
  004bc31a  66c7869a0000000200    mov       word [esi+9Ah],2
  004bc323  e8a8e6fbff            call      0047A9D0h
  004bc328  8b842484000000        mov       eax,[esp+84h]
  004bc32f  8b8c2488000000        mov       ecx,[esp+88h]
  004bc336  83c410                add       esp,10h
  004bc339  895e50                mov       [esi+50h],ebx
  004bc33c  894654                mov       [esi+54h],eax
  004bc33f  894e58                mov       [esi+58h],ecx
  004bc342  33db                  xor       ebx,ebx

LAB_004bc344:
  004bc344  83ff1a                cmp       edi,1Ah
  004bc347  0f8ea1000000          jle       near 004BC3EEh
  004bc34d  83ff1c                cmp       edi,1Ch
  004bc350  0f8d98000000          jge       near 004BC3EEh
  004bc356  8b566c                mov       edx,[esi+6Ch]
  004bc359  6aff                  push      0FFFFFFFFh
  004bc35b  53                    push      ebx
  004bc35c  68dd000000            push      0DDh
  004bc361  56                    push      esi
  004bc362  895678                mov       [esi+78h],edx
  004bc365  e866e6fbff            call      0047A9D0h
  004bc36a  83c410                add       esp,10h
  004bc36d  eb7f                  jmp       short 004BC3EEh

LAB_004bc36f:
  004bc36f  83ff3c                cmp       edi,3Ch
  004bc372  7d21                  jge       short 004BC395h
  004bc374  c6464201              mov       byte [esi+42h],1
  004bc378  e8c3ad0800            call      00547140h
  004bc37d  99                    cdq
  004bc37e  33c2                  xor       eax,edx
  004bc380  c6464301              mov       byte [esi+43h],1
  004bc384  2bc2                  sub       eax,edx
  004bc386  25ff0f0000            and       eax,0FFFh
  004bc38b  33c2                  xor       eax,edx
  004bc38d  2bc2                  sub       eax,edx
  004bc38f  66894610              mov       [esi+10h],ax
  004bc393  eb59                  jmp       short 004BC3EEh

LAB_004bc395:
  004bc395  83ff5a                cmp       edi,5Ah
  004bc398  7d0a                  jge       short 004BC3A4h
  004bc39a  c6464208              mov       byte [esi+42h],8
  004bc39e  c6464301              mov       byte [esi+43h],1
  004bc3a2  eb4a                  jmp       short 004BC3EEh

LAB_004bc3a4:
  004bc3a4  c6464203              mov       byte [esi+42h],3
  004bc3a8  e893ad0800            call      00547140h
  004bc3ad  99                    cdq
  004bc3ae  b90a000000            mov       ecx,0Ah
  004bc3b3  f7f9                  idiv      ecx
  004bc3b5  8b4c2410              mov       ecx,[esp+10h]
  004bc3b9  8bc5                  mov       eax,ebp
  004bc3bb  d1f8                  sar       eax,1
  004bc3bd  8db980000000          lea       edi,[ecx+80h]
  004bc3c3  c1e704                shl       edi,4
  004bc3c6  02d0                  add       dl,al
  004bc3c8  80c214                add       dl,14h
  004bc3cb  885643                mov       [esi+43h],dl
  004bc3ce  e86dad0800            call      00547140h
  004bc3d3  99                    cdq
  004bc3d4  f7ff                  idiv      edi
  004bc3d6  d1ff                  sar       edi,1
  004bc3d8  2bd7                  sub       edx,edi
  004bc3da  8b7c2414              mov       edi,[esp+14h]
  004bc3de  03d7                  add       edx,edi
  004bc3e0  66035632              add       dx,[esi+32h]
  004bc3e4  81c200080000          add       edx,800h
  004bc3ea  66895610              mov       [esi+10h],dx

LAB_004bc3ee:
  004bc3ee  817c241858020000      cmp       dword [esp+18h],258h
  004bc3f6  0f8c00010000          jl        near 004BC4FCh
  004bc3fc  e83fad0800            call      00547140h
  004bc401  99                    cdq
  004bc402  b964000000            mov       ecx,64h
  004bc407  f7f9                  idiv      ecx
  004bc409  83fa32                cmp       edx,32h
  004bc40c  7d70                  jge       short 004BC47Eh
  004bc40e  8b442410              mov       eax,[esp+10h]
  004bc412  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004bc416  3bd1                  cmp       edx,ecx
  004bc418  7d2e                  jge       short 004BC448h
  004bc41a  c6464202              mov       byte [esi+42h],2
  004bc41e  e81dad0800            call      00547140h
  004bc423  99                    cdq
  004bc424  b90a000000            mov       ecx,0Ah
  004bc429  66c746106400          mov       word [esi+10h],64h
  004bc42f  f7f9                  idiv      ecx
  004bc431  8bc5                  mov       eax,ebp
  004bc433  d1f8                  sar       eax,1
  004bc435  02d0                  add       dl,al
  004bc437  80c214                add       dl,14h
  004bc43a  885643                mov       [esi+43h],dl
  004bc43d  5f                    pop       edi
  004bc43e  5e                    pop       esi
  004bc43f  5d                    pop       ebp
  004bc440  5b                    pop       ebx
  004bc441  81c488000000          add       esp,88h
  004bc447  c3                    ret

LAB_004bc448:
  004bc448  c6464200              mov       byte [esi+42h],0
  004bc44c  e8efac0800            call      00547140h
  004bc451  99                    cdq
  004bc452  b90a000000            mov       ecx,0Ah
  004bc457  66895e12              mov       [esi+12h],bx
  004bc45b  f7f9                  idiv      ecx
  004bc45d  8b4c2410              mov       ecx,[esp+10h]
  004bc461  8bc5                  mov       eax,ebp
  004bc463  c1f802                sar       eax,2
  004bc466  02d0                  add       dl,al
  004bc468  885643                mov       [esi+43h],dl
  004bc46b  8d540950              lea       edx,[ecx+ecx+50h]
  004bc46f  66895610              mov       [esi+10h],dx
  004bc473  5f                    pop       edi
  004bc474  5e                    pop       esi
  004bc475  5d                    pop       ebp
  004bc476  5b                    pop       ebx
  004bc477  81c488000000          add       esp,88h
  004bc47d  c3                    ret

LAB_004bc47e:
  004bc47e  83fa46                cmp       edx,46h
  004bc481  7d55                  jge       short 004BC4D8h
  004bc483  c6464203              mov       byte [esi+42h],3
  004bc487  e8b4ac0800            call      00547140h
  004bc48c  99                    cdq
  004bc48d  b90a000000            mov       ecx,0Ah
  004bc492  f7f9                  idiv      ecx
  004bc494  8b4c2410              mov       ecx,[esp+10h]
  004bc498  8bc5                  mov       eax,ebp
  004bc49a  d1f8                  sar       eax,1
  004bc49c  8db980000000          lea       edi,[ecx+80h]
  004bc4a2  c1e704                shl       edi,4
  004bc4a5  02d0                  add       dl,al
  004bc4a7  80c214                add       dl,14h
  004bc4aa  885643                mov       [esi+43h],dl
  004bc4ad  e88eac0800            call      00547140h
  004bc4b2  99                    cdq
  004bc4b3  f7ff                  idiv      edi
  004bc4b5  d1ff                  sar       edi,1
  004bc4b7  2bd7                  sub       edx,edi
  004bc4b9  8b7c2414              mov       edi,[esp+14h]
  004bc4bd  03d7                  add       edx,edi
  004bc4bf  66035632              add       dx,[esi+32h]
  004bc4c3  81c200080000          add       edx,800h
  004bc4c9  66895610              mov       [esi+10h],dx
  004bc4cd  5f                    pop       edi
  004bc4ce  5e                    pop       esi
  004bc4cf  5d                    pop       ebp
  004bc4d0  5b                    pop       ebx
  004bc4d1  81c488000000          add       esp,88h
  004bc4d7  c3                    ret

LAB_004bc4d8:
  004bc4d8  83fa5a                cmp       edx,5Ah
  004bc4db  c6464201              mov       byte [esi+42h],1
  004bc4df  7c17                  jl        short 004BC4F8h
  004bc4e1  e85aac0800            call      00547140h
  004bc4e6  99                    cdq
  004bc4e7  33c2                  xor       eax,edx
  004bc4e9  2bc2                  sub       eax,edx
  004bc4eb  25ff0f0000            and       eax,0FFFh
  004bc4f0  33c2                  xor       eax,edx
  004bc4f2  2bc2                  sub       eax,edx
  004bc4f4  66894610              mov       [esi+10h],ax

LAB_004bc4f8:
  004bc4f8  c6464301              mov       byte [esi+43h],1

LAB_004bc4fc:
  004bc4fc  5f                    pop       edi
  004bc4fd  5e                    pop       esi
  004bc4fe  5d                    pop       ebp
  004bc4ff  5b                    pop       ebx
  004bc500  81c488000000          add       esp,88h
  004bc506  c3                    ret
  004bc507  90                    nop
  004bc508  6d                    insd
  004bc509  ba4b00b8ba            mov       edx,0BAB8004Bh
  004bc50e  4b                    dec       ebx
  004bc50f  001dbb4b00d3          add       [0D3004BBBh],bl
  004bc515  bb4b0028be            mov       ebx,0BE28004Bh
  004bc51a  4b                    dec       ebx
  004bc51b  0028                  add       [eax],ch
  004bc51d  be4b0028be            mov       esi,0BE28004Bh
  004bc522  4b                    dec       ebx
  004bc523  00e3                  add       bl,ah
  004bc525  bc4b003bbd            mov       esp,0BD3B004Bh
  004bc52a  4b                    dec       ebx
  004bc52b  00f2                  add       dl,dh
  004bc52d  bd4b00                (bad)
  (octet invalide - la plage demandee deborde probablement du code)
