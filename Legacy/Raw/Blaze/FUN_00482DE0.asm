Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00482de0  section .text  offset fichier 0x821e0  234 octets

  00482de0  53                    push      ebx
  00482de1  55                    push      ebp
  00482de2  56                    push      esi
  00482de3  8b742410              mov       esi,[esp+10h]
  00482de7  57                    push      edi
  00482de8  8b06                  mov       eax,[esi]
  00482dea  6683463280            add       word [esi+32h],0FF80h
  00482def  8b7e6c                mov       edi,[esi+6Ch]
  00482df2  a900000002            test      eax,2000000h
  00482df7  7440                  je        short 00482E39h
  00482df9  668b4628              mov       ax,[esi+28h]
  00482dfd  668bc8                mov       cx,ax
  00482e00  66c1f903              sar       cx,3
  00482e04  2bc1                  sub       eax,ecx
  00482e06  66894628              mov       [esi+28h],ax
  00482e0a  668b4614              mov       ax,[esi+14h]
  00482e0e  668bd0                mov       dx,ax
  00482e11  66c1fa03              sar       dx,3
  00482e15  2bc2                  sub       eax,edx
  00482e17  66894614              mov       [esi+14h],ax
  00482e1b  8b4638                mov       eax,[esi+38h]
  00482e1e  05f8f7f7ff            add       eax,0FFF7F7F8h
  00482e23  894638                mov       [esi+38h],eax
  00482e26  755b                  jne       short 00482E83h
  00482e28  c70600000000          mov       dword [esi],0
  00482e2e  8b07                  mov       eax,[edi]
  00482e30  0d00000001            or        eax,1000000h
  00482e35  8907                  mov       [edi],eax
  00482e37  eb4a                  jmp       short 00482E83h

LAB_00482e39:
  00482e39  8b0f                  mov       ecx,[edi]
  00482e3b  0fbf5612              movsx     edx,word [esi+12h]
  00482e3f  81e100000002          and       ecx,2000000h
  00482e45  0bc8                  or        ecx,eax
  00482e47  668b4628              mov       ax,[esi+28h]
  00482e4b  890e                  mov       [esi],ecx
  00482e4d  0fbfc8                movsx     ecx,ax
  00482e50  2bd1                  sub       edx,ecx
  00482e52  c1fa03                sar       edx,3
  00482e55  03d0                  add       edx,eax
  00482e57  668b4614              mov       ax,[esi+14h]
  00482e5b  66895628              mov       [esi+28h],dx
  00482e5f  0fbf5616              movsx     edx,word [esi+16h]
  00482e63  0fbfc8                movsx     ecx,ax
  00482e66  2bd1                  sub       edx,ecx
  00482e68  c1fa03                sar       edx,3
  00482e6b  03d0                  add       edx,eax
  00482e6d  8b4638                mov       eax,[esi+38h]
  00482e70  3df0f0f000            cmp       eax,0F0F0F0h
  00482e75  66895614              mov       [esi+14h],dx
  00482e79  7308                  jae       short 00482E83h
  00482e7b  0508080800            add       eax,80808h
  00482e80  894638                mov       [esi+38h],eax

LAB_00482e83:
  00482e83  0fbf5e32              movsx     ebx,word [esi+32h]
  00482e87  668b4628              mov       ax,[esi+28h]
  00482e8b  81eb00040000          sub       ebx,400h
  00482e91  0fbf6e14              movsx     ebp,word [esi+14h]
  00482e95  53                    push      ebx
  00482e96  6689462a              mov       [esi+2Ah],ax
  00482e9a  6689462c              mov       [esi+2Ch],ax
  00482e9e  e8792b0c00            call      00545A1Ch
  00482ea3  0fafc5                imul      eax,ebp
  00482ea6  8b4f1c                mov       ecx,[edi+1Ch]
  00482ea9  53                    push      ebx
  00482eaa  03c1                  add       eax,ecx
  00482eac  89461c                mov       [esi+1Ch],eax
  00482eaf  8b5720                mov       edx,[edi+20h]
  00482eb2  895620                mov       [esi+20h],edx
  00482eb5  e85c2b0c00            call      00545A16h
  00482eba  0fafc5                imul      eax,ebp
  00482ebd  8b4f24                mov       ecx,[edi+24h]
  00482ec0  5f                    pop       edi
  00482ec1  03c1                  add       eax,ecx
  00482ec3  894624                mov       [esi+24h],eax
  00482ec6  5e                    pop       esi
  00482ec7  5d                    pop       ebp
  00482ec8  5b                    pop       ebx
  00482ec9  c3                    ret

2 cible(s) de saut interne : 0x00482e39, 0x00482e83
