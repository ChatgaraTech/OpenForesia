Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484cd0  section .text  offset fichier 0x840d0  208 octets

  00484cd0  83ec0c                sub       esp,0Ch
  00484cd3  53                    push      ebx
  00484cd4  56                    push      esi
  00484cd5  8b742418              mov       esi,[esp+18h]
  00484cd9  57                    push      edi
  00484cda  8b4e44                mov       ecx,[esi+44h]
  00484cdd  8b564c                mov       edx,[esi+4Ch]
  00484ce0  8b7e6c                mov       edi,[esi+6Ch]
  00484ce3  8d4644                lea       eax,[esi+44h]
  00484ce6  f7d9                  neg       ecx
  00484ce8  8908                  mov       [eax],ecx
  00484cea  8d4c240c              lea       ecx,[esp+0Ch]
  00484cee  f7da                  neg       edx
  00484cf0  89564c                mov       [esi+4Ch],edx
  00484cf3  51                    push      ecx
  00484cf4  8d5730                lea       edx,[edi+30h]
  00484cf7  50                    push      eax
  00484cf8  52                    push      edx
  00484cf9  897e74                mov       [esi+74h],edi
  00484cfc  c7462800180018        mov       dword [esi+28h],18001800h
  00484d03  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00484d0a  e881130b00            call      00536090h
  00484d0f  8b471c                mov       eax,[edi+1Ch]
  00484d12  8b4c2418              mov       ecx,[esp+18h]
  00484d16  8b54241c              mov       edx,[esp+1Ch]
  00484d1a  8d5e1c                lea       ebx,[esi+1Ch]
  00484d1d  03c1                  add       eax,ecx
  00484d1f  83c40c                add       esp,0Ch
  00484d22  8903                  mov       [ebx],eax
  00484d24  8b4f20                mov       ecx,[edi+20h]
  00484d27  8b442414              mov       eax,[esp+14h]
  00484d2b  03ca                  add       ecx,edx
  00484d2d  894e20                mov       [esi+20h],ecx
  00484d30  8b5724                mov       edx,[edi+24h]
  00484d33  03d0                  add       edx,eax
  00484d35  6a28                  push      28h
  00484d37  0fbf4634              movsx     eax,word [esi+34h]
  00484d3b  895624                mov       [esi+24h],edx
  00484d3e  50                    push      eax
  00484d3f  0fbf4f32              movsx     ecx,word [edi+32h]
  00484d43  8d5650                lea       edx,[esi+50h]
  00484d46  51                    push      ecx
  00484d47  52                    push      edx
  00484d48  e883dd0a00            call      00532AD0h
  00484d4d  66c7869a0000000030    mov       word [esi+9Ah],3000h
  00484d56  a1cc975e00            mov       eax,[5E97CCh]
  00484d5b  83c410                add       esp,10h
  00484d5e  8b88d4000000          mov       ecx,[eax+0D4h]
  00484d64  33c0                  xor       eax,eax
  00484d66  6a04                  push      4
  00484d68  6a00                  push      0
  00484d6a  8b11                  mov       edx,[ecx]
  00484d6c  8a4209                mov       al,[edx+9]
  00484d6f  05c0000000            add       eax,0C0h
  00484d74  50                    push      eax
  00484d75  68c81b5500            push      551BC8h
  00484d7a  53                    push      ebx
  00484d7b  56                    push      esi
  00484d7c  e8bf5effff            call      0047AC40h
  00484d81  83c418                add       esp,18h
  00484d84  53                    push      ebx
  00484d85  6810275500            push      552710h
  00484d8a  e8518e0b00            call      0053DBE0h
  00484d8f  83c408                add       esp,8
  00484d92  c70600000000          mov       dword [esi],0
  00484d98  5f                    pop       edi
  00484d99  5e                    pop       esi
  00484d9a  5b                    pop       ebx
  00484d9b  83c40c                add       esp,0Ch
  00484d9e  c3                    ret
  00484d9f  90                    nop
