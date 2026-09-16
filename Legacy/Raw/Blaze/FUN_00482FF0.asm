Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00482ff0  section .text  offset fichier 0x823f0  352 octets

  00482ff0  83ec0c                sub       esp,0Ch
  00482ff3  56                    push      esi
  00482ff4  8b742414              mov       esi,[esp+14h]
  00482ff8  57                    push      edi
  00482ff9  66c74424080000        mov       word [esp+8],0
  00483000  0fbf7e10              movsx     edi,word [esi+10h]
  00483004  66c744240a0000        mov       word [esp+0Ah],0
  0048300b  66c744240c3c00        mov       word [esp+0Ch],3Ch
  00483012  66c744240ec800        mov       word [esp+0Eh],0C8h
  00483019  66c74424100000        mov       word [esp+10h],0
  00483020  66c74424127800        mov       word [esp+12h],78h
  00483027  e814410c00            call      00547140h
  0048302c  8bcf                  mov       ecx,edi
  0048302e  25ff030000            and       eax,3FFh
  00483033  0fafcf                imul      ecx,edi
  00483036  83c108                add       ecx,8
  00483039  8b7e1c                mov       edi,[esi+1Ch]
  0048303c  c1e107                shl       ecx,7
  0048303f  03c1                  add       eax,ecx
  00483041  8b4e54                mov       ecx,[esi+54h]
  00483044  8bd0                  mov       edx,eax
  00483046  6689462c              mov       [esi+2Ch],ax
  0048304a  c1e211                shl       edx,11h
  0048304d  0bd0                  or        edx,eax
  0048304f  8b4650                mov       eax,[esi+50h]
  00483052  895628                mov       [esi+28h],edx
  00483055  8b5620                mov       edx,[esi+20h]
  00483058  03f8                  add       edi,eax
  0048305a  03d1                  add       edx,ecx
  0048305c  8b4e24                mov       ecx,[esi+24h]
  0048305f  897e1c                mov       [esi+1Ch],edi
  00483062  895620                mov       [esi+20h],edx
  00483065  8bfa                  mov       edi,edx
  00483067  8b5658                mov       edx,[esi+58h]
  0048306a  56                    push      esi
  0048306b  03ca                  add       ecx,edx
  0048306d  894e24                mov       [esi+24h],ecx
  00483070  e8cb080b00            call      00533940h
  00483075  8b4664                mov       eax,[esi+64h]
  00483078  83c404                add       esp,4
  0048307b  3bf8                  cmp       edi,eax
  0048307d  897e20                mov       [esi+20h],edi
  00483080  7f0d                  jg        short 0048308Fh
  00483082  894620                mov       [esi+20h],eax
  00483085  c7869000000000080000  mov       dword [esi+90h],800h

LAB_0048308f:
  0048308f  f7869000000002880000  test      dword [esi+90h],8802h
  00483099  7544                  jne       short 004830DFh
  0048309b  8d442408              lea       eax,[esp+8]
  0048309f  50                    push      eax
  004830a0  56                    push      esi
  004830a1  e8ca1bffff            call      00474C70h
  004830a6  8bf8                  mov       edi,eax
  004830a8  83c408                add       esp,8
  004830ab  85ff                  test      edi,edi
  004830ad  7425                  je        short 004830D4h
  004830af  a1b8e05d00            mov       eax,[5DE0B8h]
  004830b4  85c0                  test      eax,eax
  004830b6  7427                  je        short 004830DFh
  004830b8  8b4e74                mov       ecx,[esi+74h]
  004830bb  57                    push      edi
  004830bc  51                    push      ecx
  004830bd  e8be6a0b00            call      00539B80h
  004830c2  8b07                  mov       eax,[edi]
  004830c4  83c408                add       esp,8
  004830c7  0c40                  or        al,40h
  004830c9  66c787960000001000    mov       word [edi+96h],10h
  004830d2  8907                  mov       [edi],eax

LAB_004830d4:
  004830d4  66ff4610              inc       word [esi+10h]
  004830d8  66837e1008            cmp       word [esi+10h],8
  004830dd  7c06                  jl        short 004830E5h

LAB_004830df:
  004830df  c70600000000          mov       dword [esi],0

LAB_004830e5:
  004830e5  5f                    pop       edi
  004830e6  5e                    pop       esi
  004830e7  83c40c                add       esp,0Ch
  004830ea  c3                    ret
  004830eb  90                    nop
  004830ec  90                    nop
  004830ed  90                    nop
  004830ee  90                    nop
  004830ef  90                    nop
  004830f0  83ec0c                sub       esp,0Ch
  004830f3  53                    push      ebx
  004830f4  56                    push      esi
  004830f5  8b742418              mov       esi,[esp+18h]
  004830f9  57                    push      edi
  004830fa  8b06                  mov       eax,[esi]
  004830fc  8b7e6c                mov       edi,[esi+6Ch]
  004830ff  a900000001            test      eax,1000000h
  00483104  897e74                mov       [esi+74h],edi
  00483107  7473                  je        short 0048317Ch
  00483109  a802                  test      al,2
  0048310b  7456                  je        short 00483163h
  0048310d  a804                  test      al,4
  0048310f  7407                  je        short 00483118h
  00483111  0d00000002            or        eax,2000000h
  00483116  eb49                  jmp       short 00483161h

LAB_00483118:
  00483118  66ff4610              inc       word [esi+10h]
  0048311c  66837e1028            cmp       word [esi+10h],28h
  00483121  7d3a                  jge       short 0048315Dh
  00483123  e818400c00            call      00547140h
  00483128  83e03f                and       eax,3Fh
  0048312b  66894612              mov       [esi+12h],ax
  0048312f  e80c400c00            call      00547140h
  00483134  66894632              mov       [esi+32h],ax
  00483138  a1cc975e00            mov       eax,[5E97CCh]
  0048313d  6a02                  push      2
  0048313f  6a00                  push      0
  00483141  8b88d0000000          mov       ecx,[eax+0D0h]
  00483147  33c0                  xor       eax,eax
  00483149  8b11                  mov       edx,[ecx]
  0048314b  8a4210                mov       al,[edx+10h]
  0048314e  05c0                  (bad)
  (octet invalide - la plage demandee deborde probablement du code)
