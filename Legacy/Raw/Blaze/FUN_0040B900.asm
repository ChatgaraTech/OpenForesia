Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040b900  section .text  offset fichier 0xad00  64 octets

  0040b900  8b442404              mov       eax,[esp+4]
  0040b904  6683781402            cmp       word [eax+14h],2
  0040b909  66c780980000000100    mov       word [eax+98h],1
  0040b912  66c780960000000700    mov       word [eax+96h],7
  0040b91b  750e                  jne       short 0040B92Bh
  0040b91d  6683781000            cmp       word [eax+10h],0
  0040b922  7507                  jne       short 0040B92Bh
  0040b924  c7403c28f65400        mov       dword [eax+3Ch],54F628h

LAB_0040b92b:
  0040b92b  50                    push      eax
  0040b92c  e8bff8ffff            call      0040B1F0h
  0040b931  83c404                add       esp,4
  0040b934  c3                    ret
