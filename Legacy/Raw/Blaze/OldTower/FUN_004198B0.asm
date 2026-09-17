C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004198b0  section .text  offset fichier 0x18cb0  368 octets

  004198b0  53                    push      ebx
  004198b1  56                    push      esi
  004198b2  8b74240c              mov       esi,[esp+0Ch]
  004198b6  33db                  xor       ebx,ebx
  004198b8  57                    push      edi
  004198b9  bf01000000            mov       edi,1
  004198be  66395e10              cmp       [esi+10h],bx
  004198c2  7563                  jne       short 00419927h
  004198c4  66897e10              mov       [esi+10h],di
  004198c8  c7463c00fc5400        mov       dword [esi+3Ch],54FC00h
  004198cf  66895e28              mov       [esi+28h],bx
  004198d3  66895e2a              mov       [esi+2Ah],bx
  004198d7  c6460a09              mov       byte [esi+0Ah],9
  004198db  885e0b                mov       [esi+0Bh],bl
  004198de  e85dd81200            call      00547140h
  004198e3  99                    cdq
  004198e4  b9c8000000            mov       ecx,0C8h
  004198e9  66c746140a00          mov       word [esi+14h],0Ah
  004198ef  f7f9                  idiv      ecx
  004198f1  8b4620                mov       eax,[esi+20h]
  004198f4  8b4e24                mov       ecx,[esi+24h]
  004198f7  894654                mov       [esi+54h],eax
  004198fa  894e58                mov       [esi+58h],ecx
  004198fd  885e40                mov       [esi+40h],bl
  00419900  81c2fa000000          add       edx,0FAh
  00419906  66895612              mov       [esi+12h],dx
  0041990a  8b561c                mov       edx,[esi+1Ch]
  0041990d  895650                mov       [esi+50h],edx
  00419910  e82bd81200            call      00547140h
  00419915  99                    cdq
  00419916  33c2                  xor       eax,edx
  00419918  2bc2                  sub       eax,edx
  0041991a  25ff0f0000            and       eax,0FFFh
  0041991f  33c2                  xor       eax,edx
  00419921  2bc2                  sub       eax,edx
  00419923  66894616              mov       [esi+16h],ax

LAB_00419927:
  00419927  66397e10              cmp       [esi+10h],di
  0041992b  0f85e3000000          jne       near 00419A14h
  00419931  b9f4010000            mov       ecx,1F4h
  00419936  ba00100000            mov       edx,1000h
  0041993b  66014e28              add       [esi+28h],cx
  0041993f  668b4628              mov       ax,[esi+28h]
  00419943  663bc2                cmp       ax,dx
  00419946  7e04                  jle       short 0041994Ch
  00419948  66895628              mov       [esi+28h],dx

LAB_0041994c:
  0041994c  66014e2a              add       [esi+2Ah],cx
  00419950  6639562a              cmp       [esi+2Ah],dx
  00419954  7e04                  jle       short 0041995Ah
  00419956  6689562a              mov       [esi+2Ah],dx

LAB_0041995a:
  0041995a  0fbf5616              movsx     edx,word [esi+16h]
  0041995e  52                    push      edx
  0041995f  e8b8c01200            call      00545A1Ch
  00419964  0fbf4e12              movsx     ecx,word [esi+12h]
  00419968  c1f902                sar       ecx,2
  0041996b  0fafc1                imul      eax,ecx
  0041996e  0fbf5632              movsx     edx,word [esi+32h]
  00419972  8b4e50                mov       ecx,[esi+50h]
  00419975  52                    push      edx
  00419976  03c1                  add       eax,ecx
  00419978  89461c                mov       [esi+1Ch],eax
  0041997b  e896c01200            call      00545A16h
  00419980  0fbf4e12              movsx     ecx,word [esi+12h]
  00419984  0fafc1                imul      eax,ecx
  00419987  0fbf5616              movsx     edx,word [esi+16h]
  0041998b  8b4e54                mov       ecx,[esi+54h]
  0041998e  668b7e12              mov       di,[esi+12h]
  00419992  03c1                  add       eax,ecx
  00419994  52                    push      edx
  00419995  894620                mov       [esi+20h],eax
  00419998  e879c01200            call      00545A16h
  0041999d  0fbfcf                movsx     ecx,di
  004199a0  c1f902                sar       ecx,2
  004199a3  0fafc1                imul      eax,ecx
  004199a6  034658                add       eax,[esi+58h]
  004199a9  894624                mov       [esi+24h],eax
  004199ac  668b4614              mov       ax,[esi+14h]
  004199b0  668bd0                mov       dx,ax
  004199b3  66d1fa                sar       dx,1
  004199b6  2bfa                  sub       edi,edx
  004199b8  40                    inc       eax
  004199b9  66894614              mov       [esi+14h],ax
  004199bd  668b4616              mov       ax,[esi+16h]
  004199c1  66056400              add       ax,64h
  004199c5  66897e12              mov       [esi+12h],di
  004199c9  25ff0f0000            and       eax,0FFFh
  004199ce  66894616              mov       [esi+16h],ax
  004199d2  e869d71200            call      00547140h
  004199d7  99                    cdq
  004199d8  b906000000            mov       ecx,6
  004199dd  f7f9                  idiv      ecx
  004199df  8b4e1c                mov       ecx,[esi+1Ch]
  004199e2  83ea03                sub       edx,3
  004199e5  c1e20c                shl       edx,0Ch
  004199e8  03ca                  add       ecx,edx
  004199ea  894e1c                mov       [esi+1Ch],ecx
  004199ed  e84ed71200            call      00547140h
  004199f2  99                    cdq
  004199f3  b906000000            mov       ecx,6
  004199f8  f7f9                  idiv      ecx
  004199fa  8b4e24                mov       ecx,[esi+24h]
  004199fd  83ea03                sub       edx,3
  00419a00  c1e20c                shl       edx,0Ch
  00419a03  03ca                  add       ecx,edx
  00419a05  66395e12              cmp       [esi+12h],bx
  00419a09  894e24                mov       [esi+24h],ecx
  00419a0c  7d06                  jge       short 00419A14h
  00419a0e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00419a14:
  00419a14  5f                    pop       edi
  00419a15  5e                    pop       esi
  00419a16  5b                    pop       ebx
  00419a17  c3                    ret
  00419a18  90                    nop
  00419a19  90                    nop
  00419a1a  90                    nop
  00419a1b  90                    nop
  00419a1c  90                    nop
  00419a1d  90                    nop
  00419a1e  90                    nop
  00419a1f  90                    nop

4 cible(s) de saut interne : 0x00419927, 0x0041994c, 0x0041995a, 0x00419a14
