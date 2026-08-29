Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00404310  section .text  offset fichier 0x3710  322 octets

  00404310  53                    push      ebx
  00404311  56                    push      esi
  00404312  8b74240c              mov       esi,[esp+0Ch]
  00404316  33db                  xor       ebx,ebx
  00404318  57                    push      edi
  00404319  66395e10              cmp       [esi+10h],bx
  0040431d  0f85e3000000          jne       near 00404406h
  00404323  0fbf7e14              movsx     edi,word [esi+14h]
  00404327  66c746100100          mov       word [esi+10h],1
  0040432d  c7463ce0f05400        mov       dword [esi+3Ch],54F0E0h
  00404334  885e40                mov       [esi+40h],bl
  00404337  c6460a02              mov       byte [esi+0Ah],2
  0040433b  885e0b                mov       [esi+0Bh],bl
  0040433e  e8fd2d1400            call      00547140h
  00404343  8bc8                  mov       ecx,eax
  00404345  8bc7                  mov       eax,edi
  00404347  99                    cdq
  00404348  894c2410              mov       [esp+10h],ecx
  0040434c  2bc2                  sub       eax,edx
  0040434e  8b542410              mov       edx,[esp+10h]
  00404352  8bc8                  mov       ecx,eax
  00404354  8bc2                  mov       eax,edx
  00404356  99                    cdq
  00404357  d1f9                  sar       ecx,1
  00404359  f7f9                  idiv      ecx
  0040435b  8bc7                  mov       eax,edi
  0040435d  0fbf7e14              movsx     edi,word [esi+14h]
  00404361  8bca                  mov       ecx,edx
  00404363  99                    cdq
  00404364  83e203                and       edx,3
  00404367  03c2                  add       eax,edx
  00404369  8b561c                mov       edx,[esi+1Ch]
  0040436c  c1f802                sar       eax,2
  0040436f  2bc8                  sub       ecx,eax
  00404371  c1e10c                shl       ecx,0Ch
  00404374  03d1                  add       edx,ecx
  00404376  89561c                mov       [esi+1Ch],edx
  00404379  e8c22d1400            call      00547140h
  0040437e  8bc8                  mov       ecx,eax
  00404380  8bc7                  mov       eax,edi
  00404382  99                    cdq
  00404383  894c2410              mov       [esp+10h],ecx
  00404387  2bc2                  sub       eax,edx
  00404389  8b542410              mov       edx,[esp+10h]
  0040438d  8bc8                  mov       ecx,eax
  0040438f  8bc2                  mov       eax,edx
  00404391  99                    cdq
  00404392  d1f9                  sar       ecx,1
  00404394  f7f9                  idiv      ecx
  00404396  8bc7                  mov       eax,edi
  00404398  8bca                  mov       ecx,edx
  0040439a  99                    cdq
  0040439b  83e203                and       edx,3
  0040439e  03c2                  add       eax,edx
  004043a0  c1f802                sar       eax,2
  004043a3  2bc8                  sub       ecx,eax
  004043a5  8b4624                mov       eax,[esi+24h]
  004043a8  c1e10c                shl       ecx,0Ch
  004043ab  03c1                  add       eax,ecx
  004043ad  894624                mov       [esi+24h],eax
  004043b0  e88b2d1400            call      00547140h
  004043b5  8bc8                  mov       ecx,eax
  004043b7  66895e12              mov       [esi+12h],bx
  004043bb  0fbf4614              movsx     eax,word [esi+14h]
  004043bf  99                    cdq
  004043c0  2bc2                  sub       eax,edx
  004043c2  8bf8                  mov       edi,eax
  004043c4  8bc1                  mov       eax,ecx
  004043c6  99                    cdq
  004043c7  d1ff                  sar       edi,1
  004043c9  f7ff                  idiv      edi
  004043cb  8b4620                mov       eax,[esi+20h]
  004043ce  c1e20c                shl       edx,0Ch
  004043d1  03c2                  add       eax,edx
  004043d3  894620                mov       [esi+20h],eax
  004043d6  b8b80b0000            mov       eax,0BB8h
  004043db  66894628              mov       [esi+28h],ax
  004043df  6689462a              mov       [esi+2Ah],ax
  004043e3  b0a0                  mov       al,0A0h
  004043e5  884638                mov       [esi+38h],al
  004043e8  884639                mov       [esi+39h],al
  004043eb  88463a                mov       [esi+3Ah],al
  004043ee  e84d2d1400            call      00547140h
  004043f3  99                    cdq
  004043f4  33c2                  xor       eax,edx
  004043f6  2bc2                  sub       eax,edx
  004043f8  83e001                and       eax,1
  004043fb  33c2                  xor       eax,edx
  004043fd  2bc2                  sub       eax,edx
  004043ff  83c002                add       eax,2
  00404402  66894616              mov       [esi+16h],ax

LAB_00404406:
  00404406  66837e1001            cmp       word [esi+10h],1
  0040440b  7541                  jne       short 0040444Eh
  0040440d  6afe                  push      0FFFFFFFEh
  0040440f  6afe                  push      0FFFFFFFEh
  00404411  6afe                  push      0FFFFFFFEh
  00404413  56                    push      esi
  00404414  e8c7090000            call      00404DE0h
  00404419  8b5620                mov       edx,[esi+20h]
  0040441c  83c410                add       esp,10h
  0040441f  66ff4612              inc       word [esi+12h]
  00404423  668b4612              mov       ax,[esi+12h]
  00404427  81c200800000          add       edx,8000h
  0040442d  663b4616              cmp       ax,[esi+16h]
  00404431  895620                mov       [esi+20h],edx
  00404434  7c18                  jl        short 0040444Eh
  00404436  8a4e40                mov       cl,[esi+40h]
  00404439  66895e12              mov       [esi+12h],bx
  0040443d  fec1                  inc       cl
  0040443f  8ac1                  mov       al,cl
  00404441  884e40                mov       [esi+40h],cl
  00404444  3c07                  cmp       al,7
  00404446  7506                  jne       short 0040444Eh
  00404448  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040444e:
  0040444e  5f                    pop       edi
  0040444f  5e                    pop       esi
  00404450  5b                    pop       ebx
  00404451  c3                    ret

2 cible(s) de saut interne : 0x00404406, 0x0040444e
