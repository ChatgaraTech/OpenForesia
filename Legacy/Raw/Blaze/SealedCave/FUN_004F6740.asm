Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004f6740  section .text  offset fichier 0xf5b40  1824 octets

  004f6740  83ec20                sub       esp,20h
  004f6743  8d442414              lea       eax,[esp+14h]
  004f6747  8d4c2418              lea       ecx,[esp+18h]
  004f674b  8d542410              lea       edx,[esp+10h]
  004f674f  53                    push      ebx
  004f6750  55                    push      ebp
  004f6751  56                    push      esi
  004f6752  8b742430              mov       esi,[esp+30h]
  004f6756  57                    push      edi
  004f6757  50                    push      eax
  004f6758  51                    push      ecx
  004f6759  52                    push      edx
  004f675a  56                    push      esi
  004f675b  e820220400            call      00538980h
  004f6760  83c410                add       esp,10h
  004f6763  8d44241c              lea       eax,[esp+1Ch]
  004f6767  8d4c2414              lea       ecx,[esp+14h]
  004f676b  8d542434              lea       edx,[esp+34h]
  004f676f  50                    push      eax
  004f6770  51                    push      ecx
  004f6771  8d442418              lea       eax,[esp+18h]
  004f6775  52                    push      edx
  004f6776  8d4c2438              lea       ecx,[esp+38h]
  004f677a  50                    push      eax
  004f677b  51                    push      ecx
  004f677c  56                    push      esi
  004f677d  e89ef70300            call      00535F20h
  004f6782  8b44242c              mov       eax,[esp+2Ch]
  004f6786  33ed                  xor       ebp,ebp
  004f6788  83c418                add       esp,18h
  004f678b  3bc5                  cmp       eax,ebp
  004f678d  bb01000000            mov       ebx,1
  004f6792  0f84ec030000          je        near 004F6B84h
  004f6798  56                    push      esi
  004f6799  e8024efeff            call      004DB5A0h
  004f679e  8b06                  mov       eax,[esi]
  004f67a0  83c404                add       esp,4
  004f67a3  a802                  test      al,2
  004f67a5  0f84ed010000          je        near 004F6998h
  004f67ab  f644241c10            test      byte [esp+1Ch],10h
  004f67b0  740d                  je        short 004F67BFh
  004f67b2  24fd                  and       al,0FDh
  004f67b4  c6464300              mov       byte [esi+43h],0
  004f67b8  8906                  mov       [esi],eax
  004f67ba  e9c5030000            jmp       004F6B84h

LAB_004f67bf:
  004f67bf  33c9                  xor       ecx,ecx
  004f67c1  8a4e42                mov       cl,[esi+42h]
  004f67c4  2bcd                  sub       ecx,ebp
  004f67c6  0f8481010000          je        near 004F694Dh
  004f67cc  49                    dec       ecx
  004f67cd  0f84b0000000          je        near 004F6883h
  004f67d3  49                    dec       ecx
  004f67d4  0f85aa030000          jne       near 004F6B84h
  004f67da  0fbf4e10              movsx     ecx,word [esi+10h]
  004f67de  2bcd                  sub       ecx,ebp
  004f67e0  0f8451010000          je        near 004F6937h
  004f67e6  49                    dec       ecx
  004f67e7  0f85a3010000          jne       near 004F6990h
  004f67ed  a900000002            test      eax,2000000h
  004f67f2  0f8498010000          je        near 004F6990h
  004f67f8  e843090500            call      00547140h
  004f67fd  99                    cdq
  004f67fe  b903000000            mov       ecx,3
  004f6803  f7f9                  idiv      ecx
  004f6805  2bd5                  sub       edx,ebp
  004f6807  7414                  je        short 004F681Dh
  004f6809  4a                    dec       edx
  004f680a  740a                  je        short 004F6816h
  004f680c  4a                    dec       edx
  004f680d  7515                  jne       short 004F6824h
  004f680f  bf11000000            mov       edi,11h
  004f6814  eb12                  jmp       short 004F6828h

LAB_004f6816:
  004f6816  bf0c000000            mov       edi,0Ch
  004f681b  eb0b                  jmp       short 004F6828h

LAB_004f681d:
  004f681d  bf10000000            mov       edi,10h
  004f6822  eb04                  jmp       short 004F6828h

LAB_004f6824:
  004f6824  8b7c2418              mov       edi,[esp+18h]

LAB_004f6828:
  004f6828  55                    push      ebp
  004f6829  56                    push      esi
  004f682a  e841ef0300            call      00535770h
  004f682f  0fbf10                movsx     edx,word [eax]
  004f6832  c1e20c                shl       edx,0Ch
  004f6835  895644                mov       [esi+44h],edx
  004f6838  83c408                add       esp,8
  004f683b  0fbf4802              movsx     ecx,word [eax+2]
  004f683f  c1e10c                shl       ecx,0Ch
  004f6842  894e48                mov       [esi+48h],ecx
  004f6845  6a03                  push      3
  004f6847  0fbf5004              movsx     edx,word [eax+4]
  004f684b  c1e20c                shl       edx,0Ch
  004f684e  89564c                mov       [esi+4Ch],edx
  004f6851  a1cc975e00            mov       eax,[5E97CCh]
  004f6856  55                    push      ebp
  004f6857  8b88cc000000          mov       ecx,[eax+0CCh]
  004f685d  33c0                  xor       eax,eax
  004f685f  8b11                  mov       edx,[ecx]
  004f6861  8a043a                mov       al,[edx+edi]
  004f6864  05c0000000            add       eax,0C0h
  004f6869  50                    push      eax
  004f686a  56                    push      esi
  004f686b  e86041f8ff            call      0047A9D0h
  004f6870  83c410                add       esp,10h
  004f6873  66ff4e10              dec       word [esi+10h]
  004f6877  c646430a              mov       byte [esi+43h],0Ah
  004f687b  896e5c                mov       [esi+5Ch],ebp
  004f687e  e901030000            jmp       004F6B84h

LAB_004f6883:
  004f6883  0fbf4e10              movsx     ecx,word [esi+10h]
  004f6887  2bcd                  sub       ecx,ebp
  004f6889  0f84a8000000          je        near 004F6937h
  004f688f  49                    dec       ecx
  004f6890  0f85fa000000          jne       near 004F6990h
  004f6896  a900000002            test      eax,2000000h
  004f689b  0f84ef000000          je        near 004F6990h
  004f68a1  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  004f68a8  8b7e50                mov       edi,[esi+50h]
  004f68ab  8b6e54                mov       ebp,[esi+54h]
  004f68ae  8b5e58                mov       ebx,[esi+58h]
  004f68b1  6a02                  push      2
  004f68b3  56                    push      esi
  004f68b4  894c2420              mov       [esp+20h],ecx
  004f68b8  66c7869a0000000200    mov       word [esi+9Ah],2
  004f68c1  e8aaee0300            call      00535770h
  004f68c6  0fbf10                movsx     edx,word [eax]
  004f68c9  c1e20c                shl       edx,0Ch
  004f68cc  895650                mov       [esi+50h],edx
  004f68cf  83c408                add       esp,8
  004f68d2  0fbf4802              movsx     ecx,word [eax+2]
  004f68d6  c1e10c                shl       ecx,0Ch
  004f68d9  894e54                mov       [esi+54h],ecx
  004f68dc  6aff                  push      0FFFFFFFFh
  004f68de  0fbf5004              movsx     edx,word [eax+4]
  004f68e2  c1e20c                shl       edx,0Ch
  004f68e5  895658                mov       [esi+58h],edx
  004f68e8  a1cc975e00            mov       eax,[5E97CCh]
  004f68ed  6aff                  push      0FFFFFFFFh
  004f68ef  8b88cc000000          mov       ecx,[eax+0CCh]
  004f68f5  33c0                  xor       eax,eax
  004f68f7  8b11                  mov       edx,[ecx]
  004f68f9  8a420b                mov       al,[edx+0Bh]
  004f68fc  05c0000000            add       eax,0C0h
  004f6901  50                    push      eax
  004f6902  56                    push      esi
  004f6903  e8c840f8ff            call      0047A9D0h
  004f6908  668b4c2428            mov       cx,[esp+28h]
  004f690d  83c410                add       esp,10h
  004f6910  66ff4e10              dec       word [esi+10h]
  004f6914  896e54                mov       [esi+54h],ebp
  004f6917  33ed                  xor       ebp,ebp
  004f6919  895e58                mov       [esi+58h],ebx
  004f691c  897e50                mov       [esi+50h],edi
  004f691f  66898e9a000000        mov       [esi+9Ah],cx
  004f6926  c6464328              mov       byte [esi+43h],28h
  004f692a  bb01000000            mov       ebx,1
  004f692f  896e5c                mov       [esi+5Ch],ebp
  004f6932  e94d020000            jmp       004F6B84h

LAB_004f6937:
  004f6937  8a4e43                mov       cl,[esi+43h]
  004f693a  fec9                  dec       cl
  004f693c  884e43                mov       [esi+43h],cl
  004f693f  754f                  jne       short 004F6990h
  004f6941  24fd                  and       al,0FDh
  004f6943  896e5c                mov       [esi+5Ch],ebp
  004f6946  8906                  mov       [esi],eax
  004f6948  e937020000            jmp       004F6B84h

LAB_004f694d:
  004f694d  668b461a              mov       ax,[esi+1Ah]
  004f6951  663d0700              cmp       ax,7
  004f6955  7406                  je        short 004F695Dh
  004f6957  663d1600              cmp       ax,16h
  004f695b  7526                  jne       short 004F6983h

LAB_004f695d:
  004f695d  8b15cc975e00          mov       edx,[5E97CCh]
  004f6963  6a04                  push      4
  004f6965  55                    push      ebp
  004f6966  8b82cc000000          mov       eax,[edx+0CCh]
  004f696c  33d2                  xor       edx,edx
  004f696e  8b08                  mov       ecx,[eax]
  004f6970  8a5119                mov       dl,[ecx+19h]
  004f6973  81c2c0000000          add       edx,0C0h
  004f6979  52                    push      edx
  004f697a  56                    push      esi
  004f697b  e85040f8ff            call      0047A9D0h
  004f6980  83c410                add       esp,10h

LAB_004f6983:
  004f6983  8b06                  mov       eax,[esi]
  004f6985  a900000002            test      eax,2000000h
  004f698a  7404                  je        short 004F6990h
  004f698c  24fd                  and       al,0FDh
  004f698e  8906                  mov       [esi],eax

LAB_004f6990:
  004f6990  896e5c                mov       [esi+5Ch],ebp
  004f6993  e9ec010000            jmp       004F6B84h

LAB_004f6998:
  004f6998  a811                  test      al,11h
  004f699a  0f85e4010000          jne       near 004F6B84h
  004f69a0  a904080000            test      eax,804h
  004f69a5  0f85d9010000          jne       near 004F6B84h
  004f69ab  33c9                  xor       ecx,ecx
  004f69ad  8a4e42                mov       cl,[esi+42h]
  004f69b0  83f903                cmp       ecx,3
  004f69b3  0f879e010000          ja        near 004F6B57h
  004f69b9  ff248d446e4f00        jmp       dword [ecx*4+4F6E44h]
  004f69c0  66396e18              cmp       [esi+18h],bp
  004f69c4  740a                  je        short 004F69D0h
  004f69c6  0d00000001            or        eax,1000000h
  004f69cb  896e18                mov       [esi+18h],ebp
  004f69ce  8906                  mov       [esi],eax

LAB_004f69d0:
  004f69d0  817c24102c010000      cmp       dword [esp+10h],12Ch
  004f69d8  7d09                  jge       short 004F69E3h
  004f69da  c6464300              mov       byte [esi+43h],0
  004f69de  e974010000            jmp       004F6B57h

LAB_004f69e3:
  004f69e3  8a4643                mov       al,[esi+43h]
  004f69e6  84c0                  test      al,al
  004f69e8  7464                  je        short 004F6A4Eh
  004f69ea  fec8                  dec       al
  004f69ec  896e5c                mov       [esi+5Ch],ebp
  004f69ef  884643                mov       [esi+43h],al
  004f69f2  e960010000            jmp       004F6B57h
  004f69f7  66395e18              cmp       [esi+18h],bx
  004f69fb  740a                  je        short 004F6A07h
  004f69fd  0d00000001            or        eax,1000000h
  004f6a02  895e18                mov       [esi+18h],ebx
  004f6a05  8906                  mov       [esi],eax

LAB_004f6a07:
  004f6a07  8b442434              mov       eax,[esp+34h]
  004f6a0b  8d8880000000          lea       ecx,[eax+80h]
  004f6a11  81e1ff0f0000          and       ecx,0FFFh
  004f6a17  81f900010000          cmp       ecx,100h
  004f6a1d  7f10                  jg        short 004F6A2Fh
  004f6a1f  66014632              add       [esi+32h],ax
  004f6a23  c6464300              mov       byte [esi+43h],0
  004f6a27  896e5c                mov       [esi+5Ch],ebp
  004f6a2a  e928010000            jmp       004F6B57h

LAB_004f6a2f:
  004f6a2f  25ff0f0000            and       eax,0FFFh
  004f6a34  33d2                  xor       edx,edx
  004f6a36  3d00080000            cmp       eax,800h
  004f6a3b  0f9dc2                setge     dl
  004f6a3e  4a                    dec       edx
  004f6a3f  81e200010000          and       edx,100h
  004f6a45  83c280                add       edx,0FFFFFF80h
  004f6a48  8bc2                  mov       eax,edx
  004f6a4a  66014632              add       [esi+32h],ax

LAB_004f6a4e:
  004f6a4e  896e5c                mov       [esi+5Ch],ebp
  004f6a51  e901010000            jmp       004F6B57h
  004f6a56  66395e18              cmp       [esi+18h],bx
  004f6a5a  740a                  je        short 004F6A66h
  004f6a5c  0d00000001            or        eax,1000000h
  004f6a61  895e18                mov       [esi+18h],ebx
  004f6a64  8906                  mov       [esi],eax

LAB_004f6a66:
  004f6a66  0fbf4610              movsx     eax,word [esi+10h]
  004f6a6a  39442410              cmp       [esp+10h],eax
  004f6a6e  7d09                  jge       short 004F6A79h
  004f6a70  c6464300              mov       byte [esi+43h],0
  004f6a74  e9de000000            jmp       004F6B57h

LAB_004f6a79:
  004f6a79  8a4643                mov       al,[esi+43h]
  004f6a7c  84c0                  test      al,al
  004f6a7e  7405                  je        short 004F6A85h
  004f6a80  fec8                  dec       al
  004f6a82  884643                mov       [esi+43h],al

LAB_004f6a85:
  004f6a85  8b442434              mov       eax,[esp+34h]
  004f6a89  8d8880000000          lea       ecx,[eax+80h]
  004f6a8f  81e1ff0f0000          and       ecx,0FFFh
  004f6a95  81f900010000          cmp       ecx,100h
  004f6a9b  7e1b                  jle       short 004F6AB8h
  004f6a9d  25ff0f0000            and       eax,0FFFh
  004f6aa2  33d2                  xor       edx,edx
  004f6aa4  3d00080000            cmp       eax,800h
  004f6aa9  0f9dc2                setge     dl
  004f6aac  4a                    dec       edx
  004f6aad  81e200010000          and       edx,100h
  004f6ab3  83c280                add       edx,0FFFFFF80h
  004f6ab6  8bc2                  mov       eax,edx

LAB_004f6ab8:
  004f6ab8  66014632              add       [esi+32h],ax
  004f6abc  c7465c00200100        mov       dword [esi+5Ch],12000h
  004f6ac3  e98f000000            jmp       004F6B57h
  004f6ac8  8a4e43                mov       cl,[esi+43h]
  004f6acb  84c9                  test      cl,cl
  004f6acd  7405                  je        short 004F6AD4h
  004f6acf  fec9                  dec       cl
  004f6ad1  884e43                mov       [esi+43h],cl

LAB_004f6ad4:
  004f6ad4  66395e18              cmp       [esi+18h],bx
  004f6ad8  740a                  je        short 004F6AE4h
  004f6ada  0d00000001            or        eax,1000000h
  004f6adf  895e18                mov       [esi+18h],ebx
  004f6ae2  8906                  mov       [esi],eax

LAB_004f6ae4:
  004f6ae4  0fbf4610              movsx     eax,word [esi+10h]
  004f6ae8  50                    push      eax
  004f6ae9  c7465c00e00000        mov       dword [esi+5Ch],0E000h
  004f6af0  e827ef0400            call      00545A1Ch
  004f6af5  0faf465c              imul      eax,[esi+5Ch]
  004f6af9  8b4e1c                mov       ecx,[esi+1Ch]
  004f6afc  c1f80c                sar       eax,0Ch
  004f6aff  03c8                  add       ecx,eax
  004f6b01  894e1c                mov       [esi+1Ch],ecx
  004f6b04  0fbf4e10              movsx     ecx,word [esi+10h]
  004f6b08  51                    push      ecx
  004f6b09  e808ef0400            call      00545A16h
  004f6b0e  0faf465c              imul      eax,[esi+5Ch]
  004f6b12  8b4e24                mov       ecx,[esi+24h]
  004f6b15  896e5c                mov       [esi+5Ch],ebp
  004f6b18  c1f80c                sar       eax,0Ch
  004f6b1b  03c8                  add       ecx,eax
  004f6b1d  8b442434              mov       eax,[esp+34h]
  004f6b21  894e24                mov       [esi+24h],ecx
  004f6b24  8d9080000000          lea       edx,[eax+80h]
  004f6b2a  81e2ff0f0000          and       edx,0FFFh
  004f6b30  81fa00010000          cmp       edx,100h
  004f6b36  7e1b                  jle       short 004F6B53h
  004f6b38  25ff0f0000            and       eax,0FFFh
  004f6b3d  33c9                  xor       ecx,ecx
  004f6b3f  3d00080000            cmp       eax,800h
  004f6b44  0f9dc1                setge     cl
  004f6b47  49                    dec       ecx
  004f6b48  81e100010000          and       ecx,100h
  004f6b4e  83c180                add       ecx,0FFFFFF80h
  004f6b51  8bc1                  mov       eax,ecx

LAB_004f6b53:
  004f6b53  66014632              add       [esi+32h],ax

LAB_004f6b57:
  004f6b57  66395e18              cmp       [esi+18h],bx
  004f6b5b  7527                  jne       short 004F6B84h
  004f6b5d  668b461a              mov       ax,[esi+1Ah]
  004f6b61  663bc3                cmp       ax,bx
  004f6b64  7406                  je        short 004F6B6Ch
  004f6b66  663d1400              cmp       ax,14h
  004f6b6a  7518                  jne       short 004F6B84h

LAB_004f6b6c:
  004f6b6c  8d561c                lea       edx,[esi+1Ch]
  004f6b6f  52                    push      edx
  004f6b70  6800180000            push      1800h
  004f6b75  6800020000            push      200h
  004f6b7a  6a42                  push      42h
  004f6b7c  e89f6f0400            call      0053DB20h
  004f6b81  83c410                add       esp,10h

LAB_004f6b84:
  004f6b84  8b442414              mov       eax,[esp+14h]
  004f6b88  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  004f6b91  50                    push      eax
  004f6b92  56                    push      esi
  004f6b93  e8c815f9ff            call      00488160h
  004f6b98  83c408                add       esp,8
  004f6b9b  56                    push      esi
  004f6b9c  e89fcd0300            call      00533940h
  004f6ba1  83c404                add       esp,4
  004f6ba4  56                    push      esi
  004f6ba5  e8468ffeff            call      004DFAF0h
  004f6baa  8b4c2424              mov       ecx,[esp+24h]
  004f6bae  83c404                add       esp,4
  004f6bb1  51                    push      ecx
  004f6bb2  56                    push      esi
  004f6bb3  e8b8cbfeff            call      004E3770h
  004f6bb8  8b44241c              mov       eax,[esp+1Ch]
  004f6bbc  83c408                add       esp,8
  004f6bbf  3bc5                  cmp       eax,ebp
  004f6bc1  0f8472020000          je        near 004F6E39h
  004f6bc7  f70617080000          test      dword [esi],817h
  004f6bcd  0f8566020000          jne       near 004F6E39h
  004f6bd3  8a4643                mov       al,[esi+43h]
  004f6bd6  84c0                  test      al,al
  004f6bd8  0f855b020000          jne       near 004F6E39h
  004f6bde  8b466c                mov       eax,[esi+6Ch]
  004f6be1  8b561c                mov       edx,[esi+1Ch]
  004f6be4  8b781c                mov       edi,[eax+1Ch]
  004f6be7  8b6824                mov       ebp,[eax+24h]
  004f6bea  8b4624                mov       eax,[esi+24h]
  004f6bed  2bfa                  sub       edi,edx
  004f6bef  2be8                  sub       ebp,eax
  004f6bf1  c1ff0c                sar       edi,0Ch
  004f6bf4  c1fd0c                sar       ebp,0Ch
  004f6bf7  8bd7                  mov       edx,edi
  004f6bf9  8bc5                  mov       eax,ebp
  004f6bfb  0fafd7                imul      edx,edi
  004f6bfe  0fafc5                imul      eax,ebp
  004f6c01  03d0                  add       edx,eax
  004f6c03  52                    push      edx
  004f6c04  e807ee0400            call      00545A10h
  004f6c09  55                    push      ebp
  004f6c0a  57                    push      edi
  004f6c0b  89442418              mov       [esp+18h],eax
  004f6c0f  e80eee0400            call      00545A22h
  004f6c14  668b4e32              mov       cx,[esi+32h]
  004f6c18  2bc1                  sub       eax,ecx
  004f6c1a  25ff0f0000            and       eax,0FFFh
  004f6c1f  89442434              mov       [esp+34h],eax
  004f6c23  e818050500            call      00547140h
  004f6c28  bf64000000            mov       edi,64h
  004f6c2d  99                    cdq
  004f6c2e  8bcf                  mov       ecx,edi
  004f6c30  f7f9                  idiv      ecx
  004f6c32  817c24102c010000      cmp       dword [esp+10h],12Ch
  004f6c3a  0f8d01010000          jge       near 004F6D41h
  004f6c40  83fa32                cmp       edx,32h
  004f6c43  7d3a                  jge       short 004F6C7Fh
  004f6c45  8b542434              mov       edx,[esp+34h]
  004f6c49  81c200010000          add       edx,100h
  004f6c4f  81e2ff0f0000          and       edx,0FFFh
  004f6c55  81fa00020000          cmp       edx,200h
  004f6c5b  0f8f47010000          jg        near 004F6DA8h
  004f6c61  8b06                  mov       eax,[esi]
  004f6c63  c7461803000000        mov       dword [esi+18h],3
  004f6c6a  25fffffffe            and       eax,0FEFFFFFFh
  004f6c6f  c6464200              mov       byte [esi+42h],0
  004f6c73  0c02                  or        al,2
  004f6c75  8906                  mov       [esi],eax
  004f6c77  5f                    pop       edi
  004f6c78  5e                    pop       esi
  004f6c79  5d                    pop       ebp
  004f6c7a  5b                    pop       ebx
  004f6c7b  83c420                add       esp,20h
  004f6c7e  c3                    ret

LAB_004f6c7f:
  004f6c7f  83fa4b                cmp       edx,4Bh
  004f6c82  7d22                  jge       short 004F6CA6h
  004f6c84  c6464202              mov       byte [esi+42h],2
  004f6c88  e8b3040500            call      00547140h
  004f6c8d  99                    cdq
  004f6c8e  b90a000000            mov       ecx,0Ah
  004f6c93  66897e10              mov       [esi+10h],di
  004f6c97  f7f9                  idiv      ecx
  004f6c99  02d1                  add       dl,cl
  004f6c9b  885643                mov       [esi+43h],dl
  004f6c9e  5f                    pop       edi
  004f6c9f  5e                    pop       esi
  004f6ca0  5d                    pop       ebp
  004f6ca1  5b                    pop       ebx
  004f6ca2  83c420                add       esp,20h
  004f6ca5  c3                    ret

LAB_004f6ca6:
  004f6ca6  83fa5a                cmp       edx,5Ah
  004f6ca9  7d57                  jge       short 004F6D02h
  004f6cab  8b542434              mov       edx,[esp+34h]
  004f6caf  81c200010000          add       edx,100h
  004f6cb5  81e2ff0f0000          and       edx,0FFFh
  004f6cbb  81fa00020000          cmp       edx,200h
  004f6cc1  7f3f                  jg        short 004F6D02h
  004f6cc3  c6464203              mov       byte [esi+42h],3
  004f6cc7  e874040500            call      00547140h
  004f6ccc  99                    cdq
  004f6ccd  b90a000000            mov       ecx,0Ah
  004f6cd2  f7f9                  idiv      ecx
  004f6cd4  02d1                  add       dl,cl
  004f6cd6  885643                mov       [esi+43h],dl
  004f6cd9  e862040500            call      00547140h
  004f6cde  99                    cdq
  004f6cdf  b9000c0000            mov       ecx,0C00h
  004f6ce4  8b7c2434              mov       edi,[esp+34h]
  004f6ce8  f7f9                  idiv      ecx
  004f6cea  03d7                  add       edx,edi
  004f6cec  66035632              add       dx,[esi+32h]
  004f6cf0  81c200020000          add       edx,200h
  004f6cf6  66895610              mov       [esi+10h],dx
  004f6cfa  5f                    pop       edi
  004f6cfb  5e                    pop       esi
  004f6cfc  5d                    pop       ebp
  004f6cfd  5b                    pop       ebx
  004f6cfe  83c420                add       esp,20h
  004f6d01  c3                    ret

LAB_004f6d02:
  004f6d02  c6464203              mov       byte [esi+42h],3
  004f6d06  e835040500            call      00547140h
  004f6d0b  99                    cdq
  004f6d0c  b90a000000            mov       ecx,0Ah
  004f6d11  f7f9                  idiv      ecx
  004f6d13  02d1                  add       dl,cl
  004f6d15  885643                mov       [esi+43h],dl
  004f6d18  e823040500            call      00547140h
  004f6d1d  99                    cdq
  004f6d1e  b9000c0000            mov       ecx,0C00h
  004f6d23  8b7c2434              mov       edi,[esp+34h]
  004f6d27  f7f9                  idiv      ecx
  004f6d29  03d7                  add       edx,edi
  004f6d2b  66035632              add       dx,[esi+32h]
  004f6d2f  81c200020000          add       edx,200h
  004f6d35  66895610              mov       [esi+10h],dx
  004f6d39  5f                    pop       edi
  004f6d3a  5e                    pop       esi
  004f6d3b  5d                    pop       ebp
  004f6d3c  5b                    pop       ebx
  004f6d3d  83c420                add       esp,20h
  004f6d40  c3                    ret

LAB_004f6d41:
  004f6d41  83fa2d                cmp       edx,2Dh
  004f6d44  7d72                  jge       short 004F6DB8h
  004f6d46  8b442434              mov       eax,[esp+34h]
  004f6d4a  0500020000            add       eax,200h
  004f6d4f  25ff0f0000            and       eax,0FFFh
  004f6d54  3d00040000            cmp       eax,400h
  004f6d59  7f4d                  jg        short 004F6DA8h
  004f6d5b  f6c201                test      dl,1
  004f6d5e  7424                  je        short 004F6D84h
  004f6d60  8b0e                  mov       ecx,[esi]
  004f6d62  c7461805000000        mov       dword [esi+18h],5
  004f6d69  81e1fffffffe          and       ecx,0FEFFFFFFh
  004f6d6f  c6464201              mov       byte [esi+42h],1
  004f6d73  83c902                or        ecx,2
  004f6d76  66895e10              mov       [esi+10h],bx
  004f6d7a  890e                  mov       [esi],ecx
  004f6d7c  5f                    pop       edi
  004f6d7d  5e                    pop       esi
  004f6d7e  5d                    pop       ebp
  004f6d7f  5b                    pop       ebx
  004f6d80  83c420                add       esp,20h
  004f6d83  c3                    ret

LAB_004f6d84:
  004f6d84  8b16                  mov       edx,[esi]
  004f6d86  c7461804000000        mov       dword [esi+18h],4
  004f6d8d  81e2fffffffe          and       edx,0FEFFFFFFh
  004f6d93  c6464202              mov       byte [esi+42h],2
  004f6d97  83ca02                or        edx,2
  004f6d9a  66895e10              mov       [esi+10h],bx
  004f6d9e  8916                  mov       [esi],edx
  004f6da0  5f                    pop       edi
  004f6da1  5e                    pop       esi
  004f6da2  5d                    pop       ebp
  004f6da3  5b                    pop       ebx
  004f6da4  83c420                add       esp,20h
  004f6da7  c3                    ret

LAB_004f6da8:
  004f6da8  c6464201              mov       byte [esi+42h],1
  004f6dac  c6464301              mov       byte [esi+43h],1
  004f6db0  5f                    pop       edi
  004f6db1  5e                    pop       esi
  004f6db2  5d                    pop       ebp
  004f6db3  5b                    pop       ebx
  004f6db4  83c420                add       esp,20h
  004f6db7  c3                    ret

LAB_004f6db8:
  004f6db8  83fa46                cmp       edx,46h
  004f6dbb  7d3f                  jge       short 004F6DFCh
  004f6dbd  c6464203              mov       byte [esi+42h],3
  004f6dc1  e87a030500            call      00547140h
  004f6dc6  99                    cdq
  004f6dc7  b90a000000            mov       ecx,0Ah
  004f6dcc  f7f9                  idiv      ecx
  004f6dce  02d1                  add       dl,cl
  004f6dd0  885643                mov       [esi+43h],dl
  004f6dd3  e868030500            call      00547140h
  004f6dd8  99                    cdq
  004f6dd9  b9000c0000            mov       ecx,0C00h
  004f6dde  8b7c2434              mov       edi,[esp+34h]
  004f6de2  f7f9                  idiv      ecx
  004f6de4  03d7                  add       edx,edi
  004f6de6  66035632              add       dx,[esi+32h]
  004f6dea  81ea00060000          sub       edx,600h
  004f6df0  66895610              mov       [esi+10h],dx
  004f6df4  5f                    pop       edi
  004f6df5  5e                    pop       esi
  004f6df6  5d                    pop       ebp
  004f6df7  5b                    pop       ebx
  004f6df8  83c420                add       esp,20h
  004f6dfb  c3                    ret

LAB_004f6dfc:
  004f6dfc  83fa5a                cmp       edx,5Ah
  004f6dff  7d22                  jge       short 004F6E23h
  004f6e01  c6464202              mov       byte [esi+42h],2
  004f6e05  e836030500            call      00547140h
  004f6e0a  99                    cdq
  004f6e0b  b90a000000            mov       ecx,0Ah
  004f6e10  66897e10              mov       [esi+10h],di
  004f6e14  f7f9                  idiv      ecx
  004f6e16  02d1                  add       dl,cl
  004f6e18  885643                mov       [esi+43h],dl
  004f6e1b  5f                    pop       edi
  004f6e1c  5e                    pop       esi
  004f6e1d  5d                    pop       ebp
  004f6e1e  5b                    pop       ebx
  004f6e1f  83c420                add       esp,20h
  004f6e22  c3                    ret

LAB_004f6e23:
  004f6e23  c6464200              mov       byte [esi+42h],0
  004f6e27  e814030500            call      00547140h
  004f6e2c  99                    cdq
  004f6e2d  b90a000000            mov       ecx,0Ah
  004f6e32  f7f9                  idiv      ecx
  004f6e34  02d1                  add       dl,cl
  004f6e36  885643                mov       [esi+43h],dl

LAB_004f6e39:
  004f6e39  5f                    pop       edi
  004f6e3a  5e                    pop       esi
  004f6e3b  5d                    pop       ebp
  004f6e3c  5b                    pop       ebx
  004f6e3d  83c420                add       esp,20h
  004f6e40  c3                    ret
  004f6e41  90                    nop
  004f6e42  8bff                  mov       edi,edi
  004f6e44  c0694f00              shr       byte [ecx+4Fh],0
  004f6e48  f7694f                imul      dword [ecx+4Fh]
  004f6e4b  00566a                add       [esi+6Ah],dl
  004f6e4e  4f                    dec       edi
  004f6e4f  00c8                  add       al,cl
  004f6e51  6a4f                  push      4Fh
  004f6e53  009090909090          add       [eax-6F6F6F70h],dl
  004f6e59  90                    nop
  004f6e5a  90                    nop
  004f6e5b  90                    nop
  004f6e5c  90                    nop
  004f6e5d  90                    nop
  004f6e5e  90                    nop
  004f6e5f  90                    nop

37 cible(s) de saut interne : 0x004f67bf, 0x004f6816, 0x004f681d, 0x004f6824, 0x004f6828, 0x004f6883, 0x004f6937, 0x004f694d, 0x004f695d, 0x004f6983, 0x004f6990, 0x004f6998, 0x004f69d0, 0x004f69e3, 0x004f6a07, 0x004f6a2f, 0x004f6a4e, 0x004f6a66, 0x004f6a79, 0x004f6a85, 0x004f6ab8, 0x004f6ad4, 0x004f6ae4, 0x004f6b53, 0x004f6b57, 0x004f6b6c, 0x004f6b84, 0x004f6c7f, 0x004f6ca6, 0x004f6d02, 0x004f6d41, 0x004f6d84, 0x004f6da8, 0x004f6db8, 0x004f6dfc, 0x004f6e23, 0x004f6e39
