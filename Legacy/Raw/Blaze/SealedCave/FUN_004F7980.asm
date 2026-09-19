Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004f7980  section .text  offset fichier 0xf6d80  2288 octets

  004f7980  83ec24                sub       esp,24h
  004f7983  8d44241c              lea       eax,[esp+1Ch]
  004f7987  8d4c2420              lea       ecx,[esp+20h]
  004f798b  8d542418              lea       edx,[esp+18h]
  004f798f  53                    push      ebx
  004f7990  55                    push      ebp
  004f7991  56                    push      esi
  004f7992  8b742434              mov       esi,[esp+34h]
  004f7996  57                    push      edi
  004f7997  50                    push      eax
  004f7998  51                    push      ecx
  004f7999  52                    push      edx
  004f799a  56                    push      esi
  004f799b  e8e00f0400            call      00538980h
  004f79a0  83c410                add       esp,10h
  004f79a3  8d44241c              lea       eax,[esp+1Ch]
  004f79a7  8d4c2414              lea       ecx,[esp+14h]
  004f79ab  8d542438              lea       edx,[esp+38h]
  004f79af  50                    push      eax
  004f79b0  51                    push      ecx
  004f79b1  8d442418              lea       eax,[esp+18h]
  004f79b5  52                    push      edx
  004f79b6  8d4c2424              lea       ecx,[esp+24h]
  004f79ba  50                    push      eax
  004f79bb  51                    push      ecx
  004f79bc  56                    push      esi
  004f79bd  e85ee50300            call      00535F20h
  004f79c2  8b44242c              mov       eax,[esp+2Ch]
  004f79c6  83c418                add       esp,18h
  004f79c9  85c0                  test      eax,eax
  004f79cb  bd01000000            mov       ebp,1
  004f79d0  bf05000000            mov       edi,5
  004f79d5  0f8414060000          je        near 004F7FEFh
  004f79db  56                    push      esi
  004f79dc  e8bf3bfeff            call      004DB5A0h
  004f79e1  8b06                  mov       eax,[esi]
  004f79e3  83c404                add       esp,4
  004f79e6  a802                  test      al,2
  004f79e8  0f8409040000          je        near 004F7DF7h
  004f79ee  8a4c241c              mov       cl,[esp+1Ch]
  004f79f2  bb10000000            mov       ebx,10h
  004f79f7  84cb                  test      bl,cl
  004f79f9  740d                  je        short 004F7A08h
  004f79fb  24fd                  and       al,0FDh
  004f79fd  c6464300              mov       byte [esi+43h],0
  004f7a01  8906                  mov       [esi],eax
  004f7a03  e9e7050000            jmp       004F7FEFh

LAB_004f7a08:
  004f7a08  33c9                  xor       ecx,ecx
  004f7a0a  8a4e42                mov       cl,[esi+42h]
  004f7a0d  83e900                sub       ecx,0
  004f7a10  0f8425020000          je        near 004F7C3Bh
  004f7a16  49                    dec       ecx
  004f7a17  0f8482010000          je        near 004F7B9Fh
  004f7a1d  49                    dec       ecx
  004f7a1e  0f85cb050000          jne       near 004F7FEFh
  004f7a24  668b5610              mov       dx,[esi+10h]
  004f7a28  0fbfca                movsx     ecx,dx
  004f7a2b  83e900                sub       ecx,0
  004f7a2e  0f84ed010000          je        near 004F7C21h
  004f7a34  49                    dec       ecx
  004f7a35  0f85f4010000          jne       near 004F7C2Fh
  004f7a3b  a900000002            test      eax,2000000h
  004f7a40  7409                  je        short 004F7A4Bh
  004f7a42  4a                    dec       edx
  004f7a43  c6464301              mov       byte [esi+43h],1
  004f7a47  66895610              mov       [esi+10h],dx

LAB_004f7a4b:
  004f7a4b  66395e1a              cmp       [esi+1Ah],bx
  004f7a4f  0f85da010000          jne       near 004F7C2Fh
  004f7a55  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004f7a5c  0fbf6e34              movsx     ebp,word [esi+34h]
  004f7a60  8b5e5c                mov       ebx,[esi+5Ch]
  004f7a63  6a00                  push      0
  004f7a65  56                    push      esi
  004f7a66  66c74634000f          mov       word [esi+34h],0F00h
  004f7a6c  c7465c46000000        mov       dword [esi+5Ch],46h
  004f7a73  66c7869a00000000ff    mov       word [esi+9Ah],0FF00h
  004f7a7c  e8efdc0300            call      00535770h
  004f7a81  0fbf10                movsx     edx,word [eax]
  004f7a84  c1e20c                shl       edx,0Ch
  004f7a87  895644                mov       [esi+44h],edx
  004f7a8a  83c408                add       esp,8
  004f7a8d  0fbf4802              movsx     ecx,word [eax+2]
  004f7a91  c1e10c                shl       ecx,0Ch
  004f7a94  894e48                mov       [esi+48h],ecx
  004f7a97  6a02                  push      2
  004f7a99  0fbf5004              movsx     edx,word [eax+4]
  004f7a9d  c1e20c                shl       edx,0Ch
  004f7aa0  89564c                mov       [esi+4Ch],edx
  004f7aa3  a1cc975e00            mov       eax,[5E97CCh]
  004f7aa8  6a00                  push      0
  004f7aaa  8b88cc000000          mov       ecx,[eax+0CCh]
  004f7ab0  33c0                  xor       eax,eax
  004f7ab2  8b11                  mov       edx,[ecx]
  004f7ab4  8a420d                mov       al,[edx+0Dh]
  004f7ab7  05c0000000            add       eax,0C0h
  004f7abc  50                    push      eax
  004f7abd  56                    push      esi
  004f7abe  e80d2ff8ff            call      0047A9D0h
  004f7ac3  83c410                add       esp,10h
  004f7ac6  66c7869a0000000000    mov       word [esi+9Ah],0
  004f7acf  6a01                  push      1
  004f7ad1  56                    push      esi
  004f7ad2  e899dc0300            call      00535770h
  004f7ad7  0fbf08                movsx     ecx,word [eax]
  004f7ada  c1e10c                shl       ecx,0Ch
  004f7add  894e44                mov       [esi+44h],ecx
  004f7ae0  83c408                add       esp,8
  004f7ae3  0fbf5002              movsx     edx,word [eax+2]
  004f7ae7  c1e20c                shl       edx,0Ch
  004f7aea  895648                mov       [esi+48h],edx
  004f7aed  6a03                  push      3
  004f7aef  0fbf4004              movsx     eax,word [eax+4]
  004f7af3  c1e00c                shl       eax,0Ch
  004f7af6  89464c                mov       [esi+4Ch],eax
  004f7af9  8b0dcc975e00          mov       ecx,[5E97CCh]
  004f7aff  6a00                  push      0
  004f7b01  8b91cc000000          mov       edx,[ecx+0CCh]
  004f7b07  33c9                  xor       ecx,ecx
  004f7b09  8b02                  mov       eax,[edx]
  004f7b0b  8a4815                mov       cl,[eax+15h]
  004f7b0e  81c1c0000000          add       ecx,0C0h
  004f7b14  51                    push      ecx
  004f7b15  56                    push      esi
  004f7b16  e8b52ef8ff            call      0047A9D0h
  004f7b1b  83c410                add       esp,10h
  004f7b1e  66c7869a0000000001    mov       word [esi+9Ah],100h
  004f7b27  6a02                  push      2
  004f7b29  56                    push      esi
  004f7b2a  e841dc0300            call      00535770h
  004f7b2f  0fbf10                movsx     edx,word [eax]
  004f7b32  c1e20c                shl       edx,0Ch
  004f7b35  895644                mov       [esi+44h],edx
  004f7b38  83c408                add       esp,8
  004f7b3b  0fbf4802              movsx     ecx,word [eax+2]
  004f7b3f  c1e10c                shl       ecx,0Ch
  004f7b42  894e48                mov       [esi+48h],ecx
  004f7b45  6a01                  push      1
  004f7b47  0fbf5004              movsx     edx,word [eax+4]
  004f7b4b  c1e20c                shl       edx,0Ch
  004f7b4e  89564c                mov       [esi+4Ch],edx
  004f7b51  a1cc975e00            mov       eax,[5E97CCh]
  004f7b56  6a00                  push      0
  004f7b58  8b88cc000000          mov       ecx,[eax+0CCh]
  004f7b5e  33c0                  xor       eax,eax
  004f7b60  8b11                  mov       edx,[ecx]
  004f7b62  8a4214                mov       al,[edx+14h]
  004f7b65  05c0000000            add       eax,0C0h
  004f7b6a  50                    push      eax
  004f7b6b  56                    push      esi
  004f7b6c  e85f2ef8ff            call      0047A9D0h
  004f7b71  83c410                add       esp,10h
  004f7b74  8d4e1c                lea       ecx,[esi+1Ch]
  004f7b77  6689be9a000000        mov       [esi+9Ah],di
  004f7b7e  66896e34              mov       [esi+34h],bp
  004f7b82  51                    push      ecx
  004f7b83  68802f5500            push      552F80h
  004f7b88  895e5c                mov       [esi+5Ch],ebx
  004f7b8b  e850600400            call      0053DBE0h
  004f7b90  83c408                add       esp,8
  004f7b93  c7465c00000000        mov       dword [esi+5Ch],0
  004f7b9a  e950040000            jmp       004F7FEFh

LAB_004f7b9f:
  004f7b9f  0fbf4e10              movsx     ecx,word [esi+10h]
  004f7ba3  83e900                sub       ecx,0
  004f7ba6  7479                  je        short 004F7C21h
  004f7ba8  49                    dec       ecx
  004f7ba9  0f8580000000          jne       near 004F7C2Fh
  004f7baf  66397e1a              cmp       [esi+1Ah],di
  004f7bb3  7511                  jne       short 004F7BC6h
  004f7bb5  8d561c                lea       edx,[esi+1Ch]
  004f7bb8  52                    push      edx
  004f7bb9  68b02f5500            push      552FB0h
  004f7bbe  e81d600400            call      0053DBE0h
  004f7bc3  83c408                add       esp,8

LAB_004f7bc6:
  004f7bc6  f70600000002          test      dword [esi],2000000h
  004f7bcc  7461                  je        short 004F7C2Fh
  004f7bce  8b7e44                mov       edi,[esi+44h]
  004f7bd1  8b6e48                mov       ebp,[esi+48h]
  004f7bd4  c746440b000000        mov       dword [esi+44h],0Bh
  004f7bdb  c74648204e0000        mov       dword [esi+48h],4E20h
  004f7be2  a1cc975e00            mov       eax,[5E97CCh]
  004f7be7  6a02                  push      2
  004f7be9  6a00                  push      0
  004f7beb  8b88cc000000          mov       ecx,[eax+0CCh]
  004f7bf1  33c0                  xor       eax,eax
  004f7bf3  8b11                  mov       edx,[ecx]
  004f7bf5  8a420e                mov       al,[edx+0Eh]
  004f7bf8  05c0000000            add       eax,0C0h
  004f7bfd  50                    push      eax
  004f7bfe  56                    push      esi
  004f7bff  e8cc2df8ff            call      0047A9D0h
  004f7c04  83c410                add       esp,10h
  004f7c07  66ff4e10              dec       word [esi+10h]
  004f7c0b  897e44                mov       [esi+44h],edi
  004f7c0e  896e48                mov       [esi+48h],ebp
  004f7c11  c646430a              mov       byte [esi+43h],0Ah
  004f7c15  c7465c00000000        mov       dword [esi+5Ch],0
  004f7c1c  e9ce030000            jmp       004F7FEFh

LAB_004f7c21:
  004f7c21  8a4e43                mov       cl,[esi+43h]
  004f7c24  fec9                  dec       cl
  004f7c26  884e43                mov       [esi+43h],cl
  004f7c29  7504                  jne       short 004F7C2Fh
  004f7c2b  24fd                  and       al,0FDh
  004f7c2d  8906                  mov       [esi],eax

LAB_004f7c2f:
  004f7c2f  c7465c00000000        mov       dword [esi+5Ch],0
  004f7c36  e9b4030000            jmp       004F7FEFh

LAB_004f7c3b:
  004f7c3b  668b5610              mov       dx,[esi+10h]
  004f7c3f  0fbfca                movsx     ecx,dx
  004f7c42  83e900                sub       ecx,0
  004f7c45  0f84ba000000          je        near 004F7D05h
  004f7c4b  49                    dec       ecx
  004f7c4c  7461                  je        short 004F7CAFh
  004f7c4e  49                    dec       ecx
  004f7c4f  0f859a030000          jne       near 004F7FEFh
  004f7c55  8b465c                mov       eax,[esi+5Ch]
  004f7c58  8bc8                  mov       ecx,eax
  004f7c5a  c1f902                sar       ecx,2
  004f7c5d  2bc1                  sub       eax,ecx
  004f7c5f  66397e1a              cmp       [esi+1Ah],di
  004f7c63  89465c                mov       [esi+5Ch],eax
  004f7c66  7505                  jne       short 004F7C6Dh
  004f7c68  4a                    dec       edx
  004f7c69  66895610              mov       [esi+10h],dx

LAB_004f7c6d:
  004f7c6d  8b442438              mov       eax,[esp+38h]
  004f7c71  8d5040                lea       edx,[eax+40h]
  004f7c74  81e2ff0f0000          and       edx,0FFFh
  004f7c7a  81fa80000000          cmp       edx,80h
  004f7c80  7f09                  jg        short 004F7C8Bh
  004f7c82  66014632              add       [esi+32h],ax
  004f7c86  e964030000            jmp       004F7FEFh

LAB_004f7c8b:
  004f7c8b  25ff0f0000            and       eax,0FFFh
  004f7c90  33c9                  xor       ecx,ecx
  004f7c92  3d00080000            cmp       eax,800h
  004f7c97  0f9dc1                setge     cl
  004f7c9a  49                    dec       ecx
  004f7c9b  81e180000000          and       ecx,80h
  004f7ca1  83c1c0                add       ecx,0FFFFFFC0h
  004f7ca4  8bc1                  mov       eax,ecx
  004f7ca6  66014632              add       [esi+32h],ax
  004f7caa  e940030000            jmp       004F7FEFh

LAB_004f7caf:
  004f7caf  0fbf5632              movsx     edx,word [esi+32h]
  004f7cb3  52                    push      edx
  004f7cb4  c7465c00c00300        mov       dword [esi+5Ch],3C000h
  004f7cbb  e85cdd0400            call      00545A1Ch
  004f7cc0  0faf465c              imul      eax,[esi+5Ch]
  004f7cc4  8b4e50                mov       ecx,[esi+50h]
  004f7cc7  c1f80c                sar       eax,0Ch
  004f7cca  03c8                  add       ecx,eax
  004f7ccc  0fbf4632              movsx     eax,word [esi+32h]
  004f7cd0  50                    push      eax
  004f7cd1  894e50                mov       [esi+50h],ecx
  004f7cd4  e83ddd0400            call      00545A16h
  004f7cd9  0faf465c              imul      eax,[esi+5Ch]
  004f7cdd  8b5658                mov       edx,[esi+58h]
  004f7ce0  8b0e                  mov       ecx,[esi]
  004f7ce2  c1f80c                sar       eax,0Ch
  004f7ce5  03d0                  add       edx,eax
  004f7ce7  0bcd                  or        ecx,ebp
  004f7ce9  66ff4e10              dec       word [esi+10h]
  004f7ced  895658                mov       [esi+58h],edx
  004f7cf0  c7465400200300        mov       dword [esi+54h],32000h
  004f7cf7  890e                  mov       [esi],ecx
  004f7cf9  c7465c00000000        mov       dword [esi+5Ch],0
  004f7d00  e9ea020000            jmp       004F7FEFh

LAB_004f7d05:
  004f7d05  a900000002            test      eax,2000000h
  004f7d0a  7408                  je        short 004F7D14h
  004f7d0c  a801                  test      al,1
  004f7d0e  7504                  jne       short 004F7D14h
  004f7d10  24fd                  and       al,0FDh
  004f7d12  8906                  mov       [esi],eax

LAB_004f7d14:
  004f7d14  8d6e1c                lea       ebp,[esi+1Ch]
  004f7d17  6a00                  push      0
  004f7d19  68702f5500            push      552F70h
  004f7d1e  55                    push      ebp
  004f7d1f  56                    push      esi
  004f7d20  e80b1f0200            call      00519C30h
  004f7d25  8bf8                  mov       edi,eax
  004f7d27  a1b8e05d00            mov       eax,[5DE0B8h]
  004f7d2c  83c410                add       esp,10h
  004f7d2f  85c0                  test      eax,eax
  004f7d31  0f84b8020000          je        near 004F7FEFh
  004f7d37  8b0f                  mov       ecx,[edi]
  004f7d39  57                    push      edi
  004f7d3a  83c940                or        ecx,40h
  004f7d3d  56                    push      esi
  004f7d3e  66899f96000000        mov       [edi+96h],bx
  004f7d45  890f                  mov       [edi],ecx
  004f7d47  e8341e0400            call      00539B80h
  004f7d4c  83c408                add       esp,8
  004f7d4f  8d471c                lea       eax,[edi+1Ch]
  004f7d52  50                    push      eax
  004f7d53  6800180000            push      1800h
  004f7d58  6800030000            push      300h
  004f7d5d  6a03                  push      3
  004f7d5f  e8bc5d0400            call      0053DB20h
  004f7d64  8b07                  mov       eax,[edi]
  004f7d66  83c410                add       esp,10h
  004f7d69  a900000040            test      eax,40000000h
  004f7d6e  0f857b020000          jne       near 004F7FEFh
  004f7d74  0fbf4e32              movsx     ecx,word [esi+32h]
  004f7d78  51                    push      ecx
  004f7d79  e89edc0400            call      00545A1Ch
  004f7d7e  8d0440                lea       eax,[eax+eax*2]
  004f7d81  8d1480                lea       edx,[eax+eax*4]
  004f7d84  d1e2                  shl       edx,1
  004f7d86  895750                mov       [edi+50h],edx
  004f7d89  0fbf4632              movsx     eax,word [esi+32h]
  004f7d8d  50                    push      eax
  004f7d8e  e883dc0400            call      00545A16h
  004f7d93  8d0440                lea       eax,[eax+eax*2]
  004f7d96  8d0c80                lea       ecx,[eax+eax*4]
  004f7d99  8b07                  mov       eax,[edi]
  004f7d9b  d1e1                  shl       ecx,1
  004f7d9d  a801                  test      al,1
  004f7d9f  894f58                mov       [edi+58h],ecx
  004f7da2  750b                  jne       short 004F7DAFh
  004f7da4  0c01                  or        al,1
  004f7da6  c7475400800100        mov       dword [edi+54h],18000h
  004f7dad  8907                  mov       [edi],eax

LAB_004f7daf:
  004f7daf  8b5620                mov       edx,[esi+20h]
  004f7db2  8b4f1c                mov       ecx,[edi+1Ch]
  004f7db5  8b5d00                mov       ebx,[ebp]
  004f7db8  8b4624                mov       eax,[esi+24h]
  004f7dbb  89542420              mov       [esp+20h],edx
  004f7dbf  894d00                mov       [ebp],ecx
  004f7dc2  8b5720                mov       edx,[edi+20h]
  004f7dc5  6aff                  push      0FFFFFFFFh
  004f7dc7  89442428              mov       [esp+28h],eax
  004f7dcb  895620                mov       [esi+20h],edx
  004f7dce  8b4724                mov       eax,[edi+24h]
  004f7dd1  6a00                  push      0
  004f7dd3  6a39                  push      39h
  004f7dd5  56                    push      esi
  004f7dd6  894624                mov       [esi+24h],eax
  004f7dd9  e8f22bf8ff            call      0047A9D0h
  004f7dde  8b4c2430              mov       ecx,[esp+30h]
  004f7de2  8b542434              mov       edx,[esp+34h]
  004f7de6  83c410                add       esp,10h
  004f7de9  895d00                mov       [ebp],ebx
  004f7dec  894e20                mov       [esi+20h],ecx
  004f7def  895624                mov       [esi+24h],edx
  004f7df2  e9f8010000            jmp       004F7FEFh

LAB_004f7df7:
  004f7df7  a811                  test      al,11h
  004f7df9  0f85f0010000          jne       near 004F7FEFh
  004f7dff  a904080000            test      eax,804h
  004f7e04  0f85e5010000          jne       near 004F7FEFh
  004f7e0a  33c9                  xor       ecx,ecx
  004f7e0c  8a4e42                mov       cl,[esi+42h]
  004f7e0f  83f903                cmp       ecx,3
  004f7e12  0f87a9010000          ja        near 004F7FC1h
  004f7e18  ff248d60824f00        jmp       dword [ecx*4+4F8260h]
  004f7e1f  33c9                  xor       ecx,ecx
  004f7e21  66394e18              cmp       [esi+18h],cx
  004f7e25  740a                  je        short 004F7E31h
  004f7e27  0d00000001            or        eax,1000000h
  004f7e2c  894e18                mov       [esi+18h],ecx
  004f7e2f  8906                  mov       [esi],eax

LAB_004f7e31:
  004f7e31  817c24102c010000      cmp       dword [esp+10h],12Ch
  004f7e39  7d09                  jge       short 004F7E44h
  004f7e3b  c6464300              mov       byte [esi+43h],0
  004f7e3f  e97d010000            jmp       004F7FC1h

LAB_004f7e44:
  004f7e44  8a4643                mov       al,[esi+43h]
  004f7e47  84c0                  test      al,al
  004f7e49  7405                  je        short 004F7E50h
  004f7e4b  fec8                  dec       al
  004f7e4d  884643                mov       [esi+43h],al

LAB_004f7e50:
  004f7e50  894e5c                mov       [esi+5Ch],ecx
  004f7e53  e969010000            jmp       004F7FC1h
  004f7e58  66396e18              cmp       [esi+18h],bp
  004f7e5c  740a                  je        short 004F7E68h
  004f7e5e  0d00000001            or        eax,1000000h
  004f7e63  896e18                mov       [esi+18h],ebp
  004f7e66  8906                  mov       [esi],eax

LAB_004f7e68:
  004f7e68  8b442438              mov       eax,[esp+38h]
  004f7e6c  8d8880000000          lea       ecx,[eax+80h]
  004f7e72  81e1ff0f0000          and       ecx,0FFFh
  004f7e78  81f900010000          cmp       ecx,100h
  004f7e7e  7f14                  jg        short 004F7E94h
  004f7e80  66014632              add       [esi+32h],ax
  004f7e84  c6464300              mov       byte [esi+43h],0
  004f7e88  c7465c00000000        mov       dword [esi+5Ch],0
  004f7e8f  e92d010000            jmp       004F7FC1h

LAB_004f7e94:
  004f7e94  25ff0f0000            and       eax,0FFFh
  004f7e99  33d2                  xor       edx,edx
  004f7e9b  3d00080000            cmp       eax,800h
  004f7ea0  c7465c00000000        mov       dword [esi+5Ch],0
  004f7ea7  0f9dc2                setge     dl
  004f7eaa  4a                    dec       edx
  004f7eab  81e200010000          and       edx,100h
  004f7eb1  83c280                add       edx,0FFFFFF80h
  004f7eb4  8bc2                  mov       eax,edx
  004f7eb6  66014632              add       [esi+32h],ax
  004f7eba  e902010000            jmp       004F7FC1h
  004f7ebf  66396e18              cmp       [esi+18h],bp
  004f7ec3  740a                  je        short 004F7ECFh
  004f7ec5  0d00000001            or        eax,1000000h
  004f7eca  896e18                mov       [esi+18h],ebp
  004f7ecd  8906                  mov       [esi],eax

LAB_004f7ecf:
  004f7ecf  0fbf4610              movsx     eax,word [esi+10h]
  004f7ed3  39442410              cmp       [esp+10h],eax
  004f7ed7  7d09                  jge       short 004F7EE2h
  004f7ed9  c6464300              mov       byte [esi+43h],0
  004f7edd  e9df000000            jmp       004F7FC1h

LAB_004f7ee2:
  004f7ee2  8a4643                mov       al,[esi+43h]
  004f7ee5  84c0                  test      al,al
  004f7ee7  7405                  je        short 004F7EEEh
  004f7ee9  fec8                  dec       al
  004f7eeb  884643                mov       [esi+43h],al

LAB_004f7eee:
  004f7eee  8b442438              mov       eax,[esp+38h]
  004f7ef2  8d8880000000          lea       ecx,[eax+80h]
  004f7ef8  81e1ff0f0000          and       ecx,0FFFh
  004f7efe  81f900010000          cmp       ecx,100h
  004f7f04  7e1b                  jle       short 004F7F21h
  004f7f06  25ff0f0000            and       eax,0FFFh
  004f7f0b  33d2                  xor       edx,edx
  004f7f0d  3d00080000            cmp       eax,800h
  004f7f12  0f9dc2                setge     dl
  004f7f15  4a                    dec       edx
  004f7f16  81e200010000          and       edx,100h
  004f7f1c  83c280                add       edx,0FFFFFF80h
  004f7f1f  8bc2                  mov       eax,edx

LAB_004f7f21:
  004f7f21  66014632              add       [esi+32h],ax
  004f7f25  c7465c00400100        mov       dword [esi+5Ch],14000h
  004f7f2c  e990000000            jmp       004F7FC1h
  004f7f31  8a4e43                mov       cl,[esi+43h]
  004f7f34  84c9                  test      cl,cl
  004f7f36  7405                  je        short 004F7F3Dh
  004f7f38  fec9                  dec       cl
  004f7f3a  884e43                mov       [esi+43h],cl

LAB_004f7f3d:
  004f7f3d  66396e18              cmp       [esi+18h],bp
  004f7f41  740a                  je        short 004F7F4Dh
  004f7f43  0d00000001            or        eax,1000000h
  004f7f48  896e18                mov       [esi+18h],ebp
  004f7f4b  8906                  mov       [esi],eax

LAB_004f7f4d:
  004f7f4d  0fbf4610              movsx     eax,word [esi+10h]
  004f7f51  50                    push      eax
  004f7f52  c7465c00000100        mov       dword [esi+5Ch],10000h
  004f7f59  e8beda0400            call      00545A1Ch
  004f7f5e  0faf465c              imul      eax,[esi+5Ch]
  004f7f62  0fbf4e10              movsx     ecx,word [esi+10h]
  004f7f66  8b7e1c                mov       edi,[esi+1Ch]
  004f7f69  51                    push      ecx
  004f7f6a  c1f80c                sar       eax,0Ch
  004f7f6d  03f8                  add       edi,eax
  004f7f6f  897e1c                mov       [esi+1Ch],edi
  004f7f72  e89fda0400            call      00545A16h
  004f7f77  0faf465c              imul      eax,[esi+5Ch]
  004f7f7b  8b5e24                mov       ebx,[esi+24h]
  004f7f7e  c7465c00000000        mov       dword [esi+5Ch],0
  004f7f85  c1f80c                sar       eax,0Ch
  004f7f88  03d8                  add       ebx,eax
  004f7f8a  8b442438              mov       eax,[esp+38h]
  004f7f8e  895e24                mov       [esi+24h],ebx
  004f7f91  8d5060                lea       edx,[eax+60h]
  004f7f94  81e2ff0f0000          and       edx,0FFFh
  004f7f9a  81fac0000000          cmp       edx,0C0h
  004f7fa0  7e1b                  jle       short 004F7FBDh
  004f7fa2  25ff0f0000            and       eax,0FFFh
  004f7fa7  33c9                  xor       ecx,ecx
  004f7fa9  3d00080000            cmp       eax,800h
  004f7fae  0f9dc1                setge     cl
  004f7fb1  49                    dec       ecx
  004f7fb2  81e1c0000000          and       ecx,0C0h
  004f7fb8  83c1a0                add       ecx,0FFFFFFA0h
  004f7fbb  8bc1                  mov       eax,ecx

LAB_004f7fbd:
  004f7fbd  66014632              add       [esi+32h],ax

LAB_004f7fc1:
  004f7fc1  66396e18              cmp       [esi+18h],bp
  004f7fc5  7528                  jne       short 004F7FEFh
  004f7fc7  668b461a              mov       ax,[esi+1Ah]
  004f7fcb  663d0600              cmp       ax,6
  004f7fcf  7406                  je        short 004F7FD7h
  004f7fd1  663d1200              cmp       ax,12h
  004f7fd5  7518                  jne       short 004F7FEFh

LAB_004f7fd7:
  004f7fd7  8d561c                lea       edx,[esi+1Ch]
  004f7fda  52                    push      edx
  004f7fdb  6800180000            push      1800h
  004f7fe0  6800020000            push      200h
  004f7fe5  6a35                  push      35h
  004f7fe7  e8345b0400            call      0053DB20h
  004f7fec  83c410                add       esp,10h

LAB_004f7fef:
  004f7fef  8b442414              mov       eax,[esp+14h]
  004f7ff3  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  004f7ffc  50                    push      eax
  004f7ffd  56                    push      esi
  004f7ffe  e85d01f9ff            call      00488160h
  004f8003  83c408                add       esp,8
  004f8006  56                    push      esi
  004f8007  e834b90300            call      00533940h
  004f800c  83c404                add       esp,4
  004f800f  56                    push      esi
  004f8010  e8db7afeff            call      004DFAF0h
  004f8015  8b4c242c              mov       ecx,[esp+2Ch]
  004f8019  83c404                add       esp,4
  004f801c  51                    push      ecx
  004f801d  56                    push      esi
  004f801e  e84db7feff            call      004E3770h
  004f8023  8b44241c              mov       eax,[esp+1Ch]
  004f8027  83c408                add       esp,8
  004f802a  85c0                  test      eax,eax
  004f802c  0f8423020000          je        near 004F8255h
  004f8032  f70617080000          test      dword [esi],817h
  004f8038  0f8517020000          jne       near 004F8255h
  004f803e  8a4643                mov       al,[esi+43h]
  004f8041  84c0                  test      al,al
  004f8043  0f850c020000          jne       near 004F8255h
  004f8049  8b466c                mov       eax,[esi+6Ch]
  004f804c  8b5e1c                mov       ebx,[esi+1Ch]
  004f804f  8b4e24                mov       ecx,[esi+24h]
  004f8052  8b781c                mov       edi,[eax+1Ch]
  004f8055  8b6824                mov       ebp,[eax+24h]
  004f8058  2bfb                  sub       edi,ebx
  004f805a  2be9                  sub       ebp,ecx
  004f805c  c1ff0c                sar       edi,0Ch
  004f805f  c1fd0c                sar       ebp,0Ch
  004f8062  8bd7                  mov       edx,edi
  004f8064  8bc5                  mov       eax,ebp
  004f8066  0fafd7                imul      edx,edi
  004f8069  0fafc5                imul      eax,ebp
  004f806c  03d0                  add       edx,eax
  004f806e  52                    push      edx
  004f806f  e89cd90400            call      00545A10h
  004f8074  55                    push      ebp
  004f8075  57                    push      edi
  004f8076  89442418              mov       [esp+18h],eax
  004f807a  e8a3d90400            call      00545A22h
  004f807f  668b4e32              mov       cx,[esi+32h]
  004f8083  2bc1                  sub       eax,ecx
  004f8085  25ff0f0000            and       eax,0FFFh
  004f808a  89442438              mov       [esp+38h],eax
  004f808e  8b442410              mov       eax,[esp+10h]
  004f8092  3d2c010000            cmp       eax,12Ch
  004f8097  7d5e                  jge       short 004F80F7h
  004f8099  e8a2f00400            call      00547140h
  004f809e  99                    cdq
  004f809f  b964000000            mov       ecx,64h
  004f80a4  f7f9                  idiv      ecx
  004f80a6  83fa32                cmp       edx,32h
  004f80a9  7d1e                  jge       short 004F80C9h
  004f80ab  8b542438              mov       edx,[esp+38h]
  004f80af  81c200010000          add       edx,100h
  004f80b5  81e2ff0f0000          and       edx,0FFFh
  004f80bb  81fa00020000          cmp       edx,200h
  004f80c1  0f8f85000000          jg        near 004F814Ch
  004f80c7  eb58                  jmp       short 004F8121h

LAB_004f80c9:
  004f80c9  83fa4b                cmp       edx,4Bh
  004f80cc  0f8d20010000          jge       near 004F81F2h
  004f80d2  c6464202              mov       byte [esi+42h],2
  004f80d6  e865f00400            call      00547140h
  004f80db  99                    cdq
  004f80dc  b90a000000            mov       ecx,0Ah
  004f80e1  66c74610c800          mov       word [esi+10h],0C8h
  004f80e7  f7f9                  idiv      ecx
  004f80e9  80c214                add       dl,14h
  004f80ec  885643                mov       [esi+43h],dl
  004f80ef  5f                    pop       edi
  004f80f0  5e                    pop       esi
  004f80f1  5d                    pop       ebp
  004f80f2  5b                    pop       ebx
  004f80f3  83c424                add       esp,24h
  004f80f6  c3                    ret

LAB_004f80f7:
  004f80f7  e844f00400            call      00547140h
  004f80fc  99                    cdq
  004f80fd  b964000000            mov       ecx,64h
  004f8102  f7f9                  idiv      ecx
  004f8104  83fa1e                cmp       edx,1Eh
  004f8107  7d53                  jge       short 004F815Ch
  004f8109  8b542438              mov       edx,[esp+38h]
  004f810d  81c200020000          add       edx,200h
  004f8113  81e2ff0f0000          and       edx,0FFFh
  004f8119  81fa00040000          cmp       edx,400h
  004f811f  7d2b                  jge       short 004F814Ch

LAB_004f8121:
  004f8121  8b06                  mov       eax,[esi]
  004f8123  c7461803000000        mov       dword [esi+18h],3
  004f812a  25fffffffe            and       eax,0FEFFFFFFh
  004f812f  c6464200              mov       byte [esi+42h],0
  004f8133  0c02                  or        al,2
  004f8135  66c746100200          mov       word [esi+10h],2
  004f813b  8906                  mov       [esi],eax
  004f813d  c7465c00000000        mov       dword [esi+5Ch],0
  004f8144  5f                    pop       edi
  004f8145  5e                    pop       esi
  004f8146  5d                    pop       ebp
  004f8147  5b                    pop       ebx
  004f8148  83c424                add       esp,24h
  004f814b  c3                    ret

LAB_004f814c:
  004f814c  c6464201              mov       byte [esi+42h],1
  004f8150  c6464301              mov       byte [esi+43h],1
  004f8154  5f                    pop       edi
  004f8155  5e                    pop       esi
  004f8156  5d                    pop       ebp
  004f8157  5b                    pop       ebx
  004f8158  83c424                add       esp,24h
  004f815b  c3                    ret

LAB_004f815c:
  004f815c  83fa32                cmp       edx,32h
  004f815f  7d26                  jge       short 004F8187h
  004f8161  8b0e                  mov       ecx,[esi]
  004f8163  c7461805000000        mov       dword [esi+18h],5
  004f816a  81e1fffffffe          and       ecx,0FEFFFFFFh
  004f8170  c6464201              mov       byte [esi+42h],1
  004f8174  83c902                or        ecx,2
  004f8177  66c746100100          mov       word [esi+10h],1
  004f817d  890e                  mov       [esi],ecx
  004f817f  5f                    pop       edi
  004f8180  5e                    pop       esi
  004f8181  5d                    pop       ebp
  004f8182  5b                    pop       ebx
  004f8183  83c424                add       esp,24h
  004f8186  c3                    ret

LAB_004f8187:
  004f8187  83fa46                cmp       edx,46h
  004f818a  7d61                  jge       short 004F81EDh
  004f818c  8b542438              mov       edx,[esp+38h]
  004f8190  81c200020000          add       edx,200h
  004f8196  81e2ff0f0000          and       edx,0FFFh
  004f819c  81fa00040000          cmp       edx,400h
  004f81a2  7d24                  jge       short 004F81C8h
  004f81a4  8b06                  mov       eax,[esi]
  004f81a6  c7461806000000        mov       dword [esi+18h],6
  004f81ad  25fffffffe            and       eax,0FEFFFFFFh
  004f81b2  c6464202              mov       byte [esi+42h],2
  004f81b6  0c02                  or        al,2
  004f81b8  66c746100100          mov       word [esi+10h],1
  004f81be  8906                  mov       [esi],eax
  004f81c0  5f                    pop       edi
  004f81c1  5e                    pop       esi
  004f81c2  5d                    pop       ebp
  004f81c3  5b                    pop       ebx
  004f81c4  83c424                add       esp,24h
  004f81c7  c3                    ret

LAB_004f81c8:
  004f81c8  c6464202              mov       byte [esi+42h],2
  004f81cc  e86fef0400            call      00547140h
  004f81d1  99                    cdq
  004f81d2  b90a000000            mov       ecx,0Ah
  004f81d7  66c74610c800          mov       word [esi+10h],0C8h
  004f81dd  f7f9                  idiv      ecx
  004f81df  80c214                add       dl,14h
  004f81e2  885643                mov       [esi+43h],dl
  004f81e5  5f                    pop       edi
  004f81e6  5e                    pop       esi
  004f81e7  5d                    pop       ebp
  004f81e8  5b                    pop       ebx
  004f81e9  83c424                add       esp,24h
  004f81ec  c3                    ret

LAB_004f81ed:
  004f81ed  83fa5a                cmp       edx,5Ah
  004f81f0  7d4c                  jge       short 004F823Eh

LAB_004f81f2:
  004f81f2  c6464203              mov       byte [esi+42h],3
  004f81f6  e845ef0400            call      00547140h
  004f81fb  99                    cdq
  004f81fc  b90a000000            mov       ecx,0Ah
  004f8201  f7f9                  idiv      ecx
  004f8203  80c214                add       dl,14h
  004f8206  885643                mov       [esi+43h],dl
  004f8209  8b542418              mov       edx,[esp+18h]
  004f820d  8dba80000000          lea       edi,[edx+80h]
  004f8213  c1e704                shl       edi,4
  004f8216  e825ef0400            call      00547140h
  004f821b  99                    cdq
  004f821c  f7ff                  idiv      edi
  004f821e  d1ff                  sar       edi,1
  004f8220  2bd7                  sub       edx,edi
  004f8222  8b7c2438              mov       edi,[esp+38h]
  004f8226  03d7                  add       edx,edi
  004f8228  66035632              add       dx,[esi+32h]
  004f822c  81c200080000          add       edx,800h
  004f8232  66895610              mov       [esi+10h],dx
  004f8236  5f                    pop       edi
  004f8237  5e                    pop       esi
  004f8238  5d                    pop       ebp
  004f8239  5b                    pop       ebx
  004f823a  83c424                add       esp,24h
  004f823d  c3                    ret

LAB_004f823e:
  004f823e  c6464200              mov       byte [esi+42h],0
  004f8242  e8f9ee0400            call      00547140h
  004f8247  99                    cdq
  004f8248  b90a000000            mov       ecx,0Ah
  004f824d  f7f9                  idiv      ecx
  004f824f  80c214                add       dl,14h
  004f8252  885643                mov       [esi+43h],dl

LAB_004f8255:
  004f8255  5f                    pop       edi
  004f8256  5e                    pop       esi
  004f8257  5d                    pop       ebp
  004f8258  5b                    pop       ebx
  004f8259  83c424                add       esp,24h
  004f825c  c3                    ret
  004f825d  90                    nop
  004f825e  8bff                  mov       edi,edi
  004f8260  1f                    pop       ds
  004f8261  7e4f                  jle       short 004F82B2h
  004f8263  00587e                add       [eax+7Eh],bl
  004f8266  4f                    dec       edi
  004f8267  00bf7e4f0031          add       [edi+31004F7Eh],bh
  004f826d  7f4f                  jg        short 004F82BEh
  004f826f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
