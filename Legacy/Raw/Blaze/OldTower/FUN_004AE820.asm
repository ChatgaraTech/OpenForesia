Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004ae820  section .text  offset fichier 0xadc20  3392 octets

  004ae820  81ec88000000          sub       esp,88h
  004ae826  53                    push      ebx
  004ae827  55                    push      ebp
  004ae828  56                    push      esi
  004ae829  8bb42498000000        mov       esi,[esp+98h]
  004ae830  b864000000            mov       eax,64h
  004ae835  33ed                  xor       ebp,ebp
  004ae837  668944242a            mov       [esp+2Ah],ax
  004ae83c  668944242e            mov       [esp+2Eh],ax
  004ae841  8a4641                mov       al,[esi+41h]
  004ae844  57                    push      edi
  004ae845  84c0                  test      al,al
  004ae847  66896c2428            mov       [esp+28h],bp
  004ae84c  66c744242a3200        mov       word [esp+2Ah],32h
  004ae853  66c744242c5000        mov       word [esp+2Ch],50h
  004ae85a  66896c2430            mov       [esp+30h],bp
  004ae85f  7533                  jne       short 004AE894h
  004ae861  55                    push      ebp
  004ae862  6a03                  push      3
  004ae864  56                    push      esi
  004ae865  c6464101              mov       byte [esi+41h],1
  004ae869  e8f2dfffff            call      004AC860h
  004ae86e  8b4620                mov       eax,[esi+20h]
  004ae871  83c40c                add       esp,0Ch
  004ae874  894648                mov       [esi+48h],eax
  004ae877  668b4616              mov       ax,[esi+16h]
  004ae87b  663d0a00              cmp       ax,0Ah
  004ae87f  7c0d                  jl        short 004AE88Eh
  004ae881  83c0f6                add       eax,0FFFFFFF6h
  004ae884  66894616              mov       [esi+16h],ax
  004ae888  8b06                  mov       eax,[esi]
  004ae88a  24df                  and       al,0DFh
  004ae88c  eb04                  jmp       short 004AE892h

LAB_004ae88e:
  004ae88e  8b06                  mov       eax,[esi]
  004ae890  0c20                  or        al,20h

LAB_004ae892:
  004ae892  8906                  mov       [esi],eax

LAB_004ae894:
  004ae894  8b16                  mov       edx,[esi]
  004ae896  8b4e1c                mov       ecx,[esi+1Ch]
  004ae899  8b4664                mov       eax,[esi+64h]
  004ae89c  8d7e1c                lea       edi,[esi+1Ch]
  004ae89f  83e2fe                and       edx,0FFFFFFFEh
  004ae8a2  894c2458              mov       [esp+58h],ecx
  004ae8a6  8b4e60                mov       ecx,[esi+60h]
  004ae8a9  8916                  mov       [esi],edx
  004ae8ab  8b5624                mov       edx,[esi+24h]
  004ae8ae  89442440              mov       [esp+40h],eax
  004ae8b2  8b467c                mov       eax,[esi+7Ch]
  004ae8b5  89542460              mov       [esp+60h],edx
  004ae8b9  668b5632              mov       dx,[esi+32h]
  004ae8bd  894c2438              mov       [esp+38h],ecx
  004ae8c1  668b8e90000000        mov       cx,[esi+90h]
  004ae8c8  6689942486000000      mov       [esp+86h],dx
  004ae8d0  8b9680000000          mov       edx,[esi+80h]
  004ae8d6  8b5e6c                mov       ebx,[esi+6Ch]
  004ae8d9  89442448              mov       [esp+48h],eax
  004ae8dd  8b8684000000          mov       eax,[esi+84h]
  004ae8e3  66894c243c            mov       [esp+3Ch],cx
  004ae8e8  8b8e88000000          mov       ecx,[esi+88h]
  004ae8ee  8954244c              mov       [esp+4Ch],edx
  004ae8f2  8b968c000000          mov       edx,[esi+8Ch]
  004ae8f8  89442434              mov       [esp+34h],eax
  004ae8fc  894c2444              mov       [esp+44h],ecx
  004ae900  8d442464              lea       eax,[esp+64h]
  004ae904  89542450              mov       [esp+50h],edx
  004ae908  8d4c246c              lea       ecx,[esp+6Ch]
  004ae90c  50                    push      eax
  004ae90d  8d54246c              lea       edx,[esp+6Ch]
  004ae911  51                    push      ecx
  004ae912  52                    push      edx
  004ae913  56                    push      esi
  004ae914  895c2434              mov       [esp+34h],ebx
  004ae918  e863a00800            call      00538980h
  004ae91d  83c410                add       esp,10h
  004ae920  8d442454              lea       eax,[esp+54h]
  004ae924  8d4c241c              lea       ecx,[esp+1Ch]
  004ae928  8d542418              lea       edx,[esp+18h]
  004ae92c  50                    push      eax
  004ae92d  51                    push      ecx
  004ae92e  8d44241c              lea       eax,[esp+1Ch]
  004ae932  52                    push      edx
  004ae933  8d4c241c              lea       ecx,[esp+1Ch]
  004ae937  50                    push      eax
  004ae938  51                    push      ecx
  004ae939  56                    push      esi
  004ae93a  e8e1750800            call      00535F20h
  004ae93f  8b4c2428              mov       ecx,[esp+28h]
  004ae943  8b54246c              mov       edx,[esp+6Ch]
  004ae947  83c418                add       esp,18h
  004ae94a  b840000000            mov       eax,40h
  004ae94f  2bc1                  sub       eax,ecx
  004ae951  52                    push      edx
  004ae952  56                    push      esi
  004ae953  89442428              mov       [esp+28h],eax
  004ae957  e884740200            call      004D5DE0h
  004ae95c  8b07                  mov       eax,[edi]
  004ae95e  8b531c                mov       edx,[ebx+1Ch]
  004ae961  8b4e24                mov       ecx,[esi+24h]
  004ae964  2bc2                  sub       eax,edx
  004ae966  8b5324                mov       edx,[ebx+24h]
  004ae969  83c408                add       esp,8
  004ae96c  c1f80c                sar       eax,0Ch
  004ae96f  2bca                  sub       ecx,edx
  004ae971  8bd0                  mov       edx,eax
  004ae973  c1f90c                sar       ecx,0Ch
  004ae976  0fafd0                imul      edx,eax
  004ae979  8bc1                  mov       eax,ecx
  004ae97b  0fafc1                imul      eax,ecx
  004ae97e  03d0                  add       edx,eax
  004ae980  52                    push      edx
  004ae981  e88a700900            call      00545A10h
  004ae986  8b4c241c              mov       ecx,[esp+1Ch]
  004ae98a  89442414              mov       [esp+14h],eax
  004ae98e  3bcd                  cmp       ecx,ebp
  004ae990  0f8448060000          je        near 004AEFDEh
  004ae996  8b16                  mov       edx,[esi]
  004ae998  33c0                  xor       eax,eax
  004ae99a  8a4604                mov       al,[esi+4]
  004ae99d  81e2fffffffd          and       edx,0FDFFFFFFh
  004ae9a3  0fbf5e18              movsx     ebx,word [esi+18h]
  004ae9a7  8916                  mov       [esi],edx
  004ae9a9  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004ae9b0  f7c200000001          test      edx,1000000h
  004ae9b6  8b2c98                mov       ebp,[eax+ebx*4]
  004ae9b9  741d                  je        short 004AE9D8h
  004ae9bb  66ff461a              inc       word [esi+1Ah]
  004ae9bf  668b461a              mov       ax,[esi+1Ah]
  004ae9c3  33d2                  xor       edx,edx
  004ae9c5  668b5516              mov       dx,[ebp+16h]
  004ae9c9  0fbfc0                movsx     eax,ax
  004ae9cc  3bc2                  cmp       eax,edx
  004ae9ce  7c2a                  jl        short 004AE9FAh
  004ae9d0  66c7461a0000          mov       word [esi+1Ah],0
  004ae9d6  eb22                  jmp       short 004AE9FAh

LAB_004ae9d8:
  004ae9d8  66ff461a              inc       word [esi+1Ah]
  004ae9dc  668b461a              mov       ax,[esi+1Ah]
  004ae9e0  33db                  xor       ebx,ebx
  004ae9e2  668b5d16              mov       bx,[ebp+16h]
  004ae9e6  0fbfe8                movsx     ebp,ax
  004ae9e9  3beb                  cmp       ebp,ebx
  004ae9eb  7c0d                  jl        short 004AE9FAh
  004ae9ed  48                    dec       eax
  004ae9ee  81ca00000002          or        edx,2000000h
  004ae9f4  6689461a              mov       [esi+1Ah],ax
  004ae9f8  8916                  mov       [esi],edx

LAB_004ae9fa:
  004ae9fa  f60602                test      byte [esi],2
  004ae9fd  7477                  je        short 004AEA76h
  004ae9ff  bb0c000000            mov       ebx,0Ch
  004aea04  66395e1a              cmp       [esi+1Ah],bx
  004aea08  7559                  jne       short 004AEA63h
  004aea0a  8d4c2428              lea       ecx,[esp+28h]
  004aea0e  6a00                  push      0
  004aea10  51                    push      ecx
  004aea11  57                    push      edi
  004aea12  56                    push      esi
  004aea13  e818b20600            call      00519C30h
  004aea18  8be8                  mov       ebp,eax
  004aea1a  a1b8e05d00            mov       eax,[5DE0B8h]
  004aea1f  83c410                add       esp,10h
  004aea22  85c0                  test      eax,eax
  004aea24  7439                  je        short 004AEA5Fh
  004aea26  66395e1a              cmp       [esi+1Ah],bx
  004aea2a  7533                  jne       short 004AEA5Fh
  004aea2c  8b4500                mov       eax,[ebp]
  004aea2f  55                    push      ebp
  004aea30  0c40                  or        al,40h
  004aea32  56                    push      esi
  004aea33  66c785960000001000    mov       word [ebp+96h],10h
  004aea3c  894500                mov       [ebp],eax
  004aea3f  e83cb10800            call      00539B80h
  004aea44  83c408                add       esp,8
  004aea47  83c51c                add       ebp,1Ch
  004aea4a  55                    push      ebp
  004aea4b  6800180000            push      1800h
  004aea50  6800030000            push      300h
  004aea55  6a03                  push      3
  004aea57  e8c4f00800            call      0053DB20h
  004aea5c  83c410                add       esp,10h

LAB_004aea5f:
  004aea5f  8b4c241c              mov       ecx,[esp+1Ch]

LAB_004aea63:
  004aea63  8b16                  mov       edx,[esi]
  004aea65  f7c200000002          test      edx,2000000h
  004aea6b  7409                  je        short 004AEA76h
  004aea6d  83e2fd                and       edx,0FFFFFFFDh
  004aea70  c6464300              mov       byte [esi+43h],0
  004aea74  8916                  mov       [esi],edx

LAB_004aea76:
  004aea76  33c0                  xor       eax,eax
  004aea78  8a4642                mov       al,[esi+42h]
  004aea7b  83f80b                cmp       eax,0Bh
  004aea7e  0f879a040000          ja        near 004AEF1Eh
  004aea84  ff248528f54a00        jmp       dword [eax*4+4AF528h]
  004aea8b  33c9                  xor       ecx,ecx
  004aea8d  66394e12              cmp       [esi+12h],cx
  004aea91  751a                  jne       short 004AEAADh
  004aea93  8b06                  mov       eax,[esi]
  004aea95  66894e18              mov       [esi+18h],cx
  004aea99  0d00000001            or        eax,1000000h
  004aea9e  66894e1a              mov       [esi+1Ah],cx
  004aeaa2  8906                  mov       [esi],eax
  004aeaa4  66c746120100          mov       word [esi+12h],1
  004aeaaa  894e5c                mov       [esi+5Ch],ecx

LAB_004aeaad:
  004aeaad  0fbf5610              movsx     edx,word [esi+10h]
  004aeab1  39542414              cmp       [esp+14h],edx
  004aeab5  7d09                  jge       short 004AEAC0h
  004aeab7  c6464300              mov       byte [esi+43h],0
  004aeabb  e95e040000            jmp       004AEF1Eh

LAB_004aeac0:
  004aeac0  8a4643                mov       al,[esi+43h]
  004aeac3  84c0                  test      al,al
  004aeac5  0f8453020000          je        near 004AED1Eh
  004aeacb  fec8                  dec       al
  004aeacd  894e5c                mov       [esi+5Ch],ecx
  004aead0  884643                mov       [esi+43h],al
  004aead3  e946040000            jmp       004AEF1Eh
  004aead8  33c9                  xor       ecx,ecx
  004aeada  66394e12              cmp       [esi+12h],cx
  004aeade  7517                  jne       short 004AEAF7h
  004aeae0  8b06                  mov       eax,[esi]
  004aeae2  66894e18              mov       [esi+18h],cx
  004aeae6  0d00000001            or        eax,1000000h
  004aeaeb  66894e1a              mov       [esi+1Ah],cx
  004aeaef  8906                  mov       [esi],eax
  004aeaf1  66c746120100          mov       word [esi+12h],1

LAB_004aeaf7:
  004aeaf7  8b442418              mov       eax,[esp+18h]
  004aeafb  8d90c8000000          lea       edx,[eax+0C8h]
  004aeb01  81e2ff0f0000          and       edx,0FFFh
  004aeb07  81fa90010000          cmp       edx,190h
  004aeb0d  7f0c                  jg        short 004AEB1Bh
  004aeb0f  c6464300              mov       byte [esi+43h],0
  004aeb13  894e5c                mov       [esi+5Ch],ecx
  004aeb16  e903040000            jmp       004AEF1Eh

LAB_004aeb1b:
  004aeb1b  25ff0f0000            and       eax,0FFFh
  004aeb20  3d00080000            cmp       eax,800h
  004aeb25  7d13                  jge       short 004AEB3Ah
  004aeb27  8b442410              mov       eax,[esp+10h]
  004aeb2b  894e5c                mov       [esi+5Ch],ecx
  004aeb2e  83c032                add       eax,32h
  004aeb31  66014632              add       [esi+32h],ax
  004aeb35  e9e4030000            jmp       004AEF1Eh

LAB_004aeb3a:
  004aeb3a  8b542410              mov       edx,[esp+10h]
  004aeb3e  b8ceffffff            mov       eax,0FFFFFFCEh
  004aeb43  2bc2                  sub       eax,edx
  004aeb45  894e5c                mov       [esi+5Ch],ecx
  004aeb48  66014632              add       [esi+32h],ax
  004aeb4c  e9cd030000            jmp       004AEF1Eh
  004aeb51  8b4c2410              mov       ecx,[esp+10h]
  004aeb55  33c0                  xor       eax,eax
  004aeb57  66394612              cmp       [esi+12h],ax
  004aeb5b  7527                  jne       short 004AEB84h
  004aeb5d  8b2e                  mov       ebp,[esi]
  004aeb5f  8d14c9                lea       edx,[ecx+ecx*8]
  004aeb62  c1e204                shl       edx,4
  004aeb65  81cd00000001          or        ebp,1000000h
  004aeb6b  81c200500000          add       edx,5000h
  004aeb71  66894618              mov       [esi+18h],ax
  004aeb75  6689461a              mov       [esi+1Ah],ax
  004aeb79  892e                  mov       [esi],ebp
  004aeb7b  66c746120100          mov       word [esi+12h],1
  004aeb81  89565c                mov       [esi+5Ch],edx

LAB_004aeb84:
  004aeb84  66837e1201            cmp       word [esi+12h],1
  004aeb89  0f858f030000          jne       near 004AEF1Eh
  004aeb8f  0fbf5610              movsx     edx,word [esi+10h]
  004aeb93  39542414              cmp       [esp+14h],edx
  004aeb97  7d0c                  jge       short 004AEBA5h
  004aeb99  c6464300              mov       byte [esi+43h],0
  004aeb9d  89465c                mov       [esi+5Ch],eax
  004aeba0  e979030000            jmp       004AEF1Eh

LAB_004aeba5:
  004aeba5  8a4643                mov       al,[esi+43h]
  004aeba8  84c0                  test      al,al
  004aebaa  7405                  je        short 004AEBB1h
  004aebac  fec8                  dec       al
  004aebae  884643                mov       [esi+43h],al

LAB_004aebb1:
  004aebb1  8b442418              mov       eax,[esp+18h]
  004aebb5  8d9000010000          lea       edx,[eax+100h]
  004aebbb  81e2ff0f0000          and       edx,0FFFh
  004aebc1  81fa00020000          cmp       edx,200h
  004aebc7  0f8e51030000          jle       near 004AEF1Eh
  004aebcd  25ff0f0000            and       eax,0FFFh
  004aebd2  3d00080000            cmp       eax,800h
  004aebd7  7d0d                  jge       short 004AEBE6h
  004aebd9  8d440932              lea       eax,[ecx+ecx+32h]
  004aebdd  66014632              add       [esi+32h],ax
  004aebe1  e938030000            jmp       004AEF1Eh

LAB_004aebe6:
  004aebe6  03c9                  add       ecx,ecx
  004aebe8  b8ceffffff            mov       eax,0FFFFFFCEh
  004aebed  2bc1                  sub       eax,ecx
  004aebef  66014632              add       [esi+32h],ax
  004aebf3  e926030000            jmp       004AEF1Eh
  004aebf8  66837e1201            cmp       word [esi+12h],1
  004aebfd  0f859c000000          jne       near 004AEC9Fh
  004aec03  8a4643                mov       al,[esi+43h]
  004aec06  84c0                  test      al,al
  004aec08  7405                  je        short 004AEC0Fh
  004aec0a  fec8                  dec       al
  004aec0c  884643                mov       [esi+43h],al

LAB_004aec0f:
  004aec0f  8b442410              mov       eax,[esp+10h]
  004aec13  8d14c0                lea       edx,[eax+eax*8]
  004aec16  c1e204                shl       edx,4
  004aec19  8d8200500000          lea       eax,[edx+5000h]
  004aec1f  0fafc8                imul      ecx,eax
  004aec22  89465c                mov       [esi+5Ch],eax
  004aec25  0fbf4610              movsx     eax,word [esi+10h]
  004aec29  c1f90c                sar       ecx,0Ch
  004aec2c  50                    push      eax
  004aec2d  8be9                  mov       ebp,ecx
  004aec2f  e8e86d0900            call      00545A1Ch
  004aec34  0fafc5                imul      eax,ebp
  004aec37  8b0f                  mov       ecx,[edi]
  004aec39  c1f80c                sar       eax,0Ch
  004aec3c  03c8                  add       ecx,eax
  004aec3e  890f                  mov       [edi],ecx
  004aec40  0fbf4e10              movsx     ecx,word [esi+10h]
  004aec44  51                    push      ecx
  004aec45  e8cc6d0900            call      00545A16h
  004aec4a  0fafc5                imul      eax,ebp
  004aec4d  8b5e24                mov       ebx,[esi+24h]
  004aec50  c7465c00000000        mov       dword [esi+5Ch],0
  004aec57  c1f80c                sar       eax,0Ch
  004aec5a  03d8                  add       ebx,eax
  004aec5c  8b442418              mov       eax,[esp+18h]
  004aec60  895e24                mov       [esi+24h],ebx
  004aec63  8d9000010000          lea       edx,[eax+100h]
  004aec69  81e2ff0f0000          and       edx,0FFFh
  004aec6f  81fa00020000          cmp       edx,200h
  004aec75  7e28                  jle       short 004AEC9Fh
  004aec77  25ff0f0000            and       eax,0FFFh
  004aec7c  3d00080000            cmp       eax,800h
  004aec81  7d0a                  jge       short 004AEC8Dh
  004aec83  8b442410              mov       eax,[esp+10h]
  004aec87  8d440032              lea       eax,[eax+eax+32h]
  004aec8b  eb0e                  jmp       short 004AEC9Bh

LAB_004aec8d:
  004aec8d  8b4c2410              mov       ecx,[esp+10h]
  004aec91  b8ceffffff            mov       eax,0FFFFFFCEh
  004aec96  8d1409                lea       edx,[ecx+ecx]
  004aec99  2bc2                  sub       eax,edx

LAB_004aec9b:
  004aec9b  66014632              add       [esi+32h],ax

LAB_004aec9f:
  004aec9f  66837e1200            cmp       word [esi+12h],0
  004aeca4  0f8574020000          jne       near 004AEF1Eh
  004aecaa  33c0                  xor       eax,eax
  004aecac  66c746120100          mov       word [esi+12h],1
  004aecb2  66894618              mov       [esi+18h],ax
  004aecb6  6689461a              mov       [esi+1Ah],ax
  004aecba  810e00000001          or        dword [esi],1000000h
  004aecc0  e959020000            jmp       004AEF1Eh
  004aecc5  668b4e12              mov       cx,[esi+12h]
  004aecc9  6683f901              cmp       cx,1
  004aeccd  751c                  jne       short 004AECEBh
  004aeccf  0fbf4610              movsx     eax,word [esi+10h]
  004aecd3  39442414              cmp       [esp+14h],eax
  004aecd7  7d06                  jge       short 004AECDFh
  004aecd9  c6464300              mov       byte [esi+43h],0
  004aecdd  eb0c                  jmp       short 004AECEBh

LAB_004aecdf:
  004aecdf  8a4643                mov       al,[esi+43h]
  004aece2  84c0                  test      al,al
  004aece4  7405                  je        short 004AECEBh
  004aece6  fec8                  dec       al
  004aece8  884643                mov       [esi+43h],al

LAB_004aeceb:
  004aeceb  33c0                  xor       eax,eax
  004aeced  663bc8                cmp       cx,ax
  004aecf0  0f8528020000          jne       near 004AEF1Eh
  004aecf6  8b3e                  mov       edi,[esi]
  004aecf8  66894618              mov       [esi+18h],ax
  004aecfc  6689461a              mov       [esi+1Ah],ax
  004aed00  8b442410              mov       eax,[esp+10h]
  004aed04  81cf00000001          or        edi,1000000h
  004aed0a  66c746120100          mov       word [esi+12h],1
  004aed10  8d0cc0                lea       ecx,[eax+eax*8]
  004aed13  893e                  mov       [esi],edi
  004aed15  c1e104                shl       ecx,4
  004aed18  81c100500000          add       ecx,5000h

LAB_004aed1e:
  004aed1e  894e5c                mov       [esi+5Ch],ecx
  004aed21  e9f8010000            jmp       004AEF1Eh
  004aed26  33ed                  xor       ebp,ebp
  004aed28  66396e12              cmp       [esi+12h],bp
  004aed2c  0f85f6000000          jne       near 004AEE28h
  004aed32  8b442424              mov       eax,[esp+24h]
  004aed36  8b5620                mov       edx,[esi+20h]
  004aed39  81c200400b00          add       edx,0B4000h
  004aed3f  3b5020                cmp       edx,[eax+20h]
  004aed42  7d09                  jge       short 004AED4Dh
  004aed44  c6464300              mov       byte [esi+43h],0
  004aed48  e9d1010000            jmp       004AEF1Eh

LAB_004aed4d:
  004aed4d  33ff                  xor       edi,edi
  004aed4f  b800f45f00            mov       eax,5FF400h

LAB_004aed54:
  004aed54  39706c                cmp       [eax+6Ch],esi
  004aed57  750c                  jne       short 004AED65h
  004aed59  f70000000080          test      dword [eax],80000000h
  004aed5f  0f85ba070000          jne       near 004AF51Fh

LAB_004aed65:
  004aed65  059c000000            add       eax,9Ch
  004aed6a  47                    inc       edi
  004aed6b  3d80076000            cmp       eax,600780h
  004aed70  7ce2                  jl        short 004AED54h
  004aed72  8b06                  mov       eax,[esi]
  004aed74  a904080000            test      eax,804h
  004aed79  740e                  je        short 004AED89h
  004aed7b  c6464300              mov       byte [esi+43h],0
  004aed7f  25fbf7ffff            and       eax,0FFFFF7FBh
  004aed84  e993010000            jmp       004AEF1Ch

LAB_004aed89:
  004aed89  6aff                  push      0FFFFFFFFh
  004aed8b  55                    push      ebp
  004aed8c  68bd000000            push      0BDh
  004aed91  56                    push      esi
  004aed92  e849bdfcff            call      0047AAE0h
  004aed97  83c410                add       esp,10h
  004aed9a  66396e16              cmp       [esi+16h],bp
  004aed9e  bb01000000            mov       ebx,1
  004aeda3  66895e12              mov       [esi+12h],bx
  004aeda7  7517                  jne       short 004AEDC0h
  004aeda9  e892830900            call      00547140h
  004aedae  99                    cdq
  004aedaf  b903000000            mov       ecx,3
  004aedb4  f7f9                  idiv      ecx
  004aedb6  33ff                  xor       edi,edi
  004aedb8  668b3c55702a5500      mov       di,[edx*2+552A70h]

LAB_004aedc0:
  004aedc0  66395e16              cmp       [esi+16h],bx
  004aedc4  7517                  jne       short 004AEDDDh
  004aedc6  e875830900            call      00547140h
  004aedcb  99                    cdq
  004aedcc  b905000000            mov       ecx,5
  004aedd1  f7f9                  idiv      ecx
  004aedd3  33ff                  xor       edi,edi
  004aedd5  668b3c55782a5500      mov       di,[edx*2+552A78h]

LAB_004aeddd:
  004aeddd  66897e06              mov       [esi+6],di
  004aede1  a1c4975e00            mov       eax,[5E97C4h]
  004aede6  8bd7                  mov       edx,edi
  004aede8  81e7ff000000          and       edi,0FFh
  004aedee  8b889c000000          mov       ecx,[eax+9Ch]
  004aedf4  c1fa08                sar       edx,8
  004aedf7  81e2ff000000          and       edx,0FFh
  004aedfd  8d047f                lea       eax,[edi+edi*2]
  004aee00  c1e004                shl       eax,4
  004aee03  8b0c91                mov       ecx,[ecx+edx*4]
  004aee06  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  004aee0c  8b06                  mov       eax,[esi]
  004aee0e  66895610              mov       [esi+10h],dx
  004aee12  0d04080001            or        eax,1000804h
  004aee17  896e5c                mov       [esi+5Ch],ebp
  004aee1a  66c746180400          mov       word [esi+18h],4
  004aee20  66896e1a              mov       [esi+1Ah],bp
  004aee24  8906                  mov       [esi],eax
  004aee26  eb05                  jmp       short 004AEE2Dh

LAB_004aee28:
  004aee28  bb01000000            mov       ebx,1

LAB_004aee2d:
  004aee2d  66395e12              cmp       [esi+12h],bx
  004aee31  753d                  jne       short 004AEE70h
  004aee33  8b4c2424              mov       ecx,[esp+24h]
  004aee37  8b4620                mov       eax,[esi+20h]
  004aee3a  8b5120                mov       edx,[ecx+20h]
  004aee3d  2bd0                  sub       edx,eax
  004aee3f  81c200400100          add       edx,14000h
  004aee45  c1fa03                sar       edx,3
  004aee48  03d0                  add       edx,eax
  004aee4a  66ff4e10              dec       word [esi+10h]
  004aee4e  66395e10              cmp       [esi+10h],bx
  004aee52  895620                mov       [esi+20h],edx
  004aee55  7d19                  jge       short 004AEE70h
  004aee57  8b06                  mov       eax,[esi]
  004aee59  66c746120200          mov       word [esi+12h],2
  004aee5f  25fbfffffe            and       eax,0FEFFFFFBh
  004aee64  66c746180500          mov       word [esi+18h],5
  004aee6a  66896e1a              mov       [esi+1Ah],bp
  004aee6e  8906                  mov       [esi],eax

LAB_004aee70:
  004aee70  66837e1202            cmp       word [esi+12h],2
  004aee75  7532                  jne       short 004AEEA9h
  004aee77  66837e1a10            cmp       word [esi+1Ah],10h
  004aee7c  752b                  jne       short 004AEEA9h
  004aee7e  33c0                  xor       eax,eax
  004aee80  6aff                  push      0FFFFFFFFh
  004aee82  668b4606              mov       ax,[esi+6]
  004aee86  6aff                  push      0FFFFFFFFh
  004aee88  8bc8                  mov       ecx,eax
  004aee8a  c1f808                sar       eax,8
  004aee8d  81e1ff000000          and       ecx,0FFh
  004aee93  25ff000000            and       eax,0FFh
  004aee98  51                    push      ecx
  004aee99  50                    push      eax
  004aee9a  56                    push      esi
  004aee9b  e8e0ba0800            call      0053A980h
  004aeea0  83c414                add       esp,14h
  004aeea3  66c746120300          mov       word [esi+12h],3

LAB_004aeea9:
  004aeea9  66837e1203            cmp       word [esi+12h],3
  004aeeae  756e                  jne       short 004AEF1Eh
  004aeeb0  8b06                  mov       eax,[esi]
  004aeeb2  a900000002            test      eax,2000000h
  004aeeb7  7465                  je        short 004AEF1Eh
  004aeeb9  c6464300              mov       byte [esi+43h],0
  004aeebd  80e4f7                and       ah,0F7h
  004aeec0  eb5a                  jmp       short 004AEF1Ch
  004aeec2  8b542424              mov       edx,[esp+24h]
  004aeec6  8b4e20                mov       ecx,[esi+20h]
  004aeec9  8b4220                mov       eax,[edx+20h]
  004aeecc  8d9100400b00          lea       edx,[ecx+0B4000h]
  004aeed2  3bd0                  cmp       edx,eax
  004aeed4  7d0a                  jge       short 004AEEE0h
  004aeed6  8b06                  mov       eax,[esi]
  004aeed8  c6464300              mov       byte [esi+43h],0
  004aeedc  24fd                  and       al,0FDh
  004aeede  eb3c                  jmp       short 004AEF1Ch

LAB_004aeee0:
  004aeee0  2bc1                  sub       eax,ecx
  004aeee2  0500800200            add       eax,28000h
  004aeee7  c1f802                sar       eax,2
  004aeeea  03c1                  add       eax,ecx
  004aeeec  668b4e12              mov       cx,[esi+12h]
  004aeef0  6683f901              cmp       cx,1
  004aeef4  894620                mov       [esi+20h],eax
  004aeef7  7511                  jne       short 004AEF0Ah
  004aeef9  8b06                  mov       eax,[esi]
  004aeefb  a900000002            test      eax,2000000h
  004aef00  7408                  je        short 004AEF0Ah
  004aef02  24fd                  and       al,0FDh
  004aef04  c6464300              mov       byte [esi+43h],0
  004aef08  8906                  mov       [esi],eax

LAB_004aef0a:
  004aef0a  6685c9                test      cx,cx
  004aef0d  750f                  jne       short 004AEF1Eh
  004aef0f  8b06                  mov       eax,[esi]
  004aef11  66c746120100          mov       word [esi+12h],1
  004aef17  25fffffffe            and       eax,0FEFFFFFFh

LAB_004aef1c:
  004aef1c  8906                  mov       [esi],eax

LAB_004aef1e:
  004aef1e  8b06                  mov       eax,[esi]
  004aef20  8a4e42                mov       cl,[esi+42h]
  004aef23  a820                  test      al,20h
  004aef25  7468                  je        short 004AEF8Fh
  004aef27  80f90a                cmp       cl,0Ah
  004aef2a  7504                  jne       short 004AEF30h
  004aef2c  a802                  test      al,2
  004aef2e  7446                  je        short 004AEF76h

LAB_004aef30:
  004aef30  e80b820900            call      00547140h
  004aef35  99                    cdq
  004aef36  b932000000            mov       ecx,32h
  004aef3b  8b7e20                mov       edi,[esi+20h]
  004aef3e  f7f9                  idiv      ecx
  004aef40  66035614              add       dx,[esi+14h]
  004aef44  83c264                add       edx,64h
  004aef47  81e2ff0f0000          and       edx,0FFFh
  004aef4d  66895614              mov       [esi+14h],dx
  004aef51  0fbfd2                movsx     edx,dx
  004aef54  52                    push      edx
  004aef55  e8c26a0900            call      00545A1Ch
  004aef5a  8b4e48                mov       ecx,[esi+48h]
  004aef5d  8d0440                lea       eax,[eax+eax*2]
  004aef60  8d0480                lea       eax,[eax+eax*4]
  004aef63  8d1481                lea       edx,[ecx+eax*4]
  004aef66  2bd7                  sub       edx,edi
  004aef68  81c200c00800          add       edx,8C000h
  004aef6e  c1fa03                sar       edx,3
  004aef71  03d7                  add       edx,edi
  004aef73  895620                mov       [esi+20h],edx

LAB_004aef76:
  004aef76  8b4c2424              mov       ecx,[esp+24h]
  004aef7a  8b4164                mov       eax,[ecx+64h]
  004aef7d  8b4920                mov       ecx,[ecx+20h]
  004aef80  33c8                  xor       ecx,eax
  004aef82  f7c100f0ffff          test      ecx,0FFFFF000h
  004aef88  7554                  jne       short 004AEFDEh
  004aef8a  894648                mov       [esi+48h],eax
  004aef8d  eb4f                  jmp       short 004AEFDEh

LAB_004aef8f:
  004aef8f  80f90a                cmp       cl,0Ah
  004aef92  7504                  jne       short 004AEF98h
  004aef94  a802                  test      al,2
  004aef96  7446                  je        short 004AEFDEh

LAB_004aef98:
  004aef98  e8a3810900            call      00547140h
  004aef9d  99                    cdq
  004aef9e  b932000000            mov       ecx,32h
  004aefa3  8b7e20                mov       edi,[esi+20h]
  004aefa6  f7f9                  idiv      ecx
  004aefa8  66035614              add       dx,[esi+14h]
  004aefac  83c264                add       edx,64h
  004aefaf  81e2ff0f0000          and       edx,0FFFh
  004aefb5  66895614              mov       [esi+14h],dx
  004aefb9  0fbfd2                movsx     edx,dx
  004aefbc  52                    push      edx
  004aefbd  e85a6a0900            call      00545A1Ch
  004aefc2  8b4e60                mov       ecx,[esi+60h]
  004aefc5  8d0440                lea       eax,[eax+eax*2]
  004aefc8  8d0480                lea       eax,[eax+eax*4]
  004aefcb  8d1481                lea       edx,[ecx+eax*4]
  004aefce  2bd7                  sub       edx,edi
  004aefd0  81c200c00800          add       edx,8C000h
  004aefd6  c1fa03                sar       edx,3
  004aefd9  03d7                  add       edx,edi
  004aefdb  895620                mov       [esi+20h],edx

LAB_004aefde:
  004aefde  0fbf4632              movsx     eax,word [esi+32h]
  004aefe2  50                    push      eax
  004aefe3  e8346a0900            call      00545A1Ch
  004aefe8  0fbf4e32              movsx     ecx,word [esi+32h]
  004aefec  51                    push      ecx
  004aefed  89442474              mov       [esp+74h],eax
  004aeff1  e8206a0900            call      00545A16h
  004aeff6  8b5650                mov       edx,[esi+50h]
  004aeff9  89442478              mov       [esp+78h],eax
  004aeffd  8bc2                  mov       eax,edx
  004aefff  8b4c241c              mov       ecx,[esp+1Ch]
  004af003  c1f803                sar       eax,3
  004af006  0faf4e5c              imul      ecx,[esi+5Ch]
  004af00a  8bfa                  mov       edi,edx
  004af00c  56                    push      esi
  004af00d  2bf8                  sub       edi,eax
  004af00f  c1fa08                sar       edx,8
  004af012  2bfa                  sub       edi,edx
  004af014  8b5654                mov       edx,[esi+54h]
  004af017  8bc2                  mov       eax,edx
  004af019  8bea                  mov       ebp,edx
  004af01b  c1f808                sar       eax,8
  004af01e  2be8                  sub       ebp,eax
  004af020  897e50                mov       [esi+50h],edi
  004af023  c1fa03                sar       edx,3
  004af026  2bea                  sub       ebp,edx
  004af028  8b5658                mov       edx,[esi+58h]
  004af02b  8bda                  mov       ebx,edx
  004af02d  8bc2                  mov       eax,edx
  004af02f  c1fb08                sar       ebx,8
  004af032  2bc3                  sub       eax,ebx
  004af034  8b5e20                mov       ebx,[esi+20h]
  004af037  c1fa03                sar       edx,3
  004af03a  c1f90c                sar       ecx,0Ch
  004af03d  2bc2                  sub       eax,edx
  004af03f  8bd1                  mov       edx,ecx
  004af041  0faf542474            imul      edx,[esp+74h]
  004af046  0faf4c247c            imul      ecx,[esp+7Ch]
  004af04b  c1fa0c                sar       edx,0Ch
  004af04e  c1f90c                sar       ecx,0Ch
  004af051  894658                mov       [esi+58h],eax
  004af054  03d7                  add       edx,edi
  004af056  8b7e1c                mov       edi,[esi+1Ch]
  004af059  03c8                  add       ecx,eax
  004af05b  8b4624                mov       eax,[esi+24h]
  004af05e  03fa                  add       edi,edx
  004af060  03dd                  add       ebx,ebp
  004af062  03c1                  add       eax,ecx
  004af064  896e54                mov       [esi+54h],ebp
  004af067  897e1c                mov       [esi+1Ch],edi
  004af06a  895e20                mov       [esi+20h],ebx
  004af06d  894624                mov       [esi+24h],eax
  004af070  e8cb480800            call      00533940h
  004af075  8bbe90000000          mov       edi,[esi+90h]
  004af07b  8b06                  mov       eax,[esi]
  004af07d  83c404                add       esp,4
  004af080  81e7fff8ffff          and       edi,0FFFFF8FFh
  004af086  a820                  test      al,20h
  004af088  89be90000000          mov       [esi+90h],edi
  004af08e  8bcf                  mov       ecx,edi
  004af090  740c                  je        short 004AF09Eh
  004af092  81e1ffdfffdf          and       ecx,0DFFFDFFFh
  004af098  898e90000000          mov       [esi+90h],ecx

LAB_004af09e:
  004af09e  8b4e60                mov       ecx,[esi+60h]
  004af0a1  8b5620                mov       edx,[esi+20h]
  004af0a4  3bd1                  cmp       edx,ecx
  004af0a6  7f19                  jg        short 004AF0C1h
  004af0a8  8b9690000000          mov       edx,[esi+90h]
  004af0ae  f6c680                test      dh,80h
  004af0b1  750e                  jne       short 004AF0C1h
  004af0b3  24fe                  and       al,0FEh
  004af0b5  33db                  xor       ebx,ebx
  004af0b7  894e20                mov       [esi+20h],ecx
  004af0ba  8906                  mov       [esi],eax
  004af0bc  895e54                mov       [esi+54h],ebx
  004af0bf  eb02                  jmp       short 004AF0C3h

LAB_004af0c1:
  004af0c1  33db                  xor       ebx,ebx

LAB_004af0c3:
  004af0c3  f7869000000082a80020  test      dword [esi+90h],2000A882h
  004af0cd  0f84c3000000          je        near 004AF196h
  004af0d3  8a4642                mov       al,[esi+42h]
  004af0d6  3c07                  cmp       al,7
  004af0d8  7425                  je        short 004AF0FFh
  004af0da  3c0a                  cmp       al,0Ah
  004af0dc  7421                  je        short 004AF0FFh
  004af0de  c6464207              mov       byte [esi+42h],7
  004af0e2  e859800900            call      00547140h
  004af0e7  99                    cdq
  004af0e8  b928000000            mov       ecx,28h
  004af0ed  66895e12              mov       [esi+12h],bx
  004af0f1  f7f9                  idiv      ecx
  004af0f3  66c746105000          mov       word [esi+10h],50h
  004af0f9  80c214                add       dl,14h
  004af0fc  885643                mov       [esi+43h],dl

LAB_004af0ff:
  004af0ff  8b16                  mov       edx,[esi]
  004af101  8b442460              mov       eax,[esp+60h]
  004af105  8b4c2448              mov       ecx,[esp+48h]
  004af109  83e2fd                and       edx,0FFFFFFFDh
  004af10c  894624                mov       [esi+24h],eax
  004af10f  8b44243c              mov       eax,[esp+3Ch]
  004af113  8916                  mov       [esi],edx
  004af115  8b542458              mov       edx,[esp+58h]
  004af119  25ffff0000            and       eax,0FFFFh
  004af11e  89561c                mov       [esi+1Ch],edx
  004af121  8b54244c              mov       edx,[esp+4Ch]
  004af125  894e7c                mov       [esi+7Ch],ecx
  004af128  8b4c2444              mov       ecx,[esp+44h]
  004af12c  898690000000          mov       [esi+90h],eax
  004af132  8b442434              mov       eax,[esp+34h]
  004af136  899680000000          mov       [esi+80h],edx
  004af13c  8b542450              mov       edx,[esp+50h]
  004af140  898684000000          mov       [esi+84h],eax
  004af146  8b442440              mov       eax,[esp+40h]
  004af14a  898e88000000          mov       [esi+88h],ecx
  004af150  8b4c2438              mov       ecx,[esp+38h]
  004af154  6882a80020            push      2000A882h
  004af159  56                    push      esi
  004af15a  89968c000000          mov       [esi+8Ch],edx
  004af160  894664                mov       [esi+64h],eax
  004af163  894e60                mov       [esi+60h],ecx
  004af166  e8f56c0200            call      004D5E60h
  004af16b  83c408                add       esp,8
  004af16e  663bc3                cmp       ax,bx
  004af171  7515                  jne       short 004AF188h
  004af173  e8c87f0900            call      00547140h
  004af178  99                    cdq
  004af179  33c2                  xor       eax,edx
  004af17b  2bc2                  sub       eax,edx
  004af17d  83e001                and       eax,1
  004af180  33c2                  xor       eax,edx
  004af182  2bc2                  sub       eax,edx
  004af184  8d4400ff              lea       eax,[eax+eax-1]

LAB_004af188:
  004af188  8b542410              mov       edx,[esp+10h]
  004af18c  83c240                add       edx,40h
  004af18f  0fafd0                imul      edx,eax
  004af192  66015632              add       [esi+32h],dx

LAB_004af196:
  004af196  6aff                  push      0FFFFFFFFh
  004af198  6aff                  push      0FFFFFFFFh
  004af19a  6aff                  push      0FFFFFFFFh
  004af19c  56                    push      esi
  004af19d  e86e460100            call      004C3810h
  004af1a2  8b44242c              mov       eax,[esp+2Ch]
  004af1a6  83c410                add       esp,10h
  004af1a9  3bc3                  cmp       eax,ebx
  004af1ab  7517                  jne       short 004AF1C4h
  004af1ad  668b842486000000      mov       ax,[esp+86h]
  004af1b5  66894632              mov       [esi+32h],ax
  004af1b9  5f                    pop       edi
  004af1ba  5e                    pop       esi
  004af1bb  5d                    pop       ebp
  004af1bc  5b                    pop       ebx
  004af1bd  81c488000000          add       esp,88h
  004af1c3  c3                    ret

LAB_004af1c4:
  004af1c4  385e43                cmp       [esi+43h],bl
  004af1c7  0f8547030000          jne       near 004AF514h
  004af1cd  8b466c                mov       eax,[esi+6Ch]
  004af1d0  8b4e1c                mov       ecx,[esi+1Ch]
  004af1d3  8b5624                mov       edx,[esi+24h]
  004af1d6  8b781c                mov       edi,[eax+1Ch]
  004af1d9  8b6824                mov       ebp,[eax+24h]
  004af1dc  2bf9                  sub       edi,ecx
  004af1de  2bea                  sub       ebp,edx
  004af1e0  c1ff0c                sar       edi,0Ch
  004af1e3  c1fd0c                sar       ebp,0Ch
  004af1e6  8bcf                  mov       ecx,edi
  004af1e8  8bd5                  mov       edx,ebp
  004af1ea  0fafcf                imul      ecx,edi
  004af1ed  0fafd5                imul      edx,ebp
  004af1f0  03ca                  add       ecx,edx
  004af1f2  51                    push      ecx
  004af1f3  e818680900            call      00545A10h
  004af1f8  55                    push      ebp
  004af1f9  57                    push      edi
  004af1fa  8944241c              mov       [esp+1Ch],eax
  004af1fe  e81f680900            call      00545A22h
  004af203  668b4e32              mov       cx,[esi+32h]
  004af207  66895e12              mov       [esi+12h],bx
  004af20b  2bc1                  sub       eax,ecx
  004af20d  25ff0f0000            and       eax,0FFFh
  004af212  89442418              mov       [esp+18h],eax
  004af216  8b442414              mov       eax,[esp+14h]
  004af21a  3d96000000            cmp       eax,96h
  004af21f  0f8d14010000          jge       near 004AF339h
  004af225  e8167f0900            call      00547140h
  004af22a  99                    cdq
  004af22b  b964000000            mov       ecx,64h
  004af230  f7f9                  idiv      ecx
  004af232  8bea                  mov       ebp,edx
  004af234  83fd32                cmp       ebp,32h
  004af237  0f8d84000000          jge       near 004AF2C1h
  004af23d  8b542418              mov       edx,[esp+18h]
  004af241  8b7c2420              mov       edi,[esp+20h]
  004af245  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004af24c  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004af253  25ff0f0000            and       eax,0FFFh
  004af258  3bc1                  cmp       eax,ecx
  004af25a  7f5b                  jg        short 004AF2B7h
  004af25c  e8df7e0900            call      00547140h
  004af261  99                    cdq
  004af262  8d4c3f01              lea       ecx,[edi+edi+1]
  004af266  f7f9                  idiv      ecx
  004af268  8b442414              mov       eax,[esp+14h]
  004af26c  83c264                add       edx,64h
  004af26f  3bc2                  cmp       eax,edx
  004af271  7d44                  jge       short 004AF2B7h
  004af273  83fd1e                cmp       ebp,1Eh
  004af276  7d27                  jge       short 004AF29Fh
  004af278  8b16                  mov       edx,[esi]
  004af27a  c646420b              mov       byte [esi+42h],0Bh
  004af27e  81e2fffffffe          and       edx,0FEFFFFFFh
  004af284  c6464301              mov       byte [esi+43h],1
  004af288  83ca02                or        edx,2
  004af28b  895e5c                mov       [esi+5Ch],ebx
  004af28e  8916                  mov       [esi],edx
  004af290  66c746180300          mov       word [esi+18h],3
  004af296  66895e1a              mov       [esi+1Ah],bx
  004af29a  e99e000000            jmp       004AF33Dh

LAB_004af29f:
  004af29f  885e42                mov       [esi+42h],bl
  004af2a2  e8997e0900            call      00547140h
  004af2a7  99                    cdq
  004af2a8  b928000000            mov       ecx,28h
  004af2ad  f7f9                  idiv      ecx
  004af2af  885643                mov       [esi+43h],dl
  004af2b2  e986000000            jmp       004AF33Dh

LAB_004af2b7:
  004af2b7  c6464201              mov       byte [esi+42h],1
  004af2bb  c6464301              mov       byte [esi+43h],1
  004af2bf  eb7c                  jmp       short 004AF33Dh

LAB_004af2c1:
  004af2c1  83fd46                cmp       ebp,46h
  004af2c4  7d3e                  jge       short 004AF304h
  004af2c6  c6464203              mov       byte [esi+42h],3
  004af2ca  66895e12              mov       [esi+12h],bx
  004af2ce  e86d7e0900            call      00547140h
  004af2d3  99                    cdq
  004af2d4  b90a000000            mov       ecx,0Ah
  004af2d9  8b7c2420              mov       edi,[esp+20h]
  004af2dd  f7f9                  idiv      ecx
  004af2df  8bc7                  mov       eax,edi
  004af2e1  d1f8                  sar       eax,1
  004af2e3  02d0                  add       dl,al
  004af2e5  80c214                add       dl,14h
  004af2e8  885643                mov       [esi+43h],dl
  004af2eb  e8507e0900            call      00547140h
  004af2f0  99                    cdq
  004af2f1  33c2                  xor       eax,edx
  004af2f3  2bc2                  sub       eax,edx
  004af2f5  25ff0f0000            and       eax,0FFFh
  004af2fa  33c2                  xor       eax,edx
  004af2fc  2bc2                  sub       eax,edx
  004af2fe  66894610              mov       [esi+10h],ax
  004af302  eb39                  jmp       short 004AF33Dh

LAB_004af304:
  004af304  8b542410              mov       edx,[esp+10h]
  004af308  b964000000            mov       ecx,64h
  004af30d  d1fa                  sar       edx,1
  004af30f  2bcd                  sub       ecx,ebp
  004af311  83c20a                add       edx,0Ah
  004af314  3bca                  cmp       ecx,edx
  004af316  7d0e                  jge       short 004AF326h
  004af318  c646420a              mov       byte [esi+42h],0Ah
  004af31c  c6464301              mov       byte [esi+43h],1
  004af320  66895e12              mov       [esi+12h],bx
  004af324  eb13                  jmp       short 004AF339h

LAB_004af326:
  004af326  885e42                mov       [esi+42h],bl
  004af329  e8127e0900            call      00547140h
  004af32e  99                    cdq
  004af32f  b928000000            mov       ecx,28h
  004af334  f7f9                  idiv      ecx
  004af336  885643                mov       [esi+43h],dl

LAB_004af339:
  004af339  8b7c2420              mov       edi,[esp+20h]

LAB_004af33d:
  004af33d  8b442414              mov       eax,[esp+14h]
  004af341  3d58020000            cmp       eax,258h
  004af346  0f8de4000000          jge       near 004AF430h
  004af34c  3d96000000            cmp       eax,96h
  004af351  0f8cd9000000          jl        near 004AF430h
  004af357  e8e47d0900            call      00547140h
  004af35c  99                    cdq
  004af35d  b964000000            mov       ecx,64h
  004af362  f7f9                  idiv      ecx
  004af364  83fa14                cmp       edx,14h
  004af367  7d29                  jge       short 004AF392h
  004af369  8b542418              mov       edx,[esp+18h]
  004af36d  81c200040000          add       edx,400h
  004af373  81e2ff0f0000          and       edx,0FFFh
  004af379  81fa00080000          cmp       edx,800h
  004af37f  0f8c8a000000          jl        near 004AF40Fh
  004af385  c6464201              mov       byte [esi+42h],1
  004af389  c6464301              mov       byte [esi+43h],1
  004af38d  e99e000000            jmp       004AF430h

LAB_004af392:
  004af392  83fa28                cmp       edx,28h
  004af395  7d25                  jge       short 004AF3BCh
  004af397  c6464202              mov       byte [esi+42h],2
  004af39b  e8a07d0900            call      00547140h
  004af3a0  99                    cdq
  004af3a1  b90a000000            mov       ecx,0Ah
  004af3a6  66c746105000          mov       word [esi+10h],50h
  004af3ac  f7f9                  idiv      ecx
  004af3ae  8bc7                  mov       eax,edi
  004af3b0  d1f8                  sar       eax,1
  004af3b2  02d0                  add       dl,al
  004af3b4  80c214                add       dl,14h
  004af3b7  885643                mov       [esi+43h],dl
  004af3ba  eb74                  jmp       short 004AF430h

LAB_004af3bc:
  004af3bc  83fa50                cmp       edx,50h
  004af3bf  7d3a                  jge       short 004AF3FBh
  004af3c1  c6464203              mov       byte [esi+42h],3
  004af3c5  66895e12              mov       [esi+12h],bx
  004af3c9  e8727d0900            call      00547140h
  004af3ce  99                    cdq
  004af3cf  b90a000000            mov       ecx,0Ah
  004af3d4  f7f9                  idiv      ecx
  004af3d6  8bc7                  mov       eax,edi
  004af3d8  d1f8                  sar       eax,1
  004af3da  02d0                  add       dl,al
  004af3dc  80c214                add       dl,14h
  004af3df  885643                mov       [esi+43h],dl
  004af3e2  e8597d0900            call      00547140h
  004af3e7  99                    cdq
  004af3e8  33c2                  xor       eax,edx
  004af3ea  2bc2                  sub       eax,edx
  004af3ec  25ff0f0000            and       eax,0FFFh
  004af3f1  33c2                  xor       eax,edx
  004af3f3  2bc2                  sub       eax,edx
  004af3f5  66894610              mov       [esi+10h],ax
  004af3f9  eb35                  jmp       short 004AF430h

LAB_004af3fb:
  004af3fb  b964000000            mov       ecx,64h
  004af400  2bca                  sub       ecx,edx
  004af402  8b542410              mov       edx,[esp+10h]
  004af406  d1fa                  sar       edx,1
  004af408  83c20a                add       edx,0Ah
  004af40b  3bca                  cmp       ecx,edx
  004af40d  7d0e                  jge       short 004AF41Dh

LAB_004af40f:
  004af40f  c646420a              mov       byte [esi+42h],0Ah
  004af413  c6464301              mov       byte [esi+43h],1
  004af417  66895e12              mov       [esi+12h],bx
  004af41b  eb13                  jmp       short 004AF430h

LAB_004af41d:
  004af41d  885e42                mov       [esi+42h],bl
  004af420  e81b7d0900            call      00547140h
  004af425  99                    cdq
  004af426  b928000000            mov       ecx,28h
  004af42b  f7f9                  idiv      ecx
  004af42d  885643                mov       [esi+43h],dl

LAB_004af430:
  004af430  817c241458020000      cmp       dword [esp+14h],258h
  004af438  0f8cd6000000          jl        near 004AF514h
  004af43e  e8fd7c0900            call      00547140h
  004af443  99                    cdq
  004af444  b964000000            mov       ecx,64h
  004af449  f7f9                  idiv      ecx
  004af44b  83fa32                cmp       edx,32h
  004af44e  7d6f                  jge       short 004AF4BFh
  004af450  8b442410              mov       eax,[esp+10h]
  004af454  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004af458  3bd1                  cmp       edx,ecx
  004af45a  7d2e                  jge       short 004AF48Ah
  004af45c  c6464202              mov       byte [esi+42h],2
  004af460  e8db7c0900            call      00547140h
  004af465  99                    cdq
  004af466  b90a000000            mov       ecx,0Ah
  004af46b  66c746106400          mov       word [esi+10h],64h
  004af471  f7f9                  idiv      ecx
  004af473  8bc7                  mov       eax,edi
  004af475  d1f8                  sar       eax,1
  004af477  02d0                  add       dl,al
  004af479  80c214                add       dl,14h
  004af47c  885643                mov       [esi+43h],dl
  004af47f  5f                    pop       edi
  004af480  5e                    pop       esi
  004af481  5d                    pop       ebp
  004af482  5b                    pop       ebx
  004af483  81c488000000          add       esp,88h
  004af489  c3                    ret

LAB_004af48a:
  004af48a  885e42                mov       [esi+42h],bl
  004af48d  e8ae7c0900            call      00547140h
  004af492  99                    cdq
  004af493  b90a000000            mov       ecx,0Ah
  004af498  66895e12              mov       [esi+12h],bx
  004af49c  f7f9                  idiv      ecx
  004af49e  8b4c2410              mov       ecx,[esp+10h]
  004af4a2  8bc7                  mov       eax,edi
  004af4a4  c1f802                sar       eax,2
  004af4a7  02d0                  add       dl,al
  004af4a9  885643                mov       [esi+43h],dl
  004af4ac  8d540950              lea       edx,[ecx+ecx+50h]
  004af4b0  66895610              mov       [esi+10h],dx
  004af4b4  5f                    pop       edi
  004af4b5  5e                    pop       esi
  004af4b6  5d                    pop       ebp
  004af4b7  5b                    pop       ebx
  004af4b8  81c488000000          add       esp,88h
  004af4be  c3                    ret

LAB_004af4bf:
  004af4bf  83fa4b                cmp       edx,4Bh
  004af4c2  7d43                  jge       short 004AF507h
  004af4c4  c6464203              mov       byte [esi+42h],3
  004af4c8  66895e12              mov       [esi+12h],bx
  004af4cc  e86f7c0900            call      00547140h
  004af4d1  99                    cdq
  004af4d2  b90a000000            mov       ecx,0Ah
  004af4d7  f7f9                  idiv      ecx
  004af4d9  8bc7                  mov       eax,edi
  004af4db  d1f8                  sar       eax,1
  004af4dd  02d0                  add       dl,al
  004af4df  80c214                add       dl,14h
  004af4e2  885643                mov       [esi+43h],dl
  004af4e5  e8567c0900            call      00547140h
  004af4ea  99                    cdq
  004af4eb  33c2                  xor       eax,edx
  004af4ed  2bc2                  sub       eax,edx
  004af4ef  25ff0f0000            and       eax,0FFFh
  004af4f4  33c2                  xor       eax,edx
  004af4f6  2bc2                  sub       eax,edx
  004af4f8  66894610              mov       [esi+10h],ax
  004af4fc  5f                    pop       edi
  004af4fd  5e                    pop       esi
  004af4fe  5d                    pop       ebp
  004af4ff  5b                    pop       ebx
  004af500  81c488000000          add       esp,88h
  004af506  c3                    ret

LAB_004af507:
  004af507  83fa3c                cmp       edx,3Ch
  004af50a  7d08                  jge       short 004AF514h
  004af50c  c6464201              mov       byte [esi+42h],1
  004af510  c6464301              mov       byte [esi+43h],1

LAB_004af514:
  004af514  5f                    pop       edi
  004af515  5e                    pop       esi
  004af516  5d                    pop       ebp
  004af517  5b                    pop       ebx
  004af518  81c488000000          add       esp,88h
  004af51e  c3                    ret

LAB_004af51f:
  004af51f  c6464300              mov       byte [esi+43h],0
  004af523  e9f6f9ffff            jmp       004AEF1Eh
  004af528  8bea                  mov       ebp,edx
  004af52a  4a                    dec       edx
  004af52b  00d8                  add       al,bl
  004af52d  ea4a0051eb4a00        jmp       004Ah:0EB51004Ah
  004af534  f8                    clc
  004af535  eb4a                  jmp       short 004AF581h
  004af537  001e                  add       [esi],bl
  004af539  ef                    out       dx,eax
  004af53a  4a                    dec       edx
  004af53b  001e                  add       [esi],bl
  004af53d  ef                    out       dx,eax
  004af53e  4a                    dec       edx
  004af53f  001e                  add       [esi],bl
  004af541  ef                    out       dx,eax
  004af542  4a                    dec       edx
  004af543  00c5                  add       ch,al
  004af545  ec                    in        al,dx
  004af546  4a                    dec       edx
  004af547  001e                  add       [esi],bl
  004af549  ef                    out       dx,eax
  004af54a  4a                    dec       edx
  004af54b  001e                  add       [esi],bl
  004af54d  ef                    out       dx,eax
  004af54e  4a                    dec       edx
  004af54f  0026                  add       [esi],ah
  004af551  ed                    in        eax,dx
  004af552  4a                    dec       edx
  004af553  00c2                  add       dl,al
  004af555  ee                    out       dx,al
  004af556  4a                    dec       edx
  004af557  009090909090          add       [eax-6F6F6F70h],dl
  004af55d  90                    nop
  004af55e  90                    nop
  004af55f  90                    nop

68 cible(s) de saut interne : 0x004ae88e, 0x004ae892, 0x004ae894, 0x004ae9d8, 0x004ae9fa, 0x004aea5f, 0x004aea63, 0x004aea76, 0x004aeaad, 0x004aeac0, 0x004aeaf7, 0x004aeb1b, 0x004aeb3a, 0x004aeb84, 0x004aeba5, 0x004aebb1, 0x004aebe6, 0x004aec0f, 0x004aec8d, 0x004aec9b, 0x004aec9f, 0x004aecdf, 0x004aeceb, 0x004aed1e, 0x004aed4d, 0x004aed54, 0x004aed65, 0x004aed89, 0x004aedc0, 0x004aeddd, 0x004aee28, 0x004aee2d, 0x004aee70, 0x004aeea9, 0x004aeee0, 0x004aef0a, 0x004aef1c, 0x004aef1e, 0x004aef30, 0x004aef76, 0x004aef8f, 0x004aef98, 0x004aefde, 0x004af09e, 0x004af0c1, 0x004af0c3, 0x004af0ff, 0x004af188, 0x004af196, 0x004af1c4, 0x004af29f, 0x004af2b7, 0x004af2c1, 0x004af304, 0x004af326, 0x004af339, 0x004af33d, 0x004af392, 0x004af3bc, 0x004af3fb, 0x004af40f, 0x004af41d, 0x004af430, 0x004af48a, 0x004af4bf, 0x004af507, 0x004af514, 0x004af51f
