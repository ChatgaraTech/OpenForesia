  00484860  83ec0c                sub       esp,0Ch
  00484863  53                    push      ebx
  00484864  55                    push      ebp
  00484865  56                    push      esi
  00484866  8b74241c              mov       esi,[esp+1Ch]
  0048486a  57                    push      edi
  0048486b  66c744241000fc        mov       word [esp+10h],0FC00h
  00484872  8b4670                mov       eax,[esi+70h]
  00484875  8b4e1c                mov       ecx,[esi+1Ch]
  00484878  8d6e1c                lea       ebp,[esi+1Ch]
  0048487b  8b7e24                mov       edi,[esi+24h]
  0048487e  89442420              mov       [esp+20h],eax
  00484882  8b4650                mov       eax,[esi+50h]
  00484885  8b5654                mov       edx,[esi+54h]
  00484888  8b5e20                mov       ebx,[esi+20h]
  0048488b  03c8                  add       ecx,eax
  0048488d  03da                  add       ebx,edx
  0048488f  894d00                mov       [ebp],ecx
  00484892  8b4e58                mov       ecx,[esi+58h]
  00484895  03f9                  add       edi,ecx
  00484897  895e20                mov       [esi+20h],ebx
  0048489a  897e24                mov       [esi+24h],edi
  0048489d  8bf8                  mov       edi,eax
  0048489f  c1ff03                sar       edi,3
  004848a2  8bd8                  mov       ebx,eax
  004848a4  66c7442412e2ff        mov       word [esp+12h],0FFE2h
  004848ab  2bdf                  sub       ebx,edi
  004848ad  66c74424146400        mov       word [esp+14h],64h
  004848b4  c1f80c                sar       eax,0Ch
  004848b7  2bd8                  sub       ebx,eax
  004848b9  66c74424163200        mov       word [esp+16h],32h
  004848c0  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004848c7  895e50                mov       [esi+50h],ebx
  004848ca  8bf8                  mov       edi,eax
  004848cc  8bda                  mov       ebx,edx
  004848ce  66c74424180000        mov       word [esp+18h],0
  004848d5  c1ff08                sar       edi,8
  004848d8  c1fb0c                sar       ebx,0Ch
  004848db  2bfb                  sub       edi,ebx
  004848dd  66c744241a3c00        mov       word [esp+1Ah],3Ch
  004848e4  03fa                  add       edi,edx
  004848e6  8bd1                  mov       edx,ecx
  004848e8  2bf8                  sub       edi,eax
  004848ea  8bc1                  mov       eax,ecx
  004848ec  c1fa03                sar       edx,3
  004848ef  2bc2                  sub       eax,edx
  004848f1  897e54                mov       [esi+54h],edi
  004848f4  c1f90c                sar       ecx,0Ch
  004848f7  2bc1                  sub       eax,ecx
  004848f9  894658                mov       [esi+58h],eax
  004848fc  8a4642                mov       al,[esi+42h]
  004848ff  84c0                  test      al,al
  00484901  0f84a7000000          je        near 004849AEh
  00484907  0fbf4628              movsx     eax,word [esi+28h]
  0048490b  b900500000            mov       ecx,5000h
  00484910  2bc8                  sub       ecx,eax
  00484912  c1f903                sar       ecx,3
  00484915  03c1                  add       eax,ecx
  00484917  8bd0                  mov       edx,eax
  00484919  c1e210                shl       edx,10h
  0048491c  0bd0                  or        edx,eax
  0048491e  8d442410              lea       eax,[esp+10h]
  00484922  50                    push      eax
  00484923  56                    push      esi
  00484924  895628                mov       [esi+28h],edx
  00484927  e87403ffff            call      00474CA0h
  0048492c  8bf8                  mov       edi,eax
  0048492e  83c408                add       esp,8
  00484931  85ff                  test      edi,edi
  00484933  744d                  je        short 00484982h
  00484935  8a4f08                mov       cl,[edi+8]
  00484938  8b5c2420              mov       ebx,[esp+20h]
  0048493c  ba01000000            mov       edx,1
  00484941  8a4309                mov       al,[ebx+9]
  00484944  d3e2                  shl       edx,cl
  00484946  84d0                  test      al,dl
  00484948  753c                  jne       short 00484986h
  0048494a  8b4e74                mov       ecx,[esi+74h]
  0048494d  6a00                  push      0
  0048494f  57                    push      edi
  00484950  c6410707              mov       byte [ecx+7],7
  00484954  8b5674                mov       edx,[esi+74h]
  00484957  52                    push      edx
  00484958  e8c37a0b00            call      0053C420h
  0048495d  33c9                  xor       ecx,ecx
  0048495f  b001                  mov       al,1
  00484961  8a4f08                mov       cl,[edi+8]
  00484964  83c40c                add       esp,0Ch
  00484967  d2e0                  shl       al,cl
  00484969  8a4b09                mov       cl,[ebx+9]
  0048496c  0ac8                  or        cl,al
  0048496e  884b09                mov       [ebx+9],cl
  00484971  8b07                  mov       eax,[edi]
  00484973  0c40                  or        al,40h
  00484975  66c787960000000800    mov       word [edi+96h],8
  0048497e  8907                  mov       [edi],eax
  00484980  eb04                  jmp       short 00484986h
  00484982  8b5c2420              mov       ebx,[esp+20h]
  00484986  8b4638                mov       eax,[esi+38h]
  00484989  05f0efefff            add       eax,0FFEFEFF0h
  0048498e  894638                mov       [esi+38h],eax
  00484991  0f850e010000          jne       near 00484AA5h
  00484997  8b03                  mov       eax,[ebx]
  00484999  0d00000002            or        eax,2000000h
  0048499e  8903                  mov       [ebx],eax
  004849a0  c70600000000          mov       dword [esi],0
  004849a6  5f                    pop       edi
  004849a7  5e                    pop       esi
  004849a8  5d                    pop       ebp
  004849a9  5b                    pop       ebx
  004849aa  83c40c                add       esp,0Ch
  004849ad  c3                    ret
  004849ae  8b7e20                mov       edi,[esi+20h]
  004849b1  56                    push      esi
  004849b2  e889ef0a00            call      00533940h
  004849b7  8b8690000000          mov       eax,[esi+90h]
  004849bd  83c404                add       esp,4
  004849c0  f6c4a0                test      ah,0A0h
  004849c3  897e20                mov       [esi+20h],edi
  004849c6  0f85b5000000          jne       near 00484A81h
  004849cc  a902080000            test      eax,802h
  004849d1  7509                  jne       short 004849DCh
  004849d3  3b7e60                cmp       edi,[esi+60h]
  004849d6  0f8fa5000000          jg        near 00484A81h
  004849dc  8b4e6c                mov       ecx,[esi+6Ch]
  004849df  c7462800100010        mov       dword [esi+28h],10001000h
  004849e6  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  004849ed  894e70                mov       [esi+70h],ecx
  004849f0  bb0c000000            mov       ebx,0Ch
  004849f5  0fbf7e32              movsx     edi,word [esi+32h]
  004849f9  57                    push      edi
  004849fa  e81d100c00            call      00545A1Ch
  004849ff  8d1480                lea       edx,[eax+eax*4]
  00484a02  57                    push      edi
  00484a03  c1e203                shl       edx,3
  00484a06  895650                mov       [esi+50h],edx
  00484a09  c7465400e00100        mov       dword [esi+54h],1E000h
  00484a10  e801100c00            call      00545A16h
  00484a15  8d0480                lea       eax,[eax+eax*4]
  00484a18  66c7869a0000000040    mov       word [esi+9Ah],4000h
  00484a21  c1e003                shl       eax,3
  00484a24  894658                mov       [esi+58h],eax
  00484a27  c6464201              mov       byte [esi+42h],1
  00484a2b  8b0dcc975e00          mov       ecx,[5E97CCh]
  00484a31  6a03                  push      3
  00484a33  6a00                  push      0
  00484a35  8b91d4000000          mov       edx,[ecx+0D4h]
  00484a3b  33c9                  xor       ecx,ecx
  00484a3d  8b02                  mov       eax,[edx]
  00484a3f  8a4808                mov       cl,[eax+8]
  00484a42  81c1c0000000          add       ecx,0C0h
  00484a48  51                    push      ecx
  00484a49  68b81b5500            push      551BB8h
  00484a4e  55                    push      ebp
  00484a4f  56                    push      esi
  00484a50  e8eb61ffff            call      0047AC40h
  00484a55  83c418                add       esp,18h
  00484a58  81c755010000          add       edi,155h
  00484a5e  4b                    dec       ebx
  00484a5f  66897e32              mov       [esi+32h],di
  00484a63  7590                  jne       short 004849F5h
  00484a65  55                    push      ebp
  00484a66  68d0265500            push      5526D0h
  00484a6b  e870910b00            call      0053DBE0h
  00484a70  83c408                add       esp,8
  00484a73  c70600000000          mov       dword [esi],0
  00484a79  5f                    pop       edi
  00484a7a  5e                    pop       esi
  00484a7b  5d                    pop       ebp
  00484a7c  5b                    pop       ebx
  00484a7d  83c40c                add       esp,0Ch
  00484a80  c3                    ret
  00484a81  f70600000020          test      dword [esi],20000000h
  00484a87  7508                  jne       short 00484A91h
  00484a89  81ff00001efb          cmp       edi,0FB1E0000h
  00484a8f  7d14                  jge       short 00484AA5h
  00484a91  8b442420              mov       eax,[esp+20h]
  00484a95  8b08                  mov       ecx,[eax]
  00484a97  81c900000002          or        ecx,2000000h
  00484a9d  8908                  mov       [eax],ecx
  00484a9f  c70600000000          mov       dword [esi],0
  00484aa5  5f                    pop       edi
  00484aa6  5e                    pop       esi
  00484aa7  5d                    pop       ebp
  00484aa8  5b                    pop       ebx
  00484aa9  83c40c                add       esp,0Ch
  00484aac  c3                    ret
