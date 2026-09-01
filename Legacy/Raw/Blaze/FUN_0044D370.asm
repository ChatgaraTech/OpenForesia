Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044d370  section .text  offset fichier 0x4c770  1384 octets

  0044d370  83ec48                sub       esp,48h
  0044d373  53                    push      ebx
  0044d374  55                    push      ebp
  0044d375  56                    push      esi
  0044d376  8b742458              mov       esi,[esp+58h]
  0044d37a  33db                  xor       ebx,ebx
  0044d37c  57                    push      edi
  0044d37d  53                    push      ebx
  0044d37e  53                    push      ebx
  0044d37f  56                    push      esi
  0044d380  e8bbacffff            call      00448040h
  0044d385  83c40c                add       esp,0Ch
  0044d388  66395e10              cmp       [esi+10h],bx
  0044d38c  750b                  jne       short 0044D399h
  0044d38e  53                    push      ebx
  0044d38f  53                    push      ebx
  0044d390  56                    push      esi
  0044d391  e81aa9ffff            call      00447CB0h
  0044d396  83c40c                add       esp,0Ch

LAB_0044d399:
  0044d399  66395e16              cmp       [esi+16h],bx
  0044d39d  7519                  jne       short 0044D3B8h
  0044d39f  6800100000            push      1000h
  0044d3a4  6800100000            push      1000h
  0044d3a9  6800080000            push      800h
  0044d3ae  6a30                  push      30h
  0044d3b0  e8ebfa0e00            call      0053CEA0h
  0044d3b5  83c410                add       esp,10h

LAB_0044d3b8:
  0044d3b8  bf0a000000            mov       edi,0Ah
  0044d3bd  66397e16              cmp       [esi+16h],di
  0044d3c1  7519                  jne       short 0044D3DCh
  0044d3c3  6800100000            push      1000h
  0044d3c8  6800100000            push      1000h
  0044d3cd  6800080000            push      800h
  0044d3d2  6a0c                  push      0Ch
  0044d3d4  e8c7fa0e00            call      0053CEA0h
  0044d3d9  83c410                add       esp,10h

LAB_0044d3dc:
  0044d3dc  66837e160b            cmp       word [esi+16h],0Bh
  0044d3e1  7519                  jne       short 0044D3FCh
  0044d3e3  6800100000            push      1000h
  0044d3e8  6800100000            push      1000h
  0044d3ed  68a60c0000            push      0CA6h
  0044d3f2  6a30                  push      30h
  0044d3f4  e8a7fa0e00            call      0053CEA0h
  0044d3f9  83c410                add       esp,10h

LAB_0044d3fc:
  0044d3fc  bd0c000000            mov       ebp,0Ch
  0044d401  66396e16              cmp       [esi+16h],bp
  0044d405  7519                  jne       short 0044D420h
  0044d407  6800100000            push      1000h
  0044d40c  6800100000            push      1000h
  0044d411  6800080000            push      800h
  0044d416  6a1c                  push      1Ch
  0044d418  e883fa0e00            call      0053CEA0h
  0044d41d  83c410                add       esp,10h

LAB_0044d420:
  0044d420  66395e10              cmp       [esi+10h],bx
  0044d424  7549                  jne       short 0044D46Fh
  0044d426  6aff                  push      0FFFFFFFFh
  0044d428  53                    push      ebx
  0044d429  b0fa                  mov       al,0FAh
  0044d42b  689a000000            push      9Ah
  0044d430  56                    push      esi
  0044d431  66c746100100          mov       word [esi+10h],1
  0044d437  66895e16              mov       [esi+16h],bx
  0044d43b  884638                mov       [esi+38h],al
  0044d43e  884639                mov       [esi+39h],al
  0044d441  88463a                mov       [esi+3Ah],al
  0044d444  e897d60200            call      0047AAE0h
  0044d449  668146320008          add       word [esi+32h],800h
  0044d44f  668b4632              mov       ax,[esi+32h]
  0044d453  83c410                add       esp,10h
  0044d456  663d0010              cmp       ax,1000h
  0044d45a  7c09                  jl        short 0044D465h
  0044d45c  0500f0ffff            add       eax,0FFFFF000h
  0044d461  66894632              mov       [esi+32h],ax

LAB_0044d465:
  0044d465  8b4674                mov       eax,[esi+74h]
  0044d468  8b08                  mov       ecx,[eax]
  0044d46a  80c980                or        cl,80h
  0044d46d  8908                  mov       [eax],ecx

LAB_0044d46f:
  0044d46f  66837e1001            cmp       word [esi+10h],1
  0044d474  7556                  jne       short 0044D4CCh
  0044d476  b8aa000000            mov       eax,0AAh
  0044d47b  b9c8000000            mov       ecx,0C8h
  0044d480  66a360915e00          mov       [5E9160h],ax
  0044d486  66a364915e00          mov       [5E9164h],ax
  0044d48c  b802000000            mov       eax,2
  0044d491  66890d62915e00        mov       [5E9162h],cx
  0044d498  66a366915e00          mov       [5E9166h],ax
  0044d49e  66397e16              cmp       [esi+16h],di
  0044d4a2  752d                  jne       short 0044D4D1h
  0044d4a4  66894610              mov       [esi+10h],ax
  0044d4a8  c60527e05d0001        mov       byte [5DE027h],1
  0044d4af  c60524e05d0096        mov       byte [5DE024h],96h
  0044d4b6  880d25e05d00          mov       [5DE025h],cl
  0044d4bc  c60526e05d00b4        mov       byte [5DE026h],0B4h
  0044d4c3  66891d66915e00        mov       [5E9166h],bx
  0044d4ca  eb05                  jmp       short 0044D4D1h

LAB_0044d4cc:
  0044d4cc  b802000000            mov       eax,2

LAB_0044d4d1:
  0044d4d1  66394610              cmp       [esi+10h],ax
  0044d4d5  0f85a3030000          jne       near 0044D87Eh
  0044d4db  66396e16              cmp       [esi+16h],bp
  0044d4df  7506                  jne       short 0044D4E7h
  0044d4e1  66c746100300          mov       word [esi+10h],3

LAB_0044d4e7:
  0044d4e7  8d6e1c                lea       ebp,[esi+1Ch]
  0044d4ea  bfc0ca5e00            mov       edi,5ECAC0h
  0044d4ef  c744242420000000      mov       dword [esp+24h],20h

LAB_0044d4f7:
  0044d4f7  8b9e80000000          mov       ebx,[esi+80h]
  0044d4fd  8b467c                mov       eax,[esi+7Ch]
  0044d500  668b8e90000000        mov       cx,[esi+90h]
  0044d507  8b5660                mov       edx,[esi+60h]
  0044d50a  895c2440              mov       [esp+40h],ebx
  0044d50e  8b9e84000000          mov       ebx,[esi+84h]
  0044d514  89442430              mov       [esp+30h],eax
  0044d518  8b4664                mov       eax,[esi+64h]
  0044d51b  895c2444              mov       [esp+44h],ebx
  0044d51f  8b9e88000000          mov       ebx,[esi+88h]
  0044d525  66894c2434            mov       [esp+34h],cx
  0044d52a  8b4e24                mov       ecx,[esi+24h]
  0044d52d  89542438              mov       [esp+38h],edx
  0044d531  8b5620                mov       edx,[esi+20h]
  0044d534  8944243c              mov       [esp+3Ch],eax
  0044d538  8b4500                mov       eax,[ebp]
  0044d53b  895c2448              mov       [esp+48h],ebx
  0044d53f  8b9e8c000000          mov       ebx,[esi+8Ch]
  0044d545  894644                mov       [esi+44h],eax
  0044d548  895648                mov       [esi+48h],edx
  0044d54b  894e4c                mov       [esi+4Ch],ecx
  0044d54e  895c244c              mov       [esp+4Ch],ebx
  0044d552  8b5ffc                mov       ebx,[edi-4]
  0044d555  8954242c              mov       [esp+2Ch],edx
  0044d559  2bd8                  sub       ebx,eax
  0044d55b  8b07                  mov       eax,[edi]
  0044d55d  2bc2                  sub       eax,edx
  0044d55f  8b5704                mov       edx,[edi+4]
  0044d562  2bd1                  sub       edx,ecx
  0044d564  8bcb                  mov       ecx,ebx
  0044d566  89542414              mov       [esp+14h],edx
  0044d56a  89442410              mov       [esp+10h],eax
  0044d56e  c1fa0c                sar       edx,0Ch
  0044d571  c1f90c                sar       ecx,0Ch
  0044d574  c1f80c                sar       eax,0Ch
  0044d577  52                    push      edx
  0044d578  51                    push      ecx
  0044d579  894c2428              mov       [esp+28h],ecx
  0044d57d  89442420              mov       [esp+20h],eax
  0044d581  89542424              mov       [esp+24h],edx
  0044d585  e898840f00            call      00545A22h
  0044d58a  0fbf4e32              movsx     ecx,word [esi+32h]
  0044d58e  ba00080000            mov       edx,800h
  0044d593  2bd1                  sub       edx,ecx
  0044d595  03c2                  add       eax,edx
  0044d597  8944245c              mov       [esp+5Ch],eax
  0044d59b  7913                  jns       short 0044D5B0h
  0044d59d  b9ff0f0000            mov       ecx,0FFFh
  0044d5a2  2bc8                  sub       ecx,eax
  0044d5a4  c1e90c                shr       ecx,0Ch
  0044d5a7  c1e10c                shl       ecx,0Ch
  0044d5aa  03c1                  add       eax,ecx
  0044d5ac  8944245c              mov       [esp+5Ch],eax

LAB_0044d5b0:
  0044d5b0  3d00100000            cmp       eax,1000h
  0044d5b5  7c11                  jl        short 0044D5C8h
  0044d5b7  8bc8                  mov       ecx,eax
  0044d5b9  c1e90c                shr       ecx,0Ch

LAB_0044d5bc:
  0044d5bc  2d00100000            sub       eax,1000h
  0044d5c1  49                    dec       ecx
  0044d5c2  75f8                  jne       short 0044D5BCh
  0044d5c4  8944245c              mov       [esp+5Ch],eax

LAB_0044d5c8:
  0044d5c8  8b442420              mov       eax,[esp+20h]
  0044d5cc  8bd0                  mov       edx,eax
  0044d5ce  0fafd0                imul      edx,eax
  0044d5d1  8b442418              mov       eax,[esp+18h]
  0044d5d5  8bc8                  mov       ecx,eax
  0044d5d7  0fafc8                imul      ecx,eax
  0044d5da  8b44241c              mov       eax,[esp+1Ch]
  0044d5de  03d1                  add       edx,ecx
  0044d5e0  8bc8                  mov       ecx,eax
  0044d5e2  0fafc8                imul      ecx,eax
  0044d5e5  03d1                  add       edx,ecx
  0044d5e7  52                    push      edx
  0044d5e8  e823840f00            call      00545A10h
  0044d5ed  66837e160c            cmp       word [esi+16h],0Ch
  0044d5f2  0f8512020000          jne       near 0044D80Ah
  0044d5f8  3d20030000            cmp       eax,320h
  0044d5fd  0f8d07020000          jge       near 0044D80Ah
  0044d603  8b44245c              mov       eax,[esp+5Ch]
  0044d607  3d20030000            cmp       eax,320h
  0044d60c  7d04                  jge       short 0044D612h
  0044d60e  85c0                  test      eax,eax
  0044d610  7f16                  jg        short 0044D628h

LAB_0044d612:
  0044d612  3d00100000            cmp       eax,1000h
  0044d617  0f8ded010000          jge       near 0044D80Ah
  0044d61d  3de00c0000            cmp       eax,0CE0h
  0044d622  0f8ee2010000          jle       near 0044D80Ah

LAB_0044d628:
  0044d628  8b442414              mov       eax,[esp+14h]
  0044d62c  8b4c2410              mov       ecx,[esp+10h]
  0044d630  c1f802                sar       eax,2
  0044d633  c1fb02                sar       ebx,2
  0044d636  c1f902                sar       ecx,2
  0044d639  89442414              mov       [esp+14h],eax
  0044d63d  33c0                  xor       eax,eax
  0044d63f  894c2410              mov       [esp+10h],ecx
  0044d643  89442418              mov       [esp+18h],eax
  0044d647  8944245c              mov       [esp+5Ch],eax
  0044d64b  8d6e1c                lea       ebp,[esi+1Ch]

LAB_0044d64e:
  0044d64e  8b4d00                mov       ecx,[ebp]
  0044d651  8b542410              mov       edx,[esp+10h]
  0044d655  8b4620                mov       eax,[esi+20h]
  0044d658  03cb                  add       ecx,ebx
  0044d65a  03c2                  add       eax,edx
  0044d65c  894d00                mov       [ebp],ecx
  0044d65f  8b4e24                mov       ecx,[esi+24h]
  0044d662  894620                mov       [esi+20h],eax
  0044d665  8b442414              mov       eax,[esp+14h]
  0044d669  56                    push      esi
  0044d66a  03c8                  add       ecx,eax
  0044d66c  894e24                mov       [esi+24h],ecx
  0044d66f  e8cc620e00            call      00533940h
  0044d674  8b8690000000          mov       eax,[esi+90h]
  0044d67a  83c404                add       esp,4
  0044d67d  f6c488                test      ah,88h
  0044d680  740f                  je        short 0044D691h
  0044d682  b863000000            mov       eax,63h
  0044d687  c744241801000000      mov       dword [esp+18h],1
  0044d68f  eb04                  jmp       short 0044D695h

LAB_0044d691:
  0044d691  8b44245c              mov       eax,[esp+5Ch]

LAB_0044d695:
  0044d695  40                    inc       eax
  0044d696  663d0300              cmp       ax,3
  0044d69a  8944245c              mov       [esp+5Ch],eax
  0044d69e  7eae                  jle       short 0044D64Eh
  0044d6a0  66837c241801          cmp       word [esp+18h],1
  0044d6a6  0f845e010000          je        near 0044D80Ah
  0044d6ac  8d4c2450              lea       ecx,[esp+50h]
  0044d6b0  8d542454              lea       edx,[esp+54h]
  0044d6b4  51                    push      ecx
  0044d6b5  8d44242c              lea       eax,[esp+2Ch]
  0044d6b9  8d5fe0                lea       ebx,[edi-20h]
  0044d6bc  52                    push      edx
  0044d6bd  50                    push      eax
  0044d6be  53                    push      ebx
  0044d6bf  e8bcb20e00            call      00538980h
  0044d6c4  8b03                  mov       eax,[ebx]
  0044d6c6  83c410                add       esp,10h
  0044d6c9  a900000080            test      eax,80000000h
  0044d6ce  0f8436010000          je        near 0044D80Ah
  0044d6d4  a900200040            test      eax,40002000h
  0044d6d9  0f852b010000          jne       near 0044D80Ah
  0044d6df  8b4c2428              mov       ecx,[esp+28h]
  0044d6e3  f7411480000001        test      dword [ecx+14h],1000080h
  0044d6ea  0f841a010000          je        near 0044D80Ah
  0044d6f0  8b57fc                mov       edx,[edi-4]
  0044d6f3  8d6e1c                lea       ebp,[esi+1Ch]
  0044d6f6  895500                mov       [ebp],edx
  0044d6f9  8b07                  mov       eax,[edi]
  0044d6fb  668b5632              mov       dx,[esi+32h]
  0044d6ff  894620                mov       [esi+20h],eax
  0044d702  8b4f04                mov       ecx,[edi+4]
  0044d705  8b44241c              mov       eax,[esp+1Ch]
  0044d709  894e24                mov       [esi+24h],ecx
  0044d70c  8b4c2420              mov       ecx,[esp+20h]
  0044d710  50                    push      eax
  0044d711  51                    push      ecx
  0044d712  66c746143200          mov       word [esi+14h],32h
  0044d718  66c7869a0000000200    mov       word [esi+9Ah],2
  0044d721  6689542464            mov       [esp+64h],dx
  0044d726  e8f7820f00            call      00545A22h
  0044d72b  8b5678                mov       edx,[esi+78h]
  0044d72e  6aff                  push      0FFFFFFFFh
  0044d730  66894632              mov       [esi+32h],ax
  0044d734  6a00                  push      0
  0044d736  8d47e0                lea       eax,[edi-20h]
  0044d739  689b000000            push      9Bh
  0044d73e  56                    push      esi
  0044d73f  89542430              mov       [esp+30h],edx
  0044d743  894678                mov       [esi+78h],eax
  0044d746  e895d30200            call      0047AAE0h
  0044d74b  83c410                add       esp,10h
  0044d74e  6aff                  push      0FFFFFFFFh
  0044d750  6a00                  push      0
  0044d752  6894000000            push      94h
  0044d757  6a00                  push      0
  0044d759  55                    push      ebp
  0044d75a  56                    push      esi
  0044d75b  e8e0d40200            call      0047AC40h
  0044d760  83c418                add       esp,18h
  0044d763  6aff                  push      0FFFFFFFFh
  0044d765  6a00                  push      0
  0044d767  6894000000            push      94h
  0044d76c  6a00                  push      0
  0044d76e  55                    push      ebp
  0044d76f  56                    push      esi
  0044d770  e8cbd40200            call      0047AC40h
  0044d775  83c418                add       esp,18h
  0044d778  6aff                  push      0FFFFFFFFh
  0044d77a  6a00                  push      0
  0044d77c  6894000000            push      94h
  0044d781  6a00                  push      0
  0044d783  55                    push      ebp
  0044d784  56                    push      esi
  0044d785  e8b6d40200            call      0047AC40h
  0044d78a  83c418                add       esp,18h
  0044d78d  6aff                  push      0FFFFFFFFh
  0044d78f  6a00                  push      0
  0044d791  6894000000            push      94h
  0044d796  6a00                  push      0
  0044d798  55                    push      ebp
  0044d799  56                    push      esi
  0044d79a  e8a1d40200            call      0047AC40h
  0044d79f  8b4678                mov       eax,[esi+78h]
  0044d7a2  83c418                add       esp,18h
  0044d7a5  33c9                  xor       ecx,ecx
  0044d7a7  33d2                  xor       edx,edx
  0044d7a9  8a4e06                mov       cl,[esi+6]
  0044d7ac  8a5607                mov       dl,[esi+7]
  0044d7af  6a00                  push      0
  0044d7b1  51                    push      ecx
  0044d7b2  8b4e74                mov       ecx,[esi+74h]
  0044d7b5  52                    push      edx
  0044d7b6  50                    push      eax
  0044d7b7  51                    push      ecx
  0044d7b8  e8f3ca0e00            call      0053A2B0h
  0044d7bd  0fbf5632              movsx     edx,word [esi+32h]
  0044d7c1  83c414                add       esp,14h
  0044d7c4  52                    push      edx
  0044d7c5  e852820f00            call      00545A1Ch
  0044d7ca  8b4f30                mov       ecx,[edi+30h]
  0044d7cd  8d0480                lea       eax,[eax+eax*4]
  0044d7d0  d1e0                  shl       eax,1
  0044d7d2  03c8                  add       ecx,eax
  0044d7d4  894f30                mov       [edi+30h],ecx
  0044d7d7  0fbf4e32              movsx     ecx,word [esi+32h]
  0044d7db  51                    push      ecx
  0044d7dc  e835820f00            call      00545A16h
  0044d7e1  8b4f38                mov       ecx,[edi+38h]
  0044d7e4  8d1480                lea       edx,[eax+eax*4]
  0044d7e7  8b03                  mov       eax,[ebx]
  0044d7e9  66c747761000          mov       word [edi+76h],10h
  0044d7ef  d1e2                  shl       edx,1
  0044d7f1  03ca                  add       ecx,edx
  0044d7f3  0c40                  or        al,40h
  0044d7f5  894f38                mov       [edi+38h],ecx
  0044d7f8  8b4c2420              mov       ecx,[esp+20h]
  0044d7fc  8903                  mov       [ebx],eax
  0044d7fe  668b44245c            mov       ax,[esp+5Ch]
  0044d803  66894632              mov       [esi+32h],ax
  0044d807  894e78                mov       [esi+78h],ecx

LAB_0044d80a:
  0044d80a  8b442430              mov       eax,[esp+30h]
  0044d80e  8b54242c              mov       edx,[esp+2Ch]
  0044d812  89467c                mov       [esi+7Ch],eax
  0044d815  8b442434              mov       eax,[esp+34h]
  0044d819  8b4c2438              mov       ecx,[esp+38h]
  0044d81d  25ffff0000            and       eax,0FFFFh
  0044d822  898690000000          mov       [esi+90h],eax
  0044d828  8b4644                mov       eax,[esi+44h]
  0044d82b  895620                mov       [esi+20h],edx
  0044d82e  8b54243c              mov       edx,[esp+3Ch]
  0044d832  894500                mov       [ebp],eax
  0044d835  8b442444              mov       eax,[esp+44h]
  0044d839  894e60                mov       [esi+60h],ecx
  0044d83c  8b4e4c                mov       ecx,[esi+4Ch]
  0044d83f  895664                mov       [esi+64h],edx
  0044d842  8b542440              mov       edx,[esp+40h]
  0044d846  898684000000          mov       [esi+84h],eax
  0044d84c  8b442424              mov       eax,[esp+24h]
  0044d850  894e24                mov       [esi+24h],ecx
  0044d853  8b4c2448              mov       ecx,[esp+48h]
  0044d857  899680000000          mov       [esi+80h],edx
  0044d85d  8b54244c              mov       edx,[esp+4Ch]
  0044d861  81c79c000000          add       edi,9Ch
  0044d867  48                    dec       eax
  0044d868  898e88000000          mov       [esi+88h],ecx
  0044d86e  89968c000000          mov       [esi+8Ch],edx
  0044d874  89442424              mov       [esp+24h],eax
  0044d878  0f8579fcffff          jne       near 0044D4F7h

LAB_0044d87e:
  0044d87e  66837e1003            cmp       word [esi+10h],3
  0044d883  7522                  jne       short 0044D8A7h
  0044d885  c60527e05d0001        mov       byte [5DE027h],1
  0044d88c  c60524e05d0000        mov       byte [5DE024h],0
  0044d893  c60525e05d0000        mov       byte [5DE025h],0
  0044d89a  c60526e05d0000        mov       byte [5DE026h],0
  0044d8a1  66c746100400          mov       word [esi+10h],4

LAB_0044d8a7:
  0044d8a7  66837e1614            cmp       word [esi+16h],14h
  0044d8ac  750a                  jne       short 0044D8B8h
  0044d8ae  8b4674                mov       eax,[esi+74h]
  0044d8b1  8b08                  mov       ecx,[eax]
  0044d8b3  80e17f                and       cl,7Fh
  0044d8b6  8908                  mov       [eax],ecx

LAB_0044d8b8:
  0044d8b8  66ff4616              inc       word [esi+16h]
  0044d8bc  66837e163c            cmp       word [esi+16h],3Ch
  0044d8c1  7c0d                  jl        short 0044D8D0h
  0044d8c3  6a00                  push      0
  0044d8c5  6a00                  push      0
  0044d8c7  56                    push      esi
  0044d8c8  e853a7ffff            call      00448020h
  0044d8cd  83c40c                add       esp,0Ch

LAB_0044d8d0:
  0044d8d0  5f                    pop       edi
  0044d8d1  5e                    pop       esi
  0044d8d2  5d                    pop       ebp
  0044d8d3  5b                    pop       ebx
  0044d8d4  83c448                add       esp,48h
  0044d8d7  c3                    ret

24 cible(s) de saut interne : 0x0044d399, 0x0044d3b8, 0x0044d3dc, 0x0044d3fc, 0x0044d420, 0x0044d465, 0x0044d46f, 0x0044d4cc, 0x0044d4d1, 0x0044d4e7, 0x0044d4f7, 0x0044d5b0, 0x0044d5bc, 0x0044d5c8, 0x0044d612, 0x0044d628, 0x0044d64e, 0x0044d691, 0x0044d695, 0x0044d80a, 0x0044d87e, 0x0044d8a7, 0x0044d8b8, 0x0044d8d0
