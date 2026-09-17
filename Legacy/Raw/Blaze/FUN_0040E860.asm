Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040e860  section .text  offset fichier 0xdc60  416 octets

  0040e860  55                    push      ebp
  0040e861  56                    push      esi
  0040e862  8b74240c              mov       esi,[esp+0Ch]
  0040e866  57                    push      edi
  0040e867  66837e1000            cmp       word [esi+10h],0
  0040e86c  0f85fa000000          jne       near 0040E96Ch
  0040e872  66c746100100          mov       word [esi+10h],1
  0040e878  c7463c80f65400        mov       dword [esi+3Ch],54F680h
  0040e87f  c6464001              mov       byte [esi+40h],1
  0040e883  c6460a0a              mov       byte [esi+0Ah],0Ah
  0040e887  c6460b00              mov       byte [esi+0Bh],0
  0040e88b  e8b0881300            call      00547140h
  0040e890  99                    cdq
  0040e891  33c2                  xor       eax,edx
  0040e893  2bc2                  sub       eax,edx
  0040e895  25ff010000            and       eax,1FFh
  0040e89a  33c2                  xor       eax,edx
  0040e89c  2bc2                  sub       eax,edx
  0040e89e  0500080000            add       eax,800h
  0040e8a3  66894628              mov       [esi+28h],ax
  0040e8a7  6689462a              mov       [esi+2Ah],ax
  0040e8ab  e890881300            call      00547140h
  0040e8b0  99                    cdq
  0040e8b1  b905000000            mov       ecx,5
  0040e8b6  f7f9                  idiv      ecx
  0040e8b8  03d1                  add       edx,ecx
  0040e8ba  0fbffa                movsx     edi,dx
  0040e8bd  0fbf5632              movsx     edx,word [esi+32h]
  0040e8c1  52                    push      edx
  0040e8c2  e855711300            call      00545A1Ch
  0040e8c7  8be8                  mov       ebp,eax
  0040e8c9  0fafef                imul      ebp,edi
  0040e8cc  e86f881300            call      00547140h
  0040e8d1  99                    cdq
  0040e8d2  33c2                  xor       eax,edx
  0040e8d4  2bc2                  sub       eax,edx
  0040e8d6  83e003                and       eax,3
  0040e8d9  33c2                  xor       eax,edx
  0040e8db  2bc2                  sub       eax,edx
  0040e8dd  83e802                sub       eax,2
  0040e8e0  c1e00c                shl       eax,0Ch
  0040e8e3  2bc5                  sub       eax,ebp
  0040e8e5  894658                mov       [esi+58h],eax
  0040e8e8  0fbf4632              movsx     eax,word [esi+32h]
  0040e8ec  50                    push      eax
  0040e8ed  e824711300            call      00545A16h
  0040e8f2  8be8                  mov       ebp,eax
  0040e8f4  0fafef                imul      ebp,edi
  0040e8f7  e844881300            call      00547140h
  0040e8fc  99                    cdq
  0040e8fd  33c2                  xor       eax,edx
  0040e8ff  2bc2                  sub       eax,edx
  0040e901  83e003                and       eax,3
  0040e904  33c2                  xor       eax,edx
  0040e906  2bc2                  sub       eax,edx
  0040e908  83e802                sub       eax,2
  0040e90b  c1e00c                shl       eax,0Ch
  0040e90e  03e8                  add       ebp,eax
  0040e910  896e50                mov       [esi+50h],ebp
  0040e913  e828881300            call      00547140h
  0040e918  99                    cdq
  0040e919  b90a000000            mov       ecx,0Ah
  0040e91e  66c746120000          mov       word [esi+12h],0
  0040e924  f7f9                  idiv      ecx
  0040e926  83ea05                sub       edx,5
  0040e929  c1e20c                shl       edx,0Ch
  0040e92c  895654                mov       [esi+54h],edx
  0040e92f  e80c881300            call      00547140h
  0040e934  99                    cdq
  0040e935  33c2                  xor       eax,edx
  0040e937  6a00                  push      0
  0040e939  2bc2                  sub       eax,edx
  0040e93b  6a00                  push      0
  0040e93d  83e003                and       eax,3
  0040e940  6800050000            push      500h
  0040e945  33c2                  xor       eax,edx
  0040e947  6800020000            push      200h
  0040e94c  2bc2                  sub       eax,edx
  0040e94e  6a0b                  push      0Bh
  0040e950  83c003                add       eax,3
  0040e953  56                    push      esi
  0040e954  66894614              mov       [esi+14h],ax
  0040e958  e823970500            call      00468080h
  0040e95d  b0a0                  mov       al,0A0h
  0040e95f  83c418                add       esp,18h
  0040e962  c64638c8              mov       byte [esi+38h],0C8h
  0040e966  884639                mov       [esi+39h],al
  0040e969  88463a                mov       [esi+3Ah],al

LAB_0040e96c:
  0040e96c  66837e1001            cmp       word [esi+10h],1
  0040e971  0f8581000000          jne       near 0040E9F8h
  0040e977  8b5624                mov       edx,[esi+24h]
  0040e97a  8b4620                mov       eax,[esi+20h]
  0040e97d  8b4e1c                mov       ecx,[esi+1Ch]
  0040e980  6a00                  push      0
  0040e982  6a00                  push      0
  0040e984  6a00                  push      0
  0040e986  6a05                  push      5
  0040e988  6a28                  push      28h
  0040e98a  52                    push      edx
  0040e98b  50                    push      eax
  0040e98c  51                    push      ecx
  0040e98d  e8bea50000            call      00418F50h
  0040e992  8b5650                mov       edx,[esi+50h]
  0040e995  8b461c                mov       eax,[esi+1Ch]
  0040e998  8b6e20                mov       ebp,[esi+20h]
  0040e99b  8b4e58                mov       ecx,[esi+58h]
  0040e99e  8b7e24                mov       edi,[esi+24h]
  0040e9a1  03c2                  add       eax,edx
  0040e9a3  89461c                mov       [esi+1Ch],eax
  0040e9a6  8b4654                mov       eax,[esi+54h]
  0040e9a9  83c420                add       esp,20h
  0040e9ac  03e8                  add       ebp,eax
  0040e9ae  03f9                  add       edi,ecx
  0040e9b0  66ff4612              inc       word [esi+12h]
  0040e9b4  668b4612              mov       ax,[esi+12h]
  0040e9b8  896e20                mov       [esi+20h],ebp
  0040e9bb  663b4614              cmp       ax,[esi+14h]
  0040e9bf  897e24                mov       [esi+24h],edi
  0040e9c2  7e25                  jle       short 0040E9E9h
  0040e9c4  8a4640                mov       al,[esi+40h]
  0040e9c7  b900010000            mov       ecx,100h
  0040e9cc  66014e28              add       [esi+28h],cx
  0040e9d0  66014e2a              add       [esi+2Ah],cx
  0040e9d4  fec0                  inc       al
  0040e9d6  66c746120000          mov       word [esi+12h],0
  0040e9dc  3c04                  cmp       al,4
  0040e9de  884640                mov       [esi+40h],al
  0040e9e1  7206                  jb        short 0040E9E9h
  0040e9e3  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040e9e9:
  0040e9e9  6af6                  push      0FFFFFFF6h
  0040e9eb  6af6                  push      0FFFFFFF6h
  0040e9ed  6af6                  push      0FFFFFFF6h
  0040e9ef  56                    push      esi
  0040e9f0  e8eb63ffff            call      00404DE0h
  0040e9f5  83c410                add       esp,10h

LAB_0040e9f8:
  0040e9f8  5f                    pop       edi
  0040e9f9  5e                    pop       esi
  0040e9fa  5d                    pop       ebp
  0040e9fb  c3                    ret
  0040e9fc  90                    nop
  0040e9fd  90                    nop
  0040e9fe  90                    nop
  0040e9ff  90                    nop

3 cible(s) de saut interne : 0x0040e96c, 0x0040e9e9, 0x0040e9f8
