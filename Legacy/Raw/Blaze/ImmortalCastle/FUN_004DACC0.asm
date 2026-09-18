C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004dacc0  section .text  offset fichier 0xda0c0  2229 octets

  004dacc0  83ec20                sub       esp,20h
  004dacc3  8d442414              lea       eax,[esp+14h]
  004dacc7  8d4c2418              lea       ecx,[esp+18h]
  004daccb  8d542410              lea       edx,[esp+10h]
  004daccf  53                    push      ebx
  004dacd0  55                    push      ebp
  004dacd1  56                    push      esi
  004dacd2  8b742430              mov       esi,[esp+30h]
  004dacd6  57                    push      edi
  004dacd7  50                    push      eax
  004dacd8  51                    push      ecx
  004dacd9  52                    push      edx
  004dacda  56                    push      esi
  004dacdb  e8a0dc0500            call      00538980h
  004dace0  83c410                add       esp,10h
  004dace3  8d442418              lea       eax,[esp+18h]
  004dace7  8d4c2414              lea       ecx,[esp+14h]
  004daceb  8d542434              lea       edx,[esp+34h]
  004dacef  50                    push      eax
  004dacf0  51                    push      ecx
  004dacf1  8d442418              lea       eax,[esp+18h]
  004dacf5  52                    push      edx
  004dacf6  8d4c2438              lea       ecx,[esp+38h]
  004dacfa  50                    push      eax
  004dacfb  51                    push      ecx
  004dacfc  56                    push      esi
  004dacfd  e81eb20500            call      00535F20h
  004dad02  8b44242c              mov       eax,[esp+2Ch]
  004dad06  33ed                  xor       ebp,ebp
  004dad08  83c418                add       esp,18h
  004dad0b  3bc5                  cmp       eax,ebp
  004dad0d  bb2c010000            mov       ebx,12Ch
  004dad12  bf01000000            mov       edi,1
  004dad17  0f84e1050000          je        near 004DB2FEh
  004dad1d  56                    push      esi
  004dad1e  e87d080000            call      004DB5A0h
  004dad23  8b06                  mov       eax,[esi]
  004dad25  83c404                add       esp,4
  004dad28  a802                  test      al,2
  004dad2a  0f84bf020000          je        near 004DAFEFh
  004dad30  8a4c2418              mov       cl,[esp+18h]
  004dad34  bb10000000            mov       ebx,10h
  004dad39  84cb                  test      bl,cl
  004dad3b  740d                  je        short 004DAD4Ah
  004dad3d  24fd                  and       al,0FDh
  004dad3f  c6464300              mov       byte [esi+43h],0
  004dad43  8906                  mov       [esi],eax
  004dad45  e9b4050000            jmp       004DB2FEh

LAB_004dad4a:
  004dad4a  33c9                  xor       ecx,ecx
  004dad4c  8a4e42                mov       cl,[esi+42h]
  004dad4f  2bcd                  sub       ecx,ebp
  004dad51  0f843f010000          je        near 004DAE96h
  004dad57  49                    dec       ecx
  004dad58  7477                  je        short 004DADD1h
  004dad5a  49                    dec       ecx
  004dad5b  0f859d050000          jne       near 004DB2FEh
  004dad61  0fbf4e10              movsx     ecx,word [esi+10h]
  004dad65  2bcd                  sub       ecx,ebp
  004dad67  0f8413010000          je        near 004DAE80h
  004dad6d  49                    dec       ecx
  004dad6e  0f851a010000          jne       near 004DAE8Eh
  004dad74  a900000002            test      eax,2000000h
  004dad79  0f840f010000          je        near 004DAE8Eh
  004dad7f  8b566c                mov       edx,[esi+6Ch]
  004dad82  6aff                  push      0FFFFFFFFh
  004dad84  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004dad8b  66c7869a0000000200    mov       word [esi+9Ah],2
  004dad94  895678                mov       [esi+78h],edx
  004dad97  a1cc975e00            mov       eax,[5E97CCh]
  004dad9c  6aff                  push      0FFFFFFFFh
  004dad9e  8b88cc000000          mov       ecx,[eax+0CCh]
  004dada4  33c0                  xor       eax,eax
  004dada6  8b11                  mov       edx,[ecx]
  004dada8  8a420f                mov       al,[edx+0Fh]
  004dadab  05c0000000            add       eax,0C0h
  004dadb0  50                    push      eax
  004dadb1  56                    push      esi
  004dadb2  e819fcf9ff            call      0047A9D0h
  004dadb7  83c410                add       esp,10h
  004dadba  66ff4e10              dec       word [esi+10h]
  004dadbe  6689be9a000000        mov       [esi+9Ah],di
  004dadc5  c646430a              mov       byte [esi+43h],0Ah
  004dadc9  896e5c                mov       [esi+5Ch],ebp
  004dadcc  e92d050000            jmp       004DB2FEh

LAB_004dadd1:
  004dadd1  0fbf4e10              movsx     ecx,word [esi+10h]
  004dadd5  2bcd                  sub       ecx,ebp
  004dadd7  0f84a3000000          je        near 004DAE80h
  004daddd  49                    dec       ecx
  004dadde  0f85aa000000          jne       near 004DAE8Eh
  004dade4  a900000002            test      eax,2000000h
  004dade9  0f849f000000          je        near 004DAE8Eh
  004dadef  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  004dadf6  8b7e50                mov       edi,[esi+50h]
  004dadf9  8b6e54                mov       ebp,[esi+54h]
  004dadfc  8b5e58                mov       ebx,[esi+58h]
  004dadff  6a00                  push      0
  004dae01  56                    push      esi
  004dae02  894c2424              mov       [esp+24h],ecx
  004dae06  66c7869a0000000200    mov       word [esi+9Ah],2
  004dae0f  e85ca90500            call      00535770h
  004dae14  0fbf10                movsx     edx,word [eax]
  004dae17  c1e20c                shl       edx,0Ch
  004dae1a  895650                mov       [esi+50h],edx
  004dae1d  83c408                add       esp,8
  004dae20  0fbf4802              movsx     ecx,word [eax+2]
  004dae24  c1e10c                shl       ecx,0Ch
  004dae27  894e54                mov       [esi+54h],ecx
  004dae2a  6aff                  push      0FFFFFFFFh
  004dae2c  0fbf5004              movsx     edx,word [eax+4]
  004dae30  c1e20c                shl       edx,0Ch
  004dae33  895658                mov       [esi+58h],edx
  004dae36  a1cc975e00            mov       eax,[5E97CCh]
  004dae3b  6aff                  push      0FFFFFFFFh
  004dae3d  8b88cc000000          mov       ecx,[eax+0CCh]
  004dae43  33c0                  xor       eax,eax
  004dae45  8b11                  mov       edx,[ecx]
  004dae47  8a420b                mov       al,[edx+0Bh]
  004dae4a  05c0000000            add       eax,0C0h
  004dae4f  50                    push      eax
  004dae50  56                    push      esi
  004dae51  e87afbf9ff            call      0047A9D0h
  004dae56  668b4c242c            mov       cx,[esp+2Ch]
  004dae5b  83c410                add       esp,10h
  004dae5e  66ff4e10              dec       word [esi+10h]
  004dae62  896e54                mov       [esi+54h],ebp
  004dae65  33ed                  xor       ebp,ebp
  004dae67  897e50                mov       [esi+50h],edi
  004dae6a  895e58                mov       [esi+58h],ebx
  004dae6d  66898e9a000000        mov       [esi+9Ah],cx
  004dae74  c646430a              mov       byte [esi+43h],0Ah
  004dae78  896e5c                mov       [esi+5Ch],ebp
  004dae7b  e97e040000            jmp       004DB2FEh

LAB_004dae80:
  004dae80  8a4e43                mov       cl,[esi+43h]
  004dae83  fec9                  dec       cl
  004dae85  884e43                mov       [esi+43h],cl
  004dae88  7504                  jne       short 004DAE8Eh
  004dae8a  24fd                  and       al,0FDh
  004dae8c  8906                  mov       [esi],eax

LAB_004dae8e:
  004dae8e  896e5c                mov       [esi+5Ch],ebp
  004dae91  e968040000            jmp       004DB2FEh

LAB_004dae96:
  004dae96  0fbf4610              movsx     eax,word [esi+10h]
  004dae9a  2bc5                  sub       eax,ebp
  004dae9c  745e                  je        short 004DAEFCh
  004dae9e  48                    dec       eax
  004dae9f  0f8559040000          jne       near 004DB2FEh
  004daea5  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004daeaa  896e5c                mov       [esi+5Ch],ebp
  004daead  0f85f6010000          jne       near 004DB0A9h
  004daeb3  0fbf5632              movsx     edx,word [esi+32h]
  004daeb7  52                    push      edx
  004daeb8  c7465c00200200        mov       dword [esi+5Ch],22000h
  004daebf  e858ab0600            call      00545A1Ch
  004daec4  0faf465c              imul      eax,[esi+5Ch]
  004daec8  8b5e50                mov       ebx,[esi+50h]
  004daecb  c1f80c                sar       eax,0Ch
  004daece  03d8                  add       ebx,eax
  004daed0  0fbf4632              movsx     eax,word [esi+32h]
  004daed4  50                    push      eax
  004daed5  895e50                mov       [esi+50h],ebx
  004daed8  e839ab0600            call      00545A16h
  004daedd  0faf465c              imul      eax,[esi+5Ch]
  004daee1  8b4e58                mov       ecx,[esi+58h]
  004daee4  c7465c00500000        mov       dword [esi+5Ch],5000h
  004daeeb  c1f80c                sar       eax,0Ch
  004daeee  03c8                  add       ecx,eax
  004daef0  66ff4e10              dec       word [esi+10h]
  004daef4  894e58                mov       [esi+58h],ecx
  004daef7  e9ad010000            jmp       004DB0A9h

LAB_004daefc:
  004daefc  8b4650                mov       eax,[esi+50h]
  004daeff  8b4e58                mov       ecx,[esi+58h]
  004daf02  c1f80c                sar       eax,0Ch
  004daf05  8bd0                  mov       edx,eax
  004daf07  c1f90c                sar       ecx,0Ch
  004daf0a  0fafd0                imul      edx,eax
  004daf0d  8bc1                  mov       eax,ecx
  004daf0f  0fafc1                imul      eax,ecx
  004daf12  03d0                  add       edx,eax
  004daf14  52                    push      edx
  004daf15  e8f6aa0600            call      00545A10h
  004daf1a  8b0e                  mov       ecx,[esi]
  004daf1c  f7c100000002          test      ecx,2000000h
  004daf22  740a                  je        short 004DAF2Eh
  004daf24  83f808                cmp       eax,8
  004daf27  7f05                  jg        short 004DAF2Eh
  004daf29  83e1fd                and       ecx,0FFFFFFFDh
  004daf2c  890e                  mov       [esi],ecx

LAB_004daf2e:
  004daf2e  83f803                cmp       eax,3
  004daf31  0f8cc7030000          jl        near 004DB2FEh
  004daf37  8b442434              mov       eax,[esp+34h]
  004daf3b  8d4828                lea       ecx,[eax+28h]
  004daf3e  81e1ff0f0000          and       ecx,0FFFh
  004daf44  83f950                cmp       ecx,50h
  004daf47  7e18                  jle       short 004DAF61h
  004daf49  25ff0f0000            and       eax,0FFFh
  004daf4e  33d2                  xor       edx,edx
  004daf50  3d00080000            cmp       eax,800h
  004daf55  0f9dc2                setge     dl
  004daf58  4a                    dec       edx
  004daf59  83e250                and       edx,50h
  004daf5c  83c2d8                add       edx,0FFFFFFD8h
  004daf5f  8bc2                  mov       eax,edx

LAB_004daf61:
  004daf61  66014632              add       [esi+32h],ax
  004daf65  55                    push      ebp
  004daf66  8d461c                lea       eax,[esi+1Ch]
  004daf69  68c02c5500            push      552CC0h
  004daf6e  50                    push      eax
  004daf6f  56                    push      esi
  004daf70  e8bbec0300            call      00519C30h
  004daf75  8bf8                  mov       edi,eax
  004daf77  a1b8e05d00            mov       eax,[5DE0B8h]
  004daf7c  83c410                add       esp,10h
  004daf7f  3bc5                  cmp       eax,ebp
  004daf81  0f8477030000          je        near 004DB2FEh
  004daf87  8b07                  mov       eax,[edi]
  004daf89  57                    push      edi
  004daf8a  0c40                  or        al,40h
  004daf8c  56                    push      esi
  004daf8d  66899f96000000        mov       [edi+96h],bx
  004daf94  8907                  mov       [edi],eax
  004daf96  e8e5eb0500            call      00539B80h
  004daf9b  83c408                add       esp,8
  004daf9e  e89dc10600            call      00547140h
  004dafa3  99                    cdq
  004dafa4  b964000000            mov       ecx,64h
  004dafa9  f7f9                  idiv      ecx
  004dafab  83fa14                cmp       edx,14h
  004dafae  7d0e                  jge       short 004DAFBEh
  004dafb0  55                    push      ebp
  004dafb1  57                    push      edi
  004dafb2  56                    push      esi
  004dafb3  c6460707              mov       byte [esi+7],7
  004dafb7  e864140600            call      0053C420h
  004dafbc  eb11                  jmp       short 004DAFCFh

LAB_004dafbe:
  004dafbe  83fa28                cmp       edx,28h
  004dafc1  7d0f                  jge       short 004DAFD2h
  004dafc3  55                    push      ebp
  004dafc4  57                    push      edi
  004dafc5  56                    push      esi
  004dafc6  c6460707              mov       byte [esi+7],7
  004dafca  e8f1030600            call      0053B3C0h

LAB_004dafcf:
  004dafcf  83c40c                add       esp,0Ch

LAB_004dafd2:
  004dafd2  83c71c                add       edi,1Ch
  004dafd5  57                    push      edi
  004dafd6  6800180000            push      1800h
  004dafdb  6800030000            push      300h
  004dafe0  6a03                  push      3
  004dafe2  e8392b0600            call      0053DB20h
  004dafe7  83c410                add       esp,10h
  004dafea  e90f030000            jmp       004DB2FEh

LAB_004dafef:
  004dafef  a810                  test      al,10h
  004daff1  0f8507030000          jne       near 004DB2FEh
  004daff7  a904080000            test      eax,804h
  004daffc  0f842f010000          je        near 004DB131h
  004db002  f7442418e8030040      test      dword [esp+18h],400003E8h
  004db00a  740b                  je        short 004DB017h
  004db00c  25fbf7ffff            and       eax,0FFFFF7FBh
  004db011  c6464300              mov       byte [esi+43h],0
  004db015  8906                  mov       [esi],eax

LAB_004db017:
  004db017  668b4610              mov       ax,[esi+10h]
  004db01b  0fbfc8                movsx     ecx,ax
  004db01e  83f903                cmp       ecx,3
  004db021  0f87d7020000          ja        near 004DB2FEh
  004db027  ff248d78b54d00        jmp       dword [ecx*4+4DB578h]
  004db02e  8b06                  mov       eax,[esi]
  004db030  a900000002            test      eax,2000000h
  004db035  0f84c3020000          je        near 004DB2FEh
  004db03b  80e4f7                and       ah,0F7h
  004db03e  8906                  mov       [esi],eax
  004db040  e9b9020000            jmp       004DB2FEh
  004db045  b806000000            mov       eax,6
  004db04a  66394618              cmp       [esi+18h],ax
  004db04e  7411                  je        short 004DB061h
  004db050  894618                mov       [esi+18h],eax
  004db053  8b06                  mov       eax,[esi]
  004db055  25fbfffffe            and       eax,0FEFFFFFBh
  004db05a  8906                  mov       [esi],eax
  004db05c  e99d020000            jmp       004DB2FEh

LAB_004db061:
  004db061  66837e1a1d            cmp       word [esi+1Ah],1Dh
  004db066  0f8592020000          jne       near 004DB2FEh
  004db06c  33c0                  xor       eax,eax
  004db06e  6aff                  push      0FFFFFFFFh
  004db070  668b4606              mov       ax,[esi+6]
  004db074  6aff                  push      0FFFFFFFFh
  004db076  8bd0                  mov       edx,eax
  004db078  c1f808                sar       eax,8
  004db07b  81e2ff000000          and       edx,0FFh
  004db081  25ff000000            and       eax,0FFh
  004db086  52                    push      edx
  004db087  50                    push      eax
  004db088  56                    push      esi
  004db089  e8f2f80500            call      0053A980h
  004db08e  83c414                add       esp,14h
  004db091  66ff4e10              dec       word [esi+10h]
  004db095  e964020000            jmp       004DB2FEh
  004db09a  8a4e43                mov       cl,[esi+43h]
  004db09d  fec9                  dec       cl
  004db09f  884e43                mov       [esi+43h],cl
  004db0a2  7505                  jne       short 004DB0A9h
  004db0a4  48                    dec       eax
  004db0a5  66894610              mov       [esi+10h],ax

LAB_004db0a9:
  004db0a9  8b442434              mov       eax,[esp+34h]
  004db0ad  8d8880000000          lea       ecx,[eax+80h]
  004db0b3  81e1ff0f0000          and       ecx,0FFFh
  004db0b9  81f900010000          cmp       ecx,100h
  004db0bf  0f8e35020000          jle       near 004DB2FAh
  004db0c5  e915020000            jmp       004DB2DFh
  004db0ca  55                    push      ebp
  004db0cb  55                    push      ebp
  004db0cc  68bd000000            push      0BDh
  004db0d1  56                    push      esi
  004db0d2  e809faf9ff            call      0047AAE0h
  004db0d7  83c410                add       esp,10h
  004db0da  e861c00600            call      00547140h
  004db0df  99                    cdq
  004db0e0  33c2                  xor       eax,edx
  004db0e2  33c9                  xor       ecx,ecx
  004db0e4  2bc2                  sub       eax,edx
  004db0e6  83e003                and       eax,3
  004db0e9  33c2                  xor       eax,edx
  004db0eb  2bc2                  sub       eax,edx
  004db0ed  668b0c45d02c5500      mov       cx,[eax*2+552CD0h]
  004db0f5  66894e06              mov       [esi+6],cx
  004db0f9  8b15c4975e00          mov       edx,[5E97C4h]
  004db0ff  8bc1                  mov       eax,ecx
  004db101  81e1ff000000          and       ecx,0FFh
  004db107  8b929c000000          mov       edx,[edx+9Ch]
  004db10d  c1f808                sar       eax,8
  004db110  25ff000000            and       eax,0FFh
  004db115  8d0c49                lea       ecx,[ecx+ecx*2]
  004db118  c1e104                shl       ecx,4
  004db11b  8b1482                mov       edx,[edx+eax*4]
  004db11e  8a440a13              mov       al,[edx+ecx+13h]
  004db122  66ff4e10              dec       word [esi+10h]
  004db126  884643                mov       [esi+43h],al
  004db129  896e5c                mov       [esi+5Ch],ebp
  004db12c  e9cd010000            jmp       004DB2FEh

LAB_004db131:
  004db131  33c9                  xor       ecx,ecx
  004db133  8a4e42                mov       cl,[esi+42h]
  004db136  83f903                cmp       ecx,3
  004db139  0f87bf010000          ja        near 004DB2FEh
  004db13f  ff248d88b54d00        jmp       dword [ecx*4+4DB588h]
  004db146  66396e18              cmp       [esi+18h],bp
  004db14a  740a                  je        short 004DB156h
  004db14c  0d00000001            or        eax,1000000h
  004db151  896e18                mov       [esi+18h],ebp
  004db154  8906                  mov       [esi],eax

LAB_004db156:
  004db156  395c2410              cmp       [esp+10h],ebx
  004db15a  7d09                  jge       short 004DB165h
  004db15c  c6464300              mov       byte [esi+43h],0
  004db160  e999010000            jmp       004DB2FEh

LAB_004db165:
  004db165  8a4643                mov       al,[esi+43h]
  004db168  84c0                  test      al,al
  004db16a  7405                  je        short 004DB171h
  004db16c  fec8                  dec       al
  004db16e  884643                mov       [esi+43h],al

LAB_004db171:
  004db171  8b465c                mov       eax,[esi+5Ch]
  004db174  8bc8                  mov       ecx,eax
  004db176  c1f904                sar       ecx,4
  004db179  2bc1                  sub       eax,ecx
  004db17b  89465c                mov       [esi+5Ch],eax
  004db17e  e97b010000            jmp       004DB2FEh
  004db183  66396e18              cmp       [esi+18h],bp
  004db187  740a                  je        short 004DB193h
  004db189  0d00000001            or        eax,1000000h
  004db18e  896e18                mov       [esi+18h],ebp
  004db191  8906                  mov       [esi],eax

LAB_004db193:
  004db193  8b442434              mov       eax,[esp+34h]
  004db197  8d9080000000          lea       edx,[eax+80h]
  004db19d  81e2ff0f0000          and       edx,0FFFh
  004db1a3  81fa00010000          cmp       edx,100h
  004db1a9  7f1a                  jg        short 004DB1C5h
  004db1ab  66014632              add       [esi+32h],ax
  004db1af  8b465c                mov       eax,[esi+5Ch]
  004db1b2  8bd0                  mov       edx,eax
  004db1b4  c6464300              mov       byte [esi+43h],0
  004db1b8  c1fa04                sar       edx,4
  004db1bb  2bc2                  sub       eax,edx
  004db1bd  89465c                mov       [esi+5Ch],eax
  004db1c0  e939010000            jmp       004DB2FEh

LAB_004db1c5:
  004db1c5  25ff0f0000            and       eax,0FFFh
  004db1ca  33c9                  xor       ecx,ecx
  004db1cc  3d00080000            cmp       eax,800h
  004db1d1  0f9dc1                setge     cl
  004db1d4  49                    dec       ecx
  004db1d5  81e100010000          and       ecx,100h
  004db1db  83c180                add       ecx,0FFFFFF80h
  004db1de  8bc1                  mov       eax,ecx
  004db1e0  66014632              add       [esi+32h],ax
  004db1e4  8b465c                mov       eax,[esi+5Ch]
  004db1e7  8bd0                  mov       edx,eax
  004db1e9  c1fa04                sar       edx,4
  004db1ec  2bc2                  sub       eax,edx
  004db1ee  89465c                mov       [esi+5Ch],eax
  004db1f1  e908010000            jmp       004DB2FEh
  004db1f6  66397e18              cmp       [esi+18h],di
  004db1fa  740a                  je        short 004DB206h
  004db1fc  0d00000001            or        eax,1000000h
  004db201  897e18                mov       [esi+18h],edi
  004db204  8906                  mov       [esi],eax

LAB_004db206:
  004db206  395c2410              cmp       [esp+10h],ebx
  004db20a  7d09                  jge       short 004DB215h
  004db20c  c6464300              mov       byte [esi+43h],0
  004db210  e9e9000000            jmp       004DB2FEh

LAB_004db215:
  004db215  8a4643                mov       al,[esi+43h]
  004db218  84c0                  test      al,al
  004db21a  7405                  je        short 004DB221h
  004db21c  fec8                  dec       al
  004db21e  884643                mov       [esi+43h],al

LAB_004db221:
  004db221  8b442434              mov       eax,[esp+34h]
  004db225  8d8880000000          lea       ecx,[eax+80h]
  004db22b  81e1ff0f0000          and       ecx,0FFFh
  004db231  81f900010000          cmp       ecx,100h
  004db237  7e1b                  jle       short 004DB254h
  004db239  25ff0f0000            and       eax,0FFFh
  004db23e  33d2                  xor       edx,edx
  004db240  3d00080000            cmp       eax,800h
  004db245  0f9dc2                setge     dl
  004db248  4a                    dec       edx
  004db249  81e200010000          and       edx,100h
  004db24f  83c280                add       edx,0FFFFFF80h
  004db252  8bc2                  mov       eax,edx

LAB_004db254:
  004db254  66014632              add       [esi+32h],ax
  004db258  8b465c                mov       eax,[esi+5Ch]
  004db25b  b900000100            mov       ecx,10000h
  004db260  2bc8                  sub       ecx,eax
  004db262  c1f904                sar       ecx,4
  004db265  03c8                  add       ecx,eax
  004db267  894e5c                mov       [esi+5Ch],ecx
  004db26a  e98f000000            jmp       004DB2FEh
  004db26f  66396e18              cmp       [esi+18h],bp
  004db273  740a                  je        short 004DB27Fh
  004db275  0d00000001            or        eax,1000000h
  004db27a  896e18                mov       [esi+18h],ebp
  004db27d  8906                  mov       [esi],eax

LAB_004db27f:
  004db27f  8a4643                mov       al,[esi+43h]
  004db282  84c0                  test      al,al
  004db284  7405                  je        short 004DB28Bh
  004db286  fec8                  dec       al
  004db288  884643                mov       [esi+43h],al

LAB_004db28b:
  004db28b  0fbf5610              movsx     edx,word [esi+10h]
  004db28f  52                    push      edx
  004db290  c7465c00400000        mov       dword [esi+5Ch],4000h
  004db297  e880a70600            call      00545A1Ch
  004db29c  0faf465c              imul      eax,[esi+5Ch]
  004db2a0  8b7e50                mov       edi,[esi+50h]
  004db2a3  c1f80c                sar       eax,0Ch
  004db2a6  03f8                  add       edi,eax
  004db2a8  0fbf4610              movsx     eax,word [esi+10h]
  004db2ac  50                    push      eax
  004db2ad  897e50                mov       [esi+50h],edi
  004db2b0  e861a70600            call      00545A16h
  004db2b5  0faf465c              imul      eax,[esi+5Ch]
  004db2b9  8b5e58                mov       ebx,[esi+58h]
  004db2bc  896e5c                mov       [esi+5Ch],ebp
  004db2bf  c1f80c                sar       eax,0Ch
  004db2c2  03d8                  add       ebx,eax
  004db2c4  8b442434              mov       eax,[esp+34h]
  004db2c8  895e58                mov       [esi+58h],ebx
  004db2cb  8d8880000000          lea       ecx,[eax+80h]
  004db2d1  81e1ff0f0000          and       ecx,0FFFh
  004db2d7  81f900010000          cmp       ecx,100h
  004db2dd  7e1b                  jle       short 004DB2FAh

LAB_004db2df:
  004db2df  25ff0f0000            and       eax,0FFFh
  004db2e4  33d2                  xor       edx,edx
  004db2e6  3d00080000            cmp       eax,800h
  004db2eb  0f9dc2                setge     dl
  004db2ee  4a                    dec       edx
  004db2ef  81e200010000          and       edx,100h
  004db2f5  83c280                add       edx,0FFFFFF80h
  004db2f8  8bc2                  mov       eax,edx

LAB_004db2fa:
  004db2fa  66014632              add       [esi+32h],ax

LAB_004db2fe:
  004db2fe  8b442414              mov       eax,[esp+14h]
  004db302  50                    push      eax
  004db303  56                    push      esi
  004db304  e8d7f5fbff            call      0049A8E0h
  004db309  83c408                add       esp,8
  004db30c  56                    push      esi
  004db30d  e82e860500            call      00533940h
  004db312  8b9690000000          mov       edx,[esi+90h]
  004db318  8b466c                mov       eax,[esi+6Ch]
  004db31b  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  004db321  83c404                add       esp,4
  004db324  899690000000          mov       [esi+90h],edx
  004db32a  8a08                  mov       cl,[eax]
  004db32c  f6c101                test      cl,1
  004db32f  7506                  jne       short 004DB337h
  004db331  8b4860                mov       ecx,[eax+60h]
  004db334  894e48                mov       [esi+48h],ecx

LAB_004db337:
  004db337  e804be0600            call      00547140h
  004db33c  8b7e60                mov       edi,[esi+60h]
  004db33f  25ff000000            and       eax,0FFh
  004db344  0500010000            add       eax,100h
  004db349  66014614              add       [esi+14h],ax
  004db34d  8b4648                mov       eax,[esi+48h]
  004db350  668b4e14              mov       cx,[esi+14h]
  004db354  3bc7                  cmp       eax,edi
  004db356  7c02                  jl        short 004DB35Ah
  004db358  8bf8                  mov       edi,eax

LAB_004db35a:
  004db35a  f60602                test      byte [esi],2
  004db35d  7415                  je        short 004DB374h
  004db35f  8b4620                mov       eax,[esi+20h]
  004db362  2bf8                  sub       edi,eax
  004db364  81c700800200          add       edi,28000h
  004db36a  c1ff03                sar       edi,3
  004db36d  03f8                  add       edi,eax
  004db36f  897e20                mov       [esi+20h],edi
  004db372  eb22                  jmp       short 004DB396h

LAB_004db374:
  004db374  8b6e20                mov       ebp,[esi+20h]
  004db377  0fbfd1                movsx     edx,cx
  004db37a  52                    push      edx
  004db37b  e89ca60600            call      00545A1Ch
  004db380  0500410000            add       eax,4100h
  004db385  c1e005                shl       eax,5
  004db388  03c7                  add       eax,edi
  004db38a  2bc5                  sub       eax,ebp
  004db38c  c1f804                sar       eax,4
  004db38f  03c5                  add       eax,ebp
  004db391  33ed                  xor       ebp,ebp
  004db393  894620                mov       [esi+20h],eax

LAB_004db396:
  004db396  8b442420              mov       eax,[esp+20h]
  004db39a  50                    push      eax
  004db39b  56                    push      esi
  004db39c  e84f69fcff            call      004A1CF0h
  004db3a1  8b44241c              mov       eax,[esp+1Ch]
  004db3a5  83c408                add       esp,8
  004db3a8  3bc5                  cmp       eax,ebp
  004db3aa  0f84be010000          je        near 004DB56Eh
  004db3b0  f70616080000          test      dword [esi],816h
  004db3b6  0f85b2010000          jne       near 004DB56Eh
  004db3bc  8a4643                mov       al,[esi+43h]
  004db3bf  84c0                  test      al,al
  004db3c1  0f85a7010000          jne       near 004DB56Eh
  004db3c7  8b466c                mov       eax,[esi+6Ch]
  004db3ca  8b5e1c                mov       ebx,[esi+1Ch]
  004db3cd  8b4e24                mov       ecx,[esi+24h]
  004db3d0  8b781c                mov       edi,[eax+1Ch]
  004db3d3  8b6824                mov       ebp,[eax+24h]
  004db3d6  2bfb                  sub       edi,ebx
  004db3d8  2be9                  sub       ebp,ecx
  004db3da  c1ff0c                sar       edi,0Ch
  004db3dd  c1fd0c                sar       ebp,0Ch
  004db3e0  8bcf                  mov       ecx,edi
  004db3e2  8bd5                  mov       edx,ebp
  004db3e4  0fafcf                imul      ecx,edi
  004db3e7  0fafd5                imul      edx,ebp
  004db3ea  03ca                  add       ecx,edx
  004db3ec  51                    push      ecx
  004db3ed  e81ea60600            call      00545A10h
  004db3f2  55                    push      ebp
  004db3f3  57                    push      edi
  004db3f4  89442418              mov       [esp+18h],eax
  004db3f8  e825a60600            call      00545A22h
  004db3fd  668b4e32              mov       cx,[esi+32h]
  004db401  2bc1                  sub       eax,ecx
  004db403  25ff0f0000            and       eax,0FFFh
  004db408  89442434              mov       [esp+34h],eax
  004db40c  e82fbd0600            call      00547140h
  004db411  99                    cdq
  004db412  b964000000            mov       ecx,64h
  004db417  f7f9                  idiv      ecx
  004db419  817c24102c010000      cmp       dword [esp+10h],12Ch
  004db421  7d49                  jge       short 004DB46Ch
  004db423  83fa3c                cmp       edx,3Ch
  004db426  0f8d0b010000          jge       near 004DB537h
  004db42c  8b542434              mov       edx,[esp+34h]
  004db430  81c200010000          add       edx,100h
  004db436  81e2ff0f0000          and       edx,0FFFh
  004db43c  81fa00020000          cmp       edx,200h
  004db442  0f8fbb000000          jg        near 004DB503h
  004db448  8b06                  mov       eax,[esi]
  004db44a  c7461803000000        mov       dword [esi+18h],3
  004db451  25fffffffe            and       eax,0FEFFFFFFh
  004db456  c6464200              mov       byte [esi+42h],0
  004db45a  0c02                  or        al,2
  004db45c  66c746100100          mov       word [esi+10h],1
  004db462  8906                  mov       [esi],eax
  004db464  5f                    pop       edi
  004db465  5e                    pop       esi
  004db466  5d                    pop       ebp
  004db467  5b                    pop       ebx
  004db468  83c420                add       esp,20h
  004db46b  c3                    ret

LAB_004db46c:
  004db46c  83fa1e                cmp       edx,1Eh
  004db46f  0f8d9e000000          jge       near 004DB513h
  004db475  8b442434              mov       eax,[esp+34h]
  004db479  0500010000            add       eax,100h
  004db47e  25ff0f0000            and       eax,0FFFh
  004db483  3d00020000            cmp       eax,200h
  004db488  7f79                  jg        short 004DB503h
  004db48a  8bc2                  mov       eax,edx
  004db48c  b903000000            mov       ecx,3
  004db491  99                    cdq
  004db492  8bf9                  mov       edi,ecx
  004db494  f7ff                  idiv      edi
  004db496  83ea00                sub       edx,0
  004db499  744c                  je        short 004DB4E7h
  004db49b  4a                    dec       edx
  004db49c  7429                  je        short 004DB4C7h
  004db49e  4a                    dec       edx
  004db49f  0f85c9000000          jne       near 004DB56Eh
  004db4a5  8b16                  mov       edx,[esi]
  004db4a7  894e18                mov       [esi+18h],ecx
  004db4aa  81e2fffffffe          and       edx,0FEFFFFFFh
  004db4b0  c6464202              mov       byte [esi+42h],2
  004db4b4  83ca02                or        edx,2
  004db4b7  66c746100100          mov       word [esi+10h],1
  004db4bd  8916                  mov       [esi],edx
  004db4bf  5f                    pop       edi
  004db4c0  5e                    pop       esi
  004db4c1  5d                    pop       ebp
  004db4c2  5b                    pop       ebx
  004db4c3  83c420                add       esp,20h
  004db4c6  c3                    ret

LAB_004db4c7:
  004db4c7  8b06                  mov       eax,[esi]
  004db4c9  894e18                mov       [esi+18h],ecx
  004db4cc  25fffffffe            and       eax,0FEFFFFFFh
  004db4d1  c6464201              mov       byte [esi+42h],1
  004db4d5  0c02                  or        al,2
  004db4d7  66c746100100          mov       word [esi+10h],1
  004db4dd  8906                  mov       [esi],eax
  004db4df  5f                    pop       edi
  004db4e0  5e                    pop       esi
  004db4e1  5d                    pop       ebp
  004db4e2  5b                    pop       ebx
  004db4e3  83c420                add       esp,20h
  004db4e6  c3                    ret

LAB_004db4e7:
  004db4e7  8b06                  mov       eax,[esi]
  004db4e9  c7461805000000        mov       dword [esi+18h],5
  004db4f0  0d04080001            or        eax,1000804h
  004db4f5  66894e10              mov       [esi+10h],cx
  004db4f9  8906                  mov       [esi],eax
  004db4fb  5f                    pop       edi
  004db4fc  5e                    pop       esi
  004db4fd  5d                    pop       ebp
  004db4fe  5b                    pop       ebx
  004db4ff  83c420                add       esp,20h
  004db502  c3                    ret

LAB_004db503:
  004db503  c6464201              mov       byte [esi+42h],1
  004db507  c6464301              mov       byte [esi+43h],1
  004db50b  5f                    pop       edi
  004db50c  5e                    pop       esi
  004db50d  5d                    pop       ebp
  004db50e  5b                    pop       ebx
  004db50f  83c420                add       esp,20h
  004db512  c3                    ret

LAB_004db513:
  004db513  83fa50                cmp       edx,50h
  004db516  7d1f                  jge       short 004DB537h
  004db518  c6464202              mov       byte [esi+42h],2
  004db51c  e81fbc0600            call      00547140h
  004db521  99                    cdq
  004db522  b90a000000            mov       ecx,0Ah
  004db527  f7f9                  idiv      ecx
  004db529  80c214                add       dl,14h
  004db52c  885643                mov       [esi+43h],dl
  004db52f  5f                    pop       edi
  004db530  5e                    pop       esi
  004db531  5d                    pop       ebp
  004db532  5b                    pop       ebx
  004db533  83c420                add       esp,20h
  004db536  c3                    ret

LAB_004db537:
  004db537  c6464203              mov       byte [esi+42h],3
  004db53b  e800bc0600            call      00547140h
  004db540  99                    cdq
  004db541  b90a000000            mov       ecx,0Ah
  004db546  f7f9                  idiv      ecx
  004db548  02d1                  add       dl,cl
  004db54a  885643                mov       [esi+43h],dl
  004db54d  e8eebb0600            call      00547140h
  004db552  99                    cdq
  004db553  b9000c0000            mov       ecx,0C00h
  004db558  8b7c2434              mov       edi,[esp+34h]
  004db55c  f7f9                  idiv      ecx
  004db55e  03d7                  add       edx,edi
  004db560  66035632              add       dx,[esi+32h]
  004db564  81ea00060000          sub       edx,600h
  004db56a  66895610              mov       [esi+10h],dx

LAB_004db56e:
  004db56e  5f                    pop       edi
  004db56f  5e                    pop       esi
  004db570  5d                    pop       ebp
  004db571  5b                    pop       ebx
  004db572  83c420                add       esp,20h

41 cible(s) de saut interne : 0x004dad4a, 0x004dadd1, 0x004dae80, 0x004dae8e, 0x004dae96, 0x004daefc, 0x004daf2e, 0x004daf61, 0x004dafbe, 0x004dafcf, 0x004dafd2, 0x004dafef, 0x004db017, 0x004db061, 0x004db0a9, 0x004db131, 0x004db156, 0x004db165, 0x004db171, 0x004db193, 0x004db1c5, 0x004db206, 0x004db215, 0x004db221, 0x004db254, 0x004db27f, 0x004db28b, 0x004db2df, 0x004db2fa, 0x004db2fe, 0x004db337, 0x004db35a, 0x004db374, 0x004db396, 0x004db46c, 0x004db4c7, 0x004db4e7, 0x004db503, 0x004db513, 0x004db537, 0x004db56e
