
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047fb10  section .text  offset fichier 0x7ef10  112 octets

  0047fb10  8b442404              mov       eax,[esp+4]
  0047fb14  56                    push      esi
  0047fb15  57                    push      edi
  0047fb16  8b4870                mov       ecx,[eax+70h]
  0047fb19  8b30                  mov       esi,[eax]
  0047fb1b  8b11                  mov       edx,[ecx]
  0047fb1d  81e200000002          and       edx,2000000h
  0047fb23  0bf2                  or        esi,edx
  0047fb25  8bd6                  mov       edx,esi
  0047fb27  8930                  mov       [eax],esi
  0047fb29  f7c200000002          test      edx,2000000h
  0047fb2f  7409                  je        short 0047FB3Ah
  0047fb31  c70000000000          mov       dword [eax],0
  0047fb37  5f                    pop       edi
  0047fb38  5e                    pop       esi
  0047fb39  c3                    ret

LAB_0047fb3a:
  0047fb3a  668b5028              mov       dx,[eax+28h]
  0047fb3e  bf00100000            mov       edi,1000h
  0047fb43  0fbff2                movsx     esi,dx
  0047fb46  2bfe                  sub       edi,esi
  0047fb48  c1ff03                sar       edi,3
  0047fb4b  03d7                  add       edx,edi
  0047fb4d  66895028              mov       [eax+28h],dx
  0047fb51  6689502a              mov       [eax+2Ah],dx
  0047fb55  8b511c                mov       edx,[ecx+1Ch]
  0047fb58  89501c                mov       [eax+1Ch],edx
  0047fb5b  8b5120                mov       edx,[ecx+20h]
  0047fb5e  895020                mov       [eax+20h],edx
  0047fb61  8a5040                mov       dl,[eax+40h]
  0047fb64  8b4924                mov       ecx,[ecx+24h]
  0047fb67  fec2                  inc       dl
  0047fb69  885040                mov       [eax+40h],dl
  0047fb6c  894824                mov       [eax+24h],ecx
  0047fb6f  660fb6d2              movzx     dx,dl
  0047fb73  663b501a              cmp       dx,[eax+1Ah]
  0047fb77  7c04                  jl        short 0047FB7Dh
  0047fb79  c6404000              mov       byte [eax+40h],0

LAB_0047fb7d:
  0047fb7d  5f                    pop       edi
  0047fb7e  5e                    pop       esi
  0047fb7f  c3                    ret

2 cible(s) de saut interne : 0x0047fb3a, 0x0047fb7d
