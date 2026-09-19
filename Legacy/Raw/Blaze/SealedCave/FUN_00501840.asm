Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00501840  section .text  offset fichier 0x100c40  2512 octets

  00501840  83ec1c                sub       esp,1Ch
  00501843  8d442410              lea       eax,[esp+10h]
  00501847  8d4c2414              lea       ecx,[esp+14h]
  0050184b  8d54240c              lea       edx,[esp+0Ch]
  0050184f  55                    push      ebp
  00501850  56                    push      esi
  00501851  8b742428              mov       esi,[esp+28h]
  00501855  57                    push      edi
  00501856  50                    push      eax
  00501857  51                    push      ecx
  00501858  52                    push      edx
  00501859  56                    push      esi
  0050185a  e821710300            call      00538980h
  0050185f  83c410                add       esp,10h
  00501862  8d442414              lea       eax,[esp+14h]
  00501866  8d4c2410              lea       ecx,[esp+10h]
  0050186a  8d54242c              lea       edx,[esp+2Ch]
  0050186e  50                    push      eax
  0050186f  51                    push      ecx
  00501870  8d442414              lea       eax,[esp+14h]
  00501874  52                    push      edx
  00501875  8d4c2430              lea       ecx,[esp+30h]
  00501879  50                    push      eax
  0050187a  51                    push      ecx
  0050187b  56                    push      esi
  0050187c  e89f460300            call      00535F20h
  00501881  8b442428              mov       eax,[esp+28h]
  00501885  83c418                add       esp,18h
  00501888  85c0                  test      eax,eax
  0050188a  bd01000000            mov       ebp,1
  0050188f  bf07000000            mov       edi,7
  00501894  0f8492060000          je        near 00501F2Ch
  0050189a  56                    push      esi
  0050189b  e8009dfdff            call      004DB5A0h
  005018a0  8b06                  mov       eax,[esi]
  005018a2  83c404                add       esp,4
  005018a5  a802                  test      al,2
  005018a7  0f8478030000          je        near 00501C25h
  005018ad  f644241410            test      byte [esp+14h],10h
  005018b2  740d                  je        short 005018C1h
  005018b4  24fd                  and       al,0FDh
  005018b6  c6464300              mov       byte [esi+43h],0
  005018ba  8906                  mov       [esi],eax
  005018bc  e96b060000            jmp       00501F2Ch

LAB_005018c1:
  005018c1  33c9                  xor       ecx,ecx
  005018c3  8a4e42                mov       cl,[esi+42h]
  005018c6  83f903                cmp       ecx,3
  005018c9  0f875d060000          ja        near 00501F2Ch
  005018cf  ff248dd8215000        jmp       dword [ecx*4+5021D8h]
  005018d6  668b5610              mov       dx,[esi+10h]
  005018da  0fbfca                movsx     ecx,dx
  005018dd  83e900                sub       ecx,0
  005018e0  0f84b0000000          je        near 00501996h
  005018e6  49                    dec       ecx
  005018e7  7457                  je        short 00501940h
  005018e9  49                    dec       ecx
  005018ea  0f853c060000          jne       near 00501F2Ch
  005018f0  8b465c                mov       eax,[esi+5Ch]
  005018f3  8bc8                  mov       ecx,eax
  005018f5  c1f902                sar       ecx,2
  005018f8  2bc1                  sub       eax,ecx
  005018fa  66837e1a05            cmp       word [esi+1Ah],5
  005018ff  89465c                mov       [esi+5Ch],eax
  00501902  7505                  jne       short 00501909h
  00501904  4a                    dec       edx
  00501905  66895610              mov       [esi+10h],dx

LAB_00501909:
  00501909  8b44242c              mov       eax,[esp+2Ch]
  0050190d  8d5040                lea       edx,[eax+40h]
  00501910  81e2ff0f0000          and       edx,0FFFh
  00501916  81fa80000000          cmp       edx,80h
  0050191c  0f8e06060000          jle       near 00501F28h
  00501922  25ff0f0000            and       eax,0FFFh
  00501927  33c9                  xor       ecx,ecx
  00501929  3d00080000            cmp       eax,800h
  0050192e  0f9dc1                setge     cl
  00501931  49                    dec       ecx
  00501932  81e180000000          and       ecx,80h
  00501938  83c1c0                add       ecx,0FFFFFFC0h
  0050193b  e9e6050000            jmp       00501F26h

LAB_00501940:
  00501940  0fbf5632              movsx     edx,word [esi+32h]
  00501944  52                    push      edx
  00501945  c7465c00000300        mov       dword [esi+5Ch],30000h
  0050194c  e8cb400400            call      00545A1Ch
  00501951  0faf465c              imul      eax,[esi+5Ch]
  00501955  8b5650                mov       edx,[esi+50h]
  00501958  c1f80c                sar       eax,0Ch
  0050195b  03d0                  add       edx,eax
  0050195d  0fbf4632              movsx     eax,word [esi+32h]
  00501961  50                    push      eax
  00501962  895650                mov       [esi+50h],edx
  00501965  e8ac400400            call      00545A16h
  0050196a  0faf465c              imul      eax,[esi+5Ch]
  0050196e  8b5658                mov       edx,[esi+58h]
  00501971  8b0e                  mov       ecx,[esi]
  00501973  c1f80c                sar       eax,0Ch
  00501976  03d0                  add       edx,eax
  00501978  0bcd                  or        ecx,ebp
  0050197a  66ff4e10              dec       word [esi+10h]
  0050197e  895658                mov       [esi+58h],edx
  00501981  c7465400800100        mov       dword [esi+54h],18000h
  00501988  890e                  mov       [esi],ecx
  0050198a  c7465c00000000        mov       dword [esi+5Ch],0
  00501991  e996050000            jmp       00501F2Ch

LAB_00501996:
  00501996  a801                  test      al,1
  00501998  7475                  je        short 00501A0Fh
  0050199a  8b4654                mov       eax,[esi+54h]
  0050199d  85c0                  test      eax,eax
  0050199f  7c0a                  jl        short 005019ABh
  005019a1  66397e1a              cmp       [esi+1Ah],di
  005019a5  7e0e                  jle       short 005019B5h
  005019a7  66897e1a              mov       [esi+1Ah],di

LAB_005019ab:
  005019ab  66397e1a              cmp       [esi+1Ah],di
  005019af  0f8f77050000          jg        near 00501F2Ch

LAB_005019b5:
  005019b5  6a00                  push      0
  005019b7  8d4e1c                lea       ecx,[esi+1Ch]
  005019ba  68e0305500            push      5530E0h
  005019bf  51                    push      ecx
  005019c0  56                    push      esi
  005019c1  e86a820100            call      00519C30h
  005019c6  8bf8                  mov       edi,eax
  005019c8  a1b8e05d00            mov       eax,[5DE0B8h]
  005019cd  83c410                add       esp,10h
  005019d0  85c0                  test      eax,eax
  005019d2  0f8454050000          je        near 00501F2Ch
  005019d8  8b2f                  mov       ebp,[edi]
  005019da  57                    push      edi
  005019db  83cd40                or        ebp,40h
  005019de  56                    push      esi
  005019df  66c787960000001000    mov       word [edi+96h],10h
  005019e8  892f                  mov       [edi],ebp
  005019ea  e891810300            call      00539B80h
  005019ef  83c408                add       esp,8
  005019f2  83c71c                add       edi,1Ch
  005019f5  57                    push      edi
  005019f6  6800180000            push      1800h
  005019fb  6800030000            push      300h
  00501a00  6a03                  push      3
  00501a02  e819c10300            call      0053DB20h
  00501a07  83c410                add       esp,10h
  00501a0a  e91d050000            jmp       00501F2Ch

LAB_00501a0f:
  00501a0f  a900000002            test      eax,2000000h
  00501a14  0f8412050000          je        near 00501F2Ch
  00501a1a  24fd                  and       al,0FDh
  00501a1c  8906                  mov       [esi],eax
  00501a1e  e909050000            jmp       00501F2Ch
  00501a23  8a5643                mov       dl,[esi+43h]
  00501a26  8bca                  mov       ecx,edx
  00501a28  81e1ff000000          and       ecx,0FFh
  00501a2e  83e900                sub       ecx,0
  00501a31  0f84f5040000          je        near 00501F2Ch
  00501a37  49                    dec       ecx
  00501a38  7465                  je        short 00501A9Fh
  00501a3a  49                    dec       ecx
  00501a3b  0f85eb040000          jne       near 00501F2Ch
  00501a41  66837e1a0c            cmp       word [esi+1Ah],0Ch
  00501a46  7538                  jne       short 00501A80h
  00501a48  8b15cc975e00          mov       edx,[5E97CCh]
  00501a4e  6aff                  push      0FFFFFFFFh
  00501a50  6aff                  push      0FFFFFFFFh
  00501a52  8b82cc000000          mov       eax,[edx+0CCh]
  00501a58  33d2                  xor       edx,edx
  00501a5a  8b08                  mov       ecx,[eax]
  00501a5c  8a5108                mov       dl,[ecx+8]
  00501a5f  81c2c0000000          add       edx,0C0h
  00501a65  52                    push      edx
  00501a66  56                    push      esi
  00501a67  e8648ff7ff            call      0047A9D0h
  00501a6c  83c410                add       esp,10h
  00501a6f  8d461c                lea       eax,[esi+1Ch]
  00501a72  50                    push      eax
  00501a73  6818315500            push      553118h
  00501a78  e863c10300            call      0053DBE0h
  00501a7d  83c408                add       esp,8

LAB_00501a80:
  00501a80  f70600000002          test      dword [esi],2000000h
  00501a86  0f84a0040000          je        near 00501F2Ch
  00501a8c  8a4643                mov       al,[esi+43h]
  00501a8f  66c746101400          mov       word [esi+10h],14h
  00501a95  fec8                  dec       al
  00501a97  884643                mov       [esi+43h],al
  00501a9a  e98d040000            jmp       00501F2Ch

LAB_00501a9f:
  00501a9f  668b4e10              mov       cx,[esi+10h]
  00501aa3  6685c9                test      cx,cx
  00501aa6  740a                  je        short 00501AB2h
  00501aa8  49                    dec       ecx
  00501aa9  66894e10              mov       [esi+10h],cx
  00501aad  e97a040000            jmp       00501F2Ch

LAB_00501ab2:
  00501ab2  24fd                  and       al,0FDh
  00501ab4  feca                  dec       dl
  00501ab6  8906                  mov       [esi],eax
  00501ab8  885643                mov       [esi+43h],dl
  00501abb  e96c040000            jmp       00501F2Ch
  00501ac0  0fbf4e10              movsx     ecx,word [esi+10h]
  00501ac4  83e900                sub       ecx,0
  00501ac7  0f843a010000          je        near 00501C07h
  00501acd  49                    dec       ecx
  00501ace  0f8547030000          jne       near 00501E1Bh
  00501ad4  a900000002            test      eax,2000000h
  00501ad9  0f843c030000          je        near 00501E1Bh
  00501adf  0fbf7e34              movsx     edi,word [esi+34h]
  00501ae3  8b6e5c                mov       ebp,[esi+5Ch]
  00501ae6  6a00                  push      0
  00501ae8  56                    push      esi
  00501ae9  66c74634800f          mov       word [esi+34h],0F80h
  00501aef  c7465c46000000        mov       dword [esi+5Ch],46h
  00501af6  e8753c0300            call      00535770h
  00501afb  0fbf08                movsx     ecx,word [eax]
  00501afe  c1e10c                shl       ecx,0Ch
  00501b01  894e44                mov       [esi+44h],ecx
  00501b04  83c408                add       esp,8
  00501b07  0fbf5002              movsx     edx,word [eax+2]
  00501b0b  c1e20c                shl       edx,0Ch
  00501b0e  895648                mov       [esi+48h],edx
  00501b11  6a04                  push      4
  00501b13  0fbf4004              movsx     eax,word [eax+4]
  00501b17  c1e00c                shl       eax,0Ch
  00501b1a  89464c                mov       [esi+4Ch],eax
  00501b1d  8b0dcc975e00          mov       ecx,[5E97CCh]
  00501b23  6a00                  push      0
  00501b25  8b91cc000000          mov       edx,[ecx+0CCh]
  00501b2b  33c9                  xor       ecx,ecx
  00501b2d  8b02                  mov       eax,[edx]
  00501b2f  8a4812                mov       cl,[eax+12h]
  00501b32  81c1c0000000          add       ecx,0C0h
  00501b38  51                    push      ecx
  00501b39  56                    push      esi
  00501b3a  e8918ef7ff            call      0047A9D0h
  00501b3f  83c410                add       esp,10h
  00501b42  66ff4e10              dec       word [esi+10h]
  00501b46  8d561c                lea       edx,[esi+1Ch]
  00501b49  66897e34              mov       [esi+34h],di
  00501b4d  52                    push      edx
  00501b4e  6860315500            push      553160h
  00501b53  896e5c                mov       [esi+5Ch],ebp
  00501b56  c6464314              mov       byte [esi+43h],14h
  00501b5a  e881c00300            call      0053DBE0h
  00501b5f  83c408                add       esp,8
  00501b62  c7465c00000000        mov       dword [esi+5Ch],0
  00501b69  e9be030000            jmp       00501F2Ch
  00501b6e  0fbf4e10              movsx     ecx,word [esi+10h]
  00501b72  83e900                sub       ecx,0
  00501b75  0f848c000000          je        near 00501C07h
  00501b7b  49                    dec       ecx
  00501b7c  0f8599020000          jne       near 00501E1Bh
  00501b82  66837e1a0a            cmp       word [esi+1Ah],0Ah
  00501b87  7511                  jne       short 00501B9Ah
  00501b89  8d461c                lea       eax,[esi+1Ch]
  00501b8c  50                    push      eax
  00501b8d  6800315500            push      553100h
  00501b92  e849c00300            call      0053DBE0h
  00501b97  83c408                add       esp,8

LAB_00501b9a:
  00501b9a  f70600000002          test      dword [esi],2000000h
  00501ba0  0f8475020000          je        near 00501E1Bh
  00501ba6  8b7e44                mov       edi,[esi+44h]
  00501ba9  8b6e48                mov       ebp,[esi+48h]
  00501bac  e88f550400            call      00547140h
  00501bb1  99                    cdq
  00501bb2  b907000000            mov       ecx,7
  00501bb7  c74648983a0000        mov       dword [esi+48h],3A98h
  00501bbe  f7f9                  idiv      ecx
  00501bc0  6a02                  push      2
  00501bc2  6a00                  push      0
  00501bc4  83c202                add       edx,2
  00501bc7  895644                mov       [esi+44h],edx
  00501bca  8b15cc975e00          mov       edx,[5E97CCh]
  00501bd0  8b82cc000000          mov       eax,[edx+0CCh]
  00501bd6  33d2                  xor       edx,edx
  00501bd8  8b08                  mov       ecx,[eax]
  00501bda  8a510e                mov       dl,[ecx+0Eh]
  00501bdd  81c2c0000000          add       edx,0C0h
  00501be3  52                    push      edx
  00501be4  56                    push      esi
  00501be5  e8e68df7ff            call      0047A9D0h
  00501bea  83c410                add       esp,10h
  00501bed  66ff4e10              dec       word [esi+10h]
  00501bf1  897e44                mov       [esi+44h],edi
  00501bf4  896e48                mov       [esi+48h],ebp
  00501bf7  c646430a              mov       byte [esi+43h],0Ah
  00501bfb  c7465c00000000        mov       dword [esi+5Ch],0
  00501c02  e925030000            jmp       00501F2Ch

LAB_00501c07:
  00501c07  8a4e43                mov       cl,[esi+43h]
  00501c0a  fec9                  dec       cl
  00501c0c  884e43                mov       [esi+43h],cl
  00501c0f  0f8506020000          jne       near 00501E1Bh
  00501c15  24fd                  and       al,0FDh
  00501c17  c7465c00000000        mov       dword [esi+5Ch],0
  00501c1e  8906                  mov       [esi],eax
  00501c20  e907030000            jmp       00501F2Ch

LAB_00501c25:
  00501c25  a810                  test      al,10h
  00501c27  0f85ff020000          jne       near 00501F2Ch
  00501c2d  a904080000            test      eax,804h
  00501c32  0f842f010000          je        near 00501D67h
  00501c38  f7442414e8030040      test      dword [esp+14h],400003E8h
  00501c40  740b                  je        short 00501C4Dh
  00501c42  25fbf7ffff            and       eax,0FFFFF7FBh
  00501c47  c6464300              mov       byte [esi+43h],0
  00501c4b  8906                  mov       [esi],eax

LAB_00501c4d:
  00501c4d  668b4610              mov       ax,[esi+10h]
  00501c51  0fbfc8                movsx     ecx,ax
  00501c54  83f903                cmp       ecx,3
  00501c57  0f87cf020000          ja        near 00501F2Ch
  00501c5d  ff248de8215000        jmp       dword [ecx*4+5021E8h]
  00501c64  8b06                  mov       eax,[esi]
  00501c66  a900000002            test      eax,2000000h
  00501c6b  0f84bb020000          je        near 00501F2Ch
  00501c71  80e4f7                and       ah,0F7h
  00501c74  8906                  mov       [esi],eax
  00501c76  e9b1020000            jmp       00501F2Ch
  00501c7b  66397e18              cmp       [esi+18h],di
  00501c7f  7411                  je        short 00501C92h
  00501c81  8b06                  mov       eax,[esi]
  00501c83  897e18                mov       [esi+18h],edi
  00501c86  25fbfffffe            and       eax,0FEFFFFFBh
  00501c8b  8906                  mov       [esi],eax
  00501c8d  e99a020000            jmp       00501F2Ch

LAB_00501c92:
  00501c92  66837e1a0e            cmp       word [esi+1Ah],0Eh
  00501c97  0f858f020000          jne       near 00501F2Ch
  00501c9d  33c0                  xor       eax,eax
  00501c9f  6aff                  push      0FFFFFFFFh
  00501ca1  668b4606              mov       ax,[esi+6]
  00501ca5  6aff                  push      0FFFFFFFFh
  00501ca7  8bc8                  mov       ecx,eax
  00501ca9  c1f808                sar       eax,8
  00501cac  81e1ff000000          and       ecx,0FFh
  00501cb2  25ff000000            and       eax,0FFh
  00501cb7  51                    push      ecx
  00501cb8  50                    push      eax
  00501cb9  56                    push      esi
  00501cba  e8c18c0300            call      0053A980h
  00501cbf  83c414                add       esp,14h
  00501cc2  66ff4e10              dec       word [esi+10h]
  00501cc6  e961020000            jmp       00501F2Ch
  00501ccb  8a4e43                mov       cl,[esi+43h]
  00501cce  fec9                  dec       cl
  00501cd0  884e43                mov       [esi+43h],cl
  00501cd3  7505                  jne       short 00501CDAh
  00501cd5  48                    dec       eax
  00501cd6  66894610              mov       [esi+10h],ax

LAB_00501cda:
  00501cda  8b44242c              mov       eax,[esp+2Ch]
  00501cde  8d9080000000          lea       edx,[eax+80h]
  00501ce4  81e2ff0f0000          and       edx,0FFFh
  00501cea  81fa00010000          cmp       edx,100h
  00501cf0  0f8e32020000          jle       near 00501F28h
  00501cf6  e912020000            jmp       00501F0Dh
  00501cfb  6a00                  push      0
  00501cfd  6a00                  push      0
  00501cff  68bd000000            push      0BDh
  00501d04  56                    push      esi
  00501d05  e8d68df7ff            call      0047AAE0h
  00501d0a  83c410                add       esp,10h
  00501d0d  e82e540400            call      00547140h
  00501d12  99                    cdq
  00501d13  33c2                  xor       eax,edx
  00501d15  33c9                  xor       ecx,ecx
  00501d17  2bc2                  sub       eax,edx
  00501d19  23c5                  and       eax,ebp
  00501d1b  33c2                  xor       eax,edx
  00501d1d  2bc2                  sub       eax,edx
  00501d1f  668b0c45f8305500      mov       cx,[eax*2+5530F8h]
  00501d27  66894e06              mov       [esi+6],cx
  00501d2b  a1c4975e00            mov       eax,[5E97C4h]
  00501d30  8bd1                  mov       edx,ecx
  00501d32  81e1ff000000          and       ecx,0FFh
  00501d38  8b809c000000          mov       eax,[eax+9Ch]
  00501d3e  c1fa08                sar       edx,8
  00501d41  81e2ff000000          and       edx,0FFh
  00501d47  8d0c49                lea       ecx,[ecx+ecx*2]
  00501d4a  c1e104                shl       ecx,4
  00501d4d  8b1490                mov       edx,[eax+edx*4]
  00501d50  8a440a13              mov       al,[edx+ecx+13h]
  00501d54  66ff4e10              dec       word [esi+10h]
  00501d58  884643                mov       [esi+43h],al
  00501d5b  c7465c00000000        mov       dword [esi+5Ch],0
  00501d62  e9c5010000            jmp       00501F2Ch

LAB_00501d67:
  00501d67  a801                  test      al,1
  00501d69  0f85bd010000          jne       near 00501F2Ch
  00501d6f  33c9                  xor       ecx,ecx
  00501d71  8a4e42                mov       cl,[esi+42h]
  00501d74  83f903                cmp       ecx,3
  00501d77  0f87af010000          ja        near 00501F2Ch
  00501d7d  ff248df8215000        jmp       dword [ecx*4+5021F8h]
  00501d84  33c9                  xor       ecx,ecx
  00501d86  66394e18              cmp       [esi+18h],cx
  00501d8a  740a                  je        short 00501D96h
  00501d8c  0d00000001            or        eax,1000000h
  00501d91  894e18                mov       [esi+18h],ecx
  00501d94  8906                  mov       [esi],eax

LAB_00501d96:
  00501d96  817c240c2c010000      cmp       dword [esp+0Ch],12Ch
  00501d9e  7d09                  jge       short 00501DA9h
  00501da0  c6464300              mov       byte [esi+43h],0
  00501da4  e983010000            jmp       00501F2Ch

LAB_00501da9:
  00501da9  8a4643                mov       al,[esi+43h]
  00501dac  84c0                  test      al,al
  00501dae  7405                  je        short 00501DB5h
  00501db0  fec8                  dec       al
  00501db2  884643                mov       [esi+43h],al

LAB_00501db5:
  00501db5  894e5c                mov       [esi+5Ch],ecx
  00501db8  e96f010000            jmp       00501F2Ch
  00501dbd  66396e18              cmp       [esi+18h],bp
  00501dc1  740a                  je        short 00501DCDh
  00501dc3  0d00000001            or        eax,1000000h
  00501dc8  896e18                mov       [esi+18h],ebp
  00501dcb  8906                  mov       [esi],eax

LAB_00501dcd:
  00501dcd  8b44242c              mov       eax,[esp+2Ch]
  00501dd1  8d8800010000          lea       ecx,[eax+100h]
  00501dd7  81e1ff0f0000          and       ecx,0FFFh
  00501ddd  81f900020000          cmp       ecx,200h
  00501de3  7f14                  jg        short 00501DF9h
  00501de5  66014632              add       [esi+32h],ax
  00501de9  c6464300              mov       byte [esi+43h],0
  00501ded  c7465c00000000        mov       dword [esi+5Ch],0
  00501df4  e933010000            jmp       00501F2Ch

LAB_00501df9:
  00501df9  25ff0f0000            and       eax,0FFFh
  00501dfe  33d2                  xor       edx,edx
  00501e00  3d00080000            cmp       eax,800h
  00501e05  0f9dc2                setge     dl
  00501e08  4a                    dec       edx
  00501e09  81e200020000          and       edx,200h
  00501e0f  81c200ffffff          add       edx,0FFFFFF00h
  00501e15  8bc2                  mov       eax,edx
  00501e17  66014632              add       [esi+32h],ax

LAB_00501e1b:
  00501e1b  c7465c00000000        mov       dword [esi+5Ch],0
  00501e22  e905010000            jmp       00501F2Ch
  00501e27  66396e18              cmp       [esi+18h],bp
  00501e2b  740a                  je        short 00501E37h
  00501e2d  0d00000001            or        eax,1000000h
  00501e32  896e18                mov       [esi+18h],ebp
  00501e35  8906                  mov       [esi],eax

LAB_00501e37:
  00501e37  0fbf4610              movsx     eax,word [esi+10h]
  00501e3b  3944240c              cmp       [esp+0Ch],eax
  00501e3f  7d09                  jge       short 00501E4Ah
  00501e41  c6464300              mov       byte [esi+43h],0
  00501e45  e9e2000000            jmp       00501F2Ch

LAB_00501e4a:
  00501e4a  8a4643                mov       al,[esi+43h]
  00501e4d  84c0                  test      al,al
  00501e4f  7405                  je        short 00501E56h
  00501e51  fec8                  dec       al
  00501e53  884643                mov       [esi+43h],al

LAB_00501e56:
  00501e56  8b44242c              mov       eax,[esp+2Ch]
  00501e5a  8d8880000000          lea       ecx,[eax+80h]
  00501e60  81e1ff0f0000          and       ecx,0FFFh
  00501e66  81f900010000          cmp       ecx,100h
  00501e6c  7e1b                  jle       short 00501E89h
  00501e6e  25ff0f0000            and       eax,0FFFh
  00501e73  33d2                  xor       edx,edx
  00501e75  3d00080000            cmp       eax,800h
  00501e7a  0f9dc2                setge     dl
  00501e7d  4a                    dec       edx
  00501e7e  81e200010000          and       edx,100h
  00501e84  83c280                add       edx,0FFFFFF80h
  00501e87  8bc2                  mov       eax,edx

LAB_00501e89:
  00501e89  66014632              add       [esi+32h],ax
  00501e8d  c7465c00400100        mov       dword [esi+5Ch],14000h
  00501e94  e993000000            jmp       00501F2Ch
  00501e99  8a4e43                mov       cl,[esi+43h]
  00501e9c  84c9                  test      cl,cl
  00501e9e  7405                  je        short 00501EA5h
  00501ea0  fec9                  dec       cl
  00501ea2  884e43                mov       [esi+43h],cl

LAB_00501ea5:
  00501ea5  66396e18              cmp       [esi+18h],bp
  00501ea9  740a                  je        short 00501EB5h
  00501eab  0d00000001            or        eax,1000000h
  00501eb0  896e18                mov       [esi+18h],ebp
  00501eb3  8906                  mov       [esi],eax

LAB_00501eb5:
  00501eb5  0fbf4610              movsx     eax,word [esi+10h]
  00501eb9  50                    push      eax
  00501eba  c7465c00000100        mov       dword [esi+5Ch],10000h
  00501ec1  e8563b0400            call      00545A1Ch
  00501ec6  0faf465c              imul      eax,[esi+5Ch]
  00501eca  0fbf4e10              movsx     ecx,word [esi+10h]
  00501ece  8b7e1c                mov       edi,[esi+1Ch]
  00501ed1  51                    push      ecx
  00501ed2  c1f80c                sar       eax,0Ch
  00501ed5  03f8                  add       edi,eax
  00501ed7  897e1c                mov       [esi+1Ch],edi
  00501eda  e8373b0400            call      00545A16h
  00501edf  0faf465c              imul      eax,[esi+5Ch]
  00501ee3  8b6e24                mov       ebp,[esi+24h]
  00501ee6  c7465c00000000        mov       dword [esi+5Ch],0
  00501eed  c1f80c                sar       eax,0Ch
  00501ef0  03e8                  add       ebp,eax
  00501ef2  8b44242c              mov       eax,[esp+2Ch]
  00501ef6  896e24                mov       [esi+24h],ebp
  00501ef9  8d9080000000          lea       edx,[eax+80h]
  00501eff  81e2ff0f0000          and       edx,0FFFh
  00501f05  81fa00010000          cmp       edx,100h
  00501f0b  7e1b                  jle       short 00501F28h

LAB_00501f0d:
  00501f0d  25ff0f0000            and       eax,0FFFh
  00501f12  33c9                  xor       ecx,ecx
  00501f14  3d00080000            cmp       eax,800h
  00501f19  0f9dc1                setge     cl
  00501f1c  49                    dec       ecx
  00501f1d  81e100010000          and       ecx,100h
  00501f23  83c180                add       ecx,0FFFFFF80h

LAB_00501f26:
  00501f26  8bc1                  mov       eax,ecx

LAB_00501f28:
  00501f28  66014632              add       [esi+32h],ax

LAB_00501f2c:
  00501f2c  8b542410              mov       edx,[esp+10h]
  00501f30  66c7869a0000000060    mov       word [esi+9Ah],6000h
  00501f39  52                    push      edx
  00501f3a  56                    push      esi
  00501f3b  e82062f8ff            call      00488160h
  00501f40  83c408                add       esp,8
  00501f43  56                    push      esi
  00501f44  e8f7190300            call      00533940h
  00501f49  83c404                add       esp,4
  00501f4c  56                    push      esi
  00501f4d  e89edbfdff            call      004DFAF0h
  00501f52  8b44241c              mov       eax,[esp+1Ch]
  00501f56  83c404                add       esp,4
  00501f59  50                    push      eax
  00501f5a  56                    push      esi
  00501f5b  e81018feff            call      004E3770h
  00501f60  8b442418              mov       eax,[esp+18h]
  00501f64  83c408                add       esp,8
  00501f67  85c0                  test      eax,eax
  00501f69  0f8460020000          je        near 005021CFh
  00501f6f  f70617080000          test      dword [esi],817h
  00501f75  0f8554020000          jne       near 005021CFh
  00501f7b  8a4643                mov       al,[esi+43h]
  00501f7e  84c0                  test      al,al
  00501f80  0f8549020000          jne       near 005021CFh
  00501f86  8b466c                mov       eax,[esi+6Ch]
  00501f89  8b4e1c                mov       ecx,[esi+1Ch]
  00501f8c  8b5624                mov       edx,[esi+24h]
  00501f8f  8b781c                mov       edi,[eax+1Ch]
  00501f92  8b6824                mov       ebp,[eax+24h]
  00501f95  2bf9                  sub       edi,ecx
  00501f97  2bea                  sub       ebp,edx
  00501f99  c1ff0c                sar       edi,0Ch
  00501f9c  c1fd0c                sar       ebp,0Ch
  00501f9f  8bcf                  mov       ecx,edi
  00501fa1  8bd5                  mov       edx,ebp
  00501fa3  0fafcf                imul      ecx,edi
  00501fa6  0fafd5                imul      edx,ebp
  00501fa9  03ca                  add       ecx,edx
  00501fab  51                    push      ecx
  00501fac  e85f3a0400            call      00545A10h
  00501fb1  55                    push      ebp
  00501fb2  57                    push      edi
  00501fb3  89442414              mov       [esp+14h],eax
  00501fb7  e8663a0400            call      00545A22h
  00501fbc  668b4e32              mov       cx,[esi+32h]
  00501fc0  2bc1                  sub       eax,ecx
  00501fc2  25ff0f0000            and       eax,0FFFh
  00501fc7  8944242c              mov       [esp+2Ch],eax
  00501fcb  e870510400            call      00547140h
  00501fd0  99                    cdq
  00501fd1  b964000000            mov       ecx,64h
  00501fd6  f7f9                  idiv      ecx
  00501fd8  817c240c2c010000      cmp       dword [esp+0Ch],12Ch
  00501fe0  8bc2                  mov       eax,edx
  00501fe2  7d71                  jge       short 00502055h
  00501fe4  83f832                cmp       eax,32h
  00501fe7  7d3f                  jge       short 00502028h
  00501fe9  8b54242c              mov       edx,[esp+2Ch]
  00501fed  81c200010000          add       edx,100h
  00501ff3  81e2ff0f0000          and       edx,0FFFh
  00501ff9  81fa00020000          cmp       edx,200h
  00501fff  0f8f60010000          jg        near 00502165h
  00502005  8b06                  mov       eax,[esi]
  00502007  c7461803000000        mov       dword [esi+18h],3
  0050200e  25fffffffe            and       eax,0FEFFFFFFh
  00502013  c6464200              mov       byte [esi+42h],0
  00502017  0c02                  or        al,2
  00502019  66c746100200          mov       word [esi+10h],2
  0050201f  8906                  mov       [esi],eax
  00502021  5f                    pop       edi
  00502022  5e                    pop       esi
  00502023  5d                    pop       ebp
  00502024  83c41c                add       esp,1Ch
  00502027  c3                    ret

LAB_00502028:
  00502028  83f84b                cmp       eax,4Bh
  0050202b  0f8d48010000          jge       near 00502179h
  00502031  c6464202              mov       byte [esi+42h],2
  00502035  e806510400            call      00547140h
  0050203a  99                    cdq
  0050203b  b90a000000            mov       ecx,0Ah
  00502040  66c74610c800          mov       word [esi+10h],0C8h
  00502046  f7f9                  idiv      ecx
  00502048  80c214                add       dl,14h
  0050204b  885643                mov       [esi+43h],dl
  0050204e  5f                    pop       edi
  0050204f  5e                    pop       esi
  00502050  5d                    pop       ebp
  00502051  83c41c                add       esp,1Ch
  00502054  c3                    ret

LAB_00502055:
  00502055  83f828                cmp       eax,28h
  00502058  0f8da4000000          jge       near 00502102h
  0050205e  8b54242c              mov       edx,[esp+2Ch]
  00502062  81c200020000          add       edx,200h
  00502068  81e2ff0f0000          and       edx,0FFFh
  0050206e  81fa00040000          cmp       edx,400h
  00502074  0f8deb000000          jge       near 00502165h
  0050207a  99                    cdq
  0050207b  b903000000            mov       ecx,3
  00502080  f7f9                  idiv      ecx
  00502082  83ea00                sub       edx,0
  00502085  7451                  je        short 005020D8h
  00502087  4a                    dec       edx
  00502088  742b                  je        short 005020B5h
  0050208a  4a                    dec       edx
  0050208b  0f853e010000          jne       near 005021CFh
  00502091  8b16                  mov       edx,[esi]
  00502093  c7461807000000        mov       dword [esi+18h],7
  0050209a  81e2fffffffe          and       edx,0FEFFFFFFh
  005020a0  884e42                mov       [esi+42h],cl
  005020a3  83ca02                or        edx,2
  005020a6  66c746100100          mov       word [esi+10h],1
  005020ac  8916                  mov       [esi],edx
  005020ae  5f                    pop       edi
  005020af  5e                    pop       esi
  005020b0  5d                    pop       ebp
  005020b1  83c41c                add       esp,1Ch
  005020b4  c3                    ret

LAB_005020b5:
  005020b5  8b06                  mov       eax,[esi]
  005020b7  c7461805000000        mov       dword [esi+18h],5
  005020be  25fffffffe            and       eax,0FEFFFFFFh
  005020c3  c6464202              mov       byte [esi+42h],2
  005020c7  0c02                  or        al,2
  005020c9  66c746100100          mov       word [esi+10h],1
  005020cf  8906                  mov       [esi],eax
  005020d1  5f                    pop       edi
  005020d2  5e                    pop       esi
  005020d3  5d                    pop       ebp
  005020d4  83c41c                add       esp,1Ch
  005020d7  c3                    ret

LAB_005020d8:
  005020d8  8b0e                  mov       ecx,[esi]
  005020da  c7461809000000        mov       dword [esi+18h],9
  005020e1  81e1fffffffe          and       ecx,0FEFFFFFFh
  005020e7  c6464201              mov       byte [esi+42h],1
  005020eb  83c902                or        ecx,2
  005020ee  c6464302              mov       byte [esi+43h],2
  005020f2  890e                  mov       [esi],ecx
  005020f4  c7465c00000000        mov       dword [esi+5Ch],0
  005020fb  5f                    pop       edi
  005020fc  5e                    pop       esi
  005020fd  5d                    pop       ebp
  005020fe  83c41c                add       esp,1Ch
  00502101  c3                    ret

LAB_00502102:
  00502102  83f83c                cmp       eax,3Ch
  00502105  7d24                  jge       short 0050212Bh
  00502107  c6464202              mov       byte [esi+42h],2
  0050210b  e830500400            call      00547140h
  00502110  99                    cdq
  00502111  b90a000000            mov       ecx,0Ah
  00502116  66c74610c800          mov       word [esi+10h],0C8h
  0050211c  f7f9                  idiv      ecx
  0050211e  80c214                add       dl,14h
  00502121  885643                mov       [esi+43h],dl
  00502124  5f                    pop       edi
  00502125  5e                    pop       esi
  00502126  5d                    pop       ebp
  00502127  83c41c                add       esp,1Ch
  0050212a  c3                    ret

LAB_0050212b:
  0050212b  83f850                cmp       eax,50h
  0050212e  7d44                  jge       short 00502174h
  00502130  8b54242c              mov       edx,[esp+2Ch]
  00502134  81c200020000          add       edx,200h
  0050213a  81e2ff0f0000          and       edx,0FFFh
  00502140  81fa00040000          cmp       edx,400h
  00502146  7d1d                  jge       short 00502165h
  00502148  8b06                  mov       eax,[esi]
  0050214a  c7461806000000        mov       dword [esi+18h],6
  00502151  0d04080001            or        eax,1000804h
  00502156  66c746100300          mov       word [esi+10h],3
  0050215c  8906                  mov       [esi],eax
  0050215e  5f                    pop       edi
  0050215f  5e                    pop       esi
  00502160  5d                    pop       ebp
  00502161  83c41c                add       esp,1Ch
  00502164  c3                    ret

LAB_00502165:
  00502165  c6464201              mov       byte [esi+42h],1
  00502169  c6464301              mov       byte [esi+43h],1
  0050216d  5f                    pop       edi
  0050216e  5e                    pop       esi
  0050216f  5d                    pop       ebp
  00502170  83c41c                add       esp,1Ch
  00502173  c3                    ret

LAB_00502174:
  00502174  83f85a                cmp       eax,5Ah
  00502177  7d3f                  jge       short 005021B8h

LAB_00502179:
  00502179  c6464203              mov       byte [esi+42h],3
  0050217d  e8be4f0400            call      00547140h
  00502182  99                    cdq
  00502183  b90a000000            mov       ecx,0Ah
  00502188  f7f9                  idiv      ecx
  0050218a  80c214                add       dl,14h
  0050218d  885643                mov       [esi+43h],dl
  00502190  e8ab4f0400            call      00547140h
  00502195  99                    cdq
  00502196  b9000c0000            mov       ecx,0C00h
  0050219b  8b7c242c              mov       edi,[esp+2Ch]
  0050219f  f7f9                  idiv      ecx
  005021a1  03d7                  add       edx,edi
  005021a3  66035632              add       dx,[esi+32h]
  005021a7  81c200020000          add       edx,200h
  005021ad  66895610              mov       [esi+10h],dx
  005021b1  5f                    pop       edi
  005021b2  5e                    pop       esi
  005021b3  5d                    pop       ebp
  005021b4  83c41c                add       esp,1Ch
  005021b7  c3                    ret

LAB_005021b8:
  005021b8  c6464200              mov       byte [esi+42h],0
  005021bc  e87f4f0400            call      00547140h
  005021c1  99                    cdq
  005021c2  b90a000000            mov       ecx,0Ah
  005021c7  f7f9                  idiv      ecx
  005021c9  80c214                add       dl,14h
  005021cc  885643                mov       [esi+43h],dl

LAB_005021cf:
  005021cf  5f                    pop       edi
  005021d0  5e                    pop       esi
  005021d1  5d                    pop       ebp
  005021d2  83c41c                add       esp,1Ch
  005021d5  c3                    ret
  005021d6  8bff                  mov       edi,edi
  005021d8  d6                    salc
  005021d9  185000                sbb       [eax],dl
  005021dc  231a                  and       ebx,[edx]
  005021de  50                    push      eax
  005021df  00c0                  add       al,al
  005021e1  1a5000                sbb       dl,[eax]
  005021e4  6e                    outsb
  005021e5  1b5000                sbb       edx,[eax]
  005021e8  641c50                sbb       al,50h
  005021eb  007b1c                add       [ebx+1Ch],bh
  005021ee  50                    push      eax
  005021ef  00cb                  add       bl,cl
  005021f1  1c50                  sbb       al,50h
  005021f3  00fb                  add       bl,bh
  005021f5  1c50                  sbb       al,50h
  005021f7  00841d5000bd1d        add       [ebp+ebx+1DBD0050h],al
  005021fe  50                    push      eax
  005021ff  0027                  add       [edi],ah
  00502201  1e                    push      ds
  00502202  50                    push      eax
  00502203  00991e500090          add       [ecx-6FFFAFE2h],bl
  00502209  90                    nop
  0050220a  90                    nop
  0050220b  90                    nop
  0050220c  90                    nop
  0050220d  90                    nop
  0050220e  90                    nop
  0050220f  90                    nop

44 cible(s) de saut interne : 0x005018c1, 0x00501909, 0x00501940, 0x00501996, 0x005019ab, 0x005019b5, 0x00501a0f, 0x00501a80, 0x00501a9f, 0x00501ab2, 0x00501b9a, 0x00501c07, 0x00501c25, 0x00501c4d, 0x00501c92, 0x00501cda, 0x00501d67, 0x00501d96, 0x00501da9, 0x00501db5, 0x00501dcd, 0x00501df9, 0x00501e1b, 0x00501e37, 0x00501e4a, 0x00501e56, 0x00501e89, 0x00501ea5, 0x00501eb5, 0x00501f0d, 0x00501f26, 0x00501f28, 0x00501f2c, 0x00502028, 0x00502055, 0x005020b5, 0x005020d8, 0x00502102, 0x0050212b, 0x00502165, 0x00502174, 0x00502179, 0x005021b8, 0x005021cf
