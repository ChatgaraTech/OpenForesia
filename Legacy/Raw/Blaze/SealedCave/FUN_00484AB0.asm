Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484ab0  section .text  offset fichier 0x83eb0  256 octets

  00484ab0  83ec0c                sub       esp,0Ch
  00484ab3  53                    push      ebx
  00484ab4  56                    push      esi
  00484ab5  8b742418              mov       esi,[esp+18h]
  00484ab9  33db                  xor       ebx,ebx
  00484abb  57                    push      edi
  00484abc  66395e10              cmp       [esi+10h],bx
  00484ac0  8b7e6c                mov       edi,[esi+6Ch]
  00484ac3  7555                  jne       short 00484B1Ah
  00484ac5  8b4644                mov       eax,[esi+44h]
  00484ac8  8b4e4c                mov       ecx,[esi+4Ch]
  00484acb  668146320004          add       word [esi+32h],400h
  00484ad1  8d561c                lea       edx,[esi+1Ch]
  00484ad4  f7d8                  neg       eax
  00484ad6  f7d9                  neg       ecx
  00484ad8  52                    push      edx
  00484ad9  68e0265500            push      5526E0h
  00484ade  897e74                mov       [esi+74h],edi
  00484ae1  897e70                mov       [esi+70h],edi
  00484ae4  66c746360400          mov       word [esi+36h],4
  00484aea  c6460407              mov       byte [esi+4],7
  00484aee  895e28                mov       [esi+28h],ebx
  00484af1  66895e2c              mov       [esi+2Ch],bx
  00484af5  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00484afc  66899e9a000000        mov       [esi+9Ah],bx
  00484b03  66c746348003          mov       word [esi+34h],380h
  00484b09  894644                mov       [esi+44h],eax
  00484b0c  894e4c                mov       [esi+4Ch],ecx
  00484b0f  885e09                mov       [esi+9],bl
  00484b12  e8c9900b00            call      0053DBE0h
  00484b17  83c408                add       esp,8

LAB_00484b1a:
  00484b1a  f6461001              test      byte [esi+10h],1
  00484b1e  7477                  je        short 00484B97h
  00484b20  8d44240c              lea       eax,[esp+0Ch]
  00484b24  8d4e44                lea       ecx,[esi+44h]
  00484b27  50                    push      eax
  00484b28  8d5730                lea       edx,[edi+30h]
  00484b2b  51                    push      ecx
  00484b2c  52                    push      edx
  00484b2d  e85e150b00            call      00536090h
  00484b32  8b471c                mov       eax,[edi+1Ch]
  00484b35  8b4c2418              mov       ecx,[esp+18h]
  00484b39  8b54241c              mov       edx,[esp+1Ch]
  00484b3d  03c1                  add       eax,ecx
  00484b3f  89461c                mov       [esi+1Ch],eax
  00484b42  8b4f20                mov       ecx,[edi+20h]
  00484b45  03ca                  add       ecx,edx
  00484b47  83c40c                add       esp,0Ch
  00484b4a  894e20                mov       [esi+20h],ecx
  00484b4d  8b5724                mov       edx,[edi+24h]
  00484b50  8b4c2414              mov       ecx,[esp+14h]
  00484b54  6a64                  push      64h
  00484b56  03d1                  add       edx,ecx
  00484b58  68800f0000            push      0F80h
  00484b5d  895624                mov       [esi+24h],edx
  00484b60  8d4e50                lea       ecx,[esi+50h]
  00484b63  0fbf4732              movsx     eax,word [edi+32h]
  00484b67  50                    push      eax
  00484b68  51                    push      ecx
  00484b69  e862df0a00            call      00532AD0h
  00484b6e  8b15cc975e00          mov       edx,[5E97CCh]
  00484b74  83c410                add       esp,10h
  00484b77  8b82d0000000          mov       eax,[edx+0D0h]
  00484b7d  33d2                  xor       edx,edx
  00484b7f  6a02                  push      2
  00484b81  53                    push      ebx
  00484b82  8b08                  mov       ecx,[eax]
  00484b84  8a510b                mov       dl,[ecx+0Bh]
  00484b87  81c2c0000000          add       edx,0C0h
  00484b8d  52                    push      edx
  00484b8e  56                    push      esi
  00484b8f  e84c5fffff            call      0047AAE0h
  00484b94  83c410                add       esp,10h

LAB_00484b97:
  00484b97  66ff4610              inc       word [esi+10h]
  00484b9b  66837e1008            cmp       word [esi+10h],8
  00484ba0  7c02                  jl        short 00484BA4h
  00484ba2  891e                  mov       [esi],ebx

LAB_00484ba4:
  00484ba4  5f                    pop       edi
  00484ba5  5e                    pop       esi
  00484ba6  5b                    pop       ebx
  00484ba7  83c40c                add       esp,0Ch
  00484baa  c3                    ret
  00484bab  90                    nop
  00484bac  90                    nop
  00484bad  90                    nop
  00484bae  90                    nop
  00484baf  90                    nop

3 cible(s) de saut interne : 0x00484b1a, 0x00484b97, 0x00484ba4
