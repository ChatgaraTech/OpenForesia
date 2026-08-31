Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00448ef0  section .text  offset fichier 0x482f0  701 octets

  00448ef0  53                    push      ebx
  00448ef1  55                    push      ebp
  00448ef2  56                    push      esi
  00448ef3  8b742410              mov       esi,[esp+10h]
  00448ef7  33db                  xor       ebx,ebx
  00448ef9  57                    push      edi
  00448efa  53                    push      ebx
  00448efb  53                    push      ebx
  00448efc  56                    push      esi
  00448efd  e83ef1ffff            call      00448040h
  00448f02  83c40c                add       esp,0Ch
  00448f05  66395e10              cmp       [esi+10h],bx
  00448f09  750b                  jne       short 00448F16h
  00448f0b  53                    push      ebx
  00448f0c  53                    push      ebx
  00448f0d  56                    push      esi
  00448f0e  e89dedffff            call      00447CB0h
  00448f13  83c40c                add       esp,0Ch

LAB_00448f16:
  00448f16  8b7e78                mov       edi,[esi+78h]
  00448f19  3bfb                  cmp       edi,ebx
  00448f1b  7505                  jne       short 00448F22h
  00448f1d  bf58e55d00            mov       edi,5DE558h

LAB_00448f22:
  00448f22  8b471c                mov       eax,[edi+1Ch]
  00448f25  66395e10              cmp       [esi+10h],bx
  00448f29  89461c                mov       [esi+1Ch],eax
  00448f2c  8b4f20                mov       ecx,[edi+20h]
  00448f2f  894e20                mov       [esi+20h],ecx
  00448f32  8b5724                mov       edx,[edi+24h]
  00448f35  895624                mov       [esi+24h],edx
  00448f38  0f85d3010000          jne       near 00449111h
  00448f3e  66c746100100          mov       word [esi+10h],1
  00448f44  66895e16              mov       [esi+16h],bx
  00448f48  668b4732              mov       ax,[edi+32h]
  00448f4c  66894614              mov       [esi+14h],ax
  00448f50  e8ebe10f00            call      00547140h
  00448f55  99                    cdq
  00448f56  33c2                  xor       eax,edx
  00448f58  6aff                  push      0FFFFFFFFh
  00448f5a  2bc2                  sub       eax,edx
  00448f5c  53                    push      ebx
  00448f5d  25ff0f0000            and       eax,0FFFh
  00448f62  bd18000000            mov       ebp,18h
  00448f67  33c2                  xor       eax,edx
  00448f69  68a2000000            push      0A2h
  00448f6e  2bc2                  sub       eax,edx
  00448f70  56                    push      esi
  00448f71  66894616              mov       [esi+16h],ax
  00448f75  c7465c0d000000        mov       dword [esi+5Ch],0Dh
  00448f7c  895e48                mov       [esi+48h],ebx
  00448f7f  6689ae9a000000        mov       [esi+9Ah],bp
  00448f86  e8551b0300            call      0047AAE0h
  00448f8b  83c410                add       esp,10h
  00448f8e  bb19000000            mov       ebx,19h
  00448f93  66899e9a000000        mov       [esi+9Ah],bx
  00448f9a  6aff                  push      0FFFFFFFFh
  00448f9c  6a00                  push      0
  00448f9e  68a2000000            push      0A2h
  00448fa3  56                    push      esi
  00448fa4  e8371b0300            call      0047AAE0h
  00448fa9  668b4f32              mov       cx,[edi+32h]
  00448fad  83c410                add       esp,10h
  00448fb0  6681c10004            add       cx,400h
  00448fb5  66894e14              mov       [esi+14h],cx
  00448fb9  e882e10f00            call      00547140h
  00448fbe  99                    cdq
  00448fbf  33c2                  xor       eax,edx
  00448fc1  6aff                  push      0FFFFFFFFh
  00448fc3  2bc2                  sub       eax,edx
  00448fc5  6a00                  push      0
  00448fc7  25ff0f0000            and       eax,0FFFh
  00448fcc  68a2000000            push      0A2h
  00448fd1  33c2                  xor       eax,edx
  00448fd3  56                    push      esi
  00448fd4  2bc2                  sub       eax,edx
  00448fd6  c7464800040000        mov       dword [esi+48h],400h
  00448fdd  66894616              mov       [esi+16h],ax
  00448fe1  6689ae9a000000        mov       [esi+9Ah],bp
  00448fe8  e8f31a0300            call      0047AAE0h
  00448fed  83c410                add       esp,10h
  00448ff0  66899e9a000000        mov       [esi+9Ah],bx
  00448ff7  6aff                  push      0FFFFFFFFh
  00448ff9  6a00                  push      0
  00448ffb  68a2000000            push      0A2h
  00449000  56                    push      esi
  00449001  e8da1a0300            call      0047AAE0h
  00449006  668b5732              mov       dx,[edi+32h]
  0044900a  83c410                add       esp,10h
  0044900d  6681c20008            add       dx,800h
  00449012  66895614              mov       [esi+14h],dx
  00449016  e825e10f00            call      00547140h
  0044901b  99                    cdq
  0044901c  33c2                  xor       eax,edx
  0044901e  6aff                  push      0FFFFFFFFh
  00449020  2bc2                  sub       eax,edx
  00449022  6a00                  push      0
  00449024  25ff0f0000            and       eax,0FFFh
  00449029  68a2000000            push      0A2h
  0044902e  33c2                  xor       eax,edx
  00449030  56                    push      esi
  00449031  2bc2                  sub       eax,edx
  00449033  c7464800080000        mov       dword [esi+48h],800h
  0044903a  66894616              mov       [esi+16h],ax
  0044903e  6689ae9a000000        mov       [esi+9Ah],bp
  00449045  e8961a0300            call      0047AAE0h
  0044904a  83c410                add       esp,10h
  0044904d  66899e9a000000        mov       [esi+9Ah],bx
  00449054  6aff                  push      0FFFFFFFFh
  00449056  6a00                  push      0
  00449058  68a2000000            push      0A2h
  0044905d  56                    push      esi
  0044905e  e87d1a0300            call      0047AAE0h
  00449063  668b4732              mov       ax,[edi+32h]
  00449067  83c410                add       esp,10h
  0044906a  6605000c              add       ax,0C00h
  0044906e  66894614              mov       [esi+14h],ax
  00449072  e8c9e00f00            call      00547140h
  00449077  99                    cdq
  00449078  33c2                  xor       eax,edx
  0044907a  6aff                  push      0FFFFFFFFh
  0044907c  2bc2                  sub       eax,edx
  0044907e  6a00                  push      0
  00449080  25ff0f0000            and       eax,0FFFh
  00449085  68a2000000            push      0A2h
  0044908a  33c2                  xor       eax,edx
  0044908c  56                    push      esi
  0044908d  2bc2                  sub       eax,edx
  0044908f  c74648000c0000        mov       dword [esi+48h],0C00h
  00449096  66894616              mov       [esi+16h],ax
  0044909a  6689ae9a000000        mov       [esi+9Ah],bp
  004490a1  e83a1a0300            call      0047AAE0h
  004490a6  83c410                add       esp,10h
  004490a9  66899e9a000000        mov       [esi+9Ah],bx
  004490b0  6aff                  push      0FFFFFFFFh
  004490b2  6a00                  push      0
  004490b4  68a2000000            push      0A2h
  004490b9  56                    push      esi
  004490ba  e8211a0300            call      0047AAE0h
  004490bf  83c410                add       esp,10h
  004490c2  bf02000000            mov       edi,2
  004490c7  66c746160000          mov       word [esi+16h],0
  004490cd  897e5c                mov       [esi+5Ch],edi
  004490d0  6aff                  push      0FFFFFFFFh
  004490d2  6a00                  push      0
  004490d4  68a3000000            push      0A3h
  004490d9  56                    push      esi
  004490da  e8011a0300            call      0047AAE0h
  004490df  8b4674                mov       eax,[esi+74h]
  004490e2  83c410                add       esp,10h
  004490e5  bba0000000            mov       ebx,0A0h
  004490ea  8b08                  mov       ecx,[eax]
  004490ec  80c980                or        cl,80h
  004490ef  8908                  mov       [eax],ecx
  004490f1  66c70560915e00dc00    mov       word [5E9160h],0DCh
  004490fa  66891d62915e00        mov       [5E9162h],bx
  00449101  66891d64915e00        mov       [5E9164h],bx
  00449108  66893d66915e00        mov       [5E9166h],di
  0044910f  eb0a                  jmp       short 0044911Bh

LAB_00449111:
  00449111  bba0000000            mov       ebx,0A0h
  00449116  bf02000000            mov       edi,2

LAB_0044911b:
  0044911b  668b4616              mov       ax,[esi+16h]
  0044911f  663d5000              cmp       ax,50h
  00449123  7c2b                  jl        short 00449150h
  00449125  663d8c00              cmp       ax,8Ch
  00449129  66c746145000          mov       word [esi+14h],50h
  0044912f  6689be9a000000        mov       [esi+9Ah],di
  00449136  7d18                  jge       short 00449150h
  00449138  6aff                  push      0FFFFFFFFh
  0044913a  6a00                  push      0
  0044913c  68a9000000            push      0A9h
  00449141  8d461c                lea       eax,[esi+1Ch]
  00449144  6a00                  push      0
  00449146  50                    push      eax
  00449147  56                    push      esi
  00449148  e8f31a0300            call      0047AC40h
  0044914d  83c418                add       esp,18h

LAB_00449150:
  00449150  66ff4616              inc       word [esi+16h]
  00449154  668b4616              mov       ax,[esi+16h]
  00449158  663d7800              cmp       ax,78h
  0044915c  7c0a                  jl        short 00449168h
  0044915e  8b4674                mov       eax,[esi+74h]
  00449161  8b08                  mov       ecx,[eax]
  00449163  80e17f                and       cl,7Fh
  00449166  8908                  mov       [eax],ecx

LAB_00449168:
  00449168  66395e16              cmp       [esi+16h],bx
  0044916c  7c3a                  jl        short 004491A8h
  0044916e  8b4674                mov       eax,[esi+74h]
  00449171  6a00                  push      0
  00449173  6a00                  push      0
  00449175  56                    push      esi
  00449176  81207fffffff          and       dword [eax],0FFFFFF7Fh
  0044917c  e89feeffff            call      00448020h
  00449181  8b4e74                mov       ecx,[esi+74h]
  00449184  8a5606                mov       dl,[esi+6]
  00449187  83c40c                add       esp,0Ch
  0044918a  885106                mov       [ecx+6],dl
  0044918d  8b4674                mov       eax,[esi+74h]
  00449190  8a4e07                mov       cl,[esi+7]
  00449193  6a00                  push      0
  00449195  884807                mov       [eax+7],cl
  00449198  8b5678                mov       edx,[esi+78h]
  0044919b  8b4674                mov       eax,[esi+74h]
  0044919e  52                    push      edx
  0044919f  50                    push      eax
  004491a0  e80b000000            call      004491B0h
  004491a5  83c40c                add       esp,0Ch

LAB_004491a8:
  004491a8  5f                    pop       edi
  004491a9  5e                    pop       esi
  004491aa  5d                    pop       ebp
  004491ab  5b                    pop       ebx
  004491ac  c3                    ret

7 cible(s) de saut interne : 0x00448f16, 0x00448f22, 0x00449111, 0x0044911b, 0x00449150, 0x00449168, 0x004491a8
