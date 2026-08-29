
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00475d40  section .text  offset fichier 0x75140  347 octets

  00475d40  51                    push      ecx
  00475d41  53                    push      ebx
  00475d42  55                    push      ebp
  00475d43  56                    push      esi
  00475d44  8b742414              mov       esi,[esp+14h]
  00475d48  57                    push      edi
  00475d49  33ff                  xor       edi,edi
  00475d4b  8b466c                mov       eax,[esi+6Ch]
  00475d4e  66c7460a0700          mov       word [esi+0Ah],7
  00475d54  50                    push      eax
  00475d55  89442414              mov       [esp+14h],eax
  00475d59  894670                mov       [esi+70h],eax
  00475d5c  894674                mov       [esi+74h],eax
  00475d5f  894678                mov       [esi+78h],eax
  00475d62  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00475d69  897e44                mov       [esi+44h],edi
  00475d6c  e84f2e0c00            call      00538BC0h
  00475d71  83c404                add       esp,4
  00475d74  24fe                  and       al,0FEh
  00475d76  33c9                  xor       ecx,ecx
  00475d78  bd1f000000            mov       ebp,1Fh
  00475d7d  8a4e0b                mov       cl,[esi+0Bh]
  00475d80  6aff                  push      0FFFFFFFFh
  00475d82  c1e00b                shl       eax,0Bh
  00475d85  894648                mov       [esi+48h],eax
  00475d88  51                    push      ecx
  00475d89  8d461c                lea       eax,[esi+1Ch]
  00475d8c  68be000000            push      0BEh
  00475d91  68a01b5500            push      551BA0h
  00475d96  bb03000000            mov       ebx,3
  00475d9b  50                    push      eax
  00475d9c  56                    push      esi
  00475d9d  897e4c                mov       [esi+4Ch],edi
  00475da0  66896e12              mov       [esi+12h],bp
  00475da4  897e50                mov       [esi+50h],edi
  00475da7  897e54                mov       [esi+54h],edi
  00475daa  897e58                mov       [esi+58h],edi
  00475dad  c7462800200020        mov       dword [esi+28h],20002000h
  00475db4  66897e14              mov       [esi+14h],di
  00475db8  66895e16              mov       [esi+16h],bx
  00475dbc  66c7461a0100          mov       word [esi+1Ah],1
  00475dc2  e8794e0000            call      0047AC40h
  00475dc7  83c418                add       esp,18h
  00475dca  c7462800100010        mov       dword [esi+28h],10001000h
  00475dd1  66c746140010          mov       word [esi+14h],1000h
  00475dd7  66896e16              mov       [esi+16h],bp
  00475ddb  66c746120200          mov       word [esi+12h],2
  00475de1  66895e1a              mov       [esi+1Ah],bx
  00475de5  897e70                mov       [esi+70h],edi
  00475de8  c744241804000000      mov       dword [esp+18h],4

LAB_00475df0:
  00475df0  e84b130d00            call      00547140h
  00475df5  8bf8                  mov       edi,eax
  00475df7  e844130d00            call      00547140h
  00475dfc  57                    push      edi
  00475dfd  8bd8                  mov       ebx,eax
  00475dff  e812fc0c00            call      00545A16h
  00475e04  53                    push      ebx
  00475e05  8be8                  mov       ebp,eax
  00475e07  e810fc0c00            call      00545A1Ch
  00475e0c  0fafc5                imul      eax,ebp
  00475e0f  c1f806                sar       eax,6
  00475e12  24c0                  and       al,0C0h
  00475e14  57                    push      edi
  00475e15  894650                mov       [esi+50h],eax
  00475e18  e8fffb0c00            call      00545A1Ch
  00475e1d  c1e006                shl       eax,6
  00475e20  53                    push      ebx
  00475e21  894654                mov       [esi+54h],eax
  00475e24  e8edfb0c00            call      00545A16h
  00475e29  0fafc5                imul      eax,ebp
  00475e2c  c1f806                sar       eax,6
  00475e2f  33d2                  xor       edx,edx
  00475e31  24c0                  and       al,0C0h
  00475e33  8a560b                mov       dl,[esi+0Bh]
  00475e36  6aff                  push      0FFFFFFFFh
  00475e38  894658                mov       [esi+58h],eax
  00475e3b  52                    push      edx
  00475e3c  68be000000            push      0BEh
  00475e41  8d461c                lea       eax,[esi+1Ch]
  00475e44  68b8195500            push      5519B8h
  00475e49  50                    push      eax
  00475e4a  56                    push      esi
  00475e4b  e8f04d0000            call      0047AC40h
  00475e50  8b442430              mov       eax,[esp+30h]
  00475e54  83c418                add       esp,18h
  00475e57  48                    dec       eax
  00475e58  89442418              mov       [esp+18h],eax
  00475e5c  7592                  jne       short 00475DF0h
  00475e5e  33c0                  xor       eax,eax
  00475e60  6aff                  push      0FFFFFFFFh
  00475e62  8a460b                mov       al,[esi+0Bh]
  00475e65  50                    push      eax
  00475e66  8b442418              mov       eax,[esp+18h]
  00475e6a  6a29                  push      29h
  00475e6c  6808485600            push      564808h
  00475e71  8d481c                lea       ecx,[eax+1Ch]
  00475e74  51                    push      ecx
  00475e75  50                    push      eax
  00475e76  e8c54d0000            call      0047AC40h
  00475e7b  83c418                add       esp,18h
  00475e7e  8d461c                lea       eax,[esi+1Ch]
  00475e81  50                    push      eax
  00475e82  6868185500            push      551868h
  00475e87  e8547d0c00            call      0053DBE0h
  00475e8c  83c408                add       esp,8
  00475e8f  c70600000000          mov       dword [esi],0
  00475e95  5f                    pop       edi
  00475e96  5e                    pop       esi
  00475e97  5d                    pop       ebp
  00475e98  5b                    pop       ebx
  00475e99  59                    pop       ecx
  00475e9a  c3                    ret

1 cible(s) de saut interne : 0x00475df0


