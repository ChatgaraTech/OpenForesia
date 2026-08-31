Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044a080  section .text  offset fichier 0x49480  662 octets

  0044a080  53                    push      ebx
  0044a081  55                    push      ebp
  0044a082  56                    push      esi
  0044a083  8b742410              mov       esi,[esp+10h]
  0044a087  57                    push      edi
  0044a088  33db                  xor       ebx,ebx
  0044a08a  8b7e78                mov       edi,[esi+78h]
  0044a08d  3bfb                  cmp       edi,ebx
  0044a08f  7505                  jne       short 0044A096h
  0044a091  bf58e55d00            mov       edi,5DE558h

LAB_0044a096:
  0044a096  8b471c                mov       eax,[edi+1Ch]
  0044a099  8d6f1c                lea       ebp,[edi+1Ch]
  0044a09c  89461c                mov       [esi+1Ch],eax
  0044a09f  8b4f20                mov       ecx,[edi+20h]
  0044a0a2  81c100400b00          add       ecx,0B4000h
  0044a0a8  894e20                mov       [esi+20h],ecx
  0044a0ab  8b5724                mov       edx,[edi+24h]
  0044a0ae  895624                mov       [esi+24h],edx
  0044a0b1  0fbf4732              movsx     eax,word [edi+32h]
  0044a0b5  50                    push      eax
  0044a0b6  e861b90f00            call      00545A1Ch
  0044a0bb  8b561c                mov       edx,[esi+1Ch]
  0044a0be  8d0480                lea       eax,[eax+eax*4]
  0044a0c1  8d0c80                lea       ecx,[eax+eax*4]
  0044a0c4  c1e102                shl       ecx,2
  0044a0c7  03d1                  add       edx,ecx
  0044a0c9  89561c                mov       [esi+1Ch],edx
  0044a0cc  0fbf5732              movsx     edx,word [edi+32h]
  0044a0d0  52                    push      edx
  0044a0d1  e840b90f00            call      00545A16h
  0044a0d6  8b4e24                mov       ecx,[esi+24h]
  0044a0d9  8d0480                lea       eax,[eax+eax*4]
  0044a0dc  8d0480                lea       eax,[eax+eax*4]
  0044a0df  c1e002                shl       eax,2
  0044a0e2  03c8                  add       ecx,eax
  0044a0e4  66395e10              cmp       [esi+10h],bx
  0044a0e8  894e24                mov       [esi+24h],ecx
  0044a0eb  7550                  jne       short 0044A13Dh
  0044a0ed  6800130000            push      1300h
  0044a0f2  c646041a              mov       byte [esi+4],1Ah
  0044a0f6  885e0a                mov       [esi+0Ah],bl
  0044a0f9  885e0b                mov       [esi+0Bh],bl
  0044a0fc  66895e28              mov       [esi+28h],bx
  0044a100  66c7462a401f          mov       word [esi+2Ah],1F40h
  0044a106  66895e2c              mov       [esi+2Ch],bx
  0044a10a  66c746100100          mov       word [esi+10h],1
  0044a110  66c746320020          mov       word [esi+32h],2000h
  0044a116  66895e34              mov       [esi+34h],bx
  0044a11a  668b4f2a              mov       cx,[edi+2Ah]
  0044a11e  6800130000            push      1300h
  0044a123  6895010000            push      195h
  0044a128  6a16                  push      16h
  0044a12a  66894e14              mov       [esi+14h],cx
  0044a12e  66899e98000000        mov       [esi+98h],bx
  0044a135  e8662d0f00            call      0053CEA0h
  0044a13a  83c410                add       esp,10h

LAB_0044a13d:
  0044a13d  66837e1001            cmp       word [esi+10h],1
  0044a142  b890010000            mov       eax,190h
  0044a147  7548                  jne       short 0044A191h
  0044a149  66014628              add       [esi+28h],ax
  0044a14d  6601462c              add       [esi+2Ch],ax
  0044a151  668b4632              mov       ax,[esi+32h]
  0044a155  0fbf5732              movsx     edx,word [edi+32h]
  0044a159  0fbfc8                movsx     ecx,ax
  0044a15c  2bd1                  sub       edx,ecx
  0044a15e  b9401f0000            mov       ecx,1F40h
  0044a163  81c200040000          add       edx,400h
  0044a169  c1fa02                sar       edx,2
  0044a16c  03d0                  add       edx,eax
  0044a16e  668b4628              mov       ax,[esi+28h]
  0044a172  663bc1                cmp       ax,cx
  0044a175  66895632              mov       [esi+32h],dx
  0044a179  7c16                  jl        short 0044A191h
  0044a17b  66894e28              mov       [esi+28h],cx
  0044a17f  66894e2a              mov       [esi+2Ah],cx
  0044a183  66894e2c              mov       [esi+2Ch],cx
  0044a187  66c746100200          mov       word [esi+10h],2
  0044a18d  66895e12              mov       [esi+12h],bx

LAB_0044a191:
  0044a191  66837e1002            cmp       word [esi+10h],2
  0044a196  0f8575010000          jne       near 0044A311h
  0044a19c  668b4632              mov       ax,[esi+32h]
  0044a1a0  0fbf5732              movsx     edx,word [edi+32h]
  0044a1a4  0fbfc8                movsx     ecx,ax
  0044a1a7  668146349001          add       word [esi+34h],190h
  0044a1ad  2bd1                  sub       edx,ecx
  0044a1af  81c200040000          add       edx,400h
  0044a1b5  c1fa03                sar       edx,3
  0044a1b8  03d0                  add       edx,eax
  0044a1ba  66817e346c07          cmp       word [esi+34h],76Ch
  0044a1c0  66895632              mov       [esi+32h],dx
  0044a1c4  7e11                  jle       short 0044A1D7h
  0044a1c6  b818fcffff            mov       eax,0FFFFFC18h
  0044a1cb  66014628              add       [esi+28h],ax
  0044a1cf  6601462a              add       [esi+2Ah],ax
  0044a1d3  6601462c              add       [esi+2Ch],ax

LAB_0044a1d7:
  0044a1d7  66395e28              cmp       [esi+28h],bx
  0044a1db  7d1d                  jge       short 0044A1FAh
  0044a1dd  8b06                  mov       eax,[esi]
  0044a1df  668b5614              mov       dx,[esi+14h]
  0044a1e3  25ffffff7f            and       eax,7FFFFFFFh
  0044a1e8  66895e28              mov       [esi+28h],bx
  0044a1ec  66895e2a              mov       [esi+2Ah],bx
  0044a1f0  66895e2c              mov       [esi+2Ch],bx
  0044a1f4  8906                  mov       [esi],eax
  0044a1f6  6689572a              mov       [edi+2Ah],dx

LAB_0044a1fa:
  0044a1fa  66837e1203            cmp       word [esi+12h],3
  0044a1ff  0f8592000000          jne       near 0044A297h
  0044a205  6aff                  push      0FFFFFFFFh
  0044a207  53                    push      ebx
  0044a208  688e000000            push      8Eh
  0044a20d  53                    push      ebx
  0044a20e  55                    push      ebp
  0044a20f  56                    push      esi
  0044a210  e82b0a0300            call      0047AC40h
  0044a215  83c418                add       esp,18h
  0044a218  6aff                  push      0FFFFFFFFh
  0044a21a  53                    push      ebx
  0044a21b  688e000000            push      8Eh
  0044a220  53                    push      ebx
  0044a221  55                    push      ebp
  0044a222  56                    push      esi
  0044a223  e8180a0300            call      0047AC40h
  0044a228  83c418                add       esp,18h
  0044a22b  6aff                  push      0FFFFFFFFh
  0044a22d  53                    push      ebx
  0044a22e  688e000000            push      8Eh
  0044a233  53                    push      ebx
  0044a234  55                    push      ebp
  0044a235  56                    push      esi
  0044a236  e8050a0300            call      0047AC40h
  0044a23b  83c418                add       esp,18h
  0044a23e  6aff                  push      0FFFFFFFFh
  0044a240  53                    push      ebx
  0044a241  688e000000            push      8Eh
  0044a246  53                    push      ebx
  0044a247  55                    push      ebp
  0044a248  56                    push      esi
  0044a249  e8f2090300            call      0047AC40h
  0044a24e  83c418                add       esp,18h
  0044a251  6aff                  push      0FFFFFFFFh
  0044a253  53                    push      ebx
  0044a254  688e000000            push      8Eh
  0044a259  53                    push      ebx
  0044a25a  55                    push      ebp
  0044a25b  56                    push      esi
  0044a25c  e8df090300            call      0047AC40h
  0044a261  83c418                add       esp,18h
  0044a264  6aff                  push      0FFFFFFFFh
  0044a266  53                    push      ebx
  0044a267  688e000000            push      8Eh
  0044a26c  53                    push      ebx
  0044a26d  55                    push      ebp
  0044a26e  56                    push      esi
  0044a26f  e8cc090300            call      0047AC40h
  0044a274  b096                  mov       al,96h
  0044a276  c60527e05d0001        mov       byte [5DE027h],1
  0044a27d  c60524e05d00ff        mov       byte [5DE024h],0FFh
  0044a284  a225e05d00            mov       [5DE025h],al
  0044a289  a226e05d00            mov       [5DE026h],al
  0044a28e  83c418                add       esp,18h
  0044a291  66c7472a0008          mov       word [edi+2Ah],800h

LAB_0044a297:
  0044a297  66837e1205            cmp       word [esi+12h],5
  0044a29c  7519                  jne       short 0044A2B7h
  0044a29e  c60527e05d0001        mov       byte [5DE027h],1
  0044a2a5  881d24e05d00          mov       [5DE024h],bl
  0044a2ab  881d25e05d00          mov       [5DE025h],bl
  0044a2b1  881d26e05d00          mov       [5DE026h],bl

LAB_0044a2b7:
  0044a2b7  668b472a              mov       ax,[edi+2Ah]
  0044a2bb  0fbf4e14              movsx     ecx,word [esi+14h]
  0044a2bf  0fbfd0                movsx     edx,ax
  0044a2c2  2bca                  sub       ecx,edx
  0044a2c4  c1f902                sar       ecx,2
  0044a2c7  03c8                  add       ecx,eax
  0044a2c9  66894f2a              mov       [edi+2Ah],cx
  0044a2cd  66837e1202            cmp       word [esi+12h],2
  0044a2d2  7519                  jne       short 0044A2EDh
  0044a2d4  68000a0000            push      0A00h
  0044a2d9  68000a0000            push      0A00h
  0044a2de  6860090000            push      960h
  0044a2e3  6a22                  push      22h
  0044a2e5  e8b62b0f00            call      0053CEA0h
  0044a2ea  83c410                add       esp,10h

LAB_0044a2ed:
  0044a2ed  66837e1204            cmp       word [esi+12h],4
  0044a2f2  7519                  jne       short 0044A30Dh
  0044a2f4  6800130000            push      1300h
  0044a2f9  6800130000            push      1300h
  0044a2fe  68b0040000            push      4B0h
  0044a303  6a04                  push      4
  0044a305  e8962b0f00            call      0053CEA0h
  0044a30a  83c410                add       esp,10h

LAB_0044a30d:
  0044a30d  66ff4612              inc       word [esi+12h]

LAB_0044a311:
  0044a311  5f                    pop       edi
  0044a312  5e                    pop       esi
  0044a313  5d                    pop       ebp
  0044a314  5b                    pop       ebx
  0044a315  c3                    ret

10 cible(s) de saut interne : 0x0044a096, 0x0044a13d, 0x0044a191, 0x0044a1d7, 0x0044a1fa, 0x0044a297, 0x0044a2b7, 0x0044a2ed, 0x0044a30d, 0x0044a311
