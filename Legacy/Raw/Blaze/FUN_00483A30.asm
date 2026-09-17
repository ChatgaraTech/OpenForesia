Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00483a30  section .text  offset fichier 0x82e30  288 octets

  00483a30  83ec0c                sub       esp,0Ch
  00483a33  53                    push      ebx
  00483a34  56                    push      esi
  00483a35  8b742418              mov       esi,[esp+18h]
  00483a39  b900200000            mov       ecx,2000h
  00483a3e  57                    push      edi
  00483a3f  66c744240c00fc        mov       word [esp+0Ch],0FC00h
  00483a46  0fbf4628              movsx     eax,word [esi+28h]
  00483a4a  2bc8                  sub       ecx,eax
  00483a4c  8b7e24                mov       edi,[esi+24h]
  00483a4f  c1f903                sar       ecx,3
  00483a52  03c1                  add       eax,ecx
  00483a54  8b4e1c                mov       ecx,[esi+1Ch]
  00483a57  8bd0                  mov       edx,eax
  00483a59  8b5e20                mov       ebx,[esi+20h]
  00483a5c  c1e210                shl       edx,10h
  00483a5f  0bd0                  or        edx,eax
  00483a61  6689462c              mov       [esi+2Ch],ax
  00483a65  8b4650                mov       eax,[esi+50h]
  00483a68  895628                mov       [esi+28h],edx
  00483a6b  8b5658                mov       edx,[esi+58h]
  00483a6e  03c8                  add       ecx,eax
  00483a70  894e1c                mov       [esi+1Ch],ecx
  00483a73  8b4e54                mov       ecx,[esi+54h]
  00483a76  03fa                  add       edi,edx
  00483a78  03d9                  add       ebx,ecx
  00483a7a  897e24                mov       [esi+24h],edi
  00483a7d  8bf8                  mov       edi,eax
  00483a7f  895e20                mov       [esi+20h],ebx
  00483a82  8bd8                  mov       ebx,eax
  00483a84  c1ff08                sar       edi,8
  00483a87  2bdf                  sub       ebx,edi
  00483a89  8bf9                  mov       edi,ecx
  00483a8b  c1f803                sar       eax,3
  00483a8e  2bd8                  sub       ebx,eax
  00483a90  8bc1                  mov       eax,ecx
  00483a92  c1f803                sar       eax,3
  00483a95  2bf8                  sub       edi,eax
  00483a97  8bc2                  mov       eax,edx
  00483a99  c1f908                sar       ecx,8
  00483a9c  2bf9                  sub       edi,ecx
  00483a9e  8bca                  mov       ecx,edx
  00483aa0  c1f908                sar       ecx,8
  00483aa3  2bc1                  sub       eax,ecx
  00483aa5  8d4c240c              lea       ecx,[esp+0Ch]
  00483aa9  c1fa03                sar       edx,3
  00483aac  2bc2                  sub       eax,edx
  00483aae  51                    push      ecx
  00483aaf  56                    push      esi
  00483ab0  66c7442416e2ff        mov       word [esp+16h],0FFE2h
  00483ab7  66c74424186400        mov       word [esp+18h],64h
  00483abe  66c744241a3200        mov       word [esp+1Ah],32h
  00483ac5  66c744241c0000        mov       word [esp+1Ch],0
  00483acc  66c744241e3c00        mov       word [esp+1Eh],3Ch
  00483ad3  895e50                mov       [esi+50h],ebx
  00483ad6  897e54                mov       [esi+54h],edi
  00483ad9  894658                mov       [esi+58h],eax
  00483adc  e8bf11ffff            call      00474CA0h
  00483ae1  8bf8                  mov       edi,eax
  00483ae3  83c408                add       esp,8
  00483ae6  85ff                  test      edi,edi
  00483ae8  7447                  je        short 00483B31h
  00483aea  8a4f08                mov       cl,[edi+8]
  00483aed  8a4609                mov       al,[esi+9]
  00483af0  ba01000000            mov       edx,1
  00483af5  d3e2                  shl       edx,cl
  00483af7  84d0                  test      al,dl
  00483af9  7536                  jne       short 00483B31h
  00483afb  8b4e74                mov       ecx,[esi+74h]
  00483afe  6a00                  push      0
  00483b00  57                    push      edi
  00483b01  c6410707              mov       byte [ecx+7],7
  00483b05  8b5674                mov       edx,[esi+74h]
  00483b08  52                    push      edx
  00483b09  e8027d0b00            call      0053B810h
  00483b0e  33c9                  xor       ecx,ecx
  00483b10  b001                  mov       al,1
  00483b12  8a4f08                mov       cl,[edi+8]
  00483b15  83c40c                add       esp,0Ch
  00483b18  d2e0                  shl       al,cl
  00483b1a  8a4e09                mov       cl,[esi+9]
  00483b1d  0ac8                  or        cl,al
  00483b1f  884e09                mov       [esi+9],cl
  00483b22  8b07                  mov       eax,[edi]
  00483b24  0c40                  or        al,40h
  00483b26  66c787960000000800    mov       word [edi+96h],8
  00483b2f  8907                  mov       [edi],eax

LAB_00483b31:
  00483b31  8b4638                mov       eax,[esi+38h]
  00483b34  05f0efefff            add       eax,0FFEFEFF0h
  00483b39  894638                mov       [esi+38h],eax
  00483b3c  7506                  jne       short 00483B44h
  00483b3e  c70600000000          mov       dword [esi],0

LAB_00483b44:
  00483b44  5f                    pop       edi
  00483b45  5e                    pop       esi
  00483b46  5b                    pop       ebx
  00483b47  83c40c                add       esp,0Ch
  00483b4a  c3                    ret
  00483b4b  90                    nop
  00483b4c  90                    nop
  00483b4d  90                    nop
  00483b4e  90                    nop
  00483b4f  90                    nop

2 cible(s) de saut interne : 0x00483b31, 0x00483b44
