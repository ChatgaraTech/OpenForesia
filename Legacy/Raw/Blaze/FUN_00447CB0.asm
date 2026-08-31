Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00447cb0  section .text  offset fichier 0x470b0  152 octets

  00447cb0  83ec08                sub       esp,8
  00447cb3  56                    push      esi
  00447cb4  8b742414              mov       esi,[esp+14h]
  00447cb8  57                    push      edi
  00447cb9  8b7c2414              mov       edi,[esp+14h]
  00447cbd  85f6                  test      esi,esi
  00447cbf  8b4774                mov       eax,[edi+74h]
  00447cc2  894778                mov       [edi+78h],eax
  00447cc5  750d                  jne       short 00447CD4h
  00447cc7  56                    push      esi
  00447cc8  57                    push      edi
  00447cc9  e8d2ce0200            call      00474BA0h
  00447cce  83c408                add       esp,8
  00447cd1  894778                mov       [edi+78h],eax

LAB_00447cd4:
  00447cd4  83fe01                cmp       esi,1
  00447cd7  7567                  jne       short 00447D40h
  00447cd9  bac0935e00            mov       edx,5E93C0h
  00447cde  8d4c2408              lea       ecx,[esp+8]
  00447ce2  be04000000            mov       esi,4

LAB_00447ce7:
  00447ce7  8b02                  mov       eax,[edx]
  00447ce9  66c7010000            mov       word [ecx],0
  00447cee  a900000040            test      eax,40000000h
  00447cf3  740c                  je        short 00447D01h
  00447cf5  25ffdfffbf            and       eax,0BFFFDFFFh
  00447cfa  66c7010100            mov       word [ecx],1
  00447cff  8902                  mov       [edx],eax

LAB_00447d01:
  00447d01  83c102                add       ecx,2
  00447d04  81c29c000000          add       edx,9Ch
  00447d0a  4e                    dec       esi
  00447d0b  75da                  jne       short 00447CE7h
  00447d0d  6a00                  push      0
  00447d0f  57                    push      edi
  00447d10  e88bce0200            call      00474BA0h
  00447d15  894778                mov       [edi+78h],eax
  00447d18  83c408                add       esp,8
  00447d1b  b8c0935e00            mov       eax,5E93C0h
  00447d20  8d4c2408              lea       ecx,[esp+8]
  00447d24  ba04000000            mov       edx,4

LAB_00447d29:
  00447d29  66833901              cmp       word [ecx],1
  00447d2d  7506                  jne       short 00447D35h
  00447d2f  810800200040          or        dword [eax],40002000h

LAB_00447d35:
  00447d35  83c102                add       ecx,2
  00447d38  059c000000            add       eax,9Ch
  00447d3d  4a                    dec       edx
  00447d3e  75e9                  jne       short 00447D29h

LAB_00447d40:
  00447d40  5f                    pop       edi
  00447d41  5e                    pop       esi
  00447d42  83c408                add       esp,8
  00447d45  c3                    ret
  00447d46  90                    nop
  00447d47  90                    nop

6 cible(s) de saut interne : 0x00447cd4, 0x00447ce7, 0x00447d01, 0x00447d29, 0x00447d35, 0x00447d40
