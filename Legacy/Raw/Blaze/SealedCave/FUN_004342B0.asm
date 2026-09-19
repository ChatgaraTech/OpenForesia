Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004342b0  section .text  offset fichier 0x336b0  288 octets

  004342b0  56                    push      esi
  004342b1  8b742408              mov       esi,[esp+8]
  004342b5  c7463c580d5500        mov       dword [esi+3Ch],550D58h
  004342bc  e87f2e1100            call      00547140h
  004342c1  99                    cdq
  004342c2  33c2                  xor       eax,edx
  004342c4  2bc2                  sub       eax,edx
  004342c6  25ff010000            and       eax,1FFh
  004342cb  33c2                  xor       eax,edx
  004342cd  2bc2                  sub       eax,edx
  004342cf  0500070000            add       eax,700h
  004342d4  66894628              mov       [esi+28h],ax
  004342d8  e8632e1100            call      00547140h
  004342dd  99                    cdq
  004342de  33c2                  xor       eax,edx
  004342e0  c6460a09              mov       byte [esi+0Ah],9
  004342e4  2bc2                  sub       eax,edx
  004342e6  c6460b00              mov       byte [esi+0Bh],0
  004342ea  25ff010000            and       eax,1FFh
  004342ef  33c2                  xor       eax,edx
  004342f1  2bc2                  sub       eax,edx
  004342f3  0500070000            add       eax,700h
  004342f8  66837e1001            cmp       word [esi+10h],1
  004342fd  6689462a              mov       [esi+2Ah],ax
  00434301  8a4616                mov       al,[esi+16h]
  00434304  884640                mov       [esi+40h],al
  00434307  7508                  jne       short 00434311h
  00434309  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043430f  5e                    pop       esi
  00434310  c3                    ret

LAB_00434311:
  00434311  8b4e1c                mov       ecx,[esi+1Ch]
  00434314  8b4650                mov       eax,[esi+50h]
  00434317  8bd1                  mov       edx,ecx
  00434319  57                    push      edi
  0043431a  2bd0                  sub       edx,eax
  0043431c  b8abaaaa2a            mov       eax,2AAAAAABh
  00434321  f7ea                  imul      edx
  00434323  0fbf7e12              movsx     edi,word [esi+12h]
  00434327  8bc2                  mov       eax,edx
  00434329  c1e81f                shr       eax,1Fh
  0043432c  03d0                  add       edx,eax
  0043432e  8b4654                mov       eax,[esi+54h]
  00434331  0fafd7                imul      edx,edi
  00434334  2bca                  sub       ecx,edx
  00434336  894e1c                mov       [esi+1Ch],ecx
  00434339  8b4e20                mov       ecx,[esi+20h]
  0043433c  8bd1                  mov       edx,ecx
  0043433e  2bd0                  sub       edx,eax
  00434340  b8abaaaa2a            mov       eax,2AAAAAABh
  00434345  f7ea                  imul      edx
  00434347  8bc2                  mov       eax,edx
  00434349  c1e81f                shr       eax,1Fh
  0043434c  03d0                  add       edx,eax
  0043434e  8b4658                mov       eax,[esi+58h]
  00434351  0fafd7                imul      edx,edi
  00434354  2bca                  sub       ecx,edx
  00434356  894e20                mov       [esi+20h],ecx
  00434359  8b4e24                mov       ecx,[esi+24h]
  0043435c  8bd1                  mov       edx,ecx
  0043435e  2bd0                  sub       edx,eax
  00434360  b8abaaaa2a            mov       eax,2AAAAAABh
  00434365  f7ea                  imul      edx
  00434367  8bc2                  mov       eax,edx
  00434369  c1e81f                shr       eax,1Fh
  0043436c  03d0                  add       edx,eax
  0043436e  0fafd7                imul      edx,edi
  00434371  2bca                  sub       ecx,edx
  00434373  894e24                mov       [esi+24h],ecx
  00434376  e8c52d1100            call      00547140h
  0043437b  99                    cdq
  0043437c  b91e000000            mov       ecx,1Eh
  00434381  f7f9                  idiv      ecx
  00434383  8b461c                mov       eax,[esi+1Ch]
  00434386  83ea0f                sub       edx,0Fh
  00434389  c1e20c                shl       edx,0Ch
  0043438c  03c2                  add       eax,edx
  0043438e  89461c                mov       [esi+1Ch],eax
  00434391  e8aa2d1100            call      00547140h
  00434396  99                    cdq
  00434397  b928000000            mov       ecx,28h
  0043439c  f7f9                  idiv      ecx
  0043439e  8b4620                mov       eax,[esi+20h]
  004343a1  83ea14                sub       edx,14h
  004343a4  c1e20c                shl       edx,0Ch
  004343a7  03c2                  add       eax,edx
  004343a9  894620                mov       [esi+20h],eax
  004343ac  e88f2d1100            call      00547140h
  004343b1  99                    cdq
  004343b2  b91e000000            mov       ecx,1Eh
  004343b7  66c746100100          mov       word [esi+10h],1
  004343bd  f7f9                  idiv      ecx
  004343bf  8b4624                mov       eax,[esi+24h]
  004343c2  5f                    pop       edi
  004343c3  83ea0f                sub       edx,0Fh
  004343c6  c1e20c                shl       edx,0Ch
  004343c9  03c2                  add       eax,edx
  004343cb  894624                mov       [esi+24h],eax
  004343ce  5e                    pop       esi
  004343cf  c3                    ret

1 cible(s) de saut interne : 0x00434311
