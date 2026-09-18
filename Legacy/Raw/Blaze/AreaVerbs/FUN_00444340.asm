C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00444340  section .text  offset fichier 0x43740  141 octets

  00444340  0fbf442408            movsx     eax,word [esp+8]
  00444345  8d0c80                lea       ecx,[eax+eax*4]
  00444348  c1e103                shl       ecx,3
  0044434b  2bc8                  sub       ecx,eax
  0044434d  66833c8df6a55f0000    cmp       word [ecx*4+5FA5F6h],0
  00444356  8d048de0a55f00        lea       eax,[ecx*4+5FA5E0h]
  0044435d  7403                  je        short 00444362h
  0044435f  33c0                  xor       eax,eax
  00444361  c3                    ret

LAB_00444362:
  00444362  56                    push      esi
  00444363  53                    push      ebx
  00444364  8a5809                mov       bl,[eax+9]
  00444367  b8c0935e00            mov       eax,5E93C0h
  0044436c  80e30f                and       bl,0Fh
  0044436f  33c9                  xor       ecx,ecx
  00444371  885c2410              mov       [esp+10h],bl
  00444375  be00a00000            mov       esi,0A000h
  0044437a  eb04                  jmp       short 00444380h

LAB_0044437c:
  0044437c  8a5c2410              mov       bl,[esp+10h]

LAB_00444380:
  00444380  8b10                  mov       edx,[eax]
  00444382  f7c200000080          test      edx,80000000h
  00444388  7432                  je        short 004443BCh
  0044438a  f7c200000050          test      edx,50000000h
  00444390  752a                  jne       short 004443BCh
  00444392  d2eb                  shr       bl,cl
  00444394  f6c301                test      bl,1
  00444397  7423                  je        short 004443BCh
  00444399  6609b082000000        or        [eax+82h],si
  004443a0  6609b086000000        or        [eax+86h],si
  004443a7  6609b08a000000        or        [eax+8Ah],si
  004443ae  6609b08e000000        or        [eax+8Eh],si
  004443b5  c6809400000000        mov       byte [eax+94h],0

LAB_004443bc:
  004443bc  41                    inc       ecx
  004443bd  059c000000            add       eax,9Ch
  004443c2  6683f903              cmp       cx,3
  004443c6  7eb4                  jle       short 0044437Ch
  004443c8  5b                    pop       ebx
  004443c9  33c0                  xor       eax,eax
  004443cb  5e                    pop       esi
  004443cc  c3                    ret

4 cible(s) de saut interne : 0x00444362, 0x0044437c, 0x00444380, 0x004443bc
