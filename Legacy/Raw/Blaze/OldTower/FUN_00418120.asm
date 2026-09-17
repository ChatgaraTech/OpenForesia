C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00418120  section .text  offset fichier 0x17520  368 octets

  00418120  53                    push      ebx
  00418121  56                    push      esi
  00418122  8b74240c              mov       esi,[esp+0Ch]
  00418126  33db                  xor       ebx,ebx
  00418128  66395e10              cmp       [esi+10h],bx
  0041812c  0f85d6000000          jne       near 00418208h
  00418132  66c746100100          mov       word [esi+10h],1
  00418138  c7463c60fb5400        mov       dword [esi+3Ch],54FB60h
  0041813f  885e40                mov       [esi+40h],bl
  00418142  e8f9ef1200            call      00547140h
  00418147  99                    cdq
  00418148  33c2                  xor       eax,edx
  0041814a  c6460a07              mov       byte [esi+0Ah],7
  0041814e  2bc2                  sub       eax,edx
  00418150  885e0b                mov       [esi+0Bh],bl
  00418153  25ff030000            and       eax,3FFh
  00418158  33c2                  xor       eax,edx
  0041815a  2bc2                  sub       eax,edx
  0041815c  05b80b0000            add       eax,0BB8h
  00418161  66894628              mov       [esi+28h],ax
  00418165  6689462a              mov       [esi+2Ah],ax
  00418169  e8d2ef1200            call      00547140h
  0041816e  99                    cdq
  0041816f  b906000000            mov       ecx,6
  00418174  f7f9                  idiv      ecx
  00418176  8b0e                  mov       ecx,[esi]
  00418178  80cd40                or        ch,40h
  0041817b  890e                  mov       [esi],ecx
  0041817d  66895612              mov       [esi+12h],dx
  00418181  e8baef1200            call      00547140h
  00418186  99                    cdq
  00418187  b9c8000000            mov       ecx,0C8h
  0041818c  f7f9                  idiv      ecx
  0041818e  8b461c                mov       eax,[esi+1Ch]
  00418191  83ea64                sub       edx,64h
  00418194  c1e20c                shl       edx,0Ch
  00418197  03c2                  add       eax,edx
  00418199  89461c                mov       [esi+1Ch],eax
  0041819c  e89fef1200            call      00547140h
  004181a1  99                    cdq
  004181a2  b9c8000000            mov       ecx,0C8h
  004181a7  f7f9                  idiv      ecx
  004181a9  8b4e24                mov       ecx,[esi+24h]
  004181ac  83ea64                sub       edx,64h
  004181af  c1e20c                shl       edx,0Ch
  004181b2  03ca                  add       ecx,edx
  004181b4  894e24                mov       [esi+24h],ecx
  004181b7  e884ef1200            call      00547140h
  004181bc  99                    cdq
  004181bd  b90a000000            mov       ecx,0Ah
  004181c2  f7f9                  idiv      ecx
  004181c4  83c20c                add       edx,0Ch
  004181c7  66895614              mov       [esi+14h],dx
  004181cb  e870ef1200            call      00547140h
  004181d0  99                    cdq
  004181d1  b90a000000            mov       ecx,0Ah
  004181d6  f7f9                  idiv      ecx
  004181d8  b8fdffffff            mov       eax,0FFFFFFFDh
  004181dd  2bc2                  sub       eax,edx
  004181df  66894616              mov       [esi+16h],ax
  004181e3  b080                  mov       al,80h
  004181e5  884638                mov       [esi+38h],al
  004181e8  884639                mov       [esi+39h],al
  004181eb  88463a                mov       [esi+3Ah],al
  004181ee  e84def1200            call      00547140h
  004181f3  99                    cdq
  004181f4  b903000000            mov       ecx,3
  004181f9  66899e9a000000        mov       [esi+9Ah],bx
  00418200  f7f9                  idiv      ecx
  00418202  83c202                add       edx,2
  00418205  89565c                mov       [esi+5Ch],edx

LAB_00418208:
  00418208  66837e1001            cmp       word [esi+10h],1
  0041820d  7517                  jne       short 00418226h
  0041820f  66ff4e12              dec       word [esi+12h]
  00418213  66395e12              cmp       [esi+12h],bx
  00418217  7f0d                  jg        short 00418226h
  00418219  8b06                  mov       eax,[esi]
  0041821b  66c746100200          mov       word [esi+10h],2
  00418221  80e4bf                and       ah,0BFh
  00418224  8906                  mov       [esi],eax

LAB_00418226:
  00418226  66837e1002            cmp       word [esi+10h],2
  0041822b  755f                  jne       short 0041828Ch
  0041822d  668b4614              mov       ax,[esi+14h]
  00418231  8b4e20                mov       ecx,[esi+20h]
  00418234  0fbfd0                movsx     edx,ax
  00418237  c1e20c                shl       edx,0Ch
  0041823a  03ca                  add       ecx,edx
  0041823c  48                    dec       eax
  0041823d  66894614              mov       [esi+14h],ax
  00418241  668b4616              mov       ax,[esi+16h]
  00418245  50                    push      eax
  00418246  50                    push      eax
  00418247  50                    push      eax
  00418248  56                    push      esi
  00418249  894e20                mov       [esi+20h],ecx
  0041824c  e88fcbfeff            call      00404DE0h
  00418251  8b4e5c                mov       ecx,[esi+5Ch]
  00418254  83c410                add       esp,10h
  00418257  66ff869a000000        inc       word [esi+9Ah]
  0041825e  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00418265  3bc1                  cmp       eax,ecx
  00418267  7c18                  jl        short 00418281h
  00418269  8a4e40                mov       cl,[esi+40h]
  0041826c  66899e9a000000        mov       [esi+9Ah],bx
  00418273  fec1                  inc       cl
  00418275  8ac1                  mov       al,cl
  00418277  884e40                mov       [esi+40h],cl
  0041827a  3c04                  cmp       al,4
  0041827c  7503                  jne       short 00418281h
  0041827e  885e40                mov       [esi+40h],bl

LAB_00418281:
  00418281  385e38                cmp       [esi+38h],bl
  00418284  7506                  jne       short 0041828Ch
  00418286  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041828c:
  0041828c  5e                    pop       esi
  0041828d  5b                    pop       ebx
  0041828e  c3                    ret
  0041828f  90                    nop

4 cible(s) de saut interne : 0x00418208, 0x00418226, 0x00418281, 0x0041828c
