Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004821a0  section .text  offset fichier 0x815a0  348 octets

  004821a0  83ec18                sub       esp,18h
  004821a3  56                    push      esi
  004821a4  8b742420              mov       esi,[esp+20h]
  004821a8  f70600000020          test      dword [esi],20000000h
  004821ae  740b                  je        short 004821BBh
  004821b0  c70600000000          mov       dword [esi],0
  004821b6  5e                    pop       esi
  004821b7  83c418                add       esp,18h
  004821ba  c3                    ret

LAB_004821bb:
  004821bb  66837e1000            cmp       word [esi+10h],0
  004821c0  57                    push      edi
  004821c1  55                    push      ebp
  004821c2  53                    push      ebx
  004821c3  751e                  jne       short 004821E3h
  004821c5  0fbf4616              movsx     eax,word [esi+16h]
  004821c9  66c746100100          mov       word [esi+10h],1
  004821cf  8d0c80                lea       ecx,[eax+eax*4]
  004821d2  c1e103                shl       ecx,3
  004821d5  2bc8                  sub       ecx,eax
  004821d7  8d3c8de0de5e00        lea       edi,[ecx*4+5EDEE0h]
  004821de  897e70                mov       [esi+70h],edi
  004821e1  eb03                  jmp       short 004821E6h

LAB_004821e3:
  004821e3  8b7e70                mov       edi,[esi+70h]

LAB_004821e6:
  004821e6  0fbf4628              movsx     eax,word [esi+28h]
  004821ea  8b5624                mov       edx,[esi+24h]
  004821ed  8b5e1c                mov       ebx,[esi+1Ch]
  004821f0  0fbf4f34              movsx     ecx,word [edi+34h]
  004821f4  8d0480                lea       eax,[eax+eax*4]
  004821f7  89542424              mov       [esp+24h],edx
  004821fb  0fbf5732              movsx     edx,word [edi+32h]
  004821ff  8b6e20                mov       ebp,[esi+20h]
  00482202  81c200040000          add       edx,400h
  00482208  c1e004                shl       eax,4
  0048220b  c1f80c                sar       eax,0Ch
  0048220e  f7d9                  neg       ecx
  00482210  50                    push      eax
  00482211  51                    push      ecx
  00482212  8d442418              lea       eax,[esp+18h]
  00482216  52                    push      edx
  00482217  50                    push      eax
  00482218  e8b3080b00            call      00532AD0h
  0048221d  8b4f1c                mov       ecx,[edi+1Ch]
  00482220  8b542420              mov       edx,[esp+20h]
  00482224  8b442424              mov       eax,[esp+24h]
  00482228  03ca                  add       ecx,edx
  0048222a  894e1c                mov       [esi+1Ch],ecx
  0048222d  8b5720                mov       edx,[edi+20h]
  00482230  8b4c2428              mov       ecx,[esp+28h]
  00482234  03d0                  add       edx,eax
  00482236  895620                mov       [esi+20h],edx
  00482239  8b4724                mov       eax,[edi+24h]
  0048223c  8b7e1c                mov       edi,[esi+1Ch]
  0048223f  03c1                  add       eax,ecx
  00482241  2bfb                  sub       edi,ebx
  00482243  8bda                  mov       ebx,edx
  00482245  2bdd                  sub       ebx,ebp
  00482247  8b6c2434              mov       ebp,[esp+34h]
  0048224b  894624                mov       [esi+24h],eax
  0048224e  2bc5                  sub       eax,ebp
  00482250  c1ff0c                sar       edi,0Ch
  00482253  c1f80c                sar       eax,0Ch
  00482256  8be8                  mov       ebp,eax
  00482258  8bd7                  mov       edx,edi
  0048225a  0fafd7                imul      edx,edi
  0048225d  0fafc5                imul      eax,ebp
  00482260  83c410                add       esp,10h
  00482263  03d0                  add       edx,eax
  00482265  c1fb0c                sar       ebx,0Ch
  00482268  52                    push      edx
  00482269  e8a2370c00            call      00545A10h
  0048226e  55                    push      ebp
  0048226f  57                    push      edi
  00482270  89442434              mov       [esp+34h],eax
  00482274  e8a9370c00            call      00545A22h
  00482279  668b4e32              mov       cx,[esi+32h]
  0048227d  668b6e34              mov       bp,[esi+34h]
  00482281  ba00040000            mov       edx,400h
  00482286  8bf8                  mov       edi,eax
  00482288  8b44242c              mov       eax,[esp+2Ch]
  0048228c  2bd1                  sub       edx,ecx
  0048228e  03fa                  add       edi,edx
  00482290  50                    push      eax
  00482291  53                    push      ebx
  00482292  81e7ff0f0000          and       edi,0FFFh
  00482298  e885370c00            call      00545A22h
  0048229d  2bc5                  sub       eax,ebp
  0048229f  25ff0f0000            and       eax,0FFFh
  004822a4  81ff00080000          cmp       edi,800h
  004822aa  7d07                  jge       short 004822B3h
  004822ac  c1ff02                sar       edi,2
  004822af  8bcf                  mov       ecx,edi
  004822b1  eb0c                  jmp       short 004822BFh

LAB_004822b3:
  004822b3  b900100000            mov       ecx,1000h
  004822b8  2bcf                  sub       ecx,edi
  004822ba  c1f902                sar       ecx,2
  004822bd  f7d9                  neg       ecx

LAB_004822bf:
  004822bf  66014e32              add       [esi+32h],cx
  004822c3  3d00080000            cmp       eax,800h
  004822c8  7d07                  jge       short 004822D1h
  004822ca  c1f802                sar       eax,2
  004822cd  8bc8                  mov       ecx,eax
  004822cf  eb0c                  jmp       short 004822DDh

LAB_004822d1:
  004822d1  b900100000            mov       ecx,1000h
  004822d6  2bc8                  sub       ecx,eax
  004822d8  c1f902                sar       ecx,2
  004822db  f7d9                  neg       ecx

LAB_004822dd:
  004822dd  8b06                  mov       eax,[esi]
  004822df  03e9                  add       ebp,ecx
  004822e1  8b4e70                mov       ecx,[esi+70h]
  004822e4  66896e34              mov       [esi+34h],bp
  004822e8  5b                    pop       ebx
  004822e9  5d                    pop       ebp
  004822ea  8b11                  mov       edx,[ecx]
  004822ec  5f                    pop       edi
  004822ed  81e200000020          and       edx,20000000h
  004822f3  0bc2                  or        eax,edx
  004822f5  8906                  mov       [esi],eax
  004822f7  5e                    pop       esi
  004822f8  83c418                add       esp,18h
  004822fb  c3                    ret

7 cible(s) de saut interne : 0x004821bb, 0x004821e3, 0x004821e6, 0x004822b3, 0x004822bf, 0x004822d1, 0x004822dd
