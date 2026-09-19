Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004452d0  section .text  offset fichier 0x446d0  144 octets

  004452d0  53                    push      ebx
  004452d1  56                    push      esi
  004452d2  8b35b4876000          mov       esi,[6087B4h]
  004452d8  33d2                  xor       edx,edx
  004452da  b308                  mov       bl,8

LAB_004452dc:
  004452dc  0fbfc2                movsx     eax,dx
  004452df  8d0c80                lea       ecx,[eax+eax*4]
  004452e2  c1e103                shl       ecx,3
  004452e5  2bc8                  sub       ecx,eax
  004452e7  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004452ee  f7c100000080          test      ecx,80000000h
  004452f4  7416                  je        short 0044530Ch
  004452f6  f7c100000050          test      ecx,50000000h
  004452fc  750e                  jne       short 0044530Ch
  004452fe  8b0e                  mov       ecx,[esi]
  00445300  c1e00d                shl       eax,0Dh
  00445303  849c0850010f00        test      [eax+ecx+0F0150h],bl
  0044530a  750c                  jne       short 00445318h

LAB_0044530c:
  0044530c  42                    inc       edx
  0044530d  6683fa03              cmp       dx,3
  00445311  7ec9                  jle       short 004452DCh
  00445313  33c0                  xor       eax,eax
  00445315  5e                    pop       esi
  00445316  5b                    pop       ebx
  00445317  c3                    ret

LAB_00445318:
  00445318  0fbfc2                movsx     eax,dx
  0044531b  0fbf4c2410            movsx     ecx,word [esp+10h]
  00445320  8d1480                lea       edx,[eax+eax*4]
  00445323  6a00                  push      0
  00445325  c1e203                shl       edx,3
  00445328  2bd0                  sub       edx,eax
  0044532a  68b0335600            push      5633B0h
  0044532f  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00445336  8b15cc975e00          mov       edx,[5E97CCh]
  0044533c  50                    push      eax
  0044533d  8b4270                mov       eax,[edx+70h]
  00445340  8b4c8814              mov       ecx,[eax+ecx*4+14h]
  00445344  51                    push      ecx
  00445345  e806f00f00            call      00544350h
  0044534a  83c410                add       esp,10h
  0044534d  33c0                  xor       eax,eax
  0044534f  5e                    pop       esi
  00445350  5b                    pop       ebx
  00445351  c3                    ret
  00445352  90                    nop
  00445353  90                    nop
  00445354  90                    nop
  00445355  90                    nop
  00445356  90                    nop
  00445357  90                    nop
  00445358  90                    nop
  00445359  90                    nop
  0044535a  90                    nop
  0044535b  90                    nop
  0044535c  90                    nop
  0044535d  90                    nop
  0044535e  90                    nop
  0044535f  90                    nop

3 cible(s) de saut interne : 0x004452dc, 0x0044530c, 0x00445318
