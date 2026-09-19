Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004681a0  section .text  offset fichier 0x675a0  112 octets

  004681a0  8b442404              mov       eax,[esp+4]
  004681a4  f70000000040          test      dword [eax],40000000h
  004681aa  7554                  jne       short 00468200h
  004681ac  b901000000            mov       ecx,1
  004681b1  66394814              cmp       [eax+14h],cx
  004681b5  7506                  jne       short 004681BDh
  004681b7  090dc8175700          or        [5717C8h],ecx

LAB_004681bd:
  004681bd  b902000000            mov       ecx,2
  004681c2  66394814              cmp       [eax+14h],cx
  004681c6  7506                  jne       short 004681CEh
  004681c8  090dc8175700          or        [5717C8h],ecx

LAB_004681ce:
  004681ce  6683781403            cmp       word [eax+14h],3
  004681d3  b904000000            mov       ecx,4
  004681d8  7506                  jne       short 004681E0h
  004681da  090dc8175700          or        [5717C8h],ecx

LAB_004681e0:
  004681e0  66394814              cmp       [eax+14h],cx
  004681e4  7507                  jne       short 004681EDh
  004681e6  830dc817570008        or        dword [5717C8h],8

LAB_004681ed:
  004681ed  6683781405            cmp       word [eax+14h],5
  004681f2  750c                  jne       short 00468200h
  004681f4  a1c8175700            mov       eax,[5717C8h]
  004681f9  0c10                  or        al,10h
  004681fb  a3c8175700            mov       [5717C8h],eax

LAB_00468200:
  00468200  c3                    ret
  00468201  90                    nop
  00468202  90                    nop
  00468203  90                    nop
  00468204  90                    nop
  00468205  90                    nop
  00468206  90                    nop
  00468207  90                    nop
  00468208  90                    nop
  00468209  90                    nop
  0046820a  90                    nop
  0046820b  90                    nop
  0046820c  90                    nop
  0046820d  90                    nop
  0046820e  90                    nop
  0046820f  90                    nop

5 cible(s) de saut interne : 0x004681bd, 0x004681ce, 0x004681e0, 0x004681ed, 0x00468200
