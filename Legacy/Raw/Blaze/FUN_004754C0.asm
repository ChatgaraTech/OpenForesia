
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004754c0  section .text  offset fichier 0x748c0  256 octets

  004754c0  83ec0c                sub       esp,0Ch
  004754c3  53                    push      ebx
  004754c4  55                    push      ebp
  004754c5  56                    push      esi
  004754c6  8b74241c              mov       esi,[esp+1Ch]
  004754ca  57                    push      edi
  004754cb  668b4610              mov       ax,[esi+10h]
  004754cf  8b6e6c                mov       ebp,[esi+6Ch]
  004754d2  6685c0                test      ax,ax
  004754d5  7520                  jne       short 004754F7h
  004754d7  6689460a              mov       [esi+0Ah],ax
  004754db  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  004754e2  c7462800100010        mov       dword [esi+28h],10001000h
  004754e9  884640                mov       [esi+40h],al
  004754ec  66c7869a0000000040    mov       word [esi+9Ah],4000h
  004754f5  eb1e                  jmp       short 00475515h

LAB_004754f7:
  004754f7  663d0100              cmp       ax,1
  004754fb  7518                  jne       short 00475515h
  004754fd  8d461c                lea       eax,[esi+1Ch]
  00475500  50                    push      eax
  00475501  6800120000            push      1200h
  00475506  6800040000            push      400h
  0047550b  6a0b                  push      0Bh
  0047550d  e80e860c00            call      0053DB20h
  00475512  83c410                add       esp,10h

LAB_00475515:
  00475515  e8261c0d00            call      00547140h
  0047551a  8bf8                  mov       edi,eax
  0047551c  81e7ff0f0000          and       edi,0FFFh
  00475522  e8191c0d00            call      00547140h
  00475527  99                    cdq
  00475528  b93c000000            mov       ecx,3Ch
  0047552d  57                    push      edi
  0047552e  f7f9                  idiv      ecx
  00475530  8bda                  mov       ebx,edx
  00475532  e8e5040d00            call      00545A1Ch
  00475537  0fafc3                imul      eax,ebx
  0047553a  8b4d1c                mov       ecx,[ebp+1Ch]
  0047553d  57                    push      edi
  0047553e  03c1                  add       eax,ecx
  00475540  89442414              mov       [esp+14h],eax
  00475544  8b5520                mov       edx,[ebp+20h]
  00475547  89542418              mov       [esp+18h],edx
  0047554b  e8c6040d00            call      00545A16h
  00475550  0fafc3                imul      eax,ebx
  00475553  8b5d24                mov       ebx,[ebp+24h]
  00475556  57                    push      edi
  00475557  03c3                  add       eax,ebx
  00475559  8944241c              mov       [esp+1Ch],eax
  0047555d  e8ba040d00            call      00545A1Ch
  00475562  c1e003                shl       eax,3
  00475565  57                    push      edi
  00475566  894650                mov       [esi+50h],eax
  00475569  e8a8040d00            call      00545A16h
  0047556e  c1e003                shl       eax,3
  00475571  894658                mov       [esi+58h],eax
  00475574  33c0                  xor       eax,eax
  00475576  8a460b                mov       al,[esi+0Bh]
  00475579  6aff                  push      0FFFFFFFFh
  0047557b  50                    push      eax
  0047557c  6a3c                  push      3Ch
  0047557e  8d4c241c              lea       ecx,[esp+1Ch]
  00475582  68b81a5500            push      551AB8h
  00475587  51                    push      ecx
  00475588  56                    push      esi
  00475589  c7465400000100        mov       dword [esi+54h],10000h
  00475590  e8ab560000            call      0047AC40h
  00475595  83c418                add       esp,18h
  00475598  66ff4610              inc       word [esi+10h]
  0047559c  66837e1010            cmp       word [esi+10h],10h
  004755a1  7c13                  jl        short 004755B6h
  004755a3  6a00                  push      0
  004755a5  6a05                  push      5
  004755a7  55                    push      ebp
  004755a8  c70600000000          mov       dword [esi],0
  004755ae  e8fd410c00            call      005397B0h
  004755b3  83c40c                add       esp,0Ch

LAB_004755b6:
  004755b6  5f                    pop       edi
  004755b7  5e                    pop       esi
  004755b8  5d                    pop       ebp
  004755b9  5b                    pop       ebx
  004755ba  83c40c                add       esp,0Ch
  004755bd  c3                    ret
  004755be  90                    nop
  004755bf  90                    nop

3 cible(s) de saut interne : 0x004754f7, 0x00475515, 0x004755b6


