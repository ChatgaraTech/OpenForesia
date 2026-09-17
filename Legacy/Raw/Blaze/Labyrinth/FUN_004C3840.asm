Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c3840  section .text  offset fichier 0xc2c40  2512 octets

  004c3840  83ec54                sub       esp,54h
  004c3843  53                    push      ebx
  004c3844  55                    push      ebp
  004c3845  56                    push      esi
  004c3846  8b742464              mov       esi,[esp+64h]
  004c384a  57                    push      edi
  004c384b  33ff                  xor       edi,edi
  004c384d  8a4641                mov       al,[esi+41h]
  004c3850  66897c2430            mov       [esp+30h],di
  004c3855  84c0                  test      al,al
  004c3857  66c74424321e00        mov       word [esp+32h],1Eh
  004c385e  66c74424343200        mov       word [esp+34h],32h
  004c3865  66c74424366400        mov       word [esp+36h],64h
  004c386c  66897c2438            mov       [esp+38h],di
  004c3871  66c744243a5000        mov       word [esp+3Ah],50h
  004c3878  750f                  jne       short 004C3889h
  004c387a  57                    push      edi
  004c387b  57                    push      edi
  004c387c  56                    push      esi
  004c387d  c6464101              mov       byte [esi+41h],1
  004c3881  e8cafbffff            call      004C3450h
  004c3886  83c40c                add       esp,0Ch

LAB_004c3889:
  004c3889  8d442424              lea       eax,[esp+24h]
  004c388d  8d4c2428              lea       ecx,[esp+28h]
  004c3891  50                    push      eax
  004c3892  8d542430              lea       edx,[esp+30h]
  004c3896  51                    push      ecx
  004c3897  52                    push      edx
  004c3898  56                    push      esi
  004c3899  e8e2500700            call      00538980h
  004c389e  83c410                add       esp,10h
  004c38a1  8d442420              lea       eax,[esp+20h]
  004c38a5  8d4c2418              lea       ecx,[esp+18h]
  004c38a9  8d542410              lea       edx,[esp+10h]
  004c38ad  50                    push      eax
  004c38ae  51                    push      ecx
  004c38af  8d442424              lea       eax,[esp+24h]
  004c38b3  52                    push      edx
  004c38b4  8d4c2474              lea       ecx,[esp+74h]
  004c38b8  50                    push      eax
  004c38b9  51                    push      ecx
  004c38ba  56                    push      esi
  004c38bb  e860260700            call      00535F20h
  004c38c0  8bac2480000000        mov       ebp,[esp+80h]
  004c38c7  8b542438              mov       edx,[esp+38h]
  004c38cb  83c418                add       esp,18h
  004c38ce  b840000000            mov       eax,40h
  004c38d3  2bc5                  sub       eax,ebp
  004c38d5  52                    push      edx
  004c38d6  56                    push      esi
  004c38d7  8944241c              mov       [esp+1Ch],eax
  004c38db  e800250100            call      004D5DE0h
  004c38e0  8b4c2420              mov       ecx,[esp+20h]
  004c38e4  668b4632              mov       ax,[esi+32h]
  004c38e8  83c408                add       esp,8
  004c38eb  3bcf                  cmp       ecx,edi
  004c38ed  6689442452            mov       [esp+52h],ax
  004c38f2  bb07000000            mov       ebx,7
  004c38f7  0f84b9040000          je        near 004C3DB6h
  004c38fd  8b2e                  mov       ebp,[esi]
  004c38ff  33d2                  xor       edx,edx
  004c3901  8a5604                mov       dl,[esi+4]
  004c3904  81e5fffffffd          and       ebp,0FDFFFFFFh
  004c390a  0fbf7e18              movsx     edi,word [esi+18h]
  004c390e  892e                  mov       [esi],ebp
  004c3910  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004c3917  8bc5                  mov       eax,ebp
  004c3919  8b14ba                mov       edx,[edx+edi*4]
  004c391c  a900000001            test      eax,1000000h
  004c3921  741d                  je        short 004C3940h
  004c3923  66ff461a              inc       word [esi+1Ah]
  004c3927  668b461a              mov       ax,[esi+1Ah]
  004c392b  33ff                  xor       edi,edi
  004c392d  668b7a16              mov       di,[edx+16h]
  004c3931  0fbfc0                movsx     eax,ax
  004c3934  3bc7                  cmp       eax,edi
  004c3936  7c29                  jl        short 004C3961h
  004c3938  66c7461a0000          mov       word [esi+1Ah],0
  004c393e  eb21                  jmp       short 004C3961h

LAB_004c3940:
  004c3940  66ff461a              inc       word [esi+1Ah]
  004c3944  668b7e1a              mov       di,[esi+1Ah]
  004c3948  33ed                  xor       ebp,ebp
  004c394a  668b6a16              mov       bp,[edx+16h]
  004c394e  0fbfd7                movsx     edx,di
  004c3951  3bd5                  cmp       edx,ebp
  004c3953  7c0c                  jl        short 004C3961h
  004c3955  4f                    dec       edi
  004c3956  0d00000002            or        eax,2000000h
  004c395b  66897e1a              mov       [esi+1Ah],di
  004c395f  8906                  mov       [esi],eax

LAB_004c3961:
  004c3961  f60602                test      byte [esi],2
  004c3964  0f84b1000000          je        near 004C3A1Bh
  004c396a  66837e1a0d            cmp       word [esi+1Ah],0Dh
  004c396f  7409                  je        short 004C397Ah
  004c3971  385e42                cmp       [esi+42h],bl
  004c3974  0f858b000000          jne       near 004C3A05h

LAB_004c397a:
  004c397a  8d442430              lea       eax,[esp+30h]
  004c397e  6a00                  push      0
  004c3980  8d4e1c                lea       ecx,[esi+1Ch]
  004c3983  50                    push      eax
  004c3984  51                    push      ecx
  004c3985  56                    push      esi
  004c3986  e8a5620500            call      00519C30h
  004c398b  8bf8                  mov       edi,eax
  004c398d  a1b8e05d00            mov       eax,[5DE0B8h]
  004c3992  83c410                add       esp,10h
  004c3995  85c0                  test      eax,eax
  004c3997  7468                  je        short 004C3A01h
  004c3999  8b0f                  mov       ecx,[edi]
  004c399b  57                    push      edi
  004c399c  83c940                or        ecx,40h
  004c399f  56                    push      esi
  004c39a0  66c787960000001000    mov       word [edi+96h],10h
  004c39a9  890f                  mov       [edi],ecx
  004c39ab  e8d0610700            call      00539B80h
  004c39b0  83c408                add       esp,8
  004c39b3  8d571c                lea       edx,[edi+1Ch]
  004c39b6  52                    push      edx
  004c39b7  6800180000            push      1800h
  004c39bc  6800030000            push      300h
  004c39c1  6a03                  push      3
  004c39c3  e858a10700            call      0053DB20h
  004c39c8  83c410                add       esp,10h
  004c39cb  e870370800            call      00547140h
  004c39d0  99                    cdq
  004c39d1  b964000000            mov       ecx,64h
  004c39d6  f7f9                  idiv      ecx
  004c39d8  8bea                  mov       ebp,edx
  004c39da  83fd0a                cmp       ebp,0Ah
  004c39dd  7f11                  jg        short 004C39F0h
  004c39df  6a00                  push      0
  004c39e1  57                    push      edi
  004c39e2  56                    push      esi
  004c39e3  e868080000            call      004C4250h
  004c39e8  83c40c                add       esp,0Ch
  004c39eb  83fd0a                cmp       ebp,0Ah
  004c39ee  7e11                  jle       short 004C3A01h

LAB_004c39f0:
  004c39f0  83fd14                cmp       ebp,14h
  004c39f3  7f0c                  jg        short 004C3A01h
  004c39f5  6a00                  push      0
  004c39f7  57                    push      edi
  004c39f8  56                    push      esi
  004c39f9  e812080000            call      004C4210h
  004c39fe  83c40c                add       esp,0Ch

LAB_004c3a01:
  004c3a01  8b4c2418              mov       ecx,[esp+18h]

LAB_004c3a05:
  004c3a05  8b06                  mov       eax,[esi]
  004c3a07  a900000002            test      eax,2000000h
  004c3a0c  740d                  je        short 004C3A1Bh
  004c3a0e  385e42                cmp       [esi+42h],bl
  004c3a11  7408                  je        short 004C3A1Bh
  004c3a13  24fd                  and       al,0FDh
  004c3a15  c6464300              mov       byte [esi+43h],0
  004c3a19  8906                  mov       [esi],eax

LAB_004c3a1b:
  004c3a1b  8b06                  mov       eax,[esi]
  004c3a1d  a802                  test      al,2
  004c3a1f  7409                  je        short 004C3A2Ah
  004c3a21  385e42                cmp       [esi+42h],bl
  004c3a24  0f858c030000          jne       near 004C3DB6h

LAB_004c3a2a:
  004c3a2a  8bf8                  mov       edi,eax
  004c3a2c  83e701                and       edi,1
  004c3a2f  7409                  je        short 004C3A3Ah
  004c3a31  385e42                cmp       [esi+42h],bl
  004c3a34  0f857c030000          jne       near 004C3DB6h

LAB_004c3a3a:
  004c3a3a  33d2                  xor       edx,edx
  004c3a3c  8a5642                mov       dl,[esi+42h]
  004c3a3f  3bd3                  cmp       edx,ebx
  004c3a41  0f876f030000          ja        near 004C3DB6h
  004c3a47  ff2495e8414c00        jmp       dword [edx*4+4C41E8h]
  004c3a4e  817c241c2c010000      cmp       dword [esp+1Ch],12Ch
  004c3a56  0f8c56030000          jl        near 004C3DB2h
  004c3a5c  8a4643                mov       al,[esi+43h]
  004c3a5f  84c0                  test      al,al
  004c3a61  0f8481000000          je        near 004C3AE8h
  004c3a67  fec8                  dec       al
  004c3a69  c7465c00000000        mov       dword [esi+5Ch],0
  004c3a70  884643                mov       [esi+43h],al
  004c3a73  e93e030000            jmp       004C3DB6h
  004c3a78  8b4c2410              mov       ecx,[esp+10h]
  004c3a7c  8d91c8000000          lea       edx,[ecx+0C8h]
  004c3a82  81e2ff0f0000          and       edx,0FFFh
  004c3a88  81fa90010000          cmp       edx,190h
  004c3a8e  7e54                  jle       short 004C3AE4h
  004c3a90  81e1ff0f0000          and       ecx,0FFFh
  004c3a96  81f900080000          cmp       ecx,800h
  004c3a9c  7d0c                  jge       short 004C3AAAh
  004c3a9e  8b4c2468              mov       ecx,[esp+68h]
  004c3aa2  81c180000000          add       ecx,80h
  004c3aa8  eb0b                  jmp       short 004C3AB5h

LAB_004c3aaa:
  004c3aaa  8b542468              mov       edx,[esp+68h]
  004c3aae  b980ffffff            mov       ecx,0FFFFFF80h
  004c3ab3  2bca                  sub       ecx,edx

LAB_004c3ab5:
  004c3ab5  66014e32              add       [esi+32h],cx
  004c3ab9  b901000000            mov       ecx,1
  004c3abe  66394e18              cmp       [esi+18h],cx
  004c3ac2  7424                  je        short 004C3AE8h
  004c3ac4  0d00000001            or        eax,1000000h
  004c3ac9  894e18                mov       [esi+18h],ecx
  004c3acc  8906                  mov       [esi],eax
  004c3ace  c7465c00000000        mov       dword [esi+5Ch],0
  004c3ad5  e9dc020000            jmp       004C3DB6h
  004c3ada  0fbf5610              movsx     edx,word [esi+10h]
  004c3ade  3954241c              cmp       [esp+1Ch],edx
  004c3ae2  7d10                  jge       short 004C3AF4h

LAB_004c3ae4:
  004c3ae4  c6464300              mov       byte [esi+43h],0

LAB_004c3ae8:
  004c3ae8  c7465c00000000        mov       dword [esi+5Ch],0
  004c3aef  e9c2020000            jmp       004C3DB6h

LAB_004c3af4:
  004c3af4  8a4e43                mov       cl,[esi+43h]
  004c3af7  84c9                  test      cl,cl
  004c3af9  7405                  je        short 004C3B00h
  004c3afb  fec9                  dec       cl
  004c3afd  884e43                mov       [esi+43h],cl

LAB_004c3b00:
  004c3b00  b901000000            mov       ecx,1
  004c3b05  66394e18              cmp       [esi+18h],cx
  004c3b09  740a                  je        short 004C3B15h
  004c3b0b  0d00000001            or        eax,1000000h
  004c3b10  894e18                mov       [esi+18h],ecx
  004c3b13  8906                  mov       [esi],eax

LAB_004c3b15:
  004c3b15  8b442410              mov       eax,[esp+10h]
  004c3b19  8d8800010000          lea       ecx,[eax+100h]
  004c3b1f  81e1ff0f0000          and       ecx,0FFFh
  004c3b25  81f900020000          cmp       ecx,200h
  004c3b2b  8b4c2468              mov       ecx,[esp+68h]
  004c3b2f  7e1f                  jle       short 004C3B50h
  004c3b31  25ff0f0000            and       eax,0FFFh
  004c3b36  3d00080000            cmp       eax,800h
  004c3b3b  7d08                  jge       short 004C3B45h
  004c3b3d  8d8180000000          lea       eax,[ecx+80h]
  004c3b43  eb07                  jmp       short 004C3B4Ch

LAB_004c3b45:
  004c3b45  b880ffffff            mov       eax,0FFFFFF80h
  004c3b4a  2bc1                  sub       eax,ecx

LAB_004c3b4c:
  004c3b4c  66014632              add       [esi+32h],ax

LAB_004c3b50:
  004c3b50  8d14c9                lea       edx,[ecx+ecx*8]
  004c3b53  c1e204                shl       edx,4
  004c3b56  81c200a00000          add       edx,0A000h
  004c3b5c  89565c                mov       [esi+5Ch],edx
  004c3b5f  e952020000            jmp       004C3DB6h
  004c3b64  8a5643                mov       dl,[esi+43h]
  004c3b67  84d2                  test      dl,dl
  004c3b69  7405                  je        short 004C3B70h
  004c3b6b  feca                  dec       dl
  004c3b6d  885643                mov       [esi+43h],dl

LAB_004c3b70:
  004c3b70  ba01000000            mov       edx,1
  004c3b75  66395618              cmp       [esi+18h],dx
  004c3b79  740a                  je        short 004C3B85h
  004c3b7b  0d00000001            or        eax,1000000h
  004c3b80  895618                mov       [esi+18h],edx
  004c3b83  8906                  mov       [esi],eax

LAB_004c3b85:
  004c3b85  8b442468              mov       eax,[esp+68h]
  004c3b89  83c070                add       eax,70h
  004c3b8c  c1e008                shl       eax,8
  004c3b8f  0fafc8                imul      ecx,eax
  004c3b92  c1f90c                sar       ecx,0Ch
  004c3b95  8bf9                  mov       edi,ecx
  004c3b97  89465c                mov       [esi+5Ch],eax
  004c3b9a  0fbf4e10              movsx     ecx,word [esi+10h]
  004c3b9e  51                    push      ecx
  004c3b9f  e8781e0800            call      00545A1Ch
  004c3ba4  0fafc7                imul      eax,edi
  004c3ba7  0fbf5610              movsx     edx,word [esi+10h]
  004c3bab  8b4e1c                mov       ecx,[esi+1Ch]
  004c3bae  52                    push      edx
  004c3baf  c1f80c                sar       eax,0Ch
  004c3bb2  03c8                  add       ecx,eax
  004c3bb4  894e1c                mov       [esi+1Ch],ecx
  004c3bb7  e85a1e0800            call      00545A16h
  004c3bbc  0fafc7                imul      eax,edi
  004c3bbf  8b5e24                mov       ebx,[esi+24h]
  004c3bc2  c7465c00000000        mov       dword [esi+5Ch],0
  004c3bc9  c1f80c                sar       eax,0Ch
  004c3bcc  03d8                  add       ebx,eax
  004c3bce  8b442410              mov       eax,[esp+10h]
  004c3bd2  895e24                mov       [esi+24h],ebx
  004c3bd5  8d8800010000          lea       ecx,[eax+100h]
  004c3bdb  81e1ff0f0000          and       ecx,0FFFh
  004c3be1  81f900020000          cmp       ecx,200h
  004c3be7  0f8ec9010000          jle       near 004C3DB6h
  004c3bed  25ff0f0000            and       eax,0FFFh
  004c3bf2  3d00080000            cmp       eax,800h
  004c3bf7  7d14                  jge       short 004C3C0Dh
  004c3bf9  8b542468              mov       edx,[esp+68h]
  004c3bfd  8d04d532000000        lea       eax,[edx*8+32h]
  004c3c04  66014632              add       [esi+32h],ax
  004c3c08  e9a9010000            jmp       004C3DB6h

LAB_004c3c0d:
  004c3c0d  8b442468              mov       eax,[esp+68h]
  004c3c11  8d0cc500000000        lea       ecx,[eax*8]
  004c3c18  b8ceffffff            mov       eax,0FFFFFFCEh
  004c3c1d  2bc1                  sub       eax,ecx
  004c3c1f  66014632              add       [esi+32h],ax
  004c3c23  e98e010000            jmp       004C3DB6h
  004c3c28  85ff                  test      edi,edi
  004c3c2a  0f8586010000          jne       near 004C3DB6h
  004c3c30  8b542468              mov       edx,[esp+68h]
  004c3c34  0d00000001            or        eax,1000000h
  004c3c39  8906                  mov       [esi],eax
  004c3c3b  c7461801000000        mov       dword [esi+18h],1
  004c3c42  8d82c0000000          lea       eax,[edx+0C0h]
  004c3c48  c1e008                shl       eax,8
  004c3c4b  0fafc8                imul      ecx,eax
  004c3c4e  89465c                mov       [esi+5Ch],eax
  004c3c51  0fbf4610              movsx     eax,word [esi+10h]
  004c3c55  c1f90c                sar       ecx,0Ch
  004c3c58  50                    push      eax
  004c3c59  8bf9                  mov       edi,ecx
  004c3c5b  e8bc1d0800            call      00545A1Ch
  004c3c60  0fafc7                imul      eax,edi
  004c3c63  0fbf4e10              movsx     ecx,word [esi+10h]
  004c3c67  8b6e50                mov       ebp,[esi+50h]
  004c3c6a  51                    push      ecx
  004c3c6b  c1f80c                sar       eax,0Ch
  004c3c6e  03e8                  add       ebp,eax
  004c3c70  896e50                mov       [esi+50h],ebp
  004c3c73  e89e1d0800            call      00545A16h
  004c3c78  0fafc7                imul      eax,edi
  004c3c7b  8b5658                mov       edx,[esi+58h]
  004c3c7e  8b4e54                mov       ecx,[esi+54h]
  004c3c81  c1f80c                sar       eax,0Ch
  004c3c84  03d0                  add       edx,eax
  004c3c86  8b06                  mov       eax,[esi]
  004c3c88  81c100e00100          add       ecx,1E000h
  004c3c8e  0c01                  or        al,1
  004c3c90  895658                mov       [esi+58h],edx
  004c3c93  894e54                mov       [esi+54h],ecx
  004c3c96  8906                  mov       [esi],eax
  004c3c98  e919010000            jmp       004C3DB6h
  004c3c9d  668b4e12              mov       cx,[esi+12h]
  004c3ca1  6685c9                test      cx,cx
  004c3ca4  7507                  jne       short 004C3CADh
  004c3ca6  c7465c00000000        mov       dword [esi+5Ch],0

LAB_004c3cad:
  004c3cad  6683f90a              cmp       cx,0Ah
  004c3cb1  bf03000000            mov       edi,3
  004c3cb6  0f8da4000000          jge       near 004C3D60h
  004c3cbc  66837e1a09            cmp       word [esi+1Ah],9
  004c3cc1  66897e18              mov       [esi+18h],di
  004c3cc5  7c06                  jl        short 004C3CCDh
  004c3cc7  66c7461a0800          mov       word [esi+1Ah],8

LAB_004c3ccd:
  004c3ccd  41                    inc       ecx
  004c3cce  663bcb                cmp       cx,bx
  004c3cd1  66894e12              mov       [esi+12h],cx
  004c3cd5  7504                  jne       short 004C3CDBh
  004c3cd7  0c02                  or        al,2
  004c3cd9  8906                  mov       [esi],eax

LAB_004c3cdb:
  004c3cdb  6683f90a              cmp       cx,0Ah
  004c3cdf  7543                  jne       short 004C3D24h
  004c3ce1  8b542468              mov       edx,[esp+68h]
  004c3ce5  8b4e54                mov       ecx,[esi+54h]
  004c3ce8  81c100400100          add       ecx,14000h
  004c3cee  66c746126400          mov       word [esi+12h],64h
  004c3cf4  8d8200010000          lea       eax,[edx+100h]
  004c3cfa  894e54                mov       [esi+54h],ecx
  004c3cfd  8d4e1c                lea       ecx,[esi+1Ch]
  004c3d00  8d0480                lea       eax,[eax+eax*4]
  004c3d03  51                    push      ecx
  004c3d04  c1e008                shl       eax,8
  004c3d07  89465c                mov       [esi+5Ch],eax
  004c3d0a  8b06                  mov       eax,[esi]
  004c3d0c  6800120000            push      1200h
  004c3d11  0bc7                  or        eax,edi
  004c3d13  6800060000            push      600h
  004c3d18  6a24                  push      24h
  004c3d1a  8906                  mov       [esi],eax
  004c3d1c  e8ff9d0700            call      0053DB20h
  004c3d21  83c410                add       esp,10h

LAB_004c3d24:
  004c3d24  8b442410              mov       eax,[esp+10h]
  004c3d28  8d9000010000          lea       edx,[eax+100h]
  004c3d2e  81e2ff0f0000          and       edx,0FFFh
  004c3d34  81fa00020000          cmp       edx,200h
  004c3d3a  7e24                  jle       short 004C3D60h
  004c3d3c  25ff0f0000            and       eax,0FFFh
  004c3d41  3d00080000            cmp       eax,800h
  004c3d46  7d09                  jge       short 004C3D51h
  004c3d48  8b442468              mov       eax,[esp+68h]
  004c3d4c  83c032                add       eax,32h
  004c3d4f  eb0b                  jmp       short 004C3D5Ch

LAB_004c3d51:
  004c3d51  8b4c2468              mov       ecx,[esp+68h]
  004c3d55  b8ceffffff            mov       eax,0FFFFFFCEh
  004c3d5a  2bc1                  sub       eax,ecx

LAB_004c3d5c:
  004c3d5c  66014632              add       [esi+32h],ax

LAB_004c3d60:
  004c3d60  66837e1264            cmp       word [esi+12h],64h
  004c3d65  b965000000            mov       ecx,65h
  004c3d6a  752e                  jne       short 004C3D9Ah
  004c3d6c  8b465c                mov       eax,[esi+5Ch]
  004c3d6f  66897e18              mov       [esi+18h],di
  004c3d73  8bd0                  mov       edx,eax
  004c3d75  c1fa02                sar       edx,2
  004c3d78  2bc2                  sub       eax,edx
  004c3d7a  66837e1a0e            cmp       word [esi+1Ah],0Eh
  004c3d7f  89465c                mov       [esi+5Ch],eax
  004c3d82  7c06                  jl        short 004C3D8Ah
  004c3d84  66c7461a0d00          mov       word [esi+1Ah],0Dh

LAB_004c3d8a:
  004c3d8a  2500f0ffff            and       eax,0FFFFF000h
  004c3d8f  3d00a00000            cmp       eax,0A000h
  004c3d94  7d04                  jge       short 004C3D9Ah
  004c3d96  66894e12              mov       [esi+12h],cx

LAB_004c3d9a:
  004c3d9a  66394e12              cmp       [esi+12h],cx
  004c3d9e  7516                  jne       short 004C3DB6h
  004c3da0  8b0e                  mov       ecx,[esi]
  004c3da2  66897e18              mov       [esi+18h],di
  004c3da6  83e1fd                and       ecx,0FFFFFFFDh
  004c3da9  66837e1a0f            cmp       word [esi+1Ah],0Fh
  004c3dae  890e                  mov       [esi],ecx
  004c3db0  7504                  jne       short 004C3DB6h

LAB_004c3db2:
  004c3db2  c6464300              mov       byte [esi+43h],0

LAB_004c3db6:
  004c3db6  0fbf4632              movsx     eax,word [esi+32h]
  004c3dba  50                    push      eax
  004c3dbb  e85c1c0800            call      00545A1Ch
  004c3dc0  0fbf4e32              movsx     ecx,word [esi+32h]
  004c3dc4  51                    push      ecx
  004c3dc5  89442440              mov       [esp+40h],eax
  004c3dc9  e8481c0800            call      00545A16h
  004c3dce  8b5650                mov       edx,[esi+50h]
  004c3dd1  89442444              mov       [esp+44h],eax
  004c3dd5  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  004c3ddc  8bc2                  mov       eax,edx
  004c3dde  8bfa                  mov       edi,edx
  004c3de0  c1f808                sar       eax,8
  004c3de3  8b4c2418              mov       ecx,[esp+18h]
  004c3de7  2bf8                  sub       edi,eax
  004c3de9  c1fa03                sar       edx,3
  004c3dec  0faf4e5c              imul      ecx,[esi+5Ch]
  004c3df0  2bfa                  sub       edi,edx
  004c3df2  8b5654                mov       edx,[esi+54h]
  004c3df5  8bc5                  mov       eax,ebp
  004c3df7  8bda                  mov       ebx,edx
  004c3df9  c1f808                sar       eax,8
  004c3dfc  c1fb08                sar       ebx,8
  004c3dff  2bc3                  sub       eax,ebx
  004c3e01  897e50                mov       [esi+50h],edi
  004c3e04  2bc5                  sub       eax,ebp
  004c3e06  56                    push      esi
  004c3e07  03c2                  add       eax,edx
  004c3e09  8b5658                mov       edx,[esi+58h]
  004c3e0c  8bda                  mov       ebx,edx
  004c3e0e  8bea                  mov       ebp,edx
  004c3e10  c1fb03                sar       ebx,3
  004c3e13  2beb                  sub       ebp,ebx
  004c3e15  8b5e20                mov       ebx,[esi+20h]
  004c3e18  c1fa08                sar       edx,8
  004c3e1b  c1f90c                sar       ecx,0Ch
  004c3e1e  2bea                  sub       ebp,edx
  004c3e20  8bd1                  mov       edx,ecx
  004c3e22  0faf542440            imul      edx,[esp+40h]
  004c3e27  0faf4c2448            imul      ecx,[esp+48h]
  004c3e2c  c1fa0c                sar       edx,0Ch
  004c3e2f  c1f90c                sar       ecx,0Ch
  004c3e32  894654                mov       [esi+54h],eax
  004c3e35  03d7                  add       edx,edi
  004c3e37  8b7e1c                mov       edi,[esi+1Ch]
  004c3e3a  03d8                  add       ebx,eax
  004c3e3c  8b4624                mov       eax,[esi+24h]
  004c3e3f  03cd                  add       ecx,ebp
  004c3e41  03fa                  add       edi,edx
  004c3e43  03c1                  add       eax,ecx
  004c3e45  896e58                mov       [esi+58h],ebp
  004c3e48  897e1c                mov       [esi+1Ch],edi
  004c3e4b  895e20                mov       [esi+20h],ebx
  004c3e4e  894624                mov       [esi+24h],eax
  004c3e51  e8eafa0600            call      00533940h
  004c3e56  8b06                  mov       eax,[esi]
  004c3e58  8bbe90000000          mov       edi,[esi+90h]
  004c3e5e  8bc8                  mov       ecx,eax
  004c3e60  83c404                add       esp,4
  004c3e63  81e7fffcffff          and       edi,0FFFFFCFFh
  004c3e69  83e101                and       ecx,1
  004c3e6c  89be90000000          mov       [esi+90h],edi
  004c3e72  740a                  je        short 004C3E7Eh
  004c3e74  8b5660                mov       edx,[esi+60h]
  004c3e77  8b5e20                mov       ebx,[esi+20h]
  004c3e7a  3bda                  cmp       ebx,edx
  004c3e7c  7e18                  jle       short 004C3E96h

LAB_004c3e7e:
  004c3e7e  33db                  xor       ebx,ebx
  004c3e80  3bcb                  cmp       ecx,ebx
  004c3e82  7536                  jne       short 004C3EBAh
  004c3e84  8b6e20                mov       ebp,[esi+20h]
  004c3e87  8b5660                mov       edx,[esi+60h]
  004c3e8a  81ed00c00000          sub       ebp,0C000h
  004c3e90  3bea                  cmp       ebp,edx
  004c3e92  7f26                  jg        short 004C3EBAh
  004c3e94  eb02                  jmp       short 004C3E98h

LAB_004c3e96:
  004c3e96  33db                  xor       ebx,ebx

LAB_004c3e98:
  004c3e98  f7c700a00000          test      edi,0A000h
  004c3e9e  751a                  jne       short 004C3EBAh
  004c3ea0  a802                  test      al,2
  004c3ea2  895620                mov       [esi+20h],edx
  004c3ea5  750c                  jne       short 004C3EB3h
  004c3ea7  3bcb                  cmp       ecx,ebx
  004c3ea9  7408                  je        short 004C3EB3h
  004c3eab  385e43                cmp       [esi+43h],bl
  004c3eae  7403                  je        short 004C3EB3h
  004c3eb0  885e43                mov       [esi+43h],bl

LAB_004c3eb3:
  004c3eb3  24fe                  and       al,0FEh
  004c3eb5  895e54                mov       [esi+54h],ebx
  004c3eb8  8906                  mov       [esi],eax

LAB_004c3eba:
  004c3eba  f7c782ad0020          test      edi,2000AD82h
  004c3ec0  7410                  je        short 004C3ED2h
  004c3ec2  f60601                test      byte [esi],1
  004c3ec5  7508                  jne       short 004C3ECFh
  004c3ec7  c6464201              mov       byte [esi+42h],1
  004c3ecb  c6464301              mov       byte [esi+43h],1

LAB_004c3ecf:
  004c3ecf  895e5c                mov       [esi+5Ch],ebx

LAB_004c3ed2:
  004c3ed2  6aff                  push      0FFFFFFFFh
  004c3ed4  6aff                  push      0FFFFFFFFh
  004c3ed6  6aff                  push      0FFFFFFFFh
  004c3ed8  56                    push      esi
  004c3ed9  e832f9ffff            call      004C3810h
  004c3ede  8b442428              mov       eax,[esp+28h]
  004c3ee2  83c410                add       esp,10h
  004c3ee5  3bc3                  cmp       eax,ebx
  004c3ee7  7511                  jne       short 004C3EFAh
  004c3ee9  668b442452            mov       ax,[esp+52h]
  004c3eee  66894632              mov       [esi+32h],ax
  004c3ef2  5f                    pop       edi
  004c3ef3  5e                    pop       esi
  004c3ef4  5d                    pop       ebp
  004c3ef5  5b                    pop       ebx
  004c3ef6  83c454                add       esp,54h
  004c3ef9  c3                    ret

LAB_004c3efa:
  004c3efa  385e43                cmp       [esi+43h],bl
  004c3efd  0f85dc020000          jne       near 004C41DFh
  004c3f03  8b466c                mov       eax,[esi+6Ch]
  004c3f06  8b4e1c                mov       ecx,[esi+1Ch]
  004c3f09  8b5624                mov       edx,[esi+24h]
  004c3f0c  8b781c                mov       edi,[eax+1Ch]
  004c3f0f  8b6824                mov       ebp,[eax+24h]
  004c3f12  2bf9                  sub       edi,ecx
  004c3f14  2bea                  sub       ebp,edx
  004c3f16  c1ff0c                sar       edi,0Ch
  004c3f19  c1fd0c                sar       ebp,0Ch
  004c3f1c  8bcf                  mov       ecx,edi
  004c3f1e  8bd5                  mov       edx,ebp
  004c3f20  0fafcf                imul      ecx,edi
  004c3f23  0fafd5                imul      edx,ebp
  004c3f26  03ca                  add       ecx,edx
  004c3f28  51                    push      ecx
  004c3f29  e8e21a0800            call      00545A10h
  004c3f2e  55                    push      ebp
  004c3f2f  57                    push      edi
  004c3f30  89442424              mov       [esp+24h],eax
  004c3f34  e8e91a0800            call      00545A22h
  004c3f39  668b4e32              mov       cx,[esi+32h]
  004c3f3d  2bc1                  sub       eax,ecx
  004c3f3f  25ff0f0000            and       eax,0FFFh
  004c3f44  89442410              mov       [esp+10h],eax
  004c3f48  8b44241c              mov       eax,[esp+1Ch]
  004c3f4c  3dfa000000            cmp       eax,0FAh
  004c3f51  0f8d19010000          jge       near 004C4070h
  004c3f57  e8e4310800            call      00547140h
  004c3f5c  99                    cdq
  004c3f5d  b964000000            mov       ecx,64h
  004c3f62  f7f9                  idiv      ecx
  004c3f64  83fa32                cmp       edx,32h
  004c3f67  0f8d9d000000          jge       near 004C400Ah
  004c3f6d  8b7c2414              mov       edi,[esp+14h]
  004c3f71  8b542410              mov       edx,[esp+10h]
  004c3f75  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004c3f7c  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004c3f83  25ff0f0000            and       eax,0FFFh
  004c3f88  3bc1                  cmp       eax,ecx
  004c3f8a  7f3a                  jg        short 004C3FC6h
  004c3f8c  e8af310800            call      00547140h
  004c3f91  99                    cdq
  004c3f92  8d4c3f01              lea       ecx,[edi+edi+1]
  004c3f96  f7f9                  idiv      ecx
  004c3f98  8b44241c              mov       eax,[esp+1Ch]
  004c3f9c  83c264                add       edx,64h
  004c3f9f  3bc2                  cmp       eax,edx
  004c3fa1  7d23                  jge       short 004C3FC6h
  004c3fa3  8b16                  mov       edx,[esi]
  004c3fa5  c7461803000000        mov       dword [esi+18h],3
  004c3fac  81e2fffffffe          and       edx,0FEFFFFFFh
  004c3fb2  c6464301              mov       byte [esi+43h],1
  004c3fb6  83ca02                or        edx,2
  004c3fb9  895e5c                mov       [esi+5Ch],ebx
  004c3fbc  8916                  mov       [esi],edx
  004c3fbe  5f                    pop       edi
  004c3fbf  5e                    pop       esi
  004c3fc0  5d                    pop       ebp
  004c3fc1  5b                    pop       ebx
  004c3fc2  83c454                add       esp,54h
  004c3fc5  c3                    ret

LAB_004c3fc6:
  004c3fc6  8b442410              mov       eax,[esp+10h]
  004c3fca  8d14fd00040000        lea       edx,[edi*8+400h]
  004c3fd1  8d8cb800020000        lea       ecx,[eax+edi*4+200h]
  004c3fd8  81e1ff0f0000          and       ecx,0FFFh
  004c3fde  3bca                  cmp       ecx,edx
  004c3fe0  0f8fa3010000          jg        near 004C4189h
  004c3fe6  8b06                  mov       eax,[esi]
  004c3fe8  c6464207              mov       byte [esi+42h],7
  004c3fec  25fffffffe            and       eax,0FEFFFFFFh
  004c3ff1  c6464301              mov       byte [esi+43h],1
  004c3ff5  8906                  mov       [esi],eax
  004c3ff7  c7461803000000        mov       dword [esi+18h],3
  004c3ffe  66895e12              mov       [esi+12h],bx
  004c4002  5f                    pop       edi
  004c4003  5e                    pop       esi
  004c4004  5d                    pop       ebp
  004c4005  5b                    pop       ebx
  004c4006  83c454                add       esp,54h
  004c4009  c3                    ret

LAB_004c400a:
  004c400a  83fa46                cmp       edx,46h
  004c400d  0f8c02010000          jl        near 004C4115h
  004c4013  83fa50                cmp       edx,50h
  004c4016  0f8db2010000          jge       near 004C41CEh
  004c401c  c6464203              mov       byte [esi+42h],3
  004c4020  e81b310800            call      00547140h
  004c4025  99                    cdq
  004c4026  b90a000000            mov       ecx,0Ah
  004c402b  f7f9                  idiv      ecx
  004c402d  8b442414              mov       eax,[esp+14h]
  004c4031  8b4c2468              mov       ecx,[esp+68h]
  004c4035  d1f8                  sar       eax,1
  004c4037  8db980000000          lea       edi,[ecx+80h]
  004c403d  c1e704                shl       edi,4
  004c4040  02d0                  add       dl,al
  004c4042  80c214                add       dl,14h
  004c4045  885643                mov       [esi+43h],dl
  004c4048  e8f3300800            call      00547140h
  004c404d  99                    cdq
  004c404e  f7ff                  idiv      edi
  004c4050  d1ff                  sar       edi,1
  004c4052  2bd7                  sub       edx,edi
  004c4054  8b7c2410              mov       edi,[esp+10h]
  004c4058  03d7                  add       edx,edi
  004c405a  66035632              add       dx,[esi+32h]
  004c405e  81c200080000          add       edx,800h
  004c4064  66895610              mov       [esi+10h],dx
  004c4068  5f                    pop       edi
  004c4069  5e                    pop       esi
  004c406a  5d                    pop       ebp
  004c406b  5b                    pop       ebx
  004c406c  83c454                add       esp,54h
  004c406f  c3                    ret

LAB_004c4070:
  004c4070  3d58020000            cmp       eax,258h
  004c4075  0f8dc7000000          jge       near 004C4142h
  004c407b  e8c0300800            call      00547140h
  004c4080  99                    cdq
  004c4081  b964000000            mov       ecx,64h
  004c4086  f7f9                  idiv      ecx
  004c4088  83fa1e                cmp       edx,1Eh
  004c408b  7d2c                  jge       short 004C40B9h
  004c408d  8b542410              mov       edx,[esp+10h]
  004c4091  81c200040000          add       edx,400h
  004c4097  81e2ff0f0000          and       edx,0FFFh
  004c409d  81fa00080000          cmp       edx,800h
  004c40a3  0f8cab000000          jl        near 004C4154h
  004c40a9  c6464201              mov       byte [esi+42h],1
  004c40ad  c6464301              mov       byte [esi+43h],1
  004c40b1  5f                    pop       edi
  004c40b2  5e                    pop       esi
  004c40b3  5d                    pop       ebp
  004c40b4  5b                    pop       ebx
  004c40b5  83c454                add       esp,54h
  004c40b8  c3                    ret

LAB_004c40b9:
  004c40b9  83fa32                cmp       edx,32h
  004c40bc  7d4e                  jge       short 004C410Ch
  004c40be  c6464203              mov       byte [esi+42h],3
  004c40c2  e879300800            call      00547140h
  004c40c7  99                    cdq
  004c40c8  b90a000000            mov       ecx,0Ah
  004c40cd  f7f9                  idiv      ecx
  004c40cf  8b442414              mov       eax,[esp+14h]
  004c40d3  8b4c2468              mov       ecx,[esp+68h]
  004c40d7  d1f8                  sar       eax,1
  004c40d9  8db980000000          lea       edi,[ecx+80h]
  004c40df  c1e704                shl       edi,4
  004c40e2  02d0                  add       dl,al
  004c40e4  80c214                add       dl,14h
  004c40e7  885643                mov       [esi+43h],dl
  004c40ea  e851300800            call      00547140h
  004c40ef  99                    cdq
  004c40f0  f7ff                  idiv      edi
  004c40f2  8b4c2410              mov       ecx,[esp+10h]
  004c40f6  d1ff                  sar       edi,1
  004c40f8  2bd7                  sub       edx,edi
  004c40fa  03d1                  add       edx,ecx
  004c40fc  66035632              add       dx,[esi+32h]
  004c4100  66895610              mov       [esi+10h],dx
  004c4104  5f                    pop       edi
  004c4105  5e                    pop       esi
  004c4106  5d                    pop       ebp
  004c4107  5b                    pop       ebx
  004c4108  83c454                add       esp,54h
  004c410b  c3                    ret

LAB_004c410c:
  004c410c  83fa5a                cmp       edx,5Ah
  004c410f  0f8db9000000          jge       near 004C41CEh

LAB_004c4115:
  004c4115  c6464202              mov       byte [esi+42h],2
  004c4119  e822300800            call      00547140h
  004c411e  99                    cdq
  004c411f  b90a000000            mov       ecx,0Ah
  004c4124  66c746106400          mov       word [esi+10h],64h
  004c412a  f7f9                  idiv      ecx
  004c412c  8b442414              mov       eax,[esp+14h]
  004c4130  d1f8                  sar       eax,1
  004c4132  02d0                  add       dl,al
  004c4134  80c214                add       dl,14h
  004c4137  885643                mov       [esi+43h],dl
  004c413a  5f                    pop       edi
  004c413b  5e                    pop       esi
  004c413c  5d                    pop       ebp
  004c413d  5b                    pop       ebx
  004c413e  83c454                add       esp,54h
  004c4141  c3                    ret

LAB_004c4142:
  004c4142  e8f92f0800            call      00547140h
  004c4147  99                    cdq
  004c4148  b964000000            mov       ecx,64h
  004c414d  f7f9                  idiv      ecx
  004c414f  83fa0a                cmp       edx,0Ah
  004c4152  7d30                  jge       short 004C4184h

LAB_004c4154:
  004c4154  885e42                mov       [esi+42h],bl
  004c4157  e8e42f0800            call      00547140h
  004c415c  99                    cdq
  004c415d  b90a000000            mov       ecx,0Ah
  004c4162  895e18                mov       [esi+18h],ebx
  004c4165  f7f9                  idiv      ecx
  004c4167  8b442414              mov       eax,[esp+14h]
  004c416b  c1f802                sar       eax,2
  004c416e  02d0                  add       dl,al
  004c4170  8b06                  mov       eax,[esi]
  004c4172  0d00000001            or        eax,1000000h
  004c4177  885643                mov       [esi+43h],dl
  004c417a  8906                  mov       [esi],eax
  004c417c  5f                    pop       edi
  004c417d  5e                    pop       esi
  004c417e  5d                    pop       ebp
  004c417f  5b                    pop       ebx
  004c4180  83c454                add       esp,54h
  004c4183  c3                    ret

LAB_004c4184:
  004c4184  83fa32                cmp       edx,32h
  004c4187  7d10                  jge       short 004C4199h

LAB_004c4189:
  004c4189  c6464201              mov       byte [esi+42h],1
  004c418d  c6464301              mov       byte [esi+43h],1
  004c4191  5f                    pop       edi
  004c4192  5e                    pop       esi
  004c4193  5d                    pop       ebp
  004c4194  5b                    pop       ebx
  004c4195  83c454                add       esp,54h
  004c4198  c3                    ret

LAB_004c4199:
  004c4199  83fa5a                cmp       edx,5Ah
  004c419c  7d30                  jge       short 004C41CEh
  004c419e  c6464203              mov       byte [esi+42h],3
  004c41a2  e8992f0800            call      00547140h
  004c41a7  99                    cdq
  004c41a8  b90a000000            mov       ecx,0Ah
  004c41ad  f7f9                  idiv      ecx
  004c41af  8b442414              mov       eax,[esp+14h]
  004c41b3  d1f8                  sar       eax,1
  004c41b5  02d0                  add       dl,al
  004c41b7  80c214                add       dl,14h
  004c41ba  885643                mov       [esi+43h],dl
  004c41bd  e87e2f0800            call      00547140h
  004c41c2  66894610              mov       [esi+10h],ax
  004c41c6  5f                    pop       edi
  004c41c7  5e                    pop       esi
  004c41c8  5d                    pop       ebp
  004c41c9  5b                    pop       ebx
  004c41ca  83c454                add       esp,54h
  004c41cd  c3                    ret

LAB_004c41ce:
  004c41ce  c6464206              mov       byte [esi+42h],6
  004c41d2  c6464301              mov       byte [esi+43h],1
  004c41d6  e8652f0800            call      00547140h
  004c41db  66894610              mov       [esi+10h],ax

LAB_004c41df:
  004c41df  5f                    pop       edi
  004c41e0  5e                    pop       esi
  004c41e1  5d                    pop       ebp
  004c41e2  5b                    pop       ebx
  004c41e3  83c454                add       esp,54h
  004c41e6  c3                    ret
  004c41e7  90                    nop
  004c41e8  4e                    dec       esi
  004c41e9  3a4c0078              cmp       cl,[eax+eax+78h]
  004c41ed  3a4c00da              cmp       cl,[eax+eax-26h]
  004c41f1  3a4c0064              cmp       cl,[eax+eax+64h]
  004c41f5  3b4c00b6              cmp       ecx,[eax+eax-4Ah]
  004c41f9  3d4c00b63d            cmp       eax,3DB6004Ch
  004c41fe  4c                    dec       esp
  004c41ff  0028                  add       [eax],ch
  004c4201  3c4c                  cmp       al,4Ch
  004c4203  009d3c4c0090          add       [ebp-6FFFB3C4h],bl
  004c4209  90                    nop
  004c420a  90                    nop
  004c420b  90                    nop
  004c420c  90                    nop
  004c420d  90                    nop
  004c420e  90                    nop
  004c420f  90                    nop

55 cible(s) de saut interne : 0x004c3889, 0x004c3940, 0x004c3961, 0x004c397a, 0x004c39f0, 0x004c3a01, 0x004c3a05, 0x004c3a1b, 0x004c3a2a, 0x004c3a3a, 0x004c3aaa, 0x004c3ab5, 0x004c3ae4, 0x004c3ae8, 0x004c3af4, 0x004c3b00, 0x004c3b15, 0x004c3b45, 0x004c3b4c, 0x004c3b50, 0x004c3b70, 0x004c3b85, 0x004c3c0d, 0x004c3cad, 0x004c3ccd, 0x004c3cdb, 0x004c3d24, 0x004c3d51, 0x004c3d5c, 0x004c3d60, 0x004c3d8a, 0x004c3d9a, 0x004c3db2, 0x004c3db6, 0x004c3e7e, 0x004c3e96, 0x004c3e98, 0x004c3eb3, 0x004c3eba, 0x004c3ecf, 0x004c3ed2, 0x004c3efa, 0x004c3fc6, 0x004c400a, 0x004c4070, 0x004c40b9, 0x004c410c, 0x004c4115, 0x004c4142, 0x004c4154, 0x004c4184, 0x004c4189, 0x004c4199, 0x004c41ce, 0x004c41df
