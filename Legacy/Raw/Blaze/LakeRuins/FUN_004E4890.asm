Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e4890  section .text  offset fichier 0xe3c90  1680 octets

  004e4890  83ec24                sub       esp,24h
  004e4893  8d44240c              lea       eax,[esp+0Ch]
  004e4897  8d4c2420              lea       ecx,[esp+20h]
  004e489b  8d54241c              lea       edx,[esp+1Ch]
  004e489f  53                    push      ebx
  004e48a0  55                    push      ebp
  004e48a1  56                    push      esi
  004e48a2  8b742434              mov       esi,[esp+34h]
  004e48a6  57                    push      edi
  004e48a7  50                    push      eax
  004e48a8  51                    push      ecx
  004e48a9  52                    push      edx
  004e48aa  56                    push      esi
  004e48ab  e8d0400500            call      00538980h
  004e48b0  83c410                add       esp,10h
  004e48b3  8d442420              lea       eax,[esp+20h]
  004e48b7  8d4c2418              lea       ecx,[esp+18h]
  004e48bb  8d542438              lea       edx,[esp+38h]
  004e48bf  50                    push      eax
  004e48c0  51                    push      ecx
  004e48c1  8d442418              lea       eax,[esp+18h]
  004e48c5  52                    push      edx
  004e48c6  8d4c2420              lea       ecx,[esp+20h]
  004e48ca  50                    push      eax
  004e48cb  51                    push      ecx
  004e48cc  56                    push      esi
  004e48cd  e84e160500            call      00535F20h
  004e48d2  8b442430              mov       eax,[esp+30h]
  004e48d6  33db                  xor       ebx,ebx
  004e48d8  83c418                add       esp,18h
  004e48db  3bc3                  cmp       eax,ebx
  004e48dd  0f84bb020000          je        near 004E4B9Eh
  004e48e3  56                    push      esi
  004e48e4  e8b76cffff            call      004DB5A0h
  004e48e9  8b06                  mov       eax,[esi]
  004e48eb  83c404                add       esp,4
  004e48ee  a802                  test      al,2
  004e48f0  0f8416040000          je        near 004E4D0Ch
  004e48f6  f644242010            test      byte [esp+20h],10h
  004e48fb  740c                  je        short 004E4909h
  004e48fd  24fd                  and       al,0FDh
  004e48ff  885e43                mov       [esi+43h],bl
  004e4902  8906                  mov       [esi],eax
  004e4904  e995020000            jmp       004E4B9Eh

LAB_004e4909:
  004e4909  8a4642                mov       al,[esi+42h]
  004e490c  84c0                  test      al,al
  004e490e  0f858a020000          jne       near 004E4B9Eh
  004e4914  0fbf4610              movsx     eax,word [esi+10h]
  004e4918  2bc3                  sub       eax,ebx
  004e491a  0f84e7000000          je        near 004E4A07h
  004e4920  48                    dec       eax
  004e4921  0f8577020000          jne       near 004E4B9Eh
  004e4927  66837e1a0d            cmp       word [esi+1Ah],0Dh
  004e492c  895e5c                mov       [esi+5Ch],ebx
  004e492f  0f858d000000          jne       near 004E49C2h
  004e4935  0fbf5632              movsx     edx,word [esi+32h]
  004e4939  52                    push      edx
  004e493a  c7465c00800200        mov       dword [esi+5Ch],28000h
  004e4941  e8d6100600            call      00545A1Ch
  004e4946  0faf465c              imul      eax,[esi+5Ch]
  004e494a  8b4e50                mov       ecx,[esi+50h]
  004e494d  c1f80c                sar       eax,0Ch
  004e4950  03c8                  add       ecx,eax
  004e4952  0fbf4632              movsx     eax,word [esi+32h]
  004e4956  50                    push      eax
  004e4957  894e50                mov       [esi+50h],ecx
  004e495a  e8b7100600            call      00545A16h
  004e495f  0faf465c              imul      eax,[esi+5Ch]
  004e4963  8b5658                mov       edx,[esi+58h]
  004e4966  c7465c00500000        mov       dword [esi+5Ch],5000h
  004e496d  c1f80c                sar       eax,0Ch
  004e4970  03d0                  add       edx,eax
  004e4972  66ff4e10              dec       word [esi+10h]
  004e4976  895658                mov       [esi+58h],edx
  004e4979  e8c2270600            call      00547140h
  004e497e  99                    cdq
  004e497f  b964000000            mov       ecx,64h
  004e4984  f7f9                  idiv      ecx
  004e4986  83fa0a                cmp       edx,0Ah
  004e4989  7d37                  jge       short 004E49C2h
  004e498b  e8b0270600            call      00547140h
  004e4990  a1202e5500            mov       eax,[552E20h]
  004e4995  6aff                  push      0FFFFFFFFh
  004e4997  25ffff0000            and       eax,0FFFFh
  004e499c  6aff                  push      0FFFFFFFFh
  004e499e  66894606              mov       [esi+6],ax
  004e49a2  25ffff0000            and       eax,0FFFFh
  004e49a7  8bd0                  mov       edx,eax
  004e49a9  c1f808                sar       eax,8
  004e49ac  81e2ff000000          and       edx,0FFh
  004e49b2  25ff000000            and       eax,0FFh
  004e49b7  52                    push      edx
  004e49b8  50                    push      eax
  004e49b9  56                    push      esi
  004e49ba  e8c15f0500            call      0053A980h
  004e49bf  83c414                add       esp,14h

LAB_004e49c2:
  004e49c2  8b442438              mov       eax,[esp+38h]
  004e49c6  8d8880000000          lea       ecx,[eax+80h]
  004e49cc  81e1ff0f0000          and       ecx,0FFFh
  004e49d2  81f900010000          cmp       ecx,100h
  004e49d8  7f09                  jg        short 004E49E3h
  004e49da  66014632              add       [esi+32h],ax
  004e49de  e9bb010000            jmp       004E4B9Eh

LAB_004e49e3:
  004e49e3  25ff0f0000            and       eax,0FFFh
  004e49e8  33d2                  xor       edx,edx
  004e49ea  3d00080000            cmp       eax,800h
  004e49ef  0f9dc2                setge     dl
  004e49f2  4a                    dec       edx
  004e49f3  81e200010000          and       edx,100h
  004e49f9  83c280                add       edx,0FFFFFF80h
  004e49fc  8bc2                  mov       eax,edx
  004e49fe  66014632              add       [esi+32h],ax
  004e4a02  e997010000            jmp       004E4B9Eh

LAB_004e4a07:
  004e4a07  8b4650                mov       eax,[esi+50h]
  004e4a0a  8b4e58                mov       ecx,[esi+58h]
  004e4a0d  c1f80c                sar       eax,0Ch
  004e4a10  8bd0                  mov       edx,eax
  004e4a12  c1f90c                sar       ecx,0Ch
  004e4a15  0fafd0                imul      edx,eax
  004e4a18  8bc1                  mov       eax,ecx
  004e4a1a  0fafc1                imul      eax,ecx
  004e4a1d  03d0                  add       edx,eax
  004e4a1f  52                    push      edx
  004e4a20  e8eb0f0600            call      00545A10h
  004e4a25  8b0e                  mov       ecx,[esi]
  004e4a27  f7c100000002          test      ecx,2000000h
  004e4a2d  740a                  je        short 004E4A39h
  004e4a2f  83f805                cmp       eax,5
  004e4a32  7f05                  jg        short 004E4A39h
  004e4a34  83e1dd                and       ecx,0FFFFFFDDh
  004e4a37  890e                  mov       [esi],ecx

LAB_004e4a39:
  004e4a39  83f803                cmp       eax,3
  004e4a3c  0f8c5c010000          jl        near 004E4B9Eh
  004e4a42  f60620                test      byte [esi],20h
  004e4a45  0f8553010000          jne       near 004E4B9Eh
  004e4a4b  8b442438              mov       eax,[esp+38h]
  004e4a4f  8d4828                lea       ecx,[eax+28h]
  004e4a52  81e1ff0f0000          and       ecx,0FFFh
  004e4a58  83f950                cmp       ecx,50h
  004e4a5b  7e18                  jle       short 004E4A75h
  004e4a5d  25ff0f0000            and       eax,0FFFh
  004e4a62  33d2                  xor       edx,edx
  004e4a64  3d00080000            cmp       eax,800h
  004e4a69  0f9dc2                setge     dl
  004e4a6c  4a                    dec       edx
  004e4a6d  83e250                and       edx,50h
  004e4a70  83c2d8                add       edx,0FFFFFFD8h
  004e4a73  8bc2                  mov       eax,edx

LAB_004e4a75:
  004e4a75  66014632              add       [esi+32h],ax
  004e4a79  8d6e1c                lea       ebp,[esi+1Ch]
  004e4a7c  53                    push      ebx
  004e4a7d  68082e5500            push      552E08h
  004e4a82  55                    push      ebp
  004e4a83  56                    push      esi
  004e4a84  e8a7510300            call      00519C30h
  004e4a89  8bf8                  mov       edi,eax
  004e4a8b  83c410                add       esp,10h
  004e4a8e  3bfb                  cmp       edi,ebx
  004e4a90  7406                  je        short 004E4A98h
  004e4a92  8b06                  mov       eax,[esi]
  004e4a94  0c20                  or        al,20h
  004e4a96  8906                  mov       [esi],eax

LAB_004e4a98:
  004e4a98  391db8e05d00          cmp       [5DE0B8h],ebx
  004e4a9e  0f84fa000000          je        near 004E4B9Eh
  004e4aa4  8b07                  mov       eax,[edi]
  004e4aa6  66c787960000000800    mov       word [edi+96h],8
  004e4aaf  0c40                  or        al,40h
  004e4ab1  8907                  mov       [edi],eax
  004e4ab3  8d471c                lea       eax,[edi+1Ch]
  004e4ab6  50                    push      eax
  004e4ab7  6800180000            push      1800h
  004e4abc  6800030000            push      300h
  004e4ac1  6a03                  push      3
  004e4ac3  e858900500            call      0053DB20h
  004e4ac8  83c410                add       esp,10h
  004e4acb  e870260600            call      00547140h
  004e4ad0  99                    cdq
  004e4ad1  b964000000            mov       ecx,64h
  004e4ad6  f7f9                  idiv      ecx
  004e4ad8  83fa14                cmp       edx,14h
  004e4adb  0f8d1c020000          jge       near 004E4CFDh
  004e4ae1  8b4c241c              mov       ecx,[esp+1Ch]
  004e4ae5  57                    push      edi
  004e4ae6  56                    push      esi
  004e4ae7  668b4130              mov       ax,[ecx+30h]
  004e4aeb  0fbfd8                movsx     ebx,ax
  004e4aee  66d1f8                sar       ax,1
  004e4af1  66894130              mov       [ecx+30h],ax
  004e4af5  e886500500            call      00539B80h
  004e4afa  8b542424              mov       edx,[esp+24h]
  004e4afe  83c408                add       esp,8
  004e4b01  66895a30              mov       [edx+30h],bx
  004e4b05  8b07                  mov       eax,[edi]
  004e4b07  a900000040            test      eax,40000000h
  004e4b0c  0f858a000000          jne       near 004E4B9Ch
  004e4b12  0fbf4632              movsx     eax,word [esi+32h]
  004e4b16  8b4c2438              mov       ecx,[esp+38h]
  004e4b1a  03c8                  add       ecx,eax
  004e4b1c  51                    push      ecx
  004e4b1d  e8fa0e0600            call      00545A1Ch
  004e4b22  8d0440                lea       eax,[eax+eax*2]
  004e4b25  8d1480                lea       edx,[eax+eax*4]
  004e4b28  d1e2                  shl       edx,1
  004e4b2a  895750                mov       [edi+50h],edx
  004e4b2d  8b4c2438              mov       ecx,[esp+38h]
  004e4b31  0fbf4632              movsx     eax,word [esi+32h]
  004e4b35  03c8                  add       ecx,eax
  004e4b37  51                    push      ecx
  004e4b38  e8d90e0600            call      00545A16h
  004e4b3d  8d0440                lea       eax,[eax+eax*2]
  004e4b40  8d1480                lea       edx,[eax+eax*4]
  004e4b43  8b07                  mov       eax,[edi]
  004e4b45  d1e2                  shl       edx,1
  004e4b47  a801                  test      al,1
  004e4b49  895758                mov       [edi+58h],edx
  004e4b4c  750b                  jne       short 004E4B59h
  004e4b4e  0c01                  or        al,1
  004e4b50  c7475400800100        mov       dword [edi+54h],18000h
  004e4b57  8907                  mov       [edi],eax

LAB_004e4b59:
  004e4b59  8b4620                mov       eax,[esi+20h]
  004e4b5c  8b571c                mov       edx,[edi+1Ch]
  004e4b5f  8b5d00                mov       ebx,[ebp]
  004e4b62  8b4e24                mov       ecx,[esi+24h]
  004e4b65  89442424              mov       [esp+24h],eax
  004e4b69  895500                mov       [ebp],edx
  004e4b6c  8b4720                mov       eax,[edi+20h]
  004e4b6f  6aff                  push      0FFFFFFFFh
  004e4b71  894c242c              mov       [esp+2Ch],ecx
  004e4b75  894620                mov       [esi+20h],eax
  004e4b78  8b4f24                mov       ecx,[edi+24h]
  004e4b7b  6a00                  push      0
  004e4b7d  6a39                  push      39h
  004e4b7f  56                    push      esi
  004e4b80  894e24                mov       [esi+24h],ecx
  004e4b83  e8485ef9ff            call      0047A9D0h
  004e4b88  8b542434              mov       edx,[esp+34h]
  004e4b8c  8b442438              mov       eax,[esp+38h]
  004e4b90  83c410                add       esp,10h
  004e4b93  895d00                mov       [ebp],ebx
  004e4b96  895620                mov       [esi+20h],edx
  004e4b99  894624                mov       [esi+24h],eax

LAB_004e4b9c:
  004e4b9c  33db                  xor       ebx,ebx

LAB_004e4b9e:
  004e4b9e  8b4c2418              mov       ecx,[esp+18h]
  004e4ba2  51                    push      ecx
  004e4ba3  56                    push      esi
  004e4ba4  e8375dfbff            call      0049A8E0h
  004e4ba9  83c408                add       esp,8
  004e4bac  c746200000e7ff        mov       dword [esi+20h],0FFE70000h
  004e4bb3  56                    push      esi
  004e4bb4  e887ed0400            call      00533940h
  004e4bb9  8b9690000000          mov       edx,[esi+90h]
  004e4bbf  8b442430              mov       eax,[esp+30h]
  004e4bc3  83c404                add       esp,4
  004e4bc6  81f200200000          xor       edx,2000h
  004e4bcc  81e2ffffffdf          and       edx,0DFFFFFFFh
  004e4bd2  50                    push      eax
  004e4bd3  56                    push      esi
  004e4bd4  899690000000          mov       [esi+90h],edx
  004e4bda  e811d1fbff            call      004A1CF0h
  004e4bdf  8b442420              mov       eax,[esp+20h]
  004e4be3  83c408                add       esp,8
  004e4be6  3bc3                  cmp       eax,ebx
  004e4be8  0f8407010000          je        near 004E4CF5h
  004e4bee  f70616080000          test      dword [esi],816h
  004e4bf4  0f85fb000000          jne       near 004E4CF5h
  004e4bfa  8a4643                mov       al,[esi+43h]
  004e4bfd  84c0                  test      al,al
  004e4bff  0f85f0000000          jne       near 004E4CF5h
  004e4c05  8b466c                mov       eax,[esi+6Ch]
  004e4c08  8b5e1c                mov       ebx,[esi+1Ch]
  004e4c0b  8b4e24                mov       ecx,[esi+24h]
  004e4c0e  8b781c                mov       edi,[eax+1Ch]
  004e4c11  8b6824                mov       ebp,[eax+24h]
  004e4c14  2bfb                  sub       edi,ebx
  004e4c16  2be9                  sub       ebp,ecx
  004e4c18  c1ff0c                sar       edi,0Ch
  004e4c1b  c1fd0c                sar       ebp,0Ch
  004e4c1e  8bcf                  mov       ecx,edi
  004e4c20  8bd5                  mov       edx,ebp
  004e4c22  0fafcf                imul      ecx,edi
  004e4c25  0fafd5                imul      edx,ebp
  004e4c28  03ca                  add       ecx,edx
  004e4c2a  51                    push      ecx
  004e4c2b  e8e00d0600            call      00545A10h
  004e4c30  55                    push      ebp
  004e4c31  57                    push      edi
  004e4c32  89442418              mov       [esp+18h],eax
  004e4c36  e8e70d0600            call      00545A22h
  004e4c3b  668b4e32              mov       cx,[esi+32h]
  004e4c3f  2bc1                  sub       eax,ecx
  004e4c41  25ff0f0000            and       eax,0FFFh
  004e4c46  89442438              mov       [esp+38h],eax
  004e4c4a  e8f1240600            call      00547140h
  004e4c4f  99                    cdq
  004e4c50  b964000000            mov       ecx,64h
  004e4c55  f7f9                  idiv      ecx
  004e4c57  83fa1e                cmp       edx,1Eh
  004e4c5a  7d56                  jge       short 004E4CB2h
  004e4c5c  8b542438              mov       edx,[esp+38h]
  004e4c60  81c200010000          add       edx,100h
  004e4c66  81e2ff0f0000          and       edx,0FFFh
  004e4c6c  81fa00020000          cmp       edx,200h
  004e4c72  7f2e                  jg        short 004E4CA2h
  004e4c74  817c241090010000      cmp       dword [esp+10h],190h
  004e4c7c  7d24                  jge       short 004E4CA2h
  004e4c7e  8b06                  mov       eax,[esi]
  004e4c80  c7461802000000        mov       dword [esi+18h],2
  004e4c87  25fffffffe            and       eax,0FEFFFFFFh
  004e4c8c  c6464200              mov       byte [esi+42h],0
  004e4c90  0c02                  or        al,2
  004e4c92  66c746100100          mov       word [esi+10h],1
  004e4c98  8906                  mov       [esi],eax
  004e4c9a  5f                    pop       edi
  004e4c9b  5e                    pop       esi
  004e4c9c  5d                    pop       ebp
  004e4c9d  5b                    pop       ebx
  004e4c9e  83c424                add       esp,24h
  004e4ca1  c3                    ret

LAB_004e4ca2:
  004e4ca2  c6464201              mov       byte [esi+42h],1
  004e4ca6  c6464301              mov       byte [esi+43h],1
  004e4caa  5f                    pop       edi
  004e4cab  5e                    pop       esi
  004e4cac  5d                    pop       ebp
  004e4cad  5b                    pop       ebx
  004e4cae  83c424                add       esp,24h
  004e4cb1  c3                    ret

LAB_004e4cb2:
  004e4cb2  83fa46                cmp       edx,46h
  004e4cb5  7d27                  jge       short 004E4CDEh
  004e4cb7  c6464203              mov       byte [esi+42h],3
  004e4cbb  e880240600            call      00547140h
  004e4cc0  99                    cdq
  004e4cc1  b90a000000            mov       ecx,0Ah
  004e4cc6  f7f9                  idiv      ecx
  004e4cc8  02d1                  add       dl,cl
  004e4cca  885643                mov       [esi+43h],dl
  004e4ccd  e86e240600            call      00547140h
  004e4cd2  66894610              mov       [esi+10h],ax
  004e4cd6  5f                    pop       edi
  004e4cd7  5e                    pop       esi
  004e4cd8  5d                    pop       ebp
  004e4cd9  5b                    pop       ebx
  004e4cda  83c424                add       esp,24h
  004e4cdd  c3                    ret

LAB_004e4cde:
  004e4cde  c6464202              mov       byte [esi+42h],2
  004e4ce2  e859240600            call      00547140h
  004e4ce7  99                    cdq
  004e4ce8  b90a000000            mov       ecx,0Ah
  004e4ced  f7f9                  idiv      ecx
  004e4cef  80c214                add       dl,14h
  004e4cf2  885643                mov       [esi+43h],dl

LAB_004e4cf5:
  004e4cf5  5f                    pop       edi
  004e4cf6  5e                    pop       esi
  004e4cf7  5d                    pop       ebp
  004e4cf8  5b                    pop       ebx
  004e4cf9  83c424                add       esp,24h
  004e4cfc  c3                    ret

LAB_004e4cfd:
  004e4cfd  57                    push      edi
  004e4cfe  56                    push      esi
  004e4cff  e87c4e0500            call      00539B80h
  004e4d04  83c408                add       esp,8
  004e4d07  e992feffff            jmp       004E4B9Eh

LAB_004e4d0c:
  004e4d0c  a811                  test      al,11h
  004e4d0e  0f858afeffff          jne       near 004E4B9Eh
  004e4d14  a904080000            test      eax,804h
  004e4d19  0f857ffeffff          jne       near 004E4B9Eh
  004e4d1f  33c9                  xor       ecx,ecx
  004e4d21  8a4e42                mov       cl,[esi+42h]
  004e4d24  83f903                cmp       ecx,3
  004e4d27  0f8771feffff          ja        near 004E4B9Eh
  004e4d2d  ff248d104f4e00        jmp       dword [ecx*4+4E4F10h]
  004e4d34  66395e18              cmp       [esi+18h],bx
  004e4d38  740a                  je        short 004E4D44h
  004e4d3a  0d00000001            or        eax,1000000h
  004e4d3f  895e18                mov       [esi+18h],ebx
  004e4d42  8906                  mov       [esi],eax

LAB_004e4d44:
  004e4d44  817c24102c010000      cmp       dword [esp+10h],12Ch
  004e4d4c  7d09                  jge       short 004E4D57h
  004e4d4e  c6464300              mov       byte [esi+43h],0
  004e4d52  e947feffff            jmp       004E4B9Eh

LAB_004e4d57:
  004e4d57  8a4643                mov       al,[esi+43h]
  004e4d5a  84c0                  test      al,al
  004e4d5c  7474                  je        short 004E4DD2h
  004e4d5e  fec8                  dec       al
  004e4d60  884643                mov       [esi+43h],al
  004e4d63  8b465c                mov       eax,[esi+5Ch]
  004e4d66  8bc8                  mov       ecx,eax
  004e4d68  c1f904                sar       ecx,4
  004e4d6b  2bc1                  sub       eax,ecx
  004e4d6d  89465c                mov       [esi+5Ch],eax
  004e4d70  e929feffff            jmp       004E4B9Eh
  004e4d75  66395e18              cmp       [esi+18h],bx
  004e4d79  740a                  je        short 004E4D85h
  004e4d7b  0d00000001            or        eax,1000000h
  004e4d80  895e18                mov       [esi+18h],ebx
  004e4d83  8906                  mov       [esi],eax

LAB_004e4d85:
  004e4d85  8b542414              mov       edx,[esp+14h]
  004e4d89  8b4c2438              mov       ecx,[esp+38h]
  004e4d8d  8d841280000000        lea       eax,[edx+edx+80h]
  004e4d94  8d1408                lea       edx,[eax+ecx]
  004e4d97  8d3c00                lea       edi,[eax+eax]
  004e4d9a  81e2ff0f0000          and       edx,0FFFh
  004e4da0  3bd7                  cmp       edx,edi
  004e4da2  7f1a                  jg        short 004E4DBEh
  004e4da4  8b465c                mov       eax,[esi+5Ch]
  004e4da7  66014e32              add       [esi+32h],cx
  004e4dab  8bc8                  mov       ecx,eax
  004e4dad  c6464300              mov       byte [esi+43h],0
  004e4db1  c1f904                sar       ecx,4
  004e4db4  2bc1                  sub       eax,ecx
  004e4db6  89465c                mov       [esi+5Ch],eax
  004e4db9  e9e0fdffff            jmp       004E4B9Eh

LAB_004e4dbe:
  004e4dbe  81e1ff0f0000          and       ecx,0FFFh
  004e4dc4  81f900080000          cmp       ecx,800h
  004e4dca  7c02                  jl        short 004E4DCEh
  004e4dcc  f7d8                  neg       eax

LAB_004e4dce:
  004e4dce  66014632              add       [esi+32h],ax

LAB_004e4dd2:
  004e4dd2  8b465c                mov       eax,[esi+5Ch]
  004e4dd5  8bc8                  mov       ecx,eax
  004e4dd7  c1f904                sar       ecx,4
  004e4dda  2bc1                  sub       eax,ecx
  004e4ddc  89465c                mov       [esi+5Ch],eax
  004e4ddf  e9bafdffff            jmp       004E4B9Eh
  004e4de4  66395e18              cmp       [esi+18h],bx
  004e4de8  740a                  je        short 004E4DF4h
  004e4dea  0d00000001            or        eax,1000000h
  004e4def  895e18                mov       [esi+18h],ebx
  004e4df2  8906                  mov       [esi],eax

LAB_004e4df4:
  004e4df4  817c24102c010000      cmp       dword [esp+10h],12Ch
  004e4dfc  7d09                  jge       short 004E4E07h
  004e4dfe  c6464300              mov       byte [esi+43h],0
  004e4e02  e997fdffff            jmp       004E4B9Eh

LAB_004e4e07:
  004e4e07  8a4643                mov       al,[esi+43h]
  004e4e0a  84c0                  test      al,al
  004e4e0c  7405                  je        short 004E4E13h
  004e4e0e  fec8                  dec       al
  004e4e10  884643                mov       [esi+43h],al

LAB_004e4e13:
  004e4e13  8b442414              mov       eax,[esp+14h]
  004e4e17  8b4c2438              mov       ecx,[esp+38h]
  004e4e1b  c1f802                sar       eax,2
  004e4e1e  83c038                add       eax,38h
  004e4e21  8d1408                lea       edx,[eax+ecx]
  004e4e24  8d3c00                lea       edi,[eax+eax]
  004e4e27  81e2ff0f0000          and       edx,0FFFh
  004e4e2d  3bd7                  cmp       edx,edi
  004e4e2f  7f06                  jg        short 004E4E37h
  004e4e31  66014e32              add       [esi+32h],cx
  004e4e35  eb14                  jmp       short 004E4E4Bh

LAB_004e4e37:
  004e4e37  81e1ff0f0000          and       ecx,0FFFh
  004e4e3d  81f900080000          cmp       ecx,800h
  004e4e43  7c02                  jl        short 004E4E47h
  004e4e45  f7d8                  neg       eax

LAB_004e4e47:
  004e4e47  66014632              add       [esi+32h],ax

LAB_004e4e4b:
  004e4e4b  8b465c                mov       eax,[esi+5Ch]
  004e4e4e  b900000100            mov       ecx,10000h
  004e4e53  2bc8                  sub       ecx,eax
  004e4e55  c1f904                sar       ecx,4
  004e4e58  03c8                  add       ecx,eax
  004e4e5a  894e5c                mov       [esi+5Ch],ecx
  004e4e5d  e93cfdffff            jmp       004E4B9Eh
  004e4e62  66395e18              cmp       [esi+18h],bx
  004e4e66  740a                  je        short 004E4E72h
  004e4e68  0d00000001            or        eax,1000000h
  004e4e6d  895e18                mov       [esi+18h],ebx
  004e4e70  8906                  mov       [esi],eax

LAB_004e4e72:
  004e4e72  8a4643                mov       al,[esi+43h]
  004e4e75  84c0                  test      al,al
  004e4e77  7405                  je        short 004E4E7Eh
  004e4e79  fec8                  dec       al
  004e4e7b  884643                mov       [esi+43h],al

LAB_004e4e7e:
  004e4e7e  0fbf5610              movsx     edx,word [esi+10h]
  004e4e82  52                    push      edx
  004e4e83  c7465c00300000        mov       dword [esi+5Ch],3000h
  004e4e8a  e88d0b0600            call      00545A1Ch
  004e4e8f  0faf465c              imul      eax,[esi+5Ch]
  004e4e93  8b5650                mov       edx,[esi+50h]
  004e4e96  c1f80c                sar       eax,0Ch
  004e4e99  03d0                  add       edx,eax
  004e4e9b  0fbf4610              movsx     eax,word [esi+10h]
  004e4e9f  50                    push      eax
  004e4ea0  895650                mov       [esi+50h],edx
  004e4ea3  e86e0b0600            call      00545A16h
  004e4ea8  0faf465c              imul      eax,[esi+5Ch]
  004e4eac  8b7e58                mov       edi,[esi+58h]
  004e4eaf  668b5632              mov       dx,[esi+32h]
  004e4eb3  668b4e10              mov       cx,[esi+10h]
  004e4eb7  895e5c                mov       [esi+5Ch],ebx
  004e4eba  c1f80c                sar       eax,0Ch
  004e4ebd  03f8                  add       edi,eax
  004e4ebf  8b442414              mov       eax,[esp+14h]
  004e4ec3  c1f802                sar       eax,2
  004e4ec6  2bca                  sub       ecx,edx
  004e4ec8  83c040                add       eax,40h
  004e4ecb  81e1ff0f0000          and       ecx,0FFFh
  004e4ed1  897e58                mov       [esi+58h],edi
  004e4ed4  8d2c00                lea       ebp,[eax+eax]
  004e4ed7  894c2438              mov       [esp+38h],ecx
  004e4edb  8d3c08                lea       edi,[eax+ecx]
  004e4ede  81e7ff0f0000          and       edi,0FFFh
  004e4ee4  3bfd                  cmp       edi,ebp
  004e4ee6  7f0b                  jg        short 004E4EF3h
  004e4ee8  03ca                  add       ecx,edx
  004e4eea  66894e32              mov       [esi+32h],cx
  004e4eee  e9abfcffff            jmp       004E4B9Eh

LAB_004e4ef3:
  004e4ef3  81e1ff0f0000          and       ecx,0FFFh
  004e4ef9  81f900080000          cmp       ecx,800h
  004e4eff  7c02                  jl        short 004E4F03h
  004e4f01  f7d8                  neg       eax

LAB_004e4f03:
  004e4f03  03d0                  add       edx,eax
  004e4f05  66895632              mov       [esi+32h],dx
  004e4f09  e990fcffff            jmp       004E4B9Eh
  004e4f0e  8bff                  mov       edi,edi
  004e4f10  344d                  xor       al,4Dh
  004e4f12  4e                    dec       esi
  004e4f13  00754d                add       [ebp+4Dh],dh
  004e4f16  4e                    dec       esi
  004e4f17  00e4                  add       ah,ah
  004e4f19  4d                    dec       ebp
  004e4f1a  4e                    dec       esi
  004e4f1b  00624e                add       [edx+4Eh],ah
  004e4f1e  4e                    dec       esi
  004e4f1f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
