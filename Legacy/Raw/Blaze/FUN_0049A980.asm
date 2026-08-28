Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0049a980  section .text  offset fichier 0x99d80  2175 octets

  0049a980  83ec30                sub       esp,30h
  0049a983  53                    push      ebx
  0049a984  55                    push      ebp
  0049a985  56                    push      esi
  0049a986  8b742440              mov       esi,[esp+40h]
  0049a98a  8d442428              lea       eax,[esp+28h]
  0049a98e  57                    push      edi
  0049a98f  8d4c2430              lea       ecx,[esp+30h]
  0049a993  50                    push      eax
  0049a994  8d54242c              lea       edx,[esp+2Ch]
  0049a998  51                    push      ecx
  0049a999  33ff                  xor       edi,edi
  0049a99b  52                    push      edx
  0049a99c  56                    push      esi
  0049a99d  66897c2444            mov       [esp+44h],di
  0049a9a2  66c74424461e00        mov       word [esp+46h],1Eh
  0049a9a9  66c74424483200        mov       word [esp+48h],32h
  0049a9b0  66c744244a6400        mov       word [esp+4Ah],64h
  0049a9b7  66897c244c            mov       [esp+4Ch],di
  0049a9bc  66c744244e5000        mov       word [esp+4Eh],50h
  0049a9c3  e8b8df0900            call      00538980h
  0049a9c8  83c410                add       esp,10h
  0049a9cb  8d44241c              lea       eax,[esp+1Ch]
  0049a9cf  8d4c2414              lea       ecx,[esp+14h]
  0049a9d3  8d542410              lea       edx,[esp+10h]
  0049a9d7  50                    push      eax
  0049a9d8  51                    push      ecx
  0049a9d9  8d442420              lea       eax,[esp+20h]
  0049a9dd  52                    push      edx
  0049a9de  8d4c2450              lea       ecx,[esp+50h]
  0049a9e2  50                    push      eax
  0049a9e3  51                    push      ecx
  0049a9e4  56                    push      esi
  0049a9e5  e836b50900            call      00535F20h
  0049a9ea  8b4c245c              mov       ecx,[esp+5Ch]
  0049a9ee  8b44242c              mov       eax,[esp+2Ch]
  0049a9f2  bb40000000            mov       ebx,40h
  0049a9f7  83c418                add       esp,18h
  0049a9fa  2bd9                  sub       ebx,ecx
  0049a9fc  3bc7                  cmp       eax,edi
  0049a9fe  895c2424              mov       [esp+24h],ebx
  0049aa02  bd01000000            mov       ebp,1
  0049aa07  0f8491040000          je        near 0049AE9Eh
  0049aa0d  56                    push      esi
  0049aa0e  e88d0b0400            call      004DB5A0h
  0049aa13  8b06                  mov       eax,[esi]
  0049aa15  83c404                add       esp,4
  0049aa18  a802                  test      al,2
  0049aa1a  0f843b010000          je        near 0049AB5Bh
  0049aa20  f644241c10            test      byte [esp+1Ch],10h
  0049aa25  740d                  je        short 0049AA34h
  0049aa27  24fd                  and       al,0FDh
  0049aa29  c6464300              mov       byte [esi+43h],0
  0049aa2d  8906                  mov       [esi],eax
  0049aa2f  e96a040000            jmp       0049AE9Eh

LAB_0049aa34:
  0049aa34  33c9                  xor       ecx,ecx
  0049aa36  8a4e42                mov       cl,[esi+42h]
  0049aa39  2bcf                  sub       ecx,edi
  0049aa3b  0f849d000000          je        near 0049AADEh
  0049aa41  49                    dec       ecx
  0049aa42  0f8556040000          jne       near 0049AE9Eh
  0049aa48  66837e1a0e            cmp       word [esi+1Ah],0Eh
  0049aa4d  7579                  jne       short 0049AAC8h
  0049aa4f  8b5658                mov       edx,[esi+58h]
  0049aa52  8b6e50                mov       ebp,[esi+50h]
  0049aa55  8b5e54                mov       ebx,[esi+54h]
  0049aa58  89542420              mov       [esp+20h],edx
  0049aa5c  e8dfc60a00            call      00547140h
  0049aa61  8bf8                  mov       edi,eax
  0049aa63  0fbf4632              movsx     eax,word [esi+32h]
  0049aa67  83e70f                and       edi,0Fh
  0049aa6a  50                    push      eax
  0049aa6b  83c728                add       edi,28h
  0049aa6e  e8a9af0a00            call      00545A1Ch
  0049aa73  0fafc7                imul      eax,edi
  0049aa76  0fbf4e32              movsx     ecx,word [esi+32h]
  0049aa7a  51                    push      ecx
  0049aa7b  894650                mov       [esi+50h],eax
  0049aa7e  e893af0a00            call      00545A16h
  0049aa83  0fafc7                imul      eax,edi
  0049aa86  894658                mov       [esi+58h],eax
  0049aa89  c7465400800100        mov       dword [esi+54h],18000h
  0049aa90  8b15cc975e00          mov       edx,[5E97CCh]
  0049aa96  6aff                  push      0FFFFFFFFh
  0049aa98  6aff                  push      0FFFFFFFFh
  0049aa9a  8b82cc000000          mov       eax,[edx+0CCh]
  0049aaa0  33d2                  xor       edx,edx
  0049aaa2  8b08                  mov       ecx,[eax]
  0049aaa4  8a5101                mov       dl,[ecx+1]
  0049aaa7  81c2c0000000          add       edx,0C0h
  0049aaad  52                    push      edx
  0049aaae  56                    push      esi
  0049aaaf  e81cfffdff            call      0047A9D0h
  0049aab4  8b442430              mov       eax,[esp+30h]
  0049aab8  895e54                mov       [esi+54h],ebx
  0049aabb  8b5c2434              mov       ebx,[esp+34h]
  0049aabf  83c410                add       esp,10h
  0049aac2  896e50                mov       [esi+50h],ebp
  0049aac5  894658                mov       [esi+58h],eax

LAB_0049aac8:
  0049aac8  8b06                  mov       eax,[esi]
  0049aaca  a900000002            test      eax,2000000h
  0049aacf  0f84c9030000          je        near 0049AE9Eh
  0049aad5  24fd                  and       al,0FDh
  0049aad7  8906                  mov       [esi],eax
  0049aad9  e9c0030000            jmp       0049AE9Eh

LAB_0049aade:
  0049aade  66837e1a08            cmp       word [esi+1Ah],8
  0049aae3  755a                  jne       short 0049AB3Fh
  0049aae5  8d4c2434              lea       ecx,[esp+34h]
  0049aae9  6a00                  push      0
  0049aaeb  8d561c                lea       edx,[esi+1Ch]
  0049aaee  51                    push      ecx
  0049aaef  52                    push      edx
  0049aaf0  56                    push      esi
  0049aaf1  e83af10700            call      00519C30h
  0049aaf6  8bf8                  mov       edi,eax
  0049aaf8  a1b8e05d00            mov       eax,[5DE0B8h]
  0049aafd  83c410                add       esp,10h
  0049ab00  85c0                  test      eax,eax
  0049ab02  7431                  je        short 0049AB35h
  0049ab04  57                    push      edi
  0049ab05  56                    push      esi
  0049ab06  e875f00900            call      00539B80h
  0049ab0b  8b07                  mov       eax,[edi]
  0049ab0d  83c408                add       esp,8
  0049ab10  0c40                  or        al,40h
  0049ab12  66c787960000001000    mov       word [edi+96h],10h
  0049ab1b  8907                  mov       [edi],eax
  0049ab1d  83c71c                add       edi,1Ch
  0049ab20  57                    push      edi
  0049ab21  6800180000            push      1800h
  0049ab26  6800030000            push      300h
  0049ab2b  6a03                  push      3
  0049ab2d  e8ee2f0a00            call      0053DB20h
  0049ab32  83c410                add       esp,10h

LAB_0049ab35:
  0049ab35  8b06                  mov       eax,[esi]
  0049ab37  a801                  test      al,1
  0049ab39  740c                  je        short 0049AB47h
  0049ab3b  66ff4e1a              dec       word [esi+1Ah]

LAB_0049ab3f:
  0049ab3f  a801                  test      al,1
  0049ab41  0f8557030000          jne       near 0049AE9Eh

LAB_0049ab47:
  0049ab47  a900000002            test      eax,2000000h
  0049ab4c  0f8445030000          je        near 0049AE97h
  0049ab52  24fd                  and       al,0FDh
  0049ab54  8906                  mov       [esi],eax
  0049ab56  e93c030000            jmp       0049AE97h

LAB_0049ab5b:
  0049ab5b  a810                  test      al,10h
  0049ab5d  7456                  je        short 0049ABB5h
  0049ab5f  a900000002            test      eax,2000000h
  0049ab64  7412                  je        short 0049AB78h
  0049ab66  8a4e43                mov       cl,[esi+43h]
  0049ab69  84c9                  test      cl,cl
  0049ab6b  740b                  je        short 0049AB78h
  0049ab6d  fec9                  dec       cl
  0049ab6f  884e43                mov       [esi+43h],cl
  0049ab72  7504                  jne       short 0049AB78h
  0049ab74  24ef                  and       al,0EFh
  0049ab76  8906                  mov       [esi],eax

LAB_0049ab78:
  0049ab78  8b442444              mov       eax,[esp+44h]
  0049ab7c  8b4c2410              mov       ecx,[esp+10h]
  0049ab80  83c040                add       eax,40h
  0049ab83  8d1408                lea       edx,[eax+ecx]
  0049ab86  8d3c00                lea       edi,[eax+eax]
  0049ab89  81e2ff0f0000          and       edx,0FFFh
  0049ab8f  3bd7                  cmp       edx,edi
  0049ab91  7f09                  jg        short 0049AB9Ch
  0049ab93  66014e32              add       [esi+32h],cx
  0049ab97  e902030000            jmp       0049AE9Eh

LAB_0049ab9c:
  0049ab9c  81e1ff0f0000          and       ecx,0FFFh
  0049aba2  81f900080000          cmp       ecx,800h
  0049aba8  7c02                  jl        short 0049ABACh
  0049abaa  f7d8                  neg       eax

LAB_0049abac:
  0049abac  66014632              add       [esi+32h],ax
  0049abb0  e9e9020000            jmp       0049AE9Eh

LAB_0049abb5:
  0049abb5  a904080000            test      eax,804h
  0049abba  0f85de020000          jne       near 0049AE9Eh
  0049abc0  a801                  test      al,1
  0049abc2  743c                  je        short 0049AC00h
  0049abc4  e877c50a00            call      00547140h
  0049abc9  99                    cdq
  0049abca  b964000000            mov       ecx,64h
  0049abcf  f7f9                  idiv      ecx
  0049abd1  8b442444              mov       eax,[esp+44h]
  0049abd5  c1f802                sar       eax,2
  0049abd8  3bd0                  cmp       edx,eax
  0049abda  0f8dbe020000          jge       near 0049AE9Eh
  0049abe0  8b0e                  mov       ecx,[esi]
  0049abe2  c7461803000000        mov       dword [esi+18h],3
  0049abe9  81e1fffffffe          and       ecx,0FEFFFFFFh
  0049abef  c6464200              mov       byte [esi+42h],0
  0049abf3  83c902                or        ecx,2
  0049abf6  897e5c                mov       [esi+5Ch],edi
  0049abf9  890e                  mov       [esi],ecx
  0049abfb  e99e020000            jmp       0049AE9Eh

LAB_0049ac00:
  0049ac00  33c9                  xor       ecx,ecx
  0049ac02  8a4e42                mov       cl,[esi+42h]
  0049ac05  83f904                cmp       ecx,4
  0049ac08  0f8790020000          ja        near 0049AE9Eh
  0049ac0e  ff248d00b24900        jmp       dword [ecx*4+49B200h]
  0049ac15  66397e18              cmp       [esi+18h],di
  0049ac19  740a                  je        short 0049AC25h
  0049ac1b  0d00000001            or        eax,1000000h
  0049ac20  897e18                mov       [esi+18h],edi
  0049ac23  8906                  mov       [esi],eax

LAB_0049ac25:
  0049ac25  817c24182c010000      cmp       dword [esp+18h],12Ch
  0049ac2d  7d09                  jge       short 0049AC38h
  0049ac2f  c6464300              mov       byte [esi+43h],0
  0049ac33  e966020000            jmp       0049AE9Eh

LAB_0049ac38:
  0049ac38  8a4643                mov       al,[esi+43h]
  0049ac3b  84c0                  test      al,al
  0049ac3d  7460                  je        short 0049AC9Fh
  0049ac3f  fec8                  dec       al
  0049ac41  897e5c                mov       [esi+5Ch],edi
  0049ac44  884643                mov       [esi+43h],al
  0049ac47  e952020000            jmp       0049AE9Eh
  0049ac4c  66396e18              cmp       [esi+18h],bp
  0049ac50  740a                  je        short 0049AC5Ch
  0049ac52  0d00000001            or        eax,1000000h
  0049ac57  896e18                mov       [esi+18h],ebp
  0049ac5a  8906                  mov       [esi],eax

LAB_0049ac5c:
  0049ac5c  8b542444              mov       edx,[esp+44h]
  0049ac60  8b4c2410              mov       ecx,[esp+10h]
  0049ac64  8d841280000000        lea       eax,[edx+edx+80h]
  0049ac6b  8d1408                lea       edx,[eax+ecx]
  0049ac6e  8d2c00                lea       ebp,[eax+eax]
  0049ac71  81e2ff0f0000          and       edx,0FFFh
  0049ac77  3bd5                  cmp       edx,ebp
  0049ac79  7f10                  jg        short 0049AC8Bh
  0049ac7b  66014e32              add       [esi+32h],cx
  0049ac7f  c6464300              mov       byte [esi+43h],0
  0049ac83  897e5c                mov       [esi+5Ch],edi
  0049ac86  e913020000            jmp       0049AE9Eh

LAB_0049ac8b:
  0049ac8b  81e1ff0f0000          and       ecx,0FFFh
  0049ac91  81f900080000          cmp       ecx,800h
  0049ac97  7c02                  jl        short 0049AC9Bh
  0049ac99  f7d8                  neg       eax

LAB_0049ac9b:
  0049ac9b  66014632              add       [esi+32h],ax

LAB_0049ac9f:
  0049ac9f  897e5c                mov       [esi+5Ch],edi
  0049aca2  e9f7010000            jmp       0049AE9Eh
  0049aca7  66396e18              cmp       [esi+18h],bp
  0049acab  740a                  je        short 0049ACB7h
  0049acad  0d00000001            or        eax,1000000h
  0049acb2  896e18                mov       [esi+18h],ebp
  0049acb5  8906                  mov       [esi],eax

LAB_0049acb7:
  0049acb7  0fbf4610              movsx     eax,word [esi+10h]
  0049acbb  39442418              cmp       [esp+18h],eax
  0049acbf  7d09                  jge       short 0049ACCAh
  0049acc1  c6464300              mov       byte [esi+43h],0
  0049acc5  e9d4010000            jmp       0049AE9Eh

LAB_0049acca:
  0049acca  8a4643                mov       al,[esi+43h]
  0049accd  84c0                  test      al,al
  0049accf  7405                  je        short 0049ACD6h
  0049acd1  fec8                  dec       al
  0049acd3  884643                mov       [esi+43h],al

LAB_0049acd6:
  0049acd6  8b542444              mov       edx,[esp+44h]
  0049acda  8b4c2410              mov       ecx,[esp+10h]
  0049acde  8d841280000000        lea       eax,[edx+edx+80h]
  0049ace5  8d3c08                lea       edi,[eax+ecx]
  0049ace8  8d2c00                lea       ebp,[eax+eax]
  0049aceb  81e7ff0f0000          and       edi,0FFFh
  0049acf1  3bfd                  cmp       edi,ebp
  0049acf3  7f15                  jg        short 0049AD0Ah
  0049acf5  66014e32              add       [esi+32h],cx
  0049acf9  81c2c0000000          add       edx,0C0h
  0049acff  c1e207                shl       edx,7
  0049ad02  89565c                mov       [esi+5Ch],edx
  0049ad05  e994010000            jmp       0049AE9Eh

LAB_0049ad0a:
  0049ad0a  81e1ff0f0000          and       ecx,0FFFh
  0049ad10  81f900080000          cmp       ecx,800h
  0049ad16  7c02                  jl        short 0049AD1Ah
  0049ad18  f7d8                  neg       eax

LAB_0049ad1a:
  0049ad1a  66014632              add       [esi+32h],ax
  0049ad1e  81c2c0000000          add       edx,0C0h
  0049ad24  c1e207                shl       edx,7
  0049ad27  89565c                mov       [esi+5Ch],edx
  0049ad2a  e96f010000            jmp       0049AE9Eh
  0049ad2f  8a4e43                mov       cl,[esi+43h]
  0049ad32  84c9                  test      cl,cl
  0049ad34  7405                  je        short 0049AD3Bh
  0049ad36  fec9                  dec       cl
  0049ad38  884e43                mov       [esi+43h],cl

LAB_0049ad3b:
  0049ad3b  66396e18              cmp       [esi+18h],bp
  0049ad3f  740a                  je        short 0049AD4Bh
  0049ad41  0d00000001            or        eax,1000000h
  0049ad46  896e18                mov       [esi+18h],ebp
  0049ad49  8906                  mov       [esi],eax

LAB_0049ad4b:
  0049ad4b  e8f0c30a00            call      00547140h
  0049ad50  99                    cdq
  0049ad51  b964000000            mov       ecx,64h
  0049ad56  f7f9                  idiv      ecx
  0049ad58  8b442444              mov       eax,[esp+44h]
  0049ad5c  8bc8                  mov       ecx,eax
  0049ad5e  c1f903                sar       ecx,3
  0049ad61  3bd1                  cmp       edx,ecx
  0049ad63  7d5c                  jge       short 0049ADC1h
  0049ad65  8b7c2414              mov       edi,[esp+14h]
  0049ad69  0500010000            add       eax,100h
  0049ad6e  c1e007                shl       eax,7
  0049ad71  0fbf5610              movsx     edx,word [esi+10h]
  0049ad75  0faff8                imul      edi,eax
  0049ad78  52                    push      edx
  0049ad79  89465c                mov       [esi+5Ch],eax
  0049ad7c  c1ff0c                sar       edi,0Ch
  0049ad7f  e898ac0a00            call      00545A1Ch
  0049ad84  0fafc7                imul      eax,edi
  0049ad87  8b5650                mov       edx,[esi+50h]
  0049ad8a  c1f80c                sar       eax,0Ch
  0049ad8d  03d0                  add       edx,eax
  0049ad8f  0fbf4610              movsx     eax,word [esi+10h]
  0049ad93  50                    push      eax
  0049ad94  895650                mov       [esi+50h],edx
  0049ad97  e87aac0a00            call      00545A16h
  0049ad9c  0fafc7                imul      eax,edi
  0049ad9f  8b5658                mov       edx,[esi+58h]
  0049ada2  8b4e54                mov       ecx,[esi+54h]
  0049ada5  c1f80c                sar       eax,0Ch
  0049ada8  03d0                  add       edx,eax
  0049adaa  8b06                  mov       eax,[esi]
  0049adac  81c100a00000          add       ecx,0A000h
  0049adb2  0bc5                  or        eax,ebp
  0049adb4  895658                mov       [esi+58h],edx
  0049adb7  894e54                mov       [esi+54h],ecx
  0049adba  8906                  mov       [esi],eax
  0049adbc  e9d6000000            jmp       0049AE97h

LAB_0049adc1:
  0049adc1  8b7c2414              mov       edi,[esp+14h]
  0049adc5  0580000000            add       eax,80h
  0049adca  c1e007                shl       eax,7
  0049adcd  0fbf4e10              movsx     ecx,word [esi+10h]
  0049add1  0faff8                imul      edi,eax
  0049add4  51                    push      ecx
  0049add5  89465c                mov       [esi+5Ch],eax
  0049add8  c1ff0c                sar       edi,0Ch
  0049addb  e83cac0a00            call      00545A1Ch
  0049ade0  0fafc7                imul      eax,edi
  0049ade3  0fbf5610              movsx     edx,word [esi+10h]
  0049ade7  8b4e1c                mov       ecx,[esi+1Ch]
  0049adea  52                    push      edx
  0049adeb  c1f80c                sar       eax,0Ch
  0049adee  03c8                  add       ecx,eax
  0049adf0  894e1c                mov       [esi+1Ch],ecx
  0049adf3  e81eac0a00            call      00545A16h
  0049adf8  0fafc7                imul      eax,edi
  0049adfb  8b6e24                mov       ebp,[esi+24h]
  0049adfe  8b4c2410              mov       ecx,[esp+10h]
  0049ae02  c1f80c                sar       eax,0Ch
  0049ae05  03e8                  add       ebp,eax
  0049ae07  8b442444              mov       eax,[esp+44h]
  0049ae0b  896e24                mov       [esi+24h],ebp
  0049ae0e  8d048532000000        lea       eax,[eax*4+32h]
  0049ae15  8d1408                lea       edx,[eax+ecx]
  0049ae18  8d3c00                lea       edi,[eax+eax]
  0049ae1b  81e2ff0f0000          and       edx,0FFFh
  0049ae21  3bd7                  cmp       edx,edi
  0049ae23  7f06                  jg        short 0049AE2Bh
  0049ae25  66014e32              add       [esi+32h],cx
  0049ae29  eb6c                  jmp       short 0049AE97h

LAB_0049ae2b:
  0049ae2b  81e1ff0f0000          and       ecx,0FFFh
  0049ae31  81f900080000          cmp       ecx,800h
  0049ae37  7c02                  jl        short 0049AE3Bh
  0049ae39  f7d8                  neg       eax

LAB_0049ae3b:
  0049ae3b  66014632              add       [esi+32h],ax
  0049ae3f  eb56                  jmp       short 0049AE97h
  0049ae41  25fffffffe            and       eax,0FEFFFFFFh
  0049ae46  8b7c2414              mov       edi,[esp+14h]
  0049ae4a  0bc5                  or        eax,ebp
  0049ae4c  c7461806000000        mov       dword [esi+18h],6
  0049ae53  8906                  mov       [esi],eax
  0049ae55  8b442444              mov       eax,[esp+44h]
  0049ae59  05a0000000            add       eax,0A0h
  0049ae5e  c1e008                shl       eax,8
  0049ae61  0fbf4e10              movsx     ecx,word [esi+10h]
  0049ae65  0faff8                imul      edi,eax
  0049ae68  51                    push      ecx
  0049ae69  89465c                mov       [esi+5Ch],eax
  0049ae6c  c1ff0c                sar       edi,0Ch
  0049ae6f  e8a8ab0a00            call      00545A1Ch
  0049ae74  0fafc7                imul      eax,edi
  0049ae77  0fbf5610              movsx     edx,word [esi+10h]
  0049ae7b  c1f80c                sar       eax,0Ch
  0049ae7e  52                    push      edx
  0049ae7f  894650                mov       [esi+50h],eax
  0049ae82  e88fab0a00            call      00545A16h
  0049ae87  0fafc7                imul      eax,edi
  0049ae8a  c1f80c                sar       eax,0Ch
  0049ae8d  894658                mov       [esi+58h],eax
  0049ae90  c7465400900100        mov       dword [esi+54h],19000h

LAB_0049ae97:
  0049ae97  c7465c00000000        mov       dword [esi+5Ch],0

LAB_0049ae9e:
  0049ae9e  8b442414              mov       eax,[esp+14h]
  0049aea2  50                    push      eax
  0049aea3  56                    push      esi
  0049aea4  e8b7d2feff            call      00488160h
  0049aea9  83c408                add       esp,8
  0049aeac  56                    push      esi
  0049aead  e88e8a0900            call      00533940h
  0049aeb2  83c404                add       esp,4
  0049aeb5  56                    push      esi
  0049aeb6  e8354c0400            call      004DFAF0h
  0049aebb  8b4c242c              mov       ecx,[esp+2Ch]
  0049aebf  83c404                add       esp,4
  0049aec2  51                    push      ecx
  0049aec3  56                    push      esi
  0049aec4  e8a7880400            call      004E3770h
  0049aec9  8b44241c              mov       eax,[esp+1Ch]
  0049aecd  83c408                add       esp,8
  0049aed0  85c0                  test      eax,eax
  0049aed2  0f841f030000          je        near 0049B1F7h
  0049aed8  f70617080000          test      dword [esi],817h
  0049aede  0f8513030000          jne       near 0049B1F7h
  0049aee4  8a4643                mov       al,[esi+43h]
  0049aee7  84c0                  test      al,al
  0049aee9  0f8508030000          jne       near 0049B1F7h
  0049aeef  8b466c                mov       eax,[esi+6Ch]
  0049aef2  8b561c                mov       edx,[esi+1Ch]
  0049aef5  8b781c                mov       edi,[eax+1Ch]
  0049aef8  8b6824                mov       ebp,[eax+24h]
  0049aefb  8b4624                mov       eax,[esi+24h]
  0049aefe  2bfa                  sub       edi,edx
  0049af00  2be8                  sub       ebp,eax
  0049af02  c1ff0c                sar       edi,0Ch
  0049af05  c1fd0c                sar       ebp,0Ch
  0049af08  8bd7                  mov       edx,edi
  0049af0a  8bc5                  mov       eax,ebp
  0049af0c  0fafd7                imul      edx,edi
  0049af0f  0fafc5                imul      eax,ebp
  0049af12  03d0                  add       edx,eax
  0049af14  52                    push      edx
  0049af15  e8f6aa0a00            call      00545A10h
  0049af1a  55                    push      ebp
  0049af1b  57                    push      edi
  0049af1c  89442420              mov       [esp+20h],eax
  0049af20  e8fdaa0a00            call      00545A22h
  0049af25  668b4e32              mov       cx,[esi+32h]
  0049af29  2bc1                  sub       eax,ecx
  0049af2b  25ff0f0000            and       eax,0FFFh
  0049af30  89442410              mov       [esp+10h],eax
  0049af34  8b442418              mov       eax,[esp+18h]
  0049af38  3dfa000000            cmp       eax,0FAh
  0049af3d  0f8d61010000          jge       near 0049B0A4h
  0049af43  e8f8c10a00            call      00547140h
  0049af48  99                    cdq
  0049af49  b964000000            mov       ecx,64h
  0049af4e  f7f9                  idiv      ecx
  0049af50  83fa1e                cmp       edx,1Eh
  0049af53  0f8dc9000000          jge       near 0049B022h
  0049af59  8b542410              mov       edx,[esp+10h]
  0049af5d  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  0049af64  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  0049af6b  25ff0f0000            and       eax,0FFFh
  0049af70  3bc1                  cmp       eax,ecx
  0049af72  7f3e                  jg        short 0049AFB2h
  0049af74  e8c7c10a00            call      00547140h
  0049af79  99                    cdq
  0049af7a  8d4c1b01              lea       ecx,[ebx+ebx+1]
  0049af7e  f7f9                  idiv      ecx
  0049af80  8b442418              mov       eax,[esp+18h]
  0049af84  83c264                add       edx,64h
  0049af87  3bc2                  cmp       eax,edx
  0049af89  7d27                  jge       short 0049AFB2h
  0049af8b  8b16                  mov       edx,[esi]
  0049af8d  c7461803000000        mov       dword [esi+18h],3
  0049af94  81e2fffffffe          and       edx,0FEFFFFFFh
  0049af9a  c6464200              mov       byte [esi+42h],0
  0049af9e  83ca02                or        edx,2
  0049afa1  c7465c00000000        mov       dword [esi+5Ch],0
  0049afa8  8916                  mov       [esi],edx
  0049afaa  5f                    pop       edi
  0049afab  5e                    pop       esi
  0049afac  5d                    pop       ebp
  0049afad  5b                    pop       ebx
  0049afae  83c430                add       esp,30h
  0049afb1  c3                    ret

LAB_0049afb2:
  0049afb2  8b442410              mov       eax,[esp+10h]
  0049afb6  8d14dd00040000        lea       edx,[ebx*8+400h]
  0049afbd  8d8c9800020000        lea       ecx,[eax+ebx*4+200h]
  0049afc4  81e1ff0f0000          and       ecx,0FFFh
  0049afca  3bca                  cmp       ecx,edx
  0049afcc  0f8ff1010000          jg        near 0049B1C3h
  0049afd2  e869c10a00            call      00547140h
  0049afd7  99                    cdq
  0049afd8  b964000000            mov       ecx,64h
  0049afdd  f7f9                  idiv      ecx
  0049afdf  d1fb                  sar       ebx,1
  0049afe1  83c320                add       ebx,20h
  0049afe4  3bd3                  cmp       edx,ebx
  0049afe6  0f8dd7010000          jge       near 0049B1C3h
  0049afec  8b16                  mov       edx,[esi]
  0049afee  c7461805000000        mov       dword [esi+18h],5
  0049aff5  81e2fffffffe          and       edx,0FEFFFFFFh
  0049affb  83ca10                or        edx,10h
  0049affe  8916                  mov       [esi],edx
  0049b000  e83bc10a00            call      00547140h
  0049b005  99                    cdq
  0049b006  b905000000            mov       ecx,5
  0049b00b  c7465c00000000        mov       dword [esi+5Ch],0
  0049b012  f7f9                  idiv      ecx
  0049b014  80c20a                add       dl,0Ah
  0049b017  885643                mov       [esi+43h],dl
  0049b01a  5f                    pop       edi
  0049b01b  5e                    pop       esi
  0049b01c  5d                    pop       ebp
  0049b01d  5b                    pop       ebx
  0049b01e  83c430                add       esp,30h
  0049b021  c3                    ret

LAB_0049b022:
  0049b022  83fa3c                cmp       edx,3Ch
  0049b025  0f8c35010000          jl        near 0049B160h
  0049b02b  83fa4b                cmp       edx,4Bh
  0049b02e  7d24                  jge       short 0049B054h
  0049b030  8b542444              mov       edx,[esp+44h]
  0049b034  8b442418              mov       eax,[esp+18h]
  0049b038  83c264                add       edx,64h
  0049b03b  3bc2                  cmp       eax,edx
  0049b03d  7e15                  jle       short 0049B054h
  0049b03f  c6464204              mov       byte [esi+42h],4
  0049b043  e8f8c00a00            call      00547140h
  0049b048  66894610              mov       [esi+10h],ax
  0049b04c  5f                    pop       edi
  0049b04d  5e                    pop       esi
  0049b04e  5d                    pop       ebp
  0049b04f  5b                    pop       ebx
  0049b050  83c430                add       esp,30h
  0049b053  c3                    ret

LAB_0049b054:
  0049b054  c6464203              mov       byte [esi+42h],3
  0049b058  e8e3c00a00            call      00547140h
  0049b05d  99                    cdq
  0049b05e  b90a000000            mov       ecx,0Ah
  0049b063  f7f9                  idiv      ecx
  0049b065  d1fb                  sar       ebx,1
  0049b067  02d3                  add       dl,bl
  0049b069  80c214                add       dl,14h
  0049b06c  885643                mov       [esi+43h],dl
  0049b06f  8b542444              mov       edx,[esp+44h]
  0049b073  8dba80000000          lea       edi,[edx+80h]
  0049b079  c1e704                shl       edi,4
  0049b07c  e8bfc00a00            call      00547140h
  0049b081  99                    cdq
  0049b082  f7ff                  idiv      edi
  0049b084  d1ff                  sar       edi,1
  0049b086  2bd7                  sub       edx,edi
  0049b088  8b7c2410              mov       edi,[esp+10h]
  0049b08c  03d7                  add       edx,edi
  0049b08e  66035632              add       dx,[esi+32h]
  0049b092  81c200080000          add       edx,800h
  0049b098  66895610              mov       [esi+10h],dx
  0049b09c  5f                    pop       edi
  0049b09d  5e                    pop       esi
  0049b09e  5d                    pop       ebp
  0049b09f  5b                    pop       ebx
  0049b0a0  83c430                add       esp,30h
  0049b0a3  c3                    ret

LAB_0049b0a4:
  0049b0a4  3df4010000            cmp       eax,1F4h
  0049b0a9  0f8dda000000          jge       near 0049B189h
  0049b0af  e88cc00a00            call      00547140h
  0049b0b4  99                    cdq
  0049b0b5  b964000000            mov       ecx,64h
  0049b0ba  f7f9                  idiv      ecx
  0049b0bc  8b442444              mov       eax,[esp+44h]
  0049b0c0  d1f8                  sar       eax,1
  0049b0c2  83c010                add       eax,10h
  0049b0c5  3bd0                  cmp       edx,eax
  0049b0c7  7d43                  jge       short 0049B10Ch
  0049b0c9  8b4c2410              mov       ecx,[esp+10h]
  0049b0cd  81c100040000          add       ecx,400h
  0049b0d3  81e1ff0f0000          and       ecx,0FFFh
  0049b0d9  81f900080000          cmp       ecx,800h
  0049b0df  0f8dde000000          jge       near 0049B1C3h
  0049b0e5  8b16                  mov       edx,[esi]
  0049b0e7  c7461804000000        mov       dword [esi+18h],4
  0049b0ee  81e2fffffffe          and       edx,0FEFFFFFFh
  0049b0f4  c6464201              mov       byte [esi+42h],1
  0049b0f8  83ca02                or        edx,2
  0049b0fb  c7465c00000000        mov       dword [esi+5Ch],0
  0049b102  8916                  mov       [esi],edx
  0049b104  5f                    pop       edi
  0049b105  5e                    pop       esi
  0049b106  5d                    pop       ebp
  0049b107  5b                    pop       ebx
  0049b108  83c430                add       esp,30h
  0049b10b  c3                    ret

LAB_0049b10c:
  0049b10c  83fa46                cmp       edx,46h
  0049b10f  7d4a                  jge       short 0049B15Bh
  0049b111  c6464203              mov       byte [esi+42h],3
  0049b115  e826c00a00            call      00547140h
  0049b11a  99                    cdq
  0049b11b  b90a000000            mov       ecx,0Ah
  0049b120  f7f9                  idiv      ecx
  0049b122  d1fb                  sar       ebx,1
  0049b124  02d3                  add       dl,bl
  0049b126  80c214                add       dl,14h
  0049b129  885643                mov       [esi+43h],dl
  0049b12c  8b542444              mov       edx,[esp+44h]
  0049b130  8dba80000000          lea       edi,[edx+80h]
  0049b136  c1e704                shl       edi,4
  0049b139  e802c00a00            call      00547140h
  0049b13e  99                    cdq
  0049b13f  f7ff                  idiv      edi
  0049b141  8b4c2410              mov       ecx,[esp+10h]
  0049b145  d1ff                  sar       edi,1
  0049b147  2bd7                  sub       edx,edi
  0049b149  03d1                  add       edx,ecx
  0049b14b  66035632              add       dx,[esi+32h]
  0049b14f  66895610              mov       [esi+10h],dx
  0049b153  5f                    pop       edi
  0049b154  5e                    pop       esi
  0049b155  5d                    pop       ebp
  0049b156  5b                    pop       ebx
  0049b157  83c430                add       esp,30h
  0049b15a  c3                    ret

LAB_0049b15b:
  0049b15b  83fa5a                cmp       edx,5Ah
  0049b15e  7d3b                  jge       short 0049B19Bh

LAB_0049b160:
  0049b160  c6464202              mov       byte [esi+42h],2
  0049b164  e8d7bf0a00            call      00547140h
  0049b169  99                    cdq
  0049b16a  b90a000000            mov       ecx,0Ah
  0049b16f  66c746106400          mov       word [esi+10h],64h
  0049b175  f7f9                  idiv      ecx
  0049b177  d1fb                  sar       ebx,1
  0049b179  02d3                  add       dl,bl
  0049b17b  80c214                add       dl,14h
  0049b17e  885643                mov       [esi+43h],dl
  0049b181  5f                    pop       edi
  0049b182  5e                    pop       esi
  0049b183  5d                    pop       ebp
  0049b184  5b                    pop       ebx
  0049b185  83c430                add       esp,30h
  0049b188  c3                    ret

LAB_0049b189:
  0049b189  e8b2bf0a00            call      00547140h
  0049b18e  99                    cdq
  0049b18f  b964000000            mov       ecx,64h
  0049b194  f7f9                  idiv      ecx
  0049b196  83fa1e                cmp       edx,1Eh
  0049b199  7d23                  jge       short 0049B1BEh

LAB_0049b19b:
  0049b19b  c6464200              mov       byte [esi+42h],0
  0049b19f  e89cbf0a00            call      00547140h
  0049b1a4  99                    cdq
  0049b1a5  b90a000000            mov       ecx,0Ah
  0049b1aa  f7f9                  idiv      ecx
  0049b1ac  d1fb                  sar       ebx,1
  0049b1ae  02d3                  add       dl,bl
  0049b1b0  80c214                add       dl,14h
  0049b1b3  885643                mov       [esi+43h],dl
  0049b1b6  5f                    pop       edi
  0049b1b7  5e                    pop       esi
  0049b1b8  5d                    pop       ebp
  0049b1b9  5b                    pop       ebx
  0049b1ba  83c430                add       esp,30h
  0049b1bd  c3                    ret

LAB_0049b1be:
  0049b1be  83fa3c                cmp       edx,3Ch
  0049b1c1  7d10                  jge       short 0049B1D3h

LAB_0049b1c3:
  0049b1c3  c6464201              mov       byte [esi+42h],1
  0049b1c7  c6464301              mov       byte [esi+43h],1
  0049b1cb  5f                    pop       edi
  0049b1cc  5e                    pop       esi
  0049b1cd  5d                    pop       ebp
  0049b1ce  5b                    pop       ebx
  0049b1cf  83c430                add       esp,30h
  0049b1d2  c3                    ret

LAB_0049b1d3:
  0049b1d3  c6464203              mov       byte [esi+42h],3
  0049b1d7  e864bf0a00            call      00547140h
  0049b1dc  99                    cdq
  0049b1dd  b90a000000            mov       ecx,0Ah
  0049b1e2  f7f9                  idiv      ecx
  0049b1e4  d1fb                  sar       ebx,1
  0049b1e6  02d3                  add       dl,bl
  0049b1e8  80c214                add       dl,14h
  0049b1eb  885643                mov       [esi+43h],dl
  0049b1ee  e84dbf0a00            call      00547140h
  0049b1f3  66894610              mov       [esi+10h],ax

LAB_0049b1f7:
  0049b1f7  5f                    pop       edi
  0049b1f8  5e                    pop       esi
  0049b1f9  5d                    pop       ebp
  0049b1fa  5b                    pop       ebx
  0049b1fb  83c430                add       esp,30h
  0049b1fe  c3                    ret

43 cible(s) de saut interne : 0x0049aa34, 0x0049aac8, 0x0049aade, 0x0049ab35, 0x0049ab3f, 0x0049ab47, 0x0049ab5b, 0x0049ab78, 0x0049ab9c, 0x0049abac, 0x0049abb5, 0x0049ac00, 0x0049ac25, 0x0049ac38, 0x0049ac5c, 0x0049ac8b, 0x0049ac9b, 0x0049ac9f, 0x0049acb7, 0x0049acca, 0x0049acd6, 0x0049ad0a, 0x0049ad1a, 0x0049ad3b, 0x0049ad4b, 0x0049adc1, 0x0049ae2b, 0x0049ae3b, 0x0049ae97, 0x0049ae9e, 0x0049afb2, 0x0049b022, 0x0049b054, 0x0049b0a4, 0x0049b10c, 0x0049b15b, 0x0049b160, 0x0049b189, 0x0049b19b, 0x0049b1be, 0x0049b1c3, 0x0049b1d3, 0x0049b1f7
