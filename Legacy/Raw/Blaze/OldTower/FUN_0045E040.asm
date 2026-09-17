C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045e040  section .text  offset fichier 0x5d440  192 octets

  0045e040  8b442404              mov       eax,[esp+4]
  0045e044  56                    push      esi
  0045e045  8b08                  mov       ecx,[eax]
  0045e047  f7c100000040          test      ecx,40000000h
  0045e04d  0f858d000000          jne       near 0045E0E0h
  0045e053  6683781000            cmp       word [eax+10h],0
  0045e058  7548                  jne       short 0045E0A2h
  0045e05a  80cd20                or        ch,20h
  0045e05d  c740540f000000        mov       dword [eax+54h],0Fh
  0045e064  8908                  mov       [eax],ecx
  0045e066  0fbf4814              movsx     ecx,word [eax+14h]
  0045e06a  49                    dec       ecx
  0045e06b  83f904                cmp       ecx,4
  0045e06e  7732                  ja        short 0045E0A2h
  0045e070  ff248de4e04500        jmp       dword [ecx*4+45E0E4h]
  0045e077  800de016570001        or        byte [5716E0h],1
  0045e07e  eb22                  jmp       short 0045E0A2h
  0045e080  800de016570002        or        byte [5716E0h],2
  0045e087  eb19                  jmp       short 0045E0A2h
  0045e089  800de016570004        or        byte [5716E0h],4
  0045e090  eb10                  jmp       short 0045E0A2h
  0045e092  800de016570008        or        byte [5716E0h],8
  0045e099  eb07                  jmp       short 0045E0A2h
  0045e09b  800de016570010        or        byte [5716E0h],10h

LAB_0045e0a2:
  0045e0a2  668b5010              mov       dx,[eax+10h]
  0045e0a6  8b4854                mov       ecx,[eax+54h]
  0045e0a9  6683fa0c              cmp       dx,0Ch
  0045e0ad  8bf1                  mov       esi,ecx
  0045e0af  7e0a                  jle       short 0045E0BBh
  0045e0b1  c1fe03                sar       esi,3
  0045e0b4  2bce                  sub       ecx,esi
  0045e0b6  894854                mov       [eax+54h],ecx
  0045e0b9  eb08                  jmp       short 0045E0C3h

LAB_0045e0bb:
  0045e0bb  c1fe03                sar       esi,3
  0045e0be  03f1                  add       esi,ecx
  0045e0c0  897054                mov       [eax+54h],esi

LAB_0045e0c3:
  0045e0c3  8d4a01                lea       ecx,[edx+1]

LAB_0045e0c6:
  0045e0c6  6683f928              cmp       cx,28h
  0045e0ca  66894810              mov       [eax+10h],cx
  0045e0ce  7508                  jne       short 0045E0D8h
  0045e0d0  810800200040          or        dword [eax],40002000h
  0045e0d6  5e                    pop       esi
  0045e0d7  c3                    ret

LAB_0045e0d8:
  0045e0d8  668b4854              mov       cx,[eax+54h]
  0045e0dc  66294832              sub       [eax+32h],cx

LAB_0045e0e0:
  0045e0e0  5e                    pop       esi
  0045e0e1  c3                    ret
  0045e0e2  8bff                  mov       edi,edi
  0045e0e4  77e0                  ja        short 0045E0C6h
  0045e0e6  45                    inc       ebp
  0045e0e7  0080e0450089          add       [eax-76FFBA20h],al
  0045e0ed  e045                  loopne    0045E134h
  0045e0ef  0092e045009b          add       [edx-64FFBA20h],dl
  0045e0f5  e045                  loopne    0045E13Ch
  0045e0f7  009090909090          add       [eax-6F6F6F70h],dl
  0045e0fd  90                    nop
  0045e0fe  90                    nop
  0045e0ff  90                    nop

6 cible(s) de saut interne : 0x0045e0a2, 0x0045e0bb, 0x0045e0c3, 0x0045e0c6, 0x0045e0d8, 0x0045e0e0
