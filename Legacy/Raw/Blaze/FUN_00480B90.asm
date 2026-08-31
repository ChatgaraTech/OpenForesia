Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00480b90  section .text  offset fichier 0x7ff90  688 octets

  00480b90  83ec10                sub       esp,10h
  00480b93  53                    push      ebx
  00480b94  55                    push      ebp
  00480b95  56                    push      esi
  00480b96  8b742420              mov       esi,[esp+20h]
  00480b9a  57                    push      edi
  00480b9b  8b0e                  mov       ecx,[esi]
  00480b9d  8b4674                mov       eax,[esi+74h]
  00480ba0  f7c100000002          test      ecx,2000000h
  00480ba6  89442410              mov       [esp+10h],eax
  00480baa  7423                  je        short 00480BCFh
  00480bac  8b10                  mov       edx,[eax]
  00480bae  80e27f                and       dl,7Fh
  00480bb1  8910                  mov       [eax],edx
  00480bb3  8b4678                mov       eax,[esi+78h]
  00480bb6  85c0                  test      eax,eax
  00480bb8  7407                  je        short 00480BC1h
  00480bba  c7407400000000        mov       dword [eax+74h],0

LAB_00480bc1:
  00480bc1  c70600000000          mov       dword [esi],0
  00480bc7  5f                    pop       edi
  00480bc8  5e                    pop       esi
  00480bc9  5d                    pop       ebp
  00480bca  5b                    pop       ebx
  00480bcb  83c410                add       esp,10h
  00480bce  c3                    ret

LAB_00480bcf:
  00480bcf  f6c102                test      cl,2
  00480bd2  0f84e9000000          je        near 00480CC1h
  00480bd8  668b5614              mov       dx,[esi+14h]
  00480bdc  33ff                  xor       edi,edi
  00480bde  663bd7                cmp       dx,di
  00480be1  0f84a3000000          je        near 00480C8Ah
  00480be7  66397e10              cmp       [esi+10h],di
  00480beb  7546                  jne       short 00480C33h
  00480bed  6683fa0c              cmp       dx,0Ch
  00480bf1  7540                  jne       short 00480C33h
  00480bf3  66c746100100          mov       word [esi+10h],1
  00480bf9  897e28                mov       [esi+28h],edi
  00480bfc  66897e2c              mov       [esi+2Ch],di
  00480c00  897e38                mov       [esi+38h],edi
  00480c03  8b481c                mov       ecx,[eax+1Ch]
  00480c06  6aff                  push      0FFFFFFFFh
  00480c08  894e1c                mov       [esi+1Ch],ecx
  00480c0b  8b5024                mov       edx,[eax+24h]
  00480c0e  33c9                  xor       ecx,ecx
  00480c10  895624                mov       [esi+24h],edx
  00480c13  8a4e0b                mov       cl,[esi+0Bh]
  00480c16  668b4032              mov       ax,[eax+32h]
  00480c1a  51                    push      ecx
  00480c1b  6888000000            push      88h
  00480c20  56                    push      esi
  00480c21  66894632              mov       [esi+32h],ax
  00480c25  66c746340004          mov       word [esi+34h],400h
  00480c2b  e8b09effff            call      0047AAE0h
  00480c30  83c410                add       esp,10h

LAB_00480c33:
  00480c33  8b0e                  mov       ecx,[esi]
  00480c35  f7c100004000          test      ecx,400000h
  00480c3b  744d                  je        short 00480C8Ah
  00480c3d  8b4678                mov       eax,[esi+78h]
  00480c40  f70000000100          test      dword [eax],10000h
  00480c46  7409                  je        short 00480C51h
  00480c48  33c0                  xor       eax,eax
  00480c4a  a038e55d00            mov       al,[5DE538h]
  00480c4f  eb07                  jmp       short 00480C58h

LAB_00480c51:
  00480c51  33d2                  xor       edx,edx
  00480c53  8a5008                mov       dl,[eax+8]
  00480c56  8bc2                  mov       eax,edx

LAB_00480c58:
  00480c58  8d14c0                lea       edx,[eax+eax*8]
  00480c5b  8d0450                lea       eax,[eax+edx*2]
  00480c5e  33d2                  xor       edx,edx
  00480c60  8a148588915e00        mov       dl,[eax*4+5E9188h]
  00480c67  c1e204                shl       edx,4
  00480c6a  f682224a6000f0        test      byte [edx+604A22h],0F0h
  00480c71  7417                  je        short 00480C8Ah
  00480c73  0fbf4614              movsx     eax,word [esi+14h]
  00480c77  668b5612              mov       dx,[esi+12h]
  00480c7b  0fbfda                movsx     ebx,dx
  00480c7e  8d0480                lea       eax,[eax+eax*4]
  00480c81  3bd8                  cmp       ebx,eax
  00480c83  7d05                  jge       short 00480C8Ah
  00480c85  42                    inc       edx
  00480c86  66895612              mov       [esi+12h],dx

LAB_00480c8a:
  00480c8a  8b4678                mov       eax,[esi+78h]
  00480c8d  3bc7                  cmp       eax,edi
  00480c8f  7408                  je        short 00480C99h
  00480c91  f70000000050          test      dword [eax],50000000h
  00480c97  750b                  jne       short 00480CA4h

LAB_00480c99:
  00480c99  8b5674                mov       edx,[esi+74h]
  00480c9c  f70200000040          test      dword [edx],40000000h
  00480ca2  7405                  je        short 00480CA9h

LAB_00480ca4:
  00480ca4  83c904                or        ecx,4
  00480ca7  890e                  mov       [esi],ecx

LAB_00480ca9:
  00480ca9  66397e16              cmp       [esi+16h],di
  00480cad  0f8585010000          jne       near 00480E38h
  00480cb3  810e00000002          or        dword [esi],2000000h
  00480cb9  5f                    pop       edi
  00480cba  5e                    pop       esi
  00480cbb  5d                    pop       ebp
  00480cbc  5b                    pop       ebx
  00480cbd  83c410                add       esp,10h
  00480cc0  c3                    ret

LAB_00480cc1:
  00480cc1  33ff                  xor       edi,edi
  00480cc3  66397e10              cmp       [esi+10h],di
  00480cc7  0f8519010000          jne       near 00480DE6h
  00480ccd  803da00760006c        cmp       byte [6007A0h],6Ch
  00480cd4  760a                  jbe       short 00480CE0h
  00480cd6  893e                  mov       [esi],edi
  00480cd8  5f                    pop       edi
  00480cd9  5e                    pop       esi
  00480cda  5d                    pop       ebp
  00480cdb  5b                    pop       ebx
  00480cdc  83c410                add       esp,10h
  00480cdf  c3                    ret

LAB_00480ce0:
  00480ce0  8b08                  mov       ecx,[eax]
  00480ce2  50                    push      eax
  00480ce3  80c980                or        cl,80h
  00480ce6  8908                  mov       [eax],ecx
  00480ce8  804e3606              or        byte [esi+36h],6
  00480cec  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  00480cf2  897e78                mov       [esi+78h],edi
  00480cf5  e8c67e0b00            call      00538BC0h
  00480cfa  8b5e20                mov       ebx,[esi+20h]
  00480cfd  6681463400ff          add       word [esi+34h],0FF00h
  00480d03  8be8                  mov       ebp,eax
  00480d05  8b4e1c                mov       ecx,[esi+1Ch]
  00480d08  8b5624                mov       edx,[esi+24h]
  00480d0b  894c2418              mov       [esp+18h],ecx
  00480d0f  8d451e                lea       eax,[ebp+1Eh]
  00480d12  b901000000            mov       ecx,1
  00480d17  c1e00c                shl       eax,0Ch
  00480d1a  03d8                  add       ebx,eax
  00480d1c  668b4634              mov       ax,[esi+34h]
  00480d20  0fbfc0                movsx     eax,ax
  00480d23  83c404                add       esp,4
  00480d26  668146320004          add       word [esi+32h],400h
  00480d2c  895e20                mov       [esi+20h],ebx
  00480d2f  897e38                mov       [esi+38h],edi
  00480d32  897e28                mov       [esi+28h],edi
  00480d35  66897e2c              mov       [esi+2Ch],di
  00480d39  8954241c              mov       [esp+1Ch],edx
  00480d3d  66894e12              mov       [esi+12h],cx
  00480d41  66894e14              mov       [esi+14h],cx
  00480d45  89442424              mov       [esp+24h],eax
  00480d49  c6460414              mov       byte [esi+4],14h
  00480d4d  bb0c000000            mov       ebx,0Ch

LAB_00480d52:
  00480d52  0fbf7e32              movsx     edi,word [esi+32h]
  00480d56  81ef00040000          sub       edi,400h
  00480d5c  57                    push      edi
  00480d5d  e8ba4c0c00            call      00545A1Ch
  00480d62  0fafc5                imul      eax,ebp
  00480d65  8b4c2414              mov       ecx,[esp+14h]
  00480d69  57                    push      edi
  00480d6a  03c1                  add       eax,ecx
  00480d6c  89461c                mov       [esi+1Ch],eax
  00480d6f  e8a24c0c00            call      00545A16h
  00480d74  0fafc5                imul      eax,ebp
  00480d77  8b4c241c              mov       ecx,[esp+1Ch]
  00480d7b  6880000000            push      80h
  00480d80  03c1                  add       eax,ecx
  00480d82  8b4c2428              mov       ecx,[esp+28h]
  00480d86  894624                mov       [esi+24h],eax
  00480d89  51                    push      ecx
  00480d8a  8d4650                lea       eax,[esi+50h]
  00480d8d  57                    push      edi
  00480d8e  50                    push      eax
  00480d8f  e83c1d0b00            call      00532AD0h
  00480d94  83c410                add       esp,10h
  00480d97  33d2                  xor       edx,edx
  00480d99  8a560b                mov       dl,[esi+0Bh]
  00480d9c  6aff                  push      0FFFFFFFFh
  00480d9e  52                    push      edx
  00480d9f  6887000000            push      87h
  00480da4  56                    push      esi
  00480da5  e8369dffff            call      0047AAE0h
  00480daa  668146325501          add       word [esi+32h],155h
  00480db0  6683461202            add       word [esi+12h],2
  00480db5  83c410                add       esp,10h
  00480db8  66ff4614              inc       word [esi+14h]
  00480dbc  4b                    dec       ebx
  00480dbd  7593                  jne       short 00480D52h
  00480dbf  33c0                  xor       eax,eax
  00480dc1  66c746160c00          mov       word [esi+16h],0Ch
  00480dc7  66894612              mov       [esi+12h],ax
  00480dcb  66894614              mov       [esi+14h],ax
  00480dcf  8d461c                lea       eax,[esi+1Ch]
  00480dd2  50                    push      eax
  00480dd3  6888255500            push      552588h
  00480dd8  e803ce0b00            call      0053DBE0h
  00480ddd  8b442418              mov       eax,[esp+18h]
  00480de1  83c408                add       esp,8
  00480de4  33ff                  xor       edi,edi

LAB_00480de6:
  00480de6  8b0e                  mov       ecx,[esi]
  00480de8  66ff4610              inc       word [esi+10h]
  00480dec  f7c100000001          test      ecx,1000000h
  00480df2  7444                  je        short 00480E38h
  00480df4  6a01                  push      1
  00480df6  68006af60c            push      0CF66A00h
  00480dfb  50                    push      eax
  00480dfc  e8af40ffff            call      00474EB0h
  00480e01  83c40c                add       esp,0Ch
  00480e04  3bc7                  cmp       eax,edi
  00480e06  7418                  je        short 00480E20h
  00480e08  894678                mov       [esi+78h],eax
  00480e0b  897074                mov       [eax+74h],esi
  00480e0e  8b06                  mov       eax,[esi]
  00480e10  66897e10              mov       [esi+10h],di
  00480e14  0c02                  or        al,2
  00480e16  8906                  mov       [esi],eax
  00480e18  5f                    pop       edi
  00480e19  5e                    pop       esi
  00480e1a  5d                    pop       ebp
  00480e1b  5b                    pop       ebx
  00480e1c  83c410                add       esp,10h
  00480e1f  c3                    ret

LAB_00480e20:
  00480e20  66837e103c            cmp       word [esi+10h],3Ch
  00480e25  7d0b                  jge       short 00480E32h
  00480e27  8b4e74                mov       ecx,[esi+74h]
  00480e2a  f70100000040          test      dword [ecx],40000000h
  00480e30  7406                  je        short 00480E38h

LAB_00480e32:
  00480e32  810e00000002          or        dword [esi],2000000h

LAB_00480e38:
  00480e38  5f                    pop       edi
  00480e39  5e                    pop       esi
  00480e3a  5d                    pop       ebp
  00480e3b  5b                    pop       ebx
  00480e3c  83c410                add       esp,10h
  00480e3f  c3                    ret

16 cible(s) de saut interne : 0x00480bc1, 0x00480bcf, 0x00480c33, 0x00480c51, 0x00480c58, 0x00480c8a, 0x00480c99, 0x00480ca4, 0x00480ca9, 0x00480cc1, 0x00480ce0, 0x00480d52, 0x00480de6, 0x00480e20, 0x00480e32, 0x00480e38
