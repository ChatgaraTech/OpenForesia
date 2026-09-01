Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00477a90  section .text  offset fichier 0x76e90  643 octets

  00477a90  83ec0c                sub       esp,0Ch
  00477a93  53                    push      ebx
  00477a94  55                    push      ebp
  00477a95  56                    push      esi
  00477a96  8b74241c              mov       esi,[esp+1Ch]
  00477a9a  33c9                  xor       ecx,ecx
  00477a9c  57                    push      edi
  00477a9d  66394e10              cmp       [esi+10h],cx
  00477aa1  66894c2410            mov       [esp+10h],cx
  00477aa6  66c7442412c4ff        mov       word [esp+12h],0FFC4h
  00477aad  66c74424143c00        mov       word [esp+14h],3Ch
  00477ab4  66c74424166400        mov       word [esp+16h],64h
  00477abb  66894c2418            mov       [esp+18h],cx
  00477ac0  66c744241a7800        mov       word [esp+1Ah],78h
  00477ac7  751a                  jne       short 00477AE3h
  00477ac9  8b3e                  mov       edi,[esi]
  00477acb  8b4674                mov       eax,[esi+74h]
  00477ace  81cf80000000          or        edi,80h
  00477ad4  893e                  mov       [esi],edi
  00477ad6  8b10                  mov       edx,[eax]
  00477ad8  80ca80                or        dl,80h
  00477adb  8910                  mov       [eax],edx
  00477add  8b4620                mov       eax,[esi+20h]
  00477ae0  894648                mov       [esi+48h],eax

LAB_00477ae3:
  00477ae3  8b4678                mov       eax,[esi+78h]
  00477ae6  3bc1                  cmp       eax,ecx
  00477ae8  7455                  je        short 00477B3Fh
  00477aea  8b4824                mov       ecx,[eax+24h]
  00477aed  8b6e24                mov       ebp,[esi+24h]
  00477af0  8b501c                mov       edx,[eax+1Ch]
  00477af3  8b461c                mov       eax,[esi+1Ch]
  00477af6  668b7e32              mov       di,[esi+32h]
  00477afa  2bcd                  sub       ecx,ebp
  00477afc  2bd0                  sub       edx,eax
  00477afe  c1f90c                sar       ecx,0Ch
  00477b01  c1fa0c                sar       edx,0Ch
  00477b04  51                    push      ecx
  00477b05  52                    push      edx
  00477b06  e817df0c00            call      00545A22h
  00477b0b  2bc7                  sub       eax,edi
  00477b0d  25ff0f0000            and       eax,0FFFh
  00477b12  8d4840                lea       ecx,[eax+40h]
  00477b15  81e1ff0f0000          and       ecx,0FFFh
  00477b1b  81f980000000          cmp       ecx,80h
  00477b21  7e2a                  jle       short 00477B4Dh
  00477b23  33d2                  xor       edx,edx
  00477b25  3d00080000            cmp       eax,800h
  00477b2a  0f9dc2                setge     dl
  00477b2d  4a                    dec       edx
  00477b2e  81e280000000          and       edx,80h
  00477b34  83c2c0                add       edx,0FFFFFFC0h
  00477b37  03d7                  add       edx,edi
  00477b39  66895632              mov       [esi+32h],dx
  00477b3d  eb0e                  jmp       short 00477B4Dh

LAB_00477b3f:
  00477b3f  6aff                  push      0FFFFFFFFh
  00477b41  56                    push      esi
  00477b42  e8b9d3ffff            call      00474F00h
  00477b47  83c408                add       esp,8
  00477b4a  894678                mov       [esi+78h],eax

LAB_00477b4d:
  00477b4d  0fbf7e32              movsx     edi,word [esi+32h]
  00477b51  57                    push      edi
  00477b52  8d6e1c                lea       ebp,[esi+1Ch]
  00477b55  e8c2de0c00            call      00545A1Ch
  00477b5a  8b4d00                mov       ecx,[ebp]
  00477b5d  8d0480                lea       eax,[eax+eax*4]
  00477b60  d1e0                  shl       eax,1
  00477b62  03c8                  add       ecx,eax
  00477b64  57                    push      edi
  00477b65  894d00                mov       [ebp],ecx
  00477b68  e8a9de0c00            call      00545A16h
  00477b6d  0fbf5610              movsx     edx,word [esi+10h]
  00477b71  8d0c80                lea       ecx,[eax+eax*4]
  00477b74  8b4624                mov       eax,[esi+24h]
  00477b77  d1e1                  shl       ecx,1
  00477b79  c1e208                shl       edx,8
  00477b7c  03c1                  add       eax,ecx
  00477b7e  52                    push      edx
  00477b7f  894624                mov       [esi+24h],eax
  00477b82  e895de0c00            call      00545A1Ch
  00477b87  8b4e48                mov       ecx,[esi+48h]
  00477b8a  c1e005                shl       eax,5
  00477b8d  03c1                  add       eax,ecx
  00477b8f  894620                mov       [esi+20h],eax
  00477b92  0fbf4610              movsx     eax,word [esi+10h]
  00477b96  c1e009                shl       eax,9
  00477b99  50                    push      eax
  00477b9a  e87dde0c00            call      00545A1Ch
  00477b9f  d1f8                  sar       eax,1
  00477ba1  0500180000            add       eax,1800h
  00477ba6  8b7e20                mov       edi,[esi+20h]
  00477ba9  8bc8                  mov       ecx,eax
  00477bab  56                    push      esi
  00477bac  c1e110                shl       ecx,10h
  00477baf  0bc8                  or        ecx,eax
  00477bb1  894e28                mov       [esi+28h],ecx
  00477bb4  e887bd0b00            call      00533940h
  00477bb9  8b06                  mov       eax,[esi]
  00477bbb  83c404                add       esp,4
  00477bbe  a900000040            test      eax,40000000h
  00477bc3  897e20                mov       [esi+20h],edi
  00477bc6  0f852f010000          jne       near 00477CFBh
  00477bcc  f7869000000002880000  test      dword [esi+90h],8802h
  00477bd6  0f851f010000          jne       near 00477CFBh
  00477bdc  8d542410              lea       edx,[esp+10h]
  00477be0  52                    push      edx
  00477be1  56                    push      esi
  00477be2  e8b9d0ffff            call      00474CA0h
  00477be7  8bd8                  mov       ebx,eax
  00477be9  83c408                add       esp,8
  00477bec  85db                  test      ebx,ebx
  00477bee  0f84c1000000          je        near 00477CB5h
  00477bf4  8b0b                  mov       ecx,[ebx]
  00477bf6  66c783960000001000    mov       word [ebx+96h],10h
  00477bff  83c940                or        ecx,40h
  00477c02  6a00                  push      0
  00477c04  890b                  mov       [ebx],ecx
  00477c06  8b4674                mov       eax,[esi+74h]
  00477c09  668b4e06              mov       cx,[esi+6]
  00477c0d  53                    push      ebx
  00477c0e  66894806              mov       [eax+6],cx
  00477c12  8b5674                mov       edx,[esi+74h]
  00477c15  52                    push      edx
  00477c16  e895380c00            call      0053B4B0h
  00477c1b  83c40c                add       esp,0Ch
  00477c1e  85c0                  test      eax,eax
  00477c20  0f85d5000000          jne       near 00477CFBh
  00477c26  894670                mov       [esi+70h],eax
  00477c29  894644                mov       [esi+44h],eax
  00477c2c  894648                mov       [esi+48h],eax
  00477c2f  89464c                mov       [esi+4Ch],eax
  00477c32  894628                mov       [esi+28h],eax
  00477c35  b802000000            mov       eax,2
  00477c3a  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  00477c41  66894612              mov       [esi+12h],ax
  00477c45  66894616              mov       [esi+16h],ax
  00477c49  8d6e50                lea       ebp,[esi+50h]
  00477c4c  83c31c                add       ebx,1Ch
  00477c4f  bf06000000            mov       edi,6

LAB_00477c54:
  00477c54  6a20                  push      20h
  00477c56  e8e5f40c00            call      00547140h
  00477c5b  25ff030000            and       eax,3FFh
  00477c60  50                    push      eax
  00477c61  e8daf40c00            call      00547140h
  00477c66  50                    push      eax
  00477c67  55                    push      ebp
  00477c68  e863ae0b00            call      00532AD0h
  00477c6d  83c410                add       esp,10h
  00477c70  e8cbf40c00            call      00547140h
  00477c75  25ff070000            and       eax,7FFh
  00477c7a  6aff                  push      0FFFFFFFFh
  00477c7c  0500140000            add       eax,1400h
  00477c81  66894614              mov       [esi+14h],ax
  00477c85  33c0                  xor       eax,eax
  00477c87  8a460b                mov       al,[esi+0Bh]
  00477c8a  50                    push      eax
  00477c8b  68be000000            push      0BEh
  00477c90  68c01b5500            push      551BC0h
  00477c95  53                    push      ebx
  00477c96  56                    push      esi
  00477c97  e8a42f0000            call      0047AC40h
  00477c9c  83c418                add       esp,18h
  00477c9f  4f                    dec       edi
  00477ca0  75b2                  jne       short 00477C54h
  00477ca2  8d461c                lea       eax,[esi+1Ch]
  00477ca5  50                    push      eax
  00477ca6  68d8185500            push      5518D8h
  00477cab  e8305f0c00            call      0053DBE0h
  00477cb0  83c408                add       esp,8
  00477cb3  eb46                  jmp       short 00477CFBh

LAB_00477cb5:
  00477cb5  8a4e40                mov       cl,[esi+40h]
  00477cb8  fec1                  inc       cl
  00477cba  884e40                mov       [esi+40h],cl
  00477cbd  660fb6c9              movzx     cx,cl
  00477cc1  663b4e1a              cmp       cx,[esi+1Ah]
  00477cc5  7c04                  jl        short 00477CCBh
  00477cc7  c6464000              mov       byte [esi+40h],0

LAB_00477ccb:
  00477ccb  0fbf4610              movsx     eax,word [esi+10h]
  00477ccf  99                    cdq
  00477cd0  b90e000000            mov       ecx,0Eh
  00477cd5  f7f9                  idiv      ecx
  00477cd7  85d2                  test      edx,edx
  00477cd9  7515                  jne       short 00477CF0h
  00477cdb  55                    push      ebp
  00477cdc  6800120000            push      1200h
  00477ce1  6800010000            push      100h
  00477ce6  6a17                  push      17h
  00477ce8  e8335e0c00            call      0053DB20h
  00477ced  83c410                add       esp,10h

LAB_00477cf0:
  00477cf0  66ff4610              inc       word [esi+10h]
  00477cf4  66837e1028            cmp       word [esi+10h],28h
  00477cf9  7c10                  jl        short 00477D0Bh

LAB_00477cfb:
  00477cfb  8b4674                mov       eax,[esi+74h]
  00477cfe  8b08                  mov       ecx,[eax]
  00477d00  80e17f                and       cl,7Fh
  00477d03  8908                  mov       [eax],ecx
  00477d05  c70600000000          mov       dword [esi],0

LAB_00477d0b:
  00477d0b  5f                    pop       edi
  00477d0c  5e                    pop       esi
  00477d0d  5d                    pop       ebp
  00477d0e  5b                    pop       ebx
  00477d0f  83c40c                add       esp,0Ch
  00477d12  c3                    ret

9 cible(s) de saut interne : 0x00477ae3, 0x00477b3f, 0x00477b4d, 0x00477c54, 0x00477cb5, 0x00477ccb, 0x00477cf0, 0x00477cfb, 0x00477d0b
