Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004414e0  section .text  offset fichier 0x408e0  144 octets

  004414e0  56                    push      esi
  004414e1  8b742408              mov       esi,[esp+8]
  004414e5  33c0                  xor       eax,eax
  004414e7  b901000000            mov       ecx,1
  004414ec  66394610              cmp       [esi+10h],ax
  004414f0  7534                  jne       short 00441526h
  004414f2  8a15c4de5e00          mov       dl,[5EDEC4h]
  004414f8  88460b                mov       [esi+0Bh],al
  004414fb  66894628              mov       [esi+28h],ax
  004414ff  6689462c              mov       [esi+2Ch],ax
  00441503  66894612              mov       [esi+12h],ax
  00441507  8b4620                mov       eax,[esi+20h]
  0044150a  80c203                add       dl,3
  0044150d  0500600e00            add       eax,0E6000h
  00441512  885604                mov       [esi+4],dl
  00441515  c6460a08              mov       byte [esi+0Ah],8
  00441519  66c7462a0010          mov       word [esi+2Ah],1000h
  0044151f  66894e10              mov       [esi+10h],cx
  00441523  894620                mov       [esi+20h],eax

LAB_00441526:
  00441526  66394e10              cmp       [esi+10h],cx
  0044152a  753f                  jne       short 0044156Bh
  0044152c  b9e8030000            mov       ecx,3E8h
  00441531  66014e28              add       [esi+28h],cx
  00441535  668b4628              mov       ax,[esi+28h]
  00441539  66014e2c              add       [esi+2Ch],cx
  0044153d  663d983a              cmp       ax,3A98h
  00441541  7c0f                  jl        short 00441552h
  00441543  6afb                  push      0FFFFFFFBh
  00441545  6afb                  push      0FFFFFFFBh
  00441547  6afb                  push      0FFFFFFFBh
  00441549  56                    push      esi
  0044154a  e89138fcff            call      00404DE0h
  0044154f  83c410                add       esp,10h

LAB_00441552:
  00441552  b830750000            mov       eax,7530h
  00441557  66394628              cmp       [esi+28h],ax
  0044155b  7c0e                  jl        short 0044156Bh
  0044155d  66894628              mov       [esi+28h],ax
  00441561  6689462c              mov       [esi+2Ch],ax
  00441565  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044156b:
  0044156b  5e                    pop       esi
  0044156c  c3                    ret
  0044156d  90                    nop
  0044156e  90                    nop
  0044156f  90                    nop

3 cible(s) de saut interne : 0x00441526, 0x00441552, 0x0044156b
