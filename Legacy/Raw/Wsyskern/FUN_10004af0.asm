
Legacy/Binaries/wsyskern.dll  base 0x10000000
0x10004af0  section .text  offset fichier 0x3ef0  160 octets

  10004af0  51                    push      ecx
  10004af1  53                    push      ebx
  10004af2  8bd9                  mov       ebx,ecx
  10004af4  55                    push      ebp
  10004af5  8b6c2410              mov       ebp,[esp+10h]
  10004af9  8b4310                mov       eax,[ebx+10h]
  10004afc  89442408              mov       [esp+8],eax
  10004b00  8b430c                mov       eax,[ebx+0Ch]
  10004b03  3be8                  cmp       ebp,eax
  10004b05  7f0e                  jg        short 10004B15h
  10004b07  896b0c                mov       [ebx+0Ch],ebp
  10004b0a  b801000000            mov       eax,1
  10004b0f  5d                    pop       ebp
  10004b10  5b                    pop       ebx
  10004b11  59                    pop       ecx
  10004b12  c20400                ret       4

LAB_10004b15:
  10004b15  57                    push      edi
  10004b16  56                    push      esi
  10004b17  8d34ad00000000        lea       esi,[ebp*4]
  10004b1e  56                    push      esi
  10004b1f  e89c700000            call      1000BBC0h
  10004b24  8bce                  mov       ecx,esi
  10004b26  8bf8                  mov       edi,eax
  10004b28  8bd1                  mov       edx,ecx
  10004b2a  33c0                  xor       eax,eax
  10004b2c  c1e902                shr       ecx,2
  10004b2f  897b10                mov       [ebx+10h],edi
  10004b32  f3ab                  rep stosd
  10004b34  8bca                  mov       ecx,edx
  10004b36  83e103                and       ecx,3
  10004b39  f3aa                  rep stosb
  10004b3b  8b4b0c                mov       ecx,[ebx+0Ch]
  10004b3e  8b442410              mov       eax,[esp+10h]
  10004b42  8b7b10                mov       edi,[ebx+10h]
  10004b45  8bf0                  mov       esi,eax
  10004b47  c1e102                shl       ecx,2
  10004b4a  8bd1                  mov       edx,ecx
  10004b4c  50                    push      eax
  10004b4d  c1e902                shr       ecx,2
  10004b50  f3a5                  rep movsd
  10004b52  8bca                  mov       ecx,edx
  10004b54  83e103                and       ecx,3
  10004b57  f3a4                  rep movsb
  10004b59  896b0c                mov       [ebx+0Ch],ebp
  10004b5c  e8cf700000            call      1000BC30h
  10004b61  5e                    pop       esi
  10004b62  5f                    pop       edi
  10004b63  5d                    pop       ebp
  10004b64  b801000000            mov       eax,1
  10004b69  5b                    pop       ebx
  10004b6a  59                    pop       ecx
  10004b6b  c20400                ret       4
  10004b6e  90                    nop
  10004b6f  90                    nop
  10004b70  8b442404              mov       eax,[esp+4]
  10004b74  8b510c                mov       edx,[ecx+0Ch]
  10004b77  3bc2                  cmp       eax,edx
  10004b79  7205                  jb        short 10004B80h
  10004b7b  33c0                  xor       eax,eax
  10004b7d  c20400                ret       4

LAB_10004b80:
  10004b80  8b4910                mov       ecx,[ecx+10h]
  10004b83  8b0481                mov       eax,[ecx+eax*4]
  10004b86  c20400                ret       4
  10004b89  90                    nop
  10004b8a  90                    nop
  10004b8b  90                    nop
  10004b8c  90                    nop
  10004b8d  90                    nop
  10004b8e  90                    nop
  10004b8f  90                    nop

2 cible(s) de saut interne : 0x10004b15, 0x10004b80
