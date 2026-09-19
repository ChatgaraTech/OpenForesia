Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004869d0  section .text  offset fichier 0x85dd0  320 octets

  004869d0  83ec0c                sub       esp,0Ch
  004869d3  53                    push      ebx
  004869d4  56                    push      esi
  004869d5  8b742418              mov       esi,[esp+18h]
  004869d9  57                    push      edi
  004869da  66c744240c0000        mov       word [esp+0Ch],0
  004869e1  66c744240e0000        mov       word [esp+0Eh],0
  004869e8  66837e1000            cmp       word [esi+10h],0
  004869ed  66c74424103c00        mov       word [esp+10h],3Ch
  004869f4  66c7442412c800        mov       word [esp+12h],0C8h
  004869fb  66c74424140000        mov       word [esp+14h],0
  00486a02  66c74424167800        mov       word [esp+16h],78h
  00486a09  7512                  jne       short 00486A1Dh
  00486a0b  8b461c                mov       eax,[esi+1Ch]
  00486a0e  8b4e20                mov       ecx,[esi+20h]
  00486a11  8b5624                mov       edx,[esi+24h]
  00486a14  894644                mov       [esi+44h],eax
  00486a17  894e48                mov       [esi+48h],ecx
  00486a1a  89564c                mov       [esi+4Ch],edx

LAB_00486a1d:
  00486a1d  0fbf7e10              movsx     edi,word [esi+10h]
  00486a21  e81a070c00            call      00547140h
  00486a26  8bcf                  mov       ecx,edi
  00486a28  25ff030000            and       eax,3FFh
  00486a2d  0fafcf                imul      ecx,edi
  00486a30  83c108                add       ecx,8
  00486a33  8b5e44                mov       ebx,[esi+44h]
  00486a36  c1e107                shl       ecx,7
  00486a39  03c1                  add       eax,ecx
  00486a3b  8b7e48                mov       edi,[esi+48h]
  00486a3e  56                    push      esi
  00486a3f  8d1440                lea       edx,[eax+eax*2]
  00486a42  8d0c00                lea       ecx,[eax+eax]
  00486a45  c1e210                shl       edx,10h
  00486a48  0bd1                  or        edx,ecx
  00486a4a  8d0440                lea       eax,[eax+eax*2]
  00486a4d  895628                mov       [esi+28h],edx
  00486a50  8b5650                mov       edx,[esi+50h]
  00486a53  03da                  add       ebx,edx
  00486a55  8b5654                mov       edx,[esi+54h]
  00486a58  03fa                  add       edi,edx
  00486a5a  895e44                mov       [esi+44h],ebx
  00486a5d  8bcb                  mov       ecx,ebx
  00486a5f  8b5e4c                mov       ebx,[esi+4Ch]
  00486a62  897e48                mov       [esi+48h],edi
  00486a65  8bd7                  mov       edx,edi
  00486a67  8b7e58                mov       edi,[esi+58h]
  00486a6a  894e1c                mov       [esi+1Ch],ecx
  00486a6d  c1e005                shl       eax,5
  00486a70  03df                  add       ebx,edi
  00486a72  8d3c10                lea       edi,[eax+edx]
  00486a75  895e4c                mov       [esi+4Ch],ebx
  00486a78  897e20                mov       [esi+20h],edi
  00486a7b  895e24                mov       [esi+24h],ebx
  00486a7e  e8bdce0a00            call      00533940h
  00486a83  8b4664                mov       eax,[esi+64h]
  00486a86  83c404                add       esp,4
  00486a89  3bf8                  cmp       edi,eax
  00486a8b  897e20                mov       [esi+20h],edi
  00486a8e  7f0d                  jg        short 00486A9Dh
  00486a90  894620                mov       [esi+20h],eax
  00486a93  c7869000000000080000  mov       dword [esi+90h],800h

LAB_00486a9d:
  00486a9d  f7869000000002880000  test      dword [esi+90h],8802h
  00486aa7  7559                  jne       short 00486B02h
  00486aa9  8d4c240c              lea       ecx,[esp+0Ch]
  00486aad  51                    push      ecx
  00486aae  56                    push      esi
  00486aaf  e8bce1feff            call      00474C70h
  00486ab4  8bf8                  mov       edi,eax
  00486ab6  83c408                add       esp,8
  00486ab9  85ff                  test      edi,edi
  00486abb  742e                  je        short 00486AEBh
  00486abd  a1b8e05d00            mov       eax,[5DE0B8h]
  00486ac2  85c0                  test      eax,eax
  00486ac4  743c                  je        short 00486B02h
  00486ac6  8b5674                mov       edx,[esi+74h]
  00486ac9  6a00                  push      0
  00486acb  57                    push      edi
  00486acc  c6420707              mov       byte [edx+7],7
  00486ad0  8b4674                mov       eax,[esi+74h]
  00486ad3  50                    push      eax
  00486ad4  e8174e0b00            call      0053B8F0h
  00486ad9  8b07                  mov       eax,[edi]
  00486adb  83c40c                add       esp,0Ch
  00486ade  0c40                  or        al,40h
  00486ae0  66c787960000001000    mov       word [edi+96h],10h
  00486ae9  8907                  mov       [edi],eax

LAB_00486aeb:
  00486aeb  8b5638                mov       edx,[esi+38h]
  00486aee  81c2e8e7e7ff          add       edx,0FFE7E7E8h
  00486af4  66ff4610              inc       word [esi+10h]
  00486af8  66837e1008            cmp       word [esi+10h],8
  00486afd  895638                mov       [esi+38h],edx
  00486b00  7c06                  jl        short 00486B08h

LAB_00486b02:
  00486b02  c70600000000          mov       dword [esi],0

LAB_00486b08:
  00486b08  5f                    pop       edi
  00486b09  5e                    pop       esi
  00486b0a  5b                    pop       ebx
  00486b0b  83c40c                add       esp,0Ch
  00486b0e  c3                    ret
  00486b0f  90                    nop

5 cible(s) de saut interne : 0x00486a1d, 0x00486a9d, 0x00486aeb, 0x00486b02, 0x00486b08
