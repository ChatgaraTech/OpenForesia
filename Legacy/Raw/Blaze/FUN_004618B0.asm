C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004618b0  section .text  offset fichier 0x60cb0  195 octets

  004618b0  56                    push      esi
  004618b1  57                    push      edi
  004618b2  33f6                  xor       esi,esi
  004618b4  b9c0935e00            mov       ecx,5E93C0h
  004618b9  ba04000000            mov       edx,4

LAB_004618be:
  004618be  8b01                  mov       eax,[ecx]
  004618c0  a900000080            test      eax,80000000h
  004618c5  7408                  je        short 004618CFh
  004618c7  a900000150            test      eax,50010000h
  004618cc  7501                  jne       short 004618CFh
  004618ce  46                    inc       esi

LAB_004618cf:
  004618cf  81c19c000000          add       ecx,9Ch
  004618d5  4a                    dec       edx
  004618d6  75e6                  jne       short 004618BEh
  004618d8  6683fe02              cmp       si,2
  004618dc  8b74240c              mov       esi,[esp+0Ch]
  004618e0  7c22                  jl        short 00461904h
  004618e2  6800000040            push      40000000h
  004618e7  6a10                  push      10h
  004618e9  56                    push      esi
  004618ea  e811370d00            call      00535000h
  004618ef  83c40c                add       esp,0Ch
  004618f2  8bf8                  mov       edi,eax
  004618f4  e877540000            call      00466D70h
  004618f9  663bf8                cmp       di,ax
  004618fc  7506                  jne       short 00461904h
  004618fe  66c746100100          mov       word [esi+10h],1

LAB_00461904:
  00461904  668b4610              mov       ax,[esi+10h]
  00461908  6685c0                test      ax,ax
  0046190b  7504                  jne       short 00461911h
  0046190d  66894612              mov       [esi+12h],ax

LAB_00461911:
  00461911  663d0100              cmp       ax,1
  00461915  7524                  jne       short 0046193Bh
  00461917  8b5620                mov       edx,[esi+20h]
  0046191a  81c200b0ffff          add       edx,0FFFFB000h
  00461920  66ff4612              inc       word [esi+12h]
  00461924  66837e1223            cmp       word [esi+12h],23h
  00461929  895620                mov       [esi+20h],edx
  0046192c  7e0d                  jle       short 0046193Bh
  0046192e  8b06                  mov       eax,[esi]
  00461930  66c746100200          mov       word [esi+10h],2
  00461936  80cc20                or        ah,20h
  00461939  8906                  mov       [esi],eax

LAB_0046193b:
  0046193b  66837e1002            cmp       word [esi+10h],2
  00461940  752e                  jne       short 00461970h
  00461942  6683463232            add       word [esi+32h],32h
  00461947  b99cffffff            mov       ecx,0FFFFFF9Ch
  0046194c  66014e28              add       [esi+28h],cx
  00461950  668b4628              mov       ax,[esi+28h]
  00461954  66014e2c              add       [esi+2Ch],cx
  00461958  6685c0                test      ax,ax
  0046195b  7d13                  jge       short 00461970h
  0046195d  8b06                  mov       eax,[esi]
  0046195f  66c746280000          mov       word [esi+28h],0
  00461965  80cc40                or        ah,40h
  00461968  66c7462c0000          mov       word [esi+2Ch],0
  0046196e  8906                  mov       [esi],eax

LAB_00461970:
  00461970  5f                    pop       edi
  00461971  5e                    pop       esi
  00461972  c3                    ret

6 cible(s) de saut interne : 0x004618be, 0x004618cf, 0x00461904, 0x00461911, 0x0046193b, 0x00461970
