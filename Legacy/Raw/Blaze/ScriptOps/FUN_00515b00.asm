
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515b00  section .text  offset fichier 0x114f00  96 octets

  00515b00  8b44240c              mov       eax,[esp+0Ch]
  00515b04  660fb64802            movzx     cx,byte [eax+2]
  00515b09  66890d60915e00        mov       [5E9160h],cx
  00515b10  660fb65003            movzx     dx,byte [eax+3]
  00515b15  66891562915e00        mov       [5E9162h],dx
  00515b1c  660fb64804            movzx     cx,byte [eax+4]
  00515b21  66890d64915e00        mov       [5E9164h],cx
  00515b28  660fb65001            movzx     dx,byte [eax+1]
  00515b2d  66891566915e00        mov       [5E9166h],dx
  00515b34  83c005                add       eax,5
  00515b37  c3                    ret
  00515b38  90                    nop
  00515b39  90                    nop
  00515b3a  90                    nop
  00515b3b  90                    nop
  00515b3c  90                    nop
  00515b3d  90                    nop
  00515b3e  90                    nop
  00515b3f  90                    nop
  00515b40  8b442408              mov       eax,[esp+8]
  00515b44  56                    push      esi
  00515b45  8b742410              mov       esi,[esp+10h]
  00515b49  8b08                  mov       ecx,[eax]
  00515b4b  f7c10000c000          test      ecx,0C00000h
  00515b51  7458                  je        short 00515BABh
  00515b53  f7c100008000          test      ecx,800000h
  00515b59  7419                  je        short 00515B74h
  00515b5b  8b15b48760            (bad)
  (octet invalide — la plage demandee deborde probablement du code)
