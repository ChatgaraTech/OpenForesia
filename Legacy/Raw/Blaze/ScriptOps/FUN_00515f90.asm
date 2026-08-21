
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515f90  section .text  offset fichier 0x115390  96 octets

  00515f90  e8bbdfffff            call      00513F50h
  00515f95  8b44240c              mov       eax,[esp+0Ch]
  00515f99  40                    inc       eax
  00515f9a  c3                    ret
  00515f9b  90                    nop
  00515f9c  90                    nop
  00515f9d  90                    nop
  00515f9e  90                    nop
  00515f9f  90                    nop
  00515fa0  8b442408              mov       eax,[esp+8]
  00515fa4  56                    push      esi
  00515fa5  8b742410              mov       esi,[esp+10h]
  00515fa9  8b08                  mov       ecx,[eax]
  00515fab  f7c10000c000          test      ecx,0C00000h
  00515fb1  7458                  je        short 0051600Bh
  00515fb3  f7c100008000          test      ecx,800000h
  00515fb9  7419                  je        short 00515FD4h
  00515fbb  8b15b4876000          mov       edx,[6087B4h]
  00515fc1  33c9                  xor       ecx,ecx
  00515fc3  8a4808                mov       cl,[eax+8]
  00515fc6  8b02                  mov       eax,[edx]
  00515fc8  c1e10d                shl       ecx,0Dh
  00515fcb  8d840100020f00        lea       eax,[ecx+eax+0F0200h]
  00515fd2  eb12                  jmp       short 00515FE6h

LAB_00515fd4:
  00515fd4  33c9                  xor       ecx,ecx
  00515fd6  8a4808                mov       cl,[eax+8]
  00515fd9  8bc1                  mov       eax,ecx
  00515fdb  8d04c0                lea       eax,[eax+eax*8]
  00515fde  c1e005                shl       eax,5
  00515fe1  0560a65e00            add       eax,5EA660h

LAB_00515fe6:
  00515fe6  33d2                  xor       edx,edx
  00515fe8  8a4e02                mov       cl,[esi+2]
  00515feb  8a5603                mov       dl,[esi+3]
  00515fee  52                    push      edx
  00515fef  33                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
