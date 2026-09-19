Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00486d60  section .text  offset fichier 0x86160  128 octets

  00486d60  56                    push      esi
  00486d61  8b742408              mov       esi,[esp+8]
  00486d65  66837e1000            cmp       word [esi+10h],0
  00486d6a  8b4e70                mov       ecx,[esi+70h]
  00486d6d  750c                  jne       short 00486D7Bh
  00486d6f  668146320004          add       word [esi+32h],400h
  00486d75  66c746100100          mov       word [esi+10h],1

LAB_00486d7b:
  00486d7b  0fbf4616              movsx     eax,word [esi+16h]
  00486d7f  83e800                sub       eax,0
  00486d82  7418                  je        short 00486D9Ch
  00486d84  48                    dec       eax
  00486d85  7550                  jne       short 00486DD7h
  00486d87  8b4638                mov       eax,[esi+38h]
  00486d8a  05f0efefff            add       eax,0FFEFEFF0h
  00486d8f  894638                mov       [esi+38h],eax
  00486d92  7543                  jne       short 00486DD7h
  00486d94  c70600000000          mov       dword [esi],0
  00486d9a  5e                    pop       esi
  00486d9b  c3                    ret

LAB_00486d9c:
  00486d9c  0fbf4628              movsx     eax,word [esi+28h]
  00486da0  ba00180000            mov       edx,1800h
  00486da5  2bd0                  sub       edx,eax
  00486da7  c1fa02                sar       edx,2
  00486daa  03c2                  add       eax,edx
  00486dac  3dc0170000            cmp       eax,17C0h
  00486db1  66894628              mov       [esi+28h],ax
  00486db5  7e20                  jle       short 00486DD7h
  00486db7  8b01                  mov       eax,[ecx]
  00486db9  0d00000002            or        eax,2000000h
  00486dbe  8901                  mov       [ecx],eax
  00486dc0  8d461c                lea       eax,[esi+1Ch]
  00486dc3  50                    push      eax
  00486dc4  6850285500            push      552850h
  00486dc9  e8126e0b00            call      0053DBE0h
  00486dce  83c408                add       esp,8
  00486dd1  c70600000000          mov       dword [esi],0

LAB_00486dd7:
  00486dd7  5e                    pop       esi
  00486dd8  c3                    ret
  00486dd9  90                    nop
  00486dda  90                    nop
  00486ddb  90                    nop
  00486ddc  90                    nop
  00486ddd  90                    nop
  00486dde  90                    nop
  00486ddf  90                    nop

3 cible(s) de saut interne : 0x00486d7b, 0x00486d9c, 0x00486dd7
