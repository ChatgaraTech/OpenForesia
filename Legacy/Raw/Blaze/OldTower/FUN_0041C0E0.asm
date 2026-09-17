C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041c0e0  section .text  offset fichier 0x1b4e0  368 octets

  0041c0e0  53                    push      ebx
  0041c0e1  56                    push      esi
  0041c0e2  8b74240c              mov       esi,[esp+0Ch]
  0041c0e6  33db                  xor       ebx,ebx
  0041c0e8  57                    push      edi
  0041c0e9  66395e10              cmp       [esi+10h],bx
  0041c0ed  0f8598000000          jne       near 0041C18Bh
  0041c0f3  66c746100100          mov       word [esi+10h],1
  0041c0f9  c7463c80fd5400        mov       dword [esi+3Ch],54FD80h
  0041c100  c6460a02              mov       byte [esi+0Ah],2
  0041c104  885e0b                mov       [esi+0Bh],bl
  0041c107  885e40                mov       [esi+40h],bl
  0041c10a  e831b01200            call      00547140h
  0041c10f  99                    cdq
  0041c110  b950000000            mov       ecx,50h
  0041c115  8b7e20                mov       edi,[esi+20h]
  0041c118  f7f9                  idiv      ecx
  0041c11a  c1e20c                shl       edx,0Ch
  0041c11d  2bfa                  sub       edi,edx
  0041c11f  8b561c                mov       edx,[esi+1Ch]
  0041c122  8bc7                  mov       eax,edi
  0041c124  897e20                mov       [esi+20h],edi
  0041c127  894654                mov       [esi+54h],eax
  0041c12a  8b4624                mov       eax,[esi+24h]
  0041c12d  895650                mov       [esi+50h],edx
  0041c130  894658                mov       [esi+58h],eax
  0041c133  e808b01200            call      00547140h
  0041c138  99                    cdq
  0041c139  33c2                  xor       eax,edx
  0041c13b  8b7e5c                mov       edi,[esi+5Ch]
  0041c13e  2bc2                  sub       eax,edx
  0041c140  25ff0f0000            and       eax,0FFFh
  0041c145  33c2                  xor       eax,edx
  0041c147  2bc2                  sub       eax,edx
  0041c149  66894612              mov       [esi+12h],ax
  0041c14d  e8eeaf1200            call      00547140h
  0041c152  99                    cdq
  0041c153  8d4f04                lea       ecx,[edi+4]
  0041c156  f7f9                  idiv      ecx
  0041c158  d1ff                  sar       edi,1
  0041c15a  03d7                  add       edx,edi
  0041c15c  66895614              mov       [esi+14h],dx
  0041c160  e8dbaf1200            call      00547140h
  0041c165  99                    cdq
  0041c166  b914000000            mov       ecx,14h
  0041c16b  66899e9a000000        mov       [esi+9Ah],bx
  0041c172  f7f9                  idiv      ecx
  0041c174  b800080000            mov       eax,800h
  0041c179  895e5c                mov       [esi+5Ch],ebx
  0041c17c  66894628              mov       [esi+28h],ax
  0041c180  6689462a              mov       [esi+2Ah],ax
  0041c184  83c205                add       edx,5
  0041c187  66895616              mov       [esi+16h],dx

LAB_0041c18b:
  0041c18b  66837e1001            cmp       word [esi+10h],1
  0041c190  0f85a8000000          jne       near 0041C23Eh
  0041c196  0fbf5612              movsx     edx,word [esi+12h]
  0041c19a  52                    push      edx
  0041c19b  e87c981200            call      00545A1Ch
  0041c1a0  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0041c1a7  0fafc1                imul      eax,ecx
  0041c1aa  0fbf5612              movsx     edx,word [esi+12h]
  0041c1ae  8b4e50                mov       ecx,[esi+50h]
  0041c1b1  668bbe9a000000        mov       di,[esi+9Ah]
  0041c1b8  03c1                  add       eax,ecx
  0041c1ba  52                    push      edx
  0041c1bb  89461c                mov       [esi+1Ch],eax
  0041c1be  e853981200            call      00545A16h
  0041c1c3  0fbfcf                movsx     ecx,di
  0041c1c6  0fafc1                imul      eax,ecx
  0041c1c9  8b5658                mov       edx,[esi+58h]
  0041c1cc  8b4e20                mov       ecx,[esi+20h]
  0041c1cf  03c2                  add       eax,edx
  0041c1d1  894624                mov       [esi+24h],eax
  0041c1d4  668b4616              mov       ax,[esi+16h]
  0041c1d8  0fbfd0                movsx     edx,ax
  0041c1db  c1e20c                shl       edx,0Ch
  0041c1de  03ca                  add       ecx,edx
  0041c1e0  48                    dec       eax
  0041c1e1  663decff              cmp       ax,0FFECh
  0041c1e5  894e20                mov       [esi+20h],ecx
  0041c1e8  66894616              mov       [esi+16h],ax
  0041c1ec  7d06                  jge       short 0041C1F4h
  0041c1ee  66c74616ecff          mov       word [esi+16h],0FFECh

LAB_0041c1f4:
  0041c1f4  668b4614              mov       ax,[esi+14h]
  0041c1f8  6af6                  push      0FFFFFFF6h
  0041c1fa  6af6                  push      0FFFFFFF6h
  0041c1fc  6603c7                add       ax,di
  0041c1ff  6af6                  push      0FFFFFFF6h
  0041c201  56                    push      esi
  0041c202  6689869a000000        mov       [esi+9Ah],ax
  0041c209  e8d28bfeff            call      00404DE0h
  0041c20e  8a4638                mov       al,[esi+38h]
  0041c211  83c410                add       esp,10h
  0041c214  3ac3                  cmp       al,bl
  0041c216  7506                  jne       short 0041C21Eh
  0041c218  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041c21e:
  0041c21e  8b4e5c                mov       ecx,[esi+5Ch]
  0041c221  41                    inc       ecx
  0041c222  894e5c                mov       [esi+5Ch],ecx
  0041c225  8a465c                mov       al,[esi+5Ch]
  0041c228  a802                  test      al,2
  0041c22a  7512                  jne       short 0041C23Eh
  0041c22c  8a4e40                mov       cl,[esi+40h]
  0041c22f  fec1                  inc       cl
  0041c231  8ac1                  mov       al,cl
  0041c233  884e40                mov       [esi+40h],cl
  0041c236  3c04                  cmp       al,4
  0041c238  7204                  jb        short 0041C23Eh
  0041c23a  c6464003              mov       byte [esi+40h],3

LAB_0041c23e:
  0041c23e  5f                    pop       edi
  0041c23f  5e                    pop       esi
  0041c240  5b                    pop       ebx
  0041c241  c3                    ret
  0041c242  90                    nop
  0041c243  90                    nop
  0041c244  90                    nop
  0041c245  90                    nop
  0041c246  90                    nop
  0041c247  90                    nop
  0041c248  90                    nop
  0041c249  90                    nop
  0041c24a  90                    nop
  0041c24b  90                    nop
  0041c24c  90                    nop
  0041c24d  90                    nop
  0041c24e  90                    nop
  0041c24f  90                    nop

4 cible(s) de saut interne : 0x0041c18b, 0x0041c1f4, 0x0041c21e, 0x0041c23e
