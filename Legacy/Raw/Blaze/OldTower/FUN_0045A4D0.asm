C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045a4d0  section .text  offset fichier 0x598d0  352 octets

  0045a4d0  53                    push      ebx
  0045a4d1  56                    push      esi
  0045a4d2  8b74240c              mov       esi,[esp+0Ch]
  0045a4d6  57                    push      edi
  0045a4d7  33ff                  xor       edi,edi
  0045a4d9  bb01000000            mov       ebx,1
  0045a4de  66397e10              cmp       [esi+10h],di
  0045a4e2  7513                  jne       short 0045A4F7h
  0045a4e4  66895e10              mov       [esi+10h],bx
  0045a4e8  66897e12              mov       [esi+12h],di
  0045a4ec  66897e16              mov       [esi+16h],di
  0045a4f0  6689be98000000        mov       [esi+98h],di

LAB_0045a4f7:
  0045a4f7  6800000040            push      40000000h
  0045a4fc  6a10                  push      10h
  0045a4fe  56                    push      esi
  0045a4ff  e8fcaa0d00            call      00535000h
  0045a504  83c40c                add       esp,0Ch
  0045a507  663bc7                cmp       ax,di
  0045a50a  742b                  je        short 0045A537h
  0045a50c  66397e16              cmp       [esi+16h],di
  0045a510  7520                  jne       short 0045A532h
  0045a512  0fbf4e14              movsx     ecx,word [esi+14h]
  0045a516  8ad3                  mov       dl,bl
  0045a518  66c746100200          mov       word [esi+10h],2
  0045a51e  d2e2                  shl       dl,cl
  0045a520  8a0d01975e00          mov       cl,[5E9701h]
  0045a526  0aca                  or        cl,dl
  0045a528  880d01975e00          mov       [5E9701h],cl
  0045a52e  66895e16              mov       [esi+16h],bx

LAB_0045a532:
  0045a532  663bc7                cmp       ax,di
  0045a535  7504                  jne       short 0045A53Bh

LAB_0045a537:
  0045a537  66897e16              mov       [esi+16h],di

LAB_0045a53b:
  0045a53b  8a0d01975e00          mov       cl,[5E9701h]
  0045a541  33c0                  xor       eax,eax
  0045a543  f6c110                test      cl,10h
  0045a546  7502                  jne       short 0045A54Ah
  0045a548  8bc3                  mov       eax,ebx

LAB_0045a54a:
  0045a54a  f6c120                test      cl,20h
  0045a54d  7501                  jne       short 0045A550h
  0045a54f  40                    inc       eax

LAB_0045a550:
  0045a550  f6c140                test      cl,40h
  0045a553  7501                  jne       short 0045A556h
  0045a555  40                    inc       eax

LAB_0045a556:
  0045a556  f6c180                test      cl,80h
  0045a559  7501                  jne       short 0045A55Ch
  0045a55b  40                    inc       eax

LAB_0045a55c:
  0045a55c  663bc7                cmp       ax,di
  0045a55f  7506                  jne       short 0045A567h
  0045a561  66c746100300          mov       word [esi+10h],3

LAB_0045a567:
  0045a567  66395e10              cmp       [esi+10h],bx
  0045a56b  0f858f000000          jne       near 0045A600h
  0045a571  66ff4e12              dec       word [esi+12h]
  0045a575  668b4612              mov       ax,[esi+12h]
  0045a579  663d48f4              cmp       ax,0F448h
  0045a57d  7d06                  jge       short 0045A585h
  0045a57f  66c7461248f4          mov       word [esi+12h],0F448h

LAB_0045a585:
  0045a585  66397e12              cmp       [esi+12h],di
  0045a589  7d75                  jge       short 0045A600h
  0045a58b  0fbf4e14              movsx     ecx,word [esi+14h]
  0045a58f  8ac3                  mov       al,bl
  0045a591  885e0a                mov       [esi+0Ah],bl
  0045a594  d2e0                  shl       al,cl
  0045a596  8a0d01975e00          mov       cl,[5E9701h]
  0045a59c  f6d0                  not       al
  0045a59e  22c8                  and       cl,al
  0045a5a0  880d01975e00          mov       [5E9701h],cl
  0045a5a6  e895cb0e00            call      00547140h
  0045a5ab  99                    cdq
  0045a5ac  b906000000            mov       ecx,6
  0045a5b1  f7f9                  idiv      ecx
  0045a5b3  663bd7                cmp       dx,di
  0045a5b6  7510                  jne       short 0045A5C8h
  0045a5b8  57                    push      edi
  0045a5b9  57                    push      edi
  0045a5ba  6aff                  push      0FFFFFFFFh
  0045a5bc  57                    push      edi
  0045a5bd  6a37                  push      37h
  0045a5bf  56                    push      esi
  0045a5c0  e84bdb0000            call      00468110h
  0045a5c5  83c418                add       esp,18h

LAB_0045a5c8:
  0045a5c8  6639be98000000        cmp       [esi+98h],di
  0045a5cf  7517                  jne       short 0045A5E8h
  0045a5d1  57                    push      edi
  0045a5d2  57                    push      edi
  0045a5d3  6800060000            push      600h
  0045a5d8  6800040000            push      400h
  0045a5dd  6a1b                  push      1Bh
  0045a5df  56                    push      esi
  0045a5e0  e89bda0000            call      00468080h
  0045a5e5  83c418                add       esp,18h

LAB_0045a5e8:
  0045a5e8  66ff8698000000        inc       word [esi+98h]
  0045a5ef  6683be980000000d      cmp       word [esi+98h],0Dh
  0045a5f7  7c07                  jl        short 0045A600h
  0045a5f9  6689be98000000        mov       [esi+98h],di

LAB_0045a600:
  0045a600  66837e1002            cmp       word [esi+10h],2
  0045a605  750e                  jne       short 0045A615h
  0045a607  c6460a02              mov       byte [esi+0Ah],2
  0045a60b  66895e10              mov       [esi+10h],bx
  0045a60f  66c746122800          mov       word [esi+12h],28h

LAB_0045a615:
  0045a615  66837e1003            cmp       word [esi+10h],3
  0045a61a  7504                  jne       short 0045A620h
  0045a61c  c6460a02              mov       byte [esi+0Ah],2

LAB_0045a620:
  0045a620  5f                    pop       edi
  0045a621  5e                    pop       esi
  0045a622  5b                    pop       ebx
  0045a623  c3                    ret
  0045a624  90                    nop
  0045a625  90                    nop
  0045a626  90                    nop
  0045a627  90                    nop
  0045a628  90                    nop
  0045a629  90                    nop
  0045a62a  90                    nop
  0045a62b  90                    nop
  0045a62c  90                    nop
  0045a62d  90                    nop
  0045a62e  90                    nop
  0045a62f  90                    nop

15 cible(s) de saut interne : 0x0045a4f7, 0x0045a532, 0x0045a537, 0x0045a53b, 0x0045a54a, 0x0045a550, 0x0045a556, 0x0045a55c, 0x0045a567, 0x0045a585, 0x0045a5c8, 0x0045a5e8, 0x0045a600, 0x0045a615, 0x0045a620
