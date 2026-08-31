Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00480a70  section .text  offset fichier 0x7fe70  283 octets

  00480a70  83ec0c                sub       esp,0Ch
  00480a73  53                    push      ebx
  00480a74  56                    push      esi
  00480a75  8b742418              mov       esi,[esp+18h]
  00480a79  33db                  xor       ebx,ebx
  00480a7b  66895c2408            mov       [esp+8],bx
  00480a80  66c744240ab0ff        mov       word [esp+0Ah],0FFB0h
  00480a87  8b06                  mov       eax,[esi]
  00480a89  66c744240c5000        mov       word [esp+0Ch],50h
  00480a90  a900000040            test      eax,40000000h
  00480a95  66c744240ec800        mov       word [esp+0Eh],0C8h
  00480a9c  66895c2410            mov       [esp+10h],bx
  00480aa1  66c7442412a000        mov       word [esp+12h],0A0h
  00480aa8  7408                  je        short 00480AB2h
  00480aaa  891e                  mov       [esi],ebx
  00480aac  5e                    pop       esi
  00480aad  5b                    pop       ebx
  00480aae  83c40c                add       esp,0Ch
  00480ab1  c3                    ret

LAB_00480ab2:
  00480ab2  668b4e10              mov       cx,[esi+10h]
  00480ab6  57                    push      edi
  00480ab7  6683f904              cmp       cx,4
  00480abb  7d1c                  jge       short 00480AD9h
  00480abd  0c80                  or        al,80h
  00480abf  bf00190000            mov       edi,1900h
  00480ac4  8906                  mov       [esi],eax
  00480ac6  668b462a              mov       ax,[esi+2Ah]
  00480aca  0fbfd0                movsx     edx,ax
  00480acd  2bfa                  sub       edi,edx
  00480acf  d1ff                  sar       edi,1
  00480ad1  03f8                  add       edi,eax
  00480ad3  66897e2a              mov       [esi+2Ah],di
  00480ad7  eb38                  jmp       short 00480B11h

LAB_00480ad9:
  00480ad9  6683f90c              cmp       cx,0Ch
  00480add  7d18                  jge       short 00480AF7h
  00480adf  668b462a              mov       ax,[esi+2Ah]
  00480ae3  bf02140000            mov       edi,1402h
  00480ae8  0fbfd0                movsx     edx,ax
  00480aeb  2bfa                  sub       edi,edx
  00480aed  d1ff                  sar       edi,1
  00480aef  03f8                  add       edi,eax
  00480af1  66897e2a              mov       [esi+2Ah],di
  00480af5  eb1a                  jmp       short 00480B11h

LAB_00480af7:
  00480af7  6683f910              cmp       cx,10h
  00480afb  7d12                  jge       short 00480B0Fh
  00480afd  668b462a              mov       ax,[esi+2Ah]
  00480b01  668bd0                mov       dx,ax
  00480b04  66d1fa                sar       dx,1
  00480b07  2bc2                  sub       eax,edx
  00480b09  6689462a              mov       [esi+2Ah],ax
  00480b0d  eb02                  jmp       short 00480B11h

LAB_00480b0f:
  00480b0f  891e                  mov       [esi],ebx

LAB_00480b11:
  00480b11  6683f90c              cmp       cx,0Ch
  00480b15  7d69                  jge       short 00480B80h
  00480b17  8d44240c              lea       eax,[esp+0Ch]
  00480b1b  50                    push      eax
  00480b1c  56                    push      esi
  00480b1d  e87e41ffff            call      00474CA0h
  00480b22  8bf8                  mov       edi,eax
  00480b24  83c408                add       esp,8
  00480b27  3bfb                  cmp       edi,ebx
  00480b29  7455                  je        short 00480B80h
  00480b2b  8b4674                mov       eax,[esi+74h]
  00480b2e  33c9                  xor       ecx,ecx
  00480b30  8a4e06                mov       cl,[esi+6]
  00480b33  33d2                  xor       edx,edx
  00480b35  8a5607                mov       dl,[esi+7]
  00480b38  53                    push      ebx
  00480b39  51                    push      ecx
  00480b3a  52                    push      edx
  00480b3b  57                    push      edi
  00480b3c  50                    push      eax
  00480b3d  e86e970b00            call      0053A2B0h
  00480b42  0fbf5e32              movsx     ebx,word [esi+32h]
  00480b46  83c414                add       esp,14h
  00480b49  81eb00040000          sub       ebx,400h
  00480b4f  53                    push      ebx
  00480b50  e8c74e0c00            call      00545A1Ch
  00480b55  8b4f50                mov       ecx,[edi+50h]
  00480b58  53                    push      ebx
  00480b59  c1e004                shl       eax,4
  00480b5c  03c8                  add       ecx,eax
  00480b5e  894f50                mov       [edi+50h],ecx
  00480b61  e8b04e0c00            call      00545A16h
  00480b66  8b4f58                mov       ecx,[edi+58h]
  00480b69  66c787960000001000    mov       word [edi+96h],10h
  00480b72  c1e004                shl       eax,4
  00480b75  03c8                  add       ecx,eax
  00480b77  8b07                  mov       eax,[edi]
  00480b79  0c40                  or        al,40h
  00480b7b  894f58                mov       [edi+58h],ecx
  00480b7e  8907                  mov       [edi],eax

LAB_00480b80:
  00480b80  66ff4610              inc       word [esi+10h]
  00480b84  5f                    pop       edi
  00480b85  5e                    pop       esi
  00480b86  5b                    pop       ebx
  00480b87  83c40c                add       esp,0Ch
  00480b8a  c3                    ret

6 cible(s) de saut interne : 0x00480ab2, 0x00480ad9, 0x00480af7, 0x00480b0f, 0x00480b11, 0x00480b80
