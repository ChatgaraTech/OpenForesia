
Legacy/Binaries/wsyskern.dll  base 0x10000000
0x10004740  section .text  offset fichier 0x3b40  160 octets

  10004740  51                    push      ecx
  10004741  53                    push      ebx
  10004742  8bd9                  mov       ebx,ecx
  10004744  55                    push      ebp
  10004745  8b6c2410              mov       ebp,[esp+10h]
  10004749  8b4310                mov       eax,[ebx+10h]
  1000474c  89442408              mov       [esp+8],eax
  10004750  8b430c                mov       eax,[ebx+0Ch]
  10004753  3be8                  cmp       ebp,eax
  10004755  7f0e                  jg        short 10004765h
  10004757  896b0c                mov       [ebx+0Ch],ebp
  1000475a  b801000000            mov       eax,1
  1000475f  5d                    pop       ebp
  10004760  5b                    pop       ebx
  10004761  59                    pop       ecx
  10004762  c20400                ret       4

LAB_10004765:
  10004765  57                    push      edi
  10004766  56                    push      esi
  10004767  8d34ed00000000        lea       esi,[ebp*8]
  1000476e  56                    push      esi
  1000476f  e84c740000            call      1000BBC0h
  10004774  8bce                  mov       ecx,esi
  10004776  8bf8                  mov       edi,eax
  10004778  8bd1                  mov       edx,ecx
  1000477a  33c0                  xor       eax,eax
  1000477c  c1e902                shr       ecx,2
  1000477f  897b10                mov       [ebx+10h],edi
  10004782  f3ab                  rep stosd
  10004784  8bca                  mov       ecx,edx
  10004786  83e103                and       ecx,3
  10004789  f3aa                  rep stosb
  1000478b  8b4b0c                mov       ecx,[ebx+0Ch]
  1000478e  8b442410              mov       eax,[esp+10h]
  10004792  8b7b10                mov       edi,[ebx+10h]
  10004795  8bf0                  mov       esi,eax
  10004797  c1e103                shl       ecx,3
  1000479a  8bd1                  mov       edx,ecx
  1000479c  50                    push      eax
  1000479d  c1e902                shr       ecx,2
  100047a0  f3a5                  rep movsd
  100047a2  8bca                  mov       ecx,edx
  100047a4  83e103                and       ecx,3
  100047a7  f3a4                  rep movsb
  100047a9  896b0c                mov       [ebx+0Ch],ebp
  100047ac  e87f740000            call      1000BC30h
  100047b1  5e                    pop       esi
  100047b2  5f                    pop       edi
  100047b3  5d                    pop       ebp
  100047b4  b801000000            mov       eax,1
  100047b9  5b                    pop       ebx
  100047ba  59                    pop       ecx
  100047bb  c20400                ret       4
  100047be  90                    nop
  100047bf  90                    nop
  100047c0  8b442404              mov       eax,[esp+4]
  100047c4  8b510c                mov       edx,[ecx+0Ch]
  100047c7  3bc2                  cmp       eax,edx
  100047c9  7205                  jb        short 100047D0h
  100047cb  33c0                  xor       eax,eax
  100047cd  c20400                ret       4

LAB_100047d0:
  100047d0  8b4910                mov       ecx,[ecx+10h]
  100047d3  8d04c1                lea       eax,[ecx+eax*8]
  100047d6  c20400                ret       4
  100047d9  90                    nop
  100047da  90                    nop
  100047db  90                    nop
  100047dc  90                    nop
  100047dd  90                    nop
  100047de  90                    nop
  100047df  90                    nop

2 cible(s) de saut interne : 0x10004765, 0x100047d0
