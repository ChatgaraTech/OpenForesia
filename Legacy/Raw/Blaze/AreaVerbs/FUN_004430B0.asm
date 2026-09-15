Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004430b0  section .text  offset fichier 0x424b0  78 octets

  004430b0  8a0d00905e00          mov       cl,[5E9000h]
  004430b6  33c0                  xor       eax,eax
  004430b8  f6c101                test      cl,1
  004430bb  7405                  je        short 004430C2h
  004430bd  b801000000            mov       eax,1

LAB_004430c2:
  004430c2  f6c102                test      cl,2
  004430c5  7401                  je        short 004430C8h
  004430c7  40                    inc       eax

LAB_004430c8:
  004430c8  f6c104                test      cl,4
  004430cb  7401                  je        short 004430CEh
  004430cd  40                    inc       eax

LAB_004430ce:
  004430ce  f6c108                test      cl,8
  004430d1  7401                  je        short 004430D4h
  004430d3  40                    inc       eax

LAB_004430d4:
  004430d4  f6c110                test      cl,10h
  004430d7  7401                  je        short 004430DAh
  004430d9  40                    inc       eax

LAB_004430da:
  004430da  f6c120                test      cl,20h
  004430dd  7401                  je        short 004430E0h
  004430df  40                    inc       eax

LAB_004430e0:
  004430e0  f6c140                test      cl,40h
  004430e3  7401                  je        short 004430E6h
  004430e5  40                    inc       eax

LAB_004430e6:
  004430e6  f6c180                test      cl,80h
  004430e9  7401                  je        short 004430ECh
  004430eb  40                    inc       eax

LAB_004430ec:
  004430ec  f60501905e0001        test      byte [5E9001h],1
  004430f3  7401                  je        short 004430F6h
  004430f5  40                    inc       eax

LAB_004430f6:
  004430f6  a26a905e00            mov       [5E906Ah],al
  004430fb  33c0                  xor       eax,eax
  004430fd  c3                    ret

9 cible(s) de saut interne : 0x004430c2, 0x004430c8, 0x004430ce, 0x004430d4, 0x004430da, 0x004430e0, 0x004430e6, 0x004430ec, 0x004430f6
