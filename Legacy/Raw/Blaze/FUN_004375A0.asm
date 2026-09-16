Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004375a0  section .text  offset fichier 0x369a0  144 octets

  004375a0  56                    push      esi
  004375a1  8b742408              mov       esi,[esp+8]
  004375a5  57                    push      edi
  004375a6  8b0e                  mov       ecx,[esi]
  004375a8  f7c100008000          test      ecx,800000h
  004375ae  7477                  je        short 00437627h
  004375b0  8a4608                mov       al,[esi+8]
  004375b3  3c04                  cmp       al,4
  004375b5  7370                  jae       short 00437627h
  004375b7  8b15b4876000          mov       edx,[6087B4h]
  004375bd  25ff000000            and       eax,0FFh
  004375c2  83c078                add       eax,78h
  004375c5  8b3a                  mov       edi,[edx]
  004375c7  c1e00d                shl       eax,0Dh
  004375ca  03c7                  add       eax,edi
  004375cc  8a8050010000          mov       al,[eax+150h]
  004375d2  2407                  and       al,7
  004375d4  7404                  je        short 004375DAh
  004375d6  3c06                  cmp       al,6
  004375d8  754d                  jne       short 00437627h

LAB_004375da:
  004375da  f6c104                test      cl,4
  004375dd  7448                  je        short 00437627h
  004375df  8b442418              mov       eax,[esp+18h]
  004375e3  8b5624                mov       edx,[esi+24h]
  004375e6  8b4c2410              mov       ecx,[esp+10h]
  004375ea  2bc2                  sub       eax,edx
  004375ec  2b4e1c                sub       ecx,[esi+1Ch]
  004375ef  c1f80c                sar       eax,0Ch
  004375f2  c1f90c                sar       ecx,0Ch
  004375f5  50                    push      eax
  004375f6  51                    push      ecx
  004375f7  e826e41000            call      00545A22h
  004375fc  0fbf5632              movsx     edx,word [esi+32h]
  00437600  2bc2                  sub       eax,edx
  00437602  25ff0f0000            and       eax,0FFFh
  00437607  3de8030000            cmp       eax,3E8h
  0043760c  7d04                  jge       short 00437612h
  0043760e  85c0                  test      eax,eax
  00437610  7f0e                  jg        short 00437620h

LAB_00437612:
  00437612  3d00100000            cmp       eax,1000h
  00437617  7d0e                  jge       short 00437627h
  00437619  3d180c0000            cmp       eax,0C18h
  0043761e  7e07                  jle       short 00437627h

LAB_00437620:
  00437620  66b80100              mov       ax,1
  00437624  5f                    pop       edi
  00437625  5e                    pop       esi
  00437626  c3                    ret

LAB_00437627:
  00437627  5f                    pop       edi
  00437628  6633c0                xor       ax,ax
  0043762b  5e                    pop       esi
  0043762c  c3                    ret
  0043762d  90                    nop
  0043762e  90                    nop
  0043762f  90                    nop

4 cible(s) de saut interne : 0x004375da, 0x00437612, 0x00437620, 0x00437627
