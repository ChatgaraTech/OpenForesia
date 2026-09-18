
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00483f30  section .text  offset fichier 0x83330  432 octets

  00483f30  53                    push      ebx
  00483f31  55                    push      ebp
  00483f32  56                    push      esi
  00483f33  8b742410              mov       esi,[esp+10h]
  00483f37  57                    push      edi
  00483f38  8b06                  mov       eax,[esi]
  00483f3a  a900000002            test      eax,2000000h
  00483f3f  740f                  je        short 00483F50h
  00483f41  c70600000000          mov       dword [esi],0
  00483f47  66ff4610              inc       word [esi+10h]
  00483f4b  5f                    pop       edi
  00483f4c  5e                    pop       esi
  00483f4d  5d                    pop       ebp
  00483f4e  5b                    pop       ebx
  00483f4f  c3                    ret

LAB_00483f50:
  00483f50  a802                  test      al,2
  00483f52  0f84ec000000          je        near 00484044h
  00483f58  f6461003              test      byte [esi+10h],3
  00483f5c  0f8571010000          jne       near 004840D3h
  00483f62  8b466c                mov       eax,[esi+6Ch]
  00483f65  8b4e1c                mov       ecx,[esi+1Ch]
  00483f68  8b586c                mov       ebx,[eax+6Ch]
  00483f6b  894674                mov       [esi+74h],eax
  00483f6e  895e78                mov       [esi+78h],ebx
  00483f71  53                    push      ebx
  00483f72  8b7b1c                mov       edi,[ebx+1Ch]
  00483f75  2bf9                  sub       edi,ecx
  00483f77  c1ff0c                sar       edi,0Ch
  00483f7a  e8414c0b00            call      00538BC0h
  00483f7f  8b5620                mov       edx,[esi+20h]
  00483f82  8be8                  mov       ebp,eax
  00483f84  8b4320                mov       eax,[ebx+20h]
  00483f87  8b5b24                mov       ebx,[ebx+24h]
  00483f8a  2bc2                  sub       eax,edx
  00483f8c  8b5624                mov       edx,[esi+24h]
  00483f8f  2bda                  sub       ebx,edx
  00483f91  8bcf                  mov       ecx,edi
  00483f93  c1fb0c                sar       ebx,0Ch
  00483f96  0fafcf                imul      ecx,edi
  00483f99  8bd3                  mov       edx,ebx
  00483f9b  83e5fe                and       ebp,0FFFFFFFEh
  00483f9e  0fafd3                imul      edx,ebx
  00483fa1  83c404                add       esp,4
  00483fa4  03ca                  add       ecx,edx
  00483fa6  c1e50b                shl       ebp,0Bh
  00483fa9  03e8                  add       ebp,eax
  00483fab  51                    push      ecx
  00483fac  c1fd0c                sar       ebp,0Ch
  00483faf  e85c1a0c00            call      00545A10h
  00483fb4  53                    push      ebx
  00483fb5  57                    push      edi
  00483fb6  8944241c              mov       [esp+1Ch],eax
  00483fba  e8631a0c00            call      00545A22h
  00483fbf  8bf8                  mov       edi,eax
  00483fc1  e87a310c00            call      00547140h
  00483fc6  8b4c2414              mov       ecx,[esp+14h]
  00483fca  25ff030000            and       eax,3FFh
  00483fcf  51                    push      ecx
  00483fd0  55                    push      ebp
  00483fd1  8d8407000a0000        lea       eax,[edi+eax+0A00h]
  00483fd8  66894632              mov       [esi+32h],ax
  00483fdc  e8411a0c00            call      00545A22h
  00483fe1  8bf8                  mov       edi,eax
  00483fe3  e858310c00            call      00547140h
  00483fe8  25ff030000            and       eax,3FFh
  00483fed  6a64                  push      64h
  00483fef  2d00020000            sub       eax,200h
  00483ff4  8d4e50                lea       ecx,[esi+50h]
  00483ff7  2bc7                  sub       eax,edi
  00483ff9  66894634              mov       [esi+34h],ax
  00483ffd  0fbfd0                movsx     edx,ax
  00484000  0fbf4632              movsx     eax,word [esi+32h]
  00484004  2d00040000            sub       eax,400h
  00484009  52                    push      edx
  0048400a  50                    push      eax
  0048400b  51                    push      ecx
  0048400c  e8bfea0a00            call      00532AD0h
  00484011  8b15cc975e00          mov       edx,[5E97CCh]
  00484017  83c410                add       esp,10h
  0048401a  8b82d0000000          mov       eax,[edx+0D0h]
  00484020  33d2                  xor       edx,edx
  00484022  6a02                  push      2
  00484024  6a00                  push      0
  00484026  8b08                  mov       ecx,[eax]
  00484028  8a5116                mov       dl,[ecx+16h]
  0048402b  81c2c0000000          add       edx,0C0h
  00484031  52                    push      edx
  00484032  56                    push      esi
  00484033  e8a86affff            call      0047AAE0h
  00484038  83c410                add       esp,10h
  0048403b  66ff4610              inc       word [esi+10h]
  0048403f  5f                    pop       edi
  00484040  5e                    pop       esi
  00484041  5d                    pop       ebp
  00484042  5b                    pop       ebx
  00484043  c3                    ret

LAB_00484044:
  00484044  33c0                  xor       eax,eax
  00484046  66394610              cmp       [esi+10h],ax
  0048404a  0f8583000000          jne       near 004840D3h
  00484050  8b4e64                mov       ecx,[esi+64h]
  00484053  897670                mov       [esi+70h],esi
  00484056  66c746360400          mov       word [esi+36h],4
  0048405c  c7462800000050        mov       dword [esi+28h],50000000h
  00484063  6689462c              mov       [esi+2Ch],ax
  00484067  894638                mov       [esi+38h],eax
  0048406a  6689462e              mov       [esi+2Eh],ax
  0048406e  89465c                mov       [esi+5Ch],eax
  00484071  894e20                mov       [esi+20h],ecx
  00484074  c6460436              mov       byte [esi+4],36h
  00484078  8b15cc975e00          mov       edx,[5E97CCh]
  0048407e  6a02                  push      2
  00484080  50                    push      eax
  00484081  8b82d0000000          mov       eax,[edx+0D0h]
  00484087  33d2                  xor       edx,edx
  00484089  8b08                  mov       ecx,[eax]
  0048408b  8a5115                mov       dl,[ecx+15h]
  0048408e  81c2c0000000          add       edx,0C0h
  00484094  52                    push      edx
  00484095  56                    push      esi
  00484096  e8456affff            call      0047AAE0h
  0048409b  8b7e20                mov       edi,[esi+20h]
  0048409e  83c410                add       esp,10h
  004840a1  8d461c                lea       eax,[esi+1Ch]
  004840a4  81c700001900          add       edi,190000h
  004840aa  50                    push      eax
  004840ab  68a8265500            push      5526A8h
  004840b0  897e20                mov       [esi+20h],edi
  004840b3  c7462800180018        mov       dword [esi+28h],18001800h
  004840ba  66c7462c0018          mov       word [esi+2Ch],1800h
  004840c0  c7463880808000        mov       dword [esi+38h],808080h
  004840c7  c6460435              mov       byte [esi+4],35h
  004840cb  e8109b0b00            call      0053DBE0h
  004840d0  83c408                add       esp,8

LAB_004840d3:
  004840d3  66ff4610              inc       word [esi+10h]
  004840d7  5f                    pop       edi
  004840d8  5e                    pop       esi
  004840d9  5d                    pop       ebp
  004840da  5b                    pop       ebx
  004840db  c3                    ret

3 cible(s) de saut interne : 0x00483f50, 0x00484044, 0x004840d3
