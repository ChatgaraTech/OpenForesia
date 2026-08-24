Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00468110  section .text  offset fichier 0x67510  160 octets

  00468110  8b15bcde5e00          mov       edx,[5EDEBCh]
  00468116  53                    push      ebx
  00468117  56                    push      esi
  00468118  8b74240c              mov       esi,[esp+0Ch]
  0046811c  8b02                  mov       eax,[edx]
  0046811e  57                    push      edi
  0046811f  8b4e1c                mov       ecx,[esi+1Ch]
  00468122  8d7e1c                lea       edi,[esi+1Ch]
  00468125  8b5e20                mov       ebx,[esi+20h]
  00468128  c1f80c                sar       eax,0Ch
  0046812b  c1f90c                sar       ecx,0Ch
  0046812e  2bc1                  sub       eax,ecx
  00468130  8b4a04                mov       ecx,[edx+4]
  00468133  8b5208                mov       edx,[edx+8]
  00468136  c1fb0c                sar       ebx,0Ch
  00468139  c1f90c                sar       ecx,0Ch
  0046813c  2bcb                  sub       ecx,ebx
  0046813e  8b5e24                mov       ebx,[esi+24h]
  00468141  c1fb0c                sar       ebx,0Ch
  00468144  c1fa0c                sar       edx,0Ch
  00468147  2bd3                  sub       edx,ebx
  00468149  8bd8                  mov       ebx,eax
  0046814b  0fafd8                imul      ebx,eax
  0046814e  8bc1                  mov       eax,ecx
  00468150  0fafc1                imul      eax,ecx
  00468153  8bca                  mov       ecx,edx
  00468155  03d8                  add       ebx,eax
  00468157  0fafca                imul      ecx,edx
  0046815a  03d9                  add       ebx,ecx
  0046815c  53                    push      ebx
  0046815d  e8aed80d00            call      00545A10h
  00468162  33c9                  xor       ecx,ecx
  00468164  3d08070000            cmp       eax,708h
  00468169  7d29                  jge       short 00468194h
  0046816b  0fbf54241c            movsx     edx,word [esp+1Ch]
  00468170  0fbf4c2414            movsx     ecx,word [esp+14h]
  00468175  0fbf442418            movsx     eax,word [esp+18h]
  0046817a  52                    push      edx
  0046817b  81c1c0000000          add       ecx,0C0h
  00468181  50                    push      eax
  00468182  51                    push      ecx
  00468183  6a00                  push      0
  00468185  57                    push      edi
  00468186  56                    push      esi
  00468187  e8b42a0100            call      0047AC40h
  0046818c  83c418                add       esp,18h
  0046818f  b901000000            mov       ecx,1

LAB_00468194:
  00468194  5f                    pop       edi
  00468195  5e                    pop       esi
  00468196  668bc1                mov       ax,cx
  00468199  5b                    pop       ebx
  0046819a  c3                    ret
  0046819b  90                    nop
  0046819c  90                    nop
  0046819d  90                    nop
  0046819e  90                    nop
  0046819f  90                    nop
  004681a0  8b442404              mov       eax,[esp+4]
  004681a4  f70000000040          test      dword [eax],40000000h
  004681aa  7554                  jne       short 00468200h
  004681ac  b9010000              (bad)
  (octet invalide - la plage demandee deborde probablement du code)
