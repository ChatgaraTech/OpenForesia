Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00454ed0  section .text  offset fichier 0x542d0  763 octets

  00454ed0  a070155700            mov       al,[571570h]
  00454ed5  53                    push      ebx
  00454ed6  55                    push      ebp
  00454ed7  56                    push      esi
  00454ed8  8b742410              mov       esi,[esp+10h]
  00454edc  57                    push      edi
  00454edd  a803                  test      al,3
  00454edf  7506                  jne       short 00454EE7h
  00454ee1  66c746100000          mov       word [esi+10h],0

LAB_00454ee7:
  00454ee7  f6057015570001        test      byte [571570h],1
  00454eee  0f846e010000          je        near 00455062h
  00454ef4  66837e1000            cmp       word [esi+10h],0
  00454ef9  bf28000000            mov       edi,28h
  00454efe  7525                  jne       short 00454F25h
  00454f00  668b4632              mov       ax,[esi+32h]
  00454f04  66c746100100          mov       word [esi+10h],1
  00454f0a  66897e12              mov       [esi+12h],di
  00454f0e  66894616              mov       [esi+16h],ax
  00454f12  8b0dcc975e00          mov       ecx,[5E97CCh]
  00454f18  6a05                  push      5
  00454f1a  6a00                  push      0
  00454f1c  51                    push      ecx
  00454f1d  e8aeee0b00            call      00513DD0h
  00454f22  83c40c                add       esp,0Ch

LAB_00454f25:
  00454f25  bb02000000            mov       ebx,2
  00454f2a  66395e10              cmp       [esi+10h],bx
  00454f2e  750c                  jne       short 00454F3Ch
  00454f30  a170155700            mov       eax,[571570h]
  00454f35  24fe                  and       al,0FEh
  00454f37  a370155700            mov       [571570h],eax

LAB_00454f3c:
  00454f3c  66837e1001            cmp       word [esi+10h],1
  00454f41  0f8520010000          jne       near 00455067h
  00454f47  66017e12              add       [esi+12h],di
  00454f4b  66817e12a000          cmp       word [esi+12h],0A0h
  00454f51  0f8ea0000000          jle       near 00454FF7h
  00454f57  0fbf4614              movsx     eax,word [esi+14h]
  00454f5b  0fbf4e32              movsx     ecx,word [esi+32h]
  00454f5f  8b5624                mov       edx,[esi+24h]
  00454f62  8d0480                lea       eax,[eax+eax*4]
  00454f65  8b5e1c                mov       ebx,[esi+1Ch]
  00454f68  8b6e20                mov       ebp,[esi+20h]
  00454f6b  8d0480                lea       eax,[eax+eax*4]
  00454f6e  89542414              mov       [esp+14h],edx
  00454f72  c1e005                shl       eax,5
  00454f75  8d940870feffff        lea       edx,[eax+ecx-190h]
  00454f7c  52                    push      edx
  00454f7d  e89a0a0f00            call      00545A1Ch
  00454f82  8d0c40                lea       ecx,[eax+eax*2]
  00454f85  c1e103                shl       ecx,3
  00454f88  2bc8                  sub       ecx,eax
  00454f8a  0fbf5632              movsx     edx,word [esi+32h]
  00454f8e  8d0489                lea       eax,[ecx+ecx*4]
  00454f91  8b4e1c                mov       ecx,[esi+1Ch]
  00454f94  d1e0                  shl       eax,1
  00454f96  03c8                  add       ecx,eax
  00454f98  0fbf4614              movsx     eax,word [esi+14h]
  00454f9c  894e1c                mov       [esi+1Ch],ecx
  00454f9f  8d0480                lea       eax,[eax+eax*4]
  00454fa2  8d0c80                lea       ecx,[eax+eax*4]
  00454fa5  c1e105                shl       ecx,5
  00454fa8  8d841170feffff        lea       eax,[ecx+edx-190h]
  00454faf  50                    push      eax
  00454fb0  e8610a0f00            call      00545A16h
  00454fb5  8d0c40                lea       ecx,[eax+eax*2]
  00454fb8  bf0b000000            mov       edi,0Bh
  00454fbd  c1e103                shl       ecx,3
  00454fc0  2bc8                  sub       ecx,eax
  00454fc2  8b4624                mov       eax,[esi+24h]
  00454fc5  8d0c89                lea       ecx,[ecx+ecx*4]
  00454fc8  d1e1                  shl       ecx,1
  00454fca  03c1                  add       eax,ecx
  00454fcc  894624                mov       [esi+24h],eax

LAB_00454fcf:
  00454fcf  6a00                  push      0
  00454fd1  6a00                  push      0
  00454fd3  6aff                  push      0FFFFFFFFh
  00454fd5  6a00                  push      0
  00454fd7  6a31                  push      31h
  00454fd9  56                    push      esi
  00454fda  e831310100            call      00468110h
  00454fdf  83c418                add       esp,18h
  00454fe2  4f                    dec       edi
  00454fe3  75ea                  jne       short 00454FCFh
  00454fe5  8b542414              mov       edx,[esp+14h]
  00454fe9  895e1c                mov       [esi+1Ch],ebx
  00454fec  896e20                mov       [esi+20h],ebp
  00454fef  895624                mov       [esi+24h],edx
  00454ff2  bb02000000            mov       ebx,2

LAB_00454ff7:
  00454ff7  668b5614              mov       dx,[esi+14h]
  00454ffb  6685d2                test      dx,dx
  00454ffe  752d                  jne       short 0045502Dh
  00455000  668b4612              mov       ax,[esi+12h]
  00455004  66014632              add       [esi+32h],ax
  00455008  668b4616              mov       ax,[esi+16h]
  0045500c  668b4e32              mov       cx,[esi+32h]
  00455010  0fbff8                movsx     edi,ax
  00455013  0fbfc9                movsx     ecx,cx
  00455016  81c700020000          add       edi,200h
  0045501c  3bcf                  cmp       ecx,edi
  0045501e  7c0d                  jl        short 0045502Dh
  00455020  0500020000            add       eax,200h
  00455025  66895e10              mov       [esi+10h],bx
  00455029  66894632              mov       [esi+32h],ax

LAB_0045502d:
  0045502d  6683fa01              cmp       dx,1
  00455031  7534                  jne       short 00455067h
  00455033  668b5612              mov       dx,[esi+12h]
  00455037  668b4616              mov       ax,[esi+16h]
  0045503b  66295632              sub       [esi+32h],dx
  0045503f  668b4e32              mov       cx,[esi+32h]
  00455043  0fbfd0                movsx     edx,ax
  00455046  0fbfc9                movsx     ecx,cx
  00455049  81ea00020000          sub       edx,200h
  0045504f  3bca                  cmp       ecx,edx
  00455051  7f14                  jg        short 00455067h
  00455053  0500feffff            add       eax,0FFFFFE00h
  00455058  66895e10              mov       [esi+10h],bx
  0045505c  66894632              mov       [esi+32h],ax
  00455060  eb05                  jmp       short 00455067h

LAB_00455062:
  00455062  bb02000000            mov       ebx,2

LAB_00455067:
  00455067  f6057015570002        test      byte [571570h],2
  0045506e  0f8452010000          je        near 004551C6h
  00455074  66837e1000            cmp       word [esi+10h],0
  00455079  7514                  jne       short 0045508Fh
  0045507b  668b5632              mov       dx,[esi+32h]
  0045507f  66c746100100          mov       word [esi+10h],1
  00455085  66c74612f000          mov       word [esi+12h],0F0h
  0045508b  66895616              mov       [esi+16h],dx

LAB_0045508f:
  0045508f  66395e10              cmp       [esi+10h],bx
  00455093  750c                  jne       short 004550A1h
  00455095  a170155700            mov       eax,[571570h]
  0045509a  24fd                  and       al,0FDh
  0045509c  a370155700            mov       [571570h],eax

LAB_004550a1:
  004550a1  66837e1001            cmp       word [esi+10h],1
  004550a6  0f851a010000          jne       near 004551C6h
  004550ac  66834612d8            add       word [esi+12h],0FFD8h
  004550b1  66817e12a000          cmp       word [esi+12h],0A0h
  004550b7  0f85a0000000          jne       near 0045515Dh
  004550bd  8b4624                mov       eax,[esi+24h]
  004550c0  8b6e1c                mov       ebp,[esi+1Ch]
  004550c3  89442414              mov       [esp+14h],eax
  004550c7  8b5e20                mov       ebx,[esi+20h]
  004550ca  0fbf4614              movsx     eax,word [esi+14h]
  004550ce  0fbf5632              movsx     edx,word [esi+32h]
  004550d2  8d0480                lea       eax,[eax+eax*4]
  004550d5  8d0c80                lea       ecx,[eax+eax*4]
  004550d8  c1e105                shl       ecx,5
  004550db  8d841170feffff        lea       eax,[ecx+edx-190h]
  004550e2  50                    push      eax
  004550e3  e834090f00            call      00545A1Ch
  004550e8  8d0c40                lea       ecx,[eax+eax*2]
  004550eb  8b561c                mov       edx,[esi+1Ch]
  004550ee  c1e103                shl       ecx,3
  004550f1  2bc8                  sub       ecx,eax
  004550f3  0fbf4614              movsx     eax,word [esi+14h]
  004550f7  8d0c89                lea       ecx,[ecx+ecx*4]
  004550fa  d1e1                  shl       ecx,1
  004550fc  03d1                  add       edx,ecx
  004550fe  8d0480                lea       eax,[eax+eax*4]
  00455101  89561c                mov       [esi+1Ch],edx
  00455104  8d1480                lea       edx,[eax+eax*4]
  00455107  0fbf4632              movsx     eax,word [esi+32h]
  0045510b  c1e205                shl       edx,5
  0045510e  8d8c0270feffff        lea       ecx,[edx+eax-190h]
  00455115  51                    push      ecx
  00455116  e8fb080f00            call      00545A16h
  0045511b  8d0c40                lea       ecx,[eax+eax*2]
  0045511e  bf15000000            mov       edi,15h
  00455123  c1e103                shl       ecx,3
  00455126  2bc8                  sub       ecx,eax
  00455128  8b4624                mov       eax,[esi+24h]
  0045512b  8d1489                lea       edx,[ecx+ecx*4]
  0045512e  d1e2                  shl       edx,1
  00455130  03c2                  add       eax,edx
  00455132  894624                mov       [esi+24h],eax

LAB_00455135:
  00455135  6a00                  push      0
  00455137  6a00                  push      0
  00455139  6aff                  push      0FFFFFFFFh
  0045513b  6a00                  push      0
  0045513d  6a31                  push      31h
  0045513f  56                    push      esi
  00455140  e8cb2f0100            call      00468110h
  00455145  83c418                add       esp,18h
  00455148  4f                    dec       edi
  00455149  75ea                  jne       short 00455135h
  0045514b  8b442414              mov       eax,[esp+14h]
  0045514f  895e20                mov       [esi+20h],ebx
  00455152  896e1c                mov       [esi+1Ch],ebp
  00455155  894624                mov       [esi+24h],eax
  00455158  bb02000000            mov       ebx,2

LAB_0045515d:
  0045515d  668b5614              mov       dx,[esi+14h]
  00455161  6685d2                test      dx,dx
  00455164  752d                  jne       short 00455193h
  00455166  668b4e12              mov       cx,[esi+12h]
  0045516a  668b4616              mov       ax,[esi+16h]
  0045516e  66294e32              sub       [esi+32h],cx
  00455172  668b4e32              mov       cx,[esi+32h]
  00455176  0fbff8                movsx     edi,ax
  00455179  0fbfc9                movsx     ecx,cx
  0045517c  81ef00020000          sub       edi,200h
  00455182  3bcf                  cmp       ecx,edi
  00455184  7f0d                  jg        short 00455193h
  00455186  0500feffff            add       eax,0FFFFFE00h
  0045518b  66895e10              mov       [esi+10h],bx
  0045518f  66894632              mov       [esi+32h],ax

LAB_00455193:
  00455193  6683fa01              cmp       dx,1
  00455197  752d                  jne       short 004551C6h
  00455199  668b5612              mov       dx,[esi+12h]
  0045519d  668b4616              mov       ax,[esi+16h]
  004551a1  66015632              add       [esi+32h],dx
  004551a5  668b4e32              mov       cx,[esi+32h]
  004551a9  0fbfd0                movsx     edx,ax
  004551ac  0fbfc9                movsx     ecx,cx
  004551af  81c200020000          add       edx,200h
  004551b5  3bca                  cmp       ecx,edx
  004551b7  7c0d                  jl        short 004551C6h
  004551b9  0500020000            add       eax,200h
  004551be  66895e10              mov       [esi+10h],bx
  004551c2  66894632              mov       [esi+32h],ax

LAB_004551c6:
  004551c6  5f                    pop       edi
  004551c7  5e                    pop       esi
  004551c8  5d                    pop       ebp
  004551c9  5b                    pop       ebx
  004551ca  c3                    ret

14 cible(s) de saut interne : 0x00454ee7, 0x00454f25, 0x00454f3c, 0x00454fcf, 0x00454ff7, 0x0045502d, 0x00455062, 0x00455067, 0x0045508f, 0x004550a1, 0x00455135, 0x0045515d, 0x00455193, 0x004551c6
