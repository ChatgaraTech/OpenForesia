Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00408ef0  section .text  offset fichier 0x82f0  560 octets

  00408ef0  53                    push      ebx
  00408ef1  56                    push      esi
  00408ef2  8b74240c              mov       esi,[esp+0Ch]
  00408ef6  57                    push      edi
  00408ef7  bb02000000            mov       ebx,2
  00408efc  66837e1000            cmp       word [esi+10h],0
  00408f01  7545                  jne       short 00408F48h
  00408f03  a0c4de5e00            mov       al,[5EDEC4h]
  00408f08  8a4e14                mov       cl,[esi+14h]
  00408f0b  02c1                  add       al,cl
  00408f0d  66837e1400            cmp       word [esi+14h],0
  00408f12  884604                mov       [esi+4],al
  00408f15  885e0a                mov       [esi+0Ah],bl
  00408f18  c6460b00              mov       byte [esi+0Bh],0
  00408f1c  66c746100100          mov       word [esi+10h],1
  00408f22  66c746120a00          mov       word [esi+12h],0Ah
  00408f28  66c746160000          mov       word [esi+16h],0
  00408f2e  7518                  jne       short 00408F48h
  00408f30  6aff                  push      0FFFFFFFFh
  00408f32  6a00                  push      0
  00408f34  68ef000000            push      0EFh
  00408f39  8d4e1c                lea       ecx,[esi+1Ch]
  00408f3c  6a00                  push      0
  00408f3e  51                    push      ecx
  00408f3f  56                    push      esi
  00408f40  e8fb1c0700            call      0047AC40h
  00408f45  83c418                add       esp,18h

LAB_00408f48:
  00408f48  66837e1001            cmp       word [esi+10h],1
  00408f4d  0f8546010000          jne       near 00409099h
  00408f53  0fbf5612              movsx     edx,word [esi+12h]
  00408f57  8b7e20                mov       edi,[esi+20h]
  00408f5a  8b4664                mov       eax,[esi+64h]
  00408f5d  c1e20c                shl       edx,0Ch
  00408f60  2bfa                  sub       edi,edx
  00408f62  8bcf                  mov       ecx,edi
  00408f64  897e20                mov       [esi+20h],edi
  00408f67  3bc8                  cmp       ecx,eax
  00408f69  0f8f0b010000          jg        near 0040907Ah
  00408f6f  894620                mov       [esi+20h],eax
  00408f72  66895e10              mov       [esi+10h],bx
  00408f76  e8c5e11300            call      00547140h
  00408f7b  99                    cdq
  00408f7c  33c2                  xor       eax,edx
  00408f7e  2bc2                  sub       eax,edx
  00408f80  83e00f                and       eax,0Fh
  00408f83  33c2                  xor       eax,edx
  00408f85  2bc2                  sub       eax,edx
  00408f87  83e808                sub       eax,8
  00408f8a  894650                mov       [esi+50h],eax
  00408f8d  e8aee11300            call      00547140h
  00408f92  99                    cdq
  00408f93  33c2                  xor       eax,edx
  00408f95  2bc2                  sub       eax,edx
  00408f97  83e00f                and       eax,0Fh
  00408f9a  33c2                  xor       eax,edx
  00408f9c  2bc2                  sub       eax,edx
  00408f9e  83e808                sub       eax,8
  00408fa1  894658                mov       [esi+58h],eax
  00408fa4  e897e11300            call      00547140h
  00408fa9  99                    cdq
  00408faa  b903000000            mov       ecx,3
  00408faf  6aff                  push      0FFFFFFFFh
  00408fb1  f7f9                  idiv      ecx
  00408fb3  6a00                  push      0
  00408fb5  8d7e1c                lea       edi,[esi+1Ch]
  00408fb8  68f4000000            push      0F4h
  00408fbd  6a00                  push      0
  00408fbf  57                    push      edi
  00408fc0  56                    push      esi
  00408fc1  83c214                add       edx,14h
  00408fc4  895654                mov       [esi+54h],edx
  00408fc7  e8741c0700            call      0047AC40h
  00408fcc  83c418                add       esp,18h
  00408fcf  6aff                  push      0FFFFFFFFh
  00408fd1  6a00                  push      0
  00408fd3  68f4000000            push      0F4h
  00408fd8  6a00                  push      0
  00408fda  57                    push      edi
  00408fdb  56                    push      esi
  00408fdc  e85f1c0700            call      0047AC40h
  00408fe1  83c418                add       esp,18h
  00408fe4  6aff                  push      0FFFFFFFFh
  00408fe6  6a00                  push      0
  00408fe8  68f4000000            push      0F4h
  00408fed  6a00                  push      0
  00408fef  57                    push      edi
  00408ff0  56                    push      esi
  00408ff1  e84a1c0700            call      0047AC40h
  00408ff6  8b15cc975e00          mov       edx,[5E97CCh]
  00408ffc  83c418                add       esp,18h
  00408fff  8b82a8000000          mov       eax,[edx+0A8h]
  00409005  6a00                  push      0
  00409007  6a00                  push      0
  00409009  6800100000            push      1000h
  0040900e  8b08                  mov       ecx,[eax]
  00409010  6800080000            push      800h
  00409015  660fb65120            movzx     dx,byte [ecx+20h]
  0040901a  52                    push      edx
  0040901b  56                    push      esi
  0040901c  e85ff00500            call      00468080h
  00409021  83c418                add       esp,18h
  00409024  e817e11300            call      00547140h
  00409029  99                    cdq
  0040902a  b928000000            mov       ecx,28h
  0040902f  f7f9                  idiv      ecx
  00409031  83ea14                sub       edx,14h
  00409034  895644                mov       [esi+44h],edx
  00409037  e804e11300            call      00547140h
  0040903c  99                    cdq
  0040903d  b928000000            mov       ecx,28h
  00409042  f7f9                  idiv      ecx
  00409044  83ea14                sub       edx,14h
  00409047  895648                mov       [esi+48h],edx
  0040904a  e8f1e01300            call      00547140h
  0040904f  99                    cdq
  00409050  b928000000            mov       ecx,28h
  00409055  f7f9                  idiv      ecx
  00409057  83ea14                sub       edx,14h
  0040905a  89564c                mov       [esi+4Ch],edx
  0040905d  8b5624                mov       edx,[esi+24h]
  00409060  8b4620                mov       eax,[esi+20h]
  00409063  8b0f                  mov       ecx,[edi]
  00409065  6a00                  push      0
  00409067  6a00                  push      0
  00409069  6a00                  push      0
  0040906b  6a03                  push      3
  0040906d  6a50                  push      50h
  0040906f  52                    push      edx
  00409070  50                    push      eax
  00409071  51                    push      ecx
  00409072  e8d9fe0000            call      00418F50h
  00409077  83c420                add       esp,20h

LAB_0040907a:
  0040907a  8b4620                mov       eax,[esi+20h]
  0040907d  66ff4612              inc       word [esi+12h]
  00409081  3d008044ff            cmp       eax,0FF448000h
  00409086  7d11                  jge       short 00409099h
  00409088  8b06                  mov       eax,[esi]
  0040908a  66895e10              mov       [esi+10h],bx
  0040908e  25ffffff7f            and       eax,7FFFFFFFh
  00409093  8906                  mov       [esi],eax
  00409095  5f                    pop       edi
  00409096  5e                    pop       esi
  00409097  5b                    pop       ebx
  00409098  c3                    ret

LAB_00409099:
  00409099  66395e10              cmp       [esi+10h],bx
  0040909d  7578                  jne       short 00409117h
  0040909f  8b4e20                mov       ecx,[esi+20h]
  004090a2  81f9008044ff          cmp       ecx,0FF448000h
  004090a8  7d06                  jge       short 004090B0h
  004090aa  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004090b0:
  004090b0  8b5650                mov       edx,[esi+50h]
  004090b3  8b5e1c                mov       ebx,[esi+1Ch]
  004090b6  8b4658                mov       eax,[esi+58h]
  004090b9  c1e20c                shl       edx,0Ch
  004090bc  03da                  add       ebx,edx
  004090be  8b5624                mov       edx,[esi+24h]
  004090c1  c1e00c                shl       eax,0Ch
  004090c4  03d0                  add       edx,eax
  004090c6  8b4654                mov       eax,[esi+54h]
  004090c9  895624                mov       [esi+24h],edx
  004090cc  8bd0                  mov       edx,eax
  004090ce  c1e20c                shl       edx,0Ch
  004090d1  03d1                  add       edx,ecx
  004090d3  668b4e44              mov       cx,[esi+44h]
  004090d7  66014e30              add       [esi+30h],cx
  004090db  668b4e4c              mov       cx,[esi+4Ch]
  004090df  66014e34              add       [esi+34h],cx
  004090e3  8a4e16                mov       cl,[esi+16h]
  004090e6  895620                mov       [esi+20h],edx
  004090e9  668b5648              mov       dx,[esi+48h]
  004090ed  66015632              add       [esi+32h],dx
  004090f1  48                    dec       eax
  004090f2  f6c101                test      cl,1
  004090f5  8b0e                  mov       ecx,[esi]
  004090f7  895e1c                mov       [esi+1Ch],ebx
  004090fa  894654                mov       [esi+54h],eax
  004090fd  7405                  je        short 00409104h
  004090ff  80cd40                or        ch,40h
  00409102  eb03                  jmp       short 00409107h

LAB_00409104:
  00409104  80e5bf                and       ch,0BFh

LAB_00409107:
  00409107  83f8f1                cmp       eax,0FFFFFFF1h
  0040910a  890e                  mov       [esi],ecx
  0040910c  7d09                  jge       short 00409117h
  0040910e  8bc1                  mov       eax,ecx
  00409110  25ffffff7f            and       eax,7FFFFFFFh
  00409115  8906                  mov       [esi],eax

LAB_00409117:
  00409117  66ff4616              inc       word [esi+16h]
  0040911b  5f                    pop       edi
  0040911c  5e                    pop       esi
  0040911d  5b                    pop       ebx
  0040911e  c3                    ret
  0040911f  90                    nop

7 cible(s) de saut interne : 0x00408f48, 0x0040907a, 0x00409099, 0x004090b0, 0x00409104, 0x00409107, 0x00409117
