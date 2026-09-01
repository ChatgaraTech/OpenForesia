Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00477e40  section .text  offset fichier 0x77240  618 octets

  00477e40  83ec18                sub       esp,18h
  00477e43  55                    push      ebp
  00477e44  56                    push      esi
  00477e45  8b742424              mov       esi,[esp+24h]
  00477e49  57                    push      edi
  00477e4a  66c744240c0000        mov       word [esp+0Ch],0
  00477e51  66c744240ec4ff        mov       word [esp+0Eh],0FFC4h
  00477e58  66837e1000            cmp       word [esi+10h],0
  00477e5d  66c74424103c00        mov       word [esp+10h],3Ch
  00477e64  66c74424126400        mov       word [esp+12h],64h
  00477e6b  66c74424140000        mov       word [esp+14h],0
  00477e72  66c74424167800        mov       word [esp+16h],78h
  00477e79  7517                  jne       short 00477E92h
  00477e7b  8b16                  mov       edx,[esi]
  00477e7d  8b4674                mov       eax,[esi+74h]
  00477e80  80ca80                or        dl,80h
  00477e83  8916                  mov       [esi],edx
  00477e85  8b08                  mov       ecx,[eax]
  00477e87  80c980                or        cl,80h
  00477e8a  8908                  mov       [eax],ecx
  00477e8c  8b4620                mov       eax,[esi+20h]
  00477e8f  894648                mov       [esi+48h],eax

LAB_00477e92:
  00477e92  8b4678                mov       eax,[esi+78h]
  00477e95  85c0                  test      eax,eax
  00477e97  7453                  je        short 00477EECh
  00477e99  8b4824                mov       ecx,[eax+24h]
  00477e9c  8b5624                mov       edx,[esi+24h]
  00477e9f  2bca                  sub       ecx,edx
  00477ea1  8b501c                mov       edx,[eax+1Ch]
  00477ea4  c1f90c                sar       ecx,0Ch
  00477ea7  668b7e32              mov       di,[esi+32h]
  00477eab  51                    push      ecx
  00477eac  2b561c                sub       edx,[esi+1Ch]
  00477eaf  c1fa0c                sar       edx,0Ch
  00477eb2  52                    push      edx
  00477eb3  e86adb0c00            call      00545A22h
  00477eb8  2bc7                  sub       eax,edi
  00477eba  25ff0f0000            and       eax,0FFFh
  00477ebf  8d4840                lea       ecx,[eax+40h]
  00477ec2  81e1ff0f0000          and       ecx,0FFFh
  00477ec8  81f980000000          cmp       ecx,80h
  00477ece  7e2a                  jle       short 00477EFAh
  00477ed0  33d2                  xor       edx,edx
  00477ed2  3d00080000            cmp       eax,800h
  00477ed7  0f9dc2                setge     dl
  00477eda  4a                    dec       edx
  00477edb  81e280000000          and       edx,80h
  00477ee1  83c2c0                add       edx,0FFFFFFC0h
  00477ee4  03d7                  add       edx,edi
  00477ee6  66895632              mov       [esi+32h],dx
  00477eea  eb0e                  jmp       short 00477EFAh

LAB_00477eec:
  00477eec  6aff                  push      0FFFFFFFFh
  00477eee  56                    push      esi
  00477eef  e80cd0ffff            call      00474F00h
  00477ef4  83c408                add       esp,8
  00477ef7  894678                mov       [esi+78h],eax

LAB_00477efa:
  00477efa  0fbf6e32              movsx     ebp,word [esi+32h]
  00477efe  55                    push      ebp
  00477eff  8d7e1c                lea       edi,[esi+1Ch]
  00477f02  e815db0c00            call      00545A1Ch
  00477f07  8b0f                  mov       ecx,[edi]
  00477f09  8d0480                lea       eax,[eax+eax*4]
  00477f0c  d1e0                  shl       eax,1
  00477f0e  03c8                  add       ecx,eax
  00477f10  55                    push      ebp
  00477f11  890f                  mov       [edi],ecx
  00477f13  e8feda0c00            call      00545A16h
  00477f18  8b5624                mov       edx,[esi+24h]
  00477f1b  8d0c80                lea       ecx,[eax+eax*4]
  00477f1e  d1e1                  shl       ecx,1
  00477f20  03d1                  add       edx,ecx
  00477f22  895624                mov       [esi+24h],edx
  00477f25  0fbf5610              movsx     edx,word [esi+10h]
  00477f29  c1e208                shl       edx,8
  00477f2c  52                    push      edx
  00477f2d  e8eada0c00            call      00545A1Ch
  00477f32  8b6e48                mov       ebp,[esi+48h]
  00477f35  c1e005                shl       eax,5
  00477f38  03c5                  add       eax,ebp
  00477f3a  0fbf6e1a              movsx     ebp,word [esi+1Ah]
  00477f3e  894620                mov       [esi+20h],eax
  00477f41  66c7461a0400          mov       word [esi+1Ah],4
  00477f47  e8f4f10c00            call      00547140h
  00477f4c  8b0f                  mov       ecx,[edi]
  00477f4e  83e03f                and       eax,3Fh
  00477f51  83e820                sub       eax,20h
  00477f54  c1e00c                shl       eax,0Ch
  00477f57  03c1                  add       eax,ecx
  00477f59  89442418              mov       [esp+18h],eax
  00477f5d  e8def10c00            call      00547140h
  00477f62  8b4e20                mov       ecx,[esi+20h]
  00477f65  83e03f                and       eax,3Fh
  00477f68  83e820                sub       eax,20h
  00477f6b  c1e00c                shl       eax,0Ch
  00477f6e  03c1                  add       eax,ecx
  00477f70  8944241c              mov       [esp+1Ch],eax
  00477f74  e8c7f10c00            call      00547140h
  00477f79  8b4e24                mov       ecx,[esi+24h]
  00477f7c  83e03f                and       eax,3Fh
  00477f7f  83e820                sub       eax,20h
  00477f82  6aff                  push      0FFFFFFFFh
  00477f84  c1e00c                shl       eax,0Ch
  00477f87  03c1                  add       eax,ecx
  00477f89  8d4c241c              lea       ecx,[esp+1Ch]
  00477f8d  89442424              mov       [esp+24h],eax
  00477f91  33c0                  xor       eax,eax
  00477f93  8a460b                mov       al,[esi+0Bh]
  00477f96  50                    push      eax
  00477f97  68bf000000            push      0BFh
  00477f9c  68781a5500            push      551A78h
  00477fa1  51                    push      ecx
  00477fa2  56                    push      esi
  00477fa3  e8982c0000            call      0047AC40h
  00477fa8  83c418                add       esp,18h
  00477fab  66896e1a              mov       [esi+1Ah],bp
  00477faf  8b6e20                mov       ebp,[esi+20h]
  00477fb2  56                    push      esi
  00477fb3  e888b90b00            call      00533940h
  00477fb8  8b06                  mov       eax,[esi]
  00477fba  83c404                add       esp,4
  00477fbd  a900000040            test      eax,40000000h
  00477fc2  896e20                mov       [esi+20h],ebp
  00477fc5  0f85c8000000          jne       near 00478093h
  00477fcb  f7869000000002880000  test      dword [esi+90h],8802h
  00477fd5  0f85b8000000          jne       near 00478093h
  00477fdb  8d54240c              lea       edx,[esp+0Ch]
  00477fdf  52                    push      edx
  00477fe0  56                    push      esi
  00477fe1  e8baccffff            call      00474CA0h
  00477fe6  8be8                  mov       ebp,eax
  00477fe8  83c408                add       esp,8
  00477feb  85ed                  test      ebp,ebp
  00477fed  745e                  je        short 0047804Dh
  00477fef  8b4500                mov       eax,[ebp]
  00477ff2  66c785960000001000    mov       word [ebp+96h],10h
  00477ffb  0c40                  or        al,40h
  00477ffd  6a00                  push      0
  00477fff  894500                mov       [ebp],eax
  00478002  8b4674                mov       eax,[esi+74h]
  00478005  668b4e06              mov       cx,[esi+6]
  00478009  55                    push      ebp
  0047800a  66894806              mov       [eax+6],cx
  0047800e  8b5674                mov       edx,[esi+74h]
  00478011  52                    push      edx
  00478012  e849360c00            call      0053B660h
  00478017  83c40c                add       esp,0Ch
  0047801a  85c0                  test      eax,eax
  0047801c  7575                  jne       short 00478093h
  0047801e  8a460b                mov       al,[esi+0Bh]
  00478021  6aff                  push      0FFFFFFFFh
  00478023  50                    push      eax
  00478024  6a7c                  push      7Ch
  00478026  56                    push      esi
  00478027  c6460402              mov       byte [esi+4],2
  0047802b  896e70                mov       [esi+70h],ebp
  0047802e  e8ad2a0000            call      0047AAE0h
  00478033  83c410                add       esp,10h
  00478036  57                    push      edi
  00478037  6800120000            push      1200h
  0047803c  6800020000            push      200h
  00478041  6a1e                  push      1Eh
  00478043  e8d85a0c00            call      0053DB20h
  00478048  83c410                add       esp,10h
  0047804b  eb46                  jmp       short 00478093h

LAB_0047804d:
  0047804d  8a4e40                mov       cl,[esi+40h]
  00478050  fec1                  inc       cl
  00478052  884e40                mov       [esi+40h],cl
  00478055  660fb6c9              movzx     cx,cl
  00478059  663b4e1a              cmp       cx,[esi+1Ah]
  0047805d  7c04                  jl        short 00478063h
  0047805f  c6464000              mov       byte [esi+40h],0

LAB_00478063:
  00478063  0fbf4610              movsx     eax,word [esi+10h]
  00478067  99                    cdq
  00478068  b90e000000            mov       ecx,0Eh
  0047806d  f7f9                  idiv      ecx
  0047806f  85d2                  test      edx,edx
  00478071  7515                  jne       short 00478088h
  00478073  57                    push      edi
  00478074  6800120000            push      1200h
  00478079  6800010000            push      100h
  0047807e  6a17                  push      17h
  00478080  e89b5a0c00            call      0053DB20h
  00478085  83c410                add       esp,10h

LAB_00478088:
  00478088  66ff4610              inc       word [esi+10h]
  0047808c  66837e1028            cmp       word [esi+10h],28h
  00478091  7c10                  jl        short 004780A3h

LAB_00478093:
  00478093  8b4674                mov       eax,[esi+74h]
  00478096  8b08                  mov       ecx,[eax]
  00478098  80e17f                and       cl,7Fh
  0047809b  8908                  mov       [eax],ecx
  0047809d  c70600000000          mov       dword [esi],0

LAB_004780a3:
  004780a3  5f                    pop       edi
  004780a4  5e                    pop       esi
  004780a5  5d                    pop       ebp
  004780a6  83c418                add       esp,18h
  004780a9  c3                    ret

8 cible(s) de saut interne : 0x00477e92, 0x00477eec, 0x00477efa, 0x0047804d, 0x00478063, 0x00478088, 0x00478093, 0x004780a3
