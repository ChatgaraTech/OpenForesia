
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047ef60  section .text  offset fichier 0x7e360  260 octets

  0047ef60  83ec0c                sub       esp,0Ch
  0047ef63  56                    push      esi
  0047ef64  8b742414              mov       esi,[esp+14h]
  0047ef68  57                    push      edi
  0047ef69  66c74424080000        mov       word [esp+8],0
  0047ef70  0fbf7e10              movsx     edi,word [esi+10h]
  0047ef74  66c744240a0000        mov       word [esp+0Ah],0
  0047ef7b  66c744240c3c00        mov       word [esp+0Ch],3Ch
  0047ef82  66c744240ec800        mov       word [esp+0Eh],0C8h
  0047ef89  66c74424100000        mov       word [esp+10h],0
  0047ef90  66c74424127800        mov       word [esp+12h],78h
  0047ef97  e8a4810c00            call      00547140h
  0047ef9c  8bcf                  mov       ecx,edi
  0047ef9e  25ff030000            and       eax,3FFh
  0047efa3  0fafcf                imul      ecx,edi
  0047efa6  83c108                add       ecx,8
  0047efa9  8b7e1c                mov       edi,[esi+1Ch]
  0047efac  c1e107                shl       ecx,7
  0047efaf  03c1                  add       eax,ecx
  0047efb1  8b4e54                mov       ecx,[esi+54h]
  0047efb4  8bd0                  mov       edx,eax
  0047efb6  6689462c              mov       [esi+2Ch],ax
  0047efba  c1e211                shl       edx,11h
  0047efbd  0bd0                  or        edx,eax
  0047efbf  8b4650                mov       eax,[esi+50h]
  0047efc2  895628                mov       [esi+28h],edx
  0047efc5  8b5620                mov       edx,[esi+20h]
  0047efc8  03f8                  add       edi,eax
  0047efca  03d1                  add       edx,ecx
  0047efcc  8b4e24                mov       ecx,[esi+24h]
  0047efcf  897e1c                mov       [esi+1Ch],edi
  0047efd2  895620                mov       [esi+20h],edx
  0047efd5  8bfa                  mov       edi,edx
  0047efd7  8b5658                mov       edx,[esi+58h]
  0047efda  56                    push      esi
  0047efdb  03ca                  add       ecx,edx
  0047efdd  c6463bc0              mov       byte [esi+3Bh],0C0h
  0047efe1  894e24                mov       [esi+24h],ecx
  0047efe4  e857490b00            call      00533940h
  0047efe9  8b4664                mov       eax,[esi+64h]
  0047efec  83c404                add       esp,4
  0047efef  3bf8                  cmp       edi,eax
  0047eff1  897e20                mov       [esi+20h],edi
  0047eff4  7f0d                  jg        short 0047F003h
  0047eff6  894620                mov       [esi+20h],eax
  0047eff9  c7869000000000080000  mov       dword [esi+90h],800h

LAB_0047f003:
  0047f003  f7869000000002880000  test      dword [esi+90h],8802h
  0047f00d  7549                  jne       short 0047F058h
  0047f00f  8d442408              lea       eax,[esp+8]
  0047f013  50                    push      eax
  0047f014  56                    push      esi
  0047f015  e8865cffff            call      00474CA0h
  0047f01a  8bf8                  mov       edi,eax
  0047f01c  83c408                add       esp,8
  0047f01f  85ff                  test      edi,edi
  0047f021  742a                  je        short 0047F04Dh
  0047f023  8b4674                mov       eax,[esi+74h]
  0047f026  33c9                  xor       ecx,ecx
  0047f028  8a4e06                mov       cl,[esi+6]
  0047f02b  33d2                  xor       edx,edx
  0047f02d  8a5607                mov       dl,[esi+7]
  0047f030  6a00                  push      0
  0047f032  51                    push      ecx
  0047f033  52                    push      edx
  0047f034  57                    push      edi
  0047f035  50                    push      eax
  0047f036  e875b20b00            call      0053A2B0h
  0047f03b  8b07                  mov       eax,[edi]
  0047f03d  83c414                add       esp,14h
  0047f040  0c40                  or        al,40h
  0047f042  66c787960000001000    mov       word [edi+96h],10h
  0047f04b  8907                  mov       [edi],eax

LAB_0047f04d:
  0047f04d  66ff4610              inc       word [esi+10h]
  0047f051  66837e1008            cmp       word [esi+10h],8
  0047f056  7c06                  jl        short 0047F05Eh

LAB_0047f058:
  0047f058  c70600000000          mov       dword [esi],0

LAB_0047f05e:
  0047f05e  5f                    pop       edi
  0047f05f  5e                    pop       esi
  0047f060  83c40c                add       esp,0Ch
  0047f063  c3                    ret

4 cible(s) de saut interne : 0x0047f003, 0x0047f04d, 0x0047f058, 0x0047f05e
