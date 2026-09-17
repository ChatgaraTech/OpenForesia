Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004830f0  section .text  offset fichier 0x824f0  368 octets

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
  0048314e  05c0000000            add       eax,0C0h
  00483153  50                    push      eax
  00483154  56                    push      esi
  00483155  e88679ffff            call      0047AAE0h
  0048315a  83c410                add       esp,10h

LAB_0048315d:
  0048315d  8b06                  mov       eax,[esi]
  0048315f  0c04                  or        al,4

LAB_00483161:
  00483161  8906                  mov       [esi],eax

LAB_00483163:
  00483163  f70600000002          test      dword [esi],2000000h
  00483169  0f84ea000000          je        near 00483259h
  0048316f  c70600000000          mov       dword [esi],0
  00483175  5f                    pop       edi
  00483176  5e                    pop       esi
  00483177  5b                    pop       ebx
  00483178  83c40c                add       esp,0Ch
  0048317b  c3                    ret

LAB_0048317c:
  0048317c  8b4e44                mov       ecx,[esi+44h]
  0048317f  8b564c                mov       edx,[esi+4Ch]
  00483182  8d4644                lea       eax,[esi+44h]
  00483185  897670                mov       [esi+70h],esi
  00483188  f7d9                  neg       ecx
  0048318a  8908                  mov       [eax],ecx
  0048318c  8d4c240c              lea       ecx,[esp+0Ch]
  00483190  f7da                  neg       edx
  00483192  89564c                mov       [esi+4Ch],edx
  00483195  51                    push      ecx
  00483196  8d5730                lea       edx,[edi+30h]
  00483199  50                    push      eax
  0048319a  52                    push      edx
  0048319b  c7462800180018        mov       dword [esi+28h],18001800h
  004831a2  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  004831a9  e8e22e0b00            call      00536090h
  004831ae  8b471c                mov       eax,[edi+1Ch]
  004831b1  8b4c2418              mov       ecx,[esp+18h]
  004831b5  8b54241c              mov       edx,[esp+1Ch]
  004831b9  8d5e1c                lea       ebx,[esi+1Ch]
  004831bc  03c1                  add       eax,ecx
  004831be  83c40c                add       esp,0Ch
  004831c1  8903                  mov       [ebx],eax
  004831c3  8b4f20                mov       ecx,[edi+20h]
  004831c6  8b442414              mov       eax,[esp+14h]
  004831ca  03ca                  add       ecx,edx
  004831cc  894e20                mov       [esi+20h],ecx
  004831cf  8b5724                mov       edx,[edi+24h]
  004831d2  03d0                  add       edx,eax
  004831d4  6a28                  push      28h
  004831d6  0fbf4634              movsx     eax,word [esi+34h]
  004831da  895624                mov       [esi+24h],edx
  004831dd  50                    push      eax
  004831de  0fbf4f32              movsx     ecx,word [edi+32h]
  004831e2  8d5650                lea       edx,[esi+50h]
  004831e5  51                    push      ecx
  004831e6  52                    push      edx
  004831e7  e8e4f80a00            call      00532AD0h
  004831ec  66c7869a0000000030    mov       word [esi+9Ah],3000h
  004831f5  a1cc975e00            mov       eax,[5E97CCh]
  004831fa  83c410                add       esp,10h
  004831fd  8b88d4000000          mov       ecx,[eax+0D4h]
  00483203  33c0                  xor       eax,eax
  00483205  6a02                  push      2
  00483207  6a00                  push      0
  00483209  8b11                  mov       edx,[ecx]
  0048320b  8a420b                mov       al,[edx+0Bh]
  0048320e  05c0000000            add       eax,0C0h
  00483213  50                    push      eax
  00483214  68c81b5500            push      551BC8h
  00483219  53                    push      ebx
  0048321a  56                    push      esi
  0048321b  e8207affff            call      0047AC40h
  00483220  8b3e                  mov       edi,[esi]
  00483222  83c418                add       esp,18h
  00483225  81cf00000001          or        edi,1000000h
  0048322b  c7463880808000        mov       dword [esi+38h],808080h
  00483232  53                    push      ebx
  00483233  6800265500            push      552600h
  00483238  893e                  mov       [esi],edi
  0048323a  c7462800500070        mov       dword [esi+28h],70005000h
  00483241  66c7462c0050          mov       word [esi+2Ch],5000h
  00483247  66c7462ee0ff          mov       word [esi+2Eh],0FFE0h
  0048324d  c646041e              mov       byte [esi+4],1Eh
  00483251  e88aa90b00            call      0053DBE0h
  00483256  83c408                add       esp,8

LAB_00483259:
  00483259  5f                    pop       edi
  0048325a  5e                    pop       esi
  0048325b  5b                    pop       ebx
  0048325c  83c40c                add       esp,0Ch
  0048325f  c3                    ret

6 cible(s) de saut interne : 0x00483118, 0x0048315d, 0x00483161, 0x00483163, 0x0048317c, 0x00483259
