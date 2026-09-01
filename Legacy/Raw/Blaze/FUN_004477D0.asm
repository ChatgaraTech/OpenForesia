Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004477d0  section .text  offset fichier 0x46bd0  288 octets

  004477d0  53                    push      ebx
  004477d1  56                    push      esi
  004477d2  8b74240c              mov       esi,[esp+0Ch]
  004477d6  57                    push      edi
  004477d7  8b06                  mov       eax,[esi]
  004477d9  a900000040            test      eax,40000000h
  004477de  7407                  je        short 004477E7h
  004477e0  25ffffff7f            and       eax,7FFFFFFFh
  004477e5  8906                  mov       [esi],eax

LAB_004477e7:
  004477e7  668b4610              mov       ax,[esi+10h]
  004477eb  6685c0                test      ax,ax
  004477ee  0f85aa000000          jne       near 0044789Eh
  004477f4  8b3e                  mov       edi,[esi]
  004477f6  804e3602              or        byte [esi+36h],2
  004477fa  b0ff                  mov       al,0FFh
  004477fc  81cf82060000          or        edi,682h
  00447802  884638                mov       [esi+38h],al
  00447805  884639                mov       [esi+39h],al
  00447808  88463a                mov       [esi+3Ah],al
  0044780b  893e                  mov       [esi],edi
  0044780d  0fbf4632              movsx     eax,word [esi+32h]
  00447811  50                    push      eax
  00447812  66c746280008          mov       word [esi+28h],800h
  00447818  66c7462c000c          mov       word [esi+2Ch],0C00h
  0044781e  66c7462a7c15          mov       word [esi+2Ah],157Ch
  00447824  e8f3e10f00            call      00545A1Ch
  00447829  0fbf4e32              movsx     ecx,word [esi+32h]
  0044782d  8bf8                  mov       edi,eax
  0044782f  51                    push      ecx
  00447830  c1e703                shl       edi,3
  00447833  e8dee10f00            call      00545A16h
  00447838  8b561c                mov       edx,[esi+1Ch]
  0044783b  8b4e24                mov       ecx,[esi+24h]
  0044783e  8d5e1c                lea       ebx,[esi+1Ch]
  00447841  03d7                  add       edx,edi
  00447843  c1e003                shl       eax,3
  00447846  03c8                  add       ecx,eax
  00447848  6a04                  push      4
  0044784a  8913                  mov       [ebx],edx
  0044784c  894e24                mov       [esi+24h],ecx
  0044784f  8b4e6c                mov       ecx,[esi+6Ch]
  00447852  6a00                  push      0
  00447854  8d14ff                lea       edx,[edi+edi*8]
  00447857  8d04c0                lea       eax,[eax+eax*8]
  0044785a  6a51                  push      51h
  0044785c  56                    push      esi
  0044785d  895650                mov       [esi+50h],edx
  00447860  894658                mov       [esi+58h],eax
  00447863  c646041e              mov       byte [esi+4],1Eh
  00447867  894e78                mov       [esi+78h],ecx
  0044786a  e871320300            call      0047AAE0h
  0044786f  668b5612              mov       dx,[esi+12h]
  00447873  83c410                add       esp,10h
  00447876  81e200400000          and       edx,4000h
  0044787c  6685d2                test      dx,dx
  0044787f  7515                  jne       short 00447896h
  00447881  53                    push      ebx
  00447882  6800180000            push      1800h
  00447887  68a60c0000            push      0CA6h
  0044788c  6a23                  push      23h
  0044788e  e88d620f00            call      0053DB20h
  00447893  83c410                add       esp,10h

LAB_00447896:
  00447896  804e1340              or        byte [esi+13h],40h
  0044789a  5f                    pop       edi
  0044789b  5e                    pop       esi
  0044789c  5b                    pop       ebx
  0044789d  c3                    ret

LAB_0044789e:
  0044789e  663d0300              cmp       ax,3
  004478a2  7d37                  jge       short 004478DBh
  004478a4  8a4e38                mov       cl,[esi+38h]
  004478a7  8a5e39                mov       bl,[esi+39h]
  004478aa  8a563a                mov       dl,[esi+3Ah]
  004478ad  b0c0                  mov       al,0C0h
  004478af  02c8                  add       cl,al
  004478b1  6a04                  push      4
  004478b3  884e38                mov       [esi+38h],cl
  004478b6  8b0e                  mov       ecx,[esi]
  004478b8  6a00                  push      0
  004478ba  02d8                  add       bl,al
  004478bc  02d0                  add       dl,al
  004478be  80cd20                or        ch,20h
  004478c1  6a51                  push      51h
  004478c3  56                    push      esi
  004478c4  885e39                mov       [esi+39h],bl
  004478c7  88563a                mov       [esi+3Ah],dl
  004478ca  890e                  mov       [esi],ecx
  004478cc  e80f320300            call      0047AAE0h
  004478d1  8b06                  mov       eax,[esi]
  004478d3  83c410                add       esp,10h
  004478d6  80e4df                and       ah,0DFh
  004478d9  8906                  mov       [esi],eax

LAB_004478db:
  004478db  66ff4610              inc       word [esi+10h]
  004478df  66837e1010            cmp       word [esi+10h],10h
  004478e4  7c06                  jl        short 004478ECh
  004478e6  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004478ec:
  004478ec  5f                    pop       edi
  004478ed  5e                    pop       esi
  004478ee  5b                    pop       ebx
  004478ef  c3                    ret

5 cible(s) de saut interne : 0x004477e7, 0x00447896, 0x0044789e, 0x004478db, 0x004478ec
