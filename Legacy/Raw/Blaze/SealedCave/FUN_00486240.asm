Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00486240  section .text  offset fichier 0x85640  224 octets

  00486240  83ec0c                sub       esp,0Ch
  00486243  56                    push      esi
  00486244  8b742414              mov       esi,[esp+14h]
  00486248  57                    push      edi
  00486249  668b4614              mov       ax,[esi+14h]
  0048624d  8b7e70                mov       edi,[esi+70h]
  00486250  66014632              add       [esi+32h],ax
  00486254  66014634              add       [esi+34h],ax
  00486258  668b4634              mov       ax,[esi+34h]
  0048625c  668b4e32              mov       cx,[esi+32h]
  00486260  0fbf5612              movsx     edx,word [esi+12h]
  00486264  0fbfc0                movsx     eax,ax
  00486267  0fbfc9                movsx     ecx,cx
  0048626a  2d00040000            sub       eax,400h
  0048626f  52                    push      edx
  00486270  81e900040000          sub       ecx,400h
  00486276  50                    push      eax
  00486277  8d542410              lea       edx,[esp+10h]
  0048627b  51                    push      ecx
  0048627c  52                    push      edx
  0048627d  e84ec80a00            call      00532AD0h
  00486282  8b4644                mov       eax,[esi+44h]
  00486285  8b571c                mov       edx,[edi+1Ch]
  00486288  8b4c2418              mov       ecx,[esp+18h]
  0048628c  03c2                  add       eax,edx
  0048628e  8b54241c              mov       edx,[esp+1Ch]
  00486292  03c1                  add       eax,ecx
  00486294  8b4e48                mov       ecx,[esi+48h]
  00486297  89461c                mov       [esi+1Ch],eax
  0048629a  8b4720                mov       eax,[edi+20h]
  0048629d  83c410                add       esp,10h
  004862a0  03c8                  add       ecx,eax
  004862a2  8b442410              mov       eax,[esp+10h]
  004862a6  03ca                  add       ecx,edx
  004862a8  8b564c                mov       edx,[esi+4Ch]
  004862ab  894e20                mov       [esi+20h],ecx
  004862ae  8b4f24                mov       ecx,[edi+24h]
  004862b1  03d1                  add       edx,ecx
  004862b3  03d0                  add       edx,eax
  004862b5  668b462a              mov       ax,[esi+2Ah]
  004862b9  0fbfc8                movsx     ecx,ax
  004862bc  895624                mov       [esi+24h],edx
  004862bf  ba0080ffff            mov       edx,0FFFF8000h
  004862c4  2bd1                  sub       edx,ecx
  004862c6  c1fa02                sar       edx,2
  004862c9  03d0                  add       edx,eax
  004862cb  668b4612              mov       ax,[esi+12h]
  004862cf  668bc8                mov       cx,ax
  004862d2  6689562a              mov       [esi+2Ah],dx
  004862d6  66c1f902              sar       cx,2
  004862da  2bc1                  sub       eax,ecx
  004862dc  66894612              mov       [esi+12h],ax
  004862e0  663d1000              cmp       ax,10h
  004862e4  8b4638                mov       eax,[esi+38h]
  004862e7  7d18                  jge       short 00486301h
  004862e9  85c0                  test      eax,eax
  004862eb  7423                  je        short 00486310h
  004862ed  05f0efefff            add       eax,0FFEFEFF0h
  004862f2  85c0                  test      eax,eax
  004862f4  894638                mov       [esi+38h],eax
  004862f7  7517                  jne       short 00486310h
  004862f9  8906                  mov       [esi],eax
  004862fb  5f                    pop       edi
  004862fc  5e                    pop       esi
  004862fd  83c40c                add       esp,0Ch
  00486300  c3                    ret

LAB_00486301:
  00486301  3df0f0f000            cmp       eax,0F0F0F0h
  00486306  7308                  jae       short 00486310h
  00486308  0510101000            add       eax,101010h
  0048630d  894638                mov       [esi+38h],eax

LAB_00486310:
  00486310  5f                    pop       edi
  00486311  5e                    pop       esi
  00486312  83c40c                add       esp,0Ch
  00486315  c3                    ret
  00486316  90                    nop
  00486317  90                    nop
  00486318  90                    nop
  00486319  90                    nop
  0048631a  90                    nop
  0048631b  90                    nop
  0048631c  90                    nop
  0048631d  90                    nop
  0048631e  90                    nop
  0048631f  90                    nop

2 cible(s) de saut interne : 0x00486301, 0x00486310
