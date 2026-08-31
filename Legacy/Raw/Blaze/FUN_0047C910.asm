
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047c910  section .text  offset fichier 0x7bd10  309 octets

  0047c910  55                    push      ebp
  0047c911  56                    push      esi
  0047c912  8b74240c              mov       esi,[esp+0Ch]
  0047c916  57                    push      edi
  0047c917  8b7e74                mov       edi,[esi+74h]
  0047c91a  57                    push      edi
  0047c91b  e8a0c20b00            call      00538BC0h
  0047c920  8b4e20                mov       ecx,[esi+20h]
  0047c923  83c404                add       esp,4
  0047c926  24fe                  and       al,0FEh
  0047c928  c1e00b                shl       eax,0Bh
  0047c92b  6a01                  push      1
  0047c92d  03c8                  add       ecx,eax
  0047c92f  6aff                  push      0FFFFFFFFh
  0047c931  57                    push      edi
  0047c932  894e20                mov       [esi+20h],ecx
  0047c935  e87685ffff            call      00474EB0h
  0047c93a  8be8                  mov       ebp,eax
  0047c93c  83c40c                add       esp,0Ch
  0047c93f  85ed                  test      ebp,ebp
  0047c941  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047c948  c74628000c0008        mov       dword [esi+28h],8000C00h
  0047c94f  66c7462c0008          mov       word [esi+2Ch],800h
  0047c955  0f84a0000000          je        near 0047C9FBh
  0047c95b  8b461c                mov       eax,[esi+1Ch]
  0047c95e  53                    push      ebx
  0047c95f  896e78                mov       [esi+78h],ebp
  0047c962  8b5d1c                mov       ebx,[ebp+1Ch]
  0047c965  2bd8                  sub       ebx,eax
  0047c967  55                    push      ebp
  0047c968  c1fb0c                sar       ebx,0Ch
  0047c96b  e850c20b00            call      00538BC0h
  0047c970  8b4d20                mov       ecx,[ebp+20h]
  0047c973  8b6d24                mov       ebp,[ebp+24h]
  0047c976  8bf8                  mov       edi,eax
  0047c978  8b4620                mov       eax,[esi+20h]
  0047c97b  83e7fe                and       edi,0FFFFFFFEh
  0047c97e  83c404                add       esp,4
  0047c981  c1e70b                shl       edi,0Bh
  0047c984  03f9                  add       edi,ecx
  0047c986  8b4e24                mov       ecx,[esi+24h]
  0047c989  2be9                  sub       ebp,ecx
  0047c98b  2bf8                  sub       edi,eax
  0047c98d  c1fd0c                sar       ebp,0Ch
  0047c990  8bc3                  mov       eax,ebx
  0047c992  8bcd                  mov       ecx,ebp
  0047c994  0fafc3                imul      eax,ebx
  0047c997  0fafcd                imul      ecx,ebp
  0047c99a  03c1                  add       eax,ecx
  0047c99c  50                    push      eax
  0047c99d  c1ff0c                sar       edi,0Ch
  0047c9a0  e86b900c00            call      00545A10h
  0047c9a5  55                    push      ebp
  0047c9a6  53                    push      ebx
  0047c9a7  8944241c              mov       [esp+1Ch],eax
  0047c9ab  e872900c00            call      00545A22h
  0047c9b0  8bd8                  mov       ebx,eax
  0047c9b2  e889a70c00            call      00547140h
  0047c9b7  25ff0f0000            and       eax,0FFFh
  0047c9bc  8d9403000a0000        lea       edx,[ebx+eax+0A00h]
  0047c9c3  8b442414              mov       eax,[esp+14h]
  0047c9c7  50                    push      eax
  0047c9c8  57                    push      edi
  0047c9c9  66895632              mov       [esi+32h],dx
  0047c9cd  e850900c00            call      00545A22h
  0047c9d2  0fbf5632              movsx     edx,word [esi+32h]
  0047c9d6  0500010000            add       eax,100h
  0047c9db  6a20                  push      20h
  0047c9dd  0fbfc8                movsx     ecx,ax
  0047c9e0  66894634              mov       [esi+34h],ax
  0047c9e4  81ea00040000          sub       edx,400h
  0047c9ea  51                    push      ecx
  0047c9eb  8d4650                lea       eax,[esi+50h]
  0047c9ee  52                    push      edx
  0047c9ef  50                    push      eax
  0047c9f0  e8db600b00            call      00532AD0h
  0047c9f5  83c410                add       esp,10h
  0047c9f8  5b                    pop       ebx
  0047c9f9  eb29                  jmp       short 0047CA24h

LAB_0047c9fb:
  0047c9fb  0fbf7e32              movsx     edi,word [esi+32h]
  0047c9ff  57                    push      edi
  0047ca00  e817900c00            call      00545A1Ch
  0047ca05  c1e006                shl       eax,6
  0047ca08  57                    push      edi
  0047ca09  894650                mov       [esi+50h],eax
  0047ca0c  c7465400000000        mov       dword [esi+54h],0
  0047ca13  e8fe8f0c00            call      00545A16h
  0047ca18  c1e006                shl       eax,6
  0047ca1b  668146320004          add       word [esi+32h],400h
  0047ca21  894658                mov       [esi+58h],eax

LAB_0047ca24:
  0047ca24  33c9                  xor       ecx,ecx
  0047ca26  6aff                  push      0FFFFFFFFh
  0047ca28  8a4e0b                mov       cl,[esi+0Bh]
  0047ca2b  c6460414              mov       byte [esi+4],14h
  0047ca2f  51                    push      ecx
  0047ca30  6a6d                  push      6Dh
  0047ca32  56                    push      esi
  0047ca33  e8a8e0ffff            call      0047AAE0h
  0047ca38  83c410                add       esp,10h
  0047ca3b  c70600000000          mov       dword [esi],0
  0047ca41  5f                    pop       edi
  0047ca42  5e                    pop       esi
  0047ca43  5d                    pop       ebp
  0047ca44  c3                    ret

2 cible(s) de saut interne : 0x0047c9fb, 0x0047ca24
