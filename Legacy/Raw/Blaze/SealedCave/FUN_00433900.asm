Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00433900  section .text  offset fichier 0x32d00  336 octets

  00433900  56                    push      esi
  00433901  8b742408              mov       esi,[esp+8]
  00433905  57                    push      edi
  00433906  bf01000000            mov       edi,1
  0043390b  66837e1000            cmp       word [esi+10h],0
  00433910  0f85c2000000          jne       near 004339D8h
  00433916  66897e10              mov       [esi+10h],di
  0043391a  c7463ca80d5500        mov       dword [esi+3Ch],550DA8h
  00433921  c6464000              mov       byte [esi+40h],0
  00433925  c6460a02              mov       byte [esi+0Ah],2
  00433929  c6460b00              mov       byte [esi+0Bh],0
  0043392d  e80e381100            call      00547140h
  00433932  99                    cdq
  00433933  b9e8030000            mov       ecx,3E8h
  00433938  f7f9                  idiv      ecx
  0043393a  8d8220030000          lea       eax,[edx+320h]
  00433940  66894628              mov       [esi+28h],ax
  00433944  6689462a              mov       [esi+2Ah],ax
  00433948  e8f3371100            call      00547140h
  0043394d  99                    cdq
  0043394e  b920030000            mov       ecx,320h
  00433953  f7f9                  idiv      ecx
  00433955  81ea90010000          sub       edx,190h
  0043395b  66015632              add       [esi+32h],dx
  0043395f  e8dc371100            call      00547140h
  00433964  99                    cdq
  00433965  33c2                  xor       eax,edx
  00433967  2bc2                  sub       eax,edx
  00433969  83e007                and       eax,7
  0043396c  33c2                  xor       eax,edx
  0043396e  2bc2                  sub       eax,edx
  00433970  83c008                add       eax,8
  00433973  66894614              mov       [esi+14h],ax
  00433977  e8c4371100            call      00547140h
  0043397c  99                    cdq
  0043397d  b906000000            mov       ecx,6
  00433982  f7f9                  idiv      ecx
  00433984  03d1                  add       edx,ecx
  00433986  66895616              mov       [esi+16h],dx
  0043398a  e8b1371100            call      00547140h
  0043398f  99                    cdq
  00433990  b93c000000            mov       ecx,3Ch
  00433995  f7f9                  idiv      ecx
  00433997  8b4e1c                mov       ecx,[esi+1Ch]
  0043399a  83ea1e                sub       edx,1Eh
  0043399d  c1e20c                shl       edx,0Ch
  004339a0  03ca                  add       ecx,edx
  004339a2  894e1c                mov       [esi+1Ch],ecx
  004339a5  e896371100            call      00547140h
  004339aa  99                    cdq
  004339ab  b93c000000            mov       ecx,3Ch
  004339b0  f7f9                  idiv      ecx
  004339b2  8b4624                mov       eax,[esi+24h]
  004339b5  83ea1e                sub       edx,1Eh
  004339b8  c1e20c                shl       edx,0Ch
  004339bb  03c2                  add       eax,edx
  004339bd  894624                mov       [esi+24h],eax
  004339c0  e87b371100            call      00547140h
  004339c5  99                    cdq
  004339c6  b914000000            mov       ecx,14h
  004339cb  f7f9                  idiv      ecx
  004339cd  8b4620                mov       eax,[esi+20h]
  004339d0  c1e20c                shl       edx,0Ch
  004339d3  03c2                  add       eax,edx
  004339d5  894620                mov       [esi+20h],eax

LAB_004339d8:
  004339d8  66397e10              cmp       [esi+10h],di
  004339dc  7567                  jne       short 00433A45h
  004339de  0fbf5632              movsx     edx,word [esi+32h]
  004339e2  52                    push      edx
  004339e3  e834201100            call      00545A1Ch
  004339e8  0fbf4e14              movsx     ecx,word [esi+14h]
  004339ec  0fafc1                imul      eax,ecx
  004339ef  0fbf5632              movsx     edx,word [esi+32h]
  004339f3  8b4e1c                mov       ecx,[esi+1Ch]
  004339f6  668b7e14              mov       di,[esi+14h]
  004339fa  03c8                  add       ecx,eax
  004339fc  52                    push      edx
  004339fd  894e1c                mov       [esi+1Ch],ecx
  00433a00  e811201100            call      00545A16h
  00433a05  0fbfcf                movsx     ecx,di
  00433a08  0fafc1                imul      eax,ecx
  00433a0b  8b5624                mov       edx,[esi+24h]
  00433a0e  6ae2                  push      0FFFFFFE2h
  00433a10  03d0                  add       edx,eax
  00433a12  8b4620                mov       eax,[esi+20h]
  00433a15  895624                mov       [esi+24h],edx
  00433a18  6ae2                  push      0FFFFFFE2h
  00433a1a  0fbf5616              movsx     edx,word [esi+16h]
  00433a1e  c1e20c                shl       edx,0Ch
  00433a21  03c2                  add       eax,edx
  00433a23  83c7fe                add       edi,0FFFFFFFEh
  00433a26  6ae2                  push      0FFFFFFE2h
  00433a28  56                    push      esi
  00433a29  894620                mov       [esi+20h],eax
  00433a2c  66897e14              mov       [esi+14h],di
  00433a30  e8ab13fdff            call      00404DE0h
  00433a35  83c410                add       esp,10h
  00433a38  66837e1400            cmp       word [esi+14h],0
  00433a3d  7f06                  jg        short 00433A45h
  00433a3f  c706ffffff7f          mov       dword [esi],7FFFFFFFh

LAB_00433a45:
  00433a45  5f                    pop       edi
  00433a46  5e                    pop       esi
  00433a47  c3                    ret
  00433a48  90                    nop
  00433a49  90                    nop
  00433a4a  90                    nop
  00433a4b  90                    nop
  00433a4c  90                    nop
  00433a4d  90                    nop
  00433a4e  90                    nop
  00433a4f  90                    nop

2 cible(s) de saut interne : 0x004339d8, 0x00433a45
