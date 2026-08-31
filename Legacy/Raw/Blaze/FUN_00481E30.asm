Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00481e30  section .text  offset fichier 0x81230  876 octets

  00481e30  83ec18                sub       esp,18h
  00481e33  53                    push      ebx
  00481e34  55                    push      ebp
  00481e35  56                    push      esi
  00481e36  8b742428              mov       esi,[esp+28h]
  00481e3a  57                    push      edi
  00481e3b  33ff                  xor       edi,edi
  00481e3d  8b06                  mov       eax,[esi]
  00481e3f  66c744241000fc        mov       word [esp+10h],0FC00h
  00481e46  a900000020            test      eax,20000000h
  00481e4b  66c744241200ff        mov       word [esp+12h],0FF00h
  00481e52  66c74424146400        mov       word [esp+14h],64h
  00481e59  66c74424169600        mov       word [esp+16h],96h
  00481e60  66897c2418            mov       [esp+18h],di
  00481e65  66c744241a0001        mov       word [esp+1Ah],100h
  00481e6c  740a                  je        short 00481E78h
  00481e6e  893e                  mov       [esi],edi
  00481e70  5f                    pop       edi
  00481e71  5e                    pop       esi
  00481e72  5d                    pop       ebp
  00481e73  5b                    pop       ebx
  00481e74  83c418                add       esp,18h
  00481e77  c3                    ret

LAB_00481e78:
  00481e78  a900000002            test      eax,2000000h
  00481e7d  7437                  je        short 00481EB6h
  00481e7f  668b4e10              mov       cx,[esi+10h]
  00481e83  6683f91e              cmp       cx,1Eh
  00481e87  7d0c                  jge       short 00481E95h
  00481e89  bd90010000            mov       ebp,190h
  00481e8e  33db                  xor       ebx,ebx
  00481e90  e93d010000            jmp       00481FD2h

LAB_00481e95:
  00481e95  6683f964              cmp       cx,64h
  00481e99  7d0c                  jge       short 00481EA7h
  00481e9b  bd70feffff            mov       ebp,0FFFFFE70h
  00481ea0  33db                  xor       ebx,ebx
  00481ea2  e92b010000            jmp       00481FD2h

LAB_00481ea7:
  00481ea7  0d00000020            or        eax,20000000h
  00481eac  8906                  mov       [esi],eax
  00481eae  5f                    pop       edi
  00481eaf  5e                    pop       esi
  00481eb0  5d                    pop       ebp
  00481eb1  5b                    pop       ebx
  00481eb2  83c418                add       esp,18h
  00481eb5  c3                    ret

LAB_00481eb6:
  00481eb6  8b5e78                mov       ebx,[esi+78h]
  00481eb9  3bdf                  cmp       ebx,edi
  00481ebb  7412                  je        short 00481ECFh
  00481ebd  f70300000050          test      dword [ebx],50000000h
  00481ec3  741c                  je        short 00481EE1h
  00481ec5  897b74                mov       [ebx+74h],edi
  00481ec8  897e78                mov       [esi+78h],edi
  00481ecb  33db                  xor       ebx,ebx
  00481ecd  eb12                  jmp       short 00481EE1h

LAB_00481ecf:
  00481ecf  8b4674                mov       eax,[esi+74h]
  00481ed2  6a01                  push      1
  00481ed4  6aff                  push      0FFFFFFFFh
  00481ed6  50                    push      eax
  00481ed7  e8d42fffff            call      00474EB0h
  00481edc  83c40c                add       esp,0Ch
  00481edf  8bd8                  mov       ebx,eax

LAB_00481ee1:
  00481ee1  668b4610              mov       ax,[esi+10h]
  00481ee5  663d1e00              cmp       ax,1Eh
  00481ee9  7d0c                  jge       short 00481EF7h
  00481eeb  bd90010000            mov       ebp,190h
  00481ef0  33db                  xor       ebx,ebx
  00481ef2  e9bc000000            jmp       00481FB3h

LAB_00481ef7:
  00481ef7  3bdf                  cmp       ebx,edi
  00481ef9  743c                  je        short 00481F37h
  00481efb  895e78                mov       [esi+78h],ebx
  00481efe  8b7b1c                mov       edi,[ebx+1Ch]
  00481f01  897374                mov       [ebx+74h],esi
  00481f04  8b4e1c                mov       ecx,[esi+1Ch]
  00481f07  2bf9                  sub       edi,ecx
  00481f09  53                    push      ebx
  00481f0a  c1ff0c                sar       edi,0Ch
  00481f0d  e8ae6c0b00            call      00538BC0h
  00481f12  8b4b20                mov       ecx,[ebx+20h]
  00481f15  8b5620                mov       edx,[esi+20h]
  00481f18  8b5b24                mov       ebx,[ebx+24h]
  00481f1b  8be8                  mov       ebp,eax
  00481f1d  83e5fe                and       ebp,0FFFFFFFEh
  00481f20  2bca                  sub       ecx,edx
  00481f22  c1e50b                shl       ebp,0Bh
  00481f25  03e9                  add       ebp,ecx
  00481f27  8b4e24                mov       ecx,[esi+24h]
  00481f2a  2bd9                  sub       ebx,ecx
  00481f2c  83c404                add       esp,4
  00481f2f  c1fd0c                sar       ebp,0Ch
  00481f32  c1fb0c                sar       ebx,0Ch
  00481f35  eb7c                  jmp       short 00481FB3h

LAB_00481f37:
  00481f37  8b7e74                mov       edi,[esi+74h]
  00481f3a  a81f                  test      al,1Fh
  00481f3c  7538                  jne       short 00481F76h
  00481f3e  e8fd510c00            call      00547140h
  00481f43  8b571c                mov       edx,[edi+1Ch]
  00481f46  8b5e1c                mov       ebx,[esi+1Ch]
  00481f49  2bd3                  sub       edx,ebx
  00481f4b  25ff000000            and       eax,0FFh
  00481f50  c1fa0c                sar       edx,0Ch
  00481f53  8d441080              lea       eax,[eax+edx-80h]
  00481f57  894644                mov       [esi+44h],eax
  00481f5a  e8e1510c00            call      00547140h
  00481f5f  8b4f24                mov       ecx,[edi+24h]
  00481f62  8b5e24                mov       ebx,[esi+24h]
  00481f65  2bcb                  sub       ecx,ebx
  00481f67  25ff000000            and       eax,0FFh
  00481f6c  c1f90c                sar       ecx,0Ch
  00481f6f  8d540880              lea       edx,[eax+ecx-80h]
  00481f73  89564c                mov       [esi+4Ch],edx

LAB_00481f76:
  00481f76  8b4720                mov       eax,[edi+20h]
  00481f79  8b7e20                mov       edi,[esi+20h]
  00481f7c  668146128000          add       word [esi+12h],80h
  00481f82  2bc7                  sub       eax,edi
  00481f84  c1f80c                sar       eax,0Ch
  00481f87  0590010000            add       eax,190h
  00481f8c  8b7e44                mov       edi,[esi+44h]
  00481f8f  894648                mov       [esi+48h],eax
  00481f92  668b4612              mov       ax,[esi+12h]
  00481f96  0fbfc8                movsx     ecx,ax
  00481f99  51                    push      ecx
  00481f9a  e87d3a0c00            call      00545A1Ch
  00481f9f  8d0480                lea       eax,[eax+eax*4]
  00481fa2  8b5e4c                mov       ebx,[esi+4Ch]
  00481fa5  8d2c80                lea       ebp,[eax+eax*4]
  00481fa8  8b4648                mov       eax,[esi+48h]
  00481fab  c1e504                shl       ebp,4
  00481fae  c1fd0c                sar       ebp,0Ch
  00481fb1  03e8                  add       ebp,eax

LAB_00481fb3:
  00481fb3  8b566c                mov       edx,[esi+6Ch]
  00481fb6  8b0e                  mov       ecx,[esi]
  00481fb8  8b02                  mov       eax,[edx]
  00481fba  2500000002            and       eax,2000000h
  00481fbf  0bc8                  or        ecx,eax
  00481fc1  8bc1                  mov       eax,ecx
  00481fc3  890e                  mov       [esi],ecx
  00481fc5  a900000002            test      eax,2000000h
  00481fca  7406                  je        short 00481FD2h
  00481fcc  66c746100000          mov       word [esi+10h],0

LAB_00481fd2:
  00481fd2  8bcf                  mov       ecx,edi
  00481fd4  8bd3                  mov       edx,ebx
  00481fd6  0fafcf                imul      ecx,edi
  00481fd9  0fafd3                imul      edx,ebx
  00481fdc  03ca                  add       ecx,edx
  00481fde  51                    push      ecx
  00481fdf  e82c3a0c00            call      00545A10h
  00481fe4  53                    push      ebx
  00481fe5  57                    push      edi
  00481fe6  89442434              mov       [esp+34h],eax
  00481fea  e8333a0c00            call      00545A22h
  00481fef  8bf8                  mov       edi,eax
  00481ff1  8b44242c              mov       eax,[esp+2Ch]
  00481ff5  50                    push      eax
  00481ff6  55                    push      ebp
  00481ff7  e8263a0c00            call      00545A22h
  00481ffc  8bd8                  mov       ebx,eax
  00481ffe  6a10                  push      10h
  00482000  53                    push      ebx
  00482001  8d4c2424              lea       ecx,[esp+24h]
  00482005  57                    push      edi
  00482006  51                    push      ecx
  00482007  e8c40a0b00            call      00532AD0h
  0048200c  8b54242c              mov       edx,[esp+2Ch]
  00482010  8b6e50                mov       ebp,[esi+50h]
  00482013  8b442430              mov       eax,[esp+30h]
  00482017  03ea                  add       ebp,edx
  00482019  8b5654                mov       edx,[esi+54h]
  0048201c  896e50                mov       [esi+50h],ebp
  0048201f  03d0                  add       edx,eax
  00482021  668b4614              mov       ax,[esi+14h]
  00482025  8bcd                  mov       ecx,ebp
  00482027  8b6e58                mov       ebp,[esi+58h]
  0048202a  895654                mov       [esi+54h],edx
  0048202d  8b542434              mov       edx,[esp+34h]
  00482031  2bf8                  sub       edi,eax
  00482033  03ea                  add       ebp,edx
  00482035  81e7ff0f0000          and       edi,0FFFh
  0048203b  896e58                mov       [esi+58h],ebp
  0048203e  668b6e16              mov       bp,[esi+16h]
  00482042  83c410                add       esp,10h
  00482045  8d9700010000          lea       edx,[edi+100h]
  0048204b  2bdd                  sub       ebx,ebp
  0048204d  81e2ff0f0000          and       edx,0FFFh
  00482053  81e3ff0f0000          and       ebx,0FFFh
  00482059  81fa00020000          cmp       edx,200h
  0048205f  7e1e                  jle       short 0048207Fh
  00482061  33d2                  xor       edx,edx
  00482063  81ff00080000          cmp       edi,800h
  00482069  0f9dc2                setge     dl
  0048206c  4a                    dec       edx
  0048206d  81e200020000          and       edx,200h
  00482073  81c200ffffff          add       edx,0FFFFFF00h
  00482079  03d0                  add       edx,eax
  0048207b  66895614              mov       [esi+14h],dx

LAB_0048207f:
  0048207f  8d8300010000          lea       eax,[ebx+100h]
  00482085  25ff0f0000            and       eax,0FFFh
  0048208a  3d00020000            cmp       eax,200h
  0048208f  7e1e                  jle       short 004820AFh
  00482091  33d2                  xor       edx,edx
  00482093  81fb00080000          cmp       ebx,800h
  00482099  0f9dc2                setge     dl
  0048209c  4a                    dec       edx
  0048209d  81e200020000          and       edx,200h
  004820a3  81c200ffffff          add       edx,0FFFFFF00h
  004820a9  03d5                  add       edx,ebp
  004820ab  66895616              mov       [esi+16h],dx

LAB_004820af:
  004820af  668b6e32              mov       bp,[esi+32h]
  004820b3  668b4614              mov       ax,[esi+14h]
  004820b7  668b5634              mov       dx,[esi+34h]
  004820bb  668b7e16              mov       di,[esi+16h]
  004820bf  2bc5                  sub       eax,ebp
  004820c1  2bfa                  sub       edi,edx
  004820c3  0500040000            add       eax,400h
  004820c8  81e7ff0f0000          and       edi,0FFFh
  004820ce  25ff0f0000            and       eax,0FFFh
  004820d3  3d00080000            cmp       eax,800h
  004820d8  7d07                  jge       short 004820E1h
  004820da  c1f802                sar       eax,2
  004820dd  8bd8                  mov       ebx,eax
  004820df  eb0c                  jmp       short 004820EDh

LAB_004820e1:
  004820e1  bb00100000            mov       ebx,1000h
  004820e6  2bd8                  sub       ebx,eax
  004820e8  c1fb02                sar       ebx,2
  004820eb  f7db                  neg       ebx

LAB_004820ed:
  004820ed  03eb                  add       ebp,ebx
  004820ef  81ff00080000          cmp       edi,800h
  004820f5  66896e32              mov       [esi+32h],bp
  004820f9  7d07                  jge       short 00482102h
  004820fb  c1ff02                sar       edi,2
  004820fe  8bc7                  mov       eax,edi
  00482100  eb0c                  jmp       short 0048210Eh

LAB_00482102:
  00482102  b800100000            mov       eax,1000h
  00482107  2bc7                  sub       eax,edi
  00482109  c1f802                sar       eax,2
  0048210c  f7d8                  neg       eax

LAB_0048210e:
  0048210e  03d0                  add       edx,eax
  00482110  8bc1                  mov       eax,ecx
  00482112  66895634              mov       [esi+34h],dx
  00482116  8bd1                  mov       edx,ecx
  00482118  8b4e54                mov       ecx,[esi+54h]
  0048211b  8b6e20                mov       ebp,[esi+20h]
  0048211e  c1fa02                sar       edx,2
  00482121  2bc2                  sub       eax,edx
  00482123  8bd1                  mov       edx,ecx
  00482125  c1fa02                sar       edx,2
  00482128  2bca                  sub       ecx,edx
  0048212a  8b5658                mov       edx,[esi+58h]
  0048212d  8bfa                  mov       edi,edx
  0048212f  8b5e24                mov       ebx,[esi+24h]
  00482132  c1ff02                sar       edi,2
  00482135  2bd7                  sub       edx,edi
  00482137  8b7e1c                mov       edi,[esi+1Ch]
  0048213a  894650                mov       [esi+50h],eax
  0048213d  03f8                  add       edi,eax
  0048213f  8d442410              lea       eax,[esp+10h]
  00482143  03e9                  add       ebp,ecx
  00482145  03da                  add       ebx,edx
  00482147  50                    push      eax
  00482148  56                    push      esi
  00482149  894e54                mov       [esi+54h],ecx
  0048214c  895658                mov       [esi+58h],edx
  0048214f  897e1c                mov       [esi+1Ch],edi
  00482152  896e20                mov       [esi+20h],ebp
  00482155  895e24                mov       [esi+24h],ebx
  00482158  e8432bffff            call      00474CA0h
  0048215d  8bf8                  mov       edi,eax
  0048215f  83c408                add       esp,8
  00482162  85ff                  test      edi,edi
  00482164  742a                  je        short 00482190h
  00482166  8b4674                mov       eax,[esi+74h]
  00482169  33c9                  xor       ecx,ecx
  0048216b  8a4e06                mov       cl,[esi+6]
  0048216e  33d2                  xor       edx,edx
  00482170  8a5607                mov       dl,[esi+7]
  00482173  6a01                  push      1
  00482175  51                    push      ecx
  00482176  52                    push      edx
  00482177  57                    push      edi
  00482178  50                    push      eax
  00482179  e832810b00            call      0053A2B0h
  0048217e  8b07                  mov       eax,[edi]
  00482180  83c414                add       esp,14h
  00482183  0c40                  or        al,40h
  00482185  66c787960000001000    mov       word [edi+96h],10h
  0048218e  8907                  mov       [edi],eax

LAB_00482190:
  00482190  66ff4610              inc       word [esi+10h]
  00482194  5f                    pop       edi
  00482195  5e                    pop       esi
  00482196  5d                    pop       ebp
  00482197  5b                    pop       ebx
  00482198  83c418                add       esp,18h
  0048219b  c3                    ret

18 cible(s) de saut interne : 0x00481e78, 0x00481e95, 0x00481ea7, 0x00481eb6, 0x00481ecf, 0x00481ee1, 0x00481ef7, 0x00481f37, 0x00481f76, 0x00481fb3, 0x00481fd2, 0x0048207f, 0x004820af, 0x004820e1, 0x004820ed, 0x00482102, 0x0048210e, 0x00482190
