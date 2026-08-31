
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047cf30  section .text  offset fichier 0x7c330  183 octets

  0047cf30  83ec0c                sub       esp,0Ch
  0047cf33  56                    push      esi
  0047cf34  8b742414              mov       esi,[esp+14h]
  0047cf38  57                    push      edi
  0047cf39  668b4614              mov       ax,[esi+14h]
  0047cf3d  8b7e70                mov       edi,[esi+70h]
  0047cf40  66014632              add       [esi+32h],ax
  0047cf44  668b4632              mov       ax,[esi+32h]
  0047cf48  0fbf4e12              movsx     ecx,word [esi+12h]
  0047cf4c  0fbf5634              movsx     edx,word [esi+34h]
  0047cf50  0fbfc0                movsx     eax,ax
  0047cf53  51                    push      ecx
  0047cf54  2d00040000            sub       eax,400h
  0047cf59  52                    push      edx
  0047cf5a  8d4c2410              lea       ecx,[esp+10h]
  0047cf5e  50                    push      eax
  0047cf5f  51                    push      ecx
  0047cf60  e86b5b0b00            call      00532AD0h
  0047cf65  8b571c                mov       edx,[edi+1Ch]
  0047cf68  8b4e44                mov       ecx,[esi+44h]
  0047cf6b  8b442418              mov       eax,[esp+18h]
  0047cf6f  03d1                  add       edx,ecx
  0047cf71  8b4c241c              mov       ecx,[esp+1Ch]
  0047cf75  03d0                  add       edx,eax
  0047cf77  89561c                mov       [esi+1Ch],edx
  0047cf7a  8b4720                mov       eax,[edi+20h]
  0047cf7d  8b5648                mov       edx,[esi+48h]
  0047cf80  83c410                add       esp,10h
  0047cf83  03c2                  add       eax,edx
  0047cf85  8b542410              mov       edx,[esp+10h]
  0047cf89  03c1                  add       eax,ecx
  0047cf8b  894620                mov       [esi+20h],eax
  0047cf8e  8b4f24                mov       ecx,[edi+24h]
  0047cf91  8b7e4c                mov       edi,[esi+4Ch]
  0047cf94  8b4638                mov       eax,[esi+38h]
  0047cf97  03cf                  add       ecx,edi
  0047cf99  03ca                  add       ecx,edx
  0047cf9b  3df0f0f000            cmp       eax,0F0F0F0h
  0047cfa0  894e24                mov       [esi+24h],ecx
  0047cfa3  7308                  jae       short 0047CFADh
  0047cfa5  0510101000            add       eax,101010h
  0047cfaa  894638                mov       [esi+38h],eax

LAB_0047cfad:
  0047cfad  668b4628              mov       ax,[esi+28h]
  0047cfb1  663d0008              cmp       ax,800h
  0047cfb5  7e0d                  jle       short 0047CFC4h
  0047cfb7  668bd0                mov       dx,ax
  0047cfba  66c1fa02              sar       dx,2
  0047cfbe  2bc2                  sub       eax,edx
  0047cfc0  66894628              mov       [esi+28h],ax

LAB_0047cfc4:
  0047cfc4  668b4612              mov       ax,[esi+12h]
  0047cfc8  668bc8                mov       cx,ax
  0047cfcb  66c1f902              sar       cx,2
  0047cfcf  2bc1                  sub       eax,ecx
  0047cfd1  663d1000              cmp       ax,10h
  0047cfd5  66894612              mov       [esi+12h],ax
  0047cfd9  7d06                  jge       short 0047CFE1h
  0047cfdb  c70600000000          mov       dword [esi],0

LAB_0047cfe1:
  0047cfe1  5f                    pop       edi
  0047cfe2  5e                    pop       esi
  0047cfe3  83c40c                add       esp,0Ch
  0047cfe6  c3                    ret

3 cible(s) de saut interne : 0x0047cfad, 0x0047cfc4, 0x0047cfe1
