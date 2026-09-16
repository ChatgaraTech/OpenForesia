Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00443170  section .text  offset fichier 0x42570  96 octets

  00443170  33c0                  xor       eax,eax
  00443172  6a00                  push      0
  00443174  a062905e00            mov       al,[5E9062h]
  00443179  68b0335600            push      5633B0h
  0044317e  8d0c80                lea       ecx,[eax+eax*4]
  00443181  c1e103                shl       ecx,3
  00443184  2bc8                  sub       ecx,eax
  00443186  0fbf442410            movsx     eax,word [esp+10h]
  0044318b  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00443192  8b0dcc975e00          mov       ecx,[5E97CCh]
  00443198  52                    push      edx
  00443199  8b5170                mov       edx,[ecx+70h]
  0044319c  8b0482                mov       eax,[edx+eax*4]
  0044319f  50                    push      eax
  004431a0  e8ab111000            call      00544350h
  004431a5  83c410                add       esp,10h
  004431a8  33c0                  xor       eax,eax
  004431aa  c3                    ret
  004431ab  90                    nop
  004431ac  90                    nop
  004431ad  90                    nop
  004431ae  90                    nop
  004431af  90                    nop
  004431b0  53                    push      ebx
  004431b1  33db                  xor       ebx,ebx
  004431b3  b9c0935e00            mov       ecx,5E93C0h
  004431b8  ba04000000            mov       edx,4
  004431bd  8b01                  mov       eax,[ecx]
  004431bf  a900000080            test      eax,80000000h
  004431c4  7408                  je        short 004431CEh
  004431c6  a900000050            test      eax,50000000h
  004431cb  7501                  jne       short 004431CEh
  004431cd  43                    inc       ebx

LAB_004431ce:
  004431ce  81c1                  (bad)
  (octet invalide - la plage demandee deborde probablement du code)
