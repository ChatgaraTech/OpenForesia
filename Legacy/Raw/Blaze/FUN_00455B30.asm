Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00455b30  section .text  offset fichier 0x54f30  1246 octets

  00455b30  83ec24                sub       esp,24h
  00455b33  53                    push      ebx
  00455b34  55                    push      ebp
  00455b35  56                    push      esi
  00455b36  8b742434              mov       esi,[esp+34h]
  00455b3a  57                    push      edi
  00455b3b  33ff                  xor       edi,edi
  00455b3d  66397e10              cmp       [esi+10h],di
  00455b41  7556                  jne       short 00455B99h
  00455b43  b800a00000            mov       eax,0A000h
  00455b48  897e50                mov       [esi+50h],edi
  00455b4b  66898682000000        mov       [esi+82h],ax
  00455b52  66898686000000        mov       [esi+86h],ax
  00455b59  6689868a000000        mov       [esi+8Ah],ax
  00455b60  6689868e000000        mov       [esi+8Eh],ax
  00455b67  8b4620                mov       eax,[esi+20h]
  00455b6a  897e54                mov       [esi+54h],edi
  00455b6d  897e58                mov       [esi+58h],edi
  00455b70  897e44                mov       [esi+44h],edi
  00455b73  897e48                mov       [esi+48h],edi
  00455b76  897e4c                mov       [esi+4Ch],edi
  00455b79  66c746100100          mov       word [esi+10h],1
  00455b7f  894664                mov       [esi+64h],eax
  00455b82  894660                mov       [esi+60h],eax
  00455b85  a1c07b5f00            mov       eax,[5F7BC0h]
  00455b8a  6800800000            push      8000h
  00455b8f  56                    push      esi
  00455b90  50                    push      eax
  00455b91  e83ad50d00            call      005330D0h
  00455b96  83c40c                add       esp,0Ch

LAB_00455b99:
  00455b99  8b4660                mov       eax,[esi+60h]
  00455b9c  8b4e7c                mov       ecx,[esi+7Ch]
  00455b9f  8b16                  mov       edx,[esi]
  00455ba1  89442414              mov       [esp+14h],eax
  00455ba5  8b4624                mov       eax,[esi+24h]
  00455ba8  894c2438              mov       [esp+38h],ecx
  00455bac  8b4e64                mov       ecx,[esi+64h]
  00455baf  668bae90000000        mov       bp,[esi+90h]
  00455bb6  89442420              mov       [esp+20h],eax
  00455bba  8b8688000000          mov       eax,[esi+88h]
  00455bc0  89542410              mov       [esp+10h],edx
  00455bc4  8b561c                mov       edx,[esi+1Ch]
  00455bc7  894c2418              mov       [esp+18h],ecx
  00455bcb  8b8e80000000          mov       ecx,[esi+80h]
  00455bd1  8944242c              mov       [esp+2Ch],eax
  00455bd5  8a4609                mov       al,[esi+9]
  00455bd8  8954241c              mov       [esp+1Ch],edx
  00455bdc  8b9684000000          mov       edx,[esi+84h]
  00455be2  894c2424              mov       [esp+24h],ecx
  00455be6  8b8e8c000000          mov       ecx,[esi+8Ch]
  00455bec  a80f                  test      al,0Fh
  00455bee  89542428              mov       [esp+28h],edx
  00455bf2  894c2430              mov       [esp+30h],ecx
  00455bf6  7428                  je        short 00455C20h
  00455bf8  33c9                  xor       ecx,ecx

LAB_00455bfa:
  00455bfa  8ad0                  mov       dl,al
  00455bfc  d2ea                  shr       dl,cl
  00455bfe  f6c201                test      dl,1
  00455c01  7401                  je        short 00455C04h
  00455c03  47                    inc       edi

LAB_00455c04:
  00455c04  41                    inc       ecx
  00455c05  6683f903              cmp       cx,3
  00455c09  7eef                  jle       short 00455BFAh
  00455c0b  8b4650                mov       eax,[esi+50h]
  00455c0e  0fbfcf                movsx     ecx,di
  00455c11  99                    cdq
  00455c12  f7f9                  idiv      ecx
  00455c14  894650                mov       [esi+50h],eax
  00455c17  8b4658                mov       eax,[esi+58h]
  00455c1a  99                    cdq
  00455c1b  f7f9                  idiv      ecx
  00455c1d  894658                mov       [esi+58h],eax

LAB_00455c20:
  00455c20  8b7e54                mov       edi,[esi+54h]
  00455c23  8b4644                mov       eax,[esi+44h]
  00455c26  8b5658                mov       edx,[esi+58h]
  00455c29  8b4e48                mov       ecx,[esi+48h]
  00455c2c  d1ff                  sar       edi,1
  00455c2e  c1f80c                sar       eax,0Ch
  00455c31  897e54                mov       [esi+54h],edi
  00455c34  8bf8                  mov       edi,eax
  00455c36  d1fa                  sar       edx,1
  00455c38  0faff8                imul      edi,eax
  00455c3b  895658                mov       [esi+58h],edx
  00455c3e  8b564c                mov       edx,[esi+4Ch]
  00455c41  c1f90c                sar       ecx,0Ch
  00455c44  8bc1                  mov       eax,ecx
  00455c46  8b5e50                mov       ebx,[esi+50h]
  00455c49  c1fa0c                sar       edx,0Ch
  00455c4c  0fafc1                imul      eax,ecx
  00455c4f  8bca                  mov       ecx,edx
  00455c51  03f8                  add       edi,eax
  00455c53  0fafca                imul      ecx,edx
  00455c56  03f9                  add       edi,ecx
  00455c58  d1fb                  sar       ebx,1
  00455c5a  57                    push      edi
  00455c5b  895e50                mov       [esi+50h],ebx
  00455c5e  e8adfd0e00            call      00545A10h
  00455c63  8b4e54                mov       ecx,[esi+54h]
  00455c66  8b5658                mov       edx,[esi+58h]
  00455c69  8bf8                  mov       edi,eax
  00455c6b  8b4650                mov       eax,[esi+50h]
  00455c6e  c1f80c                sar       eax,0Ch
  00455c71  8bd8                  mov       ebx,eax
  00455c73  c1f90c                sar       ecx,0Ch
  00455c76  0fafd8                imul      ebx,eax
  00455c79  8bc1                  mov       eax,ecx
  00455c7b  c1fa0c                sar       edx,0Ch
  00455c7e  0fafc1                imul      eax,ecx
  00455c81  8bca                  mov       ecx,edx
  00455c83  03d8                  add       ebx,eax
  00455c85  0fafca                imul      ecx,edx
  00455c88  03d9                  add       ebx,ecx
  00455c8a  53                    push      ebx
  00455c8b  e880fd0e00            call      00545A10h
  00455c90  3bf8                  cmp       edi,eax
  00455c92  7d0c                  jge       short 00455CA0h
  00455c94  8b5650                mov       edx,[esi+50h]
  00455c97  8b4658                mov       eax,[esi+58h]
  00455c9a  895644                mov       [esi+44h],edx
  00455c9d  89464c                mov       [esi+4Ch],eax

LAB_00455ca0:
  00455ca0  8b5644                mov       edx,[esi+44h]
  00455ca3  b900e00100            mov       ecx,1E000h
  00455ca8  81e200f0ffff          and       edx,0FFFFF000h
  00455cae  3bd1                  cmp       edx,ecx
  00455cb0  7e03                  jle       short 00455CB5h
  00455cb2  894e44                mov       [esi+44h],ecx

LAB_00455cb5:
  00455cb5  8b5644                mov       edx,[esi+44h]
  00455cb8  b80020feff            mov       eax,0FFFE2000h
  00455cbd  81e200f0ffff          and       edx,0FFFFF000h
  00455cc3  3bd0                  cmp       edx,eax
  00455cc5  7d03                  jge       short 00455CCAh
  00455cc7  894644                mov       [esi+44h],eax

LAB_00455cca:
  00455cca  8b564c                mov       edx,[esi+4Ch]
  00455ccd  81e200f0ffff          and       edx,0FFFFF000h
  00455cd3  3bd1                  cmp       edx,ecx
  00455cd5  7e03                  jle       short 00455CDAh
  00455cd7  894e4c                mov       [esi+4Ch],ecx

LAB_00455cda:
  00455cda  8b4e4c                mov       ecx,[esi+4Ch]
  00455cdd  81e100f0ffff          and       ecx,0FFFFF000h
  00455ce3  3bc8                  cmp       ecx,eax
  00455ce5  7d03                  jge       short 00455CEAh
  00455ce7  89464c                mov       [esi+4Ch],eax

LAB_00455cea:
  00455cea  8b4e44                mov       ecx,[esi+44h]
  00455ced  8b7e1c                mov       edi,[esi+1Ch]
  00455cf0  8b5e4c                mov       ebx,[esi+4Ch]
  00455cf3  8b4624                mov       eax,[esi+24h]
  00455cf6  8b5620                mov       edx,[esi+20h]
  00455cf9  03f9                  add       edi,ecx
  00455cfb  897e1c                mov       [esi+1Ch],edi
  00455cfe  8b7e48                mov       edi,[esi+48h]
  00455d01  03c3                  add       eax,ebx
  00455d03  03d7                  add       edx,edi
  00455d05  894624                mov       [esi+24h],eax
  00455d08  895620                mov       [esi+20h],edx
  00455d0b  8bc1                  mov       eax,ecx
  00455d0d  8bd3                  mov       edx,ebx
  00455d0f  c1f802                sar       eax,2
  00455d12  c1fa02                sar       edx,2
  00455d15  85c0                  test      eax,eax
  00455d17  7502                  jne       short 00455D1Bh
  00455d19  8bc1                  mov       eax,ecx

LAB_00455d1b:
  00455d1b  8b4e44                mov       ecx,[esi+44h]
  00455d1e  2bc8                  sub       ecx,eax
  00455d20  85d2                  test      edx,edx
  00455d22  894e44                mov       [esi+44h],ecx
  00455d25  7502                  jne       short 00455D29h
  00455d27  8bd3                  mov       edx,ebx

LAB_00455d29:
  00455d29  8b464c                mov       eax,[esi+4Ch]
  00455d2c  8bdf                  mov       ebx,edi
  00455d2e  2bc2                  sub       eax,edx
  00455d30  8bd1                  mov       edx,ecx
  00455d32  c1fa08                sar       edx,8
  00455d35  2bca                  sub       ecx,edx
  00455d37  56                    push      esi
  00455d38  894e44                mov       [esi+44h],ecx
  00455d3b  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00455d42  8bd1                  mov       edx,ecx
  00455d44  c1fa08                sar       edx,8
  00455d47  c1fb08                sar       ebx,8
  00455d4a  2bd3                  sub       edx,ebx
  00455d4c  2bd1                  sub       edx,ecx
  00455d4e  8bc8                  mov       ecx,eax
  00455d50  c1f908                sar       ecx,8
  00455d53  03d7                  add       edx,edi
  00455d55  2bc1                  sub       eax,ecx
  00455d57  895648                mov       [esi+48h],edx
  00455d5a  89464c                mov       [esi+4Ch],eax
  00455d5d  e8dedb0d00            call      00533940h
  00455d62  8b442440              mov       eax,[esp+40h]
  00455d66  83c404                add       esp,4
  00455d69  f6c420                test      ah,20h
  00455d6c  747e                  je        short 00455DECh
  00455d6e  8b567c                mov       edx,[esi+7Ch]
  00455d71  81e5ffdf0000          and       ebp,0DFFFh
  00455d77  f6420d20              test      byte [edx+0Dh],20h
  00455d7b  746f                  je        short 00455DECh
  00455d7d  8b4c2438              mov       ecx,[esp+38h]
  00455d81  8bd5                  mov       edx,ebp
  00455d83  894e7c                mov       [esi+7Ch],ecx
  00455d86  8b4c2410              mov       ecx,[esp+10h]
  00455d8a  81e2ffff0000          and       edx,0FFFFh
  00455d90  890e                  mov       [esi],ecx
  00455d92  8b4c2418              mov       ecx,[esp+18h]
  00455d96  899690000000          mov       [esi+90h],edx
  00455d9c  8b542414              mov       edx,[esp+14h]
  00455da0  894e64                mov       [esi+64h],ecx
  00455da3  8b4c2420              mov       ecx,[esp+20h]
  00455da7  895660                mov       [esi+60h],edx
  00455daa  8b54241c              mov       edx,[esp+1Ch]
  00455dae  894e24                mov       [esi+24h],ecx
  00455db1  8b4c2428              mov       ecx,[esp+28h]
  00455db5  89561c                mov       [esi+1Ch],edx
  00455db8  8b542424              mov       edx,[esp+24h]
  00455dbc  898e84000000          mov       [esi+84h],ecx
  00455dc2  8b4c2430              mov       ecx,[esp+30h]
  00455dc6  899680000000          mov       [esi+80h],edx
  00455dcc  8b54242c              mov       edx,[esp+2Ch]
  00455dd0  898e8c000000          mov       [esi+8Ch],ecx
  00455dd6  8b4e4c                mov       ecx,[esi+4Ch]
  00455dd9  899688000000          mov       [esi+88h],edx
  00455ddf  8b5644                mov       edx,[esi+44h]
  00455de2  f7da                  neg       edx
  00455de4  f7d9                  neg       ecx
  00455de6  895644                mov       [esi+44h],edx
  00455de9  894e4c                mov       [esi+4Ch],ecx

LAB_00455dec:
  00455dec  858690000000          test      [esi+90h],eax
  00455df2  746d                  je        short 00455E61h
  00455df4  8b442410              mov       eax,[esp+10h]
  00455df8  8b4c2414              mov       ecx,[esp+14h]
  00455dfc  8b542438              mov       edx,[esp+38h]
  00455e00  8906                  mov       [esi],eax
  00455e02  8b44241c              mov       eax,[esp+1Ch]
  00455e06  894e60                mov       [esi+60h],ecx
  00455e09  8b4c2420              mov       ecx,[esp+20h]
  00455e0d  89567c                mov       [esi+7Ch],edx
  00455e10  8b542418              mov       edx,[esp+18h]
  00455e14  89461c                mov       [esi+1Ch],eax
  00455e17  8b442428              mov       eax,[esp+28h]
  00455e1b  894e24                mov       [esi+24h],ecx
  00455e1e  8b4c242c              mov       ecx,[esp+2Ch]
  00455e22  895664                mov       [esi+64h],edx
  00455e25  8b542424              mov       edx,[esp+24h]
  00455e29  898684000000          mov       [esi+84h],eax
  00455e2f  8b4644                mov       eax,[esi+44h]
  00455e32  898e88000000          mov       [esi+88h],ecx
  00455e38  8b4e4c                mov       ecx,[esi+4Ch]
  00455e3b  81e5ffff0000          and       ebp,0FFFFh
  00455e41  899680000000          mov       [esi+80h],edx
  00455e47  8b542430              mov       edx,[esp+30h]
  00455e4b  f7d8                  neg       eax
  00455e4d  f7d9                  neg       ecx
  00455e4f  89ae90000000          mov       [esi+90h],ebp
  00455e55  89968c000000          mov       [esi+8Ch],edx
  00455e5b  894644                mov       [esi+44h],eax
  00455e5e  894e4c                mov       [esi+4Ch],ecx

LAB_00455e61:
  00455e61  8b4660                mov       eax,[esi+60h]
  00455e64  8b4e20                mov       ecx,[esi+20h]
  00455e67  3bc8                  cmp       ecx,eax
  00455e69  7f26                  jg        short 00455E91h
  00455e6b  8b8e90000000          mov       ecx,[esi+90h]
  00455e71  f6c5a0                test      ch,0A0h
  00455e74  751b                  jne       short 00455E91h
  00455e76  894620                mov       [esi+20h],eax
  00455e79  8b06                  mov       eax,[esi]
  00455e7b  c1e81d                shr       eax,1Dh
  00455e7e  c7464800000000        mov       dword [esi+48h],0
  00455e85  66250100              and       ax,1
  00455e89  5f                    pop       edi
  00455e8a  5e                    pop       esi
  00455e8b  5d                    pop       ebp
  00455e8c  5b                    pop       ebx
  00455e8d  83c424                add       esp,24h
  00455e90  c3                    ret

LAB_00455e91:
  00455e91  8b06                  mov       eax,[esi]
  00455e93  66c7869a0000000030    mov       word [esi+9Ah],3000h
  00455e9c  5f                    pop       edi
  00455e9d  5e                    pop       esi
  00455e9e  c1e81d                shr       eax,1Dh
  00455ea1  5d                    pop       ebp
  00455ea2  66250100              and       ax,1
  00455ea6  5b                    pop       ebx
  00455ea7  83c424                add       esp,24h
  00455eaa  c3                    ret
  00455eab  90                    nop
  00455eac  90                    nop
  00455ead  90                    nop
  00455eae  90                    nop
  00455eaf  90                    nop
  00455eb0  53                    push      ebx
  00455eb1  56                    push      esi
  00455eb2  8b74240c              mov       esi,[esp+0Ch]
  00455eb6  33db                  xor       ebx,ebx
  00455eb8  57                    push      edi
  00455eb9  66395e10              cmp       [esi+10h],bx
  00455ebd  7504                  jne       short 00455EC3h
  00455ebf  66895e12              mov       [esi+12h],bx

LAB_00455ec3:
  00455ec3  6800080000            push      800h
  00455ec8  56                    push      esi
  00455ec9  e862fcffff            call      00455B30h
  00455ece  8b4644                mov       eax,[esi+44h]
  00455ed1  83c408                add       esp,8
  00455ed4  3bc3                  cmp       eax,ebx
  00455ed6  7d02                  jge       short 00455EDAh
  00455ed8  f7d8                  neg       eax

LAB_00455eda:
  00455eda  3d00100000            cmp       eax,1000h
  00455edf  7f10                  jg        short 00455EF1h
  00455ee1  8b464c                mov       eax,[esi+4Ch]
  00455ee4  3bc3                  cmp       eax,ebx
  00455ee6  7d02                  jge       short 00455EEAh
  00455ee8  f7d8                  neg       eax

LAB_00455eea:
  00455eea  3d00100000            cmp       eax,1000h
  00455eef  7e34                  jle       short 00455F25h

LAB_00455ef1:
  00455ef1  66395e12              cmp       [esi+12h],bx
  00455ef5  752e                  jne       short 00455F25h
  00455ef7  a1cc975e00            mov       eax,[5E97CCh]
  00455efc  53                    push      ebx
  00455efd  53                    push      ebx
  00455efe  6800100000            push      1000h
  00455f03  8b88a8000000          mov       ecx,[eax+0A8h]
  00455f09  6800080000            push      800h
  00455f0e  8b11                  mov       edx,[ecx]
  00455f10  660fb6422d            movzx     ax,byte [edx+2Dh]
  00455f15  50                    push      eax
  00455f16  56                    push      esi
  00455f17  e864210100            call      00468080h
  00455f1c  83c418                add       esp,18h
  00455f1f  66c746121400          mov       word [esi+12h],14h

LAB_00455f25:
  00455f25  66ff4e12              dec       word [esi+12h]
  00455f29  66395e12              cmp       [esi+12h],bx
  00455f2d  7d04                  jge       short 00455F33h
  00455f2f  66895e12              mov       [esi+12h],bx

LAB_00455f33:
  00455f33  817e200080daff        cmp       dword [esi+20h],0FFDA8000h
  00455f3a  0f8dab000000          jge       near 00455FEBh
  00455f40  33d2                  xor       edx,edx
  00455f42  bf00c01200            mov       edi,12C000h

LAB_00455f47:
  00455f47  0fbfca                movsx     ecx,dx
  00455f4a  8d0489                lea       eax,[ecx+ecx*4]
  00455f4d  c1e003                shl       eax,3
  00455f50  2bc1                  sub       eax,ecx
  00455f52  c1e002                shl       eax,2
  00455f55  8b88c0935e00          mov       ecx,[eax+5E93C0h]
  00455f5b  f7c100000080          test      ecx,80000000h
  00455f61  7410                  je        short 00455F73h
  00455f63  f7c100000050          test      ecx,50000000h
  00455f69  7508                  jne       short 00455F73h
  00455f6b  39b8e0935e00          cmp       [eax+5E93E0h],edi
  00455f71  7c78                  jl        short 00455FEBh

LAB_00455f73:
  00455f73  42                    inc       edx
  00455f74  6683fa03              cmp       dx,3
  00455f78  7ecd                  jle       short 00455F47h
  00455f7a  f605773c560001        test      byte [563C77h],1
  00455f81  7568                  jne       short 00455FEBh
  00455f83  8b3e                  mov       edi,[esi]
  00455f85  6633c0                xor       ax,ax
  00455f88  81e7ffffff7f          and       edi,7FFFFFFFh
  00455f8e  893e                  mov       [esi],edi
  00455f90  8a0dfa965e00          mov       cl,[5E96FAh]
  00455f96  80e10f                and       cl,0Fh
  00455f99  8ac1                  mov       al,cl
  00455f9b  663bc3                cmp       ax,bx
  00455f9e  7f02                  jg        short 00455FA2h
  00455fa0  33c0                  xor       eax,eax

LAB_00455fa2:
  00455fa2  663d0400              cmp       ax,4
  00455fa6  7c05                  jl        short 00455FADh
  00455fa8  b804000000            mov       eax,4

LAB_00455fad:
  00455fad  8b566c                mov       edx,[esi+6Ch]
  00455fb0  53                    push      ebx
  00455fb1  0fbff8                movsx     edi,ax
  00455fb4  a1cc975e00            mov       eax,[5E97CCh]
  00455fb9  68b0335600            push      5633B0h
  00455fbe  52                    push      edx
  00455fbf  8b4870                mov       ecx,[eax+70h]
  00455fc2  8b54b904              mov       edx,[ecx+edi*4+4]
  00455fc6  52                    push      edx
  00455fc7  e884e30e00            call      00544350h
  00455fcc  a1cc975e00            mov       eax,[5E97CCh]
  00455fd1  83c410                add       esp,10h
  00455fd4  57                    push      edi
  00455fd5  56                    push      esi
  00455fd6  50                    push      eax
  00455fd7  e8f4dd0b00            call      00513DD0h
  00455fdc  a0fa965e00            mov       al,[5E96FAh]
  00455fe1  83c40c                add       esp,0Ch
  00455fe4  fec0                  inc       al
  00455fe6  a2fa965e00            mov       [5E96FAh],al

LAB_00455feb:
  00455feb  8b4e20                mov       ecx,[esi+20h]
  00455fee  b80000ceff            mov       eax,0FFCE0000h
  00455ff3  3bc8                  cmp       ecx,eax
  00455ff5  7d0a                  jge       short 00456001h
  00455ff7  894620                mov       [esi+20h],eax
  00455ffa  8b06                  mov       eax,[esi]
  00455ffc  80cc60                or        ah,60h
  00455fff  8906                  mov       [esi],eax

LAB_00456001:
  00456001  895e50                mov       [esi+50h],ebx
  00456004  895e54                mov       [esi+54h],ebx
  00456007  895e58                mov       [esi+58h],ebx
  0045600a  5f                    pop       edi
  0045600b  5e                    pop       esi
  0045600c  5b                    pop       ebx
  0045600d  c3                    ret

26 cible(s) de saut interne : 0x00455b99, 0x00455bfa, 0x00455c04, 0x00455c20, 0x00455ca0, 0x00455cb5, 0x00455cca, 0x00455cda, 0x00455cea, 0x00455d1b, 0x00455d29, 0x00455dec, 0x00455e61, 0x00455e91, 0x00455ec3, 0x00455eda, 0x00455eea, 0x00455ef1, 0x00455f25, 0x00455f33, 0x00455f47, 0x00455f73, 0x00455fa2, 0x00455fad, 0x00455feb, 0x00456001
