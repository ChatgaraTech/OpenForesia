
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00403a90  section .text  offset fichier 0x2e90  374 octets

  00403a90  53                    push      ebx
  00403a91  56                    push      esi
  00403a92  8b74240c              mov       esi,[esp+0Ch]
  00403a96  33db                  xor       ebx,ebx
  00403a98  57                    push      edi
  00403a99  bf04000000            mov       edi,4
  00403a9e  66395e10              cmp       [esi+10h],bx
  00403aa2  0f85b5000000          jne       near 00403B5Dh
  00403aa8  8b4660                mov       eax,[esi+60h]
  00403aab  885e04                mov       [esi+4],bl
  00403aae  c6460a03              mov       byte [esi+0Ah],3
  00403ab2  c6460b01              mov       byte [esi+0Bh],1
  00403ab6  66c746100100          mov       word [esi+10h],1
  00403abc  894620                mov       [esi+20h],eax
  00403abf  e87c361400            call      00547140h
  00403ac4  99                    cdq
  00403ac5  b9d0070000            mov       ecx,7D0h
  00403aca  f7f9                  idiv      ecx
  00403acc  81c290010000          add       edx,190h
  00403ad2  66895628              mov       [esi+28h],dx
  00403ad6  e865361400            call      00547140h
  00403adb  99                    cdq
  00403adc  b9e8030000            mov       ecx,3E8h
  00403ae1  66c7462a606d          mov       word [esi+2Ah],6D60h
  00403ae7  f7f9                  idiv      ecx
  00403ae9  81c290010000          add       edx,190h
  00403aef  6689562c              mov       [esi+2Ch],dx
  00403af3  e848361400            call      00547140h
  00403af8  99                    cdq
  00403af9  33c2                  xor       eax,edx
  00403afb  2bc2                  sub       eax,edx
  00403afd  25ff0f0000            and       eax,0FFFh
  00403b02  33c2                  xor       eax,edx
  00403b04  2bc2                  sub       eax,edx
  00403b06  66894632              mov       [esi+32h],ax
  00403b0a  e831361400            call      00547140h
  00403b0f  99                    cdq
  00403b10  b90a000000            mov       ecx,0Ah
  00403b15  66097e36              or        [esi+36h],di
  00403b19  f7f9                  idiv      ecx
  00403b1b  66895e16              mov       [esi+16h],bx
  00403b1f  885e38                mov       [esi+38h],bl
  00403b22  885e39                mov       [esi+39h],bl
  00403b25  885e3a                mov       [esi+3Ah],bl
  00403b28  897e50                mov       [esi+50h],edi
  00403b2b  66895612              mov       [esi+12h],dx
  00403b2f  e80c361400            call      00547140h
  00403b34  99                    cdq
  00403b35  33c2                  xor       eax,edx
  00403b37  2bc2                  sub       eax,edx
  00403b39  83e003                and       eax,3
  00403b3c  33c2                  xor       eax,edx
  00403b3e  2bc2                  sub       eax,edx
  00403b40  83c006                add       eax,6
  00403b43  894654                mov       [esi+54h],eax
  00403b46  e8f5351400            call      00547140h
  00403b4b  99                    cdq
  00403b4c  33c2                  xor       eax,edx
  00403b4e  2bc2                  sub       eax,edx
  00403b50  83e003                and       eax,3
  00403b53  33c2                  xor       eax,edx
  00403b55  2bc2                  sub       eax,edx
  00403b57  83c006                add       eax,6
  00403b5a  894658                mov       [esi+58h],eax

LAB_00403b5d:
  00403b5d  66837e1001            cmp       word [esi+10h],1
  00403b62  b802000000            mov       eax,2
  00403b67  750e                  jne       short 00403B77h
  00403b69  66ff4e12              dec       word [esi+12h]
  00403b6d  66395e12              cmp       [esi+12h],bx
  00403b71  7f04                  jg        short 00403B77h
  00403b73  66894610              mov       [esi+10h],ax

LAB_00403b77:
  00403b77  66394610              cmp       [esi+10h],ax
  00403b7b  7525                  jne       short 00403BA2h
  00403b7d  668b5658              mov       dx,[esi+58h]
  00403b81  668b4654              mov       ax,[esi+54h]
  00403b85  668b4e50              mov       cx,[esi+50h]
  00403b89  52                    push      edx
  00403b8a  50                    push      eax
  00403b8b  51                    push      ecx
  00403b8c  56                    push      esi
  00403b8d  e84e120000            call      00404DE0h
  00403b92  8a4639                mov       al,[esi+39h]
  00403b95  83c410                add       esp,10h
  00403b98  3c6e                  cmp       al,6Eh
  00403b9a  7606                  jbe       short 00403BA2h
  00403b9c  66c746100300          mov       word [esi+10h],3

LAB_00403ba2:
  00403ba2  66837e1003            cmp       word [esi+10h],3
  00403ba7  7518                  jne       short 00403BC1h
  00403ba9  e892351400            call      00547140h
  00403bae  99                    cdq
  00403baf  b90a000000            mov       ecx,0Ah
  00403bb4  66897e10              mov       [esi+10h],di
  00403bb8  f7f9                  idiv      ecx
  00403bba  83c205                add       edx,5
  00403bbd  66895612              mov       [esi+12h],dx

LAB_00403bc1:
  00403bc1  66397e10              cmp       [esi+10h],di
  00403bc5  753b                  jne       short 00403C02h
  00403bc7  66ff4e12              dec       word [esi+12h]
  00403bcb  66395e12              cmp       [esi+12h],bx
  00403bcf  7d31                  jge       short 00403C02h
  00403bd1  6afa                  push      0FFFFFFFAh
  00403bd3  6afd                  push      0FFFFFFFDh
  00403bd5  6afa                  push      0FFFFFFFAh
  00403bd7  56                    push      esi
  00403bd8  66895e12              mov       [esi+12h],bx
  00403bdc  e8ff110000            call      00404DE0h
  00403be1  8a4639                mov       al,[esi+39h]
  00403be4  83c410                add       esp,10h
  00403be7  3c3c                  cmp       al,3Ch
  00403be9  730d                  jae       short 00403BF8h
  00403beb  b928000000            mov       ecx,28h
  00403bf0  66014e28              add       [esi+28h],cx
  00403bf4  66014e2c              add       [esi+2Ch],cx

LAB_00403bf8:
  00403bf8  3ac3                  cmp       al,bl
  00403bfa  7506                  jne       short 00403C02h
  00403bfc  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00403c02:
  00403c02  5f                    pop       edi
  00403c03  5e                    pop       esi
  00403c04  5b                    pop       ebx
  00403c05  c3                    ret

6 cible(s) de saut interne : 0x00403b5d, 0x00403b77, 0x00403ba2, 0x00403bc1, 0x00403bf8, 0x00403c02


