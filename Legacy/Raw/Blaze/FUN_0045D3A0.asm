Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045d3a0  section .text  offset fichier 0x5c7a0  96 octets

  0045d3a0  8b442404              mov       eax,[esp+4]
  0045d3a4  ba01000000            mov       edx,1
  0045d3a9  8b08                  mov       ecx,[eax]
  0045d3ab  f6c110                test      cl,10h
  0045d3ae  7407                  je        short 0045D3B7h
  0045d3b0  6683781000            cmp       word [eax+10h],0
  0045d3b5  740b                  je        short 0045D3C2h

LAB_0045d3b7:
  0045d3b7  f6c120                test      cl,20h
  0045d3ba  741f                  je        short 0045D3DBh
  0045d3bc  66395010              cmp       [eax+10h],dx
  0045d3c0  7519                  jne       short 0045D3DBh

LAB_0045d3c2:
  0045d3c2  8b4820                mov       ecx,[eax+20h]
  0045d3c5  81f900800c00          cmp       ecx,0C8000h
  0045d3cb  7e28                  jle       short 0045D3F5h
  0045d3cd  81c10060ffff          add       ecx,0FFFF6000h
  0045d3d3  66895010              mov       [eax+10h],dx
  0045d3d7  894820                mov       [eax+20h],ecx
  0045d3da  c3                    ret

LAB_0045d3db:
  0045d3db  8b4820                mov       ecx,[eax+20h]
  0045d3de  81f900001900          cmp       ecx,190000h
  0045d3e4  7d0f                  jge       short 0045D3F5h
  0045d3e6  81c100a00000          add       ecx,0A000h
  0045d3ec  66c740100000          mov       word [eax+10h],0
  0045d3f2  894820                mov       [eax+20h],ecx

LAB_0045d3f5:
  0045d3f5  c3                    ret
  0045d3f6  90                    nop
  0045d3f7  90                    nop
  0045d3f8  90                    nop
  0045d3f9  90                    nop
  0045d3fa  90                    nop
  0045d3fb  90                    nop
  0045d3fc  90                    nop
  0045d3fd  90                    nop
  0045d3fe  90                    nop
  0045d3ff  90                    nop

4 cible(s) de saut interne : 0x0045d3b7, 0x0045d3c2, 0x0045d3db, 0x0045d3f5
