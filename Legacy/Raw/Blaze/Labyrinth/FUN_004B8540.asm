Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004b8540  section .text  offset fichier 0xb7940  3296 octets

  004b8540  81ec88000000          sub       esp,88h
  004b8546  53                    push      ebx
  004b8547  55                    push      ebp
  004b8548  56                    push      esi
  004b8549  8bb42498000000        mov       esi,[esp+98h]
  004b8550  33db                  xor       ebx,ebx
  004b8552  57                    push      edi
  004b8553  8a4641                mov       al,[esi+41h]
  004b8556  66895c2430            mov       [esp+30h],bx
  004b855b  3ac3                  cmp       al,bl
  004b855d  66c74424321e00        mov       word [esp+32h],1Eh
  004b8564  66c74424343200        mov       word [esp+34h],32h
  004b856b  66c74424366400        mov       word [esp+36h],64h
  004b8572  66895c2438            mov       [esp+38h],bx
  004b8577  66c744243a5000        mov       word [esp+3Ah],50h
  004b857e  7519                  jne       short 004B8599h
  004b8580  66395e16              cmp       [esi+16h],bx
  004b8584  c6464101              mov       byte [esi+41h],1
  004b8588  53                    push      ebx
  004b8589  7503                  jne       short 004B858Eh
  004b858b  53                    push      ebx
  004b858c  eb02                  jmp       short 004B8590h

LAB_004b858e:
  004b858e  6a02                  push      2

LAB_004b8590:
  004b8590  56                    push      esi
  004b8591  e88a0c0000            call      004B9220h
  004b8596  83c40c                add       esp,0Ch

LAB_004b8599:
  004b8599  8b461c                mov       eax,[esi+1Ch]
  004b859c  8b4e20                mov       ecx,[esi+20h]
  004b859f  8b5624                mov       edx,[esi+24h]
  004b85a2  8d6e1c                lea       ebp,[esi+1Ch]
  004b85a5  89442424              mov       [esp+24h],eax
  004b85a9  8b4664                mov       eax,[esi+64h]
  004b85ac  894c2428              mov       [esp+28h],ecx
  004b85b0  8b4e60                mov       ecx,[esi+60h]
  004b85b3  8954242c              mov       [esp+2Ch],edx
  004b85b7  668b5632              mov       dx,[esi+32h]
  004b85bb  89442454              mov       [esp+54h],eax
  004b85bf  8b467c                mov       eax,[esi+7Ch]
  004b85c2  894c2448              mov       [esp+48h],ecx
  004b85c6  668b8e90000000        mov       cx,[esi+90h]
  004b85cd  6689942486000000      mov       [esp+86h],dx
  004b85d5  8b9680000000          mov       edx,[esi+80h]
  004b85db  8b7e6c                mov       edi,[esi+6Ch]
  004b85de  89442460              mov       [esp+60h],eax
  004b85e2  8b8684000000          mov       eax,[esi+84h]
  004b85e8  66894c244c            mov       [esp+4Ch],cx
  004b85ed  8b8e88000000          mov       ecx,[esi+88h]
  004b85f3  89542444              mov       [esp+44h],edx
  004b85f7  8b968c000000          mov       edx,[esi+8Ch]
  004b85fd  89442450              mov       [esp+50h],eax
  004b8601  894c243c              mov       [esp+3Ch],ecx
  004b8605  8d442464              lea       eax,[esp+64h]
  004b8609  8954245c              mov       [esp+5Ch],edx
  004b860d  8d4c246c              lea       ecx,[esp+6Ch]
  004b8611  50                    push      eax
  004b8612  8d54246c              lea       edx,[esp+6Ch]
  004b8616  51                    push      ecx
  004b8617  52                    push      edx
  004b8618  56                    push      esi
  004b8619  897c2450              mov       [esp+50h],edi
  004b861d  e85e030800            call      00538980h
  004b8622  83c410                add       esp,10h
  004b8625  8d442458              lea       eax,[esp+58h]
  004b8629  8d4c2420              lea       ecx,[esp+20h]
  004b862d  8d542418              lea       edx,[esp+18h]
  004b8631  50                    push      eax
  004b8632  51                    push      ecx
  004b8633  8d442424              lea       eax,[esp+24h]
  004b8637  52                    push      edx
  004b8638  8d4c2420              lea       ecx,[esp+20h]
  004b863c  50                    push      eax
  004b863d  51                    push      ecx
  004b863e  56                    push      esi
  004b863f  e8dcd80700            call      00535F20h
  004b8644  8b4c242c              mov       ecx,[esp+2Ch]
  004b8648  8b542470              mov       edx,[esp+70h]
  004b864c  83c418                add       esp,18h
  004b864f  b840000000            mov       eax,40h
  004b8654  2bc1                  sub       eax,ecx
  004b8656  52                    push      edx
  004b8657  56                    push      esi
  004b8658  89442418              mov       [esp+18h],eax
  004b865c  e87fd70100            call      004D5DE0h
  004b8661  8b4500                mov       eax,[ebp]
  004b8664  8b571c                mov       edx,[edi+1Ch]
  004b8667  8b4e20                mov       ecx,[esi+20h]
  004b866a  2bc2                  sub       eax,edx
  004b866c  8b5720                mov       edx,[edi+20h]
  004b866f  83c408                add       esp,8
  004b8672  2bca                  sub       ecx,edx
  004b8674  8b5624                mov       edx,[esi+24h]
  004b8677  2b5724                sub       edx,[edi+24h]
  004b867a  c1f80c                sar       eax,0Ch
  004b867d  8bf8                  mov       edi,eax
  004b867f  c1f90c                sar       ecx,0Ch
  004b8682  0faff8                imul      edi,eax
  004b8685  8bc1                  mov       eax,ecx
  004b8687  c1fa0c                sar       edx,0Ch
  004b868a  0fafc1                imul      eax,ecx
  004b868d  8bca                  mov       ecx,edx
  004b868f  03f8                  add       edi,eax
  004b8691  0fafca                imul      ecx,edx
  004b8694  03f9                  add       edi,ecx
  004b8696  57                    push      edi
  004b8697  e874d30800            call      00545A10h
  004b869c  8944241c              mov       [esp+1Ch],eax
  004b86a0  8b442420              mov       eax,[esp+20h]
  004b86a4  3bc3                  cmp       eax,ebx
  004b86a6  0f8400050000          je        near 004B8BACh
  004b86ac  8b0e                  mov       ecx,[esi]
  004b86ae  33d2                  xor       edx,edx
  004b86b0  8a5604                mov       dl,[esi+4]
  004b86b3  81e1fffffffd          and       ecx,0FDFFFFFFh
  004b86b9  0fbf7e18              movsx     edi,word [esi+18h]
  004b86bd  890e                  mov       [esi],ecx
  004b86bf  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004b86c6  f7c100000001          test      ecx,1000000h
  004b86cc  8b14ba                mov       edx,[edx+edi*4]
  004b86cf  741b                  je        short 004B86ECh
  004b86d1  66ff461a              inc       word [esi+1Ah]
  004b86d5  668b4e1a              mov       cx,[esi+1Ah]
  004b86d9  33ff                  xor       edi,edi
  004b86db  668b7a16              mov       di,[edx+16h]
  004b86df  0fbfc9                movsx     ecx,cx
  004b86e2  3bcf                  cmp       ecx,edi
  004b86e4  7c2a                  jl        short 004B8710h
  004b86e6  66895e1a              mov       [esi+1Ah],bx
  004b86ea  eb24                  jmp       short 004B8710h

LAB_004b86ec:
  004b86ec  66ff461a              inc       word [esi+1Ah]
  004b86f0  668b7e1a              mov       di,[esi+1Ah]
  004b86f4  33db                  xor       ebx,ebx
  004b86f6  668b5a16              mov       bx,[edx+16h]
  004b86fa  0fbfd7                movsx     edx,di
  004b86fd  3bd3                  cmp       edx,ebx
  004b86ff  7c0d                  jl        short 004B870Eh
  004b8701  4f                    dec       edi
  004b8702  81c900000002          or        ecx,2000000h
  004b8708  66897e1a              mov       [esi+1Ah],di
  004b870c  890e                  mov       [esi],ecx

LAB_004b870e:
  004b870e  33db                  xor       ebx,ebx

LAB_004b8710:
  004b8710  8a4e42                mov       cl,[esi+42h]
  004b8713  80f905                cmp       cl,5
  004b8716  8b0e                  mov       ecx,[esi]
  004b8718  7505                  jne       short 004B871Fh
  004b871a  83c910                or        ecx,10h
  004b871d  eb03                  jmp       short 004B8722h

LAB_004b871f:
  004b871f  83e1ef                and       ecx,0FFFFFFEFh

LAB_004b8722:
  004b8722  f6c102                test      cl,2
  004b8725  890e                  mov       [esi],ecx
  004b8727  0f846b0a0000          je        near 004B9198h
  004b872d  66837e1a08            cmp       word [esi+1Ah],8
  004b8732  7558                  jne       short 004B878Ch
  004b8734  8d442430              lea       eax,[esp+30h]
  004b8738  53                    push      ebx
  004b8739  50                    push      eax
  004b873a  55                    push      ebp
  004b873b  56                    push      esi
  004b873c  e8ef140600            call      00519C30h
  004b8741  8bf8                  mov       edi,eax
  004b8743  a1b8e05d00            mov       eax,[5DE0B8h]
  004b8748  83c410                add       esp,10h
  004b874b  3bc3                  cmp       eax,ebx
  004b874d  7439                  je        short 004B8788h
  004b874f  66837e1a08            cmp       word [esi+1Ah],8
  004b8754  7532                  jne       short 004B8788h
  004b8756  8b0f                  mov       ecx,[edi]
  004b8758  57                    push      edi
  004b8759  83c940                or        ecx,40h
  004b875c  56                    push      esi
  004b875d  66c787960000001000    mov       word [edi+96h],10h
  004b8766  890f                  mov       [edi],ecx
  004b8768  e813140800            call      00539B80h
  004b876d  83c408                add       esp,8
  004b8770  83c71c                add       edi,1Ch
  004b8773  57                    push      edi
  004b8774  6800180000            push      1800h
  004b8779  6800030000            push      300h
  004b877e  6a03                  push      3
  004b8780  e89b530800            call      0053DB20h
  004b8785  83c410                add       esp,10h

LAB_004b8788:
  004b8788  8b442420              mov       eax,[esp+20h]

LAB_004b878c:
  004b878c  8b0e                  mov       ecx,[esi]
  004b878e  f7c100000002          test      ecx,2000000h
  004b8794  7411                  je        short 004B87A7h
  004b8796  f6c101                test      cl,1
  004b8799  0f85ef090000          jne       near 004B918Eh
  004b879f  83e1fd                and       ecx,0FFFFFFFDh
  004b87a2  885e43                mov       [esi+43h],bl
  004b87a5  890e                  mov       [esi],ecx

LAB_004b87a7:
  004b87a7  8b7c2414              mov       edi,[esp+14h]

LAB_004b87ab:
  004b87ab  33c9                  xor       ecx,ecx
  004b87ad  8a4e42                mov       cl,[esi+42h]
  004b87b0  83f90b                cmp       ecx,0Bh
  004b87b3  0f87f3030000          ja        near 004B8BACh
  004b87b9  ff248dec914b00        jmp       dword [ecx*4+4B91ECh]
  004b87c0  66395e12              cmp       [esi+12h],bx
  004b87c4  751a                  jne       short 004B87E0h
  004b87c6  8b06                  mov       eax,[esi]
  004b87c8  66895e18              mov       [esi+18h],bx
  004b87cc  0d00000001            or        eax,1000000h
  004b87d1  66895e1a              mov       [esi+1Ah],bx
  004b87d5  8906                  mov       [esi],eax
  004b87d7  66c746120100          mov       word [esi+12h],1
  004b87dd  895e5c                mov       [esi+5Ch],ebx

LAB_004b87e0:
  004b87e0  0fbf4610              movsx     eax,word [esi+10h]
  004b87e4  3944241c              cmp       [esp+1Ch],eax
  004b87e8  7d08                  jge       short 004B87F2h
  004b87ea  885e43                mov       [esi+43h],bl
  004b87ed  e9ba030000            jmp       004B8BACh

LAB_004b87f2:
  004b87f2  8a4643                mov       al,[esi+43h]
  004b87f5  3ac3                  cmp       al,bl
  004b87f7  0f847f030000          je        near 004B8B7Ch
  004b87fd  fec8                  dec       al
  004b87ff  895e5c                mov       [esi+5Ch],ebx
  004b8802  884643                mov       [esi+43h],al
  004b8805  e9a2030000            jmp       004B8BACh
  004b880a  66395e12              cmp       [esi+12h],bx
  004b880e  751b                  jne       short 004B882Bh
  004b8810  8b0e                  mov       ecx,[esi]
  004b8812  b801000000            mov       eax,1
  004b8817  81c900000001          or        ecx,1000000h
  004b881d  66894618              mov       [esi+18h],ax
  004b8821  66895e1a              mov       [esi+1Ah],bx
  004b8825  890e                  mov       [esi],ecx
  004b8827  66894612              mov       [esi+12h],ax

LAB_004b882b:
  004b882b  8b442418              mov       eax,[esp+18h]
  004b882f  8d88c8000000          lea       ecx,[eax+0C8h]
  004b8835  81e1ff0f0000          and       ecx,0FFFh
  004b883b  81f990010000          cmp       ecx,190h
  004b8841  7f0b                  jg        short 004B884Eh
  004b8843  885e43                mov       [esi+43h],bl
  004b8846  895e5c                mov       [esi+5Ch],ebx
  004b8849  e95e030000            jmp       004B8BACh

LAB_004b884e:
  004b884e  25ff0f0000            and       eax,0FFFh
  004b8853  3d00080000            cmp       eax,800h
  004b8858  7d0f                  jge       short 004B8869h
  004b885a  8d473c                lea       eax,[edi+3Ch]
  004b885d  895e5c                mov       [esi+5Ch],ebx
  004b8860  66014632              add       [esi+32h],ax
  004b8864  e943030000            jmp       004B8BACh

LAB_004b8869:
  004b8869  b8c4ffffff            mov       eax,0FFFFFFC4h
  004b886e  895e5c                mov       [esi+5Ch],ebx
  004b8871  2bc7                  sub       eax,edi
  004b8873  66014632              add       [esi+32h],ax
  004b8877  e930030000            jmp       004B8BACh
  004b887c  66395e12              cmp       [esi+12h],bx
  004b8880  b801000000            mov       eax,1
  004b8885  7525                  jne       short 004B88ACh
  004b8887  8b0e                  mov       ecx,[esi]
  004b8889  8d14ff                lea       edx,[edi+edi*8]
  004b888c  c1e204                shl       edx,4
  004b888f  81c900000001          or        ecx,1000000h
  004b8895  81c200600000          add       edx,6000h
  004b889b  66894618              mov       [esi+18h],ax
  004b889f  66895e1a              mov       [esi+1Ah],bx
  004b88a3  890e                  mov       [esi],ecx
  004b88a5  66894612              mov       [esi+12h],ax
  004b88a9  89565c                mov       [esi+5Ch],edx

LAB_004b88ac:
  004b88ac  66394612              cmp       [esi+12h],ax
  004b88b0  0f85f6020000          jne       near 004B8BACh
  004b88b6  0fbf4610              movsx     eax,word [esi+10h]
  004b88ba  3944241c              cmp       [esp+1Ch],eax
  004b88be  7d0b                  jge       short 004B88CBh
  004b88c0  885e43                mov       [esi+43h],bl
  004b88c3  895e5c                mov       [esi+5Ch],ebx
  004b88c6  e9e1020000            jmp       004B8BACh

LAB_004b88cb:
  004b88cb  8a4643                mov       al,[esi+43h]
  004b88ce  3ac3                  cmp       al,bl
  004b88d0  7405                  je        short 004B88D7h
  004b88d2  fec8                  dec       al
  004b88d4  884643                mov       [esi+43h],al

LAB_004b88d7:
  004b88d7  8b442418              mov       eax,[esp+18h]
  004b88db  8d8800010000          lea       ecx,[eax+100h]
  004b88e1  81e1ff0f0000          and       ecx,0FFFh
  004b88e7  81f900020000          cmp       ecx,200h
  004b88ed  0f8eb9020000          jle       near 004B8BACh
  004b88f3  25ff0f0000            and       eax,0FFFh
  004b88f8  3d00080000            cmp       eax,800h
  004b88fd  7d0c                  jge       short 004B890Bh
  004b88ff  8d473c                lea       eax,[edi+3Ch]
  004b8902  66014632              add       [esi+32h],ax
  004b8906  e9a1020000            jmp       004B8BACh

LAB_004b890b:
  004b890b  b8c4ffffff            mov       eax,0FFFFFFC4h
  004b8910  2bc7                  sub       eax,edi
  004b8912  66014632              add       [esi+32h],ax
  004b8916  e991020000            jmp       004B8BACh
  004b891b  66837e1201            cmp       word [esi+12h],1
  004b8920  0f8592000000          jne       near 004B89B8h
  004b8926  8a4e43                mov       cl,[esi+43h]
  004b8929  3acb                  cmp       cl,bl
  004b892b  7405                  je        short 004B8932h
  004b892d  fec9                  dec       cl
  004b892f  884e43                mov       [esi+43h],cl

LAB_004b8932:
  004b8932  8d14ff                lea       edx,[edi+edi*8]
  004b8935  c1e204                shl       edx,4
  004b8938  8d8a00600000          lea       ecx,[edx+6000h]
  004b893e  0fafc1                imul      eax,ecx
  004b8941  c1f80c                sar       eax,0Ch
  004b8944  8bf8                  mov       edi,eax
  004b8946  894e5c                mov       [esi+5Ch],ecx
  004b8949  0fbf4610              movsx     eax,word [esi+10h]
  004b894d  50                    push      eax
  004b894e  e8c9d00800            call      00545A1Ch
  004b8953  0fafc7                imul      eax,edi
  004b8956  8b4d00                mov       ecx,[ebp]
  004b8959  c1f80c                sar       eax,0Ch
  004b895c  03c8                  add       ecx,eax
  004b895e  894d00                mov       [ebp],ecx
  004b8961  0fbf4e10              movsx     ecx,word [esi+10h]
  004b8965  51                    push      ecx
  004b8966  e8abd00800            call      00545A16h
  004b896b  0fafc7                imul      eax,edi
  004b896e  8b6e24                mov       ebp,[esi+24h]
  004b8971  895e5c                mov       [esi+5Ch],ebx
  004b8974  c1f80c                sar       eax,0Ch
  004b8977  03e8                  add       ebp,eax
  004b8979  8b442418              mov       eax,[esp+18h]
  004b897d  896e24                mov       [esi+24h],ebp
  004b8980  8d9000010000          lea       edx,[eax+100h]
  004b8986  81e2ff0f0000          and       edx,0FFFh
  004b898c  81fa00020000          cmp       edx,200h
  004b8992  7e24                  jle       short 004B89B8h
  004b8994  25ff0f0000            and       eax,0FFFh
  004b8999  3d00080000            cmp       eax,800h
  004b899e  7d09                  jge       short 004B89A9h
  004b89a0  8b442414              mov       eax,[esp+14h]
  004b89a4  83c032                add       eax,32h
  004b89a7  eb0b                  jmp       short 004B89B4h

LAB_004b89a9:
  004b89a9  8b4c2414              mov       ecx,[esp+14h]
  004b89ad  b8ceffffff            mov       eax,0FFFFFFCEh
  004b89b2  2bc1                  sub       eax,ecx

LAB_004b89b4:
  004b89b4  66014632              add       [esi+32h],ax

LAB_004b89b8:
  004b89b8  66395e12              cmp       [esi+12h],bx
  004b89bc  0f85ea010000          jne       near 004B8BACh
  004b89c2  8b06                  mov       eax,[esi]
  004b89c4  66c746180200          mov       word [esi+18h],2
  004b89ca  0d00000001            or        eax,1000000h
  004b89cf  66895e1a              mov       [esi+1Ah],bx
  004b89d3  8906                  mov       [esi],eax
  004b89d5  66c746120100          mov       word [esi+12h],1
  004b89db  e9cc010000            jmp       004B8BACh
  004b89e0  668b4e12              mov       cx,[esi+12h]
  004b89e4  6683f901              cmp       cx,1
  004b89e8  7514                  jne       short 004B89FEh
  004b89ea  8a4643                mov       al,[esi+43h]
  004b89ed  3ac3                  cmp       al,bl
  004b89ef  740d                  je        short 004B89FEh
  004b89f1  f70600000002          test      dword [esi],2000000h
  004b89f7  7405                  je        short 004B89FEh
  004b89f9  fec8                  dec       al
  004b89fb  884643                mov       [esi+43h],al

LAB_004b89fe:
  004b89fe  663bcb                cmp       cx,bx
  004b8a01  0f85a5010000          jne       near 004B8BACh
  004b8a07  66c746120100          mov       word [esi+12h],1
  004b8a0d  66c746180500          mov       word [esi+18h],5
  004b8a13  66895e1a              mov       [esi+1Ah],bx
  004b8a17  895e5c                mov       [esi+5Ch],ebx
  004b8a1a  e987010000            jmp       004B8BA6h
  004b8a1f  66395e12              cmp       [esi+12h],bx
  004b8a23  757a                  jne       short 004B8A9Fh
  004b8a25  8b0e                  mov       ecx,[esi]
  004b8a27  f6c101                test      cl,1
  004b8a2a  7405                  je        short 004B8A31h
  004b8a2c  885e43                mov       [esi+43h],bl
  004b8a2f  eb6e                  jmp       short 004B8A9Fh

LAB_004b8a31:
  004b8a31  81e1fffffffe          and       ecx,0FEFFFFFFh
  004b8a37  66c746180400          mov       word [esi+18h],4
  004b8a3d  890e                  mov       [esi],ecx
  004b8a3f  8d8f00010000          lea       ecx,[edi+100h]
  004b8a45  c1e106                shl       ecx,6
  004b8a48  0fafc1                imul      eax,ecx
  004b8a4b  894e5c                mov       [esi+5Ch],ecx
  004b8a4e  66895e1a              mov       [esi+1Ah],bx
  004b8a52  0fbf4e10              movsx     ecx,word [esi+10h]
  004b8a56  c1f80c                sar       eax,0Ch
  004b8a59  51                    push      ecx
  004b8a5a  8bf8                  mov       edi,eax
  004b8a5c  e8bbcf0800            call      00545A1Ch
  004b8a61  0fafc7                imul      eax,edi
  004b8a64  0fbf5610              movsx     edx,word [esi+10h]
  004b8a68  8b4e50                mov       ecx,[esi+50h]
  004b8a6b  52                    push      edx
  004b8a6c  c1f80c                sar       eax,0Ch
  004b8a6f  03c8                  add       ecx,eax
  004b8a71  894e50                mov       [esi+50h],ecx
  004b8a74  e89dcf0800            call      00545A16h
  004b8a79  0fafc7                imul      eax,edi
  004b8a7c  8b5658                mov       edx,[esi+58h]
  004b8a7f  8b4e54                mov       ecx,[esi+54h]
  004b8a82  c1f80c                sar       eax,0Ch
  004b8a85  03d0                  add       edx,eax
  004b8a87  8b06                  mov       eax,[esi]
  004b8a89  81c100e00100          add       ecx,1E000h
  004b8a8f  0c01                  or        al,1
  004b8a91  895658                mov       [esi+58h],edx
  004b8a94  894e54                mov       [esi+54h],ecx
  004b8a97  8906                  mov       [esi],eax
  004b8a99  66c746120100          mov       word [esi+12h],1

LAB_004b8a9f:
  004b8a9f  f60601                test      byte [esi],1
  004b8aa2  0f8504010000          jne       near 004B8BACh
  004b8aa8  885e43                mov       [esi+43h],bl
  004b8aab  e9fc000000            jmp       004B8BACh
  004b8ab0  668b4e12              mov       cx,[esi+12h]
  004b8ab4  ba01000000            mov       edx,1
  004b8ab9  663bca                cmp       cx,dx
  004b8abc  751b                  jne       short 004B8AD9h
  004b8abe  0fbf4610              movsx     eax,word [esi+10h]
  004b8ac2  3944241c              cmp       [esp+1Ch],eax
  004b8ac6  7d05                  jge       short 004B8ACDh
  004b8ac8  885e43                mov       [esi+43h],bl
  004b8acb  eb0c                  jmp       short 004B8AD9h

LAB_004b8acd:
  004b8acd  8a4643                mov       al,[esi+43h]
  004b8ad0  3ac3                  cmp       al,bl
  004b8ad2  7405                  je        short 004B8AD9h
  004b8ad4  fec8                  dec       al
  004b8ad6  884643                mov       [esi+43h],al

LAB_004b8ad9:
  004b8ad9  663bcb                cmp       cx,bx
  004b8adc  0f85ca000000          jne       near 004B8BACh
  004b8ae2  66895e1a              mov       [esi+1Ah],bx
  004b8ae6  8b1e                  mov       ebx,[esi]
  004b8ae8  8d0cff                lea       ecx,[edi+edi*8]
  004b8aeb  81cb00000001          or        ebx,1000000h
  004b8af1  c1e104                shl       ecx,4
  004b8af4  81c100600000          add       ecx,6000h
  004b8afa  66895618              mov       [esi+18h],dx
  004b8afe  891e                  mov       [esi],ebx
  004b8b00  66895612              mov       [esi+12h],dx
  004b8b04  894e5c                mov       [esi+5Ch],ecx
  004b8b07  e9a0000000            jmp       004B8BACh
  004b8b0c  b801000000            mov       eax,1
  004b8b11  66394612              cmp       [esi+12h],ax
  004b8b15  7537                  jne       short 004B8B4Eh
  004b8b17  66837e1a07            cmp       word [esi+1Ah],7
  004b8b1c  7530                  jne       short 004B8B4Eh
  004b8b1e  66394616              cmp       [esi+16h],ax
  004b8b22  7512                  jne       short 004B8B36h
  004b8b24  6aff                  push      0FFFFFFFFh
  004b8b26  53                    push      ebx
  004b8b27  6a36                  push      36h
  004b8b29  56                    push      esi
  004b8b2a  e8a11efcff            call      0047A9D0h
  004b8b2f  8b7c2424              mov       edi,[esp+24h]
  004b8b33  83c410                add       esp,10h

LAB_004b8b36:
  004b8b36  66395e16              cmp       [esi+16h],bx
  004b8b3a  7512                  jne       short 004B8B4Eh
  004b8b3c  6aff                  push      0FFFFFFFFh
  004b8b3e  53                    push      ebx
  004b8b3f  6a4b                  push      4Bh
  004b8b41  56                    push      esi
  004b8b42  e8891efcff            call      0047A9D0h
  004b8b47  8b7c2424              mov       edi,[esp+24h]
  004b8b4b  83c410                add       esp,10h

LAB_004b8b4e:
  004b8b4e  8b06                  mov       eax,[esi]
  004b8b50  a900000002            test      eax,2000000h
  004b8b55  7403                  je        short 004B8B5Ah
  004b8b57  885e43                mov       [esi+43h],bl

LAB_004b8b5a:
  004b8b5a  66395e12              cmp       [esi+12h],bx
  004b8b5e  754c                  jne       short 004B8BACh
  004b8b60  25fffffffe            and       eax,0FEFFFFFFh
  004b8b65  83ff1e                cmp       edi,1Eh
  004b8b68  66c746120100          mov       word [esi+12h],1
  004b8b6e  66c746180300          mov       word [esi+18h],3
  004b8b74  66895e1a              mov       [esi+1Ah],bx
  004b8b78  8906                  mov       [esi],eax
  004b8b7a  7d30                  jge       short 004B8BACh

LAB_004b8b7c:
  004b8b7c  895e5c                mov       [esi+5Ch],ebx
  004b8b7f  eb2b                  jmp       short 004B8BACh
  004b8b81  668b4e12              mov       cx,[esi+12h]
  004b8b85  6683f901              cmp       cx,1
  004b8b89  7510                  jne       short 004B8B9Bh
  004b8b8b  8b06                  mov       eax,[esi]
  004b8b8d  a900000002            test      eax,2000000h
  004b8b92  7407                  je        short 004B8B9Bh
  004b8b94  24fd                  and       al,0FDh
  004b8b96  885e43                mov       [esi+43h],bl
  004b8b99  8906                  mov       [esi],eax

LAB_004b8b9b:
  004b8b9b  663bcb                cmp       cx,bx
  004b8b9e  750c                  jne       short 004B8BACh
  004b8ba0  66c746120100          mov       word [esi+12h],1

LAB_004b8ba6:
  004b8ba6  8126fffffffe          and       dword [esi],0FEFFFFFFh

LAB_004b8bac:
  004b8bac  0fbf5632              movsx     edx,word [esi+32h]
  004b8bb0  52                    push      edx
  004b8bb1  e866ce0800            call      00545A1Ch
  004b8bb6  89442470              mov       [esp+70h],eax
  004b8bba  0fbf4632              movsx     eax,word [esi+32h]
  004b8bbe  50                    push      eax
  004b8bbf  e852ce0800            call      00545A16h
  004b8bc4  8b4e50                mov       ecx,[esi+50h]
  004b8bc7  89442478              mov       [esp+78h],eax
  004b8bcb  0fbf9e9a000000        movsx     ebx,word [esi+9Ah]
  004b8bd2  8bc1                  mov       eax,ecx
  004b8bd4  8bd1                  mov       edx,ecx
  004b8bd6  c1f803                sar       eax,3
  004b8bd9  8b7c2420              mov       edi,[esp+20h]
  004b8bdd  2bd0                  sub       edx,eax
  004b8bdf  8b4654                mov       eax,[esi+54h]
  004b8be2  0faf7e5c              imul      edi,[esi+5Ch]
  004b8be6  c1f908                sar       ecx,8
  004b8be9  2bd1                  sub       edx,ecx
  004b8beb  8bcb                  mov       ecx,ebx
  004b8bed  8be8                  mov       ebp,eax
  004b8bef  895650                mov       [esi+50h],edx
  004b8bf2  c1f908                sar       ecx,8
  004b8bf5  c1fd08                sar       ebp,8
  004b8bf8  2bcd                  sub       ecx,ebp
  004b8bfa  8b6e58                mov       ebp,[esi+58h]
  004b8bfd  03c8                  add       ecx,eax
  004b8bff  8bc5                  mov       eax,ebp
  004b8c01  2bcb                  sub       ecx,ebx
  004b8c03  8bdd                  mov       ebx,ebp
  004b8c05  c1fb08                sar       ebx,8
  004b8c08  2bc3                  sub       eax,ebx
  004b8c0a  894e54                mov       [esi+54h],ecx
  004b8c0d  c1fd03                sar       ebp,3
  004b8c10  2bc5                  sub       eax,ebp
  004b8c12  8b6e20                mov       ebp,[esi+20h]
  004b8c15  c1ff0c                sar       edi,0Ch
  004b8c18  03e9                  add       ebp,ecx
  004b8c1a  8bdf                  mov       ebx,edi
  004b8c1c  0faf5c2470            imul      ebx,[esp+70h]
  004b8c21  8bcf                  mov       ecx,edi
  004b8c23  894658                mov       [esi+58h],eax
  004b8c26  0faf4c2478            imul      ecx,[esp+78h]
  004b8c2b  c1fb0c                sar       ebx,0Ch
  004b8c2e  c1f90c                sar       ecx,0Ch
  004b8c31  03da                  add       ebx,edx
  004b8c33  8b561c                mov       edx,[esi+1Ch]
  004b8c36  03c8                  add       ecx,eax
  004b8c38  8b4624                mov       eax,[esi+24h]
  004b8c3b  03d3                  add       edx,ebx
  004b8c3d  03c1                  add       eax,ecx
  004b8c3f  56                    push      esi
  004b8c40  89561c                mov       [esi+1Ch],edx
  004b8c43  896e20                mov       [esi+20h],ebp
  004b8c46  894624                mov       [esi+24h],eax
  004b8c49  e8f2ac0700            call      00533940h
  004b8c4e  8b9690000000          mov       edx,[esi+90h]
  004b8c54  8b4660                mov       eax,[esi+60h]
  004b8c57  8b4e20                mov       ecx,[esi+20h]
  004b8c5a  83c404                add       esp,4
  004b8c5d  80e6fc                and       dh,0FCh
  004b8c60  3bc8                  cmp       ecx,eax
  004b8c62  899690000000          mov       [esi+90h],edx
  004b8c68  7f2a                  jg        short 004B8C94h
  004b8c6a  f6c680                test      dh,80h
  004b8c6d  7525                  jne       short 004B8C94h
  004b8c6f  8b0e                  mov       ecx,[esi]
  004b8c71  894620                mov       [esi+20h],eax
  004b8c74  f6c101                test      cl,1
  004b8c77  740f                  je        short 004B8C88h
  004b8c79  83e1fe                and       ecx,0FFFFFFFEh
  004b8c7c  33db                  xor       ebx,ebx
  004b8c7e  890e                  mov       [esi],ecx
  004b8c80  895e54                mov       [esi+54h],ebx
  004b8c83  e92a010000            jmp       004B8DB2h

LAB_004b8c88:
  004b8c88  83e1fe                and       ecx,0FFFFFFFEh
  004b8c8b  33db                  xor       ebx,ebx
  004b8c8d  890e                  mov       [esi],ecx
  004b8c8f  895e54                mov       [esi+54h],ebx
  004b8c92  eb02                  jmp       short 004B8C96h

LAB_004b8c94:
  004b8c94  33db                  xor       ebx,ebx

LAB_004b8c96:
  004b8c96  8b0e                  mov       ecx,[esi]
  004b8c98  f6c101                test      cl,1
  004b8c9b  750a                  jne       short 004B8CA7h
  004b8c9d  394620                cmp       [esi+20h],eax
  004b8ca0  7e05                  jle       short 004B8CA7h
  004b8ca2  83c901                or        ecx,1
  004b8ca5  890e                  mov       [esi],ecx

LAB_004b8ca7:
  004b8ca7  f7c282ad0020          test      edx,2000AD82h
  004b8cad  0f84ff000000          je        near 004B8DB2h
  004b8cb3  8b06                  mov       eax,[esi]
  004b8cb5  a801                  test      al,1
  004b8cb7  0f85c9000000          jne       near 004B8D86h
  004b8cbd  8a4e42                mov       cl,[esi+42h]
  004b8cc0  b007                  mov       al,7
  004b8cc2  3ac8                  cmp       cl,al
  004b8cc4  7420                  je        short 004B8CE6h
  004b8cc6  884642                mov       [esi+42h],al
  004b8cc9  e872e40800            call      00547140h
  004b8cce  99                    cdq
  004b8ccf  b928000000            mov       ecx,28h
  004b8cd4  66895e12              mov       [esi+12h],bx
  004b8cd8  f7f9                  idiv      ecx
  004b8cda  66c746105000          mov       word [esi+10h],50h
  004b8ce0  80c214                add       dl,14h
  004b8ce3  885643                mov       [esi+43h],dl

LAB_004b8ce6:
  004b8ce6  8b16                  mov       edx,[esi]
  004b8ce8  8b442428              mov       eax,[esp+28h]
  004b8cec  8b4c242c              mov       ecx,[esp+2Ch]
  004b8cf0  83e2fd                and       edx,0FFFFFFFDh
  004b8cf3  8916                  mov       [esi],edx
  004b8cf5  8b542424              mov       edx,[esp+24h]
  004b8cf9  894620                mov       [esi+20h],eax
  004b8cfc  8b44244c              mov       eax,[esp+4Ch]
  004b8d00  89561c                mov       [esi+1Ch],edx
  004b8d03  8b542460              mov       edx,[esp+60h]
  004b8d07  25ffff0000            and       eax,0FFFFh
  004b8d0c  894e24                mov       [esi+24h],ecx
  004b8d0f  8b4c2450              mov       ecx,[esp+50h]
  004b8d13  89567c                mov       [esi+7Ch],edx
  004b8d16  8b54243c              mov       edx,[esp+3Ch]
  004b8d1a  898690000000          mov       [esi+90h],eax
  004b8d20  8b442444              mov       eax,[esp+44h]
  004b8d24  898e84000000          mov       [esi+84h],ecx
  004b8d2a  8b4c2454              mov       ecx,[esp+54h]
  004b8d2e  898680000000          mov       [esi+80h],eax
  004b8d34  8b44245c              mov       eax,[esp+5Ch]
  004b8d38  899688000000          mov       [esi+88h],edx
  004b8d3e  8b542448              mov       edx,[esp+48h]
  004b8d42  6882ad0020            push      2000AD82h
  004b8d47  56                    push      esi
  004b8d48  89868c000000          mov       [esi+8Ch],eax
  004b8d4e  894e64                mov       [esi+64h],ecx
  004b8d51  895660                mov       [esi+60h],edx
  004b8d54  e807d10100            call      004D5E60h
  004b8d59  83c408                add       esp,8
  004b8d5c  663bc3                cmp       ax,bx
  004b8d5f  7515                  jne       short 004B8D76h
  004b8d61  e8dae30800            call      00547140h
  004b8d66  99                    cdq
  004b8d67  33c2                  xor       eax,edx
  004b8d69  2bc2                  sub       eax,edx
  004b8d6b  83e001                and       eax,1
  004b8d6e  33c2                  xor       eax,edx
  004b8d70  2bc2                  sub       eax,edx
  004b8d72  8d4400ff              lea       eax,[eax+eax-1]

LAB_004b8d76:
  004b8d76  8b4c2414              mov       ecx,[esp+14h]
  004b8d7a  83c140                add       ecx,40h
  004b8d7d  0fafc8                imul      ecx,eax
  004b8d80  66014e32              add       [esi+32h],cx
  004b8d84  eb2c                  jmp       short 004B8DB2h

LAB_004b8d86:
  004b8d86  8b542424              mov       edx,[esp+24h]
  004b8d8a  24fd                  and       al,0FDh
  004b8d8c  8906                  mov       [esi],eax
  004b8d8e  8b44242c              mov       eax,[esp+2Ch]
  004b8d92  895e5c                mov       [esi+5Ch],ebx
  004b8d95  895e50                mov       [esi+50h],ebx
  004b8d98  895e58                mov       [esi+58h],ebx
  004b8d9b  89561c                mov       [esi+1Ch],edx
  004b8d9e  894624                mov       [esi+24h],eax
  004b8da1  885e42                mov       [esi+42h],bl
  004b8da4  c6464328              mov       byte [esi+43h],28h
  004b8da8  66895e12              mov       [esi+12h],bx
  004b8dac  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004b8db2:
  004b8db2  6aff                  push      0FFFFFFFFh
  004b8db4  6aff                  push      0FFFFFFFFh
  004b8db6  6aff                  push      0FFFFFFFFh
  004b8db8  56                    push      esi
  004b8db9  e852aa0000            call      004C3810h
  004b8dbe  8b442430              mov       eax,[esp+30h]
  004b8dc2  83c410                add       esp,10h
  004b8dc5  3bc3                  cmp       eax,ebx
  004b8dc7  7517                  jne       short 004B8DE0h
  004b8dc9  668b8c2486000000      mov       cx,[esp+86h]
  004b8dd1  66894e32              mov       [esi+32h],cx
  004b8dd5  5f                    pop       edi
  004b8dd6  5e                    pop       esi
  004b8dd7  5d                    pop       ebp
  004b8dd8  5b                    pop       ebx
  004b8dd9  81c488000000          add       esp,88h
  004b8ddf  c3                    ret

LAB_004b8de0:
  004b8de0  385e43                cmp       [esi+43h],bl
  004b8de3  0f8525030000          jne       near 004B910Eh
  004b8de9  8b466c                mov       eax,[esi+6Ch]
  004b8dec  8b561c                mov       edx,[esi+1Ch]
  004b8def  8b781c                mov       edi,[eax+1Ch]
  004b8df2  8b6824                mov       ebp,[eax+24h]
  004b8df5  8b4624                mov       eax,[esi+24h]
  004b8df8  2bfa                  sub       edi,edx
  004b8dfa  2be8                  sub       ebp,eax
  004b8dfc  c1ff0c                sar       edi,0Ch
  004b8dff  c1fd0c                sar       ebp,0Ch
  004b8e02  8bd7                  mov       edx,edi
  004b8e04  8bc5                  mov       eax,ebp
  004b8e06  0fafd7                imul      edx,edi
  004b8e09  0fafc5                imul      eax,ebp
  004b8e0c  03d0                  add       edx,eax
  004b8e0e  52                    push      edx
  004b8e0f  e8fccb0800            call      00545A10h
  004b8e14  55                    push      ebp
  004b8e15  57                    push      edi
  004b8e16  89442424              mov       [esp+24h],eax
  004b8e1a  e803cc0800            call      00545A22h
  004b8e1f  668b4e32              mov       cx,[esi+32h]
  004b8e23  66895e12              mov       [esi+12h],bx
  004b8e27  2bc1                  sub       eax,ecx
  004b8e29  25ff0f0000            and       eax,0FFFh
  004b8e2e  89442418              mov       [esp+18h],eax
  004b8e32  8b44241c              mov       eax,[esp+1Ch]
  004b8e36  3dfa000000            cmp       eax,0FAh
  004b8e3b  0f8d70010000          jge       near 004B8FB1h
  004b8e41  e8fae20800            call      00547140h
  004b8e46  99                    cdq
  004b8e47  b964000000            mov       ecx,64h
  004b8e4c  f7f9                  idiv      ecx
  004b8e4e  8bfa                  mov       edi,edx
  004b8e50  83ff32                cmp       edi,32h
  004b8e53  0f8dba000000          jge       near 004B8F13h
  004b8e59  8b6c2410              mov       ebp,[esp+10h]
  004b8e5d  8b542418              mov       edx,[esp+18h]
  004b8e61  8d84aa00010000        lea       eax,[edx+ebp*4+100h]
  004b8e68  8d0ced00020000        lea       ecx,[ebp*8+200h]
  004b8e6f  25ff0f0000            and       eax,0FFFh
  004b8e74  3bc1                  cmp       eax,ecx
  004b8e76  7f3e                  jg        short 004B8EB6h
  004b8e78  e8c3e20800            call      00547140h
  004b8e7d  99                    cdq
  004b8e7e  8d4c2d01              lea       ecx,[ebp+ebp+1]
  004b8e82  f7f9                  idiv      ecx
  004b8e84  8b44241c              mov       eax,[esp+1Ch]
  004b8e88  83c264                add       edx,64h
  004b8e8b  3bc2                  cmp       eax,edx
  004b8e8d  7d27                  jge       short 004B8EB6h
  004b8e8f  8b16                  mov       edx,[esi]
  004b8e91  66c746180300          mov       word [esi+18h],3
  004b8e97  81e2fffffffe          and       edx,0FEFFFFFFh
  004b8e9d  66895e1a              mov       [esi+1Ah],bx
  004b8ea1  83ca02                or        edx,2
  004b8ea4  c646420b              mov       byte [esi+42h],0Bh
  004b8ea8  8916                  mov       [esi],edx
  004b8eaa  c6464301              mov       byte [esi+43h],1
  004b8eae  895e5c                mov       [esi+5Ch],ebx
  004b8eb1  e95c020000            jmp       004B9112h

LAB_004b8eb6:
  004b8eb6  8b442418              mov       eax,[esp+18h]
  004b8eba  8d14ed00040000        lea       edx,[ebp*8+400h]
  004b8ec1  8d8ca800020000        lea       ecx,[eax+ebp*4+200h]
  004b8ec8  81e1ff0f0000          and       ecx,0FFFh
  004b8ece  3bca                  cmp       ecx,edx
  004b8ed0  7f34                  jg        short 004B8F06h
  004b8ed2  e869e20800            call      00547140h
  004b8ed7  99                    cdq
  004b8ed8  b964000000            mov       ecx,64h
  004b8edd  f7f9                  idiv      ecx
  004b8edf  8bc5                  mov       eax,ebp
  004b8ee1  d1f8                  sar       eax,1
  004b8ee3  83c020                add       eax,20h
  004b8ee6  3bd0                  cmp       edx,eax
  004b8ee8  7d1c                  jge       short 004B8F06h
  004b8eea  c6464205              mov       byte [esi+42h],5
  004b8eee  e84de20800            call      00547140h
  004b8ef3  99                    cdq
  004b8ef4  b905000000            mov       ecx,5
  004b8ef9  f7f9                  idiv      ecx
  004b8efb  80c20a                add       dl,0Ah
  004b8efe  885643                mov       [esi+43h],dl
  004b8f01  e90c020000            jmp       004B9112h

LAB_004b8f06:
  004b8f06  c6464201              mov       byte [esi+42h],1
  004b8f0a  c6464301              mov       byte [esi+43h],1
  004b8f0e  e9ff010000            jmp       004B9112h

LAB_004b8f13:
  004b8f13  83ff46                cmp       edi,46h
  004b8f16  7d2c                  jge       short 004B8F44h
  004b8f18  c6464202              mov       byte [esi+42h],2
  004b8f1c  e81fe20800            call      00547140h
  004b8f21  99                    cdq
  004b8f22  b90a000000            mov       ecx,0Ah
  004b8f27  8b6c2410              mov       ebp,[esp+10h]
  004b8f2b  f7f9                  idiv      ecx
  004b8f2d  8bc5                  mov       eax,ebp
  004b8f2f  66c746106400          mov       word [esi+10h],64h
  004b8f35  d1f8                  sar       eax,1
  004b8f37  02d0                  add       dl,al
  004b8f39  80c214                add       dl,14h
  004b8f3c  885643                mov       [esi+43h],dl
  004b8f3f  e9ce010000            jmp       004B9112h

LAB_004b8f44:
  004b8f44  83ff5a                cmp       edi,5Ah
  004b8f47  7d13                  jge       short 004B8F5Ch
  004b8f49  8b4c2414              mov       ecx,[esp+14h]
  004b8f4d  8b44241c              mov       eax,[esp+1Ch]
  004b8f51  83c164                add       ecx,64h
  004b8f54  3bc1                  cmp       eax,ecx
  004b8f56  0f8f73010000          jg        near 004B90CFh

LAB_004b8f5c:
  004b8f5c  c6464203              mov       byte [esi+42h],3
  004b8f60  e8dbe10800            call      00547140h
  004b8f65  99                    cdq
  004b8f66  b90a000000            mov       ecx,0Ah
  004b8f6b  8b6c2410              mov       ebp,[esp+10h]
  004b8f6f  f7f9                  idiv      ecx
  004b8f71  8b4c2414              mov       ecx,[esp+14h]
  004b8f75  8bc5                  mov       eax,ebp
  004b8f77  d1f8                  sar       eax,1
  004b8f79  8db980000000          lea       edi,[ecx+80h]
  004b8f7f  c1e704                shl       edi,4
  004b8f82  02d0                  add       dl,al
  004b8f84  80c214                add       dl,14h
  004b8f87  885643                mov       [esi+43h],dl
  004b8f8a  e8b1e10800            call      00547140h
  004b8f8f  99                    cdq
  004b8f90  f7ff                  idiv      edi
  004b8f92  8bc7                  mov       eax,edi
  004b8f94  d1f8                  sar       eax,1
  004b8f96  2bd0                  sub       edx,eax
  004b8f98  8b442418              mov       eax,[esp+18h]
  004b8f9c  03d0                  add       edx,eax
  004b8f9e  66035632              add       dx,[esi+32h]
  004b8fa2  81c200080000          add       edx,800h
  004b8fa8  66895610              mov       [esi+10h],dx
  004b8fac  e961010000            jmp       004B9112h

LAB_004b8fb1:
  004b8fb1  3d58020000            cmp       eax,258h
  004b8fb6  0f8dcb000000          jge       near 004B9087h
  004b8fbc  e87fe10800            call      00547140h
  004b8fc1  99                    cdq
  004b8fc2  b964000000            mov       ecx,64h
  004b8fc7  f7f9                  idiv      ecx
  004b8fc9  8bfa                  mov       edi,edx
  004b8fcb  83ff1e                cmp       edi,1Eh
  004b8fce  7d32                  jge       short 004B9002h
  004b8fd0  8b542418              mov       edx,[esp+18h]
  004b8fd4  81c200040000          add       edx,400h
  004b8fda  81e2ff0f0000          and       edx,0FFFh
  004b8fe0  81fa00080000          cmp       edx,800h
  004b8fe6  7c0d                  jl        short 004B8FF5h
  004b8fe8  c6464201              mov       byte [esi+42h],1
  004b8fec  c6464301              mov       byte [esi+43h],1
  004b8ff0  e919010000            jmp       004B910Eh

LAB_004b8ff5:
  004b8ff5  c646420a              mov       byte [esi+42h],0Ah
  004b8ff9  c6464301              mov       byte [esi+43h],1
  004b8ffd  e90c010000            jmp       004B910Eh

LAB_004b9002:
  004b9002  83ff32                cmp       edi,32h
  004b9005  7d4f                  jge       short 004B9056h
  004b9007  c6464203              mov       byte [esi+42h],3
  004b900b  e830e10800            call      00547140h
  004b9010  99                    cdq
  004b9011  b90a000000            mov       ecx,0Ah
  004b9016  8b6c2410              mov       ebp,[esp+10h]
  004b901a  f7f9                  idiv      ecx
  004b901c  8b4c2414              mov       ecx,[esp+14h]
  004b9020  8bc5                  mov       eax,ebp
  004b9022  d1f8                  sar       eax,1
  004b9024  8db980000000          lea       edi,[ecx+80h]
  004b902a  c1e704                shl       edi,4
  004b902d  02d0                  add       dl,al
  004b902f  80c214                add       dl,14h
  004b9032  885643                mov       [esi+43h],dl
  004b9035  e806e10800            call      00547140h
  004b903a  99                    cdq
  004b903b  f7ff                  idiv      edi
  004b903d  8b4c2418              mov       ecx,[esp+18h]
  004b9041  8bc7                  mov       eax,edi
  004b9043  d1f8                  sar       eax,1
  004b9045  2bd0                  sub       edx,eax
  004b9047  03d1                  add       edx,ecx
  004b9049  66035632              add       dx,[esi+32h]
  004b904d  66895610              mov       [esi+10h],dx
  004b9051  e9bc000000            jmp       004B9112h

LAB_004b9056:
  004b9056  83ff5a                cmp       edi,5Ah
  004b9059  7d40                  jge       short 004B909Bh
  004b905b  c6464202              mov       byte [esi+42h],2
  004b905f  e8dce00800            call      00547140h
  004b9064  99                    cdq
  004b9065  b90a000000            mov       ecx,0Ah
  004b906a  8b6c2410              mov       ebp,[esp+10h]
  004b906e  f7f9                  idiv      ecx
  004b9070  8bc5                  mov       eax,ebp
  004b9072  66c746106400          mov       word [esi+10h],64h
  004b9078  d1f8                  sar       eax,1
  004b907a  02d0                  add       dl,al
  004b907c  80c214                add       dl,14h
  004b907f  885643                mov       [esi+43h],dl
  004b9082  e98b000000            jmp       004B9112h

LAB_004b9087:
  004b9087  e8b4e00800            call      00547140h
  004b908c  99                    cdq
  004b908d  b964000000            mov       ecx,64h
  004b9092  f7f9                  idiv      ecx
  004b9094  8bfa                  mov       edi,edx
  004b9096  83ff14                cmp       edi,14h
  004b9099  7d20                  jge       short 004B90BBh

LAB_004b909b:
  004b909b  885e42                mov       [esi+42h],bl
  004b909e  e89de00800            call      00547140h
  004b90a3  99                    cdq
  004b90a4  b90a000000            mov       ecx,0Ah
  004b90a9  8b6c2410              mov       ebp,[esp+10h]
  004b90ad  f7f9                  idiv      ecx
  004b90af  8bc5                  mov       eax,ebp
  004b90b1  c1f802                sar       eax,2
  004b90b4  02d0                  add       dl,al
  004b90b6  885643                mov       [esi+43h],dl
  004b90b9  eb57                  jmp       short 004B9112h

LAB_004b90bb:
  004b90bb  83ff3c                cmp       edi,3Ch
  004b90be  7d0a                  jge       short 004B90CAh
  004b90c0  c6464201              mov       byte [esi+42h],1
  004b90c4  c6464301              mov       byte [esi+43h],1
  004b90c8  eb44                  jmp       short 004B910Eh

LAB_004b90ca:
  004b90ca  83ff50                cmp       edi,50h
  004b90cd  7d13                  jge       short 004B90E2h

LAB_004b90cf:
  004b90cf  c6464206              mov       byte [esi+42h],6
  004b90d3  c6464301              mov       byte [esi+43h],1
  004b90d7  e864e00800            call      00547140h
  004b90dc  66894610              mov       [esi+10h],ax
  004b90e0  eb2c                  jmp       short 004B910Eh

LAB_004b90e2:
  004b90e2  c6464203              mov       byte [esi+42h],3
  004b90e6  e855e00800            call      00547140h
  004b90eb  99                    cdq
  004b90ec  b90a000000            mov       ecx,0Ah
  004b90f1  8b6c2410              mov       ebp,[esp+10h]
  004b90f5  f7f9                  idiv      ecx
  004b90f7  8bc5                  mov       eax,ebp
  004b90f9  d1f8                  sar       eax,1
  004b90fb  02d0                  add       dl,al
  004b90fd  80c214                add       dl,14h
  004b9100  885643                mov       [esi+43h],dl
  004b9103  e838e00800            call      00547140h
  004b9108  66894610              mov       [esi+10h],ax
  004b910c  eb04                  jmp       short 004B9112h

LAB_004b910e:
  004b910e  8b6c2410              mov       ebp,[esp+10h]

LAB_004b9112:
  004b9112  8b4c2418              mov       ecx,[esp+18h]
  004b9116  8d04ed00040000        lea       eax,[ebp*8+400h]
  004b911d  8d94a900020000        lea       edx,[ecx+ebp*4+200h]
  004b9124  81e2ff0f0000          and       edx,0FFFh
  004b912a  3bd0                  cmp       edx,eax
  004b912c  7f55                  jg        short 004B9183h
  004b912e  8b4c2440              mov       ecx,[esp+40h]
  004b9132  f60102                test      byte [ecx],2
  004b9135  744c                  je        short 004B9183h
  004b9137  3bfd                  cmp       edi,ebp
  004b9139  7d48                  jge       short 004B9183h
  004b913b  f60601                test      byte [esi],1
  004b913e  7543                  jne       short 004B9183h
  004b9140  66837e1805            cmp       word [esi+18h],5
  004b9145  743c                  je        short 004B9183h
  004b9147  817c241cc8000000      cmp       dword [esp+1Ch],0C8h
  004b914f  7d32                  jge       short 004B9183h
  004b9151  c6464205              mov       byte [esi+42h],5
  004b9155  e8e6df0800            call      00547140h
  004b915a  99                    cdq
  004b915b  b905000000            mov       ecx,5
  004b9160  66c7461a0600          mov       word [esi+1Ah],6
  004b9166  f7f9                  idiv      ecx
  004b9168  8b06                  mov       eax,[esi]
  004b916a  66894e18              mov       [esi+18h],cx
  004b916e  895e5c                mov       [esi+5Ch],ebx
  004b9171  66c746120100          mov       word [esi+12h],1
  004b9177  02d1                  add       dl,cl
  004b9179  25fffffffe            and       eax,0FEFFFFFFh
  004b917e  885643                mov       [esi+43h],dl
  004b9181  8906                  mov       [esi],eax

LAB_004b9183:
  004b9183  5f                    pop       edi
  004b9184  5e                    pop       esi
  004b9185  5d                    pop       ebp
  004b9186  5b                    pop       ebx
  004b9187  81c488000000          add       esp,88h
  004b918d  c3                    ret

LAB_004b918e:
  004b918e  83e1fd                and       ecx,0FFFFFFFDh
  004b9191  890e                  mov       [esi],ecx
  004b9193  e90ff6ffff            jmp       004B87A7h

LAB_004b9198:
  004b9198  f6c101                test      cl,1
  004b919b  0f8406f6ffff          je        near 004B87A7h
  004b91a1  66837e1a03            cmp       word [esi+1Ah],3
  004b91a6  0f8dfbf5ffff          jge       near 004B87A7h
  004b91ac  e88fdf0800            call      00547140h
  004b91b1  99                    cdq
  004b91b2  b93c000000            mov       ecx,3Ch
  004b91b7  8b7c2414              mov       edi,[esp+14h]
  004b91bb  f7f9                  idiv      ecx
  004b91bd  3bd7                  cmp       edx,edi
  004b91bf  7d20                  jge       short 004B91E1h
  004b91c1  8b16                  mov       edx,[esi]
  004b91c3  8b442420              mov       eax,[esp+20h]
  004b91c7  81e2fffffffe          and       edx,0FEFFFFFFh
  004b91cd  66c746180300          mov       word [esi+18h],3
  004b91d3  83ca02                or        edx,2
  004b91d6  66895e1a              mov       [esi+1Ah],bx
  004b91da  8916                  mov       [esi],edx
  004b91dc  e9caf5ffff            jmp       004B87ABh

LAB_004b91e1:
  004b91e1  8b442420              mov       eax,[esp+20h]
  004b91e5  e9c1f5ffff            jmp       004B87ABh
  004b91ea  8bff                  mov       edi,edi
  004b91ec  c0874b000a884b        rol       byte [edi-77F5FFB5h],4Bh
  004b91f3  007c884b              add       [eax+ecx*4+4Bh],bh
  004b91f7  001b                  add       [ebx],bl
  004b91f9  894b00                mov       [ebx],ecx
  004b91fc  ac                    lodsb
  004b91fd  8b4b00                mov       ecx,[ebx]
  004b9200  e089                  loopne    004B918Bh
  004b9202  4b                    dec       ebx
  004b9203  001f                  add       [edi],bl
  004b9205  8a4b00                mov       cl,[ebx]
  004b9208  b08a                  mov       al,8Ah
  004b920a  4b                    dec       ebx
  004b920b  00ac8b4b00ac8b        add       [ebx+ecx*4-7453FFB5h],ch
  004b9212  4b                    dec       ebx
  004b9213  000c8b                add       [ebx+ecx*4],cl
  004b9216  4b                    dec       ebx
  004b9217  00818b4b0090          add       [ecx-6FFFB475h],al
  004b921d  90                    nop
  004b921e  90                    nop
  004b921f  90                    nop

67 cible(s) de saut interne : 0x004b858e, 0x004b8590, 0x004b8599, 0x004b86ec, 0x004b870e, 0x004b8710, 0x004b871f, 0x004b8722, 0x004b8788, 0x004b878c, 0x004b87a7, 0x004b87ab, 0x004b87e0, 0x004b87f2, 0x004b882b, 0x004b884e, 0x004b8869, 0x004b88ac, 0x004b88cb, 0x004b88d7, 0x004b890b, 0x004b8932, 0x004b89a9, 0x004b89b4, 0x004b89b8, 0x004b89fe, 0x004b8a31, 0x004b8a9f, 0x004b8acd, 0x004b8ad9, 0x004b8b36, 0x004b8b4e, 0x004b8b5a, 0x004b8b7c, 0x004b8b9b, 0x004b8ba6, 0x004b8bac, 0x004b8c88, 0x004b8c94, 0x004b8c96, 0x004b8ca7, 0x004b8ce6, 0x004b8d76, 0x004b8d86, 0x004b8db2, 0x004b8de0, 0x004b8eb6, 0x004b8f06, 0x004b8f13, 0x004b8f44, 0x004b8f5c, 0x004b8fb1, 0x004b8ff5, 0x004b9002, 0x004b9056, 0x004b9087, 0x004b909b, 0x004b90bb, 0x004b90ca, 0x004b90cf, 0x004b90e2, 0x004b910e, 0x004b9112, 0x004b9183, 0x004b918e, 0x004b9198, 0x004b91e1
