
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047e150  section .text  offset fichier 0x7d550  169 octets

  0047e150  8b442404              mov       eax,[esp+4]
  0047e154  53                    push      ebx
  0047e155  56                    push      esi
  0047e156  57                    push      edi
  0047e157  668b4832              mov       cx,[eax+32h]
  0047e15b  8b5070                mov       edx,[eax+70h]
  0047e15e  0fbf7014              movsx     esi,word [eax+14h]
  0047e162  0fbff9                movsx     edi,cx
  0047e165  2bf7                  sub       esi,edi
  0047e167  c1fe03                sar       esi,3
  0047e16a  03f1                  add       esi,ecx
  0047e16c  66897032              mov       [eax+32h],si
  0047e170  668b7034              mov       si,[eax+34h]
  0047e174  668bce                mov       cx,si
  0047e177  66c1f903              sar       cx,3
  0047e17b  2bf1                  sub       esi,ecx
  0047e17d  668b4812              mov       cx,[eax+12h]
  0047e181  668bf9                mov       di,cx
  0047e184  66897034              mov       [eax+34h],si
  0047e188  66c1ff03              sar       di,3
  0047e18c  2bcf                  sub       ecx,edi
  0047e18e  8b7844                mov       edi,[eax+44h]
  0047e191  66894812              mov       [eax+12h],cx
  0047e195  8b5a1c                mov       ebx,[edx+1Ch]
  0047e198  0fbfc9                movsx     ecx,cx
  0047e19b  03fb                  add       edi,ebx
  0047e19d  89781c                mov       [eax+1Ch],edi
  0047e1a0  8b7a60                mov       edi,[edx+60h]
  0047e1a3  c1e10c                shl       ecx,0Ch
  0047e1a6  03cf                  add       ecx,edi
  0047e1a8  894820                mov       [eax+20h],ecx
  0047e1ab  8b484c                mov       ecx,[eax+4Ch]
  0047e1ae  8b7a24                mov       edi,[edx+24h]
  0047e1b1  03cf                  add       ecx,edi
  0047e1b3  6683fe08              cmp       si,8
  0047e1b7  894824                mov       [eax+24h],ecx
  0047e1ba  7d25                  jge       short 0047E1E1h
  0047e1bc  8b486c                mov       ecx,[eax+6Ch]
  0047e1bf  8b11                  mov       edx,[ecx]
  0047e1c1  81ca00000002          or        edx,2000000h
  0047e1c7  8911                  mov       [ecx],edx
  0047e1c9  8b4838                mov       ecx,[eax+38h]
  0047e1cc  81c1f0efefff          add       ecx,0FFEFEFF0h
  0047e1d2  894838                mov       [eax+38h],ecx
  0047e1d5  751e                  jne       short 0047E1F5h
  0047e1d7  c70000000000          mov       dword [eax],0
  0047e1dd  5f                    pop       edi
  0047e1de  5e                    pop       esi
  0047e1df  5b                    pop       ebx
  0047e1e0  c3                    ret

LAB_0047e1e1:
  0047e1e1  8b4838                mov       ecx,[eax+38h]
  0047e1e4  81f9f0f0f000          cmp       ecx,0F0F0F0h
  0047e1ea  7309                  jae       short 0047E1F5h
  0047e1ec  81c110101000          add       ecx,101010h
  0047e1f2  894838                mov       [eax+38h],ecx

LAB_0047e1f5:
  0047e1f5  5f                    pop       edi
  0047e1f6  5e                    pop       esi
  0047e1f7  5b                    pop       ebx
  0047e1f8  c3                    ret

2 cible(s) de saut interne : 0x0047e1e1, 0x0047e1f5
