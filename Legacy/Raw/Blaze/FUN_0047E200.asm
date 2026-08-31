
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047e200  section .text  offset fichier 0x7d600  90 octets

  0047e200  53                    push      ebx
  0047e201  55                    push      ebp
  0047e202  56                    push      esi
  0047e203  8b742410              mov       esi,[esp+10h]
  0047e207  57                    push      edi
  0047e208  0fbf4628              movsx     eax,word [esi+28h]
  0047e20c  0fbf6e32              movsx     ebp,word [esi+32h]
  0047e210  8b5e70                mov       ebx,[esi+70h]
  0047e213  8d0440                lea       eax,[eax+eax*2]
  0047e216  55                    push      ebp
  0047e217  8d3c80                lea       edi,[eax+eax*4]
  0047e21a  c1ff0c                sar       edi,0Ch
  0047e21d  e8fa770c00            call      00545A1Ch
  0047e222  0fafc7                imul      eax,edi
  0047e225  8b4b1c                mov       ecx,[ebx+1Ch]
  0047e228  55                    push      ebp
  0047e229  03c1                  add       eax,ecx
  0047e22b  89461c                mov       [esi+1Ch],eax
  0047e22e  8b4360                mov       eax,[ebx+60h]
  0047e231  894620                mov       [esi+20h],eax
  0047e234  e8dd770c00            call      00545A16h
  0047e239  0fafc7                imul      eax,edi
  0047e23c  034324                add       eax,[ebx+24h]
  0047e23f  894624                mov       [esi+24h],eax
  0047e242  8b4638                mov       eax,[esi+38h]
  0047e245  05f0efefff            add       eax,0FFEFEFF0h
  0047e24a  894638                mov       [esi+38h],eax
  0047e24d  7506                  jne       short 0047E255h
  0047e24f  c70600000000          mov       dword [esi],0

LAB_0047e255:
  0047e255  5f                    pop       edi
  0047e256  5e                    pop       esi
  0047e257  5d                    pop       ebp
  0047e258  5b                    pop       ebx
  0047e259  c3                    ret

1 cible(s) de saut interne : 0x0047e255
