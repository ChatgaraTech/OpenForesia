
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004757a0  section .text  offset fichier 0x74ba0  288 octets

  004757a0  83ec08                sub       esp,8
  004757a3  b90f000000            mov       ecx,0Fh
  004757a8  56                    push      esi
  004757a9  8b742410              mov       esi,[esp+10h]
  004757ad  0fbf4610              movsx     eax,word [esi+10h]
  004757b1  99                    cdq
  004757b2  f7f9                  idiv      ecx
  004757b4  83fa01                cmp       edx,1
  004757b7  7518                  jne       short 004757D1h
  004757b9  8d561c                lea       edx,[esi+1Ch]
  004757bc  52                    push      edx
  004757bd  6800120000            push      1200h
  004757c2  6800010000            push      100h
  004757c7  6a17                  push      17h
  004757c9  e852830c00            call      0053DB20h
  004757ce  83c410                add       esp,10h

LAB_004757d1:
  004757d1  668b4e10              mov       cx,[esi+10h]
  004757d5  8b4678                mov       eax,[esi+78h]
  004757d8  8ad1                  mov       dl,cl
  004757da  d0fa                  sar       dl,1
  004757dc  80e203                and       dl,3
  004757df  885640                mov       [esi+40h],dl
  004757e2  8b501c                mov       edx,[eax+1Ch]
  004757e5  89561c                mov       [esi+1Ch],edx
  004757e8  8b5020                mov       edx,[eax+20h]
  004757eb  0fbfc9                movsx     ecx,cx
  004757ee  81c200000500          add       edx,50000h
  004757f4  895620                mov       [esi+20h],edx
  004757f7  8b4024                mov       eax,[eax+24h]
  004757fa  c1e105                shl       ecx,5
  004757fd  51                    push      ecx
  004757fe  894624                mov       [esi+24h],eax
  00475801  e816020d00            call      00545A1Ch
  00475806  8bd0                  mov       edx,eax
  00475808  c1e208                shl       edx,8
  0047580b  2bd0                  sub       edx,eax
  0047580d  8bc2                  mov       eax,edx
  0047580f  0fbf5610              movsx     edx,word [esi+10h]
  00475813  c1f80c                sar       eax,0Ch
  00475816  8bc8                  mov       ecx,eax
  00475818  c1e108                shl       ecx,8
  0047581b  0bc8                  or        ecx,eax
  0047581d  c1e108                shl       ecx,8
  00475820  c1e205                shl       edx,5
  00475823  0bc8                  or        ecx,eax
  00475825  52                    push      edx
  00475826  894e38                mov       [esi+38h],ecx
  00475829  e8ee010d00            call      00545A1Ch
  0047582e  d1e0                  shl       eax,1
  00475830  8bc8                  mov       ecx,eax
  00475832  c1e110                shl       ecx,10h
  00475835  0bc8                  or        ecx,eax
  00475837  66837e1040            cmp       word [esi+10h],40h
  0047583c  894e28                mov       [esi+28h],ecx
  0047583f  7c74                  jl        short 004758B5h
  00475841  6a27                  push      27h
  00475843  6820de5e00            push      5EDE20h
  00475848  56                    push      esi
  00475849  c70600004080          mov       dword [esi],80400000h
  0047584f  e88cfa0b00            call      005352E0h
  00475854  83c40c                add       esp,0Ch
  00475857  8d542410              lea       edx,[esp+10h]
  0047585b  8d442404              lea       eax,[esp+4]
  0047585f  8d4c2408              lea       ecx,[esp+8]
  00475863  52                    push      edx
  00475864  50                    push      eax
  00475865  51                    push      ecx
  00475866  6820de5e00            push      5EDE20h
  0047586b  c60528de5e0020        mov       byte [5EDE28h],20h
  00475872  e809310c00            call      00538980h
  00475877  8b542420              mov       edx,[esp+20h]
  0047587b  8b442414              mov       eax,[esp+14h]
  0047587f  8b4c2418              mov       ecx,[esp+18h]
  00475883  83c410                add       esp,10h
  00475886  52                    push      edx
  00475887  50                    push      eax
  00475888  51                    push      ecx
  00475889  e852310c00            call      005389E0h
  0047588e  668b5606              mov       dx,[esi+6]
  00475892  83c40c                add       esp,0Ch
  00475895  66891526de5e00        mov       [5EDE26h],dx
  0047589c  8b4678                mov       eax,[esi+78h]
  0047589f  6a00                  push      0
  004758a1  50                    push      eax
  004758a2  6820de5e00            push      5EDE20h
  004758a7  e8045c0c00            call      0053B4B0h
  004758ac  83c40c                add       esp,0Ch
  004758af  c70600000000          mov       dword [esi],0

LAB_004758b5:
  004758b5  66ff4610              inc       word [esi+10h]
  004758b9  5e                    pop       esi
  004758ba  83c408                add       esp,8
  004758bd  c3                    ret
  004758be  90                    nop
  004758bf  90                    nop

2 cible(s) de saut interne : 0x004757d1, 0x004758b5


