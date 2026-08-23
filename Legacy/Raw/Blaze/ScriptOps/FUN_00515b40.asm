Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x00515b40  section .text  offset fichier 0x114f40  128 octets

  00515b40  8b442408              mov       eax,[esp+8]
  00515b44  56                    push      esi
  00515b45  8b742410              mov       esi,[esp+10h]
  00515b49  8b08                  mov       ecx,[eax]
  00515b4b  f7c10000c000          test      ecx,0C00000h
  00515b51  7458                  je        short 00515BABh
  00515b53  f7c100008000          test      ecx,800000h
  00515b59  7419                  je        short 00515B74h
  00515b5b  8b15b4876000          mov       edx,[6087B4h]
  00515b61  33c9                  xor       ecx,ecx
  00515b63  8a4808                mov       cl,[eax+8]
  00515b66  8b02                  mov       eax,[edx]
  00515b68  c1e10d                shl       ecx,0Dh
  00515b6b  8d840100010f00        lea       eax,[ecx+eax+0F0100h]
  00515b72  eb12                  jmp       short 00515B86h

LAB_00515b74:
  00515b74  33c9                  xor       ecx,ecx
  00515b76  8a4808                mov       cl,[eax+8]
  00515b79  8bc1                  mov       eax,ecx
  00515b7b  8d04c0                lea       eax,[eax+eax*8]
  00515b7e  c1e005                shl       eax,5
  00515b81  0560a55e00            add       eax,5EA560h

LAB_00515b86:
  00515b86  33d2                  xor       edx,edx
  00515b88  8a4e02                mov       cl,[esi+2]
  00515b8b  8a5603                mov       dl,[esi+3]
  00515b8e  52                    push      edx
  00515b8f  33d2                  xor       edx,edx
  00515b91  8a5601                mov       dl,[esi+1]
  00515b94  51                    push      ecx
  00515b95  03d0                  add       edx,eax
  00515b97  52                    push      edx
  00515b98  e8a3e4ffff            call      00514040h
  00515b9d  8b4c2420              mov       ecx,[esp+20h]
  00515ba1  83c40c                add       esp,0Ch
  00515ba4  8901                  mov       [ecx],eax
  00515ba6  8d4604                lea       eax,[esi+4]
  00515ba9  5e                    pop       esi
  00515baa  c3                    ret

LAB_00515bab:
  00515bab  8b542414              mov       edx,[esp+14h]
  00515baf  8d4604                lea       eax,[esi+4]
  00515bb2  5e                    pop       esi
  00515bb3  c70201000000          mov       dword [edx],1
  00515bb9  c3                    ret
  00515bba  90                    nop
  00515bbb  90                    nop
  00515bbc  90                    nop
  00515bbd  90                    nop
  00515bbe  90                    nop
  00515bbf  90                    nop

3 cible(s) de saut interne : 0x00515b74, 0x00515b86, 0x00515bab
