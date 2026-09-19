Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484bb0  section .text  offset fichier 0x83fb0  288 octets

  00484bb0  83ec0c                sub       esp,0Ch
  00484bb3  53                    push      ebx
  00484bb4  56                    push      esi
  00484bb5  8b742418              mov       esi,[esp+18h]
  00484bb9  b900200000            mov       ecx,2000h
  00484bbe  57                    push      edi
  00484bbf  66c744240c00fc        mov       word [esp+0Ch],0FC00h
  00484bc6  0fbf4628              movsx     eax,word [esi+28h]
  00484bca  2bc8                  sub       ecx,eax
  00484bcc  8b7e24                mov       edi,[esi+24h]
  00484bcf  c1f903                sar       ecx,3
  00484bd2  03c1                  add       eax,ecx
  00484bd4  8b4e1c                mov       ecx,[esi+1Ch]
  00484bd7  8bd0                  mov       edx,eax
  00484bd9  8b5e20                mov       ebx,[esi+20h]
  00484bdc  c1e210                shl       edx,10h
  00484bdf  0bd0                  or        edx,eax
  00484be1  6689462c              mov       [esi+2Ch],ax
  00484be5  8b4650                mov       eax,[esi+50h]
  00484be8  895628                mov       [esi+28h],edx
  00484beb  8b5658                mov       edx,[esi+58h]
  00484bee  03c8                  add       ecx,eax
  00484bf0  894e1c                mov       [esi+1Ch],ecx
  00484bf3  8b4e54                mov       ecx,[esi+54h]
  00484bf6  03fa                  add       edi,edx
  00484bf8  03d9                  add       ebx,ecx
  00484bfa  897e24                mov       [esi+24h],edi
  00484bfd  8bf8                  mov       edi,eax
  00484bff  895e20                mov       [esi+20h],ebx
  00484c02  8bd8                  mov       ebx,eax
  00484c04  c1ff03                sar       edi,3
  00484c07  2bdf                  sub       ebx,edi
  00484c09  8bf9                  mov       edi,ecx
  00484c0b  c1f808                sar       eax,8
  00484c0e  2bd8                  sub       ebx,eax
  00484c10  8bc1                  mov       eax,ecx
  00484c12  c1f808                sar       eax,8
  00484c15  2bf8                  sub       edi,eax
  00484c17  8bc2                  mov       eax,edx
  00484c19  c1f903                sar       ecx,3
  00484c1c  2bf9                  sub       edi,ecx
  00484c1e  8bca                  mov       ecx,edx
  00484c20  c1f908                sar       ecx,8
  00484c23  2bc1                  sub       eax,ecx
  00484c25  8d4c240c              lea       ecx,[esp+0Ch]
  00484c29  c1fa03                sar       edx,3
  00484c2c  2bc2                  sub       eax,edx
  00484c2e  51                    push      ecx
  00484c2f  56                    push      esi
  00484c30  66c7442416e2ff        mov       word [esp+16h],0FFE2h
  00484c37  66c74424186400        mov       word [esp+18h],64h
  00484c3e  66c744241a3200        mov       word [esp+1Ah],32h
  00484c45  66c744241c0000        mov       word [esp+1Ch],0
  00484c4c  66c744241e3c00        mov       word [esp+1Eh],3Ch
  00484c53  895e50                mov       [esi+50h],ebx
  00484c56  897e54                mov       [esi+54h],edi
  00484c59  894658                mov       [esi+58h],eax
  00484c5c  e83f00ffff            call      00474CA0h
  00484c61  8bf8                  mov       edi,eax
  00484c63  83c408                add       esp,8
  00484c66  85ff                  test      edi,edi
  00484c68  7447                  je        short 00484CB1h
  00484c6a  8a4f08                mov       cl,[edi+8]
  00484c6d  8a4609                mov       al,[esi+9]
  00484c70  ba01000000            mov       edx,1
  00484c75  d3e2                  shl       edx,cl
  00484c77  84d0                  test      al,dl
  00484c79  7536                  jne       short 00484CB1h
  00484c7b  8b4e74                mov       ecx,[esi+74h]
  00484c7e  6a00                  push      0
  00484c80  57                    push      edi
  00484c81  c6410707              mov       byte [ecx+7],7
  00484c85  8b5674                mov       edx,[esi+74h]
  00484c88  52                    push      edx
  00484c89  e8826b0b00            call      0053B810h
  00484c8e  33c9                  xor       ecx,ecx
  00484c90  b001                  mov       al,1
  00484c92  8a4f08                mov       cl,[edi+8]
  00484c95  83c40c                add       esp,0Ch
  00484c98  d2e0                  shl       al,cl
  00484c9a  8a4e09                mov       cl,[esi+9]
  00484c9d  0ac8                  or        cl,al
  00484c9f  884e09                mov       [esi+9],cl
  00484ca2  8b07                  mov       eax,[edi]
  00484ca4  0c40                  or        al,40h
  00484ca6  66c787960000000800    mov       word [edi+96h],8
  00484caf  8907                  mov       [edi],eax

LAB_00484cb1:
  00484cb1  8b4638                mov       eax,[esi+38h]
  00484cb4  05f0efefff            add       eax,0FFEFEFF0h
  00484cb9  894638                mov       [esi+38h],eax
  00484cbc  7506                  jne       short 00484CC4h
  00484cbe  c70600000000          mov       dword [esi],0

LAB_00484cc4:
  00484cc4  5f                    pop       edi
  00484cc5  5e                    pop       esi
  00484cc6  5b                    pop       ebx
  00484cc7  83c40c                add       esp,0Ch
  00484cca  c3                    ret
  00484ccb  90                    nop
  00484ccc  90                    nop
  00484ccd  90                    nop
  00484cce  90                    nop
  00484ccf  90                    nop

2 cible(s) de saut interne : 0x00484cb1, 0x00484cc4
