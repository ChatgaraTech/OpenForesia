Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004b95e0  section .text  offset fichier 0xb89e0  2864 octets

  004b95e0  81ec84000000          sub       esp,84h
  004b95e6  53                    push      ebx
  004b95e7  55                    push      ebp
  004b95e8  56                    push      esi
  004b95e9  8bb42494000000        mov       esi,[esp+94h]
  004b95f0  33db                  xor       ebx,ebx
  004b95f2  57                    push      edi
  004b95f3  8a4641                mov       al,[esi+41h]
  004b95f6  66895c2430            mov       [esp+30h],bx
  004b95fb  3ac3                  cmp       al,bl
  004b95fd  66c74424321e00        mov       word [esp+32h],1Eh
  004b9604  66c74424343200        mov       word [esp+34h],32h
  004b960b  66c74424366400        mov       word [esp+36h],64h
  004b9612  66895c2438            mov       [esp+38h],bx
  004b9617  66c744243a5000        mov       word [esp+3Ah],50h
  004b961e  750f                  jne       short 004B962Fh
  004b9620  53                    push      ebx
  004b9621  53                    push      ebx
  004b9622  56                    push      esi
  004b9623  c6464101              mov       byte [esi+41h],1
  004b9627  e8f4fbffff            call      004B9220h
  004b962c  83c40c                add       esp,0Ch

LAB_004b962f:
  004b962f  8b461c                mov       eax,[esi+1Ch]
  004b9632  8b4e20                mov       ecx,[esi+20h]
  004b9635  8b5624                mov       edx,[esi+24h]
  004b9638  8d6e1c                lea       ebp,[esi+1Ch]
  004b963b  89442424              mov       [esp+24h],eax
  004b963f  8b4664                mov       eax,[esi+64h]
  004b9642  894c2428              mov       [esp+28h],ecx
  004b9646  8b4e60                mov       ecx,[esi+60h]
  004b9649  8954242c              mov       [esp+2Ch],edx
  004b964d  668b5632              mov       dx,[esi+32h]
  004b9651  89442440              mov       [esp+40h],eax
  004b9655  8b467c                mov       eax,[esi+7Ch]
  004b9658  894c2448              mov       [esp+48h],ecx
  004b965c  668b8e90000000        mov       cx,[esi+90h]
  004b9663  6689942482000000      mov       [esp+82h],dx
  004b966b  8b9680000000          mov       edx,[esi+80h]
  004b9671  89442454              mov       [esp+54h],eax
  004b9675  8b8684000000          mov       eax,[esi+84h]
  004b967b  66894c2458            mov       [esp+58h],cx
  004b9680  8b8e88000000          mov       ecx,[esi+88h]
  004b9686  89542444              mov       [esp+44h],edx
  004b968a  8b968c000000          mov       edx,[esi+8Ch]
  004b9690  89442450              mov       [esp+50h],eax
  004b9694  894c243c              mov       [esp+3Ch],ecx
  004b9698  8d44245c              lea       eax,[esp+5Ch]
  004b969c  8954244c              mov       [esp+4Ch],edx
  004b96a0  8d4c2460              lea       ecx,[esp+60h]
  004b96a4  50                    push      eax
  004b96a5  8d542468              lea       edx,[esp+68h]
  004b96a9  51                    push      ecx
  004b96aa  52                    push      edx
  004b96ab  56                    push      esi
  004b96ac  e8cff20700            call      00538980h
  004b96b1  83c410                add       esp,10h
  004b96b4  8d442468              lea       eax,[esp+68h]
  004b96b8  8d4c2420              lea       ecx,[esp+20h]
  004b96bc  8d54241c              lea       edx,[esp+1Ch]
  004b96c0  50                    push      eax
  004b96c1  51                    push      ecx
  004b96c2  8d44241c              lea       eax,[esp+1Ch]
  004b96c6  52                    push      edx
  004b96c7  8d4c2424              lea       ecx,[esp+24h]
  004b96cb  50                    push      eax
  004b96cc  51                    push      ecx
  004b96cd  56                    push      esi
  004b96ce  e84dc80700            call      00535F20h
  004b96d3  8b7c2430              mov       edi,[esp+30h]
  004b96d7  8b4c2438              mov       ecx,[esp+38h]
  004b96db  b840000000            mov       eax,40h
  004b96e0  83c418                add       esp,18h
  004b96e3  2bc7                  sub       eax,edi
  004b96e5  3bcb                  cmp       ecx,ebx
  004b96e7  89442410              mov       [esp+10h],eax
  004b96eb  0f841c040000          je        near 004B9B0Dh
  004b96f1  8b06                  mov       eax,[esi]
  004b96f3  668b5618              mov       dx,[esi+18h]
  004b96f7  33db                  xor       ebx,ebx
  004b96f9  25fffffffd            and       eax,0FDFFFFFFh
  004b96fe  8a5e04                mov       bl,[esi+4]
  004b9701  8906                  mov       [esi],eax
  004b9703  0fbfd2                movsx     edx,dx
  004b9706  8b1c9d40965e00        mov       ebx,[ebx*4+5E9640h]
  004b970d  a900000001            test      eax,1000000h
  004b9712  8b1493                mov       edx,[ebx+edx*4]
  004b9715  741d                  je        short 004B9734h
  004b9717  66ff461a              inc       word [esi+1Ah]
  004b971b  668b461a              mov       ax,[esi+1Ah]
  004b971f  33db                  xor       ebx,ebx
  004b9721  668b5a16              mov       bx,[edx+16h]
  004b9725  0fbfc0                movsx     eax,ax
  004b9728  3bc3                  cmp       eax,ebx
  004b972a  7c2a                  jl        short 004B9756h
  004b972c  66c7461a0000          mov       word [esi+1Ah],0
  004b9732  eb22                  jmp       short 004B9756h

LAB_004b9734:
  004b9734  66ff461a              inc       word [esi+1Ah]
  004b9738  33db                  xor       ebx,ebx
  004b973a  668b5a16              mov       bx,[edx+16h]
  004b973e  0fbf561a              movsx     edx,word [esi+1Ah]
  004b9742  3bd3                  cmp       edx,ebx
  004b9744  7c10                  jl        short 004B9756h
  004b9746  668b561a              mov       dx,[esi+1Ah]
  004b974a  4a                    dec       edx
  004b974b  0d00000002            or        eax,2000000h
  004b9750  6689561a              mov       [esi+1Ah],dx
  004b9754  8906                  mov       [esi],eax

LAB_004b9756:
  004b9756  8b06                  mov       eax,[esi]
  004b9758  a802                  test      al,2
  004b975a  0f848f000000          je        near 004B97EFh
  004b9760  66837e1a0e            cmp       word [esi+1Ah],0Eh
  004b9765  7404                  je        short 004B976Bh
  004b9767  a801                  test      al,1
  004b9769  745b                  je        short 004B97C6h

LAB_004b976b:
  004b976b  8d442430              lea       eax,[esp+30h]
  004b976f  6a00                  push      0
  004b9771  50                    push      eax
  004b9772  55                    push      ebp
  004b9773  56                    push      esi
  004b9774  e8b7040600            call      00519C30h
  004b9779  8bf8                  mov       edi,eax
  004b977b  a1b8e05d00            mov       eax,[5DE0B8h]
  004b9780  83c410                add       esp,10h
  004b9783  85c0                  test      eax,eax
  004b9785  743f                  je        short 004B97C6h
  004b9787  8b1f                  mov       ebx,[edi]
  004b9789  57                    push      edi
  004b978a  83cb40                or        ebx,40h
  004b978d  56                    push      esi
  004b978e  66c787960000001000    mov       word [edi+96h],10h
  004b9797  891f                  mov       [edi],ebx
  004b9799  e8e2030800            call      00539B80h
  004b979e  83c408                add       esp,8
  004b97a1  83c71c                add       edi,1Ch
  004b97a4  57                    push      edi
  004b97a5  6800180000            push      1800h
  004b97aa  6800030000            push      300h
  004b97af  6a03                  push      3
  004b97b1  e86a430800            call      0053DB20h
  004b97b6  83c410                add       esp,10h
  004b97b9  66837e1663            cmp       word [esi+16h],63h
  004b97be  7506                  jne       short 004B97C6h
  004b97c0  66c746160000          mov       word [esi+16h],0

LAB_004b97c6:
  004b97c6  8b06                  mov       eax,[esi]
  004b97c8  a801                  test      al,1
  004b97ca  0f853d030000          jne       near 004B9B0Dh
  004b97d0  a900000002            test      eax,2000000h
  004b97d5  0f84ba000000          je        near 004B9895h
  004b97db  24fd                  and       al,0FDh
  004b97dd  c6464300              mov       byte [esi+43h],0
  004b97e1  8906                  mov       [esi],eax
  004b97e3  c7465c00000000        mov       dword [esi+5Ch],0
  004b97ea  e91e030000            jmp       004B9B0Dh

LAB_004b97ef:
  004b97ef  bb01000000            mov       ebx,1
  004b97f4  84c3                  test      bl,al
  004b97f6  0f8511030000          jne       near 004B9B0Dh
  004b97fc  33d2                  xor       edx,edx
  004b97fe  8a5642                mov       dl,[esi+42h]
  004b9801  83fa07                cmp       edx,7
  004b9804  0f8703030000          ja        near 004B9B0Dh
  004b980a  ff2495eca04b00        jmp       dword [edx*4+4BA0ECh]
  004b9811  817c24142c010000      cmp       dword [esp+14h],12Ch
  004b9819  7d09                  jge       short 004B9824h
  004b981b  c6464300              mov       byte [esi+43h],0
  004b981f  e9e9020000            jmp       004B9B0Dh

LAB_004b9824:
  004b9824  8a4643                mov       al,[esi+43h]
  004b9827  84c0                  test      al,al
  004b9829  746a                  je        short 004B9895h
  004b982b  fec8                  dec       al
  004b982d  c7465c00000000        mov       dword [esi+5Ch],0
  004b9834  884643                mov       [esi+43h],al
  004b9837  e9d1020000            jmp       004B9B0Dh
  004b983c  8b4c241c              mov       ecx,[esp+1Ch]
  004b9840  8d91c8000000          lea       edx,[ecx+0C8h]
  004b9846  81e2ff0f0000          and       edx,0FFFh
  004b984c  81fa90010000          cmp       edx,190h
  004b9852  7f10                  jg        short 004B9864h
  004b9854  c6464300              mov       byte [esi+43h],0
  004b9858  c7465c00000000        mov       dword [esi+5Ch],0
  004b985f  e9a9020000            jmp       004B9B0Dh

LAB_004b9864:
  004b9864  81e1ff0f0000          and       ecx,0FFFh
  004b986a  81f900080000          cmp       ecx,800h
  004b9870  7d08                  jge       short 004B987Ah
  004b9872  8d8f80000000          lea       ecx,[edi+80h]
  004b9878  eb07                  jmp       short 004B9881h

LAB_004b987a:
  004b987a  b980ffffff            mov       ecx,0FFFFFF80h
  004b987f  2bcf                  sub       ecx,edi

LAB_004b9881:
  004b9881  66014e32              add       [esi+32h],cx
  004b9885  66395e18              cmp       [esi+18h],bx
  004b9889  740a                  je        short 004B9895h
  004b988b  0d00000001            or        eax,1000000h
  004b9890  895e18                mov       [esi+18h],ebx
  004b9893  8906                  mov       [esi],eax

LAB_004b9895:
  004b9895  c7465c00000000        mov       dword [esi+5Ch],0
  004b989c  e96c020000            jmp       004B9B0Dh
  004b98a1  0fbf4e10              movsx     ecx,word [esi+10h]
  004b98a5  394c2414              cmp       [esp+14h],ecx
  004b98a9  7d09                  jge       short 004B98B4h
  004b98ab  c6464300              mov       byte [esi+43h],0
  004b98af  e959020000            jmp       004B9B0Dh

LAB_004b98b4:
  004b98b4  8a4e43                mov       cl,[esi+43h]
  004b98b7  84c9                  test      cl,cl
  004b98b9  7405                  je        short 004B98C0h
  004b98bb  fec9                  dec       cl
  004b98bd  884e43                mov       [esi+43h],cl

LAB_004b98c0:
  004b98c0  66395e18              cmp       [esi+18h],bx
  004b98c4  740b                  je        short 004B98D1h
  004b98c6  0d00000001            or        eax,1000000h
  004b98cb  66895e18              mov       [esi+18h],bx
  004b98cf  8906                  mov       [esi],eax

LAB_004b98d1:
  004b98d1  8b44241c              mov       eax,[esp+1Ch]
  004b98d5  8d9000010000          lea       edx,[eax+100h]
  004b98db  81e2ff0f0000          and       edx,0FFFh
  004b98e1  81fa00020000          cmp       edx,200h
  004b98e7  7e2f                  jle       short 004B9918h
  004b98e9  25ff0f0000            and       eax,0FFFh
  004b98ee  3d00080000            cmp       eax,800h
  004b98f3  7d18                  jge       short 004B990Dh
  004b98f5  8d8780000000          lea       eax,[edi+80h]
  004b98fb  81c700030000          add       edi,300h
  004b9901  66014632              add       [esi+32h],ax
  004b9905  c1e705                shl       edi,5
  004b9908  e9fd010000            jmp       004B9B0Ah

LAB_004b990d:
  004b990d  b880ffffff            mov       eax,0FFFFFF80h
  004b9912  2bc7                  sub       eax,edi
  004b9914  66014632              add       [esi+32h],ax

LAB_004b9918:
  004b9918  81c700030000          add       edi,300h
  004b991e  c1e705                shl       edi,5
  004b9921  e9e4010000            jmp       004B9B0Ah
  004b9926  8a4e43                mov       cl,[esi+43h]
  004b9929  84c9                  test      cl,cl
  004b992b  7405                  je        short 004B9932h
  004b992d  fec9                  dec       cl
  004b992f  884e43                mov       [esi+43h],cl

LAB_004b9932:
  004b9932  66395e18              cmp       [esi+18h],bx
  004b9936  740a                  je        short 004B9942h
  004b9938  0d00000001            or        eax,1000000h
  004b993d  895e18                mov       [esi+18h],ebx
  004b9940  8906                  mov       [esi],eax

LAB_004b9942:
  004b9942  e8f9d70800            call      00547140h
  004b9947  99                    cdq
  004b9948  b964000000            mov       ecx,64h
  004b994d  f7f9                  idiv      ecx
  004b994f  8b442418              mov       eax,[esp+18h]
  004b9953  8bc8                  mov       ecx,eax
  004b9955  c1f903                sar       ecx,3
  004b9958  3bd1                  cmp       edx,ecx
  004b995a  7d5c                  jge       short 004B99B8h
  004b995c  8b7c2420              mov       edi,[esp+20h]
  004b9960  0500020000            add       eax,200h
  004b9965  c1e006                shl       eax,6
  004b9968  0fbf5610              movsx     edx,word [esi+10h]
  004b996c  0faff8                imul      edi,eax
  004b996f  52                    push      edx
  004b9970  89465c                mov       [esi+5Ch],eax
  004b9973  c1ff0c                sar       edi,0Ch
  004b9976  e8a1c00800            call      00545A1Ch
  004b997b  0fafc7                imul      eax,edi
  004b997e  8b4e50                mov       ecx,[esi+50h]
  004b9981  c1f80c                sar       eax,0Ch
  004b9984  03c8                  add       ecx,eax
  004b9986  0fbf4610              movsx     eax,word [esi+10h]
  004b998a  50                    push      eax
  004b998b  894e50                mov       [esi+50h],ecx
  004b998e  e883c00800            call      00545A16h
  004b9993  0fafc7                imul      eax,edi
  004b9996  8b5658                mov       edx,[esi+58h]
  004b9999  8b4e54                mov       ecx,[esi+54h]
  004b999c  c1f80c                sar       eax,0Ch
  004b999f  03d0                  add       edx,eax
  004b99a1  8b06                  mov       eax,[esi]
  004b99a3  81c100a00000          add       ecx,0A000h
  004b99a9  0bc3                  or        eax,ebx
  004b99ab  895658                mov       [esi+58h],edx
  004b99ae  894e54                mov       [esi+54h],ecx
  004b99b1  8906                  mov       [esi],eax
  004b99b3  e955010000            jmp       004B9B0Dh

LAB_004b99b8:
  004b99b8  8b7c2420              mov       edi,[esp+20h]
  004b99bc  0500040000            add       eax,400h
  004b99c1  c1e004                shl       eax,4
  004b99c4  0fbf4e10              movsx     ecx,word [esi+10h]
  004b99c8  0faff8                imul      edi,eax
  004b99cb  51                    push      ecx
  004b99cc  89465c                mov       [esi+5Ch],eax
  004b99cf  c1ff0c                sar       edi,0Ch
  004b99d2  e845c00800            call      00545A1Ch
  004b99d7  0fafc7                imul      eax,edi
  004b99da  0fbf5610              movsx     edx,word [esi+10h]
  004b99de  8b4d00                mov       ecx,[ebp]
  004b99e1  52                    push      edx
  004b99e2  c1f80c                sar       eax,0Ch
  004b99e5  03c8                  add       ecx,eax
  004b99e7  894d00                mov       [ebp],ecx
  004b99ea  e827c00800            call      00545A16h
  004b99ef  0fafc7                imul      eax,edi
  004b99f2  8b5e24                mov       ebx,[esi+24h]
  004b99f5  c7465c00000000        mov       dword [esi+5Ch],0
  004b99fc  c1f80c                sar       eax,0Ch
  004b99ff  03d8                  add       ebx,eax
  004b9a01  8b44241c              mov       eax,[esp+1Ch]
  004b9a05  895e24                mov       [esi+24h],ebx
  004b9a08  8d8800010000          lea       ecx,[eax+100h]
  004b9a0e  81e1ff0f0000          and       ecx,0FFFh
  004b9a14  81f900020000          cmp       ecx,200h
  004b9a1a  0f8eed000000          jle       near 004B9B0Dh
  004b9a20  25ff0f0000            and       eax,0FFFh
  004b9a25  3d00080000            cmp       eax,800h
  004b9a2a  7d11                  jge       short 004B9A3Dh
  004b9a2c  8b542418              mov       edx,[esp+18h]
  004b9a30  8d441232              lea       eax,[edx+edx+32h]
  004b9a34  66014632              add       [esi+32h],ax
  004b9a38  e9d0000000            jmp       004B9B0Dh

LAB_004b9a3d:
  004b9a3d  8b442418              mov       eax,[esp+18h]
  004b9a41  8d0c00                lea       ecx,[eax+eax]
  004b9a44  b8ceffffff            mov       eax,0FFFFFFCEh
  004b9a49  2bc1                  sub       eax,ecx
  004b9a4b  66014632              add       [esi+32h],ax
  004b9a4f  e9b9000000            jmp       004B9B0Dh
  004b9a54  25fffffffe            and       eax,0FEFFFFFFh
  004b9a59  66c746180300          mov       word [esi+18h],3
  004b9a5f  a900000002            test      eax,2000000h
  004b9a64  8906                  mov       [esi],eax
  004b9a66  7404                  je        short 004B9A6Ch
  004b9a68  66895e12              mov       [esi+12h],bx

LAB_004b9a6c:
  004b9a6c  8d8700010000          lea       eax,[edi+100h]
  004b9a72  c1e006                shl       eax,6
  004b9a75  0fafc8                imul      ecx,eax
  004b9a78  0fbf5610              movsx     edx,word [esi+10h]
  004b9a7c  c1f90c                sar       ecx,0Ch
  004b9a7f  52                    push      edx
  004b9a80  89465c                mov       [esi+5Ch],eax
  004b9a83  8bf9                  mov       edi,ecx
  004b9a85  e892bf0800            call      00545A1Ch
  004b9a8a  0fafc7                imul      eax,edi
  004b9a8d  8b6e50                mov       ebp,[esi+50h]
  004b9a90  c1f80c                sar       eax,0Ch
  004b9a93  03e8                  add       ebp,eax
  004b9a95  0fbf4610              movsx     eax,word [esi+10h]
  004b9a99  50                    push      eax
  004b9a9a  896e50                mov       [esi+50h],ebp
  004b9a9d  e874bf0800            call      00545A16h
  004b9aa2  0fafc7                imul      eax,edi
  004b9aa5  8b5658                mov       edx,[esi+58h]
  004b9aa8  8b4e54                mov       ecx,[esi+54h]
  004b9aab  c1f80c                sar       eax,0Ch
  004b9aae  03d0                  add       edx,eax
  004b9ab0  8b06                  mov       eax,[esi]
  004b9ab2  81c100400100          add       ecx,14000h
  004b9ab8  0c03                  or        al,3
  004b9aba  895658                mov       [esi+58h],edx
  004b9abd  894e54                mov       [esi+54h],ecx
  004b9ac0  8906                  mov       [esi],eax
  004b9ac2  eb49                  jmp       short 004B9B0Dh
  004b9ac4  668b5612              mov       dx,[esi+12h]
  004b9ac8  663bd3                cmp       dx,bx
  004b9acb  751c                  jne       short 004B9AE9h
  004b9acd  0fbf4e10              movsx     ecx,word [esi+10h]
  004b9ad1  394c2414              cmp       [esp+14h],ecx
  004b9ad5  7d06                  jge       short 004B9ADDh
  004b9ad7  c6464300              mov       byte [esi+43h],0
  004b9adb  eb0c                  jmp       short 004B9AE9h

LAB_004b9add:
  004b9add  8a4e43                mov       cl,[esi+43h]
  004b9ae0  84c9                  test      cl,cl
  004b9ae2  7405                  je        short 004B9AE9h
  004b9ae4  fec9                  dec       cl
  004b9ae6  884e43                mov       [esi+43h],cl

LAB_004b9ae9:
  004b9ae9  6685d2                test      dx,dx
  004b9aec  751f                  jne       short 004B9B0Dh
  004b9aee  0d00000001            or        eax,1000000h
  004b9af3  66895e18              mov       [esi+18h],bx
  004b9af7  c1ff02                sar       edi,2
  004b9afa  6689561a              mov       [esi+1Ah],dx
  004b9afe  8906                  mov       [esi],eax
  004b9b00  66895e12              mov       [esi+12h],bx
  004b9b04  81c700400000          add       edi,4000h

LAB_004b9b0a:
  004b9b0a  897e5c                mov       [esi+5Ch],edi

LAB_004b9b0d:
  004b9b0d  0fbf5632              movsx     edx,word [esi+32h]
  004b9b11  52                    push      edx
  004b9b12  e805bf0800            call      00545A1Ch
  004b9b17  8944246c              mov       [esp+6Ch],eax
  004b9b1b  0fbf4632              movsx     eax,word [esi+32h]
  004b9b1f  50                    push      eax
  004b9b20  e8f1be0800            call      00545A16h
  004b9b25  8b5650                mov       edx,[esi+50h]
  004b9b28  8b4c2420              mov       ecx,[esp+20h]
  004b9b2c  0fbf9e9a000000        movsx     ebx,word [esi+9Ah]
  004b9b33  0faf4e5c              imul      ecx,[esi+5Ch]
  004b9b37  89442474              mov       [esp+74h],eax
  004b9b3b  8bc2                  mov       eax,edx
  004b9b3d  c1f808                sar       eax,8
  004b9b40  8bfa                  mov       edi,edx
  004b9b42  56                    push      esi
  004b9b43  2bf8                  sub       edi,eax
  004b9b45  8b4654                mov       eax,[esi+54h]
  004b9b48  c1fa03                sar       edx,3
  004b9b4b  2bfa                  sub       edi,edx
  004b9b4d  8be8                  mov       ebp,eax
  004b9b4f  8bd3                  mov       edx,ebx
  004b9b51  897e50                mov       [esi+50h],edi
  004b9b54  c1fd08                sar       ebp,8
  004b9b57  c1fa08                sar       edx,8
  004b9b5a  2bd5                  sub       edx,ebp
  004b9b5c  8b6e58                mov       ebp,[esi+58h]
  004b9b5f  03d0                  add       edx,eax
  004b9b61  8bc5                  mov       eax,ebp
  004b9b63  2bd3                  sub       edx,ebx
  004b9b65  8bdd                  mov       ebx,ebp
  004b9b67  c1fb03                sar       ebx,3
  004b9b6a  c1f90c                sar       ecx,0Ch
  004b9b6d  2bc3                  sub       eax,ebx
  004b9b6f  8bd9                  mov       ebx,ecx
  004b9b71  0faf5c2470            imul      ebx,[esp+70h]
  004b9b76  0faf4c2478            imul      ecx,[esp+78h]
  004b9b7b  c1fd08                sar       ebp,8
  004b9b7e  c1fb0c                sar       ebx,0Ch
  004b9b81  2bc5                  sub       eax,ebp
  004b9b83  8b6e1c                mov       ebp,[esi+1Ch]
  004b9b86  03df                  add       ebx,edi
  004b9b88  894658                mov       [esi+58h],eax
  004b9b8b  c1f90c                sar       ecx,0Ch
  004b9b8e  03eb                  add       ebp,ebx
  004b9b90  8b5e20                mov       ebx,[esi+20h]
  004b9b93  03c8                  add       ecx,eax
  004b9b95  8b4624                mov       eax,[esi+24h]
  004b9b98  03da                  add       ebx,edx
  004b9b9a  03c1                  add       eax,ecx
  004b9b9c  895654                mov       [esi+54h],edx
  004b9b9f  896e1c                mov       [esi+1Ch],ebp
  004b9ba2  895e20                mov       [esi+20h],ebx
  004b9ba5  894624                mov       [esi+24h],eax
  004b9ba8  e8939d0700            call      00533940h
  004b9bad  8bbe90000000          mov       edi,[esi+90h]
  004b9bb3  8b567c                mov       edx,[esi+7Ch]
  004b9bb6  81e7fffcffff          and       edi,0FFFFFCFFh
  004b9bbc  33c0                  xor       eax,eax
  004b9bbe  89be90000000          mov       [esi+90h],edi
  004b9bc4  8a420f                mov       al,[edx+0Fh]
  004b9bc7  33db                  xor       ebx,ebx
  004b9bc9  83c404                add       esp,4
  004b9bcc  3bc3                  cmp       eax,ebx
  004b9bce  8bcf                  mov       ecx,edi
  004b9bd0  7416                  je        short 004B9BE8h
  004b9bd2  33d2                  xor       edx,edx
  004b9bd4  48                    dec       eax
  004b9bd5  8a1538e05d00          mov       dl,[5DE038h]
  004b9bdb  3bd0                  cmp       edx,eax
  004b9bdd  7409                  je        short 004B9BE8h
  004b9bdf  80cd08                or        ch,8
  004b9be2  898e90000000          mov       [esi+90h],ecx

LAB_004b9be8:
  004b9be8  8b06                  mov       eax,[esi]
  004b9bea  8bd0                  mov       edx,eax
  004b9bec  83e201                and       edx,1
  004b9bef  740a                  je        short 004B9BFBh
  004b9bf1  8b4e60                mov       ecx,[esi+60h]
  004b9bf4  8b7e20                mov       edi,[esi+20h]
  004b9bf7  3bf9                  cmp       edi,ecx
  004b9bf9  7e14                  jle       short 004B9C0Fh

LAB_004b9bfb:
  004b9bfb  3bd3                  cmp       edx,ebx
  004b9bfd  7540                  jne       short 004B9C3Fh
  004b9bff  8b7e20                mov       edi,[esi+20h]
  004b9c02  8b4e60                mov       ecx,[esi+60h]
  004b9c05  81ef00c00000          sub       edi,0C000h
  004b9c0b  3bf9                  cmp       edi,ecx
  004b9c0d  7f28                  jg        short 004B9C37h

LAB_004b9c0f:
  004b9c0f  f7869000000000a00000  test      dword [esi+90h],0A000h
  004b9c19  751c                  jne       short 004B9C37h
  004b9c1b  a802                  test      al,2
  004b9c1d  894e20                mov       [esi+20h],ecx
  004b9c20  750c                  jne       short 004B9C2Eh
  004b9c22  3bd3                  cmp       edx,ebx
  004b9c24  7408                  je        short 004B9C2Eh
  004b9c26  385e43                cmp       [esi+43h],bl
  004b9c29  7403                  je        short 004B9C2Eh
  004b9c2b  885e43                mov       [esi+43h],bl

LAB_004b9c2e:
  004b9c2e  24fe                  and       al,0FEh
  004b9c30  895e54                mov       [esi+54h],ebx
  004b9c33  8906                  mov       [esi],eax
  004b9c35  eb08                  jmp       short 004B9C3Fh

LAB_004b9c37:
  004b9c37  3bd3                  cmp       edx,ebx
  004b9c39  7504                  jne       short 004B9C3Fh
  004b9c3b  0c01                  or        al,1
  004b9c3d  8906                  mov       [esi],eax

LAB_004b9c3f:
  004b9c3f  f7869000000080ad0020  test      dword [esi+90h],2000AD80h
  004b9c49  0f840a010000          je        near 004B9D59h
  004b9c4f  f60601                test      byte [esi],1
  004b9c52  0f85d9000000          jne       near 004B9D31h
  004b9c58  8a4e42                mov       cl,[esi+42h]
  004b9c5b  b007                  mov       al,7
  004b9c5d  3ac8                  cmp       cl,al
  004b9c5f  7420                  je        short 004B9C81h
  004b9c61  884642                mov       [esi+42h],al
  004b9c64  e8d7d40800            call      00547140h
  004b9c69  99                    cdq
  004b9c6a  b928000000            mov       ecx,28h
  004b9c6f  66895e12              mov       [esi+12h],bx
  004b9c73  f7f9                  idiv      ecx
  004b9c75  66c746105000          mov       word [esi+10h],50h
  004b9c7b  80c214                add       dl,14h
  004b9c7e  885643                mov       [esi+43h],dl

LAB_004b9c81:
  004b9c81  8b16                  mov       edx,[esi]
  004b9c83  8b442428              mov       eax,[esp+28h]
  004b9c87  8b4c242c              mov       ecx,[esp+2Ch]
  004b9c8b  83e2fd                and       edx,0FFFFFFFDh
  004b9c8e  8916                  mov       [esi],edx
  004b9c90  8b542424              mov       edx,[esp+24h]
  004b9c94  894620                mov       [esi+20h],eax
  004b9c97  8b442458              mov       eax,[esp+58h]
  004b9c9b  89561c                mov       [esi+1Ch],edx
  004b9c9e  8b542454              mov       edx,[esp+54h]
  004b9ca2  25ffff0000            and       eax,0FFFFh
  004b9ca7  894e24                mov       [esi+24h],ecx
  004b9caa  8b4c2450              mov       ecx,[esp+50h]
  004b9cae  89567c                mov       [esi+7Ch],edx
  004b9cb1  8b54243c              mov       edx,[esp+3Ch]
  004b9cb5  898690000000          mov       [esi+90h],eax
  004b9cbb  8b442444              mov       eax,[esp+44h]
  004b9cbf  898e84000000          mov       [esi+84h],ecx
  004b9cc5  8b4c2440              mov       ecx,[esp+40h]
  004b9cc9  898680000000          mov       [esi+80h],eax
  004b9ccf  8b44244c              mov       eax,[esp+4Ch]
  004b9cd3  899688000000          mov       [esi+88h],edx
  004b9cd9  8b542448              mov       edx,[esp+48h]
  004b9cdd  6880ad0020            push      2000AD80h
  004b9ce2  56                    push      esi
  004b9ce3  89868c000000          mov       [esi+8Ch],eax
  004b9ce9  894e64                mov       [esi+64h],ecx
  004b9cec  895660                mov       [esi+60h],edx
  004b9cef  e86cc10100            call      004D5E60h
  004b9cf4  83c408                add       esp,8
  004b9cf7  663bc3                cmp       ax,bx
  004b9cfa  7525                  jne       short 004B9D21h
  004b9cfc  e83fd40800            call      00547140h
  004b9d01  99                    cdq
  004b9d02  33c2                  xor       eax,edx
  004b9d04  8b4c2418              mov       ecx,[esp+18h]
  004b9d08  2bc2                  sub       eax,edx
  004b9d0a  83c140                add       ecx,40h
  004b9d0d  83e001                and       eax,1
  004b9d10  33c2                  xor       eax,edx
  004b9d12  2bc2                  sub       eax,edx
  004b9d14  8d4400ff              lea       eax,[eax+eax-1]
  004b9d18  0fafc8                imul      ecx,eax
  004b9d1b  66014e32              add       [esi+32h],cx
  004b9d1f  eb38                  jmp       short 004B9D59h

LAB_004b9d21:
  004b9d21  8b542418              mov       edx,[esp+18h]
  004b9d25  83c240                add       edx,40h
  004b9d28  0fafd0                imul      edx,eax
  004b9d2b  66015632              add       [esi+32h],dx
  004b9d2f  eb28                  jmp       short 004B9D59h

LAB_004b9d31:
  004b9d31  8b442424              mov       eax,[esp+24h]
  004b9d35  8b4c242c              mov       ecx,[esp+2Ch]
  004b9d39  895e5c                mov       [esi+5Ch],ebx
  004b9d3c  895e50                mov       [esi+50h],ebx
  004b9d3f  895e58                mov       [esi+58h],ebx
  004b9d42  89461c                mov       [esi+1Ch],eax
  004b9d45  894e24                mov       [esi+24h],ecx
  004b9d48  885e42                mov       [esi+42h],bl
  004b9d4b  c6464328              mov       byte [esi+43h],28h
  004b9d4f  66895e12              mov       [esi+12h],bx
  004b9d53  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004b9d59:
  004b9d59  6aff                  push      0FFFFFFFFh
  004b9d5b  6aff                  push      0FFFFFFFFh
  004b9d5d  6aff                  push      0FFFFFFFFh
  004b9d5f  56                    push      esi
  004b9d60  e8ab9a0000            call      004C3810h
  004b9d65  8b442430              mov       eax,[esp+30h]
  004b9d69  83c410                add       esp,10h
  004b9d6c  3bc3                  cmp       eax,ebx
  004b9d6e  7517                  jne       short 004B9D87h
  004b9d70  668b942482000000      mov       dx,[esp+82h]
  004b9d78  66895632              mov       [esi+32h],dx
  004b9d7c  5f                    pop       edi
  004b9d7d  5e                    pop       esi
  004b9d7e  5d                    pop       ebp
  004b9d7f  5b                    pop       ebx
  004b9d80  81c484000000          add       esp,84h
  004b9d86  c3                    ret

LAB_004b9d87:
  004b9d87  385e43                cmp       [esi+43h],bl
  004b9d8a  0f85e6020000          jne       near 004BA076h
  004b9d90  8b466c                mov       eax,[esi+6Ch]
  004b9d93  8b6e1c                mov       ebp,[esi+1Ch]
  004b9d96  8b4e24                mov       ecx,[esi+24h]
  004b9d99  8b781c                mov       edi,[eax+1Ch]
  004b9d9c  2bfd                  sub       edi,ebp
  004b9d9e  8b6824                mov       ebp,[eax+24h]
  004b9da1  2be9                  sub       ebp,ecx
  004b9da3  c1ff0c                sar       edi,0Ch
  004b9da6  c1fd0c                sar       ebp,0Ch
  004b9da9  8bc7                  mov       eax,edi
  004b9dab  8bcd                  mov       ecx,ebp
  004b9dad  0fafc7                imul      eax,edi
  004b9db0  0fafcd                imul      ecx,ebp
  004b9db3  03c1                  add       eax,ecx
  004b9db5  50                    push      eax
  004b9db6  e855bc0800            call      00545A10h
  004b9dbb  55                    push      ebp
  004b9dbc  57                    push      edi
  004b9dbd  8944241c              mov       [esp+1Ch],eax
  004b9dc1  e85cbc0800            call      00545A22h
  004b9dc6  668b5632              mov       dx,[esi+32h]
  004b9dca  2bc2                  sub       eax,edx
  004b9dcc  25ff0f0000            and       eax,0FFFh
  004b9dd1  8944241c              mov       [esp+1Ch],eax
  004b9dd5  8b442414              mov       eax,[esp+14h]
  004b9dd9  3dfa000000            cmp       eax,0FAh
  004b9dde  0f8d5a010000          jge       near 004B9F3Eh
  004b9de4  e857d30800            call      00547140h
  004b9de9  99                    cdq
  004b9dea  b964000000            mov       ecx,64h
  004b9def  f7f9                  idiv      ecx
  004b9df1  83fa1e                cmp       edx,1Eh
  004b9df4  0f8d86000000          jge       near 004B9E80h
  004b9dfa  8b54241c              mov       edx,[esp+1Ch]
  004b9dfe  8b7c2410              mov       edi,[esp+10h]
  004b9e02  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004b9e09  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004b9e10  25ff0f0000            and       eax,0FFFh
  004b9e15  3bc1                  cmp       eax,ecx
  004b9e17  7f3b                  jg        short 004B9E54h
  004b9e19  e822d30800            call      00547140h
  004b9e1e  99                    cdq
  004b9e1f  8d4c3f01              lea       ecx,[edi+edi+1]
  004b9e23  f7f9                  idiv      ecx
  004b9e25  8b442414              mov       eax,[esp+14h]
  004b9e29  83c264                add       edx,64h
  004b9e2c  3bc2                  cmp       eax,edx
  004b9e2e  7d24                  jge       short 004B9E54h
  004b9e30  8b16                  mov       edx,[esi]
  004b9e32  c7461803000000        mov       dword [esi+18h],3
  004b9e39  81e2fffffffe          and       edx,0FEFFFFFFh
  004b9e3f  c6464301              mov       byte [esi+43h],1
  004b9e43  83ca02                or        edx,2
  004b9e46  895e5c                mov       [esi+5Ch],ebx
  004b9e49  8916                  mov       [esi],edx
  004b9e4b  66895e1a              mov       [esi+1Ah],bx
  004b9e4f  e926020000            jmp       004BA07Ah

LAB_004b9e54:
  004b9e54  c6464202              mov       byte [esi+42h],2
  004b9e58  e8e3d20800            call      00547140h
  004b9e5d  99                    cdq
  004b9e5e  b90a000000            mov       ecx,0Ah
  004b9e63  66895e1a              mov       [esi+1Ah],bx
  004b9e67  f7f9                  idiv      ecx
  004b9e69  8bc7                  mov       eax,edi
  004b9e6b  66c746105000          mov       word [esi+10h],50h
  004b9e71  d1f8                  sar       eax,1
  004b9e73  02d0                  add       dl,al
  004b9e75  80c214                add       dl,14h
  004b9e78  885643                mov       [esi+43h],dl
  004b9e7b  e9fa010000            jmp       004BA07Ah

LAB_004b9e80:
  004b9e80  83fa32                cmp       edx,32h
  004b9e83  7d30                  jge       short 004B9EB5h
  004b9e85  c6464202              mov       byte [esi+42h],2
  004b9e89  e8b2d20800            call      00547140h
  004b9e8e  99                    cdq
  004b9e8f  b90a000000            mov       ecx,0Ah
  004b9e94  8b7c2410              mov       edi,[esp+10h]
  004b9e98  f7f9                  idiv      ecx
  004b9e9a  8bc7                  mov       eax,edi
  004b9e9c  66c746105000          mov       word [esi+10h],50h
  004b9ea2  d1f8                  sar       eax,1
  004b9ea4  66895e1a              mov       [esi+1Ah],bx
  004b9ea8  02d0                  add       dl,al
  004b9eaa  80c214                add       dl,14h
  004b9ead  885643                mov       [esi+43h],dl
  004b9eb0  e9c5010000            jmp       004BA07Ah

LAB_004b9eb5:
  004b9eb5  83fa5a                cmp       edx,5Ah
  004b9eb8  7d2d                  jge       short 004B9EE7h
  004b9eba  8b4c2418              mov       ecx,[esp+18h]
  004b9ebe  8b442414              mov       eax,[esp+14h]
  004b9ec2  83c164                add       ecx,64h
  004b9ec5  3bc1                  cmp       eax,ecx
  004b9ec7  7e1e                  jle       short 004B9EE7h
  004b9ec9  c6464206              mov       byte [esi+42h],6
  004b9ecd  c6464301              mov       byte [esi+43h],1
  004b9ed1  e86ad20800            call      00547140h
  004b9ed6  66894610              mov       [esi+10h],ax
  004b9eda  66895e1a              mov       [esi+1Ah],bx
  004b9ede  66895e12              mov       [esi+12h],bx
  004b9ee2  e98f010000            jmp       004BA076h

LAB_004b9ee7:
  004b9ee7  c6464203              mov       byte [esi+42h],3
  004b9eeb  e850d20800            call      00547140h
  004b9ef0  99                    cdq
  004b9ef1  b90a000000            mov       ecx,0Ah
  004b9ef6  8b7c2410              mov       edi,[esp+10h]
  004b9efa  f7f9                  idiv      ecx
  004b9efc  8b4c2418              mov       ecx,[esp+18h]
  004b9f00  8bc7                  mov       eax,edi
  004b9f02  d1f8                  sar       eax,1
  004b9f04  8da980000000          lea       ebp,[ecx+80h]
  004b9f0a  c1e504                shl       ebp,4
  004b9f0d  02d0                  add       dl,al
  004b9f0f  80c214                add       dl,14h
  004b9f12  885643                mov       [esi+43h],dl
  004b9f15  e826d20800            call      00547140h
  004b9f1a  99                    cdq
  004b9f1b  f7fd                  idiv      ebp
  004b9f1d  d1fd                  sar       ebp,1
  004b9f1f  66895e1a              mov       [esi+1Ah],bx
  004b9f23  2bd5                  sub       edx,ebp
  004b9f25  8b6c241c              mov       ebp,[esp+1Ch]
  004b9f29  03d5                  add       edx,ebp
  004b9f2b  66035632              add       dx,[esi+32h]
  004b9f2f  81c200080000          add       edx,800h
  004b9f35  66895610              mov       [esi+10h],dx
  004b9f39  e93c010000            jmp       004BA07Ah

LAB_004b9f3e:
  004b9f3e  3d58020000            cmp       eax,258h
  004b9f43  0f8dc6000000          jge       near 004BA00Fh
  004b9f49  e8f2d10800            call      00547140h
  004b9f4e  99                    cdq
  004b9f4f  b964000000            mov       ecx,64h
  004b9f54  f7f9                  idiv      ecx
  004b9f56  83fa1e                cmp       edx,1Eh
  004b9f59  7d29                  jge       short 004B9F84h
  004b9f5b  8b54241c              mov       edx,[esp+1Ch]
  004b9f5f  81c200040000          add       edx,400h
  004b9f65  81e2ff0f0000          and       edx,0FFFh
  004b9f6b  81fa00080000          cmp       edx,800h
  004b9f71  7c6c                  jl        short 004B9FDFh
  004b9f73  c6464201              mov       byte [esi+42h],1
  004b9f77  c6464301              mov       byte [esi+43h],1
  004b9f7b  66895e1a              mov       [esi+1Ah],bx
  004b9f7f  e9f2000000            jmp       004BA076h

LAB_004b9f84:
  004b9f84  83fa32                cmp       edx,32h
  004b9f87  7d51                  jge       short 004B9FDAh
  004b9f89  c6464203              mov       byte [esi+42h],3
  004b9f8d  e8aed10800            call      00547140h
  004b9f92  99                    cdq
  004b9f93  b90a000000            mov       ecx,0Ah
  004b9f98  8b7c2410              mov       edi,[esp+10h]
  004b9f9c  f7f9                  idiv      ecx
  004b9f9e  8b4c2418              mov       ecx,[esp+18h]
  004b9fa2  8bc7                  mov       eax,edi
  004b9fa4  d1f8                  sar       eax,1
  004b9fa6  8da980000000          lea       ebp,[ecx+80h]
  004b9fac  c1e504                shl       ebp,4
  004b9faf  02d0                  add       dl,al
  004b9fb1  80c214                add       dl,14h
  004b9fb4  885643                mov       [esi+43h],dl
  004b9fb7  e884d10800            call      00547140h
  004b9fbc  99                    cdq
  004b9fbd  f7fd                  idiv      ebp
  004b9fbf  8b4c241c              mov       ecx,[esp+1Ch]
  004b9fc3  66895e1a              mov       [esi+1Ah],bx
  004b9fc7  d1fd                  sar       ebp,1
  004b9fc9  2bd5                  sub       edx,ebp
  004b9fcb  03d1                  add       edx,ecx
  004b9fcd  66035632              add       dx,[esi+32h]
  004b9fd1  66895610              mov       [esi+10h],dx
  004b9fd5  e9a0000000            jmp       004BA07Ah

LAB_004b9fda:
  004b9fda  83fa5a                cmp       edx,5Ah
  004b9fdd  7c42                  jl        short 004BA021h

LAB_004b9fdf:
  004b9fdf  885e42                mov       [esi+42h],bl
  004b9fe2  e859d10800            call      00547140h
  004b9fe7  99                    cdq
  004b9fe8  b90a000000            mov       ecx,0Ah
  004b9fed  8b7c2410              mov       edi,[esp+10h]
  004b9ff1  f7f9                  idiv      ecx
  004b9ff3  8bc7                  mov       eax,edi
  004b9ff5  895e18                mov       [esi+18h],ebx
  004b9ff8  c1f802                sar       eax,2
  004b9ffb  66895e1a              mov       [esi+1Ah],bx
  004b9fff  02d0                  add       dl,al
  004ba001  8b06                  mov       eax,[esi]
  004ba003  0d00000001            or        eax,1000000h
  004ba008  885643                mov       [esi+43h],dl
  004ba00b  8906                  mov       [esi],eax
  004ba00d  eb6b                  jmp       short 004BA07Ah

LAB_004ba00f:
  004ba00f  e82cd10800            call      00547140h
  004ba014  99                    cdq
  004ba015  b964000000            mov       ecx,64h
  004ba01a  f7f9                  idiv      ecx
  004ba01c  83fa32                cmp       edx,32h
  004ba01f  7d2d                  jge       short 004BA04Eh

LAB_004ba021:
  004ba021  c6464202              mov       byte [esi+42h],2
  004ba025  e816d10800            call      00547140h
  004ba02a  99                    cdq
  004ba02b  b90a000000            mov       ecx,0Ah
  004ba030  8b7c2410              mov       edi,[esp+10h]
  004ba034  f7f9                  idiv      ecx
  004ba036  8bc7                  mov       eax,edi
  004ba038  66c746106400          mov       word [esi+10h],64h
  004ba03e  d1f8                  sar       eax,1
  004ba040  66895e1a              mov       [esi+1Ah],bx
  004ba044  02d0                  add       dl,al
  004ba046  80c214                add       dl,14h
  004ba049  885643                mov       [esi+43h],dl
  004ba04c  eb2c                  jmp       short 004BA07Ah

LAB_004ba04e:
  004ba04e  83fa5a                cmp       edx,5Ah
  004ba051  7d0e                  jge       short 004BA061h
  004ba053  c6464201              mov       byte [esi+42h],1
  004ba057  c6464301              mov       byte [esi+43h],1
  004ba05b  66895e1a              mov       [esi+1Ah],bx
  004ba05f  eb15                  jmp       short 004BA076h

LAB_004ba061:
  004ba061  c6464206              mov       byte [esi+42h],6
  004ba065  c6464301              mov       byte [esi+43h],1
  004ba069  66895e1a              mov       [esi+1Ah],bx
  004ba06d  e8ced00800            call      00547140h
  004ba072  66894610              mov       [esi+10h],ax

LAB_004ba076:
  004ba076  8b7c2410              mov       edi,[esp+10h]

LAB_004ba07a:
  004ba07a  837c241464            cmp       dword [esp+14h],64h
  004ba07f  7d60                  jge       short 004BA0E1h
  004ba081  8b4c241c              mov       ecx,[esp+1Ch]
  004ba085  8d04fd00020000        lea       eax,[edi*8+200h]
  004ba08c  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004ba093  81e2ff0f0000          and       edx,0FFFh
  004ba099  3bd0                  cmp       edx,eax
  004ba09b  7f3c                  jg        short 004BA0D9h
  004ba09d  e89ed00800            call      00547140h
  004ba0a2  99                    cdq
  004ba0a3  8d4c3f01              lea       ecx,[edi+edi+1]
  004ba0a7  f7f9                  idiv      ecx
  004ba0a9  8b442414              mov       eax,[esp+14h]
  004ba0ad  83c264                add       edx,64h
  004ba0b0  3bc2                  cmp       eax,edx
  004ba0b2  7d25                  jge       short 004BA0D9h
  004ba0b4  8b16                  mov       edx,[esi]
  004ba0b6  66c746180300          mov       word [esi+18h],3
  004ba0bc  81e2fffffffe          and       edx,0FEFFFFFFh
  004ba0c2  c6464301              mov       byte [esi+43h],1
  004ba0c6  83ca02                or        edx,2
  004ba0c9  895e5c                mov       [esi+5Ch],ebx
  004ba0cc  8916                  mov       [esi],edx
  004ba0ce  5f                    pop       edi
  004ba0cf  5e                    pop       esi
  004ba0d0  5d                    pop       ebp
  004ba0d1  5b                    pop       ebx
  004ba0d2  81c484000000          add       esp,84h
  004ba0d8  c3                    ret

LAB_004ba0d9:
  004ba0d9  c6464201              mov       byte [esi+42h],1
  004ba0dd  c6464301              mov       byte [esi+43h],1

LAB_004ba0e1:
  004ba0e1  5f                    pop       edi
  004ba0e2  5e                    pop       esi
  004ba0e3  5d                    pop       ebp
  004ba0e4  5b                    pop       ebx
  004ba0e5  81c484000000          add       esp,84h
  004ba0eb  c3                    ret
  004ba0ec  11984b003c98          adc       [eax-67C3FFB5h],ebx
  004ba0f2  4b                    dec       ebx
  004ba0f3  00a1984b0026          add       [ecx+26004B98h],ah
  004ba0f9  99                    cdq
  004ba0fa  4b                    dec       ebx
  004ba0fb  000d9b4b000d          add       [0D004B9Bh],cl
  004ba101  9b                    wait
  004ba102  4b                    dec       ebx
  004ba103  00549a4b              add       [edx+ebx*4+4Bh],dl
  004ba107  00c4                  add       ah,al
  004ba109  9a4b0090909090        call      9090h:9090004Bh

52 cible(s) de saut interne : 0x004b962f, 0x004b9734, 0x004b9756, 0x004b976b, 0x004b97c6, 0x004b97ef, 0x004b9824, 0x004b9864, 0x004b987a, 0x004b9881, 0x004b9895, 0x004b98b4, 0x004b98c0, 0x004b98d1, 0x004b990d, 0x004b9918, 0x004b9932, 0x004b9942, 0x004b99b8, 0x004b9a3d, 0x004b9a6c, 0x004b9add, 0x004b9ae9, 0x004b9b0a, 0x004b9b0d, 0x004b9be8, 0x004b9bfb, 0x004b9c0f, 0x004b9c2e, 0x004b9c37, 0x004b9c3f, 0x004b9c81, 0x004b9d21, 0x004b9d31, 0x004b9d59, 0x004b9d87, 0x004b9e54, 0x004b9e80, 0x004b9eb5, 0x004b9ee7, 0x004b9f3e, 0x004b9f84, 0x004b9fda, 0x004b9fdf, 0x004ba00f, 0x004ba021, 0x004ba04e, 0x004ba061, 0x004ba076, 0x004ba07a, 0x004ba0d9, 0x004ba0e1
