C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041afa0  section .text  offset fichier 0x1a3a0  640 octets

  0041afa0  53                    push      ebx
  0041afa1  56                    push      esi
  0041afa2  8b74240c              mov       esi,[esp+0Ch]
  0041afa6  33db                  xor       ebx,ebx
  0041afa8  57                    push      edi
  0041afa9  bf01000000            mov       edi,1
  0041afae  66395e10              cmp       [esi+10h],bx
  0041afb2  0f85bf000000          jne       near 0041B077h
  0041afb8  66897e10              mov       [esi+10h],di
  0041afbc  885e04                mov       [esi+4],bl
  0041afbf  c6460a09              mov       byte [esi+0Ah],9
  0041afc3  885e0b                mov       [esi+0Bh],bl
  0041afc6  e875c11200            call      00547140h
  0041afcb  99                    cdq
  0041afcc  33c2                  xor       eax,edx
  0041afce  2bc2                  sub       eax,edx
  0041afd0  25ff030000            and       eax,3FFh
  0041afd5  33c2                  xor       eax,edx
  0041afd7  2bc2                  sub       eax,edx
  0041afd9  66837e1628            cmp       word [esi+16h],28h
  0041afde  7455                  je        short 0041B035h
  0041afe0  0500040000            add       eax,400h
  0041afe5  66895e2a              mov       [esi+2Ah],bx
  0041afe9  66894628              mov       [esi+28h],ax
  0041afed  6689462c              mov       [esi+2Ch],ax
  0041aff1  e84ac11200            call      00547140h
  0041aff6  99                    cdq
  0041aff7  33c2                  xor       eax,edx
  0041aff9  2bc2                  sub       eax,edx
  0041affb  25ff0f0000            and       eax,0FFFh
  0041b000  33c2                  xor       eax,edx
  0041b002  2bc2                  sub       eax,edx
  0041b004  66894632              mov       [esi+32h],ax
  0041b008  e833c11200            call      00547140h
  0041b00d  99                    cdq
  0041b00e  33c2                  xor       eax,edx
  0041b010  2bc2                  sub       eax,edx
  0041b012  25ff0f0000            and       eax,0FFFh
  0041b017  33c2                  xor       eax,edx
  0041b019  2bc2                  sub       eax,edx
  0041b01b  66894612              mov       [esi+12h],ax
  0041b01f  e81cc11200            call      00547140h
  0041b024  99                    cdq
  0041b025  b964000000            mov       ecx,64h
  0041b02a  f7f9                  idiv      ecx
  0041b02c  83c228                add       edx,28h
  0041b02f  66895614              mov       [esi+14h],dx
  0041b033  eb30                  jmp       short 0041B065h

LAB_0041b035:
  0041b035  b870170000            mov       eax,1770h
  0041b03a  66895e2a              mov       [esi+2Ah],bx
  0041b03e  66894628              mov       [esi+28h],ax
  0041b042  6689462c              mov       [esi+2Ch],ax
  0041b046  e8f5c01200            call      00547140h
  0041b04b  99                    cdq
  0041b04c  33c2                  xor       eax,edx
  0041b04e  66895e12              mov       [esi+12h],bx
  0041b052  2bc2                  sub       eax,edx
  0041b054  66895e14              mov       [esi+14h],bx
  0041b058  25ff0f0000            and       eax,0FFFh
  0041b05d  33c2                  xor       eax,edx
  0041b05f  2bc2                  sub       eax,edx
  0041b061  66894632              mov       [esi+32h],ax

LAB_0041b065:
  0041b065  8b561c                mov       edx,[esi+1Ch]
  0041b068  8b4620                mov       eax,[esi+20h]
  0041b06b  8b4e24                mov       ecx,[esi+24h]
  0041b06e  895650                mov       [esi+50h],edx
  0041b071  894654                mov       [esi+54h],eax
  0041b074  894e58                mov       [esi+58h],ecx

LAB_0041b077:
  0041b077  66397e10              cmp       [esi+10h],di
  0041b07b  0f8583000000          jne       near 0041B104h
  0041b081  66ff4616              inc       word [esi+16h]
  0041b085  e8b6c01200            call      00547140h
  0041b08a  99                    cdq
  0041b08b  b914000000            mov       ecx,14h
  0041b090  f7f9                  idiv      ecx
  0041b092  663bd7                cmp       dx,di
  0041b095  7516                  jne       short 0041B0ADh
  0041b097  6aff                  push      0FFFFFFFFh
  0041b099  53                    push      ebx
  0041b09a  68fc000000            push      0FCh
  0041b09f  8d561c                lea       edx,[esi+1Ch]
  0041b0a2  53                    push      ebx
  0041b0a3  52                    push      edx
  0041b0a4  56                    push      esi
  0041b0a5  e896fb0500            call      0047AC40h
  0041b0aa  83c418                add       esp,18h

LAB_0041b0ad:
  0041b0ad  66837e163c            cmp       word [esi+16h],3Ch
  0041b0b2  7c0a                  jl        short 0041B0BEh
  0041b0b4  66c746100200          mov       word [esi+10h],2
  0041b0ba  66895e16              mov       [esi+16h],bx

LAB_0041b0be:
  0041b0be  6681462a0004          add       word [esi+2Ah],400h
  0041b0c4  668b462a              mov       ax,[esi+2Ah]
  0041b0c8  663d3075              cmp       ax,7530h
  0041b0cc  7e06                  jle       short 0041B0D4h
  0041b0ce  66c7462a3075          mov       word [esi+2Ah],7530h

LAB_0041b0d4:
  0041b0d4  0fbf4612              movsx     eax,word [esi+12h]
  0041b0d8  50                    push      eax
  0041b0d9  e83ea91200            call      00545A1Ch
  0041b0de  0fbf4e14              movsx     ecx,word [esi+14h]
  0041b0e2  0fafc1                imul      eax,ecx
  0041b0e5  0fbf5612              movsx     edx,word [esi+12h]
  0041b0e9  8b4e50                mov       ecx,[esi+50h]
  0041b0ec  52                    push      edx
  0041b0ed  03c1                  add       eax,ecx
  0041b0ef  89461c                mov       [esi+1Ch],eax
  0041b0f2  e81fa91200            call      00545A16h
  0041b0f7  0fbf4e14              movsx     ecx,word [esi+14h]
  0041b0fb  0fafc1                imul      eax,ecx
  0041b0fe  034658                add       eax,[esi+58h]
  0041b101  894624                mov       [esi+24h],eax

LAB_0041b104:
  0041b104  66837e1002            cmp       word [esi+10h],2
  0041b109  7562                  jne       short 0041B16Dh
  0041b10b  668b4612              mov       ax,[esi+12h]
  0041b10f  66ff4616              inc       word [esi+16h]
  0041b113  668146328000          add       word [esi+32h],80h
  0041b119  663d0010              cmp       ax,1000h
  0041b11d  7c09                  jl        short 0041B128h
  0041b11f  0500f0ffff            add       eax,0FFFFF000h
  0041b124  66894612              mov       [esi+12h],ax

LAB_0041b128:
  0041b128  0fbf5612              movsx     edx,word [esi+12h]
  0041b12c  52                    push      edx
  0041b12d  e8eaa81200            call      00545A1Ch
  0041b132  0fbf4e14              movsx     ecx,word [esi+14h]
  0041b136  0fafc1                imul      eax,ecx
  0041b139  0fbf5612              movsx     edx,word [esi+12h]
  0041b13d  8b4e50                mov       ecx,[esi+50h]
  0041b140  668b7e14              mov       di,[esi+14h]
  0041b144  03c1                  add       eax,ecx
  0041b146  52                    push      edx
  0041b147  89461c                mov       [esi+1Ch],eax
  0041b14a  e8c7a81200            call      00545A16h
  0041b14f  0fbfcf                movsx     ecx,di
  0041b152  0fafc1                imul      eax,ecx
  0041b155  034658                add       eax,[esi+58h]
  0041b158  894624                mov       [esi+24h],eax
  0041b15b  8d47fb                lea       eax,[edi-5]
  0041b15e  663bc3                cmp       ax,bx
  0041b161  66894614              mov       [esi+14h],ax
  0041b165  7d06                  jge       short 0041B16Dh
  0041b167  66c746100300          mov       word [esi+10h],3

LAB_0041b16d:
  0041b16d  66837e1003            cmp       word [esi+10h],3
  0041b172  0f8595000000          jne       near 0041B20Dh
  0041b178  66ff4616              inc       word [esi+16h]
  0041b17c  668b4e16              mov       cx,[esi+16h]
  0041b180  ba9cffffff            mov       edx,0FFFFFF9Ch
  0041b185  bf5a000000            mov       edi,5Ah
  0041b18a  66015628              add       [esi+28h],dx
  0041b18e  668b4628              mov       ax,[esi+28h]
  0041b192  6601562c              add       [esi+2Ch],dx
  0041b196  663bc7                cmp       ax,di
  0041b199  7d08                  jge       short 0041B1A3h
  0041b19b  66897e28              mov       [esi+28h],di
  0041b19f  66897e2c              mov       [esi+2Ch],di

LAB_0041b1a3:
  0041b1a3  6683f946              cmp       cx,46h
  0041b1a7  7519                  jne       short 0041B1C2h
  0041b1a9  6800080000            push      800h
  0041b1ae  6800080000            push      800h
  0041b1b3  68e9010000            push      1E9h
  0041b1b8  6a0f                  push      0Fh
  0041b1ba  e8e11c1200            call      0053CEA0h
  0041b1bf  83c410                add       esp,10h

LAB_0041b1c2:
  0041b1c2  66837e1646            cmp       word [esi+16h],46h
  0041b1c7  7e38                  jle       short 0041B201h
  0041b1c9  81462000600900        add       dword [esi+20h],96000h
  0041b1d0  e86bbf1200            call      00547140h
  0041b1d5  99                    cdq
  0041b1d6  b903000000            mov       ecx,3
  0041b1db  f7f9                  idiv      ecx
  0041b1dd  663bd3                cmp       dx,bx
  0041b1e0  751f                  jne       short 0041B201h
  0041b1e2  803de07c5f0064        cmp       byte [5F7CE0h],64h
  0041b1e9  7316                  jae       short 0041B201h
  0041b1eb  6aff                  push      0FFFFFFFFh
  0041b1ed  53                    push      ebx
  0041b1ee  68fb000000            push      0FBh
  0041b1f3  8d561c                lea       edx,[esi+1Ch]
  0041b1f6  53                    push      ebx
  0041b1f7  52                    push      edx
  0041b1f8  56                    push      esi
  0041b1f9  e842fa0500            call      0047AC40h
  0041b1fe  83c418                add       esp,18h

LAB_0041b201:
  0041b201  66397e16              cmp       [esi+16h],di
  0041b205  7e06                  jle       short 0041B20Dh
  0041b207  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041b20d:
  0041b20d  5f                    pop       edi
  0041b20e  5e                    pop       esi
  0041b20f  5b                    pop       ebx
  0041b210  c3                    ret
  0041b211  90                    nop
  0041b212  90                    nop
  0041b213  90                    nop
  0041b214  90                    nop
  0041b215  90                    nop
  0041b216  90                    nop
  0041b217  90                    nop
  0041b218  90                    nop
  0041b219  90                    nop
  0041b21a  90                    nop
  0041b21b  90                    nop
  0041b21c  90                    nop
  0041b21d  90                    nop
  0041b21e  90                    nop
  0041b21f  90                    nop

13 cible(s) de saut interne : 0x0041b035, 0x0041b065, 0x0041b077, 0x0041b0ad, 0x0041b0be, 0x0041b0d4, 0x0041b104, 0x0041b128, 0x0041b16d, 0x0041b1a3, 0x0041b1c2, 0x0041b201, 0x0041b20d
