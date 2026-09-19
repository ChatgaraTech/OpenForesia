Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004451b0  section .text  offset fichier 0x445b0  160 octets

  004451b0  51                    push      ecx
  004451b1  53                    push      ebx
  004451b2  55                    push      ebp
  004451b3  8b2db4876000          mov       ebp,[6087B4h]
  004451b9  56                    push      esi
  004451ba  57                    push      edi
  004451bb  33ff                  xor       edi,edi
  004451bd  897c2410              mov       [esp+10h],edi
  004451c1  b900000f00            mov       ecx,0F0000h
  004451c6  bac0935e00            mov       edx,5E93C0h
  004451cb  be04000000            mov       esi,4
  004451d0  b308                  mov       bl,8

LAB_004451d2:
  004451d2  8b02                  mov       eax,[edx]
  004451d4  a900000080            test      eax,80000000h
  004451d9  7418                  je        short 004451F3h
  004451db  a900000150            test      eax,50010000h
  004451e0  7511                  jne       short 004451F3h
  004451e2  8b4500                mov       eax,[ebp]
  004451e5  47                    inc       edi
  004451e6  849c0850010000        test      [eax+ecx+150h],bl
  004451ed  7404                  je        short 004451F3h
  004451ef  ff442410              inc       dword [esp+10h]

LAB_004451f3:
  004451f3  81c100200000          add       ecx,2000h
  004451f9  81c29c000000          add       edx,9Ch
  004451ff  4e                    dec       esi
  00445200  75d0                  jne       short 004451D2h
  00445202  8b542418              mov       edx,[esp+18h]
  00445206  8b4500                mov       eax,[ebp]
  00445209  33c9                  xor       ecx,ecx
  0044520b  8a4a08                mov       cl,[edx+8]
  0044520e  33d2                  xor       edx,edx
  00445210  c1e10d                shl       ecx,0Dh
  00445213  8a940150010f00        mov       dl,[ecx+eax+0F0150h]
  0044521a  8bc2                  mov       eax,edx
  0044521c  c1e803                shr       eax,3
  0044521f  83e001                and       eax,1
  00445222  6683ff02              cmp       di,2
  00445226  7516                  jne       short 0044523Eh
  00445228  66837c241001          cmp       word [esp+10h],1
  0044522e  750e                  jne       short 0044523Eh
  00445230  663d0100              cmp       ax,1
  00445234  7508                  jne       short 0044523Eh
  00445236  33c0                  xor       eax,eax
  00445238  5f                    pop       edi
  00445239  5e                    pop       esi
  0044523a  5d                    pop       ebp
  0044523b  5b                    pop       ebx
  0044523c  59                    pop       ecx
  0044523d  c3                    ret

LAB_0044523e:
  0044523e  5f                    pop       edi
  0044523f  5e                    pop       esi
  00445240  5d                    pop       ebp
  00445241  b801000000            mov       eax,1
  00445246  5b                    pop       ebx
  00445247  59                    pop       ecx
  00445248  c3                    ret
  00445249  90                    nop
  0044524a  90                    nop
  0044524b  90                    nop
  0044524c  90                    nop
  0044524d  90                    nop
  0044524e  90                    nop
  0044524f  90                    nop

3 cible(s) de saut interne : 0x004451d2, 0x004451f3, 0x0044523e
