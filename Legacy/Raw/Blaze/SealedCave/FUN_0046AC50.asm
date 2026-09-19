Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046ac50  section .text  offset fichier 0x6a050  288 octets

  0046ac50  53                    push      ebx
  0046ac51  56                    push      esi
  0046ac52  8b74240c              mov       esi,[esp+0Ch]
  0046ac56  57                    push      edi
  0046ac57  6800000040            push      40000000h
  0046ac5c  6a10                  push      10h
  0046ac5e  56                    push      esi
  0046ac5f  e89ca30c00            call      00535000h
  0046ac64  83c40c                add       esp,0Ch
  0046ac67  8bf8                  mov       edi,eax
  0046ac69  e802c1ffff            call      00466D70h
  0046ac6e  0fbfc8                movsx     ecx,ax
  0046ac71  33db                  xor       ebx,ebx
  0046ac73  3bf9                  cmp       edi,ecx
  0046ac75  b801000000            mov       eax,1
  0046ac7a  751b                  jne       short 0046AC97h
  0046ac7c  66395e10              cmp       [esi+10h],bx
  0046ac80  7515                  jne       short 0046AC97h
  0046ac82  8b5620                mov       edx,[esi+20h]
  0046ac85  66894610              mov       [esi+10h],ax
  0046ac89  c1fa0c                sar       edx,0Ch
  0046ac8c  66895e12              mov       [esi+12h],bx
  0046ac90  6689969a000000        mov       [esi+9Ah],dx

LAB_0046ac97:
  0046ac97  66394610              cmp       [esi+10h],ax
  0046ac9b  b902000000            mov       ecx,2
  0046aca0  7518                  jne       short 0046ACBAh
  0046aca2  66ff4612              inc       word [esi+12h]
  0046aca6  668b4612              mov       ax,[esi+12h]
  0046acaa  663b4614              cmp       ax,[esi+14h]
  0046acae  7e0a                  jle       short 0046ACBAh
  0046acb0  66894e10              mov       [esi+10h],cx
  0046acb4  66c746120500          mov       word [esi+12h],5

LAB_0046acba:
  0046acba  66394e10              cmp       [esi+10h],cx
  0046acbe  bf03000000            mov       edi,3
  0046acc3  7526                  jne       short 0046ACEBh
  0046acc5  668b4612              mov       ax,[esi+12h]
  0046acc9  8b5620                mov       edx,[esi+20h]
  0046accc  0fbfc8                movsx     ecx,ax
  0046accf  c1e10c                shl       ecx,0Ch
  0046acd2  2bd1                  sub       edx,ecx
  0046acd4  40                    inc       eax
  0046acd5  663d6400              cmp       ax,64h
  0046acd9  895620                mov       [esi+20h],edx
  0046acdc  66894612              mov       [esi+12h],ax
  0046ace0  7e04                  jle       short 0046ACE6h
  0046ace2  66897e10              mov       [esi+10h],di

LAB_0046ace6:
  0046ace6  668346340a            add       word [esi+34h],0Ah

LAB_0046aceb:
  0046aceb  66397e10              cmp       [esi+10h],di
  0046acef  ba04000000            mov       edx,4
  0046acf4  753b                  jne       short 0046AD31h
  0046acf6  b938ffffff            mov       ecx,0FFFFFF38h
  0046acfb  66014e28              add       [esi+28h],cx
  0046acff  668b4628              mov       ax,[esi+28h]
  0046ad03  66014e2a              add       [esi+2Ah],cx
  0046ad07  66014e2c              add       [esi+2Ch],cx
  0046ad0b  663bc3                cmp       ax,bx
  0046ad0e  7d21                  jge       short 0046AD31h
  0046ad10  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0046ad17  c1e00c                shl       eax,0Ch
  0046ad1a  66895e28              mov       [esi+28h],bx
  0046ad1e  66895e2a              mov       [esi+2Ah],bx
  0046ad22  66895e2c              mov       [esi+2Ch],bx
  0046ad26  66895610              mov       [esi+10h],dx
  0046ad2a  894620                mov       [esi+20h],eax
  0046ad2d  66895e34              mov       [esi+34h],bx

LAB_0046ad31:
  0046ad31  66395610              cmp       [esi+10h],dx
  0046ad35  752f                  jne       short 0046AD66h
  0046ad37  b9c8000000            mov       ecx,0C8h
  0046ad3c  66014e28              add       [esi+28h],cx
  0046ad40  66014e2a              add       [esi+2Ah],cx
  0046ad44  66014e2c              add       [esi+2Ch],cx
  0046ad48  668b4628              mov       ax,[esi+28h]
  0046ad4c  b900100000            mov       ecx,1000h
  0046ad51  663bc1                cmp       ax,cx
  0046ad54  7e10                  jle       short 0046AD66h
  0046ad56  66894e28              mov       [esi+28h],cx
  0046ad5a  66894e2a              mov       [esi+2Ah],cx
  0046ad5e  66894e2c              mov       [esi+2Ch],cx
  0046ad62  66895e10              mov       [esi+10h],bx

LAB_0046ad66:
  0046ad66  5f                    pop       edi
  0046ad67  5e                    pop       esi
  0046ad68  5b                    pop       ebx
  0046ad69  c3                    ret
  0046ad6a  90                    nop
  0046ad6b  90                    nop
  0046ad6c  90                    nop
  0046ad6d  90                    nop
  0046ad6e  90                    nop
  0046ad6f  90                    nop

6 cible(s) de saut interne : 0x0046ac97, 0x0046acba, 0x0046ace6, 0x0046aceb, 0x0046ad31, 0x0046ad66
