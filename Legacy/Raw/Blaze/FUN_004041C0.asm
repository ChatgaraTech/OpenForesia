Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004041c0  section .text  offset fichier 0x35c0  330 octets

  004041c0  83ec0c                sub       esp,0Ch
  004041c3  56                    push      esi
  004041c4  57                    push      edi
  004041c5  8b7c2418              mov       edi,[esp+18h]
  004041c9  8b776c                mov       esi,[edi+6Ch]
  004041cc  85f6                  test      esi,esi
  004041ce  0f8430010000          je        near 00404304h
  004041d4  8b461c                mov       eax,[esi+1Ch]
  004041d7  55                    push      ebp
  004041d8  8d6f1c                lea       ebp,[edi+1Ch]
  004041db  894500                mov       [ebp],eax
  004041de  8b4e20                mov       ecx,[esi+20h]
  004041e1  894f20                mov       [edi+20h],ecx
  004041e4  8b5624                mov       edx,[esi+24h]
  004041e7  33c0                  xor       eax,eax
  004041e9  895724                mov       [edi+24h],edx
  004041ec  8a4604                mov       al,[esi+4]
  004041ef  0fbf4e18              movsx     ecx,word [esi+18h]
  004041f3  8b148540965e00        mov       edx,[eax*4+5E9640h]
  004041fa  8b048a                mov       eax,[edx+ecx*4]
  004041fd  8b0e                  mov       ecx,[esi]
  004041ff  81c908100000          or        ecx,1008h
  00404205  8944240c              mov       [esp+0Ch],eax
  00404209  890e                  mov       [esi],ecx
  0040420b  66837f1000            cmp       word [edi+10h],0
  00404210  0f8582000000          jne       near 00404298h
  00404216  66c747100100          mov       word [edi+10h],1
  0040421c  e81f2f1400            call      00547140h
  00404221  99                    cdq
  00404222  b905000000            mov       ecx,5
  00404227  f7f9                  idiv      ecx
  00404229  8d442410              lea       eax,[esp+10h]
  0040422d  8d4c2414              lea       ecx,[esp+14h]
  00404231  83c20f                add       edx,0Fh
  00404234  66895712              mov       [edi+12h],dx
  00404238  8d54241c              lea       edx,[esp+1Ch]
  0040423c  52                    push      edx
  0040423d  50                    push      eax
  0040423e  51                    push      ecx
  0040423f  56                    push      esi
  00404240  c6460a00              mov       byte [esi+0Ah],0
  00404244  c7461802000000        mov       dword [esi+18h],2
  0040424b  e830471300            call      00538980h
  00404250  8b44242c              mov       eax,[esp+2Ch]
  00404254  83c410                add       esp,10h
  00404257  0fbf481c              movsx     ecx,word [eax+1Ch]
  0040425b  0fbf501e              movsx     edx,word [eax+1Eh]
  0040425f  0fbf4020              movsx     eax,word [eax+20h]
  00404263  03ca                  add       ecx,edx
  00404265  6a00                  push      0
  00404267  03c8                  add       ecx,eax
  00404269  b856555555            mov       eax,55555556h
  0040426e  f7e9                  imul      ecx
  00404270  8bca                  mov       ecx,edx
  00404272  6a00                  push      0
  00404274  c1e91f                shr       ecx,1Fh
  00404277  68000a0000            push      0A00h
  0040427c  6800040000            push      400h
  00404281  03d1                  add       edx,ecx
  00404283  6a1f                  push      1Fh
  00404285  57                    push      edi
  00404286  66895714              mov       [edi+14h],dx
  0040428a  66c747160000          mov       word [edi+16h],0
  00404290  e82befffff            call      004031C0h
  00404295  83c418                add       esp,18h

LAB_00404298:
  00404298  66ff4f12              dec       word [edi+12h]
  0040429c  6aff                  push      0FFFFFFFFh
  0040429e  6a00                  push      0
  004042a0  6a34                  push      34h
  004042a2  6a00                  push      0
  004042a4  55                    push      ebp
  004042a5  57                    push      edi
  004042a6  e895690700            call      0047AC40h
  004042ab  83c418                add       esp,18h
  004042ae  66837f1200            cmp       word [edi+12h],0
  004042b3  7d10                  jge       short 004042C5h
  004042b5  8b0f                  mov       ecx,[edi]
  004042b7  81e1ffffff7f          and       ecx,7FFFFFFFh
  004042bd  890f                  mov       [edi],ecx
  004042bf  8b06                  mov       eax,[esi]
  004042c1  24f7                  and       al,0F7h
  004042c3  8906                  mov       [esi],eax

LAB_004042c5:
  004042c5  6af1                  push      0FFFFFFF1h
  004042c7  6af1                  push      0FFFFFFF1h
  004042c9  6af1                  push      0FFFFFFF1h
  004042cb  56                    push      esi
  004042cc  e80f0b0000            call      00404DE0h
  004042d1  8a4716                mov       al,[edi+16h]
  004042d4  83c410                add       esp,10h
  004042d7  a801                  test      al,1
  004042d9  7424                  je        short 004042FFh
  004042db  8b54240c              mov       edx,[esp+0Ch]
  004042df  66ff461a              inc       word [esi+1Ah]
  004042e3  668b4e1a              mov       cx,[esi+1Ah]
  004042e7  668b4216              mov       ax,[edx+16h]
  004042eb  0fbfc9                movsx     ecx,cx
  004042ee  8bd0                  mov       edx,eax
  004042f0  81e2ffff0000          and       edx,0FFFFh
  004042f6  3bca                  cmp       ecx,edx
  004042f8  7c05                  jl        short 004042FFh
  004042fa  48                    dec       eax
  004042fb  6689461a              mov       [esi+1Ah],ax

LAB_004042ff:
  004042ff  66ff4716              inc       word [edi+16h]
  00404303  5d                    pop       ebp

LAB_00404304:
  00404304  5f                    pop       edi
  00404305  5e                    pop       esi
  00404306  83c40c                add       esp,0Ch
  00404309  c3                    ret

4 cible(s) de saut interne : 0x00404298, 0x004042c5, 0x004042ff, 0x00404304
