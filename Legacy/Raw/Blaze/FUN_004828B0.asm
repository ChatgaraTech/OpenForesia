Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004828b0  section .text  offset fichier 0x81cb0  58 octets

  004828b0  8b442404              mov       eax,[esp+4]
  004828b4  56                    push      esi
  004828b5  8b486c                mov       ecx,[eax+6Ch]
  004828b8  8b30                  mov       esi,[eax]
  004828ba  8b11                  mov       edx,[ecx]
  004828bc  81e200000002          and       edx,2000000h
  004828c2  0bf2                  or        esi,edx
  004828c4  8930                  mov       [eax],esi
  004828c6  8b511c                mov       edx,[ecx+1Ch]
  004828c9  89501c                mov       [eax+1Ch],edx
  004828cc  8b5120                mov       edx,[ecx+20h]
  004828cf  895020                mov       [eax+20h],edx
  004828d2  8b4924                mov       ecx,[ecx+24h]
  004828d5  894824                mov       [eax+24h],ecx
  004828d8  8bce                  mov       ecx,esi
  004828da  f7c100000002          test      ecx,2000000h
  004828e0  7406                  je        short 004828E8h
  004828e2  c70000000000          mov       dword [eax],0

LAB_004828e8:
  004828e8  5e                    pop       esi
  004828e9  c3                    ret

1 cible(s) de saut interne : 0x004828e8
