Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00468a20  section .text  offset fichier 0x67e20  160 octets

  00468a20  56                    push      esi
  00468a21  8b742408              mov       esi,[esp+8]
  00468a25  57                    push      edi
  00468a26  33ff                  xor       edi,edi
  00468a28  668b4610              mov       ax,[esi+10h]
  00468a2c  663bc7                cmp       ax,di
  00468a2f  7514                  jne       short 00468A45h
  00468a31  66897e2a              mov       [esi+2Ah],di
  00468a35  66c746128000          mov       word [esi+12h],80h
  00468a3b  6689be98000000        mov       [esi+98h],di
  00468a42  5f                    pop       edi
  00468a43  5e                    pop       esi
  00468a44  c3                    ret

LAB_00468a45:
  00468a45  663d0100              cmp       ax,1
  00468a49  7564                  jne       short 00468AAFh
  00468a4b  6639be98000000        cmp       [esi+98h],di
  00468a52  7531                  jne       short 00468A85h
  00468a54  a1cc975e00            mov       eax,[5E97CCh]
  00468a59  57                    push      edi
  00468a5a  57                    push      edi
  00468a5b  6800200000            push      2000h
  00468a60  8b88a8000000          mov       ecx,[eax+0A8h]
  00468a66  6800040000            push      400h
  00468a6b  8b11                  mov       edx,[ecx]
  00468a6d  660fb64227            movzx     ax,byte [edx+27h]
  00468a72  50                    push      eax
  00468a73  56                    push      esi
  00468a74  e807f6ffff            call      00468080h
  00468a79  83c418                add       esp,18h
  00468a7c  66c786980000000100    mov       word [esi+98h],1

LAB_00468a85:
  00468a85  0fbf4e12              movsx     ecx,word [esi+12h]
  00468a89  51                    push      ecx
  00468a8a  e88dcf0d00            call      00545A1Ch
  00468a8f  668146120001          add       word [esi+12h],100h
  00468a95  c1e00c                shl       eax,0Ch
  00468a98  c1f80b                sar       eax,0Bh
  00468a9b  66817e120008          cmp       word [esi+12h],800h
  00468aa1  6689462a              mov       [esi+2Ah],ax
  00468aa5  7e08                  jle       short 00468AAFh
  00468aa7  66897e10              mov       [esi+10h],di
  00468aab  66897e2a              mov       [esi+2Ah],di

LAB_00468aaf:
  00468aaf  5f                    pop       edi
  00468ab0  5e                    pop       esi
  00468ab1  c3                    ret
  00468ab2  90                    nop
  00468ab3  90                    nop
  00468ab4  90                    nop
  00468ab5  90                    nop
  00468ab6  90                    nop
  00468ab7  90                    nop
  00468ab8  90                    nop
  00468ab9  90                    nop
  00468aba  90                    nop
  00468abb  90                    nop
  00468abc  90                    nop
  00468abd  90                    nop
  00468abe  90                    nop
  00468abf  90                    nop

3 cible(s) de saut interne : 0x00468a45, 0x00468a85, 0x00468aaf
