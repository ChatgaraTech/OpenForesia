Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004487b0  section .text  offset fichier 0x47bb0  61 octets

  004487b0  56                    push      esi
  004487b1  8b74240c              mov       esi,[esp+0Ch]
  004487b5  57                    push      edi
  004487b6  8b7c240c              mov       edi,[esp+0Ch]
  004487ba  56                    push      esi
  004487bb  57                    push      edi
  004487bc  e8efdcfeff            call      004364B0h
  004487c1  83c408                add       esp,8
  004487c4  6685c0                test      ax,ax
  004487c7  751d                  jne       short 004487E6h
  004487c9  8b442414              mov       eax,[esp+14h]
  004487cd  50                    push      eax
  004487ce  56                    push      esi
  004487cf  57                    push      edi
  004487d0  e84b280f00            call      0053B020h
  004487d5  83c40c                add       esp,0Ch
  004487d8  33c9                  xor       ecx,ecx
  004487da  6685c0                test      ax,ax
  004487dd  0f95c1                setne     cl
  004487e0  668bc1                mov       ax,cx
  004487e3  5f                    pop       edi
  004487e4  5e                    pop       esi
  004487e5  c3                    ret

LAB_004487e6:
  004487e6  5f                    pop       edi
  004487e7  66b80100              mov       ax,1
  004487eb  5e                    pop       esi
  004487ec  c3                    ret

1 cible(s) de saut interne : 0x004487e6
