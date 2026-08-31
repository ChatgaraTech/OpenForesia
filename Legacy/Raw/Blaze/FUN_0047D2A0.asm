
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047d2a0  section .text  offset fichier 0x7c6a0  143 octets

  0047d2a0  8b442404              mov       eax,[esp+4]
  0047d2a4  56                    push      esi
  0047d2a5  57                    push      edi
  0047d2a6  668b5010              mov       dx,[eax+10h]
  0047d2aa  6685d2                test      dx,dx
  0047d2ad  750d                  jne       short 0047D2BCh
  0047d2af  8b4820                mov       ecx,[eax+20h]
  0047d2b2  c7402800000000        mov       dword [eax+28h],0
  0047d2b9  894848                mov       [eax+48h],ecx

LAB_0047d2bc:
  0047d2bc  6683fa08              cmp       dx,8
  0047d2c0  7d1d                  jge       short 0047D2DFh
  0047d2c2  668b4828              mov       cx,[eax+28h]
  0047d2c6  bf00300000            mov       edi,3000h
  0047d2cb  0fbff1                movsx     esi,cx
  0047d2ce  2bfe                  sub       edi,esi
  0047d2d0  c1ff02                sar       edi,2
  0047d2d3  03cf                  add       ecx,edi
  0047d2d5  66894828              mov       [eax+28h],cx
  0047d2d9  6689482a              mov       [eax+2Ah],cx
  0047d2dd  eb23                  jmp       short 0047D302h

LAB_0047d2df:
  0047d2df  6683fa10              cmp       dx,10h
  0047d2e3  7d17                  jge       short 0047D2FCh
  0047d2e5  668b4828              mov       cx,[eax+28h]
  0047d2e9  668bf1                mov       si,cx
  0047d2ec  66c1fe02              sar       si,2
  0047d2f0  2bce                  sub       ecx,esi
  0047d2f2  66894828              mov       [eax+28h],cx
  0047d2f6  6689482a              mov       [eax+2Ah],cx
  0047d2fa  eb06                  jmp       short 0047D302h

LAB_0047d2fc:
  0047d2fc  c70000000000          mov       dword [eax],0

LAB_0047d302:
  0047d302  0fbf4828              movsx     ecx,word [eax+28h]
  0047d306  8b7048                mov       esi,[eax+48h]
  0047d309  c1e106                shl       ecx,6
  0047d30c  03ce                  add       ecx,esi
  0047d30e  894820                mov       [eax+20h],ecx
  0047d311  8a4840                mov       cl,[eax+40h]
  0047d314  fec1                  inc       cl
  0047d316  884840                mov       [eax+40h],cl
  0047d319  660fb6c9              movzx     cx,cl
  0047d31d  663b481a              cmp       cx,[eax+1Ah]
  0047d321  7c04                  jl        short 0047D327h
  0047d323  c6404000              mov       byte [eax+40h],0

LAB_0047d327:
  0047d327  42                    inc       edx
  0047d328  5f                    pop       edi
  0047d329  66895010              mov       [eax+10h],dx
  0047d32d  5e                    pop       esi
  0047d32e  c3                    ret

5 cible(s) de saut interne : 0x0047d2bc, 0x0047d2df, 0x0047d2fc, 0x0047d302, 0x0047d327
