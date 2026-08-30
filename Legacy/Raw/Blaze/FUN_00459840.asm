; 0x00459840 — le debris qui retombe en clignotant (158 instructions, absente de Blaze.c). Table de sauts a 0x00459a40, indices a 0x00459a58.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00459840  section .text  offset fichier 0x58c40  512 octets

  00459840  a0fb965e00            mov       al,[5E96FBh]
  00459845  56                    push      esi
  00459846  a801                  test      al,1
  00459848  57                    push      edi
  00459849  0f84ed010000          je        near 00459A3Ch
  0045984f  8b74240c              mov       esi,[esp+0Ch]
  00459853  66837e1000            cmp       word [esi+10h],0
  00459858  0f859d000000          jne       near 004598FBh
  0045985e  66c746100100          mov       word [esi+10h],1
  00459864  e8d7d80e00            call      00547140h
  00459869  99                    cdq
  0045986a  33c2                  xor       eax,edx
  0045986c  2bc2                  sub       eax,edx
  0045986e  83e00f                and       eax,0Fh
  00459871  33c2                  xor       eax,edx
  00459873  2bc2                  sub       eax,edx
  00459875  83e808                sub       eax,8
  00459878  c1e00c                shl       eax,0Ch
  0045987b  894650                mov       [esi+50h],eax
  0045987e  e8bdd80e00            call      00547140h
  00459883  99                    cdq
  00459884  33c2                  xor       eax,edx
  00459886  2bc2                  sub       eax,edx
  00459888  83e00f                and       eax,0Fh
  0045988b  33c2                  xor       eax,edx
  0045988d  2bc2                  sub       eax,edx
  0045988f  83e808                sub       eax,8
  00459892  c1e00c                shl       eax,0Ch
  00459895  894658                mov       [esi+58h],eax
  00459898  e8a3d80e00            call      00547140h
  0045989d  99                    cdq
  0045989e  b90f000000            mov       ecx,0Fh
  004598a3  f7f9                  idiv      ecx
  004598a5  83c205                add       edx,5
  004598a8  c1e20c                shl       edx,0Ch
  004598ab  895654                mov       [esi+54h],edx
  004598ae  e88dd80e00            call      00547140h
  004598b3  99                    cdq
  004598b4  b91e000000            mov       ecx,1Eh
  004598b9  f7f9                  idiv      ecx
  004598bb  66895612              mov       [esi+12h],dx
  004598bf  e87cd80e00            call      00547140h
  004598c4  99                    cdq
  004598c5  b91e000000            mov       ecx,1Eh
  004598ca  f7f9                  idiv      ecx
  004598cc  66895614              mov       [esi+14h],dx
  004598d0  e86bd80e00            call      00547140h
  004598d5  99                    cdq
  004598d6  b90a000000            mov       ecx,0Ah
  004598db  c7465c00000000        mov       dword [esi+5Ch],0
  004598e2  f7f9                  idiv      ecx
  004598e4  8b06                  mov       eax,[esi]
  004598e6  66c786980000000000    mov       word [esi+98h],0
  004598ef  83c214                add       edx,14h
  004598f2  80cc20                or        ah,20h
  004598f5  66895616              mov       [esi+16h],dx
  004598f9  8906                  mov       [esi],eax

LAB_004598fb:
  004598fb  66837e1001            cmp       word [esi+10h],1
  00459900  0f8536010000          jne       near 00459A3Ch
  00459906  6683be9800000000      cmp       word [esi+98h],0
  0045990e  751b                  jne       short 0045992Bh
  00459910  bf10000000            mov       edi,10h

LAB_00459915:
  00459915  6a00                  push      0
  00459917  6a00                  push      0
  00459919  6aff                  push      0FFFFFFFFh
  0045991b  6a00                  push      0
  0045991d  6a31                  push      31h
  0045991f  56                    push      esi
  00459920  e8ebe70000            call      00468110h
  00459925  83c418                add       esp,18h
  00459928  4f                    dec       edi
  00459929  75ea                  jne       short 00459915h

LAB_0045992b:
  0045992b  0fbf8698000000        movsx     eax,word [esi+98h]
  00459932  83f80c                cmp       eax,0Ch
  00459935  0f8797000000          ja        near 004599D2h
  0045993b  33d2                  xor       edx,edx
  0045993d  8a90589a4500          mov       dl,[eax+459A58h]
  00459943  ff2495409a4500        jmp       dword [edx*4+459A40h]
  0045994a  6a00                  push      0
  0045994c  6a00                  push      0
  0045994e  6800180000            push      1800h
  00459953  eb38                  jmp       short 0045998Dh
  00459955  6a00                  push      0
  00459957  6a00                  push      0
  00459959  6800100000            push      1000h
  0045995e  eb50                  jmp       short 004599B0h
  00459960  8b15cc975e00          mov       edx,[5E97CCh]
  00459966  6a00                  push      0
  00459968  6a00                  push      0
  0045996a  68000b0000            push      0B00h
  0045996f  8b82a8000000          mov       eax,[edx+0A8h]
  00459975  6800040000            push      400h
  0045997a  8b08                  mov       ecx,[eax]
  0045997c  660fb6511c            movzx     dx,byte [ecx+1Ch]
  00459981  52                    push      edx
  00459982  eb45                  jmp       short 004599C9h
  00459984  6a00                  push      0
  00459986  6a00                  push      0
  00459988  6800080000            push      800h

LAB_0045998d:
  0045998d  a1cc975e00            mov       eax,[5E97CCh]
  00459992  6800040000            push      400h
  00459997  8b88a8000000          mov       ecx,[eax+0A8h]
  0045999d  8b11                  mov       edx,[ecx]
  0045999f  660fb6421c            movzx     ax,byte [edx+1Ch]
  004599a4  50                    push      eax
  004599a5  eb22                  jmp       short 004599C9h
  004599a7  6a00                  push      0
  004599a9  6a00                  push      0
  004599ab  6800040000            push      400h

LAB_004599b0:
  004599b0  8b0dcc975e00          mov       ecx,[5E97CCh]
  004599b6  6800040000            push      400h
  004599bb  8b91a8000000          mov       edx,[ecx+0A8h]
  004599c1  8b02                  mov       eax,[edx]
  004599c3  660fb6481c            movzx     cx,byte [eax+1Ch]
  004599c8  51                    push      ecx

LAB_004599c9:
  004599c9  56                    push      esi
  004599ca  e8b1e60000            call      00468080h
  004599cf  83c418                add       esp,18h

LAB_004599d2:
  004599d2  8b5650                mov       edx,[esi+50h]
  004599d5  8b461c                mov       eax,[esi+1Ch]
  004599d8  8b7e20                mov       edi,[esi+20h]
  004599db  8b4e58                mov       ecx,[esi+58h]
  004599de  03c2                  add       eax,edx
  004599e0  8b5624                mov       edx,[esi+24h]
  004599e3  89461c                mov       [esi+1Ch],eax
  004599e6  8b4654                mov       eax,[esi+54h]
  004599e9  66ff8698000000        inc       word [esi+98h]
  004599f0  03f8                  add       edi,eax
  004599f2  03d1                  add       edx,ecx
  004599f4  0500f0ffff            add       eax,0FFFFF000h
  004599f9  0fbf4e16              movsx     ecx,word [esi+16h]
  004599fd  895624                mov       [esi+24h],edx
  00459a00  668b5612              mov       dx,[esi+12h]
  00459a04  66015632              add       [esi+32h],dx
  00459a08  894654                mov       [esi+54h],eax
  00459a0b  668b4614              mov       ax,[esi+14h]
  00459a0f  897e20                mov       [esi+20h],edi
  00459a12  8b7e5c                mov       edi,[esi+5Ch]
  00459a15  66014634              add       [esi+34h],ax
  00459a19  47                    inc       edi
  00459a1a  8bc7                  mov       eax,edi
  00459a1c  897e5c                mov       [esi+5Ch],edi
  00459a1f  3bc1                  cmp       eax,ecx
  00459a21  7e06                  jle       short 00459A29h
  00459a23  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00459a29:
  00459a29  a801                  test      al,1
  00459a2b  8b06                  mov       eax,[esi]
  00459a2d  7508                  jne       short 00459A37h
  00459a2f  80cc40                or        ah,40h
  00459a32  8906                  mov       [esi],eax
  00459a34  5f                    pop       edi
  00459a35  5e                    pop       esi
  00459a36  c3                    ret

LAB_00459a37:
  00459a37  80e4bf                and       ah,0BFh
  00459a3a  8906                  mov       [esi],eax

LAB_00459a3c:
  00459a3c  5f                    pop       edi
  00459a3d  5e                    pop       esi
  00459a3e  c3                    ret
  00459a3f  90                    nop

10 cible(s) de saut interne : 0x004598fb, 0x00459915, 0x0045992b, 0x0045998d, 0x004599b0, 0x004599c9, 0x004599d2, 0x00459a29, 0x00459a37, 0x00459a3c
