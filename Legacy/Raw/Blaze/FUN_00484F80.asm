Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484f80  section .text  offset fichier 0x84380  320 octets

  00484f80  83ec20                sub       esp,20h
  00484f83  53                    push      ebx
  00484f84  56                    push      esi
  00484f85  8b74242c              mov       esi,[esp+2Ch]
  00484f89  57                    push      edi
  00484f8a  c7442418101b5500      mov       dword [esp+18h],551B10h
  00484f92  c744241c181b5500      mov       dword [esp+1Ch],551B18h
  00484f9a  66837e1000            cmp       word [esi+10h],0
  00484f9f  8b7e6c                mov       edi,[esi+6Ch]
  00484fa2  c7442420201b5500      mov       dword [esp+20h],551B20h
  00484faa  c7442424b81b5500      mov       dword [esp+24h],551BB8h
  00484fb2  c7442428a81b5500      mov       dword [esp+28h],551BA8h
  00484fba  753f                  jne       short 00484FFBh
  00484fbc  8b4644                mov       eax,[esi+44h]
  00484fbf  8b4e4c                mov       ecx,[esi+4Ch]
  00484fc2  668b969a000000        mov       dx,[esi+9Ah]
  00484fc9  897e74                mov       [esi+74h],edi
  00484fcc  f7d8                  neg       eax
  00484fce  f7d9                  neg       ecx
  00484fd0  66015632              add       [esi+32h],dx
  00484fd4  897e70                mov       [esi+70h],edi
  00484fd7  66c746360400          mov       word [esi+36h],4
  00484fdd  c646041e              mov       byte [esi+4],1Eh
  00484fe1  c7462800040008        mov       dword [esi+28h],8000400h
  00484fe8  66c7462c0004          mov       word [esi+2Ch],400h
  00484fee  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00484ff5  894644                mov       [esi+44h],eax
  00484ff8  894e4c                mov       [esi+4Ch],ecx

LAB_00484ffb:
  00484ffb  8d44240c              lea       eax,[esp+0Ch]
  00484fff  8d4e44                lea       ecx,[esi+44h]
  00485002  50                    push      eax
  00485003  8d5730                lea       edx,[edi+30h]
  00485006  51                    push      ecx
  00485007  52                    push      edx
  00485008  e883100b00            call      00536090h
  0048500d  8b471c                mov       eax,[edi+1Ch]
  00485010  8b4c2418              mov       ecx,[esp+18h]
  00485014  8b54241c              mov       edx,[esp+1Ch]
  00485018  8d5e1c                lea       ebx,[esi+1Ch]
  0048501b  03c1                  add       eax,ecx
  0048501d  83c40c                add       esp,0Ch
  00485020  8903                  mov       [ebx],eax
  00485022  8b4f20                mov       ecx,[edi+20h]
  00485025  8b442414              mov       eax,[esp+14h]
  00485029  03ca                  add       ecx,edx
  0048502b  894e20                mov       [esi+20h],ecx
  0048502e  8b5724                mov       edx,[edi+24h]
  00485031  0fbf4e34              movsx     ecx,word [esi+34h]
  00485035  03d0                  add       edx,eax
  00485037  8b465c                mov       eax,[esi+5Ch]
  0048503a  895624                mov       [esi+24h],edx
  0048503d  50                    push      eax
  0048503e  0fbf5732              movsx     edx,word [edi+32h]
  00485042  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00485049  51                    push      ecx
  0048504a  03d0                  add       edx,eax
  0048504c  8d4e50                lea       ecx,[esi+50h]
  0048504f  52                    push      edx
  00485050  51                    push      ecx
  00485051  e87ada0a00            call      00532AD0h
  00485056  0fbf7e34              movsx     edi,word [esi+34h]
  0048505a  66c746340000          mov       word [esi+34h],0
  00485060  8b15cc975e00          mov       edx,[5E97CCh]
  00485066  83c410                add       esp,10h
  00485069  8b82d4000000          mov       eax,[edx+0D4h]
  0048506f  33d2                  xor       edx,edx
  00485071  6a01                  push      1
  00485073  6a00                  push      0
  00485075  8b08                  mov       ecx,[eax]
  00485077  8a510c                mov       dl,[ecx+0Ch]
  0048507a  81c2c0000000          add       edx,0C0h
  00485080  52                    push      edx
  00485081  e8ba200c00            call      00547140h
  00485086  99                    cdq
  00485087  b905000000            mov       ecx,5
  0048508c  f7f9                  idiv      ecx
  0048508e  8b549424              mov       edx,[esp+edx*4+24h]
  00485092  52                    push      edx
  00485093  53                    push      ebx
  00485094  56                    push      esi
  00485095  e8a65bffff            call      0047AC40h
  0048509a  83c418                add       esp,18h
  0048509d  66ff4610              inc       word [esi+10h]
  004850a1  66837e1014            cmp       word [esi+10h],14h
  004850a6  66897e34              mov       [esi+34h],di
  004850aa  7c06                  jl        short 004850B2h
  004850ac  c70600000000          mov       dword [esi],0

LAB_004850b2:
  004850b2  5f                    pop       edi
  004850b3  5e                    pop       esi
  004850b4  5b                    pop       ebx
  004850b5  83c420                add       esp,20h
  004850b8  c3                    ret
  004850b9  90                    nop
  004850ba  90                    nop
  004850bb  90                    nop
  004850bc  90                    nop
  004850bd  90                    nop
  004850be  90                    nop
  004850bf  90                    nop

2 cible(s) de saut interne : 0x00484ffb, 0x004850b2
