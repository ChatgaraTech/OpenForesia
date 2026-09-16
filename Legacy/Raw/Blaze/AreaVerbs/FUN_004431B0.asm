Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004431b0  section .text  offset fichier 0x425b0  54 octets

  004431b0  53                    push      ebx
  004431b1  33db                  xor       ebx,ebx
  004431b3  b9c0935e00            mov       ecx,5E93C0h
  004431b8  ba04000000            mov       edx,4

LAB_004431bd:
  004431bd  8b01                  mov       eax,[ecx]
  004431bf  a900000080            test      eax,80000000h
  004431c4  7408                  je        short 004431CEh
  004431c6  a900000050            test      eax,50000000h
  004431cb  7501                  jne       short 004431CEh
  004431cd  43                    inc       ebx

LAB_004431ce:
  004431ce  81c19c000000          add       ecx,9Ch
  004431d4  4a                    dec       edx
  004431d5  75e6                  jne       short 004431BDh
  004431d7  0fbf44240c            movsx     eax,word [esp+0Ch]
  004431dc  889860905e00          mov       [eax+5E9060h],bl
  004431e2  33c0                  xor       eax,eax
  004431e4  5b                    pop       ebx
  004431e5  c3                    ret

2 cible(s) de saut interne : 0x004431bd, 0x004431ce
