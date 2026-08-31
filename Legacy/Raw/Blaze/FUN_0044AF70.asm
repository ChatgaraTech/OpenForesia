Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044af70  section .text  offset fichier 0x4a370  1440 octets

  0044af70  53                    push      ebx
  0044af71  55                    push      ebp
  0044af72  56                    push      esi
  0044af73  8b742410              mov       esi,[esp+10h]
  0044af77  33ed                  xor       ebp,ebp
  0044af79  57                    push      edi
  0044af7a  8b4e78                mov       ecx,[esi+78h]
  0044af7d  8bc1                  mov       eax,ecx
  0044af7f  3bc5                  cmp       eax,ebp
  0044af81  7505                  jne       short 0044AF88h
  0044af83  b858e55d00            mov       eax,5DE558h

LAB_0044af88:
  0044af88  8b501c                mov       edx,[eax+1Ch]
  0044af8b  bff4010000            mov       edi,1F4h
  0044af90  89561c                mov       [esi+1Ch],edx
  0044af93  8b5060                mov       edx,[eax+60h]
  0044af96  81c200800c00          add       edx,0C8000h
  0044af9c  66396e10              cmp       [esi+10h],bp
  0044afa0  895620                mov       [esi+20h],edx
  0044afa3  8b4024                mov       eax,[eax+24h]
  0044afa6  894624                mov       [esi+24h],eax
  0044afa9  ba01000000            mov       edx,1
  0044afae  7558                  jne       short 0044B008h
  0044afb0  8bc1                  mov       eax,ecx
  0044afb2  885604                mov       [esi+4],dl
  0044afb5  3bc5                  cmp       eax,ebp
  0044afb7  c6460a02              mov       byte [esi+0Ah],2
  0044afbb  c6460b00              mov       byte [esi+0Bh],0
  0044afbf  66895610              mov       [esi+10h],dx
  0044afc3  c6463800              mov       byte [esi+38h],0
  0044afc7  c6463900              mov       byte [esi+39h],0
  0044afcb  c6463a00              mov       byte [esi+3Ah],0
  0044afcf  66897e28              mov       [esi+28h],di
  0044afd3  66896e2a              mov       [esi+2Ah],bp
  0044afd7  66897e2c              mov       [esi+2Ch],di
  0044afdb  7505                  jne       short 0044AFE2h
  0044afdd  b858e55d00            mov       eax,5DE558h

LAB_0044afe2:
  0044afe2  8b481c                mov       ecx,[eax+1Ch]
  0044afe5  894e1c                mov       [esi+1Ch],ecx
  0044afe8  8b4860                mov       ecx,[eax+60h]
  0044afeb  81c100800c00          add       ecx,0C8000h
  0044aff1  66896e12              mov       [esi+12h],bp
  0044aff5  894e20                mov       [esi+20h],ecx
  0044aff8  8b4024                mov       eax,[eax+24h]
  0044affb  894624                mov       [esi+24h],eax
  0044affe  8b4674                mov       eax,[esi+74h]
  0044b001  8b08                  mov       ecx,[eax]
  0044b003  80c980                or        cl,80h
  0044b006  8908                  mov       [eax],ecx

LAB_0044b008:
  0044b008  668146325802          add       word [esi+32h],258h
  0044b00e  668b4632              mov       ax,[esi+32h]
  0044b012  663d0010              cmp       ax,1000h
  0044b016  7c09                  jl        short 0044B021h
  0044b018  0500f0ffff            add       eax,0FFFFF000h
  0044b01d  66894632              mov       [esi+32h],ax

LAB_0044b021:
  0044b021  66395610              cmp       [esi+10h],dx
  0044b025  0f8580000000          jne       near 0044B0ABh
  0044b02b  8b4678                mov       eax,[esi+78h]
  0044b02e  3bc5                  cmp       eax,ebp
  0044b030  7505                  jne       short 0044B037h
  0044b032  b858e55d00            mov       eax,5DE558h

LAB_0044b037:
  0044b037  8b481c                mov       ecx,[eax+1Ch]
  0044b03a  894e1c                mov       [esi+1Ch],ecx
  0044b03d  8b5024                mov       edx,[eax+24h]
  0044b040  66017e28              add       [esi+28h],di
  0044b044  668b4e28              mov       cx,[esi+28h]
  0044b048  66017e2c              add       [esi+2Ch],di
  0044b04c  895624                mov       [esi+24h],edx
  0044b04f  baf82a0000            mov       edx,2AF8h
  0044b054  663bca                cmp       cx,dx
  0044b057  7c08                  jl        short 0044B061h
  0044b059  66895628              mov       [esi+28h],dx
  0044b05d  6689562c              mov       [esi+2Ch],dx

LAB_0044b061:
  0044b061  66017e2a              add       [esi+2Ah],di
  0044b065  6683461209            add       word [esi+12h],9
  0044b06a  0fbf7e12              movsx     edi,word [esi+12h]
  0044b06e  8b5860                mov       ebx,[eax+60h]
  0044b071  668b4e2a              mov       cx,[esi+2Ah]
  0044b075  c1e70c                shl       edi,0Ch
  0044b078  2bdf                  sub       ebx,edi
  0044b07a  81c300800c00          add       ebx,0C8000h
  0044b080  663bca                cmp       cx,dx
  0044b083  895e20                mov       [esi+20h],ebx
  0044b086  7c14                  jl        short 0044B09Ch
  0044b088  6689562a              mov       [esi+2Ah],dx
  0044b08c  8b4060                mov       eax,[eax+60h]
  0044b08f  894620                mov       [esi+20h],eax
  0044b092  66c746100200          mov       word [esi+10h],2
  0044b098  66896e12              mov       [esi+12h],bp

LAB_0044b09c:
  0044b09c  6a3c                  push      3Ch
  0044b09e  6a28                  push      28h
  0044b0a0  6a28                  push      28h
  0044b0a2  56                    push      esi
  0044b0a3  e8389dfbff            call      00404DE0h
  0044b0a8  83c410                add       esp,10h

LAB_0044b0ab:
  0044b0ab  66837e1002            cmp       word [esi+10h],2
  0044b0b0  0f85f8030000          jne       near 0044B4AEh
  0044b0b6  8b4678                mov       eax,[esi+78h]
  0044b0b9  3bc5                  cmp       eax,ebp
  0044b0bb  7505                  jne       short 0044B0C2h
  0044b0bd  b858e55d00            mov       eax,5DE558h

LAB_0044b0c2:
  0044b0c2  8b481c                mov       ecx,[eax+1Ch]
  0044b0c5  894e1c                mov       [esi+1Ch],ecx
  0044b0c8  8b5060                mov       edx,[eax+60h]
  0044b0cb  0fbf4e12              movsx     ecx,word [esi+12h]
  0044b0cf  895620                mov       [esi+20h],edx
  0044b0d2  8b4024                mov       eax,[eax+24h]
  0044b0d5  51                    push      ecx
  0044b0d6  894624                mov       [esi+24h],eax
  0044b0d9  e83ea90f00            call      00545A1Ch
  0044b0de  0fbf5612              movsx     edx,word [esi+12h]
  0044b0e2  c1f807                sar       eax,7
  0044b0e5  2c38                  sub       al,38h
  0044b0e7  52                    push      edx
  0044b0e8  884638                mov       [esi+38h],al
  0044b0eb  e82ca90f00            call      00545A1Ch
  0044b0f0  c1f807                sar       eax,7
  0044b0f3  2c38                  sub       al,38h
  0044b0f5  884639                mov       [esi+39h],al
  0044b0f8  0fbf4612              movsx     eax,word [esi+12h]
  0044b0fc  50                    push      eax
  0044b0fd  e81aa90f00            call      00545A1Ch
  0044b102  668146122c01          add       word [esi+12h],12Ch
  0044b108  c1f807                sar       eax,7
  0044b10b  2c38                  sub       al,38h
  0044b10d  88463a                mov       [esi+3Ah],al
  0044b110  668b4612              mov       ax,[esi+12h]
  0044b114  663d0010              cmp       ax,1000h
  0044b118  7c09                  jl        short 0044B123h
  0044b11a  0500f0ffff            add       eax,0FFFFF000h
  0044b11f  66894612              mov       [esi+12h],ax

LAB_0044b123:
  0044b123  f70600008000          test      dword [esi],800000h
  0044b129  0f84c7000000          je        near 0044B1F6h
  0044b12f  8b4674                mov       eax,[esi+74h]
  0044b132  8b38                  mov       edi,[eax]
  0044b134  81cf80000000          or        edi,80h
  0044b13a  8938                  mov       [eax],edi
  0044b13c  8b7e74                mov       edi,[esi+74h]
  0044b13f  8b15b4876000          mov       edx,[6087B4h]
  0044b145  33c0                  xor       eax,eax
  0044b147  8a4708                mov       al,[edi+8]
  0044b14a  8b1a                  mov       ebx,[edx]
  0044b14c  8d4878                lea       ecx,[eax+78h]
  0044b14f  8d14c0                lea       edx,[eax+eax*8]
  0044b152  c1e10d                shl       ecx,0Dh
  0044b155  03cb                  add       ecx,ebx
  0044b157  8d1450                lea       edx,[eax+edx*2]
  0044b15a  33db                  xor       ebx,ebx
  0044b15c  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0044b163  c1e304                shl       ebx,4
  0044b166  668b93204a6000        mov       dx,[ebx+604A20h]
  0044b16d  668591c41f0000        test      [ecx+1FC4h],dx
  0044b174  750d                  jne       short 0044B183h
  0044b176  66c746100300          mov       word [esi+10h],3
  0044b17c  8b17                  mov       edx,[edi]
  0044b17e  80e27f                and       dl,7Fh
  0044b181  8917                  mov       [edi],edx

LAB_0044b183:
  0044b183  f78140010000e8030000  test      dword [ecx+140h],3E8h
  0044b18d  7515                  jne       short 0044B1A4h
  0044b18f  8d1480                lea       edx,[eax+eax*4]
  0044b192  c1e203                shl       edx,3
  0044b195  2bd0                  sub       edx,eax
  0044b197  f70495c0935e0000000040  test      dword [edx*4+5E93C0h],40000000h
  0044b1a2  7410                  je        short 0044B1B4h

LAB_0044b1a4:
  0044b1a4  8b4674                mov       eax,[esi+74h]
  0044b1a7  66c746100300          mov       word [esi+10h],3
  0044b1ad  8b10                  mov       edx,[eax]
  0044b1af  80e27f                and       dl,7Fh
  0044b1b2  8910                  mov       [eax],edx

LAB_0044b1b4:
  0044b1b4  a1f02c5f00            mov       eax,[5F2CF0h]
  0044b1b9  33d2                  xor       edx,edx
  0044b1bb  bf0a000000            mov       edi,0Ah
  0044b1c0  f7f7                  div       edi
  0044b1c2  85d2                  test      edx,edx
  0044b1c4  7530                  jne       short 0044B1F6h
  0044b1c6  668b814e010000        mov       ax,[ecx+14Eh]
  0044b1cd  663d0200              cmp       ax,2
  0044b1d1  7c0c                  jl        short 0044B1DFh
  0044b1d3  83c0fe                add       eax,0FFFFFFFEh
  0044b1d6  6689814e010000        mov       [ecx+14Eh],ax
  0044b1dd  eb17                  jmp       short 0044B1F6h

LAB_0044b1df:
  0044b1df  6689a94e010000        mov       [ecx+14Eh],bp
  0044b1e6  8b4674                mov       eax,[esi+74h]
  0044b1e9  66c746100300          mov       word [esi+10h],3
  0044b1ef  8b08                  mov       ecx,[eax]
  0044b1f1  80e17f                and       cl,7Fh
  0044b1f4  8908                  mov       [eax],ecx

LAB_0044b1f6:
  0044b1f6  8b4674                mov       eax,[esi+74h]
  0044b1f9  3bc5                  cmp       eax,ebp
  0044b1fb  7505                  jne       short 0044B202h
  0044b1fd  b858e55d00            mov       eax,5DE558h

LAB_0044b202:
  0044b202  f70000008000          test      dword [eax],800000h
  0044b208  0f84a0020000          je        near 0044B4AEh
  0044b20e  bfa0ca5e00            mov       edi,5ECAA0h
  0044b213  bb20000000            mov       ebx,20h

LAB_0044b218:
  0044b218  8b07                  mov       eax,[edi]
  0044b21a  a900000080            test      eax,80000000h
  0044b21f  7463                  je        short 0044B284h
  0044b221  a900000040            test      eax,40000000h
  0044b226  755c                  jne       short 0044B284h
  0044b228  8b461c                mov       eax,[esi+1Ch]
  0044b22b  8b571c                mov       edx,[edi+1Ch]
  0044b22e  8b4e20                mov       ecx,[esi+20h]
  0044b231  8b6f20                mov       ebp,[edi+20h]
  0044b234  2bc2                  sub       eax,edx
  0044b236  8b5624                mov       edx,[esi+24h]
  0044b239  2bcd                  sub       ecx,ebp
  0044b23b  8b6f24                mov       ebp,[edi+24h]
  0044b23e  c1f80c                sar       eax,0Ch
  0044b241  2bd5                  sub       edx,ebp
  0044b243  8be8                  mov       ebp,eax
  0044b245  c1f90c                sar       ecx,0Ch
  0044b248  0fafe8                imul      ebp,eax
  0044b24b  8bc1                  mov       eax,ecx
  0044b24d  c1fa0c                sar       edx,0Ch
  0044b250  0fafc1                imul      eax,ecx
  0044b253  8bca                  mov       ecx,edx
  0044b255  03e8                  add       ebp,eax
  0044b257  0fafca                imul      ecx,edx
  0044b25a  03e9                  add       ebp,ecx
  0044b25c  55                    push      ebp
  0044b25d  e8aea70f00            call      00545A10h
  0044b262  3d36010000            cmp       eax,136h
  0044b267  7d1b                  jge       short 0044B284h
  0044b269  8a4e3a                mov       cl,[esi+3Ah]
  0044b26c  66c7869a0000000200    mov       word [esi+9Ah],2
  0044b275  80c19c                add       cl,9Ch
  0044b278  884e3a                mov       [esi+3Ah],cl
  0044b27b  8b07                  mov       eax,[edi]
  0044b27d  257ff7ffff            and       eax,0FFFFF77Fh
  0044b282  8907                  mov       [edi],eax

LAB_0044b284:
  0044b284  81c79c000000          add       edi,9Ch
  0044b28a  4b                    dec       ebx
  0044b28b  758b                  jne       short 0044B218h
  0044b28d  bd402d5f00            mov       ebp,5F2D40h
  0044b292  c744241480000000      mov       dword [esp+14h],80h

LAB_0044b29a:
  0044b29a  8b4500                mov       eax,[ebp]
  0044b29d  a900000080            test      eax,80000000h
  0044b2a2  0f8485000000          je        near 0044B32Dh
  0044b2a8  a900004000            test      eax,400000h
  0044b2ad  747e                  je        short 0044B32Dh
  0044b2af  f6c408                test      ah,8
  0044b2b2  7479                  je        short 0044B32Dh
  0044b2b4  a880                  test      al,80h
  0044b2b6  7475                  je        short 0044B32Dh
  0044b2b8  8b461c                mov       eax,[esi+1Ch]
  0044b2bb  8b551c                mov       edx,[ebp+1Ch]
  0044b2be  8b4e20                mov       ecx,[esi+20h]
  0044b2c1  8b5d20                mov       ebx,[ebp+20h]
  0044b2c4  8d7d1c                lea       edi,[ebp+1Ch]
  0044b2c7  2bc2                  sub       eax,edx
  0044b2c9  8b5624                mov       edx,[esi+24h]
  0044b2cc  2bcb                  sub       ecx,ebx
  0044b2ce  8b5f08                mov       ebx,[edi+8]
  0044b2d1  c1f80c                sar       eax,0Ch
  0044b2d4  2bd3                  sub       edx,ebx
  0044b2d6  8bd8                  mov       ebx,eax
  0044b2d8  c1f90c                sar       ecx,0Ch
  0044b2db  0fafd8                imul      ebx,eax
  0044b2de  8bc1                  mov       eax,ecx
  0044b2e0  c1fa0c                sar       edx,0Ch
  0044b2e3  0fafc1                imul      eax,ecx
  0044b2e6  8bca                  mov       ecx,edx
  0044b2e8  03d8                  add       ebx,eax
  0044b2ea  0fafca                imul      ecx,edx
  0044b2ed  03d9                  add       ebx,ecx
  0044b2ef  53                    push      ebx
  0044b2f0  e81ba70f00            call      00545A10h
  0044b2f5  3d36010000            cmp       eax,136h
  0044b2fa  7d31                  jge       short 0044B32Dh
  0044b2fc  8a463a                mov       al,[esi+3Ah]
  0044b2ff  6aff                  push      0FFFFFFFFh
  0044b301  6a00                  push      0
  0044b303  68a5000000            push      0A5h
  0044b308  6a00                  push      0
  0044b30a  049c                  add       al,9Ch
  0044b30c  57                    push      edi
  0044b30d  56                    push      esi
  0044b30e  88463a                mov       [esi+3Ah],al
  0044b311  66c7869a0000000200    mov       word [esi+9Ah],2
  0044b31a  e821f90200            call      0047AC40h
  0044b31f  8b4500                mov       eax,[ebp]
  0044b322  83c418                add       esp,18h
  0044b325  0d00000040            or        eax,40000000h
  0044b32a  894500                mov       [ebp],eax

LAB_0044b32d:
  0044b32d  8b442414              mov       eax,[esp+14h]
  0044b331  81c59c000000          add       ebp,9Ch
  0044b337  48                    dec       eax
  0044b338  89442414              mov       [esp+14h],eax
  0044b33c  0f8558ffffff          jne       near 0044B29Ah
  0044b342  bde0de5e00            mov       ebp,5EDEE0h
  0044b347  c744241480000000      mov       dword [esp+14h],80h

LAB_0044b34f:
  0044b34f  8b4500                mov       eax,[ebp]
  0044b352  a900000080            test      eax,80000000h
  0044b357  0f8485000000          je        near 0044B3E2h
  0044b35d  a900004000            test      eax,400000h
  0044b362  747e                  je        short 0044B3E2h
  0044b364  f6c408                test      ah,8
  0044b367  7479                  je        short 0044B3E2h
  0044b369  a880                  test      al,80h
  0044b36b  7475                  je        short 0044B3E2h
  0044b36d  8b461c                mov       eax,[esi+1Ch]
  0044b370  8b551c                mov       edx,[ebp+1Ch]
  0044b373  8b4e20                mov       ecx,[esi+20h]
  0044b376  8b5d20                mov       ebx,[ebp+20h]
  0044b379  8d7d1c                lea       edi,[ebp+1Ch]
  0044b37c  2bc2                  sub       eax,edx
  0044b37e  8b5624                mov       edx,[esi+24h]
  0044b381  2bcb                  sub       ecx,ebx
  0044b383  8b5f08                mov       ebx,[edi+8]
  0044b386  c1f80c                sar       eax,0Ch
  0044b389  2bd3                  sub       edx,ebx
  0044b38b  8bd8                  mov       ebx,eax
  0044b38d  c1f90c                sar       ecx,0Ch
  0044b390  0fafd8                imul      ebx,eax
  0044b393  8bc1                  mov       eax,ecx
  0044b395  c1fa0c                sar       edx,0Ch
  0044b398  0fafc1                imul      eax,ecx
  0044b39b  8bca                  mov       ecx,edx
  0044b39d  03d8                  add       ebx,eax
  0044b39f  0fafca                imul      ecx,edx
  0044b3a2  03d9                  add       ebx,ecx
  0044b3a4  53                    push      ebx
  0044b3a5  e866a60f00            call      00545A10h
  0044b3aa  3d36010000            cmp       eax,136h
  0044b3af  7d31                  jge       short 0044B3E2h
  0044b3b1  8a463a                mov       al,[esi+3Ah]
  0044b3b4  6aff                  push      0FFFFFFFFh
  0044b3b6  6a00                  push      0
  0044b3b8  68a5000000            push      0A5h
  0044b3bd  6a00                  push      0
  0044b3bf  049c                  add       al,9Ch
  0044b3c1  57                    push      edi
  0044b3c2  56                    push      esi
  0044b3c3  88463a                mov       [esi+3Ah],al
  0044b3c6  66c7869a0000000200    mov       word [esi+9Ah],2
  0044b3cf  e86cf80200            call      0047AC40h
  0044b3d4  8b4500                mov       eax,[ebp]
  0044b3d7  83c418                add       esp,18h
  0044b3da  0d00000040            or        eax,40000000h
  0044b3df  894500                mov       [ebp],eax

LAB_0044b3e2:
  0044b3e2  8b442414              mov       eax,[esp+14h]
  0044b3e6  81c59c000000          add       ebp,9Ch
  0044b3ec  48                    dec       eax
  0044b3ed  89442414              mov       [esp+14h],eax
  0044b3f1  0f8558ffffff          jne       near 0044B34Fh
  0044b3f7  bd00f45f00            mov       ebp,5FF400h
  0044b3fc  c744241420000000      mov       dword [esp+14h],20h

LAB_0044b404:
  0044b404  8b4500                mov       eax,[ebp]
  0044b407  a900000080            test      eax,80000000h
  0044b40c  0f8485000000          je        near 0044B497h
  0044b412  a900004000            test      eax,400000h
  0044b417  747e                  je        short 0044B497h
  0044b419  f6c408                test      ah,8
  0044b41c  7479                  je        short 0044B497h
  0044b41e  a880                  test      al,80h
  0044b420  7475                  je        short 0044B497h
  0044b422  8b461c                mov       eax,[esi+1Ch]
  0044b425  8b551c                mov       edx,[ebp+1Ch]
  0044b428  8b4e20                mov       ecx,[esi+20h]
  0044b42b  8b5d20                mov       ebx,[ebp+20h]
  0044b42e  8d7d1c                lea       edi,[ebp+1Ch]
  0044b431  2bc2                  sub       eax,edx
  0044b433  8b5624                mov       edx,[esi+24h]
  0044b436  2bcb                  sub       ecx,ebx
  0044b438  8b5f08                mov       ebx,[edi+8]
  0044b43b  c1f80c                sar       eax,0Ch
  0044b43e  2bd3                  sub       edx,ebx
  0044b440  8bd8                  mov       ebx,eax
  0044b442  c1f90c                sar       ecx,0Ch
  0044b445  0fafd8                imul      ebx,eax
  0044b448  8bc1                  mov       eax,ecx
  0044b44a  c1fa0c                sar       edx,0Ch
  0044b44d  0fafc1                imul      eax,ecx
  0044b450  8bca                  mov       ecx,edx
  0044b452  03d8                  add       ebx,eax
  0044b454  0fafca                imul      ecx,edx
  0044b457  03d9                  add       ebx,ecx
  0044b459  53                    push      ebx
  0044b45a  e8b1a50f00            call      00545A10h
  0044b45f  3d36010000            cmp       eax,136h
  0044b464  7d31                  jge       short 0044B497h
  0044b466  8a463a                mov       al,[esi+3Ah]
  0044b469  6aff                  push      0FFFFFFFFh
  0044b46b  6a00                  push      0
  0044b46d  68a5000000            push      0A5h
  0044b472  6a00                  push      0
  0044b474  049c                  add       al,9Ch
  0044b476  57                    push      edi
  0044b477  56                    push      esi
  0044b478  88463a                mov       [esi+3Ah],al
  0044b47b  66c7869a0000000200    mov       word [esi+9Ah],2
  0044b484  e8b7f70200            call      0047AC40h
  0044b489  8b4500                mov       eax,[ebp]
  0044b48c  83c418                add       esp,18h
  0044b48f  0d00000040            or        eax,40000000h
  0044b494  894500                mov       [ebp],eax

LAB_0044b497:
  0044b497  8b442414              mov       eax,[esp+14h]
  0044b49b  81c59c000000          add       ebp,9Ch
  0044b4a1  48                    dec       eax
  0044b4a2  89442414              mov       [esp+14h],eax
  0044b4a6  0f8558ffffff          jne       near 0044B404h
  0044b4ac  33ed                  xor       ebp,ebp

LAB_0044b4ae:
  0044b4ae  66837e1003            cmp       word [esi+10h],3
  0044b4b3  7550                  jne       short 0044B505h
  0044b4b5  8b4678                mov       eax,[esi+78h]
  0044b4b8  3bc5                  cmp       eax,ebp
  0044b4ba  7505                  jne       short 0044B4C1h
  0044b4bc  b858e55d00            mov       eax,5DE558h

LAB_0044b4c1:
  0044b4c1  8b501c                mov       edx,[eax+1Ch]
  0044b4c4  6af6                  push      0FFFFFFF6h
  0044b4c6  89561c                mov       [esi+1Ch],edx
  0044b4c9  8b4860                mov       ecx,[eax+60h]
  0044b4cc  894e20                mov       [esi+20h],ecx
  0044b4cf  8b5024                mov       edx,[eax+24h]
  0044b4d2  6af6                  push      0FFFFFFF6h
  0044b4d4  6af6                  push      0FFFFFFF6h
  0044b4d6  56                    push      esi
  0044b4d7  895624                mov       [esi+24h],edx
  0044b4da  e80199fbff            call      00404DE0h
  0044b4df  8a463a                mov       al,[esi+3Ah]
  0044b4e2  83c410                add       esp,10h
  0044b4e5  84c0                  test      al,al
  0044b4e7  751c                  jne       short 0044B505h
  0044b4e9  8b0e                  mov       ecx,[esi]
  0044b4eb  81e1ffffff7f          and       ecx,7FFFFFFFh
  0044b4f1  890e                  mov       [esi],ecx
  0044b4f3  8b766c                mov       esi,[esi+6Ch]
  0044b4f6  3bf5                  cmp       esi,ebp
  0044b4f8  7505                  jne       short 0044B4FFh
  0044b4fa  be58e55d00            mov       esi,5DE558h

LAB_0044b4ff:
  0044b4ff  66c746100200          mov       word [esi+10h],2

LAB_0044b505:
  0044b505  5f                    pop       edi
  0044b506  5e                    pop       esi
  0044b507  5d                    pop       ebp
  0044b508  5b                    pop       ebx
  0044b509  c3                    ret
  0044b50a  90                    nop
  0044b50b  90                    nop
  0044b50c  90                    nop
  0044b50d  90                    nop
  0044b50e  90                    nop
  0044b50f  90                    nop

28 cible(s) de saut interne : 0x0044af88, 0x0044afe2, 0x0044b008, 0x0044b021, 0x0044b037, 0x0044b061, 0x0044b09c, 0x0044b0ab, 0x0044b0c2, 0x0044b123, 0x0044b183, 0x0044b1a4, 0x0044b1b4, 0x0044b1df, 0x0044b1f6, 0x0044b202, 0x0044b218, 0x0044b284, 0x0044b29a, 0x0044b32d, 0x0044b34f, 0x0044b3e2, 0x0044b404, 0x0044b497, 0x0044b4ae, 0x0044b4c1, 0x0044b4ff, 0x0044b505
