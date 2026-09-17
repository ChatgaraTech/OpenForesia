Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004bfe90  section .text  offset fichier 0xbf290  3280 octets

  004bfe90  81ec88000000          sub       esp,88h
  004bfe96  53                    push      ebx
  004bfe97  55                    push      ebp
  004bfe98  56                    push      esi
  004bfe99  8bb42498000000        mov       esi,[esp+98h]
  004bfea0  33db                  xor       ebx,ebx
  004bfea2  57                    push      edi
  004bfea3  8a4641                mov       al,[esi+41h]
  004bfea6  66895c2424            mov       [esp+24h],bx
  004bfeab  3ac3                  cmp       al,bl
  004bfead  66c74424261e00        mov       word [esp+26h],1Eh
  004bfeb4  66c74424283200        mov       word [esp+28h],32h
  004bfebb  66c744242a6400        mov       word [esp+2Ah],64h
  004bfec2  66895c242c            mov       [esp+2Ch],bx
  004bfec7  66c744242e5000        mov       word [esp+2Eh],50h
  004bfece  7533                  jne       short 004BFF03h
  004bfed0  53                    push      ebx
  004bfed1  6a01                  push      1
  004bfed3  56                    push      esi
  004bfed4  c6464101              mov       byte [esi+41h],1
  004bfed8  e863dfffff            call      004BDE40h
  004bfedd  8b4620                mov       eax,[esi+20h]
  004bfee0  83c40c                add       esp,0Ch
  004bfee3  894648                mov       [esi+48h],eax
  004bfee6  668b4616              mov       ax,[esi+16h]
  004bfeea  663d0a00              cmp       ax,0Ah
  004bfeee  7c0d                  jl        short 004BFEFDh
  004bfef0  83c0f6                add       eax,0FFFFFFF6h
  004bfef3  66894616              mov       [esi+16h],ax
  004bfef7  8b06                  mov       eax,[esi]
  004bfef9  24df                  and       al,0DFh
  004bfefb  eb04                  jmp       short 004BFF01h

LAB_004bfefd:
  004bfefd  8b06                  mov       eax,[esi]
  004bfeff  0c20                  or        al,20h

LAB_004bff01:
  004bff01  8906                  mov       [esi],eax

LAB_004bff03:
  004bff03  8b4e6c                mov       ecx,[esi+6Ch]
  004bff06  8b561c                mov       edx,[esi+1Ch]
  004bff09  8b4620                mov       eax,[esi+20h]
  004bff0c  894c2458              mov       [esp+58h],ecx
  004bff10  8b0e                  mov       ecx,[esi]
  004bff12  8d7e1c                lea       edi,[esi+1Ch]
  004bff15  80cd10                or        ch,10h
  004bff18  89542448              mov       [esp+48h],edx
  004bff1c  8b5664                mov       edx,[esi+64h]
  004bff1f  890e                  mov       [esi],ecx
  004bff21  8b4e24                mov       ecx,[esi+24h]
  004bff24  8944244c              mov       [esp+4Ch],eax
  004bff28  8b4660                mov       eax,[esi+60h]
  004bff2b  894c2450              mov       [esp+50h],ecx
  004bff2f  668b4e32              mov       cx,[esi+32h]
  004bff33  8954243c              mov       [esp+3Ch],edx
  004bff37  8b567c                mov       edx,[esi+7Ch]
  004bff3a  89442434              mov       [esp+34h],eax
  004bff3e  668b8690000000        mov       ax,[esi+90h]
  004bff45  66898c2486000000      mov       [esp+86h],cx
  004bff4d  8b8e80000000          mov       ecx,[esi+80h]
  004bff53  8954245c              mov       [esp+5Ch],edx
  004bff57  8b9684000000          mov       edx,[esi+84h]
  004bff5d  6689442444            mov       [esp+44h],ax
  004bff62  8b8688000000          mov       eax,[esi+88h]
  004bff68  894c2454              mov       [esp+54h],ecx
  004bff6c  8b8e8c000000          mov       ecx,[esi+8Ch]
  004bff72  89542440              mov       [esp+40h],edx
  004bff76  89442430              mov       [esp+30h],eax
  004bff7a  8d542460              lea       edx,[esp+60h]
  004bff7e  894c2438              mov       [esp+38h],ecx
  004bff82  8d442464              lea       eax,[esp+64h]
  004bff86  52                    push      edx
  004bff87  8d4c246c              lea       ecx,[esp+6Ch]
  004bff8b  50                    push      eax
  004bff8c  51                    push      ecx
  004bff8d  56                    push      esi
  004bff8e  e8ed890700            call      00538980h
  004bff93  83c410                add       esp,10h
  004bff96  8d54246c              lea       edx,[esp+6Ch]
  004bff9a  8d442420              lea       eax,[esp+20h]
  004bff9e  8d4c2414              lea       ecx,[esp+14h]
  004bffa2  52                    push      edx
  004bffa3  50                    push      eax
  004bffa4  8d542420              lea       edx,[esp+20h]
  004bffa8  51                    push      ecx
  004bffa9  8d44241c              lea       eax,[esp+1Ch]
  004bffad  52                    push      edx
  004bffae  50                    push      eax
  004bffaf  56                    push      esi
  004bffb0  e86b5f0700            call      00535F20h
  004bffb5  8b4c2428              mov       ecx,[esp+28h]
  004bffb9  b840000000            mov       eax,40h
  004bffbe  2bc1                  sub       eax,ecx
  004bffc0  83c418                add       esp,18h
  004bffc3  8944241c              mov       [esp+1Ch],eax
  004bffc7  8b442420              mov       eax,[esp+20h]
  004bffcb  3bc3                  cmp       eax,ebx
  004bffcd  0f8407060000          je        near 004C05DAh
  004bffd3  8b2e                  mov       ebp,[esi]
  004bffd5  33d2                  xor       edx,edx
  004bffd7  8a5604                mov       dl,[esi+4]
  004bffda  81e5fffffffd          and       ebp,0FDFFFFFFh
  004bffe0  0fbf5e18              movsx     ebx,word [esi+18h]
  004bffe4  892e                  mov       [esi],ebp
  004bffe6  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004bffed  f7c500000001          test      ebp,1000000h
  004bfff3  8b149a                mov       edx,[edx+ebx*4]
  004bfff6  741d                  je        short 004C0015h
  004bfff8  66ff461a              inc       word [esi+1Ah]
  004bfffc  668b5e1a              mov       bx,[esi+1Ah]
  004c0000  33ed                  xor       ebp,ebp
  004c0002  668b6a16              mov       bp,[edx+16h]
  004c0006  0fbfd3                movsx     edx,bx
  004c0009  3bd5                  cmp       edx,ebp
  004c000b  7c2b                  jl        short 004C0038h
  004c000d  33db                  xor       ebx,ebx
  004c000f  66895e1a              mov       [esi+1Ah],bx
  004c0013  eb25                  jmp       short 004C003Ah

LAB_004c0015:
  004c0015  66ff461a              inc       word [esi+1Ah]
  004c0019  33db                  xor       ebx,ebx
  004c001b  668b5a16              mov       bx,[edx+16h]
  004c001f  0fbf561a              movsx     edx,word [esi+1Ah]
  004c0023  3bd3                  cmp       edx,ebx
  004c0025  7c11                  jl        short 004C0038h
  004c0027  668b561a              mov       dx,[esi+1Ah]
  004c002b  4a                    dec       edx
  004c002c  81cd00000002          or        ebp,2000000h
  004c0032  6689561a              mov       [esi+1Ah],dx
  004c0036  892e                  mov       [esi],ebp

LAB_004c0038:
  004c0038  33db                  xor       ebx,ebx

LAB_004c003a:
  004c003a  f60602                test      byte [esi],2
  004c003d  7465                  je        short 004C00A4h
  004c003f  66837e1a04            cmp       word [esi+1Ah],4
  004c0044  755e                  jne       short 004C00A4h
  004c0046  8d442424              lea       eax,[esp+24h]
  004c004a  53                    push      ebx
  004c004b  50                    push      eax
  004c004c  57                    push      edi
  004c004d  56                    push      esi
  004c004e  e8dd9b0500            call      00519C30h
  004c0053  8be8                  mov       ebp,eax
  004c0055  a1b8e05d00            mov       eax,[5DE0B8h]
  004c005a  83c410                add       esp,10h
  004c005d  3bc3                  cmp       eax,ebx
  004c005f  743b                  je        short 004C009Ch
  004c0061  66837e1a04            cmp       word [esi+1Ah],4
  004c0066  7534                  jne       short 004C009Ch
  004c0068  8b4d00                mov       ecx,[ebp]
  004c006b  55                    push      ebp
  004c006c  83c940                or        ecx,40h
  004c006f  56                    push      esi
  004c0070  66c785960000001000    mov       word [ebp+96h],10h
  004c0079  894d00                mov       [ebp],ecx
  004c007c  e8ff9a0700            call      00539B80h
  004c0081  83c408                add       esp,8
  004c0084  83c51c                add       ebp,1Ch
  004c0087  55                    push      ebp
  004c0088  6800180000            push      1800h
  004c008d  6800030000            push      300h
  004c0092  6a03                  push      3
  004c0094  e887da0700            call      0053DB20h
  004c0099  83c410                add       esp,10h

LAB_004c009c:
  004c009c  8b442420              mov       eax,[esp+20h]
  004c00a0  8b4c2410              mov       ecx,[esp+10h]

LAB_004c00a4:
  004c00a4  33d2                  xor       edx,edx
  004c00a6  8a5642                mov       dl,[esi+42h]
  004c00a9  83fa0a                cmp       edx,0Ah
  004c00ac  0f873f040000          ja        near 004C04F1h
  004c00b2  ff2495280b4c00        jmp       dword [edx*4+4C0B28h]
  004c00b9  66395e12              cmp       [esi+12h],bx
  004c00bd  751a                  jne       short 004C00D9h
  004c00bf  8b06                  mov       eax,[esi]
  004c00c1  66895e18              mov       [esi+18h],bx
  004c00c5  0d00000001            or        eax,1000000h
  004c00ca  66895e1a              mov       [esi+1Ah],bx
  004c00ce  8906                  mov       [esi],eax
  004c00d0  66c746120100          mov       word [esi+12h],1
  004c00d6  895e5c                mov       [esi+5Ch],ebx

LAB_004c00d9:
  004c00d9  0fbf4e10              movsx     ecx,word [esi+10h]
  004c00dd  394c2418              cmp       [esp+18h],ecx
  004c00e1  7d08                  jge       short 004C00EBh
  004c00e3  885e43                mov       [esi+43h],bl
  004c00e6  e906040000            jmp       004C04F1h

LAB_004c00eb:
  004c00eb  8a4643                mov       al,[esi+43h]
  004c00ee  3ac3                  cmp       al,bl
  004c00f0  0f84af020000          je        near 004C03A5h
  004c00f6  fec8                  dec       al
  004c00f8  895e5c                mov       [esi+5Ch],ebx
  004c00fb  884643                mov       [esi+43h],al
  004c00fe  e9ee030000            jmp       004C04F1h
  004c0103  66395e12              cmp       [esi+12h],bx
  004c0107  7517                  jne       short 004C0120h
  004c0109  8b06                  mov       eax,[esi]
  004c010b  66895e18              mov       [esi+18h],bx
  004c010f  0d00000001            or        eax,1000000h
  004c0114  66895e1a              mov       [esi+1Ah],bx
  004c0118  8906                  mov       [esi],eax
  004c011a  66c746120100          mov       word [esi+12h],1

LAB_004c0120:
  004c0120  8b442414              mov       eax,[esp+14h]
  004c0124  8d9090010000          lea       edx,[eax+190h]
  004c012a  81e2ff0f0000          and       edx,0FFFh
  004c0130  81fa20030000          cmp       edx,320h
  004c0136  0f8e66020000          jle       near 004C03A2h
  004c013c  25ff0f0000            and       eax,0FFFh
  004c0141  3d00080000            cmp       eax,800h
  004c0146  7d0f                  jge       short 004C0157h
  004c0148  8d4132                lea       eax,[ecx+32h]
  004c014b  895e5c                mov       [esi+5Ch],ebx
  004c014e  66014632              add       [esi+32h],ax
  004c0152  e99a030000            jmp       004C04F1h

LAB_004c0157:
  004c0157  b8ceffffff            mov       eax,0FFFFFFCEh
  004c015c  895e5c                mov       [esi+5Ch],ebx
  004c015f  2bc1                  sub       eax,ecx
  004c0161  66014632              add       [esi+32h],ax
  004c0165  e987030000            jmp       004C04F1h
  004c016a  66395e12              cmp       [esi+12h],bx
  004c016e  7526                  jne       short 004C0196h
  004c0170  8b2e                  mov       ebp,[esi]
  004c0172  8d04c9                lea       eax,[ecx+ecx*8]
  004c0175  c1e004                shl       eax,4
  004c0178  81cd00000001          or        ebp,1000000h
  004c017e  0500500000            add       eax,5000h
  004c0183  66895e18              mov       [esi+18h],bx
  004c0187  66895e1a              mov       [esi+1Ah],bx
  004c018b  892e                  mov       [esi],ebp
  004c018d  66c746120100          mov       word [esi+12h],1
  004c0193  89465c                mov       [esi+5Ch],eax

LAB_004c0196:
  004c0196  66837e1201            cmp       word [esi+12h],1
  004c019b  0f8550030000          jne       near 004C04F1h
  004c01a1  0fbf5610              movsx     edx,word [esi+10h]
  004c01a5  8b442418              mov       eax,[esp+18h]
  004c01a9  66895e18              mov       [esi+18h],bx
  004c01ad  3bc2                  cmp       eax,edx
  004c01af  66895e1a              mov       [esi+1Ah],bx
  004c01b3  7d06                  jge       short 004C01BBh
  004c01b5  885e43                mov       [esi+43h],bl
  004c01b8  895e5c                mov       [esi+5Ch],ebx

LAB_004c01bb:
  004c01bb  8a4643                mov       al,[esi+43h]
  004c01be  3ac3                  cmp       al,bl
  004c01c0  7605                  jbe       short 004C01C7h
  004c01c2  fec8                  dec       al
  004c01c4  884643                mov       [esi+43h],al

LAB_004c01c7:
  004c01c7  8b442414              mov       eax,[esp+14h]
  004c01cb  8d9000010000          lea       edx,[eax+100h]
  004c01d1  81e2ff0f0000          and       edx,0FFFh
  004c01d7  81fa00020000          cmp       edx,200h
  004c01dd  0f8e0e030000          jle       near 004C04F1h
  004c01e3  25ff0f0000            and       eax,0FFFh
  004c01e8  3d00080000            cmp       eax,800h
  004c01ed  7d10                  jge       short 004C01FFh
  004c01ef  8d8c0980000000        lea       ecx,[ecx+ecx+80h]
  004c01f6  66014e32              add       [esi+32h],cx
  004c01fa  e9f2020000            jmp       004C04F1h

LAB_004c01ff:
  004c01ff  8d0409                lea       eax,[ecx+ecx]
  004c0202  b980ffffff            mov       ecx,0FFFFFF80h
  004c0207  2bc8                  sub       ecx,eax
  004c0209  66014e32              add       [esi+32h],cx
  004c020d  e9df020000            jmp       004C04F1h
  004c0212  66837e1201            cmp       word [esi+12h],1
  004c0217  0f85bb000000          jne       near 004C02D8h
  004c021d  8a5643                mov       dl,[esi+43h]
  004c0220  66895e18              mov       [esi+18h],bx
  004c0224  3ad3                  cmp       dl,bl
  004c0226  7405                  je        short 004C022Dh
  004c0228  feca                  dec       dl
  004c022a  885643                mov       [esi+43h],dl

LAB_004c022d:
  004c022d  83c170                add       ecx,70h
  004c0230  c1e108                shl       ecx,8
  004c0233  0fafc1                imul      eax,ecx
  004c0236  894e5c                mov       [esi+5Ch],ecx
  004c0239  0fbf4e10              movsx     ecx,word [esi+10h]
  004c023d  c1f80c                sar       eax,0Ch
  004c0240  51                    push      ecx
  004c0241  8be8                  mov       ebp,eax
  004c0243  e8d4570800            call      00545A1Ch
  004c0248  0fafc5                imul      eax,ebp
  004c024b  0fbf5610              movsx     edx,word [esi+10h]
  004c024f  8b0f                  mov       ecx,[edi]
  004c0251  52                    push      edx
  004c0252  c1f80c                sar       eax,0Ch
  004c0255  03c8                  add       ecx,eax
  004c0257  890f                  mov       [edi],ecx
  004c0259  e8b8570800            call      00545A16h
  004c025e  0fafc5                imul      eax,ebp
  004c0261  8b6e24                mov       ebp,[esi+24h]
  004c0264  895e5c                mov       [esi+5Ch],ebx
  004c0267  c1f80c                sar       eax,0Ch
  004c026a  03e8                  add       ebp,eax
  004c026c  8b442414              mov       eax,[esp+14h]
  004c0270  896e24                mov       [esi+24h],ebp
  004c0273  8d8800010000          lea       ecx,[eax+100h]
  004c0279  81e1ff0f0000          and       ecx,0FFFh
  004c027f  81f900020000          cmp       ecx,200h
  004c0285  7e28                  jle       short 004C02AFh
  004c0287  25ff0f0000            and       eax,0FFFh
  004c028c  3d00080000            cmp       eax,800h
  004c0291  7d0a                  jge       short 004C029Dh
  004c0293  8b542410              mov       edx,[esp+10h]
  004c0297  8d441232              lea       eax,[edx+edx+32h]
  004c029b  eb0e                  jmp       short 004C02ABh

LAB_004c029d:
  004c029d  8b442410              mov       eax,[esp+10h]
  004c02a1  8d0c00                lea       ecx,[eax+eax]
  004c02a4  b8ceffffff            mov       eax,0FFFFFFCEh
  004c02a9  2bc1                  sub       eax,ecx

LAB_004c02ab:
  004c02ab  66014632              add       [esi+32h],ax

LAB_004c02af:
  004c02af  668b4614              mov       ax,[esi+14h]
  004c02b3  668bd0                mov       dx,ax
  004c02b6  66c1fa03              sar       dx,3
  004c02ba  2bc2                  sub       eax,edx
  004c02bc  663d0500              cmp       ax,5
  004c02c0  66894614              mov       [esi+14h],ax
  004c02c4  7d09                  jge       short 004C02CFh
  004c02c6  885e43                mov       [esi+43h],bl
  004c02c9  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004c02cf:
  004c02cf  0500100000            add       eax,1000h
  004c02d4  6689462c              mov       [esi+2Ch],ax

LAB_004c02d8:
  004c02d8  66395e12              cmp       [esi+12h],bx
  004c02dc  0f850f020000          jne       near 004C04F1h
  004c02e2  8b0e                  mov       ecx,[esi]
  004c02e4  66895e18              mov       [esi+18h],bx
  004c02e8  81c900000001          or        ecx,1000000h
  004c02ee  66895e1a              mov       [esi+1Ah],bx
  004c02f2  890e                  mov       [esi],ecx
  004c02f4  66c746120100          mov       word [esi+12h],1
  004c02fa  e8416e0800            call      00547140h
  004c02ff  99                    cdq
  004c0300  b914000000            mov       ecx,14h
  004c0305  f7f9                  idiv      ecx
  004c0307  8b442410              mov       eax,[esp+10h]
  004c030b  8d4c0205              lea       ecx,[edx+eax+5]
  004c030f  c1e104                shl       ecx,4
  004c0312  66894e14              mov       [esi+14h],cx
  004c0316  e9d6010000            jmp       004C04F1h
  004c031b  668b5612              mov       dx,[esi+12h]
  004c031f  6683fa01              cmp       dx,1
  004c0323  751b                  jne       short 004C0340h
  004c0325  0fbf4610              movsx     eax,word [esi+10h]
  004c0329  39442418              cmp       [esp+18h],eax
  004c032d  7d05                  jge       short 004C0334h
  004c032f  885e43                mov       [esi+43h],bl
  004c0332  eb0c                  jmp       short 004C0340h

LAB_004c0334:
  004c0334  8a4643                mov       al,[esi+43h]
  004c0337  3ac3                  cmp       al,bl
  004c0339  7405                  je        short 004C0340h
  004c033b  fec8                  dec       al
  004c033d  884643                mov       [esi+43h],al

LAB_004c0340:
  004c0340  663bd3                cmp       dx,bx
  004c0343  0f85a8010000          jne       near 004C04F1h
  004c0349  66895e18              mov       [esi+18h],bx
  004c034d  66895e1a              mov       [esi+1Ah],bx
  004c0351  8b1e                  mov       ebx,[esi]
  004c0353  83e1f8                and       ecx,0FFFFFFF8h
  004c0356  83c120                add       ecx,20h
  004c0359  81cb00000001          or        ebx,1000000h
  004c035f  c1e109                shl       ecx,9
  004c0362  891e                  mov       [esi],ebx
  004c0364  66c746120100          mov       word [esi+12h],1
  004c036a  894e5c                mov       [esi+5Ch],ecx
  004c036d  e97f010000            jmp       004C04F1h
  004c0372  0fbf4610              movsx     eax,word [esi+10h]
  004c0376  668b4e32              mov       cx,[esi+32h]
  004c037a  8bf8                  mov       edi,eax
  004c037c  0fbfd1                movsx     edx,cx
  004c037f  2bfa                  sub       edi,edx
  004c0381  8bd0                  mov       edx,eax
  004c0383  c1ff03                sar       edi,3
  004c0386  03cf                  add       ecx,edi
  004c0388  66895e18              mov       [esi+18h],bx
  004c038c  66894e32              mov       [esi+32h],cx
  004c0390  0fbfc9                movsx     ecx,cx
  004c0393  2bd1                  sub       edx,ecx
  004c0395  7804                  js        short 004C039Bh
  004c0397  8bca                  mov       ecx,edx
  004c0399  eb02                  jmp       short 004C039Dh

LAB_004c039b:
  004c039b  2bc8                  sub       ecx,eax

LAB_004c039d:
  004c039d  83f932                cmp       ecx,32h
  004c03a0  7d03                  jge       short 004C03A5h

LAB_004c03a2:
  004c03a2  885e43                mov       [esi+43h],bl

LAB_004c03a5:
  004c03a5  895e5c                mov       [esi+5Ch],ebx
  004c03a8  e944010000            jmp       004C04F1h
  004c03ad  66395e12              cmp       [esi+12h],bx
  004c03b1  0f85d5000000          jne       near 004C048Ch
  004c03b7  33ff                  xor       edi,edi
  004c03b9  b800f45f00            mov       eax,5FF400h

LAB_004c03be:
  004c03be  39706c                cmp       [eax+6Ch],esi
  004c03c1  750c                  jne       short 004C03CFh
  004c03c3  f70000000080          test      dword [eax],80000000h
  004c03c9  0f854f070000          jne       near 004C0B1Eh

LAB_004c03cf:
  004c03cf  059c000000            add       eax,9Ch
  004c03d4  47                    inc       edi
  004c03d5  3d80076000            cmp       eax,600780h
  004c03da  7ce2                  jl        short 004C03BEh
  004c03dc  8b06                  mov       eax,[esi]
  004c03de  a904080000            test      eax,804h
  004c03e3  740d                  je        short 004C03F2h
  004c03e5  885e43                mov       [esi+43h],bl
  004c03e8  25fbf7ffff            and       eax,0FFFFF7FBh
  004c03ed  e9fd000000            jmp       004C04EFh

LAB_004c03f2:
  004c03f2  6aff                  push      0FFFFFFFFh
  004c03f4  53                    push      ebx
  004c03f5  68bd000000            push      0BDh
  004c03fa  56                    push      esi
  004c03fb  e8e0a6fbff            call      0047AAE0h
  004c0400  83c410                add       esp,10h
  004c0403  66395e16              cmp       [esi+16h],bx
  004c0407  bd01000000            mov       ebp,1
  004c040c  66896e12              mov       [esi+12h],bp
  004c0410  7512                  jne       short 004C0424h
  004c0412  e8296d0800            call      00547140h
  004c0417  99                    cdq
  004c0418  33c2                  xor       eax,edx
  004c041a  2bc2                  sub       eax,edx
  004c041c  23c5                  and       eax,ebp
  004c041e  33c2                  xor       eax,edx
  004c0420  8bf8                  mov       edi,eax
  004c0422  2bfa                  sub       edi,edx

LAB_004c0424:
  004c0424  66396e16              cmp       [esi+16h],bp
  004c0428  750f                  jne       short 004C0439h
  004c042a  e8116d0800            call      00547140h
  004c042f  99                    cdq
  004c0430  b911000000            mov       ecx,11h
  004c0435  f7f9                  idiv      ecx
  004c0437  8bfa                  mov       edi,edx

LAB_004c0439:
  004c0439  33c0                  xor       eax,eax
  004c043b  668b047d602b5500      mov       ax,[edi*2+552B60h]
  004c0443  66894606              mov       [esi+6],ax
  004c0447  8b0dc4975e00          mov       ecx,[5E97C4h]
  004c044d  8bd0                  mov       edx,eax
  004c044f  25ff000000            and       eax,0FFh
  004c0454  8b899c000000          mov       ecx,[ecx+9Ch]
  004c045a  c1fa08                sar       edx,8
  004c045d  81e2ff000000          and       edx,0FFh
  004c0463  8d0440                lea       eax,[eax+eax*2]
  004c0466  c1e004                shl       eax,4
  004c0469  8b0c91                mov       ecx,[ecx+edx*4]
  004c046c  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  004c0472  8b06                  mov       eax,[esi]
  004c0474  66895610              mov       [esi+10h],dx
  004c0478  0d04080001            or        eax,1000804h
  004c047d  895e5c                mov       [esi+5Ch],ebx
  004c0480  66895e18              mov       [esi+18h],bx
  004c0484  66895e1a              mov       [esi+1Ah],bx
  004c0488  8906                  mov       [esi],eax
  004c048a  eb05                  jmp       short 004C0491h

LAB_004c048c:
  004c048c  bd01000000            mov       ebp,1

LAB_004c0491:
  004c0491  66396e12              cmp       [esi+12h],bp
  004c0495  7510                  jne       short 004C04A7h
  004c0497  66ff4e10              dec       word [esi+10h]
  004c049b  66396e10              cmp       [esi+10h],bp
  004c049f  7d06                  jge       short 004C04A7h
  004c04a1  66c746120200          mov       word [esi+12h],2

LAB_004c04a7:
  004c04a7  66837e1202            cmp       word [esi+12h],2
  004c04ac  7532                  jne       short 004C04E0h
  004c04ae  8b16                  mov       edx,[esi]
  004c04b0  33c0                  xor       eax,eax
  004c04b2  668b4606              mov       ax,[esi+6]
  004c04b6  6aff                  push      0FFFFFFFFh
  004c04b8  8bc8                  mov       ecx,eax
  004c04ba  6aff                  push      0FFFFFFFFh
  004c04bc  c1f808                sar       eax,8
  004c04bf  81e1ff000000          and       ecx,0FFh
  004c04c5  25ff000000            and       eax,0FFh
  004c04ca  51                    push      ecx
  004c04cb  83e2fb                and       edx,0FFFFFFFBh
  004c04ce  50                    push      eax
  004c04cf  56                    push      esi
  004c04d0  8916                  mov       [esi],edx
  004c04d2  e8a9a40700            call      0053A980h
  004c04d7  83c414                add       esp,14h
  004c04da  66c746120300          mov       word [esi+12h],3

LAB_004c04e0:
  004c04e0  66837e1203            cmp       word [esi+12h],3
  004c04e5  750a                  jne       short 004C04F1h
  004c04e7  8b06                  mov       eax,[esi]
  004c04e9  885e43                mov       [esi+43h],bl
  004c04ec  80e4f7                and       ah,0F7h

LAB_004c04ef:
  004c04ef  8906                  mov       [esi],eax

LAB_004c04f1:
  004c04f1  8b0e                  mov       ecx,[esi]
  004c04f3  8a4642                mov       al,[esi+42h]
  004c04f6  f6c120                test      cl,20h
  004c04f9  0f8488000000          je        near 004C0587h
  004c04ff  3c0a                  cmp       al,0Ah
  004c0501  7524                  jne       short 004C0527h
  004c0503  0fbf5614              movsx     edx,word [esi+14h]
  004c0507  8b7e20                mov       edi,[esi+20h]
  004c050a  52                    push      edx
  004c050b  e80c550800            call      00545A1Ch
  004c0510  8b4e48                mov       ecx,[esi+48h]
  004c0513  8d0440                lea       eax,[eax+eax*2]
  004c0516  8d0480                lea       eax,[eax+eax*4]
  004c0519  c1e002                shl       eax,2
  004c051c  2bc7                  sub       eax,edi
  004c051e  8d940800c00800        lea       edx,[eax+ecx+8C000h]
  004c0525  eb3f                  jmp       short 004C0566h

LAB_004c0527:
  004c0527  e8146c0800            call      00547140h
  004c052c  99                    cdq
  004c052d  b932000000            mov       ecx,32h
  004c0532  8b7e20                mov       edi,[esi+20h]
  004c0535  f7f9                  idiv      ecx
  004c0537  66035614              add       dx,[esi+14h]
  004c053b  83c264                add       edx,64h
  004c053e  81e2ff0f0000          and       edx,0FFFh
  004c0544  66895614              mov       [esi+14h],dx
  004c0548  0fbfd2                movsx     edx,dx
  004c054b  52                    push      edx
  004c054c  e8cb540800            call      00545A1Ch
  004c0551  8b4e48                mov       ecx,[esi+48h]
  004c0554  8d0440                lea       eax,[eax+eax*2]
  004c0557  8d0480                lea       eax,[eax+eax*4]
  004c055a  c1e002                shl       eax,2
  004c055d  2bc7                  sub       eax,edi
  004c055f  8d940800000a00        lea       edx,[eax+ecx+0A0000h]

LAB_004c0566:
  004c0566  8b4c2458              mov       ecx,[esp+58h]
  004c056a  c1fa03                sar       edx,3
  004c056d  03d7                  add       edx,edi
  004c056f  895620                mov       [esi+20h],edx
  004c0572  8b4164                mov       eax,[ecx+64h]
  004c0575  8b4920                mov       ecx,[ecx+20h]
  004c0578  33c8                  xor       ecx,eax
  004c057a  f7c100f0ffff          test      ecx,0FFFFF000h
  004c0580  7558                  jne       short 004C05DAh
  004c0582  894648                mov       [esi+48h],eax
  004c0585  eb53                  jmp       short 004C05DAh

LAB_004c0587:
  004c0587  3c0c                  cmp       al,0Ch
  004c0589  7404                  je        short 004C058Fh
  004c058b  3c0a                  cmp       al,0Ah
  004c058d  7505                  jne       short 004C0594h

LAB_004c058f:
  004c058f  f6c102                test      cl,2
  004c0592  7446                  je        short 004C05DAh

LAB_004c0594:
  004c0594  e8a76b0800            call      00547140h
  004c0599  99                    cdq
  004c059a  b932000000            mov       ecx,32h
  004c059f  8b7e20                mov       edi,[esi+20h]
  004c05a2  f7f9                  idiv      ecx
  004c05a4  66035614              add       dx,[esi+14h]
  004c05a8  83c264                add       edx,64h
  004c05ab  81e2ff0f0000          and       edx,0FFFh
  004c05b1  66895614              mov       [esi+14h],dx
  004c05b5  0fbfd2                movsx     edx,dx
  004c05b8  52                    push      edx
  004c05b9  e85e540800            call      00545A1Ch
  004c05be  8b4e60                mov       ecx,[esi+60h]
  004c05c1  8d0440                lea       eax,[eax+eax*2]
  004c05c4  8d0480                lea       eax,[eax+eax*4]
  004c05c7  8d1481                lea       edx,[ecx+eax*4]
  004c05ca  2bd7                  sub       edx,edi
  004c05cc  81c200c00800          add       edx,8C000h
  004c05d2  c1fa03                sar       edx,3
  004c05d5  03d7                  add       edx,edi
  004c05d7  895620                mov       [esi+20h],edx

LAB_004c05da:
  004c05da  0fbf4614              movsx     eax,word [esi+14h]
  004c05de  50                    push      eax
  004c05df  e838540800            call      00545A1Ch
  004c05e4  0fbf4e14              movsx     ecx,word [esi+14h]
  004c05e8  c1f806                sar       eax,6
  004c05eb  0464                  add       al,64h
  004c05ed  51                    push      ecx
  004c05ee  884638                mov       [esi+38h],al
  004c05f1  e826540800            call      00545A1Ch
  004c05f6  0fbf5614              movsx     edx,word [esi+14h]
  004c05fa  c1f806                sar       eax,6
  004c05fd  0464                  add       al,64h
  004c05ff  52                    push      edx
  004c0600  884639                mov       [esi+39h],al
  004c0603  e814540800            call      00545A1Ch
  004c0608  c1f806                sar       eax,6
  004c060b  0464                  add       al,64h
  004c060d  88463a                mov       [esi+3Ah],al
  004c0610  0fbf4632              movsx     eax,word [esi+32h]
  004c0614  50                    push      eax
  004c0615  e802540800            call      00545A1Ch
  004c061a  0fbf4e32              movsx     ecx,word [esi+32h]
  004c061e  51                    push      ecx
  004c061f  89442474              mov       [esp+74h],eax
  004c0623  e8ee530800            call      00545A16h
  004c0628  8b5650                mov       edx,[esi+50h]
  004c062b  89442478              mov       [esp+78h],eax
  004c062f  8bc2                  mov       eax,edx
  004c0631  8b4e5c                mov       ecx,[esi+5Ch]
  004c0634  c1f808                sar       eax,8
  004c0637  0faf4c2420            imul      ecx,[esp+20h]
  004c063c  8bfa                  mov       edi,edx
  004c063e  56                    push      esi
  004c063f  2bf8                  sub       edi,eax
  004c0641  c1fa03                sar       edx,3
  004c0644  2bfa                  sub       edi,edx
  004c0646  8b5654                mov       edx,[esi+54h]
  004c0649  8bc2                  mov       eax,edx
  004c064b  8bea                  mov       ebp,edx
  004c064d  c1f808                sar       eax,8
  004c0650  2be8                  sub       ebp,eax
  004c0652  897e50                mov       [esi+50h],edi
  004c0655  c1fa03                sar       edx,3
  004c0658  2bea                  sub       ebp,edx
  004c065a  8b5658                mov       edx,[esi+58h]
  004c065d  8bda                  mov       ebx,edx
  004c065f  8bc2                  mov       eax,edx
  004c0661  c1fb08                sar       ebx,8
  004c0664  2bc3                  sub       eax,ebx
  004c0666  8b5e20                mov       ebx,[esi+20h]
  004c0669  c1fa03                sar       edx,3
  004c066c  c1f90c                sar       ecx,0Ch
  004c066f  2bc2                  sub       eax,edx
  004c0671  8bd1                  mov       edx,ecx
  004c0673  0faf542474            imul      edx,[esp+74h]
  004c0678  0faf4c247c            imul      ecx,[esp+7Ch]
  004c067d  c1fa0c                sar       edx,0Ch
  004c0680  c1f90c                sar       ecx,0Ch
  004c0683  896e54                mov       [esi+54h],ebp
  004c0686  03d7                  add       edx,edi
  004c0688  8b7e1c                mov       edi,[esi+1Ch]
  004c068b  03dd                  add       ebx,ebp
  004c068d  8b6e24                mov       ebp,[esi+24h]
  004c0690  03c8                  add       ecx,eax
  004c0692  03fa                  add       edi,edx
  004c0694  03e9                  add       ebp,ecx
  004c0696  894658                mov       [esi+58h],eax
  004c0699  897e1c                mov       [esi+1Ch],edi
  004c069c  895e20                mov       [esi+20h],ebx
  004c069f  896e24                mov       [esi+24h],ebp
  004c06a2  e899320700            call      00533940h
  004c06a7  8b9690000000          mov       edx,[esi+90h]
  004c06ad  8a0e                  mov       cl,[esi]
  004c06af  83c404                add       esp,4
  004c06b2  80e6f8                and       dh,0F8h
  004c06b5  f6c120                test      cl,20h
  004c06b8  899690000000          mov       [esi+90h],edx
  004c06be  8bc2                  mov       eax,edx
  004c06c0  740b                  je        short 004C06CDh
  004c06c2  25ffdfffdf            and       eax,0DFFFDFFFh
  004c06c7  898690000000          mov       [esi+90h],eax

LAB_004c06cd:
  004c06cd  8b8690000000          mov       eax,[esi+90h]
  004c06d3  33db                  xor       ebx,ebx
  004c06d5  a982a80020            test      eax,2000A882h
  004c06da  895e54                mov       [esi+54h],ebx
  004c06dd  0f84cd000000          je        near 004C07B0h
  004c06e3  8a4e42                mov       cl,[esi+42h]
  004c06e6  b007                  mov       al,7
  004c06e8  3ac8                  cmp       cl,al
  004c06ea  66c7462c0010          mov       word [esi+2Ch],1000h
  004c06f0  7420                  je        short 004C0712h
  004c06f2  884642                mov       [esi+42h],al
  004c06f5  e8466a0800            call      00547140h
  004c06fa  99                    cdq
  004c06fb  b928000000            mov       ecx,28h
  004c0700  66895e12              mov       [esi+12h],bx
  004c0704  f7f9                  idiv      ecx
  004c0706  66c746105000          mov       word [esi+10h],50h
  004c070c  80c214                add       dl,14h
  004c070f  885643                mov       [esi+43h],dl

LAB_004c0712:
  004c0712  8b16                  mov       edx,[esi]
  004c0714  8b44244c              mov       eax,[esp+4Ch]
  004c0718  8b4c2450              mov       ecx,[esp+50h]
  004c071c  83e2fd                and       edx,0FFFFFFFDh
  004c071f  8916                  mov       [esi],edx
  004c0721  8b542448              mov       edx,[esp+48h]
  004c0725  894620                mov       [esi+20h],eax
  004c0728  8b442444              mov       eax,[esp+44h]
  004c072c  89561c                mov       [esi+1Ch],edx
  004c072f  8b54245c              mov       edx,[esp+5Ch]
  004c0733  25ffff0000            and       eax,0FFFFh
  004c0738  894e24                mov       [esi+24h],ecx
  004c073b  8b4c2440              mov       ecx,[esp+40h]
  004c073f  89567c                mov       [esi+7Ch],edx
  004c0742  8b542430              mov       edx,[esp+30h]
  004c0746  898690000000          mov       [esi+90h],eax
  004c074c  8b442454              mov       eax,[esp+54h]
  004c0750  898e84000000          mov       [esi+84h],ecx
  004c0756  8b4c243c              mov       ecx,[esp+3Ch]
  004c075a  898680000000          mov       [esi+80h],eax
  004c0760  8b442438              mov       eax,[esp+38h]
  004c0764  899688000000          mov       [esi+88h],edx
  004c076a  8b542434              mov       edx,[esp+34h]
  004c076e  6880a80020            push      2000A880h
  004c0773  56                    push      esi
  004c0774  89868c000000          mov       [esi+8Ch],eax
  004c077a  894e64                mov       [esi+64h],ecx
  004c077d  895660                mov       [esi+60h],edx
  004c0780  e8db560100            call      004D5E60h
  004c0785  83c408                add       esp,8
  004c0788  663bc3                cmp       ax,bx
  004c078b  7515                  jne       short 004C07A2h
  004c078d  e8ae690800            call      00547140h
  004c0792  99                    cdq
  004c0793  33c2                  xor       eax,edx
  004c0795  2bc2                  sub       eax,edx
  004c0797  83e001                and       eax,1
  004c079a  33c2                  xor       eax,edx
  004c079c  2bc2                  sub       eax,edx
  004c079e  8d4400ff              lea       eax,[eax+eax-1]

LAB_004c07a2:
  004c07a2  8b4c2410              mov       ecx,[esp+10h]
  004c07a6  83c140                add       ecx,40h
  004c07a9  0fafc8                imul      ecx,eax
  004c07ac  66014e32              add       [esi+32h],cx

LAB_004c07b0:
  004c07b0  6aff                  push      0FFFFFFFFh
  004c07b2  6aff                  push      0FFFFFFFFh
  004c07b4  6aff                  push      0FFFFFFFFh
  004c07b6  56                    push      esi
  004c07b7  e854300000            call      004C3810h
  004c07bc  8b442430              mov       eax,[esp+30h]
  004c07c0  83c410                add       esp,10h
  004c07c3  3bc3                  cmp       eax,ebx
  004c07c5  7517                  jne       short 004C07DEh
  004c07c7  668b942486000000      mov       dx,[esp+86h]
  004c07cf  66895632              mov       [esi+32h],dx
  004c07d3  5f                    pop       edi
  004c07d4  5e                    pop       esi
  004c07d5  5d                    pop       ebp
  004c07d6  5b                    pop       ebx
  004c07d7  81c488000000          add       esp,88h
  004c07dd  c3                    ret

LAB_004c07de:
  004c07de  385e43                cmp       [esi+43h],bl
  004c07e1  0f852c030000          jne       near 004C0B13h
  004c07e7  8b466c                mov       eax,[esi+6Ch]
  004c07ea  8b6e1c                mov       ebp,[esi+1Ch]
  004c07ed  8b4e24                mov       ecx,[esi+24h]
  004c07f0  8b781c                mov       edi,[eax+1Ch]
  004c07f3  2bfd                  sub       edi,ebp
  004c07f5  8b6824                mov       ebp,[eax+24h]
  004c07f8  2be9                  sub       ebp,ecx
  004c07fa  c1ff0c                sar       edi,0Ch
  004c07fd  c1fd0c                sar       ebp,0Ch
  004c0800  8bc7                  mov       eax,edi
  004c0802  8bcd                  mov       ecx,ebp
  004c0804  0fafc7                imul      eax,edi
  004c0807  0fafcd                imul      ecx,ebp
  004c080a  03c1                  add       eax,ecx
  004c080c  50                    push      eax
  004c080d  e8fe510800            call      00545A10h
  004c0812  55                    push      ebp
  004c0813  57                    push      edi
  004c0814  89442420              mov       [esp+20h],eax
  004c0818  e805520800            call      00545A22h
  004c081d  668b5632              mov       dx,[esi+32h]
  004c0821  66895e12              mov       [esi+12h],bx
  004c0825  2bc2                  sub       eax,edx
  004c0827  25ff0f0000            and       eax,0FFFh
  004c082c  89442414              mov       [esp+14h],eax
  004c0830  8b442418              mov       eax,[esp+18h]
  004c0834  3dfa000000            cmp       eax,0FAh
  004c0839  0f8dc0000000          jge       near 004C08FFh
  004c083f  e8fc680800            call      00547140h
  004c0844  99                    cdq
  004c0845  b964000000            mov       ecx,64h
  004c084a  f7f9                  idiv      ecx
  004c084c  83fa32                cmp       edx,32h
  004c084f  7c3f                  jl        short 004C0890h
  004c0851  83fa46                cmp       edx,46h
  004c0854  7d26                  jge       short 004C087Ch
  004c0856  8b6c241c              mov       ebp,[esp+1Ch]
  004c085a  8b442410              mov       eax,[esp+10h]
  004c085e  8bd5                  mov       edx,ebp
  004c0860  885e42                mov       [esi+42h],bl
  004c0863  d1fa                  sar       edx,1
  004c0865  80c20a                add       dl,0Ah
  004c0868  8d4c0050              lea       ecx,[eax+eax+50h]
  004c086c  885643                mov       [esi+43h],dl
  004c086f  66894e10              mov       [esi+10h],cx
  004c0873  66895e12              mov       [esi+12h],bx
  004c0877  e987000000            jmp       004C0903h

LAB_004c087c:
  004c087c  83fa5a                cmp       edx,5Ah
  004c087f  7d5f                  jge       short 004C08E0h
  004c0881  8b542410              mov       edx,[esp+10h]
  004c0885  8b442418              mov       eax,[esp+18h]
  004c0889  83c264                add       edx,64h
  004c088c  3bc2                  cmp       eax,edx
  004c088e  7e50                  jle       short 004C08E0h

LAB_004c0890:
  004c0890  c6464203              mov       byte [esi+42h],3
  004c0894  e8a7680800            call      00547140h
  004c0899  99                    cdq
  004c089a  b90a000000            mov       ecx,0Ah
  004c089f  8b6c241c              mov       ebp,[esp+1Ch]
  004c08a3  f7f9                  idiv      ecx
  004c08a5  8b4c2410              mov       ecx,[esp+10h]
  004c08a9  8bc5                  mov       eax,ebp
  004c08ab  d1f8                  sar       eax,1
  004c08ad  8db980000000          lea       edi,[ecx+80h]
  004c08b3  c1e704                shl       edi,4
  004c08b6  02d0                  add       dl,al
  004c08b8  80c214                add       dl,14h
  004c08bb  885643                mov       [esi+43h],dl
  004c08be  e87d680800            call      00547140h
  004c08c3  99                    cdq
  004c08c4  f7ff                  idiv      edi
  004c08c6  d1ff                  sar       edi,1
  004c08c8  2bd7                  sub       edx,edi
  004c08ca  8b7c2414              mov       edi,[esp+14h]
  004c08ce  03d7                  add       edx,edi
  004c08d0  66035632              add       dx,[esi+32h]
  004c08d4  81c200080000          add       edx,800h
  004c08da  66895610              mov       [esi+10h],dx
  004c08de  eb23                  jmp       short 004C0903h

LAB_004c08e0:
  004c08e0  c6464201              mov       byte [esi+42h],1
  004c08e4  e857680800            call      00547140h
  004c08e9  99                    cdq
  004c08ea  33c2                  xor       eax,edx
  004c08ec  c6464301              mov       byte [esi+43h],1
  004c08f0  2bc2                  sub       eax,edx
  004c08f2  25ff0f0000            and       eax,0FFFh
  004c08f7  33c2                  xor       eax,edx
  004c08f9  2bc2                  sub       eax,edx
  004c08fb  66894610              mov       [esi+10h],ax

LAB_004c08ff:
  004c08ff  8b6c241c              mov       ebp,[esp+1Ch]

LAB_004c0903:
  004c0903  8b442418              mov       eax,[esp+18h]
  004c0907  3d58020000            cmp       eax,258h
  004c090c  0f8df4000000          jge       near 004C0A06h
  004c0912  3dfa000000            cmp       eax,0FAh
  004c0917  0f8ce9000000          jl        near 004C0A06h
  004c091d  e81e680800            call      00547140h
  004c0922  99                    cdq
  004c0923  b964000000            mov       ecx,64h
  004c0928  f7f9                  idiv      ecx
  004c092a  83fa14                cmp       edx,14h
  004c092d  7d54                  jge       short 004C0983h
  004c092f  8b542414              mov       edx,[esp+14h]
  004c0933  81c200040000          add       edx,400h
  004c0939  81e2ff0f0000          and       edx,0FFFh
  004c093f  81fa00080000          cmp       edx,800h
  004c0945  7c0d                  jl        short 004C0954h
  004c0947  c6464201              mov       byte [esi+42h],1
  004c094b  c6464301              mov       byte [esi+43h],1
  004c094f  e9b2000000            jmp       004C0A06h

LAB_004c0954:
  004c0954  885e42                mov       [esi+42h],bl
  004c0957  e8e4670800            call      00547140h
  004c095c  99                    cdq
  004c095d  b90a000000            mov       ecx,0Ah
  004c0962  66895e12              mov       [esi+12h],bx
  004c0966  f7f9                  idiv      ecx
  004c0968  8b4c2410              mov       ecx,[esp+10h]
  004c096c  8bc5                  mov       eax,ebp
  004c096e  c1f802                sar       eax,2
  004c0971  02d0                  add       dl,al
  004c0973  885643                mov       [esi+43h],dl
  004c0976  8d540950              lea       edx,[ecx+ecx+50h]
  004c097a  66895610              mov       [esi+10h],dx
  004c097e  e983000000            jmp       004C0A06h

LAB_004c0983:
  004c0983  83fa1e                cmp       edx,1Eh
  004c0986  7d25                  jge       short 004C09ADh
  004c0988  c6464202              mov       byte [esi+42h],2
  004c098c  e8af670800            call      00547140h
  004c0991  99                    cdq
  004c0992  b90a000000            mov       ecx,0Ah
  004c0997  66c746105000          mov       word [esi+10h],50h
  004c099d  f7f9                  idiv      ecx
  004c099f  8bc5                  mov       eax,ebp
  004c09a1  d1f8                  sar       eax,1
  004c09a3  02d0                  add       dl,al
  004c09a5  80c214                add       dl,14h
  004c09a8  885643                mov       [esi+43h],dl
  004c09ab  eb59                  jmp       short 004C0A06h

LAB_004c09ad:
  004c09ad  83fa3c                cmp       edx,3Ch
  004c09b0  7d0a                  jge       short 004C09BCh
  004c09b2  c646420a              mov       byte [esi+42h],0Ah
  004c09b6  c6464301              mov       byte [esi+43h],1
  004c09ba  eb4a                  jmp       short 004C0A06h

LAB_004c09bc:
  004c09bc  c6464203              mov       byte [esi+42h],3
  004c09c0  e87b670800            call      00547140h
  004c09c5  99                    cdq
  004c09c6  b90a000000            mov       ecx,0Ah
  004c09cb  f7f9                  idiv      ecx
  004c09cd  8b4c2410              mov       ecx,[esp+10h]
  004c09d1  8bc5                  mov       eax,ebp
  004c09d3  d1f8                  sar       eax,1
  004c09d5  8db980000000          lea       edi,[ecx+80h]
  004c09db  c1e704                shl       edi,4
  004c09de  02d0                  add       dl,al
  004c09e0  80c214                add       dl,14h
  004c09e3  885643                mov       [esi+43h],dl
  004c09e6  e855670800            call      00547140h
  004c09eb  99                    cdq
  004c09ec  f7ff                  idiv      edi
  004c09ee  d1ff                  sar       edi,1
  004c09f0  2bd7                  sub       edx,edi
  004c09f2  8b7c2414              mov       edi,[esp+14h]
  004c09f6  03d7                  add       edx,edi
  004c09f8  66035632              add       dx,[esi+32h]
  004c09fc  81c200080000          add       edx,800h
  004c0a02  66895610              mov       [esi+10h],dx

LAB_004c0a06:
  004c0a06  817c241858020000      cmp       dword [esp+18h],258h
  004c0a0e  0f8cff000000          jl        near 004C0B13h
  004c0a14  e827670800            call      00547140h
  004c0a19  99                    cdq
  004c0a1a  b964000000            mov       ecx,64h
  004c0a1f  f7f9                  idiv      ecx
  004c0a21  83fa32                cmp       edx,32h
  004c0a24  7d6f                  jge       short 004C0A95h
  004c0a26  8b442410              mov       eax,[esp+10h]
  004c0a2a  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004c0a2e  3bd1                  cmp       edx,ecx
  004c0a30  7d2e                  jge       short 004C0A60h
  004c0a32  c6464202              mov       byte [esi+42h],2
  004c0a36  e805670800            call      00547140h
  004c0a3b  99                    cdq
  004c0a3c  b90a000000            mov       ecx,0Ah
  004c0a41  66c746106400          mov       word [esi+10h],64h
  004c0a47  f7f9                  idiv      ecx
  004c0a49  8bc5                  mov       eax,ebp
  004c0a4b  d1f8                  sar       eax,1
  004c0a4d  02d0                  add       dl,al
  004c0a4f  80c214                add       dl,14h
  004c0a52  885643                mov       [esi+43h],dl
  004c0a55  5f                    pop       edi
  004c0a56  5e                    pop       esi
  004c0a57  5d                    pop       ebp
  004c0a58  5b                    pop       ebx
  004c0a59  81c488000000          add       esp,88h
  004c0a5f  c3                    ret

LAB_004c0a60:
  004c0a60  885e42                mov       [esi+42h],bl
  004c0a63  e8d8660800            call      00547140h
  004c0a68  99                    cdq
  004c0a69  b90a000000            mov       ecx,0Ah
  004c0a6e  66895e12              mov       [esi+12h],bx
  004c0a72  f7f9                  idiv      ecx
  004c0a74  8b4c2410              mov       ecx,[esp+10h]
  004c0a78  8bc5                  mov       eax,ebp
  004c0a7a  c1f802                sar       eax,2
  004c0a7d  02d0                  add       dl,al
  004c0a7f  885643                mov       [esi+43h],dl
  004c0a82  8d540950              lea       edx,[ecx+ecx+50h]
  004c0a86  66895610              mov       [esi+10h],dx
  004c0a8a  5f                    pop       edi
  004c0a8b  5e                    pop       esi
  004c0a8c  5d                    pop       ebp
  004c0a8d  5b                    pop       ebx
  004c0a8e  81c488000000          add       esp,88h
  004c0a94  c3                    ret

LAB_004c0a95:
  004c0a95  83fa46                cmp       edx,46h
  004c0a98  7d55                  jge       short 004C0AEFh
  004c0a9a  c6464203              mov       byte [esi+42h],3
  004c0a9e  e89d660800            call      00547140h
  004c0aa3  99                    cdq
  004c0aa4  b90a000000            mov       ecx,0Ah
  004c0aa9  f7f9                  idiv      ecx
  004c0aab  8b4c2410              mov       ecx,[esp+10h]
  004c0aaf  8bc5                  mov       eax,ebp
  004c0ab1  d1f8                  sar       eax,1
  004c0ab3  8db980000000          lea       edi,[ecx+80h]
  004c0ab9  c1e704                shl       edi,4
  004c0abc  02d0                  add       dl,al
  004c0abe  80c214                add       dl,14h
  004c0ac1  885643                mov       [esi+43h],dl
  004c0ac4  e877660800            call      00547140h
  004c0ac9  99                    cdq
  004c0aca  f7ff                  idiv      edi
  004c0acc  d1ff                  sar       edi,1
  004c0ace  2bd7                  sub       edx,edi
  004c0ad0  8b7c2414              mov       edi,[esp+14h]
  004c0ad4  03d7                  add       edx,edi
  004c0ad6  66035632              add       dx,[esi+32h]
  004c0ada  81c200080000          add       edx,800h
  004c0ae0  66895610              mov       [esi+10h],dx
  004c0ae4  5f                    pop       edi
  004c0ae5  5e                    pop       esi
  004c0ae6  5d                    pop       ebp
  004c0ae7  5b                    pop       ebx
  004c0ae8  81c488000000          add       esp,88h
  004c0aee  c3                    ret

LAB_004c0aef:
  004c0aef  83fa5a                cmp       edx,5Ah
  004c0af2  c6464201              mov       byte [esi+42h],1
  004c0af6  7c17                  jl        short 004C0B0Fh
  004c0af8  e843660800            call      00547140h
  004c0afd  99                    cdq
  004c0afe  33c2                  xor       eax,edx
  004c0b00  2bc2                  sub       eax,edx
  004c0b02  25ff0f0000            and       eax,0FFFh
  004c0b07  33c2                  xor       eax,edx
  004c0b09  2bc2                  sub       eax,edx
  004c0b0b  66894610              mov       [esi+10h],ax

LAB_004c0b0f:
  004c0b0f  c6464301              mov       byte [esi+43h],1

LAB_004c0b13:
  004c0b13  5f                    pop       edi
  004c0b14  5e                    pop       esi
  004c0b15  5d                    pop       ebp
  004c0b16  5b                    pop       ebx
  004c0b17  81c488000000          add       esp,88h
  004c0b1d  c3                    ret

LAB_004c0b1e:
  004c0b1e  885e43                mov       [esi+43h],bl
  004c0b21  e9cbf9ffff            jmp       004C04F1h
  004c0b26  8bff                  mov       edi,edi
  004c0b28  b9004c0003            mov       ecx,3004C00h
  004c0b2d  014c006a              add       [eax+eax+6Ah],ecx
  004c0b31  014c0012              add       [eax+eax+12h],ecx
  004c0b35  024c00f1              add       cl,[eax+eax-0Fh]
  004c0b39  044c                  add       al,4Ch
  004c0b3b  00f1                  add       cl,dh
  004c0b3d  044c                  add       al,4Ch
  004c0b3f  00f1                  add       cl,dh
  004c0b41  044c                  add       al,4Ch
  004c0b43  001b                  add       [ebx],bl
  004c0b45  034c00f1              add       ecx,[eax+eax-0Fh]
  004c0b49  044c                  add       al,4Ch
  004c0b4b  007203                add       [edx+3],dh
  004c0b4e  4c                    dec       esp
  004c0b4f  00ad034c0090          add       [ebp-6FFFB3FDh],ch
  004c0b55  90                    nop
  004c0b56  90                    nop
  004c0b57  90                    nop
  004c0b58  90                    nop
  004c0b59  90                    nop
  004c0b5a  90                    nop
  004c0b5b  90                    nop
  004c0b5c  90                    nop
  004c0b5d  90                    nop
  004c0b5e  90                    nop
  004c0b5f  90                    nop

66 cible(s) de saut interne : 0x004bfefd, 0x004bff01, 0x004bff03, 0x004c0015, 0x004c0038, 0x004c003a, 0x004c009c, 0x004c00a4, 0x004c00d9, 0x004c00eb, 0x004c0120, 0x004c0157, 0x004c0196, 0x004c01bb, 0x004c01c7, 0x004c01ff, 0x004c022d, 0x004c029d, 0x004c02ab, 0x004c02af, 0x004c02cf, 0x004c02d8, 0x004c0334, 0x004c0340, 0x004c039b, 0x004c039d, 0x004c03a2, 0x004c03a5, 0x004c03be, 0x004c03cf, 0x004c03f2, 0x004c0424, 0x004c0439, 0x004c048c, 0x004c0491, 0x004c04a7, 0x004c04e0, 0x004c04ef, 0x004c04f1, 0x004c0527, 0x004c0566, 0x004c0587, 0x004c058f, 0x004c0594, 0x004c05da, 0x004c06cd, 0x004c0712, 0x004c07a2, 0x004c07b0, 0x004c07de, 0x004c087c, 0x004c0890, 0x004c08e0, 0x004c08ff, 0x004c0903, 0x004c0954, 0x004c0983, 0x004c09ad, 0x004c09bc, 0x004c0a06, 0x004c0a60, 0x004c0a95, 0x004c0aef, 0x004c0b0f, 0x004c0b13, 0x004c0b1e
