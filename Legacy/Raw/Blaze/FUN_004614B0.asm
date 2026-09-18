C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004614b0  section .text  offset fichier 0x608b0  639 octets

  004614b0  83ec08                sub       esp,8
  004614b3  53                    push      ebx
  004614b4  55                    push      ebp
  004614b5  56                    push      esi
  004614b6  8b742418              mov       esi,[esp+18h]
  004614ba  33ed                  xor       ebp,ebp
  004614bc  57                    push      edi
  004614bd  668b4610              mov       ax,[esi+10h]
  004614c1  66c74628cc10          mov       word [esi+28h],10CCh
  004614c7  663bc5                cmp       ax,bp
  004614ca  750e                  jne       short 004614DAh
  004614cc  66896e12              mov       [esi+12h],bp
  004614d0  896e5c                mov       [esi+5Ch],ebp
  004614d3  6689ae98000000        mov       [esi+98h],bp

LAB_004614da:
  004614da  663d0100              cmp       ax,1
  004614de  0f8533020000          jne       near 00461717h
  004614e4  6639ae98000000        cmp       [esi+98h],bp
  004614eb  7528                  jne       short 00461515h
  004614ed  a1cc975e00            mov       eax,[5E97CCh]
  004614f2  55                    push      ebp
  004614f3  55                    push      ebp
  004614f4  6800180000            push      1800h
  004614f9  8b88a8000000          mov       ecx,[eax+0A8h]
  004614ff  6800030000            push      300h
  00461504  8b11                  mov       edx,[ecx]
  00461506  660fb6422a            movzx     ax,byte [edx+2Ah]
  0046150b  50                    push      eax
  0046150c  56                    push      esi
  0046150d  e86e6b0000            call      00468080h
  00461512  83c418                add       esp,18h

LAB_00461515:
  00461515  66ff8698000000        inc       word [esi+98h]
  0046151c  6683be980000000f      cmp       word [esi+98h],0Fh
  00461524  7507                  jne       short 0046152Dh
  00461526  6689ae98000000        mov       [esi+98h],bp

LAB_0046152d:
  0046152d  66ff4e12              dec       word [esi+12h]
  00461531  66396e12              cmp       [esi+12h],bp
  00461535  7d0b                  jge       short 00461542h
  00461537  8b06                  mov       eax,[esi]
  00461539  66896e12              mov       [esi+12h],bp
  0046153d  80e4df                and       ah,0DFh
  00461540  8906                  mov       [esi],eax

LAB_00461542:
  00461542  66ff4e14              dec       word [esi+14h]
  00461546  66396e14              cmp       [esi+14h],bp
  0046154a  7d11                  jge       short 0046155Dh
  0046154c  8b465c                mov       eax,[esi+5Ch]
  0046154f  48                    dec       eax
  00461550  89465c                mov       [esi+5Ch],eax
  00461553  791d                  jns       short 00461572h
  00461555  66c746100200          mov       word [esi+10h],2
  0046155b  eb15                  jmp       short 00461572h

LAB_0046155d:
  0046155d  8b565c                mov       edx,[esi+5Ch]
  00461560  42                    inc       edx
  00461561  8bc2                  mov       eax,edx
  00461563  89565c                mov       [esi+5Ch],edx
  00461566  83f810                cmp       eax,10h
  00461569  7c07                  jl        short 00461572h
  0046156b  c7465c10000000        mov       dword [esi+5Ch],10h

LAB_00461572:
  00461572  0fbf4e32              movsx     ecx,word [esi+32h]
  00461576  51                    push      ecx
  00461577  e8a0440e00            call      00545A1Ch
  0046157c  0faf465c              imul      eax,[esi+5Ch]
  00461580  0fbf5632              movsx     edx,word [esi+32h]
  00461584  8b5e1c                mov       ebx,[esi+1Ch]
  00461587  52                    push      edx
  00461588  03d8                  add       ebx,eax
  0046158a  895e1c                mov       [esi+1Ch],ebx
  0046158d  e884440e00            call      00545A16h
  00461592  0faf465c              imul      eax,[esi+5Ch]
  00461596  8b4e24                mov       ecx,[esi+24h]
  00461599  66896e16              mov       [esi+16h],bp
  0046159d  03c8                  add       ecx,eax
  0046159f  bf04000000            mov       edi,4
  004615a4  894e24                mov       [esi+24h],ecx

LAB_004615a7:
  004615a7  55                    push      ebp
  004615a8  55                    push      ebp
  004615a9  6aff                  push      0FFFFFFFFh
  004615ab  55                    push      ebp
  004615ac  6a38                  push      38h
  004615ae  56                    push      esi
  004615af  e85c6b0000            call      00468110h
  004615b4  83c418                add       esp,18h
  004615b7  4f                    dec       edi
  004615b8  75ed                  jne       short 004615A7h
  004615ba  bf02000000            mov       edi,2

LAB_004615bf:
  004615bf  55                    push      ebp
  004615c0  55                    push      ebp
  004615c1  6aff                  push      0FFFFFFFFh
  004615c3  55                    push      ebp
  004615c4  6a38                  push      38h
  004615c6  56                    push      esi
  004615c7  e8446b0000            call      00468110h
  004615cc  83c418                add       esp,18h
  004615cf  4f                    dec       edi
  004615d0  75ed                  jne       short 004615BFh
  004615d2  896c241c              mov       [esp+1Ch],ebp
  004615d6  bfe4935e00            mov       edi,5E93E4h
  004615db  c744241404000000      mov       dword [esp+14h],4
  004615e3  eb02                  jmp       short 004615E7h

LAB_004615e5:
  004615e5  33ed                  xor       ebp,ebp

LAB_004615e7:
  004615e7  8b461c                mov       eax,[esi+1Ch]
  004615ea  8b4ff8                mov       ecx,[edi-8]
  004615ed  8b17                  mov       edx,[edi]
  004615ef  2bc1                  sub       eax,ecx
  004615f1  8b4e24                mov       ecx,[esi+24h]
  004615f4  2bca                  sub       ecx,edx
  004615f6  c1f80c                sar       eax,0Ch
  004615f9  c1f90c                sar       ecx,0Ch
  004615fc  3bc5                  cmp       eax,ebp
  004615fe  8bd8                  mov       ebx,eax
  00461600  7d02                  jge       short 00461604h
  00461602  f7db                  neg       ebx

LAB_00461604:
  00461604  3bcd                  cmp       ecx,ebp
  00461606  895c2410              mov       [esp+10h],ebx
  0046160a  8bd1                  mov       edx,ecx
  0046160c  7d02                  jge       short 00461610h
  0046160e  f7da                  neg       edx

LAB_00461610:
  00461610  81fb2c010000          cmp       ebx,12Ch
  00461616  0f8dcc000000          jge       near 004616E8h
  0046161c  81fa2c010000          cmp       edx,12Ch
  00461622  0f8dc0000000          jge       near 004616E8h
  00461628  8b5e20                mov       ebx,[esi+20h]
  0046162b  8b6ffc                mov       ebp,[edi-4]
  0046162e  c1fb0c                sar       ebx,0Ch
  00461631  81ebc8000000          sub       ebx,0C8h
  00461637  c1fd0c                sar       ebp,0Ch
  0046163a  3bdd                  cmp       ebx,ebp
  0046163c  0f8da6000000          jge       near 004616E8h
  00461642  66c746120200          mov       word [esi+12h],2
  00461648  8b6fdc                mov       ebp,[edi-24h]
  0046164b  83cd01                or        ebp,1
  0046164e  896fdc                mov       [edi-24h],ebp
  00461651  668b6e32              mov       bp,[esi+32h]
  00461655  6685ed                test      bp,bp
  00461658  7427                  je        short 00461681h
  0046165a  6681fd0008            cmp       bp,800h
  0046165f  7420                  je        short 00461681h
  00461661  81fabe000000          cmp       edx,0BEh
  00461667  7e38                  jle       short 004616A1h
  00461669  8b07                  mov       eax,[edi]
  0046166b  85c9                  test      ecx,ecx
  0046166d  7d09                  jge       short 00461678h
  0046166f  050060ffff            add       eax,0FFFF6000h
  00461674  8907                  mov       [edi],eax
  00461676  eb29                  jmp       short 004616A1h

LAB_00461678:
  00461678  0500a00000            add       eax,0A000h
  0046167d  8907                  mov       [edi],eax
  0046167f  eb20                  jmp       short 004616A1h

LAB_00461681:
  00461681  817c2410be000000      cmp       dword [esp+10h],0BEh
  00461689  7e16                  jle       short 004616A1h
  0046168b  85c0                  test      eax,eax
  0046168d  8b47f8                mov       eax,[edi-8]
  00461690  7d07                  jge       short 00461699h
  00461692  050060ffff            add       eax,0FFFF6000h
  00461697  eb05                  jmp       short 0046169Eh

LAB_00461699:
  00461699  0500a00000            add       eax,0A000h

LAB_0046169e:
  0046169e  8947f8                mov       [edi-8],eax

LAB_004616a1:
  004616a1  c7473000a00000        mov       dword [edi+30h],0A000h
  004616a8  0fbf4632              movsx     eax,word [esi+32h]
  004616ac  50                    push      eax
  004616ad  e86a430e00            call      00545A1Ch
  004616b2  8d0480                lea       eax,[eax+eax*4]
  004616b5  8d0c80                lea       ecx,[eax+eax*4]
  004616b8  c1e102                shl       ecx,2
  004616bb  894f2c                mov       [edi+2Ch],ecx
  004616be  0fbf5632              movsx     edx,word [esi+32h]
  004616c2  52                    push      edx
  004616c3  e84e430e00            call      00545A16h
  004616c8  8b4c241c              mov       ecx,[esp+1Ch]
  004616cc  8d0480                lea       eax,[eax+eax*4]
  004616cf  b201                  mov       dl,1
  004616d1  8d0480                lea       eax,[eax+eax*4]
  004616d4  c1e002                shl       eax,2
  004616d7  d2e2                  shl       dl,cl
  004616d9  894734                mov       [edi+34h],eax
  004616dc  8a4609                mov       al,[esi+9]
  004616df  66896e32              mov       [esi+32h],bp
  004616e3  0ac2                  or        al,dl
  004616e5  884609                mov       [esi+9],al

LAB_004616e8:
  004616e8  8b54241c              mov       edx,[esp+1Ch]
  004616ec  8b442414              mov       eax,[esp+14h]
  004616f0  42                    inc       edx
  004616f1  81c79c000000          add       edi,9Ch
  004616f7  48                    dec       eax
  004616f8  8954241c              mov       [esp+1Ch],edx
  004616fc  89442414              mov       [esp+14h],eax
  00461700  0f85dffeffff          jne       near 004615E5h
  00461706  6a00                  push      0
  00461708  6a00                  push      0
  0046170a  6a00                  push      0
  0046170c  6a0a                  push      0Ah
  0046170e  56                    push      esi
  0046170f  e82cc3ffff            call      0045DA40h
  00461714  83c414                add       esp,14h

LAB_00461717:
  00461717  66837e1002            cmp       word [esi+10h],2
  0046171c  7507                  jne       short 00461725h
  0046171e  8b06                  mov       eax,[esi]
  00461720  80e4df                and       ah,0DFh
  00461723  8906                  mov       [esi],eax

LAB_00461725:
  00461725  5f                    pop       edi
  00461726  5e                    pop       esi
  00461727  5d                    pop       ebp
  00461728  5b                    pop       ebx
  00461729  83c408                add       esp,8
  0046172c  c3                    ret
  0046172d  90                    nop
  0046172e  90                    nop

20 cible(s) de saut interne : 0x004614da, 0x00461515, 0x0046152d, 0x00461542, 0x0046155d, 0x00461572, 0x004615a7, 0x004615bf, 0x004615e5, 0x004615e7, 0x00461604, 0x00461610, 0x00461678, 0x00461681, 0x00461699, 0x0046169e, 0x004616a1, 0x004616e8, 0x00461717, 0x00461725
