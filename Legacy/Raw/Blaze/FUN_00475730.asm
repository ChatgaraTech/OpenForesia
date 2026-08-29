Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00475730  section .text  offset fichier 0x74b30  46 octets

  00475730  8b442404              mov       eax,[esp+4]
  00475734  8b4820                mov       ecx,[eax+20h]
  00475737  668b5010              mov       dx,[eax+10h]
  0047573b  81c100a00000          add       ecx,0A000h
  00475741  894820                mov       [eax+20h],ecx
  00475744  668bca                mov       cx,dx
  00475747  66d1f9                sar       cx,1
  0047574a  80f907                cmp       cl,7
  0047574d  884840                mov       [eax+40h],cl
  00475750  7206                  jb        short 00475758h
  00475752  c70000000000          mov       dword [eax],0

LAB_00475758:
  00475758  42                    inc       edx
  00475759  66895010              mov       [eax+10h],dx
  0047575d  c3                    ret

1 cible(s) de saut interne : 0x00475758
