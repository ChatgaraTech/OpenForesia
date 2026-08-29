
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00475910  section .text  offset fichier 0x74d10  280 octets

  00475910  83ec08                sub       esp,8
  00475913  b906000000            mov       ecx,6
  00475918  56                    push      esi
  00475919  8b742410              mov       esi,[esp+10h]
  0047591d  57                    push      edi
  0047591e  0fbf4610              movsx     eax,word [esi+10h]
  00475922  99                    cdq
  00475923  f7f9                  idiv      ecx
  00475925  83fa01                cmp       edx,1
  00475928  7518                  jne       short 00475942h
  0047592a  8d561c                lea       edx,[esi+1Ch]
  0047592d  52                    push      edx
  0047592e  6800120000            push      1200h
  00475933  6800080000            push      800h
  00475938  6a14                  push      14h
  0047593a  e8e1810c00            call      0053DB20h
  0047593f  83c410                add       esp,10h

LAB_00475942:
  00475942  8b7e78                mov       edi,[esi+78h]
  00475945  e8f6170d00            call      00547140h
  0047594a  99                    cdq
  0047594b  b903000000            mov       ecx,3
  00475950  f7f9                  idiv      ecx
  00475952  885640                mov       [esi+40h],dl
  00475955  e8e6170d00            call      00547140h
  0047595a  25ff0f0000            and       eax,0FFFh
  0047595f  8d0480                lea       eax,[eax+eax*4]
  00475962  8d1480                lea       edx,[eax+eax*4]
  00475965  8b471c                mov       eax,[edi+1Ch]
  00475968  8d0c50                lea       ecx,[eax+edx*2]
  0047596b  894e1c                mov       [esi+1Ch],ecx
  0047596e  e8cd170d00            call      00547140h
  00475973  25ff0f0000            and       eax,0FFFh
  00475978  8d0440                lea       eax,[eax+eax*2]
  0047597b  8d0480                lea       eax,[eax+eax*4]
  0047597e  8d1480                lea       edx,[eax+eax*4]
  00475981  8b4720                mov       eax,[edi+20h]
  00475984  8d0c50                lea       ecx,[eax+edx*2]
  00475987  894e20                mov       [esi+20h],ecx
  0047598a  e8b1170d00            call      00547140h
  0047598f  25ff0f0000            and       eax,0FFFh
  00475994  66837e1020            cmp       word [esi+10h],20h
  00475999  8d0480                lea       eax,[eax+eax*4]
  0047599c  8d1480                lea       edx,[eax+eax*4]
  0047599f  8b4724                mov       eax,[edi+24h]
  004759a2  8d0c50                lea       ecx,[eax+edx*2]
  004759a5  894e24                mov       [esi+24h],ecx
  004759a8  7c74                  jl        short 00475A1Eh
  004759aa  6a27                  push      27h
  004759ac  6820de5e00            push      5EDE20h
  004759b1  56                    push      esi
  004759b2  c70600004080          mov       dword [esi],80400000h
  004759b8  e823f90b00            call      005352E0h
  004759bd  83c40c                add       esp,0Ch
  004759c0  8d542414              lea       edx,[esp+14h]
  004759c4  8d442408              lea       eax,[esp+8]
  004759c8  8d4c240c              lea       ecx,[esp+0Ch]
  004759cc  52                    push      edx
  004759cd  50                    push      eax
  004759ce  51                    push      ecx
  004759cf  6820de5e00            push      5EDE20h
  004759d4  c60528de5e0020        mov       byte [5EDE28h],20h
  004759db  e8a02f0c00            call      00538980h
  004759e0  8b542424              mov       edx,[esp+24h]
  004759e4  8b442418              mov       eax,[esp+18h]
  004759e8  8b4c241c              mov       ecx,[esp+1Ch]
  004759ec  83c410                add       esp,10h
  004759ef  52                    push      edx
  004759f0  50                    push      eax
  004759f1  51                    push      ecx
  004759f2  e8e92f0c00            call      005389E0h
  004759f7  668b5606              mov       dx,[esi+6]
  004759fb  83c40c                add       esp,0Ch
  004759fe  66891526de5e00        mov       [5EDE26h],dx
  00475a05  8b4678                mov       eax,[esi+78h]
  00475a08  6a00                  push      0
  00475a0a  50                    push      eax
  00475a0b  6820de5e00            push      5EDE20h
  00475a10  e8ab590c00            call      0053B3C0h
  00475a15  83c40c                add       esp,0Ch
  00475a18  c70600000000          mov       dword [esi],0

LAB_00475a1e:
  00475a1e  66ff4610              inc       word [esi+10h]
  00475a22  5f                    pop       edi
  00475a23  5e                    pop       esi
  00475a24  83c408                add       esp,8
  00475a27  c3                    ret

2 cible(s) de saut interne : 0x00475942, 0x00475a1e


