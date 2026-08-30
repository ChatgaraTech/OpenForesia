; 0x00458e90 — le decor qui flotte et qui replonge (101 instructions, absente de Blaze.c).
; Attention : le premier 'ret' (0x00458f09) est un retour de milieu de corps.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00458e90  section .text  offset fichier 0x58290  304 octets

  00458e90  53                    push      ebx
  00458e91  56                    push      esi
  00458e92  8b74240c              mov       esi,[esp+0Ch]
  00458e96  57                    push      edi
  00458e97  8b06                  mov       eax,[esi]
  00458e99  a900000040            test      eax,40000000h
  00458e9e  746a                  je        short 00458F0Ah
  00458ea0  66837e1000            cmp       word [esi+10h],0
  00458ea5  bb01000000            mov       ebx,1
  00458eaa  7514                  jne       short 00458EC0h
  00458eac  8b4e20                mov       ecx,[esi+20h]
  00458eaf  66c74616ffff          mov       word [esi+16h],0FFFFh
  00458eb5  c1f90c                sar       ecx,0Ch
  00458eb8  66894e14              mov       [esi+14h],cx
  00458ebc  66895e10              mov       [esi+10h],bx

LAB_00458ec0:
  00458ec0  66395e10              cmp       [esi+10h],bx
  00458ec4  7531                  jne       short 00458EF7h
  00458ec6  668b4e16              mov       cx,[esi+16h]
  00458eca  8b7e20                mov       edi,[esi+20h]
  00458ecd  0fbfd1                movsx     edx,cx
  00458ed0  c1e20c                shl       edx,0Ch
  00458ed3  03fa                  add       edi,edx
  00458ed5  897e20                mov       [esi+20h],edi
  00458ed8  8bd7                  mov       edx,edi
  00458eda  0fbf7e14              movsx     edi,word [esi+14h]
  00458ede  8bdf                  mov       ebx,edi
  00458ee0  c1e30c                shl       ebx,0Ch
  00458ee3  3bd3                  cmp       edx,ebx
  00458ee5  7d0a                  jge       short 00458EF1h
  00458ee7  83c7ec                add       edi,0FFFFFFECh
  00458eea  c1e70c                shl       edi,0Ch
  00458eed  3bd7                  cmp       edx,edi
  00458eef  7f06                  jg        short 00458EF7h

LAB_00458ef1:
  00458ef1  f7d9                  neg       ecx
  00458ef3  66894e16              mov       [esi+16h],cx

LAB_00458ef7:
  00458ef7  a810                  test      al,10h
  00458ef9  0f85b8000000          jne       near 00458FB7h
  00458eff  25ffffffef            and       eax,0EFFFFFFFh
  00458f04  8906                  mov       [esi],eax
  00458f06  5f                    pop       edi
  00458f07  5e                    pop       esi
  00458f08  5b                    pop       ebx
  00458f09  c3                    ret

LAB_00458f0a:
  00458f0a  668b4e10              mov       cx,[esi+10h]
  00458f0e  bb01000000            mov       ebx,1
  00458f13  663bcb                cmp       cx,bx
  00458f16  bf02000000            mov       edi,2
  00458f1b  7405                  je        short 00458F22h
  00458f1d  6685c9                test      cx,cx
  00458f20  753b                  jne       short 00458F5Dh

LAB_00458f22:
  00458f22  0d00000010            or        eax,10000000h
  00458f27  66897e10              mov       [esi+10h],di
  00458f2b  66c746120a00          mov       word [esi+12h],0Ah
  00458f31  8906                  mov       [esi],eax
  00458f33  a1cc975e00            mov       eax,[5E97CCh]
  00458f38  6a00                  push      0
  00458f3a  6a00                  push      0
  00458f3c  6800100000            push      1000h
  00458f41  8b88a8000000          mov       ecx,[eax+0A8h]
  00458f47  6800040000            push      400h
  00458f4c  8b11                  mov       edx,[ecx]
  00458f4e  660fb64218            movzx     ax,byte [edx+18h]
  00458f53  50                    push      eax
  00458f54  56                    push      esi
  00458f55  e826f10000            call      00468080h
  00458f5a  83c418                add       esp,18h

LAB_00458f5d:
  00458f5d  66397e10              cmp       [esi+10h],di
  00458f61  7554                  jne       short 00458FB7h
  00458f63  668b4612              mov       ax,[esi+12h]
  00458f67  6685c0                test      ax,ax
  00458f6a  7e17                  jle       short 00458F83h
  00458f6c  48                    dec       eax
  00458f6d  0fbfc8                movsx     ecx,ax
  00458f70  66894612              mov       [esi+12h],ax
  00458f74  8b4620                mov       eax,[esi+20h]
  00458f77  c1e10c                shl       ecx,0Ch
  00458f7a  2bc1                  sub       eax,ecx
  00458f7c  894620                mov       [esi+20h],eax
  00458f7f  5f                    pop       edi
  00458f80  5e                    pop       esi
  00458f81  5b                    pop       ebx
  00458f82  c3                    ret

LAB_00458f83:
  00458f83  8b4620                mov       eax,[esi+20h]
  00458f86  0500500000            add       eax,5000h
  00458f8b  894620                mov       [esi+20h],eax
  00458f8e  8bc8                  mov       ecx,eax
  00458f90  0fbf4614              movsx     eax,word [esi+14h]
  00458f94  c1e00c                shl       eax,0Ch
  00458f97  3bc1                  cmp       eax,ecx
  00458f99  7f1c                  jg        short 00458FB7h
  00458f9b  894620                mov       [esi+20h],eax
  00458f9e  8b06                  mov       eax,[esi]
  00458fa0  0d00000040            or        eax,40000000h
  00458fa5  66895e10              mov       [esi+10h],bx
  00458fa9  66c74616ffff          mov       word [esi+16h],0FFFFh
  00458faf  8906                  mov       [esi],eax
  00458fb1  66c746126400          mov       word [esi+12h],64h

LAB_00458fb7:
  00458fb7  5f                    pop       edi
  00458fb8  5e                    pop       esi
  00458fb9  5b                    pop       ebx
  00458fba  c3                    ret
  00458fbb  90                    nop
  00458fbc  90                    nop
  00458fbd  90                    nop
  00458fbe  90                    nop
  00458fbf  90                    nop

8 cible(s) de saut interne : 0x00458ec0, 0x00458ef1, 0x00458ef7, 0x00458f0a, 0x00458f22, 0x00458f5d, 0x00458f83, 0x00458fb7
