
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00482ef0  section .text  offset fichier 0x822f0  167 octets

  00482ef0  83ec0c                sub       esp,0Ch
  00482ef3  56                    push      esi
  00482ef4  8b742414              mov       esi,[esp+14h]
  00482ef8  57                    push      edi
  00482ef9  668b4612              mov       ax,[esi+12h]
  00482efd  6683463280            add       word [esi+32h],0FF80h
  00482f02  668bd0                mov       dx,ax
  00482f05  668b4e32              mov       cx,[esi+32h]
  00482f09  66c1fa03              sar       dx,3
  00482f0d  2bc2                  sub       eax,edx
  00482f0f  8b7e6c                mov       edi,[esi+6Ch]
  00482f12  0fbf5634              movsx     edx,word [esi+34h]
  00482f16  66894612              mov       [esi+12h],ax
  00482f1a  0fbfc0                movsx     eax,ax
  00482f1d  50                    push      eax
  00482f1e  52                    push      edx
  00482f1f  0fbfc1                movsx     eax,cx
  00482f22  8d4c2410              lea       ecx,[esp+10h]
  00482f26  50                    push      eax
  00482f27  51                    push      ecx
  00482f28  e8a3fb0a00            call      00532AD0h
  00482f2d  8b571c                mov       edx,[edi+1Ch]
  00482f30  8b442418              mov       eax,[esp+18h]
  00482f34  8b4c241c              mov       ecx,[esp+1Ch]
  00482f38  03d0                  add       edx,eax
  00482f3a  89561c                mov       [esi+1Ch],edx
  00482f3d  8b4720                mov       eax,[edi+20h]
  00482f40  8b542420              mov       edx,[esp+20h]
  00482f44  03c1                  add       eax,ecx
  00482f46  894620                mov       [esi+20h],eax
  00482f49  8b4f24                mov       ecx,[edi+24h]
  00482f4c  8b4638                mov       eax,[esi+38h]
  00482f4f  83c410                add       esp,10h
  00482f52  03ca                  add       ecx,edx
  00482f54  3de0e0e000            cmp       eax,0E0E0E0h
  00482f59  894e24                mov       [esi+24h],ecx
  00482f5c  7308                  jae       short 00482F66h
  00482f5e  0520202000            add       eax,202020h
  00482f63  894638                mov       [esi+38h],eax

LAB_00482f66:
  00482f66  8a4e40                mov       cl,[esi+40h]
  00482f69  fec1                  inc       cl
  00482f6b  660fb6d1              movzx     dx,cl
  00482f6f  663b561a              cmp       dx,[esi+1Ah]
  00482f73  884e40                mov       [esi+40h],cl
  00482f76  7c04                  jl        short 00482F7Ch
  00482f78  c6464000              mov       byte [esi+40h],0

LAB_00482f7c:
  00482f7c  66837e1230            cmp       word [esi+12h],30h
  00482f81  7c08                  jl        short 00482F8Bh
  00482f83  f70700000040          test      dword [edi],40000000h
  00482f89  7406                  je        short 00482F91h

LAB_00482f8b:
  00482f8b  c70600000000          mov       dword [esi],0

LAB_00482f91:
  00482f91  5f                    pop       edi
  00482f92  5e                    pop       esi
  00482f93  83c40c                add       esp,0Ch
  00482f96  c3                    ret

4 cible(s) de saut interne : 0x00482f66, 0x00482f7c, 0x00482f8b, 0x00482f91
