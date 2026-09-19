Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00441b80  section .text  offset fichier 0x40f80  176 octets

  00441b80  53                    push      ebx
  00441b81  56                    push      esi
  00441b82  8b74240c              mov       esi,[esp+0Ch]
  00441b86  33db                  xor       ebx,ebx
  00441b88  b901000000            mov       ecx,1
  00441b8d  66395e10              cmp       [esi+10h],bx
  00441b91  7537                  jne       short 00441BCAh
  00441b93  a0c4de5e00            mov       al,[5EDEC4h]
  00441b98  c6460a0f              mov       byte [esi+0Ah],0Fh
  00441b9c  fec0                  inc       al
  00441b9e  885e0b                mov       [esi+0Bh],bl
  00441ba1  884604                mov       [esi+4],al
  00441ba4  b8007d0000            mov       eax,7D00h
  00441ba9  66894628              mov       [esi+28h],ax
  00441bad  6689462a              mov       [esi+2Ah],ax
  00441bb1  6689462c              mov       [esi+2Ch],ax
  00441bb5  66894e10              mov       [esi+10h],cx
  00441bb9  66895e12              mov       [esi+12h],bx
  00441bbd  c6463bc0              mov       byte [esi+3Bh],0C0h
  00441bc1  885e38                mov       [esi+38h],bl
  00441bc4  885e39                mov       [esi+39h],bl
  00441bc7  885e3a                mov       [esi+3Ah],bl

LAB_00441bca:
  00441bca  66394e10              cmp       [esi+10h],cx
  00441bce  754b                  jne       short 00441C1Bh
  00441bd0  668146322c01          add       word [esi+32h],12Ch
  00441bd6  b938ffffff            mov       ecx,0FFFFFF38h
  00441bdb  66014e28              add       [esi+28h],cx
  00441bdf  668b4628              mov       ax,[esi+28h]
  00441be3  66014e2a              add       [esi+2Ah],cx
  00441be7  66014e2c              add       [esi+2Ch],cx
  00441beb  663d606d              cmp       ax,6D60h
  00441bef  7f0f                  jg        short 00441C00h
  00441bf1  6a02                  push      2
  00441bf3  6a02                  push      2
  00441bf5  6a02                  push      2
  00441bf7  56                    push      esi
  00441bf8  e8e331fcff            call      00404DE0h
  00441bfd  83c410                add       esp,10h

LAB_00441c00:
  00441c00  66395e28              cmp       [esi+28h],bx
  00441c04  7d15                  jge       short 00441C1Bh
  00441c06  8b06                  mov       eax,[esi]
  00441c08  66895e28              mov       [esi+28h],bx
  00441c0c  25ffffff7f            and       eax,7FFFFFFFh
  00441c11  66895e2a              mov       [esi+2Ah],bx
  00441c15  66895e2c              mov       [esi+2Ch],bx
  00441c19  8906                  mov       [esi],eax

LAB_00441c1b:
  00441c1b  66ff4616              inc       word [esi+16h]
  00441c1f  5e                    pop       esi
  00441c20  5b                    pop       ebx
  00441c21  c3                    ret
  00441c22  90                    nop
  00441c23  90                    nop
  00441c24  90                    nop
  00441c25  90                    nop
  00441c26  90                    nop
  00441c27  90                    nop
  00441c28  90                    nop
  00441c29  90                    nop
  00441c2a  90                    nop
  00441c2b  90                    nop
  00441c2c  90                    nop
  00441c2d  90                    nop
  00441c2e  90                    nop
  00441c2f  90                    nop

3 cible(s) de saut interne : 0x00441bca, 0x00441c00, 0x00441c1b
