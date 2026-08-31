Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004819d0  section .text  offset fichier 0x80dd0  883 octets

  004819d0  83ec0c                sub       esp,0Ch
  004819d3  53                    push      ebx
  004819d4  55                    push      ebp
  004819d5  56                    push      esi
  004819d6  8b74241c              mov       esi,[esp+1Ch]
  004819da  33ed                  xor       ebp,ebp
  004819dc  57                    push      edi
  004819dd  8b06                  mov       eax,[esi]
  004819df  66896c2410            mov       [esp+10h],bp
  004819e4  a900000002            test      eax,2000000h
  004819e9  66c744241200ff        mov       word [esp+12h],0FF00h
  004819f0  66c74424140001        mov       word [esp+14h],100h
  004819f7  66c74424169001        mov       word [esp+16h],190h
  004819fe  66896c2418            mov       [esp+18h],bp
  00481a03  66c744241a0002        mov       word [esp+1Ah],200h
  00481a0a  741b                  je        short 00481A27h
  00481a0c  8b4674                mov       eax,[esi+74h]
  00481a0f  8b08                  mov       ecx,[eax]
  00481a11  80e17f                and       cl,7Fh
  00481a14  8908                  mov       [eax],ecx
  00481a16  66892d66915e00        mov       [5E9166h],bp
  00481a1d  892e                  mov       [esi],ebp
  00481a1f  5f                    pop       edi
  00481a20  5e                    pop       esi
  00481a21  5d                    pop       ebp
  00481a22  5b                    pop       ebx
  00481a23  83c40c                add       esp,0Ch
  00481a26  c3                    ret

LAB_00481a27:
  00481a27  66396e10              cmp       [esi+10h],bp
  00481a2b  0f8528010000          jne       near 00481B59h
  00481a31  66c746100100          mov       word [esi+10h],1
  00481a37  a0a0076000            mov       al,[6007A0h]
  00481a3c  3c6c                  cmp       al,6Ch
  00481a3e  760a                  jbe       short 00481A4Ah
  00481a40  892e                  mov       [esi],ebp
  00481a42  5f                    pop       edi
  00481a43  5e                    pop       esi
  00481a44  5d                    pop       ebp
  00481a45  5b                    pop       ebx
  00481a46  83c40c                add       esp,0Ch
  00481a49  c3                    ret

LAB_00481a4a:
  00481a4a  8b4674                mov       eax,[esi+74h]
  00481a4d  6a0b                  push      0Bh
  00481a4f  55                    push      ebp
  00481a50  bf00400000            mov       edi,4000h
  00481a55  8b18                  mov       ebx,[eax]
  00481a57  6a3e                  push      3Eh
  00481a59  80cb80                or        bl,80h
  00481a5c  56                    push      esi
  00481a5d  8918                  mov       [eax],ebx
  00481a5f  668b4636              mov       ax,[esi+36h]
  00481a63  8b4e60                mov       ecx,[esi+60h]
  00481a66  24fe                  and       al,0FEh
  00481a68  0c06                  or        al,6
  00481a6a  896e78                mov       [esi+78h],ebp
  00481a6d  66894636              mov       [esi+36h],ax
  00481a71  66c7462eddff          mov       word [esi+2Eh],0FFDDh
  00481a77  894e20                mov       [esi+20h],ecx
  00481a7a  c7463800000040        mov       dword [esi+38h],40000000h
  00481a81  c7464408080800        mov       dword [esi+44h],80808h
  00481a88  c7464880808000        mov       dword [esi+48h],808080h
  00481a8f  896e28                mov       [esi+28h],ebp
  00481a92  66896e2c              mov       [esi+2Ch],bp
  00481a96  66897e12              mov       [esi+12h],di
  00481a9a  66897e14              mov       [esi+14h],di
  00481a9e  66896e16              mov       [esi+16h],bp
  00481aa2  c6460429              mov       byte [esi+4],29h
  00481aa6  e83590ffff            call      0047AAE0h
  00481aab  83c410                add       esp,10h
  00481aae  66c7462ee2ff          mov       word [esi+2Eh],0FFE2h
  00481ab4  c7463820202080        mov       dword [esi+38h],80202020h
  00481abb  c7464404040400        mov       dword [esi+44h],40404h
  00481ac2  6a03                  push      3
  00481ac4  6a01                  push      1
  00481ac6  6a3e                  push      3Eh
  00481ac8  56                    push      esi
  00481ac9  c7464840404000        mov       dword [esi+48h],404040h
  00481ad0  e80b90ffff            call      0047AAE0h
  00481ad5  83c410                add       esp,10h
  00481ad8  b800500000            mov       eax,5000h
  00481add  66c7462ed8ff          mov       word [esi+2Eh],0FFD8h
  00481ae3  896e38                mov       [esi+38h],ebp
  00481ae6  6a02                  push      2
  00481ae8  6a01                  push      1
  00481aea  6a3e                  push      3Eh
  00481aec  56                    push      esi
  00481aed  c7464408000000        mov       dword [esi+44h],8
  00481af4  c7464840000000        mov       dword [esi+48h],40h
  00481afb  896e28                mov       [esi+28h],ebp
  00481afe  66896e2c              mov       [esi+2Ch],bp
  00481b02  66894612              mov       [esi+12h],ax
  00481b06  66894614              mov       [esi+14h],ax
  00481b0a  66c746162000          mov       word [esi+16h],20h
  00481b10  c6460422              mov       byte [esi+4],22h
  00481b14  e8c78fffff            call      0047AAE0h
  00481b19  83c410                add       esp,10h
  00481b1c  896e28                mov       [esi+28h],ebp
  00481b1f  66896e2c              mov       [esi+2Ch],bp
  00481b23  66c746120060          mov       word [esi+12h],6000h
  00481b29  6a02                  push      2
  00481b2b  6a01                  push      1
  00481b2d  6a3e                  push      3Eh
  00481b2f  56                    push      esi
  00481b30  66897e14              mov       [esi+14h],di
  00481b34  66c74616e0ff          mov       word [esi+16h],0FFE0h
  00481b3a  e8a18fffff            call      0047AAE0h
  00481b3f  83c410                add       esp,10h
  00481b42  8d561c                lea       edx,[esi+1Ch]
  00481b45  52                    push      edx
  00481b46  68c0255500            push      5525C0h
  00481b4b  e890c00b00            call      0053DBE0h
  00481b50  83c408                add       esp,8
  00481b53  66c746160400          mov       word [esi+16h],4

LAB_00481b59:
  00481b59  8b06                  mov       eax,[esi]
  00481b5b  a802                  test      al,2
  00481b5d  0f84ce010000          je        near 00481D31h
  00481b63  c70560915e00a0008000  mov       dword [5E9160h],8000A0h
  00481b6d  c70564915e0080000200  mov       dword [5E9164h],20080h
  00481b77  66837e1001            cmp       word [esi+10h],1
  00481b7c  0f85be000000          jne       near 00481C40h
  00481b82  6a0d                  push      0Dh
  00481b84  55                    push      ebp
  00481b85  6a41                  push      41h
  00481b87  56                    push      esi
  00481b88  896e38                mov       [esi+38h],ebp
  00481b8b  c746280000ff7f        mov       dword [esi+28h],7FFF0000h
  00481b92  66896e2c              mov       [esi+2Ch],bp
  00481b96  c6460436              mov       byte [esi+4],36h
  00481b9a  e8418fffff            call      0047AAE0h
  00481b9f  83c410                add       esp,10h
  00481ba2  6a0a                  push      0Ah
  00481ba4  68a0076000            push      6007A0h
  00481ba9  e8e234ffff            call      00475090h
  00481bae  806636fb              and       byte [esi+36h],0FBh
  00481bb2  b800040000            mov       eax,400h
  00481bb7  66894616              mov       [esi+16h],ax
  00481bbb  66894632              mov       [esi+32h],ax
  00481bbf  66894634              mov       [esi+34h],ax
  00481bc3  33c0                  xor       eax,eax
  00481bc5  66896e14              mov       [esi+14h],bp
  00481bc9  c74638f0f0f002        mov       dword [esi+38h],2F0F0F0h
  00481bd0  c7462800200020        mov       dword [esi+28h],20002000h
  00481bd7  66c7462c0020          mov       word [esi+2Ch],2000h
  00481bdd  c6460427              mov       byte [esi+4],27h
  00481be1  a0a0076000            mov       al,[6007A0h]
  00481be6  83c408                add       esp,8
  00481be9  660fb688a2076000      movzx     cx,byte [eax+6007A2h]
  00481bf1  6a0b                  push      0Bh
  00481bf3  55                    push      ebp
  00481bf4  6a3f                  push      3Fh
  00481bf6  56                    push      esi
  00481bf7  66894e16              mov       [esi+16h],cx
  00481bfb  e8e08effff            call      0047AAE0h
  00481c00  83c410                add       esp,10h
  00481c03  c6460428              mov       byte [esi+4],28h
  00481c07  bb07000000            mov       ebx,7

LAB_00481c0c:
  00481c0c  83fb01                cmp       ebx,1
  00481c0f  7504                  jne       short 00481C15h
  00481c11  c6460413              mov       byte [esi+4],13h

LAB_00481c15:
  00481c15  33d2                  xor       edx,edx
  00481c17  33c0                  xor       eax,eax
  00481c19  8a15a0076000          mov       dl,[6007A0h]
  00481c1f  6a0b                  push      0Bh
  00481c21  55                    push      ebp
  00481c22  6a40                  push      40h
  00481c24  8a82a2076000          mov       al,[edx+6007A2h]
  00481c2a  56                    push      esi
  00481c2b  8bf8                  mov       edi,eax
  00481c2d  e8ae8effff            call      0047AAE0h
  00481c32  83c410                add       esp,10h
  00481c35  4b                    dec       ebx
  00481c36  66897e16              mov       [esi+16h],di
  00481c3a  75d0                  jne       short 00481C0Ch
  00481c3c  66896e34              mov       [esi+34h],bp

LAB_00481c40:
  00481c40  8b06                  mov       eax,[esi]
  00481c42  a900008000            test      eax,800000h
  00481c47  7477                  je        short 00481CC0h
  00481c49  8b4e74                mov       ecx,[esi+74h]
  00481c4c  8b3db4876000          mov       edi,[6087B4h]
  00481c52  33d2                  xor       edx,edx
  00481c54  8a5108                mov       dl,[ecx+8]
  00481c57  8b1f                  mov       ebx,[edi]
  00481c59  8d4a78                lea       ecx,[edx+78h]
  00481c5c  8d3cd2                lea       edi,[edx+edx*8]
  00481c5f  c1e10d                shl       ecx,0Dh
  00481c62  03cb                  add       ecx,ebx
  00481c64  8d147a                lea       edx,[edx+edi*2]
  00481c67  33db                  xor       ebx,ebx
  00481c69  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  00481c70  c1e304                shl       ebx,4
  00481c73  668b93204a6000        mov       dx,[ebx+604A20h]
  00481c7a  668591c41f0000        test      [ecx+1FC4h],dx
  00481c81  7507                  jne       short 00481C8Ah
  00481c83  0d00000002            or        eax,2000000h
  00481c88  8906                  mov       [esi],eax

LAB_00481c8a:
  00481c8a  a1f02c5f00            mov       eax,[5F2CF0h]
  00481c8f  33d2                  xor       edx,edx
  00481c91  bf05000000            mov       edi,5
  00481c96  f7f7                  div       edi
  00481c98  85d2                  test      edx,edx
  00481c9a  7533                  jne       short 00481CCFh
  00481c9c  668b814e010000        mov       ax,[ecx+14Eh]
  00481ca3  663d0200              cmp       ax,2
  00481ca7  7c0c                  jl        short 00481CB5h
  00481ca9  83c0fe                add       eax,0FFFFFFFEh
  00481cac  6689814e010000        mov       [ecx+14Eh],ax
  00481cb3  eb1a                  jmp       short 00481CCFh

LAB_00481cb5:
  00481cb5  6689a94e010000        mov       [ecx+14Eh],bp
  00481cbc  8b06                  mov       eax,[esi]
  00481cbe  eb08                  jmp       short 00481CC8h

LAB_00481cc0:
  00481cc0  66817e102c01          cmp       word [esi+10h],12Ch
  00481cc6  7c07                  jl        short 00481CCFh

LAB_00481cc8:
  00481cc8  0d00000002            or        eax,2000000h
  00481ccd  8906                  mov       [esi],eax

LAB_00481ccf:
  00481ccf  56                    push      esi
  00481cd0  e83b34ffff            call      00475110h
  00481cd5  83c404                add       esp,4
  00481cd8  85c0                  test      eax,eax
  00481cda  7406                  je        short 00481CE2h
  00481cdc  810e00000002          or        dword [esi],2000000h

LAB_00481ce2:
  00481ce2  668146329001          add       word [esi+32h],190h
  00481ce8  8d442410              lea       eax,[esp+10h]
  00481cec  50                    push      eax
  00481ced  56                    push      esi
  00481cee  e8ad2fffff            call      00474CA0h
  00481cf3  8bf8                  mov       edi,eax
  00481cf5  83c408                add       esp,8
  00481cf8  3bfd                  cmp       edi,ebp
  00481cfa  7429                  je        short 00481D25h
  00481cfc  8b4674                mov       eax,[esi+74h]
  00481cff  33c9                  xor       ecx,ecx
  00481d01  8a4e06                mov       cl,[esi+6]
  00481d04  33d2                  xor       edx,edx
  00481d06  8a5607                mov       dl,[esi+7]
  00481d09  55                    push      ebp
  00481d0a  51                    push      ecx
  00481d0b  52                    push      edx
  00481d0c  57                    push      edi
  00481d0d  50                    push      eax
  00481d0e  e89d850b00            call      0053A2B0h
  00481d13  8b07                  mov       eax,[edi]
  00481d15  83c414                add       esp,14h
  00481d18  0c40                  or        al,40h
  00481d1a  66c787960000001000    mov       word [edi+96h],10h
  00481d23  8907                  mov       [edi],eax

LAB_00481d25:
  00481d25  66ff4610              inc       word [esi+10h]
  00481d29  5f                    pop       edi
  00481d2a  5e                    pop       esi
  00481d2b  5d                    pop       ebp
  00481d2c  5b                    pop       ebx
  00481d2d  83c40c                add       esp,0Ch
  00481d30  c3                    ret

LAB_00481d31:
  00481d31  66396e16              cmp       [esi+16h],bp
  00481d35  7504                  jne       short 00481D3Bh
  00481d37  0c02                  or        al,2
  00481d39  8906                  mov       [esi],eax

LAB_00481d3b:
  00481d3b  5f                    pop       edi
  00481d3c  5e                    pop       esi
  00481d3d  5d                    pop       ebp
  00481d3e  5b                    pop       ebx
  00481d3f  83c40c                add       esp,0Ch
  00481d42  c3                    ret

15 cible(s) de saut interne : 0x00481a27, 0x00481a4a, 0x00481b59, 0x00481c0c, 0x00481c15, 0x00481c40, 0x00481c8a, 0x00481cb5, 0x00481cc0, 0x00481cc8, 0x00481ccf, 0x00481ce2, 0x00481d25, 0x00481d31, 0x00481d3b
