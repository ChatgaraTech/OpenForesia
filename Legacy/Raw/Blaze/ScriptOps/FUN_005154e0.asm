
Legacy/Binaries/Blaze.exe  base 0x00400000
0x005154e0  section .text  offset fichier 0x1148e0  96 octets

  005154e0  8b4c2410              mov       ecx,[esp+10h]
  005154e4  833900                cmp       dword [ecx],0
  005154e7  7420                  je        short 00515509h
  005154e9  8b44240c              mov       eax,[esp+0Ch]
  005154ed  33d2                  xor       edx,edx
  005154ef  8a5001                mov       dl,[eax+1]
  005154f2  8a7002                mov       dh,[eax+2]
  005154f5  8b442404              mov       eax,[esp+4]
  005154f9  8b808c000000          mov       eax,[eax+8Ch]
  005154ff  8b0490                mov       eax,[eax+edx*4]
  00515502  c70100000000          mov       dword [ecx],0
  00515508  c3                    ret

LAB_00515509:
  00515509  8b44240c              mov       eax,[esp+0Ch]
  0051550d  83c003                add       eax,3
  00515510  c3                    ret
  00515511  90                    nop
  00515512  90                    nop
  00515513  90                    nop
  00515514  90                    nop
  00515515  90                    nop
  00515516  90                    nop
  00515517  90                    nop
  00515518  90                    nop
  00515519  90                    nop
  0051551a  90                    nop
  0051551b  90                    nop
  0051551c  90                    nop
  0051551d  90                    nop
  0051551e  90                    nop
  0051551f  90                    nop
  00515520  56                    push      esi
  00515521  8b742410              mov       esi,[esp+10h]
  00515525  33c0                  xor       eax,eax
  00515527  33d2                  xor       edx,edx
  00515529  8a4603                mov       al,[esi+3]
  0051552c  8a4e02                mov       cl,[esi+2]
  0051552f  8a5601                mov       dl,[esi+1]
  00515532  50                    push      eax
  00515533  51                    push      ecx
  00515534  8b4c2414              mov       ecx,[esp+14h]
  00515538  03d1                  add       edx,ecx
  0051553a  52                    push      edx
  0051553b  e800ebffff            call      00514040h

1 cible(s) de saut interne : 0x00515509
