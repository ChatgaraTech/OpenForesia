Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00482300  section .text  offset fichier 0x81700  107 octets

  00482300  8b442404              mov       eax,[esp+4]
  00482304  56                    push      esi
  00482305  57                    push      edi
  00482306  8b10                  mov       edx,[eax]
  00482308  8b4838                mov       ecx,[eax+38h]
  0048230b  f7c200000002          test      edx,2000000h
  00482311  7414                  je        short 00482327h
  00482313  81c1f8f7f7ff          add       ecx,0FFF7F7F8h
  00482319  894838                mov       [eax+38h],ecx
  0048231c  7544                  jne       short 00482362h
  0048231e  c70000000000          mov       dword [eax],0
  00482324  5f                    pop       edi
  00482325  5e                    pop       esi
  00482326  c3                    ret

LAB_00482327:
  00482327  81f980808000          cmp       ecx,808080h
  0048232d  7309                  jae       short 00482338h
  0048232f  81c108080800          add       ecx,80808h
  00482335  894838                mov       [eax+38h],ecx

LAB_00482338:
  00482338  668b4828              mov       cx,[eax+28h]
  0048233c  bfff7f0000            mov       edi,7FFFh
  00482341  0fbff1                movsx     esi,cx
  00482344  2bfe                  sub       edi,esi
  00482346  c1ff03                sar       edi,3
  00482349  03cf                  add       ecx,edi
  0048234b  66894828              mov       [eax+28h],cx
  0048234f  6689482c              mov       [eax+2Ch],cx
  00482353  8b486c                mov       ecx,[eax+6Ch]
  00482356  8b09                  mov       ecx,[ecx]
  00482358  81e100000002          and       ecx,2000000h
  0048235e  0bca                  or        ecx,edx
  00482360  8908                  mov       [eax],ecx

LAB_00482362:
  00482362  668140328000          add       word [eax+32h],80h
  00482368  5f                    pop       edi
  00482369  5e                    pop       esi
  0048236a  c3                    ret

3 cible(s) de saut interne : 0x00482327, 0x00482338, 0x00482362
