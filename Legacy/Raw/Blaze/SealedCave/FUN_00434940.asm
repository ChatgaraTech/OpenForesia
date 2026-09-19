Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00434940  section .text  offset fichier 0x33d40  336 octets

  00434940  53                    push      ebx
  00434941  56                    push      esi
  00434942  8b74240c              mov       esi,[esp+0Ch]
  00434946  66837e1000            cmp       word [esi+10h],0
  0043494b  0f85fb000000          jne       near 00434A4Ch
  00434951  b8c8000000            mov       eax,0C8h
  00434956  c6460400              mov       byte [esi+4],0
  0043495a  c6460a03              mov       byte [esi+0Ah],3
  0043495e  c6460b01              mov       byte [esi+0Bh],1
  00434962  66894628              mov       [esi+28h],ax
  00434966  6689462a              mov       [esi+2Ah],ax
  0043496a  e8d1271100            call      00547140h
  0043496f  99                    cdq
  00434970  b9b80b0000            mov       ecx,0BB8h
  00434975  bbb4000000            mov       ebx,0B4h
  0043497a  f7f9                  idiv      ecx
  0043497c  885e38                mov       [esi+38h],bl
  0043497f  885e39                mov       [esi+39h],bl
  00434982  c6463afa              mov       byte [esi+3Ah],0FAh
  00434986  81c2a00f0000          add       edx,0FA0h
  0043498c  6689562c              mov       [esi+2Ch],dx
  00434990  a002975e00            mov       al,[5E9702h]
  00434995  a804                  test      al,4
  00434997  c7461c00c0f9ff        mov       dword [esi+1Ch],0FFF9C000h
  0043499e  7456                  je        short 004349F6h
  004349a0  e89b271100            call      00547140h
  004349a5  99                    cdq
  004349a6  f7fb                  idiv      ebx
  004349a8  c1e20c                shl       edx,0Ch
  004349ab  895620                mov       [esi+20h],edx
  004349ae  e88d271100            call      00547140h
  004349b3  99                    cdq
  004349b4  b958020000            mov       ecx,258h
  004349b9  f7f9                  idiv      ecx
  004349bb  b848f4ffff            mov       eax,0FFFFF448h
  004349c0  2bc2                  sub       eax,edx
  004349c2  c1e00c                shl       eax,0Ch
  004349c5  894624                mov       [esi+24h],eax
  004349c8  e873271100            call      00547140h
  004349cd  99                    cdq
  004349ce  b932000000            mov       ecx,32h
  004349d3  66c746320004          mov       word [esi+32h],400h
  004349d9  f7f9                  idiv      ecx
  004349db  b8ecffffff            mov       eax,0FFFFFFECh
  004349e0  66c746340008          mov       word [esi+34h],800h
  004349e6  66c74612b4fb          mov       word [esi+12h],0FBB4h
  004349ec  2bc2                  sub       eax,edx
  004349ee  c1e00c                shl       eax,0Ch
  004349f1  894650                mov       [esi+50h],eax
  004349f4  eb50                  jmp       short 00434A46h

LAB_004349f6:
  004349f6  e845271100            call      00547140h
  004349fb  99                    cdq
  004349fc  f7fb                  idiv      ebx
  004349fe  c1e20c                shl       edx,0Ch
  00434a01  895620                mov       [esi+20h],edx
  00434a04  e837271100            call      00547140h
  00434a09  99                    cdq
  00434a0a  b958020000            mov       ecx,258h
  00434a0f  f7f9                  idiv      ecx
  00434a11  b848f4ffff            mov       eax,0FFFFF448h
  00434a16  2bc2                  sub       eax,edx
  00434a18  c1e00c                shl       eax,0Ch
  00434a1b  894624                mov       [esi+24h],eax
  00434a1e  e81d271100            call      00547140h
  00434a23  99                    cdq
  00434a24  b932000000            mov       ecx,32h
  00434a29  66c74632000c          mov       word [esi+32h],0C00h
  00434a2f  f7f9                  idiv      ecx
  00434a31  66c746340008          mov       word [esi+34h],800h
  00434a37  66c746122003          mov       word [esi+12h],320h
  00434a3d  83c214                add       edx,14h
  00434a40  c1e20c                shl       edx,0Ch
  00434a43  895650                mov       [esi+50h],edx

LAB_00434a46:
  00434a46  66c746100100          mov       word [esi+10h],1

LAB_00434a4c:
  00434a4c  66837e1001            cmp       word [esi+10h],1
  00434a51  7539                  jne       short 00434A8Ch
  00434a53  8b5650                mov       edx,[esi+50h]
  00434a56  8b5e1c                mov       ebx,[esi+1Ch]
  00434a59  668b4612              mov       ax,[esi+12h]
  00434a5d  03da                  add       ebx,edx
  00434a5f  663db4fb              cmp       ax,0FBB4h
  00434a63  895e1c                mov       [esi+1Ch],ebx
  00434a66  8bcb                  mov       ecx,ebx
  00434a68  750e                  jne       short 00434A78h
  00434a6a  81f90040bbff          cmp       ecx,0FFBB4000h
  00434a70  7d06                  jge       short 00434A78h
  00434a72  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00434a78:
  00434a78  663d2003              cmp       ax,320h
  00434a7c  750e                  jne       short 00434A8Ch
  00434a7e  81f900003200          cmp       ecx,320000h
  00434a84  7e06                  jle       short 00434A8Ch
  00434a86  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00434a8c:
  00434a8c  5e                    pop       esi
  00434a8d  5b                    pop       ebx
  00434a8e  c3                    ret
  00434a8f  90                    nop

5 cible(s) de saut interne : 0x004349f6, 0x00434a46, 0x00434a4c, 0x00434a78, 0x00434a8c
