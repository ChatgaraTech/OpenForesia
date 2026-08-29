C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047a270  section .text  offset fichier 0x79670  864 octets

  0047a270  83ec18                sub       esp,18h
  0047a273  53                    push      ebx
  0047a274  55                    push      ebp
  0047a275  56                    push      esi
  0047a276  8b742428              mov       esi,[esp+28h]
  0047a27a  b832000000            mov       eax,32h
  0047a27f  57                    push      edi
  0047a280  6689442420            mov       [esp+20h],ax
  0047a285  6689442422            mov       [esp+22h],ax
  0047a28a  8b06                  mov       eax,[esi]
  0047a28c  66c744241c00fc        mov       word [esp+1Ch],0FC00h
  0047a293  a900000010            test      eax,10000000h
  0047a298  66c744241e80ff        mov       word [esp+1Eh],0FF80h
  0047a29f  66c74424240000        mov       word [esp+24h],0
  0047a2a6  66c74424268000        mov       word [esp+26h],80h
  0047a2ad  7453                  je        short 0047A302h
  0047a2af  66ff4610              inc       word [esi+10h]
  0047a2b3  668b4e10              mov       cx,[esi+10h]
  0047a2b7  f6c101                test      cl,1
  0047a2ba  7405                  je        short 0047A2C1h
  0047a2bc  80cc40                or        ah,40h
  0047a2bf  eb03                  jmp       short 0047A2C4h

LAB_0047a2c1:
  0047a2c1  80e4bf                and       ah,0BFh

LAB_0047a2c4:
  0047a2c4  8906                  mov       [esi],eax
  0047a2c6  8b4678                mov       eax,[esi+78h]
  0047a2c9  85c0                  test      eax,eax
  0047a2cb  741d                  je        short 0047A2EAh
  0047a2cd  8b5644                mov       edx,[esi+44h]
  0047a2d0  8b681c                mov       ebp,[eax+1Ch]
  0047a2d3  03d5                  add       edx,ebp
  0047a2d5  89561c                mov       [esi+1Ch],edx
  0047a2d8  8b5648                mov       edx,[esi+48h]
  0047a2db  035020                add       edx,[eax+20h]
  0047a2de  895620                mov       [esi+20h],edx
  0047a2e1  8b564c                mov       edx,[esi+4Ch]
  0047a2e4  035024                add       edx,[eax+24h]
  0047a2e7  895624                mov       [esi+24h],edx

LAB_0047a2ea:
  0047a2ea  6683f914              cmp       cx,14h
  0047a2ee  0f8cd1020000          jl        near 0047A5C5h
  0047a2f4  c70600000000          mov       dword [esi],0
  0047a2fa  5f                    pop       edi
  0047a2fb  5e                    pop       esi
  0047a2fc  5d                    pop       ebp
  0047a2fd  5b                    pop       ebx
  0047a2fe  83c418                add       esp,18h
  0047a301  c3                    ret

LAB_0047a302:
  0047a302  8b7e78                mov       edi,[esi+78h]
  0047a305  8b4e20                mov       ecx,[esi+20h]
  0047a308  8b5e24                mov       ebx,[esi+24h]
  0047a30b  80e4f7                and       ah,0F7h
  0047a30e  0d80060000            or        eax,680h
  0047a313  894c2414              mov       [esp+14h],ecx
  0047a317  8906                  mov       [esi],eax
  0047a319  8b461c                mov       eax,[esi+1Ch]
  0047a31c  85ff                  test      edi,edi
  0047a31e  89442410              mov       [esp+10h],eax
  0047a322  895c2418              mov       [esp+18h],ebx
  0047a326  0f8484000000          je        near 0047A3B0h
  0047a32c  8b4650                mov       eax,[esi+50h]
  0047a32f  8b4e58                mov       ecx,[esi+58h]
  0047a332  c1f80c                sar       eax,0Ch
  0047a335  8bd0                  mov       edx,eax
  0047a337  c1f90c                sar       ecx,0Ch
  0047a33a  0fafd0                imul      edx,eax
  0047a33d  8bc1                  mov       eax,ecx
  0047a33f  0fafc1                imul      eax,ecx
  0047a342  03d0                  add       edx,eax
  0047a344  52                    push      edx
  0047a345  e8c6b60c00            call      00545A10h
  0047a34a  8b4f24                mov       ecx,[edi+24h]
  0047a34d  8b571c                mov       edx,[edi+1Ch]
  0047a350  2bcb                  sub       ecx,ebx
  0047a352  8be8                  mov       ebp,eax
  0047a354  c1f90c                sar       ecx,0Ch
  0047a357  51                    push      ecx
  0047a358  8b4c2414              mov       ecx,[esp+14h]
  0047a35c  2bd1                  sub       edx,ecx
  0047a35e  c1fa0c                sar       edx,0Ch
  0047a361  52                    push      edx
  0047a362  e8bbb60c00            call      00545A22h
  0047a367  668b4e32              mov       cx,[esi+32h]
  0047a36b  ba00fcffff            mov       edx,0FFFFFC00h
  0047a370  2bd1                  sub       edx,ecx
  0047a372  33c9                  xor       ecx,ecx
  0047a374  03c2                  add       eax,edx
  0047a376  25ff0f0000            and       eax,0FFFh
  0047a37b  3d00080000            cmp       eax,800h
  0047a380  0f9dc1                setge     cl
  0047a383  49                    dec       ecx
  0047a384  83e1c0                and       ecx,0FFFFFFC0h
  0047a387  83c120                add       ecx,20h
  0047a38a  66014e32              add       [esi+32h],cx
  0047a38e  0fbf7e32              movsx     edi,word [esi+32h]
  0047a392  81ef00040000          sub       edi,400h
  0047a398  57                    push      edi
  0047a399  e87eb60c00            call      00545A1Ch
  0047a39e  0fafc5                imul      eax,ebp
  0047a3a1  57                    push      edi
  0047a3a2  894650                mov       [esi+50h],eax
  0047a3a5  e86cb60c00            call      00545A16h
  0047a3aa  0fafc5                imul      eax,ebp
  0047a3ad  894658                mov       [esi+58h],eax

LAB_0047a3b0:
  0047a3b0  8b4650                mov       eax,[esi+50h]
  0047a3b3  bd08000000            mov       ebp,8
  0047a3b8  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0047a3bf  8bd0                  mov       edx,eax
  0047a3c1  c1fa03                sar       edx,3
  0047a3c4  2bc2                  sub       eax,edx
  0047a3c6  8bd1                  mov       edx,ecx
  0047a3c8  894650                mov       [esi+50h],eax
  0047a3cb  8b4654                mov       eax,[esi+54h]
  0047a3ce  8bf8                  mov       edi,eax
  0047a3d0  c1fa03                sar       edx,3
  0047a3d3  c1ff03                sar       edi,3
  0047a3d6  2bd7                  sub       edx,edi
  0047a3d8  03d0                  add       edx,eax
  0047a3da  8b4658                mov       eax,[esi+58h]
  0047a3dd  2bd1                  sub       edx,ecx
  0047a3df  8bc8                  mov       ecx,eax
  0047a3e1  c1f903                sar       ecx,3
  0047a3e4  2bc1                  sub       eax,ecx
  0047a3e6  895654                mov       [esi+54h],edx
  0047a3e9  894658                mov       [esi+58h],eax
  0047a3ec  8b06                  mov       eax,[esi]
  0047a3ee  25ffffffdf            and       eax,0DFFFFFFFh
  0047a3f3  33ff                  xor       edi,edi
  0047a3f5  8906                  mov       [esi],eax

LAB_0047a3f7:
  0047a3f7  8b5650                mov       edx,[esi+50h]
  0047a3fa  8b5e1c                mov       ebx,[esi+1Ch]
  0047a3fd  8b4654                mov       eax,[esi+54h]
  0047a400  8b4e58                mov       ecx,[esi+58h]
  0047a403  c1fa03                sar       edx,3
  0047a406  03da                  add       ebx,edx
  0047a408  56                    push      esi
  0047a409  895e1c                mov       [esi+1Ch],ebx
  0047a40c  8b5e20                mov       ebx,[esi+20h]
  0047a40f  c1f803                sar       eax,3
  0047a412  03d8                  add       ebx,eax
  0047a414  8b4624                mov       eax,[esi+24h]
  0047a417  c1f903                sar       ecx,3
  0047a41a  03c1                  add       eax,ecx
  0047a41c  895e20                mov       [esi+20h],ebx
  0047a41f  894624                mov       [esi+24h],eax
  0047a422  e819950b00            call      00533940h
  0047a427  8b8e90000000          mov       ecx,[esi+90h]
  0047a42d  83c404                add       esp,4
  0047a430  0bf9                  or        edi,ecx
  0047a432  83fd02                cmp       ebp,2
  0047a435  7506                  jne       short 0047A43Dh
  0047a437  81e7fff8ffff          and       edi,0FFFFF8FFh

LAB_0047a43d:
  0047a43d  4d                    dec       ebp
  0047a43e  75b7                  jne       short 0047A3F7h
  0047a440  8b6c2410              mov       ebp,[esp+10h]
  0047a444  8b5e24                mov       ebx,[esi+24h]
  0047a447  8b442418              mov       eax,[esp+18h]
  0047a44b  8b542414              mov       edx,[esp+14h]
  0047a44f  89be90000000          mov       [esi+90h],edi
  0047a455  8b7e1c                mov       edi,[esi+1Ch]
  0047a458  2bfd                  sub       edi,ebp
  0047a45a  8b6e20                mov       ebp,[esi+20h]
  0047a45d  2bd8                  sub       ebx,eax
  0047a45f  2bea                  sub       ebp,edx
  0047a461  c1ff0c                sar       edi,0Ch
  0047a464  c1fb0c                sar       ebx,0Ch
  0047a467  8bd7                  mov       edx,edi
  0047a469  8bc3                  mov       eax,ebx
  0047a46b  0fafd7                imul      edx,edi
  0047a46e  0fafc3                imul      eax,ebx
  0047a471  03d0                  add       edx,eax
  0047a473  52                    push      edx
  0047a474  c1fd0c                sar       ebp,0Ch
  0047a477  e894b50c00            call      00545A10h
  0047a47c  53                    push      ebx
  0047a47d  57                    push      edi
  0047a47e  89442434              mov       [esp+34h],eax
  0047a482  e89bb50c00            call      00545A22h
  0047a487  8b4c242c              mov       ecx,[esp+2Ch]
  0047a48b  668b7e34              mov       di,[esi+34h]
  0047a48f  0500040000            add       eax,400h
  0047a494  51                    push      ecx
  0047a495  55                    push      ebp
  0047a496  66894632              mov       [esi+32h],ax
  0047a49a  e883b50c00            call      00545A22h
  0047a49f  0fbfd7                movsx     edx,di
  0047a4a2  2bc2                  sub       eax,edx
  0047a4a4  c1f802                sar       eax,2
  0047a4a7  03c7                  add       eax,edi
  0047a4a9  66894634              mov       [esi+34h],ax
  0047a4ad  8b8690000000          mov       eax,[esi+90h]
  0047a4b3  f6c4a0                test      ah,0A0h
  0047a4b6  754d                  jne       short 0047A505h
  0047a4b8  a902080000            test      eax,802h
  0047a4bd  750a                  jne       short 0047A4C9h
  0047a4bf  8b4620                mov       eax,[esi+20h]
  0047a4c2  8b4e60                mov       ecx,[esi+60h]
  0047a4c5  3bc1                  cmp       eax,ecx
  0047a4c7  7f3c                  jg        short 0047A505h

LAB_0047a4c9:
  0047a4c9  8b4660                mov       eax,[esi+60h]
  0047a4cc  8b4e20                mov       ecx,[esi+20h]
  0047a4cf  3bc8                  cmp       ecx,eax
  0047a4d1  7f03                  jg        short 0047A4D6h
  0047a4d3  894620                mov       [esi+20h],eax

LAB_0047a4d6:
  0047a4d6  8b0e                  mov       ecx,[esi]
  0047a4d8  6a04                  push      4
  0047a4da  6a00                  push      0
  0047a4dc  80e17f                and       cl,7Fh
  0047a4df  68bd000000            push      0BDh
  0047a4e4  8d461c                lea       eax,[esi+1Ch]
  0047a4e7  68081a5500            push      551A08h
  0047a4ec  81c900000010          or        ecx,10000000h
  0047a4f2  50                    push      eax
  0047a4f3  56                    push      esi
  0047a4f4  890e                  mov       [esi],ecx
  0047a4f6  c7467800000000        mov       dword [esi+78h],0
  0047a4fd  e83e070000            call      0047AC40h
  0047a502  83c418                add       esp,18h

LAB_0047a505:
  0047a505  8d54241c              lea       edx,[esp+1Ch]
  0047a509  52                    push      edx
  0047a50a  56                    push      esi
  0047a50b  e860a7ffff            call      00474C70h
  0047a510  8bf8                  mov       edi,eax
  0047a512  83c408                add       esp,8
  0047a515  85ff                  test      edi,edi
  0047a517  0f849a000000          je        near 0047A5B7h
  0047a51d  a1b8e05d00            mov       eax,[5DE0B8h]
  0047a522  85c0                  test      eax,eax
  0047a524  7460                  je        short 0047A586h
  0047a526  8b4674                mov       eax,[esi+74h]
  0047a529  57                    push      edi
  0047a52a  50                    push      eax
  0047a52b  e850f60b00            call      00539B80h
  0047a530  0fbf5e32              movsx     ebx,word [esi+32h]
  0047a534  83c408                add       esp,8
  0047a537  81eb00040000          sub       ebx,400h
  0047a53d  53                    push      ebx
  0047a53e  e8d9b40c00            call      00545A1Ch
  0047a543  8b4f50                mov       ecx,[edi+50h]
  0047a546  53                    push      ebx
  0047a547  c1e004                shl       eax,4
  0047a54a  03c8                  add       ecx,eax
  0047a54c  894f50                mov       [edi+50h],ecx
  0047a54f  e8c2b40c00            call      00545A16h
  0047a554  8b4f58                mov       ecx,[edi+58h]
  0047a557  66c787960000001000    mov       word [edi+96h],10h
  0047a560  c1e004                shl       eax,4
  0047a563  03c8                  add       ecx,eax
  0047a565  8b07                  mov       eax,[edi]
  0047a567  0c40                  or        al,40h
  0047a569  894f58                mov       [edi+58h],ecx
  0047a56c  8907                  mov       [edi],eax
  0047a56e  8d461c                lea       eax,[esi+1Ch]
  0047a571  50                    push      eax
  0047a572  6800180000            push      1800h
  0047a577  6800040000            push      400h
  0047a57c  6a03                  push      3
  0047a57e  e89d350c00            call      0053DB20h
  0047a583  83c410                add       esp,10h

LAB_0047a586:
  0047a586  8b0e                  mov       ecx,[esi]
  0047a588  8b561c                mov       edx,[esi+1Ch]
  0047a58b  80e17f                and       cl,7Fh
  0047a58e  8b4620                mov       eax,[esi+20h]
  0047a591  81c900000010          or        ecx,10000000h
  0047a597  897e78                mov       [esi+78h],edi
  0047a59a  890e                  mov       [esi],ecx
  0047a59c  8b5f1c                mov       ebx,[edi+1Ch]
  0047a59f  2bd3                  sub       edx,ebx
  0047a5a1  895644                mov       [esi+44h],edx
  0047a5a4  8b4f20                mov       ecx,[edi+20h]
  0047a5a7  2bc1                  sub       eax,ecx
  0047a5a9  8b4e24                mov       ecx,[esi+24h]
  0047a5ac  894648                mov       [esi+48h],eax
  0047a5af  8b4724                mov       eax,[edi+24h]
  0047a5b2  2bc8                  sub       ecx,eax
  0047a5b4  894e4c                mov       [esi+4Ch],ecx

LAB_0047a5b7:
  0047a5b7  f70600000060          test      dword [esi],60000000h
  0047a5bd  7406                  je        short 0047A5C5h
  0047a5bf  c70600000000          mov       dword [esi],0

LAB_0047a5c5:
  0047a5c5  5f                    pop       edi
  0047a5c6  5e                    pop       esi
  0047a5c7  5d                    pop       ebp
  0047a5c8  5b                    pop       ebx
  0047a5c9  83c418                add       esp,18h
  0047a5cc  c3                    ret
  0047a5cd  90                    nop
  0047a5ce  90                    nop
  0047a5cf  90                    nop

13 cible(s) de saut interne : 0x0047a2c1, 0x0047a2c4, 0x0047a2ea, 0x0047a302, 0x0047a3b0, 0x0047a3f7, 0x0047a43d, 0x0047a4c9, 0x0047a4d6, 0x0047a505, 0x0047a586, 0x0047a5b7, 0x0047a5c5
