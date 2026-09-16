Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00451b40  section .text  offset fichier 0x50f40  1354 octets

  00451b40  a040985e00            mov       al,[5E9840h]
  00451b45  53                    push      ebx
  00451b46  b310                  mov       bl,10h
  00451b48  55                    push      ebp
  00451b49  56                    push      esi
  00451b4a  8b742410              mov       esi,[esp+10h]
  00451b4e  84c3                  test      bl,al
  00451b50  57                    push      edi
  00451b51  750b                  jne       short 00451B5Eh
  00451b53  66837e1000            cmp       word [esi+10h],0
  00451b58  0f8423050000          je        near 00452081h

LAB_00451b5e:
  00451b5e  668b4610              mov       ax,[esi+10h]
  00451b62  663d0300              cmp       ax,3
  00451b66  7c21                  jl        short 00451B89h
  00451b68  663d0700              cmp       ax,7
  00451b6c  7f1b                  jg        short 00451B89h
  00451b6e  bf02000000            mov       edi,2

LAB_00451b73:
  00451b73  6a00                  push      0
  00451b75  6a00                  push      0
  00451b77  6aff                  push      0FFFFFFFFh
  00451b79  6a00                  push      0
  00451b7b  6a33                  push      33h
  00451b7d  56                    push      esi
  00451b7e  e88d650100            call      00468110h
  00451b83  83c418                add       esp,18h
  00451b86  4f                    dec       edi
  00451b87  75ea                  jne       short 00451B73h

LAB_00451b89:
  00451b89  66837e1000            cmp       word [esi+10h],0
  00451b8e  7515                  jne       short 00451BA5h
  00451b90  66c746100200          mov       word [esi+10h],2
  00451b96  66c746120300          mov       word [esi+12h],3
  00451b9c  66c786980000000000    mov       word [esi+98h],0

LAB_00451ba5:
  00451ba5  841d40985e00          test      [5E9840h],bl
  00451bab  0f84e4000000          je        near 00451C95h
  00451bb1  33c9                  xor       ecx,ecx
  00451bb3  8a0d62905e00          mov       cl,[5E9062h]
  00451bb9  8d0489                lea       eax,[ecx+ecx*4]
  00451bbc  c1e003                shl       eax,3
  00451bbf  2bc1                  sub       eax,ecx
  00451bc1  c1e002                shl       eax,2
  00451bc4  81b8e0935e0000c0c7ff  cmp       dword [eax+5E93E0h],0FFC7C000h
  00451bce  0f8dc1000000          jge       near 00451C95h
  00451bd4  66817e10ff00          cmp       word [esi+10h],0FFh
  00451bda  0f84b5000000          je        near 00451C95h
  00451be0  8d88c0935e00          lea       ecx,[eax+5E93C0h]
  00451be6  8b80c0935e00          mov       eax,[eax+5E93C0h]
  00451bec  a900000080            test      eax,80000000h
  00451bf1  740f                  je        short 00451C02h
  00451bf3  a900000050            test      eax,50000000h
  00451bf8  7508                  jne       short 00451C02h
  00451bfa  6a03                  push      3
  00451bfc  51                    push      ecx
  00451bfd  e985000000            jmp       00451C87h

LAB_00451c02:
  00451c02  660fb60538e55d00      movzx     ax,byte [5DE538h]
  00451c0a  0fbfc0                movsx     eax,ax
  00451c0d  8d0c80                lea       ecx,[eax+eax*4]
  00451c10  c1e103                shl       ecx,3
  00451c13  2bc8                  sub       ecx,eax
  00451c15  8b048dc0935e00        mov       eax,[ecx*4+5E93C0h]
  00451c1c  a900000080            test      eax,80000000h
  00451c21  8d0c8dc0935e00        lea       ecx,[ecx*4+5E93C0h]
  00451c28  7413                  je        short 00451C3Dh
  00451c2a  a900000050            test      eax,50000000h
  00451c2f  750c                  jne       short 00451C3Dh
  00451c31  8b15cc975e00          mov       edx,[5E97CCh]
  00451c37  6a03                  push      3
  00451c39  51                    push      ecx
  00451c3a  52                    push      edx
  00451c3b  eb50                  jmp       short 00451C8Dh

LAB_00451c3d:
  00451c3d  bf63000000            mov       edi,63h
  00451c42  b803000000            mov       eax,3
  00451c47  ba94955e00            mov       edx,5E9594h

LAB_00451c4c:
  00451c4c  8b0a                  mov       ecx,[edx]
  00451c4e  f7c100000080          test      ecx,80000000h
  00451c54  740a                  je        short 00451C60h
  00451c56  f7c100000050          test      ecx,50000000h
  00451c5c  7502                  jne       short 00451C60h
  00451c5e  8bf8                  mov       edi,eax

LAB_00451c60:
  00451c60  48                    dec       eax
  00451c61  81ea9c000000          sub       edx,9Ch
  00451c67  6685c0                test      ax,ax
  00451c6a  7de0                  jge       short 00451C4Ch
  00451c6c  6683ff63              cmp       di,63h
  00451c70  7423                  je        short 00451C95h
  00451c72  0fbfc7                movsx     eax,di
  00451c75  6a03                  push      3
  00451c77  8d0c80                lea       ecx,[eax+eax*4]
  00451c7a  c1e103                shl       ecx,3
  00451c7d  2bc8                  sub       ecx,eax
  00451c7f  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00451c86  52                    push      edx

LAB_00451c87:
  00451c87  a1cc975e00            mov       eax,[5E97CCh]
  00451c8c  50                    push      eax

LAB_00451c8d:
  00451c8d  e83e210c00            call      00513DD0h
  00451c92  83c40c                add       esp,0Ch

LAB_00451c95:
  00451c95  bd01000000            mov       ebp,1
  00451c9a  66396e10              cmp       [esi+10h],bp
  00451c9e  0f85cd000000          jne       near 00451D71h
  00451ca4  668b4612              mov       ax,[esi+12h]
  00451ca8  8b5620                mov       edx,[esi+20h]
  00451cab  0fbfc8                movsx     ecx,ax
  00451cae  c1e10c                shl       ecx,0Ch
  00451cb1  2bd1                  sub       edx,ecx
  00451cb3  40                    inc       eax
  00451cb4  663d0a00              cmp       ax,0Ah
  00451cb8  895620                mov       [esi+20h],edx
  00451cbb  66894612              mov       [esi+12h],ax
  00451cbf  7e06                  jle       short 00451CC7h
  00451cc1  66c746120a00          mov       word [esi+12h],0Ah

LAB_00451cc7:
  00451cc7  8b4620                mov       eax,[esi+20h]
  00451cca  66834634f6            add       word [esi+34h],0FFF6h
  00451ccf  bf0020d1ff            mov       edi,0FFD12000h
  00451cd4  3bc7                  cmp       eax,edi
  00451cd6  0f8d95000000          jge       near 00451D71h
  00451cdc  6683be9800000000      cmp       word [esi+98h],0
  00451ce4  7532                  jne       short 00451D18h
  00451ce6  8b15cc975e00          mov       edx,[5E97CCh]
  00451cec  6a00                  push      0
  00451cee  6a00                  push      0
  00451cf0  6800100000            push      1000h
  00451cf5  8b82a8000000          mov       eax,[edx+0A8h]
  00451cfb  6800030000            push      300h
  00451d00  8b08                  mov       ecx,[eax]
  00451d02  660fb6512c            movzx     dx,byte [ecx+2Ch]
  00451d07  52                    push      edx
  00451d08  56                    push      esi
  00451d09  e872630100            call      00468080h
  00451d0e  83c418                add       esp,18h
  00451d11  6689ae98000000        mov       [esi+98h],bp

LAB_00451d18:
  00451d18  897e20                mov       [esi+20h],edi
  00451d1b  bf0b000000            mov       edi,0Bh

LAB_00451d20:
  00451d20  6a00                  push      0
  00451d22  6a00                  push      0
  00451d24  6aff                  push      0FFFFFFFFh
  00451d26  6a00                  push      0
  00451d28  6a32                  push      32h
  00451d2a  56                    push      esi
  00451d2b  e8e0630100            call      00468110h
  00451d30  83c418                add       esp,18h
  00451d33  4f                    dec       edi
  00451d34  75ea                  jne       short 00451D20h
  00451d36  8a06                  mov       al,[esi]
  00451d38  66c746100300          mov       word [esi+10h],3
  00451d3e  a820                  test      al,20h
  00451d40  742f                  je        short 00451D71h
  00451d42  841d40985e00          test      [5E9840h],bl
  00451d48  7427                  je        short 00451D71h
  00451d4a  33c0                  xor       eax,eax
  00451d4c  6a00                  push      0
  00451d4e  a062905e00            mov       al,[5E9062h]
  00451d53  8d0c80                lea       ecx,[eax+eax*4]
  00451d56  c1e103                shl       ecx,3
  00451d59  2bc8                  sub       ecx,eax
  00451d5b  a1cc975e00            mov       eax,[5E97CCh]
  00451d60  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00451d67  52                    push      edx
  00451d68  50                    push      eax
  00451d69  e862200c00            call      00513DD0h
  00451d6e  83c40c                add       esp,0Ch

LAB_00451d71:
  00451d71  66837e1002            cmp       word [esi+10h],2
  00451d76  bb14000000            mov       ebx,14h
  00451d7b  752a                  jne       short 00451DA7h
  00451d7d  66ff4612              inc       word [esi+12h]
  00451d81  66395e12              cmp       [esi+12h],bx
  00451d85  7520                  jne       short 00451DA7h
  00451d87  33c0                  xor       eax,eax
  00451d89  66896e10              mov       [esi+10h],bp
  00451d8d  66c746120300          mov       word [esi+12h],3
  00451d93  66c746141405          mov       word [esi+14h],514h
  00451d99  66894616              mov       [esi+16h],ax
  00451d9d  6689869a000000        mov       [esi+9Ah],ax
  00451da4  894644                mov       [esi+44h],eax

LAB_00451da7:
  00451da7  66837e1003            cmp       word [esi+10h],3
  00451dac  bd05000000            mov       ebp,5
  00451db1  0f8596000000          jne       near 00451E4Dh
  00451db7  0fbf4e14              movsx     ecx,word [esi+14h]
  00451dbb  66ff869a000000        inc       word [esi+9Ah]
  00451dc2  51                    push      ecx
  00451dc3  e8543c0f00            call      00545A1Ch
  00451dc8  0fbf5616              movsx     edx,word [esi+16h]
  00451dcc  d1fa                  sar       edx,1
  00451dce  0fafc2                imul      eax,edx
  00451dd1  8b561c                mov       edx,[esi+1Ch]
  00451dd4  668b7e16              mov       di,[esi+16h]
  00451dd8  03d0                  add       edx,eax
  00451dda  0fbf4614              movsx     eax,word [esi+14h]
  00451dde  50                    push      eax
  00451ddf  89561c                mov       [esi+1Ch],edx
  00451de2  e82f3c0f00            call      00545A16h
  00451de7  0fbfcf                movsx     ecx,di
  00451dea  d1f9                  sar       ecx,1
  00451dec  0fafc1                imul      eax,ecx
  00451def  8b4e24                mov       ecx,[esi+24h]
  00451df2  03c8                  add       ecx,eax
  00451df4  8d4701                lea       eax,[edi+1]
  00451df7  894e24                mov       [esi+24h],ecx
  00451dfa  b9f6ffffff            mov       ecx,0FFFFFFF6h
  00451dff  66014e14              add       [esi+14h],cx
  00451e03  66817e1438ff          cmp       word [esi+14h],0FF38h
  00451e09  66894616              mov       [esi+16h],ax
  00451e0d  7d0e                  jge       short 00451E1Dh
  00451e0f  66c746100400          mov       word [esi+10h],4
  00451e15  894e5c                mov       [esi+5Ch],ecx
  00451e18  5f                    pop       edi
  00451e19  5e                    pop       esi
  00451e1a  5d                    pop       ebp
  00451e1b  5b                    pop       ebx
  00451e1c  c3                    ret

LAB_00451e1d:
  00451e1d  663bc3                cmp       ax,bx
  00451e20  7e04                  jle       short 00451E26h
  00451e22  66895e16              mov       [esi+16h],bx

LAB_00451e26:
  00451e26  8b4644                mov       eax,[esi+44h]
  00451e29  8bd0                  mov       edx,eax
  00451e2b  d1fa                  sar       edx,1
  00451e2d  66015632              add       [esi+32h],dx
  00451e31  40                    inc       eax
  00451e32  3bc3                  cmp       eax,ebx
  00451e34  894644                mov       [esi+44h],eax
  00451e37  7e03                  jle       short 00451E3Ch
  00451e39  895e44                mov       [esi+44h],ebx

LAB_00451e3c:
  00451e3c  66016e34              add       [esi+34h],bp
  00451e40  66837e3400            cmp       word [esi+34h],0
  00451e45  7e06                  jle       short 00451E4Dh
  00451e47  66c746340000          mov       word [esi+34h],0

LAB_00451e4d:
  00451e4d  66837e1004            cmp       word [esi+10h],4
  00451e52  0f85ad000000          jne       near 00451F05h
  00451e58  0fbf4614              movsx     eax,word [esi+14h]
  00451e5c  66ff869a000000        inc       word [esi+9Ah]
  00451e63  50                    push      eax
  00451e64  e8b33b0f00            call      00545A1Ch
  00451e69  0fbf4e16              movsx     ecx,word [esi+16h]
  00451e6d  d1f9                  sar       ecx,1
  00451e6f  0fafc1                imul      eax,ecx
  00451e72  0fbf5614              movsx     edx,word [esi+14h]
  00451e76  8b4e1c                mov       ecx,[esi+1Ch]
  00451e79  668b7e16              mov       di,[esi+16h]
  00451e7d  03c8                  add       ecx,eax
  00451e7f  52                    push      edx
  00451e80  894e1c                mov       [esi+1Ch],ecx
  00451e83  e88e3b0f00            call      00545A16h
  00451e88  0fbfcf                movsx     ecx,di
  00451e8b  d1f9                  sar       ecx,1
  00451e8d  668b565c              mov       dx,[esi+5Ch]
  00451e91  0fafc1                imul      eax,ecx
  00451e94  8b4e24                mov       ecx,[esi+24h]
  00451e97  66015614              add       [esi+14h],dx
  00451e9b  8b565c                mov       edx,[esi+5Ch]
  00451e9e  03c8                  add       ecx,eax
  00451ea0  83c209                add       edx,9
  00451ea3  894e24                mov       [esi+24h],ecx
  00451ea6  8bca                  mov       ecx,edx
  00451ea8  8d4701                lea       eax,[edi+1]
  00451eab  3bcb                  cmp       ecx,ebx
  00451ead  66894616              mov       [esi+16h],ax
  00451eb1  89565c                mov       [esi+5Ch],edx
  00451eb4  7e03                  jle       short 00451EB9h
  00451eb6  895e5c                mov       [esi+5Ch],ebx

LAB_00451eb9:
  00451eb9  668b8e9a000000        mov       cx,[esi+9Ah]
  00451ec0  6681f9aa00            cmp       cx,0AAh
  00451ec5  7e04                  jle       short 00451ECBh
  00451ec7  83465cfa              add       dword [esi+5Ch],0FFFFFFFAh

LAB_00451ecb:
  00451ecb  6681f9f500            cmp       cx,0F5h
  00451ed0  7c0f                  jl        short 00451EE1h
  00451ed2  66896e10              mov       [esi+10h],bp
  00451ed6  66c746120000          mov       word [esi+12h],0
  00451edc  5f                    pop       edi
  00451edd  5e                    pop       esi
  00451ede  5d                    pop       ebp
  00451edf  5b                    pop       ebx
  00451ee0  c3                    ret

LAB_00451ee1:
  00451ee1  663bc3                cmp       ax,bx
  00451ee4  7e04                  jle       short 00451EEAh
  00451ee6  66895e16              mov       [esi+16h],bx

LAB_00451eea:
  00451eea  8b4644                mov       eax,[esi+44h]
  00451eed  8bc8                  mov       ecx,eax
  00451eef  d1f9                  sar       ecx,1
  00451ef1  66014e32              add       [esi+32h],cx
  00451ef5  48                    dec       eax
  00451ef6  83f8f6                cmp       eax,0FFFFFFF6h
  00451ef9  894644                mov       [esi+44h],eax
  00451efc  7d07                  jge       short 00451F05h
  00451efe  c74644f6ffffff        mov       dword [esi+44h],0FFFFFFF6h

LAB_00451f05:
  00451f05  668b4e10              mov       cx,[esi+10h]
  00451f09  bb08000000            mov       ebx,8
  00451f0e  663bcd                cmp       cx,bp
  00451f11  753e                  jne       short 00451F51h
  00451f13  8b7e1c                mov       edi,[esi+1Ch]
  00451f16  66ff4612              inc       word [esi+12h]
  00451f1a  668b4612              mov       ax,[esi+12h]
  00451f1e  66015e32              add       [esi+32h],bx
  00451f22  ba00200000            mov       edx,2000h
  00451f27  03fa                  add       edi,edx
  00451f29  897e1c                mov       [esi+1Ch],edi
  00451f2c  8b7e24                mov       edi,[esi+24h]
  00451f2f  03fa                  add       edi,edx
  00451f31  663d3c00              cmp       ax,3Ch
  00451f35  897e24                mov       [esi+24h],edi
  00451f38  7e17                  jle       short 00451F51h
  00451f3a  66c746100600          mov       word [esi+10h],6
  00451f40  66c74614bc02          mov       word [esi+14h],2BCh
  00451f46  66c746160000          mov       word [esi+16h],0
  00451f4c  5f                    pop       edi
  00451f4d  5e                    pop       esi
  00451f4e  5d                    pop       ebp
  00451f4f  5b                    pop       ebx
  00451f50  c3                    ret

LAB_00451f51:
  00451f51  6683f906              cmp       cx,6
  00451f55  7573                  jne       short 00451FCAh
  00451f57  0fbf5614              movsx     edx,word [esi+14h]
  00451f5b  66ff869a000000        inc       word [esi+9Ah]
  00451f62  66015e32              add       [esi+32h],bx
  00451f66  52                    push      edx
  00451f67  e8b03a0f00            call      00545A1Ch
  00451f6c  0fbf4e16              movsx     ecx,word [esi+16h]
  00451f70  d1f9                  sar       ecx,1
  00451f72  0fafc1                imul      eax,ecx
  00451f75  0fbf5614              movsx     edx,word [esi+14h]
  00451f79  8b4e1c                mov       ecx,[esi+1Ch]
  00451f7c  668b7e16              mov       di,[esi+16h]
  00451f80  03c8                  add       ecx,eax
  00451f82  52                    push      edx
  00451f83  894e1c                mov       [esi+1Ch],ecx
  00451f86  e88b3a0f00            call      00545A16h
  00451f8b  0fbfcf                movsx     ecx,di
  00451f8e  d1f9                  sar       ecx,1
  00451f90  0fafc1                imul      eax,ecx
  00451f93  8b5624                mov       edx,[esi+24h]
  00451f96  03d0                  add       edx,eax
  00451f98  8d4701                lea       eax,[edi+1]
  00451f9b  663d2800              cmp       ax,28h
  00451f9f  895624                mov       [esi+24h],edx
  00451fa2  66894616              mov       [esi+16h],ax
  00451fa6  7e06                  jle       short 00451FAEh
  00451fa8  66c746162800          mov       word [esi+16h],28h

LAB_00451fae:
  00451fae  6681be9a0000001d01    cmp       word [esi+9Ah],11Dh
  00451fb7  7c11                  jl        short 00451FCAh
  00451fb9  66c746100700          mov       word [esi+10h],7
  00451fbf  66c746120d00          mov       word [esi+12h],0Dh
  00451fc5  5f                    pop       edi
  00451fc6  5e                    pop       esi
  00451fc7  5d                    pop       ebp
  00451fc8  5b                    pop       ebx
  00451fc9  c3                    ret

LAB_00451fca:
  00451fca  66837e1007            cmp       word [esi+10h],7
  00451fcf  0f8588000000          jne       near 0045205Dh
  00451fd5  0fbf5614              movsx     edx,word [esi+14h]
  00451fd9  66ff869a000000        inc       word [esi+9Ah]
  00451fe0  66015e32              add       [esi+32h],bx
  00451fe4  66834634ec            add       word [esi+34h],0FFECh
  00451fe9  52                    push      edx
  00451fea  e82d3a0f00            call      00545A1Ch
  00451fef  0fbf4e16              movsx     ecx,word [esi+16h]
  00451ff3  d1f9                  sar       ecx,1
  00451ff5  0fafc1                imul      eax,ecx
  00451ff8  0fbf5614              movsx     edx,word [esi+14h]
  00451ffc  8b4e1c                mov       ecx,[esi+1Ch]
  00451fff  668b7e16              mov       di,[esi+16h]
  00452003  03c8                  add       ecx,eax
  00452005  52                    push      edx
  00452006  894e1c                mov       [esi+1Ch],ecx
  00452009  e8083a0f00            call      00545A16h
  0045200e  0fbfcf                movsx     ecx,di
  00452011  d1f9                  sar       ecx,1
  00452013  0fafc1                imul      eax,ecx
  00452016  8b5624                mov       edx,[esi+24h]
  00452019  8b4e20                mov       ecx,[esi+20h]
  0045201c  03d0                  add       edx,eax
  0045201e  668b4612              mov       ax,[esi+12h]
  00452022  895624                mov       [esi+24h],edx
  00452025  0fbfd0                movsx     edx,ax
  00452028  c1e20c                shl       edx,0Ch
  0045202b  2bca                  sub       ecx,edx
  0045202d  40                    inc       eax
  0045202e  66894612              mov       [esi+12h],ax
  00452032  8d47fe                lea       eax,[edi-2]
  00452035  663bc5                cmp       ax,bp
  00452038  894e20                mov       [esi+20h],ecx
  0045203b  66894616              mov       [esi+16h],ax
  0045203f  7d04                  jge       short 00452045h
  00452041  66896e16              mov       [esi+16h],bp

LAB_00452045:
  00452045  817e200080a8ff        cmp       dword [esi+20h],0FFA88000h
  0045204c  7d0f                  jge       short 0045205Dh
  0045204e  66895e10              mov       [esi+10h],bx
  00452052  66c746120000          mov       word [esi+12h],0
  00452058  5f                    pop       edi
  00452059  5e                    pop       esi
  0045205a  5d                    pop       ebp
  0045205b  5b                    pop       ebx
  0045205c  c3                    ret

LAB_0045205d:
  0045205d  66395e10              cmp       [esi+10h],bx
  00452061  751e                  jne       short 00452081h
  00452063  8b3e                  mov       edi,[esi]
  00452065  81cf00600000          or        edi,6000h
  0045206b  66ff4612              inc       word [esi+12h]
  0045206f  66837e123c            cmp       word [esi+12h],3Ch
  00452074  893e                  mov       [esi],edi
  00452076  8bc7                  mov       eax,edi
  00452078  7c07                  jl        short 00452081h
  0045207a  25ffffff7f            and       eax,7FFFFFFFh
  0045207f  8906                  mov       [esi],eax

LAB_00452081:
  00452081  5f                    pop       edi
  00452082  5e                    pop       esi
  00452083  5d                    pop       ebp
  00452084  5b                    pop       ebx
  00452085  c3                    ret
  00452086  90                    nop
  00452087  90                    nop
  00452088  90                    nop
  00452089  90                    nop

31 cible(s) de saut interne : 0x00451b5e, 0x00451b73, 0x00451b89, 0x00451ba5, 0x00451c02, 0x00451c3d, 0x00451c4c, 0x00451c60, 0x00451c87, 0x00451c8d, 0x00451c95, 0x00451cc7, 0x00451d18, 0x00451d20, 0x00451d71, 0x00451da7, 0x00451e1d, 0x00451e26, 0x00451e3c, 0x00451e4d, 0x00451eb9, 0x00451ecb, 0x00451ee1, 0x00451eea, 0x00451f05, 0x00451f51, 0x00451fae, 0x00451fca, 0x00452045, 0x0045205d, 0x00452081
