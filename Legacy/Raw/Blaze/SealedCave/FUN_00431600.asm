  00431600  83ec10                sub       esp,10h
  00431603  53                    push      ebx
  00431604  55                    push      ebp
  00431605  56                    push      esi
  00431606  8b742420              mov       esi,[esp+20h]
  0043160a  57                    push      edi
  0043160b  bb01000000            mov       ebx,1
  00431610  8b4674                mov       eax,[esi+74h]
  00431613  66837e1000            cmp       word [esi+10h],0
  00431618  89442424              mov       [esp+24h],eax
  0043161c  bf05000000            mov       edi,5
  00431621  755a                  jne       short 0043167Dh
  00431623  c7463c080c5500        mov       dword [esi+3Ch],550C08h
  0043162a  e8115b1100            call      00547140h
  0043162f  99                    cdq
  00431630  b903000000            mov       ecx,3
  00431635  c6460a02              mov       byte [esi+0Ah],2
  00431639  f7f9                  idiv      ecx
  0043163b  8b4e24                mov       ecx,[esi+24h]
  0043163e  b0b4                  mov       al,0B4h
  00431640  884638                mov       [esi+38h],al
  00431643  884639                mov       [esi+39h],al
  00431646  b8dc050000            mov       eax,5DCh
  0043164b  c6460b00              mov       byte [esi+0Bh],0
  0043164f  66894628              mov       [esi+28h],ax
  00431653  6689462a              mov       [esi+2Ah],ax
  00431657  8b4620                mov       eax,[esi+20h]
  0043165a  c6463a82              mov       byte [esi+3Ah],82h
  0043165e  894654                mov       [esi+54h],eax
  00431661  894e58                mov       [esi+58h],ecx
  00431664  66895e10              mov       [esi+10h],bx
  00431668  66c746120000          mov       word [esi+12h],0
  0043166e  66897e14              mov       [esi+14h],di
  00431672  fec2                  inc       dl
  00431674  885640                mov       [esi+40h],dl
  00431677  8b561c                mov       edx,[esi+1Ch]
  0043167a  895650                mov       [esi+50h],edx
  0043167d  66395e10              cmp       [esi+10h],bx
  00431681  0f85c9010000          jne       near 00431850h
  00431687  0fbf5632              movsx     edx,word [esi+32h]
  0043168b  52                    push      edx
  0043168c  e88b431100            call      00545A1Ch
  00431691  0fbf4e12              movsx     ecx,word [esi+12h]
  00431695  0fafc1                imul      eax,ecx
  00431698  0fbf5632              movsx     edx,word [esi+32h]
  0043169c  8b4e50                mov       ecx,[esi+50h]
  0043169f  52                    push      edx
  004316a0  03c1                  add       eax,ecx
  004316a2  89461c                mov       [esi+1Ch],eax
  004316a5  e86c431100            call      00545A16h
  004316aa  0fbf4e12              movsx     ecx,word [esi+12h]
  004316ae  0fafc1                imul      eax,ecx
  004316b1  8b4e58                mov       ecx,[esi+58h]
  004316b4  6af9                  push      0FFFFFFF9h
  004316b6  03c1                  add       eax,ecx
  004316b8  6af9                  push      0FFFFFFF9h
  004316ba  894624                mov       [esi+24h],eax
  004316bd  8b4620                mov       eax,[esi+20h]
  004316c0  0500a0ffff            add       eax,0FFFFA000h
  004316c5  6af9                  push      0FFFFFFF9h
  004316c7  894620                mov       [esi+20h],eax
  004316ca  b8c8000000            mov       eax,0C8h
  004316cf  66014628              add       [esi+28h],ax
  004316d3  6601462a              add       [esi+2Ah],ax
  004316d7  56                    push      esi
  004316d8  e80337fdff            call      00404DE0h
  004316dd  8a4638                mov       al,[esi+38h]
  004316e0  83c410                add       esp,10h
  004316e3  84c0                  test      al,al
  004316e5  7506                  jne       short 004316EDh
  004316e7  66c746100200          mov       word [esi+10h],2
  004316ed  56                    push      esi
  004316ee  e84d221000            call      00533940h
  004316f3  8b8690000000          mov       eax,[esi+90h]
  004316f9  83c404                add       esp,4
  004316fc  a902880000            test      eax,8802h
  00431701  7406                  je        short 00431709h
  00431703  66c746100200          mov       word [esi+10h],2
  00431709  8b5660                mov       edx,[esi+60h]
  0043170c  8b4620                mov       eax,[esi+20h]
  0043170f  3bd0                  cmp       edx,eax
  00431711  7c06                  jl        short 00431719h
  00431713  66c746100200          mov       word [esi+10h],2
  00431719  66ff4e14              dec       word [esi+14h]
  0043171d  668b4614              mov       ax,[esi+14h]
  00431721  6683461214            add       word [esi+12h],14h
  00431726  6685c0                test      ax,ax
  00431729  7f15                  jg        short 00431740h
  0043172b  8a5640                mov       dl,[esi+40h]
  0043172e  66897e14              mov       [esi+14h],di
  00431732  fec2                  inc       dl
  00431734  8ac2                  mov       al,dl
  00431736  885640                mov       [esi+40h],dl
  00431739  3c04                  cmp       al,4
  0043173b  7503                  jne       short 00431740h
  0043173d  885e40                mov       [esi+40h],bl
  00431740  bfc0935e00            mov       edi,5E93C0h
  00431745  c744241404000000      mov       dword [esp+14h],4
  0043174d  bb10000000            mov       ebx,10h
  00431752  8b07                  mov       eax,[edi]
  00431754  a900000080            test      eax,80000000h
  00431759  0f84dc000000          je        near 0043183Bh
  0043175f  a900000050            test      eax,50000000h
  00431764  0f85d1000000          jne       near 0043183Bh
  0043176a  66837f1200            cmp       word [edi+12h],0
  0043176f  0f85c6000000          jne       near 0043183Bh
  00431775  8b4f1c                mov       ecx,[edi+1Ch]
  00431778  8b461c                mov       eax,[esi+1Ch]
  0043177b  8b5620                mov       edx,[esi+20h]
  0043177e  8b6e24                mov       ebp,[esi+24h]
  00431781  2bc8                  sub       ecx,eax
  00431783  8b4720                mov       eax,[edi+20h]
  00431786  2bc2                  sub       eax,edx
  00431788  8b5724                mov       edx,[edi+24h]
  0043178b  c1f90c                sar       ecx,0Ch
  0043178e  0500c00300            add       eax,3C000h
  00431793  2bd5                  sub       edx,ebp
  00431795  8be9                  mov       ebp,ecx
  00431797  c1f80c                sar       eax,0Ch
  0043179a  0fafe9                imul      ebp,ecx
  0043179d  8bc8                  mov       ecx,eax
  0043179f  c1fa0c                sar       edx,0Ch
  004317a2  0fafc8                imul      ecx,eax
  004317a5  8bc2                  mov       eax,edx
  004317a7  03e9                  add       ebp,ecx
  004317a9  0fafc2                imul      eax,edx
  004317ac  03e8                  add       ebp,eax
  004317ae  55                    push      ebp
  004317af  e85c421100            call      00545A10h
  004317b4  6683bf9600000000      cmp       word [edi+96h],0
  004317bc  757d                  jne       short 0043183Bh
  004317be  83f850                cmp       eax,50h
  004317c1  7d78                  jge       short 0043183Bh
  004317c3  8b4e24                mov       ecx,[esi+24h]
  004317c6  8b5620                mov       edx,[esi+20h]
  004317c9  8b461c                mov       eax,[esi+1Ch]
  004317cc  51                    push      ecx
  004317cd  52                    push      edx
  004317ce  50                    push      eax
  004317cf  57                    push      edi
  004317d0  e8cb5d0000            call      004375A0h
  004317d5  83c410                add       esp,10h
  004317d8  6685c0                test      ax,ax
  004317db  7558                  jne       short 00431835h
  004317dd  8d4c2410              lea       ecx,[esp+10h]
  004317e1  8d542418              lea       edx,[esp+18h]
  004317e5  51                    push      ecx
  004317e6  8b4c2428              mov       ecx,[esp+28h]
  004317ea  8d442420              lea       eax,[esp+20h]
  004317ee  52                    push      edx
  004317ef  50                    push      eax
  004317f0  51                    push      ecx
  004317f1  e88a711000            call      00538980h
  004317f6  8b442420              mov       eax,[esp+20h]
  004317fa  83c410                add       esp,10h
  004317fd  668b6830              mov       bp,[eax+30h]
  00431801  57                    push      edi
  00431802  668bd5                mov       dx,bp
  00431805  8bcd                  mov       ecx,ebp
  00431807  66c1fa02              sar       dx,2
  0043180b  2bca                  sub       ecx,edx
  0043180d  8b542428              mov       edx,[esp+28h]
  00431811  52                    push      edx
  00431812  66894830              mov       [eax+30h],cx
  00431816  e865831000            call      00539B80h
  0043181b  8b442418              mov       eax,[esp+18h]
  0043181f  83c408                add       esp,8
  00431822  66896830              mov       [eax+30h],bp
  00431826  66899f96000000        mov       [edi+96h],bx
  0043182d  8b07                  mov       eax,[edi]
  0043182f  0c40                  or        al,40h
  00431831  8907                  mov       [edi],eax
  00431833  eb06                  jmp       short 0043183Bh
  00431835  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043183b  8b442414              mov       eax,[esp+14h]
  0043183f  81c79c000000          add       edi,9Ch
  00431845  48                    dec       eax
  00431846  89442414              mov       [esp+14h],eax
  0043184a  0f8502ffffff          jne       near 00431752h
  00431850  66837e1002            cmp       word [esi+10h],2
  00431855  7506                  jne       short 0043185Dh
  00431857  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043185d  5f                    pop       edi
  0043185e  5e                    pop       esi
  0043185f  5d                    pop       ebp
  00431860  5b                    pop       ebx
  00431861  83c410                add       esp,10h
  00431864  c3                    ret
