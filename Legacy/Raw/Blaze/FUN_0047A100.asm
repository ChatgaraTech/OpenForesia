C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047a100  section .text  offset fichier 0x79500  368 octets

  0047a100  83ec08                sub       esp,8
  0047a103  53                    push      ebx
  0047a104  55                    push      ebp
  0047a105  56                    push      esi
  0047a106  8b742418              mov       esi,[esp+18h]
  0047a10a  57                    push      edi
  0047a10b  6aff                  push      0FFFFFFFFh
  0047a10d  56                    push      esi
  0047a10e  e8edadffff            call      00474F00h
  0047a113  668b4e36              mov       cx,[esi+36h]
  0047a117  8b1e                  mov       ebx,[esi]
  0047a119  83e1fb                and       ecx,0FFFFFFFBh
  0047a11c  83c408                add       esp,8
  0047a11f  83c902                or        ecx,2
  0047a122  83cb02                or        ebx,2
  0047a125  66894e36              mov       [esi+36h],cx
  0047a129  b900020000            mov       ecx,200h
  0047a12e  85c0                  test      eax,eax
  0047a130  89442414              mov       [esp+14h],eax
  0047a134  891e                  mov       [esi],ebx
  0047a136  66c746288003          mov       word [esi+28h],380h
  0047a13c  66894e2a              mov       [esi+2Ah],cx
  0047a140  66894e2c              mov       [esi+2Ch],cx
  0047a144  c646043a              mov       byte [esi+4],3Ah
  0047a148  7478                  je        short 0047A1C2h
  0047a14a  8b781c                mov       edi,[eax+1Ch]
  0047a14d  8b6e1c                mov       ebp,[esi+1Ch]
  0047a150  8b5824                mov       ebx,[eax+24h]
  0047a153  8b5620                mov       edx,[esi+20h]
  0047a156  2bfd                  sub       edi,ebp
  0047a158  8b6820                mov       ebp,[eax+20h]
  0047a15b  8b4624                mov       eax,[esi+24h]
  0047a15e  2bea                  sub       ebp,edx
  0047a160  2bd8                  sub       ebx,eax
  0047a162  c1ff0c                sar       edi,0Ch
  0047a165  c1fb0c                sar       ebx,0Ch
  0047a168  8bd7                  mov       edx,edi
  0047a16a  8bc3                  mov       eax,ebx
  0047a16c  0fafd7                imul      edx,edi
  0047a16f  0fafc3                imul      eax,ebx
  0047a172  03d0                  add       edx,eax
  0047a174  52                    push      edx
  0047a175  c1fd0c                sar       ebp,0Ch
  0047a178  e893b80c00            call      00545A10h
  0047a17d  668b4e32              mov       cx,[esi+32h]
  0047a181  53                    push      ebx
  0047a182  57                    push      edi
  0047a183  89442418              mov       [esp+18h],eax
  0047a187  66894c2424            mov       [esp+24h],cx
  0047a18c  e891b80c00            call      00545A22h
  0047a191  8b7c2410              mov       edi,[esp+10h]
  0047a195  8bd0                  mov       edx,eax
  0047a197  8b44241c              mov       eax,[esp+1Ch]
  0047a19b  57                    push      edi
  0047a19c  2bd0                  sub       edx,eax
  0047a19e  55                    push      ebp
  0047a19f  81e2ff0f0000          and       edx,0FFFh
  0047a1a5  03d0                  add       edx,eax
  0047a1a7  66895632              mov       [esi+32h],dx
  0047a1ab  e872b80c00            call      00545A22h
  0047a1b0  c1ff02                sar       edi,2
  0047a1b3  03c7                  add       eax,edi
  0047a1b5  66894634              mov       [esi+34h],ax
  0047a1b9  8b442414              mov       eax,[esp+14h]
  0047a1bd  894678                mov       [esi+78h],eax
  0047a1c0  eb0d                  jmp       short 0047A1CFh

LAB_0047a1c2:
  0047a1c2  66c746340001          mov       word [esi+34h],100h
  0047a1c8  c7467800000000        mov       dword [esi+78h],0

LAB_0047a1cf:
  0047a1cf  0fbf5e34              movsx     ebx,word [esi+34h]
  0047a1d3  0fbf7e32              movsx     edi,word [esi+32h]
  0047a1d7  53                    push      ebx
  0047a1d8  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  0047a1e1  e830b80c00            call      00545A16h
  0047a1e6  57                    push      edi
  0047a1e7  8be8                  mov       ebp,eax
  0047a1e9  e82eb80c00            call      00545A1Ch
  0047a1ee  0fafc5                imul      eax,ebp
  0047a1f1  c1f805                sar       eax,5
  0047a1f4  2480                  and       al,80h
  0047a1f6  53                    push      ebx
  0047a1f7  894650                mov       [esi+50h],eax
  0047a1fa  e81db80c00            call      00545A1Ch
  0047a1ff  c1e007                shl       eax,7
  0047a202  57                    push      edi
  0047a203  894654                mov       [esi+54h],eax
  0047a206  e80bb80c00            call      00545A16h
  0047a20b  0fafc5                imul      eax,ebp
  0047a20e  8b4e6c                mov       ecx,[esi+6Ch]
  0047a211  c1f805                sar       eax,5
  0047a214  2480                  and       al,80h
  0047a216  51                    push      ecx
  0047a217  894658                mov       [esi+58h],eax
  0047a21a  e8a1e90b00            call      00538BC0h
  0047a21f  8b6e20                mov       ebp,[esi+20h]
  0047a222  8b566c                mov       edx,[esi+6Ch]
  0047a225  668146320004          add       word [esi+32h],400h
  0047a22b  83c404                add       esp,4
  0047a22e  24fe                  and       al,0FEh
  0047a230  895674                mov       [esi+74h],edx
  0047a233  c1e00b                shl       eax,0Bh
  0047a236  6a08                  push      8
  0047a238  6a01                  push      1
  0047a23a  03e8                  add       ebp,eax
  0047a23c  6a52                  push      52h
  0047a23e  56                    push      esi
  0047a23f  896e20                mov       [esi+20h],ebp
  0047a242  e899080000            call      0047AAE0h
  0047a247  83c410                add       esp,10h
  0047a24a  8d461c                lea       eax,[esi+1Ch]
  0047a24d  50                    push      eax
  0047a24e  6800120000            push      1200h
  0047a253  6800200000            push      2000h
  0047a258  6a1f                  push      1Fh
  0047a25a  e8c1380c00            call      0053DB20h
  0047a25f  83c410                add       esp,10h
  0047a262  c70600000000          mov       dword [esi],0
  0047a268  5f                    pop       edi
  0047a269  5e                    pop       esi
  0047a26a  5d                    pop       ebp
  0047a26b  5b                    pop       ebx
  0047a26c  83c408                add       esp,8
  0047a26f  c3                    ret

2 cible(s) de saut interne : 0x0047a1c2, 0x0047a1cf
