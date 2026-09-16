Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00483b50  section .text  offset fichier 0x82f50  352 octets

  00483b50  83ec0c                sub       esp,0Ch
  00483b53  56                    push      esi
  00483b54  8b742414              mov       esi,[esp+14h]
  00483b58  57                    push      edi
  00483b59  66837e1000            cmp       word [esi+10h],0
  00483b5e  8b7e6c                mov       edi,[esi+6Ch]
  00483b61  753f                  jne       short 00483BA2h
  00483b63  8b4644                mov       eax,[esi+44h]
  00483b66  8b4e4c                mov       ecx,[esi+4Ch]
  00483b69  668b969a000000        mov       dx,[esi+9Ah]
  00483b70  897e74                mov       [esi+74h],edi
  00483b73  f7d8                  neg       eax
  00483b75  f7d9                  neg       ecx
  00483b77  66015632              add       [esi+32h],dx
  00483b7b  897e70                mov       [esi+70h],edi
  00483b7e  66c746360400          mov       word [esi+36h],4
  00483b84  c646041e              mov       byte [esi+4],1Eh
  00483b88  c7462800040008        mov       dword [esi+28h],8000400h
  00483b8f  66c7462c0004          mov       word [esi+2Ch],400h
  00483b95  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00483b9c  894644                mov       [esi+44h],eax
  00483b9f  894e4c                mov       [esi+4Ch],ecx

LAB_00483ba2:
  00483ba2  8d442408              lea       eax,[esp+8]
  00483ba6  8d4e44                lea       ecx,[esi+44h]
  00483ba9  50                    push      eax
  00483baa  8d5730                lea       edx,[edi+30h]
  00483bad  51                    push      ecx
  00483bae  52                    push      edx
  00483baf  e8dc240b00            call      00536090h
  00483bb4  8b471c                mov       eax,[edi+1Ch]
  00483bb7  8b4c2414              mov       ecx,[esp+14h]
  00483bbb  8b542418              mov       edx,[esp+18h]
  00483bbf  03c1                  add       eax,ecx
  00483bc1  89461c                mov       [esi+1Ch],eax
  00483bc4  8b4f20                mov       ecx,[edi+20h]
  00483bc7  8b44241c              mov       eax,[esp+1Ch]
  00483bcb  03ca                  add       ecx,edx
  00483bcd  894e20                mov       [esi+20h],ecx
  00483bd0  8b5724                mov       edx,[edi+24h]
  00483bd3  0fbf4e34              movsx     ecx,word [esi+34h]
  00483bd7  83c40c                add       esp,0Ch
  00483bda  03d0                  add       edx,eax
  00483bdc  8b465c                mov       eax,[esi+5Ch]
  00483bdf  895624                mov       [esi+24h],edx
  00483be2  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00483be9  50                    push      eax
  00483bea  51                    push      ecx
  00483beb  0fbf4732              movsx     eax,word [edi+32h]
  00483bef  03d0                  add       edx,eax
  00483bf1  8d4e50                lea       ecx,[esi+50h]
  00483bf4  52                    push      edx
  00483bf5  51                    push      ecx
  00483bf6  e8d5ee0a00            call      00532AD0h
  00483bfb  0fbf7e34              movsx     edi,word [esi+34h]
  00483bff  66c746340000          mov       word [esi+34h],0
  00483c05  8b15cc975e00          mov       edx,[5E97CCh]
  00483c0b  83c410                add       esp,10h
  00483c0e  8b82d0000000          mov       eax,[edx+0D0h]
  00483c14  33d2                  xor       edx,edx
  00483c16  6a02                  push      2
  00483c18  6a00                  push      0
  00483c1a  8b08                  mov       ecx,[eax]
  00483c1c  8a510c                mov       dl,[ecx+0Ch]
  00483c1f  81c2c0000000          add       edx,0C0h
  00483c25  52                    push      edx
  00483c26  56                    push      esi
  00483c27  e8b46effff            call      0047AAE0h
  00483c2c  83c410                add       esp,10h
  00483c2f  66ff4610              inc       word [esi+10h]
  00483c33  66837e1014            cmp       word [esi+10h],14h
  00483c38  66897e34              mov       [esi+34h],di
  00483c3c  7c06                  jl        short 00483C44h
  00483c3e  c70600000000          mov       dword [esi],0

LAB_00483c44:
  00483c44  5f                    pop       edi
  00483c45  5e                    pop       esi
  00483c46  83c40c                add       esp,0Ch
  00483c49  c3                    ret
  00483c4a  90                    nop
  00483c4b  90                    nop
  00483c4c  90                    nop
  00483c4d  90                    nop
  00483c4e  90                    nop
  00483c4f  90                    nop
  00483c50  56                    push      esi
  00483c51  8b742408              mov       esi,[esp+8]
  00483c55  57                    push      edi
  00483c56  33ff                  xor       edi,edi
  00483c58  8b466c                mov       eax,[esi+6Ch]
  00483c5b  8b4e64                mov       ecx,[esi+64h]
  00483c5e  894674                mov       [esi+74h],eax
  00483c61  66c746360400          mov       word [esi+36h],4
  00483c67  897e28                mov       [esi+28h],edi
  00483c6a  66897e2c              mov       [esi+2Ch],di
  00483c6e  897e38                mov       [esi+38h],edi
  00483c71  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  00483c77  897e5c                mov       [esi+5Ch],edi
  00483c7a  894e20                mov       [esi+20h],ecx
  00483c7d  c6460423              mov       byte [esi+4],23h
  00483c81  8b15cc975e00          mov       edx,[5E97CCh]
  00483c87  6a04                  push      4
  00483c89  57                    push      edi
  00483c8a  8b82d0000000          mov       eax,[edx+0D0h]
  00483c90  33d2                  xor       edx,edx
  00483c92  8b08                  mov       ecx,[eax]
  00483c94  8a5113                mov       dl,[ecx+13h]
  00483c97  81c2c0000000          add       edx,0C0h
  00483c9d  52                    push      edx
  00483c9e  56                    push      esi
  00483c9f  e83c6effff            call      0047AAE0h
  00483ca4  83c410                add       esp,10h
  00483ca7  8d461c                lea       eax,[esi+1Ch]
  00483caa  50                    push      eax
  00483cab  6890265500            push      552690h

2 cible(s) de saut interne : 0x00483ba2, 0x00483c44
