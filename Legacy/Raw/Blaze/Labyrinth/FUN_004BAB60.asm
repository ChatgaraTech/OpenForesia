Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004bab60  section .text  offset fichier 0xb9f60  3328 octets

  004bab60  81ec84000000          sub       esp,84h
  004bab66  53                    push      ebx
  004bab67  55                    push      ebp
  004bab68  56                    push      esi
  004bab69  8bb42494000000        mov       esi,[esp+94h]
  004bab70  b864000000            mov       eax,64h
  004bab75  33db                  xor       ebx,ebx
  004bab77  6689442432            mov       [esp+32h],ax
  004bab7c  6689442436            mov       [esp+36h],ax
  004bab81  8a4641                mov       al,[esi+41h]
  004bab84  57                    push      edi
  004bab85  3ac3                  cmp       al,bl
  004bab87  66895c2430            mov       [esp+30h],bx
  004bab8c  66c74424321e00        mov       word [esp+32h],1Eh
  004bab93  66c74424343200        mov       word [esp+34h],32h
  004bab9a  66895c2438            mov       [esp+38h],bx
  004bab9f  7510                  jne       short 004BABB1h
  004baba1  53                    push      ebx
  004baba2  6a03                  push      3
  004baba4  56                    push      esi
  004baba5  c6464101              mov       byte [esi+41h],1
  004baba9  e872e6ffff            call      004B9220h
  004babae  83c40c                add       esp,0Ch

LAB_004babb1:
  004babb1  8b461c                mov       eax,[esi+1Ch]
  004babb4  8b4e20                mov       ecx,[esi+20h]
  004babb7  8b5624                mov       edx,[esi+24h]
  004babba  8d6e1c                lea       ebp,[esi+1Ch]
  004babbd  89442424              mov       [esp+24h],eax
  004babc1  8b4664                mov       eax,[esi+64h]
  004babc4  894c2428              mov       [esp+28h],ecx
  004babc8  8b4e60                mov       ecx,[esi+60h]
  004babcb  8954242c              mov       [esp+2Ch],edx
  004babcf  668b5632              mov       dx,[esi+32h]
  004babd3  89442440              mov       [esp+40h],eax
  004babd7  8b467c                mov       eax,[esi+7Ch]
  004babda  894c2448              mov       [esp+48h],ecx
  004babde  668b8e90000000        mov       cx,[esi+90h]
  004babe5  6689942482000000      mov       [esp+82h],dx
  004babed  8b9680000000          mov       edx,[esi+80h]
  004babf3  89442454              mov       [esp+54h],eax
  004babf7  8b8684000000          mov       eax,[esi+84h]
  004babfd  66894c2458            mov       [esp+58h],cx
  004bac02  8b8e88000000          mov       ecx,[esi+88h]
  004bac08  8b7e6c                mov       edi,[esi+6Ch]
  004bac0b  89542444              mov       [esp+44h],edx
  004bac0f  8b968c000000          mov       edx,[esi+8Ch]
  004bac15  89442450              mov       [esp+50h],eax
  004bac19  894c243c              mov       [esp+3Ch],ecx
  004bac1d  8d44245c              lea       eax,[esp+5Ch]
  004bac21  8954244c              mov       [esp+4Ch],edx
  004bac25  8d4c2460              lea       ecx,[esp+60h]
  004bac29  50                    push      eax
  004bac2a  8d542468              lea       edx,[esp+68h]
  004bac2e  51                    push      ecx
  004bac2f  52                    push      edx
  004bac30  56                    push      esi
  004bac31  e84add0700            call      00538980h
  004bac36  83c410                add       esp,10h
  004bac39  8d442468              lea       eax,[esp+68h]
  004bac3d  8d4c241c              lea       ecx,[esp+1Ch]
  004bac41  8d542418              lea       edx,[esp+18h]
  004bac45  50                    push      eax
  004bac46  51                    push      ecx
  004bac47  8d44241c              lea       eax,[esp+1Ch]
  004bac4b  52                    push      edx
  004bac4c  8d4c241c              lea       ecx,[esp+1Ch]
  004bac50  50                    push      eax
  004bac51  51                    push      ecx
  004bac52  56                    push      esi
  004bac53  e8c8b20700            call      00535F20h
  004bac58  8b4c2428              mov       ecx,[esp+28h]
  004bac5c  8b571c                mov       edx,[edi+1Ch]
  004bac5f  b840000000            mov       eax,40h
  004bac64  83c418                add       esp,18h
  004bac67  2bc1                  sub       eax,ecx
  004bac69  8b4e20                mov       ecx,[esi+20h]
  004bac6c  89442420              mov       [esp+20h],eax
  004bac70  8b4500                mov       eax,[ebp]
  004bac73  2bc2                  sub       eax,edx
  004bac75  8b5720                mov       edx,[edi+20h]
  004bac78  2bca                  sub       ecx,edx
  004bac7a  8b5624                mov       edx,[esi+24h]
  004bac7d  2b5724                sub       edx,[edi+24h]
  004bac80  c1f80c                sar       eax,0Ch
  004bac83  8bf8                  mov       edi,eax
  004bac85  c1f90c                sar       ecx,0Ch
  004bac88  0faff8                imul      edi,eax
  004bac8b  8bc1                  mov       eax,ecx
  004bac8d  c1fa0c                sar       edx,0Ch
  004bac90  0fafc1                imul      eax,ecx
  004bac93  8bca                  mov       ecx,edx
  004bac95  03f8                  add       edi,eax
  004bac97  0fafca                imul      ecx,edx
  004bac9a  03f9                  add       edi,ecx
  004bac9c  57                    push      edi
  004bac9d  e86ead0800            call      00545A10h
  004baca2  89442414              mov       [esp+14h],eax
  004baca6  8b44241c              mov       eax,[esp+1Ch]
  004bacaa  3bc3                  cmp       eax,ebx
  004bacac  0f8460050000          je        near 004BB212h
  004bacb2  8b0e                  mov       ecx,[esi]
  004bacb4  33d2                  xor       edx,edx
  004bacb6  8a5604                mov       dl,[esi+4]
  004bacb9  81e1fffffffd          and       ecx,0FDFFFFFFh
  004bacbf  0fbf7e18              movsx     edi,word [esi+18h]
  004bacc3  890e                  mov       [esi],ecx
  004bacc5  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004baccc  f7c100000001          test      ecx,1000000h
  004bacd2  8b14ba                mov       edx,[edx+edi*4]
  004bacd5  741b                  je        short 004BACF2h
  004bacd7  66ff461a              inc       word [esi+1Ah]
  004bacdb  668b4e1a              mov       cx,[esi+1Ah]
  004bacdf  33ff                  xor       edi,edi
  004bace1  668b7a16              mov       di,[edx+16h]
  004bace5  0fbfc9                movsx     ecx,cx
  004bace8  3bcf                  cmp       ecx,edi
  004bacea  7c2a                  jl        short 004BAD16h
  004bacec  66895e1a              mov       [esi+1Ah],bx
  004bacf0  eb24                  jmp       short 004BAD16h

LAB_004bacf2:
  004bacf2  66ff461a              inc       word [esi+1Ah]
  004bacf6  668b7e1a              mov       di,[esi+1Ah]
  004bacfa  33db                  xor       ebx,ebx
  004bacfc  668b5a16              mov       bx,[edx+16h]
  004bad00  0fbfd7                movsx     edx,di
  004bad03  3bd3                  cmp       edx,ebx
  004bad05  7c0d                  jl        short 004BAD14h
  004bad07  4f                    dec       edi
  004bad08  81c900000002          or        ecx,2000000h
  004bad0e  66897e1a              mov       [esi+1Ah],di
  004bad12  890e                  mov       [esi],ecx

LAB_004bad14:
  004bad14  33db                  xor       ebx,ebx

LAB_004bad16:
  004bad16  8b0e                  mov       ecx,[esi]
  004bad18  f6c102                test      cl,2
  004bad1b  0f84bc0a0000          je        near 004BB7DDh
  004bad21  66837e1a0f            cmp       word [esi+1Ah],0Fh
  004bad26  7558                  jne       short 004BAD80h
  004bad28  8d442430              lea       eax,[esp+30h]
  004bad2c  53                    push      ebx
  004bad2d  50                    push      eax
  004bad2e  55                    push      ebp
  004bad2f  56                    push      esi
  004bad30  e8fbee0500            call      00519C30h
  004bad35  8bf8                  mov       edi,eax
  004bad37  a1b8e05d00            mov       eax,[5DE0B8h]
  004bad3c  83c410                add       esp,10h
  004bad3f  3bc3                  cmp       eax,ebx
  004bad41  7439                  je        short 004BAD7Ch
  004bad43  66837e1a0f            cmp       word [esi+1Ah],0Fh
  004bad48  7532                  jne       short 004BAD7Ch
  004bad4a  8b0f                  mov       ecx,[edi]
  004bad4c  57                    push      edi
  004bad4d  83c940                or        ecx,40h
  004bad50  56                    push      esi
  004bad51  66c787960000001000    mov       word [edi+96h],10h
  004bad5a  890f                  mov       [edi],ecx
  004bad5c  e81fee0700            call      00539B80h
  004bad61  83c408                add       esp,8
  004bad64  83c71c                add       edi,1Ch
  004bad67  57                    push      edi
  004bad68  6800180000            push      1800h
  004bad6d  6800030000            push      300h
  004bad72  6a03                  push      3
  004bad74  e8a72d0800            call      0053DB20h
  004bad79  83c410                add       esp,10h

LAB_004bad7c:
  004bad7c  8b44241c              mov       eax,[esp+1Ch]

LAB_004bad80:
  004bad80  8b0e                  mov       ecx,[esi]
  004bad82  f7c100000002          test      ecx,2000000h
  004bad88  7411                  je        short 004BAD9Bh
  004bad8a  f6c101                test      cl,1
  004bad8d  0f85400a0000          jne       near 004BB7D3h
  004bad93  83e1fd                and       ecx,0FFFFFFFDh
  004bad96  885e43                mov       [esi+43h],bl
  004bad99  890e                  mov       [esi],ecx

LAB_004bad9b:
  004bad9b  8b4c2410              mov       ecx,[esp+10h]

LAB_004bad9f:
  004bad9f  33d2                  xor       edx,edx
  004bada1  8a5642                mov       dl,[esi+42h]
  004bada4  83fa0b                cmp       edx,0Bh
  004bada7  0f8765040000          ja        near 004BB212h
  004badad  ff249530b84b00        jmp       dword [edx*4+4BB830h]
  004badb4  66395e12              cmp       [esi+12h],bx
  004badb8  751a                  jne       short 004BADD4h
  004badba  8b06                  mov       eax,[esi]
  004badbc  66895e18              mov       [esi+18h],bx
  004badc0  0d00000001            or        eax,1000000h
  004badc5  66895e1a              mov       [esi+1Ah],bx
  004badc9  8906                  mov       [esi],eax
  004badcb  66c746120100          mov       word [esi+12h],1
  004badd1  895e5c                mov       [esi+5Ch],ebx

LAB_004badd4:
  004badd4  0fbf4610              movsx     eax,word [esi+10h]
  004badd8  39442414              cmp       [esp+14h],eax
  004baddc  7d08                  jge       short 004BADE6h
  004badde  885e43                mov       [esi+43h],bl
  004bade1  e92c040000            jmp       004BB212h

LAB_004bade6:
  004bade6  8a4643                mov       al,[esi+43h]
  004bade9  3ac3                  cmp       al,bl
  004badeb  0f84ee030000          je        near 004BB1DFh
  004badf1  fec8                  dec       al
  004badf3  895e5c                mov       [esi+5Ch],ebx
  004badf6  884643                mov       [esi+43h],al
  004badf9  e914040000            jmp       004BB212h
  004badfe  66395e12              cmp       [esi+12h],bx
  004bae02  751b                  jne       short 004BAE1Fh
  004bae04  8b16                  mov       edx,[esi]
  004bae06  b801000000            mov       eax,1
  004bae0b  81ca00000001          or        edx,1000000h
  004bae11  66894618              mov       [esi+18h],ax
  004bae15  66895e1a              mov       [esi+1Ah],bx
  004bae19  8916                  mov       [esi],edx
  004bae1b  66894612              mov       [esi+12h],ax

LAB_004bae1f:
  004bae1f  8b442418              mov       eax,[esp+18h]
  004bae23  8d90c8000000          lea       edx,[eax+0C8h]
  004bae29  81e2ff0f0000          and       edx,0FFFh
  004bae2f  81fa90010000          cmp       edx,190h
  004bae35  7f0b                  jg        short 004BAE42h
  004bae37  885e43                mov       [esi+43h],bl
  004bae3a  895e5c                mov       [esi+5Ch],ebx
  004bae3d  e9d0030000            jmp       004BB212h

LAB_004bae42:
  004bae42  25ff0f0000            and       eax,0FFFh
  004bae47  3d00080000            cmp       eax,800h
  004bae4c  7d12                  jge       short 004BAE60h
  004bae4e  8d8180000000          lea       eax,[ecx+80h]
  004bae54  895e5c                mov       [esi+5Ch],ebx
  004bae57  66014632              add       [esi+32h],ax
  004bae5b  e9b2030000            jmp       004BB212h

LAB_004bae60:
  004bae60  b880ffffff            mov       eax,0FFFFFF80h
  004bae65  895e5c                mov       [esi+5Ch],ebx
  004bae68  2bc1                  sub       eax,ecx
  004bae6a  66014632              add       [esi+32h],ax
  004bae6e  e99f030000            jmp       004BB212h
  004bae73  66395e12              cmp       [esi+12h],bx
  004bae77  b801000000            mov       eax,1
  004bae7c  7525                  jne       short 004BAEA3h
  004bae7e  8b16                  mov       edx,[esi]
  004bae80  66894618              mov       [esi+18h],ax
  004bae84  81ca00000001          or        edx,1000000h
  004bae8a  66895e1a              mov       [esi+1Ah],bx
  004bae8e  8916                  mov       [esi],edx
  004bae90  8d14c9                lea       edx,[ecx+ecx*8]
  004bae93  c1e204                shl       edx,4
  004bae96  81c200500000          add       edx,5000h
  004bae9c  66894612              mov       [esi+12h],ax
  004baea0  89565c                mov       [esi+5Ch],edx

LAB_004baea3:
  004baea3  66394612              cmp       [esi+12h],ax
  004baea7  0f8565030000          jne       near 004BB212h
  004baead  0fbf4610              movsx     eax,word [esi+10h]
  004baeb1  39442414              cmp       [esp+14h],eax
  004baeb5  7d0b                  jge       short 004BAEC2h
  004baeb7  885e43                mov       [esi+43h],bl
  004baeba  895e5c                mov       [esi+5Ch],ebx
  004baebd  e950030000            jmp       004BB212h

LAB_004baec2:
  004baec2  8a4643                mov       al,[esi+43h]
  004baec5  3ac3                  cmp       al,bl
  004baec7  7405                  je        short 004BAECEh
  004baec9  fec8                  dec       al
  004baecb  884643                mov       [esi+43h],al

LAB_004baece:
  004baece  8b442418              mov       eax,[esp+18h]
  004baed2  8d9000010000          lea       edx,[eax+100h]
  004baed8  81e2ff0f0000          and       edx,0FFFh
  004baede  81fa00020000          cmp       edx,200h
  004baee4  0f8e28030000          jle       near 004BB212h
  004baeea  25ff0f0000            and       eax,0FFFh
  004baeef  3d00080000            cmp       eax,800h
  004baef4  7d10                  jge       short 004BAF06h
  004baef6  8d840980000000        lea       eax,[ecx+ecx+80h]
  004baefd  66014632              add       [esi+32h],ax
  004baf01  e90c030000            jmp       004BB212h

LAB_004baf06:
  004baf06  03c9                  add       ecx,ecx
  004baf08  b880ffffff            mov       eax,0FFFFFF80h
  004baf0d  2bc1                  sub       eax,ecx
  004baf0f  66014632              add       [esi+32h],ax
  004baf13  e9fa020000            jmp       004BB212h
  004baf18  66837e1201            cmp       word [esi+12h],1
  004baf1d  0f8596000000          jne       near 004BAFB9h
  004baf23  8a5643                mov       dl,[esi+43h]
  004baf26  3ad3                  cmp       dl,bl
  004baf28  7405                  je        short 004BAF2Fh
  004baf2a  feca                  dec       dl
  004baf2c  885643                mov       [esi+43h],dl

LAB_004baf2f:
  004baf2f  8d14c9                lea       edx,[ecx+ecx*8]
  004baf32  c1e204                shl       edx,4
  004baf35  8d8a00500000          lea       ecx,[edx+5000h]
  004baf3b  0fafc1                imul      eax,ecx
  004baf3e  c1f80c                sar       eax,0Ch
  004baf41  8bf8                  mov       edi,eax
  004baf43  894e5c                mov       [esi+5Ch],ecx
  004baf46  0fbf4610              movsx     eax,word [esi+10h]
  004baf4a  50                    push      eax
  004baf4b  e8ccaa0800            call      00545A1Ch
  004baf50  0fafc7                imul      eax,edi
  004baf53  8b4d00                mov       ecx,[ebp]
  004baf56  c1f80c                sar       eax,0Ch
  004baf59  03c8                  add       ecx,eax
  004baf5b  894d00                mov       [ebp],ecx
  004baf5e  0fbf4e10              movsx     ecx,word [esi+10h]
  004baf62  51                    push      ecx
  004baf63  e8aeaa0800            call      00545A16h
  004baf68  0fafc7                imul      eax,edi
  004baf6b  8b6e24                mov       ebp,[esi+24h]
  004baf6e  895e5c                mov       [esi+5Ch],ebx
  004baf71  c1f80c                sar       eax,0Ch
  004baf74  03e8                  add       ebp,eax
  004baf76  8b442418              mov       eax,[esp+18h]
  004baf7a  896e24                mov       [esi+24h],ebp
  004baf7d  8d9000010000          lea       edx,[eax+100h]
  004baf83  81e2ff0f0000          and       edx,0FFFh
  004baf89  81fa00020000          cmp       edx,200h
  004baf8f  7e28                  jle       short 004BAFB9h
  004baf91  25ff0f0000            and       eax,0FFFh
  004baf96  3d00080000            cmp       eax,800h
  004baf9b  7d0a                  jge       short 004BAFA7h
  004baf9d  8b442410              mov       eax,[esp+10h]
  004bafa1  8d440032              lea       eax,[eax+eax+32h]
  004bafa5  eb0e                  jmp       short 004BAFB5h

LAB_004bafa7:
  004bafa7  8b4c2410              mov       ecx,[esp+10h]
  004bafab  b8ceffffff            mov       eax,0FFFFFFCEh
  004bafb0  8d1409                lea       edx,[ecx+ecx]
  004bafb3  2bc2                  sub       eax,edx

LAB_004bafb5:
  004bafb5  66014632              add       [esi+32h],ax

LAB_004bafb9:
  004bafb9  66395e12              cmp       [esi+12h],bx
  004bafbd  0f854f020000          jne       near 004BB212h
  004bafc3  8b0e                  mov       ecx,[esi]
  004bafc5  b801000000            mov       eax,1
  004bafca  81c900000001          or        ecx,1000000h
  004bafd0  66894618              mov       [esi+18h],ax
  004bafd4  66895e1a              mov       [esi+1Ah],bx
  004bafd8  890e                  mov       [esi],ecx
  004bafda  66894612              mov       [esi+12h],ax
  004bafde  e92f020000            jmp       004BB212h
  004bafe3  66395e12              cmp       [esi+12h],bx
  004bafe7  757b                  jne       short 004BB064h
  004bafe9  8b16                  mov       edx,[esi]
  004bafeb  f6c201                test      dl,1
  004bafee  7405                  je        short 004BAFF5h
  004baff0  885e43                mov       [esi+43h],bl
  004baff3  eb6f                  jmp       short 004BB064h

LAB_004baff5:
  004baff5  81c100010000          add       ecx,100h
  004baffb  81e2fffffffe          and       edx,0FEFFFFFFh
  004bb001  c1e106                shl       ecx,6
  004bb004  0fafc1                imul      eax,ecx
  004bb007  c1f80c                sar       eax,0Ch
  004bb00a  8bf8                  mov       edi,eax
  004bb00c  bd01000000            mov       ebp,1
  004bb011  0fbf4610              movsx     eax,word [esi+10h]
  004bb015  50                    push      eax
  004bb016  8916                  mov       [esi],edx
  004bb018  66896e18              mov       [esi+18h],bp
  004bb01c  66895e1a              mov       [esi+1Ah],bx
  004bb020  894e5c                mov       [esi+5Ch],ecx
  004bb023  e8f4a90800            call      00545A1Ch
  004bb028  0fafc7                imul      eax,edi
  004bb02b  8b4e50                mov       ecx,[esi+50h]
  004bb02e  c1f80c                sar       eax,0Ch
  004bb031  03c8                  add       ecx,eax
  004bb033  894e50                mov       [esi+50h],ecx
  004bb036  0fbf4e10              movsx     ecx,word [esi+10h]
  004bb03a  51                    push      ecx
  004bb03b  e8d6a90800            call      00545A16h
  004bb040  0fafc7                imul      eax,edi
  004bb043  8b5658                mov       edx,[esi+58h]
  004bb046  8b4e54                mov       ecx,[esi+54h]
  004bb049  c1f80c                sar       eax,0Ch
  004bb04c  03d0                  add       edx,eax
  004bb04e  8b06                  mov       eax,[esi]
  004bb050  81c100e00100          add       ecx,1E000h
  004bb056  0bc5                  or        eax,ebp
  004bb058  895658                mov       [esi+58h],edx
  004bb05b  894e54                mov       [esi+54h],ecx
  004bb05e  8906                  mov       [esi],eax
  004bb060  66896e12              mov       [esi+12h],bp

LAB_004bb064:
  004bb064  f60601                test      byte [esi],1
  004bb067  0f85a5010000          jne       near 004BB212h
  004bb06d  885e43                mov       [esi+43h],bl
  004bb070  e99d010000            jmp       004BB212h
  004bb075  668b5612              mov       dx,[esi+12h]
  004bb079  bf01000000            mov       edi,1
  004bb07e  663bd7                cmp       dx,di
  004bb081  751b                  jne       short 004BB09Eh
  004bb083  0fbf4610              movsx     eax,word [esi+10h]
  004bb087  39442414              cmp       [esp+14h],eax
  004bb08b  7d05                  jge       short 004BB092h
  004bb08d  885e43                mov       [esi+43h],bl
  004bb090  eb0c                  jmp       short 004BB09Eh

LAB_004bb092:
  004bb092  8a4643                mov       al,[esi+43h]
  004bb095  3ac3                  cmp       al,bl
  004bb097  7405                  je        short 004BB09Eh
  004bb099  fec8                  dec       al
  004bb09b  884643                mov       [esi+43h],al

LAB_004bb09e:
  004bb09e  663bd3                cmp       dx,bx
  004bb0a1  0f856b010000          jne       near 004BB212h
  004bb0a7  66895e1a              mov       [esi+1Ah],bx
  004bb0ab  8b1e                  mov       ebx,[esi]
  004bb0ad  d1f9                  sar       ecx,1
  004bb0af  81cb00000001          or        ebx,1000000h
  004bb0b5  81c100400000          add       ecx,4000h
  004bb0bb  66897e18              mov       [esi+18h],di
  004bb0bf  891e                  mov       [esi],ebx
  004bb0c1  66897e12              mov       [esi+12h],di
  004bb0c5  894e5c                mov       [esi+5Ch],ecx
  004bb0c8  e945010000            jmp       004BB212h
  004bb0cd  668b4e12              mov       cx,[esi+12h]
  004bb0d1  bf01000000            mov       edi,1
  004bb0d6  663bcf                cmp       cx,di
  004bb0d9  7539                  jne       short 004BB114h
  004bb0db  8b465c                mov       eax,[esi+5Ch]
  004bb0de  66c746180200          mov       word [esi+18h],2
  004bb0e4  8bd0                  mov       edx,eax
  004bb0e6  66897e1a              mov       [esi+1Ah],di
  004bb0ea  c1fa03                sar       edx,3
  004bb0ed  2bc2                  sub       eax,edx
  004bb0ef  3d00500000            cmp       eax,5000h
  004bb0f4  89465c                mov       [esi+5Ch],eax
  004bb0f7  7d0c                  jge       short 004BB105h
  004bb0f9  885e43                mov       [esi+43h],bl
  004bb0fc  895e5c                mov       [esi+5Ch],ebx
  004bb0ff  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004bb105:
  004bb105  8b465c                mov       eax,[esi+5Ch]
  004bb108  c1f805                sar       eax,5
  004bb10b  0500100000            add       eax,1000h
  004bb110  6689462c              mov       [esi+2Ch],ax

LAB_004bb114:
  004bb114  663bcb                cmp       cx,bx
  004bb117  0f85f5000000          jne       near 004BB212h
  004bb11d  8b16                  mov       edx,[esi]
  004bb11f  66897e18              mov       [esi+18h],di
  004bb123  81e2fffffffe          and       edx,0FEFFFFFFh
  004bb129  66895e1a              mov       [esi+1Ah],bx
  004bb12d  8916                  mov       [esi],edx
  004bb12f  e80cc00800            call      00547140h
  004bb134  99                    cdq
  004bb135  b932000000            mov       ecx,32h
  004bb13a  66897e12              mov       [esi+12h],di
  004bb13e  f7f9                  idiv      ecx
  004bb140  8b442410              mov       eax,[esp+10h]
  004bb144  24fc                  and       al,0FCh
  004bb146  8d0c90                lea       ecx,[eax+edx*4]
  004bb149  c1e10a                shl       ecx,0Ah
  004bb14c  894e5c                mov       [esi+5Ch],ecx
  004bb14f  e9be000000            jmp       004BB212h
  004bb154  0fbf4610              movsx     eax,word [esi+10h]
  004bb158  668b4e32              mov       cx,[esi+32h]
  004bb15c  8bf8                  mov       edi,eax
  004bb15e  0fbfd1                movsx     edx,cx
  004bb161  2bfa                  sub       edi,edx
  004bb163  8bd0                  mov       edx,eax
  004bb165  c1ff03                sar       edi,3
  004bb168  03cf                  add       ecx,edi
  004bb16a  66895e18              mov       [esi+18h],bx
  004bb16e  66894e32              mov       [esi+32h],cx
  004bb172  0fbfc9                movsx     ecx,cx
  004bb175  2bd1                  sub       edx,ecx
  004bb177  7804                  js        short 004BB17Dh
  004bb179  8bca                  mov       ecx,edx
  004bb17b  eb02                  jmp       short 004BB17Fh

LAB_004bb17d:
  004bb17d  2bc8                  sub       ecx,eax

LAB_004bb17f:
  004bb17f  83f932                cmp       ecx,32h
  004bb182  7d5b                  jge       short 004BB1DFh
  004bb184  885e43                mov       [esi+43h],bl
  004bb187  895e5c                mov       [esi+5Ch],ebx
  004bb18a  e983000000            jmp       004BB212h
  004bb18f  bf01000000            mov       edi,1
  004bb194  66397e12              cmp       [esi+12h],di
  004bb198  7519                  jne       short 004BB1B3h
  004bb19a  66837e1a11            cmp       word [esi+1Ah],11h
  004bb19f  7512                  jne       short 004BB1B3h
  004bb1a1  6aff                  push      0FFFFFFFFh
  004bb1a3  53                    push      ebx
  004bb1a4  6a36                  push      36h
  004bb1a6  56                    push      esi
  004bb1a7  e824f8fbff            call      0047A9D0h
  004bb1ac  8b4c2420              mov       ecx,[esp+20h]
  004bb1b0  83c410                add       esp,10h

LAB_004bb1b3:
  004bb1b3  8b06                  mov       eax,[esi]
  004bb1b5  a900000002            test      eax,2000000h
  004bb1ba  7403                  je        short 004BB1BFh
  004bb1bc  885e43                mov       [esi+43h],bl

LAB_004bb1bf:
  004bb1bf  66395e12              cmp       [esi+12h],bx
  004bb1c3  754d                  jne       short 004BB212h
  004bb1c5  25fffffffe            and       eax,0FEFFFFFFh
  004bb1ca  83f91e                cmp       ecx,1Eh
  004bb1cd  66897e12              mov       [esi+12h],di
  004bb1d1  66c746180400          mov       word [esi+18h],4
  004bb1d7  66895e1a              mov       [esi+1Ah],bx
  004bb1db  8906                  mov       [esi],eax
  004bb1dd  7d33                  jge       short 004BB212h

LAB_004bb1df:
  004bb1df  895e5c                mov       [esi+5Ch],ebx
  004bb1e2  eb2e                  jmp       short 004BB212h
  004bb1e4  668b4e12              mov       cx,[esi+12h]
  004bb1e8  6683f901              cmp       cx,1
  004bb1ec  7510                  jne       short 004BB1FEh
  004bb1ee  8b06                  mov       eax,[esi]
  004bb1f0  a900000002            test      eax,2000000h
  004bb1f5  7407                  je        short 004BB1FEh
  004bb1f7  24fd                  and       al,0FDh
  004bb1f9  885e43                mov       [esi+43h],bl
  004bb1fc  8906                  mov       [esi],eax

LAB_004bb1fe:
  004bb1fe  663bcb                cmp       cx,bx
  004bb201  750f                  jne       short 004BB212h
  004bb203  8b06                  mov       eax,[esi]
  004bb205  66c746120100          mov       word [esi+12h],1
  004bb20b  25fffffffe            and       eax,0FEFFFFFFh
  004bb210  8906                  mov       [esi],eax

LAB_004bb212:
  004bb212  0fbf4632              movsx     eax,word [esi+32h]
  004bb216  50                    push      eax
  004bb217  e800a80800            call      00545A1Ch
  004bb21c  0fbf4e32              movsx     ecx,word [esi+32h]
  004bb220  51                    push      ecx
  004bb221  89442470              mov       [esp+70h],eax
  004bb225  e8eca70800            call      00545A16h
  004bb22a  8b5650                mov       edx,[esi+50h]
  004bb22d  8b4c241c              mov       ecx,[esp+1Ch]
  004bb231  0fbf9e9a000000        movsx     ebx,word [esi+9Ah]
  004bb238  0faf4e5c              imul      ecx,[esi+5Ch]
  004bb23c  89442474              mov       [esp+74h],eax
  004bb240  8bc2                  mov       eax,edx
  004bb242  c1f803                sar       eax,3
  004bb245  8bfa                  mov       edi,edx
  004bb247  56                    push      esi
  004bb248  2bf8                  sub       edi,eax
  004bb24a  8b4654                mov       eax,[esi+54h]
  004bb24d  c1fa08                sar       edx,8
  004bb250  2bfa                  sub       edi,edx
  004bb252  8be8                  mov       ebp,eax
  004bb254  8bd3                  mov       edx,ebx
  004bb256  897e50                mov       [esi+50h],edi
  004bb259  c1fd08                sar       ebp,8
  004bb25c  c1fa08                sar       edx,8
  004bb25f  2bd5                  sub       edx,ebp
  004bb261  8b6e58                mov       ebp,[esi+58h]
  004bb264  03d0                  add       edx,eax
  004bb266  8bc5                  mov       eax,ebp
  004bb268  2bd3                  sub       edx,ebx
  004bb26a  8bdd                  mov       ebx,ebp
  004bb26c  c1fb08                sar       ebx,8
  004bb26f  c1f90c                sar       ecx,0Ch
  004bb272  2bc3                  sub       eax,ebx
  004bb274  8bd9                  mov       ebx,ecx
  004bb276  0faf5c2470            imul      ebx,[esp+70h]
  004bb27b  0faf4c2478            imul      ecx,[esp+78h]
  004bb280  c1fd03                sar       ebp,3
  004bb283  c1fb0c                sar       ebx,0Ch
  004bb286  2bc5                  sub       eax,ebp
  004bb288  8b6e1c                mov       ebp,[esi+1Ch]
  004bb28b  03df                  add       ebx,edi
  004bb28d  895654                mov       [esi+54h],edx
  004bb290  03eb                  add       ebp,ebx
  004bb292  8b5e20                mov       ebx,[esi+20h]
  004bb295  c1f90c                sar       ecx,0Ch
  004bb298  896e1c                mov       [esi+1Ch],ebp
  004bb29b  8b6e24                mov       ebp,[esi+24h]
  004bb29e  03c8                  add       ecx,eax
  004bb2a0  03da                  add       ebx,edx
  004bb2a2  03e9                  add       ebp,ecx
  004bb2a4  894658                mov       [esi+58h],eax
  004bb2a7  895e20                mov       [esi+20h],ebx
  004bb2aa  896e24                mov       [esi+24h],ebp
  004bb2ad  e88e860700            call      00533940h
  004bb2b2  8b9690000000          mov       edx,[esi+90h]
  004bb2b8  8b4660                mov       eax,[esi+60h]
  004bb2bb  8b4e20                mov       ecx,[esi+20h]
  004bb2be  83c404                add       esp,4
  004bb2c1  80e6fc                and       dh,0FCh
  004bb2c4  3bc8                  cmp       ecx,eax
  004bb2c6  899690000000          mov       [esi+90h],edx
  004bb2cc  7f2a                  jg        short 004BB2F8h
  004bb2ce  f6c680                test      dh,80h
  004bb2d1  7525                  jne       short 004BB2F8h
  004bb2d3  8b0e                  mov       ecx,[esi]
  004bb2d5  894620                mov       [esi+20h],eax
  004bb2d8  f6c101                test      cl,1
  004bb2db  740f                  je        short 004BB2ECh
  004bb2dd  83e1fe                and       ecx,0FFFFFFFEh
  004bb2e0  33db                  xor       ebx,ebx
  004bb2e2  890e                  mov       [esi],ecx
  004bb2e4  895e54                mov       [esi+54h],ebx
  004bb2e7  e935010000            jmp       004BB421h

LAB_004bb2ec:
  004bb2ec  83e1fe                and       ecx,0FFFFFFFEh
  004bb2ef  33db                  xor       ebx,ebx
  004bb2f1  890e                  mov       [esi],ecx
  004bb2f3  895e54                mov       [esi+54h],ebx
  004bb2f6  eb02                  jmp       short 004BB2FAh

LAB_004bb2f8:
  004bb2f8  33db                  xor       ebx,ebx

LAB_004bb2fa:
  004bb2fa  8b0e                  mov       ecx,[esi]
  004bb2fc  f6c101                test      cl,1
  004bb2ff  750a                  jne       short 004BB30Bh
  004bb301  394620                cmp       [esi+20h],eax
  004bb304  7e05                  jle       short 004BB30Bh
  004bb306  83c901                or        ecx,1
  004bb309  890e                  mov       [esi],ecx

LAB_004bb30b:
  004bb30b  f7c282ad0020          test      edx,2000AD82h
  004bb311  0f840a010000          je        near 004BB421h
  004bb317  f60601                test      byte [esi],1
  004bb31a  0f85d9000000          jne       near 004BB3F9h
  004bb320  8a4e42                mov       cl,[esi+42h]
  004bb323  b007                  mov       al,7
  004bb325  3ac8                  cmp       cl,al
  004bb327  7420                  je        short 004BB349h
  004bb329  884642                mov       [esi+42h],al
  004bb32c  e80fbe0800            call      00547140h
  004bb331  99                    cdq
  004bb332  b928000000            mov       ecx,28h
  004bb337  66895e12              mov       [esi+12h],bx
  004bb33b  f7f9                  idiv      ecx
  004bb33d  66c746105000          mov       word [esi+10h],50h
  004bb343  80c214                add       dl,14h
  004bb346  885643                mov       [esi+43h],dl

LAB_004bb349:
  004bb349  8b16                  mov       edx,[esi]
  004bb34b  8b442428              mov       eax,[esp+28h]
  004bb34f  8b4c242c              mov       ecx,[esp+2Ch]
  004bb353  83e2fd                and       edx,0FFFFFFFDh
  004bb356  8916                  mov       [esi],edx
  004bb358  8b542424              mov       edx,[esp+24h]
  004bb35c  894620                mov       [esi+20h],eax
  004bb35f  8b442458              mov       eax,[esp+58h]
  004bb363  89561c                mov       [esi+1Ch],edx
  004bb366  8b542454              mov       edx,[esp+54h]
  004bb36a  25ffff0000            and       eax,0FFFFh
  004bb36f  894e24                mov       [esi+24h],ecx
  004bb372  8b4c2450              mov       ecx,[esp+50h]
  004bb376  89567c                mov       [esi+7Ch],edx
  004bb379  8b54243c              mov       edx,[esp+3Ch]
  004bb37d  898690000000          mov       [esi+90h],eax
  004bb383  8b442444              mov       eax,[esp+44h]
  004bb387  898e84000000          mov       [esi+84h],ecx
  004bb38d  8b4c2440              mov       ecx,[esp+40h]
  004bb391  898680000000          mov       [esi+80h],eax
  004bb397  8b44244c              mov       eax,[esp+4Ch]
  004bb39b  899688000000          mov       [esi+88h],edx
  004bb3a1  8b542448              mov       edx,[esp+48h]
  004bb3a5  6882ad0020            push      2000AD82h
  004bb3aa  56                    push      esi
  004bb3ab  89868c000000          mov       [esi+8Ch],eax
  004bb3b1  894e64                mov       [esi+64h],ecx
  004bb3b4  895660                mov       [esi+60h],edx
  004bb3b7  e8a4aa0100            call      004D5E60h
  004bb3bc  83c408                add       esp,8
  004bb3bf  663bc3                cmp       ax,bx
  004bb3c2  7525                  jne       short 004BB3E9h
  004bb3c4  e877bd0800            call      00547140h
  004bb3c9  99                    cdq
  004bb3ca  33c2                  xor       eax,edx
  004bb3cc  8b4c2410              mov       ecx,[esp+10h]
  004bb3d0  2bc2                  sub       eax,edx
  004bb3d2  83c140                add       ecx,40h
  004bb3d5  83e001                and       eax,1
  004bb3d8  33c2                  xor       eax,edx
  004bb3da  2bc2                  sub       eax,edx
  004bb3dc  8d4400ff              lea       eax,[eax+eax-1]
  004bb3e0  0fafc8                imul      ecx,eax
  004bb3e3  66014e32              add       [esi+32h],cx
  004bb3e7  eb38                  jmp       short 004BB421h

LAB_004bb3e9:
  004bb3e9  8b542410              mov       edx,[esp+10h]
  004bb3ed  83c240                add       edx,40h
  004bb3f0  0fafd0                imul      edx,eax
  004bb3f3  66015632              add       [esi+32h],dx
  004bb3f7  eb28                  jmp       short 004BB421h

LAB_004bb3f9:
  004bb3f9  8b442424              mov       eax,[esp+24h]
  004bb3fd  8b4c242c              mov       ecx,[esp+2Ch]
  004bb401  895e5c                mov       [esi+5Ch],ebx
  004bb404  895e50                mov       [esi+50h],ebx
  004bb407  895e58                mov       [esi+58h],ebx
  004bb40a  89461c                mov       [esi+1Ch],eax
  004bb40d  894e24                mov       [esi+24h],ecx
  004bb410  885e42                mov       [esi+42h],bl
  004bb413  c6464328              mov       byte [esi+43h],28h
  004bb417  66895e12              mov       [esi+12h],bx
  004bb41b  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004bb421:
  004bb421  6aff                  push      0FFFFFFFFh
  004bb423  6aff                  push      0FFFFFFFFh
  004bb425  6aff                  push      0FFFFFFFFh
  004bb427  56                    push      esi
  004bb428  e8e3830000            call      004C3810h
  004bb42d  8b44242c              mov       eax,[esp+2Ch]
  004bb431  83c410                add       esp,10h
  004bb434  3bc3                  cmp       eax,ebx
  004bb436  7517                  jne       short 004BB44Fh
  004bb438  668b942482000000      mov       dx,[esp+82h]
  004bb440  66895632              mov       [esi+32h],dx
  004bb444  5f                    pop       edi
  004bb445  5e                    pop       esi
  004bb446  5d                    pop       ebp
  004bb447  5b                    pop       ebx
  004bb448  81c484000000          add       esp,84h
  004bb44e  c3                    ret

LAB_004bb44f:
  004bb44f  385e43                cmp       [esi+43h],bl
  004bb452  0f8570030000          jne       near 004BB7C8h
  004bb458  8b466c                mov       eax,[esi+6Ch]
  004bb45b  8b6e1c                mov       ebp,[esi+1Ch]
  004bb45e  8b4e24                mov       ecx,[esi+24h]
  004bb461  8b781c                mov       edi,[eax+1Ch]
  004bb464  2bfd                  sub       edi,ebp
  004bb466  8b6824                mov       ebp,[eax+24h]
  004bb469  2be9                  sub       ebp,ecx
  004bb46b  c1ff0c                sar       edi,0Ch
  004bb46e  c1fd0c                sar       ebp,0Ch
  004bb471  8bc7                  mov       eax,edi
  004bb473  8bcd                  mov       ecx,ebp
  004bb475  0fafc7                imul      eax,edi
  004bb478  0fafcd                imul      ecx,ebp
  004bb47b  03c1                  add       eax,ecx
  004bb47d  50                    push      eax
  004bb47e  e88da50800            call      00545A10h
  004bb483  55                    push      ebp
  004bb484  57                    push      edi
  004bb485  8944241c              mov       [esp+1Ch],eax
  004bb489  e894a50800            call      00545A22h
  004bb48e  668b5632              mov       dx,[esi+32h]
  004bb492  66895e12              mov       [esi+12h],bx
  004bb496  2bc2                  sub       eax,edx
  004bb498  25ff0f0000            and       eax,0FFFh
  004bb49d  89442418              mov       [esp+18h],eax
  004bb4a1  8b442414              mov       eax,[esp+14h]
  004bb4a5  3dfa000000            cmp       eax,0FAh
  004bb4aa  0f8dcf000000          jge       near 004BB57Fh
  004bb4b0  e88bbc0800            call      00547140h
  004bb4b5  99                    cdq
  004bb4b6  b964000000            mov       ecx,64h
  004bb4bb  f7f9                  idiv      ecx
  004bb4bd  83fa32                cmp       edx,32h
  004bb4c0  7d64                  jge       short 004BB526h
  004bb4c2  8b542418              mov       edx,[esp+18h]
  004bb4c6  8b7c2420              mov       edi,[esp+20h]
  004bb4ca  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004bb4d1  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004bb4d8  25ff0f0000            and       eax,0FFFh
  004bb4dd  3bc1                  cmp       eax,ecx
  004bb4df  7f3b                  jg        short 004BB51Ch
  004bb4e1  e85abc0800            call      00547140h
  004bb4e6  99                    cdq
  004bb4e7  8d4c3f01              lea       ecx,[edi+edi+1]
  004bb4eb  f7f9                  idiv      ecx
  004bb4ed  8b442414              mov       eax,[esp+14h]
  004bb4f1  83c264                add       edx,64h
  004bb4f4  3bc2                  cmp       eax,edx
  004bb4f6  7d24                  jge       short 004BB51Ch
  004bb4f8  8b16                  mov       edx,[esi]
  004bb4fa  c646420b              mov       byte [esi+42h],0Bh
  004bb4fe  81e2fffffffe          and       edx,0FEFFFFFFh
  004bb504  c6464301              mov       byte [esi+43h],1
  004bb508  83ca02                or        edx,2
  004bb50b  895e5c                mov       [esi+5Ch],ebx
  004bb50e  8916                  mov       [esi],edx
  004bb510  66c746180300          mov       word [esi+18h],3
  004bb516  66895e1a              mov       [esi+1Ah],bx
  004bb51a  eb67                  jmp       short 004BB583h

LAB_004bb51c:
  004bb51c  c6464201              mov       byte [esi+42h],1
  004bb520  c6464301              mov       byte [esi+43h],1
  004bb524  eb5d                  jmp       short 004BB583h

LAB_004bb526:
  004bb526  83fa3c                cmp       edx,3Ch
  004bb529  7c2b                  jl        short 004BB556h
  004bb52b  83fa46                cmp       edx,46h
  004bb52e  7d26                  jge       short 004BB556h
  004bb530  8b4c2410              mov       ecx,[esp+10h]
  004bb534  8b442414              mov       eax,[esp+14h]
  004bb538  83c164                add       ecx,64h
  004bb53b  3bc1                  cmp       eax,ecx
  004bb53d  7e17                  jle       short 004BB556h
  004bb53f  c6464206              mov       byte [esi+42h],6
  004bb543  c6464301              mov       byte [esi+43h],1
  004bb547  e8f4bb0800            call      00547140h
  004bb54c  66894610              mov       [esi+10h],ax
  004bb550  66895e12              mov       [esi+12h],bx
  004bb554  eb29                  jmp       short 004BB57Fh

LAB_004bb556:
  004bb556  c6464202              mov       byte [esi+42h],2
  004bb55a  e8e1bb0800            call      00547140h
  004bb55f  99                    cdq
  004bb560  b90a000000            mov       ecx,0Ah
  004bb565  8b7c2420              mov       edi,[esp+20h]
  004bb569  f7f9                  idiv      ecx
  004bb56b  8bc7                  mov       eax,edi
  004bb56d  66c746105000          mov       word [esi+10h],50h
  004bb573  d1f8                  sar       eax,1
  004bb575  02d0                  add       dl,al
  004bb577  80c214                add       dl,14h
  004bb57a  885643                mov       [esi+43h],dl
  004bb57d  eb04                  jmp       short 004BB583h

LAB_004bb57f:
  004bb57f  8b7c2420              mov       edi,[esp+20h]

LAB_004bb583:
  004bb583  8b442414              mov       eax,[esp+14h]
  004bb587  3d58020000            cmp       eax,258h
  004bb58c  0f8dfa000000          jge       near 004BB68Ch
  004bb592  3dfa000000            cmp       eax,0FAh
  004bb597  0f8cef000000          jl        near 004BB68Ch
  004bb59d  e89ebb0800            call      00547140h
  004bb5a2  99                    cdq
  004bb5a3  b964000000            mov       ecx,64h
  004bb5a8  f7f9                  idiv      ecx
  004bb5aa  83fa14                cmp       edx,14h
  004bb5ad  7d50                  jge       short 004BB5FFh
  004bb5af  8b542418              mov       edx,[esp+18h]
  004bb5b3  81c200040000          add       edx,400h
  004bb5b9  81e2ff0f0000          and       edx,0FFFh
  004bb5bf  81fa00080000          cmp       edx,800h
  004bb5c5  7c0d                  jl        short 004BB5D4h
  004bb5c7  c6464201              mov       byte [esi+42h],1
  004bb5cb  c6464301              mov       byte [esi+43h],1
  004bb5cf  e9b8000000            jmp       004BB68Ch

LAB_004bb5d4:
  004bb5d4  885e42                mov       [esi+42h],bl
  004bb5d7  e864bb0800            call      00547140h
  004bb5dc  99                    cdq
  004bb5dd  b90a000000            mov       ecx,0Ah
  004bb5e2  f7f9                  idiv      ecx
  004bb5e4  8b4c2410              mov       ecx,[esp+10h]
  004bb5e8  8bc7                  mov       eax,edi
  004bb5ea  c1f802                sar       eax,2
  004bb5ed  02d0                  add       dl,al
  004bb5ef  885643                mov       [esi+43h],dl
  004bb5f2  8d540950              lea       edx,[ecx+ecx+50h]
  004bb5f6  66895610              mov       [esi+10h],dx
  004bb5fa  e989000000            jmp       004BB688h

LAB_004bb5ff:
  004bb5ff  83fa28                cmp       edx,28h
  004bb602  7d25                  jge       short 004BB629h
  004bb604  c6464202              mov       byte [esi+42h],2
  004bb608  e833bb0800            call      00547140h
  004bb60d  99                    cdq
  004bb60e  b90a000000            mov       ecx,0Ah
  004bb613  66c746105000          mov       word [esi+10h],50h
  004bb619  f7f9                  idiv      ecx
  004bb61b  8bc7                  mov       eax,edi
  004bb61d  d1f8                  sar       eax,1
  004bb61f  02d0                  add       dl,al
  004bb621  80c214                add       dl,14h
  004bb624  885643                mov       [esi+43h],dl
  004bb627  eb63                  jmp       short 004BB68Ch

LAB_004bb629:
  004bb629  83fa3c                cmp       edx,3Ch
  004bb62c  7d13                  jge       short 004BB641h
  004bb62e  c6464206              mov       byte [esi+42h],6
  004bb632  c6464301              mov       byte [esi+43h],1
  004bb636  e805bb0800            call      00547140h
  004bb63b  66894610              mov       [esi+10h],ax
  004bb63f  eb47                  jmp       short 004BB688h

LAB_004bb641:
  004bb641  83fa50                cmp       edx,50h
  004bb644  7d3a                  jge       short 004BB680h
  004bb646  c6464203              mov       byte [esi+42h],3
  004bb64a  66895e12              mov       [esi+12h],bx
  004bb64e  e8edba0800            call      00547140h
  004bb653  99                    cdq
  004bb654  b90a000000            mov       ecx,0Ah
  004bb659  f7f9                  idiv      ecx
  004bb65b  8bc7                  mov       eax,edi
  004bb65d  d1f8                  sar       eax,1
  004bb65f  02d0                  add       dl,al
  004bb661  80c214                add       dl,14h
  004bb664  885643                mov       [esi+43h],dl
  004bb667  e8d4ba0800            call      00547140h
  004bb66c  99                    cdq
  004bb66d  33c2                  xor       eax,edx
  004bb66f  2bc2                  sub       eax,edx
  004bb671  25ff0f0000            and       eax,0FFFh
  004bb676  33c2                  xor       eax,edx
  004bb678  2bc2                  sub       eax,edx
  004bb67a  66894610              mov       [esi+10h],ax
  004bb67e  eb0c                  jmp       short 004BB68Ch

LAB_004bb680:
  004bb680  c646420a              mov       byte [esi+42h],0Ah
  004bb684  c6464301              mov       byte [esi+43h],1

LAB_004bb688:
  004bb688  66895e12              mov       [esi+12h],bx

LAB_004bb68c:
  004bb68c  817c241458020000      cmp       dword [esp+14h],258h
  004bb694  0f8c2e010000          jl        near 004BB7C8h
  004bb69a  e8a1ba0800            call      00547140h
  004bb69f  bd64000000            mov       ebp,64h
  004bb6a4  99                    cdq
  004bb6a5  8bcd                  mov       ecx,ebp
  004bb6a7  f7f9                  idiv      ecx
  004bb6a9  83fa1e                cmp       edx,1Eh
  004bb6ac  7d45                  jge       short 004BB6F3h
  004bb6ae  8b442410              mov       eax,[esp+10h]
  004bb6b2  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004bb6b6  3bd1                  cmp       edx,ecx
  004bb6b8  0f8ccd000000          jl        near 004BB78Bh
  004bb6be  885e42                mov       [esi+42h],bl
  004bb6c1  e87aba0800            call      00547140h
  004bb6c6  99                    cdq
  004bb6c7  b90a000000            mov       ecx,0Ah
  004bb6cc  66895e12              mov       [esi+12h],bx
  004bb6d0  f7f9                  idiv      ecx
  004bb6d2  8b4c2410              mov       ecx,[esp+10h]
  004bb6d6  8bc7                  mov       eax,edi
  004bb6d8  c1f802                sar       eax,2
  004bb6db  02d0                  add       dl,al
  004bb6dd  885643                mov       [esi+43h],dl
  004bb6e0  8d540950              lea       edx,[ecx+ecx+50h]
  004bb6e4  66895610              mov       [esi+10h],dx
  004bb6e8  5f                    pop       edi
  004bb6e9  5e                    pop       esi
  004bb6ea  5d                    pop       ebp
  004bb6eb  5b                    pop       ebx
  004bb6ec  81c484000000          add       esp,84h
  004bb6f2  c3                    ret

LAB_004bb6f3:
  004bb6f3  83fa28                cmp       edx,28h
  004bb6f6  7d43                  jge       short 004BB73Bh
  004bb6f8  c6464203              mov       byte [esi+42h],3
  004bb6fc  66895e12              mov       [esi+12h],bx
  004bb700  e83bba0800            call      00547140h
  004bb705  99                    cdq
  004bb706  b90a000000            mov       ecx,0Ah
  004bb70b  f7f9                  idiv      ecx
  004bb70d  8bc7                  mov       eax,edi
  004bb70f  d1f8                  sar       eax,1
  004bb711  02d0                  add       dl,al
  004bb713  80c214                add       dl,14h
  004bb716  885643                mov       [esi+43h],dl
  004bb719  e822ba0800            call      00547140h
  004bb71e  99                    cdq
  004bb71f  33c2                  xor       eax,edx
  004bb721  2bc2                  sub       eax,edx
  004bb723  25ff0f0000            and       eax,0FFFh
  004bb728  33c2                  xor       eax,edx
  004bb72a  2bc2                  sub       eax,edx
  004bb72c  66894610              mov       [esi+10h],ax
  004bb730  5f                    pop       edi
  004bb731  5e                    pop       esi
  004bb732  5d                    pop       ebp
  004bb733  5b                    pop       ebx
  004bb734  81c484000000          add       esp,84h
  004bb73a  c3                    ret

LAB_004bb73b:
  004bb73b  83fa3c                cmp       edx,3Ch
  004bb73e  7d13                  jge       short 004BB753h
  004bb740  c6464201              mov       byte [esi+42h],1
  004bb744  c6464301              mov       byte [esi+43h],1
  004bb748  5f                    pop       edi
  004bb749  5e                    pop       esi
  004bb74a  5d                    pop       ebp
  004bb74b  5b                    pop       ebx
  004bb74c  81c484000000          add       esp,84h
  004bb752  c3                    ret

LAB_004bb753:
  004bb753  83fa50                cmp       edx,50h
  004bb756  7d5f                  jge       short 004BB7B7h
  004bb758  8b4c2418              mov       ecx,[esp+18h]
  004bb75c  8d04fd00020000        lea       eax,[edi*8+200h]
  004bb763  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004bb76a  81e2ff0f0000          and       edx,0FFFh
  004bb770  3bd0                  cmp       edx,eax
  004bb772  7f17                  jg        short 004BB78Bh
  004bb774  c646420a              mov       byte [esi+42h],0Ah
  004bb778  c6464301              mov       byte [esi+43h],1
  004bb77c  66895e12              mov       [esi+12h],bx
  004bb780  5f                    pop       edi
  004bb781  5e                    pop       esi
  004bb782  5d                    pop       ebp
  004bb783  5b                    pop       ebx
  004bb784  81c484000000          add       esp,84h
  004bb78a  c3                    ret

LAB_004bb78b:
  004bb78b  c6464202              mov       byte [esi+42h],2
  004bb78f  e8acb90800            call      00547140h
  004bb794  99                    cdq
  004bb795  b90a000000            mov       ecx,0Ah
  004bb79a  66896e10              mov       [esi+10h],bp
  004bb79e  f7f9                  idiv      ecx
  004bb7a0  8bc7                  mov       eax,edi
  004bb7a2  d1f8                  sar       eax,1
  004bb7a4  02d0                  add       dl,al
  004bb7a6  80c214                add       dl,14h
  004bb7a9  885643                mov       [esi+43h],dl
  004bb7ac  5f                    pop       edi
  004bb7ad  5e                    pop       esi
  004bb7ae  5d                    pop       ebp
  004bb7af  5b                    pop       ebx
  004bb7b0  81c484000000          add       esp,84h
  004bb7b6  c3                    ret

LAB_004bb7b7:
  004bb7b7  c6464206              mov       byte [esi+42h],6
  004bb7bb  c6464301              mov       byte [esi+43h],1
  004bb7bf  e87cb90800            call      00547140h
  004bb7c4  66894610              mov       [esi+10h],ax

LAB_004bb7c8:
  004bb7c8  5f                    pop       edi
  004bb7c9  5e                    pop       esi
  004bb7ca  5d                    pop       ebp
  004bb7cb  5b                    pop       ebx
  004bb7cc  81c484000000          add       esp,84h
  004bb7d2  c3                    ret

LAB_004bb7d3:
  004bb7d3  83e1fd                and       ecx,0FFFFFFFDh
  004bb7d6  890e                  mov       [esi],ecx
  004bb7d8  e9bef5ffff            jmp       004BAD9Bh

LAB_004bb7dd:
  004bb7dd  f6c101                test      cl,1
  004bb7e0  0f84b5f5ffff          je        near 004BAD9Bh
  004bb7e6  66837e1a03            cmp       word [esi+1Ah],3
  004bb7eb  0f8daaf5ffff          jge       near 004BAD9Bh
  004bb7f1  e84ab90800            call      00547140h
  004bb7f6  99                    cdq
  004bb7f7  b928000000            mov       ecx,28h
  004bb7fc  f7f9                  idiv      ecx
  004bb7fe  8b4c2410              mov       ecx,[esp+10h]
  004bb802  3bd1                  cmp       edx,ecx
  004bb804  7d20                  jge       short 004BB826h
  004bb806  8b16                  mov       edx,[esi]
  004bb808  8b44241c              mov       eax,[esp+1Ch]
  004bb80c  81e2fffffffe          and       edx,0FEFFFFFFh
  004bb812  66c746180300          mov       word [esi+18h],3
  004bb818  83ca02                or        edx,2
  004bb81b  66895e1a              mov       [esi+1Ah],bx
  004bb81f  8916                  mov       [esi],edx
  004bb821  e979f5ffff            jmp       004BAD9Fh

LAB_004bb826:
  004bb826  8b44241c              mov       eax,[esp+1Ch]
  004bb82a  e970f5ffff            jmp       004BAD9Fh
  004bb82f  90                    nop
  004bb830  b4ad                  mov       ah,0ADh
  004bb832  4b                    dec       ebx
  004bb833  00fe                  add       dh,bh
  004bb835  ad                    lodsd
  004bb836  4b                    dec       ebx
  004bb837  0073ae                add       [ebx-52h],dh
  004bb83a  4b                    dec       ebx
  004bb83b  0018                  add       [eax],bl
  004bb83d  af                    scasd
  004bb83e  4b                    dec       ebx
  004bb83f  0012                  add       [edx],dl
  004bb841  b24b                  mov       dl,4Bh
  004bb843  0012                  add       [edx],dl
  004bb845  b24b                  mov       dl,4Bh
  004bb847  00e3                  add       bl,ah
  004bb849  af                    scasd
  004bb84a  4b                    dec       ebx
  004bb84b  0075b0                add       [ebp-50h],dh
  004bb84e  4b                    dec       ebx
  004bb84f  00cd                  add       ch,cl
  004bb851  b04b                  mov       al,4Bh
  004bb853  0054b14b              add       [ecx+esi*4+4Bh],dl
  004bb857  008fb14b00e4          add       [edi-1BFFB44Fh],cl
  004bb85d  b14b                  mov       cl,4Bh
  004bb85f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
