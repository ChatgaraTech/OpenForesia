Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041bf00  section .text  offset fichier 0x1b300  160 octets

  0041bf00  53                    push      ebx
  0041bf01  56                    push      esi
  0041bf02  8b74240c              mov       esi,[esp+0Ch]
  0041bf06  33db                  xor       ebx,ebx
  0041bf08  b901000000            mov       ecx,1
  0041bf0d  66395e10              cmp       [esi+10h],bx
  0041bf11  7543                  jne       short 0041BF56h
  0041bf13  8a4614                mov       al,[esi+14h]
  0041bf16  66394e14              cmp       [esi+14h],cx
  0041bf1a  66894e10              mov       [esi+10h],cx
  0041bf1e  884604                mov       [esi+4],al
  0041bf21  7506                  jne       short 0041BF29h
  0041bf23  c6460a02              mov       byte [esi+0Ah],2
  0041bf27  eb04                  jmp       short 0041BF2Dh

LAB_0041bf29:
  0041bf29  c6460a09              mov       byte [esi+0Ah],9

LAB_0041bf2d:
  0041bf2d  8b4620                mov       eax,[esi+20h]
  0041bf30  885e0b                mov       [esi+0Bh],bl
  0041bf33  0500400100            add       eax,14000h
  0041bf38  66895e28              mov       [esi+28h],bx
  0041bf3c  894620                mov       [esi+20h],eax
  0041bf3f  b0ff                  mov       al,0FFh
  0041bf41  66895e2a              mov       [esi+2Ah],bx
  0041bf45  66895e2c              mov       [esi+2Ch],bx
  0041bf49  66895e34              mov       [esi+34h],bx
  0041bf4d  884638                mov       [esi+38h],al
  0041bf50  884639                mov       [esi+39h],al
  0041bf53  88463a                mov       [esi+3Ah],al

LAB_0041bf56:
  0041bf56  66394e10              cmp       [esi+10h],cx
  0041bf5a  7539                  jne       short 0041BF95h
  0041bf5c  8a4604                mov       al,[esi+4]
  0041bf5f  3ac1                  cmp       al,cl
  0041bf61  b890010000            mov       eax,190h
  0041bf66  7405                  je        short 0041BF6Dh
  0041bf68  b820030000            mov       eax,320h

LAB_0041bf6d:
  0041bf6d  66014628              add       [esi+28h],ax
  0041bf71  6601462a              add       [esi+2Ah],ax
  0041bf75  6601462c              add       [esi+2Ch],ax
  0041bf79  6afa                  push      0FFFFFFFAh
  0041bf7b  6afa                  push      0FFFFFFFAh
  0041bf7d  6afa                  push      0FFFFFFFAh
  0041bf7f  56                    push      esi
  0041bf80  e85b8efeff            call      00404DE0h
  0041bf85  8a4638                mov       al,[esi+38h]
  0041bf88  83c410                add       esp,10h
  0041bf8b  3ac3                  cmp       al,bl
  0041bf8d  7506                  jne       short 0041BF95h
  0041bf8f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041bf95:
  0041bf95  5e                    pop       esi
  0041bf96  5b                    pop       ebx
  0041bf97  c3                    ret
  0041bf98  90                    nop
  0041bf99  90                    nop
  0041bf9a  90                    nop
  0041bf9b  90                    nop
  0041bf9c  90                    nop
  0041bf9d  90                    nop
  0041bf9e  90                    nop
  0041bf9f  90                    nop

5 cible(s) de saut interne : 0x0041bf29, 0x0041bf2d, 0x0041bf56, 0x0041bf6d, 0x0041bf95
