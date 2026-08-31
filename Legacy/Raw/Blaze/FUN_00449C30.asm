Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449c30  section .text  offset fichier 0x49030  448 octets

  00449c30  56                    push      esi
  00449c31  8b742408              mov       esi,[esp+8]
  00449c35  57                    push      edi
  00449c36  6a00                  push      0
  00449c38  6a00                  push      0
  00449c3a  56                    push      esi
  00449c3b  e800e4ffff            call      00448040h
  00449c40  83c40c                add       esp,0Ch
  00449c43  66837e1000            cmp       word [esi+10h],0
  00449c48  750d                  jne       short 00449C57h
  00449c4a  6a00                  push      0
  00449c4c  6a00                  push      0
  00449c4e  56                    push      esi
  00449c4f  e85ce0ffff            call      00447CB0h
  00449c54  83c40c                add       esp,0Ch

LAB_00449c57:
  00449c57  8b4678                mov       eax,[esi+78h]
  00449c5a  85c0                  test      eax,eax
  00449c5c  7505                  jne       short 00449C63h
  00449c5e  b858e55d00            mov       eax,5DE558h

LAB_00449c63:
  00449c63  8b481c                mov       ecx,[eax+1Ch]
  00449c66  8d7e1c                lea       edi,[esi+1Ch]
  00449c69  66837e1000            cmp       word [esi+10h],0
  00449c6e  890f                  mov       [edi],ecx
  00449c70  8b5020                mov       edx,[eax+20h]
  00449c73  895620                mov       [esi+20h],edx
  00449c76  8b4824                mov       ecx,[eax+24h]
  00449c79  894e24                mov       [esi+24h],ecx
  00449c7c  668b5032              mov       dx,[eax+32h]
  00449c80  66895632              mov       [esi+32h],dx
  00449c84  7542                  jne       short 00449CC8h
  00449c86  6aff                  push      0FFFFFFFFh
  00449c88  6a00                  push      0
  00449c8a  68a9000000            push      0A9h
  00449c8f  56                    push      esi
  00449c90  66c746100100          mov       word [esi+10h],1
  00449c96  66c746160000          mov       word [esi+16h],0
  00449c9c  66c7869a0000000600    mov       word [esi+9Ah],6
  00449ca5  66c746140010          mov       word [esi+14h],1000h
  00449cab  c7464400100000        mov       dword [esi+44h],1000h
  00449cb2  c7464846000000        mov       dword [esi+48h],46h
  00449cb9  c7465c2c010000        mov       dword [esi+5Ch],12Ch
  00449cc0  e81b0e0300            call      0047AAE0h
  00449cc5  83c410                add       esp,10h

LAB_00449cc8:
  00449cc8  66837e1600            cmp       word [esi+16h],0
  00449ccd  7519                  jne       short 00449CE8h
  00449ccf  6800120000            push      1200h
  00449cd4  6800120000            push      1200h
  00449cd9  686a010000            push      16Ah
  00449cde  6a30                  push      30h
  00449ce0  e8bb310f00            call      0053CEA0h
  00449ce5  83c410                add       esp,10h

LAB_00449ce8:
  00449ce8  66837e1601            cmp       word [esi+16h],1
  00449ced  7519                  jne       short 00449D08h
  00449cef  6800120000            push      1200h
  00449cf4  6800120000            push      1200h
  00449cf9  6800020000            push      200h
  00449cfe  6a10                  push      10h
  00449d00  e89b310f00            call      0053CEA0h
  00449d05  83c410                add       esp,10h

LAB_00449d08:
  00449d08  66837e1615            cmp       word [esi+16h],15h
  00449d0d  7519                  jne       short 00449D28h
  00449d0f  6800100000            push      1000h
  00449d14  6800100000            push      1000h
  00449d19  6800010000            push      100h
  00449d1e  6a16                  push      16h
  00449d20  e87b310f00            call      0053CEA0h
  00449d25  83c410                add       esp,10h

LAB_00449d28:
  00449d28  66837e1617            cmp       word [esi+16h],17h
  00449d2d  7519                  jne       short 00449D48h
  00449d2f  6800100000            push      1000h
  00449d34  6800100000            push      1000h
  00449d39  682a010000            push      12Ah
  00449d3e  6a16                  push      16h
  00449d40  e85b310f00            call      0053CEA0h
  00449d45  83c410                add       esp,10h

LAB_00449d48:
  00449d48  668b4616              mov       ax,[esi+16h]
  00449d4c  663d1400              cmp       ax,14h
  00449d50  7e5f                  jle       short 00449DB1h
  00449d52  663d2800              cmp       ax,28h
  00449d56  7d59                  jge       short 00449DB1h
  00449d58  a801                  test      al,1
  00449d5a  7455                  je        short 00449DB1h
  00449d5c  6aff                  push      0FFFFFFFFh
  00449d5e  6a00                  push      0
  00449d60  688d000000            push      8Dh
  00449d65  6a00                  push      0
  00449d67  57                    push      edi
  00449d68  56                    push      esi
  00449d69  66c7869a0000000600    mov       word [esi+9Ah],6
  00449d72  66c746141e00          mov       word [esi+14h],1Eh
  00449d78  c746445a000000        mov       dword [esi+44h],5Ah
  00449d7f  c7465c78000000        mov       dword [esi+5Ch],78h
  00449d86  c7464800000000        mov       dword [esi+48h],0
  00449d8d  e8ae0e0300            call      0047AC40h
  00449d92  83c418                add       esp,18h
  00449d95  c7464800080000        mov       dword [esi+48h],800h
  00449d9c  6aff                  push      0FFFFFFFFh
  00449d9e  6a00                  push      0
  00449da0  688d000000            push      8Dh
  00449da5  6a00                  push      0
  00449da7  57                    push      edi
  00449da8  56                    push      esi
  00449da9  e8920e0300            call      0047AC40h
  00449dae  83c418                add       esp,18h

LAB_00449db1:
  00449db1  66ff4616              inc       word [esi+16h]
  00449db5  66837e1664            cmp       word [esi+16h],64h
  00449dba  7c31                  jl        short 00449DEDh
  00449dbc  8b4674                mov       eax,[esi+74h]
  00449dbf  8a4e06                mov       cl,[esi+6]
  00449dc2  6a00                  push      0
  00449dc4  884806                mov       [eax+6],cl
  00449dc7  8b5674                mov       edx,[esi+74h]
  00449dca  8a4607                mov       al,[esi+7]
  00449dcd  884207                mov       [edx+7],al
  00449dd0  8b4e78                mov       ecx,[esi+78h]
  00449dd3  8b5674                mov       edx,[esi+74h]
  00449dd6  51                    push      ecx
  00449dd7  52                    push      edx
  00449dd8  e813000000            call      00449DF0h
  00449ddd  83c40c                add       esp,0Ch
  00449de0  6a00                  push      0
  00449de2  6a00                  push      0
  00449de4  56                    push      esi
  00449de5  e836e2ffff            call      00448020h
  00449dea  83c40c                add       esp,0Ch

LAB_00449ded:
  00449ded  5f                    pop       edi
  00449dee  5e                    pop       esi
  00449def  c3                    ret

9 cible(s) de saut interne : 0x00449c57, 0x00449c63, 0x00449cc8, 0x00449ce8, 0x00449d08, 0x00449d28, 0x00449d48, 0x00449db1, 0x00449ded
