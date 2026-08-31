Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004487f0  section .text  offset fichier 0x47bf0  1104 octets

  004487f0  53                    push      ebx
  004487f1  55                    push      ebp
  004487f2  56                    push      esi
  004487f3  8b742410              mov       esi,[esp+10h]
  004487f7  33db                  xor       ebx,ebx
  004487f9  57                    push      edi
  004487fa  8b466c                mov       eax,[esi+6Ch]
  004487fd  3bc3                  cmp       eax,ebx
  004487ff  7505                  jne       short 00448806h
  00448801  b858e55d00            mov       eax,5DE558h

LAB_00448806:
  00448806  8b481c                mov       ecx,[eax+1Ch]
  00448809  bd01000000            mov       ebp,1
  0044880e  894e50                mov       [esi+50h],ecx
  00448811  8b5020                mov       edx,[eax+20h]
  00448814  81c200c00300          add       edx,3C000h
  0044881a  66395e10              cmp       [esi+10h],bx
  0044881e  895654                mov       [esi+54h],edx
  00448821  8b4024                mov       eax,[eax+24h]
  00448824  894658                mov       [esi+58h],eax
  00448827  7548                  jne       short 00448871h
  00448829  8a8e9a000000          mov       cl,[esi+9Ah]
  0044882f  8a565c                mov       dl,[esi+5Ch]
  00448832  b870170000            mov       eax,1770h
  00448837  66896e10              mov       [esi+10h],bp
  0044883b  66894628              mov       [esi+28h],ax
  0044883f  6689462a              mov       [esi+2Ah],ax
  00448843  6689462c              mov       [esi+2Ch],ax
  00448847  668b4614              mov       ax,[esi+14h]
  0044884b  25ff0f0000            and       eax,0FFFh
  00448850  884e04                mov       [esi+4],cl
  00448853  88560a                mov       [esi+0Ah],dl
  00448856  885e0b                mov       [esi+0Bh],bl
  00448859  c7465c00803e00        mov       dword [esi+5Ch],3E8000h
  00448860  66899e9a000000        mov       [esi+9Ah],bx
  00448867  894644                mov       [esi+44h],eax
  0044886a  66899e98000000        mov       [esi+98h],bx

LAB_00448871:
  00448871  66399e98000000        cmp       [esi+98h],bx
  00448878  7520                  jne       short 0044889Ah
  0044887a  66837e1003            cmp       word [esi+10h],3
  0044887f  7d19                  jge       short 0044889Ah
  00448881  6800130000            push      1300h
  00448886  6800130000            push      1300h
  0044888b  68a9050000            push      5A9h
  00448890  6a0d                  push      0Dh
  00448892  e809460f00            call      0053CEA0h
  00448897  83c410                add       esp,10h

LAB_0044889a:
  0044889a  66ff8698000000        inc       word [esi+98h]
  004488a1  66837e1063            cmp       word [esi+10h],63h
  004488a6  0f8484000000          je        near 00448930h
  004488ac  0fbf4e16              movsx     ecx,word [esi+16h]
  004488b0  51                    push      ecx
  004488b1  e860d10f00            call      00545A16h
  004488b6  8b565c                mov       edx,[esi+5Ch]
  004488b9  8bf8                  mov       edi,eax
  004488bb  c1fa0c                sar       edx,0Ch
  004488be  0fbf4614              movsx     eax,word [esi+14h]
  004488c2  0faffa                imul      edi,edx
  004488c5  50                    push      eax
  004488c6  c1ff0c                sar       edi,0Ch
  004488c9  e84ed10f00            call      00545A1Ch
  004488ce  0faff8                imul      edi,eax
  004488d1  0fbf4e16              movsx     ecx,word [esi+16h]
  004488d5  8b4650                mov       eax,[esi+50h]
  004488d8  51                    push      ecx
  004488d9  03f8                  add       edi,eax
  004488db  897e1c                mov       [esi+1Ch],edi
  004488de  e833d10f00            call      00545A16h
  004488e3  8b565c                mov       edx,[esi+5Ch]
  004488e6  8bf8                  mov       edi,eax
  004488e8  c1fa0c                sar       edx,0Ch
  004488eb  0fbf4614              movsx     eax,word [esi+14h]
  004488ef  0faffa                imul      edi,edx
  004488f2  50                    push      eax
  004488f3  c1ff0c                sar       edi,0Ch
  004488f6  e81bd10f00            call      00545A16h
  004488fb  0faff8                imul      edi,eax
  004488fe  8b4658                mov       eax,[esi+58h]
  00448901  0fbf4e16              movsx     ecx,word [esi+16h]
  00448905  03f8                  add       edi,eax
  00448907  51                    push      ecx
  00448908  897e24                mov       [esi+24h],edi
  0044890b  8b7e5c                mov       edi,[esi+5Ch]
  0044890e  e809d10f00            call      00545A1Ch
  00448913  8bd7                  mov       edx,edi
  00448915  c1fa0c                sar       edx,0Ch
  00448918  0fafc2                imul      eax,edx
  0044891b  034654                add       eax,[esi+54h]
  0044891e  894620                mov       [esi+20h],eax
  00448921  b800000500            mov       eax,50000h
  00448926  2bc7                  sub       eax,edi
  00448928  c1f804                sar       eax,4
  0044892b  03c7                  add       eax,edi
  0044892d  89465c                mov       [esi+5Ch],eax

LAB_00448930:
  00448930  66396e10              cmp       [esi+10h],bp
  00448934  0f85bb000000          jne       near 004489F5h
  0044893a  8b465c                mov       eax,[esi+5Ch]
  0044893d  b900000500            mov       ecx,50000h
  00448942  668b5632              mov       dx,[esi+32h]
  00448946  2bc8                  sub       ecx,eax
  00448948  c1f904                sar       ecx,4
  0044894b  6681c2c800            add       dx,0C8h
  00448950  8d3c01                lea       edi,[ecx+eax]
  00448953  668b4634              mov       ax,[esi+34h]
  00448957  66056400              add       ax,64h
  0044895b  81e2ff0f0000          and       edx,0FFFh
  00448961  25ff0f0000            and       eax,0FFFh
  00448966  66ff869a000000        inc       word [esi+9Ah]
  0044896d  6683be9a00000004      cmp       word [esi+9Ah],4
  00448975  897e5c                mov       [esi+5Ch],edi
  00448978  66895632              mov       [esi+32h],dx
  0044897c  66894634              mov       [esi+34h],ax
  00448980  7c39                  jl        short 004489BBh
  00448982  8a460a                mov       al,[esi+0Ah]
  00448985  66899e9a000000        mov       [esi+9Ah],bx
  0044898c  3c0e                  cmp       al,0Eh
  0044898e  7507                  jne       short 00448997h
  00448990  c7465c09000000        mov       dword [esi+5Ch],9

LAB_00448997:
  00448997  3c0d                  cmp       al,0Dh
  00448999  7507                  jne       short 004489A2h
  0044899b  c7465c02000000        mov       dword [esi+5Ch],2

LAB_004489a2:
  004489a2  6aff                  push      0FFFFFFFFh
  004489a4  53                    push      ebx
  004489a5  6897000000            push      97h
  004489aa  8d4e1c                lea       ecx,[esi+1Ch]
  004489ad  53                    push      ebx
  004489ae  51                    push      ecx
  004489af  56                    push      esi
  004489b0  e88b220300            call      0047AC40h
  004489b5  83c418                add       esp,18h
  004489b8  897e5c                mov       [esi+5Ch],edi

LAB_004489bb:
  004489bb  668b4616              mov       ax,[esi+16h]
  004489bf  668b5614              mov       dx,[esi+14h]
  004489c3  0fbfc8                movsx     ecx,ax
  004489c6  f7d9                  neg       ecx
  004489c8  c1f905                sar       ecx,5
  004489cb  6683c264              add       dx,64h
  004489cf  03c8                  add       ecx,eax
  004489d1  8b465c                mov       eax,[esi+5Ch]
  004489d4  81e2ff0f0000          and       edx,0FFFh
  004489da  81e1ff0f0000          and       ecx,0FFFh
  004489e0  3d00500500            cmp       eax,55000h
  004489e5  66895614              mov       [esi+14h],dx
  004489e9  66894e16              mov       [esi+16h],cx
  004489ed  7d06                  jge       short 004489F5h
  004489ef  66c746100200          mov       word [esi+10h],2

LAB_004489f5:
  004489f5  66837e1002            cmp       word [esi+10h],2
  004489fa  7574                  jne       short 00448A70h
  004489fc  668b4614              mov       ax,[esi+14h]
  00448a00  8b4e44                mov       ecx,[esi+44h]
  00448a03  0fbfd0                movsx     edx,ax
  00448a06  2bca                  sub       ecx,edx
  00448a08  c1f904                sar       ecx,4
  00448a0b  03c8                  add       ecx,eax
  00448a0d  668b4616              mov       ax,[esi+16h]
  00448a11  0fbfd0                movsx     edx,ax
  00448a14  f7da                  neg       edx
  00448a16  c1fa04                sar       edx,4
  00448a19  03d0                  add       edx,eax
  00448a1b  8b4648                mov       eax,[esi+48h]
  00448a1e  668b4632              mov       ax,[esi+32h]
  00448a22  66894e14              mov       [esi+14h],cx
  00448a26  0fbfc8                movsx     ecx,ax
  00448a29  66895616              mov       [esi+16h],dx
  00448a2d  8b5644                mov       edx,[esi+44h]
  00448a30  2bd1                  sub       edx,ecx
  00448a32  668b4e34              mov       cx,[esi+34h]
  00448a36  c1fa03                sar       edx,3
  00448a39  03c2                  add       eax,edx
  00448a3b  66894632              mov       [esi+32h],ax
  00448a3f  25ff0f0000            and       eax,0FFFh
  00448a44  66894632              mov       [esi+32h],ax
  00448a48  b800100000            mov       eax,1000h
  00448a4d  2bc1                  sub       eax,ecx
  00448a4f  c1f803                sar       eax,3
  00448a52  03c1                  add       eax,ecx
  00448a54  25ff0f0000            and       eax,0FFFh
  00448a59  663d3c0f              cmp       ax,0F3Ch
  00448a5d  66894634              mov       [esi+34h],ax
  00448a61  7e0d                  jle       short 00448A70h
  00448a63  66c746100300          mov       word [esi+10h],3
  00448a69  66899e98000000        mov       [esi+98h],bx

LAB_00448a70:
  00448a70  66837e1003            cmp       word [esi+10h],3
  00448a75  0f8597000000          jne       near 00448B12h
  00448a7b  6639ae98000000        cmp       [esi+98h],bp
  00448a82  7519                  jne       short 00448A9Dh
  00448a84  6800130000            push      1300h
  00448a89  6800130000            push      1300h
  00448a8e  68fd060000            push      6FDh
  00448a93  6a0a                  push      0Ah
  00448a95  e806440f00            call      0053CEA0h
  00448a9a  83c410                add       esp,10h

LAB_00448a9d:
  00448a9d  6683be9800000006      cmp       word [esi+98h],6
  00448aa5  7519                  jne       short 00448AC0h
  00448aa7  6800130000            push      1300h
  00448aac  6800130000            push      1300h
  00448ab1  6800010000            push      100h
  00448ab6  6a0f                  push      0Fh
  00448ab8  e8e3430f00            call      0053CEA0h
  00448abd  83c410                add       esp,10h

LAB_00448ac0:
  00448ac0  807e0419              cmp       byte [esi+4],19h
  00448ac4  752b                  jne       short 00448AF1h
  00448ac6  668b4634              mov       ax,[esi+34h]
  00448aca  66052c01              add       ax,12Ch
  00448ace  25ff0f0000            and       eax,0FFFh
  00448ad3  663d4006              cmp       ax,640h
  00448ad7  66894634              mov       [esi+34h],ax
  00448adb  7e35                  jle       short 00448B12h
  00448add  663dd007              cmp       ax,7D0h
  00448ae1  7d2f                  jge       short 00448B12h
  00448ae3  66c746344006          mov       word [esi+34h],640h
  00448ae9  66c746106300          mov       word [esi+10h],63h
  00448aef  eb21                  jmp       short 00448B12h

LAB_00448af1:
  00448af1  6683463428            add       word [esi+34h],28h
  00448af6  668b4634              mov       ax,[esi+34h]
  00448afa  663d3011              cmp       ax,1130h
  00448afe  7c12                  jl        short 00448B12h
  00448b00  25ff0f0000            and       eax,0FFFh
  00448b05  66c746106300          mov       word [esi+10h],63h
  00448b0b  66894634              mov       [esi+34h],ax
  00448b0f  895e48                mov       [esi+48h],ebx

LAB_00448b12:
  00448b12  66837e1063            cmp       word [esi+10h],63h
  00448b17  0f8500010000          jne       near 00448C1Dh
  00448b1d  0fbf4616              movsx     eax,word [esi+16h]
  00448b21  50                    push      eax
  00448b22  8d7e1c                lea       edi,[esi+1Ch]
  00448b25  e8ecce0f00            call      00545A16h
  00448b2a  8b4e5c                mov       ecx,[esi+5Ch]
  00448b2d  8be8                  mov       ebp,eax
  00448b2f  c1f90c                sar       ecx,0Ch
  00448b32  0fbf5614              movsx     edx,word [esi+14h]
  00448b36  0fafe9                imul      ebp,ecx
  00448b39  52                    push      edx
  00448b3a  c1fd0c                sar       ebp,0Ch
  00448b3d  e8dace0f00            call      00545A1Ch
  00448b42  0fafe8                imul      ebp,eax
  00448b45  036e50                add       ebp,[esi+50h]
  00448b48  0fbf4616              movsx     eax,word [esi+16h]
  00448b4c  50                    push      eax
  00448b4d  892f                  mov       [edi],ebp
  00448b4f  e8c2ce0f00            call      00545A16h
  00448b54  8b4e5c                mov       ecx,[esi+5Ch]
  00448b57  8be8                  mov       ebp,eax
  00448b59  c1f90c                sar       ecx,0Ch
  00448b5c  0fbf5614              movsx     edx,word [esi+14h]
  00448b60  0fafe9                imul      ebp,ecx
  00448b63  52                    push      edx
  00448b64  c1fd0c                sar       ebp,0Ch
  00448b67  e8aace0f00            call      00545A16h
  00448b6c  0fafe8                imul      ebp,eax
  00448b6f  8b5658                mov       edx,[esi+58h]
  00448b72  0fbf4616              movsx     eax,word [esi+16h]
  00448b76  03ea                  add       ebp,edx
  00448b78  50                    push      eax
  00448b79  896e24                mov       [esi+24h],ebp
  00448b7c  8b6e5c                mov       ebp,[esi+5Ch]
  00448b7f  e898ce0f00            call      00545A1Ch
  00448b84  8bcd                  mov       ecx,ebp
  00448b86  8b5654                mov       edx,[esi+54h]
  00448b89  c1f90c                sar       ecx,0Ch
  00448b8c  0fafc1                imul      eax,ecx
  00448b8f  034648                add       eax,[esi+48h]
  00448b92  03c2                  add       eax,edx
  00448b94  66ff869a000000        inc       word [esi+9Ah]
  00448b9b  6683be9a00000004      cmp       word [esi+9Ah],4
  00448ba3  894620                mov       [esi+20h],eax
  00448ba6  7c36                  jl        short 00448BDEh
  00448ba8  8a460a                mov       al,[esi+0Ah]
  00448bab  66899e9a000000        mov       [esi+9Ah],bx
  00448bb2  3c0e                  cmp       al,0Eh
  00448bb4  7507                  jne       short 00448BBDh
  00448bb6  c7465c09000000        mov       dword [esi+5Ch],9

LAB_00448bbd:
  00448bbd  3c0d                  cmp       al,0Dh
  00448bbf  7507                  jne       short 00448BC8h
  00448bc1  c7465c02000000        mov       dword [esi+5Ch],2

LAB_00448bc8:
  00448bc8  6aff                  push      0FFFFFFFFh
  00448bca  53                    push      ebx
  00448bcb  6897000000            push      97h
  00448bd0  53                    push      ebx
  00448bd1  57                    push      edi
  00448bd2  56                    push      esi
  00448bd3  e868200300            call      0047AC40h
  00448bd8  83c418                add       esp,18h
  00448bdb  896e5c                mov       [esi+5Ch],ebp

LAB_00448bde:
  00448bde  807e4364              cmp       byte [esi+43h],64h
  00448be2  7639                  jbe       short 00448C1Dh
  00448be4  8b5648                mov       edx,[esi+48h]
  00448be7  66814632c800          add       word [esi+32h],0C8h
  00448bed  b938ffffff            mov       ecx,0FFFFFF38h
  00448bf2  81c200f00000          add       edx,0F000h
  00448bf8  66014e28              add       [esi+28h],cx
  00448bfc  668b4628              mov       ax,[esi+28h]
  00448c00  66014e2c              add       [esi+2Ch],cx
  00448c04  663bc3                cmp       ax,bx
  00448c07  895648                mov       [esi+48h],edx
  00448c0a  7d11                  jge       short 00448C1Dh
  00448c0c  8b06                  mov       eax,[esi]
  00448c0e  66895e28              mov       [esi+28h],bx
  00448c12  25ffffff7f            and       eax,7FFFFFFFh
  00448c17  66895e2a              mov       [esi+2Ah],bx
  00448c1b  8906                  mov       [esi],eax

LAB_00448c1d:
  00448c1d  8a5e43                mov       bl,[esi+43h]
  00448c20  fec3                  inc       bl
  00448c22  8ac3                  mov       al,bl
  00448c24  885e43                mov       [esi+43h],bl
  00448c27  25ff000000            and       eax,0FFh
  00448c2c  3d88130000            cmp       eax,1388h
  00448c31  7c06                  jl        short 00448C39h
  00448c33  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00448c39:
  00448c39  5f                    pop       edi
  00448c3a  5e                    pop       esi
  00448c3b  5d                    pop       ebp
  00448c3c  5b                    pop       ebx
  00448c3d  c3                    ret
  00448c3e  90                    nop
  00448c3f  90                    nop

18 cible(s) de saut interne : 0x00448806, 0x00448871, 0x0044889a, 0x00448930, 0x00448997, 0x004489a2, 0x004489bb, 0x004489f5, 0x00448a70, 0x00448a9d, 0x00448ac0, 0x00448af1, 0x00448b12, 0x00448bbd, 0x00448bc8, 0x00448bde, 0x00448c1d, 0x00448c39
