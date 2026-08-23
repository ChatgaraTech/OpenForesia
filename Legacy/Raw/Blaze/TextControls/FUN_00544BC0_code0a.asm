Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x00544bc0  section .text  offset fichier 0x143fc0  80 octets

  00544bc0  53                    push      ebx
  00544bc1  56                    push      esi
  00544bc2  8b74240c              mov       esi,[esp+0Ch]
  00544bc6  8b15cc975e00          mov       edx,[5E97CCh]
  00544bcc  33c0                  xor       eax,eax
  00544bce  57                    push      edi
  00544bcf  8b7c2414              mov       edi,[esp+14h]
  00544bd3  8a6601                mov       ah,[esi+1]
  00544bd6  8a06                  mov       al,[esi]
  00544bd8  8b8fb0080000          mov       ecx,[edi+8B0h]
  00544bde  8b9fac080000          mov       ebx,[edi+8ACh]
  00544be4  50                    push      eax
  00544be5  51                    push      ecx
  00544be6  52                    push      edx
  00544be7  e8e4f1fcff            call      00513DD0h
  00544bec  8bbfac080000          mov       edi,[edi+8ACh]
  00544bf2  83c40c                add       esp,0Ch
  00544bf5  3bdf                  cmp       ebx,edi
  00544bf7  7509                  jne       short 00544C02h
  00544bf9  83c602                add       esi,2
  00544bfc  8bc6                  mov       eax,esi
  00544bfe  5f                    pop       edi
  00544bff  5e                    pop       esi
  00544c00  5b                    pop       ebx
  00544c01  c3                    ret

LAB_00544c02:
  00544c02  8bf7                  mov       esi,edi
  00544c04  5f                    pop       edi
  00544c05  8bc6                  mov       eax,esi
  00544c07  5e                    pop       esi
  00544c08  5b                    pop       ebx
  00544c09  c3                    ret
  00544c0a  90                    nop
  00544c0b  90                    nop
  00544c0c  90                    nop
  00544c0d  90                    nop
  00544c0e  90                    nop
  00544c0f  90                    nop

1 cible(s) de saut interne : 0x00544c02
