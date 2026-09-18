
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00483cc0  section .text  offset fichier 0x830c0  464 octets

  00483cc0  83ec18                sub       esp,18h
  00483cc3  53                    push      ebx
  00483cc4  55                    push      ebp
  00483cc5  56                    push      esi
  00483cc6  8b742428              mov       esi,[esp+28h]
  00483cca  33ed                  xor       ebp,ebp
  00483ccc  57                    push      edi
  00483ccd  8b06                  mov       eax,[esi]
  00483ccf  66896c2410            mov       [esp+10h],bp
  00483cd4  a900000002            test      eax,2000000h
  00483cd9  66c74424126aff        mov       word [esp+12h],0FF6Ah
  00483ce0  66c74424149600        mov       word [esp+14h],96h
  00483ce7  66c74424163200        mov       word [esp+16h],32h
  00483cee  66896c2418            mov       [esp+18h],bp
  00483cf3  66c744241a2c01        mov       word [esp+1Ah],12Ch
  00483cfa  0f844d010000          je        near 00483E4Dh
  00483d00  0fbf5e32              movsx     ebx,word [esi+32h]
  00483d04  8b7e5c                mov       edi,[esi+5Ch]
  00483d07  53                    push      ebx
  00483d08  e80f1d0c00            call      00545A1Ch
  00483d0d  0fafc7                imul      eax,edi
  00483d10  8b4e1c                mov       ecx,[esi+1Ch]
  00483d13  53                    push      ebx
  00483d14  c1f80c                sar       eax,0Ch
  00483d17  03c8                  add       ecx,eax
  00483d19  894e1c                mov       [esi+1Ch],ecx
  00483d1c  e8f51c0c00            call      00545A16h
  00483d21  0fafc7                imul      eax,edi
  00483d24  8b5624                mov       edx,[esi+24h]
  00483d27  c1f80c                sar       eax,0Ch
  00483d2a  03d0                  add       edx,eax
  00483d2c  668b4610              mov       ax,[esi+10h]
  00483d30  663d2800              cmp       ax,28h
  00483d34  895624                mov       [esi+24h],edx
  00483d37  0f8def000000          jge       near 00483E2Ch
  00483d3d  40                    inc       eax
  00483d3e  8d4c2410              lea       ecx,[esp+10h]
  00483d42  66894610              mov       [esi+10h],ax
  00483d46  b800a00000            mov       eax,0A000h
  00483d4b  2bc7                  sub       eax,edi
  00483d4d  51                    push      ecx
  00483d4e  c1f803                sar       eax,3
  00483d51  03c7                  add       eax,edi
  00483d53  56                    push      esi
  00483d54  89465c                mov       [esi+5Ch],eax
  00483d57  e8440fffff            call      00474CA0h
  00483d5c  8bf8                  mov       edi,eax
  00483d5e  83c408                add       esp,8
  00483d61  3bfd                  cmp       edi,ebp
  00483d63  741c                  je        short 00483D81h
  00483d65  8b5674                mov       edx,[esi+74h]
  00483d68  57                    push      edi
  00483d69  52                    push      edx
  00483d6a  e8115e0b00            call      00539B80h
  00483d6f  8b07                  mov       eax,[edi]
  00483d71  83c408                add       esp,8
  00483d74  0c40                  or        al,40h
  00483d76  66c787960000000800    mov       word [edi+96h],8
  00483d7f  8907                  mov       [edi],eax

LAB_00483d81:
  00483d81  f6461007              test      byte [esi+10h],7
  00483d85  0f85f6000000          jne       near 00483E81h
  00483d8b  0fbf5632              movsx     edx,word [esi+32h]
  00483d8f  8b4620                mov       eax,[esi+20h]
  00483d92  8b4e24                mov       ecx,[esi+24h]
  00483d95  8b6e1c                mov       ebp,[esi+1Ch]
  00483d98  89442420              mov       [esp+20h],eax
  00483d9c  894c2424              mov       [esp+24h],ecx
  00483da0  8954242c              mov       [esp+2Ch],edx
  00483da4  e897330c00            call      00547140h
  00483da9  8bd8                  mov       ebx,eax
  00483dab  e890330c00            call      00547140h
  00483db0  99                    cdq
  00483db1  b964000000            mov       ecx,64h
  00483db6  53                    push      ebx
  00483db7  f7f9                  idiv      ecx
  00483db9  8bfa                  mov       edi,edx
  00483dbb  83c714                add       edi,14h
  00483dbe  e8591c0c00            call      00545A1Ch
  00483dc3  0fafc7                imul      eax,edi
  00483dc6  03c5                  add       eax,ebp
  00483dc8  53                    push      ebx
  00483dc9  89461c                mov       [esi+1Ch],eax
  00483dcc  e8451c0c00            call      00545A16h
  00483dd1  0fafc7                imul      eax,edi
  00483dd4  8b7c2424              mov       edi,[esp+24h]
  00483dd8  81c300080000          add       ebx,800h
  00483dde  03c7                  add       eax,edi
  00483de0  66895e32              mov       [esi+32h],bx
  00483de4  894624                mov       [esi+24h],eax
  00483de7  8b15cc975e00          mov       edx,[5E97CCh]
  00483ded  6a02                  push      2
  00483def  6a00                  push      0
  00483df1  8b82d0000000          mov       eax,[edx+0D0h]
  00483df7  33d2                  xor       edx,edx
  00483df9  8b08                  mov       ecx,[eax]
  00483dfb  8a5114                mov       dl,[ecx+14h]
  00483dfe  81c2c0000000          add       edx,0C0h
  00483e04  52                    push      edx
  00483e05  56                    push      esi
  00483e06  e8d56cffff            call      0047AAE0h
  00483e0b  8b442430              mov       eax,[esp+30h]
  00483e0f  668b4c243c            mov       cx,[esp+3Ch]
  00483e14  83c410                add       esp,10h
  00483e17  896e1c                mov       [esi+1Ch],ebp
  00483e1a  894620                mov       [esi+20h],eax
  00483e1d  897e24                mov       [esi+24h],edi
  00483e20  66894e32              mov       [esi+32h],cx
  00483e24  5f                    pop       edi
  00483e25  5e                    pop       esi
  00483e26  5d                    pop       ebp
  00483e27  5b                    pop       ebx
  00483e28  83c418                add       esp,18h
  00483e2b  c3                    ret

LAB_00483e2c:
  00483e2c  8b4638                mov       eax,[esi+38h]
  00483e2f  8bd7                  mov       edx,edi
  00483e31  c1fa03                sar       edx,3
  00483e34  2bfa                  sub       edi,edx
  00483e36  05f8f7f7ff            add       eax,0FFF7F7F8h
  00483e3b  897e5c                mov       [esi+5Ch],edi
  00483e3e  894638                mov       [esi+38h],eax
  00483e41  753e                  jne       short 00483E81h
  00483e43  892e                  mov       [esi],ebp
  00483e45  5f                    pop       edi
  00483e46  5e                    pop       esi
  00483e47  5d                    pop       ebp
  00483e48  5b                    pop       ebx
  00483e49  83c418                add       esp,18h
  00483e4c  c3                    ret

LAB_00483e4d:
  00483e4d  0fbf4e28              movsx     ecx,word [esi+28h]
  00483e51  ba00300000            mov       edx,3000h
  00483e56  2bd1                  sub       edx,ecx
  00483e58  c1fa03                sar       edx,3
  00483e5b  03ca                  add       ecx,edx
  00483e5d  8b5638                mov       edx,[esi+38h]
  00483e60  81c208080800          add       edx,80808h
  00483e66  894e28                mov       [esi+28h],ecx
  00483e69  66894e2c              mov       [esi+2Ch],cx
  00483e6d  8bca                  mov       ecx,edx
  00483e6f  81f980808000          cmp       ecx,808080h
  00483e75  895638                mov       [esi+38h],edx
  00483e78  7507                  jne       short 00483E81h
  00483e7a  0d00000002            or        eax,2000000h
  00483e7f  8906                  mov       [esi],eax

LAB_00483e81:
  00483e81  5f                    pop       edi
  00483e82  5e                    pop       esi
  00483e83  5d                    pop       ebp
  00483e84  5b                    pop       ebx
  00483e85  83c418                add       esp,18h
  00483e88  c3                    ret

4 cible(s) de saut interne : 0x00483d81, 0x00483e2c, 0x00483e4d, 0x00483e81
