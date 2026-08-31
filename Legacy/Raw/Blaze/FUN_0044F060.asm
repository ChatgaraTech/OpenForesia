Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044f060  section .text  offset fichier 0x4e460  558 octets

  0044f060  83ec14                sub       esp,14h
  0044f063  53                    push      ebx
  0044f064  55                    push      ebp
  0044f065  56                    push      esi
  0044f066  33db                  xor       ebx,ebx
  0044f068  57                    push      edi
  0044f069  8b7c2428              mov       edi,[esp+28h]
  0044f06d  53                    push      ebx
  0044f06e  53                    push      ebx
  0044f06f  57                    push      edi
  0044f070  e8cb8fffff            call      00448040h
  0044f075  83c40c                add       esp,0Ch
  0044f078  66395f10              cmp       [edi+10h],bx
  0044f07c  750b                  jne       short 0044F089h
  0044f07e  53                    push      ebx
  0044f07f  53                    push      ebx
  0044f080  57                    push      edi
  0044f081  e82a8cffff            call      00447CB0h
  0044f086  83c40c                add       esp,0Ch

LAB_0044f089:
  0044f089  66395f10              cmp       [edi+10h],bx
  0044f08d  be01000000            mov       esi,1
  0044f092  7544                  jne       short 0044F0D8h
  0044f094  6800100000            push      1000h
  0044f099  6800100000            push      1000h
  0044f09e  b8a0000000            mov       eax,0A0h
  0044f0a3  6853060000            push      653h
  0044f0a8  66897710              mov       [edi+10h],si
  0044f0ac  66895f16              mov       [edi+16h],bx
  0044f0b0  6a0a                  push      0Ah
  0044f0b2  66a360915e00          mov       [5E9160h],ax
  0044f0b8  66a362915e00          mov       [5E9162h],ax
  0044f0be  66c70564915e00b400    mov       word [5E9164h],0B4h
  0044f0c7  66c70566915e000200    mov       word [5E9166h],2
  0044f0d0  e8cbdd0e00            call      0053CEA0h
  0044f0d5  83c410                add       esp,10h

LAB_0044f0d8:
  0044f0d8  66397710              cmp       [edi+10h],si
  0044f0dc  0f856e010000          jne       near 0044F250h
  0044f0e2  8b6f74                mov       ebp,[edi+74h]
  0044f0e5  3beb                  cmp       ebp,ebx
  0044f0e7  7505                  jne       short 0044F0EEh
  0044f0e9  bd58e55d00            mov       ebp,5DE558h

LAB_0044f0ee:
  0044f0ee  c744241010270000      mov       dword [esp+10h],2710h
  0044f0f6  c7442414ffffffff      mov       dword [esp+14h],0FFFFFFFFh
  0044f0fe  895c2428              mov       [esp+28h],ebx
  0044f102  bebcca5e00            mov       esi,5ECABCh

LAB_0044f107:
  0044f107  8d44241c              lea       eax,[esp+1Ch]
  0044f10b  8d4c2420              lea       ecx,[esp+20h]
  0044f10f  50                    push      eax
  0044f110  8d54241c              lea       edx,[esp+1Ch]
  0044f114  8d5ee4                lea       ebx,[esi-1Ch]
  0044f117  51                    push      ecx
  0044f118  52                    push      edx
  0044f119  53                    push      ebx
  0044f11a  e861980e00            call      00538980h
  0044f11f  8b1b                  mov       ebx,[ebx]
  0044f121  83c410                add       esp,10h
  0044f124  f7c300000080          test      ebx,80000000h
  0044f12a  0f84bc000000          je        near 0044F1ECh
  0044f130  f7c300200040          test      ebx,40002000h
  0044f136  0f85b0000000          jne       near 0044F1ECh
  0044f13c  8b4658                mov       eax,[esi+58h]
  0044f13f  85c0                  test      eax,eax
  0044f141  0f85a5000000          jne       near 0044F1ECh
  0044f147  8b442418              mov       eax,[esp+18h]
  0044f14b  f7401400000003        test      dword [eax+14h],3000000h
  0044f152  0f8494000000          je        near 0044F1ECh
  0044f158  8b06                  mov       eax,[esi]
  0044f15a  8b551c                mov       edx,[ebp+1Ch]
  0044f15d  8b4e04                mov       ecx,[esi+4]
  0044f160  8b5d20                mov       ebx,[ebp+20h]
  0044f163  2bc2                  sub       eax,edx
  0044f165  8b5608                mov       edx,[esi+8]
  0044f168  2bcb                  sub       ecx,ebx
  0044f16a  8b5d24                mov       ebx,[ebp+24h]
  0044f16d  c1f80c                sar       eax,0Ch
  0044f170  2bd3                  sub       edx,ebx
  0044f172  8bd8                  mov       ebx,eax
  0044f174  c1f90c                sar       ecx,0Ch
  0044f177  0fafd8                imul      ebx,eax
  0044f17a  8bc1                  mov       eax,ecx
  0044f17c  c1fa0c                sar       edx,0Ch
  0044f17f  0fafc1                imul      eax,ecx
  0044f182  8bca                  mov       ecx,edx
  0044f184  03d8                  add       ebx,eax
  0044f186  0fafca                imul      ecx,edx
  0044f189  03d9                  add       ebx,ecx
  0044f18b  53                    push      ebx
  0044f18c  e87f680f00            call      00545A10h
  0044f191  3ddc050000            cmp       eax,5DCh
  0044f196  7d54                  jge       short 0044F1ECh
  0044f198  8b471c                mov       eax,[edi+1Ch]
  0044f19b  8b16                  mov       edx,[esi]
  0044f19d  8b4f20                mov       ecx,[edi+20h]
  0044f1a0  8b5e08                mov       ebx,[esi+8]
  0044f1a3  c1f80c                sar       eax,0Ch
  0044f1a6  c1fa0c                sar       edx,0Ch
  0044f1a9  2bc2                  sub       eax,edx
  0044f1ab  8b5604                mov       edx,[esi+4]
  0044f1ae  c1f90c                sar       ecx,0Ch
  0044f1b1  c1fa0c                sar       edx,0Ch
  0044f1b4  2bca                  sub       ecx,edx
  0044f1b6  8b5724                mov       edx,[edi+24h]
  0044f1b9  c1fa0c                sar       edx,0Ch
  0044f1bc  c1fb0c                sar       ebx,0Ch
  0044f1bf  2bd3                  sub       edx,ebx
  0044f1c1  8bd8                  mov       ebx,eax
  0044f1c3  0fafd8                imul      ebx,eax
  0044f1c6  8bc1                  mov       eax,ecx
  0044f1c8  0fafc1                imul      eax,ecx
  0044f1cb  8bca                  mov       ecx,edx
  0044f1cd  03d8                  add       ebx,eax
  0044f1cf  0fafca                imul      ecx,edx
  0044f1d2  03d9                  add       ebx,ecx
  0044f1d4  53                    push      ebx
  0044f1d5  e836680f00            call      00545A10h
  0044f1da  39442410              cmp       [esp+10h],eax
  0044f1de  7e0c                  jle       short 0044F1ECh
  0044f1e0  8b542428              mov       edx,[esp+28h]
  0044f1e4  89442410              mov       [esp+10h],eax
  0044f1e8  89542414              mov       [esp+14h],edx

LAB_0044f1ec:
  0044f1ec  8b442428              mov       eax,[esp+28h]
  0044f1f0  81c69c000000          add       esi,9Ch
  0044f1f6  40                    inc       eax
  0044f1f7  663d2000              cmp       ax,20h
  0044f1fb  89442428              mov       [esp+28h],eax
  0044f1ff  0f8c02ffffff          jl        near 0044F107h
  0044f205  8b442414              mov       eax,[esp+14h]
  0044f209  663dffff              cmp       ax,0FFFFh
  0044f20d  742f                  je        short 0044F23Eh
  0044f20f  0fbfc0                movsx     eax,ax
  0044f212  8b576c                mov       edx,[edi+6Ch]
  0044f215  6aff                  push      0FFFFFFFFh
  0044f217  8d0c80                lea       ecx,[eax+eax*4]
  0044f21a  6a00                  push      0
  0044f21c  c1e103                shl       ecx,3
  0044f21f  2bc8                  sub       ecx,eax
  0044f221  689e000000            push      9Eh
  0044f226  57                    push      edi
  0044f227  8d048da0ca5e00        lea       eax,[ecx*4+5ECAA0h]
  0044f22e  894778                mov       [edi+78h],eax
  0044f231  895074                mov       [eax+74h],edx
  0044f234  e8a7b80200            call      0047AAE0h
  0044f239  83c410                add       esp,10h
  0044f23c  eb0c                  jmp       short 0044F24Ah

LAB_0044f23e:
  0044f23e  66c747106300          mov       word [edi+10h],63h
  0044f244  66c747164600          mov       word [edi+16h],46h

LAB_0044f24a:
  0044f24a  66c747100200          mov       word [edi+10h],2

LAB_0044f250:
  0044f250  66837f1002            cmp       word [edi+10h],2
  0044f255  750d                  jne       short 0044F264h
  0044f257  66837f165a            cmp       word [edi+16h],5Ah
  0044f25c  7c06                  jl        short 0044F264h
  0044f25e  66c747106300          mov       word [edi+10h],63h

LAB_0044f264:
  0044f264  66ff4716              inc       word [edi+16h]
  0044f268  66837f1063            cmp       word [edi+10h],63h
  0044f26d  668b4716              mov       ax,[edi+16h]
  0044f271  7513                  jne       short 0044F286h
  0044f273  663d5a00              cmp       ax,5Ah
  0044f277  7c0d                  jl        short 0044F286h
  0044f279  6a00                  push      0
  0044f27b  6a00                  push      0
  0044f27d  57                    push      edi
  0044f27e  e89d8dffff            call      00448020h
  0044f283  83c40c                add       esp,0Ch

LAB_0044f286:
  0044f286  5f                    pop       edi
  0044f287  5e                    pop       esi
  0044f288  5d                    pop       ebp
  0044f289  5b                    pop       ebx
  0044f28a  83c414                add       esp,14h
  0044f28d  c3                    ret

10 cible(s) de saut interne : 0x0044f089, 0x0044f0d8, 0x0044f0ee, 0x0044f107, 0x0044f1ec, 0x0044f23e, 0x0044f24a, 0x0044f250, 0x0044f264, 0x0044f286
