Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00536090  section .text  offset fichier 0x135490  224 octets

  00536090  8b4c2404              mov       ecx,[esp+4]
  00536094  83ec20                sub       esp,20h
  00536097  8d442400              lea       eax,[esp]
  0053609b  53                    push      ebx
  0053609c  55                    push      ebp
  0053609d  56                    push      esi
  0053609e  57                    push      edi
  0053609f  50                    push      eax
  005360a0  51                    push      ecx
  005360a1  e85a62feff            call      0051C300h
  005360a6  83c408                add       esp,8
  005360a9  8d542410              lea       edx,[esp+10h]
  005360ad  52                    push      edx
  005360ae  e817fa0000            call      00545ACAh
  005360b3  8b0d10886000          mov       ecx,[608810h]
  005360b9  8b742438              mov       esi,[esp+38h]
  005360bd  0fbf11                movsx     edx,word [ecx]
  005360c0  8b06                  mov       eax,[esi]
  005360c2  f7ea                  imul      edx
  005360c4  8bf8                  mov       edi,eax
  005360c6  8bda                  mov       ebx,edx
  005360c8  0fbf4102              movsx     eax,word [ecx+2]
  005360cc  f76e04                imul      dword [esi+4]
  005360cf  03f8                  add       edi,eax
  005360d1  0fbf4104              movsx     eax,word [ecx+4]
  005360d5  13da                  adc       ebx,edx
  005360d7  f76e08                imul      dword [esi+8]
  005360da  03f8                  add       edi,eax
  005360dc  b90c000000            mov       ecx,0Ch
  005360e1  13da                  adc       ebx,edx
  005360e3  8bc7                  mov       eax,edi
  005360e5  8bd3                  mov       edx,ebx
  005360e7  e884100100            call      00547170h
  005360ec  8b7c243c              mov       edi,[esp+3Ch]
  005360f0  8907                  mov       [edi],eax
  005360f2  8b0d10886000          mov       ecx,[608810h]
  005360f8  0fbf4106              movsx     eax,word [ecx+6]
  005360fc  f72e                  imul      dword [esi]
  005360fe  8bd8                  mov       ebx,eax
  00536100  8bea                  mov       ebp,edx
  00536102  0fbf410a              movsx     eax,word [ecx+0Ah]
  00536106  f76e08                imul      dword [esi+8]
  00536109  03d8                  add       ebx,eax
  0053610b  0fbf4108              movsx     eax,word [ecx+8]
  0053610f  13ea                  adc       ebp,edx
  00536111  f76e04                imul      dword [esi+4]
  00536114  03d8                  add       ebx,eax
  00536116  b90c000000            mov       ecx,0Ch
  0053611b  13ea                  adc       ebp,edx
  0053611d  8bc3                  mov       eax,ebx
  0053611f  8bd5                  mov       edx,ebp
  00536121  e84a100100            call      00547170h
  00536126  894704                mov       [edi+4],eax
  00536129  8b0d10886000          mov       ecx,[608810h]
  0053612f  0fbf410c              movsx     eax,word [ecx+0Ch]
  00536133  f72e                  imul      dword [esi]
  00536135  8bd8                  mov       ebx,eax
  00536137  8bea                  mov       ebp,edx
  00536139  0fbf4110              movsx     eax,word [ecx+10h]
  0053613d  f76e08                imul      dword [esi+8]
  00536140  03d8                  add       ebx,eax
  00536142  0fbf410e              movsx     eax,word [ecx+0Eh]
  00536146  13ea                  adc       ebp,edx
  00536148  f76e04                imul      dword [esi+4]
  0053614b  03d8                  add       ebx,eax
  0053614d  b90c000000            mov       ecx,0Ch
  00536152  13ea                  adc       ebp,edx
  00536154  8bc3                  mov       eax,ebx
  00536156  8bd5                  mov       edx,ebp
  00536158  e813100100            call      00547170h
  0053615d  894708                mov       [edi+8],eax
  00536160  5f                    pop       edi
  00536161  5e                    pop       esi
  00536162  5d                    pop       ebp
  00536163  5b                    pop       ebx
  00536164  83c420                add       esp,20h
  00536167  c3                    ret
  00536168  90                    nop
  00536169  90                    nop
  0053616a  90                    nop
  0053616b  90                    nop
  0053616c  90                    nop
  0053616d  90                    nop
  0053616e  90                    nop
  0053616f  90                    nop
