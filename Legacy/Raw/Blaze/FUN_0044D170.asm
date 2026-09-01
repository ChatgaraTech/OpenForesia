Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044d170  section .text  offset fichier 0x4c570  507 octets

  0044d170  53                    push      ebx
  0044d171  56                    push      esi
  0044d172  8b74240c              mov       esi,[esp+0Ch]
  0044d176  33db                  xor       ebx,ebx
  0044d178  8b4678                mov       eax,[esi+78h]
  0044d17b  3bc3                  cmp       eax,ebx
  0044d17d  7505                  jne       short 0044D184h
  0044d17f  b858e55d00            mov       eax,5DE558h

LAB_0044d184:
  0044d184  8b481c                mov       ecx,[eax+1Ch]
  0044d187  66395e10              cmp       [esi+10h],bx
  0044d18b  894e1c                mov       [esi+1Ch],ecx
  0044d18e  8b5060                mov       edx,[eax+60h]
  0044d191  895620                mov       [esi+20h],edx
  0044d194  8b4024                mov       eax,[eax+24h]
  0044d197  894624                mov       [esi+24h],eax
  0044d19a  7535                  jne       short 0044D1D1h
  0044d19c  c7463c98155500        mov       dword [esi+3Ch],551598h
  0044d1a3  885e40                mov       [esi+40h],bl
  0044d1a6  c6460a07              mov       byte [esi+0Ah],7
  0044d1aa  885e0b                mov       [esi+0Bh],bl
  0044d1ad  66c746100100          mov       word [esi+10h],1
  0044d1b3  c7465cfa000000        mov       dword [esi+5Ch],0FAh
  0044d1ba  66895e28              mov       [esi+28h],bx
  0044d1be  66895e2a              mov       [esi+2Ah],bx
  0044d1c2  885e38                mov       [esi+38h],bl
  0044d1c5  885e39                mov       [esi+39h],bl
  0044d1c8  885e3a                mov       [esi+3Ah],bl
  0044d1cb  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h

LAB_0044d1d1:
  0044d1d1  66837e1001            cmp       word [esi+10h],1
  0044d1d6  757e                  jne       short 0044D256h
  0044d1d8  668b4628              mov       ax,[esi+28h]
  0044d1dc  bab80b0000            mov       edx,0BB8h
  0044d1e1  0fbfc8                movsx     ecx,ax
  0044d1e4  2bd1                  sub       edx,ecx
  0044d1e6  c1fa02                sar       edx,2
  0044d1e9  03d0                  add       edx,eax
  0044d1eb  668b462a              mov       ax,[esi+2Ah]
  0044d1ef  0fbfc8                movsx     ecx,ax
  0044d1f2  66895628              mov       [esi+28h],dx
  0044d1f6  bab80b0000            mov       edx,0BB8h
  0044d1fb  2bd1                  sub       edx,ecx
  0044d1fd  c1fa02                sar       edx,2
  0044d200  03d0                  add       edx,eax
  0044d202  8b465c                mov       eax,[esi+5Ch]
  0044d205  8bc8                  mov       ecx,eax
  0044d207  6689562a              mov       [esi+2Ah],dx
  0044d20b  8b5620                mov       edx,[esi+20h]
  0044d20e  83c0fe                add       eax,0FFFFFFFEh
  0044d211  c1e10c                shl       ecx,0Ch
  0044d214  03d1                  add       edx,ecx
  0044d216  83f81e                cmp       eax,1Eh
  0044d219  895620                mov       [esi+20h],edx
  0044d21c  89465c                mov       [esi+5Ch],eax
  0044d21f  7d14                  jge       short 0044D235h
  0044d221  c7465c1e000000        mov       dword [esi+5Ch],1Eh
  0044d228  66c746100200          mov       word [esi+10h],2
  0044d22e  66895e12              mov       [esi+12h],bx
  0044d232  5e                    pop       esi
  0044d233  5b                    pop       ebx
  0044d234  c3                    ret

LAB_0044d235:
  0044d235  6a08                  push      8
  0044d237  6a08                  push      8
  0044d239  6a08                  push      8
  0044d23b  56                    push      esi
  0044d23c  e89f7bfbff            call      00404DE0h
  0044d241  8a4e38                mov       cl,[esi+38h]
  0044d244  b0a0                  mov       al,0A0h
  0044d246  83c410                add       esp,10h
  0044d249  3ac8                  cmp       cl,al
  0044d24b  7209                  jb        short 0044D256h
  0044d24d  884638                mov       [esi+38h],al
  0044d250  884639                mov       [esi+39h],al
  0044d253  88463a                mov       [esi+3Ah],al

LAB_0044d256:
  0044d256  66837e1002            cmp       word [esi+10h],2
  0044d25b  0f8507010000          jne       near 0044D368h
  0044d261  8b565c                mov       edx,[esi+5Ch]
  0044d264  8b4620                mov       eax,[esi+20h]
  0044d267  c1e20c                shl       edx,0Ch
  0044d26a  03c2                  add       eax,edx
  0044d26c  894620                mov       [esi+20h],eax
  0044d26f  b8f4010000            mov       eax,1F4h
  0044d274  66014628              add       [esi+28h],ax
  0044d278  6601462a              add       [esi+2Ah],ax
  0044d27c  66837e120f            cmp       word [esi+12h],0Fh
  0044d281  7f63                  jg        short 0044D2E6h
  0044d283  660fb60524e05d00      movzx     ax,byte [5DE024h]
  0044d28b  660fb61525e05d00      movzx     dx,byte [5DE025h]
  0044d293  660fb60d26e05d00      movzx     cx,byte [5DE026h]
  0044d29b  83c00f                add       eax,0Fh
  0044d29e  83c20f                add       edx,0Fh
  0044d2a1  83c10f                add       ecx,0Fh
  0044d2a4  663dff00              cmp       ax,0FFh
  0044d2a8  c60527e05d0001        mov       byte [5DE027h],1
  0044d2af  7e05                  jle       short 0044D2B6h
  0044d2b1  b8ff000000            mov       eax,0FFh

LAB_0044d2b6:
  0044d2b6  6681faff00            cmp       dx,0FFh
  0044d2bb  7e05                  jle       short 0044D2C2h
  0044d2bd  baff000000            mov       edx,0FFh

LAB_0044d2c2:
  0044d2c2  6681f9ff00            cmp       cx,0FFh
  0044d2c7  7e05                  jle       short 0044D2CEh
  0044d2c9  b9ff000000            mov       ecx,0FFh

LAB_0044d2ce:
  0044d2ce  a224e05d00            mov       [5DE024h],al
  0044d2d3  881525e05d00          mov       [5DE025h],dl
  0044d2d9  880d26e05d00          mov       [5DE026h],cl
  0044d2df  66ff4612              inc       word [esi+12h]
  0044d2e3  5e                    pop       esi
  0044d2e4  5b                    pop       ebx
  0044d2e5  c3                    ret

LAB_0044d2e6:
  0044d2e6  8b06                  mov       eax,[esi]
  0044d2e8  80cc40                or        ah,40h
  0044d2eb  8906                  mov       [esi],eax
  0044d2ed  c60527e05d0001        mov       byte [5DE027h],1
  0044d2f4  660fb60524e05d00      movzx     ax,byte [5DE024h]
  0044d2fc  660fb61525e05d00      movzx     dx,byte [5DE025h]
  0044d304  660fb60d26e05d00      movzx     cx,byte [5DE026h]
  0044d30c  83e80f                sub       eax,0Fh
  0044d30f  83ea0f                sub       edx,0Fh
  0044d312  83e90f                sub       ecx,0Fh
  0044d315  663bc3                cmp       ax,bx
  0044d318  7d02                  jge       short 0044D31Ch
  0044d31a  33c0                  xor       eax,eax

LAB_0044d31c:
  0044d31c  663bd3                cmp       dx,bx
  0044d31f  7d02                  jge       short 0044D323h
  0044d321  33d2                  xor       edx,edx

LAB_0044d323:
  0044d323  663bcb                cmp       cx,bx
  0044d326  7d02                  jge       short 0044D32Ah
  0044d328  33c9                  xor       ecx,ecx

LAB_0044d32a:
  0044d32a  a224e05d00            mov       [5DE024h],al
  0044d32f  881525e05d00          mov       [5DE025h],dl
  0044d335  880d26e05d00          mov       [5DE026h],cl
  0044d33b  66837e1220            cmp       word [esi+12h],20h
  0044d340  7522                  jne       short 0044D364h
  0044d342  8b06                  mov       eax,[esi]
  0044d344  25ffffff7f            and       eax,7FFFFFFFh
  0044d349  8906                  mov       [esi],eax
  0044d34b  c60527e05d0001        mov       byte [5DE027h],1
  0044d352  881d24e05d00          mov       [5DE024h],bl
  0044d358  881d25e05d00          mov       [5DE025h],bl
  0044d35e  881d26e05d00          mov       [5DE026h],bl

LAB_0044d364:
  0044d364  66ff4612              inc       word [esi+12h]

LAB_0044d368:
  0044d368  5e                    pop       esi
  0044d369  5b                    pop       ebx
  0044d36a  c3                    ret

13 cible(s) de saut interne : 0x0044d184, 0x0044d1d1, 0x0044d235, 0x0044d256, 0x0044d2b6, 0x0044d2c2, 0x0044d2ce, 0x0044d2e6, 0x0044d31c, 0x0044d323, 0x0044d32a, 0x0044d364, 0x0044d368
