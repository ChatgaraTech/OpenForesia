Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004834d0  section .text  offset fichier 0x828d0  374 octets

  004834d0  83ec0c                sub       esp,0Ch
  004834d3  53                    push      ebx
  004834d4  55                    push      ebp
  004834d5  56                    push      esi
  004834d6  8b74241c              mov       esi,[esp+1Ch]
  004834da  33c0                  xor       eax,eax
  004834dc  b928000000            mov       ecx,28h
  004834e1  66394610              cmp       [esi+10h],ax
  004834e5  57                    push      edi
  004834e6  6689442410            mov       [esp+10h],ax
  004834eb  66c7442412d8ff        mov       word [esp+12h],0FFD8h
  004834f2  66894c2414            mov       [esp+14h],cx
  004834f7  66894c2416            mov       [esp+16h],cx
  004834fc  6689442418            mov       [esp+18h],ax
  00483501  66c744241a5000        mov       word [esp+1Ah],50h
  00483508  7512                  jne       short 0048351Ch
  0048350a  8b06                  mov       eax,[esi]
  0048350c  66c746100100          mov       word [esi+10h],1
  00483512  80e4f7                and       ah,0F7h
  00483515  0d80060000            or        eax,680h
  0048351a  8906                  mov       [esi],eax

LAB_0048351c:
  0048351c  8b4650                mov       eax,[esi+50h]
  0048351f  8b4e1c                mov       ecx,[esi+1Ch]
  00483522  8d7e1c                lea       edi,[esi+1Ch]
  00483525  8b5e24                mov       ebx,[esi+24h]
  00483528  8b5654                mov       edx,[esi+54h]
  0048352b  8b6e20                mov       ebp,[esi+20h]
  0048352e  03c8                  add       ecx,eax
  00483530  03ea                  add       ebp,edx
  00483532  890f                  mov       [edi],ecx
  00483534  8b4e58                mov       ecx,[esi+58h]
  00483537  03d9                  add       ebx,ecx
  00483539  896e20                mov       [esi+20h],ebp
  0048353c  895e24                mov       [esi+24h],ebx
  0048353f  8bd8                  mov       ebx,eax
  00483541  c1fb0c                sar       ebx,0Ch
  00483544  8be8                  mov       ebp,eax
  00483546  56                    push      esi
  00483547  2beb                  sub       ebp,ebx
  00483549  8bda                  mov       ebx,edx
  0048354b  c1f803                sar       eax,3
  0048354e  2be8                  sub       ebp,eax
  00483550  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00483557  896e50                mov       [esi+50h],ebp
  0048355a  8be8                  mov       ebp,eax
  0048355c  c1fb0c                sar       ebx,0Ch
  0048355f  c1fd08                sar       ebp,8
  00483562  2beb                  sub       ebp,ebx
  00483564  8b5e20                mov       ebx,[esi+20h]
  00483567  03ea                  add       ebp,edx
  00483569  8bd1                  mov       edx,ecx
  0048356b  2be8                  sub       ebp,eax
  0048356d  8bc1                  mov       eax,ecx
  0048356f  c1fa0c                sar       edx,0Ch
  00483572  2bc2                  sub       eax,edx
  00483574  896e54                mov       [esi+54h],ebp
  00483577  c1f903                sar       ecx,3
  0048357a  2bc1                  sub       eax,ecx
  0048357c  894658                mov       [esi+58h],eax
  0048357f  e8bc030b00            call      00533940h
  00483584  8b8690000000          mov       eax,[esi+90h]
  0048358a  83c404                add       esp,4
  0048358d  f6c4a0                test      ah,0A0h
  00483590  895e20                mov       [esi+20h],ebx
  00483593  7534                  jne       short 004835C9h
  00483595  2502080000            and       eax,802h
  0048359a  7505                  jne       short 004835A1h
  0048359c  3b5e60                cmp       ebx,[esi+60h]
  0048359f  7f28                  jg        short 004835C9h

LAB_004835a1:
  004835a1  8b0e                  mov       ecx,[esi]
  004835a3  f6c110                test      cl,10h
  004835a6  0f858c000000          jne       near 00483638h
  004835ac  85c0                  test      eax,eax
  004835ae  0f8584000000          jne       near 00483638h
  004835b4  81c910200000          or        ecx,2010h
  004835ba  c7465400a00000        mov       dword [esi+54h],0A000h
  004835c1  890e                  mov       [esi],ecx
  004835c3  8b4e60                mov       ecx,[esi+60h]
  004835c6  894e20                mov       [esi+20h],ecx

LAB_004835c9:
  004835c9  f70600000060          test      dword [esi],60000000h
  004835cf  7567                  jne       short 00483638h
  004835d1  8d542410              lea       edx,[esp+10h]
  004835d5  52                    push      edx
  004835d6  56                    push      esi
  004835d7  e89416ffff            call      00474C70h
  004835dc  8bd8                  mov       ebx,eax
  004835de  83c408                add       esp,8
  004835e1  85db                  test      ebx,ebx
  004835e3  7459                  je        short 0048363Eh
  004835e5  a1b8e05d00            mov       eax,[5DE0B8h]
  004835ea  85c0                  test      eax,eax
  004835ec  744a                  je        short 00483638h
  004835ee  8b4674                mov       eax,[esi+74h]
  004835f1  53                    push      ebx
  004835f2  50                    push      eax
  004835f3  e888650b00            call      00539B80h
  004835f8  8b13                  mov       edx,[ebx]
  004835fa  83c408                add       esp,8
  004835fd  83ca40                or        edx,40h
  00483600  66c783960000001000    mov       word [ebx+96h],10h
  00483609  57                    push      edi
  0048360a  6800180000            push      1800h
  0048360f  6800040000            push      400h
  00483614  6a03                  push      3
  00483616  8913                  mov       [ebx],edx
  00483618  e803a50b00            call      0053DB20h
  0048361d  83c410                add       esp,10h
  00483620  6a04                  push      4
  00483622  6a00                  push      0
  00483624  68bd000000            push      0BDh
  00483629  68081a5500            push      551A08h
  0048362e  57                    push      edi
  0048362f  56                    push      esi
  00483630  e80b76ffff            call      0047AC40h
  00483635  83c418                add       esp,18h

LAB_00483638:
  00483638  c70600000000          mov       dword [esi],0

LAB_0048363e:
  0048363e  5f                    pop       edi
  0048363f  5e                    pop       esi
  00483640  5d                    pop       ebp
  00483641  5b                    pop       ebx
  00483642  83c40c                add       esp,0Ch
  00483645  c3                    ret

5 cible(s) de saut interne : 0x0048351c, 0x004835a1, 0x004835c9, 0x00483638, 0x0048363e
