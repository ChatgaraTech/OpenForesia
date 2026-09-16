Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004901b0  section .text  offset fichier 0x8f5b0  4320 octets

  004901b0  81ec88000000          sub       esp,88h
  004901b6  53                    push      ebx
  004901b7  55                    push      ebp
  004901b8  56                    push      esi
  004901b9  8bb42498000000        mov       esi,[esp+98h]
  004901c0  b850000000            mov       eax,50h
  004901c5  33ed                  xor       ebp,ebp
  004901c7  668944242c            mov       [esp+2Ch],ax
  004901cc  6689442432            mov       [esp+32h],ax
  004901d1  8a4641                mov       al,[esi+41h]
  004901d4  57                    push      edi
  004901d5  84c0                  test      al,al
  004901d7  66896c242c            mov       [esp+2Ch],bp
  004901dc  66c744242e1400        mov       word [esp+2Eh],14h
  004901e3  66c74424326400        mov       word [esp+32h],64h
  004901ea  66896c2434            mov       [esp+34h],bp
  004901ef  7533                  jne       short 00490224h
  004901f1  55                    push      ebp
  004901f2  6a03                  push      3
  004901f4  56                    push      esi
  004901f5  c6464101              mov       byte [esi+41h],1
  004901f9  e892100000            call      00491290h
  004901fe  8b4620                mov       eax,[esi+20h]
  00490201  83c40c                add       esp,0Ch
  00490204  894648                mov       [esi+48h],eax
  00490207  668b4616              mov       ax,[esi+16h]
  0049020b  663d0a00              cmp       ax,0Ah
  0049020f  7c0d                  jl        short 0049021Eh
  00490211  83c0f6                add       eax,0FFFFFFF6h
  00490214  66894616              mov       [esi+16h],ax
  00490218  8b06                  mov       eax,[esi]
  0049021a  24df                  and       al,0DFh
  0049021c  eb04                  jmp       short 00490222h

LAB_0049021e:
  0049021e  8b06                  mov       eax,[esi]
  00490220  0c20                  or        al,20h

LAB_00490222:
  00490222  8906                  mov       [esi],eax

LAB_00490224:
  00490224  8b16                  mov       edx,[esi]
  00490226  8b4e1c                mov       ecx,[esi+1Ch]
  00490229  8b4664                mov       eax,[esi+64h]
  0049022c  8d7e1c                lea       edi,[esi+1Ch]
  0049022f  83e2fe                and       edx,0FFFFFFFEh
  00490232  894c245c              mov       [esp+5Ch],ecx
  00490236  8b4e60                mov       ecx,[esi+60h]
  00490239  8916                  mov       [esi],edx
  0049023b  8b5624                mov       edx,[esi+24h]
  0049023e  8944243c              mov       [esp+3Ch],eax
  00490242  8b467c                mov       eax,[esi+7Ch]
  00490245  89542464              mov       [esp+64h],edx
  00490249  668b5632              mov       dx,[esi+32h]
  0049024d  894c2440              mov       [esp+40h],ecx
  00490251  668b8e90000000        mov       cx,[esi+90h]
  00490258  6689942486000000      mov       [esp+86h],dx
  00490260  8b9680000000          mov       edx,[esi+80h]
  00490266  8b5e6c                mov       ebx,[esi+6Ch]
  00490269  89442458              mov       [esp+58h],eax
  0049026d  8b8684000000          mov       eax,[esi+84h]
  00490273  66894c2448            mov       [esp+48h],cx
  00490278  8b8e88000000          mov       ecx,[esi+88h]
  0049027e  89542438              mov       [esp+38h],edx
  00490282  8b968c000000          mov       edx,[esi+8Ch]
  00490288  8944244c              mov       [esp+4Ch],eax
  0049028c  894c2454              mov       [esp+54h],ecx
  00490290  8d442428              lea       eax,[esp+28h]
  00490294  89542450              mov       [esp+50h],edx
  00490298  8d4c2468              lea       ecx,[esp+68h]
  0049029c  50                    push      eax
  0049029d  8d542470              lea       edx,[esp+70h]
  004902a1  51                    push      ecx
  004902a2  52                    push      edx
  004902a3  56                    push      esi
  004902a4  895c2434              mov       [esp+34h],ebx
  004902a8  e8d3860a00            call      00538980h
  004902ad  83c410                add       esp,10h
  004902b0  8d442444              lea       eax,[esp+44h]
  004902b4  8d4c241c              lea       ecx,[esp+1Ch]
  004902b8  8d542414              lea       edx,[esp+14h]
  004902bc  50                    push      eax
  004902bd  51                    push      ecx
  004902be  8d442420              lea       eax,[esp+20h]
  004902c2  52                    push      edx
  004902c3  8d4c241c              lea       ecx,[esp+1Ch]
  004902c7  50                    push      eax
  004902c8  51                    push      ecx
  004902c9  56                    push      esi
  004902ca  e8515c0a00            call      00535F20h
  004902cf  8b4c2428              mov       ecx,[esp+28h]
  004902d3  8b54245c              mov       edx,[esp+5Ch]
  004902d7  83c418                add       esp,18h
  004902da  b840000000            mov       eax,40h
  004902df  2bc1                  sub       eax,ecx
  004902e1  52                    push      edx
  004902e2  56                    push      esi
  004902e3  89442428              mov       [esp+28h],eax
  004902e7  e8f45a0400            call      004D5DE0h
  004902ec  8b07                  mov       eax,[edi]
  004902ee  8b531c                mov       edx,[ebx+1Ch]
  004902f1  8b4e24                mov       ecx,[esi+24h]
  004902f4  2bc2                  sub       eax,edx
  004902f6  8b5324                mov       edx,[ebx+24h]
  004902f9  83c408                add       esp,8
  004902fc  c1f80c                sar       eax,0Ch
  004902ff  2bca                  sub       ecx,edx
  00490301  8bd0                  mov       edx,eax
  00490303  c1f90c                sar       ecx,0Ch
  00490306  0fafd0                imul      edx,eax
  00490309  8bc1                  mov       eax,ecx
  0049030b  0fafc1                imul      eax,ecx
  0049030e  03d0                  add       edx,eax
  00490310  52                    push      edx
  00490311  e8fa560b00            call      00545A10h
  00490316  89442418              mov       [esp+18h],eax
  0049031a  8b44241c              mov       eax,[esp+1Ch]
  0049031e  3bc5                  cmp       eax,ebp
  00490320  0f842e090000          je        near 00490C54h
  00490326  8b0e                  mov       ecx,[esi]
  00490328  33d2                  xor       edx,edx
  0049032a  8a5604                mov       dl,[esi+4]
  0049032d  81e1fffffffd          and       ecx,0FDFFFFFFh
  00490333  0fbf6e18              movsx     ebp,word [esi+18h]
  00490337  890e                  mov       [esi],ecx
  00490339  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00490340  f7c100000001          test      ecx,1000000h
  00490346  8b14aa                mov       edx,[edx+ebp*4]
  00490349  741d                  je        short 00490368h
  0049034b  66ff461a              inc       word [esi+1Ah]
  0049034f  668b4e1a              mov       cx,[esi+1Ah]
  00490353  33ed                  xor       ebp,ebp
  00490355  668b6a16              mov       bp,[edx+16h]
  00490359  0fbfc9                movsx     ecx,cx
  0049035c  3bcd                  cmp       ecx,ebp
  0049035e  7c2e                  jl        short 0049038Eh
  00490360  66c7461a0000          mov       word [esi+1Ah],0
  00490366  eb26                  jmp       short 0049038Eh

LAB_00490368:
  00490368  66ff461a              inc       word [esi+1Ah]
  0049036c  668b6e1a              mov       bp,[esi+1Ah]
  00490370  33db                  xor       ebx,ebx
  00490372  668b5a16              mov       bx,[edx+16h]
  00490376  0fbfd5                movsx     edx,bp
  00490379  3bd3                  cmp       edx,ebx
  0049037b  7c0d                  jl        short 0049038Ah
  0049037d  4d                    dec       ebp
  0049037e  81c900000002          or        ecx,2000000h
  00490384  66896e1a              mov       [esi+1Ah],bp
  00490388  890e                  mov       [esi],ecx

LAB_0049038a:
  0049038a  8b5c2424              mov       ebx,[esp+24h]

LAB_0049038e:
  0049038e  f60602                test      byte [esi],2
  00490391  7475                  je        short 00490408h
  00490393  807e420d              cmp       byte [esi+42h],0Dh
  00490397  0f841e0e0000          je        near 004911BBh
  0049039d  8d44242c              lea       eax,[esp+2Ch]
  004903a1  6a00                  push      0
  004903a3  50                    push      eax
  004903a4  57                    push      edi
  004903a5  56                    push      esi
  004903a6  e885980800            call      00519C30h
  004903ab  8be8                  mov       ebp,eax
  004903ad  a1b8e05d00            mov       eax,[5DE0B8h]
  004903b2  83c410                add       esp,10h
  004903b5  85c0                  test      eax,eax
  004903b7  7434                  je        short 004903EDh
  004903b9  8b4d00                mov       ecx,[ebp]
  004903bc  55                    push      ebp
  004903bd  83c940                or        ecx,40h
  004903c0  56                    push      esi
  004903c1  66c785960000000400    mov       word [ebp+96h],4
  004903ca  894d00                mov       [ebp],ecx
  004903cd  e8ae970a00            call      00539B80h
  004903d2  83c408                add       esp,8
  004903d5  83c51c                add       ebp,1Ch
  004903d8  55                    push      ebp
  004903d9  6800180000            push      1800h
  004903de  6800030000            push      300h
  004903e3  6a03                  push      3
  004903e5  e836d70a00            call      0053DB20h
  004903ea  83c410                add       esp,10h

LAB_004903ed:
  004903ed  8b06                  mov       eax,[esi]
  004903ef  a900000002            test      eax,2000000h
  004903f4  740e                  je        short 00490404h
  004903f6  807e420c              cmp       byte [esi+42h],0Ch
  004903fa  7408                  je        short 00490404h
  004903fc  24fd                  and       al,0FDh
  004903fe  c6464300              mov       byte [esi+43h],0
  00490402  8906                  mov       [esi],eax

LAB_00490404:
  00490404  8b44241c              mov       eax,[esp+1Ch]

LAB_00490408:
  00490408  33c9                  xor       ecx,ecx
  0049040a  8a4e42                mov       cl,[esi+42h]
  0049040d  83f90d                cmp       ecx,0Dh
  00490410  0f8784070000          ja        near 00490B9Ah
  00490416  ff248d50124900        jmp       dword [ecx*4+491250h]
  0049041d  33c9                  xor       ecx,ecx
  0049041f  66394e12              cmp       [esi+12h],cx
  00490423  751a                  jne       short 0049043Fh
  00490425  8b06                  mov       eax,[esi]
  00490427  66894e18              mov       [esi+18h],cx
  0049042b  0d00000001            or        eax,1000000h
  00490430  66894e1a              mov       [esi+1Ah],cx
  00490434  8906                  mov       [esi],eax
  00490436  66c746120100          mov       word [esi+12h],1
  0049043c  894e5c                mov       [esi+5Ch],ecx

LAB_0049043f:
  0049043f  0fbf4610              movsx     eax,word [esi+10h]
  00490443  39442418              cmp       [esp+18h],eax
  00490447  7d09                  jge       short 00490452h
  00490449  c6464300              mov       byte [esi+43h],0
  0049044d  e948070000            jmp       00490B9Ah

LAB_00490452:
  00490452  8a4643                mov       al,[esi+43h]
  00490455  84c0                  test      al,al
  00490457  0f845e020000          je        near 004906BBh
  0049045d  fec8                  dec       al
  0049045f  894e5c                mov       [esi+5Ch],ecx
  00490462  884643                mov       [esi+43h],al
  00490465  e930070000            jmp       00490B9Ah
  0049046a  33c9                  xor       ecx,ecx
  0049046c  66394e12              cmp       [esi+12h],cx
  00490470  7517                  jne       short 00490489h
  00490472  8b06                  mov       eax,[esi]
  00490474  66894e18              mov       [esi+18h],cx
  00490478  0d00000001            or        eax,1000000h
  0049047d  66894e1a              mov       [esi+1Ah],cx
  00490481  8906                  mov       [esi],eax
  00490483  66c746120100          mov       word [esi+12h],1

LAB_00490489:
  00490489  8b442414              mov       eax,[esp+14h]
  0049048d  8d90c8000000          lea       edx,[eax+0C8h]
  00490493  81e2ff0f0000          and       edx,0FFFh
  00490499  81fa90010000          cmp       edx,190h
  0049049f  7f0c                  jg        short 004904ADh
  004904a1  c6464300              mov       byte [esi+43h],0
  004904a5  894e5c                mov       [esi+5Ch],ecx
  004904a8  e9ed060000            jmp       00490B9Ah

LAB_004904ad:
  004904ad  25ff0f0000            and       eax,0FFFh
  004904b2  3d00080000            cmp       eax,800h
  004904b7  7d15                  jge       short 004904CEh
  004904b9  8b442410              mov       eax,[esp+10h]
  004904bd  894e5c                mov       [esi+5Ch],ecx
  004904c0  0580000000            add       eax,80h
  004904c5  66014632              add       [esi+32h],ax
  004904c9  e9cc060000            jmp       00490B9Ah

LAB_004904ce:
  004904ce  8b542410              mov       edx,[esp+10h]
  004904d2  b880ffffff            mov       eax,0FFFFFF80h
  004904d7  2bc2                  sub       eax,edx
  004904d9  894e5c                mov       [esi+5Ch],ecx
  004904dc  66014632              add       [esi+32h],ax
  004904e0  e9b5060000            jmp       00490B9Ah
  004904e5  8b4c2410              mov       ecx,[esp+10h]
  004904e9  33d2                  xor       edx,edx
  004904eb  66395612              cmp       [esi+12h],dx
  004904ef  b801000000            mov       eax,1
  004904f4  7525                  jne       short 0049051Bh
  004904f6  8b3e                  mov       edi,[esi]
  004904f8  66894618              mov       [esi+18h],ax
  004904fc  81cf00000001          or        edi,1000000h
  00490502  6689561a              mov       [esi+1Ah],dx
  00490506  893e                  mov       [esi],edi
  00490508  8d3cc9                lea       edi,[ecx+ecx*8]
  0049050b  c1e704                shl       edi,4
  0049050e  81c700600000          add       edi,6000h
  00490514  66894612              mov       [esi+12h],ax
  00490518  897e5c                mov       [esi+5Ch],edi

LAB_0049051b:
  0049051b  66394612              cmp       [esi+12h],ax
  0049051f  0f8575060000          jne       near 00490B9Ah
  00490525  0fbf4610              movsx     eax,word [esi+10h]
  00490529  39442418              cmp       [esp+18h],eax
  0049052d  7d0c                  jge       short 0049053Bh
  0049052f  c6464300              mov       byte [esi+43h],0
  00490533  89565c                mov       [esi+5Ch],edx
  00490536  e95f060000            jmp       00490B9Ah

LAB_0049053b:
  0049053b  8a4643                mov       al,[esi+43h]
  0049053e  84c0                  test      al,al
  00490540  7405                  je        short 00490547h
  00490542  fec8                  dec       al
  00490544  884643                mov       [esi+43h],al

LAB_00490547:
  00490547  8b442414              mov       eax,[esp+14h]
  0049054b  8d9000010000          lea       edx,[eax+100h]
  00490551  81e2ff0f0000          and       edx,0FFFh
  00490557  81fa00020000          cmp       edx,200h
  0049055d  0f8e37060000          jle       near 00490B9Ah
  00490563  25ff0f0000            and       eax,0FFFh
  00490568  3d00080000            cmp       eax,800h
  0049056d  7d0d                  jge       short 0049057Ch
  0049056f  8d440928              lea       eax,[ecx+ecx+28h]
  00490573  66014632              add       [esi+32h],ax
  00490577  e91e060000            jmp       00490B9Ah

LAB_0049057c:
  0049057c  03c9                  add       ecx,ecx
  0049057e  b8d8ffffff            mov       eax,0FFFFFFD8h
  00490583  2bc1                  sub       eax,ecx
  00490585  66014632              add       [esi+32h],ax
  00490589  e90c060000            jmp       00490B9Ah
  0049058e  66837e1201            cmp       word [esi+12h],1
  00490593  0f859c000000          jne       near 00490635h
  00490599  8a4e43                mov       cl,[esi+43h]
  0049059c  84c9                  test      cl,cl
  0049059e  7405                  je        short 004905A5h
  004905a0  fec9                  dec       cl
  004905a2  884e43                mov       [esi+43h],cl

LAB_004905a5:
  004905a5  8b4c2410              mov       ecx,[esp+10h]
  004905a9  8d14c9                lea       edx,[ecx+ecx*8]
  004905ac  c1e204                shl       edx,4
  004905af  8d8a00500000          lea       ecx,[edx+5000h]
  004905b5  0fafc1                imul      eax,ecx
  004905b8  c1f80c                sar       eax,0Ch
  004905bb  8be8                  mov       ebp,eax
  004905bd  894e5c                mov       [esi+5Ch],ecx
  004905c0  0fbf4610              movsx     eax,word [esi+10h]
  004905c4  50                    push      eax
  004905c5  e852540b00            call      00545A1Ch
  004905ca  0fafc5                imul      eax,ebp
  004905cd  8b0f                  mov       ecx,[edi]
  004905cf  c1f80c                sar       eax,0Ch
  004905d2  03c8                  add       ecx,eax
  004905d4  890f                  mov       [edi],ecx
  004905d6  0fbf4e10              movsx     ecx,word [esi+10h]
  004905da  51                    push      ecx
  004905db  e836540b00            call      00545A16h
  004905e0  0fafc5                imul      eax,ebp
  004905e3  8b6e24                mov       ebp,[esi+24h]
  004905e6  c7465c00000000        mov       dword [esi+5Ch],0
  004905ed  c1f80c                sar       eax,0Ch
  004905f0  03e8                  add       ebp,eax
  004905f2  8b442414              mov       eax,[esp+14h]
  004905f6  896e24                mov       [esi+24h],ebp
  004905f9  8d9000010000          lea       edx,[eax+100h]
  004905ff  81e2ff0f0000          and       edx,0FFFh
  00490605  81fa00020000          cmp       edx,200h
  0049060b  7e28                  jle       short 00490635h
  0049060d  25ff0f0000            and       eax,0FFFh
  00490612  3d00080000            cmp       eax,800h
  00490617  7d0a                  jge       short 00490623h
  00490619  8b442410              mov       eax,[esp+10h]
  0049061d  8d440032              lea       eax,[eax+eax+32h]
  00490621  eb0e                  jmp       short 00490631h

LAB_00490623:
  00490623  8b4c2410              mov       ecx,[esp+10h]
  00490627  b8ceffffff            mov       eax,0FFFFFFCEh
  0049062c  8d1409                lea       edx,[ecx+ecx]
  0049062f  2bc2                  sub       eax,edx

LAB_00490631:
  00490631  66014632              add       [esi+32h],ax

LAB_00490635:
  00490635  66837e1200            cmp       word [esi+12h],0
  0049063a  0f855a050000          jne       near 00490B9Ah
  00490640  8b0e                  mov       ecx,[esi]
  00490642  b801000000            mov       eax,1
  00490647  81c900000001          or        ecx,1000000h
  0049064d  66894618              mov       [esi+18h],ax
  00490651  66c7461a0000          mov       word [esi+1Ah],0
  00490657  890e                  mov       [esi],ecx
  00490659  66894612              mov       [esi+12h],ax
  0049065d  e938050000            jmp       00490B9Ah
  00490662  668b4e12              mov       cx,[esi+12h]
  00490666  ba01000000            mov       edx,1
  0049066b  663bca                cmp       cx,dx
  0049066e  751c                  jne       short 0049068Ch
  00490670  0fbf4610              movsx     eax,word [esi+10h]
  00490674  39442418              cmp       [esp+18h],eax
  00490678  7d06                  jge       short 00490680h
  0049067a  c6464300              mov       byte [esi+43h],0
  0049067e  eb0c                  jmp       short 0049068Ch

LAB_00490680:
  00490680  8a4643                mov       al,[esi+43h]
  00490683  84c0                  test      al,al
  00490685  7405                  je        short 0049068Ch
  00490687  fec8                  dec       al
  00490689  884643                mov       [esi+43h],al

LAB_0049068c:
  0049068c  6685c9                test      cx,cx
  0049068f  0f8505050000          jne       near 00490B9Ah
  00490695  8b442410              mov       eax,[esp+10h]
  00490699  8b3e                  mov       edi,[esi]
  0049069b  66894e1a              mov       [esi+1Ah],cx
  0049069f  81cf00000001          or        edi,1000000h
  004906a5  8d0cc0                lea       ecx,[eax+eax*8]
  004906a8  66895618              mov       [esi+18h],dx
  004906ac  c1e104                shl       ecx,4
  004906af  893e                  mov       [esi],edi
  004906b1  66895612              mov       [esi+12h],dx
  004906b5  81c100a00000          add       ecx,0A000h

LAB_004906bb:
  004906bb  894e5c                mov       [esi+5Ch],ecx
  004906be  e9d7040000            jmp       00490B9Ah
  004906c3  33ff                  xor       edi,edi
  004906c5  66397e12              cmp       [esi+12h],di
  004906c9  7534                  jne       short 004906FFh
  004906cb  e8706a0b00            call      00547140h
  004906d0  99                    cdq
  004906d1  33c2                  xor       eax,edx
  004906d3  66816632ff0f          and       word [esi+32h],0FFFh
  004906d9  2bc2                  sub       eax,edx
  004906db  66c746120100          mov       word [esi+12h],1
  004906e1  25ff0f0000            and       eax,0FFFh
  004906e6  66897e18              mov       [esi+18h],di
  004906ea  33c2                  xor       eax,edx
  004906ec  66897e1a              mov       [esi+1Ah],di
  004906f0  2bc2                  sub       eax,edx
  004906f2  66894610              mov       [esi+10h],ax
  004906f6  8b06                  mov       eax,[esi]
  004906f8  0d00000001            or        eax,1000000h
  004906fd  8906                  mov       [esi],eax

LAB_004906ff:
  004906ff  66837e1201            cmp       word [esi+12h],1
  00490704  0f8590040000          jne       near 00490B9Ah
  0049070a  668b4e32              mov       cx,[esi+32h]
  0049070e  0fbf4610              movsx     eax,word [esi+10h]
  00490712  0fbfd1                movsx     edx,cx
  00490715  8bf8                  mov       edi,eax
  00490717  2bfa                  sub       edi,edx
  00490719  c1ff03                sar       edi,3
  0049071c  03cf                  add       ecx,edi
  0049071e  66894e32              mov       [esi+32h],cx
  00490722  0fbfc9                movsx     ecx,cx
  00490725  2bc1                  sub       eax,ecx
  00490727  83f814                cmp       eax,14h
  0049072a  0f8d6a040000          jge       near 00490B9Ah
  00490730  e80b6a0b00            call      00547140h
  00490735  99                    cdq
  00490736  b914000000            mov       ecx,14h
  0049073b  c6464207              mov       byte [esi+42h],7
  0049073f  f7f9                  idiv      ecx
  00490741  80c21e                add       dl,1Eh
  00490744  885643                mov       [esi+43h],dl
  00490747  e94e040000            jmp       00490B9Ah
  0049074c  8b4e20                mov       ecx,[esi+20h]
  0049074f  8b4320                mov       eax,[ebx+20h]
  00490752  8d9100400b00          lea       edx,[ecx+0B4000h]
  00490758  3bd0                  cmp       edx,eax
  0049075a  7d0f                  jge       short 0049076Bh
  0049075c  8b06                  mov       eax,[esi]
  0049075e  c6464300              mov       byte [esi+43h],0
  00490762  24fd                  and       al,0FDh
  00490764  8906                  mov       [esi],eax
  00490766  e92f040000            jmp       00490B9Ah

LAB_0049076b:
  0049076b  2bc1                  sub       eax,ecx
  0049076d  0500800200            add       eax,28000h
  00490772  c1f802                sar       eax,2
  00490775  03c1                  add       eax,ecx
  00490777  668b4e12              mov       cx,[esi+12h]
  0049077b  6683f901              cmp       cx,1
  0049077f  894620                mov       [esi+20h],eax
  00490782  7511                  jne       short 00490795h
  00490784  8b06                  mov       eax,[esi]
  00490786  a900000002            test      eax,2000000h
  0049078b  7408                  je        short 00490795h
  0049078d  24fd                  and       al,0FDh
  0049078f  c6464300              mov       byte [esi+43h],0
  00490793  8906                  mov       [esi],eax

LAB_00490795:
  00490795  6685c9                test      cx,cx
  00490798  0f85fc030000          jne       near 00490B9Ah
  0049079e  8b06                  mov       eax,[esi]
  004907a0  66c746120100          mov       word [esi+12h],1
  004907a6  25fffffffe            and       eax,0FEFFFFFFh
  004907ab  8906                  mov       [esi],eax
  004907ad  e9e8030000            jmp       00490B9Ah
  004907b2  8b7c2410              mov       edi,[esp+10h]
  004907b6  8b542414              mov       edx,[esp+14h]
  004907ba  bd01000000            mov       ebp,1
  004907bf  66396e12              cmp       [esi+12h],bp
  004907c3  756d                  jne       short 00490832h
  004907c5  8b4620                mov       eax,[esi+20h]
  004907c8  8b4e60                mov       ecx,[esi+60h]
  004907cb  0500300000            add       eax,3000h
  004907d0  894620                mov       [esi+20h],eax
  004907d3  2bc1                  sub       eax,ecx
  004907d5  c1f80c                sar       eax,0Ch
  004907d8  3dc8000000            cmp       eax,0C8h
  004907dd  7e1d                  jle       short 004907FCh
  004907df  33c0                  xor       eax,eax
  004907e1  66c746120200          mov       word [esi+12h],2
  004907e7  66894610              mov       [esi+10h],ax
  004907eb  6689461a              mov       [esi+1Ah],ax
  004907ef  8b06                  mov       eax,[esi]
  004907f1  66896e18              mov       [esi+18h],bp
  004907f5  0d02000001            or        eax,1000002h
  004907fa  8906                  mov       [esi],eax

LAB_004907fc:
  004907fc  8d8200010000          lea       eax,[edx+100h]
  00490802  25ff0f0000            and       eax,0FFFh
  00490807  3d00020000            cmp       eax,200h
  0049080c  7e24                  jle       short 00490832h
  0049080e  8bca                  mov       ecx,edx
  00490810  81e1ff0f0000          and       ecx,0FFFh
  00490816  81f900080000          cmp       ecx,800h
  0049081c  7d06                  jge       short 00490824h
  0049081e  8d443f28              lea       eax,[edi+edi+28h]
  00490822  eb0a                  jmp       short 0049082Eh

LAB_00490824:
  00490824  8d0c3f                lea       ecx,[edi+edi]
  00490827  b8d8ffffff            mov       eax,0FFFFFFD8h
  0049082c  2bc1                  sub       eax,ecx

LAB_0049082e:
  0049082e  66014632              add       [esi+32h],ax

LAB_00490832:
  00490832  66837e1202            cmp       word [esi+12h],2
  00490837  0f858f000000          jne       near 004908CCh
  0049083d  8b4e20                mov       ecx,[esi+20h]
  00490840  c7465c00900100        mov       dword [esi+5Ch],19000h
  00490847  8b4320                mov       eax,[ebx+20h]
  0049084a  8da900400b00          lea       ebp,[ecx+0B4000h]
  00490850  3be8                  cmp       ebp,eax
  00490852  7d16                  jge       short 0049086Ah
  00490854  8b06                  mov       eax,[esi]
  00490856  c6464300              mov       byte [esi+43h],0
  0049085a  24fd                  and       al,0FDh
  0049085c  c7465c00000000        mov       dword [esi+5Ch],0
  00490863  8906                  mov       [esi],eax
  00490865  e930030000            jmp       00490B9Ah

LAB_0049086a:
  0049086a  2bc1                  sub       eax,ecx
  0049086c  0500800200            add       eax,28000h
  00490871  c1f802                sar       eax,2
  00490874  03c1                  add       eax,ecx
  00490876  66ff4610              inc       word [esi+10h]
  0049087a  668b4e10              mov       cx,[esi+10h]
  0049087e  894620                mov       [esi+20h],eax
  00490881  8d8200010000          lea       eax,[edx+100h]
  00490887  25ff0f0000            and       eax,0FFFh
  0049088c  3d00020000            cmp       eax,200h
  00490891  7e22                  jle       short 004908B5h
  00490893  81e2ff0f0000          and       edx,0FFFh
  00490899  81fa00080000          cmp       edx,800h
  0049089f  7d06                  jge       short 004908A7h
  004908a1  8d443f28              lea       eax,[edi+edi+28h]
  004908a5  eb0a                  jmp       short 004908B1h

LAB_004908a7:
  004908a7  8d143f                lea       edx,[edi+edi]
  004908aa  b8d8ffffff            mov       eax,0FFFFFFD8h
  004908af  2bc2                  sub       eax,edx

LAB_004908b1:
  004908b1  66014632              add       [esi+32h],ax

LAB_004908b5:
  004908b5  6683f921              cmp       cx,21h
  004908b9  7c0c                  jl        short 004908C7h
  004908bb  8b06                  mov       eax,[esi]
  004908bd  66c746120300          mov       word [esi+12h],3
  004908c3  24fd                  and       al,0FDh
  004908c5  8906                  mov       [esi],eax

LAB_004908c7:
  004908c7  bd01000000            mov       ebp,1

LAB_004908cc:
  004908cc  668b4e12              mov       cx,[esi+12h]
  004908d0  6683f903              cmp       cx,3
  004908d4  751d                  jne       short 004908F3h
  004908d6  8b465c                mov       eax,[esi+5Ch]
  004908d9  8bd0                  mov       edx,eax
  004908db  c1fa03                sar       edx,3
  004908de  2bc2                  sub       eax,edx
  004908e0  89465c                mov       [esi+5Ch],eax
  004908e3  2500f0ffff            and       eax,0FFFFF000h
  004908e8  3d00a00000            cmp       eax,0A000h
  004908ed  7d04                  jge       short 004908F3h
  004908ef  c6464300              mov       byte [esi+43h],0

LAB_004908f3:
  004908f3  33c0                  xor       eax,eax
  004908f5  663bc8                cmp       cx,ax
  004908f8  0f859c020000          jne       near 00490B9Ah
  004908fe  6689461a              mov       [esi+1Ah],ax
  00490902  89465c                mov       [esi+5Ch],eax
  00490905  8b06                  mov       eax,[esi]
  00490907  66896e12              mov       [esi+12h],bp
  0049090b  0d00000001            or        eax,1000000h
  00490910  66896e18              mov       [esi+18h],bp
  00490914  8906                  mov       [esi],eax
  00490916  e97f020000            jmp       00490B9Ah
  0049091b  33d2                  xor       edx,edx
  0049091d  66395612              cmp       [esi+12h],dx
  00490921  7548                  jne       short 0049096Bh
  00490923  b901000000            mov       ecx,1
  00490928  66394e16              cmp       [esi+16h],cx
  0049092c  7408                  je        short 00490936h
  0049092e  885643                mov       [esi+43h],dl
  00490931  e964020000            jmp       00490B9Ah

LAB_00490936:
  00490936  8b442414              mov       eax,[esp+14h]
  0049093a  25ff0f0000            and       eax,0FFFh
  0049093f  663bc2                cmp       ax,dx
  00490942  66894612              mov       [esi+12h],ax
  00490946  7504                  jne       short 0049094Ch
  00490948  66894e12              mov       [esi+12h],cx

LAB_0049094c:
  0049094c  668b4612              mov       ax,[esi+12h]
  00490950  89565c                mov       [esi+5Ch],edx
  00490953  66f7d8                neg       ax
  00490956  66894612              mov       [esi+12h],ax
  0049095a  8b06                  mov       eax,[esi]
  0049095c  0d00000001            or        eax,1000000h
  00490961  66894e18              mov       [esi+18h],cx
  00490965  6689561a              mov       [esi+1Ah],dx
  00490969  8906                  mov       [esi],eax

LAB_0049096b:
  0049096b  668b4612              mov       ax,[esi+12h]
  0049096f  663bc2                cmp       ax,dx
  00490972  0f8447010000          je        near 00490ABFh
  00490978  6685c0                test      ax,ax
  0049097b  0f8d3e010000          jge       near 00490ABFh
  00490981  8b2f                  mov       ebp,[edi]
  00490983  0fbfc8                movsx     ecx,ax
  00490986  f7d9                  neg       ecx
  00490988  51                    push      ecx
  00490989  e88e500b00            call      00545A1Ch
  0049098e  8d0440                lea       eax,[eax+eax*2]
  00490991  8d1480                lea       edx,[eax+eax*4]
  00490994  8b431c                mov       eax,[ebx+1Ch]
  00490997  c1e202                shl       edx,2
  0049099a  2bd5                  sub       edx,ebp
  0049099c  03d0                  add       edx,eax
  0049099e  0fbf4612              movsx     eax,word [esi+12h]
  004909a2  c1fa03                sar       edx,3
  004909a5  f7d8                  neg       eax
  004909a7  03d5                  add       edx,ebp
  004909a9  8b6e24                mov       ebp,[esi+24h]
  004909ac  50                    push      eax
  004909ad  8917                  mov       [edi],edx
  004909af  e862500b00            call      00545A16h
  004909b4  8b5324                mov       edx,[ebx+24h]
  004909b7  8d0440                lea       eax,[eax+eax*2]
  004909ba  8d0c80                lea       ecx,[eax+eax*4]
  004909bd  8b4620                mov       eax,[esi+20h]
  004909c0  c1e102                shl       ecx,2
  004909c3  2bcd                  sub       ecx,ebp
  004909c5  03ca                  add       ecx,edx
  004909c7  c1f903                sar       ecx,3
  004909ca  03cd                  add       ecx,ebp
  004909cc  894e24                mov       [esi+24h],ecx
  004909cf  8b5320                mov       edx,[ebx+20h]
  004909d2  2bd0                  sub       edx,eax
  004909d4  81c200800200          add       edx,28000h
  004909da  c1fa03                sar       edx,3
  004909dd  03d0                  add       edx,eax
  004909df  0fbf4612              movsx     eax,word [esi+12h]
  004909e3  f7d8                  neg       eax
  004909e5  50                    push      eax
  004909e6  895620                mov       [esi+20h],edx
  004909e9  e82e500b00            call      00545A1Ch
  004909ee  8b2f                  mov       ebp,[edi]
  004909f0  8b531c                mov       edx,[ebx+1Ch]
  004909f3  8d0c80                lea       ecx,[eax+eax*4]
  004909f6  c1e104                shl       ecx,4
  004909f9  2bcd                  sub       ecx,ebp
  004909fb  03ca                  add       ecx,edx
  004909fd  f7c1f8ffffff          test      ecx,0FFFFFFF8h
  00490a03  7c20                  jl        short 00490A25h
  00490a05  0fbf5612              movsx     edx,word [esi+12h]
  00490a09  f7da                  neg       edx
  00490a0b  52                    push      edx
  00490a0c  e80b500b00            call      00545A1Ch
  00490a11  8b2f                  mov       ebp,[edi]
  00490a13  8b531c                mov       edx,[ebx+1Ch]
  00490a16  8d0480                lea       eax,[eax+eax*4]
  00490a19  c1e004                shl       eax,4
  00490a1c  2bc5                  sub       eax,ebp
  00490a1e  03c2                  add       eax,edx
  00490a20  c1f803                sar       eax,3
  00490a23  eb20                  jmp       short 00490A45h

LAB_00490a25:
  00490a25  0fbf4612              movsx     eax,word [esi+12h]
  00490a29  f7d8                  neg       eax
  00490a2b  50                    push      eax
  00490a2c  e8eb4f0b00            call      00545A1Ch
  00490a31  8b0f                  mov       ecx,[edi]
  00490a33  8b6b1c                mov       ebp,[ebx+1Ch]
  00490a36  8d0480                lea       eax,[eax+eax*4]
  00490a39  c1e004                shl       eax,4
  00490a3c  2bc1                  sub       eax,ecx
  00490a3e  03c5                  add       eax,ebp
  00490a40  c1f803                sar       eax,3
  00490a43  f7d8                  neg       eax

LAB_00490a45:
  00490a45  2500f0ffff            and       eax,0FFFFF000h
  00490a4a  3d00500000            cmp       eax,5000h
  00490a4f  7d1d                  jge       short 00490A6Eh
  00490a51  668b4e12              mov       cx,[esi+12h]
  00490a55  8b06                  mov       eax,[esi]
  00490a57  66f7d9                neg       cx
  00490a5a  0c02                  or        al,2
  00490a5c  66894e12              mov       [esi+12h],cx
  00490a60  8906                  mov       [esi],eax
  00490a62  66c746180100          mov       word [esi+18h],1
  00490a68  66c7461a0000          mov       word [esi+1Ah],0

LAB_00490a6e:
  00490a6e  8b442414              mov       eax,[esp+14h]
  00490a72  8d9000010000          lea       edx,[eax+100h]
  00490a78  81e2ff0f0000          and       edx,0FFFh
  00490a7e  81fa00020000          cmp       edx,200h
  00490a84  7e2b                  jle       short 00490AB1h
  00490a86  25ff0f0000            and       eax,0FFFh
  00490a8b  3d00080000            cmp       eax,800h
  00490a90  7d0d                  jge       short 00490A9Fh
  00490a92  8b442410              mov       eax,[esp+10h]
  00490a96  8d840080000000        lea       eax,[eax+eax+80h]
  00490a9d  eb0e                  jmp       short 00490AADh

LAB_00490a9f:
  00490a9f  8b4c2410              mov       ecx,[esp+10h]
  00490aa3  b880ffffff            mov       eax,0FFFFFF80h
  00490aa8  8d1409                lea       edx,[ecx+ecx]
  00490aab  2bc2                  sub       eax,edx

LAB_00490aad:
  00490aad  66014632              add       [esi+32h],ax

LAB_00490ab1:
  00490ab1  8a4643                mov       al,[esi+43h]
  00490ab4  fec8                  dec       al
  00490ab6  884643                mov       [esi+43h],al
  00490ab9  7504                  jne       short 00490ABFh
  00490abb  c6464300              mov       byte [esi+43h],0

LAB_00490abf:
  00490abf  668b4612              mov       ax,[esi+12h]
  00490ac3  6685c0                test      ax,ax
  00490ac6  0f84ce000000          je        near 00490B9Ah
  00490acc  0f8ec8000000          jle       near 00490B9Ah
  00490ad2  0fbfc0                movsx     eax,ax
  00490ad5  50                    push      eax
  00490ad6  e8414f0b00            call      00545A1Ch
  00490adb  8b531c                mov       edx,[ebx+1Ch]
  00490ade  8d0c80                lea       ecx,[eax+eax*4]
  00490ae1  c1e104                shl       ecx,4
  00490ae4  03ca                  add       ecx,edx
  00490ae6  0fbf5612              movsx     edx,word [esi+12h]
  00490aea  52                    push      edx
  00490aeb  890f                  mov       [edi],ecx
  00490aed  e8244f0b00            call      00545A16h
  00490af2  8b7b24                mov       edi,[ebx+24h]
  00490af5  8d0480                lea       eax,[eax+eax*4]
  00490af8  c1e004                shl       eax,4
  00490afb  03c7                  add       eax,edi
  00490afd  894624                mov       [esi+24h],eax
  00490b00  8b4b20                mov       ecx,[ebx+20h]
  00490b03  8a4643                mov       al,[esi+43h]
  00490b06  81c100800200          add       ecx,28000h
  00490b0c  fec8                  dec       al
  00490b0e  894e20                mov       [esi+20h],ecx
  00490b11  884643                mov       [esi+43h],al
  00490b14  7504                  jne       short 00490B1Ah
  00490b16  c6464300              mov       byte [esi+43h],0

LAB_00490b1a:
  00490b1a  8b442414              mov       eax,[esp+14h]
  00490b1e  8d9000010000          lea       edx,[eax+100h]
  00490b24  81e2ff0f0000          and       edx,0FFFh
  00490b2a  81fa00020000          cmp       edx,200h
  00490b30  7e2b                  jle       short 00490B5Dh
  00490b32  25ff0f0000            and       eax,0FFFh
  00490b37  3d00080000            cmp       eax,800h
  00490b3c  7d0d                  jge       short 00490B4Bh
  00490b3e  8b442410              mov       eax,[esp+10h]
  00490b42  8d840080000000        lea       eax,[eax+eax+80h]
  00490b49  eb0e                  jmp       short 00490B59h

LAB_00490b4b:
  00490b4b  8b4c2410              mov       ecx,[esp+10h]
  00490b4f  b880ffffff            mov       eax,0FFFFFF80h
  00490b54  8d1409                lea       edx,[ecx+ecx]
  00490b57  2bc2                  sub       eax,edx

LAB_00490b59:
  00490b59  66014632              add       [esi+32h],ax

LAB_00490b5d:
  00490b5d  f70300000040          test      dword [ebx],40000000h
  00490b63  7404                  je        short 00490B69h
  00490b65  c6464300              mov       byte [esi+43h],0

LAB_00490b69:
  00490b69  33c0                  xor       eax,eax
  00490b6b  8a4308                mov       al,[ebx+8]
  00490b6e  8d0cc0                lea       ecx,[eax+eax*8]
  00490b71  8d1448                lea       edx,[eax+ecx*2]
  00490b74  33c0                  xor       eax,eax
  00490b76  8a049588915e00        mov       al,[edx*4+5E9188h]
  00490b7d  c1e004                shl       eax,4
  00490b80  f680224a6000f0        test      byte [eax+604A22h],0F0h
  00490b87  7411                  je        short 00490B9Ah
  00490b89  33c0                  xor       eax,eax
  00490b8b  8a4643                mov       al,[esi+43h]
  00490b8e  83e805                sub       eax,5
  00490b91  85c0                  test      eax,eax
  00490b93  7f02                  jg        short 00490B97h
  00490b95  33c0                  xor       eax,eax

LAB_00490b97:
  00490b97  884643                mov       [esi+43h],al

LAB_00490b9a:
  00490b9a  8b0e                  mov       ecx,[esi]
  00490b9c  8a4642                mov       al,[esi+42h]
  00490b9f  f6c120                test      cl,20h
  00490ba2  745e                  je        short 00490C02h
  00490ba4  3c0c                  cmp       al,0Ch
  00490ba6  7445                  je        short 00490BEDh
  00490ba8  e893650b00            call      00547140h
  00490bad  99                    cdq
  00490bae  b932000000            mov       ecx,32h
  00490bb3  8b7e20                mov       edi,[esi+20h]
  00490bb6  f7f9                  idiv      ecx
  00490bb8  66035614              add       dx,[esi+14h]
  00490bbc  03d1                  add       edx,ecx
  00490bbe  81e2ff0f0000          and       edx,0FFFh
  00490bc4  66895614              mov       [esi+14h],dx
  00490bc8  0fbfd2                movsx     edx,dx
  00490bcb  52                    push      edx
  00490bcc  e84b4e0b00            call      00545A1Ch
  00490bd1  8b4e48                mov       ecx,[esi+48h]
  00490bd4  8d0440                lea       eax,[eax+eax*2]
  00490bd7  8d0480                lea       eax,[eax+eax*4]
  00490bda  8d1481                lea       edx,[ecx+eax*4]
  00490bdd  2bd7                  sub       edx,edi
  00490bdf  81c200c00800          add       edx,8C000h
  00490be5  c1fa03                sar       edx,3
  00490be8  03d7                  add       edx,edi
  00490bea  895620                mov       [esi+20h],edx

LAB_00490bed:
  00490bed  8b4364                mov       eax,[ebx+64h]
  00490bf0  8b4b20                mov       ecx,[ebx+20h]
  00490bf3  33c8                  xor       ecx,eax
  00490bf5  f7c100f0ffff          test      ecx,0FFFFF000h
  00490bfb  7557                  jne       short 00490C54h
  00490bfd  894648                mov       [esi+48h],eax
  00490c00  eb52                  jmp       short 00490C54h

LAB_00490c02:
  00490c02  3c0c                  cmp       al,0Ch
  00490c04  7404                  je        short 00490C0Ah
  00490c06  3c0a                  cmp       al,0Ah
  00490c08  7505                  jne       short 00490C0Fh

LAB_00490c0a:
  00490c0a  f6c102                test      cl,2
  00490c0d  7445                  je        short 00490C54h

LAB_00490c0f:
  00490c0f  e82c650b00            call      00547140h
  00490c14  99                    cdq
  00490c15  b932000000            mov       ecx,32h
  00490c1a  8b7e20                mov       edi,[esi+20h]
  00490c1d  f7f9                  idiv      ecx
  00490c1f  66035614              add       dx,[esi+14h]
  00490c23  03d1                  add       edx,ecx
  00490c25  81e2ff0f0000          and       edx,0FFFh
  00490c2b  66895614              mov       [esi+14h],dx
  00490c2f  0fbfd2                movsx     edx,dx
  00490c32  52                    push      edx
  00490c33  e8e44d0b00            call      00545A1Ch
  00490c38  8b4e60                mov       ecx,[esi+60h]
  00490c3b  8d0440                lea       eax,[eax+eax*2]
  00490c3e  8d0480                lea       eax,[eax+eax*4]
  00490c41  8d1481                lea       edx,[ecx+eax*4]
  00490c44  2bd7                  sub       edx,edi
  00490c46  81c200a00a00          add       edx,0AA000h
  00490c4c  c1fa03                sar       edx,3
  00490c4f  03d7                  add       edx,edi
  00490c51  895620                mov       [esi+20h],edx

LAB_00490c54:
  00490c54  0fbf4632              movsx     eax,word [esi+32h]
  00490c58  50                    push      eax
  00490c59  e8be4d0b00            call      00545A1Ch
  00490c5e  0fbf4e32              movsx     ecx,word [esi+32h]
  00490c62  51                    push      ecx
  00490c63  89442474              mov       [esp+74h],eax
  00490c67  e8aa4d0b00            call      00545A16h
  00490c6c  8b5650                mov       edx,[esi+50h]
  00490c6f  89442478              mov       [esp+78h],eax
  00490c73  8bc2                  mov       eax,edx
  00490c75  8b4c241c              mov       ecx,[esp+1Ch]
  00490c79  c1f803                sar       eax,3
  00490c7c  0faf4e5c              imul      ecx,[esi+5Ch]
  00490c80  8bfa                  mov       edi,edx
  00490c82  56                    push      esi
  00490c83  2bf8                  sub       edi,eax
  00490c85  c1fa08                sar       edx,8
  00490c88  2bfa                  sub       edi,edx
  00490c8a  8b5654                mov       edx,[esi+54h]
  00490c8d  8bc2                  mov       eax,edx
  00490c8f  8bea                  mov       ebp,edx
  00490c91  c1f803                sar       eax,3
  00490c94  2be8                  sub       ebp,eax
  00490c96  897e50                mov       [esi+50h],edi
  00490c99  c1fa08                sar       edx,8
  00490c9c  2bea                  sub       ebp,edx
  00490c9e  8b5658                mov       edx,[esi+58h]
  00490ca1  8bda                  mov       ebx,edx
  00490ca3  8bc2                  mov       eax,edx
  00490ca5  c1fb03                sar       ebx,3
  00490ca8  2bc3                  sub       eax,ebx
  00490caa  8b5e20                mov       ebx,[esi+20h]
  00490cad  c1fa08                sar       edx,8
  00490cb0  c1f90c                sar       ecx,0Ch
  00490cb3  2bc2                  sub       eax,edx
  00490cb5  8bd1                  mov       edx,ecx
  00490cb7  0faf542474            imul      edx,[esp+74h]
  00490cbc  0faf4c247c            imul      ecx,[esp+7Ch]
  00490cc1  c1fa0c                sar       edx,0Ch
  00490cc4  c1f90c                sar       ecx,0Ch
  00490cc7  894658                mov       [esi+58h],eax
  00490cca  03d7                  add       edx,edi
  00490ccc  8b7e1c                mov       edi,[esi+1Ch]
  00490ccf  03c8                  add       ecx,eax
  00490cd1  8b4624                mov       eax,[esi+24h]
  00490cd4  03fa                  add       edi,edx
  00490cd6  03dd                  add       ebx,ebp
  00490cd8  03c1                  add       eax,ecx
  00490cda  896e54                mov       [esi+54h],ebp
  00490cdd  897e1c                mov       [esi+1Ch],edi
  00490ce0  895e20                mov       [esi+20h],ebx
  00490ce3  894624                mov       [esi+24h],eax
  00490ce6  e8552c0a00            call      00533940h
  00490ceb  8bbe90000000          mov       edi,[esi+90h]
  00490cf1  8b06                  mov       eax,[esi]
  00490cf3  83c404                add       esp,4
  00490cf6  81e7fff8ffff          and       edi,0FFFFF8FFh
  00490cfc  a820                  test      al,20h
  00490cfe  89be90000000          mov       [esi+90h],edi
  00490d04  8bcf                  mov       ecx,edi
  00490d06  740c                  je        short 00490D14h
  00490d08  81e1ffdfffdf          and       ecx,0DFFFDFFFh
  00490d0e  898e90000000          mov       [esi+90h],ecx

LAB_00490d14:
  00490d14  8b4e60                mov       ecx,[esi+60h]
  00490d17  8b5620                mov       edx,[esi+20h]
  00490d1a  3bd1                  cmp       edx,ecx
  00490d1c  7f19                  jg        short 00490D37h
  00490d1e  8b9690000000          mov       edx,[esi+90h]
  00490d24  f6c680                test      dh,80h
  00490d27  750e                  jne       short 00490D37h
  00490d29  24fe                  and       al,0FEh
  00490d2b  33db                  xor       ebx,ebx
  00490d2d  894e20                mov       [esi+20h],ecx
  00490d30  8906                  mov       [esi],eax
  00490d32  895e54                mov       [esi+54h],ebx
  00490d35  eb02                  jmp       short 00490D39h

LAB_00490d37:
  00490d37  33db                  xor       ebx,ebx

LAB_00490d39:
  00490d39  f7869000000082a80020  test      dword [esi+90h],2000A882h
  00490d43  0f84c0000000          je        near 00490E09h
  00490d49  8a4e42                mov       cl,[esi+42h]
  00490d4c  b007                  mov       al,7
  00490d4e  3ac8                  cmp       cl,al
  00490d50  7420                  je        short 00490D72h
  00490d52  884642                mov       [esi+42h],al
  00490d55  e8e6630b00            call      00547140h
  00490d5a  99                    cdq
  00490d5b  b928000000            mov       ecx,28h
  00490d60  66895e12              mov       [esi+12h],bx
  00490d64  f7f9                  idiv      ecx
  00490d66  66c746105000          mov       word [esi+10h],50h
  00490d6c  80c214                add       dl,14h
  00490d6f  885643                mov       [esi+43h],dl

LAB_00490d72:
  00490d72  8b16                  mov       edx,[esi]
  00490d74  8b442464              mov       eax,[esp+64h]
  00490d78  8b4c2458              mov       ecx,[esp+58h]
  00490d7c  83e2fd                and       edx,0FFFFFFFDh
  00490d7f  894624                mov       [esi+24h],eax
  00490d82  8b442448              mov       eax,[esp+48h]
  00490d86  8916                  mov       [esi],edx
  00490d88  8b54245c              mov       edx,[esp+5Ch]
  00490d8c  25ffff0000            and       eax,0FFFFh
  00490d91  89561c                mov       [esi+1Ch],edx
  00490d94  8b542438              mov       edx,[esp+38h]
  00490d98  894e7c                mov       [esi+7Ch],ecx
  00490d9b  8b4c2454              mov       ecx,[esp+54h]
  00490d9f  898690000000          mov       [esi+90h],eax
  00490da5  8b44244c              mov       eax,[esp+4Ch]
  00490da9  899680000000          mov       [esi+80h],edx
  00490daf  8b542450              mov       edx,[esp+50h]
  00490db3  898684000000          mov       [esi+84h],eax
  00490db9  8b44243c              mov       eax,[esp+3Ch]
  00490dbd  898e88000000          mov       [esi+88h],ecx
  00490dc3  8b4c2440              mov       ecx,[esp+40h]
  00490dc7  6882a80020            push      2000A882h
  00490dcc  56                    push      esi
  00490dcd  89968c000000          mov       [esi+8Ch],edx
  00490dd3  894664                mov       [esi+64h],eax
  00490dd6  894e60                mov       [esi+60h],ecx
  00490dd9  e882500400            call      004D5E60h
  00490dde  83c408                add       esp,8
  00490de1  663bc3                cmp       ax,bx
  00490de4  7515                  jne       short 00490DFBh
  00490de6  e855630b00            call      00547140h
  00490deb  99                    cdq
  00490dec  33c2                  xor       eax,edx
  00490dee  2bc2                  sub       eax,edx
  00490df0  83e001                and       eax,1
  00490df3  33c2                  xor       eax,edx
  00490df5  2bc2                  sub       eax,edx
  00490df7  8d4400ff              lea       eax,[eax+eax-1]

LAB_00490dfb:
  00490dfb  8b542410              mov       edx,[esp+10h]
  00490dff  83c240                add       edx,40h
  00490e02  0fafd0                imul      edx,eax
  00490e05  66015632              add       [esi+32h],dx

LAB_00490e09:
  00490e09  6aff                  push      0FFFFFFFFh
  00490e0b  6aff                  push      0FFFFFFFFh
  00490e0d  6aff                  push      0FFFFFFFFh
  00490e0f  56                    push      esi
  00490e10  e8fb290300            call      004C3810h
  00490e15  8b44242c              mov       eax,[esp+2Ch]
  00490e19  83c410                add       esp,10h
  00490e1c  3bc3                  cmp       eax,ebx
  00490e1e  7517                  jne       short 00490E37h
  00490e20  668b842486000000      mov       ax,[esp+86h]
  00490e28  66894632              mov       [esi+32h],ax
  00490e2c  5f                    pop       edi
  00490e2d  5e                    pop       esi
  00490e2e  5d                    pop       ebp
  00490e2f  5b                    pop       ebx
  00490e30  81c488000000          add       esp,88h
  00490e36  c3                    ret

LAB_00490e37:
  00490e37  385e43                cmp       [esi+43h],bl
  00490e3a  0f8570030000          jne       near 004911B0h
  00490e40  8b466c                mov       eax,[esi+6Ch]
  00490e43  8b4e1c                mov       ecx,[esi+1Ch]
  00490e46  8b5624                mov       edx,[esi+24h]
  00490e49  8b781c                mov       edi,[eax+1Ch]
  00490e4c  8b6824                mov       ebp,[eax+24h]
  00490e4f  2bf9                  sub       edi,ecx
  00490e51  2bea                  sub       ebp,edx
  00490e53  c1ff0c                sar       edi,0Ch
  00490e56  c1fd0c                sar       ebp,0Ch
  00490e59  8bcf                  mov       ecx,edi
  00490e5b  8bd5                  mov       edx,ebp
  00490e5d  0fafcf                imul      ecx,edi
  00490e60  0fafd5                imul      edx,ebp
  00490e63  03ca                  add       ecx,edx
  00490e65  51                    push      ecx
  00490e66  e8a54b0b00            call      00545A10h
  00490e6b  55                    push      ebp
  00490e6c  57                    push      edi
  00490e6d  89442420              mov       [esp+20h],eax
  00490e71  e8ac4b0b00            call      00545A22h
  00490e76  668b4e32              mov       cx,[esi+32h]
  00490e7a  66895e12              mov       [esi+12h],bx
  00490e7e  2bc1                  sub       eax,ecx
  00490e80  25ff0f0000            and       eax,0FFFh
  00490e85  89442414              mov       [esp+14h],eax
  00490e89  8b442418              mov       eax,[esp+18h]
  00490e8d  3d96000000            cmp       eax,96h
  00490e92  0f8d38010000          jge       near 00490FD0h
  00490e98  e8a3620b00            call      00547140h
  00490e9d  99                    cdq
  00490e9e  b964000000            mov       ecx,64h
  00490ea3  f7f9                  idiv      ecx
  00490ea5  8bfa                  mov       edi,edx
  00490ea7  83ff32                cmp       edi,32h
  00490eaa  0f8dbb000000          jge       near 00490F6Bh
  00490eb0  8b542414              mov       edx,[esp+14h]
  00490eb4  8b6c2420              mov       ebp,[esp+20h]
  00490eb8  8d84aa00010000        lea       eax,[edx+ebp*4+100h]
  00490ebf  8d0ced00020000        lea       ecx,[ebp*8+200h]
  00490ec6  25ff0f0000            and       eax,0FFFh
  00490ecb  3bc1                  cmp       eax,ecx
  00490ecd  7f6b                  jg        short 00490F3Ah
  00490ecf  e86c620b00            call      00547140h
  00490ed4  99                    cdq
  00490ed5  8d4c2d01              lea       ecx,[ebp+ebp+1]
  00490ed9  f7f9                  idiv      ecx
  00490edb  8b442418              mov       eax,[esp+18h]
  00490edf  83c264                add       edx,64h
  00490ee2  3bc2                  cmp       eax,edx
  00490ee4  7d54                  jge       short 00490F3Ah
  00490ee6  83ff1e                cmp       edi,1Eh
  00490ee9  7d27                  jge       short 00490F12h
  00490eeb  8b16                  mov       edx,[esi]
  00490eed  c646420b              mov       byte [esi+42h],0Bh
  00490ef1  81e2fffffffe          and       edx,0FEFFFFFFh
  00490ef7  c6464301              mov       byte [esi+43h],1
  00490efb  83ca02                or        edx,2
  00490efe  895e5c                mov       [esi+5Ch],ebx
  00490f01  8916                  mov       [esi],edx
  00490f03  66c746180100          mov       word [esi+18h],1
  00490f09  66895e1a              mov       [esi+1Ah],bx
  00490f0d  e9c2000000            jmp       00490FD4h

LAB_00490f12:
  00490f12  66837e1601            cmp       word [esi+16h],1
  00490f17  7509                  jne       short 00490F22h
  00490f19  8a4e42                mov       cl,[esi+42h]
  00490f1c  b00d                  mov       al,0Dh
  00490f1e  3ac8                  cmp       cl,al
  00490f20  7528                  jne       short 00490F4Ah

LAB_00490f22:
  00490f22  885e42                mov       [esi+42h],bl
  00490f25  e816620b00            call      00547140h
  00490f2a  99                    cdq
  00490f2b  b928000000            mov       ecx,28h
  00490f30  f7f9                  idiv      ecx
  00490f32  885643                mov       [esi+43h],dl
  00490f35  e99a000000            jmp       00490FD4h

LAB_00490f3a:
  00490f3a  66837e1601            cmp       word [esi+16h],1
  00490f3f  7520                  jne       short 00490F61h
  00490f41  8a4e42                mov       cl,[esi+42h]
  00490f44  b00d                  mov       al,0Dh
  00490f46  3ac8                  cmp       cl,al
  00490f48  7417                  je        short 00490F61h

LAB_00490f4a:
  00490f4a  884642                mov       [esi+42h],al
  00490f4d  e8ee610b00            call      00547140h
  00490f52  99                    cdq
  00490f53  b928000000            mov       ecx,28h
  00490f58  f7f9                  idiv      ecx
  00490f5a  02d1                  add       dl,cl
  00490f5c  885643                mov       [esi+43h],dl
  00490f5f  eb73                  jmp       short 00490FD4h

LAB_00490f61:
  00490f61  c6464209              mov       byte [esi+42h],9
  00490f65  c6464301              mov       byte [esi+43h],1
  00490f69  eb69                  jmp       short 00490FD4h

LAB_00490f6b:
  00490f6b  83ff46                cmp       edi,46h
  00490f6e  7d3e                  jge       short 00490FAEh
  00490f70  c6464203              mov       byte [esi+42h],3
  00490f74  66895e12              mov       [esi+12h],bx
  00490f78  e8c3610b00            call      00547140h
  00490f7d  99                    cdq
  00490f7e  b90a000000            mov       ecx,0Ah
  00490f83  8b6c2420              mov       ebp,[esp+20h]
  00490f87  f7f9                  idiv      ecx
  00490f89  8bc5                  mov       eax,ebp
  00490f8b  d1f8                  sar       eax,1
  00490f8d  02d0                  add       dl,al
  00490f8f  80c214                add       dl,14h
  00490f92  885643                mov       [esi+43h],dl
  00490f95  e8a6610b00            call      00547140h
  00490f9a  99                    cdq
  00490f9b  33c2                  xor       eax,edx
  00490f9d  2bc2                  sub       eax,edx
  00490f9f  25ff0f0000            and       eax,0FFFh
  00490fa4  33c2                  xor       eax,edx
  00490fa6  2bc2                  sub       eax,edx
  00490fa8  66894610              mov       [esi+10h],ax
  00490fac  eb26                  jmp       short 00490FD4h

LAB_00490fae:
  00490fae  8b542410              mov       edx,[esp+10h]
  00490fb2  b964000000            mov       ecx,64h
  00490fb7  d1fa                  sar       edx,1
  00490fb9  2bcf                  sub       ecx,edi
  00490fbb  83c20a                add       edx,0Ah
  00490fbe  3bca                  cmp       ecx,edx
  00490fc0  7d06                  jge       short 00490FC8h
  00490fc2  c646420c              mov       byte [esi+42h],0Ch
  00490fc6  eb04                  jmp       short 00490FCCh

LAB_00490fc8:
  00490fc8  c6464209              mov       byte [esi+42h],9

LAB_00490fcc:
  00490fcc  c6464301              mov       byte [esi+43h],1

LAB_00490fd0:
  00490fd0  8b6c2420              mov       ebp,[esp+20h]

LAB_00490fd4:
  00490fd4  8b442418              mov       eax,[esp+18h]
  00490fd8  3d58020000            cmp       eax,258h
  00490fdd  0f8de9000000          jge       near 004910CCh
  00490fe3  3d96000000            cmp       eax,96h
  00490fe8  0f8cde000000          jl        near 004910CCh
  00490fee  e84d610b00            call      00547140h
  00490ff3  99                    cdq
  00490ff4  b964000000            mov       ecx,64h
  00490ff9  f7f9                  idiv      ecx
  00490ffb  83fa14                cmp       edx,14h
  00490ffe  7d32                  jge       short 00491032h
  00491000  8b542414              mov       edx,[esp+14h]
  00491004  81c200040000          add       edx,400h
  0049100a  81e2ff0f0000          and       edx,0FFFh
  00491010  81fa00080000          cmp       edx,800h
  00491016  7c0d                  jl        short 00491025h
  00491018  c6464201              mov       byte [esi+42h],1
  0049101c  c6464301              mov       byte [esi+43h],1
  00491020  e9a7000000            jmp       004910CCh

LAB_00491025:
  00491025  c646420c              mov       byte [esi+42h],0Ch
  00491029  c6464301              mov       byte [esi+43h],1
  0049102d  e99a000000            jmp       004910CCh

LAB_00491032:
  00491032  83fa28                cmp       edx,28h
  00491035  7d25                  jge       short 0049105Ch
  00491037  c6464202              mov       byte [esi+42h],2
  0049103b  e800610b00            call      00547140h
  00491040  99                    cdq
  00491041  b90a000000            mov       ecx,0Ah
  00491046  66c746105000          mov       word [esi+10h],50h
  0049104c  f7f9                  idiv      ecx
  0049104e  8bc5                  mov       eax,ebp
  00491050  d1f8                  sar       eax,1
  00491052  02d0                  add       dl,al
  00491054  80c214                add       dl,14h
  00491057  885643                mov       [esi+43h],dl
  0049105a  eb70                  jmp       short 004910CCh

LAB_0049105c:
  0049105c  83fa50                cmp       edx,50h
  0049105f  7d3a                  jge       short 0049109Bh
  00491061  c6464203              mov       byte [esi+42h],3
  00491065  66895e12              mov       [esi+12h],bx
  00491069  e8d2600b00            call      00547140h
  0049106e  99                    cdq
  0049106f  b90a000000            mov       ecx,0Ah
  00491074  f7f9                  idiv      ecx
  00491076  8bc5                  mov       eax,ebp
  00491078  d1f8                  sar       eax,1
  0049107a  02d0                  add       dl,al
  0049107c  80c214                add       dl,14h
  0049107f  885643                mov       [esi+43h],dl
  00491082  e8b9600b00            call      00547140h
  00491087  99                    cdq
  00491088  33c2                  xor       eax,edx
  0049108a  2bc2                  sub       eax,edx
  0049108c  25ff0f0000            and       eax,0FFFh
  00491091  33c2                  xor       eax,edx
  00491093  2bc2                  sub       eax,edx
  00491095  66894610              mov       [esi+10h],ax
  00491099  eb31                  jmp       short 004910CCh

LAB_0049109b:
  0049109b  b964000000            mov       ecx,64h
  004910a0  2bca                  sub       ecx,edx
  004910a2  8b542410              mov       edx,[esp+10h]
  004910a6  d1fa                  sar       edx,1
  004910a8  83c20a                add       edx,0Ah
  004910ab  3bca                  cmp       ecx,edx
  004910ad  7d0a                  jge       short 004910B9h
  004910af  c6464209              mov       byte [esi+42h],9
  004910b3  c6464301              mov       byte [esi+43h],1
  004910b7  eb13                  jmp       short 004910CCh

LAB_004910b9:
  004910b9  885e42                mov       [esi+42h],bl
  004910bc  e87f600b00            call      00547140h
  004910c1  99                    cdq
  004910c2  b928000000            mov       ecx,28h
  004910c7  f7f9                  idiv      ecx
  004910c9  885643                mov       [esi+43h],dl

LAB_004910cc:
  004910cc  817c241858020000      cmp       dword [esp+18h],258h
  004910d4  0f8cd6000000          jl        near 004911B0h
  004910da  e861600b00            call      00547140h
  004910df  99                    cdq
  004910e0  b964000000            mov       ecx,64h
  004910e5  f7f9                  idiv      ecx
  004910e7  83fa32                cmp       edx,32h
  004910ea  7d6f                  jge       short 0049115Bh
  004910ec  8b442410              mov       eax,[esp+10h]
  004910f0  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004910f4  3bd1                  cmp       edx,ecx
  004910f6  7d2e                  jge       short 00491126h
  004910f8  c6464202              mov       byte [esi+42h],2
  004910fc  e83f600b00            call      00547140h
  00491101  99                    cdq
  00491102  b90a000000            mov       ecx,0Ah
  00491107  66c746106400          mov       word [esi+10h],64h
  0049110d  f7f9                  idiv      ecx
  0049110f  8bc5                  mov       eax,ebp
  00491111  d1f8                  sar       eax,1
  00491113  02d0                  add       dl,al
  00491115  80c214                add       dl,14h
  00491118  885643                mov       [esi+43h],dl
  0049111b  5f                    pop       edi
  0049111c  5e                    pop       esi
  0049111d  5d                    pop       ebp
  0049111e  5b                    pop       ebx
  0049111f  81c488000000          add       esp,88h
  00491125  c3                    ret

LAB_00491126:
  00491126  885e42                mov       [esi+42h],bl
  00491129  e812600b00            call      00547140h
  0049112e  99                    cdq
  0049112f  b90a000000            mov       ecx,0Ah
  00491134  66895e12              mov       [esi+12h],bx
  00491138  f7f9                  idiv      ecx
  0049113a  8b4c2410              mov       ecx,[esp+10h]
  0049113e  8bc5                  mov       eax,ebp
  00491140  c1f802                sar       eax,2
  00491143  02d0                  add       dl,al
  00491145  885643                mov       [esi+43h],dl
  00491148  8d540950              lea       edx,[ecx+ecx+50h]
  0049114c  66895610              mov       [esi+10h],dx
  00491150  5f                    pop       edi
  00491151  5e                    pop       esi
  00491152  5d                    pop       ebp
  00491153  5b                    pop       ebx
  00491154  81c488000000          add       esp,88h
  0049115a  c3                    ret

LAB_0049115b:
  0049115b  83fa4b                cmp       edx,4Bh
  0049115e  7d43                  jge       short 004911A3h
  00491160  c6464203              mov       byte [esi+42h],3
  00491164  66895e12              mov       [esi+12h],bx
  00491168  e8d35f0b00            call      00547140h
  0049116d  99                    cdq
  0049116e  b90a000000            mov       ecx,0Ah
  00491173  f7f9                  idiv      ecx
  00491175  8bc5                  mov       eax,ebp
  00491177  d1f8                  sar       eax,1
  00491179  02d0                  add       dl,al
  0049117b  80c214                add       dl,14h
  0049117e  885643                mov       [esi+43h],dl
  00491181  e8ba5f0b00            call      00547140h
  00491186  99                    cdq
  00491187  33c2                  xor       eax,edx
  00491189  2bc2                  sub       eax,edx
  0049118b  25ff0f0000            and       eax,0FFFh
  00491190  33c2                  xor       eax,edx
  00491192  2bc2                  sub       eax,edx
  00491194  66894610              mov       [esi+10h],ax
  00491198  5f                    pop       edi
  00491199  5e                    pop       esi
  0049119a  5d                    pop       ebp
  0049119b  5b                    pop       ebx
  0049119c  81c488000000          add       esp,88h
  004911a2  c3                    ret

LAB_004911a3:
  004911a3  83fa3c                cmp       edx,3Ch
  004911a6  7d08                  jge       short 004911B0h
  004911a8  c6464201              mov       byte [esi+42h],1
  004911ac  c6464301              mov       byte [esi+43h],1

LAB_004911b0:
  004911b0  5f                    pop       edi
  004911b1  5e                    pop       esi
  004911b2  5d                    pop       ebp
  004911b3  5b                    pop       ebx
  004911b4  81c488000000          add       esp,88h
  004911ba  c3                    ret

LAB_004911bb:
  004911bb  66837e1a02            cmp       word [esi+1Ah],2
  004911c0  0f8542f2ffff          jne       near 00490408h
  004911c6  8d4c242c              lea       ecx,[esp+2Ch]
  004911ca  6a00                  push      0
  004911cc  51                    push      ecx
  004911cd  57                    push      edi
  004911ce  56                    push      esi
  004911cf  e85c8a0800            call      00519C30h
  004911d4  8be8                  mov       ebp,eax
  004911d6  a1b8e05d00            mov       eax,[5DE0B8h]
  004911db  83c410                add       esp,10h
  004911de  85c0                  test      eax,eax
  004911e0  0f841ef2ffff          je        near 00490404h
  004911e6  66837e1a02            cmp       word [esi+1Ah],2
  004911eb  0f8513f2ffff          jne       near 00490404h
  004911f1  8b5d00                mov       ebx,[ebp]
  004911f4  66c785960000000400    mov       word [ebp+96h],4
  004911fd  83cb40                or        ebx,40h
  00491200  55                    push      ebp
  00491201  895d00                mov       [ebp],ebx
  00491204  8b4c242c              mov       ecx,[esp+2Ch]
  00491208  56                    push      esi
  00491209  668b4130              mov       ax,[ecx+30h]
  0049120d  0fbfd8                movsx     ebx,ax
  00491210  66c1f802              sar       ax,2
  00491214  66894130              mov       [ecx+30h],ax
  00491218  e863890a00            call      00539B80h
  0049121d  8b542430              mov       edx,[esp+30h]
  00491221  83c408                add       esp,8
  00491224  83c51c                add       ebp,1Ch
  00491227  55                    push      ebp
  00491228  6800180000            push      1800h
  0049122d  6800030000            push      300h
  00491232  6a03                  push      3
  00491234  66895a30              mov       [edx+30h],bx
  00491238  e8e3c80a00            call      0053DB20h
  0049123d  8b44242c              mov       eax,[esp+2Ch]
  00491241  8b5c2434              mov       ebx,[esp+34h]
  00491245  83c410                add       esp,10h
  00491248  e9bbf1ffff            jmp       00490408h
  0049124d  90                    nop
  0049124e  8bff                  mov       edi,edi
  00491250  1d0449006a            sbb       eax,6A004904h
  00491255  0449                  add       al,49h
  00491257  00e5                  add       ch,ah
  00491259  0449                  add       al,49h
  0049125b  008e0549009a          add       [esi-65FFB6FBh],cl
  00491261  0b4900                or        ecx,[ecx]
  00491264  9a0b49009a0b49        call      490Bh:9A00490Bh
  0049126b  006206                add       [edx+6],ah
  0049126e  49                    dec       ecx
  0049126f  009a0b4900c3          add       [edx-3CFFB6F5h],bl
  00491275  06                    push      es
  00491276  49                    dec       ecx
  00491277  009a0b49004c          add       [edx+4C00490Bh],bl
  0049127d  07                    pop       es
  0049127e  49                    dec       ecx
  0049127f  00b20749001b          add       [edx+1B004907h],dh
  00491285  094900                or        [ecx],ecx
  00491288  90                    nop
  00491289  90                    nop
  0049128a  90                    nop
  0049128b  90                    nop
  0049128c  90                    nop
  0049128d  90                    nop
  0049128e  90                    nop
  0049128f  90                    nop

90 cible(s) de saut interne : 0x0049021e, 0x00490222, 0x00490224, 0x00490368, 0x0049038a, 0x0049038e, 0x004903ed, 0x00490404, 0x00490408, 0x0049043f, 0x00490452, 0x00490489, 0x004904ad, 0x004904ce, 0x0049051b, 0x0049053b, 0x00490547, 0x0049057c, 0x004905a5, 0x00490623, 0x00490631, 0x00490635, 0x00490680, 0x0049068c, 0x004906bb, 0x004906ff, 0x0049076b, 0x00490795, 0x004907fc, 0x00490824, 0x0049082e, 0x00490832, 0x0049086a, 0x004908a7, 0x004908b1, 0x004908b5, 0x004908c7, 0x004908cc, 0x004908f3, 0x00490936, 0x0049094c, 0x0049096b, 0x00490a25, 0x00490a45, 0x00490a6e, 0x00490a9f, 0x00490aad, 0x00490ab1, 0x00490abf, 0x00490b1a, 0x00490b4b, 0x00490b59, 0x00490b5d, 0x00490b69, 0x00490b97, 0x00490b9a, 0x00490bed, 0x00490c02, 0x00490c0a, 0x00490c0f, 0x00490c54, 0x00490d14, 0x00490d37, 0x00490d39, 0x00490d72, 0x00490dfb, 0x00490e09, 0x00490e37, 0x00490f12, 0x00490f22, 0x00490f3a, 0x00490f4a, 0x00490f61, 0x00490f6b, 0x00490fae, 0x00490fc8, 0x00490fcc, 0x00490fd0, 0x00490fd4, 0x00491025, 0x00491032, 0x0049105c, 0x0049109b, 0x004910b9, 0x004910cc, 0x00491126, 0x0049115b, 0x004911a3, 0x004911b0, 0x004911bb
