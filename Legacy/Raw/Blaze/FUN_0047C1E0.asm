
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047c1e0  section .text  offset fichier 0x7b5e0  216 octets

  0047c1e0  56                    push      esi
  0047c1e1  8b742408              mov       esi,[esp+8]
  0047c1e5  57                    push      edi
  0047c1e6  8b06                  mov       eax,[esi]
  0047c1e8  a900000003            test      eax,3000000h
  0047c1ed  742a                  je        short 0047C219h
  0047c1ef  0fbf4628              movsx     eax,word [esi+28h]
  0047c1f3  8bc8                  mov       ecx,eax
  0047c1f5  c1f902                sar       ecx,2
  0047c1f8  2bc1                  sub       eax,ecx
  0047c1fa  33c9                  xor       ecx,ecx
  0047c1fc  8a4e38                mov       cl,[esi+38h]
  0047c1ff  8bd1                  mov       edx,ecx
  0047c201  c1fa02                sar       edx,2
  0047c204  2bca                  sub       ecx,edx
  0047c206  3d00010000            cmp       eax,100h
  0047c20b  0f8d87000000          jge       near 0047C298h
  0047c211  c70600000000          mov       dword [esi],0
  0047c217  eb7f                  jmp       short 0047C298h

LAB_0047c219:
  0047c219  8b7e6c                mov       edi,[esi+6Ch]
  0047c21c  8b0f                  mov       ecx,[edi]
  0047c21e  81e100000003          and       ecx,3000000h
  0047c224  0bc8                  or        ecx,eax
  0047c226  33c0                  xor       eax,eax
  0047c228  66394610              cmp       [esi+10h],ax
  0047c22c  890e                  mov       [esi],ecx
  0047c22e  752c                  jne       short 0047C25Ch
  0047c230  66c746100100          mov       word [esi+10h],1
  0047c236  894628                mov       [esi+28h],eax
  0047c239  6689462c              mov       [esi+2Ch],ax
  0047c23d  e8feae0c00            call      00547140h
  0047c242  66894630              mov       [esi+30h],ax
  0047c246  e8f5ae0c00            call      00547140h
  0047c24b  66894632              mov       [esi+32h],ax
  0047c24f  e8ecae0c00            call      00547140h
  0047c254  66894634              mov       [esi+34h],ax
  0047c258  c646041d              mov       byte [esi+4],1Dh

LAB_0047c25c:
  0047c25c  0fbf4628              movsx     eax,word [esi+28h]
  0047c260  0fbf5612              movsx     edx,word [esi+12h]
  0047c264  2bd0                  sub       edx,eax
  0047c266  33c9                  xor       ecx,ecx
  0047c268  8a4e38                mov       cl,[esi+38h]
  0047c26b  c1fa02                sar       edx,2
  0047c26e  03c2                  add       eax,edx
  0047c270  baff000000            mov       edx,0FFh
  0047c275  2bd1                  sub       edx,ecx
  0047c277  c1fa02                sar       edx,2
  0047c27a  03ca                  add       ecx,edx
  0047c27c  8b571c                mov       edx,[edi+1Ch]
  0047c27f  89561c                mov       [esi+1Ch],edx
  0047c282  8b5720                mov       edx,[edi+20h]
  0047c285  895620                mov       [esi+20h],edx
  0047c288  8b5724                mov       edx,[edi+24h]
  0047c28b  6683463230            add       word [esi+32h],30h
  0047c290  6683463440            add       word [esi+34h],40h
  0047c295  895624                mov       [esi+24h],edx

LAB_0047c298:
  0047c298  8bd0                  mov       edx,eax
  0047c29a  6689462c              mov       [esi+2Ch],ax
  0047c29e  c1e210                shl       edx,10h
  0047c2a1  0bd0                  or        edx,eax
  0047c2a3  8bc1                  mov       eax,ecx
  0047c2a5  c1e008                shl       eax,8
  0047c2a8  0bc1                  or        eax,ecx
  0047c2aa  895628                mov       [esi+28h],edx
  0047c2ad  c1e008                shl       eax,8
  0047c2b0  0bc1                  or        eax,ecx
  0047c2b2  5f                    pop       edi
  0047c2b3  894638                mov       [esi+38h],eax
  0047c2b6  5e                    pop       esi
  0047c2b7  c3                    ret

3 cible(s) de saut interne : 0x0047c219, 0x0047c25c, 0x0047c298
