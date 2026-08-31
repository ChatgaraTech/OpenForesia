Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004484b0  section .text  offset fichier 0x478b0  61 octets

  004484b0  56                    push      esi
  004484b1  8b74240c              mov       esi,[esp+0Ch]
  004484b5  57                    push      edi
  004484b6  8b7c240c              mov       edi,[esp+0Ch]
  004484ba  56                    push      esi
  004484bb  57                    push      edi
  004484bc  e8efdffeff            call      004364B0h
  004484c1  83c408                add       esp,8
  004484c4  6685c0                test      ax,ax
  004484c7  751d                  jne       short 004484E6h
  004484c9  8b442414              mov       eax,[esp+14h]
  004484cd  50                    push      eax
  004484ce  56                    push      esi
  004484cf  57                    push      edi
  004484d0  e8db3a0f00            call      0053BFB0h
  004484d5  83c40c                add       esp,0Ch
  004484d8  33c9                  xor       ecx,ecx
  004484da  6685c0                test      ax,ax
  004484dd  0f95c1                setne     cl
  004484e0  668bc1                mov       ax,cx
  004484e3  5f                    pop       edi
  004484e4  5e                    pop       esi
  004484e5  c3                    ret

LAB_004484e6:
  004484e6  5f                    pop       edi
  004484e7  66b80100              mov       ax,1
  004484eb  5e                    pop       esi
  004484ec  c3                    ret

1 cible(s) de saut interne : 0x004484e6
