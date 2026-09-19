Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004406b0  section .text  offset fichier 0x3fab0  256 octets

  004406b0  56                    push      esi
  004406b1  8b742408              mov       esi,[esp+8]
  004406b5  66837e1000            cmp       word [esi+10h],0
  004406ba  0f85a6000000          jne       near 00440766h
  004406c0  c7463cf8145500        mov       dword [esi+3Ch],5514F8h
  004406c7  c6464000              mov       byte [esi+40h],0
  004406cb  c6460a0c              mov       byte [esi+0Ah],0Ch
  004406cf  c6460b00              mov       byte [esi+0Bh],0
  004406d3  66c746100100          mov       word [esi+10h],1
  004406d9  e8626a1000            call      00547140h
  004406de  99                    cdq
  004406df  b90a000000            mov       ecx,0Ah
  004406e4  f7f9                  idiv      ecx
  004406e6  83c205                add       edx,5
  004406e9  66895612              mov       [esi+12h],dx
  004406ed  e84e6a1000            call      00547140h
  004406f2  99                    cdq
  004406f3  33c2                  xor       eax,edx
  004406f5  2bc2                  sub       eax,edx
  004406f7  25ff030000            and       eax,3FFh
  004406fc  33c2                  xor       eax,edx
  004406fe  2bc2                  sub       eax,edx
  00440700  0500080000            add       eax,800h
  00440705  66894628              mov       [esi+28h],ax
  00440709  6689462a              mov       [esi+2Ah],ax
  0044070d  b0b4                  mov       al,0B4h
  0044070f  884638                mov       [esi+38h],al
  00440712  884639                mov       [esi+39h],al
  00440715  88463a                mov       [esi+3Ah],al
  00440718  e8236a1000            call      00547140h
  0044071d  99                    cdq
  0044071e  b92c010000            mov       ecx,12Ch
  00440723  f7f9                  idiv      ecx
  00440725  8b461c                mov       eax,[esi+1Ch]
  00440728  81ea96000000          sub       edx,96h
  0044072e  c1e20c                shl       edx,0Ch
  00440731  03c2                  add       eax,edx
  00440733  89461c                mov       [esi+1Ch],eax
  00440736  e8056a1000            call      00547140h
  0044073b  99                    cdq
  0044073c  b92c010000            mov       ecx,12Ch
  00440741  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  00440747  f7f9                  idiv      ecx
  00440749  8b4e24                mov       ecx,[esi+24h]
  0044074c  81ea96000000          sub       edx,96h
  00440752  c1e20c                shl       edx,0Ch
  00440755  03ca                  add       ecx,edx
  00440757  8b5664                mov       edx,[esi+64h]
  0044075a  81c200200300          add       edx,32000h
  00440760  894e24                mov       [esi+24h],ecx
  00440763  895620                mov       [esi+20h],edx

LAB_00440766:
  00440766  66837e1001            cmp       word [esi+10h],1
  0044076b  7533                  jne       short 004407A0h
  0044076d  668b4612              mov       ax,[esi+12h]
  00440771  8b5620                mov       edx,[esi+20h]
  00440774  0fbfc8                movsx     ecx,ax
  00440777  c1e10c                shl       ecx,0Ch
  0044077a  6afd                  push      0FFFFFFFDh
  0044077c  03d1                  add       edx,ecx
  0044077e  6afd                  push      0FFFFFFFDh
  00440780  40                    inc       eax
  00440781  6afd                  push      0FFFFFFFDh
  00440783  56                    push      esi
  00440784  895620                mov       [esi+20h],edx
  00440787  66894612              mov       [esi+12h],ax
  0044078b  e85046fcff            call      00404DE0h
  00440790  8a4638                mov       al,[esi+38h]
  00440793  83c410                add       esp,10h
  00440796  84c0                  test      al,al
  00440798  7506                  jne       short 004407A0h
  0044079a  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004407a0:
  004407a0  5e                    pop       esi
  004407a1  c3                    ret
  004407a2  90                    nop
  004407a3  90                    nop
  004407a4  90                    nop
  004407a5  90                    nop
  004407a6  90                    nop
  004407a7  90                    nop
  004407a8  90                    nop
  004407a9  90                    nop
  004407aa  90                    nop
  004407ab  90                    nop
  004407ac  90                    nop
  004407ad  90                    nop
  004407ae  90                    nop
  004407af  90                    nop

2 cible(s) de saut interne : 0x00440766, 0x004407a0
