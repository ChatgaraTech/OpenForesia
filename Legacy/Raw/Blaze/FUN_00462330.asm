C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00462330  section .text  offset fichier 0x61730  630 octets

  00462330  a168e05d00            mov       eax,[5DE068h]
  00462335  53                    push      ebx
  00462336  56                    push      esi
  00462337  57                    push      edi
  00462338  a900f0ffff            test      eax,0FFFFF000h
  0046233d  0f8c5f020000          jl        near 004625A2h
  00462343  813d6ce05d0000c0e0ff  cmp       dword [5DE06Ch],0FFE0C000h
  0046234d  0f8f4f020000          jg        near 004625A2h
  00462353  8b742410              mov       esi,[esp+10h]
  00462357  56                    push      esi
  00462358  e863ecffff            call      00460FC0h
  0046235d  33db                  xor       ebx,ebx
  0046235f  83c404                add       esp,4
  00462362  66395e10              cmp       [esi+10h],bx
  00462366  bf10000000            mov       edi,10h
  0046236b  7538                  jne       short 004623A5h
  0046236d  56                    push      esi
  0046236e  66897e10              mov       [esi+10h],di
  00462372  66895e12              mov       [esi+12h],bx
  00462376  66895e14              mov       [esi+14h],bx
  0046237a  66c746160100          mov       word [esi+16h],1
  00462380  c7462000c095ff        mov       dword [esi+20h],0FF95C000h
  00462387  c7461c00007d00        mov       dword [esi+1Ch],7D0000h
  0046238e  c7462400c0fffe        mov       dword [esi+24h],0FEFFC000h
  00462395  66895e2a              mov       [esi+2Ah],bx
  00462399  66895e28              mov       [esi+28h],bx
  0046239d  e89e150d00            call      00533940h
  004623a2  83c404                add       esp,4

LAB_004623a5:
  004623a5  668b4616              mov       ax,[esi+16h]
  004623a9  663bc3                cmp       ax,bx
  004623ac  746e                  je        short 0046241Ch
  004623ae  663d0100              cmp       ax,1
  004623b2  ba02000000            mov       edx,2
  004623b7  7527                  jne       short 004623E0h
  004623b9  b9c8000000            mov       ecx,0C8h
  004623be  66014e28              add       [esi+28h],cx
  004623c2  668b4628              mov       ax,[esi+28h]
  004623c6  66014e2a              add       [esi+2Ah],cx
  004623ca  663d0010              cmp       ax,1000h
  004623ce  7e10                  jle       short 004623E0h
  004623d0  66c746280010          mov       word [esi+28h],1000h
  004623d6  66c7462a0010          mov       word [esi+2Ah],1000h
  004623dc  66895616              mov       [esi+16h],dx

LAB_004623e0:
  004623e0  66395616              cmp       [esi+16h],dx
  004623e4  0f85b8010000          jne       near 004625A2h
  004623ea  66ff4614              inc       word [esi+14h]
  004623ee  8b4e20                mov       ecx,[esi+20h]
  004623f1  0fbf4614              movsx     eax,word [esi+14h]
  004623f5  66017e34              add       [esi+34h],di
  004623f9  c1e00c                shl       eax,0Ch
  004623fc  2bc8                  sub       ecx,eax
  004623fe  8bc1                  mov       eax,ecx
  00462400  894e20                mov       [esi+20h],ecx
  00462403  3d00605aff            cmp       eax,0FF5A6000h
  00462408  0f8f94010000          jg        near 004625A2h
  0046240e  66895e16              mov       [esi+16h],bx
  00462412  66c74614c0ff          mov       word [esi+14h],0FFC0h
  00462418  5f                    pop       edi
  00462419  5e                    pop       esi
  0046241a  5b                    pop       ebx
  0046241b  c3                    ret

LAB_0046241c:
  0046241c  0fbf4e14              movsx     ecx,word [esi+14h]
  00462420  51                    push      ecx
  00462421  e8f6350e00            call      00545A1Ch
  00462426  85c0                  test      eax,eax
  00462428  7557                  jne       short 00462481h
  0046242a  53                    push      ebx
  0046242b  53                    push      ebx
  0046242c  68e7030000            push      3E7h
  00462431  53                    push      ebx
  00462432  68e7030000            push      3E7h
  00462437  56                    push      esi
  00462438  e8239afbff            call      0041BE60h
  0046243d  83c418                add       esp,18h
  00462440  bf0f000000            mov       edi,0Fh

LAB_00462445:
  00462445  53                    push      ebx
  00462446  53                    push      ebx
  00462447  6aff                  push      0FFFFFFFFh
  00462449  53                    push      ebx
  0046244a  6a30                  push      30h
  0046244c  56                    push      esi
  0046244d  e8be5c0000            call      00468110h
  00462452  83c418                add       esp,18h
  00462455  4f                    dec       edi
  00462456  75ed                  jne       short 00462445h
  00462458  8b15cc975e00          mov       edx,[5E97CCh]
  0046245e  53                    push      ebx
  0046245f  53                    push      ebx
  00462460  6800180000            push      1800h
  00462465  8b82a8000000          mov       eax,[edx+0A8h]
  0046246b  6800030000            push      300h
  00462470  8b08                  mov       ecx,[eax]
  00462472  660fb65119            movzx     dx,byte [ecx+19h]
  00462477  52                    push      edx
  00462478  56                    push      esi
  00462479  e8025c0000            call      00468080h
  0046247e  83c418                add       esp,18h

LAB_00462481:
  00462481  66ff4612              inc       word [esi+12h]
  00462485  8b461c                mov       eax,[esi+1Ch]
  00462488  0fbf5612              movsx     edx,word [esi+12h]
  0046248c  8b4e20                mov       ecx,[esi+20h]
  0046248f  81c280020000          add       edx,280h
  00462495  894644                mov       [esi+44h],eax
  00462498  8b4624                mov       eax,[esi+24h]
  0046249b  c1e207                shl       edx,7
  0046249e  03d0                  add       edx,eax
  004624a0  56                    push      esi
  004624a1  894e48                mov       [esi+48h],ecx
  004624a4  89464c                mov       [esi+4Ch],eax
  004624a7  895624                mov       [esi+24h],edx
  004624aa  e891140d00            call      00533940h
  004624af  0fbf4614              movsx     eax,word [esi+14h]
  004624b3  83c404                add       esp,4
  004624b6  50                    push      eax
  004624b7  e860350e00            call      00545A1Ch
  004624bc  c1e008                shl       eax,8
  004624bf  780f                  js        short 004624D0h
  004624c1  0fbf4e14              movsx     ecx,word [esi+14h]
  004624c5  51                    push      ecx
  004624c6  e851350e00            call      00545A1Ch
  004624cb  c1e008                shl       eax,8
  004624ce  eb0f                  jmp       short 004624DFh

LAB_004624d0:
  004624d0  0fbf5614              movsx     edx,word [esi+14h]
  004624d4  52                    push      edx
  004624d5  e842350e00            call      00545A1Ch
  004624da  c1e008                shl       eax,8
  004624dd  f7d8                  neg       eax

LAB_004624df:
  004624df  8b4e60                mov       ecx,[esi+60h]
  004624e2  6683461440            add       word [esi+14h],40h
  004624e7  8d940100e00100        lea       edx,[ecx+eax+1E000h]
  004624ee  668b4614              mov       ax,[esi+14h]
  004624f2  663d0010              cmp       ax,1000h
  004624f6  895620                mov       [esi+20h],edx
  004624f9  7e09                  jle       short 00462504h
  004624fb  0500f0ffff            add       eax,0FFFFF000h
  00462500  66894614              mov       [esi+14h],ax

LAB_00462504:
  00462504  0fbf4614              movsx     eax,word [esi+14h]
  00462508  50                    push      eax
  00462509  e80e350e00            call      00545A1Ch
  0046250e  85c0                  test      eax,eax
  00462510  7534                  jne       short 00462546h
  00462512  6683461020            add       word [esi+10h],20h
  00462517  53                    push      ebx
  00462518  53                    push      ebx
  00462519  68e7030000            push      3E7h
  0046251e  6a1e                  push      1Eh
  00462520  68e7030000            push      3E7h
  00462525  56                    push      esi
  00462526  e83599fbff            call      0041BE60h
  0046252b  83c418                add       esp,18h
  0046252e  bf0f000000            mov       edi,0Fh

LAB_00462533:
  00462533  53                    push      ebx
  00462534  53                    push      ebx
  00462535  6aff                  push      0FFFFFFFFh
  00462537  53                    push      ebx
  00462538  6a30                  push      30h
  0046253a  56                    push      esi
  0046253b  e8d05b0000            call      00468110h
  00462540  83c418                add       esp,18h
  00462543  4f                    dec       edi
  00462544  75ed                  jne       short 00462533h

LAB_00462546:
  00462546  668b4e10              mov       cx,[esi+10h]
  0046254a  66014e34              add       [esi+34h],cx
  0046254e  668b4634              mov       ax,[esi+34h]
  00462552  663d0010              cmp       ax,1000h
  00462556  7e09                  jle       short 00462561h
  00462558  0500f0ffff            add       eax,0FFFFF000h
  0046255d  66894634              mov       [esi+34h],ax

LAB_00462561:
  00462561  66817e12f500          cmp       word [esi+12h],0F5h
  00462567  7e2b                  jle       short 00462594h
  00462569  b938ffffff            mov       ecx,0FFFFFF38h
  0046256e  66014e2a              add       [esi+2Ah],cx
  00462572  668b462a              mov       ax,[esi+2Ah]
  00462576  66014e28              add       [esi+28h],cx
  0046257a  668b4e28              mov       cx,[esi+28h]
  0046257e  663bc3                cmp       ax,bx
  00462581  7c05                  jl        short 00462588h
  00462583  663bcb                cmp       cx,bx
  00462586  7d0c                  jge       short 00462594h

LAB_00462588:
  00462588  66895e2a              mov       [esi+2Ah],bx
  0046258c  66895e28              mov       [esi+28h],bx
  00462590  66895e10              mov       [esi+10h],bx

LAB_00462594:
  00462594  53                    push      ebx
  00462595  53                    push      ebx
  00462596  53                    push      ebx
  00462597  6a0a                  push      0Ah
  00462599  56                    push      esi
  0046259a  e8a1b4ffff            call      0045DA40h
  0046259f  83c414                add       esp,14h

LAB_004625a2:
  004625a2  5f                    pop       edi
  004625a3  5e                    pop       esi
  004625a4  5b                    pop       ebx
  004625a5  c3                    ret

14 cible(s) de saut interne : 0x004623a5, 0x004623e0, 0x0046241c, 0x00462445, 0x00462481, 0x004624d0, 0x004624df, 0x00462504, 0x00462533, 0x00462546, 0x00462561, 0x00462588, 0x00462594, 0x004625a2
