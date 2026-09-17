Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00418640  section .text  offset fichier 0x17a40  96 octets

  00418640  8b442404              mov       eax,[esp+4]
  00418644  50                    push      eax
  00418645  c7405c3b000000        mov       dword [eax+5Ch],3Bh
  0041864c  8b0d34a75f00          mov       ecx,[5FA734h]
  00418652  89481c                mov       [eax+1Ch],ecx
  00418655  8b1538a75f00          mov       edx,[5FA738h]
  0041865b  895020                mov       [eax+20h],edx
  0041865e  8b0d3ca75f00          mov       ecx,[5FA73Ch]
  00418664  81c100400600          add       ecx,64000h
  0041866a  894824                mov       [eax+24h],ecx
  0041866d  668b1548a75f00        mov       dx,[5FA748h]
  00418674  66895030              mov       [eax+30h],dx
  00418678  668b0d4aa75f00        mov       cx,[5FA74Ah]
  0041867f  66894832              mov       [eax+32h],cx
  00418683  668b154ca75f00        mov       dx,[5FA74Ch]
  0041868a  66895034              mov       [eax+34h],dx
  0041868e  e83dfdffff            call      004183D0h
  00418693  83c404                add       esp,4
  00418696  c3                    ret
  00418697  90                    nop
  00418698  90                    nop
  00418699  90                    nop
  0041869a  90                    nop
  0041869b  90                    nop
  0041869c  90                    nop
  0041869d  90                    nop
  0041869e  90                    nop
  0041869f  90                    nop
