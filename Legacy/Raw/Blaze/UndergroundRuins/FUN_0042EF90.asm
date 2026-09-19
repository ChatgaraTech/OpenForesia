Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042ef90  section .text  offset fichier 0x2e390  128 octets

  0042ef90  53                    push      ebx
  0042ef91  56                    push      esi
  0042ef92  57                    push      edi
  0042ef93  bea0ca5e00            mov       esi,5ECAA0h
  0042ef98  bf20000000            mov       edi,20h
  0042ef9d  bb02000000            mov       ebx,2

LAB_0042efa2:
  0042efa2  385e0c                cmp       [esi+0Ch],bl
  0042efa5  7546                  jne       short 0042EFEDh
  0042efa7  8b06                  mov       eax,[esi]
  0042efa9  a900000080            test      eax,80000000h
  0042efae  743d                  je        short 0042EFEDh
  0042efb0  a808                  test      al,8
  0042efb2  7439                  je        short 0042EFEDh
  0042efb4  56                    push      esi
  0042efb5  e866020000            call      0042F220h
  0042efba  8b465c                mov       eax,[esi+5Ch]
  0042efbd  83c404                add       esp,4
  0042efc0  25ffffff3f            and       eax,3FFFFFFFh
  0042efc5  83f801                cmp       eax,1
  0042efc8  750b                  jne       short 0042EFD5h
  0042efca  6a00                  push      0
  0042efcc  56                    push      esi
  0042efcd  e83e000000            call      0042F010h
  0042efd2  83c408                add       esp,8

LAB_0042efd5:
  0042efd5  8b4e5c                mov       ecx,[esi+5Ch]
  0042efd8  81e1ffffff3f          and       ecx,3FFFFFFFh
  0042efde  3bcb                  cmp       ecx,ebx
  0042efe0  750b                  jne       short 0042EFEDh
  0042efe2  6a03                  push      3
  0042efe4  56                    push      esi
  0042efe5  e846010000            call      0042F130h
  0042efea  83c408                add       esp,8

LAB_0042efed:
  0042efed  81c69c000000          add       esi,9Ch
  0042eff3  4f                    dec       edi
  0042eff4  75ac                  jne       short 0042EFA2h
  0042eff6  8b442410              mov       eax,[esp+10h]
  0042effa  5f                    pop       edi
  0042effb  5e                    pop       esi
  0042effc  5b                    pop       ebx
  0042effd  8120ffffff7f          and       dword [eax],7FFFFFFFh
  0042f003  c3                    ret
  0042f004  90                    nop
  0042f005  90                    nop
  0042f006  90                    nop
  0042f007  90                    nop
  0042f008  90                    nop
  0042f009  90                    nop
  0042f00a  90                    nop
  0042f00b  90                    nop
  0042f00c  90                    nop
  0042f00d  90                    nop
  0042f00e  90                    nop
  0042f00f  90                    nop

3 cible(s) de saut interne : 0x0042efa2, 0x0042efd5, 0x0042efed
