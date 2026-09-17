C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00416a80  section .text  offset fichier 0x15e80  448 octets

  00416a80  83ec08                sub       esp,8
  00416a83  53                    push      ebx
  00416a84  55                    push      ebp
  00416a85  56                    push      esi
  00416a86  8b742418              mov       esi,[esp+18h]
  00416a8a  57                    push      edi
  00416a8b  8b5e6c                mov       ebx,[esi+6Ch]
  00416a8e  85db                  test      ebx,ebx
  00416a90  7420                  je        short 00416AB2h
  00416a92  8b431c                mov       eax,[ebx+1Ch]
  00416a95  89461c                mov       [esi+1Ch],eax
  00416a98  8b4b20                mov       ecx,[ebx+20h]
  00416a9b  81c100800c00          add       ecx,0C8000h
  00416aa1  894e20                mov       [esi+20h],ecx
  00416aa4  8b5324                mov       edx,[ebx+24h]
  00416aa7  895624                mov       [esi+24h],edx
  00416aaa  668b4332              mov       ax,[ebx+32h]
  00416aae  66894632              mov       [esi+32h],ax

LAB_00416ab2:
  00416ab2  66837e1000            cmp       word [esi+10h],0
  00416ab7  752e                  jne       short 00416AE7h
  00416ab9  6a00                  push      0
  00416abb  6a00                  push      0
  00416abd  6800100000            push      1000h
  00416ac2  68fd060000            push      6FDh
  00416ac7  6a1f                  push      1Fh
  00416ac9  56                    push      esi
  00416aca  66c746100100          mov       word [esi+10h],1
  00416ad0  66c746161e00          mov       word [esi+16h],1Eh
  00416ad6  66c78698000000ffff    mov       word [esi+98h],0FFFFh
  00416adf  e89c150500            call      00468080h
  00416ae4  83c418                add       esp,18h

LAB_00416ae7:
  00416ae7  6683be9800000000      cmp       word [esi+98h],0
  00416aef  7519                  jne       short 00416B0Ah
  00416af1  6a00                  push      0
  00416af3  6a00                  push      0
  00416af5  6800100000            push      1000h
  00416afa  6800100000            push      1000h
  00416aff  6a23                  push      23h
  00416b01  56                    push      esi
  00416b02  e879150500            call      00468080h
  00416b07  83c418                add       esp,18h

LAB_00416b0a:
  00416b0a  66ff8698000000        inc       word [esi+98h]
  00416b11  6683be9800000005      cmp       word [esi+98h],5
  00416b19  7509                  jne       short 00416B24h
  00416b1b  66c786980000000000    mov       word [esi+98h],0

LAB_00416b24:
  00416b24  0fbf4632              movsx     eax,word [esi+32h]
  00416b28  8b4e20                mov       ecx,[esi+20h]
  00416b2b  8b5624                mov       edx,[esi+24h]
  00416b2e  8b6e1c                mov       ebp,[esi+1Ch]
  00416b31  8d7e1c                lea       edi,[esi+1Ch]
  00416b34  50                    push      eax
  00416b35  894c2418              mov       [esp+18h],ecx
  00416b39  89542420              mov       [esp+20h],edx
  00416b3d  89442414              mov       [esp+14h],eax
  00416b41  e8d6ee1200            call      00545A1Ch
  00416b46  8b0f                  mov       ecx,[edi]
  00416b48  8d0440                lea       eax,[eax+eax*2]
  00416b4b  8d0480                lea       eax,[eax+eax*4]
  00416b4e  c1e003                shl       eax,3
  00416b51  03c8                  add       ecx,eax
  00416b53  890f                  mov       [edi],ecx
  00416b55  0fbf4e32              movsx     ecx,word [esi+32h]
  00416b59  51                    push      ecx
  00416b5a  e8b7ee1200            call      00545A16h
  00416b5f  8b4e20                mov       ecx,[esi+20h]
  00416b62  8d0440                lea       eax,[eax+eax*2]
  00416b65  81c10060ffff          add       ecx,0FFFF6000h
  00416b6b  8d1480                lea       edx,[eax+eax*4]
  00416b6e  8b4624                mov       eax,[esi+24h]
  00416b71  c1e203                shl       edx,3
  00416b74  03c2                  add       eax,edx
  00416b76  894e20                mov       [esi+20h],ecx
  00416b79  85db                  test      ebx,ebx
  00416b7b  894624                mov       [esi+24h],eax
  00416b7e  741f                  je        short 00416B9Fh
  00416b80  6683bb9a00000000      cmp       word [ebx+9Ah],0
  00416b88  7515                  jne       short 00416B9Fh
  00416b8a  6aff                  push      0FFFFFFFFh
  00416b8c  6a00                  push      0
  00416b8e  68ed000000            push      0EDh
  00416b93  6a00                  push      0
  00416b95  57                    push      edi
  00416b96  56                    push      esi
  00416b97  e8a4400600            call      0047AC40h
  00416b9c  83c418                add       esp,18h

LAB_00416b9f:
  00416b9f  668146320008          add       word [esi+32h],800h
  00416ba5  8b44241c              mov       eax,[esp+1Ch]
  00416ba9  0fbf4e32              movsx     ecx,word [esi+32h]
  00416bad  51                    push      ecx
  00416bae  892f                  mov       [edi],ebp
  00416bb0  894624                mov       [esi+24h],eax
  00416bb3  e864ee1200            call      00545A1Ch
  00416bb8  8d0440                lea       eax,[eax+eax*2]
  00416bbb  8d1480                lea       edx,[eax+eax*4]
  00416bbe  8b07                  mov       eax,[edi]
  00416bc0  c1e203                shl       edx,3
  00416bc3  03c2                  add       eax,edx
  00416bc5  8907                  mov       [edi],eax
  00416bc7  0fbf4632              movsx     eax,word [esi+32h]
  00416bcb  50                    push      eax
  00416bcc  e845ee1200            call      00545A16h
  00416bd1  8b5624                mov       edx,[esi+24h]
  00416bd4  8d0440                lea       eax,[eax+eax*2]
  00416bd7  8d0c80                lea       ecx,[eax+eax*4]
  00416bda  c1e103                shl       ecx,3
  00416bdd  03d1                  add       edx,ecx
  00416bdf  85db                  test      ebx,ebx
  00416be1  895624                mov       [esi+24h],edx
  00416be4  741f                  je        short 00416C05h
  00416be6  6683bb9a00000001      cmp       word [ebx+9Ah],1
  00416bee  7515                  jne       short 00416C05h
  00416bf0  6aff                  push      0FFFFFFFFh
  00416bf2  6a00                  push      0
  00416bf4  68ed000000            push      0EDh
  00416bf9  6a00                  push      0
  00416bfb  57                    push      edi
  00416bfc  56                    push      esi
  00416bfd  e83e400600            call      0047AC40h
  00416c02  83c418                add       esp,18h

LAB_00416c05:
  00416c05  668b542410            mov       dx,[esp+10h]
  00416c0a  8b442414              mov       eax,[esp+14h]
  00416c0e  8b4c241c              mov       ecx,[esp+1Ch]
  00416c12  66ff4e16              dec       word [esi+16h]
  00416c16  66837e1600            cmp       word [esi+16h],0
  00416c1b  66895632              mov       [esi+32h],dx
  00416c1f  892f                  mov       [edi],ebp
  00416c21  894620                mov       [esi+20h],eax
  00416c24  894e24                mov       [esi+24h],ecx
  00416c27  7d06                  jge       short 00416C2Fh
  00416c29  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00416c2f:
  00416c2f  5f                    pop       edi
  00416c30  5e                    pop       esi
  00416c31  5d                    pop       ebp
  00416c32  5b                    pop       ebx
  00416c33  83c408                add       esp,8
  00416c36  c3                    ret
  00416c37  90                    nop
  00416c38  90                    nop
  00416c39  90                    nop
  00416c3a  90                    nop
  00416c3b  90                    nop
  00416c3c  90                    nop
  00416c3d  90                    nop
  00416c3e  90                    nop
  00416c3f  90                    nop

7 cible(s) de saut interne : 0x00416ab2, 0x00416ae7, 0x00416b0a, 0x00416b24, 0x00416b9f, 0x00416c05, 0x00416c2f
