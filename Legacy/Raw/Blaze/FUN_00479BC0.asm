Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00479bc0  section .text  offset fichier 0x78fc0  144 octets

  00479bc0  8b442404              mov       eax,[esp+4]
  00479bc4  53                    push      ebx
  00479bc5  55                    push      ebp
  00479bc6  56                    push      esi
  00479bc7  8b706c                mov       esi,[eax+6Ch]
  00479bca  8b5824                mov       ebx,[eax+24h]
  00479bcd  57                    push      edi
  00479bce  8b7820                mov       edi,[eax+20h]
  00479bd1  8b5620                mov       edx,[esi+20h]
  00479bd4  8b4e24                mov       ecx,[esi+24h]
  00479bd7  8b6e1c                mov       ebp,[esi+1Ch]
  00479bda  2bd7                  sub       edx,edi
  00479bdc  c1fa02                sar       edx,2
  00479bdf  03fa                  add       edi,edx
  00479be1  2bcb                  sub       ecx,ebx
  00479be3  8b581c                mov       ebx,[eax+1Ch]
  00479be6  897820                mov       [eax+20h],edi
  00479be9  8b7824                mov       edi,[eax+24h]
  00479bec  2beb                  sub       ebp,ebx
  00479bee  c1f902                sar       ecx,2
  00479bf1  03cf                  add       ecx,edi
  00479bf3  c1fd02                sar       ebp,2
  00479bf6  894824                mov       [eax+24h],ecx
  00479bf9  8b4838                mov       ecx,[eax+38h]
  00479bfc  03eb                  add       ebp,ebx
  00479bfe  81f9e0e0e000          cmp       ecx,0E0E0E0h
  00479c04  89681c                mov       [eax+1Ch],ebp
  00479c07  7309                  jae       short 00479C12h
  00479c09  81c120202000          add       ecx,202020h
  00479c0f  894838                mov       [eax+38h],ecx

LAB_00479c12:
  00479c12  8a5840                mov       bl,[eax+40h]
  00479c15  fec3                  inc       bl
  00479c17  660fb6cb              movzx     cx,bl
  00479c1b  663b481a              cmp       cx,[eax+1Ah]
  00479c1f  885840                mov       [eax+40h],bl
  00479c22  7c04                  jl        short 00479C28h
  00479c24  c6404000              mov       byte [eax+40h],0

LAB_00479c28:
  00479c28  81fa00040000          cmp       edx,400h
  00479c2e  7c08                  jl        short 00479C38h
  00479c30  f70600000040          test      dword [esi],40000000h
  00479c36  7406                  je        short 00479C3Eh

LAB_00479c38:
  00479c38  c70000000000          mov       dword [eax],0

LAB_00479c3e:
  00479c3e  5f                    pop       edi
  00479c3f  5e                    pop       esi
  00479c40  5d                    pop       ebp
  00479c41  5b                    pop       ebx
  00479c42  c3                    ret
  00479c43  90                    nop
  00479c44  90                    nop
  00479c45  90                    nop
  00479c46  90                    nop
  00479c47  90                    nop
  00479c48  90                    nop
  00479c49  90                    nop
  00479c4a  90                    nop
  00479c4b  90                    nop
  00479c4c  90                    nop
  00479c4d  90                    nop
  00479c4e  90                    nop
  00479c4f  90                    nop

4 cible(s) de saut interne : 0x00479c12, 0x00479c28, 0x00479c38, 0x00479c3e
