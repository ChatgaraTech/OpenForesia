Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040be50  section .text  offset fichier 0xb250  256 octets

  0040be50  53                    push      ebx
  0040be51  56                    push      esi
  0040be52  8b74240c              mov       esi,[esp+0Ch]
  0040be56  33db                  xor       ebx,ebx
  0040be58  66395e10              cmp       [esi+10h],bx
  0040be5c  0f858f000000          jne       near 0040BEF1h
  0040be62  b800100000            mov       eax,1000h
  0040be67  66c746100100          mov       word [esi+10h],1
  0040be6d  c7463ce8f45400        mov       dword [esi+3Ch],54F4E8h
  0040be74  885e40                mov       [esi+40h],bl
  0040be77  66894628              mov       [esi+28h],ax
  0040be7b  6689462a              mov       [esi+2Ah],ax
  0040be7f  e8bcb21300            call      00547140h
  0040be84  99                    cdq
  0040be85  b90a000000            mov       ecx,0Ah
  0040be8a  f7f9                  idiv      ecx
  0040be8c  83c208                add       edx,8
  0040be8f  66895612              mov       [esi+12h],dx
  0040be93  e8a8b21300            call      00547140h
  0040be98  99                    cdq
  0040be99  b904010000            mov       ecx,104h
  0040be9e  f7f9                  idiv      ecx
  0040bea0  8b461c                mov       eax,[esi+1Ch]
  0040bea3  81ea82000000          sub       edx,82h
  0040bea9  c1e20c                shl       edx,0Ch
  0040beac  03c2                  add       eax,edx
  0040beae  89461c                mov       [esi+1Ch],eax
  0040beb1  e88ab21300            call      00547140h
  0040beb6  99                    cdq
  0040beb7  b904010000            mov       ecx,104h
  0040bebc  c6460a09              mov       byte [esi+0Ah],9
  0040bec0  f7f9                  idiv      ecx
  0040bec2  8b4e24                mov       ecx,[esi+24h]
  0040bec5  885e0b                mov       [esi+0Bh],bl
  0040bec8  66895e14              mov       [esi+14h],bx
  0040becc  81ea82000000          sub       edx,82h
  0040bed2  c1e20c                shl       edx,0Ch
  0040bed5  03ca                  add       ecx,edx
  0040bed7  894e24                mov       [esi+24h],ecx
  0040beda  e861b21300            call      00547140h
  0040bedf  99                    cdq
  0040bee0  b90a000000            mov       ecx,0Ah
  0040bee5  f7f9                  idiv      ecx
  0040bee7  83c20f                add       edx,0Fh
  0040beea  6689969a000000        mov       [esi+9Ah],dx

LAB_0040bef1:
  0040bef1  66837e1001            cmp       word [esi+10h],1
  0040bef6  7547                  jne       short 0040BF3Fh
  0040bef8  0fbf5612              movsx     edx,word [esi+12h]
  0040befc  8b4620                mov       eax,[esi+20h]
  0040beff  c1e20c                shl       edx,0Ch
  0040bf02  03c2                  add       eax,edx
  0040bf04  66ff4614              inc       word [esi+14h]
  0040bf08  894620                mov       [esi+20h],eax
  0040bf0b  668b4614              mov       ax,[esi+14h]
  0040bf0f  663b869a000000        cmp       ax,[esi+9Ah]
  0040bf16  7e27                  jle       short 0040BF3Fh
  0040bf18  b99cffffff            mov       ecx,0FFFFFF9Ch
  0040bf1d  66014e28              add       [esi+28h],cx
  0040bf21  668b4628              mov       ax,[esi+28h]
  0040bf25  66014e2a              add       [esi+2Ah],cx
  0040bf29  663bc3                cmp       ax,bx
  0040bf2c  7d11                  jge       short 0040BF3Fh
  0040bf2e  8b06                  mov       eax,[esi]
  0040bf30  66895e28              mov       [esi+28h],bx
  0040bf34  25ffffff7f            and       eax,7FFFFFFFh
  0040bf39  66895e2a              mov       [esi+2Ah],bx
  0040bf3d  8906                  mov       [esi],eax

LAB_0040bf3f:
  0040bf3f  5e                    pop       esi
  0040bf40  5b                    pop       ebx
  0040bf41  c3                    ret
  0040bf42  90                    nop
  0040bf43  90                    nop
  0040bf44  90                    nop
  0040bf45  90                    nop
  0040bf46  90                    nop
  0040bf47  90                    nop
  0040bf48  90                    nop
  0040bf49  90                    nop
  0040bf4a  90                    nop
  0040bf4b  90                    nop
  0040bf4c  90                    nop
  0040bf4d  90                    nop
  0040bf4e  90                    nop
  0040bf4f  90                    nop

2 cible(s) de saut interne : 0x0040bef1, 0x0040bf3f
