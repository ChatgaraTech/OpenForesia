Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00480a20  section .text  offset fichier 0x7fe20  79 octets

  00480a20  8b442404              mov       eax,[esp+4]
  00480a24  56                    push      esi
  00480a25  8b486c                mov       ecx,[eax+6Ch]
  00480a28  8b30                  mov       esi,[eax]
  00480a2a  8b11                  mov       edx,[ecx]
  00480a2c  81e200000002          and       edx,2000000h
  00480a32  0bf2                  or        esi,edx
  00480a34  8bd6                  mov       edx,esi
  00480a36  8930                  mov       [eax],esi
  00480a38  f7c200000002          test      edx,2000000h
  00480a3e  7408                  je        short 00480A48h
  00480a40  c70000000000          mov       dword [eax],0
  00480a46  5e                    pop       esi
  00480a47  c3                    ret

LAB_00480a48:
  00480a48  8b5044                mov       edx,[eax+44h]
  00480a4b  8b711c                mov       esi,[ecx+1Ch]
  00480a4e  03d6                  add       edx,esi
  00480a50  89501c                mov       [eax+1Ch],edx
  00480a53  8b5048                mov       edx,[eax+48h]
  00480a56  035120                add       edx,[ecx+20h]
  00480a59  895020                mov       [eax+20h],edx
  00480a5c  8b504c                mov       edx,[eax+4Ch]
  00480a5f  8b7124                mov       esi,[ecx+24h]
  00480a62  6681403400ff          add       word [eax+34h],0FF00h
  00480a68  03d6                  add       edx,esi
  00480a6a  5e                    pop       esi
  00480a6b  895024                mov       [eax+24h],edx
  00480a6e  c3                    ret

1 cible(s) de saut interne : 0x00480a48
