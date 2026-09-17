Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045d340  section .text  offset fichier 0x5c740  96 octets

  0045d340  8b442404              mov       eax,[esp+4]
  0045d344  56                    push      esi
  0045d345  8b08                  mov       ecx,[eax]
  0045d347  f7c100000040          test      ecx,40000000h
  0045d34d  7545                  jne       short 0045D394h
  0045d34f  33f6                  xor       esi,esi
  0045d351  66397010              cmp       [eax+10h],si
  0045d355  750e                  jne       short 0045D365h
  0045d357  66c740100100          mov       word [eax+10h],1
  0045d35d  66897012              mov       [eax+12h],si
  0045d361  66897014              mov       [eax+14h],si

LAB_0045d365:
  0045d365  668b5014              mov       dx,[eax+14h]
  0045d369  6683fa03              cmp       dx,3
  0045d36d  7d07                  jge       short 0045D376h
  0045d36f  42                    inc       edx
  0045d370  66895014              mov       [eax+14h],dx
  0045d374  5e                    pop       esi
  0045d375  c3                    ret

LAB_0045d376:
  0045d376  668140349000          add       word [eax+34h],90h
  0045d37c  668178340010          cmp       word [eax+34h],1000h
  0045d382  7e10                  jle       short 0045D394h
  0045d384  81c900000040          or        ecx,40000000h
  0045d38a  66897034              mov       [eax+34h],si
  0045d38e  66897010              mov       [eax+10h],si
  0045d392  8908                  mov       [eax],ecx

LAB_0045d394:
  0045d394  5e                    pop       esi
  0045d395  c3                    ret
  0045d396  90                    nop
  0045d397  90                    nop
  0045d398  90                    nop
  0045d399  90                    nop
  0045d39a  90                    nop
  0045d39b  90                    nop
  0045d39c  90                    nop
  0045d39d  90                    nop
  0045d39e  90                    nop
  0045d39f  90                    nop

3 cible(s) de saut interne : 0x0045d365, 0x0045d376, 0x0045d394
