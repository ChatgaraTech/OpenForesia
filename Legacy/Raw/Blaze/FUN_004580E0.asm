Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004580e0  section .text  offset fichier 0x574e0  141 octets

  004580e0  8b542404              mov       edx,[esp+4]
  004580e4  56                    push      esi
  004580e5  8b35bcde5e00          mov       esi,[5EDEBCh]
  004580eb  57                    push      edi
  004580ec  8b4a1c                mov       ecx,[edx+1Ch]
  004580ef  8b7a20                mov       edi,[edx+20h]
  004580f2  8b06                  mov       eax,[esi]
  004580f4  c1f90c                sar       ecx,0Ch
  004580f7  c1f80c                sar       eax,0Ch
  004580fa  2bc1                  sub       eax,ecx
  004580fc  8b4e04                mov       ecx,[esi+4]
  004580ff  c1ff0c                sar       edi,0Ch
  00458102  c1f90c                sar       ecx,0Ch
  00458105  2bcf                  sub       ecx,edi
  00458107  8b7a24                mov       edi,[edx+24h]
  0045810a  8b5608                mov       edx,[esi+8]
  0045810d  8bf0                  mov       esi,eax
  0045810f  c1ff0c                sar       edi,0Ch
  00458112  0faff0                imul      esi,eax
  00458115  c1fa0c                sar       edx,0Ch
  00458118  8bc1                  mov       eax,ecx
  0045811a  2bd7                  sub       edx,edi
  0045811c  0fafc1                imul      eax,ecx
  0045811f  8bca                  mov       ecx,edx
  00458121  03f0                  add       esi,eax
  00458123  0fafca                imul      ecx,edx
  00458126  03f1                  add       esi,ecx
  00458128  56                    push      esi
  00458129  e8e2d80e00            call      00545A10h
  0045812e  8b74241c              mov       esi,[esp+1Ch]
  00458132  8bc8                  mov       ecx,eax
  00458134  33c0                  xor       eax,eax
  00458136  3bce                  cmp       ecx,esi
  00458138  7d30                  jge       short 0045816Ah
  0045813a  8bc6                  mov       eax,esi
  0045813c  2bc1                  sub       eax,ecx
  0045813e  c1e00c                shl       eax,0Ch
  00458141  99                    cdq
  00458142  f7fe                  idiv      esi
  00458144  0fbf542418            movsx     edx,word [esp+18h]
  00458149  0fbf4c2410            movsx     ecx,word [esp+10h]
  0045814e  0fafc2                imul      eax,edx
  00458151  c1f80c                sar       eax,0Ch
  00458154  50                    push      eax
  00458155  50                    push      eax
  00458156  0fbf44241c            movsx     eax,word [esp+1Ch]
  0045815b  50                    push      eax
  0045815c  51                    push      ecx
  0045815d  e83e4d0e00            call      0053CEA0h
  00458162  83c410                add       esp,10h
  00458165  b801000000            mov       eax,1

LAB_0045816a:
  0045816a  5f                    pop       edi
  0045816b  5e                    pop       esi
  0045816c  c3                    ret

1 cible(s) de saut interne : 0x0045816a
