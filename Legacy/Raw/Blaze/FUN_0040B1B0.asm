Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040b1b0  section .text  offset fichier 0xa5b0  64 octets

  0040b1b0  8b442404              mov       eax,[esp+4]
  0040b1b4  6683781402            cmp       word [eax+14h],2
  0040b1b9  66c780980000000000    mov       word [eax+98h],0
  0040b1c2  66c780960000000900    mov       word [eax+96h],9
  0040b1cb  750e                  jne       short 0040B1DBh
  0040b1cd  6683781000            cmp       word [eax+10h],0
  0040b1d2  7507                  jne       short 0040B1DBh
  0040b1d4  c7403c20f65400        mov       dword [eax+3Ch],54F620h

LAB_0040b1db:
  0040b1db  50                    push      eax
  0040b1dc  e80f000000            call      0040B1F0h
  0040b1e1  83c404                add       esp,4
  0040b1e4  c3                    ret
