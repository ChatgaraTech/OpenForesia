C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041bc90  section .text  offset fichier 0x1b090  464 octets

  0041bc90  53                    push      ebx
  0041bc91  56                    push      esi
  0041bc92  8b74240c              mov       esi,[esp+0Ch]
  0041bc96  33db                  xor       ebx,ebx
  0041bc98  57                    push      edi
  0041bc99  66395e10              cmp       [esi+10h],bx
  0041bc9d  7540                  jne       short 0041BCDFh
  0041bc9f  8a4614                mov       al,[esi+14h]
  0041bca2  8b4e64                mov       ecx,[esi+64h]
  0041bca5  884604                mov       [esi+4],al
  0041bca8  b8581b0000            mov       eax,1B58h
  0041bcad  81c100803e00          add       ecx,3E8000h
  0041bcb3  66c746100100          mov       word [esi+10h],1
  0041bcb9  c6460a02              mov       byte [esi+0Ah],2
  0041bcbd  885e0b                mov       [esi+0Bh],bl
  0041bcc0  66c746287017          mov       word [esi+28h],1770h
  0041bcc6  6689462a              mov       [esi+2Ah],ax
  0041bcca  6689462c              mov       [esi+2Ch],ax
  0041bcce  894e20                mov       [esi+20h],ecx
  0041bcd1  66895e12              mov       [esi+12h],bx
  0041bcd5  66895e32              mov       [esi+32h],bx
  0041bcd9  66c74634000c          mov       word [esi+34h],0C00h

LAB_0041bcdf:
  0041bcdf  66837e1001            cmp       word [esi+10h],1
  0041bce4  0f85f9000000          jne       near 0041BDE3h
  0041bcea  668b4612              mov       ax,[esi+12h]
  0041bcee  8b7e20                mov       edi,[esi+20h]
  0041bcf1  0fbfd0                movsx     edx,ax
  0041bcf4  66814632fa00          add       word [esi+32h],0FAh
  0041bcfa  83c00a                add       eax,0Ah
  0041bcfd  c1e20c                shl       edx,0Ch
  0041bd00  66894612              mov       [esi+12h],ax
  0041bd04  8b4664                mov       eax,[esi+64h]
  0041bd07  2bfa                  sub       edi,edx
  0041bd09  0500400100            add       eax,14000h
  0041bd0e  8bcf                  mov       ecx,edi
  0041bd10  897e20                mov       [esi+20h],edi
  0041bd13  3bc8                  cmp       ecx,eax
  0041bd15  0f8dc8000000          jge       near 0041BDE3h
  0041bd1b  894620                mov       [esi+20h],eax
  0041bd1e  8a4604                mov       al,[esi+4]
  0041bd21  3c39                  cmp       al,39h
  0041bd23  66c746100200          mov       word [esi+10h],2
  0041bd29  66895e12              mov       [esi+12h],bx
  0041bd2d  66c746141400          mov       word [esi+14h],14h
  0041bd33  7568                  jne       short 0041BD9Dh
  0041bd35  6800100000            push      1000h
  0041bd3a  6800100000            push      1000h
  0041bd3f  6800020000            push      200h
  0041bd44  6a2e                  push      2Eh
  0041bd46  e855111200            call      0053CEA0h
  0041bd4b  83c410                add       esp,10h
  0041bd4e  6800100000            push      1000h
  0041bd53  6800100000            push      1000h
  0041bd58  682a030000            push      32Ah
  0041bd5d  6a2e                  push      2Eh
  0041bd5f  e83c111200            call      0053CEA0h
  0041bd64  668b7e14              mov       di,[esi+14h]
  0041bd68  83c410                add       esp,10h
  0041bd6b  66c746140700          mov       word [esi+14h],7
  0041bd71  6aff                  push      0FFFFFFFFh
  0041bd73  53                    push      ebx
  0041bd74  68c3000000            push      0C3h
  0041bd79  56                    push      esi
  0041bd7a  e861ed0500            call      0047AAE0h
  0041bd7f  83c410                add       esp,10h
  0041bd82  66c746140100          mov       word [esi+14h],1
  0041bd88  6aff                  push      0FFFFFFFFh
  0041bd8a  53                    push      ebx
  0041bd8b  68c3000000            push      0C3h
  0041bd90  56                    push      esi
  0041bd91  e84aed0500            call      0047AAE0h
  0041bd96  83c410                add       esp,10h
  0041bd99  66897e14              mov       [esi+14h],di

LAB_0041bd9d:
  0041bd9d  8b3e                  mov       edi,[esi]
  0041bd9f  6aff                  push      0FFFFFFFFh
  0041bda1  81cf00400000          or        edi,4000h
  0041bda7  53                    push      ebx
  0041bda8  893e                  mov       [esi],edi
  0041bdaa  8d7e1c                lea       edi,[esi+1Ch]
  0041bdad  68f9000000            push      0F9h
  0041bdb2  53                    push      ebx
  0041bdb3  57                    push      edi
  0041bdb4  56                    push      esi
  0041bdb5  e886ee0500            call      0047AC40h
  0041bdba  83c418                add       esp,18h
  0041bdbd  6aff                  push      0FFFFFFFFh
  0041bdbf  53                    push      ebx
  0041bdc0  68f9000000            push      0F9h
  0041bdc5  53                    push      ebx
  0041bdc6  57                    push      edi
  0041bdc7  56                    push      esi
  0041bdc8  e873ee0500            call      0047AC40h
  0041bdcd  83c418                add       esp,18h
  0041bdd0  6aff                  push      0FFFFFFFFh
  0041bdd2  53                    push      ebx
  0041bdd3  68f9000000            push      0F9h
  0041bdd8  53                    push      ebx
  0041bdd9  57                    push      edi
  0041bdda  56                    push      esi
  0041bddb  e860ee0500            call      0047AC40h
  0041bde0  83c418                add       esp,18h

LAB_0041bde3:
  0041bde3  66837e1002            cmp       word [esi+10h],2
  0041bde8  bf03000000            mov       edi,3
  0041bded  753f                  jne       short 0041BE2Eh
  0041bdef  668b4614              mov       ax,[esi+14h]
  0041bdf3  663bc3                cmp       ax,bx
  0041bdf6  7d03                  jge       short 0041BDFBh
  0041bdf8  40                    inc       eax
  0041bdf9  eb01                  jmp       short 0041BDFCh

LAB_0041bdfb:
  0041bdfb  48                    dec       eax

LAB_0041bdfc:
  0041bdfc  66894614              mov       [esi+14h],ax
  0041be00  53                    push      ebx
  0041be01  66f7d8                neg       ax
  0041be04  66894614              mov       [esi+14h],ax
  0041be08  53                    push      ebx
  0041be09  0fbfc0                movsx     eax,ax
  0041be0c  68e7030000            push      3E7h
  0041be11  50                    push      eax
  0041be12  68e7030000            push      3E7h
  0041be17  56                    push      esi
  0041be18  e843000000            call      0041BE60h
  0041be1d  83c418                add       esp,18h
  0041be20  66395e14              cmp       [esi+14h],bx
  0041be24  7508                  jne       short 0041BE2Eh
  0041be26  66897e10              mov       [esi+10h],di
  0041be2a  66895e12              mov       [esi+12h],bx

LAB_0041be2e:
  0041be2e  66397e10              cmp       [esi+10h],di
  0041be32  7525                  jne       short 0041BE59h
  0041be34  0fbf4e12              movsx     ecx,word [esi+12h]
  0041be38  51                    push      ecx
  0041be39  e8de9b1200            call      00545A1Ch
  0041be3e  668146120001          add       word [esi+12h],100h
  0041be44  c1f804                sar       eax,4
  0041be47  66817e12e02e          cmp       word [esi+12h],2EE0h
  0041be4d  66894632              mov       [esi+32h],ax
  0041be51  7c06                  jl        short 0041BE59h
  0041be53  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041be59:
  0041be59  5f                    pop       edi
  0041be5a  5e                    pop       esi
  0041be5b  5b                    pop       ebx
  0041be5c  c3                    ret
  0041be5d  90                    nop
  0041be5e  90                    nop
  0041be5f  90                    nop

7 cible(s) de saut interne : 0x0041bcdf, 0x0041bd9d, 0x0041bde3, 0x0041bdfb, 0x0041bdfc, 0x0041be2e, 0x0041be59
