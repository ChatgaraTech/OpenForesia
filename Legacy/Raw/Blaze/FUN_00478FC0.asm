Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00478fc0  section .text  offset fichier 0x783c0  97 octets

  00478fc0  8b442404              mov       eax,[esp+4]
  00478fc4  56                    push      esi
  00478fc5  668b4828              mov       cx,[eax+28h]
  00478fc9  8b5070                mov       edx,[eax+70h]
  00478fcc  668bf1                mov       si,cx
  00478fcf  668140328000          add       word [eax+32h],80h
  00478fd5  66c1fe02              sar       si,2
  00478fd9  2bce                  sub       ecx,esi
  00478fdb  66894828              mov       [eax+28h],cx
  00478fdf  668b482c              mov       cx,[eax+2Ch]
  00478fe3  668bf1                mov       si,cx
  00478fe6  66c1fe02              sar       si,2
  00478fea  2bce                  sub       ecx,esi
  00478fec  6689482c              mov       [eax+2Ch],cx
  00478ff0  8b4844                mov       ecx,[eax+44h]
  00478ff3  034a1c                add       ecx,[edx+1Ch]
  00478ff6  89481c                mov       [eax+1Ch],ecx
  00478ff9  8b4848                mov       ecx,[eax+48h]
  00478ffc  034a20                add       ecx,[edx+20h]
  00478fff  894820                mov       [eax+20h],ecx
  00479002  8b484c                mov       ecx,[eax+4Ch]
  00479005  034a24                add       ecx,[edx+24h]
  00479008  894824                mov       [eax+24h],ecx
  0047900b  8b4838                mov       ecx,[eax+38h]
  0047900e  81c1f0efefff          add       ecx,0FFEFEFF0h
  00479014  894838                mov       [eax+38h],ecx
  00479017  7506                  jne       short 0047901Fh
  00479019  c70000000000          mov       dword [eax],0

LAB_0047901f:
  0047901f  5e                    pop       esi
  00479020  c3                    ret

1 cible(s) de saut interne : 0x0047901f
