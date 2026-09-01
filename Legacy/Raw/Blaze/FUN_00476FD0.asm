Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00476fd0  section .text  offset fichier 0x763d0  199 octets

  00476fd0  53                    push      ebx
  00476fd1  56                    push      esi
  00476fd2  8b74240c              mov       esi,[esp+0Ch]
  00476fd6  57                    push      edi
  00476fd7  f70600000002          test      dword [esi],2000000h
  00476fdd  740a                  je        short 00476FE9h
  00476fdf  c70600000000          mov       dword [esi],0
  00476fe5  5f                    pop       edi
  00476fe6  5e                    pop       esi
  00476fe7  5b                    pop       ebx
  00476fe8  c3                    ret

LAB_00476fe9:
  00476fe9  8b4674                mov       eax,[esi+74h]
  00476fec  6a01                  push      1
  00476fee  6aff                  push      0FFFFFFFFh
  00476ff0  50                    push      eax
  00476ff1  e8badeffff            call      00474EB0h
  00476ff6  8bf8                  mov       edi,eax
  00476ff8  33db                  xor       ebx,ebx
  00476ffa  83c40c                add       esp,0Ch
  00476ffd  3bfb                  cmp       edi,ebx
  00476fff  0f848c000000          je        near 00477091h
  00477005  57                    push      edi
  00477006  897e78                mov       [esi+78h],edi
  00477009  e8b21b0c00            call      00538BC0h
  0047700e  8b5620                mov       edx,[esi+20h]
  00477011  8d882c010000          lea       ecx,[eax+12Ch]
  00477017  c1e10c                shl       ecx,0Ch
  0047701a  c1e00c                shl       eax,0Ch
  0047701d  03d1                  add       edx,ecx
  0047701f  8bc8                  mov       ecx,eax
  00477021  b8b5814e1b            mov       eax,1B4E81B5h
  00477026  895620                mov       [esi+20h],edx
  00477029  f7e9                  imul      ecx
  0047702b  c1fa03                sar       edx,3
  0047702e  8bc2                  mov       eax,edx
  00477030  895e28                mov       [esi+28h],ebx
  00477033  c1e81f                shr       eax,1Fh
  00477036  03d0                  add       edx,eax
  00477038  66895e2c              mov       [esi+2Ch],bx
  0047703c  66895612              mov       [esi+12h],dx
  00477040  66895e14              mov       [esi+14h],bx
  00477044  895e50                mov       [esi+50h],ebx
  00477047  895e54                mov       [esi+54h],ebx
  0047704a  895e58                mov       [esi+58h],ebx
  0047704d  66c7869a0000000060    mov       word [esi+9Ah],6000h
  00477056  668b4f32              mov       cx,[edi+32h]
  0047705a  83c404                add       esp,4
  0047705d  66894e32              mov       [esi+32h],cx
  00477061  8b571c                mov       edx,[edi+1Ch]
  00477064  33c9                  xor       ecx,ecx
  00477066  6aff                  push      0FFFFFFFFh
  00477068  8a4e0b                mov       cl,[esi+0Bh]
  0047706b  89561c                mov       [esi+1Ch],edx
  0047706e  8b4724                mov       eax,[edi+24h]
  00477071  51                    push      ecx
  00477072  6a7e                  push      7Eh
  00477074  56                    push      esi
  00477075  894624                mov       [esi+24h],eax
  00477078  c6460431              mov       byte [esi+4],31h
  0047707c  e85f3a0000            call      0047AAE0h
  00477081  8b06                  mov       eax,[esi]
  00477083  83c410                add       esp,10h
  00477086  0d00000002            or        eax,2000000h
  0047708b  8906                  mov       [esi],eax
  0047708d  5f                    pop       edi
  0047708e  5e                    pop       esi
  0047708f  5b                    pop       ebx
  00477090  c3                    ret

LAB_00477091:
  00477091  891e                  mov       [esi],ebx
  00477093  5f                    pop       edi
  00477094  5e                    pop       esi
  00477095  5b                    pop       ebx
  00477096  c3                    ret

2 cible(s) de saut interne : 0x00476fe9, 0x00477091
