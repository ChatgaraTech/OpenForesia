Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00477600  section .text  offset fichier 0x76a00  96 octets

  00477600  8b442404              mov       eax,[esp+4]
  00477604  56                    push      esi
  00477605  57                    push      edi
  00477606  bfff7f0000            mov       edi,7FFFh
  0047760b  668b702a              mov       si,[eax+2Ah]
  0047760f  8b5070                mov       edx,[eax+70h]
  00477612  0fbfce                movsx     ecx,si
  00477615  2bf9                  sub       edi,ecx
  00477617  8a4814                mov       cl,[eax+14h]
  0047761a  d3ff                  sar       edi,cl
  0047761c  8b4844                mov       ecx,[eax+44h]
  0047761f  03fe                  add       edi,esi
  00477621  6689782a              mov       [eax+2Ah],di
  00477625  8b7a1c                mov       edi,[edx+1Ch]
  00477628  03cf                  add       ecx,edi
  0047762a  89481c                mov       [eax+1Ch],ecx
  0047762d  8b484c                mov       ecx,[eax+4Ch]
  00477630  8b7224                mov       esi,[edx+24h]
  00477633  8b5020                mov       edx,[eax+20h]
  00477636  03ce                  add       ecx,esi
  00477638  81c200400100          add       edx,14000h
  0047763e  894824                mov       [eax+24h],ecx
  00477641  8b4838                mov       ecx,[eax+38h]
  00477644  81c1f0efefff          add       ecx,0FFEFEFF0h
  0047764a  895020                mov       [eax+20h],edx
  0047764d  894838                mov       [eax+38h],ecx
  00477650  7506                  jne       short 00477658h
  00477652  c70000000000          mov       dword [eax],0

LAB_00477658:
  00477658  5f                    pop       edi
  00477659  5e                    pop       esi
  0047765a  c3                    ret
  0047765b  90                    nop
  0047765c  90                    nop
  0047765d  90                    nop
  0047765e  90                    nop
  0047765f  90                    nop

1 cible(s) de saut interne : 0x00477658
