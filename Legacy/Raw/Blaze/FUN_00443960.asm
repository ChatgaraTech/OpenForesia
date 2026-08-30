Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00443960  section .text  offset fichier 0x42d60  58 octets

  00443960  53                    push      ebx
  00443961  33db                  xor       ebx,ebx
  00443963  b9c0935e00            mov       ecx,5E93C0h
  00443968  ba04000000            mov       edx,4

LAB_0044396d:
  0044396d  8b01                  mov       eax,[ecx]
  0044396f  a900000080            test      eax,80000000h
  00443974  7408                  je        short 0044397Eh
  00443976  a900000050            test      eax,50000000h
  0044397b  7501                  jne       short 0044397Eh
  0044397d  43                    inc       ebx

LAB_0044397e:
  0044397e  81c19c000000          add       ecx,9Ch
  00443984  4a                    dec       edx
  00443985  75e6                  jne       short 0044396Dh
  00443987  8b44240c              mov       eax,[esp+0Ch]
  0044398b  25ff000000            and       eax,0FFh
  00443990  889860905e00          mov       [eax+5E9060h],bl
  00443996  33c0                  xor       eax,eax
  00443998  5b                    pop       ebx
  00443999  c3                    ret

2 cible(s) de saut interne : 0x0044396d, 0x0044397e
