
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515e20  section .text  offset fichier 0x115220  96 octets

  00515e20  8b442408              mov       eax,[esp+8]
  00515e24  8b08                  mov       ecx,[eax]
  00515e26  8bd1                  mov       edx,ecx
  00515e28  81e20000fc00          and       edx,0FC0000h
  00515e2e  81fa00008000          cmp       edx,800000h
  00515e34  7533                  jne       short 00515E69h
  00515e36  668b5032              mov       dx,[eax+32h]
  00515e3a  81e1fffdfff7          and       ecx,0F7FFFDFFh
  00515e40  80cd20                or        ch,20h
  00515e43  66ff4012              inc       word [eax+12h]
  00515e47  66899098000000        mov       [eax+98h],dx
  00515e4e  8908                  mov       [eax],ecx
  00515e50  8a4008                mov       al,[eax+8]
  00515e53  c605f8455600fe        mov       byte [5645F8h],0FEh
  00515e5a  a23ce55d00            mov       [5DE53Ch],al
  00515e5f  c70510e55d0002000000  mov       dword [5DE510h],2

LAB_00515e69:
  00515e69  8b4c240c              mov       ecx,[esp+0Ch]
  00515e6d  8d4101                lea       eax,[ecx+1]
  00515e70  c3                    ret
  00515e71  90                    nop
  00515e72  90                    nop
  00515e73  90                    nop
  00515e74  90                    nop
  00515e75  90                    nop
  00515e76  90                    nop
  00515e77  90                    nop
  00515e78  90                    nop
  00515e79  90                    nop
  00515e7a  90                    nop
  00515e7b  90                    nop
  00515e7c  90                    nop
  00515e7d  90                    nop
  00515e7e  90                    nop
  00515e7f  90                    nop

1 cible(s) de saut interne : 0x00515e69
