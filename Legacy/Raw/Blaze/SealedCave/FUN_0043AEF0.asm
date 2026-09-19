Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043aef0  section .text  offset fichier 0x3a2f0  128 octets

  0043aef0  53                    push      ebx
  0043aef1  55                    push      ebp
  0043aef2  56                    push      esi
  0043aef3  57                    push      edi
  0043aef4  8b7c2414              mov       edi,[esp+14h]
  0043aef8  bec0935e00            mov       esi,5E93C0h
  0043aefd  bb04000000            mov       ebx,4
  0043af02  bd00010000            mov       ebp,100h

LAB_0043af07:
  0043af07  8b06                  mov       eax,[esi]
  0043af09  a900000080            test      eax,80000000h
  0043af0e  743e                  je        short 0043AF4Eh
  0043af10  a900000050            test      eax,50000000h
  0043af15  7537                  jne       short 0043AF4Eh
  0043af17  85ae90000000          test      [esi+90h],ebp
  0043af1d  742f                  je        short 0043AF4Eh
  0043af1f  a801                  test      al,1
  0043af21  752b                  jne       short 0043AF4Eh
  0043af23  e818c21000            call      00547140h
  0043af28  99                    cdq
  0043af29  b905000000            mov       ecx,5
  0043af2e  f7f9                  idiv      ecx
  0043af30  6683fa01              cmp       dx,1
  0043af34  7518                  jne       short 0043AF4Eh
  0043af36  6aff                  push      0FFFFFFFFh
  0043af38  6a00                  push      0
  0043af3a  68ef000000            push      0EFh
  0043af3f  8d561c                lea       edx,[esi+1Ch]
  0043af42  6a00                  push      0
  0043af44  52                    push      edx
  0043af45  57                    push      edi
  0043af46  e8f5fc0300            call      0047AC40h
  0043af4b  83c418                add       esp,18h

LAB_0043af4e:
  0043af4e  81c69c000000          add       esi,9Ch
  0043af54  4b                    dec       ebx
  0043af55  75b0                  jne       short 0043AF07h
  0043af57  8b07                  mov       eax,[edi]
  0043af59  25ffffff7f            and       eax,7FFFFFFFh
  0043af5e  8907                  mov       [edi],eax
  0043af60  5f                    pop       edi
  0043af61  5e                    pop       esi
  0043af62  5d                    pop       ebp
  0043af63  5b                    pop       ebx
  0043af64  c3                    ret
  0043af65  90                    nop
  0043af66  90                    nop
  0043af67  90                    nop
  0043af68  90                    nop
  0043af69  90                    nop
  0043af6a  90                    nop
  0043af6b  90                    nop
  0043af6c  90                    nop
  0043af6d  90                    nop
  0043af6e  90                    nop
  0043af6f  90                    nop

2 cible(s) de saut interne : 0x0043af07, 0x0043af4e
