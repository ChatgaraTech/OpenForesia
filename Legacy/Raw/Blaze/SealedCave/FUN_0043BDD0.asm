Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043bdd0  section .text  offset fichier 0x3b1d0  336 octets

  0043bdd0  56                    push      esi
  0043bdd1  8b742408              mov       esi,[esp+8]
  0043bdd5  57                    push      edi
  0043bdd6  bf01000000            mov       edi,1
  0043bddb  66837e1000            cmp       word [esi+10h],0
  0043bde0  0f85c2000000          jne       near 0043BEA8h
  0043bde6  66897e10              mov       [esi+10h],di
  0043bdea  c7463c60115500        mov       dword [esi+3Ch],551160h
  0043bdf1  c6464000              mov       byte [esi+40h],0
  0043bdf5  c6460a02              mov       byte [esi+0Ah],2
  0043bdf9  c6460b00              mov       byte [esi+0Bh],0
  0043bdfd  e83eb31000            call      00547140h
  0043be02  99                    cdq
  0043be03  b9e8030000            mov       ecx,3E8h
  0043be08  f7f9                  idiv      ecx
  0043be0a  8d8220030000          lea       eax,[edx+320h]
  0043be10  66894628              mov       [esi+28h],ax
  0043be14  6689462a              mov       [esi+2Ah],ax
  0043be18  e823b31000            call      00547140h
  0043be1d  99                    cdq
  0043be1e  b920030000            mov       ecx,320h
  0043be23  f7f9                  idiv      ecx
  0043be25  81ea90010000          sub       edx,190h
  0043be2b  66015632              add       [esi+32h],dx
  0043be2f  e80cb31000            call      00547140h
  0043be34  99                    cdq
  0043be35  33c2                  xor       eax,edx
  0043be37  2bc2                  sub       eax,edx
  0043be39  83e007                and       eax,7
  0043be3c  33c2                  xor       eax,edx
  0043be3e  2bc2                  sub       eax,edx
  0043be40  83c008                add       eax,8
  0043be43  66894614              mov       [esi+14h],ax
  0043be47  e8f4b21000            call      00547140h
  0043be4c  99                    cdq
  0043be4d  b906000000            mov       ecx,6
  0043be52  f7f9                  idiv      ecx
  0043be54  03d1                  add       edx,ecx
  0043be56  66895616              mov       [esi+16h],dx
  0043be5a  e8e1b21000            call      00547140h
  0043be5f  99                    cdq
  0043be60  b93c000000            mov       ecx,3Ch
  0043be65  f7f9                  idiv      ecx
  0043be67  8b4e1c                mov       ecx,[esi+1Ch]
  0043be6a  83ea1e                sub       edx,1Eh
  0043be6d  c1e20c                shl       edx,0Ch
  0043be70  03ca                  add       ecx,edx
  0043be72  894e1c                mov       [esi+1Ch],ecx
  0043be75  e8c6b21000            call      00547140h
  0043be7a  99                    cdq
  0043be7b  b93c000000            mov       ecx,3Ch
  0043be80  f7f9                  idiv      ecx
  0043be82  8b4624                mov       eax,[esi+24h]
  0043be85  83ea1e                sub       edx,1Eh
  0043be88  c1e20c                shl       edx,0Ch
  0043be8b  03c2                  add       eax,edx
  0043be8d  894624                mov       [esi+24h],eax
  0043be90  e8abb21000            call      00547140h
  0043be95  99                    cdq
  0043be96  b914000000            mov       ecx,14h
  0043be9b  f7f9                  idiv      ecx
  0043be9d  8b4620                mov       eax,[esi+20h]
  0043bea0  c1e20c                shl       edx,0Ch
  0043bea3  03c2                  add       eax,edx
  0043bea5  894620                mov       [esi+20h],eax

LAB_0043bea8:
  0043bea8  66397e10              cmp       [esi+10h],di
  0043beac  7567                  jne       short 0043BF15h
  0043beae  0fbf5632              movsx     edx,word [esi+32h]
  0043beb2  52                    push      edx
  0043beb3  e8649b1000            call      00545A1Ch
  0043beb8  0fbf4e14              movsx     ecx,word [esi+14h]
  0043bebc  0fafc1                imul      eax,ecx
  0043bebf  0fbf5632              movsx     edx,word [esi+32h]
  0043bec3  8b4e1c                mov       ecx,[esi+1Ch]
  0043bec6  668b7e14              mov       di,[esi+14h]
  0043beca  03c8                  add       ecx,eax
  0043becc  52                    push      edx
  0043becd  894e1c                mov       [esi+1Ch],ecx
  0043bed0  e8419b1000            call      00545A16h
  0043bed5  0fbfcf                movsx     ecx,di
  0043bed8  0fafc1                imul      eax,ecx
  0043bedb  8b5624                mov       edx,[esi+24h]
  0043bede  6ae2                  push      0FFFFFFE2h
  0043bee0  03d0                  add       edx,eax
  0043bee2  8b4620                mov       eax,[esi+20h]
  0043bee5  895624                mov       [esi+24h],edx
  0043bee8  6ae2                  push      0FFFFFFE2h
  0043beea  0fbf5616              movsx     edx,word [esi+16h]
  0043beee  c1e20c                shl       edx,0Ch
  0043bef1  03c2                  add       eax,edx
  0043bef3  83c7fe                add       edi,0FFFFFFFEh
  0043bef6  6ae2                  push      0FFFFFFE2h
  0043bef8  56                    push      esi
  0043bef9  894620                mov       [esi+20h],eax
  0043befc  66897e14              mov       [esi+14h],di
  0043bf00  e8db8efcff            call      00404DE0h
  0043bf05  83c410                add       esp,10h
  0043bf08  66837e1400            cmp       word [esi+14h],0
  0043bf0d  7f06                  jg        short 0043BF15h
  0043bf0f  c706ffffff7f          mov       dword [esi],7FFFFFFFh

LAB_0043bf15:
  0043bf15  5f                    pop       edi
  0043bf16  5e                    pop       esi
  0043bf17  c3                    ret
  0043bf18  90                    nop
  0043bf19  90                    nop
  0043bf1a  90                    nop
  0043bf1b  90                    nop
  0043bf1c  90                    nop
  0043bf1d  90                    nop
  0043bf1e  90                    nop
  0043bf1f  90                    nop

2 cible(s) de saut interne : 0x0043bea8, 0x0043bf15
