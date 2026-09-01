Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00476cf0  section .text  offset fichier 0x760f0  727 octets

  00476cf0  83ec18                sub       esp,18h
  00476cf3  53                    push      ebx
  00476cf4  55                    push      ebp
  00476cf5  56                    push      esi
  00476cf6  8b742428              mov       esi,[esp+28h]
  00476cfa  33db                  xor       ebx,ebx
  00476cfc  57                    push      edi
  00476cfd  66395e10              cmp       [esi+10h],bx
  00476d01  66895c2410            mov       [esp+10h],bx
  00476d06  66c7442412c4ff        mov       word [esp+12h],0FFC4h
  00476d0d  66c74424143c00        mov       word [esp+14h],3Ch
  00476d14  66c74424166400        mov       word [esp+16h],64h
  00476d1b  66895c2418            mov       [esp+18h],bx
  00476d20  66c744241a7800        mov       word [esp+1Ah],78h
  00476d27  7517                  jne       short 00476D40h
  00476d29  8b16                  mov       edx,[esi]
  00476d2b  8b4674                mov       eax,[esi+74h]
  00476d2e  80ca80                or        dl,80h
  00476d31  8916                  mov       [esi],edx
  00476d33  8b08                  mov       ecx,[eax]
  00476d35  80c980                or        cl,80h
  00476d38  8908                  mov       [eax],ecx
  00476d3a  8b4620                mov       eax,[esi+20h]
  00476d3d  894648                mov       [esi+48h],eax

LAB_00476d40:
  00476d40  8b4678                mov       eax,[esi+78h]
  00476d43  3bc3                  cmp       eax,ebx
  00476d45  7453                  je        short 00476D9Ah
  00476d47  8b4824                mov       ecx,[eax+24h]
  00476d4a  8b5624                mov       edx,[esi+24h]
  00476d4d  2bca                  sub       ecx,edx
  00476d4f  8b501c                mov       edx,[eax+1Ch]
  00476d52  c1f90c                sar       ecx,0Ch
  00476d55  668b7e32              mov       di,[esi+32h]
  00476d59  51                    push      ecx
  00476d5a  2b561c                sub       edx,[esi+1Ch]
  00476d5d  c1fa0c                sar       edx,0Ch
  00476d60  52                    push      edx
  00476d61  e8bcec0c00            call      00545A22h
  00476d66  2bc7                  sub       eax,edi
  00476d68  25ff0f0000            and       eax,0FFFh
  00476d6d  8d4840                lea       ecx,[eax+40h]
  00476d70  81e1ff0f0000          and       ecx,0FFFh
  00476d76  81f980000000          cmp       ecx,80h
  00476d7c  7e2a                  jle       short 00476DA8h
  00476d7e  33d2                  xor       edx,edx
  00476d80  3d00080000            cmp       eax,800h
  00476d85  0f9dc2                setge     dl
  00476d88  4a                    dec       edx
  00476d89  81e280000000          and       edx,80h
  00476d8f  83c2c0                add       edx,0FFFFFFC0h
  00476d92  03d7                  add       edx,edi
  00476d94  66895632              mov       [esi+32h],dx
  00476d98  eb0e                  jmp       short 00476DA8h

LAB_00476d9a:
  00476d9a  6aff                  push      0FFFFFFFFh
  00476d9c  56                    push      esi
  00476d9d  e85ee1ffff            call      00474F00h
  00476da2  83c408                add       esp,8
  00476da5  894678                mov       [esi+78h],eax

LAB_00476da8:
  00476da8  0fbf7e32              movsx     edi,word [esi+32h]
  00476dac  57                    push      edi
  00476dad  8d6e1c                lea       ebp,[esi+1Ch]
  00476db0  e867ec0c00            call      00545A1Ch
  00476db5  8b4d00                mov       ecx,[ebp]
  00476db8  8d0480                lea       eax,[eax+eax*4]
  00476dbb  d1e0                  shl       eax,1
  00476dbd  03c8                  add       ecx,eax
  00476dbf  57                    push      edi
  00476dc0  894d00                mov       [ebp],ecx
  00476dc3  e84eec0c00            call      00545A16h
  00476dc8  8b5624                mov       edx,[esi+24h]
  00476dcb  8d0c80                lea       ecx,[eax+eax*4]
  00476dce  d1e1                  shl       ecx,1
  00476dd0  03d1                  add       edx,ecx
  00476dd2  895624                mov       [esi+24h],edx
  00476dd5  0fbf5610              movsx     edx,word [esi+10h]
  00476dd9  c1e208                shl       edx,8
  00476ddc  52                    push      edx
  00476ddd  e83aec0c00            call      00545A1Ch
  00476de2  8b7e48                mov       edi,[esi+48h]
  00476de5  c1e005                shl       eax,5
  00476de8  03c7                  add       eax,edi
  00476dea  0fbf7e1a              movsx     edi,word [esi+1Ah]
  00476dee  894620                mov       [esi+20h],eax
  00476df1  66c7461a0100          mov       word [esi+1Ah],1
  00476df7  e844030d00            call      00547140h
  00476dfc  8b4d00                mov       ecx,[ebp]
  00476dff  83e03f                and       eax,3Fh
  00476e02  83e820                sub       eax,20h
  00476e05  c1e00c                shl       eax,0Ch
  00476e08  03c1                  add       eax,ecx
  00476e0a  8944241c              mov       [esp+1Ch],eax
  00476e0e  e82d030d00            call      00547140h
  00476e13  8b4e20                mov       ecx,[esi+20h]
  00476e16  83e03f                and       eax,3Fh
  00476e19  83e820                sub       eax,20h
  00476e1c  c1e00c                shl       eax,0Ch
  00476e1f  03c1                  add       eax,ecx
  00476e21  89442420              mov       [esp+20h],eax
  00476e25  e816030d00            call      00547140h
  00476e2a  8b4e24                mov       ecx,[esi+24h]
  00476e2d  83e03f                and       eax,3Fh
  00476e30  83e820                sub       eax,20h
  00476e33  6aff                  push      0FFFFFFFFh
  00476e35  c1e00c                shl       eax,0Ch
  00476e38  03c1                  add       eax,ecx
  00476e3a  8d4c2420              lea       ecx,[esp+20h]
  00476e3e  89442428              mov       [esp+28h],eax
  00476e42  33c0                  xor       eax,eax
  00476e44  8a460b                mov       al,[esi+0Bh]
  00476e47  50                    push      eax
  00476e48  68bf000000            push      0BFh
  00476e4d  68b81b5500            push      551BB8h
  00476e52  51                    push      ecx
  00476e53  56                    push      esi
  00476e54  e8e73d0000            call      0047AC40h
  00476e59  83c418                add       esp,18h
  00476e5c  66897e1a              mov       [esi+1Ah],di
  00476e60  8b7e20                mov       edi,[esi+20h]
  00476e63  56                    push      esi
  00476e64  e8d7ca0b00            call      00533940h
  00476e69  8b06                  mov       eax,[esi]
  00476e6b  83c404                add       esp,4
  00476e6e  a900000040            test      eax,40000000h
  00476e73  897e20                mov       [esi+20h],edi
  00476e76  0f8537010000          jne       near 00476FB3h
  00476e7c  f7869000000002880000  test      dword [esi+90h],8802h
  00476e86  0f8527010000          jne       near 00476FB3h
  00476e8c  8d542410              lea       edx,[esp+10h]
  00476e90  52                    push      edx
  00476e91  56                    push      esi
  00476e92  e809deffff            call      00474CA0h
  00476e97  8bf8                  mov       edi,eax
  00476e99  83c408                add       esp,8
  00476e9c  3bfb                  cmp       edi,ebx
  00476e9e  0f84ca000000          je        near 00476F6Eh
  00476ea4  8b4674                mov       eax,[esi+74h]
  00476ea7  668b4e06              mov       cx,[esi+6]
  00476eab  57                    push      edi
  00476eac  66894806              mov       [eax+6],cx
  00476eb0  e8cb180c00            call      00538780h
  00476eb5  8b5674                mov       edx,[esi+74h]
  00476eb8  83c404                add       esp,4
  00476ebb  53                    push      ebx
  00476ebc  57                    push      edi
  00476ebd  52                    push      edx
  00476ebe  e86d480c00            call      0053B730h
  00476ec3  8b07                  mov       eax,[edi]
  00476ec5  83c40c                add       esp,0Ch
  00476ec8  0c40                  or        al,40h
  00476eca  66c787960000001000    mov       word [edi+96h],10h
  00476ed3  57                    push      edi
  00476ed4  8907                  mov       [edi],eax
  00476ed6  e8e51c0c00            call      00538BC0h
  00476edb  8bc8                  mov       ecx,eax
  00476edd  83c404                add       esp,4
  00476ee0  83e1fe                and       ecx,0FFFFFFFEh
  00476ee3  895e44                mov       [esi+44h],ebx
  00476ee6  c1e10b                shl       ecx,0Bh
  00476ee9  c1e00c                shl       eax,0Ch
  00476eec  894e48                mov       [esi+48h],ecx
  00476eef  8bc8                  mov       ecx,eax
  00476ef1  b8b5814e1b            mov       eax,1B4E81B5h
  00476ef6  6a03                  push      3
  00476ef8  f7e9                  imul      ecx
  00476efa  c1fa03                sar       edx,3
  00476efd  8bc2                  mov       eax,edx
  00476eff  53                    push      ebx
  00476f00  c1e81f                shr       eax,1Fh
  00476f03  03d0                  add       edx,eax
  00476f05  68be000000            push      0BEh
  00476f0a  8bca                  mov       ecx,edx
  00476f0c  68d0195500            push      5519D0h
  00476f11  c1e110                shl       ecx,10h
  00476f14  0bca                  or        ecx,edx
  00476f16  55                    push      ebp
  00476f17  56                    push      esi
  00476f18  895e4c                mov       [esi+4Ch],ebx
  00476f1b  895e50                mov       [esi+50h],ebx
  00476f1e  895e54                mov       [esi+54h],ebx
  00476f21  895e58                mov       [esi+58h],ebx
  00476f24  894e28                mov       [esi+28h],ecx
  00476f27  66c746161f00          mov       word [esi+16h],1Fh
  00476f2d  c7463860606001        mov       dword [esi+38h],1606060h
  00476f34  66c7461a0700          mov       word [esi+1Ah],7
  00476f3a  897e70                mov       [esi+70h],edi
  00476f3d  e8fe3c0000            call      0047AC40h
  00476f42  83c418                add       esp,18h
  00476f45  55                    push      ebp
  00476f46  6800120000            push      1200h
  00476f4b  6829030000            push      329h
  00476f50  6a21                  push      21h
  00476f52  e8c96b0c00            call      0053DB20h
  00476f57  8b4674                mov       eax,[esi+74h]
  00476f5a  83c410                add       esp,10h
  00476f5d  8b08                  mov       ecx,[eax]
  00476f5f  80e17f                and       cl,7Fh
  00476f62  8908                  mov       [eax],ecx
  00476f64  891e                  mov       [esi],ebx
  00476f66  5f                    pop       edi
  00476f67  5e                    pop       esi
  00476f68  5d                    pop       ebp
  00476f69  5b                    pop       ebx
  00476f6a  83c418                add       esp,18h
  00476f6d  c3                    ret

LAB_00476f6e:
  00476f6e  8a4e40                mov       cl,[esi+40h]
  00476f71  fec1                  inc       cl
  00476f73  660fb6d1              movzx     dx,cl
  00476f77  663b561a              cmp       dx,[esi+1Ah]
  00476f7b  884e40                mov       [esi+40h],cl
  00476f7e  7c03                  jl        short 00476F83h
  00476f80  885e40                mov       [esi+40h],bl

LAB_00476f83:
  00476f83  0fbf4610              movsx     eax,word [esi+10h]
  00476f87  99                    cdq
  00476f88  b90e000000            mov       ecx,0Eh
  00476f8d  f7f9                  idiv      ecx
  00476f8f  85d2                  test      edx,edx
  00476f91  7515                  jne       short 00476FA8h
  00476f93  55                    push      ebp
  00476f94  6800120000            push      1200h
  00476f99  6800010000            push      100h
  00476f9e  6a17                  push      17h
  00476fa0  e87b6b0c00            call      0053DB20h
  00476fa5  83c410                add       esp,10h

LAB_00476fa8:
  00476fa8  66ff4610              inc       word [esi+10h]
  00476fac  66837e1028            cmp       word [esi+10h],28h
  00476fb1  7c0c                  jl        short 00476FBFh

LAB_00476fb3:
  00476fb3  8b4674                mov       eax,[esi+74h]
  00476fb6  8b08                  mov       ecx,[eax]
  00476fb8  80e17f                and       cl,7Fh
  00476fbb  8908                  mov       [eax],ecx
  00476fbd  891e                  mov       [esi],ebx

LAB_00476fbf:
  00476fbf  5f                    pop       edi
  00476fc0  5e                    pop       esi
  00476fc1  5d                    pop       ebp
  00476fc2  5b                    pop       ebx
  00476fc3  83c418                add       esp,18h
  00476fc6  c3                    ret

8 cible(s) de saut interne : 0x00476d40, 0x00476d9a, 0x00476da8, 0x00476f6e, 0x00476f83, 0x00476fa8, 0x00476fb3, 0x00476fbf
