Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004439a0  section .text  offset fichier 0x42da0  84 octets

  004439a0  8a0d00905e00          mov       cl,[5E9000h]
  004439a6  33c0                  xor       eax,eax
  004439a8  f6c101                test      cl,1
  004439ab  7405                  je        short 004439B2h
  004439ad  b801000000            mov       eax,1

LAB_004439b2:
  004439b2  f6c102                test      cl,2
  004439b5  7401                  je        short 004439B8h
  004439b7  40                    inc       eax

LAB_004439b8:
  004439b8  f6c104                test      cl,4
  004439bb  7401                  je        short 004439BEh
  004439bd  40                    inc       eax

LAB_004439be:
  004439be  f6c108                test      cl,8
  004439c1  7401                  je        short 004439C4h
  004439c3  40                    inc       eax

LAB_004439c4:
  004439c4  f6c110                test      cl,10h
  004439c7  7401                  je        short 004439CAh
  004439c9  40                    inc       eax

LAB_004439ca:
  004439ca  f6c120                test      cl,20h
  004439cd  7401                  je        short 004439D0h
  004439cf  40                    inc       eax

LAB_004439d0:
  004439d0  f6c140                test      cl,40h
  004439d3  7401                  je        short 004439D6h
  004439d5  40                    inc       eax

LAB_004439d6:
  004439d6  f6c180                test      cl,80h
  004439d9  7401                  je        short 004439DCh
  004439db  40                    inc       eax

LAB_004439dc:
  004439dc  f60501905e0001        test      byte [5E9001h],1
  004439e3  7401                  je        short 004439E6h
  004439e5  40                    inc       eax

LAB_004439e6:
  004439e6  33c9                  xor       ecx,ecx
  004439e8  6639442408            cmp       [esp+8],ax
  004439ed  0f9fc1                setg      cl
  004439f0  8bc1                  mov       eax,ecx
  004439f2  c3                    ret
  004439f3  90                    nop

9 cible(s) de saut interne : 0x004439b2, 0x004439b8, 0x004439be, 0x004439c4, 0x004439ca, 0x004439d0, 0x004439d6, 0x004439dc, 0x004439e6
