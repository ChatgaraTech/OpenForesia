
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047bc40  section .text  offset fichier 0x7b040  181 octets

  0047bc40  56                    push      esi
  0047bc41  8b742408              mov       esi,[esp+8]
  0047bc45  57                    push      edi
  0047bc46  668b4610              mov       ax,[esi+10h]
  0047bc4a  8b7e78                mov       edi,[esi+78h]
  0047bc4d  6685c0                test      ax,ax
  0047bc50  7516                  jne       short 0047BC68h
  0047bc52  c7462800180018        mov       dword [esi+28h],18001800h
  0047bc59  66c7462c0018          mov       word [esi+2Ch],1800h
  0047bc5f  c7463800000000        mov       dword [esi+38h],0
  0047bc66  eb44                  jmp       short 0047BCACh

LAB_0047bc68:
  0047bc68  663d2000              cmp       ax,20h
  0047bc6c  7d2d                  jge       short 0047BC9Bh
  0047bc6e  66817e28000c          cmp       word [esi+28h],0C00h
  0047bc74  7e11                  jle       short 0047BC87h
  0047bc76  8b4e28                mov       ecx,[esi+28h]
  0047bc79  81c180ff7fff          add       ecx,0FF7FFF80h
  0047bc7f  6683462c80            add       word [esi+2Ch],0FF80h
  0047bc84  894e28                mov       [esi+28h],ecx

LAB_0047bc87:
  0047bc87  8b4638                mov       eax,[esi+38h]
  0047bc8a  3df0f0f000            cmp       eax,0F0F0F0h
  0047bc8f  731b                  jae       short 0047BCACh
  0047bc91  0510101000            add       eax,101010h
  0047bc96  894638                mov       [esi+38h],eax
  0047bc99  eb11                  jmp       short 0047BCACh

LAB_0047bc9b:
  0047bc9b  8b4e28                mov       ecx,[esi+28h]
  0047bc9e  81c180ff7fff          add       ecx,0FF7FFF80h
  0047bca4  6683462c80            add       word [esi+2Ch],0FF80h
  0047bca9  894e28                mov       [esi+28h],ecx

LAB_0047bcac:
  0047bcac  8b471c                mov       eax,[edi+1Ch]
  0047bcaf  8b5674                mov       edx,[esi+74h]
  0047bcb2  89461c                mov       [esi+1Ch],eax
  0047bcb5  8b4f24                mov       ecx,[edi+24h]
  0047bcb8  52                    push      edx
  0047bcb9  894e24                mov       [esi+24h],ecx
  0047bcbc  e8ffce0b00            call      00538BC0h
  0047bcc1  8b4f20                mov       ecx,[edi+20h]
  0047bcc4  24fe                  and       al,0FEh
  0047bcc6  6681463240ff          add       word [esi+32h],0FF40h
  0047bccc  83c404                add       esp,4
  0047bccf  c1e00b                shl       eax,0Bh
  0047bcd2  03c1                  add       eax,ecx
  0047bcd4  894620                mov       [esi+20h],eax
  0047bcd7  668b4610              mov       ax,[esi+10h]
  0047bcdb  6685c0                test      ax,ax
  0047bcde  740d                  je        short 0047BCEDh
  0047bce0  8b4e28                mov       ecx,[esi+28h]
  0047bce3  85c9                  test      ecx,ecx
  0047bce5  7506                  jne       short 0047BCEDh
  0047bce7  c70600000000          mov       dword [esi],0

LAB_0047bced:
  0047bced  40                    inc       eax
  0047bcee  5f                    pop       edi
  0047bcef  66894610              mov       [esi+10h],ax
  0047bcf3  5e                    pop       esi
  0047bcf4  c3                    ret

5 cible(s) de saut interne : 0x0047bc68, 0x0047bc87, 0x0047bc9b, 0x0047bcac, 0x0047bced
