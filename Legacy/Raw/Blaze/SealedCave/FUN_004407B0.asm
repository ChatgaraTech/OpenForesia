Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004407b0  section .text  offset fichier 0x3fbb0  144 octets

  004407b0  53                    push      ebx
  004407b1  56                    push      esi
  004407b2  8b74240c              mov       esi,[esp+0Ch]
  004407b6  33db                  xor       ebx,ebx
  004407b8  b901000000            mov       ecx,1
  004407bd  66395e10              cmp       [esi+10h],bx
  004407c1  7539                  jne       short 004407FCh
  004407c3  66894e10              mov       [esi+10h],cx
  004407c7  a0c4de5e00            mov       al,[5EDEC4h]
  004407cc  884604                mov       [esi+4],al
  004407cf  8b4620                mov       eax,[esi+20h]
  004407d2  0500000500            add       eax,50000h
  004407d7  c6460a03              mov       byte [esi+0Ah],3
  004407db  894620                mov       [esi+20h],eax
  004407de  b0b4                  mov       al,0B4h
  004407e0  885e0b                mov       [esi+0Bh],bl
  004407e3  66895e28              mov       [esi+28h],bx
  004407e7  66895e2a              mov       [esi+2Ah],bx
  004407eb  66895e2c              mov       [esi+2Ch],bx
  004407ef  66895e34              mov       [esi+34h],bx
  004407f3  884638                mov       [esi+38h],al
  004407f6  884639                mov       [esi+39h],al
  004407f9  88463a                mov       [esi+3Ah],al

LAB_004407fc:
  004407fc  66394e10              cmp       [esi+10h],cx
  00440800  752d                  jne       short 0044082Fh
  00440802  6af6                  push      0FFFFFFF6h
  00440804  6af6                  push      0FFFFFFF6h
  00440806  b878050000            mov       eax,578h
  0044080b  6af6                  push      0FFFFFFF6h
  0044080d  66014628              add       [esi+28h],ax
  00440811  6601462a              add       [esi+2Ah],ax
  00440815  6601462c              add       [esi+2Ch],ax
  00440819  56                    push      esi
  0044081a  e8c145fcff            call      00404DE0h
  0044081f  8a4638                mov       al,[esi+38h]
  00440822  83c410                add       esp,10h
  00440825  3ac3                  cmp       al,bl
  00440827  7506                  jne       short 0044082Fh
  00440829  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044082f:
  0044082f  5e                    pop       esi
  00440830  5b                    pop       ebx
  00440831  c3                    ret
  00440832  90                    nop
  00440833  90                    nop
  00440834  90                    nop
  00440835  90                    nop
  00440836  90                    nop
  00440837  90                    nop
  00440838  90                    nop
  00440839  90                    nop
  0044083a  90                    nop
  0044083b  90                    nop
  0044083c  90                    nop
  0044083d  90                    nop
  0044083e  90                    nop
  0044083f  90                    nop

2 cible(s) de saut interne : 0x004407fc, 0x0044082f
