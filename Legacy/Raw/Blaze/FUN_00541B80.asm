C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00541b80  section .text  offset fichier 0x140f80  259 octets

  00541b80  83ec08                sub       esp,8
  00541b83  53                    push      ebx
  00541b84  56                    push      esi
  00541b85  8b742414              mov       esi,[esp+14h]
  00541b89  33c0                  xor       eax,eax
  00541b8b  bb00000000            mov       ebx,0
  00541b90  8a4604                mov       al,[esi+4]
  00541b93  0fbf4e18              movsx     ecx,word [esi+18h]
  00541b97  8b148580965e00        mov       edx,[eax*4+5E9680h]
  00541b9e  8b048a                mov       eax,[edx+ecx*4]
  00541ba1  33c9                  xor       ecx,ecx
  00541ba3  668b4816              mov       cx,[eax+16h]
  00541ba7  d1e9                  shr       ecx,1
  00541ba9  894c2414              mov       [esp+14h],ecx
  00541bad  7433                  je        short 00541BE2h
  00541baf  0fbf461a              movsx     eax,word [esi+1Ah]
  00541bb3  33d2                  xor       edx,edx
  00541bb5  f7f1                  div       ecx
  00541bb7  85d2                  test      edx,edx
  00541bb9  7405                  je        short 00541BC0h
  00541bbb  f60601                test      byte [esi],1
  00541bbe  7422                  je        short 00541BE2h

LAB_00541bc0:
  00541bc0  8b8690000000          mov       eax,[esi+90h]
  00541bc6  f6c402                test      ah,2
  00541bc9  7417                  je        short 00541BE2h
  00541bcb  6aff                  push      0FFFFFFFFh
  00541bcd  6a18                  push      18h
  00541bcf  68fe000000            push      0FEh
  00541bd4  8d4e1c                lea       ecx,[esi+1Ch]
  00541bd7  53                    push      ebx
  00541bd8  51                    push      ecx
  00541bd9  56                    push      esi
  00541bda  e86190f3ff            call      0047AC40h
  00541bdf  83c418                add       esp,18h

LAB_00541be2:
  00541be2  8b15b4876000          mov       edx,[6087B4h]
  00541be8  33c0                  xor       eax,eax
  00541bea  8a4608                mov       al,[esi+8]
  00541bed  8b0a                  mov       ecx,[edx]
  00541bef  83c078                add       eax,78h
  00541bf2  c1e00d                shl       eax,0Dh
  00541bf5  03c1                  add       eax,ecx
  00541bf7  8b4e7c                mov       ecx,[esi+7Ch]
  00541bfa  6639590a              cmp       [ecx+0Ah],bx
  00541bfe  747d                  je        short 00541C7Dh
  00541c00  8a9050010000          mov       dl,[eax+150h]
  00541c06  80e207                and       dl,7
  00541c09  80fa04                cmp       dl,4
  00541c0c  756f                  jne       short 00541C7Dh
  00541c0e  33c0                  xor       eax,eax
  00541c10  53                    push      ebx
  00541c11  668b460a              mov       ax,[esi+0Ah]
  00541c15  66895e0a              mov       [esi+0Ah],bx
  00541c19  8b0dc4975e00          mov       ecx,[5E97C4h]
  00541c1f  89442418              mov       [esp+18h],eax
  00541c23  53                    push      ebx
  00541c24  6a2a                  push      2Ah
  00541c26  8b9194000000          mov       edx,[ecx+94h]
  00541c2c  8d4e1c                lea       ecx,[esi+1Ch]
  00541c2f  8b4224                mov       eax,[edx+24h]
  00541c32  50                    push      eax
  00541c33  51                    push      ecx
  00541c34  56                    push      esi
  00541c35  e80690f3ff            call      0047AC40h
  00541c3a  668b54242c            mov       dx,[esp+2Ch]
  00541c3f  8b467c                mov       eax,[esi+7Ch]
  00541c42  83c418                add       esp,18h
  00541c45  6689560a              mov       [esi+0Ah],dx
  00541c49  885c2408              mov       [esp+8],bl
  00541c4d  885c2409              mov       [esp+9],bl
  00541c51  885c240a              mov       [esp+0Ah],bl
  00541c55  8a480a                mov       cl,[eax+0Ah]
  00541c58  8d542414              lea       edx,[esp+14h]
  00541c5c  8d442408              lea       eax,[esp+8]
  00541c60  52                    push      edx
  00541c61  50                    push      eax
  00541c62  884c2413              mov       [esp+13h],cl
  00541c66  c64424140d            mov       byte [esp+14h],0Dh
  00541c6b  c644241504            mov       byte [esp+15h],4
  00541c70  c6442416f7            mov       byte [esp+16h],0F7h
  00541c75  e86623fdff            call      00513FE0h
  00541c7a  83c408                add       esp,8

LAB_00541c7d:
  00541c7d  5e                    pop       esi
  00541c7e  5b                    pop       ebx
  00541c7f  83c408                add       esp,8
  00541c82  c3                    ret

3 cible(s) de saut interne : 0x00541bc0, 0x00541be2, 0x00541c7d
