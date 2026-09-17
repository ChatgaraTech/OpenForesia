C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00419030  section .text  offset fichier 0x18430  560 octets

  00419030  53                    push      ebx
  00419031  56                    push      esi
  00419032  8b74240c              mov       esi,[esp+0Ch]
  00419036  33db                  xor       ebx,ebx
  00419038  66395e10              cmp       [esi+10h],bx
  0041903c  7576                  jne       short 004190B4h
  0041903e  0fbf4632              movsx     eax,word [esi+32h]
  00419042  50                    push      eax
  00419043  66c746100100          mov       word [esi+10h],1
  00419049  c7463c48fc5400        mov       dword [esi+3Ch],54FC48h
  00419050  885e40                mov       [esi+40h],bl
  00419053  e8c4c91200            call      00545A1Ch
  00419058  8bc8                  mov       ecx,eax
  0041905a  8b561c                mov       edx,[esi+1Ch]
  0041905d  f7d9                  neg       ecx
  0041905f  c1e102                shl       ecx,2
  00419062  2bc8                  sub       ecx,eax
  00419064  c1e104                shl       ecx,4
  00419067  03d1                  add       edx,ecx
  00419069  89561c                mov       [esi+1Ch],edx
  0041906c  0fbf5632              movsx     edx,word [esi+32h]
  00419070  52                    push      edx
  00419071  e8a0c91200            call      00545A16h
  00419076  8bc8                  mov       ecx,eax
  00419078  8b5624                mov       edx,[esi+24h]
  0041907b  f7d9                  neg       ecx
  0041907d  c1e102                shl       ecx,2
  00419080  2bc8                  sub       ecx,eax
  00419082  8b4620                mov       eax,[esi+20h]
  00419085  c1e104                shl       ecx,4
  00419088  03d1                  add       edx,ecx
  0041908a  0500000500            add       eax,50000h
  0041908f  894620                mov       [esi+20h],eax
  00419092  b800080000            mov       eax,800h
  00419097  895624                mov       [esi+24h],edx
  0041909a  66894628              mov       [esi+28h],ax
  0041909e  6689462a              mov       [esi+2Ah],ax
  004190a2  66895e12              mov       [esi+12h],bx
  004190a6  c6460a0a              mov       byte [esi+0Ah],0Ah
  004190aa  885e0b                mov       [esi+0Bh],bl
  004190ad  66899e9a000000        mov       [esi+9Ah],bx

LAB_004190b4:
  004190b4  66837e1001            cmp       word [esi+10h],1
  004190b9  0f8587000000          jne       near 00419146h
  004190bf  0fbf5632              movsx     edx,word [esi+32h]
  004190c3  52                    push      edx
  004190c4  e853c91200            call      00545A1Ch
  004190c9  8b561c                mov       edx,[esi+1Ch]
  004190cc  8bc8                  mov       ecx,eax
  004190ce  c1e104                shl       ecx,4
  004190d1  2bc8                  sub       ecx,eax
  004190d3  f7d9                  neg       ecx
  004190d5  03d1                  add       edx,ecx
  004190d7  89561c                mov       [esi+1Ch],edx
  004190da  0fbf5632              movsx     edx,word [esi+32h]
  004190de  52                    push      edx
  004190df  e832c91200            call      00545A16h
  004190e4  8b5624                mov       edx,[esi+24h]
  004190e7  8bc8                  mov       ecx,eax
  004190e9  c1e104                shl       ecx,4
  004190ec  2bc8                  sub       ecx,eax
  004190ee  f7d9                  neg       ecx
  004190f0  03d1                  add       edx,ecx
  004190f2  66ff4612              inc       word [esi+12h]
  004190f6  66837e120a            cmp       word [esi+12h],0Ah
  004190fb  895624                mov       [esi+24h],edx
  004190fe  7c46                  jl        short 00419146h
  00419100  66c746100200          mov       word [esi+10h],2
  00419106  66895e12              mov       [esi+12h],bx
  0041910a  c7465000808900        mov       dword [esi+50h],898000h
  00419111  c7465400401a00        mov       dword [esi+54h],1A4000h
  00419118  895e58                mov       [esi+58h],ebx
  0041911b  e820e01200            call      00547140h
  00419120  99                    cdq
  00419121  33c2                  xor       eax,edx
  00419123  2bc2                  sub       eax,edx
  00419125  25ff0f0000            and       eax,0FFFh
  0041912a  33c2                  xor       eax,edx
  0041912c  2bc2                  sub       eax,edx
  0041912e  66894614              mov       [esi+14h],ax
  00419132  e809e01200            call      00547140h
  00419137  99                    cdq
  00419138  b964000000            mov       ecx,64h
  0041913d  f7f9                  idiv      ecx
  0041913f  83c232                add       edx,32h
  00419142  66895616              mov       [esi+16h],dx

LAB_00419146:
  00419146  66837e1002            cmp       word [esi+10h],2
  0041914b  0f8585000000          jne       near 004191D6h
  00419151  0fbf5614              movsx     edx,word [esi+14h]
  00419155  52                    push      edx
  00419156  e8c1c81200            call      00545A1Ch
  0041915b  8b4e50                mov       ecx,[esi+50h]
  0041915e  8d0440                lea       eax,[eax+eax*2]
  00419161  8d0480                lea       eax,[eax+eax*4]
  00419164  8d0480                lea       eax,[eax+eax*4]
  00419167  8d1441                lea       edx,[ecx+eax*2]
  0041916a  0fbf4614              movsx     eax,word [esi+14h]
  0041916e  50                    push      eax
  0041916f  89561c                mov       [esi+1Ch],edx
  00419172  e89fc81200            call      00545A16h
  00419177  8b5658                mov       edx,[esi+58h]
  0041917a  8d0440                lea       eax,[eax+eax*2]
  0041917d  8d0480                lea       eax,[eax+eax*4]
  00419180  8d0c80                lea       ecx,[eax+eax*4]
  00419183  8d044a                lea       eax,[edx+ecx*2]
  00419186  668b4e16              mov       cx,[esi+16h]
  0041918a  66014e14              add       [esi+14h],cx
  0041918e  894624                mov       [esi+24h],eax
  00419191  8b4620                mov       eax,[esi+20h]
  00419194  0500800000            add       eax,8000h
  00419199  894620                mov       [esi+20h],eax
  0041919c  668b4614              mov       ax,[esi+14h]
  004191a0  663d0010              cmp       ax,1000h
  004191a4  7c09                  jl        short 004191AFh
  004191a6  0500f0ffff            add       eax,0FFFFF000h
  004191ab  66894614              mov       [esi+14h],ax

LAB_004191af:
  004191af  66ff4612              inc       word [esi+12h]
  004191b3  66837e121c            cmp       word [esi+12h],1Ch
  004191b8  7c1c                  jl        short 004191D6h
  004191ba  b960ffffff            mov       ecx,0FFFFFF60h
  004191bf  66014e28              add       [esi+28h],cx
  004191c3  668b4628              mov       ax,[esi+28h]
  004191c7  66014e2a              add       [esi+2Ah],cx
  004191cb  663bc3                cmp       ax,bx
  004191ce  7f06                  jg        short 004191D6h
  004191d0  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004191d6:
  004191d6  66ff869a000000        inc       word [esi+9Ah]
  004191dd  6683be9a00000003      cmp       word [esi+9Ah],3
  004191e5  7c18                  jl        short 004191FFh
  004191e7  8a4e40                mov       cl,[esi+40h]
  004191ea  66899e9a000000        mov       [esi+9Ah],bx
  004191f1  fec1                  inc       cl
  004191f3  8ac1                  mov       al,cl
  004191f5  884e40                mov       [esi+40h],cl
  004191f8  3c04                  cmp       al,4
  004191fa  7503                  jne       short 004191FFh
  004191fc  885e40                mov       [esi+40h],bl

LAB_004191ff:
  004191ff  e83cdf1200            call      00547140h
  00419204  99                    cdq
  00419205  b914000000            mov       ecx,14h
  0041920a  f7f9                  idiv      ecx
  0041920c  8b461c                mov       eax,[esi+1Ch]
  0041920f  83ea0a                sub       edx,0Ah
  00419212  c1e20c                shl       edx,0Ch
  00419215  03c2                  add       eax,edx
  00419217  89461c                mov       [esi+1Ch],eax
  0041921a  e821df1200            call      00547140h
  0041921f  99                    cdq
  00419220  b914000000            mov       ecx,14h
  00419225  f7f9                  idiv      ecx
  00419227  8b4620                mov       eax,[esi+20h]
  0041922a  83ea0a                sub       edx,0Ah
  0041922d  c1e20c                shl       edx,0Ch
  00419230  03c2                  add       eax,edx
  00419232  894620                mov       [esi+20h],eax
  00419235  e806df1200            call      00547140h
  0041923a  99                    cdq
  0041923b  b914000000            mov       ecx,14h
  00419240  f7f9                  idiv      ecx
  00419242  8b4624                mov       eax,[esi+24h]
  00419245  83ea0a                sub       edx,0Ah
  00419248  c1e20c                shl       edx,0Ch
  0041924b  03c2                  add       eax,edx
  0041924d  894624                mov       [esi+24h],eax
  00419250  5e                    pop       esi
  00419251  5b                    pop       ebx
  00419252  c3                    ret
  00419253  90                    nop
  00419254  90                    nop
  00419255  90                    nop
  00419256  90                    nop
  00419257  90                    nop
  00419258  90                    nop
  00419259  90                    nop
  0041925a  90                    nop
  0041925b  90                    nop
  0041925c  90                    nop
  0041925d  90                    nop
  0041925e  90                    nop
  0041925f  90                    nop

5 cible(s) de saut interne : 0x004190b4, 0x00419146, 0x004191af, 0x004191d6, 0x004191ff
