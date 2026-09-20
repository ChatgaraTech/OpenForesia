  00434510  83ec10                sub       esp,10h
  00434513  53                    push      ebx
  00434514  55                    push      ebp
  00434515  56                    push      esi
  00434516  8b742420              mov       esi,[esp+20h]
  0043451a  57                    push      edi
  0043451b  8b4674                mov       eax,[esi+74h]
  0043451e  85c0                  test      eax,eax
  00434520  89442424              mov       [esp+24h],eax
  00434524  7508                  jne       short 0043452Eh
  00434526  c744242458e55d00      mov       dword [esp+24h],5DE558h
  0043452e  66837e1000            cmp       word [esi+10h],0
  00434533  bb01000000            mov       ebx,1
  00434538  bf05000000            mov       edi,5
  0043453d  755a                  jne       short 00434599h
  0043453f  c7463c080e5500        mov       dword [esi+3Ch],550E08h
  00434546  e8f52b1100            call      00547140h
  0043454b  99                    cdq
  0043454c  b903000000            mov       ecx,3
  00434551  c6460a02              mov       byte [esi+0Ah],2
  00434555  f7f9                  idiv      ecx
  00434557  8b4e24                mov       ecx,[esi+24h]
  0043455a  b0b4                  mov       al,0B4h
  0043455c  884638                mov       [esi+38h],al
  0043455f  884639                mov       [esi+39h],al
  00434562  b8dc050000            mov       eax,5DCh
  00434567  c6460b00              mov       byte [esi+0Bh],0
  0043456b  66894628              mov       [esi+28h],ax
  0043456f  6689462a              mov       [esi+2Ah],ax
  00434573  8b4620                mov       eax,[esi+20h]
  00434576  c6463a82              mov       byte [esi+3Ah],82h
  0043457a  894654                mov       [esi+54h],eax
  0043457d  894e58                mov       [esi+58h],ecx
  00434580  66895e10              mov       [esi+10h],bx
  00434584  66c746120000          mov       word [esi+12h],0
  0043458a  66897e14              mov       [esi+14h],di
  0043458e  fec2                  inc       dl
  00434590  885640                mov       [esi+40h],dl
  00434593  8b561c                mov       edx,[esi+1Ch]
  00434596  895650                mov       [esi+50h],edx
  00434599  66395e10              cmp       [esi+10h],bx
  0043459d  0f85c9010000          jne       near 0043476Ch
  004345a3  0fbf5632              movsx     edx,word [esi+32h]
  004345a7  52                    push      edx
  004345a8  e86f141100            call      00545A1Ch
  004345ad  0fbf4e12              movsx     ecx,word [esi+12h]
  004345b1  0fafc1                imul      eax,ecx
  004345b4  0fbf5632              movsx     edx,word [esi+32h]
  004345b8  8b4e50                mov       ecx,[esi+50h]
  004345bb  52                    push      edx
  004345bc  03c1                  add       eax,ecx
  004345be  89461c                mov       [esi+1Ch],eax
  004345c1  e850141100            call      00545A16h
  004345c6  0fbf4e12              movsx     ecx,word [esi+12h]
  004345ca  0fafc1                imul      eax,ecx
  004345cd  8b4e58                mov       ecx,[esi+58h]
  004345d0  6af9                  push      0FFFFFFF9h
  004345d2  03c1                  add       eax,ecx
  004345d4  6af9                  push      0FFFFFFF9h
  004345d6  894624                mov       [esi+24h],eax
  004345d9  8b4620                mov       eax,[esi+20h]
  004345dc  0500a0ffff            add       eax,0FFFFA000h
  004345e1  6af9                  push      0FFFFFFF9h
  004345e3  894620                mov       [esi+20h],eax
  004345e6  b8c8000000            mov       eax,0C8h
  004345eb  66014628              add       [esi+28h],ax
  004345ef  6601462a              add       [esi+2Ah],ax
  004345f3  56                    push      esi
  004345f4  e8e707fdff            call      00404DE0h
  004345f9  8a4638                mov       al,[esi+38h]
  004345fc  83c410                add       esp,10h
  004345ff  84c0                  test      al,al
  00434601  7506                  jne       short 00434609h
  00434603  66c746100200          mov       word [esi+10h],2
  00434609  56                    push      esi
  0043460a  e831f30f00            call      00533940h
  0043460f  8b8690000000          mov       eax,[esi+90h]
  00434615  83c404                add       esp,4
  00434618  a902880000            test      eax,8802h
  0043461d  7406                  je        short 00434625h
  0043461f  66c746100200          mov       word [esi+10h],2
  00434625  8b5660                mov       edx,[esi+60h]
  00434628  8b4620                mov       eax,[esi+20h]
  0043462b  3bd0                  cmp       edx,eax
  0043462d  7c06                  jl        short 00434635h
  0043462f  66c746100200          mov       word [esi+10h],2
  00434635  66ff4e14              dec       word [esi+14h]
  00434639  668b4614              mov       ax,[esi+14h]
  0043463d  6683461214            add       word [esi+12h],14h
  00434642  6685c0                test      ax,ax
  00434645  7f15                  jg        short 0043465Ch
  00434647  8a5640                mov       dl,[esi+40h]
  0043464a  66897e14              mov       [esi+14h],di
  0043464e  fec2                  inc       dl
  00434650  8ac2                  mov       al,dl
  00434652  885640                mov       [esi+40h],dl
  00434655  3c04                  cmp       al,4
  00434657  7503                  jne       short 0043465Ch
  00434659  885e40                mov       [esi+40h],bl
  0043465c  bfc0935e00            mov       edi,5E93C0h
  00434661  c744241404000000      mov       dword [esp+14h],4
  00434669  bb10000000            mov       ebx,10h
  0043466e  8b07                  mov       eax,[edi]
  00434670  a900000080            test      eax,80000000h
  00434675  0f84dc000000          je        near 00434757h
  0043467b  a900000050            test      eax,50000000h
  00434680  0f85d1000000          jne       near 00434757h
  00434686  66837f1200            cmp       word [edi+12h],0
  0043468b  0f85c6000000          jne       near 00434757h
  00434691  8b4f1c                mov       ecx,[edi+1Ch]
  00434694  8b461c                mov       eax,[esi+1Ch]
  00434697  8b5620                mov       edx,[esi+20h]
  0043469a  8b6e24                mov       ebp,[esi+24h]
  0043469d  2bc8                  sub       ecx,eax
  0043469f  8b4720                mov       eax,[edi+20h]
  004346a2  2bc2                  sub       eax,edx
  004346a4  8b5724                mov       edx,[edi+24h]
  004346a7  c1f90c                sar       ecx,0Ch
  004346aa  0500c00300            add       eax,3C000h
  004346af  2bd5                  sub       edx,ebp
  004346b1  8be9                  mov       ebp,ecx
  004346b3  c1f80c                sar       eax,0Ch
  004346b6  0fafe9                imul      ebp,ecx
  004346b9  8bc8                  mov       ecx,eax
  004346bb  c1fa0c                sar       edx,0Ch
  004346be  0fafc8                imul      ecx,eax
  004346c1  8bc2                  mov       eax,edx
  004346c3  03e9                  add       ebp,ecx
  004346c5  0fafc2                imul      eax,edx
  004346c8  03e8                  add       ebp,eax
  004346ca  55                    push      ebp
  004346cb  e840131100            call      00545A10h
  004346d0  6683bf9600000000      cmp       word [edi+96h],0
  004346d8  757d                  jne       short 00434757h
  004346da  83f850                cmp       eax,50h
  004346dd  7d78                  jge       short 00434757h
  004346df  8b4e24                mov       ecx,[esi+24h]
  004346e2  8b5620                mov       edx,[esi+20h]
  004346e5  8b461c                mov       eax,[esi+1Ch]
  004346e8  51                    push      ecx
  004346e9  52                    push      edx
  004346ea  50                    push      eax
  004346eb  57                    push      edi
  004346ec  e8af2e0000            call      004375A0h
  004346f1  83c410                add       esp,10h
  004346f4  6685c0                test      ax,ax
  004346f7  7558                  jne       short 00434751h
  004346f9  8d4c2410              lea       ecx,[esp+10h]
  004346fd  8d542418              lea       edx,[esp+18h]
  00434701  51                    push      ecx
  00434702  8b4c2428              mov       ecx,[esp+28h]
  00434706  8d442420              lea       eax,[esp+20h]
  0043470a  52                    push      edx
  0043470b  50                    push      eax
  0043470c  51                    push      ecx
  0043470d  e86e421000            call      00538980h
  00434712  8b442420              mov       eax,[esp+20h]
  00434716  83c410                add       esp,10h
  00434719  668b6830              mov       bp,[eax+30h]
  0043471d  57                    push      edi
  0043471e  668bd5                mov       dx,bp
  00434721  8bcd                  mov       ecx,ebp
  00434723  66c1fa02              sar       dx,2
  00434727  2bca                  sub       ecx,edx
  00434729  8b542428              mov       edx,[esp+28h]
  0043472d  52                    push      edx
  0043472e  66894830              mov       [eax+30h],cx
  00434732  e849541000            call      00539B80h
  00434737  8b442418              mov       eax,[esp+18h]
  0043473b  83c408                add       esp,8
  0043473e  66896830              mov       [eax+30h],bp
  00434742  66899f96000000        mov       [edi+96h],bx
  00434749  8b07                  mov       eax,[edi]
  0043474b  0c40                  or        al,40h
  0043474d  8907                  mov       [edi],eax
  0043474f  eb06                  jmp       short 00434757h
  00434751  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00434757  8b442414              mov       eax,[esp+14h]
  0043475b  81c79c000000          add       edi,9Ch
  00434761  48                    dec       eax
  00434762  89442414              mov       [esp+14h],eax
  00434766  0f8502ffffff          jne       near 0043466Eh
  0043476c  66837e1002            cmp       word [esi+10h],2
  00434771  7506                  jne       short 00434779h
  00434773  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00434779  5f                    pop       edi
  0043477a  5e                    pop       esi
  0043477b  5d                    pop       ebp
  0043477c  5b                    pop       ebx
  0043477d  83c410                add       esp,10h
  00434780  c3                    ret
