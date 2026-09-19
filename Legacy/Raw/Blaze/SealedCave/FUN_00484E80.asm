Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484e80  section .text  offset fichier 0x84280  256 octets

  00484e80  53                    push      ebx
  00484e81  55                    push      ebp
  00484e82  56                    push      esi
  00484e83  8b742410              mov       esi,[esp+10h]
  00484e87  57                    push      edi
  00484e88  56                    push      esi
  00484e89  8b4650                mov       eax,[esi+50h]
  00484e8c  8b4e1c                mov       ecx,[esi+1Ch]
  00484e8f  8d7e1c                lea       edi,[esi+1Ch]
  00484e92  8b5e24                mov       ebx,[esi+24h]
  00484e95  8b5654                mov       edx,[esi+54h]
  00484e98  8b6e20                mov       ebp,[esi+20h]
  00484e9b  03c8                  add       ecx,eax
  00484e9d  03ea                  add       ebp,edx
  00484e9f  890f                  mov       [edi],ecx
  00484ea1  8b4e58                mov       ecx,[esi+58h]
  00484ea4  03d9                  add       ebx,ecx
  00484ea6  896e20                mov       [esi+20h],ebp
  00484ea9  895e24                mov       [esi+24h],ebx
  00484eac  8bd8                  mov       ebx,eax
  00484eae  c1fb03                sar       ebx,3
  00484eb1  8be8                  mov       ebp,eax
  00484eb3  2beb                  sub       ebp,ebx
  00484eb5  c1f80c                sar       eax,0Ch
  00484eb8  2be8                  sub       ebp,eax
  00484eba  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00484ec1  896e50                mov       [esi+50h],ebp
  00484ec4  8bd8                  mov       ebx,eax
  00484ec6  8bea                  mov       ebp,edx
  00484ec8  c1fb08                sar       ebx,8
  00484ecb  c1fd0c                sar       ebp,0Ch
  00484ece  2bdd                  sub       ebx,ebp
  00484ed0  2bd8                  sub       ebx,eax
  00484ed2  8bc1                  mov       eax,ecx
  00484ed4  03da                  add       ebx,edx
  00484ed6  8bd1                  mov       edx,ecx
  00484ed8  c1f803                sar       eax,3
  00484edb  2bd0                  sub       edx,eax
  00484edd  895e54                mov       [esi+54h],ebx
  00484ee0  8b5e20                mov       ebx,[esi+20h]
  00484ee3  c1f90c                sar       ecx,0Ch
  00484ee6  2bd1                  sub       edx,ecx
  00484ee8  895658                mov       [esi+58h],edx
  00484eeb  e850ea0a00            call      00533940h
  00484ef0  8b8690000000          mov       eax,[esi+90h]
  00484ef6  83c404                add       esp,4
  00484ef9  f6c4a0                test      ah,0A0h
  00484efc  895e20                mov       [esi+20h],ebx
  00484eff  7563                  jne       short 00484F64h
  00484f01  a902080000            test      eax,802h
  00484f06  7505                  jne       short 00484F0Dh
  00484f08  3b5e60                cmp       ebx,[esi+60h]
  00484f0b  7f57                  jg        short 00484F64h

LAB_00484f0d:
  00484f0d  8b4660                mov       eax,[esi+60h]
  00484f10  33db                  xor       ebx,ebx
  00484f12  894620                mov       [esi+20h],eax
  00484f15  895e28                mov       [esi+28h],ebx
  00484f18  66895e2c              mov       [esi+2Ch],bx
  00484f1c  895e38                mov       [esi+38h],ebx
  00484f1f  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  00484f25  c6460423              mov       byte [esi+4],23h
  00484f29  8b0dcc975e00          mov       ecx,[5E97CCh]
  00484f2f  6a04                  push      4
  00484f31  53                    push      ebx
  00484f32  8b91d0000000          mov       edx,[ecx+0D0h]
  00484f38  33c9                  xor       ecx,ecx
  00484f3a  8b02                  mov       eax,[edx]
  00484f3c  8a480f                mov       cl,[eax+0Fh]
  00484f3f  81c1c0000000          add       ecx,0C0h
  00484f45  51                    push      ecx
  00484f46  56                    push      esi
  00484f47  e8945bffff            call      0047AAE0h
  00484f4c  83c410                add       esp,10h
  00484f4f  57                    push      edi
  00484f50  6820275500            push      552720h
  00484f55  e8868c0b00            call      0053DBE0h
  00484f5a  83c408                add       esp,8
  00484f5d  891e                  mov       [esi],ebx
  00484f5f  5f                    pop       edi
  00484f60  5e                    pop       esi
  00484f61  5d                    pop       ebp
  00484f62  5b                    pop       ebx
  00484f63  c3                    ret

LAB_00484f64:
  00484f64  f70600000020          test      dword [esi],20000000h
  00484f6a  7508                  jne       short 00484F74h
  00484f6c  81fb00001efb          cmp       ebx,0FB1E0000h
  00484f72  7d06                  jge       short 00484F7Ah

LAB_00484f74:
  00484f74  c70600000000          mov       dword [esi],0

LAB_00484f7a:
  00484f7a  5f                    pop       edi
  00484f7b  5e                    pop       esi
  00484f7c  5d                    pop       ebp
  00484f7d  5b                    pop       ebx
  00484f7e  c3                    ret
  00484f7f  90                    nop

4 cible(s) de saut interne : 0x00484f0d, 0x00484f64, 0x00484f74, 0x00484f7a
