Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00403d60  section .text  offset fichier 0x3160  277 octets

  00403d60  56                    push      esi
  00403d61  8b742408              mov       esi,[esp+8]
  00403d65  57                    push      edi
  00403d66  66837e1000            cmp       word [esi+10h],0
  00403d6b  0f85c3000000          jne       near 00403E34h
  00403d71  8a4616                mov       al,[esi+16h]
  00403d74  66c746100100          mov       word [esi+10h],1
  00403d7a  0404                  add       al,4
  00403d7c  c7463ce0f05400        mov       dword [esi+3Ch],54F0E0h
  00403d83  c6464000              mov       byte [esi+40h],0
  00403d87  c6460b01              mov       byte [esi+0Bh],1
  00403d8b  88460a                mov       [esi+0Ah],al
  00403d8e  e8ad331400            call      00547140h
  00403d93  99                    cdq
  00403d94  33c2                  xor       eax,edx
  00403d96  2bc2                  sub       eax,edx
  00403d98  25ff030000            and       eax,3FFh
  00403d9d  0fbf7e14              movsx     edi,word [esi+14h]
  00403da1  33c2                  xor       eax,edx
  00403da3  2bc2                  sub       eax,edx
  00403da5  05c4090000            add       eax,9C4h
  00403daa  66894628              mov       [esi+28h],ax
  00403dae  6689462a              mov       [esi+2Ah],ax
  00403db2  e889331400            call      00547140h
  00403db7  8bcf                  mov       ecx,edi
  00403db9  99                    cdq
  00403dba  d1f9                  sar       ecx,1
  00403dbc  f7f9                  idiv      ecx
  00403dbe  c1ff02                sar       edi,2
  00403dc1  2bd7                  sub       edx,edi
  00403dc3  8b7e1c                mov       edi,[esi+1Ch]
  00403dc6  c1e20c                shl       edx,0Ch
  00403dc9  03fa                  add       edi,edx
  00403dcb  897e1c                mov       [esi+1Ch],edi
  00403dce  0fbf7e14              movsx     edi,word [esi+14h]
  00403dd2  e869331400            call      00547140h
  00403dd7  8bcf                  mov       ecx,edi
  00403dd9  99                    cdq
  00403dda  d1f9                  sar       ecx,1
  00403ddc  f7f9                  idiv      ecx
  00403dde  c1ff02                sar       edi,2
  00403de1  2bd7                  sub       edx,edi
  00403de3  8b7e24                mov       edi,[esi+24h]
  00403de6  c1e20c                shl       edx,0Ch
  00403de9  03fa                  add       edi,edx
  00403deb  897e24                mov       [esi+24h],edi
  00403dee  e84d331400            call      00547140h
  00403df3  0fbf4e14              movsx     ecx,word [esi+14h]
  00403df7  99                    cdq
  00403df8  f7f9                  idiv      ecx
  00403dfa  8b4620                mov       eax,[esi+20h]
  00403dfd  c1e20c                shl       edx,0Ch
  00403e00  03c2                  add       eax,edx
  00403e02  894620                mov       [esi+20h],eax
  00403e05  e836331400            call      00547140h
  00403e0a  99                    cdq
  00403e0b  33c2                  xor       eax,edx
  00403e0d  66c746120000          mov       word [esi+12h],0
  00403e13  2bc2                  sub       eax,edx
  00403e15  83e001                and       eax,1
  00403e18  33c2                  xor       eax,edx
  00403e1a  2bc2                  sub       eax,edx
  00403e1c  40                    inc       eax
  00403e1d  66894616              mov       [esi+16h],ax
  00403e21  b080                  mov       al,80h
  00403e23  884638                mov       [esi+38h],al
  00403e26  884639                mov       [esi+39h],al
  00403e29  88463a                mov       [esi+3Ah],al
  00403e2c  8a463b                mov       al,[esi+3Bh]
  00403e2f  24fd                  and       al,0FDh
  00403e31  88463b                mov       [esi+3Bh],al

LAB_00403e34:
  00403e34  66837e1001            cmp       word [esi+10h],1
  00403e39  7537                  jne       short 00403E72h
  00403e3b  6afc                  push      0FFFFFFFCh
  00403e3d  6afc                  push      0FFFFFFFCh
  00403e3f  6afc                  push      0FFFFFFFCh
  00403e41  56                    push      esi
  00403e42  e8990f0000            call      00404DE0h
  00403e47  83c410                add       esp,10h
  00403e4a  66ff4612              inc       word [esi+12h]
  00403e4e  668b4612              mov       ax,[esi+12h]
  00403e52  663b4616              cmp       ax,[esi+16h]
  00403e56  7c1a                  jl        short 00403E72h
  00403e58  8a4e40                mov       cl,[esi+40h]
  00403e5b  66c746120000          mov       word [esi+12h],0
  00403e61  fec1                  inc       cl
  00403e63  8ac1                  mov       al,cl
  00403e65  884e40                mov       [esi+40h],cl
  00403e68  3c07                  cmp       al,7
  00403e6a  7506                  jne       short 00403E72h
  00403e6c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00403e72:
  00403e72  5f                    pop       edi
  00403e73  5e                    pop       esi
  00403e74  c3                    ret

2 cible(s) de saut interne : 0x00403e34, 0x00403e72
