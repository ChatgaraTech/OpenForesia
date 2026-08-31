
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047db50  section .text  offset fichier 0x7cf50  698 octets

  0047db50  83ec0c                sub       esp,0Ch
  0047db53  56                    push      esi
  0047db54  8b742414              mov       esi,[esp+14h]
  0047db58  f70600000002          test      dword [esi],2000000h
  0047db5e  740b                  je        short 0047DB6Bh
  0047db60  c70600000000          mov       dword [esi],0
  0047db66  5e                    pop       esi
  0047db67  83c40c                add       esp,0Ch
  0047db6a  c3                    ret

LAB_0047db6b:
  0047db6b  57                    push      edi
  0047db6c  55                    push      ebp
  0047db6d  33ed                  xor       ebp,ebp
  0047db6f  53                    push      ebx
  0047db70  66396e10              cmp       [esi+10h],bp
  0047db74  7539                  jne       short 0047DBAFh
  0047db76  8b4674                mov       eax,[esi+74h]
  0047db79  8b08                  mov       ecx,[eax]
  0047db7b  80c980                or        cl,80h
  0047db7e  8908                  mov       [eax],ecx
  0047db80  8b06                  mov       eax,[esi]
  0047db82  0c80                  or        al,80h
  0047db84  896e78                mov       [esi+78h],ebp
  0047db87  8906                  mov       [esi],eax
  0047db89  8d461c                lea       eax,[esi+1Ch]
  0047db8c  50                    push      eax
  0047db8d  6800120000            push      1200h
  0047db92  6800010000            push      100h
  0047db97  6a03                  push      3
  0047db99  c7462800180018        mov       dword [esi+28h],18001800h
  0047dba0  c7463880808000        mov       dword [esi+38h],808080h
  0047dba7  e874ff0b00            call      0053DB20h
  0047dbac  83c410                add       esp,10h

LAB_0047dbaf:
  0047dbaf  668b4610              mov       ax,[esi+10h]
  0047dbb3  663d0800              cmp       ax,8
  0047dbb7  0f8d90000000          jge       near 0047DC4Dh
  0047dbbd  8b7e74                mov       edi,[esi+74h]
  0047dbc0  66894612              mov       [esi+12h],ax
  0047dbc4  e877950c00            call      00547140h
  0047dbc9  8b5f1c                mov       ebx,[edi+1Ch]
  0047dbcc  25ff000000            and       eax,0FFh
  0047dbd1  2d80000000            sub       eax,80h
  0047dbd6  c1e00c                shl       eax,0Ch
  0047dbd9  03c3                  add       eax,ebx
  0047dbdb  89442410              mov       [esp+10h],eax
  0047dbdf  8b4f60                mov       ecx,[edi+60h]
  0047dbe2  894c2414              mov       [esp+14h],ecx
  0047dbe6  e855950c00            call      00547140h
  0047dbeb  8b5f24                mov       ebx,[edi+24h]
  0047dbee  25ff000000            and       eax,0FFh
  0047dbf3  2d80000000            sub       eax,80h
  0047dbf8  33d2                  xor       edx,edx
  0047dbfa  8a560b                mov       dl,[esi+0Bh]
  0047dbfd  6aff                  push      0FFFFFFFFh
  0047dbff  c1e00c                shl       eax,0Ch
  0047dc02  03c3                  add       eax,ebx
  0047dc04  52                    push      edx
  0047dc05  89442420              mov       [esp+20h],eax
  0047dc09  6a4f                  push      4Fh
  0047dc0b  8d44241c              lea       eax,[esp+1Ch]
  0047dc0f  68d0195500            push      5519D0h
  0047dc14  50                    push      eax
  0047dc15  56                    push      esi
  0047dc16  e825d0ffff            call      0047AC40h
  0047dc1b  8b4f1c                mov       ecx,[edi+1Ch]
  0047dc1e  83c418                add       esp,18h
  0047dc21  894e1c                mov       [esi+1Ch],ecx
  0047dc24  57                    push      edi
  0047dc25  e896af0b00            call      00538BC0h
  0047dc2a  8b4f20                mov       ecx,[edi+20h]
  0047dc2d  83c064                add       eax,64h
  0047dc30  c1e00c                shl       eax,0Ch
  0047dc33  83c404                add       esp,4
  0047dc36  03c1                  add       eax,ecx
  0047dc38  894620                mov       [esi+20h],eax
  0047dc3b  8b5724                mov       edx,[edi+24h]
  0047dc3e  66ff4610              inc       word [esi+10h]
  0047dc42  5b                    pop       ebx
  0047dc43  5d                    pop       ebp
  0047dc44  895624                mov       [esi+24h],edx
  0047dc47  5f                    pop       edi
  0047dc48  5e                    pop       esi
  0047dc49  83c40c                add       esp,0Ch
  0047dc4c  c3                    ret

LAB_0047dc4d:
  0047dc4d  663d1000              cmp       ax,10h
  0047dc51  7d0f                  jge       short 0047DC62h
  0047dc53  8b4674                mov       eax,[esi+74h]
  0047dc56  8b08                  mov       ecx,[eax]
  0047dc58  80e17f                and       cl,7Fh
  0047dc5b  8908                  mov       [eax],ecx
  0047dc5d  e9ef000000            jmp       0047DD51h

LAB_0047dc62:
  0047dc62  8b7e78                mov       edi,[esi+78h]
  0047dc65  3bfd                  cmp       edi,ebp
  0047dc67  7412                  je        short 0047DC7Bh
  0047dc69  f70700000050          test      dword [edi],50000000h
  0047dc6f  741c                  je        short 0047DC8Dh
  0047dc71  896f74                mov       [edi+74h],ebp
  0047dc74  896e78                mov       [esi+78h],ebp
  0047dc77  33ff                  xor       edi,edi
  0047dc79  eb12                  jmp       short 0047DC8Dh

LAB_0047dc7b:
  0047dc7b  8b4674                mov       eax,[esi+74h]
  0047dc7e  6a01                  push      1
  0047dc80  6aff                  push      0FFFFFFFFh
  0047dc82  50                    push      eax
  0047dc83  e82872ffff            call      00474EB0h
  0047dc88  83c40c                add       esp,0Ch
  0047dc8b  8bf8                  mov       edi,eax

LAB_0047dc8d:
  0047dc8d  3bfd                  cmp       edi,ebp
  0047dc8f  7448                  je        short 0047DCD9h
  0047dc91  897e78                mov       [esi+78h],edi
  0047dc94  8b4f1c                mov       ecx,[edi+1Ch]
  0047dc97  897774                mov       [edi+74h],esi
  0047dc9a  8b461c                mov       eax,[esi+1Ch]
  0047dc9d  8b5e20                mov       ebx,[esi+20h]
  0047dca0  2bc8                  sub       ecx,eax
  0047dca2  c1f903                sar       ecx,3
  0047dca5  03c8                  add       ecx,eax
  0047dca7  57                    push      edi
  0047dca8  894e1c                mov       [esi+1Ch],ecx
  0047dcab  e810af0b00            call      00538BC0h
  0047dcb0  8b5720                mov       edx,[edi+20h]
  0047dcb3  24fe                  and       al,0FEh
  0047dcb5  c1e00b                shl       eax,0Bh
  0047dcb8  03c2                  add       eax,edx
  0047dcba  83c404                add       esp,4
  0047dcbd  2bc3                  sub       eax,ebx
  0047dcbf  c1f803                sar       eax,3
  0047dcc2  03c3                  add       eax,ebx
  0047dcc4  894620                mov       [esi+20h],eax
  0047dcc7  8b4624                mov       eax,[esi+24h]
  0047dcca  8b5724                mov       edx,[edi+24h]
  0047dccd  2bd0                  sub       edx,eax
  0047dccf  c1fa03                sar       edx,3
  0047dcd2  03d0                  add       edx,eax
  0047dcd4  895624                mov       [esi+24h],edx
  0047dcd7  eb26                  jmp       short 0047DCFFh

LAB_0047dcd9:
  0047dcd9  8b7e74                mov       edi,[esi+74h]
  0047dcdc  57                    push      edi
  0047dcdd  8b471c                mov       eax,[edi+1Ch]
  0047dce0  89461c                mov       [esi+1Ch],eax
  0047dce3  e8d8ae0b00            call      00538BC0h
  0047dce8  8b4f20                mov       ecx,[edi+20h]
  0047dceb  83c064                add       eax,64h
  0047dcee  c1e00c                shl       eax,0Ch
  0047dcf1  83c404                add       esp,4
  0047dcf4  03c1                  add       eax,ecx
  0047dcf6  894620                mov       [esi+20h],eax
  0047dcf9  8b4f24                mov       ecx,[edi+24h]
  0047dcfc  894e24                mov       [esi+24h],ecx

LAB_0047dcff:
  0047dcff  8b5620                mov       edx,[esi+20h]
  0047dd02  56                    push      esi
  0047dd03  89542418              mov       [esp+18h],edx
  0047dd07  e8345c0b00            call      00533940h
  0047dd0c  8b4c2418              mov       ecx,[esp+18h]
  0047dd10  8b4660                mov       eax,[esi+60h]
  0047dd13  83c404                add       esp,4
  0047dd16  3bc8                  cmp       ecx,eax
  0047dd18  894e20                mov       [esi+20h],ecx
  0047dd1b  7f0d                  jg        short 0047DD2Ah
  0047dd1d  894620                mov       [esi+20h],eax
  0047dd20  c7869000000000080000  mov       dword [esi+90h],800h

LAB_0047dd2a:
  0047dd2a  f70600000040          test      dword [esi],40000000h
  0047dd30  7519                  jne       short 0047DD4Bh
  0047dd32  f7869000000002880000  test      dword [esi+90h],8802h
  0047dd3c  750d                  jne       short 0047DD4Bh
  0047dd3e  56                    push      esi
  0047dd3f  e8cc73ffff            call      00475110h
  0047dd44  83c404                add       esp,4
  0047dd47  85c0                  test      eax,eax
  0047dd49  7406                  je        short 0047DD51h

LAB_0047dd4b:
  0047dd4b  810e00000002          or        dword [esi],2000000h

LAB_0047dd51:
  0047dd51  8b06                  mov       eax,[esi]
  0047dd53  a900008000            test      eax,800000h
  0047dd58  0f8492000000          je        near 0047DDF0h
  0047dd5e  8b4e74                mov       ecx,[esi+74h]
  0047dd61  8b3db4876000          mov       edi,[6087B4h]
  0047dd67  33d2                  xor       edx,edx
  0047dd69  8a5108                mov       dl,[ecx+8]
  0047dd6c  8b1f                  mov       ebx,[edi]
  0047dd6e  8d4a78                lea       ecx,[edx+78h]
  0047dd71  8d3cd2                lea       edi,[edx+edx*8]
  0047dd74  c1e10d                shl       ecx,0Dh
  0047dd77  03cb                  add       ecx,ebx
  0047dd79  8d147a                lea       edx,[edx+edi*2]
  0047dd7c  33db                  xor       ebx,ebx
  0047dd7e  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0047dd85  c1e304                shl       ebx,4
  0047dd88  668b93204a6000        mov       dx,[ebx+604A20h]
  0047dd8f  668591c41f0000        test      [ecx+1FC4h],dx
  0047dd96  7507                  jne       short 0047DD9Fh
  0047dd98  0d00000002            or        eax,2000000h
  0047dd9d  8906                  mov       [esi],eax

LAB_0047dd9f:
  0047dd9f  a1f02c5f00            mov       eax,[5F2CF0h]
  0047dda4  33d2                  xor       edx,edx
  0047dda6  bf14000000            mov       edi,14h
  0047ddab  f7f7                  div       edi
  0047ddad  85d2                  test      edx,edx
  0047ddaf  754d                  jne       short 0047DDFEh
  0047ddb1  668b814e010000        mov       ax,[ecx+14Eh]
  0047ddb8  663d0400              cmp       ax,4
  0047ddbc  7c16                  jl        short 0047DDD4h
  0047ddbe  83c0fc                add       eax,0FFFFFFFCh
  0047ddc1  5b                    pop       ebx
  0047ddc2  6689814e010000        mov       [ecx+14Eh],ax
  0047ddc9  66ff4610              inc       word [esi+10h]
  0047ddcd  5d                    pop       ebp
  0047ddce  5f                    pop       edi
  0047ddcf  5e                    pop       esi
  0047ddd0  83c40c                add       esp,0Ch
  0047ddd3  c3                    ret

LAB_0047ddd4:
  0047ddd4  6689a94e010000        mov       [ecx+14Eh],bp
  0047dddb  8b06                  mov       eax,[esi]
  0047dddd  0d00000002            or        eax,2000000h
  0047dde2  66ff4610              inc       word [esi+10h]
  0047dde6  5b                    pop       ebx
  0047dde7  5d                    pop       ebp
  0047dde8  8906                  mov       [esi],eax
  0047ddea  5f                    pop       edi
  0047ddeb  5e                    pop       esi
  0047ddec  83c40c                add       esp,0Ch
  0047ddef  c3                    ret

LAB_0047ddf0:
  0047ddf0  66837e1078            cmp       word [esi+10h],78h
  0047ddf5  7c07                  jl        short 0047DDFEh
  0047ddf7  0d00000002            or        eax,2000000h
  0047ddfc  8906                  mov       [esi],eax

LAB_0047ddfe:
  0047ddfe  66ff4610              inc       word [esi+10h]
  0047de02  5b                    pop       ebx
  0047de03  5d                    pop       ebp
  0047de04  5f                    pop       edi
  0047de05  5e                    pop       esi
  0047de06  83c40c                add       esp,0Ch
  0047de09  c3                    ret

15 cible(s) de saut interne : 0x0047db6b, 0x0047dbaf, 0x0047dc4d, 0x0047dc62, 0x0047dc7b, 0x0047dc8d, 0x0047dcd9, 0x0047dcff, 0x0047dd2a, 0x0047dd4b, 0x0047dd51, 0x0047dd9f, 0x0047ddd4, 0x0047ddf0, 0x0047ddfe
