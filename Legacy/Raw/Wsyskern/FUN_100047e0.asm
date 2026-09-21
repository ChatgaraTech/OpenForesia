
Legacy/Binaries/wsyskern.dll  base 0x10000000
0x100047e0  section .text  offset fichier 0x3be0  112 octets

  100047e0  8b442404              mov       eax,[esp+4]
  100047e4  8b510c                mov       edx,[ecx+0Ch]
  100047e7  83ec20                sub       esp,20h
  100047ea  3bc2                  cmp       eax,edx
  100047ec  7208                  jb        short 100047F6h
  100047ee  33c0                  xor       eax,eax
  100047f0  83c420                add       esp,20h
  100047f3  c20c00                ret       0Ch

LAB_100047f6:
  100047f6  57                    push      edi
  100047f7  56                    push      esi
  100047f8  8b742430              mov       esi,[esp+30h]
  100047fc  c60600                mov       byte [esi],0
  100047ff  8b4910                mov       ecx,[ecx+10h]
  10004802  8b54c104              mov       edx,[ecx+eax*8+4]
  10004806  52                    push      edx
  10004807  8b04c1                mov       eax,[ecx+eax*8]
  1000480a  8d4c240c              lea       ecx,[esp+0Ch]
  1000480e  50                    push      eax
  1000480f  68149a0410            push      10049A14h
  10004814  51                    push      ecx
  10004815  e8e6e50000            call      10012E00h
  1000481a  8b7c2444              mov       edi,[esp+44h]
  1000481e  83c410                add       esp,10h
  10004821  8d442408              lea       eax,[esp+8]
  10004825  8d57ff                lea       edx,[edi-1]
  10004828  52                    push      edx
  10004829  50                    push      eax
  1000482a  56                    push      esi
  1000482b  e890e20000            call      10012AC0h
  10004830  83c40c                add       esp,0Ch
  10004833  c6443eff00            mov       byte [esi+edi-1],0
  10004838  b801000000            mov       eax,1
  1000483d  5e                    pop       esi
  1000483e  5f                    pop       edi
  1000483f  83c420                add       esp,20h
  10004842  c20c00                ret       0Ch
  10004845  90                    nop
  10004846  90                    nop
  10004847  90                    nop
  10004848  90                    nop
  10004849  90                    nop
  1000484a  90                    nop
  1000484b  90                    nop
  1000484c  90                    nop
  1000484d  90                    nop
  1000484e  90                    nop
  1000484f  90                    nop

1 cible(s) de saut interne : 0x100047f6
