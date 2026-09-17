C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00417e10  section .text  offset fichier 0x17210  368 octets

  00417e10  53                    push      ebx
  00417e11  56                    push      esi
  00417e12  8b74240c              mov       esi,[esp+0Ch]
  00417e16  33db                  xor       ebx,ebx
  00417e18  57                    push      edi
  00417e19  bf01000000            mov       edi,1
  00417e1e  66395e10              cmp       [esi+10h],bx
  00417e22  7563                  jne       short 00417E87h
  00417e24  66897e10              mov       [esi+10h],di
  00417e28  c7463c58fa5400        mov       dword [esi+3Ch],54FA58h
  00417e2f  66895e28              mov       [esi+28h],bx
  00417e33  66895e2a              mov       [esi+2Ah],bx
  00417e37  c6460a09              mov       byte [esi+0Ah],9
  00417e3b  885e0b                mov       [esi+0Bh],bl
  00417e3e  e8fdf21200            call      00547140h
  00417e43  99                    cdq
  00417e44  b9c8000000            mov       ecx,0C8h
  00417e49  66c746140a00          mov       word [esi+14h],0Ah
  00417e4f  f7f9                  idiv      ecx
  00417e51  8b4620                mov       eax,[esi+20h]
  00417e54  8b4e24                mov       ecx,[esi+24h]
  00417e57  894654                mov       [esi+54h],eax
  00417e5a  894e58                mov       [esi+58h],ecx
  00417e5d  885e40                mov       [esi+40h],bl
  00417e60  81c2fa000000          add       edx,0FAh
  00417e66  66895612              mov       [esi+12h],dx
  00417e6a  8b561c                mov       edx,[esi+1Ch]
  00417e6d  895650                mov       [esi+50h],edx
  00417e70  e8cbf21200            call      00547140h
  00417e75  99                    cdq
  00417e76  33c2                  xor       eax,edx
  00417e78  2bc2                  sub       eax,edx
  00417e7a  25ff0f0000            and       eax,0FFFh
  00417e7f  33c2                  xor       eax,edx
  00417e81  2bc2                  sub       eax,edx
  00417e83  66894616              mov       [esi+16h],ax

LAB_00417e87:
  00417e87  66397e10              cmp       [esi+10h],di
  00417e8b  0f85e3000000          jne       near 00417F74h
  00417e91  b9f4010000            mov       ecx,1F4h
  00417e96  ba00100000            mov       edx,1000h
  00417e9b  66014e28              add       [esi+28h],cx
  00417e9f  668b4628              mov       ax,[esi+28h]
  00417ea3  663bc2                cmp       ax,dx
  00417ea6  7e04                  jle       short 00417EACh
  00417ea8  66895628              mov       [esi+28h],dx

LAB_00417eac:
  00417eac  66014e2a              add       [esi+2Ah],cx
  00417eb0  6639562a              cmp       [esi+2Ah],dx
  00417eb4  7e04                  jle       short 00417EBAh
  00417eb6  6689562a              mov       [esi+2Ah],dx

LAB_00417eba:
  00417eba  0fbf5616              movsx     edx,word [esi+16h]
  00417ebe  52                    push      edx
  00417ebf  e858db1200            call      00545A1Ch
  00417ec4  0fbf4e12              movsx     ecx,word [esi+12h]
  00417ec8  c1f902                sar       ecx,2
  00417ecb  0fafc1                imul      eax,ecx
  00417ece  0fbf5632              movsx     edx,word [esi+32h]
  00417ed2  8b4e50                mov       ecx,[esi+50h]
  00417ed5  52                    push      edx
  00417ed6  03c1                  add       eax,ecx
  00417ed8  89461c                mov       [esi+1Ch],eax
  00417edb  e836db1200            call      00545A16h
  00417ee0  0fbf4e12              movsx     ecx,word [esi+12h]
  00417ee4  0fafc1                imul      eax,ecx
  00417ee7  0fbf5616              movsx     edx,word [esi+16h]
  00417eeb  8b4e58                mov       ecx,[esi+58h]
  00417eee  668b7e12              mov       di,[esi+12h]
  00417ef2  03c1                  add       eax,ecx
  00417ef4  52                    push      edx
  00417ef5  894624                mov       [esi+24h],eax
  00417ef8  e819db1200            call      00545A16h
  00417efd  0fbfcf                movsx     ecx,di
  00417f00  c1f902                sar       ecx,2
  00417f03  0fafc1                imul      eax,ecx
  00417f06  034654                add       eax,[esi+54h]
  00417f09  894620                mov       [esi+20h],eax
  00417f0c  668b4614              mov       ax,[esi+14h]
  00417f10  668bd0                mov       dx,ax
  00417f13  66d1fa                sar       dx,1
  00417f16  2bfa                  sub       edi,edx
  00417f18  40                    inc       eax
  00417f19  66894614              mov       [esi+14h],ax
  00417f1d  668b4616              mov       ax,[esi+16h]
  00417f21  66056400              add       ax,64h
  00417f25  66897e12              mov       [esi+12h],di
  00417f29  25ff0f0000            and       eax,0FFFh
  00417f2e  66894616              mov       [esi+16h],ax
  00417f32  e809f21200            call      00547140h
  00417f37  99                    cdq
  00417f38  b906000000            mov       ecx,6
  00417f3d  f7f9                  idiv      ecx
  00417f3f  8b4e1c                mov       ecx,[esi+1Ch]
  00417f42  83ea03                sub       edx,3
  00417f45  c1e20c                shl       edx,0Ch
  00417f48  03ca                  add       ecx,edx
  00417f4a  894e1c                mov       [esi+1Ch],ecx
  00417f4d  e8eef11200            call      00547140h
  00417f52  99                    cdq
  00417f53  b906000000            mov       ecx,6
  00417f58  f7f9                  idiv      ecx
  00417f5a  8b4e24                mov       ecx,[esi+24h]
  00417f5d  83ea03                sub       edx,3
  00417f60  c1e20c                shl       edx,0Ch
  00417f63  03ca                  add       ecx,edx
  00417f65  66395e12              cmp       [esi+12h],bx
  00417f69  894e24                mov       [esi+24h],ecx
  00417f6c  7d06                  jge       short 00417F74h
  00417f6e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00417f74:
  00417f74  5f                    pop       edi
  00417f75  5e                    pop       esi
  00417f76  5b                    pop       ebx
  00417f77  c3                    ret
  00417f78  90                    nop
  00417f79  90                    nop
  00417f7a  90                    nop
  00417f7b  90                    nop
  00417f7c  90                    nop
  00417f7d  90                    nop
  00417f7e  90                    nop
  00417f7f  90                    nop

4 cible(s) de saut interne : 0x00417e87, 0x00417eac, 0x00417eba, 0x00417f74
