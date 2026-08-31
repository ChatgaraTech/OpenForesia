C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00480490  section .text  offset fichier 0x7f890  362 octets

  00480490  83ec0c                sub       esp,0Ch
  00480493  53                    push      ebx
  00480494  55                    push      ebp
  00480495  56                    push      esi
  00480496  8b74241c              mov       esi,[esp+1Ch]
  0048049a  57                    push      edi
  0048049b  8b06                  mov       eax,[esi]
  0048049d  a900000002            test      eax,2000000h
  004804a2  7431                  je        short 004804D5h
  004804a4  668b4628              mov       ax,[esi+28h]
  004804a8  668bc8                mov       cx,ax
  004804ab  66c1f902              sar       cx,2
  004804af  2bc1                  sub       eax,ecx
  004804b1  663d0001              cmp       ax,100h
  004804b5  66894628              mov       [esi+28h],ax
  004804b9  6689462a              mov       [esi+2Ah],ax
  004804bd  6689462c              mov       [esi+2Ch],ax
  004804c1  0f8d2b010000          jge       near 004805F2h
  004804c7  c70600000000          mov       dword [esi],0
  004804cd  5f                    pop       edi
  004804ce  5e                    pop       esi
  004804cf  5d                    pop       ebp
  004804d0  5b                    pop       ebx
  004804d1  83c40c                add       esp,0Ch
  004804d4  c3                    ret

LAB_004804d5:
  004804d5  8b566c                mov       edx,[esi+6Ch]
  004804d8  8b0a                  mov       ecx,[edx]
  004804da  81e100000002          and       ecx,2000000h
  004804e0  0bc8                  or        ecx,eax
  004804e2  890e                  mov       [esi],ecx
  004804e4  33c9                  xor       ecx,ecx
  004804e6  66394e10              cmp       [esi+10h],cx
  004804ea  752d                  jne       short 00480519h
  004804ec  33c0                  xor       eax,eax
  004804ee  66c746100100          mov       word [esi+10h],1
  004804f4  8a4616                mov       al,[esi+16h]
  004804f7  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  004804fe  894e28                mov       [esi+28h],ecx
  00480501  66894e2c              mov       [esi+2Ch],cx
  00480505  8d1480                lea       edx,[eax+eax*4]
  00480508  c1e203                shl       edx,3
  0048050b  2bd0                  sub       edx,eax
  0048050d  8d3c95e0de5e00        lea       edi,[edx*4+5EDEE0h]
  00480514  897e70                mov       [esi+70h],edi
  00480517  eb03                  jmp       short 0048051Ch

LAB_00480519:
  00480519  8b7e70                mov       edi,[esi+70h]

LAB_0048051c:
  0048051c  668b4628              mov       ax,[esi+28h]
  00480520  ba00180000            mov       edx,1800h
  00480525  0fbfc8                movsx     ecx,ax
  00480528  668b5e32              mov       bx,[esi+32h]
  0048052c  2bd1                  sub       edx,ecx
  0048052e  668b6e34              mov       bp,[esi+34h]
  00480532  c1fa02                sar       edx,2
  00480535  03d0                  add       edx,eax
  00480537  66895628              mov       [esi+28h],dx
  0048053b  6689562a              mov       [esi+2Ah],dx
  0048053f  6689562c              mov       [esi+2Ch],dx
  00480543  668b4732              mov       ax,[edi+32h]
  00480547  668b4f34              mov       cx,[edi+34h]
  0048054b  2bc3                  sub       eax,ebx
  0048054d  2bcd                  sub       ecx,ebp
  0048054f  25ff0f0000            and       eax,0FFFh
  00480554  81e1ff0f0000          and       ecx,0FFFh
  0048055a  3d00080000            cmp       eax,800h
  0048055f  7d07                  jge       short 00480568h
  00480561  c1f802                sar       eax,2
  00480564  8be8                  mov       ebp,eax
  00480566  eb0c                  jmp       short 00480574h

LAB_00480568:
  00480568  bd00100000            mov       ebp,1000h
  0048056d  2be8                  sub       ebp,eax
  0048056f  c1fd02                sar       ebp,2
  00480572  f7dd                  neg       ebp

LAB_00480574:
  00480574  03dd                  add       ebx,ebp
  00480576  81f900080000          cmp       ecx,800h
  0048057c  66895e32              mov       [esi+32h],bx
  00480580  7d07                  jge       short 00480589h
  00480582  c1f902                sar       ecx,2
  00480585  8bc1                  mov       eax,ecx
  00480587  eb0c                  jmp       short 00480595h

LAB_00480589:
  00480589  b800100000            mov       eax,1000h
  0048058e  2bc1                  sub       eax,ecx
  00480590  c1f802                sar       eax,2
  00480593  f7d8                  neg       eax

LAB_00480595:
  00480595  668b4e34              mov       cx,[esi+34h]
  00480599  03c8                  add       ecx,eax
  0048059b  0fbfc2                movsx     eax,dx
  0048059e  66894e34              mov       [esi+34h],cx
  004805a2  0fbf4f34              movsx     ecx,word [edi+34h]
  004805a6  0fbf5732              movsx     edx,word [edi+32h]
  004805aa  8d0440                lea       eax,[eax+eax*2]
  004805ad  81c200040000          add       edx,400h
  004805b3  f7d9                  neg       ecx
  004805b5  8d0480                lea       eax,[eax+eax*4]
  004805b8  c1e002                shl       eax,2
  004805bb  c1f80c                sar       eax,0Ch
  004805be  50                    push      eax
  004805bf  51                    push      ecx
  004805c0  8d442418              lea       eax,[esp+18h]
  004805c4  52                    push      edx
  004805c5  50                    push      eax
  004805c6  e805250b00            call      00532AD0h
  004805cb  8b4f1c                mov       ecx,[edi+1Ch]
  004805ce  8b6c2420              mov       ebp,[esp+20h]
  004805d2  8b5c2424              mov       ebx,[esp+24h]
  004805d6  03cd                  add       ecx,ebp
  004805d8  894e1c                mov       [esi+1Ch],ecx
  004805db  8b5720                mov       edx,[edi+20h]
  004805de  8b4c2428              mov       ecx,[esp+28h]
  004805e2  03d3                  add       edx,ebx
  004805e4  895620                mov       [esi+20h],edx
  004805e7  8b4724                mov       eax,[edi+24h]
  004805ea  83c410                add       esp,10h
  004805ed  03c1                  add       eax,ecx
  004805ef  894624                mov       [esi+24h],eax

LAB_004805f2:
  004805f2  5f                    pop       edi
  004805f3  5e                    pop       esi
  004805f4  5d                    pop       ebp
  004805f5  5b                    pop       ebx
  004805f6  83c40c                add       esp,0Ch
  004805f9  c3                    ret

8 cible(s) de saut interne : 0x004804d5, 0x00480519, 0x0048051c, 0x00480568, 0x00480574, 0x00480589, 0x00480595, 0x004805f2
