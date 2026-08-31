
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047f400  section .text  offset fichier 0x7e800  98 octets

  0047f400  8b442404              mov       eax,[esp+4]
  0047f404  56                    push      esi
  0047f405  57                    push      edi
  0047f406  0fbf7012              movsx     esi,word [eax+12h]
  0047f40a  668b5028              mov       dx,[eax+28h]
  0047f40e  8bfe                  mov       edi,esi
  0047f410  0fbfca                movsx     ecx,dx
  0047f413  2bf9                  sub       edi,ecx
  0047f415  8a4814                mov       cl,[eax+14h]
  0047f418  6683403240            add       word [eax+32h],40h
  0047f41d  d3ff                  sar       edi,cl
  0047f41f  8d0c17                lea       ecx,[edi+edx]
  0047f422  8b5038                mov       edx,[eax+38h]
  0047f425  81faf0f0f000          cmp       edx,0F0F0F0h
  0047f42b  66894828              mov       [eax+28h],cx
  0047f42f  6689482a              mov       [eax+2Ah],cx
  0047f433  6689482c              mov       [eax+2Ch],cx
  0047f437  7309                  jae       short 0047F442h
  0047f439  81c210101000          add       edx,101010h
  0047f43f  895038                mov       [eax+38h],edx

LAB_0047f442:
  0047f442  0fbfd1                movsx     edx,cx
  0047f445  83c6f0                add       esi,0FFFFFFF0h
  0047f448  3bd6                  cmp       edx,esi
  0047f44a  7c13                  jl        short 0047F45Fh
  0047f44c  8b486c                mov       ecx,[eax+6Ch]
  0047f44f  8b11                  mov       edx,[ecx]
  0047f451  81ca00000002          or        edx,2000000h
  0047f457  8911                  mov       [ecx],edx
  0047f459  c70000000000          mov       dword [eax],0

LAB_0047f45f:
  0047f45f  5f                    pop       edi
  0047f460  5e                    pop       esi
  0047f461  c3                    ret

2 cible(s) de saut interne : 0x0047f442, 0x0047f45f
