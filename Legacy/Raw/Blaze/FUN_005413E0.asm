Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x005413e0  section .text  offset fichier 0x1407e0  160 octets

  005413e0  8b4c2404              mov       ecx,[esp+4]
  005413e4  33c0                  xor       eax,eax
  005413e6  56                    push      esi
  005413e7  33f6                  xor       esi,esi
  005413e9  8a4104                mov       al,[ecx+4]
  005413ec  0fbf5118              movsx     edx,word [ecx+18h]
  005413f0  8b048580965e00        mov       eax,[eax*4+5E9680h]
  005413f7  8b1490                mov       edx,[eax+edx*4]
  005413fa  668b7216              mov       si,[edx+16h]
  005413fe  d1ee                  shr       esi,1
  00541400  7434                  je        short 00541436h
  00541402  0fbf411a              movsx     eax,word [ecx+1Ah]
  00541406  33d2                  xor       edx,edx
  00541408  f7f6                  div       esi
  0054140a  85d2                  test      edx,edx
  0054140c  7405                  je        short 00541413h
  0054140e  f60101                test      byte [ecx],1
  00541411  7423                  je        short 00541436h

LAB_00541413:
  00541413  8b8190000000          mov       eax,[ecx+90h]
  00541419  f6c402                test      ah,2
  0054141c  7418                  je        short 00541436h
  0054141e  6aff                  push      0FFFFFFFFh
  00541420  6a18                  push      18h
  00541422  68fe000000            push      0FEh
  00541427  8d411c                lea       eax,[ecx+1Ch]
  0054142a  6a00                  push      0
  0054142c  50                    push      eax
  0054142d  51                    push      ecx
  0054142e  e80d98f3ff            call      0047AC40h
  00541433  83c418                add       esp,18h

LAB_00541436:
  00541436  5e                    pop       esi
  00541437  c3                    ret
  00541438  90                    nop
  00541439  90                    nop
  0054143a  90                    nop
  0054143b  90                    nop
  0054143c  90                    nop
  0054143d  90                    nop
  0054143e  90                    nop
  0054143f  90                    nop
  00541440  83ec08                sub       esp,8
  00541443  53                    push      ebx
  00541444  56                    push      esi
  00541445  8b742414              mov       esi,[esp+14h]
  00541449  33c0                  xor       eax,eax
  0054144b  bb00000000            mov       ebx,0
  00541450  8a4604                mov       al,[esi+4]
  00541453  0fbf4e18              movsx     ecx,word [esi+18h]
  00541457  8b148580965e00        mov       edx,[eax*4+5E9680h]
  0054145e  8b048a                mov       eax,[edx+ecx*4]
  00541461  33c9                  xor       ecx,ecx
  00541463  668b4816              mov       cx,[eax+16h]
  00541467  d1e9                  shr       ecx,1
  00541469  894c2414              mov       [esp+14h],ecx
  0054146d  7433                  je        short 005414A2h
  0054146f  0fbf461a              movsx     eax,word [esi+1Ah]
  00541473  33d2                  xor       edx,edx
  00541475  f7f1                  div       ecx
  00541477  85d2                  test      edx,edx
  00541479  7405                  je        short 00541480h
  0054147b  f60601                test      byte [esi],1
  0054147e  7422                  je        short 005414A2h

2 cible(s) de saut interne : 0x00541413, 0x00541436
