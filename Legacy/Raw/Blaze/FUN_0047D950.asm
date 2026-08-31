
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047d950  section .text  offset fichier 0x7cd50  241 octets

  0047d950  53                    push      ebx
  0047d951  56                    push      esi
  0047d952  8b74240c              mov       esi,[esp+0Ch]
  0047d956  57                    push      edi
  0047d957  668b4610              mov       ax,[esi+10h]
  0047d95b  8b5e78                mov       ebx,[esi+78h]
  0047d95e  6685c0                test      ax,ax
  0047d961  7530                  jne       short 0047D993h
  0047d963  c7463800000000        mov       dword [esi+38h],0
  0047d96a  66c7462eecff          mov       word [esi+2Eh],0FFECh
  0047d970  c7462800000000        mov       dword [esi+28h],0
  0047d977  6689462c              mov       [esi+2Ch],ax
  0047d97b  e8c0970c00            call      00547140h
  0047d980  53                    push      ebx
  0047d981  66894632              mov       [esi+32h],ax
  0047d985  e836b20b00            call      00538BC0h
  0047d98a  83c404                add       esp,4
  0047d98d  66894612              mov       [esi+12h],ax
  0047d991  eb7b                  jmp       short 0047DA0Eh

LAB_0047d993:
  0047d993  663d2800              cmp       ax,28h
  0047d997  7d3d                  jge       short 0047D9D6h
  0047d999  0fbf5612              movsx     edx,word [esi+12h]
  0047d99d  33c9                  xor       ecx,ecx
  0047d99f  b8ff000000            mov       eax,0FFh
  0047d9a4  8a4e38                mov       cl,[esi+38h]
  0047d9a7  668b7e28              mov       di,[esi+28h]
  0047d9ab  2bc1                  sub       eax,ecx
  0047d9ad  c1f802                sar       eax,2
  0047d9b0  03c8                  add       ecx,eax
  0047d9b2  b81f85eb51            mov       eax,51EB851Fh
  0047d9b7  c1e20c                shl       edx,0Ch
  0047d9ba  f7ea                  imul      edx
  0047d9bc  c1fa05                sar       edx,5
  0047d9bf  8bc2                  mov       eax,edx
  0047d9c1  c1e81f                shr       eax,1Fh
  0047d9c4  03d0                  add       edx,eax
  0047d9c6  0fbfc7                movsx     eax,di
  0047d9c9  2bd0                  sub       edx,eax
  0047d9cb  c1fa02                sar       edx,2
  0047d9ce  03d7                  add       edx,edi
  0047d9d0  66895628              mov       [esi+28h],dx
  0047d9d4  eb1d                  jmp       short 0047D9F3h

LAB_0047d9d6:
  0047d9d6  668b4628              mov       ax,[esi+28h]
  0047d9da  33c9                  xor       ecx,ecx
  0047d9dc  8a4e38                mov       cl,[esi+38h]
  0047d9df  8bd1                  mov       edx,ecx
  0047d9e1  c1fa02                sar       edx,2
  0047d9e4  2bca                  sub       ecx,edx
  0047d9e6  668bd0                mov       dx,ax
  0047d9e9  66c1fa02              sar       dx,2
  0047d9ed  2bc2                  sub       eax,edx
  0047d9ef  66894628              mov       [esi+28h],ax

LAB_0047d9f3:
  0047d9f3  8bc1                  mov       eax,ecx
  0047d9f5  c1e008                shl       eax,8
  0047d9f8  0bc1                  or        eax,ecx
  0047d9fa  c1e008                shl       eax,8
  0047d9fd  0bc1                  or        eax,ecx
  0047d9ff  894638                mov       [esi+38h],eax
  0047da02  668b4628              mov       ax,[esi+28h]
  0047da06  6689462a              mov       [esi+2Ah],ax
  0047da0a  6689462c              mov       [esi+2Ch],ax

LAB_0047da0e:
  0047da0e  0fbf5612              movsx     edx,word [esi+12h]
  0047da12  8b4b1c                mov       ecx,[ebx+1Ch]
  0047da15  83e2fe                and       edx,0FFFFFFFEh
  0047da18  894e1c                mov       [esi+1Ch],ecx
  0047da1b  8b4320                mov       eax,[ebx+20h]
  0047da1e  c1e20b                shl       edx,0Bh
  0047da21  03c2                  add       eax,edx
  0047da23  894620                mov       [esi+20h],eax
  0047da26  8b4b24                mov       ecx,[ebx+24h]
  0047da29  66ff4610              inc       word [esi+10h]
  0047da2d  66837e103c            cmp       word [esi+10h],3Ch
  0047da32  894e24                mov       [esi+24h],ecx
  0047da35  7c06                  jl        short 0047DA3Dh
  0047da37  c70600000000          mov       dword [esi],0

LAB_0047da3d:
  0047da3d  5f                    pop       edi
  0047da3e  5e                    pop       esi
  0047da3f  5b                    pop       ebx
  0047da40  c3                    ret

5 cible(s) de saut interne : 0x0047d993, 0x0047d9d6, 0x0047d9f3, 0x0047da0e, 0x0047da3d
