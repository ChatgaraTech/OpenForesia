Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004acc20  section .text  offset fichier 0xac020  3776 octets

  004acc20  81ec88000000          sub       esp,88h
  004acc26  53                    push      ebx
  004acc27  55                    push      ebp
  004acc28  56                    push      esi
  004acc29  8bb42498000000        mov       esi,[esp+98h]
  004acc30  33ed                  xor       ebp,ebp
  004acc32  57                    push      edi
  004acc33  8a4641                mov       al,[esi+41h]
  004acc36  66896c2428            mov       [esp+28h],bp
  004acc3b  84c0                  test      al,al
  004acc3d  66c744242a1400        mov       word [esp+2Ah],14h
  004acc44  66c744242c5000        mov       word [esp+2Ch],50h
  004acc4b  66c744242e6400        mov       word [esp+2Eh],64h
  004acc52  66896c2430            mov       [esp+30h],bp
  004acc57  66c74424327800        mov       word [esp+32h],78h
  004acc5e  7533                  jne       short 004ACC93h
  004acc60  55                    push      ebp
  004acc61  6a05                  push      5
  004acc63  56                    push      esi
  004acc64  c6464101              mov       byte [esi+41h],1
  004acc68  e8f3fbffff            call      004AC860h
  004acc6d  8b4620                mov       eax,[esi+20h]
  004acc70  83c40c                add       esp,0Ch
  004acc73  894648                mov       [esi+48h],eax
  004acc76  668b4616              mov       ax,[esi+16h]
  004acc7a  663d0a00              cmp       ax,0Ah
  004acc7e  7c0d                  jl        short 004ACC8Dh
  004acc80  83c0f6                add       eax,0FFFFFFF6h
  004acc83  66894616              mov       [esi+16h],ax
  004acc87  8b06                  mov       eax,[esi]
  004acc89  24df                  and       al,0DFh
  004acc8b  eb04                  jmp       short 004ACC91h

LAB_004acc8d:
  004acc8d  8b06                  mov       eax,[esi]
  004acc8f  0c20                  or        al,20h

LAB_004acc91:
  004acc91  8906                  mov       [esi],eax

LAB_004acc93:
  004acc93  8b3e                  mov       edi,[esi]
  004acc95  8b4e1c                mov       ecx,[esi+1Ch]
  004acc98  8b5620                mov       edx,[esi+20h]
  004acc9b  83e7fe                and       edi,0FFFFFFFEh
  004acc9e  8b4624                mov       eax,[esi+24h]
  004acca1  893e                  mov       [esi],edi
  004acca3  8d7e1c                lea       edi,[esi+1Ch]
  004acca6  894c2444              mov       [esp+44h],ecx
  004accaa  8b4e64                mov       ecx,[esi+64h]
  004accad  89542448              mov       [esp+48h],edx
  004accb1  8b5660                mov       edx,[esi+60h]
  004accb4  8944244c              mov       [esp+4Ch],eax
  004accb8  668b4632              mov       ax,[esi+32h]
  004accbc  894c2440              mov       [esp+40h],ecx
  004accc0  8b4e7c                mov       ecx,[esi+7Ch]
  004accc3  89542438              mov       [esp+38h],edx
  004accc7  668b9690000000        mov       dx,[esi+90h]
  004accce  6689842486000000      mov       [esp+86h],ax
  004accd6  8b8680000000          mov       eax,[esi+80h]
  004accdc  8b5e6c                mov       ebx,[esi+6Ch]
  004accdf  894c2460              mov       [esp+60h],ecx
  004acce3  8b8e84000000          mov       ecx,[esi+84h]
  004acce9  668954243c            mov       [esp+3Ch],dx
  004accee  8b9688000000          mov       edx,[esi+88h]
  004accf4  89442450              mov       [esp+50h],eax
  004accf8  8b868c000000          mov       eax,[esi+8Ch]
  004accfe  894c2434              mov       [esp+34h],ecx
  004acd02  8954245c              mov       [esp+5Ch],edx
  004acd06  8d4c2464              lea       ecx,[esp+64h]
  004acd0a  89442454              mov       [esp+54h],eax
  004acd0e  8d54246c              lea       edx,[esp+6Ch]
  004acd12  51                    push      ecx
  004acd13  8d44246c              lea       eax,[esp+6Ch]
  004acd17  52                    push      edx
  004acd18  50                    push      eax
  004acd19  56                    push      esi
  004acd1a  895c2434              mov       [esp+34h],ebx
  004acd1e  e85dbc0800            call      00538980h
  004acd23  83c410                add       esp,10h
  004acd26  8d4c2458              lea       ecx,[esp+58h]
  004acd2a  8d542420              lea       edx,[esp+20h]
  004acd2e  8d442418              lea       eax,[esp+18h]
  004acd32  51                    push      ecx
  004acd33  52                    push      edx
  004acd34  8d4c241c              lea       ecx,[esp+1Ch]
  004acd38  50                    push      eax
  004acd39  8d54241c              lea       edx,[esp+1Ch]
  004acd3d  51                    push      ecx
  004acd3e  52                    push      edx
  004acd3f  56                    push      esi
  004acd40  e8db910800            call      00535F20h
  004acd45  8b4c2428              mov       ecx,[esp+28h]
  004acd49  b840000000            mov       eax,40h
  004acd4e  2bc1                  sub       eax,ecx
  004acd50  83c418                add       esp,18h
  004acd53  8944241c              mov       [esp+1Ch],eax
  004acd57  8b442458              mov       eax,[esp+58h]
  004acd5b  50                    push      eax
  004acd5c  56                    push      esi
  004acd5d  e87e900200            call      004D5DE0h
  004acd62  8b07                  mov       eax,[edi]
  004acd64  8b531c                mov       edx,[ebx+1Ch]
  004acd67  8b4e24                mov       ecx,[esi+24h]
  004acd6a  2bc2                  sub       eax,edx
  004acd6c  8b5324                mov       edx,[ebx+24h]
  004acd6f  83c408                add       esp,8
  004acd72  c1f80c                sar       eax,0Ch
  004acd75  2bca                  sub       ecx,edx
  004acd77  8bd0                  mov       edx,eax
  004acd79  c1f90c                sar       ecx,0Ch
  004acd7c  0fafd0                imul      edx,eax
  004acd7f  8bc1                  mov       eax,ecx
  004acd81  0fafc1                imul      eax,ecx
  004acd84  03d0                  add       edx,eax
  004acd86  52                    push      edx
  004acd87  e8848c0900            call      00545A10h
  004acd8c  8b4c2420              mov       ecx,[esp+20h]
  004acd90  89442414              mov       [esp+14h],eax
  004acd94  3bcd                  cmp       ecx,ebp
  004acd96  0f847c070000          je        near 004AD518h
  004acd9c  8b16                  mov       edx,[esi]
  004acd9e  33c0                  xor       eax,eax
  004acda0  8a4604                mov       al,[esi+4]
  004acda3  81e2fffffffd          and       edx,0FDFFFFFFh
  004acda9  0fbf5e18              movsx     ebx,word [esi+18h]
  004acdad  8916                  mov       [esi],edx
  004acdaf  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004acdb6  f7c200000001          test      edx,1000000h
  004acdbc  8b2c98                mov       ebp,[eax+ebx*4]
  004acdbf  741d                  je        short 004ACDDEh
  004acdc1  66ff461a              inc       word [esi+1Ah]
  004acdc5  668b461a              mov       ax,[esi+1Ah]
  004acdc9  33d2                  xor       edx,edx
  004acdcb  668b5516              mov       dx,[ebp+16h]
  004acdcf  0fbfc0                movsx     eax,ax
  004acdd2  3bc2                  cmp       eax,edx
  004acdd4  7c2a                  jl        short 004ACE00h
  004acdd6  66c7461a0000          mov       word [esi+1Ah],0
  004acddc  eb22                  jmp       short 004ACE00h

LAB_004acdde:
  004acdde  66ff461a              inc       word [esi+1Ah]
  004acde2  668b461a              mov       ax,[esi+1Ah]
  004acde6  33db                  xor       ebx,ebx
  004acde8  668b5d16              mov       bx,[ebp+16h]
  004acdec  0fbfe8                movsx     ebp,ax
  004acdef  3beb                  cmp       ebp,ebx
  004acdf1  7c0d                  jl        short 004ACE00h
  004acdf3  48                    dec       eax
  004acdf4  81ca00000002          or        edx,2000000h
  004acdfa  6689461a              mov       [esi+1Ah],ax
  004acdfe  8916                  mov       [esi],edx

LAB_004ace00:
  004ace00  f60602                test      byte [esi],2
  004ace03  747d                  je        short 004ACE82h
  004ace05  bb08000000            mov       ebx,8
  004ace0a  66395e1a              cmp       [esi+1Ah],bx
  004ace0e  7559                  jne       short 004ACE69h
  004ace10  8d4c2428              lea       ecx,[esp+28h]
  004ace14  6a00                  push      0
  004ace16  51                    push      ecx
  004ace17  57                    push      edi
  004ace18  56                    push      esi
  004ace19  e812ce0600            call      00519C30h
  004ace1e  8be8                  mov       ebp,eax
  004ace20  a1b8e05d00            mov       eax,[5DE0B8h]
  004ace25  83c410                add       esp,10h
  004ace28  85c0                  test      eax,eax
  004ace2a  7439                  je        short 004ACE65h
  004ace2c  66395e1a              cmp       [esi+1Ah],bx
  004ace30  7533                  jne       short 004ACE65h
  004ace32  8b4500                mov       eax,[ebp]
  004ace35  55                    push      ebp
  004ace36  0c40                  or        al,40h
  004ace38  56                    push      esi
  004ace39  66c785960000001400    mov       word [ebp+96h],14h
  004ace42  894500                mov       [ebp],eax
  004ace45  e836cd0800            call      00539B80h
  004ace4a  83c408                add       esp,8
  004ace4d  83c51c                add       ebp,1Ch
  004ace50  55                    push      ebp
  004ace51  6800180000            push      1800h
  004ace56  6800030000            push      300h
  004ace5b  6a03                  push      3
  004ace5d  e8be0c0900            call      0053DB20h
  004ace62  83c410                add       esp,10h

LAB_004ace65:
  004ace65  8b4c2420              mov       ecx,[esp+20h]

LAB_004ace69:
  004ace69  8b16                  mov       edx,[esi]
  004ace6b  f7c200000002          test      edx,2000000h
  004ace71  740f                  je        short 004ACE82h
  004ace73  807e420c              cmp       byte [esi+42h],0Ch
  004ace77  7409                  je        short 004ACE82h
  004ace79  83e2fd                and       edx,0FFFFFFFDh
  004ace7c  c6464300              mov       byte [esi+43h],0
  004ace80  8916                  mov       [esi],edx

LAB_004ace82:
  004ace82  33d2                  xor       edx,edx
  004ace84  8a5642                mov       dl,[esi+42h]
  004ace87  83fa0c                cmp       edx,0Ch
  004ace8a  0f87c2050000          ja        near 004AD452h
  004ace90  ff2495acda4a00        jmp       dword [edx*4+4ADAACh]
  004ace97  33c9                  xor       ecx,ecx
  004ace99  66394e12              cmp       [esi+12h],cx
  004ace9d  751e                  jne       short 004ACEBDh
  004ace9f  8b16                  mov       edx,[esi]
  004acea1  b801000000            mov       eax,1
  004acea6  81ca00000001          or        edx,1000000h
  004aceac  66894618              mov       [esi+18h],ax
  004aceb0  66894e1a              mov       [esi+1Ah],cx
  004aceb4  8916                  mov       [esi],edx
  004aceb6  66894612              mov       [esi+12h],ax
  004aceba  894e5c                mov       [esi+5Ch],ecx

LAB_004acebd:
  004acebd  0fbf5610              movsx     edx,word [esi+10h]
  004acec1  39542414              cmp       [esp+14h],edx
  004acec5  7d09                  jge       short 004ACED0h
  004acec7  c6464300              mov       byte [esi+43h],0
  004acecb  e982050000            jmp       004AD452h

LAB_004aced0:
  004aced0  8a4643                mov       al,[esi+43h]
  004aced3  84c0                  test      al,al
  004aced5  0f848d000000          je        near 004ACF68h
  004acedb  fec8                  dec       al
  004acedd  c7465c00000000        mov       dword [esi+5Ch],0
  004acee4  884643                mov       [esi+43h],al
  004acee7  e966050000            jmp       004AD452h
  004aceec  33c0                  xor       eax,eax
  004aceee  66394612              cmp       [esi+12h],ax
  004acef2  7517                  jne       short 004ACF0Bh
  004acef4  66894618              mov       [esi+18h],ax
  004acef8  6689461a              mov       [esi+1Ah],ax
  004acefc  8b06                  mov       eax,[esi]
  004acefe  66c746120100          mov       word [esi+12h],1
  004acf04  0d00000001            or        eax,1000000h
  004acf09  8906                  mov       [esi],eax

LAB_004acf0b:
  004acf0b  8b442418              mov       eax,[esp+18h]
  004acf0f  8d88c8000000          lea       ecx,[eax+0C8h]
  004acf15  81e1ff0f0000          and       ecx,0FFFh
  004acf1b  81f990010000          cmp       ecx,190h
  004acf21  7f10                  jg        short 004ACF33h
  004acf23  c6464300              mov       byte [esi+43h],0
  004acf27  c7465c00000000        mov       dword [esi+5Ch],0
  004acf2e  e91f050000            jmp       004AD452h

LAB_004acf33:
  004acf33  25ff0f0000            and       eax,0FFFh
  004acf38  3d00080000            cmp       eax,800h
  004acf3d  7d1a                  jge       short 004ACF59h
  004acf3f  8b542410              mov       edx,[esp+10h]
  004acf43  c7465c00000000        mov       dword [esi+5Ch],0
  004acf4a  8d8280000000          lea       eax,[edx+80h]
  004acf50  66014632              add       [esi+32h],ax
  004acf54  e9f9040000            jmp       004AD452h

LAB_004acf59:
  004acf59  8b4c2410              mov       ecx,[esp+10h]
  004acf5d  b880ffffff            mov       eax,0FFFFFF80h
  004acf62  2bc1                  sub       eax,ecx
  004acf64  66014632              add       [esi+32h],ax

LAB_004acf68:
  004acf68  c7465c00000000        mov       dword [esi+5Ch],0
  004acf6f  e9de040000            jmp       004AD452h
  004acf74  8b4c2410              mov       ecx,[esp+10h]
  004acf78  33c0                  xor       eax,eax
  004acf7a  66394612              cmp       [esi+12h],ax
  004acf7e  752f                  jne       short 004ACFAFh
  004acf80  8b2e                  mov       ebp,[esi]
  004acf82  66894618              mov       [esi+18h],ax
  004acf86  6689461a              mov       [esi+1Ah],ax
  004acf8a  8d14c9                lea       edx,[ecx+ecx*8]
  004acf8d  0fbf4616              movsx     eax,word [esi+16h]
  004acf91  81cd00000001          or        ebp,1000000h
  004acf97  66c746120100          mov       word [esi+12h],1
  004acf9d  c1e00d                shl       eax,0Dh
  004acfa0  c1e204                shl       edx,4
  004acfa3  892e                  mov       [esi],ebp
  004acfa5  8d841000700000        lea       eax,[eax+edx+7000h]
  004acfac  89465c                mov       [esi+5Ch],eax

LAB_004acfaf:
  004acfaf  66837e1201            cmp       word [esi+12h],1
  004acfb4  0f8598040000          jne       near 004AD452h
  004acfba  0fbf5610              movsx     edx,word [esi+10h]
  004acfbe  39542414              cmp       [esp+14h],edx
  004acfc2  7d09                  jge       short 004ACFCDh
  004acfc4  c6464300              mov       byte [esi+43h],0
  004acfc8  e985040000            jmp       004AD452h

LAB_004acfcd:
  004acfcd  8a4643                mov       al,[esi+43h]
  004acfd0  84c0                  test      al,al
  004acfd2  7405                  je        short 004ACFD9h
  004acfd4  fec8                  dec       al
  004acfd6  884643                mov       [esi+43h],al

LAB_004acfd9:
  004acfd9  8b442418              mov       eax,[esp+18h]
  004acfdd  8d9000010000          lea       edx,[eax+100h]
  004acfe3  81e2ff0f0000          and       edx,0FFFh
  004acfe9  81fa00020000          cmp       edx,200h
  004acfef  0f8e5d040000          jle       near 004AD452h
  004acff5  25ff0f0000            and       eax,0FFFh
  004acffa  3d00080000            cmp       eax,800h
  004acfff  7d0d                  jge       short 004AD00Eh
  004ad001  8d440932              lea       eax,[ecx+ecx+32h]
  004ad005  66014632              add       [esi+32h],ax
  004ad009  e944040000            jmp       004AD452h

LAB_004ad00e:
  004ad00e  03c9                  add       ecx,ecx
  004ad010  b8ceffffff            mov       eax,0FFFFFFCEh
  004ad015  2bc1                  sub       eax,ecx
  004ad017  66014632              add       [esi+32h],ax
  004ad01b  e932040000            jmp       004AD452h
  004ad020  66837e1201            cmp       word [esi+12h],1
  004ad025  0f85a4000000          jne       near 004AD0CFh
  004ad02b  8a4643                mov       al,[esi+43h]
  004ad02e  84c0                  test      al,al
  004ad030  7405                  je        short 004AD037h
  004ad032  fec8                  dec       al
  004ad034  884643                mov       [esi+43h],al

LAB_004ad037:
  004ad037  8b442410              mov       eax,[esp+10h]
  004ad03b  0fbf5616              movsx     edx,word [esi+16h]
  004ad03f  8d04c0                lea       eax,[eax+eax*8]
  004ad042  c1e20d                shl       edx,0Dh
  004ad045  c1e004                shl       eax,4
  004ad048  8d840200500000        lea       eax,[edx+eax+5000h]
  004ad04f  0fafc8                imul      ecx,eax
  004ad052  c1f90c                sar       ecx,0Ch
  004ad055  8be9                  mov       ebp,ecx
  004ad057  89465c                mov       [esi+5Ch],eax
  004ad05a  0fbf4e10              movsx     ecx,word [esi+10h]
  004ad05e  51                    push      ecx
  004ad05f  e8b8890900            call      00545A1Ch
  004ad064  0fafc5                imul      eax,ebp
  004ad067  0fbf5610              movsx     edx,word [esi+10h]
  004ad06b  8b0f                  mov       ecx,[edi]
  004ad06d  52                    push      edx
  004ad06e  c1f80c                sar       eax,0Ch
  004ad071  03c8                  add       ecx,eax
  004ad073  890f                  mov       [edi],ecx
  004ad075  e89c890900            call      00545A16h
  004ad07a  0fafc5                imul      eax,ebp
  004ad07d  8b5e24                mov       ebx,[esi+24h]
  004ad080  c7465c00000000        mov       dword [esi+5Ch],0
  004ad087  c1f80c                sar       eax,0Ch
  004ad08a  03d8                  add       ebx,eax
  004ad08c  8b442418              mov       eax,[esp+18h]
  004ad090  895e24                mov       [esi+24h],ebx
  004ad093  8d8800010000          lea       ecx,[eax+100h]
  004ad099  81e1ff0f0000          and       ecx,0FFFh
  004ad09f  81f900020000          cmp       ecx,200h
  004ad0a5  7e28                  jle       short 004AD0CFh
  004ad0a7  25ff0f0000            and       eax,0FFFh
  004ad0ac  3d00080000            cmp       eax,800h
  004ad0b1  7d0a                  jge       short 004AD0BDh
  004ad0b3  8b542410              mov       edx,[esp+10h]
  004ad0b7  8d441232              lea       eax,[edx+edx+32h]
  004ad0bb  eb0e                  jmp       short 004AD0CBh

LAB_004ad0bd:
  004ad0bd  8b442410              mov       eax,[esp+10h]
  004ad0c1  8d0c00                lea       ecx,[eax+eax]
  004ad0c4  b8ceffffff            mov       eax,0FFFFFFCEh
  004ad0c9  2bc1                  sub       eax,ecx

LAB_004ad0cb:
  004ad0cb  66014632              add       [esi+32h],ax

LAB_004ad0cf:
  004ad0cf  66837e1200            cmp       word [esi+12h],0
  004ad0d4  0f8578030000          jne       near 004AD452h
  004ad0da  33c0                  xor       eax,eax
  004ad0dc  66c746120100          mov       word [esi+12h],1
  004ad0e2  66894618              mov       [esi+18h],ax
  004ad0e6  6689461a              mov       [esi+1Ah],ax
  004ad0ea  810e00000001          or        dword [esi],1000000h
  004ad0f0  e95d030000            jmp       004AD452h
  004ad0f5  668b4e12              mov       cx,[esi+12h]
  004ad0f9  6683f901              cmp       cx,1
  004ad0fd  751c                  jne       short 004AD11Bh
  004ad0ff  0fbf5610              movsx     edx,word [esi+10h]
  004ad103  39542414              cmp       [esp+14h],edx
  004ad107  7d06                  jge       short 004AD10Fh
  004ad109  c6464300              mov       byte [esi+43h],0
  004ad10d  eb0c                  jmp       short 004AD11Bh

LAB_004ad10f:
  004ad10f  8a4643                mov       al,[esi+43h]
  004ad112  84c0                  test      al,al
  004ad114  7405                  je        short 004AD11Bh
  004ad116  fec8                  dec       al
  004ad118  884643                mov       [esi+43h],al

LAB_004ad11b:
  004ad11b  6685c9                test      cx,cx
  004ad11e  0f852e030000          jne       near 004AD452h
  004ad124  8b3e                  mov       edi,[esi]
  004ad126  33c0                  xor       eax,eax
  004ad128  66894618              mov       [esi+18h],ax
  004ad12c  6689461a              mov       [esi+1Ah],ax
  004ad130  8b442410              mov       eax,[esp+10h]
  004ad134  81cf00000001          or        edi,1000000h
  004ad13a  893e                  mov       [esi],edi
  004ad13c  66c746120100          mov       word [esi+12h],1
  004ad142  8d04c0                lea       eax,[eax+eax*8]
  004ad145  c1e004                shl       eax,4
  004ad148  0500500000            add       eax,5000h
  004ad14d  89465c                mov       [esi+5Ch],eax
  004ad150  e9fd020000            jmp       004AD452h
  004ad155  33db                  xor       ebx,ebx
  004ad157  b801000000            mov       eax,1
  004ad15c  66395e12              cmp       [esi+12h],bx
  004ad160  7513                  jne       short 004AD175h
  004ad162  8b0e                  mov       ecx,[esi]
  004ad164  66894618              mov       [esi+18h],ax
  004ad168  83e1fd                and       ecx,0FFFFFFFDh
  004ad16b  66895e1a              mov       [esi+1Ah],bx
  004ad16f  66894612              mov       [esi+12h],ax
  004ad173  890e                  mov       [esi],ecx

LAB_004ad175:
  004ad175  66394612              cmp       [esi+12h],ax
  004ad179  7564                  jne       short 004AD1DFh
  004ad17b  66394616              cmp       [esi+16h],ax
  004ad17f  7509                  jne       short 004AD18Ah
  004ad181  c6464300              mov       byte [esi+43h],0
  004ad185  e9c8020000            jmp       004AD452h

LAB_004ad18a:
  004ad18a  8b2e                  mov       ebp,[esi]
  004ad18c  8b5660                mov       edx,[esi+60h]
  004ad18f  81cd00000001          or        ebp,1000000h
  004ad195  8bca                  mov       ecx,edx
  004ad197  892e                  mov       [esi],ebp
  004ad199  8b6e20                mov       ebp,[esi+20h]
  004ad19c  2bcd                  sub       ecx,ebp
  004ad19e  895e5c                mov       [esi+5Ch],ebx
  004ad1a1  c1f905                sar       ecx,5
  004ad1a4  66894618              mov       [esi+18h],ax
  004ad1a8  8bf9                  mov       edi,ecx
  004ad1aa  7902                  jns       short 004AD1AEh
  004ad1ac  f7df                  neg       edi

LAB_004ad1ae:
  004ad1ae  81e700f0ffff          and       edi,0FFFFF000h
  004ad1b4  81ff00300000          cmp       edi,3000h
  004ad1ba  7d02                  jge       short 004AD1BEh
  004ad1bc  d1e1                  shl       ecx,1

LAB_004ad1be:
  004ad1be  03cd                  add       ecx,ebp
  004ad1c0  2bd1                  sub       edx,ecx
  004ad1c2  894e20                mov       [esi+20h],ecx
  004ad1c5  c1fa0c                sar       edx,0Ch
  004ad1c8  7902                  jns       short 004AD1CCh
  004ad1ca  f7da                  neg       edx

LAB_004ad1cc:
  004ad1cc  83fa0a                cmp       edx,0Ah
  004ad1cf  7d0e                  jge       short 004AD1DFh
  004ad1d1  66c746120200          mov       word [esi+12h],2
  004ad1d7  66895e18              mov       [esi+18h],bx
  004ad1db  66895e1a              mov       [esi+1Ah],bx

LAB_004ad1df:
  004ad1df  66837e1202            cmp       word [esi+12h],2
  004ad1e4  0f8568020000          jne       near 004AD452h
  004ad1ea  8b0e                  mov       ecx,[esi]
  004ad1ec  8a4643                mov       al,[esi+43h]
  004ad1ef  81c900000002          or        ecx,2000000h
  004ad1f5  fec8                  dec       al
  004ad1f7  66c746180000          mov       word [esi+18h],0
  004ad1fd  890e                  mov       [esi],ecx
  004ad1ff  884643                mov       [esi+43h],al
  004ad202  7504                  jne       short 004AD208h
  004ad204  c6464300              mov       byte [esi+43h],0

LAB_004ad208:
  004ad208  837c241450            cmp       dword [esp+14h],50h
  004ad20d  0f8d3f020000          jge       near 004AD452h
  004ad213  66837e1600            cmp       word [esi+16h],0
  004ad218  7509                  jne       short 004AD223h
  004ad21a  c6464300              mov       byte [esi+43h],0
  004ad21e  e92f020000            jmp       004AD452h

LAB_004ad223:
  004ad223  e8189f0900            call      00547140h
  004ad228  99                    cdq
  004ad229  b950000000            mov       ecx,50h
  004ad22e  f7f9                  idiv      ecx
  004ad230  3b542410              cmp       edx,[esp+10h]
  004ad234  7d2e                  jge       short 004AD264h
  004ad236  8b16                  mov       edx,[esi]
  004ad238  c646420c              mov       byte [esi+42h],0Ch
  004ad23c  81e2fffffffe          and       edx,0FEFFFFFFh
  004ad242  66c746120200          mov       word [esi+12h],2
  004ad248  83ca02                or        edx,2
  004ad24b  66c746100000          mov       word [esi+10h],0
  004ad251  66c746180300          mov       word [esi+18h],3
  004ad257  66c7461a0600          mov       word [esi+1Ah],6
  004ad25d  8916                  mov       [esi],edx
  004ad25f  e9ee010000            jmp       004AD452h

LAB_004ad264:
  004ad264  c6464300              mov       byte [esi+43h],0
  004ad268  e9e5010000            jmp       004AD452h
  004ad26d  8b4e20                mov       ecx,[esi+20h]
  004ad270  8b442424              mov       eax,[esp+24h]
  004ad274  8b4020                mov       eax,[eax+20h]
  004ad277  8d9100400b00          lea       edx,[ecx+0B4000h]
  004ad27d  3bd0                  cmp       edx,eax
  004ad27f  7d0d                  jge       short 004AD28Eh
  004ad281  8b06                  mov       eax,[esi]
  004ad283  c6464300              mov       byte [esi+43h],0
  004ad287  24fd                  and       al,0FDh
  004ad289  e9c2010000            jmp       004AD450h

LAB_004ad28e:
  004ad28e  2bc1                  sub       eax,ecx
  004ad290  0500800200            add       eax,28000h
  004ad295  c1f802                sar       eax,2
  004ad298  03c1                  add       eax,ecx
  004ad29a  668b4e12              mov       cx,[esi+12h]
  004ad29e  6683f901              cmp       cx,1
  004ad2a2  894620                mov       [esi+20h],eax
  004ad2a5  7511                  jne       short 004AD2B8h
  004ad2a7  8b06                  mov       eax,[esi]
  004ad2a9  a900000002            test      eax,2000000h
  004ad2ae  7408                  je        short 004AD2B8h
  004ad2b0  24fd                  and       al,0FDh
  004ad2b2  c6464300              mov       byte [esi+43h],0
  004ad2b6  8906                  mov       [esi],eax

LAB_004ad2b8:
  004ad2b8  6685c9                test      cx,cx
  004ad2bb  0f8591010000          jne       near 004AD452h
  004ad2c1  8b06                  mov       eax,[esi]
  004ad2c3  66c746120100          mov       word [esi+12h],1
  004ad2c9  25fffffffe            and       eax,0FEFFFFFFh
  004ad2ce  e97d010000            jmp       004AD450h
  004ad2d3  66837e1201            cmp       word [esi+12h],1
  004ad2d8  8b7c2410              mov       edi,[esp+10h]
  004ad2dc  8b542418              mov       edx,[esp+18h]
  004ad2e0  7579                  jne       short 004AD35Bh
  004ad2e2  8b4620                mov       eax,[esi+20h]
  004ad2e5  8b6e60                mov       ebp,[esi+60h]
  004ad2e8  0500300000            add       eax,3000h
  004ad2ed  894620                mov       [esi+20h],eax
  004ad2f0  2bc5                  sub       eax,ebp
  004ad2f2  c1f80c                sar       eax,0Ch
  004ad2f5  3dc8000000            cmp       eax,0C8h
  004ad2fa  7e25                  jle       short 004AD321h
  004ad2fc  8b06                  mov       eax,[esi]
  004ad2fe  33db                  xor       ebx,ebx
  004ad300  25fffffffe            and       eax,0FEFFFFFFh
  004ad305  66c746120200          mov       word [esi+12h],2
  004ad30b  0c02                  or        al,2
  004ad30d  66895e10              mov       [esi+10h],bx
  004ad311  66c746180300          mov       word [esi+18h],3
  004ad317  66c7461a0600          mov       word [esi+1Ah],6
  004ad31d  8906                  mov       [esi],eax
  004ad31f  eb02                  jmp       short 004AD323h

LAB_004ad321:
  004ad321  33db                  xor       ebx,ebx

LAB_004ad323:
  004ad323  8d8a00010000          lea       ecx,[edx+100h]
  004ad329  81e1ff0f0000          and       ecx,0FFFh
  004ad32f  81f900020000          cmp       ecx,200h
  004ad335  7e26                  jle       short 004AD35Dh
  004ad337  8bc2                  mov       eax,edx
  004ad339  25ff0f0000            and       eax,0FFFh
  004ad33e  3d00080000            cmp       eax,800h
  004ad343  7d09                  jge       short 004AD34Eh
  004ad345  8d4732                lea       eax,[edi+32h]
  004ad348  66014632              add       [esi+32h],ax
  004ad34c  eb0f                  jmp       short 004AD35Dh

LAB_004ad34e:
  004ad34e  b8ceffffff            mov       eax,0FFFFFFCEh
  004ad353  2bc7                  sub       eax,edi
  004ad355  66014632              add       [esi+32h],ax
  004ad359  eb02                  jmp       short 004AD35Dh

LAB_004ad35b:
  004ad35b  33db                  xor       ebx,ebx

LAB_004ad35d:
  004ad35d  66837e1202            cmp       word [esi+12h],2
  004ad362  0f85a1000000          jne       near 004AD409h
  004ad368  8bcf                  mov       ecx,edi
  004ad36a  b814000000            mov       eax,14h
  004ad36f  83e1fc                and       ecx,0FFFFFFFCh
  004ad372  c1e10a                shl       ecx,0Ah
  004ad375  83c10c                add       ecx,0Ch
  004ad378  d3e0                  shl       eax,cl
  004ad37a  8b4e20                mov       ecx,[esi+20h]
  004ad37d  8da900400b00          lea       ebp,[ecx+0B4000h]
  004ad383  89465c                mov       [esi+5Ch],eax
  004ad386  8b442424              mov       eax,[esp+24h]
  004ad38a  8b4020                mov       eax,[eax+20h]
  004ad38d  3be8                  cmp       ebp,eax
  004ad38f  7d12                  jge       short 004AD3A3h
  004ad391  8b06                  mov       eax,[esi]
  004ad393  c6464300              mov       byte [esi+43h],0
  004ad397  24fd                  and       al,0FDh
  004ad399  895e5c                mov       [esi+5Ch],ebx
  004ad39c  8906                  mov       [esi],eax
  004ad39e  e9af000000            jmp       004AD452h

LAB_004ad3a3:
  004ad3a3  2bc1                  sub       eax,ecx
  004ad3a5  0500400100            add       eax,14000h
  004ad3aa  c1f802                sar       eax,2
  004ad3ad  03c1                  add       eax,ecx
  004ad3af  66ff4610              inc       word [esi+10h]
  004ad3b3  668b4e10              mov       cx,[esi+10h]
  004ad3b7  894620                mov       [esi+20h],eax
  004ad3ba  8d8200010000          lea       eax,[edx+100h]
  004ad3c0  25ff0f0000            and       eax,0FFFh
  004ad3c5  3d00020000            cmp       eax,200h
  004ad3ca  7e1e                  jle       short 004AD3EAh
  004ad3cc  81e2ff0f0000          and       edx,0FFFh
  004ad3d2  81fa00080000          cmp       edx,800h
  004ad3d8  7d05                  jge       short 004AD3DFh
  004ad3da  8d4732                lea       eax,[edi+32h]
  004ad3dd  eb07                  jmp       short 004AD3E6h

LAB_004ad3df:
  004ad3df  b8ceffffff            mov       eax,0FFFFFFCEh
  004ad3e4  2bc7                  sub       eax,edi

LAB_004ad3e6:
  004ad3e6  66014632              add       [esi+32h],ax

LAB_004ad3ea:
  004ad3ea  0fbf4616              movsx     eax,word [esi+16h]
  004ad3ee  83c002                add       eax,2
  004ad3f1  8d1480                lea       edx,[eax+eax*4]
  004ad3f4  0fbfc1                movsx     eax,cx
  004ad3f7  d1e2                  shl       edx,1
  004ad3f9  3bc2                  cmp       eax,edx
  004ad3fb  7c0c                  jl        short 004AD409h
  004ad3fd  8b06                  mov       eax,[esi]
  004ad3ff  66c746120300          mov       word [esi+12h],3
  004ad405  24fd                  and       al,0FDh
  004ad407  8906                  mov       [esi],eax

LAB_004ad409:
  004ad409  668b4e12              mov       cx,[esi+12h]
  004ad40d  6683f903              cmp       cx,3
  004ad411  751d                  jne       short 004AD430h
  004ad413  8b465c                mov       eax,[esi+5Ch]
  004ad416  8bd0                  mov       edx,eax
  004ad418  c1fa03                sar       edx,3
  004ad41b  2bc2                  sub       eax,edx
  004ad41d  89465c                mov       [esi+5Ch],eax
  004ad420  2500f0ffff            and       eax,0FFFFF000h
  004ad425  3d00a00000            cmp       eax,0A000h
  004ad42a  7d04                  jge       short 004AD430h
  004ad42c  c6464300              mov       byte [esi+43h],0

LAB_004ad430:
  004ad430  663bcb                cmp       cx,bx
  004ad433  751d                  jne       short 004AD452h
  004ad435  b801000000            mov       eax,1
  004ad43a  66895e1a              mov       [esi+1Ah],bx
  004ad43e  66894612              mov       [esi+12h],ax
  004ad442  66894618              mov       [esi+18h],ax
  004ad446  8b06                  mov       eax,[esi]
  004ad448  895e5c                mov       [esi+5Ch],ebx
  004ad44b  0d00000001            or        eax,1000000h

LAB_004ad450:
  004ad450  8906                  mov       [esi],eax

LAB_004ad452:
  004ad452  8b0e                  mov       ecx,[esi]
  004ad454  8a4642                mov       al,[esi+42h]
  004ad457  f6c120                test      cl,20h
  004ad45a  746a                  je        short 004AD4C6h
  004ad45c  3c0c                  cmp       al,0Ch
  004ad45e  7404                  je        short 004AD464h
  004ad460  3c0a                  cmp       al,0Ah
  004ad462  7505                  jne       short 004AD469h

LAB_004ad464:
  004ad464  f6c102                test      cl,2
  004ad467  7444                  je        short 004AD4ADh

LAB_004ad469:
  004ad469  e8d29c0900            call      00547140h
  004ad46e  99                    cdq
  004ad46f  b932000000            mov       ecx,32h
  004ad474  8b7e20                mov       edi,[esi+20h]
  004ad477  f7f9                  idiv      ecx
  004ad479  66035614              add       dx,[esi+14h]
  004ad47d  03d1                  add       edx,ecx
  004ad47f  81e2ff0f0000          and       edx,0FFFh
  004ad485  66895614              mov       [esi+14h],dx
  004ad489  0fbfd2                movsx     edx,dx
  004ad48c  52                    push      edx
  004ad48d  e88a850900            call      00545A1Ch
  004ad492  8b4e48                mov       ecx,[esi+48h]
  004ad495  0500300000            add       eax,3000h
  004ad49a  8d0440                lea       eax,[eax+eax*2]
  004ad49d  8d0480                lea       eax,[eax+eax*4]
  004ad4a0  8d1481                lea       edx,[ecx+eax*4]
  004ad4a3  2bd7                  sub       edx,edi
  004ad4a5  c1fa03                sar       edx,3
  004ad4a8  03d7                  add       edx,edi
  004ad4aa  895620                mov       [esi+20h],edx

LAB_004ad4ad:
  004ad4ad  8b4c2424              mov       ecx,[esp+24h]
  004ad4b1  8b4164                mov       eax,[ecx+64h]
  004ad4b4  8b4920                mov       ecx,[ecx+20h]
  004ad4b7  33c8                  xor       ecx,eax
  004ad4b9  f7c100f0ffff          test      ecx,0FFFFF000h
  004ad4bf  7557                  jne       short 004AD518h
  004ad4c1  894648                mov       [esi+48h],eax
  004ad4c4  eb52                  jmp       short 004AD518h

LAB_004ad4c6:
  004ad4c6  3c0c                  cmp       al,0Ch
  004ad4c8  7404                  je        short 004AD4CEh
  004ad4ca  3c0a                  cmp       al,0Ah
  004ad4cc  7505                  jne       short 004AD4D3h

LAB_004ad4ce:
  004ad4ce  f6c102                test      cl,2
  004ad4d1  7445                  je        short 004AD518h

LAB_004ad4d3:
  004ad4d3  e8689c0900            call      00547140h
  004ad4d8  99                    cdq
  004ad4d9  b932000000            mov       ecx,32h
  004ad4de  8b7e20                mov       edi,[esi+20h]
  004ad4e1  f7f9                  idiv      ecx
  004ad4e3  66035614              add       dx,[esi+14h]
  004ad4e7  03d1                  add       edx,ecx
  004ad4e9  81e2ff0f0000          and       edx,0FFFh
  004ad4ef  66895614              mov       [esi+14h],dx
  004ad4f3  0fbfd2                movsx     edx,dx
  004ad4f6  52                    push      edx
  004ad4f7  e820850900            call      00545A1Ch
  004ad4fc  8b4e60                mov       ecx,[esi+60h]
  004ad4ff  8d0440                lea       eax,[eax+eax*2]
  004ad502  8d0480                lea       eax,[eax+eax*4]
  004ad505  8d1481                lea       edx,[ecx+eax*4]
  004ad508  2bd7                  sub       edx,edi
  004ad50a  81c200c00800          add       edx,8C000h
  004ad510  c1fa03                sar       edx,3
  004ad513  03d7                  add       edx,edi
  004ad515  895620                mov       [esi+20h],edx

LAB_004ad518:
  004ad518  0fbf4632              movsx     eax,word [esi+32h]
  004ad51c  50                    push      eax
  004ad51d  e8fa840900            call      00545A1Ch
  004ad522  0fbf4e32              movsx     ecx,word [esi+32h]
  004ad526  51                    push      ecx
  004ad527  89442474              mov       [esp+74h],eax
  004ad52b  e8e6840900            call      00545A16h
  004ad530  8b5650                mov       edx,[esi+50h]
  004ad533  89442478              mov       [esp+78h],eax
  004ad537  8bc2                  mov       eax,edx
  004ad539  8b4c2420              mov       ecx,[esp+20h]
  004ad53d  c1f808                sar       eax,8
  004ad540  0faf4e5c              imul      ecx,[esi+5Ch]
  004ad544  8bfa                  mov       edi,edx
  004ad546  56                    push      esi
  004ad547  2bf8                  sub       edi,eax
  004ad549  c1fa03                sar       edx,3
  004ad54c  2bfa                  sub       edi,edx
  004ad54e  8b5654                mov       edx,[esi+54h]
  004ad551  8bc2                  mov       eax,edx
  004ad553  8bea                  mov       ebp,edx
  004ad555  c1f808                sar       eax,8
  004ad558  2be8                  sub       ebp,eax
  004ad55a  897e50                mov       [esi+50h],edi
  004ad55d  c1fa03                sar       edx,3
  004ad560  2bea                  sub       ebp,edx
  004ad562  8b5658                mov       edx,[esi+58h]
  004ad565  8bda                  mov       ebx,edx
  004ad567  8bc2                  mov       eax,edx
  004ad569  c1fb08                sar       ebx,8
  004ad56c  2bc3                  sub       eax,ebx
  004ad56e  8b5e1c                mov       ebx,[esi+1Ch]
  004ad571  c1fa03                sar       edx,3
  004ad574  c1f90c                sar       ecx,0Ch
  004ad577  2bc2                  sub       eax,edx
  004ad579  8bd1                  mov       edx,ecx
  004ad57b  0faf542474            imul      edx,[esp+74h]
  004ad580  0faf4c247c            imul      ecx,[esp+7Ch]
  004ad585  c1fa0c                sar       edx,0Ch
  004ad588  03d7                  add       edx,edi
  004ad58a  8b7e20                mov       edi,[esi+20h]
  004ad58d  03da                  add       ebx,edx
  004ad58f  03fd                  add       edi,ebp
  004ad591  c1f90c                sar       ecx,0Ch
  004ad594  895e1c                mov       [esi+1Ch],ebx
  004ad597  8b5e24                mov       ebx,[esi+24h]
  004ad59a  03c8                  add       ecx,eax
  004ad59c  896e54                mov       [esi+54h],ebp
  004ad59f  03d9                  add       ebx,ecx
  004ad5a1  894658                mov       [esi+58h],eax
  004ad5a4  897e20                mov       [esi+20h],edi
  004ad5a7  895e24                mov       [esi+24h],ebx
  004ad5aa  e891630800            call      00533940h
  004ad5af  8b8690000000          mov       eax,[esi+90h]
  004ad5b5  83c404                add       esp,4
  004ad5b8  a900200020            test      eax,20002000h
  004ad5bd  7414                  je        short 004AD5D3h
  004ad5bf  807e420a              cmp       byte [esi+42h],0Ah
  004ad5c3  750e                  jne       short 004AD5D3h
  004ad5c5  8b4c2448              mov       ecx,[esp+48h]
  004ad5c9  33db                  xor       ebx,ebx
  004ad5cb  885e43                mov       [esi+43h],bl
  004ad5ce  894e20                mov       [esi+20h],ecx
  004ad5d1  eb02                  jmp       short 004AD5D5h

LAB_004ad5d3:
  004ad5d3  33db                  xor       ebx,ebx

LAB_004ad5d5:
  004ad5d5  8b0e                  mov       ecx,[esi]
  004ad5d7  80e4f8                and       ah,0F8h
  004ad5da  f6c120                test      cl,20h
  004ad5dd  898690000000          mov       [esi+90h],eax
  004ad5e3  740b                  je        short 004AD5F0h
  004ad5e5  25ffdfffdf            and       eax,0DFFFDFFFh
  004ad5ea  898690000000          mov       [esi+90h],eax

LAB_004ad5f0:
  004ad5f0  8b4660                mov       eax,[esi+60h]
  004ad5f3  8b5620                mov       edx,[esi+20h]
  004ad5f6  3bd0                  cmp       edx,eax
  004ad5f8  7f16                  jg        short 004AD610h
  004ad5fa  8b9690000000          mov       edx,[esi+90h]
  004ad600  f6c680                test      dh,80h
  004ad603  750b                  jne       short 004AD610h
  004ad605  83e1fe                and       ecx,0FFFFFFFEh
  004ad608  894620                mov       [esi+20h],eax
  004ad60b  890e                  mov       [esi],ecx
  004ad60d  895e54                mov       [esi+54h],ebx

LAB_004ad610:
  004ad610  f7869000000082a80020  test      dword [esi+90h],2000A882h
  004ad61a  0f84c0000000          je        near 004AD6E0h
  004ad620  8a4e42                mov       cl,[esi+42h]
  004ad623  b007                  mov       al,7
  004ad625  3ac8                  cmp       cl,al
  004ad627  7420                  je        short 004AD649h
  004ad629  884642                mov       [esi+42h],al
  004ad62c  e80f9b0900            call      00547140h
  004ad631  99                    cdq
  004ad632  b928000000            mov       ecx,28h
  004ad637  66895e12              mov       [esi+12h],bx
  004ad63b  f7f9                  idiv      ecx
  004ad63d  66c746105000          mov       word [esi+10h],50h
  004ad643  80c214                add       dl,14h
  004ad646  885643                mov       [esi+43h],dl

LAB_004ad649:
  004ad649  8b16                  mov       edx,[esi]
  004ad64b  8b44244c              mov       eax,[esp+4Ch]
  004ad64f  8b4c2460              mov       ecx,[esp+60h]
  004ad653  83e2fd                and       edx,0FFFFFFFDh
  004ad656  894624                mov       [esi+24h],eax
  004ad659  8b44243c              mov       eax,[esp+3Ch]
  004ad65d  8916                  mov       [esi],edx
  004ad65f  8b542444              mov       edx,[esp+44h]
  004ad663  25ffff0000            and       eax,0FFFFh
  004ad668  89561c                mov       [esi+1Ch],edx
  004ad66b  8b542450              mov       edx,[esp+50h]
  004ad66f  894e7c                mov       [esi+7Ch],ecx
  004ad672  8b4c245c              mov       ecx,[esp+5Ch]
  004ad676  898690000000          mov       [esi+90h],eax
  004ad67c  8b442434              mov       eax,[esp+34h]
  004ad680  899680000000          mov       [esi+80h],edx
  004ad686  8b542454              mov       edx,[esp+54h]
  004ad68a  898684000000          mov       [esi+84h],eax
  004ad690  8b442440              mov       eax,[esp+40h]
  004ad694  898e88000000          mov       [esi+88h],ecx
  004ad69a  8b4c2438              mov       ecx,[esp+38h]
  004ad69e  6882a80020            push      2000A882h
  004ad6a3  56                    push      esi
  004ad6a4  89968c000000          mov       [esi+8Ch],edx
  004ad6aa  894664                mov       [esi+64h],eax
  004ad6ad  894e60                mov       [esi+60h],ecx
  004ad6b0  e8ab870200            call      004D5E60h
  004ad6b5  83c408                add       esp,8
  004ad6b8  663bc3                cmp       ax,bx
  004ad6bb  7515                  jne       short 004AD6D2h
  004ad6bd  e87e9a0900            call      00547140h
  004ad6c2  99                    cdq
  004ad6c3  33c2                  xor       eax,edx
  004ad6c5  2bc2                  sub       eax,edx
  004ad6c7  83e001                and       eax,1
  004ad6ca  33c2                  xor       eax,edx
  004ad6cc  2bc2                  sub       eax,edx
  004ad6ce  8d4400ff              lea       eax,[eax+eax-1]

LAB_004ad6d2:
  004ad6d2  8b542410              mov       edx,[esp+10h]
  004ad6d6  83c240                add       edx,40h
  004ad6d9  0fafd0                imul      edx,eax
  004ad6dc  66015632              add       [esi+32h],dx

LAB_004ad6e0:
  004ad6e0  6aff                  push      0FFFFFFFFh
  004ad6e2  6aff                  push      0FFFFFFFFh
  004ad6e4  6aff                  push      0FFFFFFFFh
  004ad6e6  56                    push      esi
  004ad6e7  e824610100            call      004C3810h
  004ad6ec  8b442430              mov       eax,[esp+30h]
  004ad6f0  83c410                add       esp,10h
  004ad6f3  3bc3                  cmp       eax,ebx
  004ad6f5  7517                  jne       short 004AD70Eh
  004ad6f7  668b842486000000      mov       ax,[esp+86h]
  004ad6ff  66894632              mov       [esi+32h],ax
  004ad703  5f                    pop       edi
  004ad704  5e                    pop       esi
  004ad705  5d                    pop       ebp
  004ad706  5b                    pop       ebx
  004ad707  81c488000000          add       esp,88h
  004ad70d  c3                    ret

LAB_004ad70e:
  004ad70e  385e43                cmp       [esi+43h],bl
  004ad711  0f8588030000          jne       near 004ADA9Fh
  004ad717  8b466c                mov       eax,[esi+6Ch]
  004ad71a  8b4e1c                mov       ecx,[esi+1Ch]
  004ad71d  8b5624                mov       edx,[esi+24h]
  004ad720  8b781c                mov       edi,[eax+1Ch]
  004ad723  8b6824                mov       ebp,[eax+24h]
  004ad726  2bf9                  sub       edi,ecx
  004ad728  2bea                  sub       ebp,edx
  004ad72a  c1ff0c                sar       edi,0Ch
  004ad72d  c1fd0c                sar       ebp,0Ch
  004ad730  8bcf                  mov       ecx,edi
  004ad732  8bd5                  mov       edx,ebp
  004ad734  0fafcf                imul      ecx,edi
  004ad737  0fafd5                imul      edx,ebp
  004ad73a  03ca                  add       ecx,edx
  004ad73c  51                    push      ecx
  004ad73d  e8ce820900            call      00545A10h
  004ad742  55                    push      ebp
  004ad743  57                    push      edi
  004ad744  8944241c              mov       [esp+1Ch],eax
  004ad748  e8d5820900            call      00545A22h
  004ad74d  668b4e32              mov       cx,[esi+32h]
  004ad751  66895e12              mov       [esi+12h],bx
  004ad755  2bc1                  sub       eax,ecx
  004ad757  25ff0f0000            and       eax,0FFFh
  004ad75c  89442418              mov       [esp+18h],eax
  004ad760  8b442414              mov       eax,[esp+14h]
  004ad764  3d96000000            cmp       eax,96h
  004ad769  0f8d21010000          jge       near 004AD890h
  004ad76f  e8cc990900            call      00547140h
  004ad774  99                    cdq
  004ad775  b964000000            mov       ecx,64h
  004ad77a  f7f9                  idiv      ecx
  004ad77c  8bea                  mov       ebp,edx
  004ad77e  83fd32                cmp       ebp,32h
  004ad781  0f8d87000000          jge       near 004AD80Eh
  004ad787  8b542418              mov       edx,[esp+18h]
  004ad78b  8b7c241c              mov       edi,[esp+1Ch]
  004ad78f  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004ad796  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004ad79d  25ff0f0000            and       eax,0FFFh
  004ad7a2  3bc1                  cmp       eax,ecx
  004ad7a4  7f5b                  jg        short 004AD801h
  004ad7a6  e895990900            call      00547140h
  004ad7ab  99                    cdq
  004ad7ac  8d4c3f01              lea       ecx,[edi+edi+1]
  004ad7b0  f7f9                  idiv      ecx
  004ad7b2  8b442414              mov       eax,[esp+14h]
  004ad7b6  83c264                add       edx,64h
  004ad7b9  3bc2                  cmp       eax,edx
  004ad7bb  7d44                  jge       short 004AD801h
  004ad7bd  83fd1e                cmp       ebp,1Eh
  004ad7c0  7d27                  jge       short 004AD7E9h
  004ad7c2  8b16                  mov       edx,[esi]
  004ad7c4  c646420b              mov       byte [esi+42h],0Bh
  004ad7c8  81e2fffffffe          and       edx,0FEFFFFFFh
  004ad7ce  c6464301              mov       byte [esi+43h],1
  004ad7d2  83ca02                or        edx,2
  004ad7d5  895e5c                mov       [esi+5Ch],ebx
  004ad7d8  8916                  mov       [esi],edx
  004ad7da  66c746180300          mov       word [esi+18h],3
  004ad7e0  66895e1a              mov       [esi+1Ah],bx
  004ad7e4  e9ab000000            jmp       004AD894h

LAB_004ad7e9:
  004ad7e9  885e42                mov       [esi+42h],bl
  004ad7ec  e84f990900            call      00547140h
  004ad7f1  99                    cdq
  004ad7f2  b928000000            mov       ecx,28h
  004ad7f7  f7f9                  idiv      ecx
  004ad7f9  885643                mov       [esi+43h],dl
  004ad7fc  e993000000            jmp       004AD894h

LAB_004ad801:
  004ad801  c6464201              mov       byte [esi+42h],1
  004ad805  c6464301              mov       byte [esi+43h],1
  004ad809  e986000000            jmp       004AD894h

LAB_004ad80e:
  004ad80e  83fd46                cmp       ebp,46h
  004ad811  7d3e                  jge       short 004AD851h
  004ad813  c6464203              mov       byte [esi+42h],3
  004ad817  66895e12              mov       [esi+12h],bx
  004ad81b  e820990900            call      00547140h
  004ad820  99                    cdq
  004ad821  b90a000000            mov       ecx,0Ah
  004ad826  8b7c241c              mov       edi,[esp+1Ch]
  004ad82a  f7f9                  idiv      ecx
  004ad82c  8bc7                  mov       eax,edi
  004ad82e  d1f8                  sar       eax,1
  004ad830  02d0                  add       dl,al
  004ad832  80c214                add       dl,14h
  004ad835  885643                mov       [esi+43h],dl
  004ad838  e803990900            call      00547140h
  004ad83d  99                    cdq
  004ad83e  33c2                  xor       eax,edx
  004ad840  2bc2                  sub       eax,edx
  004ad842  25ff0f0000            and       eax,0FFFh
  004ad847  33c2                  xor       eax,edx
  004ad849  2bc2                  sub       eax,edx
  004ad84b  66894610              mov       [esi+10h],ax
  004ad84f  eb43                  jmp       short 004AD894h

LAB_004ad851:
  004ad851  8b542410              mov       edx,[esp+10h]
  004ad855  b964000000            mov       ecx,64h
  004ad85a  d1fa                  sar       edx,1
  004ad85c  2bcd                  sub       ecx,ebp
  004ad85e  83c20a                add       edx,0Ah
  004ad861  3bca                  cmp       ecx,edx
  004ad863  7d23                  jge       short 004AD888h
  004ad865  66395e16              cmp       [esi+16h],bx
  004ad869  751d                  jne       short 004AD888h
  004ad86b  c646420a              mov       byte [esi+42h],0Ah
  004ad86f  e8cc980900            call      00547140h
  004ad874  99                    cdq
  004ad875  b91e000000            mov       ecx,1Eh
  004ad87a  66895e12              mov       [esi+12h],bx
  004ad87e  f7f9                  idiv      ecx
  004ad880  80c214                add       dl,14h
  004ad883  885643                mov       [esi+43h],dl
  004ad886  eb08                  jmp       short 004AD890h

LAB_004ad888:
  004ad888  c646420c              mov       byte [esi+42h],0Ch
  004ad88c  c6464301              mov       byte [esi+43h],1

LAB_004ad890:
  004ad890  8b7c241c              mov       edi,[esp+1Ch]

LAB_004ad894:
  004ad894  8b442414              mov       eax,[esp+14h]
  004ad898  3d58020000            cmp       eax,258h
  004ad89d  0f8d12010000          jge       near 004AD9B5h
  004ad8a3  3d96000000            cmp       eax,96h
  004ad8a8  0f8c07010000          jl        near 004AD9B5h
  004ad8ae  e88d980900            call      00547140h
  004ad8b3  99                    cdq
  004ad8b4  b964000000            mov       ecx,64h
  004ad8b9  f7f9                  idiv      ecx
  004ad8bb  83fa14                cmp       edx,14h
  004ad8be  7d29                  jge       short 004AD8E9h
  004ad8c0  8b542418              mov       edx,[esp+18h]
  004ad8c4  81c200040000          add       edx,400h
  004ad8ca  81e2ff0f0000          and       edx,0FFFh
  004ad8d0  81fa00080000          cmp       edx,800h
  004ad8d6  0f8cbc000000          jl        near 004AD998h
  004ad8dc  c6464201              mov       byte [esi+42h],1
  004ad8e0  c6464301              mov       byte [esi+43h],1
  004ad8e4  e9cc000000            jmp       004AD9B5h

LAB_004ad8e9:
  004ad8e9  83fa28                cmp       edx,28h
  004ad8ec  7c11                  jl        short 004AD8FFh
  004ad8ee  83fa50                cmp       edx,50h
  004ad8f1  7d6e                  jge       short 004AD961h
  004ad8f3  66837e1601            cmp       word [esi+16h],1
  004ad8f8  752d                  jne       short 004AD927h
  004ad8fa  83fa3c                cmp       edx,3Ch
  004ad8fd  7d28                  jge       short 004AD927h

LAB_004ad8ff:
  004ad8ff  c6464202              mov       byte [esi+42h],2
  004ad903  e838980900            call      00547140h
  004ad908  99                    cdq
  004ad909  b90a000000            mov       ecx,0Ah
  004ad90e  66c746105000          mov       word [esi+10h],50h
  004ad914  f7f9                  idiv      ecx
  004ad916  8bc7                  mov       eax,edi
  004ad918  d1f8                  sar       eax,1
  004ad91a  02d0                  add       dl,al
  004ad91c  80c214                add       dl,14h
  004ad91f  885643                mov       [esi+43h],dl
  004ad922  e98e000000            jmp       004AD9B5h

LAB_004ad927:
  004ad927  c6464203              mov       byte [esi+42h],3
  004ad92b  66895e12              mov       [esi+12h],bx
  004ad92f  e80c980900            call      00547140h
  004ad934  99                    cdq
  004ad935  b90a000000            mov       ecx,0Ah
  004ad93a  f7f9                  idiv      ecx
  004ad93c  8bc7                  mov       eax,edi
  004ad93e  d1f8                  sar       eax,1
  004ad940  02d0                  add       dl,al
  004ad942  80c214                add       dl,14h
  004ad945  885643                mov       [esi+43h],dl
  004ad948  e8f3970900            call      00547140h
  004ad94d  99                    cdq
  004ad94e  33c2                  xor       eax,edx
  004ad950  2bc2                  sub       eax,edx
  004ad952  25ff0f0000            and       eax,0FFFh
  004ad957  33c2                  xor       eax,edx
  004ad959  2bc2                  sub       eax,edx
  004ad95b  66894610              mov       [esi+10h],ax
  004ad95f  eb54                  jmp       short 004AD9B5h

LAB_004ad961:
  004ad961  b964000000            mov       ecx,64h
  004ad966  2bca                  sub       ecx,edx
  004ad968  8b542410              mov       edx,[esp+10h]
  004ad96c  d1fa                  sar       edx,1
  004ad96e  83c20a                add       edx,0Ah
  004ad971  3bca                  cmp       ecx,edx
  004ad973  7d2d                  jge       short 004AD9A2h
  004ad975  66395e16              cmp       [esi+16h],bx
  004ad979  751d                  jne       short 004AD998h
  004ad97b  c646420a              mov       byte [esi+42h],0Ah
  004ad97f  e8bc970900            call      00547140h
  004ad984  99                    cdq
  004ad985  b91e000000            mov       ecx,1Eh
  004ad98a  66895e12              mov       [esi+12h],bx
  004ad98e  f7f9                  idiv      ecx
  004ad990  80c214                add       dl,14h
  004ad993  885643                mov       [esi+43h],dl
  004ad996  eb1d                  jmp       short 004AD9B5h

LAB_004ad998:
  004ad998  c646420c              mov       byte [esi+42h],0Ch
  004ad99c  c6464301              mov       byte [esi+43h],1
  004ad9a0  eb13                  jmp       short 004AD9B5h

LAB_004ad9a2:
  004ad9a2  885e42                mov       [esi+42h],bl
  004ad9a5  e896970900            call      00547140h
  004ad9aa  99                    cdq
  004ad9ab  b928000000            mov       ecx,28h
  004ad9b0  f7f9                  idiv      ecx
  004ad9b2  885643                mov       [esi+43h],dl

LAB_004ad9b5:
  004ad9b5  817c241458020000      cmp       dword [esp+14h],258h
  004ad9bd  0f8cdc000000          jl        near 004ADA9Fh
  004ad9c3  e878970900            call      00547140h
  004ad9c8  bd64000000            mov       ebp,64h
  004ad9cd  99                    cdq
  004ad9ce  8bcd                  mov       ecx,ebp
  004ad9d0  f7f9                  idiv      ecx
  004ad9d2  83fa32                cmp       edx,32h
  004ad9d5  7d45                  jge       short 004ADA1Ch
  004ad9d7  8b442410              mov       eax,[esp+10h]
  004ad9db  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004ad9df  3bd1                  cmp       edx,ecx
  004ad9e1  0f8c97000000          jl        near 004ADA7Eh
  004ad9e7  885e42                mov       [esi+42h],bl
  004ad9ea  e851970900            call      00547140h
  004ad9ef  99                    cdq
  004ad9f0  b90a000000            mov       ecx,0Ah
  004ad9f5  66895e12              mov       [esi+12h],bx
  004ad9f9  f7f9                  idiv      ecx
  004ad9fb  8b4c2410              mov       ecx,[esp+10h]
  004ad9ff  8bc7                  mov       eax,edi
  004ada01  c1f802                sar       eax,2
  004ada04  02d0                  add       dl,al
  004ada06  885643                mov       [esi+43h],dl
  004ada09  8d540950              lea       edx,[ecx+ecx+50h]
  004ada0d  66895610              mov       [esi+10h],dx
  004ada11  5f                    pop       edi
  004ada12  5e                    pop       esi
  004ada13  5d                    pop       ebp
  004ada14  5b                    pop       ebx
  004ada15  81c488000000          add       esp,88h
  004ada1b  c3                    ret

LAB_004ada1c:
  004ada1c  83fa4b                cmp       edx,4Bh
  004ada1f  7d5d                  jge       short 004ADA7Eh
  004ada21  66837e1601            cmp       word [esi+16h],1
  004ada26  7513                  jne       short 004ADA3Bh
  004ada28  c646420c              mov       byte [esi+42h],0Ch
  004ada2c  c6464301              mov       byte [esi+43h],1
  004ada30  5f                    pop       edi
  004ada31  5e                    pop       esi
  004ada32  5d                    pop       ebp
  004ada33  5b                    pop       ebx
  004ada34  81c488000000          add       esp,88h
  004ada3a  c3                    ret

LAB_004ada3b:
  004ada3b  c6464203              mov       byte [esi+42h],3
  004ada3f  66895e12              mov       [esi+12h],bx
  004ada43  e8f8960900            call      00547140h
  004ada48  99                    cdq
  004ada49  b90a000000            mov       ecx,0Ah
  004ada4e  f7f9                  idiv      ecx
  004ada50  8bc7                  mov       eax,edi
  004ada52  d1f8                  sar       eax,1
  004ada54  02d0                  add       dl,al
  004ada56  80c214                add       dl,14h
  004ada59  885643                mov       [esi+43h],dl
  004ada5c  e8df960900            call      00547140h
  004ada61  99                    cdq
  004ada62  33c2                  xor       eax,edx
  004ada64  2bc2                  sub       eax,edx
  004ada66  25ff0f0000            and       eax,0FFFh
  004ada6b  33c2                  xor       eax,edx
  004ada6d  2bc2                  sub       eax,edx
  004ada6f  66894610              mov       [esi+10h],ax
  004ada73  5f                    pop       edi
  004ada74  5e                    pop       esi
  004ada75  5d                    pop       ebp
  004ada76  5b                    pop       ebx
  004ada77  81c488000000          add       esp,88h
  004ada7d  c3                    ret

LAB_004ada7e:
  004ada7e  c6464202              mov       byte [esi+42h],2
  004ada82  e8b9960900            call      00547140h
  004ada87  99                    cdq
  004ada88  b90a000000            mov       ecx,0Ah
  004ada8d  66896e10              mov       [esi+10h],bp
  004ada91  f7f9                  idiv      ecx
  004ada93  8bc7                  mov       eax,edi
  004ada95  d1f8                  sar       eax,1
  004ada97  02d0                  add       dl,al
  004ada99  80c21e                add       dl,1Eh
  004ada9c  885643                mov       [esi+43h],dl

LAB_004ada9f:
  004ada9f  5f                    pop       edi
  004adaa0  5e                    pop       esi
  004adaa1  5d                    pop       ebp
  004adaa2  5b                    pop       ebx
  004adaa3  81c488000000          add       esp,88h
  004adaa9  c3                    ret
  004adaaa  8bff                  mov       edi,edi
  004adaac  97                    xchg      edi,eax
  004adaad  ce                    into
  004adaae  4a                    dec       edx
  004adaaf  00ec                  add       ah,ch
  004adab1  ce                    into
  004adab2  4a                    dec       edx
  004adab3  0074cf4a              add       [edi+ecx*8+4Ah],dh
  004adab7  0020                  add       [eax],ah
  004adab9  d04a00                ror       byte [edx],1
  004adabc  52                    push      edx
  004adabd  d44a                  aam       4Ah
  004adabf  0052d4                add       [edx-2Ch],dl
  004adac2  4a                    dec       edx
  004adac3  0052d4                add       [edx-2Ch],dl
  004adac6  4a                    dec       edx
  004adac7  00f5                  add       ch,dh
  004adac9  d04a00                ror       byte [edx],1
  004adacc  52                    push      edx
  004adacd  d44a                  aam       4Ah
  004adacf  0052d4                add       [edx-2Ch],dl
  004adad2  4a                    dec       edx
  004adad3  0055d1                add       [ebp-2Fh],dl
  004adad6  4a                    dec       edx
  004adad7  006dd2                add       [ebp-2Eh],ch
  004adada  4a                    dec       edx
  004adadb  00d3                  add       bl,dl
  004adadd  d24a00                ror       byte [edx],cl

81 cible(s) de saut interne : 0x004acc8d, 0x004acc91, 0x004acc93, 0x004acdde, 0x004ace00, 0x004ace65, 0x004ace69, 0x004ace82, 0x004acebd, 0x004aced0, 0x004acf0b, 0x004acf33, 0x004acf59, 0x004acf68, 0x004acfaf, 0x004acfcd, 0x004acfd9, 0x004ad00e, 0x004ad037, 0x004ad0bd, 0x004ad0cb, 0x004ad0cf, 0x004ad10f, 0x004ad11b, 0x004ad175, 0x004ad18a, 0x004ad1ae, 0x004ad1be, 0x004ad1cc, 0x004ad1df, 0x004ad208, 0x004ad223, 0x004ad264, 0x004ad28e, 0x004ad2b8, 0x004ad321, 0x004ad323, 0x004ad34e, 0x004ad35b, 0x004ad35d, 0x004ad3a3, 0x004ad3df, 0x004ad3e6, 0x004ad3ea, 0x004ad409, 0x004ad430, 0x004ad450, 0x004ad452, 0x004ad464, 0x004ad469, 0x004ad4ad, 0x004ad4c6, 0x004ad4ce, 0x004ad4d3, 0x004ad518, 0x004ad5d3, 0x004ad5d5, 0x004ad5f0, 0x004ad610, 0x004ad649, 0x004ad6d2, 0x004ad6e0, 0x004ad70e, 0x004ad7e9, 0x004ad801, 0x004ad80e, 0x004ad851, 0x004ad888, 0x004ad890, 0x004ad894, 0x004ad8e9, 0x004ad8ff, 0x004ad927, 0x004ad961, 0x004ad998, 0x004ad9a2, 0x004ad9b5, 0x004ada1c, 0x004ada3b, 0x004ada7e, 0x004ada9f
