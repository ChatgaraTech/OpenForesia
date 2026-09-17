C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00443c60  section .text  offset fichier 0x43060  72 octets

  00443c60  33d2                  xor       edx,edx

LAB_00443c62:
  00443c62  0fbfca                movsx     ecx,dx
  00443c65  8d0489                lea       eax,[ecx+ecx*4]
  00443c68  c1e003                shl       eax,3
  00443c6b  2bc1                  sub       eax,ecx
  00443c6d  c1e002                shl       eax,2
  00443c70  8b88c0935e00          mov       ecx,[eax+5E93C0h]
  00443c76  f7c100000080          test      ecx,80000000h
  00443c7c  7410                  je        short 00443C8Eh
  00443c7e  f7c100000010          test      ecx,10000000h
  00443c84  7408                  je        short 00443C8Eh
  00443c86  f7c100000040          test      ecx,40000000h
  00443c8c  7414                  je        short 00443CA2h

LAB_00443c8e:
  00443c8e  6683b8d2935e0000      cmp       word [eax+5E93D2h],0
  00443c96  750a                  jne       short 00443CA2h
  00443c98  42                    inc       edx
  00443c99  6683fa03              cmp       dx,3
  00443c9d  7ec3                  jle       short 00443C62h
  00443c9f  33c0                  xor       eax,eax
  00443ca1  c3                    ret

LAB_00443ca2:
  00443ca2  b801000000            mov       eax,1
  00443ca7  c3                    ret

3 cible(s) de saut interne : 0x00443c62, 0x00443c8e, 0x00443ca2
