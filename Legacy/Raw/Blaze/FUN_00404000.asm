Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00404000  section .text  offset fichier 0x3400  440 octets

  00404000  53                    push      ebx
  00404001  56                    push      esi
  00404002  8b74240c              mov       esi,[esp+0Ch]
  00404006  33db                  xor       ebx,ebx
  00404008  57                    push      edi
  00404009  bf01000000            mov       edi,1
  0040400e  66395e10              cmp       [esi+10h],bx
  00404012  0f85ca000000          jne       near 004040E2h
  00404018  66897e10              mov       [esi+10h],di
  0040401c  c7463ce0f05400        mov       dword [esi+3Ch],54F0E0h
  00404023  885e40                mov       [esi+40h],bl
  00404026  c6460a02              mov       byte [esi+0Ah],2
  0040402a  885e0b                mov       [esi+0Bh],bl
  0040402d  e80e311400            call      00547140h
  00404032  99                    cdq
  00404033  33c2                  xor       eax,edx
  00404035  66899e98000000        mov       [esi+98h],bx
  0040403c  2bc2                  sub       eax,edx
  0040403e  25ff0f0000            and       eax,0FFFh
  00404043  33c2                  xor       eax,edx
  00404045  2bc2                  sub       eax,edx
  00404047  66894612              mov       [esi+12h],ax
  0040404b  e8f0301400            call      00547140h
  00404050  99                    cdq
  00404051  33c2                  xor       eax,edx
  00404053  8b4e24                mov       ecx,[esi+24h]
  00404056  2bc2                  sub       eax,edx
  00404058  894e58                mov       [esi+58h],ecx
  0040405b  25ff030000            and       eax,3FFh
  00404060  33c2                  xor       eax,edx
  00404062  2bc2                  sub       eax,edx
  00404064  05000c0000            add       eax,0C00h
  00404069  66894628              mov       [esi+28h],ax
  0040406d  6689462a              mov       [esi+2Ah],ax
  00404071  8b461c                mov       eax,[esi+1Ch]
  00404074  894650                mov       [esi+50h],eax
  00404077  e8c4301400            call      00547140h
  0040407c  99                    cdq
  0040407d  33c2                  xor       eax,edx
  0040407f  66895e16              mov       [esi+16h],bx
  00404083  2bc2                  sub       eax,edx
  00404085  66899e9a000000        mov       [esi+9Ah],bx
  0040408c  23c7                  and       eax,edi
  0040408e  33c2                  xor       eax,edx
  00404090  2bc2                  sub       eax,edx
  00404092  40                    inc       eax
  00404093  894654                mov       [esi+54h],eax
  00404096  e8a5301400            call      00547140h
  0040409b  99                    cdq
  0040409c  b919000000            mov       ecx,19h
  004040a1  f7f9                  idiv      ecx
  004040a3  80c220                add       dl,20h
  004040a6  885638                mov       [esi+38h],dl
  004040a9  e892301400            call      00547140h
  004040ae  99                    cdq
  004040af  b919000000            mov       ecx,19h
  004040b4  f7f9                  idiv      ecx
  004040b6  80c23c                add       dl,3Ch
  004040b9  885639                mov       [esi+39h],dl
  004040bc  e87f301400            call      00547140h
  004040c1  99                    cdq
  004040c2  b919000000            mov       ecx,19h
  004040c7  f7f9                  idiv      ecx
  004040c9  80c240                add       dl,40h
  004040cc  88563a                mov       [esi+3Ah],dl
  004040cf  e86c301400            call      00547140h
  004040d4  99                    cdq
  004040d5  b90f000000            mov       ecx,0Fh
  004040da  f7f9                  idiv      ecx
  004040dc  83c219                add       edx,19h
  004040df  89565c                mov       [esi+5Ch],edx

LAB_004040e2:
  004040e2  66397e10              cmp       [esi+10h],di
  004040e6  0f85c7000000          jne       near 004041B3h
  004040ec  8b4e54                mov       ecx,[esi+54h]
  004040ef  8b7e20                mov       edi,[esi+20h]
  004040f2  8bd1                  mov       edx,ecx
  004040f4  b889888888            mov       eax,88888889h
  004040f9  c1e20a                shl       edx,0Ah
  004040fc  03fa                  add       edi,edx
  004040fe  897e20                mov       [esi+20h],edi
  00404101  0fbf7e14              movsx     edi,word [esi+14h]
  00404105  f7ef                  imul      edi
  00404107  03d7                  add       edx,edi
  00404109  c1fa06                sar       edx,6
  0040410c  8bc2                  mov       eax,edx
  0040410e  c1e81f                shr       eax,1Fh
  00404111  03d0                  add       edx,eax
  00404113  8d4c0a01              lea       ecx,[edx+ecx+1]
  00404117  0fbf5612              movsx     edx,word [esi+12h]
  0040411b  52                    push      edx
  0040411c  894e54                mov       [esi+54h],ecx
  0040411f  e8f8181400            call      00545A1Ch
  00404124  0fbf8e98000000        movsx     ecx,word [esi+98h]
  0040412b  0fafc1                imul      eax,ecx
  0040412e  0fbf5612              movsx     edx,word [esi+12h]
  00404132  8b4e50                mov       ecx,[esi+50h]
  00404135  52                    push      edx
  00404136  03c1                  add       eax,ecx
  00404138  89461c                mov       [esi+1Ch],eax
  0040413b  e8d6181400            call      00545A16h
  00404140  0fbf8e98000000        movsx     ecx,word [esi+98h]
  00404147  0fafc1                imul      eax,ecx
  0040414a  8b5658                mov       edx,[esi+58h]
  0040414d  6683461264            add       word [esi+12h],64h
  00404152  03c2                  add       eax,edx
  00404154  66ff869a000000        inc       word [esi+9Ah]
  0040415b  668b8e9a000000        mov       cx,[esi+9Ah]
  00404162  894624                mov       [esi+24h],eax
  00404165  668b4614              mov       ax,[esi+14h]
  00404169  8d5050                lea       edx,[eax+50h]
  0040416c  83c050                add       eax,50h
  0040416f  66015628              add       [esi+28h],dx
  00404173  6601462a              add       [esi+2Ah],ax
  00404177  663b4e16              cmp       cx,[esi+16h]
  0040417b  7c18                  jl        short 00404195h
  0040417d  8a4e40                mov       cl,[esi+40h]
  00404180  66899e9a000000        mov       [esi+9Ah],bx
  00404187  fec1                  inc       cl
  00404189  8ac1                  mov       al,cl
  0040418b  884e40                mov       [esi+40h],cl
  0040418e  3c07                  cmp       al,7
  00404190  7503                  jne       short 00404195h
  00404192  885e40                mov       [esi+40h],bl

LAB_00404195:
  00404195  6afe                  push      0FFFFFFFEh
  00404197  6aff                  push      0FFFFFFFFh
  00404199  6afe                  push      0FFFFFFFEh
  0040419b  56                    push      esi
  0040419c  e83f0c0000            call      00404DE0h
  004041a1  8b465c                mov       eax,[esi+5Ch]
  004041a4  83c410                add       esp,10h
  004041a7  48                    dec       eax
  004041a8  89465c                mov       [esi+5Ch],eax
  004041ab  7906                  jns       short 004041B3h
  004041ad  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004041b3:
  004041b3  5f                    pop       edi
  004041b4  5e                    pop       esi
  004041b5  5b                    pop       ebx
  004041b6  c3                    ret
  004041b7  90                    nop

3 cible(s) de saut interne : 0x004040e2, 0x00404195, 0x004041b3
