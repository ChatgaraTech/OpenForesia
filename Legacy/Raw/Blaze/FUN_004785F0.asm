Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004785f0  section .text  offset fichier 0x779f0  205 octets

  004785f0  53                    push      ebx
  004785f1  56                    push      esi
  004785f2  8b74240c              mov       esi,[esp+0Ch]
  004785f6  57                    push      edi
  004785f7  66837e1000            cmp       word [esi+10h],0
  004785fc  7545                  jne       short 00478643h
  004785fe  6a00                  push      0
  00478600  56                    push      esi
  00478601  e89ac5ffff            call      00474BA0h
  00478606  83c408                add       esp,8
  00478609  8bf8                  mov       edi,eax
  0047860b  897e78                mov       [esi+78h],edi
  0047860e  897e70                mov       [esi+70h],edi
  00478611  57                    push      edi
  00478612  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00478619  c7462800100010        mov       dword [esi+28h],10001000h
  00478620  66c7462c1f00          mov       word [esi+2Ch],1Fh
  00478626  66c7869a0000000040    mov       word [esi+9Ah],4000h
  0047862f  66c7461a0100          mov       word [esi+1Ah],1
  00478635  e886050c00            call      00538BC0h
  0047863a  83c404                add       esp,4
  0047863d  66894612              mov       [esi+12h],ax
  00478641  eb03                  jmp       short 00478646h

LAB_00478643:
  00478643  8b7e78                mov       edi,[esi+78h]

LAB_00478646:
  00478646  e8f5ea0c00            call      00547140h
  0047864b  8bd8                  mov       ebx,eax
  0047864d  53                    push      ebx
  0047864e  e8c9d30c00            call      00545A1Ch
  00478653  c1e004                shl       eax,4
  00478656  53                    push      ebx
  00478657  894650                mov       [esi+50h],eax
  0047865a  e8b7d30c00            call      00545A16h
  0047865f  c1e004                shl       eax,4
  00478662  894658                mov       [esi+58h],eax
  00478665  33c0                  xor       eax,eax
  00478667  8a460b                mov       al,[esi+0Bh]
  0047866a  6aff                  push      0FFFFFFFFh
  0047866c  50                    push      eax
  0047866d  68bf000000            push      0BFh
  00478672  83c71c                add       edi,1Ch
  00478675  68c81b5500            push      551BC8h
  0047867a  57                    push      edi
  0047867b  56                    push      esi
  0047867c  c7465400e00100        mov       dword [esi+54h],1E000h
  00478683  e8b8250000            call      0047AC40h
  00478688  83c418                add       esp,18h
  0047868b  66837e1014            cmp       word [esi+10h],14h
  00478690  7c23                  jl        short 004786B5h
  00478692  8b4e74                mov       ecx,[esi+74h]
  00478695  668b5606              mov       dx,[esi+6]
  00478699  6a00                  push      0
  0047869b  66895106              mov       [ecx+6],dx
  0047869f  8b4678                mov       eax,[esi+78h]
  004786a2  8b4e74                mov       ecx,[esi+74h]
  004786a5  50                    push      eax
  004786a6  51                    push      ecx
  004786a7  e8f43b0c00            call      0053C2A0h
  004786ac  83c40c                add       esp,0Ch
  004786af  c70600000000          mov       dword [esi],0

LAB_004786b5:
  004786b5  66ff4610              inc       word [esi+10h]
  004786b9  5f                    pop       edi
  004786ba  5e                    pop       esi
  004786bb  5b                    pop       ebx
  004786bc  c3                    ret

3 cible(s) de saut interne : 0x00478643, 0x00478646, 0x004786b5
