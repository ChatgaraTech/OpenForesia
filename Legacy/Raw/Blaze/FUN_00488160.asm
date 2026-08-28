Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00488160  section .text  offset fichier 0x87560  169 octets

  00488160  83ec28                sub       esp,28h
  00488163  53                    push      ebx
  00488164  55                    push      ebp
  00488165  56                    push      esi
  00488166  8b742438              mov       esi,[esp+38h]
  0048816a  57                    push      edi
  0048816b  0fbf7e32              movsx     edi,word [esi+32h]
  0048816f  57                    push      edi
  00488170  e8a7d80b00            call      00545A1Ch
  00488175  57                    push      edi
  00488176  89442414              mov       [esp+14h],eax
  0048817a  e897d80b00            call      00545A16h
  0048817f  8b5650                mov       edx,[esi+50h]
  00488182  89442418              mov       [esp+18h],eax
  00488186  0fbf9e9a000000        movsx     ebx,word [esi+9Ah]
  0048818d  8bc2                  mov       eax,edx
  0048818f  8bfa                  mov       edi,edx
  00488191  c1f803                sar       eax,3
  00488194  8b4e5c                mov       ecx,[esi+5Ch]
  00488197  2bf8                  sub       edi,eax
  00488199  c1fa08                sar       edx,8
  0048819c  0faf4c2440            imul      ecx,[esp+40h]
  004881a1  2bfa                  sub       edi,edx
  004881a3  8b5654                mov       edx,[esi+54h]
  004881a6  8bc3                  mov       eax,ebx
  004881a8  8bea                  mov       ebp,edx
  004881aa  c1f808                sar       eax,8
  004881ad  c1fd08                sar       ebp,8
  004881b0  2bc5                  sub       eax,ebp
  004881b2  897e50                mov       [esi+50h],edi
  004881b5  2bc3                  sub       eax,ebx
  004881b7  03c2                  add       eax,edx
  004881b9  8b5658                mov       edx,[esi+58h]
  004881bc  8bea                  mov       ebp,edx
  004881be  8bda                  mov       ebx,edx
  004881c0  c1fd08                sar       ebp,8
  004881c3  2bdd                  sub       ebx,ebp
  004881c5  8b6e20                mov       ebp,[esi+20h]
  004881c8  c1fa03                sar       edx,3
  004881cb  c1f90c                sar       ecx,0Ch
  004881ce  2bda                  sub       ebx,edx
  004881d0  8bd1                  mov       edx,ecx
  004881d2  0faf542410            imul      edx,[esp+10h]
  004881d7  0faf4c2418            imul      ecx,[esp+18h]
  004881dc  c1fa0c                sar       edx,0Ch
  004881df  03d7                  add       edx,edi
  004881e1  8b7e1c                mov       edi,[esi+1Ch]
  004881e4  c1f90c                sar       ecx,0Ch
  004881e7  894654                mov       [esi+54h],eax
  004881ea  03e8                  add       ebp,eax
  004881ec  8b4624                mov       eax,[esi+24h]
  004881ef  03cb                  add       ecx,ebx
  004881f1  03fa                  add       edi,edx
  004881f3  03c1                  add       eax,ecx
  004881f5  897e1c                mov       [esi+1Ch],edi
  004881f8  895e58                mov       [esi+58h],ebx
  004881fb  896e20                mov       [esi+20h],ebp
  004881fe  894624                mov       [esi+24h],eax
  00488201  5f                    pop       edi
  00488202  5e                    pop       esi
  00488203  5d                    pop       ebp
  00488204  5b                    pop       ebx
  00488205  83c428                add       esp,28h
  00488208  c3                    ret
