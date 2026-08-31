Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004828f0  section .text  offset fichier 0x81cf0  1260 octets

  004828f0  83ec1c                sub       esp,1Ch
  004828f3  53                    push      ebx
  004828f4  55                    push      ebp
  004828f5  56                    push      esi
  004828f6  8b74242c              mov       esi,[esp+2Ch]
  004828fa  33db                  xor       ebx,ebx
  004828fc  57                    push      edi
  004828fd  8b0e                  mov       ecx,[esi]
  004828ff  8b4674                mov       eax,[esi+74h]
  00482902  f7c100000002          test      ecx,2000000h
  00482908  66895c2414            mov       [esp+14h],bx
  0048290d  66c744241600ff        mov       word [esp+16h],0FF00h
  00482914  66c74424180001        mov       word [esp+18h],100h
  0048291b  66c744241a2c01        mov       word [esp+1Ah],12Ch
  00482922  66895c241c            mov       [esp+1Ch],bx
  00482927  66c744241e0002        mov       word [esp+1Eh],200h
  0048292e  89442430              mov       [esp+30h],eax
  00482932  741d                  je        short 00482951h
  00482934  f7c100000001          test      ecx,1000000h
  0048293a  0f8453040000          je        near 00482D93h
  00482940  891e                  mov       [esi],ebx
  00482942  8b08                  mov       ecx,[eax]
  00482944  80e17f                and       cl,7Fh
  00482947  8908                  mov       [eax],ecx
  00482949  5f                    pop       edi
  0048294a  5e                    pop       esi
  0048294b  5d                    pop       ebp
  0048294c  5b                    pop       ebx
  0048294d  83c41c                add       esp,1Ch
  00482950  c3                    ret

LAB_00482951:
  00482951  66395e10              cmp       [esi+10h],bx
  00482955  0f85d7000000          jne       near 00482A32h
  0048295b  8b08                  mov       ecx,[eax]
  0048295d  50                    push      eax
  0048295e  80c980                or        cl,80h
  00482961  8908                  mov       [eax],ecx
  00482963  668b4e36              mov       cx,[esi+36h]
  00482967  8b2e                  mov       ebp,[esi]
  00482969  83e1fe                and       ecx,0FFFFFFFEh
  0048296c  81cd80000000          or        ebp,80h
  00482972  83c906                or        ecx,6
  00482975  892e                  mov       [esi],ebp
  00482977  66894e36              mov       [esi+36h],cx
  0048297b  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  00482981  e83a620b00            call      00538BC0h
  00482986  8b4e20                mov       ecx,[esi+20h]
  00482989  83c404                add       esp,4
  0048298c  24fe                  and       al,0FEh
  0048298e  895e38                mov       [esi+38h],ebx
  00482991  c1e00b                shl       eax,0Bh
  00482994  6a07                  push      7
  00482996  53                    push      ebx
  00482997  03c8                  add       ecx,eax
  00482999  6a79                  push      79h
  0048299b  56                    push      esi
  0048299c  894e20                mov       [esi+20h],ecx
  0048299f  895e28                mov       [esi+28h],ebx
  004829a2  66895e2c              mov       [esi+2Ch],bx
  004829a6  66c746120018          mov       word [esi+12h],1800h
  004829ac  66895e14              mov       [esi+14h],bx
  004829b0  66895e16              mov       [esi+16h],bx
  004829b4  c6460402              mov       byte [esi+4],2
  004829b8  e82381ffff            call      0047AAE0h
  004829bd  83c410                add       esp,10h
  004829c0  66c746120020          mov       word [esi+12h],2000h
  004829c6  66895e14              mov       [esi+14h],bx
  004829ca  c6460430              mov       byte [esi+4],30h
  004829ce  33ff                  xor       edi,edi
  004829d0  bd08000000            mov       ebp,8

LAB_004829d5:
  004829d5  e866470c00            call      00547140h
  004829da  83e01f                and       eax,1Fh
  004829dd  0596000000            add       eax,96h
  004829e2  66894616              mov       [esi+16h],ax
  004829e6  e855470c00            call      00547140h
  004829eb  83e03f                and       eax,3Fh
  004829ee  8d5438e0              lea       edx,[eax+edi-20h]
  004829f2  66895632              mov       [esi+32h],dx
  004829f6  e845470c00            call      00547140h
  004829fb  25ff010000            and       eax,1FFh
  00482a00  6a01                  push      1
  00482a02  53                    push      ebx
  00482a03  2d00010000            sub       eax,100h
  00482a08  6a79                  push      79h
  00482a0a  56                    push      esi
  00482a0b  66894634              mov       [esi+34h],ax
  00482a0f  e8cc80ffff            call      0047AAE0h
  00482a14  83c410                add       esp,10h
  00482a17  81c700020000          add       edi,200h
  00482a1d  4d                    dec       ebp
  00482a1e  75b5                  jne       short 004829D5h
  00482a20  8b442430              mov       eax,[esp+30h]
  00482a24  668b4832              mov       cx,[eax+32h]
  00482a28  66c746161900          mov       word [esi+16h],19h
  00482a2e  66894e32              mov       [esi+32h],cx

LAB_00482a32:
  00482a32  0fbf7e32              movsx     edi,word [esi+32h]
  00482a36  0fbf5e34              movsx     ebx,word [esi+34h]
  00482a3a  e801470c00            call      00547140h
  00482a3f  83e07f                and       eax,7Fh
  00482a42  c7462800300030        mov       dword [esi+28h],30003000h
  00482a49  0520030000            add       eax,320h
  00482a4e  c7463800000000        mov       dword [esi+38h],0
  00482a55  66894612              mov       [esi+12h],ax
  00482a59  66c7461a0300          mov       word [esi+1Ah],3
  00482a5f  e8dc460c00            call      00547140h
  00482a64  66894632              mov       [esi+32h],ax
  00482a68  e8d3460c00            call      00547140h
  00482a6d  6a07                  push      7
  00482a6f  6a00                  push      0
  00482a71  25ff010000            and       eax,1FFh
  00482a76  8d6e1c                lea       ebp,[esi+1Ch]
  00482a79  6a50                  push      50h
  00482a7b  68a0195500            push      5519A0h
  00482a80  2d00010000            sub       eax,100h
  00482a85  55                    push      ebp
  00482a86  56                    push      esi
  00482a87  66894634              mov       [esi+34h],ax
  00482a8b  e8b081ffff            call      0047AC40h
  00482a90  668b4616              mov       ax,[esi+16h]
  00482a94  83c418                add       esp,18h
  00482a97  6685c0                test      ax,ax
  00482a9a  66897e32              mov       [esi+32h],di
  00482a9e  66895e34              mov       [esi+34h],bx
  00482aa2  740a                  je        short 00482AAEh
  00482aa4  48                    dec       eax
  00482aa5  66894616              mov       [esi+16h],ax
  00482aa9  e9a5010000            jmp       00482C53h

LAB_00482aae:
  00482aae  66817e10b400          cmp       word [esi+10h],0B4h
  00482ab4  7d7a                  jge       short 00482B30h
  00482ab6  e885460c00            call      00547140h
  00482abb  a80f                  test      al,0Fh
  00482abd  7571                  jne       short 00482B30h
  00482abf  0fbf7e32              movsx     edi,word [esi+32h]
  00482ac3  0fbf5e34              movsx     ebx,word [esi+34h]
  00482ac7  c7462800600060        mov       dword [esi+28h],60006000h
  00482ace  66c7462c0060          mov       word [esi+2Ch],6000h
  00482ad4  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00482adb  e860460c00            call      00547140h
  00482ae0  66894632              mov       [esi+32h],ax
  00482ae4  e857460c00            call      00547140h
  00482ae9  6a07                  push      7
  00482aeb  6a00                  push      0
  00482aed  6a7a                  push      7Ah
  00482aef  56                    push      esi
  00482af0  66894634              mov       [esi+34h],ax
  00482af4  c646041d              mov       byte [esi+4],1Dh
  00482af8  e8e37fffff            call      0047AAE0h
  00482afd  83c410                add       esp,10h
  00482b00  66897e32              mov       [esi+32h],di
  00482b04  66895e34              mov       [esi+34h],bx
  00482b08  55                    push      ebp
  00482b09  6800120000            push      1200h
  00482b0e  6800080000            push      800h
  00482b13  6a0f                  push      0Fh
  00482b15  e806b00b00            call      0053DB20h
  00482b1a  83c410                add       esp,10h
  00482b1d  c705a0935e00ff00ff00  mov       dword [5E93A0h],0FF00FFh
  00482b27  66c705a4935e00ff00    mov       word [5E93A4h],0FFh

LAB_00482b30:
  00482b30  668b4610              mov       ax,[esi+10h]
  00482b34  663dc800              cmp       ax,0C8h
  00482b38  0f8dd7000000          jge       near 00482C15h
  00482b3e  a803                  test      al,3
  00482b40  0f85cf000000          jne       near 00482C15h
  00482b46  8b5500                mov       edx,[ebp]
  00482b49  8b4620                mov       eax,[esi+20h]
  00482b4c  8b4e24                mov       ecx,[esi+24h]
  00482b4f  89542420              mov       [esp+20h],edx
  00482b53  0fbf5632              movsx     edx,word [esi+32h]
  00482b57  89442424              mov       [esp+24h],eax
  00482b5b  894c2428              mov       [esp+28h],ecx
  00482b5f  0fbf4634              movsx     eax,word [esi+34h]
  00482b63  89542430              mov       [esp+30h],edx
  00482b67  89442410              mov       [esp+10h],eax
  00482b6b  e8d0450c00            call      00547140h
  00482b70  8bd8                  mov       ebx,eax
  00482b72  e8c9450c00            call      00547140h
  00482b77  8bf8                  mov       edi,eax
  00482b79  53                    push      ebx
  00482b7a  81e7ff000000          and       edi,0FFh
  00482b80  81c700010000          add       edi,100h
  00482b86  e8912e0c00            call      00545A1Ch
  00482b8b  0fafc7                imul      eax,edi
  00482b8e  8b4d00                mov       ecx,[ebp]
  00482b91  53                    push      ebx
  00482b92  03c8                  add       ecx,eax
  00482b94  894d00                mov       [ebp],ecx
  00482b97  8b4e60                mov       ecx,[esi+60h]
  00482b9a  894e20                mov       [esi+20h],ecx
  00482b9d  e8742e0c00            call      00545A16h
  00482ba2  0fafc7                imul      eax,edi
  00482ba5  8b7e24                mov       edi,[esi+24h]
  00482ba8  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00482baf  03f8                  add       edi,eax
  00482bb1  c7462800500050        mov       dword [esi+28h],50005000h
  00482bb8  897e24                mov       [esi+24h],edi
  00482bbb  66c7462c0050          mov       word [esi+2Ch],5000h
  00482bc1  e87a450c00            call      00547140h
  00482bc6  66894632              mov       [esi+32h],ax
  00482bca  66c746340004          mov       word [esi+34h],400h
  00482bd0  e86b450c00            call      00547140h
  00482bd5  2403                  and       al,3
  00482bd7  6a07                  push      7
  00482bd9  6a00                  push      0
  00482bdb  0408                  add       al,8
  00482bdd  6883000000            push      83h
  00482be2  56                    push      esi
  00482be3  884604                mov       [esi+4],al
  00482be6  e8f57effff            call      0047AAE0h
  00482beb  668b542440            mov       dx,[esp+40h]
  00482bf0  668b442420            mov       ax,[esp+20h]
  00482bf5  8b4c2430              mov       ecx,[esp+30h]
  00482bf9  66895632              mov       [esi+32h],dx
  00482bfd  8b542434              mov       edx,[esp+34h]
  00482c01  66894634              mov       [esi+34h],ax
  00482c05  8b442438              mov       eax,[esp+38h]
  00482c09  83c410                add       esp,10h
  00482c0c  894d00                mov       [ebp],ecx
  00482c0f  895620                mov       [esi+20h],edx
  00482c12  894624                mov       [esi+24h],eax

LAB_00482c15:
  00482c15  8d4c2414              lea       ecx,[esp+14h]
  00482c19  51                    push      ecx
  00482c1a  56                    push      esi
  00482c1b  e88020ffff            call      00474CA0h
  00482c20  8bf8                  mov       edi,eax
  00482c22  83c408                add       esp,8
  00482c25  85ff                  test      edi,edi
  00482c27  742a                  je        short 00482C53h
  00482c29  8b4e74                mov       ecx,[esi+74h]
  00482c2c  33d2                  xor       edx,edx
  00482c2e  8a5606                mov       dl,[esi+6]
  00482c31  33c0                  xor       eax,eax
  00482c33  8a4607                mov       al,[esi+7]
  00482c36  6a00                  push      0
  00482c38  52                    push      edx
  00482c39  50                    push      eax
  00482c3a  57                    push      edi
  00482c3b  51                    push      ecx
  00482c3c  e86f760b00            call      0053A2B0h
  00482c41  8b07                  mov       eax,[edi]
  00482c43  83c414                add       esp,14h
  00482c46  0c40                  or        al,40h
  00482c48  66c787960000001000    mov       word [edi+96h],10h
  00482c51  8907                  mov       [edi],eax

LAB_00482c53:
  00482c53  0fbf7e32              movsx     edi,word [esi+32h]
  00482c57  0fbf5e16              movsx     ebx,word [esi+16h]
  00482c5b  57                    push      edi
  00482c5c  e8bb2d0c00            call      00545A1Ch
  00482c61  0fafc3                imul      eax,ebx
  00482c64  8b4d00                mov       ecx,[ebp]
  00482c67  57                    push      edi
  00482c68  03c8                  add       ecx,eax
  00482c6a  894d00                mov       [ebp],ecx
  00482c6d  e8a42d0c00            call      00545A16h
  00482c72  0fafc3                imul      eax,ebx
  00482c75  8b7e24                mov       edi,[esi+24h]
  00482c78  56                    push      esi
  00482c79  03f8                  add       edi,eax
  00482c7b  897e24                mov       [esi+24h],edi
  00482c7e  8b7e20                mov       edi,[esi+20h]
  00482c81  e8ba0c0b00            call      00533940h
  00482c86  8b8690000000          mov       eax,[esi+90h]
  00482c8c  83c404                add       esp,4
  00482c8f  a902880000            test      eax,8802h
  00482c94  897e20                mov       [esi+20h],edi
  00482c97  7406                  je        short 00482C9Fh
  00482c99  66c746160000          mov       word [esi+16h],0

LAB_00482c9f:
  00482c9f  f70600008000          test      dword [esi],800000h
  00482ca5  740c                  je        short 00482CB3h
  00482ca7  b820000000            mov       eax,20h
  00482cac  bda0ca5e00            mov       ebp,5ECAA0h
  00482cb1  eb0a                  jmp       short 00482CBDh

LAB_00482cb3:
  00482cb3  a120905e00            mov       eax,[5E9020h]
  00482cb8  bdc0935e00            mov       ebp,5E93C0h

LAB_00482cbd:
  00482cbd  85c0                  test      eax,eax
  00482cbf  747e                  je        short 00482D3Fh
  00482cc1  89442430              mov       [esp+30h],eax

LAB_00482cc5:
  00482cc5  f7450000000080        test      dword [ebp],80000000h
  00482ccc  7460                  je        short 00482D2Eh
  00482cce  8b7d1c                mov       edi,[ebp+1Ch]
  00482cd1  8b561c                mov       edx,[esi+1Ch]
  00482cd4  8b5d24                mov       ebx,[ebp+24h]
  00482cd7  8b4624                mov       eax,[esi+24h]
  00482cda  2bfa                  sub       edi,edx
  00482cdc  2bd8                  sub       ebx,eax
  00482cde  c1ff0c                sar       edi,0Ch
  00482ce1  c1fb0c                sar       ebx,0Ch
  00482ce4  8bd7                  mov       edx,edi
  00482ce6  8bc3                  mov       eax,ebx
  00482ce8  0fafd7                imul      edx,edi
  00482ceb  0fafc3                imul      eax,ebx
  00482cee  03d0                  add       edx,eax
  00482cf0  52                    push      edx
  00482cf1  e81a2d0c00            call      00545A10h
  00482cf6  3d20030000            cmp       eax,320h
  00482cfb  7d31                  jge       short 00482D2Eh
  00482cfd  53                    push      ebx
  00482cfe  57                    push      edi
  00482cff  e81e2d0c00            call      00545A22h
  00482d04  8bf8                  mov       edi,eax
  00482d06  81ef00060000          sub       edi,600h
  00482d0c  57                    push      edi
  00482d0d  e80a2d0c00            call      00545A1Ch
  00482d12  8b4d50                mov       ecx,[ebp+50h]
  00482d15  57                    push      edi
  00482d16  c1e002                shl       eax,2
  00482d19  03c8                  add       ecx,eax
  00482d1b  894d50                mov       [ebp+50h],ecx
  00482d1e  e8f32c0c00            call      00545A16h
  00482d23  8b4d58                mov       ecx,[ebp+58h]
  00482d26  c1e002                shl       eax,2
  00482d29  03c8                  add       ecx,eax
  00482d2b  894d58                mov       [ebp+58h],ecx

LAB_00482d2e:
  00482d2e  8b442430              mov       eax,[esp+30h]
  00482d32  81c59c000000          add       ebp,9Ch
  00482d38  48                    dec       eax
  00482d39  89442430              mov       [esp+30h],eax
  00482d3d  7586                  jne       short 00482CC5h

LAB_00482d3f:
  00482d3f  e8fc430c00            call      00547140h
  00482d44  83e00f                and       eax,0Fh
  00482d47  c70560915e0040005000  mov       dword [5E9160h],500040h
  00482d51  83e808                sub       eax,8
  00482d54  c70564915e0060000200  mov       dword [5E9164h],20060h
  00482d5e  a3ace05d00            mov       [5DE0ACh],eax
  00482d63  66817e10c800          cmp       word [esi+10h],0C8h
  00482d69  7d13                  jge       short 00482D7Eh
  00482d6b  f70600000040          test      dword [esi],40000000h
  00482d71  750b                  jne       short 00482D7Eh
  00482d73  8b4e74                mov       ecx,[esi+74h]
  00482d76  f70100000040          test      dword [ecx],40000000h
  00482d7c  7415                  je        short 00482D93h

LAB_00482d7e:
  00482d7e  8b06                  mov       eax,[esi]
  00482d80  0d00000002            or        eax,2000000h
  00482d85  8906                  mov       [esi],eax
  00482d87  33c0                  xor       eax,eax
  00482d89  66894610              mov       [esi+10h],ax
  00482d8d  66a366915e00          mov       [5E9166h],ax

LAB_00482d93:
  00482d93  0fbf4610              movsx     eax,word [esi+10h]
  00482d97  99                    cdq
  00482d98  b90e000000            mov       ecx,0Eh
  00482d9d  f7f9                  idiv      ecx
  00482d9f  85d2                  test      edx,edx
  00482da1  752d                  jne       short 00482DD0h
  00482da3  8d7e1c                lea       edi,[esi+1Ch]
  00482da6  57                    push      edi
  00482da7  6800120000            push      1200h
  00482dac  6800040000            push      400h
  00482db1  6a23                  push      23h
  00482db3  e868ad0b00            call      0053DB20h
  00482db8  83c410                add       esp,10h
  00482dbb  57                    push      edi
  00482dbc  6800120000            push      1200h
  00482dc1  6800020000            push      200h
  00482dc6  6a2d                  push      2Dh
  00482dc8  e853ad0b00            call      0053DB20h
  00482dcd  83c410                add       esp,10h

LAB_00482dd0:
  00482dd0  66ff4610              inc       word [esi+10h]
  00482dd4  5f                    pop       edi
  00482dd5  5e                    pop       esi
  00482dd6  5d                    pop       ebp
  00482dd7  5b                    pop       ebx
  00482dd8  83c41c                add       esp,1Ch
  00482ddb  c3                    ret

16 cible(s) de saut interne : 0x00482951, 0x004829d5, 0x00482a32, 0x00482aae, 0x00482b30, 0x00482c15, 0x00482c53, 0x00482c9f, 0x00482cb3, 0x00482cbd, 0x00482cc5, 0x00482d2e, 0x00482d3f, 0x00482d7e, 0x00482d93, 0x00482dd0
