Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00457240  section .text  offset fichier 0x56640  300 octets

  00457240  53                    push      ebx
  00457241  56                    push      esi
  00457242  8b74240c              mov       esi,[esp+0Ch]
  00457246  57                    push      edi
  00457247  8b06                  mov       eax,[esi]
  00457249  a900000040            test      eax,40000000h
  0045724e  746a                  je        short 004572BAh
  00457250  66837e1000            cmp       word [esi+10h],0
  00457255  bb01000000            mov       ebx,1
  0045725a  7514                  jne       short 00457270h
  0045725c  8b4e20                mov       ecx,[esi+20h]
  0045725f  66c74616ffff          mov       word [esi+16h],0FFFFh
  00457265  c1f90c                sar       ecx,0Ch
  00457268  66894e14              mov       [esi+14h],cx
  0045726c  66895e10              mov       [esi+10h],bx

LAB_00457270:
  00457270  66395e10              cmp       [esi+10h],bx
  00457274  7531                  jne       short 004572A7h
  00457276  668b4e16              mov       cx,[esi+16h]
  0045727a  8b7e20                mov       edi,[esi+20h]
  0045727d  0fbfd1                movsx     edx,cx
  00457280  c1e20c                shl       edx,0Ch
  00457283  03fa                  add       edi,edx
  00457285  897e20                mov       [esi+20h],edi
  00457288  8bd7                  mov       edx,edi
  0045728a  0fbf7e14              movsx     edi,word [esi+14h]
  0045728e  8bdf                  mov       ebx,edi
  00457290  c1e30c                shl       ebx,0Ch
  00457293  3bd3                  cmp       edx,ebx
  00457295  7d0a                  jge       short 004572A1h
  00457297  83c7ec                add       edi,0FFFFFFECh
  0045729a  c1e70c                shl       edi,0Ch
  0045729d  3bd7                  cmp       edx,edi
  0045729f  7f06                  jg        short 004572A7h

LAB_004572a1:
  004572a1  f7d9                  neg       ecx
  004572a3  66894e16              mov       [esi+16h],cx

LAB_004572a7:
  004572a7  a810                  test      al,10h
  004572a9  0f85b8000000          jne       near 00457367h
  004572af  25ffffffef            and       eax,0EFFFFFFFh
  004572b4  8906                  mov       [esi],eax
  004572b6  5f                    pop       edi
  004572b7  5e                    pop       esi
  004572b8  5b                    pop       ebx
  004572b9  c3                    ret

LAB_004572ba:
  004572ba  668b4e10              mov       cx,[esi+10h]
  004572be  bb01000000            mov       ebx,1
  004572c3  663bcb                cmp       cx,bx
  004572c6  bf02000000            mov       edi,2
  004572cb  7405                  je        short 004572D2h
  004572cd  6685c9                test      cx,cx
  004572d0  753b                  jne       short 0045730Dh

LAB_004572d2:
  004572d2  0d00000010            or        eax,10000000h
  004572d7  66897e10              mov       [esi+10h],di
  004572db  66c746120a00          mov       word [esi+12h],0Ah
  004572e1  8906                  mov       [esi],eax
  004572e3  a1cc975e00            mov       eax,[5E97CCh]
  004572e8  6a00                  push      0
  004572ea  6a00                  push      0
  004572ec  6800100000            push      1000h
  004572f1  8b88a8000000          mov       ecx,[eax+0A8h]
  004572f7  6800040000            push      400h
  004572fc  8b11                  mov       edx,[ecx]
  004572fe  660fb64218            movzx     ax,byte [edx+18h]
  00457303  50                    push      eax
  00457304  56                    push      esi
  00457305  e8760d0100            call      00468080h
  0045730a  83c418                add       esp,18h

LAB_0045730d:
  0045730d  66397e10              cmp       [esi+10h],di
  00457311  7554                  jne       short 00457367h
  00457313  668b4612              mov       ax,[esi+12h]
  00457317  6685c0                test      ax,ax
  0045731a  7e17                  jle       short 00457333h
  0045731c  48                    dec       eax
  0045731d  0fbfc8                movsx     ecx,ax
  00457320  66894612              mov       [esi+12h],ax
  00457324  8b4620                mov       eax,[esi+20h]
  00457327  c1e10c                shl       ecx,0Ch
  0045732a  2bc1                  sub       eax,ecx
  0045732c  894620                mov       [esi+20h],eax
  0045732f  5f                    pop       edi
  00457330  5e                    pop       esi
  00457331  5b                    pop       ebx
  00457332  c3                    ret

LAB_00457333:
  00457333  8b4620                mov       eax,[esi+20h]
  00457336  0500500000            add       eax,5000h
  0045733b  894620                mov       [esi+20h],eax
  0045733e  8bc8                  mov       ecx,eax
  00457340  0fbf4614              movsx     eax,word [esi+14h]
  00457344  c1e00c                shl       eax,0Ch
  00457347  3bc1                  cmp       eax,ecx
  00457349  7f1c                  jg        short 00457367h
  0045734b  894620                mov       [esi+20h],eax
  0045734e  8b06                  mov       eax,[esi]
  00457350  0d00000040            or        eax,40000000h
  00457355  66895e10              mov       [esi+10h],bx
  00457359  66c74616ffff          mov       word [esi+16h],0FFFFh
  0045735f  8906                  mov       [esi],eax
  00457361  66c746126400          mov       word [esi+12h],64h

LAB_00457367:
  00457367  5f                    pop       edi
  00457368  5e                    pop       esi
  00457369  5b                    pop       ebx
  0045736a  c3                    ret
  0045736b  90                    nop

8 cible(s) de saut interne : 0x00457270, 0x004572a1, 0x004572a7, 0x004572ba, 0x004572d2, 0x0045730d, 0x00457333, 0x00457367
