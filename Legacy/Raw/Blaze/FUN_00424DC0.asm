
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00424dc0  section .text  offset fichier 0x241c0  752 octets

  00424dc0  53                    push      ebx
  00424dc1  55                    push      ebp
  00424dc2  56                    push      esi
  00424dc3  57                    push      edi
  00424dc4  33ff                  xor       edi,edi
  00424dc6  e88561ffff            call      0041AF50h
  00424dcb  e87098feff            call      0040E640h
  00424dd0  8b742414              mov       esi,[esp+14h]
  00424dd4  8b6c2414              mov       ebp,[esp+14h]
  00424dd8  b8dbca5e00            mov       eax,5ECADBh
  00424ddd  ba20000000            mov       edx,20h
  00424de2  bb01000000            mov       ebx,1

LAB_00424de7:
  00424de7  3858d1                cmp       [eax-2Fh],bl
  00424dea  7545                  jne       short 00424E31h
  00424dec  f640c880              test      byte [eax-38h],80h
  00424df0  8d48c5                lea       ecx,[eax-3Bh]
  00424df3  743c                  je        short 00424E31h
  00424df5  8b58e1                mov       ebx,[eax-1Fh]
  00424df8  8bf9                  mov       edi,ecx
  00424dfa  895e1c                mov       [esi+1Ch],ebx
  00424dfd  8b58e5                mov       ebx,[eax-1Bh]
  00424e00  895e20                mov       [esi+20h],ebx
  00424e03  8b58e9                mov       ebx,[eax-17h]
  00424e06  895e24                mov       [esi+24h],ebx
  00424e09  33db                  xor       ebx,ebx
  00424e0b  8a58c9                mov       bl,[eax-37h]
  00424e0e  66c740dd0200          mov       word [eax-23h],2
  00424e14  8b1c9d40965e00        mov       ebx,[ebx*4+5E9640h]
  00424e1b  8b6b08                mov       ebp,[ebx+8]
  00424e1e  8a18                  mov       bl,[eax]
  00424e20  80cb02                or        bl,2
  00424e23  8818                  mov       [eax],bl
  00424e25  8b19                  mov       ebx,[ecx]
  00424e27  80cf10                or        bh,10h
  00424e2a  8919                  mov       [ecx],ebx
  00424e2c  bb01000000            mov       ebx,1

LAB_00424e31:
  00424e31  059c000000            add       eax,9Ch
  00424e36  4a                    dec       edx
  00424e37  75ae                  jne       short 00424DE7h
  00424e39  33c0                  xor       eax,eax
  00424e3b  3bf8                  cmp       edi,eax
  00424e3d  7505                  jne       short 00424E44h
  00424e3f  bf58e55d00            mov       edi,5DE558h

LAB_00424e44:
  00424e44  66394610              cmp       [esi+10h],ax
  00424e48  754a                  jne       short 00424E94h
  00424e4a  66895e10              mov       [esi+10h],bx
  00424e4e  66894612              mov       [esi+12h],ax
  00424e52  66894614              mov       [esi+14h],ax
  00424e56  66894616              mov       [esi+16h],ax
  00424e5a  0fbf0db8925e00        movsx     ecx,word [5E92B8h]
  00424e61  894e50                mov       [esi+50h],ecx
  00424e64  0fbf15ba925e00        movsx     edx,word [5E92BAh]
  00424e6b  895654                mov       [esi+54h],edx
  00424e6e  0fbf0dbc925e00        movsx     ecx,word [5E92BCh]
  00424e75  894e58                mov       [esi+58h],ecx
  00424e78  8a5709                mov       dl,[edi+9]
  00424e7b  6689471a              mov       [edi+1Ah],ax
  00424e7f  c6470a00              mov       byte [edi+0Ah],0
  00424e83  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  00424e8a  66895f36              mov       [edi+36h],bx
  00424e8e  8815d8145700          mov       [5714D8h],dl

LAB_00424e94:
  00424e94  66395e10              cmp       [esi+10h],bx
  00424e98  7555                  jne       short 00424EEFh
  00424e9a  6aff                  push      0FFFFFFFFh
  00424e9c  50                    push      eax
  00424e9d  68fa000000            push      0FAh
  00424ea2  50                    push      eax
  00424ea3  8d461c                lea       eax,[esi+1Ch]
  00424ea6  50                    push      eax
  00424ea7  56                    push      esi
  00424ea8  e8935d0500            call      0047AC40h
  00424ead  8a4616                mov       al,[esi+16h]
  00424eb0  83c418                add       esp,18h
  00424eb3  a802                  test      al,2
  00424eb5  7520                  jne       short 00424ED7h
  00424eb7  66ff471a              inc       word [edi+1Ah]
  00424ebb  668b4f1a              mov       cx,[edi+1Ah]
  00424ebf  668b4516              mov       ax,[ebp+16h]
  00424ec3  0fbfc9                movsx     ecx,cx
  00424ec6  8bd0                  mov       edx,eax
  00424ec8  81e2ffff0000          and       edx,0FFFFh
  00424ece  3bca                  cmp       ecx,edx
  00424ed0  7c05                  jl        short 00424ED7h
  00424ed2  48                    dec       eax
  00424ed3  6689471a              mov       [edi+1Ah],ax

LAB_00424ed7:
  00424ed7  8a5738                mov       dl,[edi+38h]
  00424eda  8a4f39                mov       cl,[edi+39h]
  00424edd  8a473a                mov       al,[edi+3Ah]
  00424ee0  feca                  dec       dl
  00424ee2  fec9                  dec       cl
  00424ee4  fec8                  dec       al
  00424ee6  885738                mov       [edi+38h],dl
  00424ee9  884f39                mov       [edi+39h],cl
  00424eec  88473a                mov       [edi+3Ah],al

LAB_00424eef:
  00424eef  e84c221200            call      00547140h
  00424ef4  99                    cdq
  00424ef5  33c2                  xor       eax,edx
  00424ef7  2bc2                  sub       eax,edx
  00424ef9  25ff010000            and       eax,1FFh
  00424efe  33c2                  xor       eax,edx
  00424f00  8be8                  mov       ebp,eax
  00424f02  2bea                  sub       ebp,edx
  00424f04  81c500020000          add       ebp,200h
  00424f0a  66837e1600            cmp       word [esi+16h],0
  00424f0f  752a                  jne       short 00424F3Bh
  00424f11  8b0dcc975e00          mov       ecx,[5E97CCh]
  00424f17  6800150000            push      1500h
  00424f1c  0fbfc5                movsx     eax,bp
  00424f1f  8b91a8000000          mov       edx,[ecx+0A8h]
  00424f25  6800150000            push      1500h
  00424f2a  50                    push      eax
  00424f2b  33c9                  xor       ecx,ecx
  00424f2d  8b02                  mov       eax,[edx]
  00424f2f  8a482d                mov       cl,[eax+2Dh]
  00424f32  51                    push      ecx
  00424f33  e8687f1100            call      0053CEA0h
  00424f38  83c410                add       esp,10h

LAB_00424f3b:
  00424f3b  66837e160f            cmp       word [esi+16h],0Fh
  00424f40  7529                  jne       short 00424F6Bh
  00424f42  a1cc975e00            mov       eax,[5E97CCh]
  00424f47  6800150000            push      1500h
  00424f4c  0fbfd5                movsx     edx,bp
  00424f4f  8b88a8000000          mov       ecx,[eax+0A8h]
  00424f55  6800150000            push      1500h
  00424f5a  52                    push      edx
  00424f5b  33c0                  xor       eax,eax
  00424f5d  8b11                  mov       edx,[ecx]
  00424f5f  8a422d                mov       al,[edx+2Dh]
  00424f62  50                    push      eax
  00424f63  e8387f1100            call      0053CEA0h
  00424f68  83c410                add       esp,10h

LAB_00424f6b:
  00424f6b  66837e161e            cmp       word [esi+16h],1Eh
  00424f70  752a                  jne       short 00424F9Ch
  00424f72  8b15cc975e00          mov       edx,[5E97CCh]
  00424f78  6800150000            push      1500h
  00424f7d  0fbfcd                movsx     ecx,bp
  00424f80  8b82a8000000          mov       eax,[edx+0A8h]
  00424f86  6800150000            push      1500h
  00424f8b  51                    push      ecx
  00424f8c  33d2                  xor       edx,edx
  00424f8e  8b08                  mov       ecx,[eax]
  00424f90  8a512d                mov       dl,[ecx+2Dh]
  00424f93  52                    push      edx
  00424f94  e8077f1100            call      0053CEA0h
  00424f99  83c410                add       esp,10h

LAB_00424f9c:
  00424f9c  66837e162d            cmp       word [esi+16h],2Dh
  00424fa1  752a                  jne       short 00424FCDh
  00424fa3  8b0dcc975e00          mov       ecx,[5E97CCh]
  00424fa9  6800150000            push      1500h
  00424fae  0fbfc5                movsx     eax,bp
  00424fb1  8b91a8000000          mov       edx,[ecx+0A8h]
  00424fb7  6800150000            push      1500h
  00424fbc  50                    push      eax
  00424fbd  33c9                  xor       ecx,ecx
  00424fbf  8b02                  mov       eax,[edx]
  00424fc1  8a482d                mov       cl,[eax+2Dh]
  00424fc4  51                    push      ecx
  00424fc5  e8d67e1100            call      0053CEA0h
  00424fca  83c410                add       esp,10h

LAB_00424fcd:
  00424fcd  66837e163c            cmp       word [esi+16h],3Ch
  00424fd2  7538                  jne       short 0042500Ch
  00424fd4  6800120000            push      1200h
  00424fd9  6800120000            push      1200h
  00424fde  68e6000000            push      0E6h
  00424fe3  6a05                  push      5
  00424fe5  e8b67e1100            call      0053CEA0h
  00424fea  83c410                add       esp,10h
  00424fed  6aff                  push      0FFFFFFFFh
  00424fef  6a00                  push      0
  00424ff1  68c7000000            push      0C7h
  00424ff6  56                    push      esi
  00424ff7  e8e45a0500            call      0047AAE0h
  00424ffc  66c746100200          mov       word [esi+10h],2
  00425002  8b07                  mov       eax,[edi]
  00425004  83c410                add       esp,10h
  00425007  80cc40                or        ah,40h
  0042500a  8907                  mov       [edi],eax

LAB_0042500c:
  0042500c  66837e163d            cmp       word [esi+16h],3Dh
  00425011  7519                  jne       short 0042502Ch
  00425013  6800120000            push      1200h
  00425018  6800120000            push      1200h
  0042501d  6800020000            push      200h
  00425022  6a05                  push      5
  00425024  e8777e1100            call      0053CEA0h
  00425029  83c410                add       esp,10h

LAB_0042502c:
  0042502c  66ff4616              inc       word [esi+16h]
  00425030  66837e1664            cmp       word [esi+16h],64h
  00425035  7c68                  jl        short 0042509Fh
  00425037  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0042503d  e86ee70d00            call      005037B0h
  00425042  a1d8145700            mov       eax,[5714D8h]
  00425047  8b0dcc975e00          mov       ecx,[5E97CCh]
  0042504d  25ff000000            and       eax,0FFh
  00425052  6a00                  push      0
  00425054  8d1480                lea       edx,[eax+eax*4]
  00425057  c1e203                shl       edx,3
  0042505a  2bd0                  sub       edx,eax
  0042505c  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00425063  50                    push      eax
  00425064  51                    push      ecx
  00425065  e866ed0e00            call      00513DD0h
  0042506a  8b07                  mov       eax,[edi]
  0042506c  83c40c                add       esp,0Ch
  0042506f  0d00400002            or        eax,2004000h
  00425074  8907                  mov       [edi],eax
  00425076  668b5650              mov       dx,[esi+50h]
  0042507a  66891560915e00        mov       [5E9160h],dx
  00425081  668b4654              mov       ax,[esi+54h]
  00425085  66a362915e00          mov       [5E9162h],ax
  0042508b  668b4e58              mov       cx,[esi+58h]
  0042508f  66890d64915e00        mov       [5E9164h],cx
  00425096  66c70566915e000200    mov       word [5E9166h],2

LAB_0042509f:
  0042509f  5f                    pop       edi
  004250a0  5e                    pop       esi
  004250a1  5d                    pop       ebp
  004250a2  5b                    pop       ebx
  004250a3  c3                    ret

13 cible(s) de saut interne : 0x00424de7, 0x00424e31, 0x00424e44, 0x00424e94, 0x00424ed7, 0x00424eef, 0x00424f3b, 0x00424f6b, 0x00424f9c, 0x00424fcd, 0x0042500c, 0x0042502c, 0x0042509f
