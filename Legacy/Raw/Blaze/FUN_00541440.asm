Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00541440  section .text  offset fichier 0x140840  271 octets

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

LAB_00541480:
  00541480  8b8690000000          mov       eax,[esi+90h]
  00541486  f6c402                test      ah,2
  00541489  7417                  je        short 005414A2h
  0054148b  6aff                  push      0FFFFFFFFh
  0054148d  6a18                  push      18h
  0054148f  68fe000000            push      0FEh
  00541494  8d4e1c                lea       ecx,[esi+1Ch]
  00541497  53                    push      ebx
  00541498  51                    push      ecx
  00541499  56                    push      esi
  0054149a  e8a197f3ff            call      0047AC40h
  0054149f  83c418                add       esp,18h

LAB_005414a2:
  005414a2  8b15b4876000          mov       edx,[6087B4h]
  005414a8  33c0                  xor       eax,eax
  005414aa  8a4608                mov       al,[esi+8]
  005414ad  8b0a                  mov       ecx,[edx]
  005414af  83c078                add       eax,78h
  005414b2  c1e00d                shl       eax,0Dh
  005414b5  03c1                  add       eax,ecx
  005414b7  8b4e7c                mov       ecx,[esi+7Ch]
  005414ba  6639590a              cmp       [ecx+0Ah],bx
  005414be  0f8485000000          je        near 00541549h
  005414c4  f70600000050          test      dword [esi],50000000h
  005414ca  757d                  jne       short 00541549h
  005414cc  8a9050010000          mov       dl,[eax+150h]
  005414d2  80e207                and       dl,7
  005414d5  80fa04                cmp       dl,4
  005414d8  756f                  jne       short 00541549h
  005414da  33c0                  xor       eax,eax
  005414dc  53                    push      ebx
  005414dd  668b460a              mov       ax,[esi+0Ah]
  005414e1  66895e0a              mov       [esi+0Ah],bx
  005414e5  8b0dc4975e00          mov       ecx,[5E97C4h]
  005414eb  89442418              mov       [esp+18h],eax
  005414ef  53                    push      ebx
  005414f0  6a2a                  push      2Ah
  005414f2  8b9194000000          mov       edx,[ecx+94h]
  005414f8  8d4e1c                lea       ecx,[esi+1Ch]
  005414fb  8b4224                mov       eax,[edx+24h]
  005414fe  50                    push      eax
  005414ff  51                    push      ecx
  00541500  56                    push      esi
  00541501  e83a97f3ff            call      0047AC40h
  00541506  668b54242c            mov       dx,[esp+2Ch]
  0054150b  8b467c                mov       eax,[esi+7Ch]
  0054150e  83c418                add       esp,18h
  00541511  6689560a              mov       [esi+0Ah],dx
  00541515  885c2408              mov       [esp+8],bl
  00541519  885c2409              mov       [esp+9],bl
  0054151d  885c240a              mov       [esp+0Ah],bl
  00541521  8a480a                mov       cl,[eax+0Ah]
  00541524  8d542414              lea       edx,[esp+14h]
  00541528  8d442408              lea       eax,[esp+8]
  0054152c  52                    push      edx
  0054152d  50                    push      eax
  0054152e  884c2413              mov       [esp+13h],cl
  00541532  c64424140d            mov       byte [esp+14h],0Dh
  00541537  c644241504            mov       byte [esp+15h],4
  0054153c  c6442416f7            mov       byte [esp+16h],0F7h
  00541541  e89a2afdff            call      00513FE0h
  00541546  83c408                add       esp,8

LAB_00541549:
  00541549  5e                    pop       esi
  0054154a  5b                    pop       ebx
  0054154b  83c408                add       esp,8
  0054154e  c3                    ret

3 cible(s) de saut interne : 0x00541480, 0x005414a2, 0x00541549
