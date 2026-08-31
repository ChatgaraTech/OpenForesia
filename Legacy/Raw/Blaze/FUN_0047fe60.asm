C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047fe60  section .text  offset fichier 0x7f260  532 octets

  0047fe60  53                    push      ebx
  0047fe61  55                    push      ebp
  0047fe62  56                    push      esi
  0047fe63  8b742410              mov       esi,[esp+10h]
  0047fe67  57                    push      edi
  0047fe68  f70600000002          test      dword [esi],2000000h
  0047fe6e  7414                  je        short 0047FE84h
  0047fe70  66c70566915e000000    mov       word [5E9166h],0
  0047fe79  c70600000000          mov       dword [esi],0
  0047fe7f  5f                    pop       edi
  0047fe80  5e                    pop       esi
  0047fe81  5d                    pop       ebp
  0047fe82  5b                    pop       ebx
  0047fe83  c3                    ret

LAB_0047fe84:
  0047fe84  33db                  xor       ebx,ebx
  0047fe86  66395e10              cmp       [esi+10h],bx
  0047fe8a  0f8511010000          jne       near 0047FFA1h
  0047fe90  803da007600076        cmp       byte [6007A0h],76h
  0047fe97  7607                  jbe       short 0047FEA0h
  0047fe99  891e                  mov       [esi],ebx
  0047fe9b  5f                    pop       edi
  0047fe9c  5e                    pop       esi
  0047fe9d  5d                    pop       ebp
  0047fe9e  5b                    pop       ebx
  0047fe9f  c3                    ret

LAB_0047fea0:
  0047fea0  8b4674                mov       eax,[esi+74h]
  0047fea3  8b38                  mov       edi,[eax]
  0047fea5  81cf80000000          or        edi,80h
  0047feab  8938                  mov       [eax],edi
  0047fead  8b4674                mov       eax,[esi+74h]
  0047feb0  0fbf6e32              movsx     ebp,word [esi+32h]
  0047feb4  50                    push      eax
  0047feb5  895e78                mov       [esi+78h],ebx
  0047feb8  e8038d0b00            call      00538BC0h
  0047febd  83c404                add       esp,4
  0047fec0  8bf8                  mov       edi,eax
  0047fec2  d1ff                  sar       edi,1
  0047fec4  55                    push      ebp
  0047fec5  e8525b0c00            call      00545A1Ch
  0047feca  0fafc7                imul      eax,edi
  0047fecd  8b4e1c                mov       ecx,[esi+1Ch]
  0047fed0  55                    push      ebp
  0047fed1  03c8                  add       ecx,eax
  0047fed3  894e1c                mov       [esi+1Ch],ecx
  0047fed6  e83b5b0c00            call      00545A16h
  0047fedb  0fafc7                imul      eax,edi
  0047fede  8b5624                mov       edx,[esi+24h]
  0047fee1  66895e12              mov       [esi+12h],bx
  0047fee5  03d0                  add       edx,eax
  0047fee7  8b4620                mov       eax,[esi+20h]
  0047feea  c1e70c                shl       edi,0Ch
  0047feed  03c7                  add       eax,edi
  0047feef  895624                mov       [esi+24h],edx
  0047fef2  894620                mov       [esi+20h],eax
  0047fef5  bd03000000            mov       ebp,3
  0047fefa  bb55050000            mov       ebx,555h

LAB_0047feff:
  0047feff  6a02                  push      2
  0047ff01  68a0076000            push      6007A0h
  0047ff06  e88551ffff            call      00475090h
  0047ff0b  83c408                add       esp,8
  0047ff0e  33c0                  xor       eax,eax

LAB_0047ff10:
  0047ff10  33c9                  xor       ecx,ecx
  0047ff12  40                    inc       eax
  0047ff13  8a0da0076000          mov       cl,[6007A0h]
  0047ff19  83f802                cmp       eax,2
  0047ff1c  8a9408a1076000        mov       dl,[eax+ecx+6007A1h]
  0047ff23  88543015              mov       [eax+esi+15h],dl
  0047ff27  7ce7                  jl        short 0047FF10h
  0047ff29  0fbf7e32              movsx     edi,word [esi+32h]
  0047ff2d  57                    push      edi
  0047ff2e  e8e95a0c00            call      00545A1Ch
  0047ff33  8b4e50                mov       ecx,[esi+50h]
  0047ff36  57                    push      edi
  0047ff37  c1e006                shl       eax,6
  0047ff3a  03c8                  add       ecx,eax
  0047ff3c  894e50                mov       [esi+50h],ecx
  0047ff3f  e8d25a0c00            call      00545A16h
  0047ff44  8b5658                mov       edx,[esi+58h]
  0047ff47  668146320004          add       word [esi+32h],400h
  0047ff4d  c1e006                shl       eax,6
  0047ff50  03d0                  add       edx,eax
  0047ff52  33c0                  xor       eax,eax
  0047ff54  8a460b                mov       al,[esi+0Bh]
  0047ff57  6aff                  push      0FFFFFFFFh
  0047ff59  50                    push      eax
  0047ff5a  6a3c                  push      3Ch
  0047ff5c  56                    push      esi
  0047ff5d  895658                mov       [esi+58h],edx
  0047ff60  c6460424              mov       byte [esi+4],24h
  0047ff64  e877abffff            call      0047AAE0h
  0047ff69  83c410                add       esp,10h
  0047ff6c  33c9                  xor       ecx,ecx
  0047ff6e  8a4e0b                mov       cl,[esi+0Bh]
  0047ff71  c6460425              mov       byte [esi+4],25h
  0047ff75  6aff                  push      0FFFFFFFFh
  0047ff77  51                    push      ecx
  0047ff78  6a3d                  push      3Dh
  0047ff7a  56                    push      esi
  0047ff7b  e860abffff            call      0047AAE0h
  0047ff80  66015e12              add       [esi+12h],bx
  0047ff84  83c410                add       esp,10h
  0047ff87  4d                    dec       ebp
  0047ff88  0f8571ffffff          jne       near 0047FEFFh
  0047ff8e  8d461c                lea       eax,[esi+1Ch]
  0047ff91  50                    push      eax
  0047ff92  6878255500            push      552578h
  0047ff97  e844dc0b00            call      0053DBE0h
  0047ff9c  83c408                add       esp,8
  0047ff9f  33db                  xor       ebx,ebx

LAB_0047ffa1:
  0047ffa1  f60602                test      byte [esi],2
  0047ffa4  0f84ad000000          je        near 00480057h
  0047ffaa  8b4674                mov       eax,[esi+74h]
  0047ffad  8b10                  mov       edx,[eax]
  0047ffaf  80e27f                and       dl,7Fh
  0047ffb2  8910                  mov       [eax],edx
  0047ffb4  8b06                  mov       eax,[esi]
  0047ffb6  a900008000            test      eax,800000h
  0047ffbb  7479                  je        short 00480036h
  0047ffbd  8b5674                mov       edx,[esi+74h]
  0047ffc0  8b3db4876000          mov       edi,[6087B4h]
  0047ffc6  33c9                  xor       ecx,ecx
  0047ffc8  8a4a08                mov       cl,[edx+8]
  0047ffcb  8b2f                  mov       ebp,[edi]
  0047ffcd  8bd1                  mov       edx,ecx
  0047ffcf  8d3cd2                lea       edi,[edx+edx*8]
  0047ffd2  8d4a78                lea       ecx,[edx+78h]
  0047ffd5  c1e10d                shl       ecx,0Dh
  0047ffd8  8d3c7a                lea       edi,[edx+edi*2]
  0047ffdb  33d2                  xor       edx,edx
  0047ffdd  03cd                  add       ecx,ebp
  0047ffdf  8a14bd88915e00        mov       dl,[edi*4+5E9188h]
  0047ffe6  c1e204                shl       edx,4
  0047ffe9  668b92204a6000        mov       dx,[edx+604A20h]
  0047fff0  668591c41f0000        test      [ecx+1FC4h],dx
  0047fff7  7507                  jne       short 00480000h
  0047fff9  0d00000002            or        eax,2000000h
  0047fffe  8906                  mov       [esi],eax

LAB_00480000:
  00480000  a1f02c5f00            mov       eax,[5F2CF0h]
  00480005  33d2                  xor       edx,edx
  00480007  bf14000000            mov       edi,14h
  0048000c  f7f7                  div       edi
  0048000e  85d2                  test      edx,edx
  00480010  7532                  jne       short 00480044h
  00480012  668b814e010000        mov       ax,[ecx+14Eh]
  00480019  663d0500              cmp       ax,5
  0048001d  7c0c                  jl        short 0048002Bh
  0048001f  83c0fb                add       eax,0FFFFFFFBh
  00480022  6689814e010000        mov       [ecx+14Eh],ax
  00480029  eb19                  jmp       short 00480044h

LAB_0048002b:
  0048002b  6689994e010000        mov       [ecx+14Eh],bx
  00480032  8b06                  mov       eax,[esi]
  00480034  eb07                  jmp       short 0048003Dh

LAB_00480036:
  00480036  66837e1078            cmp       word [esi+10h],78h
  0048003b  7c07                  jl        short 00480044h

LAB_0048003d:
  0048003d  0d00000002            or        eax,2000000h
  00480042  8906                  mov       [esi],eax

LAB_00480044:
  00480044  56                    push      esi
  00480045  e8c650ffff            call      00475110h
  0048004a  83c404                add       esp,4
  0048004d  85c0                  test      eax,eax
  0048004f  7406                  je        short 00480057h
  00480051  810e00000002          or        dword [esi],2000000h

LAB_00480057:
  00480057  c70560915e0040004000  mov       dword [5E9160h],400040h
  00480061  c70564915e0080000200  mov       dword [5E9164h],20080h
  0048006b  66ff4610              inc       word [esi+10h]
  0048006f  5f                    pop       edi
  00480070  5e                    pop       esi
  00480071  5d                    pop       ebp
  00480072  5b                    pop       ebx
  00480073  c3                    ret

11 cible(s) de saut interne : 0x0047fe84, 0x0047fea0, 0x0047feff, 0x0047ff10, 0x0047ffa1, 0x00480000, 0x0048002b, 0x00480036, 0x0048003d, 0x00480044, 0x00480057
