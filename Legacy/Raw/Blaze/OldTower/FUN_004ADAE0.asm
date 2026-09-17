Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004adae0  section .text  offset fichier 0xacee0  3392 octets

  004adae0  81ec84000000          sub       esp,84h
  004adae6  53                    push      ebx
  004adae7  55                    push      ebp
  004adae8  56                    push      esi
  004adae9  8bb42494000000        mov       esi,[esp+94h]
  004adaf0  b864000000            mov       eax,64h
  004adaf5  33db                  xor       ebx,ebx
  004adaf7  6689442432            mov       [esp+32h],ax
  004adafc  6689442436            mov       [esp+36h],ax
  004adb01  8a4641                mov       al,[esi+41h]
  004adb04  57                    push      edi
  004adb05  3ac3                  cmp       al,bl
  004adb07  66895c2430            mov       [esp+30h],bx
  004adb0c  66c74424321e00        mov       word [esp+32h],1Eh
  004adb13  66c74424343200        mov       word [esp+34h],32h
  004adb1a  66895c2438            mov       [esp+38h],bx
  004adb1f  7510                  jne       short 004ADB31h
  004adb21  53                    push      ebx
  004adb22  6a02                  push      2
  004adb24  56                    push      esi
  004adb25  c6464101              mov       byte [esi+41h],1
  004adb29  e832edffff            call      004AC860h
  004adb2e  83c40c                add       esp,0Ch

LAB_004adb31:
  004adb31  8b461c                mov       eax,[esi+1Ch]
  004adb34  8b4e20                mov       ecx,[esi+20h]
  004adb37  8b5624                mov       edx,[esi+24h]
  004adb3a  8d6e1c                lea       ebp,[esi+1Ch]
  004adb3d  89442424              mov       [esp+24h],eax
  004adb41  668b4632              mov       ax,[esi+32h]
  004adb45  894c2428              mov       [esp+28h],ecx
  004adb49  8b4e64                mov       ecx,[esi+64h]
  004adb4c  8954242c              mov       [esp+2Ch],edx
  004adb50  8b5660                mov       edx,[esi+60h]
  004adb53  6689842482000000      mov       [esp+82h],ax
  004adb5b  8b467c                mov       eax,[esi+7Ch]
  004adb5e  894c2444              mov       [esp+44h],ecx
  004adb62  668b8e90000000        mov       cx,[esi+90h]
  004adb69  89542440              mov       [esp+40h],edx
  004adb6d  8b9680000000          mov       edx,[esi+80h]
  004adb73  8944245c              mov       [esp+5Ch],eax
  004adb77  8b8684000000          mov       eax,[esi+84h]
  004adb7d  66894c2448            mov       [esp+48h],cx
  004adb82  8b8e88000000          mov       ecx,[esi+88h]
  004adb88  8b7e6c                mov       edi,[esi+6Ch]
  004adb8b  8954244c              mov       [esp+4Ch],edx
  004adb8f  8b968c000000          mov       edx,[esi+8Ch]
  004adb95  8944243c              mov       [esp+3Ch],eax
  004adb99  894c2458              mov       [esp+58h],ecx
  004adb9d  8d442460              lea       eax,[esp+60h]
  004adba1  89542450              mov       [esp+50h],edx
  004adba5  8d4c2468              lea       ecx,[esp+68h]
  004adba9  50                    push      eax
  004adbaa  8d542468              lea       edx,[esp+68h]
  004adbae  51                    push      ecx
  004adbaf  52                    push      edx
  004adbb0  56                    push      esi
  004adbb1  e8caad0800            call      00538980h
  004adbb6  83c410                add       esp,10h
  004adbb9  8d442454              lea       eax,[esp+54h]
  004adbbd  8d4c241c              lea       ecx,[esp+1Ch]
  004adbc1  8d542418              lea       edx,[esp+18h]
  004adbc5  50                    push      eax
  004adbc6  51                    push      ecx
  004adbc7  8d44241c              lea       eax,[esp+1Ch]
  004adbcb  52                    push      edx
  004adbcc  8d4c241c              lea       ecx,[esp+1Ch]
  004adbd0  50                    push      eax
  004adbd1  51                    push      ecx
  004adbd2  56                    push      esi
  004adbd3  e848830800            call      00535F20h
  004adbd8  8b4c2428              mov       ecx,[esp+28h]
  004adbdc  8b54246c              mov       edx,[esp+6Ch]
  004adbe0  83c418                add       esp,18h
  004adbe3  b840000000            mov       eax,40h
  004adbe8  2bc1                  sub       eax,ecx
  004adbea  52                    push      edx
  004adbeb  56                    push      esi
  004adbec  89442428              mov       [esp+28h],eax
  004adbf0  e8eb810200            call      004D5DE0h
  004adbf5  8b4500                mov       eax,[ebp]
  004adbf8  8b571c                mov       edx,[edi+1Ch]
  004adbfb  8b4e20                mov       ecx,[esi+20h]
  004adbfe  2bc2                  sub       eax,edx
  004adc00  8b5720                mov       edx,[edi+20h]
  004adc03  83c408                add       esp,8
  004adc06  2bca                  sub       ecx,edx
  004adc08  8b5624                mov       edx,[esi+24h]
  004adc0b  2b5724                sub       edx,[edi+24h]
  004adc0e  c1f80c                sar       eax,0Ch
  004adc11  8bf8                  mov       edi,eax
  004adc13  c1f90c                sar       ecx,0Ch
  004adc16  0faff8                imul      edi,eax
  004adc19  8bc1                  mov       eax,ecx
  004adc1b  c1fa0c                sar       edx,0Ch
  004adc1e  0fafc1                imul      eax,ecx
  004adc21  8bca                  mov       ecx,edx
  004adc23  03f8                  add       edi,eax
  004adc25  0fafca                imul      ecx,edx
  004adc28  03f9                  add       edi,ecx
  004adc2a  57                    push      edi
  004adc2b  e8e07d0900            call      00545A10h
  004adc30  8b4c241c              mov       ecx,[esp+1Ch]
  004adc34  89442414              mov       [esp+14h],eax
  004adc38  3bcb                  cmp       ecx,ebx
  004adc3a  0f84d4050000          je        near 004AE214h
  004adc40  8b16                  mov       edx,[esi]
  004adc42  33c0                  xor       eax,eax
  004adc44  8a4604                mov       al,[esi+4]
  004adc47  81e2fffffffd          and       edx,0FDFFFFFFh
  004adc4d  0fbf7e18              movsx     edi,word [esi+18h]
  004adc51  8916                  mov       [esi],edx
  004adc53  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004adc5a  f7c200000001          test      edx,1000000h
  004adc60  8b3cb8                mov       edi,[eax+edi*4]
  004adc63  741b                  je        short 004ADC80h
  004adc65  66ff461a              inc       word [esi+1Ah]
  004adc69  668b461a              mov       ax,[esi+1Ah]
  004adc6d  33d2                  xor       edx,edx
  004adc6f  668b5716              mov       dx,[edi+16h]
  004adc73  0fbfc0                movsx     eax,ax
  004adc76  3bc2                  cmp       eax,edx
  004adc78  7c2a                  jl        short 004ADCA4h
  004adc7a  66895e1a              mov       [esi+1Ah],bx
  004adc7e  eb24                  jmp       short 004ADCA4h

LAB_004adc80:
  004adc80  66ff461a              inc       word [esi+1Ah]
  004adc84  668b461a              mov       ax,[esi+1Ah]
  004adc88  33db                  xor       ebx,ebx
  004adc8a  668b5f16              mov       bx,[edi+16h]
  004adc8e  0fbff8                movsx     edi,ax
  004adc91  3bfb                  cmp       edi,ebx
  004adc93  7c0d                  jl        short 004ADCA2h
  004adc95  48                    dec       eax
  004adc96  81ca00000002          or        edx,2000000h
  004adc9c  6689461a              mov       [esi+1Ah],ax
  004adca0  8916                  mov       [esi],edx

LAB_004adca2:
  004adca2  33db                  xor       ebx,ebx

LAB_004adca4:
  004adca4  8a4642                mov       al,[esi+42h]
  004adca7  3c05                  cmp       al,5
  004adca9  8b06                  mov       eax,[esi]
  004adcab  7504                  jne       short 004ADCB1h
  004adcad  0c10                  or        al,10h
  004adcaf  eb02                  jmp       short 004ADCB3h

LAB_004adcb1:
  004adcb1  24ef                  and       al,0EFh

LAB_004adcb3:
  004adcb3  8906                  mov       [esi],eax
  004adcb5  8a06                  mov       al,[esi]
  004adcb7  a802                  test      al,2
  004adcb9  747d                  je        short 004ADD38h
  004adcbb  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004adcc0  7558                  jne       short 004ADD1Ah
  004adcc2  8d4c2430              lea       ecx,[esp+30h]
  004adcc6  53                    push      ebx
  004adcc7  51                    push      ecx
  004adcc8  55                    push      ebp
  004adcc9  56                    push      esi
  004adcca  e861bf0600            call      00519C30h
  004adccf  8bf8                  mov       edi,eax
  004adcd1  a1b8e05d00            mov       eax,[5DE0B8h]
  004adcd6  83c410                add       esp,10h
  004adcd9  3bc3                  cmp       eax,ebx
  004adcdb  7439                  je        short 004ADD16h
  004adcdd  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004adce2  7532                  jne       short 004ADD16h
  004adce4  8b0f                  mov       ecx,[edi]
  004adce6  57                    push      edi
  004adce7  83c940                or        ecx,40h
  004adcea  56                    push      esi
  004adceb  66c787960000001000    mov       word [edi+96h],10h
  004adcf4  890f                  mov       [edi],ecx
  004adcf6  e885be0800            call      00539B80h
  004adcfb  83c408                add       esp,8
  004adcfe  83c71c                add       edi,1Ch
  004add01  57                    push      edi
  004add02  6800180000            push      1800h
  004add07  6800030000            push      300h
  004add0c  6a03                  push      3
  004add0e  e80dfe0800            call      0053DB20h
  004add13  83c410                add       esp,10h

LAB_004add16:
  004add16  8b4c241c              mov       ecx,[esp+1Ch]

LAB_004add1a:
  004add1a  8b16                  mov       edx,[esi]
  004add1c  f7c200000002          test      edx,2000000h
  004add22  7414                  je        short 004ADD38h
  004add24  f6c201                test      dl,1
  004add27  750a                  jne       short 004ADD33h
  004add29  83e2fd                and       edx,0FFFFFFFDh
  004add2c  885e43                mov       [esi+43h],bl
  004add2f  8916                  mov       [esi],edx
  004add31  eb05                  jmp       short 004ADD38h

LAB_004add33:
  004add33  83e2fd                and       edx,0FFFFFFFDh
  004add36  8916                  mov       [esi],edx

LAB_004add38:
  004add38  33c0                  xor       eax,eax
  004add3a  8a4642                mov       al,[esi+42h]
  004add3d  83f80b                cmp       eax,0Bh
  004add40  0f87ce040000          ja        near 004AE214h
  004add46  ff2485e8e74a00        jmp       dword [eax*4+4AE7E8h]
  004add4d  66395e12              cmp       [esi+12h],bx
  004add51  751a                  jne       short 004ADD6Dh
  004add53  8b06                  mov       eax,[esi]
  004add55  66895e18              mov       [esi+18h],bx
  004add59  0d00000001            or        eax,1000000h
  004add5e  66895e1a              mov       [esi+1Ah],bx
  004add62  8906                  mov       [esi],eax
  004add64  66c746120100          mov       word [esi+12h],1
  004add6a  895e5c                mov       [esi+5Ch],ebx

LAB_004add6d:
  004add6d  0fbf5610              movsx     edx,word [esi+10h]
  004add71  39542414              cmp       [esp+14h],edx
  004add75  7d08                  jge       short 004ADD7Fh
  004add77  885e43                mov       [esi+43h],bl
  004add7a  e995040000            jmp       004AE214h

LAB_004add7f:
  004add7f  8a4643                mov       al,[esi+43h]
  004add82  3ac3                  cmp       al,bl
  004add84  0f84ce000000          je        near 004ADE58h
  004add8a  fec8                  dec       al
  004add8c  895e5c                mov       [esi+5Ch],ebx
  004add8f  884643                mov       [esi+43h],al
  004add92  e97d040000            jmp       004AE214h
  004add97  66395e12              cmp       [esi+12h],bx
  004add9b  751b                  jne       short 004ADDB8h
  004add9d  8b0e                  mov       ecx,[esi]
  004add9f  b801000000            mov       eax,1
  004adda4  81c900000001          or        ecx,1000000h
  004addaa  66894618              mov       [esi+18h],ax
  004addae  66895e1a              mov       [esi+1Ah],bx
  004addb2  890e                  mov       [esi],ecx
  004addb4  66894612              mov       [esi+12h],ax

LAB_004addb8:
  004addb8  8b442418              mov       eax,[esp+18h]
  004addbc  8d88c8000000          lea       ecx,[eax+0C8h]
  004addc2  81e1ff0f0000          and       ecx,0FFFh
  004addc8  81f990010000          cmp       ecx,190h
  004addce  0f8e81000000          jle       near 004ADE55h
  004addd4  25ff0f0000            and       eax,0FFFh
  004addd9  3d00080000            cmp       eax,800h
  004addde  7d16                  jge       short 004ADDF6h
  004adde0  8b542410              mov       edx,[esp+10h]
  004adde4  895e5c                mov       [esi+5Ch],ebx
  004adde7  8d8280000000          lea       eax,[edx+80h]
  004added  66014632              add       [esi+32h],ax
  004addf1  e91e040000            jmp       004AE214h

LAB_004addf6:
  004addf6  8b4c2410              mov       ecx,[esp+10h]
  004addfa  b880ffffff            mov       eax,0FFFFFF80h
  004addff  2bc1                  sub       eax,ecx
  004ade01  895e5c                mov       [esi+5Ch],ebx
  004ade04  66014632              add       [esi+32h],ax
  004ade08  e907040000            jmp       004AE214h
  004ade0d  66395e12              cmp       [esi+12h],bx
  004ade11  8b4c2410              mov       ecx,[esp+10h]
  004ade15  b801000000            mov       eax,1
  004ade1a  7525                  jne       short 004ADE41h
  004ade1c  8b16                  mov       edx,[esi]
  004ade1e  66894618              mov       [esi+18h],ax
  004ade22  81ca00000001          or        edx,1000000h
  004ade28  66895e1a              mov       [esi+1Ah],bx
  004ade2c  8916                  mov       [esi],edx
  004ade2e  8d14c9                lea       edx,[ecx+ecx*8]
  004ade31  c1e204                shl       edx,4
  004ade34  81c200500000          add       edx,5000h
  004ade3a  66894612              mov       [esi+12h],ax
  004ade3e  89565c                mov       [esi+5Ch],edx

LAB_004ade41:
  004ade41  66394612              cmp       [esi+12h],ax
  004ade45  0f85c9030000          jne       near 004AE214h
  004ade4b  0fbf4610              movsx     eax,word [esi+10h]
  004ade4f  39442414              cmp       [esp+14h],eax
  004ade53  7d0b                  jge       short 004ADE60h

LAB_004ade55:
  004ade55  885e43                mov       [esi+43h],bl

LAB_004ade58:
  004ade58  895e5c                mov       [esi+5Ch],ebx
  004ade5b  e9b4030000            jmp       004AE214h

LAB_004ade60:
  004ade60  8a4643                mov       al,[esi+43h]
  004ade63  3ac3                  cmp       al,bl
  004ade65  7405                  je        short 004ADE6Ch
  004ade67  fec8                  dec       al
  004ade69  884643                mov       [esi+43h],al

LAB_004ade6c:
  004ade6c  8b442418              mov       eax,[esp+18h]
  004ade70  8d9000010000          lea       edx,[eax+100h]
  004ade76  81e2ff0f0000          and       edx,0FFFh
  004ade7c  81fa00020000          cmp       edx,200h
  004ade82  0f8e8c030000          jle       near 004AE214h
  004ade88  25ff0f0000            and       eax,0FFFh
  004ade8d  3d00080000            cmp       eax,800h
  004ade92  7d10                  jge       short 004ADEA4h
  004ade94  8d840980000000        lea       eax,[ecx+ecx+80h]
  004ade9b  66014632              add       [esi+32h],ax
  004ade9f  e970030000            jmp       004AE214h

LAB_004adea4:
  004adea4  03c9                  add       ecx,ecx
  004adea6  b880ffffff            mov       eax,0FFFFFF80h
  004adeab  2bc1                  sub       eax,ecx
  004adead  66014632              add       [esi+32h],ax
  004adeb1  e95e030000            jmp       004AE214h
  004adeb6  66837e1201            cmp       word [esi+12h],1
  004adebb  0f85a1000000          jne       near 004ADF62h
  004adec1  8a4643                mov       al,[esi+43h]
  004adec4  3ac3                  cmp       al,bl
  004adec6  7405                  je        short 004ADECDh
  004adec8  fec8                  dec       al
  004adeca  884643                mov       [esi+43h],al

LAB_004adecd:
  004adecd  8b442410              mov       eax,[esp+10h]
  004aded1  8d14c0                lea       edx,[eax+eax*8]
  004aded4  c1e204                shl       edx,4
  004aded7  8d8200500000          lea       eax,[edx+5000h]
  004adedd  0fafc8                imul      ecx,eax
  004adee0  89465c                mov       [esi+5Ch],eax
  004adee3  0fbf4610              movsx     eax,word [esi+10h]
  004adee7  c1f90c                sar       ecx,0Ch
  004adeea  50                    push      eax
  004adeeb  8bf9                  mov       edi,ecx
  004adeed  e82a7b0900            call      00545A1Ch
  004adef2  0fafc7                imul      eax,edi
  004adef5  8b4d00                mov       ecx,[ebp]
  004adef8  c1f80c                sar       eax,0Ch
  004adefb  03c8                  add       ecx,eax
  004adefd  894d00                mov       [ebp],ecx
  004adf00  0fbf4e10              movsx     ecx,word [esi+10h]
  004adf04  51                    push      ecx
  004adf05  e80c7b0900            call      00545A16h
  004adf0a  0fafc7                imul      eax,edi
  004adf0d  8b6e24                mov       ebp,[esi+24h]
  004adf10  895e5c                mov       [esi+5Ch],ebx
  004adf13  c1f80c                sar       eax,0Ch
  004adf16  03e8                  add       ebp,eax
  004adf18  8b442418              mov       eax,[esp+18h]
  004adf1c  896e24                mov       [esi+24h],ebp
  004adf1f  8d9000010000          lea       edx,[eax+100h]
  004adf25  81e2ff0f0000          and       edx,0FFFh
  004adf2b  81fa00020000          cmp       edx,200h
  004adf31  7e2f                  jle       short 004ADF62h
  004adf33  25ff0f0000            and       eax,0FFFh
  004adf38  3d00080000            cmp       eax,800h
  004adf3d  7d0d                  jge       short 004ADF4Ch
  004adf3f  8b442410              mov       eax,[esp+10h]
  004adf43  8d04c532000000        lea       eax,[eax*8+32h]
  004adf4a  eb12                  jmp       short 004ADF5Eh

LAB_004adf4c:
  004adf4c  8b4c2410              mov       ecx,[esp+10h]
  004adf50  b8ceffffff            mov       eax,0FFFFFFCEh
  004adf55  8d14cd00000000        lea       edx,[ecx*8]
  004adf5c  2bc2                  sub       eax,edx

LAB_004adf5e:
  004adf5e  66014632              add       [esi+32h],ax

LAB_004adf62:
  004adf62  66395e12              cmp       [esi+12h],bx
  004adf66  0f85a8020000          jne       near 004AE214h
  004adf6c  8b0e                  mov       ecx,[esi]
  004adf6e  b801000000            mov       eax,1
  004adf73  81c900000001          or        ecx,1000000h
  004adf79  66894618              mov       [esi+18h],ax
  004adf7d  66895e1a              mov       [esi+1Ah],bx
  004adf81  890e                  mov       [esi],ecx
  004adf83  66894612              mov       [esi+12h],ax
  004adf87  e988020000            jmp       004AE214h
  004adf8c  66395e12              cmp       [esi+12h],bx
  004adf90  757c                  jne       short 004AE00Eh
  004adf92  8b06                  mov       eax,[esi]
  004adf94  a801                  test      al,1
  004adf96  7405                  je        short 004ADF9Dh
  004adf98  885e43                mov       [esi+43h],bl
  004adf9b  eb71                  jmp       short 004AE00Eh

LAB_004adf9d:
  004adf9d  25fffffffe            and       eax,0FEFFFFFFh
  004adfa2  bd01000000            mov       ebp,1
  004adfa7  8906                  mov       [esi],eax
  004adfa9  8b442410              mov       eax,[esp+10h]
  004adfad  0500010000            add       eax,100h
  004adfb2  66896e18              mov       [esi+18h],bp
  004adfb6  c1e006                shl       eax,6
  004adfb9  0fafc8                imul      ecx,eax
  004adfbc  c1f90c                sar       ecx,0Ch
  004adfbf  8bf9                  mov       edi,ecx
  004adfc1  66895e1a              mov       [esi+1Ah],bx
  004adfc5  0fbf4e10              movsx     ecx,word [esi+10h]
  004adfc9  51                    push      ecx
  004adfca  89465c                mov       [esi+5Ch],eax
  004adfcd  e84a7a0900            call      00545A1Ch
  004adfd2  0fafc7                imul      eax,edi
  004adfd5  0fbf5610              movsx     edx,word [esi+10h]
  004adfd9  8b4e50                mov       ecx,[esi+50h]
  004adfdc  52                    push      edx
  004adfdd  c1f80c                sar       eax,0Ch
  004adfe0  03c8                  add       ecx,eax
  004adfe2  894e50                mov       [esi+50h],ecx
  004adfe5  e82c7a0900            call      00545A16h
  004adfea  0fafc7                imul      eax,edi
  004adfed  8b5658                mov       edx,[esi+58h]
  004adff0  8b4e54                mov       ecx,[esi+54h]
  004adff3  c1f80c                sar       eax,0Ch
  004adff6  03d0                  add       edx,eax
  004adff8  8b06                  mov       eax,[esi]
  004adffa  81c100e00100          add       ecx,1E000h
  004ae000  0bc5                  or        eax,ebp
  004ae002  895658                mov       [esi+58h],edx
  004ae005  894e54                mov       [esi+54h],ecx
  004ae008  8906                  mov       [esi],eax
  004ae00a  66896e12              mov       [esi+12h],bp

LAB_004ae00e:
  004ae00e  f60601                test      byte [esi],1
  004ae011  0f85fd010000          jne       near 004AE214h
  004ae017  885e43                mov       [esi+43h],bl
  004ae01a  e9f5010000            jmp       004AE214h
  004ae01f  668b4e12              mov       cx,[esi+12h]
  004ae023  ba01000000            mov       edx,1
  004ae028  663bca                cmp       cx,dx
  004ae02b  751b                  jne       short 004AE048h
  004ae02d  0fbf4610              movsx     eax,word [esi+10h]
  004ae031  39442414              cmp       [esp+14h],eax
  004ae035  7d05                  jge       short 004AE03Ch
  004ae037  885e43                mov       [esi+43h],bl
  004ae03a  eb0c                  jmp       short 004AE048h

LAB_004ae03c:
  004ae03c  8a4643                mov       al,[esi+43h]
  004ae03f  3ac3                  cmp       al,bl
  004ae041  7405                  je        short 004AE048h
  004ae043  fec8                  dec       al
  004ae045  884643                mov       [esi+43h],al

LAB_004ae048:
  004ae048  663bcb                cmp       cx,bx
  004ae04b  0f85c3010000          jne       near 004AE214h
  004ae051  8b442410              mov       eax,[esp+10h]
  004ae055  8b3e                  mov       edi,[esi]
  004ae057  81cf00000001          or        edi,1000000h
  004ae05d  66895618              mov       [esi+18h],dx
  004ae061  8d0cc0                lea       ecx,[eax+eax*8]
  004ae064  66895e1a              mov       [esi+1Ah],bx
  004ae068  c1e104                shl       ecx,4
  004ae06b  81c100500000          add       ecx,5000h
  004ae071  893e                  mov       [esi],edi
  004ae073  66895612              mov       [esi+12h],dx
  004ae077  894e5c                mov       [esi+5Ch],ecx
  004ae07a  e995010000            jmp       004AE214h
  004ae07f  66395e12              cmp       [esi+12h],bx
  004ae083  0f85dc000000          jne       near 004AE165h
  004ae089  33ff                  xor       edi,edi
  004ae08b  b800f45f00            mov       eax,5FF400h

LAB_004ae090:
  004ae090  39706c                cmp       [eax+6Ch],esi
  004ae093  750c                  jne       short 004AE0A1h
  004ae095  f70000000080          test      dword [eax],80000000h
  004ae09b  0f8540010000          jne       near 004AE1E1h

LAB_004ae0a1:
  004ae0a1  059c000000            add       eax,9Ch
  004ae0a6  47                    inc       edi
  004ae0a7  3d80076000            cmp       eax,600780h
  004ae0ac  7ce2                  jl        short 004AE090h
  004ae0ae  8b06                  mov       eax,[esi]
  004ae0b0  a904080000            test      eax,804h
  004ae0b5  740d                  je        short 004AE0C4h
  004ae0b7  885e43                mov       [esi+43h],bl
  004ae0ba  25fbf7ffff            and       eax,0FFFFF7FBh
  004ae0bf  e94e010000            jmp       004AE212h

LAB_004ae0c4:
  004ae0c4  6aff                  push      0FFFFFFFFh
  004ae0c6  53                    push      ebx
  004ae0c7  68bd000000            push      0BDh
  004ae0cc  56                    push      esi
  004ae0cd  e80ecafcff            call      0047AAE0h
  004ae0d2  83c410                add       esp,10h
  004ae0d5  66395e16              cmp       [esi+16h],bx
  004ae0d9  bd01000000            mov       ebp,1
  004ae0de  66896e12              mov       [esi+12h],bp
  004ae0e2  751b                  jne       short 004AE0FFh
  004ae0e4  e857900900            call      00547140h
  004ae0e9  99                    cdq
  004ae0ea  33c2                  xor       eax,edx
  004ae0ec  2bc2                  sub       eax,edx
  004ae0ee  83e003                and       eax,3
  004ae0f1  33c2                  xor       eax,edx
  004ae0f3  2bc2                  sub       eax,edx
  004ae0f5  33ff                  xor       edi,edi
  004ae0f7  668b3c45602a5500      mov       di,[eax*2+552A60h]

LAB_004ae0ff:
  004ae0ff  66396e16              cmp       [esi+16h],bp
  004ae103  7517                  jne       short 004AE11Ch
  004ae105  e836900900            call      00547140h
  004ae10a  99                    cdq
  004ae10b  b903000000            mov       ecx,3
  004ae110  f7f9                  idiv      ecx
  004ae112  33ff                  xor       edi,edi
  004ae114  668b3c55682a5500      mov       di,[edx*2+552A68h]

LAB_004ae11c:
  004ae11c  66897e06              mov       [esi+6],di
  004ae120  a1c4975e00            mov       eax,[5E97C4h]
  004ae125  8bd7                  mov       edx,edi
  004ae127  81e7ff000000          and       edi,0FFh
  004ae12d  8b889c000000          mov       ecx,[eax+9Ch]
  004ae133  c1fa08                sar       edx,8
  004ae136  81e2ff000000          and       edx,0FFh
  004ae13c  8d047f                lea       eax,[edi+edi*2]
  004ae13f  c1e004                shl       eax,4
  004ae142  8b0c91                mov       ecx,[ecx+edx*4]
  004ae145  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  004ae14b  8b06                  mov       eax,[esi]
  004ae14d  66895610              mov       [esi+10h],dx
  004ae151  0d04080001            or        eax,1000804h
  004ae156  895e5c                mov       [esi+5Ch],ebx
  004ae159  66c746180400          mov       word [esi+18h],4
  004ae15f  66895e1a              mov       [esi+1Ah],bx
  004ae163  8906                  mov       [esi],eax

LAB_004ae165:
  004ae165  b801000000            mov       eax,1
  004ae16a  66394612              cmp       [esi+12h],ax
  004ae16e  7523                  jne       short 004AE193h
  004ae170  66ff4e10              dec       word [esi+10h]
  004ae174  66394610              cmp       [esi+10h],ax
  004ae178  7d19                  jge       short 004AE193h
  004ae17a  8b06                  mov       eax,[esi]
  004ae17c  66c746120200          mov       word [esi+12h],2
  004ae182  25fbfffffe            and       eax,0FEFFFFFBh
  004ae187  66c746180500          mov       word [esi+18h],5
  004ae18d  66895e1a              mov       [esi+1Ah],bx
  004ae191  8906                  mov       [esi],eax

LAB_004ae193:
  004ae193  66837e1202            cmp       word [esi+12h],2
  004ae198  7532                  jne       short 004AE1CCh
  004ae19a  66837e1a07            cmp       word [esi+1Ah],7
  004ae19f  752b                  jne       short 004AE1CCh
  004ae1a1  33c0                  xor       eax,eax
  004ae1a3  6aff                  push      0FFFFFFFFh
  004ae1a5  668b4606              mov       ax,[esi+6]
  004ae1a9  6aff                  push      0FFFFFFFFh
  004ae1ab  8bc8                  mov       ecx,eax
  004ae1ad  c1f808                sar       eax,8
  004ae1b0  81e1ff000000          and       ecx,0FFh
  004ae1b6  25ff000000            and       eax,0FFh
  004ae1bb  51                    push      ecx
  004ae1bc  50                    push      eax
  004ae1bd  56                    push      esi
  004ae1be  e8bdc70800            call      0053A980h
  004ae1c3  83c414                add       esp,14h
  004ae1c6  66c746120300          mov       word [esi+12h],3

LAB_004ae1cc:
  004ae1cc  66837e1203            cmp       word [esi+12h],3
  004ae1d1  7541                  jne       short 004AE214h
  004ae1d3  8b06                  mov       eax,[esi]
  004ae1d5  a900000002            test      eax,2000000h
  004ae1da  7438                  je        short 004AE214h
  004ae1dc  80e4f7                and       ah,0F7h
  004ae1df  8906                  mov       [esi],eax

LAB_004ae1e1:
  004ae1e1  885e43                mov       [esi+43h],bl
  004ae1e4  eb2e                  jmp       short 004AE214h
  004ae1e6  668b4e12              mov       cx,[esi+12h]
  004ae1ea  6683f901              cmp       cx,1
  004ae1ee  7510                  jne       short 004AE200h
  004ae1f0  8b06                  mov       eax,[esi]
  004ae1f2  a900000002            test      eax,2000000h
  004ae1f7  7407                  je        short 004AE200h
  004ae1f9  24fd                  and       al,0FDh
  004ae1fb  885e43                mov       [esi+43h],bl
  004ae1fe  8906                  mov       [esi],eax

LAB_004ae200:
  004ae200  663bcb                cmp       cx,bx
  004ae203  750f                  jne       short 004AE214h
  004ae205  8b06                  mov       eax,[esi]
  004ae207  66c746120100          mov       word [esi+12h],1
  004ae20d  25fffffffe            and       eax,0FEFFFFFFh

LAB_004ae212:
  004ae212  8906                  mov       [esi],eax

LAB_004ae214:
  004ae214  0fbf5632              movsx     edx,word [esi+32h]
  004ae218  52                    push      edx
  004ae219  e8fe770900            call      00545A1Ch
  004ae21e  8944246c              mov       [esp+6Ch],eax
  004ae222  0fbf4632              movsx     eax,word [esi+32h]
  004ae226  50                    push      eax
  004ae227  e8ea770900            call      00545A16h
  004ae22c  8b5650                mov       edx,[esi+50h]
  004ae22f  89442474              mov       [esp+74h],eax
  004ae233  8bc2                  mov       eax,edx
  004ae235  8b6e54                mov       ebp,[esi+54h]
  004ae238  c1f808                sar       eax,8
  004ae23b  8bfa                  mov       edi,edx
  004ae23d  8b4c241c              mov       ecx,[esp+1Ch]
  004ae241  2bf8                  sub       edi,eax
  004ae243  0faf4e5c              imul      ecx,[esi+5Ch]
  004ae247  c1fa03                sar       edx,3
  004ae24a  2bfa                  sub       edi,edx
  004ae24c  8bdd                  mov       ebx,ebp
  004ae24e  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004ae255  8bc2                  mov       eax,edx
  004ae257  897e50                mov       [esi+50h],edi
  004ae25a  c1f808                sar       eax,8
  004ae25d  c1fb08                sar       ebx,8
  004ae260  2bc3                  sub       eax,ebx
  004ae262  56                    push      esi
  004ae263  2bc2                  sub       eax,edx
  004ae265  8b5658                mov       edx,[esi+58h]
  004ae268  8bda                  mov       ebx,edx
  004ae26a  03c5                  add       eax,ebp
  004ae26c  c1fb03                sar       ebx,3
  004ae26f  8bea                  mov       ebp,edx
  004ae271  894654                mov       [esi+54h],eax
  004ae274  2beb                  sub       ebp,ebx
  004ae276  8b5e1c                mov       ebx,[esi+1Ch]
  004ae279  c1fa08                sar       edx,8
  004ae27c  c1f90c                sar       ecx,0Ch
  004ae27f  2bea                  sub       ebp,edx
  004ae281  8bd1                  mov       edx,ecx
  004ae283  0faf542470            imul      edx,[esp+70h]
  004ae288  0faf4c2478            imul      ecx,[esp+78h]
  004ae28d  c1fa0c                sar       edx,0Ch
  004ae290  c1f90c                sar       ecx,0Ch
  004ae293  896e58                mov       [esi+58h],ebp
  004ae296  03d7                  add       edx,edi
  004ae298  8b7e20                mov       edi,[esi+20h]
  004ae29b  03cd                  add       ecx,ebp
  004ae29d  8b6e24                mov       ebp,[esi+24h]
  004ae2a0  03da                  add       ebx,edx
  004ae2a2  03f8                  add       edi,eax
  004ae2a4  03e9                  add       ebp,ecx
  004ae2a6  895e1c                mov       [esi+1Ch],ebx
  004ae2a9  897e20                mov       [esi+20h],edi
  004ae2ac  896e24                mov       [esi+24h],ebp
  004ae2af  e88c560800            call      00533940h
  004ae2b4  8b9690000000          mov       edx,[esi+90h]
  004ae2ba  8b4660                mov       eax,[esi+60h]
  004ae2bd  8b4e20                mov       ecx,[esi+20h]
  004ae2c0  83c404                add       esp,4
  004ae2c3  80e6fc                and       dh,0FCh
  004ae2c6  3bc8                  cmp       ecx,eax
  004ae2c8  899690000000          mov       [esi+90h],edx
  004ae2ce  7f2a                  jg        short 004AE2FAh
  004ae2d0  f6c680                test      dh,80h
  004ae2d3  7525                  jne       short 004AE2FAh
  004ae2d5  8b0e                  mov       ecx,[esi]
  004ae2d7  894620                mov       [esi+20h],eax
  004ae2da  f6c101                test      cl,1
  004ae2dd  740f                  je        short 004AE2EEh
  004ae2df  83e1fe                and       ecx,0FFFFFFFEh
  004ae2e2  33db                  xor       ebx,ebx
  004ae2e4  890e                  mov       [esi],ecx
  004ae2e6  895e54                mov       [esi+54h],ebx
  004ae2e9  e925010000            jmp       004AE413h

LAB_004ae2ee:
  004ae2ee  83e1fe                and       ecx,0FFFFFFFEh
  004ae2f1  33db                  xor       ebx,ebx
  004ae2f3  890e                  mov       [esi],ecx
  004ae2f5  895e54                mov       [esi+54h],ebx
  004ae2f8  eb02                  jmp       short 004AE2FCh

LAB_004ae2fa:
  004ae2fa  33db                  xor       ebx,ebx

LAB_004ae2fc:
  004ae2fc  8b0e                  mov       ecx,[esi]
  004ae2fe  f6c101                test      cl,1
  004ae301  750a                  jne       short 004AE30Dh
  004ae303  394620                cmp       [esi+20h],eax
  004ae306  7e05                  jle       short 004AE30Dh
  004ae308  83c901                or        ecx,1
  004ae30b  890e                  mov       [esi],ecx

LAB_004ae30d:
  004ae30d  f7c282ad0020          test      edx,2000AD82h
  004ae313  0f84fa000000          je        near 004AE413h
  004ae319  f60601                test      byte [esi],1
  004ae31c  0f85c9000000          jne       near 004AE3EBh
  004ae322  8a4e42                mov       cl,[esi+42h]
  004ae325  b007                  mov       al,7
  004ae327  3ac8                  cmp       cl,al
  004ae329  7420                  je        short 004AE34Bh
  004ae32b  884642                mov       [esi+42h],al
  004ae32e  e80d8e0900            call      00547140h
  004ae333  99                    cdq
  004ae334  b928000000            mov       ecx,28h
  004ae339  66895e12              mov       [esi+12h],bx
  004ae33d  f7f9                  idiv      ecx
  004ae33f  66c746105000          mov       word [esi+10h],50h
  004ae345  80c214                add       dl,14h
  004ae348  885643                mov       [esi+43h],dl

LAB_004ae34b:
  004ae34b  8b16                  mov       edx,[esi]
  004ae34d  8b442428              mov       eax,[esp+28h]
  004ae351  8b4c242c              mov       ecx,[esp+2Ch]
  004ae355  83e2fd                and       edx,0FFFFFFFDh
  004ae358  8916                  mov       [esi],edx
  004ae35a  8b542424              mov       edx,[esp+24h]
  004ae35e  894620                mov       [esi+20h],eax
  004ae361  8b442448              mov       eax,[esp+48h]
  004ae365  89561c                mov       [esi+1Ch],edx
  004ae368  8b54245c              mov       edx,[esp+5Ch]
  004ae36c  25ffff0000            and       eax,0FFFFh
  004ae371  894e24                mov       [esi+24h],ecx
  004ae374  8b4c243c              mov       ecx,[esp+3Ch]
  004ae378  89567c                mov       [esi+7Ch],edx
  004ae37b  8b542458              mov       edx,[esp+58h]
  004ae37f  898690000000          mov       [esi+90h],eax
  004ae385  8b44244c              mov       eax,[esp+4Ch]
  004ae389  898e84000000          mov       [esi+84h],ecx
  004ae38f  8b4c2444              mov       ecx,[esp+44h]
  004ae393  898680000000          mov       [esi+80h],eax
  004ae399  8b442450              mov       eax,[esp+50h]
  004ae39d  899688000000          mov       [esi+88h],edx
  004ae3a3  8b542440              mov       edx,[esp+40h]
  004ae3a7  6882ad0020            push      2000AD82h
  004ae3ac  56                    push      esi
  004ae3ad  89868c000000          mov       [esi+8Ch],eax
  004ae3b3  894e64                mov       [esi+64h],ecx
  004ae3b6  895660                mov       [esi+60h],edx
  004ae3b9  e8a27a0200            call      004D5E60h
  004ae3be  83c408                add       esp,8
  004ae3c1  663bc3                cmp       ax,bx
  004ae3c4  7515                  jne       short 004AE3DBh
  004ae3c6  e8758d0900            call      00547140h
  004ae3cb  99                    cdq
  004ae3cc  33c2                  xor       eax,edx
  004ae3ce  2bc2                  sub       eax,edx
  004ae3d0  83e001                and       eax,1
  004ae3d3  33c2                  xor       eax,edx
  004ae3d5  2bc2                  sub       eax,edx
  004ae3d7  8d4400ff              lea       eax,[eax+eax-1]

LAB_004ae3db:
  004ae3db  8b4c2410              mov       ecx,[esp+10h]
  004ae3df  83c140                add       ecx,40h
  004ae3e2  0fafc8                imul      ecx,eax
  004ae3e5  66014e32              add       [esi+32h],cx
  004ae3e9  eb28                  jmp       short 004AE413h

LAB_004ae3eb:
  004ae3eb  8b542424              mov       edx,[esp+24h]
  004ae3ef  8b44242c              mov       eax,[esp+2Ch]
  004ae3f3  895e5c                mov       [esi+5Ch],ebx
  004ae3f6  895e50                mov       [esi+50h],ebx
  004ae3f9  895e58                mov       [esi+58h],ebx
  004ae3fc  89561c                mov       [esi+1Ch],edx
  004ae3ff  894624                mov       [esi+24h],eax
  004ae402  885e42                mov       [esi+42h],bl
  004ae405  c6464328              mov       byte [esi+43h],28h
  004ae409  66895e12              mov       [esi+12h],bx
  004ae40d  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004ae413:
  004ae413  6aff                  push      0FFFFFFFFh
  004ae415  6aff                  push      0FFFFFFFFh
  004ae417  6aff                  push      0FFFFFFFFh
  004ae419  56                    push      esi
  004ae41a  e8f1530100            call      004C3810h
  004ae41f  8b44242c              mov       eax,[esp+2Ch]
  004ae423  83c410                add       esp,10h
  004ae426  3bc3                  cmp       eax,ebx
  004ae428  7517                  jne       short 004AE441h
  004ae42a  668b8c2482000000      mov       cx,[esp+82h]
  004ae432  66894e32              mov       [esi+32h],cx
  004ae436  5f                    pop       edi
  004ae437  5e                    pop       esi
  004ae438  5d                    pop       ebp
  004ae439  5b                    pop       ebx
  004ae43a  81c484000000          add       esp,84h
  004ae440  c3                    ret

LAB_004ae441:
  004ae441  385e43                cmp       [esi+43h],bl
  004ae444  0f8550030000          jne       near 004AE79Ah
  004ae44a  8b466c                mov       eax,[esi+6Ch]
  004ae44d  8b561c                mov       edx,[esi+1Ch]
  004ae450  8b781c                mov       edi,[eax+1Ch]
  004ae453  8b6824                mov       ebp,[eax+24h]
  004ae456  8b4624                mov       eax,[esi+24h]
  004ae459  2bfa                  sub       edi,edx
  004ae45b  2be8                  sub       ebp,eax
  004ae45d  c1ff0c                sar       edi,0Ch
  004ae460  c1fd0c                sar       ebp,0Ch
  004ae463  8bd7                  mov       edx,edi
  004ae465  8bc5                  mov       eax,ebp
  004ae467  0fafd7                imul      edx,edi
  004ae46a  0fafc5                imul      eax,ebp
  004ae46d  03d0                  add       edx,eax
  004ae46f  52                    push      edx
  004ae470  e89b750900            call      00545A10h
  004ae475  55                    push      ebp
  004ae476  57                    push      edi
  004ae477  8944241c              mov       [esp+1Ch],eax
  004ae47b  e8a2750900            call      00545A22h
  004ae480  668b4e32              mov       cx,[esi+32h]
  004ae484  66895e12              mov       [esi+12h],bx
  004ae488  2bc1                  sub       eax,ecx
  004ae48a  25ff0f0000            and       eax,0FFFh
  004ae48f  89442418              mov       [esp+18h],eax
  004ae493  8b442414              mov       eax,[esp+14h]
  004ae497  3dfa000000            cmp       eax,0FAh
  004ae49c  0f8ddf000000          jge       near 004AE581h
  004ae4a2  e8998c0900            call      00547140h
  004ae4a7  99                    cdq
  004ae4a8  b964000000            mov       ecx,64h
  004ae4ad  f7f9                  idiv      ecx
  004ae4af  8bea                  mov       ebp,edx
  004ae4b1  83fd32                cmp       ebp,32h
  004ae4b4  0f8d8d000000          jge       near 004AE547h
  004ae4ba  8b542418              mov       edx,[esp+18h]
  004ae4be  8b7c2420              mov       edi,[esp+20h]
  004ae4c2  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004ae4c9  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004ae4d0  25ff0f0000            and       eax,0FFFh
  004ae4d5  3bc1                  cmp       eax,ecx
  004ae4d7  7f64                  jg        short 004AE53Dh
  004ae4d9  e8628c0900            call      00547140h
  004ae4de  99                    cdq
  004ae4df  8d4c3f01              lea       ecx,[edi+edi+1]
  004ae4e3  f7f9                  idiv      ecx
  004ae4e5  8b442414              mov       eax,[esp+14h]
  004ae4e9  83c264                add       edx,64h
  004ae4ec  3bc2                  cmp       eax,edx
  004ae4ee  7d4d                  jge       short 004AE53Dh
  004ae4f0  83fd1e                cmp       ebp,1Eh
  004ae4f3  7d24                  jge       short 004AE519h
  004ae4f5  8b16                  mov       edx,[esi]
  004ae4f7  c646420b              mov       byte [esi+42h],0Bh
  004ae4fb  81e2fffffffe          and       edx,0FEFFFFFFh
  004ae501  c6464301              mov       byte [esi+43h],1
  004ae505  83ca02                or        edx,2
  004ae508  895e5c                mov       [esi+5Ch],ebx
  004ae50b  8916                  mov       [esi],edx
  004ae50d  66c746180300          mov       word [esi+18h],3
  004ae513  66895e1a              mov       [esi+1Ah],bx
  004ae517  eb6c                  jmp       short 004AE585h

LAB_004ae519:
  004ae519  885e42                mov       [esi+42h],bl
  004ae51c  e81f8c0900            call      00547140h
  004ae521  99                    cdq
  004ae522  b928000000            mov       ecx,28h
  004ae527  66c746105000          mov       word [esi+10h],50h
  004ae52d  f7f9                  idiv      ecx
  004ae52f  8bc7                  mov       eax,edi
  004ae531  d1f8                  sar       eax,1
  004ae533  02d0                  add       dl,al
  004ae535  80c214                add       dl,14h
  004ae538  885643                mov       [esi+43h],dl
  004ae53b  eb48                  jmp       short 004AE585h

LAB_004ae53d:
  004ae53d  c6464201              mov       byte [esi+42h],1
  004ae541  c6464301              mov       byte [esi+43h],1
  004ae545  eb3e                  jmp       short 004AE585h

LAB_004ae547:
  004ae547  83fd3c                cmp       ebp,3Ch
  004ae54a  0f8c55020000          jl        near 004AE7A5h
  004ae550  83fd46                cmp       ebp,46h
  004ae553  0f8d4c020000          jge       near 004AE7A5h
  004ae559  8b4c2410              mov       ecx,[esp+10h]
  004ae55d  8b442414              mov       eax,[esp+14h]
  004ae561  83c164                add       ecx,64h
  004ae564  3bc1                  cmp       eax,ecx
  004ae566  0f8e39020000          jle       near 004AE7A5h
  004ae56c  c6464206              mov       byte [esi+42h],6
  004ae570  c6464301              mov       byte [esi+43h],1
  004ae574  e8c78b0900            call      00547140h
  004ae579  66894610              mov       [esi+10h],ax
  004ae57d  66895e12              mov       [esi+12h],bx

LAB_004ae581:
  004ae581  8b7c2420              mov       edi,[esp+20h]

LAB_004ae585:
  004ae585  8b442414              mov       eax,[esp+14h]
  004ae589  3d58020000            cmp       eax,258h
  004ae58e  0f8de2000000          jge       near 004AE676h
  004ae594  3dfa000000            cmp       eax,0FAh
  004ae599  0f8cd7000000          jl        near 004AE676h
  004ae59f  e89c8b0900            call      00547140h
  004ae5a4  99                    cdq
  004ae5a5  b964000000            mov       ecx,64h
  004ae5aa  f7f9                  idiv      ecx
  004ae5ac  83fa14                cmp       edx,14h
  004ae5af  7d25                  jge       short 004AE5D6h
  004ae5b1  8b542418              mov       edx,[esp+18h]
  004ae5b5  81c200040000          add       edx,400h
  004ae5bb  81e2ff0f0000          and       edx,0FFFh
  004ae5c1  81fa00080000          cmp       edx,800h
  004ae5c7  7c67                  jl        short 004AE630h
  004ae5c9  c6464201              mov       byte [esi+42h],1
  004ae5cd  c6464301              mov       byte [esi+43h],1
  004ae5d1  e9a0000000            jmp       004AE676h

LAB_004ae5d6:
  004ae5d6  83fa28                cmp       edx,28h
  004ae5d9  7d25                  jge       short 004AE600h
  004ae5db  c6464202              mov       byte [esi+42h],2
  004ae5df  e85c8b0900            call      00547140h
  004ae5e4  99                    cdq
  004ae5e5  b90a000000            mov       ecx,0Ah
  004ae5ea  66c746105000          mov       word [esi+10h],50h
  004ae5f0  f7f9                  idiv      ecx
  004ae5f2  8bc7                  mov       eax,edi
  004ae5f4  d1f8                  sar       eax,1
  004ae5f6  02d0                  add       dl,al
  004ae5f8  80c214                add       dl,14h
  004ae5fb  885643                mov       [esi+43h],dl
  004ae5fe  eb76                  jmp       short 004AE676h

LAB_004ae600:
  004ae600  83fa46                cmp       edx,46h
  004ae603  7d17                  jge       short 004AE61Ch
  004ae605  c6464206              mov       byte [esi+42h],6
  004ae609  c6464301              mov       byte [esi+43h],1
  004ae60d  e82e8b0900            call      00547140h
  004ae612  66894610              mov       [esi+10h],ax
  004ae616  66895e12              mov       [esi+12h],bx
  004ae61a  eb5a                  jmp       short 004AE676h

LAB_004ae61c:
  004ae61c  8b4c2410              mov       ecx,[esp+10h]
  004ae620  b864000000            mov       eax,64h
  004ae625  d1f9                  sar       ecx,1
  004ae627  83c10a                add       ecx,0Ah
  004ae62a  2bc2                  sub       eax,edx
  004ae62c  3bc1                  cmp       eax,ecx
  004ae62e  7d0e                  jge       short 004AE63Eh

LAB_004ae630:
  004ae630  c646420a              mov       byte [esi+42h],0Ah
  004ae634  c6464301              mov       byte [esi+43h],1
  004ae638  66895e12              mov       [esi+12h],bx
  004ae63c  eb38                  jmp       short 004AE676h

LAB_004ae63e:
  004ae63e  c6464203              mov       byte [esi+42h],3
  004ae642  66895e12              mov       [esi+12h],bx
  004ae646  e8f58a0900            call      00547140h
  004ae64b  99                    cdq
  004ae64c  b90a000000            mov       ecx,0Ah
  004ae651  f7f9                  idiv      ecx
  004ae653  8bc7                  mov       eax,edi
  004ae655  d1f8                  sar       eax,1
  004ae657  02d0                  add       dl,al
  004ae659  80c214                add       dl,14h
  004ae65c  885643                mov       [esi+43h],dl
  004ae65f  e8dc8a0900            call      00547140h
  004ae664  99                    cdq
  004ae665  33c2                  xor       eax,edx
  004ae667  2bc2                  sub       eax,edx
  004ae669  25ff0f0000            and       eax,0FFFh
  004ae66e  33c2                  xor       eax,edx
  004ae670  2bc2                  sub       eax,edx
  004ae672  66894610              mov       [esi+10h],ax

LAB_004ae676:
  004ae676  817c241458020000      cmp       dword [esp+14h],258h
  004ae67e  0f8c16010000          jl        near 004AE79Ah
  004ae684  e8b78a0900            call      00547140h
  004ae689  99                    cdq
  004ae68a  b964000000            mov       ecx,64h
  004ae68f  f7f9                  idiv      ecx
  004ae691  83fa1e                cmp       edx,1Eh
  004ae694  7d35                  jge       short 004AE6CBh
  004ae696  885e42                mov       [esi+42h],bl
  004ae699  e8a28a0900            call      00547140h
  004ae69e  99                    cdq
  004ae69f  b90a000000            mov       ecx,0Ah
  004ae6a4  66895e12              mov       [esi+12h],bx
  004ae6a8  f7f9                  idiv      ecx
  004ae6aa  8b4c2410              mov       ecx,[esp+10h]
  004ae6ae  8bc7                  mov       eax,edi
  004ae6b0  c1f802                sar       eax,2
  004ae6b3  02d0                  add       dl,al
  004ae6b5  885643                mov       [esi+43h],dl
  004ae6b8  8d540950              lea       edx,[ecx+ecx+50h]
  004ae6bc  66895610              mov       [esi+10h],dx
  004ae6c0  5f                    pop       edi
  004ae6c1  5e                    pop       esi
  004ae6c2  5d                    pop       ebp
  004ae6c3  5b                    pop       ebx
  004ae6c4  81c484000000          add       esp,84h
  004ae6ca  c3                    ret

LAB_004ae6cb:
  004ae6cb  83fa28                cmp       edx,28h
  004ae6ce  7d43                  jge       short 004AE713h
  004ae6d0  c6464203              mov       byte [esi+42h],3
  004ae6d4  66895e12              mov       [esi+12h],bx
  004ae6d8  e8638a0900            call      00547140h
  004ae6dd  99                    cdq
  004ae6de  b90a000000            mov       ecx,0Ah
  004ae6e3  f7f9                  idiv      ecx
  004ae6e5  8bc7                  mov       eax,edi
  004ae6e7  d1f8                  sar       eax,1
  004ae6e9  02d0                  add       dl,al
  004ae6eb  80c214                add       dl,14h
  004ae6ee  885643                mov       [esi+43h],dl
  004ae6f1  e84a8a0900            call      00547140h
  004ae6f6  99                    cdq
  004ae6f7  33c2                  xor       eax,edx
  004ae6f9  2bc2                  sub       eax,edx
  004ae6fb  25ff0f0000            and       eax,0FFFh
  004ae700  33c2                  xor       eax,edx
  004ae702  2bc2                  sub       eax,edx
  004ae704  66894610              mov       [esi+10h],ax
  004ae708  5f                    pop       edi
  004ae709  5e                    pop       esi
  004ae70a  5d                    pop       ebp
  004ae70b  5b                    pop       ebx
  004ae70c  81c484000000          add       esp,84h
  004ae712  c3                    ret

LAB_004ae713:
  004ae713  83fa3c                cmp       edx,3Ch
  004ae716  7d13                  jge       short 004AE72Bh
  004ae718  c6464201              mov       byte [esi+42h],1
  004ae71c  c6464301              mov       byte [esi+43h],1
  004ae720  5f                    pop       edi
  004ae721  5e                    pop       esi
  004ae722  5d                    pop       ebp
  004ae723  5b                    pop       ebx
  004ae724  81c484000000          add       esp,84h
  004ae72a  c3                    ret

LAB_004ae72b:
  004ae72b  83fa50                cmp       edx,50h
  004ae72e  7d59                  jge       short 004AE789h
  004ae730  8b4c2410              mov       ecx,[esp+10h]
  004ae734  b864000000            mov       eax,64h
  004ae739  d1f9                  sar       ecx,1
  004ae73b  83c10a                add       ecx,0Ah
  004ae73e  2bc2                  sub       eax,edx
  004ae740  3bc1                  cmp       eax,ecx
  004ae742  7d17                  jge       short 004AE75Bh
  004ae744  c646420a              mov       byte [esi+42h],0Ah
  004ae748  c6464301              mov       byte [esi+43h],1
  004ae74c  66895e12              mov       [esi+12h],bx
  004ae750  5f                    pop       edi
  004ae751  5e                    pop       esi
  004ae752  5d                    pop       ebp
  004ae753  5b                    pop       ebx
  004ae754  81c484000000          add       esp,84h
  004ae75a  c3                    ret

LAB_004ae75b:
  004ae75b  c6464202              mov       byte [esi+42h],2
  004ae75f  e8dc890900            call      00547140h
  004ae764  99                    cdq
  004ae765  b90a000000            mov       ecx,0Ah
  004ae76a  66c746106400          mov       word [esi+10h],64h
  004ae770  f7f9                  idiv      ecx
  004ae772  8bc7                  mov       eax,edi
  004ae774  d1f8                  sar       eax,1
  004ae776  02d0                  add       dl,al
  004ae778  80c214                add       dl,14h
  004ae77b  885643                mov       [esi+43h],dl
  004ae77e  5f                    pop       edi
  004ae77f  5e                    pop       esi
  004ae780  5d                    pop       ebp
  004ae781  5b                    pop       ebx
  004ae782  81c484000000          add       esp,84h
  004ae788  c3                    ret

LAB_004ae789:
  004ae789  c6464206              mov       byte [esi+42h],6
  004ae78d  c6464301              mov       byte [esi+43h],1
  004ae791  e8aa890900            call      00547140h
  004ae796  66894610              mov       [esi+10h],ax

LAB_004ae79a:
  004ae79a  5f                    pop       edi
  004ae79b  5e                    pop       esi
  004ae79c  5d                    pop       ebp
  004ae79d  5b                    pop       ebx
  004ae79e  81c484000000          add       esp,84h
  004ae7a4  c3                    ret

LAB_004ae7a5:
  004ae7a5  c6464203              mov       byte [esi+42h],3
  004ae7a9  66895e12              mov       [esi+12h],bx
  004ae7ad  e88e890900            call      00547140h
  004ae7b2  99                    cdq
  004ae7b3  b90a000000            mov       ecx,0Ah
  004ae7b8  8b7c2420              mov       edi,[esp+20h]
  004ae7bc  f7f9                  idiv      ecx
  004ae7be  8bc7                  mov       eax,edi
  004ae7c0  d1f8                  sar       eax,1
  004ae7c2  02d0                  add       dl,al
  004ae7c4  80c214                add       dl,14h
  004ae7c7  885643                mov       [esi+43h],dl
  004ae7ca  e871890900            call      00547140h
  004ae7cf  99                    cdq
  004ae7d0  33c2                  xor       eax,edx
  004ae7d2  2bc2                  sub       eax,edx
  004ae7d4  25ff0f0000            and       eax,0FFFh
  004ae7d9  33c2                  xor       eax,edx
  004ae7db  2bc2                  sub       eax,edx
  004ae7dd  66894610              mov       [esi+10h],ax
  004ae7e1  e99ffdffff            jmp       004AE585h
  004ae7e6  8bff                  mov       edi,edi
  004ae7e8  4d                    dec       ebp
  004ae7e9  dd4a00                fisttp    qword [edx]
  004ae7ec  97                    xchg      edi,eax
  004ae7ed  dd4a00                fisttp    qword [edx]
  004ae7f0  0dde4a00b6            or        eax,0B6004ADEh
  004ae7f5  de4a00                fimul     word [edx]
  004ae7f8  14e2                  adc       al,0E2h
  004ae7fa  4a                    dec       edx
  004ae7fb  0014e2                add       [edx],dl
  004ae7fe  4a                    dec       edx
  004ae7ff  008cdf4a001fe0        add       [edi+ebx*8-1FE0FFB6h],cl
  004ae806  4a                    dec       edx
  004ae807  0014e2                add       [edx],dl
  004ae80a  4a                    dec       edx
  004ae80b  0014e2                add       [edx],dl
  004ae80e  4a                    dec       edx
  004ae80f  007fe0                add       [edi-20h],bh
  004ae812  4a                    dec       edx
  004ae813  00e6                  add       dh,ah
  004ae815  e14a                  loope     004AE861h
  004ae817  009090909090          add       [eax-6F6F6F70h],dl
  004ae81d  90                    nop
  004ae81e  90                    nop
  004ae81f  90                    nop

67 cible(s) de saut interne : 0x004adb31, 0x004adc80, 0x004adca2, 0x004adca4, 0x004adcb1, 0x004adcb3, 0x004add16, 0x004add1a, 0x004add33, 0x004add38, 0x004add6d, 0x004add7f, 0x004addb8, 0x004addf6, 0x004ade41, 0x004ade55, 0x004ade58, 0x004ade60, 0x004ade6c, 0x004adea4, 0x004adecd, 0x004adf4c, 0x004adf5e, 0x004adf62, 0x004adf9d, 0x004ae00e, 0x004ae03c, 0x004ae048, 0x004ae090, 0x004ae0a1, 0x004ae0c4, 0x004ae0ff, 0x004ae11c, 0x004ae165, 0x004ae193, 0x004ae1cc, 0x004ae1e1, 0x004ae200, 0x004ae212, 0x004ae214, 0x004ae2ee, 0x004ae2fa, 0x004ae2fc, 0x004ae30d, 0x004ae34b, 0x004ae3db, 0x004ae3eb, 0x004ae413, 0x004ae441, 0x004ae519, 0x004ae53d, 0x004ae547, 0x004ae581, 0x004ae585, 0x004ae5d6, 0x004ae600, 0x004ae61c, 0x004ae630, 0x004ae63e, 0x004ae676, 0x004ae6cb, 0x004ae713, 0x004ae72b, 0x004ae75b, 0x004ae789, 0x004ae79a, 0x004ae7a5
