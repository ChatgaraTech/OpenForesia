Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047bd00  section .text  offset fichier 0x7b100  239 octets

  0047bd00  53                    push      ebx
  0047bd01  56                    push      esi
  0047bd02  8b74240c              mov       esi,[esp+0Ch]
  0047bd06  57                    push      edi
  0047bd07  66837e1000            cmp       word [esi+10h],0
  0047bd0c  8b7e78                mov       edi,[esi+78h]
  0047bd0f  7533                  jne       short 0047BD44h
  0047bd11  0fbf4612              movsx     eax,word [esi+12h]
  0047bd15  8bc8                  mov       ecx,eax
  0047bd17  6689462c              mov       [esi+2Ch],ax
  0047bd1b  c1e110                shl       ecx,10h
  0047bd1e  0bc8                  or        ecx,eax
  0047bd20  894e28                mov       [esi+28h],ecx
  0047bd23  e818b40c00            call      00547140h
  0047bd28  66894630              mov       [esi+30h],ax
  0047bd2c  e80fb40c00            call      00547140h
  0047bd31  66894632              mov       [esi+32h],ax
  0047bd35  e806b40c00            call      00547140h
  0047bd3a  66894634              mov       [esi+34h],ax
  0047bd3e  c646041d              mov       byte [esi+4],1Dh
  0047bd42  eb3e                  jmp       short 0047BD82h

LAB_0047bd44:
  0047bd44  0fbf4614              movsx     eax,word [esi+14h]
  0047bd48  668b4e28              mov       cx,[esi+28h]
  0047bd4c  8bd8                  mov       ebx,eax
  0047bd4e  0fbfd1                movsx     edx,cx
  0047bd51  2bda                  sub       ebx,edx
  0047bd53  c1fb02                sar       ebx,2
  0047bd56  03d9                  add       ebx,ecx
  0047bd58  668b4e2a              mov       cx,[esi+2Ah]
  0047bd5c  0fbfd1                movsx     edx,cx
  0047bd5f  66895e28              mov       [esi+28h],bx
  0047bd63  8bd8                  mov       ebx,eax
  0047bd65  2bda                  sub       ebx,edx
  0047bd67  c1fb02                sar       ebx,2
  0047bd6a  03d9                  add       ebx,ecx
  0047bd6c  668b4e2c              mov       cx,[esi+2Ch]
  0047bd70  0fbfd1                movsx     edx,cx
  0047bd73  2bc2                  sub       eax,edx
  0047bd75  66895e2a              mov       [esi+2Ah],bx
  0047bd79  c1f802                sar       eax,2
  0047bd7c  03c1                  add       eax,ecx
  0047bd7e  6689462c              mov       [esi+2Ch],ax

LAB_0047bd82:
  0047bd82  0fbf4610              movsx     eax,word [esi+10h]
  0047bd86  c1e007                shl       eax,7
  0047bd89  50                    push      eax
  0047bd8a  e88d9c0c00            call      00545A1Ch
  0047bd8f  8bc8                  mov       ecx,eax
  0047bd91  c1e108                shl       ecx,8
  0047bd94  2bc8                  sub       ecx,eax
  0047bd96  8bc1                  mov       eax,ecx
  0047bd98  c1f80c                sar       eax,0Ch
  0047bd9b  8bd0                  mov       edx,eax
  0047bd9d  c1e208                shl       edx,8
  0047bda0  0bd0                  or        edx,eax
  0047bda2  c1e208                shl       edx,8
  0047bda5  0bd0                  or        edx,eax
  0047bda7  895638                mov       [esi+38h],edx
  0047bdaa  8b471c                mov       eax,[edi+1Ch]
  0047bdad  8b5674                mov       edx,[esi+74h]
  0047bdb0  89461c                mov       [esi+1Ch],eax
  0047bdb3  8b4f24                mov       ecx,[edi+24h]
  0047bdb6  52                    push      edx
  0047bdb7  894e24                mov       [esi+24h],ecx
  0047bdba  e801ce0b00            call      00538BC0h
  0047bdbf  8b5f20                mov       ebx,[edi+20h]
  0047bdc2  6683463260            add       word [esi+32h],60h
  0047bdc7  668146348000          add       word [esi+34h],80h
  0047bdcd  24fe                  and       al,0FEh
  0047bdcf  c1e00b                shl       eax,0Bh
  0047bdd2  83c404                add       esp,4
  0047bdd5  03c3                  add       eax,ebx
  0047bdd7  66ff4610              inc       word [esi+10h]
  0047bddb  66837e1010            cmp       word [esi+10h],10h
  0047bde0  894620                mov       [esi+20h],eax
  0047bde3  7c06                  jl        short 0047BDEBh
  0047bde5  c70600000000          mov       dword [esi],0

LAB_0047bdeb:
  0047bdeb  5f                    pop       edi
  0047bdec  5e                    pop       esi
  0047bded  5b                    pop       ebx
  0047bdee  c3                    ret

3 cible(s) de saut interne : 0x0047bd44, 0x0047bd82, 0x0047bdeb
