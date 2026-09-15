Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00447a90  section .text  offset fichier 0x46e90  536 octets

  00447a90  83ec0c                sub       esp,0Ch
  00447a93  53                    push      ebx
  00447a94  56                    push      esi
  00447a95  8b742418              mov       esi,[esp+18h]
  00447a99  57                    push      edi
  00447a9a  66c744240c0000        mov       word [esp+0Ch],0
  00447aa1  66c744240eceff        mov       word [esp+0Eh],0FFCEh
  00447aa8  8b06                  mov       eax,[esi]
  00447aaa  66c74424103200        mov       word [esp+10h],32h
  00447ab1  a900000040            test      eax,40000000h
  00447ab6  66c74424125000        mov       word [esp+12h],50h
  00447abd  66c74424140000        mov       word [esp+14h],0
  00447ac4  66c74424166400        mov       word [esp+16h],64h
  00447acb  7407                  je        short 00447AD4h
  00447acd  25ffffff7f            and       eax,7FFFFFFFh
  00447ad2  8906                  mov       [esi],eax

LAB_00447ad4:
  00447ad4  66837e1000            cmp       word [esi+10h],0
  00447ad9  0f85c6000000          jne       near 00447BA5h
  00447adf  6a00                  push      0
  00447ae1  6a00                  push      0
  00447ae3  56                    push      esi
  00447ae4  e8c7010000            call      00447CB0h
  00447ae9  83c40c                add       esp,0Ch
  00447aec  66837e1000            cmp       word [esi+10h],0
  00447af1  0f85ae000000          jne       near 00447BA5h
  00447af7  8d7e1c                lea       edi,[esi+1Ch]
  00447afa  57                    push      edi
  00447afb  6800180000            push      1800h
  00447b00  6840060000            push      640h
  00447b05  6a23                  push      23h
  00447b07  e814600f00            call      0053DB20h
  00447b0c  8b07                  mov       eax,[edi]
  00447b0e  8b4e20                mov       ecx,[esi+20h]
  00447b11  8b5624                mov       edx,[esi+24h]
  00447b14  894650                mov       [esi+50h],eax
  00447b17  668b4632              mov       ax,[esi+32h]
  00447b1b  83c410                add       esp,10h
  00447b1e  66c17e2802            sar       word [esi+28h],2
  00447b23  66d17e2a              sar       word [esi+2Ah],1
  00447b27  66c17e2c02            sar       word [esi+2Ch],2
  00447b2c  66894612              mov       [esi+12h],ax
  00447b30  b0fa                  mov       al,0FAh
  00447b32  66c746100100          mov       word [esi+10h],1
  00447b38  66c746160000          mov       word [esi+16h],0
  00447b3e  894e54                mov       [esi+54h],ecx
  00447b41  895658                mov       [esi+58h],edx
  00447b44  c7465c1e000000        mov       dword [esi+5Ch],1Eh
  00447b4b  66c7869a0000000000    mov       word [esi+9Ah],0
  00447b54  884638                mov       [esi+38h],al
  00447b57  884639                mov       [esi+39h],al
  00447b5a  88463a                mov       [esi+3Ah],al
  00447b5d  bf990a0000            mov       edi,0A99h
  00447b62  bb58020000            mov       ebx,258h

LAB_00447b67:
  00447b67  6aff                  push      0FFFFFFFFh
  00447b69  6a00                  push      0
  00447b6b  68a1000000            push      0A1h
  00447b70  56                    push      esi
  00447b71  e86a2f0300            call      0047AAE0h
  00447b76  66015e28              add       [esi+28h],bx
  00447b7a  66015e2a              add       [esi+2Ah],bx
  00447b7e  66015e2c              add       [esi+2Ch],bx
  00447b82  83c410                add       esp,10h
  00447b85  66897e34              mov       [esi+34h],di
  00447b89  6aec                  push      0FFFFFFECh
  00447b8b  6aec                  push      0FFFFFFECh
  00447b8d  6af6                  push      0FFFFFFF6h
  00447b8f  56                    push      esi
  00447b90  e84bd2fbff            call      00404DE0h
  00447b95  81c799000000          add       edi,99h
  00447b9b  83c410                add       esp,10h
  00447b9e  6681ff9310            cmp       di,1093h
  00447ba3  7ec2                  jle       short 00447B67h

LAB_00447ba5:
  00447ba5  66837e1001            cmp       word [esi+10h],1
  00447baa  0f85d9000000          jne       near 00447C89h
  00447bb0  0fbf4e12              movsx     ecx,word [esi+12h]
  00447bb4  51                    push      ecx
  00447bb5  8d5e1c                lea       ebx,[esi+1Ch]
  00447bb8  e85fde0f00            call      00545A1Ch
  00447bbd  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00447bc4  0fafc2                imul      eax,edx
  00447bc7  8b7e50                mov       edi,[esi+50h]
  00447bca  03c7                  add       eax,edi
  00447bcc  668bbe9a000000        mov       di,[esi+9Ah]
  00447bd3  8903                  mov       [ebx],eax
  00447bd5  0fbf4612              movsx     eax,word [esi+12h]
  00447bd9  50                    push      eax
  00447bda  e837de0f00            call      00545A16h
  00447bdf  0fbfcf                movsx     ecx,di
  00447be2  0fafc1                imul      eax,ecx
  00447be5  8b4e58                mov       ecx,[esi+58h]
  00447be8  668b565c              mov       dx,[esi+5Ch]
  00447bec  03c1                  add       eax,ecx
  00447bee  6603d7                add       dx,di
  00447bf1  56                    push      esi
  00447bf2  894624                mov       [esi+24h],eax
  00447bf5  6689969a000000        mov       [esi+9Ah],dx
  00447bfc  e83fbd0e00            call      00533940h
  00447c01  8b8690000000          mov       eax,[esi+90h]
  00447c07  83c404                add       esp,4
  00447c0a  a902880000            test      eax,8802h
  00447c0f  751f                  jne       short 00447C30h
  00447c11  8b4660                mov       eax,[esi+60h]
  00447c14  8b4e20                mov       ecx,[esi+20h]
  00447c17  8d9000e0fcff          lea       edx,[eax-32000h]
  00447c1d  3bca                  cmp       ecx,edx
  00447c1f  7c0f                  jl        short 00447C30h
  00447c21  8d9000200300          lea       edx,[eax+32000h]
  00447c27  3bca                  cmp       ecx,edx
  00447c29  7f05                  jg        short 00447C30h
  00447c2b  894620                mov       [esi+20h],eax
  00447c2e  eb0c                  jmp       short 00447C3Ch

LAB_00447c30:
  00447c30  66c746100200          mov       word [esi+10h],2
  00447c36  66c746120500          mov       word [esi+12h],5

LAB_00447c3c:
  00447c3c  8b4e6c                mov       ecx,[esi+6Ch]
  00447c3f  8d44240c              lea       eax,[esp+0Ch]
  00447c43  6a00                  push      0
  00447c45  50                    push      eax
  00447c46  53                    push      ebx
  00447c47  51                    push      ecx
  00447c48  e8e31f0d00            call      00519C30h
  00447c4d  8bf8                  mov       edi,eax
  00447c4f  83c410                add       esp,10h
  00447c52  85ff                  test      edi,edi
  00447c54  741c                  je        short 00447C72h
  00447c56  8b566c                mov       edx,[esi+6Ch]
  00447c59  57                    push      edi
  00447c5a  52                    push      edx
  00447c5b  e8201f0f00            call      00539B80h
  00447c60  8b07                  mov       eax,[edi]
  00447c62  83c408                add       esp,8
  00447c65  0c40                  or        al,40h
  00447c67  66c787960000001000    mov       word [edi+96h],10h
  00447c70  8907                  mov       [edi],eax

LAB_00447c72:
  00447c72  66ff4616              inc       word [esi+16h]
  00447c76  66837e163c            cmp       word [esi+16h],3Ch
  00447c7b  7c0c                  jl        short 00447C89h
  00447c7d  66c746100200          mov       word [esi+10h],2
  00447c83  66c746120500          mov       word [esi+12h],5

LAB_00447c89:
  00447c89  66837e1002            cmp       word [esi+10h],2
  00447c8e  7511                  jne       short 00447CA1h
  00447c90  66ff4e12              dec       word [esi+12h]
  00447c94  66837e1200            cmp       word [esi+12h],0
  00447c99  7f06                  jg        short 00447CA1h
  00447c9b  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00447ca1:
  00447ca1  5f                    pop       edi
  00447ca2  5e                    pop       esi
  00447ca3  5b                    pop       ebx
  00447ca4  83c40c                add       esp,0Ch
  00447ca7  c3                    ret

8 cible(s) de saut interne : 0x00447ad4, 0x00447b67, 0x00447ba5, 0x00447c30, 0x00447c3c, 0x00447c72, 0x00447c89, 0x00447ca1
